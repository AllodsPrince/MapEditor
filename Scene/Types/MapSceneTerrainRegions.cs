using System;
using Tools.Landscape;

namespace MapEditor.Scene.Types
{
	// Token: 0x020000F9 RID: 249
	internal class MapSceneTerrainRegions
	{
		// Token: 0x06000C47 RID: 3143 RVA: 0x00069981 File Offset: 0x00068981
		private void OnTerrainRegionCreating(TerrainRegionContainer terrainRegionContainer, int index, int x, int y)
		{
			if (this.mapEditorScene.EditorScene != null)
			{
				this.mapEditorScene.EditorScene.NumberedTerrainRegionCreate(index, x, y);
			}
		}

		// Token: 0x06000C48 RID: 3144 RVA: 0x000699A4 File Offset: 0x000689A4
		private void OnTerrainRegionDeleting(TerrainRegionContainer terrainRegionContainer, int index, int x, int y)
		{
			if (this.mapEditorScene.EditorScene != null)
			{
				this.mapEditorScene.EditorScene.NumberedTerrainRegionDelete(index, x, y);
			}
		}

		// Token: 0x06000C49 RID: 3145 RVA: 0x000699C8 File Offset: 0x000689C8
		public void Bind(MapEditorScene _mapEditorScene)
		{
			this.mapEditorScene = _mapEditorScene;
			if (this.mapEditorScene != null && this.mapEditorScene.TerrainRegionContainer != null)
			{
				this.mapEditorScene.TerrainRegionContainer.TerrainRegionCreating += this.OnTerrainRegionCreating;
				this.mapEditorScene.TerrainRegionContainer.TerrainRegionDeleting += this.OnTerrainRegionDeleting;
			}
		}

		// Token: 0x06000C4A RID: 3146 RVA: 0x00069A2C File Offset: 0x00068A2C
		public void Unbind()
		{
			if (this.mapEditorScene != null)
			{
				if (this.mapEditorScene.TerrainRegionContainer != null)
				{
					this.mapEditorScene.TerrainRegionContainer.TerrainRegionCreating -= this.OnTerrainRegionCreating;
					this.mapEditorScene.TerrainRegionContainer.TerrainRegionDeleting -= this.OnTerrainRegionDeleting;
				}
				this.mapEditorScene = null;
			}
		}

		// Token: 0x04000A1D RID: 2589
		private MapEditorScene mapEditorScene;
	}
}
