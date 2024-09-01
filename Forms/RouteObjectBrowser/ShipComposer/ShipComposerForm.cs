using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using Db;
using MapEditor.Properties;
using Tools.SafeObjMan;
using Tools.WindowParams;

namespace MapEditor.Forms.RouteObjectBrowser.ShipComposer
{
	// Token: 0x02000112 RID: 274
	public partial class ShipComposerForm : Form
	{
		// Token: 0x06000D79 RID: 3449 RVA: 0x0007107C File Offset: 0x0007007C
		private void ClearListView()
		{
			this.ShipListView.Items.Clear();
			this.ShipPropertyControl.SelectedObject = null;
			this.shipMan = null;
		}

		// Token: 0x06000D7A RID: 3450 RVA: 0x000710A4 File Offset: 0x000700A4
		private void FillShipListView()
		{
			IDatabase mainDb = IDatabase.GetMainDatabase();
			if (mainDb != null)
			{
				DBID objectDBID = IDatabase.CreateDBIDByName(this.ship);
				if (!objectDBID.IsEmpty())
				{
					this.shipMan = mainDb.GetManipulator(objectDBID);
					this.ShipPropertyControl.SelectedObject = this.shipMan;
					Dictionary<string, string> existingSlots = new Dictionary<string, string>();
					DBID equipmentDBID = mainDb.GetDBIDByName(SafeObjMan.GetDBID(this.shipMan, "equipmentList"));
					if (!DBID.IsNullOrEmpty(equipmentDBID))
					{
						IObjMan equipmentMan = mainDb.GetManipulator(equipmentDBID);
						if (equipmentMan != null)
						{
							int slots = SafeObjMan.GetInt(equipmentMan, "slotDevices");
							for (int index = 0; index < slots; index++)
							{
								string slotPrefix = string.Format("slotDevices.[{0}].", index);
								string slot = SafeObjMan.GetString(equipmentMan, slotPrefix + "slot");
								string device = SafeObjMan.GetDBID(equipmentMan, slotPrefix + "device");
								if (!string.IsNullOrEmpty(slot) && !string.IsNullOrEmpty(device))
								{
									existingSlots[slot] = device;
								}
							}
						}
					}
					DBID visualShipDBID = mainDb.GetDBIDByName(SafeObjMan.GetDBID(this.shipMan, "visualShip"));
					if (!DBID.IsNullOrEmpty(visualShipDBID))
					{
						IObjMan visualShipMan = mainDb.GetManipulator(visualShipDBID);
						if (visualShipMan != null)
						{
							int slots2 = SafeObjMan.GetInt(visualShipMan, "slots");
							for (int index2 = 0; index2 < slots2; index2++)
							{
								string slotPrefix2 = string.Format("slots.[{0}].", index2);
								string slot2 = SafeObjMan.GetString(visualShipMan, slotPrefix2 + "name");
								ListViewItem listViewItem = new ListViewItem();
								listViewItem.Text = slot2;
								listViewItem.Tag = slot2;
								string device2;
								if (!existingSlots.TryGetValue(slot2, out device2))
								{
									device2 = string.Empty;
								}
								listViewItem.SubItems.Add(new ListViewItem.ListViewSubItem(listViewItem, device2));
								this.ShipListView.Items.Add(listViewItem);
							}
						}
					}
				}
			}
		}

