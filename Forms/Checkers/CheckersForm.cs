using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using InputState;
using MapEditor.Forms.Base;
using MapEditor.Map.MapCheckers;
using MapEditor.Properties;
using MapEditor.Resources.Strings;
using Tools.MapObjects;
using Tools.WindowParams;

namespace MapEditor.Forms.Checkers
{
	// Token: 0x020000B8 RID: 184
	public partial class CheckersForm : BaseForm
	{
		// Token: 0x17000181 RID: 385
		// (get) Token: 0x060008F5 RID: 2293 RVA: 0x0004E09C File Offset: 0x0004D09C
		// (set) Token: 0x060008F6 RID: 2294 RVA: 0x0004E0A4 File Offset: 0x0004D0A4
		public string OutFileName
		{
			get
			{
				return this.fileName;
			}
			set
			{
				this.fileName = value;
			}
		}

		// Token: 0x060008F7 RID: 2295 RVA: 0x0004E0B0 File Offset: 0x0004D0B0
		private void OnLoadParams(FormParams formParams)
		{
			base.ParamsSaver.FormParams.ResizeString(1, new string[]
			{
				string.Empty
			});
			this.fileName = base.ParamsSaver.FormParams.GetString(0);
			this.created = true;
			this.UpdateControls();
			this.MainTooltip.SetToolTip(this.DescriptionTextBox, Strings.CHECKERS_FORM_DESCRIPTION_TOOLTIP);
			this.MainTooltip.SetToolTip(this.ResultTextBox, Strings.CHECKERS_FORM_RESULT_TOOLTIP);
			this.MainTooltip.SetToolTip(this.InfoTextBox, Strings.CHECKERS_FORM_INFO_TOOLTIP);
		}

		// Token: 0x060008F8 RID: 2296 RVA: 0x0004E144 File Offset: 0x0004D144
		private void OnSaveParams(FormParams formParams)
		{
			base.ParamsSaver.FormParams.ResizeString(1);
			base.ParamsSaver.FormParams.SetString(0, this.fileName);
		}

		// Token: 0x060008F9 RID: 2297 RVA: 0x0004E170 File Offset: 0x0004D170
		private IMapChecker GetSelectedChecker()
		{
			if (this.mapCheckerContainer != null && this.mapCheckerContainer.MapCheckers.Count > 0)
			{
				ListView.SelectedListViewItemCollection selectedItems = this.CheckersListView.SelectedItems;
				if (selectedItems.Count > 0)
				{
					return selectedItems[0].Tag as IMapChecker;
				}
			}
			return null;
		}

		// Token: 0x060008FA RID: 2298 RVA: 0x0004E1C0 File Offset: 0x0004D1C0
		private bool UpdateFileName()
		{
			SaveFileDialog saveFileDialog = new SaveFileDialog();
			saveFileDialog.Filter = "Text Files|*.txt|All Files|*.*";
			saveFileDialog.RestoreDirectory = true;
			if (string.IsNullOrEmpty(this.fileName))
			{
				saveFileDialog.InitialDirectory = EditorEnvironment.EditorFolder.Replace('/', '\\');
			}
			else
			{
				saveFileDialog.InitialDirectory = Str.CutFileName(this.fileName, false).Replace('/', '\\');
			}
			if (!Directory.Exists(saveFileDialog.InitialDirectory))
			{
				Directory.CreateDirectory(saveFileDialog.InitialDirectory);
			}
			if (saveFileDialog.ShowDialog(base.Context.MainForm) == DialogResult.OK)
			{
				this.fileName = saveFileDialog.FileName.Replace('\\', '/');
				return true;
			}
			return false;
		}

		// Token: 0x060008FB RID: 2299 RVA: 0x0004E268 File Offset: 0x0004D268
		public void CheckAll()
		{
			Cursor previousCursor = Cursor.Current;
			Cursor.Current = Cursors.WaitCursor;
			if (this.mapCheckerContainer != null && this.mapCheckerContainer.MapCheckers.Count > 0)
			{
				this.mapCheckerContainer.CheckAll(this);
			}
			this.UpdateControls();
			Cursor.Current = previousCursor;
		}

