using System;
using Db;

namespace MapEditor.Map.MapObjectElements
{
	// Token: 0x0200028B RID: 651
	public class SpawnTimeQuest : SpawnTimeAbstract
	{
		// Token: 0x06001EAA RID: 7850 RVA: 0x000C6496 File Offset: 0x000C5496
		public override void Load(IObjMan objectMan)
		{
		}

		// Token: 0x06001EAB RID: 7851 RVA: 0x000C6498 File Offset: 0x000C5498
		public override void Save(IObjMan objectMan)
		{
			if (objectMan != null)
			{
				objectMan.SetStructPtrInstance("spawnTime", "gameMechanics.elements.spawn.TimeQuest");
			}
		}

		// Token: 0x06001EAC RID: 7852 RVA: 0x000C64B0 File Offset: 0x000C54B0
		public override SpawnTimeAbstract Clone()
		{
			return new SpawnTimeQuest
			{
				skipFirstSpawn = this.skipFirstSpawn
			};
		}

		// Token: 0x1700070E RID: 1806
		// (get) Token: 0x06001EAD RID: 7853 RVA: 0x000C64D0 File Offset: 0x000C54D0
		// (set) Token: 0x06001EAE RID: 7854 RVA: 0x000C64D8 File Offset: 0x000C54D8
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
				base.InvokeChangedEvent("QuestSkipFirstSpawn", _skipFirstSpawn, this.skipFirstSpawn);
			}
		}

		// Token: 0x04001324 RID: 4900
		public const string dbType = "gameMechanics.elements.spawn.TimeQuest";

		// Token: 0x04001325 RID: 4901
		public const string skipFirstSpawnField = "QuestSkipFirstSpawn";

		// Token: 0x04001326 RID: 4902
		private bool skipFirstSpawn;
	}
}
