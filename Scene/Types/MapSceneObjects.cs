using System;
using System.Collections.Generic;
using System.Drawing;
using MapEditor.Map.MapObjects;
using Tools.MapObjects;

namespace MapEditor.Scene.Types
{
	// Token: 0x02000073 RID: 115
	public class MapSceneObjects
	{
		// Token: 0x060005A5 RID: 1445 RVA: 0x0002E7AA File Offset: 0x0002D7AA
		private static bool IgnoreAdditionalScaleRatio(string sceneName)
		{
			return string.Equals(sceneName, MapLocator.DefaultVisObject, StringComparison.OrdinalIgnoreCase) || string.Equals(sceneName, SpawnPoint.DefaultVisObject, StringComparison.OrdinalIgnoreCase);
		}

		// Token: 0x060005A6 RID: 1446 RVA: 0x0002E7C8 File Offset: 0x0002D7C8
		private int GetMapSceneObjectType(IMapObject mapObject)
		{
			if (this.mapSceneObjectTypeContainer != null)
			{
				return this.mapSceneObjectTypeContainer.GetMapSceneObjectType(mapObject);
			}
			return 0;
		}

		// Token: 0x060005A7 RID: 1447 RVA: 0x0002E7E0 File Offset: 0x0002D7E0
		private void OnMapObjectAdded(MapObjectContainer mapObjectContainer, IMapObject mapObject)
		{
			if (mapObject != null)
			{
				int editorSceneObjectID = this.mapEditorScene.EditorScene.CreateObject(this.GetMapSceneObjectType(mapObject), this.mapEditorScene.CalculateColor, mapObject.SceneName, mapObject.DefaultSceneName, mapObject.Type.Type == MapObjectFactory.Type.StaticObject);
				if (editorSceneObjectID != -1)
				{
					this.editorSceneObjectIDMap[mapObject.ID] = editorSceneObjectID;
					this.mapObjectIDMap[editorSceneObjectID] = mapObject.ID;
					Position position = mapObject.Position;
					Rotation rotation = mapObject.Rotation;
					this.mapEditorScene.EditorScene.MoveObject(editorSceneObjectID, ref position);
					this.mapEditorScene.EditorScene.RotateObject(editorSceneObjectID, ref rotation);
					Scale scale = MapObjectCreationInfo.DefaultScale;
					if (mapObject.Type.Type == MapObjectFactory.Type.StaticObject || mapObject.Type.Type == MapObjectFactory.Type.RoutePoint || mapObject.Type.Type == MapObjectFactory.Type.PermanentDevice)
					{
						scale = mapObject.Scale;
					}
					if (!MapSceneObjects.IgnoreAdditionalScaleRatio(mapObject.SceneName))
					{
						scale *= (float)this.mapEditorScene.ScaleRatio;
					}
					this.mapEditorScene.EditorScene.ScaleObject(editorSceneObjectID, ref scale);
					if (mapObject.UseManualColor)
					{
						EditorSceneDLLImport.COLOR_INFO info = mapObject.Info;
						this.mapEditorScene.EditorScene.SetObjectColorInfo(editorSceneObjectID, ref info);
					}
					Volume volume;
					this.mapEditorScene.EditorScene.GetObjectVolume(editorSceneObjectID, out volume);
					mapObject.Volume = volume;
					this.mapEditorScene.EditorScene.SelectObject(editorSceneObjectID, mapObject.HighlightColor.A > 0, mapObject.HighlightColor);
					if (mapObject.Type.Type == MapObjectFactory.Type.SpawnPoint)
					{
						this.mapEditorSceneSpawnPoints.OnMapObjectAdded(mapObjectContainer, mapObject, editorSceneObjectID);
						this.mapEditorAggroCircles.OnMapObjectAdded(mapObjectContainer, mapObject);
						if (this.mapEditorScene.MapSceneParams.DynamicObjectsFall)
						{
							Position fallPosition = this.mapEditorScene.EditorScene.GetFallPosition(ref position);
							this.mapEditorScene.EditorScene.MoveObject(editorSceneObjectID, ref fallPosition);
							return;
						}
					}
					else
					{
						if (mapObject.Type.Type == MapObjectFactory.Type.ScriptArea)
						{
							this.mapEditorSceneScriptAreas.OnMapObjectAdded(mapObjectContainer, mapObject);
							return;
						}
						if (mapObject.Type.Type == MapObjectFactory.Type.ZoneLocator)
						{
							this.mapEditorSceneZoneLocators.OnMapObjectAdded(mapObjectContainer, mapObject);
							return;
						}
						if (mapObject.Type.Type == MapObjectFactory.Type.PatrolNode)
						{
							this.mapEditorAggroCircles.OnMapObjectAdded(mapObjectContainer, mapObject);
							return;
						}
						if (mapObject.Type.Type == MapObjectFactory.Type.RoutePoint)
						{
							this.mapEditorAggroCircles.OnMapObjectAdded(mapObjectContainer, mapObject);
							return;
						}
						if (mapObject.Type.Type == MapObjectFactory.Type.AstralBorder)
						{
							this.mapEditorSceneAstralBorders.OnMapObjectAdded(mapObjectContainer, mapObject);
							return;
						}
						if (mapObject.Type.Type == MapObjectFactory.Type.Projectile)
						{
							this.mapEditorSceneProjectiles.OnMapObjectAdded(mapObjectContainer, mapObject);
						}
					}
				}
			}
		}

