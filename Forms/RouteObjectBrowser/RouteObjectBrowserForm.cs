using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;
using MapEditor.Forms.Base;
using MapEditor.Forms.RouteObjectBrowser.ShipComposer;
using MapEditor.Map.Containers;
using MapEditor.Map.MapObjects;
using MapEditor.Map.SaveLoad.DataSources;
using MapEditor.Scene;
using Tools.Geometry;
using Tools.LinkContainer;
using Tools.MapObjects;

namespace MapEditor.Forms.RouteObjectBrowser
{
	// Token: 0x02000186 RID: 390
	public partial class RouteObjectBrowserForm : BaseForm
	{
		// Token: 0x0600128E RID: 4750 RVA: 0x000871DE File Offset: 0x000861DE
		private static void SafeTrackBarSetValue(TrackBar trackBar, int value)
		{
			if (trackBar != null)
			{
				trackBar.Value = Math.Max(trackBar.Minimum, Math.Min(trackBar.Maximum, value));
			}
		}

		// Token: 0x0600128F RID: 4751 RVA: 0x00087200 File Offset: 0x00086200
		private void UpdateControls()
		{
			if (this.created)
			{
				this.created = false;
				bool enable = this.mapObjectContainer != null;
				this.ShowCheckBox.Enabled = enable;
				this.SpeedEditBox.Enabled = enable;
				this.SpeedTrackBar.Enabled = enable;
				this.AnimateRouteObjectCheckBox.Enabled = enable;
				this.PathTrackBar.Enabled = enable;
				this.PathEditBox.Enabled = enable;
				this.PointEditBox.Enabled = enable;
				this.created = true;
				this.UpdateShowRouteObjectsControls();
				this.UpdateCameraButtons();
			}
		}

		// Token: 0x06001290 RID: 4752 RVA: 0x00087290 File Offset: 0x00086290
		private void UpdateShowRouteObjectsControls()
		{
			if (this.mapObjectContainer != null && this.created)
			{
				this.created = false;
				RouteObject routeObject = this.GetSelectedRouteObject();
				bool enable = this.mapSceneParams != null && this.mapSceneParams.ShowRouteObjects;
				this.ShowCheckBox.Checked = enable;
				enable = (enable && routeObject != null);
				this.SpeedEditBox.Enabled = enable;
				this.SpeedTrackBar.Enabled = enable;
				this.AnimateRouteObjectCheckBox.Enabled = enable;
				this.PathTrackBar.Enabled = enable;
				this.PathEditBox.Enabled = enable;
				this.PointEditBox.Enabled = enable;
				this.created = true;
				this.UpdateRouteObjectControls();
			}
		}

		// Token: 0x06001291 RID: 4753 RVA: 0x00087348 File Offset: 0x00086348
		private void UpdateRouteObjectControls()
		{
			if (this.mapObjectContainer != null && this.created)
			{
				this.created = false;
				RouteObject routeObject = this.GetSelectedRouteObject();
				if (routeObject != null)
				{
					bool enable = routeObject.Run;
					this.AnimateRouteObjectCheckBox.Checked = enable;
					this.SpeedEditBox.Enabled = (this.AnimateRouteObjectCheckBox.Enabled && enable);
					this.SpeedTrackBar.Enabled = (this.AnimateRouteObjectCheckBox.Enabled && enable);
					this.PathTrackBar.Enabled = (this.AnimateRouteObjectCheckBox.Enabled && !enable);
					this.PathEditBox.Enabled = (this.AnimateRouteObjectCheckBox.Enabled && !enable);
					this.PointEditBox.Enabled = (this.AnimateRouteObjectCheckBox.Enabled && !enable);
				}
				this.created = true;
				this.UpdateSpeed();
				this.UpdatePath();
			}
		}

		// Token: 0x06001292 RID: 4754 RVA: 0x0008743C File Offset: 0x0008643C
		private void UpdateSpeed()
		{
			if (this.mapObjectContainer != null && this.created)
			{
				this.created = false;
				RouteObject routeObject = this.GetSelectedRouteObject();
				if (routeObject != null)
				{
					this.SpeedEditBox.Text = routeObject.Speed.ToString();
					RouteObjectBrowserForm.SafeTrackBarSetValue(this.SpeedTrackBar, (int)routeObject.Speed);
				}
				this.created = true;
			}
		}

		// Token: 0x06001293 RID: 4755 RVA: 0x0008749C File Offset: 0x0008649C
		private void UpdatePath()
		{
			if (this.mapObjectContainer != null && this.created)
			{
				this.created = false;
				RouteObject routeObject = this.GetSelectedRouteObject();
				if (routeObject != null)
				{
					if (routeObject.Length > MathConsts.DOUBLE_EPSILON)
					{
						double time = SplinePath.NormalizeTime(routeObject.Time, routeObject.Length, routeObject.Circle);
						this.PathEditBox.Text = string.Format("{0:0.###}", time);
						RouteObjectBrowserForm.SafeTrackBarSetValue(this.PathTrackBar, (int)(time * 100.0 / routeObject.Length));
						this.PointEditBox.Text = string.Format("{0:0.###}", routeObject.GetPointByTime(time));
					}
					else
					{
						this.PathEditBox.Text = "0";
						this.PointEditBox.Text = "0";
						RouteObjectBrowserForm.SafeTrackBarSetValue(this.PathTrackBar, 0);
					}
				}
				this.created = true;
			}
		}

