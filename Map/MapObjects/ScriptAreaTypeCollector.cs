using System;
using Tools.ValueCollector;

namespace MapEditor.Map.MapObjects
{
	// Token: 0x020001CC RID: 460
	public class ScriptAreaTypeCollector : ValueCollector<ScriptAreaType>
	{
		// Token: 0x0600179F RID: 6047 RVA: 0x000A1129 File Offset: 0x000A0129
		public ScriptAreaTypeCollector() : base(ScriptAreaData.DefaultScriptAreaType, ScriptAreaTypeCollector.undefined, new ScriptAreaTypeCollector.Comparer())
		{
		}

		// Token: 0x170005AA RID: 1450
		// (get) Token: 0x060017A0 RID: 6048 RVA: 0x000A1140 File Offset: 0x000A0140
		public static ScriptAreaType Undefined
		{
			get
			{
				return ScriptAreaTypeCollector.undefined;
			}
		}

		// Token: 0x04000F74 RID: 3956
		private static readonly ScriptAreaType undefined;

		// Token: 0x020001CD RID: 461
		private class Comparer : ValueCollector<ScriptAreaType>.IComparer
		{
			// Token: 0x060017A1 RID: 6049 RVA: 0x000A1147 File Offset: 0x000A0147
			public bool NotEquial(ScriptAreaType value0, ScriptAreaType value1)
			{
				return value0 != value1;
			}
		}
	}
}
