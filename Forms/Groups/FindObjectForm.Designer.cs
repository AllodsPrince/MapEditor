namespace MapEditor.Forms.Groups
{
	// Token: 0x0200007B RID: 123
	public partial class FindObjectForm : global::System.Windows.Forms.Form
	{
		// Token: 0x06000600 RID: 1536 RVA: 0x00033098 File Offset: 0x00032098
		protected override void Dispose(bool disposing)
		{
			if (disposing && this.components != null)
			{
				this.components.Dispose();
			}
			base.Dispose(disposing);
		}

		// Token: 0x06000601 RID: 1537 RVA: 0x000330B8 File Offset: 0x000320B8
		private void InitializeComponent()
		{
			global::System.ComponentModel.ComponentResourceManager resources = new global::System.ComponentModel.ComponentResourceManager(typeof(global::MapEditor.Forms.Groups.FindObjectForm));
			this.FindButton = new global::System.Windows.Forms.Button();
			this.CloseButton = new global::System.Windows.Forms.Button();
			this.FindComboBox = new global::System.Windows.Forms.ComboBox();
			base.SuspendLayout();
			resources.ApplyResources(this.FindButton, "FindButton");
			this.FindButton.DialogResult = global::System.Windows.Forms.DialogResult.OK;
			this.FindButton.Name = "FindButton";
			this.FindButton.UseVisualStyleBackColor = true;
			this.CloseButton.DialogResult = global::System.Windows.Forms.DialogResult.Cancel;
			resources.ApplyResources(this.CloseButton, "CloseButton");
			this.CloseButton.Name = "CloseButton";
			this.CloseButton.UseVisualStyleBackColor = true;
			resources.ApplyResources(this.FindComboBox, "FindComboBox");
			this.FindComboBox.DropDownHeight = 209;
			this.FindComboBox.FormattingEnabled = true;
			this.FindComboBox.Name = "FindComboBox";
			this.FindComboBox.Leave += new global::System.EventHandler(this.FindComboBox_Leave);
			base.AcceptButton = this.FindButton;
			resources.ApplyResources(this, "$this");
			base.AutoScaleMode = global::System.Windows.Forms.AutoScaleMode.Font;
			base.CancelButton = this.CloseButton;
			base.Controls.Add(this.FindComboBox);
			base.Controls.Add(this.CloseButton);
			base.Controls.Add(this.FindButton);
			base.FormBorderStyle = global::System.Windows.Forms.FormBorderStyle.FixedDialog;
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.Name = "FindObjectForm";
			base.ShowIcon = false;
			base.ShowInTaskbar = false;
			base.FormClosing += new global::System.Windows.Forms.FormClosingEventHandler(this.FindObjectForm_FormClosing);
			base.ResumeLayout(false);
		}

		// Token: 0x0400048E RID: 1166
		private global::System.ComponentModel.IContainer components;

		// Token: 0x0400048F RID: 1167
		private global::System.Windows.Forms.Button FindButton;

		// Token: 0x04000490 RID: 1168
		private global::System.Windows.Forms.Button CloseButton;

		// Token: 0x04000491 RID: 1169
		private global::System.Windows.Forms.ComboBox FindComboBox;
	}
}
