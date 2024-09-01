using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using Db;
using MapEditor.Resources.Strings;
using Tools.Geometry;
using Tools.LinkContainer;
using Tools.MapObjects;
using Tools.SafeObjMan;

namespace MapEditor.Map.MapObjects
{
	// Token: 0x02000088 RID: 136
	public class RoutePoint : MapObject, IMapObjectInterfaceExtention
	{
		// Token: 0x14000016 RID: 22
		// (add) Token: 0x0600065A RID: 1626 RVA: 0x00034BC7 File Offset: 0x00033BC7
		// (remove) Token: 0x0600065B RID: 1627 RVA: 0x00034BDE File Offset: 0x00033BDE
		public static event RoutePoint.RoutePointFieldChangedEvent<string> RouteChanged;

		// Token: 0x14000017 RID: 23
		// (add) Token: 0x0600065C RID: 1628 RVA: 0x00034BF5 File Offset: 0x00033BF5
		// (remove) Token: 0x0600065D RID: 1629 RVA: 0x00034C0C File Offset: 0x00033C0C
		public static event RoutePoint.RoutePointFieldChangedEvent<RoutePointType> RoutePointTypeChanged;

		// Token: 0x14000018 RID: 24
		// (add) Token: 0x0600065E RID: 1630 RVA: 0x00034C23 File Offset: 0x00033C23
		// (remove) Token: 0x0600065F RID: 1631 RVA: 0x00034C3A File Offset: 0x00033C3A
		public static event RoutePoint.RoutePointFieldChangedEvent<double> SpeedChanged;

		// Token: 0x14000019 RID: 25
		// (add) Token: 0x06000660 RID: 1632 RVA: 0x00034C51 File Offset: 0x00033C51
		// (remove) Token: 0x06000661 RID: 1633 RVA: 0x00034C68 File Offset: 0x00033C68
		public static event RoutePoint.RoutePointFieldChangedEvent<double> LatencyChanged;

		// Token: 0x1400001A RID: 26
		// (add) Token: 0x06000662 RID: 1634 RVA: 0x00034C7F File Offset: 0x00033C7F
		// (remove) Token: 0x06000663 RID: 1635 RVA: 0x00034C96 File Offset: 0x00033C96
		public static event RoutePoint.RoutePointFieldChangedEvent<string> LabelChanged;

		// Token: 0x1400001B RID: 27
		// (add) Token: 0x06000664 RID: 1636 RVA: 0x00034CAD File Offset: 0x00033CAD
		// (remove) Token: 0x06000665 RID: 1637 RVA: 0x00034CC4 File Offset: 0x00033CC4
		public static event RoutePoint.RoutePointFieldChangedEvent<string> ScriptChanged;

		// Token: 0x1400001C RID: 28
		// (add) Token: 0x06000666 RID: 1638 RVA: 0x00034CDB File Offset: 0x00033CDB
		// (remove) Token: 0x06000667 RID: 1639 RVA: 0x00034CF2 File Offset: 0x00033CF2
		public static event RoutePoint.RoutePointFieldChangedEvent<double> AggroRadiusChanged;

		// Token: 0x170000DF RID: 223
		// (get) Token: 0x06000668 RID: 1640 RVA: 0x00034D09 File Offset: 0x00033D09
		public static Color InterfaceColor
		{
			get
			{
				return RoutePoint.interfaceColor;
			}
		}

		// Token: 0x170000E0 RID: 224
		// (get) Token: 0x06000669 RID: 1641 RVA: 0x00034D10 File Offset: 0x00033D10
		public static string InterfaceSingleObjectTypeName
		{
			get
			{
				return RoutePoint.interfaceSingleObjectTypeName;
			}
		}

		// Token: 0x170000E1 RID: 225
		// (get) Token: 0x0600066A RID: 1642 RVA: 0x00034D17 File Offset: 0x00033D17
		public static string InterfaceSeveralObjectsTypeName
		{
			get
			{
				return RoutePoint.interfaceSeveralObjectsTypeName;
			}
		}

