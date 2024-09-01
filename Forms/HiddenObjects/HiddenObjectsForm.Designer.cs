namespace MapEditor.Forms.HiddenObjects
{
	// Token: 0x020001B8 RID: 440
	public partial class HiddenObjectsForm : global::MapEditor.Forms.Base.BaseForm
	{
		// Token: 0x06001557 RID: 5463 RVA: 0x0009AE4C File Offset: 0x00099E4C
		protected override void Dispose(bool disposing)
		{
			if (disposing && this.components != null)
			{
				this.components.Dispose();
			}
			base.Dispose(disposing);
		}

		// Token: 0x06001558 RID: 5464 RVA: 0x0009AE6C File Offset: 0x00099E6C
		private void InitializeComponent()
		{
			this.addButton = new global::System.Windows.Forms.Button();
			this.hiddenObjectsListBox = new global::System.Windows.Forms.ListBox();
			this.removeButton = new global::System.Windows.Forms.Button();
			base.SuspendLayout();
			this.addButton.Location = new global::System.Drawing.Point(3, 3);
			this.addButton.Name = "addButton";
			this.addButton.Size = new global::System.Drawing.Size(74, 22);
			this.addButton.TabIndex = 1;
			this.addButton.Text = "Hide object";
			this.addButton.UseVisualStyleBackColor = true;
			this.hiddenObjectsListBox.Anchor = (global::System.Windows.Forms.AnchorStyles.Top | global::System.Windows.Forms.AnchorStyles.Bottom | global::System.Windows.Forms.AnchorStyles.Left | global::System.Windows.Forms.AnchorStyles.Right);
			this.hiddenObjectsListBox.FormattingEnabled = true;
			this.hiddenObjectsListBox.Location = new global::System.Drawing.Point(3, 28);
			this.hiddenObjectsListBox.Name = "hiddenObjectsListBox";
			this.hiddenObjectsListBox.SelectionMode = global::System.Windows.Forms.SelectionMode.MultiExtended;
			this.hiddenObjectsListBox.Size = new global::System.Drawing.Size(286, 238);
			this.hiddenObjectsListBox.TabIndex = 0;
			this.removeButton.Enabled = false;
			this.removeButton.Location = new global::System.Drawing.Point(83, 3);
			this.removeButton.Name = "removeButton";
			this.removeButton.Size = new global::System.Drawing.Size(81, 22);
			this.removeButton.TabIndex = 2;
			this.removeButton.Text = "Show object";
			this.removeButton.UseVisualStyleBackColor = true;
			base.AutoScaleDimensions = new global::System.Drawing.SizeF(6f, 13f);
			base.AutoScaleMode = global::System.Windows.Forms.AutoScaleMode.Font;
			base.ClientSize = new global::System.Drawing.Size(292, 268);
			base.Controls.Add(this.removeButton);
			base.Controls.Add(this.addButton);
			base.Controls.Add(this.hiddenObjectsListBox);
			base.Name = "HiddenObjectsForm";
			this.Text = "Hidden Objects";
			base.ResumeLayout(false);
		}

		// Token: 0x04000F0D RID: 3853
		private global::System.ComponentModel.IContainer components;

		// Token: 0x04000F0E RID: 3854
		private global::System.Windows.Forms.Button addButton;

		// Token: 0x04000F0F RID: 3855
		private global::System.Windows.Forms.ListBox hiddenObjectsListBox;

		// Token: 0x04000F10 RID: 3856
		private global::System.Windows.Forms.Button removeButton;
	}
}
