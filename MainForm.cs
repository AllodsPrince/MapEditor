using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Text;
using System.Windows.Forms;
using System.Windows.Forms.CreateObject;
using Db;
using Db.Consistency;
using InputState;
using LauncherTools.InputState;
using Logging;
using MapEditor.Forms.About;
using MapEditor.Forms.Base;
using MapEditor.Forms.CameraFOV;
using MapEditor.Forms.CameraSpeed;
using MapEditor.Forms.Checkers;
using MapEditor.Forms.CreateMimimap;
using MapEditor.Forms.FactionEditor;
using MapEditor.Forms.Game;
using MapEditor.Forms.Groups;
using MapEditor.Forms.HiddenObjects;
using MapEditor.Forms.HSVColorEditor;
using MapEditor.Forms.ImportCues;
using MapEditor.Forms.Layers;
using MapEditor.Forms.LightEditor;
using MapEditor.Forms.Log;
using MapEditor.Forms.Minimap;
using MapEditor.Forms.MobScripts;
using MapEditor.Forms.MobTable;
using MapEditor.Forms.ModelViewer;
using MapEditor.Forms.MultiObjectBrowser;
using MapEditor.Forms.ObjectEditor;
using MapEditor.Forms.ObjectSetBrowser;
using MapEditor.Forms.Property;
using MapEditor.Forms.PropertyControl;
using MapEditor.Forms.Quests;
using MapEditor.Forms.Quests.QuickObjectGenerator;
using MapEditor.Forms.RoadParamsBrowser;
using MapEditor.Forms.RouteObjectBrowser;
using MapEditor.Forms.ScriptEditor;
using MapEditor.Forms.ServerLuncher;
using MapEditor.Forms.SoundStaticObject;
using MapEditor.Forms.Time;
using MapEditor.Forms.Tool;
using MapEditor.Forms.UnselectableObjects;
using MapEditor.Forms.Water;
using MapEditor.Map;
using MapEditor.Map.DataProviders;
using MapEditor.Map.Dialogs;
using MapEditor.Map.MapCheckers;
using MapEditor.Map.MapObjects;
using MapEditor.Map.SaveLoad.DataSources;
using MapEditor.Map.States;
using MapEditor.Properties;
using MapEditor.Resources.Images;
using MapEditor.Resources.Strings;
using MapEditor.Scene;
using ModelEditor.Forms.Main;
using Operations;
using Tools.BaseForm;
using Tools.DragAndDrop;
using Tools.Geometry;
using Tools.InputState;
using Tools.ItemDataContainer;
using Tools.MainState;
using Tools.PropertyControl;
using Tools.RecentList;
using Tools.Statusbar;
using Tools.ViewFormState;
using Tools.WindowParams;

namespace MapEditor
{
	// Token: 0x020000AD RID: 173
	public partial class MainForm : Form
	{
		// Token: 0x060007FF RID: 2047 RVA: 0x0003EC8D File Offset: 0x0003DC8D
		private void CreateRecentList()
		{
			this.mapRecentListDataMiner = new MainForm.MapRecentItemDataMiner();
			this.mapRecentList = new RecentList(EditorEnvironment.EditorFormsFolder + "OpenMap_RecentList.xml", this.subMenuMapRecent, this.mapRecentListDataMiner);
			this.mapRecentList.LoadRecentList();
		}

		// Token: 0x06000800 RID: 2048 RVA: 0x0003ECCB File Offset: 0x0003DCCB
		private void DestroyRecentList()
		{
			this.mapRecentList.SaveRecentList();
			this.mapRecentList = null;
			this.mapRecentListDataMiner = null;
		}

		// Token: 0x06000801 RID: 2049 RVA: 0x0003ECE8 File Offset: 0x0003DCE8
		private void CreateScene()
		{
			this.editorScene = new EditorScene();
			this.editorScene.Create(this, 2);
			this.editorViewID = this.editorScene.CreateView(this);
			this.UpdateSceneAspect();
			this.editorSceneParams = Serializer.Load<EditorSceneParams>(EditorEnvironment.EditorFormsFolder + "EditorSceneParams.xml");
			if (this.editorSceneParams == null)
			{
				this.editorSceneParams = new EditorSceneParams();
			}
			this.editorSceneState = new EditorSceneState(this.MainFormContext);
			this.stateContainer.BindState(this.editorSceneState);
			this.editorSceneReadyForUpdate = true;
		}

		// Token: 0x06000802 RID: 2050 RVA: 0x0003ED7C File Offset: 0x0003DD7C
		private void DestroyScene()
		{
			Serializer.Save(EditorEnvironment.EditorFormsFolder + "EditorSceneParams.xml", this.editorSceneParams, false);
			if (this.editorSceneState != null)
			{
				this.stateContainer.UnbindState(this.editorSceneState);
				this.editorSceneState.Destroy();
				this.editorSceneState = null;
			}
			if (this.editorScene != null)
			{
				this.editorScene.Destroy();
				this.editorScene = null;
			}
			this.editorSceneReadyForUpdate = false;
		}

		// Token: 0x06000803 RID: 2051 RVA: 0x0003EDF1 File Offset: 0x0003DDF1
		private void UpdateScene()
		{
			if (this.editorScene != null && this.editorSceneReadyForUpdate && !Logger.AssertInFly())
			{
				this.editorSceneReadyForUpdate = false;
				this.editorScene.Step(this.editorViewID, true);
				this.editorSceneReadyForUpdate = true;
			}
		}

		// Token: 0x06000804 RID: 2052 RVA: 0x0003EE2C File Offset: 0x0003DE2C
		private void UpdateSceneAspect()
		{
			if (this.editorScene != null && base.ClientRectangle.Width != 0 && this.editorScene != null)
			{
				this.editorScene.SetAspect(this.editorViewID, (float)base.ClientRectangle.Height * 1f / (float)base.ClientRectangle.Width);
				this.UpdateScene();
			}
		}

		// Token: 0x06000805 RID: 2053 RVA: 0x0003EE98 File Offset: 0x0003DE98
		private void CreateBinds()
		{
			if (this.stateContainer != null)
			{
				if (Directory.Exists(EditorEnvironment.EditorFolder + "Binds"))
				{
					TextReader fileReader = new StreamReader(EditorEnvironment.EditorFolder + "Binds/Accelerators.cfg");
					this.accelerators.AddConfig(fileReader.ReadToEnd());
				}
				if (Directory.Exists(EditorEnvironment.EditorFolder + "Binds"))
				{
					TextReader fileReader2 = new StreamReader(EditorEnvironment.EditorFolder + "Binds/Events.cfg");
					this.events.AddConfig(fileReader2.ReadToEnd());
				}
			}
		}

		// Token: 0x06000806 RID: 2054 RVA: 0x0003EF28 File Offset: 0x0003DF28
		private void CreateStateContainer()
		{
			this.stateContainer = new StateContainer();
			this.accelerators = new Accelerators(new EventHandler(this.stateContainer.EventHandler));
			this.events = new Events(new EventHandler(this.stateContainer.EventHandler));
			this.CreateBinds();
			this.stateContainer.Bind(this);
			this.events.Bind(this);
			this.accelerators.Bind(this);
			this.stateContainer.Events = this.events;
			this.stateContainer.Accelerators = this.accelerators;
			EditorEnvironment.StateContainer = this.stateContainer;
		}

		// Token: 0x06000807 RID: 2055 RVA: 0x0003EFD0 File Offset: 0x0003DFD0
		private void CreateStates()
		{
			this.stateContainer.BindState(this.mainFormState);
			this.stateContainer.BindState(this.viewFormState);
			this.stateContainer.BindState(this.viewState);
			this.stateContainer.BindState(this.operationState);
		}

		// Token: 0x06000808 RID: 2056 RVA: 0x0003F021 File Offset: 0x0003E021
		private void CreateMapState()
		{
			this.mapState = new MapState(this.MainFormContext);
			this.stateContainer.BindState(this.mapState);
		}

		// Token: 0x06000809 RID: 2057 RVA: 0x0003F045 File Offset: 0x0003E045
		private void DestroyMapState()
		{
			this.stateContainer.UnbindState(this.mapState);
		}

		// Token: 0x0600080A RID: 2058 RVA: 0x0003F058 File Offset: 0x0003E058
		private void DestroyStates()
		{
			this.stateContainer.UnbindState(this.mainFormState);
			this.stateContainer.UnbindState(this.viewState);
			this.stateContainer.UnbindState(this.viewFormState);
			this.stateContainer.UnbindState(this.operationState);
		}

		// Token: 0x0600080B RID: 2059 RVA: 0x0003F0A9 File Offset: 0x0003E0A9
		private void DestroyStateContainer()
		{
			this.accelerators.Unbind();
			this.events.Unbind();
			this.stateContainer.Unbind();
			EditorEnvironment.StateContainer = null;
		}

		// Token: 0x0600080C RID: 2060 RVA: 0x0003F0D2 File Offset: 0x0003E0D2
		private void ShowSplashScreen()
		{
			if (!Debugger.IsAttached)
			{
				if (this.splashScreenForm == null)
				{
					this.splashScreenForm = new SplashScreenForm();
				}
				if (this.splashScreenForm != null)
				{
					this.splashScreenForm.Show();
				}
			}
		}

		// Token: 0x0600080D RID: 2061 RVA: 0x0003F101 File Offset: 0x0003E101
		private void HideSplashScreen()
		{
			if (this.splashScreenForm != null)
			{
				this.splashScreenForm.Close();
				this.splashScreenForm = null;
			}
		}

		// Token: 0x17000147 RID: 327
		// (get) Token: 0x0600080E RID: 2062 RVA: 0x0003F11D File Offset: 0x0003E11D
		// (set) Token: 0x0600080F RID: 2063 RVA: 0x0003F139 File Offset: 0x0003E139
		private bool MainToolbarVisible
		{
			get
			{
				return FormParamsSaver.IntToBool(this.formParamsSaver.FormParams.GetInt(MainForm.formParamsSaver_mainToolbarIntIndex));
			}
			set
			{
				this.formParamsSaver.FormParams.SetInt(MainForm.formParamsSaver_mainToolbarIntIndex, FormParamsSaver.BoolToInt(value));
			}
		}

		// Token: 0x17000148 RID: 328
		// (get) Token: 0x06000810 RID: 2064 RVA: 0x0003F156 File Offset: 0x0003E156
		// (set) Token: 0x06000811 RID: 2065 RVA: 0x0003F172 File Offset: 0x0003E172
		private bool ToolsToolbarVisible
		{
			get
			{
				return FormParamsSaver.IntToBool(this.formParamsSaver.FormParams.GetInt(MainForm.formParamsSaver_toolsToolbarIntIndex));
			}
			set
			{
				this.formParamsSaver.FormParams.SetInt(MainForm.formParamsSaver_toolsToolbarIntIndex, FormParamsSaver.BoolToInt(value));
			}
		}

		// Token: 0x17000149 RID: 329
		// (get) Token: 0x06000812 RID: 2066 RVA: 0x0003F18F File Offset: 0x0003E18F
		// (set) Token: 0x06000813 RID: 2067 RVA: 0x0003F1AB File Offset: 0x0003E1AB
		private bool StatusbarVisible
		{
			get
			{
				return FormParamsSaver.IntToBool(this.formParamsSaver.FormParams.GetInt(MainForm.formParamsSaver_statusbarIntIndex));
			}
			set
			{
				this.formParamsSaver.FormParams.SetInt(MainForm.formParamsSaver_statusbarIntIndex, FormParamsSaver.BoolToInt(value));
			}
		}

		// Token: 0x1700014A RID: 330
		// (get) Token: 0x06000814 RID: 2068 RVA: 0x0003F1C8 File Offset: 0x0003E1C8
		// (set) Token: 0x06000815 RID: 2069 RVA: 0x0003F1E4 File Offset: 0x0003E1E4
		private bool StatusbarHelpVisible
		{
			get
			{
				return FormParamsSaver.IntToBool(this.formParamsSaver.FormParams.GetInt(MainForm.formParamsSaver_statusbarHelpIntIndex));
			}
			set
			{
				this.formParamsSaver.FormParams.SetInt(MainForm.formParamsSaver_statusbarHelpIntIndex, FormParamsSaver.BoolToInt(value));
			}
		}

