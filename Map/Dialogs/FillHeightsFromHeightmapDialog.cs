using System;
using System.ComponentModel;
using System.IO;
using System.Windows.Forms;
using Tools.Geometry;
using Tools.WindowParams;

namespace MapEditor.Map.Dialogs
{
	// Token: 0x02000181 RID: 385
	public partial class FillHeightsFromHeightmapDialog : Form
	{
		// Token: 0x06001254 RID: 4692 RVA: 0x00085B90 File Offset: 0x00084B90
		public FillHeightsFromHeightmapDialog(bool _mapLoaded)
		{
			this.mapLoaded = _mapLoaded;
			this.InitializeComponent();
			this.paramsSaver = new FormParamsSaver(this, EditorEnvironment.EditorFormsFolder + "FillHeightsFromHeightmapDialog.xml", false);
			this.paramsSaver.AutoregisterControls = false;
			this.paramsSaver.RegisterControl(new RadioButton[]
			{
				this.ApplyToActiveMapRadioButton,
				this.ApplyToSeveralPatchesRadioButton
			});
			this.paramsSaver.RegisterControl(this.HeightmapTextbox);
			this.paramsSaver.RegisterControl(this.WhiteHeightTextBox);
			this.paramsSaver.RegisterControl(this.BlackHeightTextBox);
			this.paramsSaver.RegisterControl(this.StartXYTextBox);
			this.paramsSaver.RegisterControl(this.FinishXYTextBox);
			this.paramsSaver.PostLoadParams += this.OnLoadParams;
			this.paramsSaver.SaveParams += this.OnSaveParams;
		}

		// Token: 0x170003B4 RID: 948
		// (get) Token: 0x06001255 RID: 4693 RVA: 0x00085CD8 File Offset: 0x00084CD8
		public string HeighmapFileName
		{
			get
			{
				return this.heighmapFileName;
			}
		}

		// Token: 0x170003B5 RID: 949
		// (get) Token: 0x06001256 RID: 4694 RVA: 0x00085CE0 File Offset: 0x00084CE0
		public bool ActiveMap
		{
			get
			{
				return this.activeMap && this.mapLoaded;
			}
		}

		// Token: 0x170003B6 RID: 950
		// (get) Token: 0x06001257 RID: 4695 RVA: 0x00085CF2 File Offset: 0x00084CF2
		public double WhiteHeight
		{
			get
			{
				return this.whiteHeight;
			}
		}

		// Token: 0x170003B7 RID: 951
		// (get) Token: 0x06001258 RID: 4696 RVA: 0x00085CFA File Offset: 0x00084CFA
		public double BlackHeight
		{
			get
			{
				return this.blackHeight;
			}
		}

		// Token: 0x170003B8 RID: 952
		// (get) Token: 0x06001259 RID: 4697 RVA: 0x00085D02 File Offset: 0x00084D02
		public Rect PatchBounds
		{
			get
			{
				return new Rect(this.startX, this.startY, this.finishX + 1, this.finishY + 1);
			}
		}

		// Token: 0x170003B9 RID: 953
		// (get) Token: 0x0600125A RID: 4698 RVA: 0x00085D25 File Offset: 0x00084D25
		public int TerrainIndex
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x0600125B RID: 4699 RVA: 0x00085D28 File Offset: 0x00084D28
		private void OnLoadParams(FormParams formParams)
		{
			this.created = true;
			this.activeMap = FormParamsSaver.IntToBool(this.paramsSaver.FormParams.GetInt(0));
			this.UpdateEditBoxes(true);
			this.UpdateRadioButtons();
			this.UpdateOKButton();
		}

		// Token: 0x0600125C RID: 4700 RVA: 0x00085D60 File Offset: 0x00084D60
		private void OnSaveParams(FormParams formParams)
		{
			this.paramsSaver.FormParams.SetInt(0, FormParamsSaver.BoolToInt(this.activeMap));
			this.UpdateEditBoxes(false);
		}

