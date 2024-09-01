using System;
using System.Collections.Generic;
using Db;
using Db.Main;
using MapEditor.Map.Containers;
using MapEditor.Map.MapObjects;
using MapEditor.Resources.Strings;
using Tools.MapObjects;
using Tools.Progress;
using Tools.SafeObjMan;

namespace MapEditor.Map.SaveLoad.DataSources
{
	// Token: 0x02000026 RID: 38
	internal class StartPointsDataSource : SaveLoad.IDataSource
	{
		// Token: 0x060002BA RID: 698 RVA: 0x0001D68A File Offset: 0x0001C68A
		public int GetProgressSteps(bool forSave)
		{
			if (forSave)
			{
				return 1;
			}
			return 1;
		}

		// Token: 0x060002BB RID: 699 RVA: 0x0001D694 File Offset: 0x0001C694
		public bool Save(MapEditorMap map, MainForm.Context context, IProgressContainer progressContainer)
		{
			if (progressContainer != null)
			{
				progressContainer.AddString(Strings.SAVING_START_POINTS);
			}
			IDatabase mainDb = IDatabase.GetMainDatabase();
			if (mainDb != null)
			{
				ObjMan.StartMassEditing();
				StartPointContainer startPointContainer = map.MapEditorMapObjectContainer.StartPointContainer;
				if (startPointContainer != null)
				{
					foreach (KeyValuePair<int, IMapObject> keyValuePair in startPointContainer.MapObjects)
					{
						StartPoint startPoint = keyValuePair.Value as StartPoint;
						if (startPoint != null && !startPoint.Temporary)
						{
							DBID characterDBID = mainDb.GetDBIDByName(startPoint.Character);
							IObjMan characterMan = mainDb.GetManipulator(characterDBID);
							SafeObjMan.SetPosition(characterMan, "coord", startPoint.Position);
							SafeObjMan.SetFloat(characterMan, "yaw", startPoint.Rotation.Yaw);
							SafeObjMan.SetDBID(characterMan, "map", map.Data.MapResourceName);
						}
					}
				}
				ObjMan.StopMassEditing();
			}
			if (progressContainer != null)
			{
				progressContainer.Progress++;
			}
			return true;
		}

		// Token: 0x060002BC RID: 700 RVA: 0x0001D7A0 File Offset: 0x0001C7A0
		public bool Load(MapEditorMap map, MainForm.Context context, IProgressContainer progressContainer, out bool somethingCreated)
		{
			somethingCreated = false;
			if (progressContainer != null)
			{
				progressContainer.AddString(Strings.LOADING_START_POINTS);
			}
			IDatabase mainDb = IDatabase.GetMainDatabase();
			if (mainDb != null)
			{
				string mapName = map.Data.MapResourceName;
				DBItemSource startPointItemListSource = new DBItemSource(string.Empty, StartPoint.CharacterDBType, false, true);
				IEnumerable<string> characters = startPointItemListSource.Items;
				foreach (string character in characters)
				{
					DBID characterDBID = mainDb.GetDBIDByName(character);
					IObjMan characterMan = mainDb.GetManipulator(characterDBID);
					if (characterMan != null)
					{
						string _mapName = SafeObjMan.GetDBID(characterMan, "map");
						if (string.Equals(_mapName, mapName, StringComparison.OrdinalIgnoreCase))
						{
							Position position = SafeObjMan.GetPosition(characterMan, "coord");
							Rotation rotation = new Rotation(SafeObjMan.GetFloat(characterMan, "yaw"), Rotation.Empty.Pitch, Rotation.Empty.Roll);
							if ((double)(map.Data.MinXMinYPatchCoords.X * Constants.PatchSize) <= position.X && position.X <= (double)((map.Data.MinXMinYPatchCoords.X + map.Data.MapSize.X) * Constants.PatchSize) && (double)(map.Data.MinXMinYPatchCoords.Y * Constants.PatchSize) <= position.Y && position.Y <= (double)((map.Data.MinXMinYPatchCoords.Y + map.Data.MapSize.Y) * Constants.PatchSize))
							{
								map.MapEditorMapObjectContainer.AddMapObject(new MapObjectType(MapObjectFactory.Type.StartPoint, character), false, position, rotation);
							}
						}
					}
				}
			}
			if (progressContainer != null)
			{
				progressContainer.Progress++;
			}
			return true;
		}
	}
}
