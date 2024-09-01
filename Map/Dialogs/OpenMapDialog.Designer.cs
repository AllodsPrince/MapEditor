namespace MapEditor.Map.Dialogs
{
	// Token: 0x02000166 RID: 358
	public partial class OpenMapDialog : global::System.Windows.Forms.Form
	{
		// Token: 0x06001190 RID: 4496 RVA: 0x0008298C File Offset: 0x0008198C
		protected override void Dispose(bool disposing)
		{
			if (disposing && this.components != null)
			{
				this.components.Dispose();
			}
			base.Dispose(disposing);
		}

		// Token: 0x06001191 RID: 4497 RVA: 0x000829AC File Offset: 0x000819AC
		private void InitializeComponent()
		{
			this.components = new global::System.ComponentModel.Container();
			global::System.ComponentModel.ComponentResourceManager resources = new global::System.ComponentModel.ComponentResourceManager(typeof(global::MapEditor.Map.Dialogs.OpenMapDialog));
			this.cmbBoxName = new global::System.Windows.Forms.ComboBox();
			this.labelName = new global::System.Windows.Forms.Label();
			this.labelY = new global::System.Windows.Forms.Label();
			this.labelX = new global::System.Windows.Forms.Label();
			this.textBoxY = new global::System.Windows.Forms.TextBox();
			this.textBoxX = new global::System.Windows.Forms.TextBox();
			this.Cancelbutton = new global::System.Windows.Forms.Button();
			this.OKbutton = new global::System.Windows.Forms.Button();
			this.panel1 = new global::System.Windows.Forms.Panel();
			this.CoordRadioButton = new global::System.Windows.Forms.RadioButton();
			this.GlCoordRadioButton = new global::System.Windows.Forms.RadioButton();
			this.PatchRadioButton = new global::System.Windows.Forms.RadioButton();
			this.AlternativeCoordLabel = new global::System.Windows.Forms.Label();
			this.createBottomCheckbox = new global::System.Windows.Forms.CheckBox();
			this.createTerrainCheckbox = new global::System.Windows.Forms.CheckBox();
			this.OpenMapDialogTimer = new global::System.Windows.Forms.Timer(this.components);
			this.mapDialogControl = new global::MapEditor.Map.Dialogs.MapDialogControl();
			this.cmbBoxFilters = new global::System.Windows.Forms.ComboBox();
			this.label1 = new global::System.Windows.Forms.Label();
			this.filterEditorButton = new global::System.Windows.Forms.Button();
			this.label2 = new global::System.Windows.Forms.Label();
			this.panel2 = new global::System.Windows.Forms.Panel();
			this.size4RadioButton = new global::System.Windows.Forms.RadioButton();
			this.size1RadioButton = new global::System.Windows.Forms.RadioButton();
			this.panel1.SuspendLayout();
			this.panel2.SuspendLayout();
			base.SuspendLayout();
			resources.ApplyResources(this.cmbBoxName, "cmbBoxName");
			this.cmbBoxName.DropDownStyle = global::System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cmbBoxName.FormattingEnabled = true;
			this.cmbBoxName.Name = "cmbBoxName";
			this.cmbBoxName.Sorted = true;
			resources.ApplyResources(this.labelName, "labelName");
			this.labelName.Name = "labelName";
			resources.ApplyResources(this.labelY, "labelY");
			this.labelY.Name = "labelY";
			resources.ApplyResources(this.labelX, "labelX");
			this.labelX.Name = "labelX";
			resources.ApplyResources(this.textBoxY, "textBoxY");
			this.textBoxY.Name = "textBoxY";
			resources.ApplyResources(this.textBoxX, "textBoxX");
			this.textBoxX.Name = "textBoxX";
			resources.ApplyResources(this.Cancelbutton, "Cancelbutton");
			this.Cancelbutton.DialogResult = global::System.Windows.Forms.DialogResult.Cancel;
			this.Cancelbutton.Name = "Cancelbutton";
			this.Cancelbutton.UseVisualStyleBackColor = true;
			resources.ApplyResources(this.OKbutton, "OKbutton");
			this.OKbutton.DialogResult = global::System.Windows.Forms.DialogResult.OK;
			this.OKbutton.Name = "OKbutton";
			this.OKbutton.UseVisualStyleBackColor = true;
			this.panel1.Controls.Add(this.CoordRadioButton);
			this.panel1.Controls.Add(this.GlCoordRadioButton);
			this.panel1.Controls.Add(this.PatchRadioButton);
			resources.ApplyResources(this.panel1, "panel1");
			this.panel1.Name = "panel1";
			resources.ApplyResources(this.CoordRadioButton, "CoordRadioButton");
			this.CoordRadioButton.Name = "CoordRadioButton";
			this.CoordRadioButton.UseVisualStyleBackColor = true;
			resources.ApplyResources(this.GlCoordRadioButton, "GlCoordRadioButton");
			this.GlCoordRadioButton.Name = "GlCoordRadioButton";
			this.GlCoordRadioButton.UseVisualStyleBackColor = true;
			resources.ApplyResources(this.PatchRadioButton, "PatchRadioButton");
			this.PatchRadioButton.Checked = true;
			this.PatchRadioButton.Name = "PatchRadioButton";
			this.PatchRadioButton.TabStop = true;
			this.PatchRadioButton.UseVisualStyleBackColor = true;
			resources.ApplyResources(this.AlternativeCoordLabel, "AlternativeCoordLabel");
			this.AlternativeCoordLabel.Name = "AlternativeCoordLabel";
			resources.ApplyResources(this.createBottomCheckbox, "createBottomCheckbox");
			this.createBottomCheckbox.Name = "createBottomCheckbox";
			this.createBottomCheckbox.UseVisualStyleBackColor = true;
			this.createBottomCheckbox.CheckedChanged += new global::System.EventHandler(this.createBottomCheckbox_CheckedChanged);
			resources.ApplyResources(this.createTerrainCheckbox, "createTerrainCheckbox");
			this.createTerrainCheckbox.Name = "createTerrainCheckbox";
			this.createTerrainCheckbox.UseVisualStyleBackColor = true;
			this.createTerrainCheckbox.CheckedChanged += new global::System.EventHandler(this.createTerrainCheckbox_CheckedChanged);
			this.OpenMapDialogTimer.Interval = 300;
			resources.ApplyResources(this.mapDialogControl, "mapDialogControl");
			this.mapDialogControl.BackColor = global::System.Drawing.SystemColors.Control;
			this.mapDialogControl.EnableDraw = false;
			this.mapDialogControl.Name = "mapDialogControl";
			resources.ApplyResources(this.cmbBoxFilters, "cmbBoxFilters");
			this.cmbBoxFilters.DropDownStyle = global::System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cmbBoxFilters.FormattingEnabled = true;
			this.cmbBoxFilters.Name = "cmbBoxFilters";
			resources.ApplyResources(this.label1, "label1");
			this.label1.Name = "label1";
			resources.ApplyResources(this.filterEditorButton, "filterEditorButton");
			this.filterEditorButton.Name = "filterEditorButton";
			this.filterEditorButton.UseVisualStyleBackColor = true;
			resources.ApplyResources(this.label2, "label2");
			this.label2.Name = "label2";
			this.panel2.Controls.Add(this.size1RadioButton);
			this.panel2.Controls.Add(this.size4RadioButton);
			resources.ApplyResources(this.panel2, "panel2");
			this.panel2.Name = "panel2";
			resources.ApplyResources(this.size4RadioButton, "size4RadioButton");
			this.size4RadioButton.Checked = true;
			this.size4RadioButton.Name = "size4RadioButton";
			this.size4RadioButton.UseVisualStyleBackColor = true;
			resources.ApplyResources(this.size1RadioButton, "size1RadioButton");
			this.size1RadioButton.Name = "size1RadioButton";
			this.size1RadioButton.UseVisualStyleBackColor = true;
			base.AcceptButton = this.OKbutton;
			resources.ApplyResources(this, "$this");
			base.AutoScaleMode = global::System.Windows.Forms.AutoScaleMode.Font;
			base.CancelButton = this.Cancelbutton;
			base.Controls.Add(this.panel2);
			base.Controls.Add(this.label2);
			base.Controls.Add(this.filterEditorButton);
			base.Controls.Add(this.label1);
			base.Controls.Add(this.cmbBoxFilters);
			base.Controls.Add(this.createBottomCheckbox);
			base.Controls.Add(this.createTerrainCheckbox);
			base.Controls.Add(this.AlternativeCoordLabel);
			base.Controls.Add(this.panel1);
			base.Controls.Add(this.cmbBoxName);
			base.Controls.Add(this.labelName);
			base.Controls.Add(this.labelY);
			base.Controls.Add(this.labelX);
			base.Controls.Add(this.textBoxY);
			base.Controls.Add(this.textBoxX);
			base.Controls.Add(this.Cancelbutton);
			base.Controls.Add(this.OKbutton);
			base.Controls.Add(this.mapDialogControl);
			base.Name = "OpenMapDialog";
			base.ShowInTaskbar = false;
			base.FormClosing += new global::System.Windows.Forms.FormClosingEventHandler(this.OpenMapDialog_FormClosing);
			this.panel1.ResumeLayout(false);
			this.panel1.PerformLayout();
			this.panel2.ResumeLayout(false);
			this.panel2.PerformLayout();
			base.ResumeLayout(false);
			base.PerformLayout();
		}

		// Token: 0x04000C90 RID: 3216
		private global::System.Windows.Forms.ComboBox cmbBoxName;

		// Token: 0x04000C91 RID: 3217
		private global::System.Windows.Forms.Label labelName;

		// Token: 0x04000C92 RID: 3218
		private global::System.Windows.Forms.Label labelY;

		// Token: 0x04000C93 RID: 3219
		private global::System.Windows.Forms.Label labelX;

		// Token: 0x04000C94 RID: 3220
		private global::System.Windows.Forms.TextBox textBoxY;

		// Token: 0x04000C95 RID: 3221
		private global::System.Windows.Forms.TextBox textBoxX;

		// Token: 0x04000C96 RID: 3222
		private global::System.Windows.Forms.Button Cancelbutton;

		// Token: 0x04000C97 RID: 3223
		private global::System.Windows.Forms.Button OKbutton;

		// Token: 0x04000C98 RID: 3224
		private global::MapEditor.Map.Dialogs.MapDialogControl mapDialogControl;

		// Token: 0x04000C99 RID: 3225
		private global::System.Windows.Forms.Panel panel1;

		// Token: 0x04000C9A RID: 3226
		private global::System.Windows.Forms.RadioButton PatchRadioButton;

		// Token: 0x04000C9B RID: 3227
		private global::System.Windows.Forms.RadioButton GlCoordRadioButton;

		// Token: 0x04000C9C RID: 3228
		private global::System.Windows.Forms.Label AlternativeCoordLabel;

		// Token: 0x04000C9D RID: 3229
		private global::System.Windows.Forms.CheckBox createBottomCheckbox;

		// Token: 0x04000C9E RID: 3230
		private global::System.Windows.Forms.CheckBox createTerrainCheckbox;

		// Token: 0x04000C9F RID: 3231
		private global::System.Windows.Forms.Timer OpenMapDialogTimer;

		// Token: 0x04000CA0 RID: 3232
		private global::System.ComponentModel.IContainer components;

		// Token: 0x04000CA1 RID: 3233
		private global::System.Windows.Forms.RadioButton CoordRadioButton;

		// Token: 0x04000CA2 RID: 3234
		private global::System.Windows.Forms.ComboBox cmbBoxFilters;

		// Token: 0x04000CA3 RID: 3235
		private global::System.Windows.Forms.Label label1;

		// Token: 0x04000CA4 RID: 3236
		private global::System.Windows.Forms.Button filterEditorButton;

		// Token: 0x04000CA5 RID: 3237
		private global::System.Windows.Forms.Label label2;

		// Token: 0x04000CA6 RID: 3238
		private global::System.Windows.Forms.Panel panel2;

		// Token: 0x04000CA7 RID: 3239
		private global::System.Windows.Forms.RadioButton size1RadioButton;

		// Token: 0x04000CA8 RID: 3240
		private global::System.Windows.Forms.RadioButton size4RadioButton;
	}
}
