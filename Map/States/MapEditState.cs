using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Windows.Forms;
using Db;
using InputState;
using LauncherTools.InputState;
using MapEditor.Map.Containers;
using MapEditor.Map.Dialogs;
using MapEditor.Map.MapCheckers;
using MapEditor.Map.MapCheckers.SpecificCheckers;
using MapEditor.Map.MapObjects;
using MapEditor.Map.SaveLoad;
using MapEditor.Map.SaveLoad.DataSources;
using MapEditor.Resources.Strings;
using MapEditor.Scene;
using MapInfo;
using Tools.Geometry;
using Tools.InputState;
using Tools.Landscape;
using Tools.MainState;
using Tools.MapObjects;
using Tools.MapSound;
using Tools.MapZoneLights;
using Tools.PenTablet;

namespace MapEditor.Map.States
{
	// Token: 0x0200001E RID: 30
	internal class MapEditState : State
	{
		// Token: 0x0600026C RID: 620 RVA: 0x0001ACF4 File Offset: 0x00019CF4
		private void CreateSaveLoad(OpenMapDialog.Params mapParams)
		{
			this.saveLoad = new SaveLoad(this.map, this.context, EditorEnvironment.EditorFolder + "SaveLoad.txt");
			if (this.map.Data.ContinentType == ContinentType.AstralHub)
			{
				this.saveLoad.AddDataSource(new PreSceneDataSource());
				this.saveLoad.AddDataSource(new AstralSkyDataSource());
				this.saveLoad.AddDataSource(new PostSceneDataSource());
				this.saveLoad.AddDataSource(new AstralServerObjectsDataSource());
				this.saveLoad.AddDataSource(new DBDataSource());
				return;
			}
			if (this.map.Data.ContinentType == ContinentType.Continent)
			{
				this.saveLoad.AddDataSource(new PreSceneDataSource());
				this.terrainDataSource = new TerrainDataSource(mapParams.CreateTerrain, mapParams.CreateBottom);
				this.saveLoad.AddDataSource(this.terrainDataSource);
				this.saveLoad.AddDataSource(new StaticObjectsDataSource(this.map));
				this.saveLoad.AddDataSource(new ClientObjectsDataSource());
				this.saveLoad.AddDataSource(new PostSceneDataSource());
				this.saveLoad.AddDataSource(new MapZonesDataSource());
				this.saveLoad.AddDataSource(new LightsDataSource());
				this.saveLoad.AddDataSource(new SoundDataSource("musics", this.map.MapMusicContainer));
				this.saveLoad.AddDataSource(new SoundDataSource("ambiences", this.map.MapAmbienceContainer));
				this.saveLoad.AddDataSource(new StartPointsDataSource());
				this.saveLoad.AddDataSource(new GraveyardsDataSource());
				this.saveLoad.AddDataSource(new SanctuariesDataSource());
				this.saveLoad.AddDataSource(new ZoneLocatorsDataSource());
				this.saveLoad.AddDataSource(new RoutePointsDataSource());
				this.saveLoad.AddDataSource(new ExtendedSoundsDataSource());
				this.saveLoad.AddDataSource(new SpawnPointsDataSource(this.map));
				this.saveLoad.AddDataSource(new StartPositionDataSource());
				this.saveLoad.AddDataSource(new MinimapDataSource());
				this.saveLoad.AddDataSource(new MapResourceDataSource());
				this.saveLoad.AddDataSource(new DBDataSource());
			}
		}

		// Token: 0x0600026D RID: 621 RVA: 0x0001AF25 File Offset: 0x00019F25
		private void DestroySaveLoad()
		{
			this.saveLoad.Clear();
			this.terrainDataSource = null;
			this.saveLoad = null;
		}

		// Token: 0x0600026E RID: 622 RVA: 0x0001AF40 File Offset: 0x00019F40
		private void CreateCheckers()
		{
			this.mapCheckerContainer = new MapCheckerContainer(this.map);
			if (this.map.Data.ContinentType == ContinentType.AstralHub)
			{
				this.mapCheckerContainer.MapCheckers.Add(new MapObjectCountChecker());
				this.mapCheckerContainer.MapCheckers.Add(new MapObjectTypeChecker());
				this.mapCheckerContainer.MapCheckers.Add(new SpecialMapObjectCountChecker());
				this.mapCheckerContainer.MapCheckers.Add(new SpawnPointTypeChecker());
				this.mapCheckerContainer.MapCheckers.Add(new PositionChecker());
				return;
			}
			if (this.map.Data.ContinentType == ContinentType.Continent)
			{
				this.mapCheckerContainer.MapCheckers.Add(new MapObjectCountChecker());
				this.mapCheckerContainer.MapCheckers.Add(new MapObjectTypeChecker());
				this.mapCheckerContainer.MapCheckers.Add(new SpecialMapObjectCountChecker());
				this.mapCheckerContainer.MapCheckers.Add(new SpawnPointTypeChecker());
				this.mapCheckerContainer.MapCheckers.Add(new PermanentDeviceTypeChecker());
				this.mapCheckerContainer.MapCheckers.Add(new RoutePointTypeChecker());
				this.mapCheckerContainer.MapCheckers.Add(new PatrolNodeTypeChecker());
				this.mapCheckerContainer.MapCheckers.Add(new ScriptIDChecker());
				this.mapCheckerContainer.MapCheckers.Add(new ScanRadiusUsageChecker());
				this.mapCheckerContainer.MapCheckers.Add(new EditorSceneChecker(this.context.EditorScene));
				this.mapCheckerContainer.MapCheckers.Add(new OutOfTerrainChecker(this.context.EditorScene));
				this.mapCheckerContainer.MapCheckers.Add(new PassabilityChecker(this.context.EditorScene));
				this.mapCheckerContainer.MapCheckers.Add(new PatchTerrainBorderChecker(this.context.EditorScene));
				this.mapCheckerContainer.MapCheckers.Add(new PositionChecker());
				this.mapCheckerContainer.MapCheckers.Add(new ShipTourChecker());
				this.mapCheckerContainer.MapCheckers.Add(new GraveyardFactionCheker());
				this.mapCheckerContainer.MapCheckers.Add(new GraveyardVendorCheker());
				this.mapCheckerContainer.MapCheckers.Add(new RabbitChecker(this.context.EditorScene));
				this.mapCheckerContainer.MapCheckers.Add(new LinksCheker());
			}
		}