		// Token: 0x170000E2 RID: 226
		// (get) Token: 0x0600066B RID: 1643 RVA: 0x00034D1E File Offset: 0x00033D1E
		public static RoutePointType DefaultRoutePointType
		{
			get
			{
				return RoutePointType.Simple;
			}
		}

		// Token: 0x170000E3 RID: 227
		// (get) Token: 0x0600066C RID: 1644 RVA: 0x00034D21 File Offset: 0x00033D21
		public static string SimpleVisObject
		{
			get
			{
				return RoutePoint.simpleVisObject;
			}
		}

		// Token: 0x170000E4 RID: 228
		// (get) Token: 0x0600066D RID: 1645 RVA: 0x00034D28 File Offset: 0x00033D28
		public static string ComplexVisObject
		{
			get
			{
				return RoutePoint.complexVisObject;
			}
		}

		// Token: 0x170000E5 RID: 229
		// (get) Token: 0x0600066E RID: 1646 RVA: 0x00034D2F File Offset: 0x00033D2F
		public static string TourFolder
		{
			get
			{
				return RoutePoint.tourFolder;
			}
		}

		// Token: 0x170000E6 RID: 230
		// (get) Token: 0x0600066F RID: 1647 RVA: 0x00034D36 File Offset: 0x00033D36
		public static string RouteFolder
		{
			get
			{
				return RoutePoint.routeFolder;
			}
		}

		// Token: 0x170000E7 RID: 231
		// (get) Token: 0x06000670 RID: 1648 RVA: 0x00034D3D File Offset: 0x00033D3D
		public static string PatrolRouteFolder
		{
			get
			{
				return RoutePoint.patrolRouteFolder;
			}
		}

		// Token: 0x170000E8 RID: 232
		// (get) Token: 0x06000671 RID: 1649 RVA: 0x00034D44 File Offset: 0x00033D44
		public static string TourDBType
		{
			get
			{
				return RoutePoint.tourDBType;
			}
		}

		// Token: 0x170000E9 RID: 233
		// (get) Token: 0x06000672 RID: 1650 RVA: 0x00034D4B File Offset: 0x00033D4B
		public static string RouteDBType
		{
			get
			{
				return RoutePoint.routeDBType;
			}
		}

		// Token: 0x170000EA RID: 234
		// (get) Token: 0x06000673 RID: 1651 RVA: 0x00034D52 File Offset: 0x00033D52
		public static string RouteDBExtention
		{
			get
			{
				return RoutePoint.routeDBExtention;
			}
		}

		// Token: 0x170000EB RID: 235
		// (get) Token: 0x06000674 RID: 1652 RVA: 0x00034D59 File Offset: 0x00033D59
		public static double DefaultTangentLength
		{
			get
			{
				return RoutePoint.defaultTangentLength;
			}
		}

		// Token: 0x06000675 RID: 1653 RVA: 0x00034D60 File Offset: 0x00033D60
		public static bool RouteAdvanceIsDirect(RoutePoint left, RoutePoint right)
		{
			bool direct;
			if (left.RouteAdvance < right.RouteAdvance)
			{
				direct = true;
				if (right.RouteAdvance - left.RouteAdvance > 1.0)
				{
					direct = false;
				}
			}
			else
			{
				direct = false;
				if (left.RouteAdvance - right.RouteAdvance > 1.0)
				{
					direct = true;
				}
			}
			return direct;
		}

