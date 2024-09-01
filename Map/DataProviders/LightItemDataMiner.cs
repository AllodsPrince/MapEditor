using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using Db;
using Tools.ItemDataContainer;

namespace MapEditor.Map.DataProviders
{
	// Token: 0x020001AB RID: 427
	internal class LightItemDataMiner : IItemDataMiner
	{
		// Token: 0x06001492 RID: 5266 RVA: 0x00094A80 File Offset: 0x00093A80
		private bool IsValid(string item, out DBID dbid)
		{
			if (this.mainDb != null && item.EndsWith(".xdb"))
			{
				dbid = this.mainDb.GetDBIDByName(item);
				if (this.mainDb.GetClassTypeName(dbid) == "ZoneLights")
				{
					return true;
				}
			}
			dbid = DBID.Empty;
			return false;
		}

		// Token: 0x06001493 RID: 5267 RVA: 0x00094AD3 File Offset: 0x00093AD3
		public LightItemDataMiner()
		{
			this.mainDb = IDatabase.GetMainDatabase();
		}

		// Token: 0x06001494 RID: 5268 RVA: 0x00094AE8 File Offset: 0x00093AE8
		public bool GetItemData(string item, out string text, out string toolTip, out Bitmap smallBitmap, out Bitmap largeBitmap)
		{
			text = item;
			toolTip = item;
			smallBitmap = null;
			largeBitmap = null;
			DBID dbid;
			if (this.IsValid(item, out dbid))
			{
				Point smallBitmapSize = new Point(16, 16);
				Point largeBitmapSize = new Point(64, 64);
				IObjMan lightMan = this.mainDb.GetManipulator(dbid);
				if (lightMan != null)
				{
					text = Str.CutFileExtention(Str.CutFilePathAndExtention(item));
					toolTip = text;
					int zoneColor;
					lightMan.GetValue("color", out zoneColor);
					SolidBrush solidBrush = new SolidBrush(Color.FromArgb(255, Color.FromArgb(zoneColor)));
					smallBitmap = new Bitmap(smallBitmapSize.X, smallBitmapSize.Y, PixelFormat.Format24bppRgb);
					Graphics smallGraphics = Graphics.FromImage(smallBitmap);
					smallGraphics.FillRectangle(solidBrush, 0, 0, smallBitmapSize.X, smallBitmapSize.Y);
					smallGraphics.Dispose();
					largeBitmap = new Bitmap(largeBitmapSize.X, largeBitmapSize.Y, PixelFormat.Format24bppRgb);
					Graphics largeGraphics = Graphics.FromImage(largeBitmap);
					largeGraphics.FillRectangle(solidBrush, 0, 0, largeBitmapSize.X, largeBitmapSize.Y);
					largeGraphics.Dispose();
					return true;
				}
			}
			return false;
		}

		// Token: 0x06001495 RID: 5269 RVA: 0x00094C00 File Offset: 0x00093C00
		public bool CacheValid(string item, DateTime dateTime, out bool cacheValid)
		{
			cacheValid = false;
			DBID dbid;
			if (!this.IsValid(item, out dbid))
			{
				return false;
			}
			string filePath = EditorEnvironment.DataFolder + item;
			if (File.Exists(filePath) && File.GetLastWriteTime(filePath) < dateTime)
			{
				cacheValid = true;
			}
			return true;
		}

		// Token: 0x17000410 RID: 1040
		// (get) Token: 0x06001496 RID: 5270 RVA: 0x00094C43 File Offset: 0x00093C43
		public string StandardIconKey
		{
			get
			{
				return string.Empty;
			}
		}

		// Token: 0x17000411 RID: 1041
		// (get) Token: 0x06001497 RID: 5271 RVA: 0x00094C4A File Offset: 0x00093C4A
		public bool Cache
		{
			get
			{
				return true;
			}
		}

		// Token: 0x04000E79 RID: 3705
		private readonly IDatabase mainDb;
	}
}
