using System;
using Tools.Landscape;
using Tools.Landscape.LandscapeToolParams;

namespace MapEditor.Map.Landscape
{
	// Token: 0x02000264 RID: 612
	public class MapEditorLandscapeToolFactory : LandscapeToolFactory
	{
		// Token: 0x06001CEE RID: 7406 RVA: 0x000B8918 File Offset: 0x000B7918
		private static ILandscapeTool InnerCreateSimpleLandscapeTool(int landscapeToolID, ILandscapeToolParams landscapeToolParams)
		{
			if (landscapeToolParams != null && MapEditorLandscapeToolFactory.IsSimpleLandscapeToolParams(landscapeToolParams.LandscapeToolType))
			{
				if (landscapeToolParams.LandscapeToolType == LandscapeToolType.Tile)
				{
					return new TileLandscapeTool(landscapeToolID);
				}
				if (landscapeToolParams.LandscapeToolType == LandscapeToolType.TilePicker)
				{
					return new TilePickerLandscapeTool(landscapeToolID);
				}
				if (landscapeToolParams.LandscapeToolType == LandscapeToolType.Gradient)
				{
					return new GradientLandscapeTool(landscapeToolID);
				}
				if (landscapeToolParams.LandscapeToolType == LandscapeToolType.Water)
				{
					return new WaterLandscapeTool(landscapeToolID);
				}
				if (landscapeToolParams.LandscapeToolType == LandscapeToolType.Height)
				{
					return new HeightLandscapeTool(landscapeToolID);
				}
				if (landscapeToolParams.LandscapeToolType == LandscapeToolType.HeightPicker)
				{
					return new HeightPickerLandscapeTool(landscapeToolID);
				}
			}
			return null;
		}

		// Token: 0x06001CEF RID: 7407 RVA: 0x000B8996 File Offset: 0x000B7996
		public static bool IsSimpleLandscapeToolParams(LandscapeToolType landscapeToolType)
		{
			return landscapeToolType == LandscapeToolType.Tile || landscapeToolType == LandscapeToolType.TilePicker || landscapeToolType == LandscapeToolType.Gradient || landscapeToolType == LandscapeToolType.Height || landscapeToolType == LandscapeToolType.HeightPicker;
		}

		// Token: 0x06001CF0 RID: 7408 RVA: 0x000B89B7 File Offset: 0x000B79B7
		public static bool IsWaterLandscapeToolParams(LandscapeToolType landscapeToolType)
		{
			return landscapeToolType == LandscapeToolType.Water;
		}

		// Token: 0x06001CF1 RID: 7409 RVA: 0x000B89C0 File Offset: 0x000B79C0
		public static ILandscapeTool CreateSimpleLandscapeTool(int landscapeToolID, LandscapeToolContext landscapeToolContext, LandscapeRegion landscapeRegion, ILandscapeParams landscapeParams)
		{
			if (landscapeToolContext.LandscapeToolContextMouseStatus.Left)
			{
				ILandscapeToolParams leftLandscapeToolParams = landscapeParams.LandscapeToolParamsContainer.Get(true, landscapeParams.ActiveLandscapeToolParamsIndex);
				if (leftLandscapeToolParams != null)
				{
					ILandscapeTool leftLandscapeTool = MapEditorLandscapeToolFactory.InnerCreateSimpleLandscapeTool(landscapeToolID, leftLandscapeToolParams);
					if (leftLandscapeTool != null)
					{
						if (leftLandscapeToolParams.Timed)
						{
							leftLandscapeTool = new LandscapeToolTimer(landscapeToolID, leftLandscapeTool, landscapeParams.LeftLandscapeSimpleTimer);
						}
						leftLandscapeTool.Create(landscapeToolContext, landscapeRegion, leftLandscapeToolParams);
						leftLandscapeTool.Created = true;
					}
					return leftLandscapeTool;
				}
			}
			else if (landscapeToolContext.LandscapeToolContextMouseStatus.Right)
			{
				ILandscapeToolParams rightLandscapeToolParams = landscapeParams.LandscapeToolParamsContainer.Get(false, landscapeParams.ActiveLandscapeToolParamsIndex);
				ILandscapeTool rightLandscapeTool = MapEditorLandscapeToolFactory.InnerCreateSimpleLandscapeTool(landscapeToolID, rightLandscapeToolParams);
				if (rightLandscapeTool != null)
				{
					if (rightLandscapeToolParams.Timed)
					{
						rightLandscapeTool = new LandscapeToolTimer(landscapeToolID, rightLandscapeTool, landscapeParams.RightLandscapeSimpleTimer);
					}
					rightLandscapeTool.Create(landscapeToolContext, landscapeRegion, rightLandscapeToolParams);
					rightLandscapeTool.Created = true;
					return rightLandscapeTool;
				}
			}
			return null;
		}

