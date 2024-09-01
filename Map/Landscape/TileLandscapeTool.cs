using System;
using MapEditor.Map.DataProviders;
using Tools.Geometry;
using Tools.Landscape;
using Tools.Landscape.LandscapeToolParams;

namespace MapEditor.Map.Landscape
{
	// Token: 0x020002CE RID: 718
	public class TileLandscapeTool : LandscapeTool
	{
		// Token: 0x0600213B RID: 8507 RVA: 0x000D2534 File Offset: 0x000D1534
		public TileLandscapeTool(int _id) : base(_id)
		{
			base.AffectParams.AffectTile = true;
		}

		// Token: 0x0600213C RID: 8508 RVA: 0x000D2558 File Offset: 0x000D1558
		public override void Create()
		{
			this.Destroy();
			TileLandscapeToolParams tileLandscapeToolParams = base.LandscapeToolParams as TileLandscapeToolParams;
			if (tileLandscapeToolParams != null && base.LandscapeRegion != null)
			{
				if (tileLandscapeToolParams.TileForPaint != LandscapeTileItemSource.GetRemoveTerrainIndex())
				{
					base.LandscapeRegion.IgnoreCenter = !tileLandscapeToolParams.AutomaticTool;
					double minAngle;
					double maxAngle;
					tileLandscapeToolParams.AngleRestrictionsParams.GetMinMaxAngles(out minAngle, out maxAngle, false);
					if (base.LandscapeRegion.LandscapeRegionParams.Type == LandscapeRegionType.Ellipse || base.LandscapeRegion.LandscapeRegionParams.Type == LandscapeRegionType.Square)
					{
						bool ellipse = base.LandscapeRegion.LandscapeRegionParams.Type == LandscapeRegionType.Ellipse;
						Point size = new Point(base.LandscapeRegion.LandscapeRegionParams.Size, base.LandscapeRegion.LandscapeRegionParams.Size);
						this.editorSceneTileLandscapeToolID = base.LandscapeToolContext.EditorScene.CreateSimpleLayerLandscapeTool(ref size, ellipse, tileLandscapeToolParams.SpotTool || !tileLandscapeToolParams.AutomaticTool, tileLandscapeToolParams.StrengthSmoothParams.Smooth, tileLandscapeToolParams.TileForPaint, tileLandscapeToolParams.StrengthSmoothParams.Strength, minAngle, maxAngle, tileLandscapeToolParams.ReplaceTool ? tileLandscapeToolParams.TileForReplace : -1);
					}
					else if (base.LandscapeRegion.LandscapeRegionParams.Type == LandscapeRegionType.Polygon)
					{
						Vec3 mapOffset = base.LandscapeToolContext.MapOffset;
						this.editorSceneTileLandscapeToolID = base.LandscapeToolContext.EditorScene.CreateSimpleLayerLandscapeTool(ref mapOffset, base.LandscapeRegion.Polygon, tileLandscapeToolParams.SpotTool || !tileLandscapeToolParams.AutomaticTool, tileLandscapeToolParams.StrengthSmoothParams.Smooth, tileLandscapeToolParams.TileForPaint, tileLandscapeToolParams.StrengthSmoothParams.Strength, minAngle, maxAngle, tileLandscapeToolParams.ReplaceTool ? tileLandscapeToolParams.TileForReplace : -1);
					}
					else if (base.LandscapeRegion.LandscapeRegionParams.Type == LandscapeRegionType.Stripe)
					{
						Vec3 mapOffset2 = base.LandscapeToolContext.MapOffset;
						if (base.LandscapeRegion.LandscapeRegionParams.Side == LandscapeRegionSide.Both)
						{
							this.editorSceneTileLandscapeToolID = base.LandscapeToolContext.EditorScene.CreateSimpleLayerLandscapeTool(ref mapOffset2, base.LandscapeRegion.Stripe.Bounds, tileLandscapeToolParams.SpotTool || !tileLandscapeToolParams.AutomaticTool, tileLandscapeToolParams.StrengthSmoothParams.Smooth, tileLandscapeToolParams.TileForPaint, tileLandscapeToolParams.StrengthSmoothParams.Strength, minAngle, maxAngle, tileLandscapeToolParams.ReplaceTool ? tileLandscapeToolParams.TileForReplace : -1);
						}
						else
						{
							this.editorSceneTileLandscapeToolID = base.LandscapeToolContext.EditorScene.CreateSimpleLayerLandscapeTool(ref mapOffset2, base.LandscapeRegion.Stripe.Bounds, base.LandscapeRegion.LandscapeRegionParams.Side == LandscapeRegionSide.Left, tileLandscapeToolParams.SpotTool || !tileLandscapeToolParams.AutomaticTool, tileLandscapeToolParams.StrengthSmoothParams.Smooth, tileLandscapeToolParams.TileForPaint, tileLandscapeToolParams.StrengthSmoothParams.Strength, minAngle, maxAngle, tileLandscapeToolParams.ReplaceTool ? tileLandscapeToolParams.TileForReplace : -1);
						}
					}
					else
					{
						LandscapeRegionType type = base.LandscapeRegion.LandscapeRegionParams.Type;
					}
				}
				if (base.LandscapeRegion.LandscapeRegionParams.Type == LandscapeRegionType.Ellipse || base.LandscapeRegion.LandscapeRegionParams.Type == LandscapeRegionType.Square)
				{
					this.editorSceneMakeTerrainLandscapeToolID = base.LandscapeToolContext.EditorScene.CreateMakeTerrainLandscapeTool(tileLandscapeToolParams.TileForPaint != LandscapeTileItemSource.GetRemoveTerrainIndex());
				}
			}
		}

