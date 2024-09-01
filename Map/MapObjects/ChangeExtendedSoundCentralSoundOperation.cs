using System;
using Operations;

namespace MapEditor.Map.MapObjects
{
	// Token: 0x02000176 RID: 374
	internal class ChangeExtendedSoundCentralSoundOperation : IOperation
	{
		// Token: 0x0600121A RID: 4634 RVA: 0x00084995 File Offset: 0x00083995
		public ChangeExtendedSoundCentralSoundOperation(ExtendedSound _extendedSound, ref Sound _oldValue, ref Sound _newValue)
		{
			this.extendedSound = _extendedSound;
			this.oldValue = _oldValue;
			this.newValue = _newValue;
		}

		// Token: 0x0600121B RID: 4635 RVA: 0x000849B4 File Offset: 0x000839B4
		public bool Undo()
		{
			if (!this.IsEmpty)
			{
				this.extendedSound.CentralSound = this.oldValue;
				return true;
			}
			return false;
		}

		// Token: 0x0600121C RID: 4636 RVA: 0x000849D2 File Offset: 0x000839D2
		public bool Redo()
		{
			if (!this.IsEmpty)
			{
				this.extendedSound.CentralSound = this.newValue;
				return true;
			}
			return false;
		}

		// Token: 0x0600121D RID: 4637 RVA: 0x000849F0 File Offset: 0x000839F0
		public void Destroy()
		{
			this.extendedSound = null;
		}

		// Token: 0x170003A1 RID: 929
		// (get) Token: 0x0600121E RID: 4638 RVA: 0x000849F9 File Offset: 0x000839F9
		public bool IsEmpty
		{
			get
			{
				return this.extendedSound == null;
			}
		}

		// Token: 0x170003A2 RID: 930
		// (get) Token: 0x0600121F RID: 4639 RVA: 0x00084A04 File Offset: 0x00083A04
		public bool IsAbsolute
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170003A3 RID: 931
		// (get) Token: 0x06001220 RID: 4640 RVA: 0x00084A07 File Offset: 0x00083A07
		public bool IsHighPriority
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06001221 RID: 4641 RVA: 0x00084A0A File Offset: 0x00083A0A
		public bool ContainsLabel(string label)
		{
			return label == this.Label;
		}

		// Token: 0x170003A4 RID: 932
		// (get) Token: 0x06001222 RID: 4642 RVA: 0x00084A18 File Offset: 0x00083A18
		public string Label
		{
			get
			{
				return "MapObjectContainer";
			}
		}

		// Token: 0x170003A5 RID: 933
		// (get) Token: 0x06001223 RID: 4643 RVA: 0x00084A1F File Offset: 0x00083A1F
		public string Description
		{
			get
			{
				return "ChangeExtendedSoundCentralSoundOperation";
			}
		}

		// Token: 0x04000CE5 RID: 3301
		private ExtendedSound extendedSound;

		// Token: 0x04000CE6 RID: 3302
		private readonly Sound oldValue;

		// Token: 0x04000CE7 RID: 3303
		private readonly Sound newValue;
	}
}
