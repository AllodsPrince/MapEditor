using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using MapEditor.Forms.CollectionBrowser.DataProviders;
using MapEditor.Properties;
using Tools.WindowParams;

namespace MapEditor.Forms.CollectionBrowser
{
	// Token: 0x02000070 RID: 112
	public partial class CollectionBrowser : Form
	{
		// Token: 0x06000589 RID: 1417 RVA: 0x0002DF74 File Offset: 0x0002CF74
		private int GetSelectedType()
		{
			if (this.collectionBrowserParams != null)
			{
				string selectedItem = this.TypeComboBox.Text;
				if (!string.IsNullOrEmpty(selectedItem))
				{
					int count = this.collectionBrowserParams.TypeNames.Count;
					for (int index = 0; index < count; index++)
					{
						if (selectedItem == this.collectionBrowserParams.TypeNames[index])
						{
							return index;
						}
					}
				}
			}
			return -1;
		}

		// Token: 0x0600058A RID: 1418 RVA: 0x0002DFD6 File Offset: 0x0002CFD6
		private int GetSelectedItem()
		{
			if (this.collectionBrowserParams != null && this.CollectionListView.SelectedItems.Count > 0)
			{
				return this.CollectionListView.Items.IndexOf(this.CollectionListView.SelectedItems[0]);
			}
			return -1;
		}

		// Token: 0x0600058B RID: 1419 RVA: 0x0002E018 File Offset: 0x0002D018
		private void FillTypeComboBox()
		{
			if (this.collectionBrowserParams != null)
			{
				int count = this.collectionBrowserParams.TypeNames.Count;
				for (int index = 0; index < count; index++)
				{
					this.TypeComboBox.Items.Add(this.collectionBrowserParams.TypeNames[index]);
				}
				this.TypeComboBox.SelectedIndex = 0;
			}
		}

		// Token: 0x0600058C RID: 1420 RVA: 0x0002E078 File Offset: 0x0002D078
		private void FillCollectionList()
		{
			if (this.collectionBrowserParams != null)
			{
				int count = this.collectionBrowserParams.ItemCount;
				for (int index = 0; index < count; index++)
				{
					int typeIndex = this.collectionBrowserParams.GetItemType(index);
					if (typeIndex >= 0 && typeIndex < this.collectionBrowserParams.TypeNames.Count)
					{
						this.CollectionListView.Items.Add(this.collectionBrowserParams.TypeNames[typeIndex]);
					}
				}
			}
		}

		// Token: 0x0600058D RID: 1421 RVA: 0x0002E0EC File Offset: 0x0002D0EC
		private void SwapItems(int left, int right)
		{
			int itemCount = this.CollectionListView.Items.Count;
			if (itemCount > 1 && left != right && left >= 0 && left < itemCount && right >= 0 && right < itemCount)
			{
				string leftText = this.CollectionListView.Items[left].Text;
				this.CollectionListView.Items[left].Text = this.CollectionListView.Items[right].Text;
				this.CollectionListView.Items[right].Text = leftText;
			}
		}

		// Token: 0x0600058E RID: 1422 RVA: 0x0002E17C File Offset: 0x0002D17C
		private void SelectItemInCollectionList(int itemIndex)
		{
			int count = this.CollectionListView.Items.Count;
			for (int index = 0; index < count; index++)
			{
				if (index != itemIndex)
				{
					if (this.CollectionListView.Items[index].Selected)
					{
						this.CollectionListView.Items[index].Selected = false;
					}
				}
				else if (!this.CollectionListView.Items[index].Selected)
				{
					this.CollectionListView.Items[index].Selected = true;
				}
			}
			this.ItemPropertyGrid.SelectedObject = this.collectionBrowserParams.GetItem(itemIndex);
		}

		// Token: 0x0600058F RID: 1423 RVA: 0x0002E224 File Offset: 0x0002D224
		private void UpdateButtons()
		{
			int itemIndex = this.GetSelectedItem();
			this.UpdateButtons(itemIndex);
		}

