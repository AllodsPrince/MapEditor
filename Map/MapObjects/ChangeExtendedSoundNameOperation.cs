using System;
using Operations;

namespace MapEditor.Map.MapObjects
{
	// Token: 0x02000178 RID: 376
	internal class ChangeExtendedSoundNameOperation : IOperation
	{
		// Token: 0x0600122E RID: 4654 RVA: 0x00084AB7 File Offset: 0x00083AB7
		public ChangeExtendedSoundNameOperation(ExtendedSound _extendedSound, ref string _oldValue, ref string _newValue)
		{
			this.extendedSound = _extendedSound;
			this.oldValue = _oldValue;
			this.newValue = _newValue;
		}

		// Token: 0x0600122F RID: 4655 RVA: 0x00084AD6 File Offset: 0x00083AD6
		public bool Undo()
		{
			if (!this.IsEmpty)
			{
				this.extendedSound.Name = this.oldValue;
				return true;
			}
			return false;
		}

		// Token: 0x06001230 RID: 4656 RVA: 0x00084AF4 File Offset: 0x00083AF4
		public bool Redo()
		{
			if (!this.IsEmpty)
			{
				this.extendedSound.Name = this.newValue;
				return true;
			}
			return false;
		}

		// Token: 0x06001231 RID: 4657 RVA: 0x00084B12 File Offset: 0x00083B12
		public void Destroy()
		{
			this.extendedSound = null;
		}

		// Token: 0x170003AB RID: 939
		// (get) Token: 0x06001232 RID: 4658 RVA: 0x00084B1B File Offset: 0x00083B1B
		public bool IsEmpty
		{
			get
			{
				return this.extendedSound == null;
			}
		}

		// Token: 0x170003AC RID: 940
		// (get) Token: 0x06001233 RID: 4659 RVA: 0x00084B26 File Offset: 0x00083B26
		public bool IsAbsolute
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170003AD RID: 941
		// (get) Token: 0x06001234 RID: 4660 RVA: 0x00084B29 File Offset: 0x00083B29
		public bool IsHighPriority
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06001235 RID: 4661 RVA: 0x00084B2C File Offset: 0x00083B2C
		public bool ContainsLabel(string label)
		{
			return label == this.Label;
		}

		// Token: 0x170003AE RID: 942
		// (get) Token: 0x06001236 RID: 4662 RVA: 0x00084B3A File Offset: 0x00083B3A
		public string Label
		{
			get
			{
				return "MapObjectContainer";
			}
		}

		// Token: 0x170003AF RID: 943
		// (get) Token: 0x06001237 RID: 4663 RVA: 0x00084B41 File Offset: 0x00083B41
		public string Description
		{
			get
			{
				return "ChangeExtendedSoundNameOperation";
			}
		}

		// Token: 0x04000CEB RID: 3307
		private ExtendedSound extendedSound;

		// Token: 0x04000CEC RID: 3308
		private readonly string oldValue;

		// Token: 0x04000CED RID: 3309
		private readonly string newValue;
	}
}
