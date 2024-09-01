using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using InputState;
using LauncherTools.InputState;
using MapEditor.Forms.Base;
using MapEditor.Map;
using MapEditor.Properties;
using Tools.Geometry;
using Tools.Landscape;
using Tools.MapObjects;
using Tools.WindowParams;

namespace MapEditor.Forms.Minimap
{
	// Token: 0x02000164 RID: 356
	public partial class MinimapForm : BaseForm
	{
		// Token: 0x0600113D RID: 4413 RVA: 0x0007F8BC File Offset: 0x0007E8BC
		private void OnLoadParams(FormParams formParams)
		{
			base.ParamsSaver.FormParams.ResizeInt(2, new int[]
			{
				FormParamsSaver.BoolToInt(this.showPatchGrid),
				FormParamsSaver.BoolToInt(this.showTerrainRegions)
			});
			this.SetShowPatchGrid(FormParamsSaver.IntToBool(base.ParamsSaver.FormParams.GetInt(0)));
			this.SetShowTerrainRegions(FormParamsSaver.IntToBool(base.ParamsSaver.FormParams.GetInt(1)));
			base.ParamsSaver.FormParams.ResizeString(1, new string[]
			{
				"TerrainMode"
			});
			this.minimapMode = base.ParamsSaver.FormParams.GetString(0);
		}

		// Token: 0x0600113E RID: 4414 RVA: 0x0007F970 File Offset: 0x0007E970
		private void OnSaveParams(FormParams formParams)
		{
			base.ParamsSaver.FormParams.ResizeInt(2);
			base.ParamsSaver.FormParams.SetInt(0, FormParamsSaver.BoolToInt(this.showPatchGrid));
			base.ParamsSaver.FormParams.SetInt(1, FormParamsSaver.BoolToInt(this.showTerrainRegions));
			base.ParamsSaver.FormParams.ResizeString(1);
			base.ParamsSaver.FormParams.SetString(0, this.minimapMode);
		}

		// Token: 0x0600113F RID: 4415 RVA: 0x0007F9F0 File Offset: 0x0007E9F0
		private void OnImagePanelPaint(object sender, PaintEventArgs e)
		{
			if (base.Visible && this.mimimapBitmap != null && this.minimapGraphics != null)
			{
				Rectangle minimapRectangle = this.ImagePanel.ClientRectangle;
				if (minimapRectangle.Width > 0 && minimapRectangle.Height > 0)
				{
					Graphics graphics = e.Graphics;
					if (this.MapBinded)
					{
						Bitmap minimap = this.map.MinimapContainer.GetMinimap();
						if (minimap != null)
						{
							this.minimapGraphics.DrawImage(minimap, minimapRectangle);
						}
						if (this.showTerrainRegions)
						{
							this.DrawTerrainRegions(this.minimapGraphics);
						}
						if (this.showPatchGrid)
						{
							this.DrawPatchGrid(this.minimapGraphics);
						}
						this.DrawCamera(this.minimapGraphics);
					}
					if (this.camera != null && this.camera.TerrainIndex == 1)
					{
						Matrix mymat = new Matrix();
						mymat.Scale(-1f, 1f);
						mymat.Translate((float)(-1 * minimapRectangle.Width), 0f);
						graphics.Transform = mymat;
						graphics.DrawImage(this.mimimapBitmap, minimapRectangle);
						graphics.ResetTransform();
						return;
					}
					graphics.DrawImage(this.mimimapBitmap, minimapRectangle);
				}
			}
		}

		// Token: 0x06001140 RID: 4416 RVA: 0x0007FB14 File Offset: 0x0007EB14
		private void DrawCamera(Graphics graphics)
		{
			System.Drawing.Point center;
			int radius;
			float yaw;
			if (graphics != null && this.camera != null && this.camera.DrawCamera(out center, out radius, out yaw, this.ImagePanel.ClientRectangle))
			{
				int ellipseLeftTopX = center.X - radius;
				int ellipseLeftTopY = center.Y - radius;
				int lineStartX = center.X;
				int lineStartY = center.Y;
				float radius2 = (float)radius * 1.8f;
				int lineEndX = center.X + (int)(Math.Cos((double)yaw) * (double)radius2);
				int lineEndY = center.Y - (int)(Math.Sin((double)yaw) * (double)radius2);
				int radius3 = radius * 2;
				graphics.DrawEllipse(MinimapForm.cameraPenShadow, ellipseLeftTopX + 1, ellipseLeftTopY + 1, radius3, radius3);
				graphics.DrawEllipse(MinimapForm.cameraPenShadow, ellipseLeftTopX, ellipseLeftTopY + 1, radius3, radius3);
				graphics.DrawEllipse(MinimapForm.cameraPenShadow, ellipseLeftTopX + 1, ellipseLeftTopY, radius3, radius3);
				graphics.DrawLine(MinimapForm.cameraPenShadow, lineStartX + 1, lineStartY + 1, lineEndX + 1, lineEndY + 1);
				graphics.DrawLine(MinimapForm.cameraPenShadow, lineStartX, lineStartY + 1, lineEndX, lineEndY + 1);
				graphics.DrawLine(MinimapForm.cameraPenShadow, lineStartX + 1, lineStartY, lineEndX + 1, lineEndY);
				graphics.DrawEllipse(MinimapForm.cameraPen, ellipseLeftTopX, ellipseLeftTopY, radius3, radius3);
				graphics.DrawLine(MinimapForm.cameraPen, lineStartX, lineStartY, lineEndX, lineEndY);
			}
		}

