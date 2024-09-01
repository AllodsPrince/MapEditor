namespace MapEditor.Forms.Property
{
	// Token: 0x02000081 RID: 129
	public partial class PropertiesForm : global::MapEditor.Forms.Base.BaseForm
	{
		// Token: 0x0600062E RID: 1582 RVA: 0x000346F0 File Offset: 0x000336F0
		protected override void Dispose(bool disposing)
		{
			if (disposing && this.components != null)
			{
				this.components.Dispose();
			}
			base.Dispose(disposing);
		}

		// Token: 0x0600062F RID: 1583 RVA: 0x00034710 File Offset: 0x00033710
		private void InitializeComponent()
		{
			global::System.ComponentModel.ComponentResourceManager resources = new global::System.ComponentModel.ComponentResourceManager(typeof(global::MapEditor.Forms.Property.PropertiesForm));
			this.propertyControl = new global::System.Windows.Forms.PropertyGrid();
			base.SuspendLayout();
			this.propertyControl.AccessibleDescription = null;
			this.propertyControl.AccessibleName = null;
			resources.ApplyResources(this.propertyControl, "propertyControl");
			this.propertyControl.BackgroundImage = null;
			this.propertyControl.Font = null;
			this.propertyControl.Name = "propertyControl";
			this.propertyControl.SelectedObject = null;
			this.propertyControl.SelectedObjects = new object[0];
			base.AccessibleDescription = null;
			base.AccessibleName = null;
			resources.ApplyResources(this, "$this");
			base.AutoScaleMode = global::System.Windows.Forms.AutoScaleMode.Font;
			this.BackgroundImage = null;
			base.Controls.Add(this.propertyControl);
			this.Font = null;
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.Name = "PropertiesForm";
			base.ShowInTaskbar = false;
			base.ResumeLayout(false);
		}

		// Token: 0x040004AD RID: 1197
		private global::System.ComponentModel.IContainer components;

		// Token: 0x040004AE RID: 1198
		private global::System.Windows.Forms.PropertyGrid propertyControl;
	}
}
