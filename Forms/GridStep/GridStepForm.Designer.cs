namespace MapEditor.Forms.GridStep
{
	// Token: 0x02000113 RID: 275
	public partial class GridStepForm : global::System.Windows.Forms.Form
	{
		// Token: 0x06000DA2 RID: 3490 RVA: 0x00072DED File Offset: 0x00071DED
		protected override void Dispose(bool disposing)
		{
			if (disposing && this.components != null)
			{
				this.components.Dispose();
			}
			base.Dispose(disposing);
		}

		// Token: 0x06000DA3 RID: 3491 RVA: 0x00072E0C File Offset: 0x00071E0C
		private void InitializeComponent()
		{
			this.GridStepTrackBar = new global::System.Windows.Forms.TrackBar();
			this.GridStepLabel = new global::System.Windows.Forms.Label();
			((global::System.ComponentModel.ISupportInitialize)this.GridStepTrackBar).BeginInit();
			base.SuspendLayout();
			this.GridStepTrackBar.ImeMode = global::System.Windows.Forms.ImeMode.NoControl;
			this.GridStepTrackBar.LargeChange = 1;
			this.GridStepTrackBar.Location = new global::System.Drawing.Point(10, 12);
			this.GridStepTrackBar.Maximum = 8;
			this.GridStepTrackBar.Name = "GridStepTrackBar";
			this.GridStepTrackBar.Size = new global::System.Drawing.Size(303, 42);
			this.GridStepTrackBar.TabIndex = 0;
			this.GridStepTrackBar.Value = 4;
			this.GridStepTrackBar.Scroll += new global::System.EventHandler(this.GridStepTrackBar_Scroll);
			this.GridStepLabel.AutoSize = true;
			this.GridStepLabel.ImeMode = global::System.Windows.Forms.ImeMode.NoControl;
			this.GridStepLabel.Location = new global::System.Drawing.Point(10, 46);
			this.GridStepLabel.Name = "GridStepLabel";
			this.GridStepLabel.Size = new global::System.Drawing.Size(300, 13);
			this.GridStepLabel.TabIndex = 1;
			this.GridStepLabel.Text = "1/16    1/8      1/4      1/2        1         2          4         8         16";
			base.AutoScaleDimensions = new global::System.Drawing.SizeF(6f, 13f);
			base.AutoScaleMode = global::System.Windows.Forms.AutoScaleMode.Font;
			base.ClientSize = new global::System.Drawing.Size(322, 68);
			base.ControlBox = false;
			base.Controls.Add(this.GridStepLabel);
			base.Controls.Add(this.GridStepTrackBar);
			base.FormBorderStyle = global::System.Windows.Forms.FormBorderStyle.FixedToolWindow;
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.Name = "GridStepForm";
			base.ShowIcon = false;
			base.ShowInTaskbar = false;
			base.SizeGripStyle = global::System.Windows.Forms.SizeGripStyle.Hide;
			this.Text = "Grid Step";
			base.TopMost = true;
			base.Deactivate += new global::System.EventHandler(this.GridStepForm_Deactivate);
			((global::System.ComponentModel.ISupportInitialize)this.GridStepTrackBar).EndInit();
			base.ResumeLayout(false);
			base.PerformLayout();
		}

		// Token: 0x04000AF6 RID: 2806
		private global::System.ComponentModel.IContainer components;

		// Token: 0x04000AF7 RID: 2807
		private global::System.Windows.Forms.TrackBar GridStepTrackBar;

		// Token: 0x04000AF8 RID: 2808
		private global::System.Windows.Forms.Label GridStepLabel;
	}
}
