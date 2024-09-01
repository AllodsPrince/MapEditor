using System;

namespace MapEditor.Map.DataProviders
{
	// Token: 0x02000290 RID: 656
	public class MultiObjectItemListSource : FileItemSource
	{
		// Token: 0x06001EC9 RID: 7881 RVA: 0x000C69EC File Offset: 0x000C59EC
		public MultiObjectItemListSource() : base(MultiObjectItemListSource.folder, "*.xml", null, true)
		{
		}

		// Token: 0x17000714 RID: 1812
		// (get) Token: 0x06001ECA RID: 7882 RVA: 0x000C6A00 File Offset: 0x000C5A00
		public static string Folder
		{
			get
			{
				return MultiObjectItemListSource.folder;
			}
		}

		// Token: 0x06001ECB RID: 7883 RVA: 0x000C6A07 File Offset: 0x000C5A07
		public static bool ValidMultiObject(string item)
		{
			return item.StartsWith(MultiObjectItemListSource.folder);
		}

		// Token: 0x0400132E RID: 4910
		private static readonly string folder = EditorEnvironment.EditorFolder + "MultiObjects/";
	}
}
