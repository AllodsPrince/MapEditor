using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using Db;
using Db.Main;
using InputState;
using LauncherTools.InputState;
using MapEditor.Forms.CameraFOV;
using MapEditor.Forms.CameraSpeed;
using MapEditor.Map;
using MapEditor.Map.DataProviders;
using MapEditor.Map.Dialogs;
using MapEditor.Map.MapObjects;
using MapEditor.Map.States;
using MapEditor.Resources.Strings;
using MapEditor.Scene;
using Operations;
using Tools.Geometry;
using Tools.InputState;
using Tools.MapObjects;

namespace MapEditor.Forms.ObjectEditor
{
	// Token: 0x0200029F RID: 671
	internal class ObjectEditorEditState : State
	{
		// Token: 0x06001F2C RID: 7980 RVA: 0x000C7880 File Offset: 0x000C6880
		private void CreateStates()
		{
			this.objectsState = new ObjectsState(this.context, this.stateContainer, this.map.MapEditorMapObjectContainer, this.map.OperationContainer, null, this.context.Tool, this.context.Tool, this.context.ItemDataContainer, string.Empty, string.Empty, ContinentType.Unknown, this.mapEditorScene, this.context.PropertyControl, this.objectEditorForm, this.context.MultiObjectBrowser, this.objectEditorForm, "ObjectEditor_ObjectSelectorData.xml");
			this.objectsState.Container = this.stateContainer;
			this.context.StateContainer.AddMethod("_object_item_list_selected", new Method(this.OnItemListSelected));
			this.context.StateContainer.AddMethod("_object_item_list_double_clicked", new Method(this.OnItemListDoubleClicked));
			this.context.StateContainer.AddMethod("_object_item_list_unselected", new Method(this.OnItemListUnselected));
			this.context.StateContainer.AddMethod("_object_item_list_cleared", new Method(this.OnItemListCleared));
		}

		// Token: 0x06001F2D RID: 7981 RVA: 0x000C79AC File Offset: 0x000C69AC
		private void DestroyStates()
		{
			this.context.StateContainer.RemoveMethod("_object_item_list_selected", new Method(this.OnItemListSelected));
			this.context.StateContainer.RemoveMethod("_object_item_list_double_clicked", new Method(this.OnItemListDoubleClicked));
			this.context.StateContainer.RemoveMethod("_object_item_list_unselected", new Method(this.OnItemListUnselected));
			this.context.StateContainer.RemoveMethod("_object_item_list_cleared", new Method(this.OnItemListCleared));
			if (this.objectsState != null)
			{
				this.objectsState.Destroy();
				this.objectsState = null;
			}
		}

		// Token: 0x06001F2E RID: 7982 RVA: 0x000C7A58 File Offset: 0x000C6A58
		private void UpdateMethods()
		{
			base.SetMethodParams("open_map", true, true, false, false);
			base.SetMethodParams("save_map", true, this.Modified, false, false);
			base.SetMethodParams("toggle_save_as", true, !string.IsNullOrEmpty(this.openFileName), false, false);
		}

		// Token: 0x06001F2F RID: 7983 RVA: 0x000C7AA4 File Offset: 0x000C6AA4
		private void CenterCamera()
		{
			CameraPlacement placement = new CameraPlacement(new Position(0.0, -30.0, 30.0), new Rotation(1.5707964f, 0.62831855f, 0f));
			this.editorScene.SetPlacement(this.editorViewID, ref placement);
		}

		// Token: 0x06001F30 RID: 7984 RVA: 0x000C7B04 File Offset: 0x000C6B04
		private void UpdateCameraMethods()
		{
			base.SetMethodParams("camera_slow", false, false, true, this.cameraParams.CameraType == ObjectEditorCameraParams.ObjectEditorCameraType.Slow);
			base.SetMethodParams("camera_normal", false, false, true, this.cameraParams.CameraType == ObjectEditorCameraParams.ObjectEditorCameraType.Normal);
			base.SetMethodParams("camera_custom", false, false, true, this.cameraParams.CameraType == ObjectEditorCameraParams.ObjectEditorCameraType.Custom);
		}

		// Token: 0x06001F31 RID: 7985 RVA: 0x000C7B68 File Offset: 0x000C6B68
		private void CreateCameraControls()
		{
			this.cameraParams = Serializer.Load<ObjectEditorCameraParams>(EditorEnvironment.EditorFormsFolder + "ObjectEditorCameraParams.xml");
			if (this.cameraParams == null)
			{
				this.cameraParams = new ObjectEditorCameraParams();
			}
			this.cameraFOVForm = new CameraFOVForm();
			this.cameraFOVForm.FovChanged += this.SetCameraFOV;
			this.cameraSpeedForm = new CameraSpeedForm();
			this.cameraSpeedForm.SpeedChanged += this.SetCameraSpeed;
			this.editorScene.SetCameraFOV(this.editorViewID, this.cameraParams.CameraFOV);
			this.editorScene.SetCameraMoveSpeed(this.editorViewID, this.cameraParams.GetCameraSpeed());
			base.AddMethod("camera_slow", new Method(this.OnCameraSpeedSlow));
			base.AddMethod("camera_normal", new Method(this.OnCameraSpeedNormal));
			base.AddMethod("camera_custom", new Method(this.OnCameraSpeedCustom));
			base.AddMethod("camera_reset", new Method(this.OnResetCamera));
			base.AddMethod("camera_move", new Method(this.OnMoveCamera));
			base.AddMethod("camera_fov", new Method(this.OnCameraFOV));
			base.AddMethod("camera_speed", new Method(this.OnCameraSpeed));
			this.CenterCamera();
		}

