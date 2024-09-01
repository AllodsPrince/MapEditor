using System;
using System.Collections.Generic;
using MapEditor.Map;
using MapInfo;
using Tools.Geometry;
using Tools.MapObjects;
using Tools.MapSound;
using Tools.MapZoneLights;

namespace MapEditor.Scene
{
	// Token: 0x0200019E RID: 414
	public class EditingBlocker
	{
		// Token: 0x06001432 RID: 5170 RVA: 0x00092520 File Offset: 0x00091520
		private bool PositionIsBlocked(ref Position position)
		{
			int x = (int)position.X / Constants.PatchSize - this.map.Data.MinXMinYPatchCoords.X;
			int y = (int)position.Y / Constants.PatchSize - this.map.Data.MinXMinYPatchCoords.Y;
			return this.PatchIsBlocked(x, y);
		}

		// Token: 0x06001433 RID: 5171 RVA: 0x00092584 File Offset: 0x00091584
		private void OnMapObjectChanging(MapObjectContainer container, IMapObject mapObject, MapObject.FieldChangingArgs args, ref bool cancel)
		{
			if (args != null && this.patches != null && mapObject != null && !cancel && (args.ChangingField == MapObject.MapObjectFiels.Position || args.ChangingField == MapObject.MapObjectFiels.Rotation || args.ChangingField == MapObject.MapObjectFiels.Scale))
			{
				Position position = (args.ChangingField == MapObject.MapObjectFiels.Position && args.NewValue is Position) ? ((Position)args.NewValue) : mapObject.Position;
				cancel = this.PositionIsBlocked(ref position);
			}
		}

		// Token: 0x06001434 RID: 5172 RVA: 0x000925F4 File Offset: 0x000915F4
		private void OnMapObjectAdding(MapObjectContainer container, IMapObject mapObject, ref bool cancel)
		{
			if (this.patches != null && mapObject != null && !cancel)
			{
				Position position = mapObject.Position;
				cancel = this.PositionIsBlocked(ref position);
			}
		}

		// Token: 0x06001435 RID: 5173 RVA: 0x00092624 File Offset: 0x00091624
		private void CheckZoneTiles(IList<Point> tiles)
		{
			if (this.patches != null && tiles != null)
			{
				int count = tiles.Count;
				for (int index = count - 1; index >= 0; index--)
				{
					Point tile = tiles[index];
					if (this.PatchIsBlocked(tile.X / EditingBlocker.zoneToPathCoeff, tile.Y / EditingBlocker.zoneToPathCoeff))
					{
						tiles.RemoveAt(index);
					}
				}
			}
		}

		// Token: 0x06001436 RID: 5174 RVA: 0x00092682 File Offset: 0x00091682
		private void OnZoneApplying(MapZoneContainer zoneContainer, List<Point> tiles, string zone)
		{
			this.CheckZoneTiles(tiles);
		}

		// Token: 0x06001437 RID: 5175 RVA: 0x0009268B File Offset: 0x0009168B
		private void OnLightSetting(ZoneLightContainer container, List<Point> tiles, string light)
		{
			this.CheckZoneTiles(tiles);
		}

		// Token: 0x06001438 RID: 5176 RVA: 0x00092694 File Offset: 0x00091694
		private void OnSoundSetting(MapSoundContainer container, List<Point> tiles, string light)
		{
			this.CheckZoneTiles(tiles);
		}

		// Token: 0x06001439 RID: 5177 RVA: 0x000926A0 File Offset: 0x000916A0
		private void BlockAll(bool block)
		{
			if (this.patches != null)
			{
				for (int x = 0; x < this.map.Data.MapSize.X; x++)
				{
					for (int y = 0; y < this.map.Data.MapSize.Y; y++)
					{
						if (this.patches[x, y] != block)
						{
							this.BlockPatch(x, y, block);
						}
					}
				}
			}
		}

		// Token: 0x0600143A RID: 5178 RVA: 0x00092714 File Offset: 0x00091714
		public EditingBlocker(MapEditorMap _map, EditorScene _editorScene)
		{
			this.map = _map;
			this.editorScene = _editorScene;
		}

		// Token: 0x0600143B RID: 5179 RVA: 0x0009272C File Offset: 0x0009172C
		public void StartBlocking()
		{
			this.patches = new bool[this.map.Data.MapSize.X, this.map.Data.MapSize.Y];
			this.map.MapEditorMapObjectContainer.Changing += this.OnMapObjectChanging;
			this.map.MapEditorMapObjectContainer.MapObjectAdding += this.OnMapObjectAdding;
			this.map.MapZoneContainer.ZoneApplyingToTiles += this.OnZoneApplying;
			this.map.ZoneLightContainer.LightSetting += this.OnLightSetting;
			this.map.MapAmbienceContainer.SoundSetting += this.OnSoundSetting;
			this.map.MapMusicContainer.SoundSetting += this.OnSoundSetting;
			this.BlockAll(true);
		}

		// Token: 0x0600143C RID: 5180 RVA: 0x00092824 File Offset: 0x00091824
		public void EndBlocking()
		{
			if (this.patches != null)
			{
				this.BlockAll(false);
				this.patches = null;
			}
			this.map.MapEditorMapObjectContainer.Changing -= this.OnMapObjectChanging;
			this.map.MapEditorMapObjectContainer.MapObjectAdding -= this.OnMapObjectAdding;
			this.map.MapZoneContainer.ZoneApplyingToTiles -= this.OnZoneApplying;
			this.map.ZoneLightContainer.LightSetting -= this.OnLightSetting;
			this.map.MapAmbienceContainer.SoundSetting -= this.OnSoundSetting;
			this.map.MapMusicContainer.SoundSetting -= this.OnSoundSetting;
		}

		// Token: 0x0600143D RID: 5181 RVA: 0x000928EF File Offset: 0x000918EF
		public void BlockPatch(int x, int y, bool block)
		{
			this.patches[x, y] = block;
			if (this.editorScene != null)
			{
				this.editorScene.BlockTerrainPatch(x, y, block);
			}
		}

		// Token: 0x0600143E RID: 5182 RVA: 0x00092915 File Offset: 0x00091915
		public bool PatchIsBlocked(int x, int y)
		{
			return x > -1 && y > -1 && x < this.patches.GetLength(0) && y < this.patches.GetLength(1) && this.patches[x, y];
		}

		// Token: 0x04000E4A RID: 3658
		private readonly MapEditorMap map;

		// Token: 0x04000E4B RID: 3659
		private readonly EditorScene editorScene;

		// Token: 0x04000E4C RID: 3660
		private bool[,] patches;

		// Token: 0x04000E4D RID: 3661
		private static readonly int zoneToPathCoeff = Constants.PatchSize / Constants.ZoneGranularity;
	}
}
