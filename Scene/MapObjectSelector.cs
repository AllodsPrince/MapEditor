using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using System.Xml.Serialization;
using InputState;
using LauncherTools.InputState;
using MapEditor.Forms.GridStep;
using MapEditor.Map.MapObjects;
using MapEditor.Map.States;
using Operations;
using Tools.Geometry;
using Tools.Groups;
using Tools.LinkContainer;
using Tools.MapObjects;

namespace MapEditor.Scene
{
	// Token: 0x02000193 RID: 403
	public class MapObjectSelector
	{
		// Token: 0x14000076 RID: 118
		// (add) Token: 0x06001334 RID: 4916 RVA: 0x0008D6AA File Offset: 0x0008C6AA
		// (remove) Token: 0x06001335 RID: 4917 RVA: 0x0008D6C3 File Offset: 0x0008C6C3
		public event MapObjectSelector.SelectorChangedEvent SelectionChanged;

		// Token: 0x14000077 RID: 119
		// (add) Token: 0x06001336 RID: 4918 RVA: 0x0008D6DC File Offset: 0x0008C6DC
		// (remove) Token: 0x06001337 RID: 4919 RVA: 0x0008D6F5 File Offset: 0x0008C6F5
		public event MapObjectSelector.SelectorFieldChangedEvent<bool> ObjectOrientedChanged;

		// Token: 0x14000078 RID: 120
		// (add) Token: 0x06001338 RID: 4920 RVA: 0x0008D70E File Offset: 0x0008C70E
		// (remove) Token: 0x06001339 RID: 4921 RVA: 0x0008D727 File Offset: 0x0008C727
		public event MapObjectSelector.SelectorFieldChangedEvent<TerrainSurface> SurfaceChanged;

		// Token: 0x14000079 RID: 121
		// (add) Token: 0x0600133A RID: 4922 RVA: 0x0008D740 File Offset: 0x0008C740
		// (remove) Token: 0x0600133B RID: 4923 RVA: 0x0008D759 File Offset: 0x0008C759
		public event MapObjectSelector.SelectorFieldChangedEvent<bool> AlignToGridChanged;

		// Token: 0x1400007A RID: 122
		// (add) Token: 0x0600133C RID: 4924 RVA: 0x0008D772 File Offset: 0x0008C772
		// (remove) Token: 0x0600133D RID: 4925 RVA: 0x0008D78B File Offset: 0x0008C78B
		public event MapObjectSelector.SelectorFieldChangedEvent<bool> PlaceAlongNormalChanged;

		// Token: 0x1400007B RID: 123
		// (add) Token: 0x0600133E RID: 4926 RVA: 0x0008D7A4 File Offset: 0x0008C7A4
		// (remove) Token: 0x0600133F RID: 4927 RVA: 0x0008D7BD File Offset: 0x0008C7BD
		public event MapObjectSelector.SelectorFieldChangedEvent<bool> LockSelectionChanged;

		// Token: 0x1400007C RID: 124
		// (add) Token: 0x06001340 RID: 4928 RVA: 0x0008D7D6 File Offset: 0x0008C7D6
		// (remove) Token: 0x06001341 RID: 4929 RVA: 0x0008D7EF File Offset: 0x0008C7EF
		public event MapObjectSelector.SelectorFieldChangedEvent<bool> EditObjectAfterAddChanged;

		// Token: 0x1400007D RID: 125
		// (add) Token: 0x06001342 RID: 4930 RVA: 0x0008D808 File Offset: 0x0008C808
		// (remove) Token: 0x06001343 RID: 4931 RVA: 0x0008D821 File Offset: 0x0008C821
		public event MapObjectSelector.SelectorFieldChangedEvent<bool> AutolinkObjectAfterAddChanged;

		// Token: 0x1400007E RID: 126
		// (add) Token: 0x06001344 RID: 4932 RVA: 0x0008D83A File Offset: 0x0008C83A
		// (remove) Token: 0x06001345 RID: 4933 RVA: 0x0008D853 File Offset: 0x0008C853
		public event MapObjectSelector.SelectorFieldChangedEvent<bool> CreateCrosslinksChanged;

		// Token: 0x1400007F RID: 127
		// (add) Token: 0x06001346 RID: 4934 RVA: 0x0008D86C File Offset: 0x0008C86C
		// (remove) Token: 0x06001347 RID: 4935 RVA: 0x0008D885 File Offset: 0x0008C885
		public event MapObjectSelector.SelectorFieldChangedEvent<int> GridStepIndexChanged;

		// Token: 0x06001348 RID: 4936 RVA: 0x0008D8A0 File Offset: 0x0008C8A0
		private static void InternalArrandeLinkedObject(IMapObject mapObject, IMapObject additionalMapObject, MapObjectContainer _mapObjectContainer)
		{
			if (mapObject != null && mapObject.Type.Type == MapObjectFactory.Type.RoutePoint)
			{
				RoutePoint middle = mapObject as RoutePoint;
				if (middle != null)
				{
					Dictionary<IMapObject, ILinkData> links = _mapObjectContainer.GetLinks(middle);
					if (links != null && links.Count > 0)
					{
						int linksCount = 0;
						foreach (KeyValuePair<IMapObject, ILinkData> keyValuePair in links)
						{
							if (keyValuePair.Key.Type.Type == MapObjectFactory.Type.RoutePoint)
							{
								linksCount++;
							}
						}
						if (linksCount > 0)
						{
							RoutePoint left = MapObjectContainer.GetOtherLinkedMapObject(links, MapObjectFactory.Type.RoutePoint, null, false) as RoutePoint;
							RoutePoint right = MapObjectContainer.GetOtherLinkedMapObject(links, MapObjectFactory.Type.RoutePoint, left, false) as RoutePoint;
							if (left != null || right != null)
							{
								if (left != null && right != null)
								{
									if (!RoutePoint.RouteAdvanceIsDirect(left, middle))
									{
										RoutePoint cache = right;
										right = left;
										left = cache;
									}
								}
								else if (left != null)
								{
									if (RoutePoint.RouteAdvanceIsDirect(left, middle))
									{
										right = middle;
									}
									else
									{
										right = left;
										left = middle;
									}
								}
								else if (RoutePoint.RouteAdvanceIsDirect(middle, right))
								{
									left = middle;
								}
								else
								{
									left = right;
									right = middle;
								}
								Vec3 tangent = (right.Position - left.Position).Vec3;
								tangent.Normalize();
								Quat quat = new Quat(tangent);
								Rotation _rotation = new Rotation(ref quat);
								mapObject.Rotation = _rotation;
								if (additionalMapObject != null)
								{
									additionalMapObject.Rotation = _rotation;
								}
							}
						}
					}
				}
			}
		}

		// Token: 0x06001349 RID: 4937 RVA: 0x0008DA30 File Offset: 0x0008CA30
		private void CreateSelectorData()
		{
			MapObjectSelector.SelectorData _selectorData = Serializer.Load<MapObjectSelector.SelectorData>(EditorEnvironment.EditorFormsFolder + this.selectorDataFileName);
			if (_selectorData != null)
			{
				this.selectorData = _selectorData;
			}
			else
			{
				this.selectorData = new MapObjectSelector.SelectorData();
			}
			this.selectorData.SelectorTypeChanged += this.OnSelectorTypeChanged;
			this.selectorData.ObjectOrientedChanged += this.OnObjectOrientedChanged;
			this.selectorData.SurfaceChanged += this.OnSurfaceChanged;
			this.selectorData.AlignToGridChanged += this.OnAlignToGridChanged;
			this.selectorData.PlaceAlongNormalChanged += this.OnPlaceAlongNormalChanged;
			this.selectorData.LockSelectionChanged += this.OnLockSelectionChanged;
			this.selectorData.EditObjectAfterAddChanged += this.OnEditObjectAfterAddChanged;
			this.selectorData.AutolinkObjectAfterAddChanged += this.OnAutolinkObjectAfterAddChanged;
			this.selectorData.CreateCrosslinksChanged += this.OnCreateCrosslinksChanged;
			this.selectorData.GridStepIndexChanged += this.OnGridStepIndexChanged;
			this.selectorData.Active = true;
		}

		// Token: 0x0600134A RID: 4938 RVA: 0x0008DB5C File Offset: 0x0008CB5C
		private void DestroySelectorData()
		{
			if (this.selectorData != null)
			{
				Serializer.Save(EditorEnvironment.EditorFormsFolder + this.selectorDataFileName, this.selectorData, false);
				this.selectorData.SelectorTypeChanged -= this.OnSelectorTypeChanged;
				this.selectorData.ObjectOrientedChanged -= this.OnObjectOrientedChanged;
				this.selectorData.SurfaceChanged -= this.OnSurfaceChanged;
				this.selectorData.AlignToGridChanged -= this.OnAlignToGridChanged;
				this.selectorData.PlaceAlongNormalChanged -= this.OnPlaceAlongNormalChanged;
				this.selectorData.LockSelectionChanged -= this.OnLockSelectionChanged;
				this.selectorData.EditObjectAfterAddChanged -= this.OnEditObjectAfterAddChanged;
				this.selectorData.AutolinkObjectAfterAddChanged -= this.OnAutolinkObjectAfterAddChanged;
				this.selectorData.CreateCrosslinksChanged -= this.OnCreateCrosslinksChanged;
				this.selectorData.GridStepIndexChanged -= this.OnGridStepIndexChanged;
				this.selectorData = null;
			}
		}

		// Token: 0x0600134B RID: 4939 RVA: 0x0008DC7E File Offset: 0x0008CC7E
		private void OnSelectorMove(MethodArgs methodArgs)
		{
			if (this.selectorData != null)
			{
				this.selectorData.SelectorType = MapObjectSelector.SelectorType.Move;
			}
		}

		// Token: 0x0600134C RID: 4940 RVA: 0x0008DC94 File Offset: 0x0008CC94
		private void OnSelectorRotate(MethodArgs methodArgs)
		{
			if (this.selectorData != null)
			{
				this.selectorData.SelectorType = MapObjectSelector.SelectorType.Rotate;
			}
		}

		// Token: 0x0600134D RID: 4941 RVA: 0x0008DCAA File Offset: 0x0008CCAA
		private void OnSelectorScale(MethodArgs methodArgs)
		{
			if (this.selectorData != null)
			{
				this.selectorData.SelectorType = MapObjectSelector.SelectorType.Scale;
			}
		}

		// Token: 0x0600134E RID: 4942 RVA: 0x0008DCC0 File Offset: 0x0008CCC0
		private void OnSelectorAlongTerrain(MethodArgs methodArgs)
		{
			if (this.selectorData != null)
			{
				if (this.selectorData.Surface == TerrainSurface.Terrain)
				{
					this.selectorData.Surface = TerrainSurface.None;
					return;
				}
				this.selectorData.Surface = TerrainSurface.Terrain;
			}
		}

		// Token: 0x0600134F RID: 4943 RVA: 0x0008DCF1 File Offset: 0x0008CCF1
		private void OnSelectorAlongWater(MethodArgs methodArgs)
		{
			if (this.selectorData != null)
			{
				if (this.selectorData.Surface == TerrainSurface.Water)
				{
					this.selectorData.Surface = TerrainSurface.None;
					return;
				}
				this.selectorData.Surface = TerrainSurface.Water;
			}
		}

		// Token: 0x06001350 RID: 4944 RVA: 0x0008DD22 File Offset: 0x0008CD22
		private void OnSelectorAlongObject(MethodArgs methodArgs)
		{
			if (this.selectorData != null)
			{
				if (this.selectorData.Surface == TerrainSurface.Object)
				{
					this.selectorData.Surface = TerrainSurface.None;
					return;
				}
				this.selectorData.Surface = TerrainSurface.Object;
			}
		}

		// Token: 0x06001351 RID: 4945 RVA: 0x0008DD53 File Offset: 0x0008CD53
		private void OnSelectorAlignToGrid(MethodArgs methodArgs)
		{
			if (this.selectorData != null)
			{
				this.selectorData.AlignToGrid = !this.selectorData.AlignToGrid;
			}
		}

		// Token: 0x06001352 RID: 4946 RVA: 0x0008DD76 File Offset: 0x0008CD76
		private void OnSelectorPlaceAlongNormal(MethodArgs methodArgs)
		{
			if (this.selectorData != null)
			{
				this.selectorData.PlaceAlongNormal = !this.selectorData.PlaceAlongNormal;
			}
		}

		// Token: 0x06001353 RID: 4947 RVA: 0x0008DD99 File Offset: 0x0008CD99
		private void OnSelectorLockSelection(MethodArgs methodArgs)
		{
			if (this.selectorData != null)
			{
				this.selectorData.LockSelection = !this.selectorData.LockSelection;
			}
		}

		// Token: 0x06001354 RID: 4948 RVA: 0x0008DDBC File Offset: 0x0008CDBC
		private void OnSelectorEditObjectAfterAdd(MethodArgs methodArgs)
		{
			if (this.selectorData != null)
			{
				this.selectorData.EditObjectAfterAdd = !this.selectorData.EditObjectAfterAdd;
			}
		}

		// Token: 0x06001355 RID: 4949 RVA: 0x0008DDDF File Offset: 0x0008CDDF
		private void OnSelectorAutolinkObjectAfterAdd(MethodArgs methodArgs)
		{
			if (this.selectorData != null)
			{
				this.selectorData.AutolinkObjectAfterAdd = !this.selectorData.AutolinkObjectAfterAdd;
			}
		}

		// Token: 0x06001356 RID: 4950 RVA: 0x0008DE02 File Offset: 0x0008CE02
		private void OnSelectorCreateCrosslinks(MethodArgs methodArgs)
		{
			if (this.selectorData != null)
			{
				this.selectorData.CreateCrosslinks = !this.selectorData.CreateCrosslinks;
			}
		}

		// Token: 0x06001357 RID: 4951 RVA: 0x0008DE25 File Offset: 0x0008CE25
		private void OnGridStepFormGridStepIndexChanged(int gridStepIndex)
		{
			this.selectorData.GridStepIndex = gridStepIndex;
		}

		// Token: 0x06001358 RID: 4952 RVA: 0x0008DE34 File Offset: 0x0008CE34
		private void OnSelectorGridStep(MethodArgs methodArgs)
		{
			this.gridStepForm.Show();
			this.gridStepForm.SetGridStepIndex(this.selectorData.GridStepIndex);
			this.gridStepForm.SetBounds(Cursor.Position.X - this.gridStepForm.Size.Width / 2, Cursor.Position.Y - this.gridStepForm.Size.Height / 2, 0, 0, BoundsSpecified.Location);
		}

		// Token: 0x06001359 RID: 4953 RVA: 0x0008DEB6 File Offset: 0x0008CEB6
		private void OnSelectorObjectOriented(MethodArgs methodArgs)
		{
			if (this.selectorData != null)
			{
				this.selectorData.ObjectOriented = !this.selectorData.ObjectOriented;
			}
		}