		// Token: 0x06000D7B RID: 3451 RVA: 0x00071278 File Offset: 0x00070278
		private bool UpdateShipMan(string slot, string device)
		{
			if (this.created)
			{
				this.created = false;
				IDatabase mainDb = IDatabase.GetMainDatabase();
				if (mainDb != null)
				{
					DBID equipmentDBID = mainDb.GetDBIDByName(SafeObjMan.GetDBID(this.shipMan, "equipmentList"));
					if (!DBID.IsNullOrEmpty(equipmentDBID))
					{
						IObjMan equipmentMan = mainDb.GetManipulator(equipmentDBID);
						if (equipmentMan != null)
						{
							int slots = SafeObjMan.GetInt(equipmentMan, "slotDevices");
							if (!string.IsNullOrEmpty(device))
							{
								string slotPrefix;
								string deviceCheck;
								for (int index = 0; index < slots; index++)
								{
									slotPrefix = string.Format("slotDevices.[{0}].", index);
									string _slot = SafeObjMan.GetString(equipmentMan, slotPrefix + "slot");
									if (string.Equals(_slot, slot))
									{
										SafeObjMan.SetDBID(equipmentMan, slotPrefix + "device", device);
										deviceCheck = SafeObjMan.GetDBID(equipmentMan, slotPrefix + "device");
										this.ShipPropertyControl.Update();
										this.created = true;
										return string.Compare(deviceCheck, device, StringComparison.OrdinalIgnoreCase) == 0;
									}
								}
								equipmentMan.Insert("slotDevices", slots);
								slotPrefix = string.Format("slotDevices.[{0}].", slots);
								SafeObjMan.SetStringOnlyModified(equipmentMan, slotPrefix + "slot", slot);
								SafeObjMan.SetDBID(equipmentMan, slotPrefix + "device", device);
								deviceCheck = SafeObjMan.GetDBID(equipmentMan, slotPrefix + "device");
								this.ShipPropertyControl.Update();
								this.created = true;
								return string.Compare(deviceCheck, device, StringComparison.OrdinalIgnoreCase) == 0;
							}
							for (int index2 = 0; index2 < slots; index2++)
							{
								string slotPrefix2 = string.Format("slotDevices.[{0}].", index2);
								string _slot2 = SafeObjMan.GetString(equipmentMan, slotPrefix2 + "slot");
								if (string.Equals(_slot2, slot))
								{
									equipmentMan.Remove("slotDevices", index2);
									this.ShipPropertyControl.Update();
									this.created = true;
									return true;
								}
							}
						}
					}
				}
				this.created = true;
			}
			return false;
		}

		// Token: 0x06000D7C RID: 3452 RVA: 0x0007145C File Offset: 0x0007045C
		private void OnDBObjectChanged(DBID dbid)
		{
			if (this.created && this.shipMan != null)
			{
				IDatabase mainDb = IDatabase.GetMainDatabase();
				bool somethingChanged = false;
				if (mainDb != null && !DBID.IsNullOrEmpty(dbid))
				{
					if (mainDb.GetClassTypeName(dbid) == ShipComposerForm.shipDBType)
					{
						string changedShip = dbid.ToString();
						somethingChanged = (string.Compare(this.ship, changedShip) == 0);
					}
					else if (mainDb.GetClassTypeName(dbid) == ShipComposerForm.equipmentListDBType)
					{
						string equipment = SafeObjMan.GetDBID(this.shipMan, "equipmentList");
						string changedEquipment = dbid.ToString();
						somethingChanged = (string.Compare(equipment, changedEquipment) == 0);
					}
					else if (mainDb.GetClassTypeName(dbid) == ShipComposerForm.visualShipDBType)
					{
						string visualShip = SafeObjMan.GetDBID(this.shipMan, "visualShip");
						string changedVisualShip = dbid.ToString();
						somethingChanged = (string.Compare(visualShip, changedVisualShip) == 0);
					}
				}
				if (somethingChanged)
				{
					this.modified = true;
					this.UpdateTitle();
					this.RefreshShip();
				}
			}
		}

		// Token: 0x06000D7D RID: 3453 RVA: 0x00071550 File Offset: 0x00070550
		private void ShipComposerForm_Load(object sender, EventArgs e)
		{
			this.defaultTitle = this.Text;
			IDatabase mainDb = IDatabase.GetMainDatabase();
			if (mainDb != null)
			{
				this.dbEventsGenerator = new DbEventsGenerator(mainDb);
				this.dbEventsGenerator.DBObjectChanged += this.OnDBObjectChanged;
				this.created = true;
				if (!string.IsNullOrEmpty(this.ship))
				{
					this.LoadShip(this.ship);
				}
			}
			this.UpdateTitle();
			this.UpdateControls();
		}

