namespace MapEditor.Forms.Water
{
	// Token: 0x0200006A RID: 106
	public partial class WaterEditorForm : global::MapEditor.Forms.Base.BaseForm
	{
		// Token: 0x06000524 RID: 1316 RVA: 0x0002952F File Offset: 0x0002852F
		protected override void Dispose(bool disposing)
		{
			if (disposing && this.components != null)
			{
				this.components.Dispose();
			}
			base.Dispose(disposing);
		}

		// Token: 0x06000525 RID: 1317 RVA: 0x00029550 File Offset: 0x00028550
		private void InitializeComponent()
		{
			global::System.ComponentModel.ComponentResourceManager resources = new global::System.ComponentModel.ComponentResourceManager(typeof(global::MapEditor.Forms.Water.WaterEditorForm));
			this.label1 = new global::System.Windows.Forms.Label();
			this.nameComboBox = new global::System.Windows.Forms.ComboBox();
			this.label2 = new global::System.Windows.Forms.Label();
			this.continentComboBox = new global::System.Windows.Forms.ComboBox();
			this.propertyControl = new global::System.Windows.Forms.PropertyControl();
			this.newWaterButton = new global::System.Windows.Forms.Button();
			this.deleteWaterButton = new global::System.Windows.Forms.Button();
			this.saveDbButton = new global::System.Windows.Forms.Button();
			this.exportBumpTextButton = new global::System.Windows.Forms.Button();
			this.exportFresnelUpButton = new global::System.Windows.Forms.Button();
			this.exportFresnelDownButton = new global::System.Windows.Forms.Button();
			this.exportWaveButton = new global::System.Windows.Forms.Button();
			this.exportWaveBumpButton = new global::System.Windows.Forms.Button();
			this.exportButtomsGroupBox = new global::System.Windows.Forms.GroupBox();
			this.exportButtomsGroupBox.SuspendLayout();
			base.SuspendLayout();
			resources.ApplyResources(this.label1, "label1");
			this.label1.Name = "label1";
			resources.ApplyResources(this.nameComboBox, "nameComboBox");
			this.nameComboBox.DropDownStyle = global::System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.nameComboBox.FormattingEnabled = true;
			this.nameComboBox.Name = "nameComboBox";
			this.nameComboBox.Sorted = true;
			resources.ApplyResources(this.label2, "label2");
			this.label2.Name = "label2";
			resources.ApplyResources(this.continentComboBox, "continentComboBox");
			this.continentComboBox.DropDownStyle = global::System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.continentComboBox.FormattingEnabled = true;
			this.continentComboBox.Name = "continentComboBox";
			this.continentComboBox.Sorted = true;
			this.propertyControl.AllowDrop = true;
			resources.ApplyResources(this.propertyControl, "propertyControl");
			this.propertyControl.DefaultLocationFolder = "";
			this.propertyControl.Name = "propertyControl";
			this.propertyControl.SelectedObject = null;
			this.propertyControl.SelectedObjects = new object[0];
			this.propertyControl.SkipRefresh = false;
			this.propertyControl.TitleControl = null;
			this.propertyControl.TitleRelativeFrom = null;
			this.propertyControl.TitleStart = "";
			resources.ApplyResources(this.newWaterButton, "newWaterButton");
			this.newWaterButton.Name = "newWaterButton";
			this.newWaterButton.UseVisualStyleBackColor = true;
			resources.ApplyResources(this.deleteWaterButton, "deleteWaterButton");
			this.deleteWaterButton.Name = "deleteWaterButton";
			this.deleteWaterButton.UseVisualStyleBackColor = true;
			resources.ApplyResources(this.saveDbButton, "saveDbButton");
			this.saveDbButton.Name = "saveDbButton";
			this.saveDbButton.UseVisualStyleBackColor = true;
			resources.ApplyResources(this.exportBumpTextButton, "exportBumpTextButton");
			this.exportBumpTextButton.Name = "exportBumpTextButton";
			this.exportBumpTextButton.Tag = "bumpTexture";
			this.exportBumpTextButton.UseVisualStyleBackColor = true;
			resources.ApplyResources(this.exportFresnelUpButton, "exportFresnelUpButton");
			this.exportFresnelUpButton.Name = "exportFresnelUpButton";
			this.exportFresnelUpButton.Tag = "fresnelUp";
			this.exportFresnelUpButton.UseVisualStyleBackColor = true;
			resources.ApplyResources(this.exportFresnelDownButton, "exportFresnelDownButton");
			this.exportFresnelDownButton.Name = "exportFresnelDownButton";
			this.exportFresnelDownButton.Tag = "fresnelDown";
			this.exportFresnelDownButton.UseVisualStyleBackColor = true;
			resources.ApplyResources(this.exportWaveButton, "exportWaveButton");
			this.exportWaveButton.Name = "exportWaveButton";
			this.exportWaveButton.Tag = "wave";
			this.exportWaveButton.UseVisualStyleBackColor = true;
			resources.ApplyResources(this.exportWaveBumpButton, "exportWaveBumpButton");
			this.exportWaveBumpButton.Name = "exportWaveBumpButton";
			this.exportWaveBumpButton.Tag = "waveBump";
			this.exportWaveBumpButton.UseVisualStyleBackColor = true;
			resources.ApplyResources(this.exportButtomsGroupBox, "exportButtomsGroupBox");
			this.exportButtomsGroupBox.Controls.Add(this.exportFresnelDownButton);
			this.exportButtomsGroupBox.Controls.Add(this.exportWaveBumpButton);
			this.exportButtomsGroupBox.Controls.Add(this.exportBumpTextButton);
			this.exportButtomsGroupBox.Controls.Add(this.exportWaveButton);
			this.exportButtomsGroupBox.Controls.Add(this.exportFresnelUpButton);
			this.exportButtomsGroupBox.Name = "exportButtomsGroupBox";
			this.exportButtomsGroupBox.TabStop = false;
			resources.ApplyResources(this, "$this");
			base.AutoScaleMode = global::System.Windows.Forms.AutoScaleMode.Font;
			base.Controls.Add(this.exportButtomsGroupBox);
			base.Controls.Add(this.saveDbButton);
			base.Controls.Add(this.deleteWaterButton);
			base.Controls.Add(this.newWaterButton);
			base.Controls.Add(this.propertyControl);
			base.Controls.Add(this.continentComboBox);
			base.Controls.Add(this.label2);
			base.Controls.Add(this.nameComboBox);
			base.Controls.Add(this.label1);
			base.Name = "WaterEditorForm";
			this.exportButtomsGroupBox.ResumeLayout(false);
			base.ResumeLayout(false);
			base.PerformLayout();
		}

