using System;
using Tools.Geometry;
using Tools.Landscape;
using Tools.Landscape.LandscapeToolParams;

namespace MapEditor.Map.Landscape
{
	// Token: 0x020002AB RID: 683
	public class HeightLandscapeTool : LandscapeTool
	{
		// Token: 0x17000731 RID: 1841
		// (get) Token: 0x06001FA1 RID: 8097 RVA: 0x000CA804 File Offset: 0x000C9804
		private double Autoreverse
		{
			get
			{
				if (!base.LandscapeRegion.LandscapeRegionParams.InvertHeightTools || base.LandscapeRegion.LandscapeRegionParams.TerrainNumber == 0)
				{
					return 1.0;
				}
				return -1.0;
			}
		}

		// Token: 0x06001FA2 RID: 8098 RVA: 0x000CA83D File Offset: 0x000C983D
		public HeightLandscapeTool(int _id) : base(_id)
		{
			base.AffectParams.AffectHeight = true;
		}

		// Token: 0x06001FA3 RID: 8099 RVA: 0x000CA860 File Offset: 0x000C9860
		public override void Create()
		{
			this.Destroy();
			HeightLandscapeToolParams heightLandscapeToolParams = base.LandscapeToolParams as HeightLandscapeToolParams;
			if (heightLandscapeToolParams != null && base.LandscapeRegion != null)
			{
				base.LandscapeRegion.IgnoreCenter = !heightLandscapeToolParams.AutomaticTool;
				base.AffectParams.UpdateObjectHeightsParams.Update = heightLandscapeToolParams.UpdateObjectHeightsParams.Update;
				base.AffectParams.UpdateObjectHeightsParams.HeightRange = heightLandscapeToolParams.UpdateObjectHeightsParams.HeightRange;
				if (base.LandscapeRegion.LandscapeRegionParams.Type == LandscapeRegionType.Ellipse || base.LandscapeRegion.LandscapeRegionParams.Type == LandscapeRegionType.Square)
				{
					bool ellipse = base.LandscapeRegion.LandscapeRegionParams.Type == LandscapeRegionType.Ellipse;
					Point size = new Point(base.LandscapeRegion.LandscapeRegionParams.Size, base.LandscapeRegion.LandscapeRegionParams.Size);
					if (heightLandscapeToolParams.HeightToolType == LandscapeHeightToolType.Up || heightLandscapeToolParams.HeightToolType == LandscapeHeightToolType.Down)
					{
						this.heightmapTool = heightLandscapeToolParams.FileName.ToLower().Contains("heightmap");
						if (this.heightmapTool)
						{
							this.editorSceneLandscapeToolID = base.LandscapeToolContext.EditorScene.CreateSimpleHeightmapLandscapeTool(ref size, ellipse, !heightLandscapeToolParams.AutomaticTool || heightLandscapeToolParams.PreciseTool, heightLandscapeToolParams.StrengthSmoothParams.Smooth, EditorEnvironment.EditorFolder + heightLandscapeToolParams.FileName, heightLandscapeToolParams.StrengthSmoothParams.Strength * this.Autoreverse * ((heightLandscapeToolParams.HeightToolType == LandscapeHeightToolType.Down) ? -1.0 : 1.0), heightLandscapeToolParams.PreciseTool);
							return;
						}
						this.editorSceneLandscapeToolID = base.LandscapeToolContext.EditorScene.CreateSimpleProfileLandscapeTool(ref size, ellipse, !heightLandscapeToolParams.AutomaticTool || heightLandscapeToolParams.PreciseTool, heightLandscapeToolParams.StrengthSmoothParams.Smooth, EditorEnvironment.EditorFolder + heightLandscapeToolParams.FileName, heightLandscapeToolParams.StrengthSmoothParams.Strength * this.Autoreverse * ((heightLandscapeToolParams.HeightToolType == LandscapeHeightToolType.Down) ? -1.0 : 1.0), heightLandscapeToolParams.PreciseTool);
						return;
					}
					else
					{
						if (heightLandscapeToolParams.HeightToolType == LandscapeHeightToolType.Plato)
						{
							this.editorSceneLandscapeToolID = base.LandscapeToolContext.EditorScene.CreateSimplePlatoLandscapeTool(ref size, ellipse, !heightLandscapeToolParams.AutomaticTool, heightLandscapeToolParams.StrengthSmoothParams.Smooth, heightLandscapeToolParams.Height, heightLandscapeToolParams.StrengthSmoothParams.Strength, heightLandscapeToolParams.HeightPlatoToolType == LandscapeHeightPlatoToolType.Raise, heightLandscapeToolParams.HeightPlatoToolType == LandscapeHeightPlatoToolType.Lower);
							return;
						}
						if (heightLandscapeToolParams.HeightToolType == LandscapeHeightToolType.Plane)
						{
							this.editorSceneLandscapeToolID = base.LandscapeToolContext.EditorScene.CreateSimplePlaneLandscapeTool(ref size, ellipse, !heightLandscapeToolParams.AutomaticTool, heightLandscapeToolParams.StrengthSmoothParams.Smooth, heightLandscapeToolParams.Height, heightLandscapeToolParams.StrengthSmoothParams.Strength);
							return;
						}
						if (heightLandscapeToolParams.HeightToolType == LandscapeHeightToolType.Smooth)
						{
							this.editorSceneLandscapeToolID = base.LandscapeToolContext.EditorScene.CreateSimpleSmoothV2LandscapeTool(ref size, ellipse, !heightLandscapeToolParams.AutomaticTool, heightLandscapeToolParams.StrengthSmoothParams.Smooth, heightLandscapeToolParams.StrengthSmoothParams.Strength);
							return;
						}
						if (heightLandscapeToolParams.HeightToolType == LandscapeHeightToolType.LevelToPlane)
						{
							if (heightLandscapeToolParams.PreciseTool)
							{
								this.levelTool = true;
								this.editorSceneLandscapeToolID = base.LandscapeToolContext.EditorScene.CreateSimpleLevelLandscapeTool(ref size, ellipse, !heightLandscapeToolParams.AutomaticTool, heightLandscapeToolParams.StrengthSmoothParams.Smooth, heightLandscapeToolParams.StrengthSmoothParams.Strength);
								return;
							}
							this.levelTool = false;
							this.editorSceneLandscapeToolID = base.LandscapeToolContext.EditorScene.CreateSimpleSmoothLandscapeTool(ref size, ellipse, !heightLandscapeToolParams.AutomaticTool, heightLandscapeToolParams.StrengthSmoothParams.Smooth, heightLandscapeToolParams.StrengthSmoothParams.Strength, false);
							return;
						}
					}
				}
				else if (base.LandscapeRegion.LandscapeRegionParams.Type == LandscapeRegionType.Polygon)
				{
					Vec3 mapOffset = base.LandscapeToolContext.MapOffset;
					if (heightLandscapeToolParams.HeightToolType == LandscapeHeightToolType.Up || heightLandscapeToolParams.HeightToolType == LandscapeHeightToolType.Down)
					{
						this.heightmapTool = heightLandscapeToolParams.FileName.ToLower().Contains("heightmap");
						if (this.heightmapTool)
						{
							this.editorSceneLandscapeToolID = base.LandscapeToolContext.EditorScene.CreateSimpleHeightmapLandscapeTool(ref mapOffset, base.LandscapeRegion.Polygon, !heightLandscapeToolParams.AutomaticTool || heightLandscapeToolParams.PreciseTool, heightLandscapeToolParams.StrengthSmoothParams.Smooth, EditorEnvironment.EditorFolder + heightLandscapeToolParams.FileName, heightLandscapeToolParams.StrengthSmoothParams.Strength * this.Autoreverse * ((heightLandscapeToolParams.HeightToolType == LandscapeHeightToolType.Down) ? -1.0 : 1.0), heightLandscapeToolParams.PreciseTool);
							return;
						}
						this.editorSceneLandscapeToolID = base.LandscapeToolContext.EditorScene.CreateSimpleProfileLandscapeTool(ref mapOffset, base.LandscapeRegion.Polygon, !heightLandscapeToolParams.AutomaticTool || heightLandscapeToolParams.PreciseTool, heightLandscapeToolParams.StrengthSmoothParams.Smooth, EditorEnvironment.EditorFolder + heightLandscapeToolParams.FileName, heightLandscapeToolParams.StrengthSmoothParams.Strength * this.Autoreverse * ((heightLandscapeToolParams.HeightToolType == LandscapeHeightToolType.Down) ? -1.0 : 1.0), heightLandscapeToolParams.PreciseTool);
						return;
					}
					else
					{
						if (heightLandscapeToolParams.HeightToolType == LandscapeHeightToolType.Plato)
						{
							this.editorSceneLandscapeToolID = base.LandscapeToolContext.EditorScene.CreateSimplePlatoLandscapeTool(ref mapOffset, base.LandscapeRegion.Polygon, !heightLandscapeToolParams.AutomaticTool, heightLandscapeToolParams.StrengthSmoothParams.Smooth, heightLandscapeToolParams.Height, heightLandscapeToolParams.StrengthSmoothParams.Strength, heightLandscapeToolParams.HeightPlatoToolType == LandscapeHeightPlatoToolType.Raise, heightLandscapeToolParams.HeightPlatoToolType == LandscapeHeightPlatoToolType.Lower);
							return;
						}
						if (heightLandscapeToolParams.HeightToolType == LandscapeHeightToolType.Plane)
						{
							this.editorSceneLandscapeToolID = base.LandscapeToolContext.EditorScene.CreateSimplePlaneLandscapeTool(ref mapOffset, base.LandscapeRegion.Polygon, !heightLandscapeToolParams.AutomaticTool, heightLandscapeToolParams.StrengthSmoothParams.Smooth, heightLandscapeToolParams.Height, heightLandscapeToolParams.StrengthSmoothParams.Strength);
							return;
						}
						if (heightLandscapeToolParams.HeightToolType == LandscapeHeightToolType.Smooth)
						{
							this.editorSceneLandscapeToolID = base.LandscapeToolContext.EditorScene.CreateSimpleSmoothV2LandscapeTool(ref mapOffset, base.LandscapeRegion.Polygon, !heightLandscapeToolParams.AutomaticTool, heightLandscapeToolParams.StrengthSmoothParams.Smooth, heightLandscapeToolParams.StrengthSmoothParams.Strength);
							return;
						}
						if (heightLandscapeToolParams.HeightToolType == LandscapeHeightToolType.LevelToPlane)
						{
							if (heightLandscapeToolParams.PreciseTool)
							{
								this.levelTool = true;
								this.editorSceneLandscapeToolID = base.LandscapeToolContext.EditorScene.CreateSimpleLevelLandscapeTool(ref mapOffset, base.LandscapeRegion.Polygon, !heightLandscapeToolParams.AutomaticTool, heightLandscapeToolParams.StrengthSmoothParams.Smooth, heightLandscapeToolParams.StrengthSmoothParams.Strength);
								return;
							}
							this.levelTool = false;
							this.editorSceneLandscapeToolID = base.LandscapeToolContext.EditorScene.CreateSimpleSmoothLandscapeTool(ref mapOffset, base.LandscapeRegion.Polygon, !heightLandscapeToolParams.AutomaticTool, heightLandscapeToolParams.StrengthSmoothParams.Smooth, heightLandscapeToolParams.StrengthSmoothParams.Strength, false);
							return;
						}
					}
				}
				else if (base.LandscapeRegion.LandscapeRegionParams.Type == LandscapeRegionType.Stripe)
				{
					Vec3 mapOffset2 = base.LandscapeToolContext.MapOffset;
					if (heightLandscapeToolParams.HeightToolType == LandscapeHeightToolType.Up || heightLandscapeToolParams.HeightToolType == LandscapeHeightToolType.Down)
					{
						this.heightmapTool = heightLandscapeToolParams.FileName.ToLower().Contains("heightmap");
						if (this.heightmapTool)
						{
							this.editorSceneLandscapeToolID = base.LandscapeToolContext.EditorScene.CreateSimpleHeightmapLandscapeTool(ref mapOffset2, base.LandscapeRegion.Stripe.Bounds, !heightLandscapeToolParams.AutomaticTool || heightLandscapeToolParams.PreciseTool, heightLandscapeToolParams.StrengthSmoothParams.Smooth, EditorEnvironment.EditorFolder + heightLandscapeToolParams.FileName, heightLandscapeToolParams.StrengthSmoothParams.Strength * this.Autoreverse * ((heightLandscapeToolParams.HeightToolType == LandscapeHeightToolType.Down) ? -1.0 : 1.0), heightLandscapeToolParams.PreciseTool);
							return;
						}
						if (base.LandscapeRegion.LandscapeRegionParams.Side == LandscapeRegionSide.Both)
						{
							this.editorSceneLandscapeToolID = base.LandscapeToolContext.EditorScene.CreateSimpleProfileLandscapeTool(ref mapOffset2, base.LandscapeRegion.Stripe.Bounds, !heightLandscapeToolParams.AutomaticTool || heightLandscapeToolParams.PreciseTool, heightLandscapeToolParams.StrengthSmoothParams.Smooth, EditorEnvironment.EditorFolder + heightLandscapeToolParams.FileName, heightLandscapeToolParams.StrengthSmoothParams.Strength * this.Autoreverse * ((heightLandscapeToolParams.HeightToolType == LandscapeHeightToolType.Down) ? -1.0 : 1.0), heightLandscapeToolParams.PreciseTool);
							return;
						}
						this.editorSceneLandscapeToolID = base.LandscapeToolContext.EditorScene.CreateSimpleProfileLandscapeTool(ref mapOffset2, base.LandscapeRegion.Stripe.Bounds, base.LandscapeRegion.LandscapeRegionParams.Side == LandscapeRegionSide.Left, !heightLandscapeToolParams.AutomaticTool || heightLandscapeToolParams.PreciseTool, heightLandscapeToolParams.StrengthSmoothParams.Smooth, EditorEnvironment.EditorFolder + heightLandscapeToolParams.FileName, heightLandscapeToolParams.StrengthSmoothParams.Strength * this.Autoreverse * ((heightLandscapeToolParams.HeightToolType == LandscapeHeightToolType.Down) ? -1.0 : 1.0), heightLandscapeToolParams.PreciseTool);
						return;
					}
					else
					{
						if (heightLandscapeToolParams.HeightToolType == LandscapeHeightToolType.Plato)
						{
							this.editorSceneLandscapeToolID = base.LandscapeToolContext.EditorScene.CreateSimplePlatoLandscapeTool(ref mapOffset2, base.LandscapeRegion.Stripe.Bounds, !heightLandscapeToolParams.AutomaticTool, heightLandscapeToolParams.StrengthSmoothParams.Smooth, heightLandscapeToolParams.Height, heightLandscapeToolParams.StrengthSmoothParams.Strength, heightLandscapeToolParams.HeightPlatoToolType == LandscapeHeightPlatoToolType.Raise, heightLandscapeToolParams.HeightPlatoToolType == LandscapeHeightPlatoToolType.Lower);
							return;
						}
						if (heightLandscapeToolParams.HeightToolType == LandscapeHeightToolType.Plane)
						{
							this.editorSceneLandscapeToolID = base.LandscapeToolContext.EditorScene.CreateSimplePlaneLandscapeTool(ref mapOffset2, base.LandscapeRegion.Stripe.Bounds, !heightLandscapeToolParams.AutomaticTool, heightLandscapeToolParams.StrengthSmoothParams.Smooth, heightLandscapeToolParams.Height, heightLandscapeToolParams.StrengthSmoothParams.Strength);
							return;
						}
						if (heightLandscapeToolParams.HeightToolType == LandscapeHeightToolType.Smooth)
						{
							this.editorSceneLandscapeToolID = base.LandscapeToolContext.EditorScene.CreateSimpleSmoothV2LandscapeTool(ref mapOffset2, base.LandscapeRegion.Stripe.Bounds, !heightLandscapeToolParams.AutomaticTool, heightLandscapeToolParams.StrengthSmoothParams.Smooth, heightLandscapeToolParams.StrengthSmoothParams.Strength);
							return;
						}
						if (heightLandscapeToolParams.HeightToolType == LandscapeHeightToolType.LevelToPlane)
						{
							if (heightLandscapeToolParams.PreciseTool)
							{
								this.levelTool = true;
								this.editorSceneLandscapeToolID = base.LandscapeToolContext.EditorScene.CreateSimpleLevelLandscapeTool(ref mapOffset2, base.LandscapeRegion.Stripe.Bounds, !heightLandscapeToolParams.AutomaticTool, heightLandscapeToolParams.StrengthSmoothParams.Smooth, heightLandscapeToolParams.StrengthSmoothParams.Strength);
								return;
							}
							this.levelTool = false;
							this.editorSceneLandscapeToolID = base.LandscapeToolContext.EditorScene.CreateSimpleSmoothLandscapeTool(ref mapOffset2, base.LandscapeRegion.Stripe.Bounds, !heightLandscapeToolParams.AutomaticTool, heightLandscapeToolParams.StrengthSmoothParams.Smooth, heightLandscapeToolParams.StrengthSmoothParams.Strength, false);
							return;
						}
					}
				}
				else
				{
					LandscapeRegionType type = base.LandscapeRegion.LandscapeRegionParams.Type;
				}
			}
		}