		// Token: 0x06000D7E RID: 3454 RVA: 0x000715C1 File Offset: 0x000705C1
		private void ShipComposerForm_FormClosed(object sender, FormClosedEventArgs e)
		{
			if (this.dbEventsGenerator != null)
			{
				this.dbEventsGenerator.DBObjectChanged -= this.OnDBObjectChanged;
				this.dbEventsGenerator = null;
			}
		}

		// Token: 0x06000D7F RID: 3455 RVA: 0x000715EC File Offset: 0x000705EC
		private bool SaveShip(bool askForSaveChanges)
		{
			if (this.created)
			{
				if (this.modified)
				{
					IDatabase mainDb = IDatabase.GetMainDatabase();
					if (mainDb != null)
					{
						mainDb.SaveChanges();
					}
					this.modified = false;
					this.UpdateTitle();
					this.UpdateControls();
				}
				return true;
			}
			return false;
		}

		// Token: 0x06000D80 RID: 3456 RVA: 0x00071630 File Offset: 0x00070630
		private void LoadShip(string newShip)
		{
			if (!this.SaveShip(true))
			{
				return;
			}
			if (this.created)
			{
				if (string.IsNullOrEmpty(newShip))
				{
					OpenFileDialog openFileDialog = new OpenFileDialog();
					openFileDialog.Filter = "XDB Files|*.xdb|All Files|*.*";
					openFileDialog.RestoreDirectory = true;
					string initialDirectory = this.paramsSaver.FormParams.GetString(0);
					if (string.IsNullOrEmpty(initialDirectory))
					{
						initialDirectory = EditorEnvironment.DataFolder;
					}
					initialDirectory = initialDirectory.Replace('/', '\\');
					if (!Directory.Exists(initialDirectory))
					{
						Directory.CreateDirectory(initialDirectory);
					}
					openFileDialog.InitialDirectory = initialDirectory;
					openFileDialog.Multiselect = false;
					openFileDialog.CheckFileExists = false;
					if (openFileDialog.ShowDialog(this) == DialogResult.OK)
					{
						string fileName = openFileDialog.FileName.Replace('\\', '/');
						if (fileName.Length > EditorEnvironment.DataFolder.Length)
						{
							initialDirectory = Str.CutFileName(fileName, false);
							this.paramsSaver.FormParams.SetString(0, initialDirectory);
							newShip = fileName.Substring(EditorEnvironment.DataFolder.Length);
							bool fileIsOK = false;
							IDatabase mainDb = IDatabase.GetMainDatabase();
							if (mainDb != null)
							{
								DBID newShipDBID = mainDb.GetDBIDByName(newShip);
								if (!DBID.IsNullOrEmpty(newShipDBID) && mainDb.GetClassTypeName(newShipDBID) == ShipComposerForm.shipDBType)
								{
									fileIsOK = true;
								}
							}
							if (!fileIsOK)
							{
								newShip = string.Empty;
							}
						}
					}
					else
					{
						newShip = string.Empty;
					}
				}
				this.CloseShip();
				this.ship = newShip;
				this.UpdateTitle();
				if (!string.IsNullOrEmpty(this.ship))
				{
					this.FillShipListView();
					this.UpdateControls();
				}
			}
		}

		// Token: 0x06000D81 RID: 3457 RVA: 0x00071795 File Offset: 0x00070795
		private void CloseShip()
		{
			if (!this.SaveShip(true))
			{
				return;
			}
			if (this.created)
			{
				this.ClearListView();
				this.ship = string.Empty;
				this.modified = false;
				this.UpdateTitle();
				this.UpdateControls();
			}
		}

		// Token: 0x06000D82 RID: 3458 RVA: 0x000717CD File Offset: 0x000707CD
		private void RefreshShip()
		{
			if (this.created && !string.IsNullOrEmpty(this.ship))
			{
				this.ClearListView();
				this.FillShipListView();
				this.UpdateControls();
			}
		}

