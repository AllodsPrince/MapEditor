using System;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using MapEditor.Forms.Base;
using MapEditor.Map.DataProviders;
using MapEditor.Map.MapObjects;
using MapEditor.Resources.Strings;
using Rcs;
using Tools.ItemDataContainer;
using Tools.MapObjects;
using Tools.WindowParams;

namespace MapEditor.Forms.MultiObjectBrowser
{
	// Token: 0x0200006E RID: 110
	public partial class MultiObjectBrowserForm : BaseForm
	{
		// Token: 0x170000C8 RID: 200
		// (get) Token: 0x0600054D RID: 1357 RVA: 0x0002B6A0 File Offset: 0x0002A6A0
		public static string EmptyTypeString
		{
			get
			{
				return MultiObjectBrowserForm.emptyTypeString;
			}
		}

		// Token: 0x0600054E RID: 1358 RVA: 0x0002B6A8 File Offset: 0x0002A6A8
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

		// Token: 0x0600054F RID: 1359 RVA: 0x0002B6FD File Offset: 0x0002A6FD
		private void OnSaveParams(FormParams formParams)
		{
			base.ParamsSaver.FormParams.ResizeInt(1);
			base.ParamsSaver.FormParams.SetInt(0, this.SplitContainer.SplitterDistance);
		}

		// Token: 0x06000550 RID: 1360 RVA: 0x0002B72C File Offset: 0x0002A72C
		private void UpdateMultiObjectsButtons()
		{
			bool itemSelected = !string.IsNullOrEmpty(this.multiObjectItemList.SelectedItem);
			bool activeNameSelected = !string.IsNullOrEmpty(this.MultiObjectNameTextBox.Text);
			this.SaveButton.Enabled = activeNameSelected;
			this.LoadButton.Enabled = itemSelected;
			this.DeleteButton.Enabled = itemSelected;
		}

		// Token: 0x06000551 RID: 1361 RVA: 0x0002B788 File Offset: 0x0002A788
		private void UpdateStaticObjectsButtons()
		{
			bool multiObjectLoaded = this.multiObjectSaver != null;
			bool staticObjectSelected = !string.IsNullOrEmpty(this.staticObjectItemList.SelectedItem);
			bool objectSelected = this.ObjectsTreeView.SelectedNode != null && this.ObjectsTreeView.SelectedNode.Tag is string;
			this.AddStaticObjectButton.Enabled = (multiObjectLoaded && staticObjectSelected);
			this.RemoveStaticObjectButton.Enabled = (multiObjectLoaded && objectSelected);
			this.AutoCompleteButton.Enabled = multiObjectLoaded;
			this.AutoCompleteWithEmptyButton.Enabled = multiObjectLoaded;
			this.ResetButton.Enabled = multiObjectLoaded;
		}

		// Token: 0x06000552 RID: 1362 RVA: 0x0002B828 File Offset: 0x0002A828
		private string GetActiveFileName()
		{
			string name = MultiObjectItemListSource.Folder + this.MultiObjectNameTextBox.Text;
			name = Str.ExtendFileExtention(name, ".xml");
			return name.Replace('\\', '/');
		}

		// Token: 0x06000553 RID: 1363 RVA: 0x0002B864 File Offset: 0x0002A864
		private void SetActiveFileName(string item)
		{
			string _item = item.Replace('\\', '/');
			if (_item.StartsWith(MultiObjectItemListSource.Folder, StringComparison.OrdinalIgnoreCase))
			{
				_item = _item.Substring(MultiObjectItemListSource.Folder.Length);
				_item = Str.CutFileExtention(_item, ".xml");
				this.MultiObjectNameTextBox.Text = _item;
				this.UpdateMultiObjectsButtons();
			}
		}

