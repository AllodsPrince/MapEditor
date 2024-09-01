using System;
using System.ComponentModel;
using Db;
using Tools.SafeObjMan;

namespace MapEditor.Map.MapObjectElements
{
	// Token: 0x02000288 RID: 648
	public class SpawnTimeRare : SpawnTimeAbstract
	{
		// Token: 0x06001E98 RID: 7832 RVA: 0x000C62D0 File Offset: 0x000C52D0
		public override void Load(IObjMan objectMan)
		{
			objectMan = SpawnTimeAbstract.GetSpawnTimeManipulator(objectMan);
			this.id = SafeObjMan.GetDBID(objectMan, "id");
			this.min = SafeObjMan.GetInt(objectMan, "timeRange.min");
			this.max = SafeObjMan.GetInt(objectMan, "timeRange.max");
		}

		// Token: 0x06001E99 RID: 7833 RVA: 0x000C6310 File Offset: 0x000C5310
		public override void Save(IObjMan objectMan)
		{
			if (objectMan != null)
			{
				objectMan.SetStructPtrInstance("spawnTime", "gameMechanics.elements.spawn.TimeRare");
				objectMan = SpawnTimeAbstract.GetSpawnTimeManipulator(objectMan);
				SafeObjMan.SetDBID(objectMan, "id", this.id);
				SafeObjMan.SetInt(objectMan, "timeRange.min", this.min);
				SafeObjMan.SetInt(objectMan, "timeRange.max", this.max);
			}
		}

		// Token: 0x06001E9A RID: 7834 RVA: 0x000C636C File Offset: 0x000C536C
		public override SpawnTimeAbstract Clone()
		{
			return new SpawnTimeRare
			{
				id = this.id,
				min = this.min,
				max = this.max
			};
		}

		// Token: 0x1700070B RID: 1803
		// (get) Token: 0x06001E9B RID: 7835 RVA: 0x000C63A4 File Offset: 0x000C53A4
		// (set) Token: 0x06001E9C RID: 7836 RVA: 0x000C63AC File Offset: 0x000C53AC
		[ReadOnly(true)]
		public string Id
		{
			get
			{
				return this.id;
			}
			set
			{
				this.id = value;
			}
		}

		// Token: 0x1700070C RID: 1804
		// (get) Token: 0x06001E9D RID: 7837 RVA: 0x000C63B5 File Offset: 0x000C53B5
		// (set) Token: 0x06001E9E RID: 7838 RVA: 0x000C63C0 File Offset: 0x000C53C0
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
				base.InvokeChangedEvent("TimeRareMin", _min, this.min);
			}
		}

		// Token: 0x1700070D RID: 1805
		// (get) Token: 0x06001E9F RID: 7839 RVA: 0x000C63F7 File Offset: 0x000C53F7
		// (set) Token: 0x06001EA0 RID: 7840 RVA: 0x000C6400 File Offset: 0x000C5400
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
				base.InvokeChangedEvent("TimeRareMax", _max, this.max);
			}
		}

		// Token: 0x0400131C RID: 4892
		public const string dbType = "gameMechanics.elements.spawn.TimeRare";

		// Token: 0x0400131D RID: 4893
		public const string minField = "TimeRareMin";

		// Token: 0x0400131E RID: 4894
		public const string maxField = "TimeRareMax";

		// Token: 0x0400131F RID: 4895
		private string id = string.Empty;

		// Token: 0x04001320 RID: 4896
		private int min;

		// Token: 0x04001321 RID: 4897
		private int max;
	}
}
