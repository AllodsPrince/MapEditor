using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using Db;
using InputState;
using LauncherTools.InputState;
using Tools.Geometry;
using Tools.InputState;
using Tools.ItemDataContainer;
using Tools.MapObjects;

namespace MapEditor.Map.States
{
	// Token: 0x0200011D RID: 285
	internal abstract class ApplyToPatchState : State
	{
		// Token: 0x06000E03 RID: 3587 RVA: 0x00074098 File Offset: 0x00073098
		private bool HitTestEditPolygonSubtate(string previousStateLabel)
		{
			if (this.addPolygonSubstate != null && this.patchRegion != null && this.patchRegion.Polygon != null)
			{
				if (this.patchRegion.Polygon.Count > 2)
				{
					Vec3 point = this.patchRegion.SquareCenter;
					this.editPolygonSubstate.PointTested = this.patchRegion.Polygon.LocateNearestPoint(ref point, 1.0);
					if (this.editPolygonSubstate.PointTested != -1)
					{
						this.editPolygonSubstate.PreviousStateLabel = previousStateLabel;
						this.multiState.ActiveStateLabel = this.editPolygonSubstate.Label;
						return true;
					}
				}
				if (KeyStatus.Control)
				{
					this.editPolygonSubstate.CenterTested = true;
					this.editPolygonSubstate.PreviousStateLabel = previousStateLabel;
					this.multiState.ActiveStateLabel = this.editPolygonSubstate.Label;
					return true;
				}
			}
			return false;
		}

		// Token: 0x06000E04 RID: 3588 RVA: 0x0007417C File Offset: 0x0007317C
		private void InvokeLeaveFromRegionState()
		{
			if (this.multiState.ActiveStateLabel == this.addPolygonSubstate.Label)
			{
				this.addPolygonSubstate.InvokeLeave(false, true);
				return;
			}
			if (this.multiState.ActiveStateLabel == this.editPolygonSubstate.Label)
			{
				this.editPolygonSubstate.InvokeLeave(false);
			}
		}

		// Token: 0x06000E05 RID: 3589 RVA: 0x000741E0 File Offset: 0x000731E0
		private void ActivateAddPolygonSubstate(string previousStateLabel)
		{
			if (this.addPolygonSubstate != null && this.patchRegion != null && this.patchRegion.Polygon != null)
			{
				this.addPolygonSubstate.PreviousStateLabel = previousStateLabel;
				this.multiState.ActiveStateLabel = this.addPolygonSubstate.Label;
			}
		}

		// Token: 0x06000E06 RID: 3590 RVA: 0x0007422C File Offset: 0x0007322C
		private void ActivateEditPolygonSubtate(string previousStateLabel)
		{
			if (this.addPolygonSubstate != null && this.patchRegion != null && this.patchRegion.Polygon != null)
			{
				if (this.patchRegion.Polygon.Count < 3)
				{
					this.addPolygonSubstate.PreviousStateLabel = previousStateLabel;
					this.multiState.ActiveStateLabel = this.addPolygonSubstate.Label;
					return;
				}
				this.editPolygonSubstate.PreviousStateLabel = previousStateLabel;
				this.multiState.ActiveStateLabel = this.editPolygonSubstate.Label;
			}
		}

		// Token: 0x06000E07 RID: 3591 RVA: 0x000742B0 File Offset: 0x000732B0
		private void CreatePatchRegion()
		{
			this.patchRegion = new PatchRegion(this.mapEditState.Map.MapEditorMapObjectContainer, this.mapEditState.Map.SquareContainer, this.mapEditState.Map.PolygonContainer);
			this.patchRegion.TypeChanged += this.OnPatchRegionTypeChanged;
		}

		// Token: 0x06000E08 RID: 3592 RVA: 0x0007430F File Offset: 0x0007330F
		private void DestroyPatchRegion()
		{
			if (this.patchRegion != null)
			{
				this.patchRegion.TypeChanged -= this.OnPatchRegionTypeChanged;
				this.patchRegion.Destroy();
				this.patchRegion = null;
			}
		}

		// Token: 0x06000E09 RID: 3593 RVA: 0x00074344 File Offset: 0x00073344
		private void AddMethods()
		{
			base.AddMethod(this.GetApplyModeSetLabel(), new Method(this.OnApplyModeSet));
			base.AddMethod(this.GetApplyModeClearLabel(), new Method(this.OnApplyModeClear));
			base.AddMethod(this.GetItemListSelectedLabel(), new Method(this.OnItemListSelected));
			base.AddMethod(this.GetItemListClearedLabel(), new Method(this.OnItemListCleared));
			base.AddMethod(this.GetItemListRefreshingLabel(), new Method(this.OnItemListRefreshing));
			base.AddMethod(this.GetItemEditChoosedLabel(), new Method(this.OnItemEditChoosed));
			base.AddMethod(this.GetAddItemChoosedLabel(), new Method(this.OnAddItemChoosed));
			base.AddMethod(this.GetRemoveItemChoosedLabel(), new Method(this.OnRemoveItemChoosed));
			base.AddMethod(this.GetSizeChangedLabel(), new Method(this.OnSizeChanged));
		}

		// Token: 0x06000E0A RID: 3594 RVA: 0x00074429 File Offset: 0x00073429
		private void RecreateItemList()
		{
			this.UnbindItemList();
			this.LoadItemList();
			this.BindItemList();
		}

		// Token: 0x06000E0B RID: 3595 RVA: 0x00074440 File Offset: 0x00073440
		private void CreateStates()
		{
			this.multiState = new MultiState(this.GetMultiStateName());
			this.states = new List<IState>();
			this.applyState = new ApplyToPatchState.ApplyState(this.GetApplyStateName(), this);
			this.addPolygonSubstate = new ApplyToPatchState.AddPolygonSubstate(this);
			this.editPolygonSubstate = new ApplyToPatchState.EditPolygonSubstate(this);
			this.states.Add(this.applyState);
			this.states.Add(this.addPolygonSubstate);
			this.states.Add(this.editPolygonSubstate);
			this.multiState.AddStates(this.states);
			if (this.patchRegion.Type == PatchRegionType.Polygon && this.patchRegion != null && this.patchRegion.Polygon.Count < 3)
			{
				this.multiState.ActiveStateLabel = this.addPolygonSubstate.Label;
				return;
			}
			this.multiState.ActiveStateLabel = this.applyState.Label;
		}

		// Token: 0x06000E0C RID: 3596 RVA: 0x0007452D File Offset: 0x0007352D
		private void DestroyStates()
		{
			this.multiState.RemoveStates(this.states);
			this.states.Clear();
			this.states = null;
			this.multiState = null;
		}

		// Token: 0x06000E0D RID: 3597 RVA: 0x00074559 File Offset: 0x00073559
		private void BindItemList()
		{
			if (this.mapEditState != null)
			{
				this.CreateItemListSource();
			}
		}

		// Token: 0x06000E0E RID: 3598 RVA: 0x00074569 File Offset: 0x00073569
		private void UnbindItemList()
		{
			if (this.mapEditState != null)
			{
				this.DestroyItemListSource();
			}
		}

