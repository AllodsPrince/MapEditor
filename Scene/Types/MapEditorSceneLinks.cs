using System;
using System.Collections.Generic;
using System.Drawing;
using MapEditor.Map.Containers;
using MapEditor.Map.MapObjects;
using Tools.Geometry;
using Tools.LinkContainer;
using Tools.MapObjects;

namespace MapEditor.Scene.Types
{
	// Token: 0x020001BA RID: 442
	internal class MapEditorSceneLinks
	{
		// Token: 0x0600155D RID: 5469 RVA: 0x0009B0B4 File Offset: 0x0009A0B4
		private static Color GetLinkColor(MapObjectContainer mapObjectContainer, IMapObject left, IMapObject right)
		{
			if (mapObjectContainer != null)
			{
				ILinkData linkData = mapObjectContainer.GetLinkData(left, right);
				if (linkData != null)
				{
					PatrolLinkData patrolLinkData = linkData as PatrolLinkData;
					if (patrolLinkData != null)
					{
						if (!string.IsNullOrEmpty(patrolLinkData.Start))
						{
							return MapEditorSceneLinks.directionalUserGeometryColor;
						}
						if (patrolLinkData.Type == PatrolNodeLinkType.Walk)
						{
							return MapEditorSceneLinks.walkUserGeometryColor;
						}
						if (patrolLinkData.Type == PatrolNodeLinkType.Fly)
						{
							return MapEditorSceneLinks.flyUserGeometryColor;
						}
						if (patrolLinkData.Type == PatrolNodeLinkType.Teleport)
						{
							return MapEditorSceneLinks.teleportUserGeometryColor;
						}
					}
					else
					{
						GraveyardLinkData graveyardLinkData = linkData as GraveyardLinkData;
						if (graveyardLinkData != null)
						{
							return MapEditorSceneLinks.graveyardUserGeometryColor;
						}
						SanctuaryLinkData sanctuaryLinkData = linkData as SanctuaryLinkData;
						if (sanctuaryLinkData != null)
						{
							return MapEditorSceneLinks.sanctuaryUserGeometryColor;
						}
					}
				}
			}
			return MapEditorSceneLinks.defaultUserGeometryColor;
		}

		// Token: 0x0600155E RID: 5470 RVA: 0x0009B140 File Offset: 0x0009A140
		private void CreateUserGeomtry(IMapObject left, IMapObject right, bool createNewIfNeeded)
		{
			if (this.mapEditorScene.EditorScene != null && left != null && right != null)
			{
				int oldUserGeometryID = -1;
				bool userGeometryNotLocated = true;
				Dictionary<int, int> rightValue;
				if (this.doubleUserGeometryIDMap.TryGetValue(left.ID, out rightValue))
				{
					int _userGeometryID;
					if (rightValue.TryGetValue(right.ID, out _userGeometryID))
					{
						oldUserGeometryID = _userGeometryID;
						userGeometryNotLocated = false;
					}
				}
				else
				{
					rightValue = null;
				}
				Dictionary<int, int> leftValue = null;
				if (userGeometryNotLocated)
				{
					if (this.doubleUserGeometryIDMap.TryGetValue(right.ID, out leftValue))
					{
						int _userGeometryID2;
						if (leftValue.TryGetValue(left.ID, out _userGeometryID2))
						{
							oldUserGeometryID = _userGeometryID2;
						}
					}
					else
					{
						leftValue = null;
					}
				}
				int newUserGeometryID = oldUserGeometryID;
				if (left.Type.Type == MapObjectFactory.Type.RoutePoint && right.Type.Type == MapObjectFactory.Type.RoutePoint)
				{
					if (left.Select || right.Select || this.mapEditorScene.MapSceneParams.ShowRoutePointUserGeometry)
					{
						RoutePoint leftRoutePoint = left as RoutePoint;
						RoutePoint rightRoutePoint = right as RoutePoint;
						if (leftRoutePoint != null && rightRoutePoint != null)
						{
							Position leftPoint = leftRoutePoint.Position;
							Position rightPoint = rightRoutePoint.Position;
							Vec3 leftTangent = leftRoutePoint.Tangent;
							Vec3 rightTangent = rightRoutePoint.Tangent;
							if (RoutePoint.RouteAdvanceIsDirect(leftRoutePoint, rightRoutePoint))
							{
								newUserGeometryID = this.mapEditorScene.EditorScene.CreateUserGeometry_Spline(oldUserGeometryID, ref leftPoint, ref leftTangent, ref rightPoint, ref rightTangent, MapEditorSceneLinks.splineUserGeometryColor, leftRoutePoint.Select || rightRoutePoint.Select);
							}
							else
							{
								newUserGeometryID = this.mapEditorScene.EditorScene.CreateUserGeometry_Spline(oldUserGeometryID, ref rightPoint, ref rightTangent, ref leftPoint, ref leftTangent, MapEditorSceneLinks.splineUserGeometryColor, leftRoutePoint.Select || rightRoutePoint.Select);
							}
						}
					}
				}
				else if (left.Select || right.Select || this.mapEditorScene.MapSceneParams.ShowLinkUserGeometry)
				{
					Position start = left.Position;
					Position end = right.Position;
					Color _userGeometryColor = MapEditorSceneLinks.GetLinkColor(this.mapEditorScene.MapEditorMapObjectContainer, left, right);
					newUserGeometryID = this.mapEditorScene.EditorScene.CreateUserGeometry_Line(oldUserGeometryID, ref start, ref end, _userGeometryColor, left.Select || right.Select);
				}
				if (createNewIfNeeded || newUserGeometryID != oldUserGeometryID)
				{
					if (rightValue != null && leftValue == null)
					{
						if (rightValue.ContainsKey(right.ID))
						{
							rightValue[right.ID] = newUserGeometryID;
							return;
						}
						rightValue.Add(right.ID, newUserGeometryID);
						return;
					}
					else if (leftValue != null)
					{
						if (leftValue.ContainsKey(left.ID))
						{
							leftValue[left.ID] = newUserGeometryID;
							return;
						}
						leftValue.Add(left.ID, newUserGeometryID);
						return;
					}
					else
					{
						rightValue = new Dictionary<int, int>();
						rightValue.Add(right.ID, newUserGeometryID);
						this.doubleUserGeometryIDMap.Add(left.ID, rightValue);
					}
				}
			}
		}

