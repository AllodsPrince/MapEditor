using System;
using Tools.MapObjects;

namespace MapEditor.Map.MapObjects
{
	// Token: 0x020001A3 RID: 419
	public class ZoneLocatorPack : SerializableMapObjectPack
	{
		// Token: 0x170003FC RID: 1020
		// (get) Token: 0x06001451 RID: 5201 RVA: 0x00093A53 File Offset: 0x00092A53
		// (set) Token: 0x06001452 RID: 5202 RVA: 0x00093A5B File Offset: 0x00092A5B
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

		// Token: 0x06001453 RID: 5203 RVA: 0x00093A64 File Offset: 0x00092A64
		public override void Pack(IMapObject mapObject)
		{
			base.Pack(mapObject);
			ZoneLocator zoneLocator = mapObject as ZoneLocator;
			if (zoneLocator != null)
			{
				this.mapZone = zoneLocator.MapZone;
			}
		}

		// Token: 0x06001454 RID: 5204 RVA: 0x00093A90 File Offset: 0x00092A90
		public override void Unpack(IMapObject mapObject)
		{
			base.Unpack(mapObject);
			ZoneLocator zoneLocator = mapObject as ZoneLocator;
			if (zoneLocator != null)
			{
				zoneLocator.MapZone = this.mapZone;
			}
		}

		// Token: 0x04000E54 RID: 3668
		private string mapZone = string.Empty;
	}
}
