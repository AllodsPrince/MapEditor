using System;
using Tools.Landscape;

namespace MapEditor.Map.Landscape
{
	// Token: 0x02000204 RID: 516
	public class GradientLandscapeTool : LandscapeTool
	{
		// Token: 0x0600197A RID: 6522 RVA: 0x000A6F86 File Offset: 0x000A5F86
		public GradientLandscapeTool(int _id) : base(_id)
		{
			base.AffectParams.AffectTile = true;
		}

		// Token: 0x0600197B RID: 6523 RVA: 0x000A6F9B File Offset: 0x000A5F9B
		public override void Create()
		{
		}

		// Token: 0x0600197C RID: 6524 RVA: 0x000A6F9D File Offset: 0x000A5F9D
		public override void Apply()
		{
		}

		// Token: 0x0600197D RID: 6525 RVA: 0x000A6F9F File Offset: 0x000A5F9F
		public override void Destroy()
		{
		}
	}
}
