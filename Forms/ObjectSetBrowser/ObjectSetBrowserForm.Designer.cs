namespace MapEditor.Forms.ObjectSetBrowser
{
	// Token: 0x02000111 RID: 273
	public partial class ObjectSetBrowserForm : global::MapEditor.Forms.Base.BaseForm
	{
		// Token: 0x06000D77 RID: 3447 RVA: 0x00070463 File Offset: 0x0006F463
		protected override void Dispose(bool disposing)
		{
			if (disposing && this.components != null)
			{
				this.components.Dispose();
			}
			base.Dispose(disposing);
		}

		// Token: 0x06000D78 RID: 3448 RVA: 0x00070484 File Offset: 0x0006F484
		private void InitializeComponent()
		{
			global::System.ComponentModel.ComponentResourceManager resources = new global::System.ComponentModel.ComponentResourceManager(typeof(global::MapEditor.Forms.ObjectSetBrowser.ObjectSetBrowserForm));
			this.SplitContainer = new global::System.Windows.Forms.SplitContainer();
			this.WeightComboBox = new global::System.Windows.Forms.ComboBox();
			this.WeightButton = new global::System.Windows.Forms.Button();
			this.ObjectListView = new global::System.Windows.Forms.ListView();
			this.label1 = new global::System.Windows.Forms.Label();
			this.objectsLabel = new global::System.Windows.Forms.Label();
			this.CloseButton = new global::System.Windows.Forms.Button();
			this.TabControl = new global::System.Windows.Forms.TabControl();
			this.ObjectSetsTabPage = new global::System.Windows.Forms.TabPage();
			this.ObjectSetFiltersComboBox = new global::System.Windows.Forms.ComboBox();
			this.ObjectSetsFiltersLabel = new global::System.Windows.Forms.Label();
			this.ObjectSetsFiltersButton = new global::System.Windows.Forms.Button();
			this.DeleteButton = new global::System.Windows.Forms.Button();
			this.LoadButton = new global::System.Windows.Forms.Button();
			this.ObjectSetListView = new global::System.Windows.Forms.ListView();
			this.ObjectSetNameLabel = new global::System.Windows.Forms.Label();
			this.ObjectSetNameButton = new global::System.Windows.Forms.Button();
			this.ObjectSetNameTextBox = new global::System.Windows.Forms.TextBox();
			this.StaticObjectsTabPage = new global::System.Windows.Forms.TabPage();
			this.StaticObjectFiltersComboBox = new global::System.Windows.Forms.ComboBox();
			this.StaticObjectFiltersButton = new global::System.Windows.Forms.Button();
			this.StaticObjectsFiltersLabel = new global::System.Windows.Forms.Label();
			this.AddStaticObjectButton = new global::System.Windows.Forms.Button();
			this.StaticObjectListView = new global::System.Windows.Forms.ListView();
			this.RemoveStaticObjectButton = new global::System.Windows.Forms.Button();
			this.AutoCompleteButton = new global::System.Windows.Forms.Button();
			this.SaveButton = new global::System.Windows.Forms.Button();
			this.ResetButton = new global::System.Windows.Forms.Button();
			this.SplitContainer.Panel1.SuspendLayout();
			this.SplitContainer.Panel2.SuspendLayout();
			this.SplitContainer.SuspendLayout();
			this.TabControl.SuspendLayout();
			this.ObjectSetsTabPage.SuspendLayout();
			this.StaticObjectsTabPage.SuspendLayout();
			base.SuspendLayout();
			resources.ApplyResources(this.SplitContainer, "SplitContainer");
			this.SplitContainer.Name = "SplitContainer";
			this.SplitContainer.Panel1.Controls.Add(this.WeightComboBox);
			this.SplitContainer.Panel1.Controls.Add(this.WeightButton);
			this.SplitContainer.Panel1.Controls.Add(this.ObjectListView);
			this.SplitContainer.Panel1.Controls.Add(this.label1);
			this.SplitContainer.Panel1.Controls.Add(this.objectsLabel);
			this.SplitContainer.Panel2.Controls.Add(this.CloseButton);
			this.SplitContainer.Panel2.Controls.Add(this.TabControl);
			this.SplitContainer.Panel2.Controls.Add(this.AutoCompleteButton);
			this.SplitContainer.Panel2.Controls.Add(this.SaveButton);
			this.SplitContainer.Panel2.Controls.Add(this.ResetButton);
			resources.ApplyResources(this.WeightComboBox, "WeightComboBox");
			this.WeightComboBox.FormattingEnabled = true;
			this.WeightComboBox.Name = "WeightComboBox";
			this.WeightComboBox.Leave += new global::System.EventHandler(this.WeightComboBox_Leave);
			resources.ApplyResources(this.WeightButton, "WeightButton");
			this.WeightButton.Name = "WeightButton";
			this.WeightButton.UseVisualStyleBackColor = true;
			this.WeightButton.Click += new global::System.EventHandler(this.WeightButton_Click);
			resources.ApplyResources(this.ObjectListView, "ObjectListView");
			this.ObjectListView.HideSelection = false;
			this.ObjectListView.Name = "ObjectListView";
			this.ObjectListView.ShowItemToolTips = true;
			this.ObjectListView.Sorting = global::System.Windows.Forms.SortOrder.Ascending;
			this.ObjectListView.UseCompatibleStateImageBehavior = false;
			this.ObjectListView.View = global::System.Windows.Forms.View.List;
			this.ObjectListView.SelectedIndexChanged += new global::System.EventHandler(this.ObjectListView_SelectedIndexChanged);
			this.ObjectListView.DoubleClick += new global::System.EventHandler(this.ObjectListView_DoubleClick);
			resources.ApplyResources(this.label1, "label1");
			this.label1.Name = "label1";
			resources.ApplyResources(this.objectsLabel, "objectsLabel");
			this.objectsLabel.Name = "objectsLabel";
			resources.ApplyResources(this.CloseButton, "CloseButton");
			this.CloseButton.DialogResult = global::System.Windows.Forms.DialogResult.Cancel;
			this.CloseButton.Name = "CloseButton";
			this.CloseButton.UseVisualStyleBackColor = true;
			this.CloseButton.Click += new global::System.EventHandler(this.CloseButton_Click);
			resources.ApplyResources(this.TabControl, "TabControl");
			this.TabControl.Controls.Add(this.ObjectSetsTabPage);
			this.TabControl.Controls.Add(this.StaticObjectsTabPage);
			this.TabControl.Name = "TabControl";
			this.TabControl.SelectedIndex = 0;
			this.ObjectSetsTabPage.Controls.Add(this.ObjectSetFiltersComboBox);
			this.ObjectSetsTabPage.Controls.Add(this.ObjectSetsFiltersLabel);
			this.ObjectSetsTabPage.Controls.Add(this.ObjectSetsFiltersButton);
			this.ObjectSetsTabPage.Controls.Add(this.DeleteButton);
			this.ObjectSetsTabPage.Controls.Add(this.LoadButton);
			this.ObjectSetsTabPage.Controls.Add(this.ObjectSetListView);
			this.ObjectSetsTabPage.Controls.Add(this.ObjectSetNameLabel);
			this.ObjectSetsTabPage.Controls.Add(this.ObjectSetNameButton);
			this.ObjectSetsTabPage.Controls.Add(this.ObjectSetNameTextBox);
			resources.ApplyResources(this.ObjectSetsTabPage, "ObjectSetsTabPage");
			this.ObjectSetsTabPage.Name = "ObjectSetsTabPage";
			this.ObjectSetsTabPage.UseVisualStyleBackColor = true;
			resources.ApplyResources(this.ObjectSetFiltersComboBox, "ObjectSetFiltersComboBox");
			this.ObjectSetFiltersComboBox.FormattingEnabled = true;
			this.ObjectSetFiltersComboBox.Name = "ObjectSetFiltersComboBox";
			resources.ApplyResources(this.ObjectSetsFiltersLabel, "ObjectSetsFiltersLabel");
			this.ObjectSetsFiltersLabel.Name = "ObjectSetsFiltersLabel";
			resources.ApplyResources(this.ObjectSetsFiltersButton, "ObjectSetsFiltersButton");
			this.ObjectSetsFiltersButton.Name = "ObjectSetsFiltersButton";
			this.ObjectSetsFiltersButton.UseVisualStyleBackColor = true;
			this.ObjectSetsFiltersButton.Click += new global::System.EventHandler(this.ObjectSetsFiltersButton_Click);
			resources.ApplyResources(this.DeleteButton, "DeleteButton");
			this.DeleteButton.Name = "DeleteButton";
			this.DeleteButton.UseVisualStyleBackColor = true;
			this.DeleteButton.Click += new global::System.EventHandler(this.DeleteButton_Click);
			resources.ApplyResources(this.LoadButton, "LoadButton");
			this.LoadButton.Name = "LoadButton";
			this.LoadButton.UseVisualStyleBackColor = true;
			this.LoadButton.Click += new global::System.EventHandler(this.LoadButton_Click);
			resources.ApplyResources(this.ObjectSetListView, "ObjectSetListView");
			this.ObjectSetListView.HideSelection = false;
			this.ObjectSetListView.Name = "ObjectSetListView";
			this.ObjectSetListView.ShowItemToolTips = true;
			this.ObjectSetListView.Sorting = global::System.Windows.Forms.SortOrder.Ascending;
			this.ObjectSetListView.UseCompatibleStateImageBehavior = false;
			this.ObjectSetListView.View = global::System.Windows.Forms.View.List;
			this.ObjectSetListView.DoubleClick += new global::System.EventHandler(this.ObjectSetListView_DoubleClick);
			resources.ApplyResources(this.ObjectSetNameLabel, "ObjectSetNameLabel");
			this.ObjectSetNameLabel.Name = "ObjectSetNameLabel";
			resources.ApplyResources(this.ObjectSetNameButton, "ObjectSetNameButton");
			this.ObjectSetNameButton.Name = "ObjectSetNameButton";
			this.ObjectSetNameButton.UseVisualStyleBackColor = true;
			this.ObjectSetNameButton.Click += new global::System.EventHandler(this.ObjectSetNameButton_Click);
			resources.ApplyResources(this.ObjectSetNameTextBox, "ObjectSetNameTextBox");
			this.ObjectSetNameTextBox.Name = "ObjectSetNameTextBox";
			this.ObjectSetNameTextBox.TextChanged += new global::System.EventHandler(this.ObjectSetNameTextBox_TextChanged);
			this.StaticObjectsTabPage.Controls.Add(this.StaticObjectFiltersComboBox);
			this.StaticObjectsTabPage.Controls.Add(this.StaticObjectFiltersButton);
			this.StaticObjectsTabPage.Controls.Add(this.StaticObjectsFiltersLabel);
			this.StaticObjectsTabPage.Controls.Add(this.AddStaticObjectButton);
			this.StaticObjectsTabPage.Controls.Add(this.StaticObjectListView);
			this.StaticObjectsTabPage.Controls.Add(this.RemoveStaticObjectButton);
			resources.ApplyResources(this.StaticObjectsTabPage, "StaticObjectsTabPage");
			this.StaticObjectsTabPage.Name = "StaticObjectsTabPage";
			this.StaticObjectsTabPage.UseVisualStyleBackColor = true;
			resources.ApplyResources(this.StaticObjectFiltersComboBox, "StaticObjectFiltersComboBox");
			this.StaticObjectFiltersComboBox.FormattingEnabled = true;
			this.StaticObjectFiltersComboBox.Name = "StaticObjectFiltersComboBox";
			resources.ApplyResources(this.StaticObjectFiltersButton, "StaticObjectFiltersButton");
			this.StaticObjectFiltersButton.Name = "StaticObjectFiltersButton";
			this.StaticObjectFiltersButton.UseVisualStyleBackColor = true;
			this.StaticObjectFiltersButton.Click += new global::System.EventHandler(this.StaticObjectFiltersButton_Click);
			resources.ApplyResources(this.StaticObjectsFiltersLabel, "StaticObjectsFiltersLabel");
			this.StaticObjectsFiltersLabel.Name = "StaticObjectsFiltersLabel";
			resources.ApplyResources(this.AddStaticObjectButton, "AddStaticObjectButton");
			this.AddStaticObjectButton.Name = "AddStaticObjectButton";
			this.AddStaticObjectButton.UseVisualStyleBackColor = true;
			this.AddStaticObjectButton.Click += new global::System.EventHandler(this.AddStaticObjectButton_Click);
			resources.ApplyResources(this.StaticObjectListView, "StaticObjectListView");
			this.StaticObjectListView.HideSelection = false;
			this.StaticObjectListView.Name = "StaticObjectListView";
			this.StaticObjectListView.ShowItemToolTips = true;
			this.StaticObjectListView.Sorting = global::System.Windows.Forms.SortOrder.Ascending;
			this.StaticObjectListView.UseCompatibleStateImageBehavior = false;
			this.StaticObjectListView.View = global::System.Windows.Forms.View.List;
			this.StaticObjectListView.DoubleClick += new global::System.EventHandler(this.StaticObjectListView_DoubleClick);
			resources.ApplyResources(this.RemoveStaticObjectButton, "RemoveStaticObjectButton");
			this.RemoveStaticObjectButton.Name = "RemoveStaticObjectButton";
			this.RemoveStaticObjectButton.UseVisualStyleBackColor = true;
			this.RemoveStaticObjectButton.Click += new global::System.EventHandler(this.RemoveStaticObjectButton_Click);
			resources.ApplyResources(this.AutoCompleteButton, "AutoCompleteButton");
			this.AutoCompleteButton.Name = "AutoCompleteButton";
			this.AutoCompleteButton.UseVisualStyleBackColor = true;
			this.AutoCompleteButton.Click += new global::System.EventHandler(this.AutoCompleteButton_Click);
			resources.ApplyResources(this.SaveButton, "SaveButton");
			this.SaveButton.Name = "SaveButton";
			this.SaveButton.UseVisualStyleBackColor = true;
			this.SaveButton.Click += new global::System.EventHandler(this.SaveButton_Click);
			resources.ApplyResources(this.ResetButton, "ResetButton");
			this.ResetButton.Name = "ResetButton";
			this.ResetButton.UseVisualStyleBackColor = true;
			this.ResetButton.Click += new global::System.EventHandler(this.ResetButton_Click);
			base.AcceptButton = this.LoadButton;
			resources.ApplyResources(this, "$this");
			base.AutoScaleMode = global::System.Windows.Forms.AutoScaleMode.Font;
			base.CancelButton = this.CloseButton;
			base.Controls.Add(this.SplitContainer);
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.Name = "ObjectSetBrowserForm";
			base.ShowInTaskbar = false;
			base.Load += new global::System.EventHandler(this.ObjectSetBrowserForm_Load);
			base.FormClosing += new global::System.Windows.Forms.FormClosingEventHandler(this.ObjectSetBrowserForm_FormClosing);
			this.SplitContainer.Panel1.ResumeLayout(false);
			this.SplitContainer.Panel1.PerformLayout();
			this.SplitContainer.Panel2.ResumeLayout(false);
			this.SplitContainer.ResumeLayout(false);
			this.TabControl.ResumeLayout(false);
			this.ObjectSetsTabPage.ResumeLayout(false);
			this.ObjectSetsTabPage.PerformLayout();
			this.StaticObjectsTabPage.ResumeLayout(false);
			this.StaticObjectsTabPage.PerformLayout();
			base.ResumeLayout(false);
		}

		// Token: 0x04000AB2 RID: 2738
		private global::System.ComponentModel.IContainer components;

		// Token: 0x04000AB3 RID: 2739
		private global::System.Windows.Forms.Button CloseButton;

		// Token: 0x04000AB4 RID: 2740
		private global::System.Windows.Forms.SplitContainer SplitContainer;

		// Token: 0x04000AB5 RID: 2741
		private global::System.Windows.Forms.Label objectsLabel;

		// Token: 0x04000AB6 RID: 2742
		private global::System.Windows.Forms.TabControl TabControl;

		// Token: 0x04000AB7 RID: 2743
		private global::System.Windows.Forms.TabPage ObjectSetsTabPage;

		// Token: 0x04000AB8 RID: 2744
		private global::System.Windows.Forms.ComboBox ObjectSetFiltersComboBox;

		// Token: 0x04000AB9 RID: 2745
		private global::System.Windows.Forms.Label ObjectSetsFiltersLabel;

		// Token: 0x04000ABA RID: 2746
		private global::System.Windows.Forms.Button ObjectSetsFiltersButton;

		// Token: 0x04000ABB RID: 2747
		private global::System.Windows.Forms.Button DeleteButton;

		// Token: 0x04000ABC RID: 2748
		private global::System.Windows.Forms.Button LoadButton;

		// Token: 0x04000ABD RID: 2749
		private global::System.Windows.Forms.ListView ObjectSetListView;

		// Token: 0x04000ABE RID: 2750
		private global::System.Windows.Forms.Button SaveButton;

		// Token: 0x04000ABF RID: 2751
		private global::System.Windows.Forms.Label ObjectSetNameLabel;

		// Token: 0x04000AC0 RID: 2752
		private global::System.Windows.Forms.Button ObjectSetNameButton;

		// Token: 0x04000AC1 RID: 2753
		private global::System.Windows.Forms.TextBox ObjectSetNameTextBox;

		// Token: 0x04000AC2 RID: 2754
		private global::System.Windows.Forms.TabPage StaticObjectsTabPage;

		// Token: 0x04000AC3 RID: 2755
		private global::System.Windows.Forms.ComboBox StaticObjectFiltersComboBox;

		// Token: 0x04000AC4 RID: 2756
		private global::System.Windows.Forms.Button StaticObjectFiltersButton;

		// Token: 0x04000AC5 RID: 2757
		private global::System.Windows.Forms.Label StaticObjectsFiltersLabel;

		// Token: 0x04000AC6 RID: 2758
		private global::System.Windows.Forms.Button AddStaticObjectButton;

		// Token: 0x04000AC7 RID: 2759
		private global::System.Windows.Forms.Button ResetButton;

		// Token: 0x04000AC8 RID: 2760
		private global::System.Windows.Forms.ListView StaticObjectListView;

		// Token: 0x04000AC9 RID: 2761
		private global::System.Windows.Forms.Button AutoCompleteButton;

		// Token: 0x04000ACA RID: 2762
		private global::System.Windows.Forms.Button RemoveStaticObjectButton;

		// Token: 0x04000ACB RID: 2763
		private global::System.Windows.Forms.ListView ObjectListView;

		// Token: 0x04000ACC RID: 2764
		private global::System.Windows.Forms.Label label1;

		// Token: 0x04000ACD RID: 2765
		private global::System.Windows.Forms.Button WeightButton;

		// Token: 0x04000ACE RID: 2766
		private global::System.Windows.Forms.ComboBox WeightComboBox;
	}
}