		// Token: 0x0600026F RID: 623 RVA: 0x0001B1B7 File Offset: 0x0001A1B7
		private void DestroyCheckers()
		{
			this.mapCheckerContainer.MapCheckers.Clear();
			this.mapCheckerContainer = null;
		}

		// Token: 0x06000270 RID: 624 RVA: 0x0001B1D0 File Offset: 0x0001A1D0
		private void CreateStates()
		{
			this.multiState = new MultiState("MapEditMultiState");
			if (this.map.Data.ContinentType == ContinentType.AstralHub)
			{
				this.objectsState = new ObjectsState(this.context, this.context.StateContainer, this.map.MapEditorMapObjectContainer, this.map.OperationContainer, this.map.Groups, this.context.Tool, this.context.Tool, this.context.ItemDataContainer, this.map.Data.MapResourceName, this.map.Data.ContinentName, this.map.Data.ContinentType, this.mapEditorScene, this.context.PropertyControl, this.context.Statusbar, this.context.MultiObjectBrowser, this.context.MainForm, "MapEditor_ObjectSelectorData.xml");
				this.multiState.AddState(this.objectsState);
				this.multiState.ActiveStateLabel = this.objectsState.Label;
				return;
			}
			if (this.map.Data.ContinentType == ContinentType.Continent)
			{
				this.objectsState = new ObjectsState(this.context, this.context.StateContainer, this.map.MapEditorMapObjectContainer, this.map.OperationContainer, this.map.Groups, this.context.Tool, this.context.Tool, this.context.ItemDataContainer, this.map.Data.MapResourceName, this.map.Data.ContinentName, this.map.Data.ContinentType, this.mapEditorScene, this.context.PropertyControl, this.context.Statusbar, this.context.MultiObjectBrowser, this.context.MainForm, "MapEditor_ObjectSelectorData.xml");
				if (this.objectsState.MapObjectSelector != null)
				{
					this.objectsState.MapObjectSelector.LinkCheckers.Add(new SpawnPointLinkChecker());
					this.objectsState.MapObjectSelector.LinkCheckers.Add(new RoutePointLinkChecker());
					this.objectsState.MapObjectSelector.LinkCheckers.Add(new ExtendedSoundLinkChecker());
					this.objectsState.MapObjectSelector.LinkCheckers.Add(new ClientPatrolNodeLinkChecker());
					this.objectsState.MapObjectSelector.LinkCheckers.Add(new GraveyardLinkChecker());
					this.objectsState.MapObjectSelector.LinkCheckers.Add(new SanctuaryLinkChecker());
				}
				this.landscapeState = new LandscapeState(this.context.StateContainer, this.context.MainState, this.context.ItemDataContainer, this.map.MapEditorLandscapeToolContainer, this.map.OperationContainer, this.map.SquareContainer, this.map.PolygonContainer, this.map.StripeContainer, this.map.MapEditorMapObjectContainer, this.map.MapEditorMapObjectContainer, this.context.Tool, this.map.Data.ContinentName, new Vec3((double)(this.map.Data.MinXMinYPatchCoords.X * Constants.PatchSize), (double)(this.map.Data.MinXMinYPatchCoords.Y * Constants.PatchSize), 0.0), this.context.EditorScene, this.context.EditorSceneViewID, this.context.Statusbar, this.context.MainForm, this.penTablet);
				this.zonesState = new MapZonesState(this);
				this.lightsState = new LightsState(this);
				this.soundState = new SoundState(this);
				this.distanceState = new DistanceState(this.map.PolygonContainer, this.context.StateContainer, this.multiState, this.map.MapEditorMapObjectContainer, this.context.EditorScene, this.context.EditorSceneViewID, this.context.Statusbar);
				this.multiState.AddState(this.objectsState);
				this.multiState.AddState(this.landscapeState);
				this.multiState.AddState(this.zonesState);
				this.multiState.AddState(this.lightsState);
				this.multiState.AddState(this.soundState);
				this.multiState.AddState(this.distanceState);
				this.multiState.ActiveStateLabel = this.objectsState.Label;
			}
		}

		// Token: 0x06000271 RID: 625 RVA: 0x0001B67C File Offset: 0x0001A67C
		private void DestroyStates()
		{
			if (this.landscapeState != null)
			{
				this.landscapeState.Destroy();
				this.landscapeState = null;
			}
			if (this.objectsState != null)
			{
				this.context.PropertyControl.SelectedObject = null;
				this.objectsState.Destroy();
				this.objectsState = null;
			}
			if (this.zonesState != null)
			{
				this.zonesState.Destroy();
				this.zonesState = null;
			}
			if (this.lightsState != null)
			{
				this.lightsState.Destroy();
				this.lightsState = null;
			}
			if (this.soundState != null)
			{
				this.soundState.Destroy();
				this.soundState = null;
			}
			if (this.distanceState != null)
			{
				this.distanceState.Destroy();
				this.distanceState = null;
			}
			this.multiState = null;
		}

