using System;

namespace MapEditor.Map.MapObjects
{
	// Token: 0x02000133 RID: 307
	public class GuardSpawnPointData : SpawnPointData
	{
		// Token: 0x06000EE0 RID: 3808 RVA: 0x00077B00 File Offset: 0x00076B00
		public GuardSpawnPointData() : base(null, SpawnPointType.Guard)
		{
		}

		// Token: 0x06000EE1 RID: 3809 RVA: 0x00077B0A File Offset: 0x00076B0A
		public GuardSpawnPointData(SpawnPoint _spawnPoint) : base(_spawnPoint, SpawnPointType.Guard)
		{
		}

		// Token: 0x06000EE2 RID: 3810 RVA: 0x00077B14 File Offset: 0x00076B14
		public override void CopyFrom(SpawnPointData spawnPointData)
		{
			if (spawnPointData != null && spawnPointData.SpawnPointType == SpawnPointType.Guard)
			{
				GuardSpawnPointData guardSpawnPointData = spawnPointData as GuardSpawnPointData;
			}
		}
	}
}
