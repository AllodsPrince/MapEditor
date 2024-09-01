using System;
using Tools.MapObjects;

namespace MapEditor.Map.MapObjects
{
	// Token: 0x0200003A RID: 58
	public class ClientPatrolNodePack : SerializableMapObjectPack
	{
		// Token: 0x1700006F RID: 111
		// (get) Token: 0x0600036D RID: 877 RVA: 0x0001FACF File Offset: 0x0001EACF
		// (set) Token: 0x0600036E RID: 878 RVA: 0x0001FAD7 File Offset: 0x0001EAD7
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

		// Token: 0x17000070 RID: 112
		// (get) Token: 0x0600036F RID: 879 RVA: 0x0001FAE0 File Offset: 0x0001EAE0
		// (set) Token: 0x06000370 RID: 880 RVA: 0x0001FAE8 File Offset: 0x0001EAE8
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

		// Token: 0x06000371 RID: 881 RVA: 0x0001FAF4 File Offset: 0x0001EAF4
		public override void Pack(IMapObject mapObject)
		{
			base.Pack(mapObject);
			ClientPatrolNode clientPatrolNode = mapObject as ClientPatrolNode;
			if (clientPatrolNode != null)
			{
				this.scene = clientPatrolNode.Scene;
				this.scriptID = clientPatrolNode.ScriptID;
			}
		}

		// Token: 0x06000372 RID: 882 RVA: 0x0001FB2C File Offset: 0x0001EB2C
		public override void Unpack(IMapObject mapObject)
		{
			base.Unpack(mapObject);
			ClientPatrolNode clientPatrolNode = mapObject as ClientPatrolNode;
			if (clientPatrolNode != null)
			{
				clientPatrolNode.Scene = this.scene;
				clientPatrolNode.ScriptID = this.scriptID;
			}
		}

		// Token: 0x0400028F RID: 655
		private string scene = string.Empty;

		// Token: 0x04000290 RID: 656
		private string scriptID = string.Empty;
	}
}
