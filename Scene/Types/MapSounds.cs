using System;
using System.Collections.Generic;
using System.Drawing;
using Tools.Geometry;
using Tools.MapSound;

namespace MapEditor.Scene.Types
{
	// Token: 0x02000058 RID: 88
	internal class MapSounds
	{
		// Token: 0x06000477 RID: 1143 RVA: 0x0002460C File Offset: 0x0002360C
		private string GetLightMap(MapSoundContainer mapSoundContainer)
		{
			if (mapSoundContainer == this.mapEditorScene.MapAmbienceContainer)
			{
				return "Show_sound_ambience";
			}
			return "Show_sound_music";
		}

		// Token: 0x06000478 RID: 1144 RVA: 0x00024628 File Offset: 0x00023628
		private void OnSoundSetted(MapSoundContainer mapSoundContainer, List<MapSoundContainer.SoundedTile> soundedTiles, string sound)
		{
			if (this.mapEditorScene.EditorScene != null && mapSoundContainer != null && soundedTiles != null)
			{
				Color color = mapSoundContainer.GetSoundColor(sound);
				foreach (MapSoundContainer.SoundedTile tile in soundedTiles)
				{
					this.mapEditorScene.EditorScene.SetTileColor(this.GetLightMap(mapSoundContainer), tile.Tile, color);
				}
			}
		}

		// Token: 0x06000479 RID: 1145 RVA: 0x000246A8 File Offset: 0x000236A8
		private void OnSoundUpdated(MapSoundContainer mapSoundContainer, List<Tools.Geometry.Point> tiles, string sound)
		{
			if (!string.IsNullOrEmpty(sound) && mapSoundContainer != null && tiles != null)
			{
				Color color = mapSoundContainer.GetSoundColor(sound);
				foreach (Tools.Geometry.Point tile in tiles)
				{
					this.mapEditorScene.EditorScene.SetTileColor(this.GetLightMap(mapSoundContainer), tile, color);
				}
			}
		}

		// Token: 0x0600047A RID: 1146 RVA: 0x00024720 File Offset: 0x00023720
		public void Bind(MapEditorScene _mapEditorScene)
		{
			this.mapEditorScene = _mapEditorScene;
			if (this.mapEditorScene != null)
			{
				if (this.mapEditorScene.MapMusicContainer != null)
				{
					this.mapEditorScene.MapMusicContainer.SoundSetted += this.OnSoundSetted;
					this.mapEditorScene.MapMusicContainer.SoundUpdated += this.OnSoundUpdated;
				}
				if (this.mapEditorScene.MapAmbienceContainer != null)
				{
					this.mapEditorScene.MapAmbienceContainer.SoundSetted += this.OnSoundSetted;
					this.mapEditorScene.MapAmbienceContainer.SoundUpdated += this.OnSoundUpdated;
				}
			}
		}

		// Token: 0x0600047B RID: 1147 RVA: 0x000247CC File Offset: 0x000237CC
		public void Unbind()
		{
			if (this.mapEditorScene != null)
			{
				if (this.mapEditorScene.MapMusicContainer != null)
				{
					this.mapEditorScene.MapMusicContainer.SoundSetted -= this.OnSoundSetted;
					this.mapEditorScene.MapMusicContainer.SoundUpdated -= this.OnSoundUpdated;
				}
				if (this.mapEditorScene.MapAmbienceContainer != null)
				{
					this.mapEditorScene.MapAmbienceContainer.SoundSetted -= this.OnSoundSetted;
					this.mapEditorScene.MapAmbienceContainer.SoundUpdated -= this.OnSoundUpdated;
				}
				this.mapEditorScene = null;
			}
		}

		// Token: 0x04000311 RID: 785
		private MapEditorScene mapEditorScene;
	}
}