		// Token: 0x06000554 RID: 1364 RVA: 0x0002B8BC File Offset: 0x0002A8BC
		private TreeNode CreateTreeNode(string stats)
		{
			TreeNode treeNode = null;
			if (base.Context != null && base.Context.ItemDataContainer != null)
			{
				ItemData itemData = base.Context.ItemDataContainer.GetItemData(stats);
				if (itemData != null)
				{
					treeNode = new TreeNode(itemData.Text, itemData.IconIndex, itemData.IconIndex);
					treeNode.ToolTipText = itemData.Tooltip;
				}
			}
			if (treeNode == null)
			{
				treeNode = new TreeNode(Str.CutFilePathAndExtention(stats));
			}
			return treeNode;
		}

		// Token: 0x06000555 RID: 1365 RVA: 0x0002B92C File Offset: 0x0002A92C
		private void PopulateObjects()
		{
			this.ObjectsTreeView.Nodes.Clear();
			if (this.multiObjectSaver != null)
			{
				this.ObjectsTreeView.BeginUpdate();
				ListViewItem emptyTypeItem = new ListViewItem(MultiObjectBrowserForm.emptyTypeString);
				emptyTypeItem.Tag = MultiObjectBrowserForm.emptyTypeString;
				foreach (SerializableMapObjectPack serializableMapObjectPack in this.multiObjectSaver.SerializableMapObjectPacks)
				{
					StaticObjectPack staticObjectPack = serializableMapObjectPack as StaticObjectPack;
					if (staticObjectPack != null)
					{
						TreeNode nodeObject = this.CreateTreeNode(staticObjectPack.Type.Stats);
						nodeObject.Tag = staticObjectPack;
						nodeObject.ForeColor = Color.Blue;
						foreach (MapObjectType mapObjectType in staticObjectPack.MapObjectTypes)
						{
							TreeNode nodeType = this.CreateTreeNode(mapObjectType.Stats);
							nodeType.Tag = mapObjectType.Stats;
							nodeObject.Nodes.Add(nodeType);
						}
						this.ObjectsTreeView.Nodes.Add(nodeObject);
					}
				}
				this.ObjectsTreeView.ExpandAll();
				this.ObjectsTreeView.EndUpdate();
			}
			this.UpdateStaticObjectsButtons();
		}

		// Token: 0x06000556 RID: 1366 RVA: 0x0002BA8C File Offset: 0x0002AA8C
		private void AutoFillTypes(bool addEmpty)
		{
			this.ObjectsTreeView.BeginUpdate();
			foreach (object obj in this.ObjectsTreeView.Nodes)
			{
				TreeNode treeNode = (TreeNode)obj;
				StaticObjectPack staticObjectPack = treeNode.Tag as StaticObjectPack;
				if (staticObjectPack != null)
				{
					string name = treeNode.Text;
					name = name.TrimEnd(MultiObjectBrowserForm.numbers);
					staticObjectPack.MapObjectTypes.Clear();
					treeNode.Nodes.Clear();
					foreach (object obj2 in this.StaticObjectsListView.Items)
					{
						ListViewItem listViewItem = (ListViewItem)obj2;
						string typeName = listViewItem.Text.TrimEnd(MultiObjectBrowserForm.numbers);
						if (name == typeName)
						{
							MapObjectType type = new MapObjectType(MapObjectFactory.Type.StaticObject, listViewItem.Tag as string);
							if (!staticObjectPack.MapObjectTypes.Contains(type))
							{
								staticObjectPack.MapObjectTypes.Add(type);
								TreeNode node = this.CreateTreeNode(type.Stats);
								node.Tag = (listViewItem.Tag as string);
								if (listViewItem.Text == MultiObjectBrowserForm.emptyTypeString)
								{
									treeNode.Nodes.Insert(0, node);
								}
								else
								{
									treeNode.Nodes.Add(node);
								}
							}
						}
					}
					if (addEmpty)
					{
						MapObjectType type2 = new MapObjectType(MapObjectFactory.Type.StaticObject, MultiObjectBrowserForm.emptyTypeString);
						if (!staticObjectPack.MapObjectTypes.Contains(type2))
						{
							staticObjectPack.MapObjectTypes.Add(type2);
							TreeNode node2 = new TreeNode(MultiObjectBrowserForm.emptyTypeString);
							node2.Tag = MultiObjectBrowserForm.emptyTypeString;
							treeNode.Nodes.Insert(0, node2);
						}
					}
				}
			}
			this.ObjectsTreeView.EndUpdate();
			this.UpdateStaticObjectsButtons();
			this.wasChanged = true;
		}

