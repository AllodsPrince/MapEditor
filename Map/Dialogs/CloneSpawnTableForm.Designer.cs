namespace MapEditor.Map.Dialogs
{
	// Token: 0x0200015B RID: 347
	public partial class CloneSpawnTableForm : global::System.Windows.Forms.Form
	{
		// Token: 0x060010A8 RID: 4264 RVA: 0x0007CC9C File Offset: 0x0007BC9C
		protected override void Dispose(bool disposing)
		{
			if (disposing && this.components != null)
			{
				this.components.Dispose();
			}
			base.Dispose(disposing);
		}

		// Token: 0x060010A9 RID: 4265 RVA: 0x0007CCBC File Offset: 0x0007BCBC
		private void InitializeComponent()
		{
			this.components = new global::System.ComponentModel.Container();
			global::System.ComponentModel.ComponentResourceManager resources = new global::System.ComponentModel.ComponentResourceManager(typeof(global::MapEditor.Map.Dialogs.CloneSpawnTableForm));
			this.cancelButton = new global::System.Windows.Forms.Button();
			this.CloneButton = new global::System.Windows.Forms.Button();
			this.TableListView = new global::System.Windows.Forms.ListView();
			this.DestinationColumnHeader = new global::System.Windows.Forms.ColumnHeader();
			this.SourceColumnHeader = new global::System.Windows.Forms.ColumnHeader();
			this.CountColumnHeader = new global::System.Windows.Forms.ColumnHeader();
			this.TableListViewContextMenuStrip = new global::System.Windows.Forms.ContextMenuStrip(this.components);
			this.editToolStripMenuItem = new global::System.Windows.Forms.ToolStripMenuItem();
			this.EditButton = new global::System.Windows.Forms.Button();
			this.TableListViewContextMenuStrip.SuspendLayout();
			base.SuspendLayout();
			resources.ApplyResources(this.cancelButton, "cancelButton");
			this.cancelButton.DialogResult = global::System.Windows.Forms.DialogResult.Cancel;
			this.cancelButton.Name = "cancelButton";
			this.cancelButton.UseVisualStyleBackColor = true;
			resources.ApplyResources(this.CloneButton, "CloneButton");
			this.CloneButton.DialogResult = global::System.Windows.Forms.DialogResult.OK;
			this.CloneButton.Name = "CloneButton";
			this.CloneButton.UseVisualStyleBackColor = true;
			resources.ApplyResources(this.TableListView, "TableListView");
			this.TableListView.Columns.AddRange(new global::System.Windows.Forms.ColumnHeader[]
			{
				this.DestinationColumnHeader,
				this.SourceColumnHeader,
				this.CountColumnHeader
			});
			this.TableListView.ContextMenuStrip = this.TableListViewContextMenuStrip;
			this.TableListView.FullRowSelect = true;
			this.TableListView.HeaderStyle = global::System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
			this.TableListView.HideSelection = false;
			this.TableListView.LabelEdit = true;
			this.TableListView.MultiSelect = false;
			this.TableListView.Name = "TableListView";
			this.TableListView.Sorting = global::System.Windows.Forms.SortOrder.Ascending;
			this.TableListView.UseCompatibleStateImageBehavior = false;
			this.TableListView.View = global::System.Windows.Forms.View.Details;
			this.TableListView.AfterLabelEdit += new global::System.Windows.Forms.LabelEditEventHandler(this.TableListView_AfterLabelEdit);
			this.TableListView.ItemSelectionChanged += new global::System.Windows.Forms.ListViewItemSelectionChangedEventHandler(this.TableListView_ItemSelectionChanged);
			resources.ApplyResources(this.DestinationColumnHeader, "DestinationColumnHeader");
			resources.ApplyResources(this.SourceColumnHeader, "SourceColumnHeader");
			resources.ApplyResources(this.CountColumnHeader, "CountColumnHeader");
			this.TableListViewContextMenuStrip.Items.AddRange(new global::System.Windows.Forms.ToolStripItem[]
			{
				this.editToolStripMenuItem
			});
			this.TableListViewContextMenuStrip.Name = "TableListViewContextMenuStrip";
			resources.ApplyResources(this.TableListViewContextMenuStrip, "TableListViewContextMenuStrip");
			this.TableListViewContextMenuStrip.Opened += new global::System.EventHandler(this.TableListViewContextMenuStrip_Opened);
			this.editToolStripMenuItem.Name = "editToolStripMenuItem";
			resources.ApplyResources(this.editToolStripMenuItem, "editToolStripMenuItem");
			this.editToolStripMenuItem.Click += new global::System.EventHandler(this.editToolStripMenuItem_Click);
			resources.ApplyResources(this.EditButton, "EditButton");
			this.EditButton.Name = "EditButton";
			this.EditButton.UseVisualStyleBackColor = true;
			this.EditButton.Click += new global::System.EventHandler(this.EditButton_Click);
			base.AcceptButton = this.CloneButton;
			resources.ApplyResources(this, "$this");
			base.AutoScaleMode = global::System.Windows.Forms.AutoScaleMode.Font;
			base.CancelButton = this.cancelButton;
			base.Controls.Add(this.EditButton);
			base.Controls.Add(this.TableListView);
			base.Controls.Add(this.CloneButton);
			base.Controls.Add(this.cancelButton);
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.Name = "CloneSpawnTableForm";
			base.ShowIcon = false;
			base.ShowInTaskbar = false;
			this.TableListViewContextMenuStrip.ResumeLayout(false);
			base.ResumeLayout(false);
		}

		// Token: 0x04000C35 RID: 3125
		private global::System.ComponentModel.IContainer components;

		// Token: 0x04000C36 RID: 3126
		private global::System.Windows.Forms.Button cancelButton;

		// Token: 0x04000C37 RID: 3127
		private global::System.Windows.Forms.Button CloneButton;

		// Token: 0x04000C38 RID: 3128
		private global::System.Windows.Forms.ListView TableListView;

		// Token: 0x04000C39 RID: 3129
		private global::System.Windows.Forms.ColumnHeader DestinationColumnHeader;

		// Token: 0x04000C3A RID: 3130
		private global::System.Windows.Forms.ColumnHeader CountColumnHeader;

		// Token: 0x04000C3B RID: 3131
		private global::System.Windows.Forms.ColumnHeader SourceColumnHeader;

		// Token: 0x04000C3C RID: 3132
		private global::System.Windows.Forms.Button EditButton;

		// Token: 0x04000C3D RID: 3133
		private global::System.Windows.Forms.ContextMenuStrip TableListViewContextMenuStrip;

		// Token: 0x04000C3E RID: 3134
		private global::System.Windows.Forms.ToolStripMenuItem editToolStripMenuItem;
	}
}
