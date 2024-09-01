namespace MapEditor.Forms.Groups
{
	// Token: 0x02000245 RID: 581
	public partial class GroupsForm : global::MapEditor.Forms.Base.BaseForm
	{
		// Token: 0x06001BC3 RID: 7107 RVA: 0x000B4E91 File Offset: 0x000B3E91
		protected override void Dispose(bool disposing)
		{
			if (disposing && this.components != null)
			{
				this.components.Dispose();
			}
			base.Dispose(disposing);
		}

		// Token: 0x06001BC4 RID: 7108 RVA: 0x000B4EB0 File Offset: 0x000B3EB0
		private void InitializeComponent()
		{
			this.components = new global::System.ComponentModel.Container();
			global::System.ComponentModel.ComponentResourceManager resources = new global::System.ComponentModel.ComponentResourceManager(typeof(global::MapEditor.Forms.Groups.GroupsForm));
			this.GroupButton = new global::System.Windows.Forms.Button();
			this.UngroupButton = new global::System.Windows.Forms.Button();
			this.CreateMultiobjectButton = new global::System.Windows.Forms.Button();
			this.SelectionType02RadioButton = new global::System.Windows.Forms.RadioButton();
			this.SelectionType01RadioButton = new global::System.Windows.Forms.RadioButton();
			this.SelectionTypeGroupBox = new global::System.Windows.Forms.GroupBox();
			this.SelectionType00RadioButton = new global::System.Windows.Forms.RadioButton();
			this.ObjectsTreeView = new global::MapEditor.Forms.Groups.TreeViewWithMultiselection();
			this.ObjectsTreeContextMenuStrip = new global::System.Windows.Forms.ContextMenuStrip(this.components);
			this.filterObjectToolStripMenuItem = new global::System.Windows.Forms.ToolStripMenuItem();
			this.clearFilterToolStripMenuItem = new global::System.Windows.Forms.ToolStripMenuItem();
			this.FlattenButton = new global::System.Windows.Forms.Button();
			this.FilterLabel = new global::System.Windows.Forms.Label();
			this.FilterComboBox = new global::System.Windows.Forms.ComboBox();
			this.ComboBoxTimer = new global::System.Windows.Forms.Timer(this.components);
			this.SelectionTypeGroupBox.SuspendLayout();
			this.ObjectsTreeContextMenuStrip.SuspendLayout();
			base.SuspendLayout();
			resources.ApplyResources(this.GroupButton, "GroupButton");
			this.GroupButton.Name = "GroupButton";
			this.GroupButton.UseVisualStyleBackColor = true;
			this.GroupButton.Click += new global::System.EventHandler(this.GroupButton_Click);
			resources.ApplyResources(this.UngroupButton, "UngroupButton");
			this.UngroupButton.Name = "UngroupButton";
			this.UngroupButton.UseVisualStyleBackColor = true;
			this.UngroupButton.Click += new global::System.EventHandler(this.UngroupButton_Click);
			resources.ApplyResources(this.CreateMultiobjectButton, "CreateMultiobjectButton");
			this.CreateMultiobjectButton.Name = "CreateMultiobjectButton";
			this.CreateMultiobjectButton.UseVisualStyleBackColor = true;
			this.CreateMultiobjectButton.Click += new global::System.EventHandler(this.CreateMultiobjectButton_Click);
			resources.ApplyResources(this.SelectionType02RadioButton, "SelectionType02RadioButton");
			this.SelectionType02RadioButton.Name = "SelectionType02RadioButton";
			this.SelectionType02RadioButton.Tag = "group_selection_type_all_levels";
			this.SelectionType02RadioButton.UseVisualStyleBackColor = true;
			this.SelectionType02RadioButton.CheckedChanged += new global::System.EventHandler(this.SelectionTypeRadioButton_CheckedChanged);
			resources.ApplyResources(this.SelectionType01RadioButton, "SelectionType01RadioButton");
			this.SelectionType01RadioButton.Checked = true;
			this.SelectionType01RadioButton.Name = "SelectionType01RadioButton";
			this.SelectionType01RadioButton.TabStop = true;
			this.SelectionType01RadioButton.Tag = "group_selection_type_one_level";
			this.SelectionType01RadioButton.UseVisualStyleBackColor = true;
			this.SelectionType01RadioButton.CheckedChanged += new global::System.EventHandler(this.SelectionTypeRadioButton_CheckedChanged);
			resources.ApplyResources(this.SelectionTypeGroupBox, "SelectionTypeGroupBox");
			this.SelectionTypeGroupBox.Controls.Add(this.SelectionType02RadioButton);
			this.SelectionTypeGroupBox.Controls.Add(this.SelectionType01RadioButton);
			this.SelectionTypeGroupBox.Controls.Add(this.SelectionType00RadioButton);
			this.SelectionTypeGroupBox.Name = "SelectionTypeGroupBox";
			this.SelectionTypeGroupBox.TabStop = false;
			resources.ApplyResources(this.SelectionType00RadioButton, "SelectionType00RadioButton");
			this.SelectionType00RadioButton.Name = "SelectionType00RadioButton";
			this.SelectionType00RadioButton.Tag = "group_selection_type_free";
			this.SelectionType00RadioButton.UseVisualStyleBackColor = true;
			this.SelectionType00RadioButton.CheckedChanged += new global::System.EventHandler(this.SelectionTypeRadioButton_CheckedChanged);
			this.ObjectsTreeView.AllowDrop = true;
			resources.ApplyResources(this.ObjectsTreeView, "ObjectsTreeView");
			this.ObjectsTreeView.ContextMenuStrip = this.ObjectsTreeContextMenuStrip;
			this.ObjectsTreeView.HideSelection = false;
			this.ObjectsTreeView.LabelEdit = true;
			this.ObjectsTreeView.Name = "ObjectsTreeView";
			this.ObjectsTreeView.SelectedNodes = (global::System.Collections.ArrayList)resources.GetObject("ObjectsTreeView.SelectedNodes");
			this.ObjectsTreeContextMenuStrip.Items.AddRange(new global::System.Windows.Forms.ToolStripItem[]
			{
				this.filterObjectToolStripMenuItem,
				this.clearFilterToolStripMenuItem
			});
			this.ObjectsTreeContextMenuStrip.Name = "ObjectsTreeContextMenuStrip";
			resources.ApplyResources(this.ObjectsTreeContextMenuStrip, "ObjectsTreeContextMenuStrip");
			this.ObjectsTreeContextMenuStrip.Opening += new global::System.ComponentModel.CancelEventHandler(this.ObjectsTreeContextMenuStrip_Opening);
			this.filterObjectToolStripMenuItem.Name = "filterObjectToolStripMenuItem";
			resources.ApplyResources(this.filterObjectToolStripMenuItem, "filterObjectToolStripMenuItem");
			this.filterObjectToolStripMenuItem.Click += new global::System.EventHandler(this.filterObjectToolStripMenuItem_Click);
			this.clearFilterToolStripMenuItem.Name = "clearFilterToolStripMenuItem";
			resources.ApplyResources(this.clearFilterToolStripMenuItem, "clearFilterToolStripMenuItem");
			this.clearFilterToolStripMenuItem.Click += new global::System.EventHandler(this.clearFilterToolStripMenuItem_Click);
			resources.ApplyResources(this.FlattenButton, "FlattenButton");
			this.FlattenButton.Name = "FlattenButton";
			this.FlattenButton.UseVisualStyleBackColor = true;
			this.FlattenButton.Click += new global::System.EventHandler(this.FlattenButton_Click);
			resources.ApplyResources(this.FilterLabel, "FilterLabel");
			this.FilterLabel.Name = "FilterLabel";
			resources.ApplyResources(this.FilterComboBox, "FilterComboBox");
			this.FilterComboBox.FormattingEnabled = true;
			this.FilterComboBox.Name = "FilterComboBox";
			this.FilterComboBox.TextChanged += new global::System.EventHandler(this.FilterComboBox_TextChanged);
			this.ComboBoxTimer.Interval = 1000;
			this.ComboBoxTimer.Tick += new global::System.EventHandler(this.ComboBoxTimer_Tick);
			resources.ApplyResources(this, "$this");
			base.AutoScaleMode = global::System.Windows.Forms.AutoScaleMode.Font;
			base.Controls.Add(this.FilterComboBox);
			base.Controls.Add(this.FilterLabel);
			base.Controls.Add(this.FlattenButton);
			base.Controls.Add(this.ObjectsTreeView);
			base.Controls.Add(this.SelectionTypeGroupBox);
			base.Controls.Add(this.GroupButton);
			base.Controls.Add(this.UngroupButton);
			base.Controls.Add(this.CreateMultiobjectButton);
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.Name = "GroupsForm";
			base.ShowInTaskbar = false;
			this.SelectionTypeGroupBox.ResumeLayout(false);
			this.SelectionTypeGroupBox.PerformLayout();
			this.ObjectsTreeContextMenuStrip.ResumeLayout(false);
			base.ResumeLayout(false);
			base.PerformLayout();
		}

		// Token: 0x040011EA RID: 4586
		private global::System.ComponentModel.IContainer components;

		// Token: 0x040011EB RID: 4587
		private global::System.Windows.Forms.Button GroupButton;

		// Token: 0x040011EC RID: 4588
		private global::System.Windows.Forms.Button UngroupButton;

		// Token: 0x040011ED RID: 4589
		private global::System.Windows.Forms.Button CreateMultiobjectButton;

		// Token: 0x040011EE RID: 4590
		private global::System.Windows.Forms.RadioButton SelectionType02RadioButton;

		// Token: 0x040011EF RID: 4591
		private global::System.Windows.Forms.RadioButton SelectionType01RadioButton;

		// Token: 0x040011F0 RID: 4592
		private global::System.Windows.Forms.GroupBox SelectionTypeGroupBox;

		// Token: 0x040011F1 RID: 4593
		private global::System.Windows.Forms.RadioButton SelectionType00RadioButton;

		// Token: 0x040011F2 RID: 4594
		private global::MapEditor.Forms.Groups.TreeViewWithMultiselection ObjectsTreeView;

		// Token: 0x040011F3 RID: 4595
		private global::System.Windows.Forms.Button FlattenButton;

		// Token: 0x040011F4 RID: 4596
		private global::System.Windows.Forms.Label FilterLabel;

		// Token: 0x040011F5 RID: 4597
		private global::System.Windows.Forms.ComboBox FilterComboBox;

		// Token: 0x040011F6 RID: 4598
		private global::System.Windows.Forms.Timer ComboBoxTimer;

		// Token: 0x040011F7 RID: 4599
		private global::System.Windows.Forms.ContextMenuStrip ObjectsTreeContextMenuStrip;

		// Token: 0x040011F8 RID: 4600
		private global::System.Windows.Forms.ToolStripMenuItem filterObjectToolStripMenuItem;

		// Token: 0x040011F9 RID: 4601
		private global::System.Windows.Forms.ToolStripMenuItem clearFilterToolStripMenuItem;
	}
}
