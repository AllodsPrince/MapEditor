namespace MapEditor.Forms.Quests.QuickObjectGenerator.DialogForms
{
	// Token: 0x0200004E RID: 78
	public partial class SelectIconDialogForm : global::System.Windows.Forms.Form
	{
		// Token: 0x06000421 RID: 1057 RVA: 0x000225FE File Offset: 0x000215FE
		protected override void Dispose(bool disposing)
		{
			if (disposing && this.components != null)
			{
				this.components.Dispose();
			}
			base.Dispose(disposing);
		}

		// Token: 0x06000422 RID: 1058 RVA: 0x00022620 File Offset: 0x00021620
		private void InitializeComponent()
		{
			this.components = new global::System.ComponentModel.Container();
			global::System.ComponentModel.ComponentResourceManager resources = new global::System.ComponentModel.ComponentResourceManager(typeof(global::MapEditor.Forms.Quests.QuickObjectGenerator.DialogForms.SelectIconDialogForm));
			this.IconImageList = new global::System.Windows.Forms.ImageList(this.components);
			this.listView = new global::System.Windows.Forms.ListView();
			this.OkFormButton = new global::System.Windows.Forms.Button();
			this.CancelFormButton = new global::System.Windows.Forms.Button();
			this.filterTextBox = new global::System.Windows.Forms.TextBox();
			this.findButton = new global::System.Windows.Forms.Button();
			base.SuspendLayout();
			this.IconImageList.ColorDepth = global::System.Windows.Forms.ColorDepth.Depth32Bit;
			resources.ApplyResources(this.IconImageList, "IconImageList");
			this.IconImageList.TransparentColor = global::System.Drawing.Color.Transparent;
			resources.ApplyResources(this.listView, "listView");
			this.listView.LargeImageList = this.IconImageList;
			this.listView.Name = "listView";
			this.listView.UseCompatibleStateImageBehavior = false;
			resources.ApplyResources(this.OkFormButton, "OkFormButton");
			this.OkFormButton.DialogResult = global::System.Windows.Forms.DialogResult.OK;
			this.OkFormButton.Name = "OkFormButton";
			this.OkFormButton.Text = "OK";
			this.OkFormButton.UseVisualStyleBackColor = true;
			resources.ApplyResources(this.CancelFormButton, "CancelFormButton");
			this.CancelFormButton.DialogResult = global::System.Windows.Forms.DialogResult.Cancel;
			this.CancelFormButton.Name = "CancelFormButton";
			this.CancelFormButton.Text = "Cancel";
			this.CancelFormButton.UseVisualStyleBackColor = true;
			resources.ApplyResources(this.filterTextBox, "filterTextBox");
			this.filterTextBox.Name = "filterTextBox";
			resources.ApplyResources(this.findButton, "findButton");
			this.findButton.Name = "findButton";
			this.findButton.Text = "Find";
			this.findButton.UseVisualStyleBackColor = true;
			resources.ApplyResources(this, "$this");
			base.AutoScaleMode = global::System.Windows.Forms.AutoScaleMode.Font;
			base.CancelButton = this.CancelFormButton;
			base.Controls.Add(this.findButton);
			base.Controls.Add(this.filterTextBox);
			base.Controls.Add(this.CancelFormButton);
			base.Controls.Add(this.OkFormButton);
			base.Controls.Add(this.listView);
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.Name = "SelectIconDialogForm";
			base.ShowInTaskbar = false;
			this.Text = "Choose Icon";
			base.ResumeLayout(false);
			base.PerformLayout();
		}

		// Token: 0x040002DB RID: 731
		private global::System.ComponentModel.IContainer components;

		// Token: 0x040002DC RID: 732
		private global::System.Windows.Forms.ImageList IconImageList;

		// Token: 0x040002DD RID: 733
		private global::System.Windows.Forms.ListView listView;

		// Token: 0x040002DE RID: 734
		private global::System.Windows.Forms.Button OkFormButton;

		// Token: 0x040002DF RID: 735
		private global::System.Windows.Forms.Button CancelFormButton;

		// Token: 0x040002E0 RID: 736
		private global::System.Windows.Forms.TextBox filterTextBox;

		// Token: 0x040002E1 RID: 737
		private global::System.Windows.Forms.Button findButton;
	}
}
