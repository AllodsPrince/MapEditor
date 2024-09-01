using System;
using System.Drawing;
using MapEditor.Resources.Images;
using Tools.Geometry;
using Tools.ItemDataContainer;

namespace MapEditor.Map.DataProviders
{
	// Token: 0x02000211 RID: 529
	internal class LandscapeClipboardItemDataMiner : IItemDataMiner
	{
		// Token: 0x060019C1 RID: 6593 RVA: 0x000A7C88 File Offset: 0x000A6C88
		public bool GetItemData(string item, out string text, out string toolTip, out Bitmap smallBitmap, out Bitmap largeBitmap)
		{
			if (LandscapeClipboardItemSource.ValidProfile(item))
			{
				text = item.Substring(LandscapeClipboardItemSource.Folder.Length);
				text = text.Substring(0, text.LastIndexOf('.'));
				toolTip = item;
				Tools.Geometry.Point smallBitmapSize = new Tools.Geometry.Point(16, 16);
				Tools.Geometry.Point largeBitmapSize = new Tools.Geometry.Point(64, 64);
				smallBitmap = new Bitmap(Images.landscape_clipboard, smallBitmapSize.X, smallBitmapSize.Y);
				largeBitmap = new Bitmap(Images.landscape_clipboard, largeBitmapSize.X, largeBitmapSize.Y);
				return true;
			}
			text = null;
			toolTip = null;
			smallBitmap = null;
			largeBitmap = null;
			return false;
		}

		// Token: 0x060019C2 RID: 6594 RVA: 0x000A7D22 File Offset: 0x000A6D22
		public bool CacheValid(string item, DateTime dateTime, out bool cacheValid)
		{
			cacheValid = false;
			return false;
		}

		// Token: 0x17000636 RID: 1590
		// (get) Token: 0x060019C3 RID: 6595 RVA: 0x000A7D28 File Offset: 0x000A6D28
		public string StandardIconKey
		{
			get
			{
				return "LandscapeClipboardIconKey";
			}
		}

		// Token: 0x17000637 RID: 1591
		// (get) Token: 0x060019C4 RID: 6596 RVA: 0x000A7D2F File Offset: 0x000A6D2F
		public bool Cache
		{
			get
			{
				return false;
			}
		}
	}
}
