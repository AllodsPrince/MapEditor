using System;
using System.Collections.Generic;
using System.Drawing;
using Db;
using Db.Main;
using MapEditor.Map.DataProviders;
using MapEditor.Resources.Strings;
using MapInfo;
using Tools.Geometry;
using Tools.Progress;
using Tools.SafeObjMan;

namespace MapEditor.Map.SaveLoad.DataSources
{
	// Token: 0x02000005 RID: 5
	internal class MapZonesDataSource : SaveLoad.IDataSource
	{
		// Token: 0x06000011 RID: 17 RVA: 0x0000251C File Offset: 0x0000151C
		private static bool LoadMapRegion(MapEditorMap map, MainForm.Context context, Tools.Geometry.Point patchIndex)
		{
			IDatabase mainDb = IDatabase.GetMainDatabase();
			if (mainDb == null)
			{
				return false;
			}
			DBID mapRegionDBID = mainDb.GetDBIDByName(Constants.PatchFolder(map.Data.ContinentName, patchIndex) + MapZonesDataSource.mapRegionFileName);
			IObjMan mapRegionMan = mainDb.GetManipulator(mapRegionDBID);
			if (mapRegionMan != null)
			{
				List<Tools.Geometry.Point> tiles = new List<Tools.Geometry.Point>(1);
				string objectPropertyName = "tiles";
				Tools.Geometry.Point patch = new Tools.Geometry.Point(patchIndex.X - map.Data.MinXMinYPatchCoords.X, patchIndex.Y - map.Data.MinXMinYPatchCoords.Y);
				Tools.Geometry.Point tileDelta = new Tools.Geometry.Point(patch.X * MapZonesDataSource.tilesCount.X, patch.Y * MapZonesDataSource.tilesCount.Y);
				int countY = SafeObjMan.GetInt(mapRegionMan, objectPropertyName);
				for (int indexY = 0; indexY < Math.Min(countY, MapZonesDataSource.tilesCount.Y); indexY++)
				{
					int countX = SafeObjMan.GetInt(mapRegionMan, string.Concat(new object[]
					{
						objectPropertyName,
						".[",
						indexY,
						"]"
					}));
					for (int indexX = 0; indexX < Math.Min(countX, MapZonesDataSource.tilesCount.X); indexX++)
					{
						string mapZone = SafeObjMan.GetDBID(mapRegionMan, string.Concat(new object[]
						{
							objectPropertyName,
							".[",
							indexY,
							"].[",
							indexX,
							"]"
						}));
						Tools.Geometry.Point tile = new Tools.Geometry.Point(tileDelta.X + indexX, tileDelta.Y + indexY);
						tiles.Clear();
						tiles.Add(tile);
						map.MapZoneContainer.ApplyZoneToTiles(tiles, mapZone);
					}
				}
			}
			return true;
		}

		// Token: 0x06000012 RID: 18 RVA: 0x00002704 File Offset: 0x00001704
		private static bool RecreateMapRegion(MapEditorMap map, MainForm.Context context, Tools.Geometry.Point patchIndex)
		{
			IDatabase mainDb = IDatabase.GetMainDatabase();
			if (mainDb != null)
			{
				DBID mapRegionDBID = IDatabase.CreateDBIDByName(Constants.PatchFolder(map.Data.ContinentName, patchIndex) + MapZonesDataSource.mapRegionFileName);
				IObjMan mapRegionMan;
				if (mainDb.DoesObjectExist(mapRegionDBID))
				{
					mapRegionMan = mainDb.GetManipulator(mapRegionDBID);
					SafeObjMan.SetInt(mapRegionMan, "tiles", 0);
					SafeObjMan.SetInt(mapRegionMan, "zones", 0);
				}
				else
				{
					mapRegionMan = mainDb.CreateNewObject("mapLoader.MapRegion");
					if (mapRegionMan != null)
					{
						mainDb.AddNewObject(mapRegionDBID, mapRegionMan);
					}
				}
				if (mapRegionMan != null)
				{
					string propertyName = "tiles";
					mapRegionMan.Insert(propertyName, -1, MapZonesDataSource.tilesCount.Y);
					for (int y = 0; y < MapZonesDataSource.tilesCount.Y; y++)
					{
						string indexString = propertyName + string.Format(".[{0}]", y);
						mapRegionMan.Insert(indexString, -1, MapZonesDataSource.tilesCount.X);
					}
				}
			}
			return true;
		}

