using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using MapEditor.Map.DataProviders;
using MapEditor.Map.MapObjects;
using MapEditor.Scene;
using Tools.Geometry;
using Tools.Landscape;
using Tools.Landscape.LandscapeToolParams;
using Tools.LinkContainer;
using Tools.MapObjects;

namespace MapEditor.Map.Landscape
{
	// Token: 0x02000077 RID: 119
	public class ClipboardLandscapeTool : LandscapeTool
	{
		// Token: 0x060005C0 RID: 1472 RVA: 0x0002FFB0 File Offset: 0x0002EFB0
		private void SetClipboardParams()
		{
			if (this.editorSceneLandscapeToolID != -1)
			{
				ClipboardLandscapeToolParams clipboardLandscapeToolParams = base.LandscapeToolParams as ClipboardLandscapeToolParams;
				if (clipboardLandscapeToolParams != null && base.LandscapeRegion != null)
				{
					base.LandscapeToolContext.EditorScene.SetUndoPrevious_ClipboardLandscapeTool(this.editorSceneLandscapeToolID, true);
					base.LandscapeToolContext.EditorScene.SetPrecise_ClipboardLandscapeTool(this.editorSceneLandscapeToolID, clipboardLandscapeToolParams.PreciseTool);
					base.LandscapeToolContext.EditorScene.SetHeightType_ClipboardLandscapeTool(this.editorSceneLandscapeToolID, clipboardLandscapeToolParams.GetCopyHeightTypeIndex());
					base.LandscapeToolContext.EditorScene.SetPasteParts_ClipboardLandscapeTool(this.editorSceneLandscapeToolID, clipboardLandscapeToolParams.CopyTiles, clipboardLandscapeToolParams.CopyHeights);
				}
			}
		}

		// Token: 0x060005C1 RID: 1473 RVA: 0x00030054 File Offset: 0x0002F054
		private void SetClipboardParams(ref Position position, ref Rotation rotation, ref Scale scale, double additionalHeight, bool setRegionParams)
		{
			if (this.editorSceneLandscapeToolID != -1)
			{
				ClipboardLandscapeToolParams clipboardLandscapeToolParams = base.LandscapeToolParams as ClipboardLandscapeToolParams;
				if (clipboardLandscapeToolParams != null && base.LandscapeRegion != null)
				{
					Point _position = base.LandscapeToolContext.GetTerrainPosition(ref position);
					base.LandscapeToolContext.EditorScene.SetPosition_ClipboardLandscapeTool(this.editorSceneLandscapeToolID, ref _position);
					if ((double)Math.Abs(rotation.Yaw) > MathConsts.DOUBLE_EPSILON)
					{
						base.LandscapeToolContext.EditorScene.SetAngle_ClipboardLandscapeTool(this.editorSceneLandscapeToolID, (double)rotation.Yaw);
					}
					else
					{
						base.LandscapeToolContext.EditorScene.SetAngle_ClipboardLandscapeTool(this.editorSceneLandscapeToolID, 0.0);
					}
					if (setRegionParams)
					{
						base.LandscapeToolContext.EditorScene.SetLandscapeRegionParams_ClipboardLandscapeTool(this.editorSceneLandscapeToolID, ref scale, clipboardLandscapeToolParams.StrengthSmoothParams.Smooth);
					}
					base.LandscapeToolContext.EditorScene.SetAdditionalHeight_ClipboardLandscapeTool(this.editorSceneLandscapeToolID, additionalHeight);
					base.LandscapeToolContext.EditorScene.SetUndoPrevious_ClipboardLandscapeTool(this.editorSceneLandscapeToolID, true);
					base.LandscapeToolContext.EditorScene.SetOperation_ClipboardLandscapeTool(this.editorSceneLandscapeToolID, (((double)Math.Abs(rotation.Yaw) > MathConsts.DOUBLE_EPSILON) ? 1 : 0) + (clipboardLandscapeToolParams.FlipVertical ? 2 : 0) + (clipboardLandscapeToolParams.FlipHorisontal ? 4 : 0));
					base.LandscapeToolContext.EditorScene.SetPrecise_ClipboardLandscapeTool(this.editorSceneLandscapeToolID, clipboardLandscapeToolParams.PreciseTool);
					base.LandscapeToolContext.EditorScene.SetHeightType_ClipboardLandscapeTool(this.editorSceneLandscapeToolID, clipboardLandscapeToolParams.GetCopyHeightTypeIndex());
					base.LandscapeToolContext.EditorScene.SetPasteParts_ClipboardLandscapeTool(this.editorSceneLandscapeToolID, clipboardLandscapeToolParams.CopyTiles, clipboardLandscapeToolParams.CopyHeights);
				}
			}
		}

