namespace MapEditor.Forms.Quests
{
	// Token: 0x020000AC RID: 172
	public partial class SelectRankDialogForm : global::System.Windows.Forms.Form
	{
		// Token: 0x060007F8 RID: 2040 RVA: 0x0003E965 File Offset: 0x0003D965
		protected override void Dispose(bool disposing)
		{
			if (disposing && this.components != null)
			{
				this.components.Dispose();
			}
			base.Dispose(disposing);
		}

		// Token: 0x060007F9 RID: 2041 RVA: 0x0003E984 File Offset: 0x0003D984
		private void InitializeComponent()
		{
			this.listBox = new global::System.Windows.Forms.ListBox();
			this.button1 = new global::System.Windows.Forms.Button();
			this.okButton = new global::System.Windows.Forms.Button();
			base.SuspendLayout();
			this.listBox.FormattingEnabled = true;
			this.listBox.Location = new global::System.Drawing.Point(0, 0);
			this.listBox.Name = "listBox";
			this.listBox.Size = new global::System.Drawing.Size(357, 238);
			this.listBox.TabIndex = 0;
			this.button1.DialogResult = global::System.Windows.Forms.DialogResult.Cancel;
			this.button1.Location = new global::System.Drawing.Point(282, 244);
			this.button1.Name = "button1";
			this.button1.Size = new global::System.Drawing.Size(75, 23);
			this.button1.TabIndex = 1;
			this.button1.Text = "Cancel";
			this.button1.UseVisualStyleBackColor = true;
			this.okButton.DialogResult = global::System.Windows.Forms.DialogResult.OK;
			this.okButton.Enabled = false;
			this.okButton.Location = new global::System.Drawing.Point(201, 244);
			this.okButton.Name = "okButton";
			this.okButton.Size = new global::System.Drawing.Size(75, 23);
			this.okButton.TabIndex = 2;
			this.okButton.Text = "Ok";
			this.okButton.UseVisualStyleBackColor = true;
			base.AutoScaleDimensions = new global::System.Drawing.SizeF(6f, 13f);
			base.AutoScaleMode = global::System.Windows.Forms.AutoScaleMode.Font;
			base.ClientSize = new global::System.Drawing.Size(357, 269);
			base.Controls.Add(this.okButton);
			base.Controls.Add(this.button1);
			base.Controls.Add(this.listBox);
			base.FormBorderStyle = global::System.Windows.Forms.FormBorderStyle.FixedDialog;
			base.Name = "SelectRankDialogForm";
			this.Text = "Select Rank";
			base.ResumeLayout(false);
		}

		// Token: 0x040005C9 RID: 1481
		private global::System.ComponentModel.IContainer components;

		// Token: 0x040005CA RID: 1482
		private global::System.Windows.Forms.ListBox listBox;

		// Token: 0x040005CB RID: 1483
		private global::System.Windows.Forms.Button button1;

		// Token: 0x040005CC RID: 1484
		private global::System.Windows.Forms.Button okButton;
	}
}
