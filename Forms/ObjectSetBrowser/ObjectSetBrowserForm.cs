using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Windows.Forms;
using MapEditor.Forms.Base;
using MapEditor.Map.DataProviders;
using MapEditor.Map.MapObjects;
using MapEditor.Resources.Strings;
using Rcs;
using Tools.ItemDataContainer;
using Tools.WeightList;
using Tools.WindowParams;

namespace MapEditor.Forms.ObjectSetBrowser
{
	// Token: 0x02000111 RID: 273
	public partial class ObjectSetBrowserForm : BaseForm
	{
		// Token: 0x06000D4A RID: 3402 RVA: 0x0006F5B8 File Offset: 0x0006E5B8
		private void OnLoadParams(FormParams formParams)
		{
			FormParams formParams2 = base.ParamsSaver.FormParams;
			int size = 1;
			int[] defaultValues = new int[1];
			formParams2.ResizeInt(size, defaultValues);
			if (base.ParamsSaver.FormParams.GetInt(0) != 0)
			{
				this.SplitContainer.SplitterDistance = base.ParamsSaver.FormParams.GetInt(0);
			}
		}

		// Token: 0x06000D4B RID: 3403 RVA: 0x0006F60D File Offset: 0x0006E60D
		private void OnSaveParams(FormParams formParams)
		{
			base.ParamsSaver.FormParams.ResizeInt(1);
			base.ParamsSaver.FormParams.SetInt(0, this.SplitContainer.SplitterDistance);
		}

		// Token: 0x06000D4C RID: 3404 RVA: 0x0006F63C File Offset: 0x0006E63C
		private void ObjectSetNameButton_Click(object sender, EventArgs e)
		{
			OpenFileDialog openFileDialog = new OpenFileDialog();
			openFileDialog.Filter = "XML Files|*.xml|All Files|*.*";
			openFileDialog.RestoreDirectory = true;
			openFileDialog.InitialDirectory = ObjectSetItemListSource.Folder.Replace('/', '\\');
			if (!Directory.Exists(openFileDialog.InitialDirectory))
			{
				Directory.CreateDirectory(openFileDialog.InitialDirectory);
			}
			if (openFileDialog.ShowDialog(this) == DialogResult.OK)
			{
				this.LoadObjectSet(openFileDialog.FileName);
			}
		}

		// Token: 0x06000D4D RID: 3405 RVA: 0x0006F6A4 File Offset: 0x0006E6A4
		private void ObjectSetsFiltersButton_Click(object sender, EventArgs e)
		{
			this.objectSetItemFiltes.ShowDialog(this.objectSetItemList, this);
		}

		// Token: 0x06000D4E RID: 3406 RVA: 0x0006F6B9 File Offset: 0x0006E6B9
		private void CloseButton_Click(object sender, EventArgs e)
		{
			base.Close();
		}

		// Token: 0x06000D4F RID: 3407 RVA: 0x0006F6C1 File Offset: 0x0006E6C1
		private void SaveButton_Click(object sender, EventArgs e)
		{
			this.Save(this.GetActiveFileName(), true);
		}

		// Token: 0x06000D50 RID: 3408 RVA: 0x0006F6D1 File Offset: 0x0006E6D1
		private void LoadButton_Click(object sender, EventArgs e)
		{
			this.LoadObjectSet(this.objectSetItemList.SelectedItem);
		}

		// Token: 0x06000D51 RID: 3409 RVA: 0x0006F6E4 File Offset: 0x0006E6E4
		private void DeleteButton_Click(object sender, EventArgs e)
		{
			if (this.ObjectSetListView.SelectedItems.Count > 0)
			{
				ListViewItem item = this.ObjectSetListView.SelectedItems[0];
				if (item != null && MessageBox.Show(Strings.MAP_EDITOR_DELETE_CONFIRM, Strings.MAP_EDITOR_DELETE_CONFIRM, MessageBoxButtons.YesNo) == DialogResult.Yes)
				{
					File.Delete(this.GetActiveFileName());
					if (RcsCreator.GetMainRcs() != null)
					{
						try
						{
							RcsCreator.GetMainRcs().RemoveItem(this.GetActiveFileName());
						}
						catch (RcsException ex)
						{
							Console.WriteLine(ex);
						}
					}
					this.objectSetSaver = null;
					this.PopulateObjects();
					this.objectSetItemList.Refresh();
					this.wasChanged = false;
					this.ObjectSetNameTextBox.Text = null;
				}
			}
		}

