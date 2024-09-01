using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Db;
using Tools.DBGameObjects;
using Tools.DBGameObjects.GameObjects;

namespace MapEditor.Forms.Quests
{
	// Token: 0x0200009D RID: 157
	public class QuestEnvironment
	{
		// Token: 0x14000026 RID: 38
		// (add) Token: 0x06000747 RID: 1863 RVA: 0x00039448 File Offset: 0x00038448
		// (remove) Token: 0x06000748 RID: 1864 RVA: 0x00039461 File Offset: 0x00038461
		public event QuestEnvironment.QuestChangedEvent QuestChanged;

		// Token: 0x14000027 RID: 39
		// (add) Token: 0x06000749 RID: 1865 RVA: 0x0003947A File Offset: 0x0003847A
		// (remove) Token: 0x0600074A RID: 1866 RVA: 0x00039493 File Offset: 0x00038493
		public event QuestEnvironment.QuestDeletedEvent QuestDeleted;

		// Token: 0x14000028 RID: 40
		// (add) Token: 0x0600074B RID: 1867 RVA: 0x000394AC File Offset: 0x000384AC
		// (remove) Token: 0x0600074C RID: 1868 RVA: 0x000394C5 File Offset: 0x000384C5
		public event QuestEnvironment.ObjectDeletingEvent ObjectDeleting;

		// Token: 0x14000029 RID: 41
		// (add) Token: 0x0600074D RID: 1869 RVA: 0x000394DE File Offset: 0x000384DE
		// (remove) Token: 0x0600074E RID: 1870 RVA: 0x000394F7 File Offset: 0x000384F7
		public event QuestEnvironment.ZoneLoadedEvent ZoneLoaded;

		// Token: 0x0600074F RID: 1871 RVA: 0x00039510 File Offset: 0x00038510
		private void RegisterQuestEvents(QuestClass quest, bool register)
		{
			if (quest != null)
			{
				if (register)
				{
					quest.RegisterDBEvent(this.dbEventRegister);
					this.dbEventRegister.RegisterEventHandler("quest_deleting_event", new DbEventRegister.GameObjectEventHandler(quest.DeletePrereqQuest));
					return;
				}
				quest.UnregisterDBEvent(this.dbEventRegister);
				this.dbEventRegister.UnregisterEventHandler("quest_deleting_event", new DbEventRegister.GameObjectEventHandler(quest.DeletePrereqQuest));
			}
		}

		// Token: 0x06000750 RID: 1872 RVA: 0x00039574 File Offset: 0x00038574
		private void RegisterQuestGiverEvents(QuestGiverClass questGiver, bool register)
		{
			if (questGiver != null)
			{
				if (register)
				{
					this.dbEventRegister.RegisterEventHandler("quest_deleting_event", new DbEventRegister.GameObjectEventHandler(questGiver.RemoveQuest));
					return;
				}
				this.dbEventRegister.UnregisterEventHandler("quest_deleting_event", new DbEventRegister.GameObjectEventHandler(questGiver.RemoveQuest));
			}
		}