		// Token: 0x06001294 RID: 4756 RVA: 0x00087588 File Offset: 0x00086588
		private void UpdateCameraButtons()
		{
			if (!this.created)
			{
				this.StartCameraButton.Enabled = false;
				this.SetObjectButton.Enabled = false;
				this.StopCameraButton.Enabled = false;
				return;
			}
			if (this.cameraController != null || this.CameraTimer.Enabled)
			{
				this.StartCameraButton.Enabled = false;
				this.SetObjectButton.Enabled = false;
				this.StopCameraButton.Enabled = true;
				return;
			}
			this.StartCameraButton.Enabled = (this.mapObjectContainer != null && (this.RoutesListView.SelectedItems.Count == 1 || this.RoutesListView.SelectedItems.Count == 2));
			this.SetObjectButton.Enabled = (this.selector != null && this.selector.MapObjects.Count > 0);
			this.StopCameraButton.Enabled = false;
		}

		// Token: 0x06001295 RID: 4757 RVA: 0x00087673 File Offset: 0x00086673
		private void OnShowRouteObjectsChanged(MapSceneParams _mapSceneParams, ref bool oldValue, ref bool newValue)
		{
			this.UpdateShowRouteObjectsControls();
			if (this.mapObjectContainer != null && this.mapSceneParams != null && this.mapSceneParams.ShowRouteObjects)
			{
				this.FillRouteObjectList();
			}
		}

		// Token: 0x06001296 RID: 4758 RVA: 0x0008769E File Offset: 0x0008669E
		private void ShowCheckBox_CheckedChanged(object sender, EventArgs e)
		{
			if (this.mapSceneParams != null && this.created)
			{
				this.mapSceneParams.ShowRouteObjects = this.ShowCheckBox.Checked;
			}
		}

		// Token: 0x06001297 RID: 4759 RVA: 0x000876C8 File Offset: 0x000866C8
		private void RouteComposerButton_Click(object sender, EventArgs e)
		{
			if (this.created)
			{
				ShipComposerForm shipComposerForm = new ShipComposerForm();
				string ship = string.Empty;
				RouteObject routeObject = this.GetSelectedRouteObject();
				if (routeObject != null)
				{
					ship = RoutePoint.GetShipResource(routeObject.Route);
				}
				shipComposerForm.Ship = ship;
				shipComposerForm.ShowDialog(this);
			}
		}

		// Token: 0x06001298 RID: 4760 RVA: 0x00087710 File Offset: 0x00086710
		private void StartCameraButton_Click(object sender, EventArgs e)
		{
			if (this.created)
			{
				this.FillRouteObjectList();
				ListViewItem cameraItem = null;
				ListViewItem targetItem = null;
				if (this.RoutesListView.SelectedItems.Count == 1)
				{
					cameraItem = this.RoutesListView.SelectedItems[0];
				}
				else if (this.RoutesListView.SelectedItems.Count == 2)
				{
					if (this.RoutesListView.FocusedItem == this.RoutesListView.SelectedItems[0])
					{
						cameraItem = this.RoutesListView.SelectedItems[1];
						targetItem = this.RoutesListView.SelectedItems[0];
					}
					else
					{
						cameraItem = this.RoutesListView.SelectedItems[0];
						targetItem = this.RoutesListView.SelectedItems[1];
					}
				}
				RouteObject cameraRouteObject = null;
				if (cameraItem != null)
				{
					cameraRouteObject = (cameraItem.Tag as RouteObject);
				}
				RouteObject targetRouteObject = null;
				if (targetItem != null)
				{
					targetRouteObject = (targetItem.Tag as RouteObject);
				}
				this.cameraController = new RouteObjectBrowserForm.CameraController(base.Context.EditorScene, base.Context.EditorSceneViewID, cameraRouteObject, targetRouteObject);
				if (this.cameraController.Start(Environment.TickCount))
				{
					this.CameraTimer.Interval = RouteObjectBrowserForm.CameraController.StartDelay;
					this.CameraTimer.Start();
				}
				else
				{
					this.cameraController.Stop(false);
					this.cameraController = null;
				}
				this.UpdateCameraButtons();
			}
		}

		// Token: 0x06001299 RID: 4761 RVA: 0x00087861 File Offset: 0x00086861
		private void StopCameraButton_Click(object sender, EventArgs e)
		{
			if (this.created)
			{
				this.CameraTimer.Stop();
				if (this.cameraController != null)
				{
					this.cameraController.Stop(false);
					this.cameraController = null;
				}
				this.UpdateCameraButtons();
			}
		}

