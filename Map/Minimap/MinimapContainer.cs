using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using InputState;
using MapInfo;
using Operations;
using Tools.Geometry;
using Tools.Landscape;
using Tools.MapSound;
using Tools.MapZoneLights;

namespace MapEditor.Map.Minimap
{
	// Token: 0x020000FF RID: 255
	public class MinimapContainer
	{
		// Token: 0x06000CC8 RID: 3272 RVA: 0x0006CB58 File Offset: 0x0006BB58
		private static bool CheckForMinimapChanges(IOperation operation)
		{
			return operation != null && (operation.ContainsLabel("LandscapeToolContainer") || operation.ContainsLabel("MapZoneContainer") || operation.ContainsLabel("ZoneLightContainer") || operation.ContainsLabel("MapSoundContainer"));
		}

		// Token: 0x06000CC9 RID: 3273 RVA: 0x0006CB93 File Offset: 0x0006BB93
		private void UpdateChangedRectangle(ref Rectangle _changedRectangle)
		{
			if (this.changed)
			{
				this.changedRectangle = Rectangle.Union(this.changedRectangle, _changedRectangle);
				return;
			}
			this.changedRectangle = _changedRectangle;
			this.changed = true;
		}

		// Token: 0x06000CCA RID: 3274 RVA: 0x0006CBC8 File Offset: 0x0006BBC8
		private void OnLandscapeToolApplied(LandscapeToolContainer _landscapeToolContainer, ILandscapeTool landscapeTool)
		{
			if (landscapeTool != null && landscapeTool.AffectParams != null && !landscapeTool.AffectParams.AffectedRect.IsEmpty)
			{
				Rectangle rectangle = new Rectangle(landscapeTool.AffectParams.AffectedRect.Min.X, this.mapSize.Height - landscapeTool.AffectParams.AffectedRect.Max.Y - 1, landscapeTool.AffectParams.AffectedRect.Max.X - landscapeTool.AffectParams.AffectedRect.Min.X + 1, landscapeTool.AffectParams.AffectedRect.Max.Y - landscapeTool.AffectParams.AffectedRect.Min.Y + 1);
				this.UpdateChangedRectangle(ref rectangle);
			}
		}

		// Token: 0x06000CCB RID: 3275 RVA: 0x0006CCD8 File Offset: 0x0006BCD8
		private void OnZoneAppliedToTiles(MapZoneContainer _mapZoneContainer, List<MapZoneContainer.ZonedTile> tiles, string _zone)
		{
			foreach (MapZoneContainer.ZonedTile tile in tiles)
			{
				Rectangle rectangle = new Rectangle(tile.Tile.X * Constants.ZoneGranularity, tile.Tile.Y * Constants.ZoneGranularity, (tile.Tile.X + 1) * Constants.ZoneGranularity, (tile.Tile.Y + 1) * Constants.ZoneGranularity);
				this.UpdateChangedRectangle(ref rectangle);
			}
		}

		// Token: 0x06000CCC RID: 3276 RVA: 0x0006CD8C File Offset: 0x0006BD8C
		private void OnLightAppliedToTiles(ZoneLightContainer _zoneLightContainer, List<ZoneLightContainer.LightedTile> tiles, string _light)
		{
			foreach (ZoneLightContainer.LightedTile tile in tiles)
			{
				Rectangle rectangle = new Rectangle(tile.Tile.X * Constants.ZoneGranularity, tile.Tile.Y * Constants.ZoneGranularity, (tile.Tile.X + 1) * Constants.ZoneGranularity, (tile.Tile.Y + 1) * Constants.ZoneGranularity);
				this.UpdateChangedRectangle(ref rectangle);
			}
		}

		// Token: 0x06000CCD RID: 3277 RVA: 0x0006CE40 File Offset: 0x0006BE40
		private void OnSoundAppliedToTiles(MapSoundContainer mapSoundContainer, List<MapSoundContainer.SoundedTile> tiles, string sound)
		{
			foreach (MapSoundContainer.SoundedTile tile in tiles)
			{
				Rectangle rectangle = new Rectangle(tile.Tile.X * Constants.ZoneGranularity, tile.Tile.Y * Constants.ZoneGranularity, (tile.Tile.X + 1) * Constants.ZoneGranularity, (tile.Tile.Y + 1) * Constants.ZoneGranularity);
				this.UpdateChangedRectangle(ref rectangle);
			}
		}

