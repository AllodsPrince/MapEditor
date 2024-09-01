using System;
using CommandLine;

namespace MapEditor
{
	// Token: 0x0200001F RID: 31
	public class GUIParams : Params
	{
		// Token: 0x1700002C RID: 44
		// (get) Token: 0x0600029C RID: 668 RVA: 0x0001D0F0 File Offset: 0x0001C0F0
		// (set) Token: 0x0600029D RID: 669 RVA: 0x0001D0F8 File Offset: 0x0001C0F8
		[Param("mode", "map editor GUI mode")]
		public GUIParams.MapEditorMode Mode
		{
			get
			{
				return this.mode;
			}
			set
			{
				this.mode = value;
			}
		}

		// Token: 0x1700002D RID: 45
		// (get) Token: 0x0600029E RID: 670 RVA: 0x0001D101 File Offset: 0x0001C101
		// (set) Token: 0x0600029F RID: 671 RVA: 0x0001D109 File Offset: 0x0001C109
		[Param("int", "int parameter")]
		public int IntParam
		{
			get
			{
				return this.intParam;
			}
			set
			{
				this.intParam = value;
			}
		}

		// Token: 0x1700002E RID: 46
		// (get) Token: 0x060002A0 RID: 672 RVA: 0x0001D112 File Offset: 0x0001C112
		// (set) Token: 0x060002A1 RID: 673 RVA: 0x0001D11A File Offset: 0x0001C11A
		[Param("string", "string parameter")]
		public string StringParam
		{
			get
			{
				return this.stringParam;
			}
			set
			{
				this.stringParam = value;
			}
		}

		// Token: 0x060002A2 RID: 674 RVA: 0x0001D123 File Offset: 0x0001C123
		public bool IsValid()
		{
			return true;
		}

		// Token: 0x060002A3 RID: 675 RVA: 0x0001D126 File Offset: 0x0001C126
		public void ShowHelp(Parser parser)
		{
		}

		// Token: 0x1700002F RID: 47
		// (get) Token: 0x060002A4 RID: 676 RVA: 0x0001D128 File Offset: 0x0001C128
		// (set) Token: 0x060002A5 RID: 677 RVA: 0x0001D130 File Offset: 0x0001C130
		public string CommandLine
		{
			get
			{
				return this.commandLine;
			}
			set
			{
				this.commandLine = value;
			}
		}

		// Token: 0x04000239 RID: 569
		private GUIParams.MapEditorMode mode;

		// Token: 0x0400023A RID: 570
		private int intParam = 1;

		// Token: 0x0400023B RID: 571
		private string stringParam = string.Empty;

		// Token: 0x0400023C RID: 572
		private string commandLine = string.Empty;

		// Token: 0x02000020 RID: 32
		public enum MapEditorMode
		{
			// Token: 0x0400023E RID: 574
			Unknown,
			// Token: 0x0400023F RID: 575
			Mode1,
			// Token: 0x04000240 RID: 576
			Mode2,
			// Token: 0x04000241 RID: 577
			Mode3
		}
	}
}