		// Token: 0x06000751 RID: 1873 RVA: 0x000395C0 File Offset: 0x000385C0
		private void LoadViews(Predicate<DBID> filter)
		{
			List<Tools.DBGameObjects.View> viewList;
			if (this.GetViewList("gameMechanics.world.device.DeviceResource", out viewList))
			{
				DBMethods.LoadObjects("gameMechanics.world.device.DeviceResource", viewList, filter, true, true);
			}
			if (this.GetViewList("gameMechanics.constructor.schemes.item.ItemResource", out viewList))
			{
				DBMethods.LoadObjects("gameMechanics.constructor.schemes.item.ItemResource", viewList, filter, true, false);
			}
			if (this.GetViewList("gameMechanics.world.mob.MobWorld", out viewList))
			{
				DBMethods.LoadObjects("gameMechanics.world.mob.MobWorld", viewList, filter, true, false);
			}
			if (this.GetViewList("gameMechanics.world.avatar.CharacterRace", out viewList))
			{
				DBMethods.LoadObjects("gameMechanics.world.avatar.CharacterRace", viewList, null, false, false);
			}
			if (this.GetViewList("gameMechanics.world.creature.Faction", out viewList))
			{
				DBMethods.LoadObjects("gameMechanics.world.creature.Faction", viewList, null, false, false);
			}
			if (this.GetViewList("gameMechanics.constructor.schemes.item.AlternativeCurrency", out viewList))
			{
				DBMethods.LoadObjects("gameMechanics.constructor.schemes.item.AlternativeCurrency", viewList, null, false, false);
			}
			DBMethods.LoadObjects("gameMechanics.constructor.schemes.quest.QuestResource", this.quests, filter, true);
			foreach (GameObjectClass gameObjectClass in this.questGivers)
			{
				QuestGiverClass questGiver = (QuestGiverClass)gameObjectClass;
				this.RegisterQuestGiverEvents(questGiver, true);
			}
			foreach (GameObjectClass gameObjectClass2 in this.quests)
			{
				QuestClass quest = (QuestClass)gameObjectClass2;
				this.RegisterQuestEvents(quest, true);
			}
		}

		// Token: 0x06000752 RID: 1874 RVA: 0x0003971C File Offset: 0x0003871C
		private void OnQuestTextsImported(IEnumerable<string> questList)
		{
			this.InvokeQuestChanged(this, questList);
		}

		// Token: 0x06000753 RID: 1875 RVA: 0x00039728 File Offset: 0x00038728
		private void OnDbObjectAddedToDb(DBID dbid)
		{
			foreach (Predicate<DBID> predicate in this.loadedZones.Values)
			{
				if (predicate(dbid))
				{
					IDatabase mainDb = IDatabase.GetMainDatabase();
					string type;
					if (mainDb != null && QuestEnvironment.CheckDBIDType(mainDb, dbid, out type))
					{
						IObjMan objMan = mainDb.GetManipulator(dbid);
						GameObjectClass newGameObject = GameObjectFactory.CreateGameObject(type, objMan);
						List<Tools.DBGameObjects.View> viewList;
						if (this.GetViewList(type, out viewList))
						{
							foreach (Tools.DBGameObjects.View view2 in viewList)
							{
								GeneralView view = (GeneralView)view2;
								view.AddObjectIfNotFinded(newGameObject);
							}
						}
						this.RegisterQuestEvents(newGameObject as QuestClass, true);
						this.RegisterQuestGiverEvents(newGameObject as QuestGiverClass, true);
						break;
					}
					break;
				}
			}
		}

		// Token: 0x06000754 RID: 1876 RVA: 0x00039828 File Offset: 0x00038828
		private void OnDbObjectRemovingFromDb(DBID dbid)
		{
			IDatabase mainDb = IDatabase.GetMainDatabase();
			string type;
			List<Tools.DBGameObjects.View> viewList;
			if (mainDb != null && QuestEnvironment.CheckDBIDType(mainDb, dbid, out type) && this.GetViewList(type, out viewList))
			{
				GameObjectClass deletingObject = null;
				foreach (Tools.DBGameObjects.View view2 in viewList)
				{
					GeneralView view = (GeneralView)view2;
					if (deletingObject == null)
					{
						deletingObject = view.GetObjectByDBID(dbid.ToString());
					}
					view.RemoveObject(deletingObject);
				}
				if (deletingObject != null)
				{
					this.RegisterQuestEvents(deletingObject as QuestClass, false);
					this.RegisterQuestGiverEvents(deletingObject as QuestGiverClass, false);
					if (this.ObjectDeleting != null)
					{
						this.ObjectDeleting(this, deletingObject);
					}
				}
			}
		}

		// Token: 0x06000755 RID: 1877 RVA: 0x000398EC File Offset: 0x000388EC
		private static bool CheckDBIDType(IDatabase mainDb, DBID dbid, out string type)
		{
			type = mainDb.GetClassTypeName(dbid);
			if (type == "gameMechanics.constructor.schemes.quest.QuestResource")
			{
				return true;
			}
			if (type == "gameMechanics.constructor.schemes.item.ItemResource")
			{
				return true;
			}
			if (type == "gameMechanics.world.mob.MobWorld")
			{
				return true;
			}
			if (DBMethods.TypeIsDerivedFrom(type, "gameMechanics.world.device.DeviceResource"))
			{
				type = "gameMechanics.world.device.DeviceResource";
				return true;
			}
			return false;
		}

