using System;
using Operations;

namespace MapEditor.Map.MapObjects
{
	// Token: 0x0200008F RID: 143
	internal class ChangeRoutePointScriptOperation : IOperation
	{
		// Token: 0x060006D4 RID: 1748 RVA: 0x00035CD4 File Offset: 0x00034CD4
		public ChangeRoutePointScriptOperation(RoutePoint _routePoint, ref string _oldValue, ref string _newValue)
		{
			this.routePoint = _routePoint;
			this.oldValue = _oldValue;
			this.newValue = _newValue;
		}

		// Token: 0x060006D5 RID: 1749 RVA: 0x00035CF3 File Offset: 0x00034CF3
		public bool Undo()
		{
			if (!this.IsEmpty)
			{
				this.routePoint.Script = this.oldValue;
				return true;
			}
			return false;
		}

		// Token: 0x060006D6 RID: 1750 RVA: 0x00035D11 File Offset: 0x00034D11
		public bool Redo()
		{
			if (!this.IsEmpty)
			{
				this.routePoint.Script = this.newValue;
				return true;
			}
			return false;
		}

		// Token: 0x060006D7 RID: 1751 RVA: 0x00035D2F File Offset: 0x00034D2F
		public void Destroy()
		{
			this.routePoint = null;
		}

		// Token: 0x17000110 RID: 272
		// (get) Token: 0x060006D8 RID: 1752 RVA: 0x00035D38 File Offset: 0x00034D38
		public bool IsEmpty
		{
			get
			{
				return this.routePoint == null;
			}
		}

		// Token: 0x17000111 RID: 273
		// (get) Token: 0x060006D9 RID: 1753 RVA: 0x00035D43 File Offset: 0x00034D43
		public bool IsAbsolute
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000112 RID: 274
		// (get) Token: 0x060006DA RID: 1754 RVA: 0x00035D46 File Offset: 0x00034D46
		public bool IsHighPriority
		{
			get
			{
				return false;
			}
		}

		// Token: 0x060006DB RID: 1755 RVA: 0x00035D49 File Offset: 0x00034D49
		public bool ContainsLabel(string label)
		{
			return label == this.Label;
		}

		// Token: 0x17000113 RID: 275
		// (get) Token: 0x060006DC RID: 1756 RVA: 0x00035D57 File Offset: 0x00034D57
		public string Label
		{
			get
			{
				return "MapObjectContainer";
			}
		}

		// Token: 0x17000114 RID: 276
		// (get) Token: 0x060006DD RID: 1757 RVA: 0x00035D5E File Offset: 0x00034D5E
		public string Description
		{
			get
			{
				return "ChangeRoutePointScriptOperation";
			}
		}

		// Token: 0x040004EE RID: 1262
		private RoutePoint routePoint;

		// Token: 0x040004EF RID: 1263
		private readonly string oldValue;

		// Token: 0x040004F0 RID: 1264
		private readonly string newValue;
	}
}
