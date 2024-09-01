using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;
using MapEditor.Forms.Base;
using Tools.DbCommon;
using Tools.DBGameObjects;
using Tools.Excel;

namespace MapEditor.Forms.ImportCues
{
	// Token: 0x0200007A RID: 122
	public partial class ImportCuesForm : BaseForm
	{
		// Token: 0x060005F3 RID: 1523 RVA: 0x000329A4 File Offset: 0x000319A4
		private void OnBrowseClick(object sender, EventArgs e)
		{
			OpenFileDialog fileDialog = new OpenFileDialog();
			fileDialog.Filter = "Excel files(*.xlsx)|*.xlsx|Excel files(*.xls)|*.xls";
			fileDialog.Multiselect = false;
			fileDialog.RestoreDirectory = true;
			fileDialog.FilterIndex = 0;
			string filePath = this.FilePathTextBox.Text.Trim();
			if (!string.IsNullOrEmpty(filePath) && File.Exists(filePath))
			{
				FileInfo file = new FileInfo(filePath);
				if (file.Extension == "xls")
				{
					fileDialog.FilterIndex = 1;
				}
				fileDialog.InitialDirectory = file.Directory.FullName;
			}
			DialogResult result = fileDialog.ShowDialog();
			if (result == DialogResult.OK)
			{
				this.FilePathTextBox.Text = fileDialog.FileName;
				this.ResultTextBox.Clear();
			}
		}

		// Token: 0x060005F4 RID: 1524 RVA: 0x00032A50 File Offset: 0x00031A50
		private static bool WriteFileText(string _filePath, string text, bool useTags)
		{
			string filePath = _filePath;
			DbCommonMethods.CheckFileRef(ref filePath, false);
			if (!string.IsNullOrEmpty(filePath) && File.Exists(filePath))
			{
				if (useTags)
				{
					DBMethods.AddTags(ref text);
				}
				File.WriteAllText(filePath, text, Encoding.Unicode);
				return true;
			}
			return false;
		}

		// Token: 0x060005F5 RID: 1525 RVA: 0x00032A94 File Offset: 0x00031A94
		private static void ImportFile(object[,] result, int rowIndex, int pathIndex, int fileTextIndex, bool useTags, ref int success, List<string> invalidFiles)
		{
			rowIndex++;
			pathIndex++;
			fileTextIndex++;
			string path = result.GetValue(rowIndex, pathIndex) as string;
			if (!string.IsNullOrEmpty(path))
			{
				if (ImportCuesForm.WriteFileText(path, result.GetValue(rowIndex, fileTextIndex) as string, useTags))
				{
					success++;
					return;
				}
				invalidFiles.Add(path);
			}
		}

		// Token: 0x060005F6 RID: 1526 RVA: 0x00032AF0 File Offset: 0x00031AF0
		private static bool Import(object[,] result, out int success, out List<string> invalidFiles)
		{
			invalidFiles = new List<string>();
			success = 0;
			if (result != null)
			{
				int count = result.GetLength(0);
				for (int index = 0; index < count; index++)
				{
					ImportCuesForm.ImportFile(result, index, 2, 3, false, ref success, invalidFiles);
					ImportCuesForm.ImportFile(result, index, 4, 5, true, ref success, invalidFiles);
				}
				return success > 0;
			}
			return false;
		}

		// Token: 0x060005F7 RID: 1527 RVA: 0x00032B40 File Offset: 0x00031B40
		private void OnImportClick(object sender, EventArgs e)
		{
			this.ResultTextBox.Clear();
			string filePath = this.FilePathTextBox.Text.Trim();
			if (!File.Exists(filePath))
			{
				this.ResultTextBox.AppendText("File not founded!");
				return;
			}
			if (!filePath.EndsWith("xlsx") && !filePath.EndsWith("xls"))
			{
				this.ResultTextBox.AppendText("Invalid file extension!");
				return;
			}
			this.Cursor = Cursors.WaitCursor;
			object[,] result;
			ExportImport.FromExcel(filePath, 6, true, out result);
			int success;
			List<string> invalidFiles;
			if (ImportCuesForm.Import(result, out success, out invalidFiles))
			{
				this.ResultTextBox.AppendText(string.Format("{0} text files imported successfully!\n", success));
				if (invalidFiles.Count <= 0)
				{
					goto IL_11D;
				}
				this.ResultTextBox.AppendText(string.Format("Can't find {0} file pathes:\n", invalidFiles.Count));
				using (List<string>.Enumerator enumerator = invalidFiles.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						string invalidFile = enumerator.Current;
						this.ResultTextBox.AppendText(invalidFile + '\n');
					}
					goto IL_11D;
				}
			}
			this.ResultTextBox.AppendText("Import failed!");
			IL_11D:
			this.Cursor = Cursors.Default;
		}

		// Token: 0x060005F8 RID: 1528 RVA: 0x00032C88 File Offset: 0x00031C88
		public ImportCuesForm(MainForm.Context context) : base(EditorEnvironment.EditorFormsFolder + "ImportCuesForm.xml", context)
		{
			this.InitializeComponent();
			base.ParamsSaver.AutoregisterControls = false;
			base.ParamsSaver.RegisterControl(this.FilePathTextBox);
			this.BrowseButton.Click += this.OnBrowseClick;
			this.ImportButton.Click += this.OnImportClick;
		}

		// Token: 0x04000482 RID: 1154
		private const int columnsCount = 6;

		// Token: 0x04000483 RID: 1155
		private const int namePathIndex = 2;

		// Token: 0x04000484 RID: 1156
		private const int nameIndex = 3;

		// Token: 0x04000485 RID: 1157
		private const int textPathIndex = 4;

		// Token: 0x04000486 RID: 1158
		private const int textIndex = 5;
	}
}
