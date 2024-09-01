using System;
using System.Collections.Generic;
using Db;
using Db.Main;
using MapEditor.Map.MapObjectElements;
using MapEditor.Map.MapObjects;
using MapEditor.Resources.Strings;
using Tools.Geometry;
using Tools.LinkContainer;
using Tools.MapObjects;
using Tools.Progress;
using Tools.SafeObjMan;

namespace MapEditor.Map.SaveLoad.DataSources
{
	// Token: 0x020000D0 RID: 208
	internal class SpawnPointsDataSource : SaveLoad.IDataSource
	{
		// Token: 0x17000226 RID: 550
		// (get) Token: 0x06000AA8 RID: 2728 RVA: 0x0005644A File Offset: 0x0005544A
		public static string FileDBType
		{
			get
			{
				return SpawnPointsDataSource.fileDBType;
			}
		}

		// Token: 0x06000AA9 RID: 2729 RVA: 0x00056454 File Offset: 0x00055454
		private static bool ApplyToAllPatches(MapEditorMap map, MainForm.Context context, SpawnPointsDataSource.PatchMethod patchMethod, IProgressContainer progressContainer)
		{
			bool result = true;
			for (int x = 0; x < map.Data.MapSize.X; x++)
			{
				for (int y = 0; y < map.Data.MapSize.Y; y++)
				{
					if (!patchMethod(map, context, new Point(map.Data.MinXMinYPatchCoords.X + x, map.Data.MinXMinYPatchCoords.Y + y)))
					{
						result = false;
					}
					if (progressContainer != null)
					{
						progressContainer.Progress++;
					}
				}
			}
			return result;
		}

		// Token: 0x06000AAA RID: 2730 RVA: 0x000564F4 File Offset: 0x000554F4
		private static void LoadPatrolRoute(MapEditorMap map, IDatabase mainDb, string patrolRouteName, List<RoutePoint> patrolRoute, ILinkData linkData, out bool isClosed)
		{
			isClosed = false;
			if (patrolRoute != null)
			{
				patrolRoute.Clear();
			}
			if (map != null && mainDb != null)
			{
				Vec3 minMapPosition = new Vec3((double)(map.Data.MinXMinYPatchCoords.X * Constants.PatchSize), (double)(map.Data.MinXMinYPatchCoords.Y * Constants.PatchSize), 0.0);
				Vec3 maxMapPosition = new Vec3((double)((map.Data.MinXMinYPatchCoords.X + map.Data.MapSize.X) * Constants.PatchSize), (double)((map.Data.MinXMinYPatchCoords.Y + map.Data.MapSize.Y) * Constants.PatchSize), 0.0);
				DBID patrolRouteDBID = mainDb.GetDBIDByName(patrolRouteName);
				IObjMan patrolRouteMan = mainDb.GetManipulator(patrolRouteDBID);
				bool inside;
				RouteSaveData routeSaveData = RoutePointsDataSource.LoadRouteSaveData(patrolRouteMan, minMapPosition, maxMapPosition, out inside);
				if (routeSaveData != null)
				{
					routeSaveData.RoutePointType = RoutePointType.PatrolNode;
					routeSaveData.Route = patrolRouteName;
					RoutePointsDataSource.CreateRoute(map, routeSaveData, patrolRoute, linkData, true);
				}
			}
		}

		// Token: 0x06000AAB RID: 2731 RVA: 0x0005660C File Offset: 0x0005560C
		public SpawnPointsDataSource(MapEditorMap map)
		{
			this.mapSize = map.Data.MapSize;
		}

