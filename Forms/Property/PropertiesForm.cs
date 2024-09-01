using System;
using System.ComponentModel;
using System.Windows.Forms;
using MapEditor.Forms.Base;

namespace MapEditor.Forms.Property
{
	// Token: 0x02000081 RID: 129
	public partial class PropertiesForm : BaseForm
	{
		// Token: 0x06000629 RID: 1577 RVA: 0x00034635 File Offset: 0x00033635
		private void OnVisibleChanged(object senter, EventArgs eventArgs)
		{
			if (base.Visible)
			{
				this.propertyControl.SelectedObject = this.selectedObject;
			}
		}

		// Token: 0x0600062A RID: 1578 RVA: 0x00034650 File Offset: 0x00033650
		public PropertiesForm(MainForm.Context context) : base(EditorEnvironment.EditorFormsFolder + "PropertiesForm.xml", context)
		{
			this.InitializeComponent();
			base.VisibleChanged += this.OnVisibleChanged;
		}

		// Token: 0x170000CD RID: 205
		// (get) Token: 0x0600062B RID: 1579 RVA: 0x00034680 File Offset: 0x00033680
		// (set) Token: 0x0600062C RID: 1580 RVA: 0x00034688 File Offset: 0x00033688
		public object SelectedObject
		{
			get
			{
				return this.selectedObject;
			}
			set
			{
				this.selectedObject = value;
				if (this.selectedObject == null || base.Visible)
				{
					this.propertyControl.SelectedObject = this.selectedObject;
				}
			}
		}

		// Token: 0x0600062D RID: 1581 RVA: 0x000346B4 File Offset: 0x000336B4
		public void UpdatePropertyControl(bool reloadObject)
		{
			if (base.Visible)
			{
				if (reloadObject)
				{
					object _object = this.propertyControl.SelectedObject;
					this.propertyControl.SelectedObject = _object;
					return;
				}
				this.propertyControl.Update();
			}
		}

		// Token: 0x040004AC RID: 1196
		private object selectedObject;
	}
}