		// Token: 0x06000E0F RID: 3599 RVA: 0x0007457C File Offset: 0x0007357C
		private void OnDbItemChanged(DBID dbid)
		{
			if (this.mainDb != null && !DBID.IsNullOrEmpty(dbid) && this.mainDb.GetClassTypeName(dbid) == this.GetClassTypeName() && this.mapEditState != null)
			{
				string item = dbid.ToString();
				this.mapEditState.Context.ItemDataContainer.RefreshItemData(item);
				this.ChangeAdditionalDBData(item);
			}
		}

		// Token: 0x06000E10 RID: 3600 RVA: 0x000745DE File Offset: 0x000735DE
		private void OnDbItemAdded(DBID dbid)
		{
			if (this.mainDb != null && !DBID.IsNullOrEmpty(dbid) && this.mainDb.GetClassTypeName(dbid) == this.GetClassTypeName())
			{
				this.RecreateItemList();
			}
		}

		// Token: 0x06000E11 RID: 3601 RVA: 0x0007460F File Offset: 0x0007360F
		private void OnDbItemRemoved(DBID dbid)
		{
			if (this.mainDb != null && this.mainDb.GetClassTypeName(dbid) == this.GetClassTypeName())
			{
				this.removingItemDBID = dbid;
			}
		}

		// Token: 0x06000E12 RID: 3602 RVA: 0x0007463C File Offset: 0x0007363C
		private void OnDbAfterItemRemoved(DBID dbid)
		{
			if (!DBID.IsNullOrEmpty(dbid) && this.removingItemDBID == dbid)
			{
				if (this.mapEditState != null)
				{
					this.mapEditState.Context.ItemDataContainer.RemoveCacheFile(dbid.ToString());
				}
				this.RecreateItemList();
				this.removingItemDBID = DBID.Empty;
			}
		}

		// Token: 0x06000E13 RID: 3603 RVA: 0x00074694 File Offset: 0x00073694
		private void OnEnterState(IState state)
		{
			base.Container.BindState(this.multiState);
			this.previousLightmapState = this.mapEditState.Context.EditorSceneParams.ActiveLightmapState;
			this.mapEditState.Context.EditorSceneParams.ActiveLightmapState = this.GetActiveLightmapState();
			this.patchRegion.Visible = true;
			this.AdditionalEnterState();
		}

		// Token: 0x06000E14 RID: 3604 RVA: 0x000746FC File Offset: 0x000736FC
		private void OnLeaveState(IState state)
		{
			this.mapEditState.Context.Statusbar.StatusHelp.Text = string.Empty;
			this.patchRegion.Visible = false;
			this.mapEditState.Context.EditorSceneParams.ActiveLightmapState = this.previousLightmapState;
			base.Container.UnbindState(this.multiState);
		}

		// Token: 0x06000E15 RID: 3605 RVA: 0x00074760 File Offset: 0x00073760
		private void OnPatchRegionTypeChanged(PatchRegion _patchRegion, ref PatchRegionType oldValue, ref PatchRegionType newValue)
		{
			if (this.patchRegion != null)
			{
				this.InvokeLeaveFromRegionState();
				if (this.patchRegion.Type == PatchRegionType.Polygon && this.patchRegion.Polygon.Count < 3 && this.multiState.ActiveStateLabel == this.applyState.Label)
				{
					this.ActivateAddPolygonSubstate(this.applyState.Label);
				}
			}
		}

		// Token: 0x06000E16 RID: 3606 RVA: 0x000747CA File Offset: 0x000737CA
		private void OnApplyModeSet(MethodArgs methodArgs)
		{
			this.activeApplyMode = ApplyToPatchState.ApplyMode.Set;
		}

		// Token: 0x06000E17 RID: 3607 RVA: 0x000747D3 File Offset: 0x000737D3
		private void OnApplyModeClear(MethodArgs methodArgs)
		{
			this.activeApplyMode = ApplyToPatchState.ApplyMode.Clear;
		}

		// Token: 0x06000E18 RID: 3608 RVA: 0x000747DC File Offset: 0x000737DC
		private void OnItemListSelected(MethodArgs methodArgs)
		{
			this.currentItem = (methodArgs.sender as string);
		}

		// Token: 0x06000E19 RID: 3609 RVA: 0x000747F0 File Offset: 0x000737F0
		private void OnItemListCleared(MethodArgs methodArgs)
		{
			this.currentItem = string.Empty;
		}

		// Token: 0x06000E1A RID: 3610 RVA: 0x000747FD File Offset: 0x000737FD
		private void OnItemListRefreshing(MethodArgs methodArgs)
		{
			if (this.itemListSource != null)
			{
				this.RefreshItemListSource();
			}
		}

		// Token: 0x06000E1B RID: 3611 RVA: 0x0007480D File Offset: 0x0007380D
		private void OnItemEditChoosed(MethodArgs methodArgs)
		{
			if (this.mapEditState.Context != null)
			{
				this.mapEditState.Context.SelectExistingObjectInDatabaseEditor(methodArgs.sender as string);
			}
		}

		// Token: 0x06000E1C RID: 3612 RVA: 0x00074838 File Offset: 0x00073838
		private void OnAddItemChoosed(MethodArgs methodArgs)
		{
			if (this.mapEditState.Context != null)
			{
				string filePath = this.mapEditState.Context.CreateAndBrowseItemInXDBBrowse(this.mapEditState.Map.Data.ContinentName, this.GetItemListSourceFolder(), this.GetClassTypeName(), methodArgs.form);
				if (!string.IsNullOrEmpty(filePath) && this.mapEditState.Context != null)
				{
					this.mapEditState.Context.SelectExistingObjectInDatabaseEditor(filePath);
				}
			}
		}

		// Token: 0x06000E1D RID: 3613 RVA: 0x000748B1 File Offset: 0x000738B1
		private void OnRemoveItemChoosed(MethodArgs methodArgs)
		{
			if (this.mapEditState.Context != null)
			{
				this.mapEditState.Context.UnselectAndRemoveObjectInDatabaseEditor(methodArgs.sender as string);
				this.RecreateItemList();
			}
		}

		// Token: 0x06000E1E RID: 3614 RVA: 0x000748E4 File Offset: 0x000738E4
		private void OnSizeChanged(MethodArgs methodArg)
		{
			int size = 0;
			try
			{
				size = (int)Convert.ToInt16(methodArg.sender);
			}
			catch (FormatException)
			{
			}
			if (size == 0)
			{
				this.patchRegion.Type = PatchRegionType.Polygon;
				return;
			}
			this.patchRegion.SquareSize = (int)Convert.ToInt16(methodArg.sender);
			this.patchRegion.Type = PatchRegionType.Square;
		}

		// Token: 0x06000E1F RID: 3615 RVA: 0x00074948 File Offset: 0x00073948
		private void OnMouseWheel(MethodArgs methodArgs)
		{
			MouseEventArgs mouseEventArgs = methodArgs.eventArgs as MouseEventArgs;
			KeyStatus.ClearCache();
			if (mouseEventArgs != null && KeyStatus.Shift)
			{
				this.AdditionalMouseWheel(mouseEventArgs);
			}
		}

