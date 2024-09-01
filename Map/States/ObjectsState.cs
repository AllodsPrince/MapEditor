using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using Db;
using InputState;
using LauncherTools.InputState;
using MapEditor.Forms.ExtendedSoundBrowser;
using MapEditor.Forms.MultiObjectBrowser;
using MapEditor.Forms.ObjectsBrowser;
using MapEditor.Forms.TypedObjectsBrowser;
using MapEditor.Forms.TypedObjectsBrowser.DataProviders;
using MapEditor.Map.DataProviders;
using MapEditor.Map.Dialogs;
using MapEditor.Map.MapObjects;
using MapEditor.Resources.Strings;
using MapEditor.Scene;
using ModelEditor.Forms.Main;
using Operations;
using Tools.DBGameObjects;
using Tools.Geometry;
using Tools.Groups;
using Tools.InputState;
using Tools.ItemDataContainer;
using Tools.LinkContainer;
using Tools.MapObjects;
using Tools.PropertyControl;
using Tools.SafeObjMan;
using Tools.Statusbar;
using Tools.StringCollector;
using Tools.ValueCollector;

namespace MapEditor.Map.States
{
	// Token: 0x02000273 RID: 627
	internal class ObjectsState : State
	{
		// Token: 0x06001D87 RID: 7559 RVA: 0x000BC96D File Offset: 0x000BB96D
		public static bool CheckForObjectsChanges(IOperation operation)
		{
			return true;
		}

		// Token: 0x06001D88 RID: 7560 RVA: 0x000BC970 File Offset: 0x000BB970
		private void OnGroupSelectedObjects(MethodArgs methodArgs)
		{
			Dictionary<IMapObject, IMapObject> selectedList = new Dictionary<IMapObject, IMapObject>(this.selector.MapObjects);
			this.selector.Clear();
			this.groupContainer.GroupSelectedObjects(selectedList);
			foreach (KeyValuePair<IMapObject, IMapObject> keyValuePair in selectedList)
			{
				this.selector.Add(GroupContainer.GroupSelectionMode.Free, keyValuePair.Key);
			}
		}

		// Token: 0x06001D89 RID: 7561 RVA: 0x000BC9F4 File Offset: 0x000BB9F4
		private void OnUngroupSelectedObjects(MethodArgs methodArgs)
		{
			Dictionary<IMapObject, IMapObject> selectedList = new Dictionary<IMapObject, IMapObject>(this.selector.MapObjects);
			this.selector.Clear();
			this.groupContainer.UngroupSelectedObjects(selectedList);
			foreach (KeyValuePair<IMapObject, IMapObject> keyValuePair in selectedList)
			{
				this.selector.Add(GroupContainer.GroupSelectionMode.Free, keyValuePair.Key);
			}
		}

		// Token: 0x06001D8A RID: 7562 RVA: 0x000BCA78 File Offset: 0x000BBA78
		private void OnFlattenSelectedObjects(MethodArgs methodArgs)
		{
			Dictionary<IMapObject, IMapObject> selectedList = new Dictionary<IMapObject, IMapObject>(this.selector.MapObjects);
			this.selector.Clear();
			this.groupContainer.FlattenGroup(selectedList);
			foreach (KeyValuePair<IMapObject, IMapObject> keyValuePair in selectedList)
			{
				this.selector.Add(GroupContainer.GroupSelectionMode.Free, keyValuePair.Key);
			}
		}

		// Token: 0x06001D8B RID: 7563 RVA: 0x000BCAFC File Offset: 0x000BBAFC
		private void OnGroupSelectionTypeFree(MethodArgs methodArgs)
		{
			this.selector.GroupSelectionMode = GroupContainer.GroupSelectionMode.Free;
		}

		// Token: 0x06001D8C RID: 7564 RVA: 0x000BCB0A File Offset: 0x000BBB0A
		private void OnGroupSelectionTypeOneLevel(MethodArgs methodArgs)
		{
			this.selector.GroupSelectionMode = GroupContainer.GroupSelectionMode.OneLevel;
		}

		// Token: 0x06001D8D RID: 7565 RVA: 0x000BCB18 File Offset: 0x000BBB18
		private void OnGroupSelectionTypeAllLevels(MethodArgs methodArgs)
		{
			this.selector.GroupSelectionMode = GroupContainer.GroupSelectionMode.AllLevels;
		}

		// Token: 0x06001D8E RID: 7566 RVA: 0x000BCB26 File Offset: 0x000BBB26
		private void OnOperationUndo(OperationContainer _operationContainer, IOperation operation, int index, bool result)
		{
			if (result && ObjectsState.CheckForObjectsChanges(operation))
			{
				this.ShowProperties(false, false, ObjectsState.PropetiesType.Default);
				this.UpdatePosition(0, 0, false);
			}
		}

		// Token: 0x06001D8F RID: 7567 RVA: 0x000BCB46 File Offset: 0x000BBB46
		private void OnOperationRedo(OperationContainer _operationContainer, IOperation operation, int index, bool result)
		{
			if (result && ObjectsState.CheckForObjectsChanges(operation))
			{
				this.ShowProperties(false, false, ObjectsState.PropetiesType.Default);
				this.UpdatePosition(0, 0, false);
			}
		}

		// Token: 0x06001D90 RID: 7568 RVA: 0x000BCB66 File Offset: 0x000BBB66
		private void OnProperties(MethodArgs methodArgs)
		{
			this.ShowProperties(true, true, ObjectsState.PropetiesType.Default);
		}

		// Token: 0x06001D91 RID: 7569 RVA: 0x000BCB71 File Offset: 0x000BBB71
		private void OnQuickProperties(MethodArgs methodArgs)
		{
			this.ShowProperties(false, true, ObjectsState.PropetiesType.Default);
		}

		// Token: 0x06001D92 RID: 7570 RVA: 0x000BCB7C File Offset: 0x000BBB7C
		private void OnSpecialProperties(MethodArgs methodArgs)
		{
			this.ShowProperties(true, true, ObjectsState.PropetiesType.Special);
		}

		// Token: 0x06001D93 RID: 7571 RVA: 0x000BCB88 File Offset: 0x000BBB88
		private void OnStatsProperties(MethodArgs methodArgs)
		{
			if (this.context != null && this.selector.MapObjects.Count == 1)
			{
				string stats = string.Empty;
				using (Dictionary<IMapObject, IMapObject>.Enumerator enumerator = this.selector.MapObjects.GetEnumerator())
				{
					if (enumerator.MoveNext())
					{
						KeyValuePair<IMapObject, IMapObject> keyValuePair = enumerator.Current;
						IMapObjectInterfaceExtention mapObjectInterfaceExtention = keyValuePair.Key as IMapObjectInterfaceExtention;
						if (mapObjectInterfaceExtention != null)
						{
							stats = mapObjectInterfaceExtention.GetStatsForDBBrowse();
						}
					}
				}
				if (this.context != null)
				{
					this.context.SelectExistingObjectInDatabaseEditor(stats);
				}
			}
		}

		// Token: 0x06001D94 RID: 7572 RVA: 0x000BCC28 File Offset: 0x000BBC28
		private void OnStatsSpecialProperties(MethodArgs methodArgs)
		{
			if (this.context != null && this.selector.MapObjects.Count == 1)
			{
				string stats = string.Empty;
				using (Dictionary<IMapObject, IMapObject>.Enumerator enumerator = this.selector.MapObjects.GetEnumerator())
				{
					if (enumerator.MoveNext())
					{
						KeyValuePair<IMapObject, IMapObject> keyValuePair = enumerator.Current;
						IMapObjectInterfaceExtention mapObjectInterfaceExtention = keyValuePair.Key as IMapObjectInterfaceExtention;
						if (mapObjectInterfaceExtention != null)
						{
							stats = mapObjectInterfaceExtention.GetSpecialStatsForDBBrowse();
						}
					}
				}
				if (this.context != null)
				{
					this.context.SelectExistingObjectInDatabaseEditor(stats);
				}
			}
		}

		// Token: 0x06001D95 RID: 7573 RVA: 0x000BCCC8 File Offset: 0x000BBCC8
		private void OnSpawnTuner(MethodArgs methodArgs)
		{
			this.ShowProperties(true, true, ObjectsState.PropetiesType.SpawnTuner);
		}

		// Token: 0x06001D96 RID: 7574 RVA: 0x000BCCD4 File Offset: 0x000BBCD4
		private bool ShowStartPointProperties(IList<StartPoint> startPoints)
		{
			if (startPoints == null || startPoints.Count != 1)
			{
				return false;
			}
			ObjectsBrowserForm startPointBrowserForm = new ObjectsBrowserForm(new DBItemSource(EditorEnvironment.DataFolder + StartPoint.CharacterFolder, StartPoint.CharacterDBType, false, false), this.itemDataContainer, EditorEnvironment.EditorFormsFolder + "StartPointItemList.xml", EditorEnvironment.EditorFolder + "Filters/StartPointFilters.xml");
			startPointBrowserForm.SelectedObject = startPoints[0].Character;
			DialogResult dialogResult = startPointBrowserForm.ShowDialog(this.parentForm);
			if (dialogResult == DialogResult.OK)
			{
				bool transactionInProgress = this.operationContainer.DoesTransactionInProgress;
				if (!transactionInProgress)
				{
					this.operationContainer.BeginTransaction();
				}
				startPoints[0].Character = startPointBrowserForm.SelectedObject;
				this.mapObjectContainer.InvokeModified();
				if (!transactionInProgress)
				{
					this.operationContainer.EndTransaction();
				}
			}
			return true;
		}

		// Token: 0x06001D97 RID: 7575 RVA: 0x000BCDA0 File Offset: 0x000BBDA0
		private bool ShowSpawnPointProperties(IEnumerable<SpawnPoint> spawnPoints, ValueCollector<SpawnPointType> spawnPointTypeCollector, ValueCollector<SpawnTableType> spawnTableTypeCollector, ValueCollector<string> spawnTableCollector)
		{
			if (string.IsNullOrEmpty(this.continentName))
			{
				return false;
			}
			if (this.continentType != ContinentType.AstralHub && this.continentType != ContinentType.Continent)
			{
				return false;
			}
			SpawnTableParams spawnTableParams = new SpawnTableParams(this.continentName, this.continentType);
			spawnTableParams.SelectedSpawnPointType = spawnPointTypeCollector.Collected;
			spawnTableParams.Name = spawnTableCollector.Collected;
			spawnTableParams.SelectedSpawnTableType = spawnTableTypeCollector.Collected;
			TypedObjectsBrowserForm spawnTablesBrowserForm = new TypedObjectsBrowserForm(spawnTableParams, this.itemDataContainer, EditorEnvironment.EditorFormsFolder + "SpawnTableBrowserForm.xml", EditorEnvironment.EditorFormsFolder + "SpawnTableItemList.xml", EditorEnvironment.EditorFolder + "Filters/SpawnTableFilters.xml");
			DialogResult dialogResult = spawnTablesBrowserForm.ShowDialog(this.parentForm);
			if (dialogResult == DialogResult.OK)
			{
				bool transactionInProgress = this.operationContainer.DoesTransactionInProgress;
				if (!transactionInProgress)
				{
					this.operationContainer.BeginTransaction();
				}
				SpawnPointType selectedSpawnPointType = spawnTableParams.SelectedSpawnPointType;
				if (spawnPointTypeCollector.Collected != selectedSpawnPointType && selectedSpawnPointType != SpawnPointTypeCollector.Undefined)
				{
					foreach (SpawnPoint spawnPoint in spawnPoints)
					{
						spawnPoint.SpawnPointType = selectedSpawnPointType;
					}
					this.mapObjectContainer.InvokeModified();
				}
				string selectedSpawnTable = spawnTableParams.Name;
				if (spawnTableCollector.Collected != selectedSpawnTable && selectedSpawnTable != StringCollector.Undefined)
				{
					foreach (SpawnPoint spawnPoint2 in spawnPoints)
					{
						spawnPoint2.SpawnTable = selectedSpawnTable;
					}
					this.mapObjectContainer.InvokeModified();
				}
				if (!transactionInProgress)
				{
					this.operationContainer.EndTransaction();
				}
			}
			return true;
		}

		// Token: 0x06001D98 RID: 7576 RVA: 0x000BCF5C File Offset: 0x000BBF5C
		private bool ShowScriptAreaProperties(IEnumerable<ScriptArea> scriptAreas, ValueCollector<ScriptAreaType> scriptAreaTypeCollector, ValueCollector<string> scriptZoneCollector)
		{
			if (string.IsNullOrEmpty(this.continentName))
			{
				return false;
			}
			ScriptZoneParams scriptZoneParams = new ScriptZoneParams(this.continentName);
			scriptZoneParams.SelectedScriptAreaType = scriptAreaTypeCollector.Collected;
			scriptZoneParams.Name = scriptZoneCollector.Collected;
			TypedObjectsBrowserForm scriptZonesBrowserForm = new TypedObjectsBrowserForm(scriptZoneParams, this.itemDataContainer, EditorEnvironment.EditorFormsFolder + "ScriptZoneBrowserForm.xml", EditorEnvironment.EditorFormsFolder + "ScriptZoneItemList.xml", EditorEnvironment.EditorFolder + "Filters/ScriptZoneFilters.xml");
			DialogResult dialogResult = scriptZonesBrowserForm.ShowDialog(this.parentForm);
			if (dialogResult == DialogResult.OK)
			{
				bool transactionInProgress = this.operationContainer.DoesTransactionInProgress;
				if (!transactionInProgress)
				{
					this.operationContainer.BeginTransaction();
				}
				ScriptAreaType selectedScriptAreaType = scriptZoneParams.SelectedScriptAreaType;
				if (scriptAreaTypeCollector.Collected != selectedScriptAreaType && selectedScriptAreaType != ScriptAreaTypeCollector.Undefined)
				{
					foreach (ScriptArea scriptArea in scriptAreas)
					{
						scriptArea.ScriptAreaType = selectedScriptAreaType;
					}
					this.mapObjectContainer.InvokeModified();
				}
				string selectedScriptZone = scriptZoneParams.Name;
				if (scriptZoneCollector.Collected != selectedScriptZone && selectedScriptZone != StringCollector.Undefined)
				{
					foreach (ScriptArea scriptArea2 in scriptAreas)
					{
						scriptArea2.ScriptZone = selectedScriptZone;
					}
					this.mapObjectContainer.InvokeModified();
				}
				if (!transactionInProgress)
				{
					this.operationContainer.EndTransaction();
				}
			}
			return true;
		}

		// Token: 0x06001D99 RID: 7577 RVA: 0x000BD0F0 File Offset: 0x000BC0F0
		private bool ShowZoneLocatorProperties(IEnumerable<ZoneLocator> zoneLocators, ValueCollector<string> mapZoneCollector)
		{
			if (string.IsNullOrEmpty(this.continentName))
			{
				return false;
			}
			ObjectsBrowserForm zoneLocatorBrowserForm = new ObjectsBrowserForm(new DBItemSource(EditorEnvironment.DataFolder + Constants.ContinentFolder(this.continentName) + ZoneLocator.ZoneFolder, ZoneLocator.ZoneDBType, false, false), this.itemDataContainer, EditorEnvironment.EditorFormsFolder + "ZoneLocatorItemList.xml", EditorEnvironment.EditorFolder + "Filters/ZoneLocatorFilters.xml");
			zoneLocatorBrowserForm.SelectedObject = mapZoneCollector.Collected;
			DialogResult dialogResult = zoneLocatorBrowserForm.ShowDialog(this.parentForm);
			if (dialogResult == DialogResult.OK)
			{
				bool transactionInProgress = this.operationContainer.DoesTransactionInProgress;
				if (!transactionInProgress)
				{
					this.operationContainer.BeginTransaction();
				}
				string selectedMapZone = zoneLocatorBrowserForm.SelectedObject;
				if (mapZoneCollector.Collected != selectedMapZone && selectedMapZone != StringCollector.Undefined)
				{
					foreach (ZoneLocator zoneLocator in zoneLocators)
					{
						zoneLocator.MapZone = selectedMapZone;
					}
					this.mapObjectContainer.InvokeModified();
				}
				if (!transactionInProgress)
				{
					this.operationContainer.EndTransaction();
				}
			}
			return true;
		}

		// Token: 0x06001D9A RID: 7578 RVA: 0x000BD218 File Offset: 0x000BC218
		private bool ShowRoutePointProperties(IEnumerable<RoutePoint> routePoints, ValueCollector<string> routeCollector)
		{
			if (string.IsNullOrEmpty(this.continentName))
			{
				return false;
			}
			ObjectsBrowserForm routePointBrowserForm = new ObjectsBrowserForm(new DBItemSource(EditorEnvironment.DataFolder + Constants.ContinentFolder(this.continentName) + RoutePoint.TourFolder, RoutePoint.TourDBType, false, false), this.itemDataContainer, EditorEnvironment.EditorFormsFolder + "RoutePointItemList.xml", EditorEnvironment.EditorFolder + "Filters/RoutePointFilters.xml");
			routePointBrowserForm.SelectedObject = routeCollector.Collected;
			DialogResult dialogResult = routePointBrowserForm.ShowDialog(this.parentForm);
			if (dialogResult == DialogResult.OK)
			{
				bool transactionInProgress = this.operationContainer.DoesTransactionInProgress;
				if (!transactionInProgress)
				{
					this.operationContainer.BeginTransaction();
				}
				string selectedRoute = routePointBrowserForm.SelectedObject;
				if (routeCollector.Collected != selectedRoute && selectedRoute != StringCollector.Undefined)
				{
					foreach (RoutePoint routePoint in routePoints)
					{
						routePoint.Route = selectedRoute;
					}
					this.mapObjectContainer.InvokeModified();
				}
				if (!transactionInProgress)
				{
					this.operationContainer.EndTransaction();
				}
			}
			return true;
		}

		// Token: 0x06001D9B RID: 7579 RVA: 0x000BD340 File Offset: 0x000BC340
		private bool ShowPatrolNodeProperties(IEnumerable<IMapObject> mapObjects, ValueCollector<string> scriptCollector)
		{
			if (string.IsNullOrEmpty(this.continentName))
			{
				return false;
			}
			ObjectsBrowserForm scriptBrowserForm = new ObjectsBrowserForm(new DBItemSource(EditorEnvironment.DataFolder + Constants.ContinentFolder(this.continentName) + PatrolNode.ScriptFolder, PatrolNode.ScriptDBType, false, false), this.itemDataContainer, EditorEnvironment.EditorFormsFolder + "PatrolScriptItemList.xml", EditorEnvironment.EditorFolder + "Filters/PatrolScriptFilters.xml");
			scriptBrowserForm.SelectedObject = scriptCollector.Collected;
			DialogResult dialogResult = scriptBrowserForm.ShowDialog(this.parentForm);
			if (dialogResult == DialogResult.OK)
			{
				bool transactionInProgress = this.operationContainer.DoesTransactionInProgress;
				if (!transactionInProgress)
				{
					this.operationContainer.BeginTransaction();
				}
				string selectedScript = scriptBrowserForm.SelectedObject;
				if (scriptCollector.Collected != selectedScript && selectedScript != StringCollector.Undefined)
				{
					foreach (IMapObject mapObject in mapObjects)
					{
						PatrolNode.SetScript(mapObject, selectedScript);
					}
					this.mapObjectContainer.InvokeModified();
				}
				if (!transactionInProgress)
				{
					this.operationContainer.EndTransaction();
				}
			}
			return true;
		}

		// Token: 0x06001D9C RID: 7580 RVA: 0x000BD468 File Offset: 0x000BC468
		private void ShowPermanentDeviceProperties(IEnumerable<IMapObject> mapObjects, ValueCollector<string> deviceCollector)
		{
			ObjectsBrowserForm permanentDeviceBrowserForm = new ObjectsBrowserForm(new DBItemSource(EditorEnvironment.DataFolder, PermanentDevice.DeviceDBType, true, false), this.itemDataContainer, EditorEnvironment.EditorFormsFolder + "PermanentDeviceItemList.xml", EditorEnvironment.EditorFolder + "Filters/PermanentDeviceFilters.xml");
			permanentDeviceBrowserForm.SelectedObject = deviceCollector.Collected;
			DialogResult dialogResult = permanentDeviceBrowserForm.ShowDialog(this.parentForm);
			if (dialogResult == DialogResult.OK)
			{
				bool transactionInProgress = this.operationContainer.DoesTransactionInProgress;
				if (!transactionInProgress)
				{
					this.operationContainer.BeginTransaction();
				}
				string selectedDevice = permanentDeviceBrowserForm.SelectedObject;
				if (deviceCollector.Collected != selectedDevice && selectedDevice != StringCollector.Undefined)
				{
					foreach (IMapObject mapObject in mapObjects)
					{
						if (mapObject.Type.Type == MapObjectFactory.Type.StaticObject)
						{
							StaticObject staticObject = mapObject as StaticObject;
							if (staticObject != null)
							{
								if (!string.IsNullOrEmpty(selectedDevice))
								{
									Dictionary<IMapObject, ILinkData> links = this.mapObjectContainer.GetLinks(staticObject);
									this.selector.Remove(staticObject);
									this.mapObjectContainer.RemoveMapObject(staticObject);
									PermanentDevice permanentDevice = staticObject.CloneToPermanentDevice();
									permanentDevice.Device = selectedDevice;
									this.mapObjectContainer.AddMapObject(permanentDevice);
									if (links != null && links.Count > 0)
									{
										foreach (KeyValuePair<IMapObject, ILinkData> keyValuePair in links)
										{
											this.mapObjectContainer.AddLink(permanentDevice, keyValuePair.Key, keyValuePair.Value);
										}
									}
									this.selector.Add(permanentDevice);
								}
								else
								{
									staticObject.Device = selectedDevice;
								}
							}
						}
						else if (mapObject.Type.Type == MapObjectFactory.Type.PermanentDevice)
						{
							PermanentDevice permanentDevice2 = mapObject as PermanentDevice;
							if (permanentDevice2 != null)
							{
								if (string.IsNullOrEmpty(selectedDevice))
								{
									this.selector.Remove(permanentDevice2);
									Dictionary<IMapObject, ILinkData> links2 = this.mapObjectContainer.GetLinks(permanentDevice2);
									this.mapObjectContainer.RemoveMapObject(permanentDevice2);
									StaticObject staticObject2 = permanentDevice2.CloneToStaticObject();
									this.mapObjectContainer.AddMapObject(staticObject2);
									if (links2 != null && links2.Count > 0)
									{
										foreach (KeyValuePair<IMapObject, ILinkData> keyValuePair2 in links2)
										{
											this.mapObjectContainer.AddLink(staticObject2, keyValuePair2.Key, keyValuePair2.Value);
										}
									}
									this.selector.Add(staticObject2);
								}
								else
								{
									permanentDevice2.Device = selectedDevice;
								}
							}
						}
					}
					this.mapObjectContainer.InvokeModified();
				}
				if (!transactionInProgress)
				{
					this.operationContainer.EndTransaction();
				}
			}
		}

