using System;
using Operations;

namespace MapEditor.Map.MapObjects
{
	// Token: 0x02000061 RID: 97
	internal class ChangeMapLocatorScanRadiusOperation : IOperation
	{
		// Token: 0x060004F5 RID: 1269 RVA: 0x00028298 File Offset: 0x00027298
		public ChangeMapLocatorScanRadiusOperation(MapLocator _mapLocator, ref double _oldValue, ref double _newValue)
		{
			this.mapLocator = _mapLocator;
			this.oldValue = _oldValue;
			this.newValue = _newValue;
		}

		// Token: 0x060004F6 RID: 1270 RVA: 0x000282B7 File Offset: 0x000272B7
		public bool Undo()
		{
			if (!this.IsEmpty)
			{
				this.mapLocator.ScanRadius = this.oldValue;
				return true;
			}
			return false;
		}

		// Token: 0x060004F7 RID: 1271 RVA: 0x000282D5 File Offset: 0x000272D5
		public bool Redo()
		{
			if (!this.IsEmpty)
			{
				this.mapLocator.ScanRadius = this.newValue;
				return true;
			}
			return false;
		}

		// Token: 0x060004F8 RID: 1272 RVA: 0x000282F3 File Offset: 0x000272F3
		public void Destroy()
		{
			this.mapLocator = null;
		}

		// Token: 0x170000BA RID: 186
		// (get) Token: 0x060004F9 RID: 1273 RVA: 0x000282FC File Offset: 0x000272FC
		public bool IsEmpty
		{
			get
			{
				return this.mapLocator == null;
			}
		}

		// Token: 0x170000BB RID: 187
		// (get) Token: 0x060004FA RID: 1274 RVA: 0x00028307 File Offset: 0x00027307
		public bool IsAbsolute
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170000BC RID: 188
		// (get) Token: 0x060004FB RID: 1275 RVA: 0x0002830A File Offset: 0x0002730A
		public bool IsHighPriority
		{
			get
			{
				return false;
			}
		}

		// Token: 0x060004FC RID: 1276 RVA: 0x0002830D File Offset: 0x0002730D
		public bool ContainsLabel(string label)
		{
			return label == this.Label;
		}

		// Token: 0x170000BD RID: 189
		// (get) Token: 0x060004FD RID: 1277 RVA: 0x0002831B File Offset: 0x0002731B
		public string Label
		{
			get
			{
				return "MapObjectContainer";
			}
		}

		// Token: 0x170000BE RID: 190
		// (get) Token: 0x060004FE RID: 1278 RVA: 0x00028322 File Offset: 0x00027322
		public string Description
		{
			get
			{
				return "ChangeMapLocatorScanRadiusOperation";
			}
		}

		// Token: 0x040003A3 RID: 931
		private MapLocator mapLocator;

		// Token: 0x040003A4 RID: 932
		private readonly double oldValue;

		// Token: 0x040003A5 RID: 933
		private readonly double newValue;
	}
}
