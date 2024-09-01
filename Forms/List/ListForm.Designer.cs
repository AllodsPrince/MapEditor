namespace MapEditor.Forms.List
{
	// Token: 0x020001B7 RID: 439
	public partial class ListForm : global::MapEditor.Forms.Base.BaseForm
	{
		// Token: 0x06001548 RID: 5448 RVA: 0x0009A941 File Offset: 0x00099941
		protected override void Dispose(bool disposing)
		{
			if (disposing && this.components != null)
			{
				this.components.Dispose();
			}
			base.Dispose(disposing);
		}

		// Token: 0x06001549 RID: 5449 RVA: 0x0009A960 File Offset: 0x00099960
		private void InitializeComponent()
		{
			global::System.ComponentModel.ComponentResourceManager resources = new global::System.ComponentModel.ComponentResourceManager(typeof(global::MapEditor.Forms.List.ListForm));
			base.SuspendLayout();
			resources.ApplyResources(this, "$this");
			base.AutoScaleMode = global::System.Windows.Forms.AutoScaleMode.Font;
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.Name = "ListForm";
			base.ShowInTaskbar = false;
			base.ResumeLayout(false);
		}

		// Token: 0x04000F0B RID: 3851
		private global::System.ComponentModel.IContainer components;
	}
}
