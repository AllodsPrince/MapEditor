using System;
using System.Drawing;
using System.IO;
using System.Text.RegularExpressions;
using Tools.EditorImage;
using Tools.Geometry;
using Tools.ItemDataContainer;

namespace MapEditor.Map.DataProviders
{
	// Token: 0x0200020F RID: 527
	internal class LandscapeProfileItemDataMiner : IItemDataMiner
	{
		// Token: 0x060019B7 RID: 6583 RVA: 0x000A7A60 File Offset: 0x000A6A60
		public bool GetItemData(string item, out string text, out string toolTip, out Bitmap smallBitmap, out Bitmap largeBitmap)
		{
			if (LandscapeProfileItemSource.ValidProfile(item))
			{
				text = item.Substring(LandscapeProfileItemSource.Folder.Length);
				text = text.Substring(0, text.LastIndexOf('.'));
				toolTip = item;
				Tools.Geometry.Point smallBitmapSize = new Tools.Geometry.Point(16, 16);
				Tools.Geometry.Point largeBitmapSize = new Tools.Geometry.Point(64, 64);
				string imagePath = EditorEnvironment.EditorFolder + item;
				EditorImage.LoadTGAImageToRGBBitmap(imagePath, largeBitmapSize, out largeBitmap);
				if (largeBitmap != null)
				{
					smallBitmap = new Bitmap(largeBitmap, smallBitmapSize.X, smallBitmapSize.Y);
				}
				else
				{
					smallBitmap = null;
				}
				return true;
			}
			text = null;
			toolTip = null;
			smallBitmap = null;
			largeBitmap = null;
			return false;
		}

		// Token: 0x060019B8 RID: 6584 RVA: 0x000A7B00 File Offset: 0x000A6B00
		public bool CacheValid(string item, DateTime dateTime, out bool cacheValid)
		{
			Regex r = new Regex(string.Format("{0}(.)*.tga", LandscapeProfileItemSource.Folder));
			Match i = r.Match(item);
			cacheValid = false;
			if (!i.Success)
			{
				return false;
			}
			string imagePath = EditorEnvironment.EditorFolder + item;
			if (!File.Exists(imagePath))
			{
				return false;
			}
			if (File.GetLastWriteTime(imagePath) < dateTime)
			{
				cacheValid = true;
			}
			return true;
		}

		// Token: 0x17000632 RID: 1586
		// (get) Token: 0x060019B9 RID: 6585 RVA: 0x000A7B5F File Offset: 0x000A6B5F
		public string StandardIconKey
		{
			get
			{
				return string.Empty;
			}
		}

		// Token: 0x17000633 RID: 1587
		// (get) Token: 0x060019BA RID: 6586 RVA: 0x000A7B66 File Offset: 0x000A6B66
		public bool Cache
		{
			get
			{
				return true;
			}
		}
	}
}
