using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Db;
using MapEditor.Resources.Strings;
using Tools.DBGameObjects;
using Tools.DBGameObjects.GameObjects;
using Tools.Excel;

namespace MapEditor.Forms.Quests
{
	// Token: 0x02000189 RID: 393
	public class ImportExport
	{
		// Token: 0x14000074 RID: 116
		// (add) Token: 0x060012CA RID: 4810 RVA: 0x00088CD9 File Offset: 0x00087CD9
		// (remove) Token: 0x060012CB RID: 4811 RVA: 0x00088CF2 File Offset: 0x00087CF2
		public event ImportExport.ImportEvent QuestTextsImported;

		// Token: 0x060012CC RID: 4812 RVA: 0x00088D0C File Offset: 0x00087D0C
		private string GetObjectGameName(string dbid, View view)
		{
			GameObjectClass gameObject = this.questEnviroment.FindGameObject(view, dbid);
			if (gameObject != null)
			{
				return gameObject.GameName;
			}
			return dbid;
		}

		// Token: 0x060012CD RID: 4813 RVA: 0x00088D34 File Offset: 0x00087D34
		private static StringBuilder RemoveLastNewLine(StringBuilder stringBuilder)
		{
			int len = stringBuilder.Length;
			if (len > 0)
			{
				stringBuilder.Replace("\n", string.Empty, len - 1, 1);
			}
			return stringBuilder;
		}

		// Token: 0x060012CE RID: 4814 RVA: 0x00088D64 File Offset: 0x00087D64
		private string GetQuestItems(QuestClass quest)
		{
			if (quest != null && quest.StartImpactsView != null && this.questEnviroment.Items != null)
			{
				StringBuilder result = new StringBuilder();
				int count;
				quest.StartImpactsView.GetValue("startImpacts", out count);
				for (int index = 0; index < count; index++)
				{
					string fieldNameArr = string.Format("{0}.[{1}]", "startImpacts", index);
					foreach (string giveQuestItem in ImportExport.questItemTypes)
					{
						if (quest.StartImpactsView.IsStructPtrInstanceCompatible(fieldNameArr, giveQuestItem))
						{
							string item;
							quest.StartImpactsView.GetValue(fieldNameArr + ".item", out item);
							int _count = 1;
							if (giveQuestItem == "gameMechanics.elements.impacts.ImpactGiveItem")
							{
								quest.StartImpactsView.GetValue(fieldNameArr + ".count", out _count);
							}
							result.AppendFormat("{0} ({1})\n", this.GetObjectGameName(item, this.questEnviroment.Items), _count);
							break;
						}
					}
				}
				return ImportExport.RemoveLastNewLine(result).ToString();
			}
			return string.Empty;
		}

		// Token: 0x060012CF RID: 4815 RVA: 0x00088E84 File Offset: 0x00087E84
		private string GetRewardString(QuestClass quest)
		{
			StringBuilder result = new StringBuilder();
			result.AppendFormat(Strings.QUEST_EXPORT_QUEST_REWARD_EXPERIENCE, quest.RewExpereince, QuestEditor.GetRealExperienceQuest(quest.Level, quest.RewExpereince), '\n');
			int copper = quest.RewMoney % 100;
			int gold = (quest.RewMoney - copper) / 100;
			int silver = gold % 100;
			gold = (gold - silver) / 100;
			result.AppendFormat(Strings.QUEST_EXPORT_QUEST_REWARD_MONEY, new object[]
			{
				gold,
				silver,
				copper,
				'\n'
			});
			Dictionary<string, QuestClass.Pair<int, bool>> mandatoryItems;
			quest.GetMandatoryRewardItems(out mandatoryItems);
			if (mandatoryItems != null && this.questEnviroment.Items != null)
			{
				bool caption = false;
				foreach (KeyValuePair<string, QuestClass.Pair<int, bool>> reward in mandatoryItems)
				{
					if (!caption)
					{
						result.AppendFormat(Strings.QUEST_EXPORT_QUEST_REWARD_MONDATOTY_ITEMS, '\n');
						caption = true;
					}
					result.AppendFormat("{0} ({1})\n", this.GetObjectGameName(reward.Key, this.questEnviroment.Items), reward.Value.First);
				}
			}
			Dictionary<string, QuestClass.Pair<int, bool>> alternativeItems;
			quest.GetAlternativeRewardItems(out alternativeItems);
			if (alternativeItems != null && this.questEnviroment.Items != null)
			{
				bool caption2 = false;
				foreach (KeyValuePair<string, QuestClass.Pair<int, bool>> reward2 in alternativeItems)
				{
					if (!caption2)
					{
						result.AppendFormat(Strings.QUEST_EXPORT_QUEST_REWARD_ALTERNATIVE_ITEMS, '\n');
						caption2 = true;
					}
					result.AppendFormat("{0} ({1})\n", this.GetObjectGameName(reward2.Key, this.questEnviroment.Items), reward2.Value.First);
				}
			}
			return ImportExport.RemoveLastNewLine(result).ToString();
		}