		// Token: 0x06001D9D RID: 7581 RVA: 0x000BD784 File Offset: 0x000BC784
		private bool ShowSpawnTunerProperties(IEnumerable<SpawnPoint> spawnPoints, ValueCollector<string> spawnTunerCollector)
		{
			if (string.IsNullOrEmpty(this.continentName))
			{
				return false;
			}
			ObjectsBrowserForm spawnTunerBrowserForm = new ObjectsBrowserForm(new DBItemSource(EditorEnvironment.DataFolder + Constants.ContinentFolder(this.continentName) + SpawnPoint.SpawnTunerFolder, SpawnPoint.SpawnTunerBaseType, true, false), this.itemDataContainer, EditorEnvironment.EditorFormsFolder + "SpawnTunerItemList.xml", EditorEnvironment.EditorFolder + "Filters/SpawnTunerFilters.xml");
			spawnTunerBrowserForm.SelectedObject = spawnTunerCollector.Collected;
			DialogResult dialogResult = spawnTunerBrowserForm.ShowDialog(this.parentForm);
			if (dialogResult == DialogResult.OK)
			{
				bool transactionInProgress = this.operationContainer.DoesTransactionInProgress;
				if (!transactionInProgress)
				{
					this.operationContainer.BeginTransaction();
				}
				string selectedSpawnTuner = spawnTunerBrowserForm.SelectedObject;
				if (spawnTunerCollector.Collected != selectedSpawnTuner && selectedSpawnTuner != StringCollector.Undefined)
				{
					foreach (SpawnPoint spawnPoint in spawnPoints)
					{
						if (spawnPoint != null)
						{
							spawnPoint.SpawnTuner = selectedSpawnTuner;
						}
					}
					this.mapObjectContainer.InvokeModified();
				}
				if (!transactionInProgress)
				{
					this.operationContainer.EndTransaction();
				}
			}
			return true;
		}

		// Token: 0x06001D9E RID: 7582 RVA: 0x000BD8B0 File Offset: 0x000BC8B0
		private void ShowClientSpawnPointSpecialProperties(IEnumerable<ClientSpawnPoint> spawnPoints, ValueCollector<string> spawnPointTypeCollector, ValueCollector<string> visObjectCollector)
		{
			ClientSpawnTableParams spawnTableParams = new ClientSpawnTableParams();
			spawnTableParams.SelectedSpawnPointType = spawnPointTypeCollector.Collected;
			spawnTableParams.Name = visObjectCollector.Collected;
			TypedObjectsBrowserForm spawnTablesBrowserForm = new TypedObjectsBrowserForm(spawnTableParams, this.itemDataContainer, EditorEnvironment.EditorFormsFolder + "ClientSpawnPointBrowserForm.xml", EditorEnvironment.EditorFormsFolder + "ClientSpawnPointVisObjectItemList.xml", EditorEnvironment.EditorFolder + "Filters/ClientSpawnPointVisObjectFilters.xml");
			DialogResult dialogResult = spawnTablesBrowserForm.ShowDialog(this.parentForm);
			if (dialogResult == DialogResult.OK)
			{
				bool transactionInProgress = this.operationContainer.DoesTransactionInProgress;
				if (!transactionInProgress)
				{
					this.operationContainer.BeginTransaction();
				}
				string selectedSpawnPointType = spawnTableParams.SelectedSpawnPointType;
				if (spawnPointTypeCollector.Collected != selectedSpawnPointType)
				{
					foreach (ClientSpawnPoint spawnPoint in spawnPoints)
					{
						spawnPoint.ClientSpawnPointData = ClientSpawnPointData.Create(selectedSpawnPointType);
						spawnPoint.VisObject = null;
					}
					this.mapObjectContainer.InvokeModified();
				}
				string selectedObject = spawnTableParams.Name;
				if (visObjectCollector.Collected != selectedObject)
				{
					foreach (ClientSpawnPoint spawnPoint2 in spawnPoints)
					{
						spawnPoint2.VisObject = selectedObject;
					}
					this.mapObjectContainer.InvokeModified();
				}
				if (!transactionInProgress)
				{
					this.operationContainer.EndTransaction();
				}
			}
		}

		// Token: 0x06001D9F RID: 7583 RVA: 0x000BDA28 File Offset: 0x000BCA28
		private bool ShowClientSpawnPointProperties(IEnumerable<IMapObject> mapObjects, ValueCollector<string> sceneCollector)
		{
			if (string.IsNullOrEmpty(this.continentName))
			{
				return false;
			}
			ObjectsBrowserForm clientSpawnPointBrowserForm = new ObjectsBrowserForm(new DBItemSource(EditorEnvironment.DataFolder + Constants.ContinentFolder(this.continentName) + ClientSpawnPoint.SceneFolder, ClientSpawnPoint.SceneDBType, false, false), this.itemDataContainer, EditorEnvironment.EditorFormsFolder + "ClientSpawnPointSceneItemList.xml", EditorEnvironment.EditorFolder + "Filters/ClientSpawnPointSceneFilters.xml");
			clientSpawnPointBrowserForm.SelectedObject = sceneCollector.Collected;
			DialogResult dialogResult = clientSpawnPointBrowserForm.ShowDialog(this.parentForm);
			if (dialogResult == DialogResult.OK)
			{
				bool transactionInProgress = this.operationContainer.DoesTransactionInProgress;
				if (!transactionInProgress)
				{
					this.operationContainer.BeginTransaction();
				}
				string selectedScene = clientSpawnPointBrowserForm.SelectedObject;
				if (sceneCollector.Collected != selectedScene && selectedScene != StringCollector.Undefined)
				{
					foreach (IMapObject mapObject in mapObjects)
					{
						if (mapObject.Type.Type == MapObjectFactory.Type.ClientSpawnPoint)
						{
							ClientSpawnPoint clientSpawnPoint = mapObject as ClientSpawnPoint;
							if (clientSpawnPoint != null)
							{
								clientSpawnPoint.Scene = selectedScene;
							}
						}
						else if (mapObject.Type.Type == MapObjectFactory.Type.ClientPatrolNode)
						{
							ClientPatrolNode clientPatrolNode = mapObject as ClientPatrolNode;
							if (clientPatrolNode != null)
							{
								clientPatrolNode.Scene = selectedScene;
							}
						}
					}
					this.mapObjectContainer.InvokeModified();
				}
				if (!transactionInProgress)
				{
					this.operationContainer.EndTransaction();
				}
			}
			return true;
		}

		// Token: 0x06001DA0 RID: 7584 RVA: 0x000BDBA8 File Offset: 0x000BCBA8
		private bool ShowExtendedSoundProperties(List<ExtendedSound> mapObjects, SoundCollector centralSoundCollector, SoundCollector sideSoundCollector, StringCollector nameCollector)
		{
			ExtendedSoundBrowserForm extendedSoundBrowserForm = new ExtendedSoundBrowserForm(EditorEnvironment.EditorFormsFolder + "ExtendedSoundBrowser.xml");
			ExtendedSoundParams collected = new ExtendedSoundParams(centralSoundCollector.Collected, sideSoundCollector.Collected, nameCollector.Collected);
			extendedSoundBrowserForm.SelectedObject = collected;
			DialogResult dialogResult = extendedSoundBrowserForm.ShowDialog(this.parentForm);
			if (dialogResult == DialogResult.OK)
			{
				bool transactionInProgress = this.operationContainer.DoesTransactionInProgress;
				if (!transactionInProgress)
				{
					this.operationContainer.BeginTransaction();
				}
				if (collected.CentralSound != centralSoundCollector.Collected || collected.SideSound != sideSoundCollector.Collected || collected.Name != nameCollector.Collected)
				{
					foreach (ExtendedSound extendedSound in mapObjects)
					{
						if (extendedSound != null)
						{
							extendedSound.CentralSound = collected.CentralSound;
							extendedSound.SideSound = collected.SideSound;
							extendedSound.Name = collected.Name;
						}
					}
					this.mapObjectContainer.InvokeModified();
				}
				if (!transactionInProgress)
				{
					this.operationContainer.EndTransaction();
				}
			}
			return true;
		}

		// Token: 0x06001DA1 RID: 7585 RVA: 0x000BDCCC File Offset: 0x000BCCCC
		private bool ShowProjectileProperties(IEnumerable<IMapObject> mapObjects, ValueCollector<string> sceneCollector)
		{
			if (string.IsNullOrEmpty(this.continentName))
			{
				return false;
			}
			ObjectsBrowserForm projectileBrowserForm = new ObjectsBrowserForm(new DBItemSource(string.Empty, Projectile.SceneDBType, false, false), this.itemDataContainer, EditorEnvironment.EditorFormsFolder + "ProjectileSceneItemList.xml", EditorEnvironment.EditorFolder + "Filters/ProjectileSceneFilters.xml");
			projectileBrowserForm.SelectedObject = sceneCollector.Collected;
			DialogResult dialogResult = projectileBrowserForm.ShowDialog(this.parentForm);
			if (dialogResult == DialogResult.OK)
			{
				bool transactionInProgress = this.operationContainer.DoesTransactionInProgress;
				if (!transactionInProgress)
				{
					this.operationContainer.BeginTransaction();
				}
				string selectedScene = projectileBrowserForm.SelectedObject;
				if (sceneCollector.Collected != selectedScene && selectedScene != StringCollector.Undefined)
				{
					foreach (IMapObject mapObject in mapObjects)
					{
						if (mapObject.Type.Type == MapObjectFactory.Type.Projectile)
						{
							Projectile projectile = mapObject as Projectile;
							if (projectile != null)
							{
								projectile.ProjectileDBID = selectedScene;
							}
						}
					}
					this.mapObjectContainer.InvokeModified();
				}
				if (!transactionInProgress)
				{
					this.operationContainer.EndTransaction();
				}
			}
			return true;
		}

		// Token: 0x06001DA2 RID: 7586 RVA: 0x000BDE04 File Offset: 0x000BCE04
		private bool ShowPlayerRespawnPlaceFactionProperties(IEnumerable<PlayerRespawnPlace> playerRespawnPlaces, ValueCollector<string> factionCollector)
		{
			ObjectsBrowserForm playerRespawnPlaceBrowserForm = new ObjectsBrowserForm(new DBItemSource(EditorEnvironment.DataFolder, PlayerRespawnPlace.FactionDBType, true, false), this.itemDataContainer, EditorEnvironment.EditorFormsFolder + "PlayerRespawnPlaceFactionItemList.xml", EditorEnvironment.EditorFolder + "Filters/PlayerRespawnPlacefactionFilters.xml");
			playerRespawnPlaceBrowserForm.SelectedObject = factionCollector.Collected;
			DialogResult dialogResult = playerRespawnPlaceBrowserForm.ShowDialog(this.parentForm);
			if (dialogResult == DialogResult.OK)
			{
				string selectedFaction = playerRespawnPlaceBrowserForm.SelectedObject;
				if (factionCollector.Collected != selectedFaction)
				{
					bool transactionInProgress = this.operationContainer.DoesTransactionInProgress;
					if (!transactionInProgress)
					{
						this.operationContainer.BeginTransaction();
					}
					foreach (PlayerRespawnPlace playerRespawnPlace in playerRespawnPlaces)
					{
						playerRespawnPlace.Faction = selectedFaction;
					}
					this.mapObjectContainer.InvokeModified();
					if (!transactionInProgress)
					{
						this.operationContainer.EndTransaction();
					}
				}
			}
			return true;
		}

		// Token: 0x06001DA3 RID: 7587 RVA: 0x000BDEF8 File Offset: 0x000BCEF8
		private bool ShowPlayerRespawnPlaceProperties(IEnumerable<PlayerRespawnPlace> playerRespawnPlaces, ValueCollector<string> deviceCollector)
		{
			ObjectsBrowserForm playerRespawnPlaceBrowserForm = new ObjectsBrowserForm(new DBItemSource(EditorEnvironment.DataFolder, PlayerRespawnPlace.DeviceDBType, false, false), this.itemDataContainer, EditorEnvironment.EditorFormsFolder + "PlayerRespawnPlaceItemList.xml", EditorEnvironment.EditorFolder + "Filters/PlayerRespawnPlaceFilters.xml");
			playerRespawnPlaceBrowserForm.SelectedObject = deviceCollector.Collected;
			DialogResult dialogResult = playerRespawnPlaceBrowserForm.ShowDialog(this.parentForm);
			if (dialogResult == DialogResult.OK)
			{
				string selectedDevice = playerRespawnPlaceBrowserForm.SelectedObject;
				if (deviceCollector.Collected != selectedDevice)
				{
					bool transactionInProgress = this.operationContainer.DoesTransactionInProgress;
					if (!transactionInProgress)
					{
						this.operationContainer.BeginTransaction();
					}
					foreach (PlayerRespawnPlace playerRespawnPlace in playerRespawnPlaces)
					{
						playerRespawnPlace.Device = selectedDevice;
					}
					this.mapObjectContainer.InvokeModified();
					if (!transactionInProgress)
					{
						this.operationContainer.EndTransaction();
					}
				}
			}
			return true;
		}

		// Token: 0x06001DA4 RID: 7588 RVA: 0x000BDFEC File Offset: 0x000BCFEC
		private void ShowProperties(bool showModalDialog, bool forceShowDialog, ObjectsState.PropetiesType properiesType)
		{
			if (this.selector.MapObjects.Count > 0)
			{
				bool modalDialogCreated = false;
				if (showModalDialog)
				{
					List<int> selectedSpecialTypes = new List<int>();
					for (int index = MapObjectFactory.Type.FirstSpecialType; index <= MapObjectFactory.Type.LastSpecialType; index++)
					{
						selectedSpecialTypes.Add(0);
					}
					foreach (KeyValuePair<IMapObject, IMapObject> keyValuePair in this.selector.MapObjects)
					{
						if (MapObjectFactory.Type.SpecialType(keyValuePair.Key.Type.Type))
						{
							List<int> list;
							int index3;
							(list = selectedSpecialTypes)[index3 = keyValuePair.Key.Type.Type - MapObjectFactory.Type.FirstSpecialType] = list[index3] + 1;
						}
					}
					int selectedTypesCount = 0;
					int selectedType = MapObjectFactory.Type.Unknown;
					for (int index2 = MapObjectFactory.Type.FirstSpecialType; index2 <= MapObjectFactory.Type.LastSpecialType; index2++)
					{
						if (selectedSpecialTypes[index2 - MapObjectFactory.Type.FirstSpecialType] > 0)
						{
							selectedTypesCount++;
							selectedType = index2;
							if (selectedTypesCount > 1)
							{
								break;
							}
						}
					}
					if (selectedTypesCount == 1 && selectedType == MapObjectFactory.Type.ClientSpawnPoint && properiesType == ObjectsState.PropetiesType.Special)
					{
						List<ClientSpawnPoint> mapObjects = new List<ClientSpawnPoint>();
						ValueCollector<string> spawnPointTypeCollector = new StringCollector();
						ValueCollector<string> visObjectCollector = new StringCollector();
						foreach (KeyValuePair<IMapObject, IMapObject> keyValuePair2 in this.selector.MapObjects)
						{
							if (keyValuePair2.Key.Type.Type == MapObjectFactory.Type.ClientSpawnPoint)
							{
								ClientSpawnPoint clientSpawnPoint = keyValuePair2.Key as ClientSpawnPoint;
								if (clientSpawnPoint != null)
								{
									visObjectCollector.Collect(clientSpawnPoint.VisObject);
									spawnPointTypeCollector.Collect((clientSpawnPoint.ClientSpawnPointData != null) ? clientSpawnPoint.ClientSpawnPointData.Type : string.Empty);
									mapObjects.Add(clientSpawnPoint);
								}
							}
						}
						if (mapObjects.Count > 0)
						{
							this.ShowClientSpawnPointSpecialProperties(mapObjects, spawnPointTypeCollector, visObjectCollector);
							modalDialogCreated = true;
						}
					}
					else if ((selectedTypesCount == 1 || selectedTypesCount == 2) && (selectedType == MapObjectFactory.Type.ClientSpawnPoint || selectedType == MapObjectFactory.Type.ClientPatrolNode))
					{
						List<IMapObject> mapObjects2 = new List<IMapObject>();
						ValueCollector<string> sceneCollector = new StringCollector();
						foreach (KeyValuePair<IMapObject, IMapObject> keyValuePair3 in this.selector.MapObjects)
						{
							if (keyValuePair3.Key.Type.Type == MapObjectFactory.Type.ClientSpawnPoint)
							{
								ClientSpawnPoint clientSpawnPoint2 = keyValuePair3.Key as ClientSpawnPoint;
								if (clientSpawnPoint2 != null)
								{
									sceneCollector.Collect(clientSpawnPoint2.Scene);
									mapObjects2.Add(clientSpawnPoint2);
								}
							}
							else if (keyValuePair3.Key.Type.Type == MapObjectFactory.Type.ClientPatrolNode)
							{
								ClientPatrolNode clientPatrolNode = keyValuePair3.Key as ClientPatrolNode;
								if (clientPatrolNode != null)
								{
									sceneCollector.Collect(clientPatrolNode.Scene);
									mapObjects2.Add(clientPatrolNode);
								}
							}
						}
						if (mapObjects2.Count > 0 && this.ShowClientSpawnPointProperties(mapObjects2, sceneCollector))
						{
							modalDialogCreated = true;
						}
					}
					else if (selectedTypesCount == 0 || (selectedTypesCount == 1 && selectedType == MapObjectFactory.Type.PermanentDevice))
					{
						List<IMapObject> mapObjects3 = new List<IMapObject>();
						ValueCollector<string> deviceCollector = new StringCollector();
						foreach (KeyValuePair<IMapObject, IMapObject> keyValuePair4 in this.selector.MapObjects)
						{
							if (keyValuePair4.Key.Type.Type == MapObjectFactory.Type.StaticObject)
							{
								StaticObject staticObject = keyValuePair4.Key as StaticObject;
								if (staticObject != null)
								{
									deviceCollector.Collect(staticObject.Device);
									mapObjects3.Add(staticObject);
								}
							}
							else if (keyValuePair4.Key.Type.Type == MapObjectFactory.Type.PermanentDevice)
							{
								PermanentDevice permanentDevice = keyValuePair4.Key as PermanentDevice;
								if (permanentDevice != null)
								{
									deviceCollector.Collect(permanentDevice.Device);
									mapObjects3.Add(permanentDevice);
								}
							}
						}
						this.ShowPermanentDeviceProperties(mapObjects3, deviceCollector);
						modalDialogCreated = true;
					}
					else if (selectedTypesCount == 1 && selectedType == MapObjectFactory.Type.PatrolNode && properiesType == ObjectsState.PropetiesType.Special)
					{
						List<IMapObject> mapObjects4 = new List<IMapObject>();
						ValueCollector<string> scriptCollector = new StringCollector();
						foreach (KeyValuePair<IMapObject, IMapObject> keyValuePair5 in this.selector.MapObjects)
						{
							string script;
							if (PatrolNode.GetScript(keyValuePair5.Key, out script))
							{
								scriptCollector.Collect(script);
								mapObjects4.Add(keyValuePair5.Key);
							}
						}
						if (mapObjects4.Count > 0 && this.ShowPatrolNodeProperties(mapObjects4, scriptCollector))
						{
							modalDialogCreated = true;
						}
					}
					else if (selectedTypesCount == 1 && selectedType == MapObjectFactory.Type.SpawnPoint && properiesType == ObjectsState.PropetiesType.SpawnTuner)
					{
						List<SpawnPoint> mapObjects5 = new List<SpawnPoint>();
						ValueCollector<string> spawnTunerCollector = new StringCollector();
						foreach (KeyValuePair<IMapObject, IMapObject> keyValuePair6 in this.selector.MapObjects)
						{
							SpawnPoint spawnPoint = keyValuePair6.Key as SpawnPoint;
							if (spawnPoint != null)
							{
								spawnTunerCollector.Collect(spawnPoint.SpawnTuner);
								mapObjects5.Add(spawnPoint);
							}
						}
						if (mapObjects5.Count > 0 && this.ShowSpawnTunerProperties(mapObjects5, spawnTunerCollector))
						{
							modalDialogCreated = true;
						}
					}
					else if (selectedTypesCount == 1 && selectedType == MapObjectFactory.Type.ExtendedSound)
					{
						if (properiesType == ObjectsState.PropetiesType.Default)
						{
							List<ExtendedSound> mapObjects6 = new List<ExtendedSound>();
							SoundCollector centralSoundCollector = new SoundCollector();
							SoundCollector sideSoundCollector = new SoundCollector();
							StringCollector nameCollector = new StringCollector();
							foreach (KeyValuePair<IMapObject, IMapObject> keyValuePair7 in this.selector.MapObjects)
							{
								ExtendedSound extendedSound = keyValuePair7.Key as ExtendedSound;
								if (extendedSound != null)
								{
									centralSoundCollector.Collect(extendedSound.CentralSound);
									sideSoundCollector.Collect(extendedSound.SideSound);
									nameCollector.Collect(extendedSound.Name);
									if (!mapObjects6.Contains(extendedSound))
									{
										mapObjects6.Add(extendedSound);
									}
									Dictionary<IMapObject, ILinkData> links = this.mapObjectContainer.GetLinksRecursively(extendedSound);
									foreach (IMapObject mapObject in links.Keys)
									{
										ExtendedSound linkedExtendedSound = mapObject as ExtendedSound;
										if (linkedExtendedSound != null && !mapObjects6.Contains(linkedExtendedSound))
										{
											centralSoundCollector.Collect(linkedExtendedSound.CentralSound);
											sideSoundCollector.Collect(linkedExtendedSound.SideSound);
											nameCollector.Collect(extendedSound.Name);
											mapObjects6.Add(linkedExtendedSound);
										}
									}
								}
							}
							if (mapObjects6.Count > 0 && this.ShowExtendedSoundProperties(mapObjects6, centralSoundCollector, sideSoundCollector, nameCollector))
							{
								modalDialogCreated = true;
							}
						}
					}
					else if (selectedTypesCount == 1)
					{
						if (selectedType == MapObjectFactory.Type.StartPoint)
						{
							List<StartPoint> startPoints = new List<StartPoint>();
							foreach (KeyValuePair<IMapObject, IMapObject> keyValuePair8 in this.selector.MapObjects)
							{
								if (keyValuePair8.Key.Type.Type == MapObjectFactory.Type.StartPoint)
								{
									StartPoint startPoint = keyValuePair8.Key as StartPoint;
									if (startPoint != null)
									{
										startPoints.Add(startPoint);
									}
								}
							}
							if (this.ShowStartPointProperties(startPoints))
							{
								modalDialogCreated = true;
							}
						}
						else if (selectedType == MapObjectFactory.Type.Graveyard)
						{
							modalDialogCreated = false;
						}
						else if (selectedType == MapObjectFactory.Type.SpawnPoint)
						{
							List<SpawnPoint> spawnPoints = new List<SpawnPoint>();
							ValueCollector<SpawnPointType> spawnPointTypeCollector2 = new SpawnPointTypeCollector();
							ValueCollector<SpawnTableType> spawnTableTypeCollector = new SpawnTableTypeCollector();
							ValueCollector<string> spawnTableCollector = new StringCollector();
							foreach (KeyValuePair<IMapObject, IMapObject> keyValuePair9 in this.selector.MapObjects)
							{
								if (keyValuePair9.Key.Type.Type == MapObjectFactory.Type.SpawnPoint)
								{
									SpawnPoint spawnPoint2 = keyValuePair9.Key as SpawnPoint;
									if (spawnPoint2 != null)
									{
										spawnPointTypeCollector2.Collect(spawnPoint2.SpawnPointType);
										spawnTableTypeCollector.Collect(spawnPoint2.SpawnTableType);
										spawnTableCollector.Collect(spawnPoint2.SpawnTable);
										spawnPoints.Add(spawnPoint2);
									}
								}
							}
							if (this.ShowSpawnPointProperties(spawnPoints, spawnPointTypeCollector2, spawnTableTypeCollector, spawnTableCollector))
							{
								modalDialogCreated = true;
							}
						}
						else if (selectedType == MapObjectFactory.Type.ScriptArea)
						{
							List<ScriptArea> scriptAreas = new List<ScriptArea>();
							ValueCollector<ScriptAreaType> scriptAreaTypeCollector = new ScriptAreaTypeCollector();
							ValueCollector<string> scriptZoneCollector = new StringCollector();
							foreach (KeyValuePair<IMapObject, IMapObject> keyValuePair10 in this.selector.MapObjects)
							{
								if (keyValuePair10.Key.Type.Type == MapObjectFactory.Type.ScriptArea)
								{
									ScriptArea scriptArea = keyValuePair10.Key as ScriptArea;
									if (scriptArea != null)
									{
										scriptAreaTypeCollector.Collect(scriptArea.ScriptAreaType);
										scriptZoneCollector.Collect(scriptArea.ScriptZone);
										scriptAreas.Add(scriptArea);
									}
								}
							}
							if (this.ShowScriptAreaProperties(scriptAreas, scriptAreaTypeCollector, scriptZoneCollector))
							{
								modalDialogCreated = true;
							}
						}
						else if (selectedType == MapObjectFactory.Type.ZoneLocator)
						{
							List<ZoneLocator> zoneLocators = new List<ZoneLocator>();
							ValueCollector<string> mapZoneCollector = new StringCollector();
							foreach (KeyValuePair<IMapObject, IMapObject> keyValuePair11 in this.selector.MapObjects)
							{
								if (keyValuePair11.Key.Type.Type == MapObjectFactory.Type.ZoneLocator)
								{
									ZoneLocator zoneLocator = keyValuePair11.Key as ZoneLocator;
									if (zoneLocator != null)
									{
										mapZoneCollector.Collect(zoneLocator.MapZone);
										zoneLocators.Add(zoneLocator);
									}
								}
							}
							if (this.ShowZoneLocatorProperties(zoneLocators, mapZoneCollector))
							{
								modalDialogCreated = true;
							}
						}
						else if (selectedType == MapObjectFactory.Type.RoutePoint)
						{
							List<RoutePoint> routePoints = new List<RoutePoint>();
							ValueCollector<string> routeCollector = new StringCollector();
							foreach (KeyValuePair<IMapObject, IMapObject> keyValuePair12 in this.selector.MapObjects)
							{
								if (keyValuePair12.Key.Type.Type == MapObjectFactory.Type.RoutePoint)
								{
									RoutePoint routePoint = keyValuePair12.Key as RoutePoint;
									if (routePoint != null)
									{
										routeCollector.Collect(routePoint.Route);
										routePoints.Add(routePoint);
									}
								}
							}
							if (this.ShowRoutePointProperties(routePoints, routeCollector))
							{
								modalDialogCreated = true;
							}
						}
						else if (selectedType == MapObjectFactory.Type.Projectile)
						{
							List<IMapObject> projectiles = new List<IMapObject>();
							ValueCollector<string> projectileCollector = new StringCollector();
							foreach (KeyValuePair<IMapObject, IMapObject> keyValuePair13 in this.selector.MapObjects)
							{
								if (keyValuePair13.Key.Type.Type == MapObjectFactory.Type.Projectile)
								{
									Projectile projectile = keyValuePair13.Key as Projectile;
									if (projectile != null)
									{
										projectileCollector.Collect(projectile.ProjectileDBID);
										projectiles.Add(projectile);
									}
								}
							}
							if (this.ShowProjectileProperties(projectiles, projectileCollector))
							{
								modalDialogCreated = true;
							}
						}
						else if (selectedType == MapObjectFactory.Type.PlayerRespawnPlace)
						{
							if (properiesType == ObjectsState.PropetiesType.Special)
							{
								List<PlayerRespawnPlace> playerRespawnPlaces = new List<PlayerRespawnPlace>();
								ValueCollector<string> factionCollector = new StringCollector();
								foreach (KeyValuePair<IMapObject, IMapObject> keyValuePair14 in this.selector.MapObjects)
								{
									if (keyValuePair14.Key.Type.Type == MapObjectFactory.Type.PlayerRespawnPlace)
									{
										PlayerRespawnPlace playerRespawnPlace = keyValuePair14.Key as PlayerRespawnPlace;
										if (playerRespawnPlace != null)
										{
											factionCollector.Collect(playerRespawnPlace.Faction);
											playerRespawnPlaces.Add(playerRespawnPlace);
										}
									}
								}
								if (this.ShowPlayerRespawnPlaceFactionProperties(playerRespawnPlaces, factionCollector))
								{
									modalDialogCreated = true;
								}
							}
							else
							{
								List<PlayerRespawnPlace> playerRespawnPlaces2 = new List<PlayerRespawnPlace>();
								ValueCollector<string> deviceCollector2 = new StringCollector();
								foreach (KeyValuePair<IMapObject, IMapObject> keyValuePair15 in this.selector.MapObjects)
								{
									if (keyValuePair15.Key.Type.Type == MapObjectFactory.Type.PlayerRespawnPlace)
									{
										PlayerRespawnPlace playerRespawnPlace2 = keyValuePair15.Key as PlayerRespawnPlace;
										if (playerRespawnPlace2 != null)
										{
											deviceCollector2.Collect(playerRespawnPlace2.Device);
											playerRespawnPlaces2.Add(playerRespawnPlace2);
										}
									}
								}
								if (this.ShowPlayerRespawnPlaceProperties(playerRespawnPlaces2, deviceCollector2))
								{
									modalDialogCreated = true;
								}
							}
						}
						else if (selectedType == MapObjectFactory.Type.MapLocator)
						{
							modalDialogCreated = false;
						}
						else if (selectedType == MapObjectFactory.Type.Sanctuary)
						{
							modalDialogCreated = false;
						}
						else if (selectedType == MapObjectFactory.Type.AstralBorder)
						{
							modalDialogCreated = false;
						}
					}
				}
				if (this.propertyControl != null)
				{
					if (!modalDialogCreated && forceShowDialog)
					{
						this.propertyControl.Visible = true;
					}
					if (this.selector.MapObjects.Count == 1)
					{
						using (Dictionary<IMapObject, IMapObject>.Enumerator enumerator = this.selector.MapObjects.GetEnumerator())
						{
							if (enumerator.MoveNext())
							{
								KeyValuePair<IMapObject, IMapObject> keyValuePair16 = enumerator.Current;
								this.propertyControl.SelectedObject = keyValuePair16.Key;
								this.propertyControl.UpdatePropertyControl(false);
							}
							return;
						}
					}
					if (this.selector.MapObjects.Count == 2)
					{
						IMapObject left = null;
						IMapObject right = null;
						foreach (KeyValuePair<IMapObject, IMapObject> keyValuePair17 in this.selector.MapObjects)
						{
							if (left == null)
							{
								left = keyValuePair17.Key;
							}
							else if (right == null)
							{
								right = keyValuePair17.Key;
							}
						}
						object linkData = this.mapObjectContainer.GetLinkData(left, right);
						if (linkData != null)
						{
							this.propertyControl.SelectedObject = linkData;
						}
						else
						{
							this.propertyControl.SelectedObject = this.selector;
						}
						this.propertyControl.UpdatePropertyControl(false);
						return;
					}
					this.propertyControl.SelectedObject = this.selector;
					this.propertyControl.UpdatePropertyControl(false);
					return;
				}
			}
			else if (this.propertyControl != null && this.propertyControl.SelectedObject != null)
			{
				this.propertyControl.SelectedObject = null;
				this.propertyControl.UpdatePropertyControl(false);
			}
		}