		// Token: 0x06000676 RID: 1654 RVA: 0x00034DB8 File Offset: 0x00033DB8
		public static string GetShipResource(string route)
		{
			if (!string.IsNullOrEmpty(route))
			{
				IDatabase mainDb = IDatabase.GetMainDatabase();
				if (mainDb != null)
				{
					DBID routeDBID = mainDb.GetDBIDByName(route);
					if (!routeDBID.IsEmpty())
					{
						string classTypeName = mainDb.GetClassTypeName(routeDBID);
						if (classTypeName == RoutePoint.tourDBType)
						{
							IObjMan tourMan = mainDb.GetManipulator(routeDBID);
							if (tourMan != null && !tourMan.IsStructPtrZero("tourTraveler"))
							{
								IObjMan tourTravelerMan = tourMan.CreateManipulator("tourTraveler");
								if (tourTravelerMan != null)
								{
									return SafeObjMan.GetDBID(tourTravelerMan, "shipResource");
								}
							}
						}
					}
				}
			}
			return string.Empty;
		}

		// Token: 0x06000677 RID: 1655 RVA: 0x00034E38 File Offset: 0x00033E38
		public static string GetRouteObject(string route, out bool rotate)
		{
			rotate = true;
			if (!string.IsNullOrEmpty(route))
			{
				IDatabase mainDb = IDatabase.GetMainDatabase();
				if (mainDb != null)
				{
					DBID routeDBID = mainDb.GetDBIDByName(route);
					if (!routeDBID.IsEmpty())
					{
						string classTypeName = mainDb.GetClassTypeName(routeDBID);
						if (classTypeName == RoutePoint.tourDBType)
						{
							IObjMan tourMan = mainDb.GetManipulator(routeDBID);
							if (tourMan != null && !tourMan.IsStructPtrZero("tourTraveler"))
							{
								IObjMan tourTravelerMan = tourMan.CreateManipulator("tourTraveler");
								if (tourTravelerMan != null)
								{
									IObjMan shuttleControllerMan = mainDb.GetManipulator(mainDb.GetDBIDByName(SafeObjMan.GetDBID(tourTravelerMan, "shuttleController")));
									if (shuttleControllerMan != null)
									{
										rotate = SafeObjMan.GetBool(shuttleControllerMan, "turnsInMotion");
									}
									IObjMan shipResourceMan = mainDb.GetManipulator(mainDb.GetDBIDByName(SafeObjMan.GetDBID(tourTravelerMan, "shipResource")));
									if (shipResourceMan != null)
									{
										IObjMan visualShipMan = mainDb.GetManipulator(mainDb.GetDBIDByName(SafeObjMan.GetDBID(shipResourceMan, "visualShip")));
										if (visualShipMan != null)
										{
											IObjMan visualTransportMan = mainDb.GetManipulator(mainDb.GetDBIDByName(SafeObjMan.GetDBID(visualShipMan, "visualTransport")));
											return SafeObjMan.GetDBID(visualTransportMan, "transportVisObject");
										}
									}
								}
							}
						}
					}
				}
			}
			return string.Empty;
		}

		// Token: 0x06000678 RID: 1656 RVA: 0x00034F50 File Offset: 0x00033F50
		public static RoutePoint GetStartRoutePoint(IMapObject mapObject, MapObjectContainer mapObjectContainer)
		{
			if (mapObject != null && mapObject.Type.Type == MapObjectFactory.Type.RoutePoint)
			{
				RoutePoint middle = mapObject as RoutePoint;
				if (middle != null)
				{
					Dictionary<IMapObject, ILinkData> links = mapObjectContainer.GetLinks(middle);
					if (links != null && links.Count > 0)
					{
						int linksCount = 0;
						foreach (KeyValuePair<IMapObject, ILinkData> keyValuePair in links)
						{
							if (keyValuePair.Key.Type.Type == MapObjectFactory.Type.RoutePoint)
							{
								linksCount++;
							}
						}
						if (linksCount > 0)
						{
							RoutePoint left = MapObjectContainer.GetOtherLinkedMapObject(links, MapObjectFactory.Type.RoutePoint, null, false) as RoutePoint;
							RoutePoint right = MapObjectContainer.GetOtherLinkedMapObject(links, MapObjectFactory.Type.RoutePoint, left, false) as RoutePoint;
							if (left != null && right != null)
							{
								if (middle.routeAdvance - left.routeAdvance > 1.0)
								{
									return left;
								}
								if (left.routeAdvance - middle.routeAdvance > 1.0)
								{
									return middle;
								}
								if (middle.routeAdvance - right.routeAdvance > 1.0)
								{
									return right;
								}
								if (right.routeAdvance - middle.routeAdvance > 1.0)
								{
									return middle;
								}
								if (middle.routeAdvance > left.routeAdvance)
								{
									return RoutePoint.GetStartRoutePoint(left, mapObjectContainer);
								}
								if (middle.routeAdvance > right.routeAdvance)
								{
									return RoutePoint.GetStartRoutePoint(right, mapObjectContainer);
								}
								return middle;
							}
							else if (left != null)
							{
								if (middle.routeAdvance > left.routeAdvance)
								{
									return RoutePoint.GetStartRoutePoint(left, mapObjectContainer);
								}
								return middle;
							}
							else
							{
								if (right == null)
								{
									return middle;
								}
								if (middle.routeAdvance > right.routeAdvance)
								{
									return RoutePoint.GetStartRoutePoint(right, mapObjectContainer);
								}
								return middle;
							}
						}
					}
				}
			}
			return null;
		}

