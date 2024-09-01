using System;
using System.Collections.Generic;
using System.Windows.Forms;
using InputState;
using MapEditor.Map.DataProviders;
using MapEditor.Map.SaveLoad.DataSources;
using MapEditor.Resources.Strings;
using Tools.Geometry;

namespace MapEditor.Map.States
{
	// Token: 0x02000161 RID: 353
	internal class MapZonesState : ApplyToPatchState
	{
		// Token: 0x060010FE RID: 4350 RVA: 0x0007E29D File Offset: 0x0007D29D
		public MapZonesState(MapEditState _mapEditState) : base("MapEditorZonesState", _mapEditState)
		{
		}

		// Token: 0x060010FF RID: 4351 RVA: 0x0007E2AB File Offset: 0x0007D2AB
		protected override void LoadItemList()
		{
			MapZonesDataSource.LoadZonesList(base.MapEditState.Map);
		}

		// Token: 0x06001100 RID: 4352 RVA: 0x0007E2BE File Offset: 0x0007D2BE
		protected override string GetMultiStateName()
		{
			return "MapZonesMultiState";
		}

		// Token: 0x06001101 RID: 4353 RVA: 0x0007E2C5 File Offset: 0x0007D2C5
		protected override string GetApplyStateName()
		{
			return "ApplyMapZoneState";
		}

		// Token: 0x06001102 RID: 4354 RVA: 0x0007E2CC File Offset: 0x0007D2CC
		protected override void CreateItemListSource()
		{
			base.ItemListSource = new MapZoneItemListSource(base.MapEditState.Map);
			base.MapEditState.Context.StateContainer.Invoke("_add_map_zones_item_list_source", new MethodArgs(null, base.ItemListSource, null));
			base.MapEditState.Context.StateContainer.Invoke("_bind_map_zones_item_list", default(MethodArgs));
		}

		// Token: 0x06001103 RID: 4355 RVA: 0x0007E33C File Offset: 0x0007D33C
		protected override void DestroyItemListSource()
		{
			base.MapEditState.Context.StateContainer.Invoke("_unbind_map_zones_item_list", default(MethodArgs));
			base.ItemListSource = null;
		}

		// Token: 0x06001104 RID: 4356 RVA: 0x0007E374 File Offset: 0x0007D374
		protected override void RefreshItemListSource()
		{
			MapZoneItemListSource mapZoneItemListSource = base.ItemListSource as MapZoneItemListSource;
			if (mapZoneItemListSource != null)
			{
				mapZoneItemListSource.Refresh();
			}
		}

		// Token: 0x06001105 RID: 4357 RVA: 0x0007E396 File Offset: 0x0007D396
		protected override string GetClassTypeName()
		{
			return "gameMechanics.map.zone.ZoneResource";
		}

		// Token: 0x06001106 RID: 4358 RVA: 0x0007E39D File Offset: 0x0007D39D
		protected override string GetItemListSourceFolder()
		{
			return "Zones";
		}

		// Token: 0x06001107 RID: 4359 RVA: 0x0007E3A4 File Offset: 0x0007D3A4
		protected override void ChangeAdditionalDBData(string item)
		{
			base.MapEditState.Container.Invoke("_map_zone_list_item_changed", new MethodArgs(null, item, null));
			MapZonesDataSource.UpdateZone(base.MapEditState.Map, item);
			base.MapEditState.Map.MapZoneContainer.Refresh(item);
		}

		// Token: 0x06001108 RID: 4360 RVA: 0x0007E3F5 File Offset: 0x0007D3F5
		protected override EditorSceneParams.LightmapState GetActiveLightmapState()
		{
			return EditorSceneParams.LightmapState.Zones;
		}

		// Token: 0x06001109 RID: 4361 RVA: 0x0007E3F8 File Offset: 0x0007D3F8
		protected override void AdditionalEnterState()
		{
			base.MapEditState.Context.Statusbar.StatusHelp.Text = Strings.HELP_MAP_MAPZONE_SHORTCUTS;
			base.MapEditState.Context.StateContainer.Invoke("_map_zones_tab_send_data", default(MethodArgs));
		}

