using System;
using System.Collections.Generic;

namespace MapEditor.Map.MapObjects
{
	// Token: 0x02000082 RID: 130
	public class RouteData
	{
		// Token: 0x170000CE RID: 206
		// (get) Token: 0x06000630 RID: 1584 RVA: 0x00034813 File Offset: 0x00033813
		// (set) Token: 0x06000631 RID: 1585 RVA: 0x0003481B File Offset: 0x0003381B
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

		// Token: 0x170000CF RID: 207
		// (get) Token: 0x06000632 RID: 1586 RVA: 0x00034824 File Offset: 0x00033824
		// (set) Token: 0x06000633 RID: 1587 RVA: 0x0003482C File Offset: 0x0003382C
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

		// Token: 0x170000D0 RID: 208
		// (get) Token: 0x06000634 RID: 1588 RVA: 0x00034835 File Offset: 0x00033835
		public List<RoutePoint> RoutePoints
		{
			get
			{
				return this.routePoints;
			}
		}

		// Token: 0x040004AF RID: 1199
		private string route = string.Empty;

		// Token: 0x040004B0 RID: 1200
		private bool circle;

		// Token: 0x040004B1 RID: 1201
		private readonly List<RoutePoint> routePoints = new List<RoutePoint>();
	}
}