		// Token: 0x060005A8 RID: 1448 RVA: 0x0002EAC8 File Offset: 0x0002DAC8
		private void OnMapObjectRemoved(MapObjectContainer mapObjectContainer, IMapObject mapObject)
		{
			if (mapObject != null)
			{
				int editorSceneObjectID;
				if (this.editorSceneObjectIDMap.TryGetValue(mapObject.ID, out editorSceneObjectID))
				{
					this.mapEditorScene.EditorScene.DeleteObject(editorSceneObjectID);
					this.editorSceneObjectIDMap.Remove(mapObject.ID);
					this.mapObjectIDMap.Remove(editorSceneObjectID);
				}
				if (mapObject.Type.Type == MapObjectFactory.Type.SpawnPoint)
				{
					this.mapEditorSceneSpawnPoints.OnMapObjectRemoved(mapObjectContainer, mapObject);
					this.mapEditorAggroCircles.OnMapObjectRemoved(mapObjectContainer, mapObject);
					return;
				}
				if (mapObject.Type.Type == MapObjectFactory.Type.ScriptArea)
				{
					this.mapEditorSceneScriptAreas.OnMapObjectRemoved(mapObjectContainer, mapObject);
					return;
				}
				if (mapObject.Type.Type == MapObjectFactory.Type.ZoneLocator)
				{
					this.mapEditorSceneZoneLocators.OnMapObjectRemoved(mapObjectContainer, mapObject);
					return;
				}
				if (mapObject.Type.Type == MapObjectFactory.Type.PatrolNode)
				{
					this.mapEditorAggroCircles.OnMapObjectRemoved(mapObjectContainer, mapObject);
					return;
				}
				if (mapObject.Type.Type == MapObjectFactory.Type.RoutePoint)
				{
					this.mapEditorAggroCircles.OnMapObjectRemoved(mapObjectContainer, mapObject);
					return;
				}
				if (mapObject.Type.Type == MapObjectFactory.Type.AstralBorder)
				{
					this.mapEditorSceneAstralBorders.OnMapObjectRemoved(mapObjectContainer, mapObject);
				}
			}
		}

