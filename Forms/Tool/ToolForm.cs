using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using InputState;
using LauncherTools.InputState;
using MapEditor.Forms.Base;
using MapEditor.Map;
using MapEditor.Map.DataProviders;
using MapEditor.Map.MapObjects;
using MapEditor.Map.States;
using Tools.ItemDataContainer;
using Tools.Landscape;
using Tools.Landscape.LandscapeToolParams;
using Tools.MainState;
using Tools.MapObjects;
using Tools.WindowParams;
using Win32;

namespace MapEditor.Forms.Tool
{
	// Token: 0x02000013 RID: 19
	public partial class ToolForm : BaseForm, ILandscapeParams, IObjectsStateParams, IMapObjectRandomizer, IMapObjectFilter
	{
		// Token: 0x06000066 RID: 102 RVA: 0x00003F80 File Offset: 0x00002F80
		private void CreateLandscapeParams()
		{
			this.landscapeRegionParams = Serializer.Load<LandscapeRegionParams>(EditorEnvironment.EditorFormsFolder + "ToolForm/LandscapeRegionParams.xml");
			if (this.landscapeRegionParams == null)
			{
				this.landscapeRegionParams = new LandscapeRegionParams();
			}
			this.landscapeToolParamsContainer.Load(EditorEnvironment.EditorFormsFolder + "ToolForm/LandscapeToolParams");
			this.CreateLandscapeItemLists();
			this.BindLandscapeRegionControls();
			this.BindLandscapeToolParamsControls();
		}

		// Token: 0x06000067 RID: 103 RVA: 0x00003FE8 File Offset: 0x00002FE8
		private void DestroyLandscapeParams()
		{
			this.landscapeToolParamsContainer.Save(EditorEnvironment.EditorFormsFolder + "ToolForm/LandscapeToolParams");
			Serializer.Save(EditorEnvironment.EditorFormsFolder + "ToolForm/LandscapeRegionParams.xml", this.landscapeRegionParams, false);
			this.UnbindLandscapeRegionControls();
			this.UnbindLandscapeToolParamsControls();
			this.DestroyLandscapeItemLists();
		}

		// Token: 0x06000068 RID: 104 RVA: 0x00004040 File Offset: 0x00003040
		private void CreateLandscapeTabControls()
		{
			this.LandscapeSubstateLeftButton00.Tag = 0;
			this.LandscapeSubstateLeftButton01.Tag = 1;
			this.LandscapeSubstateLeftButton02.Tag = 2;
			this.LandscapeSubstateLeftButton03.Tag = 3;
			this.LandscapeSubstateLeftButton04.Tag = 4;
			this.LandscapeSubstateLeftButton05.Tag = 5;
			this.LandscapeSubstateLeftButton06.Tag = 6;
			this.LandscapeSubstateLeftButton07.Tag = 7;
			this.LandscapeSubstateLeftButton08.Tag = 8;
			this.LandscapeSubstateLeftButton09.Tag = 9;
			this.LandscapeSubstateRightButton00.Tag = 0;
			this.LandscapeSubstateRightButton01.Tag = 1;
			this.LandscapeSubstateRightButton02.Tag = 2;
			this.LandscapeSubstateRightButton03.Tag = 3;
			this.LandscapeSubstateRightButton04.Tag = 4;
			this.LandscapeSubstateRightButton05.Tag = 5;
			this.LandscapeSubstateRightButton06.Tag = 6;
			this.LandscapeSubstateRightButton07.Tag = 7;
			this.LandscapeSubstateRightButton08.Tag = 8;
			this.LandscapeSubstateRightButton09.Tag = 9;
			this.LandscapeSubstateLeftButton00.CheckedChanged += this.OnSubstateRadioButtonLeftCheckedChanged;
			this.LandscapeSubstateLeftButton01.CheckedChanged += this.OnSubstateRadioButtonLeftCheckedChanged;
			this.LandscapeSubstateLeftButton02.CheckedChanged += this.OnSubstateRadioButtonLeftCheckedChanged;
			this.LandscapeSubstateLeftButton03.CheckedChanged += this.OnSubstateRadioButtonLeftCheckedChanged;
			this.LandscapeSubstateLeftButton04.CheckedChanged += this.OnSubstateRadioButtonLeftCheckedChanged;
			this.LandscapeSubstateLeftButton05.CheckedChanged += this.OnSubstateRadioButtonLeftCheckedChanged;
			this.LandscapeSubstateLeftButton06.CheckedChanged += this.OnSubstateRadioButtonLeftCheckedChanged;
			this.LandscapeSubstateLeftButton07.CheckedChanged += this.OnSubstateRadioButtonLeftCheckedChanged;
			this.LandscapeSubstateLeftButton08.CheckedChanged += this.OnSubstateRadioButtonLeftCheckedChanged;
			this.LandscapeSubstateLeftButton09.CheckedChanged += this.OnSubstateRadioButtonLeftCheckedChanged;
			this.LandscapeSubstateRightButton00.CheckedChanged += this.OnSubstateRadioButtonRightCheckedChanged;
			this.LandscapeSubstateRightButton01.CheckedChanged += this.OnSubstateRadioButtonRightCheckedChanged;
			this.LandscapeSubstateRightButton02.CheckedChanged += this.OnSubstateRadioButtonRightCheckedChanged;
			this.LandscapeSubstateRightButton03.CheckedChanged += this.OnSubstateRadioButtonRightCheckedChanged;
			this.LandscapeSubstateRightButton04.CheckedChanged += this.OnSubstateRadioButtonRightCheckedChanged;
			this.LandscapeSubstateRightButton05.CheckedChanged += this.OnSubstateRadioButtonRightCheckedChanged;
			this.LandscapeSubstateRightButton06.CheckedChanged += this.OnSubstateRadioButtonRightCheckedChanged;
			this.LandscapeSubstateRightButton07.CheckedChanged += this.OnSubstateRadioButtonRightCheckedChanged;
			this.LandscapeSubstateRightButton08.CheckedChanged += this.OnSubstateRadioButtonRightCheckedChanged;
			this.LandscapeSubstateRightButton09.CheckedChanged += this.OnSubstateRadioButtonRightCheckedChanged;
			this.landscapeToolLeftButtons.Add(this.LandscapeSubstateLeftButton00);
			this.landscapeToolLeftButtons.Add(this.LandscapeSubstateLeftButton01);
			this.landscapeToolLeftButtons.Add(this.LandscapeSubstateLeftButton02);
			this.landscapeToolLeftButtons.Add(this.LandscapeSubstateLeftButton03);
			this.landscapeToolLeftButtons.Add(this.LandscapeSubstateLeftButton04);
			this.landscapeToolLeftButtons.Add(this.LandscapeSubstateLeftButton05);
			this.landscapeToolLeftButtons.Add(this.LandscapeSubstateLeftButton06);
			this.landscapeToolLeftButtons.Add(this.LandscapeSubstateLeftButton07);
			this.landscapeToolLeftButtons.Add(this.LandscapeSubstateLeftButton08);
			this.landscapeToolLeftButtons.Add(this.LandscapeSubstateLeftButton09);
			this.landscapeToolRightButtons.Add(this.LandscapeSubstateRightButton00);
			this.landscapeToolRightButtons.Add(this.LandscapeSubstateRightButton01);
			this.landscapeToolRightButtons.Add(this.LandscapeSubstateRightButton02);
			this.landscapeToolRightButtons.Add(this.LandscapeSubstateRightButton03);
			this.landscapeToolRightButtons.Add(this.LandscapeSubstateRightButton04);
			this.landscapeToolRightButtons.Add(this.LandscapeSubstateRightButton05);
			this.landscapeToolRightButtons.Add(this.LandscapeSubstateRightButton06);
			this.landscapeToolRightButtons.Add(this.LandscapeSubstateRightButton07);
			this.landscapeToolRightButtons.Add(this.LandscapeSubstateRightButton08);
			this.landscapeToolRightButtons.Add(this.LandscapeSubstateRightButton09);
			this.tilePickerControlElements.Add(new ToolForm.TilePickerControlElement(this.LTPTilePickerRadioButton00, this.LTPTilePickerPictureBox00, this.LTPTilePickerLabel00));
			this.tilePickerControlElements.Add(new ToolForm.TilePickerControlElement(this.LTPTilePickerRadioButton01, this.LTPTilePickerPictureBox01, this.LTPTilePickerLabel01));
			this.tilePickerControlElements.Add(new ToolForm.TilePickerControlElement(this.LTPTilePickerRadioButton02, this.LTPTilePickerPictureBox02, this.LTPTilePickerLabel02));
			this.tilePickerControlElements.Add(new ToolForm.TilePickerControlElement(this.LTPTilePickerRadioButton03, this.LTPTilePickerPictureBox03, this.LTPTilePickerLabel03));
			this.CreateLandscapeParams();
			base.Context.MainState.ActiveSubstateChanged += this.OnLandscapeMainStateActiveSubstateChanged;
			this.leftLandscapeSimpleTimer.StartTimer += this.OnStartLeftLandscapeFormTimer;
			this.leftLandscapeSimpleTimer.StopTimer += this.OnStopLeftLandscapeFormTimer;
			this.rightLandscapeSimpleTimer.StartTimer += this.OnStartRightLandscapeFormTimer;
			this.rightLandscapeSimpleTimer.StopTimer += this.OnStopRightLandscapeFormTimer;
		}

		// Token: 0x06000069 RID: 105 RVA: 0x000045CC File Offset: 0x000035CC
		private void DestroyLandscapeTabControls()
		{
			this.leftLandscapeSimpleTimer.StartTimer -= this.OnStartLeftLandscapeFormTimer;
			this.leftLandscapeSimpleTimer.StopTimer -= this.OnStopLeftLandscapeFormTimer;
			this.rightLandscapeSimpleTimer.StartTimer -= this.OnStartRightLandscapeFormTimer;
			this.rightLandscapeSimpleTimer.StopTimer -= this.OnStopRightLandscapeFormTimer;
			base.Context.MainState.ActiveSubstateChanged -= this.OnLandscapeMainStateActiveSubstateChanged;
			this.DestroyLandscapeParams();
			this.tilePickerControlElements.Clear();
			this.landscapeToolLeftButtons.Clear();
			this.landscapeToolRightButtons.Clear();
		}

		// Token: 0x0600006A RID: 106 RVA: 0x00004678 File Offset: 0x00003678
		private void UpdateLandscapeTabControls()
		{
			this.UpdateActiveLandscapeToolParamsIndexControls(-1);
			this.UpdateLandscapeRegionControls();
			this.UpdateLandscapeToolParamsControls();
		}

		// Token: 0x0600006B RID: 107 RVA: 0x00004690 File Offset: 0x00003690
		private void UpdateActiveLandscapeToolParamsIndexControls(int oldIndex)
		{
			if (this.created)
			{
				int newIndex = -1;
				if (ToolForm.landscapeMainStateIndex >= 0 && ToolForm.landscapeMainStateIndex < base.Context.MainState.ActiveSubstates.Count && base.Context.MainState.ActiveSubstates[ToolForm.landscapeMainStateIndex] >= 0 && base.Context.MainState.ActiveSubstates[ToolForm.landscapeMainStateIndex] < this.landscapeToolParamsContainer.Count * 2)
				{
					newIndex = base.Context.MainState.ActiveSubstates[ToolForm.landscapeMainStateIndex];
				}
				this.created = false;
				if (oldIndex != -1)
				{
					this.landscapeToolLeftButtons[oldIndex % this.landscapeToolParamsContainer.Count].Checked = false;
					this.landscapeToolRightButtons[oldIndex % this.landscapeToolParamsContainer.Count].Checked = false;
				}
				if (newIndex != -1)
				{
					this.leftLandscapeToolBinded = (newIndex < this.landscapeToolParamsContainer.Count);
					this.landscapeToolLeftButtons[newIndex % this.landscapeToolParamsContainer.Count].Checked = this.leftLandscapeToolBinded;
					this.landscapeToolRightButtons[newIndex % this.landscapeToolParamsContainer.Count].Checked = !this.leftLandscapeToolBinded;
				}
				this.created = true;
			}
		}

		// Token: 0x0600006C RID: 108 RVA: 0x000047E0 File Offset: 0x000037E0
		private void OnSubstateRadioButtonLeftCheckedChanged(object sender, EventArgs e)
		{
			if (this.created)
			{
				RadioButton radioButton = sender as RadioButton;
				if (radioButton != null && radioButton.Checked && radioButton.Tag is int)
				{
					this.UnbindLandscapeToolParamsControls();
					int substateIndex = (int)radioButton.Tag;
					this.created = false;
					this.landscapeToolRightButtons[substateIndex].Checked = true;
					this.landscapeToolRightButtons[substateIndex].Checked = false;
					IntPtr previousFocusedForm = new IntPtr(User.GetFocus());
					this.leftLandscapeToolBinded = true;
					base.Context.MainState.ActiveSubstate = (this.leftLandscapeToolBinded ? substateIndex : (substateIndex + this.landscapeToolParamsContainer.Count));
					User.SetFocus(previousFocusedForm);
					this.created = true;
					this.BindLandscapeToolParamsControls();
					this.UpdateLandscapeToolParamsControls();
				}
			}
		}

		// Token: 0x0600006D RID: 109 RVA: 0x000048B4 File Offset: 0x000038B4
		private void OnSubstateRadioButtonRightCheckedChanged(object sender, EventArgs e)
		{
			if (this.created)
			{
				RadioButton radioButton = sender as RadioButton;
				if (radioButton != null && radioButton.Checked && radioButton.Tag is int)
				{
					this.UnbindLandscapeToolParamsControls();
					int substateIndex = (int)radioButton.Tag;
					this.created = false;
					this.landscapeToolLeftButtons[substateIndex].Checked = true;
					this.landscapeToolLeftButtons[substateIndex].Checked = false;
					IntPtr previousFocusedForm = new IntPtr(User.GetFocus());
					this.leftLandscapeToolBinded = false;
					base.Context.MainState.ActiveSubstate = (this.leftLandscapeToolBinded ? substateIndex : (substateIndex + this.landscapeToolParamsContainer.Count));
					User.SetFocus(previousFocusedForm);
					this.created = true;
					this.BindLandscapeToolParamsControls();
					this.UpdateLandscapeToolParamsControls();
				}
			}
		}

		// Token: 0x0600006E RID: 110 RVA: 0x00004986 File Offset: 0x00003986
		private void OnLandscapeMainStateActiveSubstateChanged(MainState _mainState, ref int oldValue, ref int newValue)
		{
			if (base.Context.MainState.ActiveState == ToolForm.landscapeMainStateIndex && this.created)
			{
				this.UnbindLandscapeToolParamsControls();
				this.UpdateActiveLandscapeToolParamsIndexControls(oldValue);
				this.BindLandscapeToolParamsControls();
				this.UpdateLandscapeToolParamsControls();
			}
		}

		// Token: 0x0600006F RID: 111 RVA: 0x000049C4 File Offset: 0x000039C4
		private void BindLandscapeRegionControls()
		{
			this.landscapeRegionParams.TypeChanged += this.OnLandscapeRegionParamsTypeChanged;
			this.landscapeRegionParams.SideChanged += this.OnLandscapeRegionParamsSideChanged;
			this.landscapeRegionParams.SizeChanged += this.OnLandscapeRegionParamsSizeChanged;
			this.landscapeRegionParams.TerrainNumberChanged += this.OnLandscapeRegionParamsTerrainNumberChanged;
			this.landscapeRegionParams.InvertHeightToolsChanged += this.OnLandscapeRegionParamsInvertHeightToolsChanged;
		}

		// Token: 0x06000070 RID: 112 RVA: 0x00004A44 File Offset: 0x00003A44
		private void UnbindLandscapeRegionControls()
		{
			this.landscapeRegionParams.TypeChanged -= this.OnLandscapeRegionParamsTypeChanged;
			this.landscapeRegionParams.SideChanged -= this.OnLandscapeRegionParamsSideChanged;
			this.landscapeRegionParams.SizeChanged -= this.OnLandscapeRegionParamsSizeChanged;
			this.landscapeRegionParams.TerrainNumberChanged -= this.OnLandscapeRegionParamsTerrainNumberChanged;
			this.landscapeRegionParams.InvertHeightToolsChanged -= this.OnLandscapeRegionParamsInvertHeightToolsChanged;
		}

		// Token: 0x06000071 RID: 113 RVA: 0x00004AC4 File Offset: 0x00003AC4
		private void UpdateLandscapeRegionFromLandscapeToolParams()
		{
			if (this.created && this.LandscapeUseSeparateRegionsCheckBox.Checked)
			{
				ILandscapeToolParams landscapeToolParams = this.landscapeToolParamsContainer.Get(this.leftLandscapeToolBinded, this.ActiveLandscapeToolParamsIndex);
				if (landscapeToolParams != null)
				{
					this.landscapeRegionParams.RegionUpdated = false;
					this.landscapeRegionParams.Type = landscapeToolParams.LandscapeRegionType;
					this.landscapeRegionParams.Side = landscapeToolParams.LandscapeRegionSide;
					this.landscapeRegionParams.Size = landscapeToolParams.LandscapeRegionSize;
					this.landscapeRegionParams.RegionUpdated = true;
				}
			}
		}

		// Token: 0x06000072 RID: 114 RVA: 0x00004B4C File Offset: 0x00003B4C
		private void UpdateLandscapeRegionControls()
		{
			this.UpdateLandscapeRegionParamsType();
			this.UpdateLandscapeRegionParamsSide();
			this.UpdateLandscapeRegionParamsSize();
			this.UpdateLandscapeRegionParamsTerrainNumber();
			this.UpdateLandscapeRegionParamsInvertHeightTools();
		}

		// Token: 0x06000073 RID: 115 RVA: 0x00004B6C File Offset: 0x00003B6C
		private void UpdateLandscapeRegionParamsType()
		{
			if (this.created)
			{
				this.created = false;
				this.LandscapeRegionShapeButton00.Checked = (this.landscapeRegionParams.Type == LandscapeRegionType.Ellipse);
				this.LandscapeRegionShapeButton01.Checked = (this.landscapeRegionParams.Type == LandscapeRegionType.Square);
				this.LandscapeRegionShapeButton02.Checked = (this.landscapeRegionParams.Type == LandscapeRegionType.Polygon);
				this.LandscapeRegionShapeButton03.Checked = (this.landscapeRegionParams.Type == LandscapeRegionType.Stripe);
				this.LandscapeRegionShapeButton04.Checked = (this.landscapeRegionParams.Type == LandscapeRegionType.AllMap);
				this.created = true;
			}
		}

		// Token: 0x06000074 RID: 116 RVA: 0x00004C10 File Offset: 0x00003C10
		private void UpdateLandscapeRegionParamsSide()
		{
			if (this.created)
			{
				this.created = false;
				this.LandscapeRegionSideButton00.Checked = (this.landscapeRegionParams.Side == LandscapeRegionSide.Left);
				this.LandscapeRegionSideButton01.Checked = (this.landscapeRegionParams.Side == LandscapeRegionSide.Right);
				this.LandscapeRegionSideButton02.Checked = (this.landscapeRegionParams.Side == LandscapeRegionSide.Both);
				this.created = true;
			}
		}

		// Token: 0x06000075 RID: 117 RVA: 0x00004C80 File Offset: 0x00003C80
		private void UpdateLandscapeRegionParamsSize()
		{
			if (this.created)
			{
				this.created = false;
				this.LandscapeRegionSizeEditBox.Text = (this.landscapeRegionParams.Size - 1).ToString();
				this.LandscapeRegionSizeTrackBar.Value = this.landscapeRegionParams.Size - 1;
				this.created = true;
			}
		}

		// Token: 0x06000076 RID: 118 RVA: 0x00004CDC File Offset: 0x00003CDC
		private void UpdateLandscapeRegionParamsTerrainNumber()
		{
			if (this.created)
			{
				this.created = false;
				this.LandscapeTerrainNumberButton00.Checked = (this.landscapeRegionParams.TerrainNumber == 0);
				this.LandscapeTerrainNumberButton01.Checked = (this.landscapeRegionParams.TerrainNumber == 1);
				this.created = true;
			}
		}

		// Token: 0x06000077 RID: 119 RVA: 0x00004D31 File Offset: 0x00003D31
		private void UpdateLandscapeRegionParamsInvertHeightTools()
		{
			if (this.created)
			{
				this.created = false;
				this.LandscapeInvertHeightToolsCheckBox.Checked = this.landscapeRegionParams.InvertHeightTools;
				this.created = true;
			}
		}

		// Token: 0x06000078 RID: 120 RVA: 0x00004D60 File Offset: 0x00003D60
		private void OnLandscapeRegionParamsTypeChanged(LandscapeRegionParams _landscapeRegionParams, ref LandscapeRegionType oldValue, ref LandscapeRegionType newValue)
		{
			this.UpdateLandscapeRegionParamsType();
			if (this.landscapeRegionParams.RegionUpdated)
			{
				ILandscapeToolParams landscapeToolParams = this.landscapeToolParamsContainer.Get(this.leftLandscapeToolBinded, this.ActiveLandscapeToolParamsIndex);
				if (landscapeToolParams != null)
				{
					landscapeToolParams.LandscapeRegionType = this.landscapeRegionParams.Type;
				}
			}
		}

		// Token: 0x06000079 RID: 121 RVA: 0x00004DAC File Offset: 0x00003DAC
		private void OnLandscapeRegionParamsSideChanged(LandscapeRegionParams _landscapeRegionParams, ref LandscapeRegionSide oldValue, ref LandscapeRegionSide newValue)
		{
			this.UpdateLandscapeRegionParamsSide();
			if (this.landscapeRegionParams.RegionUpdated)
			{
				ILandscapeToolParams landscapeToolParams = this.landscapeToolParamsContainer.Get(this.leftLandscapeToolBinded, this.ActiveLandscapeToolParamsIndex);
				if (landscapeToolParams != null)
				{
					landscapeToolParams.LandscapeRegionSide = this.landscapeRegionParams.Side;
				}
			}
		}

		// Token: 0x0600007A RID: 122 RVA: 0x00004DF8 File Offset: 0x00003DF8
		private void OnLandscapeRegionParamsSizeChanged(LandscapeRegionParams _landscapeRegionParams, ref int oldValue, ref int newValue)
		{
			this.UpdateLandscapeRegionParamsSize();
			if (this.landscapeRegionParams.RegionUpdated)
			{
				ILandscapeToolParams landscapeToolParams = this.landscapeToolParamsContainer.Get(this.leftLandscapeToolBinded, this.ActiveLandscapeToolParamsIndex);
				if (landscapeToolParams != null)
				{
					landscapeToolParams.LandscapeRegionSize = this.landscapeRegionParams.Size;
				}
			}
		}

		// Token: 0x0600007B RID: 123 RVA: 0x00004E44 File Offset: 0x00003E44
		private void OnLandscapeRegionParamsTerrainNumberChanged(LandscapeRegionParams _landscapeRegionParams, ref int oldValue, ref int newValue)
		{
			this.UpdateLandscapeRegionParamsTerrainNumber();
		}

		// Token: 0x0600007C RID: 124 RVA: 0x00004E4C File Offset: 0x00003E4C
		private void OnLandscapeRegionParamsInvertHeightToolsChanged(LandscapeRegionParams _landscapeRegionParams, ref bool oldValue, ref bool newValue)
		{
			this.UpdateLandscapeRegionParamsInvertHeightTools();
		}

		// Token: 0x0600007D RID: 125 RVA: 0x00004E54 File Offset: 0x00003E54
		private void OnSwitchTerrain(MethodArgs methodArgs)
		{
			if (this.created)
			{
				this.landscapeRegionParams.TerrainNumber = 0;
			}
		}

		// Token: 0x0600007E RID: 126 RVA: 0x00004E6A File Offset: 0x00003E6A
		private void OnSwitchBottom(MethodArgs methodArgs)
		{
			if (this.created)
			{
				this.landscapeRegionParams.TerrainNumber = 1;
			}
		}

		// Token: 0x0600007F RID: 127 RVA: 0x00004E80 File Offset: 0x00003E80
		private void OnNewWaterLayerAdded(MethodArgs methodArgs)
		{
			if (this.landscapeWaterItemList != null && this.landscapeWaterItemList.Binded)
			{
				DBID layerDBID = methodArgs.sender as DBID;
				if (layerDBID != null)
				{
					foreach (ItemList.IItemSource itemSource in this.landscapeWaterItemList.ItemSources)
					{
						LandscapeWaterItemSource waterItemSource = itemSource as LandscapeWaterItemSource;
						if (waterItemSource != null && waterItemSource.LayerDBID == layerDBID)
						{
							waterItemSource.Refresh();
							this.landscapeWaterItemList.Refresh();
						}
					}
				}
			}
		}

		// Token: 0x06000080 RID: 128 RVA: 0x00004F28 File Offset: 0x00003F28
		private void LandscapeRegionShapeButton00_CheckedChanged(object sender, EventArgs e)
		{
			if (this.created && this.LandscapeRegionShapeButton00.Checked)
			{
				this.created = false;
				this.landscapeRegionParams.Type = LandscapeRegionType.Ellipse;
				this.created = true;
			}
		}

		// Token: 0x06000081 RID: 129 RVA: 0x00004F59 File Offset: 0x00003F59
		private void LandscapeRegionShapeButton01_CheckedChanged(object sender, EventArgs e)
		{
			if (this.created && this.LandscapeRegionShapeButton01.Checked)
			{
				this.created = false;
				this.landscapeRegionParams.Type = LandscapeRegionType.Square;
				this.created = true;
			}
		}

		// Token: 0x06000082 RID: 130 RVA: 0x00004F8A File Offset: 0x00003F8A
		private void LandscapeRegionShapeButton02_CheckedChanged(object sender, EventArgs e)
		{
			if (this.created && this.LandscapeRegionShapeButton02.Checked)
			{
				this.created = false;
				this.landscapeRegionParams.Type = LandscapeRegionType.Polygon;
				this.created = true;
			}
		}

		// Token: 0x06000083 RID: 131 RVA: 0x00004FBB File Offset: 0x00003FBB
		private void LandscapeRegionShapeButton03_CheckedChanged(object sender, EventArgs e)
		{
			if (this.created && this.LandscapeRegionShapeButton03.Checked)
			{
				this.created = false;
				this.landscapeRegionParams.Type = LandscapeRegionType.Stripe;
				this.created = true;
			}
		}

		// Token: 0x06000084 RID: 132 RVA: 0x00004FEC File Offset: 0x00003FEC
		private void LandscapeRegionShapeButton04_CheckedChanged(object sender, EventArgs e)
		{
			if (this.created && this.LandscapeRegionShapeButton04.Checked)
			{
				this.created = false;
				this.landscapeRegionParams.Type = LandscapeRegionType.AllMap;
				this.created = true;
			}
		}

		// Token: 0x06000085 RID: 133 RVA: 0x00005020 File Offset: 0x00004020
		private void LandscapeRegionSizeEditBox_TextChanged(object sender, EventArgs e)
		{
			if (this.created)
			{
				int size;
				if (int.TryParse(this.LandscapeRegionSizeEditBox.Text, out size))
				{
					this.created = false;
					this.landscapeRegionParams.Size = size + 1;
					this.created = true;
				}
				this.editBoxForUpdateNames[this.LandscapeRegionSizeEditBox.Name] = 0;
				this.EditBoxTimer.Start();
			}
		}

		// Token: 0x06000086 RID: 134 RVA: 0x00005087 File Offset: 0x00004087
		private void LandscapeRegionSizeTrackBar_Scroll(object sender, EventArgs e)
		{
			if (this.created)
			{
				this.created = false;
				this.landscapeRegionParams.Size = this.LandscapeRegionSizeTrackBar.Value + 1;
				this.created = true;
				this.UpdateLandscapeRegionParamsSize();
			}
		}

		// Token: 0x06000087 RID: 135 RVA: 0x000050BD File Offset: 0x000040BD
		private void LandscapeTerrainNumberButton00_CheckedChanged(object sender, EventArgs e)
		{
			if (this.created && this.LandscapeTerrainNumberButton00.Checked)
			{
				this.created = false;
				this.landscapeRegionParams.TerrainNumber = 0;
				this.created = true;
			}
		}

		// Token: 0x06000088 RID: 136 RVA: 0x000050EE File Offset: 0x000040EE
		private void LandscapeTerrainNumberButton01_CheckedChanged(object sender, EventArgs e)
		{
			if (this.created && this.LandscapeTerrainNumberButton01.Checked)
			{
				this.created = false;
				this.landscapeRegionParams.TerrainNumber = 1;
				this.created = true;
			}
		}

		// Token: 0x06000089 RID: 137 RVA: 0x0000511F File Offset: 0x0000411F
		private void LandscapeInvertHeightToolsCheckBox_CheckedChanged(object sender, EventArgs e)
		{
			if (this.created)
			{
				this.created = false;
				this.landscapeRegionParams.InvertHeightTools = this.LandscapeInvertHeightToolsCheckBox.Checked;
				this.created = true;
			}
		}

		// Token: 0x0600008A RID: 138 RVA: 0x0000514D File Offset: 0x0000414D
		private void LandscapeRegionSideButton00_CheckedChanged(object sender, EventArgs e)
		{
			if (this.created && this.LandscapeRegionSideButton00.Checked)
			{
				this.created = false;
				this.landscapeRegionParams.Side = LandscapeRegionSide.Left;
				this.created = true;
			}
		}

		// Token: 0x0600008B RID: 139 RVA: 0x0000517E File Offset: 0x0000417E
		private void LandscapeRegionSideButton01_CheckedChanged(object sender, EventArgs e)
		{
			if (this.created && this.LandscapeRegionSideButton01.Checked)
			{
				this.created = false;
				this.landscapeRegionParams.Side = LandscapeRegionSide.Right;
				this.created = true;
			}
		}

		// Token: 0x0600008C RID: 140 RVA: 0x000051AF File Offset: 0x000041AF
		private void LandscapeRegionSideButton02_CheckedChanged(object sender, EventArgs e)
		{
			if (this.created && this.LandscapeRegionSideButton02.Checked)
			{
				this.created = false;
				this.landscapeRegionParams.Side = LandscapeRegionSide.Both;
				this.created = true;
			}
		}

