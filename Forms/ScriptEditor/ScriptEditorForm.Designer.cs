namespace MapEditor.Forms.ScriptEditor
{
	// Token: 0x0200004C RID: 76
	public partial class ScriptEditorForm : global::MapEditor.Forms.Base.BaseForm
	{
		// Token: 0x06000415 RID: 1045 RVA: 0x00021EB5 File Offset: 0x00020EB5
		protected override void Dispose(bool disposing)
		{
			if (disposing && this.components != null)
			{
				this.components.Dispose();
			}
			base.Dispose(disposing);
		}

		// Token: 0x06000416 RID: 1046 RVA: 0x00021ED4 File Offset: 0x00020ED4
		private void InitializeComponent()
		{
			global::System.ComponentModel.ComponentResourceManager resources = new global::System.ComponentModel.ComponentResourceManager(typeof(global::MapEditor.Forms.ScriptEditor.ScriptEditorForm));
			this.scriptEditorControl = new global::System.Windows.Forms.ScriptEditorFormControl(this);
			this.menuStrip1 = new global::System.Windows.Forms.MenuStrip();
			this.historyToolStripMenuItem = new global::System.Windows.Forms.ToolStripMenuItem();
			this.recentitem0ToolStripMenuItem = new global::System.Windows.Forms.ToolStripMenuItem();
			this.recentitem1ToolStripMenuItem = new global::System.Windows.Forms.ToolStripMenuItem();
			this.recentitem2ToolStripMenuItem = new global::System.Windows.Forms.ToolStripMenuItem();
			this.recentitem3ToolStripMenuItem = new global::System.Windows.Forms.ToolStripMenuItem();
			this.recentitem4ToolStripMenuItem = new global::System.Windows.Forms.ToolStripMenuItem();
			this.menuStrip1.SuspendLayout();
			base.SuspendLayout();
			resources.ApplyResources(this.scriptEditorControl, "scriptEditorControl");
			this.scriptEditorControl.Name = "scriptEditorControl";
			this.scriptEditorControl.Dock = global::System.Windows.Forms.DockStyle.Fill;
			this.menuStrip1.Items.AddRange(new global::System.Windows.Forms.ToolStripItem[]
			{
				this.historyToolStripMenuItem
			});
			resources.ApplyResources(this.menuStrip1, "menuStrip1");
			this.menuStrip1.Name = "menuStrip1";
			this.historyToolStripMenuItem.DropDownItems.AddRange(new global::System.Windows.Forms.ToolStripItem[]
			{
				this.recentitem0ToolStripMenuItem,
				this.recentitem1ToolStripMenuItem,
				this.recentitem2ToolStripMenuItem,
				this.recentitem3ToolStripMenuItem,
				this.recentitem4ToolStripMenuItem
			});
			this.historyToolStripMenuItem.Name = "historyToolStripMenuItem";
			resources.ApplyResources(this.historyToolStripMenuItem, "historyToolStripMenuItem");
			this.recentitem0ToolStripMenuItem.Name = "recentitem0ToolStripMenuItem";
			resources.ApplyResources(this.recentitem0ToolStripMenuItem, "recentitem0ToolStripMenuItem");
			this.recentitem0ToolStripMenuItem.Tag = "recent_item_0";
			this.recentitem1ToolStripMenuItem.Name = "recentitem1ToolStripMenuItem";
			resources.ApplyResources(this.recentitem1ToolStripMenuItem, "recentitem1ToolStripMenuItem");
			this.recentitem1ToolStripMenuItem.Tag = "recent_item_1";
			this.recentitem2ToolStripMenuItem.Name = "recentitem2ToolStripMenuItem";
			resources.ApplyResources(this.recentitem2ToolStripMenuItem, "recentitem2ToolStripMenuItem");
			this.recentitem2ToolStripMenuItem.Tag = "recent_item_2";
			this.recentitem3ToolStripMenuItem.Name = "recentitem3ToolStripMenuItem";
			resources.ApplyResources(this.recentitem3ToolStripMenuItem, "recentitem3ToolStripMenuItem");
			this.recentitem3ToolStripMenuItem.Tag = "recent_item_3";
			this.recentitem4ToolStripMenuItem.Name = "recentitem4ToolStripMenuItem";
			resources.ApplyResources(this.recentitem4ToolStripMenuItem, "recentitem4ToolStripMenuItem");
			this.recentitem4ToolStripMenuItem.Tag = "recent_item_4";
			resources.ApplyResources(this, "$this");
			base.AutoScaleMode = global::System.Windows.Forms.AutoScaleMode.Font;
			base.Controls.Add(this.scriptEditorControl);
			base.Controls.Add(this.menuStrip1);
			base.MainMenuStrip = this.menuStrip1;
			base.Name = "ScriptEditorForm";
			this.menuStrip1.ResumeLayout(false);
			this.menuStrip1.PerformLayout();
			base.ResumeLayout(false);
			base.PerformLayout();
		}

		// Token: 0x040002D0 RID: 720
		private global::System.ComponentModel.IContainer components;

		// Token: 0x040002D1 RID: 721
		private global::System.Windows.Forms.ScriptEditorFormControl scriptEditorControl;

		// Token: 0x040002D2 RID: 722
		private global::System.Windows.Forms.MenuStrip menuStrip1;

		// Token: 0x040002D3 RID: 723
		private global::System.Windows.Forms.ToolStripMenuItem historyToolStripMenuItem;

		// Token: 0x040002D4 RID: 724
		private global::System.Windows.Forms.ToolStripMenuItem recentitem0ToolStripMenuItem;

		// Token: 0x040002D5 RID: 725
		private global::System.Windows.Forms.ToolStripMenuItem recentitem1ToolStripMenuItem;

		// Token: 0x040002D6 RID: 726
		private global::System.Windows.Forms.ToolStripMenuItem recentitem2ToolStripMenuItem;

		// Token: 0x040002D7 RID: 727
		private global::System.Windows.Forms.ToolStripMenuItem recentitem3ToolStripMenuItem;

		// Token: 0x040002D8 RID: 728
		private global::System.Windows.Forms.ToolStripMenuItem recentitem4ToolStripMenuItem;
	}
}
