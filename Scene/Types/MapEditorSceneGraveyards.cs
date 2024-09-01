using System;
using MapEditor.Map.Containers;
using MapEditor.Map.MapObjects;

namespace MapEditor.Scene.Types
{
	// Token: 0x020001EF RID: 495
	internal class MapEditorSceneGraveyards
	{
		// Token: 0x060018D2 RID: 6354 RVA: 0x000A520F File Offset: 0x000A420F
		private void OnGraveyardRespawnTypeChanged(MapEditorMapObjectContainer mapEditorMapObjectContainer, Graveyard graveyard, ref RespawnType oldValue, ref RespawnType newValue)
		{
			this.mapSceneObjects.RecreateMapObject(graveyard, false);
		}

		// Token: 0x060018D3 RID: 6355 RVA: 0x000A5220 File Offset: 0x000A4220
		public void Bind(MapEditorScene _mapEditorScene, MapSceneObjects _mapSceneObjects)
		{
			this.mapEditorScene = _mapEditorScene;
			this.mapSceneObjects = _mapSceneObjects;
			if (this.mapEditorScene != null && this.mapSceneObjects != null)
			{
				GraveyardContainer graveyardContainer = this.mapEditorScene.MapEditorMapObjectContainer.GraveyardContainer;
				if (graveyardContainer != null)
				{
					graveyardContainer.GraveyardRespawnTypeChanged += this.OnGraveyardRespawnTypeChanged;
				}
			}
		}

		// Token: 0x060018D4 RID: 6356 RVA: 0x000A5274 File Offset: 0x000A4274
		public void Unbind()
		{
			if (this.mapEditorScene != null)
			{
				GraveyardContainer graveyardContainer = this.mapEditorScene.MapEditorMapObjectContainer.GraveyardContainer;
				if (graveyardContainer != null)
				{
					graveyardContainer.GraveyardRespawnTypeChanged -= this.OnGraveyardRespawnTypeChanged;
				}
			}
			this.mapEditorScene = null;
			this.mapSceneObjects = null;
		}

		// Token: 0x04001018 RID: 4120
		private MapEditorScene mapEditorScene;

		// Token: 0x04001019 RID: 4121
		private MapSceneObjects mapSceneObjects;
	}
}
