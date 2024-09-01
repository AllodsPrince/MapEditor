using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Windows.Forms;
using MapEditor.Forms.Base;
using MapEditor.Map;
using MapEditor.Map.DataProviders;
using MapEditor.Map.MapObjects;
using MapEditor.Resources.Strings;
using Tools.ItemDataContainer;
using Tools.Landscape.LandscapeToolParams;
using Tools.WindowParams;

namespace MapEditor.Forms.RoadParamsBrowser
{
	// Token: 0x020001B4 RID: 436
	public partial class RoadParamsBrowserForm : BaseForm
	{
		// Token: 0x060014C6 RID: 5318 RVA: 0x00095438 File Offset: 0x00094438
		private void OnPostLoadParams(FormParams formParams)
		{
			this.roadParamsItemList.ItemSources.Add(this.roadParamsItemSource);
			this.roadParamsItemFiltes.Bind(this.roadParamsItemList.ItemFilters);
			this.roadParamsItemList.Bind(this.RoadListView, this.RoadFilterComboBox);
			this.tileItemFiltes.Bind(this.tileItemList.ItemFilters);
			this.tileItemList.Bind(this.AvailableTilesListView, this.AvailableTilesFilterComboBox);
			this.staticObjectItemList.ItemSources.Add(this.staticObjectItemListSource);
			this.staticObjectItemFiltes.Bind(this.staticObjectItemList.ItemFilters);
			this.staticObjectItemList.Bind(this.AvailableStaticObjectsListView, this.AvailableStaticObjectsFilterComboBox);
			this.roadItemSetItemList.ItemSources.Add(this.roadItemSetItemListSource);
			this.roadItemSetItemList.Bind(this.RoadItemSetItemsListView, null);
			this.CheckRoadParams();
			this.PopulateRoadParamsControls(null);
			this.created = true;
			this.modified = false;
		}

		// Token: 0x060014C7 RID: 5319 RVA: 0x0009553C File Offset: 0x0009453C
		private void OnSaveParams(FormParams formParams)
		{
			this.roadParamsItemList.Unbind();
			this.roadParamsItemFiltes.Unbind();
			this.roadParamsItemList.ItemSources.Clear();
			this.tileItemList.Unbind();
			this.tileItemFiltes.Unbind();
			this.tileItemList.ItemSources.Clear();
			this.staticObjectItemList.Unbind();
			this.staticObjectItemFiltes.Unbind();
			this.staticObjectItemList.ItemSources.Clear();
			this.roadItemSetItemList.Unbind();
			this.roadItemSetItemList.ItemSources.Clear();
		}

		// Token: 0x060014C8 RID: 5320 RVA: 0x000955D6 File Offset: 0x000945D6
		private void OnRoadParamsChanged(RoadParams _roadParams)
		{
			if (this.created)
			{
				this.modified = true;
				this.UpdateMainControls();
				this.UpdateRoadStripeControls();
				this.UpdateActiveRoadStripeListViewItem();
				this.UpdateRoadItemSetControls();
				this.UpdateActiveRoadItemSetListViewItem();
			}
		}

		// Token: 0x060014C9 RID: 5321 RVA: 0x00095605 File Offset: 0x00094605
		private void RoadNameTextBox_TextChanged(object sender, EventArgs e)
		{
			if (this.created)
			{
				this.modified = true;
				this.UpdateMainControls();
			}
		}

		// Token: 0x060014CA RID: 5322 RVA: 0x0009561C File Offset: 0x0009461C
		private void UpdateMainControls()
		{
			bool roadParamsExisting = this.roadParams != null;
			bool roadParamsFileNameExisting = !string.IsNullOrEmpty(this.RoadNameTextBox.Text);
			bool roadParamsSelectionExisting = this.RoadListView.SelectedItems.Count > 0;
			this.LoadButton.Enabled = roadParamsSelectionExisting;
			this.RoadContextMenuItemLoad.Enabled = roadParamsSelectionExisting;
			this.SaveButton.Enabled = (roadParamsFileNameExisting && roadParamsExisting && this.modified);
			this.SaveToButton.Enabled = (roadParamsSelectionExisting && roadParamsExisting);
			this.RoadContextMenuItemSaveTo.Enabled = (roadParamsSelectionExisting && roadParamsExisting);
		}

		// Token: 0x060014CB RID: 5323 RVA: 0x000956B4 File Offset: 0x000946B4
		private void UpdateActiveRoadStripeListViewItem()
		{
			if (this.roadParams != null && this.RoadStripeListView.SelectedItems.Count > 0)
			{
				ListViewItem listViewItem = this.RoadStripeListView.SelectedItems[0];
				int selectedIndex = (int)listViewItem.Tag;
				if (selectedIndex >= 0 && selectedIndex < this.roadParams.Stripes.Count)
				{
					RoadStripe stripe = this.roadParams.Stripes[selectedIndex];
					if (stripe != null)
					{
						RoadParamsBrowserForm.FillListViewItemByRoadStripe(listViewItem, stripe);
					}
				}
			}
		}

		// Token: 0x060014CC RID: 5324 RVA: 0x00095730 File Offset: 0x00094730
		private void UpdateRoadStripeControls()
		{
			bool roadParamsExisting = this.roadParams != null;
			bool roadStripeSelected = false;
			bool roadStripeTileSelected = false;
			bool roadTileSelected = false;
			bool availableTileSelected = this.AvailableTilesListView.SelectedItems.Count > 0;
			if (roadParamsExisting)
			{
				roadTileSelected = (this.roadParams.Tile != -1);
				RoadStripe stripe = this.GetSelectedRoadStripe();
				if (stripe != null)
				{
					roadStripeSelected = true;
					roadStripeTileSelected = (stripe.Tile != -1);
				}
				else
				{
					roadStripeSelected = false;
					roadStripeTileSelected = false;
				}
			}
			this.RoadStripeButtonAdd.Enabled = roadParamsExisting;
			this.RoadStripeContextMenuItemAdd.Enabled = roadParamsExisting;
			this.RoadStripeButtonRemove.Enabled = roadStripeSelected;
			this.RoadStripeContextMenuItemRemove.Enabled = roadStripeSelected;
			this.RoadStripeButtonDefaults.Enabled = roadStripeSelected;
			this.RoadStripeContextMenuItemDefaults.Enabled = roadStripeSelected;
			this.SetRoadTileButton.Enabled = (roadParamsExisting && availableTileSelected);
			this.SetRoadTileButton.Enabled = (roadParamsExisting && availableTileSelected);
			this.AvailableTilesContextMenuItemSetRoadTile.Enabled = (roadParamsExisting && availableTileSelected);
			this.ClearRoadTileButton.Enabled = roadTileSelected;
			this.SetStripeTileButton.Enabled = (roadStripeSelected && availableTileSelected);
			this.AvailableTilesContextMenuItemSetStripeTile.Enabled = (roadStripeSelected && availableTileSelected);
			this.ClearStripeTileButton.Enabled = roadStripeTileSelected;
		}

		// Token: 0x060014CD RID: 5325 RVA: 0x0009585C File Offset: 0x0009485C
		private void UpdateActiveRoadItemSetListViewItem()
		{
			if (this.roadParams != null && this.RoadItemSetListView.SelectedItems.Count > 0)
			{
				ListViewItem listViewItem = this.RoadItemSetListView.SelectedItems[0];
				int selectedIndex = (int)listViewItem.Tag;
				if (selectedIndex >= 0 && selectedIndex < this.roadParams.ItemSets.Count)
				{
					RoadItemSet itemSet = this.roadParams.ItemSets[selectedIndex];
					if (itemSet != null)
					{
						RoadParamsBrowserForm.FillListViewItemByRoadItemSet(listViewItem, itemSet);
					}
				}
			}
		}

