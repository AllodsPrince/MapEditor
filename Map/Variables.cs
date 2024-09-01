using System;
using System.Collections.Generic;
using Tools.Geometry;
using Tools.MapObjects;

namespace MapEditor.Map
{
	// Token: 0x02000127 RID: 295
	public class Variables
	{
		// Token: 0x06000E9B RID: 3739 RVA: 0x000765B0 File Offset: 0x000755B0
		public Variables(int _patchX, int _patchY, string _mapResourceName, string _continentName, ContinentType _continentType, ref Position _startCameraPosition, int _mapSizeInPatches)
		{
			this.mapResourceName = _mapResourceName;
			this.continentName = _continentName;
			this.continentType = _continentType;
			this.startCameraPosition = _startCameraPosition;
			this.mapSizeInPatches = _mapSizeInPatches;
			this.mapSize = new Point(this.mapSizeInPatches, this.mapSizeInPatches);
			this.tilesUpperBounds = new Point(Constants.PatchSize * this.MapSize.X / Constants.MapZonePieceSize.X, Constants.PatchSize * this.MapSize.Y / Constants.MapZonePieceSize.Y);
			if (this.continentType == ContinentType.AstralHub)
			{
				if (this.startCameraPosition.X > (double)(this.MapSize.X * Constants.PatchSize))
				{
					this.startCameraPosition.X = (double)(this.MapSize.X * Constants.PatchSize) / 2.0;
				}
				if (this.startCameraPosition.Y > (double)(this.MapSize.Y * Constants.PatchSize))
				{
					this.startCameraPosition.Y = (double)(this.MapSize.Y * Constants.PatchSize) / 2.0;
					return;
				}
			}
			else if (this.continentType == ContinentType.Continent)
			{
				this.minXMinYPatchCoords.X = _patchX;
				this.minXMinYPatchCoords.Y = _patchY;
			}
		}

		// Token: 0x170002B7 RID: 695
		// (get) Token: 0x06000E9C RID: 3740 RVA: 0x0007674B File Offset: 0x0007574B
		// (set) Token: 0x06000E9D RID: 3741 RVA: 0x00076753 File Offset: 0x00075753
		public Point MinXMinYPatchCoords
		{
			get
			{
				return this.minXMinYPatchCoords;
			}
			set
			{
				this.minXMinYPatchCoords = value;
			}
		}

		// Token: 0x170002B8 RID: 696
		// (get) Token: 0x06000E9E RID: 3742 RVA: 0x0007675C File Offset: 0x0007575C
		public string MapResourceName
		{
			get
			{
				return this.mapResourceName;
			}
		}

		// Token: 0x170002B9 RID: 697
		// (get) Token: 0x06000E9F RID: 3743 RVA: 0x00076764 File Offset: 0x00075764
		public string ContinentName
		{
			get
			{
				return this.continentName;
			}
		}

		// Token: 0x170002BA RID: 698
		// (get) Token: 0x06000EA0 RID: 3744 RVA: 0x0007676C File Offset: 0x0007576C
		public int MapSizeInPatches
		{
			get
			{
				return this.mapSizeInPatches;
			}
		}

		// Token: 0x170002BB RID: 699
		// (get) Token: 0x06000EA1 RID: 3745 RVA: 0x00076774 File Offset: 0x00075774
		public Point MapSize
		{
			get
			{
				return this.mapSize;
			}
		}

		// Token: 0x06000EA2 RID: 3746 RVA: 0x0007677C File Offset: 0x0007577C
		public string GetEditorMapCaption()
		{
			if (this.mapResourceName == null || !this.mapResourceName.EndsWith("heroic_MapResource.xdb"))
			{
				return this.continentName;
			}
			return this.continentName + " - HEROIC";
		}

		// Token: 0x170002BC RID: 700
		// (get) Token: 0x06000EA3 RID: 3747 RVA: 0x000767AF File Offset: 0x000757AF
		// (set) Token: 0x06000EA4 RID: 3748 RVA: 0x000767B7 File Offset: 0x000757B7
		public ContinentType ContinentType
		{
			get
			{
				return this.continentType;
			}
			set
			{
				this.continentType = value;
			}
		}

		// Token: 0x170002BD RID: 701
		// (get) Token: 0x06000EA5 RID: 3749 RVA: 0x000767C0 File Offset: 0x000757C0
		// (set) Token: 0x06000EA6 RID: 3750 RVA: 0x000767C8 File Offset: 0x000757C8
		public Position StartCameraPosition
		{
			get
			{
				return this.startCameraPosition;
			}
			set
			{
				this.startCameraPosition = value;
			}
		}

		// Token: 0x170002BE RID: 702
		// (get) Token: 0x06000EA7 RID: 3751 RVA: 0x000767D4 File Offset: 0x000757D4
		public double ScaleRatio
		{
			get
			{
				if (this.continentType == ContinentType.AstralHub)
				{
					double continentSizeInPatches = Constants.GetContinentMapSizeInPatches(this.continentName);
					if (continentSizeInPatches > MathConsts.DOUBLE_EPSILON)
					{
						return (double)this.MapSizeInPatches / continentSizeInPatches;
					}
				}
				return 1.0;
			}
		}

