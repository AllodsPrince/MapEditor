using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using Db;
using InputState;
using LauncherTools.InputState;
using MapEditor.Map.DataProviders;
using MapEditor.Map.SaveLoad.DataSources;
using MapEditor.Resources.Strings;
using Tools.Geometry;
using Tools.InputState;
using Tools.ItemDataContainer;
using Tools.MapObjects;
using Tools.MapSound;

namespace MapEditor.Map.States
{
	// Token: 0x0200021E RID: 542
	internal class SoundState : State
	{
		// Token: 0x06001A30 RID: 6704 RVA: 0x000AC9F8 File Offset: 0x000AB9F8
		private void OnDbItemChanged(DBID dbid)
		{
			if (SoundState.mainDb != null && !DBID.IsNullOrEmpty(dbid) && SoundState.mainDb.GetClassTypeName(dbid) == "Sound2DTassel" && this.mapEditState != null)
			{
				string item = dbid.ToString();
				this.mapEditState.Context.ItemDataContainer.RefreshItemData(item);
				this.mapEditState.Container.Invoke("_sound_list_item_changed", new MethodArgs(null, item, null));
				SoundDataSource.Update(this.mapEditState.Map, item);
			}
		}

		// Token: 0x06001A31 RID: 6705 RVA: 0x000ACA7E File Offset: 0x000ABA7E
		private void OnDbItemAdded(DBID dbid)
		{
			if (SoundState.mainDb != null && !DBID.IsNullOrEmpty(dbid) && SoundState.mainDb.GetClassTypeName(dbid) == "Sound2DTassel")
			{
				this.RefreshItemList();
			}
		}

		// Token: 0x06001A32 RID: 6706 RVA: 0x000ACAAC File Offset: 0x000ABAAC
		private void OnDbItemRemoved(DBID dbid)
		{
			if (SoundState.mainDb != null && SoundState.mainDb.GetClassTypeName(dbid) == "ZoneLights")
			{
				this.removingItemDBID = dbid;
			}
		}

		// Token: 0x06001A33 RID: 6707 RVA: 0x000ACAD4 File Offset: 0x000ABAD4
		private void OnDbAfterItemRemoved(DBID dbid)
		{
			if (this.removingItemDBID == dbid && !DBID.IsNullOrEmpty(dbid))
			{
				if (this.mapEditState != null)
				{
					this.mapEditState.Context.ItemDataContainer.RemoveCacheFile(dbid.ToString());
				}
				this.RefreshItemList();
				this.removingItemDBID = DBID.Empty;
			}
		}

		// Token: 0x06001A34 RID: 6708 RVA: 0x000ACB2C File Offset: 0x000ABB2C
		private void RefreshItemList()
		{
			this.UnbindItemList();
			SoundDataSource.LoadList(this.mapEditState.Map, this.mapEditState.Map.MapMusicContainer);
			SoundDataSource.LoadList(this.mapEditState.Map, this.mapEditState.Map.MapAmbienceContainer);
			this.BindItemList();
		}

		// Token: 0x06001A35 RID: 6709 RVA: 0x000ACB88 File Offset: 0x000ABB88
		private void OnMouseWheel(MethodArgs methodArgs)
		{
			MouseEventArgs mouseEventArgs = methodArgs.eventArgs as MouseEventArgs;
			KeyStatus.ClearCache();
			if (mouseEventArgs != null && KeyStatus.Shift)
			{
				if (mouseEventArgs.Delta > 0)
				{
					this.mapEditState.Context.StateContainer.Invoke("_sounds_increase_size", default(MethodArgs));
					return;
				}
				this.mapEditState.Context.StateContainer.Invoke("_sounds_decrease_size", default(MethodArgs));
			}
		}

		// Token: 0x06001A36 RID: 6710 RVA: 0x000ACC04 File Offset: 0x000ABC04
		private void OnMouseClick(MethodArgs methodArgs)
		{
			MouseEventArgs mouseEventArgs = methodArgs.eventArgs as MouseEventArgs;
			Tools.Geometry.Point tile;
			if (mouseEventArgs != null && this.mapSoundContainer != null && mouseEventArgs.Button == MouseButtons.Middle && this.mapEditState.Map.Data.GetTile(this.currentPosition.Vec3, out tile))
			{
				this.mapEditState.Context.StateContainer.Invoke("_sound_list_select_item", new MethodArgs(null, this.mapSoundContainer.GetSound(tile), null));
			}
		}

