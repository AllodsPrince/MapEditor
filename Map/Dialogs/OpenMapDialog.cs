using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using MapEditor.Resources.Strings;
using Tools.Geometry;
using Tools.MapObjects;
using Tools.WindowParams;

namespace MapEditor.Map.Dialogs
{
	// Token: 0x02000166 RID: 358
	public partial class OpenMapDialog : Form
	{
		// Token: 0x06001177 RID: 4471 RVA: 0x00081B2C File Offset: 0x00080B2C
		private void OnLoadParams(FormParams formParams)
		{
			string continent = this.cmbBoxName.Text;
			this.LoadContinentCmbBox();
			if (this.cmbBoxName.Items.Count > 0)
			{
				this.cmbBoxName.SelectedIndex = 0;
				for (int index = 0; index < this.cmbBoxName.Items.Count; index++)
				{
					if (this.cmbBoxName.Items[index].ToString() == continent)
					{
						this.cmbBoxName.SelectedIndex = index;
					}
				}
			}
			this.paramsSaver.FormParams.ResizeInt(3, new int[]
			{
				1,
				OpenMapDialog.defaultVisibleAreaPosition.X,
				OpenMapDialog.defaultVisibleAreaPosition.Y
			});
			Tools.Geometry.Point visibleAreaPosition = new Tools.Geometry.Point(this.paramsSaver.FormParams.GetInt(1), this.paramsSaver.FormParams.GetInt(2));
			this.mapDialogControl.Initialize(this.resultParams, this.paramsSaver.FormParams.GetInt(0), visibleAreaPosition);
			this.resultParams.CreateTerrain = this.createTerrainCheckbox.Checked;
			this.resultParams.CreateBottom = this.createBottomCheckbox.Checked;
			this.resultParams.MapSize = (this.size4RadioButton.Checked ? 4 : 1);
			this.paramsSaver.FormParams.ResizeDouble(3, new double[]
			{
				Position.Empty.X + (double)Constants.PatchSize * (double)this.resultParams.MapSize / 2.0,
				Position.Empty.Y + (double)Constants.PatchSize * (double)this.resultParams.MapSize / 2.0,
				Position.Empty.Z
			});
			this.resultParams.CameraPosition = new Position(this.paramsSaver.FormParams.GetDouble(0), this.paramsSaver.FormParams.GetDouble(1), this.paramsSaver.FormParams.GetDouble(2));
			this.resultParams.Name = this.cmbBoxName.Text;
			this.SetParamUnit();
			this.cmbBoxFilters.SelectedIndexChanged += this.OnFilterChanged;
			this.cmbBoxName.SelectedIndexChanged += this.OnContinentChanged;
			this.filterEditorButton.Click += this.OnEditFilterClick;
			this.mapDialogControl.MouseDoubleClick += this.OnMapDialogControl_MouseDoubleClick;
			this.textBoxX.LostFocus += this.OnTextBoxLostFocus;
			this.textBoxY.LostFocus += this.OnTextBoxLostFocus;
			this.textBoxX.KeyUp += new KeyEventHandler(this.OnTextBoxKeyUp);
			this.textBoxY.KeyUp += new KeyEventHandler(this.OnTextBoxKeyUp);
			base.MouseWheel += this.OnMouseWheel;
			this.PatchRadioButton.CheckedChanged += this.OnCoordChoosed;
			this.GlCoordRadioButton.CheckedChanged += this.OnCoordChoosed;
			this.OpenMapDialogTimer.Tick += this.OnTime_Tick;
			this.size4RadioButton.CheckedChanged += this.OnSize4RadioButtonCHeckedChanged;
			this.mapDialogControl.EnableDraw = true;
		}

		// Token: 0x06001178 RID: 4472 RVA: 0x00081EA0 File Offset: 0x00080EA0
		private void OnSize4RadioButtonCHeckedChanged(object sender, EventArgs e)
		{
			this.resultParams.MapSize = (this.size4RadioButton.Checked ? 4 : 1);
		}