		// Token: 0x06001F32 RID: 7986 RVA: 0x000C7CC8 File Offset: 0x000C6CC8
		private void DestroyCameraControls()
		{
			Serializer.Save(EditorEnvironment.EditorFormsFolder + "ObjectEditorCameraParams.xml", this.cameraParams, false);
			base.RemoveMethod("camera_slow");
			base.RemoveMethod("camera_normal");
			base.RemoveMethod("camera_custom");
			base.RemoveMethod("camera_reset");
			base.RemoveMethod("camera_move");
			base.RemoveMethod("camera_fov");
			base.RemoveMethod("camera_speed");
			this.cameraFOVForm.FovChanged -= this.SetCameraFOV;
			this.cameraSpeedForm.SpeedChanged -= this.SetCameraSpeed;
		}

		// Token: 0x06001F33 RID: 7987 RVA: 0x000C7D6C File Offset: 0x000C6D6C
		private void SetCameraFOV(double fov)
		{
			this.cameraParams.CameraFOV = fov;
			this.editorScene.SetCameraFOV(this.editorViewID, this.cameraParams.CameraFOV);
		}

		// Token: 0x06001F34 RID: 7988 RVA: 0x000C7D96 File Offset: 0x000C6D96
		private void SetCameraSpeed(double speed)
		{
			this.cameraParams.CameraSpeed = speed;
			this.editorScene.SetCameraMoveSpeed(this.editorViewID, this.cameraParams.GetCameraSpeed());
		}

		// Token: 0x06001F35 RID: 7989 RVA: 0x000C7DC0 File Offset: 0x000C6DC0
		private void OnCameraFOV(MethodArgs methodArgs)
		{
			this.cameraFOVForm.Show();
			this.cameraFOVForm.SetFOV(this.cameraParams.CameraFOV);
			this.cameraFOVForm.SetBounds(Cursor.Position.X - this.cameraFOVForm.Size.Width / 2, Cursor.Position.Y - this.cameraFOVForm.Size.Height / 2, 0, 0, BoundsSpecified.Location);
		}

		// Token: 0x06001F36 RID: 7990 RVA: 0x000C7E44 File Offset: 0x000C6E44
		private void OnCameraSpeed(MethodArgs methodArgs)
		{
			this.cameraSpeedForm.Show();
			this.cameraSpeedForm.SetSpeed(this.cameraParams.CameraSpeed);
			this.cameraSpeedForm.SetBounds(Cursor.Position.X - this.cameraSpeedForm.Size.Width / 2, Cursor.Position.Y - this.cameraSpeedForm.Size.Height / 2, 0, 0, BoundsSpecified.Location);
		}

		// Token: 0x06001F37 RID: 7991 RVA: 0x000C7EC6 File Offset: 0x000C6EC6
		private void OnCameraSpeedSlow(MethodArgs methodArgs)
		{
			this.cameraParams.CameraType = ObjectEditorCameraParams.ObjectEditorCameraType.Slow;
			this.editorScene.SetCameraMoveSpeed(this.editorViewID, this.cameraParams.GetCameraSpeed());
			this.UpdateCameraMethods();
		}

		// Token: 0x06001F38 RID: 7992 RVA: 0x000C7EF6 File Offset: 0x000C6EF6
		private void OnCameraSpeedNormal(MethodArgs methodArgs)
		{
			this.cameraParams.CameraType = ObjectEditorCameraParams.ObjectEditorCameraType.Normal;
			this.editorScene.SetCameraMoveSpeed(this.editorViewID, this.cameraParams.GetCameraSpeed());
			this.UpdateCameraMethods();
		}

		// Token: 0x06001F39 RID: 7993 RVA: 0x000C7F26 File Offset: 0x000C6F26
		private void OnCameraSpeedCustom(MethodArgs methodArgs)
		{
			this.cameraParams.CameraType = ObjectEditorCameraParams.ObjectEditorCameraType.Custom;
			this.editorScene.SetCameraMoveSpeed(this.editorViewID, this.cameraParams.GetCameraSpeed());
			this.UpdateCameraMethods();
		}