		// Token: 0x0600008D RID: 141 RVA: 0x000051E0 File Offset: 0x000041E0
		private void CreateLandscapeItemLists()
		{
			this.landscapeTileFolderItemFiltes = new FolderItemFilters(EditorEnvironment.EditorFolder + "Filters/ToolForm/LandscapeTileFilters.xml", EditorEnvironment.EditorFormsFolder);
			this.landscapeHeightFolderItemFiltes = new FolderItemFilters(EditorEnvironment.EditorFolder + "Filters/ToolForm/LandscapeHeightFilters.xml", EditorEnvironment.EditorFormsFolder);
			this.landscapeClipboardFolderItemFiltes = new FolderItemFilters(EditorEnvironment.EditorFolder + "Filters/ToolForm/LandscapeClipboardFilters.xml", EditorEnvironment.EditorFormsFolder);
			this.landscapeHillFolderItemFiltes = new FolderItemFilters(EditorEnvironment.EditorFolder + "Filters/ToolForm/LandscapeHillPresets.xml", EditorEnvironment.EditorFormsFolder);
			this.landscapeRoadFolderItemFiltes = new FolderItemFilters(EditorEnvironment.EditorFolder + "Filters/ToolForm/LandscapeRoadPresets.xml", EditorEnvironment.EditorFormsFolder);
			this.landscapeWaterFolderItemFiltes = new FolderItemFilters(EditorEnvironment.EditorFolder + "Filters/ToolForm/LandscapeWaterFilters.xml", EditorEnvironment.EditorFormsFolder);
			this.landscapeTileItemList = new ItemList(EditorEnvironment.EditorFormsFolder + "ToolForm/LandscapeTileItemList.xml", base.Context.ItemDataContainer);
			this.landscapeHeightItemList = new ItemList(EditorEnvironment.EditorFormsFolder + "ToolForm/LandscapeHeightItemList.xml", base.Context.ItemDataContainer);
			this.landscapeClipboardItemList = new ItemList(EditorEnvironment.EditorFormsFolder + "ToolForm/LandscapeClipboardItemList.xml", base.Context.ItemDataContainer);
			this.landscapeHillItemList = new ItemList(EditorEnvironment.EditorFormsFolder + "ToolForm/LandscapeHillItemList.xml", base.Context.ItemDataContainer);
			this.landscapeRoadItemList = new ItemList(EditorEnvironment.EditorFormsFolder + "ToolForm/LandscapeRoadItemList.xml", base.Context.ItemDataContainer);
			this.landscapeWaterItemList = new ItemList(EditorEnvironment.EditorFormsFolder + "ToolForm/LandscapeWaterItemList.xml", base.Context.ItemDataContainer);
			this.landscapeTileItemList.FilterItemData = true;
			ItemList itemList = this.landscapeTileItemList;
			itemList.ItemSelected = (ItemList.ItemEvent)Delegate.Combine(itemList.ItemSelected, new ItemList.ItemEvent(this.OnLandscapeTileItemListItemSelected));
			ItemList itemList2 = this.landscapeTileItemList;
			itemList2.ItemUnselected = (ItemList.ItemEvent)Delegate.Combine(itemList2.ItemUnselected, new ItemList.ItemEvent(this.OnLandscapeTileItemListItemUnselected));
			ItemList itemList3 = this.landscapeTileItemList;
			itemList3.ListCleared = (ItemList.ItemEvent)Delegate.Combine(itemList3.ListCleared, new ItemList.ItemEvent(this.OnLandscapeTileItemListItemListCleared));
			ItemList itemList4 = this.landscapeTileItemList;
			itemList4.ListRefreshing = (ItemList.ItemEvent)Delegate.Combine(itemList4.ListRefreshing, new ItemList.ItemEvent(this.OnTileItemListRefreshing));
			ItemList itemList5 = this.landscapeHeightItemList;
			itemList5.ItemSelected = (ItemList.ItemEvent)Delegate.Combine(itemList5.ItemSelected, new ItemList.ItemEvent(this.OnLandscapeHeightItemListItemSelected));
			ItemList itemList6 = this.landscapeHeightItemList;
			itemList6.ItemUnselected = (ItemList.ItemEvent)Delegate.Combine(itemList6.ItemUnselected, new ItemList.ItemEvent(this.OnLandscapeHeightItemListItemUnselected));
			ItemList itemList7 = this.landscapeHeightItemList;
			itemList7.ListCleared = (ItemList.ItemEvent)Delegate.Combine(itemList7.ListCleared, new ItemList.ItemEvent(this.OnLandscapeHeightItemListItemListCleared));
			ItemList itemList8 = this.landscapeClipboardItemList;
			itemList8.ItemSelected = (ItemList.ItemEvent)Delegate.Combine(itemList8.ItemSelected, new ItemList.ItemEvent(this.OnLandscapeClipboardItemListItemSelected));
			ItemList itemList9 = this.landscapeClipboardItemList;
			itemList9.ItemUnselected = (ItemList.ItemEvent)Delegate.Combine(itemList9.ItemUnselected, new ItemList.ItemEvent(this.OnLandscapeClipboardItemListItemUnselected));
			ItemList itemList10 = this.landscapeClipboardItemList;
			itemList10.ListCleared = (ItemList.ItemEvent)Delegate.Combine(itemList10.ListCleared, new ItemList.ItemEvent(this.OnLandscapeClipboardItemListItemListCleared));
			ItemList itemList11 = this.landscapeHillItemList;
			itemList11.ItemSelected = (ItemList.ItemEvent)Delegate.Combine(itemList11.ItemSelected, new ItemList.ItemEvent(this.OnLandscapeHillItemListItemSelected));
			ItemList itemList12 = this.landscapeRoadItemList;
			itemList12.ItemSelected = (ItemList.ItemEvent)Delegate.Combine(itemList12.ItemSelected, new ItemList.ItemEvent(this.OnLandscapeRoadItemListItemSelected));
			ItemList itemList13 = this.landscapeRoadItemList;
			itemList13.ItemDoubleClicked = (ItemList.ItemEvent)Delegate.Combine(itemList13.ItemDoubleClicked, new ItemList.ItemEvent(this.OnLandscapeRoadItemListItemDoubleClicked));
			ItemList itemList14 = this.landscapeWaterItemList;
			itemList14.ItemDoubleClicked = (ItemList.ItemEvent)Delegate.Combine(itemList14.ItemDoubleClicked, new ItemList.ItemEvent(this.OnLandscapeWaterItemListItemDoubleClicked));
			this.landscapeWaterItemList.FilterItemData = true;
			ItemList itemList15 = this.landscapeWaterItemList;
			itemList15.ItemSelected = (ItemList.ItemEvent)Delegate.Combine(itemList15.ItemSelected, new ItemList.ItemEvent(this.OnLandscapeWaterItemListItemSelected));
			ItemList itemList16 = this.landscapeWaterItemList;
			itemList16.ItemUnselected = (ItemList.ItemEvent)Delegate.Combine(itemList16.ItemUnselected, new ItemList.ItemEvent(this.OnLandscapeWaterItemListItemUnselected));
			ItemList itemList17 = this.landscapeWaterItemList;
			itemList17.ListCleared = (ItemList.ItemEvent)Delegate.Combine(itemList17.ListCleared, new ItemList.ItemEvent(this.OnLandscapeWaterItemListItemListCleared));
			this.landscapeHeightItemList.ItemSources.Add(new LandscapeProfileItemSource());
			this.landscapeHeightItemList.ItemSources.Add(new LandscapeHeightmapItemSource());
			this.landscapeClipboardItemList.ItemSources.Add(new LandscapeClipboardItemSource());
			this.landscapeHillItemList.ItemSources.Add(new LandscapeHillItemSource());
			this.landscapeRoadItemList.ItemSources.Add(new LandscapeRoadItemSource());
			this.landscapeTileFolderItemFiltes.Bind(this.landscapeTileItemList.ItemFilters);
			this.landscapeHeightFolderItemFiltes.Bind(this.landscapeHeightItemList.ItemFilters);
			this.landscapeClipboardFolderItemFiltes.Bind(this.landscapeClipboardItemList.ItemFilters);
			this.landscapeHillFolderItemFiltes.Bind(this.landscapeHillItemList.ItemFilters);
			this.landscapeRoadFolderItemFiltes.Bind(this.landscapeRoadItemList.ItemFilters);
			this.landscapeWaterFolderItemFiltes.Bind(this.landscapeWaterItemList.ItemFilters);
			this.landscapeTileItemList.Bind(this.LTPTileBrushListView, this.LTPTileBrushComboBox);
			this.landscapeHeightItemList.Bind(this.LTPHeightBrushListView, this.LTPHeightBrushComboBox);
			this.landscapeClipboardItemList.Bind(this.LTPClipboardBrushListView, this.LTPClipboardBrushComboBox);
			this.landscapeHillItemList.Bind(this.LTPHillPresetListView, this.LTPHillPresetComboBox);
			this.landscapeRoadItemList.Bind(this.LTPRoadPresetListView, this.LTPRoadPresetComboBox);
			this.landscapeWaterItemList.Bind(this.LTPWaterBrushListView, this.LTPWaterBrushComboBox);
		}

		// Token: 0x0600008E RID: 142 RVA: 0x000057AC File Offset: 0x000047AC
		private void DestroyLandscapeItemLists()
		{
			this.landscapeTileItemList.Unbind();
			this.landscapeHeightItemList.Unbind();
			this.landscapeClipboardItemList.Unbind();
			this.landscapeHillItemList.Unbind();
			this.landscapeRoadItemList.Unbind();
			this.landscapeWaterItemList.Unbind();
			this.landscapeTileFolderItemFiltes.Unbind();
			this.landscapeHeightFolderItemFiltes.Unbind();
			this.landscapeClipboardFolderItemFiltes.Unbind();
			this.landscapeHillFolderItemFiltes.Unbind();
			this.landscapeRoadFolderItemFiltes.Unbind();
			this.landscapeWaterFolderItemFiltes.Unbind();
			this.landscapeHeightItemList.ItemSources.Clear();
			this.landscapeClipboardItemList.ItemSources.Clear();
			this.landscapeHillItemList.ItemSources.Clear();
			this.landscapeRoadItemList.ItemSources.Clear();
			ItemList itemList = this.landscapeTileItemList;
			itemList.ItemSelected = (ItemList.ItemEvent)Delegate.Remove(itemList.ItemSelected, new ItemList.ItemEvent(this.OnLandscapeTileItemListItemSelected));
			ItemList itemList2 = this.landscapeTileItemList;
			itemList2.ItemUnselected = (ItemList.ItemEvent)Delegate.Remove(itemList2.ItemUnselected, new ItemList.ItemEvent(this.OnLandscapeTileItemListItemUnselected));
			ItemList itemList3 = this.landscapeTileItemList;
			itemList3.ListCleared = (ItemList.ItemEvent)Delegate.Remove(itemList3.ListCleared, new ItemList.ItemEvent(this.OnLandscapeTileItemListItemListCleared));
			ItemList itemList4 = this.landscapeHeightItemList;
			itemList4.ItemSelected = (ItemList.ItemEvent)Delegate.Remove(itemList4.ItemSelected, new ItemList.ItemEvent(this.OnLandscapeHeightItemListItemSelected));
			ItemList itemList5 = this.landscapeHeightItemList;
			itemList5.ItemUnselected = (ItemList.ItemEvent)Delegate.Remove(itemList5.ItemUnselected, new ItemList.ItemEvent(this.OnLandscapeHeightItemListItemUnselected));
			ItemList itemList6 = this.landscapeHeightItemList;
			itemList6.ListCleared = (ItemList.ItemEvent)Delegate.Remove(itemList6.ListCleared, new ItemList.ItemEvent(this.OnLandscapeHeightItemListItemListCleared));
			ItemList itemList7 = this.landscapeClipboardItemList;
			itemList7.ItemSelected = (ItemList.ItemEvent)Delegate.Remove(itemList7.ItemSelected, new ItemList.ItemEvent(this.OnLandscapeClipboardItemListItemSelected));
			ItemList itemList8 = this.landscapeClipboardItemList;
			itemList8.ItemUnselected = (ItemList.ItemEvent)Delegate.Remove(itemList8.ItemUnselected, new ItemList.ItemEvent(this.OnLandscapeClipboardItemListItemUnselected));
			ItemList itemList9 = this.landscapeClipboardItemList;
			itemList9.ListCleared = (ItemList.ItemEvent)Delegate.Remove(itemList9.ListCleared, new ItemList.ItemEvent(this.OnLandscapeClipboardItemListItemListCleared));
			ItemList itemList10 = this.landscapeHillItemList;
			itemList10.ItemSelected = (ItemList.ItemEvent)Delegate.Remove(itemList10.ItemSelected, new ItemList.ItemEvent(this.OnLandscapeHillItemListItemSelected));
			ItemList itemList11 = this.landscapeRoadItemList;
			itemList11.ItemSelected = (ItemList.ItemEvent)Delegate.Remove(itemList11.ItemSelected, new ItemList.ItemEvent(this.OnLandscapeRoadItemListItemSelected));
			ItemList itemList12 = this.landscapeRoadItemList;
			itemList12.ItemDoubleClicked = (ItemList.ItemEvent)Delegate.Remove(itemList12.ItemDoubleClicked, new ItemList.ItemEvent(this.OnLandscapeRoadItemListItemDoubleClicked));
			this.landscapeTileItemList = null;
			this.landscapeHeightItemList = null;
			this.landscapeClipboardItemList = null;
			this.landscapeHillItemList = null;
			this.landscapeRoadItemList = null;
			this.landscapeWaterItemList = null;
			this.landscapeTileFolderItemFiltes = null;
			this.landscapeHeightFolderItemFiltes = null;
			this.landscapeClipboardFolderItemFiltes = null;
			this.landscapeHillFolderItemFiltes = null;
			this.landscapeRoadFolderItemFiltes = null;
			this.landscapeWaterFolderItemFiltes = null;
		}

		// Token: 0x0600008F RID: 143 RVA: 0x00005AA8 File Offset: 0x00004AA8
		private void UnbindLandscapeToolParamsControls()
		{
			if (this.bindedLandscapeToolParamsIndex != -1)
			{
				ILandscapeToolParams landscapeToolParams = this.landscapeToolParamsContainer.Get(this.leftLandscapeToolBinded, this.bindedLandscapeToolParamsIndex);
				if (landscapeToolParams != null)
				{
					if (landscapeToolParams.LandscapeToolType == LandscapeToolType.Tile)
					{
						TileLandscapeToolParams tileLandscapeToolParams = landscapeToolParams as TileLandscapeToolParams;
						if (tileLandscapeToolParams != null)
						{
							tileLandscapeToolParams.AngleRestrictionsParams.BeginAngleChanged -= this.OnTileToolAngleRestrictionsBeginAngleChanged;
							tileLandscapeToolParams.AngleRestrictionsParams.EndAngleChanged -= this.OnTileToolAngleRestrictionsEndAngleChanged;
							tileLandscapeToolParams.AngleRestrictionsParams.UseAngleRestrictionsChanged -= this.OnTileToolAngleRestrictionsUseAngleRestrictionsChanged;
							tileLandscapeToolParams.StrengthSmoothParams.StrengthChanged -= this.OnTileToolStrengthSmoothStrengthChanged;
							tileLandscapeToolParams.StrengthSmoothParams.SmoothChanged -= this.OnTileToolStrengthSmoothSmoothChanged;
							tileLandscapeToolParams.AutomaticToolChanged -= this.OnTileToolAutomaticToolChanged;
							tileLandscapeToolParams.SpotToolChanged -= this.OnTileToolSpotToolChanged;
							tileLandscapeToolParams.ReplaceToolChanged -= this.OnTileToolReplaceToolChanged;
							tileLandscapeToolParams.TileForPaintChanged -= this.OnTileToolTileForPaintChanged;
							tileLandscapeToolParams.TileForReplaceChanged -= this.OnTileToolTileForReplaceChanged;
						}
					}
					else if (landscapeToolParams.LandscapeToolType == LandscapeToolType.TilePicker)
					{
						TilePickerLandscapeToolParams tilePickerLandscapeToolParams = landscapeToolParams as TilePickerLandscapeToolParams;
						if (tilePickerLandscapeToolParams != null)
						{
							tilePickerLandscapeToolParams.SelectedIndexChanged -= this.OnTilePickerToolSelectedIndexChanged;
							tilePickerLandscapeToolParams.TilesChanged -= this.OnTilePickerToolTilesChanged;
						}
					}
					else if (landscapeToolParams.LandscapeToolType != LandscapeToolType.Gradient)
					{
						if (landscapeToolParams.LandscapeToolType == LandscapeToolType.Water)
						{
							WaterLandscapeToolParams waterLandscapeToolParams = landscapeToolParams as WaterLandscapeToolParams;
							if (waterLandscapeToolParams != null)
							{
								waterLandscapeToolParams.WaterToolTypeChanged -= this.OnWaterToolWaterToolTypeChanged;
								waterLandscapeToolParams.HeightChanged -= this.OnWaterToolHeightChanged;
								waterLandscapeToolParams.LayerIndexChanged -= this.OnWaterToolLayerIndexChanged;
							}
						}
						else if (landscapeToolParams.LandscapeToolType == LandscapeToolType.Height)
						{
							HeightLandscapeToolParams heightLandscapeToolParams = landscapeToolParams as HeightLandscapeToolParams;
							if (heightLandscapeToolParams != null)
							{
								heightLandscapeToolParams.StrengthSmoothParams.StrengthChanged -= this.OnHeightToolStrengthSmoothStrengthChanged;
								heightLandscapeToolParams.StrengthSmoothParams.SmoothChanged -= this.OnHeightToolStrengthSmoothSmoothChanged;
								heightLandscapeToolParams.UpdateObjectHeightsParams.UpdateChanged -= this.OnHeightToolUpdateObjectHeightsUpdateChanged;
								heightLandscapeToolParams.UpdateObjectHeightsParams.HeightRangeChanged -= this.OnHeightToolUpdateObjectHeightsHeightRangeChanged;
								heightLandscapeToolParams.HeightToolTypeChanged -= this.OnHeightToolHeightToolTypeChanged;
								heightLandscapeToolParams.HeightPlatoToolTypeChanged -= this.OnHeightToolHeightPlatoToolTypeChanged;
								heightLandscapeToolParams.HeightChanged -= this.OnHeightToolHeightChanged;
								heightLandscapeToolParams.PreciseToolChanged -= this.OnHeightToolPreciseToolChanged;
								heightLandscapeToolParams.AutomaticToolChanged -= this.OnHeightToolAutomaticToolChanged;
								heightLandscapeToolParams.FileNameChanged -= this.OnHeightToolFileNameChanged;
							}
						}
						else if (landscapeToolParams.LandscapeToolType != LandscapeToolType.HeightPicker)
						{
							if (landscapeToolParams.LandscapeToolType == LandscapeToolType.Clipboard)
							{
								ClipboardLandscapeToolParams clipboardLandscapeToolParams = landscapeToolParams as ClipboardLandscapeToolParams;
								if (clipboardLandscapeToolParams != null)
								{
									clipboardLandscapeToolParams.StrengthSmoothParams.StrengthChanged -= this.OnClipboardToolStrengthSmoothStrengthChanged;
									clipboardLandscapeToolParams.StrengthSmoothParams.SmoothChanged -= this.OnClipboardToolStrengthSmoothSmoothChanged;
									clipboardLandscapeToolParams.UpdateObjectHeightsParams.UpdateChanged -= this.OClipboardToolUpdateObjectHeightsUpdateChanged;
									clipboardLandscapeToolParams.UpdateObjectHeightsParams.HeightRangeChanged -= this.OnClipboardToolUpdateObjectHeightsHeightRangeChanged;
									clipboardLandscapeToolParams.TwoSidedChanged -= this.OnClipboardToolTwoSidedChanged;
									clipboardLandscapeToolParams.CopyTilesChanged -= this.OnClipboardToolCopyTilesChanged;
									clipboardLandscapeToolParams.CopyHeightsChanged -= this.OnClipboardToolCopyHeightsChanged;
									clipboardLandscapeToolParams.CopyObjectsChanged -= this.OnClipboardToolCopyObjectsChanged;
									clipboardLandscapeToolParams.CopyHeightTypeChanged -= this.OnClipboardToolCopyHeightTypeChanged;
									clipboardLandscapeToolParams.PreciseToolChanged -= this.OnClipboardToolPreciseToolChanged;
									clipboardLandscapeToolParams.FlipHorisontalChanged -= this.OnClipboardToolFlipHorisontalChanged;
									clipboardLandscapeToolParams.FlipVerticalChanged -= this.OnClipboardToolFlipVerticalChanged;
									clipboardLandscapeToolParams.FileNameChanged -= this.OnClipboardToolFileNameChanged;
									clipboardLandscapeToolParams.GroupChanged -= this.OnClipboardToolGroupChanged;
								}
							}
							else if (landscapeToolParams.LandscapeToolType == LandscapeToolType.Hill)
							{
								HillLandscapeToolParams hillLandscapeToolParams = landscapeToolParams as HillLandscapeToolParams;
								if (hillLandscapeToolParams != null)
								{
									hillLandscapeToolParams.TwoSidedChanged -= this.OnHillLandscapeToolParamsTwoSidedChanged;
									this.LTPHillPropertyGrid00.SelectedObject = null;
									this.LTPHillPropertyGrid01.SelectedObject = null;
								}
							}
							else if (landscapeToolParams.LandscapeToolType == LandscapeToolType.Road)
							{
								RoadLandscapeToolParams roadLandscapeToolParams = landscapeToolParams as RoadLandscapeToolParams;
								if (roadLandscapeToolParams != null)
								{
									this.LTPRoadPropertyGrid.SelectedObject = null;
								}
							}
						}
					}
				}
				this.bindedLandscapeToolParamsIndex = -1;
			}
		}

		// Token: 0x06000090 RID: 144 RVA: 0x00005F0C File Offset: 0x00004F0C
		private void BindLandscapeToolParamsControls()
		{
			if (this.bindedLandscapeToolParamsIndex != -1)
			{
				this.UnbindLandscapeToolParamsControls();
			}
			this.bindedLandscapeToolParamsIndex = this.ActiveLandscapeToolParamsIndex;
			this.leftLandscapeToolBinded = (base.Context.MainState.ActiveSubstates[ToolForm.landscapeMainStateIndex] < this.landscapeToolParamsContainer.Count);
			ILandscapeToolParams landscapeToolParams = this.landscapeToolParamsContainer.Get(this.leftLandscapeToolBinded, this.bindedLandscapeToolParamsIndex);
			if (landscapeToolParams != null)
			{
				if (landscapeToolParams.LandscapeToolType == LandscapeToolType.Tile)
				{
					TileLandscapeToolParams tileLandscapeToolParams = landscapeToolParams as TileLandscapeToolParams;
					if (tileLandscapeToolParams != null)
					{
						tileLandscapeToolParams.AngleRestrictionsParams.BeginAngleChanged += this.OnTileToolAngleRestrictionsBeginAngleChanged;
						tileLandscapeToolParams.AngleRestrictionsParams.EndAngleChanged += this.OnTileToolAngleRestrictionsEndAngleChanged;
						tileLandscapeToolParams.AngleRestrictionsParams.UseAngleRestrictionsChanged += this.OnTileToolAngleRestrictionsUseAngleRestrictionsChanged;
						tileLandscapeToolParams.StrengthSmoothParams.StrengthChanged += this.OnTileToolStrengthSmoothStrengthChanged;
						tileLandscapeToolParams.StrengthSmoothParams.SmoothChanged += this.OnTileToolStrengthSmoothSmoothChanged;
						tileLandscapeToolParams.AutomaticToolChanged -= this.OnTileToolAutomaticToolChanged;
						tileLandscapeToolParams.SpotToolChanged += this.OnTileToolSpotToolChanged;
						tileLandscapeToolParams.ReplaceToolChanged += this.OnTileToolReplaceToolChanged;
						tileLandscapeToolParams.TileForPaintChanged += this.OnTileToolTileForPaintChanged;
						tileLandscapeToolParams.TileForReplaceChanged += this.OnTileToolTileForReplaceChanged;
					}
				}
				else if (landscapeToolParams.LandscapeToolType == LandscapeToolType.TilePicker)
				{
					TilePickerLandscapeToolParams tilePickerLandscapeToolParams = landscapeToolParams as TilePickerLandscapeToolParams;
					if (tilePickerLandscapeToolParams != null)
					{
						tilePickerLandscapeToolParams.SelectedIndexChanged += this.OnTilePickerToolSelectedIndexChanged;
						tilePickerLandscapeToolParams.TilesChanged += this.OnTilePickerToolTilesChanged;
					}
				}
				else if (landscapeToolParams.LandscapeToolType != LandscapeToolType.Gradient)
				{
					if (landscapeToolParams.LandscapeToolType == LandscapeToolType.Water)
					{
						WaterLandscapeToolParams waterLandscapeToolParams = landscapeToolParams as WaterLandscapeToolParams;
						if (waterLandscapeToolParams != null)
						{
							waterLandscapeToolParams.WaterToolTypeChanged += this.OnWaterToolWaterToolTypeChanged;
							waterLandscapeToolParams.HeightChanged += this.OnWaterToolHeightChanged;
							waterLandscapeToolParams.LayerIndexChanged += this.OnWaterToolLayerIndexChanged;
						}
					}
					else if (landscapeToolParams.LandscapeToolType == LandscapeToolType.Height)
					{
						HeightLandscapeToolParams heightLandscapeToolParams = landscapeToolParams as HeightLandscapeToolParams;
						if (heightLandscapeToolParams != null)
						{
							heightLandscapeToolParams.StrengthSmoothParams.StrengthChanged += this.OnHeightToolStrengthSmoothStrengthChanged;
							heightLandscapeToolParams.StrengthSmoothParams.SmoothChanged += this.OnHeightToolStrengthSmoothSmoothChanged;
							heightLandscapeToolParams.UpdateObjectHeightsParams.UpdateChanged += this.OnHeightToolUpdateObjectHeightsUpdateChanged;
							heightLandscapeToolParams.UpdateObjectHeightsParams.HeightRangeChanged += this.OnHeightToolUpdateObjectHeightsHeightRangeChanged;
							heightLandscapeToolParams.HeightToolTypeChanged += this.OnHeightToolHeightToolTypeChanged;
							heightLandscapeToolParams.HeightPlatoToolTypeChanged += this.OnHeightToolHeightPlatoToolTypeChanged;
							heightLandscapeToolParams.HeightChanged += this.OnHeightToolHeightChanged;
							heightLandscapeToolParams.PreciseToolChanged += this.OnHeightToolPreciseToolChanged;
							heightLandscapeToolParams.AutomaticToolChanged += this.OnHeightToolAutomaticToolChanged;
							heightLandscapeToolParams.FileNameChanged += this.OnHeightToolFileNameChanged;
						}
					}
					else if (landscapeToolParams.LandscapeToolType != LandscapeToolType.HeightPicker)
					{
						if (landscapeToolParams.LandscapeToolType == LandscapeToolType.Clipboard)
						{
							ClipboardLandscapeToolParams clipboardLandscapeToolParams = landscapeToolParams as ClipboardLandscapeToolParams;
							if (clipboardLandscapeToolParams != null)
							{
								clipboardLandscapeToolParams.StrengthSmoothParams.StrengthChanged += this.OnClipboardToolStrengthSmoothStrengthChanged;
								clipboardLandscapeToolParams.StrengthSmoothParams.SmoothChanged += this.OnClipboardToolStrengthSmoothSmoothChanged;
								clipboardLandscapeToolParams.UpdateObjectHeightsParams.UpdateChanged += this.OClipboardToolUpdateObjectHeightsUpdateChanged;
								clipboardLandscapeToolParams.UpdateObjectHeightsParams.HeightRangeChanged += this.OnClipboardToolUpdateObjectHeightsHeightRangeChanged;
								clipboardLandscapeToolParams.TwoSidedChanged += this.OnClipboardToolTwoSidedChanged;
								clipboardLandscapeToolParams.CopyTilesChanged += this.OnClipboardToolCopyTilesChanged;
								clipboardLandscapeToolParams.CopyHeightsChanged += this.OnClipboardToolCopyHeightsChanged;
								clipboardLandscapeToolParams.CopyObjectsChanged += this.OnClipboardToolCopyObjectsChanged;
								clipboardLandscapeToolParams.CopyHeightTypeChanged += this.OnClipboardToolCopyHeightTypeChanged;
								clipboardLandscapeToolParams.PreciseToolChanged += this.OnClipboardToolPreciseToolChanged;
								clipboardLandscapeToolParams.FlipHorisontalChanged += this.OnClipboardToolFlipHorisontalChanged;
								clipboardLandscapeToolParams.FlipVerticalChanged += this.OnClipboardToolFlipVerticalChanged;
								clipboardLandscapeToolParams.FileNameChanged += this.OnClipboardToolFileNameChanged;
								clipboardLandscapeToolParams.GroupChanged += this.OnClipboardToolGroupChanged;
							}
						}
						else if (landscapeToolParams.LandscapeToolType == LandscapeToolType.Hill)
						{
							HillLandscapeToolParams hillLandscapeToolParams = landscapeToolParams as HillLandscapeToolParams;
							if (hillLandscapeToolParams != null)
							{
								hillLandscapeToolParams.TwoSidedChanged += this.OnHillLandscapeToolParamsTwoSidedChanged;
								this.LTPHillPropertyGrid00.SelectedObject = hillLandscapeToolParams.TerrainHillParams;
								this.LTPHillPropertyGrid01.SelectedObject = hillLandscapeToolParams.BottomHillParams;
							}
						}
						else if (landscapeToolParams.LandscapeToolType == LandscapeToolType.Road)
						{
							RoadLandscapeToolParams roadLandscapeToolParams = landscapeToolParams as RoadLandscapeToolParams;
							if (roadLandscapeToolParams != null)
							{
								this.LTPRoadPropertyGrid.SelectedObject = roadLandscapeToolParams.RoadParams;
							}
						}
					}
				}
				ILandscapeToolParams leftLandscapeToolParams = this.landscapeToolParamsContainer.Get(true, this.bindedLandscapeToolParamsIndex);
				ILandscapeToolParams rightLandscapeToolParams = this.landscapeToolParamsContainer.Get(false, this.bindedLandscapeToolParamsIndex);
				if (leftLandscapeToolParams.LandscapeToolType == LandscapeToolType.TilePicker)
				{
					TilePickerLandscapeToolParams tilePickerLandscapeToolParams2 = landscapeToolParams as TilePickerLandscapeToolParams;
					if (tilePickerLandscapeToolParams2 != null)
					{
						tilePickerLandscapeToolParams2.LandscapeToolTileCallback = (this.landscapeToolParamsContainer.Get(false, this.bindedLandscapeToolParamsIndex) as ILandscapeToolTileCallback);
					}
				}
				else if (leftLandscapeToolParams.LandscapeToolType == LandscapeToolType.HeightPicker)
				{
					HeightPickerLandscapeToolParams heightPickerLandscapeToolParams = landscapeToolParams as HeightPickerLandscapeToolParams;
					if (heightPickerLandscapeToolParams != null)
					{
						heightPickerLandscapeToolParams.LandscapeToolHeightCallback = (this.landscapeToolParamsContainer.Get(false, this.bindedLandscapeToolParamsIndex) as ILandscapeToolHeightCallback);
					}
				}
				if (rightLandscapeToolParams.LandscapeToolType == LandscapeToolType.TilePicker)
				{
					TilePickerLandscapeToolParams tilePickerLandscapeToolParams3 = landscapeToolParams as TilePickerLandscapeToolParams;
					if (tilePickerLandscapeToolParams3 != null)
					{
						tilePickerLandscapeToolParams3.LandscapeToolTileCallback = (this.landscapeToolParamsContainer.Get(true, this.bindedLandscapeToolParamsIndex) as ILandscapeToolTileCallback);
						return;
					}
				}
				else if (rightLandscapeToolParams.LandscapeToolType == LandscapeToolType.HeightPicker)
				{
					HeightPickerLandscapeToolParams heightPickerLandscapeToolParams2 = landscapeToolParams as HeightPickerLandscapeToolParams;
					if (heightPickerLandscapeToolParams2 != null)
					{
						heightPickerLandscapeToolParams2.LandscapeToolHeightCallback = (this.landscapeToolParamsContainer.Get(true, this.bindedLandscapeToolParamsIndex) as ILandscapeToolHeightCallback);
					}
				}
			}
		}

		// Token: 0x06000091 RID: 145 RVA: 0x000064B0 File Offset: 0x000054B0
		private static int GetTabIndexByLandscapeToolType(LandscapeToolType landscapeToolType)
		{
			if (landscapeToolType == LandscapeToolType.Tile)
			{
				return 0;
			}
			if (landscapeToolType == LandscapeToolType.Gradient)
			{
				return 1;
			}
			if (landscapeToolType == LandscapeToolType.Water)
			{
				return 2;
			}
			if (landscapeToolType == LandscapeToolType.Height)
			{
				return 3;
			}
			if (landscapeToolType == LandscapeToolType.Clipboard)
			{
				return 4;
			}
			if (landscapeToolType == LandscapeToolType.TilePicker)
			{
				return 5;
			}
			if (landscapeToolType == LandscapeToolType.HeightPicker)
			{
				return 6;
			}
			if (landscapeToolType == LandscapeToolType.Hill)
			{
				return 7;
			}
			if (landscapeToolType == LandscapeToolType.Road)
			{
				return 8;
			}
			return -1;
		}

		// Token: 0x06000092 RID: 146 RVA: 0x000064EA File Offset: 0x000054EA
		private static LandscapeToolType GetLandscapeToolTypeByTabIndex(int index)
		{
			if (index == 0)
			{
				return LandscapeToolType.Tile;
			}
			if (index == 1)
			{
				return LandscapeToolType.Gradient;
			}
			if (index == 2)
			{
				return LandscapeToolType.Water;
			}
			if (index == 3)
			{
				return LandscapeToolType.Height;
			}
			if (index == 4)
			{
				return LandscapeToolType.Clipboard;
			}
			if (index == 5)
			{
				return LandscapeToolType.TilePicker;
			}
			if (index == 6)
			{
				return LandscapeToolType.HeightPicker;
			}
			if (index == 7)
			{
				return LandscapeToolType.Hill;
			}
			if (index == 8)
			{
				return LandscapeToolType.Road;
			}
			return LandscapeToolType.Unknown;
		}

		// Token: 0x06000093 RID: 147 RVA: 0x00006524 File Offset: 0x00005524
		private LandscapeToolType GetActiveLandscapeToolType()
		{
			ILandscapeToolParams landscapeToolParams = this.landscapeToolParamsContainer.Get(this.leftLandscapeToolBinded, this.ActiveLandscapeToolParamsIndex);
			if (landscapeToolParams != null)
			{
				return landscapeToolParams.LandscapeToolType;
			}
			return LandscapeToolType.Unknown;
		}

		// Token: 0x06000094 RID: 148 RVA: 0x00006554 File Offset: 0x00005554
		private TileLandscapeToolParams GetActiveTileLandscapeToolParams()
		{
			ILandscapeToolParams landscapeToolParams = this.landscapeToolParamsContainer.Get(this.leftLandscapeToolBinded, this.ActiveLandscapeToolParamsIndex);
			if (landscapeToolParams != null && landscapeToolParams.LandscapeToolType == LandscapeToolType.Tile)
			{
				return landscapeToolParams as TileLandscapeToolParams;
			}
			return null;
		}

		// Token: 0x06000095 RID: 149 RVA: 0x00006590 File Offset: 0x00005590
		private TilePickerLandscapeToolParams GetActiveTilePickerLandscapeToolParams()
		{
			ILandscapeToolParams landscapeToolParams = this.landscapeToolParamsContainer.Get(this.leftLandscapeToolBinded, this.ActiveLandscapeToolParamsIndex);
			if (landscapeToolParams != null && landscapeToolParams.LandscapeToolType == LandscapeToolType.TilePicker)
			{
				return landscapeToolParams as TilePickerLandscapeToolParams;
			}
			return null;
		}

		// Token: 0x06000096 RID: 150 RVA: 0x000065CC File Offset: 0x000055CC
		private GradientLandscapeToolParams GetActiveLandscapeGradientToolParams()
		{
			ILandscapeToolParams landscapeToolParams = this.landscapeToolParamsContainer.Get(this.leftLandscapeToolBinded, this.ActiveLandscapeToolParamsIndex);
			if (landscapeToolParams != null && landscapeToolParams.LandscapeToolType == LandscapeToolType.Gradient)
			{
				return landscapeToolParams as GradientLandscapeToolParams;
			}
			return null;
		}

		// Token: 0x06000097 RID: 151 RVA: 0x00006608 File Offset: 0x00005608
		private WaterLandscapeToolParams GetActiveWaterLandscapeToolParams()
		{
			ILandscapeToolParams landscapeToolParams = this.landscapeToolParamsContainer.Get(this.leftLandscapeToolBinded, this.ActiveLandscapeToolParamsIndex);
			if (landscapeToolParams != null && landscapeToolParams.LandscapeToolType == LandscapeToolType.Water)
			{
				return landscapeToolParams as WaterLandscapeToolParams;
			}
			return null;
		}

