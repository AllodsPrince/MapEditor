using System;
using System.ComponentModel;
using System.Windows.Forms;
using Tools.WindowParams;

namespace MapEditor.Forms.Groups
{
	// Token: 0x0200007B RID: 123
	public partial class FindObjectForm : Form
	{
		// Token: 0x060005FB RID: 1531 RVA: 0x00032FA0 File Offset: 0x00031FA0
		private void UpdateComboBox()
		{
			if (this.FindComboBox != null)
			{
				string newItem = this.FindComboBox.Text;
				if (!string.IsNullOrEmpty(newItem) && !this.FindComboBox.Items.Contains(newItem))
				{
					this.FindComboBox.Items.Insert(0, newItem);
					if (this.FindComboBox.Items.Count > FindObjectForm.maxFormComboBoxItemCount)
					{
						this.FindComboBox.Items.RemoveAt(this.FindComboBox.Items.Count - 1);
					}
				}
			}
		}

		// Token: 0x060005FC RID: 1532 RVA: 0x00033027 File Offset: 0x00032027
		private void FindComboBox_Leave(object sender, EventArgs e)
		{
			this.UpdateComboBox();
		}

		// Token: 0x060005FD RID: 1533 RVA: 0x0003302F File Offset: 0x0003202F
		private void FindObjectForm_FormClosing(object sender, FormClosingEventArgs e)
		{
			this.UpdateComboBox();
		}

		// Token: 0x060005FE RID: 1534 RVA: 0x00033038 File Offset: 0x00032038
		public FindObjectForm()
		{
			this.InitializeComponent();
			this.formParamsSaver = new FormParamsSaver(this, EditorEnvironment.EditorFormsFolder + "FindObjectForm.xml", false);
			this.formParamsSaver.AutoregisterControls = false;
			this.formParamsSaver.RegisterControl(this.FindComboBox, false);
		}

		// Token: 0x170000CB RID: 203
		// (get) Token: 0x060005FF RID: 1535 RVA: 0x0003308B File Offset: 0x0003208B
		public string ObjectName
		{
			get
			{
				return this.FindComboBox.Text;
			}
		}

		// Token: 0x0400048C RID: 1164
		private static readonly int maxFormComboBoxItemCount = 25;

		// Token: 0x0400048D RID: 1165
		private readonly FormParamsSaver formParamsSaver;
	}
}