		// Token: 0x06000CCE RID: 3278 RVA: 0x0006CEF4 File Offset: 0x0006BEF4
		private void OnUndo(OperationContainer _operationContainer, IOperation operation, int index, bool result)
		{
			if (result && MinimapContainer.CheckForMinimapChanges(operation))
			{
				this.Repaint();
			}
		}

		// Token: 0x06000CCF RID: 3279 RVA: 0x0006CF08 File Offset: 0x0006BF08
		private void OnRedo(OperationContainer _operationContainer, IOperation operation, int index, bool result)
		{
			if (result && MinimapContainer.CheckForMinimapChanges(operation))
			{
				this.Repaint();
			}
		}

		// Token: 0x06000CD0 RID: 3280 RVA: 0x0006CF1C File Offset: 0x0006BF1C
		private void OnMinimapRefresh(object sender)
		{
			MinimapContainer.IMinimapPainter painter = sender as MinimapContainer.IMinimapPainter;
			if (painter != null && sender == this.activePainter)
			{
				this.context.StateContainer.Invoke("_minimap_refresh", default(MethodArgs));
			}
		}

		// Token: 0x06000CD1 RID: 3281 RVA: 0x0006CF5A File Offset: 0x0006BF5A
		private void ClearChanges()
		{
			if (this.changed)
			{
				this.changedRectangle = new Rectangle(0, 0, 0, 0);
				this.changed = false;
			}
		}

		// Token: 0x06000CD2 RID: 3282 RVA: 0x0006CF7C File Offset: 0x0006BF7C
		public MinimapContainer(MapEditorMap _mapEditorMap, OperationContainer _operationContainer, MainForm.Context _context)
		{
			this.context = _context;
			this.mapEditorMap = _mapEditorMap;
			this.operationContainer = _operationContainer;
			if (this.mapEditorMap != null)
			{
				if (this.mapEditorMap.MapEditorLandscapeToolContainer != null)
				{
					this.mapEditorMap.MapEditorLandscapeToolContainer.LandscapeToolApplied += this.OnLandscapeToolApplied;
				}
				if (this.mapEditorMap.MapZoneContainer != null)
				{
					this.mapEditorMap.MapZoneContainer.ZoneAppliedToTiles += this.OnZoneAppliedToTiles;
				}
				if (this.mapEditorMap.ZoneLightContainer != null)
				{
					this.mapEditorMap.ZoneLightContainer.LightSetted += this.OnLightAppliedToTiles;
				}
				if (this.mapEditorMap.MapAmbienceContainer != null)
				{
					this.mapEditorMap.MapAmbienceContainer.SoundSetted += this.OnSoundAppliedToTiles;
				}
				if (this.mapEditorMap.MapMusicContainer != null)
				{
					this.mapEditorMap.MapMusicContainer.SoundSetted += this.OnSoundAppliedToTiles;
				}
				if (this.operationContainer != null)
				{
					this.operationContainer.OperationRedoInvoked += this.OnRedo;
					this.operationContainer.OperationUndoInvoked += this.OnUndo;
				}
				int width = this.mapEditorMap.Data.MapSize.X * Constants.PatchSize;
				int height = this.mapEditorMap.Data.MapSize.Y * Constants.PatchSize;
				this.mapSize = new Size(width, height);
				this.terrainMinimap = new MinimapContainer.LayerMinimap(this.mapSize, this.context.EditorScene, 0, this.mapEditorMap.Data.MapSize);
				this.lowerTerrainMinimap = new MinimapContainer.LayerMinimap(this.mapSize, this.context.EditorScene, 1, this.mapEditorMap.Data.MapSize);
				this.heightMinimap = new MinimapContainer.HeightMinimap(this.mapSize, this.context.EditorScene, 0, this.mapEditorMap.Data.MapSize);
				this.lowerHeightMinimap = new MinimapContainer.HeightMinimap(this.mapSize, this.context.EditorScene, 1, this.mapEditorMap.Data.MapSize);
				this.zoneMinimap = new MinimapContainer.ZoneMinimap(_mapEditorMap.MapZoneContainer, this.mapSize, this.context.EditorScene, 0, this.mapEditorMap.Data.MapSize);
				this.lightMinimap = new MinimapContainer.LightMinimap(_mapEditorMap.ZoneLightContainer, this.mapSize, this.context.EditorScene, 0, this.mapEditorMap.Data.MapSize);
				this.ambientMinimap = new MinimapContainer.SoundMinimap(_mapEditorMap.MapAmbienceContainer, this.mapSize, this.context.EditorScene, 0, this.mapEditorMap.Data.MapSize);
				this.musicMinimap = new MinimapContainer.SoundMinimap(_mapEditorMap.MapMusicContainer, this.mapSize, this.context.EditorScene, 0, this.mapEditorMap.Data.MapSize);
				this.terrainMinimap.Painted += this.OnMinimapRefresh;
				this.lowerTerrainMinimap.Painted += this.OnMinimapRefresh;
				this.heightMinimap.Painted += this.OnMinimapRefresh;
				this.lowerHeightMinimap.Painted += this.OnMinimapRefresh;
				this.zoneMinimap.Painted += this.OnMinimapRefresh;
				this.lightMinimap.Painted += this.OnMinimapRefresh;
				this.ambientMinimap.Painted += this.OnMinimapRefresh;
				this.musicMinimap.Painted += this.OnMinimapRefresh;
			}
		}

