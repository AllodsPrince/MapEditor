using System;
using MapEditor.Map.Containers;
using Operations;

namespace MapEditor.Map.MapObjects
{
	// Token: 0x02000035 RID: 53
	public class RespawnOperationContainer
	{
		// Token: 0x06000316 RID: 790 RVA: 0x0001E290 File Offset: 0x0001D290
		private void OnRespawnMapZoneChanged(MapEditorMapObjectContainer _mapEditorMapObjectContainer, Respawn respawn, ref string oldValue, ref string newValue)
		{
			if (this.mapEditorMapObjectContainer != null && respawn != null && this.operationContainer != null && !respawn.Temporary)
			{
				ChangeRespawnMapZoneOperation operation = new ChangeRespawnMapZoneOperation(respawn, ref oldValue, ref newValue);
				this.operationContainer.Add(operation);
			}
		}

		// Token: 0x06000317 RID: 791 RVA: 0x0001E2D0 File Offset: 0x0001D2D0
		private void OnRespawnTypeChanged(MapEditorMapObjectContainer _mapEditorMapObjectContainer, Respawn respawn, ref RespawnType oldValue, ref RespawnType newValue)
		{
			if (this.mapEditorMapObjectContainer != null && respawn != null && this.operationContainer != null && !respawn.Temporary)
			{
				ChangeRespawnTypeOperation operation = new ChangeRespawnTypeOperation(respawn, ref oldValue, ref newValue);
				this.operationContainer.Add(operation);
			}
		}

		// Token: 0x06000318 RID: 792 RVA: 0x0001E310 File Offset: 0x0001D310
		private void OnFactionChanged(MapEditorMapObjectContainer _mapEditorMapObjectContainer, Respawn respawn, ref Faction oldValue, ref Faction newValue)
		{
			if (this.mapEditorMapObjectContainer != null && respawn != null && this.operationContainer != null && !respawn.Temporary)
			{
				ChangeFactionOperation operation = new ChangeFactionOperation(respawn, ref oldValue, ref newValue);
				this.operationContainer.Add(operation);
			}
		}

		// Token: 0x06000319 RID: 793 RVA: 0x0001E34E File Offset: 0x0001D34E
		public RespawnOperationContainer(OperationContainer _operationContainer)
		{
			this.operationContainer = _operationContainer;
		}

		// Token: 0x0600031A RID: 794 RVA: 0x0001E35D File Offset: 0x0001D35D
		public void Destroy()
		{
			if (this.operationContainer != null)
			{
				this.operationContainer = null;
			}
		}

		// Token: 0x0600031B RID: 795 RVA: 0x0001E370 File Offset: 0x0001D370
		public void Bind(MapEditorMapObjectContainer _mapEditorMapObjectContainer)
		{
			this.Unbind();
			this.mapEditorMapObjectContainer = _mapEditorMapObjectContainer;
			if (this.mapEditorMapObjectContainer != null)
			{
				GraveyardContainer graveyardContainer = this.mapEditorMapObjectContainer.GraveyardContainer;
				if (graveyardContainer != null)
				{
					graveyardContainer.GraveyardMapZoneChanged += new GraveyardContainer.GraveyardFieldChangedEvent<string>(this.OnRespawnMapZoneChanged);
					graveyardContainer.GraveyardRespawnTypeChanged += new GraveyardContainer.GraveyardFieldChangedEvent<RespawnType>(this.OnRespawnTypeChanged);
					graveyardContainer.GraveyardFactionChanged += new GraveyardContainer.GraveyardFieldChangedEvent<Faction>(this.OnFactionChanged);
				}
				SanctuaryContainer sanctuaryContainer = this.mapEditorMapObjectContainer.SanctuaryContainer;
				if (sanctuaryContainer != null)
				{
					sanctuaryContainer.SanctuaryMapZoneChanged += new SanctuaryContainer.SanctuaryFieldChangedEvent<string>(this.OnRespawnMapZoneChanged);
					sanctuaryContainer.SanctuaryRespawnTypeChanged += new SanctuaryContainer.SanctuaryFieldChangedEvent<RespawnType>(this.OnRespawnTypeChanged);
					sanctuaryContainer.SanctuaryFactionChanged += new SanctuaryContainer.SanctuaryFieldChangedEvent<Faction>(this.OnFactionChanged);
				}
			}
		}

		// Token: 0x0600031C RID: 796 RVA: 0x0001E420 File Offset: 0x0001D420
		public void Unbind()
		{
			if (this.mapEditorMapObjectContainer != null)
			{
				GraveyardContainer graveyardContainer = this.mapEditorMapObjectContainer.GraveyardContainer;
				if (graveyardContainer != null)
				{
					graveyardContainer.GraveyardMapZoneChanged -= new GraveyardContainer.GraveyardFieldChangedEvent<string>(this.OnRespawnMapZoneChanged);
					graveyardContainer.GraveyardRespawnTypeChanged -= new GraveyardContainer.GraveyardFieldChangedEvent<RespawnType>(this.OnRespawnTypeChanged);
					graveyardContainer.GraveyardFactionChanged -= new GraveyardContainer.GraveyardFieldChangedEvent<Faction>(this.OnFactionChanged);
				}
				SanctuaryContainer sanctuaryContainer = this.mapEditorMapObjectContainer.SanctuaryContainer;
				if (sanctuaryContainer != null)
				{
					sanctuaryContainer.SanctuaryMapZoneChanged -= new SanctuaryContainer.SanctuaryFieldChangedEvent<string>(this.OnRespawnMapZoneChanged);
					sanctuaryContainer.SanctuaryRespawnTypeChanged -= new SanctuaryContainer.SanctuaryFieldChangedEvent<RespawnType>(this.OnRespawnTypeChanged);
					sanctuaryContainer.SanctuaryFactionChanged -= new SanctuaryContainer.SanctuaryFieldChangedEvent<Faction>(this.OnFactionChanged);
				}
				this.mapEditorMapObjectContainer = null;
			}
		}

		// Token: 0x04000268 RID: 616
		private MapEditorMapObjectContainer mapEditorMapObjectContainer;

		// Token: 0x04000269 RID: 617
		private OperationContainer operationContainer;
	}
}
