using System;
using Operations;

namespace MapEditor.Map.MapObjects
{
	// Token: 0x0200024C RID: 588
	internal class ChangeProjectileScriptIDOperation : IOperation
	{
		// Token: 0x06001C13 RID: 7187 RVA: 0x000B5E56 File Offset: 0x000B4E56
		public ChangeProjectileScriptIDOperation(Projectile _projectile, ref string _oldValue, ref string _newValue)
		{
			this.projectile = _projectile;
			this.oldValue = _oldValue;
			this.newValue = _newValue;
		}

		// Token: 0x06001C14 RID: 7188 RVA: 0x000B5E75 File Offset: 0x000B4E75
		public bool Undo()
		{
			if (!this.IsEmpty)
			{
				this.projectile.ScriptID = this.oldValue;
				return true;
			}
			return false;
		}

		// Token: 0x06001C15 RID: 7189 RVA: 0x000B5E93 File Offset: 0x000B4E93
		public bool Redo()
		{
			if (!this.IsEmpty)
			{
				this.projectile.ScriptID = this.newValue;
				return true;
			}
			return false;
		}

		// Token: 0x06001C16 RID: 7190 RVA: 0x000B5EB1 File Offset: 0x000B4EB1
		public void Destroy()
		{
			this.projectile = null;
		}

		// Token: 0x17000694 RID: 1684
		// (get) Token: 0x06001C17 RID: 7191 RVA: 0x000B5EBA File Offset: 0x000B4EBA
		public bool IsEmpty
		{
			get
			{
				return this.projectile == null;
			}
		}

		// Token: 0x17000695 RID: 1685
		// (get) Token: 0x06001C18 RID: 7192 RVA: 0x000B5EC5 File Offset: 0x000B4EC5
		public bool IsAbsolute
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000696 RID: 1686
		// (get) Token: 0x06001C19 RID: 7193 RVA: 0x000B5EC8 File Offset: 0x000B4EC8
		public bool IsHighPriority
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06001C1A RID: 7194 RVA: 0x000B5ECB File Offset: 0x000B4ECB
		public bool ContainsLabel(string label)
		{
			return label == this.Label;
		}

		// Token: 0x17000697 RID: 1687
		// (get) Token: 0x06001C1B RID: 7195 RVA: 0x000B5ED9 File Offset: 0x000B4ED9
		public string Label
		{
			get
			{
				return "MapObjectContainer";
			}
		}

		// Token: 0x17000698 RID: 1688
		// (get) Token: 0x06001C1C RID: 7196 RVA: 0x000B5EE0 File Offset: 0x000B4EE0
		public string Description
		{
			get
			{
				return "ChangeProjectileScriptIDOperation";
			}
		}

		// Token: 0x0400121A RID: 4634
		private Projectile projectile;

		// Token: 0x0400121B RID: 4635
		private readonly string oldValue;

		// Token: 0x0400121C RID: 4636
		private readonly string newValue;
	}
}
