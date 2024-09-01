using System;
using System.Drawing;
using Tools.Geometry;
using Tools.MapObjects;

namespace MapEditor.Map.States
{
	// Token: 0x0200011B RID: 283
	internal class PatchRegion
	{
		// Token: 0x1400003D RID: 61
		// (add) Token: 0x06000DE3 RID: 3555 RVA: 0x00073BC4 File Offset: 0x00072BC4
		// (remove) Token: 0x06000DE4 RID: 3556 RVA: 0x00073BDD File Offset: 0x00072BDD
		public event PatchRegion.PatchRegionChangedEvent<PatchRegionType> TypeChanged;

		// Token: 0x1400003E RID: 62
		// (add) Token: 0x06000DE5 RID: 3557 RVA: 0x00073BF6 File Offset: 0x00072BF6
		// (remove) Token: 0x06000DE6 RID: 3558 RVA: 0x00073C0F File Offset: 0x00072C0F
		public event PatchRegion.PatchRegionChangedEvent<int> SquareSizeChanged;

		// Token: 0x1400003F RID: 63
		// (add) Token: 0x06000DE7 RID: 3559 RVA: 0x00073C28 File Offset: 0x00072C28
		// (remove) Token: 0x06000DE8 RID: 3560 RVA: 0x00073C41 File Offset: 0x00072C41
		public event PatchRegion.PatchRegionChangedEvent<Vec3> SquareCenterChanged;

		// Token: 0x06000DE9 RID: 3561 RVA: 0x00073C5C File Offset: 0x00072C5C
		private void Create(PatchRegionType _type)
		{
			if (_type == PatchRegionType.Square)
			{
				if (this.square != null)
				{
					this.square.SquareType = AxisAlignedSquareType.Square;
					this.square.Size = new Vec2((double)((float)this.squareSize * PatchRegion.squareSizeRate), (double)((float)this.squareSize * PatchRegion.squareSizeRate));
					this.square.Selected = true;
					this.square.Color = PatchRegion.patchRegionColor;
					this.square.Center = this.squareCenter;
					return;
				}
			}
			else if (_type == PatchRegionType.Polygon && this.polygon != null)
			{
				this.polygon.Closed = true;
				this.polygon.Selected = true;
				this.polygon.Color = PatchRegion.patchRegionColor;
			}
		}

		// Token: 0x06000DEA RID: 3562 RVA: 0x00073D14 File Offset: 0x00072D14
		private void Show(PatchRegionType _type)
		{
			if (_type == PatchRegionType.Square)
			{
				if (this.squareContainer != null)
				{
					this.squareContainer.AddSquare(this.square);
					return;
				}
			}
			else if (_type == PatchRegionType.Polygon && this.polygonContainer != null && this.polygon != null)
			{
				this.polygonContainer.AddPolygon(this.polygon);
			}
		}

		// Token: 0x06000DEB RID: 3563 RVA: 0x00073D68 File Offset: 0x00072D68
		private void Hide(PatchRegionType _type)
		{
			if (_type == PatchRegionType.Square)
			{
				if (this.squareContainer != null)
				{
					this.squareContainer.RemoveSquare(this.square);
					return;
				}
			}
			else if (_type == PatchRegionType.Polygon && this.polygonContainer != null && this.polygon != null)
			{
				this.polygonContainer.RemovePolygon(this.polygon);
			}
		}

		// Token: 0x06000DEC RID: 3564 RVA: 0x00073DB8 File Offset: 0x00072DB8
		public PatchRegion(ICollisionMap _collisionMap, AxisAlignedSquareContainer _squareContainer, PolygonContainer _polygonContainer)
		{
			this.square = new AxisAlignedSquare(-1, _collisionMap);
			this.polygon = new Polygon(-1, _collisionMap);
			this.squareContainer = _squareContainer;
			this.polygonContainer = _polygonContainer;
			this.Create(this.type);
		}

		// Token: 0x06000DED RID: 3565 RVA: 0x00073E14 File Offset: 0x00072E14
		public void Destroy()
		{
			this.Hide(this.type);
			if (this.square != null)
			{
				this.square.Clear();
				this.square = null;
			}
			if (this.polygon != null)
			{
				this.polygon.Clear();
				this.polygon = null;
			}
			this.squareContainer = null;
			this.polygonContainer = null;
		}

		// Token: 0x06000DEE RID: 3566 RVA: 0x00073E71 File Offset: 0x00072E71
		public void Update()
		{
			if (this.type == PatchRegionType.Square)
			{
				if (this.square != null)
				{
					this.square.Update();
					return;
				}
			}
			else if (this.type == PatchRegionType.Polygon && this.polygon != null)
			{
				this.polygon.Update();
			}
		}

		// Token: 0x17000296 RID: 662
		// (get) Token: 0x06000DEF RID: 3567 RVA: 0x00073EAB File Offset: 0x00072EAB
		// (set) Token: 0x06000DF0 RID: 3568 RVA: 0x00073EB3 File Offset: 0x00072EB3
		public bool Visible
		{
			get
			{
				return this.visible;
			}
			set
			{
				if (this.visible != value)
				{
					this.visible = value;
					if (this.visible)
					{
						this.Show(this.type);
						return;
					}
					this.Hide(this.type);
				}
			}
		}

		// Token: 0x17000297 RID: 663
		// (get) Token: 0x06000DF1 RID: 3569 RVA: 0x00073EE6 File Offset: 0x00072EE6
		public static Color PatchRegionColor
		{
			get
			{
				return PatchRegion.patchRegionColor;
			}
		}

