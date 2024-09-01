using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace MapEditor.Forms.CameraFOV
{
	// Token: 0x020000F7 RID: 247
	public partial class CameraFOVForm : Form
	{
		// Token: 0x14000038 RID: 56
		// (add) Token: 0x06000C3B RID: 3131 RVA: 0x00069739 File Offset: 0x00068739
		// (remove) Token: 0x06000C3C RID: 3132 RVA: 0x00069752 File Offset: 0x00068752
		public event CameraFOVForm.FovChangedEvent FovChanged;

		// Token: 0x06000C3D RID: 3133 RVA: 0x0006976C File Offset: 0x0006876C
		private void CameraFOVTrackBar_Scroll(object sender, EventArgs e)
		{
			double fov = (double)this.CameraFOVTrackBar.Value * 3.141592653589793 / 18.0;
			if (this.FovChanged != null)
			{
				this.FovChanged(fov);
			}
		}

		// Token: 0x06000C3E RID: 3134 RVA: 0x000697AE File Offset: 0x000687AE
		public CameraFOVForm()
		{
			this.InitializeComponent();
		}

		// Token: 0x06000C3F RID: 3135 RVA: 0x000697BC File Offset: 0x000687BC
		public void SetFOV(double fov)
		{
			this.CameraFOVTrackBar.Value = (int)(fov / 3.141592653589793 * 18.0);
		}

		// Token: 0x06000C40 RID: 3136 RVA: 0x000697DF File Offset: 0x000687DF
		private void CameraFOVForm_Deactivate(object sender, EventArgs e)
		{
			if (base.Visible)
			{
				base.Hide();
			}
		}

		// Token: 0x020000F8 RID: 248
		// (Invoke) Token: 0x06000C44 RID: 3140
		public delegate void FovChangedEvent(double fov);
	}
}
