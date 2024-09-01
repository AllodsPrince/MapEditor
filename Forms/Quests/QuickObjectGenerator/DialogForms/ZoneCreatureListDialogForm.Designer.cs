namespace MapEditor.Forms.Quests.QuickObjectGenerator.DialogForms
{
	// Token: 0x02000267 RID: 615
	public partial class ZoneCreatureListDialogForm : global::System.Windows.Forms.Form
	{
		// Token: 0x06001D03 RID: 7427 RVA: 0x000B8DC8 File Offset: 0x000B7DC8
		protected override void Dispose(bool disposing)
		{
			if (disposing && this.components != null)
			{
				this.components.Dispose();
			}
			base.Dispose(disposing);
		}

		// Token: 0x06001D04 RID: 7428 RVA: 0x000B8DE8 File Offset: 0x000B7DE8
		private void InitializeComponent()
		{
			this.components = new global::System.ComponentModel.Container();
			global::System.ComponentModel.ComponentResourceManager resources = new global::System.ComponentModel.ComponentResourceManager(typeof(global::MapEditor.Forms.Quests.QuickObjectGenerator.DialogForms.ZoneCreatureListDialogForm));
			this.listView = new global::System.Windows.Forms.ListView();
			this.columnHeader1 = new global::System.Windows.Forms.ColumnHeader();
			this.ListViewMenuStrip = new global::System.Windows.Forms.ContextMenuStrip(this.components);
			this.AddMenuItem = new global::System.Windows.Forms.ToolStripMenuItem();
			this.BrowseMenuItem = new global::System.Windows.Forms.ToolStripMenuItem();
			this.DeleteMenuItem = new global::System.Windows.Forms.ToolStripMenuItem();
			this.ZoneComboBox = new global::System.Windows.Forms.ComboBox();
			this.label1 = new global::System.Windows.Forms.Label();
			this.SaveButton = new global::System.Windows.Forms.Button();
			this.CloseButton = new global::System.Windows.Forms.Button();
			this.ListViewMenuStrip.SuspendLayout();
			base.SuspendLayout();
			resources.ApplyResources(this.listView, "listView");
			this.listView.Columns.AddRange(new global::System.Windows.Forms.ColumnHeader[]
			{
				this.columnHeader1
			});
			this.listView.ContextMenuStrip = this.ListViewMenuStrip;
			this.listView.GridLines = true;
			this.listView.LabelEdit = true;
			this.listView.Name = "listView";
			this.listView.Sorting = global::System.Windows.Forms.SortOrder.Ascending;
			this.listView.UseCompatibleStateImageBehavior = false;
			this.listView.View = global::System.Windows.Forms.View.Details;
			resources.ApplyResources(this.columnHeader1, "columnHeader1");
			this.ListViewMenuStrip.Items.AddRange(new global::System.Windows.Forms.ToolStripItem[]
			{
				this.AddMenuItem,
				this.BrowseMenuItem,
				this.DeleteMenuItem
			});
			this.ListViewMenuStrip.Name = "ListViewMenuStrip";
			resources.ApplyResources(this.ListViewMenuStrip, "ListViewMenuStrip");
			this.AddMenuItem.Name = "AddMenuItem";
			resources.ApplyResources(this.AddMenuItem, "AddMenuItem");
			this.BrowseMenuItem.Name = "BrowseMenuItem";
			resources.ApplyResources(this.BrowseMenuItem, "BrowseMenuItem");
			this.DeleteMenuItem.Name = "DeleteMenuItem";
			resources.ApplyResources(this.DeleteMenuItem, "DeleteMenuItem");
			this.ZoneComboBox.DropDownStyle = global::System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.ZoneComboBox.FormattingEnabled = true;
			resources.ApplyResources(this.ZoneComboBox, "ZoneComboBox");
			this.ZoneComboBox.Name = "ZoneComboBox";
			this.ZoneComboBox.Sorted = true;
			resources.ApplyResources(this.label1, "label1");
			this.label1.Name = "label1";
			resources.ApplyResources(this.SaveButton, "SaveButton");
			this.SaveButton.Name = "SaveButton";
			this.SaveButton.UseVisualStyleBackColor = true;
			resources.ApplyResources(this.CloseButton, "CloseButton");
			this.CloseButton.DialogResult = global::System.Windows.Forms.DialogResult.OK;
			this.CloseButton.Name = "CloseButton";
			this.CloseButton.UseVisualStyleBackColor = true;
			base.AcceptButton = this.CloseButton;
			resources.ApplyResources(this, "$this");
			base.AutoScaleMode = global::System.Windows.Forms.AutoScaleMode.Font;
			base.CancelButton = this.CloseButton;
			base.Controls.Add(this.CloseButton);
			base.Controls.Add(this.label1);
			base.Controls.Add(this.ZoneComboBox);
			base.Controls.Add(this.SaveButton);
			base.Controls.Add(this.listView);
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.Name = "ZoneCreatureListDialogForm";
			base.ShowInTaskbar = false;
			this.ListViewMenuStrip.ResumeLayout(false);
			base.ResumeLayout(false);
			base.PerformLayout();
		}

		// Token: 0x04001270 RID: 4720
		private global::System.ComponentModel.IContainer components;

		// Token: 0x04001271 RID: 4721
		private global::System.Windows.Forms.ListView listView;

		// Token: 0x04001272 RID: 4722
		private global::System.Windows.Forms.ComboBox ZoneComboBox;

		// Token: 0x04001273 RID: 4723
		private global::System.Windows.Forms.Label label1;

		// Token: 0x04001274 RID: 4724
		private global::System.Windows.Forms.ColumnHeader columnHeader1;

		// Token: 0x04001275 RID: 4725
		private global::System.Windows.Forms.ContextMenuStrip ListViewMenuStrip;

		// Token: 0x04001276 RID: 4726
		private global::System.Windows.Forms.ToolStripMenuItem AddMenuItem;

		// Token: 0x04001277 RID: 4727
		private global::System.Windows.Forms.ToolStripMenuItem DeleteMenuItem;

		// Token: 0x04001278 RID: 4728
		private global::System.Windows.Forms.Button SaveButton;

		// Token: 0x04001279 RID: 4729
		private global::System.Windows.Forms.Button CloseButton;

		// Token: 0x0400127A RID: 4730
		private global::System.Windows.Forms.ToolStripMenuItem BrowseMenuItem;
	}
}
