namespace MapEditor.Forms.Quests
{
	// Token: 0x02000015 RID: 21
	public partial class QuestSelectDialogForm : global::System.Windows.Forms.Form
	{
		// Token: 0x06000207 RID: 519 RVA: 0x0001650C File Offset: 0x0001550C
		protected override void Dispose(bool disposing)
		{
			if (disposing && this.components != null)
			{
				this.components.Dispose();
			}
			base.Dispose(disposing);
		}

		// Token: 0x06000208 RID: 520 RVA: 0x0001652C File Offset: 0x0001552C
		private void InitializeComponent()
		{
			global::System.ComponentModel.ComponentResourceManager resources = new global::System.ComponentModel.ComponentResourceManager(typeof(global::MapEditor.Forms.Quests.QuestSelectDialogForm));
			this.label16 = new global::System.Windows.Forms.Label();
			this.ZoneComboBox = new global::System.Windows.Forms.ComboBox();
			this.label18 = new global::System.Windows.Forms.Label();
			this.GameNameTextBox = new global::System.Windows.Forms.TextBox();
			this.label17 = new global::System.Windows.Forms.Label();
			this.NameTextBox = new global::System.Windows.Forms.TextBox();
			this.LevelFromTextBox = new global::System.Windows.Forms.TextBox();
			this.label4 = new global::System.Windows.Forms.Label();
			this.LevelToTextBox = new global::System.Windows.Forms.TextBox();
			this.label1 = new global::System.Windows.Forms.Label();
			this.label2 = new global::System.Windows.Forms.Label();
			this.CancelSelectButton = new global::System.Windows.Forms.Button();
			this.FindButton = new global::System.Windows.Forms.Button();
			this.ItemsView = new global::System.Windows.Forms.DataGridView();
			this.Column0 = new global::System.Windows.Forms.DataGridViewTextBoxColumn();
			this.Column1 = new global::System.Windows.Forms.DataGridViewTextBoxColumn();
			this.Column3 = new global::System.Windows.Forms.DataGridViewTextBoxColumn();
			this.Column4 = new global::System.Windows.Forms.DataGridViewTextBoxColumn();
			this.ClearSelectButton = new global::System.Windows.Forms.Button();
			this.label3 = new global::System.Windows.Forms.Label();
			this.TypesComboBox = new global::System.Windows.Forms.ComboBox();
			this.label5 = new global::System.Windows.Forms.Label();
			this.DBIDTextBox = new global::System.Windows.Forms.TextBox();
			this.ExportButton = new global::System.Windows.Forms.Button();
			this.dataGridViewTextBoxColumn1 = new global::System.Windows.Forms.DataGridViewTextBoxColumn();
			this.dataGridViewTextBoxColumn2 = new global::System.Windows.Forms.DataGridViewTextBoxColumn();
			this.dataGridViewTextBoxColumn3 = new global::System.Windows.Forms.DataGridViewTextBoxColumn();
			this.dataGridViewTextBoxColumn4 = new global::System.Windows.Forms.DataGridViewTextBoxColumn();
			this.dataGridViewTextBoxColumn5 = new global::System.Windows.Forms.DataGridViewTextBoxColumn();
			((global::System.ComponentModel.ISupportInitialize)this.ItemsView).BeginInit();
			base.SuspendLayout();
			resources.ApplyResources(this.label16, "label16");
			this.label16.Name = "label16";
			this.ZoneComboBox.DropDownStyle = global::System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.ZoneComboBox.FormattingEnabled = true;
			resources.ApplyResources(this.ZoneComboBox, "ZoneComboBox");
			this.ZoneComboBox.Name = "ZoneComboBox";
			resources.ApplyResources(this.label18, "label18");
			this.label18.Name = "label18";
			resources.ApplyResources(this.GameNameTextBox, "GameNameTextBox");
			this.GameNameTextBox.Name = "GameNameTextBox";
			resources.ApplyResources(this.label17, "label17");
			this.label17.Name = "label17";
			resources.ApplyResources(this.NameTextBox, "NameTextBox");
			this.NameTextBox.Name = "NameTextBox";
			resources.ApplyResources(this.LevelFromTextBox, "LevelFromTextBox");
			this.LevelFromTextBox.Name = "LevelFromTextBox";
			resources.ApplyResources(this.label4, "label4");
			this.label4.Name = "label4";
			resources.ApplyResources(this.LevelToTextBox, "LevelToTextBox");
			this.LevelToTextBox.Name = "LevelToTextBox";
			resources.ApplyResources(this.label1, "label1");
			this.label1.Name = "label1";
			resources.ApplyResources(this.label2, "label2");
			this.label2.Name = "label2";
			resources.ApplyResources(this.CancelSelectButton, "CancelSelectButton");
			this.CancelSelectButton.Name = "CancelSelectButton";
			this.CancelSelectButton.UseVisualStyleBackColor = true;
			resources.ApplyResources(this.FindButton, "FindButton");
			this.FindButton.Name = "FindButton";
			this.FindButton.UseVisualStyleBackColor = true;
			this.ItemsView.AllowUserToAddRows = false;
			this.ItemsView.AllowUserToDeleteRows = false;
			resources.ApplyResources(this.ItemsView, "ItemsView");
			this.ItemsView.ColumnHeadersHeightSizeMode = global::System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.ItemsView.Columns.AddRange(new global::System.Windows.Forms.DataGridViewColumn[]
			{
				this.Column0,
				this.Column1,
				this.Column3,
				this.Column4
			});
			this.ItemsView.MultiSelect = false;
			this.ItemsView.Name = "ItemsView";
			this.ItemsView.ReadOnly = true;
			resources.ApplyResources(this.Column0, "Column0");
			this.Column0.Name = "Column0";
			this.Column0.ReadOnly = true;
			resources.ApplyResources(this.Column1, "Column1");
			this.Column1.Name = "Column1";
			this.Column1.ReadOnly = true;
			resources.ApplyResources(this.Column3, "Column3");
			this.Column3.Name = "Column3";
			this.Column3.ReadOnly = true;
			resources.ApplyResources(this.Column4, "Column4");
			this.Column4.Name = "Column4";
			this.Column4.ReadOnly = true;
			resources.ApplyResources(this.ClearSelectButton, "ClearSelectButton");
			this.ClearSelectButton.Name = "ClearSelectButton";
			this.ClearSelectButton.UseVisualStyleBackColor = true;
			resources.ApplyResources(this.label3, "label3");
			this.label3.Name = "label3";
			this.TypesComboBox.DropDownStyle = global::System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.TypesComboBox.FormattingEnabled = true;
			resources.ApplyResources(this.TypesComboBox, "TypesComboBox");
			this.TypesComboBox.Name = "TypesComboBox";
			resources.ApplyResources(this.label5, "label5");
			this.label5.Name = "label5";
			resources.ApplyResources(this.DBIDTextBox, "DBIDTextBox");
			this.DBIDTextBox.Name = "DBIDTextBox";
			resources.ApplyResources(this.ExportButton, "ExportButton");
			this.ExportButton.Name = "ExportButton";
			this.ExportButton.UseVisualStyleBackColor = true;
			resources.ApplyResources(this.dataGridViewTextBoxColumn1, "dataGridViewTextBoxColumn1");
			this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
			resources.ApplyResources(this.dataGridViewTextBoxColumn2, "dataGridViewTextBoxColumn2");
			this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
			resources.ApplyResources(this.dataGridViewTextBoxColumn3, "dataGridViewTextBoxColumn3");
			this.dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
			resources.ApplyResources(this.dataGridViewTextBoxColumn4, "dataGridViewTextBoxColumn4");
			this.dataGridViewTextBoxColumn4.Name = "dataGridViewTextBoxColumn4";
			resources.ApplyResources(this.dataGridViewTextBoxColumn5, "dataGridViewTextBoxColumn5");
			this.dataGridViewTextBoxColumn5.Name = "dataGridViewTextBoxColumn5";
			base.AcceptButton = this.FindButton;
			resources.ApplyResources(this, "$this");
			base.AutoScaleMode = global::System.Windows.Forms.AutoScaleMode.Font;
			base.Controls.Add(this.ExportButton);
			base.Controls.Add(this.label5);
			base.Controls.Add(this.DBIDTextBox);
			base.Controls.Add(this.label3);
			base.Controls.Add(this.TypesComboBox);
			base.Controls.Add(this.ClearSelectButton);
			base.Controls.Add(this.ItemsView);
			base.Controls.Add(this.FindButton);
			base.Controls.Add(this.CancelSelectButton);
			base.Controls.Add(this.label2);
			base.Controls.Add(this.label1);
			base.Controls.Add(this.LevelToTextBox);
			base.Controls.Add(this.label18);
			base.Controls.Add(this.GameNameTextBox);
			base.Controls.Add(this.label17);
			base.Controls.Add(this.NameTextBox);
			base.Controls.Add(this.LevelFromTextBox);
			base.Controls.Add(this.label4);
			base.Controls.Add(this.label16);
			base.Controls.Add(this.ZoneComboBox);
			base.Name = "QuestSelectDialogForm";
			((global::System.ComponentModel.ISupportInitialize)this.ItemsView).EndInit();
			base.ResumeLayout(false);
			base.PerformLayout();
		}

