using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;
using Tools.WindowParams;

namespace MapEditor.Map.Dialogs
{
	// Token: 0x0200015B RID: 347
	public partial class CloneSpawnTableForm : Form
	{
		// Token: 0x060010A1 RID: 4257 RVA: 0x0007C98C File Offset: 0x0007B98C
		private string CutCommonFolder(string value)
		{
			if (!string.IsNullOrEmpty(this.commonFolder) && value.StartsWith(this.commonFolder, StringComparison.OrdinalIgnoreCase))
			{
				return value.Substring(this.commonFolder.Length);
			}
			return value;
		}

		// Token: 0x060010A2 RID: 4258 RVA: 0x0007C9C0 File Offset: 0x0007B9C0
		private void TableListView_AfterLabelEdit(object sender, LabelEditEventArgs e)
		{
			if (!string.IsNullOrEmpty(e.Label) && e.Item >= 0 && e.Item < this.TableListView.Items.Count)
			{
				ListViewItem listViewItem = this.TableListView.Items[e.Item];
				string source = listViewItem.Tag as string;
				string destination = e.Label;
				if (!string.IsNullOrEmpty(this.commonFolder))
				{
					string oldDestination;
					this.clonedSpawnTables.TryGetValue(source, out oldDestination);
					if (oldDestination.StartsWith(this.commonFolder, StringComparison.OrdinalIgnoreCase))
					{
						destination = this.commonFolder + destination;
					}
				}
				if (string.Compare(source, destination, StringComparison.OrdinalIgnoreCase) != 0)
				{
					this.clonedSpawnTables[source] = destination;
					return;
				}
				e.CancelEdit = true;
			}
		}

		// Token: 0x060010A3 RID: 4259 RVA: 0x0007CA85 File Offset: 0x0007BA85
		private void editToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if (this.TableListView.SelectedItems.Count > 0)
			{
				this.TableListView.SelectedItems[0].BeginEdit();
			}
		}

		// Token: 0x060010A4 RID: 4260 RVA: 0x0007CAB0 File Offset: 0x0007BAB0
		private void EditButton_Click(object sender, EventArgs e)
		{
			if (this.TableListView.SelectedItems.Count > 0)
			{
				this.TableListView.SelectedItems[0].BeginEdit();
			}
		}

		// Token: 0x060010A5 RID: 4261 RVA: 0x0007CADB File Offset: 0x0007BADB
		private void TableListView_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
		{
			this.EditButton.Enabled = (this.TableListView.SelectedItems.Count > 0);
		}

		// Token: 0x060010A6 RID: 4262 RVA: 0x0007CAFB File Offset: 0x0007BAFB
		private void TableListViewContextMenuStrip_Opened(object sender, EventArgs e)
		{
			this.editToolStripMenuItem.Enabled = (this.TableListView.SelectedItems.Count > 0);
		}

		// Token: 0x060010A7 RID: 4263 RVA: 0x0007CB1C File Offset: 0x0007BB1C
		public CloneSpawnTableForm(Dictionary<string, int> _spawnTables, Dictionary<string, string> _clonedSpawnTables, string _commonFolder)
		{
			this.InitializeComponent();
			this.spawnTables = _spawnTables;
			this.clonedSpawnTables = _clonedSpawnTables;
			this.commonFolder = _commonFolder;
			this.paramsSaver = new FormParamsSaver(this, EditorEnvironment.EditorFormsFolder + "CloneSpawnTableForm.xml", false);
			this.paramsSaver.AutoregisterControls = false;
			this.paramsSaver.RegisterControl(this.TableListView);
			if (this.spawnTables != null && this.clonedSpawnTables != null)
			{
				foreach (KeyValuePair<string, int> keyValuePair in this.spawnTables)
				{
					string destination;
					if (this.clonedSpawnTables.TryGetValue(keyValuePair.Key, out destination))
					{
						ListViewItem listViewItem = new ListViewItem(this.CutCommonFolder(destination));
						listViewItem.Tag = keyValuePair.Key;
						listViewItem.SubItems.Add(this.CutCommonFolder(keyValuePair.Key));
						listViewItem.SubItems.Add(keyValuePair.Value.ToString());
						this.TableListView.Items.Add(listViewItem);
					}
				}
			}
			this.EditButton.Enabled = (this.TableListView.SelectedItems.Count > 0);
			this.CloneButton.Enabled = (this.TableListView.Items.Count > 0);
		}

		// Token: 0x04000C31 RID: 3121
		private readonly Dictionary<string, int> spawnTables;

		// Token: 0x04000C32 RID: 3122
		private readonly Dictionary<string, string> clonedSpawnTables;

		// Token: 0x04000C33 RID: 3123
		private readonly string commonFolder = string.Empty;

		// Token: 0x04000C34 RID: 3124
		private readonly FormParamsSaver paramsSaver;
	}
}