		// Token: 0x0600129A RID: 4762 RVA: 0x00087898 File Offset: 0x00086898
		private void SetObjectButton_Click(object sender, EventArgs e)
		{
			if (this.created && this.selector != null)
			{
				double distance;
				if (double.TryParse(this.SetObjectDistanceTextBox.Text, out distance))
				{
					if (distance < 0.0)
					{
						distance = 0.0;
					}
					else if (distance > 64.0)
					{
						distance = 64.0;
					}
				}
				else
				{
					distance = 0.0;
				}
				CameraPlacement cameraPlacement;
				base.Context.EditorScene.GetPlacement(base.Context.EditorSceneViewID, out cameraPlacement);
				bool transactionInProgress = false;
				if (base.Context.OperationContainer != null)
				{
					transactionInProgress = base.Context.OperationContainer.DoesTransactionInProgress;
					if (!transactionInProgress)
					{
						base.Context.OperationContainer.BeginTransaction();
					}
				}
				if (distance > MathConsts.DOUBLE_EPSILON)
				{
					Quat quat = new Quat(cameraPlacement.Rotation);
					Vec3 axisX;
					Vec3 axisY;
					Vec3 axisZ;
					quat.GetPivot(out axisX, out axisY, out axisZ);
					this.selector.Position = cameraPlacement.Position + new Position(axisX * distance);
				}
				else
				{
					this.selector.Position = cameraPlacement.Position;
				}
				if (this.SetObjectRotationCheckBox.Checked)
				{
					this.selector.Rotation = cameraPlacement.Rotation;
				}
				if (base.Context.OperationContainer != null && !transactionInProgress)
				{
					base.Context.OperationContainer.EndTransaction();
				}
			}
		}

		// Token: 0x0600129B RID: 4763 RVA: 0x000879F6 File Offset: 0x000869F6
		private void UpdateRoutesButton_Click(object sender, EventArgs e)
		{
			if (this.mapObjectContainer != null && this.created)
			{
				this.FillRouteObjectList();
			}
		}

		// Token: 0x0600129C RID: 4764 RVA: 0x00087A0E File Offset: 0x00086A0E
		private void RoutesListView_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
		{
			this.UpdateShowRouteObjectsControls();
			this.UpdateCameraButtons();
		}

		// Token: 0x0600129D RID: 4765 RVA: 0x00087A1C File Offset: 0x00086A1C
		private void RoutesListView_MouseDoubleClick(object sender, MouseEventArgs e)
		{
			RouteObject routeObject = this.GetSelectedRouteObject();
			if (base.Context != null && routeObject != null)
			{
				base.Context.SelectExistingObjectInDatabaseEditor(routeObject.Route);
			}
		}

		// Token: 0x0600129E RID: 4766 RVA: 0x00087A4C File Offset: 0x00086A4C
		private void AnimateRouteObjectCheckBox_CheckedChanged(object sender, EventArgs e)
		{
			if (this.created)
			{
				this.UpdateSelectedRouteObjects();
				foreach (RouteObject routeObject in this.selectedRouteObjects)
				{
					routeObject.Run = this.AnimateRouteObjectCheckBox.Checked;
				}
				this.UpdateRouteObjectControls();
			}
		}

		// Token: 0x0600129F RID: 4767 RVA: 0x00087AC0 File Offset: 0x00086AC0
		private void SpeedEditBox_TextChanged(object sender, EventArgs e)
		{
			if (this.created)
			{
				double speed;
				if (double.TryParse(this.SpeedEditBox.Text, out speed))
				{
					this.created = false;
					this.UpdateSelectedRouteObjects();
					foreach (RouteObject routeObject in this.selectedRouteObjects)
					{
						routeObject.Speed = speed;
					}
					this.created = true;
				}
				this.editBoxForUpdateNames[this.SpeedEditBox.Name] = 0;
				this.EditBoxTimer.Start();
			}
		}

		// Token: 0x060012A0 RID: 4768 RVA: 0x00087B68 File Offset: 0x00086B68
		private void PathEditBox_TextChanged(object sender, EventArgs e)
		{
			if (this.created)
			{
				double time;
				if (double.TryParse(this.PathEditBox.Text, out time))
				{
					this.created = false;
					this.UpdateSelectedRouteObjects();
					foreach (RouteObject routeObject in this.selectedRouteObjects)
					{
						if (Math.Abs(time - routeObject.Length) < RouteObjectBrowserForm.path_epsilon)
						{
							time = routeObject.Length - RouteObjectBrowserForm.path_epsilon;
						}
						routeObject.Time = time;
					}
					this.created = true;
				}
				this.editBoxForUpdateNames[this.PathEditBox.Name] = 0;
				this.EditBoxTimer.Start();
			}
		}

		// Token: 0x060012A1 RID: 4769 RVA: 0x00087C34 File Offset: 0x00086C34
		private void PointEditBox_TextChanged(object sender, EventArgs e)
		{
			if (this.created)
			{
				double point;
				if (double.TryParse(this.PointEditBox.Text, out point))
				{
					this.created = false;
					this.UpdateSelectedRouteObjects();
					foreach (RouteObject routeObject in this.selectedRouteObjects)
					{
						double time = routeObject.GetTimeByPoint(point);
						if (Math.Abs(time - routeObject.Length) < RouteObjectBrowserForm.path_epsilon)
						{
							time = routeObject.Length - RouteObjectBrowserForm.path_epsilon;
						}
						routeObject.Time = time;
					}
					this.created = true;
				}
				this.editBoxForUpdateNames[this.PointEditBox.Name] = 0;
				this.EditBoxTimer.Start();
			}
		}