		// Token: 0x06001CF2 RID: 7410 RVA: 0x000B8A7C File Offset: 0x000B7A7C
		public static ILandscapeTool CreateWaterLandscapeTool(int landscapeToolID, LandscapeToolContext landscapeToolContext, LandscapeRegion landscapeRegion, ILandscapeParams landscapeParams)
		{
			if (landscapeToolContext.LandscapeToolContextMouseStatus.Left)
			{
				WaterLandscapeToolParams waterLandscapeToolParams = landscapeParams.LandscapeToolParamsContainer.Get(true, landscapeParams.ActiveLandscapeToolParamsIndex) as WaterLandscapeToolParams;
				if (waterLandscapeToolParams != null)
				{
					ILandscapeTool waterLandscapeTool = new WaterLandscapeTool(landscapeToolID);
					if (waterLandscapeToolParams.Timed)
					{
						waterLandscapeTool = new LandscapeToolTimer(landscapeToolID, waterLandscapeTool, landscapeParams.LeftLandscapeSimpleTimer);
					}
					if (waterLandscapeToolParams.WaterToolType == LandscapeWaterToolType.PickAndMoveToHeight)
					{
						ILandscapeToolParams heightPickerLandscapeToolParams = landscapeParams.LandscapeToolParamsContainer.Get(false, landscapeParams.ActiveLandscapeToolParamsIndex);
						ILandscapeTool heightPickerLandscapeTool = null;
						if (heightPickerLandscapeToolParams.LandscapeToolType == LandscapeToolType.HeightPicker)
						{
							heightPickerLandscapeTool = MapEditorLandscapeToolFactory.InnerCreateSimpleLandscapeTool(landscapeToolID, heightPickerLandscapeToolParams);
						}
						if (heightPickerLandscapeTool != null)
						{
							if (heightPickerLandscapeToolParams.Timed)
							{
								heightPickerLandscapeTool = new LandscapeToolTimer(landscapeToolID, heightPickerLandscapeTool, landscapeParams.RightLandscapeSimpleTimer);
							}
							waterLandscapeTool = new LandscapeToolPair(landscapeToolID, heightPickerLandscapeToolParams, waterLandscapeToolParams, heightPickerLandscapeTool, waterLandscapeTool);
						}
					}
					waterLandscapeTool.Create(landscapeToolContext, landscapeRegion, waterLandscapeToolParams);
					waterLandscapeTool.Created = true;
					return waterLandscapeTool;
				}
			}
			else if (landscapeToolContext.LandscapeToolContextMouseStatus.Right)
			{
				ILandscapeToolParams rightLandscapeToolParams = landscapeParams.LandscapeToolParamsContainer.Get(false, landscapeParams.ActiveLandscapeToolParamsIndex);
				ILandscapeTool rightLandscapeTool = MapEditorLandscapeToolFactory.InnerCreateSimpleLandscapeTool(landscapeToolID, rightLandscapeToolParams);
				if (rightLandscapeTool != null)
				{
					if (rightLandscapeToolParams.Timed)
					{
						rightLandscapeTool = new LandscapeToolTimer(landscapeToolID, rightLandscapeTool, landscapeParams.RightLandscapeSimpleTimer);
					}
					rightLandscapeTool.Create(landscapeToolContext, landscapeRegion, rightLandscapeToolParams);
					rightLandscapeTool.Created = true;
					return rightLandscapeTool;
				}
			}
			return null;
		}

		// Token: 0x06001CF3 RID: 7411 RVA: 0x000B8B98 File Offset: 0x000B7B98
		public static ILandscapeTool CreateClipboardLandscapeTool(int landscapeToolID, LandscapeToolContext landscapeToolContext, LandscapeRegion landscapeRegion, ClipboardLandscapeToolParams clipboardLandscapeToolParams)
		{
			ILandscapeTool clipboardLandscapeTool = new ClipboardLandscapeTool(landscapeToolID);
			clipboardLandscapeTool.Create(landscapeToolContext, landscapeRegion, clipboardLandscapeToolParams);
			clipboardLandscapeTool.Created = true;
			return clipboardLandscapeTool;
		}

		// Token: 0x06001CF4 RID: 7412 RVA: 0x000B8BC0 File Offset: 0x000B7BC0
		public static ILandscapeTool CreateHillLandscapeTool(int landscapeToolID, LandscapeToolContext landscapeToolContext, LandscapeRegion landscapeRegion, HillLandscapeToolParams hillLandscapeToolParams)
		{
			ILandscapeTool hillLandscapeTool = new HillLandscapeTool(landscapeToolID);
			hillLandscapeTool.Create(landscapeToolContext, landscapeRegion, hillLandscapeToolParams);
			hillLandscapeTool.Created = true;
			return hillLandscapeTool;
		}

		// Token: 0x06001CF5 RID: 7413 RVA: 0x000B8BE8 File Offset: 0x000B7BE8
		public static ILandscapeTool CreateRoadLandscapeTool(int landscapeToolID, LandscapeToolContext landscapeToolContext, LandscapeRegion landscapeRegion, RoadLandscapeToolParams roadLandscapeToolParams)
		{
			ILandscapeTool roadLandscapeTool = new RoadLandscapeTool(landscapeToolID);
			roadLandscapeTool.Create(landscapeToolContext, landscapeRegion, roadLandscapeToolParams);
			roadLandscapeTool.Created = true;
			return roadLandscapeTool;
		}
	}
}
