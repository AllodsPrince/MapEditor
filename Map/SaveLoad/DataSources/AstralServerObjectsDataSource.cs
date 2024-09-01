using System;
using System.Collections.Generic;
using Db;
using Db.Main;
using MapEditor.Map.MapObjectElements;
using MapEditor.Map.MapObjects;
using MapEditor.Resources.Strings;
using Tools.MapObjects;
using Tools.Progress;
using Tools.SafeObjMan;

namespace MapEditor.Map.SaveLoad.DataSources
{
	// Token: 0x0200005C RID: 92
	internal class AstralServerObjectsDataSource : SaveLoad.IDataSource
	{
		// Token: 0x060004C3 RID: 1219 RVA: 0x000277D1 File Offset: 0x000267D1
		private static bool DoesValidSpawnTableTypeForAstral(SpawnTableType spawnTableType)
		{
			return spawnTableType == SpawnTableType.SingleDevice || spawnTableType == SpawnTableType.AstralMob || spawnTableType == SpawnTableType.AstralWreck || spawnTableType == SpawnTableType.AstralTeleport;
		}

		// Token: 0x060004C4 RID: 1220 RVA: 0x000277E5 File Offset: 0x000267E5
		public int GetProgressSteps(bool forSave)
		{
			return 1;
		}

