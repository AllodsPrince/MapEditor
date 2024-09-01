using System;

namespace MapEditor.Map.DataProviders
{
	// Token: 0x02000291 RID: 657
	public class ObjectSetItemListSource : FileItemSource
	{
		// Token: 0x06001ECD RID: 7885 RVA: 0x000C6A2A File Offset: 0x000C5A2A
		public ObjectSetItemListSource() : base(ObjectSetItemListSource.folder, "*.xml", null, true)
		{
		}

		// Token: 0x17000715 RID: 1813
		// (get) Token: 0x06001ECE RID: 7886 RVA: 0x000C6A3E File Offset: 0x000C5A3E
		public static string Folder
		{
			get
			{
				return ObjectSetItemListSource.folder;
			}
		}

		// Token: 0x06001ECF RID: 7887 RVA: 0x000C6A45 File Offset: 0x000C5A45
		public static bool ValidObjectSet(string item)
		{
			return item.StartsWith(ObjectSetItemListSource.folder);
		}

		// Token: 0x0400132F RID: 4911
		private static readonly string folder = EditorEnvironment.EditorFolder + "ObjectSets/";
	}
}
