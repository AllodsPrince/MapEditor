using System;
using System.Drawing;

namespace MapEditor.Forms.Groups
{
	// Token: 0x0200018D RID: 397
	internal class NodeColorInfo
	{
		// Token: 0x06001301 RID: 4865 RVA: 0x0008C8B8 File Offset: 0x0008B8B8
		public NodeColorInfo(Color _foreColor)
		{
			this.foreColor = _foreColor;
		}

		// Token: 0x170003C2 RID: 962
		// (get) Token: 0x06001302 RID: 4866 RVA: 0x0008C8D2 File Offset: 0x0008B8D2
		public Color ForeColor
		{
			get
			{
				return this.foreColor;
			}
		}

		// Token: 0x04000DD4 RID: 3540
		private readonly Color foreColor = SystemColors.ControlText;
	}
}
