using System;
using System.ComponentModel;
using System.Windows.Forms;
using MapEditor.Forms.Base;

namespace MapEditor.Forms.List
{
	// Token: 0x020001B7 RID: 439
	public partial class ListForm : BaseForm
	{
		// Token: 0x0600154A RID: 5450 RVA: 0x0009A9BD File Offset: 0x000999BD
		public ListForm(MainForm.Context context) : base(EditorEnvironment.EditorFormsFolder + "ListForm.xml", context)
		{
			this.InitializeComponent();
		}
	}
}