		// Token: 0x06001F3A RID: 7994 RVA: 0x000C7F56 File Offset: 0x000C6F56
		private void OnResetCamera(MethodArgs methodArgs)
		{
			this.CenterCamera();
		}

		// Token: 0x06001F3B RID: 7995 RVA: 0x000C7F60 File Offset: 0x000C6F60
		private void OnMoveCamera(MethodArgs methodArgs)
		{
			MoveCameraForm moveCameraForm = new MoveCameraForm(false);
			if (moveCameraForm.ShowDialog() == DialogResult.OK && moveCameraForm.CoordsValid)
			{
				CameraPlacement cameraPlacement;
				this.editorScene.GetPlacement(this.editorViewID, out cameraPlacement);
				Position positon = moveCameraForm.CameraPosition;
				if (moveCameraForm.OnlyXYCoordsValid)
				{
					positon.Z = cameraPlacement.Position.Z;
				}
				cameraPlacement.Position = positon;
				this.editorScene.SetPlacement(this.editorViewID, ref cameraPlacement);
			}
		}

		// Token: 0x06001F3C RID: 7996 RVA: 0x000C7FDC File Offset: 0x000C6FDC
		private void UpdateTitle()
		{
			string text = string.Empty;
			if (!string.IsNullOrEmpty(this.openFileName))
			{
				text = this.openFileName;
				if (this.Modified)
				{
					text += "*";
				}
				text += " - ";
			}
			text += Strings.OBJECT_EDITOR_TITLE;
			if (this.objectEditorForm != null)
			{
				this.objectEditorForm.Text = text;
			}
		}

		// Token: 0x17000723 RID: 1827
		// (get) Token: 0x06001F3D RID: 7997 RVA: 0x000C8043 File Offset: 0x000C7043
		// (set) Token: 0x06001F3E RID: 7998 RVA: 0x000C804B File Offset: 0x000C704B
		public bool Modified
		{
			get
			{
				return this.modified;
			}
			set
			{
				this.modified = value;
				this.UpdateMethods();
			}
		}

		// Token: 0x06001F3F RID: 7999 RVA: 0x000C805A File Offset: 0x000C705A
		private void OnEnterState(IState mapEditState)
		{
			this.UpdateMethods();
			this.UpdateCameraMethods();
		}

		// Token: 0x06001F40 RID: 8000 RVA: 0x000C8068 File Offset: 0x000C7068
		private void OnLeaveState(IState mapEditState)
		{
			this.UpdateMethods();
			this.UpdateCameraMethods();
		}

		// Token: 0x06001F41 RID: 8001 RVA: 0x000C8076 File Offset: 0x000C7076
		private void OnMouseEnter(MethodArgs methodArgs)
		{
			if (this.objectEditorForm != null)
			{
				this.objectEditorForm.Focus();
				this.context.LastActiveForm = this.objectEditorForm;
			}
		}

		// Token: 0x06001F42 RID: 8002 RVA: 0x000C80A0 File Offset: 0x000C70A0
		private void OnOpenMap(MethodArgs methodArgs)
		{
			OpenFileDialog openDialog = new OpenFileDialog();
			openDialog.Filter = "(StaticObject).xdb files|*.(StaticObject).xdb";
			if (string.IsNullOrEmpty(this.objectEditorForm.OpenFileFolder) || !this.objectEditorForm.OpenFileFolder.StartsWith(EditorEnvironment.DataFolder))
			{
				openDialog.InitialDirectory = EditorEnvironment.DataFolder.Replace('/', '\\');
			}
			else
			{
				openDialog.InitialDirectory = this.objectEditorForm.OpenFileFolder.Replace('/', '\\');
			}
			if (!Directory.Exists(openDialog.InitialDirectory))
			{
				Directory.CreateDirectory(openDialog.InitialDirectory);
			}
			openDialog.RestoreDirectory = true;
			openDialog.Multiselect = false;
			if (openDialog.ShowDialog() == DialogResult.OK)
			{
				FileInfo fileInfo = new FileInfo(openDialog.FileName);
				if (fileInfo.Directory != null)
				{
					this.objectEditorForm.OpenFileFolder = fileInfo.Directory.ToString().Replace('\\', '/');
				}
				this.Load(openDialog.FileName);
				this.Modified = false;
			}
			this.UpdateMethods();
		}

		// Token: 0x06001F43 RID: 8003 RVA: 0x000C8194 File Offset: 0x000C7194
		private void OnLoadObject(MethodArgs methodArgs)
		{
			string fileName = methodArgs.sender as string;
			if (fileName != null)
			{
				this.Load(fileName);
				this.Modified = false;
			}
		}

