namespace MapEditor.Forms.Checkers
{
	// Token: 0x020000B8 RID: 184
	public partial class CheckersForm : global::MapEditor.Forms.Base.BaseForm
	{
		// Token: 0x060008F3 RID: 2291 RVA: 0x0004D42D File Offset: 0x0004C42D
		protected override void Dispose(bool disposing)
		{
			if (disposing && this.components != null)
			{
				this.components.Dispose();
			}
			base.Dispose(disposing);
		}

		// Token: 0x060008F4 RID: 2292 RVA: 0x0004D44C File Offset: 0x0004C44C
		private void InitializeComponent()
		{
			this.components = new global::System.ComponentModel.Container();
			global::System.ComponentModel.ComponentResourceManager resources = new global::System.ComponentModel.ComponentResourceManager(typeof(global::MapEditor.Forms.Checkers.CheckersForm));
			this.MainSplitContainer = new global::System.Windows.Forms.SplitContainer();
			this.CheckersSplitContainer = new global::System.Windows.Forms.SplitContainer();
			this.CheckersListView = new global::System.Windows.Forms.ListView();
			this.CheckerListColumnHeader00 = new global::System.Windows.Forms.ColumnHeader();
			this.CheckerListColumnHeader01 = new global::System.Windows.Forms.ColumnHeader();
			this.CheckerListColumnHeader02 = new global::System.Windows.Forms.ColumnHeader();
			this.CheckerListColumnHeader03 = new global::System.Windows.Forms.ColumnHeader();
			this.CheckersContextMenu = new global::System.Windows.Forms.ContextMenuStrip(this.components);
			this.MenuStripItemCheck = new global::System.Windows.Forms.ToolStripMenuItem();
			this.MenuStripItemSeparator00 = new global::System.Windows.Forms.ToolStripSeparator();
			this.MenuStripItemSave = new global::System.Windows.Forms.ToolStripMenuItem();
			this.MenuStripItemSaveAll = new global::System.Windows.Forms.ToolStripMenuItem();
			this.MenuStripItemFix = new global::System.Windows.Forms.ToolStripMenuItem();
			this.MenuStripItemFixAll = new global::System.Windows.Forms.ToolStripMenuItem();
			this.CheckersImageList = new global::System.Windows.Forms.ImageList(this.components);
			this.ResultSplitContainer = new global::System.Windows.Forms.SplitContainer();
			this.DescriptionTextBox = new global::System.Windows.Forms.TextBox();
			this.ResultTextBox = new global::System.Windows.Forms.TextBox();
			this.InfoTabControl = new global::System.Windows.Forms.TabControl();
			this.TreeViewInfoTabPage = new global::System.Windows.Forms.TabPage();
			this.InfoTreeView = new global::System.Windows.Forms.TreeView();
			this.TextInfoTabPage = new global::System.Windows.Forms.TabPage();
			this.InfoTextBox = new global::System.Windows.Forms.TextBox();
			this.MainTooltip = new global::System.Windows.Forms.ToolTip(this.components);
			this.MainToolstrip = new global::System.Windows.Forms.ToolStrip();
			this.ToolbarItemCheck = new global::System.Windows.Forms.ToolStripButton();
			this.ToolbarItemSeparator00 = new global::System.Windows.Forms.ToolStripSeparator();
			this.ToolbarItemSave = new global::System.Windows.Forms.ToolStripButton();
			this.ToolbarItemSaveAll = new global::System.Windows.Forms.ToolStripButton();
			this.ToolbarItemSeparator01 = new global::System.Windows.Forms.ToolStripSeparator();
			this.ToolbarItemFix = new global::System.Windows.Forms.ToolStripButton();
			this.ToolbarItemFixAll = new global::System.Windows.Forms.ToolStripButton();
			this.MainSplitContainer.Panel1.SuspendLayout();
			this.MainSplitContainer.Panel2.SuspendLayout();
			this.MainSplitContainer.SuspendLayout();
			this.CheckersSplitContainer.Panel1.SuspendLayout();
			this.CheckersSplitContainer.Panel2.SuspendLayout();
			this.CheckersSplitContainer.SuspendLayout();
			this.CheckersContextMenu.SuspendLayout();
			this.ResultSplitContainer.Panel1.SuspendLayout();
			this.ResultSplitContainer.Panel2.SuspendLayout();
			this.ResultSplitContainer.SuspendLayout();
			this.InfoTabControl.SuspendLayout();
			this.TreeViewInfoTabPage.SuspendLayout();
			this.TextInfoTabPage.SuspendLayout();
			this.MainToolstrip.SuspendLayout();
			base.SuspendLayout();
			resources.ApplyResources(this.MainSplitContainer, "MainSplitContainer");
			this.MainSplitContainer.Name = "MainSplitContainer";
			this.MainSplitContainer.Panel1.Controls.Add(this.CheckersSplitContainer);
			this.MainSplitContainer.Panel2.Controls.Add(this.InfoTabControl);
			resources.ApplyResources(this.CheckersSplitContainer, "CheckersSplitContainer");
			this.CheckersSplitContainer.Name = "CheckersSplitContainer";
			this.CheckersSplitContainer.Panel1.Controls.Add(this.CheckersListView);
			this.CheckersSplitContainer.Panel2.Controls.Add(this.ResultSplitContainer);
			resources.ApplyResources(this.CheckersListView, "CheckersListView");
			this.CheckersListView.Columns.AddRange(new global::System.Windows.Forms.ColumnHeader[]
			{
				this.CheckerListColumnHeader00,
				this.CheckerListColumnHeader01,
				this.CheckerListColumnHeader02,
				this.CheckerListColumnHeader03
			});
			this.CheckersListView.ContextMenuStrip = this.CheckersContextMenu;
			this.CheckersListView.FullRowSelect = true;
			this.CheckersListView.HideSelection = false;
			this.CheckersListView.LargeImageList = this.CheckersImageList;
			this.CheckersListView.MultiSelect = false;
			this.CheckersListView.Name = "CheckersListView";
			this.CheckersListView.ShowItemToolTips = true;
			this.CheckersListView.SmallImageList = this.CheckersImageList;
			this.CheckersListView.Sorting = global::System.Windows.Forms.SortOrder.Ascending;
			this.CheckersListView.UseCompatibleStateImageBehavior = false;
			this.CheckersListView.View = global::System.Windows.Forms.View.Details;
			this.CheckersListView.SelectedIndexChanged += new global::System.EventHandler(this.CheckersListView_SelectedIndexChanged);
			resources.ApplyResources(this.CheckerListColumnHeader00, "CheckerListColumnHeader00");
			resources.ApplyResources(this.CheckerListColumnHeader01, "CheckerListColumnHeader01");
			resources.ApplyResources(this.CheckerListColumnHeader02, "CheckerListColumnHeader02");
			resources.ApplyResources(this.CheckerListColumnHeader03, "CheckerListColumnHeader03");
			this.CheckersContextMenu.Items.AddRange(new global::System.Windows.Forms.ToolStripItem[]
			{
				this.MenuStripItemCheck,
				this.MenuStripItemSeparator00,
				this.MenuStripItemSave,
				this.MenuStripItemSaveAll,
				this.MenuStripItemFix,
				this.MenuStripItemFixAll
			});
			this.CheckersContextMenu.Name = "CheckersContextMenu";
			resources.ApplyResources(this.CheckersContextMenu, "CheckersContextMenu");
			this.MenuStripItemCheck.Image = global::MapEditor.Properties.Resources.reallocate;
			resources.ApplyResources(this.MenuStripItemCheck, "MenuStripItemCheck");
			this.MenuStripItemCheck.Name = "MenuStripItemCheck";
			this.MenuStripItemCheck.Click += new global::System.EventHandler(this.MenuStripItemCheck_Click);
			this.MenuStripItemSeparator00.Name = "MenuStripItemSeparator00";
			resources.ApplyResources(this.MenuStripItemSeparator00, "MenuStripItemSeparator00");
			this.MenuStripItemSave.Image = global::MapEditor.Properties.Resources.save;
			resources.ApplyResources(this.MenuStripItemSave, "MenuStripItemSave");
			this.MenuStripItemSave.Name = "MenuStripItemSave";
			this.MenuStripItemSave.Click += new global::System.EventHandler(this.MenuStripItemSave_Click);
			this.MenuStripItemSaveAll.Image = global::MapEditor.Properties.Resources.save_all;
			resources.ApplyResources(this.MenuStripItemSaveAll, "MenuStripItemSaveAll");
			this.MenuStripItemSaveAll.Name = "MenuStripItemSaveAll";
			this.MenuStripItemSaveAll.Click += new global::System.EventHandler(this.MenuStripItemSaveAll_Click);
			this.MenuStripItemFix.Image = global::MapEditor.Properties.Resources.fix_checker;
			resources.ApplyResources(this.MenuStripItemFix, "MenuStripItemFix");
			this.MenuStripItemFix.Name = "MenuStripItemFix";
			this.MenuStripItemFix.Click += new global::System.EventHandler(this.MenuStripItemFix_Click);
			this.MenuStripItemFixAll.Image = global::MapEditor.Properties.Resources.fix_checkers;
			resources.ApplyResources(this.MenuStripItemFixAll, "MenuStripItemFixAll");
			this.MenuStripItemFixAll.Name = "MenuStripItemFixAll";
			this.MenuStripItemFixAll.Click += new global::System.EventHandler(this.MenuStripItemFixAll_Click);
			this.CheckersImageList.ImageStream = (global::System.Windows.Forms.ImageListStreamer)resources.GetObject("CheckersImageList.ImageStream");
			this.CheckersImageList.TransparentColor = global::System.Drawing.Color.Magenta;
			this.CheckersImageList.Images.SetKeyName(0, "green.bmp");
			this.CheckersImageList.Images.SetKeyName(1, "yellow.bmp");
			this.CheckersImageList.Images.SetKeyName(2, "red.bmp");
			resources.ApplyResources(this.ResultSplitContainer, "ResultSplitContainer");
			this.ResultSplitContainer.Name = "ResultSplitContainer";
			this.ResultSplitContainer.Panel1.Controls.Add(this.DescriptionTextBox);
			this.ResultSplitContainer.Panel2.Controls.Add(this.ResultTextBox);
			resources.ApplyResources(this.DescriptionTextBox, "DescriptionTextBox");
			this.DescriptionTextBox.Name = "DescriptionTextBox";
			this.DescriptionTextBox.ReadOnly = true;
			resources.ApplyResources(this.ResultTextBox, "ResultTextBox");
			this.ResultTextBox.Name = "ResultTextBox";
			this.ResultTextBox.ReadOnly = true;
			this.InfoTabControl.Controls.Add(this.TreeViewInfoTabPage);
			this.InfoTabControl.Controls.Add(this.TextInfoTabPage);
			resources.ApplyResources(this.InfoTabControl, "InfoTabControl");
			this.InfoTabControl.Name = "InfoTabControl";
			this.InfoTabControl.SelectedIndex = 0;
			this.TreeViewInfoTabPage.Controls.Add(this.InfoTreeView);
			resources.ApplyResources(this.TreeViewInfoTabPage, "TreeViewInfoTabPage");
			this.TreeViewInfoTabPage.Name = "TreeViewInfoTabPage";
			this.TreeViewInfoTabPage.UseVisualStyleBackColor = true;
			resources.ApplyResources(this.InfoTreeView, "InfoTreeView");
			this.InfoTreeView.HideSelection = false;
			this.InfoTreeView.Name = "InfoTreeView";
			this.TextInfoTabPage.Controls.Add(this.InfoTextBox);
			resources.ApplyResources(this.TextInfoTabPage, "TextInfoTabPage");
			this.TextInfoTabPage.Name = "TextInfoTabPage";
			this.TextInfoTabPage.UseVisualStyleBackColor = true;
			resources.ApplyResources(this.InfoTextBox, "InfoTextBox");
			this.InfoTextBox.Name = "InfoTextBox";
			this.InfoTextBox.ReadOnly = true;
			this.MainToolstrip.Items.AddRange(new global::System.Windows.Forms.ToolStripItem[]
			{
				this.ToolbarItemCheck,
				this.ToolbarItemSeparator00,
				this.ToolbarItemSave,
				this.ToolbarItemSaveAll,
				this.ToolbarItemSeparator01,
				this.ToolbarItemFix,
				this.ToolbarItemFixAll
			});
			resources.ApplyResources(this.MainToolstrip, "MainToolstrip");
			this.MainToolstrip.Name = "MainToolstrip";
			this.ToolbarItemCheck.DisplayStyle = global::System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.ToolbarItemCheck.Image = global::MapEditor.Properties.Resources.reallocate;
			resources.ApplyResources(this.ToolbarItemCheck, "ToolbarItemCheck");
			this.ToolbarItemCheck.Name = "ToolbarItemCheck";
			this.ToolbarItemCheck.Click += new global::System.EventHandler(this.ToolbarItemCheck_Click);
			this.ToolbarItemSeparator00.Name = "ToolbarItemSeparator00";
			resources.ApplyResources(this.ToolbarItemSeparator00, "ToolbarItemSeparator00");
			this.ToolbarItemSave.DisplayStyle = global::System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.ToolbarItemSave.Image = global::MapEditor.Properties.Resources.save;
			resources.ApplyResources(this.ToolbarItemSave, "ToolbarItemSave");
			this.ToolbarItemSave.Name = "ToolbarItemSave";
			this.ToolbarItemSave.Click += new global::System.EventHandler(this.ToolbarItemSave_Click);
			this.ToolbarItemSaveAll.DisplayStyle = global::System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.ToolbarItemSaveAll.Image = global::MapEditor.Properties.Resources.save_all;
			resources.ApplyResources(this.ToolbarItemSaveAll, "ToolbarItemSaveAll");
			this.ToolbarItemSaveAll.Name = "ToolbarItemSaveAll";
			this.ToolbarItemSaveAll.Click += new global::System.EventHandler(this.ToolbarItemSaveAll_Click);
			this.ToolbarItemSeparator01.Name = "ToolbarItemSeparator01";
			resources.ApplyResources(this.ToolbarItemSeparator01, "ToolbarItemSeparator01");
			this.ToolbarItemFix.DisplayStyle = global::System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.ToolbarItemFix.Image = global::MapEditor.Properties.Resources.fix_checker;
			resources.ApplyResources(this.ToolbarItemFix, "ToolbarItemFix");
			this.ToolbarItemFix.Name = "ToolbarItemFix";
			this.ToolbarItemFix.Click += new global::System.EventHandler(this.ToolbarItemFix_Click);
			this.ToolbarItemFixAll.DisplayStyle = global::System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.ToolbarItemFixAll.Image = global::MapEditor.Properties.Resources.fix_checkers;
			resources.ApplyResources(this.ToolbarItemFixAll, "ToolbarItemFixAll");
			this.ToolbarItemFixAll.Name = "ToolbarItemFixAll";
			this.ToolbarItemFixAll.Click += new global::System.EventHandler(this.ToolbarItemFixAll_Click);
			resources.ApplyResources(this, "$this");
			base.AutoScaleMode = global::System.Windows.Forms.AutoScaleMode.Font;
			base.Controls.Add(this.MainToolstrip);
			base.Controls.Add(this.MainSplitContainer);
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.Name = "CheckersForm";
			base.ShowInTaskbar = false;
			this.MainSplitContainer.Panel1.ResumeLayout(false);
			this.MainSplitContainer.Panel2.ResumeLayout(false);
			this.MainSplitContainer.ResumeLayout(false);
			this.CheckersSplitContainer.Panel1.ResumeLayout(false);
			this.CheckersSplitContainer.Panel2.ResumeLayout(false);
			this.CheckersSplitContainer.ResumeLayout(false);
			this.CheckersContextMenu.ResumeLayout(false);
			this.ResultSplitContainer.Panel1.ResumeLayout(false);
			this.ResultSplitContainer.Panel1.PerformLayout();
			this.ResultSplitContainer.Panel2.ResumeLayout(false);
			this.ResultSplitContainer.Panel2.PerformLayout();
			this.ResultSplitContainer.ResumeLayout(false);
			this.InfoTabControl.ResumeLayout(false);
			this.TreeViewInfoTabPage.ResumeLayout(false);
			this.TextInfoTabPage.ResumeLayout(false);
			this.TextInfoTabPage.PerformLayout();
			this.MainToolstrip.ResumeLayout(false);
			this.MainToolstrip.PerformLayout();
			base.ResumeLayout(false);
			base.PerformLayout();
		}

