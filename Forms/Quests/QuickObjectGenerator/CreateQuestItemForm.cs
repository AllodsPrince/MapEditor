using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Text;
using System.Windows.Forms;
using Db;
using MapEditor.Forms.Base;
using MapEditor.Forms.Quests.QuickObjectGenerator.DialogForms;
using MapEditor.Resources.Strings;
using Tools.ControlValidators;
using Tools.DBGameObjects;
using Tools.WindowParams;

namespace MapEditor.Forms.Quests.QuickObjectGenerator
{
	// Token: 0x020001E5 RID: 485
	public partial class CreateQuestItemForm : BaseForm
	{
		// Token: 0x170005F3 RID: 1523
		// (get) Token: 0x06001875 RID: 6261 RVA: 0x000A285D File Offset: 0x000A185D
		private CreateQuestItemForm.FormMode Mode
		{
			get
			{
				if (!this.FileNameTextBox.Enabled)
				{
					return CreateQuestItemForm.FormMode.Edit;
				}
				return CreateQuestItemForm.FormMode.Create;
			}
		}

		// Token: 0x06001876 RID: 6262 RVA: 0x000A2870 File Offset: 0x000A1870
		private bool CheckValues()
		{
			foreach (object obj in base.Controls)
			{
				Control control = (Control)obj;
				if (control != null && (control == this.IconLabel || control.GetType() == typeof(TextBox) || control.GetType() == typeof(ComboBox)) && control.Enabled && control != this.ItemDescTextBox && string.IsNullOrEmpty(control.Text))
				{
					MessageBox.Show(Strings.QUEST_EDITOR_MANDATORYFIELDERROR_MESSAGE, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Hand);
					return false;
				}
			}
			return this.stackLimitValidator.ValidateText() && this.sellPriceValidator.ValidateText() && this.buyPriceValidator.ValidateText();
		}

		// Token: 0x06001877 RID: 6263 RVA: 0x000A2954 File Offset: 0x000A1954
		private string GetItemDBID()
		{
			return string.Format("{0}{1}/{2}/{2}.({3}).xdb", new object[]
			{
				"Items/QuestItems/",
				this.ZoneComboBox.Text,
				this.FileNameTextBox.Text,
				"ItemResource"
			});
		}

		// Token: 0x06001878 RID: 6264 RVA: 0x000A29A0 File Offset: 0x000A19A0
		private bool Create(out IObjMan itemMan)
		{
			itemMan = null;
			ObjectGeneratorConfig.QuestItemGeneratorConfig config = null;
			if (File.Exists(ObjectGeneratorConfig.resourceConfigPath))
			{
				config = Serializer.Load<ObjectGeneratorConfig.QuestItemGeneratorConfig>(ObjectGeneratorConfig.questItemConfigPath);
			}
			if (config == null)
			{
				MessageBox.Show(string.Format(Strings.QUEST_EDITOR_CANT_FIND_ERROR, ObjectGeneratorConfig.questItemConfigPath), Strings.QUEST_EDITOR_TITLE, MessageBoxButtons.OK, MessageBoxIcon.Hand);
				return false;
			}
			int budgetUsage = config.BudgetUsage;
			string quality = config.Quality;
			string itemClass = config.ItemClass;
			this.Cursor = Cursors.WaitCursor;
			DBID itemDBID = IDatabase.CreateDBIDByName(this.GetItemDBID());
			Dictionary<DBID, string> dbidDict = new Dictionary<DBID, string>(1);
			Dictionary<DBID, IObjMan> objDict = new Dictionary<DBID, IObjMan>(1);
			dbidDict.Add(itemDBID, "gameMechanics.constructor.schemes.item.ItemResource");
			if (!ObjectGeneratorConfig.CreateNewObjects(this, CreateQuestItemForm.mainDb, dbidDict, ref objDict, 1))
			{
				this.Cursor = Cursors.Default;
				return false;
			}
			objDict.TryGetValue(itemDBID, out itemMan);
			if (itemMan != null)
			{
				itemMan.SetValue("budgetUsage", budgetUsage);
				itemMan.SetValue("quality", quality);
				itemMan.SetValue("itemClass", itemClass);
				itemMan.SetValue("customSellPrice", true);
				itemMan.SetValue("binding", "BindOnPickup");
				itemMan.SetValue("isQuestRelated", true);
				itemMan.SetValue("category", "Mechanics/ItemCategories/Quest.xdb");
				return true;
			}
			return false;
		}

		// Token: 0x06001879 RID: 6265 RVA: 0x000A2ACC File Offset: 0x000A1ACC
		private void Edit(IObjMan itemMan)
		{
			if (itemMan != null)
			{
				DBMethods.SetTextValue(itemMan, "name", this.ItemNameTextBox.Text, itemMan.DBID.GetFileShortName() + "_Name.txt", false);
				DBMethods.SetTextValue(itemMan, "description", this.ItemDescTextBox.Text, itemMan.DBID.GetFileShortName() + "_Description.txt", false);
				itemMan.SetValue("stackLimit", int.Parse(this.StackLimitTextBox.Text));
				itemMan.SetValue("sellPrice", int.Parse(this.SellPriceTextBox.Text));
				itemMan.SetValue("buyPrice", int.Parse(this.BuyPriceTextBox.Text));
				DBID iconDBID = IDatabase.CreateDBIDByName(this.IconLabel.Text);
				if (!DBID.IsNullOrEmpty(iconDBID) && CreateQuestItemForm.mainDb.DoesObjectExist(iconDBID))
				{
					itemMan.SetValue("image", iconDBID);
				}
			}
		}