		// Token: 0x0600135A RID: 4954 RVA: 0x0008DED9 File Offset: 0x0008CED9
		private void OnSelectorSizeUp(MethodArgs methodArgs)
		{
			if (this.selectorData != null)
			{
				this.selectorData.SizeUpScale();
			}
		}

		// Token: 0x0600135B RID: 4955 RVA: 0x0008DEEF File Offset: 0x0008CEEF
		private void OnSelectorSizeDown(MethodArgs methodArgs)
		{
			if (this.selectorData != null)
			{
				this.selectorData.SizeDownScale();
			}
		}

		// Token: 0x0600135C RID: 4956 RVA: 0x0008DF08 File Offset: 0x0008CF08
		private void OnLinkObjects(MethodArgs methodArgs)
		{
			if (this.LinkAvailable())
			{
				IMapObject left = null;
				IMapObject right = null;
				foreach (KeyValuePair<IMapObject, IMapObject> keyValuePair in this.multiMapObject.MapObjects)
				{
					if (left == null)
					{
						left = keyValuePair.Key;
					}
					else if (right == null)
					{
						right = keyValuePair.Key;
					}
				}
				this.LinkObjects(left, right);
			}
		}

		// Token: 0x0600135D RID: 4957 RVA: 0x0008DF88 File Offset: 0x0008CF88
		private void OnUnlinkObjects(MethodArgs methodArgs)
		{
			if (this.UnlinkAvailable())
			{
				IMapObject left = null;
				IMapObject right = null;
				foreach (KeyValuePair<IMapObject, IMapObject> keyValuePair in this.multiMapObject.MapObjects)
				{
					if (left == null)
					{
						left = keyValuePair.Key;
					}
					else if (right == null)
					{
						right = keyValuePair.Key;
					}
				}
				this.UnlinkObjects(left, right);
			}
		}

		// Token: 0x0600135E RID: 4958 RVA: 0x0008E008 File Offset: 0x0008D008
		private void OnSelectorTypeChanged(MapObjectSelector.SelectorData _selectorData, ref MapObjectSelector.SelectorType oldValue, ref MapObjectSelector.SelectorType newValue)
		{
			this.UpdateSelectorTypeControls();
		}

		// Token: 0x0600135F RID: 4959 RVA: 0x0008E010 File Offset: 0x0008D010
		private void OnObjectOrientedChanged(MapObjectSelector.SelectorData _selectorData, ref bool oldValue, ref bool newValue)
		{
			this.UpdateObjectOrientedControls();
			if (this.ObjectOrientedChanged != null)
			{
				this.ObjectOrientedChanged(this, ref oldValue, ref newValue);
			}
		}

		// Token: 0x06001360 RID: 4960 RVA: 0x0008E02E File Offset: 0x0008D02E
		private void OnSurfaceChanged(MapObjectSelector.SelectorData _selectorData, ref TerrainSurface oldValue, ref TerrainSurface newValue)
		{
			this.multiMapObject.Surface = this.selectorData.Surface;
			this.UpdateSurfaceControls();
			if (this.SurfaceChanged != null)
			{
				this.SurfaceChanged(this, ref oldValue, ref newValue);
			}
		}

		// Token: 0x06001361 RID: 4961 RVA: 0x0008E062 File Offset: 0x0008D062
		private void OnAlignToGridChanged(MapObjectSelector.SelectorData _selectorData, ref bool oldValue, ref bool newValue)
		{
			this.UpdateAlignToGridControls();
			if (this.AlignToGridChanged != null)
			{
				this.AlignToGridChanged(this, ref oldValue, ref newValue);
			}
		}

		// Token: 0x06001362 RID: 4962 RVA: 0x0008E080 File Offset: 0x0008D080
		private void OnPlaceAlongNormalChanged(MapObjectSelector.SelectorData _selectorData, ref bool oldValue, ref bool newValue)
		{
			this.UpdatePlaceAlongNormalControls();
			if (this.PlaceAlongNormalChanged != null)
			{
				this.PlaceAlongNormalChanged(this, ref oldValue, ref newValue);
			}
		}

		// Token: 0x06001363 RID: 4963 RVA: 0x0008E09E File Offset: 0x0008D09E
		private void OnLockSelectionChanged(MapObjectSelector.SelectorData _selectorData, ref bool oldValue, ref bool newValue)
		{
			this.UpdateLockSelectionControls();
			if (this.LockSelectionChanged != null)
			{
				this.LockSelectionChanged(this, ref oldValue, ref newValue);
			}
		}

		// Token: 0x06001364 RID: 4964 RVA: 0x0008E0BC File Offset: 0x0008D0BC
		private void OnEditObjectAfterAddChanged(MapObjectSelector.SelectorData _selectorData, ref bool oldValue, ref bool newValue)
		{
			this.UpdateEditObjectAfterAddControls();
			if (this.EditObjectAfterAddChanged != null)
			{
				this.EditObjectAfterAddChanged(this, ref oldValue, ref newValue);
			}
		}

		// Token: 0x06001365 RID: 4965 RVA: 0x0008E0DA File Offset: 0x0008D0DA
		private void OnAutolinkObjectAfterAddChanged(MapObjectSelector.SelectorData _selectorData, ref bool oldValue, ref bool newValue)
		{
			this.UpdateAutolinkObjectAfterAddControls();
			if (this.AutolinkObjectAfterAddChanged != null)
			{
				this.AutolinkObjectAfterAddChanged(this, ref oldValue, ref newValue);
			}
		}

		// Token: 0x06001366 RID: 4966 RVA: 0x0008E0F8 File Offset: 0x0008D0F8
		private void OnCreateCrosslinksChanged(MapObjectSelector.SelectorData _selectorData, ref bool oldValue, ref bool newValue)
		{
			this.UpdateCreateCrosslinksControls();
			if (this.CreateCrosslinksChanged != null)
			{
				this.CreateCrosslinksChanged(this, ref oldValue, ref newValue);
			}
		}

		// Token: 0x06001367 RID: 4967 RVA: 0x0008E116 File Offset: 0x0008D116
		private void OnGridStepIndexChanged(MapObjectSelector.SelectorData _selectorData, ref int oldValue, ref int newValue)
		{
			this.UpdateGridStepControls();
			if (this.GridStepIndexChanged != null)
			{
				this.GridStepIndexChanged(this, ref oldValue, ref newValue);
			}
		}

		// Token: 0x06001368 RID: 4968 RVA: 0x0008E134 File Offset: 0x0008D134
		private void UpdateSelectorTypeControls()
		{
			this.selectorState.SetMethodParams("selector_move", true, true, true, this.selectorData.SelectorType == MapObjectSelector.SelectorType.Move);
			this.selectorState.SetMethodParams("selector_rotate", true, true, true, this.selectorData.SelectorType == MapObjectSelector.SelectorType.Rotate);
			this.selectorState.SetMethodParams("selector_scale", true, true, true, this.selectorData.SelectorType == MapObjectSelector.SelectorType.Scale);
		}

		// Token: 0x06001369 RID: 4969 RVA: 0x0008E1A4 File Offset: 0x0008D1A4
		private void UpdateSurfaceControls()
		{
			this.selectorState.SetMethodParams("selector_along_object", true, true, true, this.selectorData.Surface == TerrainSurface.Object);
			this.selectorState.SetMethodParams("selector_along_terrain", true, true, true, this.selectorData.Surface == TerrainSurface.Terrain);
			this.selectorState.SetMethodParams("selector_along_water", true, true, true, this.selectorData.Surface == TerrainSurface.Water);
		}

		// Token: 0x0600136A RID: 4970 RVA: 0x0008E214 File Offset: 0x0008D214
		private void UpdateAlignToGridControls()
		{
			this.selectorState.SetMethodParams("selector_align_to_grid", true, true, true, this.selectorData.AlignToGrid);
		}

		// Token: 0x0600136B RID: 4971 RVA: 0x0008E234 File Offset: 0x0008D234
		private void UpdatePlaceAlongNormalControls()
		{
			this.selectorState.SetMethodParams("selector_place_along_normal", true, true, true, this.selectorData.PlaceAlongNormal);
		}

		// Token: 0x0600136C RID: 4972 RVA: 0x0008E254 File Offset: 0x0008D254
		private void UpdateLockSelectionControls()
		{
			this.selectorState.SetMethodParams("selector_lock_selection", true, true, true, this.selectorData.LockSelection);
		}

		// Token: 0x0600136D RID: 4973 RVA: 0x0008E274 File Offset: 0x0008D274
		private void UpdateEditObjectAfterAddControls()
		{
			this.selectorState.SetMethodParams("selector_edit_object_after_add", true, true, true, this.selectorData.EditObjectAfterAdd);
		}

		// Token: 0x0600136E RID: 4974 RVA: 0x0008E294 File Offset: 0x0008D294
		private void UpdateAutolinkObjectAfterAddControls()
		{
			this.selectorState.SetMethodParams("selector_autolink_after_add", true, true, true, this.selectorData.AutolinkObjectAfterAdd);
		}

		// Token: 0x0600136F RID: 4975 RVA: 0x0008E2B4 File Offset: 0x0008D2B4
		private void UpdateCreateCrosslinksControls()
		{
			this.selectorState.SetMethodParams("selector_create_crosslinks", true, true, true, this.selectorData.CreateCrosslinks);
		}

		// Token: 0x06001370 RID: 4976 RVA: 0x0008E2D4 File Offset: 0x0008D2D4
		private void UpdateGridStepControls()
		{
			this.selectorState.SetMethodText("selector_grid_step", this.selectorData.GridStepText);
		}

		// Token: 0x06001371 RID: 4977 RVA: 0x0008E2F1 File Offset: 0x0008D2F1
		private void UpdateObjectOrientedControls()
		{
			this.selectorState.SetMethodParams("selector_object_oriented", true, true, true, this.selectorData.ObjectOriented);
		}

		// Token: 0x06001372 RID: 4978 RVA: 0x0008E311 File Offset: 0x0008D311
		private void UpdateLinkControls()
		{
			this.selectorState.SetMethodParams("link", true, this.LinkAvailable(), false, false);
			this.selectorState.SetMethodParams("unlink", true, this.UnlinkAvailable(), false, false);
		}

		// Token: 0x06001373 RID: 4979 RVA: 0x0008E348 File Offset: 0x0008D348
		private ILinkData CreateLinkData(IMapObject left, IMapObject right)
		{
			if (left != null && right != null)
			{
				foreach (ILinkChecker linkChecker in this.linkCheckers)
				{
					ILinkData newLinkData = linkChecker.CreateLinkData(left, right, this.mapObjectContainer);
					if (newLinkData != null)
					{
						return newLinkData;
					}
				}
			}
			return null;
		}

		// Token: 0x06001374 RID: 4980 RVA: 0x0008E3B4 File Offset: 0x0008D3B4
		private bool LinkAvailable()
		{
			if (this.multiMapObject != null && this.multiMapObject.MapObjects.Count == 2)
			{
				IMapObject left = null;
				IMapObject right = null;
				foreach (KeyValuePair<IMapObject, IMapObject> keyValuePair in this.multiMapObject.MapObjects)
				{
					if (left == null)
					{
						left = keyValuePair.Key;
					}
					else if (right == null)
					{
						right = keyValuePair.Key;
					}
				}
				return this.LinkAvailable(left, right);
			}
			return false;
		}

		// Token: 0x06001375 RID: 4981 RVA: 0x0008E448 File Offset: 0x0008D448
		private bool UnlinkAvailable()
		{
			return this.multiMapObject != null && (this.multiMapObject.MapObjects.Count == 1 || this.multiMapObject.MapObjects.Count == 2);
		}

		// Token: 0x06001376 RID: 4982 RVA: 0x0008E47C File Offset: 0x0008D47C
		private void OnOperationUndo(OperationContainer _operationContainer, IOperation operation, int index, bool result)
		{
			if (result && ObjectsState.CheckForObjectsChanges(operation) && this.multiMapObject != null && this.multiMapObject.MapObjects.Count > 0)
			{
				this.multiMapObject.RecreateData(this.mapObjectContainer.MapObjects);
				this.UpdataLastZPlane();
			}
		}

		// Token: 0x06001377 RID: 4983 RVA: 0x0008E4CC File Offset: 0x0008D4CC
		private void OnOperationRedo(OperationContainer _operationContainer, IOperation operation, int index, bool result)
		{
			if (result && ObjectsState.CheckForObjectsChanges(operation) && this.multiMapObject != null && this.multiMapObject.MapObjects.Count > 0)
			{
				this.multiMapObject.RecreateData(this.mapObjectContainer.MapObjects);
				this.UpdataLastZPlane();
			}
		}

		// Token: 0x06001378 RID: 4984 RVA: 0x0008E51C File Offset: 0x0008D51C
		private void OnTransactionCanceled(OperationContainer _operationContainer)
		{
			if (this.multiMapObject != null && this.multiMapObject.MapObjects.Count > 0)
			{
				this.multiMapObject.RecreateData(this.mapObjectContainer.MapObjects);
				this.UpdataLastZPlane();
			}
		}

		// Token: 0x06001379 RID: 4985 RVA: 0x0008E555 File Offset: 0x0008D555
		private void OnBuffersChanged(OperationContainer _operationContainer)
		{
			if (this.multiMapObject != null && this.multiMapObject.MapObjects.Count == 1)
			{
				this.multiMapObject.RecreateData(this.mapObjectContainer.MapObjects);
				this.UpdataLastZPlane();
			}
		}

		// Token: 0x0600137A RID: 4986 RVA: 0x0008E590 File Offset: 0x0008D590
		private void UpdataLastZPlane()
		{
			if (this.multiMapObject.MapObjects.Count > 0)
			{
				this.lastZPlane = this.multiMapObject.Position.Z;
			}
		}

		// Token: 0x0600137B RID: 4987 RVA: 0x0008E5CC File Offset: 0x0008D5CC
		private void InternalAdd(GroupContainer.GroupSelectionMode groupSelectionMode, IMapObject mapObjectToAdd)
		{
			List<IMapObject> mapObjects = new List<IMapObject>();
			if (this.groupContainer != null)
			{
				this.groupContainer.GetMapObjects(groupSelectionMode, mapObjectToAdd, mapObjects);
			}
			else
			{
				mapObjects.Add(mapObjectToAdd);
			}
			foreach (IMapObject mapObject in mapObjects)
			{
				if (this.mapObjectFilter == null || this.mapObjectFilter.FilterObject(mapObject))
				{
					this.multiMapObject.Add(mapObject);
				}
			}
			this.UpdataLastZPlane();
			this.UpdateLinkControls();
			if (this.SelectionChanged != null)
			{
				this.SelectionChanged(this);
			}
		}

