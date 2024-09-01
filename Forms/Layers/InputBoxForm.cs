using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace MapEditor.Forms.Layers
{
	// Token: 0x020000EE RID: 238
	public partial class InputBoxForm : Form
	{
		// Token: 0x06000C04 RID: 3076 RVA: 0x00067BB7 File Offset: 0x00066BB7
		public InputBoxForm()
		{
			this.InitializeComponent();
			this.OkInputButton.Click += this.OnOkClick;
			this.CancelInputButton.Click += this.OnCancelClick;
		}

		// Token: 0x17000240 RID: 576
		// (get) Token: 0x06000C05 RID: 3077 RVA: 0x00067BF3 File Offset: 0x00066BF3
		public string InputText
		{
			get
			{
				return this.InputTextBox.Text;
			}
		}

		// Token: 0x17000241 RID: 577
		// (set) Token: 0x06000C06 RID: 3078 RVA: 0x00067C00 File Offset: 0x00066C00
		public string InputCaption
		{
			set
			{
				this.Text = value;
			}
		}

		// Token: 0x06000C07 RID: 3079 RVA: 0x00067C0C File Offset: 0x00066C0C
		public void OnOkClick(object sender, EventArgs e)
		{
			this.InputTextBox.Text = this.InputTextBox.Text.Trim();
			if (this.InputTextBox.Text != "")
			{
				base.DialogResult = DialogResult.OK;
				base.Close();
			}
		}

		// Token: 0x06000C08 RID: 3080 RVA: 0x00067C58 File Offset: 0x00066C58
		public void OnCancelClick(object sender, EventArgs e)
		{
			base.DialogResult = DialogResult.Cancel;
			base.Close();
		}
	}
}