		// Token: 0x06001179 RID: 4473 RVA: 0x00081EC0 File Offset: 0x00080EC0
		private void OnSaveParams(FormParams formParams)
		{
			this.paramsSaver.FormParams.ResizeInt(3);
			this.paramsSaver.FormParams.SetInt(0, this.mapDialogControl.GetZoom());
			this.paramsSaver.FormParams.SetInt(1, this.mapDialogControl.GetVisibleAreaPosition().X);
			this.paramsSaver.FormParams.SetInt(2, this.mapDialogControl.GetVisibleAreaPosition().Y);
			this.paramsSaver.FormParams.ResizeDouble(3);
			this.paramsSaver.FormParams.SetDouble(0, this.resultParams.CameraPosition.X);
			this.paramsSaver.FormParams.SetDouble(1, this.resultParams.CameraPosition.Y);
			this.paramsSaver.FormParams.SetDouble(2, this.resultParams.CameraPosition.Z);
		}

		// Token: 0x0600117A RID: 4474 RVA: 0x00081FC0 File Offset: 0x00080FC0
		private void SetParamPositionFromTextBoxes()
		{
			int x;
			int y;
			if (!string.IsNullOrEmpty(this.textBoxX.Text) && !string.IsNullOrEmpty(this.textBoxY.Text) && int.TryParse(this.textBoxX.Text, out x) && int.TryParse(this.textBoxY.Text, out y))
			{
				Tools.Geometry.Point posTmp = new Tools.Geometry.Point(x, y);
				if (this.PatchRadioButton.Checked)
				{
					this.resultParams.SetPatch(posTmp);
					return;
				}
				if (this.CoordRadioButton.Checked)
				{
					this.resultParams.CameraPosition = new Position((double)posTmp.X, (double)posTmp.Y, 0.0);
					return;
				}
				if (this.GlCoordRadioButton.Checked)
				{
					this.resultParams.CameraPosition = new Position(0f, 0f, 0f, posTmp.X, posTmp.Y, 0);
				}
			}
		}

		// Token: 0x0600117B RID: 4475 RVA: 0x000820BC File Offset: 0x000810BC
		private void SetPositionTextBoxes()
		{
			bool continent = this.resultParams.Type == ContinentType.Continent;
			this.PatchRadioButton.Enabled = continent;
			this.CoordRadioButton.Enabled = continent;
			this.GlCoordRadioButton.Enabled = continent;
			this.labelX.Enabled = continent;
			this.labelY.Enabled = continent;
			this.textBoxX.Enabled = continent;
			this.textBoxY.Enabled = continent;
			this.createTerrainCheckbox.Enabled = continent;
			this.createBottomCheckbox.Enabled = continent;
			this.mapDialogControl.Enabled = continent;
			if (continent)
			{
				Tools.Geometry.Point posTmp;
				if (this.PatchRadioButton.Checked)
				{
					posTmp = this.resultParams.GetPatch();
				}
				else if (this.CoordRadioButton.Checked)
				{
					posTmp = new Tools.Geometry.Point((int)this.resultParams.CameraPosition.X, (int)this.resultParams.CameraPosition.Y);
				}
				else
				{
					if (!this.GlCoordRadioButton.Checked)
					{
						return;
					}
					posTmp = new Tools.Geometry.Point(this.resultParams.CameraPosition.GlobalX, this.resultParams.CameraPosition.GlobalY);
				}
				this.textBoxX.Text = posTmp.X.ToString();
				this.textBoxY.Text = posTmp.Y.ToString();
				Tools.Geometry.Point patch = this.resultParams.GetPatch();
				this.AlternativeCoordLabel.Text = string.Format("{0} ({3}, {4}); {1}: ({5}, {6}); {2}: ({7}, {8});", new object[]
				{
					this.PatchRadioButton.Text,
					this.CoordRadioButton.Text,
					this.GlCoordRadioButton.Text,
					patch.X,
					patch.Y,
					this.resultParams.CameraPosition.X,
					this.resultParams.CameraPosition.Y,
					this.resultParams.CameraPosition.GlobalX,
					this.resultParams.CameraPosition.GlobalY
				});
				return;
			}
			this.textBoxX.Text = string.Empty;
			this.textBoxY.Text = string.Empty;
			this.AlternativeCoordLabel.Text = Strings.OPEN_FILE_DIALOG_ASTRAL_HUB_DESCRIPTION;
		}

