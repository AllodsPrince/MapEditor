namespace MapEditor.Forms.MobScripts
{
	// Token: 0x020000D8 RID: 216
	public partial class MobScriptsForm : global::MapEditor.Forms.Base.BaseForm
	{
		// Token: 0x06000B0B RID: 2827 RVA: 0x0005A572 File Offset: 0x00059572
		protected override void Dispose(bool disposing)
		{
			if (disposing && this.components != null)
			{
				this.components.Dispose();
			}
			base.Dispose(disposing);
		}

		// Token: 0x06000B0C RID: 2828 RVA: 0x0005A594 File Offset: 0x00059594
		private void InitializeComponent()
		{
			this.aggroControl = new global::MapEditor.Forms.MobScripts.SayActionListControl();
			this.tabPages = new global::System.Windows.Forms.TabControl();
			this.tabPage1 = new global::System.Windows.Forms.TabPage();
			this.tabPage2 = new global::System.Windows.Forms.TabPage();
			this.mobHealthTextBox = new global::System.Windows.Forms.TextBox();
			this.label1 = new global::System.Windows.Forms.Label();
			this.mobHealthControl = new global::MapEditor.Forms.MobScripts.SayActionListControl();
			this.tabPage3 = new global::System.Windows.Forms.TabPage();
			this.enemyHealthTextBox = new global::System.Windows.Forms.TextBox();
			this.label2 = new global::System.Windows.Forms.Label();
			this.enemyHelthControl = new global::MapEditor.Forms.MobScripts.SayActionListControl();
			this.tabPage4 = new global::System.Windows.Forms.TabPage();
			this.idlePeriodMaxTextBox = new global::System.Windows.Forms.TextBox();
			this.label4 = new global::System.Windows.Forms.Label();
			this.idlePeriodMinTextBox = new global::System.Windows.Forms.TextBox();
			this.label3 = new global::System.Windows.Forms.Label();
			this.idleScirptControl = new global::MapEditor.Forms.MobScripts.SayActionListControl();
			this.tabPage5 = new global::System.Windows.Forms.TabPage();
			this.autoplayDeathAnimCheckBox = new global::System.Windows.Forms.CheckBox();
			this.deathScirptControl = new global::MapEditor.Forms.MobScripts.SayActionListControl();
			this.saveButton = new global::System.Windows.Forms.Button();
			this.loadButton = new global::System.Windows.Forms.Button();
			this.modelViewerButton = new global::System.Windows.Forms.Button();
			this.tabPages.SuspendLayout();
			this.tabPage1.SuspendLayout();
			this.tabPage2.SuspendLayout();
			this.tabPage3.SuspendLayout();
			this.tabPage4.SuspendLayout();
			this.tabPage5.SuspendLayout();
			base.SuspendLayout();
			this.aggroControl.Anchor = (global::System.Windows.Forms.AnchorStyles.Top | global::System.Windows.Forms.AnchorStyles.Bottom | global::System.Windows.Forms.AnchorStyles.Left | global::System.Windows.Forms.AnchorStyles.Right);
			this.aggroControl.BorderStyle = global::System.Windows.Forms.BorderStyle.FixedSingle;
			this.aggroControl.Location = new global::System.Drawing.Point(5, 7);
			this.aggroControl.Name = "aggroControl";
			this.aggroControl.Size = new global::System.Drawing.Size(429, 435);
			this.aggroControl.TabIndex = 0;
			this.tabPages.Anchor = (global::System.Windows.Forms.AnchorStyles.Top | global::System.Windows.Forms.AnchorStyles.Bottom | global::System.Windows.Forms.AnchorStyles.Left | global::System.Windows.Forms.AnchorStyles.Right);
			this.tabPages.Controls.Add(this.tabPage1);
			this.tabPages.Controls.Add(this.tabPage2);
			this.tabPages.Controls.Add(this.tabPage3);
			this.tabPages.Controls.Add(this.tabPage4);
			this.tabPages.Controls.Add(this.tabPage5);
			this.tabPages.Location = new global::System.Drawing.Point(1, 32);
			this.tabPages.Name = "tabPages";
			this.tabPages.SelectedIndex = 0;
			this.tabPages.Size = new global::System.Drawing.Size(446, 470);
			this.tabPages.TabIndex = 1;
			this.tabPage1.Controls.Add(this.aggroControl);
			this.tabPage1.Location = new global::System.Drawing.Point(4, 22);
			this.tabPage1.Name = "tabPage1";
			this.tabPage1.Padding = new global::System.Windows.Forms.Padding(3);
			this.tabPage1.Size = new global::System.Drawing.Size(438, 444);
			this.tabPage1.TabIndex = 0;
			this.tabPage1.Text = "aggroScript";
			this.tabPage1.UseVisualStyleBackColor = true;
			this.tabPage2.Controls.Add(this.mobHealthTextBox);
			this.tabPage2.Controls.Add(this.label1);
			this.tabPage2.Controls.Add(this.mobHealthControl);
			this.tabPage2.Location = new global::System.Drawing.Point(4, 22);
			this.tabPage2.Name = "tabPage2";
			this.tabPage2.Padding = new global::System.Windows.Forms.Padding(3);
			this.tabPage2.Size = new global::System.Drawing.Size(438, 444);
			this.tabPage2.TabIndex = 1;
			this.tabPage2.Text = "mobHealth";
			this.tabPage2.UseVisualStyleBackColor = true;
			this.mobHealthTextBox.Location = new global::System.Drawing.Point(49, 9);
			this.mobHealthTextBox.Name = "mobHealthTextBox";
			this.mobHealthTextBox.Size = new global::System.Drawing.Size(70, 20);
			this.mobHealthTextBox.TabIndex = 3;
			this.label1.AutoSize = true;
			this.label1.Location = new global::System.Drawing.Point(2, 12);
			this.label1.Name = "label1";
			this.label1.Size = new global::System.Drawing.Size(41, 13);
			this.label1.TabIndex = 2;
			this.label1.Text = "Health:";
			this.mobHealthControl.Anchor = (global::System.Windows.Forms.AnchorStyles.Top | global::System.Windows.Forms.AnchorStyles.Bottom | global::System.Windows.Forms.AnchorStyles.Left | global::System.Windows.Forms.AnchorStyles.Right);
			this.mobHealthControl.BorderStyle = global::System.Windows.Forms.BorderStyle.FixedSingle;
			this.mobHealthControl.Location = new global::System.Drawing.Point(5, 45);
			this.mobHealthControl.Name = "mobHealthControl";
			this.mobHealthControl.Size = new global::System.Drawing.Size(430, 373);
			this.mobHealthControl.TabIndex = 1;
			this.tabPage3.Controls.Add(this.enemyHealthTextBox);
			this.tabPage3.Controls.Add(this.label2);
			this.tabPage3.Controls.Add(this.enemyHelthControl);
			this.tabPage3.Location = new global::System.Drawing.Point(4, 22);
			this.tabPage3.Name = "tabPage3";
			this.tabPage3.Size = new global::System.Drawing.Size(438, 444);
			this.tabPage3.TabIndex = 2;
			this.tabPage3.Text = "enemyHealth";
			this.tabPage3.UseVisualStyleBackColor = true;
			this.enemyHealthTextBox.Location = new global::System.Drawing.Point(49, 9);
			this.enemyHealthTextBox.Name = "enemyHealthTextBox";
			this.enemyHealthTextBox.Size = new global::System.Drawing.Size(70, 20);
			this.enemyHealthTextBox.TabIndex = 5;
			this.label2.AutoSize = true;
			this.label2.Location = new global::System.Drawing.Point(2, 12);
			this.label2.Name = "label2";
			this.label2.Size = new global::System.Drawing.Size(41, 13);
			this.label2.TabIndex = 4;
			this.label2.Text = "Health:";
			this.enemyHelthControl.Anchor = (global::System.Windows.Forms.AnchorStyles.Top | global::System.Windows.Forms.AnchorStyles.Bottom | global::System.Windows.Forms.AnchorStyles.Left | global::System.Windows.Forms.AnchorStyles.Right);
			this.enemyHelthControl.BorderStyle = global::System.Windows.Forms.BorderStyle.FixedSingle;
			this.enemyHelthControl.Location = new global::System.Drawing.Point(5, 45);
			this.enemyHelthControl.Name = "enemyHelthControl";
			this.enemyHelthControl.Size = new global::System.Drawing.Size(430, 373);
			this.enemyHelthControl.TabIndex = 2;
			this.tabPage4.Controls.Add(this.idlePeriodMaxTextBox);
			this.tabPage4.Controls.Add(this.label4);
			this.tabPage4.Controls.Add(this.idlePeriodMinTextBox);
			this.tabPage4.Controls.Add(this.label3);
			this.tabPage4.Controls.Add(this.idleScirptControl);
			this.tabPage4.Location = new global::System.Drawing.Point(4, 22);
			this.tabPage4.Name = "tabPage4";
			this.tabPage4.Size = new global::System.Drawing.Size(438, 444);
			this.tabPage4.TabIndex = 3;
			this.tabPage4.Text = "idleScriptParams";
			this.tabPage4.UseVisualStyleBackColor = true;
			this.idlePeriodMaxTextBox.Location = new global::System.Drawing.Point(247, 9);
			this.idlePeriodMaxTextBox.Name = "idlePeriodMaxTextBox";
			this.idlePeriodMaxTextBox.Size = new global::System.Drawing.Size(70, 20);
			this.idlePeriodMaxTextBox.TabIndex = 9;
			this.label4.AutoSize = true;
			this.label4.Location = new global::System.Drawing.Point(167, 12);
			this.label4.Name = "label4";
			this.label4.Size = new global::System.Drawing.Size(83, 13);
			this.label4.TabIndex = 8;
			this.label4.Text = "Idle Period Max:";
			this.idlePeriodMinTextBox.Location = new global::System.Drawing.Point(82, 9);
			this.idlePeriodMinTextBox.Name = "idlePeriodMinTextBox";
			this.idlePeriodMinTextBox.Size = new global::System.Drawing.Size(70, 20);
			this.idlePeriodMinTextBox.TabIndex = 7;
			this.label3.AutoSize = true;
			this.label3.Location = new global::System.Drawing.Point(2, 12);
			this.label3.Name = "label3";
			this.label3.Size = new global::System.Drawing.Size(80, 13);
			this.label3.TabIndex = 6;
			this.label3.Text = "Idle Period Min:";
			this.idleScirptControl.Anchor = (global::System.Windows.Forms.AnchorStyles.Top | global::System.Windows.Forms.AnchorStyles.Bottom | global::System.Windows.Forms.AnchorStyles.Left | global::System.Windows.Forms.AnchorStyles.Right);
			this.idleScirptControl.BorderStyle = global::System.Windows.Forms.BorderStyle.FixedSingle;
			this.idleScirptControl.Location = new global::System.Drawing.Point(5, 45);
			this.idleScirptControl.Name = "idleScirptControl";
			this.idleScirptControl.Size = new global::System.Drawing.Size(433, 373);
			this.idleScirptControl.TabIndex = 3;
			this.tabPage5.Controls.Add(this.autoplayDeathAnimCheckBox);
			this.tabPage5.Controls.Add(this.deathScirptControl);
			this.tabPage5.Location = new global::System.Drawing.Point(4, 22);
			this.tabPage5.Name = "tabPage5";
			this.tabPage5.Size = new global::System.Drawing.Size(438, 444);
			this.tabPage5.TabIndex = 4;
			this.tabPage5.Text = "deathScriptParams";
			this.tabPage5.UseVisualStyleBackColor = true;
			this.autoplayDeathAnimCheckBox.AutoSize = true;
			this.autoplayDeathAnimCheckBox.Location = new global::System.Drawing.Point(5, 13);
			this.autoplayDeathAnimCheckBox.Name = "autoplayDeathAnimCheckBox";
			this.autoplayDeathAnimCheckBox.Size = new global::System.Drawing.Size(125, 17);
			this.autoplayDeathAnimCheckBox.TabIndex = 5;
			this.autoplayDeathAnimCheckBox.Text = "Autoplay Death Anim";
			this.autoplayDeathAnimCheckBox.UseVisualStyleBackColor = true;
			this.deathScirptControl.Anchor = (global::System.Windows.Forms.AnchorStyles.Top | global::System.Windows.Forms.AnchorStyles.Bottom | global::System.Windows.Forms.AnchorStyles.Left | global::System.Windows.Forms.AnchorStyles.Right);
			this.deathScirptControl.BorderStyle = global::System.Windows.Forms.BorderStyle.FixedSingle;
			this.deathScirptControl.Location = new global::System.Drawing.Point(5, 45);
			this.deathScirptControl.Name = "deathScirptControl";
			this.deathScirptControl.Size = new global::System.Drawing.Size(430, 373);
			this.deathScirptControl.TabIndex = 4;
			this.saveButton.Enabled = false;
			this.saveButton.Location = new global::System.Drawing.Point(58, 3);
			this.saveButton.Name = "saveButton";
			this.saveButton.Size = new global::System.Drawing.Size(51, 23);
			this.saveButton.TabIndex = 12;
			this.saveButton.Text = "Save";
			this.saveButton.UseVisualStyleBackColor = true;
			this.loadButton.Location = new global::System.Drawing.Point(1, 3);
			this.loadButton.Name = "loadButton";
			this.loadButton.Size = new global::System.Drawing.Size(51, 23);
			this.loadButton.TabIndex = 13;
			this.loadButton.Text = "Open...";
			this.loadButton.UseVisualStyleBackColor = true;
			this.modelViewerButton.Anchor = (global::System.Windows.Forms.AnchorStyles.Top | global::System.Windows.Forms.AnchorStyles.Right);
			this.modelViewerButton.Enabled = false;
			this.modelViewerButton.Location = new global::System.Drawing.Point(309, 3);
			this.modelViewerButton.Name = "modelViewerButton";
			this.modelViewerButton.Size = new global::System.Drawing.Size(138, 23);
			this.modelViewerButton.TabIndex = 15;
			this.modelViewerButton.Text = "Open in model viewer...";
			this.modelViewerButton.UseVisualStyleBackColor = true;
			base.AutoScaleDimensions = new global::System.Drawing.SizeF(6f, 13f);
			base.AutoScaleMode = global::System.Windows.Forms.AutoScaleMode.Font;
			base.ClientSize = new global::System.Drawing.Size(449, 502);
			base.Controls.Add(this.modelViewerButton);
			base.Controls.Add(this.loadButton);
			base.Controls.Add(this.saveButton);
			base.Controls.Add(this.tabPages);
			base.Name = "MobScriptsForm";
			this.Text = "Mob Scripts";
			this.tabPages.ResumeLayout(false);
			this.tabPage1.ResumeLayout(false);
			this.tabPage2.ResumeLayout(false);
			this.tabPage2.PerformLayout();
			this.tabPage3.ResumeLayout(false);
			this.tabPage3.PerformLayout();
			this.tabPage4.ResumeLayout(false);
			this.tabPage4.PerformLayout();
			this.tabPage5.ResumeLayout(false);
			this.tabPage5.PerformLayout();
			base.ResumeLayout(false);
		}

		// Token: 0x04000863 RID: 2147
		private global::System.ComponentModel.IContainer components;

		// Token: 0x04000864 RID: 2148
		private global::MapEditor.Forms.MobScripts.SayActionListControl aggroControl;

		// Token: 0x04000865 RID: 2149
		private global::System.Windows.Forms.TabControl tabPages;

		// Token: 0x04000866 RID: 2150
		private global::System.Windows.Forms.TabPage tabPage1;

		// Token: 0x04000867 RID: 2151
		private global::System.Windows.Forms.TabPage tabPage2;

		// Token: 0x04000868 RID: 2152
		private global::System.Windows.Forms.Button saveButton;

		// Token: 0x04000869 RID: 2153
		private global::System.Windows.Forms.TextBox mobHealthTextBox;

		// Token: 0x0400086A RID: 2154
		private global::System.Windows.Forms.Label label1;

		// Token: 0x0400086B RID: 2155
		private global::MapEditor.Forms.MobScripts.SayActionListControl mobHealthControl;

		// Token: 0x0400086C RID: 2156
		private global::System.Windows.Forms.TabPage tabPage3;

		// Token: 0x0400086D RID: 2157
		private global::System.Windows.Forms.TabPage tabPage4;

		// Token: 0x0400086E RID: 2158
		private global::System.Windows.Forms.TabPage tabPage5;

		// Token: 0x0400086F RID: 2159
		private global::MapEditor.Forms.MobScripts.SayActionListControl enemyHelthControl;

		// Token: 0x04000870 RID: 2160
		private global::MapEditor.Forms.MobScripts.SayActionListControl idleScirptControl;

		// Token: 0x04000871 RID: 2161
		private global::MapEditor.Forms.MobScripts.SayActionListControl deathScirptControl;

		// Token: 0x04000872 RID: 2162
		private global::System.Windows.Forms.TextBox enemyHealthTextBox;

		// Token: 0x04000873 RID: 2163
		private global::System.Windows.Forms.Label label2;

		// Token: 0x04000874 RID: 2164
		private global::System.Windows.Forms.TextBox idlePeriodMaxTextBox;

		// Token: 0x04000875 RID: 2165
		private global::System.Windows.Forms.Label label4;

		// Token: 0x04000876 RID: 2166
		private global::System.Windows.Forms.TextBox idlePeriodMinTextBox;

		// Token: 0x04000877 RID: 2167
		private global::System.Windows.Forms.Label label3;

		// Token: 0x04000878 RID: 2168
		private global::System.Windows.Forms.CheckBox autoplayDeathAnimCheckBox;

		// Token: 0x04000879 RID: 2169
		private global::System.Windows.Forms.Button loadButton;

		// Token: 0x0400087A RID: 2170
		private global::System.Windows.Forms.Button modelViewerButton;
	}
}
