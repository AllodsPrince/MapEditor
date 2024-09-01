using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;
using InputState;
using MapEditor.Forms.Base;
using MapEditor.Map;
using MapEditor.Resources.Strings;

namespace MapEditor.Forms.Layers
{
	// Token: 0x0200018C RID: 396
	public partial class LayersForm : BaseForm
	{
		// Token: 0x060012E4 RID: 4836 RVA: 0x0008B424 File Offset: 0x0008A424
		public LayersForm(MainForm.Context _context) : base(EditorEnvironment.EditorFormsFolder + "LayersForm.xml", _context)
		{
			this.InitializeComponent();
			this.LoadContinents();
			this.LoadLayers();
			this.LayerComboBox.SelectedIndexChanged += this.OnLayerChange;
			this.SaveButton.Click += this.OnSave;
			this.CopyLayerButton.Click += this.OnCopyLayer;
			this.NewLayerButton.Click += this.OnNewLayer;
			this.RenameLayerButton.Click += this.OnRenameLayer;
			this.MinimapColorButton.Click += this.OnChooseMinimapColor;
			this.ExportFoliageTextureButton.Click += this.OnExportFoliageTexture;
			this.DeleteFoliageTextureButton.Click += this.DeleteTexture;
			this.ExportTerrainTextureButton.Click += this.OnExportTerrainTexture;
			this.CloseButton.Click += this.OnCloseButtonPressed;
			this.ContinentComboBox.SelectedIndexChanged += this.OnContinentChange;
			this.Foliage0RadioButton.CheckedChanged += this.OnFoliageChange;
			this.Foliage1RadioButton.CheckedChanged += this.OnFoliageChange;
			this.Foliage2RadioButton.CheckedChanged += this.OnFoliageChange;
			this.Foliage3RadioButton.CheckedChanged += this.OnFoliageChange;
			base.VisibleChanged += this.OnVisibleChanged;
			this.TerrainTextureTextBox.TextChanged += this.OnChanged;
			this.FoliageTextureTextBox.TextChanged += this.OnChanged;
			this.ProbabilityTextBox.TextChanged += this.OnChanged;
			this.NumLeavesTextBox.TextChanged += this.OnChanged;
			this.MinScaleTextBox.TextChanged += this.OnChanged;
			this.MaxScaleTextBox.TextChanged += this.OnChanged;
			this.TopHeightTextBox.TextChanged += this.OnChanged;
			this.TopOffsetTextBox.TextChanged += this.OnChanged;
			this.TopWidthTextBox.TextChanged += this.OnChanged;
			this.BottomHeightTextBox.TextChanged += this.OnChanged;
			this.BottomOffsetTextBox.TextChanged += this.OnChanged;
			this.BottomWidthTextBox.TextChanged += this.OnChanged;
			this.ProbabilityTextBox.Validating += this.OnValidating;
			this.NumLeavesTextBox.Validating += this.OnValidating;
			this.MinScaleTextBox.Validating += this.OnValidating;
			this.MaxScaleTextBox.Validating += this.OnValidating;
			this.TopHeightTextBox.Validating += this.OnValidating;
			this.TopOffsetTextBox.Validating += this.OnValidating;
			this.TopWidthTextBox.Validating += this.OnValidating;
			this.BottomHeightTextBox.Validating += this.OnValidating;
			this.BottomOffsetTextBox.Validating += this.OnValidating;
			this.BottomWidthTextBox.Validating += this.OnValidating;
			this.ProbabilityTrackBar.ValueChanged += this.OnTrackBarValueChanged;
			this.NumLeavesTrackBar.ValueChanged += this.OnTrackBarValueChanged;
			this.MinScaleTrackBar.ValueChanged += this.OnTrackBarValueChanged;
			this.MaxScaleTrackBar.ValueChanged += this.OnTrackBarValueChanged;
			this.TopHeightTrackBar.ValueChanged += this.OnTrackBarValueChanged;
			this.TopOffsetTrackBar.ValueChanged += this.OnTrackBarValueChanged;
			this.TopWidthTrackBar.ValueChanged += this.OnTrackBarValueChanged;
			this.BottomHeightTrackBar.ValueChanged += this.OnTrackBarValueChanged;
			this.BottomOffsetTrackBar.ValueChanged += this.OnTrackBarValueChanged;
			this.BottomWidthTrackBar.ValueChanged += this.OnTrackBarValueChanged;
			this.layers.BeforeSelectedIndexChanged += this.OnBeforeLayerSelectedIndexChanged;
			this.layers.LayersListModified += this.OnLayersListModified;
			this.SaveButton.Enabled = false;
		}

