using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using Db;

namespace MapEditor.Forms.Quests.QuickObjectGenerator.DialogForms
{
	// Token: 0x0200004E RID: 78
	public partial class SelectIconDialogForm : Form
	{
		// Token: 0x06000419 RID: 1049 RVA: 0x000221EC File Offset: 0x000211EC
		private void FillListView()
		{
			this.Cursor = Cursors.WaitCursor;
			string filter = this.filterTextBox.Text;
			ListViewItem selectedItem = (this.listView.SelectedItems.Count > 0) ? this.listView.SelectedItems[0] : null;
			this.listView.BeginUpdate();
			this.listView.Items.Clear();
			if (this.items != null)
			{
				foreach (ListViewItem item in this.items)
				{
					if (string.IsNullOrEmpty(filter) || item.Text.IndexOf(filter, StringComparison.InvariantCultureIgnoreCase) != -1)
					{
						this.listView.Items.Add(item);
					}
				}
			}
			this.listView.EndUpdate();
			if (selectedItem != null)
			{
				int index = this.listView.Items.IndexOf(selectedItem);
				if (index != -1)
				{
					this.listView.EnsureVisible(index);
				}
			}
			this.listView.Select();
			this.Cursor = Cursors.Default;
		}

		// Token: 0x0600041A RID: 1050 RVA: 0x0002230C File Offset: 0x0002130C
		private void OnFormLoad(object sender, EventArgs e)
		{
			IDatabase mainDb = IDatabase.GetMainDatabase();
			if (mainDb != null)
			{
				List<DBID> objList = mainDb.GetObjectsList("UISingleTexture");
				objList.Sort(new SelectIconDialogForm.DBIDComparer());
				this.items.Capacity = objList.Count;
				List<Image> images = new List<Image>(objList.Count);
				int iconPathLen = "Interface/Icons/".Length;
				int imageIndex = 0;
				foreach (DBID dbid in objList)
				{
					string key = dbid.ToString();
					if (key.StartsWith("Interface/Icons/"))
					{
						string groupKey = dbid.GetFileFolder(string.Empty).Remove(0, iconPathLen);
						ListViewGroup itemGroup = null;
						foreach (object obj in this.listView.Groups)
						{
							ListViewGroup group = (ListViewGroup)obj;
							if (group.Header == groupKey)
							{
								itemGroup = group;
								break;
							}
						}
						if (itemGroup == null)
						{
							itemGroup = new ListViewGroup(groupKey);
							this.listView.Groups.Add(itemGroup);
						}
						ListViewItem item = new ListViewItem(dbid.GetFileShortName(), -1, itemGroup);
						item.Tag = key;
						this.items.Add(item);
						Bitmap icon = ObjectGeneratorConfig.IconCache.GetIcon(key);
						if (icon != null)
						{
							images.Add(icon);
							item.ImageIndex = imageIndex;
							imageIndex++;
						}
					}
				}
				this.IconImageList.Images.AddRange(images.ToArray());
				this.FillListView();
			}
		}

		// Token: 0x0600041B RID: 1051 RVA: 0x000224E4 File Offset: 0x000214E4
		private static void OnClosed(object sender, EventArgs e)
		{
			ObjectGeneratorConfig.IconCache.SaveCacheIfChanged();
		}

		// Token: 0x0600041C RID: 1052 RVA: 0x000224F0 File Offset: 0x000214F0
		private void OnIconSelected(object sender, EventArgs e)
		{
			base.DialogResult = DialogResult.OK;
		}

		// Token: 0x0600041D RID: 1053 RVA: 0x000224F9 File Offset: 0x000214F9
		private void OnFindClick(object sender, EventArgs e)
		{
			this.FillListView();
		}

		// Token: 0x0600041E RID: 1054 RVA: 0x00022501 File Offset: 0x00021501
		private void OnFilterKeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Return)
			{
				this.FillListView();
			}
		}

		// Token: 0x0600041F RID: 1055 RVA: 0x00022514 File Offset: 0x00021514
		public SelectIconDialogForm()
		{
			this.InitializeComponent();
			this.listView.DoubleClick += this.OnIconSelected;
			this.filterTextBox.KeyDown += this.OnFilterKeyDown;
			this.findButton.Click += this.OnFindClick;
			base.Load += this.OnFormLoad;
			base.Closed += SelectIconDialogForm.OnClosed;
		}

		// Token: 0x06000420 RID: 1056 RVA: 0x000225A4 File Offset: 0x000215A4
		public bool GetSelectedIconKey(out string key)
		{
			key = null;
			if (this.listView.SelectedItems != null && this.listView.SelectedItems.Count > 0)
			{
				ListViewItem item = this.listView.SelectedItems[0];
				if (item != null)
				{
					key = (item.Tag as string);
				}
			}
			return key != null;
		}

		// Token: 0x040002D9 RID: 729
		private const string iconPath = "Interface/Icons/";

		// Token: 0x040002DA RID: 730
		private readonly List<ListViewItem> items = new List<ListViewItem>();

		// Token: 0x0200004F RID: 79
		private class DBIDComparer : IComparer<DBID>
		{
			// Token: 0x06000423 RID: 1059 RVA: 0x0002289E File Offset: 0x0002189E
			public int Compare(DBID dbid1, DBID dbid2)
			{
				return dbid1.ToString().CompareTo(dbid2.ToString());
			}
		}
	}
}
