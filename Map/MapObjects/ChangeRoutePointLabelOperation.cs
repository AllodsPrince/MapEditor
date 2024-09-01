using System;
using Operations;

namespace MapEditor.Map.MapObjects
{
	// Token: 0x0200008E RID: 142
	internal class ChangeRoutePointLabelOperation : IOperation
	{
		// Token: 0x060006CA RID: 1738 RVA: 0x00035C43 File Offset: 0x00034C43
		public ChangeRoutePointLabelOperation(RoutePoint _routePoint, ref string _oldValue, ref string _newValue)
		{
			this.routePoint = _routePoint;
			this.oldValue = _oldValue;
			this.newValue = _newValue;
		}

		// Token: 0x060006CB RID: 1739 RVA: 0x00035C62 File Offset: 0x00034C62
		public bool Undo()
		{
			if (!this.IsEmpty)
			{
				this.routePoint.Label = this.oldValue;
				return true;
			}
			return false;
		}

		// Token: 0x060006CC RID: 1740 RVA: 0x00035C80 File Offset: 0x00034C80
		public bool Redo()
		{
			if (!this.IsEmpty)
			{
				this.routePoint.Label = this.newValue;
				return true;
			}
			return false;
		}

		// Token: 0x060006CD RID: 1741 RVA: 0x00035C9E File Offset: 0x00034C9E
		public void Destroy()
		{
			this.routePoint = null;
		}

		// Token: 0x1700010B RID: 267
		// (get) Token: 0x060006CE RID: 1742 RVA: 0x00035CA7 File Offset: 0x00034CA7
		public bool IsEmpty
		{
			get
			{
				return this.routePoint == null;
			}
		}

		// Token: 0x1700010C RID: 268
		// (get) Token: 0x060006CF RID: 1743 RVA: 0x00035CB2 File Offset: 0x00034CB2
		public bool IsAbsolute
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700010D RID: 269
		// (get) Token: 0x060006D0 RID: 1744 RVA: 0x00035CB5 File Offset: 0x00034CB5
		public bool IsHighPriority
		{
			get
			{
				return false;
			}
		}

		// Token: 0x060006D1 RID: 1745 RVA: 0x00035CB8 File Offset: 0x00034CB8
		public bool ContainsLabel(string label)
		{
			return label == this.Label;
		}

		// Token: 0x1700010E RID: 270
		// (get) Token: 0x060006D2 RID: 1746 RVA: 0x00035CC6 File Offset: 0x00034CC6
		public string Label
		{
			get
			{
				return "MapObjectContainer";
			}
		}

		// Token: 0x1700010F RID: 271
		// (get) Token: 0x060006D3 RID: 1747 RVA: 0x00035CCD File Offset: 0x00034CCD
		public string Description
		{
			get
			{
				return "ChangeRoutePointLabelOperation";
			}
		}

		// Token: 0x040004EB RID: 1259
		private RoutePoint routePoint;

		// Token: 0x040004EC RID: 1260
		private readonly string oldValue;

		// Token: 0x040004ED RID: 1261
		private readonly string newValue;
	}
}