		// Token: 0x0600187A RID: 6266 RVA: 0x000A2BBC File Offset: 0x000A1BBC
		private void OnProcessClick(object sender, EventArgs e)
		{
			this.ResultTextBox.Clear();
			if (!this.CheckValues())
			{
				return;
			}
			IObjMan itemMan = null;
			if (this.Mode == CreateQuestItemForm.FormMode.Create)
			{
				if (!this.Create(out itemMan))
				{
					return;
				}
			}
			else
			{
				DBID itemDBID = CreateQuestItemForm.mainDb.GetDBIDByName(this.FileNameTextBox.Text);
				if (!DBID.IsNullOrEmpty(itemDBID))
				{
					itemMan = CreateQuestItemForm.mainDb.GetManipulator(itemDBID);
				}
				if (itemMan == null)
				{
					MessageBox.Show(string.Format(Strings.QUEST_EDITOR_CANT_FIND_ERROR, this.IconLabel.Text), Strings.QUEST_EDITOR_TITLE, MessageBoxButtons.OK, MessageBoxIcon.Hand);
				}
			}
			if (itemMan != null)
			{
				this.Edit(itemMan);
				CreateQuestItemForm.mainDb.SaveChanges();
				this.Cursor = Cursors.Default;
				this.createdItem = itemMan.DBID.ToString();
				this.ResultTextBox.AppendText(this.createdItem);
				if (this.Mode == CreateQuestItemForm.FormMode.Create)
				{
					MessageBox.Show(string.Format(Strings.QUEST_EDITOR_OBJECTS_CREATED_MESSAGE, 1), Strings.QUEST_EDITOR_TITLE, MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
					this.Open(itemMan);
					return;
				}
				MessageBox.Show(string.Format(Strings.QUEST_EDITOR_OBJECTS_MODIFIED_MESSAGE, 1), Strings.QUEST_EDITOR_TITLE, MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
			}
		}

		// Token: 0x0600187B RID: 6267 RVA: 0x000A2CD4 File Offset: 0x000A1CD4
		private void OnChooseIconClick(object sender, EventArgs e)
		{
			SelectIconDialogForm dialog = new SelectIconDialogForm();
			this.Cursor = Cursors.WaitCursor;
			if (dialog.ShowDialog(this) == DialogResult.OK)
			{
				this.IconPictureBox.Image = null;
				this.IconLabel.Text = string.Empty;
				string key;
				if (dialog.GetSelectedIconKey(out key) && !string.IsNullOrEmpty(key))
				{
					this.SetIconByKey(key);
				}
			}
			this.Cursor = Cursors.Default;
		}

		// Token: 0x0600187C RID: 6268 RVA: 0x000A2D3C File Offset: 0x000A1D3C
		private void SetIconByKey(string key)
		{
			this.IconLabel.Text = key;
			this.IconPictureBox.Image = ObjectGeneratorConfig.IconCache.GetIcon(key);
		}

		// Token: 0x0600187D RID: 6269 RVA: 0x000A2D60 File Offset: 0x000A1D60
		private void OnPostLoadParams(FormParams formParams)
		{
			this.IconLabel.Text = string.Empty;
			this.FileNameTextBox.Clear();
			if (base.ParamsSaver != null)
			{
				base.ParamsSaver.PostLoadParams -= this.OnPostLoadParams;
			}
		}

		// Token: 0x0600187E RID: 6270 RVA: 0x000A2D9C File Offset: 0x000A1D9C
		private void Open(IObjMan itemMan)
		{
			if (itemMan != null)
			{
				this.ItemNameTextBox.Text = DBMethods.GetTextValue(itemMan, "name", false);
				this.ItemDescTextBox.Text = DBMethods.GetTextValue(itemMan, "description", false);
				int stackLimit;
				itemMan.GetValue("stackLimit", out stackLimit);
				this.StackLimitTextBox.Text = stackLimit.ToString();
				int sellPrice;
				itemMan.GetValue("sellPrice", out sellPrice);
				this.SellPriceTextBox.Text = sellPrice.ToString();
				int buyPrice;
				itemMan.GetValue("buyPrice", out buyPrice);
				this.BuyPriceTextBox.Text = buyPrice.ToString();
				string iconDBID;
				itemMan.GetValue("image", out iconDBID);
				this.SetIconByKey(iconDBID);
				this.NewButton.Enabled = true;
				this.ZoneComboBox.Enabled = false;
				this.FileNameTextBox.Enabled = false;
				this.FileNameTextBox.Text = itemMan.DBID.ToString();
			}
		}

		// Token: 0x0600187F RID: 6271 RVA: 0x000A2E88 File Offset: 0x000A1E88
		private void OnOpenButtonClick(object sender, EventArgs e)
		{
			OpenFileDialog dialog = new OpenFileDialog();
			StringBuilder initDir = new StringBuilder(EditorEnvironment.DataFolder);
			if (this.Mode == CreateQuestItemForm.FormMode.Edit && !string.IsNullOrEmpty(this.FileNameTextBox.Text))
			{
				initDir.Append(this.FileNameTextBox.Text.Remove(this.FileNameTextBox.Text.LastIndexOf('/') + 1));
			}
			else
			{
				initDir.Append("Items/QuestItems/");
				if (!string.IsNullOrEmpty(this.ZoneComboBox.Text))
				{
					initDir.Append(this.ZoneComboBox.Text);
				}
			}
			initDir.Replace('/', '\\');
			if (initDir.ToString().EndsWith("\\"))
			{
				initDir.Remove(initDir.Length - 1, 1);
			}
			dialog.InitialDirectory = initDir.ToString();
			dialog.Filter = string.Format(".({0}).xdb files|*.({0}).xdb|.xdb files | *.xdb", "ItemResource");
			dialog.SupportMultiDottedExtensions = true;
			if (dialog.ShowDialog(this) == DialogResult.OK)
			{
				DBID dbid = CreateQuestItemForm.mainDb.GetDBIDByName(dialog.FileName);
				if (!DBID.IsNullOrEmpty(dbid))
				{
					this.Open(CreateQuestItemForm.mainDb.GetManipulator(dbid));
				}
			}
		}

		// Token: 0x06001880 RID: 6272 RVA: 0x000A2FA7 File Offset: 0x000A1FA7
		private void OnNewButtonClick(object sender, EventArgs e)
		{
			this.ZoneComboBox.Enabled = true;
			this.FileNameTextBox.Enabled = true;
			this.FileNameTextBox.Clear();
			this.NewButton.Enabled = false;
		}

		// Token: 0x06001881 RID: 6273 RVA: 0x000A2FD8 File Offset: 0x000A1FD8
		private void OnLoadForm(object sender, EventArgs e)
		{
			QuestEnvironment.LoadZones(this.ZoneComboBox, false);
		}

		// Token: 0x06001882 RID: 6274 RVA: 0x000A2FE8 File Offset: 0x000A1FE8
		public CreateQuestItemForm(MainForm.Context context) : base(EditorEnvironment.EditorFormsFolder + "CreateQuestItemForm.xml", context)
		{
			this.InitializeComponent();
			base.AllowClose = true;
			base.Load += this.OnLoadForm;
			this.OpenButton.Click += this.OnOpenButtonClick;
			this.NewButton.Click += this.OnNewButtonClick;
			this.ChooseIconButton.Click += this.OnChooseIconClick;
			this.stackLimitValidator = new IntTextValidator(this.StackLimitTextBox, Strings.QUEST_EDITOR_WRONGFORMATERROR_MESSAGE, Strings.QUEST_EDITOR_ERROR_TITLE);
			this.sellPriceValidator = new IntTextValidator(this.SellPriceTextBox, Strings.QUEST_EDITOR_WRONGFORMATERROR_MESSAGE, Strings.QUEST_EDITOR_ERROR_TITLE);
			this.buyPriceValidator = new IntTextValidator(this.BuyPriceTextBox, Strings.QUEST_EDITOR_WRONGFORMATERROR_MESSAGE, Strings.QUEST_EDITOR_ERROR_TITLE);
			this.SaveButton.Click += this.OnProcessClick;
			if (base.ParamsSaver != null)
			{
				base.ParamsSaver.PostLoadParams += this.OnPostLoadParams;
			}
		}

		// Token: 0x170005F4 RID: 1524
		// (get) Token: 0x06001883 RID: 6275 RVA: 0x000A30F6 File Offset: 0x000A20F6
		public string CreatedItem
		{
			get
			{
				return this.createdItem;
			}
		}

		// Token: 0x04000FD8 RID: 4056
		private const string questItemPath = "Items/QuestItems/";

		// Token: 0x04000FD9 RID: 4057
		private const string category = "Mechanics/ItemCategories/Quest.xdb";

		// Token: 0x04000FDA RID: 4058
		private static readonly IDatabase mainDb = IDatabase.GetMainDatabase();

		// Token: 0x04000FDB RID: 4059
		private readonly IntTextValidator stackLimitValidator;

		// Token: 0x04000FDC RID: 4060
		private readonly IntTextValidator sellPriceValidator;

		// Token: 0x04000FDD RID: 4061
		private readonly IntTextValidator buyPriceValidator;

		// Token: 0x04000FDE RID: 4062
		private string createdItem;

		// Token: 0x020001E6 RID: 486
		private enum FormMode
		{
			// Token: 0x04000FF7 RID: 4087
			Create,
			// Token: 0x04000FF8 RID: 4088
			Edit
		}
	}
}
