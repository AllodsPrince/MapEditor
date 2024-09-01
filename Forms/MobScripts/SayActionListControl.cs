using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Globalization;
using System.Windows.Forms;
using Db;
using MapEditor.Forms.PropertyControl.ExtendedPropertyControl;

namespace MapEditor.Forms.MobScripts
{
	// Token: 0x020000A3 RID: 163
	public class SayActionListControl : UserControl
	{
		// Token: 0x06000786 RID: 1926 RVA: 0x0003A26F File Offset: 0x0003926F
		protected override void Dispose(bool disposing)
		{
			if (disposing && this.components != null)
			{
				this.components.Dispose();
			}
			base.Dispose(disposing);
		}

		// Token: 0x06000787 RID: 1927 RVA: 0x0003A290 File Offset: 0x00039290
		private void InitializeComponent()
		{
			DataGridViewCellStyle dataGridViewCellStyle = new DataGridViewCellStyle();
			this.sayActionRadioButton = new RadioButton();
			this.randomSayActionRadioButton = new RadioButton();
			this.panel1 = new Panel();
			this.typesComboBox = new ComboBox();
			this.emptyRadioButton = new RadioButton();
			this.customControlCheckBox = new CheckBox();
			this.dataGridView = new DataGridView();
			this.propertyControl = new ExtendedPropertyControl();
			this.Column1 = new DataGridViewTextBoxColumn();
			this.Column2 = new DataGridViewTextBoxColumn();
			this.panel1.SuspendLayout();
			((ISupportInitialize)this.dataGridView).BeginInit();
			base.SuspendLayout();
			this.sayActionRadioButton.AutoSize = true;
			this.sayActionRadioButton.Location = new Point(3, 3);
			this.sayActionRadioButton.Name = "sayActionRadioButton";
			this.sayActionRadioButton.Size = new Size(73, 17);
			this.sayActionRadioButton.TabIndex = 6;
			this.sayActionRadioButton.Text = "SayAction";
			this.sayActionRadioButton.UseVisualStyleBackColor = true;
			this.randomSayActionRadioButton.AutoSize = true;
			this.randomSayActionRadioButton.Location = new Point(82, 3);
			this.randomSayActionRadioButton.Name = "randomSayActionRadioButton";
			this.randomSayActionRadioButton.Size = new Size(116, 17);
			this.randomSayActionRadioButton.TabIndex = 7;
			this.randomSayActionRadioButton.Text = "Random SayAction";
			this.randomSayActionRadioButton.UseVisualStyleBackColor = true;
			this.panel1.Controls.Add(this.typesComboBox);
			this.panel1.Controls.Add(this.emptyRadioButton);
			this.panel1.Controls.Add(this.sayActionRadioButton);
			this.panel1.Controls.Add(this.randomSayActionRadioButton);
			this.panel1.Location = new Point(5, 3);
			this.panel1.Name = "panel1";
			this.panel1.Size = new Size(315, 22);
			this.panel1.TabIndex = 12;
			this.typesComboBox.FormattingEnabled = true;
			this.typesComboBox.Location = new Point(0, 1);
			this.typesComboBox.Name = "typesComboBox";
			this.typesComboBox.Size = new Size(246, 21);
			this.typesComboBox.Sorted = true;
			this.typesComboBox.TabIndex = 9;
			this.typesComboBox.Visible = false;
			this.emptyRadioButton.AutoSize = true;
			this.emptyRadioButton.Location = new Point(204, 2);
			this.emptyRadioButton.Name = "emptyRadioButton";
			this.emptyRadioButton.Size = new Size(54, 17);
			this.emptyRadioButton.TabIndex = 8;
			this.emptyRadioButton.Text = "Empty";
			this.emptyRadioButton.UseVisualStyleBackColor = true;
			this.customControlCheckBox.Anchor = (AnchorStyles.Bottom | AnchorStyles.Right);
			this.customControlCheckBox.AutoSize = true;
			this.customControlCheckBox.Checked = true;
			this.customControlCheckBox.CheckState = CheckState.Checked;
			this.customControlCheckBox.Location = new Point(506, 357);
			this.customControlCheckBox.Name = "customControlCheckBox";
			this.customControlCheckBox.Size = new Size(119, 17);
			this.customControlCheckBox.TabIndex = 13;
			this.customControlCheckBox.Text = "Use Custom Control";
			this.customControlCheckBox.UseVisualStyleBackColor = true;
			this.dataGridView.Anchor = (AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right);
			this.dataGridView.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.dataGridView.Columns.AddRange(new DataGridViewColumn[]
			{
				this.Column1,
				this.Column2
			});
			this.dataGridView.Cursor = Cursors.Arrow;
			dataGridViewCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle.BackColor = SystemColors.Window;
			dataGridViewCellStyle.Font = new Font("Microsoft Sans Serif", 9f, FontStyle.Regular, GraphicsUnit.Point, 204);
			dataGridViewCellStyle.ForeColor = SystemColors.ControlText;
			dataGridViewCellStyle.SelectionBackColor = SystemColors.Highlight;
			dataGridViewCellStyle.SelectionForeColor = SystemColors.HighlightText;
			dataGridViewCellStyle.WrapMode = DataGridViewTriState.False;
			this.dataGridView.DefaultCellStyle = dataGridViewCellStyle;
			this.dataGridView.Location = new Point(5, 29);
			this.dataGridView.Name = "dataGridView";
			this.dataGridView.Size = new Size(620, 322);
			this.dataGridView.TabIndex = 14;
			this.propertyControl.AllowDrop = true;
			this.propertyControl.Anchor = (AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right);
			this.propertyControl.DefaultLocationFolder = null;
			this.propertyControl.Location = new Point(5, 31);
			this.propertyControl.Name = "propertyControl";
			this.propertyControl.SelectedObject = null;
			this.propertyControl.SelectedObjects = new object[0];
			this.propertyControl.Size = new Size(620, 320);
			this.propertyControl.SkipRefresh = false;
			this.propertyControl.TabIndex = 15;
			this.propertyControl.TitleControl = null;
			this.propertyControl.TitleRelativeFrom = null;
			this.propertyControl.TitleStart = null;
			this.propertyControl.Visible = false;
			this.Column1.HeaderText = "Prob.";
			this.Column1.Name = "Column1";
			this.Column1.SortMode = DataGridViewColumnSortMode.NotSortable;
			this.Column1.Width = 50;
			this.Column2.HeaderText = "Text";
			this.Column2.Name = "Column2";
			this.Column2.SortMode = DataGridViewColumnSortMode.NotSortable;
			this.Column2.Width = 300;
			base.AutoScaleDimensions = new SizeF(6f, 13f);
			base.AutoScaleMode = AutoScaleMode.Font;
			base.BorderStyle = BorderStyle.FixedSingle;
			base.Controls.Add(this.customControlCheckBox);
			base.Controls.Add(this.panel1);
			base.Controls.Add(this.dataGridView);
			base.Controls.Add(this.propertyControl);
			base.Name = "SayActionListControl";
			base.Size = new Size(629, 379);
			this.panel1.ResumeLayout(false);
			this.panel1.PerformLayout();
			((ISupportInitialize)this.dataGridView).EndInit();
			base.ResumeLayout(false);
			base.PerformLayout();
		}

