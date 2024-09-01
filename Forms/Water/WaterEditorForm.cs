using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;
using System.Windows.Forms.DynamicProperty;
using Db;
using InputState;
using MapEditor.Forms.Base;
using MapEditor.Map;
using MapEditor.Resources.Strings;
using Tools.InputBox;
using Tools.WindowParams;

namespace MapEditor.Forms.Water
{
	// Token: 0x0200006A RID: 106
	public partial class WaterEditorForm : BaseForm
	{
		// Token: 0x06000526 RID: 1318 RVA: 0x00029A98 File Offset: 0x00028A98
		private void OnLoadParams(FormParams formParams)
		{
			List<string> items = new List<string>();
			Constants.GetContinentNameList(ref items);
			foreach (string item in items)
			{
				if (item.IndexOf("Test", StringComparison.InvariantCultureIgnoreCase) == -1)
				{
					this.continentComboBox.Items.Add(item);
				}
			}
			foreach (object obj in this.exportButtomsGroupBox.Controls)
			{
				Control control = (Control)obj;
				Button button = control as Button;
				if (button != null)
				{
					button.Click += this.OnExportButtonClick;
				}
			}
		}

		// Token: 0x06000527 RID: 1319 RVA: 0x00029B78 File Offset: 0x00028B78
		private void OnContinentIndexChanged(object sender, EventArgs e)
		{
			if (this.arrayObjMan != null)
			{
				this.propertyControl.SelectedObject = null;
				this.arrayObjMan.Dispose();
				this.arrayObjMan = null;
			}
			string mapResource = this.continentComboBox.Text;
			if (!string.IsNullOrEmpty(mapResource))
			{
				DBID layerDBID = WaterEditorForm.mainDb.GetDBIDByName(mapResource + "/layers.xdb");
				if (!DBID.IsNullOrEmpty(layerDBID))
				{
					IObjMan layerObjMan = WaterEditorForm.mainDb.GetManipulator(layerDBID);
					if (layerObjMan != null)
					{
						this.arrayObjMan = layerObjMan.CreateManipulator("waterLayers");
						layerObjMan.Dispose();
					}
				}
			}
			this.LoadNameList(null);
			this.newWaterButton.Enabled = (this.arrayObjMan != null);
		}

		// Token: 0x06000528 RID: 1320 RVA: 0x00029C21 File Offset: 0x00028C21
		private void OnNameIndexChanged(object sender, EventArgs e)
		{
			this.SetPropertyControl();
		}

		// Token: 0x06000529 RID: 1321 RVA: 0x00029C2C File Offset: 0x00028C2C
		private void OnSetDbValue(PropertyGrid sender, IFieldDesc desc, object value)
		{
			if (base.Context.EditorScene != null && this.arrayObjMan != null && sender == this.propertyControl.PropertyGrid && desc != null)
			{
				if (desc.FieldName == "SymbolName")
				{
					this.LoadNameList(this.nameComboBox.Text);
					return;
				}
				base.Context.EditorScene.UpdateWater();
			}
		}

		// Token: 0x0600052A RID: 1322 RVA: 0x00029C93 File Offset: 0x00028C93
		private void OnPropertyControlUndoRedo()
		{
			if (base.Context.EditorScene != null && this.arrayObjMan != null)
			{
				this.LoadNameList(this.nameComboBox.Text);
				base.Context.EditorScene.UpdateWater();
			}
		}

		// Token: 0x0600052B RID: 1323 RVA: 0x00029CCC File Offset: 0x00028CCC
		private void OnNewClick(object sender, EventArgs e)
		{
			if (this.arrayObjMan != null)
			{
				InputBoxForm inputBox = new InputBoxForm(new InputBoxForm.IsOk(this.CheckNewWaterName));
				if (inputBox.ShowDialog(this) == DialogResult.OK)
				{
					int cnt = this.arrayObjMan.GetArraySize();
					for (int index = 0; index < cnt; index++)
					{
						this.arrayObjMan.SetArrayIndex(index);
						string symbolName;
						this.arrayObjMan.GetValue("SymbolName", out symbolName);
						if (string.IsNullOrEmpty(symbolName))
						{
							this.arrayObjMan.SetValue("SymbolName", inputBox.InputText);
							this.LoadNameList(inputBox.InputText);
							base.Context.StateContainer.Invoke("_water_layer_list_changed", new MethodArgs(this, this.arrayObjMan.DBID, null));
							return;
						}
					}
				}
			}
		}