		// Token: 0x060012A2 RID: 4770 RVA: 0x00087D08 File Offset: 0x00086D08
		private void SpeedTrackBar_Scroll(object sender, EventArgs e)
		{
			if (this.created)
			{
				this.created = false;
				this.UpdateSelectedRouteObjects();
				foreach (RouteObject routeObject in this.selectedRouteObjects)
				{
					routeObject.Speed = (double)this.SpeedTrackBar.Value;
				}
				this.created = true;
				this.UpdateSpeed();
			}
		}

		// Token: 0x060012A3 RID: 4771 RVA: 0x00087D88 File Offset: 0x00086D88
		private void PathTrackBar_Scroll(object sender, EventArgs e)
		{
			if (this.created)
			{
				this.created = false;
				this.UpdateSelectedRouteObjects();
				foreach (RouteObject routeObject in this.selectedRouteObjects)
				{
					double time = (double)this.PathTrackBar.Value * routeObject.Length / 100.0;
					if (Math.Abs(time - routeObject.Length) < RouteObjectBrowserForm.path_epsilon)
					{
						time = routeObject.Length - RouteObjectBrowserForm.path_epsilon;
					}
					routeObject.Time = time;
				}
				this.created = true;
				this.UpdatePath();
			}
		}

		// Token: 0x060012A4 RID: 4772 RVA: 0x00087E40 File Offset: 0x00086E40
		private static void FillListRouteObjectViewItem(ListViewItem listViewItem, RouteObject routeObject, bool selected)
		{
			if (listViewItem != null && routeObject != null)
			{
				listViewItem.Tag = routeObject;
				listViewItem.Text = Str.CutFilePath(routeObject.Route);
				listViewItem.Selected = selected;
				if (listViewItem.SubItems.Count < 5)
				{
					listViewItem.SubItems.Add(string.Empty);
					listViewItem.SubItems.Add(string.Empty);
					listViewItem.SubItems.Add(string.Empty);
					listViewItem.SubItems.Add(string.Empty);
				}
				listViewItem.SubItems[1].Text = routeObject.Count.ToString();
				listViewItem.SubItems[2].Text = (routeObject.Circle ? "*" : string.Empty);
				listViewItem.SubItems[3].Text = routeObject.Route;
				listViewItem.SubItems[4].Text = routeObject.SceneName;
			}
		}

		// Token: 0x060012A5 RID: 4773 RVA: 0x00087F3C File Offset: 0x00086F3C
		private RouteObject GetSelectedRouteObject()
		{
			int count = this.RoutesListView.Items.Count;
			for (int index = 0; index < count; index++)
			{
				if (this.RoutesListView.Items[index].Selected)
				{
					return this.RoutesListView.Items[index].Tag as RouteObject;
				}
			}
			return null;
		}

		// Token: 0x060012A6 RID: 4774 RVA: 0x00087F9C File Offset: 0x00086F9C
		private void UpdateSelectedRouteObjects()
		{
			this.selectedRouteObjects.Clear();
			int count = this.RoutesListView.Items.Count;
			for (int index = 0; index < count; index++)
			{
				if (this.RoutesListView.Items[index].Selected)
				{
					RouteObject routeObject = this.RoutesListView.Items[index].Tag as RouteObject;
					if (routeObject != null)
					{
						this.selectedRouteObjects.Add(routeObject);
					}
				}
			}
		}

		// Token: 0x060012A7 RID: 4775 RVA: 0x00088014 File Offset: 0x00087014
		private void ClearRouteObjectList()
		{
			this.RoutesListView.Items.Clear();
			if (this.mapObjectContainer != null)
			{
				Dictionary<int, IMapObject> mapObjectToDelete = new Dictionary<int, IMapObject>();
				foreach (KeyValuePair<int, IMapObject> keyValuePair in this.mapObjectContainer.RouteObjectContainer.MapObjects)
				{
					mapObjectToDelete.Add(keyValuePair.Key, keyValuePair.Value);
				}
				foreach (KeyValuePair<int, IMapObject> keyValuePair2 in mapObjectToDelete)
				{
					this.mapObjectContainer.RemoveMapObject(keyValuePair2.Value);
				}
				mapObjectToDelete.Clear();
			}
		}