		// Token: 0x0600137C RID: 4988 RVA: 0x0008E67C File Offset: 0x0008D67C
		public MapObjectSelector(MapObjectContainer _mapObjectContainer, StateContainer _stateContainer, OperationContainer _operationContainer, GroupContainer _groupContainer, IMapObjectFilter _mapObjectFilter, EditorScene _editorScene, string _selectorDataFileName)
		{
			this.mapObjectContainer = _mapObjectContainer;
			this.stateContainer = _stateContainer;
			this.operationContainer = _operationContainer;
			this.groupContainer = _groupContainer;
			this.mapObjectFilter = _mapObjectFilter;
			this.selectorDataFileName = _selectorDataFileName;
			this.CreateSelectorData();
			if (this.mapObjectContainer != null)
			{
				IMapObject mapObject = this.mapObjectContainer.MapObjectFactory.CreateMapObject(-1, new MapObjectType(MapObjectFactory.Type.MultiObject, string.Empty), this.mapObjectContainer, true, true);
				this.multiMapObject = (mapObject as MultiMapObject);
				if (this.multiMapObject != null)
				{
					this.multiMapObject.Select = true;
					this.multiMapObject.Highlight("MapObjectSelector", MapObjectSelector.selectionColor, true);
					this.multiMapObject.Surface = this.selectorData.Surface;
					this.multiMapObject.ObjectBounds = this.mapObjectContainer.ObjectBounds;
				}
			}
			if (this.stateContainer != null)
			{
				this.selectorState = new State("MapObjectSelectorState");
				this.selectorState.AddMethod("selector_move", new Method(this.OnSelectorMove));
				this.selectorState.AddMethod("selector_rotate", new Method(this.OnSelectorRotate));
				this.selectorState.AddMethod("selector_scale", new Method(this.OnSelectorScale));
				this.selectorState.AddMethod("selector_object_oriented", new Method(this.OnSelectorObjectOriented));
				this.selectorState.AddMethod("selector_along_terrain", new Method(this.OnSelectorAlongTerrain));
				this.selectorState.AddMethod("selector_along_water", new Method(this.OnSelectorAlongWater));
				this.selectorState.AddMethod("selector_along_object", new Method(this.OnSelectorAlongObject));
				this.selectorState.AddMethod("selector_align_to_grid", new Method(this.OnSelectorAlignToGrid));
				this.selectorState.AddMethod("selector_place_along_normal", new Method(this.OnSelectorPlaceAlongNormal));
				this.selectorState.AddMethod("selector_lock_selection", new Method(this.OnSelectorLockSelection));
				this.selectorState.AddMethod("selector_edit_object_after_add", new Method(this.OnSelectorEditObjectAfterAdd));
				this.selectorState.AddMethod("selector_autolink_after_add", new Method(this.OnSelectorAutolinkObjectAfterAdd));
				this.selectorState.AddMethod("selector_create_crosslinks", new Method(this.OnSelectorCreateCrosslinks));
				this.selectorState.AddMethod("selector_grid_step", new Method(this.OnSelectorGridStep));
				this.selectorState.AddMethod("selector_size_up", new Method(this.OnSelectorSizeUp));
				this.selectorState.AddMethod("selector_size_down", new Method(this.OnSelectorSizeDown));
				this.selectorState.AddMethod("link", new Method(this.OnLinkObjects));
				this.selectorState.AddMethod("unlink", new Method(this.OnUnlinkObjects));
			}
			if (this.operationContainer != null)
			{
				this.operationContainer.OperationUndoInvoked += this.OnOperationUndo;
				this.operationContainer.OperationRedoInvoked += this.OnOperationRedo;
				this.operationContainer.TransactionCanceled += this.OnTransactionCanceled;
				this.operationContainer.BuffersChanged += this.OnBuffersChanged;
			}
			this.widgetData = new MapObjectSelector.WidgetData(_editorScene);
			this.widgetData.Bind(this.selectorData, this.multiMapObject);
			this.gridStepForm = new GridStepForm();
			this.gridStepForm.GridStepIndexChanged += this.OnGridStepFormGridStepIndexChanged;
			this.mapObjectSelectorOperationContainer = new MapObjectSelectorOperationContainer(this.operationContainer, this);
		}

		// Token: 0x0600137D RID: 4989 RVA: 0x0008EA34 File Offset: 0x0008DA34
		public void Destroy()
		{
			if (this.mapObjectSelectorOperationContainer != null)
			{
				this.mapObjectSelectorOperationContainer.Destroy();
				this.mapObjectSelectorOperationContainer = null;
			}
			if (this.widgetData != null)
			{
				this.widgetData.Unbind();
				this.widgetData = null;
				if (this.multiMapObject != null)
				{
					this.multiMapObject.Destroy();
					this.multiMapObject = null;
				}
				this.DestroySelectorData();
			}
			if (this.stateContainer != null)
			{
				this.stateContainer.UnbindState(this.selectorState);
				this.selectorState = null;
				this.stateContainer = null;
			}
			if (this.operationContainer != null)
			{
				this.operationContainer.OperationUndoInvoked -= this.OnOperationUndo;
				this.operationContainer.OperationRedoInvoked -= this.OnOperationRedo;
				this.operationContainer.TransactionCanceled -= this.OnTransactionCanceled;
				this.operationContainer.BuffersChanged -= this.OnBuffersChanged;
				this.operationContainer = null;
			}
			this.gridStepForm.GridStepIndexChanged -= this.OnGridStepFormGridStepIndexChanged;
			this.gridStepForm.Close();
			this.gridStepForm = null;
			this.mapObjectContainer = null;
			this.groupContainer = null;
			this.mapObjectFilter = null;
			this.linkCheckers.Clear();
		}

		// Token: 0x0600137E RID: 4990 RVA: 0x0008EB70 File Offset: 0x0008DB70
		public void Bind()
		{
			if (this.stateContainer != null && this.selectorState != null)
			{
				this.stateContainer.BindState(this.selectorState);
			}
			this.UpdateSelectorTypeControls();
			this.UpdateObjectOrientedControls();
			this.UpdateSurfaceControls();
			this.UpdateAlignToGridControls();
			this.UpdatePlaceAlongNormalControls();
			this.UpdateLockSelectionControls();
			this.UpdateEditObjectAfterAddControls();
			this.UpdateAutolinkObjectAfterAddControls();
			this.UpdateCreateCrosslinksControls();
			this.UpdateGridStepControls();
			this.UpdateLinkControls();
		}

		// Token: 0x0600137F RID: 4991 RVA: 0x0008EBE0 File Offset: 0x0008DBE0
		public void Unbind()
		{
			if (this.stateContainer != null && this.selectorState != null)
			{
				this.stateContainer.UnbindState(this.selectorState);
			}
		}

		// Token: 0x06001380 RID: 4992 RVA: 0x0008EC03 File Offset: 0x0008DC03
		public bool Contains(IMapObject mapObject)
		{
			return this.multiMapObject.MapObjects.ContainsKey(mapObject);
		}

		// Token: 0x06001381 RID: 4993 RVA: 0x0008EC18 File Offset: 0x0008DC18
		public void Clear()
		{
			this.multiMapObject.Clear();
			this.multiMapObject.Select = true;
			this.multiMapObject.Highlight("MapObjectSelector", MapObjectSelector.selectionColor, true);
			this.multiMapObject.Surface = this.selectorData.Surface;
			this.UpdateLinkControls();
			if (this.SelectionChanged != null)
			{
				this.SelectionChanged(this);
			}
		}

		// Token: 0x06001382 RID: 4994 RVA: 0x0008EC82 File Offset: 0x0008DC82
		public void Add(GroupContainer.GroupSelectionMode groupSelectionMode, IMapObject mapObject)
		{
			this.InternalAdd(groupSelectionMode, mapObject);
		}

		// Token: 0x06001383 RID: 4995 RVA: 0x0008EC8C File Offset: 0x0008DC8C
		public void Add(IMapObject mapObject)
		{
			this.InternalAdd(this.selectorData.GroupSelectionMode, mapObject);
		}

		// Token: 0x06001384 RID: 4996 RVA: 0x0008ECA0 File Offset: 0x0008DCA0
		public bool Remove(IMapObject mapObject)
		{
			bool result = this.multiMapObject.Remove(mapObject);
			this.UpdataLastZPlane();
			this.UpdateLinkControls();
			if (this.SelectionChanged != null)
			{
				this.SelectionChanged(this);
			}
			return result;
		}

		// Token: 0x06001385 RID: 4997 RVA: 0x0008ECDC File Offset: 0x0008DCDC
		public void Add(List<IMapObject> mapObjects)
		{
			if (mapObjects != null)
			{
				foreach (IMapObject mapObject in mapObjects)
				{
					if (this.mapObjectFilter == null || this.mapObjectFilter.FilterObject(mapObject))
					{
						this.multiMapObject.Add(mapObject);
					}
				}
				this.UpdataLastZPlane();
				this.UpdateLinkControls();
				if (this.SelectionChanged != null)
				{
					this.SelectionChanged(this);
				}
			}
		}

		// Token: 0x06001386 RID: 4998 RVA: 0x0008ED68 File Offset: 0x0008DD68
		public void Add(IPickedMapObjectPicker pickedMapObjectPicker)
		{
			foreach (PickedMapObject pickedMapObject in pickedMapObjectPicker.MapObjects)
			{
				if (this.mapObjectFilter == null || this.mapObjectFilter.FilterObject(pickedMapObject.MapObject))
				{
					this.multiMapObject.Add(pickedMapObject.MapObject);
				}
			}
			this.UpdataLastZPlane();
			this.UpdateLinkControls();
			if (this.SelectionChanged != null)
			{
				this.SelectionChanged(this);
			}
		}

		// Token: 0x06001387 RID: 4999 RVA: 0x0008EE00 File Offset: 0x0008DE00
		public void Sync(IPickedMapObjectPicker pickedMapObjectPicker)
		{
			Dictionary<IMapObject, IMapObject> pickedMapObjects = new Dictionary<IMapObject, IMapObject>();
			foreach (PickedMapObject pickedMapObject in pickedMapObjectPicker.MapObjects)
			{
				List<IMapObject> mapObjects = new List<IMapObject>();
				if (this.groupContainer != null)
				{
					this.groupContainer.GetMapObjects(this.selectorData.GroupSelectionMode, pickedMapObject.MapObject, mapObjects);
				}
				else
				{
					mapObjects.Add(pickedMapObject.MapObject);
				}
				foreach (IMapObject mapObject in mapObjects)
				{
					if (!pickedMapObjects.ContainsKey(mapObject) && (this.mapObjectFilter == null || this.mapObjectFilter.FilterObject(mapObject)))
					{
						pickedMapObjects.Add(mapObject, mapObject.Clone(mapObject.ID, true, false));
					}
				}
			}
			this.multiMapObject.Sync(pickedMapObjects);
			this.UpdataLastZPlane();
			this.UpdateLinkControls();
			if (this.SelectionChanged != null)
			{
				this.SelectionChanged(this);
			}
		}

		// Token: 0x06001388 RID: 5000 RVA: 0x0008EF2C File Offset: 0x0008DF2C
		public void DropToNearestHeight()
		{
			if (this.multiMapObject != null && this.multiMapObject.MapObjects.Count > 0)
			{
				bool transactionInProgress = false;
				if (this.operationContainer != null)
				{
					transactionInProgress = this.operationContainer.DoesTransactionInProgress;
					if (!transactionInProgress)
					{
						this.operationContainer.BeginTransaction();
					}
				}
				foreach (KeyValuePair<IMapObject, IMapObject> keyValuePair in this.multiMapObject.MapObjects)
				{
					Position _mapObjectPosition = keyValuePair.Key.Position;
					bool exists;
					double nearestFlatHeight = this.mapObjectContainer.GetNearestFlatHeight(ref _mapObjectPosition, keyValuePair.Key.ID, out exists);
					if (exists)
					{
						Position newMapObjectPosition = new Position(keyValuePair.Key.Position.X, keyValuePair.Key.Position.Y, nearestFlatHeight);
						if (this.selectorData.AlignToGrid)
						{
							newMapObjectPosition = newMapObjectPosition.AlignToGrid(this.GridStep);
						}
						keyValuePair.Key.Position = newMapObjectPosition;
						keyValuePair.Value.Position = newMapObjectPosition;
						Rotation newRotation;
						if (this.selectorData.PlaceAlongNormal && this.GetRotationAlongNormal(keyValuePair.Key, out newRotation))
						{
							keyValuePair.Key.Rotation = newRotation;
							keyValuePair.Value.Rotation = newRotation;
						}
					}
					else if (this.selectorData.Surface == TerrainSurface.Water)
					{
						double waterHeight = this.mapObjectContainer.GetWaterHeight(0, keyValuePair.Key.Position.X, keyValuePair.Key.Position.Y);
						double terrainHeight = this.mapObjectContainer.GetTerrainHeight(0, keyValuePair.Key.Position.X, keyValuePair.Key.Position.Y);
						if (waterHeight > terrainHeight)
						{
							Position newMapObjectPosition2 = new Position(keyValuePair.Key.Position.X, keyValuePair.Key.Position.Y, waterHeight);
							if (this.selectorData.AlignToGrid)
							{
								newMapObjectPosition2 = newMapObjectPosition2.AlignToGrid(this.GridStep);
							}
							keyValuePair.Key.Position = newMapObjectPosition2;
							keyValuePair.Value.Position = newMapObjectPosition2;
							Rotation newRotation2;
							if (this.selectorData.PlaceAlongNormal && this.GetRotationAlongNormal(keyValuePair.Key, out newRotation2))
							{
								keyValuePair.Key.Rotation = newRotation2;
								keyValuePair.Value.Rotation = newRotation2;
							}
						}
					}
				}
				this.multiMapObject.RecreateData(this.mapObjectContainer.MapObjects);
				this.UpdataLastZPlane();
				if (this.operationContainer != null && !transactionInProgress)
				{
					this.operationContainer.EndTransaction();
				}
				if (this.SelectionChanged != null)
				{
					this.SelectionChanged(this);
				}
			}
		}

		// Token: 0x06001389 RID: 5001 RVA: 0x0008F22C File Offset: 0x0008E22C
		public void ArrangeLinkedObjects()
		{
			if (this.multiMapObject != null && this.multiMapObject.MapObjects.Count > 0)
			{
				bool transactionInProgress = false;
				if (this.operationContainer != null)
				{
					transactionInProgress = this.operationContainer.DoesTransactionInProgress;
					if (!transactionInProgress)
					{
						this.operationContainer.BeginTransaction();
					}
				}
				foreach (KeyValuePair<IMapObject, IMapObject> keyValuePair in this.multiMapObject.MapObjects)
				{
					MapObjectSelector.InternalArrandeLinkedObject(keyValuePair.Key, keyValuePair.Value, this.mapObjectContainer);
				}
				this.multiMapObject.RecreateData(this.mapObjectContainer.MapObjects);
				this.UpdataLastZPlane();
				if (this.operationContainer != null && !transactionInProgress)
				{
					this.operationContainer.EndTransaction();
				}
				if (this.SelectionChanged != null)
				{
					this.SelectionChanged(this);
				}
			}
		}