		// Token: 0x06000E20 RID: 3616 RVA: 0x00074978 File Offset: 0x00073978
		private void OnMouseClick(MethodArgs methodArgs)
		{
			MouseEventArgs mouseEventArgs = methodArgs.eventArgs as MouseEventArgs;
			Tools.Geometry.Point tile;
			if (mouseEventArgs != null && mouseEventArgs.Button == MouseButtons.Middle && this.mapEditState.Map.Data.GetTile(this.patchRegion.SquareCenter, out tile) && this.mapEditState.Context != null)
			{
				this.AdditionalMiddleMouse(tile);
			}
		}

		// Token: 0x06000E21 RID: 3617 RVA: 0x000749DC File Offset: 0x000739DC
		private void OnMouseDoubleClick(MethodArgs methodArgs)
		{
			MouseEventArgs mouseEventArgs = methodArgs.eventArgs as MouseEventArgs;
			Tools.Geometry.Point tile;
			if (mouseEventArgs != null && mouseEventArgs.Button == MouseButtons.Middle && this.mapEditState.Map.Data.GetTile(this.patchRegion.SquareCenter, out tile) && this.mapEditState.Context != null)
			{
				this.AdditionalDoubleMiddleMouse(tile);
			}
		}

		// Token: 0x06000E22 RID: 3618 RVA: 0x00074A40 File Offset: 0x00073A40
		private void OnFocusOn(MethodArgs methodArgs)
		{
			Position position = new Position(this.patchRegion.SquareCenter);
			Vec3 distance = new Vec3(0.0, -10.0, 10.0);
			this.mapEditState.Context.EditorScene.SetAnchor(this.mapEditState.Context.EditorSceneViewID, ref position, ref distance);
		}

		// Token: 0x06000E23 RID: 3619 RVA: 0x00074AB4 File Offset: 0x00073AB4
		private void UpdatePosition(int x, int y)
		{
			Position position;
			this.mapEditState.Map.MapEditorMapObjectContainer.PickPosition(0, x, y, TerrainSurface.Terrain, out position);
			this.patchRegion.SquareCenter = position.Vec3;
			if (this.mapEditState.Context.Statusbar.StatusPosition != null)
			{
				this.mapEditState.Context.Statusbar.StatusPosition.Text = string.Format("x:{0:0.###} y:{1:0.###} z:{2:0.###}, patch:[{3}, {4}]", new object[]
				{
					this.patchRegion.SquareCenter.X,
					this.patchRegion.SquareCenter.Y,
					this.patchRegion.SquareCenter.Z,
					(int)(this.patchRegion.SquareCenter.X / (double)Constants.PatchSize),
					(int)(this.patchRegion.SquareCenter.Y / (double)Constants.PatchSize)
				});
			}
			Tools.Geometry.Point tile;
			if (this.mapEditState.Map.Data.GetTile(this.patchRegion.SquareCenter, out tile))
			{
				string item = this.GetItemForStatusMessage(tile);
				if (!string.IsNullOrEmpty(item))
				{
					ItemData itemData = this.MapEditState.Context.ItemDataContainer.GetItemData(item);
					this.MapEditState.Context.Statusbar.StatusMessage.Text = itemData.Text;
				}
				else
				{
					this.MapEditState.Context.Statusbar.StatusMessage.Text = string.Empty;
				}
			}
			this.mapEditState.Context.Statusbar.UpdateStatusbar();
		}

		// Token: 0x06000E24 RID: 3620 RVA: 0x00074C79 File Offset: 0x00073C79
		private void Begin()
		{
			this.operationIsAllowed = (this.mapEditState.Context.EditorSceneParams.ActiveLightmapState == this.GetActiveLightmapState());
			this.mapEditState.Map.OperationContainer.BeginTransaction();
		}

		// Token: 0x06000E25 RID: 3621 RVA: 0x00074CB4 File Offset: 0x00073CB4
		private void End(bool applyChanges)
		{
			if (applyChanges)
			{
				if (this.mapEditState.Map.OperationContainer.EndTransaction() && base.Container != null)
				{
					this.ApplyChanges(true);
					return;
				}
			}
			else if (this.mapEditState.Map.OperationContainer.CancelTransaction() && base.Container != null)
			{
				this.ApplyChanges(false);
			}
		}

		// Token: 0x06000E26 RID: 3622 RVA: 0x00074D14 File Offset: 0x00073D14
		private void Apply()
		{
			if (this.operationIsAllowed && (this.activeApplyMode == ApplyToPatchState.ApplyMode.Clear || !string.IsNullOrEmpty(this.currentItem)))
			{
				bool result = false;
				List<Tools.Geometry.Point> tiles = null;
				if (this.patchRegion.Type == PatchRegionType.Polygon)
				{
					result = this.mapEditState.Map.Data.GetEditedTiles(this.patchRegion.Polygon, out tiles);
				}
				else if (this.patchRegion.Type == PatchRegionType.Square)
				{
					result = this.mapEditState.Map.Data.GetEditedTiles(this.patchRegion.SquareCenter, this.patchRegion.SquareSize, out tiles);
				}
				if (result)
				{
					this.Apply(tiles, (this.activeApplyMode == ApplyToPatchState.ApplyMode.Clear) ? string.Empty : this.currentItem);
				}
			}
		}

		// Token: 0x06000E27 RID: 3623 RVA: 0x00074DD8 File Offset: 0x00073DD8
		protected ApplyToPatchState(string _stateLabel, MapEditState _mapEditState) : base(_stateLabel)
		{
			this.mapEditState = _mapEditState;
			this.EnterState = (State.ActivateEvent)Delegate.Combine(this.EnterState, new State.ActivateEvent(this.OnEnterState));
			this.LeaveState = (State.ActivateEvent)Delegate.Combine(this.LeaveState, new State.ActivateEvent(this.OnLeaveState));
			this.AddMethods();
			base.AddMethod("mouse_wheel", new Method(this.OnMouseWheel));
			base.AddMethod("mouse_click", new Method(this.OnMouseClick));
			base.AddMethod("mouse_double_click", new Method(this.OnMouseDoubleClick));
			base.AddMethod("focus_on", new Method(this.OnFocusOn));
			this.BindItemList();
			this.CreatePatchRegion();
			this.CreateStates();
			this.mainDb = IDatabase.GetMainDatabase();
			if (this.mainDb != null)
			{
				this.dbEventsGenerator = new DbEventsGenerator(this.mainDb);
				this.dbEventsGenerator.DBObjectChanged += this.OnDbItemChanged;
				this.dbEventsGenerator.DBObjectAdded += this.OnDbItemAdded;
				this.dbEventsGenerator.DBObjectRemoved += this.OnDbItemRemoved;
				this.dbEventsGenerator.DBAfterObjectRemoved += this.OnDbAfterItemRemoved;
			}
		}

