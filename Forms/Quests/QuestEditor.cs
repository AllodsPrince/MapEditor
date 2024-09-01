using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using Db;
using InputState;
using LauncherTools.InputState;
using MapEditor.Map.MapObjects;
using MapEditor.Resources.Strings;
using Tools.DBGameObjects;
using Tools.DBGameObjects.GameObjects;
using Tools.MapObjects;

namespace MapEditor.Forms.Quests
{
	// Token: 0x0200023E RID: 574
	public class QuestEditor
	{
		// Token: 0x140000C4 RID: 196
		// (add) Token: 0x06001B5F RID: 7007 RVA: 0x000B1895 File Offset: 0x000B0895
		// (remove) Token: 0x06001B60 RID: 7008 RVA: 0x000B18AE File Offset: 0x000B08AE
		public event QuestEditor.LoadZoneEvent LoadZone;

		// Token: 0x140000C5 RID: 197
		// (add) Token: 0x06001B61 RID: 7009 RVA: 0x000B18C7 File Offset: 0x000B08C7
		// (remove) Token: 0x06001B62 RID: 7010 RVA: 0x000B18E0 File Offset: 0x000B08E0
		public event QuestEditor.LoadQuestEvent LoadQuest;

		// Token: 0x140000C6 RID: 198
		// (add) Token: 0x06001B63 RID: 7011 RVA: 0x000B18F9 File Offset: 0x000B08F9
		// (remove) Token: 0x06001B64 RID: 7012 RVA: 0x000B1912 File Offset: 0x000B0912
		public event QuestEditor.LoadQuestEvent LoadQuestToDiagram;

		// Token: 0x140000C7 RID: 199
		// (add) Token: 0x06001B65 RID: 7013 RVA: 0x000B192B File Offset: 0x000B092B
		// (remove) Token: 0x06001B66 RID: 7014 RVA: 0x000B1944 File Offset: 0x000B0944
		public event QuestEditor.CreateNewQuestEvent CreateNewQuestDelegate;

		// Token: 0x06001B67 RID: 7015 RVA: 0x000B1960 File Offset: 0x000B0960
		private bool LoadQuestsFromMapObjectList(IEnumerable<IMapObject> mapObjects, out GeneralView quests)
		{
			quests = null;
			if (mapObjects != null)
			{
				foreach (IMapObject mapObject in mapObjects)
				{
					if (mapObject.Type.Type == MapObjectFactory.Type.SpawnPoint && quests == null)
					{
						SpawnPoint spawnPoint = mapObject as SpawnPoint;
						if (spawnPoint != null)
						{
							quests = this.LoadQuestsFormSpawnPoint(spawnPoint.SpawnTable);
						}
					}
				}
			}
			return quests != null;
		}

		// Token: 0x06001B68 RID: 7016 RVA: 0x000B19E0 File Offset: 0x000B09E0
		private GeneralView LoadQuestsFormSpawnPoint(string spawn)
		{
			GeneralView view = null;
			IDatabase mainDb = IDatabase.GetMainDatabase();
			List<string> findedQuests = new List<string>();
			foreach (QuestGiverClass questGiver in this.LoadQuestGiversFromSpawnPoint(spawn))
			{
				questGiver.GetAllQuests(findedQuests);
			}
			if (mainDb != null)
			{
				view = new GeneralView(null);
				foreach (string findedQuest in findedQuests)
				{
					DBID questDBID = mainDb.GetDBIDByName(findedQuest);
					if (!DBID.IsNullOrEmpty(questDBID))
					{
						IObjMan questMan = mainDb.GetManipulator(questDBID);
						if (questMan != null)
						{
							view.AddObjectIfNotFinded(new QuestClass(questMan));
						}
					}
				}
			}
			return view;
		}

		// Token: 0x06001B69 RID: 7017 RVA: 0x000B1AB8 File Offset: 0x000B0AB8
		private static void ParseSpawnPoint(string spawn, ref List<DBID> resources)
		{
			if (resources == null)
			{
				resources = new List<DBID>();
			}
			IDatabase mainDb = IDatabase.GetMainDatabase();
			if (mainDb != null)
			{
				DBID spawnDBID = mainDb.GetDBIDByName(spawn);
				if (!DBID.IsNullOrEmpty(spawnDBID))
				{
					if (mainDb.GetClassTypeName(spawnDBID) == "gameMechanics.map.spawn.SpawnTable")
					{
						IObjMan objMan = mainDb.GetManipulator(spawnDBID);
						if (objMan != null)
						{
							int cnt;
							objMan.GetValue("singles", out cnt);
							for (int index = 0; index < cnt; index++)
							{
								DBID obj;
								objMan.GetValue(string.Format("singles.[{0}].object", index), out obj);
								resources.Add(obj);
							}
							objMan.GetValue("commons", out cnt);
							for (int index2 = 0; index2 < cnt; index2++)
							{
								DBID obj2;
								objMan.GetValue(string.Format("commons.[{0}].object", index2), out obj2);
								resources.Add(obj2);
							}
							return;
						}
					}
					else
					{
						resources.Add(spawnDBID);
					}
				}
			}
		}

