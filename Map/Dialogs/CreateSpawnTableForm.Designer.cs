namespace MapEditor.Map.Dialogs
{
	// Token: 0x020000D7 RID: 215
	public partial class CreateSpawnTableForm : global::System.Windows.Forms.Form
	{
		// Token: 0x06000B09 RID: 2825 RVA: 0x0005A128 File Offset: 0x00059128
		protected override void Dispose(bool disposing)
		{
			if (disposing && this.components != null)
			{
				this.components.Dispose();
			}
			base.Dispose(disposing);
		}

		// Token: 0x06000B0A RID: 2826 RVA: 0x0005A148 File Offset: 0x00059148
		private void InitializeComponent()
		{
			this.components = new global::System.ComponentModel.Container();
			global::System.ComponentModel.ComponentResourceManager resources = new global::System.ComponentModel.ComponentResourceManager(typeof(global::MapEditor.Map.Dialogs.CreateSpawnTableForm));
			this.CreateButton = new global::System.Windows.Forms.Button();
			this.cancelButton = new global::System.Windows.Forms.Button();
			this.NewTableTextBox = new global::System.Windows.Forms.TextBox();
			this.SpawnIDListView = new global::System.Windows.Forms.ListView();
			this.SpawnIDColumnHeader = new global::System.Windows.Forms.ColumnHeader();
			this.MobColumnHeader = new global::System.Windows.Forms.ColumnHeader();
			this.SpawnIDListViewContextMenuStrip = new global::System.Windows.Forms.ContextMenuStrip(this.components);
			this.editToolStripMenuItem = new global::System.Windows.Forms.ToolStripMenuItem();
			this.NewTableLabel = new global::System.Windows.Forms.Label();
			this.EditButton = new global::System.Windows.Forms.Button();
			this.SpawnIDListViewContextMenuStrip.SuspendLayout();
			base.SuspendLayout();
			resources.ApplyResources(this.CreateButton, "CreateButton");
			this.CreateButton.DialogResult = global::System.Windows.Forms.DialogResult.OK;
			this.CreateButton.Name = "CreateButton";
			this.CreateButton.UseVisualStyleBackColor = true;
			resources.ApplyResources(this.cancelButton, "cancelButton");
			this.cancelButton.DialogResult = global::System.Windows.Forms.DialogResult.Cancel;
			this.cancelButton.Name = "cancelButton";
			this.cancelButton.UseVisualStyleBackColor = true;
			resources.ApplyResources(this.NewTableTextBox, "NewTableTextBox");
			this.NewTableTextBox.Name = "NewTableTextBox";
			this.NewTableTextBox.TextChanged += new global::System.EventHandler(this.NewTableTextBox_TextChanged);
			resources.ApplyResources(this.SpawnIDListView, "SpawnIDListView");
			this.SpawnIDListView.Columns.AddRange(new global::System.Windows.Forms.ColumnHeader[]
			{
				this.SpawnIDColumnHeader,
				this.MobColumnHeader
			});
			this.SpawnIDListView.ContextMenuStrip = this.SpawnIDListViewContextMenuStrip;
			this.SpawnIDListView.FullRowSelect = true;
			this.SpawnIDListView.HeaderStyle = global::System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
			this.SpawnIDListView.HideSelection = false;
			this.SpawnIDListView.LabelEdit = true;
			this.SpawnIDListView.MultiSelect = false;
			this.SpawnIDListView.Name = "SpawnIDListView";
			this.SpawnIDListView.Sorting = global::System.Windows.Forms.SortOrder.Ascending;
			this.SpawnIDListView.UseCompatibleStateImageBehavior = false;
			this.SpawnIDListView.View = global::System.Windows.Forms.View.Details;
			this.SpawnIDListView.AfterLabelEdit += new global::System.Windows.Forms.LabelEditEventHandler(this.SpawnIDListView_AfterLabelEdit);
			this.SpawnIDListView.ItemSelectionChanged += new global::System.Windows.Forms.ListViewItemSelectionChangedEventHandler(this.SpawnIDListView_ItemSelectionChanged);
			resources.ApplyResources(this.SpawnIDColumnHeader, "SpawnIDColumnHeader");
			resources.ApplyResources(this.MobColumnHeader, "MobColumnHeader");
			this.SpawnIDListViewContextMenuStrip.Items.AddRange(new global::System.Windows.Forms.ToolStripItem[]
			{
				this.editToolStripMenuItem
			});
			this.SpawnIDListViewContextMenuStrip.Name = "SpawnIDListViewContextMenuStrip";
			resources.ApplyResources(this.SpawnIDListViewContextMenuStrip, "SpawnIDListViewContextMenuStrip");
			this.SpawnIDListViewContextMenuStrip.Opened += new global::System.EventHandler(this.SpawnIDListViewContextMenuStrip_Opened);
			this.editToolStripMenuItem.Name = "editToolStripMenuItem";
			resources.ApplyResources(this.editToolStripMenuItem, "editToolStripMenuItem");
			this.editToolStripMenuItem.Click += new global::System.EventHandler(this.editToolStripMenuItem_Click);
			resources.ApplyResources(this.NewTableLabel, "NewTableLabel");
			this.NewTableLabel.Name = "NewTableLabel";
			resources.ApplyResources(this.EditButton, "EditButton");
			this.EditButton.Name = "EditButton";
			this.EditButton.UseVisualStyleBackColor = true;
			this.EditButton.Click += new global::System.EventHandler(this.EditButton_Click);
			base.AcceptButton = this.CreateButton;
			resources.ApplyResources(this, "$this");
			base.AutoScaleMode = global::System.Windows.Forms.AutoScaleMode.Font;
			base.CancelButton = this.cancelButton;
			base.Controls.Add(this.EditButton);
			base.Controls.Add(this.NewTableLabel);
			base.Controls.Add(this.SpawnIDListView);
			base.Controls.Add(this.NewTableTextBox);
			base.Controls.Add(this.CreateButton);
			base.Controls.Add(this.cancelButton);
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.Name = "CreateSpawnTableForm";
			base.ShowIcon = false;
			base.ShowInTaskbar = false;
			this.SpawnIDListViewContextMenuStrip.ResumeLayout(false);
			base.ResumeLayout(false);
			base.PerformLayout();
		}

		// Token: 0x04000858 RID: 2136
		private global::System.ComponentModel.IContainer components;

		// Token: 0x04000859 RID: 2137
		private global::System.Windows.Forms.Button CreateButton;

		// Token: 0x0400085A RID: 2138
		private global::System.Windows.Forms.Button cancelButton;

		// Token: 0x0400085B RID: 2139
		private global::System.Windows.Forms.TextBox NewTableTextBox;

		// Token: 0x0400085C RID: 2140
		private global::System.Windows.Forms.ListView SpawnIDListView;

		// Token: 0x0400085D RID: 2141
		private global::System.Windows.Forms.Label NewTableLabel;

		// Token: 0x0400085E RID: 2142
		private global::System.Windows.Forms.Button EditButton;

		// Token: 0x0400085F RID: 2143
		private global::System.Windows.Forms.ColumnHeader SpawnIDColumnHeader;

		// Token: 0x04000860 RID: 2144
		private global::System.Windows.Forms.ColumnHeader MobColumnHeader;

		// Token: 0x04000861 RID: 2145
		private global::System.Windows.Forms.ContextMenuStrip SpawnIDListViewContextMenuStrip;

		// Token: 0x04000862 RID: 2146
		private global::System.Windows.Forms.ToolStripMenuItem editToolStripMenuItem;
	}
}