		// Token: 0x0600110A RID: 4362 RVA: 0x0007E448 File Offset: 0x0007D448
		protected override void AdditionalMouseWheel(MouseEventArgs mouseEventArgs)
		{
			if (mouseEventArgs.Delta > 0)
			{
				base.MapEditState.Context.StateContainer.Invoke("_map_zones_increase_size", default(MethodArgs));
				return;
			}
			base.MapEditState.Context.StateContainer.Invoke("_map_zones_decrease_size", default(MethodArgs));
		}

		// Token: 0x0600110B RID: 4363 RVA: 0x0007E4A5 File Offset: 0x0007D4A5
		protected override void AdditionalMiddleMouse(Point tile)
		{
			base.MapEditState.Context.StateContainer.Invoke("_map_zone_list_select_item", new MethodArgs(null, base.MapEditState.Map.MapZoneContainer.GetZone(tile), null));
		}

		// Token: 0x0600110C RID: 4364 RVA: 0x0007E4DE File Offset: 0x0007D4DE
		protected override void AdditionalDoubleMiddleMouse(Point tile)
		{
			base.MapEditState.Context.SelectExistingObjectInDatabaseEditor(base.MapEditState.Map.MapZoneContainer.GetZone(tile));
		}

		// Token: 0x0600110D RID: 4365 RVA: 0x0007E506 File Offset: 0x0007D506
		protected override string GetItemForStatusMessage(Point tile)
		{
			return base.MapEditState.Map.MapZoneContainer.GetZone(tile);
		}

		// Token: 0x0600110E RID: 4366 RVA: 0x0007E520 File Offset: 0x0007D520
		protected override void ApplyChanges(bool apply)
		{
			if (apply)
			{
				base.Container.Invoke("_map_zone_state_applied", default(MethodArgs));
				return;
			}
			base.Container.Invoke("_map_zone_state_not_applied", default(MethodArgs));
		}

		// Token: 0x0600110F RID: 4367 RVA: 0x0007E563 File Offset: 0x0007D563
		protected override void Apply(List<Point> tiles, string item)
		{
			base.MapEditState.Map.MapZoneContainer.ApplyZoneToTiles(tiles, item);
		}

		// Token: 0x06001110 RID: 4368 RVA: 0x0007E57D File Offset: 0x0007D57D
		protected override string GetApplyModeSetLabel()
		{
			return "_add_to_map_zone_choosed";
		}

		// Token: 0x06001111 RID: 4369 RVA: 0x0007E584 File Offset: 0x0007D584
		protected override string GetApplyModeClearLabel()
		{
			return "_clear_map_zone_choosed";
		}

		// Token: 0x06001112 RID: 4370 RVA: 0x0007E58B File Offset: 0x0007D58B
		protected override string GetItemListSelectedLabel()
		{
			return "_map_zones_item_list_selected";
		}

		// Token: 0x06001113 RID: 4371 RVA: 0x0007E592 File Offset: 0x0007D592
		protected override string GetItemListClearedLabel()
		{
			return "_map_zones_item_list_cleared";
		}

		// Token: 0x06001114 RID: 4372 RVA: 0x0007E599 File Offset: 0x0007D599
		protected override string GetItemListRefreshingLabel()
		{
			return "_map_zones_item_list_refreshing";
		}

		// Token: 0x06001115 RID: 4373 RVA: 0x0007E5A0 File Offset: 0x0007D5A0
		protected override string GetItemEditChoosedLabel()
		{
			return "_map_zones_item_edit";
		}

		// Token: 0x06001116 RID: 4374 RVA: 0x0007E5A7 File Offset: 0x0007D5A7
		protected override string GetAddItemChoosedLabel()
		{
			return "_map_zones_item_add";
		}

		// Token: 0x06001117 RID: 4375 RVA: 0x0007E5AE File Offset: 0x0007D5AE
		protected override string GetRemoveItemChoosedLabel()
		{
			return "_map_zones_item_remove";
		}

		// Token: 0x06001118 RID: 4376 RVA: 0x0007E5B5 File Offset: 0x0007D5B5
		protected override string GetSizeChangedLabel()
		{
			return "_map_zones_tab_size_changed";
		}
	}
}
