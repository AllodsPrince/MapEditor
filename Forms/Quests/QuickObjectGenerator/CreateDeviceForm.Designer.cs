namespace MapEditor.Forms.Quests.QuickObjectGenerator
{
	// Token: 0x0200009C RID: 156
	public partial class CreateDeviceForm : global::MapEditor.Forms.Base.BaseForm
	{
		// Token: 0x06000738 RID: 1848 RVA: 0x00038223 File Offset: 0x00037223
		protected override void Dispose(bool disposing)
		{
			if (disposing && this.components != null)
			{
				this.components.Dispose();
			}
			base.Dispose(disposing);
		}

		// Token: 0x06000739 RID: 1849 RVA: 0x00038244 File Offset: 0x00037244
		private void InitializeComponent()
		{
			this.components = new global::System.ComponentModel.Container();
			global::System.ComponentModel.ComponentResourceManager resources = new global::System.ComponentModel.ComponentResourceManager(typeof(global::MapEditor.Forms.Quests.QuickObjectGenerator.CreateDeviceForm));
			this.ChestRadioButton = new global::System.Windows.Forms.RadioButton();
			this.label1 = new global::System.Windows.Forms.Label();
			this.SteleRadioButton = new global::System.Windows.Forms.RadioButton();
			this.label2 = new global::System.Windows.Forms.Label();
			this.ZoneComboBox = new global::System.Windows.Forms.ComboBox();
			this.label3 = new global::System.Windows.Forms.Label();
			this.FileNameTextBox = new global::System.Windows.Forms.TextBox();
			this.label4 = new global::System.Windows.Forms.Label();
			this.ResNameTextBox = new global::System.Windows.Forms.TextBox();
			this.ResultTextBox = new global::System.Windows.Forms.RichTextBox();
			this.ChooseVisObjButton = new global::System.Windows.Forms.Button();
			this.VisObjTextBox = new global::System.Windows.Forms.TextBox();
			this.label5 = new global::System.Windows.Forms.Label();
			this.CollisionLabel = new global::System.Windows.Forms.Label();
			this.label7 = new global::System.Windows.Forms.Label();
			this.visObjTimer = new global::System.Windows.Forms.Timer(this.components);
			this.CreateButton = new global::System.Windows.Forms.Button();
			this.ExploitingTextBox = new global::System.Windows.Forms.TextBox();
			this.label6 = new global::System.Windows.Forms.Label();
			this.SpawnTableMapPanel = new global::System.Windows.Forms.Panel();
			this.MapComboBox = new global::System.Windows.Forms.ComboBox();
			this.label32 = new global::System.Windows.Forms.Label();
			this.panel5 = new global::System.Windows.Forms.Panel();
			this.ExistingSTRadioButton = new global::System.Windows.Forms.RadioButton();
			this.NewSTRadioButton = new global::System.Windows.Forms.RadioButton();
			this.SpawnTablePanel = new global::System.Windows.Forms.Panel();
			this.label33 = new global::System.Windows.Forms.Label();
			this.SpawnTableTextBox = new global::System.Windows.Forms.TextBox();
			this.ChooseSpawnTablebButton = new global::System.Windows.Forms.Button();
			this.label13 = new global::System.Windows.Forms.Label();
			this.SpawnTableMapPanel.SuspendLayout();
			this.panel5.SuspendLayout();
			this.SpawnTablePanel.SuspendLayout();
			base.SuspendLayout();
			resources.ApplyResources(this.ChestRadioButton, "ChestRadioButton");
			this.ChestRadioButton.Checked = true;
			this.ChestRadioButton.Name = "ChestRadioButton";
			this.ChestRadioButton.TabStop = true;
			this.ChestRadioButton.UseVisualStyleBackColor = true;
			resources.ApplyResources(this.label1, "label1");
			this.label1.Name = "label1";
			resources.ApplyResources(this.SteleRadioButton, "SteleRadioButton");
			this.SteleRadioButton.Name = "SteleRadioButton";
			this.SteleRadioButton.UseVisualStyleBackColor = true;
			resources.ApplyResources(this.label2, "label2");
			this.label2.Name = "label2";
			resources.ApplyResources(this.ZoneComboBox, "ZoneComboBox");
			this.ZoneComboBox.DropDownStyle = global::System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.ZoneComboBox.FormattingEnabled = true;
			this.ZoneComboBox.Name = "ZoneComboBox";
			this.ZoneComboBox.Sorted = true;
			resources.ApplyResources(this.label3, "label3");
			this.label3.Name = "label3";
			resources.ApplyResources(this.FileNameTextBox, "FileNameTextBox");
			this.FileNameTextBox.Name = "FileNameTextBox";
			resources.ApplyResources(this.label4, "label4");
			this.label4.Name = "label4";
			resources.ApplyResources(this.ResNameTextBox, "ResNameTextBox");
			this.ResNameTextBox.Name = "ResNameTextBox";
			resources.ApplyResources(this.ResultTextBox, "ResultTextBox");
			this.ResultTextBox.Name = "ResultTextBox";
			this.ResultTextBox.ReadOnly = true;
			resources.ApplyResources(this.ChooseVisObjButton, "ChooseVisObjButton");
			this.ChooseVisObjButton.Name = "ChooseVisObjButton";
			this.ChooseVisObjButton.UseVisualStyleBackColor = true;
			resources.ApplyResources(this.VisObjTextBox, "VisObjTextBox");
			this.VisObjTextBox.Name = "VisObjTextBox";
			resources.ApplyResources(this.label5, "label5");
			this.label5.Name = "label5";
			resources.ApplyResources(this.CollisionLabel, "CollisionLabel");
			this.CollisionLabel.Name = "CollisionLabel";
			resources.ApplyResources(this.label7, "label7");
			this.label7.Name = "label7";
			this.visObjTimer.Interval = 300;
			resources.ApplyResources(this.CreateButton, "CreateButton");
			this.CreateButton.Name = "CreateButton";
			this.CreateButton.UseVisualStyleBackColor = true;
			resources.ApplyResources(this.ExploitingTextBox, "ExploitingTextBox");
			this.ExploitingTextBox.Name = "ExploitingTextBox";
			resources.ApplyResources(this.label6, "label6");
			this.label6.Name = "label6";
			resources.ApplyResources(this.SpawnTableMapPanel, "SpawnTableMapPanel");
			this.SpawnTableMapPanel.Controls.Add(this.MapComboBox);
			this.SpawnTableMapPanel.Controls.Add(this.label32);
			this.SpawnTableMapPanel.Name = "SpawnTableMapPanel";
			resources.ApplyResources(this.MapComboBox, "MapComboBox");
			this.MapComboBox.DropDownStyle = global::System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.MapComboBox.FormattingEnabled = true;
			this.MapComboBox.Name = "MapComboBox";
			this.MapComboBox.Sorted = true;
			resources.ApplyResources(this.label32, "label32");
			this.label32.Name = "label32";
			this.panel5.Controls.Add(this.ExistingSTRadioButton);
			this.panel5.Controls.Add(this.NewSTRadioButton);
			resources.ApplyResources(this.panel5, "panel5");
			this.panel5.Name = "panel5";
			resources.ApplyResources(this.ExistingSTRadioButton, "ExistingSTRadioButton");
			this.ExistingSTRadioButton.Name = "ExistingSTRadioButton";
			this.ExistingSTRadioButton.UseVisualStyleBackColor = true;
			resources.ApplyResources(this.NewSTRadioButton, "NewSTRadioButton");
			this.NewSTRadioButton.Checked = true;
			this.NewSTRadioButton.Name = "NewSTRadioButton";
			this.NewSTRadioButton.TabStop = true;
			this.NewSTRadioButton.UseVisualStyleBackColor = true;
			resources.ApplyResources(this.SpawnTablePanel, "SpawnTablePanel");
			this.SpawnTablePanel.Controls.Add(this.label33);
			this.SpawnTablePanel.Controls.Add(this.SpawnTableTextBox);
			this.SpawnTablePanel.Controls.Add(this.ChooseSpawnTablebButton);
			this.SpawnTablePanel.Name = "SpawnTablePanel";
			resources.ApplyResources(this.label33, "label33");
			this.label33.Name = "label33";
			resources.ApplyResources(this.SpawnTableTextBox, "SpawnTableTextBox");
			this.SpawnTableTextBox.Name = "SpawnTableTextBox";
			resources.ApplyResources(this.ChooseSpawnTablebButton, "ChooseSpawnTablebButton");
			this.ChooseSpawnTablebButton.Name = "ChooseSpawnTablebButton";
			this.ChooseSpawnTablebButton.UseVisualStyleBackColor = true;
			resources.ApplyResources(this.label13, "label13");
			this.label13.Name = "label13";
			resources.ApplyResources(this, "$this");
			base.AutoScaleMode = global::System.Windows.Forms.AutoScaleMode.Font;
			base.Controls.Add(this.SpawnTableMapPanel);
			base.Controls.Add(this.panel5);
			base.Controls.Add(this.SpawnTablePanel);
			base.Controls.Add(this.label13);
			base.Controls.Add(this.label6);
			base.Controls.Add(this.ExploitingTextBox);
			base.Controls.Add(this.CreateButton);
			base.Controls.Add(this.label7);
			base.Controls.Add(this.CollisionLabel);
			base.Controls.Add(this.label5);
			base.Controls.Add(this.ChooseVisObjButton);
			base.Controls.Add(this.VisObjTextBox);
			base.Controls.Add(this.ResultTextBox);
			base.Controls.Add(this.label4);
			base.Controls.Add(this.ResNameTextBox);
			base.Controls.Add(this.label3);
			base.Controls.Add(this.FileNameTextBox);
			base.Controls.Add(this.ZoneComboBox);
			base.Controls.Add(this.label2);
			base.Controls.Add(this.SteleRadioButton);
			base.Controls.Add(this.label1);
			base.Controls.Add(this.ChestRadioButton);
			base.Name = "CreateDeviceForm";
			this.SpawnTableMapPanel.ResumeLayout(false);
			this.SpawnTableMapPanel.PerformLayout();
			this.panel5.ResumeLayout(false);
			this.panel5.PerformLayout();
			this.SpawnTablePanel.ResumeLayout(false);
			this.SpawnTablePanel.PerformLayout();
			base.ResumeLayout(false);
			base.PerformLayout();
		}

		// Token: 0x04000539 RID: 1337
		private global::System.ComponentModel.IContainer components;

		// Token: 0x0400053A RID: 1338
		private global::System.Windows.Forms.RadioButton ChestRadioButton;

		// Token: 0x0400053B RID: 1339
		private global::System.Windows.Forms.Label label1;

		// Token: 0x0400053C RID: 1340
		private global::System.Windows.Forms.RadioButton SteleRadioButton;

		// Token: 0x0400053D RID: 1341
		private global::System.Windows.Forms.Label label2;

		// Token: 0x0400053E RID: 1342
		private global::System.Windows.Forms.ComboBox ZoneComboBox;

		// Token: 0x0400053F RID: 1343
		private global::System.Windows.Forms.Label label3;

		// Token: 0x04000540 RID: 1344
		private global::System.Windows.Forms.TextBox FileNameTextBox;

		// Token: 0x04000541 RID: 1345
		private global::System.Windows.Forms.Label label4;

		// Token: 0x04000542 RID: 1346
		private global::System.Windows.Forms.TextBox ResNameTextBox;

		// Token: 0x04000543 RID: 1347
		private global::System.Windows.Forms.RichTextBox ResultTextBox;

		// Token: 0x04000544 RID: 1348
		private global::System.Windows.Forms.Button ChooseVisObjButton;

		// Token: 0x04000545 RID: 1349
		private global::System.Windows.Forms.TextBox VisObjTextBox;

		// Token: 0x04000546 RID: 1350
		private global::System.Windows.Forms.Label label5;

		// Token: 0x04000547 RID: 1351
		private global::System.Windows.Forms.Label CollisionLabel;

		// Token: 0x04000548 RID: 1352
		private global::System.Windows.Forms.Label label7;

		// Token: 0x04000549 RID: 1353
		private global::System.Windows.Forms.Timer visObjTimer;

		// Token: 0x0400054A RID: 1354
		private global::System.Windows.Forms.Button CreateButton;

		// Token: 0x0400054B RID: 1355
		private global::System.Windows.Forms.TextBox ExploitingTextBox;

		// Token: 0x0400054C RID: 1356
		private global::System.Windows.Forms.Label label6;

		// Token: 0x0400054D RID: 1357
		private global::System.Windows.Forms.Panel SpawnTableMapPanel;

		// Token: 0x0400054E RID: 1358
		private global::System.Windows.Forms.ComboBox MapComboBox;

		// Token: 0x0400054F RID: 1359
		private global::System.Windows.Forms.Label label32;

		// Token: 0x04000550 RID: 1360
		private global::System.Windows.Forms.Panel panel5;

		// Token: 0x04000551 RID: 1361
		private global::System.Windows.Forms.RadioButton ExistingSTRadioButton;

		// Token: 0x04000552 RID: 1362
		private global::System.Windows.Forms.RadioButton NewSTRadioButton;

		// Token: 0x04000553 RID: 1363
		public global::System.Windows.Forms.Panel SpawnTablePanel;

		// Token: 0x04000554 RID: 1364
		private global::System.Windows.Forms.Label label33;

		// Token: 0x04000555 RID: 1365
		private global::System.Windows.Forms.TextBox SpawnTableTextBox;

		// Token: 0x04000556 RID: 1366
		private global::System.Windows.Forms.Button ChooseSpawnTablebButton;

		// Token: 0x04000557 RID: 1367
		private global::System.Windows.Forms.Label label13;
	}
}
