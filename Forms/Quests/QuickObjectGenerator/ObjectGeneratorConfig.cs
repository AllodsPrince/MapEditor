using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using Db;
using MapEditor.Map;
using MapEditor.Resources.Strings;
using Tools.IconCache;

namespace MapEditor.Forms.Quests.QuickObjectGenerator
{
	// Token: 0x020001DE RID: 478
	public class ObjectGeneratorConfig
	{
		// Token: 0x06001849 RID: 6217 RVA: 0x000A238C File Offset: 0x000A138C
		public static int CheckDBIDPathExists(string filePathFromData, bool create)
		{
			filePathFromData.Replace('\\', '/');
			string[] pathArr = filePathFromData.Split(new char[]
			{
				'/'
			});
			if (pathArr.Length == 0)
			{
				return 0;
			}
			int result = 0;
			DirectoryInfo currentDir = new DirectoryInfo(EditorEnvironment.DataFolder.TrimEnd(new char[]
			{
				'/'
			}));
			int pathLen = pathArr.Length - 1;
			for (int index = 0; index < pathLen; index++)
			{
				DirectoryInfo[] subdir = currentDir.GetDirectories(pathArr[index]);
				if (subdir.Length > 0)
				{
					currentDir = subdir[0];
				}
				else
				{
					if (result == 0)
					{
						result = pathLen - index;
					}
					if (!create)
					{
						return result;
					}
					currentDir = currentDir.CreateSubdirectory(pathArr[index]);
				}
			}
			return result;
		}

		// Token: 0x0600184A RID: 6218 RVA: 0x000A242D File Offset: 0x000A142D
		public static bool CreateNewObjects(Form form, IDatabase mainDb, Dictionary<DBID, string> dbidDictionary, ref Dictionary<DBID, IObjMan> objManDictionary)
		{
			return ObjectGeneratorConfig.CreateNewObjects(form, mainDb, dbidDictionary, ref objManDictionary, 0);
		}