		// Token: 0x06000756 RID: 1878 RVA: 0x0003994C File Offset: 0x0003894C
		private bool GetViewList(string type, out List<Tools.DBGameObjects.View> viewList)
		{
			viewList = new List<Tools.DBGameObjects.View>(9);
			if (type == "gameMechanics.constructor.schemes.quest.QuestResource")
			{
				viewList.Add(this.quests);
				return true;
			}
			if (type == "gameMechanics.constructor.schemes.item.ItemResource")
			{
				viewList.Add(this.items);
				viewList.Add(this.questGivers);
				return true;
			}
			if (type == "gameMechanics.world.mob.MobWorld")
			{
				viewList.Add(this.respawnableResources);
				viewList.Add(this.lootableObjects);
				viewList.Add(this.questGivers);
				return true;
			}
			if (type == "gameMechanics.world.device.DeviceResource")
			{
				viewList.Add(this.questGivers);
				viewList.Add(this.lootableObjects);
				viewList.Add(this.respawnableResources);
				return true;
			}
			if (type == "gameMechanics.world.avatar.CharacterRace")
			{
				viewList.Add(this.killAvatarCounterObjects);
				return true;
			}
			if (type == "gameMechanics.world.creature.Faction")
			{
				viewList.Add(this.killAvatarCounterObjects);
				viewList.Add(this.factions);
				return true;
			}
			if (type == "gameMechanics.constructor.schemes.item.AlternativeCurrency")
			{
				viewList.Add(this.currencies);
				return true;
			}
			return false;
		}

		// Token: 0x06000757 RID: 1879 RVA: 0x00039A78 File Offset: 0x00038A78
		private bool GetTypesByView(Tools.DBGameObjects.View view, out string[] types)
		{
			types = null;
			if (view == this.quests)
			{
				types = new string[]
				{
					"gameMechanics.constructor.schemes.quest.QuestResource"
				};
			}
			else if (view == this.items)
			{
				types = new string[]
				{
					"gameMechanics.constructor.schemes.item.ItemResource"
				};
			}
			else if (view == this.respawnableResources)
			{
				types = new string[]
				{
					"gameMechanics.world.mob.MobWorld",
					"gameMechanics.world.device.DeviceResource"
				};
			}
			else if (view == this.questGivers)
			{
				types = new string[]
				{
					"gameMechanics.constructor.schemes.item.ItemResource",
					"gameMechanics.world.mob.MobWorld",
					"gameMechanics.world.device.DeviceResource"
				};
			}
			else if (view == this.lootableObjects)
			{
				types = new string[]
				{
					"gameMechanics.world.mob.MobWorld",
					"gameMechanics.world.device.DeviceResource"
				};
			}
			else if (view == this.killAvatarCounterObjects)
			{
				types = new string[]
				{
					"gameMechanics.world.avatar.CharacterRace",
					"gameMechanics.world.creature.Faction"
				};
			}
			else if (view == this.factions)
			{
				types = new string[]
				{
					"gameMechanics.world.creature.Faction"
				};
			}
			else if (view == this.currencies)
			{
				types = new string[]
				{
					"gameMechanics.constructor.schemes.item.AlternativeCurrency"
				};
			}
			return types != null;
		}

		// Token: 0x06000758 RID: 1880 RVA: 0x00039BB8 File Offset: 0x00038BB8
		public QuestEnvironment(MainForm.Context context)
		{
			this.importExport = new ImportExport(this);
			this.questEditor = new QuestEditor(context, this);
			this.importExport.QuestTextsImported += this.OnQuestTextsImported;
			this.dbEventsGenerator = new DbEventsGenerator(IDatabase.GetMainDatabase());
			this.dbEventsGenerator.DBObjectAdded += this.OnDbObjectAddedToDb;
			this.dbEventsGenerator.DBObjectRemoved += this.OnDbObjectRemovingFromDb;
		}

