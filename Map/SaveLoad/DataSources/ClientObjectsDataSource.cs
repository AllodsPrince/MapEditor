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
	// Token: 0x0200019F RID: 415
	internal class ClientObjectsDataSource : SaveLoad.IDataSource
	{
		// Token: 0x06001440 RID: 5184 RVA: 0x00092960 File Offset: 0x00091960
		private static void GetScenes(IDictionary<string, ClientObjectsDataSource.SceneData> scenes, MapEditorMapObjectContainer mapEditorMapObjectContainer)
		{
			if (scenes != null && mapEditorMapObjectContainer != null)
			{
				foreach (KeyValuePair<int, IMapObject> keyValuePair in mapEditorMapObjectContainer.ClientSpawnPointContainer.MapObjects)
				{
					ClientSpawnPoint clientSpawnPoint = keyValuePair.Value as ClientSpawnPoint;
					if (clientSpawnPoint != null && !clientSpawnPoint.Temporary)
					{
						string scene = clientSpawnPoint.Scene;
						if (scene == null)
						{
							scene = string.Empty;
						}
						ClientObjectsDataSource.SceneData sceneData;
						if (!scenes.TryGetValue(scene, out sceneData))
						{
							sceneData = new ClientObjectsDataSource.SceneData();
							scenes.Add(scene, sceneData);
						}
						if (sceneData != null)
						{
							sceneData.ClientSpawnPoints.Add(clientSpawnPoint);
						}
					}
				}
				Dictionary<ClientPatrolNode, int> alreadyAddedClientPatrolNodes = new Dictionary<ClientPatrolNode, int>();
				foreach (KeyValuePair<int, IMapObject> keyValuePair2 in mapEditorMapObjectContainer.ClientPatrolNodeContainer.MapObjects)
				{
					ClientPatrolNode clientPatrolNode = keyValuePair2.Value as ClientPatrolNode;
					if (clientPatrolNode != null && !clientPatrolNode.Temporary && !alreadyAddedClientPatrolNodes.ContainsKey(clientPatrolNode))
					{
						string scene2 = clientPatrolNode.Scene;
						if (scene2 == null)
						{
							scene2 = string.Empty;
						}
						ClientObjectsDataSource.SceneData sceneData2;
						if (!scenes.TryGetValue(scene2, out sceneData2))
						{
							sceneData2 = new ClientObjectsDataSource.SceneData();
							scenes.Add(scene2, sceneData2);
						}
						if (sceneData2 != null)
						{
							string scriptID;
							List<ClientPatrolNode> clientPatrolNodes;
							mapEditorMapObjectContainer.ClientPatrolNodeContainer.GetLocator(clientPatrolNode, out scriptID, out clientPatrolNodes);
							if (clientPatrolNodes != null)
							{
								sceneData2.ClientPatrolNodes.Add(new KeyValuePair<string, List<ClientPatrolNode>>(scriptID, clientPatrolNodes));
								foreach (ClientPatrolNode _clientPatrolNode in clientPatrolNodes)
								{
									if (!alreadyAddedClientPatrolNodes.ContainsKey(_clientPatrolNode))
									{
										alreadyAddedClientPatrolNodes.Add(_clientPatrolNode, 0);
									}
								}
							}
						}
					}
				}
			}
		}

		// Token: 0x06001441 RID: 5185 RVA: 0x00092B3C File Offset: 0x00091B3C
		private static void SaveScene(MapEditorMap map, IObjMan sceneMan, ClientObjectsDataSource.SceneData sceneData)
		{
			if (map != null && sceneMan != null && sceneData != null)
			{
				string mapResource = map.Data.MapResourceName;
				SafeObjMan.SetDBID(sceneMan, "place.map", mapResource);
				Position center = sceneData.Center;
				SafeObjMan.SetPosition(sceneMan, "place", center);
				SafeObjMan.SetInt(sceneMan, "mobs", 0);
				SafeObjMan.SetInt(sceneMan, "devices", 0);
				SafeObjMan.SetInt(sceneMan, "paths", 0);
				int mobIndex = 0;
				int deviceIndex = 0;
				sceneData.ClientSpawnPoints.Sort(ClientObjectsDataSource.clientSpawnPointIDComparer);
				foreach (ClientSpawnPoint clientSpawnPoint in sceneData.ClientSpawnPoints)
				{
					if (clientSpawnPoint != null && clientSpawnPoint.ClientSpawnPointData != null)
					{
						if (clientSpawnPoint.ClientSpawnPointData.Type == "VisualMob")
						{
							sceneMan.Insert("mobs", mobIndex);
							string mobPropertyName = string.Format("mobs.[{0}].", mobIndex);
							SafeObjMan.SetStringOnlyModified(sceneMan, mobPropertyName + "scriptID", clientSpawnPoint.ScriptID);
							SafeObjMan.SetPosition(sceneMan, mobPropertyName + "offset", clientSpawnPoint.Position - center);
							SafeObjMan.SetFloat(sceneMan, mobPropertyName + "yaw", clientSpawnPoint.Rotation.Yaw);
							SafeObjMan.SetStringOnlyModified(sceneMan, mobPropertyName + "groupName", clientSpawnPoint.GroupName);
							SafeObjMan.SetDBID(sceneMan, mobPropertyName + "visualMob", clientSpawnPoint.VisObject);
							mobIndex++;
						}
						else if (clientSpawnPoint.ClientSpawnPointData.Type == "gameMechanics.world.device.DeviceResource")
						{
							sceneMan.Insert("devices", deviceIndex);
							string devicePropertyName = string.Format("devices.[{0}].", deviceIndex);
							SafeObjMan.SetStringOnlyModified(sceneMan, devicePropertyName + "scriptID", clientSpawnPoint.ScriptID);
							SafeObjMan.SetPosition(sceneMan, devicePropertyName + "offset", clientSpawnPoint.Position - center);
							SafeObjMan.SetFloat(sceneMan, devicePropertyName + "yaw", clientSpawnPoint.Rotation.Yaw);
							SafeObjMan.SetDBID(sceneMan, devicePropertyName + "device", clientSpawnPoint.VisObject);
							DeviceClientSpawnPointData data = (DeviceClientSpawnPointData)clientSpawnPoint.ClientSpawnPointData;
							SafeObjMan.SetInt(sceneMan, devicePropertyName + "visualState", data.VisualState);
							deviceIndex++;
						}
					}
				}
				int pathIndex = 0;
				List<KeyValuePair<string, List<ClientPatrolNode>>> keyValuePairs = new List<KeyValuePair<string, List<ClientPatrolNode>>>();
				foreach (KeyValuePair<string, List<ClientPatrolNode>> keyValuePair in sceneData.ClientPatrolNodes)
				{
					if (keyValuePair.Value != null)
					{
						keyValuePairs.Add(keyValuePair);
					}
				}
				keyValuePairs.Sort(ClientObjectsDataSource.clientPatrolNodeListIDComparer);
				foreach (KeyValuePair<string, List<ClientPatrolNode>> keyValuePair2 in keyValuePairs)
				{
					sceneMan.Insert("paths", pathIndex);
					string pathPropertyName = string.Format("paths.[{0}].", pathIndex);
					SafeObjMan.SetStringOnlyModified(sceneMan, pathPropertyName + "scriptID", keyValuePair2.Key);
					int pointIndex = 0;
					foreach (ClientPatrolNode clientPatrolNode in keyValuePair2.Value)
					{
						if (clientPatrolNode != null)
						{
							sceneMan.Insert(pathPropertyName + "points", pointIndex);
							SafeObjMan.SetPosition(sceneMan, string.Format(pathPropertyName + "points.[{0}]", pointIndex), clientPatrolNode.Position - center);
							pointIndex++;
						}
					}
					if (keyValuePair2.Value.Count > 0)
					{
						SafeObjMan.SetFloat(sceneMan, pathPropertyName + "yaw", keyValuePair2.Value[keyValuePair2.Value.Count - 1].Rotation.Yaw);
						SafeObjMan.SetStringOnlyModified(sceneMan, pathPropertyName + "groupName", keyValuePair2.Value[0].GroupName);
					}
					pathIndex++;
				}
			}
		}

		// Token: 0x06001442 RID: 5186 RVA: 0x00092FCC File Offset: 0x00091FCC
		private static void LoadScene(MapEditorMap map, string scene, IObjMan sceneMan, Vec3 minMapPosition, Vec3 maxMapPosition, out bool inside)
		{
			inside = false;
			if (map != null && sceneMan != null)
			{
				string loadedMapResource = map.Data.MapResourceName;
				string mapResource = SafeObjMan.GetDBID(sceneMan, "place.map");
				Position center = SafeObjMan.GetPosition(sceneMan, "place");
				inside = (string.Equals(loadedMapResource, mapResource, StringComparison.OrdinalIgnoreCase) && minMapPosition.X <= center.X && center.X <= maxMapPosition.X && minMapPosition.Y <= center.Y && center.Y <= maxMapPosition.Y);
				if (inside)
				{
					int mobCount = SafeObjMan.GetInt(sceneMan, "mobs");
					for (int mobIndex = 0; mobIndex < mobCount; mobIndex++)
					{
						string mobPropertyName = string.Format("mobs.[{0}].", mobIndex);
						MapObjectCreationInfo info = new MapObjectCreationInfo();
						info.Position = SafeObjMan.GetPosition(sceneMan, mobPropertyName + "offset") + center;
						info.Rotation = new Rotation(SafeObjMan.GetFloat(sceneMan, mobPropertyName + "yaw"), Rotation.Empty.Pitch, Rotation.Empty.Roll);
						info.GroupName = SafeObjMan.GetString(sceneMan, mobPropertyName + "groupName");
						string visObject = SafeObjMan.GetDBID(sceneMan, mobPropertyName + "visualMob");
						int permanentDeviceID = map.MapEditorMapObjectContainer.AddMapObject(new MapObjectType(MapObjectFactory.Type.ClientSpawnPoint, visObject), false, info);
						IMapObject mapObject;
						if (map.MapEditorMapObjectContainer.MapObjects.TryGetValue(permanentDeviceID, out mapObject))
						{
							ClientSpawnPoint clientSpawnPoint = mapObject as ClientSpawnPoint;
							if (clientSpawnPoint != null)
							{
								clientSpawnPoint.Scene = scene;
								clientSpawnPoint.ScriptID = SafeObjMan.GetDBID(sceneMan, mobPropertyName + "scriptID");
								clientSpawnPoint.ClientSpawnPointData = new MobClientSpawnPointData();
							}
						}
					}
					int deviceCount = SafeObjMan.GetInt(sceneMan, "devices");
					for (int deviceIndex = 0; deviceIndex < deviceCount; deviceIndex++)
					{
						string devicePropertyName = string.Format("devices.[{0}].", deviceIndex);
						MapObjectCreationInfo info2 = new MapObjectCreationInfo();
						info2.Position = SafeObjMan.GetPosition(sceneMan, devicePropertyName + "offset") + center;
						info2.Rotation = new Rotation(SafeObjMan.GetFloat(sceneMan, devicePropertyName + "yaw"), Rotation.Empty.Pitch, Rotation.Empty.Roll);
						string device = SafeObjMan.GetDBID(sceneMan, devicePropertyName + "device");
						int permanentDeviceID2 = map.MapEditorMapObjectContainer.AddMapObject(new MapObjectType(MapObjectFactory.Type.ClientSpawnPoint, device), false, info2);
						IMapObject mapObject2;
						if (map.MapEditorMapObjectContainer.MapObjects.TryGetValue(permanentDeviceID2, out mapObject2))
						{
							ClientSpawnPoint clientSpawnPoint2 = mapObject2 as ClientSpawnPoint;
							if (clientSpawnPoint2 != null)
							{
								clientSpawnPoint2.Scene = scene;
								clientSpawnPoint2.ScriptID = SafeObjMan.GetDBID(sceneMan, devicePropertyName + "scriptID");
								clientSpawnPoint2.ClientSpawnPointData = new DeviceClientSpawnPointData
								{
									VisualState = SafeObjMan.GetInt(sceneMan, devicePropertyName + "visualState")
								};
							}
						}
					}
					int pathCount = SafeObjMan.GetInt(sceneMan, "paths");
					List<ClientPatrolNode> addedClientPatrolNodes = new List<ClientPatrolNode>();
					for (int pathIndex = 0; pathIndex < pathCount; pathIndex++)
					{
						string pathPropertyName = string.Format("paths.[{0}].", pathIndex);
						string groupName = SafeObjMan.GetString(sceneMan, pathPropertyName + "groupName");
						addedClientPatrolNodes.Clear();
						int pointCount = SafeObjMan.GetInt(sceneMan, pathPropertyName + "points");
						for (int pointIndex = 0; pointIndex < pointCount; pointIndex++)
						{
							MapObjectCreationInfo info3 = new MapObjectCreationInfo();
							info3.Position = SafeObjMan.GetPosition(sceneMan, string.Format(pathPropertyName + "points.[{0}]", pointIndex)) + center;
							info3.GroupName = groupName;
							int clientPatrolNodeID = map.MapEditorMapObjectContainer.AddMapObject(new MapObjectType(MapObjectFactory.Type.ClientPatrolNode, string.Empty), false, info3);
							IMapObject mapObject3;
							if (map.MapEditorMapObjectContainer.MapObjects.TryGetValue(clientPatrolNodeID, out mapObject3))
							{
								ClientPatrolNode clientPatrolNode = mapObject3 as ClientPatrolNode;
								if (clientPatrolNode != null)
								{
									clientPatrolNode.Scene = scene;
									addedClientPatrolNodes.Add(clientPatrolNode);
								}
							}
						}
						int clientPatrolNodeCount = addedClientPatrolNodes.Count;
						if (clientPatrolNodeCount > 0)
						{
							addedClientPatrolNodes[0].ScriptID = SafeObjMan.GetDBID(sceneMan, pathPropertyName + "scriptID");
							addedClientPatrolNodes[addedClientPatrolNodes.Count - 1].Rotation = new Rotation(SafeObjMan.GetFloat(sceneMan, pathPropertyName + "yaw"), Rotation.Empty.Pitch, Rotation.Empty.Roll);
							if (clientPatrolNodeCount > 1)
							{
								for (int clientPatrolNodeIndex = 0; clientPatrolNodeIndex < clientPatrolNodeCount - 1; clientPatrolNodeIndex++)
								{
									map.MapEditorMapObjectContainer.AddLink(addedClientPatrolNodes[clientPatrolNodeIndex], addedClientPatrolNodes[clientPatrolNodeIndex + 1], null);
								}
							}
						}
					}
				}
			}
		}

		// Token: 0x06001443 RID: 5187 RVA: 0x0009348D File Offset: 0x0009248D
		public int GetProgressSteps(bool forSave)
		{
			if (forSave)
			{
				return 1;
			}
			return 1;
		}

		// Token: 0x06001444 RID: 5188 RVA: 0x00093498 File Offset: 0x00092498
		public bool Save(MapEditorMap map, MainForm.Context context, IProgressContainer progressContainer)
		{
			if (progressContainer != null)
			{
				progressContainer.AddString(Strings.SAVING_ROUTE_POINTS);
			}
			IDatabase mainDb = IDatabase.GetMainDatabase();
			if (mainDb != null)
			{
				List<string> newUsedScenes = new List<string>();
				Dictionary<string, ClientObjectsDataSource.SceneData> scenes = new Dictionary<string, ClientObjectsDataSource.SceneData>();
				ClientObjectsDataSource.GetScenes(scenes, map.MapEditorMapObjectContainer);
				ObjMan.StartMassEditing();
				foreach (KeyValuePair<string, ClientObjectsDataSource.SceneData> keyValuePair in scenes)
				{
					string scene = keyValuePair.Key;
					if (!string.IsNullOrEmpty(scene))
					{
						DBID sceneDBID = mainDb.GetDBIDByName(scene);
						IObjMan sceneMan;
						if (sceneDBID.IsEmpty())
						{
							sceneDBID = IDatabase.CreateDBIDByName(scene);
							sceneMan = mainDb.CreateNewObject(ClientSpawnPoint.SceneDBType);
							if (sceneMan != null)
							{
								mainDb.AddNewObject(sceneDBID, sceneMan);
							}
						}
						else
						{
							sceneMan = mainDb.GetManipulator(sceneDBID);
						}
						if (sceneMan != null)
						{
							ClientObjectsDataSource.SaveScene(map, sceneMan, keyValuePair.Value);
							newUsedScenes.Add(scene);
							this.usedScenes.Remove(scene);
						}
					}
				}
				foreach (string scene2 in this.usedScenes)
				{
					DBID sceneDBID2 = mainDb.GetDBIDByName(scene2);
					mainDb.RemoveObject(sceneDBID2);
				}
				ObjMan.StopMassEditing();
				this.usedScenes.Clear();
				this.usedScenes.AddRange(newUsedScenes);
			}
			if (progressContainer != null)
			{
				progressContainer.Progress++;
			}
			return true;
		}

		// Token: 0x06001445 RID: 5189 RVA: 0x0009361C File Offset: 0x0009261C
		public bool Load(MapEditorMap map, MainForm.Context context, IProgressContainer progressContainer, out bool somethingCreated)
		{
			Vec3 minMapPosition = new Vec3((double)(map.Data.MinXMinYPatchCoords.X * Constants.PatchSize), (double)(map.Data.MinXMinYPatchCoords.Y * Constants.PatchSize), 0.0);
			Vec3 maxMapPosition = new Vec3((double)((map.Data.MinXMinYPatchCoords.X + map.Data.MapSize.X) * Constants.PatchSize), (double)((map.Data.MinXMinYPatchCoords.Y + map.Data.MapSize.Y) * Constants.PatchSize), 0.0);
			somethingCreated = false;
			if (progressContainer != null)
			{
				progressContainer.AddString(Strings.LOADING_ROUTE_POINTS);
			}
			this.usedScenes.Clear();
			IDatabase mainDb = IDatabase.GetMainDatabase();
			if (mainDb != null)
			{
				DBItemSource sceneSource = new DBItemSource(EditorEnvironment.DataFolder + Constants.ContinentFolder(map.Data.ContinentName) + ClientSpawnPoint.SceneFolder, ClientSpawnPoint.SceneDBType, false, true);
				IEnumerable<string> scenes = sceneSource.Items;
				foreach (string scene in scenes)
				{
					DBID sceneDBID = mainDb.GetDBIDByName(scene);
					IObjMan sceneMan = mainDb.GetManipulator(sceneDBID);
					if (sceneMan != null)
					{
						bool inside;
						ClientObjectsDataSource.LoadScene(map, scene, sceneMan, minMapPosition, maxMapPosition, out inside);
						if (inside)
						{
							this.usedScenes.Add(scene);
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

		// Token: 0x04000E4E RID: 3662
		private static readonly ClientObjectsDataSource.ClientSpawnPointIDComparer clientSpawnPointIDComparer = new ClientObjectsDataSource.ClientSpawnPointIDComparer();

		// Token: 0x04000E4F RID: 3663
		private static readonly ClientObjectsDataSource.ClientPatrolNodeListIDComparer clientPatrolNodeListIDComparer = new ClientObjectsDataSource.ClientPatrolNodeListIDComparer();

		// Token: 0x04000E50 RID: 3664
		private readonly List<string> usedScenes = new List<string>();

		// Token: 0x020001A0 RID: 416
		private class ClientSpawnPointIDComparer : IComparer<ClientSpawnPoint>
		{
			// Token: 0x06001448 RID: 5192 RVA: 0x000937E9 File Offset: 0x000927E9
			public int Compare(ClientSpawnPoint left, ClientSpawnPoint right)
			{
				return MapObjectContainer.MapObjectIDComparer.Compare(left, right);
			}
		}

		// Token: 0x020001A1 RID: 417
		private class ClientPatrolNodeListIDComparer : IComparer<KeyValuePair<string, List<ClientPatrolNode>>>
		{
			// Token: 0x0600144A RID: 5194 RVA: 0x00093800 File Offset: 0x00092800
			public int Compare(KeyValuePair<string, List<ClientPatrolNode>> left, KeyValuePair<string, List<ClientPatrolNode>> right)
			{
				if (left.Value != null && right.Value != null)
				{
					int minLeft = -1;
					int minRight = -1;
					for (int index = 0; index < left.Value.Count; index++)
					{
						if (left.Value[index] != null && (minLeft == -1 || left.Value[index].ID < minLeft))
						{
							minLeft = left.Value[index].ID;
						}
					}
					for (int index2 = 0; index2 < right.Value.Count; index2++)
					{
						if (right.Value[index2] != null && (minRight == -1 || right.Value[index2].ID < minRight))
						{
							minRight = right.Value[index2].ID;
						}
					}
					if (minLeft < minRight)
					{
						return -1;
					}
					if (minLeft > minRight)
					{
						return 1;
					}
				}
				return 0;
			}
		}

		// Token: 0x020001A2 RID: 418
		private class SceneData
		{
			// Token: 0x0600144C RID: 5196 RVA: 0x000938E4 File Offset: 0x000928E4
			private void CalculateCenter()
			{
				this.center = Position.Empty;
				int count = 0;
				foreach (ClientSpawnPoint clientSpawnPoint in this.clientSpawnPoints)
				{
					if (clientSpawnPoint != null)
					{
						this.center += clientSpawnPoint.Position;
						count++;
					}
				}
				foreach (KeyValuePair<string, List<ClientPatrolNode>> keyValuePair in this.clientPatrolNodes)
				{
					foreach (ClientPatrolNode clientPatrolNode in keyValuePair.Value)
					{
						if (clientPatrolNode != null)
						{
							this.center += clientPatrolNode.Position;
							count++;
						}
					}
				}
				if (count > 0)
				{
					this.center /= (double)count;
				}
			}

			// Token: 0x170003F9 RID: 1017
			// (get) Token: 0x0600144D RID: 5197 RVA: 0x00093A0C File Offset: 0x00092A0C
			public Position Center
			{
				get
				{
					this.CalculateCenter();
					return this.center;
				}
			}

			// Token: 0x170003FA RID: 1018
			// (get) Token: 0x0600144E RID: 5198 RVA: 0x00093A1A File Offset: 0x00092A1A
			public List<ClientSpawnPoint> ClientSpawnPoints
			{
				get
				{
					return this.clientSpawnPoints;
				}
			}

			// Token: 0x170003FB RID: 1019
			// (get) Token: 0x0600144F RID: 5199 RVA: 0x00093A22 File Offset: 0x00092A22
			public List<KeyValuePair<string, List<ClientPatrolNode>>> ClientPatrolNodes
			{
				get
				{
					return this.clientPatrolNodes;
				}
			}

			// Token: 0x04000E51 RID: 3665
			private Position center = Position.Empty;

			// Token: 0x04000E52 RID: 3666
			private readonly List<ClientSpawnPoint> clientSpawnPoints = new List<ClientSpawnPoint>();

			// Token: 0x04000E53 RID: 3667
			private readonly List<KeyValuePair<string, List<ClientPatrolNode>>> clientPatrolNodes = new List<KeyValuePair<string, List<ClientPatrolNode>>>();
		}
	}
}
