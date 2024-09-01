using System;
using System.Collections.Generic;
using System.Drawing;
using Db;
using MapEditor.Map.Containers;
using MapEditor.Map.MapObjects;
using Tools.MapObjects;

namespace MapEditor.Scene.Types
{
	// Token: 0x0200001D RID: 29
	internal class MapEditorSceneSpawnPoints
	{
		// Token: 0x06000259 RID: 601 RVA: 0x0001A440 File Offset: 0x00019440
		private int CreateUserGeometry(IMapObject mapObject, int userGeometryID)
		{
			if (this.mapEditorScene.MapSceneParams.ShowSpawnPointUserGeometry || mapObject.Select)
			{
				SpawnPoint spawnPoint = mapObject as SpawnPoint;
				if (spawnPoint != null && spawnPoint.SpawnPointData != null)
				{
					if (spawnPoint.SpawnPointData.SpawnPointType == SpawnPointType.Circle)
					{
						CircleSpawnPointData circleSpawnPointData = spawnPoint.SpawnPointData as CircleSpawnPointData;
						if (circleSpawnPointData != null)
						{
							Position position = mapObject.Position;
							return this.mapEditorScene.EditorScene.CreateUserGeometry_Circle(userGeometryID, ref position, 0f, circleSpawnPointData.Radius * 2.0, circleSpawnPointData.Radius * 2.0, MapEditorSceneSpawnPoints.userGeometryColor, mapObject.Select, false);
						}
					}
					else if (spawnPoint.SpawnPointData.SpawnPointType == SpawnPointType.Ellipse)
					{
						EllipseSpawnPointData ellipseSpawnPointData = spawnPoint.SpawnPointData as EllipseSpawnPointData;
						if (ellipseSpawnPointData != null)
						{
							Position position2 = mapObject.Position;
							return this.mapEditorScene.EditorScene.CreateUserGeometry_Circle(userGeometryID, ref position2, mapObject.Rotation.Yaw, ellipseSpawnPointData.SemiaxisA * 2.0, ellipseSpawnPointData.SemiaxisB * 2.0, MapEditorSceneSpawnPoints.userGeometryColor, mapObject.Select, false);
						}
					}
					else if (spawnPoint.SpawnPointData.SpawnPointType == SpawnPointType.SpawnCircle)
					{
						SpawnCircleSpawnPointData spawnCircleSpawnPointData = spawnPoint.SpawnPointData as SpawnCircleSpawnPointData;
						if (spawnCircleSpawnPointData != null)
						{
							Position position3 = mapObject.Position;
							return this.mapEditorScene.EditorScene.CreateUserGeometry_Circle(userGeometryID, ref position3, 0f, spawnCircleSpawnPointData.Radius * 2.0, spawnCircleSpawnPointData.Radius * 2.0, MapEditorSceneSpawnPoints.userGeometryColor, mapObject.Select, false);
						}
					}
				}
			}
			return -1;
		}

		// Token: 0x0600025A RID: 602 RVA: 0x0001A5D4 File Offset: 0x000195D4
		private void DestroyUserGeometry(IMapObject mapObject, int userGeometryID)
		{
			SpawnPoint spawnPoint = mapObject as SpawnPoint;
			if (spawnPoint != null && userGeometryID != -1)
			{
				this.mapEditorScene.EditorScene.DeleteUserGeometry(userGeometryID);
			}
		}

		// Token: 0x0600025B RID: 603 RVA: 0x0001A600 File Offset: 0x00019600
		private void CreateUserGeometries()
		{
			foreach (KeyValuePair<int, IMapObject> keyValuePair in this.mapEditorScene.MapEditorMapObjectContainer.MapObjects)
			{
				IMapObject mapObject = keyValuePair.Value;
				int userGeometryID;
				if (mapObject != null && mapObject.Type.Type == MapObjectFactory.Type.SpawnPoint && !this.userGeometryIDMap.TryGetValue(mapObject.ID, out userGeometryID))
				{
					userGeometryID = this.CreateUserGeometry(mapObject, -1);
					if (userGeometryID != -1)
					{
						this.userGeometryIDMap[mapObject.ID] = userGeometryID;
					}
				}
			}
		}