		// Token: 0x06000759 RID: 1881 RVA: 0x00039CCF File Offset: 0x00038CCF
		public void Destroy()
		{
			this.questEditor.Destroy();
		}

		// Token: 0x0600075A RID: 1882 RVA: 0x00039CDC File Offset: 0x00038CDC
		public void Load(string zone)
		{
			if (!string.IsNullOrEmpty(zone) && !this.loadedZones.ContainsKey(zone))
			{
				Predicate<DBID> predicate;
				if (zone != "_OtherFolder")
				{
					DBIDSubstringPredicate filter = new DBIDSubstringPredicate(zone);
					predicate = new Predicate<DBID>(filter.Filter);
				}
				else
				{
					predicate = new Predicate<DBID>(QuestEnvironment.FromOtherZone);
				}
				this.LoadViews(predicate);
				this.loadedZones.Add(zone, predicate);
				if (this.ZoneLoaded != null)
				{
					this.ZoneLoaded(zone);
				}
			}
		}

		// Token: 0x0600075B RID: 1883 RVA: 0x00039D58 File Offset: 0x00038D58
		public void LoadByFilter(string _filter)
		{
			DBIDSubstringPredicate filter = new DBIDSubstringPredicate(_filter);
			this.LoadViews(new Predicate<DBID>(filter.Filter));
		}

		// Token: 0x0600075C RID: 1884 RVA: 0x00039D7E File Offset: 0x00038D7E
		public void InvokeQuestChanged(object sender, IEnumerable<string> changedQuests)
		{
			if (this.QuestChanged != null)
			{
				this.QuestChanged(sender, changedQuests);
			}
		}

		// Token: 0x0600075D RID: 1885 RVA: 0x00039D95 File Offset: 0x00038D95
		public void InvokeQuestDeleted(object sender, string zone)
		{
			if (this.QuestDeleted != null)
			{
				this.QuestDeleted(sender, zone);
			}
		}

		// Token: 0x1700011C RID: 284
		// (get) Token: 0x0600075E RID: 1886 RVA: 0x00039DAC File Offset: 0x00038DAC
		public ImportExport ImportExport
		{
			get
			{
				return this.importExport;
			}
		}

		// Token: 0x1700011D RID: 285
		// (get) Token: 0x0600075F RID: 1887 RVA: 0x00039DB4 File Offset: 0x00038DB4
		public QuestEditor QuestEditor
		{
			get
			{
				return this.questEditor;
			}
		}

		// Token: 0x1700011E RID: 286
		// (get) Token: 0x06000760 RID: 1888 RVA: 0x00039DBC File Offset: 0x00038DBC
		public GeneralView Items
		{
			get
			{
				return this.items;
			}
		}

		// Token: 0x1700011F RID: 287
		// (get) Token: 0x06000761 RID: 1889 RVA: 0x00039DC4 File Offset: 0x00038DC4
		public GeneralView RespawnableResources
		{
			get
			{
				return this.respawnableResources;
			}
		}

		// Token: 0x17000120 RID: 288
		// (get) Token: 0x06000762 RID: 1890 RVA: 0x00039DCC File Offset: 0x00038DCC
		public GeneralView QuestGivers
		{
			get
			{
				return this.questGivers;
			}
		}

		// Token: 0x17000121 RID: 289
		// (get) Token: 0x06000763 RID: 1891 RVA: 0x00039DD4 File Offset: 0x00038DD4
		public GeneralView Quests
		{
			get
			{
				return this.quests;
			}
		}

		// Token: 0x17000122 RID: 290
		// (get) Token: 0x06000764 RID: 1892 RVA: 0x00039DDC File Offset: 0x00038DDC
		public GeneralView LootableObjects
		{
			get
			{
				return this.lootableObjects;
			}
		}

		// Token: 0x17000123 RID: 291
		// (get) Token: 0x06000765 RID: 1893 RVA: 0x00039DE4 File Offset: 0x00038DE4
		public GeneralView KillAvatarCounterObjects
		{
			get
			{
				return this.killAvatarCounterObjects;
			}
		}