		// Token: 0x06000D52 RID: 3410 RVA: 0x0006F798 File Offset: 0x0006E798
		private void StaticObjectFiltersButton_Click(object sender, EventArgs e)
		{
			this.staticObjectItemFiltes.ShowDialog(this.staticObjectItemList, this);
		}

		// Token: 0x06000D53 RID: 3411 RVA: 0x0006F7AD File Offset: 0x0006E7AD
		private void StaticObjectListView_DoubleClick(object sender, EventArgs e)
		{
			this.AddStaticObjectButton_Click(sender, e);
		}

		// Token: 0x06000D54 RID: 3412 RVA: 0x0006F7B8 File Offset: 0x0006E7B8
		private void AddStaticObjectButton_Click(object sender, EventArgs e)
		{
			if (!this.AddStaticObjectButton.Enabled || this.StaticObjectListView.SelectedItems.Count < 1)
			{
				return;
			}
			foreach (object obj in this.StaticObjectListView.SelectedItems)
			{
				ListViewItem listViewItem = (ListViewItem)obj;
				string item = listViewItem.Tag as string;
				if (!string.IsNullOrEmpty(item))
				{
					bool isNew = true;
					foreach (object obj2 in this.ObjectListView.Items)
					{
						ListViewItem _listViewItem = (ListViewItem)obj2;
						WeightList<string>.WeightItem weightItem = _listViewItem.Tag as WeightList<string>.WeightItem;
						if (weightItem != null && weightItem.Item == item)
						{
							isNew = false;
							break;
						}
					}
					if (isNew)
					{
						ListViewItem newItem = new ListViewItem();
						this.UpdateItemListView(newItem, new WeightList<string>.WeightItem(item, this.GetWeight()));
						this.ObjectListView.Items.Add(newItem);
						this.wasChanged = true;
					}
				}
			}
			this.UpdateStaticObjectButtons();
			this.UpdateObjectSetButtons();
		}

		// Token: 0x06000D55 RID: 3413 RVA: 0x0006F90C File Offset: 0x0006E90C
		private void RemoveStaticObjectButton_Click(object sender, EventArgs e)
		{
			if (!this.RemoveStaticObjectButton.Enabled || this.ObjectListView.SelectedItems.Count > 0)
			{
				List<ListViewItem> toRemove = new List<ListViewItem>(this.ObjectListView.SelectedItems.Count);
				foreach (object obj in this.ObjectListView.SelectedItems)
				{
					ListViewItem item = (ListViewItem)obj;
					toRemove.Add(item);
				}
				this.ObjectListView.BeginUpdate();
				foreach (ListViewItem item2 in toRemove)
				{
					this.ObjectListView.Items.Remove(item2);
				}
				this.ObjectListView.EndUpdate();
				this.UpdateStaticObjectButtons();
				this.UpdateObjectSetButtons();
				this.wasChanged = true;
			}
		}

		// Token: 0x06000D56 RID: 3414 RVA: 0x0006FA18 File Offset: 0x0006EA18
		private void AutoCompleteButton_Click(object sender, EventArgs e)
		{
			this.Autocomplete();
		}

		// Token: 0x06000D57 RID: 3415 RVA: 0x0006FA20 File Offset: 0x0006EA20
		private void ResetButton_Click(object sender, EventArgs e)
		{
			this.PopulateObjects();
			this.UpdateObjectSetButtons();
			this.wasChanged = false;
		}

		// Token: 0x06000D58 RID: 3416 RVA: 0x0006FA38 File Offset: 0x0006EA38
		private void WeightButton_Click(object sender, EventArgs e)
		{
			foreach (object obj in this.ObjectListView.SelectedItems)
			{
				ListViewItem listViewItem = (ListViewItem)obj;
				WeightList<string>.WeightItem weightItem = listViewItem.Tag as WeightList<string>.WeightItem;
				if (weightItem != null)
				{
					this.UpdateItemListView(listViewItem, new WeightList<string>.WeightItem(weightItem.Item, this.GetWeight()));
				}
			}
		}

