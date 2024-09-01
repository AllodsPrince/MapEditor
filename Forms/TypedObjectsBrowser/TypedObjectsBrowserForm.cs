using System;
using System.ComponentModel;
using System.IO;
using System.Windows.Forms;
using Db;
using MapEditor.Forms.TypedObjectsBrowser.DataProviders;
using Tools.ItemDataContainer;
using Tools.WindowParams;

namespace MapEditor.Forms.TypedObjectsBrowser
{
	// Token: 0x02000078 RID: 120
	public partial class TypedObjectsBrowserForm : Form
	{
		// Token: 0x060005CF RID: 1487 RVA: 0x00030FBC File Offset: 0x0002FFBC
		private DBItemSource GetActiveDBItemSource()
		{
			if (this.typedObjectParams != null && this.typedObjectParams.SelectedObjectTypeIndex >= 0 && this.typedObjectParams.SelectedObjectTypeIndex < this.typedObjectParams.DBItemSources.Count)
			{
				return this.typedObjectParams.DBItemSources[this.typedObjectParams.SelectedObjectTypeIndex];
			}
			return null;
		}

		// Token: 0x060005D0 RID: 1488 RVA: 0x0003101C File Offset: 0x0003001C
		private void CreateItemList()
		{
			DBItemSource dbItemSource = this.GetActiveDBItemSource();
			if (dbItemSource != null)
			{
				this.itemList.ItemSources.Add(dbItemSource);
				this.itemFiltes.Bind(this.itemList.ItemFilters);
				this.itemList.Bind(this.TypedObjectsListView, this.TypedObjectFiltersComboBox, this.typedObjectParams.Name);
			}
		}

		// Token: 0x060005D1 RID: 1489 RVA: 0x0003107C File Offset: 0x0003007C
		private void DestroyItemList()
		{
			this.itemList.Unbind();
			this.itemFiltes.Unbind();
			this.itemList.ItemSources.Clear();
		}

		// Token: 0x060005D2 RID: 1490 RVA: 0x000310A4 File Offset: 0x000300A4
		private void OnLoadParams(FormParams formParams)
		{
			FormParams formParams2 = this.paramsSaver.FormParams;
			int size = 2;
			int[] defaultValues = new int[2];
			formParams2.ResizeInt(size, defaultValues);
			if (this.paramsSaver.FormParams.GetInt(0) != 0)
			{
				this.SplitContainer.SplitterDistance = this.paramsSaver.FormParams.GetInt(0);
			}
			if (this.typedObjectParams != null && string.IsNullOrEmpty(this.typedObjectParams.Name))
			{
				this.typedObjectParams.SelectedObjectTypeIndex = this.paramsSaver.FormParams.GetInt(1);
				if (this.typedObjectParams.SelectedObjectTypeIndex < 0 || this.typedObjectParams.SelectedObjectTypeIndex >= this.typedObjectParams.ObjectTypeCount)
				{
					this.typedObjectParams.SelectedObjectTypeIndex = 0;
				}
			}
		}

		// Token: 0x060005D3 RID: 1491 RVA: 0x00031164 File Offset: 0x00030164
		private void OnSaveParams(FormParams formParams)
		{
			this.paramsSaver.FormParams.ResizeInt(2);
			this.paramsSaver.FormParams.SetInt(0, this.SplitContainer.SplitterDistance);
			if (this.typedObjectParams != null)
			{
				this.paramsSaver.FormParams.SetInt(1, this.typedObjectParams.SelectedObjectTypeIndex);
				return;
			}
			this.paramsSaver.FormParams.SetInt(1, 0);
		}

		// Token: 0x060005D4 RID: 1492 RVA: 0x000311D5 File Offset: 0x000301D5
		private void OnItemSelected(ItemList sender, string item)
		{
			if (this.created)
			{
				this.CurrentTypedObjectTextBox.Text = item;
				this.ShowProperties();
			}
		}

