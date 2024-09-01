using System;
using Tools.Geometry;
using Tools.Landscape;
using Tools.Landscape.LandscapeToolParams;

namespace MapEditor.Map.Landscape
{
	// Token: 0x0200010E RID: 270
	public class WaterLandscapeTool : LandscapeTool
	{
		// Token: 0x06000D36 RID: 3382 RVA: 0x0006EC28 File Offset: 0x0006DC28
		public WaterLandscapeTool(int _id) : base(_id)
		{
		}

		// Token: 0x06000D37 RID: 3383 RVA: 0x0006EC38 File Offset: 0x0006DC38
		public override void Create()
		{
			this.Destroy();
			WaterLandscapeToolParams waterLandscapeToolParams = base.LandscapeToolParams as WaterLandscapeToolParams;
			if (waterLandscapeToolParams != null)
			{
				if ((waterLandscapeToolParams.WaterToolType == LandscapeWaterToolType.Add && !KeyStatus.Control) || (waterLandscapeToolParams.WaterToolType == LandscapeWaterToolType.Remove && KeyStatus.Control))
				{
					if (waterLandscapeToolParams.LayerIndex != -1)
					{
						this.editorSceneLandscapeToolID = base.LandscapeToolContext.EditorScene.CreateMakeWaterLandscapeTool(true, waterLandscapeToolParams.LayerIndex, waterLandscapeToolParams.Height);
						return;
					}
				}
				else
				{
					if ((waterLandscapeToolParams.WaterToolType == LandscapeWaterToolType.Remove && !KeyStatus.Control) || (waterLandscapeToolParams.WaterToolType == LandscapeWaterToolType.Add && KeyStatus.Control))
					{
						this.editorSceneLandscapeToolID = base.LandscapeToolContext.EditorScene.CreateMakeWaterLandscapeTool(false, waterLandscapeToolParams.LayerIndex, waterLandscapeToolParams.Height);
						return;
					}
					if (waterLandscapeToolParams.WaterToolType == LandscapeWaterToolType.MoveToHeight || waterLandscapeToolParams.WaterToolType == LandscapeWaterToolType.PickAndMoveToHeight)
					{
						this.editorSceneLandscapeToolID = base.LandscapeToolContext.EditorScene.CreateModifyWaterHeightLandscapeTool(waterLandscapeToolParams.Height);
					}
				}
			}
		}

		// Token: 0x06000D38 RID: 3384 RVA: 0x0006ED20 File Offset: 0x0006DD20
		public override void Apply()
		{
			if (this.editorSceneLandscapeToolID != -1)
			{
				Point position = base.LandscapeToolContext.LandscapeToolContextPosition.TerrainPosition;
				WaterLandscapeToolParams waterLandscapeToolParams = base.LandscapeToolParams as WaterLandscapeToolParams;
				if (waterLandscapeToolParams != null && waterLandscapeToolParams.WaterToolType == LandscapeWaterToolType.PickAndMoveToHeight)
				{
					base.LandscapeToolContext.EditorScene.SetWaterHeight_ModifyWaterHeightLandscapeTool(this.editorSceneLandscapeToolID, waterLandscapeToolParams.Height);
				}
				Rect affectedRect;
				base.LandscapeToolContext.EditorScene.ApplyWaterLandscapeTool(this.editorSceneLandscapeToolID, ref position, out affectedRect);
				base.AffectParams.AffectedRect = affectedRect;
			}
		}

		// Token: 0x06000D39 RID: 3385 RVA: 0x0006EDA1 File Offset: 0x0006DDA1
		public override void Destroy()
		{
			if (this.editorSceneLandscapeToolID != -1)
			{
				base.LandscapeToolContext.EditorScene.DeleteLandscapeTool(this.editorSceneLandscapeToolID);
				this.editorSceneLandscapeToolID = -1;
			}
		}

		// Token: 0x04000A97 RID: 2711
		private int editorSceneLandscapeToolID = -1;
	}
}