		// Token: 0x06000AAC RID: 2732 RVA: 0x00056630 File Offset: 0x00055630
		public static SpawnPoint LoadSpawnPoint(MapEditorMap map, IDatabase mainDb, IObjMan objectMan, string placePropertyName, SpawnTableType spawnTableType, string spawnTable, string scriptID, double scanRadius, double additionalRatio, ref Position additionalPosition)
		{
			IObjMan placeMan = objectMan.CreateManipulator(placePropertyName);
			if (placeMan != null)
			{
				SpawnPointType spawnPointType = SpawnPointType.Undefined;
				if (objectMan.IsStructPtrInstanceCompatible(placePropertyName, "gameMechanics.map.spawn.SpawnPlacePoint"))
				{
					spawnPointType = SpawnPointType.Guard;
				}
				else if (objectMan.IsStructPtrInstanceCompatible(placePropertyName, "gameMechanics.map.spawn.SpawnPlaceRoamingArea"))
				{
					spawnPointType = SpawnPointType.Circle;
					if (placeMan.IsStructPtrInstanceCompatible("roamingArea", "gameMechanics.map.spawn.random2D.CircleRoamingArea"))
					{
						spawnPointType = SpawnPointType.Circle;
					}
					else if (placeMan.IsStructPtrInstanceCompatible("roamingArea", "gameMechanics.map.spawn.random2D.EllipseRoamingArea"))
					{
						spawnPointType = SpawnPointType.Ellipse;
					}
					else if (placeMan.IsStructPtrInstanceCompatible("roamingArea", "gameMechanics.map.spawn.random2D.ConvexPolygonRoamingArea"))
					{
					}
				}
				else if (objectMan.IsStructPtrInstanceCompatible(placePropertyName, "gameMechanics.map.spawn.patrol.SpawnPlacePatrol"))
				{
					spawnPointType = SpawnPointType.Patrol;
				}
				else if (objectMan.IsStructPtrInstanceCompatible(placePropertyName, "gameMechanics.map.spawn.SpawnPlaceCircle"))
				{
					spawnPointType = SpawnPointType.SpawnCircle;
				}
				if (spawnPointType != SpawnPointType.Undefined)
				{
					MapObjectCreationInfo info = new MapObjectCreationInfo();
					List<IMapObject> locatedMapObjects = null;
					if (spawnPointType == SpawnPointType.Guard || spawnPointType == SpawnPointType.Circle || spawnPointType == SpawnPointType.Ellipse || spawnPointType == SpawnPointType.SpawnCircle)
					{
						info.Position = SafeObjMan.GetPosition(placeMan, "center") * additionalRatio + additionalPosition;
						if (spawnPointType == SpawnPointType.Guard)
						{
							info.Rotation = new Rotation(SafeObjMan.GetFloat(placeMan, "yaw"), SafeObjMan.GetFloat(placeMan, "pitch"), SafeObjMan.GetFloat(placeMan, "roll"));
						}
						else
						{
							info.Rotation = new Rotation(SafeObjMan.GetFloat(placeMan, "yaw"), Rotation.Empty.Pitch, Rotation.Empty.Roll);
						}
					}
					else if (spawnPointType == SpawnPointType.Patrol)
					{
						info.Position = SafeObjMan.GetPosition(placeMan, "points.[0].coords") * additionalRatio + additionalPosition;
						locatedMapObjects = map.MapEditorMapObjectContainer.Locate(info.Position);
					}
					info.GroupName = SafeObjMan.GetString(placeMan, "groupName");
					string spawnID = SafeObjMan.GetString(placeMan, "spawnId");
					string spawnTuner = SafeObjMan.GetDBID(placeMan, "tuner");
					int spawnPointID = map.MapEditorMapObjectContainer.AddMapObject(new MapObjectType(MapObjectFactory.Type.SpawnPoint, spawnTable), false, info);
					IMapObject mapObject;
					if (map.MapEditorMapObjectContainer.MapObjects.TryGetValue(spawnPointID, out mapObject))
					{
						SpawnPoint spawnPoint = mapObject as SpawnPoint;
						if (spawnPoint != null)
						{
							spawnPoint.ScriptID = scriptID;
							spawnPoint.ScanRadius = scanRadius;
							spawnPoint.SpawnPointType = spawnPointType;
							if (spawnTableType != SpawnTableType.Undefined)
							{
								spawnPoint.SetSpawnTableType(spawnTableType);
							}
							spawnPoint.SpawnID = spawnID;
							spawnPoint.SpawnTuner = spawnTuner;
							if (spawnPointType != SpawnPointType.Guard)
							{
								if (spawnPointType == SpawnPointType.Circle)
								{
									spawnPoint.CachedNormal = SafeObjMan.GetVec3(placeMan, "normal");
									IObjMan roamingAreaMan = placeMan.CreateManipulator("roamingArea");
									if (roamingAreaMan != null)
									{
										double radius = SafeObjMan.GetDouble(roamingAreaMan, "radius");
										if (radius > MathConsts.DOUBLE_EPSILON)
										{
											CircleSpawnPointData circleSpawnPointData = spawnPoint.SpawnPointData as CircleSpawnPointData;
											if (circleSpawnPointData != null)
											{
												circleSpawnPointData.Radius = radius;
											}
										}
									}
								}
								else if (spawnPointType == SpawnPointType.Ellipse)
								{
									spawnPoint.CachedNormal = SafeObjMan.GetVec3(placeMan, "normal");
									IObjMan roamingAreaMan2 = placeMan.CreateManipulator("roamingArea");
									if (roamingAreaMan2 != null)
									{
										double semiaxisA = SafeObjMan.GetDouble(roamingAreaMan2, "semiaxisA");
										double semiaxisB = SafeObjMan.GetDouble(roamingAreaMan2, "semiaxisB");
										float yaw = SafeObjMan.GetFloat(roamingAreaMan2, "yaw");
										if (semiaxisA > MathConsts.DOUBLE_EPSILON && semiaxisB > MathConsts.DOUBLE_EPSILON)
										{
											EllipseSpawnPointData ellipseSpawnPointData = spawnPoint.SpawnPointData as EllipseSpawnPointData;
											if (ellipseSpawnPointData != null)
											{
												ellipseSpawnPointData.SemiaxisA = semiaxisA;
												ellipseSpawnPointData.SemiaxisB = semiaxisB;
											}
										}
										if (yaw != spawnPoint.Rotation.Yaw)
										{
											spawnPoint.Rotation = new Rotation(yaw, spawnPoint.Rotation.Pitch, spawnPoint.Rotation.Roll);
										}
									}
								}
								else if (spawnPointType == SpawnPointType.SpawnCircle)
								{
									double radius2 = SafeObjMan.GetDouble(placeMan, "radius");
									int multiplicity = SafeObjMan.GetInt(placeMan, "multiplicity");
									if (radius2 > MathConsts.DOUBLE_EPSILON && multiplicity >= SpawnCircleSpawnPointData.DefaultMultiplicity)
									{
										SpawnCircleSpawnPointData spawnCircleSpawnPointData = spawnPoint.SpawnPointData as SpawnCircleSpawnPointData;
										if (spawnCircleSpawnPointData != null)
										{
											spawnCircleSpawnPointData.Radius = radius2;
											spawnCircleSpawnPointData.Multiplicity = multiplicity;
										}
									}
								}
								else if (spawnPointType == SpawnPointType.Patrol)
								{
									PatrolSpawnPointData patrolSpawnPointData = spawnPoint.SpawnPointData as PatrolSpawnPointData;
									if (patrolSpawnPointData != null)
									{
										IObjMan pointMan = placeMan.CreateManipulator("points.[0]");
										if (pointMan != null)
										{
											patrolSpawnPointData.Label = SafeObjMan.GetString(pointMan, "label");
											patrolSpawnPointData.Script = SafeObjMan.GetDBID(pointMan, "script");
										}
									}
									IMapObject alreadyExistingPatrolNode = null;
									if (locatedMapObjects != null)
									{
										foreach (IMapObject locatedMapObject in locatedMapObjects)
										{
											if (locatedMapObject.Type.Type == MapObjectFactory.Type.PatrolNode)
											{
												PatrolNode patrolNode = locatedMapObject as PatrolNode;
												if (patrolNode != null)
												{
													alreadyExistingPatrolNode = locatedMapObject;
													break;
												}
											}
											else if (locatedMapObject.Type.Type == MapObjectFactory.Type.RoutePoint)
											{
												RoutePoint routePoint = locatedMapObject as RoutePoint;
												if (routePoint != null && routePoint.RoutePointType == RoutePointType.PatrolNode)
												{
													alreadyExistingPatrolNode = locatedMapObject;
													break;
												}
											}
										}
									}
									if (alreadyExistingPatrolNode != null)
									{
										map.MapEditorMapObjectContainer.CopyLinks(spawnPoint, alreadyExistingPatrolNode);
										map.MapEditorMapObjectContainer.RemoveMapObject(alreadyExistingPatrolNode);
									}
									else
									{
										List<IMapObject> patrolNodes = new List<IMapObject>();
										patrolNodes.Add(spawnPoint);
										int pointsCount = SafeObjMan.GetInt(placeMan, "points");
										for (int pointIndex = 1; pointIndex < pointsCount; pointIndex++)
										{
											string pointPropertyName = string.Format("points.[{0}]", pointIndex);
											IObjMan pointMan2 = placeMan.CreateManipulator(pointPropertyName);
											if (pointMan2 != null)
											{
												MapObjectCreationInfo patrolNodeInfo = new MapObjectCreationInfo();
												patrolNodeInfo.Position = SafeObjMan.GetPosition(pointMan2, "coords") * additionalRatio + additionalPosition;
												int patrolNodeID = map.MapEditorMapObjectContainer.AddMapObject(new MapObjectType(MapObjectFactory.Type.PatrolNode, string.Empty), false, patrolNodeInfo);
												IMapObject _mapObject;
												if (map.MapEditorMapObjectContainer.MapObjects.TryGetValue(patrolNodeID, out _mapObject))
												{
													PatrolNode patrolNode2 = _mapObject as PatrolNode;
													if (patrolNode2 != null)
													{
														patrolNode2.Label = SafeObjMan.GetString(pointMan2, "label");
														patrolNode2.Script = SafeObjMan.GetDBID(pointMan2, "script");
														patrolNodes.Add(patrolNode2);
														patrolNode2.SetAggroRadius(spawnPoint.AgrroRadius);
													}
												}
											}
										}
										int linksCount = SafeObjMan.GetInt(placeMan, "links");
										for (int linkIndex = 0; linkIndex < linksCount; linkIndex++)
										{
											string linkPropertyName = string.Format("links.[{0}]", linkIndex);
											IObjMan linkMan = placeMan.CreateManipulator(linkPropertyName);
											if (linkMan != null)
											{
												int left = SafeObjMan.GetInt(linkMan, "first");
												int right = SafeObjMan.GetInt(linkMan, "second");
												int weight = SafeObjMan.GetInt(linkMan, "weight");
												bool directional = SafeObjMan.GetBool(linkMan, "directional");
												string start = string.Empty;
												if (directional && left >= 0 && left < patrolNodes.Count)
												{
													PatrolNode.GetLabel(patrolNodes[left], out start);
												}
												if (linkMan.IsStructPtrInstanceCompatible("transferenceType", "gameMechanics.map.spawn.patrol.Spline"))
												{
													if (left >= 0 && left < patrolNodes.Count && right >= 0 && right < patrolNodes.Count)
													{
														IObjMan transferenceTypeMan = linkMan.CreateManipulator("transferenceType");
														string patrolRouteName = SafeObjMan.GetDBID(transferenceTypeMan, "route");
														List<RoutePoint> patrolRoute = new List<RoutePoint>();
														bool isClosed;
														SpawnPointsDataSource.LoadPatrolRoute(map, mainDb, patrolRouteName, patrolRoute, new PatrolLinkData(PatrolNodeLinkType.Walk, start, weight), out isClosed);
														if (patrolRoute.Count > 1)
														{
															double aggroRadius;
															if (PatrolNode.GetAggroRadius(patrolRoute[0], out aggroRadius))
															{
																foreach (RoutePoint routePoint2 in patrolRoute)
																{
																	routePoint2.SetAggroRadius(aggroRadius);
																}
															}
															if (left == right)
															{
																if (!isClosed)
																{
																	map.MapEditorMapObjectContainer.AddLink(patrolRoute[0], patrolRoute[patrolRoute.Count - 1], new PatrolLinkData(PatrolNodeLinkType.Walk, start, weight));
																}
																map.MapEditorMapObjectContainer.CopyLinks(patrolRoute[0], patrolNodes[left]);
																string script;
																if (PatrolNode.GetScript(patrolNodes[left], out script))
																{
																	PatrolNode.SetScript(patrolRoute[0], script);
																}
																string label;
																if (PatrolNode.GetLabel(patrolNodes[left], out label))
																{
																	PatrolNode.SetLabel(patrolRoute[0], label);
																}
																map.MapEditorMapObjectContainer.RemoveMapObject(patrolNodes[left]);
																patrolNodes[left] = patrolRoute[0];
															}
															else if (left != right)
															{
																if (isClosed)
																{
																	map.MapEditorMapObjectContainer.RemoveLink(patrolRoute[0], patrolRoute[patrolRoute.Count - 1]);
																}
																map.MapEditorMapObjectContainer.CopyLinks(patrolRoute[0], patrolNodes[left]);
																map.MapEditorMapObjectContainer.CopyLinks(patrolRoute[patrolRoute.Count - 1], patrolNodes[right]);
																string script2;
																if (PatrolNode.GetScript(patrolNodes[left], out script2))
																{
																	PatrolNode.SetScript(patrolRoute[0], script2);
																}
																if (PatrolNode.GetScript(patrolNodes[right], out script2))
																{
																	PatrolNode.SetScript(patrolRoute[patrolRoute.Count - 1], script2);
																}
																string label2;
																if (PatrolNode.GetLabel(patrolNodes[left], out label2))
																{
																	PatrolNode.SetLabel(patrolRoute[0], label2);
																}
																if (PatrolNode.GetLabel(patrolNodes[right], out label2))
																{
																	PatrolNode.SetLabel(patrolRoute[patrolRoute.Count - 1], label2);
																}
																map.MapEditorMapObjectContainer.RemoveMapObject(patrolNodes[left]);
																map.MapEditorMapObjectContainer.RemoveMapObject(patrolNodes[right]);
																patrolNodes[left] = patrolRoute[0];
																patrolNodes[right] = patrolRoute[patrolRoute.Count - 1];
															}
														}
														else
														{
															map.MapEditorMapObjectContainer.AddLink(patrolNodes[left], patrolNodes[right], new PatrolLinkData(PatrolNodeLinkType.Walk, start, weight));
														}
													}
												}
												else if (linkMan.IsStructPtrInstanceCompatible("transferenceType", "gameMechanics.map.spawn.patrol.Teleport"))
												{
													if (left >= 0 && left < patrolNodes.Count && right >= 0 && right < patrolNodes.Count)
													{
														map.MapEditorMapObjectContainer.AddLink(patrolNodes[left], patrolNodes[right], new PatrolLinkData(PatrolNodeLinkType.Teleport, start, weight));
													}
												}
												else if (linkMan.IsStructPtrInstanceCompatible("transferenceType", "gameMechanics.map.spawn.patrol.Walk") && left >= 0 && left < patrolNodes.Count && right >= 0 && right < patrolNodes.Count)
												{
													IObjMan transferenceTypeMan2 = linkMan.CreateManipulator("transferenceType");
													bool isFly = SafeObjMan.GetBool(transferenceTypeMan2, "isFly");
													map.MapEditorMapObjectContainer.AddLink(patrolNodes[left], patrolNodes[right], new PatrolLinkData(isFly ? PatrolNodeLinkType.Fly : PatrolNodeLinkType.Walk, start, weight));
												}
											}
										}
									}
								}
							}
							return spawnPoint;
						}
					}
				}
			}
			return null;
		}