		// Token: 0x060005A9 RID: 1449 RVA: 0x0002EC04 File Offset: 0x0002DC04
		private void OnPositionChanged(MapObjectContainer mapObjectContainer, IMapObject mapObject, ref Position oldValue, ref Position newValue)
		{
			if (mapObject != null)
			{
				int editorSceneObjectID;
				if (this.editorSceneObjectIDMap.TryGetValue(mapObject.ID, out editorSceneObjectID))
				{
					this.mapEditorScene.EditorScene.MoveObject(editorSceneObjectID, ref newValue);
				}
				this.mapEditorSceneLinks.OnPositionChanged(mapObjectContainer, mapObject, ref oldValue, ref newValue);
				if (mapObject.Type.Type == MapObjectFactory.Type.SpawnPoint)
				{
					this.mapEditorSceneSpawnPoints.OnPositionChanged(mapObjectContainer, mapObject, ref oldValue, ref newValue);
					this.mapEditorAggroCircles.OnPositionChanged(mapObjectContainer, mapObject, ref oldValue, ref newValue);
					if (this.mapEditorScene.MapSceneParams.DynamicObjectsFall)
					{
						Position fallPosition = this.mapEditorScene.EditorScene.GetFallPosition(ref newValue);
						this.mapEditorScene.EditorScene.MoveObject(editorSceneObjectID, ref fallPosition);
						return;
					}
				}
				else
				{
					if (mapObject.Type.Type == MapObjectFactory.Type.ScriptArea)
					{
						this.mapEditorSceneScriptAreas.OnPositionChanged(mapObjectContainer, mapObject, ref oldValue, ref newValue);
						return;
					}
					if (mapObject.Type.Type == MapObjectFactory.Type.ZoneLocator)
					{
						this.mapEditorSceneZoneLocators.OnPositionChanged(mapObjectContainer, mapObject, ref oldValue, ref newValue);
						return;
					}
					if (mapObject.Type.Type == MapObjectFactory.Type.PatrolNode)
					{
						this.mapEditorAggroCircles.OnPositionChanged(mapObjectContainer, mapObject, ref oldValue, ref newValue);
						return;
					}
					if (mapObject.Type.Type == MapObjectFactory.Type.RoutePoint)
					{
						this.mapEditorAggroCircles.OnPositionChanged(mapObjectContainer, mapObject, ref oldValue, ref newValue);
						return;
					}
					if (mapObject.Type.Type == MapObjectFactory.Type.AstralBorder)
					{
						this.mapEditorSceneAstralBorders.OnPositionChanged(mapObjectContainer, mapObject, ref oldValue, ref newValue);
					}
				}
			}
		}

		// Token: 0x060005AA RID: 1450 RVA: 0x0002ED84 File Offset: 0x0002DD84
		private void OnRotationChanged(MapObjectContainer mapObjectContainer, IMapObject mapObject, ref Rotation oldValue, ref Rotation newValue)
		{
			if (mapObject != null)
			{
				int editorSceneObjectID;
				if (this.editorSceneObjectIDMap.TryGetValue(mapObject.ID, out editorSceneObjectID))
				{
					this.mapEditorScene.EditorScene.RotateObject(editorSceneObjectID, ref newValue);
				}
				this.mapEditorSceneLinks.OnRotationChanged(mapObjectContainer, mapObject, ref oldValue, ref newValue);
				if (mapObject.Type.Type == MapObjectFactory.Type.SpawnPoint)
				{
					this.mapEditorSceneSpawnPoints.OnRotationChanged(mapObjectContainer, mapObject, ref oldValue, ref newValue);
					this.mapEditorAggroCircles.OnRotationChanged(mapObjectContainer, mapObject, ref oldValue, ref newValue);
					return;
				}
				if (mapObject.Type.Type == MapObjectFactory.Type.ScriptArea)
				{
					this.mapEditorSceneScriptAreas.OnRotationChanged(mapObjectContainer, mapObject, ref oldValue, ref newValue);
					return;
				}
				if (mapObject.Type.Type == MapObjectFactory.Type.ZoneLocator)
				{
					this.mapEditorSceneZoneLocators.OnRotationChanged(mapObjectContainer, mapObject, ref oldValue, ref newValue);
					return;
				}
				if (mapObject.Type.Type == MapObjectFactory.Type.PatrolNode)
				{
					this.mapEditorAggroCircles.OnRotationChanged(mapObjectContainer, mapObject, ref oldValue, ref newValue);
					return;
				}
				if (mapObject.Type.Type == MapObjectFactory.Type.RoutePoint)
				{
					this.mapEditorAggroCircles.OnRotationChanged(mapObjectContainer, mapObject, ref oldValue, ref newValue);
					return;
				}
				if (mapObject.Type.Type == MapObjectFactory.Type.AstralBorder)
				{
					this.mapEditorSceneAstralBorders.OnRotationChanged(mapObjectContainer, mapObject, ref oldValue, ref newValue);
				}
			}
		}

