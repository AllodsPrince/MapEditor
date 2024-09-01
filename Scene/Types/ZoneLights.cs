using System;
using System.Collections.Generic;
using System.Drawing;
using Tools.Geometry;
using Tools.MapZoneLights;

namespace MapEditor.Scene.Types
{
	// Token: 0x020001BB RID: 443
	internal class ZoneLights
	{
		// Token: 0x06001570 RID: 5488 RVA: 0x0009C14C File Offset: 0x0009B14C
		private void SetLight(Tools.Geometry.Point tile, Color color, string light)
		{
			string zone = this.mapEditorScene.MapZoneContainer.GetZone(tile);
			if (!string.IsNullOrEmpty(light))
			{
				this.mapEditorScene.EditorScene.SetTileColor("Show zone lights", tile, color);
				this.mapEditorScene.EditorScene.SetTileLights(tile, light);
				return;
			}
			if (!string.IsNullOrEmpty(zone))
			{
				light = this.mapEditorScene.MapZoneContainer.GetZoneLights(zone);
				this.mapEditorScene.EditorScene.SetTileColor("Show zone lights", tile, color);
			}
			else
			{
				this.mapEditorScene.EditorScene.SetTileColor("Show zone lights", tile, ZoneLights.notLightedColor);
			}
			this.mapEditorScene.EditorScene.SetTileLights(tile, light);
		}

		// Token: 0x06001571 RID: 5489 RVA: 0x0009C200 File Offset: 0x0009B200
		private void OnLightSetted(ZoneLightContainer zoneLightContainer, List<ZoneLightContainer.LightedTile> lightedTiles, string light)
		{
			if (this.mapEditorScene.EditorScene != null && lightedTiles != null)
			{
				Color color = this.mapEditorScene.ZoneLightContainer.GetLightColor(light);
				foreach (ZoneLightContainer.LightedTile tile in lightedTiles)
				{
					this.SetLight(tile.Tile, color, light);
				}
			}
		}

		// Token: 0x06001572 RID: 5490 RVA: 0x0009C278 File Offset: 0x0009B278
		private void OnLightUpdated(string light, List<Tools.Geometry.Point> tiles)
		{
			if (!string.IsNullOrEmpty(light) && tiles != null && this.mapEditorScene.EditorScene != null)
			{
				Color color = this.mapEditorScene.ZoneLightContainer.GetLightColor(light);
				foreach (Tools.Geometry.Point tile in tiles)
				{
					this.SetLight(tile, color, light);
				}
			}
		}

		// Token: 0x06001573 RID: 5491 RVA: 0x0009C2F4 File Offset: 0x0009B2F4
		public void Bind(MapEditorScene _mapEditorScene)
		{
			this.mapEditorScene = _mapEditorScene;
			if (this.mapEditorScene != null && this.mapEditorScene.ZoneLightContainer != null)
			{
				this.mapEditorScene.ZoneLightContainer.LightSetted += this.OnLightSetted;
				this.mapEditorScene.ZoneLightContainer.LightUpdated += this.OnLightUpdated;
			}
		}

		// Token: 0x06001574 RID: 5492 RVA: 0x0009C358 File Offset: 0x0009B358
		public void Unbind()
		{
			if (this.mapEditorScene != null)
			{
				if (this.mapEditorScene.ZoneLightContainer != null)
				{
					this.mapEditorScene.ZoneLightContainer.LightSetted -= this.OnLightSetted;
					this.mapEditorScene.ZoneLightContainer.LightUpdated -= this.OnLightUpdated;
				}
				this.mapEditorScene = null;
			}
		}

		// Token: 0x04000F1E RID: 3870
		public const string lightmapKey = "Show zone lights";

		// Token: 0x04000F1F RID: 3871
		public static readonly Color notLightedColor = Color.Black;

		// Token: 0x04000F20 RID: 3872
		private MapEditorScene mapEditorScene;
	}
}
