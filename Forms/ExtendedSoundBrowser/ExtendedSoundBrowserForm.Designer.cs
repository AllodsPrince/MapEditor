namespace MapEditor.Forms.ExtendedSoundBrowser
{
	// Token: 0x0200007C RID: 124
	public partial class ExtendedSoundBrowserForm : global::System.Windows.Forms.Form
	{
		// Token: 0x06000606 RID: 1542 RVA: 0x000332B8 File Offset: 0x000322B8
		protected override void Dispose(bool disposing)
		{
			if (disposing && this.components != null)
			{
				this.components.Dispose();
			}
			base.Dispose(disposing);
		}

		// Token: 0x06000607 RID: 1543 RVA: 0x000332D8 File Offset: 0x000322D8
		private void InitializeComponent()
		{
			global::System.ComponentModel.ComponentResourceManager resources = new global::System.ComponentModel.ComponentResourceManager(typeof(global::MapEditor.Forms.ExtendedSoundBrowser.ExtendedSoundBrowserForm));
			this.PropertyControl = new global::System.Windows.Forms.PropertyGrid();
			this._acceptButton = new global::System.Windows.Forms.Button();
			this._cancelButton = new global::System.Windows.Forms.Button();
			base.SuspendLayout();
			resources.ApplyResources(this.PropertyControl, "PropertyControl");
			this.PropertyControl.Name = "PropertyControl";
			resources.ApplyResources(this._acceptButton, "_acceptButton");
			this._acceptButton.DialogResult = global::System.Windows.Forms.DialogResult.OK;
			this._acceptButton.Name = "_acceptButton";
			this._acceptButton.UseVisualStyleBackColor = true;
			resources.ApplyResources(this._cancelButton, "_cancelButton");
			this._cancelButton.DialogResult = global::System.Windows.Forms.DialogResult.Cancel;
			this._cancelButton.Name = "_cancelButton";
			this._cancelButton.UseVisualStyleBackColor = true;
			base.AcceptButton = this._acceptButton;
			resources.ApplyResources(this, "$this");
			base.AutoScaleMode = global::System.Windows.Forms.AutoScaleMode.Font;
			base.CancelButton = this._cancelButton;
			base.Controls.Add(this._cancelButton);
			base.Controls.Add(this._acceptButton);
			base.Controls.Add(this.PropertyControl);
			base.Name = "ExtendedSoundBrowserForm";
			base.ResumeLayout(false);
		}

		// Token: 0x04000493 RID: 1171
		private global::System.ComponentModel.IContainer components;

		// Token: 0x04000494 RID: 1172
		private global::System.Windows.Forms.PropertyGrid PropertyControl;

		// Token: 0x04000495 RID: 1173
		private global::System.Windows.Forms.Button _cancelButton;

		// Token: 0x04000496 RID: 1174
		private global::System.Windows.Forms.Button _acceptButton;
	}
}
