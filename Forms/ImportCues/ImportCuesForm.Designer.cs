namespace MapEditor.Forms.ImportCues
{
	// Token: 0x0200007A RID: 122
	public partial class ImportCuesForm : global::MapEditor.Forms.Base.BaseForm
	{
		// Token: 0x060005F9 RID: 1529 RVA: 0x00032CFC File Offset: 0x00031CFC
		protected override void Dispose(bool disposing)
		{
			if (disposing && this.components != null)
			{
				this.components.Dispose();
			}
			base.Dispose(disposing);
		}

		// Token: 0x060005FA RID: 1530 RVA: 0x00032D1C File Offset: 0x00031D1C
		private void InitializeComponent()
		{
			this.ResultTextBox = new global::System.Windows.Forms.RichTextBox();
			this.ImportButton = new global::System.Windows.Forms.Button();
			this.BrowseButton = new global::System.Windows.Forms.Button();
			this.FilePathTextBox = new global::System.Windows.Forms.TextBox();
			base.SuspendLayout();
			this.ResultTextBox.Anchor = (global::System.Windows.Forms.AnchorStyles.Top | global::System.Windows.Forms.AnchorStyles.Bottom | global::System.Windows.Forms.AnchorStyles.Left | global::System.Windows.Forms.AnchorStyles.Right);
			this.ResultTextBox.BackColor = global::System.Drawing.SystemColors.InactiveBorder;
			this.ResultTextBox.Location = new global::System.Drawing.Point(63, 44);
			this.ResultTextBox.Name = "ResultTextBox";
			this.ResultTextBox.ReadOnly = true;
			this.ResultTextBox.Size = new global::System.Drawing.Size(400, 324);
			this.ResultTextBox.TabIndex = 7;
			this.ResultTextBox.Text = "";
			this.ImportButton.ImeMode = global::System.Windows.Forms.ImeMode.NoControl;
			this.ImportButton.Location = new global::System.Drawing.Point(0, 44);
			this.ImportButton.Name = "ImportButton";
			this.ImportButton.Size = new global::System.Drawing.Size(57, 24);
			this.ImportButton.TabIndex = 6;
			this.ImportButton.Text = "Import";
			this.ImportButton.UseVisualStyleBackColor = true;
			this.BrowseButton.ImeMode = global::System.Windows.Forms.ImeMode.NoControl;
			this.BrowseButton.Location = new global::System.Drawing.Point(0, 0);
			this.BrowseButton.Name = "BrowseButton";
			this.BrowseButton.Size = new global::System.Drawing.Size(57, 24);
			this.BrowseButton.TabIndex = 5;
			this.BrowseButton.Text = "Browse";
			this.BrowseButton.UseVisualStyleBackColor = true;
			this.FilePathTextBox.Anchor = (global::System.Windows.Forms.AnchorStyles.Top | global::System.Windows.Forms.AnchorStyles.Left | global::System.Windows.Forms.AnchorStyles.Right);
			this.FilePathTextBox.Location = new global::System.Drawing.Point(63, 3);
			this.FilePathTextBox.Name = "FilePathTextBox";
			this.FilePathTextBox.Size = new global::System.Drawing.Size(400, 20);
			this.FilePathTextBox.TabIndex = 4;
			base.AutoScaleDimensions = new global::System.Drawing.SizeF(6f, 13f);
			base.AutoScaleMode = global::System.Windows.Forms.AutoScaleMode.Font;
			base.ClientSize = new global::System.Drawing.Size(466, 375);
			base.Controls.Add(this.ResultTextBox);
			base.Controls.Add(this.ImportButton);
			base.Controls.Add(this.BrowseButton);
			base.Controls.Add(this.FilePathTextBox);
			base.Name = "ImportCuesForm";
			this.Text = "Import Cues";
			base.ResumeLayout(false);
			base.PerformLayout();
		}

		// Token: 0x04000487 RID: 1159
		private global::System.ComponentModel.IContainer components;

		// Token: 0x04000488 RID: 1160
		private global::System.Windows.Forms.RichTextBox ResultTextBox;

		// Token: 0x04000489 RID: 1161
		private global::System.Windows.Forms.Button ImportButton;

		// Token: 0x0400048A RID: 1162
		private global::System.Windows.Forms.Button BrowseButton;

		// Token: 0x0400048B RID: 1163
		private global::System.Windows.Forms.TextBox FilePathTextBox;
	}
}