		// Token: 0x06001A37 RID: 6711 RVA: 0x000ACC88 File Offset: 0x000ABC88
		private void OnMouseDoubleClick(MethodArgs methodArgs)
		{
			MouseEventArgs mouseEventArgs = methodArgs.eventArgs as MouseEventArgs;
			Tools.Geometry.Point tile;
			if ((mouseEventArgs != null & this.mapSoundContainer != null) && mouseEventArgs.Button == MouseButtons.Middle && this.mapEditState.Map.Data.GetTile(this.currentPosition.Vec3, out tile) && this.mapEditState.Context != null)
			{
				this.mapEditState.Context.SelectExistingObjectInDatabaseEditor(this.mapSoundContainer.GetSound(tile));
			}
		}

		// Token: 0x06001A38 RID: 6712 RVA: 0x000ACD12 File Offset: 0x000ABD12
		private void OnItemEditChoosed(MethodArgs methodArgs)
		{
			if (this.mapEditState.Context != null)
			{
				this.mapEditState.Context.SelectExistingObjectInDatabaseEditor(methodArgs.sender as string);
			}
		}

		// Token: 0x06001A39 RID: 6713 RVA: 0x000ACD40 File Offset: 0x000ABD40
		private void OnAddItemChoosed(MethodArgs methodArgs)
		{
			if (this.mapEditState.Context != null)
			{
				string filePath = this.mapEditState.Context.CreateAndBrowseItemInXDBBrowse(this.mapEditState.Map.Data.ContinentName, "Sound", "Sound2DTassel", methodArgs.form);
				if (!string.IsNullOrEmpty(filePath) && this.mapEditState.Context != null)
				{
					this.mapEditState.Context.SelectExistingObjectInDatabaseEditor(filePath);
				}
			}
		}

		// Token: 0x06001A3A RID: 6714 RVA: 0x000ACDB7 File Offset: 0x000ABDB7
		private void OnRemoveItemChoosed(MethodArgs methodArgs)
		{
			if (this.mapEditState.Context != null)
			{
				this.mapEditState.Context.UnselectAndRemoveObjectInDatabaseEditor(methodArgs.sender as string);
				this.RefreshItemList();
			}
		}

		// Token: 0x06001A3B RID: 6715 RVA: 0x000ACDEC File Offset: 0x000ABDEC
		private void ApplyBrush(string brush)
		{
			List<Tools.Geometry.Point> tiles;
			if (this.operationIsAllowed && this.mapSoundContainer != null && this.mapEditState.Map.Data.GetEditedTiles(this.currentPosition.Vec3, this.brushSize, out tiles))
			{
				this.mapSoundContainer.SetSound(tiles, brush);
			}
		}

		// Token: 0x06001A3C RID: 6716 RVA: 0x000ACE44 File Offset: 0x000ABE44
		private void Begin()
		{
			this.operationIsAllowed = (this.mapEditState.Context.EditorSceneParams.ActiveLightmapState == EditorSceneParams.LightmapState.Music || this.mapEditState.Context.EditorSceneParams.ActiveLightmapState == EditorSceneParams.LightmapState.Ambience);
			this.mapEditState.Map.OperationContainer.BeginTransaction();
		}

		// Token: 0x06001A3D RID: 6717 RVA: 0x000ACEA0 File Offset: 0x000ABEA0
		private void End(bool applyChanges)
		{
			if (applyChanges)
			{
				if (this.mapEditState.Map.OperationContainer.EndTransaction() && base.Container != null && this.mapSoundContainer != null)
				{
					if (this.mapSoundContainer == this.mapEditState.Map.MapAmbienceContainer)
					{
						base.Container.Invoke("_sound_state_ambient_applied", default(MethodArgs));
						return;
					}
					if (this.mapSoundContainer == this.mapEditState.Map.MapMusicContainer)
					{
						base.Container.Invoke("_sound_state_music_applied", default(MethodArgs));
						return;
					}
				}
			}
			else if (this.mapEditState.Map.OperationContainer.CancelTransaction() && base.Container != null && this.mapSoundContainer != null)
			{
				if (this.mapSoundContainer == this.mapEditState.Map.MapAmbienceContainer)
				{
					base.Container.Invoke("_sound_state_ambient_not_applied", default(MethodArgs));
					return;
				}
				if (this.mapSoundContainer == this.mapEditState.Map.MapMusicContainer)
				{
					base.Container.Invoke("_sound_state_music_not_applied", default(MethodArgs));
				}
			}
		}

