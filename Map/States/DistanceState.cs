using System;
using System.Windows.Forms;
using InputState;
using LauncherTools.InputState;
using Tools.Geometry;
using Tools.InputState;
using Tools.MapObjects;
using Tools.Statusbar;

namespace MapEditor.Map.States
{
	// Token: 0x02000222 RID: 546
	internal class DistanceState : State
	{
		// Token: 0x1700063F RID: 1599
		// (get) Token: 0x06001A5A RID: 6746 RVA: 0x000ADA53 File Offset: 0x000ACA53
		// (set) Token: 0x06001A59 RID: 6745 RVA: 0x000ADA4A File Offset: 0x000ACA4A
		public string PreviousState
		{
			get
			{
				return this.previousState;
			}
			set
			{
				this.previousState = value;
			}
		}

		// Token: 0x06001A5B RID: 6747 RVA: 0x000ADA5C File Offset: 0x000ACA5C
		public DistanceState(PolygonContainer _polygonContainer, StateContainer _stateContainer, MultiState _multiState, ICollisionMap _collisionMap, EditorScene _editorScene, int _editorSceneViewID, IStatusbar _statusbar) : base("MapEditorDistanceState")
		{
			this.polygonContainer = _polygonContainer;
			this.stateContainer = _stateContainer;
			this.multiState = _multiState;
			this.collisionMap = _collisionMap;
			this.editorScene = _editorScene;
			this.editorSceneViewID = _editorSceneViewID;
			this.statusbar = _statusbar;
			if (this.stateContainer != null)
			{
				this.EnterState = (State.ActivateEvent)Delegate.Combine(this.EnterState, new State.ActivateEvent(this.OnEnterState));
				this.LeaveState = (State.ActivateEvent)Delegate.Combine(this.LeaveState, new State.ActivateEvent(this.OnLeaveState));
			}
			base.AddMethod("mouse_move", new Method(this.OnMouseMove));
			base.AddMethod("mouse_up", new Method(this.OnMouseUp));
			base.AddMethod("break", new Method(this.OnBreak));
			base.AddMethod("focus_on", new Method(this.OnFocusOn));
		}

		// Token: 0x06001A5C RID: 6748 RVA: 0x000ADB70 File Offset: 0x000ACB70
		public void Destroy()
		{
			this.Clear();
			if (this.stateContainer != null)
			{
				this.EnterState = (State.ActivateEvent)Delegate.Remove(this.EnterState, new State.ActivateEvent(this.OnEnterState));
				this.LeaveState = (State.ActivateEvent)Delegate.Remove(this.LeaveState, new State.ActivateEvent(this.OnLeaveState));
			}
			this.polygonContainer = null;
			this.stateContainer = null;
			this.multiState = null;
			this.collisionMap = null;
			this.editorScene = null;
			this.editorSceneViewID = -1;
			this.statusbar = null;
		}

		// Token: 0x06001A5D RID: 6749 RVA: 0x000ADC00 File Offset: 0x000ACC00
		public void Begin()
		{
			if (this.multiState != null && this.stateContainer != null)
			{
				this.multiState.ActiveStateLabel = base.Label;
				this.stateContainer.SetMethodParams("toggle_measure_distance", true, true, true, true);
			}
		}

		// Token: 0x06001A5E RID: 6750 RVA: 0x000ADC37 File Offset: 0x000ACC37
		public void End()
		{
			if (this.multiState != null && this.stateContainer != null)
			{
				this.multiState.ActiveStateLabel = this.previousState;
				this.stateContainer.SetMethodParams("toggle_measure_distance", true, true, true, false);
			}
		}

		// Token: 0x06001A5F RID: 6751 RVA: 0x000ADC6E File Offset: 0x000ACC6E
		private void RemovePoint()
		{
			if (this.polygon != null && this.polygon.Count > 1)
			{
				this.polygon.DeletePoint(this.polygon.Count - 2);
			}
		}

		// Token: 0x06001A60 RID: 6752 RVA: 0x000ADCA0 File Offset: 0x000ACCA0
		private void AddPoint()
		{
			if (this.polygon != null)
			{
				if (this.polygon.Count > 0)
				{
					this.polygon.InsertPoint(this.polygon.Count - 1, this.lastPosition.Vec3, true, false);
					return;
				}
				this.polygon.AddPoint(this.lastPosition.Vec3, false, false);
			}
		}

		// Token: 0x06001A61 RID: 6753 RVA: 0x000ADD04 File Offset: 0x000ACD04
		private void Create()
		{
			this.Clear();
			if (this.polygonContainer != null)
			{
				int polygonID = this.polygonContainer.AddPolygon();
				this.polygonContainer.Polygons.TryGetValue(polygonID, out this.polygon);
			}
		}

		// Token: 0x06001A62 RID: 6754 RVA: 0x000ADD43 File Offset: 0x000ACD43
		private void Clear()
		{
			if (this.polygonContainer != null && this.polygon != null)
			{
				this.polygonContainer.RemovePolygon(this.polygon.ID);
				this.polygon = null;
			}
		}