		// Token: 0x06000816 RID: 2070 RVA: 0x0003F204 File Offset: 0x0003E204
		private void CreateForms()
		{
			this.cameraFOVForm = new CameraFOVForm();
			this.cameraFOVForm.FovChanged += this.SetCameraFOV;
			this.cameraSpeedForm = new CameraSpeedForm();
			this.cameraSpeedForm.SpeedChanged += this.SetCameraSpeed;
			this.timeForm = new TimeForm(this.MainFormContext);
			this.viewFormState = new ViewFormState(this, this.formParamsSaver, MainForm.formParamsSaver_formsIntIndex);
			this.viewFormState.AddForm(new MinimapForm(this.MainFormContext), "toggle_minimap");
			this.viewFormState.AddForm(new ToolForm(this.MainFormContext), "toggle_tool");
			this.viewFormState.AddForm(new ObjectEditorForm(this.MainFormContext), "toggle_object_editor");
			this.viewFormState.AddForm(new PropertiesForm(this.MainFormContext), "toggle_properties");
			this.viewFormState.AddForm(new GroupsForm(this.MainFormContext), "toggle_list");
			this.viewFormState.AddForm(new LogForm(this.MainFormContext), "toggle_log");
			this.viewFormState.AddForm(new LayersForm(this.MainFormContext), "toggle_layers");
			this.viewFormState.AddForm(new QuestDiagramForm(this.MainFormContext), "toggle_quest_diagram");
			this.viewFormState.AddForm(new QuestImportForm(this.MainFormContext), "toggle_import_quest_texts");
			this.viewFormState.AddForm(new CreateNPCForm(this.MainFormContext), "toggle_create_NPC");
			this.viewFormState.AddForm(new CreateMobForm(this.MainFormContext), "toggle_create_mob");
			this.viewFormState.AddForm(new CreateDeviceForm(this.MainFormContext), "toggle_create_resource");
			this.viewFormState.AddForm(new CreateQuestItemForm(this.MainFormContext), "toggle_create_quest_item");
			this.viewFormState.AddForm(new PropertyControlForm(this.MainFormContext), "toggle_property_control");
			this.viewFormState.AddForm(new LightEditorForm(this.MainFormContext), "toggle_light");
			this.viewFormState.AddForm(new ModelViewerForm(this.MainFormContext), "toggle_model_viewer");
			this.viewFormState.AddForm(new FactionEditorForm(this.MainFormContext), "toggle_faction_editor");
			this.viewFormState.AddForm(new MultiObjectBrowserForm(this.MainFormContext), "toggle_multiobject_browser");
			this.viewFormState.AddForm(new ObjectSetBrowserForm(this.MainFormContext), "toggle_object_set_browser");
			this.viewFormState.AddForm(new ScriptEditorForm(this.MainFormContext), "toggle_script_editor");
			this.viewFormState.AddForm(new RoadParamsBrowserForm(this.MainFormContext), "toggle_road_params_browser");
			this.viewFormState.AddForm(new CheckersForm(this.MainFormContext), "toggle_checkers");
			this.viewFormState.AddForm(new GameForm(this.MainFormContext), "toggle_game", true);
			this.viewFormState.AddForm(new RouteObjectBrowserForm(this.MainFormContext), "toggle_route_object_browser");
			this.viewFormState.AddForm(new MobScriptsForm(this.MainFormContext), "toggle_visual_mob_scripts");
			this.viewFormState.AddForm(new WaterEditorForm(this.MainFormContext), "toggle_water_editor");
			this.viewFormState.AddForm(new HSVColorEditorForm(this.MainFormContext), "toggle_hsv_color_editor");
			this.viewFormState.AddForm(new MobTableForm(this.MainFormContext), "toggle_vendors_table");
			this.viewFormState.AddForm(new ImportCuesForm(this.MainFormContext), "toggle_import_cues");
			this.viewFormState.AddForm(new UnselectableObjectsForm(this.MainFormContext), "toggle_unselectable_objects");
			this.viewFormState.AddForm(new ServerLuncherForm(this.MainFormContext), "toggle_server_lunch");
			this.viewFormState.AddForm(new SoundStaticObjectForm(this.MainFormContext), "toggle_create_sound_static_object");
			this.viewFormState.AddForm(new CreateMinimapForm(this.MainFormContext), "toggle_create_minimap");
			this.viewFormState.AddForm(new HiddenObjectsForm(this.MainFormContext), "toggle_hidden_objects");
			this.viewFormState.AddForm(new MainForm(false), "toggle_model_editor");
			this.viewState.AddMethod("toggle_main_toolbar", new Method(this.OnToggleMainToolbar));
			this.viewState.AddMethod("toggle_tools_toolbar", new Method(this.OnToggleToolsToolbar));
			this.viewState.AddMethod("toggle_statusbar", new Method(this.OnToggleStatusbar));
			this.viewState.AddMethod("toggle_statusbar_help", new Method(this.OnToggleStatusbarHelp));
			this.viewState.AddMethod("camera_slow", new Method(this.OnCameraSpeedSlow));
			this.viewState.AddMethod("camera_normal", new Method(this.OnCameraSpeedNormal));
			this.viewState.AddMethod("camera_fast", new Method(this.OnCameraSpeedFast));
			this.viewState.AddMethod("camera_custom", new Method(this.OnCameraSpeedCustom));
			this.viewState.AddMethod("camera_bounding", new Method(this.OnCameraBounding));
			this.viewState.AddMethod("camera_fov", new Method(this.OnCameraFOV));
			this.viewState.AddMethod("camera_speed", new Method(this.OnCameraSpeed));
			this.viewState.AddMethod("camera_fps", new Method(this.OnFPSCamera));
			this.viewState.AddMethod("camera_rts", new Method(this.OnRTSCamera));
			this.viewState.AddMethod("toggle_time", new Method(this.OnTime));
			this.viewState.AddMethod("toggle_sound", new Method(this.OnToggleSound));
			this.viewState.AddMethod("toggle_quests", new Method(this.OnToggleQuests));
			State state = this.viewState;
			state.EnterState = (State.ActivateEvent)Delegate.Combine(state.EnterState, new State.ActivateEvent(this.OnEnterViewFormsState));
		}

		// Token: 0x06000817 RID: 2071 RVA: 0x0003F814 File Offset: 0x0003E814
		private void DestroyForms()
		{
			if (this.cameraFOVForm != null)
			{
				this.cameraFOVForm.FovChanged -= this.SetCameraFOV;
			}
			if (this.cameraSpeedForm != null)
			{
				this.cameraSpeedForm.SpeedChanged -= this.SetCameraSpeed;
			}
			State state = this.viewState;
			state.EnterState = (State.ActivateEvent)Delegate.Remove(state.EnterState, new State.ActivateEvent(this.OnEnterViewFormsState));
			for (int index = 0; index < this.viewFormState.FormCount; index++)
			{
				Tools.BaseForm.BaseForm baseForm = this.GetCommonBaseForm(index);
				if (baseForm != null)
				{
					baseForm.AllowClose = true;
				}
			}
			this.viewFormState.DestroyForms();
		}

		// Token: 0x06000818 RID: 2072 RVA: 0x0003F8BC File Offset: 0x0003E8BC
		private void OnEnterViewFormsState(IState _viewFormsState)
		{
			this.UpdateMainToolbar();
			this.UpdateToolsToolbar();
			this.UpdateStatusbar();
			this.UpdateStatusbarHelp();
			this.UpdateCamera();
			this.UpdateCameraButtons();
			if (!this.context.FullDatabase)
			{
				this.viewState.SetMethodParams("toggle_layers", true, false, false, false);
				this.viewState.SetMethodParams("toggle_quests", true, false, false, false);
				this.viewState.SetMethodParams("toggle_quest_diagram", true, false, false, false);
				this.viewState.SetMethodParams("toggle_import_quest_texts", true, false, false, false);
				this.viewState.SetMethodParams("toggle_light", true, false, false, false);
				this.viewState.SetMethodParams("toggle_property_control", true, false, false, false);
			}
		}

		// Token: 0x06000819 RID: 2073 RVA: 0x0003F974 File Offset: 0x0003E974
		private void NormalizeCameraParams()
		{
			int cameraSpeed = this.formParamsSaver.FormParams.GetInt(MainForm.formParamsSaver_cameraSpeedIndex);
			if (cameraSpeed != MainForm.cameraSpeedSlow && cameraSpeed != MainForm.cameraSpeedNormal && cameraSpeed != MainForm.cameraSpeedFast)
			{
				this.formParamsSaver.FormParams.SetInt(MainForm.formParamsSaver_cameraSpeedIndex, -1);
			}
			double cameraSpeed2 = this.formParamsSaver.FormParams.GetDouble(MainForm.formParamsSaver_cameraSpeedDoubleIndex);
			if (cameraSpeed2 < (double)MainForm.cameraSpeedMinimal * 1.0 || cameraSpeed2 > (double)MainForm.cameraSpeedFast * 1.0)
			{
				this.formParamsSaver.FormParams.SetDouble(MainForm.formParamsSaver_cameraSpeedDoubleIndex, (double)MainForm.cameraSpeedNormal * 1.0);
			}
			int time = this.formParamsSaver.FormParams.GetInt(MainForm.formParamsSaver_timeIndex);
			if (time < 0 || time > 24)
			{
				this.formParamsSaver.FormParams.SetInt(MainForm.formParamsSaver_timeIndex, 12);
			}
		}

		// Token: 0x0600081A RID: 2074 RVA: 0x0003FA5C File Offset: 0x0003EA5C
		private void UpdateCamera()
		{
			this.NormalizeCameraParams();
			int @int = this.formParamsSaver.FormParams.GetInt(MainForm.formParamsSaver_cameraTypeIndex);
			string cameraType;
			if (@int == 0)
			{
				cameraType = "FPSType";
			}
			else
			{
				cameraType = "RTSType";
			}
			this.editorScene.SetCameraType(this.editorViewID, cameraType);
			int cameraSpeed = this.formParamsSaver.FormParams.GetInt(MainForm.formParamsSaver_cameraSpeedIndex);
			if (cameraSpeed == -1)
			{
				this.editorScene.SetCameraMoveSpeed(this.editorViewID, this.formParamsSaver.FormParams.GetDouble(MainForm.formParamsSaver_cameraSpeedDoubleIndex));
			}
			else
			{
				this.editorScene.SetCameraMoveSpeed(this.editorViewID, (double)cameraSpeed * 1.0);
			}
			this.editorScene.SetCameraBoundingMode(this.editorViewID, FormParamsSaver.IntToBool(this.formParamsSaver.FormParams.GetInt(MainForm.formParamsSaver_cameraBoundingIndex)));
			this.editorScene.SetCameraFOV(this.editorViewID, this.formParamsSaver.FormParams.GetDouble(MainForm.formParamsSaver_cameraFOVDoubleIndex));
			this.editorScene.SetCameraBounds(this.editorViewID, 0, 128, 0, 128, 0, 128);
			this.editorScene.SetTime((float)this.formParamsSaver.FormParams.GetInt(MainForm.formParamsSaver_timeIndex));
		}

		// Token: 0x0600081B RID: 2075 RVA: 0x0003FB9D File Offset: 0x0003EB9D
		private void UpdateMainToolbar()
		{
			if (this.MainToolbarVisible)
			{
				this.MainToolStrip.Show();
			}
			else
			{
				this.MainToolStrip.Hide();
			}
			this.viewState.SetMethodParams("toggle_main_toolbar", false, false, true, this.MainToolbarVisible);
		}

		// Token: 0x0600081C RID: 2076 RVA: 0x0003FBD8 File Offset: 0x0003EBD8
		private void UpdateToolsToolbar()
		{
			if (this.ToolsToolbarVisible)
			{
				this.ToolsToolStrip.Show();
			}
			else
			{
				this.ToolsToolStrip.Hide();
			}
			this.viewState.SetMethodParams("toggle_tools_toolbar", false, false, true, this.ToolsToolbarVisible);
		}

		// Token: 0x0600081D RID: 2077 RVA: 0x0003FC13 File Offset: 0x0003EC13
		private void UpdateStatusbar()
		{
			if (this.StatusbarVisible)
			{
				this.statusStrip.Show();
			}
			else
			{
				this.statusStrip.Hide();
			}
			this.viewState.SetMethodParams("toggle_statusbar", false, false, true, this.StatusbarVisible);
		}

		// Token: 0x0600081E RID: 2078 RVA: 0x0003FC4E File Offset: 0x0003EC4E
		private void UpdateStatusbarHelp()
		{
			if (this.StatusbarHelpVisible)
			{
				this.statusHelp.Show();
			}
			else
			{
				this.statusHelp.Hide();
			}
			this.viewState.SetMethodParams("toggle_statusbar_help", false, false, true, this.StatusbarHelpVisible);
		}

		// Token: 0x0600081F RID: 2079 RVA: 0x0003FC89 File Offset: 0x0003EC89
		private void ToggleMainToolbar()
		{
			this.MainToolbarVisible = !this.MainToolbarVisible;
			this.UpdateMainToolbar();
		}

		// Token: 0x06000820 RID: 2080 RVA: 0x0003FCA0 File Offset: 0x0003ECA0
		private void ToggleToolsToolbar()
		{
			this.ToolsToolbarVisible = !this.ToolsToolbarVisible;
			this.UpdateToolsToolbar();
		}

		// Token: 0x06000821 RID: 2081 RVA: 0x0003FCB7 File Offset: 0x0003ECB7
		private void ToggleStatusbar()
		{
			this.StatusbarVisible = !this.StatusbarVisible;
			this.UpdateStatusbar();
		}

		// Token: 0x06000822 RID: 2082 RVA: 0x0003FCCE File Offset: 0x0003ECCE
		private void ToggleStatusbarHelp()
		{
			this.StatusbarHelpVisible = !this.StatusbarHelpVisible;
			this.UpdateStatusbarHelp();
		}

		// Token: 0x06000823 RID: 2083 RVA: 0x0003FCE5 File Offset: 0x0003ECE5
		private void OnToggleMainToolbar(MethodArgs methodArgs)
		{
			this.ToggleMainToolbar();
		}

		// Token: 0x06000824 RID: 2084 RVA: 0x0003FCED File Offset: 0x0003ECED
		private void OnToggleToolsToolbar(MethodArgs methodArgs)
		{
			this.ToggleToolsToolbar();
		}

		// Token: 0x06000825 RID: 2085 RVA: 0x0003FCF5 File Offset: 0x0003ECF5
		private void OnToggleStatusbar(MethodArgs methodArgs)
		{
			this.ToggleStatusbar();
		}

		// Token: 0x06000826 RID: 2086 RVA: 0x0003FCFD File Offset: 0x0003ECFD
		private void OnToggleStatusbarHelp(MethodArgs methodArgs)
		{
			this.ToggleStatusbarHelp();
		}

		// Token: 0x06000827 RID: 2087 RVA: 0x0003FD08 File Offset: 0x0003ED08
		private void OnCameraSpeedSlow(MethodArgs methodArgs)
		{
			this.editorScene.SetCameraMoveSpeed(this.editorViewID, (double)MainForm.cameraSpeedSlow * 1.0);
			this.formParamsSaver.FormParams.SetInt(MainForm.formParamsSaver_cameraSpeedIndex, MainForm.cameraSpeedSlow);
			this.UpdateCameraButtons();
		}