		// Token: 0x06000D59 RID: 3417 RVA: 0x0006FAB8 File Offset: 0x0006EAB8
		private bool Save(string fileName, bool askForRewrite)
		{
			if (!string.IsNullOrEmpty(fileName))
			{
				if (this.objectSetSaver == null)
				{
					this.objectSetSaver = new ObjectSetSaver();
				}
				if (this.ObjectSetNameTextBox.Text.Length > 0)
				{
					if (askForRewrite && File.Exists(fileName) && MessageBox.Show(Strings.MAP_EDITOR_OVERWRITE, Strings.MAP_EDITOR_FILE_EXISTS, MessageBoxButtons.YesNo) != DialogResult.Yes)
					{
						return false;
					}
					this.objectSetSaver.Clear();
					List<WeightList<string>.WeightItem> weights;
					this.CreateWeightItemList(out weights);
					this.objectSetSaver.Objects.AddRange(weights);
					ObjectSetSaver.Save(this.objectSetSaver, fileName, true);
					this.objectSetItemList.Refresh();
					this.wasChanged = false;
					return true;
				}
			}
			return false;
		}

		// Token: 0x06000D5A RID: 3418 RVA: 0x0006FB5C File Offset: 0x0006EB5C
		private bool PrompToSave()
		{
			if (this.wasChanged)
			{
				string lastLoadedFileName = this.GetActiveFileName();
				if (!string.IsNullOrEmpty(lastLoadedFileName))
				{
					DialogResult dialogResult = MessageBox.Show(this, string.Format(Strings.SAVE_OBJECT_SET_QUESTION, lastLoadedFileName), this.Text, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
					if (dialogResult == DialogResult.Yes)
					{
						return this.Save(lastLoadedFileName, false);
					}
					if (dialogResult == DialogResult.No)
					{
						return true;
					}
					if (dialogResult == DialogResult.Cancel)
					{
						return false;
					}
				}
			}
			return true;
		}

		// Token: 0x06000D5B RID: 3419 RVA: 0x0006FBB8 File Offset: 0x0006EBB8
		private void LoadObjectSet(string fileName)
		{
			if (!this.PrompToSave())
			{
				return;
			}
			this.SetActiveFileName(fileName);
			ObjectSetSaver newObjectSetSaver = ObjectSetSaver.Load(this.GetActiveFileName());
			if (newObjectSetSaver != null)
			{
				this.objectSetSaver = newObjectSetSaver;
				this.PopulateObjects();
			}
			this.wasChanged = false;
		}

		// Token: 0x06000D5C RID: 3420 RVA: 0x0006FBF8 File Offset: 0x0006EBF8
		private string GetActiveFileName()
		{
			string name = ObjectSetItemListSource.Folder + this.ObjectSetNameTextBox.Text;
			name = Str.ExtendFileExtention(name, ".xml");
			return name.Replace('\\', '/');
		}

		// Token: 0x06000D5D RID: 3421 RVA: 0x0006FC34 File Offset: 0x0006EC34
		private void SetActiveFileName(string item)
		{
			string _item = item.Replace('\\', '/');
			if (_item.StartsWith(ObjectSetItemListSource.Folder, StringComparison.OrdinalIgnoreCase))
			{
				_item = _item.Substring(ObjectSetItemListSource.Folder.Length);
				_item = Str.CutFileExtention(_item, ".xml");
				this.ObjectSetNameTextBox.Text = _item;
				this.UpdateObjectSetButtons();
			}
		}

		// Token: 0x06000D5E RID: 3422 RVA: 0x0006FC89 File Offset: 0x0006EC89
		private void PopulateObjects()
		{
			this.PopulateObjects((this.objectSetSaver != null) ? this.objectSetSaver.Objects : null);
		}

		// Token: 0x06000D5F RID: 3423 RVA: 0x0006FCA8 File Offset: 0x0006ECA8
		private void PopulateObjects(WeightList<string> weightList)
		{
			this.ObjectListView.BeginUpdate();
			this.ObjectListView.Clear();
			if (weightList != null)
			{
				foreach (WeightList<string>.WeightItem weightItem in weightList.Items)
				{
					if (weightItem != null)
					{
						ListViewItem newItem = new ListViewItem();
						this.UpdateItemListView(newItem, weightItem);
						this.ObjectListView.Items.Add(newItem);
					}
				}
			}
			this.ObjectListView.EndUpdate();
			this.UpdateStaticObjectButtons();
		}

		// Token: 0x06000D60 RID: 3424 RVA: 0x0006FD44 File Offset: 0x0006ED44
		private void CreateWeightItemList(out List<WeightList<string>.WeightItem> weights)
		{
			weights = new List<WeightList<string>.WeightItem>(this.ObjectSetListView.Items.Count);
			foreach (object obj in this.ObjectListView.Items)
			{
				ListViewItem item = (ListViewItem)obj;
				WeightList<string>.WeightItem weightItem = item.Tag as WeightList<string>.WeightItem;
				if (weightItem != null)
				{
					weights.Add(weightItem);
				}
			}
		}

		// Token: 0x06000D61 RID: 3425 RVA: 0x0006FDCC File Offset: 0x0006EDCC
		private void Autocomplete()
		{
			List<WeightList<string>.WeightItem> weights;
			this.CreateWeightItemList(out weights);
			WeightList<string> weightList = new WeightList<string>();
			weightList.AddRange(weights);
			ObjectSetSaver.Autocomplete(this.staticObjectItemListSource, weightList);
			this.PopulateObjects(weightList);
		}

		// Token: 0x06000D62 RID: 3426 RVA: 0x0006FE04 File Offset: 0x0006EE04
		private void UpdateItemListView(ListViewItem item, WeightList<string>.WeightItem weightItem)
		{
			if (item != null && weightItem != null)
			{
				item.Tag = weightItem;
				if (base.Context != null && base.Context.ItemDataContainer != null)
				{
					ItemData itemData = base.Context.ItemDataContainer.GetItemData(weightItem.Item);
					if (itemData != null)
					{
						item.Text = string.Format("{0} ({1})", itemData.Text, weightItem.Weight);
						item.ImageIndex = itemData.IconIndex;
						item.ToolTipText = itemData.Tooltip;
						this.wasChanged = true;
						return;
					}
				}
				item.Text = weightItem.Item;
			}
		}

		// Token: 0x06000D63 RID: 3427 RVA: 0x0006FEA0 File Offset: 0x0006EEA0
		private int GetWeight()
		{
			int weight;
			if (int.TryParse(this.WeightComboBox.Text, out weight))
			{
				return weight;
			}
			return 1;
		}

		// Token: 0x06000D64 RID: 3428 RVA: 0x0006FEC4 File Offset: 0x0006EEC4
		private void UpdateObjectSetButtons()
		{
			bool itemSelected = !string.IsNullOrEmpty(this.objectSetItemList.SelectedItem);
			bool activeNameSelected = !string.IsNullOrEmpty(this.ObjectSetNameTextBox.Text);
			this.SaveButton.Enabled = (activeNameSelected && this.ObjectListView.Items.Count > 0);
			this.LoadButton.Enabled = itemSelected;
			this.DeleteButton.Enabled = activeNameSelected;
		}

		// Token: 0x06000D65 RID: 3429 RVA: 0x0006FF38 File Offset: 0x0006EF38
		private void UpdateStaticObjectButtons()
		{
			bool multiObjectLoaded = this.objectSetSaver != null;
			bool staticObjectSelected = !string.IsNullOrEmpty(this.staticObjectItemList.SelectedItem);
			bool objectSelected = this.ObjectListView.SelectedItems.Count > 0;
			bool activeNameSelected = !string.IsNullOrEmpty(this.ObjectSetNameTextBox.Text);
			this.AddStaticObjectButton.Enabled = staticObjectSelected;
			this.RemoveStaticObjectButton.Enabled = objectSelected;
			this.AutoCompleteButton.Enabled = activeNameSelected;
			this.ResetButton.Enabled = multiObjectLoaded;
			this.WeightButton.Enabled = objectSelected;
		}

		// Token: 0x06000D66 RID: 3430 RVA: 0x0006FFCA File Offset: 0x0006EFCA
		private void ObjectListView_DoubleClick(object sender, EventArgs e)
		{
			this.RemoveStaticObjectButton_Click(sender, e);
		}

		// Token: 0x06000D67 RID: 3431 RVA: 0x0006FFD4 File Offset: 0x0006EFD4
		private void ObjectListView_SelectedIndexChanged(object sender, EventArgs e)
		{
			this.UpdateStaticObjectButtons();
		}

		// Token: 0x06000D68 RID: 3432 RVA: 0x0006FFDC File Offset: 0x0006EFDC
		private void ObjectSetListView_DoubleClick(object sender, EventArgs e)
		{
			this.LoadButton_Click(sender, e);
		}

		// Token: 0x06000D69 RID: 3433 RVA: 0x0006FFE6 File Offset: 0x0006EFE6
		private void OnObjectSetItemList_ItemSelected(ItemList sender, string item)
		{
			this.UpdateObjectSetButtons();
		}

		// Token: 0x06000D6A RID: 3434 RVA: 0x0006FFEE File Offset: 0x0006EFEE
		private void OnObjectSetItemList_ItemUnselected(ItemList sender, string item)
		{
			this.UpdateObjectSetButtons();
		}

		// Token: 0x06000D6B RID: 3435 RVA: 0x0006FFF6 File Offset: 0x0006EFF6
		private void OnObjectSetItemList_ListCleared(ItemList sender, string item)
		{
			this.UpdateObjectSetButtons();
		}

		// Token: 0x06000D6C RID: 3436 RVA: 0x0006FFFE File Offset: 0x0006EFFE
		private void OnStaticObjectItemList_ItemSelected(ItemList sender, string item)
		{
			this.UpdateStaticObjectButtons();
		}

		// Token: 0x06000D6D RID: 3437 RVA: 0x00070006 File Offset: 0x0006F006
		private void OnStaticObjectItemList_ItemUnselected(ItemList sender, string item)
		{
			this.UpdateStaticObjectButtons();
		}

		// Token: 0x06000D6E RID: 3438 RVA: 0x0007000E File Offset: 0x0006F00E
		private void OnStaticObjectItemList_ListCleared(ItemList sender, string item)
		{
			this.UpdateStaticObjectButtons();
		}

		// Token: 0x06000D6F RID: 3439 RVA: 0x00070016 File Offset: 0x0006F016
		private void OnObjectListViewKeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Delete)
			{
				this.RemoveStaticObjectButton_Click(sender, e);
			}
		}