		// Token: 0x06000AAD RID: 2733 RVA: 0x000570EC File Offset: 0x000560EC
		public static ScriptArea LoadScriptArea(MapEditorMap map, IObjMan objectMan, double additionalRatio, ref Position additionalPosition)
		{
			string scriptZone = SafeObjMan.GetDBID(objectMan, "zone");
			ScriptAreaType scriptAreaType = ScriptAreaType.Undefined;
			if (objectMan.IsStructPtrInstanceCompatible("shape", "space.scaner.observer.Cylinder"))
			{
				scriptAreaType = ScriptAreaType.Cylinder;
			}
			if (scriptAreaType != ScriptAreaType.Undefined)
			{
				MapObjectCreationInfo info = new MapObjectCreationInfo();
				if (scriptAreaType == ScriptAreaType.Cylinder)
				{
					info.Position = SafeObjMan.GetPosition(objectMan, "center") * additionalRatio + additionalPosition;
				}
				info.GroupName = SafeObjMan.GetString(objectMan, "groupName");
				int scriptAreaID = map.MapEditorMapObjectContainer.AddMapObject(new MapObjectType(MapObjectFactory.Type.ScriptArea, scriptZone), false, info);
				IMapObject mapObject;
				if (map.MapEditorMapObjectContainer.MapObjects.TryGetValue(scriptAreaID, out mapObject))
				{
					ScriptArea scriptArea = mapObject as ScriptArea;
					if (scriptArea != null)
					{
						scriptArea.ScriptID = SafeObjMan.GetString(objectMan, "scriptID");
						scriptArea.ScanRadius = SafeObjMan.GetDouble(objectMan, "scanRadius");
						scriptArea.ScriptAreaType = scriptAreaType;
						if (scriptAreaType == ScriptAreaType.Cylinder)
						{
							IObjMan shapeMan = objectMan.CreateManipulator("shape");
							if (shapeMan != null)
							{
								double radius = SafeObjMan.GetDouble(shapeMan, "radius");
								double halfheight = SafeObjMan.GetDouble(shapeMan, "halfHeight");
								if (radius > MathConsts.DOUBLE_EPSILON && halfheight > MathConsts.DOUBLE_EPSILON)
								{
									CylinderScriptAreaData cylinderScriptAreaData = scriptArea.ScriptAreaData as CylinderScriptAreaData;
									if (cylinderScriptAreaData != null)
									{
										cylinderScriptAreaData.Radius = radius;
										cylinderScriptAreaData.Halfheight = halfheight;
									}
								}
							}
						}
						return scriptArea;
					}
				}
			}
			return null;
		}

		// Token: 0x06000AAE RID: 2734 RVA: 0x00057238 File Offset: 0x00056238
		public static PermanentDevice LoadPermanentDevice(MapEditorMap map, IObjMan objectMan, double additionalRatio, ref Position additionalPosition)
		{
			MapObjectCreationInfo info = new MapObjectCreationInfo();
			info.Position = SafeObjMan.GetPosition(objectMan, "coord") * additionalRatio + additionalPosition;
			info.Rotation = new Rotation(SafeObjMan.GetFloat(objectMan, "yaw"), SafeObjMan.GetFloat(objectMan, "pitch"), SafeObjMan.GetFloat(objectMan, "roll"));
			info.GroupName = SafeObjMan.GetString(objectMan, "groupName");
			int permanentDeviceID = map.MapEditorMapObjectContainer.AddMapObject(new MapObjectType(MapObjectFactory.Type.PermanentDevice, PermanentDevice.DefaultVisObject), false, info);
			IMapObject mapObject;
			if (map.MapEditorMapObjectContainer.MapObjects.TryGetValue(permanentDeviceID, out mapObject))
			{
				PermanentDevice permanentDevice = mapObject as PermanentDevice;
				if (permanentDevice != null)
				{
					permanentDevice.Device = SafeObjMan.GetDBID(objectMan, "device");
					permanentDevice.ScriptID = SafeObjMan.GetString(objectMan, "scriptID");
					permanentDevice.ScanRadius = SafeObjMan.GetDouble(objectMan, "scanRadius");
					return permanentDevice;
				}
			}
			return null;
		}

		// Token: 0x06000AAF RID: 2735 RVA: 0x0005731C File Offset: 0x0005631C
		public static MapLocator LoadMapLocator(MapEditorMap map, IObjMan objectMan, double additionalRatio, ref Position additionalPosition)
		{
			MapObjectCreationInfo info = new MapObjectCreationInfo();
			info.Position = SafeObjMan.GetPosition(objectMan, "position") * additionalRatio + additionalPosition;
			info.GroupName = SafeObjMan.GetString(objectMan, "groupName");
			int mapLocatorID = map.MapEditorMapObjectContainer.AddMapObject(new MapObjectType(MapObjectFactory.Type.MapLocator, string.Empty), false, info);
			IMapObject mapObject;
			if (map.MapEditorMapObjectContainer.MapObjects.TryGetValue(mapLocatorID, out mapObject))
			{
				MapLocator mapLocator = mapObject as MapLocator;
				if (mapLocator != null)
				{
					mapLocator.ScriptID = SafeObjMan.GetString(objectMan, "scriptID");
					mapLocator.ScanRadius = SafeObjMan.GetDouble(objectMan, "scanRadius");
					return mapLocator;
				}
			}
			return null;
		}

		// Token: 0x06000AB0 RID: 2736 RVA: 0x000573C4 File Offset: 0x000563C4
		public static PlayerRespawnPlace LoadPlayerRespawnPlace(MapEditorMap map, IObjMan objectMan, double additionalRatio, ref Position additionalPosition)
		{
			MapObjectCreationInfo info = new MapObjectCreationInfo();
			info.Position = SafeObjMan.GetPosition(objectMan, "place.coord") * additionalRatio + additionalPosition;
			info.Rotation = new Rotation(SafeObjMan.GetFloat(objectMan, "place.yaw"), MapObjectCreationInfo.DefaultRotation.Pitch, MapObjectCreationInfo.DefaultRotation.Roll);
			string device = SafeObjMan.GetDBID(objectMan, "device");
			int mapLocatorID = map.MapEditorMapObjectContainer.AddMapObject(new MapObjectType(MapObjectFactory.Type.PlayerRespawnPlace, device), false, info);
			IMapObject mapObject;
			if (map.MapEditorMapObjectContainer.MapObjects.TryGetValue(mapLocatorID, out mapObject))
			{
				PlayerRespawnPlace playerRespawnPlace = mapObject as PlayerRespawnPlace;
				if (playerRespawnPlace != null)
				{
					playerRespawnPlace.Faction = SafeObjMan.GetDBID(objectMan, "initialMark");
					return playerRespawnPlace;
				}
			}
			return null;
		}

		// Token: 0x06000AB1 RID: 2737 RVA: 0x00057488 File Offset: 0x00056488
		public static AstralBorder LoadAstralBorder(MapEditorMap map, IObjMan objectMan, double additionalRatio, ref Position additionalPosition)
		{
			MapObjectCreationInfo info = new MapObjectCreationInfo();
			info.Position = SafeObjMan.GetPosition(objectMan, "position") * additionalRatio + additionalPosition;
			info.GroupName = SafeObjMan.GetString(objectMan, "groupName");
			int mapLocatorID = map.MapEditorMapObjectContainer.AddMapObject(new MapObjectType(MapObjectFactory.Type.AstralBorder, string.Empty), false, info);
			IMapObject mapObject;
			if (map.MapEditorMapObjectContainer.MapObjects.TryGetValue(mapLocatorID, out mapObject))
			{
				AstralBorder astralBorder = mapObject as AstralBorder;
				if (astralBorder != null)
				{
					astralBorder.StabilityRadius = SafeObjMan.GetDouble(objectMan, "stabilityZoneRadius");
					return astralBorder;
				}
			}
			return null;
		}

		// Token: 0x06000AB2 RID: 2738 RVA: 0x00057520 File Offset: 0x00056520
		public static Projectile LoadProjectile(MapEditorMap map, IDatabase mainDb, IObjMan objectMan, string placePropertyName, string projectileDBID, string scriptID, double scanRadius, double additionalRatio, ref Position additionalPosition)
		{
			IObjMan placeMan = objectMan.CreateManipulator(placePropertyName);
			if (placeMan != null)
			{
				MapObjectCreationInfo info = new MapObjectCreationInfo();
				if (objectMan.IsStructPtrInstanceCompatible(placePropertyName, "gameMechanics.map.spawn.SpawnPlacePoint"))
				{
					info.Position = SafeObjMan.GetPosition(placeMan, "center") * additionalRatio + additionalPosition;
					info.Rotation = new Rotation(SafeObjMan.GetFloat(placeMan, "yaw"), SafeObjMan.GetFloat(placeMan, "pitch"), SafeObjMan.GetFloat(placeMan, "roll"));
				}
				info.GroupName = SafeObjMan.GetString(placeMan, "groupName");
				int spawnProjectileID = map.MapEditorMapObjectContainer.AddMapObject(new MapObjectType(MapObjectFactory.Type.Projectile, projectileDBID), false, info);
				IMapObject mapObject;
				if (map.MapEditorMapObjectContainer.MapObjects.TryGetValue(spawnProjectileID, out mapObject))
				{
					Projectile projectile = mapObject as Projectile;
					if (projectile != null)
					{
						projectile.ProjectileDBID = projectileDBID;
						projectile.ScriptID = scriptID;
						projectile.ScanRadius = scanRadius;
						projectile.SpawnTime = SpawnTimeAbstract.Create(objectMan);
						return projectile;
					}
				}
			}
			return null;
		}