		// Token: 0x060012D0 RID: 4816 RVA: 0x0008908C File Offset: 0x0008808C
		private Dictionary<string, List<QuestGiverClass>> HashQuestGivers()
		{
			Dictionary<string, List<QuestGiverClass>> questGiverHash = new Dictionary<string, List<QuestGiverClass>>();
			if (this.questEnviroment.QuestGivers != null)
			{
				List<string> availableQuests = new List<string>();
				foreach (GameObjectClass gameObjectClass in this.questEnviroment.QuestGivers)
				{
					QuestGiverClass questGiver = (QuestGiverClass)gameObjectClass;
					availableQuests.Clear();
					questGiver.GetAllQuests(availableQuests);
					foreach (string quest in availableQuests)
					{
						List<QuestGiverClass> questGivers;
						if (!questGiverHash.TryGetValue(quest, out questGivers))
						{
							questGivers = new List<QuestGiverClass>();
							questGiverHash.Add(quest, questGivers);
						}
						questGivers.Add(questGiver);
					}
				}
			}
			return questGiverHash;
		}

		// Token: 0x060012D1 RID: 4817 RVA: 0x00089168 File Offset: 0x00088168
		private void CreatQuestRows(IEnumerable<GameObjectClass> questView, ref List<string[]> questList)
		{
			Dictionary<string, List<QuestGiverClass>> questGiverHash = this.HashQuestGivers();
			foreach (GameObjectClass gameObject in questView)
			{
				QuestClass quest = gameObject as QuestClass;
				if (quest != null)
				{
					string[] quetsLine = new string[12];
					quetsLine[0] = quest.GameObject;
					quetsLine[1] = quest.GameName;
					StringBuilder questGiverBuilder = new StringBuilder();
					List<QuestGiverClass> questGivers;
					if (questGiverHash.TryGetValue(quest.GameObject, out questGivers))
					{
						foreach (QuestGiverClass questGiver in questGivers)
						{
							questGiverBuilder.AppendFormat("{0}\n", this.GetObjectGameName(questGiver.GameObject, this.questEnviroment.QuestGivers));
						}
					}
					quetsLine[2] = ImportExport.RemoveLastNewLine(questGiverBuilder).ToString();
					quetsLine[3] = quest.IssueText;
					quetsLine[4] = quest.GoalText;
					quetsLine[5] = quest.NotCompletedText;
					if (this.questEnviroment.QuestGivers != null)
					{
						quetsLine[6] = this.GetObjectGameName(quest.Finisher, this.questEnviroment.QuestGivers);
					}
					quetsLine[7] = quest.CompletedText;
					quetsLine[8] = this.GetQuestItems(quest);
					List<string> lootItems = new List<string>();
					StringBuilder itemLoot = new StringBuilder();
					if (this.questEnviroment.Items != null)
					{
						List<QuestClass.LootTableRow> lootTable;
						quest.GetLootTable(out lootTable);
						foreach (QuestClass.LootTableRow row in lootTable)
						{
							if (!lootItems.Contains(row.Item))
							{
								lootItems.Add(row.Item);
								itemLoot.AppendFormat("{0}\n", this.GetObjectGameName(row.Item, this.questEnviroment.Items));
							}
						}
					}
					quetsLine[9] = ImportExport.RemoveLastNewLine(itemLoot).ToString();
					quetsLine[10] = this.GetRewardString(quest);
					List<string> finishedQuests;
					List<string> notStartedQuests;
					string characterClass;
					quest.GetPrerequisits(out finishedQuests, out notStartedQuests, out characterClass);
					StringBuilder prevQuests = new StringBuilder();
					if (finishedQuests != null)
					{
						foreach (string finishedQuest in finishedQuests)
						{
							prevQuests.AppendFormat("{0}\n", finishedQuest);
						}
					}
					quetsLine[11] = ImportExport.RemoveLastNewLine(prevQuests).ToString();
					questList.Add(quetsLine);
				}
			}
		}

