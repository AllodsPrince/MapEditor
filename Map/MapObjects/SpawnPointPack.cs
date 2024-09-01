using System;
using MapEditor.Map.MapObjectElements;
using Tools.MapObjects;

namespace MapEditor.Map.MapObjects
{
	// Token: 0x0200013C RID: 316
	public class SpawnPointPack : SerializableMapObjectPack
	{
		// Token: 0x170002E2 RID: 738
		// (get) Token: 0x06000F22 RID: 3874 RVA: 0x00078327 File Offset: 0x00077327
		// (set) Token: 0x06000F23 RID: 3875 RVA: 0x0007832F File Offset: 0x0007732F
		public string SpawnTable
		{
			get
			{
				return this.spawnTable;
			}
			set
			{
				this.spawnTable = value;
			}
		}

		// Token: 0x170002E3 RID: 739
		// (get) Token: 0x06000F24 RID: 3876 RVA: 0x00078338 File Offset: 0x00077338
		// (set) Token: 0x06000F25 RID: 3877 RVA: 0x00078340 File Offset: 0x00077340
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

		// Token: 0x170002E4 RID: 740
		// (get) Token: 0x06000F26 RID: 3878 RVA: 0x00078349 File Offset: 0x00077349
		// (set) Token: 0x06000F27 RID: 3879 RVA: 0x00078351 File Offset: 0x00077351
		public string SpawnID
		{
			get
			{
				return this.spawnID;
			}
			set
			{
				this.spawnID = value;
			}
		}

		// Token: 0x170002E5 RID: 741
		// (get) Token: 0x06000F28 RID: 3880 RVA: 0x0007835A File Offset: 0x0007735A
		// (set) Token: 0x06000F29 RID: 3881 RVA: 0x00078362 File Offset: 0x00077362
		public double ScanRadius
		{
			get
			{
				return this.scanRadius;
			}
			set
			{
				this.scanRadius = value;
			}
		}

		// Token: 0x170002E6 RID: 742
		// (get) Token: 0x06000F2A RID: 3882 RVA: 0x0007836B File Offset: 0x0007736B
		// (set) Token: 0x06000F2B RID: 3883 RVA: 0x00078373 File Offset: 0x00077373
		public SpawnPointData SpawnPointData
		{
			get
			{
				return this.spawnPointData;
			}
			set
			{
				this.spawnPointData = value;
			}
		}

		// Token: 0x170002E7 RID: 743
		// (get) Token: 0x06000F2C RID: 3884 RVA: 0x0007837C File Offset: 0x0007737C
		// (set) Token: 0x06000F2D RID: 3885 RVA: 0x00078384 File Offset: 0x00077384
		public SpawnTimeAbstract SpawnTime
		{
			get
			{
				return this.spawnTime;
			}
			set
			{
				this.spawnTime = value;
			}
		}

		// Token: 0x170002E8 RID: 744
		// (get) Token: 0x06000F2E RID: 3886 RVA: 0x0007838D File Offset: 0x0007738D
		// (set) Token: 0x06000F2F RID: 3887 RVA: 0x00078395 File Offset: 0x00077395
		public string SpawnTuner
		{
			get
			{
				return this.spawnTuner;
			}
			set
			{
				this.spawnTuner = value;
			}
		}

		// Token: 0x170002E9 RID: 745
		// (get) Token: 0x06000F30 RID: 3888 RVA: 0x0007839E File Offset: 0x0007739E
		// (set) Token: 0x06000F31 RID: 3889 RVA: 0x000783A6 File Offset: 0x000773A6
		public SingleSpawnController SingleSpawnController
		{
			get
			{
				return this.singleSpawnController;
			}
			set
			{
				this.singleSpawnController = value;
			}
		}

		// Token: 0x06000F32 RID: 3890 RVA: 0x000783B0 File Offset: 0x000773B0
		public override void Pack(IMapObject mapObject)
		{
			base.Pack(mapObject);
			SpawnPoint spawnPoint = mapObject as SpawnPoint;
			if (spawnPoint != null)
			{
				this.spawnTable = spawnPoint.SpawnTable;
				this.scriptID = spawnPoint.ScriptID;
				this.spawnID = spawnPoint.SpawnID;
				this.scanRadius = spawnPoint.ScanRadius;
				this.spawnPointData = spawnPoint.SpawnPointData;
				this.spawnTime = spawnPoint.SpawnTime;
				this.spawnTuner = spawnPoint.SpawnTuner;
				this.singleSpawnController = spawnPoint.SingleSpawnController;
			}
		}

		// Token: 0x06000F33 RID: 3891 RVA: 0x00078430 File Offset: 0x00077430
		public override void Unpack(IMapObject mapObject)
		{
			base.Unpack(mapObject);
			SpawnPoint spawnPoint = mapObject as SpawnPoint;
			if (spawnPoint != null)
			{
				spawnPoint.SpawnTable = this.spawnTable;
				spawnPoint.ScriptID = this.scriptID;
				spawnPoint.SpawnID = this.spawnID;
				spawnPoint.ScanRadius = this.scanRadius;
				spawnPoint.SpawnPointData = this.spawnPointData;
				spawnPoint.SpawnTime = this.spawnTime;
				spawnPoint.SpawnTuner = this.spawnTuner;
				spawnPoint.SingleSpawnController = this.singleSpawnController;
			}
		}

		// Token: 0x04000BA5 RID: 2981
		private string spawnTable = string.Empty;

		// Token: 0x04000BA6 RID: 2982
		private string scriptID = string.Empty;

		// Token: 0x04000BA7 RID: 2983
		private string spawnID = string.Empty;

		// Token: 0x04000BA8 RID: 2984
		private double scanRadius;

		// Token: 0x04000BA9 RID: 2985
		private SpawnPointData spawnPointData;

		// Token: 0x04000BAA RID: 2986
		private SpawnTimeAbstract spawnTime;

		// Token: 0x04000BAB RID: 2987
		private string spawnTuner;

		// Token: 0x04000BAC RID: 2988
		private SingleSpawnController singleSpawnController;
	}
}