		// Token: 0x06000557 RID: 1367 RVA: 0x0002BCB4 File Offset: 0x0002ACB4
		private void ResetTypes()
		{
			foreach (object obj in this.ObjectsTreeView.Nodes)
			{
				TreeNode treeNode = (TreeNode)obj;
				StaticObjectPack staticObjectPack = treeNode.Tag as StaticObjectPack;
				if (staticObjectPack != null)
				{
					staticObjectPack.MapObjectTypes.Clear();
					treeNode.Nodes.Clear();
					MapObjectType type = new MapObjectType(MapObjectFactory.Type.StaticObject, staticObjectPack.Type.Stats);
					staticObjectPack.MapObjectTypes.Add(type);
					TreeNode node = this.CreateTreeNode(staticObjectPack.Type.Stats);
					node.Tag = staticObjectPack.Type.Stats;
					treeNode.Nodes.Add(node);
				}
			}
			this.UpdateStaticObjectsButtons();
			this.wasChanged = false;
		}

		// Token: 0x06000558 RID: 1368 RVA: 0x0002BDAC File Offset: 0x0002ADAC
		private bool Save(string fileName, bool askifRewrite)
		{
			if (this.multiObjectSaver == null || string.IsNullOrEmpty(fileName))
			{
				return false;
			}
			if (askifRewrite && File.Exists(fileName) && MessageBox.Show(Strings.MAP_EDITOR_OVERWRITE, Strings.MAP_EDITOR_FILE_EXISTS, MessageBoxButtons.YesNo) != DialogResult.Yes)
			{
				return false;
			}
			MultiObjectSaver.Save(this.multiObjectSaver, fileName, true);
			this.multiObjectItemList.Refresh();
			this.wasChanged = false;
			return true;
		}

