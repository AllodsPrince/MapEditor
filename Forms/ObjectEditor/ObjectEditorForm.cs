using System;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using Db;
using InputState;
using LauncherTools.InputState;
using MapEditor.Forms.Base;
using MapEditor.Properties;
using Operations;
using Tools.InputState;
using Tools.MapObjects;
using Tools.RecentList;
using Tools.Statusbar;
using Tools.WindowParams;

namespace MapEditor.Forms.ObjectEditor
{
	// Token: 0x0200005A RID: 90
	public partial class ObjectEditorForm : BaseForm, IStatusbar
	{
		// Token: 0x06000482 RID: 1154 RVA: 0x00026708 File Offset: 0x00025708
		private void CreateBinds()
		{
			if (this.stateContainer != null)
			{
				TextReader fileReader = new StreamReader(EditorEnvironment.EditorFolder + "Binds/Accelerators.cfg");
				this.accelerators.AddConfig(fileReader.ReadToEnd());
				TextReader fileReader2 = new StreamReader(EditorEnvironment.EditorFolder + "Binds/Events.cfg");
				this.events.AddConfig(fileReader2.ReadToEnd());
			}
		}

		// Token: 0x06000483 RID: 1155 RVA: 0x0002676C File Offset: 0x0002576C
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
		}

		// Token: 0x06000484 RID: 1156 RVA: 0x00026808 File Offset: 0x00025808
		private void CreateStates()
		{
			this.stateContainer.BindState(this.operationState);
			this.stateContainer.BindState(this.objectEditorEditState);
		}

		// Token: 0x06000485 RID: 1157 RVA: 0x0002682C File Offset: 0x0002582C
		private void DestroyStates()
		{
			this.stateContainer.UnbindState(this.objectEditorEditState);
			this.stateContainer.UnbindState(this.operationState);
		}

		// Token: 0x06000486 RID: 1158 RVA: 0x00026850 File Offset: 0x00025850
		private void DestroyStateContainer()
		{
			this.accelerators.Unbind();
			this.events.Unbind();
			this.stateContainer.Unbind();
		}

		// Token: 0x170000A2 RID: 162
		// (get) Token: 0x06000487 RID: 1159 RVA: 0x00026873 File Offset: 0x00025873
		// (set) Token: 0x06000488 RID: 1160 RVA: 0x0002688A File Offset: 0x0002588A
		public string OpenFileFolder
		{
			get
			{
				return this.formParamsSaver.FormParams.GetString(ObjectEditorForm.formParamsSaver_openFileFolderStringIndex);
			}
			set
			{
				this.formParamsSaver.FormParams.SetString(ObjectEditorForm.formParamsSaver_openFileFolderStringIndex, value);
			}
		}

		// Token: 0x170000A3 RID: 163
		// (get) Token: 0x06000489 RID: 1161 RVA: 0x000268A2 File Offset: 0x000258A2
		// (set) Token: 0x0600048A RID: 1162 RVA: 0x000268BE File Offset: 0x000258BE
		private bool ToolbarVisible
		{
			get
			{
				return FormParamsSaver.IntToBool(this.formParamsSaver.FormParams.GetInt(ObjectEditorForm.formParamsSaver_toolbarIntIndex));
			}
			set
			{
				this.formParamsSaver.FormParams.SetInt(ObjectEditorForm.formParamsSaver_toolbarIntIndex, FormParamsSaver.BoolToInt(value));
			}
		}

		// Token: 0x170000A4 RID: 164
		// (get) Token: 0x0600048B RID: 1163 RVA: 0x000268DB File Offset: 0x000258DB
		// (set) Token: 0x0600048C RID: 1164 RVA: 0x000268F7 File Offset: 0x000258F7
		private bool StatusbarVisible
		{
			get
			{
				return FormParamsSaver.IntToBool(this.formParamsSaver.FormParams.GetInt(ObjectEditorForm.formParamsSaver_statusbarIntIndex));
			}
			set
			{
				this.formParamsSaver.FormParams.SetInt(ObjectEditorForm.formParamsSaver_statusbarIntIndex, FormParamsSaver.BoolToInt(value));
			}
		}

