using System;
using System.Collections.Generic;
using Db;
using Db.Main;
using MapEditor.Map.Containers;
using MapEditor.Map.MapObjects;
using MapEditor.Resources.Strings;
using Tools.Geometry;
using Tools.LinkContainer;
using Tools.MapObjects;
using Tools.Progress;
using Tools.SafeObjMan;

namespace MapEditor.Map.SaveLoad.DataSources
{
	// Token: 0x020002BD RID: 701
	internal class RoutePointsDataSource : SaveLoad.IDataSource
	{
		// Token: 0x060020B3 RID: 8371 RVA: 0x000CFE88 File Offset: 0x000CEE88
		public static void GetTours(Dictionary<string, List<RouteData>> tours, MapEditorMapObjectContainer mapEditorMapObjectContainer)
		{
			if (tours != null && mapEditorMapObjectContainer != null)
			{
				Dictionary<RoutePoint, int> usedRoutePoints = new Dictionary<RoutePoint, int>();
				foreach (KeyValuePair<int, IMapObject> keyValuePair in mapEditorMapObjectContainer.RoutePointContainer.MapObjects)
				{
					RoutePoint routePoint = keyValuePair.Value as RoutePoint;
					if (routePoint != null && !routePoint.Temporary && routePoint.RoutePointType == RoutePointType.Simple && !usedRoutePoints.ContainsKey(routePoint))
					{
						string tour = routePoint.Route;
						if (tour == null)
						{
							tour = string.Empty;
						}
						List<RouteData> _tours;
						if (!tours.TryGetValue(tour, out _tours))
						{
							_tours = new List<RouteData>();
							tours.Add(tour, _tours);
						}
						RouteData routeData = new RouteData();
						routeData.Route = tour;
						RoutePointsDataSource.AddRoutePointToRoute(mapEditorMapObjectContainer, routeData, routePoint, null, usedRoutePoints);
						_tours.Add(routeData);
					}
				}
			}
		}

		// Token: 0x060020B4 RID: 8372 RVA: 0x000CFF6C File Offset: 0x000CEF6C
		private static void AddRoutePointToRoute(MapObjectContainer mapObjectContainer, RouteData routeData, RoutePoint routePoint, RoutePoint fromRoutePoint, Dictionary<RoutePoint, int> usedRoutePoints)
		{
			if (usedRoutePoints.ContainsKey(routePoint))
			{
				routeData.Circle = true;
				return;
			}
			usedRoutePoints.Add(routePoint, 0);
			int count = routeData.RoutePoints.Count;
			int insertIndex = count;
			for (int index = 0; index < count; index++)
			{
				if (routeData.RoutePoints[index].RouteAdvance > routePoint.RouteAdvance)
				{
					insertIndex = index;
					break;
				}
			}
			routeData.RoutePoints.Insert(insertIndex, routePoint);
			if (routePoint.RouteLinks > 0)
			{
				Dictionary<IMapObject, ILinkData> links = mapObjectContainer.GetLinks(routePoint);
				if (links != null && links.Count > 0)
				{
					int linksCount = 0;
					foreach (KeyValuePair<IMapObject, ILinkData> keyValuePair in links)
					{
						if (!keyValuePair.Key.Temporary && keyValuePair.Key.Type.Type == MapObjectFactory.Type.RoutePoint)
						{
							linksCount++;
						}
					}
					if (linksCount > 0)
					{
						RoutePoint firstLinkedRoutePoint = null;
						RoutePoint secondLinkedRoutePoint = null;
						foreach (KeyValuePair<IMapObject, ILinkData> leyValuePair in links)
						{
							IMapObject linkedMapObject = leyValuePair.Key;
							if (linkedMapObject != null && !linkedMapObject.Temporary)
							{
								RoutePoint linkedRoutePoint = linkedMapObject as RoutePoint;
								if (linkedRoutePoint != null)
								{
									if (firstLinkedRoutePoint != null)
									{
										secondLinkedRoutePoint = linkedRoutePoint;
										break;
									}
									firstLinkedRoutePoint = linkedRoutePoint;
								}
							}
						}
						if (firstLinkedRoutePoint != null && firstLinkedRoutePoint != fromRoutePoint)
						{
							RoutePointsDataSource.AddRoutePointToRoute(mapObjectContainer, routeData, firstLinkedRoutePoint, routePoint, usedRoutePoints);
						}
						if (secondLinkedRoutePoint != null && secondLinkedRoutePoint != fromRoutePoint)
						{
							RoutePointsDataSource.AddRoutePointToRoute(mapObjectContainer, routeData, secondLinkedRoutePoint, routePoint, usedRoutePoints);
						}
					}
				}
			}
		}

