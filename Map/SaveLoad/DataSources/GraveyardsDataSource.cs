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
	// Token: 0x020002BF RID: 703
	internal class GraveyardsDataSource : SaveLoad.IDataSource
	{
		// Token: 0x060020C2 RID: 8386 RVA: 0x000D0D70 File Offset: 0x000CFD70
		private static void GetGraveyardIndices(MapEditorMap map, IDictionary<string, List<int>> graveyardIndices)
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
						if (!string.IsNullOrEmpty(mapZone) && !graveyardIndices.ContainsKey(mapZone))
						{
							graveyardIndices.Add(mapZone, new List<int>());
							string rootMapZone = MapZoneContainer.GetRootZone(mainDb, mapZone);
							if (!string.IsNullOrEmpty(rootMapZone) && !graveyardIndices.ContainsKey(rootMapZone))
							{
								graveyardIndices.Add(rootMapZone, new List<int>());
							}
						}
					}
				}
				foreach (KeyValuePair<string, List<int>> keyValuePair in graveyardIndices)
				{
					DBID zoneDBID = mainDb.GetDBIDByName(keyValuePair.Key);
					if (!DBID.IsNullOrEmpty(zoneDBID))
					{
						IObjMan zoneMan = mainDb.GetManipulator(zoneDBID);
						if (zoneMan != null && !zoneMan.IsStructPtrZero("graveyards"))
						{
							int count = SafeObjMan.GetInt(zoneMan, "graveyards.places");
							for (int index = 0; index < count; index++)
							{
								string graveyardSubstring = string.Format("graveyards.places.[{0}].", index);
								Position position = new Position(SafeObjMan.GetDouble(zoneMan, graveyardSubstring + "coord.x"), SafeObjMan.GetDouble(zoneMan, graveyardSubstring + "coord.y"), 0.0);
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

		// Token: 0x060020C3 RID: 8387 RVA: 0x000D0FD4 File Offset: 0x000CFFD4
		public int GetProgressSteps(bool forSave)
		{
			if (forSave)
			{
				return 1;
			}
			return 1;
		}

		// Token: 0x060020C4 RID: 8388 RVA: 0x000D0FDC File Offset: 0x000CFFDC
		public bool Save(MapEditorMap map, MainForm.Context context, IProgressContainer progressContainer)
		{
			if (progressContainer != null)
			{
				progressContainer.AddString(Strings.SAVING_GRAVEYARDS);
			}
			GraveyardContainer graveyardContainer = map.MapEditorMapObjectContainer.GraveyardContainer;
			IDatabase mainDb = IDatabase.GetMainDatabase();
			if (graveyardContainer != null && mainDb != null)
			{
				ObjMan.StartMassEditing();
				Dictionary<string, List<int>> graveyardIndices = new Dictionary<string, List<int>>();
				GraveyardsDataSource.GetGraveyardIndices(map, graveyardIndices);
				List<Graveyard> graveyards = new List<Graveyard>();
				foreach (KeyValuePair<int, IMapObject> keyValuePair in graveyardContainer.MapObjects)
				{
					Graveyard graveyard = keyValuePair.Value as Graveyard;
					if (graveyard != null && !graveyard.Temporary)
					{
						Dictionary<IMapObject, ILinkData> links = map.MapEditorMapObjectContainer.GetLinks(graveyard);
						if (links == null || links.Count == 0)
						{
							graveyards.Add(graveyard);
						}
					}
				}
				graveyards.Sort(GraveyardsDataSource.graveyardIDComparer);
				foreach (Graveyard graveyard2 in graveyards)
				{
					string mapZone = map.MapZoneContainer.GetZone((int)((graveyard2.Position.X - (double)(map.Data.MinXMinYPatchCoords.X * Constants.PatchSize)) / 16.0), (int)((graveyard2.Position.Y - (double)(map.Data.MinXMinYPatchCoords.Y * Constants.PatchSize)) / 16.0));
					if (graveyard2.RespawnType == RespawnType.Common)
					{
						mapZone = MapZoneContainer.GetRootZone(mainDb, mapZone);
					}
					if (!string.IsNullOrEmpty(mapZone))
					{
						DBID zoneDBID = mainDb.GetDBIDByName(mapZone);
						IObjMan zoneMan = mainDb.GetManipulator(zoneDBID);
						if (zoneMan != null)
						{
							if (zoneMan.IsStructPtrZero("graveyards"))
							{
								zoneMan.InitStructPtrInstance("graveyards");
							}
							int graveyardCount = SafeObjMan.GetInt(zoneMan, "graveyards.places");
							int graveyardIndex = graveyardCount;
							List<int> indices;
							if (graveyardIndices.TryGetValue(mapZone, out indices) && indices.Count > 0)
							{
								graveyardIndex = indices[0];
								indices.RemoveAt(0);
							}
							if (graveyardIndex >= graveyardCount)
							{
								graveyardIndex = graveyardCount;
								zoneMan.Insert("graveyards.places", graveyardIndex);
							}
							string graveyardSubstring = string.Format("graveyards.places.[{0}].", graveyardIndex);
							SafeObjMan.SetPosition(zoneMan, graveyardSubstring + "coord", graveyard2.Position);
							SafeObjMan.SetFloat(zoneMan, graveyardSubstring + "yaw", graveyard2.Rotation.Yaw);
							SafeObjMan.SetDBIDOnlyModified(zoneMan, graveyardSubstring + "faction", Respawn.GetFactionName(graveyard2.Faction));
						}
					}
				}
				foreach (KeyValuePair<string, List<int>> keyValuePair2 in graveyardIndices)
				{
					DBID zoneDBID2 = mainDb.GetDBIDByName(keyValuePair2.Key);
					IObjMan zoneMan2 = mainDb.GetManipulator(zoneDBID2);
					if (zoneMan2 != null && !zoneMan2.IsStructPtrZero("graveyards") && keyValuePair2.Value.Count > 0)
					{
						int count = keyValuePair2.Value.Count;
						for (int index = count - 1; index >= 0; index--)
						{
							int graveyardIndex2 = keyValuePair2.Value[index];
							zoneMan2.Remove("graveyards.places", graveyardIndex2);
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

		// Token: 0x060020C5 RID: 8389 RVA: 0x000D137C File Offset: 0x000D037C
		public bool Load(MapEditorMap map, MainForm.Context context, IProgressContainer progressContainer, out bool somethingCreated)
		{
			somethingCreated = false;
			if (progressContainer != null)
			{
				progressContainer.AddString(Strings.LOADING_GRAVEYARDS);
			}
			IDatabase mainDb = IDatabase.GetMainDatabase();
			if (mainDb != null)
			{
				Dictionary<string, List<int>> graveyardIndices = new Dictionary<string, List<int>>();
				GraveyardsDataSource.GetGraveyardIndices(map, graveyardIndices);
				foreach (KeyValuePair<string, List<int>> keyValuePair in graveyardIndices)
				{
					if (keyValuePair.Value.Count > 0)
					{
						DBID mapZoneDBID = mainDb.GetDBIDByName(keyValuePair.Key);
						IObjMan zoneMan = mainDb.GetManipulator(mapZoneDBID);
						if (zoneMan != null && !zoneMan.IsStructPtrZero("graveyards"))
						{
							string parentMapZone = SafeObjMan.GetDBID(zoneMan, "parentZone");
							int count = keyValuePair.Value.Count;
							for (int index = 0; index < count; index++)
							{
								int graveyardIndex = keyValuePair.Value[index];
								string graveyardSubstring = string.Format("graveyards.places.[{0}].", graveyardIndex);
								Position position = SafeObjMan.GetPosition(zoneMan, graveyardSubstring + "coord");
								Rotation rotation = new Rotation(SafeObjMan.GetFloat(zoneMan, graveyardSubstring + "yaw"), 0f, 0f);
								string factionName = SafeObjMan.GetDBID(zoneMan, graveyardSubstring + "faction");
								int graveyardID = map.MapEditorMapObjectContainer.AddMapObject(new MapObjectType(MapObjectFactory.Type.Graveyard, string.Empty), false, position, rotation);
								IMapObject mapObject;
								if (map.MapEditorMapObjectContainer.MapObjects.TryGetValue(graveyardID, out mapObject))
								{
									Graveyard graveyard = mapObject as Graveyard;
									if (graveyard != null)
									{
										graveyard.RespawnType = (string.IsNullOrEmpty(parentMapZone) ? RespawnType.Common : RespawnType.Sector);
										graveyard.Faction = Respawn.GetFaction(factionName);
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

		// Token: 0x040013FB RID: 5115
		private static readonly GraveyardsDataSource.GraveyardIDComparer graveyardIDComparer = new GraveyardsDataSource.GraveyardIDComparer();

		// Token: 0x020002C0 RID: 704
		private class GraveyardIDComparer : IComparer<Graveyard>
		{
			// Token: 0x060020C8 RID: 8392 RVA: 0x000D1574 File Offset: 0x000D0574
			public int Compare(Graveyard left, Graveyard right)
			{
				return MapObjectContainer.MapObjectIDComparer.Compare(left, right);
			}
		}
	}
}
