namespace MapEditor.Forms.Notification
{
	// Token: 0x0200018B RID: 395
	public partial class NotificationForm : global::System.Windows.Forms.Form
	{
		// Token: 0x060012DE RID: 4830 RVA: 0x00089C22 File Offset: 0x00088C22
		protected override void Dispose(bool disposing)
		{
			if (disposing && this.components != null)
			{
				this.components.Dispose();
			}
			base.Dispose(disposing);
		}

		// Token: 0x060012DF RID: 4831 RVA: 0x00089C44 File Offset: 0x00088C44
		private void InitializeComponent()
		{
			this.label1 = new global::System.Windows.Forms.Label();
			base.SuspendLayout();
			this.label1.AutoSize = true;
			this.label1.ForeColor = global::System.Drawing.Color.Maroon;
			this.label1.Location = new global::System.Drawing.Point(23, 25);
			this.label1.Name = "label1";
			this.label1.Size = new global::System.Drawing.Size(234, 13);
			this.label1.TabIndex = 0;
			this.label1.Text = "Осторожно, более 30 минут без сохранения!";
			base.AutoScaleDimensions = new global::System.Drawing.SizeF(6f, 13f);
			base.AutoScaleMode = global::System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = global::System.Drawing.Color.FromArgb(255, 224, 192);
			base.ClientSize = new global::System.Drawing.Size(282, 62);
			base.Controls.Add(this.label1);
			base.FormBorderStyle = global::System.Windows.Forms.FormBorderStyle.None;
			base.Name = "NotificationForm";
			base.Opacity = 0.4;
			base.StartPosition = global::System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "NotificationForm";
			base.ResumeLayout(false);
			base.PerformLayout();
		}

		// Token: 0x04000D80 RID: 3456
		private global::System.ComponentModel.IContainer components;

		// Token: 0x04000D81 RID: 3457
		private global::System.Windows.Forms.Label label1;
	}
}
