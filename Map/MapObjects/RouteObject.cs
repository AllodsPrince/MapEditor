using System;
using System.Collections.Generic;
using System.Drawing;
using MapEditor.Map.Containers;
using MapEditor.Resources.Strings;
using Tools.Geometry;
using Tools.MapObjects;
using Win32;

namespace MapEditor.Map.MapObjects
{
	// Token: 0x020000D4 RID: 212
	public class RouteObject : MapObject, IMapObjectInterfaceExtention
	{
		// Token: 0x1400002F RID: 47
		// (add) Token: 0x06000ACE RID: 2766 RVA: 0x0005969E File Offset: 0x0005869E
		// (remove) Token: 0x06000ACF RID: 2767 RVA: 0x000596B5 File Offset: 0x000586B5
		public static event RouteObject.RouteObjectChangedEvent Created;

		// Token: 0x14000030 RID: 48
		// (add) Token: 0x06000AD0 RID: 2768 RVA: 0x000596CC File Offset: 0x000586CC
		// (remove) Token: 0x06000AD1 RID: 2769 RVA: 0x000596E3 File Offset: 0x000586E3
		public static event RouteObject.RouteObjectChangedEvent Destroyed;

		// Token: 0x14000031 RID: 49
		// (add) Token: 0x06000AD2 RID: 2770 RVA: 0x000596FA File Offset: 0x000586FA
		// (remove) Token: 0x06000AD3 RID: 2771 RVA: 0x00059711 File Offset: 0x00058711
		public static event RouteObject.RouteObjectChangedEvent StatsChanged;

		// Token: 0x06000AD4 RID: 2772 RVA: 0x00059728 File Offset: 0x00058728
		public RouteObject(int _id, MapObjectType _type, ICollisionMap _collisionMap) : base(_id, _type, _collisionMap)
		{
		}

		// Token: 0x17000227 RID: 551
		// (get) Token: 0x06000AD5 RID: 2773 RVA: 0x00059787 File Offset: 0x00058787
		public static Color InterfaceColor
		{
			get
			{
				return RouteObject.interfaceColor;
			}
		}

		// Token: 0x17000228 RID: 552
		// (get) Token: 0x06000AD6 RID: 2774 RVA: 0x0005978E File Offset: 0x0005878E
		public static string InterfaceSingleObjectTypeName
		{
			get
			{
				return RouteObject.interfaceSingleObjectTypeName;
			}
		}

		// Token: 0x17000229 RID: 553
		// (get) Token: 0x06000AD7 RID: 2775 RVA: 0x00059795 File Offset: 0x00058795
		public static string InterfaceSeveralObjectsTypeName
		{
			get
			{
				return RouteObject.interfaceSeveralObjectsTypeName;
			}
		}

		// Token: 0x1700022A RID: 554
		// (get) Token: 0x06000AD8 RID: 2776 RVA: 0x0005979C File Offset: 0x0005879C
		public static double DefaultTime
		{
			get
			{
				return RouteObject.defaultTime;
			}
		}

		// Token: 0x1700022B RID: 555
		// (get) Token: 0x06000AD9 RID: 2777 RVA: 0x000597A3 File Offset: 0x000587A3
		public static double DefaultSpeed
		{
			get
			{
				return RouteObject.defaultSpeed;
			}
		}

		// Token: 0x1700022C RID: 556
		// (get) Token: 0x06000ADA RID: 2778 RVA: 0x000597AA File Offset: 0x000587AA
		public static bool DefaultRun
		{
			get
			{
				return RouteObject.defaultRun;
			}
		}

		// Token: 0x06000ADB RID: 2779 RVA: 0x000597B4 File Offset: 0x000587B4
		public override IMapObject Clone(int newID, bool newTemporary, bool newActive)
		{
			RouteObject mapObject = new RouteObject(newID, new MapObjectType(base.Type.Type, base.Type.Stats), base.CollisionMap);
			base.CopyTo(mapObject, newTemporary, newActive);
			return mapObject;
		}

		// Token: 0x06000ADC RID: 2780 RVA: 0x000597F9 File Offset: 0x000587F9
		private void Start()
		{
			this.run = true;
			this.startTickCount = Kernel.GetTickCount();
			this.startTime = this.time;
		}