		// Token: 0x060014CE RID: 5326 RVA: 0x000958D8 File Offset: 0x000948D8
		private void UpdateRoadItemSetControls()
		{
			bool roadParamsExisting = this.roadParams != null;
			bool roadItemSetSelected = false;
			bool availableStaticObjectsSelected = this.AvailableStaticObjectsListView.SelectedItems.Count > 0;
			bool itemSetItemsSelected = false;
			bool itemSetItemsExists = false;
			if (roadParamsExisting)
			{
				RoadItemSet itemSet = this.GetSelectedRoadItemSet();
				roadItemSetSelected = (itemSet != null);
				itemSetItemsSelected = (itemSet != null && this.RoadItemSetItemsListView.SelectedItems.Count > 0);
				if (itemSet != null)
				{
					roadItemSetSelected = true;
					itemSetItemsSelected = (this.RoadItemSetItemsListView.SelectedItems.Count > 0);
					itemSetItemsExists = (itemSet.Items.Items.Count > 0);
				}
			}
			this.RoadItemSetButtonAdd.Enabled = roadParamsExisting;
			this.RoadItemSetContextMenuItemAdd.Enabled = roadParamsExisting;
			this.RoadItemSetButtonRemove.Enabled = roadItemSetSelected;
			this.RoadItemSetContextMenuItemRemove.Enabled = roadItemSetSelected;
			this.RoadItemSetButtonDefaults.Enabled = roadItemSetSelected;
			this.RoadItemSetContextMenuItemDefaults.Enabled = roadItemSetSelected;
			this.StaticObjectAddButton.Enabled = (roadItemSetSelected && availableStaticObjectsSelected);
			this.AvailableStaticObjectsContextMenuAdd.Enabled = (roadItemSetSelected && availableStaticObjectsSelected);
			this.StaticObjectRemoveButton.Enabled = itemSetItemsSelected;
			this.RoadItemSetItemsContextMenuRemove.Enabled = itemSetItemsSelected;
			this.StaticObjectSetButton.Enabled = itemSetItemsSelected;
			this.RoadItemSetItemsContextMenuSet.Enabled = itemSetItemsSelected;
			this.StaticObjectAutoCompleteButton.Enabled = itemSetItemsExists;
			this.RoadItemSetItemsContextMenuAuto.Enabled = itemSetItemsExists;
		}

		// Token: 0x060014CF RID: 5327 RVA: 0x00095A24 File Offset: 0x00094A24
		private string GetActiveFileName()
		{
			string name = LandscapeRoadItemSource.Folder + this.RoadNameTextBox.Text;
			name = Str.ExtendFileExtention(name, ".xml");
			return name.Replace('\\', '/');
		}

		// Token: 0x060014D0 RID: 5328 RVA: 0x00095A60 File Offset: 0x00094A60
		private void SetActiveFileName(string item)
		{
			string _item = item.Replace('\\', '/');
			if (_item.StartsWith(LandscapeRoadItemSource.Folder, StringComparison.OrdinalIgnoreCase))
			{
				_item = _item.Substring(LandscapeRoadItemSource.Folder.Length);
				_item = Str.CutFileExtention(_item, ".xml");
				this.created = false;
				this.RoadNameTextBox.Text = _item;
				this.created = true;
				this.UpdateMainControls();
			}
		}

		// Token: 0x060014D1 RID: 5329 RVA: 0x00095AC3 File Offset: 0x00094AC3
		private int GetSelectedRoadStripeIndex()
		{
			if (this.roadParams != null && this.RoadStripeListView.SelectedItems.Count > 0)
			{
				return (int)this.RoadStripeListView.SelectedItems[0].Tag;
			}
			return -1;
		}

		// Token: 0x060014D2 RID: 5330 RVA: 0x00095B00 File Offset: 0x00094B00
		private RoadStripe GetSelectedRoadStripe()
		{
			if (this.roadParams != null)
			{
				int stripeIndex = this.GetSelectedRoadStripeIndex();
				if (stripeIndex >= 0 && stripeIndex < this.roadParams.Stripes.Count)
				{
					return this.roadParams.Stripes[stripeIndex];
				}
			}
			return null;
		}

		// Token: 0x060014D3 RID: 5331 RVA: 0x00095B46 File Offset: 0x00094B46
		private int GetSelectedTile()
		{
			return LandscapeTileItemSource.GetTileIndex(this.tileItemList.SelectedItem);
		}

		// Token: 0x060014D4 RID: 5332 RVA: 0x00095B58 File Offset: 0x00094B58
		private int GetSelectedRoadItemSetIndex()
		{
			if (this.roadParams != null && this.RoadItemSetListView.SelectedItems.Count > 0)
			{
				return (int)this.RoadItemSetListView.SelectedItems[0].Tag;
			}
			return -1;
		}

		// Token: 0x060014D5 RID: 5333 RVA: 0x00095B94 File Offset: 0x00094B94
		private RoadItemSet GetSelectedRoadItemSet()
		{
			if (this.roadParams != null)
			{
				int itemSetIndex = this.GetSelectedRoadItemSetIndex();
				if (itemSetIndex >= 0 && itemSetIndex < this.roadParams.ItemSets.Count)
				{
					return this.roadParams.ItemSets[itemSetIndex];
				}
			}
			return null;
		}

		// Token: 0x060014D6 RID: 5334 RVA: 0x00095BDA File Offset: 0x00094BDA
		private List<string> GetSelectedStaticObjects()
		{
			return this.staticObjectItemList.SelectedItems;
		}

		// Token: 0x060014D7 RID: 5335 RVA: 0x00095BE7 File Offset: 0x00094BE7
		private List<string> GetSelectedItemSetItemObjects()
		{
			return this.roadItemSetItemList.SelectedItems;
		}

		// Token: 0x060014D8 RID: 5336 RVA: 0x00095BF4 File Offset: 0x00094BF4
		private int GetSelectedStaticObjectWeight()
		{
			int weight;
			if (int.TryParse(this.StaticObjectWeightComboBox.Text, out weight))
			{
				return weight;
			}
			return 1;
		}

		// Token: 0x060014D9 RID: 5337 RVA: 0x00095C18 File Offset: 0x00094C18
		private void PopulateRoadSelectionControls()
		{
			this.RoadPropertyGrid.SelectedObject = this.roadParams;
			if (this.roadParams == null || this.roadParams.Tile == -1)
			{
				this.RoadImage.Image = null;
				this.TileToolTip.SetToolTip(this.RoadImage, string.Empty);
				return;
			}
			ItemData itemData = this.tileItemList.GetItemData(LandscapeTileItemSource.GetTileItem(this.roadParams.Tile, this.continentName));
			if (itemData != null)
			{
				if (this.AvailableTilesListView.LargeImageList != null)
				{
					this.RoadImage.Image = this.AvailableTilesListView.LargeImageList.Images[itemData.IconIndex];
				}
				this.TileToolTip.SetToolTip(this.RoadImage, itemData.Tooltip);
			}
		}

