using System;
using System.Collections.Generic;
using System.Drawing;
using MapEditor.Map.Containers;
using MapEditor.Map.MapObjects;
using Tools.MapObjects;

namespace MapEditor.Scene.Types
{
	// Token: 0x020002A1 RID: 673
	internal class MapEditorAggroCircles
	{
		// Token: 0x06001F5D RID: 8029 RVA: 0x000C927C File Offset: 0x000C827C
		private int CreateUserGeometry(IMapObject mapObject, int userGeometryID)
		{
			double aggroRadius;
			if ((!this.mapEditorScene.MapSceneParams.ShowMobsAggroRadius && !mapObject.Select) || !PatrolNode.GetAggroRadius(mapObject, out aggroRadius))
			{
				return -1;
			}
			Position position = mapObject.Position;
			if (this.mapEditorScene.MapSceneParams.ShowMobsAggroRadiusAsVolumes)
			{
				return this.mapEditorScene.EditorScene.CreateUserGeometry_Cylinder(userGeometryID, ref position, 0f, aggroRadius * 2.0 * this.mapEditorScene.ScaleRatio, aggroRadius * 2.0 * this.mapEditorScene.ScaleRatio, MapEditorAggroCircles.aggroVolumeHalfheight, MapEditorAggroCircles.userGeometryColor, mapObject.Select);
			}
			return this.mapEditorScene.EditorScene.CreateUserGeometry_Circle(userGeometryID, ref position, 0f, aggroRadius * 2.0 * this.mapEditorScene.ScaleRatio, aggroRadius * 2.0 * this.mapEditorScene.ScaleRatio, MapEditorAggroCircles.userGeometryColor, mapObject.Select, false);
		}

		// Token: 0x06001F5E RID: 8030 RVA: 0x000C9377 File Offset: 0x000C8377
		private void DestroyUserGeometry(int userGeometryID)
		{
			if (userGeometryID != -1)
			{
				this.mapEditorScene.EditorScene.DeleteUserGeometry(userGeometryID);
			}
		}

		// Token: 0x06001F5F RID: 8031 RVA: 0x000C9390 File Offset: 0x000C8390
		private void UpdateUserGeometry(IMapObject mapObject)
		{
			if (mapObject != null)
			{
				int userGeometryID;
				if (!this.userGeometryIDMap.TryGetValue(mapObject.ID, out userGeometryID))
				{
					userGeometryID = -1;
				}
				if (userGeometryID != -1)
				{
					int newUserGeometryID = this.CreateUserGeometry(mapObject, userGeometryID);
					if (newUserGeometryID == -1)
					{
						this.DestroyUserGeometry(userGeometryID);
						this.userGeometryIDMap.Remove(mapObject.ID);
						return;
					}
				}
				else
				{
					int newUserGeometryID2 = this.CreateUserGeometry(mapObject, -1);
					if (newUserGeometryID2 != -1)
					{
						this.userGeometryIDMap[mapObject.ID] = newUserGeometryID2;
					}
				}
			}
		}

		// Token: 0x06001F60 RID: 8032 RVA: 0x000C9404 File Offset: 0x000C8404
		private void UpdateUserGeometries()
		{
			if (this.mapEditorScene.MapSceneParams.ShowMobsAggroRadius)
			{
				this.DestroyUserGeometries(true);
				this.CreateUserGeometries();
				return;
			}
			List<IMapObject> mapObjects = new List<IMapObject>();
			foreach (KeyValuePair<int, int> keyValuePair in this.userGeometryIDMap)
			{
				IMapObject mapObject;
				if (this.mapEditorScene.MapEditorMapObjectContainer.MapObjects.TryGetValue(keyValuePair.Key, out mapObject) && mapObject != null)
				{
					mapObjects.Add(mapObject);
				}
			}
			this.DestroyUserGeometries(true);
			foreach (IMapObject mapObject2 in mapObjects)
			{
				int userGeometryID = this.CreateUserGeometry(mapObject2, -1);
				if (userGeometryID != -1)
				{
					this.userGeometryIDMap[mapObject2.ID] = userGeometryID;
				}
			}
		}

		// Token: 0x06001F61 RID: 8033 RVA: 0x000C9504 File Offset: 0x000C8504
		private void CreateUserGeometries()
		{
			foreach (KeyValuePair<int, IMapObject> keyValuePair in this.mapEditorScene.MapEditorMapObjectContainer.MapObjects)
			{
				IMapObject mapObject = keyValuePair.Value;
				int userGeometryID;
				if (mapObject != null && !this.userGeometryIDMap.TryGetValue(mapObject.ID, out userGeometryID))
				{
					userGeometryID = this.CreateUserGeometry(mapObject, -1);
					if (userGeometryID != -1)
					{
						this.userGeometryIDMap[mapObject.ID] = userGeometryID;
					}
				}
			}
		}