		// Token: 0x06001B6A RID: 7018 RVA: 0x000B1B9C File Offset: 0x000B0B9C
		private List<QuestGiverClass> LoadQuestGiversFromSpawnPoint(string spawn)
		{
			List<QuestGiverClass> resultList = new List<QuestGiverClass>();
			List<DBID> questGiverDBIDList = new List<DBID>();
			QuestEditor.ParseSpawnPoint(spawn, ref questGiverDBIDList);
			IDatabase mainDb = IDatabase.GetMainDatabase();
			if (mainDb != null)
			{
				foreach (DBID dbid in questGiverDBIDList)
				{
					IObjMan objMan = mainDb.GetManipulator(dbid);
					if (objMan != null)
					{
						resultList.Add(this.questEnvironment.FindGameObject(this.questEnvironment.QuestGivers, dbid.ToString()) as QuestGiverClass);
					}
				}
			}
			return resultList;
		}

		// Token: 0x06001B6B RID: 7019 RVA: 0x000B1C38 File Offset: 0x000B0C38
		public void ParseMapObjectList(IEnumerable<IMapObject> mapObjects, out List<SpawnPoint> spwnPoints)
		{
			spwnPoints = null;
			if (mapObjects != null)
			{
				foreach (IMapObject mapObject in mapObjects)
				{
					if (mapObject.Type.Type == MapObjectFactory.Type.SpawnPoint)
					{
						if (spwnPoints == null)
						{
							spwnPoints = new List<SpawnPoint>();
						}
						spwnPoints.Add(mapObject as SpawnPoint);
					}
				}
			}
		}

		// Token: 0x06001B6C RID: 7020 RVA: 0x000B1CAC File Offset: 0x000B0CAC
		private void OnLoadQuestListByMapObjects(MethodArgs args)
		{
			GeneralView view;
			if (this.LoadQuestsFromMapObjectList(args.sender as IEnumerable<IMapObject>, out view))
			{
				QuestSelectDialogForm dialogForm = new QuestSelectDialogForm(this.questEnvironment, view, null);
				if (dialogForm.ShowDialog() == DialogResult.OK)
				{
					QuestClass quest = dialogForm.GetSelectedItem() as QuestClass;
					if (quest != null)
					{
						this.Load(quest);
					}
				}
			}
		}

		// Token: 0x06001B6D RID: 7021 RVA: 0x000B1CFC File Offset: 0x000B0CFC
		private void OnAddQuestToMapObject(MethodArgs args)
		{
			List<SpawnPoint> spawnPoints;
			this.ParseMapObjectList(args.sender as IEnumerable<IMapObject>, out spawnPoints);
			if (spawnPoints != null && spawnPoints.Count == 1 && spawnPoints[0] != null)
			{
				List<QuestGiverClass> list = this.LoadQuestGiversFromSpawnPoint(spawnPoints[0].SpawnTable);
				if (list != null && list.Count == 1 && list[0] != null)
				{
					QuestGiverClass questGiver = list[0];
					AddQuestDialog dialog = new AddQuestDialog(null);
					if (dialog.ShowDialog(this.context.MainForm) == DialogResult.OK)
					{
						if (this.questEditorForm == null)
						{
							this.questEditorForm = new QuestEditorForm(this.context);
						}
						if (!this.questEditorForm.Visible)
						{
							this.questEditorForm.Show(this.context.MainForm);
						}
						if (this.CreateNewQuestDelegate != null)
						{
							this.CreateNewQuestDelegate(dialog.QuestZone, dialog.QuestName, questGiver);
						}
					}
				}
			}
		}