		// Token: 0x06001141 RID: 4417 RVA: 0x0007FC64 File Offset: 0x0007EC64
		private void DrawPatchGrid(Graphics graphics)
		{
			if (graphics != null)
			{
				Rectangle mmRectangle = this.ImagePanel.ClientRectangle;
				Pen pen = new Pen(Color.DimGray, 1f);
				pen.DashStyle = DashStyle.Dot;
				for (int horizontalLine = 1; horizontalLine < this.map.Data.MapSize.X; horizontalLine++)
				{
					int horizontalLineY = (mmRectangle.Height - 2) * horizontalLine / this.map.Data.MapSize.Y;
					graphics.DrawLine(pen, 0, horizontalLineY + 1, mmRectangle.Width, horizontalLineY + 1);
				}
				for (int verticalLine = 1; verticalLine < this.map.Data.MapSize.Y; verticalLine++)
				{
					int verticalLineX = (mmRectangle.Width - 2) * verticalLine / this.map.Data.MapSize.X;
					graphics.DrawLine(pen, verticalLineX + 1, 0, verticalLineX + 1, mmRectangle.Height);
				}
				pen.Color = Color.White;
				for (int horizontalLine2 = 1; horizontalLine2 < this.map.Data.MapSize.X; horizontalLine2++)
				{
					int horizontalLineY2 = (mmRectangle.Height - 2) * horizontalLine2 / this.map.Data.MapSize.Y;
					graphics.DrawLine(pen, 0, horizontalLineY2, mmRectangle.Width, horizontalLineY2);
				}
				for (int verticalLine2 = 1; verticalLine2 < this.map.Data.MapSize.Y; verticalLine2++)
				{
					int verticalLineX2 = (mmRectangle.Width - 2) * verticalLine2 / this.map.Data.MapSize.X;
					graphics.DrawLine(pen, verticalLineX2, 0, verticalLineX2, mmRectangle.Height);
				}
			}
		}

		// Token: 0x06001142 RID: 4418 RVA: 0x0007FE34 File Offset: 0x0007EE34
		private void DrawTerrainRegions(Graphics graphics)
		{
			if (graphics != null && base.Context != null && this.map != null)
			{
				Rectangle mmRectangle = this.ImagePanel.ClientRectangle;
				int patchSizeX = mmRectangle.Width / this.map.Data.MapSize.X;
				int patchSizeY = mmRectangle.Height / this.map.Data.MapSize.Y;
				int offsetX = 3;
				int offsetY = 3;
				if (patchSizeX < offsetX * 2)
				{
					offsetX = 0;
				}
				if (patchSizeY < offsetY * 2)
				{
					offsetY = 0;
				}
				Brush brush = new HatchBrush(HatchStyle.DiagonalCross, MinimapForm.terrainRegionColor, MinimapForm.terrainRegionBackgroundColor);
				Pen pen = new Pen(MinimapForm.terrainRegionColor);
				for (int x = 0; x < this.map.Data.MapSize.X; x++)
				{
					for (int y = 0; y < this.map.Data.MapSize.Y; y++)
					{
						if (!base.Context.EditorScene.DoesNumberedTerrainRegionExist(this.camera.TerrainIndex, x, y))
						{
							Rectangle rectangle = new Rectangle(mmRectangle.Left + x * patchSizeX + offsetX - 1, mmRectangle.Bottom - (y + 1) * patchSizeY + offsetY - 1, patchSizeX - offsetX * 2, patchSizeY - offsetY * 2);
							graphics.DrawRectangle(pen, rectangle);
							graphics.FillRectangle(brush, rectangle);
						}
					}
				}
			}
		}

		// Token: 0x06001143 RID: 4419 RVA: 0x0007FFA8 File Offset: 0x0007EFA8
		private void ProcessRegion(System.Drawing.Point position)
		{
			if (base.Context != null && this.map != null)
			{
				Rectangle mmRectangle = this.ImagePanel.ClientRectangle;
				int patchSizeX = mmRectangle.Width / this.map.Data.MapSize.X;
				int patchSizeY = mmRectangle.Height / this.map.Data.MapSize.Y;
				if (patchSizeX > 0 && patchSizeY > 0)
				{
					int x;
					int y;
					if (this.camera.TerrainIndex == 0)
					{
						x = (position.X - mmRectangle.Left) / patchSizeX;
						y = (mmRectangle.Bottom - position.Y) / patchSizeY;
					}
					else
					{
						x = (mmRectangle.Right - position.X) / patchSizeX;
						y = (mmRectangle.Bottom - position.Y) / patchSizeY;
					}
					if (base.Context.EditorScene.DoesNumberedTerrainRegionExist(this.camera.TerrainIndex, x, y))
					{
						this.map.TerrainRegionContainer.DeleteTerrainRegion(this.camera.TerrainIndex, x, y);
						return;
					}
					this.map.TerrainRegionContainer.CreateTerrainRegion(this.camera.TerrainIndex, x, y);
				}
			}
		}