		// Token: 0x06000828 RID: 2088 RVA: 0x0003FD58 File Offset: 0x0003ED58
		private void OnCameraSpeedNormal(MethodArgs methodArgs)
		{
			this.editorScene.SetCameraMoveSpeed(this.editorViewID, (double)MainForm.cameraSpeedNormal * 1.0);
			this.formParamsSaver.FormParams.SetInt(MainForm.formParamsSaver_cameraSpeedIndex, MainForm.cameraSpeedNormal);
			this.UpdateCameraButtons();
		}

		// Token: 0x06000829 RID: 2089 RVA: 0x0003FDA8 File Offset: 0x0003EDA8
		private void OnCameraSpeedFast(MethodArgs methodArgs)
		{
			this.editorScene.SetCameraMoveSpeed(this.editorViewID, (double)MainForm.cameraSpeedFast * 1.0);
			this.formParamsSaver.FormParams.SetInt(MainForm.formParamsSaver_cameraSpeedIndex, MainForm.cameraSpeedFast);
			this.UpdateCameraButtons();
		}

		// Token: 0x0600082A RID: 2090 RVA: 0x0003FDF8 File Offset: 0x0003EDF8
		private void OnCameraSpeedCustom(MethodArgs methodArgs)
		{
			this.editorScene.SetCameraMoveSpeed(this.editorViewID, this.formParamsSaver.FormParams.GetDouble(MainForm.formParamsSaver_cameraSpeedDoubleIndex));
			this.formParamsSaver.FormParams.SetInt(MainForm.formParamsSaver_cameraSpeedIndex, -1);
			this.UpdateCameraButtons();
		}

		// Token: 0x0600082B RID: 2091 RVA: 0x0003FE48 File Offset: 0x0003EE48
		private void OnCameraBounding(MethodArgs methodArgs)
		{
			if (FormParamsSaver.IntToBool(this.formParamsSaver.FormParams.GetInt(MainForm.formParamsSaver_cameraBoundingIndex)))
			{
				this.formParamsSaver.FormParams.SetInt(MainForm.formParamsSaver_cameraBoundingIndex, FormParamsSaver.BoolToInt(false));
			}
			else
			{
				this.formParamsSaver.FormParams.SetInt(MainForm.formParamsSaver_cameraBoundingIndex, FormParamsSaver.BoolToInt(true));
			}
			this.editorScene.SetCameraBoundingMode(this.editorViewID, FormParamsSaver.IntToBool(this.formParamsSaver.FormParams.GetInt(MainForm.formParamsSaver_cameraBoundingIndex)));
			this.UpdateCameraButtons();
		}

		// Token: 0x0600082C RID: 2092 RVA: 0x0003FEDC File Offset: 0x0003EEDC
		private void UpdateCameraButtons()
		{
			if (this.context != null && this.context.StateContainer != null)
			{
				string cameraType;
				this.editorScene.GetCameraType(this.editorViewID, out cameraType);
				string a;
				if ((a = cameraType) != null)
				{
					if (!(a == "RTSType"))
					{
						if (a == "FPSType")
						{
							this.context.StateContainer.SetMethodParams("camera_fps", false, false, true, true);
							this.context.StateContainer.SetMethodParams("camera_rts", false, false, true, false);
						}
					}
					else
					{
						this.context.StateContainer.SetMethodParams("camera_fps", false, false, true, false);
						this.context.StateContainer.SetMethodParams("camera_rts", false, false, true, true);
					}
				}
				if (this.editorScene.GetCameraBoundingMode(this.editorViewID))
				{
					this.context.StateContainer.SetMethodParams("camera_bounding", false, false, true, true);
				}
				else
				{
					this.context.StateContainer.SetMethodParams("camera_bounding", false, false, true, false);
				}
				int cameraSpeed = this.formParamsSaver.FormParams.GetInt(MainForm.formParamsSaver_cameraSpeedIndex);
				this.context.StateContainer.SetMethodParams("camera_slow", false, false, true, false);
				this.context.StateContainer.SetMethodParams("camera_normal", false, false, true, false);
				this.context.StateContainer.SetMethodParams("camera_fast", false, false, true, false);
				this.context.StateContainer.SetMethodParams("camera_custom", false, false, true, false);
				if (cameraSpeed == MainForm.cameraSpeedSlow)
				{
					this.context.StateContainer.SetMethodParams("camera_slow", false, false, true, true);
					return;
				}
				if (cameraSpeed == MainForm.cameraSpeedNormal)
				{
					this.context.StateContainer.SetMethodParams("camera_normal", false, false, true, true);
					return;
				}
				if (cameraSpeed == MainForm.cameraSpeedFast)
				{
					this.context.StateContainer.SetMethodParams("camera_fast", false, false, true, true);
					return;
				}
				if (cameraSpeed == -1)
				{
					this.context.StateContainer.SetMethodParams("camera_custom", false, false, true, true);
				}
			}
		}

		// Token: 0x0600082D RID: 2093 RVA: 0x000400E8 File Offset: 0x0003F0E8
		private void OnCameraFOV(MethodArgs methodArgs)
		{
			this.cameraFOVForm.Show();
			this.cameraFOVForm.SetFOV(this.formParamsSaver.FormParams.GetDouble(MainForm.formParamsSaver_cameraFOVDoubleIndex));
			this.cameraFOVForm.SetBounds(Cursor.Position.X - this.cameraFOVForm.Size.Width / 2, Cursor.Position.Y - this.cameraFOVForm.Size.Height / 2, 0, 0, BoundsSpecified.Location);
		}

		// Token: 0x0600082E RID: 2094 RVA: 0x00040174 File Offset: 0x0003F174
		private void OnCameraSpeed(MethodArgs methodArgs)
		{
			this.cameraSpeedForm.Show();
			this.cameraSpeedForm.SetSpeed(this.formParamsSaver.FormParams.GetDouble(MainForm.formParamsSaver_cameraSpeedDoubleIndex));
			this.cameraSpeedForm.SetBounds(Cursor.Position.X - this.cameraSpeedForm.Size.Width / 2, Cursor.Position.Y - this.cameraSpeedForm.Size.Height / 2, 0, 0, BoundsSpecified.Location);
		}

		// Token: 0x0600082F RID: 2095 RVA: 0x00040200 File Offset: 0x0003F200
		private void OnFPSCamera(MethodArgs methodArgs)
		{
			this.formParamsSaver.FormParams.SetInt(MainForm.formParamsSaver_cameraTypeIndex, 0);
			this.editorScene.SetCameraType(this.editorViewID, "FPSType");
			this.UpdateCameraButtons();
		}

		// Token: 0x06000830 RID: 2096 RVA: 0x00040234 File Offset: 0x0003F234
		private void OnRTSCamera(MethodArgs methodArgs)
		{
			this.formParamsSaver.FormParams.SetInt(MainForm.formParamsSaver_cameraTypeIndex, 1);
			this.editorScene.SetCameraType(this.editorViewID, "RTSType");
			this.UpdateCameraButtons();
		}

		// Token: 0x06000831 RID: 2097 RVA: 0x00040268 File Offset: 0x0003F268
		private void OnTime(MethodArgs methodArgs)
		{
			if (this.editorScene != null)
			{
				this.timeForm.Show();
				this.timeForm.SetTime(this.editorScene.GetTime());
				this.timeForm.SetBounds(Cursor.Position.X - this.timeForm.Size.Width / 2, Cursor.Position.Y - this.timeForm.Size.Height / 2, 0, 0, BoundsSpecified.Location);
			}
		}

		// Token: 0x06000832 RID: 2098 RVA: 0x000402F4 File Offset: 0x0003F2F4
		private void OnToggleQuests(MethodArgs methodArgs)
		{
			QuestEditorZoneForm questEditorZoneForm = new QuestEditorZoneForm();
			if (questEditorZoneForm.ShowDialog(this) == DialogResult.OK)
			{
				this.context.QuestEnvironment.QuestEditor.Load(questEditorZoneForm.Filter, questEditorZoneForm.Zone);
			}
		}

		// Token: 0x06000833 RID: 2099 RVA: 0x00040332 File Offset: 0x0003F332
		public void SetCameraFOV(double fov)
		{
			this.editorScene.SetCameraFOV(this.editorViewID, fov);
			this.formParamsSaver.FormParams.SetDouble(MainForm.formParamsSaver_cameraFOVDoubleIndex, fov);
		}

		// Token: 0x06000834 RID: 2100 RVA: 0x0004035C File Offset: 0x0003F35C
		public void SetCameraSpeed(double speed)
		{
			this.formParamsSaver.FormParams.SetDouble(MainForm.formParamsSaver_cameraSpeedDoubleIndex, speed);
			if (this.formParamsSaver.FormParams.GetInt(MainForm.formParamsSaver_cameraSpeedIndex) == -1)
			{
				this.editorScene.SetCameraMoveSpeed(this.editorViewID, speed);
			}
		}

		// Token: 0x06000835 RID: 2101 RVA: 0x000403A9 File Offset: 0x0003F3A9
		public float GetTimeFromParams()
		{
			return (float)this.formParamsSaver.FormParams.GetDouble(MainForm.formParamsSaver_timeIndex);
		}

		// Token: 0x06000836 RID: 2102 RVA: 0x000403C1 File Offset: 0x0003F3C1
		public void SetTimeToParams(float time)
		{
			this.formParamsSaver.FormParams.SetDouble(MainForm.formParamsSaver_timeIndex, (double)time);
		}

		// Token: 0x06000837 RID: 2103 RVA: 0x000403DC File Offset: 0x0003F3DC
		public void OnToggleSound(MethodArgs methodArgs)
		{
			int mute = 1 - this.formParamsSaver.FormParams.GetInt(MainForm.formParamsSaver_muteIndex);
			this.formParamsSaver.FormParams.SetInt(MainForm.formParamsSaver_muteIndex, mute);
			this.editorScene.SetMute(mute > 0);
			this.viewState.SetMethodParams("toggle_sound", false, false, true, 1 - mute > 0);
		}

		// Token: 0x06000838 RID: 2104 RVA: 0x0004043F File Offset: 0x0003F43F
		public MapEditor.Forms.Base.BaseForm GetBaseForm(int index)
		{
			return this.viewFormState.GetForm(index) as MapEditor.Forms.Base.BaseForm;
		}

		// Token: 0x06000839 RID: 2105 RVA: 0x00040452 File Offset: 0x0003F452
		public Tools.BaseForm.BaseForm GetCommonBaseForm(int index)
		{
			return this.viewFormState.GetForm(index) as Tools.BaseForm.BaseForm;
		}

		// Token: 0x0600083A RID: 2106 RVA: 0x00040465 File Offset: 0x0003F465
		public void ShowBaseForm(int index, bool visible)
		{
			this.viewFormState.ShowForm(index, visible);
		}

		// Token: 0x0600083B RID: 2107 RVA: 0x00040474 File Offset: 0x0003F474
		private void CreateOperationContainer()
		{
			this.defaultUndoTooltip = this.toolbarItemUndo.ToolTipText;
			this.defaultRedoTooltip = this.toolbarItemRedo.ToolTipText;
			this.operationState.AddMethod("undo", new Method(this.OnUndo));
			this.operationState.AddMethod("redo", new Method(this.OnRedo));
			State state = this.operationState;
			state.EnterState = (State.ActivateEvent)Delegate.Combine(state.EnterState, new State.ActivateEvent(this.OnEnterOperationState));
			this.operationContainer.BuffersChanged += this.OnOperationContainerEvent;
			this.operationContainer.UndoInvoked += this.OnOperationContainerEvent;
			this.operationContainer.RedoInvoked += this.OnOperationContainerEvent;
		}

		// Token: 0x0600083C RID: 2108 RVA: 0x00040548 File Offset: 0x0003F548
		private void DestroyOperationContainer()
		{
			State state = this.operationState;
			state.EnterState = (State.ActivateEvent)Delegate.Remove(state.EnterState, new State.ActivateEvent(this.OnEnterOperationState));
			this.operationContainer.BuffersChanged -= this.OnOperationContainerEvent;
			this.operationContainer.UndoInvoked -= this.OnOperationContainerEvent;
			this.operationContainer.RedoInvoked -= this.OnOperationContainerEvent;
		}

		// Token: 0x0600083D RID: 2109 RVA: 0x000405C4 File Offset: 0x0003F5C4
		private void UpdateOperationStateMethodParams()
		{
			if (this.operationContainer.CanUndo)
			{
				this.toolbarItemUndo.ToolTipText = string.Format(Strings.UNDO_CHANGES, this.operationContainer.UndoDescription);
			}
			else
			{
				this.toolbarItemUndo.ToolTipText = this.defaultUndoTooltip;
			}
			if (this.operationContainer.CanRedo)
			{
				this.toolbarItemRedo.ToolTipText = string.Format(Strings.REDO_CHANGES, this.operationContainer.RedoDescription);
			}
			else
			{
				this.toolbarItemRedo.ToolTipText = this.defaultRedoTooltip;
			}
			this.operationState.SetMethodParams("undo", true, this.operationContainer.CanUndo, false, false);
			this.operationState.SetMethodParams("redo", true, this.operationContainer.CanRedo, false, false);
		}

		// Token: 0x0600083E RID: 2110 RVA: 0x0004068D File Offset: 0x0003F68D
		private void OnEnterOperationState(IState _operationState)
		{
			this.UpdateOperationStateMethodParams();
		}

		// Token: 0x0600083F RID: 2111 RVA: 0x00040695 File Offset: 0x0003F695
		private void OnOperationContainerEvent(OperationContainer _operationContainer)
		{
			this.UpdateOperationStateMethodParams();
		}

		// Token: 0x06000840 RID: 2112 RVA: 0x0004069D File Offset: 0x0003F69D
		private void OnUndo(MethodArgs methodArgs)
		{
			this.operationContainer.Undo(1);
			this.context.EditorScene.SynchronizeObjects(false);
		}

