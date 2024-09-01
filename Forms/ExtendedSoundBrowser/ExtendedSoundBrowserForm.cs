using System;
using System.ComponentModel;
using System.Windows.Forms;
using MapEditor.Map.MapObjects;
using Tools.WindowParams;

namespace MapEditor.Forms.ExtendedSoundBrowser
{
	// Token: 0x0200007C RID: 124
	public partial class ExtendedSoundBrowserForm : Form
	{
		// Token: 0x06000603 RID: 1539 RVA: 0x00033270 File Offset: 0x00032270
		public ExtendedSoundBrowserForm(string configFileName)
		{
			this.InitializeComponent();
			this.paramsSaver = new FormParamsSaver(this, configFileName, false);
			this.paramsSaver.AutoregisterControls = false;
		}

		// Token: 0x170000CC RID: 204
		// (get) Token: 0x06000604 RID: 1540 RVA: 0x00033298 File Offset: 0x00032298
		// (set) Token: 0x06000605 RID: 1541 RVA: 0x000332AA File Offset: 0x000322AA
		public ExtendedSoundParams SelectedObject
		{
			get
			{
				return (ExtendedSoundParams)this.PropertyControl.SelectedObject;
			}
			set
			{
				this.PropertyControl.SelectedObject = value;
			}
		}

		// Token: 0x04000492 RID: 1170
		private readonly FormParamsSaver paramsSaver;
	}
}