		// Token: 0x0600025C RID: 604 RVA: 0x0001A6AC File Offset: 0x000196AC
		private void DestroyUserGeometries()
		{
			foreach (KeyValuePair<int, IMapObject> keyValuePair in this.mapEditorScene.MapEditorMapObjectContainer.MapObjects)
			{
				IMapObject mapObject = keyValuePair.Value;
				int userGeometryID;
				if (mapObject != null && mapObject.Type.Type == MapObjectFactory.Type.SpawnPoint && !mapObject.Select && this.userGeometryIDMap.TryGetValue(mapObject.ID, out userGeometryID))
				{
					this.DestroyUserGeometry(mapObject, userGeometryID);
					this.userGeometryIDMap.Remove(mapObject.ID);
				}
			}
		}

		// Token: 0x0600025D RID: 605 RVA: 0x0001A75C File Offset: 0x0001975C
		private void OnShowUserGeometryChanged(MapSceneParams mapSceneParams, ref bool oldValue, ref bool newValue)
		{
			if (this.mapEditorScene.MapSceneParams != null)
			{
				if (this.mapEditorScene.MapSceneParams.ShowSpawnPointUserGeometry)
				{
					this.CreateUserGeometries();
					return;
				}
				this.DestroyUserGeometries();
			}
		}

		// Token: 0x0600025E RID: 606 RVA: 0x0001A78C File Offset: 0x0001978C
		public void OnMapObjectAdded(MapObjectContainer mapObjectContainer, IMapObject mapObject, int editorSceneObjectID)
		{
			int userGeometryID = this.CreateUserGeometry(mapObject, -1);
			if (userGeometryID != -1)
			{
				this.userGeometryIDMap[mapObject.ID] = userGeometryID;
			}
		}

		// Token: 0x0600025F RID: 607 RVA: 0x0001A7B8 File Offset: 0x000197B8
		public void OnMapObjectRemoved(MapObjectContainer mapObjectContainer, IMapObject mapObject)
		{
			int userGeometryID;
			if (this.userGeometryIDMap.TryGetValue(mapObject.ID, out userGeometryID) && userGeometryID != -1)
			{
				this.DestroyUserGeometry(mapObject, userGeometryID);
				this.userGeometryIDMap.Remove(mapObject.ID);
			}
		}

		// Token: 0x06000260 RID: 608 RVA: 0x0001A7F8 File Offset: 0x000197F8
		public void OnPositionChanged(MapObjectContainer mapObjectContainer, IMapObject mapObject, ref Position oldValue, ref Position newValue)
		{
			int userGeometryID;
			if (this.userGeometryIDMap.TryGetValue(mapObject.ID, out userGeometryID) && userGeometryID != -1)
			{
				this.CreateUserGeometry(mapObject, userGeometryID);
			}
		}

		// Token: 0x06000261 RID: 609 RVA: 0x0001A828 File Offset: 0x00019828
		public void OnRotationChanged(MapObjectContainer mapObjectContainer, IMapObject mapObject, ref Rotation oldValue, ref Rotation newValue)
		{
			SpawnPoint spawnPoint = mapObject as SpawnPoint;
			if (spawnPoint != null && spawnPoint.SpawnPointData != null)
			{
				if (spawnPoint.SpawnPointData.SpawnPointType == SpawnPointType.Circle)
				{
					return;
				}
				if (spawnPoint.SpawnPointData.SpawnPointType == SpawnPointType.Ellipse)
				{
					int userGeometryID;
					if (this.userGeometryIDMap.TryGetValue(mapObject.ID, out userGeometryID) && userGeometryID != -1)
					{
						this.CreateUserGeometry(mapObject, userGeometryID);
						return;
					}
				}
				else
				{
					SpawnPointType spawnPointType = spawnPoint.SpawnPointData.SpawnPointType;
				}
			}
		}

		// Token: 0x06000262 RID: 610 RVA: 0x0001A898 File Offset: 0x00019898
		public void OnScaleChanged(MapObjectContainer mapObjectContainer, IMapObject mapObject, ref Scale oldValue, ref Scale newValue)
		{
			int userGeometryID;
			if (this.userGeometryIDMap.TryGetValue(mapObject.ID, out userGeometryID) && userGeometryID != -1)
			{
				this.CreateUserGeometry(mapObject, userGeometryID);
			}
		}