		// Token: 0x04000791 RID: 1937
		private global::System.ComponentModel.IContainer components;

		// Token: 0x04000792 RID: 1938
		private global::System.Windows.Forms.SplitContainer MainSplitContainer;

		// Token: 0x04000793 RID: 1939
		private global::System.Windows.Forms.SplitContainer CheckersSplitContainer;

		// Token: 0x04000794 RID: 1940
		private global::System.Windows.Forms.ListView CheckersListView;

		// Token: 0x04000795 RID: 1941
		private global::System.Windows.Forms.SplitContainer ResultSplitContainer;

		// Token: 0x04000796 RID: 1942
		private global::System.Windows.Forms.TextBox DescriptionTextBox;

		// Token: 0x04000797 RID: 1943
		private global::System.Windows.Forms.TextBox ResultTextBox;

		// Token: 0x04000798 RID: 1944
		private global::System.Windows.Forms.ToolTip MainTooltip;

		// Token: 0x04000799 RID: 1945
		private global::System.Windows.Forms.ToolStrip MainToolstrip;

		// Token: 0x0400079A RID: 1946
		private global::System.Windows.Forms.ToolStripButton ToolbarItemCheck;

		// Token: 0x0400079B RID: 1947
		private global::System.Windows.Forms.ToolStripButton ToolbarItemSave;