		// Token: 0x060014DA RID: 5338 RVA: 0x00095CE0 File Offset: 0x00094CE0
		private static void FillListViewItemByRoadStripe(ListViewItem listViewItem, RoadStripe stripe)
		{
			if (listViewItem != null && stripe != null)
			{
				listViewItem.Text = stripe.Name;
				if (listViewItem.SubItems.Count < 4)
				{
					listViewItem.SubItems.Add(string.Empty);
					listViewItem.SubItems.Add(string.Empty);
					listViewItem.SubItems.Add(string.Empty);
				}
				listViewItem.SubItems[1].Text = stripe.Anchor.ToString();
				if (Math.Abs(stripe.TileWidth - stripe.HeightWidth) < MathConsts.DOUBLE_EPSILON)
				{
					listViewItem.SubItems[2].Text = stripe.TileWidth.ToString();
				}
				else if (stripe.TileWidth < stripe.HeightWidth)
				{
					listViewItem.SubItems[2].Text = string.Format("{0}...{1}", stripe.TileWidth, stripe.HeightWidth);
				}
				else
				{
					listViewItem.SubItems[2].Text = string.Format("{0}...{1}", stripe.HeightWidth, stripe.TileWidth);
				}
				listViewItem.SubItems[3].Text = stripe.Tile.ToString();
			}
		}

		// Token: 0x060014DB RID: 5339 RVA: 0x00095E34 File Offset: 0x00094E34
		private static void FillListViewItemByRoadItemSet(ListViewItem listViewItem, RoadItemSet itemSet)
		{
			if (listViewItem != null && itemSet != null)
			{
				listViewItem.Text = itemSet.Name;
				if (listViewItem.SubItems.Count < 5)
				{
					listViewItem.SubItems.Add(string.Empty);
					listViewItem.SubItems.Add(string.Empty);
					listViewItem.SubItems.Add(string.Empty);
					listViewItem.SubItems.Add(string.Empty);
				}
				listViewItem.SubItems[1].Text = itemSet.Anchor.ToString();
				listViewItem.SubItems[2].Text = itemSet.AlignType.ToString();
				listViewItem.SubItems[3].Text = string.Format("{0}...{1}", itemSet.MinDistance, itemSet.MaxDistance);
				listViewItem.SubItems[4].Text = itemSet.Items.Items.Count.ToString();
			}
		}

		// Token: 0x060014DC RID: 5340 RVA: 0x00095F48 File Offset: 0x00094F48
		private void PopulateRoadStripeListView()
		{
			this.created = false;
			this.RoadStripeListView.Items.Clear();
			if (this.roadParams != null)
			{
				for (int index = 0; index < this.roadParams.Stripes.Count; index++)
				{
					RoadStripe stripe = this.roadParams.Stripes[index];
					ListViewItem listViewItem = new ListViewItem();
					listViewItem.Tag = index;
					RoadParamsBrowserForm.FillListViewItemByRoadStripe(listViewItem, stripe);
					this.RoadStripeListView.Items.Add(listViewItem);
				}
			}
			this.UpdateRoadStripeControls();
			this.created = true;
		}

		// Token: 0x060014DD RID: 5341 RVA: 0x00095FDC File Offset: 0x00094FDC
		private void PopulateRoadStripeSelectionControls()
		{
			this.created = false;
			RoadStripe stripe = this.GetSelectedRoadStripe();
			this.RoadStripePropertyGrid.SelectedObject = stripe;
			if (stripe == null || stripe.Tile == -1)
			{
				this.RoadStripeImage.Image = null;
				this.TileToolTip.SetToolTip(this.RoadStripeImage, string.Empty);
			}
			else
			{
				ItemData itemData = this.tileItemList.GetItemData(LandscapeTileItemSource.GetTileItem(stripe.Tile, this.continentName));
				if (itemData != null)
				{
					if (this.AvailableTilesListView.LargeImageList != null)
					{
						this.RoadStripeImage.Image = this.AvailableTilesListView.LargeImageList.Images[itemData.IconIndex];
					}
					this.TileToolTip.SetToolTip(this.RoadStripeImage, itemData.Tooltip);
				}
			}
			this.created = true;
		}

		// Token: 0x060014DE RID: 5342 RVA: 0x000960A4 File Offset: 0x000950A4
		private void PopulateRoadStripeControls()
		{
			this.PopulateRoadStripeListView();
			this.PopulateRoadStripeSelectionControls();
		}

		// Token: 0x060014DF RID: 5343 RVA: 0x000960B4 File Offset: 0x000950B4
		private void PopulateRoadItemSetListView()
		{
			this.created = false;
			this.RoadItemSetListView.Items.Clear();
			if (this.roadParams != null)
			{
				for (int index = 0; index < this.roadParams.ItemSets.Count; index++)
				{
					RoadItemSet itemSet = this.roadParams.ItemSets[index];
					ListViewItem listViewItem = new ListViewItem();
					listViewItem.Tag = index;
					RoadParamsBrowserForm.FillListViewItemByRoadItemSet(listViewItem, itemSet);
					this.RoadItemSetListView.Items.Add(listViewItem);
				}
			}
			this.UpdateRoadItemSetControls();
			this.created = true;
		}

		// Token: 0x060014E0 RID: 5344 RVA: 0x00096148 File Offset: 0x00095148
		private void PopulateRoadItemSetSelectionControls()
		{
			this.created = false;
			RoadItemSet roadItemSet = this.GetSelectedRoadItemSet();
			this.RoadItemSetPropertyGrid.SelectedObject = roadItemSet;
			this.roadItemSetItemListSource.RoadItemSet = roadItemSet;
			this.roadItemSetItemList.Refresh();
			this.created = true;
		}

		// Token: 0x060014E1 RID: 5345 RVA: 0x0009618D File Offset: 0x0009518D
		private void PopulateRoadItemSetControls()
		{
			this.PopulateRoadItemSetListView();
			this.PopulateRoadItemSetSelectionControls();
		}

		// Token: 0x060014E2 RID: 5346 RVA: 0x0009619B File Offset: 0x0009519B
		private void PopulateRoadParamsControls(string item)
		{
			if (!string.IsNullOrEmpty(item))
			{
				this.SetActiveFileName(item);
			}
			this.PopulateRoadSelectionControls();
			this.PopulateRoadStripeControls();
			this.PopulateRoadItemSetControls();
			this.UpdateMainControls();
		}

		// Token: 0x060014E3 RID: 5347 RVA: 0x000961C4 File Offset: 0x000951C4
		private void CheckRoadParams()
		{
			if (this.roadParams == null)
			{
				this.LoadRoadParams(this.GetActiveFileName());
			}
			if (this.roadParams == null)
			{
				this.CreateRoadParams();
			}
		}

		// Token: 0x060014E4 RID: 5348 RVA: 0x000961EC File Offset: 0x000951EC
		private void CreateRoadParams()
		{
			RoadParams _roadParams = new RoadParams();
			if (this.roadParams != null)
			{
				this.roadParams.Clone(_roadParams);
			}
			else
			{
				this.roadParams = _roadParams;
				this.roadParams.Changed += this.OnRoadParamsChanged;
			}
			this.modified = true;
		}

		// Token: 0x060014E5 RID: 5349 RVA: 0x0009623C File Offset: 0x0009523C
		private bool LoadRoadParams(string item)
		{
			RoadParams _roadParams = Serializer.Load<RoadParams>(EditorEnvironment.EditorFolder + item);
			if (_roadParams != null)
			{
				if (this.roadParams != null)
				{
					this.roadParams.Clone(_roadParams);
				}
				else
				{
					this.roadParams = _roadParams;
					this.roadParams.Changed += this.OnRoadParamsChanged;
				}
				return true;
			}
			return false;
		}

