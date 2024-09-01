namespace MapEditor.Forms.Minimap
{
	// Token: 0x02000164 RID: 356
	public partial class MinimapForm : global::MapEditor.Forms.Base.BaseForm
	{
		// Token: 0x06001166 RID: 4454 RVA: 0x00080F98 File Offset: 0x0007FF98
		protected override void Dispose(bool disposing)
		{
			if (disposing && this.components != null)
			{
				this.components.Dispose();
			}
			base.Dispose(disposing);
		}

		// Token: 0x06001167 RID: 4455 RVA: 0x00080FB8 File Offset: 0x0007FFB8
		private void InitializeComponent()
		{
			this.components = new global::System.ComponentModel.Container();
			global::System.ComponentModel.ComponentResourceManager resources = new global::System.ComponentModel.ComponentResourceManager(typeof(global::MapEditor.Forms.Minimap.MinimapForm));
			this.MinimapToolStrip = new global::System.Windows.Forms.ToolStrip();
			this.PatchGridButton = new global::System.Windows.Forms.ToolStripButton();
			this.TerrainRegionsButton = new global::System.Windows.Forms.ToolStripButton();
			this.MinimapToolStripSeparator00 = new global::System.Windows.Forms.ToolStripSeparator();
			this.TerrainMMButton = new global::System.Windows.Forms.ToolStripButton();
			this.BottomTerrainMMButton = new global::System.Windows.Forms.ToolStripButton();
			this.HeightMMButton = new global::System.Windows.Forms.ToolStripButton();
			this.BottomHeightMMButton = new global::System.Windows.Forms.ToolStripButton();
			this.ZoneMMButton = new global::System.Windows.Forms.ToolStripButton();
			this.LightMMButton = new global::System.Windows.Forms.ToolStripButton();
			this.AmbientMMButton = new global::System.Windows.Forms.ToolStripButton();
			this.MusicMMButton = new global::System.Windows.Forms.ToolStripButton();
			this.ImagePanel = new global::System.Windows.Forms.Panel();
			this.contextMenuStrip = new global::System.Windows.Forms.ContextMenuStrip(this.components);
			this.blockPatchToolStripMenuItem = new global::System.Windows.Forms.ToolStripMenuItem();
			this.unblockPatchToolStripMenuItem = new global::System.Windows.Forms.ToolStripMenuItem();
			this.MinimapToolStrip.SuspendLayout();
			this.contextMenuStrip.SuspendLayout();
			base.SuspendLayout();
			this.MinimapToolStrip.Items.AddRange(new global::System.Windows.Forms.ToolStripItem[]
			{
				this.PatchGridButton,
				this.TerrainRegionsButton,
				this.MinimapToolStripSeparator00,
				this.TerrainMMButton,
				this.BottomTerrainMMButton,
				this.HeightMMButton,
				this.BottomHeightMMButton,
				this.ZoneMMButton,
				this.LightMMButton,
				this.AmbientMMButton,
				this.MusicMMButton
			});
			resources.ApplyResources(this.MinimapToolStrip, "MinimapToolStrip");
			this.MinimapToolStrip.Name = "MinimapToolStrip";
			this.PatchGridButton.DisplayStyle = global::System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.PatchGridButton.Image = global::MapEditor.Properties.Resources.minimap_grid;
			resources.ApplyResources(this.PatchGridButton, "PatchGridButton");
			this.PatchGridButton.Name = "PatchGridButton";
			this.TerrainRegionsButton.DisplayStyle = global::System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.TerrainRegionsButton.Image = global::MapEditor.Properties.Resources.minimap_regions;
			resources.ApplyResources(this.TerrainRegionsButton, "TerrainRegionsButton");
			this.TerrainRegionsButton.Name = "TerrainRegionsButton";
			this.MinimapToolStripSeparator00.Name = "MinimapToolStripSeparator00";
			resources.ApplyResources(this.MinimapToolStripSeparator00, "MinimapToolStripSeparator00");
			this.TerrainMMButton.DisplayStyle = global::System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.TerrainMMButton.Image = global::MapEditor.Properties.Resources.minimap_terrain;
			resources.ApplyResources(this.TerrainMMButton, "TerrainMMButton");
			this.TerrainMMButton.Name = "TerrainMMButton";
			this.BottomTerrainMMButton.DisplayStyle = global::System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.BottomTerrainMMButton.Image = global::MapEditor.Properties.Resources.minimap_bottom;
			resources.ApplyResources(this.BottomTerrainMMButton, "BottomTerrainMMButton");
			this.BottomTerrainMMButton.Name = "BottomTerrainMMButton";
			this.HeightMMButton.DisplayStyle = global::System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.HeightMMButton.Image = global::MapEditor.Properties.Resources.minimap_terrain_heightmap;
			resources.ApplyResources(this.HeightMMButton, "HeightMMButton");
			this.HeightMMButton.Name = "HeightMMButton";
			this.BottomHeightMMButton.DisplayStyle = global::System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.BottomHeightMMButton.Image = global::MapEditor.Properties.Resources.minimap_bottom_heightmap;
			resources.ApplyResources(this.BottomHeightMMButton, "BottomHeightMMButton");
			this.BottomHeightMMButton.Name = "BottomHeightMMButton";
			this.ZoneMMButton.DisplayStyle = global::System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.ZoneMMButton.Image = global::MapEditor.Properties.Resources.minimap_zones;
			resources.ApplyResources(this.ZoneMMButton, "ZoneMMButton");
			this.ZoneMMButton.Name = "ZoneMMButton";
			this.LightMMButton.DisplayStyle = global::System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.LightMMButton.Image = global::MapEditor.Properties.Resources.minimap_lights;
			resources.ApplyResources(this.LightMMButton, "LightMMButton");
			this.LightMMButton.Name = "LightMMButton";
			this.AmbientMMButton.DisplayStyle = global::System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.AmbientMMButton.Image = global::MapEditor.Properties.Resources.minimap_ambient;
			resources.ApplyResources(this.AmbientMMButton, "AmbientMMButton");
			this.AmbientMMButton.Name = "AmbientMMButton";
			this.MusicMMButton.DisplayStyle = global::System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.MusicMMButton.Image = global::MapEditor.Properties.Resources.minimap_music;
			resources.ApplyResources(this.MusicMMButton, "MusicMMButton");
			this.MusicMMButton.Name = "MusicMMButton";
			resources.ApplyResources(this.ImagePanel, "ImagePanel");
			this.ImagePanel.Name = "ImagePanel";
			this.contextMenuStrip.Items.AddRange(new global::System.Windows.Forms.ToolStripItem[]
			{
				this.blockPatchToolStripMenuItem,
				this.unblockPatchToolStripMenuItem
			});
			this.contextMenuStrip.Name = "contextMenuStrip";
			resources.ApplyResources(this.contextMenuStrip, "contextMenuStrip");
			this.blockPatchToolStripMenuItem.Name = "blockPatchToolStripMenuItem";
			resources.ApplyResources(this.blockPatchToolStripMenuItem, "blockPatchToolStripMenuItem");
			this.unblockPatchToolStripMenuItem.Name = "unblockPatchToolStripMenuItem";
			resources.ApplyResources(this.unblockPatchToolStripMenuItem, "unblockPatchToolStripMenuItem");
			resources.ApplyResources(this, "$this");
			base.AutoScaleMode = global::System.Windows.Forms.AutoScaleMode.Font;
			base.Controls.Add(this.ImagePanel);
			base.Controls.Add(this.MinimapToolStrip);
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.Name = "MinimapForm";
			base.ShowInTaskbar = false;
			this.MinimapToolStrip.ResumeLayout(false);
			this.MinimapToolStrip.PerformLayout();
			this.contextMenuStrip.ResumeLayout(false);
			base.ResumeLayout(false);
			base.PerformLayout();
		}

		// Token: 0x04000C73 RID: 3187
		private global::System.ComponentModel.IContainer components;

		// Token: 0x04000C74 RID: 3188
		private global::System.Windows.Forms.ToolStrip MinimapToolStrip;

		// Token: 0x04000C75 RID: 3189
		private global::System.Windows.Forms.ToolStripButton PatchGridButton;

		// Token: 0x04000C76 RID: 3190
		private global::System.Windows.Forms.Panel ImagePanel;

		// Token: 0x04000C77 RID: 3191
		private global::System.Windows.Forms.ToolStripButton TerrainMMButton;

		// Token: 0x04000C78 RID: 3192
		private global::System.Windows.Forms.ToolStripButton HeightMMButton;

		// Token: 0x04000C79 RID: 3193
		private global::System.Windows.Forms.ToolStripButton BottomHeightMMButton;

		// Token: 0x04000C7A RID: 3194
		private global::System.Windows.Forms.ToolStripButton BottomTerrainMMButton;

		// Token: 0x04000C7B RID: 3195
		private global::System.Windows.Forms.ToolStripButton TerrainRegionsButton;

		// Token: 0x04000C7C RID: 3196
		private global::System.Windows.Forms.ToolStripSeparator MinimapToolStripSeparator00;

		// Token: 0x04000C7D RID: 3197
		private global::System.Windows.Forms.ToolStripButton ZoneMMButton;

		// Token: 0x04000C7E RID: 3198
		private global::System.Windows.Forms.ToolStripButton LightMMButton;

		// Token: 0x04000C7F RID: 3199
		private global::System.Windows.Forms.ToolStripButton AmbientMMButton;

		// Token: 0x04000C80 RID: 3200
		private global::System.Windows.Forms.ToolStripButton MusicMMButton;

		// Token: 0x04000C81 RID: 3201
		private global::System.Windows.Forms.ContextMenuStrip contextMenuStrip;

		// Token: 0x04000C82 RID: 3202
		private global::System.Windows.Forms.ToolStripMenuItem blockPatchToolStripMenuItem;

		// Token: 0x04000C83 RID: 3203
		private global::System.Windows.Forms.ToolStripMenuItem unblockPatchToolStripMenuItem;
	}
}
