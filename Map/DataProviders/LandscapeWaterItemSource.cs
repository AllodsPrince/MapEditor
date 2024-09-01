using System;
using System.Collections.Generic;
using Db;

namespace MapEditor.Map.DataProviders
{
	// Token: 0x0200020D RID: 525
	internal class LandscapeWaterItemSource : ItemList.IItemSource
	{
		// Token: 0x1700062E RID: 1582
		// (get) Token: 0x060019AA RID: 6570 RVA: 0x000A759A File Offset: 0x000A659A
		public IEnumerable<string> Items
		{
			get
			{
				return this.items;
			}
		}

		// Token: 0x060019AB RID: 6571 RVA: 0x000A75A4 File Offset: 0x000A65A4
		public LandscapeWaterItemSource(string _continentName)
		{
			this.continentName = _continentName;
			this.layersMan = LandscapeWaterItemSource.mainDb.GetManipulator(LandscapeWaterItemSource.mainDb.GetDBIDByName(this.continentName + "/layers.xdb"));
			this.Refresh();
		}

		// Token: 0x060019AC RID: 6572 RVA: 0x000A75FC File Offset: 0x000A65FC
		public void Refresh()
		{
			this.items.Clear();
			if (this.layersMan != null)
			{
				int cnt;
				this.layersMan.GetValue("waterLayers", out cnt);
				for (int i = 0; i < cnt; i++)
				{
					string field = "waterLayers.[" + i + "]";
					string objectPropertyName = field + "SymbolName";
					string name;
					this.layersMan.GetValue(objectPropertyName, out name);
					if (!string.IsNullOrEmpty(name.Trim()))
					{
						this.items.Add(string.Concat(new object[]
						{
							this.continentName,
							'_',
							"WaterLayer",
							'_',
							i
						}));
					}
				}
			}
		}

		// Token: 0x1700062F RID: 1583
		// (get) Token: 0x060019AD RID: 6573 RVA: 0x000A76CE File Offset: 0x000A66CE
		public DBID LayerDBID
		{
			get
			{
				if (this.layersMan == null)
				{
					return null;
				}
				return this.layersMan.DBID;
			}
		}

		// Token: 0x060019AE RID: 6574 RVA: 0x000A76E8 File Offset: 0x000A66E8
		public static string GetWaterLayerItem(int index, string continentName)
		{
			return string.Concat(new object[]
			{
				continentName,
				'_',
				"WaterLayer",
				'_',
				index
			});
		}

		// Token: 0x060019AF RID: 6575 RVA: 0x000A772C File Offset: 0x000A672C
		public static int GetWaterLayerIndex(string item)
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

		// Token: 0x0400105C RID: 4188
		public const string layerPrefix = "WaterLayer";

		// Token: 0x0400105D RID: 4189
		private readonly List<string> items = new List<string>();

		// Token: 0x0400105E RID: 4190
		private readonly IObjMan layersMan;

		// Token: 0x0400105F RID: 4191
		private static readonly IDatabase mainDb = IDatabase.GetMainDatabase();

		// Token: 0x04001060 RID: 4192
		private readonly string continentName;
	}
}
