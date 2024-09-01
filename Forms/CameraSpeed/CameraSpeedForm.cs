using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace MapEditor.Forms.CameraSpeed
{
	// Token: 0x02000116 RID: 278
	public partial class CameraSpeedForm : Form
	{
		// Token: 0x1400003C RID: 60
		// (add) Token: 0x06000DB5 RID: 3509 RVA: 0x0007321D File Offset: 0x0007221D
		// (remove) Token: 0x06000DB6 RID: 3510 RVA: 0x00073236 File Offset: 0x00072236
		public event CameraSpeedForm.SpeedChangedEvent SpeedChanged;

		// Token: 0x06000DB7 RID: 3511 RVA: 0x00073250 File Offset: 0x00072250
		private void CameraSpeedTrackBar_Scroll(object sender, EventArgs e)
		{
			double speed = (double)this.CameraSpeedTrackBar.Value * 1.0;
			if (speed < 1.0)
			{
				speed = 1.0;
			}
			else if (speed > 250.0)
			{
				speed = 250.0;
			}
			if (this.SpeedChanged != null)
			{
				this.SpeedChanged(speed);
			}
		}

		// Token: 0x06000DB8 RID: 3512 RVA: 0x000732B6 File Offset: 0x000722B6
		public CameraSpeedForm()
		{
			this.InitializeComponent();
		}

		// Token: 0x06000DB9 RID: 3513 RVA: 0x000732C4 File Offset: 0x000722C4
		public void SetSpeed(double speed)
		{
			if (speed < 1.0)
			{
				speed = 1.0;
			}
			else if (speed > 250.0)
			{
				speed = 250.0;
			}
			this.CameraSpeedTrackBar.Value = (int)speed;
		}

		// Token: 0x06000DBA RID: 3514 RVA: 0x00073303 File Offset: 0x00072303
		private void CameraSpeedForm_Deactivate(object sender, EventArgs e)
		{
			if (base.Visible)
			{
				base.Hide();
			}
		}

		// Token: 0x02000117 RID: 279
		// (Invoke) Token: 0x06000DBE RID: 3518
		public delegate void SpeedChangedEvent(double speed);
	}
}
