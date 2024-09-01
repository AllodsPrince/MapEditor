using System;
using System.Drawing;
using InputState;
using LauncherTools.InputState;
using MapEditor.Map;
using Tools.InputState;
using Tools.MapObjects;

namespace MapEditor
{
	// Token: 0x020000FE RID: 254
	internal class EditorSceneState : State
	{
		// Token: 0x06000C95 RID: 3221 RVA: 0x0006B544 File Offset: 0x0006A544
		private void ShowAstralGrid(bool show, EditorSceneParams _editorSceneParams)
		{
			if (this.context != null)
			{
				if (show)
				{
					Position position = _editorSceneParams.AstralShift;
					this.astralGridUserGeometryID = this.context.EditorScene.CreateUserGeometry_AstralGrid(this.astralGridUserGeometryID, _editorSceneParams.LargeTerrainGrid, ref position, _editorSceneParams.AstralRadius, Color.FromArgb(MapObjectCreationInfo.DefaultTransparentColorAlpha, Color.FromArgb(_editorSceneParams.TerrainGridColor)));
					return;
				}
				if (this.astralGridUserGeometryID != -1)
				{
					this.context.EditorScene.DeleteUserGeometry(this.astralGridUserGeometryID);
					this.astralGridUserGeometryID = -1;
				}
			}
		}

		// Token: 0x06000C96 RID: 3222 RVA: 0x0006B5CC File Offset: 0x0006A5CC
		private void ShowAstralBounds(bool show, EditorSceneParams _editorSceneParams)
		{
			if (show)
			{
				Position position = _editorSceneParams.AstralShift;
				this.astralBoundsUserGeometryID = this.context.EditorScene.CreateUserGeometry_AstralBounds(this.astralBoundsUserGeometryID, _editorSceneParams.LargeBottomGrid, ref position, _editorSceneParams.AstralRadius, _editorSceneParams.AstralHalfHeight, Color.FromArgb(MapObjectCreationInfo.DefaultTransparentColorAlpha, Color.FromArgb(_editorSceneParams.BottomGridColor)));
				return;
			}
			if (this.astralBoundsUserGeometryID != -1)
			{
				this.context.EditorScene.DeleteUserGeometry(this.astralBoundsUserGeometryID);
				this.astralBoundsUserGeometryID = -1;
			}
		}

		// Token: 0x06000C97 RID: 3223 RVA: 0x0006B650 File Offset: 0x0006A650
		private void SetTerrainGridBarams(EditorSceneParams _editorSceneParams)
		{
			if (this.context != null)
			{
				if (_editorSceneParams.ContinentType == ContinentType.AstralHub)
				{
					this.ShowAstralGrid(_editorSceneParams.ShowTerrainGrid, _editorSceneParams);
					return;
				}
				if (_editorSceneParams.ContinentType == ContinentType.Continent)
				{
					this.context.EditorScene.SetTerrainGridParams(this.context.EditorSceneViewID, 0, _editorSceneParams.ShowTerrainGrid, _editorSceneParams.LargeTerrainGrid, Color.FromArgb(MapObjectCreationInfo.DefaultTransparentColorAlpha, Color.FromArgb(_editorSceneParams.TerrainGridColor)));
				}
			}
		}

		// Token: 0x06000C98 RID: 3224 RVA: 0x0006B6C4 File Offset: 0x0006A6C4
		private void SetBottomGridBarams(EditorSceneParams _editorSceneParams)
		{
			if (this.context != null)
			{
				if (_editorSceneParams.ContinentType == ContinentType.AstralHub)
				{
					this.ShowAstralBounds(_editorSceneParams.ShowBottomGrid, _editorSceneParams);
					return;
				}
				if (_editorSceneParams.ContinentType == ContinentType.Continent)
				{
					this.context.EditorScene.SetTerrainGridParams(this.context.EditorSceneViewID, 1, _editorSceneParams.ShowBottomGrid, _editorSceneParams.LargeBottomGrid, Color.FromArgb(MapObjectCreationInfo.DefaultTransparentColorAlpha, Color.FromArgb(_editorSceneParams.BottomGridColor)));
				}
			}
		}

		// Token: 0x06000C99 RID: 3225 RVA: 0x0006B736 File Offset: 0x0006A736
		private void OnShowTerrainGridChanged(EditorSceneParams _editorSceneParams)
		{
			this.SetTerrainGridBarams(_editorSceneParams);
			base.SetMethodParams("toggle_terrain_grid", true, true, true, _editorSceneParams.ShowTerrainGrid);
		}

		// Token: 0x06000C9A RID: 3226 RVA: 0x0006B753 File Offset: 0x0006A753
		private void OnShowBottomGridChanged(EditorSceneParams _editorSceneParams)
		{
			this.SetBottomGridBarams(_editorSceneParams);
			base.SetMethodParams("toggle_bottom_grid", true, true, true, _editorSceneParams.ShowBottomGrid);
		}

		// Token: 0x06000C9B RID: 3227 RVA: 0x0006B770 File Offset: 0x0006A770
		private void OnLargeTerrainGridChanged(EditorSceneParams _editorSceneParams)
		{
			this.SetTerrainGridBarams(_editorSceneParams);
			base.SetMethodParams("large_terrain_grid", true, true, true, _editorSceneParams.LargeTerrainGrid);
		}

