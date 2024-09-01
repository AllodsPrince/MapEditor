using System;
using Db;
using Tools.SafeObjMan;

namespace MapEditor.Map.MapObjectElements
{
	// Token: 0x02000286 RID: 646
	public class SpawnTimeSimple : SpawnTimeAbstract
	{
		// Token: 0x06001E8C RID: 7820 RVA: 0x000C6183 File Offset: 0x000C5183
		public override void Load(IObjMan objectMan)
		{
			objectMan = SpawnTimeAbstract.GetSpawnTimeManipulator(objectMan);
			this.skipFirstSpawn = SafeObjMan.GetBool(objectMan, "skipFirstSpawn");
			this.time = SafeObjMan.GetInt(objectMan, "time");
		}

		// Token: 0x06001E8D RID: 7821 RVA: 0x000C61AF File Offset: 0x000C51AF
		public override void Save(IObjMan objectMan)
		{
			if (objectMan != null)
			{
				objectMan.SetStructPtrInstance("spawnTime", "gameMechanics.elements.spawn.TimeSimple");
				objectMan = SpawnTimeAbstract.GetSpawnTimeManipulator(objectMan);
				SafeObjMan.SetBool(objectMan, "skipFirstSpawn", this.skipFirstSpawn);
				SafeObjMan.SetInt(objectMan, "time", this.time);
			}
		}

		// Token: 0x06001E8E RID: 7822 RVA: 0x000C61F0 File Offset: 0x000C51F0
		public override SpawnTimeAbstract Clone()
		{
			return new SpawnTimeSimple
			{
				skipFirstSpawn = this.skipFirstSpawn,
				time = this.time
			};
		}

		// Token: 0x17000709 RID: 1801
		// (get) Token: 0x06001E8F RID: 7823 RVA: 0x000C621C File Offset: 0x000C521C
		// (set) Token: 0x06001E90 RID: 7824 RVA: 0x000C6224 File Offset: 0x000C5224
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
				base.InvokeChangedEvent("SimpleSkipFirstSpawn", _skipFirstSpawn, this.skipFirstSpawn);
			}
		}

		// Token: 0x1700070A RID: 1802
		// (get) Token: 0x06001E91 RID: 7825 RVA: 0x000C625B File Offset: 0x000C525B
		// (set) Token: 0x06001E92 RID: 7826 RVA: 0x000C6264 File Offset: 0x000C5264
		public int Time
		{
			get
			{
				return this.time;
			}
			set
			{
				int _time = this.time;
				this.time = value;
				base.InvokeChangedEvent("SimpleTime", _time, this.time);
			}
		}

		// Token: 0x04001316 RID: 4886
		public const string dbType = "gameMechanics.elements.spawn.TimeSimple";

		// Token: 0x04001317 RID: 4887
		public const string skipFirstSpawnField = "SimpleSkipFirstSpawn";

		// Token: 0x04001318 RID: 4888
		public const string timeField = "SimpleTime";

		// Token: 0x04001319 RID: 4889
		private bool skipFirstSpawn;

		// Token: 0x0400131A RID: 4890
		private int time = -1;
	}
}
