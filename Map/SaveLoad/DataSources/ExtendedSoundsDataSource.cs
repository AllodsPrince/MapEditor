using System;
using System.Collections.Generic;
using System.IO;
using Db;
using Db.Main;
using MapEditor.Map.Containers;
using MapEditor.Map.MapObjects;
using MapEditor.Resources.Strings;
using Tools.LinkContainer;
using Tools.MapObjects;
using Tools.Progress;
using Tools.SafeObjMan;

namespace MapEditor.Map.SaveLoad.DataSources
{
	// Token: 0x0200016C RID: 364
	internal class ExtendedSoundsDataSource : SaveLoad.IDataSource
	{
		// Token: 0x060011AE RID: 4526 RVA: 0x000835A4 File Offset: 0x000825A4
		private static Sound LoadSound(IObjMan manipulator, string soundName)
		{
			return new Sound
			{
				Project = SafeObjMan.GetDBID(manipulator, string.Format("{0}.{1}", soundName, "project")),
				Name = SafeObjMan.GetString(manipulator, string.Format("{0}.{1}", soundName, "name"))
			};
		}

		// Token: 0x060011AF RID: 4527 RVA: 0x000835F0 File Offset: 0x000825F0
		private static void SaveSound(IObjMan manipulator, string soundName, Sound sound)
		{
			if (sound != null)
			{
				SafeObjMan.SetDBID(manipulator, string.Format("{0}.{1}", soundName, "project"), sound.Project);
				SafeObjMan.SetString(manipulator, string.Format("{0}.{1}", soundName, "name"), sound.Name);
			}
		}

		// Token: 0x060011B0 RID: 4528 RVA: 0x00083630 File Offset: 0x00082630
		private static List<Position> LoadBoundPoints(IObjMan manipulator)
		{
			List<Position> boundPoints = new List<Position>();
			int count = SafeObjMan.GetInt(manipulator, "boundPoints");
			for (int i = 0; i < count; i++)
			{
				string propertyName = string.Format("{0}.[{1}]", "boundPoints", i);
				Position boundPoint = SafeObjMan.GetPosition(manipulator, propertyName);
				boundPoints.Add(boundPoint);
			}
			return boundPoints;
		}

		// Token: 0x060011B1 RID: 4529 RVA: 0x00083684 File Offset: 0x00082684
		private static void SaveBoundPoints(IObjMan manipulator, List<Position> boundPoints)
		{
			SafeObjMan.SetInt(manipulator, "boundPoints", 0);
			for (int i = 0; i < boundPoints.Count; i++)
			{
				manipulator.Insert("boundPoints", i);
				SafeObjMan.SetPosition(manipulator, string.Format("{0}.[{1}]", "boundPoints", i), boundPoints[i]);
			}
		}

		// Token: 0x060011B2 RID: 4530 RVA: 0x000836E0 File Offset: 0x000826E0
		private static List<int> LoadBoundIndices(IObjMan manipulator)
		{
			List<int> boundIndices = new List<int>();
			int count = SafeObjMan.GetInt(manipulator, "boundIndices");
			for (int i = 0; i < count; i++)
			{
				string propertyName = string.Format("{0}.[{1}]", "boundIndices", i);
				int boundIndex = SafeObjMan.GetInt(manipulator, propertyName);
				boundIndices.Add(boundIndex);
			}
			return boundIndices;
		}

		// Token: 0x060011B3 RID: 4531 RVA: 0x00083734 File Offset: 0x00082734
		private static void SaveBoundIndices(IObjMan manipulator, List<int> boundIndices)
		{
			SafeObjMan.SetInt(manipulator, "boundIndices", 0);
			for (int i = 0; i < boundIndices.Count; i++)
			{
				manipulator.Insert("boundIndices", i);
				SafeObjMan.SetInt(manipulator, string.Format("{0}.[{1}]", "boundIndices", i), boundIndices[i]);
			}
		}