		// Token: 0x17000124 RID: 292
		// (get) Token: 0x06000766 RID: 1894 RVA: 0x00039DEC File Offset: 0x00038DEC
		public GeneralView Factions
		{
			get
			{
				return this.factions;
			}
		}

		// Token: 0x17000125 RID: 293
		// (get) Token: 0x06000767 RID: 1895 RVA: 0x00039DF4 File Offset: 0x00038DF4
		public GeneralView Currencies
		{
			get
			{
				return this.currencies;
			}
		}

		// Token: 0x06000768 RID: 1896 RVA: 0x00039DFC File Offset: 0x00038DFC
		public static void LoadZones(Dictionary<string, DBID> zones)
		{
			IDatabase mainDb = IDatabase.GetMainDatabase();
			if (mainDb != null)
			{
				List<DBID> dbidList = mainDb.GetObjectsList("gameMechanics.map.zone.ZoneResource");
				if (dbidList != null)
				{
					foreach (DBID dbid in dbidList)
					{
						if (!DBID.IsNullOrEmpty(dbid))
						{
							string zoneName = dbid.GetFileShortName();
							string key = dbid.ToString();
							int index = key.LastIndexOf('/');
							if (index > 0)
							{
								int index2 = key.LastIndexOf('/', index - 1);
								string folderName = key.Substring(index2 + 1, index - index2 - 1);
								if (string.Equals(folderName, zoneName, StringComparison.InvariantCultureIgnoreCase) && !zones.ContainsKey(folderName))
								{
									zones.Add(folderName, dbid);
								}
							}
						}
					}
				}
			}
		}

		// Token: 0x06000769 RID: 1897 RVA: 0x00039ECC File Offset: 0x00038ECC
		public static void LoadZones(List<string> zones)
		{
			Dictionary<string, DBID> _zones = new Dictionary<string, DBID>();
			QuestEnvironment.LoadZones(_zones);
			zones.AddRange(_zones.Keys);
		}

		// Token: 0x0600076A RID: 1898 RVA: 0x00039EF4 File Offset: 0x00038EF4
		public static void LoadZones(ComboBox ZoneComboBox, bool addEmpty)
		{
			ZoneComboBox.Items.Clear();
			if (addEmpty)
			{
				ZoneComboBox.Items.Add(string.Empty);
			}
			List<string> zones = new List<string>();
			QuestEnvironment.LoadZones(zones);
			ZoneComboBox.Items.AddRange(zones.ToArray());
		}

		// Token: 0x0600076B RID: 1899 RVA: 0x00039F40 File Offset: 0x00038F40
		public void LoadLoadedZones(ComboBox ZoneComboBox, bool addEmpty)
		{
			if (addEmpty)
			{
				ZoneComboBox.Items.Add(string.Empty);
			}
			foreach (string zone in this.loadedZones.Keys)
			{
				if (zone != "_OtherFolder")
				{
					ZoneComboBox.Items.Add(zone);
				}
			}
		}