		// Token: 0x170000A5 RID: 165
		// (get) Token: 0x0600048D RID: 1165 RVA: 0x00026914 File Offset: 0x00025914
		// (set) Token: 0x0600048E RID: 1166 RVA: 0x00026930 File Offset: 0x00025930
		private bool GridVisible
		{
			get
			{
				return FormParamsSaver.IntToBool(this.formParamsSaver.FormParams.GetInt(ObjectEditorForm.formParamsSaver_gridIntIndex));
			}
			set
			{
				this.formParamsSaver.FormParams.SetInt(ObjectEditorForm.formParamsSaver_gridIntIndex, FormParamsSaver.BoolToInt(value));
			}
		}

		// Token: 0x170000A6 RID: 166
		// (get) Token: 0x0600048F RID: 1167 RVA: 0x0002694D File Offset: 0x0002594D
		// (set) Token: 0x06000490 RID: 1168 RVA: 0x00026969 File Offset: 0x00025969
		private bool CollisionGeometryVisible
		{
			get
			{
				return FormParamsSaver.IntToBool(this.formParamsSaver.FormParams.GetInt(ObjectEditorForm.formParamsSaver_collisionGeometryIntIndex));
			}
			set
			{
				this.formParamsSaver.FormParams.SetInt(ObjectEditorForm.formParamsSaver_collisionGeometryIntIndex, FormParamsSaver.BoolToInt(value));
			}
		}

		// Token: 0x06000491 RID: 1169 RVA: 0x00026988 File Offset: 0x00025988
		public ObjectEditorForm(MainForm.Context _context) : base(EditorEnvironment.EditorFormsFolder + "ObjectEditorForm.xml", _context)
		{
			this.context = _context;
			this.InitializeComponent();
			this.formParamsSaver = new FormParamsSaver(this, EditorEnvironment.EditorFormsFolder + "ObjectEditorForm.xml", false);
			this.formParamsSaver.LoadParams += this.OnLoadParams;
			this.context.StateContainer.AddMethod("_open_object_in_object_editor", new Method(this.OnEditObject));
			this.context.StateContainer.AddMethod("_add_object_to_object_editor", new Method(this.OnAddObject));
		}

		// Token: 0x06000492 RID: 1170 RVA: 0x00026A54 File Offset: 0x00025A54
		public void Init()
		{
			this.CreateStateContainer();
			this.CreateOperationContainer();
			this.CreateScene();
			this.objectEditorEditState = new ObjectEditorEditState(this.context, this.stateContainer, this.operationContainer, this, this.editorScene, this.editorViewID);
			this.CreateStates();
			this.stateContainer.AddMethod("close_object_editor", new Method(this.OnCloseEditor));
			this.stateContainer.AddMethod("toggle_toolbar", new Method(this.OnToggleToolbar));
			this.stateContainer.AddMethod("toggle_statusbar", new Method(this.OnToggleStatusbar));
			this.stateContainer.AddMethod("toggle_grid", new Method(this.OnToggleGrid));
			this.stateContainer.AddMethod("toggle_collision_geometry", new Method(this.OnToggleCollisionGeometry));
			this.stateContainer.AddMethod("set_obj_editor_background_color", new Method(this.OnSetBackGround));
			this.stateContainer.AddMethod("generate_object_icon", new Method(this.OnGenerateObjectIcon));
			this.objectEditorEditState.AddMethod("recent_object_0", new Method(this.OnOpenRecentObject));
			this.objectEditorEditState.AddMethod("recent_object_1", new Method(this.OnOpenRecentObject));
			this.objectEditorEditState.AddMethod("recent_object_2", new Method(this.OnOpenRecentObject));
			this.objectEditorEditState.AddMethod("recent_object_3", new Method(this.OnOpenRecentObject));
			this.objectEditorEditState.AddMethod("recent_object_4", new Method(this.OnOpenRecentObject));
			this.objectEditorEditState.AddMethod("recent_object_5", new Method(this.OnOpenRecentObject));
			this.objectEditorEditState.AddMethod("recent_object_6", new Method(this.OnOpenRecentObject));
			this.objectEditorEditState.AddMethod("recent_object_7", new Method(this.OnOpenRecentObject));
			this.objectEditorEditState.AddMethod("recent_object_8", new Method(this.OnOpenRecentObject));
			this.objectEditorEditState.AddMethod("recent_object_9", new Method(this.OnOpenRecentObject));
			this.CreateRecentList();
		}