		// Token: 0x06000098 RID: 152 RVA: 0x00006644 File Offset: 0x00005644
		private HeightLandscapeToolParams GetActiveHeightLandscapeToolParams()
		{
			ILandscapeToolParams landscapeToolParams = this.landscapeToolParamsContainer.Get(this.leftLandscapeToolBinded, this.ActiveLandscapeToolParamsIndex);
			if (landscapeToolParams != null && landscapeToolParams.LandscapeToolType == LandscapeToolType.Height)
			{
				return landscapeToolParams as HeightLandscapeToolParams;
			}
			return null;
		}

		// Token: 0x06000099 RID: 153 RVA: 0x00006680 File Offset: 0x00005680
		private HeightPickerLandscapeToolParams GetActiveHeightPickerLandscapeToolParams()
		{
			ILandscapeToolParams landscapeToolParams = this.landscapeToolParamsContainer.Get(this.leftLandscapeToolBinded, this.ActiveLandscapeToolParamsIndex);
			if (landscapeToolParams != null && landscapeToolParams.LandscapeToolType == LandscapeToolType.HeightPicker)
			{
				return landscapeToolParams as HeightPickerLandscapeToolParams;
			}
			return null;
		}

		// Token: 0x0600009A RID: 154 RVA: 0x000066BC File Offset: 0x000056BC
		private ClipboardLandscapeToolParams GetActiveClipboardLandscapeToolParams()
		{
			ILandscapeToolParams landscapeToolParams = this.landscapeToolParamsContainer.Get(this.leftLandscapeToolBinded, this.ActiveLandscapeToolParamsIndex);
			if (landscapeToolParams != null && landscapeToolParams.LandscapeToolType == LandscapeToolType.Clipboard)
			{
				return landscapeToolParams as ClipboardLandscapeToolParams;
			}
			return null;
		}

		// Token: 0x0600009B RID: 155 RVA: 0x000066F8 File Offset: 0x000056F8
		private HillLandscapeToolParams GetActiveHillLandscapeToolParams()
		{
			ILandscapeToolParams landscapeToolParams = this.landscapeToolParamsContainer.Get(this.leftLandscapeToolBinded, this.ActiveLandscapeToolParamsIndex);
			if (landscapeToolParams != null && landscapeToolParams.LandscapeToolType == LandscapeToolType.Hill)
			{
				return landscapeToolParams as HillLandscapeToolParams;
			}
			return null;
		}

		// Token: 0x0600009C RID: 156 RVA: 0x00006734 File Offset: 0x00005734
		private RoadLandscapeToolParams GetActiveRoadLandscapeToolParams()
		{
			ILandscapeToolParams landscapeToolParams = this.landscapeToolParamsContainer.Get(this.leftLandscapeToolBinded, this.ActiveLandscapeToolParamsIndex);
			if (landscapeToolParams != null && landscapeToolParams.LandscapeToolType == LandscapeToolType.Road)
			{
				return landscapeToolParams as RoadLandscapeToolParams;
			}
			return null;
		}

		// Token: 0x0600009D RID: 157 RVA: 0x0000676E File Offset: 0x0000576E
		private void UpdateLandscapeToolParamsControls()
		{
			this.UpdateLandscapeRegionFromLandscapeToolParams();
			this.UpdateLandscapeToolType();
		}

		// Token: 0x0600009E RID: 158 RVA: 0x0000677C File Offset: 0x0000577C
		private void UpdateLandscapeToolType()
		{
			if (this.created)
			{
				LandscapeToolType activeLandscapeToolType = this.GetActiveLandscapeToolType();
				if (activeLandscapeToolType != LandscapeToolType.Unknown)
				{
					IntPtr previousFocusedForm = new IntPtr(User.GetFocus());
					this.created = false;
					int newLandscapeTabIndex = ToolForm.GetTabIndexByLandscapeToolType(activeLandscapeToolType);
					this.LandscapeTabControl.SelectTab(newLandscapeTabIndex);
					this.landscapeToolTabIndexMap[newLandscapeTabIndex] = this.ActiveLandscapeSubstate;
					this.landscapeTabControlSelectedIndexBackup = this.LandscapeTabControl.SelectedIndex;
					this.created = true;
					if (activeLandscapeToolType == LandscapeToolType.Tile)
					{
						this.UpdateTileToolAngleRestrictionsBeginAngle();
						this.UpdateTileToolAngleRestrictionsEndAngle();
						this.UpdateTileToolAngleRestrictionsUseAngleRestrictions();
						this.UpdateTileToolAngleRestrictionsImage();
						this.UpdateTileToolStrengthSmooth();
						this.UpdateTileToolStrengthSmoothImage();
						this.UpdateTileToolAutomaticTool();
						this.UpdateTileToolSpotTool();
						this.UpdateTileToolReplaceTool();
						this.UpdateTileToolTileForPaint();
						this.UpdateTileToolTileForReplace();
					}
					else if (activeLandscapeToolType == LandscapeToolType.TilePicker)
					{
						this.UpdateTilePickerTiles(false);
						this.UpdateTilePickerSelectedIndex();
					}
					else if (activeLandscapeToolType != LandscapeToolType.Gradient)
					{
						if (activeLandscapeToolType == LandscapeToolType.Water)
						{
							this.UpdateWaterToolType();
							this.UpdateWaterToolHeight();
							this.UpdateWaterToolLayerIndex();
						}
						else if (activeLandscapeToolType == LandscapeToolType.Height)
						{
							this.UpdateHeightToolStrengthSmooth();
							this.UpdateHeightToolStrengthSmoothImage();
							this.UpdateHeightToolUpdateObjectHeights();
							this.UpdateHeightToolUpdateObjectHeightsHeightRange();
							this.UpdateHeightToolType();
							this.UpdateHeightPlatoToolType();
							this.UpdateHeightToolHeight();
							this.UpdateHeightToolPreciseTool();
							this.UpdateHeightToolAutomaticTool();
							this.UpdateHeightToolFileName();
						}
						else if (activeLandscapeToolType != LandscapeToolType.HeightPicker)
						{
							if (activeLandscapeToolType == LandscapeToolType.Clipboard)
							{
								this.UpdateClipboardToolStrengthSmooth();
								this.UpdateClipboardToolStrengthSmoothImage();
								this.UpdateClipboardToolUpdateObjectHeights();
								this.UpdateClipboardToolUpdateObjectHeightsHeightRange();
								this.UpdateClipboardTwoSided();
								this.UpdateClipboardToolCopyTiles();
								this.UpdateClipboardToolCopyHeights();
								this.UpdateClipboardToolCopyObjects();
								this.UpdateClipboardToolCopyHeightType();
								this.UpdateClipboardToolPreciseTool();
								this.UpdateClipboardToolFlipHorisontal();
								this.UpdateClipboardToolFlipVertical();
								this.UpdateClipboardToolFileName();
								this.UpdateClipboardToolGroup();
							}
							else if (activeLandscapeToolType == LandscapeToolType.Hill)
							{
								this.UpdateHillLandscapeToolTwoSided();
							}
						}
					}
					User.SetFocus(previousFocusedForm);
				}
			}
		}

		// Token: 0x0600009F RID: 159 RVA: 0x00006928 File Offset: 0x00005928
		private void UpdateTileToolAngleRestrictionsBeginAngle()
		{
			if (this.created)
			{
				TileLandscapeToolParams tileLandscapeToolParams = this.GetActiveTileLandscapeToolParams();
				if (tileLandscapeToolParams != null)
				{
					this.created = false;
					int degreeAngle = (int)(tileLandscapeToolParams.AngleRestrictionsParams.BeginAngle * 180.0 / 3.141592653589793 + 0.5);
					this.LTPTileAngle00TextBox.Text = degreeAngle.ToString();
					this.LTPTileAngle00TrackBar.Value = degreeAngle;
					this.created = true;
				}
			}
		}

		// Token: 0x060000A0 RID: 160 RVA: 0x000069A0 File Offset: 0x000059A0
		private void UpdateTileToolAngleRestrictionsEndAngle()
		{
			if (this.created)
			{
				TileLandscapeToolParams tileLandscapeToolParams = this.GetActiveTileLandscapeToolParams();
				if (tileLandscapeToolParams != null)
				{
					this.created = false;
					int degreeAngle = (int)(tileLandscapeToolParams.AngleRestrictionsParams.EndAngle * 180.0 / 3.141592653589793 + 0.5);
					this.LTPTileAngle01TextBox.Text = degreeAngle.ToString();
					this.LTPTileAngle01TrackBar.Value = degreeAngle;
					this.created = true;
				}
			}
		}

		// Token: 0x060000A1 RID: 161 RVA: 0x00006A18 File Offset: 0x00005A18
		private void UpdateTileToolAngleRestrictionsUseAngleRestrictions()
		{
			if (this.created)
			{
				TileLandscapeToolParams tileLandscapeToolParams = this.GetActiveTileLandscapeToolParams();
				if (tileLandscapeToolParams != null)
				{
					this.created = false;
					this.LTPTileUseAngleRestrictionsCheckBox.Checked = tileLandscapeToolParams.AngleRestrictionsParams.UseAngleRestrictions;
					this.created = true;
				}
			}
		}

		// Token: 0x060000A2 RID: 162 RVA: 0x00006A5C File Offset: 0x00005A5C
		private void UpdateTileToolAngleRestrictionsImage()
		{
			if (this.created)
			{
				PaintEventArgs paintEventArgs = new PaintEventArgs(Graphics.FromHwnd(this.LTPTileAnglePictureBox.Handle), this.LTPTileAnglePictureBox.ClientRectangle);
				base.InvokePaint(this.LTPTileAnglePictureBox, paintEventArgs);
			}
		}

		// Token: 0x060000A3 RID: 163 RVA: 0x00006AA0 File Offset: 0x00005AA0
		private void UpdateTileToolStrengthSmooth()
		{
			if (this.created)
			{
				TileLandscapeToolParams tileLandscapeToolParams = this.GetActiveTileLandscapeToolParams();
				if (tileLandscapeToolParams != null)
				{
					this.created = false;
					int strength = (int)(tileLandscapeToolParams.StrengthSmoothParams.Strength * 100.0 + 0.5);
					int smooth = (int)(tileLandscapeToolParams.StrengthSmoothParams.Smooth * 100.0 + 0.5);
					this.LTPTileStrengthTrackBar.Value = strength;
					this.LTPTileSmoothTrackBar.Value = smooth;
					this.LTPTileStrengthSmoothTextBox.Text = string.Format("{0}/{1}", strength, smooth);
					this.created = true;
				}
			}
		}

		// Token: 0x060000A4 RID: 164 RVA: 0x00006B50 File Offset: 0x00005B50
		private void UpdateTileToolStrengthSmoothImage()
		{
			if (this.created)
			{
				PaintEventArgs paintEventArgs = new PaintEventArgs(Graphics.FromHwnd(this.LTPTileStrengthSmoothPictureBox.Handle), this.LTPTileStrengthSmoothPictureBox.ClientRectangle);
				base.InvokePaint(this.LTPTileStrengthSmoothPictureBox, paintEventArgs);
			}
		}

		// Token: 0x060000A5 RID: 165 RVA: 0x00006B94 File Offset: 0x00005B94
		private void UpdateTileToolAutomaticTool()
		{
			if (this.created)
			{
				TileLandscapeToolParams tileLandscapeToolParams = this.GetActiveTileLandscapeToolParams();
				if (tileLandscapeToolParams != null)
				{
					this.created = false;
					this.LTPTileAutomaticToolCheckBox.Checked = tileLandscapeToolParams.AutomaticTool;
					this.created = true;
				}
			}
		}

		// Token: 0x060000A6 RID: 166 RVA: 0x00006BD4 File Offset: 0x00005BD4
		private void UpdateTileToolSpotTool()
		{
			if (this.created)
			{
				TileLandscapeToolParams tileLandscapeToolParams = this.GetActiveTileLandscapeToolParams();
				if (tileLandscapeToolParams != null)
				{
					this.created = false;
					this.LTPTileSpotToolCheckBox.Checked = tileLandscapeToolParams.SpotTool;
					this.created = true;
				}
			}
		}

		// Token: 0x060000A7 RID: 167 RVA: 0x00006C14 File Offset: 0x00005C14
		private void UpdateTileToolReplaceTool()
		{
			if (this.created)
			{
				TileLandscapeToolParams tileLandscapeToolParams = this.GetActiveTileLandscapeToolParams();
				if (tileLandscapeToolParams != null)
				{
					this.created = false;
					this.LTPTileReplaceToolCheckBox.Checked = tileLandscapeToolParams.ReplaceTool;
					this.created = true;
				}
			}
		}

		// Token: 0x060000A8 RID: 168 RVA: 0x00006C54 File Offset: 0x00005C54
		private void UpdateTileToolTileForPaint()
		{
			if (this.created && !string.IsNullOrEmpty(this.continentName))
			{
				TileLandscapeToolParams tileLandscapeToolParams = this.GetActiveTileLandscapeToolParams();
				if (tileLandscapeToolParams != null)
				{
					this.created = false;
					if (tileLandscapeToolParams.TileForPaint == -1)
					{
						this.landscapeTileItemList.ClearSelection();
					}
					else
					{
						this.landscapeTileItemList.SelectedItem = LandscapeTileItemSource.GetTileItem(tileLandscapeToolParams.TileForPaint, this.continentName);
					}
					this.created = true;
				}
			}
		}

		// Token: 0x060000A9 RID: 169 RVA: 0x00006CC0 File Offset: 0x00005CC0
		private void UpdateTileToolTileForReplace()
		{
			if (this.created && !string.IsNullOrEmpty(this.continentName))
			{
				TileLandscapeToolParams tileLandscapeToolParams = this.GetActiveTileLandscapeToolParams();
				if (tileLandscapeToolParams != null)
				{
					this.created = false;
					this.LTPTileReplaceToolTextBox.Text = tileLandscapeToolParams.TileForReplace.ToString();
					if (tileLandscapeToolParams.TileForReplace == -1)
					{
						this.LTPTileReplaceToolImage.Image = null;
						this.MainToolTip.SetToolTip(this.LTPTileReplaceToolImage, string.Empty);
					}
					else
					{
						ItemData itemData = this.landscapeTileItemList.GetItemData(LandscapeTileItemSource.GetTileItem(tileLandscapeToolParams.TileForReplace, this.continentName));
						if (itemData != null && itemData.IconIndex >= 0 && itemData.IconIndex < this.LTPTileBrushListView.LargeImageList.Images.Count)
						{
							this.LTPTileReplaceToolImage.Image = this.LTPTileBrushListView.LargeImageList.Images[itemData.IconIndex];
							this.MainToolTip.SetToolTip(this.LTPTileReplaceToolImage, itemData.Tooltip);
						}
						else
						{
							this.LTPTileReplaceToolImage.Image = null;
							this.MainToolTip.SetToolTip(this.LTPTileReplaceToolImage, string.Empty);
						}
					}
					this.created = true;
				}
			}
		}

		// Token: 0x060000AA RID: 170 RVA: 0x00006DF4 File Offset: 0x00005DF4
		private void UpdateWaterToolForPaint()
		{
			if (this.created && !string.IsNullOrEmpty(this.continentName))
			{
				WaterLandscapeToolParams waterLandscapeToolParams = this.GetActiveWaterLandscapeToolParams();
				if (waterLandscapeToolParams != null)
				{
					this.created = false;
					waterLandscapeToolParams.WaterToolType = LandscapeWaterToolType.Add;
					waterLandscapeToolParams.LayerIndex = -1;
					this.landscapeWaterItemList.ClearSelection();
					this.LTPWaterWaterToolButton00.Checked = true;
					this.created = true;
				}
			}
		}

		// Token: 0x060000AB RID: 171 RVA: 0x00006E54 File Offset: 0x00005E54
		private void UpdateWaterToolType()
		{
			if (this.created)
			{
				WaterLandscapeToolParams waterLandscapeToolParams = this.GetActiveWaterLandscapeToolParams();
				if (waterLandscapeToolParams != null)
				{
					this.created = false;
					this.LTPWaterWaterToolButton00.Checked = (waterLandscapeToolParams.WaterToolType == LandscapeWaterToolType.Add);
					this.LTPWaterWaterToolButton01.Checked = (waterLandscapeToolParams.WaterToolType == LandscapeWaterToolType.Remove);
					this.LTPWaterWaterToolButton02.Checked = (waterLandscapeToolParams.WaterToolType == LandscapeWaterToolType.MoveToHeight);
					this.LTPWaterWaterToolButton03.Checked = (waterLandscapeToolParams.WaterToolType == LandscapeWaterToolType.PickAndMoveToHeight);
					this.created = true;
				}
			}
		}

		// Token: 0x060000AC RID: 172 RVA: 0x00006ED4 File Offset: 0x00005ED4
		private void UpdateWaterToolHeight()
		{
			if (this.created)
			{
				WaterLandscapeToolParams waterLandscapeToolParams = this.GetActiveWaterLandscapeToolParams();
				if (waterLandscapeToolParams != null)
				{
					this.created = false;
					this.LTPWaterHeightTextBox.Text = waterLandscapeToolParams.Height.ToString();
					this.created = true;
				}
			}
		}

		// Token: 0x060000AD RID: 173 RVA: 0x00006F1C File Offset: 0x00005F1C
		private void UpdateWaterToolLayerIndex()
		{
			if (this.created)
			{
				WaterLandscapeToolParams waterLandscapeToolParams = this.GetActiveWaterLandscapeToolParams();
				if (waterLandscapeToolParams != null)
				{
					this.created = false;
					this.landscapeWaterItemList.SelectItem(LandscapeWaterItemSource.GetWaterLayerItem(waterLandscapeToolParams.LayerIndex, this.continentName));
					this.created = true;
				}
			}
		}

		// Token: 0x060000AE RID: 174 RVA: 0x00006F68 File Offset: 0x00005F68
		private void UpdateHeightToolStrengthSmooth()
		{
			if (this.created)
			{
				HeightLandscapeToolParams heightLandscapeToolParams = this.GetActiveHeightLandscapeToolParams();
				if (heightLandscapeToolParams != null)
				{
					this.created = false;
					int strength = (int)(heightLandscapeToolParams.StrengthSmoothParams.Strength * 100.0 + 0.5);
					int smooth = (int)(heightLandscapeToolParams.StrengthSmoothParams.Smooth * 100.0 + 0.5);
					this.LTPHeightStrengthTrackBar.Value = strength;
					this.LTPHeightSmoothTrackBar.Value = smooth;
					this.LTPHeightStrengthSmoothTextBox.Text = string.Format("{0}/{1}", strength, smooth);
					this.created = true;
				}
			}
		}

		// Token: 0x060000AF RID: 175 RVA: 0x00007018 File Offset: 0x00006018
		private void UpdateHeightToolStrengthSmoothImage()
		{
			if (this.created)
			{
				PaintEventArgs paintEventArgs = new PaintEventArgs(Graphics.FromHwnd(this.LTPHeightStrengthSmoothPictureBox.Handle), this.LTPHeightStrengthSmoothPictureBox.ClientRectangle);
				base.InvokePaint(this.LTPHeightStrengthSmoothPictureBox, paintEventArgs);
			}
		}

		// Token: 0x060000B0 RID: 176 RVA: 0x0000705C File Offset: 0x0000605C
		private void UpdateHeightToolUpdateObjectHeights()
		{
			if (this.created)
			{
				HeightLandscapeToolParams heightLandscapeToolParams = this.GetActiveHeightLandscapeToolParams();
				if (heightLandscapeToolParams != null)
				{
					this.created = false;
					this.LTPHeightUpdateObjectsCheckBox.Checked = heightLandscapeToolParams.UpdateObjectHeightsParams.Update;
					this.created = true;
				}
			}
		}

		// Token: 0x060000B1 RID: 177 RVA: 0x000070A0 File Offset: 0x000060A0
		private void UpdateHeightToolUpdateObjectHeightsHeightRange()
		{
			if (this.created)
			{
				HeightLandscapeToolParams heightLandscapeToolParams = this.GetActiveHeightLandscapeToolParams();
				if (heightLandscapeToolParams != null)
				{
					this.created = false;
					this.LTPHeightUpdateObjectsTextBox.Text = heightLandscapeToolParams.UpdateObjectHeightsParams.HeightRange.ToString();
					this.created = true;
				}
			}
		}

		// Token: 0x060000B2 RID: 178 RVA: 0x000070EC File Offset: 0x000060EC
		private void SetHeightToolType()
		{
			int previousLandscapeSubstate = -1;
			if (this.LTPHeightHeightToolButton00.Checked)
			{
				previousLandscapeSubstate = this.landscapeToolParamsContainer.GetSutableLandscapeHeightSubstate(LandscapeHeightToolType.Up);
			}
			else if (this.LTPHeightHeightToolButton01.Checked)
			{
				previousLandscapeSubstate = this.landscapeToolParamsContainer.GetSutableLandscapeHeightSubstate(LandscapeHeightToolType.Down);
			}
			else if (this.LTPHeightHeightToolButton02.Checked)
			{
				previousLandscapeSubstate = this.landscapeToolParamsContainer.GetSutableLandscapeHeightSubstate(LandscapeHeightToolType.Plato);
			}
			else if (this.LTPHeightHeightToolButton03.Checked)
			{
				previousLandscapeSubstate = this.landscapeToolParamsContainer.GetSutableLandscapeHeightSubstate(LandscapeHeightToolType.Plane);
			}
			else if (this.LTPHeightHeightToolButton04.Checked)
			{
				previousLandscapeSubstate = this.landscapeToolParamsContainer.GetSutableLandscapeHeightSubstate(LandscapeHeightToolType.Smooth);
			}
			else if (this.LTPHeightHeightToolButton05.Checked)
			{
				previousLandscapeSubstate = this.landscapeToolParamsContainer.GetSutableLandscapeHeightSubstate(LandscapeHeightToolType.LevelToPlane);
			}
			if (previousLandscapeSubstate != -1)
			{
				base.Context.MainState.ActiveSubstate = previousLandscapeSubstate;
			}
		}

		// Token: 0x060000B3 RID: 179 RVA: 0x000071BC File Offset: 0x000061BC
		private void UpdateHeightToolType()
		{
			if (this.created)
			{
				HeightLandscapeToolParams heightLandscapeToolParams = this.GetActiveHeightLandscapeToolParams();
				if (heightLandscapeToolParams != null)
				{
					this.created = false;
					this.LTPHeightHeightToolButton00.Checked = (heightLandscapeToolParams.HeightToolType == LandscapeHeightToolType.Up);
					this.LTPHeightHeightToolButton01.Checked = (heightLandscapeToolParams.HeightToolType == LandscapeHeightToolType.Down);
					this.LTPHeightHeightToolButton02.Checked = (heightLandscapeToolParams.HeightToolType == LandscapeHeightToolType.Plato);
					this.LTPHeightHeightToolButton03.Checked = (heightLandscapeToolParams.HeightToolType == LandscapeHeightToolType.Plane);
					this.LTPHeightHeightToolButton04.Checked = (heightLandscapeToolParams.HeightToolType == LandscapeHeightToolType.Smooth);
					this.LTPHeightHeightToolButton05.Checked = (heightLandscapeToolParams.HeightToolType == LandscapeHeightToolType.LevelToPlane);
					this.created = true;
				}
			}
		}

		// Token: 0x060000B4 RID: 180 RVA: 0x00007268 File Offset: 0x00006268
		private void UpdateHeightPlatoToolType()
		{
			if (this.created)
			{
				HeightLandscapeToolParams heightLandscapeToolParams = this.GetActiveHeightLandscapeToolParams();
				if (heightLandscapeToolParams != null)
				{
					this.created = false;
					this.LTPHeightRaisePlatoCheckBox.Checked = (heightLandscapeToolParams.HeightPlatoToolType == LandscapeHeightPlatoToolType.Raise);
					this.LTPHeightLowerPlatoCheckBox.Checked = (heightLandscapeToolParams.HeightPlatoToolType == LandscapeHeightPlatoToolType.Lower);
					this.created = true;
				}
			}
		}

		// Token: 0x060000B5 RID: 181 RVA: 0x000072C0 File Offset: 0x000062C0
		private void UpdateHeightToolHeight()
		{
			if (this.created)
			{
				HeightLandscapeToolParams heightLandscapeToolParams = this.GetActiveHeightLandscapeToolParams();
				if (heightLandscapeToolParams != null)
				{
					this.created = false;
					this.LTPHeightHeightTextBox.Text = heightLandscapeToolParams.Height.ToString();
					this.created = true;
				}
			}
		}

		// Token: 0x060000B6 RID: 182 RVA: 0x00007308 File Offset: 0x00006308
		private void UpdateHeightToolPreciseTool()
		{
			if (this.created)
			{
				HeightLandscapeToolParams heightLandscapeToolParams = this.GetActiveHeightLandscapeToolParams();
				if (heightLandscapeToolParams != null)
				{
					this.created = false;
					this.LTPHeightPreciseToolCheckBox.Checked = heightLandscapeToolParams.PreciseTool;
					this.created = true;
				}
			}
		}

		// Token: 0x060000B7 RID: 183 RVA: 0x00007348 File Offset: 0x00006348
		private void UpdateHeightToolAutomaticTool()
		{
			if (this.created)
			{
				HeightLandscapeToolParams heightLandscapeToolParams = this.GetActiveHeightLandscapeToolParams();
				if (heightLandscapeToolParams != null)
				{
					this.created = false;
					this.LTPHeightAutomaticToolCheckBox.Checked = heightLandscapeToolParams.AutomaticTool;
					this.created = true;
				}
			}
		}

		// Token: 0x060000B8 RID: 184 RVA: 0x00007388 File Offset: 0x00006388
		private void UpdateHeightToolFileName()
		{
			if (this.created)
			{
				HeightLandscapeToolParams heightLandscapeToolParams = this.GetActiveHeightLandscapeToolParams();
				if (heightLandscapeToolParams != null)
				{
					this.created = false;
					if (string.IsNullOrEmpty(heightLandscapeToolParams.FileName))
					{
						this.landscapeHeightItemList.ClearSelection();
					}
					else
					{
						this.landscapeHeightItemList.SelectedItem = heightLandscapeToolParams.FileName;
					}
					this.created = true;
				}
			}
		}

		// Token: 0x060000B9 RID: 185 RVA: 0x000073E0 File Offset: 0x000063E0
		private void UpdateTilePickerTiles(bool clear)
		{
			if (this.created)
			{
				TilePickerLandscapeToolParams tilePickerLandscapeToolParams = this.GetActiveTilePickerLandscapeToolParams();
				if (tilePickerLandscapeToolParams != null)
				{
					this.created = false;
					if (clear)
					{
						for (int index = 0; index < this.tilePickerControlElements.Count; index++)
						{
							this.tilePickerControlElements[index].RadioButton.Enabled = false;
							this.tilePickerControlElements[index].RadioButton.Text = "0%";
							this.tilePickerControlElements[index].PictureBox.Image = null;
							this.MainToolTip.SetToolTip(this.tilePickerControlElements[index].PictureBox, string.Empty);
							this.tilePickerControlElements[index].Label.Text = string.Empty;
						}
					}
					else
					{
						for (int index2 = 0; index2 < tilePickerLandscapeToolParams.Tiles.Count; index2++)
						{
							ItemData itemData = this.landscapeTileItemList.GetItemData(LandscapeTileItemSource.GetTileItem(tilePickerLandscapeToolParams.Tiles[index2].Tile, this.continentName));
							if (itemData != null && itemData.IconIndex >= 0 && itemData.IconIndex < this.LTPTileBrushListView.LargeImageList.Images.Count)
							{
								this.tilePickerControlElements[index2].RadioButton.Enabled = true;
								this.tilePickerControlElements[index2].RadioButton.Text = string.Format("{0}%", (int)(tilePickerLandscapeToolParams.Tiles[index2].Weight * 100.0));
								this.tilePickerControlElements[index2].PictureBox.Image = this.LTPTileBrushListView.LargeImageList.Images[itemData.IconIndex];
								this.MainToolTip.SetToolTip(this.tilePickerControlElements[index2].PictureBox, itemData.Tooltip);
								this.tilePickerControlElements[index2].Label.Text = itemData.Tooltip;
							}
							else
							{
								this.tilePickerControlElements[index2].RadioButton.Enabled = false;
								this.tilePickerControlElements[index2].RadioButton.Text = "0%";
								this.tilePickerControlElements[index2].PictureBox.Image = null;
								this.MainToolTip.SetToolTip(this.tilePickerControlElements[index2].PictureBox, string.Empty);
								this.tilePickerControlElements[index2].Label.Text = string.Empty;
							}
						}
						for (int index3 = tilePickerLandscapeToolParams.Tiles.Count; index3 < this.tilePickerControlElements.Count; index3++)
						{
							this.tilePickerControlElements[index3].RadioButton.Enabled = false;
							this.tilePickerControlElements[index3].RadioButton.Text = "0%";
							this.tilePickerControlElements[index3].PictureBox.Image = null;
							this.MainToolTip.SetToolTip(this.tilePickerControlElements[index3].PictureBox, string.Empty);
							this.tilePickerControlElements[index3].Label.Text = string.Empty;
						}
					}
					this.created = true;
				}
			}
		}

		// Token: 0x060000BA RID: 186 RVA: 0x00007744 File Offset: 0x00006744
		private void UpdateTilePickerSelectedIndex()
		{
			if (this.created)
			{
				TilePickerLandscapeToolParams tilePickerLandscapeToolParams = this.GetActiveTilePickerLandscapeToolParams();
				if (tilePickerLandscapeToolParams != null)
				{
					this.created = false;
					if (tilePickerLandscapeToolParams.SelectedIndex >= 0 && tilePickerLandscapeToolParams.SelectedIndex < this.tilePickerControlElements.Count)
					{
						this.tilePickerControlElements[tilePickerLandscapeToolParams.SelectedIndex].RadioButton.Checked = true;
					}
					this.created = true;
				}
			}
		}

		// Token: 0x060000BB RID: 187 RVA: 0x000077AC File Offset: 0x000067AC
		private void UpdateClipboardToolStrengthSmooth()
		{
			if (this.created)
			{
				ClipboardLandscapeToolParams clipboardLandscapeToolParams = this.GetActiveClipboardLandscapeToolParams();
				if (clipboardLandscapeToolParams != null)
				{
					this.created = false;
					int strength = (int)(clipboardLandscapeToolParams.StrengthSmoothParams.Strength * 100.0 + 0.5);
					int smooth = (int)(clipboardLandscapeToolParams.StrengthSmoothParams.Smooth * 100.0 + 0.5);
					this.LTPClipboardStrengthTrackBar.Value = strength;
					this.LTPClipboardSmoothTrackBar.Value = smooth;
					this.LTPClipboardStrengthSmoothTextBox.Text = string.Format("{0}/{1}", strength, smooth);
					this.created = true;
				}
			}
		}

		// Token: 0x060000BC RID: 188 RVA: 0x0000785C File Offset: 0x0000685C
		private void UpdateClipboardToolStrengthSmoothImage()
		{
			if (this.created)
			{
				PaintEventArgs paintEventArgs = new PaintEventArgs(Graphics.FromHwnd(this.LTPClipboardStrengthSmoothPictureBox.Handle), this.LTPClipboardStrengthSmoothPictureBox.ClientRectangle);
				base.InvokePaint(this.LTPClipboardStrengthSmoothPictureBox, paintEventArgs);
			}
		}

		// Token: 0x060000BD RID: 189 RVA: 0x000078A0 File Offset: 0x000068A0
		private void UpdateClipboardToolUpdateObjectHeights()
		{
			if (this.created)
			{
				ClipboardLandscapeToolParams clipboardLandscapeToolParams = this.GetActiveClipboardLandscapeToolParams();
				if (clipboardLandscapeToolParams != null)
				{
					this.created = false;
					this.LTPClipboardUpdateObjectsCheckBox.Checked = clipboardLandscapeToolParams.UpdateObjectHeightsParams.Update;
					this.created = true;
				}
			}
		}

		// Token: 0x060000BE RID: 190 RVA: 0x000078E4 File Offset: 0x000068E4
		private void UpdateClipboardToolUpdateObjectHeightsHeightRange()
		{
			if (this.created)
			{
				ClipboardLandscapeToolParams clipboardLandscapeToolParams = this.GetActiveClipboardLandscapeToolParams();
				if (clipboardLandscapeToolParams != null)
				{
					this.created = false;
					this.LTPClipboardUpdateObjectsTextBox.Text = clipboardLandscapeToolParams.UpdateObjectHeightsParams.HeightRange.ToString();
					this.created = true;
				}
			}
		}

		// Token: 0x060000BF RID: 191 RVA: 0x00007930 File Offset: 0x00006930
		private void UpdateClipboardTwoSided()
		{
			if (this.created)
			{
				ClipboardLandscapeToolParams clipboardLandscapeToolParams = this.GetActiveClipboardLandscapeToolParams();
				if (clipboardLandscapeToolParams != null)
				{
					this.created = false;
					this.LTPClipboardTwoSidedCheckBox.Checked = clipboardLandscapeToolParams.TwoSided;
					this.created = true;
				}
			}
		}

		// Token: 0x060000C0 RID: 192 RVA: 0x00007970 File Offset: 0x00006970
		private void UpdateClipboardToolCopyTiles()
		{
			if (this.created)
			{
				ClipboardLandscapeToolParams clipboardLandscapeToolParams = this.GetActiveClipboardLandscapeToolParams();
				if (clipboardLandscapeToolParams != null)
				{
					this.created = false;
					this.LTPClipboardCopyTypeButton00.Checked = clipboardLandscapeToolParams.CopyTiles;
					this.created = true;
				}
			}
		}