		// Token: 0x060004C5 RID: 1221 RVA: 0x000277E8 File Offset: 0x000267E8
		public bool Save(MapEditorMap map, MainForm.Context context, IProgressContainer progressContainer)
		{
			if (progressContainer != null)
			{
				progressContainer.AddString(Strings.SAVING_SERVER_OBJECTS);
			}
			if (!context.FullDatabase)
			{
				return true;
			}
			if (this.mainDb == null)
			{
				this.mainDb = IDatabase.GetMainDatabase();
			}
			if (this.mainDb == null)
			{
				return false;
			}
			string mapRegion = Constants.ContinentFolder(map.Data.ContinentName) + AstralServerObjectsDataSource.serverObjectsFileName;
			DBID mapRegionDBID = IDatabase.CreateDBIDByName(mapRegion);
			IObjMan mapRegionMan;
			if (this.mainDb.DoesObjectExist(mapRegionDBID))
			{
				mapRegionMan = this.mainDb.GetManipulator(mapRegionDBID);
				SafeObjMan.SetInt(mapRegionMan, "objects", 0);
			}
			else
			{
				mapRegionMan = this.mainDb.CreateNewObject(AstralServerObjectsDataSource.fileDBType);
				if (mapRegionMan != null)
				{
					this.mainDb.AddNewObject(mapRegionDBID, mapRegionMan);
				}
			}
			if (mapRegionMan != null)
			{
				double additionalRatio = map.Data.ScaleRatio;
				Position additionalPosition = new Position((double)(map.Data.MapSize.X * Constants.PatchSize) / 2.0, (double)(map.Data.MapSize.Y * Constants.PatchSize) / 2.0, 0.0);
				ObjMan.StartMassEditing();
				List<IMapObject> serverObjects = new List<IMapObject>();
				foreach (KeyValuePair<int, IMapObject> keyValuePair in map.MapEditorMapObjectContainer.MapObjects)
				{
					IMapObject mapObject = keyValuePair.Value;
					if (mapObject != null && !mapObject.Temporary)
					{
						if (mapObject.Type.Type == MapObjectFactory.Type.SpawnPoint)
						{
							SpawnPoint spawnPoint = mapObject as SpawnPoint;
							if (spawnPoint != null && AstralServerObjectsDataSource.DoesValidSpawnTableTypeForAstral(spawnPoint.SpawnTableType))
							{
								serverObjects.Add(spawnPoint);
							}
						}
						else if (mapObject.Type.Type == MapObjectFactory.Type.AstralBorder)
						{
							AstralBorder astralBorder = mapObject as AstralBorder;
							if (astralBorder != null)
							{
								serverObjects.Add(astralBorder);
							}
						}
						else if (mapObject.Type.Type == MapObjectFactory.Type.MapLocator)
						{
							MapLocator mapLocator = mapObject as MapLocator;
							if (mapLocator != null)
							{
								serverObjects.Add(mapLocator);
							}
						}
					}
				}
				SpawnPointLinkCollector spawnPointLinkCollector = new SpawnPointLinkCollector(map.MapEditorMapObjectContainer);
				Dictionary<string, int> usedPatrolRoutes = new Dictionary<string, int>();
				serverObjects.Sort(MapObjectContainer.MapObjectIDComparer);
				foreach (IMapObject serverObject in serverObjects)
				{
					int objectIndex = SafeObjMan.GetInt(mapRegionMan, "objects");
					string objectPropertyName = string.Format("objects.[{0}]", objectIndex);
					mapRegionMan.Insert("objects", -1, 1);
					if (serverObject.Type.Type == MapObjectFactory.Type.SpawnPoint)
					{
						SpawnPoint spawnPoint2 = serverObject as SpawnPoint;
						if (spawnPoint2 != null && AstralServerObjectsDataSource.DoesValidSpawnTableTypeForAstral(spawnPoint2.SpawnTableType))
						{
							if (spawnPoint2.SpawnTableType == SpawnTableType.SingleDevice)
							{
								mapRegionMan.SetStructPtrInstance(objectPropertyName, AstralServerObjectsDataSource.spawnPointSingleDeviceEntryDBType);
							}
							else
							{
								mapRegionMan.SetStructPtrInstance(objectPropertyName, AstralServerObjectsDataSource.spawnPointSingleMobEntryDBType);
							}
							IObjMan objectMan = mapRegionMan.CreateManipulator(objectPropertyName);
							if (objectMan != null)
							{
								SafeObjMan.SetDBID(objectMan, "object", spawnPoint2.SpawnTable);
								SafeObjMan.SetStringOnlyModified(objectMan, "scriptID", spawnPoint2.ScriptID);
								SafeObjMan.SetDouble(objectMan, "scanRadius", spawnPoint2.ScanRadius);
								if (spawnPoint2.SpawnTime != null)
								{
									spawnPoint2.SpawnTime.Save(objectMan);
								}
								else
								{
									objectMan.SetStructPtrZero("spawnTime");
								}
								string placePropertyName = "place";
								SpawnPointsDataSource.SaveSpawnPoint(spawnPoint2, map, this.mainDb, context, objectMan, placePropertyName, additionalRatio, ref additionalPosition, spawnPointLinkCollector, usedPatrolRoutes);
							}
						}
					}
					else if (serverObject.Type.Type == MapObjectFactory.Type.AstralBorder)
					{
						AstralBorder astralBorder2 = serverObject as AstralBorder;
						if (astralBorder2 != null)
						{
							mapRegionMan.SetStructPtrInstance(objectPropertyName, AstralServerObjectsDataSource.astralBorderEntryDBType);
							IObjMan objectMan2 = mapRegionMan.CreateManipulator(objectPropertyName);
							if (objectMan2 != null)
							{
								SpawnPointsDataSource.SaveAstralBorder(astralBorder2, objectMan2, additionalRatio, ref additionalPosition);
							}
						}
					}
					else if (serverObject.Type.Type == MapObjectFactory.Type.MapLocator)
					{
						MapLocator mapLocator2 = serverObject as MapLocator;
						if (mapLocator2 != null)
						{
							mapRegionMan.SetStructPtrInstance(objectPropertyName, AstralServerObjectsDataSource.mapLocatorEntryDBType);
							IObjMan objectMan3 = mapRegionMan.CreateManipulator(objectPropertyName);
							if (objectMan3 != null)
							{
								SpawnPointsDataSource.SaveMapLocator(mapLocator2, objectMan3, additionalRatio, ref additionalPosition);
							}
						}
					}
				}
				DBID mapDBID = this.mainDb.GetDBIDByName(map.Data.MapResourceName);
				IObjMan mapMan = this.mainDb.GetManipulator(mapDBID);
				SafeObjMan.SetDBID(mapMan, "serverObjects", mapRegion);
				ObjMan.StopMassEditing();
			}
			if (progressContainer != null)
			{
				progressContainer.Progress++;
			}
			return true;
		}

