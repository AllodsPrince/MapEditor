using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Xml.Serialization;
using Tools.Geometry;
using Tools.MapObjects;

namespace MapEditor.Map.MapObjects
{
	// Token: 0x02000083 RID: 131
	public class RouteSaveData
	{
		// Token: 0x170000D1 RID: 209
		// (get) Token: 0x06000636 RID: 1590 RVA: 0x0003485B File Offset: 0x0003385B
		// (set) Token: 0x06000637 RID: 1591 RVA: 0x00034863 File Offset: 0x00033863
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

		// Token: 0x170000D2 RID: 210
		// (get) Token: 0x06000638 RID: 1592 RVA: 0x0003486C File Offset: 0x0003386C
		// (set) Token: 0x06000639 RID: 1593 RVA: 0x00034874 File Offset: 0x00033874
		public bool Circle
		{
			get
			{
				return this.circle;
			}
			set
			{
				this.circle = value;
			}
		}

		// Token: 0x170000D3 RID: 211
		// (get) Token: 0x0600063A RID: 1594 RVA: 0x0003487D File Offset: 0x0003387D
		// (set) Token: 0x0600063B RID: 1595 RVA: 0x00034885 File Offset: 0x00033885
		public RoutePointType RoutePointType
		{
			get
			{
				return this.routePointType;
			}
			set
			{
				this.routePointType = value;
			}
		}

		// Token: 0x170000D4 RID: 212
		// (get) Token: 0x0600063C RID: 1596 RVA: 0x0003488E File Offset: 0x0003388E
		// (set) Token: 0x0600063D RID: 1597 RVA: 0x00034896 File Offset: 0x00033896
		public List<RouteSaveData.RoutePointSaveData> RoutePoints
		{
			get
			{
				return this.routePoints;
			}
			set
			{
				this.routePoints = value;
			}
		}

		// Token: 0x040004B2 RID: 1202
		private string route = string.Empty;

		// Token: 0x040004B3 RID: 1203
		private bool circle;

		// Token: 0x040004B4 RID: 1204
		private RoutePointType routePointType = RoutePointType.Simple;

		// Token: 0x040004B5 RID: 1205
		private List<RouteSaveData.RoutePointSaveData> routePoints = new List<RouteSaveData.RoutePointSaveData>();

		// Token: 0x02000084 RID: 132
		public class RoutePointSaveData
		{
			// Token: 0x170000D5 RID: 213
			// (get) Token: 0x0600063F RID: 1599 RVA: 0x000348C4 File Offset: 0x000338C4
			// (set) Token: 0x06000640 RID: 1600 RVA: 0x000348CC File Offset: 0x000338CC
			public Position Position
			{
				get
				{
					return this.position;
				}
				set
				{
					this.position = value;
				}
			}

			// Token: 0x170000D6 RID: 214
			// (get) Token: 0x06000641 RID: 1601 RVA: 0x000348D8 File Offset: 0x000338D8
			// (set) Token: 0x06000642 RID: 1602 RVA: 0x00034968 File Offset: 0x00033968
			[XmlIgnore]
			[Browsable(false)]
			public Vec3 Tangent
			{
				get
				{
					Quat quat = new Quat((double)this.Rotation.Yaw, (double)this.Rotation.Pitch, (double)this.Rotation.Roll);
					Vec3 tangent = new Vec3(1.0, 0.0, 0.0);
					tangent = quat.Rotate(tangent);
					tangent = tangent * (double)this.Scale.Ratio * RoutePoint.DefaultTangentLength;
					return tangent;
				}
				set
				{
					Quat quat = new Quat(value);
					this.Rotation = new Rotation(ref quat);
					this.Scale = new Scale((float)(value.Length / RoutePoint.DefaultTangentLength));
				}
			}

			// Token: 0x170000D7 RID: 215
			// (get) Token: 0x06000643 RID: 1603 RVA: 0x000349A8 File Offset: 0x000339A8
			// (set) Token: 0x06000644 RID: 1604 RVA: 0x000349B0 File Offset: 0x000339B0
			public Rotation Rotation
			{
				get
				{
					return this.rotation;
				}
				set
				{
					this.rotation = value;
				}
			}

			// Token: 0x170000D8 RID: 216
			// (get) Token: 0x06000645 RID: 1605 RVA: 0x000349B9 File Offset: 0x000339B9
			// (set) Token: 0x06000646 RID: 1606 RVA: 0x000349C1 File Offset: 0x000339C1
			public Scale Scale
			{
				get
				{
					return this.scale;
				}
				set
				{
					this.scale = value;
				}
			}

			// Token: 0x170000D9 RID: 217
			// (get) Token: 0x06000647 RID: 1607 RVA: 0x000349CA File Offset: 0x000339CA
			// (set) Token: 0x06000648 RID: 1608 RVA: 0x000349D2 File Offset: 0x000339D2
			public double Speed
			{
				get
				{
					return this.speed;
				}
				set
				{
					this.speed = value;
				}
			}

			// Token: 0x170000DA RID: 218
			// (get) Token: 0x06000649 RID: 1609 RVA: 0x000349DB File Offset: 0x000339DB
			// (set) Token: 0x0600064A RID: 1610 RVA: 0x000349E3 File Offset: 0x000339E3
			public double Latency
			{
				get
				{
					return this.latency;
				}
				set
				{
					this.latency = value;
				}
			}

			// Token: 0x040004B6 RID: 1206
			private Position position = Position.Empty;

			// Token: 0x040004B7 RID: 1207
			private Rotation rotation = Rotation.Empty;

			// Token: 0x040004B8 RID: 1208
			private Scale scale = Scale.Normal;

			// Token: 0x040004B9 RID: 1209
			private double speed = 1.0;

			// Token: 0x040004BA RID: 1210
			private double latency;
		}
	}
}
