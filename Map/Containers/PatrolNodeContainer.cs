using System;
using MapEditor.Map.MapObjects;
using Tools.MapObjects;

namespace MapEditor.Map.Containers
{
	// Token: 0x02000298 RID: 664
	public class PatrolNodeContainer : TypedMapObjectContainer
	{
		// Token: 0x06001EFC RID: 7932 RVA: 0x000C7110 File Offset: 0x000C6110
		private void OnPatrolNodeLabelChanged(PatrolNode patrolNode, ref string oldValue, ref string newValue)
		{
			if (this.mapEditorMapObjectContainer != null && base.Contains(patrolNode))
			{
				if (this.PatrolNodeLabelChanged != null)
				{
					this.PatrolNodeLabelChanged(this.mapEditorMapObjectContainer, patrolNode, ref oldValue, ref newValue);
				}
				if (!patrolNode.Temporary)
				{
					this.mapEditorMapObjectContainer.InvokeModified();
				}
			}
		}

		// Token: 0x06001EFD RID: 7933 RVA: 0x000C7160 File Offset: 0x000C6160
		private void OnPatrolNodeScriptChanged(PatrolNode patrolNode, ref string oldValue, ref string newValue)
		{
			if (this.mapEditorMapObjectContainer != null && base.Contains(patrolNode))
			{
				if (this.PatrolNodeScriptChanged != null)
				{
					this.PatrolNodeScriptChanged(this.mapEditorMapObjectContainer, patrolNode, ref oldValue, ref newValue);
				}
				if (!patrolNode.Temporary)
				{
					this.mapEditorMapObjectContainer.InvokeModified();
				}
			}
		}

		// Token: 0x06001EFE RID: 7934 RVA: 0x000C71B0 File Offset: 0x000C61B0
		private void OnPatrolNodeAggroRadiusChanged(PatrolNode patrolNode, ref double oldValue, ref double newValue)
		{
			if (this.mapEditorMapObjectContainer != null && base.Contains(patrolNode))
			{
				if (this.PatrolNodeAggroRadiusChanged != null)
				{
					this.PatrolNodeAggroRadiusChanged(this.mapEditorMapObjectContainer, patrolNode, ref oldValue, ref newValue);
				}
				if (!patrolNode.Temporary)
				{
					this.mapEditorMapObjectContainer.InvokeModified();
				}
			}
		}

		// Token: 0x06001EFF RID: 7935 RVA: 0x000C7200 File Offset: 0x000C6200
		private void OnPatrolLinkDataTypeChanged(PatrolLinkData patrolLinkData, ref PatrolNodeLinkType oldValue, ref PatrolNodeLinkType newValue)
		{
			IMapObject left;
			IMapObject right;
			if (this.mapEditorMapObjectContainer != null && this.mapEditorMapObjectContainer.TryGetMapObjects(patrolLinkData, out left, out right) && left != null && right != null)
			{
				if (this.PatrolLinkDataTypeChanged != null)
				{
					this.PatrolLinkDataTypeChanged(this.mapEditorMapObjectContainer, patrolLinkData, ref oldValue, ref newValue);
				}
				if (!left.Temporary && !right.Temporary)
				{
					this.mapEditorMapObjectContainer.InvokeModified();
				}
			}
		}

		// Token: 0x06001F00 RID: 7936 RVA: 0x000C7264 File Offset: 0x000C6264
		private void OnPatrolLinkDataStartChanged(PatrolLinkData patrolLinkData, ref string oldValue, ref string newValue)
		{
			IMapObject left;
			IMapObject right;
			if (this.mapEditorMapObjectContainer != null && this.mapEditorMapObjectContainer.TryGetMapObjects(patrolLinkData, out left, out right) && left != null && right != null)
			{
				if (this.PatrolLinkDataStartChanged != null)
				{
					this.PatrolLinkDataStartChanged(this.mapEditorMapObjectContainer, patrolLinkData, ref oldValue, ref newValue);
				}
				if (!left.Temporary && !right.Temporary)
				{
					this.mapEditorMapObjectContainer.InvokeModified();
				}
			}
		}

		// Token: 0x06001F01 RID: 7937 RVA: 0x000C72C8 File Offset: 0x000C62C8
		private void OnPatrolLinkDataWeightChanged(PatrolLinkData patrolLinkData, ref int oldValue, ref int newValue)
		{
			IMapObject left;
			IMapObject right;
			if (this.mapEditorMapObjectContainer != null && this.mapEditorMapObjectContainer.TryGetMapObjects(patrolLinkData, out left, out right) && left != null && right != null)
			{
				if (this.PatrolLinkDataWeightChanged != null)
				{
					this.PatrolLinkDataWeightChanged(this.mapEditorMapObjectContainer, patrolLinkData, ref oldValue, ref newValue);
				}
				if (!left.Temporary && !right.Temporary)
				{
					this.mapEditorMapObjectContainer.InvokeModified();
				}
			}
		}

