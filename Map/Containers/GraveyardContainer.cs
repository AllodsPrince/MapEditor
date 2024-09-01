using System;
using MapEditor.Map.MapObjects;

namespace MapEditor.Map.Containers
{
	// Token: 0x02000047 RID: 71
	public class GraveyardContainer : TypedMapObjectContainer
	{
		// Token: 0x060003E2 RID: 994 RVA: 0x00020B04 File Offset: 0x0001FB04
		private void OnGraveyardMapZoneChanged(Respawn respawn, ref string oldValue, ref string newValue)
		{
			if (this.mapEditorMapObjectContainer != null && base.Contains(respawn))
			{
				Graveyard graveyard = respawn as Graveyard;
				if (graveyard != null && this.GraveyardMapZoneChanged != null)
				{
					this.GraveyardMapZoneChanged(this.mapEditorMapObjectContainer, graveyard, ref oldValue, ref newValue);
				}
				if (!respawn.Temporary)
				{
					this.mapEditorMapObjectContainer.InvokeModified();
				}
			}
		}

		// Token: 0x060003E3 RID: 995 RVA: 0x00020B5C File Offset: 0x0001FB5C
		private void OnGraveyardRespawnTypeChanged(Respawn respawn, ref RespawnType oldValue, ref RespawnType newValue)
		{
			if (this.mapEditorMapObjectContainer != null && base.Contains(respawn))
			{
				Graveyard graveyard = respawn as Graveyard;
				if (graveyard != null && this.GraveyardRespawnTypeChanged != null)
				{
					this.GraveyardRespawnTypeChanged(this.mapEditorMapObjectContainer, graveyard, ref oldValue, ref newValue);
				}
				if (!respawn.Temporary)
				{
					this.mapEditorMapObjectContainer.InvokeModified();
				}
			}
		}

		// Token: 0x060003E4 RID: 996 RVA: 0x00020BB4 File Offset: 0x0001FBB4
		private void OnGraveyardFactionChanged(Respawn respawn, ref Faction oldValue, ref Faction newValue)
		{
			if (this.mapEditorMapObjectContainer != null && base.Contains(respawn))
			{
				Graveyard graveyard = respawn as Graveyard;
				if (graveyard != null && this.GraveyardFactionChanged != null)
				{
					this.GraveyardFactionChanged(this.mapEditorMapObjectContainer, graveyard, ref oldValue, ref newValue);
				}
				if (!respawn.Temporary)
				{
					this.mapEditorMapObjectContainer.InvokeModified();
				}
			}
		}

		// Token: 0x14000010 RID: 16
		// (add) Token: 0x060003E5 RID: 997 RVA: 0x00020C0B File Offset: 0x0001FC0B
		// (remove) Token: 0x060003E6 RID: 998 RVA: 0x00020C24 File Offset: 0x0001FC24
		public event GraveyardContainer.GraveyardFieldChangedEvent<string> GraveyardMapZoneChanged;

		// Token: 0x14000011 RID: 17
		// (add) Token: 0x060003E7 RID: 999 RVA: 0x00020C3D File Offset: 0x0001FC3D
		// (remove) Token: 0x060003E8 RID: 1000 RVA: 0x00020C56 File Offset: 0x0001FC56
		public event GraveyardContainer.GraveyardFieldChangedEvent<RespawnType> GraveyardRespawnTypeChanged;

		// Token: 0x14000012 RID: 18
		// (add) Token: 0x060003E9 RID: 1001 RVA: 0x00020C6F File Offset: 0x0001FC6F
		// (remove) Token: 0x060003EA RID: 1002 RVA: 0x00020C88 File Offset: 0x0001FC88
		public event GraveyardContainer.GraveyardFieldChangedEvent<Faction> GraveyardFactionChanged;

		// Token: 0x060003EB RID: 1003 RVA: 0x00020CA4 File Offset: 0x0001FCA4
		public GraveyardContainer(MapEditorMapObjectContainer _mapEditorMapObjectContainer) : base(MapObjectFactory.Type.Graveyard, false)
		{
			this.mapEditorMapObjectContainer = _mapEditorMapObjectContainer;
			if (this.mapEditorMapObjectContainer != null)
			{
				Respawn.MapZoneChanged += this.OnGraveyardMapZoneChanged;
				Respawn.RespawnTypeChanged += this.OnGraveyardRespawnTypeChanged;
				Respawn.FactionChanged += this.OnGraveyardFactionChanged;
				base.Destroyed += this.OnDestroyed;
			}
		}

		// Token: 0x060003EC RID: 1004 RVA: 0x00020D14 File Offset: 0x0001FD14
		public void OnDestroyed(TypedMapObjectContainer _typedMapObjectContainer)
		{
			if (this.mapEditorMapObjectContainer != null)
			{
				Respawn.MapZoneChanged -= this.OnGraveyardMapZoneChanged;
				Respawn.RespawnTypeChanged -= this.OnGraveyardRespawnTypeChanged;
				Respawn.FactionChanged -= this.OnGraveyardFactionChanged;
				base.Destroyed -= this.OnDestroyed;
				this.mapEditorMapObjectContainer = null;
			}
		}

		// Token: 0x040002B4 RID: 692
		private MapEditorMapObjectContainer mapEditorMapObjectContainer;

		// Token: 0x02000048 RID: 72
		// (Invoke) Token: 0x060003EE RID: 1006
		public delegate void GraveyardFieldChangedEvent<T>(MapEditorMapObjectContainer _mapEditorMapObjectContainer, Graveyard graveyard, ref T oldValue, ref T newValue);
	}
}
