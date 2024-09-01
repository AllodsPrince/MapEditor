using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using InputState;
using LauncherTools.InputState;
using MapEditor.Map.DataProviders;
using MapEditor.Map.Landscape;
using MapEditor.Resources.Strings;
using MapEditor.Scene;
using Operations;
using Tools.Geometry;
using Tools.InputState;
using Tools.ItemDataContainer;
using Tools.Landscape;
using Tools.Landscape.LandscapeToolParams;
using Tools.MainState;
using Tools.MapObjects;
using Tools.PenTablet;
using Tools.Statusbar;
using Win32;

namespace MapEditor.Map.States
{
	// Token: 0x020000BE RID: 190
	internal class LandscapeState : State
	{
		// Token: 0x060009BD RID: 2493 RVA: 0x000514FF File Offset: 0x000504FF
		private void OnUndo(OperationContainer _operationContainer, IOperation operation, int index, bool result)
		{
			if (result && operation != null && this.landscapeRegion != null && operation.ContainsLabel("LandscapeToolContainer"))
			{
				this.landscapeRegion.Update();
			}
		}

		// Token: 0x060009BE RID: 2494 RVA: 0x00051528 File Offset: 0x00050528
		private void OnRedo(OperationContainer _operationContainer, IOperation operation, int index, bool result)
		{
			if (result && operation != null && this.landscapeRegion != null && operation.ContainsLabel("LandscapeToolContainer"))
			{
				this.landscapeRegion.Update();
			}
		}

		// Token: 0x060009BF RID: 2495 RVA: 0x00051554 File Offset: 0x00050554
		private void CreateLandscapeToolContext()
		{
			this.landscapeToolContext = new LandscapeToolContext(this.collisionMap, this.mapObjectContainer, this.penTablet, this.mapOffset, this.editorScene, this.editorSceneViewID);
			this.landscapeToolContext.Updated += this.OnLandscapeToolContextUpdated;
		}

		// Token: 0x060009C0 RID: 2496 RVA: 0x000515A7 File Offset: 0x000505A7
		private void DestroyLandscapeToolContext()
		{
			if (this.landscapeToolContext != null)
			{
				this.landscapeToolContext.Updated -= this.OnLandscapeToolContextUpdated;
				this.landscapeToolContext.Destroy();
				this.landscapeToolContext = null;
			}
		}

		// Token: 0x060009C1 RID: 2497 RVA: 0x000515DC File Offset: 0x000505DC
		private void CreateLanscapeRegion()
		{
			if (this.landscapeParams != null && this.landscapeParams.LandscapeRegionParams != null)
			{
				this.landscapeRegion = new LandscapeRegion(this.landscapeParams.LandscapeRegionParams, this.collisionMap, this.squareContainer, this.polygonContainer, this.stripeContainer);
				this.landscapeParams.LandscapeRegionParams.TypeChanged += this.OnLandscapeRegionTypeChanged;
			}
		}

		// Token: 0x060009C2 RID: 2498 RVA: 0x00051648 File Offset: 0x00050648
		private void DestroyLanscapeRegion()
		{
			if (this.landscapeParams != null && this.landscapeParams.LandscapeRegionParams != null)
			{
				this.landscapeParams.LandscapeRegionParams.TypeChanged -= this.OnLandscapeRegionTypeChanged;
				if (this.landscapeRegion != null)
				{
					this.landscapeRegion.Destroy();
					this.landscapeRegion = null;
				}
			}
		}

		// Token: 0x060009C3 RID: 2499 RVA: 0x000516A0 File Offset: 0x000506A0
		private void CreateStates(StateContainer _stateContainer)
		{
			this.multiState = new MultiState("LandscapeMultiState");
			this.simpleLandscapeToolSubstate = new LandscapeState.SimpleLandscapeToolSubstate(this, this.landscapeToolContext, this.landscapeParams, this.landscapeRegion, this.mapEditorLandscapeToolContainer, this.operationContainer);
			this.clipboardToolSubstate = new LandscapeState.ClipboardToolSubstate(_stateContainer, this, this.landscapeToolContext, this.landscapeParams, this.landscapeRegion, this.mapEditorLandscapeToolContainer, this.operationContainer);
			this.addPolygonSubstate = new LandscapeState.AddPolygonSubstate(this.landscapeRegion.Polygon, this.polygonContainer, this.landscapeRegion.LandscapeRegionParams, this.multiState, LandscapeState.addRegionColor, this.collisionMap, this.editorScene, this.editorSceneViewID);
			this.editPolygonSubstate = new LandscapeState.EditPolygonSubstate(this.landscapeRegion.Polygon, this.polygonContainer, this.landscapeRegion.LandscapeRegionParams, this.multiState, LandscapeState.editRegionColor, this.collisionMap, this.editorScene, this.editorSceneViewID);
			this.addStripeSubstate = new LandscapeState.AddStripeSubstate(this.landscapeRegion.Stripe, this.stripeContainer, this.landscapeRegion.LandscapeRegionParams, this.multiState, LandscapeState.addRegionColor, this.collisionMap, this.editorScene, this.editorSceneViewID);
			this.editStripeSubstate = new LandscapeState.EditStripeSubstate(this.landscapeRegion.Stripe, this.stripeContainer, this.landscapeRegion.LandscapeRegionParams, this.multiState, LandscapeState.editRegionColor, this.collisionMap, this.editorScene, this.editorSceneViewID);
			this.multiState.AddState(this.simpleLandscapeToolSubstate);
			this.multiState.AddState(this.clipboardToolSubstate);
			this.multiState.AddState(this.addPolygonSubstate);
			this.multiState.AddState(this.editPolygonSubstate);
			this.multiState.AddState(this.addStripeSubstate);
			this.multiState.AddState(this.editStripeSubstate);
			base.AddMethod("focus_on", new Method(this.OnFocusOn));
		}

		// Token: 0x060009C4 RID: 2500 RVA: 0x000518A0 File Offset: 0x000508A0
		private void DestroyStates()
		{
			this.multiState.Destroy();
			this.simpleLandscapeToolSubstate.Destroy();
			this.clipboardToolSubstate.Destroy();
			this.addPolygonSubstate.Destroy();
			this.editPolygonSubstate.Destroy();
			this.addStripeSubstate.Destroy();
			this.editStripeSubstate.Destroy();
			this.multiState = null;
			this.simpleLandscapeToolSubstate = null;
			this.clipboardToolSubstate = null;
			this.addPolygonSubstate = null;
			this.editPolygonSubstate = null;
			this.addStripeSubstate = null;
			this.editStripeSubstate = null;
		}

		// Token: 0x060009C5 RID: 2501 RVA: 0x0005192C File Offset: 0x0005092C
		private void InvokeLeaveFromRegionState()
		{
			if (this.multiState.ActiveStateLabel == this.addPolygonSubstate.Label)
			{
				this.addPolygonSubstate.InvokeLeave(false, true);
				return;
			}
			if (this.multiState.ActiveStateLabel == this.editPolygonSubstate.Label)
			{
				this.editPolygonSubstate.InvokeLeave(false);
				return;
			}
			if (this.multiState.ActiveStateLabel == this.addStripeSubstate.Label)
			{
				this.addStripeSubstate.InvokeLeave(false, true);
				return;
			}
			if (this.multiState.ActiveStateLabel == this.editStripeSubstate.Label)
			{
				this.editStripeSubstate.InvokeLeave(false);
			}
		}

		// Token: 0x060009C6 RID: 2502 RVA: 0x000519E4 File Offset: 0x000509E4
		private void OnLandscapeRegionTypeChanged(LandscapeRegionParams _landscapeRegionParams, ref LandscapeRegionType oldValue, ref LandscapeRegionType newValue)
		{
			if (this.landscapeRegion != null && this.landscapeParams != null && this.landscapeParams.LandscapeRegionParams != null)
			{
				if (this.landscapeParams.LandscapeRegionParams.Type == LandscapeRegionType.Ellipse || this.landscapeParams.LandscapeRegionParams.Type == LandscapeRegionType.Square || this.landscapeParams.LandscapeRegionParams.Type == LandscapeRegionType.AllMap)
				{
					this.InvokeLeaveFromRegionState();
					return;
				}
				if (this.landscapeParams.LandscapeRegionParams.Type == LandscapeRegionType.Polygon)
				{
					this.InvokeLeaveFromRegionState();
					if (this.landscapeRegion.Polygon.Count < 3)
					{
						if (this.multiState.ActiveStateLabel == this.simpleLandscapeToolSubstate.Label)
						{
							this.ActivateAddPolygonSubstate(this.simpleLandscapeToolSubstate.Label);
							return;
						}
						if (this.multiState.ActiveStateLabel == this.clipboardToolSubstate.Label)
						{
							this.ActivateAddPolygonSubstate(this.clipboardToolSubstate.Label);
							return;
						}
					}
				}
				else if (this.landscapeParams.LandscapeRegionParams.Type == LandscapeRegionType.Stripe)
				{
					this.InvokeLeaveFromRegionState();
					if (this.landscapeRegion.Stripe.Center.Points.Count < 2)
					{
						if (this.multiState.ActiveStateLabel == this.simpleLandscapeToolSubstate.Label)
						{
							this.ActivateAddStripeSubstate(this.simpleLandscapeToolSubstate.Label);
							return;
						}
						if (this.multiState.ActiveStateLabel == this.clipboardToolSubstate.Label)
						{
							this.ActivateAddStripeSubstate(this.clipboardToolSubstate.Label);
						}
					}
				}
			}
		}

		// Token: 0x060009C7 RID: 2503 RVA: 0x00051B80 File Offset: 0x00050B80
		private bool HitTestEditPolygonSubtate(string previousStateLabel, ref Vec3 point)
		{
			if (this.addPolygonSubstate != null && this.landscapeRegion != null && this.landscapeRegion.Polygon != null)
			{
				if (this.landscapeRegion.Polygon.Count > 2)
				{
					this.editPolygonSubstate.PointTested = this.landscapeRegion.Polygon.LocateNearestPoint(ref point, 1.0);
					if (this.editPolygonSubstate.PointTested != -1)
					{
						this.editPolygonSubstate.PreviousStateLabel = previousStateLabel;
						this.multiState.ActiveStateLabel = this.editPolygonSubstate.Label;
						return true;
					}
				}
				if (KeyStatus.Control)
				{
					this.editPolygonSubstate.CenterTested = true;
					this.editPolygonSubstate.PreviousStateLabel = previousStateLabel;
					this.multiState.ActiveStateLabel = this.editPolygonSubstate.Label;
					return true;
				}
			}
			return false;
		}

		// Token: 0x060009C8 RID: 2504 RVA: 0x00051C55 File Offset: 0x00050C55
		private bool IsSubstateActive(string label)
		{
			return this.multiState.ActiveStateLabel == label;
		}

		// Token: 0x060009C9 RID: 2505 RVA: 0x00051C68 File Offset: 0x00050C68
		private void ActivateAddPolygonSubstate(string previousStateLabel)
		{
			if (this.addPolygonSubstate != null && this.landscapeRegion != null && this.landscapeRegion.Polygon != null)
			{
				this.addPolygonSubstate.PreviousStateLabel = previousStateLabel;
				this.multiState.ActiveStateLabel = this.addPolygonSubstate.Label;
			}
		}

		// Token: 0x060009CA RID: 2506 RVA: 0x00051CB4 File Offset: 0x00050CB4
		private void ActivateEditPolygonSubtate(string previousStateLabel)
		{
			if (this.addPolygonSubstate != null && this.landscapeRegion != null && this.landscapeRegion.Polygon != null)
			{
				if (this.landscapeRegion.Polygon.Count < 3)
				{
					this.addPolygonSubstate.PreviousStateLabel = previousStateLabel;
					this.multiState.ActiveStateLabel = this.addPolygonSubstate.Label;
					return;
				}
				this.editPolygonSubstate.PreviousStateLabel = previousStateLabel;
				this.multiState.ActiveStateLabel = this.editPolygonSubstate.Label;
			}
		}

		// Token: 0x060009CB RID: 2507 RVA: 0x00051D38 File Offset: 0x00050D38
		private bool HitTestEditStripeSubtate(string previousStateLabel, ref Vec3 point)
		{
			if (this.addStripeSubstate != null && this.landscapeRegion != null && this.landscapeRegion.Stripe != null)
			{
				if (this.landscapeRegion.Stripe.Count > 1)
				{
					this.editStripeSubstate.PointTested = this.landscapeRegion.Stripe.Center.LocateNearestPoint(ref point, 1.0);
					if (this.editStripeSubstate.PointTested != -1)
					{
						this.editStripeSubstate.PreviousStateLabel = previousStateLabel;
						this.multiState.ActiveStateLabel = this.editStripeSubstate.Label;
						return true;
					}
					this.editStripeSubstate.BoundsTested = this.landscapeRegion.Stripe.Bounds.LocateNearestPoint(ref point, 1.0);
					if (this.editStripeSubstate.BoundsTested != -1)
					{
						this.editStripeSubstate.PreviousStateLabel = previousStateLabel;
						this.multiState.ActiveStateLabel = this.editStripeSubstate.Label;
						return true;
					}
				}
				if (KeyStatus.Control)
				{
					this.editStripeSubstate.CenterTested = true;
					this.editStripeSubstate.PreviousStateLabel = previousStateLabel;
					this.multiState.ActiveStateLabel = this.editStripeSubstate.Label;
					return true;
				}
			}
			return false;
		}

		// Token: 0x060009CC RID: 2508 RVA: 0x00051E74 File Offset: 0x00050E74
		private void ActivateAddStripeSubstate(string previousStateLabel)
		{
			if (this.addStripeSubstate != null && this.landscapeRegion != null && this.landscapeRegion.Stripe != null)
			{
				this.addStripeSubstate.PreviousStateLabel = previousStateLabel;
				this.multiState.ActiveStateLabel = this.addStripeSubstate.Label;
			}
		}

		// Token: 0x060009CD RID: 2509 RVA: 0x00051EC0 File Offset: 0x00050EC0
		private void ActivateEditStripeSubtate(string previousStateLabel)
		{
			if (this.addStripeSubstate != null && this.landscapeRegion != null && this.landscapeRegion.Stripe != null)
			{
				if (this.landscapeRegion.Stripe.Count < 3)
				{
					this.addStripeSubstate.PreviousStateLabel = previousStateLabel;
					this.multiState.ActiveStateLabel = this.addStripeSubstate.Label;
					return;
				}
				this.editStripeSubstate.PreviousStateLabel = previousStateLabel;
				this.multiState.ActiveStateLabel = this.editStripeSubstate.Label;
			}
		}

		// Token: 0x060009CE RID: 2510 RVA: 0x00051F42 File Offset: 0x00050F42
		private void InvokeUpdateClipboardButtons()
		{
			if (this.simpleLandscapeToolSubstate != null)
			{
				this.simpleLandscapeToolSubstate.InvokeUpdateClipboardButtons();
			}
		}

		// Token: 0x060009CF RID: 2511 RVA: 0x00051F57 File Offset: 0x00050F57
		private void InvokeClipboardOperation(ClipboardOperationType clipboardOperation, string previousStateLabel)
		{
			if (this.clipboardToolSubstate != null)
			{
				this.clipboardToolSubstate.InvokeClipboardOperation(clipboardOperation, previousStateLabel, this.parentForm);
			}
		}

		// Token: 0x060009D0 RID: 2512 RVA: 0x00051F74 File Offset: 0x00050F74
		private bool InvokeClipboardOperationAvailable(ClipboardOperationType clipboardOperation)
		{
			return this.clipboardToolSubstate != null && this.clipboardToolSubstate.InvokeClipboardOperationAvailable(clipboardOperation);
		}

		// Token: 0x060009D1 RID: 2513 RVA: 0x00051F8C File Offset: 0x00050F8C
		private void OnEnterState(IState state)
		{
			this.landscapeToolContext.Update(true);
			if (base.Container != null)
			{
				if (this.landscapeParams != null && this.landscapeParams.LandscapeRegionParams != null)
				{
					if (this.landscapeParams.LandscapeRegionParams.Type == LandscapeRegionType.Polygon && this.landscapeRegion != null && this.landscapeRegion.Polygon != null && this.landscapeRegion.Polygon.Count < 3)
					{
						this.ActivateAddPolygonSubstate(this.simpleLandscapeToolSubstate.Label);
					}
					else if (this.landscapeParams.LandscapeRegionParams.Type == LandscapeRegionType.Stripe && this.landscapeRegion != null && this.landscapeRegion.Stripe != null && this.landscapeRegion.Stripe.Center.Points.Count < 2)
					{
						this.ActivateAddStripeSubstate(this.simpleLandscapeToolSubstate.Label);
					}
					else
					{
						this.multiState.ActiveStateLabel = this.simpleLandscapeToolSubstate.Label;
					}
				}
				base.Container.BindState(this.multiState);
			}
		}

		// Token: 0x060009D2 RID: 2514 RVA: 0x00052098 File Offset: 0x00051098
		private void OnLeaveState(IState state)
		{
			if (base.Container != null)
			{
				base.Container.UnbindState(this.multiState);
			}
		}

		// Token: 0x060009D3 RID: 2515 RVA: 0x000520B4 File Offset: 0x000510B4
		private void OnFocusOn(MethodArgs methodArgs)
		{
			Position anchorPosition = this.landscapeToolContext.LandscapeToolContextPosition.CollisionMapPosition;
			Vec3 distance = new Vec3(0.0, -10.0, 10.0);
			this.landscapeToolContext.EditorScene.SetAnchor(this.landscapeToolContext.EditorSceneViewID, ref anchorPosition, ref distance);
		}

