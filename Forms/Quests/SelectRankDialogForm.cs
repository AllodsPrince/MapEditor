using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using Db;

namespace MapEditor.Forms.Quests
{
	// Token: 0x020000AC RID: 172
	public partial class SelectRankDialogForm : Form
	{
		// Token: 0x060007FA RID: 2042 RVA: 0x0003EB84 File Offset: 0x0003DB84
		private void OnSelectedIndexChanged(object sender, EventArgs e)
		{
			this.okButton.Enabled = (this.listBox.SelectedItem != null);
		}

		// Token: 0x060007FB RID: 2043 RVA: 0x0003EBA2 File Offset: 0x0003DBA2
		private void OnListDoubleClick(object sender, EventArgs e)
		{
			if (this.listBox.SelectedItem != null)
			{
				base.DialogResult = DialogResult.OK;
			}
		}

		// Token: 0x060007FC RID: 2044 RVA: 0x0003EBB8 File Offset: 0x0003DBB8
		public SelectRankDialogForm()
		{
			this.InitializeComponent();
			IFieldDesc[] fields = SelectRankDialogForm.mainDb.GetTypeSubFields("gameMechanics.elements.quest.QuestCountHonor");
			foreach (IFieldDesc desc in fields)
			{
				if (desc.FieldName == "rank")
				{
					foreach (string type in desc.EnumValues)
					{
						this.listBox.Items.Add(type);
					}
					break;
				}
			}
			this.listBox.SelectedIndexChanged += this.OnSelectedIndexChanged;
			this.listBox.DoubleClick += this.OnListDoubleClick;
		}

		// Token: 0x17000146 RID: 326
		// (get) Token: 0x060007FD RID: 2045 RVA: 0x0003EC6F File Offset: 0x0003DC6F
		public string SelectedItem
		{
			get
			{
				return this.listBox.SelectedItem as string;
			}
		}

		// Token: 0x040005CD RID: 1485
		private static readonly IDatabase mainDb = IDatabase.GetMainDatabase();
	}
}