		// Token: 0x06001FA4 RID: 8100 RVA: 0x000CB2FC File Offset: 0x000CA2FC
		public override void Apply()
		{
			if (this.editorSceneLandscapeToolID != -1)
			{
				HeightLandscapeToolParams heightLandscapeToolParams = base.LandscapeToolParams as HeightLandscapeToolParams;
				if (heightLandscapeToolParams != null)
				{
					if (!heightLandscapeToolParams.AutomaticTool)
					{
						if (heightLandscapeToolParams.HeightToolType == LandscapeHeightToolType.Up || heightLandscapeToolParams.HeightToolType == LandscapeHeightToolType.Down)
						{
							double height = base.LandscapeToolContext.GetDifferenceWithSnapShot(heightLandscapeToolParams.StrengthSmoothParams.Strength * this.Autoreverse / HeightLandscapeTool.heightDelimiter, false, false, 0.0, 0.0);
							if (this.heightmapTool)
							{
								base.LandscapeToolContext.EditorScene.SetHeightmapHeight_SimpleHeightmapLandscapeTool(this.editorSceneLandscapeToolID, height);
							}
							else
							{
								base.LandscapeToolContext.EditorScene.SetProfileHeight_SimpleProfileLandscapeTool(this.editorSceneLandscapeToolID, height);
							}
						}
						else
						{
							double ratio = base.LandscapeToolContext.GetDifferenceWithSnapShot(heightLandscapeToolParams.StrengthSmoothParams.Strength * this.Autoreverse / HeightLandscapeTool.ratioDelimiter, true, true, 0.0, 1.0);
							if (heightLandscapeToolParams.HeightToolType == LandscapeHeightToolType.Plato)
							{
								base.LandscapeToolContext.EditorScene.SetPlatoRatio_SimplePlatoLandscapeTool(this.editorSceneLandscapeToolID, ratio);
							}
							else if (heightLandscapeToolParams.HeightToolType == LandscapeHeightToolType.Plane)
							{
								base.LandscapeToolContext.EditorScene.SetPlaneRatio_SimplePlaneLandscapeTool(this.editorSceneLandscapeToolID, ratio);
							}
							else if (heightLandscapeToolParams.HeightToolType == LandscapeHeightToolType.Smooth)
							{
								base.LandscapeToolContext.EditorScene.SetSmoothRatio_SimpleSmoothV2LandscapeTool(this.editorSceneLandscapeToolID, ratio);
							}
							else if (heightLandscapeToolParams.HeightToolType == LandscapeHeightToolType.LevelToPlane)
							{
								if (this.levelTool)
								{
									base.LandscapeToolContext.EditorScene.SetLevelRatio_SimpleLevelLandscapeTool(this.editorSceneLandscapeToolID, ratio);
								}
								else
								{
									base.LandscapeToolContext.EditorScene.SetSmoothRatio_SimpleSmoothLandscapeTool(this.editorSceneLandscapeToolID, ratio);
								}
							}
						}
					}
					Rect affectedRect = Rect.Empty;
					if (base.LandscapeRegion.LandscapeRegionParams.Type == LandscapeRegionType.Ellipse || base.LandscapeRegion.LandscapeRegionParams.Type == LandscapeRegionType.Square)
					{
						Point position = heightLandscapeToolParams.AutomaticTool ? base.LandscapeToolContext.LandscapeToolContextPosition.TerrainPosition : base.LandscapeToolContext.LandscapeToolContextPositionSnapShot.TerrainPosition;
						base.LandscapeToolContext.EditorScene.ApplySimpleLandscapeTool(this.editorSceneLandscapeToolID, ref position, out affectedRect);
					}
					else if (base.LandscapeRegion.LandscapeRegionParams.Type == LandscapeRegionType.Polygon)
					{
						base.LandscapeToolContext.EditorScene.ApplySimpleLandscapeTool(this.editorSceneLandscapeToolID, out affectedRect);
					}
					else if (base.LandscapeRegion.LandscapeRegionParams.Type == LandscapeRegionType.Stripe)
					{
						base.LandscapeToolContext.EditorScene.ApplySimpleLandscapeTool(this.editorSceneLandscapeToolID, out affectedRect);
					}
					else
					{
						LandscapeRegionType type = base.LandscapeRegion.LandscapeRegionParams.Type;
					}
					base.AffectParams.AffectedRect = affectedRect;
				}
			}
		}

		// Token: 0x06001FA5 RID: 8101 RVA: 0x000CB58A File Offset: 0x000CA58A
		public override void Destroy()
		{
			if (this.editorSceneLandscapeToolID != -1)
			{
				base.LandscapeRegion.IgnoreCenter = false;
				base.LandscapeToolContext.EditorScene.DeleteLandscapeTool(this.editorSceneLandscapeToolID);
				this.editorSceneLandscapeToolID = -1;
			}
		}

		// Token: 0x0400137E RID: 4990
		private static readonly double heightDelimiter = 10.0;

		// Token: 0x0400137F RID: 4991
		private static readonly double ratioDelimiter = 200.0;

		// Token: 0x04001380 RID: 4992
		private int editorSceneLandscapeToolID = -1;

		// Token: 0x04001381 RID: 4993
		private bool heightmapTool;

		// Token: 0x04001382 RID: 4994
		private bool levelTool = true;
	}
}
