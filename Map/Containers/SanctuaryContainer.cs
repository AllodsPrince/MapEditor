using System;
using MapEditor.Map.MapObjects;

namespace MapEditor.Map.Containers
{
	// Token: 0x0200000F RID: 15
	public class SanctuaryContainer : TypedMapObjectContainer
	{
		// Token: 0x06000048 RID: 72 RVA: 0x00003CEC File Offset: 0x00002CEC
		private void OnSanctuaryMapZoneChanged(Respawn respawn, ref string oldValue, ref string newValue)
		{
			if (this.mapEditorMapObjectContainer != null && base.Contains(respawn))
			{
				Sanctuary sanctuary = respawn as Sanctuary;
				if (sanctuary != null && this.SanctuaryMapZoneChanged != null)
				{
					this.SanctuaryMapZoneChanged(this.mapEditorMapObjectContainer, sanctuary, ref oldValue, ref newValue);
				}
				if (!respawn.Temporary)
				{
					this.mapEditorMapObjectContainer.InvokeModified();
				}
			}
		}

		// Token: 0x06000049 RID: 73 RVA: 0x00003D44 File Offset: 0x00002D44
		private void OnSanctuaryRespawnTypeChanged(Respawn respawn, ref RespawnType oldValue, ref RespawnType newValue)
		{
			if (this.mapEditorMapObjectContainer != null && base.Contains(respawn))
			{
				Sanctuary sanctuary = respawn as Sanctuary;
				if (sanctuary != null && this.SanctuaryRespawnTypeChanged != null)
				{
					this.SanctuaryRespawnTypeChanged(this.mapEditorMapObjectContainer, sanctuary, ref oldValue, ref newValue);
				}
				if (!respawn.Temporary)
				{
					this.mapEditorMapObjectContainer.InvokeModified();
				}
			}
		}

		// Token: 0x0600004A RID: 74 RVA: 0x00003D9C File Offset: 0x00002D9C
		private void OnSanctuaryFactionChanged(Respawn respawn, ref Faction oldValue, ref Faction newValue)
		{
			if (this.mapEditorMapObjectContainer != null && base.Contains(respawn))
			{
				Sanctuary sanctuary = respawn as Sanctuary;
				if (sanctuary != null && this.SanctuaryFactionChanged != null)
				{
					this.SanctuaryFactionChanged(this.mapEditorMapObjectContainer, sanctuary, ref oldValue, ref newValue);
				}
				if (!respawn.Temporary)
				{
					this.mapEditorMapObjectContainer.InvokeModified();
				}
			}
		}

		// Token: 0x14000004 RID: 4
		// (add) Token: 0x0600004B RID: 75 RVA: 0x00003DF3 File Offset: 0x00002DF3
		// (remove) Token: 0x0600004C RID: 76 RVA: 0x00003E0C File Offset: 0x00002E0C
		public event SanctuaryContainer.SanctuaryFieldChangedEvent<string> SanctuaryMapZoneChanged;

		// Token: 0x14000005 RID: 5
		// (add) Token: 0x0600004D RID: 77 RVA: 0x00003E25 File Offset: 0x00002E25
		// (remove) Token: 0x0600004E RID: 78 RVA: 0x00003E3E File Offset: 0x00002E3E
		public event SanctuaryContainer.SanctuaryFieldChangedEvent<RespawnType> SanctuaryRespawnTypeChanged;

		// Token: 0x14000006 RID: 6
		// (add) Token: 0x0600004F RID: 79 RVA: 0x00003E57 File Offset: 0x00002E57
		// (remove) Token: 0x06000050 RID: 80 RVA: 0x00003E70 File Offset: 0x00002E70
		public event SanctuaryContainer.SanctuaryFieldChangedEvent<Faction> SanctuaryFactionChanged;

		// Token: 0x06000051 RID: 81 RVA: 0x00003E8C File Offset: 0x00002E8C
		public SanctuaryContainer(MapEditorMapObjectContainer _mapEditorMapObjectContainer) : base(MapObjectFactory.Type.Sanctuary, false)
		{
			this.mapEditorMapObjectContainer = _mapEditorMapObjectContainer;
			if (this.mapEditorMapObjectContainer != null)
			{
				Respawn.MapZoneChanged += this.OnSanctuaryMapZoneChanged;
				Respawn.RespawnTypeChanged += this.OnSanctuaryRespawnTypeChanged;
				Respawn.FactionChanged += this.OnSanctuaryFactionChanged;
				base.Destroyed += this.OnDestroyed;
			}
		}

		// Token: 0x06000052 RID: 82 RVA: 0x00003EFC File Offset: 0x00002EFC
		public void OnDestroyed(TypedMapObjectContainer _typedMapObjectContainer)
		{
			if (this.mapEditorMapObjectContainer != null)
			{
				Respawn.MapZoneChanged -= this.OnSanctuaryMapZoneChanged;
				Respawn.RespawnTypeChanged -= this.OnSanctuaryRespawnTypeChanged;
				Respawn.FactionChanged -= this.OnSanctuaryFactionChanged;
				base.Destroyed -= this.OnDestroyed;
				this.mapEditorMapObjectContainer = null;
			}
		}

		// Token: 0x04000016 RID: 22
		private MapEditorMapObjectContainer mapEditorMapObjectContainer;

		// Token: 0x02000010 RID: 16
		// (Invoke) Token: 0x06000054 RID: 84
		public delegate void SanctuaryFieldChangedEvent<T>(MapEditorMapObjectContainer _mapEditorMapObjectContainer, Sanctuary sanctuary, ref T oldValue, ref T newValue);
	}
}