		// Token: 0x060012E5 RID: 4837 RVA: 0x0008B8F4 File Offset: 0x0008A8F4
		private void LoadContinents()
		{
			List<string> continentNameList = new List<string>();
			Constants.GetContinentNameList(ref continentNameList);
			continentNameList.Sort();
			foreach (string continentName in continentNameList)
			{
				this.ContinentComboBox.Items.Add(continentName);
			}
		}

		// Token: 0x060012E6 RID: 4838 RVA: 0x0008B964 File Offset: 0x0008A964
		private void OnContinentChange(object sender, EventArgs e)
		{
			this.LoadLayers();
		}

		// Token: 0x060012E7 RID: 4839 RVA: 0x0008B96C File Offset: 0x0008A96C
		private void LoadLayers()
		{
			if (this.ContinentComboBox.Text == "")
			{
				return;
			}
			this.dbPath = this.ContinentComboBox.Text + "/layers.xdb";
			if (this.layers.Load(this.dbPath))
			{
				this.layers.LoadLayersToCmb(this.LayerComboBox);
			}
			this.GetData();
			this.SaveButton.Enabled = false;
		}

		// Token: 0x060012E8 RID: 4840 RVA: 0x0008B9E4 File Offset: 0x0008A9E4
		private void SetData()
		{
			if (this.layers.SelectedLayerIndex != -1 && this.foliageIndex != -1)
			{
				this.layers.SetMinimapColor(this.MinimapColorPanel.BackColor);
				int probability;
				if (int.TryParse(this.ProbabilityTextBox.Text, out probability))
				{
					this.layers.SetProbability(this.foliageIndex, probability);
				}
				int numLeaves;
				if (int.TryParse(this.NumLeavesTextBox.Text, out numLeaves))
				{
					this.layers.SetNumLeaves(this.foliageIndex, numLeaves);
				}
				float minScale;
				if (float.TryParse(this.MinScaleTextBox.Text, out minScale))
				{
					this.layers.SetMinScale(this.foliageIndex, minScale);
				}
				float maxScale;
				if (float.TryParse(this.MaxScaleTextBox.Text, out maxScale))
				{
					this.layers.SetMaxScale(this.foliageIndex, maxScale);
				}
				float topHeight;
				if (float.TryParse(this.TopHeightTextBox.Text, out topHeight))
				{
					this.layers.SetTopHeight(this.foliageIndex, float.Parse(this.TopHeightTextBox.Text));
				}
				float topOffeset;
				if (float.TryParse(this.TopOffsetTextBox.Text, out topOffeset))
				{
					this.layers.SetTopOffset(this.foliageIndex, topOffeset);
				}
				float topWidth;
				if (float.TryParse(this.TopWidthTextBox.Text, out topWidth))
				{
					this.layers.SetTopWidth(this.foliageIndex, topWidth);
				}
				float bootomHeight;
				if (float.TryParse(this.BottomHeightTextBox.Text, out bootomHeight))
				{
					this.layers.SetBottomHeight(this.foliageIndex, bootomHeight);
				}
				float bootomOffeset;
				if (float.TryParse(this.BottomOffsetTextBox.Text, out bootomOffeset))
				{
					this.layers.SetBottomOffset(this.foliageIndex, bootomOffeset);
				}
				float bootomWidth;
				if (float.TryParse(this.BottomWidthTextBox.Text, out bootomWidth))
				{
					this.layers.SetBottomWidth(this.foliageIndex, bootomWidth);
				}
			}
		}

