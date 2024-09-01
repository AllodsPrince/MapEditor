using System;
using System.Drawing;
using MapEditor.Resources.Images;
using Tools.ItemDataContainer;

namespace MapEditor.Map.DataProviders
{
	// Token: 0x02000294 RID: 660
	internal class ObjectSetItemDataMiner : IItemDataMiner
	{
		// Token: 0x06001EDF RID: 7903 RVA: 0x000C6C00 File Offset: 0x000C5C00
		public bool GetItemData(string item, out string text, out string toolTip, out Bitmap smallBitmap, out Bitmap largeBitmap)
		{
			if (item.StartsWith(ObjectSetItemListSource.Folder) && item.EndsWith(".xml"))
			{
				string _item = item.Replace('\\', '/');
				_item = Str.CutFilePathAndExtention(_item);
				text = _item;
				toolTip = item;
				Point smallBitmapSize = new Point(16, 16);
				Point largeBitmapSize = new Point(64, 64);
				smallBitmap = new Bitmap(Images.object_set, smallBitmapSize.X, smallBitmapSize.Y);
				largeBitmap = new Bitmap(Images.object_set, largeBitmapSize.X, largeBitmapSize.Y);
				return true;
			}
			text = item;
			toolTip = item;
			smallBitmap = null;
			largeBitmap = null;
			return false;
		}

		// Token: 0x06001EE0 RID: 7904 RVA: 0x000C6C9C File Offset: 0x000C5C9C
		public bool CacheValid(string item, DateTime dateTime, out bool cacheValid)
		{
			cacheValid = false;
			return false;
		}

		// Token: 0x1700071C RID: 1820
		// (get) Token: 0x06001EE1 RID: 7905 RVA: 0x000C6CA2 File Offset: 0x000C5CA2
		public string StandardIconKey
		{
			get
			{
				return "ObjectSetIconKey";
			}
		}

		// Token: 0x1700071D RID: 1821
		// (get) Token: 0x06001EE2 RID: 7906 RVA: 0x000C6CA9 File Offset: 0x000C5CA9
		public bool Cache
		{
			get
			{
				return false;
			}
		}
	}
}