		// Token: 0x060005AB RID: 1451 RVA: 0x0002EEC8 File Offset: 0x0002DEC8
		private void OnScaleChanged(MapObjectContainer mapObjectContainer, IMapObject mapObject, ref Scale oldValue, ref Scale newValue)
		{
			if (mapObject != null && (mapObject.Type.Type == MapObjectFactory.Type.StaticObject || mapObject.Type.Type == MapObjectFactory.Type.RoutePoint || mapObject.Type.Type == MapObjectFactory.Type.PermanentDevice))
			{
				int editorSceneObjectID;
				if (this.editorSceneObjectIDMap.TryGetValue(mapObject.ID, out editorSceneObjectID))
				{
					Scale scale = newValue;
					if (!MapSceneObjects.IgnoreAdditionalScaleRatio(mapObject.SceneName))
					{
						scale *= (float)this.mapEditorScene.ScaleRatio;
					}
					this.mapEditorScene.EditorScene.ScaleObject(editorSceneObjectID, ref scale);
				}
				this.mapEditorSceneLinks.OnScaleChanged(mapObjectContainer, mapObject, ref oldValue, ref newValue);
			}
		}

		// Token: 0x060005AC RID: 1452 RVA: 0x0002EF7C File Offset: 0x0002DF7C
		private void OnSelectChanged(MapObjectContainer mapObjectContainer, IMapObject mapObject, ref bool oldValue, ref bool newValue)
		{
			if (mapObject != null)
			{
				this.mapEditorSceneLinks.OnSelectChanged(mapObjectContainer, mapObject, ref oldValue, ref newValue);
				if (mapObject.Type.Type == MapObjectFactory.Type.SpawnPoint)
				{
					this.mapEditorSceneSpawnPoints.OnSelectChanged(mapObjectContainer, mapObject, ref oldValue, ref newValue);
					this.mapEditorAggroCircles.OnSelectChanged(mapObjectContainer, mapObject, ref oldValue, ref newValue);
					return;
				}
				if (mapObject.Type.Type == MapObjectFactory.Type.ScriptArea)
				{
					this.mapEditorSceneScriptAreas.OnSelectChanged(mapObjectContainer, mapObject, ref oldValue, ref newValue);
					return;
				}
				if (mapObject.Type.Type == MapObjectFactory.Type.ZoneLocator)
				{
					this.mapEditorSceneZoneLocators.OnSelectChanged(mapObjectContainer, mapObject, ref oldValue, ref newValue);
					return;
				}
				if (mapObject.Type.Type == MapObjectFactory.Type.PatrolNode)
				{
					this.mapEditorAggroCircles.OnSelectChanged(mapObjectContainer, mapObject, ref oldValue, ref newValue);
					return;
				}
				if (mapObject.Type.Type == MapObjectFactory.Type.RoutePoint)
				{
					this.mapEditorAggroCircles.OnSelectChanged(mapObjectContainer, mapObject, ref oldValue, ref newValue);
					return;
				}
				if (mapObject.Type.Type == MapObjectFactory.Type.AstralBorder)
				{
					this.mapEditorSceneAstralBorders.OnSelectChanged(mapObjectContainer, mapObject, ref oldValue, ref newValue);
				}
			}
		}

		// Token: 0x060005AD RID: 1453 RVA: 0x0002F094 File Offset: 0x0002E094
		private void OnSelectionColorChanged(MapObjectContainer mapObjectContainer, IMapObject mapObject, ref Color oldValue, ref Color newValue)
		{
			int editorSceneObjectID;
			if (mapObject != null && this.editorSceneObjectIDMap.TryGetValue(mapObject.ID, out editorSceneObjectID))
			{
				this.mapEditorScene.EditorScene.SelectObject(editorSceneObjectID, newValue.A > 0, newValue);
			}
		}

