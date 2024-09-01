using System;

namespace MapEditor.Scene.Types
{
	// Token: 0x0200021C RID: 540
	internal class MapSceneFixTerrainTiles
	{
		// Token: 0x06001A1B RID: 6683 RVA: 0x000ABFD9 File Offset: 0x000AAFD9
		private void UpdateScene()
		{
			if (this.mapEditorScene != null)
			{
				this.mapEditorScene.EditorScene.SetFixTerrainTiles(this.mapEditorScene.MapSceneParams.FixTerrainTiles);
			}
		}

		// Token: 0x06001A1C RID: 6684 RVA: 0x000AC003 File Offset: 0x000AB003
		private void OnFixTerrainTilesChanged(MapSceneParams mapSceneParams, ref bool oldValue, ref bool newValue)
		{
			this.UpdateScene();
		}

		// Token: 0x06001A1D RID: 6685 RVA: 0x000AC00B File Offset: 0x000AB00B
		public void Bind(MapEditorScene _mapEditorScene)
		{
			this.mapEditorScene = _mapEditorScene;
			if (this.mapEditorScene != null)
			{
				this.mapEditorScene.MapSceneParams.FixTerrainTilesChanged += this.OnFixTerrainTilesChanged;
				this.UpdateScene();
			}
		}

		// Token: 0x06001A1E RID: 6686 RVA: 0x000AC03E File Offset: 0x000AB03E
		public void Unbind()
		{
			if (this.mapEditorScene != null)
			{
				this.mapEditorScene.MapSceneParams.FixTerrainTilesChanged -= this.OnFixTerrainTilesChanged;
			}
		}

		// Token: 0x040010FF RID: 4351
		private MapEditorScene mapEditorScene;
	}
}