		// Token: 0x060008FC RID: 2300 RVA: 0x0004E2B8 File Offset: 0x0004D2B8
		private void Fix()
		{
			if (this.mapCheckerContainer != null && this.mapCheckerContainer.MapCheckers.Count > 0)
			{
				IMapChecker mapChecker = this.GetSelectedChecker();
				if (mapChecker != null)
				{
					Cursor previousCursor = Cursor.Current;
					Cursor.Current = Cursors.WaitCursor;
					if (this.mapCheckerContainer != null && this.mapCheckerContainer.MapCheckers.Count > 0)
					{
						bool transactionInProgress = false;
						if (base.Context.OperationContainer != null)
						{
							transactionInProgress = base.Context.OperationContainer.DoesTransactionInProgress;
							if (!transactionInProgress)
							{
								base.Context.OperationContainer.BeginTransaction();
							}
						}
						this.mapCheckerContainer.Fix(mapChecker, this);
						if (base.Context.OperationContainer != null && !transactionInProgress)
						{
							base.Context.OperationContainer.EndTransaction();
						}
					}
					this.UpdateControls();
					Cursor.Current = previousCursor;
				}
			}
		}

		// Token: 0x060008FD RID: 2301 RVA: 0x0004E38C File Offset: 0x0004D38C
		private void FixAll()
		{
			Cursor previousCursor = Cursor.Current;
			Cursor.Current = Cursors.WaitCursor;
			if (this.mapCheckerContainer != null && this.mapCheckerContainer.MapCheckers.Count > 0)
			{
				bool transactionInProgress = false;
				if (base.Context.OperationContainer != null)
				{
					transactionInProgress = base.Context.OperationContainer.DoesTransactionInProgress;
					if (!transactionInProgress)
					{
						base.Context.OperationContainer.BeginTransaction();
					}
				}
				this.mapCheckerContainer.FixAll(this);
				if (base.Context.OperationContainer != null && !transactionInProgress)
				{
					base.Context.OperationContainer.EndTransaction();
				}
			}
			this.UpdateControls();
			Cursor.Current = previousCursor;
		}

		// Token: 0x060008FE RID: 2302 RVA: 0x0004E434 File Offset: 0x0004D434
		private static void WriteLongInfoViewToStream(LongInfoViewNode longInfoView, StreamWriter streamWriter, int level)
		{
			if (longInfoView != null && longInfoView.Nodes != null)
			{
				IEnumerable<LongInfoViewNode> nodes = longInfoView.GetSortedNodeCollection();
				foreach (LongInfoViewNode node in nodes)
				{
					streamWriter.Write("\r\n");
					for (int i = 0; i < level; i++)
					{
						streamWriter.Write("\t");
					}
					streamWriter.Write(node);
					if (node.Nodes != null)
					{
						CheckersForm.WriteLongInfoViewToStream(node, streamWriter, level + 1);
					}
				}
			}
		}

		// Token: 0x060008FF RID: 2303 RVA: 0x0004E4C4 File Offset: 0x0004D4C4
		private void SaveCheckerResultToFile()
		{
			if (this.mapCheckerContainer != null && this.mapCheckerContainer.MapCheckers.Count > 0)
			{
				IMapChecker mapChecker = this.GetSelectedChecker();
				if (mapChecker != null)
				{
					try
					{
						if (File.Exists(this.fileName))
						{
							File.Delete(this.fileName);
						}
						StreamWriter streamWriter = new StreamWriter(this.fileName);
						streamWriter.Write(string.Format("{0}  {1}  {2}  {3}", new object[]
						{
							mapChecker.Name,
							mapChecker.ShortDescription,
							mapChecker.ShortResult,
							mapChecker.ShortInfo
						}));
						streamWriter.Write("\r\n");
						streamWriter.Write(mapChecker.LongDescription);
						streamWriter.Write("\r\n");
						streamWriter.Write(mapChecker.LongResult);
						if (!string.IsNullOrEmpty(mapChecker.LongInfoText))
						{
							streamWriter.Write("\r\n");
							streamWriter.Write(mapChecker.LongInfoText);
						}
						CheckersForm.WriteLongInfoViewToStream(mapChecker.LongInfoView, streamWriter, 0);
						streamWriter.Flush();
						streamWriter.Close();
					}
					catch (Exception e)
					{
						Console.WriteLine(e);
					}
				}
			}
		}

