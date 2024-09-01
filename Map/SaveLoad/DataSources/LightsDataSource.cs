using System;
using System.Collections.Generic;
using System.Drawing;
using Db;
using Db.Main;
using MapEditor.Map.DataProviders;
using MapEditor.Resources.Strings;
using Tools.Geometry;
using Tools.MapZoneLights;
using Tools.Progress;
using Tools.SafeObjMan;

namespace MapEditor.Map.SaveLoad.DataSources
{
	// Token: 0x0200027A RID: 634
	internal class LightsDataSource : SaveLoad.IDataSource
	{
		// Token: 0x06001E2C RID: 7724 RVA: 0x000C5184 File Offset: 0x000C4184
		private static bool ApplyToAllPatches(MapEditorMap map, MainForm.Context context, LightsDataSource.PatchMethod patchMethod)
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

		// Token: 0x06001E2D RID: 7725 RVA: 0x000C5214 File Offset: 0x000C4214
		private static bool LoadLight(IDatabase mainDb, string zone, out Color color)
		{
			DBID dbid = IDatabase.CreateDBIDByName(zone);
			if (!DBID.IsNullOrEmpty(dbid))
			{
				IObjMan man = mainDb.GetManipulator(dbid);
				if (man != null)
				{
					int _color = SafeObjMan.GetInt(man, "color");
					color = Color.FromArgb(_color);
					return true;
				}
			}
			color = Color.Empty;
			return false;
		}

		// Token: 0x06001E2E RID: 7726 RVA: 0x000C5264 File Offset: 0x000C4264
		private static bool RecreateMapRegion(MapEditorMap map, MainForm.Context context, Tools.Geometry.Point patchIndex)
		{
			IDatabase mainDb = IDatabase.GetMainDatabase();
			if (mainDb != null)
			{
				DBID mapRegionDBID = IDatabase.CreateDBIDByName(Constants.PatchFolder(map.Data.ContinentName, patchIndex) + LightsDataSource.mapRegionFileName);
				IObjMan mapRegionMan;
				if (mainDb.DoesObjectExist(mapRegionDBID))
				{
					mapRegionMan = mainDb.GetManipulator(mapRegionDBID);
					SafeObjMan.SetInt(mapRegionMan, "zoneLights", 0);
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
					string propertyName = "zoneLights";
					mapRegionMan.Insert(propertyName, -1, LightsDataSource.tilesCount.Y);
					for (int y = 0; y < LightsDataSource.tilesCount.Y; y++)
					{
						string indexString = propertyName + string.Format(".[{0}]", y);
						mapRegionMan.Insert(indexString, -1, LightsDataSource.tilesCount.X);
					}
					return true;
				}
			}
			return false;
		}

		// Token: 0x06001E2F RID: 7727 RVA: 0x000C5348 File Offset: 0x000C4348
		private static bool SaveMapRegion(MapEditorMap map, MainForm.Context context, Tools.Geometry.Point patchIndex)
		{
			IDatabase mainDb = IDatabase.GetMainDatabase();
			if (mainDb != null)
			{
				DBID mapRegionDBID = mainDb.GetDBIDByName(Constants.PatchFolder(map.Data.ContinentName, patchIndex) + LightsDataSource.mapRegionFileName);
				IObjMan mapRegionMan = mainDb.GetManipulator(mapRegionDBID);
				if (mapRegionMan != null)
				{
					string propertyName = "zoneLights";
					Tools.Geometry.Point patch = new Tools.Geometry.Point(patchIndex.X - map.Data.MinXMinYPatchCoords.X, patchIndex.Y - map.Data.MinXMinYPatchCoords.Y);
					Tools.Geometry.Point tileDelta = new Tools.Geometry.Point(patch.X * LightsDataSource.tilesCount.X, patch.Y * LightsDataSource.tilesCount.Y);
					int countY = SafeObjMan.GetInt(mapRegionMan, propertyName);
					for (int indexY = 0; indexY < Math.Min(countY, LightsDataSource.tilesCount.Y); indexY++)
					{
						int countX = SafeObjMan.GetInt(mapRegionMan, string.Format("{0}.[{1}]", propertyName, indexY));
						for (int indexX = 0; indexX < Math.Min(countX, LightsDataSource.tilesCount.X); indexX++)
						{
							Tools.Geometry.Point tile = new Tools.Geometry.Point(tileDelta.X + indexX, tileDelta.Y + indexY);
							string light = map.ZoneLightContainer.GetLight(tile);
							SafeObjMan.SetDBID(mapRegionMan, string.Format("{0}.[{1}].[{2}]", propertyName, indexY, indexX), light);
						}
					}
					return true;
				}
			}
			return false;
		}

