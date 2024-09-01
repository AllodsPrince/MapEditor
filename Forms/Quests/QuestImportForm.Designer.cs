namespace MapEditor.Forms.Quests
{
	// Token: 0x02000079 RID: 121
	public partial class QuestImportForm : global::MapEditor.Forms.Base.BaseForm
	{
		// Token: 0x060005F1 RID: 1521 RVA: 0x00032709 File Offset: 0x00031709
		protected override void Dispose(bool disposing)
		{
			if (disposing && this.components != null)
			{
				this.components.Dispose();
			}
			base.Dispose(disposing);
		}

		// Token: 0x060005F2 RID: 1522 RVA: 0x00032728 File Offset: 0x00031728
		private void InitializeComponent()
		{
			global::System.ComponentModel.ComponentResourceManager resources = new global::System.ComponentModel.ComponentResourceManager(typeof(global::MapEditor.Forms.Quests.QuestImportForm));
			this.FilePathTextBox = new global::System.Windows.Forms.TextBox();
			this.importQuestsButton = new global::System.Windows.Forms.Button();
			this.ResultTextBox = new global::System.Windows.Forms.RichTextBox();
			this.importCountersButton = new global::System.Windows.Forms.Button();
			base.SuspendLayout();
			this.FilePathTextBox.AccessibleDescription = null;
			this.FilePathTextBox.AccessibleName = null;
			resources.ApplyResources(this.FilePathTextBox, "FilePathTextBox");
			this.FilePathTextBox.BackgroundImage = null;
			this.FilePathTextBox.Font = null;
			this.FilePathTextBox.Name = "FilePathTextBox";
			this.FilePathTextBox.ReadOnly = true;
			this.importQuestsButton.AccessibleDescription = null;
			this.importQuestsButton.AccessibleName = null;
			resources.ApplyResources(this.importQuestsButton, "importQuestsButton");
			this.importQuestsButton.BackgroundImage = null;
			this.importQuestsButton.Font = null;
			this.importQuestsButton.Name = "importQuestsButton";
			this.importQuestsButton.UseVisualStyleBackColor = true;
			this.ResultTextBox.AccessibleDescription = null;
			this.ResultTextBox.AccessibleName = null;
			resources.ApplyResources(this.ResultTextBox, "ResultTextBox");
			this.ResultTextBox.BackColor = global::System.Drawing.SystemColors.InactiveBorder;
			this.ResultTextBox.BackgroundImage = null;
			this.ResultTextBox.Font = null;
			this.ResultTextBox.Name = "ResultTextBox";
			this.ResultTextBox.ReadOnly = true;
			this.importCountersButton.AccessibleDescription = null;
			this.importCountersButton.AccessibleName = null;
			resources.ApplyResources(this.importCountersButton, "importCountersButton");
			this.importCountersButton.BackgroundImage = null;
			this.importCountersButton.Font = null;
			this.importCountersButton.Name = "importCountersButton";
			this.importCountersButton.UseVisualStyleBackColor = true;
			base.AccessibleDescription = null;
			base.AccessibleName = null;
			resources.ApplyResources(this, "$this");
			base.AutoScaleMode = global::System.Windows.Forms.AutoScaleMode.Font;
			this.BackgroundImage = null;
			base.Controls.Add(this.importCountersButton);
			base.Controls.Add(this.ResultTextBox);
			base.Controls.Add(this.importQuestsButton);
			base.Controls.Add(this.FilePathTextBox);
			this.Font = null;
			base.Icon = null;
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.Name = "QuestImportForm";
			base.ShowInTaskbar = false;
			base.ResumeLayout(false);
			base.PerformLayout();
		}

		// Token: 0x0400047D RID: 1149
		private global::System.ComponentModel.IContainer components;

		// Token: 0x0400047E RID: 1150
		private global::System.Windows.Forms.TextBox FilePathTextBox;

		// Token: 0x0400047F RID: 1151
		private global::System.Windows.Forms.Button importQuestsButton;

		// Token: 0x04000480 RID: 1152
		private global::System.Windows.Forms.RichTextBox ResultTextBox;

		// Token: 0x04000481 RID: 1153
		private global::System.Windows.Forms.Button importCountersButton;
	}
}