		// Token: 0x06000841 RID: 2113 RVA: 0x000406BD File Offset: 0x0003F6BD
		private void OnRedo(MethodArgs methodArgs)
		{
			this.operationContainer.Redo(1);
			this.context.EditorScene.SynchronizeObjects(false);
		}

		// Token: 0x1400002A RID: 42
		// (add) Token: 0x06000842 RID: 2114 RVA: 0x000406DD File Offset: 0x0003F6DD
		// (remove) Token: 0x06000843 RID: 2115 RVA: 0x000406F6 File Offset: 0x0003F6F6
		private event MainForm.Context.CloseFormEvent CloseMainForm;

		// Token: 0x1700014B RID: 331
		// (get) Token: 0x06000844 RID: 2116 RVA: 0x0004070F File Offset: 0x0003F70F
		public MainForm.Context MainFormContext
		{
			get
			{
				if (this.context == null)
				{
					this.context = new MainForm.Context(this);
				}
				return this.context;
			}
		}

		// Token: 0x06000847 RID: 2119 RVA: 0x00049DB1 File Offset: 0x00048DB1
		private void CreateQuests()
		{
			this.quests = new QuestEnvironment(this.context);
		}

		// Token: 0x06000848 RID: 2120 RVA: 0x00049DC4 File Offset: 0x00048DC4
		private void DestroyQuests()
		{
			this.quests.Destroy();
			this.quests = null;
		}

		// Token: 0x06000849 RID: 2121 RVA: 0x00049DD8 File Offset: 0x00048DD8
		private void CreateMainState()
		{
			this.mainState = new MainState();
			this.mainState.Create(this.stateContainer, EditorEnvironment.EditorFormsFolder + "MainStateParams.xml");
		}

		// Token: 0x0600084A RID: 2122 RVA: 0x00049E05 File Offset: 0x00048E05
		private void DestroyMainState()
		{
			this.mainState.Destroy();
			this.mainState = null;
		}

		// Token: 0x0600084B RID: 2123 RVA: 0x00049E19 File Offset: 0x00048E19
		private void CreateItemDataContainer()
		{
			this.itemDataContainer = new ItemDataContainer(EditorEnvironment.EditorFolder, false);
			this.CreateItemDataMiners();
			this.itemDataContainerReadyForUpdate = true;
		}

		// Token: 0x0600084C RID: 2124 RVA: 0x00049E39 File Offset: 0x00048E39
		private void DestroyItemDataContainer()
		{
			this.DestroyMapSpecificItemDataMiners();
			this.DestroyItemDataMiners();
			if (this.itemDataContainer != null)
			{
				this.itemDataContainer.Destroy();
				this.itemDataContainer = null;
			}
			this.itemDataContainerReadyForUpdate = false;
		}

		// Token: 0x0600084D RID: 2125 RVA: 0x00049E68 File Offset: 0x00048E68
		private void UpdateItemDataContainer()
		{
			if (this.itemDataContainer != null && this.itemDataContainerReadyForUpdate)
			{
				this.itemDataContainerReadyForUpdate = false;
				this.itemDataContainer.Step();
				this.itemDataContainerReadyForUpdate = true;
			}
		}

		// Token: 0x0600084E RID: 2126 RVA: 0x00049E94 File Offset: 0x00048E94
		private void CreateItemDataMiners()
		{
			if (this.itemDataContainer != null)
			{
				StaticObjectItemDataMiner staticObjectItemDataMiner = new StaticObjectItemDataMiner();
				this.itemDataContainer.AddDataMiner(staticObjectItemDataMiner);
				MultiObjectItemDataMiner multiObjectItemDataMiner = new MultiObjectItemDataMiner();
				this.itemDataContainer.AddDataMiner(multiObjectItemDataMiner);
				ObjectSetItemDataMiner objectSetItemDataMiner = new ObjectSetItemDataMiner();
				this.itemDataContainer.AddDataMiner(objectSetItemDataMiner);
				DBObjectItemDataMiner startPointItemDataMiner = new DBObjectItemDataMiner(StartPoint.CharacterDBType, false, Images.start_point);
				this.itemDataContainer.AddDataMiner(startPointItemDataMiner);
				DBObjectItemDataMiner spawnTableItemDataMiner = new DBObjectItemDataMiner(SpawnPoint.SpawnTableDBType, false, Images.spawn_point);
				this.itemDataContainer.AddDataMiner(spawnTableItemDataMiner);
				DBObjectItemDataMiner mobWorldItemDataMiner = new DBObjectItemDataMiner(SpawnPoint.MobWorldDBType, false, Images.mob_spawn);
				this.itemDataContainer.AddDataMiner(mobWorldItemDataMiner);
				DBObjectItemDataMiner scriptZoneItemDataMiner = new DBObjectItemDataMiner(ScriptArea.ScriptZoneDBType, false, Images.script_zone);
				this.itemDataContainer.AddDataMiner(scriptZoneItemDataMiner);
				DBObjectItemDataMiner tourDataMiner = new DBObjectItemDataMiner(RoutePoint.TourDBType, false, Images.route);
				this.itemDataContainer.AddDataMiner(tourDataMiner);
				DBObjectItemDataMiner deviceItemDataMiner = new DBObjectItemDataMiner(PermanentDevice.DeviceDBType, true, Images.permanent_device);
				this.itemDataContainer.AddDataMiner(deviceItemDataMiner);
				DBObjectItemDataMiner patrolScriptItemDataMiner = new DBObjectItemDataMiner(PatrolNode.ScriptDBType, false, Images.patrol_script);
				this.itemDataContainer.AddDataMiner(patrolScriptItemDataMiner);
				DBObjectItemDataMiner spawnTunerItemDataMiner = new DBObjectItemDataMiner(SpawnPoint.SpawnTunerBaseType, true, Images.patrol_script);
				this.itemDataContainer.AddDataMiner(spawnTunerItemDataMiner);
				DBObjectItemDataMiner clientSceneItemDataMiner = new DBObjectItemDataMiner(ClientSpawnPoint.SceneDBType, false, Images.game_view_scene);
				this.itemDataContainer.AddDataMiner(clientSceneItemDataMiner);
				DBObjectItemDataMiner clientVisObjectItemDataMiner = new DBObjectItemDataMiner(ClientSpawnPoint.VisObjectDBType, false, Images.visual_object);
				this.itemDataContainer.AddDataMiner(clientVisObjectItemDataMiner);
				DBObjectItemDataMiner astralMobItemDataMiner = new DBObjectItemDataMiner(SpawnPoint.AstralMobWorldDBType, false, Images.astral_mob);
				this.itemDataContainer.AddDataMiner(astralMobItemDataMiner);
				DBObjectItemDataMiner astralWreckItemDataMiner = new DBObjectItemDataMiner(SpawnPoint.AstralWreckDBType, false, Images.astral_wreck);
				this.itemDataContainer.AddDataMiner(astralWreckItemDataMiner);
				DBObjectItemDataMiner astralTeleportItemDataMiner = new DBObjectItemDataMiner(SpawnPoint.AstralTeleportDBType, false, Images.astral_teleport);
				this.itemDataContainer.AddDataMiner(astralTeleportItemDataMiner);
				DBObjectItemDataMiner playerSpawnPlaceDataMiner = new DBObjectItemDataMiner(PlayerRespawnPlace.DeviceDBType, true, Images.player_spawn_place);
				this.itemDataContainer.AddDataMiner(playerSpawnPlaceDataMiner);
				MapZoneItemDataMiner mapZoneItemDataMiner = new MapZoneItemDataMiner();
				this.itemDataContainer.AddDataMiner(mapZoneItemDataMiner);
				LightItemDataMiner lightItemDataMiner = new LightItemDataMiner();
				this.itemDataContainer.AddDataMiner(lightItemDataMiner);
				SoundItemDataMiner soundItemDataMiner = new SoundItemDataMiner();
				this.itemDataContainer.AddDataMiner(soundItemDataMiner);
				LandscapeProfileItemDataMiner landscapeProfileItemDataMiner = new LandscapeProfileItemDataMiner();
				this.itemDataContainer.AddDataMiner(landscapeProfileItemDataMiner);
				LandscapeHeightmapItemDataMiner landscapeHeightmapItemDataMiner = new LandscapeHeightmapItemDataMiner();
				this.itemDataContainer.AddDataMiner(landscapeHeightmapItemDataMiner);
				LandscapeClipboardItemDataMiner landscapeClipboardItemDataMiner = new LandscapeClipboardItemDataMiner();
				this.itemDataContainer.AddDataMiner(landscapeClipboardItemDataMiner);
				LandscapeHillItemDataMiner landscapeHillItemDataMiner = new LandscapeHillItemDataMiner();
				this.itemDataContainer.AddDataMiner(landscapeHillItemDataMiner);
				LandscapeRoadItemDataMiner landscapeRoadItemDataMiner = new LandscapeRoadItemDataMiner();
				this.itemDataContainer.AddDataMiner(landscapeRoadItemDataMiner);
			}
		}

		// Token: 0x0600084F RID: 2127 RVA: 0x0004A132 File Offset: 0x00049132
		private void DestroyItemDataMiners()
		{
			ItemDataContainer itemDataContainer = this.itemDataContainer;
		}

		// Token: 0x06000850 RID: 2128 RVA: 0x0004A13C File Offset: 0x0004913C
		private void CreateMapSpecificItemDataMiners(MapEditorMap map)
		{
			this.DestroyMapSpecificItemDataMiners();
			if (this.itemDataContainer != null)
			{
				this.landscapeTileItemDataMiner = new LandscapeTileItemDataMiner(map.Data.ContinentName);
				this.itemDataContainer.AddDataMiner(this.landscapeTileItemDataMiner);
				this.landscapeWaterItemDataMiner = new LandscapeWaterItemDataMiner(map.Data.ContinentName);
				this.itemDataContainer.AddDataMiner(this.landscapeWaterItemDataMiner);
			}
		}

		// Token: 0x06000851 RID: 2129 RVA: 0x0004A1A8 File Offset: 0x000491A8
		private void DestroyMapSpecificItemDataMiners()
		{
			if (this.itemDataContainer != null && this.landscapeTileItemDataMiner != null)
			{
				this.itemDataContainer.RemoveDataMiner(this.landscapeTileItemDataMiner);
				this.landscapeTileItemDataMiner = null;
				this.itemDataContainer.RemoveDataMiner(this.landscapeWaterItemDataMiner);
				this.landscapeWaterItemDataMiner = null;
			}
		}

		// Token: 0x1400002B RID: 43
		// (add) Token: 0x06000852 RID: 2130 RVA: 0x0004A1F5 File Offset: 0x000491F5
		// (remove) Token: 0x06000853 RID: 2131 RVA: 0x0004A20E File Offset: 0x0004920E
		public event EventHandler Minimized;

		// Token: 0x1400002C RID: 44
		// (add) Token: 0x06000854 RID: 2132 RVA: 0x0004A227 File Offset: 0x00049227
		// (remove) Token: 0x06000855 RID: 2133 RVA: 0x0004A240 File Offset: 0x00049240
		public event EventHandler Normalized;

		// Token: 0x1400002D RID: 45
		// (add) Token: 0x06000856 RID: 2134 RVA: 0x0004A259 File Offset: 0x00049259
		// (remove) Token: 0x06000857 RID: 2135 RVA: 0x0004A272 File Offset: 0x00049272
		public event EventHandler Maximized;

		// Token: 0x1400002E RID: 46
		// (add) Token: 0x06000858 RID: 2136 RVA: 0x0004A28B File Offset: 0x0004928B
		// (remove) Token: 0x06000859 RID: 2137 RVA: 0x0004A2A4 File Offset: 0x000492A4
		public event EventHandler Restored;

		// Token: 0x0600085A RID: 2138 RVA: 0x0004A2BD File Offset: 0x000492BD
		private static void OnExitMapEditor(MethodArgs methodArgs)
		{
			methodArgs.form.Close();
		}

		// Token: 0x0600085B RID: 2139 RVA: 0x0004A2CC File Offset: 0x000492CC
		private static void OnAbout(MethodArgs methodArgs)
		{
			AboutForm aboutForm = new AboutForm(MainForm.mapEditorVersion);
			aboutForm.ShowDialog(methodArgs.form);
		}

		// Token: 0x0600085C RID: 2140 RVA: 0x0004A2F2 File Offset: 0x000492F2
		private void AssignIcons()
		{
			this.imageEnumerator.Collect(this);
			this.imageEnumerator.Assign(this);
		}

		// Token: 0x0600085D RID: 2141 RVA: 0x0004A30C File Offset: 0x0004930C
		private void ResizeStatusBar()
		{
			int messageWidth = 500;
			int positionWidth = 300;
			int statusStripShift = 18;
			int statusHelpShift = 0;
			int statusStripWidth = this.statusStrip.Size.Width - statusStripShift;
			if (statusStripWidth > messageWidth + positionWidth + statusStripShift)
			{
				this.StatusbarMessge.Width = statusStripWidth - positionWidth;
				this.StatusbarPosition.Width = positionWidth;
			}
			else
			{
				this.StatusbarMessge.Width = messageWidth * statusStripWidth / (messageWidth + positionWidth);
				this.StatusbarPosition.Width = positionWidth * statusStripWidth / (messageWidth + positionWidth);
			}
			this.StatusbarHelp.Width = this.statusHelp.Size.Width - statusHelpShift;
		}