		// Token: 0x06000CD3 RID: 3283 RVA: 0x0006D344 File Offset: 0x0006C344
		public void Destroy()
		{
			if (this.mapEditorMap != null)
			{
				if (this.mapEditorMap.MapEditorLandscapeToolContainer != null)
				{
					this.mapEditorMap.MapEditorLandscapeToolContainer.LandscapeToolApplied -= this.OnLandscapeToolApplied;
				}
				if (this.mapEditorMap.MapZoneContainer != null)
				{
					this.mapEditorMap.MapZoneContainer.ZoneAppliedToTiles -= this.OnZoneAppliedToTiles;
				}
				if (this.mapEditorMap.ZoneLightContainer != null)
				{
					this.mapEditorMap.ZoneLightContainer.LightSetted -= this.OnLightAppliedToTiles;
				}
				if (this.mapEditorMap.MapAmbienceContainer != null)
				{
					this.mapEditorMap.MapAmbienceContainer.SoundSetted -= this.OnSoundAppliedToTiles;
				}
				if (this.mapEditorMap.MapMusicContainer != null)
				{
					this.mapEditorMap.MapMusicContainer.SoundSetted -= this.OnSoundAppliedToTiles;
				}
				this.mapEditorMap = null;
			}
			if (this.operationContainer != null)
			{
				this.operationContainer.OperationRedoInvoked -= this.OnRedo;
				this.operationContainer.OperationUndoInvoked -= this.OnUndo;
				this.operationContainer = null;
			}
		}

		// Token: 0x06000CD4 RID: 3284 RVA: 0x0006D46D File Offset: 0x0006C46D
		public void Repaint()
		{
			if (this.activePainter != null)
			{
				this.activePainter.RepaintAll();
			}
			this.ClearChanges();
		}

		// Token: 0x06000CD5 RID: 3285 RVA: 0x0006D488 File Offset: 0x0006C488
		public void AppyChanges()
		{
			if (this.changed && this.activePainter != null)
			{
				this.activePainter.Paint(this.changedRectangle);
			}
			this.ClearChanges();
		}

		// Token: 0x06000CD6 RID: 3286 RVA: 0x0006D4B1 File Offset: 0x0006C4B1
		public void UndoChanges()
		{
			this.ClearChanges();
		}

		// Token: 0x06000CD7 RID: 3287 RVA: 0x0006D4BC File Offset: 0x0006C4BC
		public void SetMinimapMode(string mode)
		{
			if (!string.IsNullOrEmpty(mode))
			{
				MinimapContainer.IMinimapPainter newPainter = null;
				switch (mode)
				{
				case "TerrainMode":
					newPainter = this.terrainMinimap;
					break;
				case "LowerTerrainMode":
					newPainter = this.lowerTerrainMinimap;
					break;
				case "HeightMode":
					newPainter = this.heightMinimap;
					break;
				case "LowerHeightMode":
					newPainter = this.lowerHeightMinimap;
					break;
				case "ZoneMode":
					newPainter = this.zoneMinimap;
					break;
				case "LightMode":
					newPainter = this.lightMinimap;
					break;
				case "ambientMode":
					newPainter = this.ambientMinimap;
					break;
				case "musicMode":
					newPainter = this.musicMinimap;
					break;
				}
				if (newPainter != null && newPainter != this.activePainter)
				{
					if (this.activePainter != null)
					{
						this.activePainter.Clear();
					}
					this.activePainter = newPainter;
					this.Repaint();
					return;
				}
			}
			else if (this.activePainter != null)
			{
				this.activePainter.Clear();
				this.activePainter = null;
			}
		}

