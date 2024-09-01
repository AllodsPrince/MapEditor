using System;
using System.ComponentModel;
using System.Windows.Forms;
using Tools.MapObjects;
using Tools.WindowParams;

namespace MapEditor.Map.Dialogs
{
	// Token: 0x020001A9 RID: 425
	public partial class MoveCameraForm : Form
	{
		// Token: 0x06001486 RID: 5254 RVA: 0x00094508 File Offset: 0x00093508
		private void OnPostLoadParams(FormParams formParams)
		{
			this.TypeRadioButton0.Visible = this.enableRelativeCoords;
			this.TypeRadioButton0.Enabled = this.enableRelativeCoords;
			this.TypeRadioButton1.Visible = this.enableRelativeCoords;
			this.TypeRadioButton1.Enabled = this.enableRelativeCoords;
			this.CoordsTextBox.SelectAll();
		}

		// Token: 0x06001487 RID: 5255 RVA: 0x00094564 File Offset: 0x00093564
		private void MoveCameraForm_FormClosing(object sender, FormClosingEventArgs e)
		{
			string text = this.CoordsTextBox.Text.Replace('.', ',');
			string[] coords = text.Split(this.separator, StringSplitOptions.RemoveEmptyEntries);
			double x;
			double y;
			if (coords.Length > 1 && double.TryParse(coords[0], out x) && double.TryParse(coords[1], out y))
			{
				this.coordsValid = true;
				double z;
				if (coords.Length > 2 && double.TryParse(coords[2], out z))
				{
					this.onlyXYCoordsValid = false;
					this.cameraPosition = new Position(x, y, z);
				}
				else
				{
					this.onlyXYCoordsValid = true;
					this.cameraPosition = new Position(x, y, Position.Empty.Z);
				}
			}
			else
			{
				this.coordsValid = false;
				this.onlyXYCoordsValid = false;
				this.cameraPosition = Position.Empty;
			}
			this.coordsRelative = this.TypeRadioButton1.Checked;
		}

		// Token: 0x06001488 RID: 5256 RVA: 0x00094634 File Offset: 0x00093634
		public MoveCameraForm(bool _enableRelativeCoords)
		{
			this.InitializeComponent();
			this.enableRelativeCoords = _enableRelativeCoords;
			this.paramsSaver = new FormParamsSaver(this, EditorEnvironment.EditorFormsFolder + "MoveCameraForm.xml", false);
			this.paramsSaver.AutoregisterControls = false;
			this.paramsSaver.RegisterControl(new RadioButton[]
			{
				this.TypeRadioButton0,
				this.TypeRadioButton1
			});
			this.paramsSaver.RegisterControl(this.CoordsTextBox);
			this.paramsSaver.PostLoadParams += this.OnPostLoadParams;
		}

		// Token: 0x1700040B RID: 1035
		// (get) Token: 0x06001489 RID: 5257 RVA: 0x000946F3 File Offset: 0x000936F3
		public Position CameraPosition
		{
			get
			{
				return this.cameraPosition;
			}
		}

		// Token: 0x1700040C RID: 1036
		// (get) Token: 0x0600148A RID: 5258 RVA: 0x000946FB File Offset: 0x000936FB
		public bool CoordsValid
		{
			get
			{
				return this.coordsValid;
			}
		}

		// Token: 0x1700040D RID: 1037
		// (get) Token: 0x0600148B RID: 5259 RVA: 0x00094703 File Offset: 0x00093703
		public bool CoordsRelative
		{
			get
			{
				return this.coordsRelative;
			}
		}

		// Token: 0x1700040E RID: 1038
		// (get) Token: 0x0600148C RID: 5260 RVA: 0x0009470B File Offset: 0x0009370B
		public bool OnlyXYCoordsValid
		{
			get
			{
				return this.onlyXYCoordsValid;
			}
		}

		// Token: 0x04000E67 RID: 3687
		private readonly char[] separator = new char[]
		{
			' ',
			';'
		};

		// Token: 0x04000E68 RID: 3688
		private readonly FormParamsSaver paramsSaver;

		// Token: 0x04000E69 RID: 3689
		private readonly bool enableRelativeCoords = true;

		// Token: 0x04000E6A RID: 3690
		private bool coordsValid;

		// Token: 0x04000E6B RID: 3691
		private bool onlyXYCoordsValid;

		// Token: 0x04000E6C RID: 3692
		private bool coordsRelative;

		// Token: 0x04000E6D RID: 3693
		private Position cameraPosition = Position.Empty;
	}
}