		// Token: 0x06000E28 RID: 3624 RVA: 0x00074F44 File Offset: 0x00073F44
		public void Destroy()
		{
			if (this.mapEditState == null)
			{
				return;
			}
			this.DestroyStates();
			this.DestroyPatchRegion();
			this.UnbindItemList();
			this.EnterState = (State.ActivateEvent)Delegate.Remove(this.EnterState, new State.ActivateEvent(this.OnEnterState));
			this.LeaveState = (State.ActivateEvent)Delegate.Remove(this.LeaveState, new State.ActivateEvent(this.OnLeaveState));
			if (this.dbEventsGenerator != null)
			{
				this.dbEventsGenerator.DBObjectChanged -= this.OnDbItemChanged;
				this.dbEventsGenerator.DBObjectAdded -= this.OnDbItemAdded;
				this.dbEventsGenerator.DBObjectRemoved -= this.OnDbItemRemoved;
				this.dbEventsGenerator.DBAfterObjectRemoved -= this.OnDbAfterItemRemoved;
				this.dbEventsGenerator = null;
			}
			this.mainDb = null;
			this.mapEditState = null;
		}

		// Token: 0x170002A1 RID: 673
		// (get) Token: 0x06000E29 RID: 3625 RVA: 0x00075029 File Offset: 0x00074029
		protected MapEditState MapEditState
		{
			get
			{
				return this.mapEditState;
			}
		}

		// Token: 0x170002A2 RID: 674
		// (get) Token: 0x06000E2A RID: 3626 RVA: 0x00075031 File Offset: 0x00074031
		// (set) Token: 0x06000E2B RID: 3627 RVA: 0x00075039 File Offset: 0x00074039
		protected ItemList.IItemSource ItemListSource
		{
			get
			{
				return this.itemListSource;
			}
			set
			{
				this.itemListSource = value;
			}
		}

		// Token: 0x06000E2C RID: 3628
		protected abstract void LoadItemList();

		// Token: 0x06000E2D RID: 3629
		protected abstract string GetMultiStateName();

		// Token: 0x06000E2E RID: 3630
		protected abstract string GetApplyStateName();

		// Token: 0x06000E2F RID: 3631
		protected abstract void CreateItemListSource();

		// Token: 0x06000E30 RID: 3632
		protected abstract void DestroyItemListSource();

		// Token: 0x06000E31 RID: 3633
		protected abstract void RefreshItemListSource();

		// Token: 0x06000E32 RID: 3634
		protected abstract string GetClassTypeName();

		// Token: 0x06000E33 RID: 3635
		protected abstract string GetItemListSourceFolder();

		// Token: 0x06000E34 RID: 3636
		protected abstract void ChangeAdditionalDBData(string item);

		// Token: 0x06000E35 RID: 3637
		protected abstract EditorSceneParams.LightmapState GetActiveLightmapState();

		// Token: 0x06000E36 RID: 3638
		protected abstract void AdditionalEnterState();

		// Token: 0x06000E37 RID: 3639
		protected abstract void AdditionalMouseWheel(MouseEventArgs mouseEventArgs);

		// Token: 0x06000E38 RID: 3640
		protected abstract void AdditionalMiddleMouse(Tools.Geometry.Point tile);

		// Token: 0x06000E39 RID: 3641
		protected abstract void AdditionalDoubleMiddleMouse(Tools.Geometry.Point tile);

		// Token: 0x06000E3A RID: 3642
		protected abstract string GetItemForStatusMessage(Tools.Geometry.Point tile);

		// Token: 0x06000E3B RID: 3643
		protected abstract void ApplyChanges(bool apply);

		// Token: 0x06000E3C RID: 3644
		protected abstract void Apply(List<Tools.Geometry.Point> tiles, string item);

		// Token: 0x06000E3D RID: 3645
		protected abstract string GetApplyModeSetLabel();

		// Token: 0x06000E3E RID: 3646
		protected abstract string GetApplyModeClearLabel();

		// Token: 0x06000E3F RID: 3647
		protected abstract string GetItemListSelectedLabel();

		// Token: 0x06000E40 RID: 3648
		protected abstract string GetItemListClearedLabel();

		// Token: 0x06000E41 RID: 3649
		protected abstract string GetItemListRefreshingLabel();

		// Token: 0x06000E42 RID: 3650
		protected abstract string GetItemEditChoosedLabel();

		// Token: 0x06000E43 RID: 3651
		protected abstract string GetAddItemChoosedLabel();

		// Token: 0x06000E44 RID: 3652
		protected abstract string GetRemoveItemChoosedLabel();

		// Token: 0x06000E45 RID: 3653
		protected abstract string GetSizeChangedLabel();

		// Token: 0x04000B1A RID: 2842
		private MapEditState mapEditState;

		// Token: 0x04000B1B RID: 2843
		private MultiState multiState;

		// Token: 0x04000B1C RID: 2844
		private List<IState> states;

		// Token: 0x04000B1D RID: 2845
		private ItemList.IItemSource itemListSource;

		// Token: 0x04000B1E RID: 2846
		private string currentItem = string.Empty;

		// Token: 0x04000B1F RID: 2847
		private bool operationIsAllowed;

		// Token: 0x04000B20 RID: 2848
		private ApplyToPatchState.ApplyMode activeApplyMode;

		// Token: 0x04000B21 RID: 2849
		private EditorSceneParams.LightmapState previousLightmapState;

		// Token: 0x04000B22 RID: 2850
		private IDatabase mainDb;

		// Token: 0x04000B23 RID: 2851
		private DbEventsGenerator dbEventsGenerator;

		// Token: 0x04000B24 RID: 2852
		private DBID removingItemDBID = DBID.Empty;

		// Token: 0x04000B25 RID: 2853
		private ApplyToPatchState.ApplyState applyState;

		// Token: 0x04000B26 RID: 2854
		private ApplyToPatchState.AddPolygonSubstate addPolygonSubstate;

		// Token: 0x04000B27 RID: 2855
		private ApplyToPatchState.EditPolygonSubstate editPolygonSubstate;

		// Token: 0x04000B28 RID: 2856
		private PatchRegion patchRegion;

		// Token: 0x0200011E RID: 286
		private enum ApplyMode
		{
			// Token: 0x04000B2A RID: 2858
			Set,
			// Token: 0x04000B2B RID: 2859
			Clear
		}

		// Token: 0x0200011F RID: 287
		internal class PolygonSubstate : State
		{
			// Token: 0x170002A3 RID: 675
			// (get) Token: 0x06000E46 RID: 3654 RVA: 0x00075042 File Offset: 0x00074042
			public PatchRegion PatchRegion
			{
				get
				{
					return this.parentState.patchRegion;
				}
			}

			// Token: 0x170002A4 RID: 676
			// (get) Token: 0x06000E47 RID: 3655 RVA: 0x0007504F File Offset: 0x0007404F
			public ICollisionMap CollisionMap
			{
				get
				{
					return this.parentState.mapEditState.Map.MapEditorMapObjectContainer;
				}
			}

