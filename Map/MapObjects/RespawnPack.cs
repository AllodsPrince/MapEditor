using System;
using Tools.MapObjects;

namespace MapEditor.Map.MapObjects
{
	// Token: 0x0200002F RID: 47
	public class RespawnPack : SerializableMapObjectPack
	{
		// Token: 0x17000034 RID: 52
		// (get) Token: 0x060002D9 RID: 729 RVA: 0x0001DDB4 File Offset: 0x0001CDB4
		// (set) Token: 0x060002DA RID: 730 RVA: 0x0001DDBC File Offset: 0x0001CDBC
		public string MapZone
		{
			get
			{
				return this.mapZone;
			}
			set
			{
				this.mapZone = value;
			}
		}

		// Token: 0x17000035 RID: 53
		// (get) Token: 0x060002DB RID: 731 RVA: 0x0001DDC5 File Offset: 0x0001CDC5
		// (set) Token: 0x060002DC RID: 732 RVA: 0x0001DDCD File Offset: 0x0001CDCD
		public RespawnType RespawnType
		{
			get
			{
				return this.respawnType;
			}
			set
			{
				this.respawnType = value;
			}
		}

		// Token: 0x17000036 RID: 54
		// (get) Token: 0x060002DD RID: 733 RVA: 0x0001DDD6 File Offset: 0x0001CDD6
		// (set) Token: 0x060002DE RID: 734 RVA: 0x0001DDDE File Offset: 0x0001CDDE
		public Faction Faction
		{
			get
			{
				return this.faction;
			}
			set
			{
				this.faction = value;
			}
		}

		// Token: 0x060002DF RID: 735 RVA: 0x0001DDE8 File Offset: 0x0001CDE8
		public override void Pack(IMapObject mapObject)
		{
			base.Pack(mapObject);
			Respawn respawn = mapObject as Respawn;
			if (respawn != null)
			{
				this.mapZone = respawn.MapZone;
				this.respawnType = respawn.RespawnType;
				this.faction = respawn.Faction;
			}
		}

		// Token: 0x060002E0 RID: 736 RVA: 0x0001DE2C File Offset: 0x0001CE2C
		public override void Unpack(IMapObject mapObject)
		{
			base.Unpack(mapObject);
			Respawn respawn = mapObject as Respawn;
			if (respawn != null)
			{
				respawn.MapZone = this.mapZone;
				respawn.RespawnType = this.respawnType;
				respawn.Faction = this.faction;
			}
		}

		// Token: 0x04000254 RID: 596
		private string mapZone = string.Empty;

		// Token: 0x04000255 RID: 597
		private RespawnType respawnType = RespawnType.Common;

		// Token: 0x04000256 RID: 598
		private Faction faction;
	}
}