		// Token: 0x060000C1 RID: 193 RVA: 0x000079B0 File Offset: 0x000069B0
		private void UpdateClipboardToolCopyHeights()
		{
			if (this.created)
			{
				ClipboardLandscapeToolParams clipboardLandscapeToolParams = this.GetActiveClipboardLandscapeToolParams();
				if (clipboardLandscapeToolParams != null)
				{
					this.created = false;
					this.LTPClipboardCopyTypeButton01.Checked = clipboardLandscapeToolParams.CopyHeights;
					this.created = true;
				}
			}
		}

		// Token: 0x060000C2 RID: 194 RVA: 0x000079F0 File Offset: 0x000069F0
		private void UpdateClipboardToolCopyObjects()
		{
			if (this.created)
			{
				ClipboardLandscapeToolParams clipboardLandscapeToolParams = this.GetActiveClipboardLandscapeToolParams();
				if (clipboardLandscapeToolParams != null)
				{
					this.created = false;
					this.LTPClipboardCopyTypeButton02.Checked = clipboardLandscapeToolParams.CopyObjects;
					this.created = true;
				}
			}
		}

		// Token: 0x060000C3 RID: 195 RVA: 0x00007A30 File Offset: 0x00006A30
		private void UpdateClipboardToolCopyHeightType()
		{
			if (this.created)
			{
				ClipboardLandscapeToolParams clipboardLandscapeToolParams = this.GetActiveClipboardLandscapeToolParams();
				if (clipboardLandscapeToolParams != null)
				{
					this.created = false;
					this.LTPClipboardCopyHeightTypeButton00.Checked = (clipboardLandscapeToolParams.CopyHeightType == LandscapeCopyHeightType.Adaptive);
					this.LTPClipboardCopyHeightTypeButton01.Checked = (clipboardLandscapeToolParams.CopyHeightType == LandscapeCopyHeightType.Min);
					this.LTPClipboardCopyHeightTypeButton02.Checked = (clipboardLandscapeToolParams.CopyHeightType == LandscapeCopyHeightType.Max);
					this.LTPClipboardCopyHeightTypeButton03.Checked = (clipboardLandscapeToolParams.CopyHeightType == LandscapeCopyHeightType.Precise);
					this.created = true;
				}
			}
		}

		// Token: 0x060000C4 RID: 196 RVA: 0x00007AB0 File Offset: 0x00006AB0
		private void UpdateClipboardToolPreciseTool()
		{
			if (this.created)
			{
				ClipboardLandscapeToolParams clipboardLandscapeToolParams = this.GetActiveClipboardLandscapeToolParams();
				if (clipboardLandscapeToolParams != null)
				{
					this.created = false;
					this.LTPClipboardPreciseToolCheckBox.Checked = clipboardLandscapeToolParams.PreciseTool;
					this.created = true;
				}
			}
		}

		// Token: 0x060000C5 RID: 197 RVA: 0x00007AF0 File Offset: 0x00006AF0
		private void UpdateClipboardToolFlipHorisontal()
		{
			if (this.created)
			{
				ClipboardLandscapeToolParams clipboardLandscapeToolParams = this.GetActiveClipboardLandscapeToolParams();
				if (clipboardLandscapeToolParams != null)
				{
					this.created = false;
					this.LTPClipboardFlipHorisontalCheckBox.Checked = clipboardLandscapeToolParams.FlipHorisontal;
					this.created = true;
				}
			}
		}

		// Token: 0x060000C6 RID: 198 RVA: 0x00007B30 File Offset: 0x00006B30
		private void UpdateClipboardToolFlipVertical()
		{
			if (this.created)
			{
				ClipboardLandscapeToolParams clipboardLandscapeToolParams = this.GetActiveClipboardLandscapeToolParams();
				if (clipboardLandscapeToolParams != null)
				{
					this.created = false;
					this.LTPClipboardFlipVerticalCheckBox.Checked = clipboardLandscapeToolParams.FlipVertical;
					this.created = true;
				}
			}
		}

		// Token: 0x060000C7 RID: 199 RVA: 0x00007B70 File Offset: 0x00006B70
		private void UpdateClipboardToolFileName()
		{
			if (this.created)
			{
				ClipboardLandscapeToolParams clipboardLandscapeToolParams = this.GetActiveClipboardLandscapeToolParams();
				if (clipboardLandscapeToolParams != null)
				{
					this.created = false;
					if (string.IsNullOrEmpty(clipboardLandscapeToolParams.FileName))
					{
						this.landscapeClipboardItemList.ClearSelection();
					}
					else
					{
						this.landscapeClipboardItemList.AddAndSelectItem(clipboardLandscapeToolParams.FileName);
					}
					this.created = true;
				}
			}
		}

		// Token: 0x060000C8 RID: 200 RVA: 0x00007BC8 File Offset: 0x00006BC8
		private void UpdateClipboardToolGroup()
		{
			if (this.created)
			{
				ClipboardLandscapeToolParams clipboardLandscapeToolParams = this.GetActiveClipboardLandscapeToolParams();
				if (clipboardLandscapeToolParams != null)
				{
					this.created = false;
					this.LTPClipboardGroupTextBox.Text = clipboardLandscapeToolParams.Group;
					this.created = true;
				}
			}
		}

		// Token: 0x060000C9 RID: 201 RVA: 0x00007C08 File Offset: 0x00006C08
		private void UpdateHillLandscapeToolTwoSided()
		{
			if (this.created)
			{
				HillLandscapeToolParams hillLandscapeToolParams = this.GetActiveHillLandscapeToolParams();
				if (hillLandscapeToolParams != null)
				{
					this.created = false;
					this.LTPHillTwoSidedCheckBox.Checked = hillLandscapeToolParams.TwoSided;
					this.created = true;
				}
			}
		}

		// Token: 0x060000CA RID: 202 RVA: 0x00007C46 File Offset: 0x00006C46
		private void OnTileToolAngleRestrictionsBeginAngleChanged(AngleRestrictionsParams angleRestrictionsParams, ref double oldValue, ref double newValue)
		{
			this.UpdateTileToolAngleRestrictionsBeginAngle();
			this.UpdateTileToolAngleRestrictionsImage();
		}

		// Token: 0x060000CB RID: 203 RVA: 0x00007C54 File Offset: 0x00006C54
		private void OnTileToolAngleRestrictionsEndAngleChanged(AngleRestrictionsParams angleRestrictionsParams, ref double oldValue, ref double newValue)
		{
			this.UpdateTileToolAngleRestrictionsEndAngle();
			this.UpdateTileToolAngleRestrictionsImage();
		}

		// Token: 0x060000CC RID: 204 RVA: 0x00007C62 File Offset: 0x00006C62
		private void OnTileToolAngleRestrictionsUseAngleRestrictionsChanged(AngleRestrictionsParams angleRestrictionsParams, ref bool oldValue, ref bool newValue)
		{
			this.UpdateTileToolAngleRestrictionsUseAngleRestrictions();
		}

		// Token: 0x060000CD RID: 205 RVA: 0x00007C6A File Offset: 0x00006C6A
		private void OnTileToolStrengthSmoothStrengthChanged(StrengthSmoothParams strengthSmoothParams, ref double oldValue, ref double newValue)
		{
			this.UpdateTileToolStrengthSmooth();
			this.UpdateTileToolStrengthSmoothImage();
		}

		// Token: 0x060000CE RID: 206 RVA: 0x00007C78 File Offset: 0x00006C78
		private void OnTileToolStrengthSmoothSmoothChanged(StrengthSmoothParams strengthSmoothParams, ref double oldValue, ref double newValue)
		{
			this.UpdateTileToolStrengthSmooth();
			this.UpdateTileToolStrengthSmoothImage();
		}

		// Token: 0x060000CF RID: 207 RVA: 0x00007C86 File Offset: 0x00006C86
		private void OnTileToolAutomaticToolChanged(TileLandscapeToolParams tileLandscapeToolParams, ref bool oldValue, ref bool newValue)
		{
			this.UpdateTileToolAutomaticTool();
		}

		// Token: 0x060000D0 RID: 208 RVA: 0x00007C8E File Offset: 0x00006C8E
		private void OnTileToolSpotToolChanged(TileLandscapeToolParams tileLandscapeToolParams, ref bool oldValue, ref bool newValue)
		{
			this.UpdateTileToolSpotTool();
		}

		// Token: 0x060000D1 RID: 209 RVA: 0x00007C96 File Offset: 0x00006C96
		private void OnTileToolReplaceToolChanged(TileLandscapeToolParams tileLandscapeToolParams, ref bool oldValue, ref bool newValue)
		{
			this.UpdateTileToolReplaceTool();
		}

		// Token: 0x060000D2 RID: 210 RVA: 0x00007C9E File Offset: 0x00006C9E
		private void OnTileToolTileForPaintChanged(TileLandscapeToolParams tileLandscapeToolParams, ref int oldValue, ref int newValue)
		{
			this.UpdateTileToolTileForPaint();
		}

		// Token: 0x060000D3 RID: 211 RVA: 0x00007CA6 File Offset: 0x00006CA6
		private void OnTileToolTileForReplaceChanged(TileLandscapeToolParams tileLandscapeToolParams, ref int oldValue, ref int newValue)
		{
			this.UpdateTileToolTileForReplace();
		}

		// Token: 0x060000D4 RID: 212 RVA: 0x00007CAE File Offset: 0x00006CAE
		private void OnWaterToolWaterToolTypeChanged(WaterLandscapeToolParams waterLandscapeToolParams, ref LandscapeWaterToolType oldValue, ref LandscapeWaterToolType newValue)
		{
			this.UpdateWaterToolType();
		}

		// Token: 0x060000D5 RID: 213 RVA: 0x00007CB6 File Offset: 0x00006CB6
		private void OnWaterToolHeightChanged(WaterLandscapeToolParams waterLandscapeToolParams, ref double oldValue, ref double newValue)
		{
			this.UpdateWaterToolHeight();
		}

		// Token: 0x060000D6 RID: 214 RVA: 0x00007CBE File Offset: 0x00006CBE
		private void OnWaterToolLayerIndexChanged(WaterLandscapeToolParams waterLandscapeToolParams, ref int oldValue, ref int newValue)
		{
			this.UpdateWaterToolLayerIndex();
		}

		// Token: 0x060000D7 RID: 215 RVA: 0x00007CC6 File Offset: 0x00006CC6
		private void OnHeightToolStrengthSmoothStrengthChanged(StrengthSmoothParams strengthSmoothParams, ref double oldValue, ref double newValue)
		{
			this.UpdateHeightToolStrengthSmooth();
			this.UpdateHeightToolStrengthSmoothImage();
		}

		// Token: 0x060000D8 RID: 216 RVA: 0x00007CD4 File Offset: 0x00006CD4
		private void OnHeightToolStrengthSmoothSmoothChanged(StrengthSmoothParams strengthSmoothParams, ref double oldValue, ref double newValue)
		{
			this.UpdateHeightToolStrengthSmooth();
			this.UpdateHeightToolStrengthSmoothImage();
		}

		// Token: 0x060000D9 RID: 217 RVA: 0x00007CE2 File Offset: 0x00006CE2
		private void OnHeightToolUpdateObjectHeightsUpdateChanged(UpdateObjectHeightsParams updateObjectHeightsParams, ref bool oldValue, ref bool newValue)
		{
			this.UpdateHeightToolUpdateObjectHeights();
		}

		// Token: 0x060000DA RID: 218 RVA: 0x00007CEA File Offset: 0x00006CEA
		private void OnHeightToolUpdateObjectHeightsHeightRangeChanged(UpdateObjectHeightsParams updateObjectHeightsParams, ref double oldValue, ref double newValue)
		{
			this.UpdateHeightToolUpdateObjectHeightsHeightRange();
		}

		// Token: 0x060000DB RID: 219 RVA: 0x00007CF2 File Offset: 0x00006CF2
		private void OnHeightToolHeightToolTypeChanged(HeightLandscapeToolParams heightLandscapeToolParams, ref LandscapeHeightToolType oldValue, ref LandscapeHeightToolType newValue)
		{
			this.UpdateHeightToolType();
		}

		// Token: 0x060000DC RID: 220 RVA: 0x00007CFA File Offset: 0x00006CFA
		private void OnHeightToolHeightPlatoToolTypeChanged(HeightLandscapeToolParams heightLandscapeToolParams, ref LandscapeHeightPlatoToolType oldValue, ref LandscapeHeightPlatoToolType newValue)
		{
			this.UpdateHeightPlatoToolType();
		}

		// Token: 0x060000DD RID: 221 RVA: 0x00007D02 File Offset: 0x00006D02
		private void OnHeightToolHeightChanged(HeightLandscapeToolParams heightLandscapeToolParams, ref double oldValue, ref double newValue)
		{
			this.UpdateHeightToolHeight();
		}

		// Token: 0x060000DE RID: 222 RVA: 0x00007D0A File Offset: 0x00006D0A
		private void OnHeightToolPreciseToolChanged(HeightLandscapeToolParams heightLandscapeToolParams, ref bool oldValue, ref bool newValue)
		{
			this.UpdateHeightToolPreciseTool();
		}

		// Token: 0x060000DF RID: 223 RVA: 0x00007D12 File Offset: 0x00006D12
		private void OnHeightToolAutomaticToolChanged(HeightLandscapeToolParams heightLandscapeToolParams, ref bool oldValue, ref bool newValue)
		{
			this.UpdateHeightToolAutomaticTool();
		}

		// Token: 0x060000E0 RID: 224 RVA: 0x00007D1A File Offset: 0x00006D1A
		private void OnHeightToolFileNameChanged(HeightLandscapeToolParams heightLandscapeToolParams, ref string oldValue, ref string newValue)
		{
			this.UpdateHeightToolFileName();
		}

		// Token: 0x060000E1 RID: 225 RVA: 0x00007D22 File Offset: 0x00006D22
		private void OnTilePickerToolSelectedIndexChanged(TilePickerLandscapeToolParams tilePickerLandscapeToolParams, ref int oldValue, ref int newValue)
		{
			this.UpdateTilePickerSelectedIndex();
		}

		// Token: 0x060000E2 RID: 226 RVA: 0x00007D2A File Offset: 0x00006D2A
		private void OnTilePickerToolTilesChanged(TilePickerLandscapeToolParams tilePickerLandscapeToolParams)
		{
			this.UpdateTilePickerTiles(false);
		}

		// Token: 0x060000E3 RID: 227 RVA: 0x00007D33 File Offset: 0x00006D33
		private void OnClipboardToolStrengthSmoothStrengthChanged(StrengthSmoothParams strengthSmoothParams, ref double oldValue, ref double newValue)
		{
			this.UpdateClipboardToolStrengthSmooth();
			this.UpdateClipboardToolStrengthSmoothImage();
		}

		// Token: 0x060000E4 RID: 228 RVA: 0x00007D41 File Offset: 0x00006D41
		private void OnClipboardToolStrengthSmoothSmoothChanged(StrengthSmoothParams strengthSmoothParams, ref double oldValue, ref double newValue)
		{
			this.UpdateClipboardToolStrengthSmooth();
			this.UpdateClipboardToolStrengthSmoothImage();
		}

		// Token: 0x060000E5 RID: 229 RVA: 0x00007D4F File Offset: 0x00006D4F
		private void OClipboardToolUpdateObjectHeightsUpdateChanged(UpdateObjectHeightsParams updateObjectHeightsParams, ref bool oldValue, ref bool newValue)
		{
			this.UpdateClipboardToolUpdateObjectHeights();
		}

		// Token: 0x060000E6 RID: 230 RVA: 0x00007D57 File Offset: 0x00006D57
		private void OnClipboardToolUpdateObjectHeightsHeightRangeChanged(UpdateObjectHeightsParams updateObjectHeightsParams, ref double oldValue, ref double newValue)
		{
			this.UpdateClipboardToolUpdateObjectHeightsHeightRange();
		}

		// Token: 0x060000E7 RID: 231 RVA: 0x00007D5F File Offset: 0x00006D5F
		private void OnClipboardToolTwoSidedChanged(ClipboardLandscapeToolParams clipboardLandscapeToolParams, ref bool oldValue, ref bool newValue)
		{
			this.UpdateClipboardTwoSided();
		}

		// Token: 0x060000E8 RID: 232 RVA: 0x00007D67 File Offset: 0x00006D67
		private void OnClipboardToolCopyTilesChanged(ClipboardLandscapeToolParams clipboardLandscapeToolParams, ref bool oldValue, ref bool newValue)
		{
			this.UpdateClipboardToolCopyTiles();
		}

		// Token: 0x060000E9 RID: 233 RVA: 0x00007D6F File Offset: 0x00006D6F
		private void OnClipboardToolCopyHeightsChanged(ClipboardLandscapeToolParams clipboardLandscapeToolParams, ref bool oldValue, ref bool newValue)
		{
			this.UpdateClipboardToolCopyHeights();
		}

		// Token: 0x060000EA RID: 234 RVA: 0x00007D77 File Offset: 0x00006D77
		private void OnClipboardToolCopyObjectsChanged(ClipboardLandscapeToolParams clipboardLandscapeToolParams, ref bool oldValue, ref bool newValue)
		{
			this.UpdateClipboardToolCopyObjects();
		}

		// Token: 0x060000EB RID: 235 RVA: 0x00007D7F File Offset: 0x00006D7F
		private void OnClipboardToolCopyHeightTypeChanged(ClipboardLandscapeToolParams clipboardLandscapeToolParams, ref LandscapeCopyHeightType oldValue, ref LandscapeCopyHeightType newValue)
		{
			this.UpdateClipboardToolCopyHeightType();
		}

		// Token: 0x060000EC RID: 236 RVA: 0x00007D87 File Offset: 0x00006D87
		private void OnClipboardToolPreciseToolChanged(ClipboardLandscapeToolParams clipboardLandscapeToolParams, ref bool oldValue, ref bool newValue)
		{
			this.UpdateClipboardToolPreciseTool();
		}

		// Token: 0x060000ED RID: 237 RVA: 0x00007D8F File Offset: 0x00006D8F
		private void OnClipboardToolFlipHorisontalChanged(ClipboardLandscapeToolParams clipboardLandscapeToolParams, ref bool oldValue, ref bool newValue)
		{
			this.UpdateClipboardToolFlipHorisontal();
		}

		// Token: 0x060000EE RID: 238 RVA: 0x00007D97 File Offset: 0x00006D97
		private void OnClipboardToolFlipVerticalChanged(ClipboardLandscapeToolParams clipboardLandscapeToolParams, ref bool oldValue, ref bool newValue)
		{
			this.UpdateClipboardToolFlipVertical();
		}

		// Token: 0x060000EF RID: 239 RVA: 0x00007D9F File Offset: 0x00006D9F
		private void OnClipboardToolFileNameChanged(ClipboardLandscapeToolParams clipboardLandscapeToolParams, ref string oldValue, ref string newValue)
		{
			this.UpdateClipboardToolFileName();
		}

		// Token: 0x060000F0 RID: 240 RVA: 0x00007DA7 File Offset: 0x00006DA7
		private void OnClipboardToolGroupChanged(ClipboardLandscapeToolParams clipboardLandscapeToolParams, ref string oldValue, ref string newValue)
		{
			this.UpdateClipboardToolGroup();
		}

		// Token: 0x060000F1 RID: 241 RVA: 0x00007DAF File Offset: 0x00006DAF
		private void OnHillLandscapeToolParamsTwoSidedChanged(HillLandscapeToolParams hillLandscapeToolParams, ref bool oldValue, ref bool newValue)
		{
			this.UpdateHillLandscapeToolTwoSided();
		}

		// Token: 0x060000F2 RID: 242 RVA: 0x00007DB7 File Offset: 0x00006DB7
		private void OnTileItemListRefreshing(ItemList itemList, string item)
		{
			if (this.landscapeTileItemSource != null)
			{
				this.landscapeTileItemSource.Refresh();
			}
		}

		// Token: 0x060000F3 RID: 243 RVA: 0x00007DCC File Offset: 0x00006DCC
		private void OnLandscapeTileItemListItemSelected(ItemList sender, string item)
		{
			if (this.created && !string.IsNullOrEmpty(this.continentName))
			{
				TileLandscapeToolParams tileLandscapeToolParams = this.GetActiveTileLandscapeToolParams();
				if (tileLandscapeToolParams != null)
				{
					this.created = false;
					tileLandscapeToolParams.TileForPaint = LandscapeTileItemSource.GetTileIndex(this.landscapeTileItemList.SelectedItem);
					this.created = true;
				}
			}
		}

		// Token: 0x060000F4 RID: 244 RVA: 0x00007E1C File Offset: 0x00006E1C
		private void OnLandscapeTileItemListItemUnselected(ItemList sender, string item)
		{
			if (this.created && !string.IsNullOrEmpty(this.continentName))
			{
				TileLandscapeToolParams tileLandscapeToolParams = this.GetActiveTileLandscapeToolParams();
				if (tileLandscapeToolParams != null)
				{
					this.created = false;
					tileLandscapeToolParams.TileForPaint = ToolForm.defaultLandscapeTileToolTileForPaint;
					this.created = true;
				}
			}
		}

		// Token: 0x060000F5 RID: 245 RVA: 0x00007E64 File Offset: 0x00006E64
		private void OnLandscapeTileItemListItemListCleared(ItemList sender, string item)
		{
			if (this.created && !string.IsNullOrEmpty(this.continentName))
			{
				TileLandscapeToolParams tileLandscapeToolParams = this.GetActiveTileLandscapeToolParams();
				if (tileLandscapeToolParams != null)
				{
					this.created = false;
					tileLandscapeToolParams.TileForPaint = ToolForm.defaultLandscapeTileToolTileForPaint;
					this.created = true;
				}
			}
		}

		// Token: 0x060000F6 RID: 246 RVA: 0x00007EAC File Offset: 0x00006EAC
		private void OnLandscapeHeightItemListItemSelected(ItemList sender, string item)
		{
			if (this.created)
			{
				HeightLandscapeToolParams heightLandscapeToolParams = this.GetActiveHeightLandscapeToolParams();
				if (heightLandscapeToolParams != null)
				{
					this.created = false;
					heightLandscapeToolParams.FileName = this.landscapeHeightItemList.SelectedItem;
					this.created = true;
				}
			}
		}

		// Token: 0x060000F7 RID: 247 RVA: 0x00007EEC File Offset: 0x00006EEC
		private void OnLandscapeHeightItemListItemUnselected(ItemList sender, string item)
		{
			if (this.created)
			{
				HeightLandscapeToolParams heightLandscapeToolParams = this.GetActiveHeightLandscapeToolParams();
				if (heightLandscapeToolParams != null)
				{
					this.created = false;
					heightLandscapeToolParams.FileName = ToolForm.defaultLandscapeHeightToolFileName;
					this.created = true;
				}
			}
		}

		// Token: 0x060000F8 RID: 248 RVA: 0x00007F24 File Offset: 0x00006F24
		private void OnLandscapeHeightItemListItemListCleared(ItemList sender, string item)
		{
			if (this.created)
			{
				HeightLandscapeToolParams heightLandscapeToolParams = this.GetActiveHeightLandscapeToolParams();
				if (heightLandscapeToolParams != null)
				{
					this.created = false;
					heightLandscapeToolParams.FileName = ToolForm.defaultLandscapeHeightToolFileName;
					this.created = true;
				}
			}
		}

		// Token: 0x060000F9 RID: 249 RVA: 0x00007F5C File Offset: 0x00006F5C
		private void OnLandscapeClipboardItemListItemSelected(ItemList sender, string item)
		{
			if (this.created)
			{
				ClipboardLandscapeToolParams clipboardLandscapeToolParams = this.GetActiveClipboardLandscapeToolParams();
				if (clipboardLandscapeToolParams != null)
				{
					this.created = false;
					clipboardLandscapeToolParams.FileName = this.landscapeClipboardItemList.SelectedItem;
					this.created = true;
				}
			}
		}

		// Token: 0x060000FA RID: 250 RVA: 0x00007F9C File Offset: 0x00006F9C
		private void OnLandscapeClipboardItemListItemUnselected(ItemList sender, string item)
		{
			if (this.created)
			{
				ClipboardLandscapeToolParams clipboardLandscapeToolParams = this.GetActiveClipboardLandscapeToolParams();
				if (clipboardLandscapeToolParams != null)
				{
					this.created = false;
					clipboardLandscapeToolParams.FileName = ToolForm.defaultLandscapeClipboardToolFileName;
					this.created = true;
				}
			}
		}

		// Token: 0x060000FB RID: 251 RVA: 0x00007FD4 File Offset: 0x00006FD4
		private void OnLandscapeClipboardItemListItemListCleared(ItemList sender, string item)
		{
			if (this.created)
			{
				ClipboardLandscapeToolParams clipboardLandscapeToolParams = this.GetActiveClipboardLandscapeToolParams();
				if (clipboardLandscapeToolParams != null)
				{
					this.created = false;
					clipboardLandscapeToolParams.FileName = ToolForm.defaultLandscapeClipboardToolFileName;
					this.created = true;
				}
			}
		}

		// Token: 0x060000FC RID: 252 RVA: 0x0000800C File Offset: 0x0000700C
		private void OnLandscapeHillItemListItemSelected(ItemList sender, string item)
		{
			if (this.created)
			{
				HillLandscapeToolParams hillLandscapeToolParams = this.GetActiveHillLandscapeToolParams();
				if (hillLandscapeToolParams != null)
				{
					this.created = false;
					hillLandscapeToolParams.Clone(LandscapeToolParamsFactory.Load(LandscapeToolType.Hill, EditorEnvironment.EditorFolder + item));
					this.LTPHillPropertyGrid00.Refresh();
					this.LTPHillPropertyGrid01.Refresh();
					this.created = true;
				}
			}
		}

		// Token: 0x060000FD RID: 253 RVA: 0x00008068 File Offset: 0x00007068
		private void OnLandscapeRoadItemListItemSelected(ItemList sender, string item)
		{
			if (this.created)
			{
				RoadLandscapeToolParams roadLandscapeToolParams = this.GetActiveRoadLandscapeToolParams();
				if (roadLandscapeToolParams != null)
				{
					this.created = false;
					roadLandscapeToolParams.RoadParams.Clone(Serializer.Load<RoadParams>(EditorEnvironment.EditorFolder + item));
					this.LTPRoadPropertyGrid.Refresh();
					this.created = true;
				}
			}
		}

		// Token: 0x060000FE RID: 254 RVA: 0x000080BC File Offset: 0x000070BC
		private void OnLandscapeRoadItemListItemDoubleClicked(ItemList sender, string item)
		{
			if (this.created)
			{
				RoadLandscapeToolParams roadLandscapeToolParams = this.GetActiveRoadLandscapeToolParams();
				if (roadLandscapeToolParams != null)
				{
					base.Context.RoadParamsBrowser.RoadParams = roadLandscapeToolParams.RoadParams;
					base.Context.ShowRoadParamsBrowser(true);
				}
			}
		}

		// Token: 0x060000FF RID: 255 RVA: 0x00008100 File Offset: 0x00007100
		private void OnLandscapeWaterItemListItemSelected(ItemList sender, string item)
		{
			if (this.created && !string.IsNullOrEmpty(this.continentName))
			{
				WaterLandscapeToolParams waterLandscapeToolParams = this.GetActiveWaterLandscapeToolParams();
				if (waterLandscapeToolParams != null)
				{
					this.created = false;
					waterLandscapeToolParams.LayerIndex = LandscapeWaterItemSource.GetWaterLayerIndex(item);
					this.created = true;
				}
			}
		}

		// Token: 0x06000100 RID: 256 RVA: 0x00008148 File Offset: 0x00007148
		private void OnLandscapeWaterItemListItemUnselected(ItemList sender, string item)
		{
			if (this.created && !string.IsNullOrEmpty(this.continentName))
			{
				WaterLandscapeToolParams waterLandscapeToolParams = this.GetActiveWaterLandscapeToolParams();
				if (waterLandscapeToolParams != null)
				{
					this.created = false;
					waterLandscapeToolParams.LayerIndex = 0;
					this.created = true;
				}
			}
		}

		// Token: 0x06000101 RID: 257 RVA: 0x0000818C File Offset: 0x0000718C
		private void OnLandscapeWaterItemListItemListCleared(ItemList sender, string item)
		{
			if (this.created && !string.IsNullOrEmpty(this.continentName))
			{
				WaterLandscapeToolParams waterLandscapeToolParams = this.GetActiveWaterLandscapeToolParams();
				if (waterLandscapeToolParams != null)
				{
					this.created = false;
					waterLandscapeToolParams.LayerIndex = 0;
					this.created = true;
				}
			}
		}

		// Token: 0x06000102 RID: 258 RVA: 0x000081CD File Offset: 0x000071CD
		private void OnLandscapeWaterItemListItemDoubleClicked(ItemList sender, string item)
		{
			if (this.created && base.Context.WaterEditorForm != null)
			{
				base.Context.WaterEditorForm.LoadWater(this.continentName, LandscapeWaterItemSource.GetWaterLayerIndex(item));
			}
		}

		// Token: 0x06000103 RID: 259 RVA: 0x00008200 File Offset: 0x00007200
		private void LandscapeTabControl_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (this.created)
			{
				int previousLandscapeSubstate;
				if (!this.landscapeToolTabIndexMap.TryGetValue(this.LandscapeTabControl.SelectedIndex, out previousLandscapeSubstate))
				{
					previousLandscapeSubstate = this.landscapeToolParamsContainer.GetSutableLandscapeSubstate(ToolForm.GetLandscapeToolTypeByTabIndex(this.LandscapeTabControl.SelectedIndex));
				}
				if (previousLandscapeSubstate != -1)
				{
					base.Context.MainState.ActiveSubstate = previousLandscapeSubstate;
					return;
				}
				this.LandscapeTabControl.SelectTab(this.landscapeTabControlSelectedIndexBackup);
			}
		}

		// Token: 0x06000104 RID: 260 RVA: 0x00008274 File Offset: 0x00007274
		private void LTPTileUseAngleRestrictionsCheckBox_CheckedChanged(object sender, EventArgs e)
		{
			if (this.created)
			{
				TileLandscapeToolParams tileLandscapeToolParams = this.GetActiveTileLandscapeToolParams();
				if (tileLandscapeToolParams != null)
				{
					this.created = false;
					tileLandscapeToolParams.AngleRestrictionsParams.UseAngleRestrictions = this.LTPTileUseAngleRestrictionsCheckBox.Checked;
					this.created = true;
				}
			}
		}

		// Token: 0x06000105 RID: 261 RVA: 0x000082B8 File Offset: 0x000072B8
		private void LTPTileAngle00TextBox_TextChanged(object sender, EventArgs e)
		{
			if (this.created)
			{
				TileLandscapeToolParams tileLandscapeToolParams = this.GetActiveTileLandscapeToolParams();
				if (tileLandscapeToolParams != null)
				{
					double degreeAngle;
					if (double.TryParse(this.LTPTileAngle00TextBox.Text, out degreeAngle))
					{
						this.created = false;
						tileLandscapeToolParams.AngleRestrictionsParams.BeginAngle = degreeAngle * 3.141592653589793 / 180.0;
						this.created = true;
					}
					this.editBoxForUpdateNames[this.LTPTileAngle00TextBox.Name] = 0;
					this.EditBoxTimer.Start();
				}
			}
		}

		// Token: 0x06000106 RID: 262 RVA: 0x0000833C File Offset: 0x0000733C
		private void LTPTileAngle01TextBox_TextChanged(object sender, EventArgs e)
		{
			if (this.created)
			{
				TileLandscapeToolParams tileLandscapeToolParams = this.GetActiveTileLandscapeToolParams();
				if (tileLandscapeToolParams != null)
				{
					double degreeAngle;
					if (double.TryParse(this.LTPTileAngle01TextBox.Text, out degreeAngle))
					{
						this.created = false;
						tileLandscapeToolParams.AngleRestrictionsParams.EndAngle = degreeAngle * 3.141592653589793 / 180.0;
						this.created = true;
					}
					this.editBoxForUpdateNames[this.LTPTileAngle01TextBox.Name] = 0;
					this.EditBoxTimer.Start();
				}
			}
		}

		// Token: 0x06000107 RID: 263 RVA: 0x000083C0 File Offset: 0x000073C0
		private void LTPTileAngle00TrackBar_Scroll(object sender, EventArgs e)
		{
			if (this.created)
			{
				TileLandscapeToolParams tileLandscapeToolParams = this.GetActiveTileLandscapeToolParams();
				if (tileLandscapeToolParams != null)
				{
					this.created = false;
					tileLandscapeToolParams.AngleRestrictionsParams.BeginAngle = (double)this.LTPTileAngle00TrackBar.Value * 3.141592653589793 / 180.0;
					this.created = true;
					this.UpdateTileToolAngleRestrictionsBeginAngle();
					this.UpdateTileToolAngleRestrictionsImage();
				}
			}
		}

		// Token: 0x06000108 RID: 264 RVA: 0x00008424 File Offset: 0x00007424
		private void LTPTileAngle01TrackBar_Scroll(object sender, EventArgs e)
		{
			if (this.created)
			{
				TileLandscapeToolParams tileLandscapeToolParams = this.GetActiveTileLandscapeToolParams();
				if (tileLandscapeToolParams != null)
				{
					this.created = false;
					tileLandscapeToolParams.AngleRestrictionsParams.EndAngle = (double)this.LTPTileAngle01TrackBar.Value * 3.141592653589793 / 180.0;
					this.created = true;
					this.UpdateTileToolAngleRestrictionsEndAngle();
					this.UpdateTileToolAngleRestrictionsImage();
				}
			}
		}

