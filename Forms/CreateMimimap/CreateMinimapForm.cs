using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using MapEditor.Forms.Base;
using MapEditor.Map;
using Tools.EditorImage;
using Tools.Geometry;
using Tools.WindowParams;

namespace MapEditor.Forms.CreateMimimap
{
	// Token: 0x0200007D RID: 125
	public partial class CreateMinimapForm : BaseForm
	{
		// Token: 0x0600060A RID: 1546 RVA: 0x0003392C File Offset: 0x0003292C
		private void Create(string mapFolder, string filePath)
		{
			Cursor.Current = Cursors.WaitCursor;
			int dataPathLen = EditorEnvironment.DataFolder.Length;
			Dictionary<string, Tools.Geometry.Point> minimapPathes = new Dictionary<string, Tools.Geometry.Point>();
			Tools.Geometry.Point minPoint = new Tools.Geometry.Point(1000, 1000);
			Tools.Geometry.Point maxPoint = new Tools.Geometry.Point(0, 0);
			foreach (string mapRegion in Directory.GetFiles(mapFolder, "*_MapRegion.xdb", SearchOption.AllDirectories))
			{
				Match match = CreateMinimapForm.patchRegex.Match(mapRegion);
				if (match != null && match.Success && match.Groups.Count == 5)
				{
					Tools.Geometry.Point patch = default(Tools.Geometry.Point);
					patch.X = int.Parse(match.Groups[1].Value) + int.Parse(match.Groups[3].Value);
					patch.Y = int.Parse(match.Groups[2].Value) + int.Parse(match.Groups[4].Value);
					string pathFromData = mapRegion.Remove(0, dataPathLen);
					string minimapPath = pathFromData.Replace("MapRegion.xdb", "minimap.(Texture).xdb");
					if (!string.IsNullOrEmpty(minimapPath) && !minimapPathes.ContainsKey(minimapPath))
					{
						minimapPathes.Add(minimapPath, patch);
						for (int index = 0; index < 2; index++)
						{
							if (minPoint[index] > patch[index])
							{
								minPoint[index] = patch[index];
							}
							if (maxPoint[index] < patch[index])
							{
								maxPoint[index] = patch[index];
							}
						}
					}
				}
			}
			int width = (maxPoint.X - minPoint.X + 1) * 128;
			int height = (maxPoint.Y - minPoint.Y + 1) * 128;
			if (width > 0 && height > 0)
			{
				Bitmap minimap = new Bitmap(width, height, PixelFormat.Format24bppRgb);
				Graphics gr = Graphics.FromImage(minimap);
				foreach (KeyValuePair<string, Tools.Geometry.Point> pair in minimapPathes)
				{
					Tools.Geometry.Point size = new Tools.Geometry.Point(128, 128);
					Bitmap patchMap;
					if (EditorImage.LoadTextureToARGBBitmap(pair.Key, size, size, out patchMap) && patchMap != null)
					{
						Tools.Geometry.Point patchFromMin = pair.Value - minPoint;
						System.Drawing.Point point = new System.Drawing.Point(patchFromMin.X * 128, height - (patchFromMin.Y + 1) * 128);
						gr.DrawImage(patchMap, point);
						patchMap.Dispose();
					}
				}
				try
				{
					minimap.Save(filePath, ImageFormat.Jpeg);
					goto IL_2FA;
				}
				catch (Exception e)
				{
					Console.WriteLine(e);
					Cursor.Current = Cursors.Default;
					MessageBox.Show(this, string.Format("Can't create file {0}.", filePath), this.Text, MessageBoxButtons.OK, MessageBoxIcon.Hand);
					return;
				}
				goto IL_2DA;
				IL_2FA:
				Cursor.Current = Cursors.Default;
				return;
			}
			IL_2DA:
			Cursor.Current = Cursors.Default;
			MessageBox.Show(this, "Can't create minimap for this continent.", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Hand);
		}

		// Token: 0x0600060B RID: 1547 RVA: 0x00033C5C File Offset: 0x00032C5C
		private void OnCreateClick(object sender, EventArgs e)
		{
			if (string.IsNullOrEmpty(this.continentComboBox.Text) || string.IsNullOrEmpty(this.directoryTextBox.Text) || string.IsNullOrEmpty(this.fileNameTextBox.Text))
			{
				MessageBox.Show(this, "Enter continent name file path and name.", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Hand);
				return;
			}
			string mapFolder = EditorEnvironment.DataFolder + this.continentComboBox.Text.Trim();
			if (!Directory.Exists(mapFolder))
			{
				MessageBox.Show(this, "Invalid continent.", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Hand);
				return;
			}
			if (!Directory.Exists(this.directoryTextBox.Text))
			{
				if (MessageBox.Show(this, "Choosen directory doesn't exists. Create?", this.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
				{
					return;
				}
				try
				{
					Directory.CreateDirectory(this.directoryTextBox.Text);
				}
				catch (Exception)
				{
					MessageBox.Show(this, "Can't create directoty.", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Hand);
					return;
				}
			}
			string filePath = this.directoryTextBox.Text.Trim() + "\\" + this.fileNameTextBox.Text.Trim() + ".jpg";
			if (!File.Exists(filePath) || MessageBox.Show(this, "File with selected name already exist. Override?", this.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
			{
				this.Create(mapFolder, filePath);
				return;
			}
		}

		// Token: 0x0600060C RID: 1548 RVA: 0x00033DAC File Offset: 0x00032DAC
		private void OnLoadParams(FormParams formParams)
		{
			List<string> continents = new List<string>();
			Constants.GetContinentNameList(ref continents);
			foreach (string continent in continents)
			{
				if (!continent.Contains("Test"))
				{
					this.continentComboBox.Items.Add(continent);
				}
			}
		}

		// Token: 0x0600060D RID: 1549 RVA: 0x00033E20 File Offset: 0x00032E20
		private void OnBrowseFolderClick(object sender, EventArgs e)
		{
			FolderBrowserDialog dialoag = new FolderBrowserDialog();
			string selectedFolder = (this.directoryTextBox.Text ?? string.Empty).Trim();
			if (!string.IsNullOrEmpty(selectedFolder) && Directory.Exists(selectedFolder))
			{
				dialoag.SelectedPath = Path.GetFullPath(selectedFolder);
			}
			dialoag.ShowNewFolderButton = true;
			if (dialoag.ShowDialog(this) == DialogResult.OK)
			{
				this.directoryTextBox.Text = dialoag.SelectedPath;
			}
		}

		// Token: 0x0600060E RID: 1550 RVA: 0x00033E8C File Offset: 0x00032E8C
		public CreateMinimapForm(MainForm.Context context) : base(EditorEnvironment.EditorFormsFolder + "CreateMinimapForm.xml", context)
		{
			this.InitializeComponent();
			base.ParamsSaver.AutoregisterControls = true;
			this.createButton.Click += this.OnCreateClick;
			base.ParamsSaver.LoadParams += this.OnLoadParams;
			this.browseButton.Click += this.OnBrowseFolderClick;
		}

		// Token: 0x04000497 RID: 1175
		private const string terrainSuff = "MapRegion.xdb";

		// Token: 0x04000498 RID: 1176
		private const string minimapSuff = "minimap.(Texture).xdb";

		// Token: 0x04000499 RID: 1177
		private const int patchImageSize = 128;

		// Token: 0x040004A4 RID: 1188
		private static readonly Regex patchRegex = new Regex("\\\\([0-9]{3})_([0-9]{3})\\\\([0-9])_([0-9])_MapRegion.xdb$");
	}
}