		// Token: 0x060012E9 RID: 4841 RVA: 0x0008BBB4 File Offset: 0x0008ABB4
		private void GetData()
		{
			if (this.layers.SelectedLayerIndex != -1 && this.foliageIndex != -1)
			{
				bool isChanged = this.SaveButton.Enabled;
				this.TerrainTextureTextBox.Text = this.layers.GetTerrainTexture();
				this.MinimapColorPanel.BackColor = this.layers.GetMinimapColor();
				this.FoliageTextureTextBox.Text = this.layers.GetFoliageTexture(this.foliageIndex);
				int ival = this.layers.GetProbability(this.foliageIndex);
				this.ProbabilityTextBox.Text = ival.ToString();
				LayersForm.SetTrackBarValue(this.ProbabilityTrackBar, ival);
				ival = this.layers.GetNumLeaves(this.foliageIndex);
				this.NumLeavesTextBox.Text = ival.ToString();
				LayersForm.SetTrackBarValue(this.NumLeavesTrackBar, ival);
				float fval = this.layers.GetMinScale(this.foliageIndex);
				this.MinScaleTextBox.Text = fval.ToString();
				this.SetTrackBarValue(this.MinScaleTrackBar, fval);
				fval = this.layers.GetMaxScale(this.foliageIndex);
				this.MaxScaleTextBox.Text = fval.ToString();
				this.SetTrackBarValue(this.MaxScaleTrackBar, fval);
				fval = this.layers.GetTopHeight(this.foliageIndex);
				this.TopHeightTextBox.Text = fval.ToString();
				this.SetTrackBarValue(this.TopHeightTrackBar, fval);
				fval = this.layers.GetTopOffset(this.foliageIndex);
				this.TopOffsetTextBox.Text = fval.ToString();
				this.SetTrackBarValue(this.TopOffsetTrackBar, fval);
				fval = this.layers.GetTopWidth(this.foliageIndex);
				this.TopWidthTextBox.Text = fval.ToString();
				this.SetTrackBarValue(this.TopWidthTrackBar, fval);
				fval = this.layers.GetBottomHeight(this.foliageIndex);
				this.BottomHeightTextBox.Text = fval.ToString();
				this.SetTrackBarValue(this.BottomHeightTrackBar, fval);
				fval = this.layers.GetBottomOffset(this.foliageIndex);
				this.BottomOffsetTextBox.Text = fval.ToString();
				this.SetTrackBarValue(this.BottomOffsetTrackBar, fval);
				fval = this.layers.GetBottomWidth(this.foliageIndex);
				this.BottomWidthTextBox.Text = fval.ToString();
				this.SetTrackBarValue(this.BottomWidthTrackBar, fval);
				this.SaveButton.Enabled = isChanged;
			}
		}

		// Token: 0x060012EA RID: 4842 RVA: 0x0008BE28 File Offset: 0x0008AE28
		private void OnLayerChange(object sender, EventArgs e)
		{
			this.SetData();
			this.Foliage0RadioButton.Checked = true;
			this.layers.SelectedLayerIndex = this.LayerComboBox.SelectedIndex;
			this.GetData();
			this.CopyLayerButton.Enabled = this.layers.Check();
		}

		// Token: 0x060012EB RID: 4843 RVA: 0x0008BE79 File Offset: 0x0008AE79
		private void OnSave(object sender, EventArgs e)
		{
			this.Save();
		}

		// Token: 0x060012EC RID: 4844 RVA: 0x0008BE84 File Offset: 0x0008AE84
		private void OnCopyLayer(object sender, EventArgs e)
		{
			if (this.layers.SelectedLayerIndex != -1 || this.foliageIndex != -1)
			{
				this.OnBeforeLayerSelectedIndexChanged(this.layers.SelectedLayerIndex, this.layers.SelectedLayerIndex);
				string newLayer;
				if (this.GetLayerName(out newLayer) && this.layers.CopyLayer(newLayer))
				{
					this.LayerComboBox.Items.Add(newLayer);
					this.LayerComboBox.SelectedIndex = this.LayerComboBox.Items.Count - 1;
					this.CopyLayerButton.Enabled = this.layers.Check();
				}
			}
		}