		// Token: 0x140000D9 RID: 217
		// (add) Token: 0x06001F02 RID: 7938 RVA: 0x000C732C File Offset: 0x000C632C
		// (remove) Token: 0x06001F03 RID: 7939 RVA: 0x000C7345 File Offset: 0x000C6345
		public event PatrolNodeContainer.PatrolNodeFieldChangedEvent<string> PatrolNodeLabelChanged;

		// Token: 0x140000DA RID: 218
		// (add) Token: 0x06001F04 RID: 7940 RVA: 0x000C735E File Offset: 0x000C635E
		// (remove) Token: 0x06001F05 RID: 7941 RVA: 0x000C7377 File Offset: 0x000C6377
		public event PatrolNodeContainer.PatrolNodeFieldChangedEvent<string> PatrolNodeScriptChanged;

		// Token: 0x140000DB RID: 219
		// (add) Token: 0x06001F06 RID: 7942 RVA: 0x000C7390 File Offset: 0x000C6390
		// (remove) Token: 0x06001F07 RID: 7943 RVA: 0x000C73A9 File Offset: 0x000C63A9
		public event PatrolNodeContainer.PatrolNodeFieldChangedEvent<double> PatrolNodeAggroRadiusChanged;

		// Token: 0x140000DC RID: 220
		// (add) Token: 0x06001F08 RID: 7944 RVA: 0x000C73C2 File Offset: 0x000C63C2
		// (remove) Token: 0x06001F09 RID: 7945 RVA: 0x000C73DB File Offset: 0x000C63DB
		public event PatrolNodeContainer.PatrolLinkDataFieldChangedEvent<PatrolNodeLinkType> PatrolLinkDataTypeChanged;

		// Token: 0x140000DD RID: 221
		// (add) Token: 0x06001F0A RID: 7946 RVA: 0x000C73F4 File Offset: 0x000C63F4
		// (remove) Token: 0x06001F0B RID: 7947 RVA: 0x000C740D File Offset: 0x000C640D
		public event PatrolNodeContainer.PatrolLinkDataFieldChangedEvent<string> PatrolLinkDataStartChanged;

		// Token: 0x140000DE RID: 222
		// (add) Token: 0x06001F0C RID: 7948 RVA: 0x000C7426 File Offset: 0x000C6426
		// (remove) Token: 0x06001F0D RID: 7949 RVA: 0x000C743F File Offset: 0x000C643F
		public event PatrolNodeContainer.PatrolLinkDataFieldChangedEvent<int> PatrolLinkDataWeightChanged;

		// Token: 0x06001F0E RID: 7950 RVA: 0x000C7458 File Offset: 0x000C6458
		public PatrolNodeContainer(MapEditorMapObjectContainer _mapEditorMapObjectContainer) : base(MapObjectFactory.Type.PatrolNode, false)
		{
			this.mapEditorMapObjectContainer = _mapEditorMapObjectContainer;
			if (this.mapEditorMapObjectContainer != null)
			{
				PatrolNode.LabelChanged += this.OnPatrolNodeLabelChanged;
				PatrolNode.ScriptChanged += this.OnPatrolNodeScriptChanged;
				PatrolNode.AggroRadiusChanged += this.OnPatrolNodeAggroRadiusChanged;
				PatrolLinkData.TypeChanged += this.OnPatrolLinkDataTypeChanged;
				PatrolLinkData.StartChanged += this.OnPatrolLinkDataStartChanged;
				PatrolLinkData.WeightChanged += this.OnPatrolLinkDataWeightChanged;
				base.Destroyed += this.OnDestroyed;
			}
		}

		// Token: 0x06001F0F RID: 7951 RVA: 0x000C74F8 File Offset: 0x000C64F8
		public void OnDestroyed(TypedMapObjectContainer _typedMapObjectContainer)
		{
			if (this.mapEditorMapObjectContainer != null)
			{
				PatrolNode.LabelChanged -= this.OnPatrolNodeLabelChanged;
				PatrolNode.ScriptChanged -= this.OnPatrolNodeScriptChanged;
				PatrolNode.AggroRadiusChanged -= this.OnPatrolNodeAggroRadiusChanged;
				PatrolLinkData.TypeChanged -= this.OnPatrolLinkDataTypeChanged;
				PatrolLinkData.StartChanged -= this.OnPatrolLinkDataStartChanged;
				PatrolLinkData.WeightChanged -= this.OnPatrolLinkDataWeightChanged;
				base.Destroyed -= this.OnDestroyed;
				this.mapEditorMapObjectContainer = null;
			}
		}

		// Token: 0x0400133A RID: 4922
		private MapEditorMapObjectContainer mapEditorMapObjectContainer;

		// Token: 0x02000299 RID: 665
		// (Invoke) Token: 0x06001F11 RID: 7953
		public delegate void PatrolNodeFieldChangedEvent<T>(MapEditorMapObjectContainer _mapEditorMapObjectContainer, PatrolNode patrolNode, ref T oldValue, ref T newValue);

		// Token: 0x0200029A RID: 666
		// (Invoke) Token: 0x06001F15 RID: 7957
		public delegate void PatrolLinkDataFieldChangedEvent<T>(MapEditorMapObjectContainer _mapEditorMapObjectContainer, PatrolLinkData patrolLinkData, ref T oldValue, ref T newValue);
	}
}