		// Token: 0x06000109 RID: 265 RVA: 0x00008488 File Offset: 0x00007488
		private void LTPTileAnglePictureBox_Paint(object sender, PaintEventArgs e)
		{
			if (base.Visible)
			{
				TileLandscapeToolParams tileLandscapeToolParams = this.GetActiveTileLandscapeToolParams();
				if (tileLandscapeToolParams != null)
				{
					double minAngle;
					double maxAngle;
					tileLandscapeToolParams.AngleRestrictionsParams.GetMinMaxAngles(out minAngle, out maxAngle, true);
					ToolForm.DrawMinMaxAngle(minAngle, maxAngle, sender as Control, e.Graphics);
				}
			}
		}

		// Token: 0x0600010A RID: 266 RVA: 0x000084CA File Offset: 0x000074CA
		private void LTPTileAnglePictureBox_Resize(object sender, EventArgs e)
		{
			this.UpdateTileToolAngleRestrictionsImage();
		}

		// Token: 0x0600010B RID: 267 RVA: 0x000084D4 File Offset: 0x000074D4
		private void LTPTileStrengthTrackBar_Scroll(object sender, EventArgs e)
		{
			if (this.created)
			{
				TileLandscapeToolParams tileLandscapeToolParams = this.GetActiveTileLandscapeToolParams();
				if (tileLandscapeToolParams != null)
				{
					this.created = false;
					tileLandscapeToolParams.StrengthSmoothParams.Strength = (double)this.LTPTileStrengthTrackBar.Value / 100.0;
					this.created = true;
					this.UpdateTileToolStrengthSmooth();
					this.UpdateTileToolStrengthSmoothImage();
				}
			}
		}

		// Token: 0x0600010C RID: 268 RVA: 0x00008530 File Offset: 0x00007530
		private void LTPTileSmoothTrackBar_Scroll(object sender, EventArgs e)
		{
			if (this.created)
			{
				TileLandscapeToolParams tileLandscapeToolParams = this.GetActiveTileLandscapeToolParams();
				if (tileLandscapeToolParams != null)
				{
					this.created = false;
					tileLandscapeToolParams.StrengthSmoothParams.Smooth = (double)this.LTPTileSmoothTrackBar.Value / 100.0;
					this.created = true;
					this.UpdateTileToolStrengthSmooth();
					this.UpdateTileToolStrengthSmoothImage();
				}
			}
		}

		// Token: 0x0600010D RID: 269 RVA: 0x0000858C File Offset: 0x0000758C
		private void LTPTileStrengthSmoothTextBox_TextChanged(object sender, EventArgs e)
		{
			if (this.created)
			{
				TileLandscapeToolParams tileLandscapeToolParams = this.GetActiveTileLandscapeToolParams();
				if (tileLandscapeToolParams != null)
				{
					string[] strings = this.LTPTileStrengthSmoothTextBox.Text.Split(new char[]
					{
						' ',
						'/'
					}, StringSplitOptions.RemoveEmptyEntries);
					int strength;
					if (strings.Length > 0 && int.TryParse(strings[0], out strength))
					{
						this.created = false;
						tileLandscapeToolParams.StrengthSmoothParams.Strength = (double)strength / 100.0;
						this.created = true;
					}
					int smooth;
					if (strings.Length > 1 && int.TryParse(strings[1], out smooth))
					{
						this.created = false;
						tileLandscapeToolParams.StrengthSmoothParams.Smooth = (double)smooth / 100.0;
						this.created = true;
					}
					this.editBoxForUpdateNames[this.LTPTileStrengthSmoothTextBox.Name] = 0;
					this.EditBoxTimer.Start();
				}
			}
		}

		// Token: 0x0600010E RID: 270 RVA: 0x0000866C File Offset: 0x0000766C
		private void LTPTileResetStrengthSmoothButton_Click(object sender, EventArgs e)
		{
			if (this.created)
			{
				TileLandscapeToolParams tileLandscapeToolParams = this.GetActiveTileLandscapeToolParams();
				if (tileLandscapeToolParams != null)
				{
					this.created = false;
					tileLandscapeToolParams.StrengthSmoothParams.Reset();
					this.created = true;
					this.UpdateTileToolStrengthSmooth();
					this.UpdateTileToolStrengthSmoothImage();
				}
			}
		}

		// Token: 0x0600010F RID: 271 RVA: 0x000086B0 File Offset: 0x000076B0
		private void LTPTileStrengthSmoothPictureBox_Paint(object sender, PaintEventArgs e)
		{
			if (base.Visible)
			{
				TileLandscapeToolParams tileLandscapeToolParams = this.GetActiveTileLandscapeToolParams();
				if (tileLandscapeToolParams != null)
				{
					ToolForm.DrawStrengthSmooth(tileLandscapeToolParams.StrengthSmoothParams.Strength, tileLandscapeToolParams.StrengthSmoothParams.Smooth, sender as Control, e.Graphics);
				}
			}
		}

		// Token: 0x06000110 RID: 272 RVA: 0x000086F6 File Offset: 0x000076F6
		private void LTPTileStrengthSmoothPictureBox_Resize(object sender, EventArgs e)
		{
			this.UpdateTileToolStrengthSmoothImage();
		}

		// Token: 0x06000111 RID: 273 RVA: 0x00008700 File Offset: 0x00007700
		private void LTPTileAutomaticToolCheckBox_CheckedChanged(object sender, EventArgs e)
		{
			if (this.created)
			{
				TileLandscapeToolParams tileLandscapeToolParams = this.GetActiveTileLandscapeToolParams();
				if (tileLandscapeToolParams != null)
				{
					this.created = false;
					tileLandscapeToolParams.AutomaticTool = this.LTPTileAutomaticToolCheckBox.Checked;
					this.created = true;
				}
			}
		}

		// Token: 0x06000112 RID: 274 RVA: 0x00008740 File Offset: 0x00007740
		private void LTPTileSpotToolCheckBox_CheckedChanged(object sender, EventArgs e)
		{
			if (this.created)
			{
				TileLandscapeToolParams tileLandscapeToolParams = this.GetActiveTileLandscapeToolParams();
				if (tileLandscapeToolParams != null)
				{
					this.created = false;
					tileLandscapeToolParams.SpotTool = this.LTPTileSpotToolCheckBox.Checked;
					this.created = true;
				}
			}
		}

		// Token: 0x06000113 RID: 275 RVA: 0x00008780 File Offset: 0x00007780
		private void LTPTileReplaceToolCheckBox_CheckedChanged(object sender, EventArgs e)
		{
			if (this.created)
			{
				TileLandscapeToolParams tileLandscapeToolParams = this.GetActiveTileLandscapeToolParams();
				if (tileLandscapeToolParams != null)
				{
					this.created = false;
					tileLandscapeToolParams.ReplaceTool = this.LTPTileReplaceToolCheckBox.Checked;
					this.created = true;
				}
			}
		}

		// Token: 0x06000114 RID: 276 RVA: 0x000087C0 File Offset: 0x000077C0
		private void LTPTileReplaceToolTextBox_TextChanged(object sender, EventArgs e)
		{
			if (this.created)
			{
				TileLandscapeToolParams tileLandscapeToolParams = this.GetActiveTileLandscapeToolParams();
				if (tileLandscapeToolParams != null)
				{
					int tileForReplace;
					if (int.TryParse(this.LTPTileReplaceToolTextBox.Text, out tileForReplace))
					{
						this.created = false;
						tileLandscapeToolParams.TileForReplace = tileForReplace;
						this.created = true;
					}
					this.editBoxForUpdateNames[this.LTPTileReplaceToolTextBox.Name] = 0;
					this.EditBoxTimer.Start();
				}
			}
		}

		// Token: 0x06000115 RID: 277 RVA: 0x0000882C File Offset: 0x0000782C
		private void LTPWaterHeightTextBox_TextChanged(object sender, EventArgs e)
		{
			if (this.created)
			{
				WaterLandscapeToolParams waterLandscapeToolParams = this.GetActiveWaterLandscapeToolParams();
				if (waterLandscapeToolParams != null)
				{
					double height;
					if (double.TryParse(this.LTPWaterHeightTextBox.Text, out height))
					{
						this.created = false;
						waterLandscapeToolParams.Height = height;
						this.created = true;
					}
					this.editBoxForUpdateNames[this.LTPWaterHeightTextBox.Name] = 0;
					this.EditBoxTimer.Start();
				}
			}
		}

		// Token: 0x06000116 RID: 278 RVA: 0x00008898 File Offset: 0x00007898
		private void LTPWaterWaterToolButton00_CheckedChanged(object sender, EventArgs e)
		{
			if (this.created && this.LTPWaterWaterToolButton00.Checked)
			{
				WaterLandscapeToolParams waterLandscapeToolParams = this.GetActiveWaterLandscapeToolParams();
				if (waterLandscapeToolParams != null)
				{
					this.created = false;
					waterLandscapeToolParams.WaterToolType = LandscapeWaterToolType.Add;
					this.created = true;
				}
			}
			this.LTPWaterBrushListView.Enabled = this.LTPWaterWaterToolButton00.Checked;
		}

		// Token: 0x06000117 RID: 279 RVA: 0x000088F0 File Offset: 0x000078F0
		private void LTPWaterWaterToolButton01_CheckedChanged(object sender, EventArgs e)
		{
			if (this.created && this.LTPWaterWaterToolButton01.Checked)
			{
				WaterLandscapeToolParams waterLandscapeToolParams = this.GetActiveWaterLandscapeToolParams();
				if (waterLandscapeToolParams != null)
				{
					this.created = false;
					waterLandscapeToolParams.WaterToolType = LandscapeWaterToolType.Remove;
					this.created = true;
				}
			}
		}

		// Token: 0x06000118 RID: 280 RVA: 0x00008934 File Offset: 0x00007934
		private void LTPWaterWaterToolButton02_CheckedChanged(object sender, EventArgs e)
		{
			if (this.created && this.LTPWaterWaterToolButton02.Checked)
			{
				WaterLandscapeToolParams waterLandscapeToolParams = this.GetActiveWaterLandscapeToolParams();
				if (waterLandscapeToolParams != null)
				{
					this.created = false;
					waterLandscapeToolParams.WaterToolType = LandscapeWaterToolType.MoveToHeight;
					this.created = true;
				}
			}
		}

		// Token: 0x06000119 RID: 281 RVA: 0x00008978 File Offset: 0x00007978
		private void LTPWaterWaterToolButton03_CheckedChanged(object sender, EventArgs e)
		{
			if (this.created && this.LTPWaterWaterToolButton03.Checked)
			{
				WaterLandscapeToolParams waterLandscapeToolParams = this.GetActiveWaterLandscapeToolParams();
				if (waterLandscapeToolParams != null)
				{
					this.created = false;
					waterLandscapeToolParams.WaterToolType = LandscapeWaterToolType.PickAndMoveToHeight;
					this.created = true;
				}
			}
		}

		// Token: 0x0600011A RID: 282 RVA: 0x000089BC File Offset: 0x000079BC
		private void LTPHeightStrengthTrackBar_Scroll(object sender, EventArgs e)
		{
			if (this.created)
			{
				HeightLandscapeToolParams heightLandscapeToolParams = this.GetActiveHeightLandscapeToolParams();
				if (heightLandscapeToolParams != null)
				{
					this.created = false;
					heightLandscapeToolParams.StrengthSmoothParams.Strength = (double)this.LTPHeightStrengthTrackBar.Value / 100.0;
					this.created = true;
					this.UpdateHeightToolStrengthSmooth();
					this.UpdateHeightToolStrengthSmoothImage();
				}
			}
		}

		// Token: 0x0600011B RID: 283 RVA: 0x00008A18 File Offset: 0x00007A18
		private void LTPHeightSmoothTrackBar_Scroll(object sender, EventArgs e)
		{
			if (this.created)
			{
				HeightLandscapeToolParams heightLandscapeToolParams = this.GetActiveHeightLandscapeToolParams();
				if (heightLandscapeToolParams != null)
				{
					this.created = false;
					heightLandscapeToolParams.StrengthSmoothParams.Smooth = (double)this.LTPHeightSmoothTrackBar.Value / 100.0;
					this.created = true;
					this.UpdateHeightToolStrengthSmooth();
					this.UpdateHeightToolStrengthSmoothImage();
				}
			}
		}

		// Token: 0x0600011C RID: 284 RVA: 0x00008A74 File Offset: 0x00007A74
		private void LTPHeightStrengthSmoothTextBox_TextChanged(object sender, EventArgs e)
		{
			if (this.created)
			{
				HeightLandscapeToolParams heightLandscapeToolParams = this.GetActiveHeightLandscapeToolParams();
				if (heightLandscapeToolParams != null)
				{
					string[] strings = this.LTPHeightStrengthSmoothTextBox.Text.Split(new char[]
					{
						' ',
						'/'
					}, StringSplitOptions.RemoveEmptyEntries);
					int strength;
					if (strings.Length > 0 && int.TryParse(strings[0], out strength))
					{
						this.created = false;
						heightLandscapeToolParams.StrengthSmoothParams.Strength = (double)strength / 100.0;
						this.created = true;
					}
					int smooth;
					if (strings.Length > 1 && int.TryParse(strings[1], out smooth))
					{
						this.created = false;
						heightLandscapeToolParams.StrengthSmoothParams.Smooth = (double)smooth / 100.0;
						this.created = true;
					}
					this.editBoxForUpdateNames[this.LTPHeightStrengthSmoothTextBox.Name] = 0;
					this.EditBoxTimer.Start();
				}
			}
		}

		// Token: 0x0600011D RID: 285 RVA: 0x00008B54 File Offset: 0x00007B54
		private void LTPHeightResetStrengthSmoothButton_Click(object sender, EventArgs e)
		{
			if (this.created)
			{
				HeightLandscapeToolParams heightLandscapeToolParams = this.GetActiveHeightLandscapeToolParams();
				if (heightLandscapeToolParams != null)
				{
					this.created = false;
					heightLandscapeToolParams.StrengthSmoothParams.Reset();
					this.created = true;
					this.UpdateHeightToolStrengthSmooth();
					this.UpdateHeightToolStrengthSmoothImage();
				}
			}
		}

		// Token: 0x0600011E RID: 286 RVA: 0x00008B98 File Offset: 0x00007B98
		private void LTPHeightStrengthSmoothPictureBox_Paint(object sender, PaintEventArgs e)
		{
			if (base.Visible)
			{
				HeightLandscapeToolParams heightLandscapeToolParams = this.GetActiveHeightLandscapeToolParams();
				if (heightLandscapeToolParams != null)
				{
					ToolForm.DrawStrengthSmooth(heightLandscapeToolParams.StrengthSmoothParams.Strength, heightLandscapeToolParams.StrengthSmoothParams.Smooth, sender as Control, e.Graphics);
				}
			}
		}

		// Token: 0x0600011F RID: 287 RVA: 0x00008BDE File Offset: 0x00007BDE
		private void LTPHeightStrengthSmoothPictureBox_Resize(object sender, EventArgs e)
		{
			this.UpdateHeightToolStrengthSmoothImage();
		}

		// Token: 0x06000120 RID: 288 RVA: 0x00008BE8 File Offset: 0x00007BE8
		private void LTPHeightUpdateObjectsCheckBox_CheckedChanged(object sender, EventArgs e)
		{
			if (this.created)
			{
				HeightLandscapeToolParams heightLandscapeToolParams = this.GetActiveHeightLandscapeToolParams();
				if (heightLandscapeToolParams != null)
				{
					this.created = false;
					heightLandscapeToolParams.UpdateObjectHeightsParams.Update = this.LTPHeightUpdateObjectsCheckBox.Checked;
					this.created = true;
				}
			}
		}

		// Token: 0x06000121 RID: 289 RVA: 0x00008C2C File Offset: 0x00007C2C
		private void LTPHeightUpdateObjectsTextBox_TextChanged(object sender, EventArgs e)
		{
			if (this.created)
			{
				HeightLandscapeToolParams heightLandscapeToolParams = this.GetActiveHeightLandscapeToolParams();
				if (heightLandscapeToolParams != null)
				{
					double heightRange;
					if (double.TryParse(this.LTPHeightUpdateObjectsTextBox.Text, out heightRange))
					{
						this.created = false;
						heightLandscapeToolParams.UpdateObjectHeightsParams.HeightRange = heightRange;
						this.created = true;
					}
					this.editBoxForUpdateNames[this.LTPHeightUpdateObjectsTextBox.Name] = 0;
					this.EditBoxTimer.Start();
				}
			}
		}

		// Token: 0x06000122 RID: 290 RVA: 0x00008C9B File Offset: 0x00007C9B
		private void LTPHeightHeightToolButton00_CheckedChanged(object sender, EventArgs e)
		{
			if (this.created && this.LTPHeightHeightToolButton00.Checked)
			{
				this.SetHeightToolType();
			}
		}

		// Token: 0x06000123 RID: 291 RVA: 0x00008CB8 File Offset: 0x00007CB8
		private void LTPHeightHeightToolButton01_CheckedChanged(object sender, EventArgs e)
		{
			if (this.created && this.LTPHeightHeightToolButton01.Checked)
			{
				this.SetHeightToolType();
			}
		}

		// Token: 0x06000124 RID: 292 RVA: 0x00008CD5 File Offset: 0x00007CD5
		private void LTPHeightHeightToolButton02_CheckedChanged(object sender, EventArgs e)
		{
			if (this.created && this.LTPHeightHeightToolButton02.Checked)
			{
				this.SetHeightToolType();
			}
		}

		// Token: 0x06000125 RID: 293 RVA: 0x00008CF2 File Offset: 0x00007CF2
		private void LTPHeightHeightToolButton03_CheckedChanged(object sender, EventArgs e)
		{
			if (this.created && this.LTPHeightHeightToolButton03.Checked)
			{
				this.SetHeightToolType();
			}
		}

		// Token: 0x06000126 RID: 294 RVA: 0x00008D0F File Offset: 0x00007D0F
		private void LTPHeightHeightToolButton04_CheckedChanged(object sender, EventArgs e)
		{
			if (this.created && this.LTPHeightHeightToolButton04.Checked)
			{
				this.SetHeightToolType();
			}
		}

		// Token: 0x06000127 RID: 295 RVA: 0x00008D2C File Offset: 0x00007D2C
		private void LTPHeightHeightToolButton05_CheckedChanged(object sender, EventArgs e)
		{
			if (this.created && this.LTPHeightHeightToolButton05.Checked)
			{
				this.SetHeightToolType();
			}
		}

		// Token: 0x06000128 RID: 296 RVA: 0x00008D4C File Offset: 0x00007D4C
		private void LTPHeightRaisePlatoCheckBox_CheckedChanged(object sender, EventArgs e)
		{
			if (this.created)
			{
				HeightLandscapeToolParams heightLandscapeToolParams = this.GetActiveHeightLandscapeToolParams();
				if (heightLandscapeToolParams != null)
				{
					this.created = false;
					if (this.LTPHeightRaisePlatoCheckBox.Checked)
					{
						heightLandscapeToolParams.HeightPlatoToolType = LandscapeHeightPlatoToolType.Raise;
						this.LTPHeightLowerPlatoCheckBox.Checked = false;
					}
					else if (this.LTPHeightLowerPlatoCheckBox.Checked)
					{
						heightLandscapeToolParams.HeightPlatoToolType = LandscapeHeightPlatoToolType.Lower;
					}
					else
					{
						heightLandscapeToolParams.HeightPlatoToolType = LandscapeHeightPlatoToolType.Both;
					}
					this.created = true;
				}
			}
		}

		// Token: 0x06000129 RID: 297 RVA: 0x00008DB8 File Offset: 0x00007DB8
		private void LTPHeightLowerPlatoCheckBox_CheckedChanged(object sender, EventArgs e)
		{
			if (this.created)
			{
				HeightLandscapeToolParams heightLandscapeToolParams = this.GetActiveHeightLandscapeToolParams();
				if (heightLandscapeToolParams != null)
				{
					this.created = false;
					if (this.LTPHeightLowerPlatoCheckBox.Checked)
					{
						heightLandscapeToolParams.HeightPlatoToolType = LandscapeHeightPlatoToolType.Lower;
						this.LTPHeightRaisePlatoCheckBox.Checked = false;
					}
					else if (this.LTPHeightRaisePlatoCheckBox.Checked)
					{
						heightLandscapeToolParams.HeightPlatoToolType = LandscapeHeightPlatoToolType.Raise;
					}
					else
					{
						heightLandscapeToolParams.HeightPlatoToolType = LandscapeHeightPlatoToolType.Both;
					}
					this.created = true;
				}
			}
		}

		// Token: 0x0600012A RID: 298 RVA: 0x00008E24 File Offset: 0x00007E24
		private void LTPHeightHeightTextBox_TextChanged(object sender, EventArgs e)
		{
			if (this.created)
			{
				HeightLandscapeToolParams heightLandscapeToolParams = this.GetActiveHeightLandscapeToolParams();
				if (heightLandscapeToolParams != null)
				{
					double height;
					if (double.TryParse(this.LTPHeightHeightTextBox.Text, out height))
					{
						this.created = false;
						heightLandscapeToolParams.Height = height;
						this.created = true;
					}
					this.editBoxForUpdateNames[this.LTPHeightHeightTextBox.Name] = 0;
					this.EditBoxTimer.Start();
				}
			}
		}

		// Token: 0x0600012B RID: 299 RVA: 0x00008E90 File Offset: 0x00007E90
		private void LTPHeightPreciseToolCheckBox_CheckedChanged(object sender, EventArgs e)
		{
			if (this.created)
			{
				HeightLandscapeToolParams heightLandscapeToolParams = this.GetActiveHeightLandscapeToolParams();
				if (heightLandscapeToolParams != null)
				{
					this.created = false;
					heightLandscapeToolParams.PreciseTool = this.LTPHeightPreciseToolCheckBox.Checked;
					this.created = true;
				}
			}
		}

		// Token: 0x0600012C RID: 300 RVA: 0x00008ED0 File Offset: 0x00007ED0
		private void LTPHeightAutomaticToolCheckBox_CheckedChanged(object sender, EventArgs e)
		{
			if (this.created)
			{
				HeightLandscapeToolParams heightLandscapeToolParams = this.GetActiveHeightLandscapeToolParams();
				if (heightLandscapeToolParams != null)
				{
					this.created = false;
					heightLandscapeToolParams.AutomaticTool = this.LTPHeightAutomaticToolCheckBox.Checked;
					this.created = true;
				}
			}
		}

		// Token: 0x0600012D RID: 301 RVA: 0x00008F10 File Offset: 0x00007F10
		private void LTPTilePickerRadioButton00_CheckedChanged(object sender, EventArgs e)
		{
			if (this.created && this.LTPTilePickerRadioButton00.Checked)
			{
				TilePickerLandscapeToolParams tilePickerLandscapeToolParams = this.GetActiveTilePickerLandscapeToolParams();
				if (tilePickerLandscapeToolParams != null)
				{
					this.created = false;
					tilePickerLandscapeToolParams.SelectedIndex = 0;
					this.created = true;
					this.UpdateTilePickerSelectedIndex();
				}
			}
		}

		// Token: 0x0600012E RID: 302 RVA: 0x00008F58 File Offset: 0x00007F58
		private void LTPTilePickerRadioButton01_CheckedChanged(object sender, EventArgs e)
		{
			if (this.created && this.LTPTilePickerRadioButton01.Checked)
			{
				TilePickerLandscapeToolParams tilePickerLandscapeToolParams = this.GetActiveTilePickerLandscapeToolParams();
				if (tilePickerLandscapeToolParams != null)
				{
					this.created = false;
					tilePickerLandscapeToolParams.SelectedIndex = 1;
					this.created = true;
					this.UpdateTilePickerSelectedIndex();
				}
			}
		}

		// Token: 0x0600012F RID: 303 RVA: 0x00008FA0 File Offset: 0x00007FA0
		private void LTPTilePickerRadioButton02_CheckedChanged(object sender, EventArgs e)
		{
			if (this.created && this.LTPTilePickerRadioButton02.Checked)
			{
				TilePickerLandscapeToolParams tilePickerLandscapeToolParams = this.GetActiveTilePickerLandscapeToolParams();
				if (tilePickerLandscapeToolParams != null)
				{
					this.created = false;
					tilePickerLandscapeToolParams.SelectedIndex = 2;
					this.created = true;
					this.UpdateTilePickerSelectedIndex();
				}
			}
		}

		// Token: 0x06000130 RID: 304 RVA: 0x00008FE8 File Offset: 0x00007FE8
		private void LTPTilePickerRadioButton03_CheckedChanged(object sender, EventArgs e)
		{
			if (this.created && this.LTPTilePickerRadioButton03.Checked)
			{
				TilePickerLandscapeToolParams tilePickerLandscapeToolParams = this.GetActiveTilePickerLandscapeToolParams();
				if (tilePickerLandscapeToolParams != null)
				{
					this.created = false;
					tilePickerLandscapeToolParams.SelectedIndex = 3;
					this.created = true;
					this.UpdateTilePickerSelectedIndex();
				}
			}
		}

		// Token: 0x06000131 RID: 305 RVA: 0x00009030 File Offset: 0x00008030
		private void LTPClipboardStrengthTrackBar_Scroll(object sender, EventArgs e)
		{
			if (this.created)
			{
				ClipboardLandscapeToolParams clipboardLandscapeToolParams = this.GetActiveClipboardLandscapeToolParams();
				if (clipboardLandscapeToolParams != null)
				{
					this.created = false;
					clipboardLandscapeToolParams.StrengthSmoothParams.Strength = (double)this.LTPClipboardStrengthTrackBar.Value / 100.0;
					this.created = true;
					this.UpdateClipboardToolStrengthSmooth();
					this.UpdateClipboardToolStrengthSmoothImage();
				}
			}
		}

		// Token: 0x06000132 RID: 306 RVA: 0x0000908C File Offset: 0x0000808C
		private void LTPClipboardSmoothTrackBar_Scroll(object sender, EventArgs e)
		{
			if (this.created)
			{
				ClipboardLandscapeToolParams clipboardLandscapeToolParams = this.GetActiveClipboardLandscapeToolParams();
				if (clipboardLandscapeToolParams != null)
				{
					this.created = false;
					clipboardLandscapeToolParams.StrengthSmoothParams.Smooth = (double)this.LTPClipboardSmoothTrackBar.Value / 100.0;
					this.created = true;
					this.UpdateClipboardToolStrengthSmooth();
					this.UpdateClipboardToolStrengthSmoothImage();
				}
			}
		}

		// Token: 0x06000133 RID: 307 RVA: 0x000090E8 File Offset: 0x000080E8
		private void LTPClipboardStrengthSmoothTextBox_TextChanged(object sender, EventArgs e)
		{
			if (this.created)
			{
				ClipboardLandscapeToolParams clipboardLandscapeToolParams = this.GetActiveClipboardLandscapeToolParams();
				if (clipboardLandscapeToolParams != null)
				{
					string[] strings = this.LTPClipboardStrengthSmoothTextBox.Text.Split(new char[]
					{
						' ',
						'/'
					}, StringSplitOptions.RemoveEmptyEntries);
					int strength;
					if (strings.Length > 0 && int.TryParse(strings[0], out strength))
					{
						this.created = false;
						clipboardLandscapeToolParams.StrengthSmoothParams.Strength = (double)strength / 100.0;
						this.created = true;
					}
					int smooth;
					if (strings.Length > 1 && int.TryParse(strings[1], out smooth))
					{
						this.created = false;
						clipboardLandscapeToolParams.StrengthSmoothParams.Smooth = (double)smooth / 100.0;
						this.created = true;
					}
					this.editBoxForUpdateNames[this.LTPClipboardStrengthSmoothTextBox.Name] = 0;
					this.EditBoxTimer.Start();
				}
			}
		}

		// Token: 0x06000134 RID: 308 RVA: 0x000091C8 File Offset: 0x000081C8
		private void LTPClipboardResetStrengthSmoothButton_Click(object sender, EventArgs e)
		{
			if (this.created)
			{
				ClipboardLandscapeToolParams clipboardLandscapeToolParams = this.GetActiveClipboardLandscapeToolParams();
				if (clipboardLandscapeToolParams != null)
				{
					this.created = false;
					clipboardLandscapeToolParams.StrengthSmoothParams.Reset();
					this.created = true;
					this.UpdateClipboardToolStrengthSmooth();
					this.UpdateClipboardToolStrengthSmoothImage();
				}
			}
		}

		// Token: 0x06000135 RID: 309 RVA: 0x0000920C File Offset: 0x0000820C
		private void LTPClipboardStrengthSmoothPictureBox_Paint(object sender, PaintEventArgs e)
		{
			if (base.Visible)
			{
				ClipboardLandscapeToolParams clipboardLandscapeToolParams = this.GetActiveClipboardLandscapeToolParams();
				if (clipboardLandscapeToolParams != null)
				{
					ToolForm.DrawStrengthSmooth(clipboardLandscapeToolParams.StrengthSmoothParams.Strength, clipboardLandscapeToolParams.StrengthSmoothParams.Smooth, sender as Control, e.Graphics);
				}
			}
		}

		// Token: 0x06000136 RID: 310 RVA: 0x00009252 File Offset: 0x00008252
		private void LTPClipboardStrengthSmoothPictureBox_Resize(object sender, EventArgs e)
		{
			this.UpdateClipboardToolStrengthSmoothImage();
		}

		// Token: 0x06000137 RID: 311 RVA: 0x0000925C File Offset: 0x0000825C
		private void LTPClipboardUpdateObjectsCheckBox_CheckedChanged(object sender, EventArgs e)
		{
			if (this.created)
			{
				ClipboardLandscapeToolParams clipboardLandscapeToolParams = this.GetActiveClipboardLandscapeToolParams();
				if (clipboardLandscapeToolParams != null)
				{
					this.created = false;
					clipboardLandscapeToolParams.UpdateObjectHeightsParams.Update = this.LTPClipboardUpdateObjectsCheckBox.Checked;
					this.created = true;
				}
			}
		}

		// Token: 0x06000138 RID: 312 RVA: 0x000092A0 File Offset: 0x000082A0
		private void LTPClipboardUpdateObjectsTextBox_TextChanged(object sender, EventArgs e)
		{
			if (this.created)
			{
				ClipboardLandscapeToolParams clipboardLandscapeToolParams = this.GetActiveClipboardLandscapeToolParams();
				if (clipboardLandscapeToolParams != null)
				{
					double heightRange;
					if (double.TryParse(this.LTPClipboardUpdateObjectsTextBox.Text, out heightRange))
					{
						this.created = false;
						clipboardLandscapeToolParams.UpdateObjectHeightsParams.HeightRange = heightRange;
						this.created = true;
					}
					this.editBoxForUpdateNames[this.LTPClipboardUpdateObjectsTextBox.Name] = 0;
					this.EditBoxTimer.Start();
				}
			}
		}

		// Token: 0x06000139 RID: 313 RVA: 0x00009310 File Offset: 0x00008310
		private void LTPClipboardTwoSidedCheckBox_CheckedChanged(object sender, EventArgs e)
		{
			if (this.created)
			{
				ClipboardLandscapeToolParams clipboardLandscapeToolParams = this.GetActiveClipboardLandscapeToolParams();
				if (clipboardLandscapeToolParams != null)
				{
					this.created = false;
					clipboardLandscapeToolParams.TwoSided = this.LTPClipboardTwoSidedCheckBox.Checked;
					this.created = true;
				}
			}
		}

		// Token: 0x0600013A RID: 314 RVA: 0x00009350 File Offset: 0x00008350
		private void LTPClipboardCopyTypeButton00_CheckedChanged(object sender, EventArgs e)
		{
			if (this.created)
			{
				ClipboardLandscapeToolParams clipboardLandscapeToolParams = this.GetActiveClipboardLandscapeToolParams();
				if (clipboardLandscapeToolParams != null)
				{
					this.created = false;
					clipboardLandscapeToolParams.CopyTiles = this.LTPClipboardCopyTypeButton00.Checked;
					this.created = true;
				}
			}
		}

		// Token: 0x0600013B RID: 315 RVA: 0x00009390 File Offset: 0x00008390
		private void LTPClipboardCopyTypeButton01_CheckedChanged(object sender, EventArgs e)
		{
			if (this.created)
			{
				ClipboardLandscapeToolParams clipboardLandscapeToolParams = this.GetActiveClipboardLandscapeToolParams();
				if (clipboardLandscapeToolParams != null)
				{
					this.created = false;
					clipboardLandscapeToolParams.CopyHeights = this.LTPClipboardCopyTypeButton01.Checked;
					this.created = true;
				}
			}
		}

		// Token: 0x0600013C RID: 316 RVA: 0x000093D0 File Offset: 0x000083D0
		private void LTPClipboardCopyTypeButton02_CheckedChanged(object sender, EventArgs e)
		{
			if (this.created)
			{
				ClipboardLandscapeToolParams clipboardLandscapeToolParams = this.GetActiveClipboardLandscapeToolParams();
				if (clipboardLandscapeToolParams != null)
				{
					this.created = false;
					clipboardLandscapeToolParams.CopyObjects = this.LTPClipboardCopyTypeButton02.Checked;
					this.created = true;
				}
			}
		}

		// Token: 0x0600013D RID: 317 RVA: 0x00009410 File Offset: 0x00008410
		private void LTPClipboardCopyHeightTypeButton00_CheckedChanged(object sender, EventArgs e)
		{
			if (this.created && this.LTPClipboardCopyHeightTypeButton00.Checked)
			{
				ClipboardLandscapeToolParams clipboardLandscapeToolParams = this.GetActiveClipboardLandscapeToolParams();
				if (clipboardLandscapeToolParams != null)
				{
					this.created = false;
					clipboardLandscapeToolParams.CopyHeightType = LandscapeCopyHeightType.Adaptive;
					this.created = true;
				}
			}
		}