		// Token: 0x06000272 RID: 626 RVA: 0x0001B73D File Offset: 0x0001A73D
		private void UpdateMainState()
		{
			this.multiState.ActiveStateIndex = this.context.MainState.ActiveState;
		}

		// Token: 0x06000273 RID: 627 RVA: 0x0001B75A File Offset: 0x0001A75A
		private void UpdateSaveButton()
		{
			base.SetMethodParams("save_map", true, this.Modified, false, false);
		}

		// Token: 0x06000274 RID: 628 RVA: 0x0001B770 File Offset: 0x0001A770
		private void ResetCamera(MethodArgs methodArgs)
		{
			this.CenterCamera();
		}

		// Token: 0x06000275 RID: 629 RVA: 0x0001B778 File Offset: 0x0001A778
		private void MoveCamera(MethodArgs methodArgs)
		{
			MoveCameraForm moveCameraForm = new MoveCameraForm(true);
			if (moveCameraForm.ShowDialog() == DialogResult.OK && moveCameraForm.CoordsValid)
			{
				CameraPlacement cameraPlacement;
				this.context.EditorScene.GetPlacement(this.context.EditorSceneViewID, out cameraPlacement);
				Position positon = moveCameraForm.CameraPosition;
				if (moveCameraForm.OnlyXYCoordsValid)
				{
					positon.Z = cameraPlacement.Position.Z;
				}
				cameraPlacement.Position = positon;
				if (moveCameraForm.CoordsRelative)
				{
					cameraPlacement.Position += new Position((double)(this.map.Data.MinXMinYPatchCoords.X * Constants.PatchSize), (double)(this.map.Data.MinXMinYPatchCoords.Y * Constants.PatchSize), 0.0);
				}
				this.context.EditorScene.SetPlacement(this.context.EditorSceneViewID, ref cameraPlacement);
			}
		}

		// Token: 0x06000276 RID: 630 RVA: 0x0001B874 File Offset: 0x0001A874
		private int GetAvarageHeight(int index)
		{
			float height = 0f;
			int sumCount = 0;
			for (int i = 0; i < this.map.Data.MapSize.X * Constants.PatchSize; i += 8)
			{
				for (int j = 0; j < this.map.Data.MapSize.Y * Constants.PatchSize; j += 8)
				{
					sumCount++;
					Tools.Geometry.Point point = new Tools.Geometry.Point(i, j);
					height += this.context.EditorScene.GetTerrainHeight(this.context.EditorSceneViewID, index, ref point);
				}
			}
			if (sumCount == 0)
			{
				sumCount = 1;
			}
			return (int)(height / (float)sumCount / 32f) * 32;
		}

		// Token: 0x06000277 RID: 631 RVA: 0x0001B928 File Offset: 0x0001A928
		public void SetCameraView(Position position, Rotation rotation)
		{
			CameraPlacement cameraPlacement;
			this.context.EditorScene.GetPlacement(this.context.EditorSceneViewID, out cameraPlacement);
			cameraPlacement.Position = position;
			cameraPlacement.Rotation = rotation;
			this.context.EditorScene.SetPlacement(this.context.EditorSceneViewID, ref cameraPlacement);
		}

		// Token: 0x06000278 RID: 632 RVA: 0x0001B980 File Offset: 0x0001A980
		public void CenterCamera()
		{
			Position cameraAnchor = Constants.PatchesCenter(new Rect(this.map.Data.MinXMinYPatchCoords, this.map.Data.MapSize.X, this.map.Data.MapSize.Y));
			this.SetCameraOverPosition(ref cameraAnchor);
		}

		// Token: 0x06000279 RID: 633 RVA: 0x0001B9E0 File Offset: 0x0001A9E0
		public void SetCameraOverPosition(ref Position cameraAnchor)
		{
			Tools.Geometry.Point mapCenter = new Tools.Geometry.Point((int)(cameraAnchor.X - (double)(Constants.PatchSize * this.map.Data.MinXMinYPatchCoords.X)), (int)(cameraAnchor.Y - (double)(Constants.PatchSize * this.map.Data.MinXMinYPatchCoords.Y)));
			cameraAnchor.Z = (double)this.context.EditorScene.GetTerrainHeight(this.context.EditorSceneViewID, 0, ref mapCenter);
			int averageHeight = this.GetAvarageHeight(0);
			this.context.EditorScene.SetCameraBounds(this.context.EditorSceneViewID, (int)(cameraAnchor.X - 256.0 * ((double)this.map.Data.MapSize.X * 0.5 + 1.0)), (int)(cameraAnchor.X + 256.0 * ((double)this.map.Data.MapSize.X * 0.5 + 1.0)), (int)(cameraAnchor.Y - 256.0 * ((double)this.map.Data.MapSize.Y * 0.5 + 1.0)), (int)(cameraAnchor.Y + 256.0 * ((double)this.map.Data.MapSize.Y * 0.5 + 1.0)), averageHeight - 512, averageHeight + 512);
			CameraPlacement cameraPlacement;
			this.context.EditorScene.GetPlacement(this.context.EditorSceneViewID, out cameraPlacement);
			cameraPlacement.Position = new Position(cameraAnchor.X, cameraAnchor.Y, cameraAnchor.Z + 64.0);
			cameraPlacement.Rotation = new Rotation(1.5707964f, 0.7853982f, 0f);
			this.context.EditorScene.SetPlacement(this.context.EditorSceneViewID, ref cameraPlacement);
		}

