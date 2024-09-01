using System;
using System.ComponentModel;
using Tools.MapObjects;

namespace MapEditor.Map.MapObjects
{
	// Token: 0x02000030 RID: 48
	public class Respawn : MapObject
	{
		// Token: 0x1400000B RID: 11
		// (add) Token: 0x060002E2 RID: 738 RVA: 0x0001DE88 File Offset: 0x0001CE88
		// (remove) Token: 0x060002E3 RID: 739 RVA: 0x0001DE9F File Offset: 0x0001CE9F
		public static event Respawn.RespawnFieldChangedEvent<string> MapZoneChanged;

		// Token: 0x1400000C RID: 12
		// (add) Token: 0x060002E4 RID: 740 RVA: 0x0001DEB6 File Offset: 0x0001CEB6
		// (remove) Token: 0x060002E5 RID: 741 RVA: 0x0001DECD File Offset: 0x0001CECD
		public static event Respawn.RespawnFieldChangedEvent<RespawnType> RespawnTypeChanged;

		// Token: 0x1400000D RID: 13
		// (add) Token: 0x060002E6 RID: 742 RVA: 0x0001DEE4 File Offset: 0x0001CEE4
		// (remove) Token: 0x060002E7 RID: 743 RVA: 0x0001DEFB File Offset: 0x0001CEFB
		public static event Respawn.RespawnFieldChangedEvent<Faction> FactionChanged;

		// Token: 0x17000037 RID: 55
		// (get) Token: 0x060002E8 RID: 744 RVA: 0x0001DF12 File Offset: 0x0001CF12
		public static RespawnType DefaultRespawnType
		{
			get
			{
				return RespawnType.Common;
			}
		}

		// Token: 0x060002E9 RID: 745 RVA: 0x0001DF15 File Offset: 0x0001CF15
		public static Faction GetFaction(string factionName)
		{
			if (factionName.Equals(Respawn.kaniaFaction, StringComparison.OrdinalIgnoreCase))
			{
				return Faction.Kania;
			}
			if (factionName.Equals(Respawn.hadaganFaction, StringComparison.OrdinalIgnoreCase))
			{
				return Faction.Hadagan;
			}
			return Faction.Undefined;
		}

		// Token: 0x060002EA RID: 746 RVA: 0x0001DF38 File Offset: 0x0001CF38
		public static string GetFactionName(Faction faction)
		{
			if (faction == Faction.Kania)
			{
				return Respawn.kaniaFaction;
			}
			if (faction == Faction.Hadagan)
			{
				return Respawn.hadaganFaction;
			}
			return string.Empty;
		}

		// Token: 0x060002EB RID: 747 RVA: 0x0001DF53 File Offset: 0x0001CF53
		public Respawn(int _id, MapObjectType _type, ICollisionMap _collisionMap, RespawnType _respawnType) : base(_id, _type, _collisionMap)
		{
			this.mapZone = _type.Stats;
			this.respawnType = _respawnType;
		}

		// Token: 0x17000038 RID: 56
		// (get) Token: 0x060002EC RID: 748 RVA: 0x0001DF85 File Offset: 0x0001CF85
		// (set) Token: 0x060002ED RID: 749 RVA: 0x0001DF90 File Offset: 0x0001CF90
		[Browsable(false)]
		public string MapZone
		{
			get
			{
				return this.mapZone;
			}
			set
			{
				if (this.mapZone != value && base.InvokeChanging(null))
				{
					string oldMapZone = this.mapZone;
					this.mapZone = value;
					base.SetNewStats(this.mapZone);
					base.InvokeChanged();
					if (base.Active && Respawn.MapZoneChanged != null)
					{
						Respawn.MapZoneChanged(this, ref oldMapZone, ref this.mapZone);
					}
				}
			}
		}

		// Token: 0x17000039 RID: 57
		// (get) Token: 0x060002EE RID: 750 RVA: 0x0001DFF6 File Offset: 0x0001CFF6
		// (set) Token: 0x060002EF RID: 751 RVA: 0x0001E000 File Offset: 0x0001D000
		[Browsable(true)]
		[Category("Respawn")]
		[DisplayName("Type")]
		public RespawnType RespawnType
		{
			get
			{
				return this.respawnType;
			}
			set
			{
				if (this.respawnType != value && base.InvokeChanging(null))
				{
					RespawnType oldRespawnType = this.respawnType;
					this.respawnType = value;
					base.InvokeChanged();
					if (base.Active && Respawn.RespawnTypeChanged != null)
					{
						Respawn.RespawnTypeChanged(this, ref oldRespawnType, ref this.respawnType);
					}
				}
			}
		}

		// Token: 0x1700003A RID: 58
		// (get) Token: 0x060002F0 RID: 752 RVA: 0x0001E055 File Offset: 0x0001D055
		// (set) Token: 0x060002F1 RID: 753 RVA: 0x0001E060 File Offset: 0x0001D060
		[Browsable(true)]
		[Category("Respawn")]
		[DisplayName("Faction")]
		public Faction Faction
		{
			get
			{
				return this.faction;
			}
			set
			{
				if (this.faction != value && base.InvokeChanging(null))
				{
					Faction oldFaction = this.faction;
					this.faction = value;
					base.InvokeChanged();
					if (base.Active && Respawn.FactionChanged != null)
					{
						Respawn.FactionChanged(this, ref oldFaction, ref this.faction);
					}
				}
			}
		}

		// Token: 0x060002F2 RID: 754 RVA: 0x0001E0B5 File Offset: 0x0001D0B5
		public void CopyToRespawn(Respawn respawn)
		{
			if (respawn != null)
			{
				respawn.faction = this.faction;
			}
		}

		// Token: 0x04000257 RID: 599
		private static readonly string kaniaFaction = "World/Factions/League.xdb";

		// Token: 0x04000258 RID: 600
		private static readonly string hadaganFaction = "World/Factions/Empire.xdb";

		// Token: 0x04000259 RID: 601
		private string mapZone = string.Empty;

		// Token: 0x0400025A RID: 602
		private RespawnType respawnType = RespawnType.Common;

		// Token: 0x0400025B RID: 603
		private Faction faction;

		// Token: 0x02000031 RID: 49
		// (Invoke) Token: 0x060002F5 RID: 757
		public delegate void RespawnFieldChangedEvent<T>(Respawn respawn, ref T oldValue, ref T newValue);
	}
}