		// Token: 0x06000AB3 RID: 2739 RVA: 0x00057618 File Offset: 0x00056618
		private static string SavePatrolRoute(MapEditorMap map, IDatabase mainDb, List<RoutePoint> patrolRoute, bool isClosed, IDictionary<string, int> usedPatrolRoutes)
		{
			string patrolRouteName = string.Empty;
			DBID patrolRouteDBID = DBID.Empty;
			if (map != null && patrolRoute != null && patrolRoute.Count > 1)
			{
				Dictionary<string, int> routePointNames = new Dictionary<string, int>();
				foreach (RoutePoint routePoint in patrolRoute)
				{
					if (!string.IsNullOrEmpty(routePoint.Route) && !routePointNames.ContainsKey(routePoint.Route))
					{
						routePointNames.Add(routePoint.Route, 0);
					}
				}
				bool createNewName = true;
				if (routePointNames.Count > 0)
				{
					foreach (KeyValuePair<string, int> keyValuePair in routePointNames)
					{
						if (!usedPatrolRoutes.ContainsKey(keyValuePair.Key))
						{
							patrolRouteDBID = mainDb.GetDBIDByName(keyValuePair.Key);
							if (!patrolRouteDBID.IsEmpty())
							{
								patrolRouteName = keyValuePair.Key;
								createNewName = false;
								break;
							}
						}
					}
				}
				IObjMan patrolRouteMan;
				if (createNewName)
				{
					patrolRouteName = SafeObjMan.CreateUniqueDBID(Constants.ContinentFolder(map.Data.ContinentName) + RoutePoint.PatrolRouteFolder + "Route" + RoutePoint.RouteDBExtention, false, mainDb);
					patrolRouteDBID = IDatabase.CreateDBIDByName(patrolRouteName);
					patrolRouteMan = mainDb.CreateNewObject(RoutePoint.RouteDBType);
					if (patrolRouteMan != null)
					{
						mainDb.AddNewObject(patrolRouteDBID, patrolRouteMan);
					}
				}
				else
				{
					patrolRouteMan = mainDb.GetManipulator(patrolRouteDBID);
				}
				if (patrolRouteMan != null)
				{
					RoutePointsDataSource.SaveRoute(patrolRouteMan, patrolRoute, isClosed);
					foreach (RoutePoint routePoint2 in patrolRoute)
					{
						routePoint2.Route = patrolRouteName;
					}
					usedPatrolRoutes.Add(patrolRouteName, 0);
				}
			}
			return patrolRouteName;
		}

		// Token: 0x06000AB4 RID: 2740 RVA: 0x000577E0 File Offset: 0x000567E0
		private static void CalculateSpawnPointNormal(SpawnPoint spawnPoint, MapEditorMap map, EditorScene editorScene, out Vec3 normal)
		{
			normal = Vec3.ZNormal;
			if (spawnPoint != null && map != null && editorScene != null && Math.Abs(spawnPoint.Altitude) < 1.0)
			{
				Vec3 _pos = spawnPoint.Position.Vec3;
				_pos.X -= (double)(map.Data.MinXMinYPatchCoords.X * Constants.PatchSize);
				_pos.Y -= (double)(map.Data.MinXMinYPatchCoords.Y * Constants.PatchSize);
				Point pos = new Point(_pos.X + 0.5, _pos.Y + 0.5, 1.0);
				if (spawnPoint.SpawnPointType == SpawnPointType.Circle)
				{
					double radius = CircleSpawnPointData.DefaultRadius;
					CircleSpawnPointData circleSpawnPointData = spawnPoint.SpawnPointData as CircleSpawnPointData;
					if (circleSpawnPointData != null)
					{
						radius = circleSpawnPointData.Radius;
					}
					Point size = new Point(radius * 2.0 + 1.0, radius * 2.0 + 1.0, 1.0);
					Vec3 _plane;
					editorScene.CalculateAveragePlane(pos, size, true, out _plane);
					normal = new Vec3(_plane.X * -1.0, _plane.Y * -1.0, 1.0);
				}
				else
				{
					if (spawnPoint.SpawnPointType != SpawnPointType.Ellipse)
					{
						return;
					}
					double semiaxisA = EllipseSpawnPointData.DefaultSemiaxis;
					double semiaxisB = EllipseSpawnPointData.DefaultSemiaxis;
					EllipseSpawnPointData ellipseSpawnPointData = spawnPoint.SpawnPointData as EllipseSpawnPointData;
					if (ellipseSpawnPointData != null)
					{
						semiaxisA = ellipseSpawnPointData.SemiaxisA;
						semiaxisB = ellipseSpawnPointData.SemiaxisB;
					}
					Point size2 = new Point(semiaxisA * 2.0 + 1.0, semiaxisB * 2.0 + 1.0, 1.0);
					Vec3 _plane2;
					editorScene.CalculateAveragePlane(pos, size2, true, out _plane2);
					normal = new Vec3(_plane2.X * -1.0, _plane2.Y * -1.0, 1.0);
				}
				normal.Normalize();
				if (Math.Abs(normal.Z) < (double)MathConsts.FLOAT_EPSILON || normal.X * normal.X + normal.Y * normal.Y < (double)MathConsts.FLOAT_EPSILON)
				{
					normal = Vec3.ZNormal;
				}
				if ((spawnPoint.CachedNormal - normal).Length2 < SpawnPoint.NormalThreshold)
				{
					normal = spawnPoint.CachedNormal;
				}
			}
		}

		// Token: 0x06000AB5 RID: 2741 RVA: 0x00057A94 File Offset: 0x00056A94
		public static void SaveSpawnPoint(SpawnPoint spawnPoint, MapEditorMap map, IDatabase mainDb, MainForm.Context context, IObjMan objectMan, string placePropertyName, double additionalRatio, ref Position additionalPosition, SpawnPointLinkCollector spawnPointLinkCollector, IDictionary<string, int> usedPatrolRoutes)
		{
			if (spawnPoint.SpawnPointType == SpawnPointType.Guard)
			{
				objectMan.SetStructPtrInstance(placePropertyName, "gameMechanics.map.spawn.SpawnPlacePoint");
			}
			else if (spawnPoint.SpawnPointType == SpawnPointType.Circle)
			{
				objectMan.SetStructPtrInstance(placePropertyName, "gameMechanics.map.spawn.SpawnPlaceRoamingArea");
			}
			else if (spawnPoint.SpawnPointType == SpawnPointType.Ellipse)
			{
				objectMan.SetStructPtrInstance(placePropertyName, "gameMechanics.map.spawn.SpawnPlaceRoamingArea");
			}
			else if (spawnPoint.SpawnPointType == SpawnPointType.Patrol)
			{
				objectMan.SetStructPtrInstance(placePropertyName, "gameMechanics.map.spawn.patrol.SpawnPlacePatrol");
			}
			else if (spawnPoint.SpawnPointType == SpawnPointType.SpawnCircle)
			{
				objectMan.SetStructPtrInstance(placePropertyName, "gameMechanics.map.spawn.SpawnPlaceCircle");
			}
			IObjMan placeMan = objectMan.CreateManipulator(placePropertyName);
			if (placeMan != null)
			{
				SafeObjMan.SetStringOnlyModified(placeMan, "groupName", spawnPoint.GroupName);
				SafeObjMan.SetStringOnlyModified(placeMan, "spawnId", spawnPoint.SpawnID);
				SafeObjMan.SetDBID(placeMan, "tuner", spawnPoint.SpawnTuner);
				if (spawnPoint.SpawnPointType == SpawnPointType.Guard || spawnPoint.SpawnPointType == SpawnPointType.Circle || spawnPoint.SpawnPointType == SpawnPointType.Ellipse || spawnPoint.SpawnPointType == SpawnPointType.SpawnCircle)
				{
					SafeObjMan.SetPosition(placeMan, "center", (spawnPoint.Position - additionalPosition) / additionalRatio);
					SafeObjMan.SetFloat(placeMan, "yaw", spawnPoint.Rotation.Yaw);
				}
				if (spawnPoint.SpawnPointType == SpawnPointType.Guard)
				{
					SafeObjMan.SetFloat(placeMan, "pitch", spawnPoint.Rotation.Pitch);
					SafeObjMan.SetFloat(placeMan, "roll", spawnPoint.Rotation.Roll);
					return;
				}
				if (spawnPoint.SpawnPointType == SpawnPointType.Circle)
				{
					double radius = CircleSpawnPointData.DefaultRadius;
					CircleSpawnPointData circleSpawnPointData = spawnPoint.SpawnPointData as CircleSpawnPointData;
					if (circleSpawnPointData != null)
					{
						radius = circleSpawnPointData.Radius;
					}
					Vec3 normal;
					SpawnPointsDataSource.CalculateSpawnPointNormal(spawnPoint, map, context.EditorScene, out normal);
					spawnPoint.CachedNormal = normal;
					SafeObjMan.SetVec3(placeMan, "normal", normal);
					placeMan.SetStructPtrInstance("roamingArea", "gameMechanics.map.spawn.random2D.CircleRoamingArea");
					IObjMan roamingAreaMan = placeMan.CreateManipulator("roamingArea");
					SafeObjMan.SetDouble(roamingAreaMan, "radius", radius);
					return;
				}
				if (spawnPoint.SpawnPointType == SpawnPointType.Ellipse)
				{
					double semiaxisA = EllipseSpawnPointData.DefaultSemiaxis;
					double semiaxisB = EllipseSpawnPointData.DefaultSemiaxis;
					EllipseSpawnPointData ellipseSpawnPointData = spawnPoint.SpawnPointData as EllipseSpawnPointData;
					if (ellipseSpawnPointData != null)
					{
						semiaxisA = ellipseSpawnPointData.SemiaxisA;
						semiaxisB = ellipseSpawnPointData.SemiaxisB;
					}
					Vec3 normal2;
					SpawnPointsDataSource.CalculateSpawnPointNormal(spawnPoint, map, context.EditorScene, out normal2);
					spawnPoint.CachedNormal = normal2;
					SafeObjMan.SetVec3(placeMan, "normal", normal2);
					placeMan.SetStructPtrInstance("roamingArea", "gameMechanics.map.spawn.random2D.EllipseRoamingArea");
					IObjMan roamingAreaMan2 = placeMan.CreateManipulator("roamingArea");
					SafeObjMan.SetDouble(roamingAreaMan2, "semiaxisA", semiaxisA);
					SafeObjMan.SetDouble(roamingAreaMan2, "semiaxisB", semiaxisB);
					SafeObjMan.SetFloat(roamingAreaMan2, "yaw", spawnPoint.Rotation.Yaw);
					return;
				}
				if (spawnPoint.SpawnPointType == SpawnPointType.SpawnCircle)
				{
					double radius2 = SpawnCircleSpawnPointData.DefaultRadius;
					int multiplicity = SpawnCircleSpawnPointData.DefaultMultiplicity;
					SpawnCircleSpawnPointData spawnCircleSpawnPointData = spawnPoint.SpawnPointData as SpawnCircleSpawnPointData;
					if (spawnCircleSpawnPointData != null)
					{
						radius2 = spawnCircleSpawnPointData.Radius;
						multiplicity = spawnCircleSpawnPointData.Multiplicity;
					}
					SafeObjMan.SetDouble(placeMan, "radius", radius2);
					SafeObjMan.SetInt(placeMan, "multiplicity", multiplicity);
					return;
				}
				if (spawnPoint.SpawnPointType == SpawnPointType.Patrol)
				{
					spawnPointLinkCollector.Collect(spawnPoint);
					placeMan.Insert("points", -1, spawnPointLinkCollector.PatrolNodes.Count);
					for (int pointIndex = 0; pointIndex < spawnPointLinkCollector.PatrolNodes.Count; pointIndex++)
					{
						IMapObject patrolNode = spawnPointLinkCollector.PatrolNodes[pointIndex];
						if (patrolNode != null)
						{
							IObjMan pointMan = placeMan.CreateManipulator(string.Format("points.[{0}]", pointIndex));
							SafeObjMan.SetPosition(pointMan, "coords", (patrolNode.Position - additionalPosition) / additionalRatio);
							string label;
							PatrolNode.GetLabel(patrolNode, out label);
							string script;
							PatrolNode.GetScript(patrolNode, out script);
							SafeObjMan.SetStringOnlyModified(pointMan, "label", label);
							SafeObjMan.SetDBID(pointMan, "script", script);
						}
					}
					placeMan.Insert("links", -1, spawnPointLinkCollector.Links.Count);
					for (int linkIndex = 0; linkIndex < spawnPointLinkCollector.Links.Count; linkIndex++)
					{
						SpawnPointLinkCollector.LinkEntry linkEntry = spawnPointLinkCollector.Links[linkIndex];
						if (linkEntry != null)
						{
							string linkPropertyName = string.Format("links.[{0}]", linkIndex);
							IObjMan linkMan = placeMan.CreateManipulator(linkPropertyName);
							if (linkMan != null)
							{
								if (string.IsNullOrEmpty(linkEntry.Data.Start))
								{
									SafeObjMan.SetInt(linkMan, "first", linkEntry.Left);
									SafeObjMan.SetInt(linkMan, "second", linkEntry.Right);
								}
								else
								{
									string left;
									PatrolNode.GetLabel(spawnPointLinkCollector.PatrolNodes[linkEntry.Left], out left);
									string right;
									PatrolNode.GetLabel(spawnPointLinkCollector.PatrolNodes[linkEntry.Right], out right);
									if (string.Equals(left, linkEntry.Data.Start))
									{
										SafeObjMan.SetInt(linkMan, "first", linkEntry.Left);
										SafeObjMan.SetInt(linkMan, "second", linkEntry.Right);
										SafeObjMan.SetBool(linkMan, "directional", true);
									}
									else if (string.Equals(right, linkEntry.Data.Start))
									{
										SafeObjMan.SetInt(linkMan, "first", linkEntry.Right);
										SafeObjMan.SetInt(linkMan, "second", linkEntry.Left);
										SafeObjMan.SetBool(linkMan, "directional", true);
									}
									else
									{
										SafeObjMan.SetInt(linkMan, "first", linkEntry.Left);
										SafeObjMan.SetInt(linkMan, "second", linkEntry.Right);
									}
								}
								if (linkEntry.PatrolRoute != null && linkEntry.PatrolRoute.Count > 1)
								{
									linkMan.SetStructPtrInstance("transferenceType", "gameMechanics.map.spawn.patrol.Spline");
									string patrolRouteName = SpawnPointsDataSource.SavePatrolRoute(map, mainDb, linkEntry.PatrolRoute, linkEntry.Closed, usedPatrolRoutes);
									IObjMan transferenceTypeMan = linkMan.CreateManipulator("transferenceType");
									SafeObjMan.SetDBID(transferenceTypeMan, "route", patrolRouteName);
									if (linkEntry.Data != null)
									{
										SafeObjMan.SetInt(linkMan, "weight", linkEntry.Data.Weight);
									}
								}
								else if (linkEntry.Data != null)
								{
									if (linkEntry.Data.Type == PatrolNodeLinkType.Teleport)
									{
										linkMan.SetStructPtrInstance("transferenceType", "gameMechanics.map.spawn.patrol.Teleport");
									}
									else if (linkEntry.Data.Type == PatrolNodeLinkType.Walk || linkEntry.Data.Type == PatrolNodeLinkType.Fly)
									{
										linkMan.SetStructPtrInstance("transferenceType", "gameMechanics.map.spawn.patrol.Walk");
										IObjMan transferenceTypeMan2 = linkMan.CreateManipulator("transferenceType");
										SafeObjMan.SetBool(transferenceTypeMan2, "isFly", linkEntry.Data.Type == PatrolNodeLinkType.Fly);
									}
									SafeObjMan.SetInt(linkMan, "weight", linkEntry.Data.Weight);
								}
							}
						}
					}
				}
			}
		}

