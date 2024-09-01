using System;
using System.Collections.Generic;
using Db;

namespace MapEditor.Map.DataProviders
{
	// Token: 0x020001AA RID: 426
	internal class LightItemListSource : ItemList.IItemSource
	{
		// Token: 0x0600148F RID: 5263 RVA: 0x000949A0 File Offset: 0x000939A0
		public void Refresh()
		{
			if (!string.IsNullOrEmpty(this.continentName))
			{
				this.items.Clear();
				IDatabase mainDb = IDatabase.GetMainDatabase();
				List<DBID> objectsList = mainDb.GetObjectsList("ZoneLights");
				foreach (DBID dbid in objectsList)
				{
					if (dbid.ToString().Contains(this.continentName + "/ZoneLights"))
					{
						this.items.Add(dbid.ToString());
					}
				}
			}
		}

		// Token: 0x06001490 RID: 5264 RVA: 0x00094A40 File Offset: 0x00093A40
		public LightItemListSource(MapEditorMap map)
		{
			this.continentName = map.Data.ContinentName;
			this.Refresh();
		}

		// Token: 0x1700040F RID: 1039
		// (get) Token: 0x06001491 RID: 5265 RVA: 0x00094A75 File Offset: 0x00093A75
		public IEnumerable<string> Items
		{
			get
			{
				return this.items;
			}
		}

		// Token: 0x04000E75 RID: 3701
		public const string lightType = "ZoneLights";

		// Token: 0x04000E76 RID: 3702
		public const string lightFolder = "ZoneLights";

		// Token: 0x04000E77 RID: 3703
		private readonly List<string> items = new List<string>();

		// Token: 0x04000E78 RID: 3704
		private readonly string continentName = string.Empty;
	}
}
