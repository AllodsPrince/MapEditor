using System;
using System.Collections.Generic;
using Db;

namespace MapEditor.Map.DataProviders
{
	// Token: 0x02000206 RID: 518
	internal class LandscapeTileItemSource : ItemList.IItemSource
	{
		// Token: 0x17000626 RID: 1574
		// (get) Token: 0x06001987 RID: 6535 RVA: 0x000A7144 File Offset: 0x000A6144
		public IEnumerable<string> Items
		{
			get
			{
				return this.items;
			}
		}

		// Token: 0x06001988 RID: 6536 RVA: 0x000A714C File Offset: 0x000A614C
		public LandscapeTileItemSource(string _continentName)
		{
			this.continentName = _continentName;
			this.Refresh();
		}

		// Token: 0x06001989 RID: 6537 RVA: 0x000A716C File Offset: 0x000A616C
		public static int GetRemoveTerrainIndex()
		{
			return LandscapeTileItemSource.removeTerrainIndex;
		}

		// Token: 0x0600198A RID: 6538 RVA: 0x000A7173 File Offset: 0x000A6173
		public static int GetSmoothRemoveTerrainIndex()
		{
			return LandscapeTileItemSource.smoothRemoveTerrainIndex;
		}

		// Token: 0x0600198B RID: 6539 RVA: 0x000A717C File Offset: 0x000A617C
		public static string GetRemoveTerrainItem(string continentName)
		{
			return string.Concat(new object[]
			{
				continentName,
				'_',
				LandscapeTileItemSource.layerPrefix,
				'_',
				LandscapeTileItemSource.removeTerrainIndex
			});
		}

		// Token: 0x0600198C RID: 6540 RVA: 0x000A71C4 File Offset: 0x000A61C4
		public static string GetSmoothRemoveTerrainItem(string continentName)
		{
			return string.Concat(new object[]
			{
				continentName,
				'_',
				LandscapeTileItemSource.layerPrefix,
				'_',
				LandscapeTileItemSource.smoothRemoveTerrainIndex
			});
		}

		// Token: 0x0600198D RID: 6541 RVA: 0x000A720C File Offset: 0x000A620C
		public static string GetTileItem(int tile, string continentName)
		{
			return string.Concat(new object[]
			{
				continentName,
				'_',
				LandscapeTileItemSource.layerPrefix,
				'_',
				tile
			});
		}

		// Token: 0x0600198E RID: 6542 RVA: 0x000A7250 File Offset: 0x000A6250
		public static int GetTileIndex(string item)
		{
			if (!string.IsNullOrEmpty(item))
			{
				int charIndex = item.LastIndexOf('_');
				if (charIndex >= 0)
				{
					item = item.Substring(charIndex + 1);
				}
				int index;
				if (int.TryParse(item, out index))
				{
					return index;
				}
			}
			return -1;
		}

		// Token: 0x0600198F RID: 6543 RVA: 0x000A728C File Offset: 0x000A628C
		public void Refresh()
		{
			this.items.Clear();
			IDatabase mainDb = IDatabase.GetMainDatabase();
			IObjMan layersMan = mainDb.GetManipulator(mainDb.GetDBIDByName(this.continentName + "/layers.xdb"));
			if (layersMan != null)
			{
				for (int i = 0; i < 256; i++)
				{
					string field = "Layers.[" + i + "]";
					string objectPropertyName = field + "SymbolName";
					string name;
					layersMan.GetValue(objectPropertyName, out name);
					if (!string.IsNullOrEmpty(name.Trim()))
					{
						this.items.Add(string.Concat(new object[]
						{
							this.continentName,
							'_',
							LandscapeTileItemSource.layerPrefix,
							'_',
							i
						}));
					}
				}
			}
			this.items.Add(LandscapeTileItemSource.GetRemoveTerrainItem(this.continentName));
		}

		// Token: 0x0400104F RID: 4175
		private readonly List<string> items = new List<string>();

		// Token: 0x04001050 RID: 4176
		private readonly string continentName;

		// Token: 0x04001051 RID: 4177
		private static readonly string layerPrefix = "Layer";

		// Token: 0x04001052 RID: 4178
		private static readonly int removeTerrainIndex = 256;

		// Token: 0x04001053 RID: 4179
		private static readonly int smoothRemoveTerrainIndex = 255;
	}
}