		// Token: 0x06000D70 RID: 3440 RVA: 0x0007002C File Offset: 0x0006F02C
		private void ObjectSetBrowserForm_FormClosing(object sender, FormClosingEventArgs e)
		{
			if (base.AllowClose)
			{
				this.objectSetItemList.Unbind();
				this.objectSetItemFiltes.Unbind();
				this.objectSetItemList.ItemSources.Clear();
				this.staticObjectItemList.Unbind();
				this.staticObjectItemFiltes.Unbind();
				this.staticObjectItemList.ItemSources.Clear();
			}
		}

		// Token: 0x06000D71 RID: 3441 RVA: 0x00070090 File Offset: 0x0006F090
		private void ObjectSetBrowserForm_Load(object sender, EventArgs e)
		{
			this.objectSetItemList.ItemSources.Add(this.objectSetItemListSource);
			this.objectSetItemFiltes.Bind(this.objectSetItemList.ItemFilters);
			this.objectSetItemList.Bind(this.ObjectSetListView, this.ObjectSetFiltersComboBox);
			this.staticObjectItemList.ItemSources.Add(this.staticObjectItemListSource);
			this.staticObjectItemFiltes.Bind(this.staticObjectItemList.ItemFilters);
			this.staticObjectItemList.Bind(this.StaticObjectListView, this.StaticObjectFiltersComboBox);
			this.PopulateObjects();
			this.UpdateObjectSetButtons();
			this.UpdateStaticObjectButtons();
		}