		// Token: 0x060012ED RID: 4845 RVA: 0x0008BF24 File Offset: 0x0008AF24
		private void OnNewLayer(object sender, EventArgs e)
		{
			if (this.layers.SelectedLayerIndex != -1 || this.foliageIndex != -1)
			{
				this.OnBeforeLayerSelectedIndexChanged(this.layers.SelectedLayerIndex, this.layers.SelectedLayerIndex);
			}
			string newLayer;
			if (this.GetLayerName(out newLayer))
			{
				OpenFileDialog fileDialog = new OpenFileDialog();
				fileDialog.Filter = "Texture source files(*.tga, *.psd)|*.tga;*.psd";
				fileDialog.Multiselect = true;
				if (fileDialog.ShowDialog() == DialogResult.OK)
				{
					Cursor.Current = Cursors.WaitCursor;
					string dbidString;
					if (TerrainLayers.ExportTerrain(fileDialog.FileName, out dbidString))
					{
						if (this.layers.NewLayer(newLayer, dbidString))
						{
							this.LayerComboBox.Items.Add(newLayer);
							this.LayerComboBox.SelectedIndex = this.LayerComboBox.Items.Count - 1;
							this.TerrainTextureTextBox.Text = this.layers.GetTerrainTexture();
						}
					}
					else
					{
						MessageBox.Show(Strings.MAP_EXPORT_ERROR_MESSAGE, Strings.MAP_SAVE_CHANGES_TITLE, MessageBoxButtons.OK, MessageBoxIcon.Hand);
					}
					Cursor.Current = Cursors.Default;
				}
			}
		}

		// Token: 0x060012EE RID: 4846 RVA: 0x0008C024 File Offset: 0x0008B024
		private void OnRenameLayer(object sender, EventArgs e)
		{
			string newLayer;
			if ((this.layers.SelectedLayerIndex != -1 || this.foliageIndex != -1) && this.GetLayerName(out newLayer) && this.layers.RenameLayer(newLayer))
			{
				this.LayerComboBox.Items[this.layers.SelectedLayerIndex] = newLayer;
				this.SaveButton.Enabled = true;
			}
		}

		// Token: 0x060012EF RID: 4847 RVA: 0x0008C088 File Offset: 0x0008B088
		private bool GetLayerName(out string _name)
		{
			InputBoxForm inputBox = new InputBoxForm();
			inputBox.InputCaption = Strings.LAYER_ENTERNAME_TITLE;
			if (inputBox.ShowDialog() != DialogResult.OK)
			{
				_name = string.Empty;
				return false;
			}
			_name = inputBox.InputText;
			if (this.LayerComboBox.Items.Contains(_name))
			{
				MessageBox.Show(Strings.LAYER_NAMEALREADYEXISTS_MESSAGE, Strings.MAP_SAVE_CHANGES_TITLE, MessageBoxButtons.OK, MessageBoxIcon.Hand);
				return false;
			}
			return true;
		}

		// Token: 0x060012F0 RID: 4848 RVA: 0x0008C0EC File Offset: 0x0008B0EC
		private void Save()
		{
			Cursor.Current = Cursors.WaitCursor;
			this.SetData();
			this.layers.SaveDatabase();
			base.Context.EditorScene.UpdateLayers();
			this.SaveButton.Enabled = false;
			Cursor.Current = Cursors.Default;
		}

		// Token: 0x060012F1 RID: 4849 RVA: 0x0008C13C File Offset: 0x0008B13C
		private void OnExportFoliageTexture(object sender, EventArgs e)
		{
			if (!this.layers.Check())
			{
				return;
			}
			OpenFileDialog fileDialog = new OpenFileDialog();
			fileDialog.Filter = "Texture source files(*.tga, *.psd)|*.tga;*.psd";
			fileDialog.Multiselect = true;
			if (fileDialog.ShowDialog() == DialogResult.OK)
			{
				Cursor.Current = Cursors.WaitCursor;
				bool error = true;
				string dbidString;
				if (TerrainLayers.ExportFoliage(fileDialog.FileName, out dbidString))
				{
					this.layers.SetFoliage(this.foliageIndex, dbidString);
					this.FoliageTextureTextBox.Text = this.layers.GetFoliageTexture(this.foliageIndex);
					this.SaveButton.Enabled = true;
					if (TerrainLayers.ExportFoliageAtlas(this.layers.GetName()))
					{
						error = false;
						base.Context.EditorScene.UpdateLayers();
					}
				}
				if (error)
				{
					MessageBox.Show(Strings.MAP_EXPORT_ERROR_MESSAGE, Strings.MAP_SAVE_CHANGES_TITLE, MessageBoxButtons.OK, MessageBoxIcon.Hand);
				}
				Cursor.Current = Cursors.Default;
			}
		}

