using System;
using System.Collections.Generic;
using Db;
using Db.Main;
using MapEditor.Map.Containers;
using MapEditor.Map.MapObjects;
using MapEditor.Resources.Strings;
using Tools.Geometry;
using Tools.MapObjects;
using Tools.Progress;
using Tools.SafeObjMan;

namespace MapEditor.Map.SaveLoad.DataSources
{
	// Token: 0x02000023 RID: 35
	internal class ZoneLocatorsDataSource : SaveLoad.IDataSource
	{
		// Token: 0x060002AC RID: 684 RVA: 0x0001D1DC File Offset: 0x0001C1DC
		private static bool ApplyToAllPatches(MapEditorMap map, MainForm.Context context, ZoneLocatorsDataSource.PatchMethod patchMethod)
		{
			bool result = true;
			for (int x = 0; x < map.Data.MapSize.X; x++)
			{
				for (int y = 0; y < map.Data.MapSize.Y; y++)
				{
					if (!patchMethod(map, context, new Point(map.Data.MinXMinYPatchCoords.X + x, map.Data.MinXMinYPatchCoords.Y + y)))
					{
						result = false;
					}
				}
			}
			return result;
		}

		// Token: 0x060002AD RID: 685 RVA: 0x0001D268 File Offset: 0x0001C268
		private bool LoadMapRegion(MapEditorMap map, MainForm.Context context, Point patchIndex)
		{
			Position additionalPosition = Constants.PatchMinXMinY(patchIndex);
			DBID mapRegionDBID = this.mainDb.GetDBIDByName(Constants.PatchFolder(map.Data.ContinentName, patchIndex) + ZoneLocatorsDataSource.mapRegionFileName);
			IObjMan mapRegionMan = this.mainDb.GetManipulator(mapRegionDBID);
			if (mapRegionMan != null)
			{
				int count = SafeObjMan.GetInt(mapRegionMan, "Areas");
				for (int index = 0; index < count; index++)
				{
					string objectPropertyName = string.Format("Areas.[{0}].", index);
					MapObjectCreationInfo info = new MapObjectCreationInfo();
					info.Position = SafeObjMan.GetPositionCase(mapRegionMan, objectPropertyName + "position") + additionalPosition;
					string mapZone = SafeObjMan.GetDBID(mapRegionMan, objectPropertyName + "zone");
					map.MapEditorMapObjectContainer.AddMapObject(new MapObjectType(MapObjectFactory.Type.ZoneLocator, mapZone), false, info);
				}
			}
			return true;
		}

		// Token: 0x060002AE RID: 686 RVA: 0x0001D33C File Offset: 0x0001C33C
		private bool RecreateMapRegion(MapEditorMap map, MainForm.Context context, Point patchIndex)
		{
			string mapRegionFolder = Constants.PatchFolder(map.Data.ContinentName, patchIndex);
			DBID mapRegionDBID = IDatabase.CreateDBIDByName(mapRegionFolder + ZoneLocatorsDataSource.mapRegionFileName);
			IObjMan mapRegionMan;
			if (this.mainDb.DoesObjectExist(mapRegionDBID))
			{
				mapRegionMan = this.mainDb.GetManipulator(mapRegionDBID);
				SafeObjMan.SetInt(mapRegionMan, "Areas", 0);
			}
			else
			{
				mapRegionMan = this.mainDb.CreateNewObject("mapLoader.MapRegion");
				if (mapRegionMan != null)
				{
					this.mainDb.AddNewObject(mapRegionDBID, mapRegionMan);
				}
			}
			if (mapRegionMan != null)
			{
				this.mapRegionMans[mapRegionFolder] = mapRegionMan;
				return true;
			}
			return false;
		}

		// Token: 0x060002AF RID: 687 RVA: 0x0001D3CA File Offset: 0x0001C3CA
		public int GetProgressSteps(bool forSave)
		{
			if (forSave)
			{
				return 2;
			}
			return 1;
		}