		// Token: 0x060020B5 RID: 8373 RVA: 0x000D0110 File Offset: 0x000CF110
		public static void CreateRoute(MapEditorMap map, RouteSaveData routeSaveData, List<RoutePoint> addedRoutePoints, ILinkData linkData, bool smartLinkDirection)
		{
			if (map != null && routeSaveData != null)
			{
				int count = routeSaveData.RoutePoints.Count;
				if (count > 0)
				{
					bool direct = true;
					if (smartLinkDirection && count > 1)
					{
						direct = (Vec3.Dot(routeSaveData.RoutePoints[0].Tangent, (routeSaveData.RoutePoints[1].Position - routeSaveData.RoutePoints[0].Position).Vec3) > 0.0);
					}
					List<RoutePoint> routePoints = new List<RoutePoint>();
					for (int index = 0; index < count; index++)
					{
						int newMapObjectID = map.MapEditorMapObjectContainer.AddMapObject(new MapObjectType(MapObjectFactory.Type.RoutePoint, routeSaveData.Route), false, routeSaveData.RoutePoints[index].Position, routeSaveData.RoutePoints[index].Rotation, routeSaveData.RoutePoints[index].Scale);
						IMapObject mapObject;
						if (map.MapEditorMapObjectContainer.TryGetMapObject(newMapObjectID, out mapObject))
						{
							RoutePoint routePoint = mapObject as RoutePoint;
							if (routePoint != null)
							{
								routePoint.RoutePointType = routeSaveData.RoutePointType;
								routePoint.Speed = routeSaveData.RoutePoints[index].Speed;
								routePoint.Latency = routeSaveData.RoutePoints[index].Latency;
								routePoint.RouteAdvance = (double)(direct ? index : (count - (index + 1)));
								routePoints.Add(routePoint);
								if (addedRoutePoints != null)
								{
									addedRoutePoints.Add(routePoint);
								}
							}
						}
					}
					bool previousRoutePointAdvance = RoutePointContainer.AllowRouteAdvance;
					RoutePointContainer.AllowRouteAdvance = false;
					for (int index2 = 0; index2 < routePoints.Count; index2++)
					{
						if (index2 < routePoints.Count - 1)
						{
							map.MapEditorMapObjectContainer.AddLink(routePoints[index2], routePoints[index2 + 1], (linkData != null) ? linkData.Clone() : null);
						}
						else if (routeSaveData.Circle)
						{
							map.MapEditorMapObjectContainer.AddLink(routePoints[index2], routePoints[0], (linkData != null) ? linkData.Clone() : null);
						}
					}
					RoutePointContainer.AllowRouteAdvance = previousRoutePointAdvance;
				}
			}
		}

		// Token: 0x060020B6 RID: 8374 RVA: 0x000D0314 File Offset: 0x000CF314
		public static RouteSaveData LoadRouteSaveData(IObjMan routeMan, Vec3 minMapPosition, Vec3 maxMapPosition, out bool inside)
		{
			inside = false;
			if (routeMan != null)
			{
				int count = SafeObjMan.GetInt(routeMan, "points");
				if (count > 0)
				{
					RouteSaveData routeSaveData = new RouteSaveData();
					bool isClosed = SafeObjMan.GetBool(routeMan, "isClosed");
					routeSaveData.Circle = isClosed;
					for (int index = 0; index < count; index++)
					{
						string routePointPropertyName = string.Format("points.[{0}].", index);
						RouteSaveData.RoutePointSaveData routePointSaveData = new RouteSaveData.RoutePointSaveData();
						routePointSaveData.Position = SafeObjMan.GetPosition(routeMan, routePointPropertyName + "coord");
						routePointSaveData.Tangent = SafeObjMan.GetVec3(routeMan, routePointPropertyName + "tangent");
						routePointSaveData.Speed = SafeObjMan.GetDouble(routeMan, routePointPropertyName + "speed");
						routePointSaveData.Latency = SafeObjMan.GetDouble(routeMan, routePointPropertyName + "latency");
						routeSaveData.RoutePoints.Add(routePointSaveData);
						if (minMapPosition.X <= routePointSaveData.Position.X && routePointSaveData.Position.X <= maxMapPosition.X && minMapPosition.Y <= routePointSaveData.Position.Y && routePointSaveData.Position.Y <= maxMapPosition.Y)
						{
							inside = true;
						}
					}
					return routeSaveData;
				}
			}
			return null;
		}