		// Token: 0x060009D4 RID: 2516 RVA: 0x00052118 File Offset: 0x00051118
		private void OnLandscapeToolContextUpdated(LandscapeToolContext _landscapeToolContext)
		{
			if (this.statusbar != null)
			{
				if (this.statusbar.StatusPosition != null)
				{
					this.statusbar.StatusPosition.Text = string.Format("x:{0:0.###} y:{1:0.###} z:{2:0.###}, patch:[{3}, {4}]", new object[]
					{
						this.landscapeToolContext.LandscapeToolContextPosition.CollisionMapPosition.X,
						this.landscapeToolContext.LandscapeToolContextPosition.CollisionMapPosition.Y,
						this.landscapeToolContext.LandscapeToolContextPosition.CollisionMapPosition.Z,
						(int)(this.landscapeToolContext.LandscapeToolContextPosition.CollisionMapPosition.X / (double)Constants.PatchSize),
						(int)(this.landscapeToolContext.LandscapeToolContextPosition.CollisionMapPosition.Y / (double)Constants.PatchSize)
					});
				}
				if (this.statusbar.StatusMessage != null && this.itemDataContainer != null)
				{
					Tools.Geometry.Point position = this.landscapeToolContext.LandscapeToolContextPosition.TerrainPosition;
					this.landscapeToolContext.EditorScene.GetTerrainTiles(this.landscapeToolContext.EditorSceneViewID, ref position, ref this.statusStringTiles);
					string statusMessage = string.Empty;
					for (int index = 0; index < this.statusStringTiles.Count; index++)
					{
						ItemData itemData = this.itemDataContainer.GetItemData(LandscapeTileItemSource.GetTileItem(this.statusStringTiles[index].Tile, this.continentName));
						if (itemData != null)
						{
							statusMessage += string.Format("{0}{1} {2}%", string.IsNullOrEmpty(statusMessage) ? string.Empty : ",  ", itemData.Text, (int)(this.statusStringTiles[index].Weight * 100.0));
						}
						this.statusbar.StatusMessage.Text = statusMessage;
					}
				}
				if (this.statusbar.StatusHelp != null)
				{
					this.statusbar.StatusHelp.Text = string.Format(Strings.HELP_LANDSCAPE_SHORTCUTS, base.Container.GetEventShortcuts("clear_lti_data"));
				}
				this.statusbar.UpdateStatusbar();
			}
		}

		// Token: 0x060009D5 RID: 2517 RVA: 0x00052364 File Offset: 0x00051364
		private void UpdateActiveTerrain()
		{
			if (this.landscapeToolContext != null && this.landscapeParams != null && this.landscapeParams.LandscapeRegionParams != null)
			{
				this.landscapeToolContext.EditorScene.SetActiveTerrain(this.landscapeParams.LandscapeRegionParams.TerrainNumber, this.landscapeParams.LandscapeRegionParams.InvertHeightTools);
			}
		}

		// Token: 0x060009D6 RID: 2518 RVA: 0x000523BE File Offset: 0x000513BE
		private void LandscapeRegionParamsTerrainNumberChanged(LandscapeRegionParams _landscapeRegionParams, ref int oldValue, ref int newValue)
		{
			this.UpdateActiveTerrain();
			if (this.landscapeRegion != null)
			{
				this.landscapeRegion.Update();
			}
		}

		// Token: 0x060009D7 RID: 2519 RVA: 0x000523D9 File Offset: 0x000513D9
		private void LandscapeRegionParamsInvertHeightToolsChanged(LandscapeRegionParams _landscapeRegionParams, ref bool oldValue, ref bool newValue)
		{
			this.UpdateActiveTerrain();
			if (this.landscapeRegion != null)
			{
				this.landscapeRegion.Update();
			}
		}

		// Token: 0x060009D8 RID: 2520 RVA: 0x000523F4 File Offset: 0x000513F4
		public LandscapeState(StateContainer _stateContainer, MainState _mainState, ItemDataContainer _itemDataContainer, MapEditorLandscapeToolContainer _mapEditorLandscapeToolContainer, OperationContainer _operationContainer, AxisAlignedSquareContainer _squareContainer, PolygonContainer _polygonContainer, StripeContainer _stripeContainer, ICollisionMap _collisionMap, MapObjectContainer _mapObjectContainer, ILandscapeParams _landscapeParams, string _continentName, Vec3 _mapOffset, EditorScene _editorScene, int _editorSceneViewID, IStatusbar _statusbar, Form _parentForm, PenTablet _penTablet) : base("MapEditorLandscapeState")
		{
			this.mainState = _mainState;
			this.itemDataContainer = _itemDataContainer;
			this.mapEditorLandscapeToolContainer = _mapEditorLandscapeToolContainer;
			this.operationContainer = _operationContainer;
			this.squareContainer = _squareContainer;
			this.polygonContainer = _polygonContainer;
			this.stripeContainer = _stripeContainer;
			this.collisionMap = _collisionMap;
			this.mapObjectContainer = _mapObjectContainer;
			this.penTablet = _penTablet;
			this.landscapeParams = _landscapeParams;
			this.continentName = _continentName;
			this.mapOffset = _mapOffset;
			this.editorScene = _editorScene;
			this.editorSceneViewID = _editorSceneViewID;
			this.statusbar = _statusbar;
			this.parentForm = _parentForm;
			if (this.operationContainer != null)
			{
				this.operationContainer.OperationRedoInvoked += this.OnRedo;
				this.operationContainer.OperationUndoInvoked += this.OnUndo;
			}
			if (this.landscapeParams != null)
			{
				this.landscapeParams.LandscapeRegionParams.TerrainNumberChanged += this.LandscapeRegionParamsTerrainNumberChanged;
				this.landscapeParams.LandscapeRegionParams.InvertHeightToolsChanged += this.LandscapeRegionParamsInvertHeightToolsChanged;
			}
			this.CreateLandscapeToolContext();
			this.CreateLanscapeRegion();
			this.CreateStates(_stateContainer);
			this.EnterState = (State.ActivateEvent)Delegate.Combine(this.EnterState, new State.ActivateEvent(this.OnEnterState));
			this.LeaveState = (State.ActivateEvent)Delegate.Combine(this.LeaveState, new State.ActivateEvent(this.OnLeaveState));
		}

		// Token: 0x060009D9 RID: 2521 RVA: 0x00052588 File Offset: 0x00051588
		public void Destroy()
		{
			this.EnterState = (State.ActivateEvent)Delegate.Remove(this.EnterState, new State.ActivateEvent(this.OnEnterState));
			this.LeaveState = (State.ActivateEvent)Delegate.Remove(this.LeaveState, new State.ActivateEvent(this.OnLeaveState));
			this.DestroyStates();
			this.DestroyLanscapeRegion();
			this.DestroyLandscapeToolContext();
			if (this.operationContainer != null)
			{
				this.operationContainer.OperationRedoInvoked -= this.OnRedo;
				this.operationContainer.OperationUndoInvoked -= this.OnUndo;
				this.operationContainer = null;
			}
			if (this.landscapeParams != null)
			{
				this.landscapeParams.LandscapeRegionParams.TerrainNumberChanged -= this.LandscapeRegionParamsTerrainNumberChanged;
				this.landscapeParams.LandscapeRegionParams.InvertHeightToolsChanged -= this.LandscapeRegionParamsInvertHeightToolsChanged;
				this.landscapeParams = null;
			}
			this.mainState = null;
			this.itemDataContainer = null;
			this.statusStringTiles = null;
			this.mapEditorLandscapeToolContainer = null;
			this.squareContainer = null;
			this.polygonContainer = null;
			this.stripeContainer = null;
			this.collisionMap = null;
			this.penTablet = null;
			this.mapObjectContainer = null;
			this.landscapeParams = null;
			this.continentName = string.Empty;
			this.mapOffset = Vec3.Empty;
			this.editorScene = null;
			this.editorSceneViewID = -1;
			this.statusbar = null;
			this.parentForm = null;
		}

		// Token: 0x040007D1 RID: 2001
		private static readonly Color addRegionColor = Color.Yellow;

		// Token: 0x040007D2 RID: 2002
		private static readonly Color editRegionColor = Color.Magenta;

		// Token: 0x040007D3 RID: 2003
		private MainState mainState;

		// Token: 0x040007D4 RID: 2004
		private ItemDataContainer itemDataContainer;

		// Token: 0x040007D5 RID: 2005
		private List<EditorScene.TileInfo> statusStringTiles = new List<EditorScene.TileInfo>();

		// Token: 0x040007D6 RID: 2006
		private MapEditorLandscapeToolContainer mapEditorLandscapeToolContainer;

		// Token: 0x040007D7 RID: 2007
		private OperationContainer operationContainer;

		// Token: 0x040007D8 RID: 2008
		private PolygonContainer polygonContainer;

		// Token: 0x040007D9 RID: 2009
		private AxisAlignedSquareContainer squareContainer;

		// Token: 0x040007DA RID: 2010
		private StripeContainer stripeContainer;

		// Token: 0x040007DB RID: 2011
		private ICollisionMap collisionMap;

		// Token: 0x040007DC RID: 2012
		private MapObjectContainer mapObjectContainer;

		// Token: 0x040007DD RID: 2013
		private PenTablet penTablet;

		// Token: 0x040007DE RID: 2014
		private ILandscapeParams landscapeParams;

		// Token: 0x040007DF RID: 2015
		private string continentName = string.Empty;

		// Token: 0x040007E0 RID: 2016
		private Vec3 mapOffset = Vec3.Empty;

		// Token: 0x040007E1 RID: 2017
		private EditorScene editorScene;

		// Token: 0x040007E2 RID: 2018
		private int editorSceneViewID = -1;

		// Token: 0x040007E3 RID: 2019
		private IStatusbar statusbar;

		// Token: 0x040007E4 RID: 2020
		private Form parentForm;

		// Token: 0x040007E5 RID: 2021
		private LandscapeToolContext landscapeToolContext;

		// Token: 0x040007E6 RID: 2022
		private LandscapeRegion landscapeRegion;

		// Token: 0x040007E7 RID: 2023
		private MultiState multiState;

		// Token: 0x040007E8 RID: 2024
		private LandscapeState.SimpleLandscapeToolSubstate simpleLandscapeToolSubstate;

		// Token: 0x040007E9 RID: 2025
		private LandscapeState.ClipboardToolSubstate clipboardToolSubstate;

		// Token: 0x040007EA RID: 2026
		private LandscapeState.AddPolygonSubstate addPolygonSubstate;

		// Token: 0x040007EB RID: 2027
		private LandscapeState.EditPolygonSubstate editPolygonSubstate;

		// Token: 0x040007EC RID: 2028
		private LandscapeState.AddStripeSubstate addStripeSubstate;

		// Token: 0x040007ED RID: 2029
		private LandscapeState.EditStripeSubstate editStripeSubstate;

		// Token: 0x020000BF RID: 191
		internal class RegionSubstate : State
		{
			// Token: 0x170001FF RID: 511
			// (get) Token: 0x060009DB RID: 2523 RVA: 0x00052704 File Offset: 0x00051704
			public LandscapeRegionParams LandscapeRegionParams
			{
				get
				{
					return this.landscapeRegionParams;
				}
			}

			// Token: 0x17000200 RID: 512
			// (get) Token: 0x060009DC RID: 2524 RVA: 0x0005270C File Offset: 0x0005170C
			public Color RegionColor
			{
				get
				{
					return this.regionColor;
				}
			}

			// Token: 0x17000201 RID: 513
			// (get) Token: 0x060009DD RID: 2525 RVA: 0x00052714 File Offset: 0x00051714
			public ICollisionMap CollisionMap
			{
				get
				{
					return this.collisionMap;
				}
			}

			// Token: 0x17000202 RID: 514
			// (get) Token: 0x060009DE RID: 2526 RVA: 0x0005271C File Offset: 0x0005171C
			// (set) Token: 0x060009DF RID: 2527 RVA: 0x00052724 File Offset: 0x00051724
			public string PreviousStateLabel
			{
				get
				{
					return this.previousStateLabel;
				}
				set
				{
					this.previousStateLabel = value;
				}
			}

			// Token: 0x17000203 RID: 515
			// (get) Token: 0x060009E0 RID: 2528 RVA: 0x0005272D File Offset: 0x0005172D
			// (set) Token: 0x060009E1 RID: 2529 RVA: 0x00052735 File Offset: 0x00051735
			public Vec3 LastPosition
			{
				get
				{
					return this.lastPosition;
				}
				set
				{
					this.lastPosition = value;
				}
			}

			// Token: 0x060009E2 RID: 2530 RVA: 0x00052740 File Offset: 0x00051740
			public RegionSubstate(string _stateLabel, LandscapeRegionParams _landscapeRegionParams, MultiState _multiState, Color _regionColor, ICollisionMap _collisionMap, EditorScene _editorScene, int _editorSceneViewID) : base(_stateLabel)
			{
				this.landscapeRegionParams = _landscapeRegionParams;
				this.multiState = _multiState;
				this.regionColor = _regionColor;
				this.collisionMap = _collisionMap;
				this.editorScene = _editorScene;
				this.editorSceneViewID = _editorSceneViewID;
			}

			// Token: 0x060009E3 RID: 2531 RVA: 0x000527AA File Offset: 0x000517AA
			public virtual void Destroy()
			{
				this.landscapeRegionParams = null;
				this.multiState = null;
				this.regionColor = Color.White;
				this.collisionMap = null;
				this.editorScene = null;
				this.editorSceneViewID = -1;
			}

			// Token: 0x060009E4 RID: 2532 RVA: 0x000527DC File Offset: 0x000517DC
			public void UpdateLastPosition(int x, int y)
			{
				if (this.collisionMap != null)
				{
					Position _position;
					this.collisionMap.PickPosition(x, y, TerrainSurface.Terrain, out _position);
					this.lastPosition = _position.Vec3;
				}
			}

			// Token: 0x060009E5 RID: 2533 RVA: 0x00052810 File Offset: 0x00051810
			public void UpdateLastPosition()
			{
				Tools.Geometry.Point mouseCursorPos;
				this.editorScene.GetMouseCursorPos(this.editorSceneViewID, out mouseCursorPos);
				this.UpdateLastPosition(mouseCursorPos.X, mouseCursorPos.Y);
			}

			// Token: 0x060009E6 RID: 2534 RVA: 0x00052844 File Offset: 0x00051844
			public virtual void InvokeLeave()
			{
				if (this.multiState != null)
				{
					this.multiState.ActiveStateLabel = this.previousStateLabel;
				}
			}

			// Token: 0x040007EE RID: 2030
			private LandscapeRegionParams landscapeRegionParams;

			// Token: 0x040007EF RID: 2031
			private MultiState multiState;

			// Token: 0x040007F0 RID: 2032
			private Color regionColor = Color.White;

			// Token: 0x040007F1 RID: 2033
			private ICollisionMap collisionMap;

			// Token: 0x040007F2 RID: 2034
			private EditorScene editorScene;

			// Token: 0x040007F3 RID: 2035
			private int editorSceneViewID = -1;

			// Token: 0x040007F4 RID: 2036
			private string previousStateLabel = string.Empty;

			// Token: 0x040007F5 RID: 2037
			private Vec3 lastPosition = Vec3.Empty;
		}

		// Token: 0x020000C0 RID: 192
		internal class PolygonSubstate : LandscapeState.RegionSubstate
		{
			// Token: 0x17000204 RID: 516
			// (get) Token: 0x060009E7 RID: 2535 RVA: 0x0005285F File Offset: 0x0005185F
			public LandscapeState.PolygonSubstate.PolygonBackup Backup
			{
				get
				{
					return this.backup;
				}
			}

			// Token: 0x17000205 RID: 517
			// (get) Token: 0x060009E8 RID: 2536 RVA: 0x00052867 File Offset: 0x00051867
			// (set) Token: 0x060009E9 RID: 2537 RVA: 0x0005286F File Offset: 0x0005186F
			public bool Restore
			{
				get
				{
					return this.restore;
				}
				set
				{
					this.restore = value;
				}
			}

			// Token: 0x17000206 RID: 518
			// (get) Token: 0x060009EA RID: 2538 RVA: 0x00052878 File Offset: 0x00051878
			public Polygon Polygon
			{
				get
				{
					return this.polygon;
				}
			}

			// Token: 0x17000207 RID: 519
			// (get) Token: 0x060009EB RID: 2539 RVA: 0x00052880 File Offset: 0x00051880
			public PolygonContainer PolygonContainer
			{
				get
				{
					return this.polygonContainer;
				}
			}

			// Token: 0x060009EC RID: 2540 RVA: 0x00052888 File Offset: 0x00051888
			public PolygonSubstate(string _stateLabel, Polygon _polygon, PolygonContainer _polygonContainer, LandscapeRegionParams _landscapeRegionParams, MultiState _multiState, Color _regionColor, ICollisionMap _collisionMap, EditorScene _editorScene, int _editorSceneViewID) : base(_stateLabel, _landscapeRegionParams, _multiState, _regionColor, _collisionMap, _editorScene, _editorSceneViewID)
			{
				this.polygon = _polygon;
				this.polygonContainer = _polygonContainer;
			}

			// Token: 0x060009ED RID: 2541 RVA: 0x000528BD File Offset: 0x000518BD
			public override void Destroy()
			{
				this.polygon = null;
				this.polygonContainer = null;
				base.Destroy();
			}

			// Token: 0x040007F6 RID: 2038
			private readonly LandscapeState.PolygonSubstate.PolygonBackup backup = new LandscapeState.PolygonSubstate.PolygonBackup();

