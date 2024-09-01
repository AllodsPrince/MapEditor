using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;
using Tools.DBGameObjects;
using Tools.WindowParams;

namespace MapEditor.Forms.Quests
{
	// Token: 0x02000015 RID: 21
	public partial class QuestSelectDialogForm : Form
	{
		// Token: 0x060001F6 RID: 502 RVA: 0x00015C6C File Offset: 0x00014C6C
		private static ListSortDirection IntToSortDirection(int value)
		{
			ListSortDirection listSortDirection;
			if (value == 0)
			{
				listSortDirection = ListSortDirection.Ascending;
			}
			else
			{
				listSortDirection = ListSortDirection.Descending;
			}
			return listSortDirection;
		}

		// Token: 0x060001F7 RID: 503 RVA: 0x00015C84 File Offset: 0x00014C84
		private static int SortDirectionToInt(ListSortDirection value)
		{
			int listSortDirection;
			if (value == ListSortDirection.Ascending)
			{
				listSortDirection = 0;
			}
			else
			{
				listSortDirection = 1;
			}
			return listSortDirection;
		}

		// Token: 0x060001F8 RID: 504 RVA: 0x00015C9C File Offset: 0x00014C9C
		private void OnPostLoadParams(FormParams formParams)
		{
			FormParams formParams2 = this.paramsSaver.FormParams;
			int size = 2;
			int[] defaultValues = new int[2];
			formParams2.ResizeInt(size, defaultValues);
			int sortedColumnIndex = this.paramsSaver.FormParams.GetInt(0);
			ListSortDirection listSortDirection = QuestSelectDialogForm.IntToSortDirection(this.paramsSaver.FormParams.GetInt(1));
			if (sortedColumnIndex > -1 && sortedColumnIndex < this.ItemsView.Columns.Count)
			{
				this.ItemsView.Sort(this.ItemsView.Columns[sortedColumnIndex], listSortDirection);
			}
			this.LoadList(this.startSelectedObj);
			this.paramsSaver.FormParams.ResizeString(1, new string[]
			{
				string.Empty
			});
			this.defaultLocationFolder = this.paramsSaver.FormParams.GetString(0);
		}

		// Token: 0x060001F9 RID: 505 RVA: 0x00015D64 File Offset: 0x00014D64
		private void OnSaveParams(FormParams formParams)
		{
			int sortedColIndex = 0;
			if (this.ItemsView.SortedColumn != null)
			{
				sortedColIndex = this.ItemsView.SortedColumn.DisplayIndex;
			}
			ListSortDirection listSortDirection = ListSortDirection.Ascending;
			if (this.ItemsView.SortOrder == SortOrder.Descending)
			{
				listSortDirection = ListSortDirection.Descending;
			}
			this.paramsSaver.FormParams.ResizeInt(2);
			this.paramsSaver.FormParams.SetInt(0, sortedColIndex);
			this.paramsSaver.FormParams.SetInt(1, QuestSelectDialogForm.SortDirectionToInt(listSortDirection));
			this.paramsSaver.FormParams.ResizeString(1);
			this.paramsSaver.FormParams.SetString(0, this.defaultLocationFolder);
		}

		// Token: 0x060001FA RID: 506 RVA: 0x00015E05 File Offset: 0x00014E05
		private void LoadZones()
		{
			this.questEnvironment.LoadLoadedZones(this.ZoneComboBox, true);
		}

		// Token: 0x060001FB RID: 507 RVA: 0x00015E1C File Offset: 0x00014E1C
		private void SetDisplayedList(string typeName, string dbid, string zone, string name, string minLevelString, string maxLevelString, string gameName)
		{
			FilteredView filteredView = new FilteredView(this.view);
			if (!string.IsNullOrEmpty(typeName))
			{
				filteredView.AddFilter(new TypeNameFilter(typeName));
			}
			if (!string.IsNullOrEmpty(dbid))
			{
				filteredView.AddFilter(new DBIDFilter(dbid));
			}
			if (!string.IsNullOrEmpty(zone))
			{
				filteredView.AddFilter(new DBIDFilter(zone));
			}
			if (!string.IsNullOrEmpty(name))
			{
				filteredView.AddFilter(new NameFilter(name));
			}
			try
			{
				int minLevel;
				if (int.TryParse(minLevelString, out minLevel))
				{
					filteredView.AddFilter(new LevelMinFilter(minLevel));
				}
				int maxLevel;
				if (int.TryParse(maxLevelString, out maxLevel))
				{
					filteredView.AddFilter(new LevelMaxFilter(maxLevel));
				}
			}
			catch (ArgumentException e)
			{
				Console.WriteLine(e);
			}
			if (!string.IsNullOrEmpty(gameName))
			{
				filteredView.AddFilter(new GameNameFilter(gameName));
			}
			this.displayedItems = filteredView;
		}

