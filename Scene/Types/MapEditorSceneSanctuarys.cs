using System;
using MapEditor.Map.Containers;
using MapEditor.Map.MapObjects;

namespace MapEditor.Scene.Types
{
	// Token: 0x02000246 RID: 582
	internal class MapEditorSceneSanctuarys
	{
		// Token: 0x06001BC6 RID: 7110 RVA: 0x000B54FB File Offset: 0x000B44FB
		private void OnSanctuaryRespawnTypeChanged(MapEditorMapObjectContainer mapEditorMapObjectContainer, Sanctuary sanctuary, ref RespawnType oldValue, ref RespawnType newValue)
		{
			this.mapSceneObjects.RecreateMapObject(sanctuary, false);
		}

		// Token: 0x06001BC7 RID: 7111 RVA: 0x000B550C File Offset: 0x000B450C
		public void Bind(MapEditorScene _mapEditorScene, MapSceneObjects _mapSceneObjects)
		{
			this.mapEditorScene = _mapEditorScene;
			this.mapSceneObjects = _mapSceneObjects;
			if (this.mapEditorScene != null && this.mapSceneObjects != null)
			{
				SanctuaryContainer sanctuaryContainer = this.mapEditorScene.MapEditorMapObjectContainer.SanctuaryContainer;
				if (sanctuaryContainer != null)
				{
					sanctuaryContainer.SanctuaryRespawnTypeChanged += this.OnSanctuaryRespawnTypeChanged;
				}
			}
		}

		// Token: 0x06001BC8 RID: 7112 RVA: 0x000B5560 File Offset: 0x000B4560
		public void Unbind()
		{
			if (this.mapEditorScene != null)
			{
				SanctuaryContainer sanctuaryContainer = this.mapEditorScene.MapEditorMapObjectContainer.SanctuaryContainer;
				if (sanctuaryContainer != null)
				{
					sanctuaryContainer.SanctuaryRespawnTypeChanged -= this.OnSanctuaryRespawnTypeChanged;
				}
			}
			this.mapEditorScene = null;
			this.mapSceneObjects = null;
		}

		// Token: 0x040011FA RID: 4602
		private MapEditorScene mapEditorScene;

		// Token: 0x040011FB RID: 4603
		private MapSceneObjects mapSceneObjects;
	}
}