		// Token: 0x06000ADD RID: 2781 RVA: 0x00059819 File Offset: 0x00058819
		private void Stop()
		{
			this.run = false;
		}

		// Token: 0x06000ADE RID: 2782 RVA: 0x00059822 File Offset: 0x00058822
		public void Create(string routeName, List<RoutePoint> routePoints, bool circle)
		{
			this.Create(routeName, routePoints, circle, RouteObject.defaultTime, true, RouteObject.defaultSpeed, RouteObject.defaultRun);
		}

		// Token: 0x06000ADF RID: 2783 RVA: 0x00059840 File Offset: 0x00058840
		public void Create(string routeName, List<RoutePoint> routePoints, bool circle, double param, bool paramIsTime, double _speed, bool _run)
		{
			this.Stop();
			this.splinePath.Nodes.Clear();
			if (routePoints != null && routePoints.Count > 1)
			{
				this.routeSpeed = routePoints[0].Speed;
				bool reverse = !RoutePoint.RouteAdvanceIsDirect(routePoints[0], routePoints[1]);
				foreach (RoutePoint routePoint in routePoints)
				{
					SplinePath.SplineNode node = new SplinePath.SplineNode(routePoint.Position.Vec3, routePoint.Tangent);
					if (reverse)
					{
						this.splinePath.Nodes.Insert(0, node);
					}
					else
					{
						this.splinePath.Nodes.Add(node);
					}
				}
				this.route = routeName;
				this.splinePath.Circle = circle;
				this.splinePath.Update();
				string newRouteObject = RoutePoint.GetRouteObject(routePoints[0].Route, out this.rotate);
				bool statsChanged = !string.Equals(base.Type.Stats, newRouteObject, StringComparison.OrdinalIgnoreCase);
				if (statsChanged)
				{
					base.SetNewStats(RoutePoint.GetRouteObject(routePoints[0].Route, out this.rotate));
				}
				if (paramIsTime)
				{
					this.time = param;
				}
				else
				{
					this.time = this.GetTimeByPoint(param);
				}
				this.speed = _speed;
				if (RouteObject.Created != null)
				{
					RouteObject.Created(this);
				}
				if (statsChanged && RouteObject.StatsChanged != null)
				{
					RouteObject.StatsChanged(this);
				}
				if (_run)
				{
					this.Start();
				}
			}
		}

		// Token: 0x06000AE0 RID: 2784 RVA: 0x000599E4 File Offset: 0x000589E4
		public void Destroy()
		{
			this.Stop();
			this.splinePath.Nodes.Clear();
			if (!string.IsNullOrEmpty(base.Type.Stats))
			{
				base.SetNewStats(string.Empty);
				if (RouteObject.StatsChanged != null)
				{
					RouteObject.StatsChanged(this);
				}
			}
			if (RouteObject.Destroyed != null)
			{
				RouteObject.Destroyed(this);
			}
		}

		// Token: 0x1700022D RID: 557
		// (get) Token: 0x06000AE1 RID: 2785 RVA: 0x00059A4B File Offset: 0x00058A4B
		// (set) Token: 0x06000AE2 RID: 2786 RVA: 0x00059A53 File Offset: 0x00058A53
		public bool Run
		{
			get
			{
				return this.run;
			}
			set
			{
				if (this.run != value)
				{
					if (value)
					{
						this.Start();
						return;
					}
					this.Stop();
				}
			}
		}

		// Token: 0x06000AE3 RID: 2787 RVA: 0x00059A6E File Offset: 0x00058A6E
		public void Reset()
		{
			this.time = 0.0;
		}

		// Token: 0x06000AE4 RID: 2788 RVA: 0x00059A80 File Offset: 0x00058A80
		public void Step(MapEditorMapObjectContainer mapEditorMapObjectContainer)
		{
			if (this.splinePath.Nodes.Count > 0 && !string.IsNullOrEmpty(this.SceneName))
			{
				if (this.run)
				{
					this.time = this.startTime + (double)(Kernel.GetTickCount() - this.startTickCount) / 1000.0 * this.speed;
				}
				Vec3 _position;
				Vec3 _tangent;
				this.splinePath.GetSplineParams(this.time, out _position, out _tangent);
				base.Position = new Position(_position.X, _position.Y, _position.Z);
				if (this.rotate)
				{
					Quat _quat = new Quat(_tangent);
					_quat *= RouteObject.additionalRotation;
					base.Rotation = new Rotation(ref _quat);
				}
			}
		}