		// Token: 0x060011B4 RID: 4532 RVA: 0x00083790 File Offset: 0x00082790
		private static ExtendedSoundSaveData LoadExtendedSound(IObjMan manipulator, string extendedSoundName)
		{
			if (manipulator != null)
			{
				List<Position> boundPoints = ExtendedSoundsDataSource.LoadBoundPoints(manipulator);
				List<int> boundIndices = ExtendedSoundsDataSource.LoadBoundIndices(manipulator);
				Sound centralSound = ExtendedSoundsDataSource.LoadSound(manipulator, "centralSound");
				Sound sideSound = ExtendedSoundsDataSource.LoadSound(manipulator, "sideSound");
				if (boundPoints.Count > 0)
				{
					return new ExtendedSoundSaveData(boundPoints, boundIndices, centralSound, sideSound, extendedSoundName);
				}
			}
			return null;
		}

		// Token: 0x060011B5 RID: 4533 RVA: 0x000837DC File Offset: 0x000827DC
		private static void SaveExtendedSound(IObjMan manipulator, ExtendedSoundSaveData data)
		{
			if (manipulator != null && data != null)
			{
				ExtendedSoundsDataSource.SaveBoundPoints(manipulator, data.BoundPoints);
				ExtendedSoundsDataSource.SaveBoundIndices(manipulator, data.BoundIndices);
				ExtendedSoundsDataSource.SaveSound(manipulator, "centralSound", data.CentralSound);
				ExtendedSoundsDataSource.SaveSound(manipulator, "sideSound", data.SideSound);
			}
		}

		// Token: 0x060011B6 RID: 4534 RVA: 0x0008382C File Offset: 0x0008282C
		private List<ExtendedSoundSaveData> LoadExtendedSoundsData(IObjMan manipulator, IDatabase mainDb, string folderPath)
		{
			List<ExtendedSoundSaveData> datas = new List<ExtendedSoundSaveData>();
			if (manipulator != null)
			{
				int count = SafeObjMan.GetInt(manipulator, "extendedSoundObjects");
				for (int i = 0; i < count; i++)
				{
					string extendedSoundPath = SafeObjMan.GetDBID(manipulator, string.Format("{0}.[{1}]", "extendedSoundObjects", i));
					DBID extendedSoundDBID = mainDb.GetDBIDByName(extendedSoundPath);
					IObjMan extSoundMan = mainDb.GetManipulator(extendedSoundDBID);
					string extendedSoundName = ExtendedSoundsDataSource.GetExtendedSoundNameFromPath(extendedSoundPath);
					ExtendedSoundSaveData data = ExtendedSoundsDataSource.LoadExtendedSound(extSoundMan, extendedSoundName);
					if (data != null)
					{
						datas.Add(data);
						this.usedExtendedSounds.Add(extendedSoundPath);
					}
				}
				return datas;
			}
			return null;
		}

		// Token: 0x060011B7 RID: 4535 RVA: 0x000838B8 File Offset: 0x000828B8
		private void SaveExtendedSoundsData(IObjMan manipulator, List<ExtendedSoundSaveData> datas, IDatabase mainDb, string folderPath)
		{
			if (manipulator != null && datas != null)
			{
				SafeObjMan.SetInt(manipulator, "extendedSoundObjects", 0);
				for (int i = 0; i < datas.Count; i++)
				{
					string extendedSoundPath = string.IsNullOrEmpty(datas[i].Name) ? SafeObjMan.CreateUniqueDBID(folderPath + ExtendedSound.ExtendedSoundDefaultName + ExtendedSound.ExtendedSoundDBExtention, true, mainDb) : ExtendedSoundsDataSource.GetPathFromExtendedSoundName(datas[i].Name, folderPath);
					DBID extendedSoundDBID = mainDb.GetDBIDByName(extendedSoundPath);
					IObjMan extSoundMan;
					if (extendedSoundDBID.IsEmpty())
					{
						extendedSoundDBID = IDatabase.CreateDBIDByName(extendedSoundPath);
						extSoundMan = mainDb.CreateNewObject(ExtendedSound.ExtendedSoundDBType);
						if (extSoundMan != null)
						{
							mainDb.AddNewObject(extendedSoundDBID, extSoundMan);
						}
					}
					else
					{
						extSoundMan = (mainDb.GetManipulator(extendedSoundDBID) ?? mainDb.CreateNewObject(ExtendedSound.ExtendedSoundDBType));
					}
					manipulator.Insert("extendedSoundObjects", i);
					SafeObjMan.SetDBID(manipulator, string.Format("{0}.[{1}]", "extendedSoundObjects", i), extendedSoundPath);
					ExtendedSoundsDataSource.SaveExtendedSound(extSoundMan, datas[i]);
					this.newUsedExtendedSounds.Add(extendedSoundPath);
					this.usedExtendedSounds.Remove(extendedSoundPath);
				}
			}
		}

