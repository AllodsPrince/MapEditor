using System;
using Tools.MapObjects;

namespace MapEditor.Map.MapObjects
{
	// Token: 0x02000087 RID: 135
	public class RoutePointPack : SerializableMapObjectPack
	{
		// Token: 0x170000DB RID: 219
		// (get) Token: 0x0600064F RID: 1615 RVA: 0x00034ABB File Offset: 0x00033ABB
		// (set) Token: 0x06000650 RID: 1616 RVA: 0x00034AC3 File Offset: 0x00033AC3
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

		// Token: 0x170000DC RID: 220
		// (get) Token: 0x06000651 RID: 1617 RVA: 0x00034ACC File Offset: 0x00033ACC
		// (set) Token: 0x06000652 RID: 1618 RVA: 0x00034AD4 File Offset: 0x00033AD4
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

		// Token: 0x170000DD RID: 221
		// (get) Token: 0x06000653 RID: 1619 RVA: 0x00034ADD File Offset: 0x00033ADD
		// (set) Token: 0x06000654 RID: 1620 RVA: 0x00034AE5 File Offset: 0x00033AE5
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

		// Token: 0x170000DE RID: 222
		// (get) Token: 0x06000655 RID: 1621 RVA: 0x00034AEE File Offset: 0x00033AEE
		// (set) Token: 0x06000656 RID: 1622 RVA: 0x00034AF6 File Offset: 0x00033AF6
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

		// Token: 0x06000657 RID: 1623 RVA: 0x00034B00 File Offset: 0x00033B00
		public override void Pack(IMapObject mapObject)
		{
			base.Pack(mapObject);
			RoutePoint routePoint = mapObject as RoutePoint;
			if (routePoint != null)
			{
				this.route = routePoint.Route;
				this.routePointType = routePoint.RoutePointType;
				this.speed = routePoint.Speed;
				this.latency = routePoint.Latency;
			}
		}

		// Token: 0x06000658 RID: 1624 RVA: 0x00034B50 File Offset: 0x00033B50
		public override void Unpack(IMapObject mapObject)
		{
			base.Unpack(mapObject);
			RoutePoint routePoint = mapObject as RoutePoint;
			if (routePoint != null)
			{
				routePoint.Route = this.route;
				routePoint.RoutePointType = this.routePointType;
				routePoint.Speed = this.speed;
				routePoint.Latency = this.latency;
			}
		}

		// Token: 0x040004BF RID: 1215
		private string route = string.Empty;

		// Token: 0x040004C0 RID: 1216
		private RoutePointType routePointType = RoutePointType.Simple;

		// Token: 0x040004C1 RID: 1217
		private double speed = 1.0;

		// Token: 0x040004C2 RID: 1218
		private double latency;
	}
}