		// Token: 0x06001A3E RID: 6718 RVA: 0x000ACFD4 File Offset: 0x000ABFD4
		private void UpdatePosition(int x, int y)
		{
			this.mapEditState.Map.MapEditorMapObjectContainer.PickPosition(0, x, y, TerrainSurface.Terrain, out this.currentPosition);
			if (this.selectorUserGeometryID != -1)
			{
				this.mapEditState.Context.EditorScene.CreateUserGeometry_Brush(this.selectorUserGeometryID, ref this.currentPosition, (double)(16f * (float)this.brushSize), (double)(16f * (float)this.brushSize), Color.White, false);
			}
			if (this.mapEditState.Context.Statusbar.StatusPosition != null)
			{
				this.mapEditState.Context.Statusbar.StatusPosition.Text = string.Format("x:{0:0.###} y:{1:0.###} z:{2:0.###}, patch:[{3}, {4}]", new object[]
				{
					this.currentPosition.X,
					this.currentPosition.Y,
					this.currentPosition.Z,
					(int)(this.currentPosition.X / (double)Constants.PatchSize),
					(int)(this.currentPosition.Y / (double)Constants.PatchSize)
				});
			}
			Tools.Geometry.Point tile;
			if (this.mapSoundContainer != null && this.mapEditState.Map.Data.GetTile(this.currentPosition.Vec3, out tile))
			{
				string sound = this.mapSoundContainer.GetSound(tile);
				if (!string.IsNullOrEmpty(sound))
				{
					ItemData itemData = this.mapEditState.Context.ItemDataContainer.GetItemData(sound);
					this.mapEditState.Context.Statusbar.StatusMessage.Text = itemData.Text;
				}
				else
				{
					this.mapEditState.Context.Statusbar.StatusMessage.Text = string.Empty;
				}
			}
			this.mapEditState.Context.Statusbar.UpdateStatusbar();
		}

		// Token: 0x06001A3F RID: 6719 RVA: 0x000AD1B4 File Offset: 0x000AC1B4
		private void SetLightmap()
		{
			this.mapEditState.Context.EditorSceneParams.ActiveLightmapState = ((this.mapSoundContainer == this.mapEditState.Map.MapMusicContainer) ? EditorSceneParams.LightmapState.Music : EditorSceneParams.LightmapState.Ambience);
		}

		// Token: 0x06001A40 RID: 6720 RVA: 0x000AD1E8 File Offset: 0x000AC1E8
		private void OnFocusOn(MethodArgs methodArgs)
		{
			Vec3 distance = new Vec3(0.0, -10.0, 10.0);
			this.mapEditState.Context.EditorScene.SetAnchor(this.mapEditState.Context.EditorSceneViewID, ref this.currentPosition, ref distance);
		}

		// Token: 0x06001A41 RID: 6721 RVA: 0x000AD24C File Offset: 0x000AC24C
		private void OnSizeChanged(MethodArgs methodArg)
		{
			try
			{
				this.brushSize = (int)Convert.ToInt16(methodArg.sender);
				if (this.selectorUserGeometryID != -1)
				{
					this.mapEditState.Context.EditorScene.CreateUserGeometry_Brush(this.selectorUserGeometryID, ref this.currentPosition, (double)(16f * (float)this.brushSize), (double)(16f * (float)this.brushSize), Color.White, false);
				}
			}
			catch (FormatException)
			{
			}
		}

		// Token: 0x06001A42 RID: 6722 RVA: 0x000AD2D0 File Offset: 0x000AC2D0
		private void OnClearSoundChoosed(MethodArgs methodArgs)
		{
			this.multiState.ActiveStateLabel = "MapEditorClearSoundState";
		}