		// Token: 0x0600184B RID: 6219 RVA: 0x000A243C File Offset: 0x000A143C
		public static bool CreateNewObjects(Form form, IDatabase mainDb, Dictionary<DBID, string> dbidDictionary, ref Dictionary<DBID, IObjMan> objManDictionary, int messCreatePathLevel)
		{
			if (mainDb == null)
			{
				return false;
			}
			foreach (DBID dbid in dbidDictionary.Keys)
			{
				if (dbid.IsEmpty() || mainDb.DoesObjectExist(dbid))
				{
					MessageBox.Show(form, string.Format(Strings.QUEST_EDITOR_CANT_CREATE_OBJECT_ERROR, dbid), Strings.QUEST_EDITOR_ERROR_TITLE, MessageBoxButtons.OK, MessageBoxIcon.Hand);
					return false;
				}
			}
			foreach (KeyValuePair<DBID, string> pair in dbidDictionary)
			{
				DBID dbid2 = pair.Key;
				IObjMan objMan = mainDb.CreateNewObject(pair.Value);
				if (objMan == null)
				{
					MessageBox.Show(form, string.Format(Strings.QUEST_EDITOR_CANT_CREATE_OBJECT_ERROR, dbid2), Strings.QUEST_EDITOR_ERROR_TITLE, MessageBoxButtons.OK, MessageBoxIcon.Hand);
					return false;
				}
				mainDb.AddNewObject(dbid2, objMan);
				if (ObjectGeneratorConfig.CheckDBIDPathExists(dbid2.ToString(), true) > messCreatePathLevel)
				{
					MessageBox.Show(form, string.Format(Strings.QUEST_EDITOR_NOT_EXISTING_PATH_WARNING, dbid2), Strings.QUEST_EDITOR_TITLE, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				}
				objManDictionary[dbid2] = objMan;
			}
			return true;
		}

		// Token: 0x0600184C RID: 6220 RVA: 0x000A2574 File Offset: 0x000A1574
		public static string GetSpawnTableDBID(string map, string zone, string fileName)
		{
			List<string> continentNameList = new List<string>();
			Constants.GetContinentNameList(ref continentNameList);
			foreach (string continentName in continentNameList)
			{
				if (continentName == map)
				{
					return string.Format("{0}/{1}/{2}/{3}.({4}).xdb", new object[]
					{
						continentName,
						"SpawnTables",
						zone,
						fileName,
						"MobSpawnTable"
					});
				}
			}
			return string.Empty;
		}

		// Token: 0x170005E2 RID: 1506
		// (get) Token: 0x0600184D RID: 6221 RVA: 0x000A2614 File Offset: 0x000A1614
		public static IconCache IconCache
		{
			get
			{
				return ObjectGeneratorConfig.iconCache;
			}
		}

		// Token: 0x0600184E RID: 6222 RVA: 0x000A261C File Offset: 0x000A161C
		public static void AddToSpawbTable(IObjMan stMan, DBID mobDBID, string field, string timeType)
		{
			int cnt;
			stMan.GetValue(field, out cnt);
			stMan.Insert(field, cnt, 1, true);
			stMan.SetValue(string.Format("{0}.[{1}].object", field, cnt), mobDBID);
			stMan.SetStructPtrInstance(string.Format("{0}.[{1}].spawnTime", field, cnt), timeType);
			cnt++;
			float chance = 1f / (float)cnt;
			for (int index = 0; index < cnt; index++)
			{
				stMan.SetValue(string.Format("{0}.[{1}].chance", field, index), chance);
			}
		}

		// Token: 0x04000FA8 RID: 4008
		public const string spawnTableType = "gameMechanics.map.spawn.SpawnTable";

		// Token: 0x04000FA9 RID: 4009
		public const string commonSpawnTime = "gameMechanics.elements.spawn.TimeCommon";

		// Token: 0x04000FAA RID: 4010
		public const string trashSpawnTime = "gameMechanics.elements.spawn.TimeTrash";

		// Token: 0x04000FAB RID: 4011
		public const string visualMobType = "VisualMob";

		// Token: 0x04000FAC RID: 4012
		public const string visualObjType = "VisObjectTemplate";

		// Token: 0x04000FAD RID: 4013
		public const string CollisionType = "Collision";

		// Token: 0x04000FAE RID: 4014
		public const string chestType = "gameMechanics.world.device.ChestResource";

		// Token: 0x04000FAF RID: 4015
		public const string steleType = "gameMechanics.world.device.SteleResource";

		// Token: 0x04000FB0 RID: 4016
		public const string exploitType = "gameMechanics.constructor.schemes.exploit.Exploit";

		// Token: 0x04000FB1 RID: 4017
		public const string targetImpactDeviceDieType = "gameMechanics.elements.device.DeviceDie";

		// Token: 0x04000FB2 RID: 4018
		public const string targetImpactDeviceOpenLootBag = "gameMechanics.elements.device.DeviceOpenLootBag";

		// Token: 0x04000FB3 RID: 4019
		public const string kindMobType = "gameMechanics.world.mob.MobKind";

		// Token: 0x04000FB4 RID: 4020
		public const string factionType = "gameMechanics.world.creature.Faction";

		// Token: 0x04000FB5 RID: 4021
		public const string instanceFolderName = "Instances";

		// Token: 0x04000FB6 RID: 4022
		public const string spawnTableFolderName = "SpawnTables";

		// Token: 0x04000FB7 RID: 4023
		public const string shortMobType = "MobWorld";

		// Token: 0x04000FB8 RID: 4024
		public const string shortSpawnTableType = "MobSpawnTable";

		// Token: 0x04000FB9 RID: 4025
		public const string shortChestType = "ChestResource";

		// Token: 0x04000FBA RID: 4026
		public const string shortSteleType = "SteleResource";

		// Token: 0x04000FBB RID: 4027
		public const string shortExploitType = "Exploit";

		// Token: 0x04000FBC RID: 4028
		public const string shortItemType = "ItemResource";

		// Token: 0x04000FBD RID: 4029
		public const string mechanicsFolder = "Mechanics";

		// Token: 0x04000FBE RID: 4030
		private static readonly UISTextureIconDataMiner dataMiner = new UISTextureIconDataMiner();

		// Token: 0x04000FBF RID: 4031
		public static string npcConfigPath = EditorEnvironment.EditorFolder + "Quests/ObjectGeneratorConfig/NPCGeneratorConfig.xml";

		// Token: 0x04000FC0 RID: 4032
		public static string mobConfigPath = EditorEnvironment.EditorFolder + "Quests/ObjectGeneratorConfig/MobGeneratorConfig.xml";

		// Token: 0x04000FC1 RID: 4033
		public static string resourceConfigPath = EditorEnvironment.EditorFolder + "Quests/ObjectGeneratorConfig/ResourceGeneratorConfig.xml";

		// Token: 0x04000FC2 RID: 4034
		public static string questItemConfigPath = EditorEnvironment.EditorFolder + "Quests/ObjectGeneratorConfig/QuestItemGeneratorConfig.xml";

		// Token: 0x04000FC3 RID: 4035
		private static readonly IconCache iconCache = new IconCache(ObjectGeneratorConfig.dataMiner, EditorEnvironment.EditorFormsFolder + "ItemIconCache.bin");

		// Token: 0x020001DF RID: 479
		public struct StringPair
		{
			// Token: 0x04000FC4 RID: 4036
			public string first;

			// Token: 0x04000FC5 RID: 4037
			public string second;
		}

		// Token: 0x020001E0 RID: 480
		public struct StringListPair
		{
			// Token: 0x04000FC6 RID: 4038
			public string key;

			// Token: 0x04000FC7 RID: 4039
			public List<string> values;
		}

		// Token: 0x020001E1 RID: 481
		public class NPCGeneratorConfig
		{
			// Token: 0x170005E3 RID: 1507
			// (get) Token: 0x06001851 RID: 6225 RVA: 0x000A272D File Offset: 0x000A172D
			// (set) Token: 0x06001852 RID: 6226 RVA: 0x000A2735 File Offset: 0x000A1735
			public List<ObjectGeneratorConfig.StringPair> MobKindList
			{
				get
				{
					return this.mobKindList;
				}
				set
				{
					this.mobKindList = value;
				}
			}

			// Token: 0x170005E4 RID: 1508
			// (get) Token: 0x06001853 RID: 6227 RVA: 0x000A273E File Offset: 0x000A173E
			// (set) Token: 0x06001854 RID: 6228 RVA: 0x000A2746 File Offset: 0x000A1746
			public string Quality
			{
				get
				{
					return this.quality;
				}
				set
				{
					this.quality = value;
				}
			}

			// Token: 0x04000FC8 RID: 4040
			private List<ObjectGeneratorConfig.StringPair> mobKindList;

			// Token: 0x04000FC9 RID: 4041
			private string quality;
		}

		// Token: 0x020001E2 RID: 482
		public class MobGeneratorConfig
		{
			// Token: 0x170005E5 RID: 1509
			// (get) Token: 0x06001856 RID: 6230 RVA: 0x000A2757 File Offset: 0x000A1757
			// (set) Token: 0x06001857 RID: 6231 RVA: 0x000A275F File Offset: 0x000A175F
			public List<ObjectGeneratorConfig.StringListPair> ZoneCreatureMap
			{
				get
				{
					return this.zoneCreatureMap;
				}
				set
				{
					this.zoneCreatureMap = value;
				}
			}

			// Token: 0x170005E6 RID: 1510
			// (get) Token: 0x06001858 RID: 6232 RVA: 0x000A2768 File Offset: 0x000A1768
			// (set) Token: 0x06001859 RID: 6233 RVA: 0x000A2770 File Offset: 0x000A1770
			public string MobKindSuffix
			{
				get
				{
					return this.mobKindSuffix;
				}
				set
				{
					this.mobKindSuffix = value;
				}
			}

			// Token: 0x170005E7 RID: 1511
			// (get) Token: 0x0600185A RID: 6234 RVA: 0x000A2779 File Offset: 0x000A1779
			// (set) Token: 0x0600185B RID: 6235 RVA: 0x000A2781 File Offset: 0x000A1781
			public string SilverMobKindSuffix
			{
				get
				{
					return this.silverMobKindSuffix;
				}
				set
				{
					this.silverMobKindSuffix = value;
				}
			}

			// Token: 0x170005E8 RID: 1512
			// (get) Token: 0x0600185C RID: 6236 RVA: 0x000A278A File Offset: 0x000A178A
			// (set) Token: 0x0600185D RID: 6237 RVA: 0x000A2792 File Offset: 0x000A1792
			public string MinibossMobKindSuffix
			{
				get
				{
					return this.minibossMobKindSuffix;
				}
				set
				{
					this.minibossMobKindSuffix = value;
				}
			}

			// Token: 0x170005E9 RID: 1513
			// (get) Token: 0x0600185E RID: 6238 RVA: 0x000A279B File Offset: 0x000A179B
			// (set) Token: 0x0600185F RID: 6239 RVA: 0x000A27A3 File Offset: 0x000A17A3
			public string NeutralFraction
			{
				get
				{
					return this.neutralFraction;
				}
				set
				{
					this.neutralFraction = value;
				}
			}

			// Token: 0x170005EA RID: 1514
			// (get) Token: 0x06001860 RID: 6240 RVA: 0x000A27AC File Offset: 0x000A17AC
			// (set) Token: 0x06001861 RID: 6241 RVA: 0x000A27B4 File Offset: 0x000A17B4
			public string WildFraction
			{
				get
				{
					return this.wildFraction;
				}
				set
				{
					this.wildFraction = value;
				}
			}

			// Token: 0x170005EB RID: 1515
			// (get) Token: 0x06001862 RID: 6242 RVA: 0x000A27BD File Offset: 0x000A17BD
			// (set) Token: 0x06001863 RID: 6243 RVA: 0x000A27C5 File Offset: 0x000A17C5
			public string MobQuality
			{
				get
				{
					return this.mobQuality;
				}
				set
				{
					this.mobQuality = value;
				}
			}

			// Token: 0x170005EC RID: 1516
			// (get) Token: 0x06001864 RID: 6244 RVA: 0x000A27CE File Offset: 0x000A17CE
			// (set) Token: 0x06001865 RID: 6245 RVA: 0x000A27D6 File Offset: 0x000A17D6
			public string SilverMobQuality
			{
				get
				{
					return this.silverMobQuality;
				}
				set
				{
					this.silverMobQuality = value;
				}
			}

			// Token: 0x170005ED RID: 1517
			// (get) Token: 0x06001866 RID: 6246 RVA: 0x000A27DF File Offset: 0x000A17DF
			// (set) Token: 0x06001867 RID: 6247 RVA: 0x000A27E7 File Offset: 0x000A17E7
			public string MinibossMobQuality
			{
				get
				{
					return this.minibossMobQuality;
				}
				set
				{
					this.minibossMobQuality = value;
				}
			}

			// Token: 0x04000FCA RID: 4042
			private List<ObjectGeneratorConfig.StringListPair> zoneCreatureMap;

			// Token: 0x04000FCB RID: 4043
			private string mobKindSuffix;

			// Token: 0x04000FCC RID: 4044
			private string silverMobKindSuffix;

			// Token: 0x04000FCD RID: 4045
			private string minibossMobKindSuffix;

			// Token: 0x04000FCE RID: 4046
			private string neutralFraction;

			// Token: 0x04000FCF RID: 4047
			private string wildFraction;

			// Token: 0x04000FD0 RID: 4048
			private string mobQuality;

			// Token: 0x04000FD1 RID: 4049
			private string silverMobQuality;

			// Token: 0x04000FD2 RID: 4050
			private string minibossMobQuality;
		}

		// Token: 0x020001E3 RID: 483
		public class ResourceGeneratorConfig
		{
			// Token: 0x170005EE RID: 1518
			// (get) Token: 0x06001869 RID: 6249 RVA: 0x000A27F8 File Offset: 0x000A17F8
			// (set) Token: 0x0600186A RID: 6250 RVA: 0x000A2800 File Offset: 0x000A1800
			public int PrepareDuration
			{
				get
				{
					return this.prepareDuration;
				}
				set
				{
					this.prepareDuration = value;
				}
			}

			// Token: 0x170005EF RID: 1519
			// (get) Token: 0x0600186B RID: 6251 RVA: 0x000A2809 File Offset: 0x000A1809
			// (set) Token: 0x0600186C RID: 6252 RVA: 0x000A2811 File Offset: 0x000A1811
			public long CorpseDuration
			{
				get
				{
					return this.corpseDuration;
				}
				set
				{
					this.corpseDuration = value;
				}
			}

			// Token: 0x04000FD3 RID: 4051
			private int prepareDuration;

			// Token: 0x04000FD4 RID: 4052
			private long corpseDuration;
		}

		// Token: 0x020001E4 RID: 484
		public class QuestItemGeneratorConfig
		{
			// Token: 0x170005F0 RID: 1520
			// (get) Token: 0x0600186E RID: 6254 RVA: 0x000A2822 File Offset: 0x000A1822
			// (set) Token: 0x0600186F RID: 6255 RVA: 0x000A282A File Offset: 0x000A182A
			public int BudgetUsage
			{
				get
				{
					return this.budgetUsage;
				}
				set
				{
					this.budgetUsage = value;
				}
			}

			// Token: 0x170005F1 RID: 1521
			// (get) Token: 0x06001870 RID: 6256 RVA: 0x000A2833 File Offset: 0x000A1833
			// (set) Token: 0x06001871 RID: 6257 RVA: 0x000A283B File Offset: 0x000A183B
			public string Quality
			{
				get
				{
					return this.quality;
				}
				set
				{
					this.quality = value;
				}
			}

			// Token: 0x170005F2 RID: 1522
			// (get) Token: 0x06001872 RID: 6258 RVA: 0x000A2844 File Offset: 0x000A1844
			// (set) Token: 0x06001873 RID: 6259 RVA: 0x000A284C File Offset: 0x000A184C
			public string ItemClass
			{
				get
				{
					return this.itemClass;
				}
				set
				{
					this.itemClass = value;
				}
			}

			// Token: 0x04000FD5 RID: 4053
			private int budgetUsage;

			// Token: 0x04000FD6 RID: 4054
			private string quality;

			// Token: 0x04000FD7 RID: 4055
			private string itemClass;
		}
	}
}