		// Token: 0x0600013E RID: 318 RVA: 0x00009454 File Offset: 0x00008454
		private void LTPClipboardCopyHeightTypeButton01_CheckedChanged(object sender, EventArgs e)
		{
			if (this.created && this.LTPClipboardCopyHeightTypeButton01.Checked)
			{
				ClipboardLandscapeToolParams clipboardLandscapeToolParams = this.GetActiveClipboardLandscapeToolParams();
				if (clipboardLandscapeToolParams != null)
				{
					this.created = false;
					clipboardLandscapeToolParams.CopyHeightType = LandscapeCopyHeightType.Min;
					this.created = true;
				}
			}
		}

		// Token: 0x0600013F RID: 319 RVA: 0x00009498 File Offset: 0x00008498
		private void LTPClipboardCopyHeightTypeButton02_CheckedChanged(object sender, EventArgs e)
		{
			if (this.created && this.LTPClipboardCopyHeightTypeButton02.Checked)
			{
				ClipboardLandscapeToolParams clipboardLandscapeToolParams = this.GetActiveClipboardLandscapeToolParams();
				if (clipboardLandscapeToolParams != null)
				{
					this.created = false;
					clipboardLandscapeToolParams.CopyHeightType = LandscapeCopyHeightType.Max;
					this.created = true;
				}
			}
		}

		// Token: 0x06000140 RID: 320 RVA: 0x000094DC File Offset: 0x000084DC
		private void LTPClipboardCopyHeightTypeButton03_CheckedChanged(object sender, EventArgs e)
		{
			if (this.created && this.LTPClipboardCopyHeightTypeButton03.Checked)
			{
				ClipboardLandscapeToolParams clipboardLandscapeToolParams = this.GetActiveClipboardLandscapeToolParams();
				if (clipboardLandscapeToolParams != null)
				{
					this.created = false;
					clipboardLandscapeToolParams.CopyHeightType = LandscapeCopyHeightType.Precise;
					this.created = true;
				}
			}
		}

		// Token: 0x06000141 RID: 321 RVA: 0x00009520 File Offset: 0x00008520
		private void LTPClipboardPreciseToolCheckBox_CheckedChanged(object sender, EventArgs e)
		{
			if (this.created)
			{
				ClipboardLandscapeToolParams clipboardLandscapeToolParams = this.GetActiveClipboardLandscapeToolParams();
				if (clipboardLandscapeToolParams != null)
				{
					this.created = false;
					clipboardLandscapeToolParams.PreciseTool = this.LTPClipboardPreciseToolCheckBox.Checked;
					this.created = true;
				}
			}
		}

		// Token: 0x06000142 RID: 322 RVA: 0x00009560 File Offset: 0x00008560
		private void LTPClipboardFlipHorisontalCheckBox_CheckedChanged(object sender, EventArgs e)
		{
			if (this.created)
			{
				ClipboardLandscapeToolParams clipboardLandscapeToolParams = this.GetActiveClipboardLandscapeToolParams();
				if (clipboardLandscapeToolParams != null)
				{
					this.created = false;
					clipboardLandscapeToolParams.FlipHorisontal = this.LTPClipboardFlipHorisontalCheckBox.Checked;
					this.created = true;
				}
			}
		}

		// Token: 0x06000143 RID: 323 RVA: 0x000095A0 File Offset: 0x000085A0
		private void LTPClipboardFlipVerticalCheckBox_CheckedChanged(object sender, EventArgs e)
		{
			if (this.created)
			{
				ClipboardLandscapeToolParams clipboardLandscapeToolParams = this.GetActiveClipboardLandscapeToolParams();
				if (clipboardLandscapeToolParams != null)
				{
					this.created = false;
					clipboardLandscapeToolParams.FlipVertical = this.LTPClipboardFlipVerticalCheckBox.Checked;
					this.created = true;
				}
			}
		}

		// Token: 0x06000144 RID: 324 RVA: 0x000095E0 File Offset: 0x000085E0
		private void LTPClipboardGroupTextBox_TextChanged(object sender, EventArgs e)
		{
			if (this.created)
			{
				ClipboardLandscapeToolParams clipboardLandscapeToolParams = this.GetActiveClipboardLandscapeToolParams();
				if (clipboardLandscapeToolParams != null)
				{
					clipboardLandscapeToolParams.Group = this.LTPClipboardGroupTextBox.Text;
				}
			}
		}

		// Token: 0x06000145 RID: 325 RVA: 0x00009610 File Offset: 0x00008610
		private void LTPHillTwoSidedCheckBox_CheckedChanged(object sender, EventArgs e)
		{
			if (this.created)
			{
				HillLandscapeToolParams hillLandscapeToolParams = this.GetActiveHillLandscapeToolParams();
				if (hillLandscapeToolParams != null)
				{
					this.created = false;
					hillLandscapeToolParams.TwoSided = this.LTPHillTwoSidedCheckBox.Checked;
					this.created = true;
				}
			}
		}

		// Token: 0x06000146 RID: 326 RVA: 0x00009650 File Offset: 0x00008650
		private void LTPHillUndoButton_Click(object sender, EventArgs e)
		{
			if (this.created)
			{
				HillLandscapeToolParams hillLandscapeToolParams = this.GetActiveHillLandscapeToolParams();
				if (hillLandscapeToolParams != null)
				{
					base.Context.StateContainer.Invoke("_hill_undo", default(MethodArgs));
				}
			}
		}

		// Token: 0x06000147 RID: 327 RVA: 0x00009690 File Offset: 0x00008690
		private void LTPHillApplyButton_Click(object sender, EventArgs e)
		{
			if (this.created)
			{
				HillLandscapeToolParams hillLandscapeToolParams = this.GetActiveHillLandscapeToolParams();
				if (hillLandscapeToolParams != null)
				{
					base.Context.StateContainer.Invoke("_hill_apply", default(MethodArgs));
				}
			}
		}

		// Token: 0x06000148 RID: 328 RVA: 0x000096D0 File Offset: 0x000086D0
		private void LTPHillMakeButton_Click(object sender, EventArgs e)
		{
			if (this.created)
			{
				HillLandscapeToolParams hillLandscapeToolParams = this.GetActiveHillLandscapeToolParams();
				if (hillLandscapeToolParams != null)
				{
					base.Context.StateContainer.Invoke("_hill_create", default(MethodArgs));
				}
			}
		}

		// Token: 0x06000149 RID: 329 RVA: 0x00009710 File Offset: 0x00008710
		private void LTPHillSetDefaultButton_Click(object sender, EventArgs e)
		{
			if (this.created)
			{
				HillLandscapeToolParams hillLandscapeToolParams = this.GetActiveHillLandscapeToolParams();
				if (hillLandscapeToolParams != null)
				{
					hillLandscapeToolParams.SetDefaultValues();
					this.LTPHillPropertyGrid00.Refresh();
					this.LTPHillPropertyGrid01.Refresh();
				}
			}
		}

		// Token: 0x0600014A RID: 330 RVA: 0x0000974C File Offset: 0x0000874C
		private void LTPHillSaveButton_Click(object sender, EventArgs e)
		{
			if (this.created)
			{
				HillLandscapeToolParams hillLandscapeToolParams = this.GetActiveHillLandscapeToolParams();
				if (hillLandscapeToolParams != null)
				{
					SaveFileDialog saveFileDialog = new SaveFileDialog();
					saveFileDialog.Filter = "XML Files|*.xml|All Files|*.*";
					saveFileDialog.RestoreDirectory = true;
					saveFileDialog.InitialDirectory = (EditorEnvironment.EditorFolder + LandscapeHillItemSource.Folder).Replace('/', '\\');
					if (!Directory.Exists(saveFileDialog.InitialDirectory))
					{
						Directory.CreateDirectory(saveFileDialog.InitialDirectory);
					}
					saveFileDialog.CheckFileExists = false;
					if (saveFileDialog.ShowDialog(this) == DialogResult.OK)
					{
						string fileName = saveFileDialog.FileName.Replace('\\', '/');
						LandscapeToolParamsFactory.Save(hillLandscapeToolParams, fileName);
						this.landscapeHillItemList.Refresh();
					}
				}
			}
		}

		// Token: 0x0600014B RID: 331 RVA: 0x000097F4 File Offset: 0x000087F4
		private void LTPRoadDefaultsButton_Click(object sender, EventArgs e)
		{
			if (this.created)
			{
				RoadLandscapeToolParams roadLandscapeToolParams = this.GetActiveRoadLandscapeToolParams();
				if (roadLandscapeToolParams != null)
				{
					roadLandscapeToolParams.SetDefaultValues();
					this.LTPRoadPropertyGrid.Refresh();
				}
			}
		}

		// Token: 0x0600014C RID: 332 RVA: 0x00009824 File Offset: 0x00008824
		private void LTPRoadClearButton_Click(object sender, EventArgs e)
		{
			if (this.created)
			{
				RoadLandscapeToolParams roadLandscapeToolParams = this.GetActiveRoadLandscapeToolParams();
				if (roadLandscapeToolParams != null)
				{
					base.Context.StateContainer.Invoke("_road_undo", default(MethodArgs));
				}
			}
		}

		// Token: 0x0600014D RID: 333 RVA: 0x00009864 File Offset: 0x00008864
		private void LTPRoadCreateButton_Click(object sender, EventArgs e)
		{
			if (this.created)
			{
				RoadLandscapeToolParams roadLandscapeToolParams = this.GetActiveRoadLandscapeToolParams();
				if (roadLandscapeToolParams != null)
				{
					base.Context.StateContainer.Invoke("_road_create", default(MethodArgs));
				}
			}
		}

		// Token: 0x0600014E RID: 334 RVA: 0x000098A4 File Offset: 0x000088A4
		private void LTPRoadApplyButton_Click(object sender, EventArgs e)
		{
			if (this.created)
			{
				RoadLandscapeToolParams roadLandscapeToolParams = this.GetActiveRoadLandscapeToolParams();
				if (roadLandscapeToolParams != null)
				{
					base.Context.StateContainer.Invoke("_road_apply", default(MethodArgs));
				}
			}
		}

		// Token: 0x0600014F RID: 335 RVA: 0x000098E4 File Offset: 0x000088E4
		private void LTPRoadSaveButton_Click(object sender, EventArgs e)
		{
			if (this.created)
			{
				RoadLandscapeToolParams roadLandscapeToolParams = this.GetActiveRoadLandscapeToolParams();
				if (roadLandscapeToolParams != null)
				{
					SaveFileDialog saveFileDialog = new SaveFileDialog();
					saveFileDialog.Filter = "XML Files|*.xml|All Files|*.*";
					saveFileDialog.RestoreDirectory = true;
					saveFileDialog.InitialDirectory = (EditorEnvironment.EditorFolder + LandscapeRoadItemSource.Folder).Replace('/', '\\');
					if (!Directory.Exists(saveFileDialog.InitialDirectory))
					{
						Directory.CreateDirectory(saveFileDialog.InitialDirectory);
					}
					saveFileDialog.CheckFileExists = false;
					if (saveFileDialog.ShowDialog(this) == DialogResult.OK)
					{
						string fileName = saveFileDialog.FileName.Replace('\\', '/');
						Serializer.Save(fileName, roadLandscapeToolParams.RoadParams, false);
						this.landscapeRoadItemList.Refresh();
					}
				}
			}
		}

		// Token: 0x06000150 RID: 336 RVA: 0x00009992 File Offset: 0x00008992
		private void LTPTileBrushButton_Click(object sender, EventArgs e)
		{
			if (!string.IsNullOrEmpty(this.continentName))
			{
				this.landscapeTileFolderItemFiltes.ShowDialog(this.landscapeTileItemList, base.Context.MainForm);
			}
		}

		// Token: 0x06000151 RID: 337 RVA: 0x000099BE File Offset: 0x000089BE
		private void LTPHeightBrushButton_Click(object sender, EventArgs e)
		{
			if (this.landscapeHeightItemList != null && this.landscapeHeightFolderItemFiltes != null)
			{
				this.landscapeHeightFolderItemFiltes.ShowDialog(this.landscapeHeightItemList, base.Context.MainForm);
			}
		}

		// Token: 0x06000152 RID: 338 RVA: 0x000099ED File Offset: 0x000089ED
		private void LTPClipboardBrushButton_Click(object sender, EventArgs e)
		{
			if (this.landscapeClipboardItemList != null && this.landscapeClipboardFolderItemFiltes != null)
			{
				this.landscapeClipboardFolderItemFiltes.ShowDialog(this.landscapeClipboardItemList, base.Context.MainForm);
			}
		}

		// Token: 0x06000153 RID: 339 RVA: 0x00009A1C File Offset: 0x00008A1C
		private void LTPHillPresetButton_Click(object sender, EventArgs e)
		{
			if (this.landscapeHillItemList != null && this.landscapeHillFolderItemFiltes != null)
			{
				this.landscapeHillFolderItemFiltes.ShowDialog(this.landscapeHillItemList, base.Context.MainForm);
			}
		}

		// Token: 0x06000154 RID: 340 RVA: 0x00009A4B File Offset: 0x00008A4B
		private void LTPRoadPresetButton_Click(object sender, EventArgs e)
		{
			if (this.landscapeRoadItemList != null && this.landscapeRoadFolderItemFiltes != null)
			{
				this.landscapeRoadFolderItemFiltes.ShowDialog(this.landscapeRoadItemList, base.Context.MainForm);
			}
		}

		// Token: 0x06000155 RID: 341 RVA: 0x00009A7A File Offset: 0x00008A7A
		private void LTPWaterBrushButton_Click(object sender, EventArgs e)
		{
			if (!string.IsNullOrEmpty(this.continentName))
			{
				this.landscapeWaterFolderItemFiltes.ShowDialog(this.landscapeWaterItemList, base.Context.MainForm);
			}
		}

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x06000156 RID: 342 RVA: 0x00009AA6 File Offset: 0x00008AA6
		public int LandscapeMainStateIndex
		{
			get
			{
				return ToolForm.landscapeMainStateIndex;
			}
		}

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x06000157 RID: 343 RVA: 0x00009AB0 File Offset: 0x00008AB0
		public int ActiveLandscapeSubstate
		{
			get
			{
				if (ToolForm.landscapeMainStateIndex >= 0 && ToolForm.landscapeMainStateIndex < base.Context.MainState.ActiveSubstates.Count)
				{
					return base.Context.MainState.ActiveSubstates[ToolForm.landscapeMainStateIndex];
				}
				return -1;
			}
		}

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x06000158 RID: 344 RVA: 0x00009B00 File Offset: 0x00008B00
		public int ActiveLandscapeToolParamsIndex
		{
			get
			{
				if (ToolForm.landscapeMainStateIndex >= 0 && ToolForm.landscapeMainStateIndex < base.Context.MainState.ActiveSubstates.Count)
				{
					return base.Context.MainState.ActiveSubstates[ToolForm.landscapeMainStateIndex] % this.landscapeToolParamsContainer.Count;
				}
				return -1;
			}
		}

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x06000159 RID: 345 RVA: 0x00009B59 File Offset: 0x00008B59
		public LandscapeRegionParams LandscapeRegionParams
		{
			get
			{
				return this.landscapeRegionParams;
			}
		}

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x0600015A RID: 346 RVA: 0x00009B61 File Offset: 0x00008B61
		public LandscapeToolParamsContainer LandscapeToolParamsContainer
		{
			get
			{
				return this.landscapeToolParamsContainer;
			}
		}

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x0600015B RID: 347 RVA: 0x00009B69 File Offset: 0x00008B69
		public SimpleTimer LeftLandscapeSimpleTimer
		{
			get
			{
				return this.leftLandscapeSimpleTimer;
			}
		}

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x0600015C RID: 348 RVA: 0x00009B71 File Offset: 0x00008B71
		public SimpleTimer RightLandscapeSimpleTimer
		{
			get
			{
				return this.rightLandscapeSimpleTimer;
			}
		}

		// Token: 0x0600015D RID: 349 RVA: 0x00009B7C File Offset: 0x00008B7C
		private static void DrawMinMaxAngle(double minAngle, double maxAngle, Control control, Graphics graphics)
		{
			if (control != null && graphics != null)
			{
				Rectangle clientRectangle = control.ClientRectangle;
				if (clientRectangle.Width > 0 && clientRectangle.Height > 0)
				{
					Bitmap clientBitmap = new Bitmap(clientRectangle.Width, clientRectangle.Height);
					Graphics clientBitmapGraphics = Graphics.FromImage(clientBitmap);
					double greenRatio = Math.Sqrt(2.0);
					double whiteRatio = 0.85;
					clientBitmapGraphics.FillRectangle(Brushes.DarkRed, clientRectangle);
					clientBitmapGraphics.FillPie(Brushes.Lime, (float)((double)clientRectangle.Width * greenRatio * -1.0), (float)((double)clientRectangle.Height * greenRatio * -1.0), (float)((double)clientRectangle.Width * greenRatio * 2.0), (float)((double)clientRectangle.Height * greenRatio * 2.0), (float)(90.0 - maxAngle * 180.0 / 3.141592653589793), (float)((maxAngle - minAngle) * 180.0 / 3.141592653589793));
					clientBitmapGraphics.FillPie(Brushes.White, (float)((double)clientRectangle.Width * whiteRatio * -1.0), (float)((double)clientRectangle.Height * whiteRatio * -1.0), (float)((double)clientRectangle.Width * whiteRatio * 2.0), (float)((double)clientRectangle.Height * whiteRatio * 2.0), 0f, 90f);
					clientBitmapGraphics.Dispose();
					graphics.DrawImage(clientBitmap, clientRectangle);
				}
			}
		}

		// Token: 0x0600015E RID: 350 RVA: 0x00009D0C File Offset: 0x00008D0C
		private static void DrawStrengthSmooth(double strength, double smooth, Control control, Graphics graphics)
		{
			if (control != null && graphics != null)
			{
				Rectangle clientRectangle = control.ClientRectangle;
				if (clientRectangle.Width > 0 && clientRectangle.Height > 0)
				{
					Rectangle strengthSmoothRectangle = new Rectangle(0, 0, 32, 32);
					Bitmap strengthSmoothBmp = new Bitmap(strengthSmoothRectangle.Width, strengthSmoothRectangle.Height);
					Graphics strengthSmoothGraphics = Graphics.FromImage(strengthSmoothBmp);
					double pow = 0.0;
					double _pow = 0.0;
					if (smooth > (double)MathConsts.FLOAT_EPSILON && smooth + (double)MathConsts.FLOAT_EPSILON < 1.0)
					{
						pow = 1.0 / (1.0 - smooth);
						_pow = 1.0 - smooth;
					}
					for (int x = 0; x < strengthSmoothRectangle.Width; x++)
					{
						for (int y = 0; y < strengthSmoothRectangle.Height; y++)
						{
							double _x = (double)x * 1.0 / (double)strengthSmoothRectangle.Width;
							double _y = (double)y * 1.0 / (double)strengthSmoothRectangle.Height;
							double distance = 1.0 - Math.Sqrt(_x * _x + _y * _y);
							if (distance < 0.0)
							{
								distance = 0.0;
							}
							else if (smooth + (double)MathConsts.FLOAT_EPSILON > 1.0)
							{
								distance = 1.0;
							}
							else if (smooth > (double)MathConsts.FLOAT_EPSILON)
							{
								distance = Math.Pow(Math.Pow(1.0, pow) - Math.Pow(1.0 - distance, pow), _pow);
							}
							int color = 255 - (int)(255.0 * strength * distance);
							if (color < 0)
							{
								color = 0;
							}
							if (color > 255)
							{
								color = 255;
							}
							strengthSmoothGraphics.DrawRectangle(new Pen(Color.FromArgb(color, color, color)), x, y, 1, 1);
						}
					}
					strengthSmoothGraphics.Dispose();
					Bitmap clientBitmap = new Bitmap(clientRectangle.Width, clientRectangle.Height);
					Graphics clientBitmapGraphics = Graphics.FromImage(clientBitmap);
					clientBitmapGraphics.FillRectangle(Brushes.White, clientRectangle);
					clientBitmapGraphics.DrawImage(strengthSmoothBmp, clientRectangle);
					clientBitmapGraphics.Dispose();
					graphics.DrawImage(clientBitmap, clientRectangle);
				}
			}
		}

		// Token: 0x0600015F RID: 351 RVA: 0x00009F4D File Offset: 0x00008F4D
		private void OnStartLeftLandscapeFormTimer(SimpleTimer simpleTimer)
		{
			this.LeftLandscapeFormTimer.Start();
		}

		// Token: 0x06000160 RID: 352 RVA: 0x00009F5A File Offset: 0x00008F5A
		private void OnStopLeftLandscapeFormTimer(SimpleTimer simpleTimer)
		{
			this.LeftLandscapeFormTimer.Stop();
		}

		// Token: 0x06000161 RID: 353 RVA: 0x00009F67 File Offset: 0x00008F67
		private void OnStartRightLandscapeFormTimer(SimpleTimer simpleTimer)
		{
			this.RightLandscapeFormTimer.Start();
		}

		// Token: 0x06000162 RID: 354 RVA: 0x00009F74 File Offset: 0x00008F74
		private void OnStopRightLandscapeFormTimer(SimpleTimer simpleTimer)
		{
			this.RightLandscapeFormTimer.Stop();
		}

		// Token: 0x06000163 RID: 355 RVA: 0x00009F81 File Offset: 0x00008F81
		private void LeftLandscapeFormTimer_Tick(object sender, EventArgs e)
		{
			this.leftLandscapeSimpleTimer.Stop();
			this.leftLandscapeSimpleTimer.Invoke();
			this.leftLandscapeSimpleTimer.Start();
		}

		// Token: 0x06000164 RID: 356 RVA: 0x00009FA4 File Offset: 0x00008FA4
		private void RightLandscapeFormTimer_Tick(object sender, EventArgs e)
		{
			this.rightLandscapeSimpleTimer.Stop();
			this.rightLandscapeSimpleTimer.Invoke();
			this.rightLandscapeSimpleTimer.Start();
		}

		// Token: 0x06000165 RID: 357 RVA: 0x00009FC7 File Offset: 0x00008FC7
		private void EditBoxTimer_Tick(object sender, EventArgs e)
		{
			this.EditBoxTimer.Stop();
			this.UpdateEditBoxes();
		}

		// Token: 0x06000166 RID: 358 RVA: 0x00009FDC File Offset: 0x00008FDC
		private void UpdateEditBoxes()
		{
			foreach (KeyValuePair<string, int> keyValuePair in this.editBoxForUpdateNames)
			{
				if (keyValuePair.Key == this.LandscapeRegionSizeEditBox.Name)
				{
					this.UpdateLandscapeRegionParamsSize();
				}
				else if (keyValuePair.Key == this.LTPTileAngle00TextBox.Name)
				{
					this.UpdateTileToolAngleRestrictionsBeginAngle();
					this.UpdateTileToolAngleRestrictionsImage();
				}
				else if (keyValuePair.Key == this.LTPTileAngle01TextBox.Name)
				{
					this.UpdateTileToolAngleRestrictionsEndAngle();
					this.UpdateTileToolAngleRestrictionsImage();
				}
				else if (keyValuePair.Key == this.LTPTileStrengthSmoothTextBox.Name)
				{
					this.UpdateTileToolStrengthSmooth();
					this.UpdateTileToolStrengthSmoothImage();
				}
				else if (keyValuePair.Key == this.LTPTileReplaceToolTextBox.Name)
				{
					this.UpdateTileToolTileForReplace();
				}
				else if (keyValuePair.Key == this.LTPWaterHeightTextBox.Name)
				{
					this.UpdateWaterToolHeight();
				}
				else if (keyValuePair.Key == this.LTPHeightStrengthSmoothTextBox.Name)
				{
					this.UpdateHeightToolStrengthSmooth();
					this.UpdateHeightToolStrengthSmoothImage();
				}
				else if (keyValuePair.Key == this.LTPHeightUpdateObjectsTextBox.Name)
				{
					this.UpdateHeightToolUpdateObjectHeightsHeightRange();
				}
				else if (keyValuePair.Key == this.LTPHeightHeightTextBox.Name)
				{
					this.UpdateHeightToolHeight();
				}
				else if (keyValuePair.Key == this.LTPClipboardStrengthSmoothTextBox.Name)
				{
					this.UpdateClipboardToolStrengthSmooth();
					this.UpdateClipboardToolStrengthSmoothImage();
				}
				else if (keyValuePair.Key == this.LTPClipboardUpdateObjectsTextBox.Name)
				{
					this.UpdateClipboardToolUpdateObjectHeightsHeightRange();
				}
				else if (keyValuePair.Key == this.OverTerrainTextBox.Name)
				{
					this.UpdateOverTerrainAltitudeCache();
				}
				else if (keyValuePair.Key == this.UnderTerrainTextBox.Name)
				{
					this.UpdateUnderTerrainAltitudeCache();
				}
			}
			this.editBoxForUpdateNames.Clear();
		}

		// Token: 0x06000167 RID: 359 RVA: 0x0000A22C File Offset: 0x0000922C
		private void OnBindSoundItemList(MethodArgs methodArgs)
		{
			this.soundFolderItemFiltes.Bind(this.soundItemList.ItemFilters);
			this.soundItemList.Bind(this.SoundListView, this.SoundComboBox);
		}

		// Token: 0x06000168 RID: 360 RVA: 0x0000A25C File Offset: 0x0000925C
		private void OnUnbindSoundItemList(MethodArgs methodArgs)
		{
			this.soundItemList.Unbind();
			this.soundFolderItemFiltes.Unbind();
			this.soundItemList.ItemSources.Clear();
			this.soundItemList.ItemFilters.Clear();
			this.soundItemList.ItemDataMiners.Clear();
		}

		// Token: 0x06000169 RID: 361 RVA: 0x0000A2AF File Offset: 0x000092AF
		private void OnAddSoundItemListSource(MethodArgs methodArgs)
		{
			if (methodArgs.sender != null)
			{
				this.soundItemList.ItemSources.Add(methodArgs.sender as ItemList.IItemSource);
			}
		}

		// Token: 0x0600016A RID: 362 RVA: 0x0000A2D8 File Offset: 0x000092D8
		private void OnSoundTabSendData(MethodArgs methodArgs)
		{
			base.Context.StateContainer.Invoke("_sound_item_list_selected", new MethodArgs(this, this.soundItemList.SelectedItem, null));
			this.OnSoundTypeChanged();
			this.OnSoundBrushTypeChanged();
			if (this.SoundSize0Button.Checked)
			{
				this.OnSoundSizeChanged(this.zoneTSizes[0]);
				return;
			}
			if (this.SoundSize1Button.Checked)
			{
				this.OnSoundSizeChanged(this.zoneTSizes[1]);
				return;
			}
			this.OnSoundSizeChanged(this.zoneTSizes[2]);
		}

		// Token: 0x0600016B RID: 363 RVA: 0x0000A360 File Offset: 0x00009360
		private void OnSoundSizeChanged(int size)
		{
			if (this.created)
			{
				base.Context.StateContainer.Invoke("_sound_tab_size_changed", new MethodArgs(this, size, null));
				this.SoundBrushSizeTextBox.Text = size * Constants.MapZonePieceSize.X + " x " + size * Constants.MapZonePieceSize.Y;
			}
		}

		// Token: 0x0600016C RID: 364 RVA: 0x0000A3D4 File Offset: 0x000093D4
		private void OnSoundsTabIncreaseSize(MethodArgs methodArgs)
		{
			if (this.SoundSize0Button.Checked)
			{
				this.SoundSize0Button.Checked = false;
				this.SoundSize1Button.Checked = true;
				return;
			}
			if (this.SoundSize1Button.Checked)
			{
				this.SoundSize1Button.Checked = false;
				this.SoundSize2Button.Checked = true;
			}
		}

		// Token: 0x0600016D RID: 365 RVA: 0x0000A42C File Offset: 0x0000942C
		private void OnSoundsTabDecreaseSize(MethodArgs methodArgs)
		{
			if (this.SoundSize2Button.Checked)
			{
				this.SoundSize2Button.Checked = false;
				this.SoundSize1Button.Checked = true;
				return;
			}
			if (this.SoundSize1Button.Checked)
			{
				this.SoundSize1Button.Checked = false;
				this.SoundSize0Button.Checked = true;
			}
		}

		// Token: 0x0600016E RID: 366 RVA: 0x0000A484 File Offset: 0x00009484
		private void SoundSize0Button_CheckedChanged(object sender, EventArgs e)
		{
			if (this.SoundSize0Button.Checked)
			{
				this.OnSoundSizeChanged(this.zoneTSizes[0]);
			}
		}

		// Token: 0x0600016F RID: 367 RVA: 0x0000A4A1 File Offset: 0x000094A1
		private void SoundSize1Button_CheckedChanged(object sender, EventArgs e)
		{
			if (this.SoundSize1Button.Checked)
			{
				this.OnSoundSizeChanged(this.zoneTSizes[1]);
			}
		}

		// Token: 0x06000170 RID: 368 RVA: 0x0000A4BE File Offset: 0x000094BE
		private void SoundSize2Button_CheckedChanged(object sender, EventArgs e)
		{
			if (this.SoundSize2Button.Checked)
			{
				this.OnSoundSizeChanged(this.zoneTSizes[2]);
			}
		}

		// Token: 0x06000171 RID: 369 RVA: 0x0000A4DB File Offset: 0x000094DB
		private void MusicRadioButton_CheckedChanged(object sender, EventArgs e)
		{
			this.OnSoundTypeChanged();
		}

		// Token: 0x06000172 RID: 370 RVA: 0x0000A4E3 File Offset: 0x000094E3
		private void AmbienceRadioButton_CheckedChanged(object sender, EventArgs e)
		{
			this.OnSoundTypeChanged();
		}

		// Token: 0x06000173 RID: 371 RVA: 0x0000A4EC File Offset: 0x000094EC
		private void OnSoundTypeChanged()
		{
			if (this.MusicRadioButton.Checked)
			{
				base.Context.StateContainer.Invoke("_set_sound_music", new MethodArgs(this, null, null));
				return;
			}
			base.Context.StateContainer.Invoke("_set_sound_ambience", new MethodArgs(this, null, null));
		}

		// Token: 0x06000174 RID: 372 RVA: 0x0000A541 File Offset: 0x00009541
		private void OnLightmapStateChanged(EditorSceneParams _editorSceneParams)
		{
			if (_editorSceneParams.ActiveLightmapState == EditorSceneParams.LightmapState.Ambience)
			{
				this.AmbienceRadioButton.Checked = true;
				return;
			}
			this.MusicRadioButton.Checked = true;
		}

		// Token: 0x06000175 RID: 373 RVA: 0x0000A565 File Offset: 0x00009565
		private void SetSoundRadioButton_CheckedChanged(object sender, EventArgs e)
		{
			this.OnSoundBrushTypeChanged();
		}

		// Token: 0x06000176 RID: 374 RVA: 0x0000A56D File Offset: 0x0000956D
		private void ClearSoundRadioButton_CheckedChanged(object sender, EventArgs e)
		{
			this.OnSoundBrushTypeChanged();
		}

		// Token: 0x06000177 RID: 375 RVA: 0x0000A578 File Offset: 0x00009578
		private void OnSoundBrushTypeChanged()
		{
			if (this.SetSoundRadioButton.Checked)
			{
				base.Context.StateContainer.Invoke("_set_sound_choosed", new MethodArgs(this, null, null));
				return;
			}
			base.Context.StateContainer.Invoke("_clear_sound_choosed", new MethodArgs(this, null, null));
		}

		// Token: 0x06000178 RID: 376 RVA: 0x0000A5CD File Offset: 0x000095CD
		private void OnSoundItemSelected(ItemList sender, string item)
		{
			base.Context.StateContainer.Invoke("_sound_item_list_selected", new MethodArgs(this, item, null));
		}

		// Token: 0x06000179 RID: 377 RVA: 0x0000A5EC File Offset: 0x000095EC
		private void OnSoundItemListCleared(ItemList sender, string item)
		{
			base.Context.StateContainer.Invoke("_sound_item_list_cleared", new MethodArgs(this, item, null));
		}

		// Token: 0x0600017A RID: 378 RVA: 0x0000A60B File Offset: 0x0000960B
		private void OnSoundItemListRefreshing(ItemList sender, string item)
		{
			base.Context.StateContainer.Invoke("_sound_item_list_refreshing", new MethodArgs(this, item, null));
		}

		// Token: 0x0600017B RID: 379 RVA: 0x0000A62A File Offset: 0x0000962A
		private void SoundFilterEditorButton_Click(object sender, EventArgs e)
		{
			this.soundFolderItemFiltes.ShowDialog(this.soundItemList, base.Context.MainForm);
		}

		// Token: 0x0600017C RID: 380 RVA: 0x0000A649 File Offset: 0x00009649
		private void OnSoundItemDoubleClicked(ItemList sender, string item)
		{
			base.Context.StateContainer.Invoke("_sound_item_edit", new MethodArgs(this, item, null));
		}

		// Token: 0x0600017D RID: 381 RVA: 0x0000A668 File Offset: 0x00009668
		private void OnSoundListKeyDown(ItemList sender, EventArgs e, string item)
		{
			if (!string.IsNullOrEmpty(item))
			{
				KeyEventArgs ke = e as KeyEventArgs;
				if (ke != null && ke.KeyCode == Keys.Return)
				{
					base.Context.StateContainer.Invoke("_sound_item_edit", new MethodArgs(this, item, null));
				}
			}
		}

		// Token: 0x0600017E RID: 382 RVA: 0x0000A6B0 File Offset: 0x000096B0
		private void OnSoundListSelectItem(MethodArgs methodArgs)
		{
			string item = methodArgs.sender as string;
			if (!string.IsNullOrEmpty(item))
			{
				this.soundItemList.SelectItem(item);
			}
		}

		// Token: 0x0600017F RID: 383 RVA: 0x0000A6E0 File Offset: 0x000096E0
		private void OnSoundItemChanged(MethodArgs methodArgs)
		{
			string item = methodArgs.sender as string;
			if (!string.IsNullOrEmpty(item))
			{
				this.soundItemList.UpdateItem(item);
			}
		}