		// Token: 0x06000013 RID: 19 RVA: 0x000027F0 File Offset: 0x000017F0
		private static bool SaveMapRegion(MapEditorMap map, MainForm.Context context, Tools.Geometry.Point patchIndex)
		{
			IDatabase mainDb = IDatabase.GetMainDatabase();
			if (mainDb != null)
			{
				DBID mapRegionDBID = mainDb.GetDBIDByName(Constants.PatchFolder(map.Data.ContinentName, patchIndex) + MapZonesDataSource.mapRegionFileName);
				IObjMan mapRegionMan = mainDb.GetManipulator(mapRegionDBID);
				if (mapRegionMan != null)
				{
					string objectPropertyName = "tiles";
					string zonesPropertyName = "zones";
					Tools.Geometry.Point patch = new Tools.Geometry.Point(patchIndex.X - map.Data.MinXMinYPatchCoords.X, patchIndex.Y - map.Data.MinXMinYPatchCoords.Y);
					Tools.Geometry.Point tileDelta = new Tools.Geometry.Point(patch.X * MapZonesDataSource.tilesCount.X, patch.Y * MapZonesDataSource.tilesCount.Y);
					Dictionary<string, int> collectedRootZones = new Dictionary<string, int>();
					int countY = SafeObjMan.GetInt(mapRegionMan, objectPropertyName);
					for (int indexY = 0; indexY < Math.Min(countY, MapZonesDataSource.tilesCount.Y); indexY++)
					{
						int countX = SafeObjMan.GetInt(mapRegionMan, string.Concat(new object[]
						{
							objectPropertyName,
							".[",
							indexY,
							"]"
						}));
						for (int indexX = 0; indexX < Math.Min(countX, MapZonesDataSource.tilesCount.X); indexX++)
						{
							Tools.Geometry.Point tile = new Tools.Geometry.Point(tileDelta.X + indexX, tileDelta.Y + indexY);
							string zone = map.MapZoneContainer.GetZone(tile);
							SafeObjMan.SetDBID(mapRegionMan, string.Concat(new object[]
							{
								objectPropertyName,
								".[",
								indexY,
								"].[",
								indexX,
								"]"
							}), zone);
							string rootZone = MapZoneContainer.GetRootZone(mainDb, zone);
							if (!collectedRootZones.ContainsKey(rootZone))
							{
								collectedRootZones.Add(rootZone, 0);
								mapRegionMan.Insert(zonesPropertyName, 0);
								SafeObjMan.SetDBID(mapRegionMan, zonesPropertyName + ".[0]", rootZone);
							}
						}
					}
				}
			}
			return true;
		}

		// Token: 0x06000014 RID: 20 RVA: 0x00002A0C File Offset: 0x00001A0C
		private static bool ApplyToAllPatches(MapEditorMap map, MainForm.Context context, MapZonesDataSource.PatchMethod patchMethod)
		{
			bool result = true;
			for (int x = 0; x < map.Data.MapSize.X; x++)
			{
				for (int y = 0; y < map.Data.MapSize.Y; y++)
				{
					Tools.Geometry.Point patchIndex = new Tools.Geometry.Point(map.Data.MinXMinYPatchCoords.X + x, map.Data.MinXMinYPatchCoords.Y + y);
					if (!patchMethod(map, context, patchIndex))
					{
						result = false;
					}
				}
			}
			return result;
		}

