using System;
using System.Windows.Forms;

namespace MapEditor.Forms.LightEditor
{
	// Token: 0x020002B3 RID: 691
	public class TextFloatValidator : Validator
	{
		// Token: 0x06001FD0 RID: 8144 RVA: 0x000CBCE5 File Offset: 0x000CACE5
		private void OnTextBoxChanging(object sender, EventArgs e)
		{
			base.InvokeValueChangingEvent();
		}

		// Token: 0x06001FD1 RID: 8145 RVA: 0x000CBCED File Offset: 0x000CACED
		private void OnTextBoxValidated(object sender, EventArgs e)
		{
			this.Validate();
		}

		// Token: 0x06001FD2 RID: 8146 RVA: 0x000CBCF8 File Offset: 0x000CACF8
		public TextFloatValidator(TextBox _textBox)
		{
			this.textBox = _textBox;
			if (this.textBox != null)
			{
				this.textBox.Validated += this.OnTextBoxValidated;
				this.textBox.TextChanged += this.OnTextBoxChanging;
			}
			this.SetValue(this.value);
		}

		// Token: 0x06001FD3 RID: 8147 RVA: 0x000CBD54 File Offset: 0x000CAD54
		public override bool Validate()
		{
			bool result = true;
			float newValue;
			if (!float.TryParse(this.textBox.Text, out newValue))
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

		// Token: 0x06001FD4 RID: 8148 RVA: 0x000CBD98 File Offset: 0x000CAD98
		public void SetValue(float _value)
		{
			this.value = _value;
			if (this.textBox != null)
			{
				this.textBox.Text = this.value.ToString();
			}
		}

		// Token: 0x06001FD5 RID: 8149 RVA: 0x000CBDBF File Offset: 0x000CADBF
		public override void Clear()
		{
			this.SetValue(0f);
		}

		// Token: 0x06001FD6 RID: 8150 RVA: 0x000CBDCC File Offset: 0x000CADCC
		public float GetValue()
		{
			return this.value;
		}

		// Token: 0x04001395 RID: 5013
		private TextBox textBox;

		// Token: 0x04001396 RID: 5014
		private float value;
	}
}
