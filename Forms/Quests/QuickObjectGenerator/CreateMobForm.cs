using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.IO;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using Db;
using MapEditor.Forms.Base;
using MapEditor.Forms.Quests.QuickObjectGenerator.DialogForms;
using MapEditor.Map;
using MapEditor.Resources.Strings;
using Tools.ControlContainer;
using Tools.ControlValidators;
using Tools.DBGameObjects;
using Tools.WindowParams;

namespace MapEditor.Forms.Quests.QuickObjectGenerator
{
	// Token: 0x02000217 RID: 535
	public partial class CreateMobForm : BaseForm
	{
		// Token: 0x060019E5 RID: 6629 RVA: 0x000A8394 File Offset: 0x000A7394
		private static string GetVisMobDBID(bool createNew, string source, string scale)
		{
			if (!createNew)
			{
				return source;
			}
			string[] sourceArr = source.Split(new char[]
			{
				'.'
			});
			if (sourceArr.Length > 0)
			{
				string scaleSuff = scale.Replace(CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator, "_");
				return string.Format("{0}Scale{1}.({2}).xdb", sourceArr[0], scaleSuff, "VisualMob");
			}
			return string.Empty;
		}

		// Token: 0x060019E6 RID: 6630 RVA: 0x000A83F4 File Offset: 0x000A73F4
		private bool CheckValues(ControlContainer page, IEnumerable<TextControlValidator> validators)
		{
			foreach (object obj in page)
			{
				Control control = (Control)obj;
				if (control != null && control.Enabled && (control.GetType() == typeof(TextBox) || control.GetType() == typeof(ComboBox)) && string.IsNullOrEmpty(control.Text) && control != this.TitleTextBox && control != this.AddTitleTextBox)
				{
					MessageBox.Show(Strings.QUEST_EDITOR_MANDATORYFIELDERROR_MESSAGE, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Hand);
					return false;
				}
			}
			foreach (TextControlValidator validator in validators)
			{
				if (!validator.ValidateText())
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x060019E7 RID: 6631 RVA: 0x000A84F0 File Offset: 0x000A74F0
		private void OnCreateClick(object sender, EventArgs e)
		{
			this.ResultTextBox.Clear();
			if (!this.CheckValues(this.commonControlContainer, this.commonValidators))
			{
				return;
			}
			this.Cursor = Cursors.WaitCursor;
			List<string> results = new List<string>();
			bool success = CreateMobForm.Genetator.CommonGenerate(this, this.commonFieldsParser, this.config, this.NeutralRadioButton.Checked, this.NameTextBox.Text, this.TitleTextBox.Text, this.MinLevelTextBox.Text, this.MaxLevelTextBox.Text, this.SpeedTextBox.Text, ref results);
			this.Cursor = Cursors.Default;
			if (success)
			{
				MessageBox.Show(string.Format(Strings.QUEST_EDITOR_OBJECTS_CREATED_MESSAGE, results.Count), Strings.QUEST_EDITOR_TITLE, MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
				foreach (string result in results)
				{
					this.ResultTextBox.AppendText(result + '\n');
				}
			}
			this.SourceMobTextBox.Text = this.commonFieldsParser.GetMobDBID();
			this.AddSpawnTableTextBox.Text = this.commonFieldsParser.GetSpawnTableDBID();
			this.SpawnTableTextBox.Text = this.commonFieldsParser.GetSpawnTableDBID();
			this.createdMob = this.commonFieldsParser.GetMobDBID();
		}

		// Token: 0x060019E8 RID: 6632 RVA: 0x000A865C File Offset: 0x000A765C
		private void OnAddCreateClick(object sender, EventArgs e)
		{
			this.ResultTextBox.Clear();
			if (!this.CheckValues(this.additionalControlContainer, this.additionalValidators))
			{
				return;
			}
			this.Cursor = Cursors.WaitCursor;
			List<string> results = new List<string>();
			bool success = CreateMobForm.Genetator.AdditionalGenerate(this, this.additionalFieldsParser, this.config, this.AddNameTextBox.Text, this.AddTitleTextBox.Text, this.AddSpawnTableTextBox.Text, ref results);
			this.Cursor = Cursors.Default;
			if (success)
			{
				MessageBox.Show(string.Format(Strings.QUEST_EDITOR_OBJECTS_CREATED_MESSAGE, results.Count), Strings.QUEST_EDITOR_TITLE, MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
				foreach (string result in results)
				{
					this.ResultTextBox.AppendText(result + '\n');
				}
			}
			this.createdMob = this.additionalFieldsParser.GetMobDBID();
		}

		// Token: 0x060019E9 RID: 6633 RVA: 0x000A8764 File Offset: 0x000A7764
		private void LoadCreatures()
		{
			this.CreatureComboBox.Items.Clear();
			if (this.config != null && this.config.ZoneCreatureMap != null && !string.IsNullOrEmpty(this.ZoneComboBox.Text))
			{
				foreach (ObjectGeneratorConfig.StringListPair pair in this.config.ZoneCreatureMap)
				{
					if (pair.key == this.ZoneComboBox.Text)
					{
						using (List<string>.Enumerator enumerator2 = pair.values.GetEnumerator())
						{
							while (enumerator2.MoveNext())
							{
								string creature = enumerator2.Current;
								this.CreatureComboBox.Items.Add(creature);
							}
							break;
						}
					}
				}
				if (this.CreatureComboBox.Items.Count > 0)
				{
					this.CreatureComboBox.SelectedIndex = 0;
					this.SetVisualMob();
				}
			}
		}

		// Token: 0x060019EA RID: 6634 RVA: 0x000A8884 File Offset: 0x000A7884
		private void SetVisualMob()
		{
			this.VisMobTextBox.Text = string.Empty;
			if (!string.IsNullOrEmpty(this.CreatureComboBox.Text) && this.config != null && !string.IsNullOrEmpty(this.config.MobKindSuffix))
			{
				this.VisMobTextBox.Text = string.Format("{0}/{1}.({2}).xdb", this.commonFieldsParser.GetSelectedCreatureFolder(), this.commonFieldsParser.GetSelectedCreatureName(this.config.MobKindSuffix), "VisualMob");
				if (!this.visMobValidator.ValidateText(false))
				{
					this.VisMobTextBox.Text = string.Empty;
				}
			}
		}

		// Token: 0x060019EB RID: 6635 RVA: 0x000A8926 File Offset: 0x000A7926
		private void OnSelectZone(object sender, EventArgs e)
		{
			this.LoadCreatures();
		}

		// Token: 0x060019EC RID: 6636 RVA: 0x000A892E File Offset: 0x000A792E
		private void OnSelectCreature(object sender, EventArgs e)
		{
			this.SetVisualMob();
			if (this.config != null)
			{
				this.commonFieldsParser.PromptFileName(this.config.MobKindSuffix);
			}
			this.NewSTRadioButton.Checked = true;
		}

		// Token: 0x060019ED RID: 6637 RVA: 0x000A8960 File Offset: 0x000A7960
		private void LoadMaps()
		{
			List<string> maps = new List<string>();
			Constants.GetContinentNameList(ref maps);
			foreach (string map in maps)
			{
				this.MapComboBox.Items.Add(map);
			}
		}

		// Token: 0x060019EE RID: 6638 RVA: 0x000A89C8 File Offset: 0x000A79C8
		private void OnLoadParams(FormParams formParams)
		{
			if (File.Exists(ObjectGeneratorConfig.npcConfigPath))
			{
				this.config = Serializer.Load<ObjectGeneratorConfig.MobGeneratorConfig>(ObjectGeneratorConfig.mobConfigPath);
			}
			this.LoadMaps();
			QuestEnvironment.LoadZones(this.ZoneComboBox, false);
			if (base.ParamsSaver != null)
			{
				base.ParamsSaver.LoadParams -= this.OnLoadParams;
			}
		}

		// Token: 0x060019EF RID: 6639 RVA: 0x000A8A22 File Offset: 0x000A7A22
		private void OnPostLoadParams(FormParams formParams)
		{
			this.LoadCreatures();
			if (base.ParamsSaver != null)
			{
				base.ParamsSaver.PostLoadParams -= this.OnPostLoadParams;
			}
		}

		// Token: 0x060019F0 RID: 6640 RVA: 0x000A8A4C File Offset: 0x000A7A4C
		private void OnControlEnabledChecked(object sender, EventArgs e)
		{
			Control altControl = null;
			Control control;
			bool enabled;
			if (sender == this.ExistingVMRadioButton)
			{
				control = this.NewVisMobParamsPanel;
				enabled = !this.ExistingVMRadioButton.Checked;
			}
			else if (sender == this.NewSTRadioButton)
			{
				control = this.SpawnTablePanel;
				enabled = !this.NewSTRadioButton.Checked;
				altControl = this.SpawnTableMapPanel;
			}
			else if (sender == this.CreateSilverRadioButton)
			{
				control = this.AddSpawnTablePanel;
				enabled = this.CreateSilverRadioButton.Checked;
			}
			else
			{
				if (sender != this.AddExistingVMRadioButton)
				{
					return;
				}
				control = this.AddNewVisMobParamsPanel;
				enabled = !this.AddExistingVMRadioButton.Checked;
			}
			control.Enabled = enabled;
			if (altControl != null)
			{
				altControl.Enabled = !enabled;
			}
		}

		// Token: 0x060019F1 RID: 6641 RVA: 0x000A8AF8 File Offset: 0x000A7AF8
		private void OnSelectDBIDClick(object sender, EventArgs e)
		{
			TextBox textBox;
			DBIDTextValidator validator;
			string initDirPath;
			if (sender == this.ChooseVisMobButton)
			{
				textBox = this.VisMobTextBox;
				validator = this.visMobValidator;
				initDirPath = this.commonFieldsParser.GetSelectedCreatureFolder();
			}
			else if (sender == this.ChooseSpawnTablebButton)
			{
				textBox = this.SpawnTableTextBox;
				validator = this.spawnTableValidator;
				initDirPath = DBMethods.GetContainingFolderWithoutSlash(textBox.Text);
			}
			else if (sender == this.ChooseSourceMobButton)
			{
				textBox = this.SourceMobTextBox;
				validator = this.sourceMobValidator;
				initDirPath = DBMethods.GetContainingFolderWithoutSlash(textBox.Text);
			}
			else if (sender == this.AddChooseVisMobButton)
			{
				textBox = this.AddVisMobTextBox;
				validator = this.addVisMobValidator;
				initDirPath = this.additionalFieldsParser.GetSourceMobFolder();
			}
			else
			{
				if (sender != this.AddChooseSpawnTablebButton)
				{
					return;
				}
				textBox = this.AddSpawnTableTextBox;
				validator = this.addSpawnTableValidator;
				initDirPath = DBMethods.GetContainingFolderWithoutSlash(textBox.Text);
			}
			OpenFileDialog openDialog = new OpenFileDialog();
			openDialog.Filter = ".xdb files|*.xdb";
			if (!string.IsNullOrEmpty(initDirPath))
			{
				if (!Directory.Exists(initDirPath))
				{
					initDirPath = EditorEnvironment.DataFolder + initDirPath;
				}
				if (Directory.Exists(initDirPath))
				{
					DirectoryInfo initDir = new DirectoryInfo(initDirPath);
					openDialog.InitialDirectory = initDir.FullName;
				}
			}
			openDialog.RestoreDirectory = true;
			openDialog.Multiselect = false;
			if (openDialog.ShowDialog(this) == DialogResult.OK)
			{
				textBox.Text = openDialog.FileName;
				if (!string.IsNullOrEmpty(textBox.Text))
				{
					validator.ValidateText(true);
				}
			}
		}

		// Token: 0x060019F2 RID: 6642 RVA: 0x000A8C48 File Offset: 0x000A7C48
		private void SetAddVisualMob()
		{
			if (this.sourceMobValidator.ValidateText(false))
			{
				IDatabase mainDb = IDatabase.GetMainDatabase();
				if (mainDb != null)
				{
					DBID mobDBID = mainDb.GetDBIDByName(this.SourceMobTextBox.Text);
					IObjMan man = mainDb.GetManipulator(mobDBID);
					if (man != null)
					{
						DBID visMobDBID;
						man.GetValue("visMob", out visMobDBID);
						string oldVal = this.AddVisMobTextBox.Text;
						this.AddVisMobTextBox.Text = visMobDBID.ToString();
						if (!this.addVisMobValidator.ValidateText(false))
						{
							this.AddVisMobTextBox.Text = oldVal;
						}
					}
				}
			}
		}

		// Token: 0x060019F3 RID: 6643 RVA: 0x000A8CCF File Offset: 0x000A7CCF
		private void OnSourceMobTextBoxTimerTick(object sender, EventArgs e)
		{
			this.SourceMobTextBoxTimer.Stop();
			this.SetAddVisualMob();
			this.additionalFieldsParser.PromptFileName();
		}

		// Token: 0x060019F4 RID: 6644 RVA: 0x000A8CED File Offset: 0x000A7CED
		private void OnSourceMobTextChanged(object sender, EventArgs e)
		{
			this.SourceMobTextBoxTimer.Start();
		}

		// Token: 0x060019F5 RID: 6645 RVA: 0x000A8CFC File Offset: 0x000A7CFC
		private void OnEditCreatureListClick(object sender, EventArgs e)
		{
			ZoneCreatureListDialogForm dialog = new ZoneCreatureListDialogForm(this.config);
			dialog.ShowDialog();
			this.LoadCreatures();
		}

		// Token: 0x060019F6 RID: 6646 RVA: 0x000A8D22 File Offset: 0x000A7D22
		private void OnLevelTextChanged(object sender, EventArgs e)
		{
			this.SetFileNameTimer.Start();
		}

		// Token: 0x060019F7 RID: 6647 RVA: 0x000A8D2F File Offset: 0x000A7D2F
		private void OnSetFileNameTimerTick(object sender, EventArgs e)
		{
			this.SetFileNameTimer.Stop();
			if (this.config != null)
			{
				this.commonFieldsParser.PromptFileName(this.config.MobKindSuffix);
			}
		}

		// Token: 0x060019F8 RID: 6648 RVA: 0x000A8D5A File Offset: 0x000A7D5A
		private void OnCreateSilverRadioButtonChecked(object sender, EventArgs e)
		{
			this.additionalFieldsParser.PromptFileName();
		}

		// Token: 0x060019F9 RID: 6649 RVA: 0x000A8D68 File Offset: 0x000A7D68
		public CreateMobForm(MainForm.Context context) : base(EditorEnvironment.EditorFormsFolder + "CreateMobForm.xml", context)
		{
			this.InitializeComponent();
			base.AllowClose = true;
			this.ZoneComboBox.SelectedIndexChanged += this.OnSelectZone;
			this.CreatureComboBox.SelectedIndexChanged += this.OnSelectCreature;
			this.ChooseVisMobButton.Click += this.OnSelectDBIDClick;
			this.ChooseSpawnTablebButton.Click += this.OnSelectDBIDClick;
			this.ChooseSourceMobButton.Click += this.OnSelectDBIDClick;
			this.AddChooseVisMobButton.Click += this.OnSelectDBIDClick;
			this.AddChooseSpawnTablebButton.Click += this.OnSelectDBIDClick;
			this.ExistingVMRadioButton.CheckedChanged += this.OnControlEnabledChecked;
			this.NewSTRadioButton.CheckedChanged += this.OnControlEnabledChecked;
			this.CreateSilverRadioButton.CheckedChanged += this.OnControlEnabledChecked;
			this.AddExistingVMRadioButton.CheckedChanged += this.OnControlEnabledChecked;
			this.SourceMobTextBox.TextChanged += this.OnSourceMobTextChanged;
			this.SourceMobTextBoxTimer.Tick += this.OnSourceMobTextBoxTimerTick;
			this.MinLevelTextBox.TextChanged += this.OnLevelTextChanged;
			this.MaxLevelTextBox.TextChanged += this.OnLevelTextChanged;
			this.SetFileNameTimer.Tick += this.OnSetFileNameTimerTick;
			this.CreateSilverRadioButton.CheckedChanged += this.OnCreateSilverRadioButtonChecked;
			this.EditCreatureListButton.Click += this.OnEditCreatureListClick;
			this.commonControlContainer = new ControlContainer(this.CommonTabPage);
			this.additionalControlContainer = new ControlContainer(this.AdditionalTabPage);
			if (base.ParamsSaver != null)
			{
				base.ParamsSaver.RegisterControlContainer(this.CommonTabPage);
				base.ParamsSaver.RegisterControlContainer(this.AdditionalTabPage);
				base.ParamsSaver.RegisterControlContainer(this.NewVisMobParamsPanel);
				base.ParamsSaver.RegisterControlContainer(this.SpawnTablePanel);
				base.ParamsSaver.RegisterControlContainer(this.SpawnTableMapPanel);
				base.ParamsSaver.RegisterControlContainer(this.AddNewVisMobParamsPanel);
				base.ParamsSaver.RegisterControlContainer(this.AddSpawnTablePanel);
				base.ParamsSaver.RegisterControl(new RadioButton[]
				{
					this.NeutralRadioButton,
					this.WildRadioButton
				});
				base.ParamsSaver.RegisterControl(new RadioButton[]
				{
					this.ExistingVMRadioButton,
					this.NewVMRadioButton
				});
				base.ParamsSaver.RegisterControl(new RadioButton[]
				{
					this.CreateSilverRadioButton,
					this.CreateMiniBossRadioButton
				});
				base.ParamsSaver.RegisterControl(new RadioButton[]
				{
					this.AddExistingVMRadioButton,
					this.AddNewVMRadioButton
				});
				if (base.ParamsSaver != null)
				{
					base.ParamsSaver.LoadParams += this.OnLoadParams;
					base.ParamsSaver.PostLoadParams += this.OnPostLoadParams;
				}
			}
			this.visMobValidator = new DBIDTextValidator("VisualMob", this.VisMobTextBox, string.Format(Strings.QUEST_EDITOR_WRONG_VIS_MOB_ERROR, "VisualMob"), Strings.QUEST_EDITOR_ERROR_TITLE);
			this.spawnTableValidator = new DBIDTextValidator("gameMechanics.map.spawn.SpawnTable", this.SpawnTableTextBox, string.Format(Strings.QUEST_EDITOR_WRONG_VIS_MOB_ERROR, "gameMechanics.map.spawn.SpawnTable"), Strings.QUEST_EDITOR_ERROR_TITLE);
			FloatTextValidator vmScaleValidator = new FloatTextValidator(this.VMScaleTextBox, Strings.QUEST_EDITOR_WRONGFORMATERROR_MESSAGE, Strings.QUEST_EDITOR_ERROR_TITLE);
			IntTextValidator minLevelValidator = new IntTextValidator(this.MinLevelTextBox, Strings.QUEST_EDITOR_WRONGFORMATERROR_MESSAGE, Strings.QUEST_EDITOR_ERROR_TITLE);
			IntTextValidator maxLevelValidator = new IntTextValidator(this.MaxLevelTextBox, Strings.QUEST_EDITOR_WRONGFORMATERROR_MESSAGE, Strings.QUEST_EDITOR_ERROR_TITLE);
			FloatTextValidator speedValidator = new FloatTextValidator(this.SpeedTextBox, Strings.QUEST_EDITOR_WRONGFORMATERROR_MESSAGE, Strings.QUEST_EDITOR_ERROR_TITLE);
			this.commonValidators.Add(this.visMobValidator);
			this.commonValidators.Add(this.spawnTableValidator);
			this.commonValidators.Add(vmScaleValidator);
			this.commonValidators.Add(minLevelValidator);
			this.commonValidators.Add(maxLevelValidator);
			this.commonValidators.Add(speedValidator);
			this.sourceMobValidator = new DBIDTextValidator("gameMechanics.world.mob.MobWorld", this.SourceMobTextBox, string.Format(Strings.QUEST_EDITOR_WRONG_VIS_MOB_ERROR, "gameMechanics.world.mob.MobWorld"), Strings.QUEST_EDITOR_ERROR_TITLE);
			this.addVisMobValidator = new DBIDTextValidator("VisualMob", this.AddVisMobTextBox, string.Format(Strings.QUEST_EDITOR_WRONG_VIS_MOB_ERROR, "VisualMob"), Strings.QUEST_EDITOR_ERROR_TITLE);
			FloatTextValidator addVMScaleValidator = new FloatTextValidator(this.AddVMScaleTextBox, Strings.QUEST_EDITOR_WRONGFORMATERROR_MESSAGE, Strings.QUEST_EDITOR_ERROR_TITLE);
			this.addSpawnTableValidator = new DBIDTextValidator("gameMechanics.map.spawn.SpawnTable", this.AddSpawnTableTextBox, string.Format(Strings.QUEST_EDITOR_WRONG_VIS_MOB_ERROR, "gameMechanics.map.spawn.SpawnTable"), Strings.QUEST_EDITOR_ERROR_TITLE);
			this.additionalValidators.Add(this.sourceMobValidator);
			this.additionalValidators.Add(this.addVisMobValidator);
			this.additionalValidators.Add(addVMScaleValidator);
			this.additionalValidators.Add(this.addSpawnTableValidator);
			this.commonFieldsParser = new CreateMobForm.CommonFieldsParser(this.CreatureComboBox, this.VisMobTextBox, this.NewVMRadioButton, this.VMScaleTextBox, this.ZoneComboBox, this.FileNameTextBox, this.NewSTRadioButton, this.MapComboBox, this.SpawnTableTextBox, this.MinLevelTextBox, this.MaxLevelTextBox, minLevelValidator, maxLevelValidator);
			this.additionalFieldsParser = new CreateMobForm.AdditionalFieldsParser(this.sourceMobValidator, this.SourceMobTextBox, this.AddVisMobTextBox, this.AddNewVMRadioButton, this.AddVMScaleTextBox, this.CreateSilverRadioButton, this.AddFileNameTextBox);
			this.CreateButton.Click += this.OnCreateClick;
			this.AddCreateButton.Click += this.OnAddCreateClick;
		}

		// Token: 0x1700063E RID: 1598
		// (get) Token: 0x060019FA RID: 6650 RVA: 0x000A9353 File Offset: 0x000A8353
		public string CreatedMob
		{
			get
			{
				return this.createdMob;
			}
		}

		// Token: 0x0400106A RID: 4202
		private readonly DBIDTextValidator visMobValidator;

		// Token: 0x0400106B RID: 4203
		private readonly DBIDTextValidator spawnTableValidator;

		// Token: 0x0400106C RID: 4204
		private readonly DBIDTextValidator sourceMobValidator;

		// Token: 0x0400106D RID: 4205
		private readonly DBIDTextValidator addVisMobValidator;

		// Token: 0x0400106E RID: 4206
		private readonly DBIDTextValidator addSpawnTableValidator;

		// Token: 0x0400106F RID: 4207
		private readonly List<TextControlValidator> commonValidators = new List<TextControlValidator>(10);

		// Token: 0x04001070 RID: 4208
		private readonly List<TextControlValidator> additionalValidators = new List<TextControlValidator>(10);

		// Token: 0x04001071 RID: 4209
		private readonly ControlContainer commonControlContainer;

		// Token: 0x04001072 RID: 4210
		private readonly ControlContainer additionalControlContainer;

		// Token: 0x04001073 RID: 4211
		private ObjectGeneratorConfig.MobGeneratorConfig config;

		// Token: 0x04001074 RID: 4212
		private readonly CreateMobForm.CommonFieldsParser commonFieldsParser;

		// Token: 0x04001075 RID: 4213
		private readonly CreateMobForm.AdditionalFieldsParser additionalFieldsParser;

		// Token: 0x04001076 RID: 4214
		private string createdMob;

		// Token: 0x02000218 RID: 536
		private class CommonFieldsParser
		{
			// Token: 0x060019FD RID: 6653 RVA: 0x000AB45C File Offset: 0x000AA45C
			public CommonFieldsParser(ComboBox _creatureComboBox, TextBox _visMobTextBox, RadioButton _newVMRadioButton, TextBox _scaleVisMob, ComboBox _zoneComboBox, TextBox _fileNameTextBox, RadioButton _newSTRadioButton, ComboBox _stMapComboBox, TextBox _spawnTableTextBox, TextBox _minLevelTextBox, TextBox _maxLevelTextBox, IntTextValidator _minLevelValidator, IntTextValidator _maxLevelValidator)
			{
				this.creatureComboBox = _creatureComboBox;
				this.visMobTextBox = _visMobTextBox;
				this.newVMRadioButton = _newVMRadioButton;
				this.scaleVisMob = _scaleVisMob;
				this.zoneComboBox = _zoneComboBox;
				this.fileNameTextBox = _fileNameTextBox;
				this.newSTRadioButton = _newSTRadioButton;
				this.stMapComboBox = _stMapComboBox;
				this.spawnTableTextBox = _spawnTableTextBox;
				this.minLevelTextBox = _minLevelTextBox;
				this.maxLevelTextBox = _maxLevelTextBox;
				this.minLevelValidator = _minLevelValidator;
				this.maxLevelValidator = _maxLevelValidator;
			}

			// Token: 0x060019FE RID: 6654 RVA: 0x000AB4D4 File Offset: 0x000AA4D4
			public string GetSelectedCreatureFolder()
			{
				if (!string.IsNullOrEmpty(this.creatureComboBox.Text))
				{
					return DBMethods.GetContainingFolderWithoutSlash(this.creatureComboBox.Text).Remove(0, "Mechanics".Length + 1);
				}
				return string.Empty;
			}

			// Token: 0x060019FF RID: 6655 RVA: 0x000AB510 File Offset: 0x000AA510
			public string GetSelectedCreatureName(string mobKindSuffix)
			{
				string result = this.creatureComboBox.Text.Replace('\\', '/');
				result = Str.CutFilePath(result);
				string suff = string.Format("{0}.xdb", mobKindSuffix);
				string altSuff = ".xdb";
				if (result.EndsWith(suff, StringComparison.OrdinalIgnoreCase))
				{
					result = result.Remove(result.Length - suff.Length);
				}
				else if (result.EndsWith(altSuff))
				{
					result = result.Remove(result.Length - altSuff.Length);
				}
				return result;
			}

			// Token: 0x06001A00 RID: 6656 RVA: 0x000AB589 File Offset: 0x000AA589
			public string GetVisMobDBID()
			{
				return CreateMobForm.GetVisMobDBID(this.newVMRadioButton.Checked, this.visMobTextBox.Text, this.scaleVisMob.Text);
			}

			// Token: 0x06001A01 RID: 6657 RVA: 0x000AB5B4 File Offset: 0x000AA5B4
			public string GetMobDBID()
			{
				return string.Format("{0}/{1}/{2}/{3}.({4}).xdb", new object[]
				{
					this.GetSelectedCreatureFolder(),
					"Instances",
					this.zoneComboBox.Text,
					this.fileNameTextBox.Text,
					"MobWorld"
				});
			}

			// Token: 0x06001A02 RID: 6658 RVA: 0x000AB608 File Offset: 0x000AA608
			public string GetSpawnTableDBID()
			{
				if (this.newSTRadioButton.Checked)
				{
					return ObjectGeneratorConfig.GetSpawnTableDBID(this.stMapComboBox.Text, this.zoneComboBox.Text, this.fileNameTextBox.Text);
				}
				return this.spawnTableTextBox.Text;
			}

			// Token: 0x06001A03 RID: 6659 RVA: 0x000AB654 File Offset: 0x000AA654
			public string GetKind()
			{
				return this.creatureComboBox.Text;
			}

			// Token: 0x06001A04 RID: 6660 RVA: 0x000AB661 File Offset: 0x000AA661
			public string GetOldVisMobDBID()
			{
				return this.visMobTextBox.Text;
			}

			// Token: 0x06001A05 RID: 6661 RVA: 0x000AB66E File Offset: 0x000AA66E
			public bool GetCreateNewVM()
			{
				return this.newVMRadioButton.Checked;
			}

			// Token: 0x06001A06 RID: 6662 RVA: 0x000AB67B File Offset: 0x000AA67B
			public bool GetCreateNewST()
			{
				return this.newSTRadioButton.Checked;
			}

			// Token: 0x06001A07 RID: 6663 RVA: 0x000AB688 File Offset: 0x000AA688
			public string GetVMScale()
			{
				return this.scaleVisMob.Text;
			}

			// Token: 0x06001A08 RID: 6664 RVA: 0x000AB695 File Offset: 0x000AA695
			public string GetFileName()
			{
				return this.fileNameTextBox.Text;
			}

			// Token: 0x06001A09 RID: 6665 RVA: 0x000AB6A4 File Offset: 0x000AA6A4
			public void PromptFileName(string mobKindSuffix)
			{
				if (!string.IsNullOrEmpty(this.minLevelTextBox.Text) && !string.IsNullOrEmpty(this.maxLevelTextBox.Text) && !string.IsNullOrEmpty(this.creatureComboBox.Text) && this.minLevelValidator.ValidateText() && this.maxLevelValidator.ValidateText())
				{
					this.fileNameTextBox.Text = string.Format("{0}{1}_{2}", this.GetSelectedCreatureName(mobKindSuffix), this.minLevelTextBox.Text, this.maxLevelTextBox.Text);
				}
			}

			// Token: 0x040010E9 RID: 4329
			private readonly ComboBox creatureComboBox;

			// Token: 0x040010EA RID: 4330
			private readonly TextBox visMobTextBox;

			// Token: 0x040010EB RID: 4331
			private readonly RadioButton newVMRadioButton;

			// Token: 0x040010EC RID: 4332
			private readonly TextBox scaleVisMob;

			// Token: 0x040010ED RID: 4333
			private readonly ComboBox zoneComboBox;

			// Token: 0x040010EE RID: 4334
			private readonly TextBox fileNameTextBox;

			// Token: 0x040010EF RID: 4335
			private readonly RadioButton newSTRadioButton;

			// Token: 0x040010F0 RID: 4336
			private readonly ComboBox stMapComboBox;

			// Token: 0x040010F1 RID: 4337
			private readonly TextBox spawnTableTextBox;

			// Token: 0x040010F2 RID: 4338
			private readonly TextBox minLevelTextBox;

			// Token: 0x040010F3 RID: 4339
			private readonly TextBox maxLevelTextBox;

			// Token: 0x040010F4 RID: 4340
			private readonly IntTextValidator minLevelValidator;

			// Token: 0x040010F5 RID: 4341
			private readonly IntTextValidator maxLevelValidator;
		}

		// Token: 0x02000219 RID: 537
		private class AdditionalFieldsParser
		{
			// Token: 0x06001A0A RID: 6666 RVA: 0x000AB733 File Offset: 0x000AA733
			public AdditionalFieldsParser(DBIDTextValidator _sourceMobValidator, TextBox _sourceMobTextBox, TextBox _visMobTextBox, RadioButton _newVMRadioButton, TextBox _scaleVisMob, RadioButton _createSilverRadioButton, TextBox _fileNameTextBox)
			{
				this.sourceMobValidator = _sourceMobValidator;
				this.sourceMobTextBox = _sourceMobTextBox;
				this.visMobTextBox = _visMobTextBox;
				this.newVMRadioButton = _newVMRadioButton;
				this.scaleVisMob = _scaleVisMob;
				this.createSilverRadioButton = _createSilverRadioButton;
				this.fileNameTextBox = _fileNameTextBox;
			}

			// Token: 0x06001A0B RID: 6667 RVA: 0x000AB770 File Offset: 0x000AA770
			public string GetSourceMobFolder()
			{
				if (this.sourceMobValidator.ValidateText(false))
				{
					return DBMethods.GetContainingFolderWithoutSlash(this.sourceMobTextBox.Text);
				}
				return string.Empty;
			}

			// Token: 0x06001A0C RID: 6668 RVA: 0x000AB796 File Offset: 0x000AA796
			public string GetVisMobDBID()
			{
				return CreateMobForm.GetVisMobDBID(this.newVMRadioButton.Checked, this.visMobTextBox.Text, this.scaleVisMob.Text);
			}

			// Token: 0x06001A0D RID: 6669 RVA: 0x000AB7C0 File Offset: 0x000AA7C0
			public string GetMobDBID()
			{
				int lastSlashIndex = this.sourceMobTextBox.Text.LastIndexOf('/');
				if (lastSlashIndex > -1)
				{
					string start = this.sourceMobTextBox.Text.Remove(lastSlashIndex);
					return string.Format("{0}/{1}.({2}).xdb", start, this.fileNameTextBox.Text, "MobWorld");
				}
				return string.Empty;
			}

			// Token: 0x06001A0E RID: 6670 RVA: 0x000AB817 File Offset: 0x000AA817
			public string GetSourceMobDBID()
			{
				return this.sourceMobTextBox.Text;
			}

			// Token: 0x06001A0F RID: 6671 RVA: 0x000AB824 File Offset: 0x000AA824
			public string GetOldVisMobDBID()
			{
				return this.visMobTextBox.Text;
			}

			// Token: 0x06001A10 RID: 6672 RVA: 0x000AB831 File Offset: 0x000AA831
			public bool GetCreateSilverMob()
			{
				return this.createSilverRadioButton.Checked;
			}

			// Token: 0x06001A11 RID: 6673 RVA: 0x000AB83E File Offset: 0x000AA83E
			public bool GetCreateNewVM()
			{
				return this.newVMRadioButton.Checked;
			}

			// Token: 0x06001A12 RID: 6674 RVA: 0x000AB84B File Offset: 0x000AA84B
			public string GetVMScale()
			{
				return this.scaleVisMob.Text;
			}

			// Token: 0x06001A13 RID: 6675 RVA: 0x000AB858 File Offset: 0x000AA858
			public string GetFileName()
			{
				return this.fileNameTextBox.Text;
			}

			// Token: 0x06001A14 RID: 6676 RVA: 0x000AB868 File Offset: 0x000AA868
			public void PromptFileName()
			{
				Regex regex = new Regex("(\\d+_)\\d+\\z");
				string mobDBID = IDatabase.CreateDBIDByName(this.sourceMobTextBox.Text).GetFileShortName();
				Match match = regex.Match(mobDBID);
				if (match.Success && match.Groups.Count > 1)
				{
					mobDBID = mobDBID.Remove(match.Groups[1].Index);
				}
				string suffix;
				if (this.createSilverRadioButton.Checked)
				{
					suffix = "Silver";
				}
				else
				{
					suffix = "Miniboss";
				}
				this.fileNameTextBox.Text = string.Format("{0}{1}", mobDBID, suffix);
			}

			// Token: 0x040010F6 RID: 4342
			private readonly DBIDTextValidator sourceMobValidator;

			// Token: 0x040010F7 RID: 4343
			private readonly TextBox sourceMobTextBox;

			// Token: 0x040010F8 RID: 4344
			private readonly TextBox visMobTextBox;

			// Token: 0x040010F9 RID: 4345
			private readonly RadioButton newVMRadioButton;

			// Token: 0x040010FA RID: 4346
			private readonly TextBox scaleVisMob;

			// Token: 0x040010FB RID: 4347
			private readonly RadioButton createSilverRadioButton;

			// Token: 0x040010FC RID: 4348
			private readonly TextBox fileNameTextBox;
		}

		// Token: 0x0200021A RID: 538
		private static class Genetator
		{
			// Token: 0x06001A15 RID: 6677 RVA: 0x000AB900 File Offset: 0x000AA900
			public static bool CommonGenerate(Form form, CreateMobForm.CommonFieldsParser commonFieldsParser, ObjectGeneratorConfig.MobGeneratorConfig config, bool neutral, string name, string title, string minLevel, string maxLevel, string speed, ref List<string> results)
			{
				IDatabase mainDb = IDatabase.GetMainDatabase();
				if (mainDb == null)
				{
					return false;
				}
				DBID mobDBID = IDatabase.CreateDBIDByName(commonFieldsParser.GetMobDBID());
				DBID visMobDBID = IDatabase.CreateDBIDByName(commonFieldsParser.GetVisMobDBID());
				DBID oldVisMobDBID = IDatabase.CreateDBIDByName(commonFieldsParser.GetOldVisMobDBID());
				DBID spawnTableDBID = IDatabase.CreateDBIDByName(commonFieldsParser.GetSpawnTableDBID());
				if (DBID.IsNullOrEmpty(mobDBID) || DBID.IsNullOrEmpty(visMobDBID) || DBID.IsNullOrEmpty(oldVisMobDBID) || DBID.IsNullOrEmpty(spawnTableDBID))
				{
					return false;
				}
				string kind = null;
				string faction = null;
				string quality = null;
				if (config != null)
				{
					kind = commonFieldsParser.GetKind();
					faction = (neutral ? config.NeutralFraction : config.WildFraction);
					quality = config.MobQuality;
				}
				if (string.IsNullOrEmpty(kind) || !File.Exists(EditorEnvironment.DataFolder + kind))
				{
					MessageBox.Show(string.Format(Strings.QUEST_EDITOR_CANT_FIND_OBJECT_FOR_FIELD_ERROR, kind, "kind"), Strings.QUEST_EDITOR_ERROR_TITLE, MessageBoxButtons.OK, MessageBoxIcon.Hand);
					return false;
				}
				if (string.IsNullOrEmpty(faction) || !File.Exists(EditorEnvironment.DataFolder + faction))
				{
					MessageBox.Show(string.Format(Strings.QUEST_EDITOR_CANT_FIND_OBJECT_FOR_FIELD_ERROR, faction, "faction"), Strings.QUEST_EDITOR_ERROR_TITLE, MessageBoxButtons.OK, MessageBoxIcon.Hand);
					return false;
				}
				if (string.IsNullOrEmpty(quality) || !File.Exists(EditorEnvironment.DataFolder + quality))
				{
					MessageBox.Show(string.Format(Strings.QUEST_EDITOR_CANT_FIND_OBJECT_FOR_FIELD_ERROR, quality, "quality"), Strings.QUEST_EDITOR_ERROR_TITLE, MessageBoxButtons.OK, MessageBoxIcon.Hand);
					return false;
				}
				Dictionary<DBID, string> dbidDict = new Dictionary<DBID, string>(3);
				dbidDict.Add(mobDBID, "gameMechanics.world.mob.MobWorld");
				if (commonFieldsParser.GetCreateNewST())
				{
					dbidDict.Add(spawnTableDBID, "gameMechanics.map.spawn.SpawnTable");
				}
				if (commonFieldsParser.GetCreateNewVM())
				{
					dbidDict.Add(visMobDBID, "VisualMob");
				}
				Dictionary<DBID, IObjMan> objDict = new Dictionary<DBID, IObjMan>(3);
				IObjMan mobMan;
				if (!ObjectGeneratorConfig.CreateNewObjects(form, mainDb, dbidDict, ref objDict) || !objDict.TryGetValue(mobDBID, out mobMan))
				{
					return false;
				}
				IObjMan stMan;
				if (!objDict.TryGetValue(spawnTableDBID, out stMan))
				{
					stMan = mainDb.GetManipulator(spawnTableDBID);
					if (stMan == null)
					{
						return false;
					}
				}
				IObjMan visMobMan;
				if (objDict.TryGetValue(visMobDBID, out visMobMan))
				{
					mainDb.CopyObject(visMobDBID, oldVisMobDBID);
					visMobMan.SetValue("scale", float.Parse(commonFieldsParser.GetVMScale()));
				}
				DBMethods.SetTextValue(mobMan, "name", name, commonFieldsParser.GetFileName() + "_Name.txt", false);
				DBMethods.SetTextValue(mobMan, "title", title, commonFieldsParser.GetFileName() + "_Title.txt", false);
				mobMan.SetValue("kind", kind);
				mobMan.SetValue("faction", faction);
				mobMan.SetValue("levelMin", int.Parse(minLevel));
				mobMan.SetValue("levelMax", int.Parse(maxLevel));
				mobMan.SetValue("walkSpeed", float.Parse(speed));
				mobMan.SetValue("quality", quality);
				mobMan.SetValue("visMob", visMobDBID);
				if (stMan != null)
				{
					ObjectGeneratorConfig.AddToSpawbTable(stMan, mobDBID, "commons", "gameMechanics.elements.spawn.TimeCommon");
				}
				mainDb.SaveChanges();
				results.Add(mobDBID.ToString());
				if (commonFieldsParser.GetCreateNewST())
				{
					results.Add(spawnTableDBID.ToString());
				}
				if (commonFieldsParser.GetCreateNewVM())
				{
					results.Add(visMobDBID.ToString());
				}
				return true;
			}

			// Token: 0x06001A16 RID: 6678 RVA: 0x000ABC08 File Offset: 0x000AAC08
			public static bool AdditionalGenerate(Form form, CreateMobForm.AdditionalFieldsParser additionalFieldsParser, ObjectGeneratorConfig.MobGeneratorConfig config, string name, string title, string spawnTable, ref List<string> results)
			{
				IDatabase mainDb = IDatabase.GetMainDatabase();
				if (mainDb == null)
				{
					return false;
				}
				bool silver = additionalFieldsParser.GetCreateSilverMob();
				bool newVM = additionalFieldsParser.GetCreateNewVM();
				DBID sourcsMobDBID = IDatabase.CreateDBIDByName(additionalFieldsParser.GetSourceMobDBID());
				DBID mobDBID = IDatabase.CreateDBIDByName(additionalFieldsParser.GetMobDBID());
				DBID visMobDBID = IDatabase.CreateDBIDByName(additionalFieldsParser.GetVisMobDBID());
				DBID oldVisMobDBID = IDatabase.CreateDBIDByName(additionalFieldsParser.GetOldVisMobDBID());
				if (DBID.IsNullOrEmpty(sourcsMobDBID) || DBID.IsNullOrEmpty(mobDBID) || DBID.IsNullOrEmpty(visMobDBID) || DBID.IsNullOrEmpty(oldVisMobDBID))
				{
					return false;
				}
				DBID spawnTableDBID = null;
				if (silver)
				{
					spawnTableDBID = IDatabase.CreateDBIDByName(spawnTable);
					if (DBID.IsNullOrEmpty(spawnTableDBID))
					{
						return false;
					}
				}
				IObjMan sourceMobMan = mainDb.GetManipulator(sourcsMobDBID);
				if (sourceMobMan == null)
				{
					return false;
				}
				IObjMan stMan = null;
				if (silver)
				{
					stMan = mainDb.GetManipulator(spawnTableDBID);
					if (stMan == null)
					{
						return false;
					}
				}
				string kind = null;
				string quality = null;
				if (config != null)
				{
					string kindSuff = silver ? config.SilverMobKindSuffix : config.MinibossMobKindSuffix;
					sourceMobMan.GetValue("kind", out kind);
					kind = kind.Replace(config.MobKindSuffix + ".xdb", kindSuff + ".xdb");
					quality = (silver ? config.SilverMobQuality : config.MinibossMobQuality);
				}
				if (string.IsNullOrEmpty(kind) || !File.Exists(EditorEnvironment.DataFolder + kind))
				{
					MessageBox.Show(string.Format(Strings.QUEST_EDITOR_CANT_FIND_OBJECT_FOR_FIELD_ERROR, kind, "kind"), Strings.QUEST_EDITOR_ERROR_TITLE, MessageBoxButtons.OK, MessageBoxIcon.Hand);
					return false;
				}
				if (string.IsNullOrEmpty(quality) || !File.Exists(EditorEnvironment.DataFolder + quality))
				{
					MessageBox.Show(string.Format(Strings.QUEST_EDITOR_CANT_FIND_OBJECT_FOR_FIELD_ERROR, quality, "quality"), Strings.QUEST_EDITOR_ERROR_TITLE, MessageBoxButtons.OK, MessageBoxIcon.Hand);
					return false;
				}
				Dictionary<DBID, string> dbidDict = new Dictionary<DBID, string>(3);
				dbidDict.Add(mobDBID, "gameMechanics.world.mob.MobWorld");
				if (newVM)
				{
					dbidDict.Add(visMobDBID, "VisualMob");
				}
				Dictionary<DBID, IObjMan> objDict = new Dictionary<DBID, IObjMan>(3);
				IObjMan mobMan;
				if (!ObjectGeneratorConfig.CreateNewObjects(form, mainDb, dbidDict, ref objDict) || !objDict.TryGetValue(mobDBID, out mobMan))
				{
					return false;
				}
				IObjMan visMobMan;
				if (objDict.TryGetValue(visMobDBID, out visMobMan))
				{
					mainDb.CopyObject(visMobDBID, oldVisMobDBID);
					visMobMan.SetValue("scale", float.Parse(additionalFieldsParser.GetVMScale()));
				}
				string faction;
				sourceMobMan.GetValue("faction", out faction);
				int level;
				sourceMobMan.GetValue("levelMax", out level);
				float speed;
				sourceMobMan.GetValue("walkSpeed", out speed);
				DBMethods.SetTextValue(mobMan, "name", name, additionalFieldsParser.GetFileName() + "_Name.txt", false);
				DBMethods.SetTextValue(mobMan, "title", title, additionalFieldsParser.GetFileName() + "_Title.txt", false);
				mobMan.SetValue("kind", kind);
				mobMan.SetValue("faction", faction);
				mobMan.SetValue("levelMin", level);
				mobMan.SetValue("levelMax", level);
				mobMan.SetValue("walkSpeed", speed);
				mobMan.SetValue("quality", quality);
				mobMan.SetValue("visMob", visMobDBID);
				if (silver)
				{
					ObjectGeneratorConfig.AddToSpawbTable(stMan, mobDBID, "singles", "gameMechanics.elements.spawn.TimeTrash");
				}
				mainDb.SaveChanges();
				results.Add(mobDBID.ToString());
				if (newVM)
				{
					results.Add(visMobDBID.ToString());
				}
				return true;
			}
		}
	}
}
