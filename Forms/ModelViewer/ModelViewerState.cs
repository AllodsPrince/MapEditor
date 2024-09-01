using System;
using System.Windows.Forms;
using InputState;
using LauncherTools.InputState;
using Tools.InputState;
using Tools.ModelViewerElements.Model;
using Tools.ModelViewerElements.States;

namespace MapEditor.Forms.ModelViewer
{
	// Token: 0x020002AE RID: 686
	public class ModelViewerState : State
	{
		// Token: 0x06001FB0 RID: 8112 RVA: 0x000CB6D4 File Offset: 0x000CA6D4
		private void OnSetModelScale(MethodArgs args)
		{
			string scaleString = args.sender as string;
			float scale;
			if (!string.IsNullOrEmpty(scaleString) && float.TryParse(scaleString, out scale))
			{
				this.model.Scale = scale;
			}
		}

		// Token: 0x06001FB1 RID: 8113 RVA: 0x000CB70C File Offset: 0x000CA70C
		private void OnEnterState(IState state)
		{
			base.Container.BindState(this.dressItemState);
			base.Container.BindState(this.characterBasicVarState);
			base.Container.BindState(this.changeCurrentCharacterState);
			base.Container.BindState(this.characterProportionsVarState);
			if (this.model.CurrentModelIndex != -1)
			{
				base.Container.BindState(this.selectVisMobExtElemState);
			}
			base.Container.Invoke("_model_viewer_load_model_scale", new MethodArgs(null, this.model.Scale.ToString(), null));
		}

		// Token: 0x06001FB2 RID: 8114 RVA: 0x000CB7A8 File Offset: 0x000CA7A8
		private void OnLeaveState(IState state)
		{
			base.Container.UnbindState(this.dressItemState);
			base.Container.UnbindState(this.characterBasicVarState);
			base.Container.UnbindState(this.changeCurrentCharacterState);
			base.Container.UnbindState(this.characterProportionsVarState);
			if (this.selectVisMobExtElemState.Binded)
			{
				base.Container.UnbindState(this.selectVisMobExtElemState);
			}
			base.Container.Invoke("_model_viewer_load_model_scale", new MethodArgs(null, string.Empty, null));
		}

		// Token: 0x06001FB3 RID: 8115 RVA: 0x000CB834 File Offset: 0x000CA834
		public ModelViewerState(Model _model, EditorScene editorScene, IWin32Window _mainForm) : base("ModelViewerState")
		{
			this.model = _model;
			this.dressItemState = new DressItemState(this.model, editorScene);
			this.characterBasicVarState = new CharacterBasicVarState(this.model, editorScene, _mainForm);
			this.selectVisMobExtElemState = new SelectVisMobExtElemState(this.model, editorScene);
			this.changeCurrentCharacterState = new ChangeCurrentCharacterState(this.model);
			this.characterProportionsVarState = new CharacterProportionsVarState(this.model, editorScene);
			this.EnterState = (State.ActivateEvent)Delegate.Combine(this.EnterState, new State.ActivateEvent(this.OnEnterState));
			this.LeaveState = (State.ActivateEvent)Delegate.Combine(this.LeaveState, new State.ActivateEvent(this.OnLeaveState));
			base.AddMethod("_model_viewer_set_model_scale", new Method(this.OnSetModelScale));
		}

		// Token: 0x04001385 RID: 4997
		private readonly Model model;

		// Token: 0x04001386 RID: 4998
		private readonly DressItemState dressItemState;

		// Token: 0x04001387 RID: 4999
		private readonly CharacterBasicVarState characterBasicVarState;

		// Token: 0x04001388 RID: 5000
		private readonly SelectVisMobExtElemState selectVisMobExtElemState;

		// Token: 0x04001389 RID: 5001
		private readonly ChangeCurrentCharacterState changeCurrentCharacterState;

		// Token: 0x0400138A RID: 5002
		private readonly CharacterProportionsVarState characterProportionsVarState;
	}
}
