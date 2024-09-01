using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using Db;
using Tools.ItemDataContainer;

namespace MapEditor.Map.DataProviders
{
	// Token: 0x02000237 RID: 567
	internal class SoundItemDataMiner : IItemDataMiner
	{
		// Token: 0x06001B14 RID: 6932 RVA: 0x000B023C File Offset: 0x000AF23C
		private bool IsValid(string item, out DBID dbid)
		{
			if (this.mainDb != null && item.EndsWith(".xdb"))
			{
				dbid = this.mainDb.GetDBIDByName(item);
				if (this.mainDb.GetClassTypeName(dbid) == "Sound2DTassel")
				{
					return true;
				}
			}
			dbid = DBID.Empty;
			return false;
		}

		// Token: 0x06001B15 RID: 6933 RVA: 0x000B028F File Offset: 0x000AF28F
		public SoundItemDataMiner()
		{
			this.mainDb = IDatabase.GetMainDatabase();
		}

		// Token: 0x06001B16 RID: 6934 RVA: 0x000B02A4 File Offset: 0x000AF2A4
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

		// Token: 0x06001B17 RID: 6935 RVA: 0x000B03BC File Offset: 0x000AF3BC
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

		// Token: 0x1700066E RID: 1646
		// (get) Token: 0x06001B18 RID: 6936 RVA: 0x000B03FF File Offset: 0x000AF3FF
		public string StandardIconKey
		{
			get
			{
				return string.Empty;
			}
		}

		// Token: 0x1700066F RID: 1647
		// (get) Token: 0x06001B19 RID: 6937 RVA: 0x000B0406 File Offset: 0x000AF406
		public bool Cache
		{
			get
			{
				return true;
			}
		}

		// Token: 0x04001157 RID: 4439
		private readonly IDatabase mainDb;
	}
}