		// Token: 0x06001F62 RID: 8034 RVA: 0x000C959C File Offset: 0x000C859C
		private void DestroyUserGeometries(bool all)
		{
			foreach (KeyValuePair<int, IMapObject> keyValuePair in this.mapEditorScene.MapEditorMapObjectContainer.MapObjects)
			{
				IMapObject mapObject = keyValuePair.Value;
				int userGeometryID;
				if (mapObject != null && (all || !mapObject.Select) && this.userGeometryIDMap.TryGetValue(mapObject.ID, out userGeometryID))
				{
					this.DestroyUserGeometry(userGeometryID);
					this.userGeometryIDMap.Remove(mapObject.ID);
				}
			}
		}

		// Token: 0x06001F63 RID: 8035 RVA: 0x000C9638 File Offset: 0x000C8638
		private void OnShowMobsAggroRadiusChanged(MapSceneParams mapSceneParams, ref bool oldValue, ref bool newValue)
		{
			if (this.mapEditorScene.MapSceneParams != null)
			{
				if (this.mapEditorScene.MapSceneParams.ShowMobsAggroRadius)
				{
					this.CreateUserGeometries();
					return;
				}
				this.DestroyUserGeometries(false);
			}
		}

		// Token: 0x06001F64 RID: 8036 RVA: 0x000C9667 File Offset: 0x000C8667
		private void OnShowMobsAggroRadiusAsVolumesChanged(MapSceneParams mapSceneParams, ref bool oldValue, ref bool newValue)
		{
			if (this.mapEditorScene.MapSceneParams != null)
			{
				this.UpdateUserGeometries();
			}
		}

		// Token: 0x06001F65 RID: 8037 RVA: 0x000C967C File Offset: 0x000C867C
		public void OnMapObjectAdded(MapObjectContainer mapObjectContainer, IMapObject mapObject)
		{
			int userGeometryID = this.CreateUserGeometry(mapObject, -1);
			if (userGeometryID != -1)
			{
				this.userGeometryIDMap[mapObject.ID] = userGeometryID;
			}
		}

		// Token: 0x06001F66 RID: 8038 RVA: 0x000C96A8 File Offset: 0x000C86A8
		public void OnMapObjectRemoved(MapObjectContainer mapObjectContainer, IMapObject mapObject)
		{
			int userGeometryID;
			if (this.userGeometryIDMap.TryGetValue(mapObject.ID, out userGeometryID) && userGeometryID != -1)
			{
				this.DestroyUserGeometry(userGeometryID);
				this.userGeometryIDMap.Remove(mapObject.ID);
			}
		}

		// Token: 0x06001F67 RID: 8039 RVA: 0x000C96E8 File Offset: 0x000C86E8
		public void OnPositionChanged(MapObjectContainer mapObjectContainer, IMapObject mapObject, ref Position oldValue, ref Position newValue)
		{
			int userGeometryID;
			if (this.userGeometryIDMap.TryGetValue(mapObject.ID, out userGeometryID) && userGeometryID != -1)
			{
				this.CreateUserGeometry(mapObject, userGeometryID);
			}
		}

		// Token: 0x06001F68 RID: 8040 RVA: 0x000C9717 File Offset: 0x000C8717
		public void OnRotationChanged(MapObjectContainer mapObjectContainer, IMapObject mapObject, ref Rotation oldValue, ref Rotation newValue)
		{
		}

		// Token: 0x06001F69 RID: 8041 RVA: 0x000C971C File Offset: 0x000C871C
		public void OnSelectChanged(MapObjectContainer mapObjectContainer, IMapObject mapObject, ref bool oldValue, ref bool newValue)
		{
			int userGeometryID2;
			if (newValue || this.mapEditorScene.MapSceneParams.ShowMobsAggroRadius)
			{
				int userGeometryID;
				if (this.userGeometryIDMap.TryGetValue(mapObject.ID, out userGeometryID))
				{
					this.CreateUserGeometry(mapObject, userGeometryID);
					return;
				}
				if (newValue)
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
				this.DestroyUserGeometry(userGeometryID2);
				this.userGeometryIDMap.Remove(mapObject.ID);
			}
		}

		// Token: 0x06001F6A RID: 8042 RVA: 0x000C97B6 File Offset: 0x000C87B6
		private void OnPatrolNodeAggroRadiusChanged(MapEditorMapObjectContainer mapEditorMapObjectContainer, PatrolNode patrolNode, ref double oldValue, ref double newValue)
		{
			this.UpdateUserGeometry(patrolNode);
		}

		// Token: 0x06001F6B RID: 8043 RVA: 0x000C97BF File Offset: 0x000C87BF
		private void OnRoutePointAggroRadiusChanged(MapEditorMapObjectContainer mapEditorMapObjectContainer, RoutePoint routePoint, ref double oldValue, ref double newValue)
		{
			this.UpdateUserGeometry(routePoint);
		}

		// Token: 0x06001F6C RID: 8044 RVA: 0x000C97C8 File Offset: 0x000C87C8
		private void OnRoutePointTypeChanged(MapEditorMapObjectContainer mapEditorMapObjectContainer, RoutePoint routePoint, ref RoutePointType oldValue, ref RoutePointType newValue)
		{
			this.UpdateUserGeometry(routePoint);
		}

