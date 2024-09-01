namespace MapEditor.Forms.About
{
	// Token: 0x02000118 RID: 280
	public partial class AboutForm : global::System.Windows.Forms.Form
	{
		// Token: 0x06000DC1 RID: 3521 RVA: 0x000734A3 File Offset: 0x000724A3
		protected override void Dispose(bool disposing)
		{
			if (disposing && this.components != null)
			{
				this.components.Dispose();
			}
			base.Dispose(disposing);
		}

		// Token: 0x06000DC2 RID: 3522 RVA: 0x000734C4 File Offset: 0x000724C4
		private void InitializeComponent()
		{
			global::System.ComponentModel.ComponentResourceManager resources = new global::System.ComponentModel.ComponentResourceManager(typeof(global::MapEditor.Forms.About.AboutForm));
			this.aboutOKButton = new global::System.Windows.Forms.Button();
			this.TitleLabel = new global::System.Windows.Forms.Label();
			this.VersionLabel = new global::System.Windows.Forms.Label();
			this.CopyrightLabel = new global::System.Windows.Forms.Label();
			base.SuspendLayout();
			this.aboutOKButton.AccessibleDescription = null;
			this.aboutOKButton.AccessibleName = null;
			resources.ApplyResources(this.aboutOKButton, "aboutOKButton");
			this.aboutOKButton.BackgroundImage = null;
			this.aboutOKButton.DialogResult = global::System.Windows.Forms.DialogResult.OK;
			this.aboutOKButton.Font = null;
			this.aboutOKButton.Name = "aboutOKButton";
			this.aboutOKButton.UseVisualStyleBackColor = true;
			this.TitleLabel.AccessibleDescription = null;
			this.TitleLabel.AccessibleName = null;
			resources.ApplyResources(this.TitleLabel, "TitleLabel");
			this.TitleLabel.Font = null;
			this.TitleLabel.Name = "TitleLabel";
			this.VersionLabel.AccessibleDescription = null;
			this.VersionLabel.AccessibleName = null;
			resources.ApplyResources(this.VersionLabel, "VersionLabel");
			this.VersionLabel.Font = null;
			this.VersionLabel.Name = "VersionLabel";
			this.CopyrightLabel.AccessibleDescription = null;
			this.CopyrightLabel.AccessibleName = null;
			resources.ApplyResources(this.CopyrightLabel, "CopyrightLabel");
			this.CopyrightLabel.Font = null;
			this.CopyrightLabel.Name = "CopyrightLabel";
			base.AcceptButton = this.aboutOKButton;
			base.AccessibleDescription = null;
			base.AccessibleName = null;
			resources.ApplyResources(this, "$this");
			base.AutoScaleMode = global::System.Windows.Forms.AutoScaleMode.Font;
			this.BackgroundImage = null;
			base.CancelButton = this.aboutOKButton;
			base.Controls.Add(this.CopyrightLabel);
			base.Controls.Add(this.VersionLabel);
			base.Controls.Add(this.TitleLabel);
			base.Controls.Add(this.aboutOKButton);
			this.Font = null;
			base.FormBorderStyle = global::System.Windows.Forms.FormBorderStyle.FixedDialog;
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.Name = "AboutForm";
			base.ShowInTaskbar = false;
			base.ResumeLayout(false);
		}

		// Token: 0x04000B01 RID: 2817
		private global::System.ComponentModel.IContainer components;

		// Token: 0x04000B02 RID: 2818
		private global::System.Windows.Forms.Button aboutOKButton;

		// Token: 0x04000B03 RID: 2819
		private global::System.Windows.Forms.Label TitleLabel;

		// Token: 0x04000B04 RID: 2820
		private global::System.Windows.Forms.Label VersionLabel;

		// Token: 0x04000B05 RID: 2821
		private global::System.Windows.Forms.Label CopyrightLabel;
	}
}