		// Token: 0x0600138A RID: 5002 RVA: 0x0008F324 File Offset: 0x0008E324
		public void RearrangeLinkedObjects()
		{
			List<List<RoutePoint>> routes;
			try
			{
				routes = RoutePoint.GetRoutes(this.multiMapObject.MapObjects, this.mapObjectContainer);
			}
			catch (StackOverflowException e)
			{
				Console.WriteLine(e);
				return;
			}
			if (routes != null && routes.Count > 0)
			{
				bool transactionInProgress = false;
				if (this.operationContainer != null)
				{
					transactionInProgress = this.operationContainer.DoesTransactionInProgress;
					if (!transactionInProgress)
					{
						this.operationContainer.BeginTransaction();
					}
				}
				foreach (List<RoutePoint> route in routes)
				{
					if (route != null && route.Count > 0)
					{
						int routeAdvance = route.Count;
						foreach (RoutePoint routePoint in route)
						{
							if (routePoint != null)
							{
								routePoint.RouteAdvance = (double)routeAdvance;
								routeAdvance--;
								routePoint.Rotation = new Rotation((float)Geometry.NormalizeRadian((double)routePoint.Rotation.Yaw + 3.141592653589793), routePoint.Rotation.Pitch, routePoint.Rotation.Roll);
								IMapObject mapObject;
								if (this.multiMapObject.MapObjects.TryGetValue(routePoint, out mapObject) && mapObject != null)
								{
									mapObject.Rotation = routePoint.Rotation;
								}
							}
						}
					}
				}
				this.multiMapObject.RecreateData(this.mapObjectContainer.MapObjects);
				this.UpdataLastZPlane();
				if (this.operationContainer != null && !transactionInProgress)
				{
					this.operationContainer.EndTransaction();
				}
				if (this.SelectionChanged != null)
				{
					this.SelectionChanged(this);
				}
			}
		}

		// Token: 0x0600138B RID: 5003 RVA: 0x0008F508 File Offset: 0x0008E508
		public void RotateAlongNormal()
		{
			if (this.multiMapObject != null && this.multiMapObject.MapObjects.Count > 0)
			{
				bool transactionInProgress = false;
				if (this.operationContainer != null)
				{
					transactionInProgress = this.operationContainer.DoesTransactionInProgress;
					if (!transactionInProgress)
					{
						this.operationContainer.BeginTransaction();
					}
				}
				foreach (KeyValuePair<IMapObject, IMapObject> keyValuePair in this.multiMapObject.MapObjects)
				{
					Rotation newRotation;
					if (this.GetRotationAlongNormal(keyValuePair.Key, out newRotation))
					{
						keyValuePair.Key.Rotation = newRotation;
						keyValuePair.Value.Rotation = newRotation;
					}
				}
				this.multiMapObject.RecreateData(this.mapObjectContainer.MapObjects);
				this.UpdataLastZPlane();
				if (this.operationContainer != null && !transactionInProgress)
				{
					this.operationContainer.EndTransaction();
				}
				if (this.SelectionChanged != null)
				{
					this.SelectionChanged(this);
				}
			}
		}

		// Token: 0x0600138C RID: 5004 RVA: 0x0008F614 File Offset: 0x0008E614
		public bool GetRotationAlongNormal(IMapObject mapObject, out Rotation rotation)
		{
			Vec3 normal;
			if (mapObject != null && Math.Abs(mapObject.Altitude) < 1.0 && this.mapObjectContainer.GetNormal(mapObject.Position.X, mapObject.Position.Y, out normal))
			{
				normal.Normalize();
				Quat quat = new Quat(mapObject.Rotation);
				Vec3 axisX;
				Vec3 axisY;
				Vec3 axisZ;
				quat.GetPivot(out axisX, out axisY, out axisZ);
				Vec3 newAxisX = Vec3.Cross(axisY, normal);
				if (newAxisX.Normalize() < MathConsts.DOUBLE_EPSILON)
				{
					newAxisX = Vec3.Cross(Vec3.Cross(normal, axisX), normal);
					newAxisX.Normalize();
				}
				Vec3 newAxisY = Vec3.Cross(normal, newAxisX);
				newAxisY.Normalize();
				Quat newQuat = new Quat(ref newAxisX, ref newAxisY, ref normal);
				rotation = new Rotation(ref newQuat);
				return true;
			}
			rotation = Rotation.Empty;
			return false;
		}

		// Token: 0x0600138D RID: 5005 RVA: 0x0008F6FF File Offset: 0x0008E6FF
		public bool Pack(List<IMapObject> packedMapObjects, Dictionary<int, Dictionary<int, ILinkData>> packedLinks)
		{
			return MapObjectContainer.Pack(this.mapObjectContainer, this.multiMapObject.MapObjects, true, packedMapObjects, packedLinks);
		}

		// Token: 0x0600138E RID: 5006 RVA: 0x0008F71C File Offset: 0x0008E71C
		public bool LinkAvailable(IMapObject left, IMapObject right)
		{
			if (left != null && right != null)
			{
				foreach (ILinkChecker linkChecker in this.linkCheckers)
				{
					if (linkChecker.CanLink(left, right, this.mapObjectContainer))
					{
						return true;
					}
				}
				return false;
			}
			return false;
		}

		// Token: 0x0600138F RID: 5007 RVA: 0x0008F788 File Offset: 0x0008E788
		public bool LinkObjects(IMapObject left, IMapObject right)
		{
			return this.LinkObjects(left, right, null);
		}

		// Token: 0x06001390 RID: 5008 RVA: 0x0008F794 File Offset: 0x0008E794
		public bool LinkObjects(IMapObject left, IMapObject right, ILinkData linkData)
		{
			bool result = false;
			if (left != null && right != null)
			{
				bool transactionInProgress = false;
				if (this.operationContainer != null)
				{
					transactionInProgress = this.operationContainer.DoesTransactionInProgress;
					if (!transactionInProgress)
					{
						this.operationContainer.BeginTransaction();
					}
				}
				if (linkData != null)
				{
					result = this.mapObjectContainer.AddLink(left, right, linkData);
				}
				else
				{
					result = this.mapObjectContainer.AddLink(left, right, this.CreateLinkData(left, right));
				}
				if (this.operationContainer != null && !transactionInProgress)
				{
					this.operationContainer.EndTransaction();
				}
				this.UpdateLinkControls();
				if (this.SelectionChanged != null)
				{
					this.SelectionChanged(this);
				}
			}
			return result;
		}

		// Token: 0x06001391 RID: 5009 RVA: 0x0008F830 File Offset: 0x0008E830
		public void UnlinkObjects(IMapObject left, IMapObject right)
		{
			bool transactionInProgress = false;
			if (this.operationContainer != null)
			{
				transactionInProgress = this.operationContainer.DoesTransactionInProgress;
				if (!transactionInProgress)
				{
					this.operationContainer.BeginTransaction();
				}
			}
			if (right == null)
			{
				this.mapObjectContainer.RemoveLink(left);
			}
			else
			{
				this.mapObjectContainer.RemoveLink(left, right);
			}
			if (this.operationContainer != null && !transactionInProgress)
			{
				this.operationContainer.EndTransaction();
			}
			this.UpdateLinkControls();
			if (this.SelectionChanged != null)
			{
				this.SelectionChanged(this);
			}
		}

		// Token: 0x06001392 RID: 5010 RVA: 0x0008F8B4 File Offset: 0x0008E8B4
		public void ArrangeLinkedObject(IMapObject mapObject)
		{
			if (mapObject != null)
			{
				bool transactionInProgress = false;
				if (this.operationContainer != null)
				{
					transactionInProgress = this.operationContainer.DoesTransactionInProgress;
					if (!transactionInProgress)
					{
						this.operationContainer.BeginTransaction();
					}
				}
				MapObjectSelector.InternalArrandeLinkedObject(mapObject, null, this.mapObjectContainer);
				if (this.operationContainer != null && !transactionInProgress)
				{
					this.operationContainer.EndTransaction();
				}
				if (this.SelectionChanged != null)
				{
					this.SelectionChanged(this);
				}
			}
		}

		// Token: 0x170003CE RID: 974
		// (get) Token: 0x06001393 RID: 5011 RVA: 0x0008F921 File Offset: 0x0008E921
		// (set) Token: 0x06001394 RID: 5012 RVA: 0x0008F929 File Offset: 0x0008E929
		[Browsable(false)]
		public double LastZPlane
		{
			get
			{
				return this.lastZPlane;
			}
			set
			{
				this.lastZPlane = value;
			}
		}

		// Token: 0x170003CF RID: 975
		// (get) Token: 0x06001395 RID: 5013 RVA: 0x0008F932 File Offset: 0x0008E932
		[Browsable(false)]
		public bool Empty
		{
			get
			{
				return this.multiMapObject.MapObjects.Count == 0;
			}
		}

		// Token: 0x170003D0 RID: 976
		// (get) Token: 0x06001396 RID: 5014 RVA: 0x0008F947 File Offset: 0x0008E947
		// (set) Token: 0x06001397 RID: 5015 RVA: 0x0008F954 File Offset: 0x0008E954
		[Browsable(false)]
		public bool ForceValue
		{
			get
			{
				return this.multiMapObject.ForceValue;
			}
			set
			{
				this.multiMapObject.ForceValue = value;
			}
		}

		// Token: 0x170003D1 RID: 977
		// (get) Token: 0x06001398 RID: 5016 RVA: 0x0008F962 File Offset: 0x0008E962
		[Browsable(false)]
		public MultiMapObject MultiMapObject
		{
			get
			{
				return this.multiMapObject;
			}
		}

		// Token: 0x170003D2 RID: 978
		// (get) Token: 0x06001399 RID: 5017 RVA: 0x0008F96A File Offset: 0x0008E96A
		[Browsable(false)]
		public Dictionary<IMapObject, IMapObject> MapObjects
		{
			get
			{
				return this.multiMapObject.MapObjects;
			}
		}

		// Token: 0x170003D3 RID: 979
		// (get) Token: 0x0600139A RID: 5018 RVA: 0x0008F977 File Offset: 0x0008E977
		[Browsable(false)]
		public List<ILinkChecker> LinkCheckers
		{
			get
			{
				return this.linkCheckers;
			}
		}

		// Token: 0x170003D4 RID: 980
		// (get) Token: 0x0600139B RID: 5019 RVA: 0x0008F97F File Offset: 0x0008E97F
		// (set) Token: 0x0600139C RID: 5020 RVA: 0x0008F98C File Offset: 0x0008E98C
		[TypeConverter(typeof(PositionConverter))]
		[Category("Geometry")]
		[Browsable(true)]
		public Position Position
		{
			get
			{
				return this.multiMapObject.Position;
			}
			set
			{
				bool transactionInProgress = false;
				if (this.operationContainer != null)
				{
					transactionInProgress = this.operationContainer.DoesTransactionInProgress;
					if (!transactionInProgress)
					{
						this.operationContainer.BeginTransaction();
					}
				}
				if (this.selectorData.AlignToGrid)
				{
					this.multiMapObject.Position = value.AlignToGrid(this.GridStep);
				}
				else
				{
					this.multiMapObject.Position = value;
				}
				if (this.selectorData.PlaceAlongNormal)
				{
					foreach (KeyValuePair<IMapObject, IMapObject> keyValuePair in this.multiMapObject.MapObjects)
					{
						Rotation newRotation;
						if (this.GetRotationAlongNormal(keyValuePair.Key, out newRotation))
						{
							keyValuePair.Key.Rotation = newRotation;
							keyValuePair.Value.Rotation = newRotation;
						}
					}
					this.multiMapObject.RecreateRotationMedian();
				}
				if (this.operationContainer != null && !transactionInProgress)
				{
					this.operationContainer.EndTransaction();
				}
				if (this.SelectionChanged != null)
				{
					this.SelectionChanged(this);
				}
			}
		}

		// Token: 0x170003D5 RID: 981
		// (get) Token: 0x0600139D RID: 5021 RVA: 0x0008FAA4 File Offset: 0x0008EAA4
		// (set) Token: 0x0600139E RID: 5022 RVA: 0x0008FAB4 File Offset: 0x0008EAB4
		[TypeConverter(typeof(RotationConverter))]
		[Browsable(true)]
		[Category("Geometry")]
		public Rotation Rotation
		{
			get
			{
				return this.multiMapObject.Rotation;
			}
			set
			{
				bool transactionInProgress = false;
				if (this.operationContainer != null)
				{
					transactionInProgress = this.operationContainer.DoesTransactionInProgress;
					if (!transactionInProgress)
					{
						this.operationContainer.BeginTransaction();
					}
				}
				this.multiMapObject.Rotation = value;
				if (this.operationContainer != null && !transactionInProgress)
				{
					this.operationContainer.EndTransaction();
				}
				if (this.SelectionChanged != null)
				{
					this.SelectionChanged(this);
				}
			}
		}

		// Token: 0x170003D6 RID: 982
		// (get) Token: 0x0600139F RID: 5023 RVA: 0x0008FB1D File Offset: 0x0008EB1D
		// (set) Token: 0x060013A0 RID: 5024 RVA: 0x0008FB2C File Offset: 0x0008EB2C
		[Category("Geometry")]
		[TypeConverter(typeof(ScaleConverter))]
		[Browsable(true)]
		public Scale Scale
		{
			get
			{
				return this.multiMapObject.Scale;
			}
			set
			{
				bool transactionInProgress = false;
				if (this.operationContainer != null)
				{
					transactionInProgress = this.operationContainer.DoesTransactionInProgress;
					if (!transactionInProgress)
					{
						this.operationContainer.BeginTransaction();
					}
				}
				this.multiMapObject.Scale = value;
				if (this.operationContainer != null && !transactionInProgress)
				{
					this.operationContainer.EndTransaction();
				}
				if (this.SelectionChanged != null)
				{
					this.SelectionChanged(this);
				}
			}
		}