		// Token: 0x06001144 RID: 4420 RVA: 0x000800E4 File Offset: 0x0007F0E4
		private void ImagePanel_MouseDown(object sender, MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Left || e.Button == MouseButtons.Right)
			{
				KeyStatus.ClearCache();
				if (KeyStatus.Shift && this.showTerrainRegions)
				{
					this.ProcessRegion(e.Location);
					return;
				}
				this.MoveCamera(e.Location);
			}
		}

		// Token: 0x06001145 RID: 4421 RVA: 0x00080138 File Offset: 0x0007F138
		private void ImagePanel_MouseMove(object sender, MouseEventArgs e)
		{
			if ((e.Button == MouseButtons.Left || e.Button == MouseButtons.Right) && (!KeyStatus.Shift || !this.showTerrainRegions))
			{
				this.MoveCamera(e.Location);
			}
		}

		// Token: 0x06001146 RID: 4422 RVA: 0x0008016F File Offset: 0x0007F16F
		private void MoveCamera(System.Drawing.Point position)
		{
			if (this.camera != null)
			{
				this.camera.MoveCamera(position, this.ImagePanel.ClientRectangle);
			}
		}

		// Token: 0x06001147 RID: 4423 RVA: 0x00080190 File Offset: 0x0007F190
		private void MinimapForm_VisibleChanged(object sender, EventArgs e)
		{
			this.UpdateMinimapMode();
		}

		// Token: 0x06001148 RID: 4424 RVA: 0x00080198 File Offset: 0x0007F198
		private void MinimapForm_FormClosed(object sender, EventArgs e)
		{
			base.Context.StateContainer.UnbindState(this.minimapFormState);
		}

		// Token: 0x06001149 RID: 4425 RVA: 0x000801B0 File Offset: 0x0007F1B0
		private void OnAfterEditorSceneStep(EditorScene _scene)
		{
			if (this.camera != null && this.camera.CameraPositionWasChanged())
			{
				this.Repaint();
			}
		}

		// Token: 0x0600114A RID: 4426 RVA: 0x000801CD File Offset: 0x0007F1CD
		private void OnTerrainRegionContainerModified(TerrainRegionContainer _terrainRegionContainer)
		{
			this.Repaint();
		}

		// Token: 0x0600114B RID: 4427 RVA: 0x000801D8 File Offset: 0x0007F1D8
		private void OnImagePanelResize(object sender, EventArgs e)
		{
			if (this.minimapGraphics != null)
			{
				this.minimapGraphics.Dispose();
			}
			if (this.mimimapBitmap != null)
			{
				this.mimimapBitmap.Dispose();
			}
			Rectangle minimapRectangle = this.ImagePanel.ClientRectangle;
			this.mimimapBitmap = new Bitmap(minimapRectangle.Width, minimapRectangle.Height);
			this.minimapGraphics = Graphics.FromImage(this.mimimapBitmap);
			this.Repaint();
		}

		// Token: 0x0600114C RID: 4428 RVA: 0x00080247 File Offset: 0x0007F247
		private void ShowPatchGridButton_Click(object sender, EventArgs e)
		{
			this.SetShowPatchGrid(!this.showPatchGrid);
		}

		// Token: 0x0600114D RID: 4429 RVA: 0x00080258 File Offset: 0x0007F258
		private void TerrainRegionsButton_Click(object sender, EventArgs e)
		{
			this.SetShowTerrainRegions(!this.showTerrainRegions);
		}

		// Token: 0x0600114E RID: 4430 RVA: 0x00080269 File Offset: 0x0007F269
		private void TerrainMMButton_Click(object sender, EventArgs e)
		{
			this.minimapMode = "TerrainMode";
			this.UpdateMinimapMode();
		}

		// Token: 0x0600114F RID: 4431 RVA: 0x0008027C File Offset: 0x0007F27C
		private void BottomTerrainMMButton_Click(object sender, EventArgs e)
		{
			this.minimapMode = "LowerTerrainMode";
			this.UpdateMinimapMode();
		}

		// Token: 0x06001150 RID: 4432 RVA: 0x0008028F File Offset: 0x0007F28F
		private void HeightMMButton_Click(object sender, EventArgs e)
		{
			this.minimapMode = "HeightMode";
			this.UpdateMinimapMode();
		}

		// Token: 0x06001151 RID: 4433 RVA: 0x000802A2 File Offset: 0x0007F2A2
		private void BottomHeightMMButton_Click(object sender, EventArgs e)
		{
			this.minimapMode = "LowerHeightMode";
			this.UpdateMinimapMode();
		}

		// Token: 0x06001152 RID: 4434 RVA: 0x000802B5 File Offset: 0x0007F2B5
		private void ZoneMMButton_Click(object sender, EventArgs e)
		{
			this.minimapMode = "ZoneMode";
			this.UpdateMinimapMode();
		}