		// Token: 0x06000263 RID: 611 RVA: 0x0001A8C8 File Offset: 0x000198C8
		public void OnSelectChanged(MapObjectContainer mapObjectContainer, IMapObject mapObject, ref bool oldValue, ref bool newValue)
		{
			int userGeometryID2;
			if (mapObject.Select || this.mapEditorScene.MapSceneParams.ShowSpawnPointUserGeometry)
			{
				int userGeometryID;
				if (this.userGeometryIDMap.TryGetValue(mapObject.ID, out userGeometryID))
				{
					this.CreateUserGeometry(mapObject, userGeometryID);
					return;
				}
				if (mapObject.Select)
				{
					userGeometryID = this.CreateUserGeometry(mapObject, -1);
					if (userGeometryID != -1)
					{
						this.userGeometryIDMap[mapObject.ID] = userGeometryID;
						return;
					}
				}
			}
			else if (this.userGeometryIDMap.TryGetValue(mapObject.ID, out userGeometryID2) && userGeometryID2 != -1)
			{
				this.DestroyUserGeometry(mapObject, userGeometryID2);
				this.userGeometryIDMap.Remove(mapObject.ID);
			}
		}

		// Token: 0x06000264 RID: 612 RVA: 0x0001A96C File Offset: 0x0001996C
		private void OnSpawnPointTypeChanged(MapEditorMapObjectContainer mapEditorMapObjectContainer, SpawnPoint spawnPoint, ref SpawnPointData oldValue, ref SpawnPointData newValue)
		{
			int userGeometryID3;
			if (newValue == null)
			{
				int userGeometryID;
				if (this.userGeometryIDMap.TryGetValue(spawnPoint.ID, out userGeometryID) && userGeometryID != -1)
				{
					this.DestroyUserGeometry(spawnPoint, userGeometryID);
					this.userGeometryIDMap.Remove(spawnPoint.ID);
					return;
				}
			}
			else if (newValue.SpawnPointType == SpawnPointType.Circle || newValue.SpawnPointType == SpawnPointType.Ellipse || newValue.SpawnPointType == SpawnPointType.SpawnCircle)
			{
				int userGeometryID2;
				if (this.userGeometryIDMap.TryGetValue(spawnPoint.ID, out userGeometryID2) && userGeometryID2 != -1)
				{
					this.DestroyUserGeometry(spawnPoint, userGeometryID2);
					this.userGeometryIDMap.Remove(spawnPoint.ID);
				}
				userGeometryID2 = this.CreateUserGeometry(spawnPoint, -1);
				if (userGeometryID2 != -1)
				{
					this.userGeometryIDMap[spawnPoint.ID] = userGeometryID2;
					return;
				}
			}
			else if (this.userGeometryIDMap.TryGetValue(spawnPoint.ID, out userGeometryID3) && userGeometryID3 != -1)
			{
				this.DestroyUserGeometry(spawnPoint, userGeometryID3);
				this.userGeometryIDMap.Remove(spawnPoint.ID);
			}
		}

		// Token: 0x06000265 RID: 613 RVA: 0x0001AA60 File Offset: 0x00019A60
		private void OnSpawnPointDataFieldChanged(MapEditorMapObjectContainer mapEditorMapObjectContainer, SpawnPoint spawnPoint)
		{
			int userGeometryID;
			if (spawnPoint != null && (spawnPoint.SpawnPointData.SpawnPointType == SpawnPointType.Circle || spawnPoint.SpawnPointData.SpawnPointType == SpawnPointType.Ellipse || spawnPoint.SpawnPointData.SpawnPointType == SpawnPointType.SpawnCircle) && this.userGeometryIDMap.TryGetValue(spawnPoint.ID, out userGeometryID) && userGeometryID != -1)
			{
				this.CreateUserGeometry(spawnPoint, userGeometryID);
			}
		}

		// Token: 0x06000266 RID: 614 RVA: 0x0001AABC File Offset: 0x00019ABC
		private void OnSpawnPointMobSceneNameChanged(MapEditorMapObjectContainer mapEditorMapObjectContainer, SpawnPoint spawnPoint, ref string oldValue, ref string newValue)
		{
			this.mapSceneObjects.RecreateMapObject(spawnPoint, false);
		}