		// Token: 0x170003D7 RID: 983
		// (get) Token: 0x060013A1 RID: 5025 RVA: 0x0008FB95 File Offset: 0x0008EB95
		// (set) Token: 0x060013A2 RID: 5026 RVA: 0x0008FBA4 File Offset: 0x0008EBA4
		[Category("Geometry")]
		[Browsable(true)]
		public double Altitude
		{
			get
			{
				return this.multiMapObject.Altitude;
			}
			set
			{
				bool transactionInProgress = false;
				if (this.operationContainer != null)
				{
					transactionInProgress = this.operationContainer.DoesTransactionInProgress;
					if (!transactionInProgress)
					{
						this.operationContainer.BeginTransaction();
					}
				}
				this.multiMapObject.Altitude = value;
				if (this.operationContainer != null && !transactionInProgress)
				{
					this.operationContainer.EndTransaction();
				}
				if (this.SelectionChanged != null)
				{
					this.SelectionChanged(this);
				}
			}
		}

		// Token: 0x170003D8 RID: 984
		// (get) Token: 0x060013A3 RID: 5027 RVA: 0x0008FC0D File Offset: 0x0008EC0D
		// (set) Token: 0x060013A4 RID: 5028 RVA: 0x0008FC1A File Offset: 0x0008EC1A
		[Browsable(false)]
		public TerrainSurface Surface
		{
			get
			{
				return this.selectorData.Surface;
			}
			set
			{
				this.selectorData.Surface = value;
			}
		}

		// Token: 0x170003D9 RID: 985
		// (get) Token: 0x060013A5 RID: 5029 RVA: 0x0008FC28 File Offset: 0x0008EC28
		// (set) Token: 0x060013A6 RID: 5030 RVA: 0x0008FC35 File Offset: 0x0008EC35
		[Browsable(false)]
		public bool AlignToGrid
		{
			get
			{
				return this.selectorData.AlignToGrid;
			}
			set
			{
				this.selectorData.AlignToGrid = value;
			}
		}

		// Token: 0x170003DA RID: 986
		// (get) Token: 0x060013A7 RID: 5031 RVA: 0x0008FC43 File Offset: 0x0008EC43
		// (set) Token: 0x060013A8 RID: 5032 RVA: 0x0008FC50 File Offset: 0x0008EC50
		[Browsable(false)]
		public bool PlaceAlongNormal
		{
			get
			{
				return this.selectorData.PlaceAlongNormal;
			}
			set
			{
				this.selectorData.PlaceAlongNormal = value;
			}
		}

		// Token: 0x170003DB RID: 987
		// (get) Token: 0x060013A9 RID: 5033 RVA: 0x0008FC5E File Offset: 0x0008EC5E
		// (set) Token: 0x060013AA RID: 5034 RVA: 0x0008FC6B File Offset: 0x0008EC6B
		[Browsable(false)]
		public bool LockSelection
		{
			get
			{
				return this.selectorData.LockSelection;
			}
			set
			{
				this.selectorData.LockSelection = value;
			}
		}

		// Token: 0x170003DC RID: 988
		// (get) Token: 0x060013AB RID: 5035 RVA: 0x0008FC79 File Offset: 0x0008EC79
		// (set) Token: 0x060013AC RID: 5036 RVA: 0x0008FC86 File Offset: 0x0008EC86
		[Browsable(false)]
		public bool EditObjectAfterAdd
		{
			get
			{
				return this.selectorData.EditObjectAfterAdd;
			}
			set
			{
				this.selectorData.EditObjectAfterAdd = value;
			}
		}

		// Token: 0x170003DD RID: 989
		// (get) Token: 0x060013AD RID: 5037 RVA: 0x0008FC94 File Offset: 0x0008EC94
		// (set) Token: 0x060013AE RID: 5038 RVA: 0x0008FCA1 File Offset: 0x0008ECA1
		[Browsable(false)]
		public bool AutolinkObjectAfterAdd
		{
			get
			{
				return this.selectorData.AutolinkObjectAfterAdd;
			}
			set
			{
				this.selectorData.AutolinkObjectAfterAdd = value;
			}
		}

		// Token: 0x170003DE RID: 990
		// (get) Token: 0x060013AF RID: 5039 RVA: 0x0008FCAF File Offset: 0x0008ECAF
		// (set) Token: 0x060013B0 RID: 5040 RVA: 0x0008FCBC File Offset: 0x0008ECBC
		[Browsable(false)]
		public bool CreateCrosslinks
		{
			get
			{
				return this.selectorData.CreateCrosslinks;
			}
			set
			{
				this.selectorData.CreateCrosslinks = value;
			}
		}

		// Token: 0x170003DF RID: 991
		// (get) Token: 0x060013B1 RID: 5041 RVA: 0x0008FCCA File Offset: 0x0008ECCA
		// (set) Token: 0x060013B2 RID: 5042 RVA: 0x0008FCD7 File Offset: 0x0008ECD7
		[Browsable(false)]
		public double GridStep
		{
			get
			{
				return this.selectorData.GridStep;
			}
			set
			{
				this.selectorData.GridStep = value;
			}
		}

		// Token: 0x170003E0 RID: 992
		// (get) Token: 0x060013B3 RID: 5043 RVA: 0x0008FCE5 File Offset: 0x0008ECE5
		// (set) Token: 0x060013B4 RID: 5044 RVA: 0x0008FCF2 File Offset: 0x0008ECF2
		[Browsable(false)]
		public GroupContainer.GroupSelectionMode GroupSelectionMode
		{
			get
			{
				return this.selectorData.GroupSelectionMode;
			}
			set
			{
				this.selectorData.GroupSelectionMode = value;
			}
		}

		// Token: 0x170003E1 RID: 993
		// (get) Token: 0x060013B5 RID: 5045 RVA: 0x0008FD00 File Offset: 0x0008ED00
		[Browsable(false)]
		public bool Picked
		{
			get
			{
				return this.startPickData != null;
			}
		}

		// Token: 0x060013B6 RID: 5046 RVA: 0x0008FD10 File Offset: 0x0008ED10
		public void Pick(int x, int y, IPickedMapObjectPicker pickedMapObjectPicker)
		{
			if (this.widgetData.Created)
			{
				this.selectorData.SelectedPart = MapObjectSelector.SelectedPart.Unknown;
				this.startPickData = new PickData(0, x, y, this.mapObjectContainer, this.multiMapObject, this.selectorData.ObjectOriented, this.selectorData.Surface);
				double pickAxisRadius = (double)this.startPickData.Scale.Ratio * this.selectorData.Scale * (double)MapObjectSelector.axisArrowRadius;
				double pickAxisSize = (double)this.startPickData.Scale.Ratio * this.selectorData.Scale * (double)(MapObjectSelector.axisSize - MapObjectSelector.axisArrowRadius);
				Vec3 centerSpot = this.startPickData.Position.Vec3;
				Vec3 axisXSpot = centerSpot + this.startPickData.AxisData.AxisX * pickAxisSize;
				Vec3 axisYSpot = centerSpot + this.startPickData.AxisData.AxisY * pickAxisSize;
				Vec3 axisZSpot = centerSpot + this.startPickData.AxisData.AxisZ * pickAxisSize;
				if (this.startPickData.ProjectiveRay.GetDistance(centerSpot) < pickAxisRadius)
				{
					if (this.selectorData.SelectorType == MapObjectSelector.SelectorType.Move)
					{
						if (this.selectorData.ActivePlane == MapObjectSelector.ActivePlane.X)
						{
							this.selectorData.SelectedPart = MapObjectSelector.SelectedPart.PlaneX;
							return;
						}
						if (this.selectorData.ActivePlane == MapObjectSelector.ActivePlane.Y)
						{
							this.selectorData.SelectedPart = MapObjectSelector.SelectedPart.PlaneY;
							return;
						}
						if (this.selectorData.ActivePlane == MapObjectSelector.ActivePlane.Z)
						{
							this.selectorData.SelectedPart = MapObjectSelector.SelectedPart.PlaneZ;
							return;
						}
					}
					else if (this.selectorData.SelectorType == MapObjectSelector.SelectorType.Scale)
					{
						this.multiMapObject.Surface = TerrainSurface.None;
						this.selectorData.SelectedPart = MapObjectSelector.SelectedPart.Center;
					}
					return;
				}
				if (this.startPickData.ProjectiveRay.GetDistance(axisXSpot) < pickAxisRadius)
				{
					if (this.selectorData.SelectorType == MapObjectSelector.SelectorType.Move && KeyStatus.Control)
					{
						this.selectorData.ActivePlane = MapObjectSelector.ActivePlane.X;
						return;
					}
					this.multiMapObject.Surface = TerrainSurface.None;
					if (this.selectorData.SelectorType == MapObjectSelector.SelectorType.Rotate)
					{
						this.startPickData.SavedPoint = this.startPickData.PickedPointOnPlaneZ;
					}
					this.selectorData.SelectedPart = MapObjectSelector.SelectedPart.AxisX;
					return;
				}
				else if (this.startPickData.ProjectiveRay.GetDistance(axisYSpot) < pickAxisRadius)
				{
					if (this.selectorData.SelectorType == MapObjectSelector.SelectorType.Move && KeyStatus.Control)
					{
						this.selectorData.ActivePlane = MapObjectSelector.ActivePlane.Y;
						return;
					}
					this.multiMapObject.Surface = TerrainSurface.None;
					if (this.selectorData.SelectorType == MapObjectSelector.SelectorType.Rotate)
					{
						this.startPickData.SavedPoint = this.startPickData.PickedPointOnPlaneX;
					}
					this.selectorData.SelectedPart = MapObjectSelector.SelectedPart.AxisY;
					return;
				}
				else if (this.startPickData.ProjectiveRay.GetDistance(axisZSpot) < pickAxisRadius)
				{
					if (this.selectorData.SelectorType == MapObjectSelector.SelectorType.Move && KeyStatus.Control)
					{
						this.selectorData.ActivePlane = MapObjectSelector.ActivePlane.Z;
						return;
					}
					this.multiMapObject.Surface = TerrainSurface.None;
					if (this.selectorData.SelectorType == MapObjectSelector.SelectorType.Rotate)
					{
						this.startPickData.SavedPoint = this.startPickData.PickedPointOnPlaneY;
					}
					this.selectorData.SelectedPart = MapObjectSelector.SelectedPart.AxisZ;
					return;
				}
				else
				{
					if (this.multiMapObject.MapObjects.Count > 0)
					{
						foreach (PickedMapObject pickedMapObject in pickedMapObjectPicker.MapObjects)
						{
							if (pickedMapObject != null && this.multiMapObject.MapObjects.ContainsKey(pickedMapObject.MapObject))
							{
								if (this.selectorData.SelectorType == MapObjectSelector.SelectorType.Move)
								{
									if (this.selectorData.ActivePlane == MapObjectSelector.ActivePlane.X)
									{
										this.selectorData.SelectedPart = MapObjectSelector.SelectedPart.PlaneX;
									}
									else if (this.selectorData.ActivePlane == MapObjectSelector.ActivePlane.Y)
									{
										this.selectorData.SelectedPart = MapObjectSelector.SelectedPart.PlaneY;
									}
									else if (this.selectorData.ActivePlane == MapObjectSelector.ActivePlane.Z)
									{
										this.selectorData.SelectedPart = MapObjectSelector.SelectedPart.PlaneZ;
									}
									return;
								}
								if (this.selectorData.SelectorType == MapObjectSelector.SelectorType.Rotate)
								{
									return;
								}
								if (this.selectorData.SelectorType == MapObjectSelector.SelectorType.Scale)
								{
									this.multiMapObject.Surface = TerrainSurface.None;
									this.selectorData.SelectedPart = MapObjectSelector.SelectedPart.Center;
									return;
								}
							}
						}
					}
					this.ClearPick();
				}
			}
		}

		// Token: 0x060013B7 RID: 5047 RVA: 0x00090158 File Offset: 0x0008F158
		public void ProcessPick(int x, int y)
		{
			if (this.Picked)
			{
				PickData currentPickData = this.startPickData.GetNewIntersection(0, x, y, this.mapObjectContainer, this.selectorData.Surface);
				if (this.selectorData.SelectorType == MapObjectSelector.SelectorType.Move)
				{
					if (this.selectorData.SelectedPart == MapObjectSelector.SelectedPart.PlaneX)
					{
						if (this.selectorData.Surface != TerrainSurface.None)
						{
							this.Position = this.startPickData.Position + (currentPickData.TerrainPosition - this.startPickData.TerrainPosition);
						}
						else
						{
							this.Position = this.startPickData.Position + (currentPickData.PickedPointOnPlaneX - this.startPickData.PickedPointOnPlaneX);
						}
					}
					else if (this.selectorData.SelectedPart == MapObjectSelector.SelectedPart.PlaneY)
					{
						if (this.selectorData.Surface != TerrainSurface.None)
						{
							this.Position = this.startPickData.Position + (currentPickData.TerrainPosition - this.startPickData.TerrainPosition);
						}
						else
						{
							this.Position = this.startPickData.Position + (currentPickData.PickedPointOnPlaneY - this.startPickData.PickedPointOnPlaneY);
						}
					}
					else if (this.selectorData.SelectedPart == MapObjectSelector.SelectedPart.PlaneZ)
					{
						if (this.selectorData.Surface != TerrainSurface.None)
						{
							this.Position = this.startPickData.Position + (currentPickData.TerrainPosition - this.startPickData.TerrainPosition);
						}
						else
						{
							this.Position = this.startPickData.Position + (currentPickData.PickedPointOnPlaneZ - this.startPickData.PickedPointOnPlaneZ);
						}
					}
					else if (this.selectorData.SelectedPart == MapObjectSelector.SelectedPart.AxisX)
					{
						this.Position = this.startPickData.Position + (currentPickData.PickedPointOnAxisX - this.startPickData.PickedPointOnAxisX);
					}
					else if (this.selectorData.SelectedPart == MapObjectSelector.SelectedPart.AxisY)
					{
						this.Position = this.startPickData.Position + (currentPickData.PickedPointOnAxisY - this.startPickData.PickedPointOnAxisY);
					}
					else if (this.selectorData.SelectedPart == MapObjectSelector.SelectedPart.AxisZ)
					{
						this.Position = this.startPickData.Position + (currentPickData.PickedPointOnAxisZ - this.startPickData.PickedPointOnAxisZ);
					}
				}
				else if (this.selectorData.SelectorType == MapObjectSelector.SelectorType.Rotate)
				{
					Vec3 start = Vec3.Empty;
					Vec3 current = Vec3.Empty;
					bool filled = false;
					if (this.selectorData.SelectedPart == MapObjectSelector.SelectedPart.AxisX)
					{
						start = this.startPickData.SavedPoint - this.startPickData.Position.Vec3;
						current = currentPickData.PickedPointOnPlaneZ - this.startPickData.Position.Vec3;
						this.startPickData.SavedPoint = currentPickData.PickedPointOnPlaneZ;
						filled = true;
					}
					else if (this.selectorData.SelectedPart == MapObjectSelector.SelectedPart.AxisY)
					{
						start = this.startPickData.SavedPoint - this.startPickData.Position.Vec3;
						current = currentPickData.PickedPointOnPlaneX - this.startPickData.Position.Vec3;
						this.startPickData.SavedPoint = currentPickData.PickedPointOnPlaneX;
						filled = true;
					}
					else if (this.selectorData.SelectedPart == MapObjectSelector.SelectedPart.AxisZ)
					{
						start = this.startPickData.SavedPoint - this.startPickData.Position.Vec3;
						current = currentPickData.PickedPointOnPlaneY - this.startPickData.Position.Vec3;
						this.startPickData.SavedPoint = currentPickData.PickedPointOnPlaneY;
						filled = true;
					}
					if (filled)
					{
						Vec3 v = start;
						Vec3 v2 = start + current;
						v.Normalize();
						v2.Normalize();
						Vec3 cross = Vec3.Cross(v, v2);
						double dot = Vec3.Dot(v, v2);
						Quat startQuat = new Quat(this.multiMapObject.Rotation);
						Quat currentQuat = new Quat(cross.X, cross.Y, cross.Z, dot);
						currentQuat *= startQuat;
						double yaw;
						double pitch;
						double roll;
						currentQuat.GetYawPitchRoll(out yaw, out pitch, out roll);
						this.Rotation = new Rotation((float)yaw, (float)pitch, (float)roll);
					}
				}
				else if (this.selectorData.SelectorType == MapObjectSelector.SelectorType.Scale)
				{
					double startDistanceToCenter = this.startPickData.DistanceToCenter;
					double currentDistanceToCenter = currentPickData.DistanceToCenter;
					if (startDistanceToCenter < 1.0)
					{
						startDistanceToCenter += 1.0;
						currentDistanceToCenter += 1.0;
					}
					float _ratio = this.startPickData.Scale.Ratio * (float)(currentDistanceToCenter / startDistanceToCenter);
					float _x = (this.selectorData.SelectedPart == MapObjectSelector.SelectedPart.AxisX || this.selectorData.SelectedPart == MapObjectSelector.SelectedPart.Center) ? (this.startPickData.Scale.X * (float)(currentDistanceToCenter / startDistanceToCenter)) : this.startPickData.Scale.X;
					float _y = (this.selectorData.SelectedPart == MapObjectSelector.SelectedPart.AxisY || this.selectorData.SelectedPart == MapObjectSelector.SelectedPart.Center) ? (this.startPickData.Scale.Y * (float)(currentDistanceToCenter / startDistanceToCenter)) : this.startPickData.Scale.Y;
					float _z = (this.selectorData.SelectedPart == MapObjectSelector.SelectedPart.AxisZ || this.selectorData.SelectedPart == MapObjectSelector.SelectedPart.Center) ? (this.startPickData.Scale.Z * (float)(currentDistanceToCenter / startDistanceToCenter)) : this.startPickData.Scale.Z;
					float _radius = (this.selectorData.SelectedPart == MapObjectSelector.SelectedPart.AxisX || this.selectorData.SelectedPart == MapObjectSelector.SelectedPart.AxisY || this.selectorData.SelectedPart == MapObjectSelector.SelectedPart.Center) ? (this.startPickData.Scale.Radius * (float)(currentDistanceToCenter / startDistanceToCenter)) : this.startPickData.Scale.Radius;
					this.Scale = new Scale(_ratio, _x, _y, _z, _radius);
				}
				this.UpdataLastZPlane();
			}
		}