		// Token: 0x06000788 RID: 1928 RVA: 0x0003A904 File Offset: 0x00039904
		private void OnCustomControlCheckedChanged(object sender, EventArgs e)
		{
			if (!this.customControlCheckBox.Checked)
			{
				foreach (KeyValuePair<string, RadioButton> pair in this.typeRadioButtons)
				{
					if (pair.Value.Checked)
					{
						this.lastCheckedCustomType = pair.Key;
						break;
					}
				}
			}
			this.InitControls(this.customControlCheckBox.Checked ? this.lastCheckedCustomType : string.Empty, true);
		}

		// Token: 0x06000789 RID: 1929 RVA: 0x0003A99C File Offset: 0x0003999C
		private void OnScriptTypeChecked(object sender, EventArgs e)
		{
			RadioButton rb = sender as RadioButton;
			if (rb != null && rb.Checked)
			{
				string type = rb.Tag as string;
				if (!string.IsNullOrEmpty(type))
				{
					this.InitControls(type, true);
				}
			}
		}

		// Token: 0x0600078A RID: 1930 RVA: 0x0003A9D8 File Offset: 0x000399D8
		private void OnTypeComboBoxSelectedIndexChanged(object sender, EventArgs e)
		{
			IObjMan objMan = this.propertyControl.SelectedObject as IObjMan;
			if (objMan != null)
			{
				this.propertyControl.SelectedObject = null;
				string type = this.typesComboBox.SelectedItem as string;
				if (!string.IsNullOrEmpty(type) && type != "<empty>")
				{
					if (!objMan.IsStructPtrInstanceCompatible(string.Empty, type))
					{
						objMan.SetStructPtrInstance(string.Empty, type);
					}
				}
				else if (!objMan.IsStructPtrZero(string.Empty))
				{
					objMan.SetStructPtrZero(string.Empty);
				}
				this.propertyControl.SelectedObject = objMan;
			}
		}