		// Token: 0x0400079C RID: 1948
		private global::System.Windows.Forms.ColumnHeader CheckerListColumnHeader00;

		// Token: 0x0400079D RID: 1949
		private global::System.Windows.Forms.ColumnHeader CheckerListColumnHeader01;

		// Token: 0x0400079E RID: 1950
		private global::System.Windows.Forms.ColumnHeader CheckerListColumnHeader02;

		// Token: 0x0400079F RID: 1951
		private global::System.Windows.Forms.ColumnHeader CheckerListColumnHeader03;

		// Token: 0x040007A0 RID: 1952
		private global::System.Windows.Forms.ImageList CheckersImageList;

		// Token: 0x040007A1 RID: 1953
		private global::System.Windows.Forms.ToolStripButton ToolbarItemSaveAll;

		// Token: 0x040007A2 RID: 1954
		private global::System.Windows.Forms.ContextMenuStrip CheckersContextMenu;

		// Token: 0x040007A3 RID: 1955
		private global::System.Windows.Forms.ToolStripSeparator ToolbarItemSeparator00;

		// Token: 0x040007A4 RID: 1956
		private global::System.Windows.Forms.ToolStripMenuItem MenuStripItemCheck;

		// Token: 0x040007A5 RID: 1957
		private global::System.Windows.Forms.ToolStripSeparator MenuStripItemSeparator00;

