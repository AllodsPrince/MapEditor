using System;
using System.Collections.Generic;
using System.Drawing;
using Tools.Geometry;
using Tools.MapObjects;

namespace MapEditor.Scene.Types
{
	// Token: 0x020000B9 RID: 185
	internal class MapSceneSquares
	{
		// Token: 0x06000919 RID: 2329 RVA: 0x0004F034 File Offset: 0x0004E034
		private void CreateUserGeometry(AxisAlignedSquare square, bool addNew)
		{
			int userGeometryID;
			bool userGeometryAlreadyExists = this.userGeometryMap.TryGetValue(square.ID, out userGeometryID);
			if (!userGeometryAlreadyExists)
			{
				userGeometryID = -1;
			}
			if (userGeometryAlreadyExists || addNew)
			{
				Color color = Color.FromArgb(square.Selected ? 255 : MapEditorScene.DefaultTransparentColorAlpha, square.Color);
				Position position = new Position(square.Center);
				userGeometryID = this.mapEditorScene.EditorScene.CreateUserGeometry_Brush(userGeometryID, ref position, square.Size.X, square.Size.Y, color, square.SquareType == AxisAlignedSquareType.Ellipse);
				if (userGeometryAlreadyExists)
				{
					if (userGeometryID != -1)
					{
						this.userGeometryMap[square.ID] = userGeometryID;
						return;
					}
					this.userGeometryMap.Remove(square.ID);
					return;
				}
				else if (userGeometryID != -1)
				{
					this.userGeometryMap.Add(square.ID, userGeometryID);
				}
			}
		}

		// Token: 0x0600091A RID: 2330 RVA: 0x0004F114 File Offset: 0x0004E114
		private void DestroyUserGeometry(AxisAlignedSquare square)
		{
			int userGeometryID;
			if (this.userGeometryMap.TryGetValue(square.ID, out userGeometryID))
			{
				this.mapEditorScene.EditorScene.DeleteUserGeometry(userGeometryID);
				this.userGeometryMap.Remove(square.ID);
			}
		}

		// Token: 0x0600091B RID: 2331 RVA: 0x0004F159 File Offset: 0x0004E159
		private void OnSquareAdded(AxisAlignedSquareContainer squareContainer, AxisAlignedSquare square)
		{
			if (this.mapEditorScene != null)
			{
				this.CreateUserGeometry(square, true);
			}
		}

		// Token: 0x0600091C RID: 2332 RVA: 0x0004F16B File Offset: 0x0004E16B
		private void OnSquareRemoved(AxisAlignedSquareContainer squareContainer, AxisAlignedSquare square)
		{
			if (this.mapEditorScene != null)
			{
				this.DestroyUserGeometry(square);
			}
		}

		// Token: 0x0600091D RID: 2333 RVA: 0x0004F17C File Offset: 0x0004E17C
		private void OnSquareChanged(AxisAlignedSquareContainer squareContainer, AxisAlignedSquare square)
		{
			this.CreateUserGeometry(square, false);
		}

		// Token: 0x0600091E RID: 2334 RVA: 0x0004F186 File Offset: 0x0004E186
		private void OnSquareTypeChanged(AxisAlignedSquareContainer squareContainer, AxisAlignedSquare square, ref AxisAlignedSquareType oldValue, ref AxisAlignedSquareType newValue)
		{
			this.CreateUserGeometry(square, false);
		}

		// Token: 0x0600091F RID: 2335 RVA: 0x0004F190 File Offset: 0x0004E190
		private void OnSquareCenterChanged(AxisAlignedSquareContainer squareContainer, AxisAlignedSquare square, ref Vec3 oldValue, ref Vec3 newValue)
		{
			this.CreateUserGeometry(square, false);
		}

		// Token: 0x06000920 RID: 2336 RVA: 0x0004F19A File Offset: 0x0004E19A
		private void OnSquareSizeChanged(AxisAlignedSquareContainer squareContainer, AxisAlignedSquare square, ref Vec2 oldValue, ref Vec2 newValue)
		{
			this.CreateUserGeometry(square, false);
		}

		// Token: 0x06000921 RID: 2337 RVA: 0x0004F1A4 File Offset: 0x0004E1A4
		private void OnSquareSelectedChanged(AxisAlignedSquareContainer squareContainer, AxisAlignedSquare square, ref bool oldValue, ref bool newValue)
		{
			this.CreateUserGeometry(square, false);
		}

		// Token: 0x06000922 RID: 2338 RVA: 0x0004F1AE File Offset: 0x0004E1AE
		private void OnSquareColorChanged(AxisAlignedSquareContainer squareContainer, AxisAlignedSquare square, ref Color oldValue, ref Color newValue)
		{
			this.CreateUserGeometry(square, false);
		}

		// Token: 0x06000923 RID: 2339 RVA: 0x0004F1B8 File Offset: 0x0004E1B8
		public void Bind(MapEditorScene _mapEditorScene)
		{
			this.mapEditorScene = _mapEditorScene;
			if (this.mapEditorScene != null && this.mapEditorScene.SquareContainer != null)
			{
				this.mapEditorScene.SquareContainer.SquareAdded += this.OnSquareAdded;
				this.mapEditorScene.SquareContainer.SquareRemoved += this.OnSquareRemoved;
				this.mapEditorScene.SquareContainer.SquareChanged += this.OnSquareChanged;
				this.mapEditorScene.SquareContainer.SquareTypeChanged += this.OnSquareTypeChanged;
				this.mapEditorScene.SquareContainer.SquareCenterChanged += this.OnSquareCenterChanged;
				this.mapEditorScene.SquareContainer.SquareSizeChanged += this.OnSquareSizeChanged;
				this.mapEditorScene.SquareContainer.SquareSelectedChanged += this.OnSquareSelectedChanged;
				this.mapEditorScene.SquareContainer.SquareColorChanged += this.OnSquareColorChanged;
			}
		}

		// Token: 0x06000924 RID: 2340 RVA: 0x0004F2C8 File Offset: 0x0004E2C8
		public void Unbind()
		{
			if (this.mapEditorScene != null)
			{
				if (this.mapEditorScene.SquareContainer != null)
				{
					this.mapEditorScene.SquareContainer.SquareAdded -= this.OnSquareAdded;
					this.mapEditorScene.SquareContainer.SquareRemoved -= this.OnSquareRemoved;
					this.mapEditorScene.SquareContainer.SquareChanged -= this.OnSquareChanged;
					this.mapEditorScene.SquareContainer.SquareTypeChanged -= this.OnSquareTypeChanged;
					this.mapEditorScene.SquareContainer.SquareCenterChanged -= this.OnSquareCenterChanged;
					this.mapEditorScene.SquareContainer.SquareSizeChanged -= this.OnSquareSizeChanged;
					this.mapEditorScene.SquareContainer.SquareSelectedChanged -= this.OnSquareSelectedChanged;
					this.mapEditorScene.SquareContainer.SquareColorChanged -= this.OnSquareColorChanged;
				}
				this.mapEditorScene = null;
			}
		}

		// Token: 0x040007B5 RID: 1973
		private MapEditorScene mapEditorScene;

		// Token: 0x040007B6 RID: 1974
		private readonly Dictionary<int, int> userGeometryMap = new Dictionary<int, int>();
	}
}