		// Token: 0x0600155F RID: 5471 RVA: 0x0009B3EC File Offset: 0x0009A3EC
		private void DestroyUserGeomtry(IMapObject left, IMapObject right, bool deleteIfNeeded)
		{
			if (this.mapEditorScene.EditorScene != null && left != null && right != null)
			{
				int oldUserGeometryID = -1;
				bool userGeometryNotLocated = true;
				Dictionary<int, int> rightValue;
				int _userGeometryID;
				if (this.doubleUserGeometryIDMap.TryGetValue(left.ID, out rightValue) && rightValue.TryGetValue(right.ID, out _userGeometryID))
				{
					oldUserGeometryID = _userGeometryID;
					userGeometryNotLocated = false;
					if (deleteIfNeeded)
					{
						rightValue.Remove(right.ID);
						if (rightValue.Count == 0)
						{
							this.doubleUserGeometryIDMap.Remove(left.ID);
						}
					}
					else if (oldUserGeometryID != -1)
					{
						if (left.Type.Type == MapObjectFactory.Type.RoutePoint && right.Type.Type == MapObjectFactory.Type.RoutePoint)
						{
							if (!left.Select && !right.Select && !this.mapEditorScene.MapSceneParams.ShowRoutePointUserGeometry)
							{
								rightValue[right.ID] = -1;
							}
						}
						else if (!left.Select && !right.Select && !this.mapEditorScene.MapSceneParams.ShowLinkUserGeometry)
						{
							rightValue[right.ID] = -1;
						}
					}
				}
				Dictionary<int, int> leftValue;
				int _userGeometryID2;
				if (userGeometryNotLocated && this.doubleUserGeometryIDMap.TryGetValue(right.ID, out leftValue) && leftValue.TryGetValue(left.ID, out _userGeometryID2))
				{
					oldUserGeometryID = _userGeometryID2;
					if (deleteIfNeeded)
					{
						leftValue.Remove(left.ID);
						if (leftValue.Count == 0)
						{
							this.doubleUserGeometryIDMap.Remove(right.ID);
						}
					}
					else if (oldUserGeometryID != -1)
					{
						if (left.Type.Type == MapObjectFactory.Type.RoutePoint && right.Type.Type == MapObjectFactory.Type.RoutePoint)
						{
							if (!left.Select && !right.Select && !this.mapEditorScene.MapSceneParams.ShowRoutePointUserGeometry)
							{
								leftValue[left.ID] = -1;
							}
						}
						else if (!left.Select && !right.Select && !this.mapEditorScene.MapSceneParams.ShowLinkUserGeometry)
						{
							leftValue[left.ID] = -1;
						}
					}
				}
				if (oldUserGeometryID != -1)
				{
					if (left.Type.Type == MapObjectFactory.Type.RoutePoint && right.Type.Type == MapObjectFactory.Type.RoutePoint)
					{
						if (deleteIfNeeded || (!left.Select && !right.Select && !this.mapEditorScene.MapSceneParams.ShowRoutePointUserGeometry))
						{
							this.mapEditorScene.EditorScene.DeleteUserGeometry(oldUserGeometryID);
							return;
						}
					}
					else if (deleteIfNeeded || (!left.Select && !right.Select && !this.mapEditorScene.MapSceneParams.ShowLinkUserGeometry))
					{
						this.mapEditorScene.EditorScene.DeleteUserGeometry(oldUserGeometryID);
					}
				}
			}
		}

