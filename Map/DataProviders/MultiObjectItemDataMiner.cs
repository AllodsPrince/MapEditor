using System;
using System.Drawing;
using MapEditor.Resources.Images;
using Tools.ItemDataContainer;

namespace MapEditor.Map.DataProviders
{
	// Token: 0x02000293 RID: 659
	internal class MultiObjectItemDataMiner : IItemDataMiner
	{
		// Token: 0x06001EDA RID: 7898 RVA: 0x000C6B4C File Offset: 0x000C5B4C
		public bool GetItemData(string item, out string text, out string toolTip, out Bitmap smallBitmap, out Bitmap largeBitmap)
		{
			if (item.StartsWith(MultiObjectItemListSource.Folder) && item.EndsWith(".xml"))
			{
				string _item = item.Replace('\\', '/');
				_item = Str.CutFilePathAndExtention(_item);
				text = _item;
				toolTip = item;
				Point smallBitmapSize = new Point(16, 16);
				Point largeBitmapSize = new Point(64, 64);
				smallBitmap = new Bitmap(Images.multi_object, smallBitmapSize.X, smallBitmapSize.Y);
				largeBitmap = new Bitmap(Images.multi_object, largeBitmapSize.X, largeBitmapSize.Y);
				return true;
			}
			text = item;
			toolTip = item;
			smallBitmap = null;
			largeBitmap = null;
			return false;
		}

		// Token: 0x06001EDB RID: 7899 RVA: 0x000C6BE8 File Offset: 0x000C5BE8
		public bool CacheValid(string item, DateTime dateTime, out bool cacheValid)
		{
			cacheValid = false;
			return false;
		}

		// Token: 0x1700071A RID: 1818
		// (get) Token: 0x06001EDC RID: 7900 RVA: 0x000C6BEE File Offset: 0x000C5BEE
		public string StandardIconKey
		{
			get
			{
				return "MultiObjectIconKey";
			}
		}

		// Token: 0x1700071B RID: 1819
		// (get) Token: 0x06001EDD RID: 7901 RVA: 0x000C6BF5 File Offset: 0x000C5BF5
		public bool Cache
		{
			get
			{
				return false;
			}
		}
	}
}