		// Token: 0x06001F6D RID: 8045 RVA: 0x000C97D1 File Offset: 0x000C87D1
		private void OnSpawnPointAggroRadiusChanged(MapEditorMapObjectContainer mapEditorMapObjectContainer, SpawnPoint spawnPoint, ref double oldValue, ref double newValue)
		{
			this.UpdateUserGeometry(spawnPoint);
		}

		// Token: 0x06001F6E RID: 8046 RVA: 0x000C97DA File Offset: 0x000C87DA
		private void OnSpawnPointTypeChanged(MapEditorMapObjectContainer mapEditorMapObjectContainer, SpawnPoint spawnPoint, ref SpawnPointData oldValue, ref SpawnPointData newValue)
		{
			this.UpdateUserGeometry(spawnPoint);
		}

		// Token: 0x06001F6F RID: 8047 RVA: 0x000C97E4 File Offset: 0x000C87E4
		public void Bind(MapEditorScene _mapEditorScene)
		{
			this.mapEditorScene = _mapEditorScene;
			if (this.mapEditorScene != null)
			{
				this.mapEditorScene.MapSceneParams.ShowMobsAggroRadiusChanged += this.OnShowMobsAggroRadiusChanged;
				this.mapEditorScene.MapSceneParams.ShowMobsAggroRadiusAsVolumesChanged += this.OnShowMobsAggroRadiusAsVolumesChanged;
				PatrolNodeContainer patrolNodeContainer = this.mapEditorScene.MapEditorMapObjectContainer.PatrolNodeContainer;
				if (patrolNodeContainer != null)
				{
					patrolNodeContainer.PatrolNodeAggroRadiusChanged += this.OnPatrolNodeAggroRadiusChanged;
				}
				RoutePointContainer routePointContainer = this.mapEditorScene.MapEditorMapObjectContainer.RoutePointContainer;
				if (routePointContainer != null)
				{
					routePointContainer.RoutePointAggroRadiusChanged += this.OnRoutePointAggroRadiusChanged;
					routePointContainer.RoutePointTypeChanged += this.OnRoutePointTypeChanged;
				}
				SpawnPointContainer spawnPointContainer = this.mapEditorScene.MapEditorMapObjectContainer.SpawnPointContainer;
				if (spawnPointContainer != null)
				{
					spawnPointContainer.SpawnPointAggroRadiusChanged += this.OnSpawnPointAggroRadiusChanged;
					spawnPointContainer.SpawnPointTypeChanged += this.OnSpawnPointTypeChanged;
				}
				this.CreateUserGeometries();
			}
		}

		// Token: 0x06001F70 RID: 8048 RVA: 0x000C98D8 File Offset: 0x000C88D8
		public void Unbind()
		{
			if (this.mapEditorScene != null)
			{
				this.DestroyUserGeometries(true);
				this.mapEditorScene.MapSceneParams.ShowMobsAggroRadiusChanged -= this.OnShowMobsAggroRadiusChanged;
				this.mapEditorScene.MapSceneParams.ShowMobsAggroRadiusAsVolumesChanged -= this.OnShowMobsAggroRadiusAsVolumesChanged;
				PatrolNodeContainer patrolNodeContainer = this.mapEditorScene.MapEditorMapObjectContainer.PatrolNodeContainer;
				if (patrolNodeContainer != null)
				{
					patrolNodeContainer.PatrolNodeAggroRadiusChanged -= this.OnPatrolNodeAggroRadiusChanged;
				}
				RoutePointContainer routePointContainer = this.mapEditorScene.MapEditorMapObjectContainer.RoutePointContainer;
				if (routePointContainer != null)
				{
					routePointContainer.RoutePointAggroRadiusChanged -= this.OnRoutePointAggroRadiusChanged;
					routePointContainer.RoutePointTypeChanged -= this.OnRoutePointTypeChanged;
				}
				SpawnPointContainer spawnPointContainer = this.mapEditorScene.MapEditorMapObjectContainer.SpawnPointContainer;
				if (spawnPointContainer != null)
				{
					spawnPointContainer.SpawnPointAggroRadiusChanged -= this.OnSpawnPointAggroRadiusChanged;
					spawnPointContainer.SpawnPointTypeChanged -= this.OnSpawnPointTypeChanged;
				}
				this.mapEditorScene = null;
			}
		}

		// Token: 0x04001364 RID: 4964
		private static readonly Color userGeometryColor = Color.FromArgb(MapEditorScene.DefaultTransparentColorAlpha, SpawnPoint.AggroCircleColor);

		// Token: 0x04001365 RID: 4965
		private static readonly double aggroVolumeHalfheight = SpawnPoint.DefaultAggroRadius;

		// Token: 0x04001366 RID: 4966
		private readonly Dictionary<int, int> userGeometryIDMap = new Dictionary<int, int>();

		// Token: 0x04001367 RID: 4967
		private MapEditorScene mapEditorScene;
	}
}
