using System;
using System.Collections.Generic;
using Db;
using Db.Main;
using MapEditor.Map.MapObjects;
using MapEditor.Resources.Strings;
using Tools.Geometry;
using Tools.LinkContainer;
using Tools.MapObjects;
using Tools.Progress;
using Tools.SafeObjMan;

namespace MapEditor.Map.SaveLoad.DataSources
{
	// Token: 0x020002BB RID: 699
	internal class StaticObjectsDataSource : SaveLoad.IDataSource
	{
		// Token: 0x17000754 RID: 1876
		// (get) Token: 0x060020A6 RID: 8358 RVA: 0x000CF167 File Offset: 0x000CE167
		public static string StaticObjectDataEntryDBType
		{
			get
			{
				return StaticObjectsDataSource.staticObjectDataEntryDBType;
			}
		}

		// Token: 0x060020A7 RID: 8359 RVA: 0x000CF170 File Offset: 0x000CE170
		private static bool ApplyToAllPatches(MapEditorMap map, MainForm.Context context, StaticObjectsDataSource.PatchMethod patchMethod, IProgressContainer progressContainer)
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
					if (progressContainer != null)
					{
						progressContainer.Progress++;
					}
				}
			}
			return result;
		}

		// Token: 0x060020A8 RID: 8360 RVA: 0x000CF210 File Offset: 0x000CE210
		private bool LoadMapRegion(MapEditorMap map, MainForm.Context context, Point patchIndex)
		{
			Position additionalPosition = Constants.PatchMinXMinY(patchIndex);
			DBID mapRegionDBID = this.mainDb.GetDBIDByName(Constants.PatchFolder(map.Data.ContinentName, patchIndex) + StaticObjectsDataSource.mapRegionFileName);
			IObjMan mapRegionMan = this.mainDb.GetManipulator(mapRegionDBID);
			if (mapRegionMan != null)
			{
				int count = SafeObjMan.GetInt(mapRegionMan, "Objects");
				for (int index = 0; index < count; index++)
				{
					string objectPropertyName = string.Format("Objects.[{0}].", index);
					MapObjectCreationInfo info = new MapObjectCreationInfo();
					info.Position = SafeObjMan.GetPositionCase(mapRegionMan, objectPropertyName + "Position") + additionalPosition;
					info.Rotation = SafeObjMan.GetRotationCase(mapRegionMan, objectPropertyName + "Rotation");
					info.Scale = new Scale(SafeObjMan.GetFloat(mapRegionMan, objectPropertyName + "Scale.Ratio"), Scale.Normal.X, Scale.Normal.Y, Scale.Normal.Z, Scale.Normal.Radius);
					info.GroupName = SafeObjMan.GetString(mapRegionMan, objectPropertyName + "groupName");
					string staticObjectTemplate = SafeObjMan.GetDBID(mapRegionMan, objectPropertyName + "StaticObjectTemplate");
					if (!string.IsNullOrEmpty(staticObjectTemplate))
					{
						IObjMan staticDeviceMan = null;
						if (!mapRegionMan.IsStructPtrZero(objectPropertyName + "serverStatic"))
						{
							staticDeviceMan = mapRegionMan.CreateManipulator(objectPropertyName + "serverStatic");
						}
						IMapObject addedMapObject;
						if (staticDeviceMan != null)
						{
							int permanentDeviceID = map.MapEditorMapObjectContainer.AddMapObject(new MapObjectType(MapObjectFactory.Type.PermanentDevice, staticObjectTemplate), false, info);
							if (map.MapEditorMapObjectContainer.MapObjects.TryGetValue(permanentDeviceID, out addedMapObject))
							{
								PermanentDevice permanentDevice = addedMapObject as PermanentDevice;
								if (permanentDevice != null)
								{
									permanentDevice.Device = SafeObjMan.GetDBID(staticDeviceMan, "device");
									permanentDevice.ScriptID = SafeObjMan.GetString(staticDeviceMan, "scriptID");
									permanentDevice.ScanRadius = SafeObjMan.GetDouble(staticDeviceMan, "scanRadius");
									permanentDevice.AICollision = SafeObjMan.GetBool(mapRegionMan, objectPropertyName + "aiCollision");
								}
							}
						}
						else
						{
							int staticObjectID = map.MapEditorMapObjectContainer.AddMapObject(new MapObjectType(MapObjectFactory.Type.StaticObject, staticObjectTemplate), false, info);
							if (map.MapEditorMapObjectContainer.MapObjects.TryGetValue(staticObjectID, out addedMapObject))
							{
								StaticObject staticObject = addedMapObject as StaticObject;
								if (staticObject != null)
								{
									staticObject.AICollision = SafeObjMan.GetBool(mapRegionMan, objectPropertyName + "aiCollision");
								}
							}
						}
						if (addedMapObject != null)
						{
							int optionsCount = SafeObjMan.GetInt(mapRegionMan, objectPropertyName + "options");
							if (optionsCount > 0)
							{
								for (int optionsIndex = 0; optionsIndex < optionsCount; optionsIndex++)
								{
									string optionsPropertyName = objectPropertyName + string.Format("options.[{0}]", optionsIndex);
									if (mapRegionMan.IsStructPtrInstanceCompatible(optionsPropertyName, "gameMechanics.map.zone.PlayerSpawnPlacesOption"))
									{
										IObjMan optionsMan = mapRegionMan.CreateManipulator(optionsPropertyName);
										if (optionsMan != null)
										{
											if (!optionsMan.IsStructPtrZero("graveyards"))
											{
												int graveyardCount = optionsMan.GetValue("graveyards.places");
												for (int graveyardIndex = 0; graveyardIndex < graveyardCount; graveyardIndex++)
												{
													string graveyardSubstring = string.Format("graveyards.places.[{0}].", graveyardIndex);
													Position position = SafeObjMan.GetPosition(optionsMan, graveyardSubstring + "coord");
													Rotation rotation = new Rotation(SafeObjMan.GetFloat(optionsMan, graveyardSubstring + "yaw"), 0f, 0f);
													string factionName = SafeObjMan.GetDBID(optionsMan, graveyardSubstring + "faction");
													int graveyardID = map.MapEditorMapObjectContainer.AddMapObject(new MapObjectType(MapObjectFactory.Type.Graveyard, string.Empty), false, position, rotation);
													IMapObject mapObject;
													if (map.MapEditorMapObjectContainer.MapObjects.TryGetValue(graveyardID, out mapObject))
													{
														Graveyard graveyard = mapObject as Graveyard;
														if (graveyard != null)
														{
															graveyard.RespawnType = RespawnType.Sector;
															graveyard.Faction = Respawn.GetFaction(factionName);
															map.MapEditorMapObjectContainer.AddLink(addedMapObject, mapObject, new GraveyardLinkData());
														}
													}
												}
											}
											if (!optionsMan.IsStructPtrZero("sanctuaries"))
											{
												int sanctuaryCount = optionsMan.GetValue("sanctuaries.places");
												for (int sanctuaryIndex = 0; sanctuaryIndex < sanctuaryCount; sanctuaryIndex++)
												{
													string sanctuarySubstring = string.Format("sanctuaries.places.[{0}].", sanctuaryIndex);
													Position position2 = SafeObjMan.GetPosition(optionsMan, sanctuarySubstring + "coord");
													Rotation rotation2 = new Rotation(SafeObjMan.GetFloat(optionsMan, sanctuarySubstring + "yaw"), 0f, 0f);
													string factionName2 = SafeObjMan.GetDBID(optionsMan, sanctuarySubstring + "faction");
													int sanctuaryID = map.MapEditorMapObjectContainer.AddMapObject(new MapObjectType(MapObjectFactory.Type.Sanctuary, string.Empty), false, position2, rotation2);
													IMapObject mapObject2;
													if (map.MapEditorMapObjectContainer.MapObjects.TryGetValue(sanctuaryID, out mapObject2))
													{
														Sanctuary sanctuary = mapObject2 as Sanctuary;
														if (sanctuary != null)
														{
															sanctuary.RespawnType = RespawnType.Sector;
															sanctuary.Faction = Respawn.GetFaction(factionName2);
															map.MapEditorMapObjectContainer.AddLink(addedMapObject, mapObject2, new SanctuaryLinkData());
														}
													}
												}
											}
										}
									}
								}
							}
						}
					}
				}
			}
			return true;
		}

		// Token: 0x060020A9 RID: 8361 RVA: 0x000CF714 File Offset: 0x000CE714
		private bool RecreateMapRegion(MapEditorMap map, MainForm.Context context, Point patchIndex)
		{
			string mapRegionFolder = Constants.PatchFolder(map.Data.ContinentName, patchIndex);
			DBID mapRegionDBID = IDatabase.CreateDBIDByName(mapRegionFolder + StaticObjectsDataSource.mapRegionFileName);
			IObjMan mapRegionMan;
			if (this.mainDb.DoesObjectExist(mapRegionDBID))
			{
				mapRegionMan = this.mainDb.GetManipulator(mapRegionDBID);
				SafeObjMan.SetInt(mapRegionMan, "Objects", 0);
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

		// Token: 0x060020AA RID: 8362 RVA: 0x000CF7A2 File Offset: 0x000CE7A2
		public StaticObjectsDataSource(MapEditorMap map)
		{
			this.mapSize = map.Data.MapSize;
		}

		// Token: 0x060020AB RID: 8363 RVA: 0x000CF7C8 File Offset: 0x000CE7C8
		public int GetProgressSteps(bool forSave)
		{
			if (forSave)
			{
				return 2;
			}
			return this.mapSize.X * this.mapSize.Y;
		}

		// Token: 0x060020AC RID: 8364 RVA: 0x000CF7F8 File Offset: 0x000CE7F8
		public bool Save(MapEditorMap map, MainForm.Context context, IProgressContainer progressContainer)
		{
			if (progressContainer != null)
			{
				progressContainer.AddString(Strings.SAVING_STATIC_OBJECTS);
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
			bool result = StaticObjectsDataSource.ApplyToAllPatches(map, context, new StaticObjectsDataSource.PatchMethod(this.RecreateMapRegion), null);
			if (progressContainer != null)
			{
				progressContainer.Progress++;
			}
			if (result)
			{
				ObjMan.StartMassEditing();
				List<IMapObject> mapObjects = new List<IMapObject>();
				foreach (KeyValuePair<int, IMapObject> keyValuePair in map.MapEditorMapObjectContainer.MapObjects)
				{
					if (keyValuePair.Value != null && !keyValuePair.Value.Temporary && (keyValuePair.Value.Type.Type == MapObjectFactory.Type.StaticObject || keyValuePair.Value.Type.Type == MapObjectFactory.Type.PermanentDevice) && !string.IsNullOrEmpty(keyValuePair.Value.Type.Stats))
					{
						mapObjects.Add(keyValuePair.Value);
					}
				}
				mapObjects.Sort(MapObjectContainer.MapObjectIDComparer);
				foreach (IMapObject mapObject in mapObjects)
				{
					Point localPatch = new Point(Constants.PatchIndex(mapObject.Position.X), Constants.PatchIndex(mapObject.Position.Y));
					Position additionalPosition = Constants.PatchMinXMinY(localPatch);
					string mapRegionFolder = Constants.PatchFolder(map.Data.ContinentName, localPatch);
					IObjMan mapRegionMan;
					if (this.mapRegionMans.TryGetValue(mapRegionFolder, out mapRegionMan) && mapRegionMan != null)
					{
						int index = SafeObjMan.GetInt(mapRegionMan, "Objects");
						mapRegionMan.Insert("Objects", -1);
						string objectPropertyName = string.Format("Objects.[{0}].", index);
						SafeObjMan.SetPositionCase(mapRegionMan, objectPropertyName + "Position", mapObject.Position - additionalPosition);
						SafeObjMan.SetRotationCase(mapRegionMan, objectPropertyName + "Rotation", mapObject.Rotation);
						SafeObjMan.SetFloat(mapRegionMan, objectPropertyName + "Scale.Ratio", mapObject.Scale.Ratio);
						SafeObjMan.SetStringOnlyModified(mapRegionMan, objectPropertyName + "groupName", mapObject.GroupName);
						SafeObjMan.SetDBID(mapRegionMan, objectPropertyName + "StaticObjectTemplate", mapObject.Type.Stats);
						if (mapObject.Type.Type == MapObjectFactory.Type.PermanentDevice)
						{
							PermanentDevice permanentDevice = mapObject as PermanentDevice;
							if (permanentDevice != null)
							{
								mapRegionMan.SetStructPtrInstance(objectPropertyName + "serverStatic", StaticObjectsDataSource.staticDeviceDataEntryDBType);
								IObjMan staticDeviceMan = mapRegionMan.CreateManipulator(objectPropertyName + "serverStatic");
								SafeObjMan.SetDBID(staticDeviceMan, "device", permanentDevice.Device);
								SafeObjMan.SetStringOnlyModified(staticDeviceMan, "scriptID", permanentDevice.ScriptID);
								SafeObjMan.SetDouble(staticDeviceMan, "scanRadius", permanentDevice.ScanRadius);
								SafeObjMan.SetBool(mapRegionMan, objectPropertyName + "aiCollision", permanentDevice.AICollision);
							}
						}
						else if (mapObject.Type.Type == MapObjectFactory.Type.StaticObject)
						{
							StaticObject staticObject = mapObject as StaticObject;
							if (staticObject != null)
							{
								SafeObjMan.SetBool(mapRegionMan, objectPropertyName + "aiCollision", staticObject.AICollision);
							}
						}
						Dictionary<IMapObject, ILinkData> links = map.MapEditorMapObjectContainer.GetLinks(mapObject);
						if (links != null && links.Count > 0)
						{
							mapRegionMan.Insert(objectPropertyName + "options", 0);
							mapRegionMan.SetStructPtrInstance(objectPropertyName + "options.[0]", "gameMechanics.map.zone.PlayerSpawnPlacesOption");
							int graveyardIndex = 0;
							int sanctuaryIndex = 0;
							foreach (KeyValuePair<IMapObject, ILinkData> keyValuePair2 in links)
							{
								IMapObject linkedMapObject = keyValuePair2.Key;
								if (linkedMapObject is Graveyard)
								{
									Graveyard graveyard = linkedMapObject as Graveyard;
									if (mapRegionMan.IsStructPtrZero(objectPropertyName + "options.[0].graveyards"))
									{
										mapRegionMan.InitStructPtrInstance(objectPropertyName + "options.[0].graveyards");
									}
									mapRegionMan.Insert(objectPropertyName + "options.[0].graveyards.places", graveyardIndex);
									string graveyardSubstring = string.Format(objectPropertyName + "options.[0].graveyards.places.[{0}].", graveyardIndex);
									SafeObjMan.SetPosition(mapRegionMan, graveyardSubstring + "coord", graveyard.Position);
									SafeObjMan.SetFloat(mapRegionMan, graveyardSubstring + "yaw", graveyard.Rotation.Yaw);
									SafeObjMan.SetDBIDOnlyModified(mapRegionMan, graveyardSubstring + "faction", Respawn.GetFactionName(graveyard.Faction));
									graveyardIndex++;
								}
								else if (linkedMapObject is Sanctuary)
								{
									Sanctuary sanctuary = linkedMapObject as Sanctuary;
									if (mapRegionMan.IsStructPtrZero(objectPropertyName + "options.[0].sanctuaries"))
									{
										mapRegionMan.InitStructPtrInstance(objectPropertyName + "options.[0].sanctuaries");
									}
									mapRegionMan.Insert(objectPropertyName + "options.[0].sanctuaries.places", sanctuaryIndex);
									string sanctuarySubstring = string.Format(objectPropertyName + "options.[0].sanctuaries.places.[{0}].", sanctuaryIndex);
									SafeObjMan.SetPosition(mapRegionMan, sanctuarySubstring + "coord", sanctuary.Position);
									SafeObjMan.SetFloat(mapRegionMan, sanctuarySubstring + "yaw", sanctuary.Rotation.Yaw);
									SafeObjMan.SetDBIDOnlyModified(mapRegionMan, sanctuarySubstring + "faction", Respawn.GetFactionName(sanctuary.Faction));
									sanctuaryIndex++;
								}
							}
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

		// Token: 0x060020AD RID: 8365 RVA: 0x000CFE18 File Offset: 0x000CEE18
		public bool Load(MapEditorMap map, MainForm.Context context, IProgressContainer progressContainer, out bool somethingCreated)
		{
			somethingCreated = false;
			if (progressContainer != null)
			{
				progressContainer.AddString(Strings.LOADING_STATIC_OBJECTS);
			}
			if (this.mainDb == null)
			{
				this.mainDb = IDatabase.GetMainDatabase();
			}
			return this.mainDb != null && StaticObjectsDataSource.ApplyToAllPatches(map, context, new StaticObjectsDataSource.PatchMethod(this.LoadMapRegion), progressContainer);
		}

		// Token: 0x040013F4 RID: 5108
		private static readonly string mapRegionFileName = "MapRegion.xdb";

		// Token: 0x040013F5 RID: 5109
		private static readonly string staticObjectDataEntryDBType = "gameMechanics.map.StaticObjectData";

		// Token: 0x040013F6 RID: 5110
		private static readonly string staticDeviceDataEntryDBType = "gameMechanics.map.StaticDevice";

		// Token: 0x040013F7 RID: 5111
		private readonly Dictionary<string, IObjMan> mapRegionMans = new Dictionary<string, IObjMan>();

		// Token: 0x040013F8 RID: 5112
		private IDatabase mainDb;

		// Token: 0x040013F9 RID: 5113
		private readonly Point mapSize;

		// Token: 0x020002BC RID: 700
		// (Invoke) Token: 0x060020B0 RID: 8368
		private delegate bool PatchMethod(MapEditorMap map, MainForm.Context context, Point patchIndex);
	}
}
