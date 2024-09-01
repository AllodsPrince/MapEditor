using System;
using Tools.ValueCollector;

namespace MapEditor.Map.MapObjects
{
	// Token: 0x0200012F RID: 303
	public class SpawnTableTypeCollector : ValueCollector<SpawnTableType>
	{
		// Token: 0x06000ECD RID: 3789 RVA: 0x000779FF File Offset: 0x000769FF
		public SpawnTableTypeCollector() : base(SpawnPoint.DefaultSpawnTableType, SpawnTableType.Undefined, new SpawnTableTypeCollector.Comparer())
		{
		}

		// Token: 0x170002D2 RID: 722
		// (get) Token: 0x06000ECE RID: 3790 RVA: 0x00077A12 File Offset: 0x00076A12
		public static SpawnTableType Undefined
		{
			get
			{
				return SpawnTableType.Undefined;
			}
		}

		// Token: 0x04000B8D RID: 2957
		private const SpawnTableType undefined = SpawnTableType.Undefined;

		// Token: 0x02000130 RID: 304
		private class Comparer : ValueCollector<SpawnTableType>.IComparer
		{
			// Token: 0x06000ECF RID: 3791 RVA: 0x00077A15 File Offset: 0x00076A15
			public bool NotEquial(SpawnTableType value0, SpawnTableType value1)
			{
				return value0 != value1;
			}
		}
	}
}