		// Token: 0x06000C9C RID: 3228 RVA: 0x0006B78D File Offset: 0x0006A78D
		private void OnLargeBottomGridChanged(EditorSceneParams _editorSceneParams)
		{
			this.SetBottomGridBarams(_editorSceneParams);
			base.SetMethodParams("large_bottom_grid", true, true, true, _editorSceneParams.LargeBottomGrid);
		}

		// Token: 0x06000C9D RID: 3229 RVA: 0x0006B7AA File Offset: 0x0006A7AA
		private void OnShowCollisionGeometryChanged(EditorSceneParams _editorSceneParams)
		{
			if (this.context != null)
			{
				this.context.EditorScene.ShowCollisionGeometry(_editorSceneParams.ShowCollisionGeometry);
			}
			base.SetMethodParams("toggle_collision_geometry", true, true, true, _editorSceneParams.ShowCollisionGeometry);
		}

		// Token: 0x06000C9E RID: 3230 RVA: 0x0006B7DE File Offset: 0x0006A7DE
		private void OnShowWireframeChanged(EditorSceneParams _editorSceneParams)
		{
			if (this.context != null)
			{
				this.context.EditorScene.SetWireframeMode(_editorSceneParams.ShowWireframe);
			}
			base.SetMethodParams("toggle_wireframe", true, true, true, _editorSceneParams.ShowWireframe);
		}

		// Token: 0x06000C9F RID: 3231 RVA: 0x0006B814 File Offset: 0x0006A814
		private void OnLightmapStateChanged(EditorSceneParams _editorSceneParams)
		{
			if (this.context != null)
			{
				bool showPassability = false;
				bool showZones = false;
				bool showLights = false;
				bool showMusic = false;
				bool showAmbience = false;
				bool showTerrainMask = false;
				switch (_editorSceneParams.ActiveLightmapState)
				{
				case EditorSceneParams.LightmapState.Lightmaps:
					this.context.EditorScene.SetActiveLightmapState("Show default lightmaps");
					break;
				case EditorSceneParams.LightmapState.Passability:
					showPassability = true;
					this.context.EditorScene.SetActiveLightmapState("Show obstacles");
					break;
				case EditorSceneParams.LightmapState.Zones:
					showZones = true;
					this.context.EditorScene.SetActiveLightmapState("Show zones");
					break;
				case EditorSceneParams.LightmapState.ZoneLights:
					showLights = true;
					this.context.EditorScene.SetActiveLightmapState("Show zone lights");
					break;
				case EditorSceneParams.LightmapState.Music:
					showMusic = true;
					this.context.EditorScene.SetActiveLightmapState("Show_sound_music");
					break;
				case EditorSceneParams.LightmapState.Ambience:
					showAmbience = true;
					this.context.EditorScene.SetActiveLightmapState("Show_sound_ambience");
					break;
				case EditorSceneParams.LightmapState.TerrainMask:
					showTerrainMask = true;
					this.context.EditorScene.SetActiveLightmapState("Show terrain mask");
					break;
				}
				base.SetMethodParams("toggle_passability", true, true, true, showPassability);
				base.SetMethodParams("toggle_zones", true, true, true, showZones);
				base.SetMethodParams("toggle_zone_lights", true, true, true, showLights);
				base.SetMethodParams("toggle_sound_music", true, true, true, showMusic);
				base.SetMethodParams("toggle_sound_ambience", true, true, true, showAmbience);
				base.SetMethodParams("toggle_terrain_mask", true, true, true, showTerrainMask);
			}
		}

		// Token: 0x06000CA0 RID: 3232 RVA: 0x0006B976 File Offset: 0x0006A976
		private void OnShowFogChanged(EditorSceneParams _editorSceneParams)
		{
			if (this.context != null)
			{
				this.context.EditorScene.SetFogMode(_editorSceneParams.ShowFog);
			}
			base.SetMethodParams("toggle_fog", true, true, true, _editorSceneParams.ShowFog);
		}

		// Token: 0x06000CA1 RID: 3233 RVA: 0x0006B9AA File Offset: 0x0006A9AA
		private void OnShowSkyChanged(EditorSceneParams _editorSceneParams)
		{
			if (this.context != null)
			{
				this.context.EditorScene.SetSkyMode(_editorSceneParams.ShowSky);
			}
			base.SetMethodParams("toggle_sky", true, true, true, _editorSceneParams.ShowSky);
		}

		// Token: 0x06000CA2 RID: 3234 RVA: 0x0006B9DE File Offset: 0x0006A9DE
		private void OnShowGrassChanged(EditorSceneParams _editorSceneParams)
		{
			if (this.context != null)
			{
				this.context.EditorScene.SetBoolGlobalVar("show_grass", _editorSceneParams.ShowGrass);
			}
			base.SetMethodParams("toggle_grass", true, true, true, _editorSceneParams.ShowGrass);
		}

		// Token: 0x06000CA3 RID: 3235 RVA: 0x0006BA17 File Offset: 0x0006AA17
		private void OnShowWaterChanged(EditorSceneParams _editorSceneParams)
		{
			if (this.context != null)
			{
				this.context.EditorScene.SetBoolGlobalVar("show_water", _editorSceneParams.ShowWater);
			}
			base.SetMethodParams("toggle_water", true, true, true, _editorSceneParams.ShowWater);
		}