		// Token: 0x060013B8 RID: 5048 RVA: 0x0009079A File Offset: 0x0008F79A
		public void ClearPick()
		{
			this.UpdataLastZPlane();
			this.selectorData.SelectedPart = MapObjectSelector.SelectedPart.Unknown;
			this.startPickData = null;
			this.multiMapObject.Surface = this.selectorData.Surface;
		}

		// Token: 0x04000DE3 RID: 3555
		private const string highlightKey = "MapObjectSelector";

		// Token: 0x04000DE4 RID: 3556
		public static readonly Color selectionColor = Color.FromArgb(MapObjectCreationInfo.DefaultTransparentColorAlpha + 1, Color.Magenta);

		// Token: 0x04000DE5 RID: 3557
		private static readonly float axisArrowRadius = 0.1f;

		// Token: 0x04000DE6 RID: 3558
		private static readonly float axisSize = 1.5f;

		// Token: 0x04000DE7 RID: 3559
		private MapObjectContainer mapObjectContainer;

		// Token: 0x04000DE8 RID: 3560
		private StateContainer stateContainer;

		// Token: 0x04000DE9 RID: 3561
		private OperationContainer operationContainer;

		// Token: 0x04000DEA RID: 3562
		private GroupContainer groupContainer;

		// Token: 0x04000DEB RID: 3563
		private IMapObjectFilter mapObjectFilter;

		// Token: 0x04000DEC RID: 3564
		private GridStepForm gridStepForm;

		// Token: 0x04000DED RID: 3565
		private State selectorState;

		// Token: 0x04000DEE RID: 3566
		private MultiMapObject multiMapObject;

		// Token: 0x04000DEF RID: 3567
		private MapObjectSelectorOperationContainer mapObjectSelectorOperationContainer;

		// Token: 0x04000DF0 RID: 3568
		private readonly string selectorDataFileName;

		// Token: 0x04000DF1 RID: 3569
		private MapObjectSelector.SelectorData selectorData;

		// Token: 0x04000DF2 RID: 3570
		private PickData startPickData;

		// Token: 0x04000DF3 RID: 3571
		private MapObjectSelector.WidgetData widgetData;

		// Token: 0x04000DF4 RID: 3572
		private double lastZPlane;

		// Token: 0x04000DF5 RID: 3573
		private readonly List<ILinkChecker> linkCheckers = new List<ILinkChecker>();

		// Token: 0x02000194 RID: 404
		public enum SelectorType
		{
			// Token: 0x04000E01 RID: 3585
			Unknown,
			// Token: 0x04000E02 RID: 3586
			Move,
			// Token: 0x04000E03 RID: 3587
			Rotate,
			// Token: 0x04000E04 RID: 3588
			Scale
		}

		// Token: 0x02000195 RID: 405
		public enum SelectedPart
		{
			// Token: 0x04000E06 RID: 3590
			Unknown,
			// Token: 0x04000E07 RID: 3591
			AxisX,
			// Token: 0x04000E08 RID: 3592
			AxisY,
			// Token: 0x04000E09 RID: 3593
			AxisZ,
			// Token: 0x04000E0A RID: 3594
			PlaneX,
			// Token: 0x04000E0B RID: 3595
			PlaneY,
			// Token: 0x04000E0C RID: 3596
			PlaneZ,
			// Token: 0x04000E0D RID: 3597
			Center
		}

		// Token: 0x02000196 RID: 406
		public enum ActivePlane
		{
			// Token: 0x04000E0F RID: 3599
			X,
			// Token: 0x04000E10 RID: 3600
			Y,
			// Token: 0x04000E11 RID: 3601
			Z
		}

		// Token: 0x02000197 RID: 407
		public class SelectorData
		{
			// Token: 0x060013BA RID: 5050 RVA: 0x000907F8 File Offset: 0x0008F7F8
			private bool ChangeScale(double newScale)
			{
				if (newScale > MapObjectSelector.SelectorData.maxScale)
				{
					newScale = MapObjectSelector.SelectorData.maxScale;
				}
				else if (newScale < MapObjectSelector.SelectorData.minScale)
				{
					newScale = MapObjectSelector.SelectorData.minScale;
				}
				if (newScale != this.scale)
				{
					this.InvokeChanging();
					double oldScale = this.scale;
					this.scale = newScale;
					this.InvokeChanged();
					if (this.active && this.ScaleChanged != null)
					{
						this.ScaleChanged(this, ref oldScale, ref this.scale);
					}
					return true;
				}
				return false;
			}

			// Token: 0x14000080 RID: 128
			// (add) Token: 0x060013BB RID: 5051 RVA: 0x0009086F File Offset: 0x0008F86F
			// (remove) Token: 0x060013BC RID: 5052 RVA: 0x00090888 File Offset: 0x0008F888
			public event MapObjectSelector.SelectorData.ChangeEvent Changing;

			// Token: 0x14000081 RID: 129
			// (add) Token: 0x060013BD RID: 5053 RVA: 0x000908A1 File Offset: 0x0008F8A1
			// (remove) Token: 0x060013BE RID: 5054 RVA: 0x000908BA File Offset: 0x0008F8BA
			public event MapObjectSelector.SelectorData.ChangeEvent Changed;

			// Token: 0x14000082 RID: 130
			// (add) Token: 0x060013BF RID: 5055 RVA: 0x000908D3 File Offset: 0x0008F8D3
			// (remove) Token: 0x060013C0 RID: 5056 RVA: 0x000908EC File Offset: 0x0008F8EC
			public event MapObjectSelector.SelectorData.FieldChangedEvent<MapObjectSelector.SelectorType> SelectorTypeChanged;

			// Token: 0x14000083 RID: 131
			// (add) Token: 0x060013C1 RID: 5057 RVA: 0x00090905 File Offset: 0x0008F905
			// (remove) Token: 0x060013C2 RID: 5058 RVA: 0x0009091E File Offset: 0x0008F91E
			public event MapObjectSelector.SelectorData.FieldChangedEvent<MapObjectSelector.ActivePlane> ActivePlaneChanged;

			// Token: 0x14000084 RID: 132
			// (add) Token: 0x060013C3 RID: 5059 RVA: 0x00090937 File Offset: 0x0008F937
			// (remove) Token: 0x060013C4 RID: 5060 RVA: 0x00090950 File Offset: 0x0008F950
			public event MapObjectSelector.SelectorData.FieldChangedEvent<MapObjectSelector.SelectedPart> SelectedPartChanged;

			// Token: 0x14000085 RID: 133
			// (add) Token: 0x060013C5 RID: 5061 RVA: 0x00090969 File Offset: 0x0008F969
			// (remove) Token: 0x060013C6 RID: 5062 RVA: 0x00090982 File Offset: 0x0008F982
			public event MapObjectSelector.SelectorData.FieldChangedEvent<bool> ObjectOrientedChanged;

			// Token: 0x14000086 RID: 134
			// (add) Token: 0x060013C7 RID: 5063 RVA: 0x0009099B File Offset: 0x0008F99B
			// (remove) Token: 0x060013C8 RID: 5064 RVA: 0x000909B4 File Offset: 0x0008F9B4
			public event MapObjectSelector.SelectorData.FieldChangedEvent<TerrainSurface> SurfaceChanged;

			// Token: 0x14000087 RID: 135
			// (add) Token: 0x060013C9 RID: 5065 RVA: 0x000909CD File Offset: 0x0008F9CD
			// (remove) Token: 0x060013CA RID: 5066 RVA: 0x000909E6 File Offset: 0x0008F9E6
			public event MapObjectSelector.SelectorData.FieldChangedEvent<bool> AlignToGridChanged;

			// Token: 0x14000088 RID: 136
			// (add) Token: 0x060013CB RID: 5067 RVA: 0x000909FF File Offset: 0x0008F9FF
			// (remove) Token: 0x060013CC RID: 5068 RVA: 0x00090A18 File Offset: 0x0008FA18
			public event MapObjectSelector.SelectorData.FieldChangedEvent<bool> PlaceAlongNormalChanged;

			// Token: 0x14000089 RID: 137
			// (add) Token: 0x060013CD RID: 5069 RVA: 0x00090A31 File Offset: 0x0008FA31
			// (remove) Token: 0x060013CE RID: 5070 RVA: 0x00090A4A File Offset: 0x0008FA4A
			public event MapObjectSelector.SelectorData.FieldChangedEvent<bool> LockSelectionChanged;

			// Token: 0x1400008A RID: 138
			// (add) Token: 0x060013CF RID: 5071 RVA: 0x00090A63 File Offset: 0x0008FA63
			// (remove) Token: 0x060013D0 RID: 5072 RVA: 0x00090A7C File Offset: 0x0008FA7C
			public event MapObjectSelector.SelectorData.FieldChangedEvent<bool> EditObjectAfterAddChanged;

			// Token: 0x1400008B RID: 139
			// (add) Token: 0x060013D1 RID: 5073 RVA: 0x00090A95 File Offset: 0x0008FA95
			// (remove) Token: 0x060013D2 RID: 5074 RVA: 0x00090AAE File Offset: 0x0008FAAE
			public event MapObjectSelector.SelectorData.FieldChangedEvent<bool> AutolinkObjectAfterAddChanged;

			// Token: 0x1400008C RID: 140
			// (add) Token: 0x060013D3 RID: 5075 RVA: 0x00090AC7 File Offset: 0x0008FAC7
			// (remove) Token: 0x060013D4 RID: 5076 RVA: 0x00090AE0 File Offset: 0x0008FAE0
			public event MapObjectSelector.SelectorData.FieldChangedEvent<bool> CreateCrosslinksChanged;

			// Token: 0x1400008D RID: 141
			// (add) Token: 0x060013D5 RID: 5077 RVA: 0x00090AF9 File Offset: 0x0008FAF9
			// (remove) Token: 0x060013D6 RID: 5078 RVA: 0x00090B12 File Offset: 0x0008FB12
			public event MapObjectSelector.SelectorData.FieldChangedEvent<int> GridStepIndexChanged;

			// Token: 0x1400008E RID: 142
			// (add) Token: 0x060013D7 RID: 5079 RVA: 0x00090B2B File Offset: 0x0008FB2B
			// (remove) Token: 0x060013D8 RID: 5080 RVA: 0x00090B44 File Offset: 0x0008FB44
			public event MapObjectSelector.SelectorData.FieldChangedEvent<double> ScaleChanged;

			// Token: 0x060013D9 RID: 5081 RVA: 0x00090B5D File Offset: 0x0008FB5D
			public void InvokeChanging()
			{
				if (this.active && this.Changing != null)
				{
					this.Changing(this);
				}
			}

			// Token: 0x060013DA RID: 5082 RVA: 0x00090B7B File Offset: 0x0008FB7B
			public void InvokeChanged()
			{
				if (this.active && this.Changed != null)
				{
					this.Changed(this);
				}
			}

			// Token: 0x170003E2 RID: 994
			// (set) Token: 0x060013DB RID: 5083 RVA: 0x00090B99 File Offset: 0x0008FB99
			[XmlIgnore]
			public bool Active
			{
				set
				{
					this.active = value;
				}
			}

