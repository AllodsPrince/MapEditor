namespace MapEditor.Forms.Quests
{
	// Token: 0x02000050 RID: 80
	public partial class SetLocatorsForm : global::System.Windows.Forms.Form
	{
		// Token: 0x06000425 RID: 1061 RVA: 0x000228B9 File Offset: 0x000218B9
		protected override void Dispose(bool disposing)
		{
			if (disposing && this.components != null)
			{
				this.components.Dispose();
			}
			base.Dispose(disposing);
		}

		// Token: 0x06000426 RID: 1062 RVA: 0x000228D8 File Offset: 0x000218D8
		private void InitializeComponent()
		{
			this.returnYTextBox = new global::System.Windows.Forms.TextBox();
			this.returnXTextBox = new global::System.Windows.Forms.TextBox();
			this.returnZoneButton = new global::System.Windows.Forms.Button();
			this.label5 = new global::System.Windows.Forms.Label();
			this.label6 = new global::System.Windows.Forms.Label();
			this.autoSetReturnCheckBox = new global::System.Windows.Forms.CheckBox();
			this.label7 = new global::System.Windows.Forms.Label();
			this.returnLocatorPanel = new global::System.Windows.Forms.Panel();
			this.clearReturnButton = new global::System.Windows.Forms.Button();
			this.returnZoneTextBox = new global::System.Windows.Forms.TextBox();
			this.label8 = new global::System.Windows.Forms.Label();
			this.panel3 = new global::System.Windows.Forms.Panel();
			this.okButton = new global::System.Windows.Forms.Button();
			this.cancelButton = new global::System.Windows.Forms.Button();
			this.returnLocatorPanel.SuspendLayout();
			this.panel3.SuspendLayout();
			base.SuspendLayout();
			this.returnYTextBox.Anchor = (global::System.Windows.Forms.AnchorStyles.Top | global::System.Windows.Forms.AnchorStyles.Bottom | global::System.Windows.Forms.AnchorStyles.Left | global::System.Windows.Forms.AnchorStyles.Right);
			this.returnYTextBox.Location = new global::System.Drawing.Point(41, 47);
			this.returnYTextBox.Name = "returnYTextBox";
			this.returnYTextBox.Size = new global::System.Drawing.Size(444, 20);
			this.returnYTextBox.TabIndex = 20;
			this.returnXTextBox.Anchor = (global::System.Windows.Forms.AnchorStyles.Top | global::System.Windows.Forms.AnchorStyles.Bottom | global::System.Windows.Forms.AnchorStyles.Left | global::System.Windows.Forms.AnchorStyles.Right);
			this.returnXTextBox.Location = new global::System.Drawing.Point(41, 23);
			this.returnXTextBox.Name = "returnXTextBox";
			this.returnXTextBox.Size = new global::System.Drawing.Size(444, 20);
			this.returnXTextBox.TabIndex = 18;
			this.returnZoneButton.Anchor = (global::System.Windows.Forms.AnchorStyles.Top | global::System.Windows.Forms.AnchorStyles.Right);
			this.returnZoneButton.ImeMode = global::System.Windows.Forms.ImeMode.NoControl;
			this.returnZoneButton.Location = new global::System.Drawing.Point(543, 0);
			this.returnZoneButton.Name = "returnZoneButton";
			this.returnZoneButton.Size = new global::System.Drawing.Size(33, 20);
			this.returnZoneButton.TabIndex = 16;
			this.returnZoneButton.Text = " ...";
			this.returnZoneButton.UseVisualStyleBackColor = true;
			this.label5.AutoSize = true;
			this.label5.Location = new global::System.Drawing.Point(0, 26);
			this.label5.Name = "label5";
			this.label5.Size = new global::System.Drawing.Size(17, 13);
			this.label5.TabIndex = 17;
			this.label5.Text = "X:";
			this.label6.AutoSize = true;
			this.label6.Location = new global::System.Drawing.Point(1, 5);
			this.label6.Name = "label6";
			this.label6.Size = new global::System.Drawing.Size(86, 13);
			this.label6.TabIndex = 3;
			this.label6.Text = "Return Locators:";
			this.autoSetReturnCheckBox.AutoSize = true;
			this.autoSetReturnCheckBox.Location = new global::System.Drawing.Point(3, 3);
			this.autoSetReturnCheckBox.Name = "autoSetReturnCheckBox";
			this.autoSetReturnCheckBox.Size = new global::System.Drawing.Size(48, 17);
			this.autoSetReturnCheckBox.TabIndex = 0;
			this.autoSetReturnCheckBox.Text = "Auto";
			this.autoSetReturnCheckBox.UseVisualStyleBackColor = true;
			this.label7.AutoSize = true;
			this.label7.Location = new global::System.Drawing.Point(0, 50);
			this.label7.Name = "label7";
			this.label7.Size = new global::System.Drawing.Size(17, 13);
			this.label7.TabIndex = 19;
			this.label7.Text = "Y:";
			this.returnLocatorPanel.Anchor = (global::System.Windows.Forms.AnchorStyles.Top | global::System.Windows.Forms.AnchorStyles.Bottom | global::System.Windows.Forms.AnchorStyles.Left | global::System.Windows.Forms.AnchorStyles.Right);
			this.returnLocatorPanel.Controls.Add(this.clearReturnButton);
			this.returnLocatorPanel.Controls.Add(this.returnYTextBox);
			this.returnLocatorPanel.Controls.Add(this.label7);
			this.returnLocatorPanel.Controls.Add(this.returnXTextBox);
			this.returnLocatorPanel.Controls.Add(this.label5);
			this.returnLocatorPanel.Controls.Add(this.returnZoneButton);
			this.returnLocatorPanel.Controls.Add(this.returnZoneTextBox);
			this.returnLocatorPanel.Controls.Add(this.label8);
			this.returnLocatorPanel.Location = new global::System.Drawing.Point(3, 26);
			this.returnLocatorPanel.Name = "returnLocatorPanel";
			this.returnLocatorPanel.Size = new global::System.Drawing.Size(579, 69);
			this.returnLocatorPanel.TabIndex = 1;
			this.clearReturnButton.Anchor = (global::System.Windows.Forms.AnchorStyles.Top | global::System.Windows.Forms.AnchorStyles.Right);
			this.clearReturnButton.Location = new global::System.Drawing.Point(491, 26);
			this.clearReturnButton.Name = "clearReturnButton";
			this.clearReturnButton.Size = new global::System.Drawing.Size(82, 23);
			this.clearReturnButton.TabIndex = 22;
			this.clearReturnButton.Text = "Clear";
			this.clearReturnButton.UseVisualStyleBackColor = true;
			this.returnZoneTextBox.Anchor = (global::System.Windows.Forms.AnchorStyles.Top | global::System.Windows.Forms.AnchorStyles.Bottom | global::System.Windows.Forms.AnchorStyles.Left | global::System.Windows.Forms.AnchorStyles.Right);
			this.returnZoneTextBox.Location = new global::System.Drawing.Point(41, 0);
			this.returnZoneTextBox.Name = "returnZoneTextBox";
			this.returnZoneTextBox.ReadOnly = true;
			this.returnZoneTextBox.Size = new global::System.Drawing.Size(497, 20);
			this.returnZoneTextBox.TabIndex = 1;
			this.label8.AutoSize = true;
			this.label8.Location = new global::System.Drawing.Point(0, 3);
			this.label8.Name = "label8";
			this.label8.Size = new global::System.Drawing.Size(35, 13);
			this.label8.TabIndex = 0;
			this.label8.Text = "Zone:";
			this.panel3.Anchor = (global::System.Windows.Forms.AnchorStyles.Top | global::System.Windows.Forms.AnchorStyles.Left | global::System.Windows.Forms.AnchorStyles.Right);
			this.panel3.Controls.Add(this.returnLocatorPanel);
			this.panel3.Controls.Add(this.autoSetReturnCheckBox);
			this.panel3.Location = new global::System.Drawing.Point(4, 23);
			this.panel3.Name = "panel3";
			this.panel3.Size = new global::System.Drawing.Size(587, 98);
			this.panel3.TabIndex = 2;
			this.okButton.Anchor = (global::System.Windows.Forms.AnchorStyles.Top | global::System.Windows.Forms.AnchorStyles.Right);
			this.okButton.Location = new global::System.Drawing.Point(435, 127);
			this.okButton.Name = "okButton";
			this.okButton.Size = new global::System.Drawing.Size(75, 23);
			this.okButton.TabIndex = 4;
			this.okButton.Text = "Ok";
			this.okButton.UseVisualStyleBackColor = true;
			this.cancelButton.Anchor = (global::System.Windows.Forms.AnchorStyles.Top | global::System.Windows.Forms.AnchorStyles.Right);
			this.cancelButton.DialogResult = global::System.Windows.Forms.DialogResult.Cancel;
			this.cancelButton.Location = new global::System.Drawing.Point(516, 127);
			this.cancelButton.Name = "cancelButton";
			this.cancelButton.Size = new global::System.Drawing.Size(75, 23);
			this.cancelButton.TabIndex = 5;
			this.cancelButton.Text = "Cancel";
			this.cancelButton.UseVisualStyleBackColor = true;
			base.AcceptButton = this.okButton;
			base.AutoScaleDimensions = new global::System.Drawing.SizeF(6f, 13f);
			base.AutoScaleMode = global::System.Windows.Forms.AutoScaleMode.Font;
			base.CancelButton = this.cancelButton;
			base.ClientSize = new global::System.Drawing.Size(595, 155);
			base.Controls.Add(this.cancelButton);
			base.Controls.Add(this.okButton);
			base.Controls.Add(this.label6);
			base.Controls.Add(this.panel3);
			base.FormBorderStyle = global::System.Windows.Forms.FormBorderStyle.FixedDialog;
			base.Name = "SetLocatorsForm";
			this.Text = "Set Locators";
			this.returnLocatorPanel.ResumeLayout(false);
			this.returnLocatorPanel.PerformLayout();
			this.panel3.ResumeLayout(false);
			this.panel3.PerformLayout();
			base.ResumeLayout(false);
			base.PerformLayout();
		}

		// Token: 0x040002E2 RID: 738
		private global::System.ComponentModel.IContainer components;

		// Token: 0x040002E3 RID: 739
		private global::System.Windows.Forms.TextBox returnYTextBox;

		// Token: 0x040002E4 RID: 740
		private global::System.Windows.Forms.TextBox returnXTextBox;

		// Token: 0x040002E5 RID: 741
		private global::System.Windows.Forms.Button returnZoneButton;

		// Token: 0x040002E6 RID: 742
		private global::System.Windows.Forms.Label label5;

		// Token: 0x040002E7 RID: 743
		private global::System.Windows.Forms.Label label6;

		// Token: 0x040002E8 RID: 744
		private global::System.Windows.Forms.CheckBox autoSetReturnCheckBox;

		// Token: 0x040002E9 RID: 745
		private global::System.Windows.Forms.Label label7;

		// Token: 0x040002EA RID: 746
		private global::System.Windows.Forms.Panel returnLocatorPanel;

		// Token: 0x040002EB RID: 747
		private global::System.Windows.Forms.TextBox returnZoneTextBox;

		// Token: 0x040002EC RID: 748
		private global::System.Windows.Forms.Label label8;

		// Token: 0x040002ED RID: 749
		private global::System.Windows.Forms.Panel panel3;

		// Token: 0x040002EE RID: 750
		private global::System.Windows.Forms.Button okButton;

		// Token: 0x040002EF RID: 751
		private global::System.Windows.Forms.Button cancelButton;

		// Token: 0x040002F0 RID: 752
		private global::System.Windows.Forms.Button clearReturnButton;
	}
}
