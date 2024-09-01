using System;
using Tools.ValueCollector;

namespace MapEditor.Map.MapObjects
{
	// Token: 0x0200017A RID: 378
	public class SoundCollector : ValueCollector<Sound>
	{
		// Token: 0x0600123F RID: 4671 RVA: 0x00084CF1 File Offset: 0x00083CF1
		public SoundCollector() : base(null, Sound.Empty, new SoundCollector.Comparer())
		{
		}

		// Token: 0x170003B0 RID: 944
		// (get) Token: 0x06001240 RID: 4672 RVA: 0x00084D04 File Offset: 0x00083D04
		public static Sound Undefined
		{
			get
			{
				return Sound.Empty;
			}
		}

		// Token: 0x0200017B RID: 379
		private class Comparer : ValueCollector<Sound>.IComparer
		{
			// Token: 0x06001241 RID: 4673 RVA: 0x00084D0B File Offset: 0x00083D0B
			public bool NotEquial(Sound value0, Sound value1)
			{
				return (value0 != null || value1 != null) && (value0 == null || value1 == null || string.Compare(value0.ToString(), value1.ToString(), true) != 0);
			}
		}
	}
}