			// Token: 0x040007F7 RID: 2039
			private bool restore = true;

			// Token: 0x040007F8 RID: 2040
			private Polygon polygon;

			// Token: 0x040007F9 RID: 2041
			private PolygonContainer polygonContainer;

			// Token: 0x020000C1 RID: 193
			internal class PolygonBackup
			{
				// Token: 0x060009EE RID: 2542 RVA: 0x000528D3 File Offset: 0x000518D3
				public void Backup(Polygon polygon)
				{
					if (polygon != null)
					{
						this.points.Clear();
						this.points.AddRange(polygon.Points);
						this.previousColor = polygon.Color;
					}
				}

				// Token: 0x060009EF RID: 2543 RVA: 0x00052900 File Offset: 0x00051900
				public void Restore(Polygon polygon, bool restorePoints)
				{
					if (polygon != null)
					{
						polygon.Color = this.previousColor;
						if (restorePoints)
						{
							polygon.Clear();
							if (this.points.Count > 2)
							{
								polygon.Points.AddRange(this.points);
							}
						}
					}
				}

				// Token: 0x060009F0 RID: 2544 RVA: 0x0005293A File Offset: 0x0005193A
				public void Clear()
				{
					this.previousColor = Color.White;
					this.points.Clear();
				}

				// Token: 0x17000208 RID: 520
				// (get) Token: 0x060009F1 RID: 2545 RVA: 0x00052952 File Offset: 0x00051952
				public bool Empty
				{
					get
					{
						return this.points.Count < 3;
					}
				}

				// Token: 0x040007FA RID: 2042
				private Color previousColor = Color.White;

				// Token: 0x040007FB RID: 2043
				private readonly List<Vec3> points = new List<Vec3>();
			}
		}

		// Token: 0x020000C2 RID: 194
		internal class AddPolygonSubstate : LandscapeState.PolygonSubstate
		{
			// Token: 0x060009F3 RID: 2547 RVA: 0x00052980 File Offset: 0x00051980
			private void MoveLastPointTo(int x, int y)
			{
				base.UpdateLastPosition(x, y);
				if (base.Polygon.Count > 0)
				{
					base.Polygon.MovePointTo(base.Polygon.Count - 1, base.LastPosition);
				}
			}

			// Token: 0x060009F4 RID: 2548 RVA: 0x000529B7 File Offset: 0x000519B7
			private void RemovePoint()
			{
				if (base.Polygon.Count > 1)
				{
					base.Polygon.DeletePoint(base.Polygon.Count - 2);
				}
			}

			// Token: 0x060009F5 RID: 2549 RVA: 0x000529E0 File Offset: 0x000519E0
			private void AddPoint()
			{
				if (base.Polygon.Count > 0)
				{
					base.Polygon.InsertPoint(base.Polygon.Count - 1, base.LastPosition, true, false);
					return;
				}
				base.Polygon.AddPoint(base.LastPosition, false, false);
			}

			// Token: 0x060009F6 RID: 2550 RVA: 0x00052A34 File Offset: 0x00051A34
			private void OnEnterState(IState state)
			{
				base.Backup.Backup(base.Polygon);
				base.Polygon.Clear();
				base.Polygon.Color = base.RegionColor;
				base.PolygonContainer.AddPolygon(base.Polygon);
				base.Restore = true;
				base.UpdateLastPosition();
				this.AddPoint();
			}

			// Token: 0x060009F7 RID: 2551 RVA: 0x00052A94 File Offset: 0x00051A94
			private void OnLeaveState(IState state)
			{
				base.PolygonContainer.RemovePolygon(base.Polygon);
				base.Backup.Restore(base.Polygon, base.Restore);
				base.Restore = true;
			}

			// Token: 0x060009F8 RID: 2552 RVA: 0x00052AC8 File Offset: 0x00051AC8
			private void OnMouseMove(MethodArgs methodArgs)
			{
				MouseEventArgs mouseEventArgs = methodArgs.eventArgs as MouseEventArgs;
				if (mouseEventArgs != null && !KeyStatus.RightMouse)
				{
					this.MoveLastPointTo(mouseEventArgs.X, mouseEventArgs.Y);
				}
			}

			// Token: 0x060009F9 RID: 2553 RVA: 0x00052B00 File Offset: 0x00051B00
			private void OnMouseUp(MethodArgs methodArgs)
			{
				MouseEventArgs mouseEventArgs = methodArgs.eventArgs as MouseEventArgs;
				if (mouseEventArgs != null)
				{
					if (mouseEventArgs.Button == MouseButtons.Middle && base.Polygon.Count > 1)
					{
						this.RemovePoint();
						this.MoveLastPointTo(mouseEventArgs.X, mouseEventArgs.Y);
					}
					if (mouseEventArgs.Button == MouseButtons.Left)
					{
						this.MoveLastPointTo(mouseEventArgs.X, mouseEventArgs.Y);
						this.AddPoint();
						this.MoveLastPointTo(mouseEventArgs.X, mouseEventArgs.Y);
					}
				}
			}

			// Token: 0x060009FA RID: 2554 RVA: 0x00052B88 File Offset: 0x00051B88
			private void OnMouseDoubleClick(MethodArgs methodArgs)
			{
				MouseEventArgs mouseEventArgs = methodArgs.eventArgs as MouseEventArgs;
				if (mouseEventArgs != null && (mouseEventArgs.Button == MouseButtons.Left || mouseEventArgs.Button == MouseButtons.Middle || mouseEventArgs.Button == MouseButtons.Right))
				{
					if (base.Polygon.Count > 1)
					{
						base.Polygon.DeletePoint(base.Polygon.Count - 1);
					}
					this.InvokeLeave(false, false);
				}
			}

			// Token: 0x060009FB RID: 2555 RVA: 0x00052BFB File Offset: 0x00051BFB
			private void OnProperties(MethodArgs methodArgs)
			{
				this.InvokeLeave(false, false);
			}

			// Token: 0x060009FC RID: 2556 RVA: 0x00052C05 File Offset: 0x00051C05
			private void OnBreak(MethodArgs methodArgs)
			{
				this.InvokeLeave(true, false);
			}

			// Token: 0x060009FD RID: 2557 RVA: 0x00052C10 File Offset: 0x00051C10
			public AddPolygonSubstate(Polygon _polygon, PolygonContainer _polygonContainer, LandscapeRegionParams _landscapeRegionParams, MultiState _multiState, Color _regionColor, ICollisionMap _collisionMap, EditorScene _editorScene, int _editorSceneViewID) : base("AddPolygonSubstate", _polygon, _polygonContainer, _landscapeRegionParams, _multiState, _regionColor, _collisionMap, _editorScene, _editorSceneViewID)
			{
				base.AddMethod("mouse_move", new Method(this.OnMouseMove));
				base.AddMethod("mouse_up", new Method(this.OnMouseUp));
				base.AddMethod("mouse_double_click", new Method(this.OnMouseDoubleClick));
				base.AddMethod("properties", new Method(this.OnProperties));
				base.AddMethod("break", new Method(this.OnBreak));
				this.EnterState = (State.ActivateEvent)Delegate.Combine(this.EnterState, new State.ActivateEvent(this.OnEnterState));
				this.LeaveState = (State.ActivateEvent)Delegate.Combine(this.LeaveState, new State.ActivateEvent(this.OnLeaveState));
			}

			// Token: 0x060009FE RID: 2558 RVA: 0x00052CEC File Offset: 0x00051CEC
			public override void Destroy()
			{
				this.EnterState = (State.ActivateEvent)Delegate.Remove(this.EnterState, new State.ActivateEvent(this.OnEnterState));
				this.LeaveState = (State.ActivateEvent)Delegate.Remove(this.LeaveState, new State.ActivateEvent(this.OnLeaveState));
				base.Destroy();
			}

			// Token: 0x060009FF RID: 2559 RVA: 0x00052D44 File Offset: 0x00051D44
			public void InvokeLeave(bool _restore, bool force)
			{
				if (!force && ((_restore && base.Backup.Empty) || (!_restore && base.Polygon.Count < 3)))
				{
					base.Polygon.Clear();
					base.UpdateLastPosition();
					this.AddPoint();
					return;
				}
				base.Restore = _restore;
				this.InvokeLeave();
			}
		}

		// Token: 0x020000C3 RID: 195
		internal class EditPolygonSubstate : LandscapeState.PolygonSubstate
		{
			// Token: 0x06000A00 RID: 2560 RVA: 0x00052D9C File Offset: 0x00051D9C
			private void OnEnterState(IState state)
			{
				base.Backup.Backup(base.Polygon);
				base.Polygon.Color = base.RegionColor;
				base.Polygon.Selection = this.pointTested;
				base.PolygonContainer.AddPolygon(base.Polygon);
				base.UpdateLastPosition();
				Vec3 position = base.LastPosition;
				if (base.Polygon.Selection != -1)
				{
					this.polygonStartData.Create(base.Polygon, ref position);
				}
				else if (this.centerTested)
				{
					if (base.Polygon.ClassifyPoint(ref position) == PerimeterArea.Outside)
					{
						base.Polygon.MoveTo(position);
					}
					this.polygonStartData.Create(base.Polygon, ref position);
				}
				base.Restore = true;
			}

			// Token: 0x06000A01 RID: 2561 RVA: 0x00052E60 File Offset: 0x00051E60
			private void OnLeaveState(IState state)
			{
				base.PolygonContainer.RemovePolygon(base.Polygon);
				base.Polygon.Selection = -1;
				base.Backup.Restore(base.Polygon, base.Restore);
				base.Restore = true;
				this.pointTested = -1;
				this.centerTested = false;
			}

			// Token: 0x06000A02 RID: 2562 RVA: 0x00052EB8 File Offset: 0x00051EB8
			private void OnMouseMove(MethodArgs methodArgs)
			{
				MouseEventArgs mouseEventArgs = methodArgs.eventArgs as MouseEventArgs;
				if (mouseEventArgs != null && !KeyStatus.RightMouse)
				{
					base.UpdateLastPosition(mouseEventArgs.X, mouseEventArgs.Y);
					if (base.Polygon.Selection != -1)
					{
						if (KeyStatus.Control)
						{
							base.Polygon.MoveTo(base.LastPosition - this.polygonStartData.CenterDiff);
							return;
						}
						if (!KeyStatus.Shift)
						{
							base.Polygon.MovePointTo(base.Polygon.Selection, base.LastPosition - this.polygonStartData.PointDiff);
							return;
						}
						Vec3 direction = base.LastPosition - this.polygonStartData.StartPosition;
						double length = direction.Normalize();
						if (length > MathConsts.DOUBLE_EPSILON)
						{
							Vec3 bisector;
							double bisectorLength;
							base.Polygon.GetCounterclockwiseBisector(base.Polygon.Selection, out bisector, out bisectorLength);
							if (bisector.Length2 > MathConsts.DOUBLE_EPSILON_2)
							{
								length = length * Vec3.Dot(bisector, direction) * ((base.Polygon.Rotation == RotationType.Clockwise) ? -1.0 : 1.0);
								base.Polygon.Enlarge(this.polygonStartData.StartPoints, length);
								return;
							}
						}
					}
					else if (KeyStatus.LeftMouse || KeyStatus.MiddleMouse)
					{
						base.Polygon.MoveTo(base.LastPosition - this.polygonStartData.CenterDiff);
					}
				}
			}

			// Token: 0x06000A03 RID: 2563 RVA: 0x00053034 File Offset: 0x00052034
			private void OnMouseDown(MethodArgs methodArgs)
			{
				MouseEventArgs mouseEventArgs = methodArgs.eventArgs as MouseEventArgs;
				if (mouseEventArgs != null && mouseEventArgs.Button != MouseButtons.Right)
				{
					base.UpdateLastPosition(mouseEventArgs.X, mouseEventArgs.Y);
					if (mouseEventArgs.Button == MouseButtons.Left || mouseEventArgs.Button == MouseButtons.Middle)
					{
						Vec3 position = base.LastPosition;
						base.Polygon.Selection = base.Polygon.LocateNearestPoint(ref position, 1.0);
						this.polygonStartData.Create(base.Polygon, ref position);
					}
				}
			}

			// Token: 0x06000A04 RID: 2564 RVA: 0x000530C8 File Offset: 0x000520C8
			private void OnMouseUp(MethodArgs methodArgs)
			{
				MouseEventArgs mouseEventArgs = methodArgs.eventArgs as MouseEventArgs;
				if (mouseEventArgs != null && mouseEventArgs.Button != MouseButtons.Right)
				{
					base.Polygon.Selection = -1;
					if (this.pointTested != -1)
					{
						this.InvokeLeave(false);
						return;
					}
					if (this.centerTested)
					{
						this.InvokeLeave(false);
					}
				}
			}

			// Token: 0x06000A05 RID: 2565 RVA: 0x00053120 File Offset: 0x00052120
			private void OnMouseDoubleClick(MethodArgs methodArgs)
			{
				MouseEventArgs mouseEventArgs = methodArgs.eventArgs as MouseEventArgs;
				if (mouseEventArgs != null && (mouseEventArgs.Button == MouseButtons.Left || mouseEventArgs.Button == MouseButtons.Middle || mouseEventArgs.Button == MouseButtons.Right))
				{
					this.InvokeLeave(false);
				}
			}

			// Token: 0x06000A06 RID: 2566 RVA: 0x0005316B File Offset: 0x0005216B
			private void OnProperties(MethodArgs methodArgs)
			{
				this.InvokeLeave(false);
			}

			// Token: 0x06000A07 RID: 2567 RVA: 0x00053174 File Offset: 0x00052174
			private void OnBreak(MethodArgs methodArgs)
			{
				this.InvokeLeave(true);
			}

			// Token: 0x06000A08 RID: 2568 RVA: 0x00053180 File Offset: 0x00052180
			private void OnAddPoint(MethodArgs methodArgs)
			{
				if (base.Polygon.Selection != -1)
				{
					Vec3 newPoint;
					if (base.Polygon.Selection == 0)
					{
						newPoint = (base.Polygon.Points[base.Polygon.Count - 1] + base.Polygon.Points[0]) / 2.0;
					}
					else
					{
						newPoint = (base.Polygon.Points[base.Polygon.Selection - 1] + base.Polygon.Points[base.Polygon.Selection]) / 2.0;
					}
					newPoint.Z = base.CollisionMap.GetTerrainHeight(newPoint.X, newPoint.Y);
					base.Polygon.InsertPoint(base.Polygon.Selection, newPoint, true, false);
					base.Polygon.Selection++;
				}
			}

			// Token: 0x06000A09 RID: 2569 RVA: 0x00053288 File Offset: 0x00052288
			private void OnRemovePoint(MethodArgs methodArgs)
			{
				if (base.Polygon.Count > 3 && base.Polygon.Selection != -1)
				{
					base.Polygon.DeletePoint(base.Polygon.Selection);
					base.Polygon.Selection = -1;
					if (this.pointTested != -1)
					{
						this.InvokeLeave(false);
					}
				}
			}

			// Token: 0x06000A0A RID: 2570 RVA: 0x000532E4 File Offset: 0x000522E4
			public EditPolygonSubstate(Polygon _polygon, PolygonContainer _polygonContainer, LandscapeRegionParams _landscapeRegionParams, MultiState _multiState, Color _regionColor, ICollisionMap _collisionMap, EditorScene _editorScene, int _editorSceneViewID) : base("EditPolygonSubstate", _polygon, _polygonContainer, _landscapeRegionParams, _multiState, _regionColor, _collisionMap, _editorScene, _editorSceneViewID)
			{
				base.AddMethod("mouse_move", new Method(this.OnMouseMove));
				base.AddMethod("mouse_down", new Method(this.OnMouseDown));
				base.AddMethod("mouse_up", new Method(this.OnMouseUp));
				base.AddMethod("mouse_double_click", new Method(this.OnMouseDoubleClick));
				base.AddMethod("properties", new Method(this.OnProperties));
				base.AddMethod("add", new Method(this.OnAddPoint));
				base.AddMethod("remove", new Method(this.OnRemovePoint));
				base.AddMethod("break", new Method(this.OnBreak));
				this.EnterState = (State.ActivateEvent)Delegate.Combine(this.EnterState, new State.ActivateEvent(this.OnEnterState));
				this.LeaveState = (State.ActivateEvent)Delegate.Combine(this.LeaveState, new State.ActivateEvent(this.OnLeaveState));
			}

			// Token: 0x06000A0B RID: 2571 RVA: 0x00053418 File Offset: 0x00052418
			public override void Destroy()
			{
				this.EnterState = (State.ActivateEvent)Delegate.Remove(this.EnterState, new State.ActivateEvent(this.OnEnterState));
				this.LeaveState = (State.ActivateEvent)Delegate.Remove(this.LeaveState, new State.ActivateEvent(this.OnLeaveState));
				base.Destroy();
			}

			// Token: 0x06000A0C RID: 2572 RVA: 0x0005346F File Offset: 0x0005246F
			public void InvokeLeave(bool _restore)
			{
				base.Restore = _restore;
				this.InvokeLeave();
			}

			// Token: 0x17000209 RID: 521
			// (get) Token: 0x06000A0D RID: 2573 RVA: 0x0005347E File Offset: 0x0005247E
			// (set) Token: 0x06000A0E RID: 2574 RVA: 0x00053486 File Offset: 0x00052486
			public int PointTested
			{
				get
				{
					return this.pointTested;
				}
				set
				{
					this.pointTested = value;
				}
			}