		// Token: 0x060012A8 RID: 4776 RVA: 0x000880F0 File Offset: 0x000870F0
		private void FillRouteObjectList()
		{
			this.needUpdate = false;
			this.routeObjectsParams.Clear();
			if (this.mapObjectContainer != null)
			{
				int count = this.RoutesListView.Items.Count;
				for (int index = 0; index < count; index++)
				{
					RouteObject routeObject = this.RoutesListView.Items[index].Tag as RouteObject;
					if (routeObject != null)
					{
						RouteObjectBrowserForm.RouteObjectParams routeObjectParams = new RouteObjectBrowserForm.RouteObjectParams(routeObject, this.RoutesListView.Items[index].Selected, this.RoutesListView.Items[index].Focused);
						this.routeObjectsParams[routeObject.Route] = routeObjectParams;
					}
				}
			}
			this.RoutesListView.Items.Clear();
			if (this.mapObjectContainer != null && this.created)
			{
				this.created = false;
				Dictionary<string, List<RouteData>> tours = new Dictionary<string, List<RouteData>>();
				RoutePointsDataSource.GetTours(tours, this.mapObjectContainer);
				foreach (KeyValuePair<string, List<RouteData>> keyValuePair in tours)
				{
					string tour = keyValuePair.Key;
					if (!string.IsNullOrEmpty(tour))
					{
						List<RouteData> _routes = keyValuePair.Value;
						int count2 = _routes.Count;
						if (count2 > 0)
						{
							int maxCountIndex = 0;
							int maxCount = _routes[maxCountIndex].RoutePoints.Count;
							for (int index2 = 0; index2 < count2; index2++)
							{
								int _count = _routes[index2].RoutePoints.Count;
								if (_count > maxCount)
								{
									maxCountIndex = index2;
									maxCount = _count;
								}
							}
							if (maxCount > 0)
							{
								RouteData routeData = _routes[maxCountIndex];
								RouteObjectBrowserForm.RouteObjectParams routeObjectParams2;
								if (!this.routeObjectsParams.TryGetValue(tour, out routeObjectParams2))
								{
									routeObjectParams2 = null;
								}
								RouteObject routeObject2 = null;
								if (routeObjectParams2 != null)
								{
									routeObject2 = routeObjectParams2.RouteObject;
								}
								else
								{
									int newMapObjectID = this.mapObjectContainer.AddMapObject(new MapObjectType(MapObjectFactory.Type.RouteObject, string.Empty), true);
									IMapObject mapObject;
									if (this.mapObjectContainer.TryGetMapObject(newMapObjectID, out mapObject))
									{
										routeObject2 = (mapObject as RouteObject);
									}
								}
								if (routeObject2 != null)
								{
									if (routeObjectParams2 != null)
									{
										routeObject2.Create(tour, routeData.RoutePoints, routeData.Circle, routeObjectParams2.Point, false, routeObjectParams2.Speed, routeObjectParams2.Run);
										this.routeObjectsParams.Remove(tour);
									}
									else
									{
										routeObject2.Create(tour, routeData.RoutePoints, routeData.Circle);
									}
									ListViewItem listViewItem = new ListViewItem();
									RouteObjectBrowserForm.FillListRouteObjectViewItem(listViewItem, routeObject2, routeObjectParams2 != null && routeObjectParams2.Selected);
									this.RoutesListView.Items.Add(listViewItem);
									if (routeObjectParams2 != null && routeObjectParams2.Focused)
									{
										listViewItem.Focused = true;
									}
								}
							}
						}
					}
				}
				this.created = true;
			}
			foreach (KeyValuePair<string, RouteObjectBrowserForm.RouteObjectParams> keyValuePair2 in this.routeObjectsParams)
			{
				this.mapObjectContainer.RemoveMapObject(keyValuePair2.Value.RouteObject);
			}
			this.routeObjectsParams.Clear();
		}

		// Token: 0x060012A9 RID: 4777 RVA: 0x00088430 File Offset: 0x00087430
		private void EditBoxTimer_Tick(object sender, EventArgs e)
		{
			this.EditBoxTimer.Stop();
			this.UpdateEditBoxes();
		}

		// Token: 0x060012AA RID: 4778 RVA: 0x00088444 File Offset: 0x00087444
		private void UpdateEditBoxes()
		{
			foreach (KeyValuePair<string, int> keyValuePair in this.editBoxForUpdateNames)
			{
				if (keyValuePair.Key == this.SpeedEditBox.Name)
				{
					this.UpdateSpeed();
				}
				else if (keyValuePair.Key == this.PathEditBox.Name)
				{
					this.UpdatePath();
				}
				else if (keyValuePair.Key == this.PointEditBox.Name)
				{
					this.UpdatePath();
				}
			}
			this.editBoxForUpdateNames.Clear();
		}

		// Token: 0x060012AB RID: 4779 RVA: 0x000884FC File Offset: 0x000874FC
		private void CameraTimer_Tick(object sender, EventArgs e)
		{
			this.CameraTimer.Stop();
			if (this.cameraController != null)
			{
				if (this.cameraController.Step(Environment.TickCount, this.CameraTimer.Interval == RouteObjectBrowserForm.CameraController.StartDelay))
				{
					this.CameraTimer.Interval = RouteObjectBrowserForm.CameraController.RefreshDelay;
					this.CameraTimer.Start();
				}
				else
				{
					this.cameraController.Stop(false);
					this.cameraController = null;
				}
			}
			this.UpdateCameraButtons();
		}

		// Token: 0x060012AC RID: 4780 RVA: 0x00088578 File Offset: 0x00087578
		private void SetUpdate(IMapObject mapObject)
		{
			if (mapObject != null && mapObject.Type.Type == MapObjectFactory.Type.RoutePoint)
			{
				RoutePoint routePoint = mapObject as RoutePoint;
				if (routePoint != null && routePoint.RoutePointType == RoutePointType.Simple)
				{
					this.needUpdate = true;
				}
			}
		}

		// Token: 0x060012AD RID: 4781 RVA: 0x000885B7 File Offset: 0x000875B7
		private void OnMapObjectAdded(MapObjectContainer _mapObjectContainer, IMapObject mapObject)
		{
			this.SetUpdate(mapObject);
		}

		// Token: 0x060012AE RID: 4782 RVA: 0x000885C0 File Offset: 0x000875C0
		private void OnMapObjectRemoved(MapObjectContainer _mapObjectContainer, IMapObject mapObject)
		{
			this.SetUpdate(mapObject);
		}