		// Token: 0x060005C2 RID: 1474 RVA: 0x000301F6 File Offset: 0x0002F1F6
		public ClipboardLandscapeTool(int _id) : base(_id)
		{
		}

		// Token: 0x060005C3 RID: 1475 RVA: 0x00030228 File Offset: 0x0002F228
		public override void Create()
		{
			this.Destroy();
			ClipboardLandscapeToolParams clipboardLandscapeToolParams = base.LandscapeToolParams as ClipboardLandscapeToolParams;
			if (clipboardLandscapeToolParams != null)
			{
				this.editorSceneLandscapeToolID = base.LandscapeToolContext.EditorScene.CreateClipboardLandscapeTool();
				this.Load();
			}
		}

		// Token: 0x060005C4 RID: 1476 RVA: 0x00030268 File Offset: 0x0002F268
		public override void Apply()
		{
			if (this.editorSceneLandscapeToolID != -1)
			{
				Rect affectedRect;
				base.LandscapeToolContext.EditorScene.Paste_ClipboardLandscapeTool(this.editorSceneLandscapeToolID, out affectedRect);
				base.AffectParams.AffectedRect = affectedRect;
				if (this.clipboard != null)
				{
					this.clipboard.UpdateVisibleMapObjects();
				}
			}
		}

		// Token: 0x060005C5 RID: 1477 RVA: 0x000302B5 File Offset: 0x0002F2B5
		public override void Destroy()
		{
			if (this.editorSceneLandscapeToolID != -1)
			{
				base.LandscapeToolContext.EditorScene.DeleteLandscapeTool(this.editorSceneLandscapeToolID);
				this.editorSceneLandscapeToolID = -1;
			}
		}