		// Token: 0x040001A6 RID: 422
		private global::System.ComponentModel.IContainer components;

		// Token: 0x040001A7 RID: 423
		private global::System.Windows.Forms.Label label16;

		// Token: 0x040001A8 RID: 424
		private global::System.Windows.Forms.ComboBox ZoneComboBox;

		// Token: 0x040001A9 RID: 425
		private global::System.Windows.Forms.Label label18;

		// Token: 0x040001AA RID: 426
		private global::System.Windows.Forms.TextBox GameNameTextBox;

		// Token: 0x040001AB RID: 427
		private global::System.Windows.Forms.Label label17;

		// Token: 0x040001AC RID: 428
		private global::System.Windows.Forms.TextBox NameTextBox;

		// Token: 0x040001AD RID: 429
		private global::System.Windows.Forms.TextBox LevelFromTextBox;

		// Token: 0x040001AE RID: 430
		private global::System.Windows.Forms.Label label4;

		// Token: 0x040001AF RID: 431
		private global::System.Windows.Forms.TextBox LevelToTextBox;

		// Token: 0x040001B0 RID: 432
		private global::System.Windows.Forms.Label label1;

		// Token: 0x040001B1 RID: 433
		private global::System.Windows.Forms.Label label2;

		// Token: 0x040001B2 RID: 434
		private global::System.Windows.Forms.Button CancelSelectButton;