		// Token: 0x060014E6 RID: 5350 RVA: 0x00096294 File Offset: 0x00095294
		private void SaveRoadParams(string item)
		{
			if (this.roadParams != null)
			{
				Serializer.Save(EditorEnvironment.EditorFolder + item, this.roadParams, false);
			}
		}

		// Token: 0x060014E7 RID: 5351 RVA: 0x000962B6 File Offset: 0x000952B6
		private void OnRoadParamsItemList_ItemSelected(ItemList sender, string item)
		{
			this.UpdateMainControls();
		}

		// Token: 0x060014E8 RID: 5352 RVA: 0x000962BE File Offset: 0x000952BE
		private void OnRoadParamsItemList_ItemUnselected(ItemList sender, string item)
		{
			this.UpdateMainControls();
		}

		// Token: 0x060014E9 RID: 5353 RVA: 0x000962C6 File Offset: 0x000952C6
		private void OnRoadParamsItemList_ListCleared(ItemList sender, string item)
		{
			this.UpdateMainControls();
		}

		// Token: 0x060014EA RID: 5354 RVA: 0x000962CE File Offset: 0x000952CE
		private void OnRoadParamsItemList_ItemDoubleClicked(object sender, string item)
		{
			this.LoadButton_Click(sender, null);
		}

		// Token: 0x060014EB RID: 5355 RVA: 0x000962D8 File Offset: 0x000952D8
		private void OnRoadParamsItemList_ContextMenuCreated(ItemList sender, List<ToolStripItem> toolStripItems)
		{
			for (int index = 0; index < this.RoadContextMenu.Items.Count; index++)
			{
				toolStripItems.Add(this.RoadContextMenu.Items[index]);
			}
		}

		// Token: 0x060014EC RID: 5356 RVA: 0x00096317 File Offset: 0x00095317
		private void OnTileItemList_ItemSelected(ItemList sender, string item)
		{
			this.UpdateRoadStripeControls();
		}

		// Token: 0x060014ED RID: 5357 RVA: 0x0009631F File Offset: 0x0009531F
		private void OnTileItemList_ItemUnselected(ItemList sender, string item)
		{
			this.UpdateRoadStripeControls();
		}

		// Token: 0x060014EE RID: 5358 RVA: 0x00096327 File Offset: 0x00095327
		private void OnTileItemList_ListCleared(ItemList sender, string item)
		{
			this.UpdateRoadStripeControls();
		}

		// Token: 0x060014EF RID: 5359 RVA: 0x0009632F File Offset: 0x0009532F
		private void OnTileItemList_ItemDoubleClicked(ItemList sender, string item)
		{
			this.SetStripeTileButton_Click(sender, null);
		}

		// Token: 0x060014F0 RID: 5360 RVA: 0x0009633C File Offset: 0x0009533C
		private void OnTileItemList_ContextMenuCreated(ItemList sender, List<ToolStripItem> toolStripItems)
		{
			for (int index = 0; index < this.AvailableTilesContextMenu.Items.Count; index++)
			{
				toolStripItems.Add(this.AvailableTilesContextMenu.Items[index]);
			}
		}

		// Token: 0x060014F1 RID: 5361 RVA: 0x0009637B File Offset: 0x0009537B
		private void OnStaticObjectItemList_ItemSelected(ItemList sender, string item)
		{
			this.UpdateRoadItemSetControls();
		}

		// Token: 0x060014F2 RID: 5362 RVA: 0x00096383 File Offset: 0x00095383
		private void OnStaticObjectItemList_ItemUnselected(ItemList sender, string item)
		{
			this.UpdateRoadItemSetControls();
		}

		// Token: 0x060014F3 RID: 5363 RVA: 0x0009638B File Offset: 0x0009538B
		private void OnStaticObjectItemList_ListCleared(ItemList sender, string item)
		{
			this.UpdateRoadItemSetControls();
		}

		// Token: 0x060014F4 RID: 5364 RVA: 0x00096393 File Offset: 0x00095393
		private void OnStaticObjectItemList_ItemDoubleClicked(ItemList sender, string item)
		{
			this.UpdateRoadItemSetControls();
		}

		// Token: 0x060014F5 RID: 5365 RVA: 0x0009639C File Offset: 0x0009539C
		private void OnStaticObjectItemList_ContextMenuCreated(ItemList sender, List<ToolStripItem> toolStripItems)
		{
			for (int index = 0; index < this.AvailableStaticObjectsContextMenu.Items.Count; index++)
			{
				toolStripItems.Add(this.AvailableStaticObjectsContextMenu.Items[index]);
			}
		}

		// Token: 0x060014F6 RID: 5366 RVA: 0x000963DB File Offset: 0x000953DB
		private void OnRoadItemSetItemList_ItemSelected(ItemList sender, string item)
		{
			this.UpdateRoadItemSetControls();
		}

		// Token: 0x060014F7 RID: 5367 RVA: 0x000963E3 File Offset: 0x000953E3
		private void OnRoadItemSetItemList_ItemUnselected(ItemList sender, string item)
		{
			this.UpdateRoadItemSetControls();
		}

		// Token: 0x060014F8 RID: 5368 RVA: 0x000963EB File Offset: 0x000953EB
		private void OnRoadItemSetItemList_ListCleared(ItemList sender, string item)
		{
			this.UpdateRoadItemSetControls();
		}

		// Token: 0x060014F9 RID: 5369 RVA: 0x000963F3 File Offset: 0x000953F3
		private void OnRoadItemSetItemList_ItemDoubleClicked(ItemList sender, string item)
		{
			this.UpdateRoadItemSetControls();
		}

		// Token: 0x060014FA RID: 5370 RVA: 0x000963FC File Offset: 0x000953FC
		private void OnRoadItemSetItemList_ContextMenuCreated(ItemList sender, List<ToolStripItem> toolStripItems)
		{
			for (int index = 0; index < this.RoadItemSetItemsContextMenu.Items.Count; index++)
			{
				toolStripItems.Add(this.RoadItemSetItemsContextMenu.Items[index]);
			}
		}

		// Token: 0x060014FB RID: 5371 RVA: 0x0009643C File Offset: 0x0009543C
		private void OnRoadItemSetItemList_ItemDataMined(ItemList sender, string item, ItemData itemData, out ItemData newItemData)
		{
			newItemData = null;
			RoadItemSet itemSet = this.GetSelectedRoadItemSet();
			if (itemSet != null)
			{
				for (int itemIndex = 0; itemIndex < itemSet.Items.Items.Count; itemIndex++)
				{
					if (item == itemSet.Items.Items[itemIndex].Item)
					{
						string text = string.Format("{0} ({1})", itemData.Text, itemSet.Items.Items[itemIndex].Weight);
						string tooltip = string.Format("name: {0}\nweight: {1}\nsource: {2}", itemData.Text, itemSet.Items.Items[itemIndex].Weight, item);
						newItemData = new ItemData(text, tooltip, itemData.IconIndex, itemData.StandardIconKey);
						return;
					}
				}
			}
		}