		// Token: 0x0600085E RID: 2142 RVA: 0x0004A3B0 File Offset: 0x000493B0
		private void MainForm_Load(object sender, EventArgs e)
		{
			this.ShowSplashScreen();
			EditorSceneDLLImport.ApplicationSingleton_Create(EditorEnvironment.ModuleGUID, base.Handle);
			this.title.ApplicationName = this.Text;
			this.dragAndDropContainer.Bind(this);
			this.ResizeStatusBar();
			this.CreateStateContainer();
			this.CreateRecentList();
			this.CreateOperationContainer();
			this.CreateScene();
			this.AssignIcons();
			this.CreateItemDataContainer();
			this.CreateMainState();
			this.CreateQuests();
			this.CreateForms();
			this.CreateStates();
			this.CreateMapState();
			this.HideSplashScreen();
			this.StartStepTimer();
			this.StopSaveNotificationTimer();
			this.ParseCommanLine();
		}

		// Token: 0x0600085F RID: 2143 RVA: 0x0004A454 File Offset: 0x00049454
		private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
		{
			this.formClosing = true;
			if (this.CloseMainForm != null)
			{
				this.CloseMainForm(e);
			}
			if (e.Cancel)
			{
				this.formClosing = false;
				return;
			}
			this.StopStepTimer();
			this.StopSaveNotificationTimer();
			this.DestroyMapState();
			this.DestroyStates();
			this.DestroyForms();
			this.DestroyQuests();
			this.DestroyMainState();
			this.DestroyItemDataContainer();
			this.DestroyScene();
			this.DestroyOperationContainer();
			this.DestroyRecentList();
			this.DestroyStateContainer();
			this.dragAndDropContainer.Clear();
			this.dragAndDropContainer.Unbind();
			EditorSceneDLLImport.ApplicationSingleton_Destroy();
		}

		// Token: 0x06000860 RID: 2144 RVA: 0x0004A4F0 File Offset: 0x000494F0
		private void MainForm_Layout(object sender, LayoutEventArgs e)
		{
			if (base.WindowState != this.currentWindowState)
			{
				switch (base.WindowState)
				{
				case FormWindowState.Normal:
					if (this.Normalized != null)
					{
						this.Normalized(this, EventArgs.Empty);
					}
					if (this.currentWindowState == FormWindowState.Minimized && this.Restored != null)
					{
						this.Restored(this, EventArgs.Empty);
					}
					break;
				case FormWindowState.Minimized:
					if (this.Minimized != null)
					{
						this.Minimized(this, EventArgs.Empty);
					}
					break;
				case FormWindowState.Maximized:
					if (this.Maximized != null)
					{
						this.Maximized(this, EventArgs.Empty);
					}
					if (this.currentWindowState == FormWindowState.Minimized && this.Restored != null)
					{
						this.Restored(this, EventArgs.Empty);
					}
					break;
				}
				this.currentWindowState = base.WindowState;
				this.UpdateSceneAspect();
			}
		}

		// Token: 0x06000861 RID: 2145 RVA: 0x0004A5D1 File Offset: 0x000495D1
		public void StartSaveNotificationTimer(int min)
		{
			this.saveNotificationTimer.Interval = min * 60 * 1000;
			this.saveNotificationTimer.Start();
		}

		// Token: 0x06000862 RID: 2146 RVA: 0x0004A5F3 File Offset: 0x000495F3
		public void StopSaveNotificationTimer()
		{
			this.saveNotificationTimer.Stop();
		}

		// Token: 0x06000863 RID: 2147 RVA: 0x0004A600 File Offset: 0x00049600
		private void SaveNotificationTimer_Tick(object sender, EventArgs e)
		{
			this.StopSaveNotificationTimer();
			this.StartSaveNotificationTimer(5);
		}

		// Token: 0x06000864 RID: 2148 RVA: 0x0004A60F File Offset: 0x0004960F
		private void MainForm_Resize(object sender, EventArgs e)
		{
			this.UpdateSceneAspect();
		}

		// Token: 0x06000865 RID: 2149 RVA: 0x0004A617 File Offset: 0x00049617
		private void StatusStrip_Resize(object sender, EventArgs e)
		{
			this.ResizeStatusBar();
		}

		// Token: 0x06000866 RID: 2150 RVA: 0x0004A61F File Offset: 0x0004961F
		private static void OnLoadParams(FormParams formParams)
		{
			formParams.ResizeInt(MainForm.formParamsSaver_intCount, MainForm.formParamsSaver_intDefaultValues);
			formParams.ResizeDouble(MainForm.formParamsSaver_doubleCount, MainForm.formParamsSaver_doubleDefaultValues);
		}

		// Token: 0x06000867 RID: 2151 RVA: 0x0004A641 File Offset: 0x00049641
		private void OnTitleChanged(FormTitle formTitle)
		{
			if (formTitle != null)
			{
				this.Text = formTitle.Text;
			}
		}

		// Token: 0x06000868 RID: 2152 RVA: 0x0004A652 File Offset: 0x00049652
		private void StartStepTimer()
		{
			this.stepTimer.Interval = 25;
			this.stepTimer.Enabled = true;
			this.stepTimer.Start();
		}

		// Token: 0x06000869 RID: 2153 RVA: 0x0004A678 File Offset: 0x00049678
		private void StopStepTimer()
		{
			this.stepTimer.Stop();
		}

		// Token: 0x0600086A RID: 2154 RVA: 0x0004A685 File Offset: 0x00049685
		private void Step()
		{
			this.StopStepTimer();
			this.UpdateScene();
			this.UpdateItemDataContainer();
			this.StartStepTimer();
		}

		// Token: 0x0600086B RID: 2155 RVA: 0x0004A69F File Offset: 0x0004969F
		private void stepTimer_Tick(object sender, EventArgs e)
		{
			this.Step();
		}

		// Token: 0x0600086C RID: 2156 RVA: 0x0004A6A8 File Offset: 0x000496A8
		private void ParseCommanLine()
		{
			if (this.mapState != null)
			{
				List<string> lines = new List<string>();
				Str.SplitCommandLine(Environment.CommandLine, lines);
				string startFile = "-file:";
				string startExit = "-exit";
				string startCheck = "-check";
				bool start = false;
				bool exit = false;
				bool check = false;
				string fileName = string.Empty;
				OpenMapDialog.Params param = new OpenMapDialog.Params();
				string line = string.Empty;
				for (int i = 0; i < lines.Count; i++)
				{
					if (lines[i].StartsWith(startFile, StringComparison.OrdinalIgnoreCase))
					{
						fileName = lines[i].Substring(startFile.Length);
						start = !string.IsNullOrEmpty(fileName);
					}
					else if (lines[i].StartsWith(startExit, StringComparison.OrdinalIgnoreCase))
					{
						exit = true;
					}
					else if (lines[i].StartsWith(startCheck, StringComparison.OrdinalIgnoreCase))
					{
						check = true;
						line = lines[++i];
						param.Name = "Maps/" + line + "/MapResource.xdb";
						param.SetPatch(new Tools.Geometry.Point(int.Parse(lines[++i]), int.Parse(lines[++i])));
					}
				}
				if (start)
				{
					Image tmp = Image.FromFile(fileName);
					PropertyItem info = tmp.GetPropertyItem(tmp.PropertyIdList[0]);
					Encoding enc = Encoding.Unicode;
					string myString = enc.GetString(info.Value);
					this.mapState.OpenMapFromCommandLine(myString);
				}
				if (check)
				{
					this.mapState.OpenMapFromCmdLine(param);
					this.context.Checkers.CheckAll();
					Directory.CreateDirectory(EditorEnvironment.WorkingFolder + "Personal/Logs/" + line);
					this.context.Checkers.OutFileName = string.Concat(new object[]
					{
						EditorEnvironment.WorkingFolder,
						"Personal/Logs/",
						line,
						"/(",
						param.GetPatch().X,
						"_",
						param.GetPatch().Y,
						")(",
						param.GetPatch().X + param.MapSize,
						"_",
						param.GetPatch().Y + param.MapSize,
						").log"
					});
					this.context.Checkers.SaveOnlyBadCheckerResultsToFile();
				}
				if (exit)
				{
					base.Close();
				}
			}
		}

		// Token: 0x1700014C RID: 332
		// (get) Token: 0x0600086D RID: 2157 RVA: 0x0004A94A File Offset: 0x0004994A
		public IContainer Components
		{
			get
			{
				return this.components;
			}
		}

		// Token: 0x0600086E RID: 2158 RVA: 0x0004A954 File Offset: 0x00049954
		public MainForm()
		{
			this.formParamsSaver = new FormParamsSaver(this, EditorEnvironment.EditorFormsFolder + "MainForm.xml", false);
			this.formParamsSaver.LoadParams += MainForm.OnLoadParams;
			this.currentWindowState = base.WindowState;
			this.title.TitleChanged += this.OnTitleChanged;
			this.InitializeComponent();
			this.mainFormState.AddMethod("exit", new Method(MainForm.OnExitMapEditor));
			this.mainFormState.AddMethod("about", new Method(MainForm.OnAbout));
			CheckingSystem.Init();
		}

		// Token: 0x040005CE RID: 1486
		private const int cameraFPSType = 0;

		// Token: 0x040005CF RID: 1487
		private const int cameraRTSType = 1;

		// Token: 0x040005D0 RID: 1488
		private RecentList mapRecentList;

		// Token: 0x040005D1 RID: 1489
		private MainForm.MapRecentItemDataMiner mapRecentListDataMiner;

		// Token: 0x040005D2 RID: 1490
		private EditorScene editorScene;

		// Token: 0x040005D3 RID: 1491
		private EditorSceneParams editorSceneParams = new EditorSceneParams();

		// Token: 0x040005D4 RID: 1492
		private bool editorSceneReadyForUpdate;

		// Token: 0x040005D5 RID: 1493
		private int editorViewID;

		// Token: 0x040005D6 RID: 1494
		private EditorSceneState editorSceneState;

		// Token: 0x040005D7 RID: 1495
		private StateContainer stateContainer;

		// Token: 0x040005D8 RID: 1496
		private Accelerators accelerators;

		// Token: 0x040005D9 RID: 1497
		private Events events;

		// Token: 0x040005DA RID: 1498
		private MapState mapState;

		// Token: 0x040005DB RID: 1499
		private static readonly int cameraSpeedMinimal = 1;

		// Token: 0x040005DC RID: 1500
		private static readonly int cameraSpeedSlow = 10;

		// Token: 0x040005DD RID: 1501
		private static readonly int cameraSpeedNormal = 50;

		// Token: 0x040005DE RID: 1502
		private static readonly int cameraSpeedFast = 250;

		// Token: 0x040005DF RID: 1503
		private readonly State viewState = new State("ViewState");

		// Token: 0x040005E0 RID: 1504
		private ViewFormState viewFormState;

		// Token: 0x040005E1 RID: 1505
		private CameraFOVForm cameraFOVForm;

		// Token: 0x040005E2 RID: 1506
		private CameraSpeedForm cameraSpeedForm;

		// Token: 0x040005E3 RID: 1507
		private TimeForm timeForm;

		// Token: 0x040005E4 RID: 1508
		private SplashScreenForm splashScreenForm;

		// Token: 0x040005E5 RID: 1509
		private readonly OperationContainer operationContainer = new OperationContainer();

		// Token: 0x040005E6 RID: 1510
		private readonly State operationState = new State("OperationState");

		// Token: 0x040005E7 RID: 1511
		private string defaultUndoTooltip;

		// Token: 0x040005E8 RID: 1512
		private string defaultRedoTooltip;

		// Token: 0x040005EA RID: 1514
		private MainForm.Context context;

		// Token: 0x04000731 RID: 1841
		private QuestEnvironment quests;

		// Token: 0x04000732 RID: 1842
		private MainState mainState;

		// Token: 0x04000733 RID: 1843
		private ItemDataContainer itemDataContainer;

		// Token: 0x04000734 RID: 1844
		private bool itemDataContainerReadyForUpdate;

		// Token: 0x04000735 RID: 1845
		private LandscapeTileItemDataMiner landscapeTileItemDataMiner;

		// Token: 0x04000736 RID: 1846
		private LandscapeWaterItemDataMiner landscapeWaterItemDataMiner;

		// Token: 0x04000737 RID: 1847
		private static readonly string mapEditorVersion = "1.0, build 3";

		// Token: 0x04000738 RID: 1848
		private static readonly int formParamsSaver_formsIntIndex = 0;

		// Token: 0x04000739 RID: 1849
		private static readonly int formParamsSaver_minimapIntIndex = 0;

		// Token: 0x0400073A RID: 1850
		private static readonly int formParamsSaver_toolIntIndex = 1;

		// Token: 0x0400073B RID: 1851
		private static readonly int formParamsSaver_objectEditorIntIndex = 2;

		// Token: 0x0400073C RID: 1852
		private static readonly int formParamsSaver_propertiesIntIndex = 3;

		// Token: 0x0400073D RID: 1853
		private static readonly int formParamsSaver_groupsIntIndex = 4;

		// Token: 0x0400073E RID: 1854
		private static readonly int formParamsSaver_logIntIndex = 5;

		// Token: 0x0400073F RID: 1855
		private static readonly int formParamsSaver_layersIntIndex = 6;

		// Token: 0x04000740 RID: 1856
		private static readonly int formParamSaver_questDiagramIntIndex = 7;

		// Token: 0x04000741 RID: 1857
		private static readonly int formParamSaver_questImportIndex = 8;

		// Token: 0x04000742 RID: 1858
		private static readonly int formParamSaver_createNPCFormIndex = 9;

		// Token: 0x04000743 RID: 1859
		private static readonly int formParamSaver_createMobFormIndex = 10;

		// Token: 0x04000744 RID: 1860
		private static readonly int formParamSaver_createDeviceFormIndex = 11;

		// Token: 0x04000745 RID: 1861
		private static readonly int formParamSaver_createQuestItemFormIndex = 12;

		// Token: 0x04000746 RID: 1862
		private static readonly int formParamSaver_propertyControlIntIndex = 13;

		// Token: 0x04000747 RID: 1863
		private static readonly int formParamSaver_lightIntIndex = 14;