		// Token: 0x0600076C RID: 1900 RVA: 0x00039FC0 File Offset: 0x00038FC0
		public static bool FromOtherZone(DBID dbid)
		{
			List<string> zones = new List<string>();
			QuestEnvironment.LoadZones(zones);
			foreach (string zone in zones)
			{
				if (dbid.ToString().Contains(zone))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x0600076D RID: 1901 RVA: 0x0003A028 File Offset: 0x00039028
		public GameObjectClass FindGameObject(Tools.DBGameObjects.View view, string key)
		{
			GameObjectClass gameObject = null;
			if (view != null && !string.IsNullOrEmpty(key))
			{
				gameObject = view.GetObjectByDBID(key);
				if (gameObject == null)
				{
					IDatabase mainDb = IDatabase.GetMainDatabase();
					if (mainDb != null)
					{
						string objectType = mainDb.GetClassTypeNameByFile(key);
						string[] types;
						if (this.GetTypesByView(view, out types))
						{
							foreach (string type in types)
							{
								if (objectType == type || DBMethods.TypeIsDerivedFrom(objectType, type))
								{
									DBID dbid = mainDb.GetDBIDByName(key);
									if (!DBID.IsNullOrEmpty(dbid))
									{
										IObjMan objMan = mainDb.GetManipulator(dbid);
										if (objMan != null)
										{
											gameObject = GameObjectFactory.CreateGameObject(type, objMan);
											view.AddObjectIfNotFinded(gameObject);
											break;
										}
									}
								}
							}
						}
					}
				}
			}
			return gameObject;
		}

		// Token: 0x17000126 RID: 294
		// (get) Token: 0x0600076E RID: 1902 RVA: 0x0003A0D8 File Offset: 0x000390D8
		public DbEventRegister DbEventRegister
		{
			get
			{
				return this.dbEventRegister;
			}
		}

		// Token: 0x0600076F RID: 1903 RVA: 0x0003A0E0 File Offset: 0x000390E0
		public void LoadAllQuestsWithoutOtherObjects(DBMethods.ObjectLoadedInView callBack)
		{
			DBMethods.LoadObjects("gameMechanics.constructor.schemes.quest.QuestResource", new List<Tools.DBGameObjects.View>(1)
			{
				this.quests
			}, null, true, false, callBack);
			foreach (GameObjectClass gameObjectClass in this.quests)
			{
				QuestClass quest = (QuestClass)gameObjectClass;
				this.RegisterQuestEvents(quest, true);
			}
		}

		// Token: 0x0400055C RID: 1372
		private const string zoneFolderName = "Zones";

		// Token: 0x0400055D RID: 1373
		public const string svnFolder = ".svn";

		// Token: 0x0400055E RID: 1374
		public const string otherZoneFolder = "_OtherFolder";

		// Token: 0x0400055F RID: 1375
		private readonly Dictionary<string, Predicate<DBID>> loadedZones = new Dictionary<string, Predicate<DBID>>();

		// Token: 0x04000560 RID: 1376
		private readonly GeneralView quests = new GeneralView("QuestQuestsView");

		// Token: 0x04000561 RID: 1377
		private readonly GeneralView items = new GeneralView("QuestItemsView");

		// Token: 0x04000562 RID: 1378
		private readonly GeneralView respawnableResources = new GeneralView("QuestMobsView");

		// Token: 0x04000563 RID: 1379
		private readonly GeneralView questGivers = new GeneralView("QuestGiversView");

		// Token: 0x04000564 RID: 1380
		private readonly GeneralView lootableObjects = new GeneralView("QuestLootableObjectsView");

		// Token: 0x04000565 RID: 1381
		private readonly GeneralView killAvatarCounterObjects = new GeneralView("KillAvatarCounterObjectsView");

		// Token: 0x04000566 RID: 1382
		private readonly GeneralView factions = new GeneralView("FactionsView");

		// Token: 0x04000567 RID: 1383
		private readonly GeneralView currencies = new GeneralView("CurrenciesView");

		// Token: 0x04000568 RID: 1384
		private readonly ImportExport importExport;

		// Token: 0x04000569 RID: 1385
		private readonly QuestEditor questEditor;

		// Token: 0x0400056E RID: 1390
		private readonly DbEventRegister dbEventRegister = new DbEventRegister();

		// Token: 0x0400056F RID: 1391
		private readonly DbEventsGenerator dbEventsGenerator;

		// Token: 0x0200009E RID: 158
		// (Invoke) Token: 0x06000771 RID: 1905
		public delegate void QuestChangedEvent(object sender, IEnumerable<string> quests);

		// Token: 0x0200009F RID: 159
		// (Invoke) Token: 0x06000775 RID: 1909
		public delegate void QuestDeletedEvent(object sender, string zone);

		// Token: 0x020000A0 RID: 160
		// (Invoke) Token: 0x06000779 RID: 1913
		public delegate void ObjectDeletingEvent(object sender, GameObjectClass deletingObject);

		// Token: 0x020000A1 RID: 161
		// (Invoke) Token: 0x0600077D RID: 1917
		public delegate void ZoneLoadedEvent(string zone);
	}
}
