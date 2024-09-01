using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Windows.Forms;
using Db;
using MapEditor.Forms.Base;
using MapEditor.Map;
using MapEditor.Resources.Strings;
using Tools.ControlContainer;
using Tools.ControlValidators;
using Tools.DBGameObjects;
using Tools.WindowParams;

namespace MapEditor.Forms.Quests.QuickObjectGenerator
{
	// Token: 0x0200009C RID: 156
	public partial class CreateDeviceForm : BaseForm
	{
		// Token: 0x0600073A RID: 1850 RVA: 0x00038B08 File Offset: 0x00037B08
		private bool CheckValues()
		{
			foreach (object obj in this.controlContainer)
			{
				Control control = (Control)obj;
				if (control != null && control.Enabled && (control.GetType() == typeof(TextBox) || control.GetType() == typeof(ComboBox)) && control != this.ExploitingTextBox && string.IsNullOrEmpty(control.Text))
				{
					MessageBox.Show(Strings.QUEST_EDITOR_MANDATORYFIELDERROR_MESSAGE, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Hand);
					return false;
				}
			}
			return this.visObjValidator.ValidateText();
		}

		// Token: 0x0600073B RID: 1851 RVA: 0x00038BCC File Offset: 0x00037BCC
		private string GetResourceDBID(string shortTypeName)
		{
			return string.Format("{0}{1}/{2}/{2}.({3}).xdb", new object[]
			{
				"Items/InteractiveObjects/",
				this.ZoneComboBox.Text,
				this.FileNameTextBox.Text,
				shortTypeName
			});
		}

		// Token: 0x0600073C RID: 1852 RVA: 0x00038C14 File Offset: 0x00037C14
		private static string GetExplotDBID(DBID resource)
		{
			string[] path = resource.ToString().Split(new char[]
			{
				'.'
			});
			if (path.Length > 0)
			{
				return string.Format("{0}Exploit.({1}).xdb", path[0], "Exploit");
			}
			return string.Empty;
		}

		// Token: 0x0600073D RID: 1853 RVA: 0x00038C58 File Offset: 0x00037C58
		private string GetSpTableDBID()
		{
			if (this.NewSTRadioButton.Checked)
			{
				return ObjectGeneratorConfig.GetSpawnTableDBID(this.MapComboBox.Text, this.ZoneComboBox.Text, this.FileNameTextBox.Text);
			}
			return this.SpawnTableTextBox.Text;
		}