		// Token: 0x06000900 RID: 2304 RVA: 0x0004E5E8 File Offset: 0x0004D5E8
		public void SaveAllCheckerResultsToFile()
		{
			if (this.mapCheckerContainer != null && this.mapCheckerContainer.MapCheckers.Count > 0)
			{
				try
				{
					if (File.Exists(this.fileName))
					{
						File.Delete(this.fileName);
					}
					StreamWriter streamWriter = new StreamWriter(this.fileName);
					int index = 0;
					foreach (IMapChecker mapChecker in this.mapCheckerContainer.MapCheckers)
					{
						if (index != 0)
						{
							streamWriter.Write("\r\n\r\n");
						}
						streamWriter.Write(string.Format("{0}  {1}  {2}  {3}", new object[]
						{
							mapChecker.Name,
							mapChecker.ShortDescription,
							mapChecker.ShortResult,
							mapChecker.ShortInfo
						}));
						streamWriter.Write("\r\n");
						streamWriter.Write(mapChecker.LongDescription);
						streamWriter.Write("\r\n");
						streamWriter.Write(mapChecker.LongResult);
						if (!string.IsNullOrEmpty(mapChecker.LongInfoText))
						{
							streamWriter.Write("\r\n");
							streamWriter.Write(mapChecker.LongInfoText);
						}
						CheckersForm.WriteLongInfoViewToStream(mapChecker.LongInfoView, streamWriter, 0);
						index++;
					}
					streamWriter.Flush();
					streamWriter.Close();
				}
				catch (Exception e)
				{
					Console.WriteLine(e);
				}
			}
		}

		// Token: 0x06000901 RID: 2305 RVA: 0x0004E778 File Offset: 0x0004D778
		public void SaveOnlyBadCheckerResultsToFile()
		{
			if (this.mapCheckerContainer != null && this.mapCheckerContainer.MapCheckers.Count > 0)
			{
				try
				{
					if (File.Exists(this.fileName))
					{
						File.Delete(this.fileName);
					}
					int index = 0;
					foreach (IMapChecker mapChecker in this.mapCheckerContainer.MapCheckers)
					{
						if (mapChecker.Status == MapCheckerStatus.Red)
						{
							StreamWriter streamWriter = new StreamWriter(this.fileName);
							if (index != 0)
							{
								streamWriter.Write("\r\n\r\n");
							}
							streamWriter.Write(string.Format("{0}  {1}  {2}  {3}", new object[]
							{
								mapChecker.Name,
								mapChecker.ShortDescription,
								mapChecker.ShortResult,
								mapChecker.ShortInfo
							}));
							streamWriter.Write("\r\n");
							streamWriter.Write(mapChecker.LongDescription);
							streamWriter.Write("\r\n");
							streamWriter.Write(mapChecker.LongResult);
							if (!string.IsNullOrEmpty(mapChecker.LongInfoText))
							{
								streamWriter.Write("\r\n");
								streamWriter.Write(mapChecker.LongInfoText);
							}
							CheckersForm.WriteLongInfoViewToStream(mapChecker.LongInfoView, streamWriter, 0);
							index++;
							streamWriter.Flush();
							streamWriter.Close();
						}
					}
				}
				catch (Exception e)
				{
					Console.WriteLine(e);
				}
			}
		}

