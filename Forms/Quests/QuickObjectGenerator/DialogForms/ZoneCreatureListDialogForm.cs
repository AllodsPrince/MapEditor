using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Windows.Forms;
using Db;
using MapEditor.Resources.Strings;
using Tools.WindowParams;

namespace MapEditor.Forms.Quests.QuickObjectGenerator.DialogForms
{
	// Token: 0x02000267 RID: 615
	public partial class ZoneCreatureListDialogForm : Form
	{
		// Token: 0x170006E5 RID: 1765
		// (get) Token: 0x06001D05 RID: 7429 RVA: 0x000B9171 File Offset: 0x000B8171
		// (set) Token: 0x06001D06 RID: 7430 RVA: 0x000B9179 File Offset: 0x000B8179
		public bool Changed
		{
			get
			{
				return this.changed;
			}
			set
			{
				this.changed = value;
				this.SaveButton.Enabled = this.changed;
			}
		}

		// Token: 0x06001D07 RID: 7431 RVA: 0x000B9194 File Offset: 0x000B8194
		private void Save()
		{
			if (!string.IsNullOrEmpty(this.zone) && this.config != null)
			{
				bool finded = false;
				List<string> list = new List<string>();
				for (int index = 0; index < this.listView.Items.Count; index++)
				{
					if (!string.IsNullOrEmpty(this.listView.Items[index].Text))
					{
						list.Add(this.listView.Items[index].Text);
					}
				}
				for (int index2 = 0; index2 < this.config.ZoneCreatureMap.Count; index2++)
				{
					if (this.config.ZoneCreatureMap[index2].key == this.zone)
					{
						this.config.ZoneCreatureMap[index2].values.Clear();
						this.config.ZoneCreatureMap[index2].values.AddRange(list);
						finded = true;
					}
					else if (!this.ZoneComboBox.Items.Contains(this.config.ZoneCreatureMap[index2].key))
					{
						this.config.ZoneCreatureMap[index2].values.Clear();
					}
				}
				if (!finded)
				{
					ObjectGeneratorConfig.StringListPair pair = default(ObjectGeneratorConfig.StringListPair);
					pair.key = this.zone;
					pair.values = list;
					this.config.ZoneCreatureMap.Add(pair);
				}
				Serializer.Save(ObjectGeneratorConfig.mobConfigPath, this.config, false);
				this.Changed = false;
			}
		}

