using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using MapEditor.Forms.Base;
using MapEditor.Resources.Strings;
using Tools.WindowParams;

namespace MapEditor.Forms.Quests
{
	// Token: 0x02000079 RID: 121
	public partial class QuestImportForm : BaseForm
	{
		// Token: 0x060005EA RID: 1514 RVA: 0x00032466 File Offset: 0x00031466
		private void OnLoadParams(FormParams formParams)
		{
			this.lastOpenedDir = formParams.GetString(0);
		}

		// Token: 0x060005EB RID: 1515 RVA: 0x00032475 File Offset: 0x00031475
		private void OnSaveParams(FormParams formParams)
		{
			formParams.SetString(0, this.lastOpenedDir);
		}

		// Token: 0x060005EC RID: 1516 RVA: 0x00032484 File Offset: 0x00031484
		private string Browse()
		{
			OpenFileDialog fileDialog = new OpenFileDialog();
			fileDialog.Filter = "Excel files(*.xlsx)|*.xlsx|Excel files(*.xls)|*.xls";
			fileDialog.Multiselect = false;
			fileDialog.RestoreDirectory = true;
			fileDialog.FilterIndex = 0;
			if (Directory.Exists(this.lastOpenedDir))
			{
				fileDialog.InitialDirectory = this.lastOpenedDir;
			}
			if (fileDialog.ShowDialog() == DialogResult.OK)
			{
				this.lastOpenedDir = Path.GetDirectoryName(fileDialog.FileName);
				this.FilePathTextBox.Text = fileDialog.FileName;
				return fileDialog.FileName;
			}
			return null;
		}

		// Token: 0x060005ED RID: 1517 RVA: 0x00032504 File Offset: 0x00031504
		private void OnImport(bool counters)
		{
			string filePath = this.Browse();
			if (!string.IsNullOrEmpty(filePath))
			{
				this.ResultTextBox.Clear();
				if (!File.Exists(filePath))
				{
					this.ResultTextBox.AppendText(Strings.QUEST_IMPORT_FILE_NOT_EXISTS_MSG);
					return;
				}
				if (!filePath.EndsWith("xlsx") && !filePath.EndsWith("xls"))
				{
					this.ResultTextBox.AppendText(Strings.QUEST_IMPORT_WRONG_FILE_EXT_MSG);
					return;
				}
				if (base.Context.QuestEnvironment != null)
				{
					this.Cursor = Cursors.WaitCursor;
					List<string> notFoundedQuests;
					int result;
					if (!counters)
					{
						result = base.Context.QuestEnvironment.ImportExport.ImportFromExcel(filePath, out notFoundedQuests);
					}
					else
					{
						result = base.Context.QuestEnvironment.ImportExport.ImportCustomCounters(filePath, out notFoundedQuests);
					}
					this.ResultTextBox.AppendText(string.Format(Strings.QUEST_IMPORT_FINISHED_MSG, result));
					if (notFoundedQuests.Count > 0)
					{
						this.ResultTextBox.AppendText('\n' + Strings.QUEST_IMPORT_FILES_NOT_FOUNDED_MSG);
						foreach (string quest in notFoundedQuests)
						{
							this.ResultTextBox.AppendText('\n' + quest);
						}
					}
					this.Cursor = Cursors.Default;
				}
			}
		}

		// Token: 0x060005EE RID: 1518 RVA: 0x00032664 File Offset: 0x00031664
		private void OnImportQuestsClick(object sender, EventArgs e)
		{
			this.OnImport(false);
		}

		// Token: 0x060005EF RID: 1519 RVA: 0x0003266D File Offset: 0x0003166D
		private void OnImportCountersClick(object sender, EventArgs e)
		{
			this.OnImport(true);
		}

		// Token: 0x060005F0 RID: 1520 RVA: 0x00032678 File Offset: 0x00031678
		public QuestImportForm(MainForm.Context context) : base(EditorEnvironment.EditorFormsFolder + "QuestImportForm.xml", context)
		{
			this.InitializeComponent();
			this.importQuestsButton.Click += this.OnImportQuestsClick;
			this.importCountersButton.Click += this.OnImportCountersClick;
			base.ParamsSaver.AutoregisterControls = false;
			base.ParamsSaver.LoadParams += this.OnLoadParams;
			base.ParamsSaver.SaveParams += this.OnSaveParams;
		}

		// Token: 0x0400047C RID: 1148
		private string lastOpenedDir;
	}
}
