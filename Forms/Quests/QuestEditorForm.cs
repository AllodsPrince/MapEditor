using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using Db;
using EditorControls.PropertyControlDialog;
using InputState;
using MapEditor.Forms.Base;
using MapEditor.Forms.PropertyControl.ExtendedPropertyControl;
using MapEditor.Forms.Quests.QuestLine;
using MapEditor.Forms.Quests.QuickObjectGenerator;
using MapEditor.Resources.Images;
using MapEditor.Resources.Strings;
using Tools.DbCommon;
using Tools.DBGameObjects;
using Tools.DBGameObjects.GameObjects;
using Tools.EditorImage;
using Tools.Geometry;
using Tools.InputBox;
using Tools.Progress;

namespace MapEditor.Forms.Quests
{
	// Token: 0x020000D9 RID: 217
	public partial class QuestEditorForm : BaseForm
	{
		// Token: 0x06000B18 RID: 2840 RVA: 0x0005BA38 File Offset: 0x0005AA38
		private static bool OnToolStripClicked(MainForm.Context context, IWin32Window owner, string tag, string oldValueDBID, out string newValueDBID)
		{
			newValueDBID = null;
			if (tag == "clear_cell")
			{
				return true;
			}
			if (tag == "create_mob")
			{
				if (context != null)
				{
					CreateMobForm dialogForm = context.CreateMobForm;
					if (dialogForm.Visible)
					{
						dialogForm.Close();
					}
					dialogForm.ShowDialog();
					newValueDBID = dialogForm.CreatedMob;
					return !string.IsNullOrEmpty(newValueDBID);
				}
			}
			else if (tag == "create_npc")
			{
				if (context != null)
				{
					CreateNPCForm dialogForm2 = context.CreateNPCForm;
					if (dialogForm2.Visible)
					{
						dialogForm2.Close();
					}
					dialogForm2.ShowDialog();
					newValueDBID = dialogForm2.CreatedNPC;
					return !string.IsNullOrEmpty(newValueDBID);
				}
			}
			else if (tag == "create_quest_item")
			{
				if (context != null)
				{
					CreateQuestItemForm dialogForm3 = context.CreateQuestItemForm;
					if (dialogForm3.Visible)
					{
						dialogForm3.Close();
					}
					dialogForm3.ShowDialog();
					newValueDBID = dialogForm3.CreatedItem;
					return !string.IsNullOrEmpty(newValueDBID);
				}
			}
			else if (tag == "create_device")
			{
				if (context != null)
				{
					CreateDeviceForm dialogForm4 = context.CreateDeviceForm;
					if (dialogForm4.Visible)
					{
						dialogForm4.Close();
					}
					dialogForm4.ShowDialog();
					newValueDBID = dialogForm4.CreatedDevice;
					return !string.IsNullOrEmpty(newValueDBID);
				}
			}
			else
			{
				if (tag == "edit_game_name")
				{
					newValueDBID = oldValueDBID;
					IDatabase mainDb = IDatabase.GetMainDatabase();
					if (mainDb != null && !string.IsNullOrEmpty(oldValueDBID))
					{
						DBID dbid = mainDb.GetDBIDByName(oldValueDBID);
						IObjMan objMan = mainDb.GetManipulator(dbid);
						if (objMan != null)
						{
							string gameNameField = "name";
							if (objMan.GetFieldDesc(gameNameField) != null)
							{
								string name = DBMethods.GetTextValue(objMan, gameNameField, false);
								InputBoxForm inputBox = new InputBoxForm();
								inputBox.InputText = name;
								inputBox.InputCaption = Strings.QUEST_EDITOR_GAME_NAME_TITLE;
								if (inputBox.ShowDialog(owner) == DialogResult.OK)
								{
									DBMethods.SetTextValue(objMan, gameNameField, inputBox.InputText, dbid.GetFileShortName() + "_Name.txt", false);
									mainDb.SaveChanges();
									return true;
								}
							}
						}
					}
					return false;
				}
				if (tag == "edit_properties")
				{
					if (!string.IsNullOrEmpty(oldValueDBID))
					{
						newValueDBID = oldValueDBID;
						PropertyControlDialogForm properties = new PropertyControlDialogForm(EditorEnvironment.EditorFormsFolder);
						properties.SetObject(oldValueDBID);
						properties.ShowDialog(owner);
						return true;
					}
				}
				else if (tag == "find_on_map" && context != null)
				{
					context.MainState.ActiveState = 0;
					context.StateContainer.Invoke("_select_object_by_string_key", new MethodArgs(null, oldValueDBID, null));
				}
			}
			return false;
		}

		// Token: 0x06000B19 RID: 2841 RVA: 0x0005BC90 File Offset: 0x0005AC90
		private static void EditCellContextMenu(DataGridViewCell cell)
		{
			if (cell != null && cell.ContextMenuStrip != null)
			{
				GameObjectClass gameObj = cell.Value as GameObjectClass;
				foreach (object obj in cell.ContextMenuStrip.Items)
				{
					ToolStripMenuItem item = (ToolStripMenuItem)obj;
					string tag = item.Tag as string;
					string a;
					if ((a = tag) != null && (a == "edit_game_name" || a == "edit_properties"))
					{
						item.Visible = (gameObj != null);
					}
				}
			}
		}

		// Token: 0x06000B1C RID: 2844 RVA: 0x00060320 File Offset: 0x0005F320
		private void OnFormLoad(object sender, EventArgs e)
		{
			this.questEditorFormData.LoadQuestTypeList(this.QuestTypeComboBox);
			this.questEditorFormData.LoadCharacterList(this.CharacterClassComboBox);
			this.rewMandatoryItemsListener = new QuestEditorForm.SimpleGridListener(base.Context, this.MandatoryItemsMenuStrip, this.RewMandatoryItemsGrid, this.rewItemCol.Keys, base.Context.QuestEnvironment.Items);
			this.rewAlternativeItemsListener = new QuestEditorForm.SimpleGridListener(base.Context, this.AlternativeItemsMenuStrip, this.RewAlternativeItemsGrid, this.rewItemCol.Keys, base.Context.QuestEnvironment.Items);
			this.finishedQuestListener = new QuestEditorForm.SimpleGridListener(base.Context, this.PrevQuestMenuStrip, this.FinishedQuestGrid, 0, base.Context.QuestEnvironment.Quests);
			this.notStartedQuestListener = new QuestEditorForm.SimpleGridListener(base.Context, this.RestrQuestMenuStrip, this.NotStartedQuestGrid, 0, base.Context.QuestEnvironment.Quests);
			this.questGiverListener = new QuestEditorForm.SimpleStringValueListener(base.Context, this.QuestGiverTextBox, this.ChooseQuestGiverButton, base.Context.QuestEnvironment.QuestGivers);
			this.compleaterListener = new QuestEditorForm.SimpleStringValueListener(base.Context, this.CompleterTextBox, this.ChooseCompeterButton, base.Context.QuestEnvironment.QuestGivers);
			this.lootTableListenerMobListener = new QuestEditorForm.SimpleGridListener(base.Context, this.LootTableMobMenuStrip, this.LootTableGrid, 0, base.Context.QuestEnvironment.LootableObjects);
			this.lootTableListenerItemListener = new QuestEditorForm.SimpleGridListener(base.Context, this.LootTableItemMenuStrip, this.LootTableGrid, 1, base.Context.QuestEnvironment.Items);
			this.rewReputationListener = new QuestEditorForm.SimpleGridListener(base.Context, null, this.RewReputationGrid, 0, base.Context.QuestEnvironment.Factions);
			this.rewCurrencyListener = new QuestEditorForm.SimpleGridListener(base.Context, null, this.RewCurrencyGrid, 0, base.Context.QuestEnvironment.Currencies);
			this.counterGridHandler = new QuestEditorForm.CounterGridHandler(this.questEditorFormData, base.Context, this.CounterMenuStrip, this.CounterGrid, base.Context.QuestEnvironment.Items, base.Context.QuestEnvironment.RespawnableResources, base.Context.QuestEnvironment.KillAvatarCounterObjects);
			this.questMarker.Init();
			this.rewMandatoryItemsListener.Changed += this.OnDataChanged;
			this.rewAlternativeItemsListener.Changed += this.OnDataChanged;
			this.finishedQuestListener.Changed += this.OnDataChanged;
			this.notStartedQuestListener.Changed += this.OnDataChanged;
			this.compleaterListener.Changed += this.OnDataChanged;
			this.questGiverListener.Changed += this.OnDataChanged;
			this.lootTableListenerMobListener.Changed += this.OnDataChanged;
			this.lootTableListenerItemListener.Changed += this.OnDataChanged;
			this.rewReputationListener.Changed += this.OnDataChanged;
			this.rewCurrencyListener.Changed += this.OnDataChanged;
			this.counterGridHandler.Changed += this.OnDataChanged;
			base.Context.QuestEnvironment.QuestEditor.LoadZone += this.OnLoadZone;
			base.Context.QuestEnvironment.QuestEditor.LoadQuest += this.OnLoadQuest;
			base.Context.QuestEnvironment.QuestEditor.CreateNewQuestDelegate += this.OnCreateNewQuest;
			base.Context.QuestEnvironment.ZoneLoaded += this.OnZoneLoaded;
			base.Context.AddCloseFormEvent(new MainForm.Context.CloseFormEvent(this.OnMainFormClosingEvent));
			this.SetFormCondition();
			foreach (object obj in this.MainTabControl.TabPages)
			{
				TabPage tabPage = (TabPage)obj;
				foreach (object obj2 in tabPage.Controls)
				{
					Control control = (Control)obj2;
					TextBoxBase textBox = control as TextBoxBase;
					if (textBox != null)
					{
						textBox.KeyDown += QuestEditorForm.OnTextBoxKeyDown;
					}
				}
			}
		}

		// Token: 0x06000B1D RID: 2845 RVA: 0x000607CC File Offset: 0x0005F7CC
		private void OnMainFormClosingEvent(FormClosingEventArgs e)
		{
			if (base.Visible)
			{
				e.Cancel = !this.PrompSave(base.Context.MainForm);
			}
		}

		// Token: 0x06000B1E RID: 2846 RVA: 0x000607F0 File Offset: 0x0005F7F0
		private void OnFormClosing(object sender, FormClosingEventArgs e)
		{
			if (e.CloseReason == CloseReason.UserClosing && !base.Context.FormClosing)
			{
				e.Cancel = true;
				if (base.Visible && this.PrompSave(base.Context.MainForm))
				{
					this.questEditorFormData.CurrentQuest = null;
					this.ClearAll();
					this.SetFormCondition();
					base.Visible = false;
				}
			}
		}

		// Token: 0x06000B1F RID: 2847 RVA: 0x00060854 File Offset: 0x0005F854
		private void ChooseQuest(object sender, EventArgs e)
		{
			if (this.questEditorFormData != null && this.PrompSave(this))
			{
				QuestSelectDialogForm dialogForm = new QuestSelectDialogForm(base.Context.QuestEnvironment, base.Context.QuestEnvironment.Quests, this.questEditorFormData.CurrentQuest);
				if (dialogForm.ShowDialog() == DialogResult.OK && dialogForm.GetSelectedItem() != this.questEditorFormData.CurrentQuest)
				{
					this.ClearAll();
					if (dialogForm.GetSelectedItem() != null)
					{
						this.LoadQuest(dialogForm.GetSelectedItem());
						return;
					}
					this.questEditorFormData.CurrentQuest = null;
					this.ClearAll();
					this.SetFormCondition();
				}
			}
		}

		// Token: 0x06000B20 RID: 2848 RVA: 0x000608F0 File Offset: 0x0005F8F0
		private void OnAddNewQuest(object sender, EventArgs e)
		{
			if (this.PrompSave(this))
			{
				this.AddNewQuest();
			}
		}

		// Token: 0x06000B21 RID: 2849 RVA: 0x00060901 File Offset: 0x0005F901
		private void OnSaveAsButton(object sender, EventArgs e)
		{
			this.SaveAs();
		}

		// Token: 0x06000B22 RID: 2850 RVA: 0x0006090C File Offset: 0x0005F90C
		private void DeleteQuest(object sender, EventArgs e)
		{
			if (this.questEditorFormData.CurrentQuest != null && MessageBox.Show(Strings.QUEST_EDITOR_DELETE_MESSAGE, Strings.QUEST_EDITOR_TITLE, MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) == DialogResult.Yes)
			{
				List<string> errors;
				if (!this.questEditorFormData.CheckBeforDelete(out errors))
				{
					foreach (string error in errors)
					{
						if (MessageBox.Show(error, Strings.QUEST_EDITOR_TITLE, MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) == DialogResult.No)
						{
							return;
						}
					}
				}
				Cursor.Current = Cursors.WaitCursor;
				if (this.questEditorFormData.DeleteQuest())
				{
					this.ClearAll();
					this.SetFormCondition();
					Cursor.Current = Cursors.Default;
					return;
				}
				Cursor.Current = Cursors.Default;
				MessageBox.Show(Strings.QUEST_EDITOR_DELETEQUEST_ERROR, Strings.QUEST_EDITOR_ERROR_TITLE, MessageBoxButtons.OK, MessageBoxIcon.Hand);
			}
		}

		// Token: 0x06000B23 RID: 2851 RVA: 0x000609E8 File Offset: 0x0005F9E8
		private void LoadQuest(GameObjectClass gameObject)
		{
			this.questEditorFormData.CurrentQuest = (gameObject as QuestClass);
			if (this.questEditorFormData.CurrentQuest == null)
			{
				return;
			}
			this.ResurceIdTextBox.Text = this.questEditorFormData.GetResourceId().ToString();
			this.QuestTypeComboBox.Text = this.questEditorFormData.CurrentQuest.QuestType;
			this.SecretSequenceCheckBox.Checked = this.questEditorFormData.CurrentQuest.SecretSequence;
			this.PVPCheckBox.Checked = this.questEditorFormData.CurrentQuest.PVP;
			this.TutorialCheckBox.Checked = this.questEditorFormData.CurrentQuest.Tutorial;
			this.FilePathTextBox.Text = this.questEditorFormData.CurrentQuest.GetShortFileName();
			this.QuestLevelTextBox.Text = this.questEditorFormData.CurrentQuest.Level.ToString();
			this.QuestNameTextBox.Text = this.questEditorFormData.CurrentQuest.Name;
			this.ZoneTextBox.Text = this.questEditorFormData.CurrentQuest.GetZoneFolder();
			this.GameNameTextBox.Text = this.questEditorFormData.CurrentQuest.GameName;
			this.LoadQuestGiver();
			this.PlotLineTextBox.Text = this.questEditorFormData.CurrentQuest.PlotLine;
			this.QuestPropertyControl.SelectedObject = this.questEditorFormData.CurrentQuest.QuestMan;
			List<string> finishedQuests;
			List<string> notStartedQuests;
			string characterClass;
			this.questEditorFormData.CurrentQuest.GetPrerequisits(out finishedQuests, out notStartedQuests, out characterClass);
			this.LoadPrereqQuests(this.FinishedQuestGrid, finishedQuests);
			this.LoadPrereqQuests(this.NotStartedQuestGrid, notStartedQuests);
			this.PlayerLevelTextBox.Text = this.questEditorFormData.CurrentQuest.RequiredLevel.ToString();
			this.IssueTextBox.Text = this.questEditorFormData.CurrentQuest.IssueText;
			this.GoalTextBox.Text = this.questEditorFormData.CurrentQuest.GoalText;
			this.NotCompletedTextBox.Text = this.questEditorFormData.CurrentQuest.NotCompletedText;
			this.KickTextBox.Text = this.questEditorFormData.CurrentQuest.KickText;
			this.LoadCharacterClass(characterClass);
			this.compleaterListener.SetValueByKey(this.questEditorFormData.CurrentQuest.Finisher);
			this.counterGridHandler.Load();
			this.RewExpQtyTextBox.Text = this.questEditorFormData.CurrentQuest.RewExpereince.ToString();
			this.experienceValidator.Validate(false);
			this.moneyValidator.Money = this.questEditorFormData.CurrentQuest.RewMoney;
			this.RewHonorQtyTextBox.Text = this.questEditorFormData.CurrentQuest.RewHonor.ToString();
			Dictionary<string, QuestClass.Pair<int, bool>> rewardItems;
			this.questEditorFormData.CurrentQuest.GetMandatoryRewardItems(out rewardItems);
			this.LoadRewItemsGrid(rewardItems, this.RewMandatoryItemsGrid);
			this.questEditorFormData.CurrentQuest.GetAlternativeRewardItems(out rewardItems);
			this.LoadRewItemsGrid(rewardItems, this.RewAlternativeItemsGrid);
			Dictionary<string, int> rewReputation;
			this.questEditorFormData.CurrentQuest.GetRewardReputations(out rewReputation);
			this.LoadDictionaryToGrid(this.RewReputationGrid, rewReputation, base.Context.QuestEnvironment.Factions, 0, 1);
			Dictionary<string, int> rewCurrency;
			this.questEditorFormData.CurrentQuest.GetRewardCurrencies(out rewCurrency);
			this.LoadDictionaryToGrid(this.RewCurrencyGrid, rewCurrency, base.Context.QuestEnvironment.Currencies, 0, 1);
			this.CompletedTextBox.Text = this.questEditorFormData.CurrentQuest.CompletedText;
			this.LoadLootTableGrid();
			QuestClass.QuestLocator goalLocator;
			QuestClass.QuestLocator[] additionalGoalLocators;
			this.questEditorFormData.CurrentQuest.GetGoalLocator(out goalLocator, out additionalGoalLocators);
			QuestClass.QuestLocator returnLocator;
			this.questEditorFormData.CurrentQuest.GetReturnLocator(out returnLocator);
			this.questMarker.Load(this.questEditorFormData.CurrentQuest.AutoSetGoalLocation, goalLocator, additionalGoalLocators, returnLocator, this.questEditorFormData.CurrentQuest.AutoSetReturnLocation, this.questEditorFormData.CurrentQuest.Zone);
			this.SetFormCondition();
		}

		// Token: 0x06000B24 RID: 2852 RVA: 0x00060DFC File Offset: 0x0005FDFC
		private bool Save()
		{
			if (!this.CheckData())
			{
				return false;
			}
			if (string.IsNullOrEmpty(this.QuestGiverTextBox.Text) && MessageBox.Show(Strings.QUEST_EDITOR_QUESTGIVER_WARNING, Strings.QUEST_EDITOR_TITLE, MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) == DialogResult.No)
			{
				this.MainTabControl.SelectedTab = this.CommonTabPage;
				return false;
			}
			Cursor.Current = Cursors.WaitCursor;
			if (this.questEditorFormData.IsNew)
			{
				DBID zone;
				if (!this.questEditorFormData.CheckNewQuestBeforeCreate(out zone))
				{
					return false;
				}
				if (!this.questEditorFormData.AddNewQuest(zone))
				{
					MessageBox.Show(Strings.QUEST_EDITOR_SAVE_ERROR_MESSAGE, Strings.QUEST_EDITOR_ERROR_TITLE, MessageBoxButtons.OK, MessageBoxIcon.Hand);
					Cursor.Current = Cursors.Default;
					return false;
				}
			}
			if (this.questEditorFormData.CurrentQuest == null)
			{
				MessageBox.Show(Strings.QUEST_EDITOR_SAVE_ERROR_MESSAGE, Strings.QUEST_EDITOR_ERROR_TITLE, MessageBoxButtons.OK, MessageBoxIcon.Hand);
				Cursor.Current = Cursors.Default;
				return false;
			}
			if (this.isChangedThroughProprtyControl)
			{
				this.questEditorFormData.SaveDb();
			}
			this.questEditorFormData.CurrentQuest.QuestType = this.QuestTypeComboBox.Text;
			this.questEditorFormData.CurrentQuest.SecretSequence = this.SecretSequenceCheckBox.Checked;
			this.questEditorFormData.CurrentQuest.PVP = this.PVPCheckBox.Checked;
			this.questEditorFormData.CurrentQuest.Tutorial = this.TutorialCheckBox.Checked;
			this.questEditorFormData.CurrentQuest.SetName(this.QuestNameTextBox.Text);
			this.questEditorFormData.CurrentQuest.Level = QuestEditorForm.QuestEditorFormData.ConvertToInteger(this.QuestLevelTextBox.Text);
			this.questEditorFormData.CurrentQuest.SetGameName(this.GameNameTextBox.Text);
			this.questEditorFormData.CurrentQuest.RequiredLevel = QuestEditorForm.QuestEditorFormData.ConvertToInteger(this.PlayerLevelTextBox.Text);
			this.questEditorFormData.CurrentQuest.SetPrerequisits(QuestEditorForm.GetPrereqQuests(this.FinishedQuestGrid), QuestEditorForm.GetPrereqQuests(this.NotStartedQuestGrid), this.GetCharacterClass());
			this.questEditorFormData.CurrentQuest.IssueText = this.IssueTextBox.Text;
			this.questEditorFormData.CurrentQuest.GoalText = this.GoalTextBox.Text;
			this.SavePlotLine();
			Dictionary<int, QuestClass.QuestCounter> oldCounters;
			List<QuestClass.QuestCounter> newCounters;
			this.counterGridHandler.GetValues(out oldCounters, out newCounters);
			this.questEditorFormData.CurrentQuest.SetCounters(oldCounters, newCounters);
			this.questEditorFormData.CurrentQuest.NotCompletedText = this.NotCompletedTextBox.Text;
			this.questEditorFormData.CurrentQuest.KickText = this.KickTextBox.Text;
			this.questEditorFormData.CurrentQuest.RewExpereince = QuestEditorForm.QuestEditorFormData.ConvertToInteger(this.RewExpQtyTextBox.Text);
			this.questEditorFormData.CurrentQuest.RewMoney = this.moneyValidator.Money;
			this.questEditorFormData.CurrentQuest.RewHonor = QuestEditorForm.QuestEditorFormData.ConvertToInteger(this.RewHonorQtyTextBox.Text);
			Dictionary<string, QuestClass.Pair<int, bool>> rewardItems;
			this.SaveRewItemsGrid(this.RewMandatoryItemsGrid, out rewardItems);
			this.questEditorFormData.CurrentQuest.SetMandatoryRewardItems(rewardItems);
			this.SaveRewItemsGrid(this.RewAlternativeItemsGrid, out rewardItems);
			this.questEditorFormData.CurrentQuest.SetAlternativeRewardItems(rewardItems);
			Dictionary<string, int> rewReputation;
			QuestEditorForm.SaveDictionaryFromGrid(this.RewReputationGrid, out rewReputation, 0, 1);
			this.questEditorFormData.CurrentQuest.SetRewardReputations(rewReputation);
			Dictionary<string, int> rewCurrency;
			QuestEditorForm.SaveDictionaryFromGrid(this.RewCurrencyGrid, out rewCurrency, 0, 1);
			this.questEditorFormData.CurrentQuest.SetRewardCurrencies(rewCurrency);
			if (this.questGiverListener.Enabled)
			{
				this.questEditorFormData.SaveQuestGiver(this.questGiverListener.GetValue());
			}
			this.questEditorFormData.CurrentQuest.Finisher = this.compleaterListener.GetValue();
			this.questEditorFormData.CurrentQuest.CompletedText = this.CompletedTextBox.Text;
			this.SaveLootTableGrid();
			this.questEditorFormData.CurrentQuest.AutoSetGoalLocation = this.questMarker.GoalAutoSet;
			QuestClass.QuestLocator[] goalLocators;
			this.questMarker.GetGoalLocators(out goalLocators);
			this.questEditorFormData.CurrentQuest.SetGoalLocators(goalLocators);
			this.questEditorFormData.CurrentQuest.AutoSetReturnLocation = this.questMarker.ReturnAutoSet;
			this.questEditorFormData.CurrentQuest.SetReturnLocator(this.questMarker.ReturnLocator);
			this.questEditorFormData.SaveDb();
			this.LoadQuest(this.questEditorFormData.CurrentQuest);
			this.InvokeQuestChangedMessage(this);
			Cursor.Current = Cursors.Default;
			return true;
		}

