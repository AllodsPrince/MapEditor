using System;

namespace MapEditor.Map.DataProviders
{
	// Token: 0x02000209 RID: 521
	internal class LandscapeClipboardItemSource : FileItemSource
	{
		// Token: 0x06001999 RID: 6553 RVA: 0x000A7425 File Offset: 0x000A6425
		public LandscapeClipboardItemSource() : base(EditorEnvironment.EditorFolder + LandscapeClipboardItemSource.folder, "*.bin", EditorEnvironment.EditorFolder, true)
		{
		}

		// Token: 0x17000629 RID: 1577
		// (get) Token: 0x0600199A RID: 6554 RVA: 0x000A7447 File Offset: 0x000A6447
		public static string Folder
		{
			get
			{
				return LandscapeClipboardItemSource.folder;
			}
		}

		// Token: 0x0600199B RID: 6555 RVA: 0x000A744E File Offset: 0x000A644E
		public static bool ValidProfile(string item)
		{
			return item.StartsWith(LandscapeClipboardItemSource.folder);
		}

		// Token: 0x04001056 RID: 4182
		private static readonly string folder = "Clipboard/";
	}
}