		// Token: 0x060012AF RID: 4783 RVA: 0x000885C9 File Offset: 0x000875C9
		private void OnPositionChanged(MapObjectContainer _mapObjectContainer, IMapObject mapObject, ref Position oldValue, ref Position newValue)
		{
			this.SetUpdate(mapObject);
		}

		// Token: 0x060012B0 RID: 4784 RVA: 0x000885D2 File Offset: 0x000875D2
		private void OnRotationChanged(MapObjectContainer _mapObjectContainer, IMapObject mapObject, ref Rotation oldValue, ref Rotation newValue)
		{
			this.SetUpdate(mapObject);
		}

		// Token: 0x060012B1 RID: 4785 RVA: 0x000885DB File Offset: 0x000875DB
		private void OnScaleChanged(MapObjectContainer _mapObjectContainer, IMapObject mapObject, ref Scale oldValue, ref Scale newValue)
		{
			this.SetUpdate(mapObject);
		}

		// Token: 0x060012B2 RID: 4786 RVA: 0x000885E4 File Offset: 0x000875E4
		private void OnPostLinkAdded(MapObjectContainer _mapObjectContainer, LinkContainer<IMapObject> linkContainer, IMapObject mapObjectLeft, IMapObject mapObjectRight, ILinkData linkData)
		{
			this.SetUpdate(mapObjectLeft);
			this.SetUpdate(mapObjectRight);
		}

		// Token: 0x060012B3 RID: 4787 RVA: 0x000885F5 File Offset: 0x000875F5
		private void OnPostLinkRemoved(MapObjectContainer _mapObjectContainer, LinkContainer<IMapObject> linkContainer, IMapObject mapObjectLeft, IMapObject mapObjectRight, ILinkData linkData)
		{
			this.SetUpdate(mapObjectLeft);
			this.SetUpdate(mapObjectRight);
		}

		// Token: 0x060012B4 RID: 4788 RVA: 0x00088606 File Offset: 0x00087606
		public void OnRoutePointRouteChanged(MapEditorMapObjectContainer _mapObjectContainer, RoutePoint routePoint, ref string oldValue, ref string newValue)
		{
			this.SetUpdate(routePoint);
		}

		// Token: 0x060012B5 RID: 4789 RVA: 0x0008860F File Offset: 0x0008760F
		public void OnSelectionChanged(MapObjectSelector mapObjectSelector)
		{
			this.UpdateCameraButtons();
		}

		// Token: 0x060012B6 RID: 4790 RVA: 0x00088617 File Offset: 0x00087617
		private void OnEditorSceneBeforeStep(EditorScene _editorScene)
		{
			if (this.mapObjectContainer != null && this.mapSceneParams != null && this.mapSceneParams.ShowRouteObjects && this.needUpdate && this.mapObjectContainer != null && this.created)
			{
				this.FillRouteObjectList();
			}
		}

		// Token: 0x060012B7 RID: 4791 RVA: 0x00088654 File Offset: 0x00087654
		public RouteObjectBrowserForm(MainForm.Context context) : base(EditorEnvironment.EditorFormsFolder + "RouteObjectBrowserForm.xml", context)
		{
			this.InitializeComponent();
			if (base.ParamsSaver != null)
			{
				base.ParamsSaver.RegisterControl(this.AnimateRouteObjectCheckBox);
				base.ParamsSaver.RegisterControl(this.RoutesListView);
				base.ParamsSaver.RegisterControl(this.SetObjectDistanceTextBox);
				base.ParamsSaver.RegisterControl(this.SetObjectRotationCheckBox);
			}
			this.created = true;
			this.UpdateControls();
		}

		// Token: 0x060012B8 RID: 4792 RVA: 0x000886F8 File Offset: 0x000876F8
		public void Bind(MapEditorMapObjectContainer _mapObjectContainer, MapObjectSelector _selector, MapEditorScene mapEditorScene)
		{
			this.Unbind();
			this.mapObjectContainer = _mapObjectContainer;
			this.selector = _selector;
			if (this.mapObjectContainer != null)
			{
				this.mapObjectContainer.MapObjectAdded += this.OnMapObjectAdded;
				this.mapObjectContainer.MapObjectRemoved += this.OnMapObjectRemoved;
				this.mapObjectContainer.PositionChanged += this.OnPositionChanged;
				this.mapObjectContainer.RotationChanged += this.OnRotationChanged;
				this.mapObjectContainer.ScaleChanged += this.OnScaleChanged;
				this.mapObjectContainer.PostLinkAdded += this.OnPostLinkAdded;
				this.mapObjectContainer.PostLinkRemoved += this.OnPostLinkRemoved;
				if (this.mapObjectContainer.RoutePointContainer != null)
				{
					this.mapObjectContainer.RoutePointContainer.RoutePointRouteChanged += this.OnRoutePointRouteChanged;
				}
			}
			if (this.selector != null)
			{
				this.selector.SelectionChanged += this.OnSelectionChanged;
			}
			this.mapSceneParams = mapEditorScene.MapSceneParams;
			if (this.mapSceneParams != null)
			{
				this.mapSceneParams.ShowRouteObjectsChanged += this.OnShowRouteObjectsChanged;
			}
			base.Context.EditorScene.BeforeStep += this.OnEditorSceneBeforeStep;
			this.UpdateControls();
			this.FillRouteObjectList();
		}

