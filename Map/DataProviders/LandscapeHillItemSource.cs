using System;

namespace MapEditor.Map.DataProviders
{
	// Token: 0x0200020A RID: 522
	internal class LandscapeHillItemSource : FileItemSource
	{
		// Token: 0x0600199D RID: 6557 RVA: 0x000A7467 File Offset: 0x000A6467
		public LandscapeHillItemSource() : base(EditorEnvironment.EditorFolder + LandscapeHillItemSource.folder, "*.xml", EditorEnvironment.EditorFolder, true)
		{
		}

		// Token: 0x1700062A RID: 1578
		// (get) Token: 0x0600199E RID: 6558 RVA: 0x000A7489 File Offset: 0x000A6489
		public static string Folder
		{
			get
			{
				return LandscapeHillItemSource.folder;
			}
		}

		// Token: 0x0600199F RID: 6559 RVA: 0x000A7490 File Offset: 0x000A6490
		public static bool ValidProfile(string item)
		{
			return item.StartsWith(LandscapeHillItemSource.folder);
		}

		// Token: 0x04001057 RID: 4183
		private static readonly string folder = "Hills/";
	}
}