		// Token: 0x060005AE RID: 1454 RVA: 0x0002F0DC File Offset: 0x0002E0DC
		private void OnDynamicObjectsFallChanged(MapSceneParams mapSceneParams, ref bool oldValue, ref bool newValue)
		{
			foreach (KeyValuePair<int, IMapObject> keyValuePair in this.mapEditorScene.MapEditorMapObjectContainer.MapObjects)
			{
				IMapObject mapObject = keyValuePair.Value;
				int editorSceneObjectID;
				if (mapObject != null && mapObject.Type.Type == MapObjectFactory.Type.SpawnPoint && this.editorSceneObjectIDMap.TryGetValue(mapObject.ID, out editorSceneObjectID))
				{
					Position startPositon = mapObject.Position;
					if (this.mapEditorScene.MapSceneParams.DynamicObjectsFall)
					{
						Position fallPosition = this.mapEditorScene.EditorScene.GetFallPosition(ref startPositon);
						this.mapEditorScene.EditorScene.MoveObject(editorSceneObjectID, ref fallPosition);
					}
					else
					{
						this.mapEditorScene.EditorScene.MoveObject(editorSceneObjectID, ref startPositon);
					}
				}
			}
		}

		// Token: 0x060005AF RID: 1455 RVA: 0x0002F1C8 File Offset: 0x0002E1C8
		public void Bind(MapEditorScene _mapEditorScene)
		{
			this.mapEditorScene = _mapEditorScene;
			if (this.mapEditorScene != null && this.mapEditorScene.MapEditorMapObjectContainer != null)
			{
				this.mapEditorScene.MapEditorMapObjectContainer.MapObjectAdded += this.OnMapObjectAdded;
				this.mapEditorScene.MapEditorMapObjectContainer.MapObjectRemoved += this.OnMapObjectRemoved;
				this.mapEditorScene.MapEditorMapObjectContainer.PositionChanged += this.OnPositionChanged;
				this.mapEditorScene.MapEditorMapObjectContainer.RotationChanged += this.OnRotationChanged;
				this.mapEditorScene.MapEditorMapObjectContainer.ScaleChanged += this.OnScaleChanged;
				this.mapEditorScene.MapEditorMapObjectContainer.SelectChanged += this.OnSelectChanged;
				this.mapEditorScene.MapEditorMapObjectContainer.SelectionColorChanged += this.OnSelectionColorChanged;
				this.mapEditorScene.MapSceneParams.DynamicObjectsFallChanged += this.OnDynamicObjectsFallChanged;
				this.mapEditorSceneGraveyards.Bind(this.mapEditorScene, this);
				this.mapEditorSceneSpawnPoints.Bind(this.mapEditorScene, this);
				this.mapEditorSceneScriptAreas.Bind(this.mapEditorScene);
				this.mapEditorSceneZoneLocators.Bind(this.mapEditorScene);
				this.mapEditorSceneRoutePoints.Bind(this.mapEditorScene, this);
				this.mapEditorSceneSanctuarys.Bind(this.mapEditorScene, this);
				this.mapEditorSceneLinks.Bind(this.mapEditorScene);
				this.mapEditorAggroCircles.Bind(this.mapEditorScene);
				this.mapEditorSceneRouteObjects.Bind(this.mapEditorScene, this);
				this.mapEditorSceneClientSpawnPoints.Bind(this.mapEditorScene, this);
				this.mapEditorSceneAstralBorders.Bind(this.mapEditorScene);
				this.mapEditorSceneProjectiles.Bind(this.mapEditorScene, this);
				this.mapEditorScenePlayerRespawnPoints.Bind(this.mapEditorScene, this);
			}
		}

