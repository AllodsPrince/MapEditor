using System;
using Tools.ValueCollector;

namespace MapEditor.Map.MapObjects
{
	// Token: 0x0200012D RID: 301
	public class SpawnPointTypeCollector : ValueCollector<SpawnPointType>
	{
		// Token: 0x06000EC9 RID: 3785 RVA: 0x000779D8 File Offset: 0x000769D8
		public SpawnPointTypeCollector() : base(SpawnPointData.DefaultSpawnPointType, SpawnPointType.Undefined, new SpawnPointTypeCollector.Comparer())
		{
		}

		// Token: 0x170002D1 RID: 721
		// (get) Token: 0x06000ECA RID: 3786 RVA: 0x000779EB File Offset: 0x000769EB
		public static SpawnPointType Undefined
		{
			get
			{
				return SpawnPointType.Undefined;
			}
		}

		// Token: 0x04000B8C RID: 2956
		private const SpawnPointType undefined = SpawnPointType.Undefined;

		// Token: 0x0200012E RID: 302
		private class Comparer : ValueCollector<SpawnPointType>.IComparer
		{
			// Token: 0x06000ECB RID: 3787 RVA: 0x000779EE File Offset: 0x000769EE
			public bool NotEquial(SpawnPointType value0, SpawnPointType value1)
			{
				return value0 != value1;
			}
		}
	}
}