		// Token: 0x060012D2 RID: 4818 RVA: 0x00089420 File Offset: 0x00088420
		private static void CreateCaptions(out string[] captions)
		{
			captions = new string[12];
			captions[0] = Strings.QUEST_EXPORT_DBID_COL_TILTE;
			captions[1] = Strings.QUEST_EXPORT_QUEST_NAME_COL_TITLE;
			captions[2] = Strings.QUEST_EXPORT_QUEST_GIVER_COL_TITLE;
			captions[3] = Strings.QUEST_EXPORT_QUEST_START_TEXT_COL_TITLE;
			captions[4] = Strings.QUEST_EXPORT_QUEST_GOAL_COL_TITLE;
			captions[5] = Strings.QUEST_EXPORT_QUEST_NOT_COMPLETED_TEXT_COL_TITLE;
			captions[6] = Strings.QUEST_EXPORT_QUEST_FINISHER_COL_TITLE;
			captions[7] = Strings.QUEST_EXPORT_QUEST_COMPLETED_TEXT_COL_TITLE;
			captions[8] = Strings.QUEST_EXPORT_QUEST_START_ITEMS_COL_TITLE;
			captions[9] = Strings.QUEST_EXPORT_QUEST_LOOT_COL_TITLE;
			captions[10] = Strings.QUEST_EXPORT_QUEST_REWARD_COL_TITLE;
			captions[11] = Strings.QUEST_EXPORT_PREVIOUS_QUEST_COL_TITLE;
		}

		// Token: 0x060012D3 RID: 4819 RVA: 0x000894A8 File Offset: 0x000884A8
		private int WriteTextToDB(Array questLines, ref List<string> notFoundedQuest)
		{
			IDatabase mainDb = IDatabase.GetMainDatabase();
			if (mainDb == null || questLines.Rank != 2 || questLines.GetLength(1) != 12)
			{
				return 0;
			}
			List<string> importedQuest = new List<string>(questLines.GetLength(0));
			int firstColNum = questLines.GetLowerBound(1);
			for (int questNum = questLines.GetLowerBound(0); questNum < questLines.GetUpperBound(0) + 1; questNum++)
			{
				object questIdObj = questLines.GetValue(questNum, 1);
				if (questIdObj != null && !string.IsNullOrEmpty(questIdObj.ToString()))
				{
					string questId = questIdObj.ToString();
					bool success = false;
					if (questId.EndsWith(".xdb") && File.Exists(EditorEnvironment.DataFolder + questId))
					{
						DBID questDBID = mainDb.GetDBIDByName(questId);
						if (!DBID.IsNullOrEmpty(questDBID))
						{
							IObjMan questMan = mainDb.GetManipulator(questDBID);
							if (questMan != null)
							{
								string questName = string.Empty;
								string startText = string.Empty;
								string goal = string.Empty;
								string notCompletedText = string.Empty;
								string completedText = string.Empty;
								object value = questLines.GetValue(questNum, firstColNum + 1);
								if (value != null && !string.IsNullOrEmpty(value.ToString()))
								{
									questName = value.ToString();
								}
								value = questLines.GetValue(questNum, firstColNum + 3);
								if (value != null && !string.IsNullOrEmpty(value.ToString()))
								{
									startText = value.ToString();
								}
								value = questLines.GetValue(questNum, firstColNum + 4);
								if (value != null && !string.IsNullOrEmpty(value.ToString()))
								{
									goal = value.ToString();
								}
								value = questLines.GetValue(questNum, firstColNum + 5);
								if (value != null && !string.IsNullOrEmpty(value.ToString()))
								{
									notCompletedText = value.ToString();
								}
								value = questLines.GetValue(questNum, firstColNum + 7);
								if (value != null && !string.IsNullOrEmpty(value.ToString()))
								{
									completedText = value.ToString();
								}
								DBMethods.SetTextValue(questMan, "name", questName, QuestClass.defaultGameNameFileName, false);
								DBMethods.SetTextValue(questMan, "startText", startText, QuestClass.defaultStartTextFileName, true);
								DBMethods.SetTextValue(questMan, "goal", goal, QuestClass.defaultGoalFileName, true);
								DBMethods.SetTextValue(questMan, "checkText", notCompletedText, QuestClass.defaultCheckTextFileName, true);
								DBMethods.SetTextValue(questMan, "finishText", completedText, QuestClass.defaultFinishTextFileName, true);
								success = true;
								importedQuest.Add(questDBID.ToString());
							}
						}
					}
					if (!success)
					{
						notFoundedQuest.Add(questId);
					}
				}
			}
			int result = importedQuest.Count;
			if (result > 0)
			{
				mainDb.SaveChanges();
				if (this.QuestTextsImported != null)
				{
					this.QuestTextsImported(importedQuest);
				}
			}
			return result;
		}