		// Token: 0x06001153 RID: 4435 RVA: 0x000802C8 File Offset: 0x0007F2C8
		private void LightMMButton_Click(object sender, EventArgs e)
		{
			this.minimapMode = "LightMode";
			this.UpdateMinimapMode();
		}

		// Token: 0x06001154 RID: 4436 RVA: 0x000802DB File Offset: 0x0007F2DB
		private void AmbientMMButton_Click(object sender, EventArgs e)
		{
			this.minimapMode = "ambientMode";
			this.UpdateMinimapMode();
		}

		// Token: 0x06001155 RID: 4437 RVA: 0x000802EE File Offset: 0x0007F2EE
		private void MusicMMButton_Click(object sender, EventArgs e)
		{
			this.minimapMode = "musicMode";
			this.UpdateMinimapMode();
		}

		// Token: 0x06001156 RID: 4438 RVA: 0x00080301 File Offset: 0x0007F301
		private void OnMinimapRepaint(MethodArgs methodArgs)
		{
			if (base.Visible && this.map != null)
			{
				this.map.MinimapContainer.Repaint();
			}
		}

		// Token: 0x06001157 RID: 4439 RVA: 0x00080323 File Offset: 0x0007F323
		private void OnMinimapRefresh(MethodArgs methodArgs)
		{
			if (base.Visible && this.map != null)
			{
				this.Repaint();
			}
		}

		// Token: 0x06001158 RID: 4440 RVA: 0x0008033C File Offset: 0x0007F33C
		private void UpdateMinimapModeControls()
		{
			if (this.minimapMode == "TerrainMode")
			{
				this.TerrainMMButton.Checked = true;
				this.BottomTerrainMMButton.Checked = false;
				this.HeightMMButton.Checked = false;
				this.BottomHeightMMButton.Checked = false;
				this.ZoneMMButton.Checked = false;
				this.LightMMButton.Checked = false;
				this.AmbientMMButton.Checked = false;
				this.MusicMMButton.Checked = false;
				if (this.camera != null)
				{
					this.camera.TerrainIndex = 0;
					return;
				}
			}
			else if (this.minimapMode == "LowerTerrainMode")
			{
				this.TerrainMMButton.Checked = false;
				this.BottomTerrainMMButton.Checked = true;
				this.HeightMMButton.Checked = false;
				this.BottomHeightMMButton.Checked = false;
				this.ZoneMMButton.Checked = false;
				this.LightMMButton.Checked = false;
				this.AmbientMMButton.Checked = false;
				this.MusicMMButton.Checked = false;
				if (this.camera != null)
				{
					this.camera.TerrainIndex = 1;
					return;
				}
			}
			else if (this.minimapMode == "HeightMode")
			{
				this.TerrainMMButton.Checked = false;
				this.BottomTerrainMMButton.Checked = false;
				this.HeightMMButton.Checked = true;
				this.BottomHeightMMButton.Checked = false;
				this.ZoneMMButton.Checked = false;
				this.LightMMButton.Checked = false;
				this.AmbientMMButton.Checked = false;
				this.MusicMMButton.Checked = false;
				if (this.camera != null)
				{
					this.camera.TerrainIndex = 0;
					return;
				}
			}
			else if (this.minimapMode == "LowerHeightMode")
			{
				this.TerrainMMButton.Checked = false;
				this.BottomTerrainMMButton.Checked = false;
				this.HeightMMButton.Checked = false;
				this.BottomHeightMMButton.Checked = true;
				this.ZoneMMButton.Checked = false;
				this.LightMMButton.Checked = false;
				this.AmbientMMButton.Checked = false;
				this.MusicMMButton.Checked = false;
				if (this.camera != null)
				{
					this.camera.TerrainIndex = 1;
					return;
				}
			}
			else if (this.minimapMode == "ZoneMode")
			{
				this.TerrainMMButton.Checked = false;
				this.BottomTerrainMMButton.Checked = false;
				this.HeightMMButton.Checked = false;
				this.BottomHeightMMButton.Checked = false;
				this.ZoneMMButton.Checked = true;
				this.LightMMButton.Checked = false;
				this.AmbientMMButton.Checked = false;
				this.MusicMMButton.Checked = false;
				if (this.camera != null)
				{
					this.camera.TerrainIndex = 0;
					return;
				}
			}
			else if (this.minimapMode == "LightMode")
			{
				this.TerrainMMButton.Checked = false;
				this.BottomTerrainMMButton.Checked = false;
				this.HeightMMButton.Checked = false;
				this.BottomHeightMMButton.Checked = false;
				this.ZoneMMButton.Checked = false;
				this.LightMMButton.Checked = true;
				this.AmbientMMButton.Checked = false;
				this.MusicMMButton.Checked = false;
				if (this.camera != null)
				{
					this.camera.TerrainIndex = 0;
					return;
				}
			}
			else if (this.minimapMode == "ambientMode")
			{
				this.TerrainMMButton.Checked = false;
				this.BottomTerrainMMButton.Checked = false;
				this.HeightMMButton.Checked = false;
				this.BottomHeightMMButton.Checked = false;
				this.ZoneMMButton.Checked = false;
				this.LightMMButton.Checked = false;
				this.AmbientMMButton.Checked = true;
				this.MusicMMButton.Checked = false;
				if (this.camera != null)
				{
					this.camera.TerrainIndex = 0;
					return;
				}
			}
			else if (this.minimapMode == "musicMode")
			{
				this.TerrainMMButton.Checked = false;
				this.BottomTerrainMMButton.Checked = false;
				this.HeightMMButton.Checked = false;
				this.BottomHeightMMButton.Checked = false;
				this.ZoneMMButton.Checked = false;
				this.LightMMButton.Checked = false;
				this.AmbientMMButton.Checked = false;
				this.MusicMMButton.Checked = true;
				if (this.camera != null)
				{
					this.camera.TerrainIndex = 0;
				}
			}
		}

