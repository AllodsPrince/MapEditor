using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace MapEditor.Forms.Notification
{
	// Token: 0x0200018B RID: 395
	public partial class NotificationForm : Form
	{
		// Token: 0x060012E0 RID: 4832 RVA: 0x00089D70 File Offset: 0x00088D70
		public NotificationForm()
		{
			this.InitializeComponent();
			this.lifeTime.Interval = 2000;
			this.lifeTime.Tick += this.LifeTime_Tick;
			this.lifeTime.Start();
		}

		// Token: 0x060012E1 RID: 4833 RVA: 0x00089DC6 File Offset: 0x00088DC6
		private void LifeTime_Tick(object sender, EventArgs e)
		{
			base.Close();
		}

		// Token: 0x04000D82 RID: 3458
		private readonly Timer lifeTime = new Timer();
	}
}