		// Token: 0x06000493 RID: 1171 RVA: 0x00026C85 File Offset: 0x00025C85
		public void Cleanup()
		{
			this.DestroyStates();
			this.objectEditorEditState.Destroy();
			this.objectEditorEditState = null;
			this.DestroyScene();
			this.DestroyOperationContainer();
			this.DestroyStateContainer();
			this.DestroyRecentList();
		}

		// Token: 0x06000494 RID: 1172 RVA: 0x00026CB8 File Offset: 0x00025CB8
		private void CreateScene()
		{
			this.editorScene = new EditorScene();
			this.editorScene.Create(this, 2);
			this.editorScene.SetFogMode(false);
			this.editorViewID = this.editorScene.CreateView(this);
			this.UpdateSceneAspect();
			this.sceneDrawTimer.Interval = 20;
			this.sceneDrawTimer.Enabled = true;
			this.editorSceneParams = Serializer.Load<EditorSceneParams>(EditorEnvironment.EditorFormsFolder + "ObjectEditorSceneParams.xml");
			if (this.editorSceneParams == null)
			{
				this.editorSceneParams = new EditorSceneParams();
			}
			EditorSceneParams editorSceneParams = this.editorSceneParams;
			editorSceneParams.BackgroundColorChanged = (EditorSceneParams.ParamsEvent)Delegate.Combine(editorSceneParams.BackgroundColorChanged, new EditorSceneParams.ParamsEvent(this.OnBackgroundColorChanged));
			this.OnBackgroundColorChanged(this.editorSceneParams);
		}

		// Token: 0x06000495 RID: 1173 RVA: 0x00026D7C File Offset: 0x00025D7C
		private void DestroyScene()
		{
			Serializer.Save(EditorEnvironment.EditorFormsFolder + "ObjectEditorSceneParams.xml", this.editorSceneParams, false);
			EditorSceneParams editorSceneParams = this.editorSceneParams;
			editorSceneParams.BackgroundColorChanged = (EditorSceneParams.ParamsEvent)Delegate.Remove(editorSceneParams.BackgroundColorChanged, new EditorSceneParams.ParamsEvent(this.OnBackgroundColorChanged));
			this.editorScene.DeleteUserGeometry(this.gridID);
			this.gridID = -1;
			this.sceneDrawTimer.Enabled = false;
			if (this.editorScene != null)
			{
				this.editorScene.Destroy();
			}
		}

		// Token: 0x06000496 RID: 1174 RVA: 0x00026E03 File Offset: 0x00025E03
		private void UpdateScene()
		{
			if (this.editorSceneReadyForUpdate)
			{
				this.editorSceneReadyForUpdate = false;
				if (this.editorScene != null)
				{
					this.editorScene.Step(this.editorViewID, false);
				}
				this.editorSceneReadyForUpdate = true;
			}
		}

		// Token: 0x06000497 RID: 1175 RVA: 0x00026E38 File Offset: 0x00025E38
		private void UpdateSceneAspect()
		{
			if (this.editorScene != null && base.ClientRectangle.Width != 0 && this.editorScene != null)
			{
				this.editorScene.SetAspect(this.editorViewID, (float)base.ClientRectangle.Height / (float)base.ClientRectangle.Width);
				this.UpdateScene();
			}
		}