		// Token: 0x06000559 RID: 1369 RVA: 0x0002BE0C File Offset: 0x0002AE0C
		private bool PrompToSave()
		{
			if (this.wasChanged)
			{
				string prevFileName = this.GetActiveFileName();
				if (!string.IsNullOrEmpty(prevFileName))
				{
					DialogResult result = MessageBox.Show(Strings.SAVE_MULTI_OBJECT_QUESTION, this.Text, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
					if (result == DialogResult.Yes)
					{
						return this.Save(prevFileName, false);
					}
					if (result == DialogResult.Cancel)
					{
						return false;
					}
				}
			}
			return true;
		}

		// Token: 0x0600055A RID: 1370 RVA: 0x0002BE58 File Offset: 0x0002AE58
		private void OnMultiObjectItemList_ItemSelected(ItemList sender, string item)
		{
			this.UpdateMultiObjectsButtons();
		}

		// Token: 0x0600055B RID: 1371 RVA: 0x0002BE60 File Offset: 0x0002AE60
		private void OnMultiObjectItemList_ItemUnselected(ItemList sender, string item)
		{
			this.UpdateMultiObjectsButtons();
		}

		// Token: 0x0600055C RID: 1372 RVA: 0x0002BE68 File Offset: 0x0002AE68
		private void OnMultiObjectItemList_ListCleared(ItemList sender, string item)
		{
			this.UpdateMultiObjectsButtons();
		}

		// Token: 0x0600055D RID: 1373 RVA: 0x0002BE70 File Offset: 0x0002AE70
		private void OnStaticObjectItemList_ItemSelected(ItemList sender, string item)
		{
			this.UpdateStaticObjectsButtons();
		}

		// Token: 0x0600055E RID: 1374 RVA: 0x0002BE78 File Offset: 0x0002AE78
		private void OnStaticObjectItemList_ItemUnselected(ItemList sender, string item)
		{
			this.UpdateStaticObjectsButtons();
		}

		// Token: 0x0600055F RID: 1375 RVA: 0x0002BE80 File Offset: 0x0002AE80
		private void OnStaticObjectItemList_ListCleared(ItemList sender, string item)
		{
			this.UpdateStaticObjectsButtons();
		}

		// Token: 0x06000560 RID: 1376 RVA: 0x0002BE88 File Offset: 0x0002AE88
		private void MultiObjectsFiltersButton_Click(object sender, EventArgs e)
		{
			this.multiObjectItemFiltes.ShowDialog(this.multiObjectItemList, this);
		}

		// Token: 0x06000561 RID: 1377 RVA: 0x0002BE9D File Offset: 0x0002AE9D
		private void StaticObjectsFiltersButton_Click(object sender, EventArgs e)
		{
			this.staticObjectItemFiltes.ShowDialog(this.staticObjectItemList, this);
		}

		// Token: 0x06000562 RID: 1378 RVA: 0x0002BEB4 File Offset: 0x0002AEB4
		private void MultiObjectBrowser_Load(object sender, EventArgs e)
		{
			this.multiObjectItemList.ItemSources.Add(this.multiObjectItemListSource);
			this.multiObjectItemFiltes.Bind(this.multiObjectItemList.ItemFilters);
			this.multiObjectItemList.Bind(this.MultiObjectsListView, this.MultiObjectsFiltersComboBox);
			this.staticObjectItemList.ItemSources.Add(this.staticObjectItemListSource);
			this.staticObjectItemFiltes.Bind(this.staticObjectItemList.ItemFilters);
			this.staticObjectItemList.Bind(this.StaticObjectsListView, this.StaticObjectsFiltersComboBox);
			this.PopulateObjects();
			this.UpdateMultiObjectsButtons();
			this.UpdateStaticObjectsButtons();
		}

		// Token: 0x06000563 RID: 1379 RVA: 0x0002BF5C File Offset: 0x0002AF5C
		private void MultiObjectBrowser_FormClosing(object sender, FormClosingEventArgs e)
		{
			if (base.AllowClose)
			{
				this.multiObjectItemList.Unbind();
				this.multiObjectItemFiltes.Unbind();
				this.multiObjectItemList.ItemSources.Clear();
				this.staticObjectItemList.Unbind();
				this.staticObjectItemFiltes.Unbind();
				this.staticObjectItemList.ItemSources.Clear();
			}
		}

		// Token: 0x06000564 RID: 1380 RVA: 0x0002BFBD File Offset: 0x0002AFBD
		private void ObjectsTreeView_DoubleClick(object sender, EventArgs e)
		{
			this.RemoveStaticObjectButton_Click(sender, e);
		}

		// Token: 0x06000565 RID: 1381 RVA: 0x0002BFC7 File Offset: 0x0002AFC7
		private void MultiObjectsListView_DoubleClick(object sender, EventArgs e)
		{
			this.LoadButton_Click(sender, e);
		}

		// Token: 0x06000566 RID: 1382 RVA: 0x0002BFD1 File Offset: 0x0002AFD1
		private void StaticObjectsListView_DoubleClick(object sender, EventArgs e)
		{
			this.AddStaticObjectButton_Click(sender, e);
		}

		// Token: 0x06000567 RID: 1383 RVA: 0x0002BFDB File Offset: 0x0002AFDB
		private void SaveButton_Click(object sender, EventArgs e)
		{
			this.Save(this.GetActiveFileName(), true);
		}

		// Token: 0x06000568 RID: 1384 RVA: 0x0002BFEC File Offset: 0x0002AFEC
		private void LoadButton_Click(object sender, EventArgs e)
		{
			if (!this.PrompToSave())
			{
				return;
			}
			this.SetActiveFileName(this.multiObjectItemList.SelectedItem);
			MultiObjectSaver newMultiObjectSaver = MultiObjectSaver.Load(this.GetActiveFileName());
			if (newMultiObjectSaver != null)
			{
				this.multiObjectSaver = newMultiObjectSaver;
				this.PopulateObjects();
			}
			this.wasChanged = false;
		}

		// Token: 0x06000569 RID: 1385 RVA: 0x0002C038 File Offset: 0x0002B038
		private void DeleteButton_Click(object sender, EventArgs e)
		{
			if (this.MultiObjectsListView.SelectedItems.Count > 0)
			{
				ListViewItem item = this.MultiObjectsListView.SelectedItems[0];
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
					this.PopulateObjects();
					this.multiObjectItemList.Refresh();
					this.wasChanged = false;
				}
			}
		}