		// Token: 0x06001560 RID: 5472 RVA: 0x0009B6AC File Offset: 0x0009A6AC
		private void CreateUserGeometries()
		{
			foreach (KeyValuePair<int, Dictionary<int, int>> keyValuePair in this.doubleUserGeometryIDMap)
			{
				Dictionary<int, int> createdItems = new Dictionary<int, int>();
				foreach (KeyValuePair<int, int> _keyValuePair in keyValuePair.Value)
				{
					int userGeometryID = _keyValuePair.Value;
					if (userGeometryID == -1)
					{
						IMapObject left;
						this.mapEditorScene.MapEditorMapObjectContainer.TryGetMapObject(keyValuePair.Key, out left);
						IMapObject right;
						this.mapEditorScene.MapEditorMapObjectContainer.TryGetMapObject(_keyValuePair.Key, out right);
						if (left != null && right != null)
						{
							if (left.Type.Type == MapObjectFactory.Type.RoutePoint && right.Type.Type == MapObjectFactory.Type.RoutePoint)
							{
								if (this.mapEditorScene.MapSceneParams.ShowRoutePointUserGeometry)
								{
									RoutePoint leftRoutePoint = left as RoutePoint;
									RoutePoint rightRoutePoint = right as RoutePoint;
									if (leftRoutePoint != null && rightRoutePoint != null)
									{
										Position leftPoint = leftRoutePoint.Position;
										Position rightPoint = rightRoutePoint.Position;
										Vec3 leftTangent = leftRoutePoint.Tangent;
										Vec3 rightTangent = rightRoutePoint.Tangent;
										if (RoutePoint.RouteAdvanceIsDirect(leftRoutePoint, rightRoutePoint))
										{
											userGeometryID = this.mapEditorScene.EditorScene.CreateUserGeometry_Spline(-1, ref leftPoint, ref leftTangent, ref rightPoint, ref rightTangent, MapEditorSceneLinks.splineUserGeometryColor, leftRoutePoint.Select || rightRoutePoint.Select);
										}
										else
										{
											userGeometryID = this.mapEditorScene.EditorScene.CreateUserGeometry_Spline(-1, ref rightPoint, ref rightTangent, ref leftPoint, ref leftTangent, MapEditorSceneLinks.splineUserGeometryColor, leftRoutePoint.Select || rightRoutePoint.Select);
										}
									}
								}
							}
							else if (this.mapEditorScene.MapSceneParams.ShowLinkUserGeometry)
							{
								Position start = left.Position;
								Position end = right.Position;
								Color _userGeometryColor = MapEditorSceneLinks.GetLinkColor(this.mapEditorScene.MapEditorMapObjectContainer, left, right);
								userGeometryID = this.mapEditorScene.EditorScene.CreateUserGeometry_Line(-1, ref start, ref end, _userGeometryColor, left.Select || right.Select);
							}
							createdItems.Add(_keyValuePair.Key, userGeometryID);
						}
					}
				}
				foreach (KeyValuePair<int, int> createdItem in createdItems)
				{
					keyValuePair.Value[createdItem.Key] = createdItem.Value;
				}
			}
		}