		// Token: 0x06000AB6 RID: 2742 RVA: 0x00058114 File Offset: 0x00057114
		public static void SaveProjectile(Projectile projectile, IObjMan objectMan, string placePropertyName, double additionalRatio, ref Position additionalPosition)
		{
			objectMan.SetStructPtrInstance(placePropertyName, "gameMechanics.map.spawn.SpawnPlacePoint");
			IObjMan placeMan = objectMan.CreateManipulator(placePropertyName);
			if (placeMan != null)
			{
				SafeObjMan.SetStringOnlyModified(placeMan, "groupName", projectile.GroupName);
				SafeObjMan.SetPosition(placeMan, "center", (projectile.Position - additionalPosition) / additionalRatio);
				SafeObjMan.SetFloat(placeMan, "yaw", projectile.Rotation.Yaw);
				SafeObjMan.SetFloat(placeMan, "pitch", projectile.Rotation.Pitch);
				SafeObjMan.SetFloat(placeMan, "roll", projectile.Rotation.Roll);
			}
		}

		// Token: 0x06000AB7 RID: 2743 RVA: 0x000581B8 File Offset: 0x000571B8
		public static void SaveScriptArea(ScriptArea scriptArea, IObjMan objectMan, double additionalRatio, ref Position additionalPosition)
		{
			SafeObjMan.SetDBID(objectMan, "zone", scriptArea.ScriptZone);
			SafeObjMan.SetStringOnlyModified(objectMan, "groupName", scriptArea.GroupName);
			SafeObjMan.SetStringOnlyModified(objectMan, "scriptID", scriptArea.ScriptID);
			SafeObjMan.SetDouble(objectMan, "scanRadius", scriptArea.ScanRadius);
			if (scriptArea.ScriptAreaType == ScriptAreaType.Cylinder)
			{
				SafeObjMan.SetPosition(objectMan, "center", (scriptArea.Position - additionalPosition) / additionalRatio);
			}
			if (scriptArea.ScriptAreaType == ScriptAreaType.Cylinder)
			{
				double radius = CylinderScriptAreaData.DefaultRadius;
				double halfheight = CylinderScriptAreaData.DefaultHalfheight;
				CylinderScriptAreaData cylinderScriptAreaData = scriptArea.ScriptAreaData as CylinderScriptAreaData;
				if (cylinderScriptAreaData != null)
				{
					radius = cylinderScriptAreaData.Radius;
					halfheight = cylinderScriptAreaData.Halfheight;
				}
				objectMan.SetStructPtrInstance("shape", "space.scaner.observer.Cylinder");
				IObjMan shapeMan = objectMan.CreateManipulator("shape");
				SafeObjMan.SetDouble(shapeMan, "radius", radius);
				SafeObjMan.SetDouble(shapeMan, "halfHeight", halfheight);
			}
		}

		// Token: 0x06000AB8 RID: 2744 RVA: 0x0005829C File Offset: 0x0005729C
		public static void SaveMapLocator(MapLocator mapLocator, IObjMan objectMan, double additionalRatio, ref Position additionalPosition)
		{
			SafeObjMan.SetStringOnlyModified(objectMan, "groupName", mapLocator.GroupName);
			SafeObjMan.SetStringOnlyModified(objectMan, "scriptID", mapLocator.ScriptID);
			SafeObjMan.SetDouble(objectMan, "scanRadius", mapLocator.ScanRadius);
			SafeObjMan.SetPosition(objectMan, "position", (mapLocator.Position - additionalPosition) / additionalRatio);
		}

		// Token: 0x06000AB9 RID: 2745 RVA: 0x00058300 File Offset: 0x00057300
		public static void SavePlayerRespawnPlace(PlayerRespawnPlace playerRespawnPlace, IObjMan objectMan, double additionalRatio, ref Position additionalPosition)
		{
			SafeObjMan.SetDBIDOnlyModified(objectMan, "device", playerRespawnPlace.Device);
			SafeObjMan.SetDBIDOnlyModified(objectMan, "initialMark", playerRespawnPlace.Faction);
			SafeObjMan.SetPosition(objectMan, "place.coord", (playerRespawnPlace.Position - additionalPosition) / additionalRatio);
			SafeObjMan.SetFloat(objectMan, "place.yaw", playerRespawnPlace.Rotation.Yaw);
		}

		// Token: 0x06000ABA RID: 2746 RVA: 0x0005836C File Offset: 0x0005736C
		public static void SaveAstralBorder(AstralBorder astralBorder, IObjMan objectMan, double additionalRatio, ref Position additionalPosition)
		{
			SafeObjMan.SetStringOnlyModified(objectMan, "groupName", astralBorder.GroupName);
			SafeObjMan.SetDouble(objectMan, "stabilityZoneRadius", astralBorder.StabilityRadius);
			SafeObjMan.SetPosition(objectMan, "position", (astralBorder.Position - additionalPosition) / additionalRatio);
		}

		// Token: 0x06000ABB RID: 2747 RVA: 0x000583BD File Offset: 0x000573BD
		public static string CreateKey(string spawnTable, string scriptID, double scanRadius)
		{
			if (spawnTable == null)
			{
				spawnTable = string.Empty;
			}
			if (scriptID == null)
			{
				scriptID = string.Empty;
			}
			if (Math.Abs(scanRadius) < MathConsts.DOUBLE_EPSILON)
			{
				scanRadius = 0.0;
			}
			return string.Format("{0}|{1}|{2}", scanRadius, scriptID, spawnTable);
		}

