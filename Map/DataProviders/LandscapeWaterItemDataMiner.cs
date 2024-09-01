using System;
using System.Drawing;
using System.IO;
using Db;
using Tools.EditorImage;
using Tools.Geometry;
using Tools.ItemDataContainer;

namespace MapEditor.Map.DataProviders
{
	// Token: 0x02000214 RID: 532
	internal class LandscapeWaterItemDataMiner : IItemDataMiner
	{
		// Token: 0x060019D0 RID: 6608 RVA: 0x000A7EA2 File Offset: 0x000A6EA2
		public LandscapeWaterItemDataMiner(string _continentName)
		{
			this.continentName = _continentName;
		}

		// Token: 0x060019D1 RID: 6609 RVA: 0x000A7EB4 File Offset: 0x000A6EB4
		public bool GetItemData(string item, out string text, out string toolTip, out Bitmap smallBitmap, out Bitmap largeBitmap)
		{
			IDatabase mainDb = IDatabase.GetMainDatabase();
			IObjMan layersMan = mainDb.GetManipulator(mainDb.GetDBIDByName(this.continentName + "/layers.xdb"));
			text = item;
			toolTip = item;
			smallBitmap = null;
			largeBitmap = null;
			if (layersMan != null)
			{
				string tile = this.continentName + string.Format("_{0}_", "WaterLayer");
				if (item.Contains(tile))
				{
					string s = item.Substring(tile.Length, item.Length - tile.Length);
					int index = int.Parse(s);
					string field = "waterLayers.[" + index + "]";
					string objectPropertyName = field + "SymbolName";
					string tgaPropertyName = field + "bumpTexture";
					string name;
					layersMan.GetValue(objectPropertyName, out name);
					DBID tgaDBID;
					layersMan.GetValue(tgaPropertyName, out tgaDBID);
					if (!string.IsNullOrEmpty(name.Trim()))
					{
						IObjMan tgaMan = mainDb.GetManipulator(tgaDBID);
						string source = string.Empty;
						if (tgaMan != null)
						{
							tgaMan.GetValue("sourceFile", out source);
							EditorImage.LoadTextureToRGBBitmap(tgaDBID.ToString(), LandscapeWaterItemDataMiner.largeBitmapSize, out largeBitmap);
						}
						text = string.Format("{0} ({1})", name, index);
						toolTip = string.Format("name: {0}\nindex: {1}\nsource: {2}", name, index, source);
						if (largeBitmap != null)
						{
							smallBitmap = new Bitmap(largeBitmap, LandscapeWaterItemDataMiner.smallBitmapSize.X, LandscapeWaterItemDataMiner.smallBitmapSize.Y);
						}
					}
					return true;
				}
			}
			return false;
		}

		// Token: 0x060019D2 RID: 6610 RVA: 0x000A8024 File Offset: 0x000A7024
		public bool CacheValid(string item, DateTime dateTime, out bool cacheValid)
		{
			cacheValid = false;
			string layer = this.continentName + string.Format("_{0}_", "WaterLayer");
			if (item.Contains(layer))
			{
				string filePath = EditorEnvironment.DataFolder + this.continentName + "/layers.xdb";
				if (File.Exists(filePath) && File.GetLastWriteTime(filePath) < dateTime)
				{
					cacheValid = true;
				}
				return true;
			}
			return false;
		}

		// Token: 0x1700063C RID: 1596
		// (get) Token: 0x060019D3 RID: 6611 RVA: 0x000A808A File Offset: 0x000A708A
		public string StandardIconKey
		{
			get
			{
				return string.Empty;
			}
		}

		// Token: 0x1700063D RID: 1597
		// (get) Token: 0x060019D4 RID: 6612 RVA: 0x000A8091 File Offset: 0x000A7091
		public bool Cache
		{
			get
			{
				return true;
			}
		}

		// Token: 0x04001064 RID: 4196
		private readonly string continentName;

		// Token: 0x04001065 RID: 4197
		private static Tools.Geometry.Point smallBitmapSize = new Tools.Geometry.Point(16, 16);

		// Token: 0x04001066 RID: 4198
		private static Tools.Geometry.Point largeBitmapSize = new Tools.Geometry.Point(64, 64);
	}
}