		// Token: 0x06000590 RID: 1424 RVA: 0x0002E240 File Offset: 0x0002D240
		private void UpdateButtons(int itemIndex)
		{
			int itemCount = this.CollectionListView.Items.Count;
			this.RemoveButton.Enabled = (itemCount > 0 && itemIndex != -1);
			this.DownButton.Enabled = (itemCount > 0 && itemIndex != -1 && itemIndex < itemCount - 1);
			this.UpButton.Enabled = (itemCount > 0 && itemIndex != -1 && itemIndex > 0);
		}

		// Token: 0x06000591 RID: 1425 RVA: 0x0002E2B0 File Offset: 0x0002D2B0
		private void OnLoadParams(FormParams formParams)
		{
			FormParams formParams2 = this.paramsSaver.FormParams;
			int size = 1;
			int[] defaultValues = new int[1];
			formParams2.ResizeInt(size, defaultValues);
			if (this.paramsSaver.FormParams.GetInt(0) > 100)
			{
				this.SplitContainer.SplitterDistance = this.paramsSaver.FormParams.GetInt(0);
				return;
			}
			this.SplitContainer.SplitterDistance = 100;
		}

		// Token: 0x06000592 RID: 1426 RVA: 0x0002E315 File Offset: 0x0002D315
		private void OnSaveParams(FormParams formParams)
		{
			this.paramsSaver.FormParams.ResizeInt(1);
			this.paramsSaver.FormParams.SetInt(0, this.SplitContainer.SplitterDistance);
		}

		// Token: 0x06000593 RID: 1427 RVA: 0x0002E344 File Offset: 0x0002D344
		private void CollectionBrowser_Load(object sender, EventArgs e)
		{
			if (this.collectionBrowserParams != null)
			{
				if (!string.IsNullOrEmpty(this.collectionBrowserParams.FormLabel))
				{
					this.Text = this.collectionBrowserParams.FormLabel;
				}
				if (!string.IsNullOrEmpty(this.collectionBrowserParams.CollectionPanelLabel))
				{
					this.CollectionLabel.Text = this.collectionBrowserParams.CollectionPanelLabel;
				}
				if (!string.IsNullOrEmpty(this.collectionBrowserParams.PropertyGridPanelLabel))
				{
					this.ItemLabel.Text = this.collectionBrowserParams.PropertyGridPanelLabel;
				}
				this.FillTypeComboBox();
				this.FillCollectionList();
				this.UpdateButtons();
				this.created = true;
			}
		}

		// Token: 0x06000594 RID: 1428 RVA: 0x0002E3E8 File Offset: 0x0002D3E8
		private void AddItem()
		{
			if (this.created)
			{
				this.created = false;
				if (this.collectionBrowserParams != null)
				{
					int itemIndex = this.GetSelectedItem();
					if (itemIndex == -1)
					{
						itemIndex = this.collectionBrowserParams.ItemCount;
					}
					int typeIndex = this.GetSelectedType();
					this.CollectionListView.Items.Insert(itemIndex, this.collectionBrowserParams.TypeNames[typeIndex]);
					this.collectionBrowserParams.InsertItem(itemIndex, typeIndex);
					this.SelectItemInCollectionList(itemIndex);
					this.UpdateButtons(itemIndex);
					this.created = true;
				}
			}
		}

		// Token: 0x06000595 RID: 1429 RVA: 0x0002E470 File Offset: 0x0002D470
		private void RemoveItem()
		{
			if (this.created && this.collectionBrowserParams != null)
			{
				this.created = false;
				int itemIndex = this.GetSelectedItem();
				if (itemIndex != -1)
				{
					this.collectionBrowserParams.RemoveItem(itemIndex);
					this.CollectionListView.Items.RemoveAt(itemIndex);
					this.ItemPropertyGrid.SelectedObject = null;
					this.UpdateButtons(-1);
				}
				this.created = true;
			}
		}

