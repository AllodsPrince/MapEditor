using System;
using System.Collections.Generic;
using System.Drawing;
using Db;
using Db.Main;
using MapEditor.Map.DataProviders;
using MapEditor.Resources.Strings;
using Tools.Geometry;
using Tools.MapSound;
using Tools.Progress;
using Tools.SafeObjMan;

namespace MapEditor.Map.SaveLoad.DataSources
{
	// Token: 0x02000223 RID: 547
	internal class SoundDataSource : SaveLoad.IDataSource
	{
		// Token: 0x06001A6A RID: 6762 RVA: 0x000AE044 File Offset: 0x000AD044
		private static bool ApplyToAllPatches(MapEditorMap map, MainForm.Context context, SoundDataSource.PatchMethod patchMethod)
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

		// Token: 0x06001A6B RID: 6763 RVA: 0x000AE0D4 File Offset: 0x000AD0D4
		private bool RecreateMapRegion(MapEditorMap map, MainForm.Context context, Tools.Geometry.Point patchIndex)
		{
			IDatabase mainDb = IDatabase.GetMainDatabase();
			if (mainDb != null)
			{
				DBID mapRegionDBID = IDatabase.CreateDBIDByName(Constants.PatchFolder(map.Data.ContinentName, patchIndex) + "MapRegion.xdb");
				IObjMan mapRegionMan;
				if (mainDb.DoesObjectExist(mapRegionDBID))
				{
					mapRegionMan = mainDb.GetManipulator(mapRegionDBID);
					SafeObjMan.SetInt(mapRegionMan, this.fieldName, 0);
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
					string propertyName = this.fieldName;
					mapRegionMan.Insert(propertyName, -1, SoundDataSource.tilesCount.Y);
					for (int y = 0; y < SoundDataSource.tilesCount.Y; y++)
					{
						string indexString = propertyName + string.Format(".[{0}]", y);
						mapRegionMan.Insert(indexString, -1, SoundDataSource.tilesCount.X);
					}
					return true;
				}
			}
			return false;
		}