		// Token: 0x0600027A RID: 634 RVA: 0x0001BC1C File Offset: 0x0001AC1C
		public void FillHeightsFromHeightmap(int terrainIndex, double blackHeight, double whiteHeight, string heightmapFileName)
		{
			if (this.landscapeState != null)
			{
				Cursor previousCursor = Cursor.Current;
				Cursor.Current = Cursors.WaitCursor;
				this.Context.OperationContainer.BeginTransaction();
				this.Context.EditorScene.FillHeightsFromHeightmap(terrainIndex, blackHeight, whiteHeight, heightmapFileName);
				this.map.MapEditorMapObjectContainer.Update(true, -1.0);
				this.map.LandscapeToolOperationContainer.CreateApplyLandscapeToolOperation();
				base.Container.Invoke("_minimap_repaint", default(MethodArgs));
				this.Context.OperationContainer.EndTransaction();
				Cursor.Current = previousCursor;
			}
		}

		// Token: 0x0600027B RID: 635 RVA: 0x0001BCC4 File Offset: 0x0001ACC4
		private void UpdateTitle()
		{
			this.context.Title.ObjectName = this.map.Data.GetEditorMapCaption();
			if (this.map.Data.ContinentType == ContinentType.AstralHub)
			{
				this.context.Title.AdditionalText = string.Format("({0})", Strings.ASTRAL_HUB);
				return;
			}
			if (this.map.Data.ContinentType == ContinentType.Continent)
			{
				this.context.Title.AdditionalText = string.Format("({0:000}_{1:000}) [{2}x{3}]", new object[]
				{
					this.map.Data.MinXMinYPatchCoords.X,
					this.map.Data.MinXMinYPatchCoords.Y,
					this.map.Data.MapSize.X,
					this.map.Data.MapSize.Y
				});
			}
		}

		// Token: 0x0600027C RID: 636 RVA: 0x0001BDE0 File Offset: 0x0001ADE0
		private void OnStaticObjectChanged(MethodArgs args)
		{
			string dbidKey = args.sender as string;
			if (!string.IsNullOrEmpty(dbidKey))
			{
				foreach (IMapObject mapObject in this.map.MapEditorMapObjectContainer.StaticObjectContainer.MapObjects.Values)
				{
					if (mapObject.SceneName == dbidKey)
					{
						this.mapEditorScene.MapSceneObjects.RecreateMapObject(mapObject, false);
					}
				}
			}
		}

		// Token: 0x0600027D RID: 637 RVA: 0x0001BE78 File Offset: 0x0001AE78
		private void OnDbObjectChanged(DBID dbid)
		{
			this.Modified = true;
			if (this.map != null && this.mainDb != null)
			{
				string type = this.mainDb.GetClassTypeName(dbid);
				List<SpawnPoint> spawnPoints = new List<SpawnPoint>();
				if (type == "VisualMob")
				{
					string visMobKey = dbid.ToString();
					if (!string.IsNullOrEmpty(visMobKey))
					{
						List<int> sceneObjectIdList = new List<int>();
						foreach (IMapObject mapObject in this.map.MapEditorMapObjectContainer.SpawnPointContainer.MapObjects.Values)
						{
							if (string.Equals(mapObject.SceneName, visMobKey))
							{
								sceneObjectIdList.Add(this.mapEditorScene.MapSceneObjects.MapObjectIDToEditorSceneObjectID(mapObject.ID));
								spawnPoints.Add(mapObject as SpawnPoint);
							}
						}
						foreach (IMapObject mapObject2 in this.map.MapEditorMapObjectContainer.PermanentDeviceContainer.MapObjects.Values)
						{
							if (string.Equals(mapObject2.SceneName, visMobKey))
							{
								sceneObjectIdList.Add(this.mapEditorScene.MapSceneObjects.MapObjectIDToEditorSceneObjectID(mapObject2.ID));
							}
						}
						foreach (IMapObject mapObject3 in this.map.MapEditorMapObjectContainer.ClientSpawnPointContainer.MapObjects.Values)
						{
							if (string.Equals(mapObject3.SceneName, visMobKey))
							{
								sceneObjectIdList.Add(this.mapEditorScene.MapSceneObjects.MapObjectIDToEditorSceneObjectID(mapObject3.ID));
							}
						}
						foreach (IMapObject mapObject4 in this.map.MapEditorMapObjectContainer.PlayerRespawnPlaceContainer.MapObjects.Values)
						{
							if (string.Equals(mapObject4.SceneName, visMobKey))
							{
								sceneObjectIdList.Add(this.mapEditorScene.MapSceneObjects.MapObjectIDToEditorSceneObjectID(mapObject4.ID));
							}
						}
						if (sceneObjectIdList.Count > 0)
						{
							this.mapEditorScene.EditorScene.SetObjectVisMob(sceneObjectIdList.ToArray(), visMobKey);
						}
					}
				}
				else
				{
					if (type == "gameMechanics.world.mob.MobWorld" || type == "gameMechanics.map.spawn.SpawnTable")
					{
						string dbidKey = dbid.ToString();
						using (Dictionary<int, IMapObject>.ValueCollection.Enumerator enumerator5 = this.map.MapEditorMapObjectContainer.SpawnPointContainer.MapObjects.Values.GetEnumerator())
						{
							while (enumerator5.MoveNext())
							{
								IMapObject mapObject5 = enumerator5.Current;
								SpawnPoint spawnPoint = (SpawnPoint)mapObject5;
								if (spawnPoint.SpawnTable == dbidKey)
								{
									spawnPoints.Add(spawnPoint);
								}
							}
							goto IL_372;
						}
					}
					if (PermanentDeviceContainer.IsDeviceType(type))
					{
						string dbidKey2 = dbid.ToString();
						using (Dictionary<int, IMapObject>.ValueCollection.Enumerator enumerator6 = this.map.MapEditorMapObjectContainer.SpawnPointContainer.MapObjects.Values.GetEnumerator())
						{
							while (enumerator6.MoveNext())
							{
								IMapObject mapObject6 = enumerator6.Current;
								SpawnPoint spawnPoint2 = (SpawnPoint)mapObject6;
								if (spawnPoint2.SpawnTable == dbidKey2)
								{
									this.mapEditorScene.MapSceneObjects.RecreateMapObject(spawnPoint2, false);
								}
							}
							goto IL_372;
						}
					}
					if (type == "SkeletalAnimation")
					{
						this.mapEditorScene.EditorScene.GameRenderSceneClearCaches();
					}
				}
				IL_372:
				foreach (SpawnPoint spawnPoint3 in spawnPoints)
				{
					if (spawnPoint3 != null)
					{
						spawnPoint3.CheckVisualData();
					}
				}
			}
		}

