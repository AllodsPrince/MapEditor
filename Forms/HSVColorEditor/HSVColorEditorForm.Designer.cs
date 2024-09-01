namespace MapEditor.Forms.HSVColorEditor
{
	// Token: 0x020000B3 RID: 179
	public partial class HSVColorEditorForm : global::MapEditor.Forms.Base.BaseForm
	{
		// Token: 0x060008DA RID: 2266 RVA: 0x0004C875 File Offset: 0x0004B875
		protected override void Dispose(bool disposing)
		{
			if (disposing && this.components != null)
			{
				this.components.Dispose();
			}
			base.Dispose(disposing);
		}

		// Token: 0x060008DB RID: 2267 RVA: 0x0004C894 File Offset: 0x0004B894
		private void InitializeComponent()
		{
			this.components = new global::System.ComponentModel.Container();
			this.selectedFieldsListView = new global::System.Windows.Forms.ListView();
			this.propertyControl = new global::System.Windows.Forms.PropertyControl();
			this.splitContainer = new global::System.Windows.Forms.SplitContainer();
			this.addFieldButton = new global::System.Windows.Forms.Button();
			this.redoButton = new global::System.Windows.Forms.Button();
			this.undoButton = new global::System.Windows.Forms.Button();
			this.findComboBox = new global::System.Windows.Forms.ComboBox();
			this.findOtherButton = new global::System.Windows.Forms.Button();
			this.findButton = new global::System.Windows.Forms.Button();
			this.removeAllFieldsButton = new global::System.Windows.Forms.Button();
			this.removeFieldButton = new global::System.Windows.Forms.Button();
			this.toolTip1 = new global::System.Windows.Forms.ToolTip(this.components);
			this.splitContainer.Panel1.SuspendLayout();
			this.splitContainer.Panel2.SuspendLayout();
			this.splitContainer.SuspendLayout();
			base.SuspendLayout();
			this.selectedFieldsListView.Anchor = (global::System.Windows.Forms.AnchorStyles.Top | global::System.Windows.Forms.AnchorStyles.Bottom | global::System.Windows.Forms.AnchorStyles.Left | global::System.Windows.Forms.AnchorStyles.Right);
			this.selectedFieldsListView.HideSelection = false;
			this.selectedFieldsListView.Location = new global::System.Drawing.Point(44, 32);
			this.selectedFieldsListView.Name = "selectedFieldsListView";
			this.selectedFieldsListView.Size = new global::System.Drawing.Size(198, 352);
			this.selectedFieldsListView.Sorting = global::System.Windows.Forms.SortOrder.Ascending;
			this.selectedFieldsListView.TabIndex = 41;
			this.selectedFieldsListView.TileSize = new global::System.Drawing.Size(168, 15);
			this.selectedFieldsListView.UseCompatibleStateImageBehavior = false;
			this.selectedFieldsListView.View = global::System.Windows.Forms.View.SmallIcon;
			this.propertyControl.AllowDrop = true;
			this.propertyControl.Anchor = (global::System.Windows.Forms.AnchorStyles.Top | global::System.Windows.Forms.AnchorStyles.Bottom | global::System.Windows.Forms.AnchorStyles.Left | global::System.Windows.Forms.AnchorStyles.Right);
			this.propertyControl.DefaultLocationFolder = "";
			this.propertyControl.Location = new global::System.Drawing.Point(0, 3);
			this.propertyControl.Name = "propertyControl";
			this.propertyControl.SelectedObject = null;
			this.propertyControl.SelectedObjects = new object[0];
			this.propertyControl.Size = new global::System.Drawing.Size(326, 405);
			this.propertyControl.SkipRefresh = false;
			this.propertyControl.TabIndex = 36;
			this.propertyControl.TitleControl = null;
			this.propertyControl.TitleRelativeFrom = null;
			this.propertyControl.TitleStart = "";
			this.splitContainer.BorderStyle = global::System.Windows.Forms.BorderStyle.Fixed3D;
			this.splitContainer.Dock = global::System.Windows.Forms.DockStyle.Fill;
			this.splitContainer.Location = new global::System.Drawing.Point(0, 0);
			this.splitContainer.Name = "splitContainer";
			this.splitContainer.Panel1.Controls.Add(this.propertyControl);
			this.splitContainer.Panel1.Controls.Add(this.addFieldButton);
			this.splitContainer.Panel2.Controls.Add(this.redoButton);
			this.splitContainer.Panel2.Controls.Add(this.undoButton);
			this.splitContainer.Panel2.Controls.Add(this.findComboBox);
			this.splitContainer.Panel2.Controls.Add(this.findOtherButton);
			this.splitContainer.Panel2.Controls.Add(this.findButton);
			this.splitContainer.Panel2.Controls.Add(this.removeAllFieldsButton);
			this.splitContainer.Panel2.Controls.Add(this.removeFieldButton);
			this.splitContainer.Panel2.Controls.Add(this.selectedFieldsListView);
			this.splitContainer.Size = new global::System.Drawing.Size(621, 415);
			this.splitContainer.SplitterDistance = 373;
			this.splitContainer.TabIndex = 41;
			this.addFieldButton.Anchor = (global::System.Windows.Forms.AnchorStyles.Top | global::System.Windows.Forms.AnchorStyles.Right);
			this.addFieldButton.Location = new global::System.Drawing.Point(332, 53);
			this.addFieldButton.Name = "addFieldButton";
			this.addFieldButton.Size = new global::System.Drawing.Size(35, 23);
			this.addFieldButton.TabIndex = 39;
			this.addFieldButton.Text = ">>";
			this.toolTip1.SetToolTip(this.addFieldButton, "Add");
			this.addFieldButton.UseVisualStyleBackColor = true;
			this.redoButton.Anchor = (global::System.Windows.Forms.AnchorStyles.Bottom | global::System.Windows.Forms.AnchorStyles.Left);
			this.redoButton.Enabled = false;
			this.redoButton.Location = new global::System.Drawing.Point(94, 387);
			this.redoButton.Name = "redoButton";
			this.redoButton.Size = new global::System.Drawing.Size(44, 21);
			this.redoButton.TabIndex = 48;
			this.redoButton.Text = "Redo";
			this.redoButton.UseVisualStyleBackColor = true;
			this.undoButton.Anchor = (global::System.Windows.Forms.AnchorStyles.Bottom | global::System.Windows.Forms.AnchorStyles.Left);
			this.undoButton.Enabled = false;
			this.undoButton.Location = new global::System.Drawing.Point(44, 387);
			this.undoButton.Name = "undoButton";
			this.undoButton.Size = new global::System.Drawing.Size(44, 21);
			this.undoButton.TabIndex = 47;
			this.undoButton.Text = "Undo";
			this.undoButton.UseVisualStyleBackColor = true;
			this.findComboBox.Anchor = (global::System.Windows.Forms.AnchorStyles.Top | global::System.Windows.Forms.AnchorStyles.Left | global::System.Windows.Forms.AnchorStyles.Right);
			this.findComboBox.FlatStyle = global::System.Windows.Forms.FlatStyle.Flat;
			this.findComboBox.FormattingEnabled = true;
			this.findComboBox.Location = new global::System.Drawing.Point(44, 5);
			this.findComboBox.Name = "findComboBox";
			this.findComboBox.Size = new global::System.Drawing.Size(100, 21);
			this.findComboBox.TabIndex = 46;
			this.findOtherButton.Anchor = (global::System.Windows.Forms.AnchorStyles.Top | global::System.Windows.Forms.AnchorStyles.Right);
			this.findOtherButton.Location = new global::System.Drawing.Point(191, 4);
			this.findOtherButton.Name = "findOtherButton";
			this.findOtherButton.Size = new global::System.Drawing.Size(49, 21);
			this.findOtherButton.TabIndex = 45;
			this.findOtherButton.Text = "Others";
			this.findOtherButton.UseVisualStyleBackColor = true;
			this.findButton.Anchor = (global::System.Windows.Forms.AnchorStyles.Top | global::System.Windows.Forms.AnchorStyles.Right);
			this.findButton.Location = new global::System.Drawing.Point(146, 4);
			this.findButton.Name = "findButton";
			this.findButton.Size = new global::System.Drawing.Size(44, 21);
			this.findButton.TabIndex = 44;
			this.findButton.Text = "Find";
			this.findButton.UseVisualStyleBackColor = true;
			this.removeAllFieldsButton.Location = new global::System.Drawing.Point(3, 82);
			this.removeAllFieldsButton.Name = "removeAllFieldsButton";
			this.removeAllFieldsButton.Size = new global::System.Drawing.Size(35, 23);
			this.removeAllFieldsButton.TabIndex = 42;
			this.removeAllFieldsButton.Text = "<<<";
			this.toolTip1.SetToolTip(this.removeAllFieldsButton, "Remove All");
			this.removeAllFieldsButton.UseVisualStyleBackColor = true;
			this.removeFieldButton.Location = new global::System.Drawing.Point(3, 53);
			this.removeFieldButton.Name = "removeFieldButton";
			this.removeFieldButton.Size = new global::System.Drawing.Size(35, 23);
			this.removeFieldButton.TabIndex = 40;
			this.removeFieldButton.Text = "<<";
			this.toolTip1.SetToolTip(this.removeFieldButton, "Remove");
			this.removeFieldButton.UseVisualStyleBackColor = true;
			base.AutoScaleDimensions = new global::System.Drawing.SizeF(6f, 13f);
			base.AutoScaleMode = global::System.Windows.Forms.AutoScaleMode.Font;
			base.ClientSize = new global::System.Drawing.Size(621, 415);
			base.Controls.Add(this.splitContainer);
			base.Name = "HSVColorEditorForm";
			this.Text = "HSV Color Editor";
			this.splitContainer.Panel1.ResumeLayout(false);
			this.splitContainer.Panel2.ResumeLayout(false);
			this.splitContainer.ResumeLayout(false);
			base.ResumeLayout(false);
		}

		// Token: 0x0400077C RID: 1916
		private global::System.ComponentModel.IContainer components;

		// Token: 0x0400077D RID: 1917
		private global::System.Windows.Forms.ListView selectedFieldsListView;

		// Token: 0x0400077E RID: 1918
		private global::System.Windows.Forms.PropertyControl propertyControl;

		// Token: 0x0400077F RID: 1919
		private global::System.Windows.Forms.SplitContainer splitContainer;

		// Token: 0x04000780 RID: 1920
		private global::System.Windows.Forms.Button addFieldButton;

		// Token: 0x04000781 RID: 1921
		private global::System.Windows.Forms.Button removeFieldButton;

		// Token: 0x04000782 RID: 1922
		private global::System.Windows.Forms.ToolTip toolTip1;

		// Token: 0x04000783 RID: 1923
		private global::System.Windows.Forms.Button removeAllFieldsButton;

		// Token: 0x04000784 RID: 1924
		private global::System.Windows.Forms.Button findButton;

		// Token: 0x04000785 RID: 1925
		private global::System.Windows.Forms.Button findOtherButton;

		// Token: 0x04000786 RID: 1926
		private global::System.Windows.Forms.ComboBox findComboBox;

		// Token: 0x04000787 RID: 1927
		private global::System.Windows.Forms.Button redoButton;

		// Token: 0x04000788 RID: 1928
		private global::System.Windows.Forms.Button undoButton;
	}
}