		// Token: 0x060005B0 RID: 1456 RVA: 0x0002F3BC File Offset: 0x0002E3BC
		public void Unbind()
		{
			if (this.mapEditorScene != null)
			{
				if (this.mapEditorScene.MapEditorMapObjectContainer != null)
				{
					this.mapEditorSceneGraveyards.Unbind();
					this.mapEditorSceneSpawnPoints.Unbind();
					this.mapEditorSceneScriptAreas.Unbind();
					this.mapEditorSceneZoneLocators.Unbind();
					this.mapEditorSceneRoutePoints.Unbind();
					this.mapEditorSceneSanctuarys.Unbind();
					this.mapEditorSceneLinks.Unbind();
					this.mapEditorAggroCircles.Unbind();
					this.mapEditorSceneRouteObjects.Unbind();
					this.mapEditorSceneClientSpawnPoints.Unbind();
					this.mapEditorSceneAstralBorders.Unbind();
					this.mapEditorSceneProjectiles.Unbind();
					this.mapEditorScenePlayerRespawnPoints.Unbind();
					this.mapEditorScene.MapEditorMapObjectContainer.MapObjectAdded -= this.OnMapObjectAdded;
					this.mapEditorScene.MapEditorMapObjectContainer.MapObjectRemoved -= this.OnMapObjectRemoved;
					this.mapEditorScene.MapEditorMapObjectContainer.PositionChanged -= this.OnPositionChanged;
					this.mapEditorScene.MapEditorMapObjectContainer.RotationChanged -= this.OnRotationChanged;
					this.mapEditorScene.MapEditorMapObjectContainer.ScaleChanged -= this.OnScaleChanged;
					this.mapEditorScene.MapEditorMapObjectContainer.SelectChanged -= this.OnSelectChanged;
					this.mapEditorScene.MapEditorMapObjectContainer.SelectionColorChanged -= this.OnSelectionColorChanged;
					this.mapEditorScene.MapSceneParams.DynamicObjectsFallChanged -= this.OnDynamicObjectsFallChanged;
				}
				this.mapEditorScene = null;
			}
		}

		// Token: 0x060005B1 RID: 1457 RVA: 0x0002F55C File Offset: 0x0002E55C
		public void RecreateMapObject(IMapObject mapObject, bool createNew)
		{
			if (this.mapEditorScene != null)
			{
				int editorSceneObjectID;
				bool objectExists = this.editorSceneObjectIDMap.TryGetValue(mapObject.ID, out editorSceneObjectID);
				if (objectExists)
				{
					this.mapEditorScene.EditorScene.DeleteObject(editorSceneObjectID);
					this.editorSceneObjectIDMap.Remove(mapObject.ID);
					this.mapObjectIDMap.Remove(editorSceneObjectID);
				}
				if (createNew || objectExists)
				{
					editorSceneObjectID = this.mapEditorScene.EditorScene.CreateObject(this.GetMapSceneObjectType(mapObject), this.mapEditorScene.CalculateColor, mapObject.SceneName, mapObject.DefaultSceneName, mapObject.Type.Type == MapObjectFactory.Type.StaticObject);
					if (editorSceneObjectID != -1)
					{
						this.editorSceneObjectIDMap[mapObject.ID] = editorSceneObjectID;
						this.mapObjectIDMap[editorSceneObjectID] = mapObject.ID;
						Position position = mapObject.Position;
						Rotation rotation = mapObject.Rotation;
						this.mapEditorScene.EditorScene.MoveObject(editorSceneObjectID, ref position);
						this.mapEditorScene.EditorScene.RotateObject(editorSceneObjectID, ref rotation);
						Scale scale = MapObjectCreationInfo.DefaultScale;
						if (mapObject.Type.Type == MapObjectFactory.Type.StaticObject || mapObject.Type.Type == MapObjectFactory.Type.RoutePoint || mapObject.Type.Type == MapObjectFactory.Type.PermanentDevice)
						{
							scale = mapObject.Scale;
						}
						if (!MapSceneObjects.IgnoreAdditionalScaleRatio(mapObject.SceneName))
						{
							scale *= (float)this.mapEditorScene.ScaleRatio;
						}
						this.mapEditorScene.EditorScene.ScaleObject(editorSceneObjectID, ref scale);
						if (mapObject.Type.Type == MapObjectFactory.Type.SpawnPoint && this.mapEditorScene.MapSceneParams.DynamicObjectsFall)
						{
							Position fallPosition = this.mapEditorScene.EditorScene.GetFallPosition(ref position);
							this.mapEditorScene.EditorScene.MoveObject(editorSceneObjectID, ref fallPosition);
						}
						Volume volume;
						this.mapEditorScene.EditorScene.GetObjectVolume(editorSceneObjectID, out volume);
						mapObject.Volume = volume;
						this.mapEditorScene.EditorScene.SelectObject(editorSceneObjectID, mapObject.HighlightColor.A > 0, mapObject.HighlightColor);
					}
				}
			}
		}