		// Token: 0x06001159 RID: 4441 RVA: 0x00080795 File Offset: 0x0007F795
		private void OnApplyMinimapChanges(MethodArgs methodArgs)
		{
			if (base.Visible && this.map != null)
			{
				this.map.MinimapContainer.AppyChanges();
			}
		}

		// Token: 0x0600115A RID: 4442 RVA: 0x000807B7 File Offset: 0x0007F7B7
		private void OnUndoMinimapChanges(MethodArgs methodArgs)
		{
			if (base.Visible && this.map != null)
			{
				this.map.MinimapContainer.UndoChanges();
			}
		}

		// Token: 0x0600115B RID: 4443 RVA: 0x000807DC File Offset: 0x0007F7DC
		private void UpdateMinimapMode()
		{
			this.UpdateMinimapModeControls();
			if (this.map != null)
			{
				if (base.Visible)
				{
					this.map.MinimapContainer.SetMinimapMode(this.minimapMode);
					return;
				}
				this.map.MinimapContainer.SetMinimapMode(string.Empty);
			}
		}

		// Token: 0x0600115C RID: 4444 RVA: 0x0008082C File Offset: 0x0007F82C
		private void Repaint()
		{
			if (base.Visible && this.map != null)
			{
				PaintEventArgs paintEventArgs = new PaintEventArgs(Graphics.FromHwnd(this.ImagePanel.Handle), this.ImagePanel.ClientRectangle);
				this.OnImagePanelPaint(this.ImagePanel, paintEventArgs);
			}
		}

		// Token: 0x0600115D RID: 4445 RVA: 0x00080877 File Offset: 0x0007F877
		private void SetShowPatchGrid(bool show)
		{
			if (this.showPatchGrid != show)
			{
				this.showPatchGrid = show;
				this.Repaint();
			}
			this.PatchGridButton.Checked = this.showPatchGrid;
		}

		// Token: 0x0600115E RID: 4446 RVA: 0x000808A0 File Offset: 0x0007F8A0
		private void SetShowTerrainRegions(bool show)
		{
			if (this.showTerrainRegions != show)
			{
				this.showTerrainRegions = show;
				this.Repaint();
			}
			this.TerrainRegionsButton.Checked = this.showTerrainRegions;
		}

		// Token: 0x1700037C RID: 892
		// (get) Token: 0x0600115F RID: 4447 RVA: 0x000808C9 File Offset: 0x0007F8C9
		private bool MapBinded
		{
			get
			{
				return this.map != null;
			}
		}

		// Token: 0x06001160 RID: 4448 RVA: 0x000808D8 File Offset: 0x0007F8D8
		private void GetPatch(System.Drawing.Point point, out int patchX, out int patchY)
		{
			patchX = point.X * this.map.Data.MapSize.X / this.ImagePanel.ClientSize.Width;
			patchY = this.map.Data.MapSize.Y - 1 - point.Y * this.map.Data.MapSize.Y / this.ImagePanel.ClientSize.Height;
		}

		// Token: 0x06001161 RID: 4449 RVA: 0x00080970 File Offset: 0x0007F970
		private void OnContextMenuStripOpening(object sender, CancelEventArgs ce)
		{
			System.Drawing.Point mousePos = Cursor.Position;
			if (this.map != null && base.Context.EditorSceneParams.BlockEditing && this.ImagePanel.ClientSize.Height > 0 && this.ImagePanel.ClientSize.Width > 0)
			{
				int patchX;
				int patchY;
				this.GetPatch(this.ImagePanel.PointToClient(mousePos), out patchX, out patchY);
				bool patchIsBlocked = this.map.EditingBlocker.PatchIsBlocked(patchX, patchY);
				this.blockPatchToolStripMenuItem.Enabled = !patchIsBlocked;
				this.unblockPatchToolStripMenuItem.Enabled = patchIsBlocked;
				this.contextMenuStrip.Tag = new System.Drawing.Point(patchX, patchY);
				return;
			}
			ce.Cancel = true;
		}

