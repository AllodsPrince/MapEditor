using System;
using Tools.MapObjects;

namespace MapEditor.Map.MapObjects
{
	// Token: 0x02000154 RID: 340
	public class AstralBorderPack : SerializableMapObjectPack
	{
		// Token: 0x17000364 RID: 868
		// (get) Token: 0x0600106B RID: 4203 RVA: 0x0007B728 File Offset: 0x0007A728
		// (set) Token: 0x0600106C RID: 4204 RVA: 0x0007B730 File Offset: 0x0007A730
		public double StabilityRadius
		{
			get
			{
				return this.stabilityRadius;
			}
			set
			{
				this.stabilityRadius = value;
			}
		}

		// Token: 0x0600106D RID: 4205 RVA: 0x0007B73C File Offset: 0x0007A73C
		public override void Pack(IMapObject mapObject)
		{
			base.Pack(mapObject);
			AstralBorder astralBorder = mapObject as AstralBorder;
			if (astralBorder != null)
			{
				this.stabilityRadius = astralBorder.StabilityRadius;
			}
		}

		// Token: 0x0600106E RID: 4206 RVA: 0x0007B768 File Offset: 0x0007A768
		public override void Unpack(IMapObject mapObject)
		{
			base.Unpack(mapObject);
			AstralBorder astralBorder = mapObject as AstralBorder;
			if (astralBorder != null)
			{
				astralBorder.StabilityRadius = this.stabilityRadius;
			}
		}

		// Token: 0x04000C20 RID: 3104
		private double stabilityRadius;
	}
}
