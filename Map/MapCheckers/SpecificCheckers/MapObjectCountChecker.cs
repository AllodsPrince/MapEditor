using System;
using System.Collections.Generic;
using Db;
using MapEditor.Map.MapObjects;
using MapEditor.Resources.Strings;
using Tools.MapObjects;
using Tools.Progress;

namespace MapEditor.Map.MapCheckers.SpecificCheckers
{
	// Token: 0x02000044 RID: 68
	public class MapObjectCountChecker : MapChecker
	{
		// Token: 0x060003D6 RID: 982 RVA: 0x00020418 File Offset: 0x0001F418
		public MapObjectCountChecker()
		{
			base.Name = Strings.CLIENT_OBJECT_COUNT_CHECKER_NAME;
			base.ShortDescription = string.Format("{0}", MapObjectCountChecker.yellowCountForPatch);
			base.LongDescription = string.Format(Strings.CLIENT_OBJECT_COUNT_CHECKER_LONG_DESCRIPTION, MapObjectCountChecker.yellowCountForPatch, MapObjectCountChecker.yellowCount);
		}

		// Token: 0x060003D7 RID: 983 RVA: 0x00020474 File Offset: 0x0001F474
		public override void Check(MapEditorMap map, IProgressContainer progressContainer)
		{
			if (progressContainer != null)
			{
				progressContainer.AddString(Strings.RUN_MAP_OBJECT_COUNT_CHECKER);
			}
			IDatabase mainDb = IDatabase.GetMainDatabase();
			int[,] array = new int[4, 4];
			int[,] patchCount = array;
			int totalCount = 0;
			int outOfBoundsCount = 0;
			base.LongInfoView = new LongInfoViewNode(true);
			foreach (KeyValuePair<int, IMapObject> keyValuePair in map.MapEditorMapObjectContainer.MapObjects)
			{
				if (keyValuePair.Value.Type.Type == MapObjectFactory.Type.StaticObject)
				{
					StaticObject staticObject = keyValuePair.Value as StaticObject;
					if (staticObject != null)
					{
						int additionalParts = 0;
						DBID objectDBID = mainDb.GetDBIDByName(staticObject.SceneName);
						if (mainDb.GetClassTypeName(objectDBID) == StaticObject.DBType)
						{
							IObjMan objectMan = mainDb.GetManipulator(objectDBID);
							if (objectMan != null)
							{
								objectMan.GetValue("parts", out additionalParts);
							}
							int count = 1 + additionalParts;
							totalCount += count;
							Position pos = staticObject.Position;
							int x;
							int y;
							if (map.Data.GetPatch(ref pos, out x, out y))
							{
								patchCount[x, y] += count;
							}
							else
							{
								outOfBoundsCount += count;
								base.LongInfoView.FindOrAddNode(Strings.CLIENT_OBJECT_CHECKER_OUT_OF_BOUNDS_GROUP, false).AddNode(staticObject, string.Format(Strings.CLIENT_OBJECT_COUNT_CHECKER_VIEW_ELEM_TEXT, staticObject.SceneName, count), false);
							}
							if (additionalParts > 0)
							{
								base.LongInfoView.FindOrAddNode(Strings.CLIENT_OBJECT_CHECKER_MULTIOBJECT_GROUP, false).AddNode(staticObject, string.Format(Strings.CLIENT_OBJECT_COUNT_CHECKER_VIEW_ELEM_TEXT, staticObject.SceneName, count), false);
							}
						}
					}
				}
			}
			base.Status = MapCheckerStatus.Green;
			for (int x2 = 0; x2 < 4; x2++)
			{
				for (int y2 = 0; y2 < 4; y2++)
				{
					if (patchCount[x2, y2] > MapObjectCountChecker.redCountForPatch)
					{
						base.Status = MapCheckerStatus.Red;
						break;
					}
					if (base.Status == MapCheckerStatus.Green && patchCount[x2, y2] > MapObjectCountChecker.yellowCountForPatch)
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
			base.LongResult = string.Format(Strings.CLIENT_OBJECT_COUNT_CHECKER_LONG_RESULT, totalCount);
			base.LongResult += "\r\n\r\n";
			base.LongResult += MapCheckerHelper.FormatTable(patchCount, MapObjectCountChecker.yellowCountForPatch, MapObjectCountChecker.redCountForPatch);
			if (outOfBoundsCount > 0)
			{
				base.LongResult += "\r\n\r\n";
				base.LongResult += string.Format(Strings.CLIENT_OBJECT_COUNT_CHECKER_LONG_RESULT_OUT_OF_BOUNDS, outOfBoundsCount);
			}
			if (progressContainer != null)
			{
				progressContainer.Progress++;
			}
		}

		// Token: 0x040002AC RID: 684
		private static readonly int yellowCountForPatch = 300;

		// Token: 0x040002AD RID: 685
		private static readonly int redCountForPatch = 500;

		// Token: 0x040002AE RID: 686
		private static readonly int yellowCount = 4800;
	}
}
