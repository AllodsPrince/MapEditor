namespace MapEditor
{
	// Token: 0x02000059 RID: 89
	public partial class SplashScreenForm : global::System.Windows.Forms.Form
	{
		// Token: 0x0600047D RID: 1149 RVA: 0x0002487D File Offset: 0x0002387D
		protected override void Dispose(bool disposing)
		{
			if (disposing && this.components != null)
			{
				this.components.Dispose();
			}
			base.Dispose(disposing);
		}

		// Token: 0x0600047E RID: 1150 RVA: 0x0002489C File Offset: 0x0002389C
		private void InitializeComponent()
		{
			base.SuspendLayout();
			base.AutoScaleDimensions = new global::System.Drawing.SizeF(6f, 13f);
			base.AutoScaleMode = global::System.Windows.Forms.AutoScaleMode.Font;
			this.BackgroundImage = global::MapEditor.Properties.Resources.splash_screen;
			this.BackgroundImageLayout = global::System.Windows.Forms.ImageLayout.Center;
			base.ClientSize = new global::System.Drawing.Size(350, 299);
			base.ControlBox = false;
			base.FormBorderStyle = global::System.Windows.Forms.FormBorderStyle.None;
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.Name = "SplashScreenForm";
			base.ShowIcon = false;
			base.ShowInTaskbar = false;
			base.StartPosition = global::System.Windows.Forms.FormStartPosition.CenterScreen;
			base.ResumeLayout(false);
		}

		// Token: 0x04000312 RID: 786
		private global::System.ComponentModel.IContainer components;
	}
}
