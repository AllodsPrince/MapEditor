using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using InputState;

namespace MapEditor.Forms.PropertyControl.ExtendedPropertyControl
{
	// Token: 0x020000A2 RID: 162
	public class ExtendedPropertyControl : PropertyControl
	{
		// Token: 0x06000780 RID: 1920 RVA: 0x0003A158 File Offset: 0x00039158
		private void OnMenuItemClicked(object sender, EventArgs e)
		{
			ToolStripButton button = sender as ToolStripButton;
			if (this.stateContainer != null && button != null)
			{
				string label = button.Tag as string;
				MethodArgs args = new MethodArgs(null, base.SelectedObject, null);
				if (!string.IsNullOrEmpty(label))
				{
					this.stateContainer.Invoke(label, args);
				}
			}
		}

		// Token: 0x06000781 RID: 1921 RVA: 0x0003A1A7 File Offset: 0x000391A7
		public ExtendedPropertyControl()
		{
			this.InitializeComponent();
		}

		// Token: 0x17000127 RID: 295
		// (set) Token: 0x06000782 RID: 1922 RVA: 0x0003A1B5 File Offset: 0x000391B5
		public StateContainer StateContainer
		{
			set
			{
				this.stateContainer = value;
			}
		}

		// Token: 0x06000783 RID: 1923 RVA: 0x0003A1C0 File Offset: 0x000391C0
		public ToolStripButton AddButton(string label, string text, Image icon)
		{
			ToolStripButton item = new ToolStripButton(icon);
			item.ToolTipText = text;
			item.Tag = label;
			item.Click += this.OnMenuItemClicked;
			base.AddMenuItem(item);
			return item;
		}

		// Token: 0x06000784 RID: 1924 RVA: 0x0003A1FC File Offset: 0x000391FC
		protected override void Dispose(bool disposing)
		{
			if (disposing && this.components != null)
			{
				this.components.Dispose();
			}
			base.Dispose(disposing);
		}

		// Token: 0x06000785 RID: 1925 RVA: 0x0003A21C File Offset: 0x0003921C
		private void InitializeComponent()
		{
			base.SuspendLayout();
			base.AutoScaleDimensions = new SizeF(6f, 13f);
			base.AutoScaleMode = AutoScaleMode.Font;
			base.Name = "ExtendedPropertyControl";
			base.SelectedObjects = new object[0];
			base.ResumeLayout(false);
			base.PerformLayout();
		}

		// Token: 0x04000570 RID: 1392
		private StateContainer stateContainer;

		// Token: 0x04000571 RID: 1393
		private IContainer components;
	}
}
