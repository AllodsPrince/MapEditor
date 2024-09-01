using System;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using Tools.Geometry;

namespace MapEditor.Map.Dialogs
{
	// Token: 0x020001CA RID: 458
	public class MapDialogControl : UserControl
	{
		// Token: 0x06001781 RID: 6017 RVA: 0x0009FFFC File Offset: 0x0009EFFC
		private void MapDialogControl_Paint(object sender, PaintEventArgs e)
		{
			if (this.enableDraw && base.Visible && base.Width > 0 && base.Height > 0)
			{
				Rectangle boardRectangle = new Rectangle(0, 0, base.Width - 1, base.Height - 1);
				Graphics graphics = e.Graphics;
				if (this.drawBitmap == null || this.drawBitmap.Width != base.Width || this.drawBitmap.Height != base.Height)
				{
					this.drawBitmap = new Bitmap(base.Width, base.Height);
				}
				if (this.solidBrush == null)
				{
					this.solidBrush = new SolidBrush(this.BackColor);
				}
				Graphics graphicsIm = Graphics.FromImage(this.drawBitmap);
				graphicsIm.FillRectangle(this.solidBrush, boardRectangle);
				if (this.drawMap)
				{
					if (this.map != null)
					{
						graphicsIm.DrawImage(this.map, boardRectangle);
					}
					this.DrawTarget(graphicsIm);
				}
				graphicsIm.DrawRectangle(MapDialogControl.pen, boardRectangle);
				graphics.DrawImage(this.drawBitmap, boardRectangle);
				graphicsIm.Dispose();
				graphics.Dispose();
			}
		}

		// Token: 0x06001782 RID: 6018 RVA: 0x000A0118 File Offset: 0x0009F118
		private void OnMouseMove(object sender, MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Left)
			{
				this.SetPosition(this.MouseClickToPosition(e.Location), false);
				return;
			}
			if (e.Button == MouseButtons.Right)
			{
				Tools.Geometry.Point imageMovement = default(Tools.Geometry.Point);
				bool move = false;
				imageMovement.X = (e.X - this.imageMovementStartPoint.X) / 10;
				imageMovement.Y = (e.Y - this.imageMovementStartPoint.Y) / 10;
				if (Math.Abs(imageMovement.X) > 0)
				{
					this.imageMovementStartPoint.X = e.X;
					move = true;
				}
				if (Math.Abs(imageMovement.Y) > 0)
				{
					this.imageMovementStartPoint.Y = e.Y;
					move = true;
				}
				if (move)
				{
					imageMovement.X = -1 * Math.Sign(imageMovement.X);
					imageMovement.Y = Math.Sign(imageMovement.Y);
					this.MoveVisibleArea(imageMovement);
				}
			}
		}