		// Token: 0x06000CA4 RID: 3236 RVA: 0x0006BA50 File Offset: 0x0006AA50
		private void OnShowWorldCutSphereChanged(EditorSceneParams _editorSceneParams)
		{
			if (this.context != null)
			{
				this.context.EditorScene.ShowWorldCutSphere(_editorSceneParams.ShowWorldCutSphere);
			}
			base.SetMethodParams("toggle_world_cut_sphere", true, true, true, _editorSceneParams.ShowWorldCutSphere);
		}

		// Token: 0x06000CA5 RID: 3237 RVA: 0x0006BA84 File Offset: 0x0006AA84
		private void OnTerrainGridColorChanged(EditorSceneParams _editorSceneParams)
		{
			this.SetTerrainGridBarams(_editorSceneParams);
		}

		// Token: 0x06000CA6 RID: 3238 RVA: 0x0006BA8D File Offset: 0x0006AA8D
		private void OnBottomGridColorChanged(EditorSceneParams _editorSceneParams)
		{
			this.SetBottomGridBarams(_editorSceneParams);
		}

		// Token: 0x06000CA7 RID: 3239 RVA: 0x0006BA96 File Offset: 0x0006AA96
		private void OnBackgroundColorChanged(EditorSceneParams _editorSceneParams)
		{
			if (this.context != null)
			{
				this.context.EditorScene.SetBackgroundColor(this.context.EditorSceneViewID, Color.FromArgb(_editorSceneParams.BackgroundColor));
			}
		}

		// Token: 0x06000CA8 RID: 3240 RVA: 0x0006BAC6 File Offset: 0x0006AAC6
		private void OnArtDirectoryChanged(EditorSceneParams _editorSceneParams)
		{
			if (this.context != null && !string.IsNullOrEmpty(_editorSceneParams.ArtDirectory))
			{
				this.context.EditorScene.SetArtDirectory(_editorSceneParams.ArtDirectory);
			}
		}

		// Token: 0x06000CA9 RID: 3241 RVA: 0x0006BAF4 File Offset: 0x0006AAF4
		private void OnTypeChanged(EditorSceneParams _editorSceneParams)
		{
			if (this.context != null)
			{
				if (_editorSceneParams.ContinentType == ContinentType.Unknown)
				{
					this.context.EditorScene.SetTerrainGridParams(this.context.EditorSceneViewID, 0, false, _editorSceneParams.LargeTerrainGrid, Color.FromArgb(MapObjectCreationInfo.DefaultTransparentColorAlpha, Color.FromArgb(_editorSceneParams.TerrainGridColor)));
					this.context.EditorScene.SetTerrainGridParams(this.context.EditorSceneViewID, 1, false, _editorSceneParams.LargeBottomGrid, Color.FromArgb(MapObjectCreationInfo.DefaultTransparentColorAlpha, Color.FromArgb(_editorSceneParams.BottomGridColor)));
					this.ShowAstralGrid(false, _editorSceneParams);
					this.ShowAstralBounds(false, _editorSceneParams);
					return;
				}
				if (_editorSceneParams.ContinentType == ContinentType.AstralHub)
				{
					this.context.EditorScene.SetTerrainGridParams(this.context.EditorSceneViewID, 0, false, _editorSceneParams.LargeTerrainGrid, Color.FromArgb(MapObjectCreationInfo.DefaultTransparentColorAlpha, Color.FromArgb(_editorSceneParams.TerrainGridColor)));
					this.context.EditorScene.SetTerrainGridParams(this.context.EditorSceneViewID, 1, false, _editorSceneParams.LargeBottomGrid, Color.FromArgb(MapObjectCreationInfo.DefaultTransparentColorAlpha, Color.FromArgb(_editorSceneParams.BottomGridColor)));
					this.ShowAstralGrid(_editorSceneParams.ShowTerrainGrid, _editorSceneParams);
					this.ShowAstralBounds(_editorSceneParams.ShowBottomGrid, _editorSceneParams);
					return;
				}
				if (_editorSceneParams.ContinentType == ContinentType.Continent)
				{
					this.context.EditorScene.SetTerrainGridParams(this.context.EditorSceneViewID, 0, _editorSceneParams.ShowTerrainGrid, _editorSceneParams.LargeTerrainGrid, Color.FromArgb(MapObjectCreationInfo.DefaultTransparentColorAlpha, Color.FromArgb(_editorSceneParams.TerrainGridColor)));
					this.context.EditorScene.SetTerrainGridParams(this.context.EditorSceneViewID, 1, _editorSceneParams.ShowBottomGrid, _editorSceneParams.LargeBottomGrid, Color.FromArgb(MapObjectCreationInfo.DefaultTransparentColorAlpha, Color.FromArgb(_editorSceneParams.BottomGridColor)));
					this.ShowAstralGrid(false, _editorSceneParams);
					this.ShowAstralBounds(false, _editorSceneParams);
				}
			}
		}

		// Token: 0x06000CAA RID: 3242 RVA: 0x0006BCC5 File Offset: 0x0006ACC5
		private void OnAutoFocusChanged(EditorSceneParams _editorSceneParams)
		{
			base.SetMethodParams("toggle_auto_focus", true, true, true, _editorSceneParams.AutoFocus);
		}

		// Token: 0x06000CAB RID: 3243 RVA: 0x0006BCDB File Offset: 0x0006ACDB
		private void OnBlockEditingChanged(EditorSceneParams _editorSceneParams)
		{
			base.SetMethodParams("toggle_block_editing", true, true, true, _editorSceneParams.BlockEditing);
		}