		// Token: 0x06000D83 RID: 3459 RVA: 0x000717F8 File Offset: 0x000707F8
		private void EditSlot()
		{
			if (this.created && !string.IsNullOrEmpty(this.ship) && this.ShipListView.FocusedItem != null)
			{
				OpenFileDialog openFileDialog = new OpenFileDialog();
				openFileDialog.Filter = "XDB Files|*.xdb|All Files|*.*";
				openFileDialog.RestoreDirectory = true;
				string initialDirectory = this.paramsSaver.FormParams.GetString(1);
				if (string.IsNullOrEmpty(initialDirectory))
				{
					initialDirectory = EditorEnvironment.DataFolder;
				}
				initialDirectory = initialDirectory.Replace('/', '\\');
				if (!Directory.Exists(initialDirectory))
				{
					Directory.CreateDirectory(initialDirectory);
				}
				openFileDialog.InitialDirectory = initialDirectory;
				openFileDialog.Multiselect = false;
				openFileDialog.CheckFileExists = false;
				if (openFileDialog.ShowDialog(this) == DialogResult.OK)
				{
					string fileName = openFileDialog.FileName.Replace('\\', '/');
					if (fileName.Length > EditorEnvironment.DataFolder.Length)
					{
						initialDirectory = Str.CutFileName(fileName, false);
						this.paramsSaver.FormParams.SetString(1, initialDirectory);
						string newDevice = fileName.Substring(EditorEnvironment.DataFolder.Length);
						if (this.UpdateShipMan(this.ShipListView.FocusedItem.Text, newDevice) && this.ShipListView.FocusedItem.SubItems.Count > 1)
						{
							this.modified = (string.Compare(this.ShipListView.FocusedItem.SubItems[1].Text, newDevice, StringComparison.OrdinalIgnoreCase) != 0);
							this.ShipListView.FocusedItem.SubItems[1].Text = newDevice;
							this.UpdateTitle();
							this.UpdateControls();
						}
					}
				}
			}
		}

		// Token: 0x06000D84 RID: 3460 RVA: 0x0007197C File Offset: 0x0007097C
		private void ClearSlot()
		{
			if (this.created && !string.IsNullOrEmpty(this.ship) && this.ShipListView.FocusedItem != null && this.UpdateShipMan(this.ShipListView.FocusedItem.Text, string.Empty) && this.ShipListView.FocusedItem.SubItems.Count > 1)
			{
				this.modified = !string.IsNullOrEmpty(this.ShipListView.FocusedItem.SubItems[1].Text);
				this.ShipListView.FocusedItem.SubItems[1].Text = string.Empty;
				this.UpdateTitle();
				this.UpdateControls();
			}
		}

		// Token: 0x06000D85 RID: 3461 RVA: 0x00071A40 File Offset: 0x00070A40
		private void UpdateControls()
		{
			if (this.created)
			{
				bool shipExists = !string.IsNullOrEmpty(this.ship) && this.shipMan != null;
				string equipment = SafeObjMan.GetDBID(this.shipMan, "equipmentList");
				string visualShip = SafeObjMan.GetDBID(this.shipMan, "visualShip");
				bool partsExists = !string.IsNullOrEmpty(equipment) && !string.IsNullOrEmpty(visualShip);
				bool selectionExists = this.ShipListView.FocusedItem != null;
				bool selectionExistsNotEmpty = this.ShipListView.FocusedItem != null && !string.IsNullOrEmpty(this.ShipListView.FocusedItem.SubItems[1].Text);
				this.ShipMenuItemSave.Enabled = (shipExists && this.modified);
				this.ToolStripButtonSave.Enabled = (shipExists && this.modified);
				this.ShipContextMenuStripSave.Enabled = (shipExists && this.modified);
				this.SlotMenuItemEdit.Enabled = (shipExists && partsExists && selectionExists);
				this.ToolStripButtonSlotEdit.Enabled = (shipExists && partsExists && selectionExists);
				this.ShipContextMenuStripEditSlot.Enabled = (shipExists && partsExists && selectionExists);
				this.ShipMenuItemRefresh.Enabled = shipExists;
				this.ToolStripButtonRefresh.Enabled = shipExists;
				this.ShipContextMenuStripRefresh.Enabled = shipExists;
				this.SlotMenuItemClear.Enabled = (shipExists && partsExists && selectionExistsNotEmpty);
				this.ToolStripButtonSlotClear.Enabled = (shipExists && partsExists && selectionExistsNotEmpty);
				this.ShipContextMenuStripClearSlot.Enabled = (shipExists && partsExists && selectionExistsNotEmpty);
			}
		}

