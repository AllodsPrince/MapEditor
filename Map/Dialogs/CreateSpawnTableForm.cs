using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;
using MapEditor.Map.MapObjects;
using Tools.MapObjects;
using Tools.WindowParams;

namespace MapEditor.Map.Dialogs
{
	// Token: 0x020000D7 RID: 215
	public partial class CreateSpawnTableForm : Form
	{
		// Token: 0x06000B00 RID: 2816 RVA: 0x00059E0E File Offset: 0x00058E0E
		private string CutCommonFolder(string value)
		{
			if (!string.IsNullOrEmpty(this.commonFolder) && value.StartsWith(this.commonFolder, StringComparison.OrdinalIgnoreCase))
			{
				return value.Substring(this.commonFolder.Length);
			}
			return value;
		}

		// Token: 0x06000B01 RID: 2817 RVA: 0x00059E40 File Offset: 0x00058E40
		private void SpawnIDListView_AfterLabelEdit(object sender, LabelEditEventArgs e)
		{
			if (!string.IsNullOrEmpty(e.Label) && e.Item >= 0 && e.Item < this.SpawnIDListView.Items.Count)
			{
				ListViewItem listViewItem = this.SpawnIDListView.Items[e.Item];
				IMapObject mapObject = listViewItem.Tag as IMapObject;
				this.spawnIDs[mapObject] = e.Label;
			}
		}

		// Token: 0x06000B02 RID: 2818 RVA: 0x00059EB0 File Offset: 0x00058EB0
		private void editToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if (this.SpawnIDListView.SelectedItems.Count > 0)
			{
				this.SpawnIDListView.SelectedItems[0].BeginEdit();
			}
		}

		// Token: 0x06000B03 RID: 2819 RVA: 0x00059EDB File Offset: 0x00058EDB
		private void EditButton_Click(object sender, EventArgs e)
		{
			if (this.SpawnIDListView.SelectedItems.Count > 0)
			{
				this.SpawnIDListView.SelectedItems[0].BeginEdit();
			}
		}

		// Token: 0x06000B04 RID: 2820 RVA: 0x00059F06 File Offset: 0x00058F06
		private void SpawnIDListView_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
		{
			this.EditButton.Enabled = (this.SpawnIDListView.SelectedItems.Count > 0);
		}

		// Token: 0x06000B05 RID: 2821 RVA: 0x00059F26 File Offset: 0x00058F26
		private void SpawnIDListViewContextMenuStrip_Opened(object sender, EventArgs e)
		{
			this.editToolStripMenuItem.Enabled = (this.SpawnIDListView.SelectedItems.Count > 0);
		}

		// Token: 0x06000B06 RID: 2822 RVA: 0x00059F48 File Offset: 0x00058F48
		private void NewTableTextBox_TextChanged(object sender, EventArgs e)
		{
			if (this.created)
			{
				if (string.IsNullOrEmpty(this.commonFolder))
				{
					this.newSpawnTable = this.NewTableTextBox.Text;
					return;
				}
				this.newSpawnTable = this.commonFolder + this.NewTableTextBox.Text;
			}
		}

		// Token: 0x06000B07 RID: 2823 RVA: 0x00059F98 File Offset: 0x00058F98
		public CreateSpawnTableForm(string _newSpawnTablem, bool _useExistingSpawnTable, Dictionary<IMapObject, string> _spawnIDs, string _commonFolder)
		{
			this.InitializeComponent();
			this.newSpawnTable = _newSpawnTablem;
			this.useExistingSpawnTable = _useExistingSpawnTable;
			this.spawnIDs = _spawnIDs;
			this.commonFolder = _commonFolder;
			this.paramsSaver = new FormParamsSaver(this, EditorEnvironment.EditorFormsFolder + "CreateSpawnTableForm.xml", false);
			this.paramsSaver.AutoregisterControls = false;
			this.paramsSaver.RegisterControl(this.SpawnIDListView);
			this.NewTableTextBox.Text = this.CutCommonFolder(this.newSpawnTable);
			this.NewTableTextBox.Enabled = !this.useExistingSpawnTable;
			if (this.spawnIDs != null)
			{
				foreach (KeyValuePair<IMapObject, string> keyValuePair in this.spawnIDs)
				{
					SpawnPoint spawnPoint = keyValuePair.Key as SpawnPoint;
					if (spawnPoint != null)
					{
						ListViewItem listViewItem = new ListViewItem(keyValuePair.Value);
						listViewItem.Tag = keyValuePair.Key;
						listViewItem.SubItems.Add(spawnPoint.SpawnTable);
						this.SpawnIDListView.Items.Add(listViewItem);
					}
				}
			}
			this.EditButton.Enabled = (this.SpawnIDListView.SelectedItems.Count > 0);
			this.CreateButton.Enabled = (this.SpawnIDListView.Items.Count > 0);
			this.created = true;
		}

		// Token: 0x17000235 RID: 565
		// (get) Token: 0x06000B08 RID: 2824 RVA: 0x0005A120 File Offset: 0x00059120
		public string NewSpawnTable
		{
			get
			{
				return this.newSpawnTable;
			}
		}

		// Token: 0x04000852 RID: 2130
		private string newSpawnTable = string.Empty;

		// Token: 0x04000853 RID: 2131
		private readonly bool useExistingSpawnTable;

		// Token: 0x04000854 RID: 2132
		private readonly Dictionary<IMapObject, string> spawnIDs;

		// Token: 0x04000855 RID: 2133
		private readonly string commonFolder = string.Empty;

		// Token: 0x04000856 RID: 2134
		private readonly FormParamsSaver paramsSaver;

		// Token: 0x04000857 RID: 2135
		private readonly bool created;
	}
}