		// Token: 0x06000CAC RID: 3244 RVA: 0x0006BCF4 File Offset: 0x0006ACF4
		private void OnEnterState(IState mapState)
		{
			EditorSceneParams editorSceneParams = this.context.EditorSceneParams;
			editorSceneParams.ShowTerrainGridChanged = (EditorSceneParams.ParamsEvent)Delegate.Combine(editorSceneParams.ShowTerrainGridChanged, new EditorSceneParams.ParamsEvent(this.OnShowTerrainGridChanged));
			EditorSceneParams editorSceneParams2 = this.context.EditorSceneParams;
			editorSceneParams2.ShowBottomGridChanged = (EditorSceneParams.ParamsEvent)Delegate.Combine(editorSceneParams2.ShowBottomGridChanged, new EditorSceneParams.ParamsEvent(this.OnShowBottomGridChanged));
			EditorSceneParams editorSceneParams3 = this.context.EditorSceneParams;
			editorSceneParams3.LargeTerrainGridChanged = (EditorSceneParams.ParamsEvent)Delegate.Combine(editorSceneParams3.LargeTerrainGridChanged, new EditorSceneParams.ParamsEvent(this.OnLargeTerrainGridChanged));
			EditorSceneParams editorSceneParams4 = this.context.EditorSceneParams;
			editorSceneParams4.LargeBottomGridChanged = (EditorSceneParams.ParamsEvent)Delegate.Combine(editorSceneParams4.LargeBottomGridChanged, new EditorSceneParams.ParamsEvent(this.OnLargeBottomGridChanged));
			EditorSceneParams editorSceneParams5 = this.context.EditorSceneParams;
			editorSceneParams5.ShowCollisionGeometryChanged = (EditorSceneParams.ParamsEvent)Delegate.Combine(editorSceneParams5.ShowCollisionGeometryChanged, new EditorSceneParams.ParamsEvent(this.OnShowCollisionGeometryChanged));
			EditorSceneParams editorSceneParams6 = this.context.EditorSceneParams;
			editorSceneParams6.ShowWireframeChanged = (EditorSceneParams.ParamsEvent)Delegate.Combine(editorSceneParams6.ShowWireframeChanged, new EditorSceneParams.ParamsEvent(this.OnShowWireframeChanged));
			EditorSceneParams editorSceneParams7 = this.context.EditorSceneParams;
			editorSceneParams7.LightmapStateChanged = (EditorSceneParams.ParamsEvent)Delegate.Combine(editorSceneParams7.LightmapStateChanged, new EditorSceneParams.ParamsEvent(this.OnLightmapStateChanged));
			EditorSceneParams editorSceneParams8 = this.context.EditorSceneParams;
			editorSceneParams8.TerrainGridColorChanged = (EditorSceneParams.ParamsEvent)Delegate.Combine(editorSceneParams8.TerrainGridColorChanged, new EditorSceneParams.ParamsEvent(this.OnTerrainGridColorChanged));
			EditorSceneParams editorSceneParams9 = this.context.EditorSceneParams;
			editorSceneParams9.BottomGridColorChanged = (EditorSceneParams.ParamsEvent)Delegate.Combine(editorSceneParams9.BottomGridColorChanged, new EditorSceneParams.ParamsEvent(this.OnBottomGridColorChanged));
			EditorSceneParams editorSceneParams10 = this.context.EditorSceneParams;
			editorSceneParams10.BackgroundColorChanged = (EditorSceneParams.ParamsEvent)Delegate.Combine(editorSceneParams10.BackgroundColorChanged, new EditorSceneParams.ParamsEvent(this.OnBackgroundColorChanged));
			EditorSceneParams editorSceneParams11 = this.context.EditorSceneParams;
			editorSceneParams11.ShowFogChanged = (EditorSceneParams.ParamsEvent)Delegate.Combine(editorSceneParams11.ShowFogChanged, new EditorSceneParams.ParamsEvent(this.OnShowFogChanged));
			EditorSceneParams editorSceneParams12 = this.context.EditorSceneParams;
			editorSceneParams12.ShowSkyChanged = (EditorSceneParams.ParamsEvent)Delegate.Combine(editorSceneParams12.ShowSkyChanged, new EditorSceneParams.ParamsEvent(this.OnShowSkyChanged));
			EditorSceneParams editorSceneParams13 = this.context.EditorSceneParams;
			editorSceneParams13.ShowGrassChanged = (EditorSceneParams.ParamsEvent)Delegate.Combine(editorSceneParams13.ShowGrassChanged, new EditorSceneParams.ParamsEvent(this.OnShowGrassChanged));
			EditorSceneParams editorSceneParams14 = this.context.EditorSceneParams;
			editorSceneParams14.ShowWaterChanged = (EditorSceneParams.ParamsEvent)Delegate.Combine(editorSceneParams14.ShowWaterChanged, new EditorSceneParams.ParamsEvent(this.OnShowWaterChanged));
			EditorSceneParams editorSceneParams15 = this.context.EditorSceneParams;
			editorSceneParams15.ShowWorldCutSphereChanged = (EditorSceneParams.ParamsEvent)Delegate.Combine(editorSceneParams15.ShowWorldCutSphereChanged, new EditorSceneParams.ParamsEvent(this.OnShowWorldCutSphereChanged));
			EditorSceneParams editorSceneParams16 = this.context.EditorSceneParams;
			editorSceneParams16.ArtDirectoryChanged = (EditorSceneParams.ParamsEvent)Delegate.Combine(editorSceneParams16.ArtDirectoryChanged, new EditorSceneParams.ParamsEvent(this.OnArtDirectoryChanged));
			EditorSceneParams editorSceneParams17 = this.context.EditorSceneParams;
			editorSceneParams17.ContinentTypeChanged = (EditorSceneParams.ParamsEvent)Delegate.Combine(editorSceneParams17.ContinentTypeChanged, new EditorSceneParams.ParamsEvent(this.OnTypeChanged));
			EditorSceneParams editorSceneParams18 = this.context.EditorSceneParams;
			editorSceneParams18.AutoFocusChanged = (EditorSceneParams.ParamsEvent)Delegate.Combine(editorSceneParams18.AutoFocusChanged, new EditorSceneParams.ParamsEvent(this.OnAutoFocusChanged));
			EditorSceneParams editorSceneParams19 = this.context.EditorSceneParams;
			editorSceneParams19.BlockEditingChanged = (EditorSceneParams.ParamsEvent)Delegate.Combine(editorSceneParams19.BlockEditingChanged, new EditorSceneParams.ParamsEvent(this.OnBlockEditingChanged));
			this.OnShowTerrainGridChanged(this.context.EditorSceneParams);
			this.OnShowBottomGridChanged(this.context.EditorSceneParams);
			this.OnLargeTerrainGridChanged(this.context.EditorSceneParams);
			this.OnLargeBottomGridChanged(this.context.EditorSceneParams);
			this.OnShowCollisionGeometryChanged(this.context.EditorSceneParams);
			this.OnShowWireframeChanged(this.context.EditorSceneParams);
			this.OnLightmapStateChanged(this.context.EditorSceneParams);
			this.OnTerrainGridColorChanged(this.context.EditorSceneParams);
			this.OnBottomGridColorChanged(this.context.EditorSceneParams);
			this.OnBackgroundColorChanged(this.context.EditorSceneParams);
			this.OnShowFogChanged(this.context.EditorSceneParams);
			this.OnShowSkyChanged(this.context.EditorSceneParams);
			this.OnShowGrassChanged(this.context.EditorSceneParams);
			this.OnShowWaterChanged(this.context.EditorSceneParams);
			this.OnShowWorldCutSphereChanged(this.context.EditorSceneParams);
			this.OnArtDirectoryChanged(this.context.EditorSceneParams);
			this.OnAutoFocusChanged(this.context.EditorSceneParams);
			this.OnBlockEditingChanged(this.context.EditorSceneParams);
		}