		// Token: 0x060005B2 RID: 1458 RVA: 0x0002F784 File Offset: 0x0002E784
		public int MapObjectIDToEditorSceneObjectID(int id)
		{
			int _id;
			if (this.editorSceneObjectIDMap.TryGetValue(id, out _id))
			{
				return _id;
			}
			return -1;
		}

		// Token: 0x060005B3 RID: 1459 RVA: 0x0002F7A4 File Offset: 0x0002E7A4
		public int EditorSceneObjectIDToMapObjectID(int id)
		{
			int _id;
			if (this.mapObjectIDMap.TryGetValue(id, out _id))
			{
				return _id;
			}
			return -1;
		}

		// Token: 0x0400043E RID: 1086
		private readonly IMapSceneObjectTypeContainer mapSceneObjectTypeContainer = new MapSceneObjectTypeContainer();

		// Token: 0x0400043F RID: 1087
		private readonly MapEditorSceneGraveyards mapEditorSceneGraveyards = new MapEditorSceneGraveyards();

		// Token: 0x04000440 RID: 1088
		private readonly MapEditorSceneSpawnPoints mapEditorSceneSpawnPoints = new MapEditorSceneSpawnPoints();

		// Token: 0x04000441 RID: 1089
		private readonly MapEditorSceneScriptAreas mapEditorSceneScriptAreas = new MapEditorSceneScriptAreas();

		// Token: 0x04000442 RID: 1090
		private readonly MapEditorSceneZoneLocators mapEditorSceneZoneLocators = new MapEditorSceneZoneLocators();

		// Token: 0x04000443 RID: 1091
		private readonly MapEditorSceneRoutePoints mapEditorSceneRoutePoints = new MapEditorSceneRoutePoints();

		// Token: 0x04000444 RID: 1092
		private readonly MapEditorSceneSanctuarys mapEditorSceneSanctuarys = new MapEditorSceneSanctuarys();

		// Token: 0x04000445 RID: 1093
		private readonly MapEditorSceneLinks mapEditorSceneLinks = new MapEditorSceneLinks();

		// Token: 0x04000446 RID: 1094
		private readonly MapEditorAggroCircles mapEditorAggroCircles = new MapEditorAggroCircles();

		// Token: 0x04000447 RID: 1095
		private readonly MapEditorSceneRouteObjects mapEditorSceneRouteObjects = new MapEditorSceneRouteObjects();

		// Token: 0x04000448 RID: 1096
		private readonly MapEditorSceneClientSpawnPoints mapEditorSceneClientSpawnPoints = new MapEditorSceneClientSpawnPoints();

		// Token: 0x04000449 RID: 1097
		private readonly MapEditorSceneAstralBorders mapEditorSceneAstralBorders = new MapEditorSceneAstralBorders();

		// Token: 0x0400044A RID: 1098
		private readonly MapEditorSceneProjectiles mapEditorSceneProjectiles = new MapEditorSceneProjectiles();

		// Token: 0x0400044B RID: 1099
		private readonly MapEditorScenePlayerRespawnPoints mapEditorScenePlayerRespawnPoints = new MapEditorScenePlayerRespawnPoints();

		// Token: 0x0400044C RID: 1100
		private readonly Dictionary<int, int> editorSceneObjectIDMap = new Dictionary<int, int>();

		// Token: 0x0400044D RID: 1101
		private readonly Dictionary<int, int> mapObjectIDMap = new Dictionary<int, int>();

		// Token: 0x0400044E RID: 1102
		private MapEditorScene mapEditorScene;
	}
}
