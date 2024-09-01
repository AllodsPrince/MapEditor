using System;
using MapEditor.Map.Containers;
using Operations;
using Tools.MapObjects;

namespace MapEditor.Map.MapObjects
{
	// Token: 0x0200025B RID: 603
	public class PatrolNodeOperationContainer
	{
		// Token: 0x06001CAD RID: 7341 RVA: 0x000B6DFC File Offset: 0x000B5DFC
		private void OnPatrolNodeLabelChanged(MapEditorMapObjectContainer _mapEditorMapObjectContainer, PatrolNode patrolNode, ref string oldValue, ref string newValue)
		{
			if (this.mapEditorMapObjectContainer != null && patrolNode != null && this.operationContainer != null && !patrolNode.Temporary)
			{
				ChangePatrolNodeLabelOperation operation = new ChangePatrolNodeLabelOperation(patrolNode, ref oldValue, ref newValue);
				this.operationContainer.Add(operation);
			}
		}

		// Token: 0x06001CAE RID: 7342 RVA: 0x000B6E3C File Offset: 0x000B5E3C
		private void OnPatrolNodeScriptChanged(MapEditorMapObjectContainer _mapEditorMapObjectContainer, PatrolNode patrolNode, ref string oldValue, ref string newValue)
		{
			if (this.mapEditorMapObjectContainer != null && patrolNode != null && this.operationContainer != null && !patrolNode.Temporary)
			{
				ChangePatrolNodeScriptOperation operation = new ChangePatrolNodeScriptOperation(patrolNode, ref oldValue, ref newValue);
				this.operationContainer.Add(operation);
			}
		}

		// Token: 0x06001CAF RID: 7343 RVA: 0x000B6E7C File Offset: 0x000B5E7C
		private void OnPatrolLinkDataTypeChanged(MapEditorMapObjectContainer _mapEditorMapObjectContainer, PatrolLinkData patrolLinkData, ref PatrolNodeLinkType oldValue, ref PatrolNodeLinkType newValue)
		{
			IMapObject left;
			IMapObject right;
			if (this.mapEditorMapObjectContainer != null && patrolLinkData != null && this.operationContainer != null && this.mapEditorMapObjectContainer.TryGetMapObjects(patrolLinkData, out left, out right) && left != null && right != null && !left.Temporary && !right.Temporary)
			{
				ChangePatrolLinkDataTypeOperation operation = new ChangePatrolLinkDataTypeOperation(patrolLinkData, ref oldValue, ref newValue);
				this.operationContainer.Add(operation);
			}
		}

		// Token: 0x06001CB0 RID: 7344 RVA: 0x000B6EDC File Offset: 0x000B5EDC
		private void OnPatrolLinkDataStartChanged(MapEditorMapObjectContainer _mapEditorMapObjectContainer, PatrolLinkData patrolLinkData, ref string oldValue, ref string newValue)
		{
			IMapObject left;
			IMapObject right;
			if (this.mapEditorMapObjectContainer != null && patrolLinkData != null && this.operationContainer != null && this.mapEditorMapObjectContainer.TryGetMapObjects(patrolLinkData, out left, out right) && left != null && right != null && !left.Temporary && !right.Temporary)
			{
				ChangePatrolLinkDataStartOperation operation = new ChangePatrolLinkDataStartOperation(patrolLinkData, ref oldValue, ref newValue);
				this.operationContainer.Add(operation);
			}
		}

		// Token: 0x06001CB1 RID: 7345 RVA: 0x000B6F3C File Offset: 0x000B5F3C
		private void OnPatrolLinkDataWeightChanged(MapEditorMapObjectContainer _mapEditorMapObjectContainer, PatrolLinkData patrolLinkData, ref int oldValue, ref int newValue)
		{
			IMapObject left;
			IMapObject right;
			if (this.mapEditorMapObjectContainer != null && patrolLinkData != null && this.operationContainer != null && this.mapEditorMapObjectContainer.TryGetMapObjects(patrolLinkData, out left, out right) && left != null && right != null && !left.Temporary && !right.Temporary)
			{
				ChangePatrolLinkDataWeightOperation operation = new ChangePatrolLinkDataWeightOperation(patrolLinkData, ref oldValue, ref newValue);
				this.operationContainer.Add(operation);
			}
		}

		// Token: 0x06001CB2 RID: 7346 RVA: 0x000B6F9A File Offset: 0x000B5F9A
		public PatrolNodeOperationContainer(OperationContainer _operationContainer)
		{
			this.operationContainer = _operationContainer;
		}

		// Token: 0x06001CB3 RID: 7347 RVA: 0x000B6FA9 File Offset: 0x000B5FA9
		public void Destroy()
		{
			if (this.operationContainer != null)
			{
				this.operationContainer = null;
			}
		}

		// Token: 0x06001CB4 RID: 7348 RVA: 0x000B6FBC File Offset: 0x000B5FBC
		public void Bind(MapEditorMapObjectContainer _mapEditorMapObjectContainer)
		{
			this.Unbind();
			this.mapEditorMapObjectContainer = _mapEditorMapObjectContainer;
			if (this.mapEditorMapObjectContainer != null)
			{
				PatrolNodeContainer patrolNodeContainer = this.mapEditorMapObjectContainer.PatrolNodeContainer;
				if (patrolNodeContainer != null)
				{
					patrolNodeContainer.PatrolNodeLabelChanged += this.OnPatrolNodeLabelChanged;
					patrolNodeContainer.PatrolNodeScriptChanged += this.OnPatrolNodeScriptChanged;
					patrolNodeContainer.PatrolLinkDataTypeChanged += this.OnPatrolLinkDataTypeChanged;
					patrolNodeContainer.PatrolLinkDataStartChanged += this.OnPatrolLinkDataStartChanged;
					patrolNodeContainer.PatrolLinkDataWeightChanged += this.OnPatrolLinkDataWeightChanged;
				}
			}
		}

		// Token: 0x06001CB5 RID: 7349 RVA: 0x000B7048 File Offset: 0x000B6048
		public void Unbind()
		{
			if (this.mapEditorMapObjectContainer != null)
			{
				PatrolNodeContainer patrolNodeContainer = this.mapEditorMapObjectContainer.PatrolNodeContainer;
				if (patrolNodeContainer != null)
				{
					patrolNodeContainer.PatrolNodeLabelChanged -= this.OnPatrolNodeLabelChanged;
					patrolNodeContainer.PatrolNodeScriptChanged -= this.OnPatrolNodeScriptChanged;
					patrolNodeContainer.PatrolLinkDataTypeChanged -= this.OnPatrolLinkDataTypeChanged;
					patrolNodeContainer.PatrolLinkDataStartChanged -= this.OnPatrolLinkDataStartChanged;
					patrolNodeContainer.PatrolLinkDataWeightChanged -= this.OnPatrolLinkDataWeightChanged;
				}
				this.mapEditorMapObjectContainer = null;
			}
		}

		// Token: 0x0400124B RID: 4683
		private MapEditorMapObjectContainer mapEditorMapObjectContainer;

		// Token: 0x0400124C RID: 4684
		private OperationContainer operationContainer;
	}
}