		// Token: 0x0600052C RID: 1324 RVA: 0x00029D8C File Offset: 0x00028D8C
		private void OnDeleteClick(object sender, EventArgs e)
		{
			if (this.arrayObjMan != null && MessageBox.Show(Strings.WATER_EDITOR_DELETE_WARNING, this.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) == DialogResult.Yes)
			{
				this.arrayObjMan.Remove(string.Empty, this.arrayObjMan.GetArrayIndex());
				this.LoadNameList(null);
				base.Context.StateContainer.Invoke("_water_layer_list_changed", new MethodArgs(this, this.arrayObjMan.DBID, null));
			}
		}

		// Token: 0x0600052D RID: 1325 RVA: 0x00029E04 File Offset: 0x00028E04
		private void OnSaveDbClick(object sender, EventArgs e)
		{
			Cursor.Current = Cursors.WaitCursor;
			WaterEditorForm.mainDb.SaveChanges();
			if (base.Context.EditorScene != null && this.arrayObjMan != null)
			{
				base.Context.EditorScene.UpdateWater();
			}
			Cursor.Current = Cursors.Default;
		}

		// Token: 0x0600052E RID: 1326 RVA: 0x00029E54 File Offset: 0x00028E54
		private void OnExportButtonClick(object sender, EventArgs e)
		{
			if (this.arrayObjMan != null)
			{
				Button button = sender as Button;
				if (button != null)
				{
					string fieldName = button.Tag as string;
					if (!string.IsNullOrEmpty(fieldName) && this.arrayObjMan.GetFieldDesc(fieldName) != null)
					{
						OpenFileDialog fileDialog = new OpenFileDialog();
						fileDialog.Filter = "Texture source files(*.tga, *.psd)|*.tga;*.psd";
						fileDialog.Multiselect = true;
						if (fileDialog.ShowDialog() == DialogResult.OK)
						{
							Cursor.Current = Cursors.WaitCursor;
							string dbidString;
							if (TerrainLayers.ExportTerrain(fileDialog.FileName, out dbidString))
							{
								this.arrayObjMan.SetValue(fieldName, dbidString);
							}
							else
							{
								MessageBox.Show(Strings.MAP_EXPORT_ERROR_MESSAGE, Strings.MAP_SAVE_CHANGES_TITLE, MessageBoxButtons.OK, MessageBoxIcon.Hand);
							}
							Cursor.Current = Cursors.Default;
						}
					}
				}
			}
		}

		// Token: 0x0600052F RID: 1327 RVA: 0x00029F04 File Offset: 0x00028F04
		private void LoadNameList(string selectedName)
		{
			this.nameComboBox.Items.Clear();
			if (this.arrayObjMan != null)
			{
				int cnt = this.arrayObjMan.GetArraySize();
				for (int index = 0; index < cnt; index++)
				{
					this.arrayObjMan.SetArrayIndex(index);
					string symbolName;
					this.arrayObjMan.GetValue("SymbolName", out symbolName);
					if (!string.IsNullOrEmpty(symbolName))
					{
						this.nameComboBox.Items.Add(symbolName);
					}
				}
				if (!string.IsNullOrEmpty(selectedName) && this.nameComboBox.Items.Contains(selectedName))
				{
					this.nameComboBox.SelectedItem = selectedName;
				}
				else if (this.nameComboBox.Items.Count > 0)
				{
					this.nameComboBox.SelectedIndex = 0;
				}
			}
			this.SetPropertyControl();
		}

