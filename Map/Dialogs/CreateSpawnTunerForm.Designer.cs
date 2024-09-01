namespace MapEditor.Map.Dialogs
{
	// Token: 0x0200010F RID: 271
	public partial class CreateSpawnTunerForm : global::System.Windows.Forms.Form
	{
		// Token: 0x06000D3F RID: 3391 RVA: 0x0006EF0B File Offset: 0x0006DF0B
		protected override void Dispose(bool disposing)
		{
			if (disposing && this.components != null)
			{
				this.components.Dispose();
			}
			base.Dispose(disposing);
		}

		// Token: 0x06000D40 RID: 3392 RVA: 0x0006EF2C File Offset: 0x0006DF2C
		private void InitializeComponent()
		{
			this.components = new global::System.ComponentModel.Container();
			global::System.ComponentModel.ComponentResourceManager resources = new global::System.ComponentModel.ComponentResourceManager(typeof(global::MapEditor.Map.Dialogs.CreateSpawnTunerForm));
			this.NewSpawnTunerLabel = new global::System.Windows.Forms.Label();
			this.NewSpawnTunerTextBox = new global::System.Windows.Forms.TextBox();
			this.CreateButton = new global::System.Windows.Forms.Button();
			this.cancelButton = new global::System.Windows.Forms.Button();
			this.TextBoxTimer = new global::System.Windows.Forms.Timer(this.components);
			this.QuestionLabel = new global::System.Windows.Forms.Label();
			base.SuspendLayout();
			resources.ApplyResources(this.NewSpawnTunerLabel, "NewSpawnTunerLabel");
			this.NewSpawnTunerLabel.Name = "NewSpawnTunerLabel";
			resources.ApplyResources(this.NewSpawnTunerTextBox, "NewSpawnTunerTextBox");
			this.NewSpawnTunerTextBox.Name = "NewSpawnTunerTextBox";
			this.NewSpawnTunerTextBox.TextChanged += new global::System.EventHandler(this.NewSpawnTunerTextBox_TextChanged);
			this.CreateButton.DialogResult = global::System.Windows.Forms.DialogResult.OK;
			resources.ApplyResources(this.CreateButton, "CreateButton");
			this.CreateButton.Name = "CreateButton";
			this.CreateButton.UseVisualStyleBackColor = true;
			this.cancelButton.DialogResult = global::System.Windows.Forms.DialogResult.Cancel;
			resources.ApplyResources(this.cancelButton, "cancelButton");
			this.cancelButton.Name = "cancelButton";
			this.cancelButton.UseVisualStyleBackColor = true;
			this.TextBoxTimer.Interval = 1000;
			this.TextBoxTimer.Tick += new global::System.EventHandler(this.TextBoxTimer_Tick);
			resources.ApplyResources(this.QuestionLabel, "QuestionLabel");
			this.QuestionLabel.Name = "QuestionLabel";
			base.AcceptButton = this.CreateButton;
			resources.ApplyResources(this, "$this");
			base.AutoScaleMode = global::System.Windows.Forms.AutoScaleMode.Font;
			base.CancelButton = this.cancelButton;
			base.Controls.Add(this.QuestionLabel);
			base.Controls.Add(this.NewSpawnTunerLabel);
			base.Controls.Add(this.NewSpawnTunerTextBox);
			base.Controls.Add(this.CreateButton);
			base.Controls.Add(this.cancelButton);
			base.FormBorderStyle = global::System.Windows.Forms.FormBorderStyle.FixedSingle;
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.Name = "CreateSpawnTunerForm";
			base.ShowIcon = false;
			base.ShowInTaskbar = false;
			base.ResumeLayout(false);
			base.PerformLayout();
		}

		// Token: 0x04000A9C RID: 2716
		private global::System.ComponentModel.IContainer components;

		// Token: 0x04000A9D RID: 2717
		private global::System.Windows.Forms.Label NewSpawnTunerLabel;

		// Token: 0x04000A9E RID: 2718
		private global::System.Windows.Forms.TextBox NewSpawnTunerTextBox;

		// Token: 0x04000A9F RID: 2719
		private global::System.Windows.Forms.Button CreateButton;

		// Token: 0x04000AA0 RID: 2720
		private global::System.Windows.Forms.Button cancelButton;

		// Token: 0x04000AA1 RID: 2721
		private global::System.Windows.Forms.Timer TextBoxTimer;

		// Token: 0x04000AA2 RID: 2722
		private global::System.Windows.Forms.Label QuestionLabel;
	}
}