		// Token: 0x06000498 RID: 1176 RVA: 0x00026E9B File Offset: 0x00025E9B
		private void AssignIcons()
		{
			this.imageEnumerator.Collect(this);
			this.imageEnumerator.Assign(this);
		}

		// Token: 0x06000499 RID: 1177 RVA: 0x00026EB8 File Offset: 0x00025EB8
		private void ResizeStatusBar()
		{
			int messageWidth = 500;
			int positionWidth = 300;
			int nShift = 18;
			int width = this.statusStrip.Size.Width - nShift;
			if (width > messageWidth + positionWidth + nShift)
			{
				this.StatusbarMessge.Width = width - positionWidth;
				this.StatusbarPosition.Width = positionWidth;
				return;
			}
			this.StatusbarMessge.Width = messageWidth * width / (messageWidth + positionWidth);
			this.StatusbarPosition.Width = positionWidth * width / (messageWidth + positionWidth);
		}

		// Token: 0x0600049A RID: 1178 RVA: 0x00026F36 File Offset: 0x00025F36
		private void sceneDrawTimer_Tick(object sender, EventArgs e)
		{
			if (base.Visible)
			{
				this.sceneDrawTimer.Stop();
				this.UpdateScene();
				this.sceneDrawTimer.Start();
			}
		}

		// Token: 0x0600049B RID: 1179 RVA: 0x00026F5C File Offset: 0x00025F5C
		private void ObjectEditorForm_Load(object sender, EventArgs e)
		{
			this.Init();
			this.AssignIcons();
			this.ResizeStatusBar();
		}

		// Token: 0x0600049C RID: 1180 RVA: 0x00026F70 File Offset: 0x00025F70
		private void ObjectEditorForm_Resize(object sender, EventArgs e)
		{
			this.UpdateSceneAspect();
		}

		// Token: 0x0600049D RID: 1181 RVA: 0x00026F78 File Offset: 0x00025F78
		private void OnEditObject(MethodArgs methodArgs)
		{
			if (!base.Visible)
			{
				base.Context.StateContainer.Invoke("toggle_object_editor", default(MethodArgs));
			}
			if (this.objectEditorEditState != null)
			{
				base.Visible = true;
				base.Activate();
				this.objectEditorEditState.Load(methodArgs.sender.ToString());
			}
		}

		// Token: 0x0600049E RID: 1182 RVA: 0x00026FD8 File Offset: 0x00025FD8
		private void OnAddObject(MethodArgs methodArgs)
		{
			if (!base.Visible)
			{
				base.Context.StateContainer.Invoke("toggle_object_editor", default(MethodArgs));
			}
			if (this.objectEditorEditState != null)
			{
				MapObjectInfo info = methodArgs.sender as MapObjectInfo;
				if (info != null)
				{
					base.Visible = true;
					base.Activate();
					this.objectEditorEditState.AddObject(info.sceneName, info.position, info.rotation, info.scale);
				}
			}
		}

		// Token: 0x0600049F RID: 1183 RVA: 0x00027053 File Offset: 0x00026053
		private void OnLoadParams(FormParams formParams)
		{
			formParams.ResizeInt(ObjectEditorForm.formParamsSaver_intCount, ObjectEditorForm.formParamsSaver_intDefaultValues);
			formParams.ResizeString(ObjectEditorForm.formParamsSaver_stringCount);
			this.UpdateToolbar();
			this.UpdateStatusStrip();
			this.UpdateGrid();
			this.UpdateCollisionGeometry();
		}

		// Token: 0x060004A0 RID: 1184 RVA: 0x00027088 File Offset: 0x00026088
		private void OnCloseEditor(MethodArgs methodArgs)
		{
			base.Close();
		}

		// Token: 0x060004A1 RID: 1185 RVA: 0x00027090 File Offset: 0x00026090
		private void ToggleToolbar()
		{
			this.ToolbarVisible = !this.ToolbarVisible;
			this.UpdateToolbar();
		}

