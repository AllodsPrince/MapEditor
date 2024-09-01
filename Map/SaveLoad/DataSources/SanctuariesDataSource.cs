using System;
using System.Collections.Generic;
using Db;
using Db.Main;
using MapEditor.Map.Containers;
using MapEditor.Map.MapObjects;
using MapEditor.Resources.Strings;
using MapInfo;
using Tools.Geometry;
using Tools.LinkContainer;
using Tools.MapObjects;
using Tools.Progress;
using Tools.SafeObjMan;

namespace MapEditor.Map.SaveLoad.DataSources
{
	// Token: 0x02000007 RID: 7
	internal class SanctuariesDataSource : SaveLoad.IDataSource
	{
		// Token: 0x06000021 RID: 33 RVA: 0x00002CC8 File Offset: 0x00001CC8
		private static void GetSanctuaryIndices(MapEditorMap map, IDictionary<string, List<int>> sanctuaryIndices)
		{
			IDatabase mainDb = IDatabase.GetMainDatabase();
			if (mainDb != null)
			{
				Point size = map.MapZoneContainer.Size;
				for (int x = 0; x < size.X; x++)
				{
					for (int y = 0; y < size.Y; y++)
					{
						string mapZone = map.MapZoneContainer.GetZone(x, y);
						if (!string.IsNullOrEmpty(mapZone) && !sanctuaryIndices.ContainsKey(mapZone))
						{
							sanctuaryIndices.Add(mapZone, new List<int>());
							string rootMapZone = MapZoneContainer.GetRootZone(mainDb, mapZone);
							if (!string.IsNullOrEmpty(rootMapZone) && !sanctuaryIndices.ContainsKey(rootMapZone))
							{
								sanctuaryIndices.Add(rootMapZone, new List<int>());
							}
						}
					}
				}
				foreach (KeyValuePair<string, List<int>> keyValuePair in sanctuaryIndices)
				{
					DBID zoneDBID = mainDb.GetDBIDByName(keyValuePair.Key);
					if (!DBID.IsNullOrEmpty(zoneDBID))
					{
						IObjMan zoneMan = mainDb.GetManipulator(zoneDBID);
						if (zoneMan != null && !zoneMan.IsStructPtrZero("sanctuaries"))
						{
							int count = SafeObjMan.GetInt(zoneMan, "sanctuaries.places");
							for (int index = 0; index < count; index++)
							{
								string sanctuarySubstring = string.Format("sanctuaries.places.[{0}].", index);
								Position position = new Position(SafeObjMan.GetDouble(zoneMan, sanctuarySubstring + "coord.x"), SafeObjMan.GetDouble(zoneMan, sanctuarySubstring + "coord.y"), 0.0);
								if ((double)(map.Data.MinXMinYPatchCoords.X * Constants.PatchSize) <= position.X && position.X <= (double)((map.Data.MinXMinYPatchCoords.X + map.Data.MapSize.X) * Constants.PatchSize) && (double)(map.Data.MinXMinYPatchCoords.Y * Constants.PatchSize) <= position.Y && position.Y <= (double)((map.Data.MinXMinYPatchCoords.Y + map.Data.MapSize.Y) * Constants.PatchSize))
								{
									keyValuePair.Value.Add(index);
								}
							}
						}
					}
				}
			}
		}

		// Token: 0x06000022 RID: 34 RVA: 0x00002F2C File Offset: 0x00001F2C
		public int GetProgressSteps(bool forSave)
		{
			if (forSave)
			{
				return 1;
			}
			return 1;
		}

