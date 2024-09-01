using System;
using Operations;

namespace MapEditor.Map.MapObjects
{
	// Token: 0x0200008B RID: 139
	internal class ChangeRoutePointTypeOperation : IOperation
	{
		// Token: 0x060006AC RID: 1708 RVA: 0x00035A90 File Offset: 0x00034A90
		public ChangeRoutePointTypeOperation(RoutePoint _routePoint, ref RoutePointType _oldValue, ref RoutePointType _newValue)
		{
			this.routePoint = _routePoint;
			this.oldValue = _oldValue;
			this.newValue = _newValue;
		}

		// Token: 0x060006AD RID: 1709 RVA: 0x00035AAF File Offset: 0x00034AAF
		public bool Undo()
		{
			if (!this.IsEmpty)
			{
				this.routePoint.RoutePointType = this.oldValue;
				return true;
			}
			return false;
		}

		// Token: 0x060006AE RID: 1710 RVA: 0x00035ACD File Offset: 0x00034ACD
		public bool Redo()
		{
			if (!this.IsEmpty)
			{
				this.routePoint.RoutePointType = this.newValue;
				return true;
			}
			return false;
		}

		// Token: 0x060006AF RID: 1711 RVA: 0x00035AEB File Offset: 0x00034AEB
		public void Destroy()
		{
			this.routePoint = null;
		}

		// Token: 0x170000FC RID: 252
		// (get) Token: 0x060006B0 RID: 1712 RVA: 0x00035AF4 File Offset: 0x00034AF4
		public bool IsHighPriority
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170000FD RID: 253
		// (get) Token: 0x060006B1 RID: 1713 RVA: 0x00035AF7 File Offset: 0x00034AF7
		public bool IsEmpty
		{
			get
			{
				return this.routePoint == null;
			}
		}

		// Token: 0x170000FE RID: 254
		// (get) Token: 0x060006B2 RID: 1714 RVA: 0x00035B02 File Offset: 0x00034B02
		public bool IsAbsolute
		{
			get
			{
				return false;
			}
		}

		// Token: 0x060006B3 RID: 1715 RVA: 0x00035B05 File Offset: 0x00034B05
		public bool ContainsLabel(string label)
		{
			return label == this.Label;
		}

		// Token: 0x170000FF RID: 255
		// (get) Token: 0x060006B4 RID: 1716 RVA: 0x00035B13 File Offset: 0x00034B13
		public string Label
		{
			get
			{
				return "MapObjectContainer";
			}
		}

		// Token: 0x17000100 RID: 256
		// (get) Token: 0x060006B5 RID: 1717 RVA: 0x00035B1A File Offset: 0x00034B1A
		public string Description
		{
			get
			{
				return "ChangeRoutePointTypeOperation";
			}
		}

		// Token: 0x040004E2 RID: 1250
		private RoutePoint routePoint;

		// Token: 0x040004E3 RID: 1251
		private readonly RoutePointType oldValue;

		// Token: 0x040004E4 RID: 1252
		private readonly RoutePointType newValue;
	}
}
