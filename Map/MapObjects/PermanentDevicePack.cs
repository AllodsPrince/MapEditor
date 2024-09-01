using System;
using Tools.MapObjects;

namespace MapEditor.Map.MapObjects
{
	// Token: 0x020001F9 RID: 505
	public class PermanentDevicePack : SerializableMapObjectPack
	{
		// Token: 0x17000605 RID: 1541
		// (get) Token: 0x06001911 RID: 6417 RVA: 0x000A5DF0 File Offset: 0x000A4DF0
		// (set) Token: 0x06001912 RID: 6418 RVA: 0x000A5DF8 File Offset: 0x000A4DF8
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

		// Token: 0x17000606 RID: 1542
		// (get) Token: 0x06001913 RID: 6419 RVA: 0x000A5E01 File Offset: 0x000A4E01
		// (set) Token: 0x06001914 RID: 6420 RVA: 0x000A5E09 File Offset: 0x000A4E09
		public string ScriptID
		{
			get
			{
				return this.scriptID;
			}
			set
			{
				this.scriptID = value;
			}
		}

		// Token: 0x17000607 RID: 1543
		// (get) Token: 0x06001915 RID: 6421 RVA: 0x000A5E12 File Offset: 0x000A4E12
		// (set) Token: 0x06001916 RID: 6422 RVA: 0x000A5E1A File Offset: 0x000A4E1A
		public double ScanRadius
		{
			get
			{
				return this.scanRadius;
			}
			set
			{
				this.scanRadius = value;
			}
		}

		// Token: 0x17000608 RID: 1544
		// (get) Token: 0x06001917 RID: 6423 RVA: 0x000A5E23 File Offset: 0x000A4E23
		// (set) Token: 0x06001918 RID: 6424 RVA: 0x000A5E2B File Offset: 0x000A4E2B
		public bool AICollision
		{
			get
			{
				return this.aiCollision;
			}
			set
			{
				this.aiCollision = value;
			}
		}

		// Token: 0x06001919 RID: 6425 RVA: 0x000A5E34 File Offset: 0x000A4E34
		public override void Pack(IMapObject mapObject)
		{
			base.Pack(mapObject);
			PermanentDevice permanentDevice = mapObject as PermanentDevice;
			if (permanentDevice != null)
			{
				this.device = permanentDevice.Device;
				this.scriptID = permanentDevice.ScriptID;
				this.scanRadius = permanentDevice.ScanRadius;
				this.aiCollision = permanentDevice.AICollision;
			}
		}

		// Token: 0x0600191A RID: 6426 RVA: 0x000A5E84 File Offset: 0x000A4E84
		public override void Unpack(IMapObject mapObject)
		{
			base.Unpack(mapObject);
			PermanentDevice permanentDevice = mapObject as PermanentDevice;
			if (permanentDevice != null)
			{
				permanentDevice.Device = this.device;
				permanentDevice.ScriptID = this.scriptID;
				permanentDevice.ScanRadius = this.scanRadius;
				permanentDevice.AICollision = this.aiCollision;
			}
		}

		// Token: 0x04001028 RID: 4136
		private string device = string.Empty;

		// Token: 0x04001029 RID: 4137
		private string scriptID = string.Empty;

		// Token: 0x0400102A RID: 4138
		private double scanRadius;

		// Token: 0x0400102B RID: 4139
		private bool aiCollision = true;
	}
}