		// Token: 0x060004A2 RID: 1186 RVA: 0x000270A7 File Offset: 0x000260A7
		private void ToggleStatusbar()
		{
			this.StatusbarVisible = !this.StatusbarVisible;
			this.UpdateStatusStrip();
		}

		// Token: 0x060004A3 RID: 1187 RVA: 0x000270BE File Offset: 0x000260BE
		private void OnToggleToolbar(MethodArgs methodArgs)
		{
			this.ToggleToolbar();
		}

		// Token: 0x060004A4 RID: 1188 RVA: 0x000270C6 File Offset: 0x000260C6
		private void OnToggleStatusbar(MethodArgs methodArgs)
		{
			this.ToggleStatusbar();
		}

		// Token: 0x060004A5 RID: 1189 RVA: 0x000270CE File Offset: 0x000260CE
		private void UpdateToolbar()
		{
			if (this.ToolbarVisible)
			{
				this.toolStrip.Show();
			}
			else
			{
				this.toolStrip.Hide();
			}
			this.stateContainer.SetMethodParams("toggle_toolbar", false, false, true, this.ToolbarVisible);
		}

		// Token: 0x060004A6 RID: 1190 RVA: 0x00027109 File Offset: 0x00026109
		private void UpdateStatusStrip()
		{
			if (this.StatusbarVisible)
			{
				this.statusStrip.Show();
			}
			else
			{
				this.statusStrip.Hide();
			}
			this.stateContainer.SetMethodParams("toggle_statusbar", false, false, true, this.StatusbarVisible);
		}

		// Token: 0x060004A7 RID: 1191 RVA: 0x00027144 File Offset: 0x00026144
		private void OnToggleGrid(MethodArgs methodArgs)
		{
			this.GridVisible = !this.GridVisible;
			this.UpdateGrid();
		}

		// Token: 0x060004A8 RID: 1192 RVA: 0x0002715B File Offset: 0x0002615B
		private void OnToggleCollisionGeometry(MethodArgs methodArgs)
		{
			this.CollisionGeometryVisible = !this.CollisionGeometryVisible;
			this.UpdateCollisionGeometry();
		}

		// Token: 0x060004A9 RID: 1193 RVA: 0x00027172 File Offset: 0x00026172
		private void OnSetBackGround(MethodArgs methodArgs)
		{
			this.editorSceneParams.ShowColorDialog("set_background_color");
		}

		// Token: 0x060004AA RID: 1194 RVA: 0x00027184 File Offset: 0x00026184
		private void OnBackgroundColorChanged(EditorSceneParams _editorSceneParams)
		{
			if (this.editorScene != null)
			{
				this.editorScene.SetBackgroundColor(this.editorViewID, Color.FromArgb(_editorSceneParams.BackgroundColor));
			}
		}

		// Token: 0x060004AB RID: 1195 RVA: 0x000271AC File Offset: 0x000261AC
		private void UpdateGrid()
		{
			if (this.GridVisible)
			{
				this.gridID = this.editorScene.CreateUserGeometry_Grid(this.gridID, 0.0, 50, 1.0, Color.FromArgb(100, Color.White), false);
			}
			else if (this.gridID != -1)
			{
				this.editorScene.DeleteUserGeometry(this.gridID);
				this.gridID = -1;
			}
			this.stateContainer.SetMethodParams("toggle_grid", false, false, true, this.GridVisible);
		}

		// Token: 0x060004AC RID: 1196 RVA: 0x00027235 File Offset: 0x00026235
		private void UpdateCollisionGeometry()
		{
			this.editorScene.ShowCollisionGeometry(this.CollisionGeometryVisible);
			this.stateContainer.SetMethodParams("toggle_collision_geometry", false, false, true, this.CollisionGeometryVisible);
		}