		// Token: 0x06000679 RID: 1657 RVA: 0x00035120 File Offset: 0x00034120
		public static List<RoutePoint> GetRoute(IMapObject startMapObject, MapObjectContainer mapObjectContainer)
		{
			if (startMapObject != null && startMapObject.Type.Type == MapObjectFactory.Type.RoutePoint)
			{
				RoutePoint start = startMapObject as RoutePoint;
				if (start != null)
				{
					List<RoutePoint> route = new List<RoutePoint>();
					for (;;)
					{
						route.Add(start);
						Dictionary<IMapObject, ILinkData> links = mapObjectContainer.GetLinks(start);
						if (links != null && links.Count > 0)
						{
							int linksCount = 0;
							foreach (KeyValuePair<IMapObject, ILinkData> keyValuePair in links)
							{
								if (keyValuePair.Key.Type.Type == MapObjectFactory.Type.RoutePoint)
								{
									linksCount++;
								}
							}
							if (linksCount > 0)
							{
								RoutePoint left = MapObjectContainer.GetOtherLinkedMapObject(links, MapObjectFactory.Type.RoutePoint, null, false) as RoutePoint;
								RoutePoint right = MapObjectContainer.GetOtherLinkedMapObject(links, MapObjectFactory.Type.RoutePoint, left, false) as RoutePoint;
								if (left != null && right != null)
								{
									if (start.routeAdvance - left.routeAdvance > 1.0)
									{
										break;
									}
									if (left.routeAdvance - start.routeAdvance > 1.0)
									{
										start = right;
									}
									else
									{
										if (start.routeAdvance - right.routeAdvance > 1.0)
										{
											break;
										}
										if (right.routeAdvance - start.routeAdvance > 1.0)
										{
											start = left;
										}
										else if (start.routeAdvance > left.routeAdvance)
										{
											start = right;
										}
										else
										{
											if (start.routeAdvance <= right.routeAdvance)
											{
												break;
											}
											start = left;
										}
									}
								}
								else if (left != null)
								{
									if (left.routeAdvance <= start.routeAdvance)
									{
										break;
									}
									start = left;
								}
								else
								{
									if (right == null || right.routeAdvance <= start.routeAdvance)
									{
										break;
									}
									start = right;
								}
							}
						}
					}
					return route;
				}
			}
			return null;
		}