		// Token: 0x060012D4 RID: 4820 RVA: 0x0008971A File Offset: 0x0008871A
		public ImportExport(QuestEnvironment _questEnviroment)
		{
			this.questEnviroment = _questEnviroment;
		}

		// Token: 0x060012D5 RID: 4821 RVA: 0x0008972C File Offset: 0x0008872C
		public void ExportToExcel(View questView)
		{
			if (this.questEnviroment == null)
			{
				return;
			}
			List<string[]> questList = new List<string[]>();
			string[] captions;
			ImportExport.CreateCaptions(out captions);
			questList.Add(captions);
			this.CreatQuestRows(questView, ref questList);
			string[,] questLines = new string[questList.Count, 12];
			for (int i = 0; i < questList.Count; i++)
			{
				for (int j = 0; j < 12; j++)
				{
					questLines[i, j] = questList[i][j];
				}
			}
			ExportImport.ToExcel(questLines, null);
		}

		// Token: 0x060012D6 RID: 4822 RVA: 0x000897AC File Offset: 0x000887AC
		public void ExortCustomCounters(View questView)
		{
			List<string[]> customCounters = new List<string[]>();
			customCounters.Add(new string[]
			{
				Strings.QUEST_EXPORT_DBID_COL_TILTE,
				Strings.QUEST_EXPORT_QUEST_NAME_COL_TITLE,
				Strings.QUEST_EXPORT_COUNTER_DBID_COL_TITLE,
				Strings.QUEST_EXPORT_COUNTER_NAME_COL_TITLE
			});
			foreach (GameObjectClass gameObjectClass in questView)
			{
				QuestClass quest = (QuestClass)gameObjectClass;
				Dictionary<int, QuestClass.QuestCounter> counters;
				bool prototiped;
				quest.GetCounters(out counters, out prototiped);
				if (counters != null)
				{
					foreach (QuestClass.QuestCounter counter in counters.Values)
					{
						string questId = quest.GameObject;
						string questName = quest.GameName;
						if (counter.Type == "gameMechanics.elements.quest.QuestCountSpecial" && counter.ItemList.Count == 1)
						{
							customCounters.Add(new string[]
							{
								questId,
								questName,
								counter.ItemList[0],
								counter.CustomName
							});
						}
					}
				}
			}
			string[,] counterLines = new string[customCounters.Count, 12];
			for (int i = 0; i < customCounters.Count; i++)
			{
				for (int j = 0; j < 4; j++)
				{
					counterLines[i, j] = customCounters[i][j];
				}
			}
			ExportImport.ToExcel(counterLines, null);
		}

		// Token: 0x060012D7 RID: 4823 RVA: 0x00089944 File Offset: 0x00088944
		public int ImportFromExcel(string filePath, out List<string> notFoundedList)
		{
			notFoundedList = new List<string>();
			object[,] questLines;
			ExportImport.FromExcel(filePath, 12, true, out questLines);
			if (questLines != null)
			{
				return this.WriteTextToDB(questLines, ref notFoundedList);
			}
			return 0;
		}

