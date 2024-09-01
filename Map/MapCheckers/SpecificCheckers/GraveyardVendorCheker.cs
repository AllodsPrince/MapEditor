using System;
using System.Collections.Generic;
using Db;
using MapEditor.Map.MapObjects;
using Tools.MapObjects;
using Tools.Progress;

namespace MapEditor.Map.MapCheckers.SpecificCheckers
{
	// Token: 0x0200015A RID: 346
	internal class GraveyardVendorCheker : MapChecker
	{
		// Token: 0x0600109F RID: 4255 RVA: 0x0007C815 File Offset: 0x0007B815
		public GraveyardVendorCheker()
		{
			base.Name = "Продавцы миры";
			base.ShortDescription = "продавцы миры рядом с кладбищем";
			base.LongDescription = "В  радиусе 20 метров от кладбища должен быть продавец миры.";
		}

		// Token: 0x060010A0 RID: 4256 RVA: 0x0007C848 File Offset: 0x0007B848
		public override void Check(MapEditorMap map, IProgressContainer progressContainer)
		{
			base.Status = MapCheckerStatus.Green;
			foreach (KeyValuePair<int, IMapObject> mapObject in map.MapEditorMapObjectContainer.GraveyardContainer.MapObjects)
			{
				Graveyard gy = mapObject.Value as Graveyard;
				if (gy != null)
				{
					IDatabase mainDb = IDatabase.GetMainDatabase();
					foreach (IMapObject mapObject2 in map.MapEditorMapObjectContainer.SpawnPointContainer.MapObjects.Values)
					{
						MapObject obj = (MapObject)mapObject2;
						if ((obj.Position - gy.Position).Length2 > (double)(this.radius * this.radius))
						{
							try
							{
								DBID t = mainDb.GetDBIDByName(obj.SceneName);
								IObjMan tb = mainDb.GetManipulator(t);
								DBID vendTable;
								tb.GetValue("interactions.vendorTable", out vendTable);
								if (vendTable != null)
								{
									return;
								}
							}
							catch (Exception)
							{
							}
						}
					}
					base.Status = MapCheckerStatus.Red;
				}
			}
		}

		// Token: 0x04000C30 RID: 3120
		private int radius = 20;
	}
}