		// Token: 0x060014FC RID: 5372 RVA: 0x0009650C File Offset: 0x0009550C
		private void RoadNameButton_Click(object sender, EventArgs e)
		{
			SaveFileDialog saveFileDialog = new SaveFileDialog();
			saveFileDialog.Filter = "XML Files|*.xml|All Files|*.*";
			saveFileDialog.RestoreDirectory = true;
			saveFileDialog.InitialDirectory = (EditorEnvironment.EditorFolder + LandscapeRoadItemSource.Folder).Replace('/', '\\');
			if (!Directory.Exists(saveFileDialog.InitialDirectory))
			{
				Directory.CreateDirectory(saveFileDialog.InitialDirectory);
			}
			if (saveFileDialog.ShowDialog(this) == DialogResult.OK)
			{
				this.SetActiveFileName(saveFileDialog.FileName);
			}
		}

		// Token: 0x060014FD RID: 5373 RVA: 0x0009657E File Offset: 0x0009557E
		private void CloseButton_Click(object sender, EventArgs e)
		{
			base.Close();
		}

		// Token: 0x060014FE RID: 5374 RVA: 0x00096588 File Offset: 0x00095588
		private void LoadButton_Click(object sender, EventArgs e)
		{
			bool proseed = true;
			if (this.modified)
			{
				DialogResult dialogResult = MessageBox.Show(Strings.ROAD_PARAMS_BROWSER_DATA_WILL_BE_LOST_WARNING, Strings.ROAD_PARAMS_BROWSER_DATA_WILL_BE_LOST_CAPTION, MessageBoxButtons.YesNo);
				if (dialogResult == DialogResult.No)
				{
					proseed = false;
				}
			}
			if (proseed)
			{
				string selectedItem = this.roadParamsItemList.SelectedItem;
				if (!string.IsNullOrEmpty(selectedItem) && this.LoadRoadParams(selectedItem))
				{
					this.PopulateRoadParamsControls(selectedItem);
				}
				this.modified = false;
			}
		}

		// Token: 0x060014FF RID: 5375 RVA: 0x000965E4 File Offset: 0x000955E4
		private void SaveButton_Click(object sender, EventArgs e)
		{
			this.SaveRoadParams(this.GetActiveFileName());
			this.roadParamsItemList.Refresh();
		}

		// Token: 0x06001500 RID: 5376 RVA: 0x00096600 File Offset: 0x00095600
		private void SaveToButton_Click(object sender, EventArgs e)
		{
			string selectedItem = this.roadParamsItemList.SelectedItem;
			if (!string.IsNullOrEmpty(selectedItem))
			{
				this.SetActiveFileName(selectedItem);
				this.SaveRoadParams(selectedItem);
			}
		}

		// Token: 0x06001501 RID: 5377 RVA: 0x0009662F File Offset: 0x0009562F
		private void DefaultsButton_Click(object sender, EventArgs e)
		{
			this.CreateRoadParams();
			this.PopulateRoadParamsControls(null);
		}

		// Token: 0x06001502 RID: 5378 RVA: 0x0009663E File Offset: 0x0009563E
		private void RoadContextMenuItemLoad_Click(object sender, EventArgs e)
		{
			this.SaveToButton_Click(sender, e);
		}

		// Token: 0x06001503 RID: 5379 RVA: 0x00096648 File Offset: 0x00095648
		private void RoadContextMenuItemSaveTo_Click(object sender, EventArgs e)
		{
			this.LoadButton_Click(sender, e);
		}

		// Token: 0x06001504 RID: 5380 RVA: 0x00096652 File Offset: 0x00095652
		private void RoadFilterButton_Click(object sender, EventArgs e)
		{
			this.roadParamsItemFiltes.ShowDialog(this.roadParamsItemList, this);
		}

		// Token: 0x06001505 RID: 5381 RVA: 0x00096667 File Offset: 0x00095667
		private void AvailableTilesFilterButton_Click(object sender, EventArgs e)
		{
			this.tileItemFiltes.ShowDialog(this.tileItemList, this);
		}

		// Token: 0x06001506 RID: 5382 RVA: 0x0009667C File Offset: 0x0009567C
		private void AvailableStaticObjectsFilterButton_Click(object sender, EventArgs e)
		{
			this.staticObjectItemFiltes.ShowDialog(this.staticObjectItemList, this);
		}

		// Token: 0x06001507 RID: 5383 RVA: 0x00096691 File Offset: 0x00095691
		private void RoadStripeListView_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
		{
			this.PopulateRoadStripeSelectionControls();
			this.UpdateRoadStripeControls();
		}

		// Token: 0x06001508 RID: 5384 RVA: 0x000966A0 File Offset: 0x000956A0
		private void RoadStripeListView_AfterLabelEdit(object sender, LabelEditEventArgs e)
		{
			this.created = false;
			RoadStripe stripe = this.GetSelectedRoadStripe();
			if (stripe != null && !string.IsNullOrEmpty(e.Label))
			{
				stripe.Name = e.Label;
				this.RoadStripePropertyGrid.SelectedObject = stripe;
				this.UpdateRoadStripeControls();
			}
			else
			{
				e.CancelEdit = true;
			}
			this.created = true;
		}

		// Token: 0x06001509 RID: 5385 RVA: 0x000966F9 File Offset: 0x000956F9
		private void RoadStripeButtonAdd_Click(object sender, EventArgs e)
		{
			if (this.roadParams != null)
			{
				this.roadParams.Stripes.Add(new RoadStripe());
				this.roadParams.InvokeChanged();
				this.PopulateRoadStripeControls();
			}
			this.UpdateRoadStripeControls();
		}

		// Token: 0x0600150A RID: 5386 RVA: 0x00096730 File Offset: 0x00095730
		private void RoadStripeButtonRemove_Click(object sender, EventArgs e)
		{
			if (this.roadParams != null)
			{
				int index = this.GetSelectedRoadStripeIndex();
				if (index >= 0 & index < this.roadParams.Stripes.Count)
				{
					this.roadParams.Stripes.RemoveAt(index);
					this.roadParams.InvokeChanged();
					this.PopulateRoadStripeControls();
				}
			}
			this.UpdateRoadStripeControls();
		}

		// Token: 0x0600150B RID: 5387 RVA: 0x00096794 File Offset: 0x00095794
		private void RoadStripeButtonDefaults_Click(object sender, EventArgs e)
		{
			RoadStripe stripe = this.GetSelectedRoadStripe();
			if (stripe != null)
			{
				stripe.SetDefaultValues();
			}
		}

		// Token: 0x0600150C RID: 5388 RVA: 0x000967B1 File Offset: 0x000957B1
		private void SetRoadTileButton_Click(object sender, EventArgs e)
		{
			if (this.roadParams != null)
			{
				this.roadParams.Tile = this.GetSelectedTile();
				this.PopulateRoadSelectionControls();
				this.UpdateActiveRoadStripeListViewItem();
			}
		}

		// Token: 0x0600150D RID: 5389 RVA: 0x000967D8 File Offset: 0x000957D8
		private void ClearRoadTileButton_Click(object sender, EventArgs e)
		{
			if (this.roadParams != null)
			{
				this.roadParams.Tile = -1;
				this.PopulateRoadSelectionControls();
				this.UpdateActiveRoadStripeListViewItem();
			}
		}

		// Token: 0x0600150E RID: 5390 RVA: 0x000967FC File Offset: 0x000957FC
		private void SetStripeTileButton_Click(object sender, EventArgs e)
		{
			RoadStripe stripe = this.GetSelectedRoadStripe();
			if (stripe != null)
			{
				stripe.Tile = this.GetSelectedTile();
				this.PopulateRoadStripeSelectionControls();
				this.UpdateActiveRoadStripeListViewItem();
			}
		}