		// Token: 0x0600117C RID: 4476 RVA: 0x00082343 File Offset: 0x00081343
		private void OpdateOKButton()
		{
			this.OKbutton.Enabled = (!string.IsNullOrEmpty(this.resultParams.Name) && this.resultParams.Type != ContinentType.Unknown);
		}

		// Token: 0x0600117D RID: 4477 RVA: 0x00082378 File Offset: 0x00081378
		private void SetParamUnit()
		{
			if (this.PatchRadioButton.Checked)
			{
				this.resultParams.Unit = OpenMapDialog.Params.Units.Patch;
				return;
			}
			if (this.CoordRadioButton.Checked)
			{
				this.resultParams.Unit = OpenMapDialog.Params.Units.Coordinate;
				return;
			}
			if (this.GlCoordRadioButton.Checked)
			{
				this.resultParams.Unit = OpenMapDialog.Params.Units.GlobalCoordinate;
			}
		}

		// Token: 0x0600117E RID: 4478 RVA: 0x000823D4 File Offset: 0x000813D4
		private void SetUnitRB()
		{
			switch (this.resultParams.Unit)
			{
			case OpenMapDialog.Params.Units.Patch:
				this.PatchRadioButton.Checked = true;
				return;
			case OpenMapDialog.Params.Units.Coordinate:
				this.CoordRadioButton.Checked = true;
				return;
			case OpenMapDialog.Params.Units.GlobalCoordinate:
				this.GlCoordRadioButton.Checked = true;
				return;
			default:
				return;
			}
		}

		// Token: 0x0600117F RID: 4479 RVA: 0x00082426 File Offset: 0x00081426
		private void OnTextBoxKeyUp(object sender, EventArgs e)
		{
			this.OpenMapDialogTimer.Start();
		}

		// Token: 0x06001180 RID: 4480 RVA: 0x00082433 File Offset: 0x00081433
		private void OnTextBoxLostFocus(object sender, EventArgs e)
		{
			this.SetParamPositionFromTextBoxes();
		}

		// Token: 0x06001181 RID: 4481 RVA: 0x0008243B File Offset: 0x0008143B
		private void OnTime_Tick(object sender, EventArgs e)
		{
			this.OpenMapDialogTimer.Stop();
			this.SetParamPositionFromTextBoxes();
		}

		// Token: 0x06001182 RID: 4482 RVA: 0x00082450 File Offset: 0x00081450
		private void OnParamDataChanged(OpenMapDialog.Params.Fields field)
		{
			switch (field)
			{
			case OpenMapDialog.Params.Fields.Type:
				this.SetPositionTextBoxes();
				break;
			case OpenMapDialog.Params.Fields.Name:
				this.cmbBoxName.Text = this.resultParams.Name;
				break;
			case OpenMapDialog.Params.Fields.Position:
				this.SetPositionTextBoxes();
				break;
			case OpenMapDialog.Params.Fields.Terrain:
				this.createTerrainCheckbox.Checked = this.resultParams.CreateTerrain;
				this.createBottomCheckbox.Checked = this.resultParams.CreateBottom;
				break;
			case OpenMapDialog.Params.Fields.Unit:
				this.SetUnitRB();
				break;
			case OpenMapDialog.Params.Fields.Size:
				this.size1RadioButton.Checked = (this.resultParams.MapSize == 1);
				break;
			}
			this.OpdateOKButton();
		}

		// Token: 0x06001183 RID: 4483 RVA: 0x000824FC File Offset: 0x000814FC
		private void OnContinentChanged(object sender, EventArgs e)
		{
			this.resultParams.Name = this.cmbBoxName.Text;
		}

		// Token: 0x06001184 RID: 4484 RVA: 0x00082514 File Offset: 0x00081514
		private void OnMouseWheel(object sender, MouseEventArgs e)
		{
			int delta = 1;
			if (e.Delta < 0)
			{
				delta = -1;
			}
			this.mapDialogControl.SetZoom(this.mapDialogControl.GetZoom() + delta);
		}

