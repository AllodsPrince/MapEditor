using System;
using Db;

namespace MapEditor.Map.MapObjectElements
{
	// Token: 0x02000284 RID: 644
	public class SpawnTimeOnce : SpawnTimeAbstract
	{
		// Token: 0x06001E7E RID: 7806 RVA: 0x000C5FC3 File Offset: 0x000C4FC3
		public override void Load(IObjMan objectMan)
		{
		}

		// Token: 0x06001E7F RID: 7807 RVA: 0x000C5FC5 File Offset: 0x000C4FC5
		public override void Save(IObjMan objectMan)
		{
			if (objectMan != null)
			{
				objectMan.SetStructPtrInstance("spawnTime", "gameMechanics.elements.spawn.TimeOnce");
			}
		}

		// Token: 0x06001E80 RID: 7808 RVA: 0x000C5FDA File Offset: 0x000C4FDA
		public override SpawnTimeAbstract Clone()
		{
			return new SpawnTimeOnce();
		}

		// Token: 0x0400130E RID: 4878
		public const string dbType = "gameMechanics.elements.spawn.TimeOnce";
	}
}