			// Token: 0x170003E3 RID: 995
			// (get) Token: 0x060013DC RID: 5084 RVA: 0x00090BA2 File Offset: 0x0008FBA2
			// (set) Token: 0x060013DD RID: 5085 RVA: 0x00090BAC File Offset: 0x0008FBAC
			public MapObjectSelector.SelectorType SelectorType
			{
				get
				{
					return this.selectorType;
				}
				set
				{
					if (this.selectorType != value)
					{
						this.InvokeChanging();
						MapObjectSelector.SelectorType oldSelectorType = this.selectorType;
						this.selectorType = value;
						this.InvokeChanged();
						if (this.active && this.SelectorTypeChanged != null)
						{
							this.SelectorTypeChanged(this, ref oldSelectorType, ref this.selectorType);
						}
					}
				}
			}

			// Token: 0x170003E4 RID: 996
			// (get) Token: 0x060013DE RID: 5086 RVA: 0x00090C00 File Offset: 0x0008FC00
			// (set) Token: 0x060013DF RID: 5087 RVA: 0x00090C08 File Offset: 0x0008FC08
			public MapObjectSelector.ActivePlane ActivePlane
			{
				get
				{
					return this.activePlane;
				}
				set
				{
					if (this.activePlane != value)
					{
						this.InvokeChanging();
						MapObjectSelector.ActivePlane oldActivePlane = this.activePlane;
						this.activePlane = value;
						this.InvokeChanged();
						if (this.active && this.ActivePlaneChanged != null)
						{
							this.ActivePlaneChanged(this, ref oldActivePlane, ref this.activePlane);
						}
					}
				}
			}

			// Token: 0x170003E5 RID: 997
			// (get) Token: 0x060013E0 RID: 5088 RVA: 0x00090C5C File Offset: 0x0008FC5C
			// (set) Token: 0x060013E1 RID: 5089 RVA: 0x00090C64 File Offset: 0x0008FC64
			[XmlIgnore]
			public MapObjectSelector.SelectedPart SelectedPart
			{
				get
				{
					return this.selectedPart;
				}
				set
				{
					if (this.selectedPart != value)
					{
						this.InvokeChanging();
						MapObjectSelector.SelectedPart oldSelectedPart = this.selectedPart;
						this.selectedPart = value;
						this.InvokeChanged();
						if (this.active && this.SelectedPartChanged != null)
						{
							this.SelectedPartChanged(this, ref oldSelectedPart, ref this.selectedPart);
						}
					}
				}
			}

			// Token: 0x170003E6 RID: 998
			// (get) Token: 0x060013E2 RID: 5090 RVA: 0x00090CB8 File Offset: 0x0008FCB8
			// (set) Token: 0x060013E3 RID: 5091 RVA: 0x00090CC0 File Offset: 0x0008FCC0
			public bool ObjectOriented
			{
				get
				{
					return this.objectOriented;
				}
				set
				{
					if (this.objectOriented != value)
					{
						this.InvokeChanging();
						bool oldObjectOriented = this.objectOriented;
						this.objectOriented = value;
						this.InvokeChanged();
						if (this.active && this.ObjectOrientedChanged != null)
						{
							this.ObjectOrientedChanged(this, ref oldObjectOriented, ref this.objectOriented);
						}
					}
				}
			}

			// Token: 0x170003E7 RID: 999
			// (get) Token: 0x060013E4 RID: 5092 RVA: 0x00090D14 File Offset: 0x0008FD14
			// (set) Token: 0x060013E5 RID: 5093 RVA: 0x00090D1C File Offset: 0x0008FD1C
			public TerrainSurface Surface
			{
				get
				{
					return this.surface;
				}
				set
				{
					if (this.surface != value)
					{
						this.InvokeChanging();
						TerrainSurface oldSurface = this.surface;
						this.surface = value;
						this.InvokeChanged();
						if (this.active && this.SurfaceChanged != null)
						{
							this.SurfaceChanged(this, ref oldSurface, ref this.surface);
						}
					}
				}
			}

			// Token: 0x170003E8 RID: 1000
			// (get) Token: 0x060013E6 RID: 5094 RVA: 0x00090D70 File Offset: 0x0008FD70
			// (set) Token: 0x060013E7 RID: 5095 RVA: 0x00090D78 File Offset: 0x0008FD78
			public bool AlignToGrid
			{
				get
				{
					return this.alignToGrid;
				}
				set
				{
					if (this.alignToGrid != value)
					{
						this.InvokeChanging();
						bool oldAlignToGrid = this.alignToGrid;
						this.alignToGrid = value;
						this.InvokeChanged();
						if (this.active && this.AlignToGridChanged != null)
						{
							this.AlignToGridChanged(this, ref oldAlignToGrid, ref this.alignToGrid);
						}
					}
				}
			}

			// Token: 0x170003E9 RID: 1001
			// (get) Token: 0x060013E8 RID: 5096 RVA: 0x00090DCC File Offset: 0x0008FDCC
			// (set) Token: 0x060013E9 RID: 5097 RVA: 0x00090DD4 File Offset: 0x0008FDD4
			public bool PlaceAlongNormal
			{
				get
				{
					return this.placeAlongNormal;
				}
				set
				{
					if (this.placeAlongNormal != value)
					{
						this.InvokeChanging();
						bool oldPlaceAlongNormal = this.placeAlongNormal;
						this.placeAlongNormal = value;
						this.InvokeChanged();
						if (this.active && this.PlaceAlongNormalChanged != null)
						{
							this.PlaceAlongNormalChanged(this, ref oldPlaceAlongNormal, ref this.placeAlongNormal);
						}
					}
				}
			}

			// Token: 0x170003EA RID: 1002
			// (get) Token: 0x060013EA RID: 5098 RVA: 0x00090E28 File Offset: 0x0008FE28
			// (set) Token: 0x060013EB RID: 5099 RVA: 0x00090E30 File Offset: 0x0008FE30
			public bool LockSelection
			{
				get
				{
					return this.lockSelection;
				}
				set
				{
					if (this.lockSelection != value)
					{
						this.InvokeChanging();
						bool oldLockSelection = this.lockSelection;
						this.lockSelection = value;
						this.InvokeChanged();
						if (this.active && this.LockSelectionChanged != null)
						{
							this.LockSelectionChanged(this, ref oldLockSelection, ref this.lockSelection);
						}
					}
				}
			}

			// Token: 0x170003EB RID: 1003
			// (get) Token: 0x060013EC RID: 5100 RVA: 0x00090E84 File Offset: 0x0008FE84
			// (set) Token: 0x060013ED RID: 5101 RVA: 0x00090E8C File Offset: 0x0008FE8C
			public bool EditObjectAfterAdd
			{
				get
				{
					return this.editObjectAfterAdd;
				}
				set
				{
					if (this.editObjectAfterAdd != value)
					{
						this.InvokeChanging();
						bool oldEditObjectAfterAdd = this.editObjectAfterAdd;
						this.editObjectAfterAdd = value;
						this.InvokeChanged();
						if (this.active && this.EditObjectAfterAddChanged != null)
						{
							this.EditObjectAfterAddChanged(this, ref oldEditObjectAfterAdd, ref this.editObjectAfterAdd);
						}
					}
				}
			}

			// Token: 0x170003EC RID: 1004
			// (get) Token: 0x060013EE RID: 5102 RVA: 0x00090EE0 File Offset: 0x0008FEE0
			// (set) Token: 0x060013EF RID: 5103 RVA: 0x00090EE8 File Offset: 0x0008FEE8
			public bool AutolinkObjectAfterAdd
			{
				get
				{
					return this.autolinkObjectAfterAdd;
				}
				set
				{
					if (this.autolinkObjectAfterAdd != value)
					{
						this.InvokeChanging();
						bool oldAutolinkObjectAfterAdd = this.autolinkObjectAfterAdd;
						this.autolinkObjectAfterAdd = value;
						this.InvokeChanged();
						if (this.active && this.AutolinkObjectAfterAddChanged != null)
						{
							this.AutolinkObjectAfterAddChanged(this, ref oldAutolinkObjectAfterAdd, ref this.autolinkObjectAfterAdd);
						}
					}
				}
			}

			// Token: 0x170003ED RID: 1005
			// (get) Token: 0x060013F0 RID: 5104 RVA: 0x00090F3C File Offset: 0x0008FF3C
			// (set) Token: 0x060013F1 RID: 5105 RVA: 0x00090F44 File Offset: 0x0008FF44
			public bool CreateCrosslinks
			{
				get
				{
					return this.createCrosslinks;
				}
				set
				{
					if (this.createCrosslinks != value)
					{
						this.InvokeChanging();
						bool oldCreateCrosslinks = this.createCrosslinks;
						this.createCrosslinks = value;
						this.InvokeChanged();
						if (this.active && this.CreateCrosslinksChanged != null)
						{
							this.CreateCrosslinksChanged(this, ref oldCreateCrosslinks, ref this.createCrosslinks);
						}
					}
				}
			}

			// Token: 0x170003EE RID: 1006
			// (get) Token: 0x060013F2 RID: 5106 RVA: 0x00090F98 File Offset: 0x0008FF98
			// (set) Token: 0x060013F3 RID: 5107 RVA: 0x00090FA0 File Offset: 0x0008FFA0
			public int GridStepIndex
			{
				get
				{
					return this.gridStepIndex;
				}
				set
				{
					if (this.gridStepIndex != value)
					{
						this.InvokeChanging();
						int oldGridStepToGrid = this.gridStepIndex;
						this.gridStepIndex = value;
						this.InvokeChanged();
						if (this.active && this.GridStepIndexChanged != null)
						{
							this.GridStepIndexChanged(this, ref oldGridStepToGrid, ref this.gridStepIndex);
						}
					}
				}
			}

			// Token: 0x170003EF RID: 1007
			// (get) Token: 0x060013F4 RID: 5108 RVA: 0x00090FF4 File Offset: 0x0008FFF4
			// (set) Token: 0x060013F5 RID: 5109 RVA: 0x00090FFC File Offset: 0x0008FFFC
			public GroupContainer.GroupSelectionMode GroupSelectionMode
			{
				get
				{
					return this.groupSelectionMode;
				}
				set
				{
					this.groupSelectionMode = value;
				}
			}

			// Token: 0x170003F0 RID: 1008
			// (get) Token: 0x060013F6 RID: 5110 RVA: 0x00091005 File Offset: 0x00090005
			// (set) Token: 0x060013F7 RID: 5111 RVA: 0x00091038 File Offset: 0x00090038
			[XmlIgnore]
			public double GridStep
			{
				get
				{
					if (this.gridStepIndex >= 0 && this.gridStepIndex < MapObjectSelector.SelectorData.gridSteps.Length)
					{
						return MapObjectSelector.SelectorData.gridSteps[this.gridStepIndex];
					}
					return MapObjectSelector.SelectorData.gridSteps[MapObjectSelector.SelectorData.defaultGridStepIndex];
				}
				set
				{
					for (int index = 0; index < MapObjectSelector.SelectorData.gridSteps.Length; index++)
					{
						if (Math.Abs(MapObjectSelector.SelectorData.gridSteps[index] - value) < MathConsts.DOUBLE_EPSILON)
						{
							this.GridStepIndex = index;
							return;
						}
					}
				}
			}

			// Token: 0x170003F1 RID: 1009
			// (get) Token: 0x060013F8 RID: 5112 RVA: 0x00091074 File Offset: 0x00090074
			[XmlIgnore]
			public string GridStepText
			{
				get
				{
					if (this.gridStepIndex >= 0 && this.gridStepIndex < MapObjectSelector.SelectorData.gridStepTexts.Length)
					{
						return MapObjectSelector.SelectorData.gridStepTexts[this.gridStepIndex];
					}
					return MapObjectSelector.SelectorData.gridStepTexts[MapObjectSelector.SelectorData.defaultGridStepIndex];
				}
			}

			// Token: 0x170003F2 RID: 1010
			// (get) Token: 0x060013F9 RID: 5113 RVA: 0x000910A6 File Offset: 0x000900A6
			// (set) Token: 0x060013FA RID: 5114 RVA: 0x000910AE File Offset: 0x000900AE
			public double Scale
			{
				get
				{
					return this.scale;
				}
				set
				{
					this.ChangeScale(value);
				}
			}

			// Token: 0x060013FB RID: 5115 RVA: 0x000910B8 File Offset: 0x000900B8
			public bool SizeUpScale()
			{
				return this.ChangeScale(this.scale * MapObjectSelector.SelectorData.scaleMultiplier);
			}

			// Token: 0x060013FC RID: 5116 RVA: 0x000910CC File Offset: 0x000900CC
			public bool SizeDownScale()
			{
				return this.ChangeScale(this.scale / MapObjectSelector.SelectorData.scaleMultiplier);
			}

			// Token: 0x04000E12 RID: 3602
			private static readonly double maxScale = 256.0;

			// Token: 0x04000E13 RID: 3603
			private static readonly double minScale = 1.0;

			// Token: 0x04000E14 RID: 3604
			private static readonly double scaleMultiplier = 1.5;

			// Token: 0x04000E15 RID: 3605
			private static readonly double defaultScale = 4.0;

			// Token: 0x04000E16 RID: 3606
			private static readonly int defaultGridStepIndex = 4;

			// Token: 0x04000E17 RID: 3607
			private static readonly GroupContainer.GroupSelectionMode defaultGroupSelectionMode = GroupContainer.GroupSelectionMode.OneLevel;

			// Token: 0x04000E18 RID: 3608
			private static readonly double[] gridSteps = new double[]
			{
				0.0625,
				0.125,
				0.25,
				0.5,
				1.0,
				2.0,
				4.0,
				8.0,
				16.0
			};

			// Token: 0x04000E19 RID: 3609
			private static readonly string[] gridStepTexts = new string[]
			{
				"1/16",
				"1/8",
				"1/4",
				"1/2",
				"1",
				"2",
				"4",
				"8",
				"16"
			};

			// Token: 0x04000E1A RID: 3610
			private bool active;

			// Token: 0x04000E1B RID: 3611
			private MapObjectSelector.SelectorType selectorType = MapObjectSelector.SelectorType.Move;

			// Token: 0x04000E1C RID: 3612
			private MapObjectSelector.ActivePlane activePlane = MapObjectSelector.ActivePlane.Z;

			// Token: 0x04000E1D RID: 3613
			private MapObjectSelector.SelectedPart selectedPart;

			// Token: 0x04000E1E RID: 3614
			private bool objectOriented = true;

			// Token: 0x04000E1F RID: 3615
			private TerrainSurface surface = TerrainSurface.Terrain;

			// Token: 0x04000E20 RID: 3616
			private bool alignToGrid;

			// Token: 0x04000E21 RID: 3617
			private bool placeAlongNormal;

			// Token: 0x04000E22 RID: 3618
			private bool lockSelection;

			// Token: 0x04000E23 RID: 3619
			private bool editObjectAfterAdd;

			// Token: 0x04000E24 RID: 3620
			private bool autolinkObjectAfterAdd;

			// Token: 0x04000E25 RID: 3621
			private bool createCrosslinks;