		// Token: 0x060011B8 RID: 4536 RVA: 0x000839CC File Offset: 0x000829CC
		private static void LoadExtendedSoundsFromData(MapEditorMap map, ICollection<ExtendedSoundSaveData> datas)
		{
			if (map != null && datas != null)
			{
				foreach (ExtendedSoundSaveData data in datas)
				{
					ExtendedSoundsDataSource.CreateExtendedSounds(map, data);
				}
			}
		}

		// Token: 0x060011B9 RID: 4537 RVA: 0x00083A1C File Offset: 0x00082A1C
		private static List<ExtendedSoundSaveData> LoadDataFromExtendedSounds(MapEditorMapObjectContainer mapEditorMapObjectContainer, List<IMapObject> extendedSounds)
		{
			List<ExtendedSoundSaveData> datas = new List<ExtendedSoundSaveData>();
			if (mapEditorMapObjectContainer != null && extendedSounds != null)
			{
				List<IMapObject> addedPoints = new List<IMapObject>();
				List<IMapObject> points = new List<IMapObject>();
				Dictionary<IMapObject, List<IMapObject>> links = new Dictionary<IMapObject, List<IMapObject>>();
				foreach (IMapObject extendedSound in extendedSounds)
				{
					if (!addedPoints.Contains(extendedSound) && !ExtendedSoundsDataSource.AddPointWithLinks(mapEditorMapObjectContainer, extendedSound, points, links) && points.Count > 0)
					{
						addedPoints.AddRange(points);
						datas.Add(ExtendedSoundsDataSource.CreateExtendedSoundData(points, links));
						points.Clear();
						links.Clear();
					}
				}
			}
			return datas;
		}

		// Token: 0x060011BA RID: 4538 RVA: 0x00083AC8 File Offset: 0x00082AC8
		private static bool AddPointWithLinks(MapEditorMapObjectContainer mapEditorMapObjectContainer, IMapObject extendedSound, List<IMapObject> points, Dictionary<IMapObject, List<IMapObject>> links)
		{
			List<IMapObject> pointLinks = ExtendedSoundsDataSource.GetLinks(mapEditorMapObjectContainer, extendedSound);
			if (pointLinks != null && pointLinks.Count > 0)
			{
				if (!points.Contains(extendedSound))
				{
					points.Add(extendedSound);
					foreach (IMapObject linkedPoint in pointLinks)
					{
						ExtendedSoundsDataSource.AddPointWithLinks(mapEditorMapObjectContainer, linkedPoint, points, links);
						if (!points.Contains(linkedPoint))
						{
							points.Add(linkedPoint);
						}
					}
					links.Add(extendedSound, pointLinks);
				}
				return true;
			}
			return false;
		}

		// Token: 0x060011BB RID: 4539 RVA: 0x00083B58 File Offset: 0x00082B58
		private static List<IMapObject> GetLinks(MapEditorMapObjectContainer mapEditorMapObjectContainer, IMapObject extendedSound)
		{
			Dictionary<IMapObject, ILinkData> links = mapEditorMapObjectContainer.GetLinks(extendedSound);
			if (extendedSound is ExtendedSound && links != null && links.Count > 0)
			{
				List<IMapObject> linkedExtendedSounds = new List<IMapObject>();
				foreach (IMapObject mapObject in links.Keys)
				{
					if (mapObject is ExtendedSound && !mapObject.Temporary && extendedSound.ID < mapObject.ID)
					{
						linkedExtendedSounds.Add(mapObject);
					}
				}
				linkedExtendedSounds.Sort(MapObjectContainer.MapObjectIDComparer);
				return linkedExtendedSounds;
			}
			return null;
		}