		// Token: 0x17000026 RID: 38
		// (get) Token: 0x0600027E RID: 638 RVA: 0x0001C288 File Offset: 0x0001B288
		// (set) Token: 0x0600027F RID: 639 RVA: 0x0001C290 File Offset: 0x0001B290
		public bool Modified
		{
			get
			{
				return this.modified;
			}
			set
			{
				this.modified = value;
				if (this.context != null)
				{
					this.context.Title.Modified = this.modified;
				}
				this.UpdateSaveButton();
			}
		}

		// Token: 0x06000280 RID: 640 RVA: 0x0001C2C0 File Offset: 0x0001B2C0
		public bool Save(bool askForSaveChanges)
		{
			if (this.Modified)
			{
				DialogResult dialogResult = DialogResult.Yes;
				if (askForSaveChanges)
				{
					dialogResult = MessageBox.Show(Strings.MAP_SAVE_CHANGES_MESSAGE, Strings.MAP_SAVE_CHANGES_TITLE, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Exclamation);
					if (dialogResult == DialogResult.Cancel)
					{
						return false;
					}
				}
				if (dialogResult == DialogResult.Yes)
				{
					this.saveLoad.Save();
					this.context.StartSaveNotificationTimer();
				}
				this.Modified = false;
				return true;
			}
			return true;
		}

		// Token: 0x06000281 RID: 641 RVA: 0x0001C318 File Offset: 0x0001B318
		private void OnEnterState(IState mapEditState)
		{
			if (this.context != null)
			{
				this.context.AddCloseFormEvent(new MainForm.Context.CloseFormEvent(this.OnCloseMainForm));
				this.UpdateSaveButton();
				if (base.Container != null)
				{
					base.Container.BindState(this.multiState);
					base.Container.Invoke("_enter_map_edit_state", default(MethodArgs));
					this.UpdateMainState();
				}
				this.dbEventsGenerator.DBObjectChanged += this.OnDbObjectChanged;
				if (this.penTablet != null)
				{
					this.penTablet.Enable(true);
				}
			}
		}

		// Token: 0x06000282 RID: 642 RVA: 0x0001C3B0 File Offset: 0x0001B3B0
		private void OnLeaveState(IState mapEditState)
		{
			if (this.context != null)
			{
				if (this.penTablet != null)
				{
					this.penTablet.Enable(false);
				}
				if (base.Container != null)
				{
					base.Container.UnbindState(this.multiState);
					base.Container.Invoke("_leave_map_edit_state", default(MethodArgs));
				}
				this.context.RemoveCloseFormEvent(new MainForm.Context.CloseFormEvent(this.OnCloseMainForm));
				this.Save(true);
				this.context.EditorScene.Clear();
				if (this.map.Data.ContinentType == ContinentType.Continent)
				{
					this.context.EditorScene.DestroyTerrain();
				}
				else if (this.map.Data.ContinentType == ContinentType.AstralHub)
				{
					this.context.EditorScene.UnloadAstral();
				}
				this.Modified = false;
				this.context.Title.ObjectName = string.Empty;
				this.context.Title.AdditionalText = string.Empty;
				this.UpdateSaveButton();
				this.dbEventsGenerator.DBObjectChanged -= this.OnDbObjectChanged;
			}
		}

		// Token: 0x06000283 RID: 643 RVA: 0x0001C4D3 File Offset: 0x0001B4D3
		private void OnCloseMainForm(FormClosingEventArgs e)
		{
			if (!this.Save(true))
			{
				e.Cancel = true;
			}
		}

		// Token: 0x06000284 RID: 644 RVA: 0x0001C4E5 File Offset: 0x0001B4E5
		private void OnActiveStateChanged(MainState mainState, ref int oldValue, ref int newValue)
		{
			this.UpdateMainState();
		}

		// Token: 0x06000285 RID: 645 RVA: 0x0001C4F0 File Offset: 0x0001B4F0
		private void OnToggleMeasureDistance(MethodArgs methodArgs)
		{
			if (this.multiState != null && this.distanceState != null)
			{
				if (this.multiState.ActiveStateLabel == this.distanceState.Label)
				{
					this.distanceState.End();
					return;
				}
				this.distanceState.PreviousState = this.multiState.ActiveStateLabel;
				this.distanceState.Begin();
			}
		}

		// Token: 0x06000286 RID: 646 RVA: 0x0001C558 File Offset: 0x0001B558
		private void OnMouseEnter(MethodArgs methodArgs)
		{
			if (this.context.MainForm != null && this.context.EditorSceneParams.AutoFocus)
			{
				this.context.MainForm.Focus();
				this.context.LastActiveForm = this.context.MainForm;
			}
		}

		// Token: 0x06000287 RID: 647 RVA: 0x0001C5AB File Offset: 0x0001B5AB
		private void OnSaveMap(MethodArgs methodArgs)
		{
			this.context.ItemDataContainer.Trace();
			this.Save(false);
			this.context.ItemDataContainer.Trace();
			this.UpdateSaveButton();
		}