		// Token: 0x04000748 RID: 1864
		private static readonly int formParamSaver_multiObjectBrowserIntIndex = 17;

		// Token: 0x04000749 RID: 1865
		private static readonly int formParamSaver_roadParamsBrowserIntIndex = 20;

		// Token: 0x0400074A RID: 1866
		private static readonly int formParamSaver_checkersIntIndex = 21;

		// Token: 0x0400074B RID: 1867
		private static readonly int formParamSaver_gameIntIndex = 22;

		// Token: 0x0400074C RID: 1868
		private static readonly int formParamSaver_routeObjectBrowserFormIntIndex = 23;

		// Token: 0x0400074D RID: 1869
		private static readonly int formParamSaver_waterEditorFormIntIndex = 25;

		// Token: 0x0400074E RID: 1870
		private static readonly int formParamSaver_unselectableObjectsFormIntIndex = 29;

		// Token: 0x0400074F RID: 1871
		private static readonly int formRemouteServerLuncher_Index = 30;

		// Token: 0x04000750 RID: 1872
		private static readonly int formParamSaver_hiddenObjectsFormIntIndex = 33;

		// Token: 0x04000751 RID: 1873
		private static readonly int formParamSaver_modelEditorIntIndex = 34;

		// Token: 0x04000752 RID: 1874
		private static readonly int formParamsSaver_mainToolbarIntIndex = 35;

		// Token: 0x04000753 RID: 1875
		private static readonly int formParamsSaver_toolsToolbarIntIndex = 36;

		// Token: 0x04000754 RID: 1876
		private static readonly int formParamsSaver_statusbarIntIndex = 37;

		// Token: 0x04000755 RID: 1877
		private static readonly int formParamsSaver_statusbarHelpIntIndex = 38;

		// Token: 0x04000756 RID: 1878
		private static readonly int formParamsSaver_cameraTypeIndex = 39;

		// Token: 0x04000757 RID: 1879
		private static readonly int formParamsSaver_cameraSpeedIndex = 40;

		// Token: 0x04000758 RID: 1880
		private static readonly int formParamsSaver_cameraBoundingIndex = 41;

		// Token: 0x04000759 RID: 1881
		private static readonly int formParamsSaver_timeIndex = 42;

		// Token: 0x0400075A RID: 1882
		private static readonly int formParamsSaver_muteIndex = 43;

		// Token: 0x0400075B RID: 1883
		private static readonly int formParamsSaver_intCount = 44;

		// Token: 0x0400075C RID: 1884
		private static readonly int[] formParamsSaver_intDefaultValues = new int[]
		{
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			1,
			0,
			1,
			1,
			0,
			50,
			0,
			12,
			0
		};

		// Token: 0x0400075D RID: 1885
		private static readonly int formParamsSaver_cameraFOVDoubleIndex = 0;

		// Token: 0x0400075E RID: 1886
		private static readonly int formParamsSaver_cameraSpeedDoubleIndex = 1;

		// Token: 0x0400075F RID: 1887
		private static readonly int formParamsSaver_doubleCount = 2;

		// Token: 0x04000760 RID: 1888
		private static readonly double[] formParamsSaver_doubleDefaultValues = new double[]
		{
			1.0471975511965976,
			50.0
		};

		// Token: 0x04000761 RID: 1889
		private readonly FormParamsSaver formParamsSaver;

		// Token: 0x04000762 RID: 1890
		private FormWindowState currentWindowState;

		// Token: 0x04000763 RID: 1891
		private readonly ImageEnumerator imageEnumerator = new ImageEnumerator();

		// Token: 0x04000764 RID: 1892
		private readonly DragAndDropContainer dragAndDropContainer = new DragAndDropContainer();

		// Token: 0x04000765 RID: 1893
		private readonly FormTitle title = new FormTitle();

		// Token: 0x04000766 RID: 1894
		private bool formClosing;

		// Token: 0x0400076B RID: 1899
		private readonly State mainFormState = new State("MainFormState");

		// Token: 0x020000AE RID: 174
		public class MapRecentItemDataMiner : RecentList.IItemDataMiner
		{
			// Token: 0x06000870 RID: 2160 RVA: 0x0004AC90 File Offset: 0x00049C90
			private static bool TryParseItem(string item, out string name, out ContinentType type, out Tools.Geometry.Point position, out int mapSize)
			{
				name = string.Empty;
				type = ContinentType.Unknown;
				position = Tools.Geometry.Point.Empty;
				mapSize = 4;
				string[] itemSplit = item.Split(new char[]
				{
					':'
				});
				if (itemSplit.Length == 1)
				{
					name = itemSplit[0];
					type = ContinentType.AstralHub;
					position = Tools.Geometry.Point.Empty;
					return true;
				}
				if (itemSplit.Length > 2)
				{
					int x;
					int y;
					if (int.TryParse(itemSplit[1], out x) && int.TryParse(itemSplit[2], out y))
					{
						name = itemSplit[0];
						type = ContinentType.Continent;
						position = new Tools.Geometry.Point(x, y);
						return true;
					}
					if (itemSplit.Length > 3)
					{
						int.TryParse(itemSplit[3], out mapSize);
					}
				}
				return false;
			}

			// Token: 0x06000871 RID: 2161 RVA: 0x0004AD2C File Offset: 0x00049D2C
			public bool GetItemData(string item, out RecentList.ItemData itemData)
			{
				itemData = new RecentList.ItemData();
				itemData.Text = string.Empty;
				string name;
				ContinentType type;
				Tools.Geometry.Point position;
				int mapSize;
				if (MainForm.MapRecentItemDataMiner.TryParseItem(item, out name, out type, out position, out mapSize))
				{
					if (type == ContinentType.AstralHub)
					{
						itemData.Text = name;
						return true;
					}
					if (type == ContinentType.Continent)
					{
						itemData.Text = string.Format("{0} {1},{2} ({3},{4}) ({5}x{5} patches)", new object[]
						{
							name,
							position.X,
							position.Y,
							position.X * 8,
							position.Y * 8,
							mapSize
						});
						return true;
					}
				}
				return false;
			}

			// Token: 0x06000872 RID: 2162 RVA: 0x0004ADE4 File Offset: 0x00049DE4
			public bool ParseMapRecentItem(string item, out OpenMapDialog.Params openedMapParams)
			{
				openedMapParams = new OpenMapDialog.Params();
				string name;
				ContinentType type;
				Tools.Geometry.Point position;
				int mapSize;
				if (MainForm.MapRecentItemDataMiner.TryParseItem(item, out name, out type, out position, out mapSize))
				{
					openedMapParams.Name = name;
					openedMapParams.Type = type;
					openedMapParams.MapSize = mapSize;
					openedMapParams.SetPatch(position);
					return true;
				}
				return false;
			}

			// Token: 0x06000873 RID: 2163 RVA: 0x0004AE2C File Offset: 0x00049E2C
			public bool GetMapRecentItem(OpenMapDialog.Params openedMapParams, out string item)
			{
				if (openedMapParams.Type == ContinentType.AstralHub)
				{
					item = openedMapParams.Name;
					return true;
				}
				if (openedMapParams.Type == ContinentType.Continent)
				{
					Tools.Geometry.Point patch = openedMapParams.GetPatch();
					item = string.Concat(new object[]
					{
						openedMapParams.Name,
						':',
						patch.X,
						':',
						patch.Y,
						':',
						openedMapParams.MapSize
					});
					return true;
				}
				item = string.Empty;
				return false;
			}

			// Token: 0x0400076C RID: 1900
			private const char separator = ':';
		}

		// Token: 0x020000AF RID: 175
		public class Context
		{
			// Token: 0x06000875 RID: 2165 RVA: 0x0004AED0 File Offset: 0x00049ED0
			public Context(MainForm _mainForm)
			{
				this.mainForm = _mainForm;
				IDatabase mainDb = IDatabase.GetMainDatabase();
				try
				{
					IObjMan man = mainDb.CreateNewObject(SpawnPointsDataSource.FileDBType);
					if (man != null)
					{
						this.fullDatabase = true;
					}
				}
				catch (Exception e)
				{
					Console.WriteLine(e);
				}
				this.statusbar = new MainForm.Context.ContextStatusbar(this);
				this.propertyControl = new MainForm.Context.ContextPropertyControl(this);
			}

			// Token: 0x1700014D RID: 333
			// (get) Token: 0x06000876 RID: 2166 RVA: 0x0004AF3C File Offset: 0x00049F3C
			public IStatusbar Statusbar
			{
				get
				{
					return this.statusbar;
				}
			}

			// Token: 0x1700014E RID: 334
			// (get) Token: 0x06000877 RID: 2167 RVA: 0x0004AF44 File Offset: 0x00049F44
			public IPropertyControl PropertyControl
			{
				get
				{
					return this.propertyControl;
				}
			}

			// Token: 0x1700014F RID: 335
			// (get) Token: 0x06000878 RID: 2168 RVA: 0x0004AF4C File Offset: 0x00049F4C
			public MainState MainState
			{
				get
				{
					if (this.mainForm != null)
					{
						return this.mainForm.mainState;
					}
					return null;
				}
			}

			// Token: 0x17000150 RID: 336
			// (get) Token: 0x06000879 RID: 2169 RVA: 0x0004AF63 File Offset: 0x00049F63
			public bool FullDatabase
			{
				get
				{
					return this.fullDatabase;
				}
			}

			// Token: 0x17000151 RID: 337
			// (get) Token: 0x0600087A RID: 2170 RVA: 0x0004AF6B File Offset: 0x00049F6B
			public FormTitle Title
			{
				get
				{
					if (this.mainForm != null)
					{
						return this.mainForm.title;
					}
					return null;
				}
			}

			// Token: 0x17000152 RID: 338
			// (get) Token: 0x0600087B RID: 2171 RVA: 0x0004AF82 File Offset: 0x00049F82
			public DragAndDropContainer DragAndDropContainer
			{
				get
				{
					if (this.mainForm != null)
					{
						return this.mainForm.dragAndDropContainer;
					}
					return null;
				}
			}

			// Token: 0x17000153 RID: 339
			// (get) Token: 0x0600087C RID: 2172 RVA: 0x0004AF99 File Offset: 0x00049F99
			public Form MainForm
			{
				get
				{
					if (this.mainForm != null)
					{
						return this.mainForm;
					}
					return null;
				}
			}

			// Token: 0x17000154 RID: 340
			// (get) Token: 0x0600087D RID: 2173 RVA: 0x0004AFAC File Offset: 0x00049FAC
			public MinimapForm Minimap
			{
				get
				{
					if (this.mainForm != null)
					{
						MapEditor.Forms.Base.BaseForm baseForm = this.mainForm.GetBaseForm(MapEditor.MainForm.formParamsSaver_minimapIntIndex);
						return baseForm as MinimapForm;
					}
					return null;
				}
			}

			// Token: 0x17000155 RID: 341
			// (get) Token: 0x0600087E RID: 2174 RVA: 0x0004AFDC File Offset: 0x00049FDC
			public ToolForm Tool
			{
				get
				{
					if (this.mainForm != null)
					{
						MapEditor.Forms.Base.BaseForm baseForm = this.mainForm.GetBaseForm(MapEditor.MainForm.formParamsSaver_toolIntIndex);
						return baseForm as ToolForm;
					}
					return null;
				}
			}

			// Token: 0x17000156 RID: 342
			// (get) Token: 0x0600087F RID: 2175 RVA: 0x0004B00C File Offset: 0x0004A00C
			public CheckersForm Checkers
			{
				get
				{
					if (this.mainForm != null)
					{
						MapEditor.Forms.Base.BaseForm baseForm = this.mainForm.GetBaseForm(MapEditor.MainForm.formParamSaver_checkersIntIndex);
						return baseForm as CheckersForm;
					}
					return null;
				}
			}

			// Token: 0x17000157 RID: 343
			// (get) Token: 0x06000880 RID: 2176 RVA: 0x0004B03C File Offset: 0x0004A03C
			public ServerLuncherForm ServerLuncher
			{
				get
				{
					if (this.mainForm != null)
					{
						MapEditor.Forms.Base.BaseForm baseForm = this.mainForm.GetBaseForm(MapEditor.MainForm.formRemouteServerLuncher_Index);
						return baseForm as ServerLuncherForm;
					}
					return null;
				}
			}

			// Token: 0x17000158 RID: 344
			// (get) Token: 0x06000881 RID: 2177 RVA: 0x0004B06C File Offset: 0x0004A06C
			public LayersForm Layers
			{
				get
				{
					if (this.mainForm != null)
					{
						MapEditor.Forms.Base.BaseForm baseForm = this.mainForm.GetBaseForm(MapEditor.MainForm.formParamsSaver_layersIntIndex);
						return baseForm as LayersForm;
					}
					return null;
				}
			}

			// Token: 0x17000159 RID: 345
			// (get) Token: 0x06000882 RID: 2178 RVA: 0x0004B09C File Offset: 0x0004A09C
			public ObjectEditorForm ObjectEditor
			{
				get
				{
					if (this.mainForm != null)
					{
						MapEditor.Forms.Base.BaseForm baseForm = this.mainForm.GetBaseForm(MapEditor.MainForm.formParamsSaver_objectEditorIntIndex);
						return baseForm as ObjectEditorForm;
					}
					return null;
				}
			}

			// Token: 0x1700015A RID: 346
			// (get) Token: 0x06000883 RID: 2179 RVA: 0x0004B0CC File Offset: 0x0004A0CC
			public MultiObjectBrowserForm MultiObjectBrowser
			{
				get
				{
					if (this.mainForm != null)
					{
						MapEditor.Forms.Base.BaseForm baseForm = this.mainForm.GetBaseForm(MapEditor.MainForm.formParamSaver_multiObjectBrowserIntIndex);
						return baseForm as MultiObjectBrowserForm;
					}
					return null;
				}
			}

