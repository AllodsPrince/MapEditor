using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Text;
using System.Windows.Forms;
using InputState;
using LauncherTools.InputState;
using MapEditor.Map.Dialogs;
using MapEditor.Resources.Strings;
using Tools.DragAndDrop;
using Tools.Geometry;
using Tools.InputState;
using Tools.MapObjects;

namespace MapEditor.Map.States
{
	// Token: 0x0200021D RID: 541
	internal class MapState : State, IDragAndDropHandler
	{
		// Token: 0x06001A20 RID: 6688 RVA: 0x000AC06C File Offset: 0x000AB06C
		public MapState(MainForm.Context _context) : base("MapEditorMapState")
		{
			this.context = _context;
			base.AddMethod("fill_heights_from_heightmap", new Method(this.OnFillHeightsFromHeightmap));
			base.AddMethod("open_map", new Method(this.OnOpenMap));
			base.AddMethod("close_map", new Method(this.OnCloseMap));
			base.AddMethod("recent_map_0", new Method(this.OnOpenRecentMap));
			base.AddMethod("recent_map_1", new Method(this.OnOpenRecentMap));
			base.AddMethod("recent_map_2", new Method(this.OnOpenRecentMap));
			base.AddMethod("recent_map_3", new Method(this.OnOpenRecentMap));
			base.AddMethod("recent_map_4", new Method(this.OnOpenRecentMap));
			base.AddMethod("recent_map_5", new Method(this.OnOpenRecentMap));
			base.AddMethod("recent_map_6", new Method(this.OnOpenRecentMap));
			base.AddMethod("recent_map_7", new Method(this.OnOpenRecentMap));
			base.AddMethod("recent_map_8", new Method(this.OnOpenRecentMap));
			base.AddMethod("recent_map_9", new Method(this.OnOpenRecentMap));
			this.EnterState = (State.ActivateEvent)Delegate.Combine(this.EnterState, new State.ActivateEvent(this.OnEnterState));
			this.LeaveState = (State.ActivateEvent)Delegate.Combine(this.LeaveState, new State.ActivateEvent(this.OnLeaveState));
		}

		// Token: 0x06001A21 RID: 6689 RVA: 0x000AC1FC File Offset: 0x000AB1FC
		private bool CloseMap(bool askForChanges)
		{
			if (this.mapEditState == null)
			{
				return true;
			}
			if (!askForChanges || this.mapEditState.Save(true))
			{
				this.context.StateContainer.UnbindState(this.mapEditState);
				this.context.OperationContainer.Clear();
				this.mapEditState.Destroy();
				this.mapEditState = null;
				this.context.StopSaveNotificationTimer();
				return true;
			}
			return false;
		}

		// Token: 0x06001A22 RID: 6690 RVA: 0x000AC26A File Offset: 0x000AB26A
		private void UpdateMethods()
		{
			base.SetMethodParams("open_map", true, true, false, false);
			if (this.mapEditState != null)
			{
				base.SetMethodParams("close_map", true, true, false, false);
				return;
			}
			base.SetMethodParams("close_map", true, false, false, false);
		}

		// Token: 0x06001A23 RID: 6691 RVA: 0x000AC2A2 File Offset: 0x000AB2A2
		private void OnEnterState(IState mapState)
		{
			this.UpdateMethods();
			this.context.DragAndDropContainer.AddHandler(this);
		}

		// Token: 0x06001A24 RID: 6692 RVA: 0x000AC2BB File Offset: 0x000AB2BB
		private void OnLeaveState(IState mapState)
		{
			this.context.DragAndDropContainer.RemoveHandler(this);
			this.context.ItemDataContainer.ClearDataMiners();
			this.CloseMap(false);
			this.UpdateMethods();
		}

		// Token: 0x06001A25 RID: 6693 RVA: 0x000AC2EC File Offset: 0x000AB2EC
		private void OnFillHeightsFromHeightmap(MethodArgs methodArgs)
		{
			FillHeightsFromHeightmapDialog fillHeightsFromHeightmapDialog = new FillHeightsFromHeightmapDialog(this.mapEditState != null);
			if (fillHeightsFromHeightmapDialog.ShowDialog() == DialogResult.OK && MessageBox.Show(Strings.FILL_HEIGHTS_FROM_HEIGHTMAP_MESSAGE, Strings.FILL_HEIGHTS_FROM_HEIGHTMAP_TITLE, MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) == DialogResult.Yes && this.mapEditState != null && fillHeightsFromHeightmapDialog.ActiveMap)
			{
				this.mapEditState.FillHeightsFromHeightmap(fillHeightsFromHeightmapDialog.TerrainIndex, fillHeightsFromHeightmapDialog.BlackHeight, fillHeightsFromHeightmapDialog.WhiteHeight, fillHeightsFromHeightmapDialog.HeighmapFileName);
			}
		}