		// Token: 0x06000EA8 RID: 3752 RVA: 0x00076811 File Offset: 0x00075811
		public int GetMapSize()
		{
			return this.mapSizeInPatches * Constants.PatchSize;
		}

		// Token: 0x06000EA9 RID: 3753 RVA: 0x00076820 File Offset: 0x00075820
		private Point GetLocalCoord(Vec3 position)
		{
			return new Point((int)position.X - this.MinXMinYPatchCoords.X * Constants.PatchSize, (int)position.Y - this.MinXMinYPatchCoords.Y * Constants.PatchSize);
		}

		// Token: 0x06000EAA RID: 3754 RVA: 0x0007686C File Offset: 0x0007586C
		private bool CheckTile(Point tile)
		{
			return tile.X >= 0 && tile.X < this.tilesUpperBounds.X && tile.Y >= 0 && tile.Y < this.tilesUpperBounds.Y;
		}

		// Token: 0x06000EAB RID: 3755 RVA: 0x000768C0 File Offset: 0x000758C0
		public bool GetTile(Vec3 position, out Point tile)
		{
			tile = this.GetLocalCoord(position);
			if (tile.X < 0 || tile.X >= this.MapSize.X * Constants.PatchSize)
			{
				return false;
			}
			if (tile.Y < 0 || tile.Y >= this.MapSize.Y * Constants.PatchSize)
			{
				return false;
			}
			tile.X /= Constants.MapZonePieceSize.X;
			tile.Y /= Constants.MapZonePieceSize.Y;
			return true;
		}

		// Token: 0x06000EAC RID: 3756 RVA: 0x00076960 File Offset: 0x00075960
		public bool GetEditedTiles(Polygon polygon, out List<Point> tiles)
		{
			bool result = false;
			tiles = new List<Point>();
			if (polygon != null)
			{
				for (int x = 0; x < this.tilesUpperBounds.X; x++)
				{
					for (int y = 0; y < this.tilesUpperBounds.Y; y++)
					{
						Vec3 position = new Vec3(((double)x + 0.5) * (double)Constants.MapZonePieceSize.X + (double)(this.MinXMinYPatchCoords.X * Constants.PatchSize), ((double)y + 0.5) * (double)Constants.MapZonePieceSize.Y + (double)(this.MinXMinYPatchCoords.Y * Constants.PatchSize), 0.0);
						if (Geometry.IsInside(polygon.ClassifyPoint(ref position)))
						{
							result = true;
							tiles.Add(new Point(x, y));
						}
					}
				}
			}
			return result;
		}

		// Token: 0x06000EAD RID: 3757 RVA: 0x00076A58 File Offset: 0x00075A58
		public bool GetEditedTiles(Vec3 position, int brushSize, out List<Point> tiles)
		{
			bool result = false;
			tiles = new List<Point>();
			Point tile;
			if (this.GetTile(position, out tile))
			{
				Point delta = new Point(brushSize / 2, brushSize / 2);
				if (brushSize % 2 == 0)
				{
					Point localCoord = this.GetLocalCoord(position);
					if (localCoord.X % Constants.MapZonePieceSize.X >= Constants.MapZonePieceSize.X / 2)
					{
						delta.X--;
					}
					if (localCoord.Y % Constants.MapZonePieceSize.Y >= Constants.MapZonePieceSize.Y / 2)
					{
						delta.Y--;
					}
				}
				Point cornerTile = new Point(tile.X - delta.X, tile.Y - delta.Y);
				for (int x = 0; x < brushSize; x++)
				{
					for (int y = 0; y < brushSize; y++)
					{
						Point addedTile = new Point(cornerTile.X + x, cornerTile.Y + y);
						if (this.CheckTile(addedTile))
						{
							result = true;
							tiles.Add(addedTile);
						}
					}
				}
			}
			return result;
		}

		// Token: 0x06000EAE RID: 3758 RVA: 0x00076B7C File Offset: 0x00075B7C
		public bool GetPatch(ref Position pos, out int patchX, out int patchY)
		{
			patchX = (int)pos.X / Constants.PatchSize - this.MinXMinYPatchCoords.X;
			patchY = (int)pos.Y / Constants.PatchSize - this.MinXMinYPatchCoords.Y;
			return patchX > 0 && patchX < this.MapSizeInPatches && patchY > 0 && patchY < this.MapSizeInPatches;
		}

		// Token: 0x04000B48 RID: 2888
		private const string heroicMapMark = " - HEROIC";

		// Token: 0x04000B49 RID: 2889
		private const string heroicType = "heroic_MapResource.xdb";

		// Token: 0x04000B4A RID: 2890
		private Point minXMinYPatchCoords = Point.Empty;

		// Token: 0x04000B4B RID: 2891
		private readonly string continentName = string.Empty;

		// Token: 0x04000B4C RID: 2892
		private readonly string mapResourceName = string.Empty;

		// Token: 0x04000B4D RID: 2893
		private ContinentType continentType;

		// Token: 0x04000B4E RID: 2894
		private Position startCameraPosition = Position.Empty;

		// Token: 0x04000B4F RID: 2895
		private readonly int mapSizeInPatches;

		// Token: 0x04000B50 RID: 2896
		private readonly Point mapSize;

		// Token: 0x04000B51 RID: 2897
		private readonly Point tilesUpperBounds;
	}
}