			// Token: 0x1700020A RID: 522
			// (get) Token: 0x06000A0F RID: 2575 RVA: 0x0005348F File Offset: 0x0005248F
			// (set) Token: 0x06000A10 RID: 2576 RVA: 0x00053497 File Offset: 0x00052497
			public bool CenterTested
			{
				get
				{
					return this.centerTested;
				}
				set
				{
					this.centerTested = value;
				}
			}

			// Token: 0x040007FC RID: 2044
			private readonly LandscapeState.EditPolygonSubstate.PolygonStartData polygonStartData = new LandscapeState.EditPolygonSubstate.PolygonStartData();

			// Token: 0x040007FD RID: 2045
			private int pointTested = -1;

			// Token: 0x040007FE RID: 2046
			private bool centerTested;

			// Token: 0x020000C4 RID: 196
			internal class PolygonStartData
			{
				// Token: 0x1700020B RID: 523
				// (get) Token: 0x06000A11 RID: 2577 RVA: 0x000534A0 File Offset: 0x000524A0
				public List<Vec3> StartPoints
				{
					get
					{
						return this.startPoints;
					}
				}

				// Token: 0x1700020C RID: 524
				// (get) Token: 0x06000A12 RID: 2578 RVA: 0x000534A8 File Offset: 0x000524A8
				public Vec3 StartPosition
				{
					get
					{
						return this.startPosition;
					}
				}

				// Token: 0x1700020D RID: 525
				// (get) Token: 0x06000A13 RID: 2579 RVA: 0x000534B0 File Offset: 0x000524B0
				public Vec3 CenterDiff
				{
					get
					{
						return this.centerDiff;
					}
				}

				// Token: 0x1700020E RID: 526
				// (get) Token: 0x06000A14 RID: 2580 RVA: 0x000534B8 File Offset: 0x000524B8
				public Vec3 PointDiff
				{
					get
					{
						return this.pointDiff;
					}
				}

				// Token: 0x06000A15 RID: 2581 RVA: 0x000534C0 File Offset: 0x000524C0
				public void Create(Polygon polygon, ref Vec3 position)
				{
					this.startPosition = position;
					this.startPoints.Clear();
					this.startPoints.AddRange(polygon.Points);
					this.centerDiff = this.startPosition - polygon.Center;
					if (polygon.Selection != -1)
					{
						this.pointDiff = this.startPosition - polygon.Points[polygon.Selection];
						return;
					}
					this.pointDiff = Vec3.Empty;
				}

				// Token: 0x040007FF RID: 2047
				private readonly List<Vec3> startPoints = new List<Vec3>();

				// Token: 0x04000800 RID: 2048
				private Vec3 startPosition = Vec3.Empty;

				// Token: 0x04000801 RID: 2049
				private Vec3 centerDiff = Vec3.Empty;

				// Token: 0x04000802 RID: 2050
				private Vec3 pointDiff = Vec3.Empty;
			}
		}

		// Token: 0x020000C5 RID: 197
		internal class StripeSubstate : LandscapeState.RegionSubstate
		{
			// Token: 0x1700020F RID: 527
			// (get) Token: 0x06000A17 RID: 2583 RVA: 0x00053577 File Offset: 0x00052577
			public LandscapeState.StripeSubstate.StripeBackup Backup
			{
				get
				{
					return this.backup;
				}
			}

			// Token: 0x17000210 RID: 528
			// (get) Token: 0x06000A18 RID: 2584 RVA: 0x0005357F File Offset: 0x0005257F
			// (set) Token: 0x06000A19 RID: 2585 RVA: 0x00053587 File Offset: 0x00052587
			public bool Restore
			{
				get
				{
					return this.restore;
				}
				set
				{
					this.restore = value;
				}
			}

			// Token: 0x17000211 RID: 529
			// (get) Token: 0x06000A1A RID: 2586 RVA: 0x00053590 File Offset: 0x00052590
			public Stripe Stripe
			{
				get
				{
					return this.stripe;
				}
			}

			// Token: 0x17000212 RID: 530
			// (get) Token: 0x06000A1B RID: 2587 RVA: 0x00053598 File Offset: 0x00052598
			public StripeContainer StripeContainer
			{
				get
				{
					return this.stripeContainer;
				}
			}

			// Token: 0x06000A1C RID: 2588 RVA: 0x000535A0 File Offset: 0x000525A0
			public StripeSubstate(string _stateLabel, Stripe _stripe, StripeContainer _stripeContainer, LandscapeRegionParams _landscapeRegionParams, MultiState _multiState, Color _regionColor, ICollisionMap _collisionMap, EditorScene _editorScene, int _editorSceneViewID) : base(_stateLabel, _landscapeRegionParams, _multiState, _regionColor, _collisionMap, _editorScene, _editorSceneViewID)
			{
				this.stripe = _stripe;
				this.stripeContainer = _stripeContainer;
			}

			// Token: 0x06000A1D RID: 2589 RVA: 0x000535D5 File Offset: 0x000525D5
			public override void Destroy()
			{
				this.stripe = null;
				this.stripeContainer = null;
				base.Destroy();
			}

			// Token: 0x04000803 RID: 2051
			private readonly LandscapeState.StripeSubstate.StripeBackup backup = new LandscapeState.StripeSubstate.StripeBackup();

			// Token: 0x04000804 RID: 2052
			private bool restore = true;

			// Token: 0x04000805 RID: 2053
			private Stripe stripe;

			// Token: 0x04000806 RID: 2054
			private StripeContainer stripeContainer;

			// Token: 0x020000C6 RID: 198
			internal class StripeBackup
			{
				// Token: 0x06000A1E RID: 2590 RVA: 0x000535EC File Offset: 0x000525EC
				public void Backup(Stripe stripe)
				{
					if (stripe != null)
					{
						this.points.Clear();
						this.nodes.Clear();
						this.points.AddRange(stripe.Center.Points);
						this.nodes.AddRange(stripe.Nodes);
						this.previousCenterColor = stripe.CenterColor;
						this.previousBoundsColor = stripe.BoundsColor;
					}
				}

				// Token: 0x06000A1F RID: 2591 RVA: 0x00053654 File Offset: 0x00052654
				public void Restore(Stripe stripe, bool restorePoints)
				{
					if (stripe != null)
					{
						stripe.CenterColor = this.previousCenterColor;
						stripe.BoundsColor = this.previousBoundsColor;
						if (restorePoints)
						{
							stripe.Clear();
							if (this.points.Count > 1 && this.nodes.Count > 1)
							{
								int index = 0;
								while (index < this.points.Count && index < this.nodes.Count)
								{
									stripe.AddPoint(this.points[index], this.nodes[index], false, false);
									index++;
								}
							}
						}
					}
				}

				// Token: 0x06000A20 RID: 2592 RVA: 0x000536EA File Offset: 0x000526EA
				public void Clear()
				{
					this.previousCenterColor = Color.White;
					this.previousBoundsColor = Color.White;
					this.points.Clear();
					this.nodes.Clear();
				}

				// Token: 0x17000213 RID: 531
				// (get) Token: 0x06000A21 RID: 2593 RVA: 0x00053718 File Offset: 0x00052718
				public bool Empty
				{
					get
					{
						return this.points.Count < 2 && this.nodes.Count < 2;
					}
				}

				// Token: 0x04000807 RID: 2055
				private Color previousCenterColor = Color.White;

				// Token: 0x04000808 RID: 2056
				private Color previousBoundsColor = Color.White;

				// Token: 0x04000809 RID: 2057
				private readonly List<Vec3> points = new List<Vec3>();

				// Token: 0x0400080A RID: 2058
				private readonly List<Stripe.Node> nodes = new List<Stripe.Node>();
			}
		}

		// Token: 0x020000C7 RID: 199
		internal class AddStripeSubstate : LandscapeState.StripeSubstate
		{
			// Token: 0x06000A23 RID: 2595 RVA: 0x0005376C File Offset: 0x0005276C
			private void MoveLastPointTo(int x, int y)
			{
				base.UpdateLastPosition(x, y);
				if (base.Stripe.Count > 0)
				{
					base.Stripe.MovePointTo(base.Stripe.Count - 1, base.LastPosition);
				}
			}

			// Token: 0x06000A24 RID: 2596 RVA: 0x000537A3 File Offset: 0x000527A3
			private void RemovePoint()
			{
				if (base.Stripe.Count > 1)
				{
					base.Stripe.DeletePoint(base.Stripe.Count - 2);
				}
			}

			// Token: 0x06000A25 RID: 2597 RVA: 0x000537CC File Offset: 0x000527CC
			private void AddPoint()
			{
				if (base.Stripe.Count > 0)
				{
					base.Stripe.InsertPoint(base.Stripe.Count - 1, base.LastPosition, (double)base.LandscapeRegionParams.Size / 2.0, true, false);
					return;
				}
				base.Stripe.AddPoint(base.LastPosition, (double)base.LandscapeRegionParams.Size / 2.0, false, false);
			}

			// Token: 0x06000A26 RID: 2598 RVA: 0x0005384C File Offset: 0x0005284C
			private void OnEnterState(IState state)
			{
				base.Backup.Backup(base.Stripe);
				base.Stripe.Clear();
				base.Stripe.CenterColor = base.RegionColor;
				base.Stripe.BoundsColor = base.RegionColor;
				base.StripeContainer.AddStripe(base.Stripe);
				base.Restore = true;
				base.UpdateLastPosition();
				this.AddPoint();
			}

			// Token: 0x06000A27 RID: 2599 RVA: 0x000538BD File Offset: 0x000528BD
			private void OnLeaveState(IState state)
			{
				base.StripeContainer.RemoveStripe(base.Stripe);
				base.Backup.Restore(base.Stripe, base.Restore);
				base.Restore = true;
			}

			// Token: 0x06000A28 RID: 2600 RVA: 0x000538F0 File Offset: 0x000528F0
			private void OnMouseMove(MethodArgs methodArgs)
			{
				MouseEventArgs mouseEventArgs = methodArgs.eventArgs as MouseEventArgs;
				if (mouseEventArgs != null && !KeyStatus.RightMouse)
				{
					this.MoveLastPointTo(mouseEventArgs.X, mouseEventArgs.Y);
				}
			}

			// Token: 0x06000A29 RID: 2601 RVA: 0x00053928 File Offset: 0x00052928
			private void OnMouseUp(MethodArgs methodArgs)
			{
				MouseEventArgs mouseEventArgs = methodArgs.eventArgs as MouseEventArgs;
				if (mouseEventArgs != null)
				{
					if (mouseEventArgs.Button == MouseButtons.Middle && base.Stripe.Count > 1)
					{
						this.RemovePoint();
						this.MoveLastPointTo(mouseEventArgs.X, mouseEventArgs.Y);
					}
					if (mouseEventArgs.Button == MouseButtons.Left)
					{
						this.MoveLastPointTo(mouseEventArgs.X, mouseEventArgs.Y);
						this.AddPoint();
						this.MoveLastPointTo(mouseEventArgs.X, mouseEventArgs.Y);
					}
				}
			}

			// Token: 0x06000A2A RID: 2602 RVA: 0x000539B0 File Offset: 0x000529B0
			private void OnMouseDoubleClick(MethodArgs methodArgs)
			{
				MouseEventArgs mouseEventArgs = methodArgs.eventArgs as MouseEventArgs;
				if (mouseEventArgs != null && (mouseEventArgs.Button == MouseButtons.Left || mouseEventArgs.Button == MouseButtons.Middle || mouseEventArgs.Button == MouseButtons.Right))
				{
					if (base.Stripe.Count > 1)
					{
						base.Stripe.DeletePoint(base.Stripe.Count - 1);
					}
					this.InvokeLeave(false, false);
				}
			}

			// Token: 0x06000A2B RID: 2603 RVA: 0x00053A23 File Offset: 0x00052A23
			private void OnProperties(MethodArgs methodArgs)
			{
				this.InvokeLeave(false, false);
			}

			// Token: 0x06000A2C RID: 2604 RVA: 0x00053A2D File Offset: 0x00052A2D
			private void OnBreak(MethodArgs methodArgs)
			{
				this.InvokeLeave(true, false);
			}

			// Token: 0x06000A2D RID: 2605 RVA: 0x00053A38 File Offset: 0x00052A38
			public AddStripeSubstate(Stripe _stripe, StripeContainer _stripeContainer, LandscapeRegionParams _landscapeRegionParams, MultiState _multiState, Color _regionColor, ICollisionMap _collisionMap, EditorScene _editorScene, int _editorSceneViewID) : base("AddStripeSubstate", _stripe, _stripeContainer, _landscapeRegionParams, _multiState, _regionColor, _collisionMap, _editorScene, _editorSceneViewID)
			{
				base.AddMethod("mouse_move", new Method(this.OnMouseMove));
				base.AddMethod("mouse_up", new Method(this.OnMouseUp));
				base.AddMethod("mouse_double_click", new Method(this.OnMouseDoubleClick));
				base.AddMethod("properties", new Method(this.OnProperties));
				base.AddMethod("break", new Method(this.OnBreak));
				this.EnterState = (State.ActivateEvent)Delegate.Combine(this.EnterState, new State.ActivateEvent(this.OnEnterState));
				this.LeaveState = (State.ActivateEvent)Delegate.Combine(this.LeaveState, new State.ActivateEvent(this.OnLeaveState));
			}

			// Token: 0x06000A2E RID: 2606 RVA: 0x00053B14 File Offset: 0x00052B14
			public override void Destroy()
			{
				this.EnterState = (State.ActivateEvent)Delegate.Remove(this.EnterState, new State.ActivateEvent(this.OnEnterState));
				this.LeaveState = (State.ActivateEvent)Delegate.Remove(this.LeaveState, new State.ActivateEvent(this.OnLeaveState));
				base.Destroy();
			}

			// Token: 0x06000A2F RID: 2607 RVA: 0x00053B6C File Offset: 0x00052B6C
			public void InvokeLeave(bool _restore, bool force)
			{
				if (!force && ((_restore && base.Backup.Empty) || (!_restore && base.Stripe.Count < 2)))
				{
					base.Stripe.Clear();
					base.UpdateLastPosition();
					this.AddPoint();
					return;
				}
				base.Restore = _restore;
				this.InvokeLeave();
			}
		}

		// Token: 0x020000C8 RID: 200
		internal class EditStripeSubstate : LandscapeState.StripeSubstate
		{
			// Token: 0x06000A30 RID: 2608 RVA: 0x00053BC4 File Offset: 0x00052BC4
			private void OnEnterState(IState state)
			{
				base.Backup.Backup(base.Stripe);
				base.Stripe.CenterColor = base.RegionColor;
				base.Stripe.BoundsColor = base.RegionColor;
				base.Stripe.Center.Selection = this.pointTested;
				base.Stripe.Bounds.Selection = this.boundsTested;
				base.StripeContainer.AddStripe(base.Stripe);
				base.UpdateLastPosition();
				Vec3 position = base.LastPosition;
				if (base.Stripe.Center.Selection != -1)
				{
					this.stripeStartData.Create(base.Stripe, ref position);
				}
				else if (base.Stripe.Bounds.Selection != -1)
				{
					this.stripeStartData.Create(base.Stripe, ref position);
				}
				else if (this.centerTested)
				{
					if (base.Stripe.ClassifyPoint(ref position) == PerimeterArea.Outside)
					{
						base.Stripe.MoveTo(position);
					}
					this.stripeStartData.Create(base.Stripe, ref position);
				}
				base.Restore = true;
			}

			// Token: 0x06000A31 RID: 2609 RVA: 0x00053CE0 File Offset: 0x00052CE0
			private void OnLeaveState(IState state)
			{
				base.StripeContainer.RemoveStripe(base.Stripe);
				base.Stripe.Center.Selection = -1;
				base.Stripe.Bounds.Selection = -1;
				base.Backup.Restore(base.Stripe, base.Restore);
				base.Restore = true;
				this.pointTested = -1;
				this.boundsTested = -1;
				this.centerTested = false;
			}

			// Token: 0x06000A32 RID: 2610 RVA: 0x00053D54 File Offset: 0x00052D54
			private void OnMouseMove(MethodArgs methodArgs)
			{
				MouseEventArgs mouseEventArgs = methodArgs.eventArgs as MouseEventArgs;
				if (mouseEventArgs != null && !KeyStatus.RightMouse)
				{
					base.UpdateLastPosition(mouseEventArgs.X, mouseEventArgs.Y);
					if (base.Stripe.Center.Selection != -1)
					{
						if (KeyStatus.Control)
						{
							base.Stripe.MoveTo(base.LastPosition - this.stripeStartData.CenterDiff);
							return;
						}
						base.Stripe.MovePointTo(base.Stripe.Center.Selection, base.LastPosition - this.stripeStartData.PointDiff);
						return;
					}
					else if (base.Stripe.Bounds.Selection != -1)
					{
						if (KeyStatus.Control)
						{
							base.Stripe.MoveTo(base.LastPosition - this.stripeStartData.CenterDiff);
							return;
						}
						int centerIndex;
						double side;
						if (base.Stripe.Bounds.Selection >= base.Stripe.Center.Count)
						{
							centerIndex = base.Stripe.Center.Count - base.Stripe.Bounds.Selection % base.Stripe.Center.Count - 1;
							side = -1.0;
						}
						else
						{
							centerIndex = base.Stripe.Bounds.Selection;
							side = 1.0;
						}
						Vec3 _direction = base.LastPosition - this.stripeStartData.StartPosition;
						Vec2 direction = new Vec2(_direction.X, _direction.Y);
						double length = direction.Normalize();
						if (length > MathConsts.DOUBLE_EPSILON)
						{
							Vec2 bisector;
							double bisectorLength;
							base.Stripe.Center.GetCounterclockwiseBisector(centerIndex, out bisector, out bisectorLength);
							if (bisector.Length2 > MathConsts.DOUBLE_EPSILON_2)
							{
								length = length * Vec2.Dot(bisector, direction) * side;
								if (KeyStatus.Shift)
								{
									base.Stripe.Enlarge(this.stripeStartData.StartNodes, length);
									return;
								}
								base.Stripe.Enlarge(centerIndex, this.stripeStartData.StartNodes[centerIndex], length);
								return;
							}
						}
					}
					else if (KeyStatus.LeftMouse || KeyStatus.MiddleMouse)
					{
						base.Stripe.MoveTo(base.LastPosition - this.stripeStartData.CenterDiff);
					}
				}
			}