		// Token: 0x040007A6 RID: 1958
		private global::System.Windows.Forms.ToolStripMenuItem MenuStripItemSave;

		// Token: 0x040007A7 RID: 1959
		private global::System.Windows.Forms.ToolStripMenuItem MenuStripItemSaveAll;

		// Token: 0x040007A8 RID: 1960
		private global::System.Windows.Forms.TabControl InfoTabControl;

		// Token: 0x040007A9 RID: 1961
		private global::System.Windows.Forms.TabPage TreeViewInfoTabPage;

		// Token: 0x040007AA RID: 1962
		private global::System.Windows.Forms.TabPage TextInfoTabPage;

		// Token: 0x040007AB RID: 1963
		private global::System.Windows.Forms.TextBox InfoTextBox;

		// Token: 0x040007AC RID: 1964
		private global::System.Windows.Forms.TreeView InfoTreeView;

		// Token: 0x040007AD RID: 1965
		private global::System.Windows.Forms.ToolStripSeparator ToolbarItemSeparator01;

		// Token: 0x040007AE RID: 1966
		private global::System.Windows.Forms.ToolStripButton ToolbarItemFix;

		// Token: 0x040007AF RID: 1967
		private global::System.Windows.Forms.ToolStripButton ToolbarItemFixAll;

		// Token: 0x040007B0 RID: 1968
		private global::System.Windows.Forms.ToolStripMenuItem MenuStripItemFix;

		// Token: 0x040007B1 RID: 1969
		private global::System.Windows.Forms.ToolStripMenuItem MenuStripItemFixAll;
	}
}