		// Token: 0x060012B9 RID: 4793 RVA: 0x00088860 File Offset: 0x00087860
		public void Unbind()
		{
			this.ClearRouteObjectList();
			base.Context.EditorScene.BeforeStep -= this.OnEditorSceneBeforeStep;
			if (this.selector != null)
			{
				this.selector.SelectionChanged -= this.OnSelectionChanged;
				this.selector = null;
			}
			if (this.mapObjectContainer != null)
			{
				if (this.mapObjectContainer.RoutePointContainer != null)
				{
					this.mapObjectContainer.RoutePointContainer.RoutePointRouteChanged -= this.OnRoutePointRouteChanged;
				}
				this.mapObjectContainer.MapObjectAdded -= this.OnMapObjectAdded;
				this.mapObjectContainer.MapObjectRemoved -= this.OnMapObjectRemoved;
				this.mapObjectContainer.PositionChanged -= this.OnPositionChanged;
				this.mapObjectContainer.RotationChanged -= this.OnRotationChanged;
				this.mapObjectContainer.ScaleChanged -= this.OnScaleChanged;
				this.mapObjectContainer.PostLinkAdded -= this.OnPostLinkAdded;
				this.mapObjectContainer.PostLinkRemoved -= this.OnPostLinkRemoved;
				this.mapObjectContainer = null;
			}
			if (this.mapSceneParams != null)
			{
				this.mapSceneParams.ShowRouteObjectsChanged -= this.OnShowRouteObjectsChanged;
				this.mapSceneParams = null;
			}
			this.UpdateControls();
		}

		// Token: 0x04000D55 RID: 3413
		private static readonly double path_epsilon = 0.0001;

		// Token: 0x04000D56 RID: 3414
		private readonly List<RouteObject> selectedRouteObjects = new List<RouteObject>();

		// Token: 0x04000D57 RID: 3415
		private readonly Dictionary<string, RouteObjectBrowserForm.RouteObjectParams> routeObjectsParams = new Dictionary<string, RouteObjectBrowserForm.RouteObjectParams>();

		// Token: 0x04000D58 RID: 3416
		private readonly Dictionary<string, int> editBoxForUpdateNames = new Dictionary<string, int>();

		// Token: 0x04000D59 RID: 3417
		private MapEditorMapObjectContainer mapObjectContainer;

		// Token: 0x04000D5A RID: 3418
		private MapObjectSelector selector;

		// Token: 0x04000D5B RID: 3419
		private MapSceneParams mapSceneParams;

		// Token: 0x04000D5C RID: 3420
		private bool created;

		// Token: 0x04000D5D RID: 3421
		private bool needUpdate;

		// Token: 0x04000D5E RID: 3422
		private RouteObjectBrowserForm.CameraController cameraController;

		// Token: 0x02000187 RID: 391
		private class RouteObjectParams
		{
			// Token: 0x060012BB RID: 4795 RVA: 0x000889D0 File Offset: 0x000879D0
			public RouteObjectParams(RouteObject _routeObject, bool _selected, bool _focused)
			{
				this.routeObject = _routeObject;
				if (this.routeObject != null)
				{
					this.point = this.routeObject.GetPointByTime(this.routeObject.Time);
					this.speed = this.routeObject.Speed;
					this.run = this.routeObject.Run;
				}
				this.selected = _selected;
				this.focused = _focused;
			}

			// Token: 0x170003BA RID: 954
			// (get) Token: 0x060012BC RID: 4796 RVA: 0x00088A54 File Offset: 0x00087A54
			public RouteObject RouteObject
			{
				get
				{
					return this.routeObject;
				}
			}

			// Token: 0x170003BB RID: 955
			// (get) Token: 0x060012BD RID: 4797 RVA: 0x00088A5C File Offset: 0x00087A5C
			public double Point
			{
				get
				{
					return this.point;
				}
			}

			// Token: 0x170003BC RID: 956
			// (get) Token: 0x060012BE RID: 4798 RVA: 0x00088A64 File Offset: 0x00087A64
			public double Speed
			{
				get
				{
					return this.speed;
				}
			}

			// Token: 0x170003BD RID: 957
			// (get) Token: 0x060012BF RID: 4799 RVA: 0x00088A6C File Offset: 0x00087A6C
			public bool Run
			{
				get
				{
					return this.run;
				}
			}

			// Token: 0x170003BE RID: 958
			// (get) Token: 0x060012C0 RID: 4800 RVA: 0x00088A74 File Offset: 0x00087A74
			public bool Selected
			{
				get
				{
					return this.selected;
				}
			}

			// Token: 0x170003BF RID: 959
			// (get) Token: 0x060012C1 RID: 4801 RVA: 0x00088A7C File Offset: 0x00087A7C
			public bool Focused
			{
				get
				{
					return this.focused;
				}
			}

			// Token: 0x04000D5F RID: 3423
			private readonly RouteObject routeObject;

			// Token: 0x04000D60 RID: 3424
			private readonly double point;

			// Token: 0x04000D61 RID: 3425
			private readonly double speed = RouteObject.DefaultSpeed;

			// Token: 0x04000D62 RID: 3426
			private readonly bool run = RouteObject.DefaultRun;

			// Token: 0x04000D63 RID: 3427
			private readonly bool selected;

