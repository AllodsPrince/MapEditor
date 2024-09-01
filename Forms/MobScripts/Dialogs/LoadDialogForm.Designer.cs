namespace MapEditor.Forms.MobScripts.Dialogs
{
	// Token: 0x020001EA RID: 490
	public partial class LoadDialogForm : global::System.Windows.Forms.Form
	{
		// Token: 0x060018AF RID: 6319 RVA: 0x000A406C File Offset: 0x000A306C
		protected override void Dispose(bool disposing)
		{
			if (disposing && this.components != null)
			{
				this.components.Dispose();
			}
			base.Dispose(disposing);
		}

		// Token: 0x060018B0 RID: 6320 RVA: 0x000A408C File Offset: 0x000A308C
		private void InitializeComponent()
		{
			this.dbidTextBox = new global::System.Windows.Forms.TextBox();
			this.label1 = new global::System.Windows.Forms.Label();
			this.zoneComboBox = new global::System.Windows.Forms.ComboBox();
			this.label2 = new global::System.Windows.Forms.Label();
			this.dataGridView = new global::System.Windows.Forms.DataGridView();
			this.Column1 = new global::System.Windows.Forms.DataGridViewTextBoxColumn();
			this.Column2 = new global::System.Windows.Forms.DataGridViewTextBoxColumn();
			this.cancelButton = new global::System.Windows.Forms.Button();
			this.okButton = new global::System.Windows.Forms.Button();
			this.dataGridViewTextBoxColumn1 = new global::System.Windows.Forms.DataGridViewTextBoxColumn();
			this.dataGridViewTextBoxColumn2 = new global::System.Windows.Forms.DataGridViewTextBoxColumn();
			((global::System.ComponentModel.ISupportInitialize)this.dataGridView).BeginInit();
			base.SuspendLayout();
			this.dbidTextBox.Anchor = (global::System.Windows.Forms.AnchorStyles.Top | global::System.Windows.Forms.AnchorStyles.Left | global::System.Windows.Forms.AnchorStyles.Right);
			this.dbidTextBox.Location = new global::System.Drawing.Point(58, 12);
			this.dbidTextBox.Name = "dbidTextBox";
			this.dbidTextBox.Size = new global::System.Drawing.Size(450, 20);
			this.dbidTextBox.TabIndex = 0;
			this.label1.AutoSize = true;
			this.label1.Location = new global::System.Drawing.Point(1, 15);
			this.label1.Name = "label1";
			this.label1.Size = new global::System.Drawing.Size(51, 13);
			this.label1.TabIndex = 1;
			this.label1.Text = "File Path:";
			this.zoneComboBox.Anchor = (global::System.Windows.Forms.AnchorStyles.Top | global::System.Windows.Forms.AnchorStyles.Left | global::System.Windows.Forms.AnchorStyles.Right);
			this.zoneComboBox.FormattingEnabled = true;
			this.zoneComboBox.Location = new global::System.Drawing.Point(58, 38);
			this.zoneComboBox.Name = "zoneComboBox";
			this.zoneComboBox.Size = new global::System.Drawing.Size(450, 21);
			this.zoneComboBox.Sorted = true;
			this.zoneComboBox.TabIndex = 2;
			this.label2.AutoSize = true;
			this.label2.Location = new global::System.Drawing.Point(1, 41);
			this.label2.Name = "label2";
			this.label2.Size = new global::System.Drawing.Size(35, 13);
			this.label2.TabIndex = 3;
			this.label2.Text = "Zone:";
			this.dataGridView.AllowUserToAddRows = false;
			this.dataGridView.AllowUserToDeleteRows = false;
			this.dataGridView.Anchor = (global::System.Windows.Forms.AnchorStyles.Top | global::System.Windows.Forms.AnchorStyles.Bottom | global::System.Windows.Forms.AnchorStyles.Left | global::System.Windows.Forms.AnchorStyles.Right);
			this.dataGridView.ColumnHeadersHeightSizeMode = global::System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.dataGridView.Columns.AddRange(new global::System.Windows.Forms.DataGridViewColumn[]
			{
				this.Column1,
				this.Column2
			});
			this.dataGridView.Location = new global::System.Drawing.Point(4, 72);
			this.dataGridView.MultiSelect = false;
			this.dataGridView.Name = "dataGridView";
			this.dataGridView.ReadOnly = true;
			this.dataGridView.SelectionMode = global::System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
			this.dataGridView.Size = new global::System.Drawing.Size(504, 171);
			this.dataGridView.TabIndex = 4;
			this.Column1.HeaderText = "FileName";
			this.Column1.Name = "Column1";
			this.Column1.ReadOnly = true;
			this.Column1.Width = 150;
			this.Column2.HeaderText = "FilePath";
			this.Column2.Name = "Column2";
			this.Column2.ReadOnly = true;
			this.Column2.Width = 300;
			this.cancelButton.Anchor = (global::System.Windows.Forms.AnchorStyles.Bottom | global::System.Windows.Forms.AnchorStyles.Right);
			this.cancelButton.DialogResult = global::System.Windows.Forms.DialogResult.Cancel;
			this.cancelButton.Location = new global::System.Drawing.Point(424, 247);
			this.cancelButton.Name = "cancelButton";
			this.cancelButton.Size = new global::System.Drawing.Size(75, 23);
			this.cancelButton.TabIndex = 5;
			this.cancelButton.Text = "Cancel";
			this.cancelButton.UseVisualStyleBackColor = true;
			this.okButton.Anchor = (global::System.Windows.Forms.AnchorStyles.Bottom | global::System.Windows.Forms.AnchorStyles.Right);
			this.okButton.DialogResult = global::System.Windows.Forms.DialogResult.OK;
			this.okButton.Enabled = false;
			this.okButton.Location = new global::System.Drawing.Point(343, 247);
			this.okButton.Name = "okButton";
			this.okButton.Size = new global::System.Drawing.Size(75, 23);
			this.okButton.TabIndex = 6;
			this.okButton.Text = "Ok";
			this.okButton.UseVisualStyleBackColor = true;
			this.dataGridViewTextBoxColumn1.HeaderText = "FileName";
			this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
			this.dataGridViewTextBoxColumn1.Width = 150;
			this.dataGridViewTextBoxColumn2.HeaderText = "FilePath";
			this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
			this.dataGridViewTextBoxColumn2.Width = 300;
			base.AcceptButton = this.okButton;
			base.AutoScaleDimensions = new global::System.Drawing.SizeF(6f, 13f);
			base.AutoScaleMode = global::System.Windows.Forms.AutoScaleMode.Font;
			base.CancelButton = this.cancelButton;
			base.ClientSize = new global::System.Drawing.Size(511, 273);
			base.Controls.Add(this.okButton);
			base.Controls.Add(this.cancelButton);
			base.Controls.Add(this.dataGridView);
			base.Controls.Add(this.label2);
			base.Controls.Add(this.zoneComboBox);
			base.Controls.Add(this.label1);
			base.Controls.Add(this.dbidTextBox);
			base.Name = "LoadDialogForm";
			this.Text = "Load  Mob";
			((global::System.ComponentModel.ISupportInitialize)this.dataGridView).EndInit();
			base.ResumeLayout(false);
			base.PerformLayout();
		}

		// Token: 0x04001003 RID: 4099
		private global::System.ComponentModel.IContainer components;

		// Token: 0x04001004 RID: 4100
		private global::System.Windows.Forms.TextBox dbidTextBox;

		// Token: 0x04001005 RID: 4101
		private global::System.Windows.Forms.Label label1;

		// Token: 0x04001006 RID: 4102
		private global::System.Windows.Forms.ComboBox zoneComboBox;

		// Token: 0x04001007 RID: 4103
		private global::System.Windows.Forms.Label label2;

		// Token: 0x04001008 RID: 4104
		private global::System.Windows.Forms.DataGridView dataGridView;

		// Token: 0x04001009 RID: 4105
		private global::System.Windows.Forms.DataGridViewTextBoxColumn Column1;

		// Token: 0x0400100A RID: 4106
		private global::System.Windows.Forms.DataGridViewTextBoxColumn Column2;

		// Token: 0x0400100B RID: 4107
		private global::System.Windows.Forms.Button cancelButton;

		// Token: 0x0400100C RID: 4108
		private global::System.Windows.Forms.Button okButton;

		// Token: 0x0400100D RID: 4109
		private global::System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;

		// Token: 0x0400100E RID: 4110
		private global::System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
	}
}
