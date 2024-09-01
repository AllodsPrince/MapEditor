using System;
using Db;

namespace MapEditor.Map.MapObjectElements
{
	// Token: 0x02000289 RID: 649
	public class SpawnTimeTrash : SpawnTimeAbstract
	{
		// Token: 0x06001EA2 RID: 7842 RVA: 0x000C644A File Offset: 0x000C544A
		public override void Load(IObjMan objectMan)
		{
		}

		// Token: 0x06001EA3 RID: 7843 RVA: 0x000C644C File Offset: 0x000C544C
		public override void Save(IObjMan objectMan)
		{
			if (objectMan != null)
			{
				objectMan.SetStructPtrInstance("spawnTime", "gameMechanics.elements.spawn.TimeTrash");
			}
		}

		// Token: 0x06001EA4 RID: 7844 RVA: 0x000C6461 File Offset: 0x000C5461
		public override SpawnTimeAbstract Clone()
		{
			return new SpawnTimeTrash();
		}

		// Token: 0x04001322 RID: 4898
		public const string dbType = "gameMechanics.elements.spawn.TimeTrash";
	}
}
