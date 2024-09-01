using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace MapEditor.Forms.GridStep
{
	// Token: 0x02000113 RID: 275
	public partial class GridStepForm : Form
	{
		// Token: 0x06000DA4 RID: 3492 RVA: 0x00073005 File Offset: 0x00072005
		public GridStepForm()
		{
			this.InitializeComponent();
		}

		// Token: 0x1400003B RID: 59
		// (add) Token: 0x06000DA5 RID: 3493 RVA: 0x00073013 File Offset: 0x00072013
		// (remove) Token: 0x06000DA6 RID: 3494 RVA: 0x0007302C File Offset: 0x0007202C
		public event GridStepForm.GridStepIndexChangedEvent GridStepIndexChanged;

		// Token: 0x06000DA7 RID: 3495 RVA: 0x00073045 File Offset: 0x00072045
		private void GridStepTrackBar_Scroll(object sender, EventArgs e)
		{
			if (this.GridStepIndexChanged != null)
			{
				this.GridStepIndexChanged(this.GridStepTrackBar.Value);
			}
		}

		// Token: 0x06000DA8 RID: 3496 RVA: 0x00073065 File Offset: 0x00072065
		public void SetGridStepIndex(int gridStepIndex)
		{
			this.GridStepTrackBar.Value = gridStepIndex;
		}

		// Token: 0x06000DA9 RID: 3497 RVA: 0x00073073 File Offset: 0x00072073
		private void GridStepForm_Deactivate(object sender, EventArgs e)
		{
			if (base.Visible)
			{
				base.Hide();
			}
		}

		// Token: 0x02000114 RID: 276
		// (Invoke) Token: 0x06000DAB RID: 3499
		public delegate void GridStepIndexChangedEvent(int index);
	}
}