			// Token: 0x170002A5 RID: 677
			// (get) Token: 0x06000E48 RID: 3656 RVA: 0x00075066 File Offset: 0x00074066
			// (set) Token: 0x06000E49 RID: 3657 RVA: 0x0007506E File Offset: 0x0007406E
			public string PreviousStateLabel
			{
				get
				{
					return this.previousStateLabel;
				}
				set
				{
					this.previousStateLabel = value;
				}
			}

			// Token: 0x170002A6 RID: 678
			// (get) Token: 0x06000E4A RID: 3658 RVA: 0x00075077 File Offset: 0x00074077
			// (set) Token: 0x06000E4B RID: 3659 RVA: 0x0007507F File Offset: 0x0007407F
			public Vec3 LastPosition
			{
				get
				{
					return this.lastPosition;
				}
				set
				{
					this.lastPosition = value;
				}
			}

			// Token: 0x06000E4C RID: 3660 RVA: 0x00075088 File Offset: 0x00074088
			public void UpdateLastPosition(int x, int y)
			{
				if (this.CollisionMap != null)
				{
					Position _position;
					this.CollisionMap.PickPosition(x, y, TerrainSurface.Terrain, out _position);
					this.lastPosition = _position.Vec3;
				}
			}

			// Token: 0x06000E4D RID: 3661 RVA: 0x000750BC File Offset: 0x000740BC
			public void UpdateLastPosition()
			{
				Tools.Geometry.Point mouseCursorPos;
				this.parentState.mapEditState.Context.EditorScene.GetMouseCursorPos(this.parentState.mapEditState.Context.EditorSceneViewID, out mouseCursorPos);
				this.UpdateLastPosition(mouseCursorPos.X, mouseCursorPos.Y);
			}

			// Token: 0x170002A7 RID: 679
			// (get) Token: 0x06000E4E RID: 3662 RVA: 0x0007510E File Offset: 0x0007410E
			public ApplyToPatchState.PolygonSubstate.PolygonBackup Backup
			{
				get
				{
					return this.backup;
				}
			}

			// Token: 0x170002A8 RID: 680
			// (get) Token: 0x06000E4F RID: 3663 RVA: 0x00075116 File Offset: 0x00074116
			// (set) Token: 0x06000E50 RID: 3664 RVA: 0x0007511E File Offset: 0x0007411E
			public bool Restore
			{
				get
				{
					return this.restore;
				}
				set
				{
					this.restore = value;
				}
			}

			// Token: 0x170002A9 RID: 681
			// (get) Token: 0x06000E51 RID: 3665 RVA: 0x00075127 File Offset: 0x00074127
			public Polygon Polygon
			{
				get
				{
					return this.parentState.patchRegion.Polygon;
				}
			}

			// Token: 0x170002AA RID: 682
			// (get) Token: 0x06000E52 RID: 3666 RVA: 0x00075139 File Offset: 0x00074139
			public PolygonContainer PolygonContainer
			{
				get
				{
					return this.parentState.patchRegion.PolygonContainer;
				}
			}

			// Token: 0x06000E53 RID: 3667 RVA: 0x0007514B File Offset: 0x0007414B
			public PolygonSubstate(string _stateLabel, ApplyToPatchState _parentState) : base(_stateLabel)
			{
				this.parentState = _parentState;
			}

			// Token: 0x06000E54 RID: 3668 RVA: 0x00075183 File Offset: 0x00074183
			public virtual void Destroy()
			{
				this.parentState = null;
			}

			// Token: 0x06000E55 RID: 3669 RVA: 0x0007518C File Offset: 0x0007418C
			public void InvokeLeave()
			{
				if (this.parentState.multiState != null)
				{
					this.parentState.multiState.ActiveStateLabel = this.previousStateLabel;
				}
			}

			// Token: 0x04000B2C RID: 2860
			private readonly ApplyToPatchState.PolygonSubstate.PolygonBackup backup = new ApplyToPatchState.PolygonSubstate.PolygonBackup();

			// Token: 0x04000B2D RID: 2861
			private bool restore = true;

			// Token: 0x04000B2E RID: 2862
			private ApplyToPatchState parentState;

			// Token: 0x04000B2F RID: 2863
			private string previousStateLabel = string.Empty;

			// Token: 0x04000B30 RID: 2864
			private Vec3 lastPosition = Vec3.Empty;

			// Token: 0x02000120 RID: 288
			internal class PolygonBackup
			{
				// Token: 0x06000E56 RID: 3670 RVA: 0x000751B1 File Offset: 0x000741B1
				public void Backup(Polygon polygon)
				{
					if (polygon != null)
					{
						this.points.Clear();
						this.points.AddRange(polygon.Points);
						this.previousColor = polygon.Color;
					}
				}

				// Token: 0x06000E57 RID: 3671 RVA: 0x000751DE File Offset: 0x000741DE
				public void Restore(Polygon polygon, bool restorePoints)
				{
					if (polygon != null)
					{
						polygon.Color = this.previousColor;
						if (restorePoints)
						{
							polygon.Clear();
							if (this.points.Count > 2)
							{
								polygon.Points.AddRange(this.points);
							}
						}
					}
				}

				// Token: 0x06000E58 RID: 3672 RVA: 0x00075218 File Offset: 0x00074218
				public void Clear()
				{
					this.previousColor = Color.White;
					this.points.Clear();
				}

				// Token: 0x170002AB RID: 683
				// (get) Token: 0x06000E59 RID: 3673 RVA: 0x00075230 File Offset: 0x00074230
				public bool Empty
				{
					get
					{
						return this.points.Count < 3;
					}
				}

				// Token: 0x04000B31 RID: 2865
				private Color previousColor = Color.White;

				// Token: 0x04000B32 RID: 2866
				private readonly List<Vec3> points = new List<Vec3>();
			}
		}

		// Token: 0x02000121 RID: 289
		internal class AddPolygonSubstate : ApplyToPatchState.PolygonSubstate
		{
			// Token: 0x06000E5B RID: 3675 RVA: 0x0007525E File Offset: 0x0007425E
			private void MoveLastPointTo(int x, int y)
			{
				base.UpdateLastPosition(x, y);
				if (base.Polygon.Count > 0)
				{
					base.Polygon.MovePointTo(base.Polygon.Count - 1, base.LastPosition);
				}
			}

			// Token: 0x06000E5C RID: 3676 RVA: 0x00075295 File Offset: 0x00074295
			private void RemovePoint()
			{
				if (base.Polygon.Count > 1)
				{
					base.Polygon.DeletePoint(base.Polygon.Count - 2);
				}
			}

			// Token: 0x06000E5D RID: 3677 RVA: 0x000752C0 File Offset: 0x000742C0
			private void AddPoint()
			{
				if (base.Polygon.Count > 0)
				{
					base.Polygon.InsertPoint(base.Polygon.Count - 1, base.LastPosition, true, false);
					return;
				}
				base.Polygon.AddPoint(base.LastPosition, false, false);
			}