		// Token: 0x060011BC RID: 4540 RVA: 0x00083BFC File Offset: 0x00082BFC
		private static void CreateExtendedSounds(MapEditorMap map, ExtendedSoundSaveData data)
		{
			if (map != null && data != null && data.BoundPoints.Count > 0)
			{
				List<ExtendedSound> extendedSounds = new List<ExtendedSound>();
				for (int i = 0; i < data.BoundPoints.Count; i++)
				{
					ExtendedSoundsDataSource.CreateExtendedSound(map.MapEditorMapObjectContainer, data, extendedSounds, data.BoundPoints[i]);
				}
				ExtendedSoundsDataSource.CreateLinks(map.MapEditorMapObjectContainer, extendedSounds, data.BoundIndices);
			}
		}

		// Token: 0x060011BD RID: 4541 RVA: 0x00083C64 File Offset: 0x00082C64
		private static void CreateExtendedSound(MapEditorMapObjectContainer mapEditorMapObjectContainer, ExtendedSoundSaveData data, List<ExtendedSound> extendedSounds, Position point)
		{
			int newMapObjectID = mapEditorMapObjectContainer.AddMapObject(new MapObjectType(MapObjectFactory.Type.ExtendedSound, data.Name), false, point);
			IMapObject mapObject;
			if (mapEditorMapObjectContainer.TryGetMapObject(newMapObjectID, out mapObject))
			{
				ExtendedSound sound = mapObject as ExtendedSound;
				if (sound != null)
				{
					sound.CentralSound = data.CentralSound;
					sound.SideSound = data.SideSound;
					sound.Name = data.Name;
					extendedSounds.Add(sound);
				}
			}
		}

		// Token: 0x060011BE RID: 4542 RVA: 0x00083CCC File Offset: 0x00082CCC
		private static ExtendedSoundSaveData CreateExtendedSoundData(List<IMapObject> points, Dictionary<IMapObject, List<IMapObject>> links)
		{
			ExtendedSoundSaveData data = new ExtendedSoundSaveData();
			Sound centralSound = Sound.Empty;
			Sound sideSound = Sound.Empty;
			string name = string.Empty;
			int centralSoundNumber = 0;
			int sideSoundNumber = 0;
			int nameNumber = 0;
			Dictionary<Sound, int> centralSounds = new Dictionary<Sound, int>();
			Dictionary<Sound, int> sideSounds = new Dictionary<Sound, int>();
			Dictionary<string, int> names = new Dictionary<string, int>();
			points.Sort(MapObjectContainer.MapObjectIDComparer);
			for (int i = 0; i < points.Count; i++)
			{
				ExtendedSound point = points[i] as ExtendedSound;
				if (point != null)
				{
					data.BoundPoints.Add(point.Position);
					ExtendedSoundsDataSource.AddBoundIndices(data, points, links, point, i);
					ExtendedSoundsDataSource.UpdateSoundsData<Sound>(centralSounds, point.CentralSound, ref centralSound, ref centralSoundNumber);
					ExtendedSoundsDataSource.UpdateSoundsData<Sound>(sideSounds, point.SideSound, ref sideSound, ref sideSoundNumber);
					ExtendedSoundsDataSource.UpdateSoundsData<string>(names, point.Name, ref name, ref nameNumber);
				}
			}
			data.CentralSound = centralSound;
			data.SideSound = sideSound;
			data.Name = name;
			return data;
		}