		// Token: 0x060001FC RID: 508 RVA: 0x00015EF0 File Offset: 0x00014EF0
		private void LoadList(GameObjectClass selectedGameObj)
		{
			this.SetDisplayedList(this.TypesComboBox.Text, this.DBIDTextBox.Text, this.ZoneComboBox.Text, this.NameTextBox.Text, this.LevelFromTextBox.Text, this.LevelToTextBox.Text, this.GameNameTextBox.Text);
			this.ItemsView.Rows.Clear();
			List<DataGridViewRow> rows = new List<DataGridViewRow>();
			DataGridViewCell selectedCell = null;
			foreach (GameObjectClass baseObject in this.displayedItems)
			{
				string level;
				if (baseObject.LevelMin == baseObject.LevelMax)
				{
					level = baseObject.LevelMin.ToString("00");
				}
				else
				{
					level = baseObject.LevelMin.ToString("00") + "-" + baseObject.LevelMax.ToString("00");
				}
				object[] values = new object[]
				{
					baseObject.GameObject,
					baseObject,
					level,
					baseObject.GameName
				};
				DataGridViewRow row = new DataGridViewRow();
				row.CreateCells(this.ItemsView, values);
				rows.Add(row);
				if (selectedCell == null && selectedGameObj != null && selectedGameObj.GameObject == row.Cells[0].Value as string)
				{
					selectedCell = row.Cells[0];
				}
			}
			this.ItemsView.Rows.AddRange(rows.ToArray());
			ListSortDirection listSortDirection = ListSortDirection.Descending;
			if (this.ItemsView.SortOrder == SortOrder.Ascending)
			{
				listSortDirection = ListSortDirection.Ascending;
			}
			if (this.ItemsView.SortedColumn != null)
			{
				this.ItemsView.Sort(this.ItemsView.SortedColumn, listSortDirection);
			}
			if (selectedCell != null)
			{
				this.ItemsView.CurrentCell = selectedCell;
				this.ItemsView.FirstDisplayedCell = selectedCell;
			}
		}

		// Token: 0x060001FD RID: 509 RVA: 0x000160F0 File Offset: 0x000150F0
		private void LoadTypes(IEnumerable<string> typeNames)
		{
			this.TypesComboBox.Items.Add(string.Empty);
			foreach (string typeName in typeNames)
			{
				this.TypesComboBox.Items.Add(typeName);
			}
		}

		// Token: 0x060001FE RID: 510 RVA: 0x0001615C File Offset: 0x0001515C
		private void CancelSelectClick(object sender, EventArgs e)
		{
			base.DialogResult = DialogResult.Cancel;
			this.selectedItem = null;
			base.Close();
		}

		// Token: 0x060001FF RID: 511 RVA: 0x00016172 File Offset: 0x00015172
		private void FindClick(object sender, EventArgs e)
		{
			this.LoadList(null);
		}

		// Token: 0x06000200 RID: 512 RVA: 0x0001617B File Offset: 0x0001517B
		private void ItemsView_DblClick(object sender, EventArgs e)
		{
			this.SelectItem();
		}

		// Token: 0x06000201 RID: 513 RVA: 0x00016183 File Offset: 0x00015183
		private void ClearSelectCkick(object sender, EventArgs e)
		{
			this.selectedItem = null;
			base.DialogResult = DialogResult.OK;
			base.Close();
		}

		// Token: 0x06000202 RID: 514 RVA: 0x0001619C File Offset: 0x0001519C
		private void SelectItem()
		{
			if (this.ItemsView.SelectedCells.Count > 0)
			{
				int rowIndex = this.ItemsView.SelectedCells[0].RowIndex;
				GameObjectClass val = this.ItemsView.Rows[rowIndex].Cells[1].Value as GameObjectClass;
				if (val != null)
				{
					this.selectedItem = val;
					base.DialogResult = DialogResult.OK;
					base.Close();
				}
			}
		}

