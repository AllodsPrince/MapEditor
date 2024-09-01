using System;
using System.Collections.Generic;
using MapEditor.Map.MapObjects;
using MapEditor.Resources.Strings;
using Tools.MapObjects;
using Tools.Progress;

namespace MapEditor.Map.MapCheckers.SpecificCheckers
{
	// Token: 0x0200010D RID: 269
	public class PositionChecker : MapChecker
	{
		// Token: 0x06000D31 RID: 3377 RVA: 0x0006E75D File Offset: 0x0006D75D
		public PositionChecker()
		{
			base.Name = Strings.POSITION_CHECKER_NAME;
			base.ShortDescription = Strings.POSITION_CHECKER_SHORT_DESCRIPTION;
			base.LongDescription = Strings.POSITION_CHECKER_LONG_DESCRIPTION;
		}

		// Token: 0x06000D32 RID: 3378 RVA: 0x0006E788 File Offset: 0x0006D788
		private int CreateLongInfo(MapEditorMap map)
		{
			base.LongInfoView = new LongInfoViewNode(true);
			base.LongInfoText = string.Empty;
			int count = 0;
			Dictionary<int, int> usedIDs = new Dictionary<int, int>();
			foreach (KeyValuePair<int, IMapObject> keyValuePair in map.MapEditorMapObjectContainer.MapObjects)
			{
				if (!usedIDs.ContainsKey(keyValuePair.Key))
				{
					usedIDs.Add(keyValuePair.Key, 0);
					string position = string.Format("{0:0.###}, {1:0.###}, {2:0.###}", keyValuePair.Value.Position.X, keyValuePair.Value.Position.Y, keyValuePair.Value.Position.Z);
					int objectsCount = 0;
					foreach (KeyValuePair<int, IMapObject> _keyValuePair in map.MapEditorMapObjectContainer.MapObjects)
					{
						if (!usedIDs.ContainsKey(_keyValuePair.Key) && (keyValuePair.Value.Position - _keyValuePair.Value.Position).Length2 < MathConsts.DOUBLE_EPSILON_2)
						{
							usedIDs.Add(_keyValuePair.Key, 0);
							if (objectsCount == 0)
							{
								base.LongInfoView.FindOrAddNode(position, false).AddNode(keyValuePair.Value, false);
								objectsCount++;
							}
							base.LongInfoView.FindOrAddNode(position, false).AddNode(_keyValuePair.Value, false);
							objectsCount++;
						}
					}
					if (objectsCount > 0)
					{
						if (!string.IsNullOrEmpty(base.LongInfoText))
						{
							base.LongInfoText += "\r\n";
						}
						base.LongInfoText += string.Format(Strings.POSITION_CHECKER_LONG_INFO_NOT_UNIQUE, position, objectsCount);
						count++;
					}
				}
			}
			return count;
		}

		// Token: 0x06000D33 RID: 3379 RVA: 0x0006E9BC File Offset: 0x0006D9BC
		public override void Check(MapEditorMap map, IProgressContainer progressContainer)
		{
			if (progressContainer != null)
			{
				progressContainer.AddString(Strings.RUN_POSITION_CHECKER);
			}
			int count = this.CreateLongInfo(map);
			if (count > 0)
			{
				base.Status = MapCheckerStatus.Red;
				base.ShortInfo = Strings.POSITION_CHECKER_SHORT_INFO_NOT_UNIQUE;
				base.LongResult = string.Format(Strings.POSITION_CHECKER_LONG_RESULT_NOT_UNIQUE, count);
			}
			else
			{
				base.Status = MapCheckerStatus.Green;
				base.ShortInfo = Strings.CHECKER_OK;
			}
			base.ShortResult = string.Format("{0}", count);
			if (progressContainer != null)
			{
				progressContainer.Progress++;
			}
		}

		// Token: 0x06000D34 RID: 3380 RVA: 0x0006EA46 File Offset: 0x0006DA46
		public override int GetFixProgressSteps()
		{
			return 1;
		}

		// Token: 0x06000D35 RID: 3381 RVA: 0x0006EA4C File Offset: 0x0006DA4C
		public override void Fix(MapEditorMap map, IProgressContainer progressContainer)
		{
			if (progressContainer != null)
			{
				progressContainer.AddString(Strings.RUN_POSITION_CHECKER_FIX);
			}
			int count = this.CreateLongInfo(map);
			if (count > 0)
			{
				foreach (LongInfoViewNode node in base.LongInfoView.Nodes)
				{
					int nodeCount = node.Nodes.Count;
					if (nodeCount > 1)
					{
						IMapObject mapObject0 = node.Nodes[0].MapObject;
						if (mapObject0 != null && mapObject0.Type.Type == MapObjectFactory.Type.StaticObject)
						{
							bool allTheSame = true;
							for (int nodeIndex = 1; nodeIndex < nodeCount; nodeIndex++)
							{
								IMapObject mapObject = node.Nodes[nodeIndex].MapObject;
								if (mapObject == null || mapObject.Type.Type != MapObjectFactory.Type.StaticObject)
								{
									allTheSame = false;
									break;
								}
								if (!string.Equals(mapObject0.Type.Stats, mapObject.Type.Stats, StringComparison.OrdinalIgnoreCase))
								{
									allTheSame = false;
									break;
								}
							}
							if (allTheSame)
							{
								for (int nodeIndex2 = 1; nodeIndex2 < nodeCount; nodeIndex2++)
								{
									map.MapEditorMapObjectContainer.RemoveMapObject(node.Nodes[nodeIndex2].MapObject);
								}
							}
						}
					}
				}
			}
			count = this.CreateLongInfo(map);
			if (count > 0)
			{
				base.Status = MapCheckerStatus.Red;
				base.ShortInfo = Strings.POSITION_CHECKER_SHORT_INFO_NOT_UNIQUE;
				base.LongResult = string.Format(Strings.POSITION_CHECKER_LONG_RESULT_NOT_UNIQUE, count);
			}
			else
			{
				base.Status = MapCheckerStatus.Green;
				base.ShortInfo = Strings.CHECKER_OK;
			}
			base.ShortResult = string.Format("{0}", count);
			if (progressContainer != null)
			{
				progressContainer.Progress++;
			}
		}
	}
}
