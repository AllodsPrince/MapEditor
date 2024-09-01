using System;
using System.Drawing;
using MapEditor.Resources.Images;
using Tools.Geometry;
using Tools.ItemDataContainer;

namespace MapEditor.Map.DataProviders
{
	// Token: 0x02000213 RID: 531
	internal class LandscapeRoadItemDataMiner : IItemDataMiner
	{
		// Token: 0x060019CB RID: 6603 RVA: 0x000A7DF0 File Offset: 0x000A6DF0
		public bool GetItemData(string item, out string text, out string toolTip, out Bitmap smallBitmap, out Bitmap largeBitmap)
		{
			if (LandscapeRoadItemSource.ValidProfile(item))
			{
				text = item.Substring(LandscapeRoadItemSource.Folder.Length);
				text = text.Substring(0, text.LastIndexOf('.'));
				toolTip = item;
				Tools.Geometry.Point smallBitmapSize = new Tools.Geometry.Point(16, 16);
				Tools.Geometry.Point largeBitmapSize = new Tools.Geometry.Point(64, 64);
				smallBitmap = new Bitmap(Images.road, smallBitmapSize.X, smallBitmapSize.Y);
				largeBitmap = new Bitmap(Images.road, largeBitmapSize.X, largeBitmapSize.Y);
				return true;
			}
			text = null;
			toolTip = null;
			smallBitmap = null;
			largeBitmap = null;
			return false;
		}

		// Token: 0x060019CC RID: 6604 RVA: 0x000A7E8A File Offset: 0x000A6E8A
		public bool CacheValid(string item, DateTime dateTime, out bool cacheValid)
		{
			cacheValid = false;
			return false;
		}

		// Token: 0x1700063A RID: 1594
		// (get) Token: 0x060019CD RID: 6605 RVA: 0x000A7E90 File Offset: 0x000A6E90
		public string StandardIconKey
		{
			get
			{
				return "LandscapeRoadIconKey";
			}
		}

		// Token: 0x1700063B RID: 1595
		// (get) Token: 0x060019CE RID: 6606 RVA: 0x000A7E97 File Offset: 0x000A6E97
		public bool Cache
		{
			get
			{
				return false;
			}
		}
	}
}