		// Token: 0x06001561 RID: 5473 RVA: 0x0009B988 File Offset: 0x0009A988
		private void DestroyUserGeometries()
		{
			foreach (KeyValuePair<int, Dictionary<int, int>> keyValuePair in this.doubleUserGeometryIDMap)
			{
				List<int> deletedItems = new List<int>();
				foreach (KeyValuePair<int, int> _keyValuePair in keyValuePair.Value)
				{
					int userGeometryID = _keyValuePair.Value;
					if (userGeometryID != -1)
					{
						IMapObject left;
						this.mapEditorScene.MapEditorMapObjectContainer.TryGetMapObject(keyValuePair.Key, out left);
						IMapObject right;
						this.mapEditorScene.MapEditorMapObjectContainer.TryGetMapObject(_keyValuePair.Key, out right);
						if (left != null && right != null && !left.Select && !right.Select)
						{
							if (left.Type.Type == MapObjectFactory.Type.RoutePoint && right.Type.Type == MapObjectFactory.Type.RoutePoint)
							{
								if (!this.mapEditorScene.MapSceneParams.ShowRoutePointUserGeometry)
								{
									this.mapEditorScene.EditorScene.DeleteUserGeometry(userGeometryID);
									deletedItems.Add(_keyValuePair.Key);
								}
							}
							else if (!this.mapEditorScene.MapSceneParams.ShowLinkUserGeometry)
							{
								this.mapEditorScene.EditorScene.DeleteUserGeometry(userGeometryID);
								deletedItems.Add(_keyValuePair.Key);
							}
						}
					}
				}
				foreach (int deletedItem in deletedItems)
				{
					keyValuePair.Value[deletedItem] = -1;
				}
			}
		}

		// Token: 0x06001562 RID: 5474 RVA: 0x0009BB88 File Offset: 0x0009AB88
		private void OnShowLinkUserGeometryChanged(MapSceneParams mapSceneParams, ref bool oldValue, ref bool newValue)
		{
			if (this.mapEditorScene.MapSceneParams != null)
			{
				if (this.mapEditorScene.MapSceneParams.ShowLinkUserGeometry)
				{
					this.CreateUserGeometries();
					return;
				}
				this.DestroyUserGeometries();
			}
		}

		// Token: 0x06001563 RID: 5475 RVA: 0x0009BBB6 File Offset: 0x0009ABB6
		private void OnShowRoutePointUserGeometryChanged(MapSceneParams mapSceneParams, ref bool oldValue, ref bool newValue)
		{
			if (this.mapEditorScene.MapSceneParams != null)
			{
				if (this.mapEditorScene.MapSceneParams.ShowRoutePointUserGeometry)
				{
					this.CreateUserGeometries();
					return;
				}
				this.DestroyUserGeometries();
			}
		}

		// Token: 0x06001564 RID: 5476 RVA: 0x0009BBE4 File Offset: 0x0009ABE4
		public void OnPositionChanged(MapObjectContainer mapObjectContainer, IMapObject mapObject, ref Position oldValue, ref Position newValue)
		{
			Dictionary<IMapObject, ILinkData> links = this.mapEditorScene.MapEditorMapObjectContainer.GetLinks(mapObject);
			if (links != null && links.Count > 0)
			{
				foreach (KeyValuePair<IMapObject, ILinkData> keyValuePair in links)
				{
					this.CreateUserGeomtry(mapObject, keyValuePair.Key, false);
				}
			}
		}

		// Token: 0x06001565 RID: 5477 RVA: 0x0009BC58 File Offset: 0x0009AC58
		public void OnRotationChanged(MapObjectContainer mapObjectContainer, IMapObject mapObject, ref Rotation oldValue, ref Rotation newValue)
		{
			Dictionary<IMapObject, ILinkData> links = this.mapEditorScene.MapEditorMapObjectContainer.GetLinks(mapObject);
			if (links != null && links.Count > 0)
			{
				foreach (KeyValuePair<IMapObject, ILinkData> keyValuePair in links)
				{
					this.CreateUserGeomtry(mapObject, keyValuePair.Key, false);
				}
			}
		}

