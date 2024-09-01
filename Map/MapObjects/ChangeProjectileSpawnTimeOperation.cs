using System;
using MapEditor.Map.MapObjectElements;
using Operations;

namespace MapEditor.Map.MapObjects
{
	// Token: 0x0200024E RID: 590
	internal class ChangeProjectileSpawnTimeOperation : IOperation
	{
		// Token: 0x06001C27 RID: 7207 RVA: 0x000B5F78 File Offset: 0x000B4F78
		public ChangeProjectileSpawnTimeOperation(Projectile _projectile, ref SpawnTimeAbstract _oldValue, ref SpawnTimeAbstract _newValue)
		{
			this.projectile = _projectile;
			this.oldValue = _oldValue;
			this.newValue = _newValue;
		}

		// Token: 0x06001C28 RID: 7208 RVA: 0x000B5F97 File Offset: 0x000B4F97
		public bool Undo()
		{
			if (!this.IsEmpty)
			{
				this.projectile.SpawnTime = this.oldValue;
				return true;
			}
			return false;
		}

		// Token: 0x06001C29 RID: 7209 RVA: 0x000B5FB5 File Offset: 0x000B4FB5
		public bool Redo()
		{
			if (!this.IsEmpty)
			{
				this.projectile.SpawnTime = this.newValue;
				return true;
			}
			return false;
		}

		// Token: 0x06001C2A RID: 7210 RVA: 0x000B5FD3 File Offset: 0x000B4FD3
		public void Destroy()
		{
			this.projectile = null;
		}

		// Token: 0x1700069E RID: 1694
		// (get) Token: 0x06001C2B RID: 7211 RVA: 0x000B5FDC File Offset: 0x000B4FDC
		public bool IsHighPriority
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700069F RID: 1695
		// (get) Token: 0x06001C2C RID: 7212 RVA: 0x000B5FDF File Offset: 0x000B4FDF
		public bool IsEmpty
		{
			get
			{
				return this.projectile == null;
			}
		}

		// Token: 0x170006A0 RID: 1696
		// (get) Token: 0x06001C2D RID: 7213 RVA: 0x000B5FEA File Offset: 0x000B4FEA
		public bool IsAbsolute
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06001C2E RID: 7214 RVA: 0x000B5FED File Offset: 0x000B4FED
		public bool ContainsLabel(string label)
		{
			return label == this.Label;
		}

		// Token: 0x170006A1 RID: 1697
		// (get) Token: 0x06001C2F RID: 7215 RVA: 0x000B5FFB File Offset: 0x000B4FFB
		public string Label
		{
			get
			{
				return "MapObjectContainer";
			}
		}

		// Token: 0x170006A2 RID: 1698
		// (get) Token: 0x06001C30 RID: 7216 RVA: 0x000B6002 File Offset: 0x000B5002
		public string Description
		{
			get
			{
				return "ChangeProjectileSpawnTimeOperation";
			}
		}

		// Token: 0x04001220 RID: 4640
		private Projectile projectile;

		// Token: 0x04001221 RID: 4641
		private readonly SpawnTimeAbstract oldValue;

		// Token: 0x04001222 RID: 4642
		private readonly SpawnTimeAbstract newValue;
	}
}
