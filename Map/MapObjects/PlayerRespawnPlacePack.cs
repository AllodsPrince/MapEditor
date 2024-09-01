using System;
using Tools.MapObjects;

namespace MapEditor.Map.MapObjects
{
	// Token: 0x020002C1 RID: 705
	public class PlayerRespawnPlacePack : SerializableMapObjectPack
	{
		// Token: 0x17000755 RID: 1877
		// (get) Token: 0x060020CA RID: 8394 RVA: 0x000D158A File Offset: 0x000D058A
		// (set) Token: 0x060020CB RID: 8395 RVA: 0x000D1592 File Offset: 0x000D0592
		public string Device
		{
			get
			{
				return this.device;
			}
			set
			{
				this.device = value;
			}
		}

		// Token: 0x17000756 RID: 1878
		// (get) Token: 0x060020CC RID: 8396 RVA: 0x000D159B File Offset: 0x000D059B
		// (set) Token: 0x060020CD RID: 8397 RVA: 0x000D15A3 File Offset: 0x000D05A3
		public string Faction
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

		// Token: 0x060020CE RID: 8398 RVA: 0x000D15AC File Offset: 0x000D05AC
		public override void Pack(IMapObject mapObject)
		{
			base.Pack(mapObject);
			PlayerRespawnPlace playerRespawnPlace = mapObject as PlayerRespawnPlace;
			if (playerRespawnPlace != null)
			{
				this.device = playerRespawnPlace.Device;
				this.faction = playerRespawnPlace.Faction;
			}
		}

		// Token: 0x060020CF RID: 8399 RVA: 0x000D15E4 File Offset: 0x000D05E4
		public override void Unpack(IMapObject mapObject)
		{
			base.Unpack(mapObject);
			PlayerRespawnPlace playerRespawnPlace = mapObject as PlayerRespawnPlace;
			if (playerRespawnPlace != null)
			{
				playerRespawnPlace.Device = this.device;
				playerRespawnPlace.Faction = this.faction;
			}
		}

		// Token: 0x040013FC RID: 5116
		private string device = string.Empty;

		// Token: 0x040013FD RID: 5117
		private string faction = string.Empty;
	}
}