		// Token: 0x06000CAD RID: 3245 RVA: 0x0006C178 File Offset: 0x0006B178
		private void OnLeaveState(IState mapState)
		{
			EditorSceneParams editorSceneParams = this.context.EditorSceneParams;
			editorSceneParams.ShowTerrainGridChanged = (EditorSceneParams.ParamsEvent)Delegate.Remove(editorSceneParams.ShowTerrainGridChanged, new EditorSceneParams.ParamsEvent(this.OnShowTerrainGridChanged));
			EditorSceneParams editorSceneParams2 = this.context.EditorSceneParams;
			editorSceneParams2.ShowBottomGridChanged = (EditorSceneParams.ParamsEvent)Delegate.Remove(editorSceneParams2.ShowBottomGridChanged, new EditorSceneParams.ParamsEvent(this.OnShowBottomGridChanged));
			EditorSceneParams editorSceneParams3 = this.context.EditorSceneParams;
			editorSceneParams3.LargeTerrainGridChanged = (EditorSceneParams.ParamsEvent)Delegate.Remove(editorSceneParams3.LargeTerrainGridChanged, new EditorSceneParams.ParamsEvent(this.OnLargeTerrainGridChanged));
			EditorSceneParams editorSceneParams4 = this.context.EditorSceneParams;
			editorSceneParams4.LargeBottomGridChanged = (EditorSceneParams.ParamsEvent)Delegate.Remove(editorSceneParams4.LargeBottomGridChanged, new EditorSceneParams.ParamsEvent(this.OnLargeBottomGridChanged));
			EditorSceneParams editorSceneParams5 = this.context.EditorSceneParams;
			editorSceneParams5.ShowCollisionGeometryChanged = (EditorSceneParams.ParamsEvent)Delegate.Remove(editorSceneParams5.ShowCollisionGeometryChanged, new EditorSceneParams.ParamsEvent(this.OnShowCollisionGeometryChanged));
			EditorSceneParams editorSceneParams6 = this.context.EditorSceneParams;
			editorSceneParams6.ShowWireframeChanged = (EditorSceneParams.ParamsEvent)Delegate.Remove(editorSceneParams6.ShowWireframeChanged, new EditorSceneParams.ParamsEvent(this.OnShowWireframeChanged));
			EditorSceneParams editorSceneParams7 = this.context.EditorSceneParams;
			editorSceneParams7.LightmapStateChanged = (EditorSceneParams.ParamsEvent)Delegate.Remove(editorSceneParams7.LightmapStateChanged, new EditorSceneParams.ParamsEvent(this.OnLightmapStateChanged));
			EditorSceneParams editorSceneParams8 = this.context.EditorSceneParams;
			editorSceneParams8.TerrainGridColorChanged = (EditorSceneParams.ParamsEvent)Delegate.Remove(editorSceneParams8.TerrainGridColorChanged, new EditorSceneParams.ParamsEvent(this.OnTerrainGridColorChanged));
			EditorSceneParams editorSceneParams9 = this.context.EditorSceneParams;
			editorSceneParams9.BottomGridColorChanged = (EditorSceneParams.ParamsEvent)Delegate.Remove(editorSceneParams9.BottomGridColorChanged, new EditorSceneParams.ParamsEvent(this.OnBottomGridColorChanged));
			EditorSceneParams editorSceneParams10 = this.context.EditorSceneParams;
			editorSceneParams10.BackgroundColorChanged = (EditorSceneParams.ParamsEvent)Delegate.Remove(editorSceneParams10.BackgroundColorChanged, new EditorSceneParams.ParamsEvent(this.OnBackgroundColorChanged));
			EditorSceneParams editorSceneParams11 = this.context.EditorSceneParams;
			editorSceneParams11.ShowFogChanged = (EditorSceneParams.ParamsEvent)Delegate.Remove(editorSceneParams11.ShowFogChanged, new EditorSceneParams.ParamsEvent(this.OnShowFogChanged));
			EditorSceneParams editorSceneParams12 = this.context.EditorSceneParams;
			editorSceneParams12.ShowSkyChanged = (EditorSceneParams.ParamsEvent)Delegate.Remove(editorSceneParams12.ShowSkyChanged, new EditorSceneParams.ParamsEvent(this.OnShowSkyChanged));
			EditorSceneParams editorSceneParams13 = this.context.EditorSceneParams;
			editorSceneParams13.ShowGrassChanged = (EditorSceneParams.ParamsEvent)Delegate.Remove(editorSceneParams13.ShowGrassChanged, new EditorSceneParams.ParamsEvent(this.OnShowGrassChanged));
			EditorSceneParams editorSceneParams14 = this.context.EditorSceneParams;
			editorSceneParams14.ShowWaterChanged = (EditorSceneParams.ParamsEvent)Delegate.Remove(editorSceneParams14.ShowWaterChanged, new EditorSceneParams.ParamsEvent(this.OnShowWaterChanged));
			EditorSceneParams editorSceneParams15 = this.context.EditorSceneParams;
			editorSceneParams15.ShowWorldCutSphereChanged = (EditorSceneParams.ParamsEvent)Delegate.Remove(editorSceneParams15.ShowWorldCutSphereChanged, new EditorSceneParams.ParamsEvent(this.OnShowWorldCutSphereChanged));
			EditorSceneParams editorSceneParams16 = this.context.EditorSceneParams;
			editorSceneParams16.ArtDirectoryChanged = (EditorSceneParams.ParamsEvent)Delegate.Remove(editorSceneParams16.ArtDirectoryChanged, new EditorSceneParams.ParamsEvent(this.OnArtDirectoryChanged));
			EditorSceneParams editorSceneParams17 = this.context.EditorSceneParams;
			editorSceneParams17.ContinentTypeChanged = (EditorSceneParams.ParamsEvent)Delegate.Remove(editorSceneParams17.ContinentTypeChanged, new EditorSceneParams.ParamsEvent(this.OnTypeChanged));
			EditorSceneParams editorSceneParams18 = this.context.EditorSceneParams;
			editorSceneParams18.AutoFocusChanged = (EditorSceneParams.ParamsEvent)Delegate.Remove(editorSceneParams18.AutoFocusChanged, new EditorSceneParams.ParamsEvent(this.OnAutoFocusChanged));
			EditorSceneParams editorSceneParams19 = this.context.EditorSceneParams;
			editorSceneParams19.BlockEditingChanged = (EditorSceneParams.ParamsEvent)Delegate.Remove(editorSceneParams19.BlockEditingChanged, new EditorSceneParams.ParamsEvent(this.OnBlockEditingChanged));
		}