		// Token: 0x0600073E RID: 1854 RVA: 0x00038CA4 File Offset: 0x00037CA4
		private void OnCreateClick(object sender, EventArgs e)
		{
			this.ResultTextBox.Clear();
			IDatabase mainDb = IDatabase.GetMainDatabase();
			if (mainDb == null)
			{
				return;
			}
			if (!this.CheckValues())
			{
				return;
			}
			ObjectGeneratorConfig.ResourceGeneratorConfig config = null;
			if (File.Exists(ObjectGeneratorConfig.resourceConfigPath))
			{
				config = Serializer.Load<ObjectGeneratorConfig.ResourceGeneratorConfig>(ObjectGeneratorConfig.resourceConfigPath);
			}
			if (config == null)
			{
				MessageBox.Show(string.Format(Strings.QUEST_EDITOR_CANT_FIND_ERROR, ObjectGeneratorConfig.resourceConfigPath), Strings.QUEST_EDITOR_TITLE, MessageBoxButtons.OK, MessageBoxIcon.Hand);
				return;
			}
			int prepareDuration = config.PrepareDuration;
			long corpseDuration = config.CorpseDuration;
			bool chest = this.ChestRadioButton.Checked;
			string resType = chest ? "gameMechanics.world.device.ChestResource" : "gameMechanics.world.device.SteleResource";
			string shortResType = chest ? "ChestResource" : "SteleResource";
			this.Cursor = Cursors.WaitCursor;
			DBID resDBID = IDatabase.CreateDBIDByName(this.GetResourceDBID(shortResType));
			DBID exploitDBID = IDatabase.CreateDBIDByName(CreateDeviceForm.GetExplotDBID(resDBID));
			DBID spTableDBID = IDatabase.CreateDBIDByName(this.GetSpTableDBID());
			Dictionary<DBID, string> dbidDict = new Dictionary<DBID, string>(3);
			Dictionary<DBID, IObjMan> objDict = new Dictionary<DBID, IObjMan>(3);
			dbidDict.Add(resDBID, resType);
			dbidDict.Add(exploitDBID, "gameMechanics.constructor.schemes.exploit.Exploit");
			if (this.NewSTRadioButton.Checked)
			{
				dbidDict.Add(spTableDBID, "gameMechanics.map.spawn.SpawnTable");
			}
			if (!ObjectGeneratorConfig.CreateNewObjects(this, mainDb, dbidDict, ref objDict, 1))
			{
				this.Cursor = Cursors.Default;
				return;
			}
			IObjMan resMan;
			objDict.TryGetValue(resDBID, out resMan);
			if (resMan != null)
			{
				DBMethods.SetTextValue(resMan, "name", this.ResNameTextBox.Text, this.FileNameTextBox.Text + "_Name.txt", false);
				DBMethods.SetTextValue(resMan, "exploitingText", this.ExploitingTextBox.Text, this.FileNameTextBox.Text + "_ExploitingText.txt", false);
				resMan.SetValue("visObj", this.VisObjTextBox.Text);
				resMan.SetValue("exploit", exploitDBID);
				resMan.SetValue("corpseDuration", (float)corpseDuration);
				DBID collisionDBID = this.GetCollisionDBID();
				if (!DBID.IsNullOrEmpty(collisionDBID))
				{
					resMan.SetValue("collision", collisionDBID);
				}
			}
			IObjMan exploitMan;
			objDict.TryGetValue(exploitDBID, out exploitMan);
			if (exploitMan != null)
			{
				DBID proptotypeDBID = mainDb.GetDBIDByName("Items/InteractiveObjects/Templates/Exploit.xdb");
				if (!DBID.IsNullOrEmpty(proptotypeDBID))
				{
					exploitMan.PrototypesDBID = proptotypeDBID;
				}
				exploitMan.Insert("targetImpacts", 0, 1, true);
				exploitMan.SetStructPtrInstance("targetImpacts.[0]", "gameMechanics.elements.device.DeviceDie");
				if (chest)
				{
					exploitMan.Insert("targetImpacts", 1, 1, true);
					exploitMan.SetStructPtrInstance("targetImpacts.[1]", "gameMechanics.elements.device.DeviceOpenLootBag");
				}
				exploitMan.SetValue("prepareDuration", prepareDuration);
				exploitMan.SetValue("isQuestOnly", true);
			}
			IObjMan spTableMan;
			if (objDict.TryGetValue(spTableDBID, out spTableMan))
			{
				spTableMan = mainDb.GetManipulator(spTableDBID);
				if (spTableMan != null)
				{
					ObjectGeneratorConfig.AddToSpawbTable(spTableMan, resDBID, "commons", "gameMechanics.elements.spawn.TimeCommon");
				}
			}
			mainDb.SaveChanges();
			this.Cursor = Cursors.Default;
			int resCnt = 2;
			string result = string.Format("{0}\n{1}\n", resDBID, exploitDBID);
			if (this.NewSTRadioButton.Checked)
			{
				resCnt++;
				result += string.Format("{0}\n", spTableDBID);
				this.SpawnTableTextBox.Text = spTableDBID.ToString();
			}
			MessageBox.Show(string.Format(Strings.QUEST_EDITOR_OBJECTS_CREATED_MESSAGE, resCnt), Strings.QUEST_EDITOR_TITLE, MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
			this.ResultTextBox.AppendText(result);
			this.createdDevice = resDBID.ToString();
		}

		// Token: 0x0600073F RID: 1855 RVA: 0x00038FF0 File Offset: 0x00037FF0
		private DBID GetCollisionDBID()
		{
			string visObj = this.VisObjTextBox.Text;
			if (this.visObjValidator.ValidateText(false))
			{
				string[] pathArr = visObj.Split(new char[]
				{
					'.'
				});
				if (pathArr.Length > 0)
				{
					string collision = string.Format("{0}.({1}).xdb", pathArr[0], "Collision");
					IDatabase mainDb = IDatabase.GetMainDatabase();
					if (mainDb != null)
					{
						DBID collisionDBID = mainDb.GetDBIDByName(collision);
						if (!DBID.IsNullOrEmpty(collisionDBID) && mainDb.DoesObjectExist(collisionDBID))
						{
							return collisionDBID;
						}
					}
				}
			}
			return null;
		}

		// Token: 0x06000740 RID: 1856 RVA: 0x00039070 File Offset: 0x00038070
		private void OnSelectDBIDClick(object sender, EventArgs e)
		{
			TextBox textBox;
			DBIDTextValidator validator;
			string filter;
			if (sender == this.ChooseVisObjButton)
			{
				textBox = this.VisObjTextBox;
				validator = this.visObjValidator;
				filter = string.Format(".({0}).xdb files|*.({0}).xdb|.xdb files|*.xdb", "VisObjectTemplate");
			}
			else
			{
				if (sender != this.ChooseSpawnTablebButton)
				{
					return;
				}
				textBox = this.SpawnTableTextBox;
				validator = this.spTableValidator;
				filter = string.Format(".({0}).xdb files|*.({0}).xdb|.xdb files|*.xdb", "gameMechanics.map.spawn.SpawnTable");
			}
			OpenFileDialog openDialog = new OpenFileDialog();
			openDialog.Filter = filter;
			IDatabase mainDb = IDatabase.GetMainDatabase();
			if (mainDb != null && validator.ValidateText(false))
			{
				DBID dbid = mainDb.GetDBIDByName(textBox.Text);
				if (!DBID.IsNullOrEmpty(dbid))
				{
					string initDir = dbid.GetFileFolder(EditorEnvironment.DataFolder);
					DirectoryInfo initDirInfo = new DirectoryInfo(initDir);
					if (initDirInfo.Exists)
					{
						openDialog.InitialDirectory = initDirInfo.FullName;
					}
				}
			}
			openDialog.RestoreDirectory = true;
			openDialog.Multiselect = false;
			if (openDialog.ShowDialog(this) == DialogResult.OK)
			{
				textBox.Text = openDialog.FileName;
				if (!string.IsNullOrEmpty(textBox.Text))
				{
					validator.ValidateText();
				}
			}
		}

		// Token: 0x06000741 RID: 1857 RVA: 0x00039170 File Offset: 0x00038170
		private void OnVisObjTimer_Tick(object sender, EventArgs e)
		{
			this.visObjTimer.Stop();
			this.CollisionLabel.Text = string.Empty;
			DBID collision = this.GetCollisionDBID();
			if (!DBID.IsNullOrEmpty(collision))
			{
				this.CollisionLabel.Text = collision.ToString();
			}
		}

		// Token: 0x06000742 RID: 1858 RVA: 0x000391B8 File Offset: 0x000381B8
		private void OnVisObjChanged(object sender, EventArgs e)
		{
			this.visObjTimer.Stop();
			this.visObjTimer.Start();
		}

		// Token: 0x06000743 RID: 1859 RVA: 0x000391D0 File Offset: 0x000381D0
		private void OnLoadParams(FormParams formParams)
		{
			QuestEnvironment.LoadZones(this.ZoneComboBox, false);
			List<string> maps = new List<string>();
			Constants.GetContinentNameList(ref maps);
			foreach (string map in maps)
			{
				this.MapComboBox.Items.Add(map);
			}
			if (base.ParamsSaver != null)
			{
				base.ParamsSaver.LoadParams -= this.OnLoadParams;
			}
		}

		// Token: 0x06000744 RID: 1860 RVA: 0x00039264 File Offset: 0x00038264
		private void OnSTCheckedChanged(object sender, EventArgs e)
		{
			this.SpawnTablePanel.Enabled = !this.NewSTRadioButton.Checked;
			this.SpawnTableMapPanel.Enabled = this.NewSTRadioButton.Checked;
		}

		// Token: 0x06000745 RID: 1861 RVA: 0x00039298 File Offset: 0x00038298
		public CreateDeviceForm(MainForm.Context context) : base(EditorEnvironment.EditorFormsFolder + "CreateResourceForm.xml", context)
		{
			this.InitializeComponent();
			base.AllowClose = true;
			this.controlContainer = new ControlContainer(this);
			this.ChooseVisObjButton.Click += this.OnSelectDBIDClick;
			this.VisObjTextBox.TextChanged += this.OnVisObjChanged;
			this.visObjTimer.Tick += this.OnVisObjTimer_Tick;
			this.NewSTRadioButton.CheckedChanged += this.OnSTCheckedChanged;
			this.CreateButton.Click += this.OnCreateClick;
			this.ChooseSpawnTablebButton.Click += this.OnSelectDBIDClick;
			this.visObjValidator = new DBIDTextValidator("VisObjectTemplate", this.VisObjTextBox, string.Format(Strings.QUEST_EDITOR_WRONG_VIS_MOB_ERROR, "VisObjectTemplate"), Strings.QUEST_EDITOR_ERROR_TITLE);
			this.spTableValidator = new DBIDTextValidator("gameMechanics.map.spawn.SpawnTable", this.SpawnTableTextBox, string.Format(Strings.QUEST_EDITOR_WRONG_VIS_MOB_ERROR, "gameMechanics.map.spawn.SpawnTable"), Strings.QUEST_EDITOR_ERROR_TITLE);
			if (base.ParamsSaver != null)
			{
				base.ParamsSaver.RegisterControl(new RadioButton[]
				{
					this.ChestRadioButton,
					this.SteleRadioButton
				});
				base.ParamsSaver.RegisterControl(new RadioButton[]
				{
					this.NewSTRadioButton,
					this.ExistingSTRadioButton
				});
				base.ParamsSaver.RegisterControlContainer(this.SpawnTablePanel);
				base.ParamsSaver.RegisterControlContainer(this.SpawnTableMapPanel);
				base.ParamsSaver.LoadParams += this.OnLoadParams;
			}
		}

		// Token: 0x1700011B RID: 283
		// (get) Token: 0x06000746 RID: 1862 RVA: 0x00039440 File Offset: 0x00038440
		public string CreatedDevice
		{
			get
			{
				return this.createdDevice;
			}
		}

		// Token: 0x04000537 RID: 1335
		private const string resourcePath = "Items/InteractiveObjects/";

		// Token: 0x04000538 RID: 1336
		private const string exploitPrototypePath = "Items/InteractiveObjects/Templates/Exploit.xdb";

		// Token: 0x04000558 RID: 1368
		private readonly DBIDTextValidator visObjValidator;

		// Token: 0x04000559 RID: 1369
		private readonly DBIDTextValidator spTableValidator;

		// Token: 0x0400055A RID: 1370
		private readonly ControlContainer controlContainer;

		// Token: 0x0400055B RID: 1371
		private string createdDevice;
	}
}
