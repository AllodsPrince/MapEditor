using System;
using System.Drawing;
using MapEditor.Resources.Images;
using Tools.Geometry;
using Tools.ItemDataContainer;

namespace MapEditor.Map.DataProviders
{
	// Token: 0x02000212 RID: 530
	internal class LandscapeHillItemDataMiner : IItemDataMiner
	{
		// Token: 0x060019C6 RID: 6598 RVA: 0x000A7D3C File Offset: 0x000A6D3C
		public bool GetItemData(string item, out string text, out string toolTip, out Bitmap smallBitmap, out Bitmap largeBitmap)
		{
			if (LandscapeHillItemSource.ValidProfile(item))
			{
				text = item.Substring(LandscapeHillItemSource.Folder.Length);
				text = text.Substring(0, text.LastIndexOf('.'));
				toolTip = item;
				Tools.Geometry.Point smallBitmapSize = new Tools.Geometry.Point(16, 16);
				Tools.Geometry.Point largeBitmapSize = new Tools.Geometry.Point(64, 64);
				smallBitmap = new Bitmap(Images.hill_maker, smallBitmapSize.X, smallBitmapSize.Y);
				largeBitmap = new Bitmap(Images.hill_maker, largeBitmapSize.X, largeBitmapSize.Y);
				return true;
			}
			text = null;
			toolTip = null;
			smallBitmap = null;
			largeBitmap = null;
			return false;
		}

		// Token: 0x060019C7 RID: 6599 RVA: 0x000A7DD6 File Offset: 0x000A6DD6
		public bool CacheValid(string item, DateTime dateTime, out bool cacheValid)
		{
			cacheValid = false;
			return false;
		}

		// Token: 0x17000638 RID: 1592
		// (get) Token: 0x060019C8 RID: 6600 RVA: 0x000A7DDC File Offset: 0x000A6DDC
		public string StandardIconKey
		{
			get
			{
				return "LandscapeHillIconKey";
			}
		}

		// Token: 0x17000639 RID: 1593
		// (get) Token: 0x060019C9 RID: 6601 RVA: 0x000A7DE3 File Offset: 0x000A6DE3
		public bool Cache
		{
			get
			{
				return false;
			}
		}
	}
}
