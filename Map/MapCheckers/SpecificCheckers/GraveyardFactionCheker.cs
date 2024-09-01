using System;
using System.Collections.Generic;
using MapEditor.Map.MapObjects;
using MapEditor.Resources.Strings;
using Tools.MapObjects;
using Tools.Progress;

namespace MapEditor.Map.MapCheckers.SpecificCheckers
{
	// Token: 0x02000092 RID: 146
	internal class GraveyardFactionCheker : MapChecker
	{
		// Token: 0x060006EB RID: 1771 RVA: 0x0003637E File Offset: 0x0003537E
		public GraveyardFactionCheker()
		{
			base.Name = "Совместные кладбища";
			base.LongDescription = "Производит проверку всех кладбищей на зоне. В случае, если присутствует хотя бы одно кладбище с пометкой Faction равной Undefined, статус будет красный ";
			base.ShortDescription = "Запрет на создание совместных кладбищей";
		}

		// Token: 0x060006EC RID: 1772 RVA: 0x000363A8 File Offset: 0x000353A8
		public override void Check(MapEditorMap map, IProgressContainer progressContainer)
		{
			this.Clear();
			if (progressContainer != null)
			{
				progressContainer.AddString(Strings.GRAVEYARD_CHEKER_START);
			}
			foreach (KeyValuePair<int, IMapObject> graveyard in map.MapEditorMapObjectContainer.GraveyardContainer.MapObjects)
			{
				Graveyard gy = graveyard.Value as Graveyard;
				if (gy != null && gy.Faction == Faction.Undefined)
				{
					base.Status = MapCheckerStatus.Red;
					this.AddToResult(gy.DefaultSceneName);
					this.BadCount++;
				}
				this.Count++;
			}
			base.ShortResult = this.Count.ToString();
			if (progressContainer != null)
			{
				progressContainer.Progress++;
			}
		}

		// Token: 0x060006ED RID: 1773 RVA: 0x0003647C File Offset: 0x0003547C
		private void Clear()
		{
			base.Status = MapCheckerStatus.Green;
			base.ShortResult = "";
			base.LongInfoText = "";
		}

		// Token: 0x060006EE RID: 1774 RVA: 0x0003649B File Offset: 0x0003549B
		private void AddToResult(string message)
		{
			base.LongInfoText = base.LongInfoText + "\n" + message;
		}

		// Token: 0x040004F5 RID: 1269
		private int Count;

		// Token: 0x040004F6 RID: 1270
		private int BadCount;
	}
}