		// Token: 0x06000596 RID: 1430 RVA: 0x0002E4D8 File Offset: 0x0002D4D8
		private void MoveUpItem()
		{
			if (this.created)
			{
				this.created = false;
				if (this.collectionBrowserParams != null)
				{
					int itemIndex = this.GetSelectedItem();
					if (itemIndex > 0 && itemIndex < this.CollectionListView.Items.Count)
					{
						this.SwapItems(itemIndex - 1, itemIndex);
						this.collectionBrowserParams.SwapItems(itemIndex - 1, itemIndex);
						this.SelectItemInCollectionList(itemIndex - 1);
						this.UpdateButtons();
					}
				}
				this.created = true;
			}
		}

		// Token: 0x06000597 RID: 1431 RVA: 0x0002E54C File Offset: 0x0002D54C
		private void MoveDownItem()
		{
			if (this.created)
			{
				this.created = false;
				if (this.collectionBrowserParams != null)
				{
					int itemIndex = this.GetSelectedItem();
					if (itemIndex >= 0 && itemIndex < this.CollectionListView.Items.Count - 1)
					{
						this.SwapItems(itemIndex, itemIndex + 1);
						this.collectionBrowserParams.SwapItems(itemIndex, itemIndex + 1);
						this.SelectItemInCollectionList(itemIndex + 1);
						this.UpdateButtons();
					}
				}
				this.created = true;
			}
		}

		// Token: 0x06000598 RID: 1432 RVA: 0x0002E5BF File Offset: 0x0002D5BF
		private void AddButton_Click(object sender, EventArgs e)
		{
			this.AddItem();
		}

		// Token: 0x06000599 RID: 1433 RVA: 0x0002E5C7 File Offset: 0x0002D5C7
		private void RemoveButton_Click(object sender, EventArgs e)
		{
			this.RemoveItem();
		}

		// Token: 0x0600059A RID: 1434 RVA: 0x0002E5CF File Offset: 0x0002D5CF
		private void UpButton_Click(object sender, EventArgs e)
		{
			this.MoveUpItem();
		}

		// Token: 0x0600059B RID: 1435 RVA: 0x0002E5D7 File Offset: 0x0002D5D7
		private void DownButton_Click(object sender, EventArgs e)
		{
			this.MoveDownItem();
		}

		// Token: 0x0600059C RID: 1436 RVA: 0x0002E5E0 File Offset: 0x0002D5E0
		private void CollectionListView_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Insert)
			{
				this.AddItem();
				return;
			}
			if (e.KeyCode == Keys.Delete)
			{
				this.RemoveItem();
				return;
			}
			if (e.KeyCode == Keys.Up && e.Control)
			{
				this.MoveUpItem();
				return;
			}
			if (e.KeyCode == Keys.Down && e.Control)
			{
				this.MoveDownItem();
			}
		}

		// Token: 0x0600059D RID: 1437 RVA: 0x0002E640 File Offset: 0x0002D640
		private void CollectionListView_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
		{
			if (this.created && this.collectionBrowserParams != null)
			{
				int itemIndex = this.GetSelectedItem();
				if (itemIndex != -1)
				{
					this.ItemPropertyGrid.SelectedObject = this.collectionBrowserParams.GetItem(itemIndex);
				}
				else
				{
					this.ItemPropertyGrid.SelectedObject = null;
				}
				this.UpdateButtons(itemIndex);
			}
		}

		// Token: 0x0600059E RID: 1438 RVA: 0x0002E694 File Offset: 0x0002D694
		public CollectionBrowser(ICollectionBrowserParams _collectionBrowserParams)
		{
			this.paramsSaver = new FormParamsSaver(this, EditorEnvironment.EditorFormsFolder + "CollectionBrowserForm.xml", false);
			this.paramsSaver.LoadParams += this.OnLoadParams;
			this.paramsSaver.SaveParams += this.OnSaveParams;
			this.collectionBrowserParams = _collectionBrowserParams;
			this.InitializeComponent();
		}

		// Token: 0x04000439 RID: 1081
		private readonly ICollectionBrowserParams collectionBrowserParams;

		// Token: 0x0400043A RID: 1082
		private readonly FormParamsSaver paramsSaver;

		// Token: 0x0400043B RID: 1083
		private bool created;
	}
}