		// Token: 0x06001185 RID: 4485 RVA: 0x00082546 File Offset: 0x00081546
		private void OnCoordChoosed(object sender, EventArgs e)
		{
			this.SetParamUnit();
			this.SetPositionTextBoxes();
		}

		// Token: 0x06001186 RID: 4486 RVA: 0x00082554 File Offset: 0x00081554
		private void OnMapDialogControl_MouseDoubleClick(object sender, MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Left)
			{
				base.DialogResult = DialogResult.OK;
			}
		}

		// Token: 0x06001187 RID: 4487 RVA: 0x0008256A File Offset: 0x0008156A
		private void createTerrainCheckbox_CheckedChanged(object sender, EventArgs e)
		{
			this.resultParams.CreateTerrain = this.createTerrainCheckbox.Checked;
		}

		// Token: 0x06001188 RID: 4488 RVA: 0x00082582 File Offset: 0x00081582
		private void createBottomCheckbox_CheckedChanged(object sender, EventArgs e)
		{
			this.resultParams.CreateBottom = this.createBottomCheckbox.Checked;
		}

		// Token: 0x06001189 RID: 4489 RVA: 0x0008259C File Offset: 0x0008159C
		private void OpenMapDialog_FormClosing(object sender, FormClosingEventArgs e)
		{
			this.SetParamPositionFromTextBoxes();
			if (!this.resultParams.IsValid() && base.DialogResult == DialogResult.OK)
			{
				MessageBox.Show(Strings.OPEN_MAP_INCORECT_DATA_MESSAGE, Strings.OPEN_MAP_TITLE, MessageBoxButtons.OK, MessageBoxIcon.Hand);
				e.Cancel = true;
			}
			this.resultParams.Changed -= this.OnParamDataChanged;
			this.folderItemFilters.Unbind();
		}

		// Token: 0x0600118A RID: 4490 RVA: 0x00082604 File Offset: 0x00081604
		private void LoadContinentCmbBox()
		{
			ItemList.IItemFilter filter = null;
			if (!string.IsNullOrEmpty(this.cmbBoxFilters.Text))
			{
				foreach (ItemList.IItemFilter itemFilter in this.filters)
				{
					if (itemFilter.Name == this.cmbBoxFilters.Text)
					{
						filter = itemFilter;
						break;
					}
				}
			}
			this.cmbBoxName.Items.Clear();
			foreach (string continentName in this.continentItemListSource.Items)
			{
				if (filter == null || filter.Valid(continentName))
				{
					this.cmbBoxName.Items.Add(continentName);
				}
			}
			if (this.cmbBoxName.Items.Count > 0)
			{
				this.cmbBoxName.SelectedIndex = 0;
			}
		}

		// Token: 0x0600118B RID: 4491 RVA: 0x0008270C File Offset: 0x0008170C
		private void OnFilterChanged(object sender, EventArgs e)
		{
			this.LoadContinentCmbBox();
		}

		// Token: 0x0600118C RID: 4492 RVA: 0x00082714 File Offset: 0x00081714
		private void UpdateFilters(string selectedFilterName)
		{
			this.cmbBoxFilters.Items.Clear();
			foreach (ItemList.IItemFilter filter in this.filters)
			{
				this.cmbBoxFilters.Items.Add(filter.Name);
			}
			if (!string.IsNullOrEmpty(selectedFilterName) && this.cmbBoxFilters.Items.Contains(selectedFilterName))
			{
				this.cmbBoxFilters.SelectedItem = selectedFilterName;
				return;
			}
			if (this.cmbBoxFilters.Items.Count > 0)
			{
				this.cmbBoxFilters.SelectedIndex = 0;
			}
		}

		// Token: 0x0600118D RID: 4493 RVA: 0x000827D0 File Offset: 0x000817D0
		private void OnEditFilterClick(object sender, EventArgs e)
		{
			OpenMapDialog.ContinentItemListSource[] continentItemListSourceArr = new OpenMapDialog.ContinentItemListSource[]
			{
				this.continentItemListSource
			};
			this.folderItemFilters.ShowDialog(continentItemListSourceArr, this.cmbBoxName.Text, this, new FolderItemFilters.SetFilterDelegate(this.UpdateFilters));
		}

