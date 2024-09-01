using System;
using System.Collections.Generic;
using System.IO;

namespace MapEditor.Map.MapCheckers
{
	// Token: 0x0200000A RID: 10
	public static class TotalMapCheck
	{
		// Token: 0x0600002D RID: 45 RVA: 0x00003A83 File Offset: 0x00002A83
		static TotalMapCheck()
		{
			EditorEnvironment.Create("LancherMapEditor.dll", Environment.GetCommandLineArgs());
			TotalMapCheck.Dir = new DirectoryInfo(EditorEnvironment.DataFolder + "Maps");
		}

		// Token: 0x0600002E RID: 46 RVA: 0x00003AB8 File Offset: 0x00002AB8
		public static void ScanAllMaps()
		{
			TotalMapCheck.Scan(TotalMapCheck.Dir.FullName);
		}

		// Token: 0x0600002F RID: 47 RVA: 0x00003ACC File Offset: 0x00002ACC
		private static void Scan(string path)
		{
			foreach (string file in Directory.GetDirectories(path))
			{
				if (!file.Contains(".svn"))
				{
					TotalMapCheck.MapDirList.Add(new CMapCheckerHost(file));
				}
			}
		}

		// Token: 0x0400000E RID: 14
		private static DirectoryInfo Dir;

		// Token: 0x0400000F RID: 15
		public static List<CMapCheckerHost> MapDirList = new List<CMapCheckerHost>();
	}
}