		// Token: 0x0600125D RID: 4701 RVA: 0x00085D88 File Offset: 0x00084D88
		private void UpdateEditBoxes(bool fixTextBoxes)
		{
			if (this.created)
			{
				this.created = false;
				this.heighmapFileName = this.HeightmapTextbox.Text;
				if (!double.TryParse(this.WhiteHeightTextBox.Text, out this.whiteHeight))
				{
					this.whiteHeight = FillHeightsFromHeightmapDialog.defaultWhiteHeight;
					if (fixTextBoxes)
					{
						this.WhiteHeightTextBox.Text = this.whiteHeight.ToString();
					}
				}
				if (!double.TryParse(this.BlackHeightTextBox.Text, out this.blackHeight))
				{
					this.blackHeight = FillHeightsFromHeightmapDialog.defaultBlackHeight;
					if (fixTextBoxes)
					{
						this.BlackHeightTextBox.Text = this.blackHeight.ToString();
					}
				}
				string[] start = this.StartXYTextBox.Text.Split(FillHeightsFromHeightmapDialog.delimeters);
				if (start.Length < 2 || !int.TryParse(start[0], out this.startX) || !int.TryParse(start[1], out this.startY))
				{
					this.startX = FillHeightsFromHeightmapDialog.defaultStartX;
					this.startY = FillHeightsFromHeightmapDialog.defaultStartY;
					if (fixTextBoxes)
					{
						this.StartXYTextBox.Text = string.Format("{0}, {1}", this.startX, this.startY);
					}
				}
				string[] finish = this.FinishXYTextBox.Text.Split(FillHeightsFromHeightmapDialog.delimeters);
				if (finish.Length < 2 || !int.TryParse(finish[0], out this.finishX) || !int.TryParse(finish[1], out this.finishY))
				{
					this.finishX = FillHeightsFromHeightmapDialog.defaultFinishX;
					this.finishY = FillHeightsFromHeightmapDialog.defaultFinishY;
					if (fixTextBoxes)
					{
						this.FinishXYTextBox.Text = string.Format("{0}, {1}", this.finishX, this.finishY);
					}
				}
				this.created = true;
			}
		}

		// Token: 0x0600125E RID: 4702 RVA: 0x00085F37 File Offset: 0x00084F37
		private void HeightmapTextbox_TextChanged(object sender, EventArgs e)
		{
			if (this.created)
			{
				this.EditBoxTimer.Start();
			}
		}

		// Token: 0x0600125F RID: 4703 RVA: 0x00085F4C File Offset: 0x00084F4C
		private void WhiteHeightTextBox_TextChanged(object sender, EventArgs e)
		{
			if (this.created)
			{
				this.EditBoxTimer.Start();
			}
		}

		// Token: 0x06001260 RID: 4704 RVA: 0x00085F61 File Offset: 0x00084F61
		private void BlackHeightTextBox_TextChanged(object sender, EventArgs e)
		{
			if (this.created)
			{
				this.EditBoxTimer.Start();
			}
		}

		// Token: 0x06001261 RID: 4705 RVA: 0x00085F76 File Offset: 0x00084F76
		private void StartXYTextBox_TextChanged(object sender, EventArgs e)
		{
			if (this.created)
			{
				this.EditBoxTimer.Start();
			}
		}

		// Token: 0x06001262 RID: 4706 RVA: 0x00085F8B File Offset: 0x00084F8B
		private void FinishXYTextBox_TextChanged(object sender, EventArgs e)
		{
			if (this.created)
			{
				this.EditBoxTimer.Start();
			}
		}

		// Token: 0x06001263 RID: 4707 RVA: 0x00085FA0 File Offset: 0x00084FA0
		private void EditBoxTimer_Tick(object sender, EventArgs e)
		{
			if (this.created)
			{
				this.EditBoxTimer.Stop();
				this.UpdateEditBoxes(true);
			}
		}

		// Token: 0x06001264 RID: 4708 RVA: 0x00085FBC File Offset: 0x00084FBC
		private void UpdateRadioButtons()
		{
			if (this.created)
			{
				this.created = false;
				this.ApplyToActiveMapRadioButton.Enabled = this.mapLoaded;
				this.ApplyToActiveMapRadioButton.Checked = (this.activeMap && this.mapLoaded);
				this.ApplyToSeveralPatchesRadioButton.Checked = (!this.activeMap || !this.mapLoaded);
				this.StartXYTextBox.Enabled = (!this.activeMap || !this.mapLoaded);
				this.FinishXYTextBox.Enabled = (!this.activeMap || !this.mapLoaded);
				this.created = true;
			}
		}