			// Token: 0x06000A33 RID: 2611 RVA: 0x00053FA8 File Offset: 0x00052FA8
			private void OnMouseDown(MethodArgs methodArgs)
			{
				MouseEventArgs mouseEventArgs = methodArgs.eventArgs as MouseEventArgs;
				if (mouseEventArgs != null && mouseEventArgs.Button != MouseButtons.Right)
				{
					base.UpdateLastPosition(mouseEventArgs.X, mouseEventArgs.Y);
					if (mouseEventArgs.Button == MouseButtons.Left || mouseEventArgs.Button == MouseButtons.Middle)
					{
						Vec3 position = base.LastPosition;
						base.Stripe.Center.Selection = base.Stripe.Center.LocateNearestPoint(ref position, 1.0);
						if (base.Stripe.Center.Selection == -1)
						{
							base.Stripe.Bounds.Selection = base.Stripe.Bounds.LocateNearestPoint(ref position, 1.0);
						}
						this.stripeStartData.Create(base.Stripe, ref position);
					}
				}
			}

			// Token: 0x06000A34 RID: 2612 RVA: 0x0005408C File Offset: 0x0005308C
			private void OnMouseUp(MethodArgs methodArgs)
			{
				MouseEventArgs mouseEventArgs = methodArgs.eventArgs as MouseEventArgs;
				if (mouseEventArgs != null && mouseEventArgs.Button != MouseButtons.Right)
				{
					base.Stripe.Center.Selection = -1;
					base.Stripe.Bounds.Selection = -1;
					if (this.pointTested != -1)
					{
						this.InvokeLeave(false);
						return;
					}
					if (this.boundsTested != -1)
					{
						this.InvokeLeave(false);
						return;
					}
					if (this.centerTested)
					{
						this.InvokeLeave(false);
					}
				}
			}

			// Token: 0x06000A35 RID: 2613 RVA: 0x0005410C File Offset: 0x0005310C
			private void OnMouseDoubleClick(MethodArgs methodArgs)
			{
				MouseEventArgs mouseEventArgs = methodArgs.eventArgs as MouseEventArgs;
				if (mouseEventArgs != null && (mouseEventArgs.Button == MouseButtons.Left || mouseEventArgs.Button == MouseButtons.Middle || mouseEventArgs.Button == MouseButtons.Right))
				{
					this.InvokeLeave(false);
				}
			}

			// Token: 0x06000A36 RID: 2614 RVA: 0x00054157 File Offset: 0x00053157
			private void OnProperties(MethodArgs methodArgs)
			{
				this.InvokeLeave(false);
			}

			// Token: 0x06000A37 RID: 2615 RVA: 0x00054160 File Offset: 0x00053160
			private void OnBreak(MethodArgs methodArgs)
			{
				this.InvokeLeave(true);
			}

			// Token: 0x06000A38 RID: 2616 RVA: 0x0005416C File Offset: 0x0005316C
			private void OnAddPoint(MethodArgs methodArgs)
			{
				if (base.Stripe.Center.Selection != -1 && base.Stripe.Center.Selection > 0)
				{
					Vec3 newPoint = (base.Stripe.Center.Points[base.Stripe.Center.Selection - 1] + base.Stripe.Center.Points[base.Stripe.Center.Selection]) / 2.0;
					newPoint.Z = base.CollisionMap.GetTerrainHeight(newPoint.X, newPoint.Y);
					base.Stripe.InsertPoint(base.Stripe.Center.Selection, newPoint, (double)base.LandscapeRegionParams.Size / 2.0, true, false);
					base.Stripe.Center.Selection++;
				}
			}

			// Token: 0x06000A39 RID: 2617 RVA: 0x00054274 File Offset: 0x00053274
			private void OnRemovePoint(MethodArgs methodArgs)
			{
				if (base.Stripe.Count > 2 && base.Stripe.Center.Selection != -1)
				{
					base.Stripe.DeletePoint(base.Stripe.Center.Selection);
					base.Stripe.Center.Selection = -1;
					if (this.pointTested != -1)
					{
						this.InvokeLeave(false);
					}
				}
			}

			// Token: 0x06000A3A RID: 2618 RVA: 0x000542E0 File Offset: 0x000532E0
			public EditStripeSubstate(Stripe _stripe, StripeContainer _stripeContainer, LandscapeRegionParams _landscapeRegionParams, MultiState _multiState, Color _regionColor, ICollisionMap _collisionMap, EditorScene _editorScene, int _editorSceneViewID) : base("EditStripeSubstate", _stripe, _stripeContainer, _landscapeRegionParams, _multiState, _regionColor, _collisionMap, _editorScene, _editorSceneViewID)
			{
				base.AddMethod("mouse_move", new Method(this.OnMouseMove));
				base.AddMethod("mouse_down", new Method(this.OnMouseDown));
				base.AddMethod("mouse_up", new Method(this.OnMouseUp));
				base.AddMethod("mouse_double_click", new Method(this.OnMouseDoubleClick));
				base.AddMethod("properties", new Method(this.OnProperties));
				base.AddMethod("add", new Method(this.OnAddPoint));
				base.AddMethod("remove", new Method(this.OnRemovePoint));
				base.AddMethod("break", new Method(this.OnBreak));
				this.EnterState = (State.ActivateEvent)Delegate.Combine(this.EnterState, new State.ActivateEvent(this.OnEnterState));
				this.LeaveState = (State.ActivateEvent)Delegate.Combine(this.LeaveState, new State.ActivateEvent(this.OnLeaveState));
			}

			// Token: 0x06000A3B RID: 2619 RVA: 0x0005441C File Offset: 0x0005341C
			public override void Destroy()
			{
				this.EnterState = (State.ActivateEvent)Delegate.Remove(this.EnterState, new State.ActivateEvent(this.OnEnterState));
				this.LeaveState = (State.ActivateEvent)Delegate.Remove(this.LeaveState, new State.ActivateEvent(this.OnLeaveState));
				base.Destroy();
			}

			// Token: 0x06000A3C RID: 2620 RVA: 0x00054473 File Offset: 0x00053473
			public void InvokeLeave(bool _restore)
			{
				base.Restore = _restore;
				this.InvokeLeave();
			}

			// Token: 0x17000214 RID: 532
			// (get) Token: 0x06000A3D RID: 2621 RVA: 0x00054482 File Offset: 0x00053482
			// (set) Token: 0x06000A3E RID: 2622 RVA: 0x0005448A File Offset: 0x0005348A
			public int PointTested
			{
				get
				{
					return this.pointTested;
				}
				set
				{
					this.pointTested = value;
				}
			}

			// Token: 0x17000215 RID: 533
			// (get) Token: 0x06000A3F RID: 2623 RVA: 0x00054493 File Offset: 0x00053493
			// (set) Token: 0x06000A40 RID: 2624 RVA: 0x0005449B File Offset: 0x0005349B
			public int BoundsTested
			{
				get
				{
					return this.boundsTested;
				}
				set
				{
					this.boundsTested = value;
				}
			}

			// Token: 0x17000216 RID: 534
			// (get) Token: 0x06000A41 RID: 2625 RVA: 0x000544A4 File Offset: 0x000534A4
			// (set) Token: 0x06000A42 RID: 2626 RVA: 0x000544AC File Offset: 0x000534AC
			public bool CenterTested
			{
				get
				{
					return this.centerTested;
				}
				set
				{
					this.centerTested = value;
				}
			}

			// Token: 0x0400080B RID: 2059
			private readonly LandscapeState.EditStripeSubstate.StripeStartData stripeStartData = new LandscapeState.EditStripeSubstate.StripeStartData();

			// Token: 0x0400080C RID: 2060
			private int pointTested = -1;

			// Token: 0x0400080D RID: 2061
			private int boundsTested = -1;

			// Token: 0x0400080E RID: 2062
			private bool centerTested;

			// Token: 0x020000C9 RID: 201
			internal class StripeStartData
			{
				// Token: 0x17000217 RID: 535
				// (get) Token: 0x06000A43 RID: 2627 RVA: 0x000544B5 File Offset: 0x000534B5
				public List<Vec3> StartPoints
				{
					get
					{
						return this.startPoints;
					}
				}

				// Token: 0x17000218 RID: 536
				// (get) Token: 0x06000A44 RID: 2628 RVA: 0x000544BD File Offset: 0x000534BD
				public List<Stripe.Node> StartNodes
				{
					get
					{
						return this.startNodes;
					}
				}

				// Token: 0x17000219 RID: 537
				// (get) Token: 0x06000A45 RID: 2629 RVA: 0x000544C5 File Offset: 0x000534C5
				public Vec3 StartPosition
				{
					get
					{
						return this.startPosition;
					}
				}

				// Token: 0x1700021A RID: 538
				// (get) Token: 0x06000A46 RID: 2630 RVA: 0x000544CD File Offset: 0x000534CD
				public Vec3 CenterDiff
				{
					get
					{
						return this.centerDiff;
					}
				}

				// Token: 0x1700021B RID: 539
				// (get) Token: 0x06000A47 RID: 2631 RVA: 0x000544D5 File Offset: 0x000534D5
				public Vec3 PointDiff
				{
					get
					{
						return this.pointDiff;
					}
				}

				// Token: 0x06000A48 RID: 2632 RVA: 0x000544E0 File Offset: 0x000534E0
				public void Create(Stripe stripe, ref Vec3 position)
				{
					this.startPosition = position;
					this.startPoints.Clear();
					this.startNodes.Clear();
					this.startPoints.AddRange(stripe.Center.Points);
					this.startNodes.AddRange(stripe.Nodes);
					this.centerDiff = this.startPosition - stripe.CenterPoint;
					if (stripe.Center.Selection != -1)
					{
						this.pointDiff = this.startPosition - stripe.Center.Points[stripe.Center.Selection];
						return;
					}
					if (stripe.Bounds.Selection != -1)
					{
						this.pointDiff = this.startPosition - stripe.Bounds.Points[stripe.Bounds.Selection];
						return;
					}
					this.pointDiff = Vec3.Empty;
				}

				// Token: 0x0400080F RID: 2063
				private readonly List<Vec3> startPoints = new List<Vec3>();

				// Token: 0x04000810 RID: 2064
				private readonly List<Stripe.Node> startNodes = new List<Stripe.Node>();

				// Token: 0x04000811 RID: 2065
				private Vec3 startPosition = Vec3.Empty;

				// Token: 0x04000812 RID: 2066
				private Vec3 centerDiff = Vec3.Empty;

				// Token: 0x04000813 RID: 2067
				private Vec3 pointDiff = Vec3.Empty;
			}
		}

		// Token: 0x020000CA RID: 202
		internal class LandscapeToolSubstate : State
		{
			// Token: 0x1700021C RID: 540
			// (get) Token: 0x06000A4A RID: 2634 RVA: 0x00054610 File Offset: 0x00053610
			protected bool Created
			{
				get
				{
					return this.landscapeState != null && this.landscapeToolContext != null && this.landscapeParams != null && this.landscapeParams.LandscapeRegionParams != null && this.landscapeRegion != null && this.mapEditorLandscapeToolContainer != null && this.operationContainer != null;
				}
			}

			// Token: 0x1700021D RID: 541
			// (get) Token: 0x06000A4B RID: 2635 RVA: 0x00054660 File Offset: 0x00053660
			protected bool TransactionBegun
			{
				get
				{
					return this.transactionBegun;
				}
			}

			// Token: 0x06000A4C RID: 2636 RVA: 0x00054668 File Offset: 0x00053668
			protected void BeginTransaction()
			{
				if (this.operationContainer != null)
				{
					this.operationContainer.BeginTransaction();
					this.transactionBegun = true;
				}
			}

			// Token: 0x06000A4D RID: 2637 RVA: 0x00054688 File Offset: 0x00053688
			protected void EndTransaction(bool applyChanges)
			{
				if (this.operationContainer != null && this.transactionBegun)
				{
					if (applyChanges)
					{
						if (this.operationContainer.EndTransaction() && this.landscapeState.Container != null)
						{
							this.landscapeState.Container.Invoke("_landscape_state_applied", default(MethodArgs));
							return;
						}
					}
					else if (this.operationContainer.CancelTransaction() && this.landscapeState.Container != null)
					{
						this.landscapeState.Container.Invoke("_landscape_state_not_applied", default(MethodArgs));
					}
				}
			}

			// Token: 0x06000A4E RID: 2638 RVA: 0x00054719 File Offset: 0x00053719
			protected LandscapeToolSubstate(string _label, LandscapeState _landscapeState, LandscapeToolContext _landscapeToolContext, ILandscapeParams _landscapeParams, LandscapeRegion _landscapeRegion, MapEditorLandscapeToolContainer _mapEditorLandscapeToolContainer, OperationContainer _operationContainer) : base(_label)
			{
				this.landscapeState = _landscapeState;
				this.landscapeToolContext = _landscapeToolContext;
				this.landscapeParams = _landscapeParams;
				this.landscapeRegion = _landscapeRegion;
				this.mapEditorLandscapeToolContainer = _mapEditorLandscapeToolContainer;
				this.operationContainer = _operationContainer;
			}

			// Token: 0x06000A4F RID: 2639 RVA: 0x00054750 File Offset: 0x00053750
			public virtual void Destroy()
			{
				this.landscapeToolContext = null;
				this.landscapeRegion = null;
				this.landscapeParams = null;
				this.mapEditorLandscapeToolContainer = null;
				this.operationContainer = null;
				this.landscapeState = null;
			}

			// Token: 0x1700021E RID: 542
			// (get) Token: 0x06000A50 RID: 2640 RVA: 0x0005477C File Offset: 0x0005377C
			protected LandscapeState LandscapeState
			{
				get
				{
					return this.landscapeState;
				}
			}

			// Token: 0x1700021F RID: 543
			// (get) Token: 0x06000A51 RID: 2641 RVA: 0x00054784 File Offset: 0x00053784
			protected LandscapeToolContext LandscapeToolContext
			{
				get
				{
					return this.landscapeToolContext;
				}
			}

			// Token: 0x17000220 RID: 544
			// (get) Token: 0x06000A52 RID: 2642 RVA: 0x0005478C File Offset: 0x0005378C
			protected LandscapeRegion LandscapeRegion
			{
				get
				{
					return this.landscapeRegion;
				}
			}

			// Token: 0x17000221 RID: 545
			// (get) Token: 0x06000A53 RID: 2643 RVA: 0x00054794 File Offset: 0x00053794
			protected ILandscapeParams LandscapeParams
			{
				get
				{
					return this.landscapeParams;
				}
			}

			// Token: 0x17000222 RID: 546
			// (get) Token: 0x06000A54 RID: 2644 RVA: 0x0005479C File Offset: 0x0005379C
			protected MapEditorLandscapeToolContainer MapEditorLandscapeToolContainer
			{
				get
				{
					return this.mapEditorLandscapeToolContainer;
				}
			}

			// Token: 0x17000223 RID: 547
			// (get) Token: 0x06000A55 RID: 2645 RVA: 0x000547A4 File Offset: 0x000537A4
			protected OperationContainer OperationContainer
			{
				get
				{
					return this.operationContainer;
				}
			}

			// Token: 0x04000814 RID: 2068
			private LandscapeState landscapeState;

			// Token: 0x04000815 RID: 2069
			private LandscapeToolContext landscapeToolContext;

			// Token: 0x04000816 RID: 2070
			private LandscapeRegion landscapeRegion;

			// Token: 0x04000817 RID: 2071
			private ILandscapeParams landscapeParams;

			// Token: 0x04000818 RID: 2072
			private MapEditorLandscapeToolContainer mapEditorLandscapeToolContainer;

			// Token: 0x04000819 RID: 2073
			private OperationContainer operationContainer;

			// Token: 0x0400081A RID: 2074
			private bool transactionBegun;
		}

		// Token: 0x020000CB RID: 203
		internal class SimpleLandscapeToolSubstate : LandscapeState.LandscapeToolSubstate
		{
			// Token: 0x06000A56 RID: 2646 RVA: 0x000547AC File Offset: 0x000537AC
			private void CreateSimpleLandscapeTool()
			{
				if (this.simpleLandscapeToolID == -1)
				{
					this.simpleLandscapeToolID = base.MapEditorLandscapeToolContainer.AddMapEditorLandscapeTool(base.LandscapeToolContext, base.LandscapeRegion, base.LandscapeParams);
					ILandscapeTool landscapeTool;
					base.MapEditorLandscapeToolContainer.TryGetLandscapeTool(this.simpleLandscapeToolID, out landscapeTool);
					if (landscapeTool != null && !landscapeTool.Temporary)
					{
						base.BeginTransaction();
					}
				}
			}

			// Token: 0x06000A57 RID: 2647 RVA: 0x0005480A File Offset: 0x0005380A
			private void ApplySimpleLandscapeTool()
			{
				if (this.simpleLandscapeToolID != -1)
				{
					base.MapEditorLandscapeToolContainer.ApplyLandscapeTool(this.simpleLandscapeToolID);
				}
				base.LandscapeRegion.Update();
			}