		// Token: 0x06001A26 RID: 6694 RVA: 0x000AC35C File Offset: 0x000AB35C
		private void OnOpenMap(MethodArgs methodArgs)
		{
			OpenMapDialog openMapDialog = new OpenMapDialog();
			if (openMapDialog.ShowDialog() == DialogResult.OK)
			{
				this.OpenMap(openMapDialog.ResultParams);
				openMapDialog.Dispose();
			}
		}

		// Token: 0x06001A27 RID: 6695 RVA: 0x000AC38C File Offset: 0x000AB38C
		private void OnOpenRecentMap(MethodArgs methodArgs)
		{
			ToolStripItem toolStripItem = methodArgs.sender as ToolStripItem;
			if (toolStripItem != null && toolStripItem.Tag != null)
			{
				string tag = toolStripItem.Tag.ToString();
				string item;
				OpenMapDialog.Params openedMapParams;
				if (this.context.MapRecentList.GetItem(tag, out item) && this.context.MapRecentItemDataMiner.ParseMapRecentItem(item, out openedMapParams))
				{
					this.OpenMap(openedMapParams);
				}
			}
		}

		// Token: 0x06001A28 RID: 6696 RVA: 0x000AC3F0 File Offset: 0x000AB3F0
		public void OpenMap(OpenMapDialog.Params openedMapParams)
		{
			if (string.IsNullOrEmpty(openedMapParams.Name))
			{
				return;
			}
			if (this.mapEditState != null)
			{
				if (this.mapEditState.Map.Data.ContinentName == openedMapParams.Name && this.mapEditState.Map.Data.MinXMinYPatchCoords.Equals(openedMapParams.GetPatch()))
				{
					Position cameraPos = openedMapParams.CameraPosition;
					this.mapEditState.SetCameraOverPosition(ref cameraPos);
					return;
				}
				if (!this.mapEditState.Save(true))
				{
					return;
				}
			}
			this.CloseMap(true);
			this.mapEditState = new MapEditState(this.context, openedMapParams);
			this.context.StateContainer.BindState(this.mapEditState);
			string mapRecentItem;
			if (this.context.MapRecentItemDataMiner.GetMapRecentItem(openedMapParams, out mapRecentItem))
			{
				this.context.MapRecentList.AddItem(mapRecentItem);
			}
			this.UpdateMethods();
			this.context.StartSaveNotificationTimer();
		}

		// Token: 0x06001A29 RID: 6697 RVA: 0x000AC4F0 File Offset: 0x000AB4F0
		public void OpenMapFromCmdLine(OpenMapDialog.Params openedMapParams)
		{
			this.CloseMap(false);
			this.mapEditState = new MapEditState(this.context, openedMapParams);
			this.context.StateContainer.BindState(this.mapEditState);
			this.UpdateMethods();
		}

		// Token: 0x06001A2A RID: 6698 RVA: 0x000AC528 File Offset: 0x000AB528
		private void OnCloseMap(MethodArgs methodArgs)
		{
			if (this.mapEditState != null)
			{
				this.CloseMap(true);
				this.UpdateMethods();
			}
		}

		// Token: 0x06001A2B RID: 6699 RVA: 0x000AC540 File Offset: 0x000AB540
		public bool HandleDragEnter(object sender, DragEventArgs eventArgs)
		{
			if (eventArgs.Data.GetDataPresent(DataFormats.FileDrop))
			{
				eventArgs.Effect = DragDropEffects.Copy;
			}
			else
			{
				eventArgs.Effect = DragDropEffects.None;
			}
			return true;
		}

		// Token: 0x06001A2C RID: 6700 RVA: 0x000AC565 File Offset: 0x000AB565
		public bool HandleDragLeave(object sender, EventArgs eventArgs)
		{
			return false;
		}

		// Token: 0x06001A2D RID: 6701 RVA: 0x000AC568 File Offset: 0x000AB568
		public bool HandleDragDrop(object sender, DragEventArgs eventArgs)
		{
			Array array = eventArgs.Data.GetData(DataFormats.FileDrop) as Array;
			if (array != null && array.GetValue(0) != null)
			{
				string fileName = array.GetValue(0).ToString();
				Image tmp = Image.FromFile(fileName);
				PropertyItem info = tmp.GetPropertyItem(tmp.PropertyIdList[0]);
				Encoding enc = Encoding.Unicode;
				string myString = enc.GetString(info.Value);
				return this.OpenMapFromCommandLine(myString);
			}
			return false;
		}