			// Token: 0x1700015B RID: 347
			// (get) Token: 0x06000884 RID: 2180 RVA: 0x0004B0FC File Offset: 0x0004A0FC
			public RoadParamsBrowserForm RoadParamsBrowser
			{
				get
				{
					if (this.mainForm != null)
					{
						MapEditor.Forms.Base.BaseForm baseForm = this.mainForm.GetBaseForm(MapEditor.MainForm.formParamSaver_roadParamsBrowserIntIndex);
						return baseForm as RoadParamsBrowserForm;
					}
					return null;
				}
			}

			// Token: 0x1700015C RID: 348
			// (get) Token: 0x06000885 RID: 2181 RVA: 0x0004B12C File Offset: 0x0004A12C
			public PropertyControlForm DatabaseEditor
			{
				get
				{
					if (this.mainForm != null)
					{
						MapEditor.Forms.Base.BaseForm baseForm = this.mainForm.GetBaseForm(MapEditor.MainForm.formParamSaver_propertyControlIntIndex);
						return baseForm as PropertyControlForm;
					}
					return null;
				}
			}

			// Token: 0x1700015D RID: 349
			// (get) Token: 0x06000886 RID: 2182 RVA: 0x0004B15C File Offset: 0x0004A15C
			public GroupsForm Groups
			{
				get
				{
					if (this.mainForm != null)
					{
						MapEditor.Forms.Base.BaseForm baseForm = this.mainForm.GetBaseForm(MapEditor.MainForm.formParamsSaver_groupsIntIndex);
						return baseForm as GroupsForm;
					}
					return null;
				}
			}

			// Token: 0x1700015E RID: 350
			// (get) Token: 0x06000887 RID: 2183 RVA: 0x0004B18C File Offset: 0x0004A18C
			public LogForm Log
			{
				get
				{
					if (this.mainForm != null)
					{
						MapEditor.Forms.Base.BaseForm baseForm = this.mainForm.GetBaseForm(MapEditor.MainForm.formParamsSaver_logIntIndex);
						return baseForm as LogForm;
					}
					return null;
				}
			}

			// Token: 0x1700015F RID: 351
			// (get) Token: 0x06000888 RID: 2184 RVA: 0x0004B1BC File Offset: 0x0004A1BC
			public GameForm Game
			{
				get
				{
					if (this.mainForm != null)
					{
						MapEditor.Forms.Base.BaseForm baseForm = this.mainForm.GetBaseForm(MapEditor.MainForm.formParamSaver_gameIntIndex);
						return baseForm as GameForm;
					}
					return null;
				}
			}

			// Token: 0x17000160 RID: 352
			// (get) Token: 0x06000889 RID: 2185 RVA: 0x0004B1EC File Offset: 0x0004A1EC
			public QuestDiagramForm QuestDiagramForm
			{
				get
				{
					if (this.mainForm != null)
					{
						MapEditor.Forms.Base.BaseForm baseForm = this.mainForm.GetBaseForm(MapEditor.MainForm.formParamSaver_questDiagramIntIndex);
						return baseForm as QuestDiagramForm;
					}
					return null;
				}
			}

			// Token: 0x17000161 RID: 353
			// (get) Token: 0x0600088A RID: 2186 RVA: 0x0004B21C File Offset: 0x0004A21C
			public QuestImportForm QuestImportForm
			{
				get
				{
					if (this.mainForm != null)
					{
						MapEditor.Forms.Base.BaseForm baseForm = this.mainForm.GetBaseForm(MapEditor.MainForm.formParamSaver_questImportIndex);
						return baseForm as QuestImportForm;
					}
					return null;
				}
			}

			// Token: 0x17000162 RID: 354
			// (get) Token: 0x0600088B RID: 2187 RVA: 0x0004B24C File Offset: 0x0004A24C
			public LightEditorForm LightEditorForm
			{
				get
				{
					if (this.mainForm != null)
					{
						MapEditor.Forms.Base.BaseForm baseForm = this.mainForm.GetBaseForm(MapEditor.MainForm.formParamSaver_lightIntIndex);
						return baseForm as LightEditorForm;
					}
					return null;
				}
			}

			// Token: 0x17000163 RID: 355
			// (get) Token: 0x0600088C RID: 2188 RVA: 0x0004B27C File Offset: 0x0004A27C
			public CreateMobForm CreateMobForm
			{
				get
				{
					if (this.mainForm != null)
					{
						MapEditor.Forms.Base.BaseForm baseForm = this.mainForm.GetBaseForm(MapEditor.MainForm.formParamSaver_createMobFormIndex);
						return baseForm as CreateMobForm;
					}
					return null;
				}
			}

			// Token: 0x17000164 RID: 356
			// (get) Token: 0x0600088D RID: 2189 RVA: 0x0004B2AC File Offset: 0x0004A2AC
			public CreateNPCForm CreateNPCForm
			{
				get
				{
					if (this.mainForm != null)
					{
						MapEditor.Forms.Base.BaseForm baseForm = this.mainForm.GetBaseForm(MapEditor.MainForm.formParamSaver_createNPCFormIndex);
						return baseForm as CreateNPCForm;
					}
					return null;
				}
			}

			// Token: 0x17000165 RID: 357
			// (get) Token: 0x0600088E RID: 2190 RVA: 0x0004B2DC File Offset: 0x0004A2DC
			public CreateQuestItemForm CreateQuestItemForm
			{
				get
				{
					if (this.mainForm != null)
					{
						MapEditor.Forms.Base.BaseForm baseForm = this.mainForm.GetBaseForm(MapEditor.MainForm.formParamSaver_createQuestItemFormIndex);
						return baseForm as CreateQuestItemForm;
					}
					return null;
				}
			}

			// Token: 0x17000166 RID: 358
			// (get) Token: 0x0600088F RID: 2191 RVA: 0x0004B30C File Offset: 0x0004A30C
			public CreateDeviceForm CreateDeviceForm
			{
				get
				{
					if (this.mainForm != null)
					{
						MapEditor.Forms.Base.BaseForm baseForm = this.mainForm.GetBaseForm(MapEditor.MainForm.formParamSaver_createDeviceFormIndex);
						return baseForm as CreateDeviceForm;
					}
					return null;
				}
			}

			// Token: 0x17000167 RID: 359
			// (get) Token: 0x06000890 RID: 2192 RVA: 0x0004B33C File Offset: 0x0004A33C
			public RouteObjectBrowserForm RouteObjectBrowser
			{
				get
				{
					if (this.mainForm != null)
					{
						MapEditor.Forms.Base.BaseForm baseForm = this.mainForm.GetBaseForm(MapEditor.MainForm.formParamSaver_routeObjectBrowserFormIntIndex);
						return baseForm as RouteObjectBrowserForm;
					}
					return null;
				}
			}

			// Token: 0x17000168 RID: 360
			// (get) Token: 0x06000891 RID: 2193 RVA: 0x0004B36C File Offset: 0x0004A36C
			public WaterEditorForm WaterEditorForm
			{
				get
				{
					if (this.mainForm != null)
					{
						MapEditor.Forms.Base.BaseForm baseForm = this.mainForm.GetBaseForm(MapEditor.MainForm.formParamSaver_waterEditorFormIntIndex);
						return baseForm as WaterEditorForm;
					}
					return null;
				}
			}

			// Token: 0x17000169 RID: 361
			// (get) Token: 0x06000892 RID: 2194 RVA: 0x0004B39C File Offset: 0x0004A39C
			public UnselectableObjectsForm UnselectableObjectsForm
			{
				get
				{
					if (this.mainForm != null)
					{
						MapEditor.Forms.Base.BaseForm baseForm = this.mainForm.GetBaseForm(MapEditor.MainForm.formParamSaver_unselectableObjectsFormIntIndex);
						return baseForm as UnselectableObjectsForm;
					}
					return null;
				}
			}

			// Token: 0x1700016A RID: 362
			// (get) Token: 0x06000893 RID: 2195 RVA: 0x0004B3CC File Offset: 0x0004A3CC
			public HiddenObjectsForm HiddenObjectsForm
			{
				get
				{
					if (this.mainForm != null)
					{
						MapEditor.Forms.Base.BaseForm baseForm = this.mainForm.GetBaseForm(MapEditor.MainForm.formParamSaver_hiddenObjectsFormIntIndex);
						return baseForm as HiddenObjectsForm;
					}
					return null;
				}
			}

			// Token: 0x06000894 RID: 2196 RVA: 0x0004B3FA File Offset: 0x0004A3FA
			public void ShowModelEditor(bool visible)
			{
				this.mainForm.ShowBaseForm(MapEditor.MainForm.formParamSaver_modelEditorIntIndex, visible);
			}

			// Token: 0x1700016B RID: 363
			// (get) Token: 0x06000895 RID: 2197 RVA: 0x0004B40D File Offset: 0x0004A40D
			public MainForm ModelEditor
			{
				get
				{
					if (this.mainForm != null)
					{
						return this.mainForm.viewFormState.GetForm(MapEditor.MainForm.formParamSaver_modelEditorIntIndex) as MainForm;
					}
					return null;
				}
			}

			// Token: 0x1700016C RID: 364
			// (get) Token: 0x06000896 RID: 2198 RVA: 0x0004B433 File Offset: 0x0004A433
			public OperationContainer OperationContainer
			{
				get
				{
					if (this.mainForm != null)
					{
						return this.mainForm.operationContainer;
					}
					return null;
				}
			}

			// Token: 0x1700016D RID: 365
			// (get) Token: 0x06000897 RID: 2199 RVA: 0x0004B44A File Offset: 0x0004A44A
			public EditorSceneParams EditorSceneParams
			{
				get
				{
					if (this.mainForm != null)
					{
						return this.mainForm.editorSceneParams;
					}
					return null;
				}
			}

			// Token: 0x1700016E RID: 366
			// (get) Token: 0x06000898 RID: 2200 RVA: 0x0004B461 File Offset: 0x0004A461
			public EditorScene EditorScene
			{
				get
				{
					if (this.mainForm != null)
					{
						return this.mainForm.editorScene;
					}
					return null;
				}
			}

			// Token: 0x1700016F RID: 367
			// (get) Token: 0x06000899 RID: 2201 RVA: 0x0004B478 File Offset: 0x0004A478
			public int EditorSceneViewID
			{
				get
				{
					if (this.mainForm != null)
					{
						return this.mainForm.editorViewID;
					}
					return -1;
				}
			}

			// Token: 0x17000170 RID: 368
			// (get) Token: 0x0600089A RID: 2202 RVA: 0x0004B48F File Offset: 0x0004A48F
			public StateContainer StateContainer
			{
				get
				{
					if (this.mainForm != null)
					{
						return this.mainForm.stateContainer;
					}
					return null;
				}
			}

			// Token: 0x17000171 RID: 369
			// (get) Token: 0x0600089B RID: 2203 RVA: 0x0004B4A6 File Offset: 0x0004A4A6
			public RecentList MapRecentList
			{
				get
				{
					if (this.mainForm != null)
					{
						return this.mainForm.mapRecentList;
					}
					return null;
				}
			}

			// Token: 0x17000172 RID: 370
			// (get) Token: 0x0600089C RID: 2204 RVA: 0x0004B4BD File Offset: 0x0004A4BD
			public MainForm.MapRecentItemDataMiner MapRecentItemDataMiner
			{
				get
				{
					if (this.mainForm != null)
					{
						return this.mainForm.mapRecentListDataMiner;
					}
					return null;
				}
			}

			// Token: 0x17000173 RID: 371
			// (get) Token: 0x0600089D RID: 2205 RVA: 0x0004B4D4 File Offset: 0x0004A4D4
			public ItemDataContainer ItemDataContainer
			{
				get
				{
					if (this.mainForm != null)
					{
						return this.mainForm.itemDataContainer;
					}
					return null;
				}
			}

			// Token: 0x0600089E RID: 2206 RVA: 0x0004B4EB File Offset: 0x0004A4EB
			public void UpdateScene()
			{
				if (this.mainForm != null)
				{
					this.mainForm.UpdateScene();
				}
			}

			// Token: 0x0600089F RID: 2207 RVA: 0x0004B500 File Offset: 0x0004A500
			public void ShowDatabaseEditor(bool visible)
			{
				this.mainForm.ShowBaseForm(MapEditor.MainForm.formParamSaver_propertyControlIntIndex, visible);
			}

			// Token: 0x060008A0 RID: 2208 RVA: 0x0004B513 File Offset: 0x0004A513
			public void ShowMultiObjectBrowser(bool visible)
			{
				this.mainForm.ShowBaseForm(MapEditor.MainForm.formParamSaver_multiObjectBrowserIntIndex, visible);
			}

			// Token: 0x060008A1 RID: 2209 RVA: 0x0004B526 File Offset: 0x0004A526
			public void ShowRoadParamsBrowser(bool visible)
			{
				this.mainForm.ShowBaseForm(MapEditor.MainForm.formParamSaver_roadParamsBrowserIntIndex, visible);
			}

			// Token: 0x060008A2 RID: 2210 RVA: 0x0004B539 File Offset: 0x0004A539
			public void ShowQuestDiagram(bool visible)
			{
				this.mainForm.ShowBaseForm(MapEditor.MainForm.formParamSaver_questDiagramIntIndex, visible);
			}

			// Token: 0x060008A3 RID: 2211 RVA: 0x0004B54C File Offset: 0x0004A54C
			public void ShowCheckers(bool visible)
			{
				this.mainForm.ShowBaseForm(MapEditor.MainForm.formParamSaver_checkersIntIndex, visible);
			}