		// Token: 0x0600067A RID: 1658 RVA: 0x000352F4 File Offset: 0x000342F4
		public static List<List<RoutePoint>> GetRoutes(Dictionary<IMapObject, IMapObject> mapObjects, MapObjectContainer mapObjectContainer)
		{
			if (mapObjects != null && mapObjects.Count > 0 && mapObjectContainer != null)
			{
				List<List<RoutePoint>> routes = new List<List<RoutePoint>>();
				Dictionary<IMapObject, int> usedMapObjects = new Dictionary<IMapObject, int>();
				foreach (KeyValuePair<IMapObject, IMapObject> keyValuePair in mapObjects)
				{
					if (keyValuePair.Key.Type.Type == MapObjectFactory.Type.RoutePoint)
					{
						RoutePoint middle = keyValuePair.Key as RoutePoint;
						if (middle != null && !usedMapObjects.ContainsKey(middle))
						{
							RoutePoint startRoutePoint = RoutePoint.GetStartRoutePoint(middle, mapObjectContainer);
							List<RoutePoint> route = RoutePoint.GetRoute(startRoutePoint, mapObjectContainer);
							if (route != null && route.Count > 0)
							{
								foreach (RoutePoint routePoint in route)
								{
									if (mapObjects.ContainsKey(routePoint))
									{
										usedMapObjects.Add(routePoint, 0);
									}
								}
								routes.Add(route);
							}
						}
					}
				}
				if (routes.Count > 0)
				{
					return routes;
				}
			}
			return null;
		}

		// Token: 0x0600067B RID: 1659 RVA: 0x00035420 File Offset: 0x00034420
		public RoutePoint(int _id, MapObjectType _type, ICollisionMap _collisionMap, RoutePointType _routePointType) : base(_id, _type, _collisionMap)
		{
			this.route = _type.Stats;
			this.routePointType = _routePointType;
		}

		// Token: 0x170000EC RID: 236
		// (get) Token: 0x0600067C RID: 1660 RVA: 0x0003548D File Offset: 0x0003448D
		// (set) Token: 0x0600067D RID: 1661 RVA: 0x00035498 File Offset: 0x00034498
		[Browsable(true)]
		[Category("Route Point")]
		[DisplayName("Route")]
		public string Route
		{
			get
			{
				return this.route;
			}
			set
			{
				if (this.route != value && base.InvokeChanging(null))
				{
					string oldRoute = this.route;
					this.route = value;
					base.SetNewStats(this.route);
					base.InvokeChanged();
					if (base.Active && RoutePoint.RouteChanged != null)
					{
						RoutePoint.RouteChanged(this, ref oldRoute, ref this.route);
					}
				}
			}
		}

		// Token: 0x170000ED RID: 237
		// (get) Token: 0x0600067E RID: 1662 RVA: 0x000354FE File Offset: 0x000344FE
		// (set) Token: 0x0600067F RID: 1663 RVA: 0x00035508 File Offset: 0x00034508
		[DisplayName("Type")]
		[Category("Route Point")]
		[Browsable(true)]
		public RoutePointType RoutePointType
		{
			get
			{
				return this.routePointType;
			}
			[Browsable(false)]
			set
			{
				if (this.routePointType != value && base.InvokeChanging(null))
				{
					RoutePointType oldRoutePointType = this.routePointType;
					this.routePointType = value;
					base.InvokeChanged();
					if (base.Active && RoutePoint.RoutePointTypeChanged != null)
					{
						RoutePoint.RoutePointTypeChanged(this, ref oldRoutePointType, ref this.routePointType);
					}
				}
			}
		}

		// Token: 0x170000EE RID: 238
		// (get) Token: 0x06000680 RID: 1664 RVA: 0x0003555D File Offset: 0x0003455D
		// (set) Token: 0x06000681 RID: 1665 RVA: 0x00035568 File Offset: 0x00034568
		[Browsable(true)]
		[Category("Route Point")]
		[DisplayName("Speed")]
		public double Speed
		{
			get
			{
				return this.speed;
			}
			set
			{
				if (this.speed != value && base.InvokeChanging(null))
				{
					double oldSpeed = this.speed;
					this.speed = value;
					base.InvokeChanged();
					if (base.Active && RoutePoint.SpeedChanged != null)
					{
						RoutePoint.SpeedChanged(this, ref oldSpeed, ref this.speed);
					}
				}
			}
		}

