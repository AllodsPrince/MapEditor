using System;
using Tools.Landscape;
using Tools.Landscape.LandscapeToolParams;

namespace MapEditor.Map.Landscape
{
	// Token: 0x02000205 RID: 517
	public class MapEditorLandscapeToolContainer : LandscapeToolContainer
	{
		// Token: 0x0600197E RID: 6526 RVA: 0x000A6FA4 File Offset: 0x000A5FA4
		public int AddSimpleLandscapeTool(LandscapeToolContext landscapeToolContext, LandscapeRegion landscapeRegion, ILandscapeParams landscapeParams)
		{
			int newLandscapeToolID = base.FreeIDCollector.LockID();
			ILandscapeTool newLandscapeTool = MapEditorLandscapeToolFactory.CreateSimpleLandscapeTool(newLandscapeToolID, landscapeToolContext, landscapeRegion, landscapeParams);
			if (newLandscapeTool != null)
			{
				base.InternalAddLandscapeTool(newLandscapeTool);
				return newLandscapeToolID;
			}
			base.FreeIDCollector.FreeID(newLandscapeToolID);
			return -1;
		}

		// Token: 0x0600197F RID: 6527 RVA: 0x000A6FE0 File Offset: 0x000A5FE0
		public int AddWaterLandscapeTool(LandscapeToolContext landscapeToolContext, LandscapeRegion landscapeRegion, ILandscapeParams landscapeParams)
		{
			int newLandscapeToolID = base.FreeIDCollector.LockID();
			ILandscapeTool newLandscapeTool = MapEditorLandscapeToolFactory.CreateWaterLandscapeTool(newLandscapeToolID, landscapeToolContext, landscapeRegion, landscapeParams);
			if (newLandscapeTool != null)
			{
				base.InternalAddLandscapeTool(newLandscapeTool);
				return newLandscapeToolID;
			}
			base.FreeIDCollector.FreeID(newLandscapeToolID);
			return -1;
		}

		// Token: 0x06001980 RID: 6528 RVA: 0x000A701C File Offset: 0x000A601C
		public int AddClipboardLandscapeTool(LandscapeToolContext landscapeToolContext, LandscapeRegion landscapeRegion, ClipboardLandscapeToolParams clipboardLandscapeToolParams)
		{
			int newLandscapeToolID = base.FreeIDCollector.LockID();
			ILandscapeTool newLandscapeTool = MapEditorLandscapeToolFactory.CreateClipboardLandscapeTool(newLandscapeToolID, landscapeToolContext, landscapeRegion, clipboardLandscapeToolParams);
			if (newLandscapeTool != null)
			{
				base.InternalAddLandscapeTool(newLandscapeTool);
				return newLandscapeToolID;
			}
			base.FreeIDCollector.FreeID(newLandscapeToolID);
			return -1;
		}

		// Token: 0x06001981 RID: 6529 RVA: 0x000A7058 File Offset: 0x000A6058
		public int AddHillLandscapeTool(LandscapeToolContext landscapeToolContext, LandscapeRegion landscapeRegion, HillLandscapeToolParams hillLandscapeToolParams)
		{
			int newLandscapeToolID = base.FreeIDCollector.LockID();
			ILandscapeTool newLandscapeTool = MapEditorLandscapeToolFactory.CreateHillLandscapeTool(newLandscapeToolID, landscapeToolContext, landscapeRegion, hillLandscapeToolParams);
			if (newLandscapeTool != null)
			{
				base.InternalAddLandscapeTool(newLandscapeTool);
				return newLandscapeToolID;
			}
			base.FreeIDCollector.FreeID(newLandscapeToolID);
			return -1;
		}

		// Token: 0x06001982 RID: 6530 RVA: 0x000A7094 File Offset: 0x000A6094
		public int AddRoadLandscapeTool(LandscapeToolContext landscapeToolContext, LandscapeRegion landscapeRegion, RoadLandscapeToolParams roadLandscapeToolParams)
		{
			int newLandscapeToolID = base.FreeIDCollector.LockID();
			ILandscapeTool newLandscapeTool = MapEditorLandscapeToolFactory.CreateRoadLandscapeTool(newLandscapeToolID, landscapeToolContext, landscapeRegion, roadLandscapeToolParams);
			if (newLandscapeTool != null)
			{
				base.InternalAddLandscapeTool(newLandscapeTool);
				return newLandscapeToolID;
			}
			base.FreeIDCollector.FreeID(newLandscapeToolID);
			return -1;
		}

		// Token: 0x06001983 RID: 6531 RVA: 0x000A70D0 File Offset: 0x000A60D0
		public int AddMapEditorLandscapeTool(LandscapeToolContext landscapeToolContext, LandscapeRegion landscapeRegion, ILandscapeParams landscapeParams)
		{
			ILandscapeToolParams leftLandscapeToolParams = landscapeParams.LandscapeToolParamsContainer.Get(true, landscapeParams.ActiveLandscapeToolParamsIndex);
			if (leftLandscapeToolParams != null)
			{
				if (MapEditorLandscapeToolFactory.IsSimpleLandscapeToolParams(leftLandscapeToolParams.LandscapeToolType))
				{
					return this.AddSimpleLandscapeTool(landscapeToolContext, landscapeRegion, landscapeParams);
				}
				if (MapEditorLandscapeToolFactory.IsWaterLandscapeToolParams(leftLandscapeToolParams.LandscapeToolType))
				{
					return this.AddWaterLandscapeTool(landscapeToolContext, landscapeRegion, landscapeParams);
				}
			}
			return -1;
		}

		// Token: 0x06001984 RID: 6532 RVA: 0x000A7122 File Offset: 0x000A6122
		public static void ClearLandscapeToolsIntermediateData(LandscapeToolContext landscapeToolContext)
		{
			landscapeToolContext.EditorScene.ClearLandscapeToolsIntermediateData();
		}

		// Token: 0x06001985 RID: 6533 RVA: 0x000A712F File Offset: 0x000A612F
		public static bool DoesLandscapeToolsIntermediateDataEmpty(LandscapeToolContext landscapeToolContext)
		{
			return landscapeToolContext.EditorScene.DoesLandscapeToolsIntermediateDataEmpty();
		}
	}
}