		// Token: 0x060012F2 RID: 4850 RVA: 0x0008C214 File Offset: 0x0008B214
		private void OnExportTerrainTexture(object sender, EventArgs e)
		{
			OpenFileDialog fileDialog = new OpenFileDialog();
			fileDialog.Filter = "Texture source files(*.tga, *.psd)|*.tga;*.psd";
			fileDialog.Multiselect = true;
			if (fileDialog.ShowDialog() == DialogResult.OK)
			{
				Cursor.Current = Cursors.WaitCursor;
				string dbidString;
				if (TerrainLayers.ExportTerrain(fileDialog.FileName, out dbidString))
				{
					this.layers.SetTerrain(dbidString);
					this.TerrainTextureTextBox.Text = this.layers.GetTerrainTexture();
					this.SaveButton.Enabled = true;
					base.Context.EditorScene.UpdateLayers();
				}
				else
				{
					MessageBox.Show(Strings.MAP_EXPORT_ERROR_MESSAGE, Strings.MAP_SAVE_CHANGES_TITLE, MessageBoxButtons.OK, MessageBoxIcon.Hand);
				}
				Cursor.Current = Cursors.Default;
			}
		}

		// Token: 0x060012F3 RID: 4851 RVA: 0x0008C2B8 File Offset: 0x0008B2B8
		private void DeleteTexture(object sender, EventArgs e)
		{
			this.layers.SetFoliage(this.foliageIndex, string.Empty);
			this.FoliageTextureTextBox.Text = "";
			this.SaveButton.Enabled = true;
		}

		// Token: 0x060012F4 RID: 4852 RVA: 0x0008C2EC File Offset: 0x0008B2EC
		private void OnFoliageChange(object sender, EventArgs e)
		{
			this.SetData();
			if (this.Foliage0RadioButton.Checked)
			{
				this.foliageIndex = 0;
			}
			if (this.Foliage1RadioButton.Checked)
			{
				this.foliageIndex = 1;
			}
			if (this.Foliage2RadioButton.Checked)
			{
				this.foliageIndex = 2;
			}
			if (this.Foliage3RadioButton.Checked)
			{
				this.foliageIndex = 3;
			}
			this.GetData();
		}

		// Token: 0x060012F5 RID: 4853 RVA: 0x0008C358 File Offset: 0x0008B358
		private void OnVisibleChanged(object sender, EventArgs e)
		{
			if (!base.Visible && this.SaveButton.Enabled && this.layers.Check())
			{
				if (this.ShowSaveDoalog(base.Context.MainForm))
				{
					this.Save();
					return;
				}
				if (!base.Context.FormClosing)
				{
					this.layers.ResetValues();
					this.GetData();
					base.Context.EditorScene.UpdateLayers();
					this.SaveButton.Enabled = false;
				}
			}
		}

		// Token: 0x060012F6 RID: 4854 RVA: 0x0008C3DC File Offset: 0x0008B3DC
		private void OnChanged(object sender, EventArgs e)
		{
			this.SaveButton.Enabled = true;
		}