		// Token: 0x06000902 RID: 2306 RVA: 0x0004E914 File Offset: 0x0004D914
		private static void FillListViewItem(ListViewItem listViewItem, IMapChecker mapChecker)
		{
			if (listViewItem != null && mapChecker != null)
			{
				listViewItem.Name = mapChecker.Name;
				listViewItem.Text = mapChecker.Name;
				listViewItem.Tag = mapChecker;
				if (mapChecker.Status == MapCheckerStatus.Red)
				{
					listViewItem.ImageIndex = 2;
				}
				else if (mapChecker.Status == MapCheckerStatus.Yellow)
				{
					listViewItem.ImageIndex = 1;
				}
				else
				{
					listViewItem.ImageIndex = 0;
				}
				if (listViewItem.SubItems.Count < 4)
				{
					listViewItem.SubItems.Add(mapChecker.ShortDescription);
					listViewItem.SubItems.Add(mapChecker.ShortResult);
					listViewItem.SubItems.Add(mapChecker.ShortInfo);
					return;
				}
				listViewItem.SubItems[1].Text = mapChecker.ShortDescription;
				listViewItem.SubItems[2].Text = mapChecker.ShortResult;
				listViewItem.SubItems[3].Text = mapChecker.ShortInfo;
			}
		}

		// Token: 0x06000903 RID: 2307 RVA: 0x0004EA01 File Offset: 0x0004DA01
		private void UpdateControls()
		{
			this.UpdateList();
			this.UpdateTextBoxes();
			this.UpdateTabControl();
			this.UpdateButtons();
		}

		// Token: 0x06000904 RID: 2308 RVA: 0x0004EA1C File Offset: 0x0004DA1C
		private void UpdateList()
		{
			this.created = false;
			if (this.mapCheckerContainer != null && this.mapCheckerContainer.MapCheckers.Count > 0)
			{
				bool addCheckers = this.CheckersListView.Items.Count == 0;
				using (List<IMapChecker>.Enumerator enumerator = this.mapCheckerContainer.MapCheckers.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						IMapChecker mapChecker = enumerator.Current;
						if (!string.IsNullOrEmpty(mapChecker.Name))
						{
							if (addCheckers)
							{
								ListViewItem listViewItem = new ListViewItem();
								CheckersForm.FillListViewItem(listViewItem, mapChecker);
								this.CheckersListView.Items.Add(listViewItem);
							}
							else
							{
								ListViewItem[] listViewItems = this.CheckersListView.Items.Find(mapChecker.Name, false);
								if (listViewItems.Length != 0)
								{
									ListViewItem listViewItem2 = listViewItems[0];
									CheckersForm.FillListViewItem(listViewItem2, mapChecker);
								}
							}
						}
					}
					goto IL_DC;
				}
			}
			this.CheckersListView.Items.Clear();
			IL_DC:
			this.created = true;
		}

		// Token: 0x06000905 RID: 2309 RVA: 0x0004EB1C File Offset: 0x0004DB1C
		private void UpdateButtons()
		{
			if (this.mapCheckerContainer != null && this.mapCheckerContainer.MapCheckers.Count > 0)
			{
				IMapChecker mapChecker = this.GetSelectedChecker();
				bool checkersExists = this.mapCheckerContainer.GetCheckProgressCount() > 0;
				bool fixesExists = this.mapCheckerContainer.GetFixProgressCount() > 0;
				this.ToolbarItemCheck.Enabled = checkersExists;
				this.ToolbarItemSave.Enabled = (mapChecker != null && mapChecker.GetCheckProgressSteps() > 0);
				this.ToolbarItemSaveAll.Enabled = checkersExists;
				this.ToolbarItemFix.Enabled = (mapChecker != null && mapChecker.GetFixProgressSteps() > 0);
				this.ToolbarItemFixAll.Enabled = fixesExists;
				this.MenuStripItemCheck.Enabled = checkersExists;
				this.MenuStripItemSave.Enabled = (mapChecker != null && mapChecker.GetCheckProgressSteps() > 0);
				this.MenuStripItemSaveAll.Enabled = checkersExists;
				this.MenuStripItemFix.Enabled = (mapChecker != null && mapChecker.GetFixProgressSteps() > 0);
				this.MenuStripItemFixAll.Enabled = fixesExists;
				return;
			}
			this.ToolbarItemCheck.Enabled = false;
			this.ToolbarItemSave.Enabled = false;
			this.ToolbarItemSaveAll.Enabled = false;
			this.ToolbarItemFix.Enabled = false;
			this.ToolbarItemFixAll.Enabled = false;
			this.MenuStripItemCheck.Enabled = false;
			this.MenuStripItemSave.Enabled = false;
			this.MenuStripItemSaveAll.Enabled = false;
			this.MenuStripItemFix.Enabled = false;
			this.MenuStripItemFixAll.Enabled = false;
		}

