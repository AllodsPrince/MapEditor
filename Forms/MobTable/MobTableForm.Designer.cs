namespace MapEditor.Forms.MobTable
{
	// Token: 0x020000A4 RID: 164
	public partial class MobTableForm : global::MapEditor.Forms.Base.BaseForm
	{
		// Token: 0x060007AF RID: 1967 RVA: 0x0003C93B File Offset: 0x0003B93B
		protected override void Dispose(bool disposing)
		{
			if (disposing && this.components != null)
			{
				this.components.Dispose();
			}
			base.Dispose(disposing);
		}

		// Token: 0x060007B0 RID: 1968 RVA: 0x0003C95C File Offset: 0x0003B95C
		private void InitializeComponent()
		{
			this.label30 = new global::System.Windows.Forms.Label();
			this.zoneComboBox = new global::System.Windows.Forms.ComboBox();
			this.zoneGridView = new global::System.Windows.Forms.DataGridView();
			this.findTextBox = new global::System.Windows.Forms.TextBox();
			this.label1 = new global::System.Windows.Forms.Label();
			this.findButton = new global::System.Windows.Forms.Button();
			this.showProprtiesButton = new global::System.Windows.Forms.Button();
			this.findOnMapButton = new global::System.Windows.Forms.Button();
			this.toExcelButton = new global::System.Windows.Forms.Button();
			this.dialogEditorButton = new global::System.Windows.Forms.Button();
			this.cuesToExcelButton = new global::System.Windows.Forms.Button();
			this.panel2 = new global::System.Windows.Forms.Panel();
			this.label3 = new global::System.Windows.Forms.Label();
			this.ordinalHeroicTypeRadioButton = new global::System.Windows.Forms.RadioButton();
			this.heroicHeroicTypeRadioButton = new global::System.Windows.Forms.RadioButton();
			this.scriptEventsButton = new global::System.Windows.Forms.Button();
			this.refreshButton = new global::System.Windows.Forms.Button();
			this.allObjTypeRadioButton = new global::System.Windows.Forms.RadioButton();
			this.vendorsRadioButton = new global::System.Windows.Forms.RadioButton();
			this.label2 = new global::System.Windows.Forms.Label();
			this.panel1 = new global::System.Windows.Forms.Panel();
			this.panel3 = new global::System.Windows.Forms.Panel();
			this.label4 = new global::System.Windows.Forms.Label();
			this.DeviceTableRadioButton = new global::System.Windows.Forms.RadioButton();
			this.mobTableRadioButton = new global::System.Windows.Forms.RadioButton();
			((global::System.ComponentModel.ISupportInitialize)this.zoneGridView).BeginInit();
			this.panel2.SuspendLayout();
			this.panel1.SuspendLayout();
			this.panel3.SuspendLayout();
			base.SuspendLayout();
			this.label30.AutoSize = true;
			this.label30.ImeMode = global::System.Windows.Forms.ImeMode.NoControl;
			this.label30.Location = new global::System.Drawing.Point(2, 32);
			this.label30.Name = "label30";
			this.label30.Size = new global::System.Drawing.Size(35, 13);
			this.label30.TabIndex = 25;
			this.label30.Text = "Zone:";
			this.zoneComboBox.DropDownHeight = 206;
			this.zoneComboBox.DropDownStyle = global::System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.zoneComboBox.FormattingEnabled = true;
			this.zoneComboBox.IntegralHeight = false;
			this.zoneComboBox.Location = new global::System.Drawing.Point(73, 29);
			this.zoneComboBox.Name = "zoneComboBox";
			this.zoneComboBox.Size = new global::System.Drawing.Size(227, 21);
			this.zoneComboBox.Sorted = true;
			this.zoneComboBox.TabIndex = 24;
			this.zoneGridView.AllowUserToAddRows = false;
			this.zoneGridView.AllowUserToDeleteRows = false;
			this.zoneGridView.Anchor = (global::System.Windows.Forms.AnchorStyles.Top | global::System.Windows.Forms.AnchorStyles.Bottom | global::System.Windows.Forms.AnchorStyles.Left | global::System.Windows.Forms.AnchorStyles.Right);
			this.zoneGridView.AutoSizeColumnsMode = global::System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
			this.zoneGridView.ColumnHeadersHeightSizeMode = global::System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.zoneGridView.Location = new global::System.Drawing.Point(3, 86);
			this.zoneGridView.MultiSelect = false;
			this.zoneGridView.Name = "zoneGridView";
			this.zoneGridView.Size = new global::System.Drawing.Size(554, 259);
			this.zoneGridView.TabIndex = 26;
			this.findTextBox.Location = new global::System.Drawing.Point(73, 55);
			this.findTextBox.Name = "findTextBox";
			this.findTextBox.Size = new global::System.Drawing.Size(227, 20);
			this.findTextBox.TabIndex = 29;
			this.label1.AutoSize = true;
			this.label1.Location = new global::System.Drawing.Point(2, 58);
			this.label1.Name = "label1";
			this.label1.Size = new global::System.Drawing.Size(66, 13);
			this.label1.TabIndex = 30;
			this.label1.Text = "Find in XDB:";
			this.findButton.Location = new global::System.Drawing.Point(303, 53);
			this.findButton.Name = "findButton";
			this.findButton.Size = new global::System.Drawing.Size(53, 23);
			this.findButton.TabIndex = 31;
			this.findButton.Text = "Find";
			this.findButton.UseVisualStyleBackColor = true;
			this.showProprtiesButton.Anchor = (global::System.Windows.Forms.AnchorStyles.Bottom | global::System.Windows.Forms.AnchorStyles.Left);
			this.showProprtiesButton.Location = new global::System.Drawing.Point(3, 351);
			this.showProprtiesButton.Name = "showProprtiesButton";
			this.showProprtiesButton.Size = new global::System.Drawing.Size(75, 23);
			this.showProprtiesButton.TabIndex = 32;
			this.showProprtiesButton.Text = "Properties";
			this.showProprtiesButton.UseVisualStyleBackColor = true;
			this.findOnMapButton.Anchor = (global::System.Windows.Forms.AnchorStyles.Bottom | global::System.Windows.Forms.AnchorStyles.Left);
			this.findOnMapButton.Location = new global::System.Drawing.Point(84, 351);
			this.findOnMapButton.Name = "findOnMapButton";
			this.findOnMapButton.Size = new global::System.Drawing.Size(75, 23);
			this.findOnMapButton.TabIndex = 33;
			this.findOnMapButton.Text = "Find on map";
			this.findOnMapButton.UseVisualStyleBackColor = true;
			this.toExcelButton.Anchor = (global::System.Windows.Forms.AnchorStyles.Bottom | global::System.Windows.Forms.AnchorStyles.Left);
			this.toExcelButton.Location = new global::System.Drawing.Point(362, 351);
			this.toExcelButton.Name = "toExcelButton";
			this.toExcelButton.Size = new global::System.Drawing.Size(75, 23);
			this.toExcelButton.TabIndex = 34;
			this.toExcelButton.Text = "Excel";
			this.toExcelButton.UseVisualStyleBackColor = true;
			this.dialogEditorButton.Anchor = (global::System.Windows.Forms.AnchorStyles.Bottom | global::System.Windows.Forms.AnchorStyles.Left);
			this.dialogEditorButton.Location = new global::System.Drawing.Point(165, 351);
			this.dialogEditorButton.Name = "dialogEditorButton";
			this.dialogEditorButton.Size = new global::System.Drawing.Size(75, 23);
			this.dialogEditorButton.TabIndex = 35;
			this.dialogEditorButton.Text = "Dialogs ...";
			this.dialogEditorButton.UseVisualStyleBackColor = true;
			this.cuesToExcelButton.Anchor = (global::System.Windows.Forms.AnchorStyles.Bottom | global::System.Windows.Forms.AnchorStyles.Left);
			this.cuesToExcelButton.Location = new global::System.Drawing.Point(455, 351);
			this.cuesToExcelButton.Name = "cuesToExcelButton";
			this.cuesToExcelButton.Size = new global::System.Drawing.Size(75, 23);
			this.cuesToExcelButton.TabIndex = 36;
			this.cuesToExcelButton.Text = "Excel Cues";
			this.cuesToExcelButton.UseVisualStyleBackColor = true;
			this.panel2.Controls.Add(this.label3);
			this.panel2.Controls.Add(this.ordinalHeroicTypeRadioButton);
			this.panel2.Controls.Add(this.heroicHeroicTypeRadioButton);
			this.panel2.Location = new global::System.Drawing.Point(362, 45);
			this.panel2.Name = "panel2";
			this.panel2.Size = new global::System.Drawing.Size(195, 21);
			this.panel2.TabIndex = 38;
			this.label3.AutoSize = true;
			this.label3.Location = new global::System.Drawing.Point(3, 3);
			this.label3.Name = "label3";
			this.label3.Size = new global::System.Drawing.Size(64, 13);
			this.label3.TabIndex = 29;
			this.label3.Text = "Heroic type:";
			this.ordinalHeroicTypeRadioButton.AutoSize = true;
			this.ordinalHeroicTypeRadioButton.Checked = true;
			this.ordinalHeroicTypeRadioButton.Location = new global::System.Drawing.Point(69, 1);
			this.ordinalHeroicTypeRadioButton.Name = "ordinalHeroicTypeRadioButton";
			this.ordinalHeroicTypeRadioButton.Size = new global::System.Drawing.Size(58, 17);
			this.ordinalHeroicTypeRadioButton.TabIndex = 28;
			this.ordinalHeroicTypeRadioButton.TabStop = true;
			this.ordinalHeroicTypeRadioButton.Tag = "";
			this.ordinalHeroicTypeRadioButton.Text = "Ordinal";
			this.ordinalHeroicTypeRadioButton.UseVisualStyleBackColor = true;
			this.heroicHeroicTypeRadioButton.AutoSize = true;
			this.heroicHeroicTypeRadioButton.Location = new global::System.Drawing.Point(128, 1);
			this.heroicHeroicTypeRadioButton.Name = "heroicHeroicTypeRadioButton";
			this.heroicHeroicTypeRadioButton.Size = new global::System.Drawing.Size(56, 17);
			this.heroicHeroicTypeRadioButton.TabIndex = 27;
			this.heroicHeroicTypeRadioButton.Text = "Heroic";
			this.heroicHeroicTypeRadioButton.UseVisualStyleBackColor = true;
			this.scriptEventsButton.Anchor = (global::System.Windows.Forms.AnchorStyles.Bottom | global::System.Windows.Forms.AnchorStyles.Left);
			this.scriptEventsButton.Location = new global::System.Drawing.Point(246, 351);
			this.scriptEventsButton.Name = "scriptEventsButton";
			this.scriptEventsButton.Size = new global::System.Drawing.Size(75, 23);
			this.scriptEventsButton.TabIndex = 39;
			this.scriptEventsButton.Text = "Scr. events";
			this.scriptEventsButton.UseVisualStyleBackColor = true;
			this.refreshButton.Enabled = false;
			this.refreshButton.Location = new global::System.Drawing.Point(303, 27);
			this.refreshButton.Name = "refreshButton";
			this.refreshButton.Size = new global::System.Drawing.Size(53, 23);
			this.refreshButton.TabIndex = 40;
			this.refreshButton.Text = "Refresh";
			this.refreshButton.UseVisualStyleBackColor = true;
			this.allObjTypeRadioButton.AutoSize = true;
			this.allObjTypeRadioButton.Checked = true;
			this.allObjTypeRadioButton.Location = new global::System.Drawing.Point(69, 1);
			this.allObjTypeRadioButton.Name = "allObjTypeRadioButton";
			this.allObjTypeRadioButton.Size = new global::System.Drawing.Size(36, 17);
			this.allObjTypeRadioButton.TabIndex = 27;
			this.allObjTypeRadioButton.TabStop = true;
			this.allObjTypeRadioButton.Text = "All";
			this.allObjTypeRadioButton.UseVisualStyleBackColor = true;
			this.vendorsRadioButton.AutoSize = true;
			this.vendorsRadioButton.Location = new global::System.Drawing.Point(111, 1);
			this.vendorsRadioButton.Name = "vendorsRadioButton";
			this.vendorsRadioButton.Size = new global::System.Drawing.Size(64, 17);
			this.vendorsRadioButton.TabIndex = 28;
			this.vendorsRadioButton.TabStop = true;
			this.vendorsRadioButton.Tag = "";
			this.vendorsRadioButton.Text = "Vendors";
			this.vendorsRadioButton.UseVisualStyleBackColor = true;
			this.label2.AutoSize = true;
			this.label2.Location = new global::System.Drawing.Point(3, 3);
			this.label2.Name = "label2";
			this.label2.Size = new global::System.Drawing.Size(64, 13);
			this.label2.TabIndex = 29;
			this.label2.Text = "Object type:";
			this.panel1.Controls.Add(this.label2);
			this.panel1.Controls.Add(this.vendorsRadioButton);
			this.panel1.Controls.Add(this.allObjTypeRadioButton);
			this.panel1.Location = new global::System.Drawing.Point(362, 26);
			this.panel1.Name = "panel1";
			this.panel1.Size = new global::System.Drawing.Size(187, 21);
			this.panel1.TabIndex = 37;
			this.panel3.Controls.Add(this.label4);
			this.panel3.Controls.Add(this.DeviceTableRadioButton);
			this.panel3.Controls.Add(this.mobTableRadioButton);
			this.panel3.Location = new global::System.Drawing.Point(5, 2);
			this.panel3.Name = "panel3";
			this.panel3.Size = new global::System.Drawing.Size(331, 21);
			this.panel3.TabIndex = 41;
			this.label4.AutoSize = true;
			this.label4.Location = new global::System.Drawing.Point(2, 3);
			this.label4.Name = "label4";
			this.label4.Size = new global::System.Drawing.Size(60, 13);
			this.label4.TabIndex = 30;
			this.label4.Text = "Table type:";
			this.DeviceTableRadioButton.AutoSize = true;
			this.DeviceTableRadioButton.Location = new global::System.Drawing.Point(120, 1);
			this.DeviceTableRadioButton.Name = "DeviceTableRadioButton";
			this.DeviceTableRadioButton.Size = new global::System.Drawing.Size(59, 17);
			this.DeviceTableRadioButton.TabIndex = 28;
			this.DeviceTableRadioButton.TabStop = true;
			this.DeviceTableRadioButton.Tag = "";
			this.DeviceTableRadioButton.Text = "Device";
			this.DeviceTableRadioButton.UseVisualStyleBackColor = true;
			this.mobTableRadioButton.AutoSize = true;
			this.mobTableRadioButton.Checked = true;
			this.mobTableRadioButton.Location = new global::System.Drawing.Point(68, 1);
			this.mobTableRadioButton.Name = "mobTableRadioButton";
			this.mobTableRadioButton.Size = new global::System.Drawing.Size(46, 17);
			this.mobTableRadioButton.TabIndex = 27;
			this.mobTableRadioButton.TabStop = true;
			this.mobTableRadioButton.Text = "Mob";
			this.mobTableRadioButton.UseVisualStyleBackColor = true;
			base.AutoScaleDimensions = new global::System.Drawing.SizeF(6f, 13f);
			base.AutoScaleMode = global::System.Windows.Forms.AutoScaleMode.Font;
			base.ClientSize = new global::System.Drawing.Size(560, 377);
			base.Controls.Add(this.panel3);
			base.Controls.Add(this.refreshButton);
			base.Controls.Add(this.scriptEventsButton);
			base.Controls.Add(this.panel2);
			base.Controls.Add(this.panel1);
			base.Controls.Add(this.cuesToExcelButton);
			base.Controls.Add(this.dialogEditorButton);
			base.Controls.Add(this.toExcelButton);
			base.Controls.Add(this.findOnMapButton);
			base.Controls.Add(this.showProprtiesButton);
			base.Controls.Add(this.findButton);
			base.Controls.Add(this.label1);
			base.Controls.Add(this.findTextBox);
			base.Controls.Add(this.zoneGridView);
			base.Controls.Add(this.label30);
			base.Controls.Add(this.zoneComboBox);
			base.Name = "MobTableForm";
			this.Text = "Mob Table";
			((global::System.ComponentModel.ISupportInitialize)this.zoneGridView).EndInit();
			this.panel2.ResumeLayout(false);
			this.panel2.PerformLayout();
			this.panel1.ResumeLayout(false);
			this.panel1.PerformLayout();
			this.panel3.ResumeLayout(false);
			this.panel3.PerformLayout();
			base.ResumeLayout(false);
			base.PerformLayout();
		}

		// Token: 0x04000590 RID: 1424
		private global::System.ComponentModel.IContainer components;

		// Token: 0x04000591 RID: 1425
		private global::System.Windows.Forms.Label label30;

		// Token: 0x04000592 RID: 1426
		private global::System.Windows.Forms.ComboBox zoneComboBox;

		// Token: 0x04000593 RID: 1427
		private global::System.Windows.Forms.DataGridView zoneGridView;

		// Token: 0x04000594 RID: 1428
		private global::System.Windows.Forms.TextBox findTextBox;

		// Token: 0x04000595 RID: 1429
		private global::System.Windows.Forms.Label label1;

		// Token: 0x04000596 RID: 1430
		private global::System.Windows.Forms.Button findButton;

		// Token: 0x04000597 RID: 1431
		private global::System.Windows.Forms.Button showProprtiesButton;

		// Token: 0x04000598 RID: 1432
		private global::System.Windows.Forms.Button findOnMapButton;

		// Token: 0x04000599 RID: 1433
		private global::System.Windows.Forms.Button toExcelButton;

		// Token: 0x0400059A RID: 1434
		private global::System.Windows.Forms.Button dialogEditorButton;

		// Token: 0x0400059B RID: 1435
		private global::System.Windows.Forms.Button cuesToExcelButton;

		// Token: 0x0400059C RID: 1436
		private global::System.Windows.Forms.Panel panel2;

		// Token: 0x0400059D RID: 1437
		private global::System.Windows.Forms.Label label3;

		// Token: 0x0400059E RID: 1438
		private global::System.Windows.Forms.RadioButton ordinalHeroicTypeRadioButton;

		// Token: 0x0400059F RID: 1439
		private global::System.Windows.Forms.RadioButton heroicHeroicTypeRadioButton;

		// Token: 0x040005A0 RID: 1440
		private global::System.Windows.Forms.Button scriptEventsButton;

		// Token: 0x040005A1 RID: 1441
		private global::System.Windows.Forms.Button refreshButton;

		// Token: 0x040005A2 RID: 1442
		private global::System.Windows.Forms.RadioButton allObjTypeRadioButton;

		// Token: 0x040005A3 RID: 1443
		private global::System.Windows.Forms.RadioButton vendorsRadioButton;

		// Token: 0x040005A4 RID: 1444
		private global::System.Windows.Forms.Label label2;

		// Token: 0x040005A5 RID: 1445
		private global::System.Windows.Forms.Panel panel1;

		// Token: 0x040005A6 RID: 1446
		private global::System.Windows.Forms.Panel panel3;

		// Token: 0x040005A7 RID: 1447
		private global::System.Windows.Forms.Label label4;

		// Token: 0x040005A8 RID: 1448
		private global::System.Windows.Forms.RadioButton DeviceTableRadioButton;

		// Token: 0x040005A9 RID: 1449
		private global::System.Windows.Forms.RadioButton mobTableRadioButton;
	}
}
