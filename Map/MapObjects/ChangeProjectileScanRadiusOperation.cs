using System;
using Operations;

namespace MapEditor.Map.MapObjects
{
	// Token: 0x0200024D RID: 589
	internal class ChangeProjectileScanRadiusOperation : IOperation
	{
		// Token: 0x06001C1D RID: 7197 RVA: 0x000B5EE7 File Offset: 0x000B4EE7
		public ChangeProjectileScanRadiusOperation(Projectile _projectile, ref double _oldValue, ref double _newValue)
		{
			this.projectile = _projectile;
			this.oldValue = _oldValue;
			this.newValue = _newValue;
		}

		// Token: 0x06001C1E RID: 7198 RVA: 0x000B5F06 File Offset: 0x000B4F06
		public bool Undo()
		{
			if (!this.IsEmpty)
			{
				this.projectile.ScanRadius = this.oldValue;
				return true;
			}
			return false;
		}

		// Token: 0x06001C1F RID: 7199 RVA: 0x000B5F24 File Offset: 0x000B4F24
		public bool Redo()
		{
			if (!this.IsEmpty)
			{
				this.projectile.ScanRadius = this.newValue;
				return true;
			}
			return false;
		}

		// Token: 0x06001C20 RID: 7200 RVA: 0x000B5F42 File Offset: 0x000B4F42
		public void Destroy()
		{
			this.projectile = null;
		}

		// Token: 0x17000699 RID: 1689
		// (get) Token: 0x06001C21 RID: 7201 RVA: 0x000B5F4B File Offset: 0x000B4F4B
		public bool IsHighPriority
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700069A RID: 1690
		// (get) Token: 0x06001C22 RID: 7202 RVA: 0x000B5F4E File Offset: 0x000B4F4E
		public bool IsEmpty
		{
			get
			{
				return this.projectile == null;
			}
		}

		// Token: 0x1700069B RID: 1691
		// (get) Token: 0x06001C23 RID: 7203 RVA: 0x000B5F59 File Offset: 0x000B4F59
		public bool IsAbsolute
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06001C24 RID: 7204 RVA: 0x000B5F5C File Offset: 0x000B4F5C
		public bool ContainsLabel(string label)
		{
			return label == this.Label;
		}

		// Token: 0x1700069C RID: 1692
		// (get) Token: 0x06001C25 RID: 7205 RVA: 0x000B5F6A File Offset: 0x000B4F6A
		public string Label
		{
			get
			{
				return "MapObjectContainer";
			}
		}

		// Token: 0x1700069D RID: 1693
		// (get) Token: 0x06001C26 RID: 7206 RVA: 0x000B5F71 File Offset: 0x000B4F71
		public string Description
		{
			get
			{
				return "ChangeProjectileScanRadiusOperation";
			}
		}

		// Token: 0x0400121D RID: 4637
		private Projectile projectile;

		// Token: 0x0400121E RID: 4638
		private readonly double oldValue;

		// Token: 0x0400121F RID: 4639
		private readonly double newValue;
	}
}