		// Token: 0x06000D72 RID: 3442 RVA: 0x00070135 File Offset: 0x0006F135
		private void ObjectSetNameTextBox_TextChanged(object sender, EventArgs e)
		{
			this.UpdateObjectSetButtons();
		}

		// Token: 0x06000D73 RID: 3443 RVA: 0x00070140 File Offset: 0x0006F140
		private void WeightComboBox_Leave(object sender, EventArgs e)
		{
			string newItem = this.WeightComboBox.Text;
			if (!this.WeightComboBox.Items.Contains(newItem))
			{
				this.WeightComboBox.Items.Insert(0, newItem);
				if (this.WeightComboBox.Items.Count > 35)
				{
					this.WeightComboBox.Items.RemoveAt(this.WeightComboBox.Items.Count - 1);
				}
			}
		}

		// Token: 0x06000D74 RID: 3444 RVA: 0x000701B4 File Offset: 0x0006F1B4
		protected override void WndProc(ref Message message)
		{
			if (base.Visible && !base.AllowClose && message.Msg == 16 && !this.PrompToSave())
			{
				return;
			}
			base.WndProc(ref message);
		}

		// Token: 0x06000D75 RID: 3445 RVA: 0x000701E0 File Offset: 0x0006F1E0
		private void OnMainFormClosing(FormClosingEventArgs e)
		{
			if (base.Visible && e.CloseReason == CloseReason.UserClosing && !this.PrompToSave())
			{
				e.Cancel = true;
			}
		}