			// Token: 0x060008A4 RID: 2212 RVA: 0x0004B55F File Offset: 0x0004A55F
			public void AddCloseFormEvent(MainForm.Context.CloseFormEvent closeFormEvent)
			{
				if (this.mainForm != null)
				{
					MainForm mainForm = this.mainForm;
					mainForm.CloseMainForm = (MainForm.Context.CloseFormEvent)Delegate.Combine(mainForm.CloseMainForm, closeFormEvent);
				}
			}

			// Token: 0x060008A5 RID: 2213 RVA: 0x0004B585 File Offset: 0x0004A585
			public void RemoveCloseFormEvent(MainForm.Context.CloseFormEvent closeFormEvent)
			{
				if (this.mainForm != null)
				{
					MainForm mainForm = this.mainForm;
					mainForm.CloseMainForm = (MainForm.Context.CloseFormEvent)Delegate.Remove(mainForm.CloseMainForm, closeFormEvent);
				}
			}

			// Token: 0x17000174 RID: 372
			// (get) Token: 0x060008A6 RID: 2214 RVA: 0x0004B5AB File Offset: 0x0004A5AB
			public bool FormClosing
			{
				get
				{
					return this.mainForm.formClosing;
				}
			}

			// Token: 0x17000175 RID: 373
			// (get) Token: 0x060008A7 RID: 2215 RVA: 0x0004B5B8 File Offset: 0x0004A5B8
			// (set) Token: 0x060008A8 RID: 2216 RVA: 0x0004B5C0 File Offset: 0x0004A5C0
			public Form LastActiveForm
			{
				get
				{
					return this.lastActiveForm;
				}
				set
				{
					this.lastActiveForm = value;
				}
			}

			// Token: 0x17000176 RID: 374
			// (get) Token: 0x060008A9 RID: 2217 RVA: 0x0004B5C9 File Offset: 0x0004A5C9
			public QuestEnvironment QuestEnvironment
			{
				get
				{
					if (this.mainForm != null)
					{
						return this.mainForm.quests;
					}
					return null;
				}
			}

			// Token: 0x060008AA RID: 2218 RVA: 0x0004B5E0 File Offset: 0x0004A5E0
			public void Bind(MapEditorMap map, MapObjectSelector mapObjectSelector)
			{
				this.mainForm.CreateMapSpecificItemDataMiners(map);
				this.mainForm.editorSceneParams.ContinentType = map.Data.ContinentType;
				GroupsForm groups = this.Groups;
				if (groups != null)
				{
					groups.Bind(map.Groups, map.MapEditorMapObjectContainer, mapObjectSelector);
				}
			}

			// Token: 0x060008AB RID: 2219 RVA: 0x0004B634 File Offset: 0x0004A634
			public void PostBind(MapEditorMap map, MapObjectSelector mapObjectSelector, MapEditorScene mapEditorScene, MapCheckerContainer mapCheckerContainer)
			{
				MinimapForm minimap = this.Minimap;
				if (minimap != null)
				{
					minimap.PostBind(map);
				}
				ToolForm toolForm = this.Tool;
				if (toolForm != null)
				{
					toolForm.Bind(map);
				}
				RoadParamsBrowserForm roadParamsBrowser = this.RoadParamsBrowser;
				if (roadParamsBrowser != null)
				{
					roadParamsBrowser.Bind(map);
				}
				RouteObjectBrowserForm routeObjectBrowser = this.RouteObjectBrowser;
				if (routeObjectBrowser != null)
				{
					routeObjectBrowser.Bind(map.MapEditorMapObjectContainer, mapObjectSelector, mapEditorScene);
				}
				CheckersForm checkersForm = this.Checkers;
				if (checkersForm != null)
				{
					checkersForm.Bind(mapCheckerContainer);
				}
			}

			// Token: 0x060008AC RID: 2220 RVA: 0x0004B6A4 File Offset: 0x0004A6A4
			public void Unbind()
			{
				MinimapForm minimap = this.Minimap;
				if (minimap != null)
				{
					minimap.Unbind();
				}
				ToolForm toolForm = this.Tool;
				if (toolForm != null)
				{
					toolForm.Unbind();
				}
				RoadParamsBrowserForm roadParamsBrowser = this.RoadParamsBrowser;
				if (roadParamsBrowser != null)
				{
					roadParamsBrowser.Unbind();
				}
				RouteObjectBrowserForm routeObjectBrowser = this.RouteObjectBrowser;
				if (routeObjectBrowser != null)
				{
					routeObjectBrowser.Unbind();
				}
				CheckersForm checkersForm = this.Checkers;
				if (checkersForm != null)
				{
					checkersForm.Unbind();
				}
				GroupsForm groups = this.Groups;
				if (groups != null)
				{
					groups.Unbind();
				}
				this.mainForm.editorSceneParams.ContinentType = ContinentType.Unknown;
				this.mainForm.DestroyMapSpecificItemDataMiners();
			}

			// Token: 0x060008AD RID: 2221 RVA: 0x0004B734 File Offset: 0x0004A734
			public string CreateAndBrowseItemInXDBBrowse(string locationFolder, string itemFolder, string itemType, Form parentForm)
			{
				if (this.DatabaseEditor != null)
				{
					string fullPath = EditorEnvironment.DataFolder + locationFolder;
					fullPath = fullPath.Replace('/', '\\');
					if (Directory.Exists(fullPath))
					{
						locationFolder = locationFolder + '\\' + itemFolder;
						locationFolder = locationFolder.Replace('/', '\\');
						fullPath = EditorEnvironment.DataFolder + locationFolder;
						fullPath = fullPath.Replace('/', '\\');
						if (!Directory.Exists(fullPath))
						{
							Directory.CreateDirectory(fullPath);
						}
						CreateXDB_PropertyControl creator = new CreateXDB_PropertyControl(this.DatabaseEditor.PropertyControl, itemType);
						creator.LocationFolder = locationFolder;
						if (creator.ShowDialog(parentForm) == DialogResult.OK)
						{
							return creator.FullFilePath;
						}
					}
				}
				return string.Empty;
			}

			// Token: 0x060008AE RID: 2222 RVA: 0x0004B7E0 File Offset: 0x0004A7E0
			public void SelectExistingObjectInDatabaseEditor(string item)
			{
				if (this.DatabaseEditor != null)
				{
					if (!this.DatabaseEditor.Visible)
					{
						this.ShowDatabaseEditor(true);
					}
					if (!string.IsNullOrEmpty(item))
					{
						IDatabase mainDb = IDatabase.GetMainDatabase();
						if (mainDb != null)
						{
							DBID itemDBID = mainDb.GetDBIDByName(item);
							if (!DBID.IsNullOrEmpty(itemDBID))
							{
								IObjMan itemMan = mainDb.GetManipulator(itemDBID);
								if (itemMan != null)
								{
									this.DatabaseEditor.PropertyControl.SelectedObject = itemMan;
									return;
								}
							}
						}
					}
					this.DatabaseEditor.PropertyControl.SelectedObject = null;
				}
			}

			// Token: 0x060008AF RID: 2223 RVA: 0x0004B858 File Offset: 0x0004A858
			public bool UnselectAndRemoveObjectInDatabaseEditor(string item)
			{
				if (this.DatabaseEditor != null && !string.IsNullOrEmpty(item))
				{
					if (this.DatabaseEditor.PropertyControl.SelectedObject != null)
					{
						this.DatabaseEditor.PropertyControl.SelectedObject = null;
					}
					IDatabase mainDb = IDatabase.GetMainDatabase();
					if (mainDb != null)
					{
						DBID itemDBID = mainDb.GetDBIDByName(item);
						if (mainDb.DoesObjectExist(itemDBID))
						{
							IObjMan itemMan = mainDb.GetManipulator(itemDBID);
							if (itemMan != null)
							{
								return mainDb.RemoveObject(itemDBID);
							}
						}
					}
				}
				return false;
			}

			// Token: 0x060008B0 RID: 2224 RVA: 0x0004B8C6 File Offset: 0x0004A8C6
			public void SaveTime(float time)
			{
				this.mainForm.SetTimeToParams(time);
			}

			// Token: 0x060008B1 RID: 2225 RVA: 0x0004B8D4 File Offset: 0x0004A8D4
			public void StartSaveNotificationTimer()
			{
				this.mainForm.StartSaveNotificationTimer(15);
			}

			// Token: 0x060008B2 RID: 2226 RVA: 0x0004B8E3 File Offset: 0x0004A8E3
			public void StopSaveNotificationTimer()
			{
				this.mainForm.StopSaveNotificationTimer();
			}

			// Token: 0x0400076D RID: 1901
			private readonly IStatusbar statusbar;

			// Token: 0x0400076E RID: 1902
			private readonly IPropertyControl propertyControl;

			// Token: 0x0400076F RID: 1903
			private readonly MainForm mainForm;

			// Token: 0x04000770 RID: 1904
			private readonly bool fullDatabase;

			// Token: 0x04000771 RID: 1905
			private Form lastActiveForm;

			// Token: 0x020000B0 RID: 176
			private class ContextStatusbar : IStatusbar
			{
				// Token: 0x060008B3 RID: 2227 RVA: 0x0004B8F0 File Offset: 0x0004A8F0
				public ContextStatusbar(MainForm.Context _context)
				{
					this.context = _context;
				}

				// Token: 0x060008B4 RID: 2228 RVA: 0x0004B900 File Offset: 0x0004A900
				public void UpdateStatusbar()
				{
					if (this.context != null && this.context.mainForm != null)
					{
						this.context.mainForm.statusHelp.Update();
						this.context.mainForm.statusStrip.Update();
					}
				}

				// Token: 0x17000177 RID: 375
				// (get) Token: 0x060008B5 RID: 2229 RVA: 0x0004B94C File Offset: 0x0004A94C
				public ToolStripStatusLabel StatusHelp
				{
					get
					{
						if (this.context != null && this.context.mainForm != null)
						{
							return this.context.mainForm.StatusbarHelp;
						}
						return null;
					}
				}

				// Token: 0x17000178 RID: 376
				// (get) Token: 0x060008B6 RID: 2230 RVA: 0x0004B975 File Offset: 0x0004A975
				public ToolStripStatusLabel StatusMessage
				{
					get
					{
						if (this.context != null && this.context.mainForm != null)
						{
							return this.context.mainForm.StatusbarMessge;
						}
						return null;
					}
				}

				// Token: 0x17000179 RID: 377
				// (get) Token: 0x060008B7 RID: 2231 RVA: 0x0004B99E File Offset: 0x0004A99E
				public ToolStripStatusLabel StatusPosition
				{
					get
					{
						if (this.context != null && this.context.mainForm != null)
						{
							return this.context.mainForm.StatusbarPosition;
						}
						return null;
					}
				}

				// Token: 0x04000772 RID: 1906
				private readonly MainForm.Context context;
			}

			// Token: 0x020000B1 RID: 177
			private class ContextPropertyControl : IPropertyControl
			{
				// Token: 0x060008B8 RID: 2232 RVA: 0x0004B9C8 File Offset: 0x0004A9C8
				private PropertiesForm GetPropertiesForm()
				{
					if (this.context != null && this.context.mainForm != null)
					{
						MapEditor.Forms.Base.BaseForm baseForm = this.context.mainForm.GetBaseForm(MapEditor.MainForm.formParamsSaver_propertiesIntIndex);
						return baseForm as PropertiesForm;
					}
					return null;
				}

				// Token: 0x060008B9 RID: 2233 RVA: 0x0004BA08 File Offset: 0x0004AA08
				public ContextPropertyControl(MainForm.Context _context)
				{
					this.context = _context;
				}

				// Token: 0x1700017A RID: 378
				// (get) Token: 0x060008BA RID: 2234 RVA: 0x0004BA18 File Offset: 0x0004AA18
				// (set) Token: 0x060008BB RID: 2235 RVA: 0x0004BA37 File Offset: 0x0004AA37
				public bool Visible
				{
					get
					{
						PropertiesForm propertiesForm = this.GetPropertiesForm();
						return propertiesForm != null && propertiesForm.Visible;
					}
					set
					{
						if (this.context != null && this.context.mainForm != null)
						{
							this.context.mainForm.ShowBaseForm(MapEditor.MainForm.formParamsSaver_propertiesIntIndex, value);
						}
					}
				}

				// Token: 0x060008BC RID: 2236 RVA: 0x0004BA64 File Offset: 0x0004AA64
				public void BringToFront()
				{
					if (this.context != null && this.context.mainForm != null)
					{
						this.context.mainForm.ShowBaseForm(MapEditor.MainForm.formParamsSaver_propertiesIntIndex, true);
					}
				}

				// Token: 0x1700017B RID: 379
				// (get) Token: 0x060008BD RID: 2237 RVA: 0x0004BA94 File Offset: 0x0004AA94
				// (set) Token: 0x060008BE RID: 2238 RVA: 0x0004BAB4 File Offset: 0x0004AAB4
				public object SelectedObject
				{
					get
					{
						PropertiesForm propertiesForm = this.GetPropertiesForm();
						if (propertiesForm != null)
						{
							return propertiesForm.SelectedObject;
						}
						return null;
					}
					set
					{
						PropertiesForm propertiesForm = this.GetPropertiesForm();
						if (propertiesForm != null)
						{
							propertiesForm.SelectedObject = value;
						}
					}
				}

				// Token: 0x060008BF RID: 2239 RVA: 0x0004BAD4 File Offset: 0x0004AAD4
				public void UpdatePropertyControl(bool reloadObject)
				{
					PropertiesForm propertiesForm = this.GetPropertiesForm();
					if (propertiesForm != null)
					{
						propertiesForm.UpdatePropertyControl(reloadObject);
					}
				}

				// Token: 0x04000773 RID: 1907
				private readonly MainForm.Context context;
			}

			// Token: 0x020000B2 RID: 178
			// (Invoke) Token: 0x060008C1 RID: 2241
			public delegate void CloseFormEvent(FormClosingEventArgs e);
		}
	}
}