		// Token: 0x06001B6E RID: 7022 RVA: 0x000B1DE8 File Offset: 0x000B0DE8
		private void OnFindQuestForKilling(MethodArgs args)
		{
			List<SpawnPoint> spawnPoints;
			this.ParseMapObjectList(args.sender as IEnumerable<IMapObject>, out spawnPoints);
			if (spawnPoints != null && spawnPoints.Count > 0)
			{
				List<DBID> resurces = new List<DBID>();
				foreach (SpawnPoint spawnPoint in spawnPoints)
				{
					QuestEditor.ParseSpawnPoint(spawnPoint.SpawnTable, ref resurces);
				}
				if (resurces.Count > 0)
				{
					QuestEditorZoneForm zoneDialog = new QuestEditorZoneForm();
					if (zoneDialog.ShowDialog() == DialogResult.OK)
					{
						this.context.MainForm.Cursor = Cursors.WaitCursor;
						if (zoneDialog.Zone)
						{
							this.questEnvironment.Load(zoneDialog.Filter);
						}
						else
						{
							this.questEnvironment.LoadByFilter(zoneDialog.Filter);
						}
						FilteredView filteredView = new FilteredView(this.questEnvironment.Quests);
						filteredView.AddFilter(new DBIDFilter(zoneDialog.Filter));
						GeneralView view = new GeneralView(string.Empty);
						foreach (GameObjectClass gameObjectClass in filteredView)
						{
							QuestClass quest = (QuestClass)gameObjectClass;
							if (quest != null)
							{
								bool filtered = false;
								foreach (DBID dbid in resurces)
								{
									if (!DBID.IsNullOrEmpty(dbid))
									{
										string resource = dbid.ToString();
										Dictionary<int, QuestClass.QuestCounter> counters;
										bool prototiped;
										if (quest.GetCounters(out counters, out prototiped))
										{
											foreach (QuestClass.QuestCounter counter in counters.Values)
											{
												if (counter.ItemList != null && counter.ItemList.Contains(resource))
												{
													filtered = true;
													break;
												}
											}
										}
										List<QuestClass.LootTableRow> lootTable;
										if (!filtered && quest.GetLootTable(out lootTable))
										{
											foreach (QuestClass.LootTableRow row in lootTable)
											{
												if (row.Lootable == resource)
												{
													filtered = true;
													break;
												}
											}
										}
									}
									if (filtered)
									{
										view.AddObjectIfNotFinded(quest);
									}
								}
							}
						}
						this.context.MainForm.Cursor = Cursors.Default;
						QuestSelectDialogForm dialogForm = new QuestSelectDialogForm(this.questEnvironment, view, null);
						if (dialogForm.ShowDialog() == DialogResult.OK)
						{
							QuestClass quest2 = dialogForm.GetSelectedItem() as QuestClass;
							if (quest2 != null)
							{
								this.Load(quest2);
							}
						}
					}
				}
			}
		}

		// Token: 0x06001B6F RID: 7023 RVA: 0x000B20F0 File Offset: 0x000B10F0
		private static bool GetQuestZone(string questPathFromData, out DBID zone)
		{
			zone = null;
			string zoneFolder = QuestClass.GetZoneFolder(questPathFromData);
			if (string.IsNullOrEmpty(zoneFolder))
			{
				return false;
			}
			Dictionary<string, DBID> zones = new Dictionary<string, DBID>();
			QuestEnvironment.LoadZones(zones);
			return zones.TryGetValue(zoneFolder, out zone) && !DBID.IsNullOrEmpty(zone);
		}

		// Token: 0x06001B70 RID: 7024 RVA: 0x000B2134 File Offset: 0x000B1134
		public QuestEditor(MainForm.Context _context, QuestEnvironment _questEnvironment)
		{
			this.context = _context;
			this.questEnvironment = _questEnvironment;
			this.questEditorState.AddMethod("_load_quest_list_by_object", new Method(this.OnLoadQuestListByMapObjects));
			this.questEditorState.AddMethod("_add_quest_to_object", new Method(this.OnAddQuestToMapObject));
			this.questEditorState.AddMethod("_find_quests_for_killing", new Method(this.OnFindQuestForKilling));
			this.context.StateContainer.BindState(this.questEditorState);
		}

		// Token: 0x06001B71 RID: 7025 RVA: 0x000B21CF File Offset: 0x000B11CF
		public void Destroy()
		{
			this.context.StateContainer.UnbindState(this.questEditorState);
			if (this.questEditorForm != null)
			{
				this.questEditorForm.AllowClose = true;
				this.questEditorForm.Close();
				this.questEditorForm = null;
			}
		}

		// Token: 0x06001B72 RID: 7026 RVA: 0x000B2210 File Offset: 0x000B1210
		public void Load(string filter, bool asZone)
		{
			this.context.MainForm.Cursor = Cursors.WaitCursor;
			if (asZone)
			{
				this.questEnvironment.Load(filter);
			}
			else
			{
				this.questEnvironment.LoadByFilter(filter);
			}
			this.context.MainForm.Cursor = Cursors.Default;
			if (this.questEditorForm == null)
			{
				this.questEditorForm = new QuestEditorForm(this.context);
			}
			if (!this.questEditorForm.Visible)
			{
				this.questEditorForm.Show(this.context.MainForm);
			}
			this.questEditorForm.Focus();
			if (asZone && this.LoadZone != null)
			{
				this.LoadZone(filter);
			}
		}