		// Token: 0x0600078B RID: 1931 RVA: 0x0003AA6C File Offset: 0x00039A6C
		private void OnGridCellEdit(object sender, DataGridViewCellEventArgs e)
		{
			if (e.ColumnIndex == 0 && !this.dataGridView.Rows[e.RowIndex].IsNewRow)
			{
				object _val = this.dataGridView[e.ColumnIndex, e.RowIndex].Value;
				string val = (_val != null) ? _val.ToString() : null;
				float probability;
				if (!float.TryParse(val, NumberStyles.Float, CultureInfo.CurrentCulture, out probability) && !float.TryParse(val, NumberStyles.Float, CultureInfo.InvariantCulture, out probability))
				{
					probability = 0f;
				}
				this.dataGridView[e.ColumnIndex, e.RowIndex].Value = probability;
			}
		}

		// Token: 0x0600078C RID: 1932 RVA: 0x0003AB1C File Offset: 0x00039B1C
		private void InitControls(string type, bool restoreGridData)
		{
			string prevType = this.dataGridView.Tag as string;
			if (!string.IsNullOrEmpty(prevType))
			{
				List<DataGridViewRow> rows;
				if (!this.rememberedRows.TryGetValue(prevType, out rows))
				{
					rows = new List<DataGridViewRow>();
					this.rememberedRows[prevType] = rows;
				}
				rows.Clear();
				foreach (object obj in ((IEnumerable)this.dataGridView.Rows))
				{
					DataGridViewRow row = (DataGridViewRow)obj;
					if (!string.IsNullOrEmpty(row.Cells[1].Value as string))
					{
						rows.Add(row);
					}
				}
			}
			this.dataGridView.Tag = type;
			type = (type ?? string.Empty);
			this.customControlCheckBox.Checked = this.typeRadioButtons.ContainsKey(type);
			foreach (KeyValuePair<string, RadioButton> pair in this.typeRadioButtons)
			{
				pair.Value.Checked = (pair.Key == type);
			}
			if (this.customControlCheckBox.Checked)
			{
				this.dataGridView.Visible = true;
				this.propertyControl.Visible = false;
				this.typesComboBox.Visible = false;
				foreach (RadioButton rb in this.typeRadioButtons.Values)
				{
					rb.Visible = true;
				}
				this.dataGridView.Rows.Clear();
				this.dataGridView.Enabled = (type == "VisActionRandom" || type == "CreatureSayAction");
				this.dataGridView.AllowUserToAddRows = (type == "VisActionRandom");
				this.dataGridView.Columns[0].Visible = (type != "CreatureSayAction");
				if (restoreGridData && !string.IsNullOrEmpty(type))
				{
					List<DataGridViewRow> rows2;
					if (this.rememberedRows.TryGetValue(type, out rows2))
					{
						int cnt = rows2.Count;
						for (int index = 0; index < cnt; index++)
						{
							this.dataGridView.Rows.Add(rows2[index]);
						}
					}
					if (type == "CreatureSayAction" && this.dataGridView.Rows.Count == 0)
					{
						DataGridViewRowCollection rows3 = this.dataGridView.Rows;
						object[] array = new object[2];
						array[0] = 1;
						rows3.Add(array);
						return;
					}
				}
			}
			else
			{
				this.dataGridView.Visible = false;
				this.propertyControl.Visible = true;
				this.typesComboBox.Visible = true;
				foreach (RadioButton rb2 in this.typeRadioButtons.Values)
				{
					rb2.Visible = false;
				}
				IObjMan objMan = this.propertyControl.SelectedObject as IObjMan;
				if (objMan != null)
				{
					string baseType;
					string instType;
					objMan.IsStructPtr(string.Empty, out baseType, out instType);
					if (string.IsNullOrEmpty(instType))
					{
						this.typesComboBox.SelectedItem = "<empty>";
						return;
					}
					foreach (object obj2 in this.typesComboBox.Items)
					{
						string _instType = (string)obj2;
						if (_instType == instType)
						{
							this.typesComboBox.SelectedItem = _instType;
						}
					}
				}
			}
		}