		// Token: 0x0600150F RID: 5391 RVA: 0x0009682C File Offset: 0x0009582C
		private void ClearStripeTileButton_Click(object sender, EventArgs e)
		{
			RoadStripe stripe = this.GetSelectedRoadStripe();
			if (stripe != null)
			{
				stripe.Tile = -1;
				this.PopulateRoadStripeSelectionControls();
				this.UpdateActiveRoadStripeListViewItem();
			}
		}

		// Token: 0x06001510 RID: 5392 RVA: 0x00096856 File Offset: 0x00095856
		private void RoadStripeContextMenuItemAdd_Click(object sender, EventArgs e)
		{
			this.RoadStripeButtonAdd_Click(sender, e);
		}

		// Token: 0x06001511 RID: 5393 RVA: 0x00096860 File Offset: 0x00095860
		private void RoadStripeContextMenuItemRemove_Click(object sender, EventArgs e)
		{
			this.RoadStripeButtonRemove_Click(sender, e);
		}

		// Token: 0x06001512 RID: 5394 RVA: 0x0009686A File Offset: 0x0009586A
		private void RoadStripeContextMenuItemDefaults_Click(object sender, EventArgs e)
		{
			this.RoadStripeButtonDefaults_Click(sender, e);
		}

		// Token: 0x06001513 RID: 5395 RVA: 0x00096874 File Offset: 0x00095874
		private void AvailableTilesContextMenuItemSetRoadTile_Click(object sender, EventArgs e)
		{
			this.SetRoadTileButton_Click(sender, e);
		}

		// Token: 0x06001514 RID: 5396 RVA: 0x0009687E File Offset: 0x0009587E
		private void AvailableTilesContextMenuItemSetStripeTile_Click(object sender, EventArgs e)
		{
			this.SetStripeTileButton_Click(sender, e);
		}

		// Token: 0x06001515 RID: 5397 RVA: 0x00096888 File Offset: 0x00095888
		private void RoadItemSetListView_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
		{
			this.PopulateRoadItemSetSelectionControls();
			this.UpdateRoadItemSetControls();
		}

		// Token: 0x06001516 RID: 5398 RVA: 0x00096898 File Offset: 0x00095898
		private void RoadItemSetListView_AfterLabelEdit(object sender, LabelEditEventArgs e)
		{
			this.created = false;
			RoadItemSet itemSet = this.GetSelectedRoadItemSet();
			if (itemSet != null && !string.IsNullOrEmpty(e.Label))
			{
				itemSet.Name = e.Label;
				this.RoadItemSetPropertyGrid.SelectedObject = itemSet;
				this.UpdateRoadItemSetControls();
			}
			else
			{
				e.CancelEdit = true;
			}
			this.created = true;
		}

		// Token: 0x06001517 RID: 5399 RVA: 0x000968F1 File Offset: 0x000958F1
		private void RoadItemSetButtonAdd_Click(object sender, EventArgs e)
		{
			if (this.roadParams != null)
			{
				this.roadParams.ItemSets.Add(new RoadItemSet());
				this.roadParams.InvokeChanged();
				this.PopulateRoadItemSetControls();
			}
			this.UpdateActiveRoadItemSetListViewItem();
			this.UpdateRoadItemSetControls();
		}

		// Token: 0x06001518 RID: 5400 RVA: 0x00096930 File Offset: 0x00095930
		private void RoadItemSetButtonRemove_Click(object sender, EventArgs e)
		{
			if (this.roadParams != null)
			{
				int index = this.GetSelectedRoadItemSetIndex();
				if (index >= 0 & index < this.roadParams.ItemSets.Count)
				{
					this.roadParams.ItemSets.RemoveAt(index);
					this.roadParams.InvokeChanged();
					this.PopulateRoadItemSetControls();
				}
			}
			this.UpdateActiveRoadItemSetListViewItem();
			this.UpdateRoadItemSetControls();
		}

		// Token: 0x06001519 RID: 5401 RVA: 0x00096998 File Offset: 0x00095998
		private void RoadItemSetButtonDefaults_Click(object sender, EventArgs e)
		{
			RoadItemSet itemSet = this.GetSelectedRoadItemSet();
			if (itemSet != null)
			{
				itemSet.SetDefaultValues();
			}
		}

		// Token: 0x0600151A RID: 5402 RVA: 0x000969B8 File Offset: 0x000959B8
		private void StaticObjectAddButton_Click(object sender, EventArgs e)
		{
			RoadItemSet itemSet = this.GetSelectedRoadItemSet();
			if (itemSet != null)
			{
				List<string> items = this.GetSelectedStaticObjects();
				if (items != null && items.Count > 0)
				{
					int weight = this.GetSelectedStaticObjectWeight();
					foreach (string item in items)
					{
						if (!string.IsNullOrEmpty(item))
						{
							itemSet.Items.Add(item, weight);
						}
					}
					this.UpdateActiveRoadItemSetListViewItem();
					this.PopulateRoadItemSetSelectionControls();
				}
			}
		}

		// Token: 0x0600151B RID: 5403 RVA: 0x00096A48 File Offset: 0x00095A48
		private void StaticObjectRemoveButton_Click(object sender, EventArgs e)
		{
			RoadItemSet itemSet = this.GetSelectedRoadItemSet();
			if (itemSet != null)
			{
				List<string> items = this.GetSelectedItemSetItemObjects();
				if (items != null && items.Count > 0)
				{
					foreach (string item in items)
					{
						if (!string.IsNullOrEmpty(item))
						{
							itemSet.Items.Remove(item);
						}
					}
					this.UpdateActiveRoadItemSetListViewItem();
					this.PopulateRoadItemSetSelectionControls();
				}
			}
		}

		// Token: 0x0600151C RID: 5404 RVA: 0x00096ACC File Offset: 0x00095ACC
		private void StaticObjectSetButton_Click(object sender, EventArgs e)
		{
			RoadItemSet itemSet = this.GetSelectedRoadItemSet();
			if (itemSet != null)
			{
				List<string> items = this.GetSelectedItemSetItemObjects();
				if (items != null && items.Count > 0)
				{
					int weight = this.GetSelectedStaticObjectWeight();
					foreach (string item in items)
					{
						if (!string.IsNullOrEmpty(item))
						{
							itemSet.Items.SetWeight(item, weight);
						}
					}
					this.UpdateActiveRoadItemSetListViewItem();
					this.PopulateRoadItemSetSelectionControls();
				}
			}
		}

		// Token: 0x0600151D RID: 5405 RVA: 0x00096B5C File Offset: 0x00095B5C
		private void StaticObjectAutoCompleteButton_Click(object sender, EventArgs e)
		{
			RoadItemSet itemSet = this.GetSelectedRoadItemSet();
			if (itemSet != null)
			{
				this.created = false;
				ObjectSetSaver.Autocomplete(this.staticObjectItemListSource, itemSet.Items);
				this.roadItemSetItemList.Refresh();
				this.UpdateRoadItemSetControls();
				this.created = true;
			}
		}

		// Token: 0x0600151E RID: 5406 RVA: 0x00096BA3 File Offset: 0x00095BA3
		private void StaticObjectWeightComboBox_TextChanged(object sender, EventArgs e)
		{
			this.UpdateRoadItemSetControls();
		}