		// Token: 0x060020B7 RID: 8375 RVA: 0x000D045C File Offset: 0x000CF45C
		public static void SaveRoute(IObjMan routeMan, List<RoutePoint> routePoints, bool isClosed)
		{
			SafeObjMan.SetInt(routeMan, "points", 0);
			int count = routePoints.Count;
			if (count > 0)
			{
				Vec3 previousPosition = Vec3.Empty;
				Vec3 previousTangent = Vec3.Empty;
				for (int index = 0; index <= count; index++)
				{
					Vec3 currectPosition;
					Vec3 currentTangent;
					if (index < count)
					{
						routeMan.Insert("points", index);
						string routePointPropertyName = string.Format("points.[{0}].", index);
						RoutePoint routePoint = routePoints[index];
						SafeObjMan.SetPosition(routeMan, routePointPropertyName + "coord", routePoint.Position);
						SafeObjMan.SetVec3(routeMan, routePointPropertyName + "tangent", routePoint.Tangent);
						SafeObjMan.SetDouble(routeMan, routePointPropertyName + "speed", routePoint.Speed);
						SafeObjMan.SetDouble(routeMan, routePointPropertyName + "latency", routePoint.Latency);
						currectPosition = routePoint.Position.Vec3;
						currentTangent = routePoint.Tangent;
					}
					else
					{
						currectPosition = routePoints[0].Position.Vec3;
						currentTangent = routePoints[0].Tangent;
					}
					if (index > 0)
					{
						double length = 0.0;
						if (isClosed || index < count)
						{
							length = SplinePath.GetSplineLength(ref previousPosition, ref previousTangent, ref currectPosition, ref currentTangent, false);
						}
						SafeObjMan.SetDouble(routeMan, string.Format("points.[{0}].length", index - 1), length);
					}
					previousPosition = currectPosition;
					previousTangent = currentTangent;
				}
				SafeObjMan.SetBool(routeMan, "isClosed", isClosed);
				return;
			}
			SafeObjMan.SetBool(routeMan, "isClosed", false);
		}

		// Token: 0x060020B8 RID: 8376 RVA: 0x000D05D8 File Offset: 0x000CF5D8
		private static void LoadRoutes(MapEditorMap map, ICollection<RouteSaveData> routes)
		{
			if (map != null && routes != null && routes.Count > 0)
			{
				foreach (RouteSaveData routeSaveData in routes)
				{
					RoutePointsDataSource.CreateRoute(map, routeSaveData, null, null, false);
				}
			}
		}

		// Token: 0x060020B9 RID: 8377 RVA: 0x000D0634 File Offset: 0x000CF634
		public int GetProgressSteps(bool forSave)
		{
			if (forSave)
			{
				return 1;
			}
			return 1;
		}

