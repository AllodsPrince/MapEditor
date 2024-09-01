namespace MapEditor.Forms.ObjectsBrowser
{
	// Token: 0x0200004B RID: 75
	public partial class ObjectsBrowserForm : global::System.Windows.Forms.Form
	{
		// Token: 0x060003FA RID: 1018 RVA: 0x00020E6B File Offset: 0x0001FE6B
		protected override void Dispose(bool disposing)
		{
			if (disposing && this.components != null)
			{
				this.components.Dispose();
			}
			base.Dispose(disposing);
		}

		// Token: 0x060003FB RID: 1019 RVA: 0x00020E8C File Offset: 0x0001FE8C
		private void InitializeComponent()
		{
			global::System.ComponentModel.ComponentResourceManager resources = new global::System.ComponentModel.ComponentResourceManager(typeof(global::MapEditor.Forms.ObjectsBrowser.ObjectsBrowserForm));
			this.SplitContainer = new global::System.Windows.Forms.SplitContainer();
			this.ObjectsListView = new global::System.Windows.Forms.ListView();
			this.PropertyControl = new global::System.Windows.Forms.PropertyControl();
			this.cancelButton = new global::System.Windows.Forms.Button();
			this.okButton = new global::System.Windows.Forms.Button();
			this.CurrentObjectTextBox = new global::System.Windows.Forms.TextBox();
			this.CurrentObjectLabel = new global::System.Windows.Forms.Label();
			this.ObjectFiltersEditorButton = new global::System.Windows.Forms.Button();
			this.ObjectFiltersComboBox = new global::System.Windows.Forms.ComboBox();
			this.ObjectFiltersLabel = new global::System.Windows.Forms.Label();
			this.AddObjectButton = new global::System.Windows.Forms.Button();
			this.ClearButton = new global::System.Windows.Forms.Button();
			this.SplitContainer.Panel1.SuspendLayout();
			this.SplitContainer.Panel2.SuspendLayout();
			this.SplitContainer.SuspendLayout();
			base.SuspendLayout();
			this.SplitContainer.AccessibleDescription = null;
			this.SplitContainer.AccessibleName = null;
			resources.ApplyResources(this.SplitContainer, "SplitContainer");
			this.SplitContainer.BackgroundImage = null;
			this.SplitContainer.Font = null;
			this.SplitContainer.Name = "SplitContainer";
			this.SplitContainer.Panel1.AccessibleDescription = null;
			this.SplitContainer.Panel1.AccessibleName = null;
			resources.ApplyResources(this.SplitContainer.Panel1, "SplitContainer.Panel1");
			this.SplitContainer.Panel1.BackgroundImage = null;
			this.SplitContainer.Panel1.Controls.Add(this.ObjectsListView);
			this.SplitContainer.Panel1.Font = null;
			this.SplitContainer.Panel2.AccessibleDescription = null;
			this.SplitContainer.Panel2.AccessibleName = null;
			resources.ApplyResources(this.SplitContainer.Panel2, "SplitContainer.Panel2");
			this.SplitContainer.Panel2.BackgroundImage = null;
			this.SplitContainer.Panel2.Controls.Add(this.PropertyControl);
			this.SplitContainer.Panel2.Font = null;
			this.ObjectsListView.AccessibleDescription = null;
			this.ObjectsListView.AccessibleName = null;
			resources.ApplyResources(this.ObjectsListView, "ObjectsListView");
			this.ObjectsListView.BackgroundImage = null;
			this.ObjectsListView.Font = null;
			this.ObjectsListView.HideSelection = false;
			this.ObjectsListView.Name = "ObjectsListView";
			this.ObjectsListView.ShowItemToolTips = true;
			this.ObjectsListView.Sorting = global::System.Windows.Forms.SortOrder.Ascending;
			this.ObjectsListView.UseCompatibleStateImageBehavior = false;
			this.ObjectsListView.View = global::System.Windows.Forms.View.List;
			this.ObjectsListView.MouseDoubleClick += new global::System.Windows.Forms.MouseEventHandler(this.ObjectsListView_MouseDoubleClick);
			this.PropertyControl.AccessibleDescription = null;
			this.PropertyControl.AccessibleName = null;
			resources.ApplyResources(this.PropertyControl, "PropertyControl");
			this.PropertyControl.BackgroundImage = null;
			this.PropertyControl.DefaultLocationFolder = "";
			this.PropertyControl.Font = null;
			this.PropertyControl.Name = "PropertyControl";
			this.PropertyControl.SelectedObject = null;
			this.PropertyControl.SelectedObjects = new object[0];
			this.PropertyControl.SkipRefresh = false;
			this.PropertyControl.TitleControl = null;
			this.PropertyControl.TitleRelativeFrom = null;
			this.PropertyControl.TitleStart = "";
			this.cancelButton.AccessibleDescription = null;
			this.cancelButton.AccessibleName = null;
			resources.ApplyResources(this.cancelButton, "cancelButton");
			this.cancelButton.BackgroundImage = null;
			this.cancelButton.DialogResult = global::System.Windows.Forms.DialogResult.Cancel;
			this.cancelButton.Font = null;
			this.cancelButton.Name = "cancelButton";
			this.cancelButton.UseVisualStyleBackColor = true;
			this.okButton.AccessibleDescription = null;
			this.okButton.AccessibleName = null;
			resources.ApplyResources(this.okButton, "okButton");
			this.okButton.BackgroundImage = null;
			this.okButton.DialogResult = global::System.Windows.Forms.DialogResult.OK;
			this.okButton.Font = null;
			this.okButton.Name = "okButton";
			this.okButton.UseVisualStyleBackColor = true;
			this.CurrentObjectTextBox.AccessibleDescription = null;
			this.CurrentObjectTextBox.AccessibleName = null;
			resources.ApplyResources(this.CurrentObjectTextBox, "CurrentObjectTextBox");
			this.CurrentObjectTextBox.BackgroundImage = null;
			this.CurrentObjectTextBox.Font = null;
			this.CurrentObjectTextBox.Name = "CurrentObjectTextBox";
			this.CurrentObjectTextBox.TextChanged += new global::System.EventHandler(this.CurrentObjectTextBox_TextChanged);
			this.CurrentObjectLabel.AccessibleDescription = null;
			this.CurrentObjectLabel.AccessibleName = null;
			resources.ApplyResources(this.CurrentObjectLabel, "CurrentObjectLabel");
			this.CurrentObjectLabel.Font = null;
			this.CurrentObjectLabel.Name = "CurrentObjectLabel";
			this.ObjectFiltersEditorButton.AccessibleDescription = null;
			this.ObjectFiltersEditorButton.AccessibleName = null;
			resources.ApplyResources(this.ObjectFiltersEditorButton, "ObjectFiltersEditorButton");
			this.ObjectFiltersEditorButton.BackgroundImage = null;
			this.ObjectFiltersEditorButton.Font = null;
			this.ObjectFiltersEditorButton.Name = "ObjectFiltersEditorButton";
			this.ObjectFiltersEditorButton.UseVisualStyleBackColor = true;
			this.ObjectFiltersEditorButton.Click += new global::System.EventHandler(this.ObjectFiltersEditorButton_Click);
			this.ObjectFiltersComboBox.AccessibleDescription = null;
			this.ObjectFiltersComboBox.AccessibleName = null;
			resources.ApplyResources(this.ObjectFiltersComboBox, "ObjectFiltersComboBox");
			this.ObjectFiltersComboBox.BackgroundImage = null;
			this.ObjectFiltersComboBox.DropDownStyle = global::System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.ObjectFiltersComboBox.Font = null;
			this.ObjectFiltersComboBox.FormattingEnabled = true;
			this.ObjectFiltersComboBox.Name = "ObjectFiltersComboBox";
			this.ObjectFiltersLabel.AccessibleDescription = null;
			this.ObjectFiltersLabel.AccessibleName = null;
			resources.ApplyResources(this.ObjectFiltersLabel, "ObjectFiltersLabel");
			this.ObjectFiltersLabel.Font = null;
			this.ObjectFiltersLabel.Name = "ObjectFiltersLabel";
			this.AddObjectButton.AccessibleDescription = null;
			this.AddObjectButton.AccessibleName = null;
			resources.ApplyResources(this.AddObjectButton, "AddObjectButton");
			this.AddObjectButton.BackgroundImage = null;
			this.AddObjectButton.Font = null;
			this.AddObjectButton.Name = "AddObjectButton";
			this.AddObjectButton.UseVisualStyleBackColor = true;
			this.AddObjectButton.Click += new global::System.EventHandler(this.AddObjectButton_Click);
			this.ClearButton.AccessibleDescription = null;
			this.ClearButton.AccessibleName = null;
			resources.ApplyResources(this.ClearButton, "ClearButton");
			this.ClearButton.BackgroundImage = null;
			this.ClearButton.Font = null;
			this.ClearButton.Name = "ClearButton";
			this.ClearButton.UseVisualStyleBackColor = true;
			this.ClearButton.Click += new global::System.EventHandler(this.ClearButton_Click);
			base.AcceptButton = this.okButton;
			base.AccessibleDescription = null;
			base.AccessibleName = null;
			resources.ApplyResources(this, "$this");
			base.AutoScaleMode = global::System.Windows.Forms.AutoScaleMode.Font;
			this.BackgroundImage = null;
			base.CancelButton = this.cancelButton;
			base.Controls.Add(this.ClearButton);
			base.Controls.Add(this.SplitContainer);
			base.Controls.Add(this.AddObjectButton);
			base.Controls.Add(this.cancelButton);
			base.Controls.Add(this.okButton);
			base.Controls.Add(this.CurrentObjectTextBox);
			base.Controls.Add(this.CurrentObjectLabel);
			base.Controls.Add(this.ObjectFiltersEditorButton);
			base.Controls.Add(this.ObjectFiltersComboBox);
			base.Controls.Add(this.ObjectFiltersLabel);
			this.Font = null;
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.Name = "ObjectsBrowserForm";
			base.ShowInTaskbar = false;
			base.SizeGripStyle = global::System.Windows.Forms.SizeGripStyle.Show;
			base.Load += new global::System.EventHandler(this.ObjectsBrowserForm_Load);
			base.FormClosing += new global::System.Windows.Forms.FormClosingEventHandler(this.ObjectsBrowserForm_FormClosing);
			this.SplitContainer.Panel1.ResumeLayout(false);
			this.SplitContainer.Panel2.ResumeLayout(false);
			this.SplitContainer.ResumeLayout(false);
			base.ResumeLayout(false);
			base.PerformLayout();
		}

		// Token: 0x040002BA RID: 698
		private global::System.ComponentModel.IContainer components;

		// Token: 0x040002BB RID: 699
		private global::System.Windows.Forms.SplitContainer SplitContainer;

		// Token: 0x040002BC RID: 700
		private global::System.Windows.Forms.ListView ObjectsListView;

		// Token: 0x040002BD RID: 701
		private global::System.Windows.Forms.PropertyControl PropertyControl;

		// Token: 0x040002BE RID: 702
		private global::System.Windows.Forms.Button cancelButton;

		// Token: 0x040002BF RID: 703
		private global::System.Windows.Forms.Button okButton;

		// Token: 0x040002C0 RID: 704
		private global::System.Windows.Forms.TextBox CurrentObjectTextBox;

		// Token: 0x040002C1 RID: 705
		private global::System.Windows.Forms.Label CurrentObjectLabel;

		// Token: 0x040002C2 RID: 706
		private global::System.Windows.Forms.Button ObjectFiltersEditorButton;

		// Token: 0x040002C3 RID: 707
		private global::System.Windows.Forms.ComboBox ObjectFiltersComboBox;

		// Token: 0x040002C4 RID: 708
		private global::System.Windows.Forms.Label ObjectFiltersLabel;

		// Token: 0x040002C5 RID: 709
		private global::System.Windows.Forms.Button AddObjectButton;

		// Token: 0x040002C6 RID: 710
		private global::System.Windows.Forms.Button ClearButton;
	}
}