		// Token: 0x0600151F RID: 5407 RVA: 0x00096BAC File Offset: 0x00095BAC
		private void StaticObjectWeightComboBox_Leave(object sender, EventArgs e)
		{
			string newItem = this.StaticObjectWeightComboBox.Text;
			if (!this.StaticObjectWeightComboBox.Items.Contains(newItem))
			{
				this.StaticObjectWeightComboBox.Items.Insert(0, newItem);
				if (this.StaticObjectWeightComboBox.Items.Count > RoadParamsBrowserForm.maxStaticObjectWeightComboBoxItemCount)
				{
					this.StaticObjectWeightComboBox.Items.RemoveAt(this.StaticObjectWeightComboBox.Items.Count - 1);
				}
			}
		}

		// Token: 0x06001520 RID: 5408 RVA: 0x00096C23 File Offset: 0x00095C23
		private void RoadItemSetContextMenuItemAdd_Click(object sender, EventArgs e)
		{
			this.RoadItemSetButtonAdd_Click(sender, e);
		}

		// Token: 0x06001521 RID: 5409 RVA: 0x00096C2D File Offset: 0x00095C2D
		private void RoadItemSetContextMenuItemRemove_Click(object sender, EventArgs e)
		{
			this.RoadItemSetButtonRemove_Click(sender, e);
		}

		// Token: 0x06001522 RID: 5410 RVA: 0x00096C37 File Offset: 0x00095C37
		private void RoadItemSetContextMenuItemDefaults_Click(object sender, EventArgs e)
		{
			this.RoadItemSetButtonDefaults_Click(sender, e);
		}

		// Token: 0x06001523 RID: 5411 RVA: 0x00096C41 File Offset: 0x00095C41
		private void AvailableStaticObjectsContextMenuAdd_Click(object sender, EventArgs e)
		{
			this.StaticObjectAddButton_Click(sender, e);
		}

		// Token: 0x06001524 RID: 5412 RVA: 0x00096C4B File Offset: 0x00095C4B
		private void RoadItemSetItemsContextMenuRemove_Click(object sender, EventArgs e)
		{
			this.StaticObjectRemoveButton_Click(sender, e);
		}

		// Token: 0x06001525 RID: 5413 RVA: 0x00096C55 File Offset: 0x00095C55
		private void RoadItemSetItemsContextMenuSet_Click(object sender, EventArgs e)
		{
			this.StaticObjectSetButton_Click(sender, e);
		}

		// Token: 0x06001526 RID: 5414 RVA: 0x00096C5F File Offset: 0x00095C5F
		private void RoadItemSetItemsContextMenuAuto_Click(object sender, EventArgs e)
		{
			this.StaticObjectAutoCompleteButton_Click(sender, e);
		}

		// Token: 0x06001527 RID: 5415 RVA: 0x00096C6C File Offset: 0x00095C6C
		public RoadParamsBrowserForm(MainForm.Context context) : base(EditorEnvironment.EditorFormsFolder + "RoadParamsBrowserForm.xml", context)
		{
			this.InitializeComponent();
			if (base.ParamsSaver != null)
			{
				base.ParamsSaver.RegisterControl(this.RoadNameTextBox);
				base.ParamsSaver.RegisterControl(this.MainSplitContainer);
				base.ParamsSaver.RegisterControl(this.RoadStripeSplitContainer);
				base.ParamsSaver.RegisterControl(this.AvailableTilesSplitContainer);
				base.ParamsSaver.RegisterControl(this.RoadStripePropertySplitContainer);
				base.ParamsSaver.RegisterControl(this.RoadItemSetSplitContainer);
				base.ParamsSaver.RegisterControl(this.AvailableStaticObjectsSplitContainer);
				base.ParamsSaver.RegisterControl(this.RoadItemSetPropertySplitContainer);
				base.ParamsSaver.RegisterControl(this.RoadStripeListView);
				base.ParamsSaver.RegisterControl(this.RoadItemSetListView);
				base.ParamsSaver.RegisterControl(this.StaticObjectWeightComboBox, false);
				base.ParamsSaver.PostLoadParams += this.OnPostLoadParams;
				base.ParamsSaver.SaveParams += this.OnSaveParams;
			}
			if (base.Context != null && base.Context.ItemDataContainer != null)
			{
				this.roadParamsItemSource = new LandscapeRoadItemSource();
				this.roadParamsItemList = new ItemList(EditorEnvironment.EditorFormsFolder + "RoadParamsBrowser/RoadParamsItemList.xml", base.Context.ItemDataContainer, false);
				ItemList itemList = this.roadParamsItemList;
				itemList.ItemSelected = (ItemList.ItemEvent)Delegate.Combine(itemList.ItemSelected, new ItemList.ItemEvent(this.OnRoadParamsItemList_ItemSelected));
				ItemList itemList2 = this.roadParamsItemList;
				itemList2.ItemUnselected = (ItemList.ItemEvent)Delegate.Combine(itemList2.ItemUnselected, new ItemList.ItemEvent(this.OnRoadParamsItemList_ItemUnselected));
				ItemList itemList3 = this.roadParamsItemList;
				itemList3.ListCleared = (ItemList.ItemEvent)Delegate.Combine(itemList3.ListCleared, new ItemList.ItemEvent(this.OnRoadParamsItemList_ListCleared));
				ItemList itemList4 = this.roadParamsItemList;
				itemList4.ItemDoubleClicked = (ItemList.ItemEvent)Delegate.Combine(itemList4.ItemDoubleClicked, new ItemList.ItemEvent(this.OnRoadParamsItemList_ItemDoubleClicked));
				ItemList itemList5 = this.roadParamsItemList;
				itemList5.ContextMenuCreated = (ItemList.ContextMenuEvent)Delegate.Combine(itemList5.ContextMenuCreated, new ItemList.ContextMenuEvent(this.OnRoadParamsItemList_ContextMenuCreated));
				this.roadParamsItemFiltes = new FolderItemFilters(EditorEnvironment.EditorFolder + "Filters/RoadParamsBrowser/RoadParamsItemFilters.xml", EditorEnvironment.EditorFormsFolder);
				this.tileItemList = new ItemList(EditorEnvironment.EditorFormsFolder + "RoadParamsBrowser/TileItemList.xml", base.Context.ItemDataContainer, false);
				ItemList itemList6 = this.tileItemList;
				itemList6.ItemSelected = (ItemList.ItemEvent)Delegate.Combine(itemList6.ItemSelected, new ItemList.ItemEvent(this.OnTileItemList_ItemSelected));
				ItemList itemList7 = this.tileItemList;
				itemList7.ItemUnselected = (ItemList.ItemEvent)Delegate.Combine(itemList7.ItemUnselected, new ItemList.ItemEvent(this.OnTileItemList_ItemUnselected));
				ItemList itemList8 = this.tileItemList;
				itemList8.ListCleared = (ItemList.ItemEvent)Delegate.Combine(itemList8.ListCleared, new ItemList.ItemEvent(this.OnTileItemList_ListCleared));
				ItemList itemList9 = this.tileItemList;
				itemList9.ItemDoubleClicked = (ItemList.ItemEvent)Delegate.Combine(itemList9.ItemDoubleClicked, new ItemList.ItemEvent(this.OnTileItemList_ItemDoubleClicked));
				ItemList itemList10 = this.tileItemList;
				itemList10.ContextMenuCreated = (ItemList.ContextMenuEvent)Delegate.Combine(itemList10.ContextMenuCreated, new ItemList.ContextMenuEvent(this.OnTileItemList_ContextMenuCreated));
				this.tileItemFiltes = new FolderItemFilters(EditorEnvironment.EditorFolder + "Filters/RoadParamsBrowser/TileItemFilters.xml", EditorEnvironment.EditorFormsFolder);
				this.staticObjectItemListSource = new StaticObjectItemListSource();
				this.staticObjectItemList = new ItemList(EditorEnvironment.EditorFormsFolder + "RoadParamsBrowser/StaticObjectItemList.xml", base.Context.ItemDataContainer, true);
				ItemList itemList11 = this.staticObjectItemList;
				itemList11.ItemSelected = (ItemList.ItemEvent)Delegate.Combine(itemList11.ItemSelected, new ItemList.ItemEvent(this.OnStaticObjectItemList_ItemSelected));
				ItemList itemList12 = this.staticObjectItemList;
				itemList12.ItemUnselected = (ItemList.ItemEvent)Delegate.Combine(itemList12.ItemUnselected, new ItemList.ItemEvent(this.OnStaticObjectItemList_ItemUnselected));
				ItemList itemList13 = this.staticObjectItemList;
				itemList13.ListCleared = (ItemList.ItemEvent)Delegate.Combine(itemList13.ListCleared, new ItemList.ItemEvent(this.OnStaticObjectItemList_ListCleared));
				ItemList itemList14 = this.staticObjectItemList;
				itemList14.ItemDoubleClicked = (ItemList.ItemEvent)Delegate.Combine(itemList14.ItemDoubleClicked, new ItemList.ItemEvent(this.OnStaticObjectItemList_ItemDoubleClicked));
				ItemList itemList15 = this.staticObjectItemList;
				itemList15.ContextMenuCreated = (ItemList.ContextMenuEvent)Delegate.Combine(itemList15.ContextMenuCreated, new ItemList.ContextMenuEvent(this.OnStaticObjectItemList_ContextMenuCreated));
				this.staticObjectItemFiltes = new FolderItemFilters(EditorEnvironment.EditorFolder + "Filters/RoadParamsBrowser/StaticObjectItemFilters.xml", EditorEnvironment.EditorFormsFolder);
				this.roadItemSetItemListSource = new RoadItemSetItemListSource();
				this.roadItemSetItemList = new ItemList(EditorEnvironment.EditorFormsFolder + "RoadParamsBrowser/RoadItemSetItemList.xml", base.Context.ItemDataContainer, false);
				ItemList itemList16 = this.roadItemSetItemList;
				itemList16.ItemSelected = (ItemList.ItemEvent)Delegate.Combine(itemList16.ItemSelected, new ItemList.ItemEvent(this.OnRoadItemSetItemList_ItemSelected));
				ItemList itemList17 = this.roadItemSetItemList;
				itemList17.ItemUnselected = (ItemList.ItemEvent)Delegate.Combine(itemList17.ItemUnselected, new ItemList.ItemEvent(this.OnRoadItemSetItemList_ItemUnselected));
				ItemList itemList18 = this.roadItemSetItemList;
				itemList18.ListCleared = (ItemList.ItemEvent)Delegate.Combine(itemList18.ListCleared, new ItemList.ItemEvent(this.OnRoadItemSetItemList_ListCleared));
				ItemList itemList19 = this.roadItemSetItemList;
				itemList19.ItemDoubleClicked = (ItemList.ItemEvent)Delegate.Combine(itemList19.ItemDoubleClicked, new ItemList.ItemEvent(this.OnRoadItemSetItemList_ItemDoubleClicked));
				ItemList itemList20 = this.roadItemSetItemList;
				itemList20.ContextMenuCreated = (ItemList.ContextMenuEvent)Delegate.Combine(itemList20.ContextMenuCreated, new ItemList.ContextMenuEvent(this.OnRoadItemSetItemList_ContextMenuCreated));
				ItemList itemList21 = this.roadItemSetItemList;
				itemList21.ItemDataMined = (ItemList.ItemDataEvent)Delegate.Combine(itemList21.ItemDataMined, new ItemList.ItemDataEvent(this.OnRoadItemSetItemList_ItemDataMined));
			}
		}