		// Token: 0x060012F7 RID: 4855 RVA: 0x0008C3EC File Offset: 0x0008B3EC
		private void SetDataFromControl(TextBox textBox, bool updateTrackBar)
		{
			if (textBox == null)
			{
				return;
			}
			if (this.layers.SelectedLayerIndex != -1 || this.foliageIndex != -1)
			{
				TrackBar trackBar = null;
				if (textBox == this.ProbabilityTextBox || textBox == this.NumLeavesTextBox)
				{
					int val;
					if (!int.TryParse(textBox.Text, out val))
					{
						val = 0;
					}
					if (textBox == this.ProbabilityTextBox)
					{
						this.layers.SetProbability(this.foliageIndex, val);
						trackBar = this.ProbabilityTrackBar;
					}
					if (textBox == this.NumLeavesTextBox)
					{
						this.layers.SetNumLeaves(this.foliageIndex, val);
						trackBar = this.NumLeavesTrackBar;
					}
					if (trackBar != null)
					{
						textBox.Text = val.ToString();
						if (updateTrackBar)
						{
							LayersForm.SetTrackBarValue(trackBar, val);
						}
					}
				}
				if (textBox == this.MinScaleTextBox || textBox == this.MaxScaleTextBox || textBox == this.TopHeightTextBox || textBox == this.TopOffsetTextBox || textBox == this.TopWidthTextBox || textBox == this.BottomHeightTextBox || textBox == this.BottomOffsetTextBox || textBox == this.BottomWidthTextBox)
				{
					float val2;
					if (!float.TryParse(textBox.Text, out val2))
					{
						val2 = 0f;
					}
					if (textBox == this.MinScaleTextBox)
					{
						this.layers.SetMinScale(this.foliageIndex, val2);
						trackBar = this.MinScaleTrackBar;
					}
					if (textBox == this.MaxScaleTextBox)
					{
						this.layers.SetMaxScale(this.foliageIndex, val2);
						trackBar = this.MaxScaleTrackBar;
					}
					if (textBox == this.TopHeightTextBox)
					{
						this.layers.SetTopHeight(this.foliageIndex, val2);
						trackBar = this.TopHeightTrackBar;
					}
					if (textBox == this.TopOffsetTextBox)
					{
						this.layers.SetTopOffset(this.foliageIndex, val2);
						trackBar = this.TopOffsetTrackBar;
					}
					if (textBox == this.TopWidthTextBox)
					{
						this.layers.SetTopWidth(this.foliageIndex, val2);
						trackBar = this.TopWidthTrackBar;
					}
					if (textBox == this.BottomHeightTextBox)
					{
						this.layers.SetBottomHeight(this.foliageIndex, val2);
						trackBar = this.BottomHeightTrackBar;
					}
					if (textBox == this.BottomOffsetTextBox)
					{
						this.layers.SetBottomOffset(this.foliageIndex, val2);
						trackBar = this.BottomOffsetTrackBar;
					}
					if (textBox == this.BottomWidthTextBox)
					{
						this.layers.SetBottomWidth(this.foliageIndex, val2);
						trackBar = this.BottomWidthTrackBar;
					}
					if (trackBar != null)
					{
						textBox.Text = val2.ToString();
						if (updateTrackBar)
						{
							this.SetTrackBarValue(trackBar, val2);
						}
					}
				}
				if (!base.Context.FormClosing)
				{
					base.Context.EditorScene.UpdateLayers();
				}
			}
		}

		// Token: 0x060012F8 RID: 4856 RVA: 0x0008C644 File Offset: 0x0008B644
		private void OnValidating(object sender, CancelEventArgs e)
		{
			if (base.Visible)
			{
				TextBox textBox = sender as TextBox;
				if (textBox != null)
				{
					this.SetDataFromControl(textBox, true);
				}
			}
		}

		// Token: 0x060012F9 RID: 4857 RVA: 0x0008C66C File Offset: 0x0008B66C
		private void OnChooseMinimapColor(object sender, EventArgs e)
		{
			ColorDialog colorDialog = new ColorDialog();
			if (colorDialog.ShowDialog() == DialogResult.OK)
			{
				this.MinimapColorPanel.BackColor = colorDialog.Color;
				this.SaveButton.Enabled = true;
			}
		}

		// Token: 0x060012FA RID: 4858 RVA: 0x0008C6A8 File Offset: 0x0008B6A8
		private void OnBeforeLayerSelectedIndexChanged(int oldIndex, int newIndex)
		{
			if (this.SaveButton.Enabled && this.layers.Check())
			{
				if (this.ShowSaveDoalog(this))
				{
					this.Save();
					return;
				}
				this.layers.ResetValues();
				base.Context.EditorScene.UpdateLayers();
				this.SaveButton.Enabled = false;
			}
		}