		// Token: 0x06000CD8 RID: 3288 RVA: 0x0006D61A File Offset: 0x0006C61A
		public Bitmap GetMinimap()
		{
			if (this.activePainter != null)
			{
				return this.activePainter.Minimap;
			}
			return null;
		}

		// Token: 0x06000CD9 RID: 3289 RVA: 0x0006D631 File Offset: 0x0006C631
		public bool GetTerrainMinimap(out Bitmap bitmap)
		{
			return this.terrainMinimap.GetBitmap(out bitmap);
		}

		// Token: 0x04000A61 RID: 2657
		public const string terrainMode = "TerrainMode";

		// Token: 0x04000A62 RID: 2658
		public const string lowerTerrainMode = "LowerTerrainMode";

		// Token: 0x04000A63 RID: 2659
		public const string heightMode = "HeightMode";

		// Token: 0x04000A64 RID: 2660
		public const string lowerHeightMode = "LowerHeightMode";

		// Token: 0x04000A65 RID: 2661
		public const string zoneMode = "ZoneMode";

		// Token: 0x04000A66 RID: 2662
		public const string lightMode = "LightMode";

		// Token: 0x04000A67 RID: 2663
		public const string ambientMode = "ambientMode";

		// Token: 0x04000A68 RID: 2664
		public const string musicMode = "musicMode";

		// Token: 0x04000A69 RID: 2665
		private readonly MainForm.Context context;

		// Token: 0x04000A6A RID: 2666
		private MapEditorMap mapEditorMap;

		// Token: 0x04000A6B RID: 2667
		private OperationContainer operationContainer;

		// Token: 0x04000A6C RID: 2668
		private bool changed;

		// Token: 0x04000A6D RID: 2669
		private Rectangle changedRectangle = new Rectangle(0, 0, 0, 0);

		// Token: 0x04000A6E RID: 2670
		private readonly MinimapContainer.LayerMinimap terrainMinimap;

		// Token: 0x04000A6F RID: 2671
		private readonly MinimapContainer.LayerMinimap lowerTerrainMinimap;

		// Token: 0x04000A70 RID: 2672
		private readonly MinimapContainer.HeightMinimap heightMinimap;

		// Token: 0x04000A71 RID: 2673
		private readonly MinimapContainer.HeightMinimap lowerHeightMinimap;

		// Token: 0x04000A72 RID: 2674
		private readonly MinimapContainer.ZoneMinimap zoneMinimap;

		// Token: 0x04000A73 RID: 2675
		private readonly MinimapContainer.LightMinimap lightMinimap;

		// Token: 0x04000A74 RID: 2676
		private readonly MinimapContainer.SoundMinimap ambientMinimap;

		// Token: 0x04000A75 RID: 2677
		private readonly MinimapContainer.SoundMinimap musicMinimap;

		// Token: 0x04000A76 RID: 2678
		private MinimapContainer.IMinimapPainter activePainter;

		// Token: 0x04000A77 RID: 2679
		private readonly Size mapSize;

		// Token: 0x02000100 RID: 256
		// (Invoke) Token: 0x06000CDB RID: 3291
		public delegate void PaintedEvent(object sender);

		// Token: 0x02000101 RID: 257
		private interface IMinimapPainter
		{
			// Token: 0x1700025D RID: 605
			// (get) Token: 0x06000CDE RID: 3294
			Bitmap Minimap { get; }

			// Token: 0x1700025E RID: 606
			// (get) Token: 0x06000CDF RID: 3295
			int ActiveTerrain { get; }

			// Token: 0x06000CE0 RID: 3296
			void Paint(Rectangle changedRectangle);

			// Token: 0x06000CE1 RID: 3297
			void RepaintAll();

			// Token: 0x06000CE2 RID: 3298
			void Clear();
		}

		// Token: 0x02000102 RID: 258
		private class CommonMinimapPainter : MinimapContainer.IMinimapPainter
		{
			// Token: 0x14000039 RID: 57
			// (add) Token: 0x06000CE3 RID: 3299 RVA: 0x0006D63F File Offset: 0x0006C63F
			// (remove) Token: 0x06000CE4 RID: 3300 RVA: 0x0006D658 File Offset: 0x0006C658
			public event MinimapContainer.PaintedEvent Painted;

