namespace MapEditor.Forms.CreateMimimap
{
	// Token: 0x0200007D RID: 125
	public partial class CreateMinimapForm : global::MapEditor.Forms.Base.BaseForm
	{
		// Token: 0x06000608 RID: 1544 RVA: 0x0003341F File Offset: 0x0003241F
		protected override void Dispose(bool disposing)
		{
			if (disposing && this.components != null)
			{
				this.components.Dispose();
			}
			base.Dispose(disposing);
		}

		// Token: 0x06000609 RID: 1545 RVA: 0x00033440 File Offset: 0x00032440
		private void InitializeComponent()
		{
			this.label2 = new global::System.Windows.Forms.Label();
			this.directoryTextBox = new global::System.Windows.Forms.TextBox();
			this.label3 = new global::System.Windows.Forms.Label();
			this.fileNameTextBox = new global::System.Windows.Forms.TextBox();
			this.createButton = new global::System.Windows.Forms.Button();
			this.continentComboBox = new global::System.Windows.Forms.ComboBox();
			this.labelName = new global::System.Windows.Forms.Label();
			this.browseButton = new global::System.Windows.Forms.Button();
			this.label1 = new global::System.Windows.Forms.Label();
			base.SuspendLayout();
			this.label2.AutoSize = true;
			this.label2.Location = new global::System.Drawing.Point(2, 31);
			this.label2.Name = "label2";
			this.label2.Size = new global::System.Drawing.Size(71, 13);
			this.label2.TabIndex = 2;
			this.label2.Text = "Image Folder:";
			this.directoryTextBox.Anchor = (global::System.Windows.Forms.AnchorStyles.Top | global::System.Windows.Forms.AnchorStyles.Left | global::System.Windows.Forms.AnchorStyles.Right);
			this.directoryTextBox.Location = new global::System.Drawing.Point(76, 28);
			this.directoryTextBox.Name = "directoryTextBox";
			this.directoryTextBox.Size = new global::System.Drawing.Size(352, 20);
			this.directoryTextBox.TabIndex = 3;
			this.label3.AutoSize = true;
			this.label3.Location = new global::System.Drawing.Point(2, 55);
			this.label3.Name = "label3";
			this.label3.Size = new global::System.Drawing.Size(70, 13);
			this.label3.TabIndex = 4;
			this.label3.Text = "Image Name:";
			this.fileNameTextBox.Anchor = (global::System.Windows.Forms.AnchorStyles.Top | global::System.Windows.Forms.AnchorStyles.Left | global::System.Windows.Forms.AnchorStyles.Right);
			this.fileNameTextBox.Location = new global::System.Drawing.Point(76, 52);
			this.fileNameTextBox.Name = "fileNameTextBox";
			this.fileNameTextBox.Size = new global::System.Drawing.Size(352, 20);
			this.fileNameTextBox.TabIndex = 5;
			this.createButton.Location = new global::System.Drawing.Point(5, 81);
			this.createButton.Name = "createButton";
			this.createButton.Size = new global::System.Drawing.Size(71, 20);
			this.createButton.TabIndex = 6;
			this.createButton.Text = "Create";
			this.createButton.UseVisualStyleBackColor = true;
			this.continentComboBox.Anchor = (global::System.Windows.Forms.AnchorStyles.Top | global::System.Windows.Forms.AnchorStyles.Left | global::System.Windows.Forms.AnchorStyles.Right);
			this.continentComboBox.DropDownStyle = global::System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.continentComboBox.FormattingEnabled = true;
			this.continentComboBox.Location = new global::System.Drawing.Point(76, 4);
			this.continentComboBox.Name = "continentComboBox";
			this.continentComboBox.Size = new global::System.Drawing.Size(429, 21);
			this.continentComboBox.Sorted = true;
			this.continentComboBox.TabIndex = 28;
			this.labelName.AutoSize = true;
			this.labelName.ImeMode = global::System.Windows.Forms.ImeMode.NoControl;
			this.labelName.Location = new global::System.Drawing.Point(2, 9);
			this.labelName.Name = "labelName";
			this.labelName.Size = new global::System.Drawing.Size(55, 13);
			this.labelName.TabIndex = 27;
			this.labelName.Text = "Continent:";
			this.browseButton.Anchor = (global::System.Windows.Forms.AnchorStyles.Top | global::System.Windows.Forms.AnchorStyles.Right);
			this.browseButton.Location = new global::System.Drawing.Point(434, 31);
			this.browseButton.Name = "browseButton";
			this.browseButton.Size = new global::System.Drawing.Size(71, 20);
			this.browseButton.TabIndex = 29;
			this.browseButton.Text = "Browse...";
			this.browseButton.UseVisualStyleBackColor = true;
			this.label1.Anchor = (global::System.Windows.Forms.AnchorStyles.Top | global::System.Windows.Forms.AnchorStyles.Right);
			this.label1.AutoSize = true;
			this.label1.Location = new global::System.Drawing.Point(434, 55);
			this.label1.Name = "label1";
			this.label1.Size = new global::System.Drawing.Size(24, 13);
			this.label1.TabIndex = 30;
			this.label1.Text = ".jpg";
			base.AutoScaleDimensions = new global::System.Drawing.SizeF(6f, 13f);
			base.AutoScaleMode = global::System.Windows.Forms.AutoScaleMode.Font;
			base.ClientSize = new global::System.Drawing.Size(507, 106);
			base.Controls.Add(this.label1);
			base.Controls.Add(this.browseButton);
			base.Controls.Add(this.continentComboBox);
			base.Controls.Add(this.labelName);
			base.Controls.Add(this.createButton);
			base.Controls.Add(this.fileNameTextBox);
			base.Controls.Add(this.label3);
			base.Controls.Add(this.directoryTextBox);
			base.Controls.Add(this.label2);
			base.Name = "CreateMinimapForm";
			this.Text = "Create Minimap";
			base.ResumeLayout(false);
			base.PerformLayout();
		}

		// Token: 0x0400049A RID: 1178
		private global::System.ComponentModel.IContainer components;

		// Token: 0x0400049B RID: 1179
		private global::System.Windows.Forms.Label label2;

		// Token: 0x0400049C RID: 1180
		private global::System.Windows.Forms.TextBox directoryTextBox;

		// Token: 0x0400049D RID: 1181
		private global::System.Windows.Forms.Label label3;

		// Token: 0x0400049E RID: 1182
		private global::System.Windows.Forms.TextBox fileNameTextBox;

		// Token: 0x0400049F RID: 1183
		private global::System.Windows.Forms.Button createButton;

		// Token: 0x040004A0 RID: 1184
		private global::System.Windows.Forms.ComboBox continentComboBox;

		// Token: 0x040004A1 RID: 1185
		private global::System.Windows.Forms.Label labelName;

		// Token: 0x040004A2 RID: 1186
		private global::System.Windows.Forms.Button browseButton;

		// Token: 0x040004A3 RID: 1187
		private global::System.Windows.Forms.Label label1;
	}
}
