using System;
using System.Collections.Generic;
using Db;
using MapEditor.Map.MapObjects;
using MapEditor.Resources.Strings;
using Tools.MapObjects;
using Tools.Progress;

namespace MapEditor.Map.MapCheckers.SpecificCheckers
{
	// Token: 0x02000069 RID: 105
	public class MapObjectTypeChecker : MapChecker
	{
		// Token: 0x06000521 RID: 1313 RVA: 0x000292AC File Offset: 0x000282AC
		public MapObjectTypeChecker()
		{
			base.Name = Strings.CLIENT_OBJECT_TYPE_CHECKER_NAME;
			base.ShortDescription = string.Format("{0}", MapObjectTypeChecker.yellowCount);
			base.LongDescription = string.Format(Strings.CLIENT_OBJECT_TYPE_CHECKER_LONG_DESCRIPTION, MapObjectTypeChecker.yellowCount);
		}

		// Token: 0x06000522 RID: 1314 RVA: 0x00029300 File Offset: 0x00028300
		public override void Check(MapEditorMap map, IProgressContainer progressContainer)
		{
			if (progressContainer != null)
			{
				progressContainer.AddString(Strings.RUN_MAP_OBJECT_TYPE_CHECKER);
			}
			IDatabase mainDb = IDatabase.GetMainDatabase();
			base.LongInfoView = new LongInfoViewNode(true);
			foreach (KeyValuePair<int, IMapObject> keyValuePair in map.MapEditorMapObjectContainer.MapObjects)
			{
				if (keyValuePair.Value.Type.Type == MapObjectFactory.Type.StaticObject)
				{
					StaticObject staticObject = keyValuePair.Value as StaticObject;
					if (staticObject != null)
					{
						base.LongInfoView.FindOrAddNode(staticObject.SceneName, false).AddNode(staticObject, false);
						DBID objectDBID = mainDb.GetDBIDByName(staticObject.SceneName);
						if (mainDb.GetClassTypeName(objectDBID) == StaticObject.DBType)
						{
							IObjMan objectMan = mainDb.GetManipulator(objectDBID);
							if (objectMan != null)
							{
								int parts;
								objectMan.GetValue("parts", out parts);
								for (int index = 0; index < parts; index++)
								{
									DBID additionalPartDBID;
									objectMan.GetValue(string.Format("parts.[{0}].", index) + "StaticObjectTemplate", out additionalPartDBID);
									string sceneName = additionalPartDBID.ToString();
									base.LongInfoView.FindOrAddNode(sceneName, false).AddNode(staticObject, string.Format(Strings.MAP_OBJECT_TYPE_CHECKER_ADDITIONAL_PART_ELEM, sceneName, staticObject.SceneName), false);
								}
							}
						}
					}
				}
			}
			int count = base.LongInfoView.BranchCount;
			if (count > MapObjectTypeChecker.redCount)
			{
				base.Status = MapCheckerStatus.Red;
				base.ShortInfo = Strings.CHECKER_EXCEED_RED;
			}
			else if (count > MapObjectTypeChecker.yellowCount)
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
			base.LongResult = string.Format(Strings.CLIENT_OBJECT_TYPE_LONG_RESULT, count);
			if (progressContainer != null)
			{
				progressContainer.Progress++;
			}
		}

		// Token: 0x040003B1 RID: 945
		private static readonly int yellowCount = 100;

		// Token: 0x040003B2 RID: 946
		private static readonly int redCount = 150;
	}
}