			// Token: 0x06000CE5 RID: 3301 RVA: 0x0006D674 File Offset: 0x0006C674
			private void CheckBitmap()
			{
				if (this.editorScene != null && this.minimap == null && this.size.Width > 0 && this.size.Height > 0)
				{
					this.minimap = new Bitmap(this.size.Width, this.size.Height, PixelFormat.Format24bppRgb);
				}
			}

			// Token: 0x06000CE6 RID: 3302 RVA: 0x0006D6DF File Offset: 0x0006C6DF
			protected virtual bool InternalPaint(Rectangle changedRectangle)
			{
				return false;
			}

			// Token: 0x06000CE7 RID: 3303 RVA: 0x0006D6E2 File Offset: 0x0006C6E2
			protected CommonMinimapPainter(Size _size, EditorScene _editorScene, int _activeTerrain, Tools.Geometry.Point _mapSize)
			{
				this.size = _size;
				this.editorScene = _editorScene;
				this.activeTerrain = _activeTerrain;
				this.mapSize = _mapSize;
			}

			// Token: 0x1700025F RID: 607
			// (get) Token: 0x06000CE8 RID: 3304 RVA: 0x0006D714 File Offset: 0x0006C714
			protected int Width
			{
				get
				{
					return this.size.Width;
				}
			}

			// Token: 0x17000260 RID: 608
			// (get) Token: 0x06000CE9 RID: 3305 RVA: 0x0006D730 File Offset: 0x0006C730
			protected int Height
			{
				get
				{
					return this.size.Height;
				}
			}

			// Token: 0x17000261 RID: 609
			// (get) Token: 0x06000CEA RID: 3306 RVA: 0x0006D74B File Offset: 0x0006C74B
			protected EditorScene EditorScene
			{
				get
				{
					return this.editorScene;
				}
			}

			// Token: 0x17000262 RID: 610
			// (get) Token: 0x06000CEB RID: 3307 RVA: 0x0006D753 File Offset: 0x0006C753
			protected Tools.Geometry.Point MapSize
			{
				get
				{
					return this.mapSize;
				}
			}

			// Token: 0x17000263 RID: 611
			// (get) Token: 0x06000CEC RID: 3308 RVA: 0x0006D75B File Offset: 0x0006C75B
			public Bitmap Minimap
			{
				get
				{
					return this.minimap;
				}
			}

			// Token: 0x17000264 RID: 612
			// (get) Token: 0x06000CED RID: 3309 RVA: 0x0006D763 File Offset: 0x0006C763
			public int ActiveTerrain
			{
				get
				{
					return this.activeTerrain;
				}
			}

			// Token: 0x06000CEE RID: 3310 RVA: 0x0006D76B File Offset: 0x0006C76B
			public void Paint(Rectangle changedRectangle)
			{
				this.CheckBitmap();
				if (this.minimap != null && this.editorScene != null && this.InternalPaint(changedRectangle) && this.Painted != null)
				{
					this.Painted(this);
				}
			}

			// Token: 0x06000CEF RID: 3311 RVA: 0x0006D7A0 File Offset: 0x0006C7A0
			public void RepaintAll()
			{
				this.Paint(new Rectangle(0, 0, this.size.Width, this.size.Height));
			}

			// Token: 0x06000CF0 RID: 3312 RVA: 0x0006D7D6 File Offset: 0x0006C7D6
			public void Clear()
			{
				if (this.minimap != null)
				{
					this.minimap.Dispose();
					this.minimap = null;
				}
			}

			// Token: 0x04000A78 RID: 2680
			private readonly Size size = Size.Empty;

			// Token: 0x04000A79 RID: 2681
			private readonly EditorScene editorScene;

			// Token: 0x04000A7A RID: 2682
			private readonly int activeTerrain;

			// Token: 0x04000A7B RID: 2683
			private Bitmap minimap;

			// Token: 0x04000A7C RID: 2684
			private readonly Tools.Geometry.Point mapSize;
		}

		// Token: 0x02000103 RID: 259
		private class LayerMinimap : MinimapContainer.CommonMinimapPainter
		{
			// Token: 0x06000CF1 RID: 3313 RVA: 0x0006D7F2 File Offset: 0x0006C7F2
			public LayerMinimap(Size _size, EditorScene _editorScene, int _index, Tools.Geometry.Point mapSize) : base(_size, _editorScene, _index, mapSize)
			{
			}