		// Token: 0x06001DA5 RID: 7589 RVA: 0x000BEEEC File Offset: 0x000BDEEC
		private void OnSelectionChanged(MapObjectSelector mapObjectSelector)
		{
			this.ShowProperties(false, false, ObjectsState.PropetiesType.Default);
			this.UpdatePosition(0, 0, false);
		}

		// Token: 0x06001DA6 RID: 7590 RVA: 0x000BEF00 File Offset: 0x000BDF00
		private void OnFocusOn(MethodArgs methodArgs)
		{
			Position anchor = (!this.selector.Empty) ? this.selector.Position : this.currentPosition;
			Vec3 distance = new Vec3(0.0, -10.0, 10.0);
			this.mapEditorScene.EditorScene.SetAnchor(this.mapEditorScene.EditorSceneViewID, ref anchor, ref distance);
		}

		// Token: 0x06001DA7 RID: 7591 RVA: 0x000BEF74 File Offset: 0x000BDF74
		private void OnEditObject(MethodArgs methodArgs)
		{
			if (this.selector.MapObjects.Count == 1)
			{
				foreach (KeyValuePair<IMapObject, IMapObject> keyValuePair in this.selector.MapObjects)
				{
					IMapObject mapObject = keyValuePair.Key;
					if (mapObject != null)
					{
						string fileName = keyValuePair.Key.SceneName;
						base.Container.Invoke("_open_object_in_object_editor", new MethodArgs(null, fileName, null));
						break;
					}
				}
			}
		}

		// Token: 0x06001DA8 RID: 7592 RVA: 0x000BF00C File Offset: 0x000BE00C
		private void OnAddToObjectEditor(MethodArgs methodArgs)
		{
			if (this.selector.MapObjects.Count > 0)
			{
				DBID dbid = this.context.ObjectEditor.StaticObjectDBID;
				if (!DBID.IsNullOrEmpty(dbid))
				{
					string sceneName = dbid.ToString();
					using (Dictionary<int, IMapObject>.ValueCollection.Enumerator enumerator = this.mapEditorScene.MapEditorMapObjectContainer.StaticObjectContainer.MapObjects.Values.GetEnumerator())
					{
						while (enumerator.MoveNext())
						{
							IMapObject mapObject = enumerator.Current;
							if (mapObject.SceneName == sceneName)
							{
								if (mapObject.Scale.Ratio > MathConsts.FLOAT_EPSILON)
								{
									foreach (KeyValuePair<IMapObject, IMapObject> keyValuePair in this.selector.MapObjects)
									{
										IMapObject selectdMapObject = keyValuePair.Key;
										if (selectdMapObject.Type.Type == MapObjectFactory.Type.StaticObject && selectdMapObject.SceneName != mapObject.SceneName)
										{
											float ratio = 1f / mapObject.Scale.Ratio;
											MapObjectInfo info = new MapObjectInfo();
											info.sceneName = selectdMapObject.SceneName;
											info.position = (double)ratio * (selectdMapObject.Position - mapObject.Position);
											Quat quat = new Quat(mapObject.Rotation * -1f);
											info.position = new Position(quat.Rotate(info.position.Vec3));
											info.rotation = selectdMapObject.Rotation - mapObject.Rotation;
											info.scale = ratio * selectdMapObject.Scale;
											base.Container.Invoke("_add_object_to_object_editor", new MethodArgs(null, info, null));
										}
									}
								}
								break;
							}
						}
						return;
					}
				}
				this.OnEditObject(methodArgs);
			}
		}

		// Token: 0x06001DA9 RID: 7593 RVA: 0x000BF244 File Offset: 0x000BE244
		private void OnSelectObject(MethodArgs methodArgs)
		{
			IMapObject mapObject = methodArgs.sender as IMapObject;
			if (mapObject != null && this.mapObjectContainer != null && this.mapObjectContainer.Contains(mapObject))
			{
				this.selector.Clear();
				this.selector.Add(mapObject);
				this.OnFocusOn(default(MethodArgs));
			}
		}

		// Token: 0x06001DAA RID: 7594 RVA: 0x000BF2A0 File Offset: 0x000BE2A0
		private void OnSelectObjectByText(MethodArgs methodArgs)
		{
			string key = methodArgs.sender as string;
			if (!string.IsNullOrEmpty(key) && this.mapObjectContainer != null)
			{
				int beginnerId = -1;
				if (this.selector.MapObjects.Count == 1)
				{
					using (Dictionary<IMapObject, IMapObject>.ValueCollection.Enumerator enumerator = this.selector.MapObjects.Values.GetEnumerator())
					{
						if (enumerator.MoveNext())
						{
							IMapObject mapObject = enumerator.Current;
							beginnerId = mapObject.ID;
						}
					}
				}
				this.selector.Clear();
				List<int> mapObjectIdList = new List<int>(this.mapObjectContainer.MapObjects.Keys);
				int beginnerIndex = 0;
				if (beginnerId != -1)
				{
					beginnerIndex = mapObjectIdList.IndexOf(beginnerId) + 1;
				}
				if (beginnerIndex > 0 && beginnerIndex < mapObjectIdList.Count)
				{
					mapObjectIdList.AddRange(mapObjectIdList.GetRange(0, beginnerIndex));
					mapObjectIdList.RemoveRange(0, beginnerIndex);
				}
				foreach (int mapObjectId in mapObjectIdList)
				{
					IMapObject mapObject2 = this.mapObjectContainer.MapObjects[mapObjectId];
					IMapObjectInterfaceExtention mapObjectExt = mapObject2 as IMapObjectInterfaceExtention;
					if (mapObjectExt != null && mapObjectExt.ContainsText(key, false))
					{
						this.selector.Add(mapObject2);
						this.OnFocusOn(default(MethodArgs));
						break;
					}
				}
			}
		}

		// Token: 0x06001DAB RID: 7595 RVA: 0x000BF418 File Offset: 0x000BE418
		private void OnToggleDoubleClickProperties(MethodArgs methodArgs)
		{
			if (this.objectsStateParams != null)
			{
				this.objectsStateParams.EnableDoubleClickProperties = !this.objectsStateParams.EnableDoubleClickProperties;
				base.SetMethodParams("toggle_double_click_properties", true, true, true, this.objectsStateParams.EnableDoubleClickProperties);
			}
		}

		// Token: 0x06001DAC RID: 7596 RVA: 0x000BF454 File Offset: 0x000BE454
		private void OnAddRowChanged()
		{
			if (this.objectsStateParams != null)
			{
				if (this.objectsStateParams.AddRow)
				{
					if (this.multiState.ActiveStateLabel == "ObjectAddState")
					{
						string item = this.objectsStateParams.ItemList.SelectedItem;
						this.objectsStateParams.ItemList.ClearSelection();
						this.objectsStateParams.ItemList.SelectedItem = item;
						return;
					}
				}
				else if (this.multiState.ActiveStateLabel == "ObjectAddRowState")
				{
					string item2 = this.objectsStateParams.ItemList.SelectedItem;
					this.objectsStateParams.ItemList.ClearSelection();
					this.objectsStateParams.ItemList.SelectedItem = item2;
				}
			}
		}

		// Token: 0x06001DAD RID: 7597 RVA: 0x000BF50C File Offset: 0x000BE50C
		private void OnOneSidedRowChanged()
		{
			if (this.multiState.ActiveStateLabel == "ObjectAddRowState")
			{
				ObjectsState.ObjectAddRowState objectAddRowState = this.multiState.ActiveState as ObjectsState.ObjectAddRowState;
				if (objectAddRowState != null)
				{
					objectAddRowState.ReformatRow();
				}
			}
		}

		// Token: 0x06001DAE RID: 7598 RVA: 0x000BF54A File Offset: 0x000BE54A
		private void OnCopySpecial(MethodArgs methodArgs)
		{
			this.CreateMultiObject();
		}

		// Token: 0x06001DAF RID: 7599 RVA: 0x000BF552 File Offset: 0x000BE552
		private void OnMarkObjectAsUnselectable(MethodArgs methodArgs)
		{
			this.context.UnselectableObjectsForm.AddObject(this.selector.MapObjects.Keys);
			this.selector.Clear();
		}

		// Token: 0x06001DB0 RID: 7600 RVA: 0x000BF580 File Offset: 0x000BE580
		private void OnMarkObjectAsHidden(MethodArgs methodArgs)
		{
			foreach (IMapObject mapObject in this.selector.MapObjects.Keys)
			{
				if (!mapObject.Temporary)
				{
					this.context.HiddenObjectsForm.AddObject(mapObject);
				}
			}
			this.selector.Clear();
		}

		// Token: 0x06001DB1 RID: 7601 RVA: 0x000BF5FC File Offset: 0x000BE5FC
		private void OnLoadQuestList(MethodArgs methodArgs)
		{
			MapEventArgs e = new MapEventArgs(this.mapResourceName);
			base.Container.Invoke("_load_quest_list_by_object", new MethodArgs(null, this.selector.MapObjects.Keys, e));
		}

		// Token: 0x06001DB2 RID: 7602 RVA: 0x000BF63C File Offset: 0x000BE63C
		private void OnAddQuestToMapObject(MethodArgs methodArgs)
		{
			MapEventArgs e = new MapEventArgs(this.mapResourceName);
			base.Container.Invoke("_add_quest_to_object", new MethodArgs(null, this.selector.MapObjects.Keys, e));
		}

		// Token: 0x06001DB3 RID: 7603 RVA: 0x000BF67C File Offset: 0x000BE67C
		private void OnFindQuestsForKilling(MethodArgs methodArgs)
		{
			base.Container.Invoke("_find_quests_for_killing", new MethodArgs(null, this.selector.MapObjects.Keys, null));
		}

		// Token: 0x06001DB4 RID: 7604 RVA: 0x000BF6A8 File Offset: 0x000BE6A8
		private void OnLoadSpawnTunerToScriptEditor(MethodArgs methodArgs)
		{
			if (this.selector.MapObjects.Count > 0)
			{
				foreach (KeyValuePair<IMapObject, IMapObject> keyValuePair in this.selector.MapObjects)
				{
					if (keyValuePair.Key.Type.Type == MapObjectFactory.Type.SpawnPoint)
					{
						SpawnPoint spawnPoint = keyValuePair.Key as SpawnPoint;
						if (spawnPoint != null)
						{
							if (string.IsNullOrEmpty(spawnPoint.SpawnTuner))
							{
								string type = SpawnPoint.GetSpawnTunerType(spawnPoint);
								if (!string.IsNullOrEmpty(type))
								{
									IDatabase mainDb = IDatabase.GetMainDatabase();
									if (mainDb != null)
									{
										CreateSpawnTunerForm createSpawnTunerForm = new CreateSpawnTunerForm(SpawnPoint.GetSpawnTunerFolder(this.continentName), SpawnPoint.GetUniqueSpawnTunerName(this.continentName, type, mainDb), mainDb);
										IObjMan spawnTunerMan;
										if (createSpawnTunerForm.ShowDialog() == DialogResult.OK && SpawnPoint.CreateSpawnTuner(type, createSpawnTunerForm.SpawnTuner, mainDb, out spawnTunerMan))
										{
											spawnPoint.SpawnTuner = createSpawnTunerForm.SpawnTuner;
										}
									}
								}
							}
							string script = spawnPoint.SpawnTuner;
							if (!string.IsNullOrEmpty(script))
							{
								base.Container.Invoke("_load_to_script_editor_by_string", new MethodArgs(null, script, null));
							}
							break;
						}
					}
				}
			}
		}

