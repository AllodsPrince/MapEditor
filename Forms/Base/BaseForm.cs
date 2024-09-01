using System;
using Tools.BaseForm;

namespace MapEditor.Forms.Base
{
	// Token: 0x02000011 RID: 17
	public partial class BaseForm : BaseForm
	{
		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000057 RID: 87 RVA: 0x00003F5D File Offset: 0x00002F5D
		public MainForm.Context Context
		{
			get
			{
				return this.context;
			}
		}

		// Token: 0x06000058 RID: 88 RVA: 0x00003F65 File Offset: 0x00002F65
		public BaseForm()
		{
		}

		// Token: 0x06000059 RID: 89 RVA: 0x00003F6D File Offset: 0x00002F6D
		public BaseForm(string paramsFileName, MainForm.Context _context) : base(paramsFileName)
		{
			this.context = _context;
		}

		// Token: 0x0400001A RID: 26
		private readonly MainForm.Context context;
	}
}