		// Token: 0x06000ABC RID: 2748 RVA: 0x00058400 File Offset: 0x00057400
		public static void ParseKey(string key, out string spawnTable, out string scriptID, out double scanRadius)
		{
			spawnTable = string.Empty;
			scriptID = string.Empty;
			scanRadius = 0.0;
			string[] strings = key.Split(new char[]
			{
				'|'
			});
			int index = 0;
			while (index < strings.Length && index < 3)
			{
				if (index == 0)
				{
					double.TryParse(strings[index], out scanRadius);
				}
				else if (index == 1)
				{
					scriptID = strings[index];
				}
				else if (index == 2)
				{
					spawnTable = strings[index];
				}
				index++;
			}
		}

		// Token: 0x06000ABD RID: 2749 RVA: 0x0005846F File Offset: 0x0005746F
		private static string CreateKey(ref Point localPatch)
		{
			return string.Format("{0}|{1}", localPatch.X, localPatch.Y);
		}

		// Token: 0x06000ABE RID: 2750 RVA: 0x00058494 File Offset: 0x00057494
		private static void ParseKey(string key, out Point localPatch)
		{
			localPatch = default(Point);
			string[] strings = key.Split(new char[]
			{
				'|'
			});
			int index = 0;
			while (index < strings.Length && index < 2)
			{
				int value2;
				if (index == 0)
				{
					int value;
					if (int.TryParse(strings[index], out value))
					{
						localPatch.X = value;
					}
				}
				else if (index == 1 && int.TryParse(strings[index], out value2))
				{
					localPatch.Y = value2;
				}
				index++;
			}
		}

		// Token: 0x06000ABF RID: 2751 RVA: 0x00058500 File Offset: 0x00057500
		private static void AddToServerObjectsList(IDictionary<string, List<IMapObject>> serverObjects, string key, IMapObject mapObject)
		{
			List<IMapObject> _serverObjects;
			if (!serverObjects.TryGetValue(key, out _serverObjects))
			{
				_serverObjects = new List<IMapObject>();
				serverObjects.Add(key, _serverObjects);
			}
			_serverObjects.Add(mapObject);
		}

		// Token: 0x06000AC0 RID: 2752 RVA: 0x00058530 File Offset: 0x00057530
		private bool LoadMapRegion(MapEditorMap map, MainForm.Context context, Point patchIndex)
		{
			bool result = true;
			Position additionalPosition = Constants.PatchMinXMinY(patchIndex);
			string serverPrefix = Constants.GetServerPrefix(map.Data.MapResourceName);
			if (!string.IsNullOrEmpty(serverPrefix))
			{
				serverPrefix += "_";
			}
			DBID mapRegionDBID = this.mainDb.GetDBIDByName(Constants.PatchFolder(map.Data.ContinentName, patchIndex) + serverPrefix + SpawnPointsDataSource.serverObjectsFileName);
			IObjMan mapRegionMan = this.mainDb.GetManipulator(mapRegionDBID);
			if (mapRegionMan != null)
			{
				int objectCount = SafeObjMan.GetInt(mapRegionMan, "objects");
				for (int objectIndex = 0; objectIndex < objectCount; objectIndex++)
				{
					string objectPropertyName = string.Format("objects.[{0}]", objectIndex);
					IObjMan objectMan = mapRegionMan.CreateManipulator(objectPropertyName);
					if (objectMan != null)
					{
						bool isSpawnPointTable = mapRegionMan.IsStructPtrInstanceCompatible(objectPropertyName, SpawnPointsDataSource.spawnPointTableEntryDBType);
						bool isSpawnPointSingleMob = mapRegionMan.IsStructPtrInstanceCompatible(objectPropertyName, SpawnPointsDataSource.spawnPointSingleMobEntryDBType);
						bool isSpawnPointSingleDevice = mapRegionMan.IsStructPtrInstanceCompatible(objectPropertyName, SpawnPointsDataSource.spawnPointSingleDeviceEntryDBType);
						if (isSpawnPointTable || isSpawnPointSingleMob || isSpawnPointSingleDevice)
						{
							if (isSpawnPointTable)
							{
								string spawnTable = SafeObjMan.GetDBID(objectMan, "spawnTable");
								string scriptID = SafeObjMan.GetString(objectMan, "scriptID");
								double scanRadius = SafeObjMan.GetDouble(objectMan, "scanRadius");
								int placeCount = SafeObjMan.GetInt(objectMan, "places");
								for (int placeIndex = 0; placeIndex < placeCount; placeIndex++)
								{
									string placePropertyName = string.Format("places.[{0}]", placeIndex);
									SpawnPoint spawnPoint = SpawnPointsDataSource.LoadSpawnPoint(map, this.mainDb, objectMan, placePropertyName, SpawnTableType.Table, spawnTable, scriptID, scanRadius, 1.0, ref additionalPosition);
								}
							}
							else if (isSpawnPointSingleMob)
							{
								string spawnTable2 = SafeObjMan.GetDBID(objectMan, "object");
								string scriptID2 = SafeObjMan.GetString(objectMan, "scriptID");
								double scanRadius2 = SafeObjMan.GetDouble(objectMan, "scanRadius");
								string placePropertyName2 = "place";
								SpawnPoint spawnPoint2 = SpawnPointsDataSource.LoadSpawnPoint(map, this.mainDb, objectMan, placePropertyName2, SpawnTableType.SingleMob, spawnTable2, scriptID2, scanRadius2, 1.0, ref additionalPosition);
								if (spawnPoint2 != null)
								{
									spawnPoint2.SpawnTime = SpawnTimeAbstract.Create(objectMan);
									spawnPoint2.SingleSpawnController = SingleSpawnController.Create(objectMan);
								}
							}
							else
							{
								string spawnTable3 = SafeObjMan.GetDBID(objectMan, "object");
								string scriptID3 = SafeObjMan.GetString(objectMan, "scriptID");
								double scanRadius3 = SafeObjMan.GetDouble(objectMan, "scanRadius");
								string placePropertyName3 = "place";
								SpawnPoint spawnPoint3 = SpawnPointsDataSource.LoadSpawnPoint(map, this.mainDb, objectMan, placePropertyName3, SpawnTableType.SingleDevice, spawnTable3, scriptID3, scanRadius3, 1.0, ref additionalPosition);
								if (spawnPoint3 != null)
								{
									spawnPoint3.SpawnTime = SpawnTimeAbstract.Create(objectMan);
									spawnPoint3.SingleSpawnController = SingleSpawnController.Create(objectMan);
								}
							}
						}
						else if (mapRegionMan.IsStructPtrInstanceCompatible(objectPropertyName, SpawnPointsDataSource.scriptAreaEntryDBType))
						{
							SpawnPointsDataSource.LoadScriptArea(map, objectMan, 1.0, ref additionalPosition);
						}
						else if (mapRegionMan.IsStructPtrInstanceCompatible(objectPropertyName, SpawnPointsDataSource.permanentDeviceEntryDBType))
						{
							SpawnPointsDataSource.LoadPermanentDevice(map, objectMan, 1.0, ref additionalPosition);
						}
						else if (mapRegionMan.IsStructPtrInstanceCompatible(objectPropertyName, SpawnPointsDataSource.mapLocatorEntryDBType))
						{
							SpawnPointsDataSource.LoadMapLocator(map, objectMan, 1.0, ref additionalPosition);
						}
						else if (mapRegionMan.IsStructPtrInstanceCompatible(objectPropertyName, SpawnPointsDataSource.projectileDBType))
						{
							string placePropertyName4 = "place";
							SpawnPointsDataSource.LoadProjectile(map, this.mainDb, objectMan, placePropertyName4, SafeObjMan.GetDBID(objectMan, "object"), SafeObjMan.GetString(objectMan, "scriptID"), SafeObjMan.GetDouble(objectMan, "scanRadius"), 1.0, ref additionalPosition);
						}
						else if (mapRegionMan.IsStructPtrInstanceCompatible(objectPropertyName, SpawnPointsDataSource.playerRespawnPlaceEntryDBType))
						{
							SpawnPointsDataSource.LoadPlayerRespawnPlace(map, objectMan, 1.0, ref additionalPosition);
						}
					}
				}
			}
			return result;
		}

		// Token: 0x06000AC1 RID: 2753 RVA: 0x000588B4 File Offset: 0x000578B4
		private bool RecreateMapRegion(MapEditorMap map, MainForm.Context context, Point patchIndex)
		{
			string mapRegionFolder = Constants.PatchFolder(map.Data.ContinentName, patchIndex);
			string serverPrefix = Constants.GetServerPrefix(map.Data.MapResourceName);
			if (!string.IsNullOrEmpty(serverPrefix))
			{
				serverPrefix += "_";
			}
			DBID mapRegionDBID = IDatabase.CreateDBIDByName(mapRegionFolder + serverPrefix + SpawnPointsDataSource.serverObjectsFileName);
			IObjMan mapRegionMan;
			if (this.mainDb.DoesObjectExist(mapRegionDBID))
			{
				mapRegionMan = this.mainDb.GetManipulator(mapRegionDBID);
				SafeObjMan.SetInt(mapRegionMan, "objects", 0);
			}
			else
			{
				mapRegionMan = this.mainDb.CreateNewObject(SpawnPointsDataSource.fileDBType);
				if (mapRegionMan != null)
				{
					this.mainDb.AddNewObject(mapRegionDBID, mapRegionMan);
				}
			}
			if (mapRegionMan != null)
			{
				this.mapRegionMans[mapRegionFolder] = mapRegionMan;
				return true;
			}
			return false;
		}

		// Token: 0x06000AC2 RID: 2754 RVA: 0x00058968 File Offset: 0x00057968
		public int GetProgressSteps(bool forSave)
		{
			if (forSave)
			{
				return 4;
			}
			return this.mapSize.X * this.mapSize.Y;
		}

