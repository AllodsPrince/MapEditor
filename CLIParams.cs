using System;
using CommandLine;

namespace MapEditor
{
	// Token: 0x0200026D RID: 621
	public class CLIParams : Params
	{
		// Token: 0x170006F0 RID: 1776
		// (get) Token: 0x06001D6C RID: 7532 RVA: 0x000BC58B File Offset: 0x000BB58B
		// (set) Token: 0x06001D6D RID: 7533 RVA: 0x000BC593 File Offset: 0x000BB593
		[Param("mode", "map editor CLI mode")]
		public CLIParams.MapEditorMode Mode
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

		// Token: 0x170006F1 RID: 1777
		// (get) Token: 0x06001D6E RID: 7534 RVA: 0x000BC59C File Offset: 0x000BB59C
		// (set) Token: 0x06001D6F RID: 7535 RVA: 0x000BC5A4 File Offset: 0x000BB5A4
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

		// Token: 0x170006F2 RID: 1778
		// (get) Token: 0x06001D70 RID: 7536 RVA: 0x000BC5AD File Offset: 0x000BB5AD
		// (set) Token: 0x06001D71 RID: 7537 RVA: 0x000BC5B5 File Offset: 0x000BB5B5
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

		// Token: 0x06001D72 RID: 7538 RVA: 0x000BC5BE File Offset: 0x000BB5BE
		public bool IsValid()
		{
			return true;
		}

		// Token: 0x06001D73 RID: 7539 RVA: 0x000BC5C1 File Offset: 0x000BB5C1
		public void ShowHelp(Parser parser)
		{
		}

		// Token: 0x170006F3 RID: 1779
		// (get) Token: 0x06001D74 RID: 7540 RVA: 0x000BC5C3 File Offset: 0x000BB5C3
		// (set) Token: 0x06001D75 RID: 7541 RVA: 0x000BC5CB File Offset: 0x000BB5CB
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

		// Token: 0x040012B2 RID: 4786
		private CLIParams.MapEditorMode mode;

		// Token: 0x040012B3 RID: 4787
		private int intParam = 1;

		// Token: 0x040012B4 RID: 4788
		private string stringParam = string.Empty;

		// Token: 0x040012B5 RID: 4789
		private string commandLine = string.Empty;

		// Token: 0x0200026E RID: 622
		public enum MapEditorMode
		{
			// Token: 0x040012B7 RID: 4791
			Unknown,
			// Token: 0x040012B8 RID: 4792
			Mode1,
			// Token: 0x040012B9 RID: 4793
			Mode2,
			// Token: 0x040012BA RID: 4794
			Mode3
		}
	}
}
