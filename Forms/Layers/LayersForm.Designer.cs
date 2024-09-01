namespace MapEditor.Forms.Layers
{
	// Token: 0x0200018C RID: 396
	public partial class LayersForm : global::MapEditor.Forms.Base.BaseForm
	{
		// Token: 0x060012E2 RID: 4834 RVA: 0x00089DCE File Offset: 0x00088DCE
		protected override void Dispose(bool disposing)
		{
			if (disposing && this.components != null)
			{
				this.components.Dispose();
			}
			base.Dispose(disposing);
		}

		// Token: 0x060012E3 RID: 4835 RVA: 0x00089DF0 File Offset: 0x00088DF0
		private void InitializeComponent()
		{
			global::System.ComponentModel.ComponentResourceManager resources = new global::System.ComponentModel.ComponentResourceManager(typeof(global::MapEditor.Forms.Layers.LayersForm));
			this.ContinentComboBox = new global::System.Windows.Forms.ComboBox();
			this.SaveButton = new global::System.Windows.Forms.Button();
			this.ContinentLabel = new global::System.Windows.Forms.Label();
			this.TextuteLable = new global::System.Windows.Forms.Label();
			this.TopWidthLabel = new global::System.Windows.Forms.Label();
			this.panel1 = new global::System.Windows.Forms.Panel();
			this.label22 = new global::System.Windows.Forms.Label();
			this.label21 = new global::System.Windows.Forms.Label();
			this.label20 = new global::System.Windows.Forms.Label();
			this.label19 = new global::System.Windows.Forms.Label();
			this.label18 = new global::System.Windows.Forms.Label();
			this.label17 = new global::System.Windows.Forms.Label();
			this.label16 = new global::System.Windows.Forms.Label();
			this.label15 = new global::System.Windows.Forms.Label();
			this.label14 = new global::System.Windows.Forms.Label();
			this.label13 = new global::System.Windows.Forms.Label();
			this.label12 = new global::System.Windows.Forms.Label();
			this.label11 = new global::System.Windows.Forms.Label();
			this.label10 = new global::System.Windows.Forms.Label();
			this.label9 = new global::System.Windows.Forms.Label();
			this.label8 = new global::System.Windows.Forms.Label();
			this.label7 = new global::System.Windows.Forms.Label();
			this.label6 = new global::System.Windows.Forms.Label();
			this.label5 = new global::System.Windows.Forms.Label();
			this.label4 = new global::System.Windows.Forms.Label();
			this.label3 = new global::System.Windows.Forms.Label();
			this.BottomWidthTrackBar = new global::System.Windows.Forms.TrackBar();
			this.BottomOffsetTrackBar = new global::System.Windows.Forms.TrackBar();
			this.BottomHeightTrackBar = new global::System.Windows.Forms.TrackBar();
			this.TopWidthTrackBar = new global::System.Windows.Forms.TrackBar();
			this.TopOffsetTrackBar = new global::System.Windows.Forms.TrackBar();
			this.TopHeightTrackBar = new global::System.Windows.Forms.TrackBar();
			this.MaxScaleTrackBar = new global::System.Windows.Forms.TrackBar();
			this.MinScaleTrackBar = new global::System.Windows.Forms.TrackBar();
			this.NumLeavesTrackBar = new global::System.Windows.Forms.TrackBar();
			this.ProbabilityTrackBar = new global::System.Windows.Forms.TrackBar();
			this.DeleteFoliageTextureButton = new global::System.Windows.Forms.Button();
			this.ExportFoliageTextureButton = new global::System.Windows.Forms.Button();
			this.BottomWidthLabel = new global::System.Windows.Forms.Label();
			this.BottomOffsetLabel = new global::System.Windows.Forms.Label();
			this.BottomHeightLabel = new global::System.Windows.Forms.Label();
			this.TopOffsetLabel = new global::System.Windows.Forms.Label();
			this.TopHeightLabel = new global::System.Windows.Forms.Label();
			this.MaxScale = new global::System.Windows.Forms.Label();
			this.MinScalLabel = new global::System.Windows.Forms.Label();
			this.NumLeavesLable = new global::System.Windows.Forms.Label();
			this.ProbabilityLablel = new global::System.Windows.Forms.Label();
			this.BottomWidthTextBox = new global::System.Windows.Forms.TextBox();
			this.BottomOffsetTextBox = new global::System.Windows.Forms.TextBox();
			this.BottomHeightTextBox = new global::System.Windows.Forms.TextBox();
			this.TopWidthTextBox = new global::System.Windows.Forms.TextBox();
			this.TopOffsetTextBox = new global::System.Windows.Forms.TextBox();
			this.TopHeightTextBox = new global::System.Windows.Forms.TextBox();
			this.MaxScaleTextBox = new global::System.Windows.Forms.TextBox();
			this.MinScaleTextBox = new global::System.Windows.Forms.TextBox();
			this.NumLeavesTextBox = new global::System.Windows.Forms.TextBox();
			this.ProbabilityTextBox = new global::System.Windows.Forms.TextBox();
			this.FoliageTextureTextBox = new global::System.Windows.Forms.TextBox();
			this.FoliageGroupBox = new global::System.Windows.Forms.GroupBox();
			this.Foliage3RadioButton = new global::System.Windows.Forms.RadioButton();
			this.Foliage2RadioButton = new global::System.Windows.Forms.RadioButton();
			this.Foliage1RadioButton = new global::System.Windows.Forms.RadioButton();
			this.Foliage0RadioButton = new global::System.Windows.Forms.RadioButton();
			this.CopyLayerButton = new global::System.Windows.Forms.Button();
			this.LayerLabel = new global::System.Windows.Forms.Label();
			this.LayerComboBox = new global::System.Windows.Forms.ComboBox();
			this.label1 = new global::System.Windows.Forms.Label();
			this.TerrainTextureTextBox = new global::System.Windows.Forms.TextBox();
			this.label2 = new global::System.Windows.Forms.Label();
			this.ExportTerrainTextureButton = new global::System.Windows.Forms.Button();
			this.MinimapColorPanel = new global::System.Windows.Forms.Panel();
			this.MinimapColorButton = new global::System.Windows.Forms.Button();
			this.NewLayerButton = new global::System.Windows.Forms.Button();
			this.RenameLayerButton = new global::System.Windows.Forms.Button();
			this.CloseButton = new global::System.Windows.Forms.Button();
			this.panel1.SuspendLayout();
			((global::System.ComponentModel.ISupportInitialize)this.BottomWidthTrackBar).BeginInit();
			((global::System.ComponentModel.ISupportInitialize)this.BottomOffsetTrackBar).BeginInit();
			((global::System.ComponentModel.ISupportInitialize)this.BottomHeightTrackBar).BeginInit();
			((global::System.ComponentModel.ISupportInitialize)this.TopWidthTrackBar).BeginInit();
			((global::System.ComponentModel.ISupportInitialize)this.TopOffsetTrackBar).BeginInit();
			((global::System.ComponentModel.ISupportInitialize)this.TopHeightTrackBar).BeginInit();
			((global::System.ComponentModel.ISupportInitialize)this.MaxScaleTrackBar).BeginInit();
			((global::System.ComponentModel.ISupportInitialize)this.MinScaleTrackBar).BeginInit();
			((global::System.ComponentModel.ISupportInitialize)this.NumLeavesTrackBar).BeginInit();
			((global::System.ComponentModel.ISupportInitialize)this.ProbabilityTrackBar).BeginInit();
			this.FoliageGroupBox.SuspendLayout();
			base.SuspendLayout();
			this.ContinentComboBox.DropDownStyle = global::System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.ContinentComboBox.FormattingEnabled = true;
			resources.ApplyResources(this.ContinentComboBox, "ContinentComboBox");
			this.ContinentComboBox.Name = "ContinentComboBox";
			resources.ApplyResources(this.SaveButton, "SaveButton");
			this.SaveButton.Name = "SaveButton";
			this.SaveButton.UseVisualStyleBackColor = true;
			resources.ApplyResources(this.ContinentLabel, "ContinentLabel");
			this.ContinentLabel.Name = "ContinentLabel";
			resources.ApplyResources(this.TextuteLable, "TextuteLable");
			this.TextuteLable.Name = "TextuteLable";
			resources.ApplyResources(this.TopWidthLabel, "TopWidthLabel");
			this.TopWidthLabel.Name = "TopWidthLabel";
			resources.ApplyResources(this.panel1, "panel1");
			this.panel1.BorderStyle = global::System.Windows.Forms.BorderStyle.FixedSingle;
			this.panel1.Controls.Add(this.label22);
			this.panel1.Controls.Add(this.label21);
			this.panel1.Controls.Add(this.label20);
			this.panel1.Controls.Add(this.label19);
			this.panel1.Controls.Add(this.label18);
			this.panel1.Controls.Add(this.label17);
			this.panel1.Controls.Add(this.label16);
			this.panel1.Controls.Add(this.label15);
			this.panel1.Controls.Add(this.label14);
			this.panel1.Controls.Add(this.label13);
			this.panel1.Controls.Add(this.label12);
			this.panel1.Controls.Add(this.label11);
			this.panel1.Controls.Add(this.label10);
			this.panel1.Controls.Add(this.label9);
			this.panel1.Controls.Add(this.label8);
			this.panel1.Controls.Add(this.label7);
			this.panel1.Controls.Add(this.label6);
			this.panel1.Controls.Add(this.label5);
			this.panel1.Controls.Add(this.label4);
			this.panel1.Controls.Add(this.label3);
			this.panel1.Controls.Add(this.BottomWidthTrackBar);
			this.panel1.Controls.Add(this.BottomOffsetTrackBar);
			this.panel1.Controls.Add(this.BottomHeightTrackBar);
			this.panel1.Controls.Add(this.TopWidthTrackBar);
			this.panel1.Controls.Add(this.TopOffsetTrackBar);
			this.panel1.Controls.Add(this.TopHeightTrackBar);
			this.panel1.Controls.Add(this.MaxScaleTrackBar);
			this.panel1.Controls.Add(this.MinScaleTrackBar);
			this.panel1.Controls.Add(this.NumLeavesTrackBar);
			this.panel1.Controls.Add(this.ProbabilityTrackBar);
			this.panel1.Controls.Add(this.DeleteFoliageTextureButton);
			this.panel1.Controls.Add(this.ExportFoliageTextureButton);
			this.panel1.Controls.Add(this.TopWidthLabel);
			this.panel1.Controls.Add(this.TextuteLable);
			this.panel1.Controls.Add(this.BottomWidthLabel);
			this.panel1.Controls.Add(this.BottomOffsetLabel);
			this.panel1.Controls.Add(this.BottomHeightLabel);
			this.panel1.Controls.Add(this.TopOffsetLabel);
			this.panel1.Controls.Add(this.TopHeightLabel);
			this.panel1.Controls.Add(this.MaxScale);
			this.panel1.Controls.Add(this.MinScalLabel);
			this.panel1.Controls.Add(this.NumLeavesLable);
			this.panel1.Controls.Add(this.ProbabilityLablel);
			this.panel1.Controls.Add(this.BottomWidthTextBox);
			this.panel1.Controls.Add(this.BottomOffsetTextBox);
			this.panel1.Controls.Add(this.BottomHeightTextBox);
			this.panel1.Controls.Add(this.TopWidthTextBox);
			this.panel1.Controls.Add(this.TopOffsetTextBox);
			this.panel1.Controls.Add(this.TopHeightTextBox);
			this.panel1.Controls.Add(this.MaxScaleTextBox);
			this.panel1.Controls.Add(this.MinScaleTextBox);
			this.panel1.Controls.Add(this.NumLeavesTextBox);
			this.panel1.Controls.Add(this.ProbabilityTextBox);
			this.panel1.Controls.Add(this.FoliageTextureTextBox);
			this.panel1.Controls.Add(this.FoliageGroupBox);
			this.panel1.Name = "panel1";
			resources.ApplyResources(this.label22, "label22");
			this.label22.Name = "label22";
			resources.ApplyResources(this.label21, "label21");
			this.label21.Name = "label21";
			resources.ApplyResources(this.label20, "label20");
			this.label20.Name = "label20";
			resources.ApplyResources(this.label19, "label19");
			this.label19.Name = "label19";
			resources.ApplyResources(this.label18, "label18");
			this.label18.Name = "label18";
			resources.ApplyResources(this.label17, "label17");
			this.label17.Name = "label17";
			resources.ApplyResources(this.label16, "label16");
			this.label16.Name = "label16";
			resources.ApplyResources(this.label15, "label15");
			this.label15.Name = "label15";
			resources.ApplyResources(this.label14, "label14");
			this.label14.Name = "label14";
			resources.ApplyResources(this.label13, "label13");
			this.label13.Name = "label13";
			resources.ApplyResources(this.label12, "label12");
			this.label12.Name = "label12";
			resources.ApplyResources(this.label11, "label11");
			this.label11.Name = "label11";
			resources.ApplyResources(this.label10, "label10");
			this.label10.Name = "label10";
			resources.ApplyResources(this.label9, "label9");
			this.label9.Name = "label9";
			resources.ApplyResources(this.label8, "label8");
			this.label8.Name = "label8";
			resources.ApplyResources(this.label7, "label7");
			this.label7.Name = "label7";
			resources.ApplyResources(this.label6, "label6");
			this.label6.Name = "label6";
			resources.ApplyResources(this.label5, "label5");
			this.label5.Name = "label5";
			resources.ApplyResources(this.label4, "label4");
			this.label4.Name = "label4";
			resources.ApplyResources(this.label3, "label3");
			this.label3.Name = "label3";
			resources.ApplyResources(this.BottomWidthTrackBar, "BottomWidthTrackBar");
			this.BottomWidthTrackBar.Maximum = 100;
			this.BottomWidthTrackBar.Name = "BottomWidthTrackBar";
			resources.ApplyResources(this.BottomOffsetTrackBar, "BottomOffsetTrackBar");
			this.BottomOffsetTrackBar.Maximum = 100;
			this.BottomOffsetTrackBar.Name = "BottomOffsetTrackBar";
			resources.ApplyResources(this.BottomHeightTrackBar, "BottomHeightTrackBar");
			this.BottomHeightTrackBar.Maximum = 100;
			this.BottomHeightTrackBar.Name = "BottomHeightTrackBar";
			resources.ApplyResources(this.TopWidthTrackBar, "TopWidthTrackBar");
			this.TopWidthTrackBar.Maximum = 100;
			this.TopWidthTrackBar.Name = "TopWidthTrackBar";
			resources.ApplyResources(this.TopOffsetTrackBar, "TopOffsetTrackBar");
			this.TopOffsetTrackBar.Maximum = 100;
			this.TopOffsetTrackBar.Name = "TopOffsetTrackBar";
			resources.ApplyResources(this.TopHeightTrackBar, "TopHeightTrackBar");
			this.TopHeightTrackBar.Maximum = 100;
			this.TopHeightTrackBar.Name = "TopHeightTrackBar";
			resources.ApplyResources(this.MaxScaleTrackBar, "MaxScaleTrackBar");
			this.MaxScaleTrackBar.Maximum = 100;
			this.MaxScaleTrackBar.Name = "MaxScaleTrackBar";
			resources.ApplyResources(this.MinScaleTrackBar, "MinScaleTrackBar");
			this.MinScaleTrackBar.Maximum = 100;
			this.MinScaleTrackBar.Name = "MinScaleTrackBar";
			resources.ApplyResources(this.NumLeavesTrackBar, "NumLeavesTrackBar");
			this.NumLeavesTrackBar.LargeChange = 1;
			this.NumLeavesTrackBar.Name = "NumLeavesTrackBar";
			resources.ApplyResources(this.ProbabilityTrackBar, "ProbabilityTrackBar");
			this.ProbabilityTrackBar.Maximum = 30;
			this.ProbabilityTrackBar.Name = "ProbabilityTrackBar";
			resources.ApplyResources(this.DeleteFoliageTextureButton, "DeleteFoliageTextureButton");
			this.DeleteFoliageTextureButton.Name = "DeleteFoliageTextureButton";
			this.DeleteFoliageTextureButton.UseVisualStyleBackColor = true;
			resources.ApplyResources(this.ExportFoliageTextureButton, "ExportFoliageTextureButton");
			this.ExportFoliageTextureButton.Name = "ExportFoliageTextureButton";
			this.ExportFoliageTextureButton.UseVisualStyleBackColor = true;
			resources.ApplyResources(this.BottomWidthLabel, "BottomWidthLabel");
			this.BottomWidthLabel.Name = "BottomWidthLabel";
			resources.ApplyResources(this.BottomOffsetLabel, "BottomOffsetLabel");
			this.BottomOffsetLabel.Name = "BottomOffsetLabel";
			resources.ApplyResources(this.BottomHeightLabel, "BottomHeightLabel");
			this.BottomHeightLabel.Name = "BottomHeightLabel";
			resources.ApplyResources(this.TopOffsetLabel, "TopOffsetLabel");
			this.TopOffsetLabel.Name = "TopOffsetLabel";
			resources.ApplyResources(this.TopHeightLabel, "TopHeightLabel");
			this.TopHeightLabel.Name = "TopHeightLabel";
			resources.ApplyResources(this.MaxScale, "MaxScale");
			this.MaxScale.Name = "MaxScale";
			resources.ApplyResources(this.MinScalLabel, "MinScalLabel");
			this.MinScalLabel.Name = "MinScalLabel";
			resources.ApplyResources(this.NumLeavesLable, "NumLeavesLable");
			this.NumLeavesLable.Name = "NumLeavesLable";
			resources.ApplyResources(this.ProbabilityLablel, "ProbabilityLablel");
			this.ProbabilityLablel.Name = "ProbabilityLablel";
			resources.ApplyResources(this.BottomWidthTextBox, "BottomWidthTextBox");
			this.BottomWidthTextBox.Name = "BottomWidthTextBox";
			resources.ApplyResources(this.BottomOffsetTextBox, "BottomOffsetTextBox");
			this.BottomOffsetTextBox.Name = "BottomOffsetTextBox";
			resources.ApplyResources(this.BottomHeightTextBox, "BottomHeightTextBox");
			this.BottomHeightTextBox.Name = "BottomHeightTextBox";
			resources.ApplyResources(this.TopWidthTextBox, "TopWidthTextBox");
			this.TopWidthTextBox.Name = "TopWidthTextBox";
			resources.ApplyResources(this.TopOffsetTextBox, "TopOffsetTextBox");
			this.TopOffsetTextBox.Name = "TopOffsetTextBox";
			resources.ApplyResources(this.TopHeightTextBox, "TopHeightTextBox");
			this.TopHeightTextBox.Name = "TopHeightTextBox";
			resources.ApplyResources(this.MaxScaleTextBox, "MaxScaleTextBox");
			this.MaxScaleTextBox.Name = "MaxScaleTextBox";
			resources.ApplyResources(this.MinScaleTextBox, "MinScaleTextBox");
			this.MinScaleTextBox.Name = "MinScaleTextBox";
			resources.ApplyResources(this.NumLeavesTextBox, "NumLeavesTextBox");
			this.NumLeavesTextBox.Name = "NumLeavesTextBox";
			resources.ApplyResources(this.ProbabilityTextBox, "ProbabilityTextBox");
			this.ProbabilityTextBox.Name = "ProbabilityTextBox";
			resources.ApplyResources(this.FoliageTextureTextBox, "FoliageTextureTextBox");
			this.FoliageTextureTextBox.Name = "FoliageTextureTextBox";
			this.FoliageGroupBox.Controls.Add(this.Foliage3RadioButton);
			this.FoliageGroupBox.Controls.Add(this.Foliage2RadioButton);
			this.FoliageGroupBox.Controls.Add(this.Foliage1RadioButton);
			this.FoliageGroupBox.Controls.Add(this.Foliage0RadioButton);
			resources.ApplyResources(this.FoliageGroupBox, "FoliageGroupBox");
			this.FoliageGroupBox.Name = "FoliageGroupBox";
			this.FoliageGroupBox.TabStop = false;
			resources.ApplyResources(this.Foliage3RadioButton, "Foliage3RadioButton");
			this.Foliage3RadioButton.Name = "Foliage3RadioButton";
			this.Foliage3RadioButton.UseVisualStyleBackColor = true;
			resources.ApplyResources(this.Foliage2RadioButton, "Foliage2RadioButton");
			this.Foliage2RadioButton.Name = "Foliage2RadioButton";
			this.Foliage2RadioButton.UseVisualStyleBackColor = true;
			resources.ApplyResources(this.Foliage1RadioButton, "Foliage1RadioButton");
			this.Foliage1RadioButton.Name = "Foliage1RadioButton";
			this.Foliage1RadioButton.UseVisualStyleBackColor = true;
			resources.ApplyResources(this.Foliage0RadioButton, "Foliage0RadioButton");
			this.Foliage0RadioButton.Checked = true;
			this.Foliage0RadioButton.Name = "Foliage0RadioButton";
			this.Foliage0RadioButton.TabStop = true;
			this.Foliage0RadioButton.UseVisualStyleBackColor = true;
			resources.ApplyResources(this.CopyLayerButton, "CopyLayerButton");
			this.CopyLayerButton.Name = "CopyLayerButton";
			this.CopyLayerButton.UseVisualStyleBackColor = true;
			resources.ApplyResources(this.LayerLabel, "LayerLabel");
			this.LayerLabel.Name = "LayerLabel";
			this.LayerComboBox.DropDownStyle = global::System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.LayerComboBox.FormattingEnabled = true;
			resources.ApplyResources(this.LayerComboBox, "LayerComboBox");
			this.LayerComboBox.Name = "LayerComboBox";
			resources.ApplyResources(this.label1, "label1");
			this.label1.Name = "label1";
			resources.ApplyResources(this.TerrainTextureTextBox, "TerrainTextureTextBox");
			this.TerrainTextureTextBox.Name = "TerrainTextureTextBox";
			resources.ApplyResources(this.label2, "label2");
			this.label2.Name = "label2";
			resources.ApplyResources(this.ExportTerrainTextureButton, "ExportTerrainTextureButton");
			this.ExportTerrainTextureButton.Name = "ExportTerrainTextureButton";
			this.ExportTerrainTextureButton.UseVisualStyleBackColor = true;
			this.MinimapColorPanel.BorderStyle = global::System.Windows.Forms.BorderStyle.FixedSingle;
			resources.ApplyResources(this.MinimapColorPanel, "MinimapColorPanel");
			this.MinimapColorPanel.Name = "MinimapColorPanel";
			resources.ApplyResources(this.MinimapColorButton, "MinimapColorButton");
			this.MinimapColorButton.Name = "MinimapColorButton";
			this.MinimapColorButton.UseVisualStyleBackColor = true;
			resources.ApplyResources(this.NewLayerButton, "NewLayerButton");
			this.NewLayerButton.Name = "NewLayerButton";
			this.NewLayerButton.UseVisualStyleBackColor = true;
			resources.ApplyResources(this.RenameLayerButton, "RenameLayerButton");
			this.RenameLayerButton.Name = "RenameLayerButton";
			this.RenameLayerButton.UseVisualStyleBackColor = true;
			resources.ApplyResources(this.CloseButton, "CloseButton");
			this.CloseButton.DialogResult = global::System.Windows.Forms.DialogResult.Cancel;
			this.CloseButton.Name = "CloseButton";
			this.CloseButton.UseVisualStyleBackColor = true;
			resources.ApplyResources(this, "$this");
			base.AutoScaleMode = global::System.Windows.Forms.AutoScaleMode.Font;
			base.CancelButton = this.CloseButton;
			base.Controls.Add(this.CloseButton);
			base.Controls.Add(this.RenameLayerButton);
			base.Controls.Add(this.NewLayerButton);
			base.Controls.Add(this.MinimapColorButton);
			base.Controls.Add(this.MinimapColorPanel);
			base.Controls.Add(this.ExportTerrainTextureButton);
			base.Controls.Add(this.label2);
			base.Controls.Add(this.TerrainTextureTextBox);
			base.Controls.Add(this.label1);
			base.Controls.Add(this.panel1);
			base.Controls.Add(this.CopyLayerButton);
			base.Controls.Add(this.ContinentLabel);
			base.Controls.Add(this.SaveButton);
			base.Controls.Add(this.ContinentComboBox);
			base.Controls.Add(this.LayerComboBox);
			base.Controls.Add(this.LayerLabel);
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.Name = "LayersForm";
			base.ShowIcon = false;
			base.ShowInTaskbar = false;
			this.panel1.ResumeLayout(false);
			this.panel1.PerformLayout();
			((global::System.ComponentModel.ISupportInitialize)this.BottomWidthTrackBar).EndInit();
			((global::System.ComponentModel.ISupportInitialize)this.BottomOffsetTrackBar).EndInit();
			((global::System.ComponentModel.ISupportInitialize)this.BottomHeightTrackBar).EndInit();
			((global::System.ComponentModel.ISupportInitialize)this.TopWidthTrackBar).EndInit();
			((global::System.ComponentModel.ISupportInitialize)this.TopOffsetTrackBar).EndInit();
			((global::System.ComponentModel.ISupportInitialize)this.TopHeightTrackBar).EndInit();
			((global::System.ComponentModel.ISupportInitialize)this.MaxScaleTrackBar).EndInit();
			((global::System.ComponentModel.ISupportInitialize)this.MinScaleTrackBar).EndInit();
			((global::System.ComponentModel.ISupportInitialize)this.NumLeavesTrackBar).EndInit();
			((global::System.ComponentModel.ISupportInitialize)this.ProbabilityTrackBar).EndInit();
			this.FoliageGroupBox.ResumeLayout(false);
			this.FoliageGroupBox.PerformLayout();
			base.ResumeLayout(false);
			base.PerformLayout();
		}

		// Token: 0x04000D84 RID: 3460
		private global::System.ComponentModel.IContainer components;

		// Token: 0x04000D85 RID: 3461
		private global::System.Windows.Forms.ComboBox ContinentComboBox;

		// Token: 0x04000D86 RID: 3462
		private global::System.Windows.Forms.Button SaveButton;

		// Token: 0x04000D87 RID: 3463
		private global::System.Windows.Forms.Label ContinentLabel;

		// Token: 0x04000D88 RID: 3464
		private global::System.Windows.Forms.Label TextuteLable;

		// Token: 0x04000D89 RID: 3465
		private global::System.Windows.Forms.Label TopWidthLabel;

		// Token: 0x04000D8A RID: 3466
		private global::System.Windows.Forms.Panel panel1;

		// Token: 0x04000D8B RID: 3467
		private global::System.Windows.Forms.Button ExportFoliageTextureButton;

		// Token: 0x04000D8C RID: 3468
		private global::System.Windows.Forms.Label BottomWidthLabel;

		// Token: 0x04000D8D RID: 3469
		private global::System.Windows.Forms.Label BottomOffsetLabel;

		// Token: 0x04000D8E RID: 3470
		private global::System.Windows.Forms.Label BottomHeightLabel;

		// Token: 0x04000D8F RID: 3471
		private global::System.Windows.Forms.Label TopOffsetLabel;

		// Token: 0x04000D90 RID: 3472
		private global::System.Windows.Forms.Label TopHeightLabel;

		// Token: 0x04000D91 RID: 3473
		private global::System.Windows.Forms.Label MaxScale;

		// Token: 0x04000D92 RID: 3474
		private global::System.Windows.Forms.Label MinScalLabel;

		// Token: 0x04000D93 RID: 3475
		private global::System.Windows.Forms.Label NumLeavesLable;

		// Token: 0x04000D94 RID: 3476
		private global::System.Windows.Forms.Label ProbabilityLablel;

		// Token: 0x04000D95 RID: 3477
		private global::System.Windows.Forms.Label LayerLabel;

		// Token: 0x04000D96 RID: 3478
		private global::System.Windows.Forms.TextBox BottomWidthTextBox;

		// Token: 0x04000D97 RID: 3479
		private global::System.Windows.Forms.TextBox BottomOffsetTextBox;

		// Token: 0x04000D98 RID: 3480
		private global::System.Windows.Forms.TextBox BottomHeightTextBox;

		// Token: 0x04000D99 RID: 3481
		private global::System.Windows.Forms.TextBox TopWidthTextBox;

		// Token: 0x04000D9A RID: 3482
		private global::System.Windows.Forms.TextBox TopOffsetTextBox;

		// Token: 0x04000D9B RID: 3483
		private global::System.Windows.Forms.TextBox TopHeightTextBox;

		// Token: 0x04000D9C RID: 3484
		private global::System.Windows.Forms.TextBox MaxScaleTextBox;

		// Token: 0x04000D9D RID: 3485
		private global::System.Windows.Forms.TextBox MinScaleTextBox;

		// Token: 0x04000D9E RID: 3486
		private global::System.Windows.Forms.TextBox NumLeavesTextBox;

		// Token: 0x04000D9F RID: 3487
		private global::System.Windows.Forms.TextBox ProbabilityTextBox;

		// Token: 0x04000DA0 RID: 3488
		private global::System.Windows.Forms.TextBox FoliageTextureTextBox;

		// Token: 0x04000DA1 RID: 3489
		private global::System.Windows.Forms.GroupBox FoliageGroupBox;

		// Token: 0x04000DA2 RID: 3490
		private global::System.Windows.Forms.RadioButton Foliage3RadioButton;

		// Token: 0x04000DA3 RID: 3491
		private global::System.Windows.Forms.RadioButton Foliage2RadioButton;

		// Token: 0x04000DA4 RID: 3492
		private global::System.Windows.Forms.RadioButton Foliage1RadioButton;

		// Token: 0x04000DA5 RID: 3493
		private global::System.Windows.Forms.RadioButton Foliage0RadioButton;

		// Token: 0x04000DA6 RID: 3494
		private global::System.Windows.Forms.ComboBox LayerComboBox;

		// Token: 0x04000DA7 RID: 3495
		private global::System.Windows.Forms.Button CopyLayerButton;

		// Token: 0x04000DA8 RID: 3496
		private global::System.Windows.Forms.Button DeleteFoliageTextureButton;

		// Token: 0x04000DA9 RID: 3497
		private global::System.Windows.Forms.Label label1;

		// Token: 0x04000DAA RID: 3498
		private global::System.Windows.Forms.TextBox TerrainTextureTextBox;

		// Token: 0x04000DAB RID: 3499
		private global::System.Windows.Forms.Label label2;

		// Token: 0x04000DAC RID: 3500
		private global::System.Windows.Forms.Button ExportTerrainTextureButton;

		// Token: 0x04000DAD RID: 3501
		private global::System.Windows.Forms.Panel MinimapColorPanel;

		// Token: 0x04000DAE RID: 3502
		private global::System.Windows.Forms.Button MinimapColorButton;

		// Token: 0x04000DAF RID: 3503
		private global::System.Windows.Forms.Button NewLayerButton;

		// Token: 0x04000DB0 RID: 3504
		private global::System.Windows.Forms.Button RenameLayerButton;

		// Token: 0x04000DB1 RID: 3505
		private global::System.Windows.Forms.TrackBar ProbabilityTrackBar;

		// Token: 0x04000DB2 RID: 3506
		private global::System.Windows.Forms.TrackBar NumLeavesTrackBar;

		// Token: 0x04000DB3 RID: 3507
		private global::System.Windows.Forms.TrackBar BottomWidthTrackBar;

		// Token: 0x04000DB4 RID: 3508
		private global::System.Windows.Forms.TrackBar BottomOffsetTrackBar;

		// Token: 0x04000DB5 RID: 3509
		private global::System.Windows.Forms.TrackBar BottomHeightTrackBar;

		// Token: 0x04000DB6 RID: 3510
		private global::System.Windows.Forms.TrackBar TopWidthTrackBar;

		// Token: 0x04000DB7 RID: 3511
		private global::System.Windows.Forms.TrackBar TopOffsetTrackBar;

		// Token: 0x04000DB8 RID: 3512
		private global::System.Windows.Forms.TrackBar TopHeightTrackBar;

		// Token: 0x04000DB9 RID: 3513
		private global::System.Windows.Forms.TrackBar MaxScaleTrackBar;

		// Token: 0x04000DBA RID: 3514
		private global::System.Windows.Forms.TrackBar MinScaleTrackBar;

		// Token: 0x04000DBB RID: 3515
		private global::System.Windows.Forms.Label label4;

		// Token: 0x04000DBC RID: 3516
		private global::System.Windows.Forms.Label label3;

		// Token: 0x04000DBD RID: 3517
		private global::System.Windows.Forms.Label label5;

		// Token: 0x04000DBE RID: 3518
		private global::System.Windows.Forms.Label label22;

		// Token: 0x04000DBF RID: 3519
		private global::System.Windows.Forms.Label label21;

		// Token: 0x04000DC0 RID: 3520
		private global::System.Windows.Forms.Label label20;

		// Token: 0x04000DC1 RID: 3521
		private global::System.Windows.Forms.Label label19;

		// Token: 0x04000DC2 RID: 3522
		private global::System.Windows.Forms.Label label18;

		// Token: 0x04000DC3 RID: 3523
		private global::System.Windows.Forms.Label label17;

		// Token: 0x04000DC4 RID: 3524
		private global::System.Windows.Forms.Label label16;

		// Token: 0x04000DC5 RID: 3525
		private global::System.Windows.Forms.Label label15;

		// Token: 0x04000DC6 RID: 3526
		private global::System.Windows.Forms.Label label14;

		// Token: 0x04000DC7 RID: 3527
		private global::System.Windows.Forms.Label label13;

		// Token: 0x04000DC8 RID: 3528
		private global::System.Windows.Forms.Label label12;

		// Token: 0x04000DC9 RID: 3529
		private global::System.Windows.Forms.Label label11;

		// Token: 0x04000DCA RID: 3530
		private global::System.Windows.Forms.Label label10;

		// Token: 0x04000DCB RID: 3531
		private global::System.Windows.Forms.Label label9;

		// Token: 0x04000DCC RID: 3532
		private global::System.Windows.Forms.Label label8;

		// Token: 0x04000DCD RID: 3533
		private global::System.Windows.Forms.Label label7;

		// Token: 0x04000DCE RID: 3534
		private global::System.Windows.Forms.Label label6;

		// Token: 0x04000DCF RID: 3535
		private global::System.Windows.Forms.Button CloseButton;
	}
}
