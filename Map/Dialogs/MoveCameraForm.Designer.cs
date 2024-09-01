namespace MapEditor.Map.Dialogs
{
	// Token: 0x020001A9 RID: 425
	public partial class MoveCameraForm : global::System.Windows.Forms.Form
	{
		// Token: 0x0600148D RID: 5261 RVA: 0x00094713 File Offset: 0x00093713
		protected override void Dispose(bool disposing)
		{
			if (disposing && this.components != null)
			{
				this.components.Dispose();
			}
			base.Dispose(disposing);
		}

		// Token: 0x0600148E RID: 5262 RVA: 0x00094734 File Offset: 0x00093734
		private void InitializeComponent()
		{
			global::System.ComponentModel.ComponentResourceManager resources = new global::System.ComponentModel.ComponentResourceManager(typeof(global::MapEditor.Map.Dialogs.MoveCameraForm));
			this.TypeRadioButton0 = new global::System.Windows.Forms.RadioButton();
			this.TypeRadioButton1 = new global::System.Windows.Forms.RadioButton();
			this.CoordsTextBox = new global::System.Windows.Forms.TextBox();
			this.CoordsLabel = new global::System.Windows.Forms.Label();
			this.okButton = new global::System.Windows.Forms.Button();
			this.cancelButton = new global::System.Windows.Forms.Button();
			base.SuspendLayout();
			resources.ApplyResources(this.TypeRadioButton0, "TypeRadioButton0");
			this.TypeRadioButton0.Checked = true;
			this.TypeRadioButton0.Name = "TypeRadioButton0";
			this.TypeRadioButton0.TabStop = true;
			this.TypeRadioButton0.UseVisualStyleBackColor = true;
			resources.ApplyResources(this.TypeRadioButton1, "TypeRadioButton1");
			this.TypeRadioButton1.Name = "TypeRadioButton1";
			this.TypeRadioButton1.UseVisualStyleBackColor = true;
			resources.ApplyResources(this.CoordsTextBox, "CoordsTextBox");
			this.CoordsTextBox.Name = "CoordsTextBox";
			resources.ApplyResources(this.CoordsLabel, "CoordsLabel");
			this.CoordsLabel.Name = "CoordsLabel";
			this.okButton.DialogResult = global::System.Windows.Forms.DialogResult.OK;
			resources.ApplyResources(this.okButton, "okButton");
			this.okButton.Name = "okButton";
			this.okButton.UseVisualStyleBackColor = true;
			this.cancelButton.DialogResult = global::System.Windows.Forms.DialogResult.Cancel;
			resources.ApplyResources(this.cancelButton, "cancelButton");
			this.cancelButton.Name = "cancelButton";
			this.cancelButton.UseVisualStyleBackColor = true;
			base.AcceptButton = this.okButton;
			resources.ApplyResources(this, "$this");
			base.AutoScaleMode = global::System.Windows.Forms.AutoScaleMode.Font;
			base.CancelButton = this.cancelButton;
			base.Controls.Add(this.cancelButton);
			base.Controls.Add(this.okButton);
			base.Controls.Add(this.CoordsLabel);
			base.Controls.Add(this.CoordsTextBox);
			base.Controls.Add(this.TypeRadioButton1);
			base.Controls.Add(this.TypeRadioButton0);
			base.FormBorderStyle = global::System.Windows.Forms.FormBorderStyle.FixedSingle;
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.Name = "MoveCameraForm";
			base.ShowIcon = false;
			base.ShowInTaskbar = false;
			base.FormClosing += new global::System.Windows.Forms.FormClosingEventHandler(this.MoveCameraForm_FormClosing);
			base.ResumeLayout(false);
			base.PerformLayout();
		}

		// Token: 0x04000E6E RID: 3694
		private global::System.ComponentModel.IContainer components;

		// Token: 0x04000E6F RID: 3695
		private global::System.Windows.Forms.RadioButton TypeRadioButton0;

		// Token: 0x04000E70 RID: 3696
		private global::System.Windows.Forms.RadioButton TypeRadioButton1;

		// Token: 0x04000E71 RID: 3697
		private global::System.Windows.Forms.TextBox CoordsTextBox;

		// Token: 0x04000E72 RID: 3698
		private global::System.Windows.Forms.Label CoordsLabel;

		// Token: 0x04000E73 RID: 3699
		private global::System.Windows.Forms.Button okButton;

		// Token: 0x04000E74 RID: 3700
		private global::System.Windows.Forms.Button cancelButton;
	}
}
