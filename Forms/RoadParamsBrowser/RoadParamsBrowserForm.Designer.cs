namespace MapEditor.Forms.RoadParamsBrowser
{
	// Token: 0x020001B4 RID: 436
	public partial class RoadParamsBrowserForm : global::MapEditor.Forms.Base.BaseForm
	{
		// Token: 0x0600152D RID: 5421 RVA: 0x0009738E File Offset: 0x0009638E
		protected override void Dispose(bool disposing)
		{
			if (disposing && this.components != null)
			{
				this.components.Dispose();
			}
			base.Dispose(disposing);
		}

		// Token: 0x0600152E RID: 5422 RVA: 0x000973B0 File Offset: 0x000963B0
		private void InitializeComponent()
		{
			this.components = new global::System.ComponentModel.Container();
			global::System.ComponentModel.ComponentResourceManager resources = new global::System.ComponentModel.ComponentResourceManager(typeof(global::MapEditor.Forms.RoadParamsBrowser.RoadParamsBrowserForm));
			this.MainSplitContainer = new global::System.Windows.Forms.SplitContainer();
			this.MainTabControl = new global::System.Windows.Forms.TabControl();
			this.tabPage1 = new global::System.Windows.Forms.TabPage();
			this.SaveToButton = new global::System.Windows.Forms.Button();
			this.LoadButton = new global::System.Windows.Forms.Button();
			this.RoadListView = new global::System.Windows.Forms.ListView();
			this.RoadFilterButton = new global::System.Windows.Forms.Button();
			this.RoadFilterLbel = new global::System.Windows.Forms.Label();
			this.RoadFilterComboBox = new global::System.Windows.Forms.ComboBox();
			this.tabPage2 = new global::System.Windows.Forms.TabPage();
			this.RoadStripeSplitContainer = new global::System.Windows.Forms.SplitContainer();
			this.RoadStripeButtonDefaults = new global::System.Windows.Forms.Button();
			this.RoadStripeListView = new global::System.Windows.Forms.ListView();
			this.RoadStripeListViewColumnName = new global::System.Windows.Forms.ColumnHeader();
			this.RoadStripeListViewColumnAnchor = new global::System.Windows.Forms.ColumnHeader();
			this.RoadStripeListViewColumnWidth = new global::System.Windows.Forms.ColumnHeader();
			this.RoadStripeListViewColumnTile = new global::System.Windows.Forms.ColumnHeader();
			this.RoadStripeContextMenu = new global::System.Windows.Forms.ContextMenuStrip(this.components);
			this.RoadStripeContextMenuItemAdd = new global::System.Windows.Forms.ToolStripMenuItem();
			this.RoadStripeContextMenuItemRemove = new global::System.Windows.Forms.ToolStripMenuItem();
			this.RoadStripeContextMenuSeparator00 = new global::System.Windows.Forms.ToolStripSeparator();
			this.RoadStripeContextMenuItemDefaults = new global::System.Windows.Forms.ToolStripMenuItem();
			this.RoadStripeButtonAdd = new global::System.Windows.Forms.Button();
			this.RoadStripeButtonRemove = new global::System.Windows.Forms.Button();
			this.AvailableTilesSplitContainer = new global::System.Windows.Forms.SplitContainer();
			this.SetRoadTileButton = new global::System.Windows.Forms.Button();
			this.SetStripeTileButton = new global::System.Windows.Forms.Button();
			this.AvailableTilesFilterComboBox = new global::System.Windows.Forms.ComboBox();
			this.AvailableTilesFilterLabel = new global::System.Windows.Forms.Label();
			this.AvailableTilesFilterButton = new global::System.Windows.Forms.Button();
			this.AvailableTilesLabel = new global::System.Windows.Forms.Label();
			this.AvailableTilesListView = new global::System.Windows.Forms.ListView();
			this.RoadStripePropertySplitContainer = new global::System.Windows.Forms.SplitContainer();
			this.RoadStripePropertyLabel = new global::System.Windows.Forms.Label();
			this.RoadStripePropertyGrid = new global::System.Windows.Forms.PropertyGrid();
			this.RoadImage = new global::System.Windows.Forms.PictureBox();
			this.RoadTileLabel = new global::System.Windows.Forms.Label();
			this.RoadStripeImage = new global::System.Windows.Forms.PictureBox();
			this.ClearRoadTileButton = new global::System.Windows.Forms.Button();
			this.ClearStripeTileButton = new global::System.Windows.Forms.Button();
			this.RoadStripeTileLabel = new global::System.Windows.Forms.Label();
			this.tabPage3 = new global::System.Windows.Forms.TabPage();
			this.RoadItemSetSplitContainer = new global::System.Windows.Forms.SplitContainer();
			this.RoadItemSetButtonDefaults = new global::System.Windows.Forms.Button();
			this.RoadItemSetListView = new global::System.Windows.Forms.ListView();
			this.RoadItemSetListViewColumnName = new global::System.Windows.Forms.ColumnHeader();
			this.RoadItemSetListViewColumnAnchor = new global::System.Windows.Forms.ColumnHeader();
			this.RoadItemSetListViewColumnType = new global::System.Windows.Forms.ColumnHeader();
			this.RoadItemSetListViewColumnDistance = new global::System.Windows.Forms.ColumnHeader();
			this.RoadItemSetListViewColumnCount = new global::System.Windows.Forms.ColumnHeader();
			this.RoadItemSetContextMenu = new global::System.Windows.Forms.ContextMenuStrip(this.components);
			this.RoadItemSetContextMenuItemAdd = new global::System.Windows.Forms.ToolStripMenuItem();
			this.RoadItemSetContextMenuItemRemove = new global::System.Windows.Forms.ToolStripMenuItem();
			this.RoadItemSetContextMenuSeparator00 = new global::System.Windows.Forms.ToolStripSeparator();
			this.RoadItemSetContextMenuItemDefaults = new global::System.Windows.Forms.ToolStripMenuItem();
			this.RoadItemSetButtonAdd = new global::System.Windows.Forms.Button();
			this.RoadItemSetButtonRemove = new global::System.Windows.Forms.Button();
			this.AvailableStaticObjectsSplitContainer = new global::System.Windows.Forms.SplitContainer();
			this.StaticObjectWeightLabel = new global::System.Windows.Forms.Label();
			this.StaticObjectAddButton = new global::System.Windows.Forms.Button();
			this.StaticObjectWeightComboBox = new global::System.Windows.Forms.ComboBox();
			this.AvailableStaticObjectsFilterComboBox = new global::System.Windows.Forms.ComboBox();
			this.AvailableStaticObjectsFilterLable = new global::System.Windows.Forms.Label();
			this.AvailableStaticObjectsFilterButton = new global::System.Windows.Forms.Button();
			this.AvailableStaticObjectsLabel = new global::System.Windows.Forms.Label();
			this.AvailableStaticObjectsListView = new global::System.Windows.Forms.ListView();
			this.RoadItemSetPropertySplitContainer = new global::System.Windows.Forms.SplitContainer();
			this.RoadItemSetPropertyLablel = new global::System.Windows.Forms.Label();
			this.RoadItemSetPropertyGrid = new global::System.Windows.Forms.PropertyGrid();
			this.StaticObjectSetButton = new global::System.Windows.Forms.Button();
			this.StaticObjectAutoCompleteButton = new global::System.Windows.Forms.Button();
			this.StaticObjectRemoveButton = new global::System.Windows.Forms.Button();
			this.RoadItemSetItemsLabel = new global::System.Windows.Forms.Label();
			this.RoadItemSetItemsListView = new global::System.Windows.Forms.ListView();
			this.RoadPropertyLabel = new global::System.Windows.Forms.Label();
			this.RoadPropertyGrid = new global::System.Windows.Forms.PropertyGrid();
			this.CloseButton = new global::System.Windows.Forms.Button();
			this.RoadNameLabel = new global::System.Windows.Forms.Label();
			this.RoadNameButton = new global::System.Windows.Forms.Button();
			this.RoadNameTextBox = new global::System.Windows.Forms.TextBox();
			this.SaveButton = new global::System.Windows.Forms.Button();
			this.DefaultsButton = new global::System.Windows.Forms.Button();
			this.TileToolTip = new global::System.Windows.Forms.ToolTip(this.components);
			this.AvailableTilesContextMenu = new global::System.Windows.Forms.ContextMenuStrip(this.components);
			this.AvailableTilesContextMenuSeparator00 = new global::System.Windows.Forms.ToolStripSeparator();
			this.AvailableTilesContextMenuItemSetStripeTile = new global::System.Windows.Forms.ToolStripMenuItem();
			this.AvailableTilesContextMenuItemSetRoadTile = new global::System.Windows.Forms.ToolStripMenuItem();
			this.RoadContextMenu = new global::System.Windows.Forms.ContextMenuStrip(this.components);
			this.RoadContextMenuItemSetarator00 = new global::System.Windows.Forms.ToolStripSeparator();
			this.RoadContextMenuItemLoad = new global::System.Windows.Forms.ToolStripMenuItem();
			this.RoadContextMenuItemSaveTo = new global::System.Windows.Forms.ToolStripMenuItem();
			this.AvailableStaticObjectsContextMenu = new global::System.Windows.Forms.ContextMenuStrip(this.components);
			this.AvailableStaticObjectsContextMenuSeparator00 = new global::System.Windows.Forms.ToolStripSeparator();
			this.AvailableStaticObjectsContextMenuAdd = new global::System.Windows.Forms.ToolStripMenuItem();
			this.RoadItemSetItemsContextMenu = new global::System.Windows.Forms.ContextMenuStrip(this.components);
			this.RoadItemSetItemsContextMenuSeparator00 = new global::System.Windows.Forms.ToolStripSeparator();
			this.RoadItemSetItemsContextMenuRemove = new global::System.Windows.Forms.ToolStripMenuItem();
			this.RoadItemSetItemsContextMenuSet = new global::System.Windows.Forms.ToolStripMenuItem();
			this.RoadItemSetItemsContextMenuAuto = new global::System.Windows.Forms.ToolStripMenuItem();
			this.MainSplitContainer.Panel1.SuspendLayout();
			this.MainSplitContainer.Panel2.SuspendLayout();
			this.MainSplitContainer.SuspendLayout();
			this.MainTabControl.SuspendLayout();
			this.tabPage1.SuspendLayout();
			this.tabPage2.SuspendLayout();
			this.RoadStripeSplitContainer.Panel1.SuspendLayout();
			this.RoadStripeSplitContainer.Panel2.SuspendLayout();
			this.RoadStripeSplitContainer.SuspendLayout();
			this.RoadStripeContextMenu.SuspendLayout();
			this.AvailableTilesSplitContainer.Panel1.SuspendLayout();
			this.AvailableTilesSplitContainer.Panel2.SuspendLayout();
			this.AvailableTilesSplitContainer.SuspendLayout();
			this.RoadStripePropertySplitContainer.Panel1.SuspendLayout();
			this.RoadStripePropertySplitContainer.Panel2.SuspendLayout();
			this.RoadStripePropertySplitContainer.SuspendLayout();
			((global::System.ComponentModel.ISupportInitialize)this.RoadImage).BeginInit();
			((global::System.ComponentModel.ISupportInitialize)this.RoadStripeImage).BeginInit();
			this.tabPage3.SuspendLayout();
			this.RoadItemSetSplitContainer.Panel1.SuspendLayout();
			this.RoadItemSetSplitContainer.Panel2.SuspendLayout();
			this.RoadItemSetSplitContainer.SuspendLayout();
			this.RoadItemSetContextMenu.SuspendLayout();
			this.AvailableStaticObjectsSplitContainer.Panel1.SuspendLayout();
			this.AvailableStaticObjectsSplitContainer.Panel2.SuspendLayout();
			this.AvailableStaticObjectsSplitContainer.SuspendLayout();
			this.RoadItemSetPropertySplitContainer.Panel1.SuspendLayout();
			this.RoadItemSetPropertySplitContainer.Panel2.SuspendLayout();
			this.RoadItemSetPropertySplitContainer.SuspendLayout();
			this.AvailableTilesContextMenu.SuspendLayout();
			this.RoadContextMenu.SuspendLayout();
			this.AvailableStaticObjectsContextMenu.SuspendLayout();
			this.RoadItemSetItemsContextMenu.SuspendLayout();
			base.SuspendLayout();
			resources.ApplyResources(this.MainSplitContainer, "MainSplitContainer");
			this.MainSplitContainer.Name = "MainSplitContainer";
			this.MainSplitContainer.Panel1.Controls.Add(this.MainTabControl);
			this.MainSplitContainer.Panel2.Controls.Add(this.RoadPropertyLabel);
			this.MainSplitContainer.Panel2.Controls.Add(this.RoadPropertyGrid);
			resources.ApplyResources(this.MainTabControl, "MainTabControl");
			this.MainTabControl.Controls.Add(this.tabPage1);
			this.MainTabControl.Controls.Add(this.tabPage2);
			this.MainTabControl.Controls.Add(this.tabPage3);
			this.MainTabControl.Name = "MainTabControl";
			this.MainTabControl.SelectedIndex = 0;
			this.tabPage1.Controls.Add(this.SaveToButton);
			this.tabPage1.Controls.Add(this.LoadButton);
			this.tabPage1.Controls.Add(this.RoadListView);
			this.tabPage1.Controls.Add(this.RoadFilterButton);
			this.tabPage1.Controls.Add(this.RoadFilterLbel);
			this.tabPage1.Controls.Add(this.RoadFilterComboBox);
			resources.ApplyResources(this.tabPage1, "tabPage1");
			this.tabPage1.Name = "tabPage1";
			this.tabPage1.UseVisualStyleBackColor = true;
			resources.ApplyResources(this.SaveToButton, "SaveToButton");
			this.SaveToButton.Name = "SaveToButton";
			this.SaveToButton.UseVisualStyleBackColor = true;
			this.SaveToButton.Click += new global::System.EventHandler(this.SaveToButton_Click);
			resources.ApplyResources(this.LoadButton, "LoadButton");
			this.LoadButton.Name = "LoadButton";
			this.LoadButton.UseVisualStyleBackColor = true;
			this.LoadButton.Click += new global::System.EventHandler(this.LoadButton_Click);
			resources.ApplyResources(this.RoadListView, "RoadListView");
			this.RoadListView.HideSelection = false;
			this.RoadListView.MultiSelect = false;
			this.RoadListView.Name = "RoadListView";
			this.RoadListView.ShowItemToolTips = true;
			this.RoadListView.Sorting = global::System.Windows.Forms.SortOrder.Ascending;
			this.RoadListView.UseCompatibleStateImageBehavior = false;
			this.RoadListView.View = global::System.Windows.Forms.View.List;
			resources.ApplyResources(this.RoadFilterButton, "RoadFilterButton");
			this.RoadFilterButton.Name = "RoadFilterButton";
			this.RoadFilterButton.UseVisualStyleBackColor = true;
			this.RoadFilterButton.Click += new global::System.EventHandler(this.RoadFilterButton_Click);
			resources.ApplyResources(this.RoadFilterLbel, "RoadFilterLbel");
			this.RoadFilterLbel.Name = "RoadFilterLbel";
			resources.ApplyResources(this.RoadFilterComboBox, "RoadFilterComboBox");
			this.RoadFilterComboBox.FormattingEnabled = true;
			this.RoadFilterComboBox.Name = "RoadFilterComboBox";
			this.tabPage2.Controls.Add(this.RoadStripeSplitContainer);
			resources.ApplyResources(this.tabPage2, "tabPage2");
			this.tabPage2.Name = "tabPage2";
			this.tabPage2.UseVisualStyleBackColor = true;
			resources.ApplyResources(this.RoadStripeSplitContainer, "RoadStripeSplitContainer");
			this.RoadStripeSplitContainer.Name = "RoadStripeSplitContainer";
			this.RoadStripeSplitContainer.Panel1.Controls.Add(this.RoadStripeButtonDefaults);
			this.RoadStripeSplitContainer.Panel1.Controls.Add(this.RoadStripeListView);
			this.RoadStripeSplitContainer.Panel1.Controls.Add(this.RoadStripeButtonAdd);
			this.RoadStripeSplitContainer.Panel1.Controls.Add(this.RoadStripeButtonRemove);
			this.RoadStripeSplitContainer.Panel2.Controls.Add(this.AvailableTilesSplitContainer);
			resources.ApplyResources(this.RoadStripeButtonDefaults, "RoadStripeButtonDefaults");
			this.RoadStripeButtonDefaults.Name = "RoadStripeButtonDefaults";
			this.RoadStripeButtonDefaults.UseVisualStyleBackColor = true;
			this.RoadStripeButtonDefaults.Click += new global::System.EventHandler(this.RoadStripeButtonDefaults_Click);
			resources.ApplyResources(this.RoadStripeListView, "RoadStripeListView");
			this.RoadStripeListView.Columns.AddRange(new global::System.Windows.Forms.ColumnHeader[]
			{
				this.RoadStripeListViewColumnName,
				this.RoadStripeListViewColumnAnchor,
				this.RoadStripeListViewColumnWidth,
				this.RoadStripeListViewColumnTile
			});
			this.RoadStripeListView.ContextMenuStrip = this.RoadStripeContextMenu;
			this.RoadStripeListView.FullRowSelect = true;
			this.RoadStripeListView.HideSelection = false;
			this.RoadStripeListView.LabelEdit = true;
			this.RoadStripeListView.MultiSelect = false;
			this.RoadStripeListView.Name = "RoadStripeListView";
			this.RoadStripeListView.ShowItemToolTips = true;
			this.RoadStripeListView.Sorting = global::System.Windows.Forms.SortOrder.Ascending;
			this.RoadStripeListView.UseCompatibleStateImageBehavior = false;
			this.RoadStripeListView.View = global::System.Windows.Forms.View.Details;
			this.RoadStripeListView.AfterLabelEdit += new global::System.Windows.Forms.LabelEditEventHandler(this.RoadStripeListView_AfterLabelEdit);
			this.RoadStripeListView.ItemSelectionChanged += new global::System.Windows.Forms.ListViewItemSelectionChangedEventHandler(this.RoadStripeListView_ItemSelectionChanged);
			resources.ApplyResources(this.RoadStripeListViewColumnName, "RoadStripeListViewColumnName");
			resources.ApplyResources(this.RoadStripeListViewColumnAnchor, "RoadStripeListViewColumnAnchor");
			resources.ApplyResources(this.RoadStripeListViewColumnWidth, "RoadStripeListViewColumnWidth");
			resources.ApplyResources(this.RoadStripeListViewColumnTile, "RoadStripeListViewColumnTile");
			this.RoadStripeContextMenu.Items.AddRange(new global::System.Windows.Forms.ToolStripItem[]
			{
				this.RoadStripeContextMenuItemAdd,
				this.RoadStripeContextMenuItemRemove,
				this.RoadStripeContextMenuSeparator00,
				this.RoadStripeContextMenuItemDefaults
			});
			this.RoadStripeContextMenu.Name = "RoadStripeContextMenu";
			resources.ApplyResources(this.RoadStripeContextMenu, "RoadStripeContextMenu");
			this.RoadStripeContextMenuItemAdd.Name = "RoadStripeContextMenuItemAdd";
			resources.ApplyResources(this.RoadStripeContextMenuItemAdd, "RoadStripeContextMenuItemAdd");
			this.RoadStripeContextMenuItemAdd.Click += new global::System.EventHandler(this.RoadStripeContextMenuItemAdd_Click);
			this.RoadStripeContextMenuItemRemove.Name = "RoadStripeContextMenuItemRemove";
			resources.ApplyResources(this.RoadStripeContextMenuItemRemove, "RoadStripeContextMenuItemRemove");
			this.RoadStripeContextMenuItemRemove.Click += new global::System.EventHandler(this.RoadStripeContextMenuItemRemove_Click);
			this.RoadStripeContextMenuSeparator00.Name = "RoadStripeContextMenuSeparator00";
			resources.ApplyResources(this.RoadStripeContextMenuSeparator00, "RoadStripeContextMenuSeparator00");
			this.RoadStripeContextMenuItemDefaults.Name = "RoadStripeContextMenuItemDefaults";
			resources.ApplyResources(this.RoadStripeContextMenuItemDefaults, "RoadStripeContextMenuItemDefaults");
			this.RoadStripeContextMenuItemDefaults.Click += new global::System.EventHandler(this.RoadStripeContextMenuItemDefaults_Click);
			resources.ApplyResources(this.RoadStripeButtonAdd, "RoadStripeButtonAdd");
			this.RoadStripeButtonAdd.Name = "RoadStripeButtonAdd";
			this.RoadStripeButtonAdd.UseVisualStyleBackColor = true;
			this.RoadStripeButtonAdd.Click += new global::System.EventHandler(this.RoadStripeButtonAdd_Click);
			resources.ApplyResources(this.RoadStripeButtonRemove, "RoadStripeButtonRemove");
			this.RoadStripeButtonRemove.Name = "RoadStripeButtonRemove";
			this.RoadStripeButtonRemove.UseVisualStyleBackColor = true;
			this.RoadStripeButtonRemove.Click += new global::System.EventHandler(this.RoadStripeButtonRemove_Click);
			resources.ApplyResources(this.AvailableTilesSplitContainer, "AvailableTilesSplitContainer");
			this.AvailableTilesSplitContainer.Name = "AvailableTilesSplitContainer";
			this.AvailableTilesSplitContainer.Panel1.Controls.Add(this.SetRoadTileButton);
			this.AvailableTilesSplitContainer.Panel1.Controls.Add(this.SetStripeTileButton);
			this.AvailableTilesSplitContainer.Panel1.Controls.Add(this.AvailableTilesFilterComboBox);
			this.AvailableTilesSplitContainer.Panel1.Controls.Add(this.AvailableTilesFilterLabel);
			this.AvailableTilesSplitContainer.Panel1.Controls.Add(this.AvailableTilesFilterButton);
			this.AvailableTilesSplitContainer.Panel1.Controls.Add(this.AvailableTilesLabel);
			this.AvailableTilesSplitContainer.Panel1.Controls.Add(this.AvailableTilesListView);
			this.AvailableTilesSplitContainer.Panel2.Controls.Add(this.RoadStripePropertySplitContainer);
			resources.ApplyResources(this.SetRoadTileButton, "SetRoadTileButton");
			this.SetRoadTileButton.Name = "SetRoadTileButton";
			this.SetRoadTileButton.UseVisualStyleBackColor = true;
			this.SetRoadTileButton.Click += new global::System.EventHandler(this.SetRoadTileButton_Click);
			resources.ApplyResources(this.SetStripeTileButton, "SetStripeTileButton");
			this.SetStripeTileButton.Name = "SetStripeTileButton";
			this.SetStripeTileButton.UseVisualStyleBackColor = true;
			this.SetStripeTileButton.Click += new global::System.EventHandler(this.SetStripeTileButton_Click);
			resources.ApplyResources(this.AvailableTilesFilterComboBox, "AvailableTilesFilterComboBox");
			this.AvailableTilesFilterComboBox.FormattingEnabled = true;
			this.AvailableTilesFilterComboBox.Name = "AvailableTilesFilterComboBox";
			resources.ApplyResources(this.AvailableTilesFilterLabel, "AvailableTilesFilterLabel");
			this.AvailableTilesFilterLabel.Name = "AvailableTilesFilterLabel";
			resources.ApplyResources(this.AvailableTilesFilterButton, "AvailableTilesFilterButton");
			this.AvailableTilesFilterButton.Name = "AvailableTilesFilterButton";
			this.AvailableTilesFilterButton.UseVisualStyleBackColor = true;
			this.AvailableTilesFilterButton.Click += new global::System.EventHandler(this.AvailableTilesFilterButton_Click);
			resources.ApplyResources(this.AvailableTilesLabel, "AvailableTilesLabel");
			this.AvailableTilesLabel.Name = "AvailableTilesLabel";
			resources.ApplyResources(this.AvailableTilesListView, "AvailableTilesListView");
			this.AvailableTilesListView.HideSelection = false;
			this.AvailableTilesListView.MultiSelect = false;
			this.AvailableTilesListView.Name = "AvailableTilesListView";
			this.AvailableTilesListView.ShowItemToolTips = true;
			this.AvailableTilesListView.Sorting = global::System.Windows.Forms.SortOrder.Ascending;
			this.AvailableTilesListView.UseCompatibleStateImageBehavior = false;
			resources.ApplyResources(this.RoadStripePropertySplitContainer, "RoadStripePropertySplitContainer");
			this.RoadStripePropertySplitContainer.Name = "RoadStripePropertySplitContainer";
			this.RoadStripePropertySplitContainer.Panel1.Controls.Add(this.RoadStripePropertyLabel);
			this.RoadStripePropertySplitContainer.Panel1.Controls.Add(this.RoadStripePropertyGrid);
			this.RoadStripePropertySplitContainer.Panel2.Controls.Add(this.RoadImage);
			this.RoadStripePropertySplitContainer.Panel2.Controls.Add(this.RoadTileLabel);
			this.RoadStripePropertySplitContainer.Panel2.Controls.Add(this.RoadStripeImage);
			this.RoadStripePropertySplitContainer.Panel2.Controls.Add(this.ClearRoadTileButton);
			this.RoadStripePropertySplitContainer.Panel2.Controls.Add(this.ClearStripeTileButton);
			this.RoadStripePropertySplitContainer.Panel2.Controls.Add(this.RoadStripeTileLabel);
			resources.ApplyResources(this.RoadStripePropertyLabel, "RoadStripePropertyLabel");
			this.RoadStripePropertyLabel.Name = "RoadStripePropertyLabel";
			resources.ApplyResources(this.RoadStripePropertyGrid, "RoadStripePropertyGrid");
			this.RoadStripePropertyGrid.Name = "RoadStripePropertyGrid";
			resources.ApplyResources(this.RoadImage, "RoadImage");
			this.RoadImage.Name = "RoadImage";
			this.RoadImage.TabStop = false;
			resources.ApplyResources(this.RoadTileLabel, "RoadTileLabel");
			this.RoadTileLabel.Name = "RoadTileLabel";
			resources.ApplyResources(this.RoadStripeImage, "RoadStripeImage");
			this.RoadStripeImage.Name = "RoadStripeImage";
			this.RoadStripeImage.TabStop = false;
			resources.ApplyResources(this.ClearRoadTileButton, "ClearRoadTileButton");
			this.ClearRoadTileButton.Name = "ClearRoadTileButton";
			this.ClearRoadTileButton.UseVisualStyleBackColor = true;
			this.ClearRoadTileButton.Click += new global::System.EventHandler(this.ClearRoadTileButton_Click);
			resources.ApplyResources(this.ClearStripeTileButton, "ClearStripeTileButton");
			this.ClearStripeTileButton.Name = "ClearStripeTileButton";
			this.ClearStripeTileButton.UseVisualStyleBackColor = true;
			this.ClearStripeTileButton.Click += new global::System.EventHandler(this.ClearStripeTileButton_Click);
			resources.ApplyResources(this.RoadStripeTileLabel, "RoadStripeTileLabel");
			this.RoadStripeTileLabel.Name = "RoadStripeTileLabel";
			this.tabPage3.Controls.Add(this.RoadItemSetSplitContainer);
			resources.ApplyResources(this.tabPage3, "tabPage3");
			this.tabPage3.Name = "tabPage3";
			this.tabPage3.UseVisualStyleBackColor = true;
			resources.ApplyResources(this.RoadItemSetSplitContainer, "RoadItemSetSplitContainer");
			this.RoadItemSetSplitContainer.Name = "RoadItemSetSplitContainer";
			this.RoadItemSetSplitContainer.Panel1.Controls.Add(this.RoadItemSetButtonDefaults);
			this.RoadItemSetSplitContainer.Panel1.Controls.Add(this.RoadItemSetListView);
			this.RoadItemSetSplitContainer.Panel1.Controls.Add(this.RoadItemSetButtonAdd);
			this.RoadItemSetSplitContainer.Panel1.Controls.Add(this.RoadItemSetButtonRemove);
			this.RoadItemSetSplitContainer.Panel2.Controls.Add(this.AvailableStaticObjectsSplitContainer);
			resources.ApplyResources(this.RoadItemSetButtonDefaults, "RoadItemSetButtonDefaults");
			this.RoadItemSetButtonDefaults.Name = "RoadItemSetButtonDefaults";
			this.RoadItemSetButtonDefaults.UseVisualStyleBackColor = true;
			this.RoadItemSetButtonDefaults.Click += new global::System.EventHandler(this.RoadItemSetButtonDefaults_Click);
			resources.ApplyResources(this.RoadItemSetListView, "RoadItemSetListView");
			this.RoadItemSetListView.Columns.AddRange(new global::System.Windows.Forms.ColumnHeader[]
			{
				this.RoadItemSetListViewColumnName,
				this.RoadItemSetListViewColumnAnchor,
				this.RoadItemSetListViewColumnType,
				this.RoadItemSetListViewColumnDistance,
				this.RoadItemSetListViewColumnCount
			});
			this.RoadItemSetListView.ContextMenuStrip = this.RoadItemSetContextMenu;
			this.RoadItemSetListView.FullRowSelect = true;
			this.RoadItemSetListView.HideSelection = false;
			this.RoadItemSetListView.LabelEdit = true;
			this.RoadItemSetListView.MultiSelect = false;
			this.RoadItemSetListView.Name = "RoadItemSetListView";
			this.RoadItemSetListView.ShowItemToolTips = true;
			this.RoadItemSetListView.Sorting = global::System.Windows.Forms.SortOrder.Ascending;
			this.RoadItemSetListView.UseCompatibleStateImageBehavior = false;
			this.RoadItemSetListView.View = global::System.Windows.Forms.View.Details;
			this.RoadItemSetListView.AfterLabelEdit += new global::System.Windows.Forms.LabelEditEventHandler(this.RoadItemSetListView_AfterLabelEdit);
			this.RoadItemSetListView.ItemSelectionChanged += new global::System.Windows.Forms.ListViewItemSelectionChangedEventHandler(this.RoadItemSetListView_ItemSelectionChanged);
			resources.ApplyResources(this.RoadItemSetListViewColumnName, "RoadItemSetListViewColumnName");
			resources.ApplyResources(this.RoadItemSetListViewColumnAnchor, "RoadItemSetListViewColumnAnchor");
			resources.ApplyResources(this.RoadItemSetListViewColumnType, "RoadItemSetListViewColumnType");
			resources.ApplyResources(this.RoadItemSetListViewColumnDistance, "RoadItemSetListViewColumnDistance");
			resources.ApplyResources(this.RoadItemSetListViewColumnCount, "RoadItemSetListViewColumnCount");
			this.RoadItemSetContextMenu.Items.AddRange(new global::System.Windows.Forms.ToolStripItem[]
			{
				this.RoadItemSetContextMenuItemAdd,
				this.RoadItemSetContextMenuItemRemove,
				this.RoadItemSetContextMenuSeparator00,
				this.RoadItemSetContextMenuItemDefaults
			});
			this.RoadItemSetContextMenu.Name = "RoadItemSetContextMenu";
			resources.ApplyResources(this.RoadItemSetContextMenu, "RoadItemSetContextMenu");
			this.RoadItemSetContextMenuItemAdd.Name = "RoadItemSetContextMenuItemAdd";
			resources.ApplyResources(this.RoadItemSetContextMenuItemAdd, "RoadItemSetContextMenuItemAdd");
			this.RoadItemSetContextMenuItemAdd.Click += new global::System.EventHandler(this.RoadItemSetContextMenuItemAdd_Click);
			this.RoadItemSetContextMenuItemRemove.Name = "RoadItemSetContextMenuItemRemove";
			resources.ApplyResources(this.RoadItemSetContextMenuItemRemove, "RoadItemSetContextMenuItemRemove");
			this.RoadItemSetContextMenuItemRemove.Click += new global::System.EventHandler(this.RoadItemSetContextMenuItemRemove_Click);
			this.RoadItemSetContextMenuSeparator00.Name = "RoadItemSetContextMenuSeparator00";
			resources.ApplyResources(this.RoadItemSetContextMenuSeparator00, "RoadItemSetContextMenuSeparator00");
			this.RoadItemSetContextMenuItemDefaults.Name = "RoadItemSetContextMenuItemDefaults";
			resources.ApplyResources(this.RoadItemSetContextMenuItemDefaults, "RoadItemSetContextMenuItemDefaults");
			this.RoadItemSetContextMenuItemDefaults.Click += new global::System.EventHandler(this.RoadItemSetContextMenuItemDefaults_Click);
			resources.ApplyResources(this.RoadItemSetButtonAdd, "RoadItemSetButtonAdd");
			this.RoadItemSetButtonAdd.Name = "RoadItemSetButtonAdd";
			this.RoadItemSetButtonAdd.UseVisualStyleBackColor = true;
			this.RoadItemSetButtonAdd.Click += new global::System.EventHandler(this.RoadItemSetButtonAdd_Click);
			resources.ApplyResources(this.RoadItemSetButtonRemove, "RoadItemSetButtonRemove");
			this.RoadItemSetButtonRemove.Name = "RoadItemSetButtonRemove";
			this.RoadItemSetButtonRemove.UseVisualStyleBackColor = true;
			this.RoadItemSetButtonRemove.Click += new global::System.EventHandler(this.RoadItemSetButtonRemove_Click);
			resources.ApplyResources(this.AvailableStaticObjectsSplitContainer, "AvailableStaticObjectsSplitContainer");
			this.AvailableStaticObjectsSplitContainer.Name = "AvailableStaticObjectsSplitContainer";
			this.AvailableStaticObjectsSplitContainer.Panel1.Controls.Add(this.StaticObjectWeightLabel);
			this.AvailableStaticObjectsSplitContainer.Panel1.Controls.Add(this.StaticObjectAddButton);
			this.AvailableStaticObjectsSplitContainer.Panel1.Controls.Add(this.StaticObjectWeightComboBox);
			this.AvailableStaticObjectsSplitContainer.Panel1.Controls.Add(this.AvailableStaticObjectsFilterComboBox);
			this.AvailableStaticObjectsSplitContainer.Panel1.Controls.Add(this.AvailableStaticObjectsFilterLable);
			this.AvailableStaticObjectsSplitContainer.Panel1.Controls.Add(this.AvailableStaticObjectsFilterButton);
			this.AvailableStaticObjectsSplitContainer.Panel1.Controls.Add(this.AvailableStaticObjectsLabel);
			this.AvailableStaticObjectsSplitContainer.Panel1.Controls.Add(this.AvailableStaticObjectsListView);
			this.AvailableStaticObjectsSplitContainer.Panel2.Controls.Add(this.RoadItemSetPropertySplitContainer);
			resources.ApplyResources(this.StaticObjectWeightLabel, "StaticObjectWeightLabel");
			this.StaticObjectWeightLabel.Name = "StaticObjectWeightLabel";
			resources.ApplyResources(this.StaticObjectAddButton, "StaticObjectAddButton");
			this.StaticObjectAddButton.Name = "StaticObjectAddButton";
			this.StaticObjectAddButton.UseVisualStyleBackColor = true;
			this.StaticObjectAddButton.Click += new global::System.EventHandler(this.StaticObjectAddButton_Click);
			resources.ApplyResources(this.StaticObjectWeightComboBox, "StaticObjectWeightComboBox");
			this.StaticObjectWeightComboBox.FormattingEnabled = true;
			this.StaticObjectWeightComboBox.Name = "StaticObjectWeightComboBox";
			this.StaticObjectWeightComboBox.Leave += new global::System.EventHandler(this.StaticObjectWeightComboBox_Leave);
			this.StaticObjectWeightComboBox.TextChanged += new global::System.EventHandler(this.StaticObjectWeightComboBox_TextChanged);
			resources.ApplyResources(this.AvailableStaticObjectsFilterComboBox, "AvailableStaticObjectsFilterComboBox");
			this.AvailableStaticObjectsFilterComboBox.FormattingEnabled = true;
			this.AvailableStaticObjectsFilterComboBox.Name = "AvailableStaticObjectsFilterComboBox";
			resources.ApplyResources(this.AvailableStaticObjectsFilterLable, "AvailableStaticObjectsFilterLable");
			this.AvailableStaticObjectsFilterLable.Name = "AvailableStaticObjectsFilterLable";
			resources.ApplyResources(this.AvailableStaticObjectsFilterButton, "AvailableStaticObjectsFilterButton");
			this.AvailableStaticObjectsFilterButton.Name = "AvailableStaticObjectsFilterButton";
			this.AvailableStaticObjectsFilterButton.UseVisualStyleBackColor = true;
			this.AvailableStaticObjectsFilterButton.Click += new global::System.EventHandler(this.AvailableStaticObjectsFilterButton_Click);
			resources.ApplyResources(this.AvailableStaticObjectsLabel, "AvailableStaticObjectsLabel");
			this.AvailableStaticObjectsLabel.Name = "AvailableStaticObjectsLabel";
			resources.ApplyResources(this.AvailableStaticObjectsListView, "AvailableStaticObjectsListView");
			this.AvailableStaticObjectsListView.HideSelection = false;
			this.AvailableStaticObjectsListView.Name = "AvailableStaticObjectsListView";
			this.AvailableStaticObjectsListView.ShowItemToolTips = true;
			this.AvailableStaticObjectsListView.Sorting = global::System.Windows.Forms.SortOrder.Ascending;
			this.AvailableStaticObjectsListView.UseCompatibleStateImageBehavior = false;
			resources.ApplyResources(this.RoadItemSetPropertySplitContainer, "RoadItemSetPropertySplitContainer");
			this.RoadItemSetPropertySplitContainer.Name = "RoadItemSetPropertySplitContainer";
			this.RoadItemSetPropertySplitContainer.Panel1.Controls.Add(this.RoadItemSetPropertyLablel);
			this.RoadItemSetPropertySplitContainer.Panel1.Controls.Add(this.RoadItemSetPropertyGrid);
			this.RoadItemSetPropertySplitContainer.Panel2.Controls.Add(this.StaticObjectSetButton);
			this.RoadItemSetPropertySplitContainer.Panel2.Controls.Add(this.StaticObjectAutoCompleteButton);
			this.RoadItemSetPropertySplitContainer.Panel2.Controls.Add(this.StaticObjectRemoveButton);
			this.RoadItemSetPropertySplitContainer.Panel2.Controls.Add(this.RoadItemSetItemsLabel);
			this.RoadItemSetPropertySplitContainer.Panel2.Controls.Add(this.RoadItemSetItemsListView);
			resources.ApplyResources(this.RoadItemSetPropertyLablel, "RoadItemSetPropertyLablel");
			this.RoadItemSetPropertyLablel.Name = "RoadItemSetPropertyLablel";
			resources.ApplyResources(this.RoadItemSetPropertyGrid, "RoadItemSetPropertyGrid");
			this.RoadItemSetPropertyGrid.Name = "RoadItemSetPropertyGrid";
			resources.ApplyResources(this.StaticObjectSetButton, "StaticObjectSetButton");
			this.StaticObjectSetButton.Name = "StaticObjectSetButton";
			this.StaticObjectSetButton.UseVisualStyleBackColor = true;
			this.StaticObjectSetButton.Click += new global::System.EventHandler(this.StaticObjectSetButton_Click);
			resources.ApplyResources(this.StaticObjectAutoCompleteButton, "StaticObjectAutoCompleteButton");
			this.StaticObjectAutoCompleteButton.Name = "StaticObjectAutoCompleteButton";
			this.StaticObjectAutoCompleteButton.UseVisualStyleBackColor = true;
			this.StaticObjectAutoCompleteButton.Click += new global::System.EventHandler(this.StaticObjectAutoCompleteButton_Click);
			resources.ApplyResources(this.StaticObjectRemoveButton, "StaticObjectRemoveButton");
			this.StaticObjectRemoveButton.Name = "StaticObjectRemoveButton";
			this.StaticObjectRemoveButton.UseVisualStyleBackColor = true;
			this.StaticObjectRemoveButton.Click += new global::System.EventHandler(this.StaticObjectRemoveButton_Click);
			resources.ApplyResources(this.RoadItemSetItemsLabel, "RoadItemSetItemsLabel");
			this.RoadItemSetItemsLabel.Name = "RoadItemSetItemsLabel";
			resources.ApplyResources(this.RoadItemSetItemsListView, "RoadItemSetItemsListView");
			this.RoadItemSetItemsListView.HideSelection = false;
			this.RoadItemSetItemsListView.Name = "RoadItemSetItemsListView";
			this.RoadItemSetItemsListView.ShowItemToolTips = true;
			this.RoadItemSetItemsListView.Sorting = global::System.Windows.Forms.SortOrder.Ascending;
			this.RoadItemSetItemsListView.UseCompatibleStateImageBehavior = false;
			resources.ApplyResources(this.RoadPropertyLabel, "RoadPropertyLabel");
			this.RoadPropertyLabel.Name = "RoadPropertyLabel";
			resources.ApplyResources(this.RoadPropertyGrid, "RoadPropertyGrid");
			this.RoadPropertyGrid.Name = "RoadPropertyGrid";
			resources.ApplyResources(this.CloseButton, "CloseButton");
			this.CloseButton.DialogResult = global::System.Windows.Forms.DialogResult.Cancel;
			this.CloseButton.Name = "CloseButton";
			this.CloseButton.UseVisualStyleBackColor = true;
			this.CloseButton.Click += new global::System.EventHandler(this.CloseButton_Click);
			resources.ApplyResources(this.RoadNameLabel, "RoadNameLabel");
			this.RoadNameLabel.Name = "RoadNameLabel";
			resources.ApplyResources(this.RoadNameButton, "RoadNameButton");
			this.RoadNameButton.Name = "RoadNameButton";
			this.RoadNameButton.UseVisualStyleBackColor = true;
			this.RoadNameButton.Click += new global::System.EventHandler(this.RoadNameButton_Click);
			resources.ApplyResources(this.RoadNameTextBox, "RoadNameTextBox");
			this.RoadNameTextBox.Name = "RoadNameTextBox";
			this.RoadNameTextBox.TextChanged += new global::System.EventHandler(this.RoadNameTextBox_TextChanged);
			resources.ApplyResources(this.SaveButton, "SaveButton");
			this.SaveButton.Name = "SaveButton";
			this.SaveButton.UseVisualStyleBackColor = true;
			this.SaveButton.Click += new global::System.EventHandler(this.SaveButton_Click);
			resources.ApplyResources(this.DefaultsButton, "DefaultsButton");
			this.DefaultsButton.Name = "DefaultsButton";
			this.DefaultsButton.UseVisualStyleBackColor = true;
			this.DefaultsButton.Click += new global::System.EventHandler(this.DefaultsButton_Click);
			this.AvailableTilesContextMenu.Items.AddRange(new global::System.Windows.Forms.ToolStripItem[]
			{
				this.AvailableTilesContextMenuSeparator00,
				this.AvailableTilesContextMenuItemSetStripeTile,
				this.AvailableTilesContextMenuItemSetRoadTile
			});
			this.AvailableTilesContextMenu.Name = "AvailableTilesContextMenu";
			resources.ApplyResources(this.AvailableTilesContextMenu, "AvailableTilesContextMenu");
			this.AvailableTilesContextMenuSeparator00.Name = "AvailableTilesContextMenuSeparator00";
			resources.ApplyResources(this.AvailableTilesContextMenuSeparator00, "AvailableTilesContextMenuSeparator00");
			this.AvailableTilesContextMenuItemSetStripeTile.Name = "AvailableTilesContextMenuItemSetStripeTile";
			resources.ApplyResources(this.AvailableTilesContextMenuItemSetStripeTile, "AvailableTilesContextMenuItemSetStripeTile");
			this.AvailableTilesContextMenuItemSetStripeTile.Click += new global::System.EventHandler(this.AvailableTilesContextMenuItemSetStripeTile_Click);
			this.AvailableTilesContextMenuItemSetRoadTile.Name = "AvailableTilesContextMenuItemSetRoadTile";
			resources.ApplyResources(this.AvailableTilesContextMenuItemSetRoadTile, "AvailableTilesContextMenuItemSetRoadTile");
			this.AvailableTilesContextMenuItemSetRoadTile.Click += new global::System.EventHandler(this.AvailableTilesContextMenuItemSetRoadTile_Click);
			this.RoadContextMenu.Items.AddRange(new global::System.Windows.Forms.ToolStripItem[]
			{
				this.RoadContextMenuItemSetarator00,
				this.RoadContextMenuItemLoad,
				this.RoadContextMenuItemSaveTo
			});
			this.RoadContextMenu.Name = "RoadContextMenuStrip";
			resources.ApplyResources(this.RoadContextMenu, "RoadContextMenu");
			this.RoadContextMenuItemSetarator00.Name = "RoadContextMenuItemSetarator00";
			resources.ApplyResources(this.RoadContextMenuItemSetarator00, "RoadContextMenuItemSetarator00");
			this.RoadContextMenuItemLoad.Name = "RoadContextMenuItemLoad";
			resources.ApplyResources(this.RoadContextMenuItemLoad, "RoadContextMenuItemLoad");
			this.RoadContextMenuItemLoad.Click += new global::System.EventHandler(this.RoadContextMenuItemLoad_Click);
			this.RoadContextMenuItemSaveTo.Name = "RoadContextMenuItemSaveTo";
			resources.ApplyResources(this.RoadContextMenuItemSaveTo, "RoadContextMenuItemSaveTo");
			this.RoadContextMenuItemSaveTo.Click += new global::System.EventHandler(this.RoadContextMenuItemSaveTo_Click);
			this.AvailableStaticObjectsContextMenu.Items.AddRange(new global::System.Windows.Forms.ToolStripItem[]
			{
				this.AvailableStaticObjectsContextMenuSeparator00,
				this.AvailableStaticObjectsContextMenuAdd
			});
			this.AvailableStaticObjectsContextMenu.Name = "AvailableStaticObjectsContextMenu";
			resources.ApplyResources(this.AvailableStaticObjectsContextMenu, "AvailableStaticObjectsContextMenu");
			this.AvailableStaticObjectsContextMenuSeparator00.Name = "AvailableStaticObjectsContextMenuSeparator00";
			resources.ApplyResources(this.AvailableStaticObjectsContextMenuSeparator00, "AvailableStaticObjectsContextMenuSeparator00");
			this.AvailableStaticObjectsContextMenuAdd.Name = "AvailableStaticObjectsContextMenuAdd";
			resources.ApplyResources(this.AvailableStaticObjectsContextMenuAdd, "AvailableStaticObjectsContextMenuAdd");
			this.AvailableStaticObjectsContextMenuAdd.Click += new global::System.EventHandler(this.AvailableStaticObjectsContextMenuAdd_Click);
			this.RoadItemSetItemsContextMenu.Items.AddRange(new global::System.Windows.Forms.ToolStripItem[]
			{
				this.RoadItemSetItemsContextMenuSeparator00,
				this.RoadItemSetItemsContextMenuRemove,
				this.RoadItemSetItemsContextMenuSet,
				this.RoadItemSetItemsContextMenuAuto
			});
			this.RoadItemSetItemsContextMenu.Name = "RoadItemSetItemsContextMenu";
			resources.ApplyResources(this.RoadItemSetItemsContextMenu, "RoadItemSetItemsContextMenu");
			this.RoadItemSetItemsContextMenuSeparator00.Name = "RoadItemSetItemsContextMenuSeparator00";
			resources.ApplyResources(this.RoadItemSetItemsContextMenuSeparator00, "RoadItemSetItemsContextMenuSeparator00");
			this.RoadItemSetItemsContextMenuRemove.Name = "RoadItemSetItemsContextMenuRemove";
			resources.ApplyResources(this.RoadItemSetItemsContextMenuRemove, "RoadItemSetItemsContextMenuRemove");
			this.RoadItemSetItemsContextMenuRemove.Click += new global::System.EventHandler(this.RoadItemSetItemsContextMenuRemove_Click);
			this.RoadItemSetItemsContextMenuSet.Name = "RoadItemSetItemsContextMenuSet";
			resources.ApplyResources(this.RoadItemSetItemsContextMenuSet, "RoadItemSetItemsContextMenuSet");
			this.RoadItemSetItemsContextMenuSet.Click += new global::System.EventHandler(this.RoadItemSetItemsContextMenuSet_Click);
			this.RoadItemSetItemsContextMenuAuto.Name = "RoadItemSetItemsContextMenuAuto";
			resources.ApplyResources(this.RoadItemSetItemsContextMenuAuto, "RoadItemSetItemsContextMenuAuto");
			this.RoadItemSetItemsContextMenuAuto.Click += new global::System.EventHandler(this.RoadItemSetItemsContextMenuAuto_Click);
			resources.ApplyResources(this, "$this");
			base.AutoScaleMode = global::System.Windows.Forms.AutoScaleMode.Font;
			base.CancelButton = this.CloseButton;
			base.Controls.Add(this.DefaultsButton);
			base.Controls.Add(this.MainSplitContainer);
			base.Controls.Add(this.RoadNameButton);
			base.Controls.Add(this.SaveButton);
			base.Controls.Add(this.CloseButton);
			base.Controls.Add(this.RoadNameTextBox);
			base.Controls.Add(this.RoadNameLabel);
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.Name = "RoadParamsBrowserForm";
			base.ShowInTaskbar = false;
			this.MainSplitContainer.Panel1.ResumeLayout(false);
			this.MainSplitContainer.Panel2.ResumeLayout(false);
			this.MainSplitContainer.Panel2.PerformLayout();
			this.MainSplitContainer.ResumeLayout(false);
			this.MainTabControl.ResumeLayout(false);
			this.tabPage1.ResumeLayout(false);
			this.tabPage1.PerformLayout();
			this.tabPage2.ResumeLayout(false);
			this.RoadStripeSplitContainer.Panel1.ResumeLayout(false);
			this.RoadStripeSplitContainer.Panel2.ResumeLayout(false);
			this.RoadStripeSplitContainer.ResumeLayout(false);
			this.RoadStripeContextMenu.ResumeLayout(false);
			this.AvailableTilesSplitContainer.Panel1.ResumeLayout(false);
			this.AvailableTilesSplitContainer.Panel1.PerformLayout();
			this.AvailableTilesSplitContainer.Panel2.ResumeLayout(false);
			this.AvailableTilesSplitContainer.ResumeLayout(false);
			this.RoadStripePropertySplitContainer.Panel1.ResumeLayout(false);
			this.RoadStripePropertySplitContainer.Panel1.PerformLayout();
			this.RoadStripePropertySplitContainer.Panel2.ResumeLayout(false);
			this.RoadStripePropertySplitContainer.Panel2.PerformLayout();
			this.RoadStripePropertySplitContainer.ResumeLayout(false);
			((global::System.ComponentModel.ISupportInitialize)this.RoadImage).EndInit();
			((global::System.ComponentModel.ISupportInitialize)this.RoadStripeImage).EndInit();
			this.tabPage3.ResumeLayout(false);
			this.RoadItemSetSplitContainer.Panel1.ResumeLayout(false);
			this.RoadItemSetSplitContainer.Panel2.ResumeLayout(false);
			this.RoadItemSetSplitContainer.ResumeLayout(false);
			this.RoadItemSetContextMenu.ResumeLayout(false);
			this.AvailableStaticObjectsSplitContainer.Panel1.ResumeLayout(false);
			this.AvailableStaticObjectsSplitContainer.Panel1.PerformLayout();
			this.AvailableStaticObjectsSplitContainer.Panel2.ResumeLayout(false);
			this.AvailableStaticObjectsSplitContainer.ResumeLayout(false);
			this.RoadItemSetPropertySplitContainer.Panel1.ResumeLayout(false);
			this.RoadItemSetPropertySplitContainer.Panel1.PerformLayout();
			this.RoadItemSetPropertySplitContainer.Panel2.ResumeLayout(false);
			this.RoadItemSetPropertySplitContainer.Panel2.PerformLayout();
			this.RoadItemSetPropertySplitContainer.ResumeLayout(false);
			this.AvailableTilesContextMenu.ResumeLayout(false);
			this.RoadContextMenu.ResumeLayout(false);
			this.AvailableStaticObjectsContextMenu.ResumeLayout(false);
			this.RoadItemSetItemsContextMenu.ResumeLayout(false);
			base.ResumeLayout(false);
			base.PerformLayout();
		}

		// Token: 0x04000E95 RID: 3733
		private global::System.ComponentModel.IContainer components;

		// Token: 0x04000E96 RID: 3734
		private global::System.Windows.Forms.Button CloseButton;

		// Token: 0x04000E97 RID: 3735
		private global::System.Windows.Forms.Label RoadNameLabel;

		// Token: 0x04000E98 RID: 3736
		private global::System.Windows.Forms.Button RoadNameButton;

		// Token: 0x04000E99 RID: 3737
		private global::System.Windows.Forms.TextBox RoadNameTextBox;

		// Token: 0x04000E9A RID: 3738
		private global::System.Windows.Forms.SplitContainer MainSplitContainer;

		// Token: 0x04000E9B RID: 3739
		private global::System.Windows.Forms.TabControl MainTabControl;

		// Token: 0x04000E9C RID: 3740
		private global::System.Windows.Forms.TabPage tabPage1;

		// Token: 0x04000E9D RID: 3741
		private global::System.Windows.Forms.TabPage tabPage2;

		// Token: 0x04000E9E RID: 3742
		private global::System.Windows.Forms.TabPage tabPage3;

		// Token: 0x04000E9F RID: 3743
		private global::System.Windows.Forms.ListView RoadListView;

		// Token: 0x04000EA0 RID: 3744
		private global::System.Windows.Forms.Button RoadFilterButton;

		// Token: 0x04000EA1 RID: 3745
		private global::System.Windows.Forms.Label RoadFilterLbel;

		// Token: 0x04000EA2 RID: 3746
		private global::System.Windows.Forms.ComboBox RoadFilterComboBox;

		// Token: 0x04000EA3 RID: 3747
		private global::System.Windows.Forms.PropertyGrid RoadPropertyGrid;

		// Token: 0x04000EA4 RID: 3748
		private global::System.Windows.Forms.SplitContainer RoadStripeSplitContainer;

		// Token: 0x04000EA5 RID: 3749
		private global::System.Windows.Forms.ListView RoadStripeListView;

		// Token: 0x04000EA6 RID: 3750
		private global::System.Windows.Forms.Button RoadStripeButtonAdd;

		// Token: 0x04000EA7 RID: 3751
		private global::System.Windows.Forms.Button RoadStripeButtonRemove;

		// Token: 0x04000EA8 RID: 3752
		private global::System.Windows.Forms.Button RoadStripeButtonDefaults;

		// Token: 0x04000EA9 RID: 3753
		private global::System.Windows.Forms.PropertyGrid RoadStripePropertyGrid;

		// Token: 0x04000EAA RID: 3754
		private global::System.Windows.Forms.SplitContainer AvailableTilesSplitContainer;

		// Token: 0x04000EAB RID: 3755
		private global::System.Windows.Forms.SplitContainer RoadStripePropertySplitContainer;

		// Token: 0x04000EAC RID: 3756
		private global::System.Windows.Forms.ListView AvailableTilesListView;

		// Token: 0x04000EAD RID: 3757
		private global::System.Windows.Forms.Label AvailableTilesLabel;

		// Token: 0x04000EAE RID: 3758
		private global::System.Windows.Forms.Label RoadStripePropertyLabel;

		// Token: 0x04000EAF RID: 3759
		private global::System.Windows.Forms.Label RoadStripeTileLabel;

		// Token: 0x04000EB0 RID: 3760
		private global::System.Windows.Forms.SplitContainer RoadItemSetSplitContainer;

		// Token: 0x04000EB1 RID: 3761
		private global::System.Windows.Forms.Button RoadItemSetButtonDefaults;

		// Token: 0x04000EB2 RID: 3762
		private global::System.Windows.Forms.ListView RoadItemSetListView;

		// Token: 0x04000EB3 RID: 3763
		private global::System.Windows.Forms.Button RoadItemSetButtonAdd;

		// Token: 0x04000EB4 RID: 3764
		private global::System.Windows.Forms.Button RoadItemSetButtonRemove;

		// Token: 0x04000EB5 RID: 3765
		private global::System.Windows.Forms.SplitContainer AvailableStaticObjectsSplitContainer;

		// Token: 0x04000EB6 RID: 3766
		private global::System.Windows.Forms.ListView AvailableStaticObjectsListView;

		// Token: 0x04000EB7 RID: 3767
		private global::System.Windows.Forms.SplitContainer RoadItemSetPropertySplitContainer;

		// Token: 0x04000EB8 RID: 3768
		private global::System.Windows.Forms.PropertyGrid RoadItemSetPropertyGrid;

		// Token: 0x04000EB9 RID: 3769
		private global::System.Windows.Forms.ListView RoadItemSetItemsListView;

		// Token: 0x04000EBA RID: 3770
		private global::System.Windows.Forms.Label RoadPropertyLabel;

		// Token: 0x04000EBB RID: 3771
		private global::System.Windows.Forms.Label AvailableStaticObjectsLabel;

		// Token: 0x04000EBC RID: 3772
		private global::System.Windows.Forms.Label RoadItemSetPropertyLablel;

		// Token: 0x04000EBD RID: 3773
		private global::System.Windows.Forms.Label RoadItemSetItemsLabel;

		// Token: 0x04000EBE RID: 3774
		private global::System.Windows.Forms.ComboBox AvailableTilesFilterComboBox;

		// Token: 0x04000EBF RID: 3775
		private global::System.Windows.Forms.Label AvailableTilesFilterLabel;

		// Token: 0x04000EC0 RID: 3776
		private global::System.Windows.Forms.Button AvailableTilesFilterButton;

		// Token: 0x04000EC1 RID: 3777
		private global::System.Windows.Forms.ComboBox AvailableStaticObjectsFilterComboBox;

		// Token: 0x04000EC2 RID: 3778
		private global::System.Windows.Forms.Label AvailableStaticObjectsFilterLable;

		// Token: 0x04000EC3 RID: 3779
		private global::System.Windows.Forms.Button AvailableStaticObjectsFilterButton;

		// Token: 0x04000EC4 RID: 3780
		private global::System.Windows.Forms.Button SaveButton;

		// Token: 0x04000EC5 RID: 3781
		private global::System.Windows.Forms.Button LoadButton;

		// Token: 0x04000EC6 RID: 3782
		private global::System.Windows.Forms.Button SaveToButton;

		// Token: 0x04000EC7 RID: 3783
		private global::System.Windows.Forms.ColumnHeader RoadStripeListViewColumnName;

		// Token: 0x04000EC8 RID: 3784
		private global::System.Windows.Forms.ColumnHeader RoadStripeListViewColumnAnchor;

		// Token: 0x04000EC9 RID: 3785
		private global::System.Windows.Forms.ColumnHeader RoadStripeListViewColumnWidth;

		// Token: 0x04000ECA RID: 3786
		private global::System.Windows.Forms.ColumnHeader RoadStripeListViewColumnTile;

		// Token: 0x04000ECB RID: 3787
		private global::System.Windows.Forms.ColumnHeader RoadItemSetListViewColumnName;

		// Token: 0x04000ECC RID: 3788
		private global::System.Windows.Forms.ColumnHeader RoadItemSetListViewColumnAnchor;

		// Token: 0x04000ECD RID: 3789
		private global::System.Windows.Forms.ColumnHeader RoadItemSetListViewColumnDistance;

		// Token: 0x04000ECE RID: 3790
		private global::System.Windows.Forms.ColumnHeader RoadItemSetListViewColumnCount;

		// Token: 0x04000ECF RID: 3791
		private global::System.Windows.Forms.ColumnHeader RoadItemSetListViewColumnType;

		// Token: 0x04000ED0 RID: 3792
		private global::System.Windows.Forms.Button DefaultsButton;

		// Token: 0x04000ED1 RID: 3793
		private global::System.Windows.Forms.Label StaticObjectWeightLabel;

		// Token: 0x04000ED2 RID: 3794
		private global::System.Windows.Forms.Button StaticObjectAddButton;

		// Token: 0x04000ED3 RID: 3795
		private global::System.Windows.Forms.ComboBox StaticObjectWeightComboBox;

		// Token: 0x04000ED4 RID: 3796
		private global::System.Windows.Forms.Button StaticObjectRemoveButton;

		// Token: 0x04000ED5 RID: 3797
		private global::System.Windows.Forms.Button StaticObjectAutoCompleteButton;

		// Token: 0x04000ED6 RID: 3798
		private global::System.Windows.Forms.Button SetStripeTileButton;

		// Token: 0x04000ED7 RID: 3799
		private global::System.Windows.Forms.Button SetRoadTileButton;

		// Token: 0x04000ED8 RID: 3800
		private global::System.Windows.Forms.Button ClearRoadTileButton;

		// Token: 0x04000ED9 RID: 3801
		private global::System.Windows.Forms.Button ClearStripeTileButton;

		// Token: 0x04000EDA RID: 3802
		private global::System.Windows.Forms.Label RoadTileLabel;

		// Token: 0x04000EDB RID: 3803
		private global::System.Windows.Forms.PictureBox RoadImage;

		// Token: 0x04000EDC RID: 3804
		private global::System.Windows.Forms.PictureBox RoadStripeImage;

		// Token: 0x04000EDD RID: 3805
		private global::System.Windows.Forms.ToolTip TileToolTip;

		// Token: 0x04000EDE RID: 3806
		private global::System.Windows.Forms.ContextMenuStrip AvailableTilesContextMenu;

		// Token: 0x04000EDF RID: 3807
		private global::System.Windows.Forms.ToolStripMenuItem AvailableTilesContextMenuItemSetStripeTile;

		// Token: 0x04000EE0 RID: 3808
		private global::System.Windows.Forms.ToolStripMenuItem AvailableTilesContextMenuItemSetRoadTile;

		// Token: 0x04000EE1 RID: 3809
		private global::System.Windows.Forms.ToolStripSeparator AvailableTilesContextMenuSeparator00;

		// Token: 0x04000EE2 RID: 3810
		private global::System.Windows.Forms.ContextMenuStrip RoadContextMenu;

		// Token: 0x04000EE3 RID: 3811
		private global::System.Windows.Forms.ToolStripMenuItem RoadContextMenuItemLoad;

		// Token: 0x04000EE4 RID: 3812
		private global::System.Windows.Forms.ToolStripMenuItem RoadContextMenuItemSaveTo;

		// Token: 0x04000EE5 RID: 3813
		private global::System.Windows.Forms.Button StaticObjectSetButton;

		// Token: 0x04000EE6 RID: 3814
		private global::System.Windows.Forms.ToolStripSeparator RoadContextMenuItemSetarator00;

		// Token: 0x04000EE7 RID: 3815
		private global::System.Windows.Forms.ContextMenuStrip RoadStripeContextMenu;

		// Token: 0x04000EE8 RID: 3816
		private global::System.Windows.Forms.ToolStripMenuItem RoadStripeContextMenuItemAdd;

		// Token: 0x04000EE9 RID: 3817
		private global::System.Windows.Forms.ToolStripMenuItem RoadStripeContextMenuItemRemove;

		// Token: 0x04000EEA RID: 3818
		private global::System.Windows.Forms.ToolStripMenuItem RoadStripeContextMenuItemDefaults;

		// Token: 0x04000EEB RID: 3819
		private global::System.Windows.Forms.ToolStripSeparator RoadStripeContextMenuSeparator00;

		// Token: 0x04000EEC RID: 3820
		private global::System.Windows.Forms.ContextMenuStrip RoadItemSetContextMenu;

		// Token: 0x04000EED RID: 3821
		private global::System.Windows.Forms.ContextMenuStrip AvailableStaticObjectsContextMenu;

		// Token: 0x04000EEE RID: 3822
		private global::System.Windows.Forms.ContextMenuStrip RoadItemSetItemsContextMenu;

		// Token: 0x04000EEF RID: 3823
		private global::System.Windows.Forms.ToolStripMenuItem RoadItemSetContextMenuItemAdd;

		// Token: 0x04000EF0 RID: 3824
		private global::System.Windows.Forms.ToolStripMenuItem RoadItemSetContextMenuItemRemove;

		// Token: 0x04000EF1 RID: 3825
		private global::System.Windows.Forms.ToolStripSeparator RoadItemSetContextMenuSeparator00;

		// Token: 0x04000EF2 RID: 3826
		private global::System.Windows.Forms.ToolStripMenuItem RoadItemSetContextMenuItemDefaults;

		// Token: 0x04000EF3 RID: 3827
		private global::System.Windows.Forms.ToolStripSeparator AvailableStaticObjectsContextMenuSeparator00;

		// Token: 0x04000EF4 RID: 3828
		private global::System.Windows.Forms.ToolStripMenuItem AvailableStaticObjectsContextMenuAdd;

		// Token: 0x04000EF5 RID: 3829
		private global::System.Windows.Forms.ToolStripSeparator RoadItemSetItemsContextMenuSeparator00;

		// Token: 0x04000EF6 RID: 3830
		private global::System.Windows.Forms.ToolStripMenuItem RoadItemSetItemsContextMenuRemove;

		// Token: 0x04000EF7 RID: 3831
		private global::System.Windows.Forms.ToolStripMenuItem RoadItemSetItemsContextMenuSet;

		// Token: 0x04000EF8 RID: 3832
		private global::System.Windows.Forms.ToolStripMenuItem RoadItemSetItemsContextMenuAuto;
	}
}