		// Token: 0x06000267 RID: 615 RVA: 0x0001AACC File Offset: 0x00019ACC
		private void OnSpawnPointSpawnTunerChanged(MapEditorMapObjectContainer mapEditorMapObjectContainer, SpawnPoint spawnPoint, ref string oldValue, ref string newValue)
		{
			string animationName = SpawnPoint.GetFixedIdleAnimationName(spawnPoint, this.mainDb);
			if (!string.IsNullOrEmpty(animationName))
			{
				AnimationProperties animationProperties = new AnimationProperties(1f);
				animationProperties.Name = animationName;
				animationProperties.LowerName = animationName;
				animationProperties.Speed = 1f;
				animationProperties.Lower = true;
				animationProperties.Upper = true;
				animationProperties.Looped = true;
				int editorSceneObjectID = this.mapSceneObjects.MapObjectIDToEditorSceneObjectID(spawnPoint.ID);
				if (editorSceneObjectID != -1)
				{
					this.mapEditorScene.EditorScene.PlayObjectAnimation(editorSceneObjectID, ref animationProperties);
				}
			}
		}

		// Token: 0x06000268 RID: 616 RVA: 0x0001AB5C File Offset: 0x00019B5C
		public void Bind(MapEditorScene _mapEditorScene, MapSceneObjects _mapSceneObjects)
		{
			this.mapEditorScene = _mapEditorScene;
			this.mapSceneObjects = _mapSceneObjects;
			this.mainDb = IDatabase.GetMainDatabase();
			if (this.mapEditorScene != null && this.mapSceneObjects != null && this.mainDb != null)
			{
				this.mapEditorScene.MapSceneParams.ShowSpawnPointUserGeometryChanged += this.OnShowUserGeometryChanged;
				SpawnPointContainer spawnPointContainer = this.mapEditorScene.MapEditorMapObjectContainer.SpawnPointContainer;
				if (spawnPointContainer != null)
				{
					spawnPointContainer.SpawnPointTypeChanged += this.OnSpawnPointTypeChanged;
					spawnPointContainer.SpawnPointDataFieldChanged += this.OnSpawnPointDataFieldChanged;
					spawnPointContainer.SpawnPointMobSceneNameChanged += this.OnSpawnPointMobSceneNameChanged;
					spawnPointContainer.SpawnPointSpawnTunerChanged += this.OnSpawnPointSpawnTunerChanged;
				}
				this.CreateUserGeometries();
			}
		}

		// Token: 0x06000269 RID: 617 RVA: 0x0001AC20 File Offset: 0x00019C20
		public void Unbind()
		{
			if (this.mapEditorScene != null)
			{
				this.DestroyUserGeometries();
				this.mapEditorScene.MapSceneParams.ShowSpawnPointUserGeometryChanged -= this.OnShowUserGeometryChanged;
				SpawnPointContainer spawnPointContainer = this.mapEditorScene.MapEditorMapObjectContainer.SpawnPointContainer;
				if (spawnPointContainer != null)
				{
					spawnPointContainer.SpawnPointTypeChanged -= this.OnSpawnPointTypeChanged;
					spawnPointContainer.SpawnPointDataFieldChanged -= this.OnSpawnPointDataFieldChanged;
					spawnPointContainer.SpawnPointMobSceneNameChanged -= this.OnSpawnPointMobSceneNameChanged;
					spawnPointContainer.SpawnPointSpawnTunerChanged -= this.OnSpawnPointSpawnTunerChanged;
				}
				this.mapEditorScene = null;
				this.mainDb = null;
			}
			this.mapSceneObjects = null;
		}

		// Token: 0x04000223 RID: 547
		private static readonly Color userGeometryColor = Color.FromArgb(MapEditorScene.DefaultTransparentColorAlpha, SpawnPoint.InterfaceColor);

		// Token: 0x04000224 RID: 548
		private readonly Dictionary<int, int> userGeometryIDMap = new Dictionary<int, int>();

		// Token: 0x04000225 RID: 549
		private MapEditorScene mapEditorScene;

		// Token: 0x04000226 RID: 550
		private MapSceneObjects mapSceneObjects;

		// Token: 0x04000227 RID: 551
		private IDatabase mainDb;
	}
}
