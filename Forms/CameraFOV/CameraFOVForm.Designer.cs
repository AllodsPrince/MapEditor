namespace MapEditor.Forms.CameraFOV
{
	// Token: 0x020000F7 RID: 247
	public partial class CameraFOVForm : global::System.Windows.Forms.Form
	{
		// Token: 0x06000C41 RID: 3137 RVA: 0x000697EF File Offset: 0x000687EF
		protected override void Dispose(bool disposing)
		{
			if (disposing && this.components != null)
			{
				this.components.Dispose();
			}
			base.Dispose(disposing);
		}

		// Token: 0x06000C42 RID: 3138 RVA: 0x00069810 File Offset: 0x00068810
		private void InitializeComponent()
		{
			global::System.ComponentModel.ComponentResourceManager resources = new global::System.ComponentModel.ComponentResourceManager(typeof(global::MapEditor.Forms.CameraFOV.CameraFOVForm));
			this.CameraFOVTrackBar = new global::System.Windows.Forms.TrackBar();
			this.CameraFOVLabel = new global::System.Windows.Forms.Label();
			((global::System.ComponentModel.ISupportInitialize)this.CameraFOVTrackBar).BeginInit();
			base.SuspendLayout();
			this.CameraFOVTrackBar.LargeChange = 1;
			resources.ApplyResources(this.CameraFOVTrackBar, "CameraFOVTrackBar");
			this.CameraFOVTrackBar.Maximum = 12;
			this.CameraFOVTrackBar.Minimum = 3;
			this.CameraFOVTrackBar.Name = "CameraFOVTrackBar";
			this.CameraFOVTrackBar.Value = 9;
			this.CameraFOVTrackBar.Scroll += new global::System.EventHandler(this.CameraFOVTrackBar_Scroll);
			resources.ApplyResources(this.CameraFOVLabel, "CameraFOVLabel");
			this.CameraFOVLabel.Name = "CameraFOVLabel";
			resources.ApplyResources(this, "$this");
			base.AutoScaleMode = global::System.Windows.Forms.AutoScaleMode.Font;
			base.ControlBox = false;
			base.Controls.Add(this.CameraFOVLabel);
			base.Controls.Add(this.CameraFOVTrackBar);
			base.FormBorderStyle = global::System.Windows.Forms.FormBorderStyle.FixedToolWindow;
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.Name = "CameraFOVForm";
			base.ShowIcon = false;
			base.ShowInTaskbar = false;
			base.SizeGripStyle = global::System.Windows.Forms.SizeGripStyle.Hide;
			base.TopMost = true;
			base.Deactivate += new global::System.EventHandler(this.CameraFOVForm_Deactivate);
			((global::System.ComponentModel.ISupportInitialize)this.CameraFOVTrackBar).EndInit();
			base.ResumeLayout(false);
			base.PerformLayout();
		}

		// Token: 0x04000A1A RID: 2586
		private readonly global::System.ComponentModel.IContainer components;

		// Token: 0x04000A1B RID: 2587
		private global::System.Windows.Forms.TrackBar CameraFOVTrackBar;

		// Token: 0x04000A1C RID: 2588
		private global::System.Windows.Forms.Label CameraFOVLabel;
	}
}
