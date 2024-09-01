namespace MapEditor.Forms.Quests.QuickObjectGenerator
{
	// Token: 0x020001E5 RID: 485
	public partial class CreateQuestItemForm : global::MapEditor.Forms.Base.BaseForm
	{
		// Token: 0x06001884 RID: 6276 RVA: 0x000A30FE File Offset: 0x000A20FE
		protected override void Dispose(bool disposing)
		{
			if (disposing && this.components != null)
			{
				this.components.Dispose();
			}
			base.Dispose(disposing);
		}

		// Token: 0x06001885 RID: 6277 RVA: 0x000A3120 File Offset: 0x000A2120
		private void InitializeComponent()
		{
			global::System.ComponentModel.ComponentResourceManager resources = new global::System.ComponentModel.ComponentResourceManager(typeof(global::MapEditor.Forms.Quests.QuickObjectGenerator.CreateQuestItemForm));
			this.ZoneComboBox = new global::System.Windows.Forms.ComboBox();
			this.label2 = new global::System.Windows.Forms.Label();
			this.label4 = new global::System.Windows.Forms.Label();
			this.ItemNameTextBox = new global::System.Windows.Forms.TextBox();
			this.label3 = new global::System.Windows.Forms.Label();
			this.FileNameTextBox = new global::System.Windows.Forms.TextBox();
			this.label1 = new global::System.Windows.Forms.Label();
			this.ItemDescTextBox = new global::System.Windows.Forms.TextBox();
			this.label5 = new global::System.Windows.Forms.Label();
			this.StackLimitTextBox = new global::System.Windows.Forms.TextBox();
			this.SaveButton = new global::System.Windows.Forms.Button();
			this.ResultTextBox = new global::System.Windows.Forms.RichTextBox();
			this.label6 = new global::System.Windows.Forms.Label();
			this.IconLabel = new global::System.Windows.Forms.Label();
			this.IconPictureBox = new global::System.Windows.Forms.PictureBox();
			this.ChooseIconButton = new global::System.Windows.Forms.Button();
			this.OpenButton = new global::System.Windows.Forms.Button();
			this.NewButton = new global::System.Windows.Forms.Button();
			this.label7 = new global::System.Windows.Forms.Label();
			this.SellPriceTextBox = new global::System.Windows.Forms.TextBox();
			this.label8 = new global::System.Windows.Forms.Label();
			this.BuyPriceTextBox = new global::System.Windows.Forms.TextBox();
			((global::System.ComponentModel.ISupportInitialize)this.IconPictureBox).BeginInit();
			base.SuspendLayout();
			resources.ApplyResources(this.ZoneComboBox, "ZoneComboBox");
			this.ZoneComboBox.DropDownStyle = global::System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.ZoneComboBox.FormattingEnabled = true;
			this.ZoneComboBox.Name = "ZoneComboBox";
			this.ZoneComboBox.Sorted = true;
			resources.ApplyResources(this.label2, "label2");
			this.label2.Name = "label2";
			resources.ApplyResources(this.label4, "label4");
			this.label4.Name = "label4";
			resources.ApplyResources(this.ItemNameTextBox, "ItemNameTextBox");
			this.ItemNameTextBox.Name = "ItemNameTextBox";
			resources.ApplyResources(this.label3, "label3");
			this.label3.Name = "label3";
			resources.ApplyResources(this.FileNameTextBox, "FileNameTextBox");
			this.FileNameTextBox.Name = "FileNameTextBox";
			resources.ApplyResources(this.label1, "label1");
			this.label1.Name = "label1";
			resources.ApplyResources(this.ItemDescTextBox, "ItemDescTextBox");
			this.ItemDescTextBox.Name = "ItemDescTextBox";
			resources.ApplyResources(this.label5, "label5");
			this.label5.Name = "label5";
			resources.ApplyResources(this.StackLimitTextBox, "StackLimitTextBox");
			this.StackLimitTextBox.Name = "StackLimitTextBox";
			resources.ApplyResources(this.SaveButton, "SaveButton");
			this.SaveButton.Name = "SaveButton";
			this.SaveButton.UseVisualStyleBackColor = true;
			resources.ApplyResources(this.ResultTextBox, "ResultTextBox");
			this.ResultTextBox.Name = "ResultTextBox";
			this.ResultTextBox.ReadOnly = true;
			resources.ApplyResources(this.label6, "label6");
			this.label6.Name = "label6";
			resources.ApplyResources(this.IconLabel, "IconLabel");
			this.IconLabel.Name = "IconLabel";
			resources.ApplyResources(this.IconPictureBox, "IconPictureBox");
			this.IconPictureBox.Name = "IconPictureBox";
			this.IconPictureBox.TabStop = false;
			resources.ApplyResources(this.ChooseIconButton, "ChooseIconButton");
			this.ChooseIconButton.Name = "ChooseIconButton";
			this.ChooseIconButton.UseVisualStyleBackColor = true;
			resources.ApplyResources(this.OpenButton, "OpenButton");
			this.OpenButton.Name = "OpenButton";
			this.OpenButton.UseVisualStyleBackColor = true;
			resources.ApplyResources(this.NewButton, "NewButton");
			this.NewButton.Name = "NewButton";
			this.NewButton.UseVisualStyleBackColor = true;
			resources.ApplyResources(this.label7, "label7");
			this.label7.Name = "label7";
			resources.ApplyResources(this.SellPriceTextBox, "SellPriceTextBox");
			this.SellPriceTextBox.Name = "SellPriceTextBox";
			resources.ApplyResources(this.label8, "label8");
			this.label8.Name = "label8";
			resources.ApplyResources(this.BuyPriceTextBox, "BuyPriceTextBox");
			this.BuyPriceTextBox.Name = "BuyPriceTextBox";
			resources.ApplyResources(this, "$this");
			base.AutoScaleMode = global::System.Windows.Forms.AutoScaleMode.Font;
			base.Controls.Add(this.label8);
			base.Controls.Add(this.BuyPriceTextBox);
			base.Controls.Add(this.label7);
			base.Controls.Add(this.SellPriceTextBox);
			base.Controls.Add(this.NewButton);
			base.Controls.Add(this.OpenButton);
			base.Controls.Add(this.ChooseIconButton);
			base.Controls.Add(this.IconPictureBox);
			base.Controls.Add(this.IconLabel);
			base.Controls.Add(this.label6);
			base.Controls.Add(this.SaveButton);
			base.Controls.Add(this.ResultTextBox);
			base.Controls.Add(this.label5);
			base.Controls.Add(this.StackLimitTextBox);
			base.Controls.Add(this.label1);
			base.Controls.Add(this.ItemDescTextBox);
			base.Controls.Add(this.label3);
			base.Controls.Add(this.FileNameTextBox);
			base.Controls.Add(this.label4);
			base.Controls.Add(this.ItemNameTextBox);
			base.Controls.Add(this.ZoneComboBox);
			base.Controls.Add(this.label2);
			base.Name = "CreateQuestItemForm";
			((global::System.ComponentModel.ISupportInitialize)this.IconPictureBox).EndInit();
			base.ResumeLayout(false);
			base.PerformLayout();
		}

		// Token: 0x04000FDF RID: 4063
		private global::System.ComponentModel.IContainer components;

		// Token: 0x04000FE0 RID: 4064
		private global::System.Windows.Forms.ComboBox ZoneComboBox;

		// Token: 0x04000FE1 RID: 4065
		private global::System.Windows.Forms.Label label2;

		// Token: 0x04000FE2 RID: 4066
		private global::System.Windows.Forms.Label label4;

		// Token: 0x04000FE3 RID: 4067
		private global::System.Windows.Forms.TextBox ItemNameTextBox;

		// Token: 0x04000FE4 RID: 4068
		private global::System.Windows.Forms.Label label3;

		// Token: 0x04000FE5 RID: 4069
		private global::System.Windows.Forms.TextBox FileNameTextBox;

		// Token: 0x04000FE6 RID: 4070
		private global::System.Windows.Forms.Label label1;

		// Token: 0x04000FE7 RID: 4071
		private global::System.Windows.Forms.TextBox ItemDescTextBox;

		// Token: 0x04000FE8 RID: 4072
		private global::System.Windows.Forms.Label label5;

		// Token: 0x04000FE9 RID: 4073
		private global::System.Windows.Forms.TextBox StackLimitTextBox;

		// Token: 0x04000FEA RID: 4074
		private global::System.Windows.Forms.RichTextBox ResultTextBox;

		// Token: 0x04000FEB RID: 4075
		private global::System.Windows.Forms.Label label6;

		// Token: 0x04000FEC RID: 4076
		private global::System.Windows.Forms.Label IconLabel;

		// Token: 0x04000FED RID: 4077
		private global::System.Windows.Forms.PictureBox IconPictureBox;

		// Token: 0x04000FEE RID: 4078
		private global::System.Windows.Forms.Button ChooseIconButton;

		// Token: 0x04000FEF RID: 4079
		private global::System.Windows.Forms.Button OpenButton;

		// Token: 0x04000FF0 RID: 4080
		private global::System.Windows.Forms.Button SaveButton;

		// Token: 0x04000FF1 RID: 4081
		private global::System.Windows.Forms.Button NewButton;

		// Token: 0x04000FF2 RID: 4082
		private global::System.Windows.Forms.Label label7;

		// Token: 0x04000FF3 RID: 4083
		private global::System.Windows.Forms.TextBox SellPriceTextBox;

		// Token: 0x04000FF4 RID: 4084
		private global::System.Windows.Forms.Label label8;

		// Token: 0x04000FF5 RID: 4085
		private global::System.Windows.Forms.TextBox BuyPriceTextBox;
	}
}