		// Token: 0x06001F44 RID: 8004 RVA: 0x000C81C0 File Offset: 0x000C71C0
		private void OnSaveAs(MethodArgs methodArgs)
		{
			SaveFileDialog saveDialog = new SaveFileDialog();
			saveDialog.SupportMultiDottedExtensions = true;
			saveDialog.Filter = "(StaticObject).xdb files|*.(StaticObject).xdb|All xdb files|*.xdb";
			if (string.IsNullOrEmpty(this.objectEditorForm.OpenFileFolder) || !this.objectEditorForm.OpenFileFolder.StartsWith(EditorEnvironment.DataFolder, StringComparison.CurrentCultureIgnoreCase))
			{
				saveDialog.InitialDirectory = EditorEnvironment.DataFolder.Replace('/', '\\');
			}
			else
			{
				saveDialog.InitialDirectory = this.objectEditorForm.OpenFileFolder.Replace('/', '\\');
			}
			if (!Directory.Exists(saveDialog.InitialDirectory))
			{
				Directory.CreateDirectory(saveDialog.InitialDirectory);
			}
			saveDialog.RestoreDirectory = true;
			if (saveDialog.ShowDialog(this.context.MainForm) == DialogResult.OK)
			{
				FileInfo fileInfo = new FileInfo(saveDialog.FileName);
				if (fileInfo.Directory != null)
				{
					this.objectEditorForm.OpenFileFolder = fileInfo.Directory.ToString().Replace('\\', '/');
				}
				string fileName = saveDialog.FileName;
				if (!fileName.EndsWith(".xdb", StringComparison.InvariantCultureIgnoreCase))
				{
					fileName += ".xdb";
				}
				this.Save(fileName);
				this.Load(fileName);
			}
		}

		// Token: 0x06001F45 RID: 8005 RVA: 0x000C82D5 File Offset: 0x000C72D5
		private void OnSaveMap(MethodArgs methodArgs)
		{
			if (this.staticObject != null && !DBID.IsNullOrEmpty(this.staticObject.StaticObjectDBID))
			{
				this.Save(this.staticObject.StaticObjectDBID.ToString());
				this.Modified = false;
				this.UpdateMethods();
			}
		}

		// Token: 0x06001F46 RID: 8006 RVA: 0x000C8314 File Offset: 0x000C7314
		private void OnMapObjectContainerModified(MapObjectContainer mapObjectContainer)
		{
			this.Modified = true;
			this.UpdateMethods();
		}

		// Token: 0x17000724 RID: 1828
		// (get) Token: 0x06001F47 RID: 8007 RVA: 0x000C8323 File Offset: 0x000C7323
		public MainForm.Context Context
		{
			get
			{
				return this.context;
			}
		}

		// Token: 0x17000725 RID: 1829
		// (get) Token: 0x06001F48 RID: 8008 RVA: 0x000C832B File Offset: 0x000C732B
		public ObjectEditorMap ObjectEditorMap
		{
			get
			{
				return this.map;
			}
		}

		// Token: 0x17000726 RID: 1830
		// (get) Token: 0x06001F49 RID: 8009 RVA: 0x000C8333 File Offset: 0x000C7333
		public MapEditorScene MapEditorScene
		{
			get
			{
				return this.mapEditorScene;
			}
		}

		// Token: 0x06001F4A RID: 8010 RVA: 0x000C833C File Offset: 0x000C733C
		public ObjectEditorEditState(MainForm.Context _context, StateContainer _stateContainer, OperationContainer _operationContainer, ObjectEditorForm _objectEditorForm, EditorScene _editorScene, int _editorSceneViewID) : base("ObjectEditorEditState")
		{
			if (_context != null)
			{
				this.context = _context;
				this.stateContainer = _stateContainer;
				this.objectEditorForm = _objectEditorForm;
				this.operationContainer = _operationContainer;
				this.editorScene = _editorScene;
				this.editorViewID = _editorSceneViewID;
				this.EnterState = (State.ActivateEvent)Delegate.Combine(this.EnterState, new State.ActivateEvent(this.OnEnterState));
				this.LeaveState = (State.ActivateEvent)Delegate.Combine(this.LeaveState, new State.ActivateEvent(this.OnLeaveState));
				this.map = new ObjectEditorMap(this.operationContainer);
				this.mapEditorScene = new MapEditorScene(this.editorScene, this.editorViewID, this.stateContainer, EditorEnvironment.EditorFormsFolder + "ObjectEditorMapScene.xml");
				this.mapEditorScene.CalculateColor = true;
				this.mapEditorScene.Bind(ContinentType.Unknown, 1.0, this.map.MapEditorMapObjectContainer, null, null, null, null, null, null, null, null, null);
				this.map.MapEditorMapObjectContainer.Modified += this.OnMapObjectContainerModified;
				base.AddMethod("mouse_enter", new Method(this.OnMouseEnter));
				base.AddMethod("open_map", new Method(this.OnOpenMap));
				base.AddMethod("save_map", new Method(this.OnSaveMap));
				base.AddMethod("toggle_save_as", new Method(this.OnSaveAs));
				base.AddMethod("_object_editor_make_screenshot", new Method(this.OnMakeScreenShot));
				base.AddMethod("_object_editor_load_object", new Method(this.OnLoadObject));
				this.operationContainer.Clear();
				this.Modified = false;
				this.map.Data.MinXMinYPatchCoords = new Tools.Geometry.Point(0, 0);
				this.map.Data.ContinentName = "ObjectEditor";
				this.CreateCameraControls();
				this.CreateStates();
				this.UpdateTitle();
				this.objectsState.MapObjectSelector.Surface = TerrainSurface.None;
			}
		}

