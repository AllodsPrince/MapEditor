namespace MapEditor.Forms.PropertyControl
{
	// Token: 0x02000110 RID: 272
	public partial class PropertyControlForm : global::MapEditor.Forms.Base.BaseForm
	{
		// Token: 0x06000D48 RID: 3400 RVA: 0x0006F380 File Offset: 0x0006E380
		protected override void Dispose(bool disposing)
		{
			if (disposing && this.components != null)
			{
				this.components.Dispose();
			}
			base.Dispose(disposing);
		}

		// Token: 0x06000D49 RID: 3401 RVA: 0x0006F3A0 File Offset: 0x0006E3A0
		private void InitializeComponent()
		{
			global::System.ComponentModel.ComponentResourceManager resources = new global::System.ComponentModel.ComponentResourceManager(typeof(global::MapEditor.Forms.PropertyControl.PropertyControlForm));
			this.propertyControl = new global::MapEditor.Forms.PropertyControl.ExtendedPropertyControl.ExtendedPropertyControl();
			this.errorsButton = new global::System.Windows.Forms.Button();
			this.CloseButton = new global::System.Windows.Forms.Button();
			base.SuspendLayout();
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
			this.propertyControl.DragDrop += new global::System.Windows.Forms.DragEventHandler(this.propertyControl_DragDrop);
			this.propertyControl.DragEnter += new global::System.Windows.Forms.DragEventHandler(this.propertyControl_DragEnter);
			resources.ApplyResources(this.errorsButton, "errorsButton");
			this.errorsButton.Name = "errorsButton";
			this.errorsButton.Text = "Errors";
			this.errorsButton.UseVisualStyleBackColor = true;
			resources.ApplyResources(this.CloseButton, "CloseButton");
			this.CloseButton.DialogResult = global::System.Windows.Forms.DialogResult.Cancel;
			this.CloseButton.ImeMode = global::System.Windows.Forms.ImeMode.NoControl;
			this.CloseButton.Name = "CloseButton";
			this.CloseButton.Text = "Close";
			this.CloseButton.UseVisualStyleBackColor = true;
			this.CloseButton.Click += new global::System.EventHandler(this.CloseButton_Click);
			resources.ApplyResources(this, "$this");
			base.AutoScaleMode = global::System.Windows.Forms.AutoScaleMode.Font;
			base.CancelButton = this.CloseButton;
			base.Controls.Add(this.CloseButton);
			base.Controls.Add(this.errorsButton);
			base.Controls.Add(this.propertyControl);
			base.Name = "PropertyControlForm";
			this.Text = "Database Browser";
			base.ResumeLayout(false);
		}

		// Token: 0x04000AA5 RID: 2725
		private global::System.ComponentModel.IContainer components;

		// Token: 0x04000AA6 RID: 2726
		private global::MapEditor.Forms.PropertyControl.ExtendedPropertyControl.ExtendedPropertyControl propertyControl;

		// Token: 0x04000AA7 RID: 2727
		private global::System.Windows.Forms.Button errorsButton;

		// Token: 0x04000AA8 RID: 2728
		private global::System.Windows.Forms.Button CloseButton;
	}
}
