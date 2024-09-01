namespace MapEditor.Forms.CollectionBrowser
{
	// Token: 0x02000070 RID: 112
	public partial class CollectionBrowser : global::System.Windows.Forms.Form
	{
		// Token: 0x06000587 RID: 1415 RVA: 0x0002D999 File Offset: 0x0002C999
		protected override void Dispose(bool disposing)
		{
			if (disposing && this.components != null)
			{
				this.components.Dispose();
			}
			base.Dispose(disposing);
		}

		// Token: 0x06000588 RID: 1416 RVA: 0x0002D9B8 File Offset: 0x0002C9B8
		private void InitializeComponent()
		{
			global::System.ComponentModel.ComponentResourceManager resources = new global::System.ComponentModel.ComponentResourceManager(typeof(global::MapEditor.Forms.CollectionBrowser.CollectionBrowser));
			this.SplitContainer = new global::System.Windows.Forms.SplitContainer();
			this.TypeComboBox = new global::System.Windows.Forms.ComboBox();
			this.CollectionLabel = new global::System.Windows.Forms.Label();
			this.CollectionListView = new global::System.Windows.Forms.ListView();
			this.DownButton = new global::System.Windows.Forms.Button();
			this.UpButton = new global::System.Windows.Forms.Button();
			this.AddButton = new global::System.Windows.Forms.Button();
			this.RemoveButton = new global::System.Windows.Forms.Button();
			this.cancelButton = new global::System.Windows.Forms.Button();
			this.okButton = new global::System.Windows.Forms.Button();
			this.ItemLabel = new global::System.Windows.Forms.Label();
			this.ItemPropertyGrid = new global::System.Windows.Forms.PropertyGrid();
			this.SplitContainer.Panel1.SuspendLayout();
			this.SplitContainer.Panel2.SuspendLayout();
			this.SplitContainer.SuspendLayout();
			base.SuspendLayout();
			resources.ApplyResources(this.SplitContainer, "SplitContainer");
			this.SplitContainer.Name = "SplitContainer";
			this.SplitContainer.Panel1.Controls.Add(this.TypeComboBox);
			this.SplitContainer.Panel1.Controls.Add(this.CollectionLabel);
			this.SplitContainer.Panel1.Controls.Add(this.CollectionListView);
			this.SplitContainer.Panel1.Controls.Add(this.DownButton);
			this.SplitContainer.Panel1.Controls.Add(this.UpButton);
			this.SplitContainer.Panel1.Controls.Add(this.AddButton);
			this.SplitContainer.Panel1.Controls.Add(this.RemoveButton);
			this.SplitContainer.Panel2.Controls.Add(this.cancelButton);
			this.SplitContainer.Panel2.Controls.Add(this.okButton);
			this.SplitContainer.Panel2.Controls.Add(this.ItemLabel);
			this.SplitContainer.Panel2.Controls.Add(this.ItemPropertyGrid);
			resources.ApplyResources(this.TypeComboBox, "TypeComboBox");
			this.TypeComboBox.DropDownStyle = global::System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.TypeComboBox.FormattingEnabled = true;
			this.TypeComboBox.Name = "TypeComboBox";
			this.TypeComboBox.Sorted = true;
			resources.ApplyResources(this.CollectionLabel, "CollectionLabel");
			this.CollectionLabel.Name = "CollectionLabel";
			resources.ApplyResources(this.CollectionListView, "CollectionListView");
			this.CollectionListView.HideSelection = false;
			this.CollectionListView.MultiSelect = false;
			this.CollectionListView.Name = "CollectionListView";
			this.CollectionListView.ShowItemToolTips = true;
			this.CollectionListView.UseCompatibleStateImageBehavior = false;
			this.CollectionListView.View = global::System.Windows.Forms.View.List;
			this.CollectionListView.ItemSelectionChanged += new global::System.Windows.Forms.ListViewItemSelectionChangedEventHandler(this.CollectionListView_ItemSelectionChanged);
			this.CollectionListView.KeyDown += new global::System.Windows.Forms.KeyEventHandler(this.CollectionListView_KeyDown);
			resources.ApplyResources(this.DownButton, "DownButton");
			this.DownButton.Image = global::MapEditor.Properties.Resources.dialogs_down;
			this.DownButton.Name = "DownButton";
			this.DownButton.UseVisualStyleBackColor = true;
			this.DownButton.Click += new global::System.EventHandler(this.DownButton_Click);
			resources.ApplyResources(this.UpButton, "UpButton");
			this.UpButton.ForeColor = global::System.Drawing.SystemColors.ControlText;
			this.UpButton.Image = global::MapEditor.Properties.Resources.dialogs_up;
			this.UpButton.Name = "UpButton";
			this.UpButton.UseVisualStyleBackColor = true;
			this.UpButton.Click += new global::System.EventHandler(this.UpButton_Click);
			resources.ApplyResources(this.AddButton, "AddButton");
			this.AddButton.Name = "AddButton";
			this.AddButton.UseVisualStyleBackColor = true;
			this.AddButton.Click += new global::System.EventHandler(this.AddButton_Click);
			resources.ApplyResources(this.RemoveButton, "RemoveButton");
			this.RemoveButton.Image = global::MapEditor.Properties.Resources.dialogs_delete;
			this.RemoveButton.Name = "RemoveButton";
			this.RemoveButton.UseVisualStyleBackColor = true;
			this.RemoveButton.Click += new global::System.EventHandler(this.RemoveButton_Click);
			resources.ApplyResources(this.cancelButton, "cancelButton");
			this.cancelButton.DialogResult = global::System.Windows.Forms.DialogResult.Cancel;
			this.cancelButton.Name = "cancelButton";
			this.cancelButton.UseVisualStyleBackColor = true;
			resources.ApplyResources(this.okButton, "okButton");
			this.okButton.DialogResult = global::System.Windows.Forms.DialogResult.OK;
			this.okButton.Name = "okButton";
			this.okButton.UseVisualStyleBackColor = true;
			resources.ApplyResources(this.ItemLabel, "ItemLabel");
			this.ItemLabel.Name = "ItemLabel";
			resources.ApplyResources(this.ItemPropertyGrid, "ItemPropertyGrid");
			this.ItemPropertyGrid.Name = "ItemPropertyGrid";
			base.AcceptButton = this.okButton;
			resources.ApplyResources(this, "$this");
			base.AutoScaleMode = global::System.Windows.Forms.AutoScaleMode.Font;
			base.CancelButton = this.cancelButton;
			base.Controls.Add(this.SplitContainer);
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.Name = "CollectionBrowser";
			base.ShowInTaskbar = false;
			base.SizeGripStyle = global::System.Windows.Forms.SizeGripStyle.Show;
			base.Load += new global::System.EventHandler(this.CollectionBrowser_Load);
			this.SplitContainer.Panel1.ResumeLayout(false);
			this.SplitContainer.Panel2.ResumeLayout(false);
			this.SplitContainer.ResumeLayout(false);
			base.ResumeLayout(false);
		}

		// Token: 0x0400042C RID: 1068
		private global::System.ComponentModel.IContainer components;

		// Token: 0x0400042D RID: 1069
		private global::System.Windows.Forms.SplitContainer SplitContainer;

		// Token: 0x0400042E RID: 1070
		private global::System.Windows.Forms.Button AddButton;

		// Token: 0x0400042F RID: 1071
		private global::System.Windows.Forms.Button RemoveButton;

		// Token: 0x04000430 RID: 1072
		private global::System.Windows.Forms.ListView CollectionListView;

		// Token: 0x04000431 RID: 1073
		private global::System.Windows.Forms.Button DownButton;

		// Token: 0x04000432 RID: 1074
		private global::System.Windows.Forms.Button UpButton;

		// Token: 0x04000433 RID: 1075
		private global::System.Windows.Forms.PropertyGrid ItemPropertyGrid;

		// Token: 0x04000434 RID: 1076
		private global::System.Windows.Forms.Label CollectionLabel;

		// Token: 0x04000435 RID: 1077
		private global::System.Windows.Forms.Label ItemLabel;

		// Token: 0x04000436 RID: 1078
		private global::System.Windows.Forms.ComboBox TypeComboBox;

		// Token: 0x04000437 RID: 1079
		private global::System.Windows.Forms.Button cancelButton;

		// Token: 0x04000438 RID: 1080
		private global::System.Windows.Forms.Button okButton;
	}
}