		// Token: 0x17000298 RID: 664
		// (get) Token: 0x06000DF2 RID: 3570 RVA: 0x00073EED File Offset: 0x00072EED
		public static Color AddRegionColor
		{
			get
			{
				return PatchRegion.addRegionColor;
			}
		}

		// Token: 0x17000299 RID: 665
		// (get) Token: 0x06000DF3 RID: 3571 RVA: 0x00073EF4 File Offset: 0x00072EF4
		public static Color EditRegionColor
		{
			get
			{
				return PatchRegion.editRegionColor;
			}
		}

		// Token: 0x1700029A RID: 666
		// (get) Token: 0x06000DF4 RID: 3572 RVA: 0x00073EFB File Offset: 0x00072EFB
		// (set) Token: 0x06000DF5 RID: 3573 RVA: 0x00073F04 File Offset: 0x00072F04
		public PatchRegionType Type
		{
			get
			{
				return this.type;
			}
			set
			{
				if (this.type != value)
				{
					if (this.visible)
					{
						this.Hide(this.type);
					}
					PatchRegionType oldValue = this.type;
					this.type = value;
					this.Create(this.type);
					if (this.visible)
					{
						this.Show(this.type);
					}
					if (this.TypeChanged != null)
					{
						this.TypeChanged(this, ref this.type, ref oldValue);
					}
				}
			}
		}

		// Token: 0x1700029B RID: 667
		// (get) Token: 0x06000DF6 RID: 3574 RVA: 0x00073F78 File Offset: 0x00072F78
		// (set) Token: 0x06000DF7 RID: 3575 RVA: 0x00073F80 File Offset: 0x00072F80
		public int SquareSize
		{
			get
			{
				return this.squareSize;
			}
			set
			{
				if (this.squareSize != value)
				{
					int oldValue = this.squareSize;
					this.squareSize = value;
					if (this.square != null)
					{
						this.square.Size = new Vec2((double)((float)this.squareSize * PatchRegion.squareSizeRate), (double)((float)this.squareSize * PatchRegion.squareSizeRate));
					}
					if (this.SquareSizeChanged != null)
					{
						this.SquareSizeChanged(this, ref this.squareSize, ref oldValue);
					}
				}
			}
		}

		// Token: 0x1700029C RID: 668
		// (get) Token: 0x06000DF8 RID: 3576 RVA: 0x00073FF4 File Offset: 0x00072FF4
		// (set) Token: 0x06000DF9 RID: 3577 RVA: 0x00073FFC File Offset: 0x00072FFC
		public Vec3 SquareCenter
		{
			get
			{
				return this.squareCenter;
			}
			set
			{
				Vec3 oldValue = this.squareCenter;
				this.squareCenter = value;
				if (this.square != null)
				{
					this.square.Center = this.squareCenter;
				}
				if (this.SquareCenterChanged != null)
				{
					this.SquareCenterChanged(this, ref this.squareCenter, ref oldValue);
				}
			}
		}

		// Token: 0x1700029D RID: 669
		// (get) Token: 0x06000DFA RID: 3578 RVA: 0x0007404C File Offset: 0x0007304C
		public AxisAlignedSquare Square
		{
			get
			{
				return this.square;
			}
		}

		// Token: 0x1700029E RID: 670
		// (get) Token: 0x06000DFB RID: 3579 RVA: 0x00074054 File Offset: 0x00073054
		public AxisAlignedSquareContainer SquareContainer
		{
			get
			{
				return this.squareContainer;
			}
		}

		// Token: 0x1700029F RID: 671
		// (get) Token: 0x06000DFC RID: 3580 RVA: 0x0007405C File Offset: 0x0007305C
		public Polygon Polygon
		{
			get
			{
				return this.polygon;
			}
		}

		// Token: 0x170002A0 RID: 672
		// (get) Token: 0x06000DFD RID: 3581 RVA: 0x00074064 File Offset: 0x00073064
		public PolygonContainer PolygonContainer
		{
			get
			{
				return this.polygonContainer;
			}
		}

		// Token: 0x04000B0B RID: 2827
		private static readonly Color patchRegionColor = Color.White;

		// Token: 0x04000B0C RID: 2828
		private static readonly Color addRegionColor = Color.Yellow;

		// Token: 0x04000B0D RID: 2829
		private static readonly Color editRegionColor = Color.Magenta;

		// Token: 0x04000B0E RID: 2830
		private static readonly float squareSizeRate = 16f;

		// Token: 0x04000B0F RID: 2831
		private PatchRegionType type;

		// Token: 0x04000B10 RID: 2832
		private int squareSize = 1;

		// Token: 0x04000B11 RID: 2833
		private Vec3 squareCenter = Vec3.Empty;

		// Token: 0x04000B12 RID: 2834
		private AxisAlignedSquare square;

		// Token: 0x04000B13 RID: 2835
		private AxisAlignedSquareContainer squareContainer;

		// Token: 0x04000B14 RID: 2836
		private Polygon polygon;

		// Token: 0x04000B15 RID: 2837
		private PolygonContainer polygonContainer;

		// Token: 0x04000B16 RID: 2838
		private bool visible;

		// Token: 0x0200011C RID: 284
		// (Invoke) Token: 0x06000E00 RID: 3584
		public delegate void PatchRegionChangedEvent<T>(PatchRegion patchRegion, ref T oldValue, ref T newValue);
	}
}