		// Token: 0x06001566 RID: 5478 RVA: 0x0009BCCC File Offset: 0x0009ACCC
		public void OnScaleChanged(MapObjectContainer mapObjectContainer, IMapObject mapObject, ref Scale oldValue, ref Scale newValue)
		{
			Dictionary<IMapObject, ILinkData> links = this.mapEditorScene.MapEditorMapObjectContainer.GetLinks(mapObject);
			if (links != null && links.Count > 0)
			{
				foreach (KeyValuePair<IMapObject, ILinkData> keyValuePair in links)
				{
					this.CreateUserGeomtry(mapObject, keyValuePair.Key, false);
				}
			}
		}

		// Token: 0x06001567 RID: 5479 RVA: 0x0009BD40 File Offset: 0x0009AD40
		public void OnSelectChanged(MapObjectContainer mapObjectContainer, IMapObject mapObject, ref bool oldValue, ref bool newValue)
		{
			Dictionary<IMapObject, ILinkData> links = this.mapEditorScene.MapEditorMapObjectContainer.GetLinks(mapObject);
			if (links != null && links.Count > 0)
			{
				foreach (KeyValuePair<IMapObject, ILinkData> keyValuePair in links)
				{
					if (mapObject.Type.Type == MapObjectFactory.Type.RoutePoint && keyValuePair.Key.Type.Type == MapObjectFactory.Type.RoutePoint)
					{
						if (newValue || this.mapEditorScene.MapSceneParams.ShowRoutePointUserGeometry)
						{
							this.CreateUserGeomtry(mapObject, keyValuePair.Key, false);
						}
						else
						{
							this.DestroyUserGeomtry(mapObject, keyValuePair.Key, false);
						}
					}
					else if (newValue || this.mapEditorScene.MapSceneParams.ShowLinkUserGeometry)
					{
						this.CreateUserGeomtry(mapObject, keyValuePair.Key, false);
					}
					else
					{
						this.DestroyUserGeomtry(mapObject, keyValuePair.Key, false);
					}
				}
			}
		}

		// Token: 0x06001568 RID: 5480 RVA: 0x0009BE54 File Offset: 0x0009AE54
		private void OnLinkAdded(MapObjectContainer mapObjectContainer, LinkContainer<IMapObject> _linkContainer, IMapObject left, IMapObject right, object data)
		{
			this.CreateUserGeomtry(left, right, true);
		}

		// Token: 0x06001569 RID: 5481 RVA: 0x0009BE60 File Offset: 0x0009AE60
		private void OnLinkRemoved(MapObjectContainer mapObjectContainer, LinkContainer<IMapObject> _linkContainer, IMapObject left, IMapObject right, object data)
		{
			this.DestroyUserGeomtry(left, right, true);
		}

		// Token: 0x0600156A RID: 5482 RVA: 0x0009BE6C File Offset: 0x0009AE6C
		private void OnPatrolLinkDataStartChanged(MapEditorMapObjectContainer _mapEditorMapObjectContainer, PatrolLinkData patrolLinkData, ref string oldValue, ref string newValue)
		{
			IMapObject left;
			IMapObject right;
			if (this.mapEditorScene.MapEditorMapObjectContainer.TryGetMapObjects(patrolLinkData, out left, out right) && left != null && right != null)
			{
				this.CreateUserGeomtry(left, right, false);
			}
		}

		// Token: 0x0600156B RID: 5483 RVA: 0x0009BEA0 File Offset: 0x0009AEA0
		private void OnPatrolLinkDataTypeChanged(MapEditorMapObjectContainer _mapEditorMapObjectContainer, PatrolLinkData patrolLinkData, ref PatrolNodeLinkType oldValue, ref PatrolNodeLinkType newValue)
		{
			IMapObject left;
			IMapObject right;
			if (this.mapEditorScene.MapEditorMapObjectContainer.TryGetMapObjects(patrolLinkData, out left, out right) && left != null && right != null)
			{
				this.CreateUserGeomtry(left, right, false);
			}
		}