			// Token: 0x06000E5E RID: 3678 RVA: 0x00075314 File Offset: 0x00074314
			private void OnEnterState(IState state)
			{
				base.Backup.Backup(base.Polygon);
				base.Polygon.Clear();
				base.Polygon.Color = PatchRegion.AddRegionColor;
				base.Restore = true;
				base.UpdateLastPosition();
				this.AddPoint();
			}

			// Token: 0x06000E5F RID: 3679 RVA: 0x00075361 File Offset: 0x00074361
			private void OnLeaveState(IState state)
			{
				base.Backup.Restore(base.Polygon, base.Restore);
				base.Restore = true;
			}

			// Token: 0x06000E60 RID: 3680 RVA: 0x00075384 File Offset: 0x00074384
			private void OnMouseMove(MethodArgs methodArgs)
			{
				MouseEventArgs mouseEventArgs = methodArgs.eventArgs as MouseEventArgs;
				if (mouseEventArgs != null && !KeyStatus.RightMouse)
				{
					this.MoveLastPointTo(mouseEventArgs.X, mouseEventArgs.Y);
				}
			}

			// Token: 0x06000E61 RID: 3681 RVA: 0x000753BC File Offset: 0x000743BC
			private void OnMouseUp(MethodArgs methodArgs)
			{
				MouseEventArgs mouseEventArgs = methodArgs.eventArgs as MouseEventArgs;
				if (mouseEventArgs != null)
				{
					if (mouseEventArgs.Button == MouseButtons.Middle && base.Polygon.Count > 1)
					{
						this.RemovePoint();
						this.MoveLastPointTo(mouseEventArgs.X, mouseEventArgs.Y);
					}
					if (mouseEventArgs.Button == MouseButtons.Left)
					{
						this.MoveLastPointTo(mouseEventArgs.X, mouseEventArgs.Y);
						this.AddPoint();
						this.MoveLastPointTo(mouseEventArgs.X, mouseEventArgs.Y);
					}
				}
			}

			// Token: 0x06000E62 RID: 3682 RVA: 0x00075444 File Offset: 0x00074444
			private void OnMouseDoubleClick(MethodArgs methodArgs)
			{
				MouseEventArgs mouseEventArgs = methodArgs.eventArgs as MouseEventArgs;
				if (mouseEventArgs != null && (mouseEventArgs.Button == MouseButtons.Left || mouseEventArgs.Button == MouseButtons.Middle || mouseEventArgs.Button == MouseButtons.Right))
				{
					if (base.Polygon.Count > 1)
					{
						base.Polygon.DeletePoint(base.Polygon.Count - 1);
					}
					this.InvokeLeave(false, false);
				}
			}

			// Token: 0x06000E63 RID: 3683 RVA: 0x000754B7 File Offset: 0x000744B7
			private void OnProperties(MethodArgs methodArgs)
			{
				this.InvokeLeave(false, false);
			}

			// Token: 0x06000E64 RID: 3684 RVA: 0x000754C1 File Offset: 0x000744C1
			private void OnBreak(MethodArgs methodArgs)
			{
				this.InvokeLeave(true, false);
			}

			// Token: 0x06000E65 RID: 3685 RVA: 0x000754CC File Offset: 0x000744CC
			public AddPolygonSubstate(ApplyToPatchState _parentState) : base("AddPolygonSubstate", _parentState)
			{
				base.AddMethod("mouse_move", new Method(this.OnMouseMove));
				base.AddMethod("mouse_up", new Method(this.OnMouseUp));
				base.AddMethod("mouse_double_click", new Method(this.OnMouseDoubleClick));
				base.AddMethod("properties", new Method(this.OnProperties));
				base.AddMethod("break", new Method(this.OnBreak));
				this.EnterState = (State.ActivateEvent)Delegate.Combine(this.EnterState, new State.ActivateEvent(this.OnEnterState));
				this.LeaveState = (State.ActivateEvent)Delegate.Combine(this.LeaveState, new State.ActivateEvent(this.OnLeaveState));
			}

			// Token: 0x06000E66 RID: 3686 RVA: 0x0007559C File Offset: 0x0007459C
			public override void Destroy()
			{
				this.EnterState = (State.ActivateEvent)Delegate.Remove(this.EnterState, new State.ActivateEvent(this.OnEnterState));
				this.LeaveState = (State.ActivateEvent)Delegate.Remove(this.LeaveState, new State.ActivateEvent(this.OnLeaveState));
				base.Destroy();
			}

			// Token: 0x06000E67 RID: 3687 RVA: 0x000755F4 File Offset: 0x000745F4
			public void InvokeLeave(bool _restore, bool force)
			{
				if (!force && ((_restore && base.Backup.Empty) || (!_restore && base.Polygon.Count < 3)))
				{
					base.Polygon.Clear();
					base.UpdateLastPosition();
					this.AddPoint();
					return;
				}
				base.Restore = _restore;
				base.InvokeLeave();
			}
		}

		// Token: 0x02000122 RID: 290
		internal class EditPolygonSubstate : ApplyToPatchState.PolygonSubstate
		{
			// Token: 0x06000E68 RID: 3688 RVA: 0x0007564C File Offset: 0x0007464C
			private void OnEnterState(IState state)
			{
				base.Backup.Backup(base.Polygon);
				base.Polygon.Color = PatchRegion.EditRegionColor;
				base.Polygon.Selection = this.pointTested;
				base.UpdateLastPosition();
				Vec3 position = base.LastPosition;
				if (base.Polygon.Selection != -1)
				{
					this.polygonStartData.Create(base.Polygon, ref position);
				}
				else if (this.centerTested)
				{
					if (base.Polygon.ClassifyPoint(ref position) == PerimeterArea.Outside)
					{
						base.Polygon.MoveTo(position);
					}
					this.polygonStartData.Create(base.Polygon, ref position);
				}
				base.Restore = true;
			}

			// Token: 0x06000E69 RID: 3689 RVA: 0x000756FA File Offset: 0x000746FA
			private void OnLeaveState(IState state)
			{
				base.Backup.Restore(base.Polygon, base.Restore);
				base.Restore = true;
				this.pointTested = -1;
				this.centerTested = false;
			}

			// Token: 0x06000E6A RID: 3690 RVA: 0x00075728 File Offset: 0x00074728
			private void OnMouseMove(MethodArgs methodArgs)
			{
				MouseEventArgs mouseEventArgs = methodArgs.eventArgs as MouseEventArgs;
				if (mouseEventArgs != null && !KeyStatus.RightMouse)
				{
					base.UpdateLastPosition(mouseEventArgs.X, mouseEventArgs.Y);
					if (base.Polygon.Selection != -1)
					{
						if (KeyStatus.Control)
						{
							base.Polygon.MoveTo(base.LastPosition - this.polygonStartData.CenterDiff);
							return;
						}
						if (!KeyStatus.Shift)
						{
							base.Polygon.MovePointTo(base.Polygon.Selection, base.LastPosition - this.polygonStartData.PointDiff);
							return;
						}
						Vec3 direction = base.LastPosition - this.polygonStartData.StartPosition;
						double length = direction.Normalize();
						if (length > MathConsts.DOUBLE_EPSILON)
						{
							Vec3 bisector;
							double bisectorLength;
							base.Polygon.GetCounterclockwiseBisector(base.Polygon.Selection, out bisector, out bisectorLength);
							if (bisector.Length2 > MathConsts.DOUBLE_EPSILON_2)
							{
								length = length * Vec3.Dot(bisector, direction) * ((base.Polygon.Rotation == RotationType.Clockwise) ? -1.0 : 1.0);
								base.Polygon.Enlarge(this.polygonStartData.StartPoints, length);
								return;
							}
						}
					}
					else if (KeyStatus.LeftMouse || KeyStatus.MiddleMouse)
					{
						base.Polygon.MoveTo(base.LastPosition - this.polygonStartData.CenterDiff);
					}
				}
			}

