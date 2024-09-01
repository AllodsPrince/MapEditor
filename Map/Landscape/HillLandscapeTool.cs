using System;
using Tools.Geometry;
using Tools.Landscape;
using Tools.Landscape.LandscapeToolParams;

namespace MapEditor.Map.Landscape
{
	// Token: 0x02000262 RID: 610
	public class HillLandscapeTool : LandscapeTool
	{
		// Token: 0x06001CE5 RID: 7397 RVA: 0x000B8678 File Offset: 0x000B7678
		private void Apply(Polygon polygon)
		{
			HillLandscapeToolParams hillLandscapeToolParams = base.LandscapeToolParams as HillLandscapeToolParams;
			if (hillLandscapeToolParams != null)
			{
				if (this.nextOperation == HillLandscapeTool.Operation.Init)
				{
					Vec3 mapOffset = Vec3.Empty;
					if (hillLandscapeToolParams.TwoSided)
					{
						if (this.initialized)
						{
							base.LandscapeToolContext.EditorScene.ResetHillMaker(0);
							base.LandscapeToolContext.EditorScene.ResetHillMaker(1);
							base.LandscapeRegion.Update();
						}
						base.LandscapeToolContext.EditorScene.InitHillMaker(0, ref mapOffset, polygon, hillLandscapeToolParams.TerrainHillParams);
						base.LandscapeToolContext.EditorScene.ApplyHillMaker(0, hillLandscapeToolParams.TerrainHillParams);
						base.LandscapeToolContext.EditorScene.InitHillMaker(1, ref mapOffset, polygon, hillLandscapeToolParams.BottomHillParams);
						base.LandscapeToolContext.EditorScene.ApplyHillMaker(1, hillLandscapeToolParams.BottomHillParams);
					}
					else
					{
						if (this.initialized)
						{
							base.LandscapeToolContext.EditorScene.ResetHillMaker();
							base.LandscapeRegion.Update();
						}
						base.LandscapeToolContext.EditorScene.InitHillMaker(ref mapOffset, polygon, hillLandscapeToolParams.TerrainHillParams);
						base.LandscapeToolContext.EditorScene.ApplyHillMaker(hillLandscapeToolParams.TerrainHillParams);
					}
					this.somethingChanged = true;
					this.initialized = true;
					return;
				}
				if (this.nextOperation == HillLandscapeTool.Operation.Apply)
				{
					if (this.initialized)
					{
						if (hillLandscapeToolParams.TwoSided)
						{
							base.LandscapeToolContext.EditorScene.ApplyHillMaker(0, hillLandscapeToolParams.TerrainHillParams);
							base.LandscapeToolContext.EditorScene.ApplyHillMaker(1, hillLandscapeToolParams.BottomHillParams);
						}
						else
						{
							base.LandscapeToolContext.EditorScene.ApplyHillMaker(hillLandscapeToolParams.TerrainHillParams);
						}
						this.somethingChanged = true;
						return;
					}
				}
				else
				{
					if (this.nextOperation == HillLandscapeTool.Operation.Reset)
					{
						if (hillLandscapeToolParams.TwoSided)
						{
							base.LandscapeToolContext.EditorScene.ResetHillMaker(0);
							base.LandscapeToolContext.EditorScene.ResetHillMaker(1);
						}
						else
						{
							base.LandscapeToolContext.EditorScene.ResetHillMaker();
						}
						this.initialized = false;
						this.somethingChanged = true;
						return;
					}
					if (this.nextOperation == HillLandscapeTool.Operation.Complete)
					{
						this.initialized = false;
					}
				}
			}
		}

		// Token: 0x06001CE6 RID: 7398 RVA: 0x000B887B File Offset: 0x000B787B
		public HillLandscapeTool(int _id) : base(_id)
		{
		}

		// Token: 0x06001CE7 RID: 7399 RVA: 0x000B8884 File Offset: 0x000B7884
		public override void Create()
		{
		}

		// Token: 0x06001CE8 RID: 7400 RVA: 0x000B8888 File Offset: 0x000B7888
		public override void Apply()
		{
			this.somethingChanged = false;
			if (base.LandscapeToolContext != null)
			{
				if (base.LandscapeRegion.LandscapeRegionParams.Type == LandscapeRegionType.Polygon)
				{
					this.Apply(base.LandscapeRegion.Polygon);
					return;
				}
				if (base.LandscapeRegion.LandscapeRegionParams.Type == LandscapeRegionType.Stripe)
				{
					this.Apply(base.LandscapeRegion.Stripe.Bounds);
				}
			}
		}

		// Token: 0x06001CE9 RID: 7401 RVA: 0x000B88F2 File Offset: 0x000B78F2
		public override void Destroy()
		{
		}

		// Token: 0x170006E3 RID: 1763
		// (get) Token: 0x06001CEA RID: 7402 RVA: 0x000B88F4 File Offset: 0x000B78F4
		// (set) Token: 0x06001CEB RID: 7403 RVA: 0x000B88FC File Offset: 0x000B78FC
		public HillLandscapeTool.Operation NextOperation
		{
			get
			{
				return this.nextOperation;
			}
			set
			{
				this.nextOperation = value;
			}
		}

		// Token: 0x170006E4 RID: 1764
		// (get) Token: 0x06001CEC RID: 7404 RVA: 0x000B8905 File Offset: 0x000B7905
		// (set) Token: 0x06001CED RID: 7405 RVA: 0x000B890D File Offset: 0x000B790D
		public bool SomethingChanged
		{
			get
			{
				return this.somethingChanged;
			}
			set
			{
				this.somethingChanged = value;
			}
		}

		// Token: 0x04001265 RID: 4709
		private HillLandscapeTool.Operation nextOperation;

		// Token: 0x04001266 RID: 4710
		private bool somethingChanged;

		// Token: 0x04001267 RID: 4711
		private bool initialized;

		// Token: 0x02000263 RID: 611
		public enum Operation
		{
			// Token: 0x04001269 RID: 4713
			Init,
			// Token: 0x0400126A RID: 4714
			Apply,
			// Token: 0x0400126B RID: 4715
			Reset,
			// Token: 0x0400126C RID: 4716
			Complete
		}
	}
}
