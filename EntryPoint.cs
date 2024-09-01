using System;
using Tools.EditorModule;

namespace MapEditor
{
	// Token: 0x0200007E RID: 126
	[EntryClass("MapEditor", "Map editor functional module")]
	public class EntryPoint
	{
		// Token: 0x06000610 RID: 1552 RVA: 0x00033F17 File Offset: 0x00032F17
		[EntryGUI]
		public static ModuleInterface LaunchGUI(string[] args)
		{
			return GUI.Launch(args);
		}

		// Token: 0x06000611 RID: 1553 RVA: 0x00033F1F File Offset: 0x00032F1F
		[EntryCLI]
		public static int LaunchCLI(string[] args)
		{
			return CLI.Launch(args);
		}
	}
}
