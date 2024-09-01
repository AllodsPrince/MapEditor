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
	// Token: 0x020002B9 RID: 697
	internal class LightsState : ApplyToPatchState
	{
		// Token: 0x06002085 RID: 8325 RVA: 0x000CED39 File Offset: 0x000CDD39
		public LightsState(MapEditState _mapEditState) : base("MapEditorLightsState", _mapEditState)
		{
		}

		// Token: 0x06002086 RID: 8326 RVA: 0x000CED47 File Offset: 0x000CDD47
		protected override void LoadItemList()
		{
			LightsDataSource.LoadLightList(base.MapEditState.Map);
		}

		// Token: 0x06002087 RID: 8327 RVA: 0x000CED5A File Offset: 0x000CDD5A
		protected override string GetMultiStateName()
		{
			return "LightsMultiState";
		}

		// Token: 0x06002088 RID: 8328 RVA: 0x000CED61 File Offset: 0x000CDD61
		protected override string GetApplyStateName()
		{
			return "ApplyLightState";
		}

		// Token: 0x06002089 RID: 8329 RVA: 0x000CED68 File Offset: 0x000CDD68
		protected override void CreateItemListSource()
		{
			base.ItemListSource = new LightItemListSource(base.MapEditState.Map);
			base.MapEditState.Context.StateContainer.Invoke("_add_light_item_list_source", new MethodArgs(null, base.ItemListSource, null));
			base.MapEditState.Context.StateContainer.Invoke("_bind_light_item_list", default(MethodArgs));
		}

		// Token: 0x0600208A RID: 8330 RVA: 0x000CEDD8 File Offset: 0x000CDDD8
		protected override void DestroyItemListSource()
		{
			base.MapEditState.Context.StateContainer.Invoke("_unbind_light_item_list", default(MethodArgs));
			base.ItemListSource = null;
		}

		// Token: 0x0600208B RID: 8331 RVA: 0x000CEE10 File Offset: 0x000CDE10
		protected override void RefreshItemListSource()
		{
			LightItemListSource lightItemListSource = base.ItemListSource as LightItemListSource;
			if (lightItemListSource != null)
			{
				lightItemListSource.Refresh();
			}
		}

		// Token: 0x0600208C RID: 8332 RVA: 0x000CEE32 File Offset: 0x000CDE32
		protected override string GetClassTypeName()
		{
			return "ZoneLights";
		}

		// Token: 0x0600208D RID: 8333 RVA: 0x000CEE39 File Offset: 0x000CDE39
		protected override string GetItemListSourceFolder()
		{
			return "ZoneLights";
		}

		// Token: 0x0600208E RID: 8334 RVA: 0x000CEE40 File Offset: 0x000CDE40
		protected override void ChangeAdditionalDBData(string item)
		{
			base.MapEditState.Container.Invoke("_light_list_item_changed", new MethodArgs(null, item, null));
			LightsDataSource.UpdateLight(base.MapEditState.Map, item);
		}

		// Token: 0x0600208F RID: 8335 RVA: 0x000CEE70 File Offset: 0x000CDE70
		protected override EditorSceneParams.LightmapState GetActiveLightmapState()
		{
			return EditorSceneParams.LightmapState.ZoneLights;
		}

		// Token: 0x06002090 RID: 8336 RVA: 0x000CEE74 File Offset: 0x000CDE74
		protected override void AdditionalEnterState()
		{
			base.MapEditState.Context.Statusbar.StatusHelp.Text = Strings.HELP_MAP_LIGHT_SHORTCUTS;
			base.MapEditState.Context.StateContainer.Invoke("_lights_tab_send_data", default(MethodArgs));
		}

		// Token: 0x06002091 RID: 8337 RVA: 0x000CEEC4 File Offset: 0x000CDEC4
		protected override void AdditionalMouseWheel(MouseEventArgs mouseEventArgs)
		{
			if (mouseEventArgs.Delta > 0)
			{
				base.MapEditState.Context.StateContainer.Invoke("_lights_increase_size", default(MethodArgs));
				return;
			}
			base.MapEditState.Context.StateContainer.Invoke("_lights_decrease_size", default(MethodArgs));
		}

		// Token: 0x06002092 RID: 8338 RVA: 0x000CEF21 File Offset: 0x000CDF21
		protected override void AdditionalMiddleMouse(Point tile)
		{
			base.MapEditState.Context.StateContainer.Invoke("_light_list_select_item", new MethodArgs(null, base.MapEditState.Map.ZoneLightContainer.GetLight(tile), null));
		}

		// Token: 0x06002093 RID: 8339 RVA: 0x000CEF5A File Offset: 0x000CDF5A
		protected override void AdditionalDoubleMiddleMouse(Point tile)
		{
			base.MapEditState.Context.SelectExistingObjectInDatabaseEditor(base.MapEditState.Map.ZoneLightContainer.GetLight(tile));
		}

		// Token: 0x06002094 RID: 8340 RVA: 0x000CEF82 File Offset: 0x000CDF82
		protected override string GetItemForStatusMessage(Point tile)
		{
			return base.MapEditState.Map.ZoneLightContainer.GetLight(tile);
		}

		// Token: 0x06002095 RID: 8341 RVA: 0x000CEF9C File Offset: 0x000CDF9C
		protected override void ApplyChanges(bool apply)
		{
			if (apply)
			{
				base.Container.Invoke("_zone_lights_state_applied", default(MethodArgs));
				return;
			}
			base.Container.Invoke("_zone_lights_state_not_applied", default(MethodArgs));
		}

		// Token: 0x06002096 RID: 8342 RVA: 0x000CEFDF File Offset: 0x000CDFDF
		protected override void Apply(List<Point> tiles, string item)
		{
			base.MapEditState.Map.ZoneLightContainer.SetLight(tiles, item);
		}

		// Token: 0x06002097 RID: 8343 RVA: 0x000CEFF9 File Offset: 0x000CDFF9
		protected override string GetApplyModeSetLabel()
		{
			return "_set_light_choosed";
		}

		// Token: 0x06002098 RID: 8344 RVA: 0x000CF000 File Offset: 0x000CE000
		protected override string GetApplyModeClearLabel()
		{
			return "_clear_light_choosed";
		}

		// Token: 0x06002099 RID: 8345 RVA: 0x000CF007 File Offset: 0x000CE007
		protected override string GetItemListSelectedLabel()
		{
			return "_light_item_list_selected";
		}

		// Token: 0x0600209A RID: 8346 RVA: 0x000CF00E File Offset: 0x000CE00E
		protected override string GetItemListClearedLabel()
		{
			return "_light_item_list_cleared";
		}

		// Token: 0x0600209B RID: 8347 RVA: 0x000CF015 File Offset: 0x000CE015
		protected override string GetItemListRefreshingLabel()
		{
			return "_light_item_list_refreshing";
		}

		// Token: 0x0600209C RID: 8348 RVA: 0x000CF01C File Offset: 0x000CE01C
		protected override string GetItemEditChoosedLabel()
		{
			return "_light_item_edit";
		}

		// Token: 0x0600209D RID: 8349 RVA: 0x000CF023 File Offset: 0x000CE023
		protected override string GetAddItemChoosedLabel()
		{
			return "_light_item_add";
		}

		// Token: 0x0600209E RID: 8350 RVA: 0x000CF02A File Offset: 0x000CE02A
		protected override string GetRemoveItemChoosedLabel()
		{
			return "_light_item_remove";
		}

		// Token: 0x0600209F RID: 8351 RVA: 0x000CF031 File Offset: 0x000CE031
		protected override string GetSizeChangedLabel()
		{
			return "_light_tab_size_changed";
		}
	}
}
