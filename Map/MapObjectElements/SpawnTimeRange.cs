using System;
using Db;
using Tools.SafeObjMan;

namespace MapEditor.Map.MapObjectElements
{
	// Token: 0x02000285 RID: 645
	public class SpawnTimeRange : SpawnTimeAbstract
	{
		// Token: 0x06001E82 RID: 7810 RVA: 0x000C5FE9 File Offset: 0x000C4FE9
		public override void Load(IObjMan objectMan)
		{
			objectMan = SpawnTimeAbstract.GetSpawnTimeManipulator(objectMan);
			this.skipFirstSpawn = SafeObjMan.GetBool(objectMan, "skipFirstSpawn");
			this.min = SafeObjMan.GetInt(objectMan, "range.min");
			this.max = SafeObjMan.GetInt(objectMan, "range.max");
		}

		// Token: 0x06001E83 RID: 7811 RVA: 0x000C6028 File Offset: 0x000C5028
		public override void Save(IObjMan objectMan)
		{
			if (objectMan != null)
			{
				objectMan.SetStructPtrInstance("spawnTime", "gameMechanics.elements.spawn.TimeRange");
				objectMan = SpawnTimeAbstract.GetSpawnTimeManipulator(objectMan);
				SafeObjMan.SetBool(objectMan, "skipFirstSpawn", this.skipFirstSpawn);
				SafeObjMan.SetInt(objectMan, "range.min", this.min);
				SafeObjMan.SetInt(objectMan, "range.max", this.max);
			}
		}

		// Token: 0x06001E84 RID: 7812 RVA: 0x000C6084 File Offset: 0x000C5084
		public override SpawnTimeAbstract Clone()
		{
			return new SpawnTimeRange
			{
				skipFirstSpawn = this.skipFirstSpawn,
				min = this.min,
				max = this.max
			};
		}

		// Token: 0x17000706 RID: 1798
		// (get) Token: 0x06001E85 RID: 7813 RVA: 0x000C60BC File Offset: 0x000C50BC
		// (set) Token: 0x06001E86 RID: 7814 RVA: 0x000C60C4 File Offset: 0x000C50C4
		public int Min
		{
			get
			{
				return this.min;
			}
			set
			{
				int _min = this.min;
				this.min = value;
				base.InvokeChangedEvent("Min", _min, this.min);
			}
		}

		// Token: 0x17000707 RID: 1799
		// (get) Token: 0x06001E87 RID: 7815 RVA: 0x000C60FB File Offset: 0x000C50FB
		// (set) Token: 0x06001E88 RID: 7816 RVA: 0x000C6104 File Offset: 0x000C5104
		public int Max
		{
			get
			{
				return this.max;
			}
			set
			{
				int _max = this.max;
				this.max = value;
				base.InvokeChangedEvent("Max", _max, this.max);
			}
		}

		// Token: 0x17000708 RID: 1800
		// (get) Token: 0x06001E89 RID: 7817 RVA: 0x000C613B File Offset: 0x000C513B
		// (set) Token: 0x06001E8A RID: 7818 RVA: 0x000C6144 File Offset: 0x000C5144
		public bool SkipFirstSpawn
		{
			get
			{
				return this.skipFirstSpawn;
			}
			set
			{
				bool _skipFirstSpawn = this.skipFirstSpawn;
				this.skipFirstSpawn = value;
				base.InvokeChangedEvent("RangeSkipFirstSpawn", _skipFirstSpawn, this.skipFirstSpawn);
			}
		}

		// Token: 0x0400130F RID: 4879
		public const string dbType = "gameMechanics.elements.spawn.TimeRange";

		// Token: 0x04001310 RID: 4880
		public const string skipFirstSpawnField = "RangeSkipFirstSpawn";

		// Token: 0x04001311 RID: 4881
		public const string minField = "Min";

		// Token: 0x04001312 RID: 4882
		public const string maxField = "Max";

		// Token: 0x04001313 RID: 4883
		private bool skipFirstSpawn;

		// Token: 0x04001314 RID: 4884
		private int min;

		// Token: 0x04001315 RID: 4885
		private int max;
	}
}