		// Token: 0x06001162 RID: 4450 RVA: 0x00080A34 File Offset: 0x0007FA34
		private void OnContextMenuStripItemClicked(object sender, ToolStripItemClickedEventArgs e)
		{
			if (this.map != null && this.contextMenuStrip.Tag is System.Drawing.Point)
			{
				System.Drawing.Point point = (System.Drawing.Point)this.contextMenuStrip.Tag;
				if (e.ClickedItem == this.blockPatchToolStripMenuItem)
				{
					this.map.EditingBlocker.BlockPatch(point.X, point.Y, true);
					return;
				}
				if (e.ClickedItem == this.unblockPatchToolStripMenuItem)
				{
					this.map.EditingBlocker.BlockPatch(point.X, point.Y, false);
				}
			}
		}

		// Token: 0x06001163 RID: 4451 RVA: 0x00080AC8 File Offset: 0x0007FAC8
		public MinimapForm(MainForm.Context context) : base(EditorEnvironment.EditorFormsFolder + "MinimapFrom.xml", context)
		{
			this.InitializeComponent();
			Rectangle minimapRectangle = this.ImagePanel.ClientRectangle;
			this.mimimapBitmap = new Bitmap(minimapRectangle.Width, minimapRectangle.Height);
			this.minimapGraphics = Graphics.FromImage(this.mimimapBitmap);
			this.minimapFormState.AddMethod("_minimap_repaint", new Method(this.OnMinimapRepaint));
			this.minimapFormState.AddMethod("_minimap_refresh", new Method(this.OnMinimapRefresh));
			this.minimapFormState.AddMethod("_landscape_state_applied", new Method(this.OnApplyMinimapChanges));
			this.minimapFormState.AddMethod("_map_zone_state_applied", new Method(this.OnApplyMinimapChanges));
			this.minimapFormState.AddMethod("_zone_lights_state_applied", new Method(this.OnApplyMinimapChanges));
			this.minimapFormState.AddMethod("_sound_state_ambient_applied", new Method(this.OnApplyMinimapChanges));
			this.minimapFormState.AddMethod("_sound_state_music_applied", new Method(this.OnApplyMinimapChanges));
			this.minimapFormState.AddMethod("_landscape_state_not_applied", new Method(this.OnUndoMinimapChanges));
			this.minimapFormState.AddMethod("_map_zone_state_not_applied", new Method(this.OnUndoMinimapChanges));
			this.minimapFormState.AddMethod("_zone_lights_state_not_applied", new Method(this.OnUndoMinimapChanges));
			this.minimapFormState.AddMethod("_sound_state_ambient_not_applied", new Method(this.OnUndoMinimapChanges));
			this.minimapFormState.AddMethod("_sound_state_music_not_applied", new Method(this.OnUndoMinimapChanges));
			base.Context.StateContainer.BindState(this.minimapFormState);
			this.ImagePanel.Paint += this.OnImagePanelPaint;
			this.ImagePanel.MouseDown += this.ImagePanel_MouseDown;
			this.ImagePanel.MouseMove += this.ImagePanel_MouseMove;
			this.ImagePanel.Resize += this.OnImagePanelResize;
			this.PatchGridButton.Click += this.ShowPatchGridButton_Click;
			this.TerrainRegionsButton.Click += this.TerrainRegionsButton_Click;
			this.TerrainMMButton.Click += this.TerrainMMButton_Click;
			this.BottomTerrainMMButton.Click += this.BottomTerrainMMButton_Click;
			this.HeightMMButton.Click += this.HeightMMButton_Click;
			this.BottomHeightMMButton.Click += this.BottomHeightMMButton_Click;
			this.ZoneMMButton.Click += this.ZoneMMButton_Click;
			this.LightMMButton.Click += this.LightMMButton_Click;
			this.AmbientMMButton.Click += this.AmbientMMButton_Click;
			this.MusicMMButton.Click += this.MusicMMButton_Click;
			base.ParamsSaver.LoadParams += this.OnLoadParams;
			base.ParamsSaver.SaveParams += this.OnSaveParams;
			base.VisibleChanged += this.MinimapForm_VisibleChanged;
			base.Closed += this.MinimapForm_FormClosed;
			this.contextMenuStrip.Opening += this.OnContextMenuStripOpening;
			this.contextMenuStrip.ItemClicked += this.OnContextMenuStripItemClicked;
		}

		// Token: 0x06001164 RID: 4452 RVA: 0x00080E6C File Offset: 0x0007FE6C
		public void PostBind(MapEditorMap _map)
		{
			this.Unbind();
			this.map = _map;
			if (this.map != null)
			{
				this.camera = new MinimapForm.Camera(base.Context, this.map.Data.MinXMinYPatchCoords, this.map.Data.MapSize);
				base.Context.EditorScene.AfterStep += this.OnAfterEditorSceneStep;
				if (this.map != null)
				{
					this.map.TerrainRegionContainer.Modified += this.OnTerrainRegionContainerModified;
				}
				this.UpdateMinimapMode();
				this.ImagePanel.ContextMenuStrip = this.contextMenuStrip;
			}
		}

