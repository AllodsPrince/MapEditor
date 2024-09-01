using System;
using System.Collections.Generic;
using System.Drawing;
using InputState;
using LauncherTools.InputState;
using MapEditor.Map;
using MapEditor.Map.Containers;
using MapEditor.Map.MapObjects;
using MapEditor.Scene.Types;
using MapInfo;
using Tools.Geometry;
using Tools.Landscape;
using Tools.MapObjects;
using Tools.MapSound;
using Tools.MapZoneLights;

namespace MapEditor.Scene
{
	// Token: 0x020002B8 RID: 696
	public class MapEditorScene : IMapObjectPicker, ICollisionMap
	{
		// Token: 0x17000743 RID: 1859
		// (get) Token: 0x06002038 RID: 8248 RVA: 0x000CD0A1 File Offset: 0x000CC0A1
		public static int DefaultTransparentColorAlpha
		{
			get
			{
				return MapEditorScene.defaultTransparentColorAlpha;
			}
		}

		// Token: 0x17000744 RID: 1860
		// (get) Token: 0x06002039 RID: 8249 RVA: 0x000CD0A8 File Offset: 0x000CC0A8
		// (set) Token: 0x0600203A RID: 8250 RVA: 0x000CD0B0 File Offset: 0x000CC0B0
		public bool CalculateColor
		{
			get
			{
				return this.calculateColor;
			}
			set
			{
				this.calculateColor = value;
			}
		}

		// Token: 0x17000745 RID: 1861
		// (get) Token: 0x0600203B RID: 8251 RVA: 0x000CD0B9 File Offset: 0x000CC0B9
		public double ScaleRatio
		{
			get
			{
				return this.scaleRatio;
			}
		}

