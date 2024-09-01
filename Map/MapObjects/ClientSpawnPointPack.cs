using System;
using Tools.MapObjects;

namespace MapEditor.Map.MapObjects
{
	// Token: 0x02000226 RID: 550
	public class ClientSpawnPointPack : SerializableMapObjectPack
	{
		// Token: 0x17000640 RID: 1600
		// (get) Token: 0x06001A7E RID: 6782 RVA: 0x000AE85E File Offset: 0x000AD85E
		// (set) Token: 0x06001A7F RID: 6783 RVA: 0x000AE866 File Offset: 0x000AD866
		public string VisObject
		{
			get
			{
				return this.visObject;
			}
			set
			{
				this.visObject = value;
			}
		}

		// Token: 0x17000641 RID: 1601
		// (get) Token: 0x06001A80 RID: 6784 RVA: 0x000AE86F File Offset: 0x000AD86F
		// (set) Token: 0x06001A81 RID: 6785 RVA: 0x000AE877 File Offset: 0x000AD877
		public string Scene
		{
			get
			{
				return this.scene;
			}
			set
			{
				this.scene = value;
			}
		}

		// Token: 0x17000642 RID: 1602
		// (get) Token: 0x06001A82 RID: 6786 RVA: 0x000AE880 File Offset: 0x000AD880
		// (set) Token: 0x06001A83 RID: 6787 RVA: 0x000AE888 File Offset: 0x000AD888
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

		// Token: 0x17000643 RID: 1603
		// (get) Token: 0x06001A84 RID: 6788 RVA: 0x000AE891 File Offset: 0x000AD891
		// (set) Token: 0x06001A85 RID: 6789 RVA: 0x000AE899 File Offset: 0x000AD899
		public ClientSpawnPointData ClientSpawnPointData
		{
			get
			{
				return this.clientSpawnPointData;
			}
			set
			{
				this.clientSpawnPointData = value;
			}
		}

		// Token: 0x06001A86 RID: 6790 RVA: 0x000AE8A4 File Offset: 0x000AD8A4
		public override void Pack(IMapObject mapObject)
		{
			base.Pack(mapObject);
			ClientSpawnPoint clientSpawnPoint = mapObject as ClientSpawnPoint;
			if (clientSpawnPoint != null)
			{
				this.visObject = clientSpawnPoint.VisObject;
				this.scene = clientSpawnPoint.Scene;
				this.scriptID = clientSpawnPoint.ScriptID;
				this.clientSpawnPointData = clientSpawnPoint.ClientSpawnPointData;
			}
		}

		// Token: 0x06001A87 RID: 6791 RVA: 0x000AE8F4 File Offset: 0x000AD8F4
		public override void Unpack(IMapObject mapObject)
		{
			base.Unpack(mapObject);
			ClientSpawnPoint clientSpawnPoint = mapObject as ClientSpawnPoint;
			if (clientSpawnPoint != null)
			{
				clientSpawnPoint.VisObject = this.visObject;
				clientSpawnPoint.Scene = this.scene;
				clientSpawnPoint.ScriptID = this.scriptID;
				clientSpawnPoint.ClientSpawnPointData = this.clientSpawnPointData;
			}
		}

		// Token: 0x0400111F RID: 4383
		private string visObject = string.Empty;

		// Token: 0x04001120 RID: 4384
		private string scene = string.Empty;

		// Token: 0x04001121 RID: 4385
		private string scriptID = string.Empty;

		// Token: 0x04001122 RID: 4386
		private ClientSpawnPointData clientSpawnPointData;
	}
}
