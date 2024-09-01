namespace MapEditor.Forms.Quests.QuickObjectGenerator
{
	// Token: 0x0200006C RID: 108
	public partial class CreateNPCForm : global::MapEditor.Forms.Base.BaseForm
	{
		// Token: 0x06000543 RID: 1347 RVA: 0x0002AB35 File Offset: 0x00029B35
		protected override void Dispose(bool disposing)
		{
			if (disposing && this.components != null)
			{
				this.components.Dispose();
			}
			base.Dispose(disposing);
		}

		// Token: 0x06000544 RID: 1348 RVA: 0x0002AB54 File Offset: 0x00029B54
		private void InitializeComponent()
		{
			global::System.ComponentModel.ComponentResourceManager resources = new global::System.ComponentModel.ComponentResourceManager(typeof(global::MapEditor.Forms.Quests.QuickObjectGenerator.CreateNPCForm));
			this.label1 = new global::System.Windows.Forms.Label();
			this.label2 = new global::System.Windows.Forms.Label();
			this.label3 = new global::System.Windows.Forms.Label();
			this.FileNameTextBox = new global::System.Windows.Forms.TextBox();
			this.ZoneComboBox = new global::System.Windows.Forms.ComboBox();
			this.MaleRadioButton = new global::System.Windows.Forms.RadioButton();
			this.FemaleRadioButton = new global::System.Windows.Forms.RadioButton();
			this.NameTextBox = new global::System.Windows.Forms.TextBox();
			this.label4 = new global::System.Windows.Forms.Label();
			this.label5 = new global::System.Windows.Forms.Label();
			this.TitleTextBox = new global::System.Windows.Forms.TextBox();
			this.label6 = new global::System.Windows.Forms.Label();
			this.label7 = new global::System.Windows.Forms.Label();
			this.label8 = new global::System.Windows.Forms.Label();
			this.LevelTextBox = new global::System.Windows.Forms.TextBox();
			this.SpeedTextBox = new global::System.Windows.Forms.TextBox();
			this.label9 = new global::System.Windows.Forms.Label();
			this.RaceComboBox = new global::System.Windows.Forms.ComboBox();
			this.VisMobTextBox = new global::System.Windows.Forms.TextBox();
			this.ChooseVisMobButton = new global::System.Windows.Forms.Button();
			this.CreateButton = new global::System.Windows.Forms.Button();
			this.ResultTextBox = new global::System.Windows.Forms.RichTextBox();
			this.label10 = new global::System.Windows.Forms.Label();
			this.panel1 = new global::System.Windows.Forms.Panel();
			this.ChooseFactionButton = new global::System.Windows.Forms.Button();
			this.FactionTextBox = new global::System.Windows.Forms.TextBox();
			this.panel1.SuspendLayout();
			base.SuspendLayout();
			resources.ApplyResources(this.label1, "label1");
			this.label1.Name = "label1";
			resources.ApplyResources(this.label2, "label2");
			this.label2.Name = "label2";
			resources.ApplyResources(this.label3, "label3");
			this.label3.Name = "label3";
			resources.ApplyResources(this.FileNameTextBox, "FileNameTextBox");
			this.FileNameTextBox.Name = "FileNameTextBox";
			resources.ApplyResources(this.ZoneComboBox, "ZoneComboBox");
			this.ZoneComboBox.DropDownStyle = global::System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.ZoneComboBox.FormattingEnabled = true;
			this.ZoneComboBox.Name = "ZoneComboBox";
			this.ZoneComboBox.Sorted = true;
			resources.ApplyResources(this.MaleRadioButton, "MaleRadioButton");
			this.MaleRadioButton.Checked = true;
			this.MaleRadioButton.Name = "MaleRadioButton";
			this.MaleRadioButton.TabStop = true;
			this.MaleRadioButton.UseVisualStyleBackColor = true;
			resources.ApplyResources(this.FemaleRadioButton, "FemaleRadioButton");
			this.FemaleRadioButton.Name = "FemaleRadioButton";
			this.FemaleRadioButton.UseVisualStyleBackColor = true;
			resources.ApplyResources(this.NameTextBox, "NameTextBox");
			this.NameTextBox.Name = "NameTextBox";
			resources.ApplyResources(this.label4, "label4");
			this.label4.Name = "label4";
			resources.ApplyResources(this.label5, "label5");
			this.label5.Name = "label5";
			resources.ApplyResources(this.TitleTextBox, "TitleTextBox");
			this.TitleTextBox.Name = "TitleTextBox";
			resources.ApplyResources(this.label6, "label6");
			this.label6.Name = "label6";
			resources.ApplyResources(this.label7, "label7");
			this.label7.Name = "label7";
			resources.ApplyResources(this.label8, "label8");
			this.label8.Name = "label8";
			resources.ApplyResources(this.LevelTextBox, "LevelTextBox");
			this.LevelTextBox.Name = "LevelTextBox";
			resources.ApplyResources(this.SpeedTextBox, "SpeedTextBox");
			this.SpeedTextBox.Name = "SpeedTextBox";
			resources.ApplyResources(this.label9, "label9");
			this.label9.Name = "label9";
			resources.ApplyResources(this.RaceComboBox, "RaceComboBox");
			this.RaceComboBox.DropDownStyle = global::System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.RaceComboBox.FormattingEnabled = true;
			this.RaceComboBox.Name = "RaceComboBox";
			this.RaceComboBox.Sorted = true;
			resources.ApplyResources(this.VisMobTextBox, "VisMobTextBox");
			this.VisMobTextBox.Name = "VisMobTextBox";
			resources.ApplyResources(this.ChooseVisMobButton, "ChooseVisMobButton");
			this.ChooseVisMobButton.Name = "ChooseVisMobButton";
			this.ChooseVisMobButton.UseVisualStyleBackColor = true;
			resources.ApplyResources(this.CreateButton, "CreateButton");
			this.CreateButton.Name = "CreateButton";
			this.CreateButton.UseVisualStyleBackColor = true;
			resources.ApplyResources(this.ResultTextBox, "ResultTextBox");
			this.ResultTextBox.Name = "ResultTextBox";
			this.ResultTextBox.ReadOnly = true;
			resources.ApplyResources(this.label10, "label10");
			this.label10.Name = "label10";
			resources.ApplyResources(this.panel1, "panel1");
			this.panel1.Controls.Add(this.ChooseFactionButton);
			this.panel1.Controls.Add(this.FactionTextBox);
			this.panel1.Name = "panel1";
			resources.ApplyResources(this.ChooseFactionButton, "ChooseFactionButton");
			this.ChooseFactionButton.Name = "ChooseFactionButton";
			this.ChooseFactionButton.UseVisualStyleBackColor = true;
			resources.ApplyResources(this.FactionTextBox, "FactionTextBox");
			this.FactionTextBox.Name = "FactionTextBox";
			resources.ApplyResources(this, "$this");
			base.AutoScaleMode = global::System.Windows.Forms.AutoScaleMode.Font;
			base.Controls.Add(this.panel1);
			base.Controls.Add(this.label10);
			base.Controls.Add(this.ResultTextBox);
			base.Controls.Add(this.ChooseVisMobButton);
			base.Controls.Add(this.VisMobTextBox);
			base.Controls.Add(this.RaceComboBox);
			base.Controls.Add(this.label9);
			base.Controls.Add(this.SpeedTextBox);
			base.Controls.Add(this.LevelTextBox);
			base.Controls.Add(this.label8);
			base.Controls.Add(this.label7);
			base.Controls.Add(this.TitleTextBox);
			base.Controls.Add(this.label6);
			base.Controls.Add(this.NameTextBox);
			base.Controls.Add(this.label4);
			base.Controls.Add(this.label5);
			base.Controls.Add(this.CreateButton);
			base.Controls.Add(this.FemaleRadioButton);
			base.Controls.Add(this.MaleRadioButton);
			base.Controls.Add(this.ZoneComboBox);
			base.Controls.Add(this.FileNameTextBox);
			base.Controls.Add(this.label3);
			base.Controls.Add(this.label2);
			base.Controls.Add(this.label1);
			base.Name = "CreateNPCForm";
			this.panel1.ResumeLayout(false);
			this.panel1.PerformLayout();
			base.ResumeLayout(false);
			base.PerformLayout();
		}

		// Token: 0x040003D0 RID: 976
		private global::System.ComponentModel.IContainer components;

		// Token: 0x040003D1 RID: 977
		private global::System.Windows.Forms.Label label1;

		// Token: 0x040003D2 RID: 978
		private global::System.Windows.Forms.Label label2;

		// Token: 0x040003D3 RID: 979
		private global::System.Windows.Forms.Label label3;

		// Token: 0x040003D4 RID: 980
		private global::System.Windows.Forms.TextBox FileNameTextBox;

		// Token: 0x040003D5 RID: 981
		private global::System.Windows.Forms.ComboBox ZoneComboBox;

		// Token: 0x040003D6 RID: 982
		private global::System.Windows.Forms.RadioButton MaleRadioButton;

		// Token: 0x040003D7 RID: 983
		private global::System.Windows.Forms.RadioButton FemaleRadioButton;

		// Token: 0x040003D8 RID: 984
		private global::System.Windows.Forms.TextBox NameTextBox;

		// Token: 0x040003D9 RID: 985
		private global::System.Windows.Forms.Label label4;

		// Token: 0x040003DA RID: 986
		private global::System.Windows.Forms.Label label5;

		// Token: 0x040003DB RID: 987
		private global::System.Windows.Forms.TextBox TitleTextBox;

		// Token: 0x040003DC RID: 988
		private global::System.Windows.Forms.Label label6;

		// Token: 0x040003DD RID: 989
		private global::System.Windows.Forms.Label label7;

		// Token: 0x040003DE RID: 990
		private global::System.Windows.Forms.Label label8;

		// Token: 0x040003DF RID: 991
		private global::System.Windows.Forms.TextBox LevelTextBox;

		// Token: 0x040003E0 RID: 992
		private global::System.Windows.Forms.TextBox SpeedTextBox;

		// Token: 0x040003E1 RID: 993
		private global::System.Windows.Forms.Label label9;

		// Token: 0x040003E2 RID: 994
		private global::System.Windows.Forms.ComboBox RaceComboBox;

		// Token: 0x040003E3 RID: 995
		private global::System.Windows.Forms.TextBox VisMobTextBox;

		// Token: 0x040003E4 RID: 996
		private global::System.Windows.Forms.Button ChooseVisMobButton;

		// Token: 0x040003E5 RID: 997
		private global::System.Windows.Forms.Button CreateButton;

		// Token: 0x040003E6 RID: 998
		private global::System.Windows.Forms.RichTextBox ResultTextBox;

		// Token: 0x040003E7 RID: 999
		private global::System.Windows.Forms.Label label10;

		// Token: 0x040003E8 RID: 1000
		private global::System.Windows.Forms.Panel panel1;

		// Token: 0x040003E9 RID: 1001
		private global::System.Windows.Forms.Button ChooseFactionButton;

		// Token: 0x040003EA RID: 1002
		private global::System.Windows.Forms.TextBox FactionTextBox;
	}
}