			// Token: 0x04000D64 RID: 3428
			private readonly bool focused;
		}

		// Token: 0x02000188 RID: 392
		private class CameraController
		{
			// Token: 0x060012C2 RID: 4802 RVA: 0x00088A84 File Offset: 0x00087A84
			private bool PlaceCamera(int time)
			{
				double timeInterval = (double)(time - this.startTime) / 1000.0;
				if (this.cameraRouteObject != null)
				{
					timeInterval *= this.cameraRouteObject.RouteSpeed;
					bool result = true;
					if (timeInterval > this.cameraRouteObject.Length - MathConsts.DOUBLE_EPSILON)
					{
						timeInterval = this.cameraRouteObject.Length - MathConsts.DOUBLE_EPSILON;
						result = false;
					}
					if (this.targetRouteObject == null)
					{
						Position position;
						Rotation rotation;
						this.cameraRouteObject.GetSplineParams(timeInterval, out position, out rotation);
						CameraPlacement cameraPlacement = new CameraPlacement(position, rotation);
						if (this.editorScene != null)
						{
							this.editorScene.SetPlacement(this.editorSceneViewID, ref cameraPlacement);
						}
					}
					else
					{
						Position cameraPosition;
						this.cameraRouteObject.GetSplinePoint(timeInterval, out cameraPosition);
						double targetPoint = this.cameraRouteObject.GetPointByTime(timeInterval);
						if (!this.targetRouteObject.Circle)
						{
							if (targetPoint < MathConsts.DOUBLE_EPSILON)
							{
								targetPoint = 0.0;
							}
							else if (targetPoint > (double)(this.targetRouteObject.Count - 1) - MathConsts.DOUBLE_EPSILON)
							{
								targetPoint = (double)(this.targetRouteObject.Count - 1) - MathConsts.DOUBLE_EPSILON;
							}
						}
						Position targetPosition;
						this.targetRouteObject.GetSplinePoint(this.targetRouteObject.GetTimeByPoint(targetPoint), out targetPosition);
						Quat _quat = new Quat((targetPosition - cameraPosition).Vec3);
						Rotation rotation2 = new Rotation(ref _quat);
						CameraPlacement cameraPlacement2 = new CameraPlacement(cameraPosition, rotation2);
						if (this.editorScene != null)
						{
							this.editorScene.SetPlacement(this.editorSceneViewID, ref cameraPlacement2);
						}
					}
					return result;
				}
				return false;
			}

			// Token: 0x170003C0 RID: 960
			// (get) Token: 0x060012C3 RID: 4803 RVA: 0x00088C10 File Offset: 0x00087C10
			public static int StartDelay
			{
				get
				{
					return RouteObjectBrowserForm.CameraController.startDelay;
				}
			}

			// Token: 0x170003C1 RID: 961
			// (get) Token: 0x060012C4 RID: 4804 RVA: 0x00088C17 File Offset: 0x00087C17
			public static int RefreshDelay
			{
				get
				{
					return RouteObjectBrowserForm.CameraController.refreshDelay;
				}
			}

			// Token: 0x060012C5 RID: 4805 RVA: 0x00088C20 File Offset: 0x00087C20
			public CameraController(EditorScene _editorScene, int _editorSceneViewID, RouteObject _cameraRouteObject, RouteObject _targetRouteObject)
			{
				this.editorScene = _editorScene;
				this.editorSceneViewID = _editorSceneViewID;
				if (this.editorScene != null)
				{
					this.editorScene.GetPlacement(this.editorSceneViewID, out this.startCameraPlacement);
				}
				this.cameraRouteObject = _cameraRouteObject;
				this.targetRouteObject = _targetRouteObject;
			}

			// Token: 0x060012C6 RID: 4806 RVA: 0x00088C77 File Offset: 0x00087C77
			public bool Start(int time)
			{
				return this.Step(time, true);
			}

			// Token: 0x060012C7 RID: 4807 RVA: 0x00088C81 File Offset: 0x00087C81
			public bool Step(int time, bool reset)
			{
				if (reset)
				{
					this.startTime = time;
				}
				return this.PlaceCamera(time);
			}

			// Token: 0x060012C8 RID: 4808 RVA: 0x00088C94 File Offset: 0x00087C94
			public void Stop(bool restoreCamerpacement)
			{
				if (restoreCamerpacement && this.editorScene != null)
				{
					CameraPlacement cameraPlacement = this.startCameraPlacement;
					this.editorScene.SetPlacement(this.editorSceneViewID, ref cameraPlacement);
				}
			}

			// Token: 0x04000D65 RID: 3429
			private static readonly int startDelay = 2000;

			// Token: 0x04000D66 RID: 3430
			private static readonly int refreshDelay = 50;

			// Token: 0x04000D67 RID: 3431
			private readonly EditorScene editorScene;

			// Token: 0x04000D68 RID: 3432
			private readonly int editorSceneViewID = -1;

			// Token: 0x04000D69 RID: 3433
			private readonly CameraPlacement startCameraPlacement;

			// Token: 0x04000D6A RID: 3434
			private readonly RouteObject cameraRouteObject;

			// Token: 0x04000D6B RID: 3435
			private readonly RouteObject targetRouteObject;

			// Token: 0x04000D6C RID: 3436
			private int startTime;
		}
	}
}