		// Token: 0x060005C6 RID: 1478 RVA: 0x000302E0 File Offset: 0x0002F2E0
		public bool Copy(ref Point position, ClipboardOperationType clipboardOperationType, Form parentForm, out Position regionPosition)
		{
			regionPosition = Position.Empty;
			if (this.editorSceneLandscapeToolID != -1)
			{
				string fileName = LandscapeClipboardItemSource.Folder + ClipboardLandscapeTool.defaultClipboardFileName;
				if (clipboardOperationType == ClipboardOperationType.CopySpecial)
				{
					SaveFileDialog saveFileDialog = new SaveFileDialog();
					saveFileDialog.Filter = "Clipboard Files|*.bin|All Files|*.*";
					saveFileDialog.RestoreDirectory = true;
					saveFileDialog.InitialDirectory = (EditorEnvironment.EditorFolder + LandscapeClipboardItemSource.Folder).Replace('/', '\\');
					if (!Directory.Exists(saveFileDialog.InitialDirectory))
					{
						Directory.CreateDirectory(saveFileDialog.InitialDirectory);
					}
					if (saveFileDialog.ShowDialog(parentForm) != DialogResult.OK)
					{
						return false;
					}
					fileName = saveFileDialog.FileName.Replace('\\', '/');
					if (fileName.Length > EditorEnvironment.EditorFolder.Length)
					{
						fileName = fileName.Substring(EditorEnvironment.EditorFolder.Length);
					}
				}
				Vec3 mapOffset = base.LandscapeToolContext.MapOffset;
				Vec2 _regionCenter = Vec2.Empty;
				ClipboardLandscapeToolParams clipboardLandscapeToolParams = base.LandscapeToolParams as ClipboardLandscapeToolParams;
				if (clipboardLandscapeToolParams != null && base.LandscapeRegion != null)
				{
					this.copiedMapObjects.Clear();
					Position regionCenter = base.LandscapeToolContext.LandscapeToolContextPosition.CollisionMapPosition;
					if (base.LandscapeRegion.LandscapeRegionParams.Type == LandscapeRegionType.Ellipse || base.LandscapeRegion.LandscapeRegionParams.Type == LandscapeRegionType.Square)
					{
						bool ellipse = base.LandscapeRegion.LandscapeRegionParams.Type == LandscapeRegionType.Ellipse;
						Point size = new Point(base.LandscapeRegion.LandscapeRegionParams.Size, base.LandscapeRegion.LandscapeRegionParams.Size);
						double halfSize = (double)base.LandscapeRegion.LandscapeRegionParams.Size / 2.0;
						base.LandscapeToolContext.EditorScene.SetStartLandscapeRegion_ClipboardLandscapeTool(this.editorSceneLandscapeToolID, ref size, ellipse, clipboardLandscapeToolParams.StrengthSmoothParams.Smooth);
						if (clipboardOperationType == ClipboardOperationType.Cut)
						{
							this.SetClipboardParams();
							base.LandscapeToolContext.EditorScene.Cut_ClipboardLandscapeTool(this.editorSceneLandscapeToolID, ref position, clipboardLandscapeToolParams.TwoSided, ref _regionCenter);
						}
						else
						{
							base.LandscapeToolContext.EditorScene.Copy_ClipboardLandscapeTool(this.editorSceneLandscapeToolID, ref position, clipboardLandscapeToolParams.TwoSided, ref _regionCenter);
						}
						regionCenter = new Position(_regionCenter.X + mapOffset.X, _regionCenter.Y + mapOffset.Y, base.LandscapeToolContext.CollisionMap.GetHeight(_regionCenter.X + mapOffset.X, _regionCenter.Y + mapOffset.Y, TerrainSurface.Terrain));
						using (Dictionary<int, IMapObject>.Enumerator enumerator = base.LandscapeToolContext.MapObjectContainer.MapObjects.GetEnumerator())
						{
							while (enumerator.MoveNext())
							{
								KeyValuePair<int, IMapObject> keyValuePair = enumerator.Current;
								if (keyValuePair.Value != null && !keyValuePair.Value.Temporary)
								{
									if (ellipse)
									{
										if (Vec2.Abs(keyValuePair.Value.Position.X - regionCenter.X, keyValuePair.Value.Position.Y - regionCenter.Y) <= halfSize)
										{
											this.copiedMapObjects.Add(keyValuePair.Value);
										}
									}
									else if (keyValuePair.Value.Position.X >= regionCenter.X - halfSize && keyValuePair.Value.Position.X <= regionCenter.X + halfSize && keyValuePair.Value.Position.Y >= regionCenter.Y - halfSize && keyValuePair.Value.Position.Y <= regionCenter.Y + halfSize)
									{
										this.copiedMapObjects.Add(keyValuePair.Value);
									}
								}
							}
							goto IL_6CE;
						}
					}
					if (base.LandscapeRegion.LandscapeRegionParams.Type == LandscapeRegionType.Polygon)
					{
						base.LandscapeToolContext.EditorScene.SetStartLandscapeRegion_ClipboardLandscapeTool(this.editorSceneLandscapeToolID, ref mapOffset, base.LandscapeRegion.Polygon, clipboardLandscapeToolParams.StrengthSmoothParams.Smooth);
						if (clipboardOperationType == ClipboardOperationType.Cut)
						{
							this.SetClipboardParams();
							base.LandscapeToolContext.EditorScene.Cut_ClipboardLandscapeTool(this.editorSceneLandscapeToolID, clipboardLandscapeToolParams.TwoSided, ref _regionCenter);
						}
						else
						{
							base.LandscapeToolContext.EditorScene.Copy_ClipboardLandscapeTool(this.editorSceneLandscapeToolID, clipboardLandscapeToolParams.TwoSided, ref _regionCenter);
						}
						regionCenter = new Position(_regionCenter.X + mapOffset.X, _regionCenter.Y + mapOffset.Y, base.LandscapeToolContext.CollisionMap.GetHeight(_regionCenter.X + mapOffset.X, _regionCenter.Y + mapOffset.Y, TerrainSurface.Terrain));
						using (Dictionary<int, IMapObject>.Enumerator enumerator2 = base.LandscapeToolContext.MapObjectContainer.MapObjects.GetEnumerator())
						{
							while (enumerator2.MoveNext())
							{
								KeyValuePair<int, IMapObject> keyValuePair2 = enumerator2.Current;
								if (keyValuePair2.Value != null && !keyValuePair2.Value.Temporary)
								{
									Vec3 point = keyValuePair2.Value.Position.Vec3;
									PerimeterArea perimeterArea = base.LandscapeRegion.Polygon.ClassifyPoint(ref point);
									if (Geometry.IsInside(perimeterArea))
									{
										this.copiedMapObjects.Add(keyValuePair2.Value);
									}
								}
							}
							goto IL_6CE;
						}
					}
					if (base.LandscapeRegion.LandscapeRegionParams.Type == LandscapeRegionType.Stripe)
					{
						base.LandscapeToolContext.EditorScene.SetStartLandscapeRegion_ClipboardLandscapeTool(this.editorSceneLandscapeToolID, ref mapOffset, base.LandscapeRegion.Stripe.Bounds, clipboardLandscapeToolParams.StrengthSmoothParams.Smooth);
						if (clipboardOperationType == ClipboardOperationType.Cut)
						{
							this.SetClipboardParams();
							base.LandscapeToolContext.EditorScene.Cut_ClipboardLandscapeTool(this.editorSceneLandscapeToolID, clipboardLandscapeToolParams.TwoSided, ref _regionCenter);
						}
						else
						{
							base.LandscapeToolContext.EditorScene.Copy_ClipboardLandscapeTool(this.editorSceneLandscapeToolID, clipboardLandscapeToolParams.TwoSided, ref _regionCenter);
						}
						regionCenter = new Position(_regionCenter.X + mapOffset.X, _regionCenter.Y + mapOffset.Y, base.LandscapeToolContext.CollisionMap.GetHeight(_regionCenter.X + mapOffset.X, _regionCenter.Y + mapOffset.Y, TerrainSurface.Terrain));
						using (Dictionary<int, IMapObject>.Enumerator enumerator3 = base.LandscapeToolContext.MapObjectContainer.MapObjects.GetEnumerator())
						{
							while (enumerator3.MoveNext())
							{
								KeyValuePair<int, IMapObject> keyValuePair3 = enumerator3.Current;
								if (keyValuePair3.Value != null && !keyValuePair3.Value.Temporary)
								{
									Vec3 point2 = keyValuePair3.Value.Position.Vec3;
									PerimeterArea perimeterArea2 = base.LandscapeRegion.Stripe.Bounds.ClassifyPoint(ref point2);
									if (Geometry.IsInside(perimeterArea2))
									{
										this.copiedMapObjects.Add(keyValuePair3.Value);
									}
								}
							}
							goto IL_6CE;
						}
					}
					if (base.LandscapeRegion.LandscapeRegionParams.Type == LandscapeRegionType.AllMap)
					{
						fileName = string.Empty;
					}
					IL_6CE:
					regionPosition = regionCenter;
					if (!string.IsNullOrEmpty(fileName))
					{
						base.LandscapeToolContext.EditorScene.Save_ClipboardLandscapeTool(this.editorSceneLandscapeToolID, EditorEnvironment.EditorFolder + fileName);
						MapObjectContainer.Pack(base.LandscapeToolContext.MapObjectContainer, this.copiedMapObjects, true, this.multiObjectSaverMapObjects, this.multiObjectSaverLinks);
						regionCenter = new Position((double)((int)(regionCenter.X + 0.5)) * 1.0, (double)((int)(regionCenter.Y + 0.5)) * 1.0, regionCenter.Z);
						foreach (IMapObject mapObject in this.multiObjectSaverMapObjects)
						{
							double altitude = mapObject.Altitude;
							mapObject.Position -= regionCenter;
							mapObject.Altitude = altitude;
						}
						MultiObjectSaver multiObjectSaver = new MultiObjectSaver();
						multiObjectSaver.Pack(this.multiObjectSaverMapObjects, this.multiObjectSaverLinks, true, true);
						string multiObjectSaverFileName = Str.CutFileExtention(fileName);
						multiObjectSaverFileName = Str.ExtendFileExtention(multiObjectSaverFileName, ".xml");
						MultiObjectSaver.Save(multiObjectSaver, EditorEnvironment.EditorFolder + multiObjectSaverFileName, true);
						this.saveInPorogress = true;
						clipboardLandscapeToolParams.FileName = fileName;
						this.saveInPorogress = false;
					}
					if (clipboardOperationType != ClipboardOperationType.Cut)
					{
						this.copiedMapObjects.Clear();
					}
					return true;
				}
			}
			return false;
		}