		// Token: 0x0600078D RID: 1933 RVA: 0x0003AF0C File Offset: 0x00039F0C
		private void ParseGrid(out List<RandomSayActionPair> list)
		{
			list = null;
			foreach (object obj in ((IEnumerable)this.dataGridView.Rows))
			{
				DataGridViewRow row = (DataGridViewRow)obj;
				string value = (row.Cells[1].Value != null) ? row.Cells[1].Value.ToString() : null;
				if (!string.IsNullOrEmpty(value))
				{
					if (list == null)
					{
						list = new List<RandomSayActionPair>();
					}
					string _probValue = (row.Cells[0].Value != null) ? row.Cells[0].Value.ToString() : string.Empty;
					float probability;
					float.TryParse(_probValue, out probability);
					list.Add(new RandomSayActionPair(probability, null, value));
				}
			}
		}

		// Token: 0x0600078E RID: 1934 RVA: 0x0003AFFC File Offset: 0x00039FFC
		private void LoadData(MobScriptWrapper mobScriptWrapper)
		{
			this.customControlCheckBox.Enabled = (mobScriptWrapper != null && mobScriptWrapper.ScriptMan != null);
			this.propertyControl.SelectedObject = ((mobScriptWrapper != null) ? mobScriptWrapper.ScriptMan : null);
			this.typesComboBox.Items.Clear();
			if (this.customControlCheckBox.Enabled)
			{
				IFieldDesc desc = mobScriptWrapper.ScriptMan.GetFieldDesc(string.Empty);
				if (desc != null)
				{
					this.typesComboBox.Items.Add("<empty>");
					this.typesComboBox.Items.AddRange(desc.RefTypeNames);
				}
			}
			List<RandomSayActionPair> actions = null;
			string type = (mobScriptWrapper != null) ? mobScriptWrapper.GetScriptType(out actions) : string.Empty;
			this.InitControls(type, false);
			if (actions != null)
			{
				object[][] rows = new object[actions.Count][];
				int index = 0;
				foreach (RandomSayActionPair action in actions)
				{
					rows[index] = new object[2];
					rows[index][0] = action.probability;
					rows[index][1] = action.text;
					index++;
				}
				foreach (object[] row in rows)
				{
					this.dataGridView.Rows.Add(row);
				}
			}
		}

