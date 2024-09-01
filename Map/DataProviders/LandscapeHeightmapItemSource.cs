using System;

namespace MapEditor.Map.DataProviders
{
	// Token: 0x02000208 RID: 520
	internal class LandscapeHeightmapItemSource : FileItemSource
	{
		// Token: 0x06001995 RID: 6549 RVA: 0x000A73E3 File Offset: 0x000A63E3
		public LandscapeHeightmapItemSource() : base(EditorEnvironment.EditorFolder + LandscapeHeightmapItemSource.folder, "*.tga", EditorEnvironment.EditorFolder, true)
		{
		}

		// Token: 0x17000628 RID: 1576
		// (get) Token: 0x06001996 RID: 6550 RVA: 0x000A7405 File Offset: 0x000A6405
		public static string Folder
		{
			get
			{
				return LandscapeHeightmapItemSource.folder;
			}
		}

		// Token: 0x06001997 RID: 6551 RVA: 0x000A740C File Offset: 0x000A640C
		public static bool ValidProfile(string item)
		{
			return item.StartsWith(LandscapeHeightmapItemSource.folder);
		}

		// Token: 0x04001055 RID: 4181
		private static readonly string folder = "Heightmaps/";
	}
}
