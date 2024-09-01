using System;
using System.ComponentModel;
using System.IO;
using System.Windows.Forms;
using Db;
using MapEditor.Map.DataProviders;
using Tools.ItemDataContainer;
using Tools.WindowParams;

namespace MapEditor.Forms.ObjectsBrowser
{
	// Token: 0x0200004B RID: 75
	public partial class ObjectsBrowserForm : Form
	{
		// Token: 0x060003FC RID: 1020 RVA: 0x000216F0 File Offset: 0x000206F0
		private void OnLoadParams(FormParams formParams)
		{
			FormParams formParams2 = this.paramsSaver.FormParams;
			int size = 1;
			int[] defaultValues = new int[1];
			formParams2.ResizeInt(size, defaultValues);
			if (this.paramsSaver.FormParams.GetInt(0) != 0)
			{
				this.SplitContainer.SplitterDistance = this.paramsSaver.FormParams.GetInt(0);
			}
		}

		// Token: 0x060003FD RID: 1021 RVA: 0x00021745 File Offset: 0x00020745
		private void OnSaveParams(FormParams formParams)
		{
			this.paramsSaver.FormParams.ResizeInt(1);
			this.paramsSaver.FormParams.SetInt(0, this.SplitContainer.SplitterDistance);
		}

		// Token: 0x060003FE RID: 1022 RVA: 0x00021774 File Offset: 0x00020774
		private void OnItemSelected(ItemList sender, string item)
		{
			if (this.created)
			{
				this.CurrentObjectTextBox.Text = item;
				this.ShowProperties();
			}
		}

		// Token: 0x060003FF RID: 1023 RVA: 0x00021790 File Offset: 0x00020790
		private void ObjectsBrowserForm_Load(object sender, EventArgs e)
		{
			this.itemList.ItemSources.Add(this.dbItemSource);
			this.itemFiltes.Bind(this.itemList.ItemFilters);
			this.itemList.Bind(this.ObjectsListView, this.ObjectFiltersComboBox, this.selectedObject);
			this.CurrentObjectTextBox.Text = this.selectedObject;
			this.ShowProperties();
			this.created = true;
		}

		// Token: 0x06000400 RID: 1024 RVA: 0x00021804 File Offset: 0x00020804
		private void ObjectsBrowserForm_FormClosing(object sender, FormClosingEventArgs e)
		{
			this.selectedObject = this.CurrentObjectTextBox.Text;
			this.itemList.Unbind();
			this.itemFiltes.Unbind();
			this.itemList.ItemSources.Clear();
			this.itemList.ItemFilters.Clear();
			this.itemList.ItemDataMiners.Clear();
			this.PropertyControl.SelectedObject = null;
		}

		// Token: 0x06000401 RID: 1025 RVA: 0x00021874 File Offset: 0x00020874
		private void ShowProperties()
		{
			if (!string.IsNullOrEmpty(this.CurrentObjectTextBox.Text))
			{
				IDatabase mainDb = IDatabase.GetMainDatabase();
				if (mainDb != null)
				{
					DBID objectDBID = mainDb.GetDBIDByName(this.CurrentObjectTextBox.Text);
					if (!objectDBID.IsEmpty())
					{
						IObjMan objectMan = mainDb.GetManipulator(objectDBID);
						if (objectMan != null)
						{
							this.PropertyControl.SelectedObject = objectMan;
							return;
						}
					}
				}
			}
			this.PropertyControl.SelectedObject = null;
		}

		// Token: 0x06000402 RID: 1026 RVA: 0x000218DC File Offset: 0x000208DC
		private void UpdateOKButton()
		{
			if (string.IsNullOrEmpty(this.CurrentObjectTextBox.Text))
			{
				this.okButton.Enabled = true;
				return;
			}
			this.okButton.Enabled = DBObjectItemDataMiner.IsValid(this.CurrentObjectTextBox.Text, this.dbItemSource.SearchClassName, this.dbItemSource.SearchDerivedClasses);
		}

		// Token: 0x06000403 RID: 1027 RVA: 0x00021939 File Offset: 0x00020939
		private void ClearButton_Click(object sender, EventArgs e)
		{
			this.itemList.ClearSelection();
			this.CurrentObjectTextBox.Text = string.Empty;
		}

		// Token: 0x06000404 RID: 1028 RVA: 0x00021956 File Offset: 0x00020956
		private void ObjectFiltersEditorButton_Click(object sender, EventArgs e)
		{
			this.itemFiltes.ShowDialog(this.itemList, this);
		}