		// Token: 0x06000D76 RID: 3446 RVA: 0x00070204 File Offset: 0x0006F204
		public ObjectSetBrowserForm(MainForm.Context context) : base(EditorEnvironment.EditorFormsFolder + "ObjectSetBrowserForm.xml", context)
		{
			this.InitializeComponent();
			if (base.ParamsSaver != null)
			{
				base.ParamsSaver.LoadParams += this.OnLoadParams;
				base.ParamsSaver.SaveParams += this.OnSaveParams;
			}
			if (base.Context != null && base.Context.ItemDataContainer != null)
			{
				this.objectSetItemListSource = new ObjectSetItemListSource();
				this.objectSetItemList = new ItemList(string.Format("{0}ObjectSetBrowser/ObjectSetItemList.xml", EditorEnvironment.EditorFormsFolder), base.Context.ItemDataContainer, false);
				ItemList itemList = this.objectSetItemList;
				itemList.ItemSelected = (ItemList.ItemEvent)Delegate.Combine(itemList.ItemSelected, new ItemList.ItemEvent(this.OnObjectSetItemList_ItemSelected));
				ItemList itemList2 = this.objectSetItemList;
				itemList2.ItemUnselected = (ItemList.ItemEvent)Delegate.Combine(itemList2.ItemUnselected, new ItemList.ItemEvent(this.OnObjectSetItemList_ItemUnselected));
				ItemList itemList3 = this.objectSetItemList;
				itemList3.ListCleared = (ItemList.ItemEvent)Delegate.Combine(itemList3.ListCleared, new ItemList.ItemEvent(this.OnObjectSetItemList_ListCleared));
				this.objectSetItemFiltes = new FolderItemFilters(string.Format("{0}/Filtes/ObjectSetFilters.xml", EditorEnvironment.EditorFolder), EditorEnvironment.EditorFormsFolder);
				this.staticObjectItemListSource = new StaticObjectItemListSource();
				this.staticObjectItemList = new ItemList(string.Format("{0}ObjectSetBrowser/StaticObjectsItemList.xml", EditorEnvironment.EditorFormsFolder), base.Context.ItemDataContainer, true);
				ItemList itemList4 = this.staticObjectItemList;
				itemList4.ItemSelected = (ItemList.ItemEvent)Delegate.Combine(itemList4.ItemSelected, new ItemList.ItemEvent(this.OnStaticObjectItemList_ItemSelected));
				ItemList itemList5 = this.staticObjectItemList;
				itemList5.ItemUnselected = (ItemList.ItemEvent)Delegate.Combine(itemList5.ItemUnselected, new ItemList.ItemEvent(this.OnStaticObjectItemList_ItemUnselected));
				ItemList itemList6 = this.staticObjectItemList;
				itemList6.ListCleared = (ItemList.ItemEvent)Delegate.Combine(itemList6.ListCleared, new ItemList.ItemEvent(this.OnStaticObjectItemList_ListCleared));
				this.staticObjectItemFiltes = new FolderItemFilters(string.Format("{0}ObjectSetBrowser/StaticObjectsFilters.xml", EditorEnvironment.EditorFolder), EditorEnvironment.EditorFormsFolder);
				this.ObjectListView.SmallImageList = base.Context.ItemDataContainer.GetSmallImageList();
				this.ObjectListView.KeyDown += this.OnObjectListViewKeyDown;
				base.ParamsSaver.RegisterControl(this.WeightComboBox, false);
				base.Context.AddCloseFormEvent(new MainForm.Context.CloseFormEvent(this.OnMainFormClosing));
			}
		}

		// Token: 0x04000AA9 RID: 2729
		private const int maxWeightComboBoxItemCount = 35;

		// Token: 0x04000AAA RID: 2730
		private ObjectSetSaver objectSetSaver;

		// Token: 0x04000AAB RID: 2731
		private readonly ObjectSetItemListSource objectSetItemListSource;

		// Token: 0x04000AAC RID: 2732
		private readonly StaticObjectItemListSource staticObjectItemListSource;

		// Token: 0x04000AAD RID: 2733
		private readonly ItemList objectSetItemList;

		// Token: 0x04000AAE RID: 2734
		private readonly ItemList staticObjectItemList;

		// Token: 0x04000AAF RID: 2735
		private readonly FolderItemFilters objectSetItemFiltes;

		// Token: 0x04000AB0 RID: 2736
		private readonly FolderItemFilters staticObjectItemFiltes;

		// Token: 0x04000AB1 RID: 2737
		private bool wasChanged;
	}
}