		// Token: 0x060020BA RID: 8378 RVA: 0x000D063C File Offset: 0x000CF63C
		public bool Save(MapEditorMap map, MainForm.Context context, IProgressContainer progressContainer)
		{
			if (progressContainer != null)
			{
				progressContainer.AddString(Strings.SAVING_ROUTE_POINTS);
			}
			IDatabase mainDb = IDatabase.GetMainDatabase();
			if (mainDb != null)
			{
				List<string> newUsedTours = new List<string>();
				Dictionary<string, List<RouteData>> tours = new Dictionary<string, List<RouteData>>();
				RoutePointsDataSource.GetTours(tours, map.MapEditorMapObjectContainer);
				ObjMan.StartMassEditing();
				foreach (KeyValuePair<string, List<RouteData>> keyValuePair in tours)
				{
					string tour = keyValuePair.Key;
					if (!string.IsNullOrEmpty(tour))
					{
						DBID tourDBID = mainDb.GetDBIDByName(tour);
						IObjMan tourMan = mainDb.GetManipulator(tourDBID);
						if (tourMan != null)
						{
							string continentFolder = Constants.ContinentFolder(map.Data.ContinentName);
							string route = tour.Substring(continentFolder.Length + RoutePoint.TourFolder.Length);
							route = Str.CutFilePoint(route);
							route = continentFolder + RoutePoint.RouteFolder + route + RoutePoint.RouteDBExtention;
							DBID routeDBID = mainDb.GetDBIDByName(route);
							IObjMan routeMan;
							if (routeDBID.IsEmpty())
							{
								routeDBID = IDatabase.CreateDBIDByName(route);
								routeMan = mainDb.CreateNewObject(RoutePoint.RouteDBType);
								if (routeMan != null)
								{
									mainDb.AddNewObject(routeDBID, routeMan);
								}
								SafeObjMan.SetDBID(tourMan, "route", route);
							}
							else
							{
								routeMan = mainDb.GetManipulator(routeDBID);
							}
							if (routeMan != null)
							{
								SafeObjMan.SetDBID(tourMan, "route", route);
								List<RouteData> _routes = keyValuePair.Value;
								int count = _routes.Count;
								if (count > 0)
								{
									int maxCountIndex = 0;
									int maxCount = _routes[maxCountIndex].RoutePoints.Count;
									for (int index = 0; index < count; index++)
									{
										int _count = _routes[index].RoutePoints.Count;
										if (_count > maxCount)
										{
											maxCountIndex = index;
											maxCount = _count;
										}
									}
									if (maxCount > 0)
									{
										RouteData routeData = _routes[maxCountIndex];
										_routes.RemoveAt(maxCountIndex);
										RoutePointsDataSource.SaveRoute(routeMan, routeData.RoutePoints, routeData.Circle);
									}
								}
							}
							newUsedTours.Add(tour);
							this.usedTours.Remove(tour);
						}
					}
				}
				foreach (string tour2 in this.usedTours)
				{
					DBID tourDBID2 = mainDb.GetDBIDByName(tour2);
					mainDb.RemoveObject(tourDBID2);
				}
				ObjMan.StopMassEditing();
				this.usedTours.Clear();
				this.usedTours.AddRange(newUsedTours);
			}
			if (progressContainer != null)
			{
				progressContainer.Progress++;
			}
			return true;
		}

		// Token: 0x060020BB RID: 8379 RVA: 0x000D08E0 File Offset: 0x000CF8E0
		public bool Load(MapEditorMap map, MainForm.Context context, IProgressContainer progressContainer, out bool somethingCreated)
		{
			Vec3 minMapPosition = new Vec3((double)(map.Data.MinXMinYPatchCoords.X * Constants.PatchSize), (double)(map.Data.MinXMinYPatchCoords.Y * Constants.PatchSize), 0.0);
			Vec3 maxMapPosition = new Vec3((double)((map.Data.MinXMinYPatchCoords.X + map.Data.MapSize.X) * Constants.PatchSize), (double)((map.Data.MinXMinYPatchCoords.Y + map.Data.MapSize.Y) * Constants.PatchSize), 0.0);
			somethingCreated = false;
			if (progressContainer != null)
			{
				progressContainer.AddString(Strings.LOADING_ROUTE_POINTS);
			}
			this.usedTours.Clear();
			List<RouteSaveData> routes = new List<RouteSaveData>();
			IDatabase mainDb = IDatabase.GetMainDatabase();
			if (mainDb != null)
			{
				DBItemSource tourSource = new DBItemSource(EditorEnvironment.DataFolder + Constants.ContinentFolder(map.Data.ContinentName) + RoutePoint.TourFolder, RoutePoint.TourDBType, false, true);
				IEnumerable<string> tours = tourSource.Items;
				foreach (string tour in tours)
				{
					DBID tourDBID = mainDb.GetDBIDByName(tour);
					IObjMan tourMan = mainDb.GetManipulator(tourDBID);
					if (tourMan != null)
					{
						string route = SafeObjMan.GetDBID(tourMan, "route");
						DBID routeDBID = mainDb.GetDBIDByName(route);
						IObjMan routeMan = mainDb.GetManipulator(routeDBID);
						bool inside;
						RouteSaveData routeSaveData = RoutePointsDataSource.LoadRouteSaveData(routeMan, minMapPosition, maxMapPosition, out inside);
						if (routeSaveData != null)
						{
							routeSaveData.RoutePointType = RoutePointType.Simple;
							routeSaveData.Route = tour;
							if (inside)
							{
								routes.Add(routeSaveData);
								this.usedTours.Add(tour);
							}
						}
					}
				}
			}
			RoutePointsDataSource.LoadRoutes(map, routes);
			if (progressContainer != null)
			{
				progressContainer.Progress++;
			}
			return true;
		}

		// Token: 0x040013FA RID: 5114
		private readonly List<string> usedTours = new List<string>();
	}
}
