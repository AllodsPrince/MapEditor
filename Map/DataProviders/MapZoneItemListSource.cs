using System;
using System.Collections.Generic;
using Db;

namespace MapEditor.Map.DataProviders
{
	// Token: 0x02000045 RID: 69
	internal class MapZoneItemListSource : ItemList.IItemSource
	{
		// Token: 0x060003D9 RID: 985 RVA: 0x000207E4 File Offset: 0x0001F7E4
		public void Refresh()
		{
			if (!string.IsNullOrEmpty(this.continentName))
			{
				this.items.Clear();
				IDatabase mainDb = IDatabase.GetMainDatabase();
				List<DBID> objectsList = mainDb.GetObjectsList("gameMechanics.map.zone.ZoneResource");
				foreach (DBID dbid in objectsList)
				{
					if (dbid.ToString().Contains(this.continentName + "/Zones"))
					{
						this.items.Add(dbid.ToString());
					}
				}
			}
		}

		// Token: 0x17000094 RID: 148
		// (get) Token: 0x060003DA RID: 986 RVA: 0x00020884 File Offset: 0x0001F884
		public IEnumerable<string> Items
		{
			get
			{
				return this.items;
			}
		}

		// Token: 0x060003DB RID: 987 RVA: 0x0002088C File Offset: 0x0001F88C
		public MapZoneItemListSource(MapEditorMap map)
		{
			this.continentName = map.Data.ContinentName;
			this.Refresh();
		}

		// Token: 0x040002AF RID: 687
		public const string mapZoneType = "gameMechanics.map.zone.ZoneResource";

		// Token: 0x040002B0 RID: 688
		public const string mapZoneFolder = "Zones";

		// Token: 0x040002B1 RID: 689
		private readonly List<string> items = new List<string>();

		// Token: 0x040002B2 RID: 690
		private readonly string continentName = string.Empty;
	}
}