		// Token: 0x060012FB RID: 4859 RVA: 0x0008C708 File Offset: 0x0008B708
		private bool ShowSaveDoalog(Form dialogOwner)
		{
			DialogResult dialogResult = MessageBox.Show(dialogOwner, Strings.MAP_SAVE_CHANGES_MESSAGE, Strings.LAYER_EDITOR_TITLE, MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);
			if (dialogResult == DialogResult.Yes)
			{
				this.Save();
				return true;
			}
			return false;
		}

		// Token: 0x060012FC RID: 4860 RVA: 0x0008C738 File Offset: 0x0008B738
		private void OnTrackBarValueChanged(object sender, EventArgs e)
		{
			TrackBar trackBar = sender as TrackBar;
			TextBox textBox = null;
			bool floatVal = false;
			if (trackBar != null)
			{
				if (trackBar == this.ProbabilityTrackBar)
				{
					textBox = this.ProbabilityTextBox;
					floatVal = false;
				}
				if (trackBar == this.NumLeavesTrackBar)
				{
					textBox = this.NumLeavesTextBox;
					floatVal = false;
				}
				if (trackBar == this.MinScaleTrackBar)
				{
					textBox = this.MinScaleTextBox;
					floatVal = true;
				}
				if (trackBar == this.MaxScaleTrackBar)
				{
					textBox = this.MaxScaleTextBox;
					floatVal = true;
				}
				if (trackBar == this.TopHeightTrackBar)
				{
					textBox = this.TopHeightTextBox;
					floatVal = true;
				}
				if (trackBar == this.TopWidthTrackBar)
				{
					textBox = this.TopWidthTextBox;
					floatVal = true;
				}
				if (trackBar == this.TopOffsetTrackBar)
				{
					textBox = this.TopOffsetTextBox;
					floatVal = true;
				}
				if (trackBar == this.BottomHeightTrackBar)
				{
					textBox = this.BottomHeightTextBox;
					floatVal = true;
				}
				if (trackBar == this.BottomWidthTrackBar)
				{
					textBox = this.BottomWidthTextBox;
					floatVal = true;
				}
				if (trackBar == this.BottomOffsetTrackBar)
				{
					textBox = this.BottomOffsetTextBox;
					floatVal = true;
				}
				if (textBox != null)
				{
					if (floatVal)
					{
						textBox.Text = (Convert.ToSingle(trackBar.Value) / (float)this.floatTRackBatCoeff).ToString();
					}
					else
					{
						textBox.Text = trackBar.Value.ToString();
					}
					this.SetDataFromControl(textBox, false);
				}
			}
		}

		// Token: 0x060012FD RID: 4861 RVA: 0x0008C850 File Offset: 0x0008B850
		private static void SetTrackBarValue(TrackBar trackBar, int val)
		{
			if (val < trackBar.Minimum)
			{
				val = trackBar.Minimum;
			}
			if (val > trackBar.Maximum)
			{
				val = trackBar.Maximum;
			}
			trackBar.Value = val;
		}

		// Token: 0x060012FE RID: 4862 RVA: 0x0008C87B File Offset: 0x0008B87B
		private void SetTrackBarValue(TrackBar trackBar, float val)
		{
			LayersForm.SetTrackBarValue(trackBar, Convert.ToInt32(val * (float)this.floatTRackBatCoeff));
		}

		// Token: 0x060012FF RID: 4863 RVA: 0x0008C891 File Offset: 0x0008B891
		private void OnLayersListModified()
		{
			base.Context.StateContainer.Invoke("_layers_list_modified", new MethodArgs(this, null, null));
		}

		// Token: 0x06001300 RID: 4864 RVA: 0x0008C8B0 File Offset: 0x0008B8B0
		private void OnCloseButtonPressed(object sender, EventArgs e)
		{
			base.Close();
		}

		// Token: 0x04000D83 RID: 3459
		private const string layersFile = "layers.xdb";

		// Token: 0x04000DD0 RID: 3536
		public Layers layers = new Layers();

		// Token: 0x04000DD1 RID: 3537
		private string dbPath;

		// Token: 0x04000DD2 RID: 3538
		private int foliageIndex;

		// Token: 0x04000DD3 RID: 3539
		private readonly int floatTRackBatCoeff = 20;
	}
}
