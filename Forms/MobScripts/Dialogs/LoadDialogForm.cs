using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using Db;
using MapEditor.Forms.Quests;
using Tools.DBGameObjects;

namespace MapEditor.Forms.MobScripts.Dialogs
{
	// Token: 0x020001EA RID: 490
	public partial class LoadDialogForm : Form
	{
		// Token: 0x060018A8 RID: 6312 RVA: 0x000A3D98 File Offset: 0x000A2D98
		private void LoadGrid()
		{
			if (LoadDialogForm.mainDb != null)
			{
				this.dataGridView.Rows.Clear();
				GeneralView _visualMobs = new GeneralView(null);
				DBMethods.LoadObjects("gameMechanics.world.mob.MobWorld", _visualMobs, false);
				FilteredView visualMobs = new FilteredView(_visualMobs);
				string zone = this.zoneComboBox.SelectedItem as string;
				if (!string.IsNullOrEmpty(this.dbidTextBox.Text))
				{
					visualMobs.AddFilter(new DBIDFilter(this.dbidTextBox.Text));
				}
				if (!string.IsNullOrEmpty(zone))
				{
					visualMobs.AddFilter(new DBIDFilter(zone));
				}
				List<DataGridViewRow> rows = new List<DataGridViewRow>();
				foreach (GameObjectClass mob in visualMobs)
				{
					DataGridViewRow row = new DataGridViewRow();
					row.CreateCells(this.dataGridView);
					row.Cells[0].Value = mob;
					row.Cells[1].Value = mob.GameObject;
					rows.Add(row);
				}
				DataGridViewRow[] _rows = new DataGridViewRow[rows.Count];
				for (int index = 0; index < rows.Count; index++)
				{
					_rows[index] = rows[index];
				}
				this.dataGridView.Rows.AddRange(_rows);
				this.dataGridView.Sort(this.dataGridView.Columns[0], ListSortDirection.Ascending);
			}
		}

		// Token: 0x060018A9 RID: 6313 RVA: 0x000A3F10 File Offset: 0x000A2F10
		private void OnFilterChanged(object sender, EventArgs e)
		{
			this.LoadGrid();
		}

		// Token: 0x060018AA RID: 6314 RVA: 0x000A3F18 File Offset: 0x000A2F18
		private void OnSelectionChanged(object sender, EventArgs e)
		{
			this.okButton.Enabled = (this.dataGridView.SelectedRows.Count > 0);
		}

		// Token: 0x060018AB RID: 6315 RVA: 0x000A3F38 File Offset: 0x000A2F38
		private void OnGridDoubleClick(object sender, EventArgs e)
		{
			if (this.dataGridView.SelectedRows.Count > 0)
			{
				base.DialogResult = DialogResult.OK;
			}
		}

		// Token: 0x060018AC RID: 6316 RVA: 0x000A3F54 File Offset: 0x000A2F54
		private void OnFormLoad(object sender, EventArgs e)
		{
			if (this.zoneComboBox.Items.Count == 0)
			{
				QuestEnvironment.LoadZones(this.zoneComboBox, true);
			}
			this.okButton.Enabled = false;
			this.LoadGrid();
		}

		// Token: 0x060018AD RID: 6317 RVA: 0x000A3F88 File Offset: 0x000A2F88
		public LoadDialogForm()
		{
			this.InitializeComponent();
			base.Load += this.OnFormLoad;
			this.dbidTextBox.TextChanged += this.OnFilterChanged;
			this.zoneComboBox.SelectedIndexChanged += this.OnFilterChanged;
			this.dataGridView.SelectionChanged += this.OnSelectionChanged;
			this.dataGridView.DoubleClick += this.OnGridDoubleClick;
		}

		// Token: 0x060018AE RID: 6318 RVA: 0x000A4010 File Offset: 0x000A3010
		public DBID GetSelectedObject()
		{
			if (this.dataGridView.SelectedRows.Count > 0)
			{
				GameObjectClass go = this.dataGridView.SelectedRows[0].Cells[0].Value as GameObjectClass;
				if (go != null)
				{
					return LoadDialogForm.mainDb.GetDBIDByName(go.GameObject);
				}
			}
			return null;
		}

		// Token: 0x04001002 RID: 4098
		private static readonly IDatabase mainDb = IDatabase.GetMainDatabase();
	}
}
