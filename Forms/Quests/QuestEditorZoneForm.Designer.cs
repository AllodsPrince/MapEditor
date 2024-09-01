namespace MapEditor.Forms.Quests
{
	// Token: 0x020001B5 RID: 437
	public partial class QuestEditorZoneForm : global::System.Windows.Forms.Form
	{
		// Token: 0x06001530 RID: 5424 RVA: 0x00099805 File Offset: 0x00098805
		protected override void Dispose(bool disposing)
		{
			if (disposing && this.components != null)
			{
				this.components.Dispose();
			}
			base.Dispose(disposing);
		}

		// Token: 0x06001531 RID: 5425 RVA: 0x00099824 File Offset: 0x00098824
		private void InitializeComponent()
		{
			this.zoneComboBox = new global::System.Windows.Forms.ComboBox();
			this.CancelDialogButton = new global::System.Windows.Forms.Button();
			this.OkDialogButton = new global::System.Windows.Forms.Button();
			this.zoneRadioButton = new global::System.Windows.Forms.RadioButton();
			this.folderRadioButton = new global::System.Windows.Forms.RadioButton();
			this.folderTextBox = new global::System.Windows.Forms.TextBox();
			this.browserFolderButton = new global::System.Windows.Forms.Button();
			base.SuspendLayout();
			this.zoneComboBox.Anchor = (global::System.Windows.Forms.AnchorStyles.Top | global::System.Windows.Forms.AnchorStyles.Left | global::System.Windows.Forms.AnchorStyles.Right);
			this.zoneComboBox.DropDownStyle = global::System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.zoneComboBox.FormattingEnabled = true;
			this.zoneComboBox.Location = new global::System.Drawing.Point(93, 9);
			this.zoneComboBox.Name = "zoneComboBox";
			this.zoneComboBox.Size = new global::System.Drawing.Size(461, 21);
			this.zoneComboBox.Sorted = true;
			this.zoneComboBox.TabIndex = 3;
			this.CancelDialogButton.Anchor = (global::System.Windows.Forms.AnchorStyles.Top | global::System.Windows.Forms.AnchorStyles.Right);
			this.CancelDialogButton.DialogResult = global::System.Windows.Forms.DialogResult.Cancel;
			this.CancelDialogButton.Location = new global::System.Drawing.Point(479, 72);
			this.CancelDialogButton.Name = "CancelDialogButton";
			this.CancelDialogButton.Size = new global::System.Drawing.Size(62, 23);
			this.CancelDialogButton.TabIndex = 4;
			this.CancelDialogButton.Text = "Cancel";
			this.CancelDialogButton.UseVisualStyleBackColor = true;
			this.OkDialogButton.Anchor = (global::System.Windows.Forms.AnchorStyles.Top | global::System.Windows.Forms.AnchorStyles.Right);
			this.OkDialogButton.DialogResult = global::System.Windows.Forms.DialogResult.OK;
			this.OkDialogButton.Location = new global::System.Drawing.Point(411, 72);
			this.OkDialogButton.Name = "OkDialogButton";
			this.OkDialogButton.Size = new global::System.Drawing.Size(62, 23);
			this.OkDialogButton.TabIndex = 5;
			this.OkDialogButton.Text = "Ok";
			this.OkDialogButton.UseVisualStyleBackColor = true;
			this.zoneRadioButton.AutoSize = true;
			this.zoneRadioButton.Checked = true;
			this.zoneRadioButton.Location = new global::System.Drawing.Point(7, 10);
			this.zoneRadioButton.Name = "zoneRadioButton";
			this.zoneRadioButton.Size = new global::System.Drawing.Size(80, 17);
			this.zoneRadioButton.TabIndex = 6;
			this.zoneRadioButton.TabStop = true;
			this.zoneRadioButton.Text = "Load Zone:";
			this.zoneRadioButton.UseVisualStyleBackColor = true;
			this.folderRadioButton.AutoSize = true;
			this.folderRadioButton.Location = new global::System.Drawing.Point(7, 37);
			this.folderRadioButton.Name = "folderRadioButton";
			this.folderRadioButton.Size = new global::System.Drawing.Size(84, 17);
			this.folderRadioButton.TabIndex = 7;
			this.folderRadioButton.Text = "Load Folder:";
			this.folderRadioButton.UseVisualStyleBackColor = false;
			this.folderTextBox.Anchor = (global::System.Windows.Forms.AnchorStyles.Top | global::System.Windows.Forms.AnchorStyles.Left | global::System.Windows.Forms.AnchorStyles.Right);
			this.folderTextBox.Location = new global::System.Drawing.Point(93, 36);
			this.folderTextBox.Name = "folderTextBox";
			this.folderTextBox.ReadOnly = true;
			this.folderTextBox.Size = new global::System.Drawing.Size(384, 20);
			this.folderTextBox.TabIndex = 8;
			this.browserFolderButton.Anchor = (global::System.Windows.Forms.AnchorStyles.Top | global::System.Windows.Forms.AnchorStyles.Right);
			this.browserFolderButton.Enabled = false;
			this.browserFolderButton.Location = new global::System.Drawing.Point(483, 34);
			this.browserFolderButton.Name = "browserFolderButton";
			this.browserFolderButton.Size = new global::System.Drawing.Size(62, 23);
			this.browserFolderButton.TabIndex = 9;
			this.browserFolderButton.Text = "Browse";
			this.browserFolderButton.UseVisualStyleBackColor = true;
			base.AcceptButton = this.OkDialogButton;
			base.AutoScaleDimensions = new global::System.Drawing.SizeF(6f, 13f);
			base.AutoScaleMode = global::System.Windows.Forms.AutoScaleMode.Font;
			base.CancelButton = this.CancelDialogButton;
			base.ClientSize = new global::System.Drawing.Size(557, 105);
			base.Controls.Add(this.browserFolderButton);
			base.Controls.Add(this.folderTextBox);
			base.Controls.Add(this.folderRadioButton);
			base.Controls.Add(this.zoneRadioButton);
			base.Controls.Add(this.OkDialogButton);
			base.Controls.Add(this.CancelDialogButton);
			base.Controls.Add(this.zoneComboBox);
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.Name = "QuestEditorZoneForm";
			base.ShowInTaskbar = false;
			this.Text = "Quest Zone";
			base.ResumeLayout(false);
			base.PerformLayout();
		}

		// Token: 0x04000EF9 RID: 3833
		private global::System.ComponentModel.IContainer components;

		// Token: 0x04000EFA RID: 3834
		private global::System.Windows.Forms.ComboBox zoneComboBox;

		// Token: 0x04000EFB RID: 3835
		private global::System.Windows.Forms.Button CancelDialogButton;

		// Token: 0x04000EFC RID: 3836
		private global::System.Windows.Forms.Button OkDialogButton;

		// Token: 0x04000EFD RID: 3837
		private global::System.Windows.Forms.RadioButton zoneRadioButton;

		// Token: 0x04000EFE RID: 3838
		private global::System.Windows.Forms.RadioButton folderRadioButton;

		// Token: 0x04000EFF RID: 3839
		private global::System.Windows.Forms.TextBox folderTextBox;

		// Token: 0x04000F00 RID: 3840
		private global::System.Windows.Forms.Button browserFolderButton;
	}
}