		// Token: 0x06000180 RID: 384 RVA: 0x0000A710 File Offset: 0x00009710
		private void OnSoundMenuClick(object sender, ToolStripItemClickedEventArgs e)
		{
			if (e.ClickedItem == this.AddSoundToolStripMenuItem)
			{
				base.Context.StateContainer.Invoke("_sound_item_add", new MethodArgs(this, null, null));
				return;
			}
			if (e.ClickedItem == this.RemoveSoundToolStripMenuItem && !string.IsNullOrEmpty(this.soundItemList.SelectedItem))
			{
				base.Context.StateContainer.Invoke("_sound_item_remove", new MethodArgs(this, this.soundItemList.SelectedItem, null));
			}
		}

		// Token: 0x06000183 RID: 387 RVA: 0x000133DD File Offset: 0x000123DD
		private void OnPostLoadParams(FormParams formParams)
		{
			this.UpdateOverTerrainAltitudeCache();
			this.UpdateUnderTerrainAltitudeCache();
		}

		// Token: 0x06000184 RID: 388 RVA: 0x000133EC File Offset: 0x000123EC
		private void BindObjectItemList()
		{
			this.objectItemList.ItemSources.Add(this.staticObjectItemListSource);
			this.objectItemList.ItemSources.Add(this.startPointItemListSource);
			this.objectItemList.ItemSources.Add(this.multiObjectItemListSource);
			this.objectItemList.ItemSources.Add(this.objectSetItemListSource);
			this.objectFolderItemFiltes.Bind(this.objectItemList.ItemFilters);
			this.objectItemList.Bind(this.ObjectListView, this.ObjectComboBox);
		}

		// Token: 0x06000185 RID: 389 RVA: 0x00013480 File Offset: 0x00012480
		private void UnindObjectItemList()
		{
			this.objectItemList.Unbind();
			this.objectFolderItemFiltes.Unbind();
			this.objectItemList.ItemSources.Clear();
			this.objectItemList.ItemFilters.Clear();
			this.objectItemList.ItemDataMiners.Clear();
		}

		// Token: 0x14000009 RID: 9
		// (add) Token: 0x06000186 RID: 390 RVA: 0x000134D3 File Offset: 0x000124D3
		// (remove) Token: 0x06000187 RID: 391 RVA: 0x000134EC File Offset: 0x000124EC
		public event ObjectsStateParamsFieldChangedEvent AddRowChanged;

		// Token: 0x1400000A RID: 10
		// (add) Token: 0x06000188 RID: 392 RVA: 0x00013505 File Offset: 0x00012505
		// (remove) Token: 0x06000189 RID: 393 RVA: 0x0001351E File Offset: 0x0001251E
		public event ObjectsStateParamsFieldChangedEvent OneSidedRowChanged;

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x0600018A RID: 394 RVA: 0x00013537 File Offset: 0x00012537
		// (set) Token: 0x0600018B RID: 395 RVA: 0x0001354F File Offset: 0x0001254F
		public bool EnableDoubleClickProperties
		{
			get
			{
				return FormParamsSaver.IntToBool(base.ParamsSaver.FormParams.GetInt(0));
			}
			set
			{
				base.ParamsSaver.FormParams.SetInt(0, FormParamsSaver.BoolToInt(value));
			}
		}

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x0600018C RID: 396 RVA: 0x00013568 File Offset: 0x00012568
		public bool RandomizeName
		{
			get
			{
				return this.RandomObjectCheckBox.Checked;
			}
		}

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x0600018D RID: 397 RVA: 0x00013575 File Offset: 0x00012575
		public bool AddRow
		{
			get
			{
				return this.ObjectRowCheckBox.Checked;
			}
		}

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x0600018E RID: 398 RVA: 0x00013582 File Offset: 0x00012582
		public bool OneSidedRow
		{
			get
			{
				return !this.FlatRowCheckBox.Checked;
			}
		}

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x0600018F RID: 399 RVA: 0x00013592 File Offset: 0x00012592
		public string Group
		{
			get
			{
				if (this.GroupCheckBox.Checked)
				{
					return this.GroupComboBox.Text;
				}
				return string.Empty;
			}
		}

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x06000190 RID: 400 RVA: 0x000135B2 File Offset: 0x000125B2
		public ItemList.IItemSource NameSource
		{
			get
			{
				return this.staticObjectItemListSource;
			}
		}

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x06000191 RID: 401 RVA: 0x000135BA File Offset: 0x000125BA
		public ItemList ItemList
		{
			get
			{
				return this.objectItemList;
			}
		}

		// Token: 0x17000018 RID: 24
		// (get) Token: 0x06000192 RID: 402 RVA: 0x000135C2 File Offset: 0x000125C2
		public bool MapObjectRandomizerAvailable
		{
			get
			{
				return this.objectCreationRandomParams.RandomRotation || this.objectCreationRandomParams.RandomPitch || this.objectCreationRandomParams.RandomScale;
			}
		}

		// Token: 0x06000193 RID: 403 RVA: 0x000135EB File Offset: 0x000125EB
		public Position RandomizePosition(Position position)
		{
			return position;
		}

		// Token: 0x06000194 RID: 404 RVA: 0x000135F0 File Offset: 0x000125F0
		public Rotation RandomizeRotation(Rotation rotation)
		{
			if (this.objectCreationRandomParams.RandomRotation)
			{
				rotation.Yaw = ((float)this.random.NextDouble() * (this.objectCreationRandomParams.RotationTo - this.objectCreationRandomParams.RotationFrom) + this.objectCreationRandomParams.RotationFrom) * 3.1415927f / 180f;
			}
			if (this.objectCreationRandomParams.RandomPitch)
			{
				rotation.Pitch = ((float)this.random.NextDouble() * (this.objectCreationRandomParams.PitchTo - this.objectCreationRandomParams.PitchFrom) + this.objectCreationRandomParams.PitchFrom) * 3.1415927f / 180f;
				rotation.Roll = ((float)this.random.NextDouble() * (this.objectCreationRandomParams.PitchTo - this.objectCreationRandomParams.PitchFrom) + this.objectCreationRandomParams.PitchFrom) * 3.1415927f / 180f;
			}
			return rotation;
		}

		// Token: 0x06000195 RID: 405 RVA: 0x000136E4 File Offset: 0x000126E4
		public Scale RandomizeScale(Scale scale)
		{
			if (this.objectCreationRandomParams.RandomScale)
			{
				scale.Ratio = (float)this.random.NextDouble() * (this.objectCreationRandomParams.ScaleTo - this.objectCreationRandomParams.ScaleFrom) + this.objectCreationRandomParams.ScaleFrom;
			}
			return scale;
		}

		// Token: 0x06000196 RID: 406 RVA: 0x00013736 File Offset: 0x00012736
		private void ObjectRowCheckBox_CheckedChanged(object sender, EventArgs e)
		{
			if (this.created && this.AddRowChanged != null)
			{
				this.AddRowChanged();
			}
		}

		// Token: 0x06000197 RID: 407 RVA: 0x00013753 File Offset: 0x00012753
		private void FlatRowCheckBox_CheckedChanged(object sender, EventArgs e)
		{
			if (this.created && this.OneSidedRowChanged != null)
			{
				this.OneSidedRowChanged();
			}
		}

		// Token: 0x06000198 RID: 408 RVA: 0x00013770 File Offset: 0x00012770
		public bool FilterObject(IMapObject mapObject)
		{
			if (mapObject != null)
			{
				if (mapObject.Temporary)
				{
					return false;
				}
				if (this.OverTerrainCheckBox.Checked && mapObject.Altitude < this.overAltitudeCache - MathConsts.DOUBLE_EPSILON)
				{
					return false;
				}
				if (this.UnderTerrainCheckBox.Checked && mapObject.Altitude > this.underAltitudeCache + MathConsts.DOUBLE_EPSILON)
				{
					return false;
				}
				if (base.Context != null && base.Context.UnselectableObjectsForm != null && base.Context.UnselectableObjectsForm.ObjectIsUnselectable(mapObject))
				{
					return false;
				}
				if (base.Context != null && base.Context.HiddenObjectsForm != null && base.Context.HiddenObjectsForm.ObjectIsHidden(mapObject))
				{
					return false;
				}
				if (this.FilterObjectTypeCheckBox.Checked)
				{
					string selectedTypeString = this.ObjectTypeComboBox.SelectedItem as string;
					if (!string.IsNullOrEmpty(selectedTypeString))
					{
						int selectedType = MapObjectFactory.Type.SeveralObjectsTypeNameToType(selectedTypeString);
						return mapObject.Type.Type == selectedType;
					}
				}
				if (this.FilterSelectionCheckBox.Checked)
				{
					return MapObjectInterface.ItemListContainsMapObjectTemplate(this.objectItemList, mapObject);
				}
			}
			return true;
		}

		// Token: 0x06000199 RID: 409 RVA: 0x00013884 File Offset: 0x00012884
		private void CreateObjectTypeList()
		{
			this.DestroyObjectTypeList();
			List<string> mapObjectTypeList = MapObjectInterface.GetMapObjectTypeList(false);
			foreach (string mapObjectType in mapObjectTypeList)
			{
				this.ObjectTypeComboBox.Items.Add(mapObjectType);
			}
		}

		// Token: 0x0600019A RID: 410 RVA: 0x000138EC File Offset: 0x000128EC
		private void DestroyObjectTypeList()
		{
			this.ObjectTypeComboBox.Items.Clear();
		}

		// Token: 0x0600019B RID: 411 RVA: 0x000138FE File Offset: 0x000128FE
		private void FilterObjectTypeCheckBox_CheckedChanged(object sender, EventArgs e)
		{
			if (this.created)
			{
				this.created = false;
				if (this.FilterObjectTypeCheckBox.Checked)
				{
					this.FilterSelectionCheckBox.Checked = false;
				}
				this.created = true;
			}
		}

		// Token: 0x0600019C RID: 412 RVA: 0x0001392F File Offset: 0x0001292F
		private void FilterSelectionCheckBox_CheckedChanged(object sender, EventArgs e)
		{
			if (this.created)
			{
				this.created = false;
				if (this.FilterSelectionCheckBox.Checked)
				{
					this.FilterObjectTypeCheckBox.Checked = false;
				}
				this.created = true;
			}
		}

		// Token: 0x0600019D RID: 413 RVA: 0x00013960 File Offset: 0x00012960
		private void OverTerrainCheckBox_CheckedChanged(object sender, EventArgs e)
		{
			if (this.created)
			{
				this.created = false;
				this.created = true;
			}
		}

		// Token: 0x0600019E RID: 414 RVA: 0x00013978 File Offset: 0x00012978
		private void UnderTerrainCheckBox_CheckedChanged(object sender, EventArgs e)
		{
			if (this.created)
			{
				this.created = false;
				this.created = true;
			}
		}

		// Token: 0x0600019F RID: 415 RVA: 0x00013990 File Offset: 0x00012990
		private void OverTerrainTextBox_TextChanged(object sender, EventArgs e)
		{
			if (this.created)
			{
				this.editBoxForUpdateNames[this.OverTerrainTextBox.Name] = 0;
				this.EditBoxTimer.Start();
			}
		}

		// Token: 0x060001A0 RID: 416 RVA: 0x000139BC File Offset: 0x000129BC
		private void UnderTerrainTextBox_TextChanged(object sender, EventArgs e)
		{
			if (this.created)
			{
				this.editBoxForUpdateNames[this.UnderTerrainTextBox.Name] = 0;
				this.EditBoxTimer.Start();
			}
		}

		// Token: 0x060001A1 RID: 417 RVA: 0x000139E8 File Offset: 0x000129E8
		private void UpdateOverTerrainAltitudeCache()
		{
			if (!double.TryParse(this.OverTerrainTextBox.Text, out this.overAltitudeCache))
			{
				this.created = false;
				this.overAltitudeCache = ToolForm.defaultOverAltitudeCache;
				this.OverTerrainTextBox.Text = this.overAltitudeCache.ToString();
				this.created = true;
			}
		}

		// Token: 0x060001A2 RID: 418 RVA: 0x00013A3C File Offset: 0x00012A3C
		private void UpdateUnderTerrainAltitudeCache()
		{
			if (!double.TryParse(this.UnderTerrainTextBox.Text, out this.underAltitudeCache))
			{
				this.created = false;
				this.underAltitudeCache = ToolForm.defaultUnderAltitudeCache;
				this.UnderTerrainTextBox.Text = this.underAltitudeCache.ToString();
				this.created = true;
			}
		}

		// Token: 0x060001A3 RID: 419 RVA: 0x00013A90 File Offset: 0x00012A90
		private static void SetControlsEnabled(Control.ControlCollection controls, bool enabled)
		{
			foreach (object obj in controls)
			{
				Control control = (Control)obj;
				control.Enabled = enabled;
			}
		}

		// Token: 0x060001A4 RID: 420 RVA: 0x00013AE4 File Offset: 0x00012AE4
		private void FillTabs(ContinentType type)
		{
			if (type == ContinentType.AstralHub)
			{
				if (this.MainTab.TabPages.Count > 1)
				{
					this.MainTab.TabPages.Remove(this.LandscapeTab);
					this.MainTab.TabPages.Remove(this.ZonesTab);
					this.MainTab.TabPages.Remove(this.LightsTab);
					this.MainTab.TabPages.Remove(this.SoundTab);
				}
				ToolForm.SetControlsEnabled(this.ObjectsTab.Controls, true);
				ToolForm.SetControlsEnabled(this.LandscapeTab.Controls, false);
				ToolForm.SetControlsEnabled(this.ZonesTab.Controls, false);
				ToolForm.SetControlsEnabled(this.LightsTab.Controls, false);
				ToolForm.SetControlsEnabled(this.SoundTab.Controls, false);
				return;
			}
			if (type == ContinentType.Continent)
			{
				if (this.MainTab.TabPages.Count == 1)
				{
					this.MainTab.TabPages.Add(this.LandscapeTab);
					this.MainTab.TabPages.Add(this.ZonesTab);
					this.MainTab.TabPages.Add(this.LightsTab);
					this.MainTab.TabPages.Add(this.SoundTab);
				}
				ToolForm.SetControlsEnabled(this.ObjectsTab.Controls, true);
				ToolForm.SetControlsEnabled(this.LandscapeTab.Controls, true);
				ToolForm.SetControlsEnabled(this.ZonesTab.Controls, true);
				ToolForm.SetControlsEnabled(this.LightsTab.Controls, true);
				ToolForm.SetControlsEnabled(this.SoundTab.Controls, true);
				return;
			}
			if (this.MainTab.TabPages.Count == 1)
			{
				this.MainTab.TabPages.Add(this.LandscapeTab);
				this.MainTab.TabPages.Add(this.ZonesTab);
				this.MainTab.TabPages.Add(this.LightsTab);
				this.MainTab.TabPages.Add(this.SoundTab);
			}
			ToolForm.SetControlsEnabled(this.ObjectsTab.Controls, true);
			ToolForm.SetControlsEnabled(this.LandscapeTab.Controls, true);
			ToolForm.SetControlsEnabled(this.ZonesTab.Controls, false);
			ToolForm.SetControlsEnabled(this.LightsTab.Controls, false);
			ToolForm.SetControlsEnabled(this.SoundTab.Controls, false);
		}

		// Token: 0x060001A5 RID: 421 RVA: 0x00013D58 File Offset: 0x00012D58
		public ToolForm(MainForm.Context _context) : base(EditorEnvironment.EditorFormsFolder + "ToolForm/ToolForm.xml", _context)
		{
			this.InitializeComponent();
			base.ParamsSaver.PostLoadParams += this.OnPostLoadParams;
			base.ParamsSaver.RegisterControl(this.ObjectTypeComboBox, true);
			base.ParamsSaver.RegisterControl(this.FilterObjectTypeCheckBox);
			base.ParamsSaver.RegisterControl(this.FilterSelectionCheckBox);
			base.ParamsSaver.RegisterControl(this.OverTerrainCheckBox);
			base.ParamsSaver.RegisterControl(this.UnderTerrainCheckBox);
			base.ParamsSaver.RegisterControl(this.OverTerrainTextBox);
			base.ParamsSaver.RegisterControl(this.UnderTerrainTextBox);
			base.ParamsSaver.RegisterControl(this.GroupCheckBox);
			base.ParamsSaver.RegisterControl(this.RandomObjectCheckBox);
			base.ParamsSaver.RegisterControl(this.ObjectRowCheckBox);
			base.ParamsSaver.RegisterControl(this.FlatRowCheckBox);
			base.ParamsSaver.RegisterControl(this.GroupComboBox, false);
			base.ParamsSaver.RegisterControl(this.LandscapeUseSeparateRegionsCheckBox);
			this.zoneItemList = new ItemList(EditorEnvironment.EditorFormsFolder + "ToolForm/MapZonesItemList.xml", base.Context.ItemDataContainer);
			this.objectItemList = new ItemList(EditorEnvironment.EditorFormsFolder + "ToolForm/ObjectItemList.xml", base.Context.ItemDataContainer);
			this.lightItemList = new ItemList(EditorEnvironment.EditorFormsFolder + "ToolForm/LightItemList.xml", base.Context.ItemDataContainer);
			this.soundItemList = new ItemList(EditorEnvironment.EditorFormsFolder + "ToolForm/SoundItemList.xml", base.Context.ItemDataContainer);
			this.CreateStates();
			this.FillStates(ContinentType.Unknown);
			this.CreateLandscapeTabControls();
			this.FillTabs(ContinentType.Unknown);
			this.toolFormState.AddMethod("switch_terrain", new Method(this.OnSwitchTerrain));
			this.toolFormState.AddMethod("switch_bottom", new Method(this.OnSwitchBottom));
			this.toolFormState.AddMethod("_water_layer_list_changed", new Method(this.OnNewWaterLayerAdded));
			this.toolFormState.AddMethod("_clear_selection_in_object_item_list", new Method(this.OnClearSelectionInObjectItemList));
			this.toolFormState.AddMethod("_bind_map_zones_item_list", new Method(this.OnBindZonesItemList));
			this.toolFormState.AddMethod("_unbind_map_zones_item_list", new Method(this.OnUnbindZonesItemList));
			this.toolFormState.AddMethod("_add_map_zones_item_list_source", new Method(this.OnAddZonesItemListSource));
			this.toolFormState.AddMethod("_map_zones_tab_send_data", new Method(this.OnZonesTabSendData));
			this.toolFormState.AddMethod("_map_zone_list_select_item", new Method(this.OnZoneListSelectItem));
			this.toolFormState.AddMethod("_map_zone_list_item_changed", new Method(this.OnZoneItemChanged));
			this.toolFormState.AddMethod("_map_zones_increase_size", new Method(this.OnZonesTabIncreaseSize));
			this.toolFormState.AddMethod("_map_zones_decrease_size", new Method(this.OnZonesTabDecreaseSize));
			this.toolFormState.AddMethod("_bind_light_item_list", new Method(this.OnBindLightItemList));
			this.toolFormState.AddMethod("_unbind_light_item_list", new Method(this.OnUnbindLightItemList));
			this.toolFormState.AddMethod("_add_light_item_list_source", new Method(this.OnAddLightItemListSource));
			this.toolFormState.AddMethod("_lights_tab_send_data", new Method(this.OnLightsTabSendData));
			this.toolFormState.AddMethod("_light_list_select_item", new Method(this.OnLightListSelectItem));
			this.toolFormState.AddMethod("_light_list_item_changed", new Method(this.OnLightItemChanged));
			this.toolFormState.AddMethod("_lights_increase_size", new Method(this.OnLightsTabIncreaseSize));
			this.toolFormState.AddMethod("_lights_decrease_size", new Method(this.OnLightsTabDecreaseSize));
			this.toolFormState.AddMethod("_bind_sound_item_list", new Method(this.OnBindSoundItemList));
			this.toolFormState.AddMethod("_unbind_sound_item_list", new Method(this.OnUnbindSoundItemList));
			this.toolFormState.AddMethod("_add_sound_item_list_source", new Method(this.OnAddSoundItemListSource));
			this.toolFormState.AddMethod("_sound_tab_send_data", new Method(this.OnSoundTabSendData));
			this.toolFormState.AddMethod("_sound_list_select_item", new Method(this.OnSoundListSelectItem));
			this.toolFormState.AddMethod("_sound_list_item_changed", new Method(this.OnSoundItemChanged));
			this.toolFormState.AddMethod("_sounds_increase_size", new Method(this.OnSoundsTabIncreaseSize));
			this.toolFormState.AddMethod("_sounds_decrease_size", new Method(this.OnSoundsTabDecreaseSize));
			string configFileName = EditorEnvironment.EditorFormsFolder + "ObjectCreationRandomParams.xml";
			this.objectCreationRandomParams = Serializer.Load<ObjectCreationRandomParams>(configFileName);
			if (this.objectCreationRandomParams == null)
			{
				this.objectCreationRandomParams = new ObjectCreationRandomParams();
			}
			ItemList itemList = this.objectItemList;
			itemList.ItemSelected = (ItemList.ItemEvent)Delegate.Combine(itemList.ItemSelected, new ItemList.ItemEvent(this.OnObjectItemSelected));
			ItemList itemList2 = this.objectItemList;
			itemList2.ItemUnselected = (ItemList.ItemEvent)Delegate.Combine(itemList2.ItemUnselected, new ItemList.ItemEvent(this.OnObjectItemUnselected));
			ItemList itemList3 = this.objectItemList;
			itemList3.ListCleared = (ItemList.ItemEvent)Delegate.Combine(itemList3.ListCleared, new ItemList.ItemEvent(this.OnObjectItemListCleared));
			ItemList itemList4 = this.objectItemList;
			itemList4.ItemDoubleClicked = (ItemList.ItemEvent)Delegate.Combine(itemList4.ItemDoubleClicked, new ItemList.ItemEvent(this.OnObjectItemDoubleClicked));
			ItemList itemList5 = this.objectItemList;
			itemList5.ListRefreshing = (ItemList.ItemEvent)Delegate.Combine(itemList5.ListRefreshing, new ItemList.ItemEvent(this.OnObjectItemListRefreshing));
			ItemList itemList6 = this.zoneItemList;
			itemList6.ItemSelected = (ItemList.ItemEvent)Delegate.Combine(itemList6.ItemSelected, new ItemList.ItemEvent(this.OnZonesItemSelected));
			ItemList itemList7 = this.zoneItemList;
			itemList7.ItemUnselected = (ItemList.ItemEvent)Delegate.Combine(itemList7.ItemUnselected, new ItemList.ItemEvent(this.OnZonesItemUnselected));
			ItemList itemList8 = this.zoneItemList;
			itemList8.ListCleared = (ItemList.ItemEvent)Delegate.Combine(itemList8.ListCleared, new ItemList.ItemEvent(this.OnZonesItemListCleared));
			ItemList itemList9 = this.zoneItemList;
			itemList9.ListRefreshing = (ItemList.ItemEvent)Delegate.Combine(itemList9.ListRefreshing, new ItemList.ItemEvent(this.OnZonesItemListRefreshing));
			ItemList itemList10 = this.zoneItemList;
			itemList10.ItemDoubleClicked = (ItemList.ItemEvent)Delegate.Combine(itemList10.ItemDoubleClicked, new ItemList.ItemEvent(this.OnZonesItemDoubleClicked));
			ItemList itemList11 = this.zoneItemList;
			itemList11.ListKeyDown = (ItemList.ListEvent)Delegate.Combine(itemList11.ListKeyDown, new ItemList.ListEvent(this.OnZonesListKeyDown));
			this.ZonesContextMenuStrip.ItemClicked += this.OnZonesMenuClick;
			ItemList itemList12 = this.lightItemList;
			itemList12.ItemSelected = (ItemList.ItemEvent)Delegate.Combine(itemList12.ItemSelected, new ItemList.ItemEvent(this.OnLightItemSelected));
			ItemList itemList13 = this.lightItemList;
			itemList13.ListCleared = (ItemList.ItemEvent)Delegate.Combine(itemList13.ListCleared, new ItemList.ItemEvent(this.OnLightItemListCleared));
			ItemList itemList14 = this.lightItemList;
			itemList14.ListRefreshing = (ItemList.ItemEvent)Delegate.Combine(itemList14.ListRefreshing, new ItemList.ItemEvent(this.OnLightItemListRefreshing));
			ItemList itemList15 = this.lightItemList;
			itemList15.ItemDoubleClicked = (ItemList.ItemEvent)Delegate.Combine(itemList15.ItemDoubleClicked, new ItemList.ItemEvent(this.OnLightItemDoubleClicked));
			ItemList itemList16 = this.lightItemList;
			itemList16.ListKeyDown = (ItemList.ListEvent)Delegate.Combine(itemList16.ListKeyDown, new ItemList.ListEvent(this.OnLightListKeyDown));
			this.LightContextMenuStrip.ItemClicked += this.OnLightMenuClick;
			EditorSceneParams editorSceneParams = base.Context.EditorSceneParams;
			editorSceneParams.LightmapStateChanged = (EditorSceneParams.ParamsEvent)Delegate.Combine(editorSceneParams.LightmapStateChanged, new EditorSceneParams.ParamsEvent(this.OnLightmapStateChanged));
			ItemList itemList17 = this.soundItemList;
			itemList17.ItemSelected = (ItemList.ItemEvent)Delegate.Combine(itemList17.ItemSelected, new ItemList.ItemEvent(this.OnSoundItemSelected));
			ItemList itemList18 = this.soundItemList;
			itemList18.ListCleared = (ItemList.ItemEvent)Delegate.Combine(itemList18.ListCleared, new ItemList.ItemEvent(this.OnSoundItemListCleared));
			ItemList itemList19 = this.soundItemList;
			itemList19.ListRefreshing = (ItemList.ItemEvent)Delegate.Combine(itemList19.ListRefreshing, new ItemList.ItemEvent(this.OnSoundItemListRefreshing));
			ItemList itemList20 = this.soundItemList;
			itemList20.ItemDoubleClicked = (ItemList.ItemEvent)Delegate.Combine(itemList20.ItemDoubleClicked, new ItemList.ItemEvent(this.OnSoundItemDoubleClicked));
			ItemList itemList21 = this.soundItemList;
			itemList21.ListKeyDown = (ItemList.ListEvent)Delegate.Combine(itemList21.ListKeyDown, new ItemList.ListEvent(this.OnSoundListKeyDown));
			this.SoundContextMenuStrip.ItemClicked += this.OnSoundMenuClick;
			base.Context.StateContainer.BindState(this.toolFormState);
			this.BindObjectItemList();
			this.created = true;
			this.UpdateMainState();
			this.UpdateLandscapeTabControls();
			this.UpdateRandomizerParamsControls();
			this.CreateObjectTypeList();
			this.FilterObjectTypeCheckBox.Checked = false;
			this.FilterSelectionCheckBox.Checked = false;
		}

		// Token: 0x060001A6 RID: 422 RVA: 0x000147EC File Offset: 0x000137EC
		private void ToolForm_FormClosing(object sender, FormClosingEventArgs e)
		{
			if (base.AllowClose && this.created)
			{
				this.DestroyStates();
				this.DestroyLandscapeTabControls();
				this.UnindObjectItemList();
				if (this.zoneItemList.Binded)
				{
					this.OnUnbindZonesItemList(default(MethodArgs));
				}
				if (this.lightItemList.Binded)
				{
					this.OnUnbindLightItemList(default(MethodArgs));
				}
				this.created = false;
				base.Context.StateContainer.UnbindState(this.toolFormState);
				string configFileName = EditorEnvironment.EditorFormsFolder + "ObjectCreationRandomParams.xml";
				Serializer.Save(configFileName, this.objectCreationRandomParams, false);
				this.created = false;
			}
		}

		// Token: 0x060001A7 RID: 423 RVA: 0x0001489C File Offset: 0x0001389C
		public void Bind(MapEditorMap _map)
		{
			this.Unbind();
			if (this.created)
			{
				this.continentName = _map.Data.ContinentName;
				if (!string.IsNullOrEmpty(this.continentName))
				{
					this.created = false;
					this.landscapeTileItemSource = new LandscapeTileItemSource(this.continentName);
					this.landscapeTileItemList.ItemSources.Add(this.landscapeTileItemSource);
					this.landscapeTileItemList.Refresh();
					this.landscapeWaterItemList.ItemSources.Add(new LandscapeWaterItemSource(this.continentName));
					this.landscapeWaterItemList.Refresh();
					this.created = true;
					this.UpdateTileToolTileForPaint();
					this.UpdateTileToolTileForReplace();
					this.UpdateTilePickerTiles(true);
					this.UpdateWaterToolForPaint();
					this.FillTabs(_map.Data.ContinentType);
					this.FillStates(_map.Data.ContinentType);
				}
			}
		}

		// Token: 0x060001A8 RID: 424 RVA: 0x0001497C File Offset: 0x0001397C
		public void Unbind()
		{
			if (this.created)
			{
				if (!string.IsNullOrEmpty(this.continentName))
				{
					this.created = false;
					this.landscapeTileItemSource = null;
					this.landscapeTileItemList.ItemSources.Clear();
					this.landscapeTileItemList.Refresh();
					this.landscapeWaterItemList.ItemSources.Clear();
					this.landscapeWaterItemList.Refresh();
					this.continentName = string.Empty;
					this.created = true;
				}
				this.FillTabs(ContinentType.Unknown);
				this.FillStates(ContinentType.Unknown);
			}
		}

		// Token: 0x17000019 RID: 25
		// (get) Token: 0x060001A9 RID: 425 RVA: 0x00014A02 File Offset: 0x00013A02
		public ItemList ObjectItemList
		{
			get
			{
				return this.objectItemList;
			}
		}

		// Token: 0x1700001A RID: 26
		// (get) Token: 0x060001AA RID: 426 RVA: 0x00014A0A File Offset: 0x00013A0A
		public ObjectCreationRandomParams ObjectCreationRandomParams
		{
			get
			{
				return this.objectCreationRandomParams;
			}
		}

		// Token: 0x060001AB RID: 427 RVA: 0x00014A14 File Offset: 0x00013A14
		private void UpdateRandomizerParamsControls()
		{
			if (this.created)
			{
				this.created = false;
				this.RandomRotationCheckBox.Checked = this.objectCreationRandomParams.RandomRotation;
				this.RandomPitchCheckBox.Checked = this.objectCreationRandomParams.RandomPitch;
				this.RandomScaleCheckBox.Checked = this.objectCreationRandomParams.RandomScale;
				this.RandomRotationFromEditBox.Text = this.objectCreationRandomParams.RotationFrom.ToString();
				this.RandomRotationToEditBox.Text = this.objectCreationRandomParams.RotationTo.ToString();
				this.RandomPitchFromEditBox.Text = this.objectCreationRandomParams.PitchFrom.ToString();
				this.RandomPitchToEditBox.Text = this.objectCreationRandomParams.PitchTo.ToString();
				this.RandomScaleFromEditBox.Text = this.objectCreationRandomParams.ScaleFrom.ToString();
				this.RandomScaleToEditBox.Text = this.objectCreationRandomParams.ScaleTo.ToString();
				this.created = true;
			}
		}

		// Token: 0x060001AC RID: 428 RVA: 0x00014B32 File Offset: 0x00013B32
		private void OnObjectItemListRefreshing(ItemList sender, string item)
		{
			this.staticObjectItemListSource.Outdated = true;
		}

		// Token: 0x060001AD RID: 429 RVA: 0x00014B40 File Offset: 0x00013B40
		private void OnClearSelectionInObjectItemList(MethodArgs methodArgs)
		{
			this.objectItemList.ClearSelection();
		}

		// Token: 0x060001AE RID: 430 RVA: 0x00014B4D File Offset: 0x00013B4D
		private void OnObjectItemSelected(ItemList sender, string item)
		{
			base.Context.StateContainer.Invoke("_object_item_list_selected", new MethodArgs(this, item, null));
		}

		// Token: 0x060001AF RID: 431 RVA: 0x00014B6C File Offset: 0x00013B6C
		private void OnObjectItemDoubleClicked(ItemList sender, string item)
		{
			base.Context.StateContainer.Invoke("_object_item_list_double_clicked", new MethodArgs(this, item, null));
		}

		// Token: 0x060001B0 RID: 432 RVA: 0x00014B8B File Offset: 0x00013B8B
		private void OnObjectItemUnselected(ItemList sender, string item)
		{
			base.Context.StateContainer.Invoke("_object_item_list_unselected", new MethodArgs(this, item, null));
		}

		// Token: 0x060001B1 RID: 433 RVA: 0x00014BAA File Offset: 0x00013BAA
		private void OnObjectItemListCleared(ItemList sender, string item)
		{
			base.Context.StateContainer.Invoke("_object_item_list_cleared", new MethodArgs(this, null, null));
		}

		// Token: 0x060001B2 RID: 434 RVA: 0x00014BC9 File Offset: 0x00013BC9
		private void objectsFilterEditorButton_Click(object sender, EventArgs e)
		{
			this.objectFolderItemFiltes.ShowDialog(this.objectItemList, base.Context.MainForm);
		}

		// Token: 0x060001B3 RID: 435 RVA: 0x00014BE8 File Offset: 0x00013BE8
		private void GroupComboBox_Leave(object sender, EventArgs e)
		{
			string newItem = this.GroupComboBox.Text;
			if (!this.GroupComboBox.Items.Contains(newItem))
			{
				this.GroupComboBox.Items.Insert(0, newItem);
				if (this.GroupComboBox.Items.Count > ToolForm.maxGroupComboBoxItemCount)
				{
					this.GroupComboBox.Items.RemoveAt(this.GroupComboBox.Items.Count - 1);
				}
			}
		}