		// Token: 0x0600203C RID: 8252 RVA: 0x000CD0C4 File Offset: 0x000CC0C4
		private static bool MapObjectIsInvisibleInGame(IMapObject mapObject)
		{
			if (mapObject.Type.Type == MapObjectFactory.Type.StaticObject)
			{
				StaticObject staticObject = mapObject as StaticObject;
				if (staticObject != null)
				{
					return staticObject.PureSound || staticObject.SceneName.Contains("Light_Cube_");
				}
			}
			else
			{
				if (mapObject.Type.Type == MapObjectFactory.Type.PermanentDevice)
				{
					return false;
				}
				if (mapObject.Type.Type == MapObjectFactory.Type.SpawnPoint)
				{
					return false;
				}
				if (mapObject.Type.Type == MapObjectFactory.Type.ClientSpawnPoint)
				{
					return false;
				}
				if (mapObject.Type.Type == MapObjectFactory.Type.Projectile)
				{
					return mapObject.SceneName == mapObject.DefaultSceneName;
				}
				if (mapObject.Type.Type == MapObjectFactory.Type.PlayerRespawnPlace)
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x0600203D RID: 8253 RVA: 0x000CD195 File Offset: 0x000CC195
		private void OnToggleSpawnPointUserGeometry(MethodArgs methodArgs)
		{
			this.mapSceneParams.ShowSpawnPointUserGeometry = !this.mapSceneParams.ShowSpawnPointUserGeometry;
		}

		// Token: 0x0600203E RID: 8254 RVA: 0x000CD1B0 File Offset: 0x000CC1B0
		private void OnToggleScriptAreaUserGeometry(MethodArgs methodArgs)
		{
			this.mapSceneParams.ShowScriptAreaUserGeometry = !this.mapSceneParams.ShowScriptAreaUserGeometry;
		}

		// Token: 0x0600203F RID: 8255 RVA: 0x000CD1CB File Offset: 0x000CC1CB
		private void OnToggleZoneLocatorUserGeometry(MethodArgs methodArgs)
		{
			this.mapSceneParams.ShowZoneLocatorUserGeometry = !this.mapSceneParams.ShowZoneLocatorUserGeometry;
		}

		// Token: 0x06002040 RID: 8256 RVA: 0x000CD1E6 File Offset: 0x000CC1E6
		private void OnToggleRoutePointUserGeometry(MethodArgs methodArgs)
		{
			this.mapSceneParams.ShowRoutePointUserGeometry = !this.mapSceneParams.ShowRoutePointUserGeometry;
		}

		// Token: 0x06002041 RID: 8257 RVA: 0x000CD201 File Offset: 0x000CC201
		private void OnToggleRouteObjects(MethodArgs methodArgs)
		{
			this.mapSceneParams.ShowRouteObjects = !this.mapSceneParams.ShowRouteObjects;
		}

		// Token: 0x06002042 RID: 8258 RVA: 0x000CD21C File Offset: 0x000CC21C
		private void OnToggleLinkUserGeometry(MethodArgs methodArgs)
		{
			this.mapSceneParams.ShowLinkUserGeometry = !this.mapSceneParams.ShowLinkUserGeometry;
		}

		// Token: 0x06002043 RID: 8259 RVA: 0x000CD237 File Offset: 0x000CC237
		private void OnToggleAxisUserGeometry(MethodArgs methodArgs)
		{
			this.mapSceneParams.ShowAxisUserGeometry = !this.mapSceneParams.ShowAxisUserGeometry;
		}

		// Token: 0x06002044 RID: 8260 RVA: 0x000CD252 File Offset: 0x000CC252
		private void OnToggleDynamicObjectsFall(MethodArgs methodArgs)
		{
			this.mapSceneParams.DynamicObjectsFall = !this.mapSceneParams.DynamicObjectsFall;
		}

		// Token: 0x06002045 RID: 8261 RVA: 0x000CD270 File Offset: 0x000CC270
		private void OnToggleStaticObjectsVisible(MethodArgs methodArgs)
		{
			int type = MapSceneObjectTypeContainer.GetType(MapObjectFactory.Type.StaticObject);
			this.mapSceneParams.SetObjectHidden(type, !this.mapSceneParams.IsObjectHidden(type));
		}

		// Token: 0x06002046 RID: 8262 RVA: 0x000CD2A4 File Offset: 0x000CC2A4
		private void OnToggleInteractiveObjectsVisible(MethodArgs methodArgs)
		{
			int type = MapSceneObjectTypeContainer.GetType(MapSceneObjectTypeContainer.generalTypeKey);
			this.mapSceneParams.SetObjectHidden(type, !this.mapSceneParams.IsObjectHidden(type));
		}

		// Token: 0x06002047 RID: 8263 RVA: 0x000CD2D7 File Offset: 0x000CC2D7
		private void OnToggleAllObjectsVisible(MethodArgs methodArgs)
		{
			this.mapSceneParams.ShowAllObjects = !this.mapSceneParams.ShowAllObjects;
		}

		// Token: 0x06002048 RID: 8264 RVA: 0x000CD2F2 File Offset: 0x000CC2F2
		private void OnToggleShowDynamicStatistic(MethodArgs methodArgs)
		{
			this.mapSceneParams.ShowDynamicStatistic = !this.mapSceneParams.ShowDynamicStatistic;
		}

		// Token: 0x06002049 RID: 8265 RVA: 0x000CD30D File Offset: 0x000CC30D
		private void OnToggleShowMobsAggroRadius(MethodArgs methodArgs)
		{
			this.mapSceneParams.ShowMobsAggroRadius = !this.mapSceneParams.ShowMobsAggroRadius;
		}

		// Token: 0x0600204A RID: 8266 RVA: 0x000CD328 File Offset: 0x000CC328
		private void OnToggleShowMobsAggroRadiusAsVolumes(MethodArgs methodArgs)
		{
			this.mapSceneParams.ShowMobsAggroRadiusAsVolumes = !this.mapSceneParams.ShowMobsAggroRadiusAsVolumes;
		}

		// Token: 0x0600204B RID: 8267 RVA: 0x000CD343 File Offset: 0x000CC343
		private void OnToggleFixTerrainTiles(MethodArgs methodArgs)
		{
			this.mapSceneParams.FixTerrainTiles = !this.mapSceneParams.FixTerrainTiles;
		}

		// Token: 0x0600204C RID: 8268 RVA: 0x000CD35E File Offset: 0x000CC35E
		private void OnToggleAstralBorderUserGeometry(MethodArgs methodArgs)
		{
			this.mapSceneParams.ShowAstralBorderUserGeometry = !this.mapSceneParams.ShowAstralBorderUserGeometry;
		}

		// Token: 0x0600204D RID: 8269 RVA: 0x000CD379 File Offset: 0x000CC379
		private void OnToggleProjectileShowVisObjects(MethodArgs methodArgs)
		{
			this.mapSceneParams.ShowProjectileVisObjects = !this.mapSceneParams.ShowProjectileVisObjects;
		}

		// Token: 0x0600204E RID: 8270 RVA: 0x000CD394 File Offset: 0x000CC394
		private void OnToggleStopDayTime(MethodArgs methodArgs)
		{
			this.mapSceneParams.StopDayTime = !this.mapSceneParams.StopDayTime;
		}

		// Token: 0x0600204F RID: 8271 RVA: 0x000CD3AF File Offset: 0x000CC3AF
		private void OnToggleHighlightObjects(MethodArgs metodArgs)
		{
			this.mapSceneParams.HighlightObjects = !this.mapSceneParams.HighlightObjects;
		}

		// Token: 0x06002050 RID: 8272 RVA: 0x000CD3CA File Offset: 0x000CC3CA
		private void OnShowSpawnPointUserGeometryChanged(MapSceneParams _mapSceneParams, ref bool oldValue, ref bool newValue)
		{
			this.mapSceneState.SetMethodParams("toggle_spawn_point_user_geometry", true, true, true, this.mapSceneParams.ShowSpawnPointUserGeometry);
		}

		// Token: 0x06002051 RID: 8273 RVA: 0x000CD3EA File Offset: 0x000CC3EA
		private void OnShowScriptAreaUserGeometryChanged(MapSceneParams _mapSceneParams, ref bool oldValue, ref bool newValue)
		{
			this.mapSceneState.SetMethodParams("toggle_script_area_user_geometry", true, true, true, this.mapSceneParams.ShowScriptAreaUserGeometry);
		}

		// Token: 0x06002052 RID: 8274 RVA: 0x000CD40A File Offset: 0x000CC40A
		private void OnShowZoneLocatorUserGeometryChanged(MapSceneParams _mapSceneParams, ref bool oldValue, ref bool newValue)
		{
			this.mapSceneState.SetMethodParams("toggle_zone_locator_user_geometry", true, true, true, this.mapSceneParams.ShowZoneLocatorUserGeometry);
		}

		// Token: 0x06002053 RID: 8275 RVA: 0x000CD42A File Offset: 0x000CC42A
		private void OnShowRoutePointUserGeometryChanged(MapSceneParams _mapSceneParams, ref bool oldValue, ref bool newValue)
		{
			this.mapSceneState.SetMethodParams("toggle_route_point_user_geometry", true, true, true, this.mapSceneParams.ShowRoutePointUserGeometry);
		}

		// Token: 0x06002054 RID: 8276 RVA: 0x000CD44A File Offset: 0x000CC44A
		private void OnShowRouteObjectsChanged(MapSceneParams _mapSceneParams, ref bool oldValue, ref bool newValue)
		{
			this.mapSceneState.SetMethodParams("toggle_route_objects", true, true, true, this.mapSceneParams.ShowRouteObjects);
		}

		// Token: 0x06002055 RID: 8277 RVA: 0x000CD46A File Offset: 0x000CC46A
		private void OnShowLinkUserGeometryChanged(MapSceneParams _mapSceneParams, ref bool oldValue, ref bool newValue)
		{
			this.mapSceneState.SetMethodParams("toggle_link_user_geometry", true, true, true, this.mapSceneParams.ShowLinkUserGeometry);
		}

		// Token: 0x06002056 RID: 8278 RVA: 0x000CD48A File Offset: 0x000CC48A
		private void OnShowAxisUserGeometryChanged(MapSceneParams _mapSceneParams, ref bool oldValue, ref bool newValue)
		{
			this.mapSceneState.SetMethodParams("toggle_axis_user_geometry", true, true, true, this.mapSceneParams.ShowAxisUserGeometry);
		}

		// Token: 0x06002057 RID: 8279 RVA: 0x000CD4AA File Offset: 0x000CC4AA
		private void OnDynamicObjectsFallChanged(MapSceneParams _mapSceneParams, ref bool oldValue, ref bool newValue)
		{
			this.mapSceneState.SetMethodParams("toggle_dynamic_objects_fall", true, true, true, this.mapSceneParams.DynamicObjectsFall);
		}

		// Token: 0x06002058 RID: 8280 RVA: 0x000CD4CC File Offset: 0x000CC4CC
		private void OnHiddenObjectsChanged(int type, bool hidden)
		{
			this.mapSceneState.SetMethodParams("toggle_static_objects_visible", true, true, true, !this.mapSceneParams.IsObjectHidden(MapSceneObjectTypeContainer.GetType(MapObjectFactory.Type.StaticObject)));
			this.mapSceneState.SetMethodParams("toggle_interactive_objects_visible", true, true, true, !this.mapSceneParams.IsObjectHidden(MapSceneObjectTypeContainer.GetType(MapSceneObjectTypeContainer.generalTypeKey)));
			this.mapSceneState.SetMethodParams("toggle_all_objects_visible", true, true, true, this.mapSceneParams.ShowAllObjects);
			if (this.stateContainer != null && hidden)
			{
				this.stateContainer.Invoke("_set_map_object_type_transparency", new MethodArgs(null, type, null));
			}
		}

		// Token: 0x06002059 RID: 8281 RVA: 0x000CD575 File Offset: 0x000CC575
		private void OnShowDynamicStatisticChanged(MapSceneParams _mapSceneParams, ref bool oldValue, ref bool newValue)
		{
			this.mapSceneState.SetMethodParams("toggle_dynamic_statistic", true, true, true, this.mapSceneParams.ShowDynamicStatistic);
		}

		// Token: 0x0600205A RID: 8282 RVA: 0x000CD595 File Offset: 0x000CC595
		private void OnShowMobsAggroRadiusChanged(MapSceneParams _mapSceneParams, ref bool oldValue, ref bool newValue)
		{
			this.mapSceneState.SetMethodParams("toggle_aggro_radius", true, true, true, this.mapSceneParams.ShowMobsAggroRadius);
		}

		// Token: 0x0600205B RID: 8283 RVA: 0x000CD5B5 File Offset: 0x000CC5B5
		private void OnShowMobsAggroRadiusAsVolumesChanged(MapSceneParams _mapSceneParams, ref bool oldValue, ref bool newValue)
		{
			this.mapSceneState.SetMethodParams("volume_aggro_radius", true, true, true, this.mapSceneParams.ShowMobsAggroRadiusAsVolumes);
		}

		// Token: 0x0600205C RID: 8284 RVA: 0x000CD5D5 File Offset: 0x000CC5D5
		private void OnFixTerrainTilesChanged(MapSceneParams _mapSceneParams, ref bool oldValue, ref bool newValue)
		{
			this.mapSceneState.SetMethodParams("fix_terrain_tiles", true, true, true, this.mapSceneParams.FixTerrainTiles);
		}

		// Token: 0x0600205D RID: 8285 RVA: 0x000CD5F5 File Offset: 0x000CC5F5
		private void OnShowAstralBorderUserGeometryChanged(MapSceneParams _mapSceneParams, ref bool oldValue, ref bool newValue)
		{
			this.mapSceneState.SetMethodParams("toggle_astral_border_user_geometry", true, true, true, this.mapSceneParams.ShowAstralBorderUserGeometry);
		}

		// Token: 0x0600205E RID: 8286 RVA: 0x000CD618 File Offset: 0x000CC618
		private void OnShowProjectileVisObjectsChanged(MapSceneParams _mapSceneParams, ref bool oldValue, ref bool newValue)
		{
			this.mapSceneState.SetMethodParams("toggle_projectile_vis_objects", true, true, true, this.mapSceneParams.ShowProjectileVisObjects);
			foreach (IMapObject mapObject in this.MapEditorMapObjectContainer.ProjectileContainer.MapObjects.Values)
			{
				Projectile projectile = mapObject as Projectile;
				if (projectile != null)
				{
					projectile.GetSceneNameFromVisObject = newValue;
					this.MapSceneObjects.RecreateMapObject(projectile, false);
				}
			}
		}

		// Token: 0x0600205F RID: 8287 RVA: 0x000CD6B0 File Offset: 0x000CC6B0
		private void OnStopDayTimeChanged(MapSceneParams _mapSceneParams, ref bool oldValue, ref bool newValue)
		{
			this.mapSceneState.SetMethodParams("toggle_stop_day_time", true, true, true, this.mapSceneParams.StopDayTime);
			if (this.editorScene != null)
			{
				this.editorScene.StopDayTime(newValue);
			}
		}

		// Token: 0x06002060 RID: 8288 RVA: 0x000CD6E8 File Offset: 0x000CC6E8
		private void OnHighlightObjectsChanged(MapSceneParams _mapSceneParams, ref bool oldValue, ref bool newValue)
		{
			List<IMapObject> mapObjects = new List<IMapObject>();
			this.mapSceneState.SetMethodParams("toggle_highlight_interactive_objects", true, true, true, this.mapSceneParams.HighlightObjects);
			if (newValue)
			{
				using (Dictionary<int, IMapObject>.ValueCollection.Enumerator enumerator = this.mapEditorMapObjectContainer.MapObjects.Values.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						IMapObject mapObject = enumerator.Current;
						mapObject.RemoveHighlight("SceneHighlightSwitcher");
						if (!this.mapSceneParams.IsObjectHidden(mapObject.Type.Type) && MapEditorScene.MapObjectIsInvisibleInGame(mapObject))
						{
							this.editorScene.SetObjectTransparency(this.mapSceneObjects.MapObjectIDToEditorSceneObjectID(mapObject.ID), 1.0);
							mapObjects.Add(mapObject);
						}
					}
					goto IL_141;
				}
			}
			foreach (IMapObject mapObject2 in this.mapEditorMapObjectContainer.MapObjects.Values)
			{
				mapObject2.Highlight("SceneHighlightSwitcher", Color.Empty, false);
				if (MapEditorScene.MapObjectIsInvisibleInGame(mapObject2))
				{
					this.editorScene.SetObjectTransparency(this.mapSceneObjects.MapObjectIDToEditorSceneObjectID(mapObject2.ID), 0.0);
					mapObjects.Add(mapObject2);
				}
			}
			IL_141:
			if (mapObjects.Count > 0 && this.stateContainer != null)
			{
				this.stateContainer.Invoke("_set_map_object_transparency", new MethodArgs(null, mapObjects, null));
			}
		}

		// Token: 0x06002061 RID: 8289 RVA: 0x000CD87C File Offset: 0x000CC87C
		private void OnMapObjectAdding(MapObjectContainer _mapObjectContainer, IMapObject _mapObject, ref bool cancel)
		{
			if (this.mapSceneParams != null && !this.mapSceneParams.HighlightObjects)
			{
				MapObject mapObject = _mapObject as MapObject;
				if (mapObject != null && !mapObject.Temporary)
				{
					mapObject.Highlight("SceneHighlightSwitcher", Color.Empty, false);
				}
			}
		}

		// Token: 0x06002062 RID: 8290 RVA: 0x000CD8C4 File Offset: 0x000CC8C4
		public MapEditorScene(EditorScene _editorScene, int _editorSceneViewID, StateContainer _stateContainer, string _mapSceneParamsFileName)
		{
			this.editorScene = _editorScene;
			this.editorSceneViewID = _editorSceneViewID;
			this.stateContainer = _stateContainer;
			this.mapSceneParamsFileName = _mapSceneParamsFileName;
			if (!string.IsNullOrEmpty(this.mapSceneParamsFileName))
			{
				this.mapSceneParams = Serializer.Load<MapSceneParams>(this.mapSceneParamsFileName);
				if (this.mapSceneParams == null)
				{
					this.mapSceneParams = new MapSceneParams();
				}
				this.mapSceneParams.ShowSpawnPointUserGeometryChanged += this.OnShowSpawnPointUserGeometryChanged;
				this.mapSceneParams.ShowScriptAreaUserGeometryChanged += this.OnShowScriptAreaUserGeometryChanged;
				this.mapSceneParams.ShowZoneLocatorUserGeometryChanged += this.OnShowZoneLocatorUserGeometryChanged;
				this.mapSceneParams.ShowRoutePointUserGeometryChanged += this.OnShowRoutePointUserGeometryChanged;
				this.mapSceneParams.ShowRouteObjectsChanged += this.OnShowRouteObjectsChanged;
				this.mapSceneParams.ShowLinkUserGeometryChanged += this.OnShowLinkUserGeometryChanged;
				this.mapSceneParams.ShowAxisUserGeometryChanged += this.OnShowAxisUserGeometryChanged;
				this.mapSceneParams.HiddenObjectsChanged += this.OnHiddenObjectsChanged;
				this.mapSceneParams.DynamicObjectsFallChanged += this.OnDynamicObjectsFallChanged;
				this.mapSceneParams.ShowDynamicStatisticChanged += this.OnShowDynamicStatisticChanged;
				this.mapSceneParams.ShowMobsAggroRadiusChanged += this.OnShowMobsAggroRadiusChanged;
				this.mapSceneParams.ShowMobsAggroRadiusAsVolumesChanged += this.OnShowMobsAggroRadiusAsVolumesChanged;
				this.mapSceneParams.FixTerrainTilesChanged += this.OnFixTerrainTilesChanged;
				this.mapSceneParams.ShowAstralBorderUserGeometryChanged += this.OnShowAstralBorderUserGeometryChanged;
				this.mapSceneParams.ShowProjectileVisObjectsChanged += this.OnShowProjectileVisObjectsChanged;
				this.mapSceneParams.StopDayTimeChanged += this.OnStopDayTimeChanged;
				this.mapSceneParams.HighlightObjectsChanged += this.OnHighlightObjectsChanged;
			}
			if (this.stateContainer != null)
			{
				this.mapSceneState.AddMethod("toggle_spawn_point_user_geometry", new Method(this.OnToggleSpawnPointUserGeometry));
				this.mapSceneState.AddMethod("toggle_script_area_user_geometry", new Method(this.OnToggleScriptAreaUserGeometry));
				this.mapSceneState.AddMethod("toggle_zone_locator_user_geometry", new Method(this.OnToggleZoneLocatorUserGeometry));
				this.mapSceneState.AddMethod("toggle_route_point_user_geometry", new Method(this.OnToggleRoutePointUserGeometry));
				this.mapSceneState.AddMethod("toggle_route_objects", new Method(this.OnToggleRouteObjects));
				this.mapSceneState.AddMethod("toggle_link_user_geometry", new Method(this.OnToggleLinkUserGeometry));
				this.mapSceneState.AddMethod("toggle_axis_user_geometry", new Method(this.OnToggleAxisUserGeometry));
				this.mapSceneState.AddMethod("toggle_dynamic_objects_fall", new Method(this.OnToggleDynamicObjectsFall));
				this.mapSceneState.AddMethod("toggle_static_objects_visible", new Method(this.OnToggleStaticObjectsVisible));
				this.mapSceneState.AddMethod("toggle_interactive_objects_visible", new Method(this.OnToggleInteractiveObjectsVisible));
				this.mapSceneState.AddMethod("toggle_all_objects_visible", new Method(this.OnToggleAllObjectsVisible));
				this.mapSceneState.AddMethod("toggle_dynamic_statistic", new Method(this.OnToggleShowDynamicStatistic));
				this.mapSceneState.AddMethod("toggle_aggro_radius", new Method(this.OnToggleShowMobsAggroRadius));
				this.mapSceneState.AddMethod("volume_aggro_radius", new Method(this.OnToggleShowMobsAggroRadiusAsVolumes));
				this.mapSceneState.AddMethod("fix_terrain_tiles", new Method(this.OnToggleFixTerrainTiles));
				this.mapSceneState.AddMethod("toggle_astral_border_user_geometry", new Method(this.OnToggleAstralBorderUserGeometry));
				this.mapSceneState.AddMethod("toggle_projectile_vis_objects", new Method(this.OnToggleProjectileShowVisObjects));
				this.mapSceneState.AddMethod("toggle_stop_day_time", new Method(this.OnToggleStopDayTime));
				this.mapSceneState.AddMethod("toggle_highlight_interactive_objects", new Method(this.OnToggleHighlightObjects));
				this.stateContainer.BindState(this.mapSceneState);
			}
			if (this.mapSceneParams != null)
			{
				this.mapSceneState.SetMethodParams("toggle_spawn_point_user_geometry", true, true, true, this.mapSceneParams.ShowSpawnPointUserGeometry);
				this.mapSceneState.SetMethodParams("toggle_script_area_user_geometry", true, true, true, this.mapSceneParams.ShowScriptAreaUserGeometry);
				this.mapSceneState.SetMethodParams("toggle_zone_locator_user_geometry", true, true, true, this.mapSceneParams.ShowZoneLocatorUserGeometry);
				this.mapSceneState.SetMethodParams("toggle_route_point_user_geometry", true, true, true, this.mapSceneParams.ShowRoutePointUserGeometry);
				this.mapSceneState.SetMethodParams("toggle_route_objects", true, true, true, this.mapSceneParams.ShowRouteObjects);
				this.mapSceneState.SetMethodParams("toggle_link_user_geometry", true, true, true, this.mapSceneParams.ShowLinkUserGeometry);
				this.mapSceneState.SetMethodParams("toggle_axis_user_geometry", true, true, true, this.mapSceneParams.ShowAxisUserGeometry);
				this.mapSceneState.SetMethodParams("toggle_dynamic_objects_fall", true, true, true, this.mapSceneParams.DynamicObjectsFall);
				this.mapSceneState.SetMethodParams("toggle_static_objects_visible", true, true, true, !this.mapSceneParams.IsObjectHidden(MapSceneObjectTypeContainer.GetType(MapObjectFactory.Type.StaticObject)));
				this.mapSceneState.SetMethodParams("toggle_interactive_objects_visible", true, true, true, !this.mapSceneParams.IsObjectHidden(MapSceneObjectTypeContainer.GetType(MapSceneObjectTypeContainer.generalTypeKey)));
				this.mapSceneState.SetMethodParams("toggle_all_objects_visible", true, true, true, this.mapSceneParams.ShowAllObjects);
				this.mapSceneState.SetMethodParams("toggle_all_objects_visible", true, true, true, this.mapSceneParams.ShowAllObjects);
				this.mapSceneState.SetMethodParams("toggle_dynamic_statistic", true, true, true, this.mapSceneParams.ShowDynamicStatistic);
				this.mapSceneState.SetMethodParams("toggle_aggro_radius", true, true, true, this.mapSceneParams.ShowMobsAggroRadius);
				this.mapSceneState.SetMethodParams("volume_aggro_radius", true, true, true, this.mapSceneParams.ShowMobsAggroRadiusAsVolumes);
				this.mapSceneState.SetMethodParams("fix_terrain_tiles", true, true, true, this.mapSceneParams.FixTerrainTiles);
				this.mapSceneState.SetMethodParams("toggle_astral_border_user_geometry", true, true, true, this.mapSceneParams.ShowAstralBorderUserGeometry);
				this.mapSceneState.SetMethodParams("toggle_projectile_vis_objects", true, true, true, this.mapSceneParams.ShowProjectileVisObjects);
				this.mapSceneState.SetMethodParams("toggle_stop_day_time", true, true, true, this.mapSceneParams.StopDayTime);
				this.mapSceneState.SetMethodParams("toggle_highlight_interactive_objects", true, true, true, this.mapSceneParams.HighlightObjects);
			}
		}

		// Token: 0x06002063 RID: 8291 RVA: 0x000CE01C File Offset: 0x000CD01C
		public void Destroy()
		{
			if (this.mapSceneParams != null && !string.IsNullOrEmpty(this.mapSceneParamsFileName))
			{
				this.mapSceneParams.ShowSpawnPointUserGeometryChanged -= this.OnShowSpawnPointUserGeometryChanged;
				this.mapSceneParams.ShowScriptAreaUserGeometryChanged -= this.OnShowScriptAreaUserGeometryChanged;
				this.mapSceneParams.ShowZoneLocatorUserGeometryChanged -= this.OnShowZoneLocatorUserGeometryChanged;
				this.mapSceneParams.ShowRoutePointUserGeometryChanged -= this.OnShowRoutePointUserGeometryChanged;
				this.mapSceneParams.ShowRouteObjectsChanged -= this.OnShowRouteObjectsChanged;
				this.mapSceneParams.ShowLinkUserGeometryChanged -= this.OnShowLinkUserGeometryChanged;
				this.mapSceneParams.ShowAxisUserGeometryChanged -= this.OnShowAxisUserGeometryChanged;
				this.mapSceneParams.HiddenObjectsChanged -= this.OnHiddenObjectsChanged;
				this.mapSceneParams.DynamicObjectsFallChanged -= this.OnDynamicObjectsFallChanged;
				this.mapSceneParams.ShowDynamicStatisticChanged -= this.OnShowDynamicStatisticChanged;
				this.mapSceneParams.ShowMobsAggroRadiusChanged -= this.OnShowMobsAggroRadiusChanged;
				this.mapSceneParams.ShowMobsAggroRadiusAsVolumesChanged -= this.OnShowMobsAggroRadiusAsVolumesChanged;
				this.mapSceneParams.FixTerrainTilesChanged -= this.OnFixTerrainTilesChanged;
				this.mapSceneParams.ShowAstralBorderUserGeometryChanged -= this.OnShowAstralBorderUserGeometryChanged;
				this.mapSceneParams.ShowProjectileVisObjectsChanged -= this.OnShowProjectileVisObjectsChanged;
				this.mapSceneParams.StopDayTimeChanged -= this.OnStopDayTimeChanged;
				this.mapSceneParams.HighlightObjectsChanged -= this.OnHighlightObjectsChanged;
				Serializer.Save(this.mapSceneParamsFileName, this.mapSceneParams, false);
				this.mapSceneParams = null;
				this.mapSceneParamsFileName = string.Empty;
			}
			this.mapSceneTerrainRegions = null;
			this.mapSceneLandscapeChanges = null;
			this.mapSceneObjects = null;
			this.mapSceneZones = null;
			this.zoneLights = null;
			this.mapSounds = null;
			this.mapSceneAxis = null;
			this.mapSceneDynamicStatistic = null;
			this.mapSceneAllObjects = null;
			this.mapScenePolygons = null;
			this.mapSceneSquares = null;
			this.mapSceneFixTerrainTiles = null;
			if (this.stateContainer != null)
			{
				this.stateContainer.UnbindState(this.mapSceneState);
				this.mapSceneState = null;
				this.stateContainer = null;
			}
			this.editorScene = null;
			this.editorSceneViewID = -1;
		}

		// Token: 0x06002064 RID: 8292 RVA: 0x000CE27C File Offset: 0x000CD27C
		public void Bind(ContinentType _type, double _scaleRatio, MapEditorMapObjectContainer _mapEditorMapObjectContainer, TerrainRegionContainer _terrainRegionContainer, LandscapeChangesContainer _landscapeChangesContainer, MapZoneContainer _mapZoneContainer, ZoneLightContainer _zoneLightContainer, MapSoundContainer _mapMusicContainer, MapSoundContainer _mapAmbienceContainer, PolygonContainer _polygonContainer, AxisAlignedSquareContainer _squareContainer, StripeContainer _stripeContainer)
		{
			this.Unbind();
			this.continentType = _type;
			this.scaleRatio = _scaleRatio;
			this.mapEditorMapObjectContainer = _mapEditorMapObjectContainer;
			this.terrainRegionContainer = _terrainRegionContainer;
			this.landscapeChangesContainer = _landscapeChangesContainer;
			this.mapZoneContainer = _mapZoneContainer;
			this.zoneLightContainer = _zoneLightContainer;
			this.mapMusicContainer = _mapMusicContainer;
			this.mapAmbienceContainer = _mapAmbienceContainer;
			this.polygonContainer = _polygonContainer;
			this.squareContainer = _squareContainer;
			this.stripeContainer = _stripeContainer;
			if (this.mapEditorMapObjectContainer != null)
			{
				this.mapEditorMapObjectContainer.ExternalCollisionMap = this;
				this.mapEditorMapObjectContainer.ExternalMapObjectPicker = this;
				this.mapEditorMapObjectContainer.MapObjectAdding += this.OnMapObjectAdding;
			}
			if (this.polygonContainer != null)
			{
				this.polygonContainer.ExternalCollisionMap = this;
			}
			if (this.squareContainer != null)
			{
				this.squareContainer.ExternalCollisionMap = this;
			}
			StripeContainer stripeContainer = this.stripeContainer;
			this.mapSceneTerrainRegions.Bind(this);
			this.mapSceneLandscapeChanges.Bind(this);
			this.mapSceneObjects.Bind(this);
			this.mapSceneZones.Bind(this);
			this.zoneLights.Bind(this);
			this.mapSounds.Bind(this);
			this.mapSceneAxis.Bind(this);
			this.mapSceneDynamicStatistic.Bind(this);
			this.mapSceneAllObjects.Bind(this);
			this.mapScenePolygons.Bind(this);
			this.mapSceneSquares.Bind(this);
			this.mapSceneFixTerrainTiles.Bind(this);
		}

		// Token: 0x06002065 RID: 8293 RVA: 0x000CE3E4 File Offset: 0x000CD3E4
		public void Unbind()
		{
			this.mapSceneTerrainRegions.Unbind();
			this.mapSceneLandscapeChanges.Unbind();
			this.mapSceneObjects.Unbind();
			this.mapSceneZones.Unbind();
			this.zoneLights.Unbind();
			this.mapSounds.Unbind();
			this.mapSceneAxis.Unbind();
			this.mapSceneDynamicStatistic.Unbind();
			this.mapSceneAllObjects.Unbind();
			this.mapScenePolygons.Unbind();
			this.mapSceneSquares.Unbind();
			this.mapSceneFixTerrainTiles.Unbind();
			this.continentType = ContinentType.Unknown;
			this.scaleRatio = 1.0;
			if (this.mapEditorMapObjectContainer != null)
			{
				this.mapEditorMapObjectContainer.ExternalCollisionMap = null;
				this.mapEditorMapObjectContainer.ExternalMapObjectPicker = null;
				this.mapEditorMapObjectContainer.MapObjectAdding -= this.OnMapObjectAdding;
				this.mapEditorMapObjectContainer = null;
			}
			if (this.terrainRegionContainer != null)
			{
				this.terrainRegionContainer = null;
			}
			if (this.landscapeChangesContainer != null)
			{
				this.landscapeChangesContainer = null;
			}
			if (this.mapZoneContainer != null)
			{
				this.mapZoneContainer = null;
			}
			if (this.zoneLightContainer != null)
			{
				this.zoneLightContainer = null;
			}
			if (this.mapMusicContainer != null)
			{
				this.mapMusicContainer = null;
			}
			if (this.mapAmbienceContainer != null)
			{
				this.mapAmbienceContainer = null;
			}
			if (this.polygonContainer != null)
			{
				this.polygonContainer.ExternalCollisionMap = null;
				this.polygonContainer = null;
			}
			if (this.squareContainer != null)
			{
				this.squareContainer.ExternalCollisionMap = null;
				this.squareContainer = null;
			}
			if (this.stripeContainer != null)
			{
				this.stripeContainer = null;
			}
		}

		// Token: 0x17000746 RID: 1862
		// (get) Token: 0x06002066 RID: 8294 RVA: 0x000CE568 File Offset: 0x000CD568
		public EditorScene EditorScene
		{
			get
			{
				return this.editorScene;
			}
		}

		// Token: 0x17000747 RID: 1863
		// (get) Token: 0x06002067 RID: 8295 RVA: 0x000CE570 File Offset: 0x000CD570
		public int EditorSceneViewID
		{
			get
			{
				return this.editorSceneViewID;
			}
		}

		// Token: 0x17000748 RID: 1864
		// (get) Token: 0x06002068 RID: 8296 RVA: 0x000CE578 File Offset: 0x000CD578
		public MapEditorMapObjectContainer MapEditorMapObjectContainer
		{
			get
			{
				return this.mapEditorMapObjectContainer;
			}
		}

		// Token: 0x17000749 RID: 1865
		// (get) Token: 0x06002069 RID: 8297 RVA: 0x000CE580 File Offset: 0x000CD580
		public TerrainRegionContainer TerrainRegionContainer
		{
			get
			{
				return this.terrainRegionContainer;
			}
		}

		// Token: 0x1700074A RID: 1866
		// (get) Token: 0x0600206A RID: 8298 RVA: 0x000CE588 File Offset: 0x000CD588
		public LandscapeChangesContainer LandscapeChangesContainer
		{
			get
			{
				return this.landscapeChangesContainer;
			}
		}

		// Token: 0x1700074B RID: 1867
		// (get) Token: 0x0600206B RID: 8299 RVA: 0x000CE590 File Offset: 0x000CD590
		public MapZoneContainer MapZoneContainer
		{
			get
			{
				return this.mapZoneContainer;
			}
		}

		// Token: 0x1700074C RID: 1868
		// (get) Token: 0x0600206C RID: 8300 RVA: 0x000CE598 File Offset: 0x000CD598
		public ZoneLightContainer ZoneLightContainer
		{
			get
			{
				return this.zoneLightContainer;
			}
		}

		// Token: 0x1700074D RID: 1869
		// (get) Token: 0x0600206D RID: 8301 RVA: 0x000CE5A0 File Offset: 0x000CD5A0
		public MapSoundContainer MapMusicContainer
		{
			get
			{
				return this.mapMusicContainer;
			}
		}

		// Token: 0x1700074E RID: 1870
		// (get) Token: 0x0600206E RID: 8302 RVA: 0x000CE5A8 File Offset: 0x000CD5A8
		public MapSoundContainer MapAmbienceContainer
		{
			get
			{
				return this.mapAmbienceContainer;
			}
		}

		// Token: 0x1700074F RID: 1871
		// (get) Token: 0x0600206F RID: 8303 RVA: 0x000CE5B0 File Offset: 0x000CD5B0
		public PolygonContainer PolygonContainer
		{
			get
			{
				return this.polygonContainer;
			}
		}

		// Token: 0x17000750 RID: 1872
		// (get) Token: 0x06002070 RID: 8304 RVA: 0x000CE5B8 File Offset: 0x000CD5B8
		public AxisAlignedSquareContainer SquareContainer
		{
			get
			{
				return this.squareContainer;
			}
		}

		// Token: 0x17000751 RID: 1873
		// (get) Token: 0x06002071 RID: 8305 RVA: 0x000CE5C0 File Offset: 0x000CD5C0
		public MapSceneParams MapSceneParams
		{
			get
			{
				return this.mapSceneParams;
			}
		}

		// Token: 0x17000752 RID: 1874
		// (get) Token: 0x06002072 RID: 8306 RVA: 0x000CE5C8 File Offset: 0x000CD5C8
		public MapSceneObjects MapSceneObjects
		{
			get
			{
				return this.mapSceneObjects;
			}
		}

		// Token: 0x06002073 RID: 8307 RVA: 0x000CE5D0 File Offset: 0x000CD5D0
		public int PickNextObject(out Position position)
		{
			int mapObjectID = -1;
			if (this.editorScene != null)
			{
				int editorSceneObjectID = this.editorScene.PickNextObject(this.editorSceneViewID, out position);
				if (editorSceneObjectID != -1)
				{
					mapObjectID = this.mapSceneObjects.EditorSceneObjectIDToMapObjectID(editorSceneObjectID);
				}
			}
			else
			{
				position = default(Position);
			}
			return mapObjectID;
		}

		// Token: 0x06002074 RID: 8308 RVA: 0x000CE618 File Offset: 0x000CD618
		public int PickFirstObjectByPoint(int x, int y, out Position position)
		{
			int mapObjectID = -1;
			if (this.editorScene != null)
			{
				int editorSceneObjectID = this.editorScene.PickFirstObject(this.editorSceneViewID, x, y, out position);
				if (editorSceneObjectID != -1)
				{
					mapObjectID = this.mapSceneObjects.EditorSceneObjectIDToMapObjectID(editorSceneObjectID);
				}
			}
			else
			{
				position = default(Position);
			}
			return mapObjectID;
		}

		// Token: 0x06002075 RID: 8309 RVA: 0x000CE660 File Offset: 0x000CD660
		public int PickFirstObjectByScreenFrame(int startx, int starty, int finishx, int finishy, out Position position)
		{
			int mapObjectID = -1;
			if (this.editorScene != null)
			{
				Rect rect = new Rect(startx, starty, finishx, finishy);
				int editorSceneObjectID = this.editorScene.PickFirstObject(this.editorSceneViewID, rect.Min.X, rect.Min.Y, rect.Max.X, rect.Max.Y, out position);
				if (editorSceneObjectID != -1)
				{
					mapObjectID = this.mapSceneObjects.EditorSceneObjectIDToMapObjectID(editorSceneObjectID);
				}
			}
			else
			{
				position = default(Position);
			}
			return mapObjectID;
		}

		// Token: 0x06002076 RID: 8310 RVA: 0x000CE6F2 File Offset: 0x000CD6F2
		public int PickFirstObjectByFrame(int startx, int starty, int finishx, int finishy, TerrainSurface surface, out Position position)
		{
			position = Position.Empty;
			return -1;
		}

		// Token: 0x06002077 RID: 8311 RVA: 0x000CE704 File Offset: 0x000CD704
		public bool PickPosition(int index, int x, int y, TerrainSurface surface, out Position position)
		{
			if (this.continentType != ContinentType.Continent)
			{
				Line line;
				this.editorScene.GetProjectiveRay(this.editorSceneViewID, x, y, out line);
				Vec3 intersection;
				bool result = MapEditorScene.planeZ.TryGetIntersection(line, out intersection);
				position = new Position(intersection);
				return result;
			}
			if (surface == TerrainSurface.Terrain)
			{
				return this.editorScene.PickTerrain(this.editorSceneViewID, index, x, y, out position);
			}
			if (surface == TerrainSurface.Water)
			{
				return this.editorScene.PickWater(this.editorSceneViewID, index, x, y, out position);
			}
			if (surface != TerrainSurface.Object)
			{
				return this.editorScene.PickTerrain(this.editorSceneViewID, index, x, y, out position);
			}
			Line line2;
			this.editorScene.GetProjectiveRay(this.editorSceneViewID, x, y, out line2);
			Position terrainPosition;
			bool terrainResult = this.editorScene.PickTerrain(this.editorSceneViewID, index, x, y, out terrainPosition);
			bool objectResult = false;
			Position objectPosition;
			int editorSceneObjectID = this.editorScene.PickFirstObject(this.editorSceneViewID, line2, true, out objectPosition);
			while (editorSceneObjectID != -1)
			{
				int mapObjectID = this.mapSceneObjects.EditorSceneObjectIDToMapObjectID(editorSceneObjectID);
				if (mapObjectID == -1)
				{
					break;
				}
				IMapObject mapObject;
				this.mapEditorMapObjectContainer.MapObjects.TryGetValue(mapObjectID, out mapObject);
				if (mapObject == null)
				{
					break;
				}
				if (!mapObject.Select)
				{
					objectResult = true;
					break;
				}
				editorSceneObjectID = this.editorScene.PickNextObject(this.editorSceneViewID, out objectPosition);
				if (editorSceneObjectID == -1)
				{
					break;
				}
			}
			if (!terrainResult && !objectResult)
			{
				position = terrainPosition;
				return false;
			}
			if (terrainResult && objectResult)
			{
				if ((line2.Origin - terrainPosition.Vec3).Length2 < (line2.Origin - objectPosition.Vec3).Length2)
				{
					position = terrainPosition;
				}
				else
				{
					position = objectPosition;
				}
				return true;
			}
			if (terrainResult)
			{
				position = terrainPosition;
				return true;
			}
			position = objectPosition;
			return true;
		}

		// Token: 0x06002078 RID: 8312 RVA: 0x000CE8CC File Offset: 0x000CD8CC
		public double GetTerrainHeight(int index, double x, double y)
		{
			if (this.continentType == ContinentType.Continent)
			{
				this.heightMapPosition.X = x;
				this.heightMapPosition.Y = y;
				return (double)this.editorScene.GetTerrainHeight(this.editorSceneViewID, index, ref this.heightMapPosition);
			}
			return 0.0;
		}

		// Token: 0x06002079 RID: 8313 RVA: 0x000CE920 File Offset: 0x000CD920
		public double GetWaterHeight(int index, double x, double y)
		{
			if (this.continentType == ContinentType.Continent)
			{
				this.heightMapPosition.X = x;
				this.heightMapPosition.Y = y;
				return (double)this.editorScene.GetWaterHeight(this.editorSceneViewID, index, ref this.heightMapPosition);
			}
			return 0.0;
		}

		// Token: 0x0600207A RID: 8314 RVA: 0x000CE974 File Offset: 0x000CD974
		public double GetHeight(int index, double x, double y, TerrainSurface surface)
		{
			if (this.continentType != ContinentType.Continent)
			{
				return 0.0;
			}
			this.heightMapPosition.X = x;
			this.heightMapPosition.Y = y;
			if (surface == TerrainSurface.Water)
			{
				return (double)this.editorScene.GetWaterHeight(this.editorSceneViewID, index, ref this.heightMapPosition);
			}
			return (double)this.editorScene.GetTerrainHeight(this.editorSceneViewID, index, ref this.heightMapPosition);
		}

		// Token: 0x0600207B RID: 8315 RVA: 0x000CE9E4 File Offset: 0x000CD9E4
		public bool GetNormal(int index, double x, double y, out Vec3 normal)
		{
			if (this.continentType == ContinentType.Continent)
			{
				this.heightMapPosition.X = x;
				this.heightMapPosition.Y = y;
				return this.editorScene.GetTerrainNormal(index, ref this.heightMapPosition, out normal);
			}
			normal = Vec3.ZNormal;
			return true;
		}

		// Token: 0x0600207C RID: 8316 RVA: 0x000CEA34 File Offset: 0x000CDA34
		public bool PickPosition(int x, int y, TerrainSurface surface, out Position position)
		{
			return this.PickPosition(this.editorScene.GetActiveTerrain(), x, y, surface, out position);
		}

		// Token: 0x0600207D RID: 8317 RVA: 0x000CEA4C File Offset: 0x000CDA4C
		public double GetTerrainHeight(double x, double y)
		{
			if (this.continentType == ContinentType.Continent)
			{
				this.heightMapPosition.X = x;
				this.heightMapPosition.Y = y;
				return (double)this.editorScene.GetTerrainHeight(this.editorSceneViewID, ref this.heightMapPosition);
			}
			return 0.0;
		}

		// Token: 0x0600207E RID: 8318 RVA: 0x000CEA9C File Offset: 0x000CDA9C
		public double GetWaterHeight(double x, double y)
		{
			if (this.continentType == ContinentType.Continent)
			{
				this.heightMapPosition.X = x;
				this.heightMapPosition.Y = y;
				return (double)this.editorScene.GetWaterHeight(this.editorSceneViewID, ref this.heightMapPosition);
			}
			return 0.0;
		}

		// Token: 0x0600207F RID: 8319 RVA: 0x000CEAEC File Offset: 0x000CDAEC
		public double GetHeight(double x, double y, TerrainSurface surface)
		{
			if (this.continentType != ContinentType.Continent)
			{
				return 0.0;
			}
			this.heightMapPosition.X = x;
			this.heightMapPosition.Y = y;
			if (surface == TerrainSurface.Water)
			{
				return (double)this.editorScene.GetWaterHeight(this.editorSceneViewID, ref this.heightMapPosition);
			}
			return (double)this.editorScene.GetTerrainHeight(this.editorSceneViewID, ref this.heightMapPosition);
		}

		// Token: 0x06002080 RID: 8320 RVA: 0x000CEB5C File Offset: 0x000CDB5C
		public bool GetNormal(double x, double y, out Vec3 normal)
		{
			if (this.continentType == ContinentType.Continent)
			{
				this.heightMapPosition.X = x;
				this.heightMapPosition.Y = y;
				return this.editorScene.GetTerrainNormal(this.editorSceneViewID, ref this.heightMapPosition, out normal);
			}
			normal = Vec3.ZNormal;
			return true;
		}

		// Token: 0x06002081 RID: 8321 RVA: 0x000CEBAF File Offset: 0x000CDBAF
		public bool GetProjectiveRay(int x, int y, out Line line)
		{
			return this.editorScene.GetProjectiveRay(this.editorSceneViewID, x, y, out line);
		}

		// Token: 0x06002082 RID: 8322 RVA: 0x000CEBC8 File Offset: 0x000CDBC8
		public double GetNearestFlatHeight(ref Position position, int ignoreMapObjectID, out bool exists)
		{
			float terrainHeight = 0f;
			bool doesTerrainExists = false;
			if (this.continentType == ContinentType.Continent)
			{
				doesTerrainExists = this.editorScene.GetTerrainHeight(this.editorSceneViewID, 0, true, ref position, out terrainHeight);
			}
			Line line = new Line(new Vec3(0.0, 0.0, -1.0), position.Vec3);
			double objectHeight = (double)terrainHeight;
			bool doesObjectHeightExists = false;
			Position positionOnPickedObject;
			int editorSceneObjectID = this.editorScene.PickFirstObject(this.editorSceneViewID, line, true, out positionOnPickedObject);
			while (editorSceneObjectID != -1)
			{
				int mapObjectID = this.mapSceneObjects.EditorSceneObjectIDToMapObjectID(editorSceneObjectID);
				if (mapObjectID == -1)
				{
					objectHeight = positionOnPickedObject.Z;
					doesObjectHeightExists = true;
					break;
				}
				IMapObject mapObject;
				this.mapEditorMapObjectContainer.MapObjects.TryGetValue(mapObjectID, out mapObject);
				if (mapObject == null)
				{
					break;
				}
				if (!mapObject.Select && (ignoreMapObjectID == -1 || mapObjectID != ignoreMapObjectID))
				{
					objectHeight = positionOnPickedObject.Z;
					doesObjectHeightExists = true;
					break;
				}
				editorSceneObjectID = this.editorScene.PickNextObject(this.editorSceneViewID, out positionOnPickedObject);
				if (editorSceneObjectID == -1)
				{
					break;
				}
			}
			double nearestFlatHeight;
			if (doesObjectHeightExists)
			{
				if (doesTerrainExists && position.Z > (double)terrainHeight)
				{
					if ((double)terrainHeight > objectHeight)
					{
						doesObjectHeightExists = false;
						nearestFlatHeight = (double)terrainHeight;
					}
					else
					{
						nearestFlatHeight = objectHeight;
					}
				}
				else
				{
					nearestFlatHeight = objectHeight;
				}
			}
			else
			{
				nearestFlatHeight = (double)terrainHeight;
			}
			exists = doesObjectHeightExists;
			return nearestFlatHeight;
		}

		// Token: 0x06002083 RID: 8323 RVA: 0x000CECF4 File Offset: 0x000CDCF4
		public double GetNearestFlatHeight(double x, double y, double z, int ignoreMapObjectID, out bool exists)
		{
			Position position = new Position(x, y, z);
			return this.GetNearestFlatHeight(ref position, ignoreMapObjectID, out exists);
		}

		// Token: 0x040013CD RID: 5069
		private const string highlighSwitcherKey = "SceneHighlightSwitcher";

		// Token: 0x040013CE RID: 5070
		private const string lightObjectKey = "Light_Cube_";

		// Token: 0x040013CF RID: 5071
		private static readonly int defaultTransparentColorAlpha = 100;

		// Token: 0x040013D0 RID: 5072
		private EditorScene editorScene;

		// Token: 0x040013D1 RID: 5073
		private int editorSceneViewID = -1;

		// Token: 0x040013D2 RID: 5074
		private StateContainer stateContainer;

		// Token: 0x040013D3 RID: 5075
		private string mapSceneParamsFileName = string.Empty;

		// Token: 0x040013D4 RID: 5076
		private static readonly Plane planeZ = new Plane(Vec3.ZNormal, Vec3.Empty);

		// Token: 0x040013D5 RID: 5077
		private State mapSceneState = new State("MapSceneState");

		// Token: 0x040013D6 RID: 5078
		private ContinentType continentType;

		// Token: 0x040013D7 RID: 5079
		private MapSceneTerrainRegions mapSceneTerrainRegions = new MapSceneTerrainRegions();

		// Token: 0x040013D8 RID: 5080
		private MapSceneLandscapeChanges mapSceneLandscapeChanges = new MapSceneLandscapeChanges();

		// Token: 0x040013D9 RID: 5081
		private MapSceneObjects mapSceneObjects = new MapSceneObjects();

		// Token: 0x040013DA RID: 5082
		private MapSceneZones mapSceneZones = new MapSceneZones();

		// Token: 0x040013DB RID: 5083
		private ZoneLights zoneLights = new ZoneLights();

		// Token: 0x040013DC RID: 5084
		private MapSounds mapSounds = new MapSounds();

		// Token: 0x040013DD RID: 5085
		private MapSceneAxis mapSceneAxis = new MapSceneAxis();

		// Token: 0x040013DE RID: 5086
		private MapSceneDynamicStatistic mapSceneDynamicStatistic = new MapSceneDynamicStatistic();

		// Token: 0x040013DF RID: 5087
		private MapSceneAllObjects mapSceneAllObjects = new MapSceneAllObjects();

		// Token: 0x040013E0 RID: 5088
		private MapScenePolygons mapScenePolygons = new MapScenePolygons();

		// Token: 0x040013E1 RID: 5089
		private MapSceneSquares mapSceneSquares = new MapSceneSquares();

		// Token: 0x040013E2 RID: 5090
		private MapSceneFixTerrainTiles mapSceneFixTerrainTiles = new MapSceneFixTerrainTiles();

		// Token: 0x040013E3 RID: 5091
		private MapEditorMapObjectContainer mapEditorMapObjectContainer;

		// Token: 0x040013E4 RID: 5092
		private LandscapeChangesContainer landscapeChangesContainer;

		// Token: 0x040013E5 RID: 5093
		private TerrainRegionContainer terrainRegionContainer;

		// Token: 0x040013E6 RID: 5094
		private MapZoneContainer mapZoneContainer;

		// Token: 0x040013E7 RID: 5095
		private ZoneLightContainer zoneLightContainer;

		// Token: 0x040013E8 RID: 5096
		private MapSoundContainer mapMusicContainer;

		// Token: 0x040013E9 RID: 5097
		private MapSoundContainer mapAmbienceContainer;

		// Token: 0x040013EA RID: 5098
		private PolygonContainer polygonContainer;

		// Token: 0x040013EB RID: 5099
		private AxisAlignedSquareContainer squareContainer;

		// Token: 0x040013EC RID: 5100
		private StripeContainer stripeContainer;

		// Token: 0x040013ED RID: 5101
		private MapSceneParams mapSceneParams;

		// Token: 0x040013EE RID: 5102
		private Position heightMapPosition = Position.Empty;

		// Token: 0x040013EF RID: 5103
		private bool calculateColor;

		// Token: 0x040013F0 RID: 5104
		private double scaleRatio = 1.0;
	}
}