		// Token: 0x06001F4B RID: 8011 RVA: 0x000C856C File Offset: 0x000C756C
		public void Destroy()
		{
			if (this.context != null)
			{
				this.Clear();
				this.DestroyStates();
				this.DestroyCameraControls();
				this.map.MapEditorMapObjectContainer.Modified -= this.OnMapObjectContainerModified;
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

		// Token: 0x06001F4C RID: 8012 RVA: 0x000C862C File Offset: 0x000C762C
		private void OnItemListSelected(MethodArgs methodArgs)
		{
			if (this.context.LastActiveForm == this.objectEditorForm)
			{
				this.stateContainer.Invoke("_object_item_list_selected", methodArgs);
			}
		}

		// Token: 0x06001F4D RID: 8013 RVA: 0x000C8654 File Offset: 0x000C7654
		private void OnItemListDoubleClicked(MethodArgs methodArgs)
		{
			string item = methodArgs.sender as string;
			if (!string.IsNullOrEmpty(item))
			{
				this.Load(item);
			}
		}

		// Token: 0x06001F4E RID: 8014 RVA: 0x000C867D File Offset: 0x000C767D
		private void OnItemListUnselected(MethodArgs methodArgs)
		{
			if (this.context.LastActiveForm == this.objectEditorForm)
			{
				this.stateContainer.Invoke("_object_item_list_unselected", methodArgs);
			}
		}

		// Token: 0x06001F4F RID: 8015 RVA: 0x000C86A3 File Offset: 0x000C76A3
		private void OnItemListCleared(MethodArgs methodArgs)
		{
			if (this.context.LastActiveForm == this.objectEditorForm)
			{
				this.stateContainer.Invoke("_object_item_list_cleared", methodArgs);
			}
		}

		// Token: 0x06001F50 RID: 8016 RVA: 0x000C86CC File Offset: 0x000C76CC
		private void OnMakeScreenShot(MethodArgs methodArgs)
		{
			Rectangle screenshotRect;
			try
			{
				screenshotRect = (Rectangle)methodArgs.sender;
			}
			catch (Exception e)
			{
				Console.Write(e);
				return;
			}
			Bitmap bmpScreenshot = new Bitmap(screenshotRect.Width, screenshotRect.Height);
			Graphics gfxScreenshot = Graphics.FromImage(bmpScreenshot);
			gfxScreenshot.CopyFromScreen(screenshotRect.Left, screenshotRect.Top, 0, 0, screenshotRect.Size, CopyPixelOperation.SourceCopy);
			gfxScreenshot.Dispose();
			IDatabase mainDb = IDatabase.GetMainDatabase();
			if (mainDb != null && !string.IsNullOrEmpty(this.openFileName))
			{
				DBID objectDBID = mainDb.GetDBIDByName(this.openFileName);
				if (this.context == null)
				{
					return;
				}
				Bitmap smallIcon = new Bitmap(bmpScreenshot, StaticObjectItemDataMiner.SmallIconSize);
				Bitmap largeIcon = new Bitmap(bmpScreenshot, StaticObjectItemDataMiner.LargeIconSize);
				string cacheFilePath;
				if (this.context.ItemDataContainer.SetIcons(objectDBID.ToString(), smallIcon, largeIcon, out cacheFilePath) && File.Exists(cacheFilePath))
				{
					Serializer.AddToMainRCS(cacheFilePath);
				}
				return;
			}
		}

		// Token: 0x06001F51 RID: 8017 RVA: 0x000C87C0 File Offset: 0x000C77C0
		private void Clear()
		{
			this.operationContainer.Clear();
			this.operationContainer.Enable = false;
			if (this.context.StateContainer != null)
			{
				this.context.StateContainer.Invoke("_clear_selection_in_object_item_list", default(MethodArgs));
			}
			this.objectsState.MapObjectSelector.Clear();
			this.objectsState.MapObjectClipboard.Clear();
			List<int> mapObjectIDs = new List<int>();
			foreach (KeyValuePair<int, IMapObject> keyValuePair in this.map.MapEditorMapObjectContainer.MapObjects)
			{
				mapObjectIDs.Add(keyValuePair.Value.ID);
			}
			foreach (int mapObjectID in mapObjectIDs)
			{
				this.map.MapEditorMapObjectContainer.RemoveMapObject(mapObjectID);
			}
			if (this.mainObjectSceneID != -1)
			{
				this.editorScene.DeleteObject(this.mainObjectSceneID);
				this.mainObjectSceneID = -1;
			}
			this.operationContainer.Enable = true;
		}

		// Token: 0x06001F52 RID: 8018 RVA: 0x000C8908 File Offset: 0x000C7908
		public void Load(string fileName)
		{
			if (!fileName.EndsWith(".xdb"))
			{
				return;
			}
			IDatabase mainDb = IDatabase.GetMainDatabase();
			if (mainDb == null)
			{
				return;
			}
			if (mainDb.GetClassTypeNameByFile(fileName) != StaticObject.DBType)
			{
				return;
			}
			if (this.Modified && !string.IsNullOrEmpty(this.openFileName) && MessageBox.Show(Strings.MAP_SAVE_CHANGES_MESSAGE, Strings.MAP_SAVE_CHANGES_TITLE, MessageBoxButtons.YesNo) == DialogResult.Yes)
			{
				this.Save(this.openFileName);
			}
			this.Clear();
			this.operationContainer.Enable = false;
			this.objectDbIndicies.Clear();
			DBID objectDBID = mainDb.GetDBIDByName(fileName);
			this.objectEditorForm.OpenFileFolder = EditorEnvironment.DataFolder + Str.CutFileName(objectDBID.ToString(), false);
			IObjMan objectMan = this.staticObject.Load(objectDBID, mainDb);
			if (!objectDBID.IsEmpty())
			{
				if (!DBID.IsNullOrEmpty(this.staticObject.ObjectTemplate))
				{
					string objectTemplate = this.staticObject.ObjectTemplate.ToString();
					string collision = string.Empty;
					if (!DBID.IsNullOrEmpty(this.staticObject.Collision))
					{
						collision = this.staticObject.Collision.ToString();
					}
					this.mainObjectSceneID = this.editorScene.CreateObjectByVisObjectTemplate(objectTemplate, collision);
				}
				if (objectMan != null)
				{
					int partCount;
					objectMan.GetValue("parts", out partCount);
					for (int i = 0; i < partCount; i++)
					{
						string partPropertyName = string.Format("parts.[{0}].", i);
						float posX;
						objectMan.GetValue(partPropertyName + "Position.x", out posX);
						float posY;
						objectMan.GetValue(partPropertyName + "Position.y", out posY);
						float posZ;
						objectMan.GetValue(partPropertyName + "Position.z", out posZ);
						float rotX;
						objectMan.GetValue(partPropertyName + "Rotation.x", out rotX);
						float rotY;
						objectMan.GetValue(partPropertyName + "Rotation.y", out rotY);
						float rotZ;
						objectMan.GetValue(partPropertyName + "Rotation.z", out rotZ);
						float rotW;
						objectMan.GetValue(partPropertyName + "Rotation.w", out rotW);
						float ratio;
						objectMan.GetValue(partPropertyName + "Scale", out ratio);
						DBID dbid;
						objectMan.GetValue(partPropertyName + "StaticObjectTemplate", out dbid);
						bool useManualColor;
						objectMan.GetValue(partPropertyName + "useManualColor", out useManualColor);
						EditorSceneDLLImport.COLOR_INFO info;
						objectMan.GetValue(partPropertyName + "ColorInfo.occlusionColor", out info.occlusionColor);
						objectMan.GetValue(partPropertyName + "ColorInfo.ambientColor", out info.ambientColor);
						objectMan.GetValue(partPropertyName + "ColorInfo.useCustomColor", out info.useCustomColor);
						Quat quat = new Quat((double)rotX, (double)rotY, (double)rotZ, (double)rotW);
						double rot;
						double rot2;
						double rot3;
						quat.GetYawPitchRoll(out rot, out rot2, out rot3);
						Rotation rotation = new Rotation((float)rot, (float)rot2, (float)rot3);
						if (!dbid.IsEmpty())
						{
							int id = this.map.MapEditorMapObjectContainer.AddMapObject(new MapObjectType(MapObjectFactory.Type.StaticObject, dbid.ToString()), false, new Position((double)posX, (double)posY, (double)posZ), rotation, new Scale(ratio), useManualColor, info);
							this.objectDbIndicies.Add(i, id);
						}
					}
				}
			}
			this.Modified = false;
			this.openFileName = fileName;
			this.UpdateTitle();
			this.operationContainer.Enable = true;
			this.objectEditorForm.ObjectRecentList.AddItem(this.openFileName);
			this.UpdateMethods();
		}

		// Token: 0x06001F53 RID: 8019 RVA: 0x000C8C4C File Offset: 0x000C7C4C
		public void Save(string fileName)
		{
			IDatabase mainDb = IDatabase.GetMainDatabase();
			IObjMan objectMan = this.staticObject.SaveAs(fileName, mainDb);
			if (objectMan != null)
			{
				Dictionary<int, EditorSceneDLLImport.COLOR_INFO> manualColorObjects = new Dictionary<int, EditorSceneDLLImport.COLOR_INFO>();
				int partCount;
				objectMan.GetValue("parts", out partCount);
				foreach (KeyValuePair<int, int> objectPart in this.objectDbIndicies)
				{
					if (objectPart.Key < partCount)
					{
						string partPropertyName = string.Format("parts.[{0}].", objectPart.Key);
						bool useManualColor;
						objectMan.GetValue(partPropertyName + "useManualColor", out useManualColor);
						if (useManualColor)
						{
							EditorSceneDLLImport.COLOR_INFO colorInfo = default(EditorSceneDLLImport.COLOR_INFO);
							objectMan.GetValue(partPropertyName + "ColorInfo.occlusionColor", out colorInfo.occlusionColor);
							objectMan.GetValue(partPropertyName + "ColorInfo.ambientColor", out colorInfo.ambientColor);
							objectMan.GetValue(partPropertyName + "ColorInfo.useCustomColor", out colorInfo.useCustomColor);
							manualColorObjects.Add(objectPart.Value, colorInfo);
						}
					}
				}
				ObjMan.StartMassEditing();
				objectMan.SetValue("parts", 0);
				foreach (KeyValuePair<int, IMapObject> keyValuePair in this.map.MapEditorMapObjectContainer.MapObjects)
				{
					if (!keyValuePair.Value.Temporary)
					{
						int objectIndex;
						objectMan.GetValue("parts", out objectIndex);
						string partPropertyName2 = string.Format("parts.[{0}]", objectIndex);
						objectMan.Insert("parts", -1, 1);
						IMapObject mapObject = keyValuePair.Value;
						IObjMan partMan = objectMan.CreateManipulator(partPropertyName2);
						if (mapObject != null && partMan != null)
						{
							partMan.SetValue("Position.x", (float)mapObject.Position.X);
							partMan.SetValue("Position.y", (float)mapObject.Position.Y);
							partMan.SetValue("Position.z", (float)mapObject.Position.Z);
							Quat quat = new Quat(mapObject.Rotation);
							partMan.SetValue("Rotation.x", (float)quat.X);
							partMan.SetValue("Rotation.y", (float)quat.Y);
							partMan.SetValue("Rotation.z", (float)quat.Z);
							partMan.SetValue("Rotation.w", (float)quat.W);
							partMan.SetValue("Scale", mapObject.Scale.Ratio);
							DBID dbid = mainDb.GetDBIDByName(mapObject.Type.Stats);
							partMan.SetValue("StaticObjectTemplate", dbid);
							EditorSceneDLLImport.COLOR_INFO colorInfo2;
							if (manualColorObjects.TryGetValue(keyValuePair.Key, out colorInfo2))
							{
								partMan.SetValue("useManualColor", true);
							}
							else
							{
								this.context.EditorScene.GetObjectColorInfo(this.mapEditorScene.MapSceneObjects.MapObjectIDToEditorSceneObjectID(mapObject.ID), ref colorInfo2);
							}
							partMan.SetValue("ColorInfo.occlusionColor", colorInfo2.occlusionColor);
							partMan.SetValue("ColorInfo.ambientColor", colorInfo2.ambientColor);
							partMan.SetValue("ColorInfo.useCustomColor", colorInfo2.useCustomColor);
						}
					}
				}
			}
			ObjMan.StopMassEditing();
			this.context.StateContainer.Invoke("_object_editor_static_object_changed", new MethodArgs(null, this.staticObject.StaticObjectDBID.ToString(), null));
			mainDb.SaveChanges();
			this.Modified = false;
		}

		// Token: 0x17000727 RID: 1831
		// (get) Token: 0x06001F54 RID: 8020 RVA: 0x000C8FFC File Offset: 0x000C7FFC
		public DBID StaticObjectDBID
		{
			get
			{
				if (this.staticObject == null)
				{
					return null;
				}
				return this.staticObject.StaticObjectDBID;
			}
		}

		// Token: 0x06001F55 RID: 8021 RVA: 0x000C9014 File Offset: 0x000C8014
		public void AddObject(string dbid, Position position, Rotation rotation, Scale ratio)
		{
			if (!string.IsNullOrEmpty(dbid) && this.staticObject != null && !string.Equals(this.staticObject.StaticObjectDBID.ToString(), dbid, StringComparison.InvariantCultureIgnoreCase))
			{
				foreach (IMapObject mapObject in this.map.MapEditorMapObjectContainer.MapObjects.Values)
				{
					if (string.Equals(mapObject.SceneName, dbid, StringComparison.InvariantCultureIgnoreCase) && (mapObject.Position - position).Vec3.Length2 < (double)MathConsts.FLOAT_EPSILON)
					{
						return;
					}
				}
				this.map.MapEditorMapObjectContainer.AddMapObject(new MapObjectType(MapObjectFactory.Type.StaticObject, dbid), false, position, rotation, ratio);
			}
		}

		// Token: 0x04001352 RID: 4946
		private MainForm.Context context;

		// Token: 0x04001353 RID: 4947
		private ObjectEditorMap map;

		// Token: 0x04001354 RID: 4948
		private MapEditorScene mapEditorScene;

		// Token: 0x04001355 RID: 4949
		private ObjectEditorCameraParams cameraParams;

		// Token: 0x04001356 RID: 4950
		private CameraFOVForm cameraFOVForm;

		// Token: 0x04001357 RID: 4951
		private CameraSpeedForm cameraSpeedForm;

		// Token: 0x04001358 RID: 4952
		private bool modified;

		// Token: 0x04001359 RID: 4953
		private ObjectsState objectsState;

		// Token: 0x0400135A RID: 4954
		private readonly StateContainer stateContainer;

		// Token: 0x0400135B RID: 4955
		private readonly OperationContainer operationContainer;

		// Token: 0x0400135C RID: 4956
		private readonly ObjectEditorForm objectEditorForm;

		// Token: 0x0400135D RID: 4957
		private readonly EditorScene editorScene;

		// Token: 0x0400135E RID: 4958
		private readonly int editorViewID = -1;

		// Token: 0x0400135F RID: 4959
		private readonly ObjectEditorEditState.DBStaticObject staticObject = new ObjectEditorEditState.DBStaticObject();

		// Token: 0x04001360 RID: 4960
		private string openFileName;

		// Token: 0x04001361 RID: 4961
		private int mainObjectSceneID = -1;

		// Token: 0x04001362 RID: 4962
		private readonly Dictionary<int, int> objectDbIndicies = new Dictionary<int, int>(7);

		// Token: 0x020002A0 RID: 672
		internal class DBStaticObject
		{
			// Token: 0x06001F56 RID: 8022 RVA: 0x000C90F8 File Offset: 0x000C80F8
			private DBID GetFieldValue(string fieldName)
			{
				DBID dbid = DBID.Empty;
				if (this.staticObjectObjMan != null)
				{
					this.staticObjectObjMan.GetValue(fieldName, out dbid);
				}
				return dbid;
			}

			// Token: 0x17000728 RID: 1832
			// (get) Token: 0x06001F57 RID: 8023 RVA: 0x000C9122 File Offset: 0x000C8122
			public DBID ObjectTemplate
			{
				get
				{
					return this.GetFieldValue("ObjectTemplate");
				}
			}

			// Token: 0x17000729 RID: 1833
			// (get) Token: 0x06001F58 RID: 8024 RVA: 0x000C912F File Offset: 0x000C812F
			public DBID Collision
			{
				get
				{
					return this.GetFieldValue("Collision");
				}
			}

			// Token: 0x06001F59 RID: 8025 RVA: 0x000C913C File Offset: 0x000C813C
			public IObjMan Load(DBID objectDBID, IDatabase mainDb)
			{
				if (this.staticObjectObjMan == null || this.staticObjectObjMan.DBID != objectDBID)
				{
					this.staticObjectObjMan = null;
					if (mainDb.GetClassTypeName(objectDBID) == StaticObject.DBType)
					{
						this.staticObjectObjMan = mainDb.GetManipulator(objectDBID);
					}
				}
				return this.staticObjectObjMan;
			}

			// Token: 0x06001F5A RID: 8026 RVA: 0x000C9194 File Offset: 0x000C8194
			public IObjMan SaveAs(string fileName, IDatabase mainDb)
			{
				DBID objectDBID = mainDb.GetDBIDByName(fileName);
				IObjMan objMan = null;
				if (!mainDb.DoesObjectExist(objectDBID))
				{
					objectDBID = IDatabase.CreateDBIDByName(fileName);
					objMan = mainDb.CreateNewObject(StaticObject.DBType);
					mainDb.AddNewObject(objectDBID, objMan);
				}
				if (this.staticObjectObjMan != null && this.staticObjectObjMan.DBID != objectDBID && mainDb.GetClassTypeName(objectDBID) == StaticObject.DBType)
				{
					mainDb.CopyObject(objectDBID, this.staticObjectObjMan.DBID);
					this.staticObjectObjMan = (objMan ?? mainDb.GetManipulator(objectDBID));
					if (this.staticObjectObjMan != null)
					{
						this.staticObjectObjMan.SetValue("sourceFile", string.Empty);
						this.staticObjectObjMan.SetValue("sourceFileCRC", 0);
					}
				}
				return this.staticObjectObjMan;
			}

			// Token: 0x1700072A RID: 1834
			// (get) Token: 0x06001F5B RID: 8027 RVA: 0x000C9256 File Offset: 0x000C8256
			public DBID StaticObjectDBID
			{
				get
				{
					if (this.staticObjectObjMan == null)
					{
						return DBID.Empty;
					}
					return this.staticObjectObjMan.DBID;
				}
			}

			// Token: 0x04001363 RID: 4963
			private IObjMan staticObjectObjMan;
		}
	}
}