			// Token: 0x04000E26 RID: 3622
			private double scale = MapObjectSelector.SelectorData.defaultScale;

			// Token: 0x04000E27 RID: 3623
			private int gridStepIndex = MapObjectSelector.SelectorData.defaultGridStepIndex;

			// Token: 0x04000E28 RID: 3624
			private GroupContainer.GroupSelectionMode groupSelectionMode = MapObjectSelector.SelectorData.defaultGroupSelectionMode;

			// Token: 0x02000198 RID: 408
			// (Invoke) Token: 0x06001400 RID: 5120
			public delegate void ChangeEvent(MapObjectSelector.SelectorData selectorData);

			// Token: 0x02000199 RID: 409
			// (Invoke) Token: 0x06001404 RID: 5124
			public delegate void FieldChangedEvent<T>(MapObjectSelector.SelectorData selectorData, ref T oldValue, ref T newValue);
		}

		// Token: 0x0200019A RID: 410
		private class WidgetData
		{
			// Token: 0x06001407 RID: 5127 RVA: 0x00091238 File Offset: 0x00090238
			private void CreateUserGeometry()
			{
				if (this.mapObject != null)
				{
					MultiMapObject multiMapObject = this.mapObject as MultiMapObject;
					if (multiMapObject != null && multiMapObject.MapObjects.Count == 0)
					{
						if (this.widgetID != -1)
						{
							this.DestroyUserGeometry();
						}
						return;
					}
					Position position = this.mapObject.Position;
					Rotation rotation = this.selectorData.ObjectOriented ? this.mapObject.Rotation : Rotation.Empty;
					Scale scale = this.mapObject.Scale * (float)this.selectorData.Scale;
					switch (this.selectorData.SelectorType)
					{
					default:
						this.DestroyUserGeometry();
						return;
					case MapObjectSelector.SelectorType.Move:
						this.widgetID = this.editorScene.CreateUserGeometry_MoveWidget(this.widgetID, ref position, ref rotation, ref scale, this.selectorData.ActivePlane == MapObjectSelector.ActivePlane.X, this.selectorData.ActivePlane == MapObjectSelector.ActivePlane.Y, this.selectorData.ActivePlane == MapObjectSelector.ActivePlane.Z, this.selectorData.SelectedPart == MapObjectSelector.SelectedPart.PlaneX, this.selectorData.SelectedPart == MapObjectSelector.SelectedPart.PlaneY, this.selectorData.SelectedPart == MapObjectSelector.SelectedPart.PlaneZ, this.selectorData.SelectedPart == MapObjectSelector.SelectedPart.AxisX, this.selectorData.SelectedPart == MapObjectSelector.SelectedPart.AxisY, this.selectorData.SelectedPart == MapObjectSelector.SelectedPart.AxisZ);
						return;
					case MapObjectSelector.SelectorType.Rotate:
						this.widgetID = this.editorScene.CreateUserGeometry_RotationWidget(this.widgetID, ref position, ref rotation, ref scale, this.selectorData.SelectedPart == MapObjectSelector.SelectedPart.PlaneX, this.selectorData.SelectedPart == MapObjectSelector.SelectedPart.PlaneY, this.selectorData.SelectedPart == MapObjectSelector.SelectedPart.PlaneZ, this.selectorData.SelectedPart == MapObjectSelector.SelectedPart.AxisX, this.selectorData.SelectedPart == MapObjectSelector.SelectedPart.AxisY, this.selectorData.SelectedPart == MapObjectSelector.SelectedPart.AxisZ);
						return;
					case MapObjectSelector.SelectorType.Scale:
						this.widgetID = this.editorScene.CreateUserGeometry_ScaleWidget(this.widgetID, ref position, ref rotation, ref scale, this.selectorData.SelectedPart == MapObjectSelector.SelectedPart.Center || this.selectorData.SelectedPart == MapObjectSelector.SelectedPart.AxisX, this.selectorData.SelectedPart == MapObjectSelector.SelectedPart.Center || this.selectorData.SelectedPart == MapObjectSelector.SelectedPart.AxisY, this.selectorData.SelectedPart == MapObjectSelector.SelectedPart.Center || this.selectorData.SelectedPart == MapObjectSelector.SelectedPart.AxisZ);
						break;
					}
				}
			}

			// Token: 0x06001408 RID: 5128 RVA: 0x00091478 File Offset: 0x00090478
			private void DestroyUserGeometry()
			{
				if (this.widgetID != -1)
				{
					this.editorScene.DeleteUserGeometry(this.widgetID);
					this.widgetID = -1;
				}
			}

			// Token: 0x06001409 RID: 5129 RVA: 0x0009149C File Offset: 0x0009049C
			private void OnPositionChanged(IMapObject _mapObject, ref Position oldValue, ref Position newValue)
			{
				if (this.widgetID != -1 && this.mapObject == _mapObject)
				{
					Position position = this.mapObject.Position;
					this.editorScene.MoveUserGeometry(this.widgetID, ref position);
				}
			}

			// Token: 0x0600140A RID: 5130 RVA: 0x000914DC File Offset: 0x000904DC
			private void OnRotationChanged(IMapObject _mapObject, ref Rotation oldValue, ref Rotation newValue)
			{
				if (this.widgetID != -1 && this.mapObject == _mapObject)
				{
					Rotation rotation = this.selectorData.ObjectOriented ? this.mapObject.Rotation : Rotation.Empty;
					this.editorScene.RotateUserGeometry(this.widgetID, ref rotation);
				}
			}

			// Token: 0x0600140B RID: 5131 RVA: 0x00091530 File Offset: 0x00090530
			private void OnScaleChanged(IMapObject _mapObject, ref Scale oldValue, ref Scale newValue)
			{
				if (this.widgetID != -1 && this.mapObject == _mapObject)
				{
					Scale scale = this.mapObject.Scale * (float)this.selectorData.Scale;
					this.editorScene.ScaleUserGeometry(this.widgetID, ref scale);
				}
			}

			// Token: 0x0600140C RID: 5132 RVA: 0x0009157F File Offset: 0x0009057F
			private void OnSelectorTypeChanged(MapObjectSelector.SelectorData _electorData, ref MapObjectSelector.SelectorType oldValue, ref MapObjectSelector.SelectorType newValue)
			{
				if (this.widgetID != -1)
				{
					this.DestroyUserGeometry();
					this.CreateUserGeometry();
				}
			}

			// Token: 0x0600140D RID: 5133 RVA: 0x00091596 File Offset: 0x00090596
			private void OnActivePlaneChanged(MapObjectSelector.SelectorData _electorData, ref MapObjectSelector.ActivePlane oldValue, ref MapObjectSelector.ActivePlane newValue)
			{
				if (this.widgetID != -1)
				{
					this.CreateUserGeometry();
				}
			}

			// Token: 0x0600140E RID: 5134 RVA: 0x000915A7 File Offset: 0x000905A7
			private void OnSelectedPartChanged(MapObjectSelector.SelectorData _electorData, ref MapObjectSelector.SelectedPart oldValue, ref MapObjectSelector.SelectedPart newValue)
			{
				if (this.widgetID != -1)
				{
					this.CreateUserGeometry();
				}
			}

			// Token: 0x0600140F RID: 5135 RVA: 0x000915B8 File Offset: 0x000905B8
			private void OnSelectorScaleChanged(MapObjectSelector.SelectorData _electorData, ref double oldValue, ref double newValue)
			{
				if (this.widgetID != -1)
				{
					Scale scale = this.mapObject.Scale * (float)this.selectorData.Scale;
					this.editorScene.ScaleUserGeometry(this.widgetID, ref scale);
				}
			}

			// Token: 0x06001410 RID: 5136 RVA: 0x00091600 File Offset: 0x00090600
			private void OnObjectOrientedChanged(MapObjectSelector.SelectorData _electorData, ref bool oldValue, ref bool newValue)
			{
				if (this.widgetID != -1)
				{
					Rotation rotation = this.selectorData.ObjectOriented ? this.mapObject.Rotation : Rotation.Empty;
					this.editorScene.RotateUserGeometry(this.widgetID, ref rotation);
				}
			}

			// Token: 0x06001411 RID: 5137 RVA: 0x0009164C File Offset: 0x0009064C
			private void OnMapObjectAdded(MultiMapObject multiMapObject, IMapObject _mapObject)
			{
				if (multiMapObject.MapObjects.Count > 0)
				{
					if (this.widgetID == -1)
					{
						this.CreateUserGeometry();
						return;
					}
					Position position = this.mapObject.Position;
					Rotation rotation = this.selectorData.ObjectOriented ? this.mapObject.Rotation : Rotation.Empty;
					Scale scale = this.mapObject.Scale * (float)this.selectorData.Scale;
					this.editorScene.MoveUserGeometry(this.widgetID, ref position);
					this.editorScene.RotateUserGeometry(this.widgetID, ref rotation);
					this.editorScene.ScaleUserGeometry(this.widgetID, ref scale);
				}
			}

			// Token: 0x06001412 RID: 5138 RVA: 0x000916FC File Offset: 0x000906FC
			private void OnMapObjectRemoved(MultiMapObject multiMapObject, IMapObject _mapObject)
			{
				if (multiMapObject.MapObjects.Count == 0 && this.widgetID != -1)
				{
					this.DestroyUserGeometry();
				}
			}

			// Token: 0x06001413 RID: 5139 RVA: 0x0009171A File Offset: 0x0009071A
			private void OnMapObjectListCleared(MultiMapObject multiMapObject, Dictionary<IMapObject, IMapObject> mapObjects)
			{
				if (multiMapObject.MapObjects.Count == 0 && this.widgetID != -1)
				{
					this.DestroyUserGeometry();
				}
			}

			// Token: 0x06001414 RID: 5140 RVA: 0x00091738 File Offset: 0x00090738
			private void OnMapObjectListSyncronized(MultiMapObject multiMapObject, List<IMapObject> mapObjectsRemoved, List<IMapObject> mapObjectsAdded)
			{
				if (multiMapObject.MapObjects.Count == 0)
				{
					if (this.widgetID != -1)
					{
						this.DestroyUserGeometry();
						return;
					}
				}
				else if (multiMapObject.MapObjects.Count > 0)
				{
					if (this.widgetID == -1)
					{
						this.CreateUserGeometry();
						return;
					}
					Position position = this.mapObject.Position;
					Rotation rotation = this.selectorData.ObjectOriented ? this.mapObject.Rotation : Rotation.Empty;
					Scale scale = this.mapObject.Scale * (float)this.selectorData.Scale;
					this.editorScene.MoveUserGeometry(this.widgetID, ref position);
					this.editorScene.RotateUserGeometry(this.widgetID, ref rotation);
					this.editorScene.ScaleUserGeometry(this.widgetID, ref scale);
				}
			}

			// Token: 0x06001415 RID: 5141 RVA: 0x00091808 File Offset: 0x00090808
			public WidgetData(EditorScene _editorScene)
			{
				this.editorScene = _editorScene;
			}

			// Token: 0x170003F3 RID: 1011
			// (get) Token: 0x06001416 RID: 5142 RVA: 0x0009181E File Offset: 0x0009081E
			public bool Created
			{
				get
				{
					return this.widgetID != -1;
				}
			}

			// Token: 0x06001417 RID: 5143 RVA: 0x0009182C File Offset: 0x0009082C
			public void Bind(MapObjectSelector.SelectorData _selectorData, IMapObject _mapObject)
			{
				this.Unbind();
				this.selectorData = _selectorData;
				if (this.selectorData != null)
				{
					this.selectorData.SelectorTypeChanged += this.OnSelectorTypeChanged;
					this.selectorData.ActivePlaneChanged += this.OnActivePlaneChanged;
					this.selectorData.SelectedPartChanged += this.OnSelectedPartChanged;
					this.selectorData.ScaleChanged += this.OnSelectorScaleChanged;
					this.selectorData.ObjectOrientedChanged += this.OnObjectOrientedChanged;
				}
				this.mapObject = _mapObject;
				if (this.mapObject != null)
				{
					MultiMapObject multiMapObject = this.mapObject as MultiMapObject;
					if (multiMapObject != null)
					{
						multiMapObject.MapObjectAdded += this.OnMapObjectAdded;
						multiMapObject.MapObjectRemoved += this.OnMapObjectRemoved;
						multiMapObject.MapObjectListCleared += this.OnMapObjectListCleared;
						multiMapObject.MapObjectListSyncronized += this.OnMapObjectListSyncronized;
						multiMapObject.PositionChanged += this.OnPositionChanged;
						multiMapObject.RotationChanged += this.OnRotationChanged;
						multiMapObject.ScaleChanged += this.OnScaleChanged;
					}
				}
				this.CreateUserGeometry();
			}

			// Token: 0x06001418 RID: 5144 RVA: 0x00091968 File Offset: 0x00090968
			public void Unbind()
			{
				this.DestroyUserGeometry();
				if (this.selectorData != null)
				{
					this.selectorData.SelectorTypeChanged -= this.OnSelectorTypeChanged;
					this.selectorData.ActivePlaneChanged -= this.OnActivePlaneChanged;
					this.selectorData.SelectedPartChanged -= this.OnSelectedPartChanged;
					this.selectorData.ScaleChanged -= this.OnSelectorScaleChanged;
					this.selectorData.ObjectOrientedChanged -= this.OnObjectOrientedChanged;
				}
				if (this.mapObject != null)
				{
					MultiMapObject multiMapObject = this.mapObject as MultiMapObject;
					if (multiMapObject != null)
					{
						multiMapObject.MapObjectAdded -= this.OnMapObjectAdded;
						multiMapObject.MapObjectRemoved -= this.OnMapObjectRemoved;
						multiMapObject.MapObjectListCleared -= this.OnMapObjectListCleared;
						multiMapObject.PositionChanged -= this.OnPositionChanged;
						multiMapObject.RotationChanged -= this.OnRotationChanged;
						multiMapObject.ScaleChanged -= this.OnScaleChanged;
					}
				}
				this.selectorData = null;
				this.mapObject = null;
			}

			// Token: 0x04000E38 RID: 3640
			private readonly EditorScene editorScene;

			// Token: 0x04000E39 RID: 3641
			private MapObjectSelector.SelectorData selectorData;

			// Token: 0x04000E3A RID: 3642
			private IMapObject mapObject;

			// Token: 0x04000E3B RID: 3643
			private int widgetID = -1;
		}

		// Token: 0x0200019B RID: 411
		// (Invoke) Token: 0x0600141A RID: 5146
		public delegate void SelectorChangedEvent(MapObjectSelector mapObjectSelector);

		// Token: 0x0200019C RID: 412
		// (Invoke) Token: 0x0600141E RID: 5150
		public delegate void SelectorFieldChangedEvent<T>(MapObjectSelector mapObjectSelector, ref T oldValue, ref T newValue);
	}
}