		// Token: 0x060012D8 RID: 4824 RVA: 0x00089970 File Offset: 0x00088970
		public int ImportCustomCounters(string filePath, out List<string> notFoundedList)
		{
			notFoundedList = new List<string>();
			object[,] counterLines;
			ExportImport.FromExcel(filePath, 4, true, out counterLines);
			if (counterLines == null)
			{
				return 0;
			}
			Dictionary<string, string> importedQuests = new Dictionary<string, string>();
			IDatabase mainDb = IDatabase.GetMainDatabase();
			if (mainDb == null || counterLines.Rank != 2 || counterLines.GetLength(1) != 4)
			{
				return 0;
			}
			int firstColNum = counterLines.GetLowerBound(1);
			int counterCount = counterLines.GetUpperBound(0) + 1;
			for (int counterNum = counterLines.GetLowerBound(0); counterNum < counterCount; counterNum++)
			{
				object questIdObj = counterLines.GetValue(counterNum, firstColNum);
				if (questIdObj != null && !string.IsNullOrEmpty(questIdObj.ToString()))
				{
					string questId = questIdObj.ToString();
					bool success = false;
					if (questId.EndsWith(".xdb") && File.Exists(EditorEnvironment.DataFolder + questId))
					{
						DBID questDBID = mainDb.GetDBIDByName(questId);
						if (!DBID.IsNullOrEmpty(questDBID))
						{
							IObjMan questMan = mainDb.GetManipulator(questDBID);
							if (questMan != null)
							{
								object counterIdObj = counterLines.GetValue(counterNum, firstColNum + 2);
								if (counterIdObj != null && !string.IsNullOrEmpty(counterIdObj.ToString()))
								{
									string counterId = counterIdObj.ToString();
									DBID counterDBID = mainDb.GetDBIDByName(counterId);
									if (!DBID.IsNullOrEmpty(counterDBID))
									{
										IObjMan countersMan = questMan.CreateManipulator("counters");
										int cnt = countersMan.GetArraySize();
										for (int arrayIndex = 0; arrayIndex < cnt; arrayIndex++)
										{
											countersMan.SetArrayIndex(arrayIndex);
											if (countersMan.IsStructPtrInstanceCompatible(string.Empty, "gameMechanics.elements.quest.QuestCountSpecial"))
											{
												DBID _counterDBID;
												countersMan.GetValue("id", out _counterDBID);
												if (counterDBID == _counterDBID)
												{
													string fileName = null;
													int index = 0;
													while (string.IsNullOrEmpty(fileName) || File.Exists(fileName))
													{
														fileName = EditorEnvironment.DataFolder + counterId.Replace(".xdb", string.Format("_Name{0}.txt", (index != 0) ? index.ToString() : string.Empty));
														index++;
													}
													string text = null;
													object value = counterLines.GetValue(counterNum, firstColNum + 3);
													if (value != null && !string.IsNullOrEmpty(value.ToString()))
													{
														text = value.ToString();
													}
													DBMethods.SetTextValue(countersMan, "customName", text, fileName, false);
													success = true;
													if (!importedQuests.ContainsKey(questId))
													{
														importedQuests.Add(questId, null);
														break;
													}
													break;
												}
											}
										}
										if (!success)
										{
											notFoundedList.Add(counterId);
										}
									}
								}
							}
						}
					}
				}
			}
			if (importedQuests.Count > 0)
			{
				mainDb.SaveChanges();
				if (this.QuestTextsImported != null)
				{
					this.QuestTextsImported(importedQuests.Keys);
				}
			}
			return importedQuests.Count;
		}

		// Token: 0x04000D6D RID: 3437
		private const int questDBIDCol = 0;

		// Token: 0x04000D6E RID: 3438
		private const int questNameCol = 1;

		// Token: 0x04000D6F RID: 3439
		private const int questGiverCol = 2;

		// Token: 0x04000D70 RID: 3440
		private const int startTextCol = 3;

		// Token: 0x04000D71 RID: 3441
		private const int goalCol = 4;

		// Token: 0x04000D72 RID: 3442
		private const int notFinishedTextCol = 5;

		// Token: 0x04000D73 RID: 3443
		private const int FinisherCol = 6;

		// Token: 0x04000D74 RID: 3444
		private const int FinishTextCol = 7;

		// Token: 0x04000D75 RID: 3445
		private const int QuestItemCol = 8;

		// Token: 0x04000D76 RID: 3446
		private const int LootCol = 9;

		// Token: 0x04000D77 RID: 3447
		private const int RewardCol = 10;

		// Token: 0x04000D78 RID: 3448
		private const int PrevQuestCol = 11;

		// Token: 0x04000D79 RID: 3449
		private const int colCnt = 12;

		// Token: 0x04000D7A RID: 3450
		private const string firstExcelCol = "A";

		// Token: 0x04000D7B RID: 3451
		private const string lastExcelCol = "L";

		// Token: 0x04000D7C RID: 3452
		private const string impactGiveItem = "gameMechanics.elements.impacts.ImpactGiveItem";

		// Token: 0x04000D7E RID: 3454
		private readonly QuestEnvironment questEnviroment;

		// Token: 0x04000D7F RID: 3455
		private static readonly string[] questItemTypes = new string[]
		{
			"gameMechanics.elements.impacts.ImpactGiveItem",
			"gameMechanics.elements.impacts.ImpactGiveItemInActiveSlot"
		};

		// Token: 0x0200018A RID: 394
		// (Invoke) Token: 0x060012DB RID: 4827
		public delegate void ImportEvent(IEnumerable<string> quests);
	}
}