		// Token: 0x0600078F RID: 1935 RVA: 0x0003B168 File Offset: 0x0003A168
		public SayActionListControl()
		{
			this.InitializeComponent();
			this.typeRadioButtons = new Dictionary<string, RadioButton>(3);
			this.typeRadioButtons.Add("CreatureSayAction", this.sayActionRadioButton);
			this.typeRadioButtons.Add("VisActionRandom", this.randomSayActionRadioButton);
			this.typeRadioButtons.Add("EmptyScriptType", this.emptyRadioButton);
			foreach (KeyValuePair<string, RadioButton> pair in this.typeRadioButtons)
			{
				pair.Value.Tag = pair.Key;
			}
			this.customControlCheckBox.CheckedChanged += this.OnCustomControlCheckedChanged;
			this.sayActionRadioButton.CheckedChanged += this.OnScriptTypeChecked;
			this.randomSayActionRadioButton.CheckedChanged += this.OnScriptTypeChecked;
			this.emptyRadioButton.CheckedChanged += this.OnScriptTypeChecked;
			this.dataGridView.CellEndEdit += this.OnGridCellEdit;
			this.typesComboBox.SelectedIndexChanged += this.OnTypeComboBoxSelectedIndexChanged;
		}

		// Token: 0x06000790 RID: 1936 RVA: 0x0003B2C4 File Offset: 0x0003A2C4
		public void LoadData(IObjMan scriptMan)
		{
			this.rememberedRows.Clear();
			this.dataGridView.Rows.Clear();
			this.LoadData(new MobScriptWrapper(scriptMan));
		}

		// Token: 0x06000791 RID: 1937 RVA: 0x0003B2F0 File Offset: 0x0003A2F0
		public void Save(IObjMan scriptMan)
		{
			if (scriptMan != null && this.customControlCheckBox.Checked)
			{
				MobScriptWrapper mobScriptWrapper = new MobScriptWrapper(scriptMan);
				string type = null;
				foreach (KeyValuePair<string, RadioButton> pair in this.typeRadioButtons)
				{
					if (pair.Value.Checked)
					{
						type = pair.Key;
						break;
					}
				}
				if (!string.IsNullOrEmpty(type))
				{
					List<RandomSayActionPair> list;
					this.ParseGrid(out list);
					if (list == null)
					{
						type = (this.emptyRadioButton.Tag as string);
					}
					this.propertyControl.SelectedObject = null;
					mobScriptWrapper.SetValues(type, list);
					this.LoadData(mobScriptWrapper);
				}
			}
		}

		// Token: 0x04000572 RID: 1394
		private const int probabilityCol = 0;

		// Token: 0x04000573 RID: 1395
		private const int textCol = 1;

		// Token: 0x04000574 RID: 1396
		private const string nullType = "<empty>";

		// Token: 0x04000575 RID: 1397
		private IContainer components;

		// Token: 0x04000576 RID: 1398
		private RadioButton sayActionRadioButton;

		// Token: 0x04000577 RID: 1399
		private RadioButton randomSayActionRadioButton;

		// Token: 0x04000578 RID: 1400
		private Panel panel1;

		// Token: 0x04000579 RID: 1401
		private RadioButton emptyRadioButton;

		// Token: 0x0400057A RID: 1402
		private CheckBox customControlCheckBox;

		// Token: 0x0400057B RID: 1403
		private DataGridView dataGridView;

		// Token: 0x0400057C RID: 1404
		private ExtendedPropertyControl propertyControl;

		// Token: 0x0400057D RID: 1405
		private ComboBox typesComboBox;

		// Token: 0x0400057E RID: 1406
		private DataGridViewTextBoxColumn Column1;

		// Token: 0x0400057F RID: 1407
		private DataGridViewTextBoxColumn Column2;

		// Token: 0x04000580 RID: 1408
		private readonly Dictionary<string, RadioButton> typeRadioButtons;

		// Token: 0x04000581 RID: 1409
		private readonly Dictionary<string, List<DataGridViewRow>> rememberedRows = new Dictionary<string, List<DataGridViewRow>>();

		// Token: 0x04000582 RID: 1410
		private string lastCheckedCustomType = "CreatureSayAction";
	}
}
