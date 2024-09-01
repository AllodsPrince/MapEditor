using System;
using Operations;

namespace MapEditor.Map.MapObjects
{
	// Token: 0x0200008C RID: 140
	internal class ChangeRoutePointSpeedOperation : IOperation
	{
		// Token: 0x060006B6 RID: 1718 RVA: 0x00035B21 File Offset: 0x00034B21
		public ChangeRoutePointSpeedOperation(RoutePoint _routePoint, ref double _oldValue, ref double _newValue)
		{
			this.routePoint = _routePoint;
			this.oldValue = _oldValue;
			this.newValue = _newValue;
		}

		// Token: 0x060006B7 RID: 1719 RVA: 0x00035B40 File Offset: 0x00034B40
		public bool Undo()
		{
			if (!this.IsEmpty)
			{
				this.routePoint.Speed = this.oldValue;
				return true;
			}
			return false;
		}

		// Token: 0x060006B8 RID: 1720 RVA: 0x00035B5E File Offset: 0x00034B5E
		public bool Redo()
		{
			if (!this.IsEmpty)
			{
				this.routePoint.Speed = this.newValue;
				return true;
			}
			return false;
		}

		// Token: 0x060006B9 RID: 1721 RVA: 0x00035B7C File Offset: 0x00034B7C
		public void Destroy()
		{
			this.routePoint = null;
		}

		// Token: 0x17000101 RID: 257
		// (get) Token: 0x060006BA RID: 1722 RVA: 0x00035B85 File Offset: 0x00034B85
		public bool IsHighPriority
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000102 RID: 258
		// (get) Token: 0x060006BB RID: 1723 RVA: 0x00035B88 File Offset: 0x00034B88
		public bool IsEmpty
		{
			get
			{
				return this.routePoint == null;
			}
		}

		// Token: 0x17000103 RID: 259
		// (get) Token: 0x060006BC RID: 1724 RVA: 0x00035B93 File Offset: 0x00034B93
		public bool IsAbsolute
		{
			get
			{
				return false;
			}
		}

		// Token: 0x060006BD RID: 1725 RVA: 0x00035B96 File Offset: 0x00034B96
		public bool ContainsLabel(string label)
		{
			return label == this.Label;
		}

		// Token: 0x17000104 RID: 260
		// (get) Token: 0x060006BE RID: 1726 RVA: 0x00035BA4 File Offset: 0x00034BA4
		public string Label
		{
			get
			{
				return "MapObjectContainer";
			}
		}

		// Token: 0x17000105 RID: 261
		// (get) Token: 0x060006BF RID: 1727 RVA: 0x00035BAB File Offset: 0x00034BAB
		public string Description
		{
			get
			{
				return "ChangeRoutePointSpeedOperation";
			}
		}

		// Token: 0x040004E5 RID: 1253
		private RoutePoint routePoint;

		// Token: 0x040004E6 RID: 1254
		private readonly double oldValue;

		// Token: 0x040004E7 RID: 1255
		private readonly double newValue;
	}
}