		// Token: 0x06000AE5 RID: 2789 RVA: 0x00059B4A File Offset: 0x00058B4A
		public double GetPointByTime(double _time)
		{
			return this.splinePath.GetPoint(_time);
		}

		// Token: 0x06000AE6 RID: 2790 RVA: 0x00059B58 File Offset: 0x00058B58
		public double GetTimeByPoint(double _point)
		{
			return this.splinePath.GetTime(_point);
		}

		// Token: 0x1700022E RID: 558
		// (get) Token: 0x06000AE7 RID: 2791 RVA: 0x00059B66 File Offset: 0x00058B66
		// (set) Token: 0x06000AE8 RID: 2792 RVA: 0x00059B70 File Offset: 0x00058B70
		public double Time
		{
			get
			{
				return this.time;
			}
			set
			{
				bool _run = this.run;
				this.Stop();
				this.time = value;
				if (_run)
				{
					this.Start();
				}
			}
		}

		// Token: 0x1700022F RID: 559
		// (get) Token: 0x06000AE9 RID: 2793 RVA: 0x00059B9A File Offset: 0x00058B9A
		public double Length
		{
			get
			{
				return this.splinePath.Length;
			}
		}

		// Token: 0x17000230 RID: 560
		// (get) Token: 0x06000AEA RID: 2794 RVA: 0x00059BA7 File Offset: 0x00058BA7
		public bool Circle
		{
			get
			{
				return this.splinePath.Circle;
			}
		}

		// Token: 0x17000231 RID: 561
		// (get) Token: 0x06000AEB RID: 2795 RVA: 0x00059BB4 File Offset: 0x00058BB4
		// (set) Token: 0x06000AEC RID: 2796 RVA: 0x00059BBC File Offset: 0x00058BBC
		public double Speed
		{
			get
			{
				return this.speed;
			}
			set
			{
				bool _run = this.run;
				this.Stop();
				this.speed = value;
				if (_run)
				{
					this.Start();
				}
			}
		}

		// Token: 0x17000232 RID: 562
		// (get) Token: 0x06000AED RID: 2797 RVA: 0x00059BE6 File Offset: 0x00058BE6
		public double RouteSpeed
		{
			get
			{
				return this.routeSpeed;
			}
		}

		// Token: 0x17000233 RID: 563
		// (get) Token: 0x06000AEE RID: 2798 RVA: 0x00059BEE File Offset: 0x00058BEE
		// (set) Token: 0x06000AEF RID: 2799 RVA: 0x00059BF6 File Offset: 0x00058BF6
		public string Route
		{
			get
			{
				return this.route;
			}
			set
			{
				this.route = value;
			}
		}

		// Token: 0x17000234 RID: 564
		// (get) Token: 0x06000AF0 RID: 2800 RVA: 0x00059BFF File Offset: 0x00058BFF
		public int Count
		{
			get
			{
				return this.splinePath.Nodes.Count;
			}
		}

		// Token: 0x06000AF1 RID: 2801 RVA: 0x00059C14 File Offset: 0x00058C14
		public bool GetSplinePoint(double _time, out Position _postion)
		{
			if (this.splinePath.Nodes.Count > 0)
			{
				Vec3 _position;
				Vec3 _tangent;
				this.splinePath.GetSplineParams(_time, out _position, out _tangent);
				_postion = new Position(_position.X, _position.Y, _position.Z);
				return true;
			}
			_postion = Position.Empty;
			return false;
		}

