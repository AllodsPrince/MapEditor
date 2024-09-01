using System;
using CommandLine;
using Tools.EditorModule;

namespace MapEditor
{
	// Token: 0x02000022 RID: 34
	internal static class GUI
	{
		// Token: 0x060002AB RID: 683 RVA: 0x0001D19C File Offset: 0x0001C19C
		internal static ModuleInterface Launch(string[] args)
		{
			GUIParams guiParams = new GUIParams();
			Parser parser = new Parser(string.Concat(args), guiParams);
			parser.Parse();
			if (guiParams.IsValid())
			{
				return MapEditorModule.Module;
			}
			guiParams.ShowHelp(parser);
			return null;
		}
	}
}
