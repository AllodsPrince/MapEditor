using System;
using System.Drawing;
using System.Windows.Forms;
using Db;
using InputState;
using Tools.ModelViewerElements.Model;
using Tools.ModelViewerElements.Model.ModelTypes;

namespace MapEditor.Forms.ModelViewer
{
	// Token: 0x020001E7 RID: 487
	internal class ModelViewerScene
	{
		// Token: 0x140000AF RID: 175
		// (add) Token: 0x06001887 RID: 6279 RVA: 0x000A373A File Offset: 0x000A273A
		// (remove) Token: 0x06001888 RID: 6280 RVA: 0x000A3753 File Offset: 0x000A2753
		public event ModelViewerScene.ModelViewerSceneEvent ModelLoaded;

		// Token: 0x140000B0 RID: 176
		// (add) Token: 0x06001889 RID: 6281 RVA: 0x000A376C File Offset: 0x000A276C
		// (remove) Token: 0x0600188A RID: 6282 RVA: 0x000A3785 File Offset: 0x000A2785
		public event ModelViewerScene.ModelViewerModelTypeEvent ModelModified;

		// Token: 0x0600188B RID: 6283 RVA: 0x000A379E File Offset: 0x000A279E
		private void UpdateScene()
		{
			if (this.editorScene != null)
			{
				this.editorScene.Step(this.editorViewID, true);
			}
		}

		// Token: 0x0600188C RID: 6284 RVA: 0x000A37BA File Offset: 0x000A27BA
		private void OnSceneTimeTick(object sender, EventArgs e)
		{
			this.UpdateScene();
		}

		// Token: 0x0600188D RID: 6285 RVA: 0x000A37C2 File Offset: 0x000A27C2
		private void OnSceneControlResize(object sender, EventArgs e)
		{
			this.UpdateSceneAspect();
		}

		// Token: 0x0600188E RID: 6286 RVA: 0x000A37CC File Offset: 0x000A27CC
		private void UpdateSceneAspect()
		{
			if (this.editorScene != null && this.sceneTimer != null && this.sceneControl != null && this.sceneControl.Visible && this.sceneControl.ClientRectangle.Width != 0)
			{
				this.sceneTimer.Stop();
				this.editorScene.SetAspect(this.editorViewID, (float)this.sceneControl.ClientRectangle.Height / (float)this.sceneControl.ClientRectangle.Width);
				this.UpdateScene();
				this.sceneTimer.Start();
			}
		}

		// Token: 0x0600188F RID: 6287 RVA: 0x000A3870 File Offset: 0x000A2870
		private void OnModelLoaded()
		{
			if (this.editorScene != null && this.stateContainer != null && this.model != null && this.modelViewerState != null)
			{
				if (this.model.GetModelType() == VisualCharacter.TypeName || this.model.GetModelType() == VisualMob.TypeName)
				{
					this.stateContainer.BindState(this.modelViewerState);
				}
				AnimationProperties animProp = new AnimationProperties(1f);
				animProp.Name = this.model.GetDefaultAnimation();
				animProp.LowerName = animProp.Name;
				animProp.Lower = true;
				animProp.Upper = true;
				animProp.Looped = true;
				this.editorScene.PlayObjectAnimation(this.model.ModelID, ref animProp);
				if (this.ModelLoaded != null)
				{
					this.ModelLoaded();
				}
			}
		}

		// Token: 0x06001890 RID: 6288 RVA: 0x000A395C File Offset: 0x000A295C
		private void OnModelClosed()
		{
			if (this.stateContainer != null && this.modelViewerState != null && (this.model.GetModelType() == VisualCharacter.TypeName || this.model.GetModelType() == VisualMob.TypeName))
			{
				this.stateContainer.UnbindState(this.modelViewerState);
			}
		}

		// Token: 0x06001891 RID: 6289 RVA: 0x000A39B8 File Offset: 0x000A29B8
		private void OnModelModified()
		{
			if (this.model != null && this.ModelModified != null)
			{
				this.ModelModified(this.model.GetModelType());
			}
		}

		// Token: 0x06001892 RID: 6290 RVA: 0x000A39E0 File Offset: 0x000A29E0
		public ModelViewerScene(Control _sceneControl, Timer _sceneTimer, StateContainer _stateContainer)
		{
			this.sceneControl = _sceneControl;
			this.sceneTimer = _sceneTimer;
			this.stateContainer = _stateContainer;
		}

		// Token: 0x06001893 RID: 6291 RVA: 0x000A3A04 File Offset: 0x000A2A04
		public void CreateScene()
		{
			if (this.sceneControl != null)
			{
				this.editorScene = new EditorScene();
				this.editorScene.Create(this.sceneControl.Handle, 2);
				this.editorViewID = this.editorScene.CreateView(this.sceneControl);
				this.editorScene.SetBackgroundColor(this.editorViewID, Color.Black);
				this.UpdateSceneAspect();
				this.model = new Model(this.editorScene);
				this.model.ModelLoaded += this.OnModelLoaded;
				this.model.ModelClosed += this.OnModelClosed;
				this.model.ModelModified += this.OnModelModified;
				if (this.sceneTimer != null)
				{
					this.sceneTimer.Interval = 20;
					this.sceneTimer.Start();
					this.sceneTimer.Tick += this.OnSceneTimeTick;
				}
				this.sceneControl.Resize += this.OnSceneControlResize;
				this.modelViewerState = new ModelViewerState(this.model, this.editorScene, this.sceneControl);
			}
		}