		// Token: 0x06001783 RID: 6019 RVA: 0x000A0210 File Offset: 0x0009F210
		private void OnMouseDown(object sender, MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Left)
			{
				this.SetPosition(this.MouseClickToPosition(e.Location), false);
				return;
			}
			if (e.Button == MouseButtons.Right)
			{
				this.imageMovementStartPoint = e.Location;
				Cursor.Current = Cursors.Hand;
			}
		}

		// Token: 0x06001784 RID: 6020 RVA: 0x000A0261 File Offset: 0x0009F261
		private static void OnMouseUp(object sender, MouseEventArgs e)
		{
			Cursor.Current = Cursors.Default;
		}

		// Token: 0x06001785 RID: 6021 RVA: 0x000A0270 File Offset: 0x0009F270
		private void MapDialogControl_Resize(object sender, EventArgs e)
		{
			PaintEventArgs pe = new PaintEventArgs(Graphics.FromHwnd(base.Handle), base.ClientRectangle);
			base.InvokePaint(this, pe);
		}

		// Token: 0x06001786 RID: 6022 RVA: 0x000A029C File Offset: 0x0009F29C
		private void DrawTarget(Graphics graphics)
		{
			if (this.openMapParams != null)
			{
				Tools.Geometry.Point patch = this.openMapParams.GetPatch();
				System.Drawing.Point targetSize = this.RangeToControl(new Tools.Geometry.Point(this.openMapParams.MapSize, this.openMapParams.MapSize));
				System.Drawing.Point target = this.PositionToControlCoord(patch);
				graphics.DrawRectangle(MapDialogControl.targetPen, target.X, target.Y - targetSize.Y, targetSize.X, targetSize.Y);
				double cameraX = this.openMapParams.CameraPosition.X / (double)Constants.PatchSize;
				double cameraY = this.openMapParams.CameraPosition.Y / (double)Constants.PatchSize;
				int cameraMark = targetSize.X / 10;
				System.Drawing.Point cameraPosition = this.PositionToControlCoord(cameraX, cameraY);
				System.Drawing.Point strartP = new System.Drawing.Point(cameraPosition.X - cameraMark, cameraPosition.Y - cameraMark);
				System.Drawing.Point endP = new System.Drawing.Point(cameraPosition.X + cameraMark, cameraPosition.Y + cameraMark);
				graphics.DrawLine(MapDialogControl.targetPen, strartP, endP);
				strartP = new System.Drawing.Point(cameraPosition.X - cameraMark, cameraPosition.Y + cameraMark);
				endP = new System.Drawing.Point(cameraPosition.X + cameraMark, cameraPosition.Y - cameraMark);
				graphics.DrawLine(MapDialogControl.targetPen, strartP, endP);
			}
		}

		// Token: 0x06001787 RID: 6023 RVA: 0x000A03F4 File Offset: 0x0009F3F4
		private Tools.Geometry.Point MouseClickToPosition(System.Drawing.Point coord)
		{
			Tools.Geometry.Point position = this.RangeToMap(coord);
			Tools.Geometry.Point visibleAreaSides = new Tools.Geometry.Point(this.mapSize.X / this.rate, this.mapSize.Y / this.rate);
			position.Y = visibleAreaSides.Y - position.Y - 1;
			position.X -= this.openMapParams.MapSize / 2;
			position.Y -= this.openMapParams.MapSize / 2;
			position.X += this.visibleAreaPosition.X;
			position.Y += this.visibleAreaPosition.Y;
			return position;
		}

		// Token: 0x06001788 RID: 6024 RVA: 0x000A04B5 File Offset: 0x0009F4B5
		private System.Drawing.Point PositionToControlCoord(Tools.Geometry.Point position)
		{
			return this.PositionToControlCoord((double)position.X, (double)position.Y);
		}

		// Token: 0x06001789 RID: 6025 RVA: 0x000A04D0 File Offset: 0x0009F4D0
		private System.Drawing.Point PositionToControlCoord(double x, double y)
		{
			x -= (double)this.visibleAreaPosition.X;
			y -= (double)this.visibleAreaPosition.Y;
			System.Drawing.Point coord = this.RangeToControl(x, y);
			coord.Y = base.Height - coord.Y - 1;
			return coord;
		}

		// Token: 0x0600178A RID: 6026 RVA: 0x000A0520 File Offset: 0x0009F520
		private Tools.Geometry.Point RangeToMap(System.Drawing.Point coord)
		{
			Tools.Geometry.Point position = default(Tools.Geometry.Point);
			Tools.Geometry.Point visibleAreaSides = new Tools.Geometry.Point(this.mapSize.X / this.rate, this.mapSize.Y / this.rate);
			if (base.Width > 0 && base.Height > 0)
			{
				position.X = coord.X * visibleAreaSides.X / base.Width;
				position.Y = coord.Y * visibleAreaSides.Y / base.Height;
			}
			return position;
		}

		// Token: 0x0600178B RID: 6027 RVA: 0x000A05AD File Offset: 0x0009F5AD
		private System.Drawing.Point RangeToControl(Tools.Geometry.Point position)
		{
			return this.RangeToControl((double)position.X, (double)position.Y);
		}

		// Token: 0x0600178C RID: 6028 RVA: 0x000A05C8 File Offset: 0x0009F5C8
		private System.Drawing.Point RangeToControl(double x, double y)
		{
			System.Drawing.Point coord = default(System.Drawing.Point);
			Tools.Geometry.Point visibleAreaSides = new Tools.Geometry.Point(this.mapSize.X / this.rate, this.mapSize.Y / this.rate);
			if (visibleAreaSides.X > 0 && visibleAreaSides.Y > 0)
			{
				coord.X = (int)(x * (double)base.Width) / visibleAreaSides.X;
				coord.Y = (int)(y * (double)base.Height) / visibleAreaSides.Y;
			}
			return coord;
		}

		// Token: 0x0600178D RID: 6029 RVA: 0x000A0650 File Offset: 0x0009F650
		private bool ChangeImage()
		{
			this.map = null;
			bool result = true;
			Tools.Geometry.Point visibleAreaSides = new Tools.Geometry.Point(this.mapSize.X / this.rate, this.mapSize.Y / this.rate);
			if (File.Exists(this.mapPath) && visibleAreaSides.X > 0 && visibleAreaSides.Y > 0)
			{
				try
				{
					this.map = new Bitmap(Constants.MegamapSize.X, Constants.MegamapSize.Y);
					Graphics graphics = Graphics.FromImage(this.map);
					Bitmap mapTmp = new Bitmap(this.mapPath);
					Rectangle fullRect = new Rectangle(0, 0, this.map.Width, this.map.Height);
					Rectangle partRect = new Rectangle(this.map.Width * this.visibleAreaPosition.X / this.mapSize.X, this.map.Height - this.map.Height * (this.visibleAreaPosition.Y + visibleAreaSides.Y) / this.mapSize.Y, this.map.Width * visibleAreaSides.X / this.mapSize.X, this.map.Height * visibleAreaSides.Y / this.mapSize.Y);
					graphics.DrawImage(mapTmp, fullRect, partRect, GraphicsUnit.Pixel);
					graphics.Dispose();
					mapTmp.Dispose();
				}
				catch (ExternalException ex)
				{
					Console.Write(ex);
					result = false;
				}
			}
			return result;
		}

		// Token: 0x0600178E RID: 6030 RVA: 0x000A07FC File Offset: 0x0009F7FC
		private void MoveVisibleArea(Tools.Geometry.Point movement)
		{
			if (this.openMapParams != null)
			{
				this.visibleAreaPosition.X = this.visibleAreaPosition.X + movement.X;
				this.visibleAreaPosition.Y = this.visibleAreaPosition.Y + movement.Y;
				this.CorrectVisibleArea(ref this.visibleAreaPosition);
				Tools.Geometry.Point _targetPosition = this.openMapParams.GetPatch();
				if (this.CorrectTargetPosition(ref _targetPosition, false))
				{
					this.SetPosition(_targetPosition, true);
				}
				this.ChangeImage();
				PaintEventArgs e = new PaintEventArgs(Graphics.FromHwnd(base.Handle), base.ClientRectangle);
				base.InvokePaint(this, e);
			}
		}

		// Token: 0x0600178F RID: 6031 RVA: 0x000A0898 File Offset: 0x0009F898
		private bool CorrectTargetPosition(ref Tools.Geometry.Point _targetPosition, bool global)
		{
			Tools.Geometry.Point __targetPosition = _targetPosition;
			int minX;
			int minY;
			int maxX;
			int maxY;
			if (global)
			{
				minX = 0;
				minY = 0;
				maxX = this.mapSize.X - this.openMapParams.MapSize;
				maxY = this.mapSize.Y - this.openMapParams.MapSize;
			}
			else
			{
				Tools.Geometry.Point visibleAreaSides = new Tools.Geometry.Point(this.mapSize.X / this.rate, this.mapSize.Y / this.rate);
				Tools.Geometry.Point maxPosition = new Tools.Geometry.Point(this.visibleAreaPosition.X + visibleAreaSides.X - this.openMapParams.MapSize, this.visibleAreaPosition.Y + visibleAreaSides.Y - this.openMapParams.MapSize);
				minX = this.visibleAreaPosition.X;
				minY = this.visibleAreaPosition.Y;
				maxX = maxPosition.X;
				maxY = maxPosition.Y;
			}
			_targetPosition.X = ((_targetPosition.X < minX) ? minX : _targetPosition.X);
			_targetPosition.X = ((_targetPosition.X > maxX) ? maxX : _targetPosition.X);
			_targetPosition.Y = ((_targetPosition.Y < minY) ? minY : _targetPosition.Y);
			_targetPosition.Y = ((_targetPosition.Y > maxY) ? maxY : _targetPosition.Y);
			return !__targetPosition.Equals(_targetPosition);
		}

		// Token: 0x06001790 RID: 6032 RVA: 0x000A0A00 File Offset: 0x0009FA00
		private void CorrectVisibleArea(ref Tools.Geometry.Point _visibleAreaPositoin)
		{
			Tools.Geometry.Point visibleAreaSides = new Tools.Geometry.Point(this.mapSize.X / this.rate, this.mapSize.Y / this.rate);
			_visibleAreaPositoin.X = ((_visibleAreaPositoin.X < 0) ? 0 : _visibleAreaPositoin.X);
			_visibleAreaPositoin.X = ((_visibleAreaPositoin.X > this.mapSize.X - visibleAreaSides.X) ? (this.mapSize.X - visibleAreaSides.X) : _visibleAreaPositoin.X);
			_visibleAreaPositoin.Y = ((_visibleAreaPositoin.Y < 0) ? 0 : _visibleAreaPositoin.Y);
			_visibleAreaPositoin.Y = ((_visibleAreaPositoin.Y > this.mapSize.Y - visibleAreaSides.Y) ? (this.mapSize.Y - visibleAreaSides.Y) : _visibleAreaPositoin.Y);
		}

		// Token: 0x06001791 RID: 6033 RVA: 0x000A0AE0 File Offset: 0x0009FAE0
		private bool VisibleAreaContainsPoint(Tools.Geometry.Point _targetPosition, out Tools.Geometry.Point movement)
		{
			movement = Tools.Geometry.Point.Empty;
			if (this.openMapParams != null)
			{
				Tools.Geometry.Point visibleAreaSides = new Tools.Geometry.Point(this.mapSize.X / this.rate, this.mapSize.Y / this.rate);
				Tools.Geometry.Point maxPosition = new Tools.Geometry.Point(this.visibleAreaPosition.X + visibleAreaSides.X - this.openMapParams.MapSize, this.visibleAreaPosition.Y + visibleAreaSides.Y - this.openMapParams.MapSize);
				Tools.Geometry.Point targetPosition = this.openMapParams.GetPatch();
				if (_targetPosition.X < this.visibleAreaPosition.X || targetPosition.X > maxPosition.X || _targetPosition.Y < this.visibleAreaPosition.Y || targetPosition.Y > maxPosition.Y)
				{
					Tools.Geometry.Point center = _targetPosition + new Tools.Geometry.Point(this.openMapParams.MapSize, this.openMapParams.MapSize) / 2.0;
					Tools.Geometry.Point _visibleAreaPosition = center - visibleAreaSides / 2.0;
					movement = _visibleAreaPosition - this.visibleAreaPosition;
					return true;
				}
			}
			return false;
		}

		// Token: 0x06001792 RID: 6034 RVA: 0x000A0C24 File Offset: 0x0009FC24
		private void SetPosition(Tools.Geometry.Point _targetPosition, bool centerArea)
		{
			if (this.openMapParams != null)
			{
				this.CorrectTargetPosition(ref _targetPosition, centerArea);
				this.openMapParams.SetPatch(_targetPosition);
				Tools.Geometry.Point movement;
				if (centerArea && this.VisibleAreaContainsPoint(_targetPosition, out movement))
				{
					this.MoveVisibleArea(movement);
				}
				PaintEventArgs e = new PaintEventArgs(Graphics.FromHwnd(base.Handle), base.ClientRectangle);
				base.InvokePaint(this, e);
			}
		}

		// Token: 0x06001793 RID: 6035 RVA: 0x000A0C84 File Offset: 0x0009FC84
		private void SetMapPath(string _mapPath)
		{
			if (!string.IsNullOrEmpty(_mapPath) && this.mapPath != _mapPath)
			{
				this.mapPath = _mapPath;
				if (this.ChangeImage())
				{
					PaintEventArgs pe = new PaintEventArgs(Graphics.FromHwnd(base.Handle), base.ClientRectangle);
					base.InvokePaint(this, pe);
				}
			}
		}

		// Token: 0x06001794 RID: 6036 RVA: 0x000A0CD8 File Offset: 0x0009FCD8
		private void OnMapParamChanged(OpenMapDialog.Params.Fields field)
		{
			if (this.openMapParams != null)
			{
				switch (field)
				{
				case OpenMapDialog.Params.Fields.Type:
				{
					this.drawMap = (this.openMapParams.Type == ContinentType.Continent);
					PaintEventArgs pe = new PaintEventArgs(Graphics.FromHwnd(base.Handle), base.ClientRectangle);
					base.InvokePaint(this, pe);
					return;
				}
				case OpenMapDialog.Params.Fields.Name:
					this.SetMapPath(Constants.GetLastChangedMegamapPath(this.openMapParams.ContinentName));
					return;
				case OpenMapDialog.Params.Fields.Position:
					this.SetPosition(this.openMapParams.GetPatch(), true);
					return;
				case OpenMapDialog.Params.Fields.Terrain:
				case OpenMapDialog.Params.Fields.Unit:
					break;
				case OpenMapDialog.Params.Fields.Size:
				{
					this.maxRate = Math.Max((Constants.WorldBounds.Width + 1) / this.openMapParams.MapSize, (Constants.WorldBounds.Height + 1) / this.openMapParams.MapSize) / 2;
					PaintEventArgs pe2 = new PaintEventArgs(Graphics.FromHwnd(base.Handle), base.ClientRectangle);
					base.InvokePaint(this, pe2);
					break;
				}
				default:
					return;
				}
			}
		}

		// Token: 0x06001795 RID: 6037 RVA: 0x000A0DD4 File Offset: 0x0009FDD4
		public MapDialogControl()
		{
			this.InitializeComponent();
			base.MouseMove += this.OnMouseMove;
			base.MouseDown += this.OnMouseDown;
			base.MouseUp += MapDialogControl.OnMouseUp;
		}

		// Token: 0x06001796 RID: 6038 RVA: 0x000A0E80 File Offset: 0x0009FE80
		public void Initialize(OpenMapDialog.Params _openMapParams, int _rate, Tools.Geometry.Point _visibleAreaPositoin)
		{
			if (this.openMapParams == null && _openMapParams != null)
			{
				this.openMapParams = _openMapParams;
				this.openMapParams.Changed += this.OnMapParamChanged;
				_rate = ((_rate > this.maxRate) ? this.maxRate : _rate);
				_rate = ((_rate < 1) ? 1 : _rate);
				this.rate = _rate;
				this.maxRate = Math.Max((Constants.WorldBounds.Width + 1) / this.openMapParams.MapSize, (Constants.WorldBounds.Height + 1) / this.openMapParams.MapSize) / 2;
				this.CorrectVisibleArea(ref _visibleAreaPositoin);
				this.visibleAreaPosition = _visibleAreaPositoin;
				this.ChangeImage();
			}
		}

		// Token: 0x06001797 RID: 6039 RVA: 0x000A0F3A File Offset: 0x0009FF3A
		public Tools.Geometry.Point GetVisibleAreaPosition()
		{
			return this.visibleAreaPosition;
		}

		// Token: 0x06001798 RID: 6040 RVA: 0x000A0F42 File Offset: 0x0009FF42
		public int GetZoom()
		{
			return this.rate;
		}

		// Token: 0x06001799 RID: 6041 RVA: 0x000A0F4C File Offset: 0x0009FF4C
		public void SetZoom(int _rate)
		{
			if (this.openMapParams != null)
			{
				_rate = ((_rate > this.maxRate) ? this.maxRate : _rate);
				_rate = ((_rate < 1) ? 1 : _rate);
				if (this.rate != _rate)
				{
					this.rate = _rate;
					Tools.Geometry.Point targetPosition = this.openMapParams.GetPatch();
					this.visibleAreaPosition.X = targetPosition.X + this.openMapParams.MapSize / 2 - this.mapSize.X / (2 * this.rate);
					this.visibleAreaPosition.Y = targetPosition.Y + this.openMapParams.MapSize / 2 - this.mapSize.Y / (2 * this.rate);
					this.CorrectVisibleArea(ref this.visibleAreaPosition);
					this.ChangeImage();
					PaintEventArgs pe = new PaintEventArgs(Graphics.FromHwnd(base.Handle), base.ClientRectangle);
					base.InvokePaint(this, pe);
				}
			}
		}

		// Token: 0x170005A9 RID: 1449
		// (get) Token: 0x0600179A RID: 6042 RVA: 0x000A103D File Offset: 0x000A003D
		// (set) Token: 0x0600179B RID: 6043 RVA: 0x000A1045 File Offset: 0x000A0045
		public bool EnableDraw
		{
			get
			{
				return this.enableDraw;
			}
			set
			{
				this.enableDraw = value;
			}
		}

		// Token: 0x0600179C RID: 6044 RVA: 0x000A104E File Offset: 0x000A004E
		protected override void Dispose(bool disposing)
		{
			if (disposing && this.components != null)
			{
				this.components.Dispose();
			}
			base.Dispose(disposing);
		}

		// Token: 0x0600179D RID: 6045 RVA: 0x000A1070 File Offset: 0x000A0070
		private void InitializeComponent()
		{
			base.SuspendLayout();
			base.AutoScaleDimensions = new SizeF(6f, 13f);
			base.AutoScaleMode = AutoScaleMode.Font;
			this.BackColor = SystemColors.Control;
			base.Name = "MapDialogControl";
			base.Size = new Size(250, 200);
			base.Resize += this.MapDialogControl_Resize;
			base.Paint += this.MapDialogControl_Paint;
			base.ResumeLayout(false);
		}

		// Token: 0x04000F60 RID: 3936
		private const int penWidth = 1;

		// Token: 0x04000F61 RID: 3937
		private int maxRate;

		// Token: 0x04000F62 RID: 3938
		private static readonly Color targetColor = Color.Red;

		// Token: 0x04000F63 RID: 3939
		private static readonly Pen pen = new Pen(Color.Black, 1f);

		// Token: 0x04000F64 RID: 3940
		private static readonly Pen targetPen = new Pen(MapDialogControl.targetColor, 1f);

		// Token: 0x04000F65 RID: 3941
		private Tools.Geometry.Point mapSize = new Tools.Geometry.Point(Constants.WorldBounds.Width + 1, Constants.WorldBounds.Height + 1);

		// Token: 0x04000F66 RID: 3942
		private OpenMapDialog.Params openMapParams;

		// Token: 0x04000F67 RID: 3943
		private Bitmap map;

		// Token: 0x04000F68 RID: 3944
		private string mapPath = string.Empty;

		// Token: 0x04000F69 RID: 3945
		private Tools.Geometry.Point visibleAreaPosition = new Tools.Geometry.Point(0, 0);

		// Token: 0x04000F6A RID: 3946
		private System.Drawing.Point imageMovementStartPoint = new System.Drawing.Point(0, 0);

		// Token: 0x04000F6B RID: 3947
		private int rate = 1;

		// Token: 0x04000F6C RID: 3948
		private bool drawMap = true;

		// Token: 0x04000F6D RID: 3949
		private bool enableDraw;

		// Token: 0x04000F6E RID: 3950
		private Bitmap drawBitmap;

		// Token: 0x04000F6F RID: 3951
		private SolidBrush solidBrush;

		// Token: 0x04000F70 RID: 3952
		private readonly IContainer components;
	}
}
