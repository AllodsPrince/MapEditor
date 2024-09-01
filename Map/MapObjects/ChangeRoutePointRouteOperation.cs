using System;
using Operations;

namespace MapEditor.Map.MapObjects
{
	// Token: 0x0200008A RID: 138
	internal class ChangeRoutePointRouteOperation : IOperation
	{
		// Token: 0x060006A2 RID: 1698 RVA: 0x000359FF File Offset: 0x000349FF
		public ChangeRoutePointRouteOperation(RoutePoint _routePoint, ref string _oldValue, ref string _newValue)
		{
			this.routePoint = _routePoint;
			this.oldValue = _oldValue;
			this.newValue = _newValue;
		}

		// Token: 0x060006A3 RID: 1699 RVA: 0x00035A1E File Offset: 0x00034A1E
		public bool Undo()
		{
			if (!this.IsEmpty)
			{
				this.routePoint.Route = this.oldValue;
				return true;
			}
			return false;
		}

		// Token: 0x060006A4 RID: 1700 RVA: 0x00035A3C File Offset: 0x00034A3C
		public bool Redo()
		{
			if (!this.IsEmpty)
			{
				this.routePoint.Route = this.newValue;
				return true;
			}
			return false;
		}

		// Token: 0x060006A5 RID: 1701 RVA: 0x00035A5A File Offset: 0x00034A5A
		public void Destroy()
		{
			this.routePoint = null;
		}

		// Token: 0x170000F7 RID: 247
		// (get) Token: 0x060006A6 RID: 1702 RVA: 0x00035A63 File Offset: 0x00034A63
		public bool IsEmpty
		{
			get
			{
				return this.routePoint == null;
			}
		}

		// Token: 0x170000F8 RID: 248
		// (get) Token: 0x060006A7 RID: 1703 RVA: 0x00035A6E File Offset: 0x00034A6E
		public bool IsAbsolute
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170000F9 RID: 249
		// (get) Token: 0x060006A8 RID: 1704 RVA: 0x00035A71 File Offset: 0x00034A71
		public bool IsHighPriority
		{
			get
			{
				return false;
			}
		}

		// Token: 0x060006A9 RID: 1705 RVA: 0x00035A74 File Offset: 0x00034A74
		public bool ContainsLabel(string label)
		{
			return label == this.Label;
		}

		// Token: 0x170000FA RID: 250
		// (get) Token: 0x060006AA RID: 1706 RVA: 0x00035A82 File Offset: 0x00034A82
		public string Label
		{
			get
			{
				return "MapObjectContainer";
			}
		}

		// Token: 0x170000FB RID: 251
		// (get) Token: 0x060006AB RID: 1707 RVA: 0x00035A89 File Offset: 0x00034A89
		public string Description
		{
			get
			{
				return "ChangeRoutePointRouteOperation";
			}
		}

		// Token: 0x040004DF RID: 1247
		private RoutePoint routePoint;

		// Token: 0x040004E0 RID: 1248
		private readonly string oldValue;

		// Token: 0x040004E1 RID: 1249
		private readonly string newValue;
	}
}
