using System;

namespace MapEditor.Map.MapObjectElements
{
	// Token: 0x0200013F RID: 319
	public interface SpawnTimeOwner
	{
		// Token: 0x06000F4A RID: 3914
		bool ValidSpawnTimeOwnerInstance();

		// Token: 0x06000F4B RID: 3915
		void CheckSpawnTimeRareId(SpawnTimeAbstract spawnTime);
	}
}
