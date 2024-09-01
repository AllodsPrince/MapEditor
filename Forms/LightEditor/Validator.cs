using System;

namespace MapEditor.Forms.LightEditor
{
	// Token: 0x020002AF RID: 687
	public class Validator
	{
		// Token: 0x140000E2 RID: 226
		// (add) Token: 0x06001FB4 RID: 8116 RVA: 0x000CB908 File Offset: 0x000CA908
		// (remove) Token: 0x06001FB5 RID: 8117 RVA: 0x000CB921 File Offset: 0x000CA921
		public event Validator.ValidatorEvent ValueChanged;

		// Token: 0x140000E3 RID: 227
		// (add) Token: 0x06001FB6 RID: 8118 RVA: 0x000CB93A File Offset: 0x000CA93A
		// (remove) Token: 0x06001FB7 RID: 8119 RVA: 0x000CB953 File Offset: 0x000CA953
		public event Validator.ValidatorEvent ValueChanging;

		// Token: 0x06001FB8 RID: 8120 RVA: 0x000CB96C File Offset: 0x000CA96C
		public virtual bool Validate()
		{
			return true;
		}

		// Token: 0x06001FB9 RID: 8121 RVA: 0x000CB96F File Offset: 0x000CA96F
		public virtual void Clear()
		{
		}

		// Token: 0x06001FBA RID: 8122 RVA: 0x000CB971 File Offset: 0x000CA971
		public void InvokeValueChangedEvent()
		{
			if (this.ValueChanged != null)
			{
				this.ValueChanged(this);
			}
		}

		// Token: 0x06001FBB RID: 8123 RVA: 0x000CB987 File Offset: 0x000CA987
		public void InvokeValueChangingEvent()
		{
			if (this.ValueChanging != null)
			{
				this.ValueChanging(this);
			}
		}

		// Token: 0x020002B0 RID: 688
		// (Invoke) Token: 0x06001FBE RID: 8126
		public delegate void ValidatorEvent(Validator sender);
	}
}
