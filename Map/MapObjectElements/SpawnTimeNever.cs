using System;
using Db;

namespace MapEditor.Map.MapObjectElements
{
	// Token: 0x0200028A RID: 650
	public class SpawnTimeNever : SpawnTimeAbstract
	{
		// Token: 0x06001EA6 RID: 7846 RVA: 0x000C6470 File Offset: 0x000C5470
		public override void Load(IObjMan objectMan)
		{
		}

		// Token: 0x06001EA7 RID: 7847 RVA: 0x000C6472 File Offset: 0x000C5472
		public override void Save(IObjMan objectMan)
		{
			if (objectMan != null)
			{
				objectMan.SetStructPtrInstance("spawnTime", "gameMechanics.elements.spawn.TimeNever");
			}
		}

		// Token: 0x06001EA8 RID: 7848 RVA: 0x000C6487 File Offset: 0x000C5487
		public override SpawnTimeAbstract Clone()
		{
			return new SpawnTimeNever();
		}

		// Token: 0x04001323 RID: 4899
		public const string dbType = "gameMechanics.elements.spawn.TimeNever";
	}
}