		// Token: 0x06000B25 RID: 2853 RVA: 0x00061260 File Offset: 0x00060260
		private void CreateNewQuest(string zone, string questName, QuestGiverClass questGiver)
		{
			if (this.PrompSave(this))
			{
				this.ClearAll();
				this.questEditorFormData.CurrentQuest = null;
				if (this.questEditorFormData.SetNewQuestFileName(questName, zone + '/'))
				{
					this.QuestNameTextBox.Text = questName;
					this.FilePathTextBox.Text = questName;
					this.ZoneTextBox.Text = zone;
					this.questGiverListener.Enabled = true;
					if (this.questGiverListener != null && questGiver != null)
					{
						this.questGiverListener.SetValue(questGiver);
					}
					this.Cursor = Cursors.WaitCursor;
					base.Context.QuestEnvironment.Load(zone);
					this.Cursor = Cursors.Default;
				}
				else
				{
					MessageBox.Show(Strings.QUEST_EDITOR_NEWQUEST_ERROR, Strings.QUEST_EDITOR_ERROR_TITLE, MessageBoxButtons.OK, MessageBoxIcon.Hand);
				}
				this.SetFormCondition();
			}
		}

		// Token: 0x06000B26 RID: 2854 RVA: 0x00061330 File Offset: 0x00060330
		private void AddNewQuest()
		{
			AddQuestDialog dialog = new AddQuestDialog(this.ZoneTextBox.Text);
			if (dialog.ShowDialog(this) == DialogResult.OK)
			{
				this.CreateNewQuest(dialog.QuestZone, dialog.QuestName, null);
			}
		}

		// Token: 0x06000B27 RID: 2855 RVA: 0x0006136C File Offset: 0x0006036C
		private void SaveAs()
		{
			if (this.questEditorFormData.CurrentQuest != null && this.CheckData())
			{
				AddQuestDialog dialog = new AddQuestDialog(this.ZoneTextBox.Text);
				if (dialog.ShowDialog(this) == DialogResult.OK)
				{
					if (this.questEditorFormData.SetNewQuestFileName(dialog.QuestName, dialog.QuestZone + '/'))
					{
						if (this.questEditorFormData.SaveQuestAs())
						{
							this.QuestNameTextBox.Text = this.questEditorFormData.CurrentQuest.GetShortFileName();
							this.counterGridHandler.ClearOldCounters();
							this.Save();
							return;
						}
					}
					else
					{
						MessageBox.Show(Strings.QUEST_EDITOR_NEWQUEST_ERROR, Strings.QUEST_EDITOR_ERROR_TITLE, MessageBoxButtons.OK, MessageBoxIcon.Hand);
					}
				}
			}
		}

		// Token: 0x06000B28 RID: 2856 RVA: 0x00061424 File Offset: 0x00060424
		private void InvokeQuestChangedMessage(object sender)
		{
			if (this.questEditorFormData.CurrentQuest != null)
			{
				List<string> changedQuest = new List<string>(1);
				changedQuest.Add(this.questEditorFormData.CurrentQuest.GameObject);
				base.Context.QuestEnvironment.InvokeQuestChanged(sender, changedQuest);
			}
		}

		// Token: 0x06000B29 RID: 2857 RVA: 0x0006146D File Offset: 0x0006046D
		private void OnSave(object sender, EventArgs e)
		{
			this.Save();
		}

		// Token: 0x06000B2A RID: 2858 RVA: 0x00061478 File Offset: 0x00060478
		private void OnDataChanged(object sender, EventArgs e)
		{
			if (this.questEditorFormData != null && (this.questEditorFormData.CurrentQuest != null || this.questEditorFormData.IsNew))
			{
				this.isChanged = true;
				if (sender == this.FinishedQuestGrid)
				{
					this.prevQuestChanged = true;
					if (this.FinishedQuestGrid.Rows.Count == 2)
					{
						QuestClass prevQuest = this.FinishedQuestGrid.Rows[0].Cells[0].Value as QuestClass;
						if (prevQuest != null && !string.IsNullOrEmpty(prevQuest.PlotLine))
						{
							this.PlotLineTextBox.Text = prevQuest.PlotLine;
						}
					}
				}
			}
		}

		// Token: 0x06000B2B RID: 2859 RVA: 0x0006151C File Offset: 0x0006051C
		private void OnEnviromentQuestChanged(object sender, IEnumerable<string> changedQuests)
		{
			if (sender == this || this.questEditorFormData.CurrentQuest == null)
			{
				return;
			}
			bool needReload = false;
			foreach (string changedQuestDBID in changedQuests)
			{
				if (changedQuestDBID == this.questEditorFormData.CurrentQuest.GameObject)
				{
					needReload = true;
					break;
				}
			}
			if (needReload)
			{
				if (!base.Visible)
				{
					this.LoadQuest(this.questEditorFormData.CurrentQuest);
					return;
				}
				if (MessageBox.Show(this, Strings.QUEST_EDITOR_QUESTS_EDITED_OUTSIDE_MESSAGE, Strings.QUEST_EDITOR_TITLE, MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) == DialogResult.Yes)
				{
					Cursor.Current = Cursors.WaitCursor;
					this.LoadQuest(this.questEditorFormData.CurrentQuest);
					Cursor.Current = Cursors.Default;
				}
			}
		}

		// Token: 0x06000B2C RID: 2860 RVA: 0x000615E8 File Offset: 0x000605E8
		private void OnDiagramClick(object sender, EventArgs e)
		{
			this.Cursor = Cursors.WaitCursor;
			base.Context.QuestEnvironment.QuestEditor.LoadQuestToQuestDiagram(this.questEditorFormData.CurrentQuest);
			this.Cursor = Cursors.Default;
		}

		// Token: 0x06000B2D RID: 2861 RVA: 0x00061620 File Offset: 0x00060620
		private void OnShowQuestLineClick(object sender, EventArgs e)
		{
			this.questLine.Show(this.questEditorFormData.CurrentQuest);
		}

		// Token: 0x06000B2E RID: 2862 RVA: 0x00061638 File Offset: 0x00060638
		private void OnCloseButtonClick(object sender, EventArgs e)
		{
			base.Close();
		}

		// Token: 0x06000B2F RID: 2863 RVA: 0x00061640 File Offset: 0x00060640
		private void OnEnterQuestPropertyControl(object sender, EventArgs e)
		{
			this.isChangedThroughProprtyControl = true;
		}

		// Token: 0x06000B30 RID: 2864 RVA: 0x00061649 File Offset: 0x00060649
		private void OnPropertControlSaved()
		{
			this.isChangedThroughProprtyControl = false;
			this.InvokeQuestChangedMessage(this.QuestPropertyControl);
		}

		// Token: 0x06000B31 RID: 2865 RVA: 0x00061660 File Offset: 0x00060660
		private static void OnTextBoxKeyDown(object sender, KeyEventArgs e)
		{
			if (e.Control && e.KeyCode == Keys.V)
			{
				TextBoxBase textBox = sender as TextBoxBase;
				if (textBox != null)
				{
					string text = Clipboard.GetText();
					if (text != null)
					{
						int index = textBox.SelectionStart;
						if (index == -1)
						{
							textBox.AppendText(text);
							return;
						}
						textBox.Text.Remove(index, textBox.SelectionLength);
						textBox.Text.Insert(index, text);
					}
				}
			}
		}

		// Token: 0x06000B32 RID: 2866 RVA: 0x000616C8 File Offset: 0x000606C8
		private void ClearAll()
		{
			this.ResurceIdTextBox.Text = string.Empty;
			this.QuestTypeComboBox.SelectedIndex = ((this.QuestTypeComboBox.Items.Count > 0) ? 0 : -1);
			this.SecretSequenceCheckBox.Checked = false;
			this.PVPCheckBox.Checked = false;
			this.TutorialCheckBox.Checked = false;
			this.QuestTypeLabel.Text = string.Empty;
			this.FilePathTextBox.Text = string.Empty;
			this.QuestNameTextBox.Text = string.Empty;
			this.QuestLevelTextBox.Text = string.Empty;
			this.GameNameTextBox.Text = string.Empty;
			this.FinishedQuestGrid.Rows.Clear();
			this.NotStartedQuestGrid.Rows.Clear();
			this.CharacterClassComboBox.Text = string.Empty;
			this.questGiverListener.SetValue(null);
			this.compleaterListener.SetValue(null);
			this.PlayerLevelTextBox.Text = string.Empty;
			this.PlotLineTextBox.Text = string.Empty;
			this.RewExpQtyTextBox.Text = string.Empty;
			this.RewExpRealQtyLabel.Text = string.Empty;
			this.moneyValidator.Money = 0;
			QuestEditorForm.InitGrid(this.CounterGrid, 6);
			QuestEditorForm.InitGrid(this.RewMandatoryItemsGrid, 3);
			QuestEditorForm.InitGrid(this.RewAlternativeItemsGrid, 3);
			this.RewReputationGrid.Rows.Clear();
			this.RewCurrencyGrid.Rows.Clear();
			this.QuestPropertyControl.SelectedObject = null;
			this.IssueTextBox.Text = string.Empty;
			this.GoalTextBox.Text = string.Empty;
			this.NotCompletedTextBox.Text = string.Empty;
			this.KickTextBox.Text = string.Empty;
			this.CompletedTextBox.Text = string.Empty;
			this.LootTableGrid.Rows.Clear();
			this.MainTabControl.SelectedTab = this.CommonTabPage;
			this.questMarker.Load(true, null, null, null, true, string.Empty);
		}

		// Token: 0x06000B33 RID: 2867 RVA: 0x000618E8 File Offset: 0x000608E8
		private bool CheckData()
		{
			QuestEditorForm.ErrorTypes[] array = new QuestEditorForm.ErrorTypes[1];
			QuestEditorForm.ErrorTypes[] mandatoryError = array;
			QuestEditorForm.ErrorTypes[] intError = new QuestEditorForm.ErrorTypes[]
			{
				QuestEditorForm.ErrorTypes.INT_FORMAT
			};
			if (!QuestEditorForm.CheckValue(this.QuestTypeComboBox.Text, new List<QuestEditorForm.ErrorTypes>(mandatoryError), true))
			{
				this.MainTabControl.SelectedTab = this.CommonTabPage;
				return false;
			}
			if (!QuestEditorForm.CheckValue(this.QuestNameTextBox.Text, new List<QuestEditorForm.ErrorTypes>(mandatoryError), true))
			{
				this.MainTabControl.SelectedTab = this.CommonTabPage;
				return false;
			}
			if (!QuestEditorForm.CheckValue(this.QuestLevelTextBox.Text, new List<QuestEditorForm.ErrorTypes>(intError), true))
			{
				this.MainTabControl.SelectedTab = this.CommonTabPage;
				return false;
			}
			if (!QuestEditorForm.CheckValue(this.GameNameTextBox.Text, new List<QuestEditorForm.ErrorTypes>(mandatoryError), true))
			{
				this.MainTabControl.SelectedTab = this.CommonTabPage;
				return false;
			}
			if (!QuestEditorForm.CheckValue(this.PlayerLevelTextBox.Text, new List<QuestEditorForm.ErrorTypes>(intError), true))
			{
				this.MainTabControl.SelectedTab = this.CommonTabPage;
				return false;
			}
			if (!this.counterGridHandler.Validate())
			{
				this.MainTabControl.SelectedTab = this.CommonTabPage;
				return false;
			}
			if (!QuestEditorForm.CheckValue(this.RewExpQtyTextBox.Text, new List<QuestEditorForm.ErrorTypes>(intError), true))
			{
				this.MainTabControl.SelectedTab = this.CommonTabPage;
				return false;
			}
			if (!this.moneyValidator.Validate())
			{
				this.MainTabControl.SelectedTab = this.CommonTabPage;
				return false;
			}
			if (!QuestEditorForm.CheckValue(this.RewHonorQtyTextBox.Text, new List<QuestEditorForm.ErrorTypes>(intError), true))
			{
				this.MainTabControl.SelectedTab = this.CommonTabPage;
				return false;
			}
			if (!this.rewMandatoryItemsValidator.Validate())
			{
				this.MainTabControl.SelectedTab = this.CommonTabPage;
				return false;
			}
			if (!this.rewAlternativeItemsValidator.Validate())
			{
				this.MainTabControl.SelectedTab = this.CommonTabPage;
				return false;
			}
			if (!this.finishedQuestValidator.Validate())
			{
				this.MainTabControl.SelectedTab = this.CommonTabPage;
				return false;
			}
			if (!this.notStartedQuestValidator.Validate())
			{
				this.MainTabControl.SelectedTab = this.CommonTabPage;
				return false;
			}
			if (!this.rewReputationValidator.Validate())
			{
				this.MainTabControl.SelectedTab = this.CommonTabPage;
				return false;
			}
			if (!this.rewCurrencyValidator.Validate())
			{
				this.MainTabControl.SelectedTab = this.CommonTabPage;
				return false;
			}
			if (!this.countersLootTableValiodator.Validate())
			{
				this.MainTabControl.SelectedTab = this.LootTableTabPage;
				return false;
			}
			return true;
		}

		// Token: 0x06000B34 RID: 2868 RVA: 0x00061B60 File Offset: 0x00060B60
		private void SetFormCondition()
		{
			this.SaveButton.Enabled = (this.questEditorFormData.CurrentQuest != null || this.questEditorFormData.IsNew);
			this.SaveAsButton.Enabled = (this.questEditorFormData.CurrentQuest != null);
			this.DeleteQuestButton.Enabled = (this.questEditorFormData.CurrentQuest != null);
			this.ShowDiagramButton.Enabled = (this.questEditorFormData.CurrentQuest != null);
			this.ShowQuestLineButton.Enabled = (this.questEditorFormData.CurrentQuest != null);
			this.isChanged = false;
			this.isChangedThroughProprtyControl = false;
			this.prevQuestChanged = false;
			this.QuestPropertyControl.Enabled = (this.questEditorFormData.CurrentQuest != null);
			this.QuestPropertyControl.SetTitle(string.Empty);
			this.EditReturnLocatorButton.Enabled = (this.questEditorFormData.CurrentQuest != null);
			this.Text = string.Format("{0} {1}", "Quest Editor", (this.questEditorFormData.CurrentQuest != null) ? this.questEditorFormData.CurrentQuest.GameObject : string.Empty);
		}

