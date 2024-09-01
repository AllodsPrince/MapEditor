using System;
using Db;

namespace MapEditor.Map.MapObjectElements
{
	// Token: 0x02000287 RID: 647
	public class SpawnTimePatrol : SpawnTimeAbstract
	{
		// Token: 0x06001E94 RID: 7828 RVA: 0x000C62AA File Offset: 0x000C52AA
		public override void Load(IObjMan objectMan)
		{
		}

		// Token: 0x06001E95 RID: 7829 RVA: 0x000C62AC File Offset: 0x000C52AC
		public override void Save(IObjMan objectMan)
		{
			if (objectMan != null)
			{
				objectMan.SetStructPtrInstance("spawnTime", "gameMechanics.elements.spawn.TimePatrol");
			}
		}

		// Token: 0x06001E96 RID: 7830 RVA: 0x000C62C1 File Offset: 0x000C52C1
		public override SpawnTimeAbstract Clone()
		{
			return new SpawnTimePatrol();
		}

		// Token: 0x0400131B RID: 4891
		public const string dbType = "gameMechanics.elements.spawn.TimePatrol";
	}
}