		// Token: 0x06001D08 RID: 7432 RVA: 0x000B9328 File Offset: 0x000B8328
		private void PrompToSave()
		{
			if (this.Changed)
			{
				DialogResult result = MessageBox.Show(Strings.QUEST_EDITOR_SAVE_PROMP_MESSAGE, this.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
				if (result == DialogResult.Yes)
				{
					this.Save();
				}
			}
		}

		// Token: 0x06001D09 RID: 7433 RVA: 0x000B935C File Offset: 0x000B835C
		private void LoadView()
		{
			foreach (ObjectGeneratorConfig.StringListPair pair in this.config.ZoneCreatureMap)
			{
				this.listView.Items.Clear();
				if (pair.key == this.ZoneComboBox.Text)
				{
					int count = pair.values.Count;
					ListViewItem[] items = new ListViewItem[count];
					for (int index = 0; index < count; index++)
					{
						items[index] = new ListViewItem(pair.values[index]);
					}
					this.listView.Items.AddRange(items);
					break;
				}
			}
			this.zone = this.ZoneComboBox.Text;
			this.Changed = false;
		}

		// Token: 0x06001D0A RID: 7434 RVA: 0x000B943C File Offset: 0x000B843C
		private bool ChooseItemDoesntExist(string text, int checkIndex)
		{
			int count = this.listView.Items.Count;
			for (int index = 0; index < count; index++)
			{
				if (index != checkIndex && this.listView.Items[index].Text == text)
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06001D0B RID: 7435 RVA: 0x000B948C File Offset: 0x000B848C
		private bool BrowseKind(ListViewItem item)
		{
			if (item != null)
			{
				string initDir = null;
				string kind = item.Text;
				if (!string.IsNullOrEmpty(kind))
				{
					string path = EditorEnvironment.DataFolder + kind;
					if (File.Exists(path))
					{
						FileInfo fileInfo = new FileInfo(path);
						initDir = fileInfo.Directory.FullName;
					}
				}
				if (string.IsNullOrEmpty(initDir))
				{
					string path2 = EditorEnvironment.DataFolder.Remove(EditorEnvironment.DataFolder.Length - 1);
					if (Directory.Exists(path2))
					{
						initDir = path2.Replace('/', '\\');
					}
				}
				OpenFileDialog openDialog = new OpenFileDialog();
				openDialog.Filter = ".xdb files|*.xdb";
				if (!string.IsNullOrEmpty(initDir))
				{
					openDialog.InitialDirectory = initDir;
				}
				openDialog.RestoreDirectory = true;
				openDialog.Multiselect = false;
				if (openDialog.ShowDialog(this) == DialogResult.OK)
				{
					string filePath = openDialog.FileName.Replace('\\', '/');
					if (!string.IsNullOrEmpty(filePath))
					{
						string prefix = EditorEnvironment.DataFolder;
						if (filePath.ToUpper().StartsWith(prefix.ToUpper()))
						{
							filePath = filePath.Remove(0, prefix.Length);
							if (this.CheckEnteredValue(filePath, item.Index))
							{
								item.Text = filePath;
								return true;
							}
						}
					}
				}
			}
			return false;
		}

		// Token: 0x06001D0C RID: 7436 RVA: 0x000B95AD File Offset: 0x000B85AD
		private void OnSelectZone(object sender, EventArgs e)
		{
			this.PrompToSave();
			this.LoadView();
		}

		// Token: 0x06001D0D RID: 7437 RVA: 0x000B95BC File Offset: 0x000B85BC
		private void OnBrowseKind(object sender, EventArgs e)
		{
			if (this.listView.SelectedItems != null && this.listView.SelectedItems.Count > 0 && this.listView.SelectedItems[0] != null)
			{
				ListViewItem item = this.listView.SelectedItems[0];
				if (this.BrowseKind(item))
				{
					this.Changed = true;
					this.listView.Sort();
				}
			}
		}

		// Token: 0x06001D0E RID: 7438 RVA: 0x000B962C File Offset: 0x000B862C
		private void OnAddKind(object sender, EventArgs e)
		{
			if (!string.IsNullOrEmpty(this.ZoneComboBox.Text))
			{
				ListViewItem item = new ListViewItem(string.Empty);
				this.listView.Items.Add(item);
				this.Changed = true;
				item.BeginEdit();
			}
		}

		// Token: 0x06001D0F RID: 7439 RVA: 0x000B9678 File Offset: 0x000B8678
		private void OnDeleteKind(object sender, EventArgs e)
		{
			if (this.listView.SelectedItems != null && this.listView.SelectedItems.Count > 0 && this.listView.SelectedItems[0] != null)
			{
				this.Changed = true;
				this.listView.Items.Remove(this.listView.SelectedItems[0]);
			}
		}

		// Token: 0x06001D10 RID: 7440 RVA: 0x000B96E0 File Offset: 0x000B86E0
		private void OnSaveButtonClick(object sender, EventArgs e)
		{
			this.Save();
		}

		// Token: 0x06001D11 RID: 7441 RVA: 0x000B96E8 File Offset: 0x000B86E8
		private void OnFormClosed(object sender, EventArgs e)
		{
			this.PrompToSave();
		}

		// Token: 0x06001D12 RID: 7442 RVA: 0x000B96F0 File Offset: 0x000B86F0
		private void OnAfterEdit(object sender, LabelEditEventArgs e)
		{
			if (e != null && !string.IsNullOrEmpty(e.Label))
			{
				e.CancelEdit = !this.CheckEnteredValue(e.Label, e.Item);
				if (!e.CancelEdit)
				{
					this.Changed = true;
					this.listView.Sort();
				}
			}
		}

		// Token: 0x06001D13 RID: 7443 RVA: 0x000B9744 File Offset: 0x000B8744
		private bool CheckEnteredValue(string value, int index)
		{
			DBID dbid = this.mainDb.GetDBIDByName(value);
			return !DBID.IsNullOrEmpty(dbid) && this.mainDb.GetClassTypeName(dbid) == "gameMechanics.world.mob.MobKind" && this.ChooseItemDoesntExist(value, index);
		}

		// Token: 0x06001D14 RID: 7444 RVA: 0x000B978B File Offset: 0x000B878B
		private void OnListViewKeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Delete)
			{
				this.OnDeleteKind(sender, e);
			}
		}

		// Token: 0x06001D15 RID: 7445 RVA: 0x000B979F File Offset: 0x000B879F
		private void OnSelectedIndexChanged(object sender, EventArgs e)
		{
			this.listView.Sort();
		}

		// Token: 0x06001D16 RID: 7446 RVA: 0x000B97AC File Offset: 0x000B87AC
		public ZoneCreatureListDialogForm(ObjectGeneratorConfig.MobGeneratorConfig _config)
		{
			this.InitializeComponent();
			this.paramsSaver = new FormParamsSaver(this, EditorEnvironment.EditorFormsFolder + "CreatMobSetCingigDialog.xml", false);
			this.config = _config;
			this.mainDb = IDatabase.GetMainDatabase();
			if (this.config != null && this.mainDb != null)
			{
				QuestEnvironment.LoadZones(this.ZoneComboBox, false);
				this.ZoneComboBox.SelectedIndexChanged += this.OnSelectZone;
				this.listView.KeyDown += this.OnListViewKeyDown;
				this.AddMenuItem.Click += this.OnAddKind;
				this.DeleteMenuItem.Click += this.OnDeleteKind;
				this.BrowseMenuItem.Click += this.OnBrowseKind;
				this.SaveButton.Click += this.OnSaveButtonClick;
				base.Closed += this.OnFormClosed;
				this.listView.AfterLabelEdit += this.OnAfterEdit;
				this.listView.SelectedIndexChanged += this.OnSelectedIndexChanged;
			}
		}

		// Token: 0x0400127B RID: 4731
		private readonly ObjectGeneratorConfig.MobGeneratorConfig config;

		// Token: 0x0400127C RID: 4732
		private readonly IDatabase mainDb;

		// Token: 0x0400127D RID: 4733
		private bool changed;

		// Token: 0x0400127E RID: 4734
		private string zone = string.Empty;

		// Token: 0x0400127F RID: 4735
		private readonly FormParamsSaver paramsSaver;
	}
}