		// Token: 0x0600118E RID: 4494 RVA: 0x00082814 File Offset: 0x00081814
		public OpenMapDialog()
		{
			this.InitializeComponent();
			this.continentItemListSource = new OpenMapDialog.ContinentItemListSource();
			this.folderItemFilters.Bind(this.filters);
			this.UpdateFilters(null);
			this.LoadContinentCmbBox();
			this.paramsSaver = new FormParamsSaver(this, EditorEnvironment.EditorFormsFolder + "OpenMapDialog.xml", false);
			this.paramsSaver.RegisterControl(new RadioButton[]
			{
				this.PatchRadioButton,
				this.CoordRadioButton,
				this.GlCoordRadioButton
			});
			this.paramsSaver.RegisterControl(this.createTerrainCheckbox);
			this.paramsSaver.RegisterControl(this.createBottomCheckbox);
			this.paramsSaver.RegisterControl(new RadioButton[]
			{
				this.size4RadioButton,
				this.size1RadioButton
			});
			this.paramsSaver.PostLoadParams += this.OnLoadParams;
			this.paramsSaver.RegisterControl(this.cmbBoxFilters, true);
			this.paramsSaver.SaveParams += this.OnSaveParams;
			base.Controls.Add(this.mapDialogControl);
			this.resultParams.Changed += this.OnParamDataChanged;
		}

		// Token: 0x1700037E RID: 894
		// (get) Token: 0x0600118F RID: 4495 RVA: 0x00082984 File Offset: 0x00081984
		public OpenMapDialog.Params ResultParams
		{
			get
			{
				return this.resultParams;
			}
		}

		// Token: 0x04000C89 RID: 3209
		private const int defaultRate = 1;

		// Token: 0x04000C8A RID: 3210
		private static readonly Tools.Geometry.Point defaultVisibleAreaPosition = new Tools.Geometry.Point(0, 0);

		// Token: 0x04000C8B RID: 3211
		private readonly List<ItemList.IItemFilter> filters = new List<ItemList.IItemFilter>();

		// Token: 0x04000C8C RID: 3212
		private readonly FolderItemFilters folderItemFilters = new FolderItemFilters(EditorEnvironment.EditorFolder + "Filters/ContinentFilters.xml", EditorEnvironment.EditorFormsFolder);

		// Token: 0x04000C8D RID: 3213
		private readonly OpenMapDialog.ContinentItemListSource continentItemListSource;

		// Token: 0x04000C8E RID: 3214
		private readonly FormParamsSaver paramsSaver;

		// Token: 0x04000C8F RID: 3215
		private readonly OpenMapDialog.Params resultParams = new OpenMapDialog.Params();

		// Token: 0x02000167 RID: 359
		public class Params
		{
			// Token: 0x14000069 RID: 105
			// (add) Token: 0x06001193 RID: 4499 RVA: 0x00083177 File Offset: 0x00082177
			// (remove) Token: 0x06001194 RID: 4500 RVA: 0x00083190 File Offset: 0x00082190
			public event OpenMapDialog.Params.ChangedEvent Changed;

			// Token: 0x06001195 RID: 4501 RVA: 0x000831AC File Offset: 0x000821AC
			public Params()
			{
				this.cameraPosition = Constants.PatchesCenter(new Rect(0, 0, this.mapSize, this.mapSize));
				OpenMapDialog.Params.maxPatch = new Tools.Geometry.Point(Constants.WorldBounds.Width - this.mapSize + 1, Constants.WorldBounds.Height - this.mapSize + 1);
			}

			// Token: 0x1700037F RID: 895
			// (get) Token: 0x06001196 RID: 4502 RVA: 0x00083225 File Offset: 0x00082225
			// (set) Token: 0x06001197 RID: 4503 RVA: 0x0008322D File Offset: 0x0008222D
			public string Name
			{
				get
				{
					return this.name;
				}
				set
				{
					if (this.name != value)
					{
						this.name = value;
						if (this.Changed != null)
						{
							this.Changed(OpenMapDialog.Params.Fields.Name);
						}
						this.Type = Constants.GetContinentType(this.name);
					}
				}
			}