		// Token: 0x06001A2E RID: 6702 RVA: 0x000AC5DA File Offset: 0x000AB5DA
		public bool HandleDragOver(object sender, DragEventArgs eventArgs)
		{
			return false;
		}

		// Token: 0x06001A2F RID: 6703 RVA: 0x000AC5E0 File Offset: 0x000AB5E0
		public bool OpenMapFromCommandLine(string fileName)
		{
			if (fileName == "")
			{
				return false;
			}
			string[] array = fileName.Split(new char[]
			{
				' '
			});
			if (array.Length < 4)
			{
				return false;
			}
			Position startCameraPosition = Position.Empty;
			Rotation startCameraRotation = Rotation.Empty;
			OpenMapDialog.Params openMapParams = new OpenMapDialog.Params();
			openMapParams.Name = array[0].Replace("\"", "");
			openMapParams.MapSize = 4;
			try
			{
				double value;
				if (!double.TryParse(array[1].Replace(".", ","), out value))
				{
					return false;
				}
				startCameraPosition.X = value;
				if (!double.TryParse(array[2].Replace(".", ","), out value))
				{
					return false;
				}
				startCameraPosition.Y = value;
				if (!double.TryParse(array[3].Replace(".", ","), out value))
				{
					return false;
				}
				startCameraPosition.Z = value;
				float value2;
				if (!float.TryParse(array[4].Replace(".", ","), out value2))
				{
					return false;
				}
				startCameraRotation.Yaw = value2;
				if (!float.TryParse(array[5].Replace(".", ","), out value2))
				{
					return false;
				}
				startCameraRotation.Pitch = value2;
			}
			catch (ArgumentException e)
			{
				Console.WriteLine(e);
				return false;
			}
			Tools.Geometry.Point startPosition = new Tools.Geometry.Point((int)(startCameraPosition.X / (double)Constants.PatchSize + 0.5) - 2, (int)(startCameraPosition.Y / (double)Constants.PatchSize + 0.5) - 2);
			if (startPosition.X < Constants.WorldBounds.Min.X)
			{
				startPosition.X = Constants.WorldBounds.Min.X;
			}
			else if (startPosition.X > Constants.WorldBounds.Max.X - openMapParams.MapSize + 1)
			{
				startPosition.X = Constants.WorldBounds.Max.X - openMapParams.MapSize + 1;
			}
			if (startPosition.Y < Constants.WorldBounds.Min.Y)
			{
				startPosition.Y = Constants.WorldBounds.Min.Y;
			}
			else if (startPosition.Y > Constants.WorldBounds.Max.Y - openMapParams.MapSize + 1)
			{
				startPosition.Y = Constants.WorldBounds.Max.Y - openMapParams.MapSize + 1;
			}
			if (this.mapEditState != null)
			{
				double minx = ((double)this.mapEditState.Map.Data.MinXMinYPatchCoords.X + 0.5) * (double)Constants.PatchSize;
				double maxx = ((double)this.mapEditState.Map.Data.MinXMinYPatchCoords.X + 3.5) * (double)Constants.PatchSize;
				double miny = ((double)this.mapEditState.Map.Data.MinXMinYPatchCoords.Y + 0.5) * (double)Constants.PatchSize;
				double maxy = ((double)this.mapEditState.Map.Data.MinXMinYPatchCoords.Y + 3.5) * (double)Constants.PatchSize;
				if (startCameraPosition.X >= minx && startCameraPosition.X <= maxx && startCameraPosition.Y >= miny && startCameraPosition.Y <= maxy)
				{
					this.mapEditState.SetCameraView(startCameraPosition, startCameraRotation);
					return true;
				}
				if (!this.CloseMap(true))
				{
					return false;
				}
			}
			openMapParams.SetPatch(startPosition);
			this.OpenMap(openMapParams);
			if (this.mapEditState != null)
			{
				this.mapEditState.SetCameraView(startCameraPosition, startCameraRotation);
				return true;
			}
			return false;
		}

		// Token: 0x04001100 RID: 4352
		private readonly MainForm.Context context;

		// Token: 0x04001101 RID: 4353
		private MapEditState mapEditState;
	}
}
