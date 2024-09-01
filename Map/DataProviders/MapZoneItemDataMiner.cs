using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using Db;
using Tools.ItemDataContainer;

namespace MapEditor.Map.DataProviders
{
	// Token: 0x02000046 RID: 70
	internal class MapZoneItemDataMiner : IItemDataMiner
	{
		// Token: 0x060003DC RID: 988 RVA: 0x000208C4 File Offset: 0x0001F8C4
		private bool IsValid(string item, out DBID dbid)
		{
			if (this.mainDb != null && item.EndsWith(".xdb"))
			{
				dbid = this.mainDb.GetDBIDByName(item);
				if (this.mainDb.GetClassTypeName(dbid) == "gameMechanics.map.zone.ZoneResource")
				{
					return true;
				}
			}
			dbid = DBID.Empty;
			return false;
		}

		// Token: 0x060003DD RID: 989 RVA: 0x00020917 File Offset: 0x0001F917
		public MapZoneItemDataMiner()
		{
			this.mainDb = IDatabase.GetMainDatabase();
		}

		// Token: 0x060003DE RID: 990 RVA: 0x0002092C File Offset: 0x0001F92C
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
				IObjMan zoneMan = this.mainDb.GetManipulator(dbid);
				if (zoneMan != null)
				{
					string name;
					zoneMan.GetValue("internalName", out name);
					string newItem = item.Replace('\\', '/');
					string[] itemArr = newItem.Split(new char[]
					{
						'/'
					});
					newItem = itemArr[itemArr.Length - 1];
					itemArr = newItem.Split(new char[]
					{
						'.'
					});
					newItem = itemArr[0];
					if (!string.IsNullOrEmpty(name))
					{
						newItem = newItem + " (" + name + ")";
					}
					int zoneColor;
					zoneMan.GetValue("color", out zoneColor);
					text = newItem;
					toolTip = newItem;
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

		// Token: 0x060003DF RID: 991 RVA: 0x00020AB4 File Offset: 0x0001FAB4
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

		// Token: 0x17000095 RID: 149
		// (get) Token: 0x060003E0 RID: 992 RVA: 0x00020AF7 File Offset: 0x0001FAF7
		public string StandardIconKey
		{
			get
			{
				return string.Empty;
			}
		}

		// Token: 0x17000096 RID: 150
		// (get) Token: 0x060003E1 RID: 993 RVA: 0x00020AFE File Offset: 0x0001FAFE
		public bool Cache
		{
			get
			{
				return true;
			}
		}

		// Token: 0x040002B3 RID: 691
		private readonly IDatabase mainDb;
	}
}