		// Token: 0x06000906 RID: 2310 RVA: 0x0004EC98 File Offset: 0x0004DC98
		private void FindObject(TreeNode node)
		{
			if (node != null)
			{
				IMapObject mapObject = node.Tag as IMapObject;
				if (mapObject != null)
				{
					base.Context.MainState.ActiveState = 0;
					base.Context.StateContainer.Invoke("_select_object", new MethodArgs(this, mapObject, null));
					return;
				}
				if (node.Nodes.Count > 0)
				{
					this.FindObject(node.Nodes[0]);
				}
			}
		}

		// Token: 0x06000907 RID: 2311 RVA: 0x0004ED06 File Offset: 0x0004DD06
		private void OnInfoTreeViewNodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
		{
			this.FindObject(e.Node);
		}

		// Token: 0x06000908 RID: 2312 RVA: 0x0004ED14 File Offset: 0x0004DD14
		private static void FillTreeNodeCollection(LongInfoViewNode node, TreeNodeCollection nodes)
		{
			if (node != null && node.Nodes != null)
			{
				IEnumerable<LongInfoViewNode> children = node.GetSortedNodeCollection();
				foreach (LongInfoViewNode child in children)
				{
					TreeNode _node = new TreeNode(child.ToString());
					_node.Tag = child.MapObject;
					nodes.Add(_node);
					CheckersForm.FillTreeNodeCollection(child, _node.Nodes);
				}
			}
		}

		// Token: 0x06000909 RID: 2313 RVA: 0x0004ED94 File Offset: 0x0004DD94
		private void UpdateTextBoxes()
		{
			IMapChecker mapChecker = this.GetSelectedChecker();
			if (mapChecker != null)
			{
				this.DescriptionTextBox.Text = mapChecker.LongDescription;
				this.ResultTextBox.Text = mapChecker.LongResult;
				return;
			}
			this.DescriptionTextBox.Text = string.Empty;
			this.ResultTextBox.Text = string.Empty;
		}

		// Token: 0x0600090A RID: 2314 RVA: 0x0004EDF0 File Offset: 0x0004DDF0
		private void UpdateTabControl()
		{
			IMapChecker mapChecker = this.GetSelectedChecker();
			this.InfoTextBox.Text = string.Empty;
			this.InfoTreeView.Nodes.Clear();
			this.InfoTabControl.TabPages.Clear();
			if (mapChecker != null)
			{
				if (!string.IsNullOrEmpty(mapChecker.LongInfoText))
				{
					this.InfoTabControl.TabPages.Add(this.TextInfoTabPage);
					this.InfoTabControl.SelectedIndex = 0;
				}
				if (mapChecker.LongInfoView != null)
				{
					this.InfoTabControl.TabPages.Add(this.TreeViewInfoTabPage);
				}
				this.InfoTextBox.Text = (mapChecker.LongInfoText ?? string.Empty);
				CheckersForm.FillTreeNodeCollection(mapChecker.LongInfoView, this.InfoTreeView.Nodes);
			}
		}

		// Token: 0x0600090B RID: 2315 RVA: 0x0004EEB4 File Offset: 0x0004DEB4
		private void ToolbarItemCheck_Click(object sender, EventArgs e)
		{
			this.CheckAll();
		}

		// Token: 0x0600090C RID: 2316 RVA: 0x0004EEBC File Offset: 0x0004DEBC
		private void ToolbarItemSave_Click(object sender, EventArgs e)
		{
			if (this.UpdateFileName())
			{
				this.SaveCheckerResultToFile();
			}
		}