		// Token: 0x0600213D RID: 8509 RVA: 0x000D2898 File Offset: 0x000D1898
		public override void Apply()
		{
			if (this.editorSceneTileLandscapeToolID != -1)
			{
				TileLandscapeToolParams tileLandscapeToolParams = base.LandscapeToolParams as TileLandscapeToolParams;
				if (tileLandscapeToolParams != null)
				{
					if (!tileLandscapeToolParams.AutomaticTool)
					{
						double value = base.LandscapeToolContext.GetDifferenceWithSnapShot(tileLandscapeToolParams.StrengthSmoothParams.Strength / TileLandscapeTool.ratioDelimiter, true, true, 0.0, 1.0);
						base.LandscapeToolContext.EditorScene.SetValue_SimpleLayerLandscapeTool(this.editorSceneTileLandscapeToolID, value);
					}
					Rect affectedRect = Rect.Empty;
					if (base.LandscapeRegion.LandscapeRegionParams.Type == LandscapeRegionType.Ellipse || base.LandscapeRegion.LandscapeRegionParams.Type == LandscapeRegionType.Square)
					{
						Point position = tileLandscapeToolParams.AutomaticTool ? base.LandscapeToolContext.LandscapeToolContextPosition.TerrainPosition : base.LandscapeToolContext.LandscapeToolContextPositionSnapShot.TerrainPosition;
						base.LandscapeToolContext.EditorScene.ApplySimpleLandscapeTool(this.editorSceneTileLandscapeToolID, ref position, out affectedRect);
					}
					else if (base.LandscapeRegion.LandscapeRegionParams.Type == LandscapeRegionType.Polygon)
					{
						base.LandscapeToolContext.EditorScene.ApplySimpleLandscapeTool(this.editorSceneTileLandscapeToolID, out affectedRect);
					}
					else if (base.LandscapeRegion.LandscapeRegionParams.Type == LandscapeRegionType.Stripe)
					{
						base.LandscapeToolContext.EditorScene.ApplySimpleLandscapeTool(this.editorSceneTileLandscapeToolID, out affectedRect);
					}
					else
					{
						LandscapeRegionType type = base.LandscapeRegion.LandscapeRegionParams.Type;
					}
					base.AffectParams.AffectedRect = affectedRect;
				}
			}
			if (this.editorSceneMakeTerrainLandscapeToolID != -1)
			{
				TileLandscapeToolParams tileLandscapeToolParams2 = base.LandscapeToolParams as TileLandscapeToolParams;
				if (tileLandscapeToolParams2 != null && (base.LandscapeRegion.LandscapeRegionParams.Type == LandscapeRegionType.Ellipse || base.LandscapeRegion.LandscapeRegionParams.Type == LandscapeRegionType.Square))
				{
					Point position2 = tileLandscapeToolParams2.AutomaticTool ? base.LandscapeToolContext.LandscapeToolContextPosition.TerrainPosition : base.LandscapeToolContext.LandscapeToolContextPositionSnapShot.TerrainPosition;
					Rect affectedRect2;
					base.LandscapeToolContext.EditorScene.ApplyMakeTerrainLandscapeTool(this.editorSceneMakeTerrainLandscapeToolID, ref position2, out affectedRect2);
				}
			}
		}

		// Token: 0x0600213E RID: 8510 RVA: 0x000D2A88 File Offset: 0x000D1A88
		public override void Destroy()
		{
			if (this.editorSceneTileLandscapeToolID != -1)
			{
				base.LandscapeRegion.IgnoreCenter = false;
				base.LandscapeToolContext.EditorScene.DeleteLandscapeTool(this.editorSceneTileLandscapeToolID);
				this.editorSceneTileLandscapeToolID = -1;
			}
			if (this.editorSceneMakeTerrainLandscapeToolID != -1)
			{
				base.LandscapeRegion.IgnoreCenter = false;
				base.LandscapeToolContext.EditorScene.DeleteLandscapeTool(this.editorSceneMakeTerrainLandscapeToolID);
				this.editorSceneMakeTerrainLandscapeToolID = -1;
			}
		}

		// Token: 0x04001427 RID: 5159
		private static readonly double ratioDelimiter = 200.0;

		// Token: 0x04001428 RID: 5160
		private int editorSceneTileLandscapeToolID = -1;

		// Token: 0x04001429 RID: 5161
		private int editorSceneMakeTerrainLandscapeToolID = -1;
	}
}