		// Token: 0x0600156C RID: 5484 RVA: 0x0009BED4 File Offset: 0x0009AED4
		public void Bind(MapEditorScene _mapEditorScene)
		{
			this.mapEditorScene = _mapEditorScene;
			if (this.mapEditorScene != null)
			{
				this.mapEditorScene.MapSceneParams.ShowLinkUserGeometryChanged += this.OnShowLinkUserGeometryChanged;
				this.mapEditorScene.MapSceneParams.ShowRoutePointUserGeometryChanged += this.OnShowRoutePointUserGeometryChanged;
				if (this.mapEditorScene.MapEditorMapObjectContainer != null)
				{
					this.mapEditorScene.MapEditorMapObjectContainer.LinkAdded += new MapObjectContainer.LinkEvent(this.OnLinkAdded);
					this.mapEditorScene.MapEditorMapObjectContainer.LinkRemoved += new MapObjectContainer.LinkEvent(this.OnLinkRemoved);
					PatrolNodeContainer patrolNodeContainer = this.mapEditorScene.MapEditorMapObjectContainer.PatrolNodeContainer;
					if (patrolNodeContainer != null)
					{
						patrolNodeContainer.PatrolLinkDataStartChanged += this.OnPatrolLinkDataStartChanged;
						patrolNodeContainer.PatrolLinkDataTypeChanged += this.OnPatrolLinkDataTypeChanged;
					}
					this.CreateUserGeometries();
				}
			}
		}

		// Token: 0x0600156D RID: 5485 RVA: 0x0009BFB0 File Offset: 0x0009AFB0
		public void Unbind()
		{
			if (this.mapEditorScene != null)
			{
				this.DestroyUserGeometries();
				this.mapEditorScene.MapSceneParams.ShowLinkUserGeometryChanged -= this.OnShowLinkUserGeometryChanged;
				this.mapEditorScene.MapSceneParams.ShowRoutePointUserGeometryChanged -= this.OnShowRoutePointUserGeometryChanged;
				if (this.mapEditorScene.MapEditorMapObjectContainer != null)
				{
					this.mapEditorScene.MapEditorMapObjectContainer.LinkAdded -= new MapObjectContainer.LinkEvent(this.OnLinkAdded);
					this.mapEditorScene.MapEditorMapObjectContainer.LinkRemoved -= new MapObjectContainer.LinkEvent(this.OnLinkRemoved);
					PatrolNodeContainer patrolNodeContainer = this.mapEditorScene.MapEditorMapObjectContainer.PatrolNodeContainer;
					if (patrolNodeContainer != null)
					{
						patrolNodeContainer.PatrolLinkDataStartChanged -= this.OnPatrolLinkDataStartChanged;
						patrolNodeContainer.PatrolLinkDataTypeChanged -= this.OnPatrolLinkDataTypeChanged;
					}
				}
				this.mapEditorScene = null;
			}
		}

		// Token: 0x04000F14 RID: 3860
		private static readonly Color defaultUserGeometryColor = Color.FromArgb(MapEditorScene.DefaultTransparentColorAlpha, Color.White);

		// Token: 0x04000F15 RID: 3861
		private static readonly Color walkUserGeometryColor = Color.FromArgb(MapEditorScene.DefaultTransparentColorAlpha, Color.White);

		// Token: 0x04000F16 RID: 3862
		private static readonly Color flyUserGeometryColor = Color.FromArgb(MapEditorScene.DefaultTransparentColorAlpha, Color.LightSkyBlue);

		// Token: 0x04000F17 RID: 3863
		private static readonly Color teleportUserGeometryColor = Color.FromArgb(MapEditorScene.DefaultTransparentColorAlpha, Color.LightGreen);

		// Token: 0x04000F18 RID: 3864
		private static readonly Color splineUserGeometryColor = Color.FromArgb(MapEditorScene.DefaultTransparentColorAlpha, Color.White);

		// Token: 0x04000F19 RID: 3865
		private static readonly Color directionalUserGeometryColor = Color.FromArgb(MapEditorScene.DefaultTransparentColorAlpha, Color.LightCoral);

		// Token: 0x04000F1A RID: 3866
		private static readonly Color graveyardUserGeometryColor = Color.FromArgb(MapEditorScene.DefaultTransparentColorAlpha, Color.DeepPink);

		// Token: 0x04000F1B RID: 3867
		private static readonly Color sanctuaryUserGeometryColor = Color.FromArgb(MapEditorScene.DefaultTransparentColorAlpha, Color.DeepSkyBlue);

		// Token: 0x04000F1C RID: 3868
		private readonly Dictionary<int, Dictionary<int, int>> doubleUserGeometryIDMap = new Dictionary<int, Dictionary<int, int>>();

		// Token: 0x04000F1D RID: 3869
		private MapEditorScene mapEditorScene;
	}
}