		// Token: 0x06001A43 RID: 6723 RVA: 0x000AD2E2 File Offset: 0x000AC2E2
		private void OnSetSoundChoosed(MethodArgs methodArgs)
		{
			this.multiState.ActiveStateLabel = "MapEditorSetSoundState";
		}

		// Token: 0x06001A44 RID: 6724 RVA: 0x000AD2F4 File Offset: 0x000AC2F4
		private void OnSetSoundMusic(MethodArgs methodArgs)
		{
			this.mapSoundContainer = this.mapEditState.Map.MapMusicContainer;
			this.SetLightmap();
		}

		// Token: 0x06001A45 RID: 6725 RVA: 0x000AD312 File Offset: 0x000AC312
		private void OnSetSoundAmbience(MethodArgs methodArgs)
		{
			this.mapSoundContainer = this.mapEditState.Map.MapAmbienceContainer;
			this.SetLightmap();
		}

		// Token: 0x06001A46 RID: 6726 RVA: 0x000AD330 File Offset: 0x000AC330
		private void OnItemListSelected(MethodArgs methodArgs)
		{
			this.currentItem = (methodArgs.sender as string);
		}

		// Token: 0x06001A47 RID: 6727 RVA: 0x000AD344 File Offset: 0x000AC344
		private void OnItemListCleared(MethodArgs methodArgs)
		{
			this.currentItem = string.Empty;
		}

		// Token: 0x06001A48 RID: 6728 RVA: 0x000AD351 File Offset: 0x000AC351
		private void OnItemListRefreshing(MethodArgs methodArgs)
		{
			if (this.itemListSource != null)
			{
				this.itemListSource.Refresh();
			}
		}

		// Token: 0x06001A49 RID: 6729 RVA: 0x000AD368 File Offset: 0x000AC368
		private void BindItemList()
		{
			if (this.mapEditState != null)
			{
				this.itemListSource = new SoundItemListSource(this.mapEditState.Map);
				this.mapEditState.Context.StateContainer.Invoke("_add_sound_item_list_source", new MethodArgs(null, this.itemListSource, null));
				this.mapEditState.Context.StateContainer.Invoke("_bind_sound_item_list", default(MethodArgs));
			}
		}

		// Token: 0x06001A4A RID: 6730 RVA: 0x000AD3E0 File Offset: 0x000AC3E0
		private void UnbindItemList()
		{
			if (this.mapEditState != null)
			{
				this.mapEditState.Context.StateContainer.Invoke("_unbind_sound_item_list", default(MethodArgs));
			}
		}

		// Token: 0x06001A4B RID: 6731 RVA: 0x000AD418 File Offset: 0x000AC418
		private void OnEnterState(IState state)
		{
			base.Container.BindState(this.multiState);
			this.previousLightmapState = this.mapEditState.Context.EditorSceneParams.ActiveLightmapState;
			this.SetLightmap();
			this.selectorUserGeometryID = this.mapEditState.Context.EditorScene.CreateUserGeometry_Brush(this.selectorUserGeometryID, ref this.currentPosition, (double)(16f * (float)this.brushSize), (double)(16f * (float)this.brushSize), Color.White, false);
			this.mapEditState.Context.Statusbar.StatusHelp.Text = Strings.HELP_MAP_SOUND_SHORTCUTS;
			this.mapEditState.Context.StateContainer.Invoke("_sound_tab_send_data", default(MethodArgs));
		}

		// Token: 0x06001A4C RID: 6732 RVA: 0x000AD4E4 File Offset: 0x000AC4E4
		private void OnLeaveState(IState state)
		{
			this.mapEditState.Context.Statusbar.StatusHelp.Text = string.Empty;
			if (this.selectorUserGeometryID != -1)
			{
				this.mapEditState.Context.EditorSceneParams.ActiveLightmapState = this.previousLightmapState;
				this.mapEditState.Context.EditorScene.DeleteUserGeometry(this.selectorUserGeometryID);
				this.selectorUserGeometryID = -1;
			}
			base.Container.UnbindState(this.multiState);
		}