		// Token: 0x0600056A RID: 1386 RVA: 0x0002C0D8 File Offset: 0x0002B0D8
		private void MultiObjectNameButton_Click(object sender, EventArgs e)
		{
			SaveFileDialog saveFileDialog = new SaveFileDialog();
			saveFileDialog.Filter = "XML Files|*.xml|All Files|*.*";
			saveFileDialog.RestoreDirectory = true;
			saveFileDialog.InitialDirectory = MultiObjectItemListSource.Folder.Replace('/', '\\');
			if (!Directory.Exists(saveFileDialog.InitialDirectory))
			{
				Directory.CreateDirectory(saveFileDialog.InitialDirectory);
			}
			if (saveFileDialog.ShowDialog(this) == DialogResult.OK)
			{
				this.SetActiveFileName(saveFileDialog.FileName);
			}
		}

		// Token: 0x0600056B RID: 1387 RVA: 0x0002C140 File Offset: 0x0002B140
		private void AddStaticObjectButton_Click(object sender, EventArgs e)
		{
			if (this.StaticObjectsListView.SelectedItems.Count < 1 || this.ObjectsTreeView.SelectedNode == null)
			{
				return;
			}
			StaticObjectPack staticObjectPack = this.ObjectsTreeView.SelectedNode.Tag as StaticObjectPack;
			TreeNode objectNode = this.ObjectsTreeView.SelectedNode;
			if (staticObjectPack == null && this.ObjectsTreeView.SelectedNode.Parent != null)
			{
				staticObjectPack = (this.ObjectsTreeView.SelectedNode.Parent.Tag as StaticObjectPack);
				objectNode = this.ObjectsTreeView.SelectedNode.Parent;
			}
			if (staticObjectPack != null)
			{
				foreach (object obj in this.StaticObjectsListView.SelectedItems)
				{
					ListViewItem listViewItem = (ListViewItem)obj;
					MapObjectType type = new MapObjectType(MapObjectFactory.Type.StaticObject, listViewItem.Tag as string);
					if (!staticObjectPack.MapObjectTypes.Contains(type))
					{
						staticObjectPack.MapObjectTypes.Add(type);
						TreeNode node = new TreeNode(listViewItem.Text);
						node.Tag = (listViewItem.Tag as string);
						if (listViewItem.Text == MultiObjectBrowserForm.emptyTypeString)
						{
							objectNode.Nodes.Insert(0, node);
						}
						else
						{
							objectNode.Nodes.Add(node);
						}
					}
				}
				this.UpdateStaticObjectsButtons();
				this.wasChanged = true;
			}
		}

