using System;
using Operations;

namespace MapEditor.Map.MapObjects
{
	// Token: 0x0200024B RID: 587
	internal class ChangeProjectileDBIDOperation : IOperation
	{
		// Token: 0x06001C09 RID: 7177 RVA: 0x000B5DC5 File Offset: 0x000B4DC5
		public ChangeProjectileDBIDOperation(Projectile _projectile, ref string _oldValue, ref string _newValue)
		{
			this.projectile = _projectile;
			this.oldValue = _oldValue;
			this.newValue = _newValue;
		}

		// Token: 0x06001C0A RID: 7178 RVA: 0x000B5DE4 File Offset: 0x000B4DE4
		public bool Undo()
		{
			if (!this.IsEmpty)
			{
				this.projectile.ProjectileDBID = this.oldValue;
				return true;
			}
			return false;
		}

		// Token: 0x06001C0B RID: 7179 RVA: 0x000B5E02 File Offset: 0x000B4E02
		public bool Redo()
		{
			if (!this.IsEmpty)
			{
				this.projectile.ProjectileDBID = this.newValue;
				return true;
			}
			return false;
		}

		// Token: 0x06001C0C RID: 7180 RVA: 0x000B5E20 File Offset: 0x000B4E20
		public void Destroy()
		{
			this.projectile = null;
		}

		// Token: 0x1700068F RID: 1679
		// (get) Token: 0x06001C0D RID: 7181 RVA: 0x000B5E29 File Offset: 0x000B4E29
		public bool IsEmpty
		{
			get
			{
				return this.projectile == null;
			}
		}

		// Token: 0x17000690 RID: 1680
		// (get) Token: 0x06001C0E RID: 7182 RVA: 0x000B5E34 File Offset: 0x000B4E34
		public bool IsAbsolute
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000691 RID: 1681
		// (get) Token: 0x06001C0F RID: 7183 RVA: 0x000B5E37 File Offset: 0x000B4E37
		public bool IsHighPriority
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06001C10 RID: 7184 RVA: 0x000B5E3A File Offset: 0x000B4E3A
		public bool ContainsLabel(string label)
		{
			return label == this.Label;
		}

		// Token: 0x17000692 RID: 1682
		// (get) Token: 0x06001C11 RID: 7185 RVA: 0x000B5E48 File Offset: 0x000B4E48
		public string Label
		{
			get
			{
				return "MapObjectContainer";
			}
		}

		// Token: 0x17000693 RID: 1683
		// (get) Token: 0x06001C12 RID: 7186 RVA: 0x000B5E4F File Offset: 0x000B4E4F
		public string Description
		{
			get
			{
				return "ChangeProjectileDBIDOperation";
			}
		}

		// Token: 0x04001217 RID: 4631
		private Projectile projectile;

		// Token: 0x04001218 RID: 4632
		private readonly string oldValue;

		// Token: 0x04001219 RID: 4633
		private readonly string newValue;
	}
}
