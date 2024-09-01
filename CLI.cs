using System;
using CommandLine;

namespace MapEditor
{
	// Token: 0x0200026F RID: 623
	internal static class CLI
	{
		// Token: 0x06001D77 RID: 7543 RVA: 0x000BC5FC File Offset: 0x000BB5FC
		internal static int Launch(string[] args)
		{
			CLIParams cliParams = new CLIParams();
			Parser parser = new Parser(Str.MergeCommandLine(args), cliParams);
			parser.Parse();
			if (cliParams.IsValid())
			{
				return 0;
			}
			cliParams.ShowHelp(parser);
			return -1;
		}
	}
}
