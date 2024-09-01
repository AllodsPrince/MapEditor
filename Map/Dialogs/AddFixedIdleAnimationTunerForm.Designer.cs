namespace MapEditor.Map.Dialogs
{
	// Token: 0x02000093 RID: 147
	public partial class AddFixedIdleAnimationTunerForm : global::System.Windows.Forms.Form
	{
		// Token: 0x06000701 RID: 1793 RVA: 0x00037228 File Offset: 0x00036228
		protected override void Dispose(bool disposing)
		{
			if (disposing && this.components != null)
			{
				this.components.Dispose();
			}
			base.Dispose(disposing);
		}

		// Token: 0x06000702 RID: 1794 RVA: 0x00037248 File Offset: 0x00036248
		private void InitializeComponent()
		{
			global::System.ComponentModel.ComponentResourceManager resources = new global::System.ComponentModel.ComponentResourceManager(typeof(global::MapEditor.Map.Dialogs.AddFixedIdleAnimationTunerForm));
			this.MainSplitContainer = new global::System.Windows.Forms.SplitContainer();
			this.ForceCreateImpactCheckBox = new global::System.Windows.Forms.CheckBox();
			this.SpawnTunerNameTextBox = new global::System.Windows.Forms.TextBox();
			this.SpawnTunerLabel = new global::System.Windows.Forms.Label();
			this.ImpactListView = new global::System.Windows.Forms.ListView();
			this.ColumnHeader00 = new global::System.Windows.Forms.ColumnHeader();
			this.ColumnHeader01 = new global::System.Windows.Forms.ColumnHeader();
			this.ColumnHeader02 = new global::System.Windows.Forms.ColumnHeader();
			this.ImpactLabel = new global::System.Windows.Forms.Label();
			this.ForceCreateVisualStateCheckBox = new global::System.Windows.Forms.CheckBox();
			this.VisualStateNameTextBox = new global::System.Windows.Forms.TextBox();
			this.VisualStateNameLabel = new global::System.Windows.Forms.Label();
			this.VisualStateListView = new global::System.Windows.Forms.ListView();
			this.ColumnHeader10 = new global::System.Windows.Forms.ColumnHeader();
			this.ColumnHeader11 = new global::System.Windows.Forms.ColumnHeader();
			this.ColumnHeader12 = new global::System.Windows.Forms.ColumnHeader();
			this.ColumnHeader13 = new global::System.Windows.Forms.ColumnHeader();
			this.ColumnHeader14 = new global::System.Windows.Forms.ColumnHeader();
			this.VisualStateLabel = new global::System.Windows.Forms.Label();
			this._okButton = new global::System.Windows.Forms.Button();
			this._cancelButton = new global::System.Windows.Forms.Button();
			this.AnimationLabel = new global::System.Windows.Forms.Label();
			this.AnimationComboBox = new global::System.Windows.Forms.ComboBox();
			this.VisMobTextBox = new global::System.Windows.Forms.TextBox();
			this.VisMobLabel = new global::System.Windows.Forms.Label();
			this.MainSplitContainer.Panel1.SuspendLayout();
			this.MainSplitContainer.Panel2.SuspendLayout();
			this.MainSplitContainer.SuspendLayout();
			base.SuspendLayout();
			resources.ApplyResources(this.MainSplitContainer, "MainSplitContainer");
			this.MainSplitContainer.Name = "MainSplitContainer";
			this.MainSplitContainer.Panel1.Controls.Add(this.ForceCreateImpactCheckBox);
			this.MainSplitContainer.Panel1.Controls.Add(this.SpawnTunerNameTextBox);
			this.MainSplitContainer.Panel1.Controls.Add(this.SpawnTunerLabel);
			this.MainSplitContainer.Panel1.Controls.Add(this.ImpactListView);
			this.MainSplitContainer.Panel1.Controls.Add(this.ImpactLabel);
			this.MainSplitContainer.Panel2.Controls.Add(this.ForceCreateVisualStateCheckBox);
			this.MainSplitContainer.Panel2.Controls.Add(this.VisualStateNameTextBox);
			this.MainSplitContainer.Panel2.Controls.Add(this.VisualStateNameLabel);
			this.MainSplitContainer.Panel2.Controls.Add(this.VisualStateListView);
			this.MainSplitContainer.Panel2.Controls.Add(this.VisualStateLabel);
			resources.ApplyResources(this.ForceCreateImpactCheckBox, "ForceCreateImpactCheckBox");
			this.ForceCreateImpactCheckBox.Name = "ForceCreateImpactCheckBox";
			this.ForceCreateImpactCheckBox.UseVisualStyleBackColor = true;
			this.ForceCreateImpactCheckBox.CheckedChanged += new global::System.EventHandler(this.ForceCreateImpactCheckBox_CheckedChanged);
			resources.ApplyResources(this.SpawnTunerNameTextBox, "SpawnTunerNameTextBox");
			this.SpawnTunerNameTextBox.Name = "SpawnTunerNameTextBox";
			this.SpawnTunerNameTextBox.TextChanged += new global::System.EventHandler(this.SpawnTunerNameTextBox_TextChanged);
			resources.ApplyResources(this.SpawnTunerLabel, "SpawnTunerLabel");
			this.SpawnTunerLabel.Name = "SpawnTunerLabel";
			resources.ApplyResources(this.ImpactListView, "ImpactListView");
			this.ImpactListView.Columns.AddRange(new global::System.Windows.Forms.ColumnHeader[]
			{
				this.ColumnHeader00,
				this.ColumnHeader01,
				this.ColumnHeader02
			});
			this.ImpactListView.FullRowSelect = true;
			this.ImpactListView.HideSelection = false;
			this.ImpactListView.Name = "ImpactListView";
			this.ImpactListView.Sorting = global::System.Windows.Forms.SortOrder.Ascending;
			this.ImpactListView.UseCompatibleStateImageBehavior = false;
			this.ImpactListView.View = global::System.Windows.Forms.View.Details;
			resources.ApplyResources(this.ColumnHeader00, "ColumnHeader00");
			resources.ApplyResources(this.ColumnHeader01, "ColumnHeader01");
			resources.ApplyResources(this.ColumnHeader02, "ColumnHeader02");
			resources.ApplyResources(this.ImpactLabel, "ImpactLabel");
			this.ImpactLabel.Name = "ImpactLabel";
			resources.ApplyResources(this.ForceCreateVisualStateCheckBox, "ForceCreateVisualStateCheckBox");
			this.ForceCreateVisualStateCheckBox.Name = "ForceCreateVisualStateCheckBox";
			this.ForceCreateVisualStateCheckBox.UseVisualStyleBackColor = true;
			this.ForceCreateVisualStateCheckBox.CheckedChanged += new global::System.EventHandler(this.ForceCreateVisualStateCheckBox_CheckedChanged);
			resources.ApplyResources(this.VisualStateNameTextBox, "VisualStateNameTextBox");
			this.VisualStateNameTextBox.Name = "VisualStateNameTextBox";
			this.VisualStateNameTextBox.TextChanged += new global::System.EventHandler(this.VisualStateNameTextBox_TextChanged);
			resources.ApplyResources(this.VisualStateNameLabel, "VisualStateNameLabel");
			this.VisualStateNameLabel.Name = "VisualStateNameLabel";
			resources.ApplyResources(this.VisualStateListView, "VisualStateListView");
			this.VisualStateListView.Columns.AddRange(new global::System.Windows.Forms.ColumnHeader[]
			{
				this.ColumnHeader10,
				this.ColumnHeader11,
				this.ColumnHeader12,
				this.ColumnHeader13,
				this.ColumnHeader14
			});
			this.VisualStateListView.FullRowSelect = true;
			this.VisualStateListView.HideSelection = false;
			this.VisualStateListView.Name = "VisualStateListView";
			this.VisualStateListView.Sorting = global::System.Windows.Forms.SortOrder.Ascending;
			this.VisualStateListView.UseCompatibleStateImageBehavior = false;
			this.VisualStateListView.View = global::System.Windows.Forms.View.Details;
			resources.ApplyResources(this.ColumnHeader10, "ColumnHeader10");
			resources.ApplyResources(this.ColumnHeader11, "ColumnHeader11");
			resources.ApplyResources(this.ColumnHeader12, "ColumnHeader12");
			resources.ApplyResources(this.ColumnHeader13, "ColumnHeader13");
			resources.ApplyResources(this.ColumnHeader14, "ColumnHeader14");
			resources.ApplyResources(this.VisualStateLabel, "VisualStateLabel");
			this.VisualStateLabel.Name = "VisualStateLabel";
			resources.ApplyResources(this._okButton, "_okButton");
			this._okButton.DialogResult = global::System.Windows.Forms.DialogResult.OK;
			this._okButton.Name = "_okButton";
			this._okButton.UseVisualStyleBackColor = true;
			this._okButton.Click += new global::System.EventHandler(this._okButton_Click);
			resources.ApplyResources(this._cancelButton, "_cancelButton");
			this._cancelButton.DialogResult = global::System.Windows.Forms.DialogResult.Cancel;
			this._cancelButton.Name = "_cancelButton";
			this._cancelButton.UseVisualStyleBackColor = true;
			resources.ApplyResources(this.AnimationLabel, "AnimationLabel");
			this.AnimationLabel.Name = "AnimationLabel";
			resources.ApplyResources(this.AnimationComboBox, "AnimationComboBox");
			this.AnimationComboBox.DropDownStyle = global::System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.AnimationComboBox.FormattingEnabled = true;
			this.AnimationComboBox.Name = "AnimationComboBox";
			this.AnimationComboBox.Sorted = true;
			this.AnimationComboBox.TextChanged += new global::System.EventHandler(this.AnimationComboBox_TextChanged);
			resources.ApplyResources(this.VisMobTextBox, "VisMobTextBox");
			this.VisMobTextBox.Name = "VisMobTextBox";
			this.VisMobTextBox.ReadOnly = true;
			resources.ApplyResources(this.VisMobLabel, "VisMobLabel");
			this.VisMobLabel.Name = "VisMobLabel";
			base.AcceptButton = this._okButton;
			resources.ApplyResources(this, "$this");
			base.AutoScaleMode = global::System.Windows.Forms.AutoScaleMode.Font;
			base.CancelButton = this._cancelButton;
			base.Controls.Add(this.VisMobLabel);
			base.Controls.Add(this.VisMobTextBox);
			base.Controls.Add(this.MainSplitContainer);
			base.Controls.Add(this.AnimationComboBox);
			base.Controls.Add(this.AnimationLabel);
			base.Controls.Add(this._cancelButton);
			base.Controls.Add(this._okButton);
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.Name = "AddFixedIdleAnimationTunerForm";
			base.ShowIcon = false;
			base.ShowInTaskbar = false;
			base.FormClosing += new global::System.Windows.Forms.FormClosingEventHandler(this.AddFixedIdleAnimationTunerForm_FormClosing);
			this.MainSplitContainer.Panel1.ResumeLayout(false);
			this.MainSplitContainer.Panel1.PerformLayout();
			this.MainSplitContainer.Panel2.ResumeLayout(false);
			this.MainSplitContainer.Panel2.PerformLayout();
			this.MainSplitContainer.ResumeLayout(false);
			base.ResumeLayout(false);
			base.PerformLayout();
		}

		// Token: 0x04000506 RID: 1286
		private global::System.ComponentModel.IContainer components;

		// Token: 0x04000507 RID: 1287
		private global::System.Windows.Forms.Button _okButton;

		// Token: 0x04000508 RID: 1288
		private global::System.Windows.Forms.Button _cancelButton;

		// Token: 0x04000509 RID: 1289
		private global::System.Windows.Forms.Label AnimationLabel;

		// Token: 0x0400050A RID: 1290
		private global::System.Windows.Forms.ComboBox AnimationComboBox;

		// Token: 0x0400050B RID: 1291
		private global::System.Windows.Forms.Label ImpactLabel;

		// Token: 0x0400050C RID: 1292
		private global::System.Windows.Forms.SplitContainer MainSplitContainer;

		// Token: 0x0400050D RID: 1293
		private global::System.Windows.Forms.ListView ImpactListView;

		// Token: 0x0400050E RID: 1294
		private global::System.Windows.Forms.ListView VisualStateListView;

		// Token: 0x0400050F RID: 1295
		private global::System.Windows.Forms.Label VisualStateLabel;

		// Token: 0x04000510 RID: 1296
		private global::System.Windows.Forms.ColumnHeader ColumnHeader00;

		// Token: 0x04000511 RID: 1297
		private global::System.Windows.Forms.ColumnHeader ColumnHeader01;

		// Token: 0x04000512 RID: 1298
		private global::System.Windows.Forms.ColumnHeader ColumnHeader02;

		// Token: 0x04000513 RID: 1299
		private global::System.Windows.Forms.ColumnHeader ColumnHeader10;

		// Token: 0x04000514 RID: 1300
		private global::System.Windows.Forms.ColumnHeader ColumnHeader11;

		// Token: 0x04000515 RID: 1301
		private global::System.Windows.Forms.ColumnHeader ColumnHeader12;

		// Token: 0x04000516 RID: 1302
		private global::System.Windows.Forms.TextBox SpawnTunerNameTextBox;

		// Token: 0x04000517 RID: 1303
		private global::System.Windows.Forms.Label SpawnTunerLabel;

		// Token: 0x04000518 RID: 1304
		private global::System.Windows.Forms.TextBox VisualStateNameTextBox;

		// Token: 0x04000519 RID: 1305
		private global::System.Windows.Forms.Label VisualStateNameLabel;

		// Token: 0x0400051A RID: 1306
		private global::System.Windows.Forms.TextBox VisMobTextBox;

		// Token: 0x0400051B RID: 1307
		private global::System.Windows.Forms.Label VisMobLabel;

		// Token: 0x0400051C RID: 1308
		private global::System.Windows.Forms.CheckBox ForceCreateImpactCheckBox;

		// Token: 0x0400051D RID: 1309
		private global::System.Windows.Forms.CheckBox ForceCreateVisualStateCheckBox;

		// Token: 0x0400051E RID: 1310
		private global::System.Windows.Forms.ColumnHeader ColumnHeader13;

		// Token: 0x0400051F RID: 1311
		private global::System.Windows.Forms.ColumnHeader ColumnHeader14;
	}
}