		// Token: 0x06000CAE RID: 3246 RVA: 0x0006C4C9 File Offset: 0x0006B4C9
		private void OnUpdateScene(MethodArgs methodArgs)
		{
			if (this.context != null)
			{
				this.context.UpdateScene();
			}
		}

		// Token: 0x06000CAF RID: 3247 RVA: 0x0006C4DE File Offset: 0x0006B4DE
		private void OnToggleTerrainGrid(MethodArgs methodArgs)
		{
			this.context.EditorSceneParams.ShowTerrainGrid = !this.context.EditorSceneParams.ShowTerrainGrid;
		}

		// Token: 0x06000CB0 RID: 3248 RVA: 0x0006C503 File Offset: 0x0006B503
		private void OnToggleBottomGrid(MethodArgs methodArgs)
		{
			this.context.EditorSceneParams.ShowBottomGrid = !this.context.EditorSceneParams.ShowBottomGrid;
		}

		// Token: 0x06000CB1 RID: 3249 RVA: 0x0006C528 File Offset: 0x0006B528
		private void OnLargeTerrainGrid(MethodArgs methodArgs)
		{
			this.context.EditorSceneParams.LargeTerrainGrid = !this.context.EditorSceneParams.LargeTerrainGrid;
		}

		// Token: 0x06000CB2 RID: 3250 RVA: 0x0006C54D File Offset: 0x0006B54D
		private void OnLargeBottomGrid(MethodArgs methodArgs)
		{
			this.context.EditorSceneParams.LargeBottomGrid = !this.context.EditorSceneParams.LargeBottomGrid;
		}