		// Token: 0x060004C6 RID: 1222 RVA: 0x00027C94 File Offset: 0x00026C94
		public bool Load(MapEditorMap map, MainForm.Context context, IProgressContainer progressContainer, out bool somethingCreated)
		{
			somethingCreated = false;
			if (progressContainer != null)
			{
				progressContainer.AddString(Strings.LOADING_SERVER_OBJECTS);
			}
			if (!context.FullDatabase)
			{
				return true;
			}
			if (this.mainDb == null)
			{
				this.mainDb = IDatabase.GetMainDatabase();
			}
			if (this.mainDb == null)
			{
				return false;
			}
			DBID mapRegionDBID = this.mainDb.GetDBIDByName(Constants.ContinentFolder(map.Data.ContinentName) + AstralServerObjectsDataSource.serverObjectsFileName);
			IObjMan mapRegionMan = this.mainDb.GetManipulator(mapRegionDBID);
			if (mapRegionMan != null)
			{
				double additionalRatio = map.Data.ScaleRatio;
				Position additionalPosition = new Position((double)(map.Data.MapSize.X * Constants.PatchSize) / 2.0, (double)(map.Data.MapSize.Y * Constants.PatchSize) / 2.0, 0.0);
				int objectCount = SafeObjMan.GetInt(mapRegionMan, "objects");
				for (int objectIndex = 0; objectIndex < objectCount; objectIndex++)
				{
					string objectPropertyName = string.Format("objects.[{0}]", objectIndex);
					IObjMan objectMan = mapRegionMan.CreateManipulator(objectPropertyName);
					if (objectMan != null)
					{
						bool isSpawnPointSingleMob = mapRegionMan.IsStructPtrInstanceCompatible(objectPropertyName, AstralServerObjectsDataSource.spawnPointSingleMobEntryDBType);
						bool isSpawnPointSingleDevice = mapRegionMan.IsStructPtrInstanceCompatible(objectPropertyName, AstralServerObjectsDataSource.spawnPointSingleDeviceEntryDBType);
						if (isSpawnPointSingleMob || isSpawnPointSingleDevice)
						{
							string spawnTable = SafeObjMan.GetDBID(objectMan, "object");
							string scriptID = SafeObjMan.GetString(objectMan, "scriptID");
							double scanRadius = SafeObjMan.GetDouble(objectMan, "scanRadius");
							string placePropertyName = "place";
							SpawnPoint spawnPoint = SpawnPointsDataSource.LoadSpawnPoint(map, this.mainDb, objectMan, placePropertyName, SpawnTableType.Undefined, spawnTable, scriptID, scanRadius, additionalRatio, ref additionalPosition);
							if (spawnPoint != null)
							{
								spawnPoint.SpawnTime = SpawnTimeAbstract.Create(objectMan);
							}
						}
						else if (mapRegionMan.IsStructPtrInstanceCompatible(objectPropertyName, AstralServerObjectsDataSource.astralBorderEntryDBType))
						{
							SpawnPointsDataSource.LoadAstralBorder(map, objectMan, additionalRatio, ref additionalPosition);
						}
						else if (mapRegionMan.IsStructPtrInstanceCompatible(objectPropertyName, AstralServerObjectsDataSource.mapLocatorEntryDBType))
						{
							SpawnPointsDataSource.LoadMapLocator(map, objectMan, additionalRatio, ref additionalPosition);
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

		// Token: 0x0400038F RID: 911
		private static readonly string serverObjectsFileName = "ServerObjects.xdb";

		// Token: 0x04000390 RID: 912
		private static readonly string fileDBType = "gameMechanics.map.ServerObjects";

		// Token: 0x04000391 RID: 913
		private static readonly string spawnPointSingleMobEntryDBType = "gameMechanics.map.spawn.MobSingleSpawn";

		// Token: 0x04000392 RID: 914
		private static readonly string spawnPointSingleDeviceEntryDBType = "gameMechanics.map.spawn.DeviceSingleSpawn";

		// Token: 0x04000393 RID: 915
		private static readonly string astralBorderEntryDBType = "gameMechanics.map.InstabilityZone";

		// Token: 0x04000394 RID: 916
		private static readonly string mapLocatorEntryDBType = "gameMechanics.map.Locator";

		// Token: 0x04000395 RID: 917
		private IDatabase mainDb;
	}
}