			// Token: 0x17000380 RID: 896
			// (get) Token: 0x06001198 RID: 4504 RVA: 0x00083269 File Offset: 0x00082269
			public string ContinentName
			{
				get
				{
					return Str.CutFileName(this.name, false);
				}
			}

			// Token: 0x17000381 RID: 897
			// (get) Token: 0x06001199 RID: 4505 RVA: 0x00083277 File Offset: 0x00082277
			// (set) Token: 0x0600119A RID: 4506 RVA: 0x0008327F File Offset: 0x0008227F
			public ContinentType Type
			{
				get
				{
					return this.type;
				}
				set
				{
					if (this.type != value)
					{
						this.type = value;
						if (this.Changed != null)
						{
							this.Changed(OpenMapDialog.Params.Fields.Type);
						}
					}
				}
			}

			// Token: 0x17000382 RID: 898
			// (get) Token: 0x0600119B RID: 4507 RVA: 0x000832A5 File Offset: 0x000822A5
			// (set) Token: 0x0600119C RID: 4508 RVA: 0x000832AD File Offset: 0x000822AD
			public bool CreateTerrain
			{
				get
				{
					return this.createTerrain;
				}
				set
				{
					if (this.createTerrain != value)
					{
						this.createTerrain = value;
						if (this.Changed != null)
						{
							this.Changed(OpenMapDialog.Params.Fields.Terrain);
						}
					}
				}
			}

			// Token: 0x17000383 RID: 899
			// (get) Token: 0x0600119D RID: 4509 RVA: 0x000832D3 File Offset: 0x000822D3
			// (set) Token: 0x0600119E RID: 4510 RVA: 0x000832DB File Offset: 0x000822DB
			public bool CreateBottom
			{
				get
				{
					return this.createBottom;
				}
				set
				{
					if (this.createBottom != value)
					{
						this.createBottom = value;
						if (this.Changed != null)
						{
							this.Changed(OpenMapDialog.Params.Fields.Terrain);
						}
					}
				}
			}

			// Token: 0x17000384 RID: 900
			// (get) Token: 0x0600119F RID: 4511 RVA: 0x00083301 File Offset: 0x00082301
			// (set) Token: 0x060011A0 RID: 4512 RVA: 0x00083309 File Offset: 0x00082309
			public OpenMapDialog.Params.Units Unit
			{
				get
				{
					return this.unit;
				}
				set
				{
					if (this.unit != value)
					{
						this.unit = value;
						if (this.Changed != null)
						{
							this.Changed(OpenMapDialog.Params.Fields.Unit);
						}
					}
				}
			}

			// Token: 0x17000385 RID: 901
			// (get) Token: 0x060011A1 RID: 4513 RVA: 0x0008332F File Offset: 0x0008232F
			// (set) Token: 0x060011A2 RID: 4514 RVA: 0x00083337 File Offset: 0x00082337
			public int MapSize
			{
				get
				{
					return this.mapSize;
				}
				set
				{
					if (this.mapSize != value)
					{
						this.mapSize = value;
						if (this.Changed != null)
						{
							this.Changed(OpenMapDialog.Params.Fields.Size);
						}
					}
				}
			}

			// Token: 0x060011A3 RID: 4515 RVA: 0x00083360 File Offset: 0x00082360
			public bool IsValid()
			{
				if (this.cameraPosition.X >= 0.0 && this.cameraPosition.Y >= 0.0)
				{
					Tools.Geometry.Point patch = this.GetPatch();
					return patch.X <= OpenMapDialog.Params.maxPatch.X && patch.Y <= OpenMapDialog.Params.maxPatch.Y;
				}
				return false;
			}

