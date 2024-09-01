using System;
using System.Collections.Generic;
using System.Drawing;
using MapInfo;
using Tools.Geometry;
using Tools.MapZoneLights;

namespace MapEditor.Scene.Types
{
	// Token: 0x02000160 RID: 352
	internal class MapSceneZones
	{
		// Token: 0x060010F8 RID: 4344 RVA: 0x0007E02C File Offset: 0x0007D02C
		private void SetZone(Tools.Geometry.Point tile, Color color, string light)
		{
			this.mapEditorScene.EditorScene.SetTileColor("Show zones", tile, color);
			if (string.IsNullOrEmpty(this.mapEditorScene.ZoneLightContainer.GetLight(tile)))
			{
				this.mapEditorScene.EditorScene.SetTileLights(tile, light);
				if (!string.IsNullOrEmpty(light))
				{
					this.mapEditorScene.EditorScene.SetTileColor("Show zone lights", tile, ZoneLightContainer.emtyColor);
					return;
				}
				this.mapEditorScene.EditorScene.SetTileColor("Show zone lights", tile, ZoneLights.notLightedColor);
			}
		}

		// Token: 0x060010F9 RID: 4345 RVA: 0x0007E0BC File Offset: 0x0007D0BC
		private void OnMapZoneAppliedToTiles(MapZoneContainer mapZoneContainer, List<MapZoneContainer.ZonedTile> zonedTiles, string _zone)
		{
			if (this.mapEditorScene.EditorScene != null)
			{
				Color color = this.mapEditorScene.MapZoneContainer.GetZoneColor(_zone);
				string zoneLights = this.mapEditorScene.MapZoneContainer.GetZoneLights(_zone);
				foreach (MapZoneContainer.ZonedTile tile in zonedTiles)
				{
					this.SetZone(tile.Tile, color, zoneLights);
				}
			}
		}

		// Token: 0x060010FA RID: 4346 RVA: 0x0007E144 File Offset: 0x0007D144
		private void OnMapZoneRefeshed(string zone, List<Tools.Geometry.Point> tiles)
		{
			if (this.mapEditorScene.EditorScene != null && !string.IsNullOrEmpty(zone) && tiles != null)
			{
				Color color = this.mapEditorScene.MapZoneContainer.GetZoneColor(zone);
				string zoneLights = this.mapEditorScene.MapZoneContainer.GetZoneLights(zone);
				foreach (Tools.Geometry.Point tile in tiles)
				{
					this.SetZone(tile, color, zoneLights);
				}
			}
		}

		// Token: 0x060010FB RID: 4347 RVA: 0x0007E1D0 File Offset: 0x0007D1D0
		public void Bind(MapEditorScene _mapEditorScene)
		{
			this.mapEditorScene = _mapEditorScene;
			if (this.mapEditorScene != null && this.mapEditorScene.MapZoneContainer != null)
			{
				this.mapEditorScene.MapZoneContainer.ZoneAppliedToTiles += this.OnMapZoneAppliedToTiles;
				this.mapEditorScene.MapZoneContainer.Refreshed += this.OnMapZoneRefeshed;
			}
		}

		// Token: 0x060010FC RID: 4348 RVA: 0x0007E234 File Offset: 0x0007D234
		public void Unbind()
		{
			if (this.mapEditorScene != null)
			{
				if (this.mapEditorScene.MapZoneContainer != null)
				{
					this.mapEditorScene.MapZoneContainer.ZoneAppliedToTiles -= this.OnMapZoneAppliedToTiles;
					this.mapEditorScene.MapZoneContainer.Refreshed -= this.OnMapZoneRefeshed;
				}
				this.mapEditorScene = null;
			}
		}

		// Token: 0x04000C54 RID: 3156
		private const string lightmapKey = "Show zones";

		// Token: 0x04000C55 RID: 3157
		private MapEditorScene mapEditorScene;
	}
}