		// Token: 0x060004AD RID: 1197 RVA: 0x00027264 File Offset: 0x00026264
		private void OnGenerateObjectIcon(MethodArgs methodArgs)
		{
			bool somethingHidden = false;
			bool hideGrid = this.GridVisible;
			if (hideGrid)
			{
				this.OnToggleGrid(new MethodArgs(this, null, null));
				somethingHidden = true;
			}
			bool hideCollisionGeometry = this.CollisionGeometryVisible;
			if (hideCollisionGeometry)
			{
				this.OnToggleCollisionGeometry(new MethodArgs(this, null, null));
				somethingHidden = true;
			}
			if (somethingHidden && this.editorScene != null)
			{
				this.editorScene.Step(this.editorViewID, false);
			}
			int leftOffset = (base.Width - base.ClientSize.Width) / 2;
			int bottomOffset = leftOffset + (this.statusStrip.Visible ? this.statusStrip.Height : 0);
			int upOffset = base.Height - base.ClientSize.Height - leftOffset + (this.menuStrip.Visible ? this.menuStrip.Height : 0) + (this.toolStrip.Visible ? this.toolStrip.Height : 0);
			Point upperLeftCorner = new Point(base.Left + leftOffset, base.Top + upOffset);
			Size size = new Size(base.Width - 2 * leftOffset, base.Height - upOffset - bottomOffset);
			if (size.Width > size.Height)
			{
				int delta = (size.Width - size.Height) / 2;
				upperLeftCorner.X += delta;
				size.Width = size.Height;
			}
			else
			{
				int delta2 = (size.Height - size.Width) / 2;
				upperLeftCorner.Y += delta2;
				size.Height = size.Width;
			}
			if (size.Width < 1)
			{
				return;
			}
			Rectangle screenshotRect = new Rectangle(upperLeftCorner, size);
			this.stateContainer.Invoke("_object_editor_make_screenshot", new MethodArgs(this, screenshotRect, null));
			if (hideGrid)
			{
				this.OnToggleGrid(new MethodArgs(this, null, null));
			}
			if (hideCollisionGeometry)
			{
				this.OnToggleCollisionGeometry(new MethodArgs(this, null, null));
			}
		}

		// Token: 0x060004AE RID: 1198 RVA: 0x0002744D File Offset: 0x0002644D
		private void ObjectEditorForm_FormClosed(object sender, FormClosedEventArgs e)
		{
			this.Cleanup();
		}

		// Token: 0x060004AF RID: 1199 RVA: 0x00027455 File Offset: 0x00026455
		private void statusStrip_Resize(object sender, EventArgs e)
		{
			this.ResizeStatusBar();
		}

		// Token: 0x060004B0 RID: 1200 RVA: 0x00027460 File Offset: 0x00026460
		private void OnOpenRecentObject(MethodArgs methodArgs)
		{
			ToolStripItem toolStripItem = methodArgs.sender as ToolStripItem;
			if (toolStripItem != null && toolStripItem.Tag != null)
			{
				string tag = toolStripItem.Tag.ToString();
				string item;
				if (this.ObjectRecentList.GetItem(tag, out item))
				{
					this.stateContainer.Invoke("_object_editor_load_object", new MethodArgs(this, item, null));
				}
			}
		}

		// Token: 0x060004B1 RID: 1201 RVA: 0x000274B9 File Offset: 0x000264B9
		public void UpdateStatusbar()
		{
			this.statusStrip.Update();
		}

		// Token: 0x170000A7 RID: 167
		// (get) Token: 0x060004B2 RID: 1202 RVA: 0x000274C6 File Offset: 0x000264C6
		public ToolStripStatusLabel StatusHelp
		{
			get
			{
				return null;
			}
		}

		// Token: 0x170000A8 RID: 168
		// (get) Token: 0x060004B3 RID: 1203 RVA: 0x000274C9 File Offset: 0x000264C9
		public ToolStripStatusLabel StatusMessage
		{
			get
			{
				return this.StatusbarMessge;
			}
		}

		// Token: 0x170000A9 RID: 169
		// (get) Token: 0x060004B4 RID: 1204 RVA: 0x000274D1 File Offset: 0x000264D1
		public ToolStripStatusLabel StatusPosition
		{
			get
			{
				return this.StatusbarPosition;
			}
		}