		// Token: 0x06001165 RID: 4453 RVA: 0x00080F1C File Offset: 0x0007FF1C
		public void Unbind()
		{
			if (this.map != null)
			{
				base.Context.EditorScene.AfterStep -= this.OnAfterEditorSceneStep;
				this.map.TerrainRegionContainer.Modified -= this.OnTerrainRegionContainerModified;
				this.map.MinimapContainer.SetMinimapMode(string.Empty);
			}
			this.map = null;
			this.camera = null;
			this.ImagePanel.ContextMenuStrip = null;
		}

		// Token: 0x04000C67 RID: 3175
		private static readonly Color terrainRegionColor = Color.FromArgb(200, Color.LightCoral);

		// Token: 0x04000C68 RID: 3176
		private static readonly Color terrainRegionBackgroundColor = Color.FromArgb(0, Color.Black);

		// Token: 0x04000C69 RID: 3177
		private static readonly Pen cameraPenShadow = new Pen(Color.FromArgb(255, 64, 64, 64), 1f);

		// Token: 0x04000C6A RID: 3178
		private static readonly Pen cameraPen = new Pen(Color.Pink, 1f);

		// Token: 0x04000C6B RID: 3179
		private MapEditorMap map;

		// Token: 0x04000C6C RID: 3180
		private readonly State minimapFormState = new State("MinimapFormState");

		// Token: 0x04000C6D RID: 3181
		private MinimapForm.Camera camera;

		// Token: 0x04000C6E RID: 3182
		private bool showPatchGrid;

		// Token: 0x04000C6F RID: 3183
		private bool showTerrainRegions;

		// Token: 0x04000C70 RID: 3184
		private string minimapMode = "TerrainMode";

		// Token: 0x04000C71 RID: 3185
		private Bitmap mimimapBitmap;

		// Token: 0x04000C72 RID: 3186
		private Graphics minimapGraphics;

		// Token: 0x02000165 RID: 357
		private class Camera
		{
			// Token: 0x06001169 RID: 4457 RVA: 0x0008156C File Offset: 0x0008056C
			private float GetTerrainHeight(double x, double y)
			{
				Position onTerrainPos = new Position(x, y, 0.0);
				return this.context.EditorScene.GetTerrainHeight(this.context.EditorSceneViewID, this.terrainIndex, ref onTerrainPos);
			}

			// Token: 0x0600116A RID: 4458 RVA: 0x000815B4 File Offset: 0x000805B4
			private double GetDistanceToTerrain()
			{
				double height = this.cameraPlacement.Position.Z - (double)this.GetTerrainHeight(this.cameraPlacement.Position.X, this.cameraPlacement.Position.Y);
				height = Math.Abs(height);
				if (this.terrainIndex != 0)
				{
					height *= -1.0;
				}
				return height;
			}

			// Token: 0x0600116B RID: 4459 RVA: 0x00081620 File Offset: 0x00080620
			private System.Drawing.Point PositionToPoint(Position mapPosition, Rectangle _mmRectangle)
			{
				double mapSizeX = (double)(Constants.PatchSize * this.mapSize.X);
				double mapSizeY = (double)(Constants.PatchSize * this.mapSize.Y);
				double posX = mapPosition.X - (double)(this.minXMinYPatchCoords.X * Constants.PatchSize);
				double posY = mapPosition.Y - (double)(this.minXMinYPatchCoords.Y * Constants.PatchSize);
				double mmPosX = posX * (double)_mmRectangle.Width / mapSizeX;
				double mmPosY = posY * (double)_mmRectangle.Height / mapSizeY;
				Tools.Geometry.Point gpoint = new Tools.Geometry.Point((int)Math.Round(mmPosX), (int)Math.Round(mmPosY));
				return MinimapForm.Camera.GPointToPoint(gpoint, _mmRectangle);
			}

			// Token: 0x0600116C RID: 4460 RVA: 0x000816CC File Offset: 0x000806CC
			private Position PointToPosition(System.Drawing.Point point, Rectangle _mmRectangle)
			{
				Tools.Geometry.Point gpoint = MinimapForm.Camera.PointToGPoint(point, _mmRectangle);
				double mapSizeX = (double)(Constants.PatchSize * this.mapSize.X);
				double mapSizeY = (double)(Constants.PatchSize * this.mapSize.Y);
				double mmPosX = (double)gpoint.X * mapSizeX / (double)_mmRectangle.Width;
				double mmPosY = (double)gpoint.Y * mapSizeY / (double)_mmRectangle.Height;
				double posX = mmPosX + (double)(this.minXMinYPatchCoords.X * Constants.PatchSize);
				double posY = mmPosY + (double)(this.minXMinYPatchCoords.Y * Constants.PatchSize);
				double height = this.GetDistanceToTerrain();
				Position position = new Position(posX, posY, (double)this.GetTerrainHeight(posX, posY) + height);
				return position;
			}

			// Token: 0x0600116D RID: 4461 RVA: 0x00081788 File Offset: 0x00080788
			private int ScaleToMapXSize(int size, Rectangle _mmRectangle)
			{
				return size * _mmRectangle.Width / (Constants.PatchSize * this.mapSize.X);
			}