		// Token: 0x0600090D RID: 2317 RVA: 0x0004EECC File Offset: 0x0004DECC
		private void ToolbarItemSaveAll_Click(object sender, EventArgs e)
		{
			if (this.UpdateFileName())
			{
				this.SaveAllCheckerResultsToFile();
			}
		}

		// Token: 0x0600090E RID: 2318 RVA: 0x0004EEDC File Offset: 0x0004DEDC
		private void ToolbarItemFix_Click(object sender, EventArgs e)
		{
			this.Fix();
		}

		// Token: 0x0600090F RID: 2319 RVA: 0x0004EEE4 File Offset: 0x0004DEE4
		private void ToolbarItemFixAll_Click(object sender, EventArgs e)
		{
			this.FixAll();
		}

		// Token: 0x06000910 RID: 2320 RVA: 0x0004EEEC File Offset: 0x0004DEEC
		private void MenuStripItemCheck_Click(object sender, EventArgs e)
		{
			this.CheckAll();
		}

		// Token: 0x06000911 RID: 2321 RVA: 0x0004EEF4 File Offset: 0x0004DEF4
		private void MenuStripItemSave_Click(object sender, EventArgs e)
		{
			if (this.UpdateFileName())
			{
				this.SaveCheckerResultToFile();
			}
		}

		// Token: 0x06000912 RID: 2322 RVA: 0x0004EF04 File Offset: 0x0004DF04
		private void MenuStripItemSaveAll_Click(object sender, EventArgs e)
		{
			if (this.UpdateFileName())
			{
				this.SaveAllCheckerResultsToFile();
			}
		}

		// Token: 0x06000913 RID: 2323 RVA: 0x0004EF14 File Offset: 0x0004DF14
		private void MenuStripItemFix_Click(object sender, EventArgs e)
		{
			this.Fix();
		}

		// Token: 0x06000914 RID: 2324 RVA: 0x0004EF1C File Offset: 0x0004DF1C
		private void MenuStripItemFixAll_Click(object sender, EventArgs e)
		{
			this.FixAll();
		}

		// Token: 0x06000915 RID: 2325 RVA: 0x0004EF24 File Offset: 0x0004DF24
		private void CheckersListView_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (this.created)
			{
				this.UpdateTextBoxes();
				this.UpdateTabControl();
				this.UpdateButtons();
			}
		}

		// Token: 0x06000916 RID: 2326 RVA: 0x0004EF40 File Offset: 0x0004DF40
		public CheckersForm(MainForm.Context context) : base(EditorEnvironment.EditorFormsFolder + "CheckersForm.xml", context)
		{
			this.InitializeComponent();
			this.InfoTreeView.NodeMouseClick += this.OnInfoTreeViewNodeMouseClick;
			if (base.ParamsSaver != null)
			{
				base.ParamsSaver.RegisterControl(this.CheckersListView);
				base.ParamsSaver.RegisterControl(this.MainSplitContainer);
				base.ParamsSaver.RegisterControl(this.CheckersSplitContainer);
				base.ParamsSaver.RegisterControl(this.ResultSplitContainer);
				base.ParamsSaver.LoadParams += this.OnLoadParams;
				base.ParamsSaver.SaveParams += this.OnSaveParams;
			}
		}

		// Token: 0x06000917 RID: 2327 RVA: 0x0004F005 File Offset: 0x0004E005
		public void Bind(MapCheckerContainer _mapCheckerContainer)
		{
			this.mapCheckerContainer = _mapCheckerContainer;
			if (this.created)
			{
				this.UpdateControls();
			}
		}

		// Token: 0x06000918 RID: 2328 RVA: 0x0004F01C File Offset: 0x0004E01C
		public void Unbind()
		{
			this.mapCheckerContainer = null;
			if (this.created)
			{
				this.UpdateControls();
			}
		}

		// Token: 0x040007B2 RID: 1970
		private MapCheckerContainer mapCheckerContainer;

		// Token: 0x040007B3 RID: 1971
		private string fileName = string.Empty;

		// Token: 0x040007B4 RID: 1972
		private bool created;
	}
}