		// Token: 0x170000EF RID: 239
		// (get) Token: 0x06000682 RID: 1666 RVA: 0x000355BD File Offset: 0x000345BD
		// (set) Token: 0x06000683 RID: 1667 RVA: 0x000355C8 File Offset: 0x000345C8
		[DisplayName("Latency")]
		[Browsable(true)]
		[Category("Route Point")]
		public double Latency
		{
			get
			{
				return this.latency;
			}
			set
			{
				if (this.latency != value && base.InvokeChanging(null))
				{
					double oldLatency = this.latency;
					this.latency = value;
					base.InvokeChanged();
					if (base.Active && RoutePoint.LatencyChanged != null)
					{
						RoutePoint.LatencyChanged(this, ref oldLatency, ref this.latency);
					}
				}
			}
		}

		// Token: 0x170000F0 RID: 240
		// (get) Token: 0x06000684 RID: 1668 RVA: 0x0003561D File Offset: 0x0003461D
		// (set) Token: 0x06000685 RID: 1669 RVA: 0x00035625 File Offset: 0x00034625
		[DisplayName("RouteAdvance")]
		[Category("Arrangement")]
		[Browsable(true)]
		public double RouteAdvance
		{
			get
			{
				return this.routeAdvance;
			}
			set
			{
				this.routeAdvance = value;
			}
		}

		// Token: 0x170000F1 RID: 241
		// (get) Token: 0x06000686 RID: 1670 RVA: 0x0003562E File Offset: 0x0003462E
		// (set) Token: 0x06000687 RID: 1671 RVA: 0x00035636 File Offset: 0x00034636
		[DisplayName("RouteLinks")]
		[Category("Arrangement")]
		[Browsable(false)]
		public int RouteLinks
		{
			get
			{
				return this.routeLinks;
			}
			set
			{
				this.routeLinks = value;
			}
		}

		// Token: 0x170000F2 RID: 242
		// (get) Token: 0x06000688 RID: 1672 RVA: 0x0003563F File Offset: 0x0003463F
		// (set) Token: 0x06000689 RID: 1673 RVA: 0x00035648 File Offset: 0x00034648
		[Browsable(true)]
		[DisplayName("Label")]
		[Category("Route Point")]
		public string Label
		{
			get
			{
				return this.label;
			}
			set
			{
				if (this.label != value && base.InvokeChanging(null))
				{
					string oldLabel = this.label;
					this.label = value;
					base.InvokeChanged();
					if (base.Active && RoutePoint.LabelChanged != null)
					{
						RoutePoint.LabelChanged(this, ref oldLabel, ref this.label);
					}
				}
			}
		}

		// Token: 0x170000F3 RID: 243
		// (get) Token: 0x0600068A RID: 1674 RVA: 0x000356A2 File Offset: 0x000346A2
		// (set) Token: 0x0600068B RID: 1675 RVA: 0x000356AC File Offset: 0x000346AC
		[Category("Route Point")]
		[Browsable(true)]
		[DisplayName("Script")]
		public string Script
		{
			get
			{
				return this.script;
			}
			set
			{
				if (this.script != value && base.InvokeChanging(null))
				{
					string oldScript = this.script;
					this.script = value;
					base.InvokeChanged();
					if (base.Active && RoutePoint.ScriptChanged != null)
					{
						RoutePoint.ScriptChanged(this, ref oldScript, ref this.script);
					}
				}
			}
		}

		// Token: 0x170000F4 RID: 244
		// (get) Token: 0x0600068C RID: 1676 RVA: 0x00035706 File Offset: 0x00034706
		[Browsable(true)]
		[Category("Route Point")]
		[DisplayName("AggroRadius")]
		public double AgrroRadius
		{
			get
			{
				return this.aggroRadius;
			}
		}

