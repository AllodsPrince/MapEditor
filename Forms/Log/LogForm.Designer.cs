namespace MapEditor.Forms.Log
{
	// Token: 0x0200001C RID: 28
	public partial class LogForm : global::MapEditor.Forms.Base.BaseForm
	{
		// Token: 0x06000257 RID: 599 RVA: 0x00019DAD File Offset: 0x00018DAD
		protected override void Dispose(bool disposing)
		{
			if (disposing && this.components != null)
			{
				this.components.Dispose();
			}
			base.Dispose(disposing);
		}

		// Token: 0x06000258 RID: 600 RVA: 0x00019DCC File Offset: 0x00018DCC
		private void InitializeComponent()
		{
			this.components = new global::System.ComponentModel.Container();
			global::System.ComponentModel.ComponentResourceManager resources = new global::System.ComponentModel.ComponentResourceManager(typeof(global::MapEditor.Forms.Log.LogForm));
			this.toolStrip1 = new global::System.Windows.Forms.ToolStrip();
			this.toggleInfo = new global::System.Windows.Forms.ToolStripButton();
			this.toggleWarning = new global::System.Windows.Forms.ToolStripButton();
			this.toggleError = new global::System.Windows.Forms.ToolStripButton();
			this.toggleFatal = new global::System.Windows.Forms.ToolStripButton();
			this.toolStripSeparator1 = new global::System.Windows.Forms.ToolStripSeparator();
			this.logTextBox = new global::System.Windows.Forms.RichTextBox();
			this.textBoxContextMenu = new global::System.Windows.Forms.ContextMenuStrip(this.components);
			this.selectAllToolStripMenuItem = new global::System.Windows.Forms.ToolStripMenuItem();
			this.copyToolStripMenuItem = new global::System.Windows.Forms.ToolStripMenuItem();
			this.clearAllToolStripMenuItem = new global::System.Windows.Forms.ToolStripMenuItem();
			this.toolStrip1.SuspendLayout();
			this.textBoxContextMenu.SuspendLayout();
			base.SuspendLayout();
			this.toolStrip1.AccessibleDescription = null;
			this.toolStrip1.AccessibleName = null;
			resources.ApplyResources(this.toolStrip1, "toolStrip1");
			this.toolStrip1.BackgroundImage = null;
			this.toolStrip1.Font = null;
			this.toolStrip1.Items.AddRange(new global::System.Windows.Forms.ToolStripItem[]
			{
				this.toggleInfo,
				this.toggleWarning,
				this.toggleError,
				this.toggleFatal,
				this.toolStripSeparator1
			});
			this.toolStrip1.Name = "toolStrip1";
			this.toggleInfo.AccessibleDescription = null;
			this.toggleInfo.AccessibleName = null;
			resources.ApplyResources(this.toggleInfo, "toggleInfo");
			this.toggleInfo.BackgroundImage = null;
			this.toggleInfo.Checked = true;
			this.toggleInfo.CheckOnClick = true;
			this.toggleInfo.CheckState = global::System.Windows.Forms.CheckState.Checked;
			this.toggleInfo.DisplayStyle = global::System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.toggleInfo.Name = "toggleInfo";
			this.toggleWarning.AccessibleDescription = null;
			this.toggleWarning.AccessibleName = null;
			resources.ApplyResources(this.toggleWarning, "toggleWarning");
			this.toggleWarning.BackgroundImage = null;
			this.toggleWarning.Checked = true;
			this.toggleWarning.CheckOnClick = true;
			this.toggleWarning.CheckState = global::System.Windows.Forms.CheckState.Checked;
			this.toggleWarning.DisplayStyle = global::System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.toggleWarning.Name = "toggleWarning";
			this.toggleError.AccessibleDescription = null;
			this.toggleError.AccessibleName = null;
			resources.ApplyResources(this.toggleError, "toggleError");
			this.toggleError.BackgroundImage = null;
			this.toggleError.Checked = true;
			this.toggleError.CheckOnClick = true;
			this.toggleError.CheckState = global::System.Windows.Forms.CheckState.Checked;
			this.toggleError.DisplayStyle = global::System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.toggleError.Name = "toggleError";
			this.toggleFatal.AccessibleDescription = null;
			this.toggleFatal.AccessibleName = null;
			resources.ApplyResources(this.toggleFatal, "toggleFatal");
			this.toggleFatal.BackgroundImage = null;
			this.toggleFatal.Checked = true;
			this.toggleFatal.CheckOnClick = true;
			this.toggleFatal.CheckState = global::System.Windows.Forms.CheckState.Checked;
			this.toggleFatal.DisplayStyle = global::System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.toggleFatal.Name = "toggleFatal";
			this.toolStripSeparator1.AccessibleDescription = null;
			this.toolStripSeparator1.AccessibleName = null;
			resources.ApplyResources(this.toolStripSeparator1, "toolStripSeparator1");
			this.toolStripSeparator1.Name = "toolStripSeparator1";
			this.logTextBox.AccessibleDescription = null;
			this.logTextBox.AccessibleName = null;
			resources.ApplyResources(this.logTextBox, "logTextBox");
			this.logTextBox.BackgroundImage = null;
			this.logTextBox.ContextMenuStrip = this.textBoxContextMenu;
			this.logTextBox.Font = null;
			this.logTextBox.Name = "logTextBox";
			this.logTextBox.ReadOnly = true;
			this.textBoxContextMenu.AccessibleDescription = null;
			this.textBoxContextMenu.AccessibleName = null;
			resources.ApplyResources(this.textBoxContextMenu, "textBoxContextMenu");
			this.textBoxContextMenu.BackgroundImage = null;
			this.textBoxContextMenu.Font = null;
			this.textBoxContextMenu.Items.AddRange(new global::System.Windows.Forms.ToolStripItem[]
			{
				this.selectAllToolStripMenuItem,
				this.copyToolStripMenuItem,
				this.clearAllToolStripMenuItem
			});
			this.textBoxContextMenu.Name = "textBoxContextMenu";
			this.selectAllToolStripMenuItem.AccessibleDescription = null;
			this.selectAllToolStripMenuItem.AccessibleName = null;
			resources.ApplyResources(this.selectAllToolStripMenuItem, "selectAllToolStripMenuItem");
			this.selectAllToolStripMenuItem.BackgroundImage = null;
			this.selectAllToolStripMenuItem.Name = "selectAllToolStripMenuItem";
			this.selectAllToolStripMenuItem.ShortcutKeyDisplayString = null;
			this.selectAllToolStripMenuItem.Tag = "log_select_all";
			this.selectAllToolStripMenuItem.Click += new global::System.EventHandler(this.SelectAllContextMenuHandler);
			this.copyToolStripMenuItem.AccessibleDescription = null;
			this.copyToolStripMenuItem.AccessibleName = null;
			resources.ApplyResources(this.copyToolStripMenuItem, "copyToolStripMenuItem");
			this.copyToolStripMenuItem.BackgroundImage = null;
			this.copyToolStripMenuItem.Name = "copyToolStripMenuItem";
			this.copyToolStripMenuItem.ShortcutKeyDisplayString = null;
			this.copyToolStripMenuItem.Tag = "log_copy";
			this.copyToolStripMenuItem.Click += new global::System.EventHandler(this.CopyContextMenuHandler);
			this.clearAllToolStripMenuItem.AccessibleDescription = null;
			this.clearAllToolStripMenuItem.AccessibleName = null;
			resources.ApplyResources(this.clearAllToolStripMenuItem, "clearAllToolStripMenuItem");
			this.clearAllToolStripMenuItem.BackgroundImage = null;
			this.clearAllToolStripMenuItem.Name = "clearAllToolStripMenuItem";
			this.clearAllToolStripMenuItem.ShortcutKeyDisplayString = null;
			this.clearAllToolStripMenuItem.Tag = "log_clear_all";
			this.clearAllToolStripMenuItem.Click += new global::System.EventHandler(this.ClearContextMenuHandler);
			base.AccessibleDescription = null;
			base.AccessibleName = null;
			resources.ApplyResources(this, "$this");
			base.AutoScaleMode = global::System.Windows.Forms.AutoScaleMode.Font;
			this.BackgroundImage = null;
			base.Controls.Add(this.logTextBox);
			base.Controls.Add(this.toolStrip1);
			this.Font = null;
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.Name = "LogForm";
			base.ShowInTaskbar = false;
			base.FormClosing += new global::System.Windows.Forms.FormClosingEventHandler(this.FormClosingHandler);
			this.toolStrip1.ResumeLayout(false);
			this.toolStrip1.PerformLayout();
			this.textBoxContextMenu.ResumeLayout(false);
			base.ResumeLayout(false);
			base.PerformLayout();
		}

		// Token: 0x04000217 RID: 535
		private global::System.ComponentModel.IContainer components;

		// Token: 0x04000218 RID: 536
		private global::System.Windows.Forms.ToolStrip toolStrip1;

		// Token: 0x04000219 RID: 537
		private global::System.Windows.Forms.ToolStripButton toggleInfo;

		// Token: 0x0400021A RID: 538
		private global::System.Windows.Forms.ToolStripButton toggleWarning;

		// Token: 0x0400021B RID: 539
		private global::System.Windows.Forms.ToolStripButton toggleError;

		// Token: 0x0400021C RID: 540
		private global::System.Windows.Forms.ToolStripButton toggleFatal;

		// Token: 0x0400021D RID: 541
		private global::System.Windows.Forms.ToolStripSeparator toolStripSeparator1;

		// Token: 0x0400021E RID: 542
		private global::System.Windows.Forms.RichTextBox logTextBox;

		// Token: 0x0400021F RID: 543
		private global::System.Windows.Forms.ContextMenuStrip textBoxContextMenu;

		// Token: 0x04000220 RID: 544
		private global::System.Windows.Forms.ToolStripMenuItem selectAllToolStripMenuItem;

		// Token: 0x04000221 RID: 545
		private global::System.Windows.Forms.ToolStripMenuItem copyToolStripMenuItem;

		// Token: 0x04000222 RID: 546
		private global::System.Windows.Forms.ToolStripMenuItem clearAllToolStripMenuItem;
	}
}
