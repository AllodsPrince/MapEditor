using System;
using System.Xml.Serialization;

namespace MapEditor.Map.MapObjects
{
	// Token: 0x02000227 RID: 551
	[XmlInclude(typeof(MobClientSpawnPointData))]
	[XmlInclude(typeof(DeviceClientSpawnPointData))]
	public abstract class ClientSpawnPointData
	{
		// Token: 0x17000644 RID: 1604
		// (get) Token: 0x06001A89 RID: 6793
		public abstract string Type { get; }

		// Token: 0x140000B7 RID: 183
		// (add) Token: 0x06001A8A RID: 6794 RVA: 0x000AE96B File Offset: 0x000AD96B
		// (remove) Token: 0x06001A8B RID: 6795 RVA: 0x000AE982 File Offset: 0x000AD982
		public static event ClientSpawnPointData.ClientSpawnPointDataChangedEvent Changed;

		// Token: 0x06001A8C RID: 6796 RVA: 0x000AE999 File Offset: 0x000AD999
		protected void InvokeChangedEvent(string property, object oldValue, object newValue)
		{
			if (ClientSpawnPointData.Changed != null)
			{
				ClientSpawnPointData.Changed(this, property, oldValue, newValue);
			}
		}

		// Token: 0x06001A8D RID: 6797 RVA: 0x000AE9B0 File Offset: 0x000AD9B0
		public static ClientSpawnPointData Create(string type)
		{
			if (type != null)
			{
				if (type == "VisualMob")
				{
					return new MobClientSpawnPointData();
				}
				if (type == "gameMechanics.world.device.DeviceResource")
				{
					return new DeviceClientSpawnPointData();
				}
			}
			return null;
		}

		// Token: 0x04001123 RID: 4387
		public const string objectRefField = "ObjectRef";

		// Token: 0x02000228 RID: 552
		// (Invoke) Token: 0x06001A90 RID: 6800
		public delegate void ClientSpawnPointDataChangedEvent(ClientSpawnPointData sender, string property, object oldValue, object newValue);
	}
}
