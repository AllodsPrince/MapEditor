using System;

namespace MapEditor.Map.DataProviders
{
	// Token: 0x02000207 RID: 519
	internal class LandscapeProfileItemSource : FileItemSource
	{
		// Token: 0x06001991 RID: 6545 RVA: 0x000A73A1 File Offset: 0x000A63A1
		public LandscapeProfileItemSource() : base(EditorEnvironment.EditorFolder + LandscapeProfileItemSource.folder, "*.tga", EditorEnvironment.EditorFolder, true)
		{
		}

		// Token: 0x17000627 RID: 1575
		// (get) Token: 0x06001992 RID: 6546 RVA: 0x000A73C3 File Offset: 0x000A63C3
		public static string Folder
		{
			get
			{
				return LandscapeProfileItemSource.folder;
			}
		}

		// Token: 0x06001993 RID: 6547 RVA: 0x000A73CA File Offset: 0x000A63CA
		public static bool ValidProfile(string item)
		{
			return item.StartsWith(LandscapeProfileItemSource.folder);
		}

		// Token: 0x04001054 RID: 4180
		private static readonly string folder = "Profiles/";
	}
}