		// Token: 0x06001894 RID: 6292 RVA: 0x000A3B34 File Offset: 0x000A2B34
		public void DestroyScene()
		{
			if (this.sceneControl != null)
			{
				this.sceneControl.Resize -= this.OnSceneControlResize;
			}
			if (this.sceneControl != null)
			{
				this.sceneTimer.Tick -= this.OnSceneTimeTick;
				this.sceneTimer.Stop();
			}
			if (this.model != null)
			{
				this.model.ModelLoaded -= this.OnModelLoaded;
				this.model.ModelClosed -= this.OnModelClosed;
				this.model.ModelModified -= this.OnModelModified;
				this.model.Delete(false);
				this.model = null;
			}
			if (this.editorScene != null)
			{
				this.editorScene.Destroy();
				this.editorScene = null;
			}
		}

		// Token: 0x06001895 RID: 6293 RVA: 0x000A3C08 File Offset: 0x000A2C08
		public void OpenModel(string name)
		{
			if (this.model != null && !string.IsNullOrEmpty(name))
			{
				bool setCamera = this.model.ModelID == -1;
				this.model.Create(name);
				if (setCamera)
				{
					this.CenterCamera();
				}
			}
		}

		// Token: 0x06001896 RID: 6294 RVA: 0x000A3C4C File Offset: 0x000A2C4C
		public void SaveModel()
		{
			IDatabase mainDb = IDatabase.GetMainDatabase();
			if (mainDb != null && this.model != null && this.model.ModelID != -1)
			{
				this.model.Save();
				mainDb.SaveChanges();
			}
		}

		// Token: 0x06001897 RID: 6295 RVA: 0x000A3C8C File Offset: 0x000A2C8C
		public void SaveAsModel(string dbid)
		{
			IDatabase mainDb = IDatabase.GetMainDatabase();
			if (mainDb != null && this.model != null && this.model.ModelID != -1)
			{
				this.model.SaveAs(dbid);
				mainDb.SaveChanges();
			}
		}

		// Token: 0x170005F5 RID: 1525
		// (get) Token: 0x06001898 RID: 6296 RVA: 0x000A3CCB File Offset: 0x000A2CCB
		public string SaveModelAsTargetType
		{
			get
			{
				return this.model.SaveAsTargetType;
			}
		}

		// Token: 0x06001899 RID: 6297 RVA: 0x000A3CD8 File Offset: 0x000A2CD8
		public DBID GetOpenedModelDBID()
		{
			if (this.model != null)
			{
				return this.model.ModelDBID;
			}
			return DBID.Empty;
		}

		// Token: 0x0600189A RID: 6298 RVA: 0x000A3CF3 File Offset: 0x000A2CF3
		public string GetOpenedModelType()
		{
			if (this.model != null)
			{
				return this.model.GetModelType();
			}
			return null;
		}

		// Token: 0x0600189B RID: 6299 RVA: 0x000A3D0A File Offset: 0x000A2D0A
		public void SetCameraSpeed(double speed)
		{
			if (this.editorScene != null)
			{
				this.editorScene.SetCameraMoveSpeed(this.editorViewID, speed);
			}
		}

		// Token: 0x0600189C RID: 6300 RVA: 0x000A3D26 File Offset: 0x000A2D26
		public void CenterCamera()
		{
			if (this.model != null && this.model != null)
			{
				this.model.CenterCamera(this.editorViewID);
			}
		}

		// Token: 0x0600189D RID: 6301 RVA: 0x000A3D49 File Offset: 0x000A2D49
		public void SetCameraType(string type)
		{
			if (this.editorScene != null)
			{
				this.editorScene.SetCameraType(this.editorViewID, type);
				this.CenterCamera();
			}
		}

		// Token: 0x0600189E RID: 6302 RVA: 0x000A3D6B File Offset: 0x000A2D6B
		public void Stop()
		{
			if (this.editorScene != null)
			{
				this.sceneTimer.Stop();
			}
		}

		// Token: 0x0600189F RID: 6303 RVA: 0x000A3D80 File Offset: 0x000A2D80
		public void Start()
		{
			if (this.editorScene != null)
			{
				this.sceneTimer.Start();
			}
		}

		// Token: 0x04000FF9 RID: 4089
		private readonly Timer sceneTimer;

		// Token: 0x04000FFA RID: 4090
		private readonly Control sceneControl;

		// Token: 0x04000FFB RID: 4091
		private readonly StateContainer stateContainer;

		// Token: 0x04000FFC RID: 4092
		private EditorScene editorScene;

		// Token: 0x04000FFD RID: 4093
		private int editorViewID = -1;

		// Token: 0x04000FFE RID: 4094
		private Model model;

		// Token: 0x04000FFF RID: 4095
		private ModelViewerState modelViewerState;

		// Token: 0x020001E8 RID: 488
		// (Invoke) Token: 0x060018A1 RID: 6305
		public delegate void ModelViewerSceneEvent();

		// Token: 0x020001E9 RID: 489
		// (Invoke) Token: 0x060018A5 RID: 6309
		public delegate void ModelViewerModelTypeEvent(string type);
	}
}