			// Token: 0x06000A58 RID: 2648 RVA: 0x00054834 File Offset: 0x00053834
			private void UpdateLandscapeRegionCenter()
			{
				if (base.LandscapeParams.LandscapeRegionParams.Type == LandscapeRegionType.Ellipse || base.LandscapeParams.LandscapeRegionParams.Type == LandscapeRegionType.Square)
				{
					base.LandscapeRegion.Center = base.LandscapeToolContext.LandscapeToolContextPosition.CollisionMapPosition.Vec3;
				}
			}

			// Token: 0x06000A59 RID: 2649 RVA: 0x0005488A File Offset: 0x0005388A
			private void DestorySimpleLandscapeTool(bool applyChanges)
			{
				if (this.simpleLandscapeToolID != -1)
				{
					base.MapEditorLandscapeToolContainer.RemoveLandscapeTool(this.simpleLandscapeToolID);
					this.simpleLandscapeToolID = -1;
				}
				base.EndTransaction(applyChanges);
			}

			// Token: 0x06000A5A RID: 2650 RVA: 0x000548B8 File Offset: 0x000538B8
			private void UpdateClipboardButtons()
			{
				base.SetMethodParams("copy", true, base.LandscapeState.InvokeClipboardOperationAvailable(ClipboardOperationType.Copy), false, false);
				base.SetMethodParams("copy_special", true, base.LandscapeState.InvokeClipboardOperationAvailable(ClipboardOperationType.CopySpecial), false, false);
				base.SetMethodParams("cut", true, base.LandscapeState.InvokeClipboardOperationAvailable(ClipboardOperationType.Cut), false, false);
				base.SetMethodParams("paste", true, base.LandscapeState.InvokeClipboardOperationAvailable(ClipboardOperationType.Paste), false, false);
				base.SetMethodParams("paste_special", true, base.LandscapeState.InvokeClipboardOperationAvailable(ClipboardOperationType.PasteSpecial), false, false);
			}

			// Token: 0x06000A5B RID: 2651 RVA: 0x00054948 File Offset: 0x00053948
			private void OnEnterState(IState state)
			{
				base.LandscapeToolContext.Update(true);
				base.LandscapeRegion.Visible = true;
				this.DestorySimpleLandscapeTool(true);
				this.UpdateClipboardButtons();
				base.Container.BindState(this.hillMakerToolSubstate);
				base.Container.BindState(this.roadToolSubstate);
			}

			// Token: 0x06000A5C RID: 2652 RVA: 0x0005499C File Offset: 0x0005399C
			private void OnLeaveState(IState state)
			{
				base.Container.UnbindState(this.hillMakerToolSubstate);
				base.Container.UnbindState(this.roadToolSubstate);
				base.LandscapeRegion.Visible = false;
				this.DestorySimpleLandscapeTool(true);
			}

			// Token: 0x06000A5D RID: 2653 RVA: 0x000549D4 File Offset: 0x000539D4
			private void OnMouseMove(MethodArgs methodArgs)
			{
				MouseEventArgs mouseEventArgs = methodArgs.eventArgs as MouseEventArgs;
				if (mouseEventArgs != null && !KeyStatus.RightMouse)
				{
					base.LandscapeToolContext.Update(mouseEventArgs, false);
					this.UpdateLandscapeRegionCenter();
					if (this.simpleLandscapeToolID != -1)
					{
						this.ApplySimpleLandscapeTool();
					}
				}
			}

			// Token: 0x06000A5E RID: 2654 RVA: 0x00054A1C File Offset: 0x00053A1C
			private void OnMouseDown(MethodArgs methodArgs)
			{
				MouseEventArgs mouseEventArgs = methodArgs.eventArgs as MouseEventArgs;
				if (mouseEventArgs != null && mouseEventArgs.Button != MouseButtons.Right)
				{
					base.LandscapeToolContext.Update(mouseEventArgs, this.simpleLandscapeToolID == -1);
					KeyStatus.ClearCache();
					if (base.LandscapeParams.LandscapeRegionParams.Type == LandscapeRegionType.Polygon)
					{
						Vec3 point = base.LandscapeToolContext.LandscapeToolContextPosition.CollisionMapPosition.Vec3;
						if (base.LandscapeState.HitTestEditPolygonSubtate(base.Label, ref point))
						{
							return;
						}
					}
					else if (base.LandscapeParams.LandscapeRegionParams.Type == LandscapeRegionType.Stripe)
					{
						Vec3 point2 = base.LandscapeToolContext.LandscapeToolContextPosition.CollisionMapPosition.Vec3;
						if (base.LandscapeState.HitTestEditStripeSubtate(base.Label, ref point2))
						{
							return;
						}
					}
					this.UpdateLandscapeRegionCenter();
					if ((KeyStatus.LeftMouse || KeyStatus.MiddleMouse) && this.simpleLandscapeToolID == -1)
					{
						this.leftMouse = KeyStatus.LeftMouse;
						this.CreateSimpleLandscapeTool();
						this.ApplySimpleLandscapeTool();
					}
				}
			}

			// Token: 0x06000A5F RID: 2655 RVA: 0x00054B20 File Offset: 0x00053B20
			private void OnMouseUp(MethodArgs methodArgs)
			{
				MouseEventArgs mouseEventArgs = methodArgs.eventArgs as MouseEventArgs;
				if (mouseEventArgs != null && mouseEventArgs.Button != MouseButtons.Right)
				{
					base.LandscapeToolContext.Update(mouseEventArgs, false);
					KeyStatus.ClearCache();
					this.UpdateLandscapeRegionCenter();
					if (((!KeyStatus.LeftMouse && this.leftMouse) || (!KeyStatus.MiddleMouse && !this.leftMouse)) && this.simpleLandscapeToolID != -1)
					{
						this.DestorySimpleLandscapeTool(true);
					}
				}
			}

			// Token: 0x06000A60 RID: 2656 RVA: 0x00054B90 File Offset: 0x00053B90
			private void OnProperties(MethodArgs methodArgs)
			{
				if (this.simpleLandscapeToolID != -1)
				{
					this.DestorySimpleLandscapeTool(false);
					return;
				}
				if (base.LandscapeParams.LandscapeRegionParams.Type == LandscapeRegionType.Polygon)
				{
					base.LandscapeState.ActivateEditPolygonSubtate(base.Label);
					return;
				}
				if (base.LandscapeParams.LandscapeRegionParams.Type == LandscapeRegionType.Stripe)
				{
					base.LandscapeState.ActivateEditStripeSubtate(base.Label);
				}
			}

			// Token: 0x06000A61 RID: 2657 RVA: 0x00054BF8 File Offset: 0x00053BF8
			private void OnBreak(MethodArgs methodArgs)
			{
				if (this.simpleLandscapeToolID != -1)
				{
					this.DestorySimpleLandscapeTool(false);
					return;
				}
				if (base.LandscapeParams.LandscapeRegionParams.Type == LandscapeRegionType.Polygon)
				{
					base.LandscapeState.ActivateAddPolygonSubstate(base.Label);
					return;
				}
				if (base.LandscapeParams.LandscapeRegionParams.Type == LandscapeRegionType.Stripe)
				{
					base.LandscapeState.ActivateAddStripeSubstate(base.Label);
				}
			}

			// Token: 0x06000A62 RID: 2658 RVA: 0x00054C60 File Offset: 0x00053C60
			private void OnMouseWheel(MethodArgs methodArgs)
			{
				MouseEventArgs mouseEventArgs = methodArgs.eventArgs as MouseEventArgs;
				KeyStatus.ClearCache();
				if (mouseEventArgs != null && KeyStatus.Shift)
				{
					if (mouseEventArgs.Delta > 0)
					{
						base.LandscapeParams.LandscapeRegionParams.Size += 2;
						return;
					}
					base.LandscapeParams.LandscapeRegionParams.Size -= 2;
				}
			}

			// Token: 0x06000A63 RID: 2659 RVA: 0x00054CC4 File Offset: 0x00053CC4
			private void OnMouseDoubleClick(MethodArgs methodArgs)
			{
				if (this.simpleLandscapeToolID != -1)
				{
					this.DestorySimpleLandscapeTool(false);
					return;
				}
				if (base.LandscapeParams.LandscapeRegionParams.Type == LandscapeRegionType.Polygon)
				{
					base.LandscapeState.ActivateEditPolygonSubtate(base.Label);
					return;
				}
				if (base.LandscapeParams.LandscapeRegionParams.Type == LandscapeRegionType.Stripe)
				{
					base.LandscapeState.ActivateEditStripeSubtate(base.Label);
				}
			}

			// Token: 0x06000A64 RID: 2660 RVA: 0x00054D2B File Offset: 0x00053D2B
			private void OnCopy(MethodArgs methodArgs)
			{
				if (this.simpleLandscapeToolID == -1)
				{
					base.LandscapeState.InvokeClipboardOperation(ClipboardOperationType.Copy, base.Label);
					this.UpdateClipboardButtons();
				}
			}

			// Token: 0x06000A65 RID: 2661 RVA: 0x00054D4E File Offset: 0x00053D4E
			private void OnCopySpecial(MethodArgs methodArgs)
			{
				if (this.simpleLandscapeToolID == -1)
				{
					base.LandscapeState.InvokeClipboardOperation(ClipboardOperationType.CopySpecial, base.Label);
					this.UpdateClipboardButtons();
				}
			}

			// Token: 0x06000A66 RID: 2662 RVA: 0x00054D71 File Offset: 0x00053D71
			private void OnCut(MethodArgs methodArgs)
			{
				if (this.simpleLandscapeToolID == -1)
				{
					base.LandscapeState.InvokeClipboardOperation(ClipboardOperationType.Cut, base.Label);
					this.UpdateClipboardButtons();
				}
			}

			// Token: 0x06000A67 RID: 2663 RVA: 0x00054D94 File Offset: 0x00053D94
			private void OnPaste(MethodArgs methodArgs)
			{
				if (this.simpleLandscapeToolID == -1)
				{
					base.LandscapeState.InvokeClipboardOperation(ClipboardOperationType.Paste, base.Label);
					this.UpdateClipboardButtons();
				}
			}

			// Token: 0x06000A68 RID: 2664 RVA: 0x00054DB7 File Offset: 0x00053DB7
			private void OnPasteSpecial(MethodArgs methodArgs)
			{
				if (this.simpleLandscapeToolID == -1)
				{
					base.LandscapeState.InvokeClipboardOperation(ClipboardOperationType.PasteSpecial, base.Label);
					this.UpdateClipboardButtons();
				}
			}

			// Token: 0x06000A69 RID: 2665 RVA: 0x00054DDA File Offset: 0x00053DDA
			private void OnClearLandscapeToolsIntermediateData(MethodArgs methodArgs)
			{
				MapEditorLandscapeToolContainer.ClearLandscapeToolsIntermediateData(base.LandscapeToolContext);
			}

			// Token: 0x17000224 RID: 548
			// (get) Token: 0x06000A6A RID: 2666 RVA: 0x00054DE7 File Offset: 0x00053DE7
			private bool Active
			{
				get
				{
					return base.LandscapeState != null && base.LandscapeState.IsSubstateActive(base.Label);
				}
			}

			// Token: 0x06000A6B RID: 2667 RVA: 0x00054E04 File Offset: 0x00053E04
			public SimpleLandscapeToolSubstate(LandscapeState _landscapeState, LandscapeToolContext _landscapeToolContext, ILandscapeParams _landscapeParams, LandscapeRegion _landscapeRegion, MapEditorLandscapeToolContainer _mapEditorLandscapeToolContainer, OperationContainer _operationContainer) : base("SimpleLandscapeToolSubstate", _landscapeState, _landscapeToolContext, _landscapeParams, _landscapeRegion, _mapEditorLandscapeToolContainer, _operationContainer)
			{
				base.AddMethod("mouse_move", new Method(this.OnMouseMove));
				base.AddMethod("mouse_down", new Method(this.OnMouseDown));
				base.AddMethod("mouse_up", new Method(this.OnMouseUp));
				base.AddMethod("properties", new Method(this.OnProperties));
				base.AddMethod("break", new Method(this.OnBreak));
				base.AddMethod("mouse_wheel", new Method(this.OnMouseWheel));
				base.AddMethod("mouse_double_click", new Method(this.OnMouseDoubleClick));
				base.AddMethod("copy", new Method(this.OnCopy));
				base.AddMethod("copy_special", new Method(this.OnCopySpecial));
				base.AddMethod("cut", new Method(this.OnCut));
				base.AddMethod("paste", new Method(this.OnPaste));
				base.AddMethod("paste_special", new Method(this.OnPasteSpecial));
				base.AddMethod("clear_lti_data", new Method(this.OnClearLandscapeToolsIntermediateData));
				this.EnterState = (State.ActivateEvent)Delegate.Combine(this.EnterState, new State.ActivateEvent(this.OnEnterState));
				this.LeaveState = (State.ActivateEvent)Delegate.Combine(this.LeaveState, new State.ActivateEvent(this.OnLeaveState));
				this.hillMakerToolSubstate = new LandscapeState.SimpleLandscapeToolSubstate.HillLandscapeToolSubstate(this);
				this.roadToolSubstate = new LandscapeState.SimpleLandscapeToolSubstate.RoadLandscapeToolSubstate(this);
			}

			// Token: 0x06000A6C RID: 2668 RVA: 0x00054FB4 File Offset: 0x00053FB4
			public override void Destroy()
			{
				this.hillMakerToolSubstate.Destroy();
				this.hillMakerToolSubstate = null;
				this.roadToolSubstate.Destroy();
				this.roadToolSubstate = null;
				this.EnterState = (State.ActivateEvent)Delegate.Remove(this.EnterState, new State.ActivateEvent(this.OnEnterState));
				this.LeaveState = (State.ActivateEvent)Delegate.Remove(this.LeaveState, new State.ActivateEvent(this.OnLeaveState));
				this.DestorySimpleLandscapeTool(true);
				base.Destroy();
			}

			// Token: 0x06000A6D RID: 2669 RVA: 0x00055036 File Offset: 0x00054036
			public void InvokeUpdateClipboardButtons()
			{
				this.UpdateClipboardButtons();
			}

			// Token: 0x0400081B RID: 2075
			private bool leftMouse;

			// Token: 0x0400081C RID: 2076
			private int simpleLandscapeToolID = -1;

			// Token: 0x0400081D RID: 2077
			private LandscapeState.SimpleLandscapeToolSubstate.HillLandscapeToolSubstate hillMakerToolSubstate;

			// Token: 0x0400081E RID: 2078
			private LandscapeState.SimpleLandscapeToolSubstate.RoadLandscapeToolSubstate roadToolSubstate;

			// Token: 0x020000CC RID: 204
			internal class HillLandscapeToolSubstate : State
			{
				// Token: 0x06000A6E RID: 2670 RVA: 0x0005503E File Offset: 0x0005403E
				private void OnApplyParamsChanged(HillParams hillParams)
				{
					if (this.hillLandscapeTool != null && this.simpleLandscapeToolSubstate.Active)
					{
						this.ApplyHillLandscapeTool(HillLandscapeTool.Operation.Apply);
					}
				}

				// Token: 0x06000A6F RID: 2671 RVA: 0x0005505C File Offset: 0x0005405C
				private void CreateHillLandscapeTool()
				{
					if (this.simpleLandscapeToolSubstate != null)
					{
						for (int index = 0; index < this.simpleLandscapeToolSubstate.LandscapeParams.LandscapeToolParamsContainer.Count; index++)
						{
							ILandscapeToolParams leftLandscapeToolParams = this.simpleLandscapeToolSubstate.LandscapeParams.LandscapeToolParamsContainer.Get(false, index);
							if (leftLandscapeToolParams != null && leftLandscapeToolParams.LandscapeToolType == LandscapeToolType.Hill)
							{
								this.hillLandscapeToolParams = (leftLandscapeToolParams as HillLandscapeToolParams);
								if (this.hillLandscapeToolParams != null)
								{
									break;
								}
							}
						}
						if (this.hillLandscapeToolParams != null)
						{
							this.hillLandscapeToolParams.TerrainHillParams.ApplyParamsChanged += this.OnApplyParamsChanged;
							this.hillLandscapeToolParams.BottomHillParams.ApplyParamsChanged += this.OnApplyParamsChanged;
							int landscapeToolID = this.simpleLandscapeToolSubstate.MapEditorLandscapeToolContainer.AddHillLandscapeTool(this.simpleLandscapeToolSubstate.LandscapeToolContext, this.simpleLandscapeToolSubstate.LandscapeRegion, this.hillLandscapeToolParams);
							ILandscapeTool landscapeTool;
							this.simpleLandscapeToolSubstate.MapEditorLandscapeToolContainer.TryGetLandscapeTool(landscapeToolID, out landscapeTool);
							if (landscapeTool != null)
							{
								this.hillLandscapeTool = (landscapeTool as HillLandscapeTool);
							}
						}
					}
				}

