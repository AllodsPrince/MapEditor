namespace MapEditor.Forms.CameraSpeed
{
	// Token: 0x02000116 RID: 278
	public partial class CameraSpeedForm : global::System.Windows.Forms.Form
	{
		// Token: 0x06000DBB RID: 3515 RVA: 0x00073313 File Offset: 0x00072313
		protected override void Dispose(bool disposing)
		{
			if (disposing && this.components != null)
			{
				this.components.Dispose();
			}
			base.Dispose(disposing);
		}

		// Token: 0x06000DBC RID: 3516 RVA: 0x00073334 File Offset: 0x00072334
		private void InitializeComponent()
		{
			global::System.ComponentModel.ComponentResourceManager resources = new global::System.ComponentModel.ComponentResourceManager(typeof(global::MapEditor.Forms.CameraSpeed.CameraSpeedForm));
			this.CameraSpeedTrackBar = new global::System.Windows.Forms.TrackBar();
			this.CameraSpeedLabel = new global::System.Windows.Forms.Label();
			((global::System.ComponentModel.ISupportInitialize)this.CameraSpeedTrackBar).BeginInit();
			base.SuspendLayout();
			resources.ApplyResources(this.CameraSpeedTrackBar, "CameraSpeedTrackBar");
			this.CameraSpeedTrackBar.LargeChange = 25;
			this.CameraSpeedTrackBar.Maximum = 250;
			this.CameraSpeedTrackBar.Name = "CameraSpeedTrackBar";
			this.CameraSpeedTrackBar.TickFrequency = 25;
			this.CameraSpeedTrackBar.Value = 50;
			this.CameraSpeedTrackBar.Scroll += new global::System.EventHandler(this.CameraSpeedTrackBar_Scroll);
			resources.ApplyResources(this.CameraSpeedLabel, "CameraSpeedLabel");
			this.CameraSpeedLabel.Name = "CameraSpeedLabel";
			resources.ApplyResources(this, "$this");
			base.AutoScaleMode = global::System.Windows.Forms.AutoScaleMode.Font;
			base.ControlBox = false;
			base.Controls.Add(this.CameraSpeedLabel);
			base.Controls.Add(this.CameraSpeedTrackBar);
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.Name = "CameraSpeedForm";
			base.ShowIcon = false;
			base.ShowInTaskbar = false;
			base.SizeGripStyle = global::System.Windows.Forms.SizeGripStyle.Hide;
			base.TopMost = true;
			base.Deactivate += new global::System.EventHandler(this.CameraSpeedForm_Deactivate);
			((global::System.ComponentModel.ISupportInitialize)this.CameraSpeedTrackBar).EndInit();
			base.ResumeLayout(false);
			base.PerformLayout();
		}

		// Token: 0x04000AFE RID: 2814
		private global::System.ComponentModel.IContainer components;

		// Token: 0x04000AFF RID: 2815
		private global::System.Windows.Forms.TrackBar CameraSpeedTrackBar;

		// Token: 0x04000B00 RID: 2816
		private global::System.Windows.Forms.Label CameraSpeedLabel;
	}
}
