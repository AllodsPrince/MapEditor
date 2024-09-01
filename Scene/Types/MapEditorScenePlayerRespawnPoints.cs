using System;
using MapEditor.Map.Containers;
using MapEditor.Map.MapObjects;

namespace MapEditor.Scene.Types
{
	// Token: 0x02000271 RID: 625
	internal class MapEditorScenePlayerRespawnPoints
	{
		// Token: 0x06001D7F RID: 7551 RVA: 0x000BC8B6 File Offset: 0x000BB8B6
		private void OnPlayerRespawnPlaceMobSceneNameChanged(MapEditorMapObjectContainer mapEditorMapObjectContainer, PlayerRespawnPlace playerRespawnPlace, ref string oldValue, ref string newValue)
		{
			this.mapSceneObjects.RecreateMapObject(playerRespawnPlace, false);
		}

		// Token: 0x06001D80 RID: 7552 RVA: 0x000BC8C8 File Offset: 0x000BB8C8
		public void Bind(MapEditorScene _mapEditorScene, MapSceneObjects _mapSceneObjects)
		{
			this.mapEditorScene = _mapEditorScene;
			this.mapSceneObjects = _mapSceneObjects;
			if (this.mapEditorScene != null && this.mapSceneObjects != null)
			{
				PlayerRespawnPlaceContainer playerRespawnPlaceContainer = this.mapEditorScene.MapEditorMapObjectContainer.PlayerRespawnPlaceContainer;
				if (playerRespawnPlaceContainer != null)
				{
					playerRespawnPlaceContainer.PlayerRespawnPlaceMobSceneNameChanged += this.OnPlayerRespawnPlaceMobSceneNameChanged;
				}
			}
		}

		// Token: 0x06001D81 RID: 7553 RVA: 0x000BC91C File Offset: 0x000BB91C
		public void Unbind()
		{
			if (this.mapEditorScene != null)
			{
				PlayerRespawnPlaceContainer playerRespawnPlaceContainer = this.mapEditorScene.MapEditorMapObjectContainer.PlayerRespawnPlaceContainer;
				if (playerRespawnPlaceContainer != null)
				{
					playerRespawnPlaceContainer.PlayerRespawnPlaceMobSceneNameChanged -= this.OnPlayerRespawnPlaceMobSceneNameChanged;
				}
				this.mapEditorScene = null;
			}
			this.mapSceneObjects = null;
		}

		// Token: 0x040012BF RID: 4799
		private MapEditorScene mapEditorScene;

		// Token: 0x040012C0 RID: 4800
		private MapSceneObjects mapSceneObjects;
	}
}