		// Token: 0x06001A63 RID: 6755 RVA: 0x000ADD74 File Offset: 0x000ACD74
		private void OnEnterState(IState state)
		{
			this.Create();
			Point mouseCursorPos;
			this.editorScene.GetMouseCursorPos(this.editorSceneViewID, out mouseCursorPos);
			this.UpdatePosition(mouseCursorPos.X, mouseCursorPos.Y);
			this.AddPoint();
		}

		// Token: 0x06001A64 RID: 6756 RVA: 0x000ADDB4 File Offset: 0x000ACDB4
		private void OnLeaveState(IState state)
		{
			this.Clear();
		}

		// Token: 0x06001A65 RID: 6757 RVA: 0x000ADDBC File Offset: 0x000ACDBC
		private void OnMouseMove(MethodArgs methodArgs)
		{
			MouseEventArgs mouseEventArgs = methodArgs.eventArgs as MouseEventArgs;
			if (mouseEventArgs != null && !KeyStatus.RightMouse)
			{
				this.UpdatePosition(mouseEventArgs.X, mouseEventArgs.Y);
			}
		}

		// Token: 0x06001A66 RID: 6758 RVA: 0x000ADDF4 File Offset: 0x000ACDF4
		private void OnMouseUp(MethodArgs methodArgs)
		{
			MouseEventArgs mouseEventArgs = methodArgs.eventArgs as MouseEventArgs;
			if (mouseEventArgs != null)
			{
				if (mouseEventArgs.Button == MouseButtons.Middle)
				{
					if (this.polygon.Count < 2)
					{
						this.End();
					}
					else
					{
						this.RemovePoint();
						this.UpdatePosition(mouseEventArgs.X, mouseEventArgs.Y);
					}
				}
				if (mouseEventArgs.Button == MouseButtons.Left)
				{
					this.UpdatePosition(mouseEventArgs.X, mouseEventArgs.Y);
					this.AddPoint();
					this.UpdatePosition(mouseEventArgs.X, mouseEventArgs.Y);
				}
			}
		}

		// Token: 0x06001A67 RID: 6759 RVA: 0x000ADE83 File Offset: 0x000ACE83
		private void OnBreak(MethodArgs methodArgs)
		{
			this.End();
		}

		// Token: 0x06001A68 RID: 6760 RVA: 0x000ADE8C File Offset: 0x000ACE8C
		private void OnFocusOn(MethodArgs methodArgs)
		{
			Vec3 distance = new Vec3(0.0, -10.0, 10.0);
			this.editorScene.SetAnchor(this.editorSceneViewID, ref this.lastPosition, ref distance);
		}

		// Token: 0x06001A69 RID: 6761 RVA: 0x000ADEDC File Offset: 0x000ACEDC
		private void UpdatePosition(int x, int y)
		{
			if (this.collisionMap != null)
			{
				this.collisionMap.PickPosition(x, y, TerrainSurface.Terrain, out this.lastPosition);
				if (this.polygon != null && this.polygon.Count > 0)
				{
					this.polygon.MovePointTo(this.polygon.Count - 1, this.lastPosition.Vec3);
				}
				if (this.statusbar != null)
				{
					if (this.statusbar.StatusPosition != null)
					{
						this.statusbar.StatusPosition.Text = string.Format("x:{0:0.###} y:{1:0.###} z:{2:0.###}, patch:[{3}, {4}]", new object[]
						{
							this.lastPosition.X,
							this.lastPosition.Y,
							this.lastPosition.Z,
							(int)(this.lastPosition.X / (double)Constants.PatchSize),
							(int)(this.lastPosition.Y / (double)Constants.PatchSize)
						});
					}
					if (this.statusbar.StatusMessage != null)
					{
						this.statusbar.StatusMessage.Text = string.Format("distance: {0:0.###}  points: {1}", this.polygon.Perimeter, this.polygon.Count);
					}
					this.statusbar.UpdateStatusbar();
				}
			}
		}

		// Token: 0x04001111 RID: 4369
		private PolygonContainer polygonContainer;

		// Token: 0x04001112 RID: 4370
		private StateContainer stateContainer;

		// Token: 0x04001113 RID: 4371
		private MultiState multiState;

		// Token: 0x04001114 RID: 4372
		private ICollisionMap collisionMap;

		// Token: 0x04001115 RID: 4373
		private EditorScene editorScene;

		// Token: 0x04001116 RID: 4374
		private int editorSceneViewID = -1;

		// Token: 0x04001117 RID: 4375
		private IStatusbar statusbar;

		// Token: 0x04001118 RID: 4376
		private string previousState = string.Empty;

		// Token: 0x04001119 RID: 4377
		private Position lastPosition = Position.Empty;

		// Token: 0x0400111A RID: 4378
		private Polygon polygon;
	}
}
