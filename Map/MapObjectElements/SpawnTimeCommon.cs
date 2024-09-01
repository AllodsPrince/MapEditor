using System;
using Db;
using Tools.SafeObjMan;

namespace MapEditor.Map.MapObjectElements
{
	// Token: 0x02000283 RID: 643
	public class SpawnTimeCommon : SpawnTimeAbstract
	{
		// Token: 0x17000705 RID: 1797
		// (get) Token: 0x06001E78 RID: 7800 RVA: 0x000C5F06 File Offset: 0x000C4F06
		// (set) Token: 0x06001E79 RID: 7801 RVA: 0x000C5F10 File Offset: 0x000C4F10
		public float Factor
		{
			get
			{
				return this.factor;
			}
			set
			{
				float _factor = this.factor;
				this.factor = value;
				base.InvokeChangedEvent("Factor", _factor, this.factor);
			}
		}

		// Token: 0x06001E7A RID: 7802 RVA: 0x000C5F47 File Offset: 0x000C4F47
		public override void Load(IObjMan objectMan)
		{
			objectMan = SpawnTimeAbstract.GetSpawnTimeManipulator(objectMan);
			this.factor = SafeObjMan.GetFloat(objectMan, "factor");
		}

		// Token: 0x06001E7B RID: 7803 RVA: 0x000C5F62 File Offset: 0x000C4F62
		public override void Save(IObjMan objectMan)
		{
			if (objectMan != null)
			{
				objectMan.SetStructPtrInstance("spawnTime", "gameMechanics.elements.spawn.TimeCommon");
				objectMan = SpawnTimeAbstract.GetSpawnTimeManipulator(objectMan);
				SafeObjMan.SetFloat(objectMan, "factor", this.factor);
			}
		}

		// Token: 0x06001E7C RID: 7804 RVA: 0x000C5F90 File Offset: 0x000C4F90
		public override SpawnTimeAbstract Clone()
		{
			return new SpawnTimeCommon
			{
				factor = this.factor
			};
		}

		// Token: 0x0400130B RID: 4875
		public const string dbType = "gameMechanics.elements.spawn.TimeCommon";

		// Token: 0x0400130C RID: 4876
		public const string factorField = "Factor";

		// Token: 0x0400130D RID: 4877
		private float factor = 1f;
	}
}