		// Token: 0x06000405 RID: 1029 RVA: 0x0002196C File Offset: 0x0002096C
		private void AddObjectButton_Click(object sender, EventArgs e)
		{
			OpenFileDialog openFileDialog = new OpenFileDialog();
			openFileDialog.Filter = "XDB Files|*.xdb|All Files|*.*";
			openFileDialog.RestoreDirectory = true;
			openFileDialog.InitialDirectory = this.dbItemSource.SearchFolder.Replace('/', '\\');
			if (!Directory.Exists(openFileDialog.InitialDirectory))
			{
				Directory.CreateDirectory(openFileDialog.InitialDirectory);
			}
			openFileDialog.Multiselect = false;
			openFileDialog.CheckFileExists = false;
			if (openFileDialog.ShowDialog(this) == DialogResult.OK)
			{
				bool result = false;
				string objectFileName = string.Empty;
				string fileName = openFileDialog.FileName.Replace('\\', '/');
				if (fileName.Length > EditorEnvironment.DataFolder.Length)
				{
					objectFileName = fileName.Substring(EditorEnvironment.DataFolder.Length);
					IDatabase mainDb = IDatabase.GetMainDatabase();
					if (mainDb != null)
					{
						DBID objectDBID = IDatabase.CreateDBIDByName(objectFileName);
						if (!objectDBID.IsEmpty())
						{
							if (!mainDb.DoesObjectExist(objectDBID))
							{
								IObjMan objectMan = mainDb.CreateNewObject(this.dbItemSource.SearchClassName);
								if (objectMan != null)
								{
									result = mainDb.AddNewObject(objectDBID, objectMan);
								}
							}
							else
							{
								result = true;
							}
						}
					}
				}
				if (result)
				{
					this.CurrentObjectTextBox.Text = objectFileName;
					this.itemList.SelectedItem = objectFileName;
					this.itemList.Refresh();
					this.ShowProperties();
				}
			}
		}

		// Token: 0x06000406 RID: 1030 RVA: 0x00021A95 File Offset: 0x00020A95
		private void ObjectsListView_MouseDoubleClick(object sender, MouseEventArgs e)
		{
			base.InvokeOnClick(this.okButton, new EventArgs());
		}

		// Token: 0x06000407 RID: 1031 RVA: 0x00021AA8 File Offset: 0x00020AA8
		private void CurrentObjectTextBox_TextChanged(object sender, EventArgs e)
		{
			this.UpdateOKButton();
		}

		// Token: 0x06000408 RID: 1032 RVA: 0x00021AB0 File Offset: 0x00020AB0
		public ObjectsBrowserForm(DBItemSource _dbItemSource, ItemDataContainer itemDataContainer, string objectsItemListConfigFileName, string objectsFiltersConfirFileName)
		{
			this.dbItemSource = _dbItemSource;
			this.paramsSaver = new FormParamsSaver(this, EditorEnvironment.EditorFormsFolder + "ObjectsBrowserForm.xml", false);
			this.paramsSaver.AutoregisterControls = false;
			this.paramsSaver.LoadParams += this.OnLoadParams;
			this.paramsSaver.SaveParams += this.OnSaveParams;
			this.itemList = new ItemList(objectsItemListConfigFileName, itemDataContainer, true);
			ItemList itemList = this.itemList;
			itemList.ItemSelected = (ItemList.ItemEvent)Delegate.Combine(itemList.ItemSelected, new ItemList.ItemEvent(this.OnItemSelected));
			this.itemFiltes = new FolderItemFilters(objectsFiltersConfirFileName, EditorEnvironment.EditorFormsFolder);
			this.InitializeComponent();
		}

		// Token: 0x17000097 RID: 151
		// (get) Token: 0x06000409 RID: 1033 RVA: 0x00021B6D File Offset: 0x00020B6D
		// (set) Token: 0x0600040A RID: 1034 RVA: 0x00021B75 File Offset: 0x00020B75
		public string SelectedObject
		{
			get
			{
				return this.selectedObject;
			}
			set
			{
				this.selectedObject = value;
			}
		}

		// Token: 0x040002C7 RID: 711
		private string selectedObject;

		// Token: 0x040002C8 RID: 712
		private readonly FormParamsSaver paramsSaver;

		// Token: 0x040002C9 RID: 713
		private readonly ItemList itemList;

		// Token: 0x040002CA RID: 714
		private readonly FolderItemFilters itemFiltes;

		// Token: 0x040002CB RID: 715
		private readonly DBItemSource dbItemSource;

		// Token: 0x040002CC RID: 716
		private bool created;
	}
}