		// Token: 0x170000AA RID: 170
		// (get) Token: 0x060004B5 RID: 1205 RVA: 0x000274D9 File Offset: 0x000264D9
		public DBID StaticObjectDBID
		{
			get
			{
				if (this.objectEditorEditState == null)
				{
					return null;
				}
				return this.objectEditorEditState.StaticObjectDBID;
			}
		}

		// Token: 0x060004B6 RID: 1206 RVA: 0x000274F0 File Offset: 0x000264F0
		private void CreateOperationContainer()
		{
			this.operationContainer = new OperationContainer();
			this.operationState = new State("OperationState");
			this.operationState.AddMethod("undo", new Method(this.OnUndo));
			this.operationState.AddMethod("redo", new Method(this.OnRedo));
			State state = this.operationState;
			state.EnterState = (State.ActivateEvent)Delegate.Combine(state.EnterState, new State.ActivateEvent(this.OnEnterOperationState));
			this.operationContainer.BuffersChanged += this.OnOperationContainerEvent;
			this.operationContainer.UndoInvoked += this.OnOperationContainerEvent;
			this.operationContainer.RedoInvoked += this.OnOperationContainerEvent;
		}

		// Token: 0x060004B7 RID: 1207 RVA: 0x000275BC File Offset: 0x000265BC
		private void DestroyOperationContainer()
		{
			State state = this.operationState;
			state.EnterState = (State.ActivateEvent)Delegate.Remove(state.EnterState, new State.ActivateEvent(this.OnEnterOperationState));
			this.operationContainer.BuffersChanged -= this.OnOperationContainerEvent;
			this.operationContainer.UndoInvoked -= this.OnOperationContainerEvent;
			this.operationContainer.RedoInvoked -= this.OnOperationContainerEvent;
			this.operationContainer = null;
			this.operationState = null;
		}

		// Token: 0x060004B8 RID: 1208 RVA: 0x00027643 File Offset: 0x00026643
		private void UpdateOperationStateMethodParams()
		{
			this.operationState.SetMethodParams("undo", true, this.operationContainer.CanUndo, false, false);
			this.operationState.SetMethodParams("redo", true, this.operationContainer.CanRedo, false, false);
		}

		// Token: 0x060004B9 RID: 1209 RVA: 0x00027681 File Offset: 0x00026681
		private void OnEnterOperationState(IState _operationState)
		{
			this.UpdateOperationStateMethodParams();
		}

		// Token: 0x060004BA RID: 1210 RVA: 0x00027689 File Offset: 0x00026689
		private void OnOperationContainerEvent(OperationContainer _operationContainer)
		{
			this.UpdateOperationStateMethodParams();
		}

		// Token: 0x060004BB RID: 1211 RVA: 0x00027691 File Offset: 0x00026691
		private void OnUndo(MethodArgs methodArgs)
		{
			this.operationContainer.Undo(1);
			this.context.EditorScene.SynchronizeObjects(false);
		}

		// Token: 0x060004BC RID: 1212 RVA: 0x000276B1 File Offset: 0x000266B1
		private void OnRedo(MethodArgs methodArgs)
		{
			this.operationContainer.Redo(1);
			this.context.EditorScene.SynchronizeObjects(false);
		}

		// Token: 0x060004BD RID: 1213 RVA: 0x000276D1 File Offset: 0x000266D1
		private void CreateRecentList()
		{
			this.objectRecentListDataMiner = new ObjectEditorForm.ObjectRecentItemDataMiner();
			this.objectRecentList = new RecentList(EditorEnvironment.EditorFormsFolder + "ObjectEditor_Open_RecentList.xml", this.recentFilesToolStripMenuItem, this.objectRecentListDataMiner);
			this.objectRecentList.LoadRecentList();
		}

		// Token: 0x060004BE RID: 1214 RVA: 0x0002770F File Offset: 0x0002670F
		private void DestroyRecentList()
		{
			this.objectRecentList.SaveRecentList();
			this.objectRecentList = null;
			this.objectRecentListDataMiner = null;
		}

