using System;
using Operations;

namespace MapEditor.Map.MapObjects
{
	// Token: 0x02000177 RID: 375
	internal class ChangeExtendedSoundSideSoundOperation : IOperation
	{
		// Token: 0x06001224 RID: 4644 RVA: 0x00084A26 File Offset: 0x00083A26
		public ChangeExtendedSoundSideSoundOperation(ExtendedSound _extendedSound, ref Sound _oldValue, ref Sound _newValue)
		{
			this.extendedSound = _extendedSound;
			this.oldValue = _oldValue;
			this.newValue = _newValue;
		}

		// Token: 0x06001225 RID: 4645 RVA: 0x00084A45 File Offset: 0x00083A45
		public bool Undo()
		{
			if (!this.IsEmpty)
			{
				this.extendedSound.SideSound = this.oldValue;
				return true;
			}
			return false;
		}

		// Token: 0x06001226 RID: 4646 RVA: 0x00084A63 File Offset: 0x00083A63
		public bool Redo()
		{
			if (!this.IsEmpty)
			{
				this.extendedSound.SideSound = this.newValue;
				return true;
			}
			return false;
		}

		// Token: 0x06001227 RID: 4647 RVA: 0x00084A81 File Offset: 0x00083A81
		public void Destroy()
		{
			this.extendedSound = null;
		}

		// Token: 0x170003A6 RID: 934
		// (get) Token: 0x06001228 RID: 4648 RVA: 0x00084A8A File Offset: 0x00083A8A
		public bool IsEmpty
		{
			get
			{
				return this.extendedSound == null;
			}
		}

		// Token: 0x170003A7 RID: 935
		// (get) Token: 0x06001229 RID: 4649 RVA: 0x00084A95 File Offset: 0x00083A95
		public bool IsAbsolute
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170003A8 RID: 936
		// (get) Token: 0x0600122A RID: 4650 RVA: 0x00084A98 File Offset: 0x00083A98
		public bool IsHighPriority
		{
			get
			{
				return false;
			}
		}

		// Token: 0x0600122B RID: 4651 RVA: 0x00084A9B File Offset: 0x00083A9B
		public bool ContainsLabel(string label)
		{
			return label == this.Label;
		}

		// Token: 0x170003A9 RID: 937
		// (get) Token: 0x0600122C RID: 4652 RVA: 0x00084AA9 File Offset: 0x00083AA9
		public string Label
		{
			get
			{
				return "MapObjectContainer";
			}
		}

		// Token: 0x170003AA RID: 938
		// (get) Token: 0x0600122D RID: 4653 RVA: 0x00084AB0 File Offset: 0x00083AB0
		public string Description
		{
			get
			{
				return "ChangeExtendedSoundSideSoundOperation";
			}
		}

		// Token: 0x04000CE8 RID: 3304
		private ExtendedSound extendedSound;

		// Token: 0x04000CE9 RID: 3305
		private readonly Sound oldValue;

		// Token: 0x04000CEA RID: 3306
		private readonly Sound newValue;
	}
}