		// Token: 0x06000D86 RID: 3462 RVA: 0x00071BE4 File Offset: 0x00070BE4
		private void UpdateTitle()
		{
			if (string.IsNullOrEmpty(this.ship))
			{
				this.Text = this.defaultTitle;
				return;
			}
			if (this.modified)
			{
				this.Text = this.defaultTitle + " - " + this.ship + " *";
				return;
			}
			this.Text = this.defaultTitle + " - " + this.ship;
		}

		// Token: 0x06000D87 RID: 3463 RVA: 0x00071C51 File Offset: 0x00070C51
		private void ShipMenuItem_DropDownOpening(object sender, EventArgs e)
		{
			this.UpdateControls();
		}

		// Token: 0x06000D88 RID: 3464 RVA: 0x00071C59 File Offset: 0x00070C59
		private void SlotMenuItem_DropDownOpening(object sender, EventArgs e)
		{
			this.UpdateControls();
		}

		// Token: 0x06000D89 RID: 3465 RVA: 0x00071C61 File Offset: 0x00070C61
		private void ShipContextMenuStrip_Opening(object sender, CancelEventArgs e)
		{
			this.UpdateControls();
		}

		// Token: 0x06000D8A RID: 3466 RVA: 0x00071C69 File Offset: 0x00070C69
		private void ShipListView_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
		{
			this.UpdateControls();
		}

		// Token: 0x06000D8B RID: 3467 RVA: 0x00071C71 File Offset: 0x00070C71
		private void ShipMenuItemLoad_Click(object sender, EventArgs e)
		{
			this.LoadShip(string.Empty);
		}

		// Token: 0x06000D8C RID: 3468 RVA: 0x00071C7E File Offset: 0x00070C7E
		private void ToolStripButtonLoad_Click(object sender, EventArgs e)
		{
			this.LoadShip(string.Empty);
		}

		// Token: 0x06000D8D RID: 3469 RVA: 0x00071C8B File Offset: 0x00070C8B
		private void ShipMenuItemSave_Click(object sender, EventArgs e)
		{
			this.SaveShip(false);
		}

		// Token: 0x06000D8E RID: 3470 RVA: 0x00071C95 File Offset: 0x00070C95
		private void ToolStripButtonSave_Click(object sender, EventArgs e)
		{
			this.SaveShip(false);
		}

		// Token: 0x06000D8F RID: 3471 RVA: 0x00071C9F File Offset: 0x00070C9F
		private void ShipContextMenuStripSave_Click(object sender, EventArgs e)
		{
			this.SaveShip(false);
		}

		// Token: 0x06000D90 RID: 3472 RVA: 0x00071CA9 File Offset: 0x00070CA9
		private void ShipMenuItemRefresh_Click(object sender, EventArgs e)
		{
			this.RefreshShip();
		}

		// Token: 0x06000D91 RID: 3473 RVA: 0x00071CB1 File Offset: 0x00070CB1
		private void ToolStripButtonRefresh_Click(object sender, EventArgs e)
		{
			this.RefreshShip();
		}

		// Token: 0x06000D92 RID: 3474 RVA: 0x00071CB9 File Offset: 0x00070CB9
		private void ShipContextMenuStripRefresh_Click(object sender, EventArgs e)
		{
			this.RefreshShip();
		}

		// Token: 0x06000D93 RID: 3475 RVA: 0x00071CC1 File Offset: 0x00070CC1
		private void ShipMenuItemClose_Click(object sender, EventArgs e)
		{
			if (this.SaveShip(true))
			{
				base.Close();
			}
		}

		// Token: 0x06000D94 RID: 3476 RVA: 0x00071CD2 File Offset: 0x00070CD2
		private void SlotMenuItemEdit_Click(object sender, EventArgs e)
		{
			this.EditSlot();
		}