		// Token: 0x06001528 RID: 5416 RVA: 0x000971FC File Offset: 0x000961FC
		public void Destroy()
		{
			this.roadParamsItemSource = null;
			this.roadParamsItemList = null;
			this.roadParamsItemFiltes = null;
			this.tileItemList = null;
			this.tileItemFiltes = null;
			this.staticObjectItemListSource = null;
			this.staticObjectItemList = null;
			this.staticObjectItemFiltes = null;
			this.roadItemSetItemListSource = null;
			this.roadItemSetItemList = null;
		}

		// Token: 0x1700041E RID: 1054
		// (get) Token: 0x06001529 RID: 5417 RVA: 0x0009724F File Offset: 0x0009624F
		// (set) Token: 0x0600152A RID: 5418 RVA: 0x00097258 File Offset: 0x00096258
		public RoadParams RoadParams
		{
			get
			{
				return this.roadParams;
			}
			set
			{
				if (this.roadParams != null)
				{
					this.roadParams.Changed -= this.OnRoadParamsChanged;
				}
				this.roadParams = value;
				if (this.roadParams == null)
				{
					this.CreateRoadParams();
				}
				if (this.roadParams != null)
				{
					this.roadParams.Changed += this.OnRoadParamsChanged;
				}
				this.PopulateRoadParamsControls(null);
			}
		}

		// Token: 0x0600152B RID: 5419 RVA: 0x000972C0 File Offset: 0x000962C0
		public void Bind(MapEditorMap _map)
		{
			this.Unbind();
			this.continentName = _map.Data.ContinentName;
			if (!string.IsNullOrEmpty(this.continentName) && this.tileItemList != null)
			{
				this.created = false;
				this.tileItemList.ItemSources.Add(new LandscapeTileItemSource(this.continentName));
				this.tileItemList.Refresh();
				this.created = true;
			}
		}

		// Token: 0x0600152C RID: 5420 RVA: 0x00097330 File Offset: 0x00096330
		public void Unbind()
		{
			if (this.created && !string.IsNullOrEmpty(this.continentName) && this.tileItemList != null)
			{
				this.created = false;
				this.tileItemList.ItemSources.Clear();
				this.tileItemList.Refresh();
				this.continentName = string.Empty;
				this.created = true;
			}
		}

		// Token: 0x04000E86 RID: 3718
		private static readonly int maxStaticObjectWeightComboBoxItemCount = 25;

		// Token: 0x04000E87 RID: 3719
		private string continentName = string.Empty;

		// Token: 0x04000E88 RID: 3720
		private bool created;

		// Token: 0x04000E89 RID: 3721
		private bool modified;

		// Token: 0x04000E8A RID: 3722
		private RoadParams roadParams;

		// Token: 0x04000E8B RID: 3723
		private LandscapeRoadItemSource roadParamsItemSource;

		// Token: 0x04000E8C RID: 3724
		private StaticObjectItemListSource staticObjectItemListSource;

		// Token: 0x04000E8D RID: 3725
		private RoadItemSetItemListSource roadItemSetItemListSource;

		// Token: 0x04000E8E RID: 3726
		private ItemList roadParamsItemList;

		// Token: 0x04000E8F RID: 3727
		private ItemList tileItemList;

		// Token: 0x04000E90 RID: 3728
		private ItemList staticObjectItemList;

		// Token: 0x04000E91 RID: 3729
		private ItemList roadItemSetItemList;

		// Token: 0x04000E92 RID: 3730
		private FolderItemFilters roadParamsItemFiltes;

		// Token: 0x04000E93 RID: 3731
		private FolderItemFilters tileItemFiltes;

		// Token: 0x04000E94 RID: 3732
		private FolderItemFilters staticObjectItemFiltes;
	}
}