		// Token: 0x06001A6C RID: 6764 RVA: 0x000AE1B8 File Offset: 0x000AD1B8
		private static bool LoadSound(IDatabase mainDb, string sound, out Color color)
		{
			DBID dbid = IDatabase.CreateDBIDByName(sound);
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

		// Token: 0x06001A6D RID: 6765 RVA: 0x000AE208 File Offset: 0x000AD208
		private bool SaveMapRegion(MapEditorMap map, MainForm.Context context, Tools.Geometry.Point patchIndex)
		{
			IDatabase mainDb = IDatabase.GetMainDatabase();
			if (mainDb != null && this.mapSoundContainer != null)
			{
				DBID mapRegionDBID = mainDb.GetDBIDByName(Constants.PatchFolder(map.Data.ContinentName, patchIndex) + "MapRegion.xdb");
				IObjMan mapRegionMan = mainDb.GetManipulator(mapRegionDBID);
				if (mapRegionMan != null)
				{
					Tools.Geometry.Point patch = new Tools.Geometry.Point(patchIndex.X - map.Data.MinXMinYPatchCoords.X, patchIndex.Y - map.Data.MinXMinYPatchCoords.Y);
					Tools.Geometry.Point tileDelta = new Tools.Geometry.Point(patch.X * SoundDataSource.tilesCount.X, patch.Y * SoundDataSource.tilesCount.Y);
					int countY = SafeObjMan.GetInt(mapRegionMan, this.fieldName);
					for (int indexY = 0; indexY < Math.Min(countY, SoundDataSource.tilesCount.Y); indexY++)
					{
						int countX = SafeObjMan.GetInt(mapRegionMan, string.Format("{0}.[{1}]", this.fieldName, indexY));
						for (int indexX = 0; indexX < Math.Min(countX, SoundDataSource.tilesCount.X); indexX++)
						{
							Tools.Geometry.Point tile = new Tools.Geometry.Point(tileDelta.X + indexX, tileDelta.Y + indexY);
							string sound = this.mapSoundContainer.GetSound(tile);
							SafeObjMan.SetDBID(mapRegionMan, string.Format("{0}.[{1}].[{2}]", this.fieldName, indexY, indexX), sound);
						}
					}
					return true;
				}
			}
			return false;
		}

		// Token: 0x06001A6E RID: 6766 RVA: 0x000AE39C File Offset: 0x000AD39C
		private bool LoadMapRegion(MapEditorMap map, MainForm.Context context, Tools.Geometry.Point patchIndex)
		{
			IDatabase mainDb = IDatabase.GetMainDatabase();
			if (mainDb != null && this.mapSoundContainer != null)
			{
				DBID mapRegionDBID = mainDb.GetDBIDByName(Constants.PatchFolder(map.Data.ContinentName, patchIndex) + "MapRegion.xdb");
				IObjMan mapRegionMan = mainDb.GetManipulator(mapRegionDBID);
				if (mapRegionMan != null)
				{
					List<Tools.Geometry.Point> tiles = new List<Tools.Geometry.Point>(1);
					Tools.Geometry.Point patch = new Tools.Geometry.Point(patchIndex.X - map.Data.MinXMinYPatchCoords.X, patchIndex.Y - map.Data.MinXMinYPatchCoords.Y);
					Tools.Geometry.Point tileDelta = new Tools.Geometry.Point(patch.X * SoundDataSource.tilesCount.X, patch.Y * SoundDataSource.tilesCount.Y);
					int countY = SafeObjMan.GetInt(mapRegionMan, this.fieldName);
					for (int indexY = 0; indexY < Math.Min(countY, SoundDataSource.tilesCount.Y); indexY++)
					{
						int countX = SafeObjMan.GetInt(mapRegionMan, string.Format("{0}.[{1}]", this.fieldName, indexY));
						for (int indexX = 0; indexX < Math.Min(countX, SoundDataSource.tilesCount.X); indexX++)
						{
							string sound = SafeObjMan.GetDBID(mapRegionMan, string.Format("{0}.[{1}].[{2}]", this.fieldName, indexY, indexX));
							Tools.Geometry.Point tile = new Tools.Geometry.Point(tileDelta.X + indexX, tileDelta.Y + indexY);
							tiles.Clear();
							tiles.Add(tile);
							this.mapSoundContainer.SetSound(tiles, sound);
						}
					}
				}
				return true;
			}
			return false;
		}

		// Token: 0x06001A6F RID: 6767 RVA: 0x000AE545 File Offset: 0x000AD545
		public SoundDataSource(string _fieldName, MapSoundContainer _mapSoundContainer)
		{
			this.fieldName = _fieldName;
			this.mapSoundContainer = _mapSoundContainer;
		}

		// Token: 0x06001A70 RID: 6768 RVA: 0x000AE55C File Offset: 0x000AD55C
		public static void Update(MapEditorMap map, string item)
		{
			IDatabase mainDb = IDatabase.GetMainDatabase();
			Color color;
			if (mainDb != null && SoundDataSource.LoadSound(mainDb, item, out color))
			{
				map.MapMusicContainer.UpdateSound(new ColoredSound(item, color));
				map.MapAmbienceContainer.UpdateSound(new ColoredSound(item, color));
			}
		}

		// Token: 0x06001A71 RID: 6769 RVA: 0x000AE5A4 File Offset: 0x000AD5A4
		public static bool LoadList(MapEditorMap map, MapSoundContainer _mapSoundContainer)
		{
			IDatabase mainDb = IDatabase.GetMainDatabase();
			if (mainDb != null && _mapSoundContainer != null)
			{
				_mapSoundContainer.ClearSoundList();
				SoundItemListSource soundItemListSource = new SoundItemListSource(map);
				foreach (string sound in soundItemListSource.Items)
				{
					Color color;
					if (SoundDataSource.LoadSound(mainDb, sound, out color))
					{
						_mapSoundContainer.AddSound(new ColoredSound(sound, color));
					}
				}
				return true;
			}
			return false;
		}

		// Token: 0x06001A72 RID: 6770 RVA: 0x000AE624 File Offset: 0x000AD624
		public int GetProgressSteps(bool forSave)
		{
			return 1;
		}

		// Token: 0x06001A73 RID: 6771 RVA: 0x000AE628 File Offset: 0x000AD628
		public bool Save(MapEditorMap map, MainForm.Context context, IProgressContainer progressContainer)
		{
			if (progressContainer != null)
			{
				progressContainer.AddString(Strings.SAVING_SOUND);
			}
			ObjMan.StartMassEditing();
			bool result = SoundDataSource.ApplyToAllPatches(map, context, new SoundDataSource.PatchMethod(this.RecreateMapRegion)) && SoundDataSource.ApplyToAllPatches(map, context, new SoundDataSource.PatchMethod(this.SaveMapRegion));
			ObjMan.StopMassEditing();
			if (progressContainer != null)
			{
				progressContainer.Progress++;
			}
			return result;
		}

		// Token: 0x06001A74 RID: 6772 RVA: 0x000AE68C File Offset: 0x000AD68C
		public bool Load(MapEditorMap map, MainForm.Context context, IProgressContainer progressContainer, out bool somethingCreated)
		{
			somethingCreated = false;
			if (progressContainer != null)
			{
				progressContainer.AddString(Strings.LOADING_SOUND);
			}
			bool result = SoundDataSource.LoadList(map, this.mapSoundContainer) && SoundDataSource.ApplyToAllPatches(map, context, new SoundDataSource.PatchMethod(this.LoadMapRegion));
			if (progressContainer != null)
			{
				progressContainer.Progress++;
			}
			return result;
		}

		// Token: 0x0400111B RID: 4379
		private const string mapRegionFileName = "MapRegion.xdb";

		// Token: 0x0400111C RID: 4380
		private static readonly Tools.Geometry.Point tilesCount = new Tools.Geometry.Point(Constants.PatchSize / Constants.MapZonePieceSize.X, Constants.PatchSize / Constants.MapZonePieceSize.Y);

		// Token: 0x0400111D RID: 4381
		private readonly string fieldName;

		// Token: 0x0400111E RID: 4382
		private readonly MapSoundContainer mapSoundContainer;

		// Token: 0x02000224 RID: 548
		// (Invoke) Token: 0x06001A77 RID: 6775
		private delegate bool PatchMethod(MapEditorMap map, MainForm.Context context, Tools.Geometry.Point patchIndex);
	}
}