		// Token: 0x060011BF RID: 4543 RVA: 0x00083DAC File Offset: 0x00082DAC
		private static List<IMapObject> GetExtendedSounds(MapEditorMapObjectContainer mapEditorMapObjectContainer)
		{
			List<IMapObject> extendedSounds = new List<IMapObject>();
			if (mapEditorMapObjectContainer != null)
			{
				foreach (IMapObject mapObject in mapEditorMapObjectContainer.ExtendedSoundContainer.MapObjects.Values)
				{
					if (mapObject != null && !mapObject.Temporary)
					{
						extendedSounds.Add(mapObject);
					}
				}
				extendedSounds.Sort(MapObjectContainer.MapObjectIDComparer);
			}
			return extendedSounds;
		}

		// Token: 0x060011C0 RID: 4544 RVA: 0x00083E2C File Offset: 0x00082E2C
		private static void CreateLinks(MapEditorMapObjectContainer mapEditorMapObjectContainer, List<ExtendedSound> extendedSounds, List<int> indices)
		{
			for (int i = 0; i < indices.Count; i++)
			{
				if (indices[i] < extendedSounds.Count && indices[i + 1] < extendedSounds.Count && indices[i] < extendedSounds.Count && indices[i + 1] < extendedSounds.Count)
				{
					mapEditorMapObjectContainer.AddLink(extendedSounds[indices[i]], extendedSounds[indices[++i]], null);
				}
			}
		}

		// Token: 0x060011C1 RID: 4545 RVA: 0x00083EB0 File Offset: 0x00082EB0
		private static void AddBoundIndices(ExtendedSoundSaveData data, List<IMapObject> points, Dictionary<IMapObject, List<IMapObject>> links, ExtendedSound point, int i)
		{
			List<IMapObject> pointLinks;
			if (links.TryGetValue(point, out pointLinks))
			{
				foreach (IMapObject mapObject in pointLinks)
				{
					ExtendedSound linkedPoint = mapObject as ExtendedSound;
					if (linkedPoint != null)
					{
						int index = points.IndexOf(linkedPoint);
						if (index > -1)
						{
							data.BoundIndices.Add(i);
							data.BoundIndices.Add(index);
						}
					}
				}
			}
		}

		// Token: 0x060011C2 RID: 4546 RVA: 0x00083F34 File Offset: 0x00082F34
		private static void UpdateSoundsData<T>(Dictionary<T, int> sounds, T sound, ref T commonSound, ref int maxCount)
		{
			if (sounds.ContainsKey(sound))
			{
				sounds[sound]++;
			}
			else
			{
				sounds.Add(sound, 1);
			}
			if (sounds[sound] > maxCount)
			{
				maxCount = sounds[sound];
				commonSound = sound;
			}
		}

		// Token: 0x060011C3 RID: 4547 RVA: 0x00083F83 File Offset: 0x00082F83
		private static string GetExtendedSoundNameFromPath(string path)
		{
			return Path.GetFileName(path).Replace(ExtendedSound.ExtendedSoundDBExtention, string.Empty);
		}

		// Token: 0x060011C4 RID: 4548 RVA: 0x00083F9A File Offset: 0x00082F9A
		private static string GetPathFromExtendedSoundName(string extendedSoundName, string folderPath)
		{
			return string.Format("{0}{1}{2}", folderPath, extendedSoundName, ExtendedSound.ExtendedSoundDBExtention);
		}

		// Token: 0x060011C5 RID: 4549 RVA: 0x00083FAD File Offset: 0x00082FAD
		public int GetProgressSteps(bool forSave)
		{
			if (forSave)
			{
				return 1;
			}
			return 1;
		}