		// Token: 0x06001A4D RID: 6733 RVA: 0x000AD568 File Offset: 0x000AC568
		public SoundState(MapEditState _mapEditState) : base("MapEditorSoundState")
		{
			this.mapEditState = _mapEditState;
			this.multiState = new MultiState("MapEditorSoundMultiState");
			this.mapSoundContainer = this.mapEditState.Map.MapMusicContainer;
			this.multiState = new MultiState("SoundMultiState");
			this.multiState.AddState(new SoundState.SetSoundState(this));
			this.multiState.AddState(new SoundState.ClearSoundState(this));
			this.multiState.ActiveStateLabel = "MapEditorSetSoundState";
			this.BindItemList();
			this.EnterState = (State.ActivateEvent)Delegate.Combine(this.EnterState, new State.ActivateEvent(this.OnEnterState));
			this.LeaveState = (State.ActivateEvent)Delegate.Combine(this.LeaveState, new State.ActivateEvent(this.OnLeaveState));
			base.AddMethod("focus_on", new Method(this.OnFocusOn));
			base.AddMethod("_sound_item_list_selected", new Method(this.OnItemListSelected));
			base.AddMethod("_sound_item_list_cleared", new Method(this.OnItemListCleared));
			base.AddMethod("_sound_item_list_refreshing", new Method(this.OnItemListRefreshing));
			base.AddMethod("_set_sound_music", new Method(this.OnSetSoundMusic));
			base.AddMethod("_set_sound_ambience", new Method(this.OnSetSoundAmbience));
			base.AddMethod("_set_sound_choosed", new Method(this.OnSetSoundChoosed));
			base.AddMethod("_clear_sound_choosed", new Method(this.OnClearSoundChoosed));
			base.AddMethod("_sound_tab_size_changed", new Method(this.OnSizeChanged));
			base.AddMethod("_sound_item_edit", new Method(this.OnItemEditChoosed));
			base.AddMethod("mouse_wheel", new Method(this.OnMouseWheel));
			base.AddMethod("mouse_click", new Method(this.OnMouseClick));
			base.AddMethod("mouse_double_click", new Method(this.OnMouseDoubleClick));
			base.AddMethod("_sound_item_add", new Method(this.OnAddItemChoosed));
			base.AddMethod("_sound_item_remove", new Method(this.OnRemoveItemChoosed));
			if (SoundState.mainDb != null)
			{
				this.dbEventsGenerator = new DbEventsGenerator(SoundState.mainDb);
				this.dbEventsGenerator.DBObjectChanged += this.OnDbItemChanged;
				this.dbEventsGenerator.DBObjectAdded += this.OnDbItemAdded;
				this.dbEventsGenerator.DBObjectRemoved += this.OnDbItemRemoved;
				this.dbEventsGenerator.DBAfterObjectRemoved += this.OnDbAfterItemRemoved;
			}
		}

		// Token: 0x06001A4E RID: 6734 RVA: 0x000AD834 File Offset: 0x000AC834
		public void Destroy()
		{
			this.UnbindItemList();
			this.multiState.Destroy();
			this.multiState = null;
			this.mapEditState = null;
			this.mapSoundContainer = null;
			if (this.dbEventsGenerator != null)
			{
				this.dbEventsGenerator.DBObjectChanged -= this.OnDbItemChanged;
				this.dbEventsGenerator.DBObjectAdded -= this.OnDbItemAdded;
				this.dbEventsGenerator = null;
			}
		}

		// Token: 0x04001102 RID: 4354
		private const float cursorSizeRate = 16f;

		// Token: 0x04001103 RID: 4355
		private MapEditState mapEditState;

		// Token: 0x04001104 RID: 4356
		private MultiState multiState;

		// Token: 0x04001105 RID: 4357
		private MapSoundContainer mapSoundContainer;

		// Token: 0x04001106 RID: 4358
		private EditorSceneParams.LightmapState previousLightmapState;

		// Token: 0x04001107 RID: 4359
		private int selectorUserGeometryID = -1;

		// Token: 0x04001108 RID: 4360
		private Position currentPosition = Position.Empty;

		// Token: 0x04001109 RID: 4361
		private int brushSize = 1;

		// Token: 0x0400110A RID: 4362
		private string currentItem = string.Empty;