		// Token: 0x06000B35 RID: 2869 RVA: 0x00061C9C File Offset: 0x00060C9C
		private bool PrompSave(IWin32Window dialogOwner)
		{
			if (this.isChanged)
			{
				DialogResult dialogResult = MessageBox.Show(dialogOwner, Strings.QUEST_EDITOR_SAVE_PROMP_MESSAGE, Strings.QUEST_EDITOR_TITLE, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Exclamation);
				if (dialogResult == DialogResult.Yes)
				{
					return this.Save();
				}
				if (dialogResult == DialogResult.Cancel)
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06000B36 RID: 2870 RVA: 0x00061CD8 File Offset: 0x00060CD8
		private void RegisterGrid(DataGridView grid)
		{
			if (base.ParamsSaver != null)
			{
				foreach (object obj in grid.Columns)
				{
					DataGridViewColumn column = (DataGridViewColumn)obj;
					base.ParamsSaver.RegisterControl(column);
				}
			}
		}

		// Token: 0x06000B37 RID: 2871 RVA: 0x00061D40 File Offset: 0x00060D40
		private static void InitGrid(DataGridView grid, int rowNum)
		{
			if (grid != null)
			{
				grid.Rows.Clear();
				if (rowNum > 0)
				{
					grid.Rows.Add(rowNum);
				}
			}
		}

		// Token: 0x06000B38 RID: 2872 RVA: 0x00061D64 File Offset: 0x00060D64
		private static void InitGrid(DataGridView grid, int rowNum, int defColNum, object defColValue)
		{
			QuestEditorForm.InitGrid(grid, rowNum);
			if (grid != null)
			{
				for (int index = 0; index < grid.RowCount; index++)
				{
					grid.Rows[index].Cells[defColNum].Value = defColValue;
				}
			}
		}

		// Token: 0x06000B39 RID: 2873 RVA: 0x00061DAC File Offset: 0x00060DAC
		private static bool CheckValue(object obj, IEnumerable<QuestEditorForm.ErrorTypes> errorTypes, bool showMessage)
		{
			using (IEnumerator<QuestEditorForm.ErrorTypes> enumerator = errorTypes.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					switch (enumerator.Current)
					{
					case QuestEditorForm.ErrorTypes.MANDATORY_FIELD:
						if (obj == null || obj.ToString() == string.Empty)
						{
							if (showMessage)
							{
								MessageBox.Show(Strings.QUEST_EDITOR_MANDATORYFIELDERROR_MESSAGE, Strings.QUEST_EDITOR_ERROR_TITLE, MessageBoxButtons.OK, MessageBoxIcon.Hand);
							}
							return false;
						}
						break;
					case QuestEditorForm.ErrorTypes.INT_FORMAT:
						if (obj != null && obj.ToString() != string.Empty && !QuestEditorForm.QuestEditorFormData.IsInteger(obj))
						{
							if (showMessage)
							{
								MessageBox.Show(Strings.QUEST_EDITOR_WRONGFORMATERROR_MESSAGE, Strings.QUEST_EDITOR_ERROR_TITLE, MessageBoxButtons.OK, MessageBoxIcon.Hand);
							}
							return false;
						}
						break;
					case QuestEditorForm.ErrorTypes.PROBABILITY_FORMAT:
						if (obj != null && obj.ToString() != string.Empty && (!QuestEditorForm.QuestEditorFormData.IsFloat(obj) || QuestEditorForm.QuestEditorFormData.ConvertToFloat(obj) < 0f || QuestEditorForm.QuestEditorFormData.ConvertToFloat(obj) > 1f))
						{
							if (showMessage)
							{
								MessageBox.Show(Strings.QUEST_EDITOR_WRONGFORMATERROR_MESSAGE, Strings.QUEST_EDITOR_ERROR_TITLE, MessageBoxButtons.OK, MessageBoxIcon.Hand);
							}
							return false;
						}
						break;
					case QuestEditorForm.ErrorTypes.FLOAT_FORMAT:
						if (obj != null && obj.ToString() != string.Empty && !QuestEditorForm.QuestEditorFormData.IsFloat(obj) && !QuestEditorForm.QuestEditorFormData.IsFloat(obj))
						{
							if (showMessage)
							{
								MessageBox.Show(Strings.QUEST_EDITOR_WRONGFORMATERROR_MESSAGE, Strings.QUEST_EDITOR_ERROR_TITLE, MessageBoxButtons.OK, MessageBoxIcon.Hand);
							}
							return false;
						}
						break;
					default:
						return false;
					}
				}
			}
			return true;
		}

		// Token: 0x06000B3A RID: 2874 RVA: 0x00061F30 File Offset: 0x00060F30
		private static bool CheckIndexColumns(DataGridView grid, int[] indexColumns, bool uniqueInRow)
		{
			List<object[]> indexValues = new List<object[]>();
			bool success = true;
			for (int row = 0; row < grid.Rows.Count; row++)
			{
				if (uniqueInRow && !QuestEditorForm.CheckGridUniqueInRow(grid.Rows[row], indexColumns))
				{
					success = false;
					break;
				}
				if (!QuestEditorForm.CheckGridUniqueRow(grid.Rows[row], indexColumns, ref indexValues))
				{
					success = false;
					break;
				}
			}
			if (!success)
			{
				MessageBox.Show(Strings.QUEST_EDITOR_GRIDINDEX_ERROR, Strings.QUEST_EDITOR_ERROR_TITLE, MessageBoxButtons.OK, MessageBoxIcon.Hand);
			}
			return success;
		}

		// Token: 0x06000B3B RID: 2875 RVA: 0x00061FA8 File Offset: 0x00060FA8
		private static bool CheckGridUniqueRow(DataGridViewRow row, int[] indexColumns, ref List<object[]> indexValues)
		{
			foreach (object[] values in indexValues)
			{
				bool isEqual = false;
				for (int col = 0; col < indexColumns.Length; col++)
				{
					isEqual = (values[col] == row.Cells[indexColumns[col]].Value);
					if (!isEqual)
					{
						break;
					}
				}
				if (isEqual)
				{
					return false;
				}
			}
			bool emptyRow = true;
			object[] newValues = new object[indexColumns.Length];
			for (int col2 = 0; col2 < indexColumns.Length; col2++)
			{
				newValues[col2] = row.Cells[indexColumns[col2]].Value;
				if (newValues[col2] != null)
				{
					emptyRow = false;
				}
			}
			if (!emptyRow)
			{
				indexValues.Add(newValues);
			}
			return true;
		}

		// Token: 0x06000B3C RID: 2876 RVA: 0x00062078 File Offset: 0x00061078
		private static bool CheckGridUniqueInRow(DataGridViewRow row, int[] indexColumns)
		{
			List<object> rowIndexVal = new List<object>();
			for (int col = 0; col < indexColumns.Length; col++)
			{
				object val = row.Cells[indexColumns[col]].Value;
				if (val != null)
				{
					if (rowIndexVal.Contains(val))
					{
						return false;
					}
					rowIndexVal.Add(val);
				}
			}
			return true;
		}

		// Token: 0x06000B3D RID: 2877 RVA: 0x000620C4 File Offset: 0x000610C4
		private void LoadCharacterClass(string questClassPrereq)
		{
			this.CharacterClassComboBox.Text = string.Empty;
			if (this.questEditorFormData.CurrentQuest != null && !string.IsNullOrEmpty(questClassPrereq))
			{
				for (int index = 1; index < this.CharacterClassComboBox.Items.Count; index++)
				{
					GameObjectClass gameObj = this.CharacterClassComboBox.Items[index] as GameObjectClass;
					if (gameObj != null && gameObj.GameObject == questClassPrereq)
					{
						this.CharacterClassComboBox.SelectedIndex = index;
						return;
					}
				}
			}
		}

		// Token: 0x06000B3E RID: 2878 RVA: 0x00062148 File Offset: 0x00061148
		private void LoadQuestGiver()
		{
			this.questGiverListener.SetValue(null);
			this.questGiverListener.Enabled = true;
			this.QuestTypeLabel.Text = string.Empty;
			foreach (GameObjectClass gameObjectClass in base.Context.QuestEnvironment.QuestGivers)
			{
				QuestGiverClass questGiver = (QuestGiverClass)gameObjectClass;
				QuestGiverClass.QuestType questType;
				if (questGiver.FindQuest(this.questEditorFormData.CurrentQuest.GameObject, out questType))
				{
					if (string.IsNullOrEmpty(this.questGiverListener.GetValue()))
					{
						this.questGiverListener.SetValue(questGiver);
						this.questGiverListener.Enabled = (questType == QuestGiverClass.QuestType.Ordinary);
						if (questType == QuestGiverClass.QuestType.Daily)
						{
							this.QuestTypeLabel.Text = "Daily Quest";
						}
					}
					else
					{
						this.questGiverListener.Enabled = false;
						MessageBox.Show(this, "More then one quest giver!", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Hand);
					}
				}
			}
		}

		// Token: 0x06000B3F RID: 2879 RVA: 0x0006224C File Offset: 0x0006124C
		private void LoadRewItemsGrid(Dictionary<string, QuestClass.Pair<int, bool>> items, DataGridView grid)
		{
			QuestEditorForm.InitGrid(grid, 3);
			if (this.questEditorFormData.CurrentQuest != null)
			{
				int rowNum = 0;
				int gameObjNum = 0;
				foreach (KeyValuePair<string, QuestClass.Pair<int, bool>> pair in items)
				{
					int gameObjCol = this.rewItemCol.Keys[gameObjNum];
					grid[gameObjCol, rowNum].Value = base.Context.QuestEnvironment.FindGameObject(base.Context.QuestEnvironment.Items, pair.Key);
					grid[this.rewItemCol[gameObjCol].qtyCol, rowNum].Value = pair.Value.First;
					grid[this.rewItemCol[gameObjCol].isHiddenCol, rowNum].Value = pair.Value.Second;
					gameObjNum++;
					if (gameObjNum == this.rewItemCol.Count)
					{
						gameObjNum = 0;
						rowNum++;
					}
				}
			}
		}

		// Token: 0x06000B40 RID: 2880 RVA: 0x00062370 File Offset: 0x00061370
		private void LoadDictionaryToGrid(DataGridView grid, Dictionary<string, int> values, Tools.DBGameObjects.View view, int keyColNum, int valueColNum)
		{
			if (grid != null)
			{
				QuestEditorForm.InitGrid(grid, values.Count);
				int index = 0;
				foreach (KeyValuePair<string, int> pair in values)
				{
					grid[keyColNum, index].Value = base.Context.QuestEnvironment.FindGameObject(view, pair.Key);
					grid[valueColNum, index].Value = pair.Value;
					index++;
				}
			}
		}

		// Token: 0x06000B41 RID: 2881 RVA: 0x0006240C File Offset: 0x0006140C
		private void LoadLootTableGrid()
		{
			this.LootTableGrid.Rows.Clear();
			if (this.questEditorFormData.CurrentQuest != null)
			{
				List<QuestClass.LootTableRow> lootTable;
				this.questEditorFormData.CurrentQuest.GetLootTable(out lootTable);
				foreach (QuestClass.LootTableRow ltRow in lootTable)
				{
					object[] lootTableRow = new object[8];
					lootTableRow[0] = base.Context.QuestEnvironment.FindGameObject(base.Context.QuestEnvironment.LootableObjects, ltRow.Lootable);
					lootTableRow[1] = base.Context.QuestEnvironment.FindGameObject(base.Context.QuestEnvironment.Items, ltRow.Item);
					lootTableRow[2] = ltRow.Chance.ToString();
					lootTableRow[3] = ltRow.MinNumber.ToString();
					lootTableRow[4] = ltRow.MaxNumber.ToString();
					lootTableRow[5] = ltRow.IsForAll;
					this.LootTableGrid.Rows.Add(lootTableRow);
				}
			}
		}

		// Token: 0x06000B42 RID: 2882 RVA: 0x0006253C File Offset: 0x0006153C
		private void LoadPrereqQuests(DataGridView grid, IEnumerable<string> values)
		{
			grid.Rows.Clear();
			if (values != null)
			{
				foreach (string questDBID in values)
				{
					object[] row = new object[]
					{
						base.Context.QuestEnvironment.FindGameObject(base.Context.QuestEnvironment.Quests, questDBID)
					};
					grid.Rows.Add(row);
				}
			}
		}

		// Token: 0x06000B43 RID: 2883 RVA: 0x000625C4 File Offset: 0x000615C4
		private string GetCharacterClass()
		{
			GameObjectClass gameObj = this.CharacterClassComboBox.SelectedItem as GameObjectClass;
			if (gameObj != null)
			{
				return gameObj.GameObject;
			}
			return string.Empty;
		}

		// Token: 0x06000B44 RID: 2884 RVA: 0x000625F4 File Offset: 0x000615F4
		private void SaveRewItemsGrid(DataGridView grid, out Dictionary<string, QuestClass.Pair<int, bool>> items)
		{
			items = new Dictionary<string, QuestClass.Pair<int, bool>>();
			for (int i = 0; i < grid.Rows.Count; i++)
			{
				foreach (KeyValuePair<int, QuestEditorForm.RewItemColStruct> pair in this.rewItemCol)
				{
					int gameObjectCol = pair.Key;
					int qtyCol = pair.Value.qtyCol;
					int isHiddenCol = pair.Value.isHiddenCol;
					if (grid[gameObjectCol, i].Value != null)
					{
						GameObjectClass gameObj = grid[gameObjectCol, i].Value as GameObjectClass;
						if (gameObj != null && !string.IsNullOrEmpty(gameObj.GameObject) && !items.ContainsKey(gameObj.GameObject))
						{
							int qty = QuestEditorForm.QuestEditorFormData.ConvertToInteger(grid[qtyCol, i].Value);
							bool isHidden = QuestEditorForm.QuestEditorFormData.ConvertToBool(grid[isHiddenCol, i].Value);
							items.Add(gameObj.GameObject, new QuestClass.Pair<int, bool>(qty, isHidden));
						}
					}
				}
			}
		}

		// Token: 0x06000B45 RID: 2885 RVA: 0x00062710 File Offset: 0x00061710
		private static void SaveDictionaryFromGrid(DataGridView grid, out Dictionary<string, int> values, int keyColNum, int valueColNum)
		{
			values = new Dictionary<string, int>();
			for (int i = 0; i < grid.Rows.Count; i++)
			{
				if (grid[keyColNum, i].Value != null)
				{
					GameObjectClass gameObj = grid[keyColNum, i].Value as GameObjectClass;
					if (gameObj != null && !string.IsNullOrEmpty(gameObj.GameObject) && !values.ContainsKey(gameObj.GameObject))
					{
						int qty = QuestEditorForm.QuestEditorFormData.ConvertToInteger(grid[valueColNum, i].Value);
						values.Add(gameObj.GameObject, qty);
					}
				}
			}
		}

		// Token: 0x06000B46 RID: 2886 RVA: 0x0006279C File Offset: 0x0006179C
		private void SaveLootTableGrid()
		{
			List<QuestClass.LootTableRow> lootTable = new List<QuestClass.LootTableRow>(this.LootTableGrid.Rows.Count);
			for (int i = 0; i < this.LootTableGrid.Rows.Count; i++)
			{
				GameObjectClass mob = this.LootTableGrid[0, i].Value as GameObjectClass;
				GameObjectClass item = this.LootTableGrid[1, i].Value as GameObjectClass;
				if (mob != null && item != null)
				{
					lootTable.Add(new QuestClass.LootTableRow
					{
						Lootable = mob.GameObject,
						Item = item.GameObject,
						Chance = QuestEditorForm.QuestEditorFormData.ConvertToFloat(this.LootTableGrid[2, i].Value.ToString()),
						MinNumber = QuestEditorForm.QuestEditorFormData.ConvertToInteger(this.LootTableGrid[3, i].Value.ToString()),
						MaxNumber = QuestEditorForm.QuestEditorFormData.ConvertToInteger(this.LootTableGrid[4, i].Value.ToString()),
						IsForAll = QuestEditorForm.QuestEditorFormData.ConvertToBool(this.LootTableGrid[5, i].Value)
					});
				}
			}
			this.questEditorFormData.CurrentQuest.SetLootTable(lootTable);
		}

		// Token: 0x06000B47 RID: 2887 RVA: 0x000628E0 File Offset: 0x000618E0
		private void SavePlotLine()
		{
			this.questEditorFormData.CurrentQuest.PlotLine = this.PlotLineTextBox.Text.Trim();
			string plotLine = this.questEditorFormData.CurrentQuest.PlotLine;
			if (this.prevQuestChanged && !string.IsNullOrEmpty(plotLine))
			{
				List<string> prevQuestList = QuestEditorForm.GetPrereqQuests(this.FinishedQuestGrid);
				foreach (string prevQuestKey in prevQuestList)
				{
					QuestClass prevQuest = base.Context.QuestEnvironment.FindGameObject(base.Context.QuestEnvironment.Quests, prevQuestKey) as QuestClass;
					if (prevQuest != null && string.IsNullOrEmpty(prevQuest.PlotLine) && MessageBox.Show(string.Format(Strings.QUEST_EDITOR_SAVEPLOTLINE, plotLine, prevQuest.Name), Strings.QUEST_EDITOR_TITLE, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
					{
						prevQuest.PlotLine = plotLine;
					}
				}
			}
		}

		// Token: 0x06000B48 RID: 2888 RVA: 0x000629DC File Offset: 0x000619DC
		private static List<string> GetPrereqQuests(DataGridView grid)
		{
			List<string> quests = new List<string>(grid.Rows.Count);
			for (int i = 0; i < grid.Rows.Count; i++)
			{
				GameObjectClass gameObj = grid[0, i].Value as GameObjectClass;
				if (gameObj != null && !string.IsNullOrEmpty(gameObj.GameObject))
				{
					quests.Add(gameObj.GameObject);
				}
			}
			return quests;
		}

		// Token: 0x06000B49 RID: 2889 RVA: 0x00062A40 File Offset: 0x00061A40
		private void OnZoneLoaded(string zone)
		{
			if (this.questEditorFormData.CurrentQuest != null && string.IsNullOrEmpty(this.QuestGiverTextBox.Text))
			{
				this.LoadQuestGiver();
			}
		}

		// Token: 0x06000B4A RID: 2890 RVA: 0x00062A67 File Offset: 0x00061A67
		private void OnLoadZone(string zone)
		{
			this.questEditorFormData.CurrentQuest = null;
			this.ClearAll();
			if (zone != "_OtherFolder")
			{
				this.ZoneTextBox.Text = (zone ?? string.Empty);
			}
		}

		// Token: 0x06000B4B RID: 2891 RVA: 0x00062A9D File Offset: 0x00061A9D
		private void OnLoadQuest(QuestClass quest)
		{
			if (quest != null && this.PrompSave(this))
			{
				this.LoadQuest(quest);
			}
		}

		// Token: 0x06000B4C RID: 2892 RVA: 0x00062AB2 File Offset: 0x00061AB2
		private void OnCreateNewQuest(string zone, string quest, QuestGiverClass questGiver)
		{
			if (!string.IsNullOrEmpty(zone) && !string.IsNullOrEmpty(quest))
			{
				this.CreateNewQuest(zone, quest, questGiver);
			}
		}

		// Token: 0x06000B4D RID: 2893 RVA: 0x00062AD0 File Offset: 0x00061AD0
		public QuestEditorForm(MainForm.Context context) : base(EditorEnvironment.EditorFormsFolder + "QuestEditorForm.xml", context)
		{
			this.InitializeComponent();
			this.questEditorFormData = new QuestEditorForm.QuestEditorFormData(context.QuestEnvironment);
			base.Context.QuestEnvironment.QuestChanged += this.OnEnviromentQuestChanged;
			this.rewItemCol = new SortedList<int, QuestEditorForm.RewItemColStruct>(2);
			this.rewItemCol.Add(0, new QuestEditorForm.RewItemColStruct(1, 2));
			this.rewItemCol.Add(3, new QuestEditorForm.RewItemColStruct(4, 5));
			this.experienceValidator = new QuestEditorForm.ExperienceValidator(this.QuestLevelTextBox, this.RewExpQtyTextBox, this.RewExpRealQtyLabel);
			this.moneyValidator = new QuestEditorForm.MoneyValidator(this.RewGoldQtyTextBox, this.RewSilverQtyTextBox, this.RewCopperQtyTextBox, this.AutoMoneyRewardCheckBox, this.RewExpRealQtyLabel, this.QuestLevelTextBox, this.AutoMoneyRewardErrorLabel, this.experienceValidator);
			this.countersLootTableValiodator = new QuestEditorForm.LootTableValiodator(this.LootTableGrid);
			this.questLine = new QuestEditorForm.QuestLine(context.QuestEnvironment, this);
			SortedList<int, int> rewItemcColValidator = new SortedList<int, int>(this.rewItemCol.Count);
			foreach (KeyValuePair<int, QuestEditorForm.RewItemColStruct> pair in this.rewItemCol)
			{
				rewItemcColValidator.Add(pair.Key, pair.Value.qtyCol);
			}
			this.rewMandatoryItemsValidator = new QuestEditorForm.SimpleGridValidator(this.RewMandatoryItemsGrid, rewItemcColValidator);
			this.rewAlternativeItemsValidator = new QuestEditorForm.RewAlternativeGridValidator(this.RewAlternativeItemsGrid, rewItemcColValidator);
			this.rewReputationValidator = new QuestEditorForm.SimpleGridValidator(this.RewReputationGrid, 0, 1);
			this.rewCurrencyValidator = new QuestEditorForm.SimpleGridValidator(this.RewCurrencyGrid, 0, 1);
			this.finishedQuestValidator = new QuestEditorForm.SimpleGridValidator(this.FinishedQuestGrid, 0, -1);
			this.notStartedQuestValidator = new QuestEditorForm.SimpleGridValidator(this.NotStartedQuestGrid, 0, -1);
			this.questMarker = new QuestEditorForm.QuestMarker(this.autoSetGoalLocatorCheckBox, this.GoalLocatorsListView, this.MarkerZoneComboBox, this.mapPanel, this.AddGoalLocatorButton, this.RemoveGoalLocatorButton, this.EditGoalLocatorButton, this.EditReturnLocatorButton);
			if (base.ParamsSaver != null)
			{
				base.ParamsSaver.AutoregisterControls = false;
				base.ParamsSaver.RegisterControl(this.AutoMoneyRewardCheckBox);
				this.RegisterGrid(this.FinishedQuestGrid);
				this.RegisterGrid(this.NotStartedQuestGrid);
				this.RegisterGrid(this.CounterGrid);
				this.RegisterGrid(this.RewMandatoryItemsGrid);
				this.RegisterGrid(this.RewAlternativeItemsGrid);
				this.RegisterGrid(this.LootTableGrid);
				this.RegisterGrid(this.RewReputationGrid);
				this.RegisterGrid(this.RewCurrencyGrid);
			}
			base.Load += this.OnFormLoad;
			base.FormClosing += this.OnFormClosing;
			this.ChooseQuestButton.Click += this.ChooseQuest;
			this.AddNewQuestButton.Click += this.OnAddNewQuest;
			this.DeleteQuestButton.Click += this.DeleteQuest;
			this.SaveButton.Click += this.OnSave;
			this.SaveAsButton.Click += this.OnSaveAsButton;
			this.CloseButton.Click += this.OnCloseButtonClick;
			this.ShowDiagramButton.Click += this.OnDiagramClick;
			this.ShowQuestLineButton.Click += this.OnShowQuestLineClick;
			this.QuestNameTextBox.TextChanged += this.OnDataChanged;
			this.CharacterClassComboBox.SelectedIndexChanged += this.OnDataChanged;
			this.QuestLevelTextBox.TextChanged += this.OnDataChanged;
			this.GameNameTextBox.TextChanged += this.OnDataChanged;
			this.PlayerLevelTextBox.TextChanged += this.OnDataChanged;
			this.PlotLineTextBox.TextChanged += this.OnDataChanged;
			this.IssueTextBox.TextChanged += this.OnDataChanged;
			this.NotCompletedTextBox.TextChanged += this.OnDataChanged;
			this.RewExpQtyTextBox.TextChanged += this.OnDataChanged;
			this.moneyValidator.DataChanged += this.OnDataChanged;
			this.CompletedTextBox.TextChanged += this.OnDataChanged;
			this.LootTableGrid.RowsRemoved += new DataGridViewRowsRemovedEventHandler(this.OnDataChanged);
			this.LootTableGrid.CellValueChanged += new DataGridViewCellEventHandler(this.OnDataChanged);
			this.questMarker.DataChanged += this.OnDataChanged;
			this.QuestPropertyControl.StateContainer = context.StateContainer;
			this.QuestPropertyControl.AddButton("_load_to_script_editor", Strings.PROPERTY_CONTROL_SCRIPT_EDITOR, Images.script_diagram);
			this.QuestPropertyControl.Enter += this.OnEnterQuestPropertyControl;
			this.QuestPropertyControl.SaveButtonPressed += this.OnPropertControlSaved;
		}

		// Token: 0x04000883 RID: 2179
		private const int RewItemRowCnt = 3;

		// Token: 0x04000884 RID: 2180
		private const int CounterGridRowCnt = 6;

		// Token: 0x04000885 RID: 2181
		private const int LootTableGridMobCol = 0;

		// Token: 0x04000886 RID: 2182
		private const int LootTableGridItemCol = 1;

		// Token: 0x04000887 RID: 2183
		private const int LootTableGridDropRateCol = 2;

		// Token: 0x04000888 RID: 2184
		private const int LootTableGridMinCol = 3;

		// Token: 0x04000889 RID: 2185
		private const int LootTableGridMaxCol = 4;

		// Token: 0x0400088A RID: 2186
		private const int LootTableGridIsForAllCol = 5;

		// Token: 0x0400088B RID: 2187
		private const int StartConditionQuestCol = 0;

		// Token: 0x0400088C RID: 2188
		private const int RewReputationGridIdCol = 0;

		// Token: 0x0400088D RID: 2189
		private const int RewReputationGridQtyCol = 1;

		// Token: 0x0400088E RID: 2190
		private const int RewCurrencyGridIdCol = 0;

		// Token: 0x0400088F RID: 2191
		private const int RewCurrencyGridQtyCol = 1;

		// Token: 0x04000964 RID: 2404
		private readonly QuestEditorForm.QuestEditorFormData questEditorFormData;

		// Token: 0x04000965 RID: 2405
		private bool isChanged;

		// Token: 0x04000966 RID: 2406
		private bool isChangedThroughProprtyControl;

		// Token: 0x04000967 RID: 2407
		private bool prevQuestChanged;

		// Token: 0x04000968 RID: 2408
		private readonly SortedList<int, QuestEditorForm.RewItemColStruct> rewItemCol;

		// Token: 0x04000969 RID: 2409
		private readonly QuestEditorForm.MoneyValidator moneyValidator;

		// Token: 0x0400096A RID: 2410
		private readonly QuestEditorForm.ExperienceValidator experienceValidator;

		// Token: 0x0400096B RID: 2411
		private readonly QuestEditorForm.LootTableValiodator countersLootTableValiodator;

		// Token: 0x0400096C RID: 2412
		private readonly QuestEditorForm.SimpleGridValidator rewMandatoryItemsValidator;

		// Token: 0x0400096D RID: 2413
		private readonly QuestEditorForm.RewAlternativeGridValidator rewAlternativeItemsValidator;

		// Token: 0x0400096E RID: 2414
		private readonly QuestEditorForm.SimpleGridValidator finishedQuestValidator;

		// Token: 0x0400096F RID: 2415
		private readonly QuestEditorForm.SimpleGridValidator notStartedQuestValidator;

		// Token: 0x04000970 RID: 2416
		private readonly QuestEditorForm.SimpleGridValidator rewReputationValidator;

		// Token: 0x04000971 RID: 2417
		private readonly QuestEditorForm.SimpleGridValidator rewCurrencyValidator;

		// Token: 0x04000972 RID: 2418
		private QuestEditorForm.SimpleGridListener rewMandatoryItemsListener;

		// Token: 0x04000973 RID: 2419
		private QuestEditorForm.SimpleGridListener rewAlternativeItemsListener;

		// Token: 0x04000974 RID: 2420
		private QuestEditorForm.SimpleStringValueListener questGiverListener;

		// Token: 0x04000975 RID: 2421
		private QuestEditorForm.SimpleStringValueListener compleaterListener;

		// Token: 0x04000976 RID: 2422
		private QuestEditorForm.SimpleGridListener finishedQuestListener;

		// Token: 0x04000977 RID: 2423
		private QuestEditorForm.SimpleGridListener notStartedQuestListener;

		// Token: 0x04000978 RID: 2424
		private QuestEditorForm.SimpleGridListener lootTableListenerItemListener;

		// Token: 0x04000979 RID: 2425
		private QuestEditorForm.SimpleGridListener lootTableListenerMobListener;

		// Token: 0x0400097A RID: 2426
		private QuestEditorForm.CounterGridHandler counterGridHandler;

		// Token: 0x0400097B RID: 2427
		private QuestEditorForm.SimpleGridListener rewReputationListener;

		// Token: 0x0400097C RID: 2428
		private QuestEditorForm.SimpleGridListener rewCurrencyListener;

		// Token: 0x0400097D RID: 2429
		private readonly QuestEditorForm.QuestMarker questMarker;

		// Token: 0x0400097E RID: 2430
		private readonly QuestEditorForm.QuestLine questLine;

		// Token: 0x020000DA RID: 218
		private class MoneyValidator
		{
			// Token: 0x14000032 RID: 50
			// (add) Token: 0x06000B4E RID: 2894 RVA: 0x00062FE0 File Offset: 0x00061FE0
			// (remove) Token: 0x06000B4F RID: 2895 RVA: 0x00062FF9 File Offset: 0x00061FF9
			public event QuestEditorForm.MoneyValidator.MoneyValidatorEvent DataChanged;

			// Token: 0x06000B50 RID: 2896 RVA: 0x00063014 File Offset: 0x00062014
			private void LoadAutoCalculateData()
			{
				this.autoCalcualteData.Clear();
				if (File.Exists(QuestEditorForm.MoneyValidator.configFilePath))
				{
					string[] data = File.ReadAllLines(QuestEditorForm.MoneyValidator.configFilePath);
					this.autoCalcualteData.Capacity = data.Length;
					foreach (string sValue in data)
					{
						decimal dValue;
						if (!decimal.TryParse(sValue, out dValue))
						{
							dValue = 0m;
						}
						this.autoCalcualteData.Add(dValue);
					}
				}
				this.autoCalcualteData.Insert(0, 0m);
			}

			// Token: 0x06000B51 RID: 2897 RVA: 0x00063099 File Offset: 0x00062099
			private void OnTextBox_Validated(object sender, EventArgs e)
			{
				this.Validate();
			}

			// Token: 0x06000B52 RID: 2898 RVA: 0x000630A2 File Offset: 0x000620A2
			private void OnExpearnceChanged()
			{
				if (this.autoMoneyRewardCheckBox != null && this.autoMoneyRewardCheckBox.Checked)
				{
					this.AutoCalculate();
				}
			}

			// Token: 0x06000B53 RID: 2899 RVA: 0x000630C0 File Offset: 0x000620C0
			private void OnAutoCheckedChanged(object sender, EventArgs e)
			{
				if (this.autoMoneyRewardCheckBox != null)
				{
					this.goldTextBox.Enabled = !this.autoMoneyRewardCheckBox.Checked;
					this.silverTextBox.Enabled = !this.autoMoneyRewardCheckBox.Checked;
					this.copperTextBox.Enabled = !this.autoMoneyRewardCheckBox.Checked;
					if (this.autoMoneyRewardCheckBox.Checked)
					{
						this.AutoCalculate();
						return;
					}
					this.CheckMoneyValue();
				}
			}

			// Token: 0x06000B54 RID: 2900 RVA: 0x0006313C File Offset: 0x0006213C
			private void SetTextBoxes()
			{
				if (this.goldTextBox == null || this.silverTextBox == null || this.copperTextBox == null)
				{
					return;
				}
				int copper = this.money % 100;
				int silver = this.money / 100 % 100;
				int gold = this.money / 10000;
				this.copperTextBox.Text = copper.ToString();
				this.silverTextBox.Text = silver.ToString();
				this.goldTextBox.Text = gold.ToString();
			}

			// Token: 0x06000B55 RID: 2901 RVA: 0x000631BC File Offset: 0x000621BC
			private void AutoCalculate()
			{
				this.Money = this.GetAutoValue();
			}

			// Token: 0x06000B56 RID: 2902 RVA: 0x000631CA File Offset: 0x000621CA
			private void CheckMoneyValue()
			{
				if (this.autoMoneyRewardErrorLabel != null)
				{
					this.autoMoneyRewardErrorLabel.Visible = (this.autoMoneyRewardCheckBox.Checked && this.Money != this.GetAutoValue());
				}
			}

			// Token: 0x06000B57 RID: 2903 RVA: 0x00063200 File Offset: 0x00062200
			private int GetAutoValue()
			{
				int level;
				int experience;
				if (this.levelTextBox != null && int.TryParse(this.levelTextBox.Text, out level) && level > 0 && level < this.autoCalcualteData.Count && this.realExperienceLabel != null && int.TryParse(this.realExperienceLabel.Text, out experience))
				{
					return (int)Math.Round(100 * experience * this.autoCalcualteData[level]);
				}
				return 0;
			}

			// Token: 0x06000B58 RID: 2904 RVA: 0x00063280 File Offset: 0x00062280
			public MoneyValidator(TextBox _goldTextBox, TextBox _silverTextBox, TextBox _copperTextBox, CheckBox _autoMoneyRewardCheckBox, Label _realExperienceLabel, TextBox _levelTextBox, Label _autoMoneyRewardErrorLabel, QuestEditorForm.ExperienceValidator _experienceValidator)
			{
				this.goldTextBox = _goldTextBox;
				this.silverTextBox = _silverTextBox;
				this.copperTextBox = _copperTextBox;
				this.autoMoneyRewardCheckBox = _autoMoneyRewardCheckBox;
				this.realExperienceLabel = _realExperienceLabel;
				this.levelTextBox = _levelTextBox;
				this.autoMoneyRewardErrorLabel = _autoMoneyRewardErrorLabel;
				this.experienceValidator = _experienceValidator;
				this.LoadAutoCalculateData();
				if (this.goldTextBox != null)
				{
					this.goldTextBox.Validated += this.OnTextBox_Validated;
				}
				if (this.silverTextBox != null)
				{
					this.silverTextBox.Validated += this.OnTextBox_Validated;
				}
				if (this.copperTextBox != null)
				{
					this.copperTextBox.Validated += this.OnTextBox_Validated;
				}
				if (this.autoMoneyRewardCheckBox != null)
				{
					this.autoMoneyRewardCheckBox.CheckedChanged += this.OnAutoCheckedChanged;
				}
				if (this.experienceValidator != null)
				{
					this.experienceValidator.Validated += this.OnExpearnceChanged;
				}
				if (this.autoMoneyRewardCheckBox != null && this.autoMoneyRewardCheckBox.Checked)
				{
					this.AutoCalculate();
				}
				this.OnAutoCheckedChanged(this, null);
			}

			// Token: 0x06000B59 RID: 2905 RVA: 0x000633A0 File Offset: 0x000623A0
			public bool Validate()
			{
				if (this.autoMoneyRewardCheckBox != null && this.autoMoneyRewardCheckBox.Checked)
				{
					this.AutoCalculate();
				}
				else
				{
					if (this.goldTextBox == null || this.silverTextBox == null || this.copperTextBox == null)
					{
						return false;
					}
					QuestEditorForm.ErrorTypes[] intError = new QuestEditorForm.ErrorTypes[]
					{
						QuestEditorForm.ErrorTypes.INT_FORMAT
					};
					if (!QuestEditorForm.CheckValue(this.copperTextBox.Text, new List<QuestEditorForm.ErrorTypes>(intError), true))
					{
						return false;
					}
					int copper;
					if (!int.TryParse(this.copperTextBox.Text, out copper))
					{
						copper = 0;
					}
					if (!QuestEditorForm.CheckValue(this.silverTextBox.Text, new List<QuestEditorForm.ErrorTypes>(intError), true))
					{
						return false;
					}
					int silver;
					if (!int.TryParse(this.silverTextBox.Text, out silver))
					{
						silver = 0;
					}
					if (!QuestEditorForm.CheckValue(this.goldTextBox.Text, new List<QuestEditorForm.ErrorTypes>(intError), true))
					{
						return false;
					}
					int gold;
					if (!int.TryParse(this.goldTextBox.Text, out gold))
					{
						gold = 0;
					}
					if (copper > 99)
					{
						silver += copper / 100;
						copper %= 100;
					}
					if (silver > 99)
					{
						gold += silver / 100;
						silver %= 100;
					}
					this.copperTextBox.Text = copper.ToString();
					this.silverTextBox.Text = silver.ToString();
					this.goldTextBox.Text = gold.ToString();
					this.Money = copper + 100 * silver + 10000 * gold;
				}
				return true;
			}

			// Token: 0x17000236 RID: 566
			// (get) Token: 0x06000B5A RID: 2906 RVA: 0x000634FD File Offset: 0x000624FD
			// (set) Token: 0x06000B5B RID: 2907 RVA: 0x00063508 File Offset: 0x00062508
			public int Money
			{
				get
				{
					return this.money;
				}
				set
				{
					bool changed = this.money != value;
					this.money = value;
					this.SetTextBoxes();
					this.CheckMoneyValue();
					if (changed && this.DataChanged != null)
					{
						this.DataChanged(this, new EventArgs());
					}
				}
			}

			// Token: 0x0400097F RID: 2431
			private const int goldToSilver = 100;

			// Token: 0x04000980 RID: 2432
			private const int silverToCopper = 100;

			// Token: 0x04000981 RID: 2433
			private static readonly string configFilePath = EditorEnvironment.EditorFolder + "Quests/QuestEditorConfig/MoneyRewardConfig.txt";

			// Token: 0x04000982 RID: 2434
			private int money;

			// Token: 0x04000983 RID: 2435
			private readonly TextBox goldTextBox;

			// Token: 0x04000984 RID: 2436
			private readonly TextBox silverTextBox;

			// Token: 0x04000985 RID: 2437
			private readonly TextBox copperTextBox;

			// Token: 0x04000986 RID: 2438
			private readonly CheckBox autoMoneyRewardCheckBox;

			// Token: 0x04000987 RID: 2439
			private readonly Label realExperienceLabel;

			// Token: 0x04000988 RID: 2440
			private readonly TextBox levelTextBox;

			// Token: 0x04000989 RID: 2441
			private readonly Label autoMoneyRewardErrorLabel;

			// Token: 0x0400098A RID: 2442
			private readonly QuestEditorForm.ExperienceValidator experienceValidator;

			// Token: 0x0400098B RID: 2443
			private readonly List<decimal> autoCalcualteData = new List<decimal>();

			// Token: 0x020000DB RID: 219
			// (Invoke) Token: 0x06000B5E RID: 2910
			public delegate void MoneyValidatorEvent(object sender, EventArgs e);
		}

		// Token: 0x020000DC RID: 220
		private class ExperienceValidator
		{
			// Token: 0x14000033 RID: 51
			// (add) Token: 0x06000B61 RID: 2913 RVA: 0x00063567 File Offset: 0x00062567
			// (remove) Token: 0x06000B62 RID: 2914 RVA: 0x00063580 File Offset: 0x00062580
			public event QuestEditorForm.ExperienceValidator.ValidatedEvent Validated;

			// Token: 0x06000B63 RID: 2915 RVA: 0x00063599 File Offset: 0x00062599
			private void OnTextBox_Validated(object sender, EventArgs e)
			{
				this.Validate(true);
			}

			// Token: 0x06000B64 RID: 2916 RVA: 0x000635A4 File Offset: 0x000625A4
			public ExperienceValidator(TextBox _questLevelTextBox, TextBox _experienceTextBox, Label _realExperienceLabel)
			{
				this.questLevelTextBox = _questLevelTextBox;
				this.experienceTextBox = _experienceTextBox;
				this.realExperienceLabel = _realExperienceLabel;
				if (this.questLevelTextBox != null)
				{
					this.questLevelTextBox.Validated += this.OnTextBox_Validated;
				}
				if (this.experienceTextBox != null)
				{
					this.experienceTextBox.Text = string.Empty;
					this.experienceTextBox.Validated += this.OnTextBox_Validated;
				}
			}

			// Token: 0x06000B65 RID: 2917 RVA: 0x0006361C File Offset: 0x0006261C
			public bool Validate(bool invokeEvent)
			{
				if (this.questLevelTextBox == null || this.experienceTextBox == null || this.realExperienceLabel == null)
				{
					return false;
				}
				QuestEditorForm.ErrorTypes[] intError = new QuestEditorForm.ErrorTypes[]
				{
					QuestEditorForm.ErrorTypes.INT_FORMAT
				};
				if (!QuestEditorForm.CheckValue(this.questLevelTextBox.Text, new List<QuestEditorForm.ErrorTypes>(intError), true))
				{
					return false;
				}
				int questLevel;
				if (!int.TryParse(this.questLevelTextBox.Text, out questLevel))
				{
					questLevel = 0;
				}
				if (QuestEditorForm.CheckValue(this.experienceTextBox.Text, new List<QuestEditorForm.ErrorTypes>(intError), true))
				{
					int experience;
					if (!int.TryParse(this.experienceTextBox.Text, out experience))
					{
						experience = 0;
					}
					this.realExperienceLabel.Text = QuestEditor.GetRealExperienceQuest(questLevel, experience).ToString();
					if (invokeEvent && this.Validated != null)
					{
						this.Validated();
					}
					return true;
				}
				return false;
			}

			// Token: 0x0400098D RID: 2445
			private readonly TextBox questLevelTextBox;

			// Token: 0x0400098E RID: 2446
			private readonly TextBox experienceTextBox;

			// Token: 0x0400098F RID: 2447
			private readonly Label realExperienceLabel;

			// Token: 0x020000DD RID: 221
			// (Invoke) Token: 0x06000B67 RID: 2919
			public delegate void ValidatedEvent();
		}

		// Token: 0x020000DE RID: 222
		private class LootTableValiodator
		{
			// Token: 0x06000B6A RID: 2922 RVA: 0x000636E8 File Offset: 0x000626E8
			private static bool GridIsEmpty(DataGridView grid)
			{
				for (int row = 0; row < grid.Rows.Count; row++)
				{
					for (int col = 0; col < grid.Columns.Count; col++)
					{
						if (grid[col, row].Value != null && !string.IsNullOrEmpty(grid[col, row].Value.ToString()))
						{
							return false;
						}
					}
				}
				return true;
			}

			// Token: 0x06000B6B RID: 2923 RVA: 0x0006374C File Offset: 0x0006274C
			public LootTableValiodator(DataGridView _lootTableGrid)
			{
				this.lootTableGrid = _lootTableGrid;
			}

			// Token: 0x06000B6C RID: 2924 RVA: 0x0006375C File Offset: 0x0006275C
			public bool Validate()
			{
				DataGridView grid = this.lootTableGrid;
				if (grid == null)
				{
					return false;
				}
				if (!QuestEditorForm.LootTableValiodator.GridIsEmpty(this.lootTableGrid))
				{
					QuestEditorForm.ErrorTypes[] array = new QuestEditorForm.ErrorTypes[1];
					QuestEditorForm.ErrorTypes[] mandatoryError = array;
					QuestEditorForm.ErrorTypes[] mandatoryIntError = new QuestEditorForm.ErrorTypes[]
					{
						QuestEditorForm.ErrorTypes.MANDATORY_FIELD,
						QuestEditorForm.ErrorTypes.INT_FORMAT
					};
					QuestEditorForm.ErrorTypes[] mandatoryProbabilityError = new QuestEditorForm.ErrorTypes[]
					{
						QuestEditorForm.ErrorTypes.MANDATORY_FIELD,
						QuestEditorForm.ErrorTypes.PROBABILITY_FORMAT
					};
					for (int i = 0; i < grid.Rows.Count; i++)
					{
						if (QuestEditorForm.CheckValue(grid[0, i].Value, new List<QuestEditorForm.ErrorTypes>(mandatoryError), false) || QuestEditorForm.CheckValue(grid[1, i].Value, new List<QuestEditorForm.ErrorTypes>(mandatoryError), false))
						{
							if (!QuestEditorForm.CheckValue(grid[1, i].Value, new List<QuestEditorForm.ErrorTypes>(mandatoryError), true) || !QuestEditorForm.CheckValue(grid[0, i].Value, new List<QuestEditorForm.ErrorTypes>(mandatoryError), true))
							{
								return false;
							}
							if (!QuestEditorForm.CheckValue(grid[2, i].Value, new List<QuestEditorForm.ErrorTypes>(mandatoryProbabilityError), true) || !QuestEditorForm.CheckValue(grid[3, i].Value, new List<QuestEditorForm.ErrorTypes>(mandatoryIntError), true) || !QuestEditorForm.CheckValue(grid[4, i].Value, new List<QuestEditorForm.ErrorTypes>(mandatoryIntError), true))
							{
								return false;
							}
							if (QuestEditorForm.QuestEditorFormData.ConvertToInteger(grid[3, i].Value) > QuestEditorForm.QuestEditorFormData.ConvertToInteger(grid[4, i].Value))
							{
								MessageBox.Show(Strings.QUEST_EDITOR_MINMAX_ERROR_MESSAGE, Strings.QUEST_EDITOR_ERROR_TITLE, MessageBoxButtons.OK, MessageBoxIcon.Hand);
								return false;
							}
						}
					}
					if (!QuestEditorForm.CheckIndexColumns(grid, new int[]
					{
						1,
						0
					}, false))
					{
						return false;
					}
				}
				return true;
			}

			// Token: 0x04000991 RID: 2449
			private readonly DataGridView lootTableGrid;
		}

		// Token: 0x020000DF RID: 223
		private class SimpleGridValidator
		{
			// Token: 0x06000B6D RID: 2925 RVA: 0x000638F8 File Offset: 0x000628F8
			public SimpleGridValidator(DataGridView _grid, SortedList<int, int> _gridCol)
			{
				this.grid = _grid;
				this.gridCol = _gridCol;
			}

			// Token: 0x06000B6E RID: 2926 RVA: 0x0006390E File Offset: 0x0006290E
			public SimpleGridValidator(DataGridView _grid, int gameObjectCol, int qtyCol)
			{
				this.grid = _grid;
				this.gridCol = new SortedList<int, int>(1);
				this.gridCol.Add(gameObjectCol, qtyCol);
			}

			// Token: 0x06000B6F RID: 2927 RVA: 0x00063938 File Offset: 0x00062938
			public virtual bool Validate()
			{
				if (this.grid == null)
				{
					return false;
				}
				QuestEditorForm.ErrorTypes[] mandatoryIntError = new QuestEditorForm.ErrorTypes[]
				{
					QuestEditorForm.ErrorTypes.MANDATORY_FIELD,
					QuestEditorForm.ErrorTypes.INT_FORMAT
				};
				List<GameObjectClass> enteredObjects = new List<GameObjectClass>();
				for (int i = 0; i < this.grid.Rows.Count; i++)
				{
					foreach (KeyValuePair<int, int> pair in this.gridCol)
					{
						GameObjectClass gameObj = this.grid[pair.Key, i].Value as GameObjectClass;
						if (gameObj != null)
						{
							if (pair.Value > -1 && !QuestEditorForm.CheckValue(this.grid[pair.Value, i].Value, new List<QuestEditorForm.ErrorTypes>(mandatoryIntError), true))
							{
								return false;
							}
							if (enteredObjects.Contains(gameObj))
							{
								MessageBox.Show(Strings.QUEST_EDITOR_GRIDINDEX_ERROR, Strings.QUEST_EDITOR_ERROR_TITLE, MessageBoxButtons.OK, MessageBoxIcon.Hand);
								return false;
							}
							enteredObjects.Add(gameObj);
						}
					}
				}
				return true;
			}

			// Token: 0x06000B70 RID: 2928 RVA: 0x00063A50 File Offset: 0x00062A50
			public int GetFilledObjectsCount()
			{
				int filledRowsCount = 0;
				for (int row = 0; row < this.grid.Rows.Count; row++)
				{
					foreach (int gameObjectCol in this.gridCol.Keys)
					{
						if (this.grid[gameObjectCol, row].Value != null)
						{
							filledRowsCount++;
						}
					}
				}
				return filledRowsCount;
			}

			// Token: 0x04000992 RID: 2450
			private readonly DataGridView grid;

			// Token: 0x04000993 RID: 2451
			private readonly SortedList<int, int> gridCol;
		}

		// Token: 0x020000E0 RID: 224
		private class RewAlternativeGridValidator : QuestEditorForm.SimpleGridValidator
		{
			// Token: 0x06000B71 RID: 2929 RVA: 0x00063AD4 File Offset: 0x00062AD4
			public RewAlternativeGridValidator(DataGridView _grid, SortedList<int, int> gridCol) : base(_grid, gridCol)
			{
			}

			// Token: 0x06000B72 RID: 2930 RVA: 0x00063ADE File Offset: 0x00062ADE
			public override bool Validate()
			{
				if (!base.Validate())
				{
					return false;
				}
				if (base.GetFilledObjectsCount() == 1)
				{
					MessageBox.Show(Strings.QUEST_EDITOR_WRONALTITEMSSNUMBERERROR_MESSAGE, Strings.QUEST_EDITOR_ERROR_TITLE, MessageBoxButtons.OK, MessageBoxIcon.Hand);
					return false;
				}
				return true;
			}
		}

		// Token: 0x020000E1 RID: 225
		private class SimpleGridListener
		{
			// Token: 0x14000034 RID: 52
			// (add) Token: 0x06000B73 RID: 2931 RVA: 0x00063B09 File Offset: 0x00062B09
			// (remove) Token: 0x06000B74 RID: 2932 RVA: 0x00063B22 File Offset: 0x00062B22
			public event QuestEditorForm.SimpleGridListener.GeneralGridListenerEvent Changed;

			// Token: 0x06000B75 RID: 2933 RVA: 0x00063B3C File Offset: 0x00062B3C
			private void ShowSelectDialog(int rowNum, int colNum)
			{
				foreach (int column in this.gridCol)
				{
					if (this.view == null || rowNum < 0 || rowNum > this.grid.Rows.Count - 1)
					{
						break;
					}
					if (colNum == column)
					{
						QuestSelectDialogForm dialogForm = new QuestSelectDialogForm(this.context.QuestEnvironment, this.view, this.grid[column, rowNum].Value as GameObjectClass);
						if (dialogForm.ShowDialog() == DialogResult.OK)
						{
							GameObjectClass gameObj = dialogForm.GetSelectedItem();
							if (gameObj != null && this.grid.Columns[column].ReadOnly && rowNum == this.grid.Rows.Count - 1 && column == this.grid.Columns.Count - 1)
							{
								this.grid.Rows.Add();
							}
							this.grid[column, rowNum].Value = gameObj;
						}
					}
				}
			}

			// Token: 0x06000B76 RID: 2934 RVA: 0x00063C64 File Offset: 0x00062C64
			private void OnGridDblClick(object sender, DataGridViewCellEventArgs e)
			{
				this.ShowSelectDialog(e.RowIndex, e.ColumnIndex);
			}

			// Token: 0x06000B77 RID: 2935 RVA: 0x00063C78 File Offset: 0x00062C78
			private void OnToolStripClicked(object sender, ToolStripItemClickedEventArgs e)
			{
				if (e != null && e.ClickedItem != null && this.grid.SelectedCells != null && this.grid.SelectedCells.Count > 0)
				{
					string tag = e.ClickedItem.Tag as string;
					string oldValue = null;
					GameObjectClass gameObj = this.grid.SelectedCells[0].Value as GameObjectClass;
					if (gameObj != null)
					{
						oldValue = gameObj.GameObject;
					}
					if (tag == "select_dialog")
					{
						this.ShowSelectDialog(this.grid.SelectedCells[0].RowIndex, this.grid.SelectedCells[0].ColumnIndex);
						return;
					}
					string newValue;
					if (QuestEditorForm.OnToolStripClicked(this.context, this.grid, tag, oldValue, out newValue))
					{
						if (!string.IsNullOrEmpty(newValue))
						{
							this.grid.SelectedCells[0].Value = this.context.QuestEnvironment.FindGameObject(this.view, newValue);
							return;
						}
						this.grid.SelectedCells[0].Value = null;
					}
				}
			}

			// Token: 0x06000B78 RID: 2936 RVA: 0x00063D9C File Offset: 0x00062D9C
			private void OnChanged(object sender, EventArgs e)
			{
				if (this.grid != null && this.grid.SelectedCells != null && this.grid.SelectedCells.Count > 0)
				{
					QuestEditorForm.EditCellContextMenu(this.grid.SelectedCells[0]);
				}
				if (this.Changed != null)
				{
					this.Changed(sender, e);
				}
			}

			// Token: 0x06000B79 RID: 2937 RVA: 0x00063DFC File Offset: 0x00062DFC
			private void OnCellEnter(object sender, DataGridViewCellEventArgs e)
			{
				if (this.grid != null && this.contextMenuStrip != null && this.gridCol != null && this.gridCol.Contains(e.ColumnIndex) && e.RowIndex > -1 && e.RowIndex < this.grid.Rows.Count)
				{
					this.grid[e.ColumnIndex, e.RowIndex].ContextMenuStrip = this.contextMenuStrip;
					QuestEditorForm.EditCellContextMenu(this.grid[e.ColumnIndex, e.RowIndex]);
				}
			}

			// Token: 0x06000B7A RID: 2938 RVA: 0x00063E98 File Offset: 0x00062E98
			private void OnCellLeave(object sender, DataGridViewCellEventArgs e)
			{
				if (this.grid != null && this.gridCol != null && this.gridCol.Contains(e.ColumnIndex) && e.RowIndex > -1 && e.RowIndex < this.grid.Rows.Count)
				{
					this.grid[e.ColumnIndex, e.RowIndex].ContextMenuStrip = null;
				}
			}

			// Token: 0x06000B7B RID: 2939 RVA: 0x00063F08 File Offset: 0x00062F08
			private void OnKeyDown(object sender, KeyEventArgs e)
			{
				if (this.grid != null && this.grid.SelectedCells != null && e.KeyCode == Keys.Delete)
				{
					foreach (object obj in this.grid.SelectedCells)
					{
						DataGridViewCell cell = (DataGridViewCell)obj;
						cell.Value = null;
					}
				}
			}

			// Token: 0x06000B7C RID: 2940 RVA: 0x00063F88 File Offset: 0x00062F88
			private void Init()
			{
				if (this.grid != null)
				{
					this.grid.CellDoubleClick += this.OnGridDblClick;
					this.grid.RowsRemoved += new DataGridViewRowsRemovedEventHandler(this.OnChanged);
					this.grid.CellValueChanged += new DataGridViewCellEventHandler(this.OnChanged);
					this.grid.KeyDown += this.OnKeyDown;
					this.grid.CellEnter += this.OnCellEnter;
					this.grid.CellLeave += this.OnCellLeave;
					if (this.contextMenuStrip != null)
					{
						this.contextMenuStrip.ItemClicked += this.OnToolStripClicked;
					}
				}
			}

			// Token: 0x06000B7D RID: 2941 RVA: 0x0006404C File Offset: 0x0006304C
			public SimpleGridListener(MainForm.Context _context, ContextMenuStrip _contextMenuStrip, DataGridView _grid, int gameObjectCol, Tools.DBGameObjects.View _view)
			{
				this.context = _context;
				this.contextMenuStrip = _contextMenuStrip;
				this.grid = _grid;
				this.gridCol = new List<int>(1);
				this.gridCol.Add(gameObjectCol);
				this.view = _view;
				this.Init();
			}

			// Token: 0x06000B7E RID: 2942 RVA: 0x0006409B File Offset: 0x0006309B
			public SimpleGridListener(MainForm.Context _context, ContextMenuStrip _contextMenuStrip, DataGridView _grid, IEnumerable<int> _gridCol, Tools.DBGameObjects.View _view)
			{
				this.context = _context;
				this.contextMenuStrip = _contextMenuStrip;
				this.grid = _grid;
				this.gridCol = new List<int>(_gridCol);
				this.view = _view;
				this.Init();
			}

			// Token: 0x04000994 RID: 2452
			private readonly DataGridView grid;

			// Token: 0x04000995 RID: 2453
			private readonly List<int> gridCol;

			// Token: 0x04000996 RID: 2454
			private readonly Tools.DBGameObjects.View view;

			// Token: 0x04000997 RID: 2455
			private readonly MainForm.Context context;

			// Token: 0x04000998 RID: 2456
			private readonly ContextMenuStrip contextMenuStrip;

			// Token: 0x020000E2 RID: 226
			// (Invoke) Token: 0x06000B80 RID: 2944
			public delegate void GeneralGridListenerEvent(object sender, EventArgs e);
		}

		// Token: 0x020000E3 RID: 227
		private class SimpleStringValueListener
		{
			// Token: 0x14000035 RID: 53
			// (add) Token: 0x06000B83 RID: 2947 RVA: 0x000640D3 File Offset: 0x000630D3
			// (remove) Token: 0x06000B84 RID: 2948 RVA: 0x000640EC File Offset: 0x000630EC
			public event QuestEditorForm.SimpleStringValueListener.SimpleStringValueListenerEvent Changed;

			// Token: 0x06000B85 RID: 2949 RVA: 0x00064108 File Offset: 0x00063108
			private void OnButtonClick(object sender, EventArgs e)
			{
				if (this.view == null || !this.Enabled)
				{
					return;
				}
				QuestSelectDialogForm dialogForm = new QuestSelectDialogForm(this.context.QuestEnvironment, this.view, this.value);
				if (dialogForm.ShowDialog() == DialogResult.OK)
				{
					this.SetValue(dialogForm.GetSelectedItem());
					if (this.Changed != null)
					{
						this.Changed(sender, e);
					}
				}
			}

			// Token: 0x06000B86 RID: 2950 RVA: 0x00064170 File Offset: 0x00063170
			private void OnToolStripClicked(object sender, ToolStripItemClickedEventArgs e)
			{
				if (e != null && e.ClickedItem != null)
				{
					string tag = e.ClickedItem.Tag as string;
					if (tag == "select_dialog")
					{
						this.OnButtonClick(this, e);
						return;
					}
					string _value;
					if (QuestEditorForm.OnToolStripClicked(this.context, this.textBox, tag, (this.value != null) ? this.value.GameObject : null, out _value))
					{
						this.SetValueByKey(_value);
					}
				}
			}

			// Token: 0x06000B87 RID: 2951 RVA: 0x000641E4 File Offset: 0x000631E4
			public SimpleStringValueListener(MainForm.Context _context, TextBox _textBox, Button _button, Tools.DBGameObjects.View _view)
			{
				this.context = _context;
				this.textBox = _textBox;
				this.button = _button;
				this.view = _view;
				if (this.button != null)
				{
					this.button.Click += this.OnButtonClick;
				}
				if (this.textBox != null && this.textBox.ContextMenuStrip != null)
				{
					this.textBox.ContextMenuStrip.ItemClicked += this.OnToolStripClicked;
				}
			}

			// Token: 0x06000B88 RID: 2952 RVA: 0x00064264 File Offset: 0x00063264
			public void SetValue(GameObjectClass _value)
			{
				this.value = _value;
				if (this.textBox != null)
				{
					if (this.value != null)
					{
						if (!string.IsNullOrEmpty(this.value.Name))
						{
							this.textBox.Text = this.value.Name;
							return;
						}
						this.textBox.Text = this.value.GameObject;
						return;
					}
					else
					{
						this.textBox.Text = string.Empty;
					}
				}
			}

			// Token: 0x06000B89 RID: 2953 RVA: 0x000642D8 File Offset: 0x000632D8
			public void SetValueByKey(string key)
			{
				if (this.view != null)
				{
					if (!string.IsNullOrEmpty(key))
					{
						this.SetValue(this.context.QuestEnvironment.FindGameObject(this.view, key));
						return;
					}
					this.SetValue(null);
				}
			}

			// Token: 0x06000B8A RID: 2954 RVA: 0x0006430F File Offset: 0x0006330F
			public string GetValue()
			{
				if (this.value != null)
				{
					return this.value.GameObject;
				}
				return string.Empty;
			}

			// Token: 0x17000237 RID: 567
			// (get) Token: 0x06000B8B RID: 2955 RVA: 0x0006432A File Offset: 0x0006332A
			// (set) Token: 0x06000B8C RID: 2956 RVA: 0x00064337 File Offset: 0x00063337
			public bool Enabled
			{
				get
				{
					return this.button.Enabled;
				}
				set
				{
					this.button.Enabled = value;
				}
			}

			// Token: 0x0400099A RID: 2458
			private GameObjectClass value;

			// Token: 0x0400099B RID: 2459
			private readonly TextBox textBox;

			// Token: 0x0400099C RID: 2460
			private readonly Button button;

			// Token: 0x0400099D RID: 2461
			private readonly Tools.DBGameObjects.View view;

			// Token: 0x0400099E RID: 2462
			private readonly MainForm.Context context;

			// Token: 0x020000E4 RID: 228
			// (Invoke) Token: 0x06000B8E RID: 2958
			public delegate void SimpleStringValueListenerEvent(object sender, EventArgs e);
		}

		// Token: 0x020000E5 RID: 229
		private class CounterGridHandler
		{
			// Token: 0x14000036 RID: 54
			// (add) Token: 0x06000B91 RID: 2961 RVA: 0x00064345 File Offset: 0x00063345
			// (remove) Token: 0x06000B92 RID: 2962 RVA: 0x0006435E File Offset: 0x0006335E
			public event QuestEditorForm.CounterGridHandler.CounterGridHandlerEvent Changed;

			// Token: 0x06000B93 RID: 2963 RVA: 0x00064378 File Offset: 0x00063378
			private void AddColumn()
			{
				DataGridViewColumn oldCol = this.grid.Columns[7];
				DataGridViewTextBoxColumn newCol = new DataGridViewTextBoxColumn();
				newCol.SortMode = oldCol.SortMode;
				newCol.HeaderText = oldCol.HeaderText;
				this.grid.Columns.Add(newCol);
			}

			// Token: 0x06000B94 RID: 2964 RVA: 0x000643C8 File Offset: 0x000633C8
			private void UpdateCurrentRowType(int rowIndex, string type)
			{
				this.SetContextMenu(type);
				if (rowIndex != -1)
				{
					DataGridViewCell removeOnAbandonCell = this.grid[6, rowIndex];
					removeOnAbandonCell.ReadOnly = (type != "Item");
					if (removeOnAbandonCell.ReadOnly)
					{
						removeOnAbandonCell.Value = true;
					}
					removeOnAbandonCell.Style.BackColor = ((type != "Item") ? Color.FromKnownColor(KnownColor.Control) : Color.FromKnownColor(KnownColor.Window));
				}
			}

			// Token: 0x06000B95 RID: 2965 RVA: 0x0006443C File Offset: 0x0006343C
			private void SetContextMenu(string type)
			{
				if (this.grid != null && this.contextMenuStrip != null)
				{
					foreach (object obj in this.contextMenuStrip.Items)
					{
						ToolStripMenuItem item = (ToolStripMenuItem)obj;
						string tag = item.Tag as string;
						string key;
						switch (key = tag)
						{
						case "create_quest_item":
							item.Visible = (type == "Item");
							break;
						case "create_mob":
							item.Visible = (type == "Kill");
							break;
						case "create_device":
							item.Visible = (type == "Kill");
							break;
						case "bind_exploit":
							item.Visible = (type == "Special");
							break;
						case "clear_cell":
							item.Visible = (type != "Special");
							break;
						case "edit_properties":
						case "edit_game_name":
							item.Visible = (this.grid.SelectedCells != null && this.grid.SelectedCells.Count > 0 && this.grid.SelectedCells[0].Value != null);
							break;
						case "select_dialog":
							item.Visible = (type != "Special");
							break;
						}
					}
					this.contextMenuStrip.Enabled = !this.counterFieldPrototiped;
				}
			}

			// Token: 0x06000B96 RID: 2966 RVA: 0x0006465C File Offset: 0x0006365C
			private bool DoesCounterNameOccupied(string counterName)
			{
				if (File.Exists(EditorEnvironment.DataFolder + counterName))
				{
					return true;
				}
				if (this.grid != null)
				{
					foreach (object obj in ((IEnumerable)this.grid.Rows))
					{
						DataGridViewRow row = (DataGridViewRow)obj;
						if (row.Cells[1].Value as string == "Special")
						{
							QuestEditorForm.CounterGridHandler.SpecialCounter counter = row.Cells[7].Value as QuestEditorForm.CounterGridHandler.SpecialCounter;
							if (counter != null && counter.CounterId == counterName)
							{
								return true;
							}
						}
					}
				}
				return !this.questEditorFormData.IsNew && QuestEditor.DoesCounterNameOccupied(this.questEditorFormData.CurrentQuest, counterName);
			}

			// Token: 0x06000B97 RID: 2967 RVA: 0x00064748 File Offset: 0x00063748
			private string GetNewCounterName()
			{
				int countNum = 0;
				string countName = null;
				string questFolder = this.questEditorFormData.GetCurrentQuestFolder();
				if (!string.IsNullOrEmpty(questFolder))
				{
					while (string.IsNullOrEmpty(countName) || this.DoesCounterNameOccupied(countName))
					{
						countName = string.Format("{0}CountId_{1}.xdb", questFolder, ++countNum);
					}
				}
				return countName;
			}

			// Token: 0x06000B98 RID: 2968 RVA: 0x00064798 File Offset: 0x00063798
			private void OnValueChanged(object sender, DataGridViewCellEventArgs e)
			{
				if (this.grid != null && this.grid.SelectedCells != null && this.grid.SelectedCells.Count > 0)
				{
					DataGridViewCell cell = this.grid.SelectedCells[0];
					if (cell != null && cell.ColumnIndex == e.ColumnIndex && cell.RowIndex == e.RowIndex)
					{
						QuestEditorForm.EditCellContextMenu(this.grid.SelectedCells[0]);
						if (cell.ColumnIndex == this.grid.Columns.Count - 1 && cell.ColumnIndex >= 7)
						{
							this.AddColumn();
						}
					}
				}
				if (this.Changed != null)
				{
					this.Changed(sender, e);
				}
			}

			// Token: 0x06000B99 RID: 2969 RVA: 0x00064858 File Offset: 0x00063858
			private Tools.DBGameObjects.View GetViewByCell(int rowIndex, int columnIndex)
			{
				string type = this.grid[1, rowIndex].Value as string;
				string a;
				if ((a = type) != null)
				{
					if (a == "Item")
					{
						return this.itemView;
					}
					if (a == "Kill")
					{
						return this.respawnableResourceView;
					}
					if (a == "KillAvatar")
					{
						return this.killAvatarCounterObjectsView;
					}
				}
				return null;
			}

			// Token: 0x06000B9A RID: 2970 RVA: 0x000648C4 File Offset: 0x000638C4
			private void ShowSelectDialog(int rowIndex, int columnIndex)
			{
				if (!this.counterFieldPrototiped && this.grid != null && rowIndex > -1 && rowIndex < this.grid.Rows.Count && columnIndex >= 7 && columnIndex < this.grid.Columns.Count)
				{
					string type = this.grid[1, rowIndex].Value as string;
					if (type != "Honor")
					{
						Tools.DBGameObjects.View view = this.GetViewByCell(rowIndex, columnIndex);
						if (view != null)
						{
							QuestSelectDialogForm dialogForm = new QuestSelectDialogForm(this.context.QuestEnvironment, view, this.grid[columnIndex, rowIndex].Value as GameObjectClass);
							if (dialogForm.ShowDialog() == DialogResult.OK)
							{
								this.grid[columnIndex, rowIndex].Value = dialogForm.GetSelectedItem();
								return;
							}
						}
					}
					else
					{
						SelectRankDialogForm dialogForm2 = new SelectRankDialogForm();
						if (dialogForm2.ShowDialog(this.grid) == DialogResult.OK)
						{
							this.grid[columnIndex, rowIndex].Value = dialogForm2.SelectedItem;
						}
					}
				}
			}

			// Token: 0x06000B9B RID: 2971 RVA: 0x000649CA File Offset: 0x000639CA
			private void OnGridDblClick(object sender, DataGridViewCellEventArgs e)
			{
				this.ShowSelectDialog(e.RowIndex, e.ColumnIndex);
			}

			// Token: 0x06000B9C RID: 2972 RVA: 0x000649DE File Offset: 0x000639DE
			private void OnRowRemoved(object sender, EventArgs e)
			{
				if (this.Changed != null)
				{
					this.Changed(sender, e);
				}
			}

			// Token: 0x06000B9D RID: 2973 RVA: 0x000649F8 File Offset: 0x000639F8
			private void OnRowEnter(object sender, DataGridViewCellEventArgs e)
			{
				string type = this.grid[1, e.RowIndex].Value as string;
				this.UpdateCurrentRowType(e.RowIndex, type);
			}

			// Token: 0x06000B9E RID: 2974 RVA: 0x00064A30 File Offset: 0x00063A30
			private void OnCellEnter(object sender, DataGridViewCellEventArgs e)
			{
				if (this.grid != null && this.grid.CurrentCell != null)
				{
					string type = this.grid[1, this.grid.CurrentRow.Index].Value as string;
					if (this.grid.CurrentCell.ColumnIndex == 7)
					{
						this.grid.CurrentCell.ContextMenuStrip = this.contextMenuStrip;
					}
					else if (this.grid.CurrentCell.ColumnIndex > 7 && (type == "Kill" || type == "Item"))
					{
						this.grid.CurrentCell.ContextMenuStrip = this.contextMenuStrip;
					}
					QuestEditorForm.EditCellContextMenu(this.grid[e.ColumnIndex, e.RowIndex]);
				}
			}

			// Token: 0x06000B9F RID: 2975 RVA: 0x00064B09 File Offset: 0x00063B09
			private void OnCellLeave(object sender, DataGridViewCellEventArgs e)
			{
				if (this.grid != null && this.grid.CurrentCell != null)
				{
					this.grid.CurrentCell.ContextMenuStrip = null;
				}
			}

			// Token: 0x06000BA0 RID: 2976 RVA: 0x00064B34 File Offset: 0x00063B34
			private void OnEditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
			{
				if (this.grid != null && this.grid.CurrentCell.ColumnIndex == 1)
				{
					ComboBox typeComboBox = this.grid.EditingControl as ComboBox;
					if (typeComboBox != null)
					{
						typeComboBox.SelectedIndexChanged -= this.OnCounterTypeSelect;
						typeComboBox.SelectedIndexChanged += this.OnCounterTypeSelect;
					}
				}
			}

			// Token: 0x06000BA1 RID: 2977 RVA: 0x00064B94 File Offset: 0x00063B94
			private void OnCounterTypeSelect(object sender, EventArgs e)
			{
				if (this.grid != null && this.grid.CurrentCell.ColumnIndex == 1)
				{
					ComboBox typeComboBox = this.grid.EditingControl as ComboBox;
					if (typeComboBox != null)
					{
						string type = this.grid.CurrentCell.Value as string;
						if (type != typeComboBox.Text)
						{
							foreach (object obj in this.grid.CurrentRow.Cells)
							{
								DataGridViewCell cell = (DataGridViewCell)obj;
								if (cell.ColumnIndex == 3)
								{
									cell.Value = 1;
								}
								else if (cell.ColumnIndex == 5)
								{
									cell.Value = true;
								}
								else if (cell.ColumnIndex >= 7)
								{
									cell.Value = null;
								}
							}
							if (typeComboBox.Text == "Special")
							{
								this.grid[7, this.grid.CurrentCell.RowIndex].Value = new QuestEditorForm.CounterGridHandler.SpecialCounter(this.GetNewCounterName());
							}
							this.grid.CurrentCell.Value = typeComboBox.Text;
							this.UpdateCurrentRowType(this.grid.CurrentCell.RowIndex, typeComboBox.Text);
						}
					}
				}
			}

			// Token: 0x06000BA2 RID: 2978 RVA: 0x00064D00 File Offset: 0x00063D00
			private void OnToolStripClicked(object sender, ToolStripItemClickedEventArgs e)
			{
				if (e != null && e.ClickedItem != null && this.grid.SelectedCells != null && this.grid.SelectedCells.Count > 0)
				{
					string tag = e.ClickedItem.Tag as string;
					string oldValue = null;
					GameObjectClass gameObj = this.grid.SelectedCells[0].Value as GameObjectClass;
					if (gameObj != null)
					{
						oldValue = gameObj.GameObject;
					}
					if (tag == "select_dialog")
					{
						this.ShowSelectDialog(this.grid.SelectedCells[0].RowIndex, this.grid.SelectedCells[0].ColumnIndex);
						return;
					}
					string newValue;
					if (tag == "bind_exploit")
					{
						QuestEditorForm.CounterGridHandler.SpecialCounter counter = this.grid.SelectedCells[0].Value as QuestEditorForm.CounterGridHandler.SpecialCounter;
						if (counter != null)
						{
							if (!File.Exists(EditorEnvironment.DataFolder + counter.CounterId))
							{
								MessageBox.Show(this.grid, Strings.QUEST_EDITOR_CANT_BIND_EXPLOT, Strings.QUEST_EDITOR_TITLE, MessageBoxButtons.OK, MessageBoxIcon.Hand);
								return;
							}
							BindExploitToCounterForm dialogForm = new BindExploitToCounterForm(this.questEditorFormData.CurrentQuest, counter.CounterId);
							dialogForm.ShowDialog(this.grid);
							if (dialogForm.ExploitMan != null)
							{
								if (!this.context.DatabaseEditor.Visible)
								{
									this.context.ShowDatabaseEditor(true);
								}
								this.context.DatabaseEditor.PropertyControl.SelectedObject = dialogForm.ExploitMan;
								return;
							}
						}
					}
					else if (QuestEditorForm.OnToolStripClicked(this.context, this.grid, tag, oldValue, out newValue))
					{
						Tools.DBGameObjects.View view = this.GetViewByCell(this.grid.SelectedCells[0].RowIndex, this.grid.SelectedCells[0].ColumnIndex);
						if (!string.IsNullOrEmpty(newValue))
						{
							this.grid.SelectedCells[0].Value = this.context.QuestEnvironment.FindGameObject(view, newValue);
							return;
						}
						this.grid.SelectedCells[0].Value = null;
					}
				}
			}

			// Token: 0x06000BA3 RID: 2979 RVA: 0x00064F28 File Offset: 0x00063F28
			private void OnKeyDown(object sender, KeyEventArgs e)
			{
				if (this.grid != null && this.grid.SelectedCells != null && e.KeyCode == Keys.Delete)
				{
					foreach (object obj in this.grid.SelectedCells)
					{
						DataGridViewCell cell = (DataGridViewCell)obj;
						cell.Value = null;
					}
				}
			}

			// Token: 0x06000BA4 RID: 2980 RVA: 0x00064FA8 File Offset: 0x00063FA8
			public CounterGridHandler(QuestEditorForm.QuestEditorFormData _questEditorFormData, MainForm.Context _context, ContextMenuStrip _contextMenuStrip, DataGridView _grid, Tools.DBGameObjects.View _itemView, Tools.DBGameObjects.View _respawnableResourceView, Tools.DBGameObjects.View _killAvatarCounterObjects)
			{
				this.questEditorFormData = _questEditorFormData;
				this.grid = _grid;
				this.itemView = _itemView;
				this.respawnableResourceView = _respawnableResourceView;
				this.killAvatarCounterObjectsView = _killAvatarCounterObjects;
				this.context = _context;
				this.contextMenuStrip = _contextMenuStrip;
				if (this.grid != null)
				{
					this.grid.CellDoubleClick += this.OnGridDblClick;
					this.grid.RowsRemoved += new DataGridViewRowsRemovedEventHandler(this.OnRowRemoved);
					this.grid.RowEnter += this.OnRowEnter;
					this.grid.EditingControlShowing += this.OnEditingControlShowing;
					this.grid.CellEnter += this.OnCellEnter;
					this.grid.CellLeave += this.OnCellLeave;
					this.grid.KeyDown += this.OnKeyDown;
					this.defaultResColCnt = this.grid.Columns.Count - 7;
				}
				if (this.contextMenuStrip != null)
				{
					this.contextMenuStrip.ItemClicked += this.OnToolStripClicked;
				}
			}

			// Token: 0x06000BA5 RID: 2981 RVA: 0x000650DC File Offset: 0x000640DC
			public bool Validate()
			{
				if (this.counterFieldPrototiped)
				{
					return true;
				}
				if (this.grid != null)
				{
					QuestEditorForm.ErrorTypes[] array = new QuestEditorForm.ErrorTypes[1];
					QuestEditorForm.ErrorTypes[] mandatoryError = array;
					QuestEditorForm.ErrorTypes[] mandatoryIntError = new QuestEditorForm.ErrorTypes[]
					{
						QuestEditorForm.ErrorTypes.MANDATORY_FIELD,
						QuestEditorForm.ErrorTypes.INT_FORMAT
					};
					List<QuestEditorForm.ErrorTypes> mandatoryErrorList = new List<QuestEditorForm.ErrorTypes>(mandatoryError);
					List<QuestEditorForm.ErrorTypes> mandatoryIntErrorList = new List<QuestEditorForm.ErrorTypes>(mandatoryIntError);
					foreach (object obj in ((IEnumerable)this.grid.Rows))
					{
						DataGridViewRow row = (DataGridViewRow)obj;
						string type = row.Cells[1].Value as string;
						if (type != null)
						{
							if (!QuestEditorForm.CheckValue(row.Cells[3].Value, new List<QuestEditorForm.ErrorTypes>(mandatoryIntErrorList), true))
							{
								return false;
							}
							if (type != "Kill" && type != "Item")
							{
								if (!QuestEditorForm.CheckValue(row.Cells[7].Value, new List<QuestEditorForm.ErrorTypes>(mandatoryErrorList), true))
								{
									return false;
								}
							}
							else
							{
								bool filled = false;
								for (int colNum = 7; colNum < this.grid.Columns.Count; colNum++)
								{
									if (QuestEditorForm.CheckValue(row.Cells[colNum].Value, mandatoryErrorList, false))
									{
										filled = true;
										break;
									}
								}
								if (!filled)
								{
									QuestEditorForm.CheckValue(row.Cells[7].Value, mandatoryErrorList, true);
									return false;
								}
							}
						}
					}
					int[] indexColumns = new int[this.grid.Columns.Count - 7];
					indexColumns[0] = 7;
					for (int index = 1; index < indexColumns.Length; index++)
					{
						indexColumns[index] = indexColumns[index - 1] + 1;
					}
					if (!QuestEditorForm.CheckIndexColumns(this.grid, indexColumns, true))
					{
						return false;
					}
					return true;
				}
				return true;
			}

			// Token: 0x06000BA6 RID: 2982 RVA: 0x000652D4 File Offset: 0x000642D4
			public void Load()
			{
				if (this.grid != null)
				{
					this.grid.CellValueChanged -= this.OnValueChanged;
					for (int index = this.grid.Columns.Count - 1; index > 7 + this.defaultResColCnt - 1; index--)
					{
						this.grid.Columns.RemoveAt(index);
					}
					Dictionary<int, QuestClass.QuestCounter> counters;
					this.questEditorFormData.CurrentQuest.GetCounters(out counters, out this.counterFieldPrototiped);
					if (counters != null)
					{
						QuestEditorForm.InitGrid(this.grid, 6, 5, true);
						int rowIndex = 0;
						foreach (KeyValuePair<int, QuestClass.QuestCounter> counterArrayElem in counters)
						{
							DataGridViewRow row;
							if (this.grid.Rows.Count > rowIndex)
							{
								row = this.grid.Rows[rowIndex];
							}
							else
							{
								row = this.grid.Rows[this.grid.Rows.Add()];
							}
							row.Cells[0].Value = counterArrayElem.Key;
							QuestClass.QuestCounter counter = counterArrayElem.Value;
							string type;
							if ((type = counter.Type) != null)
							{
								if (!(type == "gameMechanics.elements.quest.QuestCountItem"))
								{
									if (!(type == "gameMechanics.elements.quest.QuestCountKill"))
									{
										if (!(type == "gameMechanics.elements.quest.QuestCountSpecial"))
										{
											if (!(type == "gameMechanics.elements.quest.QuestCountHonor"))
											{
												if (type == "gameMechanics.elements.quest.QuestCountKillAvatar")
												{
													row.Cells[1].Value = "KillAvatar";
												}
											}
											else
											{
												row.Cells[1].Value = "Honor";
											}
										}
										else
										{
											row.Cells[1].Value = "Special";
										}
									}
									else
									{
										row.Cells[1].Value = "Kill";
									}
								}
								else
								{
									row.Cells[1].Value = "Item";
								}
							}
							row.Cells[2].Value = counter.CustomName;
							row.Cells[3].Value = counter.Qty;
							row.Cells[4].Value = counter.IsInternal;
							row.Cells[5].Value = counter.ShowCounterValue;
							row.Cells[6].Value = counter.RemoveOnAbandon;
							row.Cells[6].Style.BackColor = ((counter.Type != "gameMechanics.elements.quest.QuestCountItem") ? Color.FromKnownColor(KnownColor.Control) : Color.FromKnownColor(KnownColor.Window));
							if (counter.ItemList != null)
							{
								int colNum = 7;
								foreach (string item in counter.ItemList)
								{
									if (!string.IsNullOrEmpty(item))
									{
										if (colNum >= this.grid.Columns.Count - 1)
										{
											this.AddColumn();
										}
										string type2;
										if ((type2 = counter.Type) != null)
										{
											if (!(type2 == "gameMechanics.elements.quest.QuestCountItem"))
											{
												if (!(type2 == "gameMechanics.elements.quest.QuestCountKill"))
												{
													if (!(type2 == "gameMechanics.elements.quest.QuestCountSpecial"))
													{
														if (!(type2 == "gameMechanics.elements.quest.QuestCountHonor"))
														{
															if (type2 == "gameMechanics.elements.quest.QuestCountKillAvatar")
															{
																GameObjectClass go = this.context.QuestEnvironment.FindGameObject(this.killAvatarCounterObjectsView, item);
																if (go == null)
																{
																	MessageBox.Show("Invalid kill avatar counter!", string.Empty, MessageBoxButtons.OK, MessageBoxIcon.Hand);
																}
																row.Cells[colNum].Value = go;
															}
														}
														else
														{
															row.Cells[colNum].Value = item;
														}
													}
													else
													{
														row.Cells[colNum].Value = new QuestEditorForm.CounterGridHandler.SpecialCounter(item);
													}
												}
												else
												{
													row.Cells[colNum].Value = this.context.QuestEnvironment.FindGameObject(this.respawnableResourceView, item);
												}
											}
											else
											{
												row.Cells[colNum].Value = this.context.QuestEnvironment.FindGameObject(this.itemView, item);
											}
										}
										colNum++;
									}
								}
							}
							rowIndex++;
						}
					}
					this.grid.CellValueChanged += this.OnValueChanged;
					this.grid.Enabled = !this.counterFieldPrototiped;
					this.grid.DefaultCellStyle.BackColor = (this.counterFieldPrototiped ? Color.FromKnownColor(KnownColor.Control) : Color.FromKnownColor(KnownColor.Window));
				}
			}

			// Token: 0x06000BA7 RID: 2983 RVA: 0x000657E4 File Offset: 0x000647E4
			public void GetValues(out Dictionary<int, QuestClass.QuestCounter> oldCounters, out List<QuestClass.QuestCounter> newCounters)
			{
				oldCounters = new Dictionary<int, QuestClass.QuestCounter>();
				newCounters = new List<QuestClass.QuestCounter>();
				if (this.grid != null)
				{
					foreach (object obj in ((IEnumerable)this.grid.Rows))
					{
						DataGridViewRow row = (DataGridViewRow)obj;
						List<string> itemList = new List<string>(1);
						string a;
						if ((a = (row.Cells[1].Value as string)) != null)
						{
							string type;
							if (!(a == "Item"))
							{
								if (!(a == "Kill"))
								{
									if (!(a == "Special"))
									{
										if (!(a == "Honor"))
										{
											if (!(a == "KillAvatar"))
											{
												continue;
											}
											type = "gameMechanics.elements.quest.QuestCountKillAvatar";
											GameObjectClass item = row.Cells[7].Value as GameObjectClass;
											if (item != null)
											{
												itemList.Add(item.GameObject);
											}
										}
										else
										{
											type = "gameMechanics.elements.quest.QuestCountHonor";
											string item2 = row.Cells[7].Value as string;
											if (!string.IsNullOrEmpty(item2))
											{
												itemList.Add(item2);
											}
										}
									}
									else
									{
										type = "gameMechanics.elements.quest.QuestCountSpecial";
										QuestEditorForm.CounterGridHandler.SpecialCounter item3 = row.Cells[7].Value as QuestEditorForm.CounterGridHandler.SpecialCounter;
										if (item3 != null)
										{
											itemList.Add(item3.CounterId);
										}
									}
								}
								else
								{
									type = "gameMechanics.elements.quest.QuestCountKill";
									for (int colNum = 7; colNum < this.grid.Columns.Count; colNum++)
									{
										GameObjectClass item4 = row.Cells[colNum].Value as GameObjectClass;
										if (item4 != null)
										{
											itemList.Add(item4.GameObject);
										}
									}
								}
							}
							else
							{
								type = "gameMechanics.elements.quest.QuestCountItem";
								for (int colNum2 = 7; colNum2 < this.grid.Columns.Count; colNum2++)
								{
									GameObjectClass item5 = row.Cells[colNum2].Value as GameObjectClass;
									if (item5 != null)
									{
										itemList.Add(item5.GameObject);
									}
								}
							}
							string customName = row.Cells[2].Value as string;
							int qty = QuestEditorForm.QuestEditorFormData.ConvertToInteger(row.Cells[3].Value);
							bool isInternal = QuestEditorForm.QuestEditorFormData.ConvertToBool(row.Cells[4].Value);
							bool showCounter = QuestEditorForm.QuestEditorFormData.ConvertToBool(row.Cells[5].Value);
							bool removeOnAbandon = QuestEditorForm.QuestEditorFormData.ConvertToBool(row.Cells[6].Value);
							QuestClass.QuestCounter counter = new QuestClass.QuestCounter(type, customName, qty, isInternal, showCounter, removeOnAbandon, itemList);
							bool counterIsOld = false;
							if (row.Cells[0].Value != null)
							{
								int counterId = QuestEditorForm.QuestEditorFormData.ConvertToInteger(row.Cells[0].Value);
								if (!oldCounters.ContainsKey(counterId))
								{
									oldCounters.Add(counterId, counter);
									counterIsOld = true;
								}
							}
							if (!counterIsOld)
							{
								newCounters.Add(counter);
							}
						}
					}
				}
			}

			// Token: 0x06000BA8 RID: 2984 RVA: 0x00065AFC File Offset: 0x00064AFC
			public void ClearOldCounters()
			{
				if (this.grid != null)
				{
					foreach (object obj in ((IEnumerable)this.grid.Rows))
					{
						DataGridViewRow row = (DataGridViewRow)obj;
						row.Cells[0].Value = null;
						if (row.Cells[1].Value as string == "Special")
						{
							row.Cells[7].Value = new QuestEditorForm.CounterGridHandler.SpecialCounter(this.GetNewCounterName());
						}
					}
				}
			}

			// Token: 0x040009A0 RID: 2464
			private const int counterIdCol = 0;

			// Token: 0x040009A1 RID: 2465
			private const int counterTypeCol = 1;

			// Token: 0x040009A2 RID: 2466
			private const int customNameCol = 2;

			// Token: 0x040009A3 RID: 2467
			private const int qtyCol = 3;

			// Token: 0x040009A4 RID: 2468
			private const int internalCol = 4;

			// Token: 0x040009A5 RID: 2469
			private const int showCounterCol = 5;

			// Token: 0x040009A6 RID: 2470
			private const int removeOnAbandonCol = 6;

			// Token: 0x040009A7 RID: 2471
			private const int firstResCol = 7;

			// Token: 0x040009A8 RID: 2472
			private const string itemCounterType = "Item";

			// Token: 0x040009A9 RID: 2473
			private const string killCounterType = "Kill";

			// Token: 0x040009AA RID: 2474
			private const string specialCounterType = "Special";

			// Token: 0x040009AB RID: 2475
			private const string honorCounterType = "Honor";

			// Token: 0x040009AC RID: 2476
			private const string killAvatarCounterType = "KillAvatar";

			// Token: 0x040009AD RID: 2477
			private readonly DataGridView grid;

			// Token: 0x040009AE RID: 2478
			private readonly Tools.DBGameObjects.View itemView;

			// Token: 0x040009AF RID: 2479
			private readonly Tools.DBGameObjects.View respawnableResourceView;

			// Token: 0x040009B0 RID: 2480
			private readonly Tools.DBGameObjects.View killAvatarCounterObjectsView;

			// Token: 0x040009B1 RID: 2481
			private readonly MainForm.Context context;

			// Token: 0x040009B2 RID: 2482
			private readonly ContextMenuStrip contextMenuStrip;

			// Token: 0x040009B3 RID: 2483
			private readonly QuestEditorForm.QuestEditorFormData questEditorFormData;

			// Token: 0x040009B4 RID: 2484
			private readonly int defaultResColCnt = 1;

			// Token: 0x040009B5 RID: 2485
			private bool counterFieldPrototiped;

			// Token: 0x020000E6 RID: 230
			private class SpecialCounter
			{
				// Token: 0x06000BA9 RID: 2985 RVA: 0x00065BB0 File Offset: 0x00064BB0
				public SpecialCounter(string _counterId)
				{
					this.counterId = _counterId;
				}

				// Token: 0x06000BAA RID: 2986 RVA: 0x00065BC0 File Offset: 0x00064BC0
				public override string ToString()
				{
					if (!string.IsNullOrEmpty(this.counterId))
					{
						string _counterId = this.counterId.Replace('\\', '/');
						return Str.CutFilePathAndExtention(_counterId);
					}
					return string.Empty;
				}

				// Token: 0x17000238 RID: 568
				// (get) Token: 0x06000BAB RID: 2987 RVA: 0x00065BF8 File Offset: 0x00064BF8
				public string CounterId
				{
					get
					{
						return this.counterId;
					}
				}

				// Token: 0x040009B7 RID: 2487
				private readonly string counterId;
			}

			// Token: 0x020000E7 RID: 231
			// (Invoke) Token: 0x06000BAD RID: 2989
			public delegate void CounterGridHandlerEvent(object sender, EventArgs e);
		}

		// Token: 0x020000E8 RID: 232
		private class QuestMarker
		{
			// Token: 0x14000037 RID: 55
			// (add) Token: 0x06000BB0 RID: 2992 RVA: 0x00065C00 File Offset: 0x00064C00
			// (remove) Token: 0x06000BB1 RID: 2993 RVA: 0x00065C19 File Offset: 0x00064C19
			public event QuestEditorForm.QuestMarker.QuestMarkerEvent DataChanged;

			// Token: 0x06000BB2 RID: 2994 RVA: 0x00065C34 File Offset: 0x00064C34
			private bool PatchIsValid(System.Drawing.Point patch)
			{
				return patch.X >= this.minPatch.X && patch.Y >= this.minPatch.Y && patch.X <= this.maxPatch.X && patch.Y <= this.maxPatch.Y;
			}

			// Token: 0x06000BB3 RID: 2995 RVA: 0x00065C94 File Offset: 0x00064C94
			private ListViewItem GetViewItem(int index)
			{
				if (index > -1 && this.goalListView.Items.Count > index)
				{
					return this.goalListView.Items[index];
				}
				return null;
			}

			// Token: 0x17000239 RID: 569
			// (set) Token: 0x06000BB4 RID: 2996 RVA: 0x00065CC0 File Offset: 0x00064CC0
			private int CurrentGoalLocatorIndex
			{
				set
				{
					if (this.goalListView.Items.Count > 0)
					{
						if (value < 0)
						{
							value = 0;
						}
						else if (value >= this.goalListView.Items.Count)
						{
							value = this.goalListView.Items.Count - 1;
						}
					}
					ListViewItem item = this.GetViewItem(this.currentGoalLocatorIndex);
					if (item != null)
					{
						item.ImageKey = "quest_goal_locator";
					}
					this.currentGoalLocatorIndex = value;
					this.UpdateCurrentGoalLocator();
				}
			}

			// Token: 0x06000BB5 RID: 2997 RVA: 0x00065D38 File Offset: 0x00064D38
			private void UpdateCurrentGoalLocator()
			{
				ListViewItem item = this.GetViewItem(this.currentGoalLocatorIndex);
				if (item != null)
				{
					item.ImageKey = "quest_current_goal_locator";
					QuestClass.QuestLocator currentLocator = this.GetCurrentGoalLocator();
					if (!QuestClass.QuestLocator.IsNullOrEmpty(currentLocator))
					{
						item.Text = currentLocator.ToString();
						this.SetZone(currentLocator.Zone.ToString());
					}
				}
				this.editGoalLocatorButton.Enabled = (this.currentGoalLocatorIndex != -1);
				this.Repaint();
			}

			// Token: 0x06000BB6 RID: 2998 RVA: 0x00065DAC File Offset: 0x00064DAC
			private QuestClass.QuestLocator GetCurrentGoalLocator()
			{
				ListViewItem currentItem = this.GetViewItem(this.currentGoalLocatorIndex);
				if (currentItem == null)
				{
					return null;
				}
				return currentItem.Tag as QuestClass.QuestLocator;
			}

			// Token: 0x06000BB7 RID: 2999 RVA: 0x00065DD8 File Offset: 0x00064DD8
			private void SetZone(string zone)
			{
				if (!string.IsNullOrEmpty(zone))
				{
					using (IEnumerator enumerator = this.zoneComboBox.Items.GetEnumerator())
					{
						while (enumerator.MoveNext())
						{
							object obj = enumerator.Current;
							GameObjectClass _zone = (GameObjectClass)obj;
							if (_zone.GameObject == zone)
							{
								this.zoneComboBox.SelectedItem = _zone;
								break;
							}
						}
						return;
					}
				}
				this.zoneComboBox.SelectedIndex = -1;
			}

			// Token: 0x06000BB8 RID: 3000 RVA: 0x00065E60 File Offset: 0x00064E60
			private bool AddLocatorToItemList(QuestClass.QuestLocator locator)
			{
				if (!QuestClass.QuestLocator.IsNullOrEmpty(locator))
				{
					ListViewItem item = this.goalListView.Items.Add(locator.ToString());
					item.Tag = locator;
					item.ImageKey = ((this.goalListView.Items.Count != 0) ? "quest_goal_locator" : "quest_current_goal_locator");
					return true;
				}
				return false;
			}

			// Token: 0x1700023A RID: 570
			// (get) Token: 0x06000BB9 RID: 3001 RVA: 0x00065EBA File Offset: 0x00064EBA
			private bool Binded
			{
				get
				{
					return this.returnLocator != null;
				}
			}

			// Token: 0x06000BBA RID: 3002 RVA: 0x00065EC8 File Offset: 0x00064EC8
			private bool SetValue(Vec2 location)
			{
				if (!this.autoSetCheckBox.Checked)
				{
					System.Drawing.Point patch = QuestEditorForm.QuestMarker.LocationToPatch(location);
					if (this.PatchIsValid(patch))
					{
						string mapRegion = this.GetMapRegionByPatch(patch.X, patch.Y);
						DBID zone = QuestEditorForm.QuestMarker.GetZone(mapRegion, (int)location.X % 256, (int)location.Y % 256);
						if (!DBID.IsNullOrEmpty(zone))
						{
							QuestClass.QuestLocator goalLocator = this.GetCurrentGoalLocator();
							if (goalLocator != null)
							{
								goalLocator.Position = new Vec3(location, 0.0);
								goalLocator.Zone = zone;
								foreach (object obj in this.goalListView.Items)
								{
									ListViewItem item = (ListViewItem)obj;
									if (item.Tag == goalLocator)
									{
										item.Text = goalLocator.ToString();
										break;
									}
								}
								if (this.DataChanged != null)
								{
									this.DataChanged(this, null);
								}
								return true;
							}
						}
					}
				}
				return false;
			}

			// Token: 0x06000BBB RID: 3003 RVA: 0x00065FE8 File Offset: 0x00064FE8
			private static bool GetPatch(string mapRegionKey, out System.Drawing.Point patch)
			{
				Match match = QuestEditorForm.QuestMarker.patchRegex.Match(mapRegionKey);
				if (match.Success && match.Groups.Count == 5)
				{
					patch = default(System.Drawing.Point);
					patch.X = int.Parse(match.Groups[1].Value) + int.Parse(match.Groups[3].Value);
					patch.Y = int.Parse(match.Groups[2].Value) + int.Parse(match.Groups[4].Value);
					return true;
				}
				patch = System.Drawing.Point.Empty;
				return false;
			}

			// Token: 0x06000BBC RID: 3004 RVA: 0x00066094 File Offset: 0x00065094
			private static DBID GetZone(string mapRegion, int posInPatchX, int posInPatchY)
			{
				DBID mapRegionDBID = QuestEditorForm.QuestMarker.mainDb.GetDBIDByName(mapRegion);
				if (!DBID.IsNullOrEmpty(mapRegionDBID))
				{
					IObjMan objMan = QuestEditorForm.QuestMarker.mainDb.GetManipulator(mapRegionDBID);
					if (objMan != null)
					{
						int cnt;
						objMan.GetValue("tiles", out cnt);
						if (cnt > 0)
						{
							DBID zone;
							objMan.GetValue(string.Format("{0}.[{1}].[{2}]", "tiles", posInPatchY / 16, posInPatchX / 16), out zone);
							return zone;
						}
					}
				}
				return null;
			}

			// Token: 0x06000BBD RID: 3005 RVA: 0x00066104 File Offset: 0x00065104
			private string GetMapRegionByPatch(int patchX, int patchY)
			{
				int patchXR = patchX % 10;
				int patchYR = patchY % 10;
				return QuestEditorForm.QuestMarker.patchRegex.Replace(this.mapRegionElem, string.Format("/{0}_{1}/{2}_{3}_MapRegion.xdb", new object[]
				{
					(patchX - patchXR).ToString("000"),
					(patchY - patchYR).ToString("000"),
					patchXR,
					patchYR
				}));
			}

			// Token: 0x06000BBE RID: 3006 RVA: 0x00066176 File Offset: 0x00065176
			private void BeginTransaction()
			{
				this.transactLevel++;
			}

			// Token: 0x06000BBF RID: 3007 RVA: 0x00066186 File Offset: 0x00065186
			private void EndTransaction()
			{
				this.transactLevel--;
				this.Repaint();
			}

			// Token: 0x06000BC0 RID: 3008 RVA: 0x0006619C File Offset: 0x0006519C
			private void PaintLocator(QuestClass.QuestLocator locator, Image image, Graphics graphics)
			{
				System.Drawing.Point point;
				if (!QuestClass.QuestLocator.IsNullOrEmpty(locator) && this.PositionToPoint(locator.Position.Vec2, out point))
				{
					graphics.DrawImage(image, point.X - 5, point.Y - 5, 11, 11);
				}
			}

			// Token: 0x06000BC1 RID: 3009 RVA: 0x000661E8 File Offset: 0x000651E8
			private void PaintLocators(Graphics graphics)
			{
				if (this.Binded)
				{
					this.PaintLocator(this.returnLocator, this.questGoalLocatorImages.Images["quest_return_locator"], graphics);
					Image image = this.questGoalLocatorImages.Images["quest_goal_locator"];
					foreach (object obj in this.goalListView.Items)
					{
						ListViewItem item = (ListViewItem)obj;
						QuestClass.QuestLocator goalLocator = item.Tag as QuestClass.QuestLocator;
						if (!QuestClass.QuestLocator.IsNullOrEmpty(goalLocator) && item.Index != this.currentGoalLocatorIndex)
						{
							this.PaintLocator(goalLocator, image, graphics);
						}
					}
					this.PaintLocator(this.GetCurrentGoalLocator(), this.questGoalLocatorImages.Images["quest_current_goal_locator"], graphics);
				}
			}

			// Token: 0x06000BC2 RID: 3010 RVA: 0x000662D4 File Offset: 0x000652D4
			private void Repaint()
			{
				Graphics graphics = Graphics.FromHwnd(this.mapPanel.Handle);
				this.OnPaintMapPanel(this, new PaintEventArgs(graphics, this.mapPanel.ClientRectangle));
				graphics.Dispose();
			}

			// Token: 0x06000BC3 RID: 3011 RVA: 0x00066310 File Offset: 0x00065310
			private static System.Drawing.Point LocationToPatch(Vec2 posotion)
			{
				return new System.Drawing.Point((int)(posotion.X / 256.0), (int)(posotion.Y / 256.0));
			}

			// Token: 0x06000BC4 RID: 3012 RVA: 0x0006633C File Offset: 0x0006533C
			private Vec2 PointToPosition(System.Drawing.Point point)
			{
				point.Offset(-this.mapOffset.X, -this.mapOffset.Y);
				return new Vec2(((double)point.X / 128.0 + (double)this.minPatch.X) * 256.0, ((double)(this.maxPatch.Y + 1) - (double)point.Y / 128.0) * 256.0);
			}

			// Token: 0x06000BC5 RID: 3013 RVA: 0x000663C4 File Offset: 0x000653C4
			private bool PositionToPoint(Vec2 position, out System.Drawing.Point point)
			{
				System.Drawing.Point patch = QuestEditorForm.QuestMarker.LocationToPatch(position);
				if (this.PatchIsValid(patch))
				{
					Vec2 location = new Vec2(position.X - (double)(this.minPatch.X * 256), (double)((this.maxPatch.Y + 1) * 256) - position.Y);
					location *= 0.5;
					point = new System.Drawing.Point((int)location.X, (int)location.Y);
					point.Offset(this.mapOffset);
					return true;
				}
				point = System.Drawing.Point.Empty;
				return false;
			}

			// Token: 0x06000BC6 RID: 3014 RVA: 0x00066463 File Offset: 0x00065463
			private System.Drawing.Point PatchToPoint(int patchX, int patchY)
			{
				return new System.Drawing.Point((patchX - this.minPatch.X) * 128, (this.maxPatch.Y - patchY) * 128);
			}

			// Token: 0x06000BC7 RID: 3015 RVA: 0x00066490 File Offset: 0x00065490
			private void LoadZone(GameObjectClass zone)
			{
				this.mapRegionElem = string.Empty;
				this.mapOffset = System.Drawing.Point.Empty;
				if (this.zoneMap != null)
				{
					this.zoneMap.Dispose();
					this.zoneMap = null;
				}
				List<string> mapRegions = null;
				if (zone != null)
				{
					string cacheFilePath = QuestEditorForm.QuestMarker.cacheFodler + zone.GetFileRelativePath().Replace('\\', '_') + ".bin";
					try
					{
						FileStream stream = new FileStream(cacheFilePath, FileMode.Open);
						BinaryFormatter binFormatter = new BinaryFormatter();
						mapRegions = (binFormatter.Deserialize(stream) as List<string>);
						stream.Close();
					}
					catch (FileNotFoundException ex)
					{
						Console.WriteLine(ex.Message);
					}
					catch (IOException ex2)
					{
						Console.WriteLine(ex2.Message);
					}
					catch (SerializationException ex3)
					{
						Console.WriteLine(ex3.Message);
					}
					if (mapRegions != null && mapRegions.Count > 0)
					{
						QuestEditorForm.QuestMarker.GetPatch(mapRegions[0], out this.minPatch);
						this.maxPatch = this.minPatch;
						this.mapRegionElem = mapRegions[0];
						foreach (string mapRegion in mapRegions)
						{
							System.Drawing.Point patch;
							if (QuestEditorForm.QuestMarker.GetPatch(mapRegion, out patch))
							{
								if (this.minPatch.X > patch.X)
								{
									this.minPatch.X = patch.X;
								}
								if (this.minPatch.Y > patch.Y)
								{
									this.minPatch.Y = patch.Y;
								}
								if (this.maxPatch.X < patch.X)
								{
									this.maxPatch.X = patch.X;
								}
								if (this.maxPatch.Y < patch.Y)
								{
									this.maxPatch.Y = patch.Y;
								}
							}
						}
						this.zoneMap = new Bitmap((this.maxPatch.X - this.minPatch.X + 1) * 128, (this.maxPatch.Y - this.minPatch.Y + 1) * 128);
						Graphics graphics = Graphics.FromImage(this.zoneMap);
						if (graphics != null)
						{
							graphics.FillRectangle(QuestEditorForm.QuestMarker.blackBrush, 0, 0, this.zoneMap.Width, this.zoneMap.Height);
							for (int patchX = this.minPatch.X; patchX <= this.maxPatch.X; patchX++)
							{
								for (int patchY = this.minPatch.Y; patchY <= this.maxPatch.Y; patchY++)
								{
									string mapRegion2 = this.GetMapRegionByPatch(patchX, patchY);
									Bitmap patchMap;
									if (EditorImage.LoadTextureToARGBBitmap(QuestEditorForm.QuestMarker.mapRegionRegex.Replace(mapRegion2, "minimap.(Texture).xdb"), 128, 128, 128, 128, out patchMap) && patchMap != null)
									{
										System.Drawing.Point point = this.PatchToPoint(patchX, patchY);
										graphics.DrawImage(patchMap, point);
										patchMap.Dispose();
									}
								}
							}
							graphics.Dispose();
						}
					}
				}
				this.Repaint();
			}

			// Token: 0x06000BC8 RID: 3016 RVA: 0x000667CC File Offset: 0x000657CC
			private void OnAutoSetCheckedChanged(object sender, EventArgs e)
			{
				this.zoneComboBox.Enabled = !this.autoSetCheckBox.Checked;
				this.addGoalLocatorButton.Enabled = !this.autoSetCheckBox.Checked;
				this.removeGoalLocatorButton.Enabled = !this.autoSetCheckBox.Checked;
				this.editGoalLocatorButton.Enabled = (!this.autoSetCheckBox.Checked && this.currentGoalLocatorIndex != -1);
			}

			// Token: 0x06000BC9 RID: 3017 RVA: 0x0006684B File Offset: 0x0006584B
			private void OnZoneSelectedIndexChanged(object sender, EventArgs e)
			{
				this.LoadZone(this.zoneComboBox.SelectedItem as GameObjectClass);
			}

			// Token: 0x06000BCA RID: 3018 RVA: 0x00066864 File Offset: 0x00065864
			private void OnGoalListSelectedIndexChanged(object sender, EventArgs e)
			{
				if (this.goalListView.SelectedIndices.Count > 0)
				{
					this.CurrentGoalLocatorIndex = this.goalListView.SelectedIndices[0];
					return;
				}
				if (this.goalListView.Items.Count > 0)
				{
					this.CurrentGoalLocatorIndex = 0;
				}
			}

			// Token: 0x06000BCB RID: 3019 RVA: 0x000668B6 File Offset: 0x000658B6
			private void OnMouseClick(object sender, MouseEventArgs e)
			{
				if (e.Button == MouseButtons.Left && this.Binded && this.SetValue(this.PointToPosition(e.Location)))
				{
					this.Repaint();
				}
			}

			// Token: 0x06000BCC RID: 3020 RVA: 0x000668E8 File Offset: 0x000658E8
			private void OnPaintMapPanel(object sender, PaintEventArgs e)
			{
				if (this.transactLevel == 0 && this.bufferGraphics != null && this.bufferBitmap != null)
				{
					this.bufferGraphics.FillRectangle(QuestEditorForm.QuestMarker.greyBrush, e.ClipRectangle);
					if (this.zoneMap != null)
					{
						this.bufferGraphics.DrawImage(this.zoneMap, this.mapOffset);
						this.PaintLocators(this.bufferGraphics);
					}
					e.Graphics.DrawImage(this.bufferBitmap, 0, 0);
				}
			}

			// Token: 0x06000BCD RID: 3021 RVA: 0x00066961 File Offset: 0x00065961
			private void OnMouseDown(object sender, MouseEventArgs e)
			{
				if (e.Button == MouseButtons.Right)
				{
					this.mapOffsetStart = e.Location;
					this.mapPanel.Cursor = Cursors.Hand;
				}
			}

			// Token: 0x06000BCE RID: 3022 RVA: 0x0006698C File Offset: 0x0006598C
			private void OnMouseUp(object sender, MouseEventArgs e)
			{
				if (e.Button == MouseButtons.Right)
				{
					this.mapPanel.Cursor = Cursors.Default;
				}
			}

			// Token: 0x06000BCF RID: 3023 RVA: 0x000669AC File Offset: 0x000659AC
			private void OnMouseMove(object sender, MouseEventArgs e)
			{
				if (e.Button == MouseButtons.Right)
				{
					this.mapOffset.Offset(e.Location.X - this.mapOffsetStart.X, e.Location.Y - this.mapOffsetStart.Y);
					this.mapOffsetStart = e.Location;
					this.Repaint();
					return;
				}
				if (e.Button == MouseButtons.Left && this.Binded && this.SetValue(this.PointToPosition(e.Location)))
				{
					this.Repaint();
				}
			}

			// Token: 0x06000BD0 RID: 3024 RVA: 0x00066A48 File Offset: 0x00065A48
			private void OnResize(object sender, EventArgs e)
			{
				if (this.Binded && this.mapPanel.Width > 0 && this.mapPanel.Height > 0)
				{
					if (this.bufferBitmap != null && this.bufferGraphics != null)
					{
						this.bufferGraphics.Dispose();
						this.bufferGraphics = null;
						this.bufferBitmap.Dispose();
						this.bufferBitmap = null;
					}
					this.bufferBitmap = new Bitmap(this.mapPanel.Width, this.mapPanel.Height);
					this.bufferGraphics = Graphics.FromImage(this.bufferBitmap);
				}
			}

			// Token: 0x06000BD1 RID: 3025 RVA: 0x00066AE2 File Offset: 0x00065AE2
			private void OnGoalListViewKeyDown(object sender, KeyEventArgs e)
			{
				if (e.KeyCode == Keys.Delete)
				{
					this.OnRemoveGoalLocatorClick(sender, e);
				}
			}

			// Token: 0x06000BD2 RID: 3026 RVA: 0x00066AF8 File Offset: 0x00065AF8
			private void OnAddGoalLocatorClick(object sender, EventArgs e)
			{
				if (this.Binded && this.returnLocator != null)
				{
					System.Drawing.Point oldOffset = this.mapOffset;
					GameObjectClass selectedZone = this.zoneComboBox.SelectedItem as GameObjectClass;
					System.Drawing.Point rightUpperPoint = new System.Drawing.Point(this.zoneMap.Width + this.mapOffset.X, this.zoneMap.Height + this.mapOffset.Y);
					rightUpperPoint.X = Math.Min(rightUpperPoint.X, this.mapPanel.Width);
					rightUpperPoint.Y = Math.Min(rightUpperPoint.Y, this.mapPanel.Height);
					System.Drawing.Point imageCenter = new System.Drawing.Point(rightUpperPoint.X / 2, rightUpperPoint.Y / 2);
					if (this.mapOffset.X > 0)
					{
						imageCenter.X += this.mapOffset.X / 2;
					}
					if (this.mapOffset.Y > 0)
					{
						imageCenter.Y += this.mapOffset.Y / 2;
					}
					Vec2 location = this.PointToPosition(imageCenter);
					Vec3 position = new Vec3(location, 0.0);
					System.Drawing.Point patch = QuestEditorForm.QuestMarker.LocationToPatch(location);
					string mapRegion = this.GetMapRegionByPatch(patch.X, patch.Y);
					DBID zone = QuestEditorForm.QuestMarker.GetZone(mapRegion, (int)location.X % 256, (int)location.Y % 256);
					QuestClass.QuestLocator locator = new QuestClass.QuestLocator(zone, position);
					this.BeginTransaction();
					if (this.AddLocatorToItemList(locator))
					{
						this.goalListView.Items[this.goalListView.Items.Count - 1].Selected = true;
						if (selectedZone != null)
						{
							this.SetZone(selectedZone.GameObject);
							this.mapOffset = oldOffset;
						}
					}
					this.EndTransaction();
				}
			}

			// Token: 0x06000BD3 RID: 3027 RVA: 0x00066CD0 File Offset: 0x00065CD0
			private void OnRemoveGoalLocatorClick(object sender, EventArgs e)
			{
				if (this.Binded && this.currentGoalLocatorIndex > -1)
				{
					int index = this.currentGoalLocatorIndex;
					this.BeginTransaction();
					this.goalListView.Items.RemoveAt(this.currentGoalLocatorIndex);
					this.CurrentGoalLocatorIndex = ((index > 0) ? (index - 1) : 0);
					this.EndTransaction();
				}
			}

			// Token: 0x06000BD4 RID: 3028 RVA: 0x00066D28 File Offset: 0x00065D28
			private void OnEditGoalLocatorClick(object sender, EventArgs e)
			{
				QuestClass.QuestLocator currentLocator = this.GetCurrentGoalLocator();
				if (currentLocator != null)
				{
					SetLocatorsForm dialog = new SetLocatorsForm(currentLocator);
					if (dialog.ShowDialog() == DialogResult.OK)
					{
						dialog.GetValue(currentLocator);
						this.UpdateCurrentGoalLocator();
					}
				}
			}

			// Token: 0x06000BD5 RID: 3029 RVA: 0x00066D5C File Offset: 0x00065D5C
			private void OnEditReturnLocatorClick(object sender, EventArgs e)
			{
				if (this.returnLocator != null)
				{
					SetLocatorsForm dialog = new SetLocatorsForm(this.returnLocatorAutoSet, this.returnLocator);
					if (dialog.ShowDialog() == DialogResult.OK)
					{
						dialog.GetValue(out this.returnLocatorAutoSet, this.returnLocator);
						this.Repaint();
					}
				}
			}

			// Token: 0x06000BD6 RID: 3030 RVA: 0x00066DA4 File Offset: 0x00065DA4
			public QuestMarker(CheckBox _autoSetCheckBox, ListView _goalListView, ComboBox _zoneComboBox, Panel _mapPanel, Button _addGoalLocatorButton, Button _removeGoalLocatorButton, Button _editGoalLocatorButton, Button _editReturnLocatorButton)
			{
				this.autoSetCheckBox = _autoSetCheckBox;
				this.goalListView = _goalListView;
				this.zoneComboBox = _zoneComboBox;
				this.mapPanel = _mapPanel;
				this.addGoalLocatorButton = _addGoalLocatorButton;
				this.removeGoalLocatorButton = _removeGoalLocatorButton;
				this.editGoalLocatorButton = _editGoalLocatorButton;
				this.editReturnLocatorButton = _editReturnLocatorButton;
				this.questGoalLocatorImages = new ImageList();
			}

			// Token: 0x06000BD7 RID: 3031 RVA: 0x00066E10 File Offset: 0x00065E10
			public void Init()
			{
				GeneralView zones = new GeneralView(string.Empty);
				DBMethods.LoadObjects("gameMechanics.map.zone.ZoneResource", zones, false);
				foreach (GameObjectClass zone in zones)
				{
					this.zoneComboBox.Items.Add(zone);
				}
				this.questGoalLocatorImages.Images.Add("quest_goal_locator", Images.quest_goal_locator);
				this.questGoalLocatorImages.Images.Add("quest_current_goal_locator", Images.quest_current_goal_locator);
				this.questGoalLocatorImages.Images.Add("quest_return_locator", Images.quest_return_locator);
				this.questGoalLocatorImages.TransparentColor = Color.White;
				this.goalListView.SelectedIndexChanged += this.OnGoalListSelectedIndexChanged;
				this.goalListView.DoubleClick += this.OnEditGoalLocatorClick;
				this.goalListView.KeyDown += this.OnGoalListViewKeyDown;
				this.goalListView.SmallImageList = this.questGoalLocatorImages;
				this.autoSetCheckBox.CheckedChanged += this.OnAutoSetCheckedChanged;
				this.zoneComboBox.SelectedIndexChanged += this.OnZoneSelectedIndexChanged;
				this.addGoalLocatorButton.Click += this.OnAddGoalLocatorClick;
				this.removeGoalLocatorButton.Click += this.OnRemoveGoalLocatorClick;
				this.editGoalLocatorButton.Click += this.OnEditGoalLocatorClick;
				this.editReturnLocatorButton.Click += this.OnEditReturnLocatorClick;
				this.mapPanel.Paint += this.OnPaintMapPanel;
				this.mapPanel.Resize += this.OnResize;
				this.mapPanel.MouseClick += this.OnMouseClick;
				this.mapPanel.MouseDown += this.OnMouseDown;
				this.mapPanel.MouseUp += this.OnMouseUp;
				this.mapPanel.MouseMove += this.OnMouseMove;
			}

			// Token: 0x06000BD8 RID: 3032 RVA: 0x00067044 File Offset: 0x00066044
			public void Load(bool autoSet, QuestClass.QuestLocator goalLocator, IEnumerable<QuestClass.QuestLocator> additionalLocators, QuestClass.QuestLocator _returnLocator, bool _returnLocatorAutoSet, string _questZone)
			{
				this.currentGoalLocatorIndex = -1;
				this.goalListView.BeginUpdate();
				this.goalListView.Items.Clear();
				this.AddLocatorToItemList(goalLocator);
				if (additionalLocators != null)
				{
					foreach (QuestClass.QuestLocator locator in additionalLocators)
					{
						this.AddLocatorToItemList(locator);
					}
				}
				this.autoSetCheckBox.Checked = autoSet;
				this.goalListView.EndUpdate();
				this.returnLocator = _returnLocator;
				this.returnLocatorAutoSet = _returnLocatorAutoSet;
				if (this.goalListView.Items.Count > 0)
				{
					this.CurrentGoalLocatorIndex = 0;
				}
				else
				{
					this.SetZone(_questZone);
				}
				this.Repaint();
				this.OnResize(this, null);
			}

			// Token: 0x1700023B RID: 571
			// (get) Token: 0x06000BD9 RID: 3033 RVA: 0x00067114 File Offset: 0x00066114
			public bool GoalAutoSet
			{
				get
				{
					return this.autoSetCheckBox.Checked;
				}
			}

			// Token: 0x1700023C RID: 572
			// (get) Token: 0x06000BDA RID: 3034 RVA: 0x00067121 File Offset: 0x00066121
			public bool ReturnAutoSet
			{
				get
				{
					return this.returnLocatorAutoSet;
				}
			}

			// Token: 0x06000BDB RID: 3035 RVA: 0x0006712C File Offset: 0x0006612C
			public void GetGoalLocators(out QuestClass.QuestLocator[] locators)
			{
				int cnt = this.goalListView.Items.Count;
				locators = new QuestClass.QuestLocator[cnt];
				for (int index = 0; index < cnt; index++)
				{
					locators[index] = ((this.goalListView.Items[index].Tag as QuestClass.QuestLocator) ?? QuestClass.QuestLocator.Empty);
				}
			}

			// Token: 0x1700023D RID: 573
			// (get) Token: 0x06000BDC RID: 3036 RVA: 0x00067186 File Offset: 0x00066186
			public QuestClass.QuestLocator ReturnLocator
			{
				get
				{
					return this.returnLocator;
				}
			}

			// Token: 0x040009B8 RID: 2488
			private const string type = "gameMechanics.map.zone.ZoneResource";

			// Token: 0x040009B9 RID: 2489
			private const string cacheFileExt = ".bin";

			// Token: 0x040009BA RID: 2490
			private const int patchImageSize = 128;

			// Token: 0x040009BB RID: 2491
			private const int patchSize = 256;

			// Token: 0x040009BC RID: 2492
			private const int markHalfSize = 5;

			// Token: 0x040009BD RID: 2493
			private const int zoneTileSize = 16;

			// Token: 0x040009BE RID: 2494
			private const string minimapSuff = "minimap.(Texture).xdb";

			// Token: 0x040009BF RID: 2495
			private const string goalLocatorImageKey = "quest_goal_locator";

			// Token: 0x040009C0 RID: 2496
			private const string currentGoalLocatorImageKey = "quest_current_goal_locator";

			// Token: 0x040009C1 RID: 2497
			private const string returnLocatorImageKey = "quest_return_locator";

			// Token: 0x040009C2 RID: 2498
			private static readonly string cacheFodler = EditorEnvironment.CommonEditorFolder.Replace('/', '\\') + "CommonFolder\\Caches\\Zones\\";

			// Token: 0x040009C3 RID: 2499
			private static readonly Regex patchRegex = new Regex("/([0-9]{3})_([0-9]{3})/([0-9])_([0-9])_MapRegion.xdb$");

			// Token: 0x040009C4 RID: 2500
			private static readonly Regex mapRegionRegex = new Regex("MapRegion.xdb$");

			// Token: 0x040009C5 RID: 2501
			private static readonly SolidBrush greyBrush = new SolidBrush(Color.Gray);

			// Token: 0x040009C6 RID: 2502
			private static readonly SolidBrush blackBrush = new SolidBrush(Color.Black);

			// Token: 0x040009C7 RID: 2503
			private static readonly IDatabase mainDb = IDatabase.GetMainDatabase();

			// Token: 0x040009C8 RID: 2504
			private readonly ImageList questGoalLocatorImages;

			// Token: 0x040009C9 RID: 2505
			private readonly ListView goalListView;

			// Token: 0x040009CA RID: 2506
			private readonly ComboBox zoneComboBox;

			// Token: 0x040009CB RID: 2507
			private readonly Panel mapPanel;

			// Token: 0x040009CC RID: 2508
			private readonly CheckBox autoSetCheckBox;

			// Token: 0x040009CD RID: 2509
			private readonly Button addGoalLocatorButton;

			// Token: 0x040009CE RID: 2510
			private readonly Button removeGoalLocatorButton;

			// Token: 0x040009CF RID: 2511
			private readonly Button editGoalLocatorButton;

			// Token: 0x040009D0 RID: 2512
			private readonly Button editReturnLocatorButton;

			// Token: 0x040009D1 RID: 2513
			private int transactLevel;

			// Token: 0x040009D2 RID: 2514
			private int currentGoalLocatorIndex = -1;

			// Token: 0x040009D3 RID: 2515
			private QuestClass.QuestLocator returnLocator;

			// Token: 0x040009D4 RID: 2516
			private bool returnLocatorAutoSet = true;

			// Token: 0x040009D5 RID: 2517
			private Bitmap zoneMap;

			// Token: 0x040009D6 RID: 2518
			private System.Drawing.Point minPatch;

			// Token: 0x040009D7 RID: 2519
			private System.Drawing.Point maxPatch;

			// Token: 0x040009D8 RID: 2520
			private string mapRegionElem;

			// Token: 0x040009D9 RID: 2521
			private System.Drawing.Point mapOffset;

			// Token: 0x040009DA RID: 2522
			private System.Drawing.Point mapOffsetStart;

			// Token: 0x040009DB RID: 2523
			private Bitmap bufferBitmap;

			// Token: 0x040009DC RID: 2524
			private Graphics bufferGraphics;

			// Token: 0x020000E9 RID: 233
			// (Invoke) Token: 0x06000BDF RID: 3039
			public delegate void QuestMarkerEvent(object sender, EventArgs e);
		}

		// Token: 0x020000EA RID: 234
		private enum ErrorTypes
		{
			// Token: 0x040009DF RID: 2527
			MANDATORY_FIELD,
			// Token: 0x040009E0 RID: 2528
			INT_FORMAT,
			// Token: 0x040009E1 RID: 2529
			PROBABILITY_FORMAT,
			// Token: 0x040009E2 RID: 2530
			FLOAT_FORMAT
		}

		// Token: 0x020000EB RID: 235
		private struct RewItemColStruct
		{
			// Token: 0x06000BE2 RID: 3042 RVA: 0x00067200 File Offset: 0x00066200
			public RewItemColStruct(int _qtyCol, int _isHiddenCol)
			{
				this.qtyCol = _qtyCol;
				this.isHiddenCol = _isHiddenCol;
			}

			// Token: 0x040009E3 RID: 2531
			public readonly int qtyCol;

			// Token: 0x040009E4 RID: 2532
			public readonly int isHiddenCol;
		}

		// Token: 0x020000EC RID: 236
		private class QuestEditorFormData
		{
			// Token: 0x06000BE3 RID: 3043 RVA: 0x00067210 File Offset: 0x00066210
			private string GetNewQuestDir(bool fullPath)
			{
				string path = QuestEditor.QuestFolder + this.newQuestSubPath + this.newQuestFileName + "/";
				if (fullPath)
				{
					return EditorEnvironment.DataFolder + path;
				}
				return path;
			}

			// Token: 0x06000BE4 RID: 3044 RVA: 0x00067249 File Offset: 0x00066249
			private string GetNewQuestFilePath()
			{
				return this.GetNewQuestDir(false) + this.newQuestFileName + ".xdb";
			}

			// Token: 0x06000BE5 RID: 3045 RVA: 0x00067264 File Offset: 0x00066264
			private bool CheckNewQuestFolder()
			{
				if (string.IsNullOrEmpty(this.newQuestFileName))
				{
					return false;
				}
				string newQuestDir = this.GetNewQuestDir(true);
				return !Directory.Exists(newQuestDir);
			}

			// Token: 0x06000BE6 RID: 3046 RVA: 0x00067291 File Offset: 0x00066291
			public QuestEditorFormData(QuestEnvironment _questEnvironment)
			{
				this.questEnvironment = _questEnvironment;
			}

			// Token: 0x06000BE7 RID: 3047 RVA: 0x000672B8 File Offset: 0x000662B8
			public static bool IsInteger(object o)
			{
				bool result;
				try
				{
					Convert.ToInt32(o);
					result = true;
				}
				catch (FormatException)
				{
					result = false;
				}
				return result;
			}

			// Token: 0x06000BE8 RID: 3048 RVA: 0x000672E8 File Offset: 0x000662E8
			public static bool IsFloat(object o)
			{
				bool result;
				try
				{
					Convert.ToSingle(o);
					result = true;
				}
				catch (FormatException)
				{
					result = false;
				}
				return result;
			}

			// Token: 0x06000BE9 RID: 3049 RVA: 0x00067318 File Offset: 0x00066318
			public static int ConvertToInteger(object o)
			{
				return QuestEditorForm.QuestEditorFormData.ConvertToInteger(o, 0);
			}

			// Token: 0x06000BEA RID: 3050 RVA: 0x00067324 File Offset: 0x00066324
			public static int ConvertToInteger(object o, int deafaultVal)
			{
				int result;
				try
				{
					result = Convert.ToInt32(o);
				}
				catch (FormatException)
				{
					result = deafaultVal;
				}
				return result;
			}

			// Token: 0x06000BEB RID: 3051 RVA: 0x00067350 File Offset: 0x00066350
			public static float ConvertToFloat(object o)
			{
				float result;
				try
				{
					result = Convert.ToSingle(o);
				}
				catch (FormatException)
				{
					result = 0f;
				}
				return result;
			}

			// Token: 0x06000BEC RID: 3052 RVA: 0x00067380 File Offset: 0x00066380
			public static bool ConvertToBool(object o)
			{
				bool result;
				try
				{
					result = Convert.ToBoolean(o);
				}
				catch (FormatException)
				{
					result = false;
				}
				return result;
			}

			// Token: 0x1700023E RID: 574
			// (get) Token: 0x06000BED RID: 3053 RVA: 0x000673AC File Offset: 0x000663AC
			// (set) Token: 0x06000BEE RID: 3054 RVA: 0x000673B4 File Offset: 0x000663B4
			public QuestClass CurrentQuest
			{
				get
				{
					return this.currentQuest;
				}
				set
				{
					this.currentQuest = value;
					this.isNew = false;
				}
			}

			// Token: 0x1700023F RID: 575
			// (get) Token: 0x06000BEF RID: 3055 RVA: 0x000673C4 File Offset: 0x000663C4
			public bool IsNew
			{
				get
				{
					return this.isNew;
				}
			}

			// Token: 0x06000BF0 RID: 3056 RVA: 0x000673CC File Offset: 0x000663CC
			public string GetCurrentQuestFolder()
			{
				if (this.IsNew)
				{
					return this.GetNewQuestDir(false);
				}
				if (this.currentQuest != null)
				{
					return DBMethods.GetContainingFolder(this.currentQuest.GameObject);
				}
				return null;
			}

			// Token: 0x06000BF1 RID: 3057 RVA: 0x000673F8 File Offset: 0x000663F8
			public void SaveDb()
			{
				if (QuestEditorForm.QuestEditorFormData.mainDb != null)
				{
					QuestEditorForm.QuestEditorFormData.mainDb.SaveChanges();
				}
			}

			// Token: 0x06000BF2 RID: 3058 RVA: 0x0006740B File Offset: 0x0006640B
			public bool SetNewQuestFileName(string _newQuestFileName, string _newQuestSubPath)
			{
				this.newQuestFileName = _newQuestFileName;
				this.newQuestSubPath = _newQuestSubPath;
				this.isNew = this.CheckNewQuestFolder();
				return this.isNew;
			}

			// Token: 0x06000BF3 RID: 3059 RVA: 0x0006742D File Offset: 0x0006642D
			public bool CheckNewQuestBeforeCreate(out DBID zone)
			{
				return QuestEditor.CheckNewQuestBeforeCreate(IDatabase.GetMainDatabase(), IDatabase.CreateDBIDByName(this.GetNewQuestFilePath()), out zone);
			}

			// Token: 0x06000BF4 RID: 3060 RVA: 0x00067448 File Offset: 0x00066448
			public bool AddNewQuest(DBID zone)
			{
				string newQuestDir = this.GetNewQuestDir(true);
				if (!this.CheckNewQuestFolder())
				{
					return false;
				}
				Directory.CreateDirectory(newQuestDir);
				if (QuestEditorForm.QuestEditorFormData.mainDb == null)
				{
					return false;
				}
				DBID _questDBID = IDatabase.CreateDBIDByName(this.GetNewQuestFilePath());
				QuestEditor.CreateNewQuest(QuestEditorForm.QuestEditorFormData.mainDb, _questDBID, zone);
				this.CurrentQuest = (this.questEnvironment.FindGameObject(this.questEnvironment.Quests, _questDBID.ToString()) as QuestClass);
				return this.CurrentQuest != null;
			}

			// Token: 0x06000BF5 RID: 3061 RVA: 0x000674C8 File Offset: 0x000664C8
			public bool DeleteQuest()
			{
				if (this.CurrentQuest == null)
				{
					return false;
				}
				bool result = this.currentQuest.DeleteObjectFromDatabase();
				this.CurrentQuest = null;
				this.SaveDb();
				return result;
			}

			// Token: 0x06000BF6 RID: 3062 RVA: 0x000674FC File Offset: 0x000664FC
			public bool CheckBeforDelete(out List<string> errorStrings)
			{
				errorStrings = new List<string>(2);
				if (this.currentQuest == null)
				{
					return false;
				}
				bool result = true;
				foreach (GameObjectClass gameObjectClass in this.questEnvironment.Quests)
				{
					QuestClass quest = (QuestClass)gameObjectClass;
					List<string> prevQuests;
					List<string> notStartedQuest;
					string characterClass;
					quest.GetPrerequisits(out prevQuests, out notStartedQuest, out characterClass);
					if (prevQuests.Contains(this.currentQuest.GameObject))
					{
						errorStrings.Add(string.Concat(new object[]
						{
							Strings.QUEST_EDITOR_QUESTLINE_MESSAGE,
							" (",
							quest.GameName,
							')'
						}));
						result = false;
					}
					if (notStartedQuest.Contains(this.currentQuest.GameObject))
					{
						errorStrings.Add(string.Concat(new object[]
						{
							Strings.QUEST_EDITOR_RESTRICTEDQUEST_MESSAGE,
							" (",
							quest.GameName,
							')'
						}));
						result = false;
					}
				}
				return result;
			}

			// Token: 0x06000BF7 RID: 3063 RVA: 0x0006761C File Offset: 0x0006661C
			public void LoadCharacterList(ComboBox charcaterComboBox)
			{
				charcaterComboBox.Items.Add(string.Empty);
				GeneralView view = new GeneralView("QuestCharacterClassesView");
				DBMethods.LoadObjects("gameMechanics.world.avatar.CharacterClass", view, false);
				foreach (GameObjectClass gameObj in view)
				{
					if (gameObj != null && gameObj.GameObject.StartsWith("Mechanics/Classes"))
					{
						charcaterComboBox.Items.Add(gameObj);
					}
				}
			}

			// Token: 0x06000BF8 RID: 3064 RVA: 0x000676A8 File Offset: 0x000666A8
			public void LoadQuestTypeList(ComboBox comboBox)
			{
				IFieldDesc[] fields = QuestEditorForm.QuestEditorFormData.mainDb.GetTypeSubFields("gameMechanics.constructor.schemes.quest.QuestResource");
				foreach (IFieldDesc field in fields)
				{
					if (field.FieldName == "type" && field.TypeType == TypeType.Enum)
					{
						comboBox.Items.AddRange(field.EnumValues);
						return;
					}
				}
			}

			// Token: 0x06000BF9 RID: 3065 RVA: 0x00067708 File Offset: 0x00066708
			public void SaveQuestGiver(string questGiverDBID)
			{
				if (this.currentQuest != null)
				{
					foreach (GameObjectClass gameObjectClass in this.questEnvironment.QuestGivers)
					{
						QuestGiverClass questGiver = (QuestGiverClass)gameObjectClass;
						if (questGiver != null)
						{
							if (questGiver.GameObject != questGiverDBID)
							{
								questGiver.RemoveQuest(this.currentQuest.GameObject);
							}
							if (questGiver.GameObject == questGiverDBID)
							{
								questGiver.AddIfNotFound(this.currentQuest.GameObject);
							}
						}
					}
				}
			}

			// Token: 0x06000BFA RID: 3066 RVA: 0x000677A4 File Offset: 0x000667A4
			public bool SaveQuestAs()
			{
				if (QuestEditorForm.QuestEditorFormData.mainDb != null && this.currentQuest != null)
				{
					QuestClass sourceQuest = this.currentQuest;
					if (this.AddNewQuest(QuestEditorForm.QuestEditorFormData.mainDb.GetDBIDByName(this.currentQuest.Zone)))
					{
						QuestEditorForm.QuestEditorFormData.mainDb.CopyObject(this.currentQuest.QuestMan.DBID, sourceQuest.QuestMan.DBID);
						DbCommonMethods.CopyRefFieldByValue(this.currentQuest.QuestMan, sourceQuest.QuestMan);
						this.currentQuest.ClearCountersWithoutAnyControl();
						return true;
					}
				}
				return false;
			}

			// Token: 0x06000BFB RID: 3067 RVA: 0x0006782E File Offset: 0x0006682E
			public int GetResourceId()
			{
				if (this.currentQuest == null)
				{
					return 0;
				}
				return this.currentQuest.GetResourceId(QuestEditorForm.QuestEditorFormData.mainDb);
			}

			// Token: 0x040009E5 RID: 2533
			private QuestClass currentQuest;

			// Token: 0x040009E6 RID: 2534
			private string newQuestSubPath = string.Empty;

			// Token: 0x040009E7 RID: 2535
			private string newQuestFileName = string.Empty;

			// Token: 0x040009E8 RID: 2536
			private bool isNew;

			// Token: 0x040009E9 RID: 2537
			private readonly QuestEnvironment questEnvironment;

			// Token: 0x040009EA RID: 2538
			private static readonly IDatabase mainDb = IDatabase.GetMainDatabase();
		}

		// Token: 0x020000ED RID: 237
		private class QuestLine
		{
			// Token: 0x06000BFD RID: 3069 RVA: 0x00067856 File Offset: 0x00066856
			private void QuestLoaded(GameObjectClass gameObj, int count)
			{
				this.progressForm.ProgressCount = count;
				this.progressForm.Progress++;
				this.questEditorForm.Update();
			}

			// Token: 0x06000BFE RID: 3070 RVA: 0x00067884 File Offset: 0x00066884
			private void Load()
			{
				if (this.loaded)
				{
					return;
				}
				this.progressForm.Create("Loading quests...");
				this.progressForm.ProgressCount = 1;
				this.progressForm.Progress = 0;
				this.progressForm.AddString("Loading quests...");
				this.questEnvironment.LoadAllQuestsWithoutOtherObjects(new DBMethods.ObjectLoadedInView(this.QuestLoaded));
				this.progressForm.Delete();
				this.loaded = true;
			}

			// Token: 0x06000BFF RID: 3071 RVA: 0x000678FB File Offset: 0x000668FB
			private void OnQuestLineFormDisposed(object sender, EventArgs e)
			{
				this.questLineForm = null;
			}

			// Token: 0x06000C00 RID: 3072 RVA: 0x00067904 File Offset: 0x00066904
			public QuestLine(QuestEnvironment _questEnvironment, Form _questEditorForm)
			{
				this.questEnvironment = _questEnvironment;
				this.questEditorForm = _questEditorForm;
			}

			// Token: 0x06000C01 RID: 3073 RVA: 0x00067928 File Offset: 0x00066928
			public void Show(QuestClass quest)
			{
				this.Load();
				if (this.questLineForm == null)
				{
					this.questLineForm = new QuestLineForm(this.questEnvironment);
					this.questLineForm.Show(this.questEditorForm);
					this.questLineForm.Disposed += this.OnQuestLineFormDisposed;
				}
				this.questLineForm.LoadLine(quest);
			}

			// Token: 0x040009EB RID: 2539
			private bool loaded;

			// Token: 0x040009EC RID: 2540
			private readonly QuestEnvironment questEnvironment;

			// Token: 0x040009ED RID: 2541
			private readonly ProgressForm progressForm = new ProgressForm();

			// Token: 0x040009EE RID: 2542
			private readonly Form questEditorForm;

			// Token: 0x040009EF RID: 2543
			private QuestLineForm questLineForm;
		}
	}
}