		// Token: 0x060011C6 RID: 4550 RVA: 0x00083FB8 File Offset: 0x00082FB8
		public bool Save(MapEditorMap map, MainForm.Context context, IProgressContainer progressContainer)
		{
			if (progressContainer != null)
			{
				progressContainer.AddString(Strings.SAVING_EXTENDED_SOUNDS);
			}
			IDatabase mainDb = IDatabase.GetMainDatabase();
			if (mainDb != null)
			{
				ObjMan.StartMassEditing();
				List<IMapObject> extendedSounds = ExtendedSoundsDataSource.GetExtendedSounds(map.MapEditorMapObjectContainer);
				List<ExtendedSoundSaveData> datas = ExtendedSoundsDataSource.LoadDataFromExtendedSounds(map.MapEditorMapObjectContainer, extendedSounds);
				if (datas != null && datas.Count > 0)
				{
					string folderPath = Constants.ContinentFolder(map.Data.ContinentName) + ExtendedSound.ExtendedSoundsFolder;
					DBID extendedSoundCollectionDBID = mainDb.GetDBIDByName(folderPath + ExtendedSound.ExtendedSoundCollectionFileName);
					IObjMan manipulator;
					if (extendedSoundCollectionDBID.IsEmpty())
					{
						extendedSoundCollectionDBID = IDatabase.CreateDBIDByName(folderPath);
						manipulator = mainDb.CreateNewObject(ExtendedSound.ExtendedSoundsDBType);
						if (manipulator != null)
						{
							mainDb.AddNewObject(extendedSoundCollectionDBID, manipulator);
						}
					}
					else
					{
						manipulator = mainDb.GetManipulator(extendedSoundCollectionDBID);
					}
					this.SaveExtendedSoundsData(manipulator, datas, mainDb, folderPath);
				}
				foreach (string extendedSoundPath in this.usedExtendedSounds)
				{
					DBID extendedSoundDBID = mainDb.GetDBIDByName(extendedSoundPath);
					mainDb.RemoveObject(extendedSoundDBID);
				}
				ObjMan.StopMassEditing();
				this.usedExtendedSounds.Clear();
				this.usedExtendedSounds.AddRange(this.newUsedExtendedSounds);
				this.newUsedExtendedSounds.Clear();
			}
			if (progressContainer != null)
			{
				progressContainer.Progress++;
			}
			return true;
		}

		// Token: 0x060011C7 RID: 4551 RVA: 0x00084110 File Offset: 0x00083110
		public bool Load(MapEditorMap map, MainForm.Context context, IProgressContainer progressContainer, out bool somethingCreated)
		{
			somethingCreated = false;
			if (progressContainer != null)
			{
				progressContainer.AddString(Strings.LOADING_EXTENDED_SOUNDS);
			}
			IDatabase mainDb = IDatabase.GetMainDatabase();
			if (mainDb != null)
			{
				string folderPath = EditorEnvironment.DataFolder + Constants.ContinentFolder(map.Data.ContinentName) + ExtendedSound.ExtendedSoundsFolder;
				DBID extendedSoundCollectionDBID = mainDb.GetDBIDByName(folderPath + ExtendedSound.ExtendedSoundCollectionFileName);
				IObjMan manipulator = mainDb.GetManipulator(extendedSoundCollectionDBID);
				List<ExtendedSoundSaveData> datas = this.LoadExtendedSoundsData(manipulator, mainDb, folderPath);
				ExtendedSoundsDataSource.LoadExtendedSoundsFromData(map, datas);
			}
			if (progressContainer != null)
			{
				progressContainer.Progress++;
			}
			return true;
		}

		// Token: 0x04000CBE RID: 3262
		private const string EXTENDED_SOUNDS = "extendedSoundObjects";

		// Token: 0x04000CBF RID: 3263
		private const string BOUND_POINTS = "boundPoints";

		// Token: 0x04000CC0 RID: 3264
		private const string BOUND_INDICES = "boundIndices";

		// Token: 0x04000CC1 RID: 3265
		private const string CENTRTAL_SOUND = "centralSound";

		// Token: 0x04000CC2 RID: 3266
		private const string SIDE_SOUND = "sideSound";

		// Token: 0x04000CC3 RID: 3267
		private const string SOUND_NAME = "name";

		// Token: 0x04000CC4 RID: 3268
		private const string SOUND_PROJECT = "project";

		// Token: 0x04000CC5 RID: 3269
		private readonly List<string> usedExtendedSounds = new List<string>();

		// Token: 0x04000CC6 RID: 3270
		private readonly List<string> newUsedExtendedSounds = new List<string>();
	}
}