			// Token: 0x06000CF2 RID: 3314 RVA: 0x0006D800 File Offset: 0x0006C800
			protected override bool InternalPaint(Rectangle changedRectangle)
			{
				if (base.ActiveTerrain == 0 || base.ActiveTerrain == 1)
				{
					Rectangle rect = new Rectangle(0, 0, base.Minimap.Width, base.Minimap.Height);
					BitmapData bmpData = base.Minimap.LockBits(rect, ImageLockMode.ReadWrite, base.Minimap.PixelFormat);
					base.EditorScene.GetTerrainBitmap(base.ActiveTerrain, bmpData.Scan0, bmpData.Stride, base.Minimap.Width, base.Minimap.Height, changedRectangle.Left, base.Minimap.Height - changedRectangle.Top - changedRectangle.Height, changedRectangle.Left + changedRectangle.Width, base.Minimap.Height - changedRectangle.Top);
					base.Minimap.UnlockBits(bmpData);
					return true;
				}
				return false;
			}

			// Token: 0x06000CF3 RID: 3315 RVA: 0x0006D8E0 File Offset: 0x0006C8E0
			public bool GetBitmap(out Bitmap bitmap)
			{
				if (base.EditorScene != null && base.Width > 0 && base.Height > 0 && (base.ActiveTerrain == 0 || base.ActiveTerrain == 1))
				{
					bitmap = new Bitmap(base.Width, base.Height, PixelFormat.Format24bppRgb);
					Rectangle rect = new Rectangle(0, 0, bitmap.Width, bitmap.Height);
					BitmapData bmpData = bitmap.LockBits(rect, ImageLockMode.ReadWrite, bitmap.PixelFormat);
					base.EditorScene.GetTerrainBitmap(base.ActiveTerrain, bmpData.Scan0, bmpData.Stride, bitmap.Width, bitmap.Height, rect.Left, bitmap.Height - rect.Top - rect.Height, rect.Left + rect.Width, bitmap.Height - rect.Top);
					bitmap.UnlockBits(bmpData);
					return true;
				}
				bitmap = null;
				return false;
			}
		}

		// Token: 0x02000104 RID: 260
		private class HeightMinimap : MinimapContainer.CommonMinimapPainter
		{
			// Token: 0x06000CF4 RID: 3316 RVA: 0x0006D9D9 File Offset: 0x0006C9D9
			public HeightMinimap(Size _size, EditorScene _editorScene, int _index, Tools.Geometry.Point mapSize) : base(_size, _editorScene, _index, mapSize)
			{
			}

			// Token: 0x06000CF5 RID: 3317 RVA: 0x0006D9E8 File Offset: 0x0006C9E8
			protected override bool InternalPaint(Rectangle changedRectangle)
			{
				if (base.ActiveTerrain == 0 || base.ActiveTerrain == 1)
				{
					Rectangle rect = new Rectangle(0, 0, base.Minimap.Width, base.Minimap.Height);
					int xRepaintBeg = changedRectangle.Left;
					int yRepaintBeg = base.Minimap.Height - changedRectangle.Top - changedRectangle.Height;
					int xRepaintEnd = changedRectangle.Left + changedRectangle.Width;
					int yRepaintEnd = base.Minimap.Height - changedRectangle.Top;
					xRepaintEnd = ((xRepaintEnd < base.MapSize.X) ? xRepaintEnd : base.MapSize.X);
					yRepaintEnd = ((yRepaintEnd < base.MapSize.Y) ? yRepaintEnd : base.MapSize.Y);
					float _heightMin;
					float _heightMax;
					base.EditorScene.GetTerrainHeightMinMax(base.ActiveTerrain, xRepaintBeg, yRepaintBeg, xRepaintEnd, yRepaintEnd, out _heightMin, out _heightMax);
					if (_heightMin < this.heightMin || _heightMax > this.heightMax)
					{
						xRepaintBeg = 0;
						yRepaintBeg = 0;
						xRepaintEnd = base.Minimap.Width;
						yRepaintEnd = base.Minimap.Height;
						this.heightMin = Math.Min(this.heightMin, _heightMin);
						this.heightMax = Math.Max(this.heightMax, _heightMax);
					}
					BitmapData bmpData = base.Minimap.LockBits(rect, ImageLockMode.ReadWrite, base.Minimap.PixelFormat);
					base.EditorScene.GetTerrainHeightmap(base.ActiveTerrain, bmpData.Scan0, bmpData.Stride, base.Minimap.Width, base.Minimap.Height, xRepaintBeg, yRepaintBeg, xRepaintEnd, yRepaintEnd, this.heightMin, this.heightMax - this.heightMin);
					base.Minimap.UnlockBits(bmpData);
					return true;
				}
				return false;
			}

