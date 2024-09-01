using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Text;
using System.Windows.Forms;
using Db;
using MapEditor.Forms.Base;
using MapEditor.Resources.Strings;
using Tools.ControlValidators;
using Tools.DBGameObjects;
using Tools.WindowParams;

namespace MapEditor.Forms.Quests.QuickObjectGenerator
{
	// Token: 0x0200006C RID: 108
	public partial class CreateNPCForm : BaseForm
	{
		// Token: 0x06000537 RID: 1335 RVA: 0x0002A2DC File Offset: 0x000292DC
		private string GetChoosedCharacterFolderName()
		{
			StringBuilder result = new StringBuilder(this.RaceComboBox.Text);
			if (this.MaleRadioButton.Checked)
			{
				result.Append("_male");
			}
			else
			{
				result.Append("_female");
			}
			return result.ToString();
		}

		// Token: 0x06000538 RID: 1336 RVA: 0x0002A327 File Offset: 0x00029327
		private string GetChoosedCharacterFolder()
		{
			return string.Format("{0}/{1}", CreateNPCForm.characterFolderPath, this.GetChoosedCharacterFolderName());
		}

		// Token: 0x06000539 RID: 1337 RVA: 0x0002A340 File Offset: 0x00029340
		private string GetMobDBID()
		{
			return string.Format("{0}/{1}/{2}/{3}.({4}).xdb", new object[]
			{
				this.GetChoosedCharacterFolder(),
				"Instances",
				this.ZoneComboBox.Text,
				this.FileNameTextBox.Text,
				"MobWorld"
			});
		}

		// Token: 0x0600053A RID: 1338 RVA: 0x0002A394 File Offset: 0x00029394
		private static string GetPairValueFromConfigByKey(string key, IEnumerable<ObjectGeneratorConfig.StringPair> pairList)
		{
			if (pairList != null)
			{
				foreach (ObjectGeneratorConfig.StringPair pair in pairList)
				{
					if (pair.first == key)
					{
						return pair.second;
					}
				}
			}
			return null;
		}