		// Token: 0x06001E30 RID: 7728 RVA: 0x000C54C8 File Offset: 0x000C44C8
		private static bool LoadMapRegion(MapEditorMap map, MainForm.Context context, Tools.Geometry.Point patchIndex)
		{
			IDatabase mainDb = IDatabase.GetMainDatabase();
			if (mainDb != null)
			{
				DBID mapRegionDBID = mainDb.GetDBIDByName(Constants.PatchFolder(map.Data.ContinentName, patchIndex) + LightsDataSource.mapRegionFileName);
				IObjMan mapRegionMan = mainDb.GetManipulator(mapRegionDBID);
				if (mapRegionMan != null)
				{
					List<Tools.Geometry.Point> tiles = new List<Tools.Geometry.Point>(1);
					string objectPropertyName = "zoneLights";
					Tools.Geometry.Point patch = new Tools.Geometry.Point(patchIndex.X - map.Data.MinXMinYPatchCoords.X, patchIndex.Y - map.Data.MinXMinYPatchCoords.Y);
					Tools.Geometry.Point tileDelta = new Tools.Geometry.Point(patch.X * LightsDataSource.tilesCount.X, patch.Y * LightsDataSource.tilesCount.Y);
					int countY = SafeObjMan.GetInt(mapRegionMan, objectPropertyName);
					for (int indexY = 0; indexY < Math.Min(countY, LightsDataSource.tilesCount.Y); indexY++)
					{
						int countX = SafeObjMan.GetInt(mapRegionMan, string.Format("{0}.[{1}]", objectPropertyName, indexY));
						for (int indexX = 0; indexX < Math.Min(countX, LightsDataSource.tilesCount.X); indexX++)
						{
							string light = SafeObjMan.GetDBID(mapRegionMan, string.Format("{0}.[{1}].[{2}]", objectPropertyName, indexY, indexX));
							Tools.Geometry.Point tile = new Tools.Geometry.Point(tileDelta.X + indexX, tileDelta.Y + indexY);
							tiles.Clear();
							tiles.Add(tile);
							map.ZoneLightContainer.SetLight(tiles, light);
						}
					}
				}
				return true;
			}
			return false;
		}

		// Token: 0x06001E31 RID: 7729 RVA: 0x000C5664 File Offset: 0x000C4664
		public static bool LoadLightList(MapEditorMap map)
		{
			map.ZoneLightContainer.ClearZoneLightList();
			IDatabase mainDb = IDatabase.GetMainDatabase();
			if (mainDb != null)
			{
				LightItemListSource mapZoneItemListSource = new LightItemListSource(map);
				foreach (string zone in mapZoneItemListSource.Items)
				{
					Color color;
					if (LightsDataSource.LoadLight(mainDb, zone, out color))
					{
						map.ZoneLightContainer.AddLight(new ZoneLight(zone, color));
					}
				}
				return true;
			}
			return false;
		}

		// Token: 0x06001E32 RID: 7730 RVA: 0x000C56EC File Offset: 0x000C46EC
		public static void UpdateLight(MapEditorMap map, string item)
		{
			IDatabase mainDb = IDatabase.GetMainDatabase();
			Color color;
			if (mainDb != null && LightsDataSource.LoadLight(mainDb, item, out color))
			{
				map.ZoneLightContainer.UpdateLight(new ZoneLight(item, color));
			}
		}

		// Token: 0x06001E33 RID: 7731 RVA: 0x000C571F File Offset: 0x000C471F
		public int GetProgressSteps(bool forSave)
		{
			return 1;
		}

		// Token: 0x06001E34 RID: 7732 RVA: 0x000C5724 File Offset: 0x000C4724
		public bool Save(MapEditorMap map, MainForm.Context context, IProgressContainer progressContainer)
		{
			if (progressContainer != null)
			{
				progressContainer.AddString(Strings.SAVING_ZONELIGHTS);
			}
			ObjMan.StartMassEditing();
			bool result = LightsDataSource.ApplyToAllPatches(map, context, new LightsDataSource.PatchMethod(LightsDataSource.RecreateMapRegion)) && LightsDataSource.ApplyToAllPatches(map, context, new LightsDataSource.PatchMethod(LightsDataSource.SaveMapRegion));
			ObjMan.StopMassEditing();
			if (progressContainer != null)
			{
				progressContainer.Progress++;
			}
			return result;
		}

		// Token: 0x06001E35 RID: 7733 RVA: 0x000C5788 File Offset: 0x000C4788
		public bool Load(MapEditorMap map, MainForm.Context context, IProgressContainer progressContainer, out bool somethingCreated)
		{
			somethingCreated = false;
			if (progressContainer != null)
			{
				progressContainer.AddString(Strings.LOADING_ZONELIGHTS);
			}
			map.ZoneLightContainer.ClearTiles();
			bool result = LightsDataSource.LoadLightList(map) && LightsDataSource.ApplyToAllPatches(map, context, new LightsDataSource.PatchMethod(LightsDataSource.LoadMapRegion));
			if (progressContainer != null)
			{
				progressContainer.Progress++;
			}
			return result;
		}

		// Token: 0x040012F9 RID: 4857
		private static readonly string mapRegionFileName = "MapRegion.xdb";

		// Token: 0x040012FA RID: 4858
		private static readonly Tools.Geometry.Point tilesCount = new Tools.Geometry.Point(Constants.PatchSize / Constants.MapZonePieceSize.X, Constants.PatchSize / Constants.MapZonePieceSize.Y);

		// Token: 0x0200027B RID: 635
		// (Invoke) Token: 0x06001E39 RID: 7737
		private delegate bool PatchMethod(MapEditorMap map, MainForm.Context context, Tools.Geometry.Point patchIndex);
	}
}