		// Token: 0x170000AB RID: 171
		// (get) Token: 0x060004BF RID: 1215 RVA: 0x0002772A File Offset: 0x0002672A
		public RecentList ObjectRecentList
		{
			get
			{
				return this.objectRecentList;
			}
		}

		// Token: 0x060004C0 RID: 1216 RVA: 0x00027734 File Offset: 0x00026734
		// Note: this type is marked as 'beforefieldinit'.
		static ObjectEditorForm()
		{
			int[] array = new int[4];
			array[0] = 1;
			array[2] = 1;
			ObjectEditorForm.formParamsSaver_intDefaultValues = array;
			ObjectEditorForm.formParamsSaver_openFileFolderStringIndex = 0;
			ObjectEditorForm.formParamsSaver_stringCount = 1;
		}

		// Token: 0x04000377 RID: 887
		private StateContainer stateContainer;

		// Token: 0x04000378 RID: 888
		private Accelerators accelerators;

		// Token: 0x04000379 RID: 889
		private Events events;

		// Token: 0x0400037A RID: 890
		private readonly MainForm.Context context;

		// Token: 0x0400037B RID: 891
		private EditorScene editorScene;

		// Token: 0x0400037C RID: 892
		private bool editorSceneReadyForUpdate = true;

		// Token: 0x0400037D RID: 893
		private int editorViewID;

		// Token: 0x0400037E RID: 894
		private int gridID = -1;

		// Token: 0x0400037F RID: 895
		private ObjectEditorEditState objectEditorEditState;

		// Token: 0x04000380 RID: 896
		private static readonly int formParamsSaver_toolbarIntIndex = 0;

		// Token: 0x04000381 RID: 897
		private static readonly int formParamsSaver_statusbarIntIndex = 1;

		// Token: 0x04000382 RID: 898
		private static readonly int formParamsSaver_gridIntIndex = 2;

		// Token: 0x04000383 RID: 899
		private static readonly int formParamsSaver_collisionGeometryIntIndex = 3;

		// Token: 0x04000384 RID: 900
		private static readonly int formParamsSaver_intCount = 4;

		// Token: 0x04000385 RID: 901
		private static readonly int[] formParamsSaver_intDefaultValues;

		// Token: 0x04000386 RID: 902
		private static readonly int formParamsSaver_openFileFolderStringIndex;

		// Token: 0x04000387 RID: 903
		private static readonly int formParamsSaver_stringCount;

		// Token: 0x04000388 RID: 904
		private readonly FormParamsSaver formParamsSaver;

		// Token: 0x04000389 RID: 905
		private EditorSceneParams editorSceneParams = new EditorSceneParams();

		// Token: 0x0400038A RID: 906
		private readonly ImageEnumerator imageEnumerator = new ImageEnumerator();

		// Token: 0x0400038B RID: 907
		private OperationContainer operationContainer;

		// Token: 0x0400038C RID: 908
		private State operationState;

		// Token: 0x0400038D RID: 909
		private RecentList objectRecentList;

		// Token: 0x0400038E RID: 910
		private ObjectEditorForm.ObjectRecentItemDataMiner objectRecentListDataMiner;

		// Token: 0x0200005B RID: 91
		public class ObjectRecentItemDataMiner : RecentList.IItemDataMiner
		{
			// Token: 0x060004C1 RID: 1217 RVA: 0x00027780 File Offset: 0x00026780
			public bool GetItemData(string item, out RecentList.ItemData itemData)
			{
				itemData = new RecentList.ItemData();
				IDatabase mainDb = IDatabase.GetMainDatabase();
				if (mainDb != null)
				{
					DBID dbid = mainDb.GetDBIDByName(item);
					if (!DBID.IsNullOrEmpty(dbid))
					{
						itemData.Text = dbid.GetFileShortName();
						return true;
					}
				}
				itemData.Text = string.Empty;
				return false;
			}
		}
	}
}
