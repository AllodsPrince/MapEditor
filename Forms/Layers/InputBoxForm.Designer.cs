namespace MapEditor.Forms.Layers
{
	// Token: 0x020000EE RID: 238
	public partial class InputBoxForm : global::System.Windows.Forms.Form
	{
		// Token: 0x06000C02 RID: 3074 RVA: 0x00067988 File Offset: 0x00066988
		protected override void Dispose(bool disposing)
		{
			if (disposing && this.components != null)
			{
				this.components.Dispose();
			}
			base.Dispose(disposing);
		}

		// Token: 0x06000C03 RID: 3075 RVA: 0x000679A8 File Offset: 0x000669A8
		private void InitializeComponent()
		{
			this.InputTextBox = new global::System.Windows.Forms.TextBox();
			this.OkInputButton = new global::System.Windows.Forms.Button();
			this.CancelInputButton = new global::System.Windows.Forms.Button();
			base.SuspendLayout();
			this.InputTextBox.Location = new global::System.Drawing.Point(12, 26);
			this.InputTextBox.Name = "InputTextBox";
			this.InputTextBox.Size = new global::System.Drawing.Size(272, 20);
			this.InputTextBox.TabIndex = 0;
			this.OkInputButton.Location = new global::System.Drawing.Point(52, 72);
			this.OkInputButton.Name = "OkInputButton";
			this.OkInputButton.Size = new global::System.Drawing.Size(85, 25);
			this.OkInputButton.TabIndex = 1;
			this.OkInputButton.Text = "Ok";
			this.OkInputButton.UseVisualStyleBackColor = true;
			this.CancelInputButton.Location = new global::System.Drawing.Point(156, 72);
			this.CancelInputButton.Name = "CancelInputButton";
			this.CancelInputButton.Size = new global::System.Drawing.Size(85, 25);
			this.CancelInputButton.TabIndex = 2;
			this.CancelInputButton.Text = "Cancel";
			this.CancelInputButton.UseVisualStyleBackColor = true;
			base.AutoScaleDimensions = new global::System.Drawing.SizeF(6f, 13f);
			base.AutoScaleMode = global::System.Windows.Forms.AutoScaleMode.Font;
			base.ClientSize = new global::System.Drawing.Size(292, 123);
			base.Controls.Add(this.CancelInputButton);
			base.Controls.Add(this.OkInputButton);
			base.Controls.Add(this.InputTextBox);
			base.MaximizeBox = false;
			this.MaximumSize = new global::System.Drawing.Size(300, 150);
			base.MinimizeBox = false;
			this.MinimumSize = new global::System.Drawing.Size(300, 150);
			base.Name = "InputBoxForm";
			base.ShowIcon = false;
			base.ShowInTaskbar = false;
			base.StartPosition = global::System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "InputBoxForm";
			base.ResumeLayout(false);
			base.PerformLayout();
		}

		// Token: 0x040009F0 RID: 2544
		private global::System.ComponentModel.IContainer components;

		// Token: 0x040009F1 RID: 2545
		private global::System.Windows.Forms.TextBox InputTextBox;

		// Token: 0x040009F2 RID: 2546
		private global::System.Windows.Forms.Button OkInputButton;

		// Token: 0x040009F3 RID: 2547
		private global::System.Windows.Forms.Button CancelInputButton;
	}
}