		// Token: 0x06000AF2 RID: 2802 RVA: 0x00059C74 File Offset: 0x00058C74
		public bool GetSplineParams(double _time, out Position _postion, out Rotation _rotation)
		{
			if (this.splinePath.Nodes.Count > 0)
			{
				Vec3 _position;
				Vec3 _tangent;
				this.splinePath.GetSplineParams(_time, out _position, out _tangent);
				_postion = new Position(_position.X, _position.Y, _position.Z);
				Quat _quat = new Quat(_tangent);
				_rotation = new Rotation(ref _quat);
				return true;
			}
			_postion = Position.Empty;
			_rotation = Rotation.Empty;
			return false;
		}

		// Token: 0x06000AF3 RID: 2803 RVA: 0x00059CF8 File Offset: 0x00058CF8
		public Color GetInterfaceColor()
		{
			return RouteObject.interfaceColor;
		}

		// Token: 0x06000AF4 RID: 2804 RVA: 0x00059CFF File Offset: 0x00058CFF
		public string GetInterfaceSingleObjectTypeName()
		{
			return RouteObject.interfaceSingleObjectTypeName;
		}

		// Token: 0x06000AF5 RID: 2805 RVA: 0x00059D06 File Offset: 0x00058D06
		public string GetInterfaceSeveralObjectsTypeName()
		{
			return RouteObject.interfaceSeveralObjectsTypeName;
		}

		// Token: 0x06000AF6 RID: 2806 RVA: 0x00059D10 File Offset: 0x00058D10
		public bool ContainsText(string text, bool ignoreCase)
		{
			if (string.IsNullOrEmpty(text))
			{
				return true;
			}
			if (string.IsNullOrEmpty(this.SceneName))
			{
				return false;
			}
			if (ignoreCase)
			{
				return this.SceneName.ToLower().Contains(text.ToLower());
			}
			return this.SceneName.Contains(text);
		}

		// Token: 0x06000AF7 RID: 2807 RVA: 0x00059D5C File Offset: 0x00058D5C
		public string GetStatsForDBBrowse()
		{
			return string.Empty;
		}

		// Token: 0x06000AF8 RID: 2808 RVA: 0x00059D63 File Offset: 0x00058D63
		public string GetSpecialStatsForDBBrowse()
		{
			return string.Empty;
		}

		// Token: 0x0400083E RID: 2110
		private static readonly Quat additionalRotation = new Quat(0.0, 0.0, MathConsts.SQRT_2 / 2.0, MathConsts.SQRT_2 / 2.0);

		// Token: 0x0400083F RID: 2111
		private static readonly Color interfaceColor = Color.FromArgb(MapObjectCreationInfo.DefaultTransparentColorAlpha, Color.Black);

		// Token: 0x04000840 RID: 2112
		private static readonly string interfaceSingleObjectTypeName = Strings.SINGLE_ROUTE_OBJECT_TYPE_NAME;

		// Token: 0x04000841 RID: 2113
		private static readonly string interfaceSeveralObjectsTypeName = Strings.SEVERAL_ROUTE_OBJECTS_TYPE_NAME;

		// Token: 0x04000842 RID: 2114
		private static readonly double defaultTime = 0.0;

		// Token: 0x04000843 RID: 2115
		private static readonly double defaultSpeed = 10.0;

		// Token: 0x04000844 RID: 2116
		private static readonly bool defaultRun = true;

		// Token: 0x04000845 RID: 2117
		private readonly SplinePath splinePath = new SplinePath();

		// Token: 0x04000846 RID: 2118
		private string route = string.Empty;

		// Token: 0x04000847 RID: 2119
		private double time = RouteObject.defaultTime;

		// Token: 0x04000848 RID: 2120
		private double speed = RouteObject.defaultSpeed;

		// Token: 0x04000849 RID: 2121
		private bool run = RouteObject.defaultRun;

		// Token: 0x0400084A RID: 2122
		private bool rotate = true;

		// Token: 0x0400084B RID: 2123
		private int startTickCount;

		// Token: 0x0400084C RID: 2124
		private double startTime;

		// Token: 0x0400084D RID: 2125
		private double routeSpeed = RouteObject.defaultSpeed;

		// Token: 0x020000D5 RID: 213
		// (Invoke) Token: 0x06000AFB RID: 2811
		public delegate void RouteObjectChangedEvent(RouteObject routeObject);
	}
}