		// Token: 0x0400110B RID: 4363
		private bool operationIsAllowed;

		// Token: 0x0400110C RID: 4364
		private DbEventsGenerator dbEventsGenerator;

		// Token: 0x0400110D RID: 4365
		private static readonly IDatabase mainDb = IDatabase.GetMainDatabase();

		// Token: 0x0400110E RID: 4366
		private SoundItemListSource itemListSource;

		// Token: 0x0400110F RID: 4367
		private DBID removingItemDBID = DBID.Empty;

		// Token: 0x0200021F RID: 543
		private abstract class BaseSetSoundState : State
		{
			// Token: 0x06001A50 RID: 6736 RVA: 0x000AD8B0 File Offset: 0x000AC8B0
			private void OnMouseMove(MethodArgs methodArgs)
			{
				if (this.soundState != null)
				{
					MouseEventArgs mouseEventArgs = methodArgs.eventArgs as MouseEventArgs;
					if (mouseEventArgs != null && !KeyStatus.RightMouse)
					{
						this.soundState.UpdatePosition(mouseEventArgs.X, mouseEventArgs.Y);
						if (KeyStatus.LeftMouse)
						{
							this.soundState.Begin();
							this.ApplyBrush();
						}
					}
				}
			}

			// Token: 0x06001A51 RID: 6737 RVA: 0x000AD90C File Offset: 0x000AC90C
			private void OnMouseDown(MethodArgs methodArgs)
			{
				if (this.soundState != null)
				{
					MouseEventArgs mouseEventArgs = methodArgs.eventArgs as MouseEventArgs;
					if (mouseEventArgs != null && mouseEventArgs.Button == MouseButtons.Left)
					{
						this.soundState.Begin();
						this.soundState.UpdatePosition(mouseEventArgs.X, mouseEventArgs.Y);
						this.ApplyBrush();
					}
				}
			}

			// Token: 0x06001A52 RID: 6738 RVA: 0x000AD968 File Offset: 0x000AC968
			private void OnMouseUp(MethodArgs methodArgs)
			{
				if (this.soundState != null)
				{
					MouseEventArgs mouseEventArgs = methodArgs.eventArgs as MouseEventArgs;
					if (mouseEventArgs != null && mouseEventArgs.Button == MouseButtons.Left)
					{
						this.soundState.End(true);
					}
				}
			}

			// Token: 0x06001A53 RID: 6739
			protected abstract void ApplyBrush();

			// Token: 0x06001A54 RID: 6740 RVA: 0x000AD9A8 File Offset: 0x000AC9A8
			protected BaseSetSoundState(SoundState _soundState, string label) : base(label)
			{
				this.soundState = _soundState;
				base.AddMethod("mouse_move", new Method(this.OnMouseMove));
				base.AddMethod("mouse_down", new Method(this.OnMouseDown));
				base.AddMethod("mouse_up", new Method(this.OnMouseUp));
			}

			// Token: 0x04001110 RID: 4368
			protected readonly SoundState soundState;
		}

		// Token: 0x02000220 RID: 544
		private class SetSoundState : SoundState.BaseSetSoundState
		{
			// Token: 0x06001A55 RID: 6741 RVA: 0x000ADA08 File Offset: 0x000ACA08
			protected override void ApplyBrush()
			{
				this.soundState.ApplyBrush(this.soundState.currentItem);
			}

			// Token: 0x06001A56 RID: 6742 RVA: 0x000ADA20 File Offset: 0x000ACA20
			public SetSoundState(SoundState _soundState) : base(_soundState, "MapEditorSetSoundState")
			{
			}
		}

		// Token: 0x02000221 RID: 545
		private class ClearSoundState : SoundState.BaseSetSoundState
		{
			// Token: 0x06001A57 RID: 6743 RVA: 0x000ADA2E File Offset: 0x000ACA2E
			protected override void ApplyBrush()
			{
				this.soundState.ApplyBrush(null);
			}

			// Token: 0x06001A58 RID: 6744 RVA: 0x000ADA3C File Offset: 0x000ACA3C
			public ClearSoundState(SoundState _soundState) : base(_soundState, "MapEditorClearSoundState")
			{
			}
		}
	}
}
