using System;
using System.Collections.Generic;
using MapEditor.Map.MapObjects;
using MapEditor.Resources.Strings;
using Tools.MapObjects;
using Tools.Progress;

namespace MapEditor.Map.MapCheckers.SpecificCheckers
{
	// Token: 0x02000043 RID: 67
	public class PatrolNodeTypeChecker : MapChecker
	{
		// Token: 0x060003D3 RID: 979 RVA: 0x000201E0 File Offset: 0x0001F1E0
		public PatrolNodeTypeChecker()
		{
			base.Name = Strings.PATROL_SCRIPT_CHECKER_NAME;
			base.ShortDescription = string.Format("{0}", PatrolNodeTypeChecker.yellowCount);
			base.LongDescription = string.Format(Strings.PATROL_SCRIPT_CHECKER_LONG_DESCRIPTION, PatrolNodeTypeChecker.yellowCount);
		}

		// Token: 0x060003D4 RID: 980 RVA: 0x00020234 File Offset: 0x0001F234
		public override void Check(MapEditorMap map, IProgressContainer progressContainer)
		{
			if (progressContainer != null)
			{
				progressContainer.AddString(Strings.RUN_PATROL_NODE_TYPE_CHECKER);
			}
			base.LongInfoView = new LongInfoViewNode(true);
			foreach (KeyValuePair<int, IMapObject> keyValuePair in map.MapEditorMapObjectContainer.SpawnPointContainer.MapObjects)
			{
				SpawnPoint spawnPoint = keyValuePair.Value as SpawnPoint;
				if (spawnPoint != null)
				{
					string script = spawnPoint.GetScript();
					if (!string.IsNullOrEmpty(script))
					{
						base.LongInfoView.FindOrAddNode(script, false).AddNode(spawnPoint, false);
					}
				}
			}
			foreach (KeyValuePair<int, IMapObject> keyValuePair2 in map.MapEditorMapObjectContainer.PatrolNodeContainer.MapObjects)
			{
				PatrolNode patrolNode = keyValuePair2.Value as PatrolNode;
				if (patrolNode != null)
				{
					string script2 = patrolNode.GetScript();
					if (!string.IsNullOrEmpty(script2))
					{
						base.LongInfoView.FindOrAddNode(script2, false).AddNode(patrolNode, false);
					}
				}
			}
			int count = base.LongInfoView.BranchCount;
			if (count > PatrolNodeTypeChecker.redCount)
			{
				base.Status = MapCheckerStatus.Red;
				base.ShortInfo = Strings.CHECKER_EXCEED_RED;
			}
			else if (count > PatrolNodeTypeChecker.yellowCount)
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
			base.ShortResult = string.Format("{0}", count);
			base.LongResult = string.Format(Strings.PATROL_SCRIPT_CHECKER_LONG_RESULT, count);
			if (progressContainer != null)
			{
				progressContainer.Progress++;
			}
		}

		// Token: 0x040002AA RID: 682
		private static readonly int yellowCount = 100;

		// Token: 0x040002AB RID: 683
		private static readonly int redCount = 150;
	}
}
