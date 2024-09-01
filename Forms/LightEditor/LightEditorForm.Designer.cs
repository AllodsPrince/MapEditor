namespace MapEditor.Forms.LightEditor
{
	// Token: 0x02000244 RID: 580
	public partial class LightEditorForm : global::MapEditor.Forms.Base.BaseForm
	{
		// Token: 0x06001B91 RID: 7057 RVA: 0x000B2613 File Offset: 0x000B1613
		protected override void Dispose(bool disposing)
		{
			if (disposing && this.components != null)
			{
				this.components.Dispose();
			}
			base.Dispose(disposing);
		}

		// Token: 0x06001B92 RID: 7058 RVA: 0x000B2634 File Offset: 0x000B1634
		private void InitializeComponent()
		{
			global::System.ComponentModel.ComponentResourceManager resources = new global::System.ComponentModel.ComponentResourceManager(typeof(global::MapEditor.Forms.LightEditor.LightEditorForm));
			this.ContinentComboBox = new global::System.Windows.Forms.ComboBox();
			this.label1 = new global::System.Windows.Forms.Label();
			this.label2 = new global::System.Windows.Forms.Label();
			this.panel1 = new global::System.Windows.Forms.Panel();
			this.CloseButton = new global::System.Windows.Forms.Button();
			this.NewButton = new global::System.Windows.Forms.Button();
			this.DeleteButton = new global::System.Windows.Forms.Button();
			this.RemoveInstantLightButton = new global::System.Windows.Forms.Button();
			this.AddInstantLightButton = new global::System.Windows.Forms.Button();
			this.ClearSkyMeshButton = new global::System.Windows.Forms.Button();
			this.SaveButton = new global::System.Windows.Forms.Button();
			this.label14 = new global::System.Windows.Forms.Label();
			this.SunLightPitchTextBox = new global::System.Windows.Forms.TextBox();
			this.label15 = new global::System.Windows.Forms.Label();
			this.SunLightYawTextBox = new global::System.Windows.Forms.TextBox();
			this.panel2 = new global::System.Windows.Forms.Panel();
			this.ContourColorAHSLLabel = new global::System.Windows.Forms.Label();
			this.WaterGradientEndAHSLLabel = new global::System.Windows.Forms.Label();
			this.WaterGradientStartAHSLLabel = new global::System.Windows.Forms.Label();
			this.SpecularWaterColorAHSLLabel = new global::System.Windows.Forms.Label();
			this.SpecularColorAHSLLabel = new global::System.Windows.Forms.Label();
			this.FogColorAHSLLabel = new global::System.Windows.Forms.Label();
			this.DiffuseColorAHSLLabel = new global::System.Windows.Forms.Label();
			this.AmbilentColorAHSLLabel = new global::System.Windows.Forms.Label();
			this.LumColorTextBox = new global::System.Windows.Forms.TextBox();
			this.SatColorTextBox = new global::System.Windows.Forms.TextBox();
			this.HueColorTextBox = new global::System.Windows.Forms.TextBox();
			this.AlphaColorTextBox = new global::System.Windows.Forms.TextBox();
			this.label9 = new global::System.Windows.Forms.Label();
			this.label13 = new global::System.Windows.Forms.Label();
			this.label12 = new global::System.Windows.Forms.Label();
			this.label11 = new global::System.Windows.Forms.Label();
			this.label10 = new global::System.Windows.Forms.Label();
			this.LumColorTrackBar = new global::System.Windows.Forms.TrackBar();
			this.SatColorTrackBar = new global::System.Windows.Forms.TrackBar();
			this.HueColorTrackBar = new global::System.Windows.Forms.TrackBar();
			this.AlphaColorTrackBar = new global::System.Windows.Forms.TrackBar();
			this.ContourColorPanel = new global::System.Windows.Forms.Panel();
			this.ContourColorRadioButton = new global::System.Windows.Forms.RadioButton();
			this.WaterGradientEndPanel = new global::System.Windows.Forms.Panel();
			this.WaterGradientEndRadioButton = new global::System.Windows.Forms.RadioButton();
			this.WaterGradientStartPanel = new global::System.Windows.Forms.Panel();
			this.WaterGradientStartRadioButton = new global::System.Windows.Forms.RadioButton();
			this.SpecularWaterColorPanel = new global::System.Windows.Forms.Panel();
			this.SpecularWaterColorRadioButton = new global::System.Windows.Forms.RadioButton();
			this.SpecularColorPanel = new global::System.Windows.Forms.Panel();
			this.SpecularColorRadioButton = new global::System.Windows.Forms.RadioButton();
			this.FogColorPanel = new global::System.Windows.Forms.Panel();
			this.FogColorRadioButton = new global::System.Windows.Forms.RadioButton();
			this.DiffuseColorPanel = new global::System.Windows.Forms.Panel();
			this.DiffuseColorRadioButton = new global::System.Windows.Forms.RadioButton();
			this.AmbientColorPanel = new global::System.Windows.Forms.Panel();
			this.AmbientColorRadioButton = new global::System.Windows.Forms.RadioButton();
			this.FogEndLabel = new global::System.Windows.Forms.Label();
			this.FogEndTextBox = new global::System.Windows.Forms.TextBox();
			this.FogStartLabel = new global::System.Windows.Forms.Label();
			this.FogStartTextBox = new global::System.Windows.Forms.TextBox();
			this.label6 = new global::System.Windows.Forms.Label();
			this.AmbientFactorTextBox = new global::System.Windows.Forms.TextBox();
			this.ChooseSkyMeshButton = new global::System.Windows.Forms.Button();
			this.label5 = new global::System.Windows.Forms.Label();
			this.SkyMeshTextBox = new global::System.Windows.Forms.TextBox();
			this.label4 = new global::System.Windows.Forms.Label();
			this.TimeTextBox = new global::System.Windows.Forms.TextBox();
			this.label3 = new global::System.Windows.Forms.Label();
			this.InstantLightsListBox = new global::System.Windows.Forms.ListBox();
			this.LightComboBox = new global::System.Windows.Forms.ComboBox();
			this.panel1.SuspendLayout();
			this.panel2.SuspendLayout();
			((global::System.ComponentModel.ISupportInitialize)this.LumColorTrackBar).BeginInit();
			((global::System.ComponentModel.ISupportInitialize)this.SatColorTrackBar).BeginInit();
			((global::System.ComponentModel.ISupportInitialize)this.HueColorTrackBar).BeginInit();
			((global::System.ComponentModel.ISupportInitialize)this.AlphaColorTrackBar).BeginInit();
			base.SuspendLayout();
			this.ContinentComboBox.FormattingEnabled = true;
			resources.ApplyResources(this.ContinentComboBox, "ContinentComboBox");
			this.ContinentComboBox.Name = "ContinentComboBox";
			resources.ApplyResources(this.label1, "label1");
			this.label1.Name = "label1";
			resources.ApplyResources(this.label2, "label2");
			this.label2.Name = "label2";
			resources.ApplyResources(this.panel1, "panel1");
			this.panel1.BorderStyle = global::System.Windows.Forms.BorderStyle.FixedSingle;
			this.panel1.Controls.Add(this.CloseButton);
			this.panel1.Controls.Add(this.NewButton);
			this.panel1.Controls.Add(this.DeleteButton);
			this.panel1.Controls.Add(this.RemoveInstantLightButton);
			this.panel1.Controls.Add(this.AddInstantLightButton);
			this.panel1.Controls.Add(this.ClearSkyMeshButton);
			this.panel1.Controls.Add(this.SaveButton);
			this.panel1.Controls.Add(this.label14);
			this.panel1.Controls.Add(this.SunLightPitchTextBox);
			this.panel1.Controls.Add(this.label15);
			this.panel1.Controls.Add(this.SunLightYawTextBox);
			this.panel1.Controls.Add(this.panel2);
			this.panel1.Controls.Add(this.FogEndLabel);
			this.panel1.Controls.Add(this.FogEndTextBox);
			this.panel1.Controls.Add(this.FogStartLabel);
			this.panel1.Controls.Add(this.FogStartTextBox);
			this.panel1.Controls.Add(this.label6);
			this.panel1.Controls.Add(this.AmbientFactorTextBox);
			this.panel1.Controls.Add(this.ChooseSkyMeshButton);
			this.panel1.Controls.Add(this.label5);
			this.panel1.Controls.Add(this.SkyMeshTextBox);
			this.panel1.Controls.Add(this.label4);
			this.panel1.Controls.Add(this.TimeTextBox);
			this.panel1.Controls.Add(this.label3);
			this.panel1.Controls.Add(this.InstantLightsListBox);
			this.panel1.Name = "panel1";
			resources.ApplyResources(this.CloseButton, "CloseButton");
			this.CloseButton.DialogResult = global::System.Windows.Forms.DialogResult.Cancel;
			this.CloseButton.Name = "CloseButton";
			this.CloseButton.UseVisualStyleBackColor = true;
			resources.ApplyResources(this.NewButton, "NewButton");
			this.NewButton.Name = "NewButton";
			this.NewButton.UseVisualStyleBackColor = true;
			resources.ApplyResources(this.DeleteButton, "DeleteButton");
			this.DeleteButton.Name = "DeleteButton";
			this.DeleteButton.UseVisualStyleBackColor = true;
			resources.ApplyResources(this.RemoveInstantLightButton, "RemoveInstantLightButton");
			this.RemoveInstantLightButton.Name = "RemoveInstantLightButton";
			this.RemoveInstantLightButton.UseVisualStyleBackColor = true;
			resources.ApplyResources(this.AddInstantLightButton, "AddInstantLightButton");
			this.AddInstantLightButton.Name = "AddInstantLightButton";
			this.AddInstantLightButton.UseVisualStyleBackColor = true;
			resources.ApplyResources(this.ClearSkyMeshButton, "ClearSkyMeshButton");
			this.ClearSkyMeshButton.Name = "ClearSkyMeshButton";
			this.ClearSkyMeshButton.UseVisualStyleBackColor = true;
			resources.ApplyResources(this.SaveButton, "SaveButton");
			this.SaveButton.Name = "SaveButton";
			this.SaveButton.UseVisualStyleBackColor = true;
			resources.ApplyResources(this.label14, "label14");
			this.label14.Name = "label14";
			resources.ApplyResources(this.SunLightPitchTextBox, "SunLightPitchTextBox");
			this.SunLightPitchTextBox.Name = "SunLightPitchTextBox";
			resources.ApplyResources(this.label15, "label15");
			this.label15.Name = "label15";
			resources.ApplyResources(this.SunLightYawTextBox, "SunLightYawTextBox");
			this.SunLightYawTextBox.Name = "SunLightYawTextBox";
			resources.ApplyResources(this.panel2, "panel2");
			this.panel2.BorderStyle = global::System.Windows.Forms.BorderStyle.FixedSingle;
			this.panel2.Controls.Add(this.ContourColorAHSLLabel);
			this.panel2.Controls.Add(this.WaterGradientEndAHSLLabel);
			this.panel2.Controls.Add(this.WaterGradientStartAHSLLabel);
			this.panel2.Controls.Add(this.SpecularWaterColorAHSLLabel);
			this.panel2.Controls.Add(this.SpecularColorAHSLLabel);
			this.panel2.Controls.Add(this.FogColorAHSLLabel);
			this.panel2.Controls.Add(this.DiffuseColorAHSLLabel);
			this.panel2.Controls.Add(this.AmbilentColorAHSLLabel);
			this.panel2.Controls.Add(this.LumColorTextBox);
			this.panel2.Controls.Add(this.SatColorTextBox);
			this.panel2.Controls.Add(this.HueColorTextBox);
			this.panel2.Controls.Add(this.AlphaColorTextBox);
			this.panel2.Controls.Add(this.label9);
			this.panel2.Controls.Add(this.label13);
			this.panel2.Controls.Add(this.label12);
			this.panel2.Controls.Add(this.label11);
			this.panel2.Controls.Add(this.label10);
			this.panel2.Controls.Add(this.LumColorTrackBar);
			this.panel2.Controls.Add(this.SatColorTrackBar);
			this.panel2.Controls.Add(this.HueColorTrackBar);
			this.panel2.Controls.Add(this.AlphaColorTrackBar);
			this.panel2.Controls.Add(this.ContourColorPanel);
			this.panel2.Controls.Add(this.ContourColorRadioButton);
			this.panel2.Controls.Add(this.WaterGradientEndPanel);
			this.panel2.Controls.Add(this.WaterGradientEndRadioButton);
			this.panel2.Controls.Add(this.WaterGradientStartPanel);
			this.panel2.Controls.Add(this.WaterGradientStartRadioButton);
			this.panel2.Controls.Add(this.SpecularWaterColorPanel);
			this.panel2.Controls.Add(this.SpecularWaterColorRadioButton);
			this.panel2.Controls.Add(this.SpecularColorPanel);
			this.panel2.Controls.Add(this.SpecularColorRadioButton);
			this.panel2.Controls.Add(this.FogColorPanel);
			this.panel2.Controls.Add(this.FogColorRadioButton);
			this.panel2.Controls.Add(this.DiffuseColorPanel);
			this.panel2.Controls.Add(this.DiffuseColorRadioButton);
			this.panel2.Controls.Add(this.AmbientColorPanel);
			this.panel2.Controls.Add(this.AmbientColorRadioButton);
			this.panel2.Name = "panel2";
			resources.ApplyResources(this.ContourColorAHSLLabel, "ContourColorAHSLLabel");
			this.ContourColorAHSLLabel.Name = "ContourColorAHSLLabel";
			resources.ApplyResources(this.WaterGradientEndAHSLLabel, "WaterGradientEndAHSLLabel");
			this.WaterGradientEndAHSLLabel.Name = "WaterGradientEndAHSLLabel";
			resources.ApplyResources(this.WaterGradientStartAHSLLabel, "WaterGradientStartAHSLLabel");
			this.WaterGradientStartAHSLLabel.Name = "WaterGradientStartAHSLLabel";
			resources.ApplyResources(this.SpecularWaterColorAHSLLabel, "SpecularWaterColorAHSLLabel");
			this.SpecularWaterColorAHSLLabel.Name = "SpecularWaterColorAHSLLabel";
			resources.ApplyResources(this.SpecularColorAHSLLabel, "SpecularColorAHSLLabel");
			this.SpecularColorAHSLLabel.Name = "SpecularColorAHSLLabel";
			resources.ApplyResources(this.FogColorAHSLLabel, "FogColorAHSLLabel");
			this.FogColorAHSLLabel.Name = "FogColorAHSLLabel";
			resources.ApplyResources(this.DiffuseColorAHSLLabel, "DiffuseColorAHSLLabel");
			this.DiffuseColorAHSLLabel.Name = "DiffuseColorAHSLLabel";
			resources.ApplyResources(this.AmbilentColorAHSLLabel, "AmbilentColorAHSLLabel");
			this.AmbilentColorAHSLLabel.Name = "AmbilentColorAHSLLabel";
			resources.ApplyResources(this.LumColorTextBox, "LumColorTextBox");
			this.LumColorTextBox.Name = "LumColorTextBox";
			resources.ApplyResources(this.SatColorTextBox, "SatColorTextBox");
			this.SatColorTextBox.Name = "SatColorTextBox";
			resources.ApplyResources(this.HueColorTextBox, "HueColorTextBox");
			this.HueColorTextBox.Name = "HueColorTextBox";
			resources.ApplyResources(this.AlphaColorTextBox, "AlphaColorTextBox");
			this.AlphaColorTextBox.Name = "AlphaColorTextBox";
			resources.ApplyResources(this.label9, "label9");
			this.label9.Name = "label9";
			resources.ApplyResources(this.label13, "label13");
			this.label13.Name = "label13";
			resources.ApplyResources(this.label12, "label12");
			this.label12.Name = "label12";
			resources.ApplyResources(this.label11, "label11");
			this.label11.Name = "label11";
			resources.ApplyResources(this.label10, "label10");
			this.label10.Name = "label10";
			resources.ApplyResources(this.LumColorTrackBar, "LumColorTrackBar");
			this.LumColorTrackBar.Maximum = 30;
			this.LumColorTrackBar.Name = "LumColorTrackBar";
			resources.ApplyResources(this.SatColorTrackBar, "SatColorTrackBar");
			this.SatColorTrackBar.Maximum = 30;
			this.SatColorTrackBar.Name = "SatColorTrackBar";
			resources.ApplyResources(this.HueColorTrackBar, "HueColorTrackBar");
			this.HueColorTrackBar.Maximum = 30;
			this.HueColorTrackBar.Name = "HueColorTrackBar";
			this.HueColorTrackBar.SmallChange = 5;
			resources.ApplyResources(this.AlphaColorTrackBar, "AlphaColorTrackBar");
			this.AlphaColorTrackBar.Maximum = 30;
			this.AlphaColorTrackBar.Name = "AlphaColorTrackBar";
			resources.ApplyResources(this.ContourColorPanel, "ContourColorPanel");
			this.ContourColorPanel.Name = "ContourColorPanel";
			resources.ApplyResources(this.ContourColorRadioButton, "ContourColorRadioButton");
			this.ContourColorRadioButton.Name = "ContourColorRadioButton";
			this.ContourColorRadioButton.TabStop = true;
			this.ContourColorRadioButton.UseVisualStyleBackColor = true;
			resources.ApplyResources(this.WaterGradientEndPanel, "WaterGradientEndPanel");
			this.WaterGradientEndPanel.Name = "WaterGradientEndPanel";
			resources.ApplyResources(this.WaterGradientEndRadioButton, "WaterGradientEndRadioButton");
			this.WaterGradientEndRadioButton.Name = "WaterGradientEndRadioButton";
			this.WaterGradientEndRadioButton.TabStop = true;
			this.WaterGradientEndRadioButton.UseVisualStyleBackColor = true;
			resources.ApplyResources(this.WaterGradientStartPanel, "WaterGradientStartPanel");
			this.WaterGradientStartPanel.Name = "WaterGradientStartPanel";
			resources.ApplyResources(this.WaterGradientStartRadioButton, "WaterGradientStartRadioButton");
			this.WaterGradientStartRadioButton.Name = "WaterGradientStartRadioButton";
			this.WaterGradientStartRadioButton.TabStop = true;
			this.WaterGradientStartRadioButton.UseVisualStyleBackColor = true;
			resources.ApplyResources(this.SpecularWaterColorPanel, "SpecularWaterColorPanel");
			this.SpecularWaterColorPanel.Name = "SpecularWaterColorPanel";
			resources.ApplyResources(this.SpecularWaterColorRadioButton, "SpecularWaterColorRadioButton");
			this.SpecularWaterColorRadioButton.Name = "SpecularWaterColorRadioButton";
			this.SpecularWaterColorRadioButton.TabStop = true;
			this.SpecularWaterColorRadioButton.UseVisualStyleBackColor = true;
			resources.ApplyResources(this.SpecularColorPanel, "SpecularColorPanel");
			this.SpecularColorPanel.Name = "SpecularColorPanel";
			resources.ApplyResources(this.SpecularColorRadioButton, "SpecularColorRadioButton");
			this.SpecularColorRadioButton.Name = "SpecularColorRadioButton";
			this.SpecularColorRadioButton.TabStop = true;
			this.SpecularColorRadioButton.UseVisualStyleBackColor = true;
			resources.ApplyResources(this.FogColorPanel, "FogColorPanel");
			this.FogColorPanel.Name = "FogColorPanel";
			resources.ApplyResources(this.FogColorRadioButton, "FogColorRadioButton");
			this.FogColorRadioButton.Name = "FogColorRadioButton";
			this.FogColorRadioButton.TabStop = true;
			this.FogColorRadioButton.UseVisualStyleBackColor = true;
			resources.ApplyResources(this.DiffuseColorPanel, "DiffuseColorPanel");
			this.DiffuseColorPanel.Name = "DiffuseColorPanel";
			resources.ApplyResources(this.DiffuseColorRadioButton, "DiffuseColorRadioButton");
			this.DiffuseColorRadioButton.Name = "DiffuseColorRadioButton";
			this.DiffuseColorRadioButton.TabStop = true;
			this.DiffuseColorRadioButton.UseVisualStyleBackColor = true;
			resources.ApplyResources(this.AmbientColorPanel, "AmbientColorPanel");
			this.AmbientColorPanel.Name = "AmbientColorPanel";
			resources.ApplyResources(this.AmbientColorRadioButton, "AmbientColorRadioButton");
			this.AmbientColorRadioButton.Name = "AmbientColorRadioButton";
			this.AmbientColorRadioButton.TabStop = true;
			this.AmbientColorRadioButton.UseVisualStyleBackColor = true;
			resources.ApplyResources(this.FogEndLabel, "FogEndLabel");
			this.FogEndLabel.Name = "FogEndLabel";
			resources.ApplyResources(this.FogEndTextBox, "FogEndTextBox");
			this.FogEndTextBox.Name = "FogEndTextBox";
			resources.ApplyResources(this.FogStartLabel, "FogStartLabel");
			this.FogStartLabel.Name = "FogStartLabel";
			resources.ApplyResources(this.FogStartTextBox, "FogStartTextBox");
			this.FogStartTextBox.Name = "FogStartTextBox";
			resources.ApplyResources(this.label6, "label6");
			this.label6.Name = "label6";
			resources.ApplyResources(this.AmbientFactorTextBox, "AmbientFactorTextBox");
			this.AmbientFactorTextBox.Name = "AmbientFactorTextBox";
			resources.ApplyResources(this.ChooseSkyMeshButton, "ChooseSkyMeshButton");
			this.ChooseSkyMeshButton.Name = "ChooseSkyMeshButton";
			this.ChooseSkyMeshButton.UseVisualStyleBackColor = true;
			resources.ApplyResources(this.label5, "label5");
			this.label5.Name = "label5";
			resources.ApplyResources(this.SkyMeshTextBox, "SkyMeshTextBox");
			this.SkyMeshTextBox.Name = "SkyMeshTextBox";
			this.SkyMeshTextBox.ReadOnly = true;
			resources.ApplyResources(this.label4, "label4");
			this.label4.Name = "label4";
			resources.ApplyResources(this.TimeTextBox, "TimeTextBox");
			this.TimeTextBox.Name = "TimeTextBox";
			resources.ApplyResources(this.label3, "label3");
			this.label3.Name = "label3";
			resources.ApplyResources(this.InstantLightsListBox, "InstantLightsListBox");
			this.InstantLightsListBox.FormattingEnabled = true;
			this.InstantLightsListBox.Name = "InstantLightsListBox";
			resources.ApplyResources(this.LightComboBox, "LightComboBox");
			this.LightComboBox.FormattingEnabled = true;
			this.LightComboBox.Name = "LightComboBox";
			resources.ApplyResources(this, "$this");
			base.AutoScaleMode = global::System.Windows.Forms.AutoScaleMode.Font;
			base.CancelButton = this.CloseButton;
			base.Controls.Add(this.LightComboBox);
			base.Controls.Add(this.panel1);
			base.Controls.Add(this.label2);
			base.Controls.Add(this.label1);
			base.Controls.Add(this.ContinentComboBox);
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.Name = "LightEditorForm";
			base.ShowInTaskbar = false;
			this.panel1.ResumeLayout(false);
			this.panel1.PerformLayout();
			this.panel2.ResumeLayout(false);
			this.panel2.PerformLayout();
			((global::System.ComponentModel.ISupportInitialize)this.LumColorTrackBar).EndInit();
			((global::System.ComponentModel.ISupportInitialize)this.SatColorTrackBar).EndInit();
			((global::System.ComponentModel.ISupportInitialize)this.HueColorTrackBar).EndInit();
			((global::System.ComponentModel.ISupportInitialize)this.AlphaColorTrackBar).EndInit();
			base.ResumeLayout(false);
			base.PerformLayout();
		}

		// Token: 0x04001190 RID: 4496
		private global::System.ComponentModel.IContainer components;

		// Token: 0x04001191 RID: 4497
		private global::System.Windows.Forms.ComboBox ContinentComboBox;

		// Token: 0x04001192 RID: 4498
		private global::System.Windows.Forms.Label label1;

		// Token: 0x04001193 RID: 4499
		private global::System.Windows.Forms.Label label2;

		// Token: 0x04001194 RID: 4500
		private global::System.Windows.Forms.Panel panel1;

		// Token: 0x04001195 RID: 4501
		private global::System.Windows.Forms.TextBox SkyMeshTextBox;

		// Token: 0x04001196 RID: 4502
		private global::System.Windows.Forms.Label label4;

		// Token: 0x04001197 RID: 4503
		private global::System.Windows.Forms.TextBox TimeTextBox;

		// Token: 0x04001198 RID: 4504
		private global::System.Windows.Forms.Label label3;

		// Token: 0x04001199 RID: 4505
		private global::System.Windows.Forms.ListBox InstantLightsListBox;

		// Token: 0x0400119A RID: 4506
		private global::System.Windows.Forms.Label label6;

		// Token: 0x0400119B RID: 4507
		private global::System.Windows.Forms.TextBox AmbientFactorTextBox;

		// Token: 0x0400119C RID: 4508
		private global::System.Windows.Forms.Button ChooseSkyMeshButton;

		// Token: 0x0400119D RID: 4509
		private global::System.Windows.Forms.Label label5;

		// Token: 0x0400119E RID: 4510
		private global::System.Windows.Forms.Label FogStartLabel;

		// Token: 0x0400119F RID: 4511
		private global::System.Windows.Forms.TextBox FogStartTextBox;

		// Token: 0x040011A0 RID: 4512
		private global::System.Windows.Forms.Label FogEndLabel;

		// Token: 0x040011A1 RID: 4513
		private global::System.Windows.Forms.TextBox FogEndTextBox;

		// Token: 0x040011A2 RID: 4514
		private global::System.Windows.Forms.Panel panel2;

		// Token: 0x040011A3 RID: 4515
		private global::System.Windows.Forms.Panel DiffuseColorPanel;

		// Token: 0x040011A4 RID: 4516
		private global::System.Windows.Forms.Panel AmbientColorPanel;

		// Token: 0x040011A5 RID: 4517
		private global::System.Windows.Forms.RadioButton AmbientColorRadioButton;

		// Token: 0x040011A6 RID: 4518
		private global::System.Windows.Forms.Panel WaterGradientStartPanel;

		// Token: 0x040011A7 RID: 4519
		private global::System.Windows.Forms.Panel SpecularWaterColorPanel;

		// Token: 0x040011A8 RID: 4520
		private global::System.Windows.Forms.Panel SpecularColorPanel;

		// Token: 0x040011A9 RID: 4521
		private global::System.Windows.Forms.Panel FogColorPanel;

		// Token: 0x040011AA RID: 4522
		private global::System.Windows.Forms.Panel ContourColorPanel;

		// Token: 0x040011AB RID: 4523
		private global::System.Windows.Forms.Panel WaterGradientEndPanel;

		// Token: 0x040011AC RID: 4524
		private global::System.Windows.Forms.RadioButton ContourColorRadioButton;

		// Token: 0x040011AD RID: 4525
		private global::System.Windows.Forms.RadioButton WaterGradientEndRadioButton;

		// Token: 0x040011AE RID: 4526
		private global::System.Windows.Forms.RadioButton WaterGradientStartRadioButton;

		// Token: 0x040011AF RID: 4527
		private global::System.Windows.Forms.RadioButton SpecularWaterColorRadioButton;

		// Token: 0x040011B0 RID: 4528
		private global::System.Windows.Forms.RadioButton SpecularColorRadioButton;

		// Token: 0x040011B1 RID: 4529
		private global::System.Windows.Forms.RadioButton FogColorRadioButton;

		// Token: 0x040011B2 RID: 4530
		private global::System.Windows.Forms.RadioButton DiffuseColorRadioButton;

		// Token: 0x040011B3 RID: 4531
		private global::System.Windows.Forms.ComboBox LightComboBox;

		// Token: 0x040011B4 RID: 4532
		private global::System.Windows.Forms.Label label13;

		// Token: 0x040011B5 RID: 4533
		private global::System.Windows.Forms.Label label12;

		// Token: 0x040011B6 RID: 4534
		private global::System.Windows.Forms.Label label11;

		// Token: 0x040011B7 RID: 4535
		private global::System.Windows.Forms.Label label10;

		// Token: 0x040011B8 RID: 4536
		private global::System.Windows.Forms.TrackBar LumColorTrackBar;

		// Token: 0x040011B9 RID: 4537
		private global::System.Windows.Forms.TrackBar SatColorTrackBar;

		// Token: 0x040011BA RID: 4538
		private global::System.Windows.Forms.TrackBar HueColorTrackBar;

		// Token: 0x040011BB RID: 4539
		private global::System.Windows.Forms.TrackBar AlphaColorTrackBar;

		// Token: 0x040011BC RID: 4540
		private global::System.Windows.Forms.Label label14;

		// Token: 0x040011BD RID: 4541
		private global::System.Windows.Forms.TextBox SunLightPitchTextBox;

		// Token: 0x040011BE RID: 4542
		private global::System.Windows.Forms.Label label15;

		// Token: 0x040011BF RID: 4543
		private global::System.Windows.Forms.TextBox SunLightYawTextBox;

		// Token: 0x040011C0 RID: 4544
		private global::System.Windows.Forms.Label label9;

		// Token: 0x040011C1 RID: 4545
		private global::System.Windows.Forms.TextBox LumColorTextBox;

		// Token: 0x040011C2 RID: 4546
		private global::System.Windows.Forms.TextBox SatColorTextBox;

		// Token: 0x040011C3 RID: 4547
		private global::System.Windows.Forms.TextBox HueColorTextBox;

		// Token: 0x040011C4 RID: 4548
		private global::System.Windows.Forms.TextBox AlphaColorTextBox;

		// Token: 0x040011C5 RID: 4549
		private global::System.Windows.Forms.Button SaveButton;

		// Token: 0x040011C6 RID: 4550
		private global::System.Windows.Forms.Button ClearSkyMeshButton;

		// Token: 0x040011C7 RID: 4551
		private global::System.Windows.Forms.Button DeleteButton;

		// Token: 0x040011C8 RID: 4552
		private global::System.Windows.Forms.Button RemoveInstantLightButton;

		// Token: 0x040011C9 RID: 4553
		private global::System.Windows.Forms.Button AddInstantLightButton;

		// Token: 0x040011CA RID: 4554
		private global::System.Windows.Forms.Button NewButton;

		// Token: 0x040011CB RID: 4555
		private global::System.Windows.Forms.Label ContourColorAHSLLabel;

		// Token: 0x040011CC RID: 4556
		private global::System.Windows.Forms.Label WaterGradientEndAHSLLabel;

		// Token: 0x040011CD RID: 4557
		private global::System.Windows.Forms.Label WaterGradientStartAHSLLabel;

		// Token: 0x040011CE RID: 4558
		private global::System.Windows.Forms.Label SpecularWaterColorAHSLLabel;

		// Token: 0x040011CF RID: 4559
		private global::System.Windows.Forms.Label SpecularColorAHSLLabel;

		// Token: 0x040011D0 RID: 4560
		private global::System.Windows.Forms.Label FogColorAHSLLabel;

		// Token: 0x040011D1 RID: 4561
		private global::System.Windows.Forms.Label DiffuseColorAHSLLabel;

		// Token: 0x040011D2 RID: 4562
		private global::System.Windows.Forms.Label AmbilentColorAHSLLabel;

		// Token: 0x040011D3 RID: 4563
		private global::System.Windows.Forms.Button CloseButton;
	}
}
