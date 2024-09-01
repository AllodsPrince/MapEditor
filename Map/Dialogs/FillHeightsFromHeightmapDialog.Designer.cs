namespace MapEditor.Map.Dialogs
{
	// Token: 0x02000181 RID: 385
	public partial class FillHeightsFromHeightmapDialog : global::System.Windows.Forms.Form
	{
		// Token: 0x06001252 RID: 4690 RVA: 0x00085504 File Offset: 0x00084504
		protected override void Dispose(bool disposing)
		{
			if (disposing && this.components != null)
			{
				this.components.Dispose();
			}
			base.Dispose(disposing);
		}

		// Token: 0x06001253 RID: 4691 RVA: 0x00085524 File Offset: 0x00084524
		private void InitializeComponent()
		{
			this.components = new global::System.ComponentModel.Container();
			global::System.ComponentModel.ComponentResourceManager resources = new global::System.ComponentModel.ComponentResourceManager(typeof(global::MapEditor.Map.Dialogs.FillHeightsFromHeightmapDialog));
			this.Cancelbutton = new global::System.Windows.Forms.Button();
			this.OKbutton = new global::System.Windows.Forms.Button();
			this.ApplyToActiveMapRadioButton = new global::System.Windows.Forms.RadioButton();
			this.ApplyToSeveralPatchesRadioButton = new global::System.Windows.Forms.RadioButton();
			this.HeightmapLabel = new global::System.Windows.Forms.Label();
			this.HeightmapTextbox = new global::System.Windows.Forms.TextBox();
			this.HeightmapBrowseButton = new global::System.Windows.Forms.Button();
			this.WhiteHeightLabelLeft = new global::System.Windows.Forms.Label();
			this.WhiteHeightTextBox = new global::System.Windows.Forms.TextBox();
			this.BlackHeightLabelLeft = new global::System.Windows.Forms.Label();
			this.BlackHeightTextBox = new global::System.Windows.Forms.TextBox();
			this.WhiteHeightLabelRight = new global::System.Windows.Forms.Label();
			this.BlackHeightLabelRight = new global::System.Windows.Forms.Label();
			this.StartXYLabelLeft = new global::System.Windows.Forms.Label();
			this.StartXYTextBox = new global::System.Windows.Forms.TextBox();
			this.FinishXYLabelLeft = new global::System.Windows.Forms.Label();
			this.FinishXYTextBox = new global::System.Windows.Forms.TextBox();
			this.StartXYLabelRight = new global::System.Windows.Forms.Label();
			this.FinishXYLabeLRight = new global::System.Windows.Forms.Label();
			this.EditBoxTimer = new global::System.Windows.Forms.Timer(this.components);
			base.SuspendLayout();
			resources.ApplyResources(this.Cancelbutton, "Cancelbutton");
			this.Cancelbutton.DialogResult = global::System.Windows.Forms.DialogResult.Cancel;
			this.Cancelbutton.Name = "Cancelbutton";
			this.Cancelbutton.UseVisualStyleBackColor = true;
			resources.ApplyResources(this.OKbutton, "OKbutton");
			this.OKbutton.DialogResult = global::System.Windows.Forms.DialogResult.OK;
			this.OKbutton.Name = "OKbutton";
			this.OKbutton.UseVisualStyleBackColor = true;
			resources.ApplyResources(this.ApplyToActiveMapRadioButton, "ApplyToActiveMapRadioButton");
			this.ApplyToActiveMapRadioButton.Name = "ApplyToActiveMapRadioButton";
			this.ApplyToActiveMapRadioButton.TabStop = true;
			this.ApplyToActiveMapRadioButton.UseVisualStyleBackColor = true;
			this.ApplyToActiveMapRadioButton.CheckedChanged += new global::System.EventHandler(this.ApplyToActiveMapRadioButton_CheckedChanged);
			resources.ApplyResources(this.ApplyToSeveralPatchesRadioButton, "ApplyToSeveralPatchesRadioButton");
			this.ApplyToSeveralPatchesRadioButton.Name = "ApplyToSeveralPatchesRadioButton";
			this.ApplyToSeveralPatchesRadioButton.TabStop = true;
			this.ApplyToSeveralPatchesRadioButton.UseVisualStyleBackColor = true;
			this.ApplyToSeveralPatchesRadioButton.CheckedChanged += new global::System.EventHandler(this.ApplyToSeveralPatchesRadioButton_CheckedChanged);
			resources.ApplyResources(this.HeightmapLabel, "HeightmapLabel");
			this.HeightmapLabel.Name = "HeightmapLabel";
			resources.ApplyResources(this.HeightmapTextbox, "HeightmapTextbox");
			this.HeightmapTextbox.Name = "HeightmapTextbox";
			this.HeightmapTextbox.TextChanged += new global::System.EventHandler(this.HeightmapTextbox_TextChanged);
			resources.ApplyResources(this.HeightmapBrowseButton, "HeightmapBrowseButton");
			this.HeightmapBrowseButton.Name = "HeightmapBrowseButton";
			this.HeightmapBrowseButton.UseVisualStyleBackColor = true;
			this.HeightmapBrowseButton.Click += new global::System.EventHandler(this.HeightmapBrowseButton_Click);
			resources.ApplyResources(this.WhiteHeightLabelLeft, "WhiteHeightLabelLeft");
			this.WhiteHeightLabelLeft.Name = "WhiteHeightLabelLeft";
			resources.ApplyResources(this.WhiteHeightTextBox, "WhiteHeightTextBox");
			this.WhiteHeightTextBox.Name = "WhiteHeightTextBox";
			this.WhiteHeightTextBox.TextChanged += new global::System.EventHandler(this.WhiteHeightTextBox_TextChanged);
			resources.ApplyResources(this.BlackHeightLabelLeft, "BlackHeightLabelLeft");
			this.BlackHeightLabelLeft.Name = "BlackHeightLabelLeft";
			resources.ApplyResources(this.BlackHeightTextBox, "BlackHeightTextBox");
			this.BlackHeightTextBox.Name = "BlackHeightTextBox";
			this.BlackHeightTextBox.TextChanged += new global::System.EventHandler(this.BlackHeightTextBox_TextChanged);
			resources.ApplyResources(this.WhiteHeightLabelRight, "WhiteHeightLabelRight");
			this.WhiteHeightLabelRight.Name = "WhiteHeightLabelRight";
			resources.ApplyResources(this.BlackHeightLabelRight, "BlackHeightLabelRight");
			this.BlackHeightLabelRight.Name = "BlackHeightLabelRight";
			resources.ApplyResources(this.StartXYLabelLeft, "StartXYLabelLeft");
			this.StartXYLabelLeft.Name = "StartXYLabelLeft";
			resources.ApplyResources(this.StartXYTextBox, "StartXYTextBox");
			this.StartXYTextBox.Name = "StartXYTextBox";
			this.StartXYTextBox.TextChanged += new global::System.EventHandler(this.StartXYTextBox_TextChanged);
			resources.ApplyResources(this.FinishXYLabelLeft, "FinishXYLabelLeft");
			this.FinishXYLabelLeft.Name = "FinishXYLabelLeft";
			resources.ApplyResources(this.FinishXYTextBox, "FinishXYTextBox");
			this.FinishXYTextBox.Name = "FinishXYTextBox";
			this.FinishXYTextBox.TextChanged += new global::System.EventHandler(this.FinishXYTextBox_TextChanged);
			resources.ApplyResources(this.StartXYLabelRight, "StartXYLabelRight");
			this.StartXYLabelRight.Name = "StartXYLabelRight";
			resources.ApplyResources(this.FinishXYLabeLRight, "FinishXYLabeLRight");
			this.FinishXYLabeLRight.Name = "FinishXYLabeLRight";
			this.EditBoxTimer.Interval = 2000;
			this.EditBoxTimer.Tick += new global::System.EventHandler(this.EditBoxTimer_Tick);
			base.AcceptButton = this.OKbutton;
			resources.ApplyResources(this, "$this");
			base.AutoScaleMode = global::System.Windows.Forms.AutoScaleMode.Font;
			this.AutoValidate = global::System.Windows.Forms.AutoValidate.EnableAllowFocusChange;
			base.CancelButton = this.Cancelbutton;
			base.Controls.Add(this.FinishXYLabeLRight);
			base.Controls.Add(this.StartXYLabelRight);
			base.Controls.Add(this.FinishXYLabelLeft);
			base.Controls.Add(this.FinishXYTextBox);
			base.Controls.Add(this.StartXYLabelLeft);
			base.Controls.Add(this.StartXYTextBox);
			base.Controls.Add(this.BlackHeightLabelRight);
			base.Controls.Add(this.WhiteHeightLabelRight);
			base.Controls.Add(this.BlackHeightLabelLeft);
			base.Controls.Add(this.BlackHeightTextBox);
			base.Controls.Add(this.WhiteHeightLabelLeft);
			base.Controls.Add(this.WhiteHeightTextBox);
			base.Controls.Add(this.HeightmapBrowseButton);
			base.Controls.Add(this.HeightmapLabel);
			base.Controls.Add(this.HeightmapTextbox);
			base.Controls.Add(this.ApplyToActiveMapRadioButton);
			base.Controls.Add(this.ApplyToSeveralPatchesRadioButton);
			base.Controls.Add(this.Cancelbutton);
			base.Controls.Add(this.OKbutton);
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.Name = "FillHeightsFromHeightmapDialog";
			base.ResumeLayout(false);
			base.PerformLayout();
		}

		// Token: 0x04000D04 RID: 3332
		private global::System.ComponentModel.IContainer components;

		// Token: 0x04000D05 RID: 3333
		private global::System.Windows.Forms.Button Cancelbutton;

		// Token: 0x04000D06 RID: 3334
		private global::System.Windows.Forms.Button OKbutton;

		// Token: 0x04000D07 RID: 3335
		private global::System.Windows.Forms.RadioButton ApplyToActiveMapRadioButton;

		// Token: 0x04000D08 RID: 3336
		private global::System.Windows.Forms.RadioButton ApplyToSeveralPatchesRadioButton;

		// Token: 0x04000D09 RID: 3337
		private global::System.Windows.Forms.Label HeightmapLabel;

		// Token: 0x04000D0A RID: 3338
		private global::System.Windows.Forms.TextBox HeightmapTextbox;

		// Token: 0x04000D0B RID: 3339
		private global::System.Windows.Forms.Button HeightmapBrowseButton;

		// Token: 0x04000D0C RID: 3340
		private global::System.Windows.Forms.Label WhiteHeightLabelLeft;

		// Token: 0x04000D0D RID: 3341
		private global::System.Windows.Forms.TextBox WhiteHeightTextBox;

		// Token: 0x04000D0E RID: 3342
		private global::System.Windows.Forms.Label BlackHeightLabelLeft;

		// Token: 0x04000D0F RID: 3343
		private global::System.Windows.Forms.TextBox BlackHeightTextBox;

		// Token: 0x04000D10 RID: 3344
		private global::System.Windows.Forms.Label WhiteHeightLabelRight;

		// Token: 0x04000D11 RID: 3345
		private global::System.Windows.Forms.Label BlackHeightLabelRight;

		// Token: 0x04000D12 RID: 3346
		private global::System.Windows.Forms.Label StartXYLabelLeft;

		// Token: 0x04000D13 RID: 3347
		private global::System.Windows.Forms.TextBox StartXYTextBox;

		// Token: 0x04000D14 RID: 3348
		private global::System.Windows.Forms.Label FinishXYLabelLeft;

		// Token: 0x04000D15 RID: 3349
		private global::System.Windows.Forms.TextBox FinishXYTextBox;

		// Token: 0x04000D16 RID: 3350
		private global::System.Windows.Forms.Label StartXYLabelRight;

		// Token: 0x04000D17 RID: 3351
		private global::System.Windows.Forms.Label FinishXYLabeLRight;

		// Token: 0x04000D18 RID: 3352
		private global::System.Windows.Forms.Timer EditBoxTimer;
	}
}