		// Token: 0x040001B3 RID: 435
		private global::System.Windows.Forms.Button FindButton;

		// Token: 0x040001B4 RID: 436
		private global::System.Windows.Forms.DataGridView ItemsView;

		// Token: 0x040001B5 RID: 437
		private global::System.Windows.Forms.Button ClearSelectButton;

		// Token: 0x040001B6 RID: 438
		private global::System.Windows.Forms.Label label3;

		// Token: 0x040001B7 RID: 439
		private global::System.Windows.Forms.ComboBox TypesComboBox;

		// Token: 0x040001B8 RID: 440
		private global::System.Windows.Forms.Label label5;

		// Token: 0x040001B9 RID: 441
		private global::System.Windows.Forms.TextBox DBIDTextBox;

		// Token: 0x040001BA RID: 442
		private global::System.Windows.Forms.Button ExportButton;

		// Token: 0x040001BB RID: 443
		private global::System.Windows.Forms.DataGridViewTextBoxColumn Column0;

		// Token: 0x040001BC RID: 444
		private global::System.Windows.Forms.DataGridViewTextBoxColumn Column1;

		// Token: 0x040001BD RID: 445
		private global::System.Windows.Forms.DataGridViewTextBoxColumn Column3;

		// Token: 0x040001BE RID: 446
		private global::System.Windows.Forms.DataGridViewTextBoxColumn Column4;

		// Token: 0x040001BF RID: 447
		private global::System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;

		// Token: 0x040001C0 RID: 448
		private global::System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;

		// Token: 0x040001C1 RID: 449
		private global::System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;

		// Token: 0x040001C2 RID: 450
		private global::System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn4;

		// Token: 0x040001C3 RID: 451
		private global::System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn5;
	}
}
