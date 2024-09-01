using System;
using System.Drawing;
using System.IO;
using System.Text.RegularExpressions;
using Tools.EditorImage;
using Tools.Geometry;
using Tools.ItemDataContainer;

namespace MapEditor.Map.DataProviders
{
	// Token: 0x02000210 RID: 528
	internal class LandscapeHeightmapItemDataMiner : IItemDataMiner
	{
		// Token: 0x060019BC RID: 6588 RVA: 0x000A7B74 File Offset: 0x000A6B74
		public bool GetItemData(string item, out string text, out string toolTip, out Bitmap smallBitmap, out Bitmap largeBitmap)
		{
			if (LandscapeHeightmapItemSource.ValidProfile(item))
			{
				text = item.Substring(LandscapeHeightmapItemSource.Folder.Length);
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

		// Token: 0x060019BD RID: 6589 RVA: 0x000A7C14 File Offset: 0x000A6C14
		public bool CacheValid(string item, DateTime dateTime, out bool cacheValid)
		{
			Regex r = new Regex(string.Format("{0}(.)*.tga", LandscapeHeightmapItemSource.Folder));
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

		// Token: 0x17000634 RID: 1588
		// (get) Token: 0x060019BE RID: 6590 RVA: 0x000A7C73 File Offset: 0x000A6C73
		public string StandardIconKey
		{
			get
			{
				return string.Empty;
			}
		}

		// Token: 0x17000635 RID: 1589
		// (get) Token: 0x060019BF RID: 6591 RVA: 0x000A7C7A File Offset: 0x000A6C7A
		public bool Cache
		{
			get
			{
				return true;
			}
		}
	}
}