		// Token: 0x06000288 RID: 648 RVA: 0x0001C5DC File Offset: 0x0001B5DC
		private void OnSaveMapBeforeStartGame(MethodArgs methodArgs)
		{
			SaveLoad saveMapBeforeStartGameSaveLoad = new SaveLoad(this.map, this.context, EditorEnvironment.EditorFolder + "SaveLoad.txt");
			saveMapBeforeStartGameSaveLoad.AddDataSource(new PreSceneDataSource());
			saveMapBeforeStartGameSaveLoad.AddDataSource(new StaticObjectsDataSource(this.map));
			saveMapBeforeStartGameSaveLoad.AddDataSource(new PostSceneDataSource());
			saveMapBeforeStartGameSaveLoad.AddDataSource(new MapZonesDataSource());
			saveMapBeforeStartGameSaveLoad.AddDataSource(new StartPositionDataSource());
			saveMapBeforeStartGameSaveLoad.Save();
			saveMapBeforeStartGameSaveLoad.Clear();
		}

		// Token: 0x06000289 RID: 649 RVA: 0x0001C654 File Offset: 0x0001B654
		private void CreateGenerateShadowsData(bool local)
		{
			if (this.objectsState == null || this.objectsState.MapObjectSelector == null || this.mapEditorScene == null)
			{
				return;
			}
			SaveLoad saveMapBeforeGenerateShadows = new SaveLoad(this.map, this.context, EditorEnvironment.EditorFolder + "SaveLoad.txt");
			saveMapBeforeGenerateShadows.AddDataSource(new GenerateShadowsDataSource(this.objectsState.MapObjectSelector, this.mapEditorScene, local));
			saveMapBeforeGenerateShadows.Save();
			saveMapBeforeGenerateShadows.Clear();
		}

