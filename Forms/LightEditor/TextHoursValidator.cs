using System;
using System.Windows.Forms;

namespace MapEditor.Forms.LightEditor
{
	// Token: 0x020002B1 RID: 689
	public class TextHoursValidator : Validator
	{
		// Token: 0x06001FC1 RID: 8129 RVA: 0x000CB9A5 File Offset: 0x000CA9A5
		private void OnTextBoxValidated(object sender, EventArgs e)
		{
			this.Validate();
		}

		// Token: 0x06001FC2 RID: 8130 RVA: 0x000CB9AE File Offset: 0x000CA9AE
		private void OnTextBoxChanging(object sender, EventArgs e)
		{
			base.InvokeValueChangingEvent();
		}

		// Token: 0x06001FC3 RID: 8131 RVA: 0x000CB9B8 File Offset: 0x000CA9B8
		public TextHoursValidator(TextBox _textBox)
		{
			this.textBox = _textBox;
			if (this.textBox != null)
			{
				this.textBox.Validated += this.OnTextBoxValidated;
				this.textBox.TextChanged += this.OnTextBoxChanging;
			}
			this.SetValue(this.value);
		}

		// Token: 0x06001FC4 RID: 8132 RVA: 0x000CBA14 File Offset: 0x000CAA14
		public override bool Validate()
		{
			bool result = true;
			int newValue;
			if (!int.TryParse(this.textBox.Text, out newValue))
			{
				result = false;
			}
			if (newValue < 0 || newValue > 24)
			{
				result = false;
			}
			if (result)
			{
				this.SetValue(newValue);
				base.InvokeValueChangedEvent();
			}
			else
			{
				this.SetValue(this.value);
			}
			return result;
		}

		// Token: 0x06001FC5 RID: 8133 RVA: 0x000CBA63 File Offset: 0x000CAA63
		public override void Clear()
		{
			this.SetValue(0);
		}

		// Token: 0x06001FC6 RID: 8134 RVA: 0x000CBA6C File Offset: 0x000CAA6C
		public void SetValue(int _value)
		{
			this.value = _value;
			this.textBox.Text = this.value.ToString();
		}

		// Token: 0x06001FC7 RID: 8135 RVA: 0x000CBA8B File Offset: 0x000CAA8B
		public int GetValue()
		{
			return this.value;
		}

		// Token: 0x0400138D RID: 5005
		private TextBox textBox;

		// Token: 0x0400138E RID: 5006
		private int value;
	}
}
