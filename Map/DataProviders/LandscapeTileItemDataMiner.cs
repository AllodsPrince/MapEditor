using System;
using System.Drawing;
using System.IO;
using Db;
using MapEditor.Resources.Images;
using Tools.EditorImage;
using Tools.Geometry;
using Tools.ItemDataContainer;

namespace MapEditor.Map.DataProviders
{
	// Token: 0x0200020E RID: 526
	internal class LandscapeTileItemDataMiner : IItemDataMiner
	{
		// Token: 0x060019B1 RID: 6577 RVA: 0x000A7772 File Offset: 0x000A6772
		public LandscapeTileItemDataMiner(string _continentName)
		{
			this.continentName = _continentName;
		}

		// Token: 0x060019B2 RID: 6578 RVA: 0x000A7784 File Offset: 0x000A6784
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
				string tile = this.continentName + "_Layer_";
				if (item == LandscapeTileItemSource.GetRemoveTerrainItem(this.continentName))
				{
					text = "Remove Terrain";
					toolTip = "Remove Terrain";
					smallBitmap = new Bitmap(Images.remove_terrain, LandscapeTileItemDataMiner.smallBitmapSize.X, LandscapeTileItemDataMiner.smallBitmapSize.Y);
					largeBitmap = new Bitmap(Images.remove_terrain, LandscapeTileItemDataMiner.largeBitmapSize.X, LandscapeTileItemDataMiner.largeBitmapSize.Y);
					return true;
				}
				if (item == LandscapeTileItemSource.GetSmoothRemoveTerrainItem(this.continentName))
				{
					text = "Smooth Remove Terrain";
					toolTip = "Smooth Remove Terrain";
					smallBitmap = new Bitmap(Images.smooth_remove_terrain, LandscapeTileItemDataMiner.smallBitmapSize.X, LandscapeTileItemDataMiner.smallBitmapSize.Y);
					largeBitmap = new Bitmap(Images.smooth_remove_terrain, LandscapeTileItemDataMiner.largeBitmapSize.X, LandscapeTileItemDataMiner.largeBitmapSize.Y);
					return true;
				}
				if (item.Contains(tile))
				{
					string s = item.Substring(tile.Length, item.Length - tile.Length);
					int index = int.Parse(s);
					string field = "Layers.[" + index + "]";
					string objectPropertyName = field + "SymbolName";
					string tgaPropertyName = field + "DiffuseTexture";
					string name;
					layersMan.GetValue(objectPropertyName, out name);
					DBID tgaDBID;
					layersMan.GetValue(tgaPropertyName, out tgaDBID);
					if (!string.IsNullOrEmpty(name.Trim()))
					{
						IObjMan tgaMan = mainDb.GetManipulator(tgaDBID);
						if (tgaMan != null)
						{
							string source;
							tgaMan.GetValue("sourceFile", out source);
							text = string.Format("{0} ({1})", name, index);
							toolTip = string.Format("name: {0}\nindex: {1}\nsource: {2}", name, index, source);
							EditorImage.LoadTextureToRGBBitmap(tgaDBID.ToString(), LandscapeTileItemDataMiner.largeBitmapSize, out largeBitmap);
							if (largeBitmap != null)
							{
								smallBitmap = new Bitmap(largeBitmap, LandscapeTileItemDataMiner.smallBitmapSize.X, LandscapeTileItemDataMiner.smallBitmapSize.Y);
							}
							return true;
						}
					}
				}
			}
			return false;
		}

		// Token: 0x060019B3 RID: 6579 RVA: 0x000A79B0 File Offset: 0x000A69B0
		public bool CacheValid(string item, DateTime dateTime, out bool cacheValid)
		{
			cacheValid = false;
			string layer = this.continentName + "_Layer_";
			if (item == LandscapeTileItemSource.GetRemoveTerrainItem(this.continentName))
			{
				return true;
			}
			if (item == LandscapeTileItemSource.GetSmoothRemoveTerrainItem(this.continentName))
			{
				return true;
			}
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

		// Token: 0x17000630 RID: 1584
		// (get) Token: 0x060019B4 RID: 6580 RVA: 0x000A7A36 File Offset: 0x000A6A36
		public string StandardIconKey
		{
			get
			{
				return string.Empty;
			}
		}

		// Token: 0x17000631 RID: 1585
		// (get) Token: 0x060019B5 RID: 6581 RVA: 0x000A7A3D File Offset: 0x000A6A3D
		public bool Cache
		{
			get
			{
				return true;
			}
		}

		// Token: 0x04001061 RID: 4193
		private readonly string continentName;

		// Token: 0x04001062 RID: 4194
		private static Tools.Geometry.Point smallBitmapSize = new Tools.Geometry.Point(16, 16);

		// Token: 0x04001063 RID: 4195
		private static Tools.Geometry.Point largeBitmapSize = new Tools.Geometry.Point(64, 64);
	}
}
