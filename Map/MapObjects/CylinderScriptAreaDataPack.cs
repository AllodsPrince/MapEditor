using System;

namespace MapEditor.Map.MapObjects
{
	// Token: 0x020001D2 RID: 466
	public class CylinderScriptAreaDataPack
	{
		// Token: 0x170005B2 RID: 1458
		// (get) Token: 0x060017C3 RID: 6083 RVA: 0x000A1445 File Offset: 0x000A0445
		// (set) Token: 0x060017C4 RID: 6084 RVA: 0x000A144D File Offset: 0x000A044D
		public double Radius
		{
			get
			{
				return this.radius;
			}
			set
			{
				this.radius = value;
			}
		}

		// Token: 0x170005B3 RID: 1459
		// (get) Token: 0x060017C5 RID: 6085 RVA: 0x000A1456 File Offset: 0x000A0456
		// (set) Token: 0x060017C6 RID: 6086 RVA: 0x000A145E File Offset: 0x000A045E
		public double Halfheight
		{
			get
			{
				return this.halfheight;
			}
			set
			{
				this.halfheight = value;
			}
		}

		// Token: 0x060017C7 RID: 6087 RVA: 0x000A1468 File Offset: 0x000A0468
		public void Pack(ScriptAreaData scriptAreaData)
		{
			CylinderScriptAreaData sylinderScriptAreaData = scriptAreaData as CylinderScriptAreaData;
			if (sylinderScriptAreaData != null)
			{
				this.radius = sylinderScriptAreaData.Radius;
				this.halfheight = sylinderScriptAreaData.Halfheight;
			}
		}

		// Token: 0x060017C8 RID: 6088 RVA: 0x000A1498 File Offset: 0x000A0498
		public void Unpack(ScriptAreaData scriptAreaData)
		{
			CylinderScriptAreaData sylinderScriptAreaData = scriptAreaData as CylinderScriptAreaData;
			if (sylinderScriptAreaData != null)
			{
				sylinderScriptAreaData.Radius = this.radius;
				sylinderScriptAreaData.Halfheight = this.halfheight;
			}
		}

		// Token: 0x04000F7F RID: 3967
		private double radius = CylinderScriptAreaData.DefaultRadius;

		// Token: 0x04000F80 RID: 3968
		private double halfheight = CylinderScriptAreaData.DefaultHalfheight;
	}
}