		// Token: 0x06000AC3 RID: 2755 RVA: 0x00058998 File Offset: 0x00057998
		public bool Save(MapEditorMap map, MainForm.Context context, IProgressContainer progressContainer)
		{
			if (progressContainer != null)
			{
				progressContainer.AddString(Strings.SAVING_SERVER_OBJECTS);
			}
			if (!context.FullDatabase)
			{
				return true;
			}
			if (this.mainDb == null)
			{
				this.mainDb = IDatabase.GetMainDatabase();
			}
			if (this.mainDb == null)
			{
				return false;
			}
			this.mapRegionMans.Clear();
			bool result = SpawnPointsDataSource.ApplyToAllPatches(map, context, new SpawnPointsDataSource.PatchMethod(this.RecreateMapRegion), null);
			if (progressContainer != null)
			{
				progressContainer.Progress++;
			}
			if (result)
			{
				ObjMan.StartMassEditing();
				Dictionary<string, Dictionary<string, List<SpawnPoint>>> spawnTablePoints = new Dictionary<string, Dictionary<string, List<SpawnPoint>>>();
				Dictionary<string, List<IMapObject>> serverObjects = new Dictionary<string, List<IMapObject>>();
				foreach (KeyValuePair<int, IMapObject> keyValuePair in map.MapEditorMapObjectContainer.MapObjects)
				{
					IMapObject mapObject = keyValuePair.Value;
					if (mapObject != null && !mapObject.Temporary)
					{
						if (mapObject.Type.Type == MapObjectFactory.Type.SpawnPoint)
						{
							SpawnPoint spawnPoint = mapObject as SpawnPoint;
							if (spawnPoint != null)
							{
								Point localPatch = new Point(Constants.PatchIndex(mapObject.Position.X), Constants.PatchIndex(mapObject.Position.Y));
								string key = SpawnPointsDataSource.CreateKey(ref localPatch);
								if (spawnPoint.SpawnTableType == SpawnTableType.Table)
								{
									Dictionary<string, List<SpawnPoint>> _spawnTablePoints;
									if (!spawnTablePoints.TryGetValue(key, out _spawnTablePoints))
									{
										_spawnTablePoints = new Dictionary<string, List<SpawnPoint>>();
										spawnTablePoints.Add(key, _spawnTablePoints);
									}
									string _key = SpawnPointsDataSource.CreateKey(spawnPoint.SpawnTable, spawnPoint.ScriptID, spawnPoint.ScanRadius);
									List<SpawnPoint> __spawnTablePoints;
									if (!_spawnTablePoints.TryGetValue(_key, out __spawnTablePoints))
									{
										__spawnTablePoints = new List<SpawnPoint>();
										_spawnTablePoints.Add(_key, __spawnTablePoints);
									}
									__spawnTablePoints.Add(spawnPoint);
								}
								else
								{
									SpawnPointsDataSource.AddToServerObjectsList(serverObjects, key, spawnPoint);
								}
							}
						}
						else if (mapObject.Type.Type == MapObjectFactory.Type.Projectile)
						{
							Projectile projectile = mapObject as Projectile;
							Point localPatch2 = new Point(Constants.PatchIndex(mapObject.Position.X), Constants.PatchIndex(mapObject.Position.Y));
							string key2 = SpawnPointsDataSource.CreateKey(ref localPatch2);
							SpawnPointsDataSource.AddToServerObjectsList(serverObjects, key2, projectile);
						}
						else if (mapObject.Type.Type == MapObjectFactory.Type.ScriptArea)
						{
							ScriptArea scriptArea = mapObject as ScriptArea;
							if (scriptArea != null)
							{
								Point localPatch3 = new Point(Constants.PatchIndex(mapObject.Position.X), Constants.PatchIndex(mapObject.Position.Y));
								string key3 = SpawnPointsDataSource.CreateKey(ref localPatch3);
								SpawnPointsDataSource.AddToServerObjectsList(serverObjects, key3, scriptArea);
							}
						}
						else if (mapObject.Type.Type == MapObjectFactory.Type.MapLocator)
						{
							MapLocator mapLocator = mapObject as MapLocator;
							if (mapLocator != null)
							{
								Point localPatch4 = new Point(Constants.PatchIndex(mapObject.Position.X), Constants.PatchIndex(mapObject.Position.Y));
								string key4 = SpawnPointsDataSource.CreateKey(ref localPatch4);
								SpawnPointsDataSource.AddToServerObjectsList(serverObjects, key4, mapLocator);
							}
						}
						else if (mapObject.Type.Type == MapObjectFactory.Type.PlayerRespawnPlace)
						{
							PlayerRespawnPlace playerRespawnPlace = mapObject as PlayerRespawnPlace;
							if (playerRespawnPlace != null)
							{
								Point localPatch5 = new Point(Constants.PatchIndex(mapObject.Position.X), Constants.PatchIndex(mapObject.Position.Y));
								string key5 = SpawnPointsDataSource.CreateKey(ref localPatch5);
								SpawnPointsDataSource.AddToServerObjectsList(serverObjects, key5, playerRespawnPlace);
							}
						}
					}
				}
				if (progressContainer != null)
				{
					progressContainer.Progress++;
				}
				SpawnPointLinkCollector spawnPointLinkCollector = new SpawnPointLinkCollector(map.MapEditorMapObjectContainer);
				Dictionary<string, int> usedPatrolRoutes = new Dictionary<string, int>();
				foreach (KeyValuePair<string, Dictionary<string, List<SpawnPoint>>> keyValuePair2 in spawnTablePoints)
				{
					Point localPatch6;
					SpawnPointsDataSource.ParseKey(keyValuePair2.Key, out localPatch6);
					Position additionalPosition = Constants.PatchMinXMinY(localPatch6);
					string mapRegionFolder = Constants.PatchFolder(map.Data.ContinentName, localPatch6);
					IObjMan mapRegionMan;
					if (this.mapRegionMans.TryGetValue(mapRegionFolder, out mapRegionMan) && mapRegionMan != null)
					{
						List<KeyValuePair<string, List<SpawnPoint>>> _keyValuePairs = new List<KeyValuePair<string, List<SpawnPoint>>>();
						foreach (KeyValuePair<string, List<SpawnPoint>> _keyValuePair in keyValuePair2.Value)
						{
							_keyValuePairs.Add(_keyValuePair);
						}
						_keyValuePairs.Sort(SpawnPointsDataSource.spawnTablePointListIDComparer);
						foreach (KeyValuePair<string, List<SpawnPoint>> _keyValuePair2 in _keyValuePairs)
						{
							int objectIndex = SafeObjMan.GetInt(mapRegionMan, "objects");
							string objectPropertyName = string.Format("objects.[{0}]", objectIndex);
							mapRegionMan.Insert("objects", -1, 1);
							mapRegionMan.SetStructPtrInstance(objectPropertyName, SpawnPointsDataSource.spawnPointTableEntryDBType);
							IObjMan objectMan = mapRegionMan.CreateManipulator(objectPropertyName);
							if (objectMan != null)
							{
								string spawnTable;
								string scriptID;
								double scanRadius;
								SpawnPointsDataSource.ParseKey(_keyValuePair2.Key, out spawnTable, out scriptID, out scanRadius);
								SafeObjMan.SetDBID(objectMan, "spawnTable", spawnTable);
								SafeObjMan.SetStringOnlyModified(objectMan, "scriptID", scriptID);
								SafeObjMan.SetDouble(objectMan, "scanRadius", scanRadius);
								_keyValuePair2.Value.Sort(SpawnPointsDataSource.spawnPointIDComparer);
								foreach (SpawnPoint spawnPoint2 in _keyValuePair2.Value)
								{
									int placeIndex = SafeObjMan.GetInt(objectMan, "places");
									string placePropertyName = string.Format("places.[{0}]", placeIndex);
									objectMan.Insert("places", -1, 1);
									SpawnPointsDataSource.SaveSpawnPoint(spawnPoint2, map, this.mainDb, context, objectMan, placePropertyName, 1.0, ref additionalPosition, spawnPointLinkCollector, usedPatrolRoutes);
								}
							}
						}
					}
				}
				if (progressContainer != null)
				{
					progressContainer.Progress++;
				}
				foreach (KeyValuePair<string, List<IMapObject>> keyValuePair3 in serverObjects)
				{
					Point localPatch7;
					SpawnPointsDataSource.ParseKey(keyValuePair3.Key, out localPatch7);
					Position additionalPosition2 = Constants.PatchMinXMinY(localPatch7);
					string mapRegionFolder2 = Constants.PatchFolder(map.Data.ContinentName, localPatch7);
					IObjMan mapRegionMan2;
					if (this.mapRegionMans.TryGetValue(mapRegionFolder2, out mapRegionMan2) && mapRegionMan2 != null)
					{
						keyValuePair3.Value.Sort(MapObjectContainer.MapObjectIDComparer);
						foreach (IMapObject mapObject2 in keyValuePair3.Value)
						{
							int objectIndex2 = SafeObjMan.GetInt(mapRegionMan2, "objects");
							string objectPropertyName2 = string.Format("objects.[{0}]", objectIndex2);
							mapRegionMan2.Insert("objects", -1, 1);
							if (mapObject2.Type.Type == MapObjectFactory.Type.SpawnPoint)
							{
								SpawnPoint spawnPoint3 = mapObject2 as SpawnPoint;
								if (spawnPoint3 != null)
								{
									if (spawnPoint3.SpawnTableType == SpawnTableType.SingleMob)
									{
										mapRegionMan2.SetStructPtrInstance(objectPropertyName2, SpawnPointsDataSource.spawnPointSingleMobEntryDBType);
										IObjMan objectMan2 = mapRegionMan2.CreateManipulator(objectPropertyName2);
										if (objectMan2 != null)
										{
											SafeObjMan.SetDBID(objectMan2, "object", spawnPoint3.SpawnTable);
											SafeObjMan.SetStringOnlyModified(objectMan2, "scriptID", spawnPoint3.ScriptID);
											SafeObjMan.SetDouble(objectMan2, "scanRadius", spawnPoint3.ScanRadius);
											if (spawnPoint3.SpawnTime != null)
											{
												spawnPoint3.SpawnTime.Save(objectMan2);
											}
											else
											{
												objectMan2.SetStructPtrZero("spawnTime");
											}
											SingleSpawnController.Save(spawnPoint3.SingleSpawnController, objectMan2);
											string placePropertyName2 = "place";
											SpawnPointsDataSource.SaveSpawnPoint(spawnPoint3, map, this.mainDb, context, objectMan2, placePropertyName2, 1.0, ref additionalPosition2, spawnPointLinkCollector, usedPatrolRoutes);
										}
									}
									else if (spawnPoint3.SpawnTableType == SpawnTableType.SingleDevice)
									{
										mapRegionMan2.SetStructPtrInstance(objectPropertyName2, SpawnPointsDataSource.spawnPointSingleDeviceEntryDBType);
										IObjMan objectMan3 = mapRegionMan2.CreateManipulator(objectPropertyName2);
										if (objectMan3 != null)
										{
											SafeObjMan.SetDBID(objectMan3, "object", spawnPoint3.SpawnTable);
											SafeObjMan.SetStringOnlyModified(objectMan3, "scriptID", spawnPoint3.ScriptID);
											SafeObjMan.SetDouble(objectMan3, "scanRadius", spawnPoint3.ScanRadius);
											if (spawnPoint3.SpawnTime != null)
											{
												spawnPoint3.SpawnTime.Save(objectMan3);
											}
											else
											{
												objectMan3.SetStructPtrZero("spawnTime");
											}
											SingleSpawnController.Save(spawnPoint3.SingleSpawnController, objectMan3);
											string placePropertyName3 = "place";
											SpawnPointsDataSource.SaveSpawnPoint(spawnPoint3, map, this.mainDb, context, objectMan3, placePropertyName3, 1.0, ref additionalPosition2, spawnPointLinkCollector, usedPatrolRoutes);
										}
									}
								}
							}
							else if (mapObject2.Type.Type == MapObjectFactory.Type.Projectile)
							{
								Projectile projectile2 = mapObject2 as Projectile;
								if (projectile2 != null)
								{
									mapRegionMan2.SetStructPtrInstance(objectPropertyName2, SpawnPointsDataSource.projectileDBType);
									IObjMan objectMan4 = mapRegionMan2.CreateManipulator(objectPropertyName2);
									if (objectMan4 != null)
									{
										SafeObjMan.SetDBID(objectMan4, "object", projectile2.ProjectileDBID);
										SafeObjMan.SetStringOnlyModified(objectMan4, "scriptID", projectile2.ScriptID);
										SafeObjMan.SetDouble(objectMan4, "scanRadius", projectile2.ScanRadius);
										if (projectile2.SpawnTime != null)
										{
											projectile2.SpawnTime.Save(objectMan4);
										}
										else
										{
											objectMan4.SetStructPtrZero("spawnTime");
										}
										string placePropertyName4 = "place";
										SpawnPointsDataSource.SaveProjectile(projectile2, objectMan4, placePropertyName4, 1.0, ref additionalPosition2);
									}
								}
							}
							else if (mapObject2.Type.Type == MapObjectFactory.Type.ScriptArea)
							{
								ScriptArea scriptArea2 = mapObject2 as ScriptArea;
								if (scriptArea2 != null)
								{
									mapRegionMan2.SetStructPtrInstance(objectPropertyName2, SpawnPointsDataSource.scriptAreaEntryDBType);
									IObjMan objectMan5 = mapRegionMan2.CreateManipulator(objectPropertyName2);
									if (objectMan5 != null)
									{
										SpawnPointsDataSource.SaveScriptArea(scriptArea2, objectMan5, 1.0, ref additionalPosition2);
									}
								}
							}
							else if (mapObject2.Type.Type == MapObjectFactory.Type.MapLocator)
							{
								MapLocator mapLocator2 = mapObject2 as MapLocator;
								if (mapLocator2 != null)
								{
									mapRegionMan2.SetStructPtrInstance(objectPropertyName2, SpawnPointsDataSource.mapLocatorEntryDBType);
									IObjMan objectMan6 = mapRegionMan2.CreateManipulator(objectPropertyName2);
									if (objectMan6 != null)
									{
										SpawnPointsDataSource.SaveMapLocator(mapLocator2, objectMan6, 1.0, ref additionalPosition2);
									}
								}
							}
							else if (mapObject2.Type.Type == MapObjectFactory.Type.PlayerRespawnPlace)
							{
								PlayerRespawnPlace playerRespawnPlace2 = mapObject2 as PlayerRespawnPlace;
								if (playerRespawnPlace2 != null)
								{
									mapRegionMan2.SetStructPtrInstance(objectPropertyName2, SpawnPointsDataSource.playerRespawnPlaceEntryDBType);
									IObjMan objectMan7 = mapRegionMan2.CreateManipulator(objectPropertyName2);
									if (objectMan7 != null)
									{
										SpawnPointsDataSource.SavePlayerRespawnPlace(playerRespawnPlace2, objectMan7, 1.0, ref additionalPosition2);
									}
								}
							}
						}
					}
				}
				if (progressContainer != null)
				{
					progressContainer.Progress++;
				}
				ObjMan.StopMassEditing();
			}
			this.mapRegionMans.Clear();
			return result;
		}

