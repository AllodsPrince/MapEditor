using System;
using Operations;

namespace MapEditor.Map.MapObjects
{
	// Token: 0x0200008D RID: 141
	internal class ChangeRoutePointLatencyOperation : IOperation
	{
		// Token: 0x060006C0 RID: 1728 RVA: 0x00035BB2 File Offset: 0x00034BB2
		public ChangeRoutePointLatencyOperation(RoutePoint _routePoint, ref double _oldValue, ref double _newValue)
		{
			this.routePoint = _routePoint;
			this.oldValue = _oldValue;
			this.newValue = _newValue;
		}

		// Token: 0x060006C1 RID: 1729 RVA: 0x00035BD1 File Offset: 0x00034BD1
		public bool Undo()
		{
			if (!this.IsEmpty)
			{
				this.routePoint.Latency = this.oldValue;
				return true;
			}
			return false;
		}

		// Token: 0x060006C2 RID: 1730 RVA: 0x00035BEF File Offset: 0x00034BEF
		public bool Redo()
		{
			if (!this.IsEmpty)
			{
				this.routePoint.Latency = this.newValue;
				return true;
			}
			return false;
		}

		// Token: 0x060006C3 RID: 1731 RVA: 0x00035C0D File Offset: 0x00034C0D
		public void Destroy()
		{
			this.routePoint = null;
		}

		// Token: 0x17000106 RID: 262
		// (get) Token: 0x060006C4 RID: 1732 RVA: 0x00035C16 File Offset: 0x00034C16
		public bool IsEmpty
		{
			get
			{
				return this.routePoint == null;
			}
		}

		// Token: 0x17000107 RID: 263
		// (get) Token: 0x060006C5 RID: 1733 RVA: 0x00035C21 File Offset: 0x00034C21
		public bool IsAbsolute
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000108 RID: 264
		// (get) Token: 0x060006C6 RID: 1734 RVA: 0x00035C24 File Offset: 0x00034C24
		public bool IsHighPriority
		{
			get
			{
				return false;
			}
		}

		// Token: 0x060006C7 RID: 1735 RVA: 0x00035C27 File Offset: 0x00034C27
		public bool ContainsLabel(string label)
		{
			return label == this.Label;
		}

		// Token: 0x17000109 RID: 265
		// (get) Token: 0x060006C8 RID: 1736 RVA: 0x00035C35 File Offset: 0x00034C35
		public string Label
		{
			get
			{
				return "MapObjectContainer";
			}
		}

		// Token: 0x1700010A RID: 266
		// (get) Token: 0x060006C9 RID: 1737 RVA: 0x00035C3C File Offset: 0x00034C3C
		public string Description
		{
			get
			{
				return "ChangeRoutePointLatencyOperation";
			}
		}

		// Token: 0x040004E8 RID: 1256
		private RoutePoint routePoint;

		// Token: 0x040004E9 RID: 1257
		private readonly double oldValue;

		// Token: 0x040004EA RID: 1258
		private readonly double newValue;
	}
}