		// Token: 0x0600056C RID: 1388 RVA: 0x0002C2BC File Offset: 0x0002B2BC
		private void RemoveStaticObjectButton_Click(object sender, EventArgs e)
		{
			if (this.ObjectsTreeView.SelectedNode == null)
			{
				return;
			}
			StaticObjectPack staticObjectPack = null;
			TreeNode objectNode = null;
			if (this.ObjectsTreeView.SelectedNode.Parent != null)
			{
				staticObjectPack = (this.ObjectsTreeView.SelectedNode.Parent.Tag as StaticObjectPack);
				objectNode = this.ObjectsTreeView.SelectedNode.Parent;
			}
			if (staticObjectPack != null && staticObjectPack.MapObjectTypes.Count > 1)
			{
				MapObjectType type = new MapObjectType(MapObjectFactory.Type.StaticObject, this.ObjectsTreeView.SelectedNode.Tag as string);
				if (staticObjectPack.MapObjectTypes.Contains(type))
				{
					staticObjectPack.MapObjectTypes.Remove(type);
					objectNode.Nodes.Remove(this.ObjectsTreeView.SelectedNode);
				}
				this.UpdateStaticObjectsButtons();
			}
			this.wasChanged = true;
		}

		// Token: 0x0600056D RID: 1389 RVA: 0x0002C389 File Offset: 0x0002B389
		private void AutoCompleteButton_Click(object sender, EventArgs e)
		{
			this.AutoFillTypes(false);
		}

		// Token: 0x0600056E RID: 1390 RVA: 0x0002C392 File Offset: 0x0002B392
		private void AutoCompleteWithEmptyButton_Click(object sender, EventArgs e)
		{
			this.AutoFillTypes(true);
		}

		// Token: 0x0600056F RID: 1391 RVA: 0x0002C39B File Offset: 0x0002B39B
		private void ResetButton_Click(object sender, EventArgs e)
		{
			this.ResetTypes();
		}

		// Token: 0x06000570 RID: 1392 RVA: 0x0002C3A3 File Offset: 0x0002B3A3
		private void MultiObjectNameTextBox_TextChanged(object sender, EventArgs e)
		{
			this.UpdateMultiObjectsButtons();
		}

		// Token: 0x06000571 RID: 1393 RVA: 0x0002C3AB File Offset: 0x0002B3AB
		protected override void WndProc(ref Message message)
		{
			if (base.Visible && !base.AllowClose && message.Msg == 16 && !this.PrompToSave())
			{
				return;
			}
			base.WndProc(ref message);
		}

		// Token: 0x06000572 RID: 1394 RVA: 0x0002C3D7 File Offset: 0x0002B3D7
		private void OnMainFormClosing(FormClosingEventArgs e)
		{
			if (base.Visible && e.CloseReason == CloseReason.UserClosing && !this.PrompToSave())
			{
				e.Cancel = true;
			}
		}

