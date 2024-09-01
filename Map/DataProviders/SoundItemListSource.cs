using System;
using System.Collections.Generic;
using Db;

namespace MapEditor.Map.DataProviders
{
	// Token: 0x02000236 RID: 566
	internal class SoundItemListSource : ItemList.IItemSource
	{
		// Token: 0x06001B10 RID: 6928 RVA: 0x000B0150 File Offset: 0x000AF150
		public void Refresh()
		{
			if (!string.IsNullOrEmpty(this.continentName))
			{
				this.items.Clear();
				List<DBID> objectsList = SoundItemListSource.mainDb.GetObjectsList("Sound2DTassel");
				foreach (DBID dbid in objectsList)
				{
					if (dbid.ToString().Contains(this.continentName + "/Sound"))
					{
						this.items.Add(dbid.ToString());
					}
				}
			}
		}

		// Token: 0x06001B11 RID: 6929 RVA: 0x000B01F0 File Offset: 0x000AF1F0
		public SoundItemListSource(MapEditorMap map)
		{
			this.continentName = map.Data.ContinentName;
			this.Refresh();
		}

		// Token: 0x1700066D RID: 1645
		// (get) Token: 0x06001B12 RID: 6930 RVA: 0x000B0225 File Offset: 0x000AF225
		public IEnumerable<string> Items
		{
			get
			{
				return this.items;
			}
		}

		// Token: 0x04001152 RID: 4434
		public const string soundFolder = "Sound";

		// Token: 0x04001153 RID: 4435
		public const string soundType = "Sound2DTassel";

		// Token: 0x04001154 RID: 4436
		private static readonly IDatabase mainDb = IDatabase.GetMainDatabase();

		// Token: 0x04001155 RID: 4437
		private readonly List<string> items = new List<string>();

		// Token: 0x04001156 RID: 4438
		private readonly string continentName = string.Empty;
	}
}