		// Token: 0x060002B0 RID: 688 RVA: 0x0001D3D4 File Offset: 0x0001C3D4
		public bool Save(MapEditorMap map, MainForm.Context context, IProgressContainer progressContainer)
		{
			if (progressContainer != null)
			{
				progressContainer.AddString(Strings.SAVING_ZONE_LOCATORS);
			}
			if (this.mainDb == null)
			{
				this.mainDb = IDatabase.GetMainDatabase();
			}
			if (this.mainDb == null)
			{
				return false;
			}
			this.mapRegionMans.Clear();
			bool result = ZoneLocatorsDataSource.ApplyToAllPatches(map, context, new ZoneLocatorsDataSource.PatchMethod(this.RecreateMapRegion));
			if (progressContainer != null)
			{
				progressContainer.Progress++;
			}
			if (result)
			{
				ObjMan.StartMassEditing();
				ZoneLocatorContainer zoneLocatorContainer = map.MapEditorMapObjectContainer.ZoneLocatorContainer;
				if (zoneLocatorContainer != null)
				{
					List<ZoneLocator> zoneLocators = new List<ZoneLocator>();
					foreach (KeyValuePair<int, IMapObject> keyValuePair in zoneLocatorContainer.MapObjects)
					{
						ZoneLocator zoneLocator = keyValuePair.Value as ZoneLocator;
						if (zoneLocator != null && !zoneLocator.Temporary)
						{
							zoneLocators.Add(zoneLocator);
						}
					}
					zoneLocators.Sort(ZoneLocatorsDataSource.zoneLocatorIDComparer);
					foreach (ZoneLocator zoneLocator2 in zoneLocators)
					{
						Point localPatch = new Point(Constants.PatchIndex(zoneLocator2.Position.X), Constants.PatchIndex(zoneLocator2.Position.Y));
						Position additionalPosition = Constants.PatchMinXMinY(localPatch);
						string mapRegionFolder = Constants.PatchFolder(map.Data.ContinentName, localPatch);
						IObjMan mapRegionMan;
						if (this.mapRegionMans.TryGetValue(mapRegionFolder, out mapRegionMan) && mapRegionMan != null)
						{
							int index = SafeObjMan.GetInt(mapRegionMan, "Areas");
							mapRegionMan.Insert("Areas", -1);
							string objectPropertyName = string.Format("Areas.[{0}].", index);
							SafeObjMan.SetPositionCase(mapRegionMan, objectPropertyName + "position", zoneLocator2.Position - additionalPosition);
							SafeObjMan.SetDBID(mapRegionMan, objectPropertyName + "zone", zoneLocator2.MapZone);
						}
					}
				}
				if (progressContainer != null)
				{
					progressContainer.Progress++;
				}
				ObjMan.StopMassEditing();
			}
			this.mapRegionMans.Clear();
			return result;
		}

		// Token: 0x060002B1 RID: 689 RVA: 0x0001D5FC File Offset: 0x0001C5FC
		public bool Load(MapEditorMap map, MainForm.Context context, IProgressContainer progressContainer, out bool somethingCreated)
		{
			somethingCreated = false;
			if (progressContainer != null)
			{
				progressContainer.AddString(Strings.LOADING_ZONE_LOCATORS);
			}
			if (this.mainDb == null)
			{
				this.mainDb = IDatabase.GetMainDatabase();
			}
			return this.mainDb != null && ZoneLocatorsDataSource.ApplyToAllPatches(map, context, new ZoneLocatorsDataSource.PatchMethod(this.LoadMapRegion));
		}

		// Token: 0x04000244 RID: 580
		private static readonly string mapRegionFileName = "MapRegion.xdb";

		// Token: 0x04000245 RID: 581
		private readonly Dictionary<string, IObjMan> mapRegionMans = new Dictionary<string, IObjMan>();

		// Token: 0x04000246 RID: 582
		private IDatabase mainDb;

		// Token: 0x04000247 RID: 583
		private static readonly ZoneLocatorsDataSource.ZoneLocatorIDComparer zoneLocatorIDComparer = new ZoneLocatorsDataSource.ZoneLocatorIDComparer();

		// Token: 0x02000024 RID: 36
		private class ZoneLocatorIDComparer : IComparer<ZoneLocator>
		{
			// Token: 0x060002B4 RID: 692 RVA: 0x0001D674 File Offset: 0x0001C674
			public int Compare(ZoneLocator left, ZoneLocator right)
			{
				return MapObjectContainer.MapObjectIDComparer.Compare(left, right);
			}
		}

		// Token: 0x02000025 RID: 37
		// (Invoke) Token: 0x060002B7 RID: 695
		private delegate bool PatchMethod(MapEditorMap map, MainForm.Context context, Point patchIndex);
	}
}