		// Token: 0x060001B4 RID: 436 RVA: 0x00014C5F File Offset: 0x00013C5F
		private void RandomRotationCheckBox_CheckedChanged(object sender, EventArgs e)
		{
			if (this.created)
			{
				this.objectCreationRandomParams.RandomRotation = ((CheckBox)sender).Checked;
			}
		}

		// Token: 0x060001B5 RID: 437 RVA: 0x00014C7F File Offset: 0x00013C7F
		private void RandomPitchCheckBox_CheckedChanged(object sender, EventArgs e)
		{
			if (this.created)
			{
				this.objectCreationRandomParams.RandomPitch = ((CheckBox)sender).Checked;
			}
		}

		// Token: 0x060001B6 RID: 438 RVA: 0x00014C9F File Offset: 0x00013C9F
		private void RandomScaleCheckBox_CheckedChanged(object sender, EventArgs e)
		{
			if (this.created)
			{
				this.objectCreationRandomParams.RandomScale = ((CheckBox)sender).Checked;
			}
		}

		// Token: 0x060001B7 RID: 439 RVA: 0x00014CC0 File Offset: 0x00013CC0
		private void RandomRotationFromEditBox_TextChanged(object sender, EventArgs e)
		{
			if (this.created)
			{
				float angle;
				if (float.TryParse(((TextBox)sender).Text, out angle))
				{
					this.objectCreationRandomParams.RotationFrom = angle;
				}
				this.RandomizerParamsTimer.Start();
			}
		}

		// Token: 0x060001B8 RID: 440 RVA: 0x00014D00 File Offset: 0x00013D00
		private void RandomRotationToEditBox_TextChanged(object sender, EventArgs e)
		{
			if (this.created)
			{
				float angle;
				if (float.TryParse(((TextBox)sender).Text, out angle))
				{
					this.objectCreationRandomParams.RotationTo = angle;
				}
				this.RandomizerParamsTimer.Start();
			}
		}

		// Token: 0x060001B9 RID: 441 RVA: 0x00014D40 File Offset: 0x00013D40
		private void RandomPitchFromEditBox_TextChanged(object sender, EventArgs e)
		{
			if (this.created)
			{
				float angle;
				if (float.TryParse(((TextBox)sender).Text, out angle))
				{
					this.objectCreationRandomParams.PitchFrom = angle;
				}
				this.RandomizerParamsTimer.Start();
			}
		}

		// Token: 0x060001BA RID: 442 RVA: 0x00014D80 File Offset: 0x00013D80
		private void RandomPitchToEditBox_TextChanged(object sender, EventArgs e)
		{
			if (this.created)
			{
				float angle;
				if (float.TryParse(((TextBox)sender).Text, out angle))
				{
					this.objectCreationRandomParams.PitchTo = angle;
				}
				this.RandomizerParamsTimer.Start();
			}
		}

		// Token: 0x060001BB RID: 443 RVA: 0x00014DC0 File Offset: 0x00013DC0
		private void RandomScaleFromEditBox_TextChanged(object sender, EventArgs e)
		{
			if (this.created)
			{
				float scale;
				if (float.TryParse(((TextBox)sender).Text, out scale))
				{
					this.objectCreationRandomParams.ScaleFrom = scale;
				}
				this.RandomizerParamsTimer.Start();
			}
		}

		// Token: 0x060001BC RID: 444 RVA: 0x00014E00 File Offset: 0x00013E00
		private void RandomScaleToEditBox_TextChanged(object sender, EventArgs e)
		{
			if (this.created)
			{
				float scale;
				if (float.TryParse(((TextBox)sender).Text, out scale))
				{
					this.objectCreationRandomParams.ScaleTo = scale;
				}
				this.RandomizerParamsTimer.Start();
			}
		}

		// Token: 0x060001BD RID: 445 RVA: 0x00014E40 File Offset: 0x00013E40
		private void RandomizerParamsTimer_Tick(object sender, EventArgs e)
		{
			this.RandomizerParamsTimer.Stop();
			this.UpdateRandomizerParamsControls();
		}

		// Token: 0x060001BE RID: 446 RVA: 0x00014E54 File Offset: 0x00013E54
		private void OnZoneSizeChanged(int size)
		{
			if (this.created)
			{
				base.Context.StateContainer.Invoke("_map_zones_tab_size_changed", new MethodArgs(this, size, null));
				this.ZoneBrushSizeTextBox.Text = size * Constants.MapZonePieceSize.X + " x " + size * Constants.MapZonePieceSize.Y;
			}
		}

		// Token: 0x060001BF RID: 447 RVA: 0x00014EC8 File Offset: 0x00013EC8
		private void ZoneSize0Button_CheckedChanged(object sender, EventArgs e)
		{
			if (this.ZoneSize0Button.Checked)
			{
				this.OnZoneSizeChanged(this.zoneTSizes[0]);
			}
		}

		// Token: 0x060001C0 RID: 448 RVA: 0x00014EE5 File Offset: 0x00013EE5
		private void ZoneSize1Button_CheckedChanged(object sender, EventArgs e)
		{
			if (this.ZoneSize1Button.Checked)
			{
				this.OnZoneSizeChanged(this.zoneTSizes[1]);
			}
		}

		// Token: 0x060001C1 RID: 449 RVA: 0x00014F02 File Offset: 0x00013F02
		private void ZoneSize2Button_CheckedChanged(object sender, EventArgs e)
		{
			if (this.ZoneSize2Button.Checked)
			{
				this.OnZoneSizeChanged(this.zoneTSizes[2]);
			}
		}

		// Token: 0x060001C2 RID: 450 RVA: 0x00014F1F File Offset: 0x00013F1F
		private void ZoneSize3Button_CheckedChanged(object sender, EventArgs e)
		{
			if (this.ZoneSize3Button.Checked)
			{
				this.OnZoneSizeChanged(this.zoneTSizes[3]);
			}
		}

		// Token: 0x060001C3 RID: 451 RVA: 0x00014F3C File Offset: 0x00013F3C
		private void OnBindZonesItemList(MethodArgs methodArgs)
		{
			this.zoneFolderItemFiltes.Bind(this.zoneItemList.ItemFilters);
			this.zoneItemList.Bind(this.ZonesListView, this.ZoneComboBox);
		}

		// Token: 0x060001C4 RID: 452 RVA: 0x00014F6B File Offset: 0x00013F6B
		private void OnAddZonesItemListSource(MethodArgs methodArgs)
		{
			if (methodArgs.sender != null)
			{
				this.zoneItemList.ItemSources.Add(methodArgs.sender as ItemList.IItemSource);
			}
		}

		// Token: 0x060001C5 RID: 453 RVA: 0x00014F92 File Offset: 0x00013F92
		private void OnZonesItemSelected(ItemList sender, string item)
		{
			base.Context.StateContainer.Invoke("_map_zones_item_list_selected", new MethodArgs(this, item, null));
		}

		// Token: 0x060001C6 RID: 454 RVA: 0x00014FB1 File Offset: 0x00013FB1
		private void OnZonesItemUnselected(ItemList sender, string item)
		{
			base.Context.StateContainer.Invoke("_map_zones_item_list_unselected", new MethodArgs(this, item, null));
		}

		// Token: 0x060001C7 RID: 455 RVA: 0x00014FD0 File Offset: 0x00013FD0
		private void OnZonesItemListCleared(ItemList sender, string item)
		{
			base.Context.StateContainer.Invoke("_map_zones_item_list_cleared", new MethodArgs(this, item, null));
		}

		// Token: 0x060001C8 RID: 456 RVA: 0x00014FEF File Offset: 0x00013FEF
		private void OnZonesItemListRefreshing(ItemList sender, string item)
		{
			base.Context.StateContainer.Invoke("_map_zones_item_list_refreshing", new MethodArgs(this, item, null));
		}

		// Token: 0x060001C9 RID: 457 RVA: 0x00015010 File Offset: 0x00014010
		private void OnZoneItemChanged(MethodArgs methodArgs)
		{
			string item = methodArgs.sender as string;
			if (!string.IsNullOrEmpty(item))
			{
				this.zoneItemList.UpdateItem(item);
			}
		}

		// Token: 0x060001CA RID: 458 RVA: 0x00015040 File Offset: 0x00014040
		private void OnZonesTabIncreaseSize(MethodArgs methodArgs)
		{
			if (this.ZoneSize0Button.Checked)
			{
				this.ZoneSize0Button.Checked = false;
				this.ZoneSize1Button.Checked = true;
				return;
			}
			if (this.ZoneSize1Button.Checked)
			{
				this.ZoneSize1Button.Checked = false;
				this.ZoneSize2Button.Checked = true;
			}
		}

		// Token: 0x060001CB RID: 459 RVA: 0x00015098 File Offset: 0x00014098
		private void OnZonesTabDecreaseSize(MethodArgs methodArgs)
		{
			if (this.ZoneSize2Button.Checked)
			{
				this.ZoneSize2Button.Checked = false;
				this.ZoneSize1Button.Checked = true;
				return;
			}
			if (this.ZoneSize1Button.Checked)
			{
				this.ZoneSize1Button.Checked = false;
				this.ZoneSize0Button.Checked = true;
			}
		}

		// Token: 0x060001CC RID: 460 RVA: 0x000150F0 File Offset: 0x000140F0
		private void OnUnbindZonesItemList(MethodArgs methodArgs)
		{
			this.zoneItemList.Unbind();
			this.zoneFolderItemFiltes.Unbind();
			this.zoneItemList.ItemSources.Clear();
			this.zoneItemList.ItemFilters.Clear();
			this.zoneItemList.ItemDataMiners.Clear();
		}

		// Token: 0x060001CD RID: 461 RVA: 0x00015144 File Offset: 0x00014144
		private void OnZonesTabSendData(MethodArgs methodArgs)
		{
			base.Context.StateContainer.Invoke("_map_zones_item_list_selected", new MethodArgs(this, this.zoneItemList.SelectedItem, null));
			if (this.AddToZoneRadioButton.Checked)
			{
				base.Context.StateContainer.Invoke("_add_to_map_zone_choosed", new MethodArgs(this, null, null));
			}
			else
			{
				base.Context.StateContainer.Invoke("_clear_map_zone_choosed", new MethodArgs(this, null, null));
			}
			if (this.ZoneSize0Button.Checked)
			{
				this.OnZoneSizeChanged(this.zoneTSizes[0]);
				return;
			}
			if (this.ZoneSize1Button.Checked)
			{
				this.OnZoneSizeChanged(this.zoneTSizes[1]);
				return;
			}
			this.OnZoneSizeChanged(this.zoneTSizes[2]);
		}

		// Token: 0x060001CE RID: 462 RVA: 0x00015207 File Offset: 0x00014207
		private void OnZonesItemDoubleClicked(ItemList sender, string item)
		{
			base.Context.StateContainer.Invoke("_map_zones_item_edit", new MethodArgs(this, item, null));
		}

		// Token: 0x060001CF RID: 463 RVA: 0x00015228 File Offset: 0x00014228
		private void OnZonesListKeyDown(ItemList sender, EventArgs e, string item)
		{
			if (!string.IsNullOrEmpty(item))
			{
				KeyEventArgs ke = e as KeyEventArgs;
				if (ke != null && ke.KeyCode == Keys.Return)
				{
					base.Context.StateContainer.Invoke("_map_zones_item_edit", new MethodArgs(this, item, null));
				}
			}
		}

		// Token: 0x060001D0 RID: 464 RVA: 0x00015270 File Offset: 0x00014270
		private void OnZonesMenuClick(object sender, ToolStripItemClickedEventArgs e)
		{
			if (e.ClickedItem == this.AddZoneToolStripMenuItem)
			{
				base.Context.StateContainer.Invoke("_map_zones_item_add", new MethodArgs(this, null, null));
			}
			if (e.ClickedItem == this.RemoveZoneToolStripMenuItem && this.zoneItemList.SelectedItem != null)
			{
				base.Context.StateContainer.Invoke("_map_zones_item_remove", new MethodArgs(this, this.zoneItemList.SelectedItem, null));
			}
		}

		// Token: 0x060001D1 RID: 465 RVA: 0x000152EA File Offset: 0x000142EA
		private void zoneFilterEditorButton_Click(object sender, EventArgs e)
		{
			this.zoneFolderItemFiltes.ShowDialog(this.zoneItemList, base.Context.MainForm);
		}

		// Token: 0x060001D2 RID: 466 RVA: 0x00015309 File Offset: 0x00014309
		private void AddToZoneRadioButton_CheckedChanged(object sender, EventArgs e)
		{
			if (this.created && this.AddToZoneRadioButton.Checked)
			{
				base.Context.StateContainer.Invoke("_add_to_map_zone_choosed", new MethodArgs(this, null, null));
			}
		}

		// Token: 0x060001D3 RID: 467 RVA: 0x0001533D File Offset: 0x0001433D
		private void ClearRadioButton_CheckedChanged(object sender, EventArgs e)
		{
			if (this.created && this.ClearRadioButton.Checked)
			{
				base.Context.StateContainer.Invoke("_clear_map_zone_choosed", new MethodArgs(this, null, null));
			}
		}

		// Token: 0x060001D4 RID: 468 RVA: 0x00015374 File Offset: 0x00014374
		private void OnZoneListSelectItem(MethodArgs methodArgs)
		{
			string item = methodArgs.sender as string;
			if (!string.IsNullOrEmpty(item))
			{
				this.zoneItemList.SelectItem(item);
			}
		}

		// Token: 0x060001D5 RID: 469 RVA: 0x000153A3 File Offset: 0x000143A3
		private void OnBindLightItemList(MethodArgs methodArgs)
		{
			this.lightFolderItemFiltes.Bind(this.lightItemList.ItemFilters);
			this.lightItemList.Bind(this.LightListView, this.LightComboBox);
		}

		// Token: 0x060001D6 RID: 470 RVA: 0x000153D4 File Offset: 0x000143D4
		private void OnUnbindLightItemList(MethodArgs methodArgs)
		{
			this.lightItemList.Unbind();
			this.lightFolderItemFiltes.Unbind();
			this.lightItemList.ItemSources.Clear();
			this.lightItemList.ItemFilters.Clear();
			this.lightItemList.ItemDataMiners.Clear();
		}

		// Token: 0x060001D7 RID: 471 RVA: 0x00015427 File Offset: 0x00014427
		private void OnAddLightItemListSource(MethodArgs methodArgs)
		{
			if (methodArgs.sender != null)
			{
				this.lightItemList.ItemSources.Add(methodArgs.sender as ItemList.IItemSource);
			}
		}

		// Token: 0x060001D8 RID: 472 RVA: 0x00015450 File Offset: 0x00014450
		private void OnLightsTabSendData(MethodArgs methodArgs)
		{
			base.Context.StateContainer.Invoke("_light_item_list_selected", new MethodArgs(this, this.lightItemList.SelectedItem, null));
			if (this.SetLightRadioButton.Checked)
			{
				base.Context.StateContainer.Invoke("_set_light_choosed", new MethodArgs(this, null, null));
			}
			else
			{
				base.Context.StateContainer.Invoke("_clear_light_choosed", new MethodArgs(this, null, null));
			}
			if (this.LightSize0Button.Checked)
			{
				this.OnLightSizeChanged(this.zoneTSizes[0]);
				return;
			}
			if (this.LightSize1Button.Checked)
			{
				this.OnLightSizeChanged(this.zoneTSizes[1]);
				return;
			}
			this.OnLightSizeChanged(this.zoneTSizes[2]);
		}

		// Token: 0x060001D9 RID: 473 RVA: 0x00015514 File Offset: 0x00014514
		private void OnLightSizeChanged(int size)
		{
			if (this.created)
			{
				base.Context.StateContainer.Invoke("_light_tab_size_changed", new MethodArgs(this, size, null));
				this.LightBrushSizeTextBox.Text = size * Constants.MapZonePieceSize.X + " x " + size * Constants.MapZonePieceSize.Y;
			}
		}

		// Token: 0x060001DA RID: 474 RVA: 0x00015588 File Offset: 0x00014588
		private void OnLightItemSelected(ItemList sender, string item)
		{
			base.Context.StateContainer.Invoke("_light_item_list_selected", new MethodArgs(this, item, null));
		}

		// Token: 0x060001DB RID: 475 RVA: 0x000155A7 File Offset: 0x000145A7
		private void OnLightItemListCleared(ItemList sender, string item)
		{
			base.Context.StateContainer.Invoke("_light_item_list_cleared", new MethodArgs(this, item, null));
		}

		// Token: 0x060001DC RID: 476 RVA: 0x000155C6 File Offset: 0x000145C6
		private void OnLightItemListRefreshing(ItemList sender, string item)
		{
			base.Context.StateContainer.Invoke("_light_item_list_refreshing", new MethodArgs(this, item, null));
		}

		// Token: 0x060001DD RID: 477 RVA: 0x000155E5 File Offset: 0x000145E5
		private void OnLightItemDoubleClicked(ItemList sender, string item)
		{
			base.Context.StateContainer.Invoke("_light_item_edit", new MethodArgs(this, item, null));
		}

		// Token: 0x060001DE RID: 478 RVA: 0x00015604 File Offset: 0x00014604
		private void OnLightListKeyDown(ItemList sender, EventArgs e, string item)
		{
			if (!string.IsNullOrEmpty(item))
			{
				KeyEventArgs ke = e as KeyEventArgs;
				if (ke != null && ke.KeyCode == Keys.Return)
				{
					base.Context.StateContainer.Invoke("_light_item_edit", new MethodArgs(this, item, null));
				}
			}
		}

		// Token: 0x060001DF RID: 479 RVA: 0x0001564C File Offset: 0x0001464C
		private void OnLightMenuClick(object sender, ToolStripItemClickedEventArgs e)
		{
			if (e.ClickedItem == this.AddLightToolStripMenuItem)
			{
				base.Context.StateContainer.Invoke("_light_item_add", new MethodArgs(this, null, null));
			}
			if (e.ClickedItem == this.RemoveLightToolStripMenuItem && !string.IsNullOrEmpty(this.lightItemList.SelectedItem))
			{
				base.Context.StateContainer.Invoke("_light_item_remove", new MethodArgs(this, this.lightItemList.SelectedItem, null));
			}
		}

		// Token: 0x060001E0 RID: 480 RVA: 0x000156CC File Offset: 0x000146CC
		private void OnLightListSelectItem(MethodArgs methodArgs)
		{
			string item = methodArgs.sender as string;
			if (!string.IsNullOrEmpty(item))
			{
				this.lightItemList.SelectItem(item);
			}
		}

		// Token: 0x060001E1 RID: 481 RVA: 0x000156FC File Offset: 0x000146FC
		private void OnLightItemChanged(MethodArgs methodArgs)
		{
			string item = methodArgs.sender as string;
			if (!string.IsNullOrEmpty(item))
			{
				this.lightItemList.UpdateItem(item);
			}
		}

		// Token: 0x060001E2 RID: 482 RVA: 0x0001572C File Offset: 0x0001472C
		private void OnLightsTabIncreaseSize(MethodArgs methodArgs)
		{
			if (this.LightSize0Button.Checked)
			{
				this.LightSize0Button.Checked = false;
				this.LightSize1Button.Checked = true;
				return;
			}
			if (this.LightSize1Button.Checked)
			{
				this.LightSize1Button.Checked = false;
				this.LightSize2Button.Checked = true;
			}
		}

		// Token: 0x060001E3 RID: 483 RVA: 0x00015784 File Offset: 0x00014784
		private void OnLightsTabDecreaseSize(MethodArgs methodArgs)
		{
			if (this.LightSize2Button.Checked)
			{
				this.LightSize2Button.Checked = false;
				this.LightSize1Button.Checked = true;
				return;
			}
			if (this.LightSize1Button.Checked)
			{
				this.LightSize1Button.Checked = false;
				this.LightSize0Button.Checked = true;
			}
		}

		// Token: 0x060001E4 RID: 484 RVA: 0x000157DC File Offset: 0x000147DC
		private void LightSize0Button_CheckedChanged(object sender, EventArgs e)
		{
			if (this.LightSize0Button.Checked)
			{
				this.OnLightSizeChanged(this.zoneTSizes[0]);
			}
		}

		// Token: 0x060001E5 RID: 485 RVA: 0x000157F9 File Offset: 0x000147F9
		private void LightSize1Button_CheckedChanged(object sender, EventArgs e)
		{
			if (this.LightSize1Button.Checked)
			{
				this.OnLightSizeChanged(this.zoneTSizes[1]);
			}
		}

		// Token: 0x060001E6 RID: 486 RVA: 0x00015816 File Offset: 0x00014816
		private void LightSize2Button_CheckedChanged(object sender, EventArgs e)
		{
			if (this.LightSize2Button.Checked)
			{
				this.OnLightSizeChanged(this.zoneTSizes[2]);
			}
		}

		// Token: 0x060001E7 RID: 487 RVA: 0x00015833 File Offset: 0x00014833
		private void LightSize3Button_CheckedChanged(object sender, EventArgs e)
		{
			if (this.LightSize3Button.Checked)
			{
				this.OnLightSizeChanged(this.zoneTSizes[3]);
			}
		}

		// Token: 0x060001E8 RID: 488 RVA: 0x00015850 File Offset: 0x00014850
		private void SetLightRadioButton_CheckedChanged(object sender, EventArgs e)
		{
			if (this.created && this.AddToZoneRadioButton.Checked)
			{
				base.Context.StateContainer.Invoke("_set_light_choosed", new MethodArgs(this, null, null));
			}
		}

		// Token: 0x060001E9 RID: 489 RVA: 0x00015884 File Offset: 0x00014884
		private void radioButton17_CheckedChanged(object sender, EventArgs e)
		{
			if (this.created && this.AddToZoneRadioButton.Checked)
			{
				base.Context.StateContainer.Invoke("_clear_light_choosed", new MethodArgs(this, null, null));
			}
		}

		// Token: 0x060001EA RID: 490 RVA: 0x000158B8 File Offset: 0x000148B8
		private void LightFilterEditorButton_Click(object sender, EventArgs e)
		{
			this.lightFolderItemFiltes.ShowDialog(this.lightItemList, base.Context.MainForm);
		}

		// Token: 0x060001EB RID: 491 RVA: 0x000158D7 File Offset: 0x000148D7
		private void CreateStates()
		{
			base.Context.MainState.ActiveStateChanged += this.OnMainStateActiveStateChanged;
		}

		// Token: 0x060001EC RID: 492 RVA: 0x000158F5 File Offset: 0x000148F5
		private void DestroyStates()
		{
			base.Context.MainState.ActiveStateChanged -= this.OnMainStateActiveStateChanged;
		}

		// Token: 0x060001ED RID: 493 RVA: 0x00015914 File Offset: 0x00014914
		private void FillStates(ContinentType type)
		{
			if (this.substateRadioButtons.Count == 0)
			{
				this.substateRadioButtons.Add(new List<RadioButton>());
				base.Context.MainState.ActiveSubstates.Add(0);
				int stateIndex = this.substateRadioButtons.Count;
				this.substateRadioButtons.Add(new List<RadioButton>());
				this.substateRadioButtons[stateIndex].Add(this.LandscapeSubstateLeftButton00);
				this.substateRadioButtons[stateIndex].Add(this.LandscapeSubstateLeftButton01);
				this.substateRadioButtons[stateIndex].Add(this.LandscapeSubstateLeftButton02);
				this.substateRadioButtons[stateIndex].Add(this.LandscapeSubstateLeftButton03);
				this.substateRadioButtons[stateIndex].Add(this.LandscapeSubstateLeftButton04);
				this.substateRadioButtons[stateIndex].Add(this.LandscapeSubstateLeftButton05);
				this.substateRadioButtons[stateIndex].Add(this.LandscapeSubstateLeftButton06);
				this.substateRadioButtons[stateIndex].Add(this.LandscapeSubstateLeftButton07);
				this.substateRadioButtons[stateIndex].Add(this.LandscapeSubstateLeftButton08);
				this.substateRadioButtons[stateIndex].Add(this.LandscapeSubstateLeftButton09);
				base.Context.MainState.ActiveSubstates.Add(0);
				this.substateRadioButtons.Add(new List<RadioButton>());
				base.Context.MainState.ActiveSubstates.Add(0);
				this.substateRadioButtons.Add(new List<RadioButton>());
				base.Context.MainState.ActiveSubstates.Add(0);
				this.substateRadioButtons.Add(new List<RadioButton>());
				base.Context.MainState.ActiveSubstates.Add(0);
			}
		}

		// Token: 0x060001EE RID: 494 RVA: 0x00015AE4 File Offset: 0x00014AE4
		private void UpdateMainState()
		{
			if (this.created)
			{
				this.created = false;
				if (base.Context.MainState.ActiveState >= 0 && base.Context.MainState.ActiveState < this.MainTab.TabCount)
				{
					IntPtr previousFocusedForm = new IntPtr(User.GetFocus());
					this.MainTab.SelectedIndex = base.Context.MainState.ActiveState;
					User.SetFocus(previousFocusedForm);
				}
				else if (this.MainTab.TabCount > 0)
				{
					IntPtr previousFocusedForm2 = new IntPtr(User.GetFocus());
					base.Context.MainState.ActiveState = 0;
					this.MainTab.SelectedIndex = base.Context.MainState.ActiveState;
					User.SetFocus(previousFocusedForm2);
				}
				this.created = true;
			}
		}

		// Token: 0x060001EF RID: 495 RVA: 0x00015BB7 File Offset: 0x00014BB7
		private void MainTab_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (this.created)
			{
				base.Context.MainState.ActiveState = this.MainTab.SelectedIndex;
			}
		}

		// Token: 0x060001F0 RID: 496 RVA: 0x00015BDC File Offset: 0x00014BDC
		private void OnMainStateActiveStateChanged(MainState _mainState, ref int oldValue, ref int newValue)
		{
			this.UpdateMainState();
		}

		// Token: 0x0400001B RID: 27
		private readonly Dictionary<string, int> editBoxForUpdateNames = new Dictionary<string, int>();

		// Token: 0x0400001C RID: 28
		private static readonly int landscapeMainStateIndex = 1;

		// Token: 0x0400001D RID: 29
		private readonly List<RadioButton> landscapeToolLeftButtons = new List<RadioButton>();

		// Token: 0x0400001E RID: 30
		private readonly List<RadioButton> landscapeToolRightButtons = new List<RadioButton>();

		// Token: 0x0400001F RID: 31
		private readonly List<ToolForm.TilePickerControlElement> tilePickerControlElements = new List<ToolForm.TilePickerControlElement>();

		// Token: 0x04000020 RID: 32
		private LandscapeRegionParams landscapeRegionParams = new LandscapeRegionParams();

		// Token: 0x04000021 RID: 33
		private readonly LandscapeToolParamsContainer landscapeToolParamsContainer = new LandscapeToolParamsContainer();

		// Token: 0x04000022 RID: 34
		private readonly SimpleTimer leftLandscapeSimpleTimer = new SimpleTimer();

		// Token: 0x04000023 RID: 35
		private readonly SimpleTimer rightLandscapeSimpleTimer = new SimpleTimer();

		// Token: 0x04000024 RID: 36
		private FolderItemFilters landscapeTileFolderItemFiltes;

		// Token: 0x04000025 RID: 37
		private FolderItemFilters landscapeHeightFolderItemFiltes;

		// Token: 0x04000026 RID: 38
		private FolderItemFilters landscapeClipboardFolderItemFiltes;

		// Token: 0x04000027 RID: 39
		private FolderItemFilters landscapeHillFolderItemFiltes;

		// Token: 0x04000028 RID: 40
		private FolderItemFilters landscapeRoadFolderItemFiltes;

		// Token: 0x04000029 RID: 41
		private FolderItemFilters landscapeWaterFolderItemFiltes;

		// Token: 0x0400002A RID: 42
		private ItemList landscapeTileItemList;

		// Token: 0x0400002B RID: 43
		private ItemList landscapeHeightItemList;

		// Token: 0x0400002C RID: 44
		private ItemList landscapeClipboardItemList;

		// Token: 0x0400002D RID: 45
		private ItemList landscapeHillItemList;

		// Token: 0x0400002E RID: 46
		private ItemList landscapeRoadItemList;

		// Token: 0x0400002F RID: 47
		private ItemList landscapeWaterItemList;

		// Token: 0x04000030 RID: 48
		private static readonly int defaultLandscapeTileToolTileForPaint = 0;

		// Token: 0x04000031 RID: 49
		private static readonly string defaultLandscapeHeightToolFileName = string.Empty;

		// Token: 0x04000032 RID: 50
		private static readonly string defaultLandscapeClipboardToolFileName = string.Empty;

		// Token: 0x04000033 RID: 51
		private int bindedLandscapeToolParamsIndex = -1;

		// Token: 0x04000034 RID: 52
		private bool leftLandscapeToolBinded = true;

		// Token: 0x04000035 RID: 53
		private string continentName = string.Empty;

		// Token: 0x04000036 RID: 54
		private readonly Dictionary<int, int> landscapeToolTabIndexMap = new Dictionary<int, int>();

		// Token: 0x04000037 RID: 55
		private int landscapeTabControlSelectedIndexBackup;

		// Token: 0x0400017E RID: 382
		private static readonly double defaultOverAltitudeCache = 0.0;

		// Token: 0x0400017F RID: 383
		private static readonly double defaultUnderAltitudeCache = 0.0;

		// Token: 0x04000180 RID: 384
		private static readonly int maxGroupComboBoxItemCount = 25;

		// Token: 0x04000181 RID: 385
		private readonly State toolFormState = new State("ToolFormState");

		// Token: 0x04000182 RID: 386
		private readonly ObjectCreationRandomParams objectCreationRandomParams;

		// Token: 0x04000183 RID: 387
		private readonly int[] zoneTSizes = new int[]
		{
			1,
			4,
			16,
			0
		};

		// Token: 0x04000184 RID: 388
		private readonly ItemList zoneItemList;

		// Token: 0x04000185 RID: 389
		private readonly ItemList lightItemList;

		// Token: 0x04000186 RID: 390
		private readonly ItemList soundItemList;

		// Token: 0x04000187 RID: 391
		private readonly FolderItemFilters objectFolderItemFiltes = new FolderItemFilters(EditorEnvironment.EditorFolder + "Filters/ObjectFilters.xml", EditorEnvironment.EditorFormsFolder);

		// Token: 0x04000188 RID: 392
		private readonly FolderItemFilters zoneFolderItemFiltes = new FolderItemFilters(EditorEnvironment.EditorFolder + "Filters/ZoneFilters.xml", EditorEnvironment.EditorFormsFolder);

		// Token: 0x04000189 RID: 393
		private readonly FolderItemFilters lightFolderItemFiltes = new FolderItemFilters(EditorEnvironment.EditorFolder + "Filters/LightFilters.xml", EditorEnvironment.EditorFormsFolder);

		// Token: 0x0400018A RID: 394
		private readonly FolderItemFilters soundFolderItemFiltes = new FolderItemFilters(EditorEnvironment.EditorFolder + "Filters/SoundFilters.xml", EditorEnvironment.EditorFormsFolder);

		// Token: 0x0400018B RID: 395
		private readonly Random random = new Random(DateTime.Now.Millisecond);

		// Token: 0x0400018C RID: 396
		private bool created;

		// Token: 0x0400018D RID: 397
		private double overAltitudeCache = ToolForm.defaultOverAltitudeCache;

		// Token: 0x0400018E RID: 398
		private double underAltitudeCache = ToolForm.defaultUnderAltitudeCache;

		// Token: 0x0400018F RID: 399
		private LandscapeTileItemSource landscapeTileItemSource;

		// Token: 0x04000190 RID: 400
		private readonly ItemList objectItemList;

		// Token: 0x04000191 RID: 401
		private readonly StaticObjectItemListSource staticObjectItemListSource = new StaticObjectItemListSource();

		// Token: 0x04000192 RID: 402
		private readonly DBItemSource startPointItemListSource = new DBItemSource(string.Empty, StartPoint.CharacterDBType, false, true);

		// Token: 0x04000193 RID: 403
		private readonly MultiObjectItemListSource multiObjectItemListSource = new MultiObjectItemListSource();

		// Token: 0x04000194 RID: 404
		private readonly ObjectSetItemListSource objectSetItemListSource = new ObjectSetItemListSource();

		// Token: 0x04000197 RID: 407
		private readonly List<List<RadioButton>> substateRadioButtons = new List<List<RadioButton>>();

		// Token: 0x02000014 RID: 20
		private class TilePickerControlElement
		{
			// Token: 0x060001F2 RID: 498 RVA: 0x00015C34 File Offset: 0x00014C34
			public TilePickerControlElement(RadioButton _radioButton, PictureBox _pictureBox, Label _label)
			{
				this.radioButton = _radioButton;
				this.pictureBox = _pictureBox;
				this.label = _label;
			}

			// Token: 0x1700001B RID: 27
			// (get) Token: 0x060001F3 RID: 499 RVA: 0x00015C51 File Offset: 0x00014C51
			public RadioButton RadioButton
			{
				get
				{
					return this.radioButton;
				}
			}

			// Token: 0x1700001C RID: 28
			// (get) Token: 0x060001F4 RID: 500 RVA: 0x00015C59 File Offset: 0x00014C59
			public PictureBox PictureBox
			{
				get
				{
					return this.pictureBox;
				}
			}

			// Token: 0x1700001D RID: 29
			// (get) Token: 0x060001F5 RID: 501 RVA: 0x00015C61 File Offset: 0x00014C61
			public Label Label
			{
				get
				{
					return this.label;
				}
			}

			// Token: 0x04000198 RID: 408
			private readonly RadioButton radioButton;

			// Token: 0x04000199 RID: 409
			private readonly PictureBox pictureBox;

			// Token: 0x0400019A RID: 410
			private readonly Label label;
		}
	}
}
