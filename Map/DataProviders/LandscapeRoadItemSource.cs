using System;

namespace MapEditor.Map.DataProviders
{
	// Token: 0x0200020B RID: 523
	internal class LandscapeRoadItemSource : FileItemSource
	{
		// Token: 0x060019A1 RID: 6561 RVA: 0x000A74A9 File Offset: 0x000A64A9
		public LandscapeRoadItemSource() : base(EditorEnvironment.EditorFolder + LandscapeRoadItemSource.folder, "*.xml", EditorEnvironment.EditorFolder, true)
		{
		}

		// Token: 0x1700062B RID: 1579
		// (get) Token: 0x060019A2 RID: 6562 RVA: 0x000A74CB File Offset: 0x000A64CB
		public static string Folder
		{
			get
			{
				return LandscapeRoadItemSource.folder;
			}
		}

		// Token: 0x060019A3 RID: 6563 RVA: 0x000A74D2 File Offset: 0x000A64D2
		public static bool ValidProfile(string item)
		{
			return item.StartsWith(LandscapeRoadItemSource.folder);
		}

		// Token: 0x04001058 RID: 4184
		private static readonly string folder = "Roads/";
	}
}