		// Token: 0x060005C7 RID: 1479 RVA: 0x00030B88 File Offset: 0x0002FB88
		public bool Load()
		{
			if (!this.saveInPorogress && this.editorSceneLandscapeToolID != -1)
			{
				ClipboardLandscapeToolParams clipboardLandscapeToolParams = base.LandscapeToolParams as ClipboardLandscapeToolParams;
				if (clipboardLandscapeToolParams != null && !string.IsNullOrEmpty(clipboardLandscapeToolParams.FileName))
				{
					string filePath = EditorEnvironment.EditorFolder + clipboardLandscapeToolParams.FileName;
					if (File.Exists(filePath))
					{
						base.LandscapeToolContext.EditorScene.Load_ClipboardLandscapeTool(this.editorSceneLandscapeToolID, filePath);
						string multiObjectSaverFileName = Str.CutFileExtention(clipboardLandscapeToolParams.FileName);
						multiObjectSaverFileName = Str.ExtendFileExtention(multiObjectSaverFileName, ".xml");
						MultiObjectSaver multiObjectSaver = MultiObjectSaver.Load(EditorEnvironment.EditorFolder + multiObjectSaverFileName);
						if (multiObjectSaver != null)
						{
							multiObjectSaver.Unpack(this.multiObjectSaverMapObjects, this.multiObjectSaverLinks, base.LandscapeToolContext.MapObjectContainer, false, true, true);
						}
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x060005C8 RID: 1480 RVA: 0x00030C4C File Offset: 0x0002FC4C
		public bool BeginPaste(ref Position position, ref Rotation rotation, ref Scale scale, double additionalHeight, ClipboardOperationType clipboardOperationType)
		{
			if (this.editorSceneLandscapeToolID != -1)
			{
				ClipboardLandscapeToolParams clipboardLandscapeToolParams = base.LandscapeToolParams as ClipboardLandscapeToolParams;
				if (clipboardLandscapeToolParams != null && !string.IsNullOrEmpty(clipboardLandscapeToolParams.FileName))
				{
					this.SetClipboardParams(ref position, ref rotation, ref scale, additionalHeight, true);
					if (clipboardLandscapeToolParams.CopyObjects && this.multiObjectSaverMapObjects.Count > 0)
					{
						if (this.copiedMapObjects.Count != 0)
						{
							this.clipboard = new MapObjectLandscapeClipboard(base.LandscapeToolContext.MapObjectContainer, this.copiedMapObjects, null, true);
						}
						else
						{
							this.clipboard = new MapObjectLandscapeClipboard(base.LandscapeToolContext.MapObjectContainer, this.multiObjectSaverMapObjects, this.multiObjectSaverLinks, false);
						}
						Point _regionCenter = base.LandscapeToolContext.GetTerrainPosition(ref position);
						Position regionCenter = new Position((double)_regionCenter.X + base.LandscapeToolContext.MapOffset.X, (double)_regionCenter.Y + base.LandscapeToolContext.MapOffset.Y, base.LandscapeToolContext.CollisionMap.GetHeight((double)_regionCenter.X + base.LandscapeToolContext.MapOffset.X, (double)_regionCenter.Y + base.LandscapeToolContext.MapOffset.Y, TerrainSurface.Terrain));
						this.clipboard.SetClipboardParams(ref regionCenter, ref rotation, ref scale, false);
						this.clipboard.Visible = true;
					}
					return true;
				}
			}
			return false;
		}

		// Token: 0x060005C9 RID: 1481 RVA: 0x00030DBC File Offset: 0x0002FDBC
		public void ContinuePaste(ref Position position, ref Rotation rotation, ref Scale scale, double additionalHeight, ClipboardOperationType clipboardOperationType, bool setRegionParams)
		{
			if (this.editorSceneLandscapeToolID != -1)
			{
				this.SetClipboardParams(ref position, ref rotation, ref scale, additionalHeight, setRegionParams);
				if (this.clipboard != null)
				{
					Point _regionCenter = base.LandscapeToolContext.GetTerrainPosition(ref position);
					Position regionCenter = new Position((double)_regionCenter.X + base.LandscapeToolContext.MapOffset.X, (double)_regionCenter.Y + base.LandscapeToolContext.MapOffset.Y, base.LandscapeToolContext.CollisionMap.GetHeight((double)_regionCenter.X + base.LandscapeToolContext.MapOffset.X, (double)_regionCenter.Y + base.LandscapeToolContext.MapOffset.Y, TerrainSurface.Terrain));
					this.clipboard.SetClipboardParams(ref regionCenter, ref rotation, ref scale, false);
				}
			}
		}

		// Token: 0x060005CA RID: 1482 RVA: 0x00030E9C File Offset: 0x0002FE9C
		public void FinishPaste(ref Position position, ref Rotation rotation, ref Scale scale, double additionalHeight, ClipboardOperationType clipboardOperationType, bool applyChanges)
		{
			if (this.editorSceneLandscapeToolID != -1)
			{
				if (applyChanges)
				{
					base.LandscapeToolContext.EditorScene.Finish_ClipboardLandscapeTool(this.editorSceneLandscapeToolID);
					if (this.clipboard != null)
					{
						ClipboardLandscapeToolParams clipboardLandscapeToolParams = base.LandscapeToolParams as ClipboardLandscapeToolParams;
						if (clipboardLandscapeToolParams != null && base.LandscapeRegion != null)
						{
							this.clipboard.Paste(null, clipboardLandscapeToolParams.Group);
						}
						this.clipboard.Visible = false;
						this.clipboard = null;
					}
				}
				else
				{
					base.LandscapeToolContext.EditorScene.Break_ClipboardLandscapeTool(this.editorSceneLandscapeToolID);
					if (this.clipboard != null)
					{
						this.clipboard.Visible = false;
						this.clipboard = null;
					}
				}
				this.copiedMapObjects.Clear();
			}
		}

		// Token: 0x060005CB RID: 1483 RVA: 0x00030F51 File Offset: 0x0002FF51
		public bool CopyAvailable(ClipboardOperationType clipboardOperationType)
		{
			return this.editorSceneLandscapeToolID != -1;
		}

		// Token: 0x060005CC RID: 1484 RVA: 0x00030F5F File Offset: 0x0002FF5F
		public bool PasteAvailable(ClipboardOperationType clipboardOperationType)
		{
			return this.editorSceneLandscapeToolID != -1 && base.LandscapeToolContext.EditorScene.PasteAvailable_ClipboardLandscapeTool(this.editorSceneLandscapeToolID);
		}

		// Token: 0x060005CD RID: 1485 RVA: 0x00030F82 File Offset: 0x0002FF82
		public double GetAverageHeight()
		{
			if (this.editorSceneLandscapeToolID != -1)
			{
				return base.LandscapeToolContext.EditorScene.GetAverageHeight_ClipboardLandscapeTool(this.editorSceneLandscapeToolID);
			}
			return 0.0;
		}

		// Token: 0x04000458 RID: 1112
		private static readonly string defaultClipboardFileName = "Default.bin";

		// Token: 0x04000459 RID: 1113
		private bool saveInPorogress;

		// Token: 0x0400045A RID: 1114
		private int editorSceneLandscapeToolID = -1;

		// Token: 0x0400045B RID: 1115
		private readonly List<IMapObject> multiObjectSaverMapObjects = new List<IMapObject>();

		// Token: 0x0400045C RID: 1116
		private readonly Dictionary<int, Dictionary<int, ILinkData>> multiObjectSaverLinks = new Dictionary<int, Dictionary<int, ILinkData>>();

		// Token: 0x0400045D RID: 1117
		private readonly List<IMapObject> copiedMapObjects = new List<IMapObject>();

		// Token: 0x0400045E RID: 1118
		private MapObjectLandscapeClipboard clipboard;
	}
}
