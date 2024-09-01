using System;
using Tools.MapObjects;

namespace MapEditor.Map.MapObjects
{
	// Token: 0x02000253 RID: 595
	public class PatrolNodePack : SerializableMapObjectPack
	{
		// Token: 0x170006A6 RID: 1702
		// (get) Token: 0x06001C4C RID: 7244 RVA: 0x000B63E3 File Offset: 0x000B53E3
		// (set) Token: 0x06001C4D RID: 7245 RVA: 0x000B63EB File Offset: 0x000B53EB
		public string Script
		{
			get
			{
				return this.script;
			}
			set
			{
				this.script = value;
			}
		}

		// Token: 0x06001C4E RID: 7246 RVA: 0x000B63F4 File Offset: 0x000B53F4
		public override void Pack(IMapObject mapObject)
		{
			base.Pack(mapObject);
			PatrolNode patrolNode = mapObject as PatrolNode;
			if (patrolNode != null)
			{
				this.script = patrolNode.Script;
			}
		}

		// Token: 0x06001C4F RID: 7247 RVA: 0x000B6420 File Offset: 0x000B5420
		public override void Unpack(IMapObject mapObject)
		{
			base.Unpack(mapObject);
			PatrolNode patrolNode = mapObject as PatrolNode;
			if (patrolNode != null)
			{
				patrolNode.Script = this.script;
			}
		}

		// Token: 0x0400122F RID: 4655
		private string script = string.Empty;
	}
}