			// Token: 0x060011A4 RID: 4516 RVA: 0x000833D0 File Offset: 0x000823D0
			public Tools.Geometry.Point GetPatch()
			{
				Tools.Geometry.Point patch = new Tools.Geometry.Point((int)(this.cameraPosition.X / (double)Constants.PatchSize), (int)(this.cameraPosition.Y / (double)Constants.PatchSize));
				Tools.Geometry.Point pathcCenter = new Tools.Geometry.Point((int)(((double)patch.X + 0.5) * (double)Constants.PatchSize), (int)(((double)patch.Y + 0.5) * (double)Constants.PatchSize));
				patch.X -= ((this.cameraPosition.X < (double)pathcCenter.X) ? (this.mapSize / 2) : ((this.mapSize - 1) / 2));
				patch.Y -= ((this.cameraPosition.Y < (double)pathcCenter.Y) ? (this.mapSize / 2) : ((this.mapSize - 1) / 2));
				patch.X = ((patch.X < 0) ? 0 : patch.X);
				patch.Y = ((patch.Y < 0) ? 0 : patch.Y);
				return patch;
			}

			// Token: 0x060011A5 RID: 4517 RVA: 0x000834EC File Offset: 0x000824EC
			public void SetPatch(Tools.Geometry.Point value)
			{
				Tools.Geometry.Point patch = this.GetPatch();
				if (value.X != patch.X || value.Y != patch.Y)
				{
					this.CameraPosition = Constants.PatchesCenter(new Rect(value, this.mapSize, this.mapSize));
				}
			}

			// Token: 0x17000386 RID: 902
			// (get) Token: 0x060011A6 RID: 4518 RVA: 0x0008353D File Offset: 0x0008253D
			// (set) Token: 0x060011A7 RID: 4519 RVA: 0x00083545 File Offset: 0x00082545
			public Position CameraPosition
			{
				get
				{
					return this.cameraPosition;
				}
				set
				{
					if (!this.cameraPosition.Equals(value))
					{
						this.cameraPosition = value;
						if (this.Changed != null)
						{
							this.Changed(OpenMapDialog.Params.Fields.Position);
						}
					}
				}
			}

			// Token: 0x04000CA9 RID: 3241
			private string name = string.Empty;

			// Token: 0x04000CAA RID: 3242
			private ContinentType type;

			// Token: 0x04000CAB RID: 3243
			private OpenMapDialog.Params.Units unit;

			// Token: 0x04000CAC RID: 3244
			private int mapSize = 4;

			// Token: 0x04000CAD RID: 3245
			private Position cameraPosition;

			// Token: 0x04000CAE RID: 3246
			private bool createTerrain;

			// Token: 0x04000CAF RID: 3247
			private bool createBottom;

			// Token: 0x04000CB0 RID: 3248
			private static Tools.Geometry.Point maxPatch;

			// Token: 0x02000168 RID: 360
			// (Invoke) Token: 0x060011A9 RID: 4521
			public delegate void ChangedEvent(OpenMapDialog.Params.Fields field);

			// Token: 0x02000169 RID: 361
			public enum Units
			{
				// Token: 0x04000CB3 RID: 3251
				Patch,
				// Token: 0x04000CB4 RID: 3252
				Coordinate,
				// Token: 0x04000CB5 RID: 3253
				GlobalCoordinate
			}

			// Token: 0x0200016A RID: 362
			public enum Fields
			{
				// Token: 0x04000CB7 RID: 3255
				Type,
				// Token: 0x04000CB8 RID: 3256
				Name,
				// Token: 0x04000CB9 RID: 3257
				Position,
				// Token: 0x04000CBA RID: 3258
				Terrain,
				// Token: 0x04000CBB RID: 3259
				Unit,
				// Token: 0x04000CBC RID: 3260
				Size
			}
		}

		// Token: 0x0200016B RID: 363
		internal class ContinentItemListSource : ItemList.IItemSource
		{
			// Token: 0x060011AC RID: 4524 RVA: 0x0008357B File Offset: 0x0008257B
			public ContinentItemListSource()
			{
				Constants.GetMapNameList(ref this.items);
			}

			// Token: 0x17000387 RID: 903
			// (get) Token: 0x060011AD RID: 4525 RVA: 0x0008359A File Offset: 0x0008259A
			public IEnumerable<string> Items
			{
				get
				{
					return this.items;
				}
			}

			// Token: 0x04000CBD RID: 3261
			private readonly List<string> items = new List<string>();
		}
	}
}