		// Token: 0x06000573 RID: 1395 RVA: 0x0002C3FC File Offset: 0x0002B3FC
		public MultiObjectBrowserForm(MainForm.Context context) : base(EditorEnvironment.EditorFormsFolder + "MultiObjectBrowserForm.xml", context)
		{
			if (base.ParamsSaver != null)
			{
				base.ParamsSaver.LoadParams += this.OnLoadParams;
				base.ParamsSaver.SaveParams += this.OnSaveParams;
			}
			this.InitializeComponent();
			if (base.Context != null && base.Context.ItemDataContainer != null)
			{
				this.multiObjectItemListSource = new MultiObjectItemListSource();
				this.multiObjectItemList = new ItemList(EditorEnvironment.EditorFormsFolder + "MultiObjectBrowser/MultiObjectsItemList.xml", base.Context.ItemDataContainer, false);
				ItemList itemList = this.multiObjectItemList;
				itemList.ItemSelected = (ItemList.ItemEvent)Delegate.Combine(itemList.ItemSelected, new ItemList.ItemEvent(this.OnMultiObjectItemList_ItemSelected));
				ItemList itemList2 = this.multiObjectItemList;
				itemList2.ItemUnselected = (ItemList.ItemEvent)Delegate.Combine(itemList2.ItemUnselected, new ItemList.ItemEvent(this.OnMultiObjectItemList_ItemUnselected));
				ItemList itemList3 = this.multiObjectItemList;
				itemList3.ListCleared = (ItemList.ItemEvent)Delegate.Combine(itemList3.ListCleared, new ItemList.ItemEvent(this.OnMultiObjectItemList_ListCleared));
				this.multiObjectItemFiltes = new FolderItemFilters(EditorEnvironment.EditorFolder + "Filters/MultiObjectBrowser/MultiObjectsFilters.xml", EditorEnvironment.EditorFormsFolder);
				this.staticObjectItemListSource = new StaticObjectItemListSource();
				this.staticObjectItemList = new ItemList(EditorEnvironment.EditorFormsFolder + "MultiObjectBrowser/StaticObjectsItemList.xml", base.Context.ItemDataContainer, true);
				ItemList itemList4 = this.staticObjectItemList;
				itemList4.ItemSelected = (ItemList.ItemEvent)Delegate.Combine(itemList4.ItemSelected, new ItemList.ItemEvent(this.OnStaticObjectItemList_ItemSelected));
				ItemList itemList5 = this.staticObjectItemList;
				itemList5.ItemUnselected = (ItemList.ItemEvent)Delegate.Combine(itemList5.ItemUnselected, new ItemList.ItemEvent(this.OnStaticObjectItemList_ItemUnselected));
				ItemList itemList6 = this.staticObjectItemList;
				itemList6.ListCleared = (ItemList.ItemEvent)Delegate.Combine(itemList6.ListCleared, new ItemList.ItemEvent(this.OnStaticObjectItemList_ListCleared));
				this.staticObjectItemFiltes = new FolderItemFilters(EditorEnvironment.EditorFolder + "Filters/MultiObjectBrowser/StaticObjectsFilters.xml", EditorEnvironment.EditorFormsFolder);
				this.ObjectsTreeView.ImageList = base.Context.ItemDataContainer.GetSmallImageList();
				base.Context.AddCloseFormEvent(new MainForm.Context.CloseFormEvent(this.OnMainFormClosing));
			}
		}

		// Token: 0x170000C9 RID: 201
		// (get) Token: 0x06000574 RID: 1396 RVA: 0x0002C632 File Offset: 0x0002B632
		// (set) Token: 0x06000575 RID: 1397 RVA: 0x0002C63A File Offset: 0x0002B63A
		public MultiObjectSaver MultiObjectSaver
		{
			get
			{
				return this.multiObjectSaver;
			}
			set
			{
				this.multiObjectSaver = value;
				this.PopulateObjects();
			}
		}

		// Token: 0x06000576 RID: 1398 RVA: 0x0002C649 File Offset: 0x0002B649
		private void CloseButton_Click(object sender, EventArgs e)
		{
			base.Close();
		}

		// Token: 0x040003F2 RID: 1010
		private static readonly string emptyTypeString = "< Empty >";

		// Token: 0x040003F3 RID: 1011
		private static readonly char[] numbers = "0123456789_".ToCharArray();

		// Token: 0x040003F4 RID: 1012
		private MultiObjectSaver multiObjectSaver;

		// Token: 0x040003F5 RID: 1013
		private readonly MultiObjectItemListSource multiObjectItemListSource;

		// Token: 0x040003F6 RID: 1014
		private readonly StaticObjectItemListSource staticObjectItemListSource;

		// Token: 0x040003F7 RID: 1015
		private readonly ItemList multiObjectItemList;

		// Token: 0x040003F8 RID: 1016
		private readonly ItemList staticObjectItemList;

		// Token: 0x040003F9 RID: 1017
		private readonly FolderItemFilters multiObjectItemFiltes;

		// Token: 0x040003FA RID: 1018
		private readonly FolderItemFilters staticObjectItemFiltes;

		// Token: 0x040003FB RID: 1019
		private bool wasChanged;
	}
}
