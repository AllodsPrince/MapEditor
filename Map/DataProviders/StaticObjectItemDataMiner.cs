using System;
using System.Drawing;
using Db;
using MapEditor.Map.MapObjects;
using Tools.ItemDataContainer;

namespace MapEditor.Map.DataProviders
{
	// Token: 0x02000292 RID: 658
	internal class StaticObjectItemDataMiner : IItemDataMiner
	{
		// Token: 0x06001ED1 RID: 7889 RVA: 0x000C6A68 File Offset: 0x000C5A68
		public static bool IsValid(string item)
		{
			if (!item.EndsWith(".xdb"))
			{
				return false;
			}
			IDatabase mainDb = IDatabase.GetMainDatabase();
			if (mainDb == null)
			{
				return false;
			}
			DBID dbid = mainDb.GetDBIDByName(item);
			return !(mainDb.GetClassTypeName(dbid) != StaticObject.DBType);
		}

		// Token: 0x17000716 RID: 1814
		// (get) Token: 0x06001ED2 RID: 7890 RVA: 0x000C6AAD File Offset: 0x000C5AAD
		public static Size SmallIconSize
		{
			get
			{
				return StaticObjectItemDataMiner.smallIconSize;
			}
		}

		// Token: 0x17000717 RID: 1815
		// (get) Token: 0x06001ED3 RID: 7891 RVA: 0x000C6AB4 File Offset: 0x000C5AB4
		public static Size LargeIconSize
		{
			get
			{
				return StaticObjectItemDataMiner.largeIconSize;
			}
		}

		// Token: 0x06001ED4 RID: 7892 RVA: 0x000C6ABC File Offset: 0x000C5ABC
		public bool GetItemData(string item, out string text, out string toolTip, out Bitmap smallBitmap, out Bitmap largeBitmap)
		{
			if (StaticObjectItemDataMiner.IsValid(item))
			{
				string _item = item.Replace('\\', '/');
				_item = Str.CutFilePathAndExtention(_item);
				_item = Str.CutFileExtention(_item);
				text = _item;
				toolTip = item;
				smallBitmap = null;
				largeBitmap = null;
				return true;
			}
			text = item;
			toolTip = item;
			smallBitmap = null;
			largeBitmap = null;
			return false;
		}

		// Token: 0x06001ED5 RID: 7893 RVA: 0x000C6B09 File Offset: 0x000C5B09
		public bool CacheValid(string item, DateTime dateTime, out bool cacheValid)
		{
			if (StaticObjectItemDataMiner.IsValid(item))
			{
				cacheValid = true;
				return true;
			}
			cacheValid = false;
			return false;
		}

		// Token: 0x17000718 RID: 1816
		// (get) Token: 0x06001ED6 RID: 7894 RVA: 0x000C6B1C File Offset: 0x000C5B1C
		public string StandardIconKey
		{
			get
			{
				return string.Empty;
			}
		}

		// Token: 0x17000719 RID: 1817
		// (get) Token: 0x06001ED7 RID: 7895 RVA: 0x000C6B23 File Offset: 0x000C5B23
		public bool Cache
		{
			get
			{
				return false;
			}
		}

		// Token: 0x04001330 RID: 4912
		private static readonly Size smallIconSize = new Size(16, 16);

		// Token: 0x04001331 RID: 4913
		private static readonly Size largeIconSize = new Size(64, 64);
	}
}
