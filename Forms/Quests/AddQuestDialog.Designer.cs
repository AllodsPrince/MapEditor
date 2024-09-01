namespace MapEditor.Forms.Quests
{
	// Token: 0x0200006D RID: 109
	public partial class AddQuestDialog : global::System.Windows.Forms.Form
	{
		// Token: 0x0600054B RID: 1355 RVA: 0x0002B449 File Offset: 0x0002A449
		protected override void Dispose(bool disposing)
		{
			if (disposing && this.components != null)
			{
				this.components.Dispose();
			}
			base.Dispose(disposing);
		}

		// Token: 0x0600054C RID: 1356 RVA: 0x0002B468 File Offset: 0x0002A468
		private void InitializeComponent()
		{
			global::System.ComponentModel.ComponentResourceManager resources = new global::System.ComponentModel.ComponentResourceManager(typeof(global::MapEditor.Forms.Quests.AddQuestDialog));
			this.OkInputButton = new global::System.Windows.Forms.Button();
			this.QuestNameTextBox = new global::System.Windows.Forms.TextBox();
			this.zoneComboBox = new global::System.Windows.Forms.ComboBox();
			this.lable1 = new global::System.Windows.Forms.Label();
			this.label2 = new global::System.Windows.Forms.Label();
			this.CancelInputButton = new global::System.Windows.Forms.Button();
			base.SuspendLayout();
			resources.ApplyResources(this.OkInputButton, "OkInputButton");
			this.OkInputButton.DialogResult = global::System.Windows.Forms.DialogResult.OK;
			this.OkInputButton.Name = "OkInputButton";
			this.OkInputButton.UseVisualStyleBackColor = true;
			resources.ApplyResources(this.QuestNameTextBox, "QuestNameTextBox");
			this.QuestNameTextBox.Name = "QuestNameTextBox";
			resources.ApplyResources(this.zoneComboBox, "zoneComboBox");
			this.zoneComboBox.FormattingEnabled = true;
			this.zoneComboBox.Name = "zoneComboBox";
			this.zoneComboBox.Sorted = true;
			resources.ApplyResources(this.lable1, "lable1");
			this.lable1.Name = "lable1";
			resources.ApplyResources(this.label2, "label2");
			this.label2.Name = "label2";
			resources.ApplyResources(this.CancelInputButton, "CancelInputButton");
			this.CancelInputButton.DialogResult = global::System.Windows.Forms.DialogResult.Cancel;
			this.CancelInputButton.Name = "CancelInputButton";
			this.CancelInputButton.UseVisualStyleBackColor = true;
			base.AcceptButton = this.OkInputButton;
			resources.ApplyResources(this, "$this");
			base.AutoScaleMode = global::System.Windows.Forms.AutoScaleMode.Font;
			base.CancelButton = this.CancelInputButton;
			base.Controls.Add(this.CancelInputButton);
			base.Controls.Add(this.label2);
			base.Controls.Add(this.lable1);
			base.Controls.Add(this.zoneComboBox);
			base.Controls.Add(this.QuestNameTextBox);
			base.Controls.Add(this.OkInputButton);
			base.FormBorderStyle = global::System.Windows.Forms.FormBorderStyle.FixedDialog;
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.Name = "AddQuestDialog";
			base.ShowInTaskbar = false;
			base.ResumeLayout(false);
			base.PerformLayout();
		}

		// Token: 0x040003EB RID: 1003
		private global::System.ComponentModel.IContainer components;

		// Token: 0x040003EC RID: 1004
		private global::System.Windows.Forms.Button OkInputButton;

		// Token: 0x040003ED RID: 1005
		private global::System.Windows.Forms.TextBox QuestNameTextBox;

		// Token: 0x040003EE RID: 1006
		private global::System.Windows.Forms.ComboBox zoneComboBox;

		// Token: 0x040003EF RID: 1007
		private global::System.Windows.Forms.Label lable1;

		// Token: 0x040003F0 RID: 1008
		private global::System.Windows.Forms.Label label2;

		// Token: 0x040003F1 RID: 1009
		private global::System.Windows.Forms.Button CancelInputButton;
	}
}