			// Token: 0x0600116E RID: 4462 RVA: 0x000817B4 File Offset: 0x000807B4
			private static bool CPlacementAreEquals(CameraPlacement cp1, CameraPlacement cp2)
			{
				return Math.Abs(cp1.Position.X - cp2.Position.X) <= 1.0 && Math.Abs(cp1.Position.Y - cp2.Position.Y) <= 1.0 && Math.Abs(cp1.Position.Z - cp2.Position.Z) <= 1.0 && Math.Abs(cp1.Rotation.Yaw - cp2.Rotation.Yaw) <= 0.01f && Math.Abs(cp1.Rotation.Pitch - cp2.Rotation.Pitch) <= 0.01f && Math.Abs(cp1.Rotation.Roll - cp2.Rotation.Roll) <= 0.01f;
			}

			// Token: 0x0600116F RID: 4463 RVA: 0x000818E4 File Offset: 0x000808E4
			private static System.Drawing.Point GPointToPoint(Tools.Geometry.Point gPoint, Rectangle _mmRectangle)
			{
				return new System.Drawing.Point(_mmRectangle.X + gPoint.X, _mmRectangle.Y + _mmRectangle.Height - gPoint.Y);
			}

			// Token: 0x06001170 RID: 4464 RVA: 0x00081911 File Offset: 0x00080911
			private static Tools.Geometry.Point PointToGPoint(System.Drawing.Point point, Rectangle _mmRectangle)
			{
				return new Tools.Geometry.Point(point.X - _mmRectangle.X, _mmRectangle.Height - (point.Y - _mmRectangle.Y));
			}

			// Token: 0x06001171 RID: 4465 RVA: 0x00081940 File Offset: 0x00080940
			public Camera(MainForm.Context _context, Tools.Geometry.Point _minXMinYPatchCoords, Tools.Geometry.Point _mapSize)
			{
				this.context = _context;
				this.minXMinYPatchCoords.X = _minXMinYPatchCoords.X;
				this.minXMinYPatchCoords.Y = _minXMinYPatchCoords.Y;
				this.mapSize = _mapSize;
			}

			// Token: 0x06001172 RID: 4466 RVA: 0x00081994 File Offset: 0x00080994
			public bool DrawCamera(out System.Drawing.Point center, out int radius, out float yaw, Rectangle _mmRectangle)
			{
				center = System.Drawing.Point.Empty;
				radius = 0;
				yaw = 0f;
				if (this.context.EditorScene != null)
				{
					center = this.PositionToPoint(this.cameraPlacement.Position, _mmRectangle);
					double height = this.GetDistanceToTerrain();
					double fov = this.context.EditorScene.GetCameraFOV(this.context.EditorSceneViewID);
					radius = Math.Abs(this.ScaleToMapXSize((int)(height * Math.Tan(fov / 2.0)), _mmRectangle));
					if (radius < 8)
					{
						radius = 8;
					}
					yaw = this.cameraPlacement.Rotation.Yaw;
					return true;
				}
				return false;
			}

			// Token: 0x06001173 RID: 4467 RVA: 0x00081A44 File Offset: 0x00080A44
			public void MoveCamera(System.Drawing.Point point, Rectangle _mmRectangle)
			{
				if (_mmRectangle.Contains(point) && this.context.EditorScene != null)
				{
					if (this.terrainIndex == 1)
					{
						point.X = _mmRectangle.Right - point.X;
					}
					Position mapPosition = this.PointToPosition(point, _mmRectangle);
					CameraPlacement newCameraPlacement = new CameraPlacement(mapPosition, this.cameraPlacement.Rotation);
					this.context.EditorScene.SetPlacement(this.context.EditorSceneViewID, ref newCameraPlacement);
				}
			}

			// Token: 0x06001174 RID: 4468 RVA: 0x00081AC8 File Offset: 0x00080AC8
			public bool CameraPositionWasChanged()
			{
				if (this.context.EditorScene != null)
				{
					CameraPlacement newCameraPlacement;
					this.context.EditorScene.GetPlacement(this.context.EditorSceneViewID, out newCameraPlacement);
					if (!MinimapForm.Camera.CPlacementAreEquals(newCameraPlacement, this.cameraPlacement))
					{
						this.cameraPlacement = newCameraPlacement;
						return true;
					}
				}
				return false;
			}

			// Token: 0x1700037D RID: 893
			// (get) Token: 0x06001175 RID: 4469 RVA: 0x00081B18 File Offset: 0x00080B18
			// (set) Token: 0x06001176 RID: 4470 RVA: 0x00081B20 File Offset: 0x00080B20
			public int TerrainIndex
			{
				get
				{
					return this.terrainIndex;
				}
				set
				{
					this.terrainIndex = value;
				}
			}

			// Token: 0x04000C84 RID: 3204
			private readonly MainForm.Context context;

			// Token: 0x04000C85 RID: 3205
			private Tools.Geometry.Point minXMinYPatchCoords = default(Tools.Geometry.Point);

			// Token: 0x04000C86 RID: 3206
			private CameraPlacement cameraPlacement;

			// Token: 0x04000C87 RID: 3207
			private int terrainIndex;

			// Token: 0x04000C88 RID: 3208
			private readonly Tools.Geometry.Point mapSize;
		}
	}
}