			// Token: 0x04000A7E RID: 2686
			private float heightMin;

			// Token: 0x04000A7F RID: 2687
			private float heightMax;
		}

		// Token: 0x02000105 RID: 261
		private class ZoneMinimap : MinimapContainer.CommonMinimapPainter
		{
			// Token: 0x06000CF6 RID: 3318 RVA: 0x0006DBAD File Offset: 0x0006CBAD
			public ZoneMinimap(MapZoneContainer _mapZoneContainer, Size _size, EditorScene _editorScene, int _index, Tools.Geometry.Point mapSize) : base(_size, _editorScene, _index, mapSize)
			{
				this.mapZoneContainer = _mapZoneContainer;
			}

			// Token: 0x06000CF7 RID: 3319 RVA: 0x0006DBC4 File Offset: 0x0006CBC4
			protected override bool InternalPaint(Rectangle changedRectangle)
			{
				if (this.mapZoneContainer != null && (base.ActiveTerrain == 0 || base.ActiveTerrain == 1))
				{
					Rectangle rect = new Rectangle(0, 0, base.Minimap.Width, base.Minimap.Height);
					BitmapData bmpData = base.Minimap.LockBits(rect, ImageLockMode.ReadWrite, base.Minimap.PixelFormat);
					base.EditorScene.GetTerrainBitmap(base.ActiveTerrain, bmpData.Scan0, bmpData.Stride, base.Minimap.Width, base.Minimap.Height, changedRectangle.Left, base.Minimap.Height - changedRectangle.Top - changedRectangle.Height, changedRectangle.Left + changedRectangle.Width, base.Minimap.Height - changedRectangle.Top);
					base.Minimap.UnlockBits(bmpData);
					Graphics graphics = Graphics.FromImage(base.Minimap);
					Rectangle zoneChangedRectangle = new Rectangle(changedRectangle.Left / Constants.ZoneGranularity, changedRectangle.Top / Constants.ZoneGranularity, (changedRectangle.Right + Constants.ZoneGranularity - 1) / Constants.ZoneGranularity, (changedRectangle.Bottom + Constants.ZoneGranularity - 1) / Constants.ZoneGranularity);
					int ySize = base.MapSize.Y * Constants.PatchSize / Constants.ZoneGranularity - 1;
					for (int x = zoneChangedRectangle.Left; x < zoneChangedRectangle.Right; x++)
					{
						for (int y = zoneChangedRectangle.Top; y < zoneChangedRectangle.Bottom; y++)
						{
							Color color = this.mapZoneContainer.GetZoneColor(this.mapZoneContainer.GetZone(x, ySize - y));
							if (color != Color.Empty)
							{
								graphics.FillRectangle(new SolidBrush(color), x * Constants.ZoneGranularity, y * Constants.ZoneGranularity, Constants.ZoneGranularity, Constants.ZoneGranularity);
							}
						}
					}
					graphics.Dispose();
					return true;
				}
				return false;
			}

			// Token: 0x04000A80 RID: 2688
			private readonly MapZoneContainer mapZoneContainer;
		}

		// Token: 0x02000106 RID: 262
		private class LightMinimap : MinimapContainer.CommonMinimapPainter
		{
			// Token: 0x06000CF8 RID: 3320 RVA: 0x0006DDB9 File Offset: 0x0006CDB9
			public LightMinimap(ZoneLightContainer _zoneLightContainer, Size _size, EditorScene _editorScene, int _index, Tools.Geometry.Point mapSize) : base(_size, _editorScene, _index, mapSize)
			{
				this.zoneLightContainer = _zoneLightContainer;
			}