		// Token: 0x060005D5 RID: 1493 RVA: 0x000311F4 File Offset: 0x000301F4
		private void TypedObjectsBrowserForm_Load(object sender, EventArgs e)
		{
			if (this.typedObjectParams != null)
			{
				int typeRadioButtonsCount = 5;
				RadioButton[] typeRadioButtons = new RadioButton[]
				{
					this.Type0RadioButton,
					this.Type1RadioButton,
					this.Type2RadioButton,
					this.Type3RadioButton,
					this.Type4RadioButton
				};
				for (int index = 0; index < this.typedObjectParams.TypeNames.Count; index++)
				{
					if (index < typeRadioButtonsCount)
					{
						typeRadioButtons[index].Text = this.typedObjectParams.TypeNames[index];
						typeRadioButtons[index].Enabled = true;
						typeRadioButtons[index].Visible = true;
					}
				}
				for (int index2 = this.typedObjectParams.TypeNames.Count; index2 < typeRadioButtonsCount; index2++)
				{
					typeRadioButtons[index2].Enabled = false;
					typeRadioButtons[index2].Visible = false;
				}
				if (this.typedObjectParams.SelectedTypeIndex >= 0 && this.typedObjectParams.SelectedTypeIndex < typeRadioButtonsCount)
				{
					typeRadioButtons[this.typedObjectParams.SelectedTypeIndex].Checked = true;
				}
				int objectTypeRadioButtonsCount = 4;
				RadioButton[] objectTypeRadioButtons = new RadioButton[]
				{
					this.ObjectType0RadioButton,
					this.ObjectType1RadioButton,
					this.ObjectType2RadioButton,
					this.ObjectType3RadioButton
				};
				for (int index3 = 0; index3 < this.typedObjectParams.ObjectTypeNames.Count; index3++)
				{
					if (index3 < objectTypeRadioButtonsCount)
					{
						objectTypeRadioButtons[index3].Text = this.typedObjectParams.ObjectTypeNames[index3];
						objectTypeRadioButtons[index3].Enabled = true;
						objectTypeRadioButtons[index3].Visible = true;
					}
				}
				for (int index4 = this.typedObjectParams.ObjectTypeNames.Count; index4 < objectTypeRadioButtonsCount; index4++)
				{
					objectTypeRadioButtons[index4].Enabled = false;
					objectTypeRadioButtons[index4].Visible = false;
				}
				if (this.typedObjectParams.SelectedObjectTypeIndex >= 0 && this.typedObjectParams.SelectedObjectTypeIndex < objectTypeRadioButtonsCount)
				{
					objectTypeRadioButtons[this.typedObjectParams.SelectedObjectTypeIndex].Checked = true;
				}
				this.CreateItemList();
				this.CurrentTypedObjectTextBox.Text = this.typedObjectParams.Name;
				this.ShowProperties();
				this.created = true;
			}
		}

		// Token: 0x060005D6 RID: 1494 RVA: 0x0003140F File Offset: 0x0003040F
		private void TypedObjectsBrowserForm_FormClosing(object sender, FormClosingEventArgs e)
		{
			if (this.typedObjectParams != null)
			{
				this.typedObjectParams.Name = this.CurrentTypedObjectTextBox.Text;
				this.DestroyItemList();
				this.PropertyControl.SelectedObject = null;
			}
		}

		// Token: 0x060005D7 RID: 1495 RVA: 0x00031444 File Offset: 0x00030444
		private void ShowProperties()
		{
			if (this.typedObjectParams != null)
			{
				if (!string.IsNullOrEmpty(this.CurrentTypedObjectTextBox.Text))
				{
					IDatabase mainDb = IDatabase.GetMainDatabase();
					if (mainDb != null)
					{
						DBID typedObjectDBID = mainDb.GetDBIDByName(this.CurrentTypedObjectTextBox.Text);
						if (!typedObjectDBID.IsEmpty())
						{
							IObjMan typedObjectMan = mainDb.GetManipulator(typedObjectDBID);
							if (typedObjectMan != null)
							{
								this.PropertyControl.SelectedObject = typedObjectMan;
								return;
							}
						}
					}
				}
				this.PropertyControl.SelectedObject = null;
			}
		}

		// Token: 0x060005D8 RID: 1496 RVA: 0x000314B2 File Offset: 0x000304B2
		private void UpdateOKButton()
		{
			if (this.typedObjectParams != null)
			{
				this.okButton.Enabled = true;
			}
		}

		// Token: 0x060005D9 RID: 1497 RVA: 0x000314C8 File Offset: 0x000304C8
		private void ClearButton_Click(object sender, EventArgs e)
		{
			this.itemList.ClearSelection();
			this.CurrentTypedObjectTextBox.Text = string.Empty;
		}

		// Token: 0x060005DA RID: 1498 RVA: 0x000314E5 File Offset: 0x000304E5
		private void TypedObjectFiltersEditorButton_Click(object sender, EventArgs e)
		{
			if (this.typedObjectParams != null)
			{
				this.itemFiltes.ShowDialog(this.itemList, this);
			}
		}

