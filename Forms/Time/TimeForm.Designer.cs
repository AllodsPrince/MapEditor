namespace MapEditor.Forms.Time
{
	// Token: 0x0200023D RID: 573
	public partial class TimeForm : global::System.Windows.Forms.Form
	{
		// Token: 0x06001B5D RID: 7005 RVA: 0x000B1427 File Offset: 0x000B0427
		protected override void Dispose(bool disposing)
		{
			if (disposing && this.components != null)
			{
				this.components.Dispose();
			}
			base.Dispose(disposing);
		}

		// Token: 0x06001B5E RID: 7006 RVA: 0x000B1448 File Offset: 0x000B0448
		private void InitializeComponent()
		{
			global::System.ComponentModel.ComponentResourceManager resources = new global::System.ComponentModel.ComponentResourceManager(typeof(global::MapEditor.Forms.Time.TimeForm));
			this.TimeTrackBar = new global::System.Windows.Forms.TrackBar();
			this.label1 = new global::System.Windows.Forms.Label();
			this.label2 = new global::System.Windows.Forms.Label();
			this.label3 = new global::System.Windows.Forms.Label();
			this.label4 = new global::System.Windows.Forms.Label();
			this.label5 = new global::System.Windows.Forms.Label();
			this.label6 = new global::System.Windows.Forms.Label();
			this.label7 = new global::System.Windows.Forms.Label();
			this.label8 = new global::System.Windows.Forms.Label();
			this.label9 = new global::System.Windows.Forms.Label();
			this.label10 = new global::System.Windows.Forms.Label();
			this.label11 = new global::System.Windows.Forms.Label();
			this.label12 = new global::System.Windows.Forms.Label();
			this.label13 = new global::System.Windows.Forms.Label();
			((global::System.ComponentModel.ISupportInitialize)this.TimeTrackBar).BeginInit();
			base.SuspendLayout();
			this.TimeTrackBar.LargeChange = 1;
			resources.ApplyResources(this.TimeTrackBar, "TimeTrackBar");
			this.TimeTrackBar.Maximum = 96;
			this.TimeTrackBar.Name = "TimeTrackBar";
			this.TimeTrackBar.TickFrequency = 4;
			this.TimeTrackBar.Value = 9;
			this.TimeTrackBar.Scroll += new global::System.EventHandler(this.TimeTrackBar_Scroll);
			resources.ApplyResources(this.label1, "label1");
			this.label1.Name = "label1";
			resources.ApplyResources(this.label2, "label2");
			this.label2.Name = "label2";
			resources.ApplyResources(this.label3, "label3");
			this.label3.Name = "label3";
			resources.ApplyResources(this.label4, "label4");
			this.label4.Name = "label4";
			resources.ApplyResources(this.label5, "label5");
			this.label5.Name = "label5";
			resources.ApplyResources(this.label6, "label6");
			this.label6.Name = "label6";
			resources.ApplyResources(this.label7, "label7");
			this.label7.Name = "label7";
			resources.ApplyResources(this.label8, "label8");
			this.label8.Name = "label8";
			resources.ApplyResources(this.label9, "label9");
			this.label9.Name = "label9";
			resources.ApplyResources(this.label10, "label10");
			this.label10.Name = "label10";
			resources.ApplyResources(this.label11, "label11");
			this.label11.Name = "label11";
			resources.ApplyResources(this.label12, "label12");
			this.label12.Name = "label12";
			resources.ApplyResources(this.label13, "label13");
			this.label13.Name = "label13";
			resources.ApplyResources(this, "$this");
			base.AutoScaleMode = global::System.Windows.Forms.AutoScaleMode.Font;
			base.ControlBox = false;
			base.Controls.Add(this.label13);
			base.Controls.Add(this.label12);
			base.Controls.Add(this.label11);
			base.Controls.Add(this.label10);
			base.Controls.Add(this.label9);
			base.Controls.Add(this.label8);
			base.Controls.Add(this.label7);
			base.Controls.Add(this.label6);
			base.Controls.Add(this.label5);
			base.Controls.Add(this.label4);
			base.Controls.Add(this.label3);
			base.Controls.Add(this.label2);
			base.Controls.Add(this.label1);
			base.Controls.Add(this.TimeTrackBar);
			base.FormBorderStyle = global::System.Windows.Forms.FormBorderStyle.FixedToolWindow;
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.Name = "TimeForm";
			base.ShowIcon = false;
			base.ShowInTaskbar = false;
			base.SizeGripStyle = global::System.Windows.Forms.SizeGripStyle.Hide;
			base.TopMost = true;
			base.Deactivate += new global::System.EventHandler(this.TimeForm_Deactivate);
			((global::System.ComponentModel.ISupportInitialize)this.TimeTrackBar).EndInit();
			base.ResumeLayout(false);
			base.PerformLayout();
		}

		// Token: 0x04001170 RID: 4464
		private global::System.ComponentModel.IContainer components;

		// Token: 0x04001171 RID: 4465
		private global::System.Windows.Forms.TrackBar TimeTrackBar;

		// Token: 0x04001172 RID: 4466
		private global::System.Windows.Forms.Label label1;

		// Token: 0x04001173 RID: 4467
		private global::System.Windows.Forms.Label label2;

		// Token: 0x04001174 RID: 4468
		private global::System.Windows.Forms.Label label3;

		// Token: 0x04001175 RID: 4469
		private global::System.Windows.Forms.Label label4;

		// Token: 0x04001176 RID: 4470
		private global::System.Windows.Forms.Label label5;

		// Token: 0x04001177 RID: 4471
		private global::System.Windows.Forms.Label label6;

		// Token: 0x04001178 RID: 4472
		private global::System.Windows.Forms.Label label7;

		// Token: 0x04001179 RID: 4473
		private global::System.Windows.Forms.Label label8;

		// Token: 0x0400117A RID: 4474
		private global::System.Windows.Forms.Label label9;

		// Token: 0x0400117B RID: 4475
		private global::System.Windows.Forms.Label label10;

		// Token: 0x0400117C RID: 4476
		private global::System.Windows.Forms.Label label11;

		// Token: 0x0400117D RID: 4477
		private global::System.Windows.Forms.Label label12;

		// Token: 0x0400117E RID: 4478
		private global::System.Windows.Forms.Label label13;
	}
}