		// Token: 0x06000530 RID: 1328 RVA: 0x00029FCC File Offset: 0x00028FCC
		private void SetPropertyControl()
		{
			bool finded = false;
			if (this.arrayObjMan != null && !string.IsNullOrEmpty(this.nameComboBox.Text))
			{
				int cnt = this.arrayObjMan.GetArraySize();
				for (int index = 0; index < cnt; index++)
				{
					this.arrayObjMan.SetArrayIndex(index);
					string symbolName;
					this.arrayObjMan.GetValue("SymbolName", out symbolName);
					if (symbolName == this.nameComboBox.Text)
					{
						this.propertyControl.SelectedObject = this.arrayObjMan;
						finded = true;
						break;
					}
				}
			}
			if (!finded)
			{
				this.propertyControl.SelectedObject = null;
			}
			this.propertyControl.PropertyGrid.Refresh();
			this.deleteWaterButton.Enabled = finded;
			foreach (object obj in this.exportButtomsGroupBox.Controls)
			{
				Control control = (Control)obj;
				Button button = control as Button;
				if (button != null)
				{
					button.Enabled = finded;
				}
			}
		}

		// Token: 0x06000531 RID: 1329 RVA: 0x0002A0E8 File Offset: 0x000290E8
		private bool CheckNewWaterName(string _name, out string errorText)
		{
			errorText = null;
			if (string.IsNullOrEmpty(_name))
			{
				errorText = string.Empty;
				return false;
			}
			if (this.nameComboBox.Items.Contains(_name))
			{
				errorText = Strings.NAME_ALREADY_EXISTS;
				return false;
			}
			return true;
		}

		// Token: 0x06000532 RID: 1330 RVA: 0x0002A11C File Offset: 0x0002911C
		public WaterEditorForm(MainForm.Context context) : base(EditorEnvironment.EditorFormsFolder + "WaterEditorForm.xml", context)
		{
			this.InitializeComponent();
			if (base.ParamsSaver != null)
			{
				base.ParamsSaver.AutoregisterControls = false;
				base.ParamsSaver.RegisterControl(this.continentComboBox, true);
				base.ParamsSaver.LoadParams += this.OnLoadParams;
			}
			this.continentComboBox.SelectedIndexChanged += this.OnContinentIndexChanged;
			this.nameComboBox.SelectedIndexChanged += this.OnNameIndexChanged;
			PropertyBag.SetDbValue += this.OnSetDbValue;
			this.propertyControl.UndoRedo += this.OnPropertyControlUndoRedo;
			this.newWaterButton.Click += this.OnNewClick;
			this.deleteWaterButton.Click += this.OnDeleteClick;
			this.saveDbButton.Click += this.OnSaveDbClick;
		}

		// Token: 0x06000533 RID: 1331 RVA: 0x0002A220 File Offset: 0x00029220
		public void LoadWater(string continentName, int index)
		{
			if (!base.Visible)
			{
				base.Context.StateContainer.Invoke("toggle_water_editor", new MethodArgs(this, null, null));
			}
			this.continentComboBox.SelectedItem = continentName;
			if (this.arrayObjMan != null && index > -1 && index < this.arrayObjMan.GetArraySize())
			{
				this.arrayObjMan.SetArrayIndex(index);
				string symbolName;
				this.arrayObjMan.GetValue("SymbolName", out symbolName);
				this.nameComboBox.SelectedItem = symbolName;
			}
		}

		// Token: 0x040003B3 RID: 947
		private const string layersFileName = "/layers.xdb";

		// Token: 0x040003B4 RID: 948
		private const string symbolNameField = "SymbolName";

		// Token: 0x040003B5 RID: 949
		private const string testRoot = "Test";

		// Token: 0x040003C5 RID: 965
		private static readonly IDatabase mainDb = IDatabase.GetMainDatabase();

		// Token: 0x040003C6 RID: 966
		private IObjMan arrayObjMan;
	}
}