			// Token: 0x06000CF9 RID: 3321 RVA: 0x0006DDD0 File Offset: 0x0006CDD0
			protected override bool InternalPaint(Rectangle changedRectangle)
			{
				if (this.zoneLightContainer != null && (base.ActiveTerrain == 0 || base.ActiveTerrain == 1))
				{
					Rectangle rect = new Rectangle(0, 0, base.Minimap.Width, base.Minimap.Height);
					BitmapData bmpData = base.Minimap.LockBits(rect, ImageLockMode.ReadWrite, base.Minimap.PixelFormat);
					base.EditorScene.GetTerrainBitmap(base.ActiveTerrain, bmpData.Scan0, bmpData.Stride, base.Minimap.Width, base.Minimap.Height, changedRectangle.Left, base.Minimap.Height - changedRectangle.Top - changedRectangle.Height, changedRectangle.Left + changedRectangle.Width, base.Minimap.Height - changedRectangle.Top);
					base.Minimap.UnlockBits(bmpData);
					Graphics graphics = Graphics.FromImage(base.Minimap);
					Rectangle zoneChangedRectangle = new Rectangle(changedRectangle.Left / Constants.ZoneGranularity, changedRectangle.Top / Constants.ZoneGranularity, (changedRectangle.Right + Constants.ZoneGranularity - 1) / Constants.ZoneGranularity, (changedRectangle.Bottom + Constants.ZoneGranularity - 1) / Constants.ZoneGranularity);
					int ySize = base.MapSize.Y * Constants.PatchSize / Constants.ZoneGranularity - 1;
					for (int x = zoneChangedRectangle.Left; x < zoneChangedRectangle.Right; x++)
					{
						for (int y = zoneChangedRectangle.Top; y < zoneChangedRectangle.Bottom; y++)
						{
							Color color = this.zoneLightContainer.GetLightColor(this.zoneLightContainer.GetLight(x, ySize - y));
							if (color != Color.Empty)
							{
								graphics.FillRectangle(new SolidBrush(color), x * Constants.ZoneGranularity, y * Constants.ZoneGranularity, Constants.ZoneGranularity, Constants.ZoneGranularity);
							}
						}
					}
					graphics.Dispose();
					return true;
				}
				return false;
			}

			// Token: 0x04000A81 RID: 2689
			private readonly ZoneLightContainer zoneLightContainer;
		}

		// Token: 0x02000107 RID: 263
		private class SoundMinimap : MinimapContainer.CommonMinimapPainter
		{
			// Token: 0x06000CFA RID: 3322 RVA: 0x0006DFC5 File Offset: 0x0006CFC5
			public SoundMinimap(MapSoundContainer _mapSoundContainer, Size _size, EditorScene _editorScene, int _index, Tools.Geometry.Point mapSize) : base(_size, _editorScene, _index, mapSize)
			{
				this.mapSoundContainer = _mapSoundContainer;
			}

			// Token: 0x06000CFB RID: 3323 RVA: 0x0006DFDC File Offset: 0x0006CFDC
			protected override bool InternalPaint(Rectangle changedRectangle)
			{
				if (this.mapSoundContainer != null && (base.ActiveTerrain == 0 || base.ActiveTerrain == 1))
				{
					Rectangle rect = new Rectangle(0, 0, base.Minimap.Width, base.Minimap.Height);
					BitmapData bmpData = base.Minimap.LockBits(rect, ImageLockMode.ReadWrite, base.Minimap.PixelFormat);
					base.EditorScene.GetTerrainBitmap(base.ActiveTerrain, bmpData.Scan0, bmpData.Stride, base.Minimap.Width, base.Minimap.Height, changedRectangle.Left, base.Minimap.Height - changedRectangle.Top - changedRectangle.Height, changedRectangle.Left + changedRectangle.Width, base.Minimap.Height - changedRectangle.Top);
					base.Minimap.UnlockBits(bmpData);
					Graphics graphics = Graphics.FromImage(base.Minimap);
					Rectangle zoneChangedRectangle = new Rectangle(changedRectangle.Left / Constants.ZoneGranularity, changedRectangle.Top / Constants.ZoneGranularity, (changedRectangle.Right + Constants.ZoneGranularity - 1) / Constants.ZoneGranularity, (changedRectangle.Bottom + Constants.ZoneGranularity - 1) / Constants.ZoneGranularity);
					int ySize = base.MapSize.Y * Constants.PatchSize / Constants.ZoneGranularity - 1;
					for (int x = zoneChangedRectangle.Left; x < zoneChangedRectangle.Right; x++)
					{
						for (int y = zoneChangedRectangle.Top; y < zoneChangedRectangle.Bottom; y++)
						{
							Color color = this.mapSoundContainer.GetSoundColor(this.mapSoundContainer.GetSound(x, ySize - y));
							if (color != Color.Empty)
							{
								graphics.FillRectangle(new SolidBrush(color), x * Constants.ZoneGranularity, y * Constants.ZoneGranularity, Constants.ZoneGranularity, Constants.ZoneGranularity);
							}
						}
					}
					graphics.Dispose();
					return true;
				}
				return false;
			}

			// Token: 0x04000A82 RID: 2690
			private readonly MapSoundContainer mapSoundContainer;
		}
	}
}