		// Token: 0x06000203 RID: 515 RVA: 0x00016214 File Offset: 0x00015214
		private void OnExport(object sender, EventArgs e)
		{
			this.Cursor = Cursors.WaitCursor;
			this.questEnvironment.ImportExport.ExportToExcel(this.displayedItems);
			this.questEnvironment.ImportExport.ExortCustomCounters(this.displayedItems);
			this.Cursor = Cursors.Default;
		}

		// Token: 0x06000204 RID: 516 RVA: 0x00016264 File Offset: 0x00015264
		private void OnCellEnter(object sender, DataGridViewCellEventArgs e)
		{
			if (e.RowIndex > -1 && e.RowIndex < this.ItemsView.Rows.Count)
			{
				this.selectedItem = (this.ItemsView.Rows[e.RowIndex].Cells[1].Value as GameObjectClass);
			}
		}

		// Token: 0x06000205 RID: 517 RVA: 0x000162C4 File Offset: 0x000152C4
		public QuestSelectDialogForm(QuestEnvironment _questEnvironment, Tools.DBGameObjects.View _view, GameObjectClass selectedObject)
		{
			this.InitializeComponent();
			this.questEnvironment = _questEnvironment;
			this.view = _view;
			this.displayedItems = this.view;
			string paramsFileName;
			if (string.IsNullOrEmpty(this.view.Name))
			{
				paramsFileName = EditorEnvironment.EditorFormsFolder + base.Name + ".xml";
			}
			else
			{
				paramsFileName = string.Concat(new string[]
				{
					EditorEnvironment.EditorFormsFolder,
					base.Name,
					"_",
					this.view.Name,
					".xml"
				});
			}
			this.paramsSaver = new FormParamsSaver(this, paramsFileName, false);
			foreach (object obj in this.ItemsView.Columns)
			{
				DataGridViewColumn column = (DataGridViewColumn)obj;
				this.paramsSaver.RegisterControl(column);
			}
			this.paramsSaver.PostLoadParams += this.OnPostLoadParams;
			this.paramsSaver.SaveParams += this.OnSaveParams;
			this.startSelectedObj = selectedObject;
			this.CancelSelectButton.Click += this.CancelSelectClick;
			this.FindButton.Click += this.FindClick;
			this.ClearSelectButton.Click += this.ClearSelectCkick;
			this.ItemsView.DoubleClick += this.ItemsView_DblClick;
			this.ItemsView.CellEnter += this.OnCellEnter;
			this.ExportButton.Click += this.OnExport;
			List<string> typeNames = this.view.GetLoadedTypeNames();
			foreach (string type in typeNames)
			{
				if (type != "gameMechanics.constructor.schemes.quest.QuestResource")
				{
					this.ExportButton.Visible = false;
				}
			}
			this.LoadTypes(typeNames);
			this.LoadZones();
		}

		// Token: 0x06000206 RID: 518 RVA: 0x00016504 File Offset: 0x00015504
		public GameObjectClass GetSelectedItem()
		{
			return this.selectedItem;
		}

		// Token: 0x0400019B RID: 411
		private const int DBIDCol = 0;

		// Token: 0x0400019C RID: 412
		private const int objectCol = 1;

		// Token: 0x0400019D RID: 413
		private const int ascendingSortDirection = 0;

		// Token: 0x0400019E RID: 414
		private const int descendingSortDirection = 1;

		// Token: 0x0400019F RID: 415
		private readonly QuestEnvironment questEnvironment;

		// Token: 0x040001A0 RID: 416
		private readonly FormParamsSaver paramsSaver;

		// Token: 0x040001A1 RID: 417
		private readonly GameObjectClass startSelectedObj;

		// Token: 0x040001A2 RID: 418
		private string defaultLocationFolder = string.Empty;

		// Token: 0x040001A3 RID: 419
		private readonly Tools.DBGameObjects.View view;

		// Token: 0x040001A4 RID: 420
		private Tools.DBGameObjects.View displayedItems;

		// Token: 0x040001A5 RID: 421
		private GameObjectClass selectedItem;
	}
}