		// Token: 0x170000F5 RID: 245
		// (get) Token: 0x0600068D RID: 1677 RVA: 0x00035710 File Offset: 0x00034710
		// (set) Token: 0x0600068E RID: 1678 RVA: 0x000357A0 File Offset: 0x000347A0
		[Browsable(false)]
		public Vec3 Tangent
		{
			get
			{
				Quat quat = new Quat((double)base.Rotation.Yaw, (double)base.Rotation.Pitch, (double)base.Rotation.Roll);
				Vec3 tangent = new Vec3(1.0, 0.0, 0.0);
				tangent = quat.Rotate(tangent);
				tangent = tangent * (double)base.Scale.Ratio * RoutePoint.defaultTangentLength;
				return tangent;
			}
			set
			{
				Quat quat = new Quat(value);
				base.Rotation = new Rotation(ref quat);
				base.Scale = new Scale((float)(value.Length / RoutePoint.defaultTangentLength));
			}
		}

		// Token: 0x170000F6 RID: 246
		// (get) Token: 0x0600068F RID: 1679 RVA: 0x000357E0 File Offset: 0x000347E0
		[Browsable(false)]
		public override string SceneName
		{
			get
			{
				if (this.routePointType == RoutePointType.Simple)
				{
					return RoutePoint.simpleVisObject;
				}
				if (this.routePointType == RoutePointType.PatrolNode)
				{
					return RoutePoint.complexVisObject;
				}
				return string.Empty;
			}
		}

		// Token: 0x06000690 RID: 1680 RVA: 0x00035808 File Offset: 0x00034808
		public override IMapObject Clone(int newID, bool newTemporary, bool newActive)
		{
			RoutePoint routePoint = new RoutePoint(newID, new MapObjectType(base.Type.Type, this.route), base.CollisionMap, this.routePointType);
			routePoint.speed = this.speed;
			routePoint.latency = this.latency;
			base.CopyTo(routePoint, newTemporary, newActive);
			return routePoint;
		}

		// Token: 0x06000691 RID: 1681 RVA: 0x00035864 File Offset: 0x00034864
		public override IMapObjectPack Pack()
		{
			RoutePointPack routePointPack = new RoutePointPack();
			routePointPack.Pack(this);
			return routePointPack;
		}

		// Token: 0x06000692 RID: 1682 RVA: 0x0003587F File Offset: 0x0003487F
		public Color GetInterfaceColor()
		{
			return RoutePoint.interfaceColor;
		}

		// Token: 0x06000693 RID: 1683 RVA: 0x00035886 File Offset: 0x00034886
		public string GetInterfaceSingleObjectTypeName()
		{
			return RoutePoint.interfaceSingleObjectTypeName;
		}

		// Token: 0x06000694 RID: 1684 RVA: 0x0003588D File Offset: 0x0003488D
		public string GetInterfaceSeveralObjectsTypeName()
		{
			return RoutePoint.interfaceSeveralObjectsTypeName;
		}

		// Token: 0x06000695 RID: 1685 RVA: 0x00035894 File Offset: 0x00034894
		public bool ContainsText(string text, bool ignoreCase)
		{
			if (string.IsNullOrEmpty(text))
			{
				return true;
			}
			if (string.IsNullOrEmpty(this.route))
			{
				return false;
			}
			if (ignoreCase)
			{
				return this.route.ToLower().Contains(text.ToLower());
			}
			return this.route.Contains(text);
		}

		// Token: 0x06000696 RID: 1686 RVA: 0x000358E0 File Offset: 0x000348E0
		public string GetStatsForDBBrowse()
		{
			return this.route;
		}

		// Token: 0x06000697 RID: 1687 RVA: 0x000358E8 File Offset: 0x000348E8
		public string GetSpecialStatsForDBBrowse()
		{
			return string.Empty;
		}

		// Token: 0x06000698 RID: 1688 RVA: 0x000358EF File Offset: 0x000348EF
		public string GetLabel()
		{
			return this.label;
		}