		// Token: 0x06000CB3 RID: 3251 RVA: 0x0006C572 File Offset: 0x0006B572
		private void OnToggleCollisionGeometry(MethodArgs methodArgs)
		{
			this.context.EditorSceneParams.ShowCollisionGeometry = !this.context.EditorSceneParams.ShowCollisionGeometry;
		}

		// Token: 0x06000CB4 RID: 3252 RVA: 0x0006C597 File Offset: 0x0006B597
		private void OnToggleWireframe(MethodArgs methodArgs)
		{
			this.context.EditorSceneParams.ShowWireframe = !this.context.EditorSceneParams.ShowWireframe;
		}

		// Token: 0x06000CB5 RID: 3253 RVA: 0x0006C5BC File Offset: 0x0006B5BC
		private void OnTogglePassability(MethodArgs methodArgs)
		{
			EditorSceneParams.LightmapState checkState = EditorSceneParams.LightmapState.Passability;
			this.context.EditorSceneParams.ActiveLightmapState = ((this.context.EditorSceneParams.ActiveLightmapState == checkState) ? EditorSceneParams.LightmapState.Lightmaps : checkState);
		}

		// Token: 0x06000CB6 RID: 3254 RVA: 0x0006C5F4 File Offset: 0x0006B5F4
		private void OnToggleZones(MethodArgs methodArgs)
		{
			EditorSceneParams.LightmapState checkState = EditorSceneParams.LightmapState.Zones;
			this.context.EditorSceneParams.ActiveLightmapState = ((this.context.EditorSceneParams.ActiveLightmapState == checkState) ? EditorSceneParams.LightmapState.Lightmaps : checkState);
		}

		// Token: 0x06000CB7 RID: 3255 RVA: 0x0006C62C File Offset: 0x0006B62C
		private void OnToggleZoneLights(MethodArgs methodArgs)
		{
			EditorSceneParams.LightmapState checkState = EditorSceneParams.LightmapState.ZoneLights;
			this.context.EditorSceneParams.ActiveLightmapState = ((this.context.EditorSceneParams.ActiveLightmapState == checkState) ? EditorSceneParams.LightmapState.Lightmaps : checkState);
		}

		// Token: 0x06000CB8 RID: 3256 RVA: 0x0006C664 File Offset: 0x0006B664
		private void OnToggleSoundMusic(MethodArgs methodArgs)
		{
			EditorSceneParams.LightmapState checkState = EditorSceneParams.LightmapState.Music;
			this.context.EditorSceneParams.ActiveLightmapState = ((this.context.EditorSceneParams.ActiveLightmapState == checkState) ? EditorSceneParams.LightmapState.Lightmaps : checkState);
		}

		// Token: 0x06000CB9 RID: 3257 RVA: 0x0006C69C File Offset: 0x0006B69C
		private void OnToggleSoundAmbience(MethodArgs methodArgs)
		{
			EditorSceneParams.LightmapState checkState = EditorSceneParams.LightmapState.Ambience;
			this.context.EditorSceneParams.ActiveLightmapState = ((this.context.EditorSceneParams.ActiveLightmapState == checkState) ? EditorSceneParams.LightmapState.Lightmaps : checkState);
		}

		// Token: 0x06000CBA RID: 3258 RVA: 0x0006C6D4 File Offset: 0x0006B6D4
		private void OnToggleTerrainMask(MethodArgs methodArgs)
		{
			EditorSceneParams.LightmapState checkState = EditorSceneParams.LightmapState.TerrainMask;
			this.context.EditorSceneParams.ActiveLightmapState = ((this.context.EditorSceneParams.ActiveLightmapState == checkState) ? EditorSceneParams.LightmapState.Lightmaps : checkState);
		}

		// Token: 0x06000CBB RID: 3259 RVA: 0x0006C70A File Offset: 0x0006B70A
		private void OnToggleFog(MethodArgs methodArgs)
		{
			this.context.EditorSceneParams.ShowFog = !this.context.EditorSceneParams.ShowFog;
		}

		// Token: 0x06000CBC RID: 3260 RVA: 0x0006C72F File Offset: 0x0006B72F
		private void OnToggleSky(MethodArgs methodArgs)
		{
			this.context.EditorSceneParams.ShowSky = !this.context.EditorSceneParams.ShowSky;
		}

		// Token: 0x06000CBD RID: 3261 RVA: 0x0006C754 File Offset: 0x0006B754
		private void OnToggleGrass(MethodArgs methodArgs)
		{
			this.context.EditorSceneParams.ShowGrass = !this.context.EditorSceneParams.ShowGrass;
		}

		// Token: 0x06000CBE RID: 3262 RVA: 0x0006C779 File Offset: 0x0006B779
		private void OnToggleWater(MethodArgs methodArgs)
		{
			this.context.EditorSceneParams.ShowWater = !this.context.EditorSceneParams.ShowWater;
		}

		// Token: 0x06000CBF RID: 3263 RVA: 0x0006C79E File Offset: 0x0006B79E
		private void OnToggleWorldCutSphere(MethodArgs methodArgs)
		{
			this.context.EditorSceneParams.ShowWorldCutSphere = !this.context.EditorSceneParams.ShowWorldCutSphere;
		}