			// Token: 0x06000E6B RID: 3691 RVA: 0x000758A4 File Offset: 0x000748A4
			private void OnMouseDown(MethodArgs methodArgs)
			{
				MouseEventArgs mouseEventArgs = methodArgs.eventArgs as MouseEventArgs;
				if (mouseEventArgs != null && mouseEventArgs.Button != MouseButtons.Right)
				{
					base.UpdateLastPosition(mouseEventArgs.X, mouseEventArgs.Y);
					if (mouseEventArgs.Button == MouseButtons.Left || mouseEventArgs.Button == MouseButtons.Middle)
					{
						Vec3 position = base.LastPosition;
						base.Polygon.Selection = base.Polygon.LocateNearestPoint(ref position, 1.0);
						this.polygonStartData.Create(base.Polygon, ref position);
					}
				}
			}

			// Token: 0x06000E6C RID: 3692 RVA: 0x00075938 File Offset: 0x00074938
			private void OnMouseUp(MethodArgs methodArgs)
			{
				MouseEventArgs mouseEventArgs = methodArgs.eventArgs as MouseEventArgs;
				if (mouseEventArgs != null && mouseEventArgs.Button != MouseButtons.Right)
				{
					base.Polygon.Selection = -1;
					if (this.pointTested != -1)
					{
						this.InvokeLeave(false);
						return;
					}
					if (this.centerTested)
					{
						this.InvokeLeave(false);
					}
				}
			}

			// Token: 0x06000E6D RID: 3693 RVA: 0x00075990 File Offset: 0x00074990
			private void OnMouseDoubleClick(MethodArgs methodArgs)
			{
				MouseEventArgs mouseEventArgs = methodArgs.eventArgs as MouseEventArgs;
				if (mouseEventArgs != null && (mouseEventArgs.Button == MouseButtons.Left || mouseEventArgs.Button == MouseButtons.Middle || mouseEventArgs.Button == MouseButtons.Right))
				{
					this.InvokeLeave(false);
				}
			}

			// Token: 0x06000E6E RID: 3694 RVA: 0x000759DB File Offset: 0x000749DB
			private void OnProperties(MethodArgs methodArgs)
			{
				this.InvokeLeave(false);
			}

			// Token: 0x06000E6F RID: 3695 RVA: 0x000759E4 File Offset: 0x000749E4
			private void OnBreak(MethodArgs methodArgs)
			{
				this.InvokeLeave(true);
			}

			// Token: 0x06000E70 RID: 3696 RVA: 0x000759F0 File Offset: 0x000749F0
			private void OnAddPoint(MethodArgs methodArgs)
			{
				if (base.Polygon.Selection != -1)
				{
					Vec3 newPoint;
					if (base.Polygon.Selection == 0)
					{
						newPoint = (base.Polygon.Points[base.Polygon.Count - 1] + base.Polygon.Points[0]) / 2.0;
					}
					else
					{
						newPoint = (base.Polygon.Points[base.Polygon.Selection - 1] + base.Polygon.Points[base.Polygon.Selection]) / 2.0;
					}
					newPoint.Z = base.CollisionMap.GetTerrainHeight(newPoint.X, newPoint.Y);
					base.Polygon.InsertPoint(base.Polygon.Selection, newPoint, true, false);
					base.Polygon.Selection++;
				}
			}

			// Token: 0x06000E71 RID: 3697 RVA: 0x00075AF8 File Offset: 0x00074AF8
			private void OnRemovePoint(MethodArgs methodArgs)
			{
				if (base.Polygon.Count > 3 && base.Polygon.Selection != -1)
				{
					base.Polygon.DeletePoint(base.Polygon.Selection);
					base.Polygon.Selection = -1;
					if (this.pointTested != -1)
					{
						this.InvokeLeave(false);
					}
				}
			}

			// Token: 0x06000E72 RID: 3698 RVA: 0x00075B54 File Offset: 0x00074B54
			public EditPolygonSubstate(ApplyToPatchState _parentState) : base("EditPolygonSubstate", _parentState)
			{
				base.AddMethod("mouse_move", new Method(this.OnMouseMove));
				base.AddMethod("mouse_down", new Method(this.OnMouseDown));
				base.AddMethod("mouse_up", new Method(this.OnMouseUp));
				base.AddMethod("mouse_double_click", new Method(this.OnMouseDoubleClick));
				base.AddMethod("properties", new Method(this.OnProperties));
				base.AddMethod("add", new Method(this.OnAddPoint));
				base.AddMethod("remove", new Method(this.OnRemovePoint));
				base.AddMethod("break", new Method(this.OnBreak));
				this.EnterState = (State.ActivateEvent)Delegate.Combine(this.EnterState, new State.ActivateEvent(this.OnEnterState));
				this.LeaveState = (State.ActivateEvent)Delegate.Combine(this.LeaveState, new State.ActivateEvent(this.OnLeaveState));
			}

			// Token: 0x06000E73 RID: 3699 RVA: 0x00075C7C File Offset: 0x00074C7C
			public override void Destroy()
			{
				this.EnterState = (State.ActivateEvent)Delegate.Remove(this.EnterState, new State.ActivateEvent(this.OnEnterState));
				this.LeaveState = (State.ActivateEvent)Delegate.Remove(this.LeaveState, new State.ActivateEvent(this.OnLeaveState));
				base.Destroy();
			}

			// Token: 0x06000E74 RID: 3700 RVA: 0x00075CD3 File Offset: 0x00074CD3
			public void InvokeLeave(bool _restore)
			{
				base.Restore = _restore;
				base.InvokeLeave();
			}

			// Token: 0x170002AC RID: 684
			// (get) Token: 0x06000E75 RID: 3701 RVA: 0x00075CE2 File Offset: 0x00074CE2
			// (set) Token: 0x06000E76 RID: 3702 RVA: 0x00075CEA File Offset: 0x00074CEA
			public int PointTested
			{
				get
				{
					return this.pointTested;
				}
				set
				{
					this.pointTested = value;
				}
			}