		// Token: 0x06000AC4 RID: 2756 RVA: 0x000594C4 File Offset: 0x000584C4
		public bool Load(MapEditorMap map, MainForm.Context context, IProgressContainer progressContainer, out bool somethingCreated)
		{
			somethingCreated = false;
			if (progressContainer != null)
			{
				progressContainer.AddString(Strings.LOADING_SERVER_OBJECTS);
			}
			if (!context.FullDatabase)
			{
				return true;
			}
			if (this.mainDb == null)
			{
				this.mainDb = IDatabase.GetMainDatabase();
			}
			return this.mainDb != null && SpawnPointsDataSource.ApplyToAllPatches(map, context, new SpawnPointsDataSource.PatchMethod(this.LoadMapRegion), progressContainer);
		}

		// Token: 0x0400082F RID: 2095
		private static readonly string serverObjectsFileName = "ServerObjects.xdb";

		// Token: 0x04000830 RID: 2096
		private static readonly string fileDBType = "gameMechanics.map.PatchObjects";

		// Token: 0x04000831 RID: 2097
		private static readonly string spawnPointTableEntryDBType = "gameMechanics.map.spawn.SpawnLocus";

		// Token: 0x04000832 RID: 2098
		private static readonly string spawnPointSingleMobEntryDBType = "gameMechanics.map.spawn.MobSingleSpawn";

		// Token: 0x04000833 RID: 2099
		private static readonly string spawnPointSingleDeviceEntryDBType = "gameMechanics.map.spawn.DeviceSingleSpawn";

		// Token: 0x04000834 RID: 2100
		private static readonly string scriptAreaEntryDBType = "gameMechanics.map.scriptZone.ScriptZoneElement";

		// Token: 0x04000835 RID: 2101
		private static readonly string permanentDeviceEntryDBType = "gameMechanics.map.PermanentDevice";

		// Token: 0x04000836 RID: 2102
		private static readonly string mapLocatorEntryDBType = "gameMechanics.map.Locator";

		// Token: 0x04000837 RID: 2103
		private static readonly string playerRespawnPlaceEntryDBType = "gameMechanics.map.spawn.recaptureable.RecaptureablePlayerSpawnPlace";

		// Token: 0x04000838 RID: 2104
		private static readonly string projectileDBType = "gameMechanics.map.spawn.ProjectileSingleSpawn";

		// Token: 0x04000839 RID: 2105
		private readonly Dictionary<string, IObjMan> mapRegionMans = new Dictionary<string, IObjMan>();

		// Token: 0x0400083A RID: 2106
		private IDatabase mainDb;

		// Token: 0x0400083B RID: 2107
		private readonly Point mapSize;

		// Token: 0x0400083C RID: 2108
		private static readonly SpawnPointsDataSource.SpawnPointIDComparer spawnPointIDComparer = new SpawnPointsDataSource.SpawnPointIDComparer();

		// Token: 0x0400083D RID: 2109
		private static readonly SpawnPointsDataSource.SpawnTablePointListIDComparer spawnTablePointListIDComparer = new SpawnPointsDataSource.SpawnTablePointListIDComparer();

		// Token: 0x020000D1 RID: 209
		private class SpawnPointIDComparer : IComparer<SpawnPoint>
		{
			// Token: 0x06000AC6 RID: 2758 RVA: 0x000595A5 File Offset: 0x000585A5
			public int Compare(SpawnPoint left, SpawnPoint right)
			{
				return MapObjectContainer.MapObjectIDComparer.Compare(left, right);
			}
		}

		// Token: 0x020000D2 RID: 210
		private class SpawnTablePointListIDComparer : IComparer<KeyValuePair<string, List<SpawnPoint>>>
		{
			// Token: 0x06000AC8 RID: 2760 RVA: 0x000595BC File Offset: 0x000585BC
			public int Compare(KeyValuePair<string, List<SpawnPoint>> left, KeyValuePair<string, List<SpawnPoint>> right)
			{
				if (left.Value != null && right.Value != null)
				{
					int minLeft = -1;
					int minRight = -1;
					for (int index = 0; index < left.Value.Count; index++)
					{
						if (left.Value[index] != null && (minLeft == -1 || left.Value[index].ID < minLeft))
						{
							minLeft = left.Value[index].ID;
						}
					}
					for (int index2 = 0; index2 < right.Value.Count; index2++)
					{
						if (right.Value[index2] != null && (minRight == -1 || right.Value[index2].ID < minRight))
						{
							minRight = right.Value[index2].ID;
						}
					}
					if (minLeft < minRight)
					{
						return -1;
					}
					if (minLeft > minRight)
					{
						return 1;
					}
				}
				return 0;
			}
		}

		// Token: 0x020000D3 RID: 211
		// (Invoke) Token: 0x06000ACB RID: 2763
		private delegate bool PatchMethod(MapEditorMap map, MainForm.Context context, Point patchIndex);
	}
}