		// Token: 0x040003B6 RID: 950
		private global::System.ComponentModel.IContainer components;

		// Token: 0x040003B7 RID: 951
		private global::System.Windows.Forms.Label label1;

		// Token: 0x040003B8 RID: 952
		private global::System.Windows.Forms.ComboBox nameComboBox;

		// Token: 0x040003B9 RID: 953
		private global::System.Windows.Forms.Label label2;

		// Token: 0x040003BA RID: 954
		private global::System.Windows.Forms.ComboBox continentComboBox;

		// Token: 0x040003BB RID: 955
		private global::System.Windows.Forms.PropertyControl propertyControl;

		// Token: 0x040003BC RID: 956
		private global::System.Windows.Forms.Button newWaterButton;

		// Token: 0x040003BD RID: 957
		private global::System.Windows.Forms.Button deleteWaterButton;

		// Token: 0x040003BE RID: 958
		private global::System.Windows.Forms.Button saveDbButton;

		// Token: 0x040003BF RID: 959
		private global::System.Windows.Forms.Button exportBumpTextButton;

		// Token: 0x040003C0 RID: 960
		private global::System.Windows.Forms.Button exportFresnelUpButton;

		// Token: 0x040003C1 RID: 961
		private global::System.Windows.Forms.Button exportFresnelDownButton;

		// Token: 0x040003C2 RID: 962
		private global::System.Windows.Forms.Button exportWaveButton;

		// Token: 0x040003C3 RID: 963
		private global::System.Windows.Forms.Button exportWaveBumpButton;

		// Token: 0x040003C4 RID: 964
		private global::System.Windows.Forms.GroupBox exportButtomsGroupBox;
	}
}