		// Token: 0x0600053B RID: 1339 RVA: 0x0002A3F4 File Offset: 0x000293F4
		private bool CheckValues()
		{
			foreach (object obj in base.Controls)
			{
				Control control = (Control)obj;
				if (control != null && (control.GetType() == typeof(TextBox) || control.GetType() == typeof(ComboBox)) && string.IsNullOrEmpty(control.Text))
				{
					MessageBox.Show(Strings.QUEST_EDITOR_MANDATORYFIELDERROR_MESSAGE, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Hand);
					return false;
				}
			}
			foreach (TextControlValidator validator in this.validaotors)
			{
				if (!validator.ValidateText())
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x0600053C RID: 1340 RVA: 0x0002A4E0 File Offset: 0x000294E0
		private void OnCreateClick(object sender, EventArgs e)
		{
			IDatabase mainDb = IDatabase.GetMainDatabase();
			if (mainDb == null)
			{
				return;
			}
			if (!this.CheckValues())
			{
				return;
			}
			string kind = null;
			string quality = null;
			if (File.Exists(ObjectGeneratorConfig.npcConfigPath))
			{
				ObjectGeneratorConfig.NPCGeneratorConfig config = Serializer.Load<ObjectGeneratorConfig.NPCGeneratorConfig>(ObjectGeneratorConfig.npcConfigPath);
				if (config != null)
				{
					kind = CreateNPCForm.GetPairValueFromConfigByKey(this.GetChoosedCharacterFolderName(), config.MobKindList);
					quality = config.Quality;
				}
			}
			if (string.IsNullOrEmpty(kind) || !File.Exists(EditorEnvironment.DataFolder + kind))
			{
				MessageBox.Show(string.Format(Strings.QUEST_EDITOR_CANT_FIND_OBJECT_FOR_FIELD_ERROR, kind, "kind"), this.Text, MessageBoxButtons.OK, MessageBoxIcon.Hand);
				return;
			}
			if (string.IsNullOrEmpty(this.FactionTextBox.Text) || !File.Exists(EditorEnvironment.DataFolder + this.FactionTextBox.Text))
			{
				MessageBox.Show(string.Format(Strings.QUEST_EDITOR_CANT_FIND_OBJECT_FOR_FIELD_ERROR, this.FactionTextBox.Text, "faction"), this.Text, MessageBoxButtons.OK, MessageBoxIcon.Hand);
				return;
			}
			if (string.IsNullOrEmpty(quality) || !File.Exists(EditorEnvironment.DataFolder + quality))
			{
				MessageBox.Show(string.Format(Strings.QUEST_EDITOR_CANT_FIND_OBJECT_FOR_FIELD_ERROR, quality, "quality"), this.Text, MessageBoxButtons.OK, MessageBoxIcon.Hand);
				return;
			}
			this.Cursor = Cursors.WaitCursor;
			this.ResultTextBox.Clear();
			DBID mobDBID = IDatabase.CreateDBIDByName(this.GetMobDBID());
			Dictionary<DBID, string> dbidDict = new Dictionary<DBID, string>(1);
			dbidDict.Add(mobDBID, "gameMechanics.world.mob.MobWorld");
			Dictionary<DBID, IObjMan> objDict = new Dictionary<DBID, IObjMan>(1);
			IObjMan mobMan;
			if (!ObjectGeneratorConfig.CreateNewObjects(this, mainDb, dbidDict, ref objDict) || !objDict.TryGetValue(mobDBID, out mobMan))
			{
				this.Cursor = Cursors.Default;
				return;
			}
			DBMethods.SetTextValue(mobMan, "name", this.NameTextBox.Text, this.FileNameTextBox.Text + "_Name.txt", false);
			DBMethods.SetTextValue(mobMan, "title", this.TitleTextBox.Text, this.FileNameTextBox.Text + "_Title.txt", false);
			mobMan.SetValue("kind", kind);
			mobMan.SetValue("faction", this.FactionTextBox.Text);
			mobMan.SetValue("levelMin", int.Parse(this.LevelTextBox.Text));
			mobMan.SetValue("levelMax", int.Parse(this.LevelTextBox.Text));
			mobMan.SetValue("walkSpeed", float.Parse(this.SpeedTextBox.Text));
			mobMan.SetValue("quality", quality);
			mobMan.SetValue("visMob", this.VisMobTextBox.Text);
			mainDb.SaveChanges();
			this.FactionTextBox.Clear();
			this.Cursor = Cursors.Default;
			this.createdNPC = mobDBID.ToString();
			MessageBox.Show(string.Format(Strings.QUEST_EDITOR_OBJECTS_CREATED_MESSAGE, 1), Strings.QUEST_EDITOR_TITLE, MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
			this.ResultTextBox.AppendText(this.createdNPC);
		}

		// Token: 0x0600053D RID: 1341 RVA: 0x0002A7BC File Offset: 0x000297BC
		private void LoadRaces()
		{
			DirectoryInfo charactersDir = new DirectoryInfo(CreateNPCForm.characterFolderPath);
			if (Directory.Exists(CreateNPCForm.characterFolderPath))
			{
				DirectoryInfo[] maleDirArr = charactersDir.GetDirectories(string.Format("*{0}", "_male"));
				foreach (DirectoryInfo maleDir in maleDirArr)
				{
					string raceName = maleDir.Name.Remove(maleDir.Name.LastIndexOf("_male"));
					if (charactersDir.GetDirectories(string.Format("*{0}{1}", raceName, "_female")).Length > 0)
					{
						this.RaceComboBox.Items.Add(raceName);
					}
				}
			}
		}

		// Token: 0x0600053E RID: 1342 RVA: 0x0002A85C File Offset: 0x0002985C
		private void OnChooseVisMobClick(object sender, EventArgs e)
		{
			OpenFileDialog openDialog = new OpenFileDialog();
			openDialog.Filter = ".xdb files|*.xdb";
			DirectoryInfo characterDirInfo = new DirectoryInfo(CreateNPCForm.characterFolderPath);
			if (characterDirInfo.Exists)
			{
				openDialog.InitialDirectory = characterDirInfo.FullName;
			}
			openDialog.RestoreDirectory = true;
			openDialog.Multiselect = false;
			if (openDialog.ShowDialog(this) == DialogResult.OK)
			{
				this.VisMobTextBox.Text = openDialog.FileName;
				if (!string.IsNullOrEmpty(this.VisMobTextBox.Text))
				{
					this.visMobValidator.ValidateText();
				}
			}
		}

		// Token: 0x0600053F RID: 1343 RVA: 0x0002A8E0 File Offset: 0x000298E0
		private void OnChooseFactionClick(object sender, EventArgs e)
		{
			OpenFileDialog openDialog = new OpenFileDialog();
			openDialog.Filter = ".xdb files|*.xdb";
			DirectoryInfo factionDirInfo = new DirectoryInfo(CreateNPCForm.factionFolderPath);
			if (factionDirInfo.Exists)
			{
				openDialog.InitialDirectory = factionDirInfo.FullName;
			}
			openDialog.RestoreDirectory = true;
			openDialog.Multiselect = false;
			if (openDialog.ShowDialog(this) == DialogResult.OK)
			{
				this.FactionTextBox.Text = openDialog.FileName;
				if (!string.IsNullOrEmpty(this.FactionTextBox.Text))
				{
					this.factionValidator.ValidateText();
				}
			}
		}

		// Token: 0x06000540 RID: 1344 RVA: 0x0002A964 File Offset: 0x00029964
		private void OnLoadParams(FormParams formParams)
		{
			this.LoadRaces();
			QuestEnvironment.LoadZones(this.ZoneComboBox, false);
			this.ChooseVisMobButton.Click += this.OnChooseVisMobClick;
			this.ChooseFactionButton.Click += this.OnChooseFactionClick;
			this.CreateButton.Click += this.OnCreateClick;
			if (base.ParamsSaver != null)
			{
				base.ParamsSaver.LoadParams -= this.OnLoadParams;
			}
		}

		// Token: 0x06000541 RID: 1345 RVA: 0x0002A9E8 File Offset: 0x000299E8
		public CreateNPCForm(MainForm.Context context) : base(EditorEnvironment.EditorFormsFolder + "CreateNPCForm.xml", context)
		{
			this.InitializeComponent();
			base.AllowClose = true;
			if (base.ParamsSaver != null)
			{
				base.ParamsSaver.RegisterControl(new RadioButton[]
				{
					this.MaleRadioButton,
					this.FemaleRadioButton
				});
				base.ParamsSaver.LoadParams += this.OnLoadParams;
			}
			this.visMobValidator = new DBIDTextValidator("VisualMob", this.VisMobTextBox, string.Format(Strings.QUEST_EDITOR_WRONG_VIS_MOB_ERROR, "VisualMob"), Strings.QUEST_EDITOR_ERROR_TITLE);
			this.factionValidator = new DBIDTextValidator("gameMechanics.world.creature.Faction", this.FactionTextBox, string.Format(Strings.QUEST_EDITOR_WRONG_TYPE_ERROR, "gameMechanics.world.creature.Faction"), Strings.QUEST_EDITOR_ERROR_TITLE);
			IntTextValidator levelValidator = new IntTextValidator(this.LevelTextBox, Strings.QUEST_EDITOR_WRONGFORMATERROR_MESSAGE, Strings.QUEST_EDITOR_ERROR_TITLE);
			FloatTextValidator speedValidator = new FloatTextValidator(this.SpeedTextBox, Strings.QUEST_EDITOR_WRONGFORMATERROR_MESSAGE, Strings.QUEST_EDITOR_ERROR_TITLE);
			this.validaotors = new List<TextControlValidator>();
			this.validaotors.Add(this.visMobValidator);
			this.validaotors.Add(levelValidator);
			this.validaotors.Add(speedValidator);
			this.validaotors.Add(this.factionValidator);
		}

		// Token: 0x170000C5 RID: 197
		// (get) Token: 0x06000542 RID: 1346 RVA: 0x0002AB2D File Offset: 0x00029B2D
		public string CreatedNPC
		{
			get
			{
				return this.createdNPC;
			}
		}

		// Token: 0x040003C8 RID: 968
		private const string maleSufix = "_male";

		// Token: 0x040003C9 RID: 969
		private const string femaleSufix = "_female";

		// Token: 0x040003CA RID: 970
		private static readonly string characterFolderPath = EditorEnvironment.DataFolder + "/Characters";

		// Token: 0x040003CB RID: 971
		private static readonly string factionFolderPath = EditorEnvironment.DataFolder + "World/Factions";

		// Token: 0x040003CC RID: 972
		private readonly DBIDTextValidator visMobValidator;

		// Token: 0x040003CD RID: 973
		private readonly DBIDTextValidator factionValidator;

		// Token: 0x040003CE RID: 974
		private readonly List<TextControlValidator> validaotors = new List<TextControlValidator>(3);

		// Token: 0x040003CF RID: 975
		private string createdNPC;
	}
}
