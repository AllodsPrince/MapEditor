using System;
using System.Collections.Generic;
using MapEditor.Map.MapObjects;
using MapEditor.Resources.Strings;
using Tools.MapObjects;
using Tools.Progress;

namespace MapEditor.Map.MapCheckers.SpecificCheckers
{
	// Token: 0x02000075 RID: 117
	public class SpawnPointTypeChecker : MapChecker
	{
		// Token: 0x060005BD RID: 1469 RVA: 0x0002FCC8 File Offset: 0x0002ECC8
		public SpawnPointTypeChecker()
		{
			base.Name = Strings.SPAWN_POINT_TYPE_CHECKER_NAME;
			base.ShortDescription = string.Format("{0}", SpawnPointTypeChecker.yellowCount);
			base.LongDescription = string.Format(Strings.SPAWN_POINT_TYPE_CHECKER_LONG_DESCRIPTION, SpawnPointTypeChecker.yellowCount);
		}

		// Token: 0x060005BE RID: 1470 RVA: 0x0002FD1C File Offset: 0x0002ED1C
		public override void Check(MapEditorMap map, IProgressContainer progressContainer)
		{
			if (progressContainer != null)
			{
				progressContainer.AddString(Strings.RUN_SPAWN_POINT_TYPE_CHECKER);
			}
			base.LongInfoView = new LongInfoViewNode(false);
			foreach (KeyValuePair<int, IMapObject> keyValuePair in map.MapEditorMapObjectContainer.SpawnPointContainer.MapObjects)
			{
				SpawnPoint spawnPoint = keyValuePair.Value as SpawnPoint;
				if (spawnPoint != null && !string.IsNullOrEmpty(spawnPoint.SpawnTable))
				{
					string type = null;
					switch (spawnPoint.SpawnTableType)
					{
					case SpawnTableType.Table:
						type = Strings.SPAWN_POINT_OBJECT_TYPE_TABLE;
						break;
					case SpawnTableType.SingleMob:
						type = Strings.SPAWN_POINT_OBJECT_TYPE_SINGLE_MOB;
						break;
					case SpawnTableType.SingleDevice:
						type = Strings.SPAWN_POINT_OBJECT_TYPE_SINGLE_DEVICE;
						break;
					case SpawnTableType.AstralMob:
						type = Strings.SPAWN_POINT_OBJECT_TYPE_ASTRAL_MOB;
						break;
					case SpawnTableType.AstralWreck:
						type = Strings.SPAWN_POINT_OBJECT_TYPE_ASTRAL_WRECK;
						break;
					case SpawnTableType.AstralTeleport:
						type = Strings.SPAWN_POINT_OBJECT_TYPE_ASTRAL_TELEPORT;
						break;
					}
					if (type != null)
					{
						base.LongInfoView.FindOrAddNode(type, false).FindOrAddNode(spawnPoint.SpawnTable, false).AddNode(spawnPoint, false);
					}
				}
			}
			int tablesCount = base.LongInfoView.GetBranchCountByChild(Strings.SPAWN_POINT_OBJECT_TYPE_TABLE);
			int mobsCount = base.LongInfoView.GetBranchCountByChild(Strings.SPAWN_POINT_OBJECT_TYPE_SINGLE_MOB);
			int devicesCount = base.LongInfoView.GetBranchCountByChild(Strings.SPAWN_POINT_OBJECT_TYPE_SINGLE_DEVICE);
			int astralMobsCount = base.LongInfoView.GetBranchCountByChild(Strings.SPAWN_POINT_OBJECT_TYPE_ASTRAL_MOB);
			int astralWrecksCount = base.LongInfoView.GetBranchCountByChild(Strings.SPAWN_POINT_OBJECT_TYPE_ASTRAL_WRECK);
			int astralTerleportsCount = base.LongInfoView.GetBranchCountByChild(Strings.SPAWN_POINT_OBJECT_TYPE_ASTRAL_TELEPORT);
			int totalCount = tablesCount + mobsCount + devicesCount + astralMobsCount + astralWrecksCount + astralTerleportsCount;
			if (totalCount > SpawnPointTypeChecker.redCount)
			{
				base.Status = MapCheckerStatus.Red;
				base.ShortInfo = Strings.CHECKER_EXCEED_RED;
			}
			else if (totalCount > SpawnPointTypeChecker.yellowCount)
			{
				base.Status = MapCheckerStatus.Yellow;
				base.ShortInfo = Strings.CHECKER_EXCEED_YELLOW;
			}
			else
			{
				base.Status = MapCheckerStatus.Green;
				base.ShortInfo = Strings.CHECKER_OK;
			}
			base.Status = MapCheckerStatus.Green;
			base.ShortInfo = Strings.CHECKER_OK;
			base.ShortResult = string.Format("{0}", totalCount);
			base.LongResult = string.Format(Strings.SPAWN_POINT_TYPE_CHECKER_LONG_RESULT, new object[]
			{
				totalCount,
				tablesCount,
				mobsCount,
				devicesCount,
				astralMobsCount,
				astralWrecksCount,
				astralTerleportsCount
			});
			if (progressContainer != null)
			{
				progressContainer.Progress++;
			}
		}

		// Token: 0x04000450 RID: 1104
		private static readonly int yellowCount = 100;

		// Token: 0x04000451 RID: 1105
		private static readonly int redCount = 150;
	}
}