				// Token: 0x06000A70 RID: 2672 RVA: 0x00055160 File Offset: 0x00054160
				private void ApplyHillLandscapeTool(HillLandscapeTool.Operation nextOperation)
				{
					if (this.hillLandscapeToolParams != null && this.hillLandscapeTool != null)
					{
						if (nextOperation == HillLandscapeTool.Operation.Init)
						{
							int seed = Kernel.GetTickCount();
							this.hillLandscapeToolParams.TerrainHillParams.Seed = seed;
							this.hillLandscapeToolParams.BottomHillParams.Seed = seed + 1;
						}
						this.hillLandscapeTool.NextOperation = nextOperation;
						this.simpleLandscapeToolSubstate.BeginTransaction();
						this.simpleLandscapeToolSubstate.MapEditorLandscapeToolContainer.ApplyLandscapeTool(this.hillLandscapeTool);
						this.simpleLandscapeToolSubstate.EndTransaction(this.hillLandscapeTool.SomethingChanged);
						if (this.hillLandscapeTool.SomethingChanged)
						{
							this.simpleLandscapeToolSubstate.LandscapeRegion.Update();
							base.Container.Invoke("_minimap_repaint", default(MethodArgs));
						}
					}
				}

				// Token: 0x06000A71 RID: 2673 RVA: 0x0005522C File Offset: 0x0005422C
				private void DestroyHillLandscapeTool()
				{
					if (this.hillLandscapeToolParams != null)
					{
						this.hillLandscapeToolParams.TerrainHillParams.ApplyParamsChanged -= this.OnApplyParamsChanged;
						this.hillLandscapeToolParams.BottomHillParams.ApplyParamsChanged -= this.OnApplyParamsChanged;
					}
					if (this.hillLandscapeTool != null)
					{
						this.simpleLandscapeToolSubstate.MapEditorLandscapeToolContainer.RemoveLandscapeTool(this.hillLandscapeTool);
						this.hillLandscapeTool = null;
					}
				}

				// Token: 0x06000A72 RID: 2674 RVA: 0x0005529F File Offset: 0x0005429F
				private void OnHillUndo(MethodArgs methodArgs)
				{
					if (this.simpleLandscapeToolSubstate.Active)
					{
						this.ApplyHillLandscapeTool(HillLandscapeTool.Operation.Reset);
					}
				}

				// Token: 0x06000A73 RID: 2675 RVA: 0x000552B5 File Offset: 0x000542B5
				private void OnHillApply(MethodArgs methodArgs)
				{
					if (this.simpleLandscapeToolSubstate.Active)
					{
						this.ApplyHillLandscapeTool(HillLandscapeTool.Operation.Complete);
					}
				}

				// Token: 0x06000A74 RID: 2676 RVA: 0x000552CB File Offset: 0x000542CB
				private void OnHillCreate(MethodArgs methodArgs)
				{
					if (this.simpleLandscapeToolSubstate.Active)
					{
						this.ApplyHillLandscapeTool(HillLandscapeTool.Operation.Init);
					}
				}

				// Token: 0x06000A75 RID: 2677 RVA: 0x000552E4 File Offset: 0x000542E4
				public HillLandscapeToolSubstate(LandscapeState.SimpleLandscapeToolSubstate _simpleLandscapeToolSubstate) : base("HillLandscapeToolSubstate")
				{
					this.simpleLandscapeToolSubstate = _simpleLandscapeToolSubstate;
					base.AddMethod("_hill_undo", new Method(this.OnHillUndo));
					base.AddMethod("_hill_apply", new Method(this.OnHillApply));
					base.AddMethod("_hill_create", new Method(this.OnHillCreate));
					this.CreateHillLandscapeTool();
				}

				// Token: 0x06000A76 RID: 2678 RVA: 0x0005534E File Offset: 0x0005434E
				public void Destroy()
				{
					this.DestroyHillLandscapeTool();
					this.simpleLandscapeToolSubstate = null;
				}

				// Token: 0x0400081F RID: 2079
				private LandscapeState.SimpleLandscapeToolSubstate simpleLandscapeToolSubstate;

				// Token: 0x04000820 RID: 2080
				private HillLandscapeTool hillLandscapeTool;

				// Token: 0x04000821 RID: 2081
				private HillLandscapeToolParams hillLandscapeToolParams;
			}

			// Token: 0x020000CD RID: 205
			internal class RoadLandscapeToolSubstate : State
			{
				// Token: 0x06000A77 RID: 2679 RVA: 0x0005535D File Offset: 0x0005435D
				private void OnLandscapeParamsChanged(RoadLandscapeToolParams _roadLandscapeToolParams)
				{
					if (this.roadLandscapeTool != null && this.simpleLandscapeToolSubstate.Active)
					{
						this.ApplyRoadLandscapeTool(RoadLandscapeTool.Operation.Apply);
					}
				}

				// Token: 0x06000A78 RID: 2680 RVA: 0x0005537C File Offset: 0x0005437C
				private void CreateRoadLandscapeTool()
				{
					if (this.simpleLandscapeToolSubstate != null)
					{
						for (int index = 0; index < this.simpleLandscapeToolSubstate.LandscapeParams.LandscapeToolParamsContainer.Count; index++)
						{
							ILandscapeToolParams leftLandscapeToolParams = this.simpleLandscapeToolSubstate.LandscapeParams.LandscapeToolParamsContainer.Get(true, index);
							if (leftLandscapeToolParams != null && leftLandscapeToolParams.LandscapeToolType == LandscapeToolType.Road)
							{
								this.roadLandscapeToolParams = (leftLandscapeToolParams as RoadLandscapeToolParams);
								if (this.roadLandscapeToolParams != null)
								{
									break;
								}
							}
						}
						if (this.roadLandscapeToolParams != null)
						{
							this.roadLandscapeToolParams.Changed += this.OnLandscapeParamsChanged;
							int landscapeToolID = this.simpleLandscapeToolSubstate.MapEditorLandscapeToolContainer.AddRoadLandscapeTool(this.simpleLandscapeToolSubstate.LandscapeToolContext, this.simpleLandscapeToolSubstate.LandscapeRegion, this.roadLandscapeToolParams);
							ILandscapeTool landscapeTool;
							this.simpleLandscapeToolSubstate.MapEditorLandscapeToolContainer.TryGetLandscapeTool(landscapeToolID, out landscapeTool);
							if (landscapeTool != null)
							{
								this.roadLandscapeTool = (landscapeTool as RoadLandscapeTool);
							}
						}
					}
				}

				// Token: 0x06000A79 RID: 2681 RVA: 0x0005545C File Offset: 0x0005445C
				private void ApplyRoadLandscapeTool(RoadLandscapeTool.Operation nextOperation)
				{
					if (this.roadLandscapeToolParams != null && this.roadLandscapeTool != null)
					{
						this.roadLandscapeToolParams.Seed = Kernel.GetTickCount();
						this.roadLandscapeTool.NextOperation = nextOperation;
						this.simpleLandscapeToolSubstate.BeginTransaction();
						this.simpleLandscapeToolSubstate.MapEditorLandscapeToolContainer.ApplyLandscapeTool(this.roadLandscapeTool);
						this.simpleLandscapeToolSubstate.EndTransaction(this.roadLandscapeTool.SomethingChanged);
						if (this.roadLandscapeTool.SomethingChanged)
						{
							this.simpleLandscapeToolSubstate.LandscapeRegion.Update();
							base.Container.Invoke("_minimap_repaint", default(MethodArgs));
						}
					}
				}

				// Token: 0x06000A7A RID: 2682 RVA: 0x0005550C File Offset: 0x0005450C
				private void DestroyRoadLandscapeTool()
				{
					if (this.roadLandscapeToolParams != null)
					{
						this.roadLandscapeToolParams.Changed -= this.OnLandscapeParamsChanged;
					}
					if (this.roadLandscapeTool != null)
					{
						this.simpleLandscapeToolSubstate.MapEditorLandscapeToolContainer.RemoveLandscapeTool(this.roadLandscapeTool);
						this.roadLandscapeTool = null;
					}
				}

				// Token: 0x06000A7B RID: 2683 RVA: 0x0005555E File Offset: 0x0005455E
				private void OnRoadUndo(MethodArgs methodArgs)
				{
					if (this.simpleLandscapeToolSubstate.Active)
					{
						this.ApplyRoadLandscapeTool(RoadLandscapeTool.Operation.Reset);
					}
				}

				// Token: 0x06000A7C RID: 2684 RVA: 0x00055574 File Offset: 0x00054574
				private void OnRoadApply(MethodArgs methodArgs)
				{
					if (this.simpleLandscapeToolSubstate.Active)
					{
						this.ApplyRoadLandscapeTool(RoadLandscapeTool.Operation.Complete);
					}
				}

				// Token: 0x06000A7D RID: 2685 RVA: 0x0005558A File Offset: 0x0005458A
				private void OnRoadCreate(MethodArgs methodArgs)
				{
					if (this.simpleLandscapeToolSubstate.Active)
					{
						this.ApplyRoadLandscapeTool(RoadLandscapeTool.Operation.Init);
					}
				}

				// Token: 0x06000A7E RID: 2686 RVA: 0x000555A0 File Offset: 0x000545A0
				public RoadLandscapeToolSubstate(LandscapeState.SimpleLandscapeToolSubstate _simpleLandscapeToolSubstate) : base("RoadLandscapeToolSubstate")
				{
					this.simpleLandscapeToolSubstate = _simpleLandscapeToolSubstate;
					base.AddMethod("_road_undo", new Method(this.OnRoadUndo));
					base.AddMethod("_road_apply", new Method(this.OnRoadApply));
					base.AddMethod("_road_create", new Method(this.OnRoadCreate));
					this.CreateRoadLandscapeTool();
				}

				// Token: 0x06000A7F RID: 2687 RVA: 0x0005560A File Offset: 0x0005460A
				public void Destroy()
				{
					this.DestroyRoadLandscapeTool();
					this.simpleLandscapeToolSubstate = null;
				}

				// Token: 0x04000822 RID: 2082
				private LandscapeState.SimpleLandscapeToolSubstate simpleLandscapeToolSubstate;

				// Token: 0x04000823 RID: 2083
				private RoadLandscapeTool roadLandscapeTool;

				// Token: 0x04000824 RID: 2084
				private RoadLandscapeToolParams roadLandscapeToolParams;
			}
		}

		// Token: 0x020000CE RID: 206
		internal class ClipboardToolSubstate : LandscapeState.LandscapeToolSubstate
		{
			// Token: 0x06000A80 RID: 2688 RVA: 0x0005561C File Offset: 0x0005461C
			private void CreateLandscapeTool()
			{
				for (int index = 0; index < base.LandscapeParams.LandscapeToolParamsContainer.Count; index++)
				{
					ILandscapeToolParams leftLandscapeToolParams = base.LandscapeParams.LandscapeToolParamsContainer.Get(true, index);
					if (leftLandscapeToolParams != null && leftLandscapeToolParams.LandscapeToolType == LandscapeToolType.Clipboard)
					{
						this.clipboardLandscapeToolParams = (leftLandscapeToolParams as ClipboardLandscapeToolParams);
						if (this.clipboardLandscapeToolParams != null)
						{
							this.clipboardSubstateParams.Index = index;
							break;
						}
					}
				}
				if (this.clipboardLandscapeToolParams != null)
				{
					this.clipboardLandscapeToolParams.FileNameChanged += this.OnParamsFileNameChanged;
					this.clipboardLandscapeToolParams.StrengthSmoothParams.SmoothChanged += this.OnParamsSmoothChanged;
					this.clipboardLandscapeToolParams.StrengthSmoothParams.StrengthChanged += this.OnParamsStrengthChanged;
					this.clipboardLandscapeToolParams.CopyTilesChanged += this.OnParamsCopyTilesChanged;
					this.clipboardLandscapeToolParams.CopyHeightsChanged += this.OnParamsCopyHeightsChanged;
					this.clipboardLandscapeToolParams.CopyHeightTypeChanged += this.OnParamsCopyHeightTypeChanged;
					this.clipboardLandscapeToolParams.PreciseToolChanged += this.OnParamsPreciseToolChanged;
					this.clipboardLandscapeToolParams.FlipHorisontalChanged += this.OnParamsFlipHorisontalChanged;
					this.clipboardLandscapeToolParams.FlipVerticalChanged += this.OnParamsFlipVerticalChanged;
					int landscapeToolID = base.MapEditorLandscapeToolContainer.AddClipboardLandscapeTool(base.LandscapeToolContext, base.LandscapeRegion, this.clipboardLandscapeToolParams);
					ILandscapeTool landscapeTool;
					base.MapEditorLandscapeToolContainer.TryGetLandscapeTool(landscapeToolID, out landscapeTool);
					if (landscapeTool != null)
					{
						this.clipboardLandscapeTool = (landscapeTool as ClipboardLandscapeTool);
					}
				}
			}

			// Token: 0x06000A81 RID: 2689 RVA: 0x000557A8 File Offset: 0x000547A8
			private void DestroyLandscapeTool()
			{
				if (this.clipboardLandscapeToolParams != null)
				{
					this.clipboardLandscapeToolParams.FileNameChanged -= this.OnParamsFileNameChanged;
					this.clipboardLandscapeToolParams.StrengthSmoothParams.SmoothChanged -= this.OnParamsSmoothChanged;
					this.clipboardLandscapeToolParams.StrengthSmoothParams.StrengthChanged -= this.OnParamsStrengthChanged;
					this.clipboardLandscapeToolParams.CopyTilesChanged -= this.OnParamsCopyTilesChanged;
					this.clipboardLandscapeToolParams.CopyHeightsChanged -= this.OnParamsCopyHeightsChanged;
					this.clipboardLandscapeToolParams.CopyHeightTypeChanged -= this.OnParamsCopyHeightTypeChanged;
					this.clipboardLandscapeToolParams.PreciseToolChanged -= this.OnParamsPreciseToolChanged;
					this.clipboardLandscapeToolParams.FlipHorisontalChanged -= this.OnParamsFlipHorisontalChanged;
					this.clipboardLandscapeToolParams.FlipVerticalChanged -= this.OnParamsFlipVerticalChanged;
				}
				if (this.clipboardLandscapeTool != null)
				{
					base.MapEditorLandscapeToolContainer.RemoveLandscapeTool(this.clipboardLandscapeTool);
					this.clipboardLandscapeTool = null;
				}
			}

			// Token: 0x06000A82 RID: 2690 RVA: 0x000558BC File Offset: 0x000548BC
			private void CreateSelector(StateContainer _stateContainer, ICollisionMap _collisionMap, EditorScene _editorScene, string _selectorDataFileName)
			{
				this.selector = new LandscapeClipboardSelector(_stateContainer, _collisionMap, _editorScene, _selectorDataFileName);
				this.selector.PositionChanged += this.OnSelectorPositionChanged;
				this.selector.RotationChanged += this.OnSelectorRotationChanged;
				this.selector.ScaleChanged += this.OnSelectorScaleChanged;
			}

			// Token: 0x06000A83 RID: 2691 RVA: 0x00055920 File Offset: 0x00054920
			private void DestroySelector()
			{
				if (this.selector != null)
				{
					this.selector.Unbind();
					this.selector.PositionChanged -= this.OnSelectorPositionChanged;
					this.selector.RotationChanged -= this.OnSelectorRotationChanged;
					this.selector.ScaleChanged -= this.OnSelectorScaleChanged;
					this.selector.Destroy();
					this.selector = null;
				}
			}

			// Token: 0x06000A84 RID: 2692 RVA: 0x00055998 File Offset: 0x00054998
			private void UpdateSelectorHeight()
			{
				if (this.selector != null)
				{
					this.selector.PositionChanged -= this.OnSelectorPositionChanged;
					Position position = this.selector.Position;
					this.selector.Position = new Position(position.X, position.Y, this.clipboardLandscapeTool.GetAverageHeight() + this.selector.AdditionalHeight);
					this.selector.PositionChanged += this.OnSelectorPositionChanged;
				}
			}

			// Token: 0x06000A85 RID: 2693 RVA: 0x00055A1C File Offset: 0x00054A1C
			private void OnSelectorPositionChanged(LandscapeClipboardSelector _selector, ref Position olValue, ref Position newValue)
			{
				if (this.selector.Visible)
				{
					this.ContinuePaste(this.clipboardOperation, false);
				}
			}

			// Token: 0x06000A86 RID: 2694 RVA: 0x00055A38 File Offset: 0x00054A38
			private void OnSelectorRotationChanged(LandscapeClipboardSelector _selector, ref Rotation olValue, ref Rotation newValue)
			{
				if (this.selector.Visible)
				{
					this.ContinuePaste(this.clipboardOperation, false);
				}
			}

			// Token: 0x06000A87 RID: 2695 RVA: 0x00055A54 File Offset: 0x00054A54
			private void OnSelectorScaleChanged(LandscapeClipboardSelector _selector, ref Scale olValue, ref Scale newValue)
			{
				if (this.selector.Visible)
				{
					this.ContinuePaste(this.clipboardOperation, true);
				}
			}

			// Token: 0x06000A88 RID: 2696 RVA: 0x00055A70 File Offset: 0x00054A70
			private void OnParamsFileNameChanged(ClipboardLandscapeToolParams _landscapeClipboardToolParams, ref string oldValue, ref string newValue)
			{
				if (this.clipboardLandscapeTool.Load())
				{
					base.LandscapeState.InvokeUpdateClipboardButtons();
					if (this.selector.Visible)
					{
						this.ContinuePaste(this.clipboardOperation, true);
						this.UpdateSelectorHeight();
					}
				}
			}

			// Token: 0x06000A89 RID: 2697 RVA: 0x00055AAA File Offset: 0x00054AAA
			private void OnParamsSmoothChanged(StrengthSmoothParams _strengthSmoothParams, ref double oldValue, ref double newValue)
			{
				if (this.selector.Visible)
				{
					this.ContinuePaste(this.clipboardOperation, true);
					this.UpdateSelectorHeight();
				}
			}

			// Token: 0x06000A8A RID: 2698 RVA: 0x00055ACC File Offset: 0x00054ACC
			private void OnParamsStrengthChanged(StrengthSmoothParams _strengthSmoothParams, ref double oldValue, ref double newValue)
			{
				bool visible = this.selector.Visible;
			}