		// Token: 0x0600028A RID: 650 RVA: 0x0001C6CC File Offset: 0x0001B6CC
		private void OnGenerateShadows(MethodArgs methodArgs)
		{
			if (this.mapEditorScene == null || this.objectsState == null || this.objectsState.MapObjectSelector == null)
			{
				return;
			}
			if (MessageBox.Show(Strings.GENERATE_SHADOWS_QUESTION, string.Empty, MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
			{
				Cursor.Current = Cursors.WaitCursor;
				this.CreateGenerateShadowsData(false);
				Cursor.Current = Cursors.Default;
			}
		}

		// Token: 0x0600028B RID: 651 RVA: 0x0001C728 File Offset: 0x0001B728
		private void OnGenerateShadowsLocal(MethodArgs methodArgs)
		{
			if (this.mapEditorScene == null || this.objectsState == null || this.objectsState.MapObjectSelector == null || this.objectsState.MapObjectSelector.MapObjects.Count == 0)
			{
				return;
			}
			Cursor.Current = Cursors.WaitCursor;
			this.CreateGenerateShadowsData(true);
			Cursor.Current = Cursors.Default;
		}

		// Token: 0x0600028C RID: 652 RVA: 0x0001C788 File Offset: 0x0001B788
		private void OnKeyUp(MethodArgs args)
		{
			if (args.sender == this.Context.MainForm)
			{
				KeyEventArgs keyEventArgs = args.eventArgs as KeyEventArgs;
				if (keyEventArgs != null && keyEventArgs.KeyCode == Keys.Snapshot)
				{
					Rectangle rect = this.Context.MainForm.ClientRectangle;
					Bitmap bmp = new Bitmap(rect.Width, rect.Height);
					Graphics gr = Graphics.FromImage(bmp);
					gr.CopyFromScreen(this.Context.MainForm.Left + rect.Left, this.Context.MainForm.Top + rect.Top, 0, 0, rect.Size);
					string dirName = EditorEnvironment.WorkingFolder + "Personal\\Screenshots\\";
					if (!Directory.Exists(dirName))
					{
						Directory.CreateDirectory(dirName);
					}
					string fileNameFormat = dirName + "EditorScreenshot_{0}.jpg";
					string fileName = null;
					for (int index = 1; index < 100; index++)
					{
						fileName = string.Format(fileNameFormat, index);
						if (!File.Exists(fileName))
						{
							break;
						}
						fileName = null;
					}
					if (string.IsNullOrEmpty(fileName))
					{
						fileName = string.Format(fileNameFormat, 1);
					}
					try
					{
						bmp.Save(fileName, ImageFormat.Jpeg);
					}
					catch (Exception e)
					{
						Console.WriteLine(e);
					}
					gr.Dispose();
				}
			}
		}

		// Token: 0x0600028D RID: 653 RVA: 0x0001C8E0 File Offset: 0x0001B8E0
		private void OnTerrainRegionContainerModified(TerrainRegionContainer _terrainRegionContainer)
		{
			if (this.terrainDataSource != null)
			{
				this.terrainDataSource.Modified = true;
			}
			this.Modified = true;
			this.UpdateSaveButton();
		}

		// Token: 0x0600028E RID: 654 RVA: 0x0001C903 File Offset: 0x0001B903
		private void OnMapObjectContainerModified(MapObjectContainer _mapObjectContainer)
		{
			this.Modified = true;
			this.UpdateSaveButton();
		}

		// Token: 0x0600028F RID: 655 RVA: 0x0001C912 File Offset: 0x0001B912
		private void OnLandscapeToolContainerModified(LandscapeToolContainer landscapeToolContainer)
		{
			if (this.terrainDataSource != null)
			{
				this.terrainDataSource.Modified = true;
			}
			this.Modified = true;
			this.UpdateSaveButton();
		}

		// Token: 0x06000290 RID: 656 RVA: 0x0001C935 File Offset: 0x0001B935
		private void OnMapZoneContainerModified(MapZoneContainer mapZonesContainer)
		{
			this.Modified = true;
			this.UpdateSaveButton();
		}

		// Token: 0x06000291 RID: 657 RVA: 0x0001C944 File Offset: 0x0001B944
		private void OnZoneLightContainerModified(ZoneLightContainer zoneLightContainer)
		{
			this.Modified = true;
			this.UpdateSaveButton();
		}

		// Token: 0x06000292 RID: 658 RVA: 0x0001C953 File Offset: 0x0001B953
		private void OnSoundContainerModified(MapSoundContainer mapSoundContainer)
		{
			this.Modified = true;
			this.UpdateSaveButton();
		}

		// Token: 0x06000293 RID: 659 RVA: 0x0001C962 File Offset: 0x0001B962
		private void OnMapProperties(MethodArgs methodArgs)
		{
			if (this.context != null)
			{
				this.context.SelectExistingObjectInDatabaseEditor(this.map.Data.MapResourceName);
			}
		}

		// Token: 0x06000294 RID: 660 RVA: 0x0001C987 File Offset: 0x0001B987
		private void OnBlockEditingChanged(EditorSceneParams param)
		{
			if (param.BlockEditing)
			{
				this.map.EditingBlocker.StartBlocking();
				return;
			}
			this.map.EditingBlocker.EndBlocking();
		}

		// Token: 0x17000027 RID: 39
		// (get) Token: 0x06000295 RID: 661 RVA: 0x0001C9B2 File Offset: 0x0001B9B2
		public MainForm.Context Context
		{
			get
			{
				return this.context;
			}
		}

		// Token: 0x17000028 RID: 40
		// (get) Token: 0x06000296 RID: 662 RVA: 0x0001C9BA File Offset: 0x0001B9BA
		public MapEditorMap Map
		{
			get
			{
				return this.map;
			}
		}

		// Token: 0x17000029 RID: 41
		// (get) Token: 0x06000297 RID: 663 RVA: 0x0001C9C2 File Offset: 0x0001B9C2
		public MultiState MultiState
		{
			get
			{
				return this.multiState;
			}
		}

		// Token: 0x1700002A RID: 42
		// (get) Token: 0x06000298 RID: 664 RVA: 0x0001C9CA File Offset: 0x0001B9CA
		public MapCheckerContainer MapCheckerContainer
		{
			get
			{
				return this.mapCheckerContainer;
			}
		}

		// Token: 0x1700002B RID: 43
		// (get) Token: 0x06000299 RID: 665 RVA: 0x0001C9D2 File Offset: 0x0001B9D2
		public PenTablet PenTablet
		{
			get
			{
				return this.penTablet;
			}
		}

		// Token: 0x0600029A RID: 666 RVA: 0x0001C9DC File Offset: 0x0001B9DC
		public MapEditState(MainForm.Context _context, OpenMapDialog.Params mapParams) : base("MapEditorMapEditState")
		{
			if (_context != null)
			{
				this.context = _context;
				this.EnterState = (State.ActivateEvent)Delegate.Combine(this.EnterState, new State.ActivateEvent(this.OnEnterState));
				this.LeaveState = (State.ActivateEvent)Delegate.Combine(this.LeaveState, new State.ActivateEvent(this.OnLeaveState));
				Tools.Geometry.Point patch = mapParams.GetPatch();
				Position startCameraPosition = mapParams.CameraPosition;
				this.map = new MapEditorMap(this.context, patch.X, patch.Y, mapParams.Name, mapParams.ContinentName, mapParams.Type, ref startCameraPosition, mapParams.MapSize);
				this.mapEditorScene = new MapEditorScene(this.context.EditorScene, this.context.EditorSceneViewID, this.context.StateContainer, EditorEnvironment.EditorFormsFolder + "MapEditorMapScene.xml");
				this.mapEditorScene.Bind(this.map.Data.ContinentType, this.map.Data.ScaleRatio, this.map.MapEditorMapObjectContainer, this.map.TerrainRegionContainer, this.map.LandscapeChangesContainer, this.map.MapZoneContainer, this.map.ZoneLightContainer, this.map.MapMusicContainer, this.map.MapAmbienceContainer, this.map.PolygonContainer, this.map.SquareContainer, this.map.StripeContainer);
				this.map.TerrainRegionContainer.Modified += this.OnTerrainRegionContainerModified;
				this.map.MapEditorLandscapeToolContainer.Modified += this.OnLandscapeToolContainerModified;
				this.map.MapEditorMapObjectContainer.Modified += this.OnMapObjectContainerModified;
				this.map.MapZoneContainer.Modified += this.OnMapZoneContainerModified;
				this.map.ZoneLightContainer.Modified += this.OnZoneLightContainerModified;
				this.map.MapMusicContainer.Modified += this.OnSoundContainerModified;
				this.map.MapAmbienceContainer.Modified += this.OnSoundContainerModified;
				this.context.MainState.ActiveStateChanged += this.OnActiveStateChanged;
				base.AddMethod("toggle_measure_distance", new Method(this.OnToggleMeasureDistance));
				base.AddMethod("mouse_enter", new Method(this.OnMouseEnter));
				base.AddMethod("save_map", new Method(this.OnSaveMap));
				base.AddMethod("_save_map_before_start_game", new Method(this.OnSaveMapBeforeStartGame));
				base.AddMethod("_object_editor_static_object_changed", new Method(this.OnStaticObjectChanged));
				base.AddMethod("camera_reset", new Method(this.ResetCamera));
				base.AddMethod("camera_move", new Method(this.MoveCamera));
				base.AddMethod("map_properties", new Method(this.OnMapProperties));
				base.AddMethod("generate_shadows", new Method(this.OnGenerateShadows));
				base.AddMethod("generate_shadows_local", new Method(this.OnGenerateShadowsLocal));
				base.AddMethod("key_up", new Method(this.OnKeyUp));
				this.context.OperationContainer.Clear();
				this.Modified = false;
				this.context.OperationContainer.Enable = false;
				this.penTablet = new PenTablet(_context.MainForm, false);
				this.CreateStates();
				this.CreateCheckers();
				this.context.Bind(this.map, this.objectsState.MapObjectSelector);
				this.CreateSaveLoad(mapParams);
				bool somethingCreated;
				this.saveLoad.Load(out somethingCreated);
				this.context.PostBind(this.map, this.objectsState.MapObjectSelector, this.mapEditorScene, this.mapCheckerContainer);
				startCameraPosition = this.map.Data.StartCameraPosition;
				this.SetCameraOverPosition(ref startCameraPosition);
				this.UpdateTitle();
				this.context.OperationContainer.Enable = true;
				this.Modified = somethingCreated;
				this.mainDb = IDatabase.GetMainDatabase();
				this.dbEventsGenerator = new DbEventsGenerator(this.mainDb);
				if (this.context.EditorSceneParams.BlockEditing)
				{
					this.map.EditingBlocker.StartBlocking();
				}
				EditorSceneParams editorSceneParams = this.context.EditorSceneParams;
				editorSceneParams.BlockEditingChanged = (EditorSceneParams.ParamsEvent)Delegate.Combine(editorSceneParams.BlockEditingChanged, new EditorSceneParams.ParamsEvent(this.OnBlockEditingChanged));
			}
		}

		// Token: 0x0600029B RID: 667 RVA: 0x0001CE80 File Offset: 0x0001BE80
		public void Destroy()
		{
			if (this.context != null)
			{
				EditorSceneParams editorSceneParams = this.context.EditorSceneParams;
				editorSceneParams.BlockEditingChanged = (EditorSceneParams.ParamsEvent)Delegate.Remove(editorSceneParams.BlockEditingChanged, new EditorSceneParams.ParamsEvent(this.OnBlockEditingChanged));
				if (this.context.EditorSceneParams.BlockEditing)
				{
					this.map.EditingBlocker.EndBlocking();
				}
				base.RemoveMethod("toggle_measure_distance");
				base.RemoveMethod("mouse_enter");
				base.RemoveMethod("save_map");
				base.RemoveMethod("_save_map_before_start_game");
				base.RemoveMethod("_object_editor_static_object_changed");
				base.RemoveMethod("camera_reset");
				base.RemoveMethod("camera_move");
				base.RemoveMethod("map_properties");
				base.RemoveMethod("generate_shadows");
				base.RemoveMethod("generate_shadows_local");
				base.RemoveMethod("key_up");
				this.context.MainState.ActiveStateChanged -= this.OnActiveStateChanged;
				this.context.Unbind();
				this.DestroySaveLoad();
				this.DestroyCheckers();
				this.DestroyStates();
				if (this.penTablet != null)
				{
					this.penTablet.Destroy();
					this.penTablet = null;
				}
				this.map.TerrainRegionContainer.Modified -= this.OnTerrainRegionContainerModified;
				this.map.MapEditorLandscapeToolContainer.Modified -= this.OnLandscapeToolContainerModified;
				this.map.MapEditorMapObjectContainer.Modified -= this.OnMapObjectContainerModified;
				this.map.MapZoneContainer.Modified -= this.OnMapZoneContainerModified;
				this.map.ZoneLightContainer.Modified -= this.OnZoneLightContainerModified;
				this.map.MapMusicContainer.Modified -= this.OnSoundContainerModified;
				this.map.MapAmbienceContainer.Modified -= this.OnSoundContainerModified;
				this.mapEditorScene.Unbind();
				this.mapEditorScene.Destroy();
				this.mapEditorScene = null;
				this.map.Destroy();
				this.map = null;
				this.EnterState = (State.ActivateEvent)Delegate.Remove(this.EnterState, new State.ActivateEvent(this.OnEnterState));
				this.LeaveState = (State.ActivateEvent)Delegate.Remove(this.LeaveState, new State.ActivateEvent(this.OnLeaveState));
				this.context = null;
			}
		}

		// Token: 0x04000228 RID: 552
		private MainForm.Context context;

		// Token: 0x04000229 RID: 553
		private bool modified;

		// Token: 0x0400022A RID: 554
		private MapEditorMap map;

		// Token: 0x0400022B RID: 555
		private MapCheckerContainer mapCheckerContainer;

		// Token: 0x0400022C RID: 556
		private TerrainDataSource terrainDataSource;

		// Token: 0x0400022D RID: 557
		private SaveLoad saveLoad;

		// Token: 0x0400022E RID: 558
		private MapEditorScene mapEditorScene;

		// Token: 0x0400022F RID: 559
		private MultiState multiState;

		// Token: 0x04000230 RID: 560
		private ObjectsState objectsState;

		// Token: 0x04000231 RID: 561
		private MapZonesState zonesState;

		// Token: 0x04000232 RID: 562
		private LightsState lightsState;

		// Token: 0x04000233 RID: 563
		private SoundState soundState;

		// Token: 0x04000234 RID: 564
		private DistanceState distanceState;

		// Token: 0x04000235 RID: 565
		private LandscapeState landscapeState;

		// Token: 0x04000236 RID: 566
		private readonly DbEventsGenerator dbEventsGenerator;

		// Token: 0x04000237 RID: 567
		private readonly IDatabase mainDb;

		// Token: 0x04000238 RID: 568
		private PenTablet penTablet;
	}
}