		// Token: 0x06001265 RID: 4709 RVA: 0x0008606C File Offset: 0x0008506C
		private void UpdateOKButton()
		{
			if (this.created)
			{
				this.created = false;
				if (File.Exists(this.heighmapFileName) && this.whiteHeight > this.blackHeight)
				{
					if (!this.mapLoaded || !this.activeMap)
					{
						if (this.startX <= this.finishX && this.startY < this.finishY && this.startX >= 0 && this.startY >= 0 && this.finishX < 100 && this.finishY < 100)
						{
							this.OKbutton.Enabled = true;
						}
						else
						{
							this.OKbutton.Enabled = false;
						}
					}
					else
					{
						this.OKbutton.Enabled = true;
					}
				}
				else
				{
					this.OKbutton.Enabled = false;
				}
				this.created = true;
			}
		}

		// Token: 0x06001266 RID: 4710 RVA: 0x00086138 File Offset: 0x00085138
		private void HeightmapBrowseButton_Click(object sender, EventArgs e)
		{
			if (this.created)
			{
				OpenFileDialog openDialog = new OpenFileDialog();
				openDialog.Filter = ".tga files|*.tga";
				openDialog.InitialDirectory = this.paramsSaver.FormParams.GetString(0);
				openDialog.RestoreDirectory = true;
				openDialog.Multiselect = false;
				if (openDialog.ShowDialog() == DialogResult.OK)
				{
					FileInfo fileInfo = new FileInfo(openDialog.FileName);
					this.paramsSaver.FormParams.SetString(0, fileInfo.Directory.ToString());
					this.heighmapFileName = openDialog.FileName;
					this.created = false;
					this.HeightmapTextbox.Text = openDialog.FileName;
					this.created = true;
				}
				this.UpdateOKButton();
			}
		}

		// Token: 0x06001267 RID: 4711 RVA: 0x000861E8 File Offset: 0x000851E8
		private void ApplyToActiveMapRadioButton_CheckedChanged(object sender, EventArgs e)
		{
			if (this.created)
			{
				this.activeMap = true;
				this.UpdateRadioButtons();
				this.UpdateOKButton();
			}
		}

		// Token: 0x06001268 RID: 4712 RVA: 0x00086205 File Offset: 0x00085205
		private void ApplyToSeveralPatchesRadioButton_CheckedChanged(object sender, EventArgs e)
		{
			if (this.created)
			{
				this.activeMap = false;
				this.UpdateRadioButtons();
				this.UpdateOKButton();
			}
		}

		// Token: 0x04000D19 RID: 3353
		private static readonly bool defaultActiveMap = true;

		// Token: 0x04000D1A RID: 3354
		private static readonly double defaultWhiteHeight = 128.0;

		// Token: 0x04000D1B RID: 3355
		private static readonly double defaultBlackHeight = 0.0;

		// Token: 0x04000D1C RID: 3356
		private static readonly int defaultStartX = 0;

		// Token: 0x04000D1D RID: 3357
		private static readonly int defaultStartY = 0;

		// Token: 0x04000D1E RID: 3358
		private static readonly int defaultFinishX = 3;

		// Token: 0x04000D1F RID: 3359
		private static readonly int defaultFinishY = 3;

		// Token: 0x04000D20 RID: 3360
		private static readonly char[] delimeters = new char[]
		{
			' ',
			',',
			';',
			':'
		};

		// Token: 0x04000D21 RID: 3361
		private readonly FormParamsSaver paramsSaver;

		// Token: 0x04000D22 RID: 3362
		private readonly bool mapLoaded;

		// Token: 0x04000D23 RID: 3363
		private bool activeMap = FillHeightsFromHeightmapDialog.defaultActiveMap;

		// Token: 0x04000D24 RID: 3364
		private string heighmapFileName = string.Empty;

		// Token: 0x04000D25 RID: 3365
		private double whiteHeight = FillHeightsFromHeightmapDialog.defaultWhiteHeight;

		// Token: 0x04000D26 RID: 3366
		private double blackHeight = FillHeightsFromHeightmapDialog.defaultBlackHeight;

		// Token: 0x04000D27 RID: 3367
		private int startX = FillHeightsFromHeightmapDialog.defaultStartX;

		// Token: 0x04000D28 RID: 3368
		private int startY = FillHeightsFromHeightmapDialog.defaultStartY;

		// Token: 0x04000D29 RID: 3369
		private int finishX = FillHeightsFromHeightmapDialog.defaultFinishX;

		// Token: 0x04000D2A RID: 3370
		private int finishY = FillHeightsFromHeightmapDialog.defaultFinishY;

		// Token: 0x04000D2B RID: 3371
		private bool created;
	}
}
