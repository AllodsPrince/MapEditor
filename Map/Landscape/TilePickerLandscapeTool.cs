using System;
using System.Collections.Generic;
using Tools.Geometry;
using Tools.Landscape;
using Tools.Landscape.LandscapeToolParams;

namespace MapEditor.Map.Landscape
{
	// Token: 0x02000180 RID: 384
	public class TilePickerLandscapeTool : LandscapeTool
	{
		// Token: 0x0600124E RID: 4686 RVA: 0x0008548E File Offset: 0x0008448E
		public TilePickerLandscapeTool(int _id) : base(_id)
		{
			base.Temporary = true;
		}

		// Token: 0x0600124F RID: 4687 RVA: 0x0008549E File Offset: 0x0008449E
		public override void Create()
		{
		}

		// Token: 0x06001250 RID: 4688 RVA: 0x000854A0 File Offset: 0x000844A0
		public override void Apply()
		{
			if (base.LandscapeToolContext != null)
			{
				TilePickerLandscapeToolParams tilePickerLandscapeToolParams = base.LandscapeToolParams as TilePickerLandscapeToolParams;
				if (tilePickerLandscapeToolParams != null)
				{
					Point terrainPosition = base.LandscapeToolContext.LandscapeToolContextPosition.TerrainPosition;
					List<EditorScene.TileInfo> tiles = new List<EditorScene.TileInfo>();
					base.LandscapeToolContext.EditorScene.GetTerrainTiles(base.LandscapeToolContext.EditorSceneViewID, ref terrainPosition, ref tiles);
					tilePickerLandscapeToolParams.Tiles = tiles;
				}
			}
		}

		// Token: 0x06001251 RID: 4689 RVA: 0x00085502 File Offset: 0x00084502
		public override void Destroy()
		{
		}
	}
}
