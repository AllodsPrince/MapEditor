namespace MapEditor.Forms.MultiObjectBrowser
{
	// Token: 0x0200006E RID: 110
	public partial class MultiObjectBrowserForm : global::MapEditor.Forms.Base.BaseForm
	{
		// Token: 0x06000577 RID: 1399 RVA: 0x0002C651 File Offset: 0x0002B651
		protected override void Dispose(bool disposing)
		{
			if (disposing && this.components != null)
			{
				this.components.Dispose();
			}
			base.Dispose(disposing);
		}

		// Token: 0x06000578 RID: 1400 RVA: 0x0002C670 File Offset: 0x0002B670
		private void InitializeComponent()
		{
			global::System.ComponentModel.ComponentResourceManager resources = new global::System.ComponentModel.ComponentResourceManager(typeof(global::MapEditor.Forms.MultiObjectBrowser.MultiObjectBrowserForm));
			this.SplitContainer = new global::System.Windows.Forms.SplitContainer();
			this.objectsLabel = new global::System.Windows.Forms.Label();
			this.ObjectsTreeView = new global::System.Windows.Forms.TreeView();
			this.AutoCompleteButton = new global::System.Windows.Forms.Button();
			this.AutoCompleteWithEmptyButton = new global::System.Windows.Forms.Button();
			this.ResetButton = new global::System.Windows.Forms.Button();
			this.CloseButton = new global::System.Windows.Forms.Button();
			this.TabControl = new global::System.Windows.Forms.TabControl();
			this.MultiObjectsTabPage = new global::System.Windows.Forms.TabPage();
			this.MultiObjectsFiltersComboBox = new global::System.Windows.Forms.ComboBox();
			this.MultiObjectsFiltersLabel = new global::System.Windows.Forms.Label();
			this.MultiObjectsFiltersButton = new global::System.Windows.Forms.Button();
			this.DeleteButton = new global::System.Windows.Forms.Button();
			this.LoadButton = new global::System.Windows.Forms.Button();
			this.MultiObjectsListView = new global::System.Windows.Forms.ListView();
			this.MultiObjectNameLabel = new global::System.Windows.Forms.Label();
			this.MultiObjectNameButton = new global::System.Windows.Forms.Button();
			this.MultiObjectNameTextBox = new global::System.Windows.Forms.TextBox();
			this.StaticObjectsTabPage = new global::System.Windows.Forms.TabPage();
			this.StaticObjectsFiltersComboBox = new global::System.Windows.Forms.ComboBox();
			this.StaticObjectsFiltersButton = new global::System.Windows.Forms.Button();
			this.StaticObjectsFiltersLabel = new global::System.Windows.Forms.Label();
			this.AddStaticObjectButton = new global::System.Windows.Forms.Button();
			this.StaticObjectsListView = new global::System.Windows.Forms.ListView();
			this.RemoveStaticObjectButton = new global::System.Windows.Forms.Button();
			this.SaveButton = new global::System.Windows.Forms.Button();
			this.SplitContainer.Panel1.SuspendLayout();
			this.SplitContainer.Panel2.SuspendLayout();
			this.SplitContainer.SuspendLayout();
			this.TabControl.SuspendLayout();
			this.MultiObjectsTabPage.SuspendLayout();
			this.StaticObjectsTabPage.SuspendLayout();
			base.SuspendLayout();
			resources.ApplyResources(this.SplitContainer, "SplitContainer");
			this.SplitContainer.Name = "SplitContainer";
			this.SplitContainer.Panel1.Controls.Add(this.objectsLabel);
			this.SplitContainer.Panel1.Controls.Add(this.ObjectsTreeView);
			this.SplitContainer.Panel1.Controls.Add(this.AutoCompleteButton);
			this.SplitContainer.Panel1.Controls.Add(this.AutoCompleteWithEmptyButton);
			this.SplitContainer.Panel1.Controls.Add(this.ResetButton);
			this.SplitContainer.Panel2.Controls.Add(this.CloseButton);
			this.SplitContainer.Panel2.Controls.Add(this.TabControl);
			this.SplitContainer.Panel2.Controls.Add(this.SaveButton);
			resources.ApplyResources(this.objectsLabel, "objectsLabel");
			this.objectsLabel.Name = "objectsLabel";
			resources.ApplyResources(this.ObjectsTreeView, "ObjectsTreeView");
			this.ObjectsTreeView.HideSelection = false;
			this.ObjectsTreeView.Name = "ObjectsTreeView";
			this.ObjectsTreeView.DoubleClick += new global::System.EventHandler(this.ObjectsTreeView_DoubleClick);
			resources.ApplyResources(this.AutoCompleteButton, "AutoCompleteButton");
			this.AutoCompleteButton.Name = "AutoCompleteButton";
			this.AutoCompleteButton.UseVisualStyleBackColor = true;
			this.AutoCompleteButton.Click += new global::System.EventHandler(this.AutoCompleteButton_Click);
			resources.ApplyResources(this.AutoCompleteWithEmptyButton, "AutoCompleteWithEmptyButton");
			this.AutoCompleteWithEmptyButton.Name = "AutoCompleteWithEmptyButton";
			this.AutoCompleteWithEmptyButton.UseVisualStyleBackColor = true;
			this.AutoCompleteWithEmptyButton.Click += new global::System.EventHandler(this.AutoCompleteWithEmptyButton_Click);
			resources.ApplyResources(this.ResetButton, "ResetButton");
			this.ResetButton.Name = "ResetButton";
			this.ResetButton.UseVisualStyleBackColor = true;
			this.ResetButton.Click += new global::System.EventHandler(this.ResetButton_Click);
			resources.ApplyResources(this.CloseButton, "CloseButton");
			this.CloseButton.DialogResult = global::System.Windows.Forms.DialogResult.Cancel;
			this.CloseButton.Name = "CloseButton";
			this.CloseButton.UseVisualStyleBackColor = true;
			this.CloseButton.Click += new global::System.EventHandler(this.CloseButton_Click);
			resources.ApplyResources(this.TabControl, "TabControl");
			this.TabControl.Controls.Add(this.MultiObjectsTabPage);
			this.TabControl.Controls.Add(this.StaticObjectsTabPage);
			this.TabControl.Name = "TabControl";
			this.TabControl.SelectedIndex = 0;
			this.MultiObjectsTabPage.Controls.Add(this.MultiObjectsFiltersComboBox);
			this.MultiObjectsTabPage.Controls.Add(this.MultiObjectsFiltersLabel);
			this.MultiObjectsTabPage.Controls.Add(this.MultiObjectsFiltersButton);
			this.MultiObjectsTabPage.Controls.Add(this.DeleteButton);
			this.MultiObjectsTabPage.Controls.Add(this.LoadButton);
			this.MultiObjectsTabPage.Controls.Add(this.MultiObjectsListView);
			this.MultiObjectsTabPage.Controls.Add(this.MultiObjectNameLabel);
			this.MultiObjectsTabPage.Controls.Add(this.MultiObjectNameButton);
			this.MultiObjectsTabPage.Controls.Add(this.MultiObjectNameTextBox);
			resources.ApplyResources(this.MultiObjectsTabPage, "MultiObjectsTabPage");
			this.MultiObjectsTabPage.Name = "MultiObjectsTabPage";
			this.MultiObjectsTabPage.UseVisualStyleBackColor = true;
			resources.ApplyResources(this.MultiObjectsFiltersComboBox, "MultiObjectsFiltersComboBox");
			this.MultiObjectsFiltersComboBox.FormattingEnabled = true;
			this.MultiObjectsFiltersComboBox.Name = "MultiObjectsFiltersComboBox";
			resources.ApplyResources(this.MultiObjectsFiltersLabel, "MultiObjectsFiltersLabel");
			this.MultiObjectsFiltersLabel.Name = "MultiObjectsFiltersLabel";
			resources.ApplyResources(this.MultiObjectsFiltersButton, "MultiObjectsFiltersButton");
			this.MultiObjectsFiltersButton.Name = "MultiObjectsFiltersButton";
			this.MultiObjectsFiltersButton.UseVisualStyleBackColor = true;
			this.MultiObjectsFiltersButton.Click += new global::System.EventHandler(this.MultiObjectsFiltersButton_Click);
			resources.ApplyResources(this.DeleteButton, "DeleteButton");
			this.DeleteButton.Name = "DeleteButton";
			this.DeleteButton.UseVisualStyleBackColor = true;
			this.DeleteButton.Click += new global::System.EventHandler(this.DeleteButton_Click);
			resources.ApplyResources(this.LoadButton, "LoadButton");
			this.LoadButton.Name = "LoadButton";
			this.LoadButton.UseVisualStyleBackColor = true;
			this.LoadButton.Click += new global::System.EventHandler(this.LoadButton_Click);
			resources.ApplyResources(this.MultiObjectsListView, "MultiObjectsListView");
			this.MultiObjectsListView.HideSelection = false;
			this.MultiObjectsListView.Name = "MultiObjectsListView";
			this.MultiObjectsListView.ShowItemToolTips = true;
			this.MultiObjectsListView.Sorting = global::System.Windows.Forms.SortOrder.Ascending;
			this.MultiObjectsListView.UseCompatibleStateImageBehavior = false;
			this.MultiObjectsListView.View = global::System.Windows.Forms.View.List;
			this.MultiObjectsListView.DoubleClick += new global::System.EventHandler(this.MultiObjectsListView_DoubleClick);
			resources.ApplyResources(this.MultiObjectNameLabel, "MultiObjectNameLabel");
			this.MultiObjectNameLabel.Name = "MultiObjectNameLabel";
			resources.ApplyResources(this.MultiObjectNameButton, "MultiObjectNameButton");
			this.MultiObjectNameButton.Name = "MultiObjectNameButton";
			this.MultiObjectNameButton.UseVisualStyleBackColor = true;
			this.MultiObjectNameButton.Click += new global::System.EventHandler(this.MultiObjectNameButton_Click);
			resources.ApplyResources(this.MultiObjectNameTextBox, "MultiObjectNameTextBox");
			this.MultiObjectNameTextBox.Name = "MultiObjectNameTextBox";
			this.MultiObjectNameTextBox.TextChanged += new global::System.EventHandler(this.MultiObjectNameTextBox_TextChanged);
			this.StaticObjectsTabPage.Controls.Add(this.StaticObjectsFiltersComboBox);
			this.StaticObjectsTabPage.Controls.Add(this.StaticObjectsFiltersButton);
			this.StaticObjectsTabPage.Controls.Add(this.StaticObjectsFiltersLabel);
			this.StaticObjectsTabPage.Controls.Add(this.AddStaticObjectButton);
			this.StaticObjectsTabPage.Controls.Add(this.StaticObjectsListView);
			this.StaticObjectsTabPage.Controls.Add(this.RemoveStaticObjectButton);
			resources.ApplyResources(this.StaticObjectsTabPage, "StaticObjectsTabPage");
			this.StaticObjectsTabPage.Name = "StaticObjectsTabPage";
			this.StaticObjectsTabPage.UseVisualStyleBackColor = true;
			resources.ApplyResources(this.StaticObjectsFiltersComboBox, "StaticObjectsFiltersComboBox");
			this.StaticObjectsFiltersComboBox.FormattingEnabled = true;
			this.StaticObjectsFiltersComboBox.Name = "StaticObjectsFiltersComboBox";
			resources.ApplyResources(this.StaticObjectsFiltersButton, "StaticObjectsFiltersButton");
			this.StaticObjectsFiltersButton.Name = "StaticObjectsFiltersButton";
			this.StaticObjectsFiltersButton.UseVisualStyleBackColor = true;
			this.StaticObjectsFiltersButton.Click += new global::System.EventHandler(this.StaticObjectsFiltersButton_Click);
			resources.ApplyResources(this.StaticObjectsFiltersLabel, "StaticObjectsFiltersLabel");
			this.StaticObjectsFiltersLabel.Name = "StaticObjectsFiltersLabel";
			resources.ApplyResources(this.AddStaticObjectButton, "AddStaticObjectButton");
			this.AddStaticObjectButton.Name = "AddStaticObjectButton";
			this.AddStaticObjectButton.UseVisualStyleBackColor = true;
			this.AddStaticObjectButton.Click += new global::System.EventHandler(this.AddStaticObjectButton_Click);
			resources.ApplyResources(this.StaticObjectsListView, "StaticObjectsListView");
			this.StaticObjectsListView.HideSelection = false;
			this.StaticObjectsListView.Name = "StaticObjectsListView";
			this.StaticObjectsListView.ShowItemToolTips = true;
			this.StaticObjectsListView.Sorting = global::System.Windows.Forms.SortOrder.Ascending;
			this.StaticObjectsListView.UseCompatibleStateImageBehavior = false;
			this.StaticObjectsListView.View = global::System.Windows.Forms.View.List;
			this.StaticObjectsListView.DoubleClick += new global::System.EventHandler(this.StaticObjectsListView_DoubleClick);
			resources.ApplyResources(this.RemoveStaticObjectButton, "RemoveStaticObjectButton");
			this.RemoveStaticObjectButton.Name = "RemoveStaticObjectButton";
			this.RemoveStaticObjectButton.UseVisualStyleBackColor = true;
			this.RemoveStaticObjectButton.Click += new global::System.EventHandler(this.RemoveStaticObjectButton_Click);
			resources.ApplyResources(this.SaveButton, "SaveButton");
			this.SaveButton.Name = "SaveButton";
			this.SaveButton.UseVisualStyleBackColor = true;
			this.SaveButton.Click += new global::System.EventHandler(this.SaveButton_Click);
			resources.ApplyResources(this, "$this");
			base.AutoScaleMode = global::System.Windows.Forms.AutoScaleMode.Font;
			base.CancelButton = this.CloseButton;
			base.Controls.Add(this.SplitContainer);
			this.DoubleBuffered = true;
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.Name = "MultiObjectBrowserForm";
			base.ShowInTaskbar = false;
			base.Load += new global::System.EventHandler(this.MultiObjectBrowser_Load);
			base.FormClosing += new global::System.Windows.Forms.FormClosingEventHandler(this.MultiObjectBrowser_FormClosing);
			this.SplitContainer.Panel1.ResumeLayout(false);
			this.SplitContainer.Panel1.PerformLayout();
			this.SplitContainer.Panel2.ResumeLayout(false);
			this.SplitContainer.ResumeLayout(false);
			this.TabControl.ResumeLayout(false);
			this.MultiObjectsTabPage.ResumeLayout(false);
			this.MultiObjectsTabPage.PerformLayout();
			this.StaticObjectsTabPage.ResumeLayout(false);
			this.StaticObjectsTabPage.PerformLayout();
			base.ResumeLayout(false);
		}

		// Token: 0x040003FC RID: 1020
		private global::System.ComponentModel.IContainer components;

		// Token: 0x040003FD RID: 1021
		private global::System.Windows.Forms.ListView StaticObjectsListView;

		// Token: 0x040003FE RID: 1022
		private global::System.Windows.Forms.Button AddStaticObjectButton;

		// Token: 0x040003FF RID: 1023
		private global::System.Windows.Forms.Button RemoveStaticObjectButton;

		// Token: 0x04000400 RID: 1024
		private global::System.Windows.Forms.Button AutoCompleteButton;

		// Token: 0x04000401 RID: 1025
		private global::System.Windows.Forms.Button AutoCompleteWithEmptyButton;

		// Token: 0x04000402 RID: 1026
		private global::System.Windows.Forms.Button ResetButton;

		// Token: 0x04000403 RID: 1027
		private global::System.Windows.Forms.SplitContainer SplitContainer;

		// Token: 0x04000404 RID: 1028
		private global::System.Windows.Forms.Label objectsLabel;

		// Token: 0x04000405 RID: 1029
		private global::System.Windows.Forms.TreeView ObjectsTreeView;

		// Token: 0x04000406 RID: 1030
		private global::System.Windows.Forms.Button CloseButton;

		// Token: 0x04000407 RID: 1031
		private global::System.Windows.Forms.TabControl TabControl;

		// Token: 0x04000408 RID: 1032
		private global::System.Windows.Forms.TabPage StaticObjectsTabPage;

		// Token: 0x04000409 RID: 1033
		private global::System.Windows.Forms.Label StaticObjectsFiltersLabel;

		// Token: 0x0400040A RID: 1034
		private global::System.Windows.Forms.TabPage MultiObjectsTabPage;

		// Token: 0x0400040B RID: 1035
		private global::System.Windows.Forms.ComboBox MultiObjectsFiltersComboBox;

		// Token: 0x0400040C RID: 1036
		private global::System.Windows.Forms.Label MultiObjectsFiltersLabel;

		// Token: 0x0400040D RID: 1037
		private global::System.Windows.Forms.Button MultiObjectsFiltersButton;

		// Token: 0x0400040E RID: 1038
		private global::System.Windows.Forms.Button DeleteButton;

		// Token: 0x0400040F RID: 1039
		private global::System.Windows.Forms.Button LoadButton;

		// Token: 0x04000410 RID: 1040
		private global::System.Windows.Forms.ListView MultiObjectsListView;

		// Token: 0x04000411 RID: 1041
		private global::System.Windows.Forms.Button SaveButton;

		// Token: 0x04000412 RID: 1042
		private global::System.Windows.Forms.Label MultiObjectNameLabel;

		// Token: 0x04000413 RID: 1043
		private global::System.Windows.Forms.Button MultiObjectNameButton;

		// Token: 0x04000414 RID: 1044
		private global::System.Windows.Forms.TextBox MultiObjectNameTextBox;

		// Token: 0x04000415 RID: 1045
		private global::System.Windows.Forms.Button StaticObjectsFiltersButton;

		// Token: 0x04000416 RID: 1046
		private global::System.Windows.Forms.ComboBox StaticObjectsFiltersComboBox;
	}
}
