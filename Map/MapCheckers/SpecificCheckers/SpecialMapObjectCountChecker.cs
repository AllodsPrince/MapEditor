using System;
using System.Collections.Generic;
using MapEditor.Map.MapObjects;
using MapEditor.Resources.Strings;
using Tools.MapObjects;
using Tools.Progress;

namespace MapEditor.Map.MapCheckers.SpecificCheckers
{
	// Token: 0x020001A8 RID: 424
	public class SpecialMapObjectCountChecker : MapChecker
	{
		// Token: 0x06001483 RID: 5251 RVA: 0x00093F70 File Offset: 0x00092F70
		public SpecialMapObjectCountChecker()
		{
			base.Name = Strings.SERVER_OBJECT_COUNT_CHECKER_NAME;
			base.ShortDescription = string.Format("{0}", SpecialMapObjectCountChecker.yellowCountForPatch);
			base.LongDescription = string.Format(Strings.SERVER_OBJECT_COUNT_CHECKER_LONG_DESCRIPTION, SpecialMapObjectCountChecker.yellowCountForPatch, SpecialMapObjectCountChecker.yellowCount);
		}

		// Token: 0x06001484 RID: 5252 RVA: 0x00093FCC File Offset: 0x00092FCC
		public override void Check(MapEditorMap map, IProgressContainer progressContainer)
		{
			if (progressContainer != null)
			{
				progressContainer.AddString(Strings.RUN_SPECIAL_MAP_OBJECT_COUNT_CHECKER);
			}
			int totalCount = 0;
			List<int[,]> patchCounts = new List<int[,]>();
			List<int> totalCounts = new List<int>();
			List<int> outOfBoundsCounts = new List<int>();
			int[,] array = new int[4, 4];
			int[,] patchCount = array;
			int typesCount = MapObjectFactory.Type.LastSpecialType - MapObjectFactory.Type.FirstSpecialType + 1;
			for (int typeIndex = 0; typeIndex < typesCount; typeIndex++)
			{
				totalCounts.Add(0);
				outOfBoundsCounts.Add(0);
				List<int[,]> list = patchCounts;
				int[,] item = new int[4, 4];
				list.Add(item);
			}
			base.LongInfoView = null;
			foreach (KeyValuePair<int, IMapObject> keyValuePair in map.MapEditorMapObjectContainer.MapObjects)
			{
				IMapObject mapObject = keyValuePair.Value;
				if (mapObject != null && MapObjectFactory.Type.SpecialType(mapObject.Type.Type))
				{
					int typeIndex2 = mapObject.Type.Type - MapObjectFactory.Type.FirstSpecialType;
					if (typeIndex2 >= 0 && typeIndex2 < typesCount)
					{
						totalCount++;
						List<int> list2;
						int index;
						(list2 = totalCounts)[index = typeIndex2] = list2[index] + 1;
						Position pos = mapObject.Position;
						int x;
						int y;
						if (map.Data.GetPatch(ref pos, out x, out y))
						{
							patchCount[x, y]++;
							patchCounts[typeIndex2][x, y]++;
						}
						else
						{
							List<int> list3;
							int index2;
							(list3 = outOfBoundsCounts)[index2 = typeIndex2] = list3[index2] + 1;
							if (base.LongInfoView == null)
							{
								base.LongInfoView = new LongInfoViewNode(true);
							}
							base.LongInfoView.FindOrAddNode(Strings.CLIENT_OBJECT_CHECKER_OUT_OF_BOUNDS_GROUP, false).AddNode(mapObject, false);
						}
					}
				}
			}
			base.Status = MapCheckerStatus.Green;
			for (int x2 = 0; x2 < 4; x2++)
			{
				for (int y2 = 0; y2 < 4; y2++)
				{
					if (patchCount[x2, y2] > SpecialMapObjectCountChecker.redCountForPatch)
					{
						base.Status = MapCheckerStatus.Red;
						break;
					}
					if (base.Status == MapCheckerStatus.Green && patchCount[x2, y2] > SpecialMapObjectCountChecker.yellowCountForPatch)
					{
						base.Status = MapCheckerStatus.Yellow;
					}
				}
				if (base.Status == MapCheckerStatus.Red)
				{
					break;
				}
			}
			base.Status = MapCheckerStatus.Green;
			if (base.Status == MapCheckerStatus.Red)
			{
				base.ShortInfo = Strings.CHECKER_EXCEED_RED;
			}
			else if (base.Status == MapCheckerStatus.Yellow)
			{
				base.ShortInfo = Strings.CHECKER_EXCEED_YELLOW;
			}
			else
			{
				base.ShortInfo = Strings.CHECKER_OK;
			}
			int maxCount = 0;
			for (int x3 = 0; x3 < 4; x3++)
			{
				for (int y3 = 0; y3 < 4; y3++)
				{
					if (patchCount[x3, y3] > maxCount)
					{
						maxCount = patchCount[x3, y3];
					}
				}
			}
			base.ShortResult = string.Format("{0}", maxCount);
			base.LongResult = string.Format(Strings.SERVER_OBJECT_COUNT_CHECKER_LONG_RESULT, totalCount);
			base.LongResult += "\r\n";
			for (int typeIndex3 = 0; typeIndex3 < typesCount; typeIndex3++)
			{
				base.LongResult += "\r\n";
				base.LongResult += string.Format("{0}: {1}", MapObjectFactory.Type.TypeToSeveralObjectsTypeName(typeIndex3 + MapObjectFactory.Type.FirstSpecialType), totalCounts[typeIndex3]);
			}
			base.LongResult += "\r\n\r\n";
			base.LongResult += MapCheckerHelper.FormatTable(patchCount, SpecialMapObjectCountChecker.yellowCountForPatch, SpecialMapObjectCountChecker.redCountForPatch);
			int outOfBoundsCount = (base.LongInfoView != null) ? base.LongInfoView.GetBranchCountByChild(Strings.CLIENT_OBJECT_CHECKER_OUT_OF_BOUNDS_GROUP) : 0;
			if (outOfBoundsCount > 0)
			{
				base.LongResult += "\r\n\r\n";
				base.LongResult += string.Format(Strings.SERVER_OBJECT_COUNT_CHECKER_LONG_RESULT_OUT_OF_BOUNDS, outOfBoundsCount);
			}
			base.LongInfoText = string.Empty;
			for (int typeIndex4 = 0; typeIndex4 < typesCount; typeIndex4++)
			{
				if (!string.IsNullOrEmpty(base.LongInfoText))
				{
					base.LongInfoText += "\r\n\r\n";
				}
				base.LongInfoText += string.Format("{0}: {1}", MapObjectFactory.Type.TypeToSeveralObjectsTypeName(typeIndex4 + MapObjectFactory.Type.FirstSpecialType), totalCounts[typeIndex4]);
				if (totalCounts[typeIndex4] > 0)
				{
					base.LongInfoText += "\r\n";
					base.LongInfoText += MapCheckerHelper.FormatTable(patchCounts[typeIndex4], SpecialMapObjectCountChecker.yellowCountForPatch, SpecialMapObjectCountChecker.redCountForPatch);
				}
				if (outOfBoundsCounts[typeIndex4] > 0)
				{
					base.LongInfoText += "\r\n";
					base.LongInfoText += string.Format(Strings.SERVER_OBJECT_COUNT_CHECKER_LONG_RESULT_OUT_OF_BOUNDS, outOfBoundsCounts[typeIndex4]);
				}
			}
			if (progressContainer != null)
			{
				progressContainer.Progress++;
			}
		}

		// Token: 0x04000E64 RID: 3684
		private static readonly int yellowCountForPatch = 200;

		// Token: 0x04000E65 RID: 3685
		private static readonly int redCountForPatch = 300;

		// Token: 0x04000E66 RID: 3686
		private static readonly int yellowCount = 3200;
	}
}