		// Token: 0x06001DB5 RID: 7605 RVA: 0x000BF7E4 File Offset: 0x000BE7E4
		private void OnLoadPatrolToScriptEditor(MethodArgs methodArgs)
		{
			if (this.selector.MapObjects.Count > 0)
			{
				foreach (KeyValuePair<IMapObject, IMapObject> keyValuePair in this.selector.MapObjects)
				{
					if (keyValuePair.Key.Type.Type == MapObjectFactory.Type.SpawnPoint)
					{
						SpawnPoint spawnPoint = keyValuePair.Key as SpawnPoint;
						if (spawnPoint != null && spawnPoint.SpawnPointType == SpawnPointType.Patrol)
						{
							DBID dbid;
							if (string.IsNullOrEmpty(spawnPoint.GetScript()) && MessageBox.Show(this.parentForm, Strings.PATROL_SCRIPT_CREATE_QUESTION, spawnPoint.SceneName, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes && this.CreatePatrolScript(out dbid))
							{
								spawnPoint.SetScript(dbid.ToString());
							}
							string script = spawnPoint.GetScript();
							if (!string.IsNullOrEmpty(script))
							{
								base.Container.Invoke("_load_to_script_editor_by_string", new MethodArgs(null, script, null));
							}
							break;
						}
					}
				}
			}
		}

		// Token: 0x06001DB6 RID: 7606 RVA: 0x000BF8F4 File Offset: 0x000BE8F4
		private void OnLoadToScriptEditor(MethodArgs methodArgs)
		{
			if (this.selector.MapObjects.Count > 0)
			{
				foreach (KeyValuePair<IMapObject, IMapObject> keyValuePair in this.selector.MapObjects)
				{
					if (keyValuePair.Key.Type.Type == MapObjectFactory.Type.SpawnPoint)
					{
						SpawnPoint spawnPoint = keyValuePair.Key as SpawnPoint;
						if (spawnPoint != null)
						{
							base.Container.Invoke("_load_to_script_editor_by_string", new MethodArgs(null, spawnPoint.SpawnTable, null));
							break;
						}
					}
					else if (keyValuePair.Key.Type.Type == MapObjectFactory.Type.ScriptArea)
					{
						ScriptArea scriptArea = keyValuePair.Key as ScriptArea;
						if (scriptArea != null)
						{
							base.Container.Invoke("_load_to_script_editor_by_string", new MethodArgs(null, scriptArea.ScriptZone, null));
							break;
						}
					}
					else if (keyValuePair.Key.Type.Type == MapObjectFactory.Type.PatrolNode)
					{
						PatrolNode patrolNode = keyValuePair.Key as PatrolNode;
						if (patrolNode != null)
						{
							DBID dbid;
							if (string.IsNullOrEmpty(patrolNode.Script) && MessageBox.Show(this.parentForm, Strings.PATROL_SCRIPT_CREATE_QUESTION, patrolNode.SceneName, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes && this.CreatePatrolScript(out dbid))
							{
								patrolNode.Script = dbid.ToString();
							}
							if (!string.IsNullOrEmpty(patrolNode.Script))
							{
								base.Container.Invoke("_load_to_script_editor_by_string", new MethodArgs(null, patrolNode.Script, null));
							}
							break;
						}
					}
					else if (keyValuePair.Key.Type.Type == MapObjectFactory.Type.Projectile)
					{
						Projectile projectile = keyValuePair.Key as Projectile;
						if (projectile != null && !string.IsNullOrEmpty(projectile.ProjectileDBID))
						{
							base.Container.Invoke("_load_to_script_editor_by_string", new MethodArgs(null, projectile.ProjectileDBID, null));
						}
						break;
					}
				}
			}
		}

		// Token: 0x06001DB7 RID: 7607 RVA: 0x000BFB08 File Offset: 0x000BEB08
		private void OnLoadToModelViewer(MethodArgs methodArgs)
		{
			if (this.selector.MapObjects.Count > 0)
			{
				foreach (KeyValuePair<IMapObject, IMapObject> keyValuePair in this.selector.MapObjects)
				{
					if (keyValuePair.Key.Type.Type == MapObjectFactory.Type.SpawnPoint)
					{
						SpawnPoint spawnPoint = keyValuePair.Key as SpawnPoint;
						if (spawnPoint != null && !string.IsNullOrEmpty(spawnPoint.SceneName))
						{
							base.Container.Invoke("_load_to_model_editor_by_string", new MethodArgs(null, spawnPoint.SceneName, null));
							break;
						}
					}
					else if (keyValuePair.Key.Type.Type == MapObjectFactory.Type.ClientSpawnPoint)
					{
						ClientSpawnPoint clientSpawnPoint = keyValuePair.Key as ClientSpawnPoint;
						if (clientSpawnPoint != null && !string.IsNullOrEmpty(clientSpawnPoint.SceneName))
						{
							base.Container.Invoke("_load_to_model_editor_by_string", new MethodArgs(null, clientSpawnPoint.SceneName, null));
							break;
						}
					}
				}
			}
		}

		// Token: 0x06001DB8 RID: 7608 RVA: 0x000BFC28 File Offset: 0x000BEC28
		private void OnLoadToModelEditor(MethodArgs methodArgs)
		{
			if (this.context == null || this.selector.MapObjects.Count == 0)
			{
				return;
			}
			foreach (KeyValuePair<IMapObject, IMapObject> keyValuePair in this.selector.MapObjects)
			{
				IMapObject mapObject = keyValuePair.Key;
				if (mapObject != null && !string.IsNullOrEmpty(mapObject.SceneName))
				{
					this.context.ShowModelEditor(true);
					MainForm modelEditor = this.context.ModelEditor;
					if (modelEditor != null)
					{
						modelEditor.BringToFront();
						modelEditor.LoadModel(mapObject.SceneName);
					}
					break;
				}
			}
		}

		// Token: 0x06001DB9 RID: 7609 RVA: 0x000BFCDC File Offset: 0x000BECDC
		private void OnLoadDialogEditor(MethodArgs methodArgs)
		{
			if (this.selector.MapObjects.Count > 0)
			{
				foreach (KeyValuePair<IMapObject, IMapObject> keyValuePair in this.selector.MapObjects)
				{
					string dialogEditorObject = null;
					if (keyValuePair.Key.Type.Type == MapObjectFactory.Type.SpawnPoint)
					{
						SpawnPoint spawnPoint = keyValuePair.Key as SpawnPoint;
						if (spawnPoint != null && spawnPoint.SpawnTableType == SpawnTableType.SingleMob)
						{
							dialogEditorObject = spawnPoint.SpawnTable;
						}
					}
					else if (keyValuePair.Key.Type.Type == MapObjectFactory.Type.PermanentDevice)
					{
						PermanentDevice permanentDevice = keyValuePair.Key as PermanentDevice;
						if (permanentDevice != null)
						{
							dialogEditorObject = permanentDevice.Device;
						}
					}
					else if (keyValuePair.Key.Type.Type == MapObjectFactory.Type.PlayerRespawnPlace)
					{
						PlayerRespawnPlace playerRespawnPlace = keyValuePair.Key as PlayerRespawnPlace;
						if (playerRespawnPlace != null)
						{
							dialogEditorObject = playerRespawnPlace.Device;
						}
					}
					if (!string.IsNullOrEmpty(dialogEditorObject))
					{
						if (!EditorSceneDLLImport.ApplicationSingleton_SendMessageToWindow(string.Format("mob_{0}{1}", EditorEnvironment.ApplicationGUID, EditorEnvironment.ExtEditorSuffix), EditorEnvironment.ShowObjectMessgeId, dialogEditorObject))
						{
							string arg = string.Format("-module:Mob {0}", dialogEditorObject);
							DirectoryInfo binDirectory = new DirectoryInfo(EditorEnvironment.WorkingFolder + "bin");
							if (binDirectory.Exists)
							{
								Environment.CurrentDirectory = binDirectory.FullName;
								Process.Start(new ProcessStartInfo(Application.ExecutablePath, arg));
							}
						}
						break;
					}
				}
			}
		}

		// Token: 0x06001DBA RID: 7610 RVA: 0x000BFE78 File Offset: 0x000BEE78
		private void OnItemListSelected(MethodArgs methodArgs)
		{
			this.currentItem = (methodArgs.sender as string);
			this.multiState.ActiveStateLabel = (this.objectsStateParams.AddRow ? "ObjectAddRowState" : "ObjectAddState");
		}

		// Token: 0x06001DBB RID: 7611 RVA: 0x000BFEB0 File Offset: 0x000BEEB0
		private void OnItemListDeselected(MethodArgs methodArgs)
		{
			this.currentItem = string.Empty;
			this.multiState.ActiveStateLabel = "ObjectSelectState";
		}

		// Token: 0x06001DBC RID: 7612 RVA: 0x000BFECD File Offset: 0x000BEECD
		private void OnItemListCleared(MethodArgs methodArgs)
		{
			this.currentItem = string.Empty;
			this.multiState.ActiveStateLabel = "ObjectSelectState";
		}

		// Token: 0x06001DBD RID: 7613 RVA: 0x000BFEEC File Offset: 0x000BEEEC
		private void OnFramePickerStarted(Tools.Geometry.Point start, Tools.Geometry.Point current)
		{
			Position _start;
			this.mapObjectContainer.PickPosition(0, start.X, start.Y, this.framePicker.Surface, out _start);
			Position _finish;
			this.mapObjectContainer.PickPosition(0, current.X, current.Y, this.framePicker.Surface, out _finish);
			this.framePickerUserGeometryID = this.mapEditorScene.EditorScene.CreateUserGeometry_Square(this.framePickerUserGeometryID, ref _start, ref _finish, this.framePickerUserGeometryColor);
		}

		// Token: 0x06001DBE RID: 7614 RVA: 0x000BFF70 File Offset: 0x000BEF70
		private void OnFramePickerContinued(Tools.Geometry.Point start, Tools.Geometry.Point current)
		{
			Position _start;
			this.mapObjectContainer.PickPosition(0, start.X, start.Y, this.framePicker.Surface, out _start);
			Position _finish;
			this.mapObjectContainer.PickPosition(0, current.X, current.Y, this.framePicker.Surface, out _finish);
			this.framePickerUserGeometryID = this.mapEditorScene.EditorScene.CreateUserGeometry_Square(this.framePickerUserGeometryID, ref _start, ref _finish, this.framePickerUserGeometryColor);
		}

		// Token: 0x06001DBF RID: 7615 RVA: 0x000BFFF3 File Offset: 0x000BEFF3
		private void OnFramePickerFinished(Tools.Geometry.Point start, Tools.Geometry.Point current)
		{
			if (this.framePickerUserGeometryID != -1)
			{
				this.mapEditorScene.EditorScene.DeleteUserGeometry(this.framePickerUserGeometryID);
				this.framePickerUserGeometryID = -1;
			}
		}

		// Token: 0x06001DC0 RID: 7616 RVA: 0x000C001C File Offset: 0x000BF01C
		private void OnScreenFramePickerStarted(Tools.Geometry.Point start, Tools.Geometry.Point current)
		{
			Rect rect = new Rect(start, current);
			this.mapEditorScene.EditorScene.SetScreenFrame(this.mapEditorScene.EditorSceneViewID, rect.Min.X, rect.Min.Y, rect.Max.X, rect.Max.Y, this.screenFramePickerUserGeometryColor);
			this.mapEditorScene.EditorScene.ShowScreenFrame(this.mapEditorScene.EditorSceneViewID, true);
		}

		// Token: 0x06001DC1 RID: 7617 RVA: 0x000C00AC File Offset: 0x000BF0AC
		private void OnScreenFramePickerContinued(Tools.Geometry.Point start, Tools.Geometry.Point current)
		{
			Rect rect = new Rect(start, current);
			this.mapEditorScene.EditorScene.SetScreenFrame(this.mapEditorScene.EditorSceneViewID, rect.Min.X, rect.Min.Y, rect.Max.X, rect.Max.Y, this.screenFramePickerUserGeometryColor);
		}

		// Token: 0x06001DC2 RID: 7618 RVA: 0x000C0120 File Offset: 0x000BF120
		private void OnScreenFramePickerFinished(Tools.Geometry.Point start, Tools.Geometry.Point current)
		{
			this.mapEditorScene.EditorScene.ShowScreenFrame(this.mapEditorScene.EditorSceneViewID, false);
		}

		// Token: 0x06001DC3 RID: 7619 RVA: 0x000C0140 File Offset: 0x000BF140
		private void OnEnterState(IState state)
		{
			if (base.Container != null)
			{
				base.Container.BindState(this.multiState);
				base.Container.Invoke("_object_tab_send_data", default(MethodArgs));
				base.Container.Invoke("_enable_group_buttons", default(MethodArgs));
				base.Container.Invoke("_clear_selection_in_object_item_list", default(MethodArgs));
				if (this.selector != null)
				{
					this.selector.Bind();
				}
				if (this.objectsStateParams != null)
				{
					base.SetMethodParams("toggle_double_click_properties", true, true, true, this.objectsStateParams.EnableDoubleClickProperties);
				}
				this.multiState.ActiveStateLabel = "ObjectSelectState";
			}
			if (this.mapEditorScene != null)
			{
				if (!this.mapEditorScene.MapSceneParams.ShowAllObjects)
				{
					this.restoreHideAllObjects = true;
					this.mapEditorScene.MapSceneParams.ShowAllObjects = true;
					return;
				}
				this.restoreHideAllObjects = false;
			}
		}

		// Token: 0x06001DC4 RID: 7620 RVA: 0x000C0234 File Offset: 0x000BF234
		private void OnLeaveState(IState state)
		{
			this.ClearPickers();
			this.selector.Clear();
			this.clipboard.Hide();
			if (base.Container != null)
			{
				base.Container.Invoke("_disable_group_buttons", default(MethodArgs));
				base.Container.UnbindState(this.multiState);
				if (this.selector != null)
				{
					this.selector.Unbind();
				}
			}
			if (this.statusbar.StatusHelp != null)
			{
				this.statusbar.StatusHelp.Text = string.Empty;
			}
			if (this.mapEditorScene != null && this.restoreHideAllObjects)
			{
				this.mapEditorScene.MapSceneParams.ShowAllObjects = false;
			}
		}

		// Token: 0x06001DC5 RID: 7621 RVA: 0x000C02E8 File Offset: 0x000BF2E8
		private void CreateSpawnTable()
		{
			IDatabase mainDb = IDatabase.GetMainDatabase();
			if (mainDb != null && !this.selector.Empty)
			{
				List<SpawnPoint> spawnPoints = new List<SpawnPoint>();
				Dictionary<string, int> spawnTables = new Dictionary<string, int>();
				SpawnPoint.LocateSpawnPoints(this.selector.MapObjects, spawnPoints, null, spawnTables);
				if (spawnPoints.Count == 0)
				{
					MessageBox.Show(this.parentForm, Strings.CREATE_SPAWN_TABLE_NOTHING_TO_MERGE, Strings.CREATE_SPAWN_TABLE_CAPTION, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
					return;
				}
				string newSpawnTable = SpawnPoint.LocateSpawnTable(spawnTables, mainDb);
				bool useExistingSpawnTable = !string.IsNullOrEmpty(newSpawnTable);
				string spawnTableFolder = Constants.ContinentFolder(this.continentName) + SpawnPoint.SpawnTableFolder;
				if (!useExistingSpawnTable)
				{
					newSpawnTable = SafeObjMan.CreateUniqueDBID(spawnTableFolder + SpawnPoint.PackSpawnTableName, false, mainDb);
				}
				Dictionary<IMapObject, string> spawnIDs = new Dictionary<IMapObject, string>();
				foreach (SpawnPoint spawnPoint in spawnPoints)
				{
					string spawnID = Str.CutFilePathAndExtention(spawnPoint.SpawnTable, false);
					spawnIDs[spawnPoint] = spawnID;
				}
				CreateSpawnTableForm createSpawnTableForm = new CreateSpawnTableForm(newSpawnTable, useExistingSpawnTable, spawnIDs, spawnTableFolder);
				if (createSpawnTableForm.ShowDialog() == DialogResult.OK)
				{
					IObjMan newSpawnTableMan = null;
					if (!useExistingSpawnTable)
					{
						newSpawnTable = createSpawnTableForm.NewSpawnTable;
						DBID newSpawnTableDBID = mainDb.GetDBIDByName(newSpawnTable);
						if (!DBID.IsNullOrEmpty(newSpawnTableDBID))
						{
							newSpawnTableMan = mainDb.GetManipulator(newSpawnTableDBID);
						}
						if (newSpawnTableMan == null)
						{
							newSpawnTableDBID = IDatabase.CreateDBIDByName(newSpawnTable);
							newSpawnTableMan = mainDb.CreateNewObject(SpawnPoint.SpawnTableDBType);
							if (newSpawnTableMan != null)
							{
								mainDb.AddNewObject(newSpawnTableDBID, newSpawnTableMan);
							}
						}
						if (newSpawnTableMan != null)
						{
							newSpawnTableMan.SetStructPtrInstance("generalSpawnTime", "gameMechanics.elements.spawn.TimeTrash");
							int count = SafeObjMan.GetInt(newSpawnTableMan, "controllers");
							if (count > 0)
							{
								newSpawnTableMan.Remove("controllers", 0, count);
							}
							newSpawnTableMan.Insert("controllers", 0);
							newSpawnTableMan.Insert("controllers", 1);
							newSpawnTableMan.SetStructPtrInstance("controllers.[0]", "gameMechanics.elements.spawn.AggroShare");
							newSpawnTableMan.SetStructPtrInstance("controllers.[1]", "gameMechanics.elements.spawn.GroupRestoreOutOfCombat");
							count = SafeObjMan.GetInt(newSpawnTableMan, "singles");
							if (count > 0)
							{
								newSpawnTableMan.Remove("singles", 0, count);
							}
						}
					}
					else
					{
						DBID newSpawnTableDBID2 = mainDb.GetDBIDByName(newSpawnTable);
						if (!DBID.IsNullOrEmpty(newSpawnTableDBID2))
						{
							newSpawnTableMan = mainDb.GetManipulator(newSpawnTableDBID2);
						}
					}
					if (newSpawnTableMan != null)
					{
						int index = SafeObjMan.GetInt(newSpawnTableMan, "singles");
						foreach (SpawnPoint spawnPoint2 in spawnPoints)
						{
							string spawnID2;
							if (spawnIDs.TryGetValue(spawnPoint2, out spawnID2))
							{
								newSpawnTableMan.Insert("singles", index);
								string prefix = string.Format("singles.[{0}].", index);
								SafeObjMan.SetDBID(newSpawnTableMan, prefix + "object", spawnPoint2.SpawnTable);
								SafeObjMan.SetStringOnlyModified(newSpawnTableMan, prefix + "spawnId", spawnID2);
								index++;
							}
						}
						this.Begin();
						foreach (SpawnPoint spawnPoint3 in spawnPoints)
						{
							string spawnID3;
							if (spawnIDs.TryGetValue(spawnPoint3, out spawnID3))
							{
								spawnPoint3.SpawnTable = newSpawnTable;
								spawnPoint3.SpawnID = spawnID3;
							}
						}
						this.End(true);
					}
				}
			}
		}

		// Token: 0x06001DC6 RID: 7622 RVA: 0x000C0630 File Offset: 0x000BF630
		private void CloneSpawnTable()
		{
			IDatabase mainDb = IDatabase.GetMainDatabase();
			if (mainDb != null && !this.selector.Empty)
			{
				Dictionary<string, int> spawnTables = new Dictionary<string, int>();
				List<SpawnPoint> spawnPoints = new List<SpawnPoint>();
				SpawnPoint.LocateSpawnPoints(this.selector.MapObjects, null, spawnPoints, spawnTables);
				if (spawnTables.Count == 0)
				{
					MessageBox.Show(this.parentForm, Strings.CLONE_SPAWN_TABLES_NOTHING_TO_CLONE, Strings.CLONE_SPAWN_TABLES_CAPTION, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
					return;
				}
				Dictionary<string, string> clonedSpawnTables = new Dictionary<string, string>();
				foreach (KeyValuePair<string, int> keyValuePair in spawnTables)
				{
					string newSpawnTable = SafeObjMan.CreateUniqueDBID(keyValuePair.Key, false, mainDb);
					clonedSpawnTables[keyValuePair.Key] = newSpawnTable;
				}
				string spawnTableFolder = Constants.ContinentFolder(this.continentName) + SpawnPoint.SpawnTableFolder;
				CloneSpawnTableForm cloneSpawnTableForm = new CloneSpawnTableForm(spawnTables, clonedSpawnTables, spawnTableFolder);
				if (cloneSpawnTableForm.ShowDialog() == DialogResult.OK)
				{
					foreach (KeyValuePair<string, int> keyValuePair2 in spawnTables)
					{
						string newSpawnTable2;
						if (clonedSpawnTables.TryGetValue(keyValuePair2.Key, out newSpawnTable2) && !string.IsNullOrEmpty(newSpawnTable2) && newSpawnTable2.Length > spawnTableFolder.Length)
						{
							IObjMan destinationMan = null;
							DBID dbidSource = mainDb.GetDBIDByName(keyValuePair2.Key);
							DBID dbidDestination = mainDb.GetDBIDByName(newSpawnTable2);
							if (!DBID.IsNullOrEmpty(dbidDestination))
							{
								destinationMan = mainDb.GetManipulator(dbidDestination);
							}
							if (destinationMan == null)
							{
								dbidDestination = IDatabase.CreateDBIDByName(newSpawnTable2);
								destinationMan = mainDb.CreateNewObject(SpawnPoint.SpawnTableDBType);
								if (destinationMan != null)
								{
									mainDb.AddNewObject(dbidDestination, destinationMan);
								}
							}
							if (destinationMan != null)
							{
								mainDb.CopyObject(dbidDestination, dbidSource);
							}
							else
							{
								clonedSpawnTables[keyValuePair2.Key] = string.Empty;
							}
						}
					}
					this.Begin();
					foreach (SpawnPoint spawnPoint in spawnPoints)
					{
						string newSpawnTable3;
						if (clonedSpawnTables.TryGetValue(spawnPoint.SpawnTable, out newSpawnTable3) && !string.IsNullOrEmpty(newSpawnTable3) && newSpawnTable3.Length > spawnTableFolder.Length)
						{
							spawnPoint.SpawnTable = newSpawnTable3;
						}
					}
					this.End(true);
				}
			}
		}

		// Token: 0x06001DC7 RID: 7623 RVA: 0x000C088C File Offset: 0x000BF88C
		private void AddFixedIdleAnimationTuner()
		{
			IDatabase mainDb = IDatabase.GetMainDatabase();
			if (mainDb != null && !this.selector.Empty)
			{
				List<SpawnPoint> spawnPoints = new List<SpawnPoint>();
				SpawnPoint.LocateSpawnPoints(this.selector.MapObjects, spawnPoints, spawnPoints, null);
				if (spawnPoints.Count == 1)
				{
					SpawnPoint spawnPoint = spawnPoints[0];
					if ((spawnPoint.SpawnTableType == SpawnTableType.AstralMob || spawnPoint.SpawnTableType == SpawnTableType.SingleMob || spawnPoint.SpawnTableType == SpawnTableType.Table) && !string.IsNullOrEmpty(spawnPoint.SceneName) && !string.Equals(spawnPoint.SceneName, SpawnPoint.DefaultVisObject, StringComparison.OrdinalIgnoreCase))
					{
						AddFixedIdleAnimationTunerForm addFixedIdleAnimationTunerForm = new AddFixedIdleAnimationTunerForm(spawnPoint, this.continentName);
						if (addFixedIdleAnimationTunerForm.ShowDialog() == DialogResult.OK)
						{
							this.mapObjectContainer.InvokeModified();
						}
						spawnPoint.CheckVisualData();
						return;
					}
					MessageBox.Show(this.parentForm, Strings.CREATE_FIXED_IDLE_ANIMATION_NO_SELECTION, Strings.CREATE_FIXED_IDLE_ANIMATION_CAPTION, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
					return;
				}
				else
				{
					if (spawnPoints.Count == 0)
					{
						MessageBox.Show(this.parentForm, Strings.CREATE_FIXED_IDLE_ANIMATION_NO_SELECTION, Strings.CREATE_FIXED_IDLE_ANIMATION_CAPTION, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
						return;
					}
					MessageBox.Show(this.parentForm, Strings.CREATE_FIXED_IDLE_ANIMATION_MULTIPLE_SELECTION, Strings.CREATE_FIXED_IDLE_ANIMATION_CAPTION, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				}
			}
		}

		// Token: 0x06001DC8 RID: 7624 RVA: 0x000C099C File Offset: 0x000BF99C
		private bool CreatePatrolScript(out DBID dbid)
		{
			IDatabase mainDb = IDatabase.GetMainDatabase();
			if (mainDb != null)
			{
				string folder = string.Concat(new object[]
				{
					EditorEnvironment.DataFolder,
					this.continentName,
					'/',
					PatrolNode.ScriptFolder
				});
				if (!Directory.Exists(folder))
				{
					Directory.CreateDirectory(folder);
				}
				int index = 0;
				for (;;)
				{
					string name = string.Format("{0}PatrolNode_{1}.(ScriptResource).xdb", folder, index);
					dbid = IDatabase.CreateDBIDByName(name);
					if (DBID.IsNullOrEmpty(dbid))
					{
						goto IL_A1;
					}
					if (!mainDb.DoesObjectExist(dbid))
					{
						break;
					}
					index++;
				}
				IObjMan objMan = mainDb.CreateNewObject("gameMechanics.map.spawn.patrol.ScriptResource");
				mainDb.AddNewObject(dbid, objMan);
				return true;
			}
			IL_A1:
			dbid = null;
			return false;
		}

		// Token: 0x06001DC9 RID: 7625 RVA: 0x000C0A50 File Offset: 0x000BFA50
		private void CreateMultiObject()
		{
			if (this.multiObjectBrowserForm != null && base.Container != null && this.selector != null && !this.selector.Empty)
			{
				List<IMapObject> mapObjects = new List<IMapObject>();
				Dictionary<int, Dictionary<int, ILinkData>> links = new Dictionary<int, Dictionary<int, ILinkData>>();
				if (this.selector.Pack(mapObjects, links))
				{
					MultiObjectSaver multiObjectSaver = new MultiObjectSaver();
					multiObjectSaver.Pack(mapObjects, links, true, true);
					this.multiObjectBrowserForm.MultiObjectSaver = multiObjectSaver;
					if (!this.multiObjectBrowserForm.Visible)
					{
						base.Container.Invoke("toggle_multiobject_browser", default(MethodArgs));
					}
				}
			}
		}

		// Token: 0x06001DCA RID: 7626 RVA: 0x000C0ADF File Offset: 0x000BFADF
		private void ClearPickers()
		{
			if (this.pointPicker != null)
			{
				this.pointPicker.Clear();
			}
			if (this.framePicker != null)
			{
				this.framePicker.Clear();
			}
			if (this.screenFramePicker != null)
			{
				this.screenFramePicker.Clear();
			}
		}

		// Token: 0x06001DCB RID: 7627 RVA: 0x000C0B1C File Offset: 0x000BFB1C
		private void RemoveSelectedItems()
		{
			List<IMapObject> mapObjectsToRemove = new List<IMapObject>(this.selector.MapObjects.Keys);
			this.selector.Clear();
			foreach (IMapObject mapObject in mapObjectsToRemove)
			{
				this.MapObjectContainer.RemoveMapObject(mapObject);
			}
		}

		// Token: 0x06001DCC RID: 7628 RVA: 0x000C0B94 File Offset: 0x000BFB94
		private void CreateStates()
		{
			this.multiState = new MultiState("ObjectMultiState");
			this.states = new List<IState>();
			this.states.Add(new ObjectsState.ObjectSelectState(this));
			this.states.Add(new ObjectsState.ObjectEditState(this));
			this.states.Add(new ObjectsState.ObjectAddState(this));
			this.states.Add(new ObjectsState.ObjectAddRowState(this));
			this.states.Add(new ObjectsState.ObjectPasteState(this));
			this.multiState.AddStates(this.states);
			this.multiState.ActiveStateLabel = "ObjectSelectState";
		}

		// Token: 0x06001DCD RID: 7629 RVA: 0x000C0C32 File Offset: 0x000BFC32
		private void DestroyStates()
		{
			this.multiState.RemoveStates(this.states);
			this.states.Clear();
			this.states = null;
			this.multiState = null;
		}

		// Token: 0x06001DCE RID: 7630 RVA: 0x000C0C5E File Offset: 0x000BFC5E
		private void Begin()
		{
			this.operationContainer.EndTransaction();
			this.operationContainer.BeginTransaction();
			this.allowOperations = true;
		}

		// Token: 0x06001DCF RID: 7631 RVA: 0x000C0C7F File Offset: 0x000BFC7F
		private void Update()
		{
			if (this.allowOperations)
			{
				this.operationContainer.UpdateTransaction();
			}
		}

		// Token: 0x06001DD0 RID: 7632 RVA: 0x000C0C95 File Offset: 0x000BFC95
		private void End(bool applyChanges)
		{
			if (this.allowOperations)
			{
				if (applyChanges)
				{
					this.operationContainer.EndTransaction();
				}
				else
				{
					this.operationContainer.CancelTransaction();
				}
				this.mapEditorScene.EditorScene.SynchronizeObjects(false);
				this.allowOperations = false;
			}
		}

		// Token: 0x06001DD1 RID: 7633 RVA: 0x000C0CD4 File Offset: 0x000BFCD4
		private void UpdatePosition(int x, int y, bool valid)
		{
			if (valid)
			{
				if (this.selector.Surface != TerrainSurface.None)
				{
					this.MapObjectContainer.PickPosition(0, x, y, this.selector.Surface, out this.currentPosition);
					this.selector.LastZPlane = this.currentPosition.Z;
				}
				else
				{
					Plane plane = new Plane(Vec3.ZNormal, new Vec3(0.0, 0.0, this.selector.LastZPlane));
					Line line;
					this.MapObjectContainer.GetProjectiveRay(x, y, out line);
					Vec3 intersection = plane.GetIntersection(line);
					this.currentPosition.X = intersection.X;
					this.currentPosition.Y = intersection.Y;
					this.currentPosition.Z = intersection.Z;
				}
			}
			if (this.statusbar != null)
			{
				if (valid && this.statusbar.StatusPosition != null)
				{
					if (this.continentType == ContinentType.Continent)
					{
						this.statusbar.StatusPosition.Text = string.Format("x:{0:0.###} y:{1:0.###} z:{2:0.###}, patch:[{3}, {4}]", new object[]
						{
							this.currentPosition.X,
							this.currentPosition.Y,
							this.currentPosition.Z,
							(int)(this.currentPosition.X / (double)Constants.PatchSize),
							(int)(this.currentPosition.Y / (double)Constants.PatchSize)
						});
					}
					else
					{
						this.statusbar.StatusPosition.Text = string.Format("x:{0:0.###}  y:{1:0.###}  z:{2:0.###}", this.currentPosition.X, this.currentPosition.Y, this.currentPosition.Z);
					}
				}
				if (this.statusbar.StatusMessage != null)
				{
					string name = string.Empty;
					Position position = Position.Empty;
					Rotation rotation = Rotation.Empty;
					Scale scale = Scale.Normal;
					bool paramsFilled = false;
					if (this.selector.MapObjects.Count == 1)
					{
						using (Dictionary<IMapObject, IMapObject>.Enumerator enumerator = this.selector.MapObjects.GetEnumerator())
						{
							if (enumerator.MoveNext())
							{
								KeyValuePair<IMapObject, IMapObject> keyValuePair = enumerator.Current;
								name = MapObjectInterface.GetInterfaceSingleObjectName(keyValuePair.Key);
								position = keyValuePair.Key.Position;
								rotation = keyValuePair.Key.Rotation;
								scale = keyValuePair.Key.Scale;
								paramsFilled = true;
							}
							goto IL_38D;
						}
					}
					if (this.selector.MapObjects.Count > 1)
					{
						name = string.Format("{0} objects selected", this.selector.MapObjects.Count);
						position = this.selector.Position;
						rotation = this.selector.Rotation;
						scale = this.selector.Scale;
						paramsFilled = true;
					}
					else if (valid)
					{
						Position positionOnObject;
						int mapObjectID = this.MapObjectContainer.PickFirstObjectByPoint(x, y, out positionOnObject);
						IMapObject mapObject;
						this.MapObjectContainer.TryGetMapObject(mapObjectID, out mapObject);
						if (mapObject != null)
						{
							name = (string.IsNullOrEmpty(mapObject.Type.Stats) ? mapObject.SceneName : mapObject.Type.Stats);
							position = mapObject.Position;
							rotation = mapObject.Rotation;
							scale = mapObject.Scale;
							paramsFilled = true;
						}
						else
						{
							name = string.Format("{0} objects", this.MapObjectContainer.MapObjects.Count);
						}
					}
					IL_38D:
					if (paramsFilled)
					{
						this.statusbar.StatusMessage.Text = string.Format("{0}   x:{1:0.###}  y:{2:0.###}  z:{3:0.###}   yaw:{4:0.##}  pitch:{5:0.##}  roll:{6:0.##}   scale:{7:0.##}", new object[]
						{
							name,
							position.X,
							position.Y,
							position.Z,
							rotation.Yaw,
							rotation.Pitch,
							rotation.Roll,
							scale.Ratio
						});
					}
					else
					{
						this.statusbar.StatusMessage.Text = name;
					}
				}
				if (this.statusbar.StatusHelp != null && base.Container != null)
				{
					if (this.selector.Empty)
					{
						this.statusbar.StatusHelp.Text = string.Empty;
					}
					else
					{
						this.statusbar.StatusHelp.Text = string.Format(Strings.HELP_MAP_OBJECT_SHORTCUTS, new object[]
						{
							base.Container.GetEventShortcuts("properties"),
							base.Container.GetEventShortcuts("quick_properties"),
							base.Container.GetEventShortcuts("special_properties"),
							base.Container.GetEventShortcuts("open_object_in_object_editor"),
							base.Container.GetEventShortcuts("load_quest_list_by_object"),
							base.Container.GetEventShortcuts("add_quest_to_object"),
							base.Container.GetEventShortcuts("load_script_to_scipt_editor")
						});
					}
				}
				this.statusbar.UpdateStatusbar();
			}
		}

		// Token: 0x06001DD2 RID: 7634 RVA: 0x000C1230 File Offset: 0x000C0230
		private void UpdatePosition(bool valid)
		{
			Tools.Geometry.Point mouseCursorPos;
			this.mapEditorScene.EditorScene.GetMouseCursorPos(this.mapEditorScene.EditorSceneViewID, out mouseCursorPos);
			this.UpdatePosition(mouseCursorPos.X, mouseCursorPos.Y, valid);
		}

		// Token: 0x06001DD3 RID: 7635 RVA: 0x000C1270 File Offset: 0x000C0270
		public ObjectsState(MainForm.Context _context, StateContainer _stateContainer, MapObjectContainer _mapObjectContainer, OperationContainer _operationContainer, GroupContainer _groupContainer, IObjectsStateParams _objectsStateParams, IMapObjectFilter _mapObjectFilter, ItemDataContainer _itemDataContainer, string _mapResourceName, string _continentName, ContinentType _continentType, MapEditorScene _mapEditorScene, IPropertyControl _propertyControl, IStatusbar _statusbar, MultiObjectBrowserForm _multiObjectBrowserForm, Form _parentForm, string selectorDataFileName) : base("MapEditorobjectsState")
		{
			this.context = _context;
			this.mapObjectContainer = _mapObjectContainer;
			if (this.mapObjectContainer != null)
			{
				this.operationContainer = _operationContainer;
				if (this.operationContainer != null)
				{
					this.operationContainer.OperationUndoInvoked += this.OnOperationUndo;
					this.operationContainer.OperationRedoInvoked += this.OnOperationRedo;
				}
				this.groupContainer = _groupContainer;
				this.objectsStateParams = _objectsStateParams;
				if (this.objectsStateParams != null)
				{
					this.objectsStateParams.AddRowChanged += this.OnAddRowChanged;
					this.objectsStateParams.OneSidedRowChanged += this.OnOneSidedRowChanged;
				}
				this.itemDataContainer = _itemDataContainer;
				this.mapResourceName = _mapResourceName;
				this.continentName = _continentName;
				this.continentType = _continentType;
				this.mapEditorScene = _mapEditorScene;
				this.propertyControl = _propertyControl;
				this.statusbar = _statusbar;
				this.multiObjectBrowserForm = _multiObjectBrowserForm;
				this.parentForm = _parentForm;
				this.EnterState = (State.ActivateEvent)Delegate.Combine(this.EnterState, new State.ActivateEvent(this.OnEnterState));
				this.LeaveState = (State.ActivateEvent)Delegate.Combine(this.LeaveState, new State.ActivateEvent(this.OnLeaveState));
				base.AddMethod("focus_on", new Method(this.OnFocusOn));
				base.AddMethod("properties", new Method(this.OnProperties));
				base.AddMethod("quick_properties", new Method(this.OnQuickProperties));
				base.AddMethod("special_properties", new Method(this.OnSpecialProperties));
				base.AddMethod("stats_properties", new Method(this.OnStatsProperties));
				base.AddMethod("stats_special_properties", new Method(this.OnStatsSpecialProperties));
				base.AddMethod("toggle_spawn_tuner", new Method(this.OnSpawnTuner));
				base.AddMethod("_object_item_list_selected", new Method(this.OnItemListSelected));
				base.AddMethod("_object_item_list_unselected", new Method(this.OnItemListDeselected));
				base.AddMethod("_object_item_list_cleared", new Method(this.OnItemListCleared));
				base.AddMethod("toggle_double_click_properties", new Method(this.OnToggleDoubleClickProperties));
				base.AddMethod("load_quest_list_by_object", new Method(this.OnLoadQuestList));
				base.AddMethod("add_quest_to_object", new Method(this.OnAddQuestToMapObject));
				base.AddMethod("load_quest_for_killing", new Method(this.OnFindQuestsForKilling));
				base.AddMethod("load_script_to_scipt_editor", new Method(this.OnLoadToScriptEditor));
				base.AddMethod("load_patrol_script_to_scipt_editor", new Method(this.OnLoadPatrolToScriptEditor));
				base.AddMethod("load_spawn_tuner_to_script_editor", new Method(this.OnLoadSpawnTunerToScriptEditor));
				base.AddMethod("load_model_to_model_viewer", new Method(this.OnLoadToModelViewer));
				base.AddMethod("load_model_to_model_editor", new Method(this.OnLoadToModelEditor));
				base.AddMethod("load_dialog_editor", new Method(this.OnLoadDialogEditor));
				base.AddMethod("group_objects", new Method(this.OnGroupSelectedObjects));
				base.AddMethod("ungroup_objects", new Method(this.OnUngroupSelectedObjects));
				base.AddMethod("flatten_objects", new Method(this.OnFlattenSelectedObjects));
				base.AddMethod("copy_special", new Method(this.OnCopySpecial));
				base.AddMethod("group_selection_type_free", new Method(this.OnGroupSelectionTypeFree));
				base.AddMethod("group_selection_type_one_level", new Method(this.OnGroupSelectionTypeOneLevel));
				base.AddMethod("group_selection_type_all_levels", new Method(this.OnGroupSelectionTypeAllLevels));
				base.AddMethod("open_object_in_object_editor", new Method(this.OnEditObject));
				base.AddMethod("add_to_object_editor", new Method(this.OnAddToObjectEditor));
				base.AddMethod("mark_as_unselectable", new Method(this.OnMarkObjectAsUnselectable));
				base.AddMethod("_select_object", new Method(this.OnSelectObject));
				base.AddMethod("_select_object_by_string_key", new Method(this.OnSelectObjectByText));
				base.AddMethod("mark_as_hidden", new Method(this.OnMarkObjectAsHidden));
				this.pointPicker = new PointPicker(this.MapObjectContainer, _mapObjectFilter);
				this.framePicker = new FramePicker(this.MapObjectContainer, _mapObjectFilter);
				this.framePicker.Started += this.OnFramePickerStarted;
				this.framePicker.Continued += this.OnFramePickerContinued;
				this.framePicker.Finished += this.OnFramePickerFinished;
				this.screenFramePicker = new ScreenFramePicker(this.MapObjectContainer, _mapObjectFilter);
				this.screenFramePicker.Started += this.OnScreenFramePickerStarted;
				this.screenFramePicker.Continued += this.OnScreenFramePickerContinued;
				this.screenFramePicker.Finished += this.OnScreenFramePickerFinished;
				this.selector = new MapObjectSelector(this.mapObjectContainer, _stateContainer, this.operationContainer, this.groupContainer, _mapObjectFilter, _mapEditorScene.EditorScene, selectorDataFileName);
				this.selector.SelectionChanged += this.OnSelectionChanged;
				this.clipboard = new MapObjectClipboard(this.mapObjectContainer, this.continentType, this.selector);
				this.CreateStates();
			}
		}

		// Token: 0x06001DD4 RID: 7636 RVA: 0x000C182C File Offset: 0x000C082C
		public void Destroy()
		{
			if (this.mapObjectContainer != null)
			{
				this.DestroyStates();
				this.selector.Destroy();
				this.selector.SelectionChanged -= this.OnSelectionChanged;
				this.clipboard.Destroy();
				this.EnterState = (State.ActivateEvent)Delegate.Remove(this.EnterState, new State.ActivateEvent(this.OnEnterState));
				this.LeaveState = (State.ActivateEvent)Delegate.Remove(this.LeaveState, new State.ActivateEvent(this.OnLeaveState));
				this.framePicker.Started -= this.OnFramePickerStarted;
				this.framePicker.Continued -= this.OnFramePickerContinued;
				this.framePicker.Finished -= this.OnFramePickerFinished;
				this.screenFramePicker.Started -= this.OnScreenFramePickerStarted;
				this.screenFramePicker.Continued -= this.OnScreenFramePickerContinued;
				this.screenFramePicker.Finished -= this.OnScreenFramePickerFinished;
				this.pointPicker = null;
				this.framePicker = null;
				this.screenFramePicker = null;
				this.mapObjectContainer = null;
				this.selector = null;
				this.clipboard = null;
				if (this.objectsStateParams != null)
				{
					this.objectsStateParams.AddRowChanged -= this.OnAddRowChanged;
					this.objectsStateParams.OneSidedRowChanged -= this.OnOneSidedRowChanged;
					this.objectsStateParams = null;
				}
				this.itemDataContainer = null;
				if (this.operationContainer != null)
				{
					this.operationContainer.OperationUndoInvoked -= this.OnOperationUndo;
					this.operationContainer.OperationRedoInvoked -= this.OnOperationRedo;
					this.operationContainer = null;
				}
				this.groupContainer = null;
				this.mapEditorScene = null;
				this.propertyControl = null;
				this.statusbar = null;
				this.multiObjectBrowserForm = null;
				this.parentForm = null;
				this.continentName = string.Empty;
				this.mapResourceName = string.Empty;
				this.continentType = ContinentType.Unknown;
				this.context = null;
			}
		}

		// Token: 0x170006F4 RID: 1780
		// (get) Token: 0x06001DD5 RID: 7637 RVA: 0x000C1A3E File Offset: 0x000C0A3E
		public MapObjectContainer MapObjectContainer
		{
			get
			{
				return this.mapObjectContainer;
			}
		}

		// Token: 0x170006F5 RID: 1781
		// (get) Token: 0x06001DD6 RID: 7638 RVA: 0x000C1A46 File Offset: 0x000C0A46
		public MapObjectSelector MapObjectSelector
		{
			get
			{
				return this.selector;
			}
		}

		// Token: 0x170006F6 RID: 1782
		// (get) Token: 0x06001DD7 RID: 7639 RVA: 0x000C1A4E File Offset: 0x000C0A4E
		public MapObjectClipboard MapObjectClipboard
		{
			get
			{
				return this.clipboard;
			}
		}

		// Token: 0x040012C1 RID: 4801
		private const string objectAddStateLabel = "ObjectAddState";

		// Token: 0x040012C2 RID: 4802
		private const string objectAddRowStateLabel = "ObjectAddRowState";

		// Token: 0x040012C3 RID: 4803
		private const string objectEditStateLabel = "ObjectEditState";

		// Token: 0x040012C4 RID: 4804
		private const string objectPasteStateLabel = "ObjectPasteState";

		// Token: 0x040012C5 RID: 4805
		private const string objectSelectStateLabel = "ObjectSelectState";

		// Token: 0x040012C6 RID: 4806
		private MainForm.Context context;

		// Token: 0x040012C7 RID: 4807
		private MapObjectContainer mapObjectContainer;

		// Token: 0x040012C8 RID: 4808
		private OperationContainer operationContainer;

		// Token: 0x040012C9 RID: 4809
		private GroupContainer groupContainer;

		// Token: 0x040012CA RID: 4810
		private IObjectsStateParams objectsStateParams;

		// Token: 0x040012CB RID: 4811
		private MapEditorScene mapEditorScene;

		// Token: 0x040012CC RID: 4812
		private IStatusbar statusbar;

		// Token: 0x040012CD RID: 4813
		private MultiObjectBrowserForm multiObjectBrowserForm;

		// Token: 0x040012CE RID: 4814
		private bool allowOperations;

		// Token: 0x040012CF RID: 4815
		private MapObjectClipboard clipboard;

		// Token: 0x040012D0 RID: 4816
		private MultiState multiState;

		// Token: 0x040012D1 RID: 4817
		private ItemDataContainer itemDataContainer;

		// Token: 0x040012D2 RID: 4818
		private Form parentForm;

		// Token: 0x040012D3 RID: 4819
		private IPropertyControl propertyControl;

		// Token: 0x040012D4 RID: 4820
		private string mapResourceName = string.Empty;

		// Token: 0x040012D5 RID: 4821
		private string continentName = string.Empty;

		// Token: 0x040012D6 RID: 4822
		private ContinentType continentType;

		// Token: 0x040012D7 RID: 4823
		private string currentItem = string.Empty;

		// Token: 0x040012D8 RID: 4824
		private Position currentPosition = Position.Empty;

		// Token: 0x040012D9 RID: 4825
		private FramePicker framePicker;

		// Token: 0x040012DA RID: 4826
		private readonly Color framePickerUserGeometryColor = Color.FromArgb(255, Color.White);

		// Token: 0x040012DB RID: 4827
		private int framePickerUserGeometryID = -1;

		// Token: 0x040012DC RID: 4828
		private bool restoreHideAllObjects;

		// Token: 0x040012DD RID: 4829
		private PointPicker pointPicker;

		// Token: 0x040012DE RID: 4830
		private ScreenFramePicker screenFramePicker;

		// Token: 0x040012DF RID: 4831
		private readonly Color screenFramePickerUserGeometryColor = Color.FromArgb(255, Color.White);

		// Token: 0x040012E0 RID: 4832
		private MapObjectSelector selector;

		// Token: 0x040012E1 RID: 4833
		private List<IState> states;

		// Token: 0x02000274 RID: 628
		private enum PropetiesType
		{
			// Token: 0x040012E3 RID: 4835
			Default,
			// Token: 0x040012E4 RID: 4836
			Special,
			// Token: 0x040012E5 RID: 4837
			SpawnTuner
		}

		// Token: 0x02000275 RID: 629
		internal class ObjectAddState : State
		{
			// Token: 0x06001DD8 RID: 7640 RVA: 0x000C1A58 File Offset: 0x000C0A58
			public ObjectAddState(ObjectsState _objectsState) : base("ObjectAddState")
			{
				this.objectsState = _objectsState;
				this.EnterState = (State.ActivateEvent)Delegate.Combine(this.EnterState, new State.ActivateEvent(this.OnEnterState));
				this.LeaveState = (State.ActivateEvent)Delegate.Combine(this.LeaveState, new State.ActivateEvent(this.OnLeaveState));
				base.AddMethod("mouse_down", new Method(this.OnMouseDown));
				base.AddMethod("mouse_move", new Method(this.OnMouseMove));
				base.AddMethod("mouse_up", new Method(this.OnMouseUp));
				base.AddMethod("mouse_double_click", new Method(this.OnMouseDoubleClick));
				base.AddMethod("break", new Method(this.OnBreak));
				this.clipboard = new MapObjectClipboard(this.objectsState.MapObjectContainer, _objectsState.continentType, this.objectsState.selector);
			}

			// Token: 0x06001DD9 RID: 7641 RVA: 0x000C1B6B File Offset: 0x000C0B6B
			public void Destroy()
			{
				this.clipboard = null;
				this.objectSetSaver = null;
				this.multiObjectSaver = null;
				this.multiObjectSaverMapObjects = null;
				this.multiObjectSaverLinks = null;
				this.objectsState = null;
			}

			// Token: 0x06001DDA RID: 7642 RVA: 0x000C1B98 File Offset: 0x000C0B98
			public bool CanEnter()
			{
				if (this.objectsState != null)
				{
					if (this.objectsState.currentItem.Contains(".xml"))
					{
						if (ObjectSetItemListSource.ValidObjectSet(this.objectsState.currentItem))
						{
							return true;
						}
						if (MultiObjectItemListSource.ValidMultiObject(this.objectsState.currentItem))
						{
							return true;
						}
					}
					else if (this.objectsState.currentItem.Contains(".xdb"))
					{
						return true;
					}
				}
				return false;
			}

			// Token: 0x06001DDB RID: 7643 RVA: 0x000C1C08 File Offset: 0x000C0C08
			private void CreateClipboard()
			{
				bool created = false;
				if (this.objectsState.currentItem.Contains(".xml"))
				{
					this.complexObject = true;
					if (ObjectSetItemListSource.ValidObjectSet(this.objectsState.currentItem))
					{
						if (this.objectSetSaver == null)
						{
							this.multiObjectSaver = null;
							this.objectSetSaver = ObjectSetSaver.Load(this.objectsState.currentItem);
							if (this.objectSetSaver != null)
							{
								this.clipboard.Create(this.objectSetSaver.Objects.Get(), this.objectsState.mapEditorScene.EditorScene);
								created = true;
							}
						}
					}
					else if (MultiObjectItemListSource.ValidMultiObject(this.objectsState.currentItem) && this.multiObjectSaver == null)
					{
						this.objectSetSaver = null;
						this.multiObjectSaver = MultiObjectSaver.Load(this.objectsState.currentItem);
						if (this.multiObjectSaver != null)
						{
							this.multiObjectSaver.Unpack(this.multiObjectSaverMapObjects, this.multiObjectSaverLinks, this.objectsState.MapObjectContainer, true, true, true);
							this.clipboard.Create(this.multiObjectSaverMapObjects, this.multiObjectSaverLinks);
							created = true;
						}
					}
				}
				else if (this.objectsState.currentItem.Contains(".xdb"))
				{
					this.complexObject = false;
					if (this.objectsState.objectsStateParams.RandomizeName)
					{
						if (this.objectSetSaver == null)
						{
							this.multiObjectSaver = null;
							this.objectSetSaver = ObjectSetSaver.Create(this.objectsState.currentItem, this.objectsState.objectsStateParams.NameSource, false);
							if (this.objectSetSaver != null)
							{
								this.clipboard.Create(this.objectSetSaver.Objects.Get(), this.objectsState.mapEditorScene.EditorScene);
								created = true;
							}
						}
					}
					else if (this.clipboard.Empty || this.objectSetSaver != null || this.multiObjectSaver != null)
					{
						this.objectSetSaver = null;
						this.multiObjectSaver = null;
						this.clipboard.Create(this.objectsState.currentItem, this.objectsState.mapEditorScene.EditorScene);
						created = true;
					}
				}
				if (created)
				{
					if (this.objectsState.objectsStateParams.MapObjectRandomizerAvailable)
					{
						this.clipboard.Randomize(this.objectsState.objectsStateParams);
					}
					this.clipboard.Show(this.objectsState.currentPosition);
				}
			}

			// Token: 0x06001DDC RID: 7644 RVA: 0x000C1E78 File Offset: 0x000C0E78
			private void RandomizeClipboard()
			{
				this.CreateClipboard();
				if (this.clipboard != null && !this.clipboard.ObjectsAutolinked)
				{
					if (this.objectSetSaver != null)
					{
						this.clipboard.Hide();
						if (this.complexObject || this.objectsState.objectsStateParams.RandomizeName)
						{
							this.clipboard.Create(this.objectSetSaver.Objects.Get(), this.objectsState.mapEditorScene.EditorScene);
						}
						if (this.objectsState.objectsStateParams.MapObjectRandomizerAvailable)
						{
							this.clipboard.Randomize(this.objectsState.objectsStateParams);
						}
						this.clipboard.Show();
						return;
					}
					if (this.multiObjectSaver != null)
					{
						this.clipboard.Hide();
						this.multiObjectSaver.Unpack(this.multiObjectSaverMapObjects, this.multiObjectSaverLinks, this.objectsState.MapObjectContainer, true, true, true);
						this.clipboard.Create(this.multiObjectSaverMapObjects, this.multiObjectSaverLinks);
						if (this.objectsState.objectsStateParams.MapObjectRandomizerAvailable)
						{
							this.clipboard.Randomize(this.objectsState.objectsStateParams);
						}
						this.clipboard.Show();
						return;
					}
					if (this.objectsState.objectsStateParams.RandomizeName)
					{
						this.objectSetSaver = ObjectSetSaver.Create(this.objectsState.currentItem, this.objectsState.objectsStateParams.NameSource, false);
						if (this.objectSetSaver != null && this.multiObjectSaver != null)
						{
							this.clipboard.Hide();
							this.multiObjectSaver.Unpack(this.multiObjectSaverMapObjects, this.multiObjectSaverLinks, this.objectsState.MapObjectContainer, true, true, true);
							this.clipboard.Create(this.objectSetSaver.Objects.Get(), this.objectsState.mapEditorScene.EditorScene);
							if (this.objectsState.objectsStateParams.MapObjectRandomizerAvailable)
							{
								this.clipboard.Randomize(this.objectsState.objectsStateParams);
							}
							this.clipboard.Show();
							return;
						}
						if (this.objectsState.objectsStateParams.MapObjectRandomizerAvailable)
						{
							this.clipboard.Hide();
							this.clipboard.Randomize(this.objectsState.objectsStateParams);
							this.clipboard.Show();
							return;
						}
					}
					else if (this.objectsState.objectsStateParams.MapObjectRandomizerAvailable)
					{
						this.clipboard.Hide();
						this.clipboard.Randomize(this.objectsState.objectsStateParams);
						this.clipboard.Show();
					}
				}
			}

			// Token: 0x06001DDD RID: 7645 RVA: 0x000C2119 File Offset: 0x000C1119
			private void OnEnterState(IState state)
			{
				if (this.objectsState != null)
				{
					this.objectsState.ClearPickers();
					this.objectsState.selector.Clear();
					this.objectsState.Begin();
					this.CreateClipboard();
				}
			}

			// Token: 0x06001DDE RID: 7646 RVA: 0x000C2150 File Offset: 0x000C1150
			private void OnLeaveState(IState state)
			{
				if (this.objectsState != null)
				{
					this.clipboard.Hide();
					this.clipboard.Clear();
					this.objectSetSaver = null;
					this.multiObjectSaver = null;
					this.multiObjectSaverMapObjects.Clear();
					this.multiObjectSaverLinks.Clear();
					this.objectsState.End(true);
					if (base.Container != null)
					{
						base.Container.Invoke("_clear_selection_in_object_item_list", default(MethodArgs));
					}
				}
			}

			// Token: 0x06001DDF RID: 7647 RVA: 0x000C21CC File Offset: 0x000C11CC
			private void OnMouseDown(MethodArgs methodArgs)
			{
				if (this.objectsState != null)
				{
					MouseEventArgs mouseEventArgs = methodArgs.eventArgs as MouseEventArgs;
					if (mouseEventArgs != null)
					{
						this.objectsState.UpdatePosition(mouseEventArgs.X, mouseEventArgs.Y, true);
					}
				}
			}

			// Token: 0x06001DE0 RID: 7648 RVA: 0x000C220C File Offset: 0x000C120C
			private void OnMouseMove(MethodArgs methodArgs)
			{
				if (this.objectsState != null)
				{
					MouseEventArgs mouseEventArgs = methodArgs.eventArgs as MouseEventArgs;
					if (mouseEventArgs != null)
					{
						this.objectsState.UpdatePosition(mouseEventArgs.X, mouseEventArgs.Y, true);
						this.clipboard.Position = this.objectsState.currentPosition;
					}
				}
			}

			// Token: 0x06001DE1 RID: 7649 RVA: 0x000C2260 File Offset: 0x000C1260
			private void OnMouseUp(MethodArgs methodArgs)
			{
				if (this.objectsState != null)
				{
					MouseEventArgs mouseEventArgs = methodArgs.eventArgs as MouseEventArgs;
					if (mouseEventArgs != null)
					{
						this.objectsState.UpdatePosition(mouseEventArgs.X, mouseEventArgs.Y, true);
						if (mouseEventArgs.Button == MouseButtons.Left)
						{
							List<IMapObject> mapObjectsForEdit = null;
							if (this.objectsState.selector.EditObjectAfterAdd)
							{
								mapObjectsForEdit = new List<IMapObject>();
							}
							this.clipboard.Paste(this.objectsState.currentPosition, mapObjectsForEdit, this.objectsState.objectsStateParams.Group);
							if (this.objectsState.selector.EditObjectAfterAdd)
							{
								this.objectsState.selector.Add(mapObjectsForEdit);
								this.objectsState.multiState.ActiveStateLabel = "ObjectSelectState";
								return;
							}
							this.RandomizeClipboard();
							return;
						}
						else if (mouseEventArgs.Button == MouseButtons.Middle)
						{
							this.RandomizeClipboard();
						}
					}
				}
			}

			// Token: 0x06001DE2 RID: 7650 RVA: 0x000C2348 File Offset: 0x000C1348
			private void OnMouseDoubleClick(MethodArgs methodArgs)
			{
				if (this.objectsState != null)
				{
					MouseEventArgs mouseEventArgs = methodArgs.eventArgs as MouseEventArgs;
					if (mouseEventArgs != null)
					{
						this.objectsState.UpdatePosition(mouseEventArgs.X, mouseEventArgs.Y, true);
						if (mouseEventArgs.Button == MouseButtons.Left || mouseEventArgs.Button == MouseButtons.Right)
						{
							this.objectsState.multiState.ActiveStateLabel = "ObjectSelectState";
						}
					}
				}
			}

			// Token: 0x06001DE3 RID: 7651 RVA: 0x000C23B4 File Offset: 0x000C13B4
			private void OnBreak(MethodArgs methodArgs)
			{
				if (this.objectsState != null)
				{
					this.objectsState.multiState.ActiveStateLabel = "ObjectSelectState";
				}
			}

			// Token: 0x040012E6 RID: 4838
			private ObjectsState objectsState;

			// Token: 0x040012E7 RID: 4839
			private MapObjectClipboard clipboard;

			// Token: 0x040012E8 RID: 4840
			private ObjectSetSaver objectSetSaver;

			// Token: 0x040012E9 RID: 4841
			private MultiObjectSaver multiObjectSaver;

			// Token: 0x040012EA RID: 4842
			private List<IMapObject> multiObjectSaverMapObjects = new List<IMapObject>();

			// Token: 0x040012EB RID: 4843
			private Dictionary<int, Dictionary<int, ILinkData>> multiObjectSaverLinks = new Dictionary<int, Dictionary<int, ILinkData>>();

			// Token: 0x040012EC RID: 4844
			private bool complexObject;
		}

		// Token: 0x02000276 RID: 630
		internal class ObjectAddRowState : State
		{
			// Token: 0x06001DE4 RID: 7652 RVA: 0x000C23D4 File Offset: 0x000C13D4
			private void MoveLastPoint(Vec3 point)
			{
				if (this.objectsState.mapObjectContainer.ObjectBounds != null)
				{
					Position position = new Position(point);
					this.objectsState.mapObjectContainer.ObjectBounds.PositionBounds.Validate(ref position);
					point = position.Vec3;
				}
				if (this.polygon.Count > 0)
				{
					this.polygon.MovePointTo(this.polygon.Count - 1, point);
				}
			}

			// Token: 0x06001DE5 RID: 7653 RVA: 0x000C2450 File Offset: 0x000C1450
			private void AddPoint(Vec3 point)
			{
				if (this.objectsState.mapObjectContainer.ObjectBounds != null)
				{
					Position position = new Position(point);
					this.objectsState.mapObjectContainer.ObjectBounds.PositionBounds.Validate(ref position);
					point = position.Vec3;
				}
				if (this.polygon.Count > 0)
				{
					this.polygon.InsertPoint(this.polygon.Count - 1, point, true, false);
					return;
				}
				this.polygon.AddPoint(point, false, false);
			}

			// Token: 0x06001DE6 RID: 7654 RVA: 0x000C24DC File Offset: 0x000C14DC
			public ObjectAddRowState(ObjectsState _objectsState) : base("ObjectAddRowState")
			{
				this.objectsState = _objectsState;
				if (this.objectsState != null)
				{
					this.EnterState = (State.ActivateEvent)Delegate.Combine(this.EnterState, new State.ActivateEvent(this.OnEnterState));
					this.LeaveState = (State.ActivateEvent)Delegate.Combine(this.LeaveState, new State.ActivateEvent(this.OnLeaveState));
					base.AddMethod("mouse_down", new Method(this.OnMouseDown));
					base.AddMethod("mouse_move", new Method(this.OnMouseMove));
					base.AddMethod("mouse_up", new Method(this.OnMouseUp));
					base.AddMethod("mouse_double_click", new Method(this.OnMouseDoubleClick));
					base.AddMethod("break", new Method(this.OnBreak));
					this.polygon = new Polygon(-1, this.objectsState.mapObjectContainer);
				}
			}

			// Token: 0x06001DE7 RID: 7655 RVA: 0x000C25D4 File Offset: 0x000C15D4
			public void Destroy()
			{
				this.polygon = null;
				if (this.rowBuilder != null)
				{
					this.rowBuilder.Destroy();
					this.rowBuilder = null;
				}
				this.objectSetSaver = null;
				this.objectsState = null;
			}

			// Token: 0x06001DE8 RID: 7656 RVA: 0x000C2608 File Offset: 0x000C1608
			public bool CanEnter()
			{
				if (this.objectsState != null)
				{
					if (this.objectsState.currentItem.Contains(".xml"))
					{
						if (ObjectSetItemListSource.ValidObjectSet(this.objectsState.currentItem))
						{
							return true;
						}
					}
					else if (this.objectsState.currentItem.Contains(".xdb"))
					{
						return true;
					}
				}
				return false;
			}

			// Token: 0x06001DE9 RID: 7657 RVA: 0x000C2664 File Offset: 0x000C1664
			private void CreateRowBuilder()
			{
				bool created = false;
				if (this.objectsState.currentItem.Contains(".xml"))
				{
					if (this.objectSetSaver == null && ObjectSetItemListSource.ValidObjectSet(this.objectsState.currentItem))
					{
						this.objectSetSaver = ObjectSetSaver.Load(this.objectsState.currentItem);
						created = true;
					}
				}
				else if (this.objectsState.currentItem.Contains(".xdb") && (this.objectSetSaver == null || this.randomizeNameRowBuilderStatus != this.objectsState.objectsStateParams.RandomizeName))
				{
					this.objectSetSaver = ObjectSetSaver.Create(this.objectsState.currentItem, this.objectsState.objectsStateParams.RandomizeName ? this.objectsState.objectsStateParams.NameSource : null, true);
					this.randomizeNameRowBuilderStatus = this.objectsState.objectsStateParams.RandomizeName;
					created = true;
				}
				if (this.rowBuilder == null || created)
				{
					if (this.rowBuilder != null)
					{
						this.rowBuilder.Destroy();
					}
					this.rowBuilder = new MapObjectRowBuilder(this.objectsState.MapObjectContainer, this.objectsState.continentType, this.objectSetSaver, this.objectsState.mapEditorScene.EditorScene);
					this.rowBuilder.Visible = true;
				}
			}

			// Token: 0x06001DEA RID: 7658 RVA: 0x000C27B4 File Offset: 0x000C17B4
			private void OnEnterState(IState state)
			{
				if (this.objectsState != null)
				{
					this.polygon.Clear();
					this.polygon.Color = ObjectsState.ObjectAddRowState.polygonColor;
					this.polygon.Surface = this.objectsState.selector.Surface;
					this.objectsState.mapEditorScene.PolygonContainer.AddPolygon(this.polygon);
					this.objectsState.UpdatePosition(true);
					this.AddPoint(this.objectsState.currentPosition.Vec3);
					this.objectsState.ClearPickers();
					this.objectsState.selector.Clear();
					this.objectsState.Begin();
					this.objectSetSaver = null;
					this.CreateRowBuilder();
					if (this.objectsState.selector.EditObjectAfterAdd)
					{
						this.mapObjectsForEdit = new List<IMapObject>();
					}
				}
			}

			// Token: 0x06001DEB RID: 7659 RVA: 0x000C2894 File Offset: 0x000C1894
			private void OnLeaveState(IState state)
			{
				if (this.objectsState != null)
				{
					if (this.rowBuilder != null)
					{
						this.rowBuilder.Visible = false;
						this.rowBuilder.Destroy();
						this.rowBuilder = null;
					}
					if (this.objectsState.selector.EditObjectAfterAdd && this.mapObjectsForEdit != null)
					{
						this.objectsState.selector.Add(this.mapObjectsForEdit);
					}
					if (this.mapObjectsForEdit != null)
					{
						this.mapObjectsForEdit.Clear();
						this.mapObjectsForEdit = null;
					}
					this.objectsState.mapEditorScene.PolygonContainer.RemovePolygon(this.polygon);
					this.objectsState.End(true);
					if (base.Container != null)
					{
						base.Container.Invoke("_clear_selection_in_object_item_list", default(MethodArgs));
					}
				}
			}

			// Token: 0x06001DEC RID: 7660 RVA: 0x000C2964 File Offset: 0x000C1964
			private void OnMouseDown(MethodArgs methodArgs)
			{
				if (this.objectsState != null)
				{
					MouseEventArgs mouseEventArgs = methodArgs.eventArgs as MouseEventArgs;
					if (mouseEventArgs != null)
					{
						this.objectsState.UpdatePosition(mouseEventArgs.X, mouseEventArgs.Y, true);
					}
				}
			}

			// Token: 0x06001DED RID: 7661 RVA: 0x000C29A4 File Offset: 0x000C19A4
			private void OnMouseMove(MethodArgs methodArgs)
			{
				if (this.objectsState == null)
				{
					return;
				}
				MouseEventArgs mouseEventArgs = methodArgs.eventArgs as MouseEventArgs;
				if (mouseEventArgs == null)
				{
					return;
				}
				this.objectsState.UpdatePosition(mouseEventArgs.X, mouseEventArgs.Y, true);
				this.MoveLastPoint(this.objectsState.currentPosition.Vec3);
				this.CreateRowBuilder();
				if (this.rowBuilder != null && this.polygon.Points.Count > 1)
				{
					Vec3 end;
					this.rowBuilder.FormatRow(this.polygon.Points[this.polygon.Points.Count - 2], this.polygon.Points[this.polygon.Points.Count - 1], this.objectsState.selector.Surface, this.objectsState.objectsStateParams.OneSidedRow, false, out end);
				}
			}

			// Token: 0x06001DEE RID: 7662 RVA: 0x000C2A90 File Offset: 0x000C1A90
			private void OnMouseUp(MethodArgs methodArgs)
			{
				if (this.objectsState == null)
				{
					return;
				}
				MouseEventArgs mouseEventArgs = methodArgs.eventArgs as MouseEventArgs;
				if (mouseEventArgs == null)
				{
					return;
				}
				this.objectsState.UpdatePosition(mouseEventArgs.X, mouseEventArgs.Y, true);
				Vec3 point = this.objectsState.currentPosition.Vec3;
				if (mouseEventArgs.Button == MouseButtons.Left)
				{
					this.MoveLastPoint(point);
					this.CreateRowBuilder();
					if (this.rowBuilder != null)
					{
						if (this.polygon.Points.Count > 1)
						{
							Vec3 end;
							this.rowBuilder.FormatRow(this.polygon.Points[this.polygon.Points.Count - 2], this.polygon.Points[this.polygon.Points.Count - 1], this.objectsState.selector.Surface, this.objectsState.objectsStateParams.OneSidedRow, false, out end);
							point = end;
							List<IMapObject> _mapObjectsForEdit = null;
							if (this.objectsState.selector.EditObjectAfterAdd)
							{
								_mapObjectsForEdit = new List<IMapObject>();
								if (this.mapObjectsForEdit == null)
								{
									this.mapObjectsForEdit = new List<IMapObject>();
								}
							}
							this.rowBuilder.Paste(_mapObjectsForEdit, this.objectsState.objectsStateParams.Group);
							this.rowBuilder.Clear();
							if (_mapObjectsForEdit != null && this.objectsState.selector.EditObjectAfterAdd)
							{
								this.mapObjectsForEdit.AddRange(_mapObjectsForEdit);
							}
						}
						this.AddPoint(point);
						if (this.polygon.Points.Count > 1)
						{
							Vec3 end;
							this.rowBuilder.FormatRow(this.polygon.Points[this.polygon.Points.Count - 2], this.polygon.Points[this.polygon.Points.Count - 1], this.objectsState.selector.Surface, this.objectsState.objectsStateParams.OneSidedRow, false, out end);
							return;
						}
					}
				}
				else if (mouseEventArgs.Button == MouseButtons.Middle)
				{
					this.MoveLastPoint(point);
					this.CreateRowBuilder();
					if (this.rowBuilder != null && this.polygon.Points.Count > 1)
					{
						Vec3 end;
						this.rowBuilder.FormatRow(this.polygon.Points[this.polygon.Points.Count - 2], this.polygon.Points[this.polygon.Points.Count - 1], this.objectsState.selector.Surface, this.objectsState.objectsStateParams.OneSidedRow, true, out end);
					}
				}
			}

			// Token: 0x06001DEF RID: 7663 RVA: 0x000C2D44 File Offset: 0x000C1D44
			private void OnMouseDoubleClick(MethodArgs methodArgs)
			{
				if (this.objectsState != null)
				{
					MouseEventArgs mouseEventArgs = methodArgs.eventArgs as MouseEventArgs;
					if (mouseEventArgs != null)
					{
						this.objectsState.UpdatePosition(mouseEventArgs.X, mouseEventArgs.Y, true);
						if (mouseEventArgs.Button == MouseButtons.Left || mouseEventArgs.Button == MouseButtons.Right)
						{
							this.objectsState.multiState.ActiveStateLabel = "ObjectSelectState";
						}
					}
				}
			}

			// Token: 0x06001DF0 RID: 7664 RVA: 0x000C2DB0 File Offset: 0x000C1DB0
			private void OnBreak(MethodArgs methodArgs)
			{
				if (this.objectsState != null)
				{
					this.objectsState.multiState.ActiveStateLabel = "ObjectSelectState";
				}
			}

			// Token: 0x06001DF1 RID: 7665 RVA: 0x000C2DD0 File Offset: 0x000C1DD0
			public void ReformatRow()
			{
				if (this.rowBuilder != null && this.polygon.Points.Count > 1)
				{
					Vec3 end;
					this.rowBuilder.FormatRow(this.polygon.Points[this.polygon.Points.Count - 2], this.polygon.Points[this.polygon.Points.Count - 1], this.objectsState.selector.Surface, this.objectsState.objectsStateParams.OneSidedRow, true, out end);
				}
			}

			// Token: 0x040012ED RID: 4845
			private static readonly Color polygonColor = Color.White;

			// Token: 0x040012EE RID: 4846
			private ObjectsState objectsState;

			// Token: 0x040012EF RID: 4847
			private MapObjectRowBuilder rowBuilder;

			// Token: 0x040012F0 RID: 4848
			private ObjectSetSaver objectSetSaver;

			// Token: 0x040012F1 RID: 4849
			private Polygon polygon;

			// Token: 0x040012F2 RID: 4850
			private List<IMapObject> mapObjectsForEdit;

			// Token: 0x040012F3 RID: 4851
			private bool randomizeNameRowBuilderStatus;
		}

		// Token: 0x02000277 RID: 631
		internal class ObjectEditState : State
		{
			// Token: 0x06001DF3 RID: 7667 RVA: 0x000C2E7C File Offset: 0x000C1E7C
			public ObjectEditState(ObjectsState _objectsState) : base("ObjectEditState")
			{
				this.objectsState = _objectsState;
				this.EnterState = (State.ActivateEvent)Delegate.Combine(this.EnterState, new State.ActivateEvent(this.OnEnterState));
				this.LeaveState = (State.ActivateEvent)Delegate.Combine(this.LeaveState, new State.ActivateEvent(this.OnLeaveState));
				base.AddMethod("mouse_down", new Method(this.OnMouseDown));
				base.AddMethod("mouse_move", new Method(this.OnMouseMove));
				base.AddMethod("mouse_up", new Method(this.OnMouseUp));
				base.AddMethod("mouse_double_click", new Method(this.OnMouseDoubleClick));
				base.AddMethod("delete", new Method(this.OnDelete));
				base.AddMethod("break", new Method(this.OnBreak));
				base.AddMethod("copy", new Method(this.OnCopy));
				base.AddMethod("paste", new Method(this.OnPaste));
				base.AddMethod("altitude_reset", new Method(this.OnAltitudeReset));
				base.AddMethod("drop_to_nearest", new Method(this.OnDropToNearest));
				base.AddMethod("rotation_reset", new Method(this.OnRotationReset));
				base.AddMethod("rotation_along_normal", new Method(this.OnRotationAlongNormal));
				base.AddMethod("scale_reset", new Method(this.OnScaleReset));
				base.AddMethod("arrange_linked_items", new Method(this.OnArrangeLinkedItems));
				base.AddMethod("rearrange_linked_items", new Method(this.OnRearrangeLinkedItems));
				base.AddMethod("create_spawn_table", new Method(this.OnCreateSpawnTable));
				base.AddMethod("clone_spawn_table", new Method(this.OnCloneSpawnTable));
				base.AddMethod("add_fixed_idle_animation_tuner", new Method(this.OnAddFixedIdleAnimationTuner));
			}

			// Token: 0x06001DF4 RID: 7668 RVA: 0x000C307D File Offset: 0x000C207D
			public void Destroy()
			{
				this.objectsState = null;
			}

			// Token: 0x06001DF5 RID: 7669 RVA: 0x000C3086 File Offset: 0x000C2086
			private void OnEnterState(IState state)
			{
				if (this.objectsState != null)
				{
					this.objectsState.Begin();
				}
			}

			// Token: 0x06001DF6 RID: 7670 RVA: 0x000C309B File Offset: 0x000C209B
			private void OnLeaveState(IState state)
			{
				if (this.objectsState != null)
				{
					this.objectsState.selector.ClearPick();
					this.objectsState.End(true);
				}
			}

			// Token: 0x06001DF7 RID: 7671 RVA: 0x000C30C4 File Offset: 0x000C20C4
			private void OnMouseDown(MethodArgs methodArgs)
			{
				if (this.objectsState != null)
				{
					MouseEventArgs mouseEventArgs = methodArgs.eventArgs as MouseEventArgs;
					if (mouseEventArgs != null)
					{
						this.objectsState.UpdatePosition(mouseEventArgs.X, mouseEventArgs.Y, true);
						if (mouseEventArgs.Button == MouseButtons.Right && KeyStatus.LeftMouse)
						{
							this.objectsState.pointPicker.StartPick(mouseEventArgs.X, mouseEventArgs.Y);
							bool result = this.objectsState.pointPicker.SearchAndSetCurrentObject(this.objectsState.selector.MapObjects);
							if (result)
							{
								PickedMapObject currentPickedMapObject = this.objectsState.pointPicker.CurrentMapObject;
								PickedMapObject nextPickedMapObject = this.objectsState.pointPicker.NextMapObject;
								if (currentPickedMapObject != null && currentPickedMapObject.MapObject != null && nextPickedMapObject != null && nextPickedMapObject.MapObject != null && currentPickedMapObject.MapObject != nextPickedMapObject.MapObject)
								{
									this.objectsState.End(true);
									this.objectsState.selector.Remove(currentPickedMapObject.MapObject);
									this.objectsState.selector.Add(nextPickedMapObject.MapObject);
									this.objectsState.selector.Pick(mouseEventArgs.X, mouseEventArgs.Y, this.objectsState.pointPicker);
									this.objectsState.Begin();
								}
							}
						}
					}
				}
			}

			// Token: 0x06001DF8 RID: 7672 RVA: 0x000C3220 File Offset: 0x000C2220
			private void OnMouseMove(MethodArgs methodArgs)
			{
				if (this.objectsState != null)
				{
					MouseEventArgs mouseEventArgs = methodArgs.eventArgs as MouseEventArgs;
					if (mouseEventArgs != null)
					{
						this.objectsState.UpdatePosition(mouseEventArgs.X, mouseEventArgs.Y, true);
						if ((KeyStatus.LeftMouse || KeyStatus.MiddleMouse) && this.objectsState.selector.Picked)
						{
							this.objectsState.selector.ProcessPick(mouseEventArgs.X, mouseEventArgs.Y);
						}
					}
				}
			}

			// Token: 0x06001DF9 RID: 7673 RVA: 0x000C329C File Offset: 0x000C229C
			private void OnMouseUp(MethodArgs methodArgs)
			{
				if (this.objectsState != null)
				{
					MouseEventArgs mouseEventArgs = methodArgs.eventArgs as MouseEventArgs;
					if (mouseEventArgs != null)
					{
						this.objectsState.UpdatePosition(mouseEventArgs.X, mouseEventArgs.Y, true);
						if (mouseEventArgs.Button == MouseButtons.Left || mouseEventArgs.Button == MouseButtons.Middle)
						{
							this.objectsState.multiState.ActiveStateLabel = "ObjectSelectState";
						}
					}
				}
			}

			// Token: 0x06001DFA RID: 7674 RVA: 0x000C3308 File Offset: 0x000C2308
			private void OnMouseDoubleClick(MethodArgs methodArgs)
			{
				if (this.objectsState != null)
				{
					MouseEventArgs mouseEventArgs = methodArgs.eventArgs as MouseEventArgs;
					if (mouseEventArgs != null)
					{
						this.objectsState.UpdatePosition(mouseEventArgs.X, mouseEventArgs.Y, true);
						if (mouseEventArgs.Button == MouseButtons.Left && this.objectsState.objectsStateParams.EnableDoubleClickProperties)
						{
							this.objectsState.ShowProperties(!KeyStatus.Shift, true, ObjectsState.PropetiesType.Default);
						}
					}
				}
			}

			// Token: 0x06001DFB RID: 7675 RVA: 0x000C337C File Offset: 0x000C237C
			private void OnDelete(MethodArgs methodArgs)
			{
				if (this.objectsState != null && !this.objectsState.selector.Empty)
				{
					this.objectsState.Update();
					this.objectsState.RemoveSelectedItems();
					this.objectsState.multiState.ActiveStateLabel = "ObjectSelectState";
				}
			}

			// Token: 0x06001DFC RID: 7676 RVA: 0x000C33CE File Offset: 0x000C23CE
			private void OnBreak(MethodArgs methodArgs)
			{
				if (this.objectsState != null && this.objectsState.selector.Picked)
				{
					this.objectsState.End(false);
					this.objectsState.multiState.ActiveStateLabel = "ObjectSelectState";
				}
			}

			// Token: 0x06001DFD RID: 7677 RVA: 0x000C340C File Offset: 0x000C240C
			private void OnCopy(MethodArgs methodArgs)
			{
				if (this.objectsState != null && !this.objectsState.selector.Empty)
				{
					this.objectsState.clipboard.Copy(this.objectsState.selector.MapObjects);
					if (this.objectsState.objectsStateParams.MapObjectRandomizerAvailable)
					{
						this.objectsState.clipboard.Randomize(this.objectsState.objectsStateParams);
					}
				}
			}

			// Token: 0x06001DFE RID: 7678 RVA: 0x000C3481 File Offset: 0x000C2481
			private void OnPaste(MethodArgs methodArgs)
			{
				if (this.objectsState != null && !this.objectsState.selector.Empty)
				{
					this.objectsState.multiState.ActiveStateLabel = "ObjectPasteState";
				}
			}

			// Token: 0x06001DFF RID: 7679 RVA: 0x000C34B4 File Offset: 0x000C24B4
			private void OnAltitudeReset(MethodArgs methodArgs)
			{
				if (this.objectsState != null && !this.objectsState.selector.Empty)
				{
					this.objectsState.selector.ForceValue = true;
					this.objectsState.selector.Altitude = MapObjectCreationInfo.DefaultAltitude;
					this.objectsState.selector.ForceValue = false;
				}
			}

			// Token: 0x06001E00 RID: 7680 RVA: 0x000C3514 File Offset: 0x000C2514
			private void OnDropToNearest(MethodArgs methodArgs)
			{
				if (this.objectsState != null && !this.objectsState.selector.Empty)
				{
					this.objectsState.Begin();
					this.objectsState.selector.DropToNearestHeight();
					this.objectsState.End(true);
				}
			}

			// Token: 0x06001E01 RID: 7681 RVA: 0x000C3564 File Offset: 0x000C2564
			private void OnRotationReset(MethodArgs methodArgs)
			{
				if (this.objectsState != null && !this.objectsState.selector.Empty)
				{
					this.objectsState.selector.ForceValue = true;
					this.objectsState.selector.Rotation = MapObjectCreationInfo.DefaultRotation;
					this.objectsState.selector.ForceValue = false;
				}
			}

			// Token: 0x06001E02 RID: 7682 RVA: 0x000C35C4 File Offset: 0x000C25C4
			private void OnRotationAlongNormal(MethodArgs methodArgs)
			{
				if (this.objectsState != null && !this.objectsState.selector.Empty)
				{
					this.objectsState.Begin();
					this.objectsState.selector.RotateAlongNormal();
					this.objectsState.End(true);
				}
			}

			// Token: 0x06001E03 RID: 7683 RVA: 0x000C3614 File Offset: 0x000C2614
			private void OnScaleReset(MethodArgs methodArgs)
			{
				if (this.objectsState != null && !this.objectsState.selector.Empty)
				{
					this.objectsState.selector.ForceValue = true;
					this.objectsState.selector.Scale = MapObjectCreationInfo.DefaultScale;
					this.objectsState.selector.ForceValue = false;
				}
			}

			// Token: 0x06001E04 RID: 7684 RVA: 0x000C3674 File Offset: 0x000C2674
			private void OnArrangeLinkedItems(MethodArgs methodArgs)
			{
				if (this.objectsState != null && !this.objectsState.selector.Empty)
				{
					this.objectsState.Begin();
					this.objectsState.selector.ArrangeLinkedObjects();
					this.objectsState.End(true);
				}
			}

			// Token: 0x06001E05 RID: 7685 RVA: 0x000C36C4 File Offset: 0x000C26C4
			private void OnRearrangeLinkedItems(MethodArgs methodArgs)
			{
				if (this.objectsState != null && !this.objectsState.selector.Empty)
				{
					this.objectsState.Begin();
					this.objectsState.selector.RearrangeLinkedObjects();
					this.objectsState.End(true);
				}
			}

			// Token: 0x06001E06 RID: 7686 RVA: 0x000C3712 File Offset: 0x000C2712
			private void OnCreateSpawnTable(MethodArgs methodArgs)
			{
				if (this.objectsState != null)
				{
					this.objectsState.CreateSpawnTable();
				}
			}

			// Token: 0x06001E07 RID: 7687 RVA: 0x000C3727 File Offset: 0x000C2727
			private void OnCloneSpawnTable(MethodArgs methodArgs)
			{
				if (this.objectsState != null)
				{
					this.objectsState.CloneSpawnTable();
				}
			}

			// Token: 0x06001E08 RID: 7688 RVA: 0x000C373C File Offset: 0x000C273C
			private void OnAddFixedIdleAnimationTuner(MethodArgs methodArgs)
			{
				if (this.objectsState != null)
				{
					this.objectsState.AddFixedIdleAnimationTuner();
				}
			}

			// Token: 0x040012F4 RID: 4852
			private ObjectsState objectsState;
		}

		// Token: 0x02000278 RID: 632
		internal class ObjectPasteState : State
		{
			// Token: 0x06001E09 RID: 7689 RVA: 0x000C3754 File Offset: 0x000C2754
			public ObjectPasteState(ObjectsState _objectsState) : base("ObjectPasteState")
			{
				this.objectsState = _objectsState;
				this.EnterState = (State.ActivateEvent)Delegate.Combine(this.EnterState, new State.ActivateEvent(this.OnEnterState));
				this.LeaveState = (State.ActivateEvent)Delegate.Combine(this.LeaveState, new State.ActivateEvent(this.OnLeaveState));
				base.AddMethod("mouse_down", new Method(this.OnMouseDown));
				base.AddMethod("mouse_move", new Method(this.OnMouseMove));
				base.AddMethod("mouse_up", new Method(this.OnMouseUp));
				base.AddMethod("mouse_double_click", new Method(this.OnMouseDoubleClick));
				base.AddMethod("break", new Method(this.OnBreak));
			}

			// Token: 0x06001E0A RID: 7690 RVA: 0x000C382A File Offset: 0x000C282A
			public void Destroy()
			{
				this.objectsState = null;
			}

			// Token: 0x06001E0B RID: 7691 RVA: 0x000C3834 File Offset: 0x000C2834
			private void OnEnterState(IState state)
			{
				if (this.objectsState != null)
				{
					this.objectsState.ClearPickers();
					this.objectsState.selector.Clear();
					this.objectsState.Begin();
					Position position = this.objectsState.currentPosition;
					position.Z += this.objectsState.clipboard.AdditionalAltitude;
					this.objectsState.clipboard.Show(position);
				}
			}

			// Token: 0x06001E0C RID: 7692 RVA: 0x000C38AA File Offset: 0x000C28AA
			private void OnLeaveState(IState state)
			{
				if (this.objectsState != null)
				{
					this.objectsState.clipboard.Hide();
					this.objectsState.End(true);
				}
			}

			// Token: 0x06001E0D RID: 7693 RVA: 0x000C38D0 File Offset: 0x000C28D0
			private void OnMouseDown(MethodArgs methodArgs)
			{
				if (this.objectsState != null)
				{
					MouseEventArgs mouseEventArgs = methodArgs.eventArgs as MouseEventArgs;
					if (mouseEventArgs != null)
					{
						this.objectsState.UpdatePosition(mouseEventArgs.X, mouseEventArgs.Y, true);
					}
				}
			}

			// Token: 0x06001E0E RID: 7694 RVA: 0x000C3910 File Offset: 0x000C2910
			private void OnMouseMove(MethodArgs methodArgs)
			{
				if (this.objectsState != null)
				{
					MouseEventArgs mouseEventArgs = methodArgs.eventArgs as MouseEventArgs;
					if (mouseEventArgs != null)
					{
						this.objectsState.UpdatePosition(mouseEventArgs.X, mouseEventArgs.Y, true);
						Position position = this.objectsState.currentPosition;
						position.Z += this.objectsState.clipboard.AdditionalAltitude;
						this.objectsState.clipboard.Position = position;
					}
				}
			}

			// Token: 0x06001E0F RID: 7695 RVA: 0x000C3988 File Offset: 0x000C2988
			private void OnMouseUp(MethodArgs methodArgs)
			{
				if (this.objectsState != null)
				{
					MouseEventArgs mouseEventArgs = methodArgs.eventArgs as MouseEventArgs;
					if (mouseEventArgs != null)
					{
						this.objectsState.UpdatePosition(mouseEventArgs.X, mouseEventArgs.Y, true);
						if (mouseEventArgs.Button == MouseButtons.Left)
						{
							List<IMapObject> mapObjects = null;
							if (this.objectsState.selector.EditObjectAfterAdd)
							{
								mapObjects = new List<IMapObject>();
							}
							Position position = this.objectsState.currentPosition;
							position.Z += this.objectsState.clipboard.AdditionalAltitude;
							this.objectsState.clipboard.Paste(position, mapObjects, this.objectsState.objectsStateParams.Group);
							if (this.objectsState.selector.EditObjectAfterAdd)
							{
								this.objectsState.selector.Add(mapObjects);
								this.objectsState.multiState.ActiveStateLabel = "ObjectSelectState";
							}
						}
					}
				}
			}

			// Token: 0x06001E10 RID: 7696 RVA: 0x000C3A7C File Offset: 0x000C2A7C
			private void OnMouseDoubleClick(MethodArgs methodArgs)
			{
				if (this.objectsState != null)
				{
					MouseEventArgs mouseEventArgs = methodArgs.eventArgs as MouseEventArgs;
					if (mouseEventArgs != null)
					{
						this.objectsState.UpdatePosition(mouseEventArgs.X, mouseEventArgs.Y, true);
						if (mouseEventArgs.Button == MouseButtons.Left || mouseEventArgs.Button == MouseButtons.Right)
						{
							this.objectsState.multiState.ActiveStateLabel = "ObjectSelectState";
						}
					}
				}
			}

			// Token: 0x06001E11 RID: 7697 RVA: 0x000C3AE8 File Offset: 0x000C2AE8
			private void OnBreak(MethodArgs methodArgs)
			{
				if (this.objectsState != null)
				{
					this.objectsState.multiState.ActiveStateLabel = "ObjectSelectState";
				}
			}

			// Token: 0x040012F5 RID: 4853
			private ObjectsState objectsState;
		}

		// Token: 0x02000279 RID: 633
		internal class ObjectSelectState : State
		{
			// Token: 0x06001E12 RID: 7698 RVA: 0x000C3B08 File Offset: 0x000C2B08
			public ObjectSelectState(ObjectsState _objectsState) : base("ObjectSelectState")
			{
				this.objectsState = _objectsState;
				this.EnterState = (State.ActivateEvent)Delegate.Combine(this.EnterState, new State.ActivateEvent(this.OnEnterState));
				this.LeaveState = (State.ActivateEvent)Delegate.Combine(this.LeaveState, new State.ActivateEvent(this.OnLeaveState));
				base.AddMethod("mouse_down", new Method(this.OnMouseDown));
				base.AddMethod("mouse_move", new Method(this.OnMouseMove));
				base.AddMethod("mouse_up", new Method(this.OnMouseUp));
				base.AddMethod("mouse_double_click", new Method(this.OnMouseDoubleClick));
				base.AddMethod("delete", new Method(this.OnDelete));
				base.AddMethod("break", new Method(this.OnBreak));
				base.AddMethod("copy", new Method(this.OnCopy));
				base.AddMethod("paste", new Method(this.OnPaste));
				base.AddMethod("altitude_reset", new Method(this.OnAltitudeReset));
				base.AddMethod("drop_to_nearest", new Method(this.OnDropToNearest));
				base.AddMethod("rotation_reset", new Method(this.OnRotationReset));
				base.AddMethod("rotation_along_normal", new Method(this.OnRotationAlongNormal));
				base.AddMethod("scale_reset", new Method(this.OnScaleReset));
				base.AddMethod("arrange_linked_items", new Method(this.OnArrangeLinkedItems));
				base.AddMethod("rearrange_linked_items", new Method(this.OnRearrangeLinkedItems));
				base.AddMethod("create_spawn_table", new Method(this.OnCreateSpawnTable));
				base.AddMethod("clone_spawn_table", new Method(this.OnCloneSpawnTable));
				base.AddMethod("add_fixed_idle_animation_tuner", new Method(this.OnAddFixedIdleAnimationTuner));
				base.AddMethod("convert_sp_to_client_sp", new Method(this.OnConvertToClientSpawnPoints));
				base.AddMethod("toggle_disassemble_object", new Method(this.OnDisassembleObject));
				base.AddMethod("toggle_replace_static_object", new Method(this.OnReplaceStaticObject));
			}

			// Token: 0x06001E13 RID: 7699 RVA: 0x000C3D4E File Offset: 0x000C2D4E
			public void Destroy()
			{
				this.objectsState = null;
			}

			// Token: 0x06001E14 RID: 7700 RVA: 0x000C3D57 File Offset: 0x000C2D57
			private void Break()
			{
				this.objectsState.ClearPickers();
				this.objectsState.selector.Clear();
			}

			// Token: 0x06001E15 RID: 7701 RVA: 0x000C3D74 File Offset: 0x000C2D74
			private void OnEnterState(IState state)
			{
				this.shiftPressed = false;
			}

			// Token: 0x06001E16 RID: 7702 RVA: 0x000C3D7D File Offset: 0x000C2D7D
			private void OnLeaveState(IState state)
			{
				this.shiftPressed = false;
				this.objectsState.operationContainer.EndTransaction();
			}

			// Token: 0x06001E17 RID: 7703 RVA: 0x000C3D98 File Offset: 0x000C2D98
			private void OnMouseDown(MethodArgs methodArgs)
			{
				if (this.objectsState == null)
				{
					return;
				}
				MouseEventArgs mouseEventArgs = methodArgs.eventArgs as MouseEventArgs;
				if (mouseEventArgs == null)
				{
					return;
				}
				this.objectsState.operationContainer.BeginTransaction();
				this.objectsState.UpdatePosition(mouseEventArgs.X, mouseEventArgs.Y, true);
				this.shiftPressed = KeyStatus.Shift;
				if (mouseEventArgs.Button == MouseButtons.Right)
				{
					if (KeyStatus.LeftMouse)
					{
						this.objectsState.pointPicker.StartPick(mouseEventArgs.X, mouseEventArgs.Y);
						bool result = this.objectsState.pointPicker.SearchAndSetCurrentObject(this.objectsState.selector.MapObjects);
						if (result)
						{
							PickedMapObject currentPickedMapObject = this.objectsState.pointPicker.CurrentMapObject;
							PickedMapObject nextPickedMapObject = this.objectsState.pointPicker.NextMapObject;
							if (currentPickedMapObject != null && currentPickedMapObject.MapObject != null && nextPickedMapObject != null && nextPickedMapObject.MapObject != null && currentPickedMapObject.MapObject != nextPickedMapObject.MapObject)
							{
								this.objectsState.selector.Remove(currentPickedMapObject.MapObject);
								this.objectsState.selector.Add(nextPickedMapObject.MapObject);
								return;
							}
						}
					}
				}
				else if (mouseEventArgs.Button == MouseButtons.Left || mouseEventArgs.Button == MouseButtons.Middle)
				{
					this.objectsState.pointPicker.Clear();
					if (mouseEventArgs.Button == MouseButtons.Left)
					{
						this.objectsState.pointPicker.StartPick(mouseEventArgs.X, mouseEventArgs.Y);
					}
					bool somethingRemoved = false;
					if (this.shiftPressed)
					{
						PickedMapObject pickedMapObject = this.objectsState.pointPicker.FirstMapObject;
						if (pickedMapObject != null && pickedMapObject.MapObject != null)
						{
							somethingRemoved = this.objectsState.selector.Remove(pickedMapObject.MapObject);
						}
					}
					if (!somethingRemoved)
					{
						this.objectsState.selector.Pick(mouseEventArgs.X, mouseEventArgs.Y, this.objectsState.pointPicker);
						if (this.objectsState.selector.Picked)
						{
							this.objectsState.multiState.ActiveStateLabel = "ObjectEditState";
							return;
						}
						if (!this.objectsState.pointPicker.Empty)
						{
							PickedMapObject pickedMapObject2 = this.objectsState.pointPicker.FirstMapObject;
							if (pickedMapObject2 != null && pickedMapObject2.MapObject != null)
							{
								if (!this.objectsState.selector.LockSelection && !this.shiftPressed)
								{
									this.objectsState.selector.Clear();
								}
								if (this.objectsState.selector.Empty || this.shiftPressed)
								{
									this.objectsState.selector.Add(pickedMapObject2.MapObject);
									return;
								}
							}
						}
						else if (this.shiftPressed || this.objectsState.selector.Empty || !this.objectsState.selector.LockSelection)
						{
							if (!this.shiftPressed)
							{
								this.objectsState.selector.Clear();
							}
							if (mouseEventArgs.Button == MouseButtons.Left)
							{
								this.objectsState.screenFramePicker.StartPick(mouseEventArgs.X, mouseEventArgs.Y);
								return;
							}
							if (mouseEventArgs.Button == MouseButtons.Middle)
							{
								this.objectsState.framePicker.Surface = this.objectsState.selector.Surface;
								this.objectsState.framePicker.StartPick(mouseEventArgs.X, mouseEventArgs.Y);
							}
						}
					}
				}
			}

			// Token: 0x06001E18 RID: 7704 RVA: 0x000C4118 File Offset: 0x000C3118
			private void OnMouseMove(MethodArgs methodArgs)
			{
				if (this.objectsState == null)
				{
					return;
				}
				MouseEventArgs mouseEventArgs = methodArgs.eventArgs as MouseEventArgs;
				if (mouseEventArgs == null)
				{
					return;
				}
				this.objectsState.UpdatePosition(mouseEventArgs.X, mouseEventArgs.Y, true);
				if (!KeyStatus.LeftMouse || !this.objectsState.screenFramePicker.Picking)
				{
					if (KeyStatus.MiddleMouse && this.objectsState.framePicker.Picking)
					{
						this.objectsState.operationContainer.BeginTransaction();
						this.objectsState.framePicker.Surface = this.objectsState.selector.Surface;
						this.objectsState.framePicker.ContinuePick(mouseEventArgs.X, mouseEventArgs.Y);
						if (this.shiftPressed)
						{
							this.objectsState.selector.Add(this.objectsState.framePicker);
							return;
						}
						this.objectsState.selector.Sync(this.objectsState.framePicker);
					}
					return;
				}
				this.objectsState.operationContainer.BeginTransaction();
				this.objectsState.screenFramePicker.ContinuePick(mouseEventArgs.X, mouseEventArgs.Y);
				if (this.shiftPressed)
				{
					this.objectsState.selector.Add(this.objectsState.screenFramePicker);
					return;
				}
				this.objectsState.selector.Sync(this.objectsState.screenFramePicker);
			}

			// Token: 0x06001E19 RID: 7705 RVA: 0x000C4288 File Offset: 0x000C3288
			private void OnMouseUp(MethodArgs methodArgs)
			{
				if (this.objectsState == null)
				{
					return;
				}
				MouseEventArgs mouseEventArgs = methodArgs.eventArgs as MouseEventArgs;
				if (mouseEventArgs == null)
				{
					return;
				}
				this.objectsState.UpdatePosition(mouseEventArgs.X, mouseEventArgs.Y, true);
				if (mouseEventArgs.Button == MouseButtons.Left && this.objectsState.screenFramePicker.Picking)
				{
					this.objectsState.screenFramePicker.FinishPick(mouseEventArgs.X, mouseEventArgs.Y);
					if (this.shiftPressed)
					{
						this.objectsState.selector.Add(this.objectsState.screenFramePicker);
					}
					else
					{
						this.objectsState.selector.Sync(this.objectsState.screenFramePicker);
					}
				}
				else if (mouseEventArgs.Button == MouseButtons.Middle && this.objectsState.framePicker.Picking)
				{
					this.objectsState.framePicker.Surface = this.objectsState.selector.Surface;
					this.objectsState.framePicker.FinishPick(mouseEventArgs.X, mouseEventArgs.Y);
					if (this.shiftPressed)
					{
						this.objectsState.selector.Add(this.objectsState.framePicker);
					}
					else
					{
						this.objectsState.selector.Sync(this.objectsState.framePicker);
					}
				}
				this.shiftPressed = false;
				this.objectsState.operationContainer.EndTransaction();
			}

			// Token: 0x06001E1A RID: 7706 RVA: 0x000C4400 File Offset: 0x000C3400
			private void OnMouseDoubleClick(MethodArgs methodArgs)
			{
				if (this.objectsState == null)
				{
					return;
				}
				MouseEventArgs mouseEventArgs = methodArgs.eventArgs as MouseEventArgs;
				if (mouseEventArgs == null)
				{
					return;
				}
				this.objectsState.UpdatePosition(mouseEventArgs.X, mouseEventArgs.Y, true);
				if (mouseEventArgs.Button == MouseButtons.Left)
				{
					if (this.objectsState.objectsStateParams.EnableDoubleClickProperties)
					{
						this.objectsState.ShowProperties(!KeyStatus.Shift, true, ObjectsState.PropetiesType.Default);
						return;
					}
				}
				else if (mouseEventArgs.Button == MouseButtons.Right && !KeyStatus.LeftMouse)
				{
					this.Break();
				}
			}

			// Token: 0x06001E1B RID: 7707 RVA: 0x000C4490 File Offset: 0x000C3490
			private void OnDelete(MethodArgs methodArgs)
			{
				if (this.objectsState == null)
				{
					return;
				}
				if (this.objectsState.selector.Empty)
				{
					return;
				}
				this.objectsState.Begin();
				this.objectsState.RemoveSelectedItems();
				this.objectsState.selector.Clear();
				this.objectsState.End(true);
				this.objectsState.ClearPickers();
			}

			// Token: 0x06001E1C RID: 7708 RVA: 0x000C44F6 File Offset: 0x000C34F6
			private void OnBreak(MethodArgs methodArgs)
			{
				if (this.objectsState != null)
				{
					this.Break();
				}
			}

			// Token: 0x06001E1D RID: 7709 RVA: 0x000C4508 File Offset: 0x000C3508
			private void OnCopy(MethodArgs methodArgs)
			{
				if (this.objectsState == null)
				{
					return;
				}
				if (this.objectsState.selector.Empty)
				{
					return;
				}
				this.objectsState.clipboard.Copy(this.objectsState.selector.MapObjects);
				if (this.objectsState.objectsStateParams.MapObjectRandomizerAvailable)
				{
					this.objectsState.clipboard.Randomize(this.objectsState.objectsStateParams);
				}
				using (Dictionary<IMapObject, IMapObject>.KeyCollection.Enumerator enumerator = this.objectsState.selector.MapObjects.Keys.GetEnumerator())
				{
					if (enumerator.MoveNext())
					{
						IMapObject mapObject = enumerator.Current;
						Clipboard.SetText(mapObject.SceneName ?? string.Empty);
					}
				}
			}

			// Token: 0x06001E1E RID: 7710 RVA: 0x000C45E4 File Offset: 0x000C35E4
			private void OnPaste(MethodArgs methodArgs)
			{
				if (this.objectsState == null)
				{
					return;
				}
				if (!this.objectsState.clipboard.Empty)
				{
					this.objectsState.multiState.ActiveStateLabel = "ObjectPasteState";
				}
			}

			// Token: 0x06001E1F RID: 7711 RVA: 0x000C4618 File Offset: 0x000C3618
			private void OnAltitudeReset(MethodArgs methodArgs)
			{
				if (this.objectsState == null)
				{
					return;
				}
				if (this.objectsState.selector.Empty)
				{
					return;
				}
				this.objectsState.Begin();
				this.objectsState.selector.ForceValue = true;
				this.objectsState.selector.Altitude = MapObjectCreationInfo.DefaultAltitude;
				this.objectsState.selector.ForceValue = false;
				this.objectsState.End(true);
			}

			// Token: 0x06001E20 RID: 7712 RVA: 0x000C4690 File Offset: 0x000C3690
			private void OnDropToNearest(MethodArgs methodArgs)
			{
				if (this.objectsState == null)
				{
					return;
				}
				if (this.objectsState.selector.Empty)
				{
					return;
				}
				this.objectsState.Begin();
				this.objectsState.selector.DropToNearestHeight();
				this.objectsState.End(true);
			}

			// Token: 0x06001E21 RID: 7713 RVA: 0x000C46E0 File Offset: 0x000C36E0
			private void OnRotationReset(MethodArgs methodArgs)
			{
				if (this.objectsState == null)
				{
					return;
				}
				if (this.objectsState.selector.Empty)
				{
					return;
				}
				this.objectsState.Begin();
				this.objectsState.selector.ForceValue = true;
				this.objectsState.selector.Rotation = MapObjectCreationInfo.DefaultRotation;
				this.objectsState.selector.ForceValue = false;
				this.objectsState.End(true);
			}

			// Token: 0x06001E22 RID: 7714 RVA: 0x000C4758 File Offset: 0x000C3758
			private void OnRotationAlongNormal(MethodArgs methodArgs)
			{
				if (this.objectsState == null)
				{
					return;
				}
				if (this.objectsState.selector.Empty)
				{
					return;
				}
				this.objectsState.Begin();
				this.objectsState.selector.RotateAlongNormal();
				this.objectsState.End(true);
			}

			// Token: 0x06001E23 RID: 7715 RVA: 0x000C47A8 File Offset: 0x000C37A8
			private void OnScaleReset(MethodArgs methodArgs)
			{
				if (this.objectsState == null)
				{
					return;
				}
				if (this.objectsState.selector.Empty)
				{
					return;
				}
				this.objectsState.Begin();
				this.objectsState.selector.ForceValue = true;
				this.objectsState.selector.Scale = MapObjectCreationInfo.DefaultScale;
				this.objectsState.selector.ForceValue = false;
				this.objectsState.End(true);
			}

			// Token: 0x06001E24 RID: 7716 RVA: 0x000C4820 File Offset: 0x000C3820
			private void OnArrangeLinkedItems(MethodArgs methodArgs)
			{
				if (this.objectsState == null)
				{
					return;
				}
				if (this.objectsState.selector.Empty)
				{
					return;
				}
				this.objectsState.Begin();
				this.objectsState.selector.ArrangeLinkedObjects();
				this.objectsState.End(true);
			}

			// Token: 0x06001E25 RID: 7717 RVA: 0x000C4870 File Offset: 0x000C3870
			private void OnRearrangeLinkedItems(MethodArgs methodArgs)
			{
				if (this.objectsState == null)
				{
					return;
				}
				if (this.objectsState.selector.Empty)
				{
					return;
				}
				this.objectsState.Begin();
				this.objectsState.selector.RearrangeLinkedObjects();
				this.objectsState.End(true);
			}

			// Token: 0x06001E26 RID: 7718 RVA: 0x000C48C0 File Offset: 0x000C38C0
			private void OnCreateSpawnTable(MethodArgs methodArgs)
			{
				if (this.objectsState != null)
				{
					this.objectsState.CreateSpawnTable();
				}
			}

			// Token: 0x06001E27 RID: 7719 RVA: 0x000C48D5 File Offset: 0x000C38D5
			private void OnCloneSpawnTable(MethodArgs methodArgs)
			{
				if (this.objectsState != null)
				{
					this.objectsState.CloneSpawnTable();
				}
			}

			// Token: 0x06001E28 RID: 7720 RVA: 0x000C48EA File Offset: 0x000C38EA
			private void OnAddFixedIdleAnimationTuner(MethodArgs methodArgs)
			{
				if (this.objectsState != null)
				{
					this.objectsState.AddFixedIdleAnimationTuner();
				}
			}

			// Token: 0x06001E29 RID: 7721 RVA: 0x000C4900 File Offset: 0x000C3900
			private void OnConvertToClientSpawnPoints(MethodArgs methodArgs)
			{
				if (this.objectsState == null || this.objectsState.selector.MapObjects.Count == 0)
				{
					return;
				}
				List<SpawnPoint> spawnPoints = new List<SpawnPoint>();
				foreach (KeyValuePair<IMapObject, IMapObject> keyValuePair in this.objectsState.selector.MapObjects)
				{
					if (keyValuePair.Key.Type.Type == MapObjectFactory.Type.SpawnPoint)
					{
						spawnPoints.Add(keyValuePair.Key as SpawnPoint);
					}
				}
				IDatabase mainDb = IDatabase.GetMainDatabase();
				int cnt = spawnPoints.Count;
				if (mainDb == null || cnt == 0)
				{
					return;
				}
				ObjectsBrowserForm clientSpawnPointBrowserForm = new ObjectsBrowserForm(new DBItemSource(EditorEnvironment.DataFolder + Constants.ContinentFolder(this.objectsState.continentName) + ClientSpawnPoint.SceneFolder, ClientSpawnPoint.SceneDBType, false, false), this.objectsState.itemDataContainer, EditorEnvironment.EditorFormsFolder + "ClientSpawnPointSceneItemList.xml", EditorEnvironment.EditorFolder + "Filters/ClientSpawnPointSceneFilters.xml");
				DialogResult dialogResult = clientSpawnPointBrowserForm.ShowDialog(this.objectsState.parentForm);
				if (dialogResult != DialogResult.OK || string.IsNullOrEmpty(clientSpawnPointBrowserForm.SelectedObject))
				{
					return;
				}
				this.objectsState.Begin();
				List<ClientSpawnPoint> clientSpawnPoints = new List<ClientSpawnPoint>(cnt);
				foreach (SpawnPoint spawnPoint in spawnPoints)
				{
					if (spawnPoint != null)
					{
						string clientObjKey = null;
						bool mobType = true;
						switch (spawnPoint.SpawnTableType)
						{
						case SpawnTableType.Table:
						{
							clientObjKey = spawnPoint.SceneName;
							string type = mainDb.GetClassTypeNameByFile(clientObjKey);
							if (type == "VisualMob")
							{
								mobType = true;
							}
							else if (DBMethods.TypeIsDerivedFrom(type, "gameMechanics.world.device.DeviceResource"))
							{
								mobType = false;
							}
							else
							{
								clientObjKey = null;
							}
							break;
						}
						case SpawnTableType.SingleMob:
							clientObjKey = spawnPoint.SceneName;
							mobType = true;
							break;
						case SpawnTableType.SingleDevice:
							clientObjKey = spawnPoint.SpawnTable;
							mobType = false;
							break;
						}
						if (!string.IsNullOrEmpty(clientObjKey))
						{
							int id = this.objectsState.mapObjectContainer.AddMapObject(new MapObjectType(MapObjectFactory.Type.ClientSpawnPoint, clientObjKey), false, spawnPoint.Position, spawnPoint.Rotation, spawnPoint.Scale);
							IMapObject newObject;
							if (this.objectsState.mapObjectContainer.TryGetMapObject(id, out newObject))
							{
								ClientSpawnPoint clientSpawnPoint = newObject as ClientSpawnPoint;
								if (clientSpawnPoint != null)
								{
									clientSpawnPoint.Scene = clientSpawnPointBrowserForm.SelectedObject;
									clientSpawnPoint.ScriptID = spawnPoint.ScriptID;
									if (mobType)
									{
										clientSpawnPoint.ClientSpawnPointData = new MobClientSpawnPointData();
									}
									else
									{
										clientSpawnPoint.ClientSpawnPointData = new DeviceClientSpawnPointData();
									}
									clientSpawnPoints.Add(clientSpawnPoint);
								}
							}
							this.objectsState.mapObjectContainer.RemoveMapObject(spawnPoint);
						}
					}
				}
				this.objectsState.End(true);
				this.objectsState.ClearPickers();
				this.objectsState.selector.Clear();
				foreach (ClientSpawnPoint clientSpawnPoint2 in clientSpawnPoints)
				{
					this.objectsState.selector.Add(clientSpawnPoint2);
				}
			}

			// Token: 0x06001E2A RID: 7722 RVA: 0x000C4C68 File Offset: 0x000C3C68
			private void OnDisassembleObject(MethodArgs methodArgs)
			{
				if (this.objectsState != null)
				{
					IDatabase mainDb = IDatabase.GetMainDatabase();
					IEnumerable<IMapObject> selectedObjects = new List<IMapObject>(this.objectsState.selector.MapObjects.Keys);
					this.objectsState.selector.Clear();
					foreach (IMapObject mapObject in selectedObjects)
					{
						StaticObject staticObject = mapObject as StaticObject;
						if (staticObject != null)
						{
							DBID complexObjDBID = mainDb.GetDBIDByName(staticObject.SceneName);
							if (!DBID.IsNullOrEmpty(complexObjDBID))
							{
								IObjMan complexObjMan = mainDb.GetManipulator(complexObjDBID);
								if (complexObjMan != null)
								{
									IObjMan partsMan = complexObjMan.CreateManipulator("parts");
									int partsCount = partsMan.GetArraySize();
									if (partsCount > 0)
									{
										OpenFileDialog dialog = new OpenFileDialog();
										dialog.Title = Strings.STATIC_OBJECT_DISASSEMBLE;
										dialog.SupportMultiDottedExtensions = true;
										dialog.Filter = "(StaticObject).xdb files|*.(StaticObject).xdb|All .xdb files |*.xdb";
										dialog.RestoreDirectory = true;
										dialog.InitialDirectory = complexObjDBID.GetFileFolder(EditorEnvironment.DataFolder);
										if (!Directory.Exists(dialog.InitialDirectory))
										{
											Directory.CreateDirectory(dialog.InitialDirectory);
										}
										if (dialog.ShowDialog(this.objectsState.context.MainForm) == DialogResult.OK)
										{
											DBID separatedObjDBID = mainDb.GetDBIDByName(dialog.FileName);
											IObjMan separatedObjMan = null;
											if (!DBID.IsNullOrEmpty(separatedObjDBID))
											{
												separatedObjMan = mainDb.GetManipulator(separatedObjDBID);
											}
											else
											{
												separatedObjDBID = IDatabase.CreateDBIDByName(dialog.FileName);
												if (!DBID.IsNullOrEmpty(separatedObjDBID))
												{
													separatedObjMan = mainDb.CreateNewObject(StaticObject.DBType);
													mainDb.AddNewObject(separatedObjDBID, separatedObjMan);
													mainDb.CopyObject(separatedObjDBID, complexObjDBID);
													separatedObjMan.SetValue("parts", 0);
												}
											}
											if (separatedObjMan != null)
											{
												this.objectsState.operationContainer.BeginTransaction();
												Position mainObjPos = mapObject.Position;
												Rotation mainObjRot = mapObject.Rotation;
												Quat mainObjRotQuat = new Quat(mainObjRot);
												Scale mainObjScale = mapObject.Scale;
												this.objectsState.MapObjectContainer.RemoveMapObject(mapObject);
												for (int index = 0; index < partsCount; index++)
												{
													partsMan.SetArrayIndex(index);
													string partDBID = SafeObjMan.GetDBID(partsMan, "StaticObjectTemplate");
													Position pos = mainObjPos + mainObjRotQuat.Rotate(SafeObjMan.GetPosition(partsMan, "Position").Vec3);
													Quat rotQuat = mainObjRotQuat * SafeObjMan.GetQuat(partsMan, "Rotation");
													Rotation rot = new Rotation(ref rotQuat);
													float scale = mainObjScale.Ratio * SafeObjMan.GetFloat(partsMan, "Scale");
													this.objectsState.MapObjectContainer.AddMapObject(new MapObjectType(MapObjectFactory.Type.StaticObject, partDBID), false, pos, rot, new Scale(scale));
												}
												int newId = this.objectsState.MapObjectContainer.AddMapObject(new MapObjectType(MapObjectFactory.Type.StaticObject, separatedObjDBID.ToString()), false, mapObject.Position, mapObject.Rotation, mainObjScale);
												this.objectsState.operationContainer.EndTransaction();
												this.objectsState.selector.Add(this.objectsState.MapObjectContainer.MapObjects[newId]);
											}
										}
										Cursor.Current = Cursors.WaitCursor;
										mainDb.SaveChanges();
										Cursor.Current = Cursors.Default;
										break;
									}
								}
							}
						}
					}
				}
			}

			// Token: 0x06001E2B RID: 7723 RVA: 0x000C4FBC File Offset: 0x000C3FBC
			private void OnReplaceStaticObject(MethodArgs methodArgs)
			{
				if (this.objectsState != null)
				{
					IDatabase mainDb = IDatabase.GetMainDatabase();
					IEnumerable<IMapObject> selectedObjects = new List<IMapObject>(this.objectsState.selector.MapObjects.Keys);
					OpenFileDialog dialog = new OpenFileDialog();
					dialog.Title = Strings.STATIC_OBJECT_EXCHANGE;
					dialog.SupportMultiDottedExtensions = true;
					dialog.Filter = "(StaticObject).xdb files|*.(StaticObject).xdb|All .xdb files |*.xdb";
					dialog.RestoreDirectory = true;
					if (!string.IsNullOrEmpty(this.staticObjectExcangeFile))
					{
						dialog.FileName = this.staticObjectExcangeFile;
					}
					else
					{
						dialog.InitialDirectory = EditorEnvironment.DataFolder.Replace('/', '\\');
					}
					if (dialog.ShowDialog(this.objectsState.context.MainForm) == DialogResult.OK)
					{
						this.staticObjectExcangeFile = dialog.FileName;
						DBID newObjectDBID = mainDb.GetDBIDByName(dialog.FileName);
						if (!DBID.IsNullOrEmpty(newObjectDBID))
						{
							this.objectsState.operationContainer.BeginTransaction();
							string newObjectId = newObjectDBID.ToString();
							this.objectsState.selector.Clear();
							foreach (IMapObject mapObject in selectedObjects)
							{
								StaticObject staticObject = mapObject as StaticObject;
								if (staticObject != null)
								{
									this.objectsState.MapObjectContainer.RemoveMapObject(mapObject);
									int newId = this.objectsState.MapObjectContainer.AddMapObject(new MapObjectType(MapObjectFactory.Type.StaticObject, newObjectId), false, mapObject.Position, mapObject.Rotation, mapObject.Scale);
									this.objectsState.selector.Add(this.objectsState.MapObjectContainer.MapObjects[newId]);
								}
							}
							this.objectsState.operationContainer.EndTransaction();
						}
					}
				}
			}

			// Token: 0x040012F6 RID: 4854
			private ObjectsState objectsState;

			// Token: 0x040012F7 RID: 4855
			private bool shiftPressed;

			// Token: 0x040012F8 RID: 4856
			private string staticObjectExcangeFile;
		}
	}
}