			// Token: 0x170002AD RID: 685
			// (get) Token: 0x06000E77 RID: 3703 RVA: 0x00075CF3 File Offset: 0x00074CF3
			// (set) Token: 0x06000E78 RID: 3704 RVA: 0x00075CFB File Offset: 0x00074CFB
			public bool CenterTested
			{
				get
				{
					return this.centerTested;
				}
				set
				{
					this.centerTested = value;
				}
			}

			// Token: 0x04000B33 RID: 2867
			private readonly ApplyToPatchState.EditPolygonSubstate.PolygonStartData polygonStartData = new ApplyToPatchState.EditPolygonSubstate.PolygonStartData();

			// Token: 0x04000B34 RID: 2868
			private int pointTested = -1;

			// Token: 0x04000B35 RID: 2869
			private bool centerTested;

			// Token: 0x02000123 RID: 291
			internal class PolygonStartData
			{
				// Token: 0x170002AE RID: 686
				// (get) Token: 0x06000E79 RID: 3705 RVA: 0x00075D04 File Offset: 0x00074D04
				public List<Vec3> StartPoints
				{
					get
					{
						return this.startPoints;
					}
				}

				// Token: 0x170002AF RID: 687
				// (get) Token: 0x06000E7A RID: 3706 RVA: 0x00075D0C File Offset: 0x00074D0C
				public Vec3 StartPosition
				{
					get
					{
						return this.startPosition;
					}
				}

				// Token: 0x170002B0 RID: 688
				// (get) Token: 0x06000E7B RID: 3707 RVA: 0x00075D14 File Offset: 0x00074D14
				public Vec3 CenterDiff
				{
					get
					{
						return this.centerDiff;
					}
				}

				// Token: 0x170002B1 RID: 689
				// (get) Token: 0x06000E7C RID: 3708 RVA: 0x00075D1C File Offset: 0x00074D1C
				public Vec3 PointDiff
				{
					get
					{
						return this.pointDiff;
					}
				}

				// Token: 0x06000E7D RID: 3709 RVA: 0x00075D24 File Offset: 0x00074D24
				public void Create(Polygon polygon, ref Vec3 position)
				{
					this.startPosition = position;
					this.startPoints.Clear();
					this.startPoints.AddRange(polygon.Points);
					this.centerDiff = this.startPosition - polygon.Center;
					if (polygon.Selection != -1)
					{
						this.pointDiff = this.startPosition - polygon.Points[polygon.Selection];
						return;
					}
					this.pointDiff = Vec3.Empty;
				}

				// Token: 0x04000B36 RID: 2870
				private readonly List<Vec3> startPoints = new List<Vec3>();

				// Token: 0x04000B37 RID: 2871
				private Vec3 startPosition = Vec3.Empty;

				// Token: 0x04000B38 RID: 2872
				private Vec3 centerDiff = Vec3.Empty;

				// Token: 0x04000B39 RID: 2873
				private Vec3 pointDiff = Vec3.Empty;
			}
		}

		// Token: 0x02000124 RID: 292
		internal class ApplyState : State
		{
			// Token: 0x06000E7F RID: 3711 RVA: 0x00075DDC File Offset: 0x00074DDC
			private void OnMouseMove(MethodArgs methodArgs)
			{
				if (this.parentState == null)
				{
					return;
				}
				MouseEventArgs mouseEventArgs = methodArgs.eventArgs as MouseEventArgs;
				if (mouseEventArgs == null)
				{
					return;
				}
				this.parentState.UpdatePosition(mouseEventArgs.X, mouseEventArgs.Y);
				if (!KeyStatus.RightMouse && KeyStatus.LeftMouse)
				{
					if (this.parentState.patchRegion.Type == PatchRegionType.Polygon)
					{
						return;
					}
					if (this.parentState.patchRegion.Type == PatchRegionType.Square)
					{
						this.parentState.Begin();
						this.parentState.Apply();
					}
				}
			}

			// Token: 0x06000E80 RID: 3712 RVA: 0x00075E64 File Offset: 0x00074E64
			private void OnMouseDown(MethodArgs methodArgs)
			{
				if (this.parentState == null)
				{
					return;
				}
				MouseEventArgs mouseEventArgs = methodArgs.eventArgs as MouseEventArgs;
				if (mouseEventArgs == null)
				{
					return;
				}
				this.parentState.UpdatePosition(mouseEventArgs.X, mouseEventArgs.Y);
				if (mouseEventArgs.Button == MouseButtons.Left)
				{
					if (this.parentState.patchRegion.Type == PatchRegionType.Polygon)
					{
						if (this.parentState.HitTestEditPolygonSubtate(base.Label))
						{
							return;
						}
					}
					else if (this.parentState.patchRegion.Type == PatchRegionType.Square)
					{
						this.parentState.Begin();
						this.parentState.Apply();
					}
				}
			}

			// Token: 0x06000E81 RID: 3713 RVA: 0x00075F00 File Offset: 0x00074F00
			private void OnMouseUp(MethodArgs methodArgs)
			{
				if (this.parentState == null)
				{
					return;
				}
				MouseEventArgs mouseEventArgs = methodArgs.eventArgs as MouseEventArgs;
				if (mouseEventArgs == null)
				{
					return;
				}
				if (mouseEventArgs.Button == MouseButtons.Left)
				{
					if (this.parentState.patchRegion.Type == PatchRegionType.Polygon)
					{
						return;
					}
					if (this.parentState.patchRegion.Type == PatchRegionType.Square)
					{
						this.parentState.End(true);
					}
				}
			}

			// Token: 0x06000E82 RID: 3714 RVA: 0x00075F68 File Offset: 0x00074F68
			private void OnProperties(MethodArgs methodArgs)
			{
				if (this.parentState.patchRegion.Type == PatchRegionType.Polygon)
				{
					this.parentState.Begin();
					this.parentState.Apply();
					this.parentState.End(true);
					return;
				}
				if (this.parentState.patchRegion.Type == PatchRegionType.Square && !KeyStatus.LeftMouse)
				{
					this.parentState.Begin();
					this.parentState.Apply();
					this.parentState.End(true);
				}
			}

			// Token: 0x06000E83 RID: 3715 RVA: 0x00075FE6 File Offset: 0x00074FE6
			private void OnMouseDoubleClick(MethodArgs methodArgs)
			{
				if (this.parentState.patchRegion.Type == PatchRegionType.Polygon)
				{
					this.parentState.ActivateEditPolygonSubtate(base.Label);
				}
			}

			// Token: 0x06000E84 RID: 3716 RVA: 0x0007600C File Offset: 0x0007500C
			public ApplyState(string label, ApplyToPatchState _parentState) : base(label)
			{
				this.parentState = _parentState;
				base.AddMethod("mouse_move", new Method(this.OnMouseMove));
				base.AddMethod("mouse_down", new Method(this.OnMouseDown));
				base.AddMethod("mouse_up", new Method(this.OnMouseUp));
				base.AddMethod("properties", new Method(this.OnProperties));
				base.AddMethod("mouse_double_click", new Method(this.OnMouseDoubleClick));
			}

			// Token: 0x04000B3A RID: 2874
			private readonly ApplyToPatchState parentState;
		}
	}
}
