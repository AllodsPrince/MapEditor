namespace MapEditor.Forms.RouteObjectBrowser.ShipComposer
{
	// Token: 0x02000112 RID: 274
	public partial class ShipComposerForm : global::System.Windows.Forms.Form
	{
		// Token: 0x06000D9F RID: 3487 RVA: 0x00071DEB File Offset: 0x00070DEB
		protected override void Dispose(bool disposing)
		{
			if (disposing && this.components != null)
			{
				this.components.Dispose();
			}
			base.Dispose(disposing);
		}

		// Token: 0x06000DA0 RID: 3488 RVA: 0x00071E0C File Offset: 0x00070E0C
		private void InitializeComponent()
		{
			this.components = new global::System.ComponentModel.Container();
			global::System.ComponentModel.ComponentResourceManager resources = new global::System.ComponentModel.ComponentResourceManager(typeof(global::MapEditor.Forms.RouteObjectBrowser.ShipComposer.ShipComposerForm));
			this.menuStrip = new global::System.Windows.Forms.MenuStrip();
			this.ShipMenuItem = new global::System.Windows.Forms.ToolStripMenuItem();
			this.ShipMenuItemLoad = new global::System.Windows.Forms.ToolStripMenuItem();
			this.ShipMenuItemSave = new global::System.Windows.Forms.ToolStripMenuItem();
			this.ShipMenuItemRefresh = new global::System.Windows.Forms.ToolStripMenuItem();
			this.ShipMenuItemSeparator00 = new global::System.Windows.Forms.ToolStripSeparator();
			this.ShipMenuItemClose = new global::System.Windows.Forms.ToolStripMenuItem();
			this.SlotMenuItem = new global::System.Windows.Forms.ToolStripMenuItem();
			this.SlotMenuItemEdit = new global::System.Windows.Forms.ToolStripMenuItem();
			this.SlotMenuItemClear = new global::System.Windows.Forms.ToolStripMenuItem();
			this.toolStrip = new global::System.Windows.Forms.ToolStrip();
			this.ToolStripButtonLoad = new global::System.Windows.Forms.ToolStripButton();
			this.ToolStripButtonSave = new global::System.Windows.Forms.ToolStripButton();
			this.ToolStripButtonRefresh = new global::System.Windows.Forms.ToolStripButton();
			this.toolStripSeparator00 = new global::System.Windows.Forms.ToolStripSeparator();
			this.ToolStripButtonSlotEdit = new global::System.Windows.Forms.ToolStripButton();
			this.ToolStripButtonSlotClear = new global::System.Windows.Forms.ToolStripButton();
			this.splitContainer = new global::System.Windows.Forms.SplitContainer();
			this.ShipListView = new global::System.Windows.Forms.ListView();
			this.ShipColumnHeaderSlot = new global::System.Windows.Forms.ColumnHeader();
			this.ShipColumnHeaderSlotValue = new global::System.Windows.Forms.ColumnHeader();
			this.ShipContextMenuStrip = new global::System.Windows.Forms.ContextMenuStrip(this.components);
			this.ShipContextMenuStripEditSlot = new global::System.Windows.Forms.ToolStripMenuItem();
			this.ShipContextMenuStripClearSlot = new global::System.Windows.Forms.ToolStripMenuItem();
			this.ShipContextMenuStripSeparator00 = new global::System.Windows.Forms.ToolStripSeparator();
			this.ShipContextMenuStripRefresh = new global::System.Windows.Forms.ToolStripMenuItem();
			this.ShipContextMenuStripSave = new global::System.Windows.Forms.ToolStripMenuItem();
			this.ShipPropertyControl = new global::System.Windows.Forms.PropertyControl();
			this.menuStrip.SuspendLayout();
			this.toolStrip.SuspendLayout();
			this.splitContainer.Panel1.SuspendLayout();
			this.splitContainer.Panel2.SuspendLayout();
			this.splitContainer.SuspendLayout();
			this.ShipContextMenuStrip.SuspendLayout();
			base.SuspendLayout();
			this.menuStrip.Items.AddRange(new global::System.Windows.Forms.ToolStripItem[]
			{
				this.ShipMenuItem,
				this.SlotMenuItem
			});
			this.menuStrip.Location = new global::System.Drawing.Point(0, 0);
			this.menuStrip.Name = "menuStrip";
			this.menuStrip.Size = new global::System.Drawing.Size(855, 24);
			this.menuStrip.TabIndex = 0;
			this.menuStrip.Text = "menuStrip";
			this.ShipMenuItem.DropDownItems.AddRange(new global::System.Windows.Forms.ToolStripItem[]
			{
				this.ShipMenuItemLoad,
				this.ShipMenuItemSave,
				this.ShipMenuItemRefresh,
				this.ShipMenuItemSeparator00,
				this.ShipMenuItemClose
			});
			this.ShipMenuItem.Name = "ShipMenuItem";
			this.ShipMenuItem.Size = new global::System.Drawing.Size(39, 20);
			this.ShipMenuItem.Text = "Ship";
			this.ShipMenuItem.DropDownOpening += new global::System.EventHandler(this.ShipMenuItem_DropDownOpening);
			this.ShipMenuItemLoad.Image = (global::System.Drawing.Image)resources.GetObject("ShipMenuItemLoad.Image");
			this.ShipMenuItemLoad.ImageTransparentColor = global::System.Drawing.Color.Magenta;
			this.ShipMenuItemLoad.Name = "ShipMenuItemLoad";
			this.ShipMenuItemLoad.Size = new global::System.Drawing.Size(123, 22);
			this.ShipMenuItemLoad.Text = "Load";
			this.ShipMenuItemLoad.Click += new global::System.EventHandler(this.ShipMenuItemLoad_Click);
			this.ShipMenuItemSave.Image = (global::System.Drawing.Image)resources.GetObject("ShipMenuItemSave.Image");
			this.ShipMenuItemSave.ImageTransparentColor = global::System.Drawing.Color.Magenta;
			this.ShipMenuItemSave.Name = "ShipMenuItemSave";
			this.ShipMenuItemSave.Size = new global::System.Drawing.Size(123, 22);
			this.ShipMenuItemSave.Text = "Save";
			this.ShipMenuItemSave.Click += new global::System.EventHandler(this.ShipMenuItemSave_Click);
			this.ShipMenuItemRefresh.Image = (global::System.Drawing.Image)resources.GetObject("ShipMenuItemRefresh.Image");
			this.ShipMenuItemRefresh.ImageTransparentColor = global::System.Drawing.Color.Magenta;
			this.ShipMenuItemRefresh.Name = "ShipMenuItemRefresh";
			this.ShipMenuItemRefresh.Size = new global::System.Drawing.Size(123, 22);
			this.ShipMenuItemRefresh.Text = "Refresh";
			this.ShipMenuItemRefresh.Click += new global::System.EventHandler(this.ShipMenuItemRefresh_Click);
			this.ShipMenuItemSeparator00.Name = "ShipMenuItemSeparator00";
			this.ShipMenuItemSeparator00.Size = new global::System.Drawing.Size(120, 6);
			this.ShipMenuItemClose.ImageTransparentColor = global::System.Drawing.Color.Magenta;
			this.ShipMenuItemClose.Name = "ShipMenuItemClose";
			this.ShipMenuItemClose.Size = new global::System.Drawing.Size(123, 22);
			this.ShipMenuItemClose.Text = "Close";
			this.ShipMenuItemClose.Click += new global::System.EventHandler(this.ShipMenuItemClose_Click);
			this.SlotMenuItem.DropDownItems.AddRange(new global::System.Windows.Forms.ToolStripItem[]
			{
				this.SlotMenuItemEdit,
				this.SlotMenuItemClear
			});
			this.SlotMenuItem.Name = "SlotMenuItem";
			this.SlotMenuItem.Size = new global::System.Drawing.Size(37, 20);
			this.SlotMenuItem.Text = "Slot";
			this.SlotMenuItem.DropDownOpening += new global::System.EventHandler(this.SlotMenuItem_DropDownOpening);
			this.SlotMenuItemEdit.Image = (global::System.Drawing.Image)resources.GetObject("SlotMenuItemEdit.Image");
			this.SlotMenuItemEdit.ImageTransparentColor = global::System.Drawing.Color.Magenta;
			this.SlotMenuItemEdit.Name = "SlotMenuItemEdit";
			this.SlotMenuItemEdit.Size = new global::System.Drawing.Size(168, 22);
			this.SlotMenuItemEdit.Text = "Set Ship Device";
			this.SlotMenuItemEdit.Click += new global::System.EventHandler(this.SlotMenuItemEdit_Click);
			this.SlotMenuItemClear.Image = global::MapEditor.Properties.Resources.delete;
			this.SlotMenuItemClear.ImageTransparentColor = global::System.Drawing.Color.Magenta;
			this.SlotMenuItemClear.Name = "SlotMenuItemClear";
			this.SlotMenuItemClear.Size = new global::System.Drawing.Size(168, 22);
			this.SlotMenuItemClear.Text = "Clear Ship Device";
			this.SlotMenuItemClear.Click += new global::System.EventHandler(this.SlotMenuItemClear_Click);
			this.toolStrip.Items.AddRange(new global::System.Windows.Forms.ToolStripItem[]
			{
				this.ToolStripButtonLoad,
				this.ToolStripButtonSave,
				this.ToolStripButtonRefresh,
				this.toolStripSeparator00,
				this.ToolStripButtonSlotEdit,
				this.ToolStripButtonSlotClear
			});
			this.toolStrip.Location = new global::System.Drawing.Point(0, 24);
			this.toolStrip.Name = "toolStrip";
			this.toolStrip.Size = new global::System.Drawing.Size(855, 25);
			this.toolStrip.TabIndex = 1;
			this.toolStrip.Text = "toolStrip1";
			this.ToolStripButtonLoad.DisplayStyle = global::System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.ToolStripButtonLoad.Image = (global::System.Drawing.Image)resources.GetObject("ToolStripButtonLoad.Image");
			this.ToolStripButtonLoad.ImageTransparentColor = global::System.Drawing.Color.Magenta;
			this.ToolStripButtonLoad.Name = "ToolStripButtonLoad";
			this.ToolStripButtonLoad.Size = new global::System.Drawing.Size(23, 22);
			this.ToolStripButtonLoad.Text = "Load Ship";
			this.ToolStripButtonLoad.Click += new global::System.EventHandler(this.ToolStripButtonLoad_Click);
			this.ToolStripButtonSave.DisplayStyle = global::System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.ToolStripButtonSave.Image = (global::System.Drawing.Image)resources.GetObject("ToolStripButtonSave.Image");
			this.ToolStripButtonSave.ImageTransparentColor = global::System.Drawing.Color.Magenta;
			this.ToolStripButtonSave.Name = "ToolStripButtonSave";
			this.ToolStripButtonSave.Size = new global::System.Drawing.Size(23, 22);
			this.ToolStripButtonSave.Text = "Save Ship";
			this.ToolStripButtonSave.Click += new global::System.EventHandler(this.ToolStripButtonSave_Click);
			this.ToolStripButtonRefresh.DisplayStyle = global::System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.ToolStripButtonRefresh.Image = (global::System.Drawing.Image)resources.GetObject("ToolStripButtonRefresh.Image");
			this.ToolStripButtonRefresh.ImageTransparentColor = global::System.Drawing.Color.Magenta;
			this.ToolStripButtonRefresh.Name = "ToolStripButtonRefresh";
			this.ToolStripButtonRefresh.Size = new global::System.Drawing.Size(23, 22);
			this.ToolStripButtonRefresh.Text = "Refresh slots";
			this.ToolStripButtonRefresh.Click += new global::System.EventHandler(this.ToolStripButtonRefresh_Click);
			this.toolStripSeparator00.Name = "toolStripSeparator00";
			this.toolStripSeparator00.Size = new global::System.Drawing.Size(6, 25);
			this.ToolStripButtonSlotEdit.DisplayStyle = global::System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.ToolStripButtonSlotEdit.Image = (global::System.Drawing.Image)resources.GetObject("ToolStripButtonSlotEdit.Image");
			this.ToolStripButtonSlotEdit.ImageTransparentColor = global::System.Drawing.Color.Magenta;
			this.ToolStripButtonSlotEdit.Name = "ToolStripButtonSlotEdit";
			this.ToolStripButtonSlotEdit.Size = new global::System.Drawing.Size(23, 22);
			this.ToolStripButtonSlotEdit.Text = "Edit slot ship device";
			this.ToolStripButtonSlotEdit.Click += new global::System.EventHandler(this.ToolStripButtonSlotEdit_Click);
			this.ToolStripButtonSlotClear.DisplayStyle = global::System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.ToolStripButtonSlotClear.Image = global::MapEditor.Properties.Resources.delete;
			this.ToolStripButtonSlotClear.ImageTransparentColor = global::System.Drawing.Color.Magenta;
			this.ToolStripButtonSlotClear.Name = "ToolStripButtonSlotClear";
			this.ToolStripButtonSlotClear.Size = new global::System.Drawing.Size(23, 22);
			this.ToolStripButtonSlotClear.Text = "toolStripButton1";
			this.ToolStripButtonSlotClear.Click += new global::System.EventHandler(this.ToolStripButtonSlotClear_Click);
			this.splitContainer.Dock = global::System.Windows.Forms.DockStyle.Fill;
			this.splitContainer.Location = new global::System.Drawing.Point(0, 49);
			this.splitContainer.Name = "splitContainer";
			this.splitContainer.Panel1.Controls.Add(this.ShipListView);
			this.splitContainer.Panel2.Controls.Add(this.ShipPropertyControl);
			this.splitContainer.Size = new global::System.Drawing.Size(855, 535);
			this.splitContainer.SplitterDistance = 475;
			this.splitContainer.TabIndex = 2;
			this.ShipListView.Columns.AddRange(new global::System.Windows.Forms.ColumnHeader[]
			{
				this.ShipColumnHeaderSlot,
				this.ShipColumnHeaderSlotValue
			});
			this.ShipListView.ContextMenuStrip = this.ShipContextMenuStrip;
			this.ShipListView.Dock = global::System.Windows.Forms.DockStyle.Fill;
			this.ShipListView.FullRowSelect = true;
			this.ShipListView.HideSelection = false;
			this.ShipListView.Location = new global::System.Drawing.Point(0, 0);
			this.ShipListView.MultiSelect = false;
			this.ShipListView.Name = "ShipListView";
			this.ShipListView.ShowItemToolTips = true;
			this.ShipListView.Size = new global::System.Drawing.Size(475, 535);
			this.ShipListView.Sorting = global::System.Windows.Forms.SortOrder.Ascending;
			this.ShipListView.TabIndex = 0;
			this.ShipListView.UseCompatibleStateImageBehavior = false;
			this.ShipListView.View = global::System.Windows.Forms.View.Details;
			this.ShipListView.MouseDoubleClick += new global::System.Windows.Forms.MouseEventHandler(this.ShipListView_MouseDoubleClick);
			this.ShipListView.ItemSelectionChanged += new global::System.Windows.Forms.ListViewItemSelectionChangedEventHandler(this.ShipListView_ItemSelectionChanged);
			this.ShipListView.KeyDown += new global::System.Windows.Forms.KeyEventHandler(this.ShipListView_KeyDown);
			this.ShipColumnHeaderSlot.Text = "Slot";
			this.ShipColumnHeaderSlot.Width = 152;
			this.ShipColumnHeaderSlotValue.Text = "Ship Device";
			this.ShipColumnHeaderSlotValue.Width = 267;
			this.ShipContextMenuStrip.Items.AddRange(new global::System.Windows.Forms.ToolStripItem[]
			{
				this.ShipContextMenuStripEditSlot,
				this.ShipContextMenuStripClearSlot,
				this.ShipContextMenuStripSeparator00,
				this.ShipContextMenuStripRefresh,
				this.ShipContextMenuStripSave
			});
			this.ShipContextMenuStrip.Name = "ShipContextMenuStrip";
			this.ShipContextMenuStrip.Size = new global::System.Drawing.Size(169, 98);
			this.ShipContextMenuStrip.Opening += new global::System.ComponentModel.CancelEventHandler(this.ShipContextMenuStrip_Opening);
			this.ShipContextMenuStripEditSlot.Image = (global::System.Drawing.Image)resources.GetObject("ShipContextMenuStripEditSlot.Image");
			this.ShipContextMenuStripEditSlot.ImageTransparentColor = global::System.Drawing.Color.Magenta;
			this.ShipContextMenuStripEditSlot.Name = "ShipContextMenuStripEditSlot";
			this.ShipContextMenuStripEditSlot.Size = new global::System.Drawing.Size(168, 22);
			this.ShipContextMenuStripEditSlot.Text = "Set Ship Device";
			this.ShipContextMenuStripEditSlot.Click += new global::System.EventHandler(this.ShipContextMenuStripEditSlot_Click);
			this.ShipContextMenuStripClearSlot.Image = global::MapEditor.Properties.Resources.delete;
			this.ShipContextMenuStripClearSlot.ImageTransparentColor = global::System.Drawing.Color.Magenta;
			this.ShipContextMenuStripClearSlot.Name = "ShipContextMenuStripClearSlot";
			this.ShipContextMenuStripClearSlot.Size = new global::System.Drawing.Size(168, 22);
			this.ShipContextMenuStripClearSlot.Text = "Clear Ship Device";
			this.ShipContextMenuStripClearSlot.Click += new global::System.EventHandler(this.ShipContextMenuStripClearSlot_Click);
			this.ShipContextMenuStripSeparator00.Name = "ShipContextMenuStripSeparator00";
			this.ShipContextMenuStripSeparator00.Size = new global::System.Drawing.Size(165, 6);
			this.ShipContextMenuStripRefresh.Image = (global::System.Drawing.Image)resources.GetObject("ShipContextMenuStripRefresh.Image");
			this.ShipContextMenuStripRefresh.ImageTransparentColor = global::System.Drawing.Color.Magenta;
			this.ShipContextMenuStripRefresh.Name = "ShipContextMenuStripRefresh";
			this.ShipContextMenuStripRefresh.Size = new global::System.Drawing.Size(168, 22);
			this.ShipContextMenuStripRefresh.Text = "Refresh";
			this.ShipContextMenuStripRefresh.Click += new global::System.EventHandler(this.ShipContextMenuStripRefresh_Click);
			this.ShipContextMenuStripSave.Image = (global::System.Drawing.Image)resources.GetObject("ShipContextMenuStripSave.Image");
			this.ShipContextMenuStripSave.ImageTransparentColor = global::System.Drawing.Color.Magenta;
			this.ShipContextMenuStripSave.Name = "ShipContextMenuStripSave";
			this.ShipContextMenuStripSave.Size = new global::System.Drawing.Size(168, 22);
			this.ShipContextMenuStripSave.Text = "Save";
			this.ShipContextMenuStripSave.Click += new global::System.EventHandler(this.ShipContextMenuStripSave_Click);
			this.ShipPropertyControl.DefaultLocationFolder = "";
			this.ShipPropertyControl.Dock = global::System.Windows.Forms.DockStyle.Fill;
			this.ShipPropertyControl.Location = new global::System.Drawing.Point(0, 0);
			this.ShipPropertyControl.Name = "ShipPropertyControl";
			this.ShipPropertyControl.SelectedObject = null;
			this.ShipPropertyControl.SelectedObjects = new object[0];
			this.ShipPropertyControl.Size = new global::System.Drawing.Size(376, 535);
			this.ShipPropertyControl.SkipRefresh = false;
			this.ShipPropertyControl.TabIndex = 0;
			this.ShipPropertyControl.TitleControl = null;
			this.ShipPropertyControl.TitleRelativeFrom = null;
			this.ShipPropertyControl.TitleStart = "";
			base.AutoScaleDimensions = new global::System.Drawing.SizeF(6f, 13f);
			base.AutoScaleMode = global::System.Windows.Forms.AutoScaleMode.Font;
			base.ClientSize = new global::System.Drawing.Size(855, 584);
			base.Controls.Add(this.splitContainer);
			base.Controls.Add(this.toolStrip);
			base.Controls.Add(this.menuStrip);
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.Name = "ShipComposerForm";
			base.ShowIcon = false;
			base.ShowInTaskbar = false;
			this.Text = "Ship Composer";
			base.Load += new global::System.EventHandler(this.ShipComposerForm_Load);
			base.FormClosed += new global::System.Windows.Forms.FormClosedEventHandler(this.ShipComposerForm_FormClosed);
			this.menuStrip.ResumeLayout(false);
			this.menuStrip.PerformLayout();
			this.toolStrip.ResumeLayout(false);
			this.toolStrip.PerformLayout();
			this.splitContainer.Panel1.ResumeLayout(false);
			this.splitContainer.Panel2.ResumeLayout(false);
			this.splitContainer.ResumeLayout(false);
			this.ShipContextMenuStrip.ResumeLayout(false);
			base.ResumeLayout(false);
			base.PerformLayout();
		}

		// Token: 0x04000AD9 RID: 2777
		private global::System.ComponentModel.IContainer components;

		// Token: 0x04000ADA RID: 2778
		private global::System.Windows.Forms.MenuStrip menuStrip;

		// Token: 0x04000ADB RID: 2779
		private global::System.Windows.Forms.ToolStripMenuItem ShipMenuItem;

		// Token: 0x04000ADC RID: 2780
		private global::System.Windows.Forms.ToolStripMenuItem ShipMenuItemLoad;

		// Token: 0x04000ADD RID: 2781
		private global::System.Windows.Forms.ToolStripMenuItem ShipMenuItemSave;

		// Token: 0x04000ADE RID: 2782
		private global::System.Windows.Forms.ToolStripSeparator ShipMenuItemSeparator00;

		// Token: 0x04000ADF RID: 2783
		private global::System.Windows.Forms.ToolStripMenuItem ShipMenuItemClose;

		// Token: 0x04000AE0 RID: 2784
		private global::System.Windows.Forms.ToolStrip toolStrip;

		// Token: 0x04000AE1 RID: 2785
		private global::System.Windows.Forms.ToolStripButton ToolStripButtonLoad;

		// Token: 0x04000AE2 RID: 2786
		private global::System.Windows.Forms.ToolStripButton ToolStripButtonSave;

		// Token: 0x04000AE3 RID: 2787
		private global::System.Windows.Forms.SplitContainer splitContainer;

		// Token: 0x04000AE4 RID: 2788
		private global::System.Windows.Forms.ListView ShipListView;

		// Token: 0x04000AE5 RID: 2789
		private global::System.Windows.Forms.ColumnHeader ShipColumnHeaderSlot;

		// Token: 0x04000AE6 RID: 2790
		private global::System.Windows.Forms.ColumnHeader ShipColumnHeaderSlotValue;

		// Token: 0x04000AE7 RID: 2791
		private global::System.Windows.Forms.PropertyControl ShipPropertyControl;

		// Token: 0x04000AE8 RID: 2792
		private global::System.Windows.Forms.ToolStripMenuItem SlotMenuItem;

		// Token: 0x04000AE9 RID: 2793
		private global::System.Windows.Forms.ToolStripMenuItem SlotMenuItemEdit;

		// Token: 0x04000AEA RID: 2794
		private global::System.Windows.Forms.ToolStripSeparator toolStripSeparator00;

		// Token: 0x04000AEB RID: 2795
		private global::System.Windows.Forms.ToolStripButton ToolStripButtonSlotEdit;

		// Token: 0x04000AEC RID: 2796
		private global::System.Windows.Forms.ToolStripMenuItem ShipMenuItemRefresh;

		// Token: 0x04000AED RID: 2797
		private global::System.Windows.Forms.ToolStripButton ToolStripButtonRefresh;

		// Token: 0x04000AEE RID: 2798
		private global::System.Windows.Forms.ContextMenuStrip ShipContextMenuStrip;

		// Token: 0x04000AEF RID: 2799
		private global::System.Windows.Forms.ToolStripMenuItem ShipContextMenuStripEditSlot;

		// Token: 0x04000AF0 RID: 2800
		private global::System.Windows.Forms.ToolStripSeparator ShipContextMenuStripSeparator00;

		// Token: 0x04000AF1 RID: 2801
		private global::System.Windows.Forms.ToolStripMenuItem ShipContextMenuStripRefresh;

		// Token: 0x04000AF2 RID: 2802
		private global::System.Windows.Forms.ToolStripMenuItem ShipContextMenuStripSave;

		// Token: 0x04000AF3 RID: 2803
		private global::System.Windows.Forms.ToolStripMenuItem SlotMenuItemClear;

		// Token: 0x04000AF4 RID: 2804
		private global::System.Windows.Forms.ToolStripButton ToolStripButtonSlotClear;

		// Token: 0x04000AF5 RID: 2805
		private global::System.Windows.Forms.ToolStripMenuItem ShipContextMenuStripClearSlot;
	}
}