		// Token: 0x06000CC0 RID: 3264 RVA: 0x0006C7C3 File Offset: 0x0006B7C3
		private void OnSetTerrainGridColor(MethodArgs methodArgs)
		{
			this.context.EditorSceneParams.ShowColorDialog("set_terrain_grid_color");
		}

		// Token: 0x06000CC1 RID: 3265 RVA: 0x0006C7DA File Offset: 0x0006B7DA
		private void OnSetBottomGridColor(MethodArgs methodArgs)
		{
			this.context.EditorSceneParams.ShowColorDialog("set_bottom_grid_color");
		}

		// Token: 0x06000CC2 RID: 3266 RVA: 0x0006C7F1 File Offset: 0x0006B7F1
		private void OnSetBackgroundColor(MethodArgs methodArgs)
		{
			this.context.EditorSceneParams.ShowColorDialog("set_background_color");
		}

		// Token: 0x06000CC3 RID: 3267 RVA: 0x0006C808 File Offset: 0x0006B808
		private void OnToggleArtDirectory(MethodArgs args)
		{
			this.context.EditorSceneParams.ShowArtDirectoryDialog();
		}

		// Token: 0x06000CC4 RID: 3268 RVA: 0x0006C81A File Offset: 0x0006B81A
		private void OnToggleAutoFocus(MethodArgs args)
		{
			this.context.EditorSceneParams.AutoFocus = !this.context.EditorSceneParams.AutoFocus;
		}

		// Token: 0x06000CC5 RID: 3269 RVA: 0x0006C83F File Offset: 0x0006B83F
		private void OnToggleBlockEditing(MethodArgs args)
		{
			this.context.EditorSceneParams.BlockEditing = !this.context.EditorSceneParams.BlockEditing;
		}

		// Token: 0x06000CC6 RID: 3270 RVA: 0x0006C864 File Offset: 0x0006B864
		public EditorSceneState(MainForm.Context _context) : base("MapEditorSceneState")
		{
			this.context = _context;
			base.AddMethod("update_scene", new Method(this.OnUpdateScene));
			base.AddMethod("toggle_terrain_grid", new Method(this.OnToggleTerrainGrid));
			base.AddMethod("toggle_bottom_grid", new Method(this.OnToggleBottomGrid));
			base.AddMethod("large_terrain_grid", new Method(this.OnLargeTerrainGrid));
			base.AddMethod("large_bottom_grid", new Method(this.OnLargeBottomGrid));
			base.AddMethod("toggle_collision_geometry", new Method(this.OnToggleCollisionGeometry));
			base.AddMethod("toggle_wireframe", new Method(this.OnToggleWireframe));
			base.AddMethod("toggle_passability", new Method(this.OnTogglePassability));
			base.AddMethod("toggle_zones", new Method(this.OnToggleZones));
			base.AddMethod("toggle_zone_lights", new Method(this.OnToggleZoneLights));
			base.AddMethod("toggle_sound_music", new Method(this.OnToggleSoundMusic));
			base.AddMethod("toggle_sound_ambience", new Method(this.OnToggleSoundAmbience));
			base.AddMethod("toggle_terrain_mask", new Method(this.OnToggleTerrainMask));
			base.AddMethod("toggle_fog", new Method(this.OnToggleFog));
			base.AddMethod("toggle_sky", new Method(this.OnToggleSky));
			base.AddMethod("toggle_grass", new Method(this.OnToggleGrass));
			base.AddMethod("toggle_water", new Method(this.OnToggleWater));
			base.AddMethod("toggle_world_cut_sphere", new Method(this.OnToggleWorldCutSphere));
			base.AddMethod("set_terrain_grid_color", new Method(this.OnSetTerrainGridColor));
			base.AddMethod("set_bottom_grid_color", new Method(this.OnSetBottomGridColor));
			base.AddMethod("set_background_color", new Method(this.OnSetBackgroundColor));
			base.AddMethod("toggle_art_directory", new Method(this.OnToggleArtDirectory));
			base.AddMethod("toggle_auto_focus", new Method(this.OnToggleAutoFocus));
			base.AddMethod("toggle_block_editing", new Method(this.OnToggleBlockEditing));
			this.EnterState = (State.ActivateEvent)Delegate.Combine(this.EnterState, new State.ActivateEvent(this.OnEnterState));
			this.LeaveState = (State.ActivateEvent)Delegate.Combine(this.LeaveState, new State.ActivateEvent(this.OnLeaveState));
		}

		// Token: 0x06000CC7 RID: 3271 RVA: 0x0006CB00 File Offset: 0x0006BB00
		public void Destroy()
		{
			this.context = null;
			this.EnterState = (State.ActivateEvent)Delegate.Remove(this.EnterState, new State.ActivateEvent(this.OnEnterState));
			this.LeaveState = (State.ActivateEvent)Delegate.Remove(this.LeaveState, new State.ActivateEvent(this.OnLeaveState));
		}

		// Token: 0x04000A5E RID: 2654
		private MainForm.Context context;

		// Token: 0x04000A5F RID: 2655
		private int astralGridUserGeometryID = -1;

		// Token: 0x04000A60 RID: 2656
		private int astralBoundsUserGeometryID = -1;
	}
}
