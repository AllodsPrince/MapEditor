using System;
using System.Collections.Generic;
using Db;
using Db.Main;
using MapEditor.Map.MapObjects;
using MapEditor.Resources.Strings;
using Tools.Progress;
using Tools.SafeObjMan;

namespace MapEditor.Map.SaveLoad.DataSources
{
	// Token: 0x02000225 RID: 549
	internal class MapResourceDataSource : SaveLoad.IDataSource
	{
		// Token: 0x06001A7A RID: 6778 RVA: 0x000AE721 File Offset: 0x000AD721
		public int GetProgressSteps(bool forSave)
		{
			return 1;
		}

		// Token: 0x06001A7B RID: 6779 RVA: 0x000AE724 File Offset: 0x000AD724
		public bool Save(MapEditorMap map, MainForm.Context context, IProgressContainer progressContainer)
		{
			if (progressContainer != null)
			{
				progressContainer.AddString(Strings.SAVING_MAP_RESOURCE);
			}
			IDatabase mainDb = IDatabase.GetMainDatabase();
			string mapResourceName = map.Data.MapResourceName;
			DBID mapResourceDBID = mainDb.GetDBIDByName(mapResourceName);
			IObjMan mapResourceMan = mainDb.GetManipulator(mapResourceDBID);
			if (mapResourceMan != null)
			{
				DBItemSource tourSource = new DBItemSource(EditorEnvironment.DataFolder + Constants.ContinentFolder(map.Data.ContinentName) + RoutePoint.TourFolder, RoutePoint.TourDBType, false, true);
				IEnumerable<string> tours = tourSource.Items;
				ObjMan.StartMassEditing();
				SafeObjMan.SetInt(mapResourceMan, "globalObjects", 0);
				int index = 0;
				foreach (string tour in tours)
				{
					mapResourceMan.Insert("globalObjects", index);
					SafeObjMan.SetDBID(mapResourceMan, string.Format("globalObjects.[{0}]", index), tour);
					index++;
				}
				ObjMan.StopMassEditing();
			}
			if (progressContainer != null)
			{
				progressContainer.Progress++;
			}
			return true;
		}

		// Token: 0x06001A7C RID: 6780 RVA: 0x000AE830 File Offset: 0x000AD830
		public bool Load(MapEditorMap map, MainForm.Context context, IProgressContainer progressContainer, out bool somethingCreated)
		{
			somethingCreated = false;
			if (progressContainer != null)
			{
				progressContainer.AddString(Strings.LOADING_MAP_RESOURCE);
			}
			if (progressContainer != null)
			{
				progressContainer.Progress++;
			}
			return true;
		}
	}
}
