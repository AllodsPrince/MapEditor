using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using Tools.WindowParams;

namespace MapEditor.Forms.Quests
{
	// Token: 0x020001B5 RID: 437
	public partial class QuestEditorZoneForm : Form
	{
		// Token: 0x06001532 RID: 5426 RVA: 0x00099CB4 File Offset: 0x00098CB4
		private void CheckData()
		{
			this.OkDialogButton.Enabled = !string.IsNullOrEmpty(this.Filter);
		}

		// Token: 0x06001533 RID: 5427 RVA: 0x00099CCF File Offset: 0x00098CCF
		private void OnZoneSelect(object sender, EventArgs e)
		{
			this.CheckData();
		}

		// Token: 0x06001534 RID: 5428 RVA: 0x00099CD7 File Offset: 0x00098CD7
		private void OnRBChecked(object sender, EventArgs e)
		{
			this.zoneComboBox.Enabled = this.zoneRadioButton.Checked;
			this.browserFolderButton.Enabled = this.folderRadioButton.Checked;
			this.CheckData();
		}

		// Token: 0x06001535 RID: 5429 RVA: 0x00099D0C File Offset: 0x00098D0C
		private void OnClickBrowseFolder(object sender, EventArgs e)
		{
			FolderBrowserDialog dialog = new FolderBrowserDialog();
			string dataFolder = EditorEnvironment.DataFolder.Replace('/', '\\');
			if (!string.IsNullOrEmpty(this.folderTextBox.Text))
			{
				dialog.SelectedPath = dataFolder + this.folderTextBox.Text;
			}
			else
			{
				dialog.SelectedPath = dataFolder.TrimEnd(new char[]
				{
					'\\'
				});
			}
			if (dialog.ShowDialog(this) == DialogResult.OK && dialog.SelectedPath.StartsWith(dataFolder, StringComparison.InvariantCultureIgnoreCase))
			{
				this.folderTextBox.Text = dialog.SelectedPath.Remove(0, dataFolder.Length);
				this.CheckData();
			}
		}

		// Token: 0x06001536 RID: 5430 RVA: 0x00099DB0 File Offset: 0x00098DB0
		public QuestEditorZoneForm()
		{
			this.InitializeComponent();
			this.paramsSaver = new FormParamsSaver(this, EditorEnvironment.EditorFormsFolder + "QuestEditorZoneForm.xml", false);
			this.paramsSaver.AutoregisterControls = false;
			this.paramsSaver.RegisterControl(this.zoneComboBox, true);
			this.paramsSaver.RegisterControl(this.folderTextBox);
			this.paramsSaver.RegisterControl(new RadioButton[]
			{
				this.zoneRadioButton,
				this.folderRadioButton
			});
			QuestEnvironment.LoadZones(this.zoneComboBox, false);
			this.zoneComboBox.Items.Add("_OtherFolder");
			this.OkDialogButton.Enabled = false;
			this.zoneComboBox.SelectedIndexChanged += this.OnZoneSelect;
			this.zoneRadioButton.CheckedChanged += this.OnRBChecked;
			this.browserFolderButton.Click += this.OnClickBrowseFolder;
		}

		// Token: 0x1700041F RID: 1055
		// (get) Token: 0x06001537 RID: 5431 RVA: 0x00099EAC File Offset: 0x00098EAC
		public bool Zone
		{
			get
			{
				return this.zoneRadioButton.Checked;
			}
		}

		// Token: 0x17000420 RID: 1056
		// (get) Token: 0x06001538 RID: 5432 RVA: 0x00099EB9 File Offset: 0x00098EB9
		public string Filter
		{
			get
			{
				if (!this.zoneRadioButton.Checked)
				{
					return this.folderTextBox.Text.Replace('\\', '/');
				}
				return this.zoneComboBox.Text;
			}
		}

		// Token: 0x04000F01 RID: 3841
		private readonly FormParamsSaver paramsSaver;
	}
}