		// Token: 0x06000015 RID: 21 RVA: 0x00002A9C File Offset: 0x00001A9C
		private static bool LoadZone(IDatabase mainDb, string zone, out Color color, out string light)
		{
			DBID dbid = IDatabase.CreateDBIDByName(zone);
			if (!DBID.IsNullOrEmpty(dbid))
			{
				IObjMan man = mainDb.GetManipulator(dbid);
				if (man != null)
				{
					int _color = SafeObjMan.GetInt(man, "color");
					color = Color.FromArgb(_color);
					light = SafeObjMan.GetDBID(man, "zoneLights");
					return true;
				}
			}
			color = Color.Empty;
			light = null;
			return false;
		}

		// Token: 0x06000016 RID: 22 RVA: 0x00002AFC File Offset: 0x00001AFC
		public static bool LoadZonesList(MapEditorMap map)
		{
			map.MapZoneContainer.ClearMapZoneList();
			IDatabase mainDb = IDatabase.GetMainDatabase();
			if (mainDb == null)
			{
				return false;
			}
			MapZoneItemListSource mapZoneItemListSource = new MapZoneItemListSource(map);
			foreach (string zone in mapZoneItemListSource.Items)
			{
				Color color;
				string light;
				if (MapZonesDataSource.LoadZone(mainDb, zone, out color, out light))
				{
					map.MapZoneContainer.AddMapZone(new MapZone(zone, color, light));
				}
			}
			return true;
		}

		// Token: 0x06000017 RID: 23 RVA: 0x00002B88 File Offset: 0x00001B88
		public static void UpdateZone(MapEditorMap map, string zone)
		{
			IDatabase mainDb = IDatabase.GetMainDatabase();
			Color color;
			string light;
			if (mainDb != null && MapZonesDataSource.LoadZone(mainDb, zone, out color, out light))
			{
				map.MapZoneContainer.UpdateMapZone(new MapZone(zone, color, light));
			}
		}

		// Token: 0x06000018 RID: 24 RVA: 0x00002BBE File Offset: 0x00001BBE
		public int GetProgressSteps(bool forSave)
		{
			return 1;
		}

		// Token: 0x06000019 RID: 25 RVA: 0x00002BC4 File Offset: 0x00001BC4
		public bool Save(MapEditorMap map, MainForm.Context context, IProgressContainer progressContainer)
		{
			if (progressContainer != null)
			{
				progressContainer.AddString(Strings.SAVING_MAPZONES);
			}
			ObjMan.StartMassEditing();
			bool result = MapZonesDataSource.ApplyToAllPatches(map, context, new MapZonesDataSource.PatchMethod(MapZonesDataSource.RecreateMapRegion)) && MapZonesDataSource.ApplyToAllPatches(map, context, new MapZonesDataSource.PatchMethod(MapZonesDataSource.SaveMapRegion));
			ObjMan.StopMassEditing();
			if (progressContainer != null)
			{
				progressContainer.Progress++;
			}
			return result;
		}

		// Token: 0x0600001A RID: 26 RVA: 0x00002C28 File Offset: 0x00001C28
		public bool Load(MapEditorMap map, MainForm.Context context, IProgressContainer progressContainer, out bool somethingCreated)
		{
			somethingCreated = false;
			if (progressContainer != null)
			{
				progressContainer.AddString(Strings.LOADING_MAPZONES);
			}
			bool result = MapZonesDataSource.LoadZonesList(map) && MapZonesDataSource.ApplyToAllPatches(map, context, new MapZonesDataSource.PatchMethod(MapZonesDataSource.LoadMapRegion));
			if (progressContainer != null)
			{
				progressContainer.Progress++;
			}
			return result;
		}

		// Token: 0x04000007 RID: 7
		private static readonly string mapRegionFileName = "MapRegion.xdb";

		// Token: 0x04000008 RID: 8
		private static readonly Tools.Geometry.Point tilesCount = new Tools.Geometry.Point(Constants.PatchSize / Constants.MapZonePieceSize.X, Constants.PatchSize / Constants.MapZonePieceSize.Y);

		// Token: 0x02000006 RID: 6
		// (Invoke) Token: 0x0600001E RID: 30
		private delegate bool PatchMethod(MapEditorMap map, MainForm.Context context, Tools.Geometry.Point patchIndex);
	}
}
