using System;

namespace MapEditor.Map.MapObjects
{
	// Token: 0x0200022A RID: 554
	public class DeviceClientSpawnPointData : ClientSpawnPointData
	{
		// Token: 0x17000646 RID: 1606
		// (get) Token: 0x06001A95 RID: 6805 RVA: 0x000AEA02 File Offset: 0x000ADA02
		public override string Type
		{
			get
			{
				return "gameMechanics.world.device.DeviceResource";
			}
		}

		// Token: 0x17000647 RID: 1607
		// (get) Token: 0x06001A96 RID: 6806 RVA: 0x000AEA09 File Offset: 0x000ADA09
		// (set) Token: 0x06001A97 RID: 6807 RVA: 0x000AEA14 File Offset: 0x000ADA14
		public int VisualState
		{
			get
			{
				return this.visualState;
			}
			set
			{
				if (this.visualState != value)
				{
					int oldValue = this.visualState;
					this.visualState = value;
					base.InvokeChangedEvent("VisualState", oldValue, value);
				}
			}
		}

		// Token: 0x04001126 RID: 4390
		public const string type = "gameMechanics.world.device.DeviceResource";

		// Token: 0x04001127 RID: 4391
		public const string visualStateField = "VisualState";

		// Token: 0x04001128 RID: 4392
		private int visualState;
	}
}
