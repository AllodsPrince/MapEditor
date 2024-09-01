using System;

namespace MapEditor.Map.MapObjects
{
	// Token: 0x02000229 RID: 553
	public class MobClientSpawnPointData : ClientSpawnPointData
	{
		// Token: 0x17000645 RID: 1605
		// (get) Token: 0x06001A93 RID: 6803 RVA: 0x000AE9F3 File Offset: 0x000AD9F3
		public override string Type
		{
			get
			{
				return "VisualMob";
			}
		}

		// Token: 0x04001125 RID: 4389
		public const string type = "VisualMob";
	}
}