		// Token: 0x06000699 RID: 1689 RVA: 0x000358F7 File Offset: 0x000348F7
		public void SetLabel(string _label)
		{
			this.Label = _label;
		}

		// Token: 0x0600069A RID: 1690 RVA: 0x00035900 File Offset: 0x00034900
		public string GetScript()
		{
			return this.script;
		}

		// Token: 0x0600069B RID: 1691 RVA: 0x00035908 File Offset: 0x00034908
		public void SetScript(string _script)
		{
			this.Script = _script;
		}

		// Token: 0x0600069C RID: 1692 RVA: 0x00035914 File Offset: 0x00034914
		public void SetAggroRadius(double newAggroRadius)
		{
			if (this.aggroRadius != newAggroRadius && base.InvokeChanging(null))
			{
				double oldAggroRadius = this.aggroRadius;
				this.aggroRadius = newAggroRadius;
				base.InvokeChanged();
				if (base.Active && RoutePoint.AggroRadiusChanged != null)
				{
					RoutePoint.AggroRadiusChanged(this, ref oldAggroRadius, ref this.aggroRadius);
				}
			}
		}

		// Token: 0x040004C3 RID: 1219
		private static readonly Color interfaceColor = Color.FromArgb(MapObjectCreationInfo.DefaultTransparentColorAlpha, Color.LightSalmon);

		// Token: 0x040004C4 RID: 1220
		private static readonly string interfaceSingleObjectTypeName = Strings.SINGLE_ROUTE_POINT_TYPE_NAME;

		// Token: 0x040004C5 RID: 1221
		private static readonly string interfaceSeveralObjectsTypeName = Strings.SEVERAL_ROUTE_POINTS_TYPE_NAME;

		// Token: 0x040004C6 RID: 1222
		private static readonly string simpleVisObject = "Editor/Map/SpecialObjects/SimpleRoutePoint/SimpleRoutePoint.(StaticObject).xdb";

		// Token: 0x040004C7 RID: 1223
		private static readonly string complexVisObject = "Editor/Map/SpecialObjects/ComplexRoutePoint/ComplexRoutePoint.(StaticObject).xdb";

		// Token: 0x040004C8 RID: 1224
		private static readonly string tourFolder = "GlobalObjects/Tours/";

		// Token: 0x040004C9 RID: 1225
		private static readonly string routeFolder = "GlobalObjects/Routes/";

		// Token: 0x040004CA RID: 1226
		private static readonly string patrolRouteFolder = "GlobalObjects/ParolRoutes/";

		// Token: 0x040004CB RID: 1227
		private static readonly string tourDBType = "gameMechanics.map.tour.Tour";

		// Token: 0x040004CC RID: 1228
		private static readonly string routeDBType = "gameMechanics.map.tour.Route";

		// Token: 0x040004CD RID: 1229
		private static readonly string routeDBExtention = ".(Route).xdb";

		// Token: 0x040004CE RID: 1230
		private static readonly double defaultTangentLength = 64.0;

		// Token: 0x040004CF RID: 1231
		private string route = string.Empty;

		// Token: 0x040004D0 RID: 1232
		private RoutePointType routePointType = RoutePointType.Simple;

		// Token: 0x040004D1 RID: 1233
		private double speed = 1.0;

		// Token: 0x040004D2 RID: 1234
		private double latency;

		// Token: 0x040004D3 RID: 1235
		private double routeAdvance;

		// Token: 0x040004D4 RID: 1236
		private int routeLinks;

		// Token: 0x040004D5 RID: 1237
		private string label = string.Empty;

		// Token: 0x040004D6 RID: 1238
		private string script = string.Empty;

		// Token: 0x040004D7 RID: 1239
		private double aggroRadius = SpawnPoint.EmptyAggroRadius;

		// Token: 0x02000089 RID: 137
		// (Invoke) Token: 0x0600069F RID: 1695
		public delegate void RoutePointFieldChangedEvent<T>(RoutePoint routePoint, ref T oldValue, ref T newValue);
	}
}