			// Token: 0x06000A8B RID: 2699 RVA: 0x00055ADA File Offset: 0x00054ADA
			private void OnParamsCopyTilesChanged(ClipboardLandscapeToolParams _landscapeClipboardToolParams, ref bool oldValue, ref bool newValue)
			{
				if (this.selector.Visible)
				{
					this.ContinuePaste(this.clipboardOperation, false);
					this.UpdateSelectorHeight();
				}
			}

			// Token: 0x06000A8C RID: 2700 RVA: 0x00055AFC File Offset: 0x00054AFC
			private void OnParamsCopyHeightsChanged(ClipboardLandscapeToolParams _landscapeClipboardToolParams, ref bool oldValue, ref bool newValue)
			{
				if (this.selector.Visible)
				{
					this.ContinuePaste(this.clipboardOperation, false);
					this.UpdateSelectorHeight();
				}
			}

			// Token: 0x06000A8D RID: 2701 RVA: 0x00055B1E File Offset: 0x00054B1E
			private void OnParamsCopyHeightTypeChanged(ClipboardLandscapeToolParams _landscapeClipboardToolParams, ref LandscapeCopyHeightType oldValue, ref LandscapeCopyHeightType newValue)
			{
				if (this.selector.Visible)
				{
					this.ContinuePaste(this.clipboardOperation, false);
					this.UpdateSelectorHeight();
				}
			}

			// Token: 0x06000A8E RID: 2702 RVA: 0x00055B40 File Offset: 0x00054B40
			private void OnParamsPreciseToolChanged(ClipboardLandscapeToolParams _landscapeClipboardToolParams, ref bool oldValue, ref bool newValue)
			{
				if (this.selector.Visible)
				{
					this.ContinuePaste(this.clipboardOperation, false);
					this.UpdateSelectorHeight();
				}
			}

			// Token: 0x06000A8F RID: 2703 RVA: 0x00055B62 File Offset: 0x00054B62
			private void OnParamsFlipHorisontalChanged(ClipboardLandscapeToolParams _landscapeClipboardToolParams, ref bool oldValue, ref bool newValue)
			{
				if (this.selector.Visible)
				{
					this.ContinuePaste(this.clipboardOperation, false);
					this.UpdateSelectorHeight();
				}
			}

			// Token: 0x06000A90 RID: 2704 RVA: 0x00055B84 File Offset: 0x00054B84
			private void OnParamsFlipVerticalChanged(ClipboardLandscapeToolParams _landscapeClipboardToolParams, ref bool oldValue, ref bool newValue)
			{
				if (this.selector.Visible)
				{
					this.ContinuePaste(this.clipboardOperation, false);
					this.UpdateSelectorHeight();
				}
			}

			// Token: 0x06000A91 RID: 2705 RVA: 0x00055BA8 File Offset: 0x00054BA8
			private void OnMouseMove(MethodArgs methodArgs)
			{
				MouseEventArgs mouseEventArgs = methodArgs.eventArgs as MouseEventArgs;
				if (mouseEventArgs != null && !KeyStatus.RightMouse)
				{
					base.LandscapeToolContext.Update(mouseEventArgs, false);
					if (KeyStatus.LeftMouse && this.selector.Visible && this.selector.Picked)
					{
						this.selector.ProcessPick(mouseEventArgs.X, mouseEventArgs.Y);
					}
				}
			}

			// Token: 0x06000A92 RID: 2706 RVA: 0x00055C14 File Offset: 0x00054C14
			private void OnMouseDown(MethodArgs methodArgs)
			{
				MouseEventArgs mouseEventArgs = methodArgs.eventArgs as MouseEventArgs;
				if (mouseEventArgs != null && mouseEventArgs.Button != MouseButtons.Right)
				{
					base.LandscapeToolContext.Update(mouseEventArgs, false);
					KeyStatus.ClearCache();
					if (KeyStatus.LeftMouse && this.selector.Visible)
					{
						this.selector.Pick(mouseEventArgs.X, mouseEventArgs.Y);
					}
				}
			}

			// Token: 0x06000A93 RID: 2707 RVA: 0x00055C7C File Offset: 0x00054C7C
			private void OnMouseUp(MethodArgs methodArgs)
			{
				MouseEventArgs mouseEventArgs = methodArgs.eventArgs as MouseEventArgs;
				if (mouseEventArgs != null && mouseEventArgs.Button != MouseButtons.Right)
				{
					base.LandscapeToolContext.Update(mouseEventArgs, false);
				}
				if (this.selector.Visible)
				{
					this.selector.ClearPick();
					this.UpdateSelectorHeight();
				}
			}

			// Token: 0x06000A94 RID: 2708 RVA: 0x00055CD1 File Offset: 0x00054CD1
			private void OnMouseDoubleClick(MethodArgs methodArgs)
			{
				this.applyChanges = true;
				base.LandscapeState.multiState.ActiveStateLabel = this.previousStateLabel;
			}

			// Token: 0x06000A95 RID: 2709 RVA: 0x00055CF0 File Offset: 0x00054CF0
			private void OnProperties(MethodArgs methodArgs)
			{
				this.applyChanges = true;
				base.LandscapeState.multiState.ActiveStateLabel = this.previousStateLabel;
			}

			// Token: 0x06000A96 RID: 2710 RVA: 0x00055D0F File Offset: 0x00054D0F
			private void OnBreak(MethodArgs methodArgs)
			{
				this.applyChanges = false;
				base.LandscapeState.multiState.ActiveStateLabel = this.previousStateLabel;
			}

			// Token: 0x06000A97 RID: 2711 RVA: 0x00055D30 File Offset: 0x00054D30
			private void OnAltitudeReset(MethodArgs methodArgs)
			{
				double additionalHeight = this.selector.AdditionalHeight;
				this.selector.AdditionalHeight = 0.0;
				this.selector.Position = new Position(this.selector.Position.X, this.selector.Position.Y, this.selector.Position.Z - additionalHeight);
			}

			// Token: 0x06000A98 RID: 2712 RVA: 0x00055DA8 File Offset: 0x00054DA8
			private void OnRotationReset(MethodArgs methodArgs)
			{
				this.selector.Rotation = MapObjectCreationInfo.DefaultRotation;
			}

			// Token: 0x06000A99 RID: 2713 RVA: 0x00055DBA File Offset: 0x00054DBA
			private void OnScaleReset(MethodArgs methodArgs)
			{
				this.selector.Scale = MapObjectCreationInfo.DefaultScale;
			}

			// Token: 0x06000A9A RID: 2714 RVA: 0x00055DCC File Offset: 0x00054DCC
			private bool Copy(ClipboardOperationType _clipboardOperation, Form parentForm)
			{
				if (this.clipboardLandscapeTool != null)
				{
					Tools.Geometry.Point position = base.LandscapeToolContext.LandscapeToolContextPosition.TerrainPosition;
					return this.clipboardLandscapeTool.Copy(ref position, _clipboardOperation, parentForm, out this.regionPosition);
				}
				return false;
			}

			// Token: 0x06000A9B RID: 2715 RVA: 0x00055E0C File Offset: 0x00054E0C
			private bool BeginPaste(ClipboardOperationType _clipboardOperation)
			{
				if (this.clipboardLandscapeTool != null)
				{
					Position position = this.selector.Position;
					Rotation rotation = this.selector.Rotation;
					Scale scale = this.selector.Scale;
					if (this.clipboardLandscapeTool.BeginPaste(ref position, ref rotation, ref scale, this.selector.AdditionalHeight, _clipboardOperation))
					{
						base.BeginTransaction();
						base.MapEditorLandscapeToolContainer.ApplyLandscapeTool(this.clipboardLandscapeTool);
						return true;
					}
				}
				return false;
			}

			// Token: 0x06000A9C RID: 2716 RVA: 0x00055E80 File Offset: 0x00054E80
			private void ContinuePaste(ClipboardOperationType _clipboardOperation, bool setRegionParams)
			{
				if (this.clipboardLandscapeTool != null)
				{
					Position position = this.selector.Position;
					Rotation rotation = this.selector.Rotation;
					Scale scale = this.selector.Scale;
					this.clipboardLandscapeTool.ContinuePaste(ref position, ref rotation, ref scale, this.selector.AdditionalHeight, _clipboardOperation, setRegionParams);
					base.MapEditorLandscapeToolContainer.ApplyLandscapeTool(this.clipboardLandscapeTool);
				}
			}

			// Token: 0x06000A9D RID: 2717 RVA: 0x00055EEC File Offset: 0x00054EEC
			private void FinishPaste(ClipboardOperationType _clipboardOperation, bool _applyChanges)
			{
				if (this.clipboardLandscapeTool != null)
				{
					Position position = this.selector.Position;
					Rotation rotation = this.selector.Rotation;
					Scale scale = this.selector.Scale;
					this.clipboardLandscapeTool.FinishPaste(ref position, ref rotation, ref scale, this.selector.AdditionalHeight, _clipboardOperation, _applyChanges);
					base.EndTransaction(_applyChanges);
				}
			}

			// Token: 0x06000A9E RID: 2718 RVA: 0x00055F4C File Offset: 0x00054F4C
			private bool Available(ClipboardOperationType _clipboardOperation)
			{
				if (this.clipboardLandscapeTool != null)
				{
					if (_clipboardOperation == ClipboardOperationType.Copy || _clipboardOperation == ClipboardOperationType.CopySpecial)
					{
						return this.clipboardLandscapeTool.CopyAvailable(_clipboardOperation);
					}
					if (_clipboardOperation == ClipboardOperationType.Cut)
					{
						return this.clipboardLandscapeTool.CopyAvailable(_clipboardOperation);
					}
					if (_clipboardOperation == ClipboardOperationType.Paste || _clipboardOperation == ClipboardOperationType.PasteSpecial)
					{
						return this.clipboardLandscapeTool.PasteAvailable(_clipboardOperation);
					}
				}
				return false;
			}

			// Token: 0x06000A9F RID: 2719 RVA: 0x00055F9C File Offset: 0x00054F9C
			private void OnEnterState(IState state)
			{
				if (this.selector != null)
				{
					this.selector.Bind();
					base.LandscapeToolContext.Update(true);
					this.applyChanges = false;
					this.selector.Position = base.LandscapeToolContext.LandscapeToolContextPosition.CollisionMapPosition;
					this.selector.AdditionalHeight = 0.0;
					if (this.clipboardOperation == ClipboardOperationType.Cut)
					{
						this.selector.Position = this.regionPosition;
						this.selector.Rotation = Rotation.Empty;
						this.selector.Scale = Scale.Normal;
					}
					if (!this.BeginPaste(this.clipboardOperation))
					{
						base.LandscapeState.multiState.ActiveStateLabel = this.previousStateLabel;
						return;
					}
					if (this.clipboardSubstateParams.Index != -1)
					{
						this.previousSubstateParams.Index = base.LandscapeState.mainState.ActiveSubstate;
						if (base.LandscapeState.mainState.ActiveSubstate % base.LandscapeParams.LandscapeToolParamsContainer.Count != this.clipboardSubstateParams.Index % base.LandscapeParams.LandscapeToolParamsContainer.Count)
						{
							base.LandscapeState.mainState.ActiveSubstate = this.clipboardSubstateParams.Index;
						}
					}
					this.UpdateSelectorHeight();
					this.selector.Visible = true;
				}
			}

			// Token: 0x06000AA0 RID: 2720 RVA: 0x000560F8 File Offset: 0x000550F8
			private void OnLeaveState(IState state)
			{
				if (this.selector != null)
				{
					this.FinishPaste(this.clipboardOperation, this.applyChanges);
					this.applyChanges = false;
					this.selector.Visible = false;
					this.selector.Unbind();
					if (this.previousSubstateParams.Index != -1 && base.LandscapeState.mainState.ActiveSubstate % base.LandscapeParams.LandscapeToolParamsContainer.Count != this.previousSubstateParams.Index % base.LandscapeParams.LandscapeToolParamsContainer.Count)
					{
						base.LandscapeState.mainState.ActiveSubstate = this.previousSubstateParams.Index;
					}
				}
			}

			// Token: 0x06000AA1 RID: 2721 RVA: 0x000561A8 File Offset: 0x000551A8
			public ClipboardToolSubstate(StateContainer _stateContainer, LandscapeState _landscapeState, LandscapeToolContext _landscapeToolContext, ILandscapeParams _landscapeParams, LandscapeRegion _landscapeRegion, MapEditorLandscapeToolContainer _mapEditorLandscapeToolContainer, OperationContainer _operationContainer) : base("ClipboardToolSubstate", _landscapeState, _landscapeToolContext, _landscapeParams, _landscapeRegion, _mapEditorLandscapeToolContainer, _operationContainer)
			{
				base.AddMethod("mouse_move", new Method(this.OnMouseMove));
				base.AddMethod("mouse_down", new Method(this.OnMouseDown));
				base.AddMethod("mouse_up", new Method(this.OnMouseUp));
				base.AddMethod("mouse_double_click", new Method(this.OnMouseDoubleClick));
				base.AddMethod("properties", new Method(this.OnProperties));
				base.AddMethod("break", new Method(this.OnBreak));
				base.AddMethod("altitude_reset", new Method(this.OnAltitudeReset));
				base.AddMethod("rotation_reset", new Method(this.OnRotationReset));
				base.AddMethod("scale_reset", new Method(this.OnScaleReset));
				this.EnterState = (State.ActivateEvent)Delegate.Combine(this.EnterState, new State.ActivateEvent(this.OnEnterState));
				this.LeaveState = (State.ActivateEvent)Delegate.Combine(this.LeaveState, new State.ActivateEvent(this.OnLeaveState));
				this.CreateLandscapeTool();
				this.CreateSelector(_stateContainer, base.LandscapeToolContext.CollisionMap, base.LandscapeToolContext.EditorScene, "MapEditor_LandscapeClipboardSelectorData.xml");
			}

			// Token: 0x06000AA2 RID: 2722 RVA: 0x00056338 File Offset: 0x00055338
			public override void Destroy()
			{
				this.DestroySelector();
				this.DestroyLandscapeTool();
				this.EnterState = (State.ActivateEvent)Delegate.Remove(this.EnterState, new State.ActivateEvent(this.OnEnterState));
				this.LeaveState = (State.ActivateEvent)Delegate.Remove(this.LeaveState, new State.ActivateEvent(this.OnLeaveState));
			}

			// Token: 0x06000AA3 RID: 2723 RVA: 0x00056398 File Offset: 0x00055398
			public void InvokeClipboardOperation(ClipboardOperationType _clipboardOperation, string _previousStateLabel, Form parentForm)
			{
				base.LandscapeToolContext.Update(false);
				if (_clipboardOperation == ClipboardOperationType.Copy || _clipboardOperation == ClipboardOperationType.CopySpecial)
				{
					this.Copy(_clipboardOperation, parentForm);
					return;
				}
				if (_clipboardOperation == ClipboardOperationType.Cut)
				{
					if (this.Copy(_clipboardOperation, parentForm))
					{
						this.previousStateLabel = _previousStateLabel;
						this.clipboardOperation = _clipboardOperation;
						base.LandscapeState.multiState.ActiveStateLabel = base.Label;
						return;
					}
				}
				else if (_clipboardOperation == ClipboardOperationType.Paste || _clipboardOperation == ClipboardOperationType.PasteSpecial)
				{
					this.previousStateLabel = _previousStateLabel;
					this.clipboardOperation = _clipboardOperation;
					base.LandscapeState.multiState.ActiveStateLabel = base.Label;
				}
			}

			// Token: 0x06000AA4 RID: 2724 RVA: 0x00056421 File Offset: 0x00055421
			public bool InvokeClipboardOperationAvailable(ClipboardOperationType _clipboardOperation)
			{
				return this.Available(_clipboardOperation);
			}

			// Token: 0x04000825 RID: 2085
			private LandscapeClipboardSelector selector;

			// Token: 0x04000826 RID: 2086
			private ClipboardLandscapeTool clipboardLandscapeTool;

			// Token: 0x04000827 RID: 2087
			private readonly LandscapeState.ClipboardToolSubstate.SubstateParams clipboardSubstateParams = new LandscapeState.ClipboardToolSubstate.SubstateParams();

			// Token: 0x04000828 RID: 2088
			private readonly LandscapeState.ClipboardToolSubstate.SubstateParams previousSubstateParams = new LandscapeState.ClipboardToolSubstate.SubstateParams();

			// Token: 0x04000829 RID: 2089
			private ClipboardLandscapeToolParams clipboardLandscapeToolParams;

			// Token: 0x0400082A RID: 2090
			private string previousStateLabel = string.Empty;

			// Token: 0x0400082B RID: 2091
			private ClipboardOperationType clipboardOperation = ClipboardOperationType.Paste;

			// Token: 0x0400082C RID: 2092
			private Position regionPosition = Position.Empty;

			// Token: 0x0400082D RID: 2093
			private bool applyChanges;

			// Token: 0x020000CF RID: 207
			internal class SubstateParams
			{
				// Token: 0x17000225 RID: 549
				// (get) Token: 0x06000AA5 RID: 2725 RVA: 0x0005642A File Offset: 0x0005542A
				// (set) Token: 0x06000AA6 RID: 2726 RVA: 0x00056432 File Offset: 0x00055432
				public int Index
				{
					get
					{
						return this.index;
					}
					set
					{
						this.index = value;
					}
				}

				// Token: 0x0400082E RID: 2094
				private int index = -1;
			}
		}
	}
}