		// Token: 0x06000D95 RID: 3477 RVA: 0x00071CDA File Offset: 0x00070CDA
		private void ToolStripButtonSlotEdit_Click(object sender, EventArgs e)
		{
			this.EditSlot();
		}

		// Token: 0x06000D96 RID: 3478 RVA: 0x00071CE2 File Offset: 0x00070CE2
		private void ShipContextMenuStripEditSlot_Click(object sender, EventArgs e)
		{
			this.EditSlot();
		}

		// Token: 0x06000D97 RID: 3479 RVA: 0x00071CEA File Offset: 0x00070CEA
		private void ShipListView_MouseDoubleClick(object sender, MouseEventArgs e)
		{
			if (this.ToolStripButtonSlotEdit.Enabled)
			{
				this.EditSlot();
			}
		}

		// Token: 0x06000D98 RID: 3480 RVA: 0x00071CFF File Offset: 0x00070CFF
		private void SlotMenuItemClear_Click(object sender, EventArgs e)
		{
			this.ClearSlot();
		}

		// Token: 0x06000D99 RID: 3481 RVA: 0x00071D07 File Offset: 0x00070D07
		private void ToolStripButtonSlotClear_Click(object sender, EventArgs e)
		{
			this.ClearSlot();
		}

		// Token: 0x06000D9A RID: 3482 RVA: 0x00071D0F File Offset: 0x00070D0F
		private void ShipContextMenuStripClearSlot_Click(object sender, EventArgs e)
		{
			this.ClearSlot();
		}

		// Token: 0x06000D9B RID: 3483 RVA: 0x00071D17 File Offset: 0x00070D17
		private void ShipListView_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Delete && this.ToolStripButtonSlotClear.Enabled)
			{
				this.ClearSlot();
			}
		}

		// Token: 0x06000D9C RID: 3484 RVA: 0x00071D38 File Offset: 0x00070D38
		public ShipComposerForm()
		{
			this.InitializeComponent();
			this.paramsSaver = new FormParamsSaver(this, EditorEnvironment.EditorFormsFolder + "ShipComposerForm.xml", false);
			this.paramsSaver.AutoregisterControls = false;
			this.paramsSaver.RegisterControl(this.splitContainer);
			this.paramsSaver.RegisterControl(this.ShipListView);
		}

		// Token: 0x17000278 RID: 632
		// (get) Token: 0x06000D9D RID: 3485 RVA: 0x00071DB1 File Offset: 0x00070DB1
		// (set) Token: 0x06000D9E RID: 3486 RVA: 0x00071DB9 File Offset: 0x00070DB9
		public string Ship
		{
			get
			{
				return this.ship;
			}
			set
			{
				if (!this.created)
				{
					this.ship = value;
					return;
				}
				if (!this.SaveShip(true))
				{
					return;
				}
				if (string.IsNullOrEmpty(value))
				{
					this.LoadShip(value);
					return;
				}
				this.CloseShip();
			}
		}

		// Token: 0x04000ACF RID: 2767
		private static readonly string shipDBType = "gameMechanics.world.ship.ShipResource";

		// Token: 0x04000AD0 RID: 2768
		private static readonly string equipmentListDBType = "gameMechanics.world.ship.device.EquipmentList";

		// Token: 0x04000AD1 RID: 2769
		private static readonly string visualShipDBType = "gameMechanics.world.ship.VisualShip";

		// Token: 0x04000AD2 RID: 2770
		private readonly FormParamsSaver paramsSaver;

		// Token: 0x04000AD3 RID: 2771
		private bool created;

		// Token: 0x04000AD4 RID: 2772
		private bool modified;

		// Token: 0x04000AD5 RID: 2773
		private string ship = string.Empty;

		// Token: 0x04000AD6 RID: 2774
		private IObjMan shipMan;

		// Token: 0x04000AD7 RID: 2775
		private string defaultTitle = string.Empty;

		// Token: 0x04000AD8 RID: 2776
		private DbEventsGenerator dbEventsGenerator;
	}
}
