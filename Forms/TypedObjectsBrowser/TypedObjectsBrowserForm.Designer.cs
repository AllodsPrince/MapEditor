namespace MapEditor.Forms.TypedObjectsBrowser
{
	// Token: 0x02000078 RID: 120
	public partial class TypedObjectsBrowserForm : global::System.Windows.Forms.Form
	{
		// Token: 0x060005E8 RID: 1512 RVA: 0x00031A18 File Offset: 0x00030A18
		protected override void Dispose(bool disposing)
		{
			if (disposing && this.components != null)
			{
				this.components.Dispose();
			}
			base.Dispose(disposing);
		}

		// Token: 0x060005E9 RID: 1513 RVA: 0x00031A38 File Offset: 0x00030A38
		private void InitializeComponent()
		{
			global::System.ComponentModel.ComponentResourceManager resources = new global::System.ComponentModel.ComponentResourceManager(typeof(global::MapEditor.Forms.TypedObjectsBrowser.TypedObjectsBrowserForm));
			this.SplitContainer = new global::System.Windows.Forms.SplitContainer();
			this.ObjectTypeGroupBox = new global::System.Windows.Forms.GroupBox();
			this.ObjectType3RadioButton = new global::System.Windows.Forms.RadioButton();
			this.ObjectType2RadioButton = new global::System.Windows.Forms.RadioButton();
			this.ObjectType1RadioButton = new global::System.Windows.Forms.RadioButton();
			this.ObjectType0RadioButton = new global::System.Windows.Forms.RadioButton();
			this.TypedObjectsListView = new global::System.Windows.Forms.ListView();
			this.PropertyControl = new global::System.Windows.Forms.PropertyControl();
			this.TypedObjectFiltersEditorButton = new global::System.Windows.Forms.Button();
			this.TypedObjectFiltersComboBox = new global::System.Windows.Forms.ComboBox();
			this.TypedObjectFiltersLabel = new global::System.Windows.Forms.Label();
			this.CurrentTypedObjectLabel = new global::System.Windows.Forms.Label();
			this.CurrentTypedObjectTextBox = new global::System.Windows.Forms.TextBox();
			this.okButton = new global::System.Windows.Forms.Button();
			this.cancelButton = new global::System.Windows.Forms.Button();
			this.AddTypedObjectButton = new global::System.Windows.Forms.Button();
			this.TypeGroupBox = new global::System.Windows.Forms.GroupBox();
			this.Type4RadioButton = new global::System.Windows.Forms.RadioButton();
			this.Type2RadioButton = new global::System.Windows.Forms.RadioButton();
			this.Type3RadioButton = new global::System.Windows.Forms.RadioButton();
			this.Type0RadioButton = new global::System.Windows.Forms.RadioButton();
			this.Type1RadioButton = new global::System.Windows.Forms.RadioButton();
			this.ClearButton = new global::System.Windows.Forms.Button();
			this.SplitContainer.Panel1.SuspendLayout();
			this.SplitContainer.Panel2.SuspendLayout();
			this.SplitContainer.SuspendLayout();
			this.ObjectTypeGroupBox.SuspendLayout();
			this.TypeGroupBox.SuspendLayout();
			base.SuspendLayout();
			resources.ApplyResources(this.SplitContainer, "SplitContainer");
			this.SplitContainer.Name = "SplitContainer";
			this.SplitContainer.Panel1.Controls.Add(this.ObjectTypeGroupBox);
			this.SplitContainer.Panel1.Controls.Add(this.TypedObjectsListView);
			this.SplitContainer.Panel2.Controls.Add(this.PropertyControl);
			resources.ApplyResources(this.ObjectTypeGroupBox, "ObjectTypeGroupBox");
			this.ObjectTypeGroupBox.Controls.Add(this.ObjectType3RadioButton);
			this.ObjectTypeGroupBox.Controls.Add(this.ObjectType2RadioButton);
			this.ObjectTypeGroupBox.Controls.Add(this.ObjectType1RadioButton);
			this.ObjectTypeGroupBox.Controls.Add(this.ObjectType0RadioButton);
			this.ObjectTypeGroupBox.Name = "ObjectTypeGroupBox";
			this.ObjectTypeGroupBox.TabStop = false;
			resources.ApplyResources(this.ObjectType3RadioButton, "ObjectType3RadioButton");
			this.ObjectType3RadioButton.Name = "ObjectType3RadioButton";
			this.ObjectType3RadioButton.TabStop = true;
			this.ObjectType3RadioButton.UseVisualStyleBackColor = true;
			this.ObjectType3RadioButton.CheckedChanged += new global::System.EventHandler(this.ObjectType3RadioButton_CheckedChanged);
			resources.ApplyResources(this.ObjectType2RadioButton, "ObjectType2RadioButton");
			this.ObjectType2RadioButton.Name = "ObjectType2RadioButton";
			this.ObjectType2RadioButton.TabStop = true;
			this.ObjectType2RadioButton.UseVisualStyleBackColor = true;
			this.ObjectType2RadioButton.CheckedChanged += new global::System.EventHandler(this.ObjectType2RadioButton_CheckedChanged);
			resources.ApplyResources(this.ObjectType1RadioButton, "ObjectType1RadioButton");
			this.ObjectType1RadioButton.Name = "ObjectType1RadioButton";
			this.ObjectType1RadioButton.TabStop = true;
			this.ObjectType1RadioButton.UseVisualStyleBackColor = true;
			this.ObjectType1RadioButton.CheckedChanged += new global::System.EventHandler(this.ObjectType1RadioButton_CheckedChanged);
			resources.ApplyResources(this.ObjectType0RadioButton, "ObjectType0RadioButton");
			this.ObjectType0RadioButton.Name = "ObjectType0RadioButton";
			this.ObjectType0RadioButton.TabStop = true;
			this.ObjectType0RadioButton.UseVisualStyleBackColor = true;
			this.ObjectType0RadioButton.CheckedChanged += new global::System.EventHandler(this.ObjectType0RadioButton_CheckedChanged);
			resources.ApplyResources(this.TypedObjectsListView, "TypedObjectsListView");
			this.TypedObjectsListView.HideSelection = false;
			this.TypedObjectsListView.Name = "TypedObjectsListView";
			this.TypedObjectsListView.ShowItemToolTips = true;
			this.TypedObjectsListView.Sorting = global::System.Windows.Forms.SortOrder.Ascending;
			this.TypedObjectsListView.UseCompatibleStateImageBehavior = false;
			this.TypedObjectsListView.View = global::System.Windows.Forms.View.List;
			this.TypedObjectsListView.MouseDoubleClick += new global::System.Windows.Forms.MouseEventHandler(this.TypedObjectsListView_MouseDoubleClick);
			this.PropertyControl.DefaultLocationFolder = "";
			resources.ApplyResources(this.PropertyControl, "PropertyControl");
			this.PropertyControl.Name = "PropertyControl";
			this.PropertyControl.SelectedObject = null;
			this.PropertyControl.SelectedObjects = new object[0];
			this.PropertyControl.SkipRefresh = false;
			this.PropertyControl.TitleControl = null;
			this.PropertyControl.TitleRelativeFrom = null;
			this.PropertyControl.TitleStart = "";
			resources.ApplyResources(this.TypedObjectFiltersEditorButton, "TypedObjectFiltersEditorButton");
			this.TypedObjectFiltersEditorButton.Name = "TypedObjectFiltersEditorButton";
			this.TypedObjectFiltersEditorButton.UseVisualStyleBackColor = true;
			this.TypedObjectFiltersEditorButton.Click += new global::System.EventHandler(this.TypedObjectFiltersEditorButton_Click);
			resources.ApplyResources(this.TypedObjectFiltersComboBox, "TypedObjectFiltersComboBox");
			this.TypedObjectFiltersComboBox.DropDownStyle = global::System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.TypedObjectFiltersComboBox.FormattingEnabled = true;
			this.TypedObjectFiltersComboBox.Name = "TypedObjectFiltersComboBox";
			resources.ApplyResources(this.TypedObjectFiltersLabel, "TypedObjectFiltersLabel");
			this.TypedObjectFiltersLabel.Name = "TypedObjectFiltersLabel";
			resources.ApplyResources(this.CurrentTypedObjectLabel, "CurrentTypedObjectLabel");
			this.CurrentTypedObjectLabel.Name = "CurrentTypedObjectLabel";
			resources.ApplyResources(this.CurrentTypedObjectTextBox, "CurrentTypedObjectTextBox");
			this.CurrentTypedObjectTextBox.Name = "CurrentTypedObjectTextBox";
			this.CurrentTypedObjectTextBox.TextChanged += new global::System.EventHandler(this.CurrentTypedObjectTextBox_TextChanged);
			resources.ApplyResources(this.okButton, "okButton");
			this.okButton.DialogResult = global::System.Windows.Forms.DialogResult.OK;
			this.okButton.Name = "okButton";
			this.okButton.UseVisualStyleBackColor = true;
			resources.ApplyResources(this.cancelButton, "cancelButton");
			this.cancelButton.DialogResult = global::System.Windows.Forms.DialogResult.Cancel;
			this.cancelButton.Name = "cancelButton";
			this.cancelButton.UseVisualStyleBackColor = true;
			resources.ApplyResources(this.AddTypedObjectButton, "AddTypedObjectButton");
			this.AddTypedObjectButton.Name = "AddTypedObjectButton";
			this.AddTypedObjectButton.UseVisualStyleBackColor = true;
			this.AddTypedObjectButton.Click += new global::System.EventHandler(this.AddTypedObjectButton_Click);
			resources.ApplyResources(this.TypeGroupBox, "TypeGroupBox");
			this.TypeGroupBox.Controls.Add(this.Type4RadioButton);
			this.TypeGroupBox.Controls.Add(this.Type2RadioButton);
			this.TypeGroupBox.Controls.Add(this.Type3RadioButton);
			this.TypeGroupBox.Controls.Add(this.Type0RadioButton);
			this.TypeGroupBox.Controls.Add(this.Type1RadioButton);
			this.TypeGroupBox.Name = "TypeGroupBox";
			this.TypeGroupBox.TabStop = false;
			resources.ApplyResources(this.Type4RadioButton, "Type4RadioButton");
			this.Type4RadioButton.Name = "Type4RadioButton";
			this.Type4RadioButton.TabStop = true;
			this.Type4RadioButton.UseVisualStyleBackColor = true;
			this.Type4RadioButton.CheckedChanged += new global::System.EventHandler(this.Type4RadioButton_CheckedChanged);
			resources.ApplyResources(this.Type2RadioButton, "Type2RadioButton");
			this.Type2RadioButton.Name = "Type2RadioButton";
			this.Type2RadioButton.TabStop = true;
			this.Type2RadioButton.UseVisualStyleBackColor = true;
			this.Type2RadioButton.CheckedChanged += new global::System.EventHandler(this.Type2RadioButton_CheckedChanged);
			resources.ApplyResources(this.Type3RadioButton, "Type3RadioButton");
			this.Type3RadioButton.Name = "Type3RadioButton";
			this.Type3RadioButton.TabStop = true;
			this.Type3RadioButton.UseVisualStyleBackColor = true;
			this.Type3RadioButton.CheckedChanged += new global::System.EventHandler(this.Type3RadioButton_CheckedChanged);
			resources.ApplyResources(this.Type0RadioButton, "Type0RadioButton");
			this.Type0RadioButton.Name = "Type0RadioButton";
			this.Type0RadioButton.TabStop = true;
			this.Type0RadioButton.UseVisualStyleBackColor = true;
			this.Type0RadioButton.CheckedChanged += new global::System.EventHandler(this.Type0RadioButton_CheckedChanged);
			resources.ApplyResources(this.Type1RadioButton, "Type1RadioButton");
			this.Type1RadioButton.Name = "Type1RadioButton";
			this.Type1RadioButton.TabStop = true;
			this.Type1RadioButton.UseVisualStyleBackColor = true;
			this.Type1RadioButton.CheckedChanged += new global::System.EventHandler(this.Type1RadioButton_CheckedChanged);
			resources.ApplyResources(this.ClearButton, "ClearButton");
			this.ClearButton.Name = "ClearButton";
			this.ClearButton.UseVisualStyleBackColor = true;
			this.ClearButton.Click += new global::System.EventHandler(this.ClearButton_Click);
			base.AcceptButton = this.okButton;
			resources.ApplyResources(this, "$this");
			base.AutoScaleMode = global::System.Windows.Forms.AutoScaleMode.Font;
			base.CancelButton = this.cancelButton;
			base.Controls.Add(this.ClearButton);
			base.Controls.Add(this.TypeGroupBox);
			base.Controls.Add(this.SplitContainer);
			base.Controls.Add(this.AddTypedObjectButton);
			base.Controls.Add(this.cancelButton);
			base.Controls.Add(this.okButton);
			base.Controls.Add(this.CurrentTypedObjectTextBox);
			base.Controls.Add(this.CurrentTypedObjectLabel);
			base.Controls.Add(this.TypedObjectFiltersEditorButton);
			base.Controls.Add(this.TypedObjectFiltersComboBox);
			base.Controls.Add(this.TypedObjectFiltersLabel);
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.Name = "TypedObjectsBrowserForm";
			base.ShowInTaskbar = false;
			base.SizeGripStyle = global::System.Windows.Forms.SizeGripStyle.Show;
			base.Load += new global::System.EventHandler(this.TypedObjectsBrowserForm_Load);
			base.FormClosing += new global::System.Windows.Forms.FormClosingEventHandler(this.TypedObjectsBrowserForm_FormClosing);
			this.SplitContainer.Panel1.ResumeLayout(false);
			this.SplitContainer.Panel2.ResumeLayout(false);
			this.SplitContainer.ResumeLayout(false);
			this.ObjectTypeGroupBox.ResumeLayout(false);
			this.TypeGroupBox.ResumeLayout(false);
			base.ResumeLayout(false);
			base.PerformLayout();
		}

		// Token: 0x04000464 RID: 1124
		private global::System.ComponentModel.IContainer components;

		// Token: 0x04000465 RID: 1125
		private global::System.Windows.Forms.Button TypedObjectFiltersEditorButton;

		// Token: 0x04000466 RID: 1126
		private global::System.Windows.Forms.ListView TypedObjectsListView;

		// Token: 0x04000467 RID: 1127
		private global::System.Windows.Forms.ComboBox TypedObjectFiltersComboBox;

		// Token: 0x04000468 RID: 1128
		private global::System.Windows.Forms.Label TypedObjectFiltersLabel;

		// Token: 0x04000469 RID: 1129
		private global::System.Windows.Forms.Label CurrentTypedObjectLabel;

		// Token: 0x0400046A RID: 1130
		private global::System.Windows.Forms.TextBox CurrentTypedObjectTextBox;

		// Token: 0x0400046B RID: 1131
		private global::System.Windows.Forms.Button okButton;

		// Token: 0x0400046C RID: 1132
		private global::System.Windows.Forms.Button cancelButton;

		// Token: 0x0400046D RID: 1133
		private global::System.Windows.Forms.Button AddTypedObjectButton;

		// Token: 0x0400046E RID: 1134
		private global::System.Windows.Forms.SplitContainer SplitContainer;

		// Token: 0x0400046F RID: 1135
		private global::System.Windows.Forms.PropertyControl PropertyControl;

		// Token: 0x04000470 RID: 1136
		private global::System.Windows.Forms.GroupBox TypeGroupBox;

		// Token: 0x04000471 RID: 1137
		private global::System.Windows.Forms.RadioButton Type0RadioButton;

		// Token: 0x04000472 RID: 1138
		private global::System.Windows.Forms.RadioButton Type1RadioButton;

		// Token: 0x04000473 RID: 1139
		private global::System.Windows.Forms.RadioButton Type2RadioButton;

		// Token: 0x04000474 RID: 1140
		private global::System.Windows.Forms.RadioButton Type3RadioButton;

		// Token: 0x04000475 RID: 1141
		private global::System.Windows.Forms.RadioButton Type4RadioButton;

		// Token: 0x04000476 RID: 1142
		private global::System.Windows.Forms.GroupBox ObjectTypeGroupBox;

		// Token: 0x04000477 RID: 1143
		private global::System.Windows.Forms.RadioButton ObjectType1RadioButton;

		// Token: 0x04000478 RID: 1144
		private global::System.Windows.Forms.RadioButton ObjectType0RadioButton;

		// Token: 0x04000479 RID: 1145
		private global::System.Windows.Forms.RadioButton ObjectType2RadioButton;

		// Token: 0x0400047A RID: 1146
		private global::System.Windows.Forms.Button ClearButton;

		// Token: 0x0400047B RID: 1147
		private global::System.Windows.Forms.RadioButton ObjectType3RadioButton;
	}
}