		// Token: 0x06000023 RID: 35 RVA: 0x00002F34 File Offset: 0x00001F34
		public bool Save(MapEditorMap map, MainForm.Context context, IProgressContainer progressContainer)
		{
			if (progressContainer != null)
			{
				progressContainer.AddString(Strings.SAVING_SANCTUARIES);
			}
			SanctuaryContainer sanctuaryContainer = map.MapEditorMapObjectContainer.SanctuaryContainer;
			IDatabase mainDb = IDatabase.GetMainDatabase();
			if (sanctuaryContainer != null && mainDb != null)
			{
				ObjMan.StartMassEditing();
				Dictionary<string, List<int>> sanctuaryIndices = new Dictionary<string, List<int>>();
				SanctuariesDataSource.GetSanctuaryIndices(map, sanctuaryIndices);
				List<Sanctuary> sanctuaries = new List<Sanctuary>();
				foreach (KeyValuePair<int, IMapObject> keyValuePair in sanctuaryContainer.MapObjects)
				{
					Sanctuary sanctuary = keyValuePair.Value as Sanctuary;
					if (sanctuary != null && !sanctuary.Temporary)
					{
						Dictionary<IMapObject, ILinkData> links = map.MapEditorMapObjectContainer.GetLinks(sanctuary);
						if (links == null || links.Count == 0)
						{
							sanctuaries.Add(sanctuary);
						}
					}
				}
				sanctuaries.Sort(SanctuariesDataSource.sanctuaryIDComparer);
				foreach (Sanctuary sanctuary2 in sanctuaries)
				{
					string mapZone = map.MapZoneContainer.GetZone((int)((sanctuary2.Position.X - (double)(map.Data.MinXMinYPatchCoords.X * Constants.PatchSize)) / 16.0), (int)((sanctuary2.Position.Y - (double)(map.Data.MinXMinYPatchCoords.Y * Constants.PatchSize)) / 16.0));
					if (sanctuary2.RespawnType == RespawnType.Common)
					{
						mapZone = MapZoneContainer.GetRootZone(mainDb, mapZone);
					}
					if (!string.IsNullOrEmpty(mapZone))
					{
						DBID zoneDBID = mainDb.GetDBIDByName(mapZone);
						IObjMan zoneMan = mainDb.GetManipulator(zoneDBID);
						if (zoneMan != null)
						{
							if (zoneMan.IsStructPtrZero("sanctuaries"))
							{
								zoneMan.InitStructPtrInstance("sanctuaries");
							}
							int sanctuaryCount = SafeObjMan.GetInt(zoneMan, "sanctuaries.places");
							int sanctuaryIndex = sanctuaryCount;
							List<int> indices;
							if (sanctuaryIndices.TryGetValue(mapZone, out indices) && indices.Count > 0)
							{
								sanctuaryIndex = indices[0];
								indices.RemoveAt(0);
							}
							if (sanctuaryIndex >= sanctuaryCount)
							{
								sanctuaryIndex = sanctuaryCount;
								zoneMan.Insert("sanctuaries.places", sanctuaryIndex);
							}
							string sanctuarySubstring = string.Format("sanctuaries.places.[{0}].", sanctuaryIndex);
							SafeObjMan.SetPosition(zoneMan, sanctuarySubstring + "coord", sanctuary2.Position);
							SafeObjMan.SetFloat(zoneMan, sanctuarySubstring + "yaw", sanctuary2.Rotation.Yaw);
							SafeObjMan.SetDBIDOnlyModified(zoneMan, sanctuarySubstring + "faction", Respawn.GetFactionName(sanctuary2.Faction));
						}
					}
				}
				foreach (KeyValuePair<string, List<int>> keyValuePair2 in sanctuaryIndices)
				{
					DBID zoneDBID2 = mainDb.GetDBIDByName(keyValuePair2.Key);
					IObjMan zoneMan2 = mainDb.GetManipulator(zoneDBID2);
					if (zoneMan2 != null && !zoneMan2.IsStructPtrZero("sanctuaries") && keyValuePair2.Value.Count > 0)
					{
						int count = keyValuePair2.Value.Count;
						for (int index = count - 1; index >= 0; index--)
						{
							int sanctuaryIndex2 = keyValuePair2.Value[index];
							zoneMan2.Remove("sanctuaries.places", sanctuaryIndex2);
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

		// Token: 0x06000024 RID: 36 RVA: 0x000032D4 File Offset: 0x000022D4
		public bool Load(MapEditorMap map, MainForm.Context context, IProgressContainer progressContainer, out bool somethingCreated)
		{
			somethingCreated = false;
			if (progressContainer != null)
			{
				progressContainer.AddString(Strings.LOADING_SANCTUARIES);
			}
			IDatabase mainDb = IDatabase.GetMainDatabase();
			if (mainDb != null)
			{
				Dictionary<string, List<int>> sanctuaryIndices = new Dictionary<string, List<int>>();
				SanctuariesDataSource.GetSanctuaryIndices(map, sanctuaryIndices);
				foreach (KeyValuePair<string, List<int>> keyValuePair in sanctuaryIndices)
				{
					if (keyValuePair.Value.Count > 0)
					{
						DBID mapZoneDBID = mainDb.GetDBIDByName(keyValuePair.Key);
						IObjMan zoneMan = mainDb.GetManipulator(mapZoneDBID);
						if (zoneMan != null && !zoneMan.IsStructPtrZero("sanctuaries"))
						{
							string parentMapZone = SafeObjMan.GetDBID(zoneMan, "parentZone");
							int count = keyValuePair.Value.Count;
							for (int index = 0; index < count; index++)
							{
								int sanctuaryIndex = keyValuePair.Value[index];
								string sanctuarySubstring = string.Format("sanctuaries.places.[{0}].", sanctuaryIndex);
								Position position = SafeObjMan.GetPosition(zoneMan, sanctuarySubstring + "coord");
								Rotation rotation = new Rotation(SafeObjMan.GetFloat(zoneMan, sanctuarySubstring + "yaw"), 0f, 0f);
								string factionName = SafeObjMan.GetDBID(zoneMan, sanctuarySubstring + "faction");
								int sanctuaryID = map.MapEditorMapObjectContainer.AddMapObject(new MapObjectType(MapObjectFactory.Type.Sanctuary, string.Empty), false, position, rotation);
								IMapObject mapObject;
								if (map.MapEditorMapObjectContainer.MapObjects.TryGetValue(sanctuaryID, out mapObject))
								{
									Sanctuary sanctuary = mapObject as Sanctuary;
									if (sanctuary != null)
									{
										sanctuary.RespawnType = (string.IsNullOrEmpty(parentMapZone) ? RespawnType.Common : RespawnType.Sector);
										sanctuary.Faction = Respawn.GetFaction(factionName);
									}
								}
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

		// Token: 0x04000009 RID: 9
		private static readonly SanctuariesDataSource.SanctuaryIDComparer sanctuaryIDComparer = new SanctuariesDataSource.SanctuaryIDComparer();

		// Token: 0x02000008 RID: 8
		private class SanctuaryIDComparer : IComparer<Sanctuary>
		{
			// Token: 0x06000027 RID: 39 RVA: 0x000034CC File Offset: 0x000024CC
			public int Compare(Sanctuary left, Sanctuary right)
			{
				return MapObjectContainer.MapObjectIDComparer.Compare(left, right);
			}
		}
	}
}
