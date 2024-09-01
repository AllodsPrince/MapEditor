using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Windows.Forms;

namespace MapEditor.Forms.Quests
{
	// Token: 0x0200006D RID: 109
	public partial class AddQuestDialog : Form
	{
		// Token: 0x06000546 RID: 1350 RVA: 0x0002B2D4 File Offset: 0x0002A2D4
		private void LoadFolderComboBox()
		{
			List<string> zones = new List<string>();
			QuestEnvironment.LoadZones(zones);
			foreach (string zone in zones)
			{
				if (Directory.Exists(string.Format("{0}{1}{2}", EditorEnvironment.DataFolder, QuestEditor.QuestFolder, zone)))
				{
					this.zoneComboBox.Items.Add(zone);
				}
			}
		}

		// Token: 0x06000547 RID: 1351 RVA: 0x0002B358 File Offset: 0x0002A358
		private void OnChanged(object sender, EventArgs e)
		{
			this.OkInputButton.Enabled = (!string.IsNullOrEmpty(this.QuestNameTextBox.Text) && !string.IsNullOrEmpty(this.zoneComboBox.Text));
		}

		// Token: 0x06000548 RID: 1352 RVA: 0x0002B390 File Offset: 0x0002A390
		public AddQuestDialog(string zoneName)
		{
			this.InitializeComponent();
			this.LoadFolderComboBox();
			if (!string.IsNullOrEmpty(zoneName))
			{
				zoneName = string.Concat(zoneName.Split(new char[]
				{
					' '
				})).Trim();
				if (this.zoneComboBox.Items.Contains(zoneName))
				{
					this.zoneComboBox.SelectedItem = zoneName;
				}
			}
			this.OkInputButton.Enabled = false;
			this.QuestNameTextBox.TextChanged += this.OnChanged;
			this.zoneComboBox.SelectedIndexChanged += this.OnChanged;
		}

		// Token: 0x170000C6 RID: 198
		// (get) Token: 0x06000549 RID: 1353 RVA: 0x0002B42F File Offset: 0x0002A42F
		public string QuestName
		{
			get
			{
				return this.QuestNameTextBox.Text;
			}
		}

		// Token: 0x170000C7 RID: 199
		// (get) Token: 0x0600054A RID: 1354 RVA: 0x0002B43C File Offset: 0x0002A43C
		public string QuestZone
		{
			get
			{
				return this.zoneComboBox.Text;
			}
		}
	}
}
