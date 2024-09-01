using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace MapEditor.Forms.About
{
	// Token: 0x02000118 RID: 280
	public partial class AboutForm : Form
	{
		// Token: 0x06000DC3 RID: 3523 RVA: 0x00073704 File Offset: 0x00072704
		public AboutForm(string version)
		{
			this.InitializeComponent();
			this.VersionLabel.Text = string.Format("{0} {1}", this.VersionLabel.Text, version);
		}
	}
}