		// Token: 0x06001B73 RID: 7027 RVA: 0x000B22C4 File Offset: 0x000B12C4
		public void Load(QuestClass quest)
		{
			if (quest != null)
			{
				string zone = quest.GetZoneFolder();
				if (string.IsNullOrEmpty(zone))
				{
					zone = "_OtherFolder";
				}
				this.context.MainForm.Cursor = Cursors.WaitCursor;
				this.questEnvironment.Load(zone);
				this.context.MainForm.Cursor = Cursors.Default;
				if (this.questEditorForm == null)
				{
					this.questEditorForm = new QuestEditorForm(this.context);
				}
				if (!this.questEditorForm.Visible)
				{
					this.questEditorForm.Show(this.context.MainForm);
				}
				this.questEditorForm.Focus();
				if (this.LoadQuest != null)
				{
					this.LoadQuest(quest);
				}
			}
		}

		// Token: 0x17000674 RID: 1652
		// (get) Token: 0x06001B74 RID: 7028 RVA: 0x000B237E File Offset: 0x000B137E
		public static string QuestFolder
		{
			get
			{
				return "World/Quests/";
			}
		}

		// Token: 0x06001B75 RID: 7029 RVA: 0x000B2388 File Offset: 0x000B1388
		public static bool DoesCounterNameOccupied(QuestClass quest, string counterName)
		{
			if (File.Exists(EditorEnvironment.DataFolder + counterName))
			{
				return true;
			}
			Dictionary<int, QuestClass.QuestCounter> counters;
			bool prototiped;
			quest.GetCounters(out counters, out prototiped);
			foreach (KeyValuePair<int, QuestClass.QuestCounter> counterArrayElem in counters)
			{
				QuestClass.QuestCounter counter = counterArrayElem.Value;
				if (counter.Type == "gameMechanics.elements.quest.QuestCountSpecial" && counter.ItemList != null && counter.ItemList.Count == 0 && counterName == counter.ItemList[0])
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06001B76 RID: 7030 RVA: 0x000B243C File Offset: 0x000B143C
		public static int GetRealExperienceQuest(int questLevel, int experience)
		{
			return QuestClass.GetRealExperience(questLevel, experience);
		}

		// Token: 0x06001B77 RID: 7031 RVA: 0x000B2448 File Offset: 0x000B1448
		public static bool CheckNewQuestBeforeCreate(IDatabase mainDb, DBID newQuestDBID, out DBID zone)
		{
			zone = null;
			return mainDb != null && !DBID.IsNullOrEmpty(newQuestDBID) && !mainDb.DoesObjectExist(newQuestDBID) && (QuestEditor.GetQuestZone(newQuestDBID.ToString(), out zone) || MessageBox.Show(string.Format(Strings.QUEST_EDITOR_CANNOT_FIND_ZONE_WARNING, newQuestDBID), Strings.QUEST_EDITOR_TITLE, MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) == DialogResult.Yes);
		}

		// Token: 0x06001B78 RID: 7032 RVA: 0x000B249C File Offset: 0x000B149C
		public static bool CreateNewQuest(IDatabase mainDb, DBID newQuestDBID, DBID zone)
		{
			if (mainDb != null && !mainDb.DoesObjectExist(newQuestDBID))
			{
				IObjMan objectMan = mainDb.CreateNewObject("gameMechanics.constructor.schemes.quest.QuestResource");
				mainDb.AddNewObject(newQuestDBID, objectMan);
				if (!DBID.IsNullOrEmpty(zone))
				{
					objectMan.SetValue("zone", zone);
				}
				return mainDb.DoesObjectExist(newQuestDBID);
			}
			return false;
		}

		// Token: 0x06001B79 RID: 7033 RVA: 0x000B24E7 File Offset: 0x000B14E7
		public void LoadQuestToQuestDiagram(QuestClass quest)
		{
			if (this.LoadQuestToDiagram != null)
			{
				this.LoadQuestToDiagram(quest);
			}
		}

		// Token: 0x0400117F RID: 4479
		private const string questFolder = "World/Quests/";

		// Token: 0x04001180 RID: 4480
		private readonly QuestEnvironment questEnvironment;

		// Token: 0x04001181 RID: 4481
		private readonly MainForm.Context context;

		// Token: 0x04001182 RID: 4482
		private readonly State questEditorState = new State("QuestEditorState");

		// Token: 0x04001183 RID: 4483
		private QuestEditorForm questEditorForm;

		// Token: 0x0200023F RID: 575
		// (Invoke) Token: 0x06001B7B RID: 7035
		public delegate void LoadZoneEvent(string zone);

		// Token: 0x02000240 RID: 576
		// (Invoke) Token: 0x06001B7F RID: 7039
		public delegate void LoadQuestEvent(QuestClass quest);

		// Token: 0x02000241 RID: 577
		// (Invoke) Token: 0x06001B83 RID: 7043
		public delegate void CreateNewQuestEvent(string zone, string quest, QuestGiverClass questGiver);
	}
}
