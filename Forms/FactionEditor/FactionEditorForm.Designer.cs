namespace MapEditor.Forms.FactionEditor
{
	// Token: 0x020000EF RID: 239
	public partial class FactionEditorForm : global::MapEditor.Forms.Base.BaseForm
	{
		// Token: 0x06000C29 RID: 3113 RVA: 0x00068C5D File Offset: 0x00067C5D
		protected override void Dispose(bool disposing)
		{
			if (disposing && this.components != null)
			{
				this.components.Dispose();
			}
			base.Dispose(disposing);
		}

		// Token: 0x06000C2A RID: 3114 RVA: 0x00068C7C File Offset: 0x00067C7C
		private void InitializeComponent()
		{
			this.components = new global::System.ComponentModel.Container();
			global::System.ComponentModel.ComponentResourceManager resources = new global::System.ComponentModel.ComponentResourceManager(typeof(global::MapEditor.Forms.FactionEditor.FactionEditorForm));
			this.SplitContainer = new global::System.Windows.Forms.SplitContainer();
			this.ClearFactionButton = new global::System.Windows.Forms.Button();
			this.FactionEditorImageList = new global::System.Windows.Forms.ImageList(this.components);
			this.FactionTreeView = new global::System.Windows.Forms.TreeView();
			this.FactionContextMenuStrip = new global::System.Windows.Forms.ContextMenuStrip(this.components);
			this.toolStripMenuItem1 = new global::System.Windows.Forms.ToolStripMenuItem();
			this.newSubfactionToolStripMenuItem = new global::System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator1 = new global::System.Windows.Forms.ToolStripSeparator();
			this.clearFactionToolStripMenuItem = new global::System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator2 = new global::System.Windows.Forms.ToolStripSeparator();
			this.hideMobWithFactionToolStripMenuItem = new global::System.Windows.Forms.ToolStripMenuItem();
			this.HideMobWithFactionCheckBox = new global::System.Windows.Forms.CheckBox();
			this.AddSubfactionButton = new global::System.Windows.Forms.Button();
			this.AddFactionButton = new global::System.Windows.Forms.Button();
			this.PropertyControl = new global::System.Windows.Forms.PropertyControl();
			this.FilterComboBox = new global::System.Windows.Forms.ComboBox();
			this.label1 = new global::System.Windows.Forms.Label();
			this.FilterButton = new global::System.Windows.Forms.Button();
			this.ErrorItemList = new global::System.Windows.Forms.ListBox();
			this.FactionEditorTooltip = new global::System.Windows.Forms.ToolTip(this.components);
			this.CloseButton = new global::System.Windows.Forms.Button();
			this.checkBox1 = new global::System.Windows.Forms.CheckBox();
			this.SplitContainer.Panel1.SuspendLayout();
			this.SplitContainer.Panel2.SuspendLayout();
			this.SplitContainer.SuspendLayout();
			this.FactionContextMenuStrip.SuspendLayout();
			base.SuspendLayout();
			resources.ApplyResources(this.SplitContainer, "SplitContainer");
			this.SplitContainer.Name = "SplitContainer";
			this.SplitContainer.Panel1.Controls.Add(this.ClearFactionButton);
			this.SplitContainer.Panel1.Controls.Add(this.FactionTreeView);
			this.SplitContainer.Panel1.Controls.Add(this.HideMobWithFactionCheckBox);
			this.SplitContainer.Panel1.Controls.Add(this.AddSubfactionButton);
			this.SplitContainer.Panel1.Controls.Add(this.AddFactionButton);
			this.SplitContainer.Panel2.Controls.Add(this.checkBox1);
			this.SplitContainer.Panel2.Controls.Add(this.PropertyControl);
			resources.ApplyResources(this.ClearFactionButton, "ClearFactionButton");
			this.ClearFactionButton.ImageList = this.FactionEditorImageList;
			this.ClearFactionButton.Name = "ClearFactionButton";
			this.FactionEditorTooltip.SetToolTip(this.ClearFactionButton, resources.GetString("ClearFactionButton.ToolTip"));
			this.ClearFactionButton.UseVisualStyleBackColor = true;
			this.FactionEditorImageList.ImageStream = (global::System.Windows.Forms.ImageListStreamer)resources.GetObject("FactionEditorImageList.ImageStream");
			this.FactionEditorImageList.TransparentColor = global::System.Drawing.Color.Magenta;
			this.FactionEditorImageList.Images.SetKeyName(0, "add_faction.bmp");
			this.FactionEditorImageList.Images.SetKeyName(1, "add_subfaction.bmp");
			this.FactionEditorImageList.Images.SetKeyName(2, "mob_spawn_red.bmp");
			this.FactionEditorImageList.Images.SetKeyName(3, "clear_factions.bmp");
			this.FactionTreeView.AllowDrop = true;
			resources.ApplyResources(this.FactionTreeView, "FactionTreeView");
			this.FactionTreeView.ContextMenuStrip = this.FactionContextMenuStrip;
			this.FactionTreeView.HideSelection = false;
			this.FactionTreeView.LabelEdit = true;
			this.FactionTreeView.Name = "FactionTreeView";
			this.FactionTreeView.ShowNodeToolTips = true;
			this.FactionContextMenuStrip.Items.AddRange(new global::System.Windows.Forms.ToolStripItem[]
			{
				this.toolStripMenuItem1,
				this.newSubfactionToolStripMenuItem,
				this.toolStripSeparator1,
				this.clearFactionToolStripMenuItem,
				this.toolStripSeparator2,
				this.hideMobWithFactionToolStripMenuItem
			});
			this.FactionContextMenuStrip.Name = "FactionContextMenuStrip";
			resources.ApplyResources(this.FactionContextMenuStrip, "FactionContextMenuStrip");
			this.FactionContextMenuStrip.Tag = "";
			resources.ApplyResources(this.toolStripMenuItem1, "toolStripMenuItem1");
			this.toolStripMenuItem1.Name = "toolStripMenuItem1";
			this.toolStripMenuItem1.Tag = "toggle_new_faction";
			resources.ApplyResources(this.newSubfactionToolStripMenuItem, "newSubfactionToolStripMenuItem");
			this.newSubfactionToolStripMenuItem.Name = "newSubfactionToolStripMenuItem";
			this.newSubfactionToolStripMenuItem.Tag = "toggle_new_subfaction";
			this.toolStripSeparator1.Name = "toolStripSeparator1";
			resources.ApplyResources(this.toolStripSeparator1, "toolStripSeparator1");
			resources.ApplyResources(this.clearFactionToolStripMenuItem, "clearFactionToolStripMenuItem");
			this.clearFactionToolStripMenuItem.Name = "clearFactionToolStripMenuItem";
			this.clearFactionToolStripMenuItem.Tag = "toggle_clear_faction";
			this.toolStripSeparator2.Name = "toolStripSeparator2";
			resources.ApplyResources(this.toolStripSeparator2, "toolStripSeparator2");
			resources.ApplyResources(this.hideMobWithFactionToolStripMenuItem, "hideMobWithFactionToolStripMenuItem");
			this.hideMobWithFactionToolStripMenuItem.Name = "hideMobWithFactionToolStripMenuItem";
			this.hideMobWithFactionToolStripMenuItem.Tag = "toggle_hide_mob_with_faction";
			resources.ApplyResources(this.HideMobWithFactionCheckBox, "HideMobWithFactionCheckBox");
			this.HideMobWithFactionCheckBox.ImageList = this.FactionEditorImageList;
			this.HideMobWithFactionCheckBox.Name = "HideMobWithFactionCheckBox";
			this.FactionEditorTooltip.SetToolTip(this.HideMobWithFactionCheckBox, resources.GetString("HideMobWithFactionCheckBox.ToolTip"));
			this.HideMobWithFactionCheckBox.UseVisualStyleBackColor = true;
			resources.ApplyResources(this.AddSubfactionButton, "AddSubfactionButton");
			this.AddSubfactionButton.ImageList = this.FactionEditorImageList;
			this.AddSubfactionButton.Name = "AddSubfactionButton";
			this.FactionEditorTooltip.SetToolTip(this.AddSubfactionButton, resources.GetString("AddSubfactionButton.ToolTip"));
			this.AddSubfactionButton.UseVisualStyleBackColor = true;
			resources.ApplyResources(this.AddFactionButton, "AddFactionButton");
			this.AddFactionButton.ImageList = this.FactionEditorImageList;
			this.AddFactionButton.Name = "AddFactionButton";
			this.FactionEditorTooltip.SetToolTip(this.AddFactionButton, resources.GetString("AddFactionButton.ToolTip"));
			this.AddFactionButton.UseVisualStyleBackColor = true;
			this.PropertyControl.DefaultLocationFolder = "";
			resources.ApplyResources(this.PropertyControl, "PropertyControl");
			this.PropertyControl.Name = "PropertyControl";
			this.PropertyControl.SelectedObject = null;
			this.PropertyControl.SelectedObjects = new object[0];
			this.PropertyControl.SkipRefresh = false;
			this.PropertyControl.TitleControl = null;
			this.PropertyControl.TitleRelativeFrom = null;
			this.PropertyControl.TitleStart = "";
			resources.ApplyResources(this.FilterComboBox, "FilterComboBox");
			this.FilterComboBox.DropDownStyle = global::System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.FilterComboBox.FormattingEnabled = true;
			this.FilterComboBox.Name = "FilterComboBox";
			resources.ApplyResources(this.label1, "label1");
			this.label1.Name = "label1";
			resources.ApplyResources(this.FilterButton, "FilterButton");
			this.FilterButton.Name = "FilterButton";
			this.FilterButton.UseVisualStyleBackColor = true;
			resources.ApplyResources(this.ErrorItemList, "ErrorItemList");
			this.ErrorItemList.ForeColor = global::System.Drawing.Color.Red;
			this.ErrorItemList.FormattingEnabled = true;
			this.ErrorItemList.MultiColumn = true;
			this.ErrorItemList.Name = "ErrorItemList";
			this.ErrorItemList.Sorted = true;
			resources.ApplyResources(this.CloseButton, "CloseButton");
			this.CloseButton.Name = "CloseButton";
			this.CloseButton.UseVisualStyleBackColor = true;
			resources.ApplyResources(this.checkBox1, "checkBox1");
			this.checkBox1.Name = "checkBox1";
			this.checkBox1.UseVisualStyleBackColor = true;
			resources.ApplyResources(this, "$this");
			base.AutoScaleMode = global::System.Windows.Forms.AutoScaleMode.Font;
			base.Controls.Add(this.CloseButton);
			base.Controls.Add(this.ErrorItemList);
			base.Controls.Add(this.SplitContainer);
			base.Controls.Add(this.FilterButton);
			base.Controls.Add(this.label1);
			base.Controls.Add(this.FilterComboBox);
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.Name = "FactionEditorForm";
			base.ShowInTaskbar = false;
			this.SplitContainer.Panel1.ResumeLayout(false);
			this.SplitContainer.Panel2.ResumeLayout(false);
			this.SplitContainer.Panel2.PerformLayout();
			this.SplitContainer.ResumeLayout(false);
			this.FactionContextMenuStrip.ResumeLayout(false);
			base.ResumeLayout(false);
			base.PerformLayout();
		}

		// Token: 0x040009FC RID: 2556
		private global::System.ComponentModel.IContainer components;

		// Token: 0x040009FD RID: 2557
		private global::System.Windows.Forms.TreeView FactionTreeView;

		// Token: 0x040009FE RID: 2558
		private global::System.Windows.Forms.ComboBox FilterComboBox;

		// Token: 0x040009FF RID: 2559
		private global::System.Windows.Forms.Label label1;

		// Token: 0x04000A00 RID: 2560
		private global::System.Windows.Forms.Button FilterButton;

		// Token: 0x04000A01 RID: 2561
		private global::System.Windows.Forms.SplitContainer SplitContainer;

		// Token: 0x04000A02 RID: 2562
		private global::System.Windows.Forms.PropertyControl PropertyControl;

		// Token: 0x04000A03 RID: 2563
		private global::System.Windows.Forms.Button AddFactionButton;

		// Token: 0x04000A04 RID: 2564
		private global::System.Windows.Forms.Button AddSubfactionButton;

		// Token: 0x04000A05 RID: 2565
		private global::System.Windows.Forms.ListBox ErrorItemList;

		// Token: 0x04000A06 RID: 2566
		private global::System.Windows.Forms.CheckBox HideMobWithFactionCheckBox;

		// Token: 0x04000A07 RID: 2567
		private global::System.Windows.Forms.Button ClearFactionButton;

		// Token: 0x04000A08 RID: 2568
		private global::System.Windows.Forms.ImageList FactionEditorImageList;

		// Token: 0x04000A09 RID: 2569
		private global::System.Windows.Forms.ToolTip FactionEditorTooltip;

		// Token: 0x04000A0A RID: 2570
		private global::System.Windows.Forms.Button CloseButton;

		// Token: 0x04000A0B RID: 2571
		private global::System.Windows.Forms.ContextMenuStrip FactionContextMenuStrip;

		// Token: 0x04000A0C RID: 2572
		private global::System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;

		// Token: 0x04000A0D RID: 2573
		private global::System.Windows.Forms.ToolStripMenuItem newSubfactionToolStripMenuItem;

		// Token: 0x04000A0E RID: 2574
		private global::System.Windows.Forms.ToolStripSeparator toolStripSeparator1;

		// Token: 0x04000A0F RID: 2575
		private global::System.Windows.Forms.ToolStripMenuItem clearFactionToolStripMenuItem;

		// Token: 0x04000A10 RID: 2576
		private global::System.Windows.Forms.ToolStripSeparator toolStripSeparator2;

		// Token: 0x04000A11 RID: 2577
		private global::System.Windows.Forms.ToolStripMenuItem hideMobWithFactionToolStripMenuItem;

		// Token: 0x04000A12 RID: 2578
		private global::System.Windows.Forms.CheckBox checkBox1;
	}
}