		// Token: 0x060005DB RID: 1499 RVA: 0x00031504 File Offset: 0x00030504
		private void AddTypedObjectButton_Click(object sender, EventArgs e)
		{
			if (this.typedObjectParams != null)
			{
				DBItemSource dbItemSource = this.GetActiveDBItemSource();
				if (dbItemSource != null)
				{
					OpenFileDialog openFileDialog = new OpenFileDialog();
					openFileDialog.Filter = "XDB Files|*.xdb|All Files|*.*";
					openFileDialog.RestoreDirectory = true;
					openFileDialog.InitialDirectory = dbItemSource.SearchFolder.Replace('/', '\\');
					if (!Directory.Exists(openFileDialog.InitialDirectory))
					{
						Directory.CreateDirectory(openFileDialog.InitialDirectory);
					}
					openFileDialog.Multiselect = false;
					openFileDialog.CheckFileExists = false;
					if (openFileDialog.ShowDialog(this) == DialogResult.OK)
					{
						bool result = false;
						string typedObjectFileName = string.Empty;
						string fileName = openFileDialog.FileName.Replace('\\', '/');
						if (fileName.Length > EditorEnvironment.DataFolder.Length)
						{
							typedObjectFileName = fileName.Substring(EditorEnvironment.DataFolder.Length);
							IDatabase mainDb = IDatabase.GetMainDatabase();
							if (mainDb != null)
							{
								DBID typedObjectDBID = IDatabase.CreateDBIDByName(typedObjectFileName);
								if (!typedObjectDBID.IsEmpty())
								{
									if (!mainDb.DoesObjectExist(typedObjectDBID))
									{
										IObjMan typedObjectMan = mainDb.CreateNewObject(dbItemSource.SearchClassName);
										if (typedObjectMan != null)
										{
											result = mainDb.AddNewObject(typedObjectDBID, typedObjectMan);
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
							this.CurrentTypedObjectTextBox.Text = typedObjectFileName;
							this.itemList.SelectedItem = typedObjectFileName;
							this.itemList.Refresh();
							this.ShowProperties();
						}
					}
				}
			}
		}

		// Token: 0x060005DC RID: 1500 RVA: 0x0003163E File Offset: 0x0003063E
		private void TypedObjectsListView_MouseDoubleClick(object sender, MouseEventArgs e)
		{
			base.InvokeOnClick(this.okButton, new EventArgs());
		}

		// Token: 0x060005DD RID: 1501 RVA: 0x00031651 File Offset: 0x00030651
		private void CurrentTypedObjectTextBox_TextChanged(object sender, EventArgs e)
		{
			this.UpdateOKButton();
		}

		// Token: 0x060005DE RID: 1502 RVA: 0x00031659 File Offset: 0x00030659
		private void Type0RadioButton_CheckedChanged(object sender, EventArgs e)
		{
			if (this.typedObjectParams != null && this.created && this.Type0RadioButton.Checked && this.typedObjectParams.TypeCount > 0)
			{
				this.typedObjectParams.SelectedTypeIndex = 0;
			}
		}

		// Token: 0x060005DF RID: 1503 RVA: 0x00031692 File Offset: 0x00030692
		private void Type1RadioButton_CheckedChanged(object sender, EventArgs e)
		{
			if (this.typedObjectParams != null && this.created && this.Type1RadioButton.Checked && this.typedObjectParams.TypeCount > 1)
			{
				this.typedObjectParams.SelectedTypeIndex = 1;
			}
		}

		// Token: 0x060005E0 RID: 1504 RVA: 0x000316CB File Offset: 0x000306CB
		private void Type2RadioButton_CheckedChanged(object sender, EventArgs e)
		{
			if (this.typedObjectParams != null && this.created && this.Type2RadioButton.Checked && this.typedObjectParams.TypeCount > 2)
			{
				this.typedObjectParams.SelectedTypeIndex = 2;
			}
		}

		// Token: 0x060005E1 RID: 1505 RVA: 0x00031704 File Offset: 0x00030704
		private void Type3RadioButton_CheckedChanged(object sender, EventArgs e)
		{
			if (this.typedObjectParams != null && this.created && this.Type3RadioButton.Checked && this.typedObjectParams.TypeCount > 3)
			{
				this.typedObjectParams.SelectedTypeIndex = 3;
			}
		}

		// Token: 0x060005E2 RID: 1506 RVA: 0x0003173D File Offset: 0x0003073D
		private void Type4RadioButton_CheckedChanged(object sender, EventArgs e)
		{
			if (this.typedObjectParams != null && this.created && this.Type4RadioButton.Checked && this.typedObjectParams.TypeCount > 4)
			{
				this.typedObjectParams.SelectedTypeIndex = 4;
			}
		}

		// Token: 0x060005E3 RID: 1507 RVA: 0x00031778 File Offset: 0x00030778
		private void ObjectType0RadioButton_CheckedChanged(object sender, EventArgs e)
		{
			if (this.typedObjectParams != null && this.created && this.ObjectType0RadioButton.Checked && this.typedObjectParams.ObjectTypeCount > 0)
			{
				this.typedObjectParams.SelectedObjectTypeIndex = 0;
				this.typedObjectParams.Name = string.Empty;
				this.DestroyItemList();
				this.CreateItemList();
				this.UpdateOKButton();
			}
		}

		// Token: 0x060005E4 RID: 1508 RVA: 0x000317E0 File Offset: 0x000307E0
		private void ObjectType1RadioButton_CheckedChanged(object sender, EventArgs e)
		{
			if (this.typedObjectParams != null && this.created && this.ObjectType1RadioButton.Checked && this.typedObjectParams.ObjectTypeCount > 1)
			{
				this.typedObjectParams.SelectedObjectTypeIndex = 1;
				this.typedObjectParams.Name = string.Empty;
				this.DestroyItemList();
				this.CreateItemList();
				this.UpdateOKButton();
			}
		}

		// Token: 0x060005E5 RID: 1509 RVA: 0x00031848 File Offset: 0x00030848
		private void ObjectType2RadioButton_CheckedChanged(object sender, EventArgs e)
		{
			if (this.typedObjectParams != null && this.created && this.ObjectType2RadioButton.Checked && this.typedObjectParams.ObjectTypeCount > 2)
			{
				this.typedObjectParams.SelectedObjectTypeIndex = 2;
				this.typedObjectParams.Name = string.Empty;
				this.DestroyItemList();
				this.CreateItemList();
				this.UpdateOKButton();
			}
		}

		// Token: 0x060005E6 RID: 1510 RVA: 0x000318B0 File Offset: 0x000308B0
		private void ObjectType3RadioButton_CheckedChanged(object sender, EventArgs e)
		{
			if (this.typedObjectParams != null && this.created && this.ObjectType3RadioButton.Checked && this.typedObjectParams.ObjectTypeCount > 3)
			{
				this.typedObjectParams.SelectedObjectTypeIndex = 3;
				this.typedObjectParams.Name = string.Empty;
				this.DestroyItemList();
				this.CreateItemList();
				this.UpdateOKButton();
			}
		}

		// Token: 0x060005E7 RID: 1511 RVA: 0x00031918 File Offset: 0x00030918
		public TypedObjectsBrowserForm(TypedObjectParams _typedObjectParams, ItemDataContainer itemDataContainer, string configFileName, string typedObjectsItemListConfigFileName, string typedObjectsFiltersConfigFileName)
		{
			this.InitializeComponent();
			this.typedObjectParams = _typedObjectParams;
			foreach (DBItemSource dbItemSource in this.typedObjectParams.DBItemSources)
			{
				Directory.CreateDirectory(dbItemSource.SearchFolder);
			}
			this.paramsSaver = new FormParamsSaver(this, configFileName, false);
			this.paramsSaver.AutoregisterControls = false;
			this.paramsSaver.LoadParams += this.OnLoadParams;
			this.paramsSaver.SaveParams += this.OnSaveParams;
			this.itemList = new ItemList(typedObjectsItemListConfigFileName, itemDataContainer, true);
			ItemList itemList = this.itemList;
			itemList.ItemSelected = (ItemList.ItemEvent)Delegate.Combine(itemList.ItemSelected, new ItemList.ItemEvent(this.OnItemSelected));
			this.itemFiltes = new FolderItemFilters(typedObjectsFiltersConfigFileName, EditorEnvironment.EditorFormsFolder);
		}

		// Token: 0x0400045F RID: 1119
		private readonly FormParamsSaver paramsSaver;

		// Token: 0x04000460 RID: 1120
		private readonly ItemList itemList;

		// Token: 0x04000461 RID: 1121
		private readonly FolderItemFilters itemFiltes;

		// Token: 0x04000462 RID: 1122
		private readonly TypedObjectParams typedObjectParams;

		// Token: 0x04000463 RID: 1123
		private bool created;
	}
}
