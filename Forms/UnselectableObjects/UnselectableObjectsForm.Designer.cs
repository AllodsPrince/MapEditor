namespace MapEditor.Forms.UnselectableObjects
{
	// Token: 0x0200001B RID: 27
	public partial class UnselectableObjectsForm : global::MapEditor.Forms.Base.BaseForm
	{
		// Token: 0x0600024A RID: 586 RVA: 0x00019ADC File Offset: 0x00018ADC
		protected override void Dispose(bool disposing)
		{
			if (disposing && this.components != null)
			{
				this.components.Dispose();
			}
			base.Dispose(disposing);
		}

		// Token: 0x0600024B RID: 587 RVA: 0x00019AFC File Offset: 0x00018AFC
		private void InitializeComponent()
		{
			global::System.ComponentModel.ComponentResourceManager resources = new global::System.ComponentModel.ComponentResourceManager(typeof(global::MapEditor.Forms.UnselectableObjects.UnselectableObjectsForm));
			this.objectsListBox = new global::System.Windows.Forms.ListBox();
			this.deleteButton = new global::System.Windows.Forms.Button();
			base.SuspendLayout();
			resources.ApplyResources(this.objectsListBox, "objectsListBox");
			this.objectsListBox.FormattingEnabled = true;
			this.objectsListBox.Name = "objectsListBox";
			this.objectsListBox.SelectionMode = global::System.Windows.Forms.SelectionMode.MultiExtended;
			this.objectsListBox.Sorted = true;
			resources.ApplyResources(this.deleteButton, "deleteButton");
			this.deleteButton.Name = "deleteButton";
			this.deleteButton.UseVisualStyleBackColor = true;
			resources.ApplyResources(this, "$this");
			base.AutoScaleMode = global::System.Windows.Forms.AutoScaleMode.Font;
			base.Controls.Add(this.deleteButton);
			base.Controls.Add(this.objectsListBox);
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.Name = "UnselectableObjectsForm";
			base.ShowInTaskbar = false;
			base.ResumeLayout(false);
		}

		// Token: 0x04000214 RID: 532
		private global::System.ComponentModel.IContainer components;

		// Token: 0x04000215 RID: 533
		private global::System.Windows.Forms.ListBox objectsListBox;

		// Token: 0x04000216 RID: 534
		private global::System.Windows.Forms.Button deleteButton;
	}
}
