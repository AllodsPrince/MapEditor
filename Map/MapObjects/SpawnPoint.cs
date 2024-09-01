using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using Db;
using MapEditor.Map.MapObjectElements;
using MapEditor.Resources.Strings;
using Tools.DBGameObjects;
using Tools.Geometry;
using Tools.MapObjects;
using Tools.SafeObjMan;

namespace MapEditor.Map.MapObjects
{
	// Token: 0x02000141 RID: 321
	public class SpawnPoint : MapObject, IMapObjectInterfaceExtention, SpawnTimeOwner, SingleSpawnControllerOwner
	{
		// Token: 0x14000048 RID: 72
		// (add) Token: 0x06000F4D RID: 3917 RVA: 0x00078BB7 File Offset: 0x00077BB7
		// (remove) Token: 0x06000F4E RID: 3918 RVA: 0x00078BCE File Offset: 0x00077BCE
		public static event SpawnPoint.SpawnPointFieldChangedEvent<SpawnPointData> TypeChanged;

		// Token: 0x14000049 RID: 73
		// (add) Token: 0x06000F4F RID: 3919 RVA: 0x00078BE5 File Offset: 0x00077BE5
		// (remove) Token: 0x06000F50 RID: 3920 RVA: 0x00078BFC File Offset: 0x00077BFC
		public static event SpawnPoint.SpawnPointFieldChangedEvent<SpawnPointData> DataChanged;

		// Token: 0x1400004A RID: 74
		// (add) Token: 0x06000F51 RID: 3921 RVA: 0x00078C13 File Offset: 0x00077C13
		// (remove) Token: 0x06000F52 RID: 3922 RVA: 0x00078C2A File Offset: 0x00077C2A
		public static event SpawnPoint.SpawnPointFieldChangedEvent<string> SpawnTableChanged;

		// Token: 0x1400004B RID: 75
		// (add) Token: 0x06000F53 RID: 3923 RVA: 0x00078C41 File Offset: 0x00077C41
		// (remove) Token: 0x06000F54 RID: 3924 RVA: 0x00078C58 File Offset: 0x00077C58
		public static event SpawnPoint.SpawnPointFieldChangedEvent<string> ScriptIDChanged;

		// Token: 0x1400004C RID: 76
		// (add) Token: 0x06000F55 RID: 3925 RVA: 0x00078C6F File Offset: 0x00077C6F
		// (remove) Token: 0x06000F56 RID: 3926 RVA: 0x00078C86 File Offset: 0x00077C86
		public static event SpawnPoint.SpawnPointFieldChangedEvent<string> SpawnIDChanged;

		// Token: 0x1400004D RID: 77
		// (add) Token: 0x06000F57 RID: 3927 RVA: 0x00078C9D File Offset: 0x00077C9D
		// (remove) Token: 0x06000F58 RID: 3928 RVA: 0x00078CB4 File Offset: 0x00077CB4
		public static event SpawnPoint.SpawnPointFieldChangedEvent<double> ScanRadiusChanged;

		// Token: 0x1400004E RID: 78
		// (add) Token: 0x06000F59 RID: 3929 RVA: 0x00078CCB File Offset: 0x00077CCB
		// (remove) Token: 0x06000F5A RID: 3930 RVA: 0x00078CE2 File Offset: 0x00077CE2
		public static event SpawnPoint.SpawnPointFieldChangedEvent<SpawnTimeAbstract> SpawnTimeChanged;

		// Token: 0x1400004F RID: 79
		// (add) Token: 0x06000F5B RID: 3931 RVA: 0x00078CF9 File Offset: 0x00077CF9
		// (remove) Token: 0x06000F5C RID: 3932 RVA: 0x00078D10 File Offset: 0x00077D10
		public static event SpawnPoint.SpawnPointFieldChangedEvent<string> SpawnTunerChanged;

		// Token: 0x14000050 RID: 80
		// (add) Token: 0x06000F5D RID: 3933 RVA: 0x00078D27 File Offset: 0x00077D27
		// (remove) Token: 0x06000F5E RID: 3934 RVA: 0x00078D3E File Offset: 0x00077D3E
		public static event SpawnPoint.SpawnPointFieldChangedEvent<string> MobSceneNameChanged;

		// Token: 0x14000051 RID: 81
		// (add) Token: 0x06000F5F RID: 3935 RVA: 0x00078D55 File Offset: 0x00077D55
		// (remove) Token: 0x06000F60 RID: 3936 RVA: 0x00078D6C File Offset: 0x00077D6C
		public static event SpawnPoint.SpawnPointFieldChangedEvent<SpawnTableType> SpawnTableTypeChanged;

		// Token: 0x14000052 RID: 82
		// (add) Token: 0x06000F61 RID: 3937 RVA: 0x00078D83 File Offset: 0x00077D83
		// (remove) Token: 0x06000F62 RID: 3938 RVA: 0x00078D9A File Offset: 0x00077D9A
		public static event SpawnPoint.SpawnPointFieldChangedEvent<double> AggroRadiusChanged;

		// Token: 0x14000053 RID: 83
		// (add) Token: 0x06000F63 RID: 3939 RVA: 0x00078DB1 File Offset: 0x00077DB1
		// (remove) Token: 0x06000F64 RID: 3940 RVA: 0x00078DC8 File Offset: 0x00077DC8
		public static event SpawnPoint.SpawnPointFieldChangedEvent<SingleSpawnController> SingleSpawnControllerChanged;

		// Token: 0x06000F65 RID: 3941 RVA: 0x00078DE0 File Offset: 0x00077DE0
		private static void ExtractVisualDataFromSelectedIndexInSpawnTable(IDatabase mainDb, IObjMan spawnTableMan, string classTypeName, string section, int selectedIndex, out string sceneName, out double aggroRadius)
		{
			if (!string.IsNullOrEmpty(section) && selectedIndex != -1)
			{
				string mob = SafeObjMan.GetDBID(spawnTableMan, string.Format("{0}.[{1}].object", section, selectedIndex));
				IObjMan mobMan = mainDb.GetManipulator(mainDb.GetDBIDByName(mob));
				if (mobMan != null)
				{
					if (mobMan.GetFieldDesc("visMob") != null)
					{
						string visMob = SafeObjMan.GetDBID(mobMan, "visMob");
						if (!string.IsNullOrEmpty(visMob))
						{
							sceneName = visMob;
						}
						else
						{
							sceneName = "Editor/Map/SpecialObjects/SpawnPoint/SpawnPoint.(StaticObject).xdb";
						}
						if (string.Equals(classTypeName, "gameMechanics.world.astralMob.AstralMobWorld"))
						{
							aggroRadius = 1000.0;
							return;
						}
						aggroRadius = 0.0;
						bool isPassive = true;
						IObjMan factionMan = mainDb.GetManipulator(mainDb.GetDBIDByName(SafeObjMan.GetDBID(mobMan, "faction")));
						if (factionMan != null)
						{
							isPassive = (SafeObjMan.GetInt(factionMan, "defaultReputation") >= 0);
						}
						if (!isPassive)
						{
							double _aggroRadius = SafeObjMan.GetDouble(mobMan, "aggroRadius");
							if (_aggroRadius > MathConsts.DOUBLE_EPSILON)
							{
								aggroRadius = _aggroRadius;
								return;
							}
						}
					}
					else
					{
						string visObj = mob;
						if (!string.IsNullOrEmpty(visObj))
						{
							sceneName = visObj;
						}
						else
						{
							sceneName = "Editor/Map/SpecialObjects/SpawnPoint/SpawnPoint.(StaticObject).xdb";
						}
						aggroRadius = 0.0;
					}
					return;
				}
			}
			sceneName = "Editor/Map/SpecialObjects/SpawnPoint/SpawnPoint.(StaticObject).xdb";
			aggroRadius = 0.0;
		}

		// Token: 0x06000F66 RID: 3942 RVA: 0x00078F14 File Offset: 0x00077F14
		private static void ExtractVisualData(string _spawnTable, string spawnID, out string sceneName, out double aggroRadius)
		{
			if (!string.IsNullOrEmpty(_spawnTable))
			{
				IDatabase mainDb = IDatabase.GetMainDatabase();
				if (mainDb != null)
				{
					DBID spawnTableDBID = mainDb.GetDBIDByName(_spawnTable);
					if (!spawnTableDBID.IsEmpty())
					{
						string classTypeName = mainDb.GetClassTypeName(spawnTableDBID);
						if (classTypeName == StaticObject.DBType || classTypeName == "gameMechanics.world.astralWreck.AstralWreck" || classTypeName == "gameMechanics.world.astralTeleports.SphericalTeleport")
						{
							sceneName = _spawnTable;
							aggroRadius = 0.0;
							return;
						}
						if (classTypeName == "gameMechanics.map.spawn.SpawnTable")
						{
							IObjMan spawnTableMan = mainDb.GetManipulator(spawnTableDBID);
							int selectedIndex = -1;
							if (spawnTableMan != null)
							{
								string[] sections = new string[]
								{
									"commons",
									"singles"
								};
								string section = null;
								float maxChance = 0f;
								foreach (string _section in sections)
								{
									int mobCount = SafeObjMan.GetInt(spawnTableMan, _section);
									if (mobCount > 0)
									{
										for (int i = 0; i < mobCount; i++)
										{
											string _spawnID = SafeObjMan.GetString(spawnTableMan, string.Format("{0}.[{1}].spawnId", _section, i));
											if (!string.IsNullOrEmpty(spawnID) && string.Equals(_spawnID, spawnID, StringComparison.OrdinalIgnoreCase))
											{
												SpawnPoint.ExtractVisualDataFromSelectedIndexInSpawnTable(mainDb, spawnTableMan, classTypeName, _section, i, out sceneName, out aggroRadius);
												return;
											}
											float _chance = SafeObjMan.GetFloat(spawnTableMan, string.Format("{0}.[{1}].chance", _section, i));
											if (_chance > maxChance)
											{
												maxChance = _chance;
												selectedIndex = i;
												section = _section;
											}
										}
									}
								}
								SpawnPoint.ExtractVisualDataFromSelectedIndexInSpawnTable(mainDb, spawnTableMan, classTypeName, section, selectedIndex, out sceneName, out aggroRadius);
								return;
							}
						}
						else
						{
							IObjMan spawnTableMan2 = mainDb.GetManipulator(spawnTableDBID);
							if (spawnTableMan2 != null)
							{
								if (spawnTableMan2.GetFieldDesc("visMob") != null)
								{
									string visMob = SafeObjMan.GetDBID(spawnTableMan2, "visMob");
									if (!string.IsNullOrEmpty(visMob))
									{
										sceneName = visMob;
									}
									else
									{
										sceneName = "Editor/Map/SpecialObjects/SpawnPoint/SpawnPoint.(StaticObject).xdb";
									}
									if (string.Equals(classTypeName, "gameMechanics.world.astralMob.AstralMobWorld"))
									{
										aggroRadius = 1000.0;
										return;
									}
									aggroRadius = 0.0;
									bool isPassive = true;
									IObjMan factionMan = mainDb.GetManipulator(mainDb.GetDBIDByName(SafeObjMan.GetDBID(spawnTableMan2, "faction")));
									if (factionMan != null)
									{
										isPassive = (SafeObjMan.GetInt(factionMan, "defaultReputation") >= 0);
									}
									if (!isPassive)
									{
										double _aggroRadius = SafeObjMan.GetDouble(spawnTableMan2, "aggroRadius");
										if (_aggroRadius > MathConsts.DOUBLE_EPSILON)
										{
											aggroRadius = _aggroRadius;
											return;
										}
									}
								}
								else
								{
									if (!string.IsNullOrEmpty(_spawnTable))
									{
										sceneName = _spawnTable;
									}
									else
									{
										sceneName = "Editor/Map/SpecialObjects/SpawnPoint/SpawnPoint.(StaticObject).xdb";
									}
									aggroRadius = 0.0;
								}
								return;
							}
						}
					}
				}
			}
			sceneName = "Editor/Map/SpecialObjects/SpawnPoint/SpawnPoint.(StaticObject).xdb";
			aggroRadius = 0.0;
		}

		// Token: 0x06000F67 RID: 3943 RVA: 0x00079185 File Offset: 0x00078185
		private static bool SingleSpawnControllerEnabled(SpawnTableType tableType)
		{
			return tableType == SpawnTableType.SingleMob || tableType == SpawnTableType.SingleDevice;
		}

		// Token: 0x06000F68 RID: 3944 RVA: 0x00079194 File Offset: 0x00078194
		private void CheckSpawnTableType()
		{
			SpawnTableType newSpawnTableType = SpawnPoint.GetSpawnTableType(this.spawnTable, this.spawnTableType);
			if (this.spawnTableType != newSpawnTableType)
			{
				SpawnTableType oldSpawnTableType = this.spawnTableType;
				this.spawnTableType = newSpawnTableType;
				bool activeBackup = base.Active;
				base.Active = false;
				if (SpawnPoint.SpawnTableTypeChanged != null)
				{
					SpawnPoint.SpawnTableTypeChanged(this, ref oldSpawnTableType, ref newSpawnTableType);
				}
				base.Active = activeBackup;
				bool oldSingleSpawnEnabled = SpawnPoint.SingleSpawnControllerEnabled(oldSpawnTableType);
				bool newSingleSpawnEnabled = SpawnPoint.SingleSpawnControllerEnabled(newSpawnTableType);
				if (oldSingleSpawnEnabled && !newSingleSpawnEnabled)
				{
					this.singleSpawnController = null;
				}
			}
		}

		// Token: 0x06000F69 RID: 3945 RVA: 0x00079214 File Offset: 0x00078214
		public static SpawnTableType GetSpawnTableType(string _spawnTable, SpawnTableType suggestedSpawnTableType)
		{
			IDatabase mainDb = IDatabase.GetMainDatabase();
			if (mainDb != null)
			{
				string classTypeName = mainDb.GetClassTypeNameByFile(_spawnTable);
				if (!string.IsNullOrEmpty(classTypeName))
				{
					if (classTypeName == "gameMechanics.map.spawn.SpawnTable")
					{
						return SpawnTableType.Table;
					}
					if (classTypeName == "gameMechanics.world.mob.MobWorld")
					{
						return SpawnTableType.SingleMob;
					}
					if (classTypeName == "gameMechanics.world.astralMob.AstralMobWorld")
					{
						return SpawnTableType.AstralMob;
					}
					if (classTypeName == "gameMechanics.world.astralWreck.AstralWreck")
					{
						return SpawnTableType.AstralWreck;
					}
					if (classTypeName == "gameMechanics.world.astralTeleports.SphericalTeleport")
					{
						return SpawnTableType.AstralTeleport;
					}
					if (!SpawnPoint.devicesDBTypesFilled)
					{
						SpawnPoint.devicesDBTypes = mainDb.GetDerivedXDBClassTypes("gameMechanics.world.device.DeviceResource");
						SpawnPoint.devicesDBTypesFilled = true;
					}
					if (SpawnPoint.devicesDBTypes != null && SpawnPoint.devicesDBTypes.Length > 0)
					{
						for (int index = 0; index < SpawnPoint.devicesDBTypes.Length; index++)
						{
							if (classTypeName == SpawnPoint.devicesDBTypes[index])
							{
								return SpawnTableType.SingleDevice;
							}
						}
					}
				}
			}
			return suggestedSpawnTableType;
		}

		// Token: 0x06000F6A RID: 3946 RVA: 0x000792E0 File Offset: 0x000782E0
		public static void LocateSpawnPoints(ICollection<KeyValuePair<IMapObject, IMapObject>> mapObjects, ICollection<SpawnPoint> singleMobSpawnPoints, ICollection<SpawnPoint> spawnTableSpawnPoints, IDictionary<string, int> spawnTables)
		{
			if (mapObjects != null && mapObjects.Count > 0)
			{
				foreach (KeyValuePair<IMapObject, IMapObject> keyValuePair in mapObjects)
				{
					if (keyValuePair.Key != null && keyValuePair.Key.Type.Type == MapObjectFactory.Type.SpawnPoint)
					{
						SpawnPoint spawnPoint = keyValuePair.Key as SpawnPoint;
						if (spawnPoint != null && !string.IsNullOrEmpty(spawnPoint.SpawnTable))
						{
							if (spawnPoint.SpawnTableType == SpawnTableType.SingleMob)
							{
								if (singleMobSpawnPoints != null)
								{
									singleMobSpawnPoints.Add(spawnPoint);
								}
							}
							else if (spawnPoint.SpawnTableType == SpawnTableType.Table)
							{
								if (spawnTableSpawnPoints != null)
								{
									spawnTableSpawnPoints.Add(spawnPoint);
								}
								if (spawnTables != null)
								{
									if (!spawnTables.ContainsKey(spawnPoint.SpawnTable))
									{
										spawnTables.Add(spawnPoint.SpawnTable, 1);
									}
									else
									{
										string key;
										spawnTables[key = spawnPoint.SpawnTable] = spawnTables[key] + 1;
									}
								}
							}
						}
					}
				}
			}
		}

		// Token: 0x06000F6B RID: 3947 RVA: 0x000793E8 File Offset: 0x000783E8
		public static string LocateSpawnTable(ICollection<KeyValuePair<string, int>> spawnTables, IDatabase mainDb)
		{
			if (spawnTables != null && spawnTables.Count == 1 && mainDb != null)
			{
				using (IEnumerator<KeyValuePair<string, int>> enumerator = spawnTables.GetEnumerator())
				{
					if (enumerator.MoveNext())
					{
						KeyValuePair<string, int> keyValuePair = enumerator.Current;
						string spawnTable = keyValuePair.Key;
						DBID spawnTableDBID = mainDb.GetDBIDByName(spawnTable);
						if (!DBID.IsNullOrEmpty(spawnTableDBID))
						{
							IObjMan spawnTableMan = mainDb.GetManipulator(spawnTableDBID);
							if (spawnTableMan != null)
							{
								bool aggroShare = false;
								bool groupRestoreOutOfCombat = false;
								int count = SafeObjMan.GetInt(spawnTableMan, "controllers");
								for (int index = 0; index < count; index++)
								{
									string fieldName = string.Format("controllers.[{0}]", index);
									if (spawnTableMan.IsStructPtrInstanceCompatible(fieldName, "gameMechanics.elements.spawn.AggroShare"))
									{
										aggroShare = true;
									}
									else if (spawnTableMan.IsStructPtrInstanceCompatible(fieldName, "gameMechanics.elements.spawn.GroupRestoreOutOfCombat"))
									{
										groupRestoreOutOfCombat = true;
									}
								}
								if (aggroShare && groupRestoreOutOfCombat)
								{
									return spawnTable;
								}
							}
						}
					}
				}
			}
			return string.Empty;
		}

		// Token: 0x06000F6C RID: 3948 RVA: 0x000794E4 File Offset: 0x000784E4
		public static string GetSpawnTunerType(SpawnPoint spawnPoint)
		{
			string type = string.Empty;
			if (spawnPoint != null)
			{
				if (spawnPoint.SpawnTableType == SpawnTableType.SingleDevice)
				{
					type = SpawnPoint.DeviceSpawnTunerDBType;
				}
				else if (spawnPoint.SpawnTableType == SpawnTableType.SingleMob)
				{
					type = SpawnPoint.MobSpawnTunerDBType;
				}
				else if (spawnPoint.SpawnTableType == SpawnTableType.Table)
				{
					IDatabase mainDb = IDatabase.GetMainDatabase();
					if (mainDb != null)
					{
						type = (DBMethods.TypeIsDerivedFrom(mainDb.GetClassTypeNameByFile(spawnPoint.SceneName), SpawnPoint.DeviceDBType) ? SpawnPoint.DeviceSpawnTunerDBType : SpawnPoint.MobSpawnTunerDBType);
					}
				}
			}
			return type;
		}

		// Token: 0x06000F6D RID: 3949 RVA: 0x00079554 File Offset: 0x00078554
		public static string GetFixedIdleAnimationName(SpawnPoint spawnPoint, IDatabase mainDb)
		{
			if (spawnPoint != null && (spawnPoint.spawnTableType == SpawnTableType.AstralMob || spawnPoint.spawnTableType == SpawnTableType.SingleMob || spawnPoint.spawnTableType == SpawnTableType.Table))
			{
				DBID visualObjectDBID = mainDb.GetDBIDByName(spawnPoint.SceneName);
				IObjMan visMobMan = mainDb.GetManipulator(visualObjectDBID);
				if (visMobMan != null & string.Equals(mainDb.GetClassTypeName(visualObjectDBID), "VisualMob"))
				{
					string animation = string.Empty;
					if (!string.IsNullOrEmpty(spawnPoint.SpawnTuner))
					{
						int visualStateID = -1;
						IObjMan spawnTunerMan = mainDb.GetManipulator(mainDb.GetDBIDByName(spawnPoint.SpawnTuner));
						if (spawnTunerMan != null)
						{
							int impactCount = SafeObjMan.GetInt(spawnTunerMan, "impacts");
							for (int impactIndex = 0; impactIndex < impactCount; impactIndex++)
							{
								string filedPrefix = string.Format("impacts.[{0}]", impactIndex);
								string baseTypeName;
								string typeName;
								spawnTunerMan.IsStructPtr(filedPrefix, out baseTypeName, out typeName);
								if (string.Equals(typeName, "gameMechanics.elements.impacts.ImpactSetVisualState"))
								{
									visualStateID = SafeObjMan.GetInt(spawnTunerMan, filedPrefix + ".visualState");
									break;
								}
							}
							if (visualStateID != -1)
							{
								int visualStateCount = SafeObjMan.GetInt(visMobMan, "visualStates");
								for (int visualStateIndex = 0; visualStateIndex < visualStateCount; visualStateIndex++)
								{
									string path = SafeObjMan.GetDBID(visMobMan, string.Format("visualStates.[{0}]", visualStateIndex));
									IObjMan visualStateMan = mainDb.GetManipulator(mainDb.GetDBIDByName(path));
									if (visualStateMan != null && visualStateID == SafeObjMan.GetInt(visualStateMan, "stateID"))
									{
										animation = SafeObjMan.GetString(visualStateMan, "fixedIdleAnimation");
										break;
									}
								}
							}
						}
					}
					if (string.IsNullOrEmpty(animation) || string.Equals(animation, "invalid"))
					{
						animation = SafeObjMan.GetString(visMobMan, "fixedIdleAnimation");
						if (string.IsNullOrEmpty(animation) || string.Equals(animation, "invalid"))
						{
							animation = "idle";
						}
					}
					return animation;
				}
			}
			return string.Empty;
		}

		// Token: 0x06000F6E RID: 3950 RVA: 0x000796FF File Offset: 0x000786FF
		public static string GetSpawnTunerFolder(string continentName)
		{
			return Constants.ContinentFolder(continentName) + "SpawnTuners/";
		}

		// Token: 0x06000F6F RID: 3951 RVA: 0x00079711 File Offset: 0x00078711
		public static string GetVisualStateFolder(string visMob)
		{
			return Str.CutFileName(visMob, true);
		}

		// Token: 0x06000F70 RID: 3952 RVA: 0x0007971C File Offset: 0x0007871C
		public static string GetUniqueSpawnTunerName(string continentName, string type, IDatabase mainDb)
		{
			if (mainDb != null)
			{
				string shortType = type.Substring(type.LastIndexOf('.') + 1);
				string spawnTunerName = string.Format("{0}.({1}).xdb", "SpawnTuner", shortType);
				return SafeObjMan.CreateUniqueDBID(Constants.ContinentFolder(continentName) + "SpawnTuners/" + spawnTunerName, false, mainDb);
			}
			return string.Empty;
		}

		// Token: 0x06000F71 RID: 3953 RVA: 0x0007976C File Offset: 0x0007876C
		public static string GetUniqueVisualStateName(string visMob, IDatabase mainDb)
		{
			if (mainDb != null)
			{
				Str.CutFileExtention(visMob, false);
				string visualStateName = string.Format("{0}{1}.(CreatureVisState).xdb", Str.CutFileExtention(visMob, false), "VisState");
				return SafeObjMan.CreateUniqueDBID(visualStateName, true, mainDb);
			}
			return string.Empty;
		}

		// Token: 0x06000F72 RID: 3954 RVA: 0x000797AC File Offset: 0x000787AC
		public static bool CreateSpawnTuner(string type, string spawnTuner, IDatabase mainDb, out IObjMan spawnTunerMan)
		{
			if (mainDb != null)
			{
				DBID spawnTunerDBID = IDatabase.CreateDBIDByName(spawnTuner);
				spawnTunerMan = mainDb.CreateNewObject(type);
				return mainDb.AddNewObject(spawnTunerDBID, spawnTunerMan);
			}
			spawnTunerMan = null;
			return false;
		}

		// Token: 0x06000F73 RID: 3955 RVA: 0x000797DC File Offset: 0x000787DC
		public static bool CreateVisualState(string visualState, IDatabase mainDb, out IObjMan visualStateMan)
		{
			if (mainDb != null)
			{
				DBID visualStateDBID = IDatabase.CreateDBIDByName(visualState);
				visualStateMan = mainDb.CreateNewObject("CreatureVisState");
				return mainDb.AddNewObject(visualStateDBID, visualStateMan);
			}
			visualStateMan = null;
			return false;
		}

		// Token: 0x170002F1 RID: 753
		// (get) Token: 0x06000F74 RID: 3956 RVA: 0x0007980E File Offset: 0x0007880E
		public static Color InterfaceColor
		{
			get
			{
				return SpawnPoint.interfaceColor;
			}
		}

		// Token: 0x170002F2 RID: 754
		// (get) Token: 0x06000F75 RID: 3957 RVA: 0x00079815 File Offset: 0x00078815
		public static Color AggroCircleColor
		{
			get
			{
				return SpawnPoint.aggroCircleColor;
			}
		}

		// Token: 0x170002F3 RID: 755
		// (get) Token: 0x06000F76 RID: 3958 RVA: 0x0007981C File Offset: 0x0007881C
		public static string InterfaceSingleObjectTypeName
		{
			get
			{
				return SpawnPoint.interfaceSingleObjectTypeName;
			}
		}

		// Token: 0x170002F4 RID: 756
		// (get) Token: 0x06000F77 RID: 3959 RVA: 0x00079823 File Offset: 0x00078823
		public static string InterfaceSeveralObjectsTypeName
		{
			get
			{
				return SpawnPoint.interfaceSeveralObjectsTypeName;
			}
		}

		// Token: 0x170002F5 RID: 757
		// (get) Token: 0x06000F78 RID: 3960 RVA: 0x0007982A File Offset: 0x0007882A
		public static string SpawnTableFolder
		{
			get
			{
				return "SpawnTables/";
			}
		}

		// Token: 0x170002F6 RID: 758
		// (get) Token: 0x06000F79 RID: 3961 RVA: 0x00079831 File Offset: 0x00078831
		public static string PackSpawnTableName
		{
			get
			{
				return "PackSpawnTable.(SpawnTable).xdb";
			}
		}

		// Token: 0x170002F7 RID: 759
		// (get) Token: 0x06000F7A RID: 3962 RVA: 0x00079838 File Offset: 0x00078838
		public static string SpawnTunerPrefix
		{
			get
			{
				return "SpawnTuner";
			}
		}

		// Token: 0x170002F8 RID: 760
		// (get) Token: 0x06000F7B RID: 3963 RVA: 0x0007983F File Offset: 0x0007883F
		public static string VisualStatePrefix
		{
			get
			{
				return "VisState";
			}
		}

		// Token: 0x170002F9 RID: 761
		// (get) Token: 0x06000F7C RID: 3964 RVA: 0x00079846 File Offset: 0x00078846
		public static string VisualStateDBType
		{
			get
			{
				return "CreatureVisState";
			}
		}

		// Token: 0x170002FA RID: 762
		// (get) Token: 0x06000F7D RID: 3965 RVA: 0x0007984D File Offset: 0x0007884D
		public static string SetVisualStateImpactDBType
		{
			get
			{
				return "gameMechanics.elements.impacts.ImpactSetVisualState";
			}
		}

		// Token: 0x170002FB RID: 763
		// (get) Token: 0x06000F7E RID: 3966 RVA: 0x00079854 File Offset: 0x00078854
		public static string DefaultVisObject
		{
			get
			{
				return "Editor/Map/SpecialObjects/SpawnPoint/SpawnPoint.(StaticObject).xdb";
			}
		}

		// Token: 0x170002FC RID: 764
		// (get) Token: 0x06000F7F RID: 3967 RVA: 0x0007985B File Offset: 0x0007885B
		public static double EmptyAggroRadius
		{
			get
			{
				return 0.0;
			}
		}

		// Token: 0x170002FD RID: 765
		// (get) Token: 0x06000F80 RID: 3968 RVA: 0x00079866 File Offset: 0x00078866
		public static double DefaultAggroRadius
		{
			get
			{
				return 15.0;
			}
		}

		// Token: 0x170002FE RID: 766
		// (get) Token: 0x06000F81 RID: 3969 RVA: 0x00079871 File Offset: 0x00078871
		public static string SpawnTableDBType
		{
			get
			{
				return "gameMechanics.map.spawn.SpawnTable";
			}
		}

		// Token: 0x170002FF RID: 767
		// (get) Token: 0x06000F82 RID: 3970 RVA: 0x00079878 File Offset: 0x00078878
		public static string DefaultSpawnID
		{
			get
			{
				return SpawnPoint.defaultSpawnID;
			}
		}

		// Token: 0x17000300 RID: 768
		// (get) Token: 0x06000F83 RID: 3971 RVA: 0x0007987F File Offset: 0x0007887F
		public static string MobWorldDBType
		{
			get
			{
				return "gameMechanics.world.mob.MobWorld";
			}
		}

		// Token: 0x17000301 RID: 769
		// (get) Token: 0x06000F84 RID: 3972 RVA: 0x00079886 File Offset: 0x00078886
		public static string DeviceDBType
		{
			get
			{
				return "gameMechanics.world.device.DeviceResource";
			}
		}

		// Token: 0x17000302 RID: 770
		// (get) Token: 0x06000F85 RID: 3973 RVA: 0x0007988D File Offset: 0x0007888D
		public static string AstralMobWorldDBType
		{
			get
			{
				return "gameMechanics.world.astralMob.AstralMobWorld";
			}
		}

		// Token: 0x17000303 RID: 771
		// (get) Token: 0x06000F86 RID: 3974 RVA: 0x00079894 File Offset: 0x00078894
		public static string AstralWreckDBType
		{
			get
			{
				return "gameMechanics.world.astralWreck.AstralWreck";
			}
		}

		// Token: 0x17000304 RID: 772
		// (get) Token: 0x06000F87 RID: 3975 RVA: 0x0007989B File Offset: 0x0007889B
		public static string AstralTeleportDBType
		{
			get
			{
				return "gameMechanics.world.astralTeleports.SphericalTeleport";
			}
		}

		// Token: 0x17000305 RID: 773
		// (get) Token: 0x06000F88 RID: 3976 RVA: 0x000798A2 File Offset: 0x000788A2
		public static string SpawnTunerFolder
		{
			get
			{
				return "SpawnTuners/";
			}
		}

		// Token: 0x17000306 RID: 774
		// (get) Token: 0x06000F89 RID: 3977 RVA: 0x000798A9 File Offset: 0x000788A9
		public static string SpawnTunerBaseType
		{
			get
			{
				return "gameMechanics.map.SpawnTuner";
			}
		}

		// Token: 0x17000307 RID: 775
		// (get) Token: 0x06000F8A RID: 3978 RVA: 0x000798B0 File Offset: 0x000788B0
		public static string MobSpawnTunerDBType
		{
			get
			{
				return "gameMechanics.map.spawn.SpawnTunerMobImpacts";
			}
		}

		// Token: 0x17000308 RID: 776
		// (get) Token: 0x06000F8B RID: 3979 RVA: 0x000798B7 File Offset: 0x000788B7
		public static string DeviceSpawnTunerDBType
		{
			get
			{
				return "gameMechanics.map.spawn.SpawnTunerDeviceImpacts";
			}
		}

		// Token: 0x17000309 RID: 777
		// (get) Token: 0x06000F8C RID: 3980 RVA: 0x000798BE File Offset: 0x000788BE
		public static SpawnTableType DefaultSpawnTableType
		{
			get
			{
				return SpawnTableType.Table;
			}
		}

		// Token: 0x1700030A RID: 778
		// (get) Token: 0x06000F8D RID: 3981 RVA: 0x000798C1 File Offset: 0x000788C1
		public static SpawnTableType DefaultAstralSpawnTableType
		{
			get
			{
				return SpawnTableType.AstralMob;
			}
		}

		// Token: 0x1700030B RID: 779
		// (get) Token: 0x06000F8E RID: 3982 RVA: 0x000798C4 File Offset: 0x000788C4
		public static double NormalThreshold
		{
			get
			{
				return 0.0025000000000000005;
			}
		}

		// Token: 0x06000F8F RID: 3983 RVA: 0x000798D0 File Offset: 0x000788D0
		public SpawnPoint(int _id, MapObjectType _type, ICollisionMap _collisionMap, SpawnPointType _spawnPointType, SpawnTableType _spawnTableType, string _spawnID) : base(_id, _type, _collisionMap)
		{
			base.Highlight(string.IsNullOrEmpty(_spawnID) ? SpawnPoint.interfaceColor : SpawnPoint.interfaceColorWithSpawnID);
			this.spawnTable = _type.Stats;
			this.spawnTableType = SpawnPoint.GetSpawnTableType(this.spawnTable, _spawnTableType);
			this.spawnID = _spawnID;
			SpawnPoint.ExtractVisualData(this.spawnTable, this.spawnID, out this.mobSceneName, out this.aggroRadius);
			this.spawnPointData = SpawnPointData.Create(_spawnPointType, this);
		}

		// Token: 0x06000F90 RID: 3984 RVA: 0x00079994 File Offset: 0x00078994
		public SpawnPoint(int _id, MapObjectType _type, ICollisionMap _collisionMap, SpawnPointType _spawnPointType, SpawnTableType _spawnTableType, string _spawnID, string _mobSceneName, double _aggroRadius) : base(_id, _type, _collisionMap)
		{
			base.Highlight(string.IsNullOrEmpty(_spawnID) ? SpawnPoint.interfaceColor : SpawnPoint.interfaceColorWithSpawnID);
			this.spawnTable = _type.Stats;
			this.spawnTableType = SpawnPoint.GetSpawnTableType(this.spawnTable, _spawnTableType);
			this.spawnID = _spawnID;
			this.mobSceneName = _mobSceneName;
			this.aggroRadius = _aggroRadius;
			this.spawnPointData = SpawnPointData.Create(_spawnPointType, this);
		}

		// Token: 0x1700030C RID: 780
		// (get) Token: 0x06000F91 RID: 3985 RVA: 0x00079A49 File Offset: 0x00078A49
		// (set) Token: 0x06000F92 RID: 3986 RVA: 0x00079A60 File Offset: 0x00078A60
		[DisplayName("Type")]
		[RefreshProperties(RefreshProperties.All)]
		[Category("SpawnPoint")]
		[Browsable(true)]
		public SpawnPointType SpawnPointType
		{
			get
			{
				if (this.spawnPointData != null)
				{
					return this.spawnPointData.SpawnPointType;
				}
				return SpawnPointType.Undefined;
			}
			set
			{
				if (this.spawnPointData.SpawnPointType != value)
				{
					SpawnPointData newSpawnPointData = SpawnPointData.Create(value, this);
					if (newSpawnPointData != null && base.InvokeChanging(null))
					{
						SpawnPointData oldSpawnPointData = this.spawnPointData;
						this.spawnPointData = newSpawnPointData;
						base.InvokeChanged();
						if (base.Active && SpawnPoint.TypeChanged != null)
						{
							SpawnPoint.TypeChanged(this, ref oldSpawnPointData, ref newSpawnPointData);
						}
					}
				}
			}
		}

		// Token: 0x1700030D RID: 781
		// (get) Token: 0x06000F93 RID: 3987 RVA: 0x00079AC1 File Offset: 0x00078AC1
		// (set) Token: 0x06000F94 RID: 3988 RVA: 0x00079ACC File Offset: 0x00078ACC
		[Browsable(true)]
		[Category("SpawnPoint")]
		[RefreshProperties(RefreshProperties.All)]
		public string SpawnTable
		{
			get
			{
				return this.spawnTable;
			}
			set
			{
				if (this.spawnTable != value && base.InvokeChanging(null))
				{
					string oldSpawnTable = this.spawnTable;
					this.spawnTable = value;
					base.SetNewStats(this.spawnTable);
					this.CheckSpawnTableType();
					this.CheckVisualData();
					this.CheckSpawnTimeRareId(this.spawnTime);
					base.InvokeChanged();
					if (base.Active && SpawnPoint.SpawnTableChanged != null)
					{
						SpawnPoint.SpawnTableChanged(this, ref oldSpawnTable, ref this.spawnTable);
					}
				}
			}
		}

		// Token: 0x1700030E RID: 782
		// (get) Token: 0x06000F95 RID: 3989 RVA: 0x00079B4A File Offset: 0x00078B4A
		[RefreshProperties(RefreshProperties.All)]
		[Browsable(true)]
		[Category("SpawnPoint")]
		public SpawnTableType SpawnTableType
		{
			get
			{
				return this.spawnTableType;
			}
		}

		// Token: 0x1700030F RID: 783
		// (get) Token: 0x06000F96 RID: 3990 RVA: 0x00079B52 File Offset: 0x00078B52
		// (set) Token: 0x06000F97 RID: 3991 RVA: 0x00079B5C File Offset: 0x00078B5C
		[Browsable(true)]
		[Category("SpawnPoint")]
		[DisplayName("ScriptID")]
		public string ScriptID
		{
			get
			{
				return this.scriptID;
			}
			set
			{
				value = Str.Trim(value);
				if (this.scriptID != value && base.InvokeChanging(null))
				{
					string oldScriptID = this.scriptID;
					this.scriptID = value;
					base.InvokeChanged();
					if (base.Active && SpawnPoint.ScriptIDChanged != null)
					{
						SpawnPoint.ScriptIDChanged(this, ref oldScriptID, ref this.scriptID);
					}
				}
			}
		}

		// Token: 0x17000310 RID: 784
		// (get) Token: 0x06000F98 RID: 3992 RVA: 0x00079BBE File Offset: 0x00078BBE
		// (set) Token: 0x06000F99 RID: 3993 RVA: 0x00079BC8 File Offset: 0x00078BC8
		[Browsable(true)]
		[DisplayName("SpawnID")]
		[Category("SpawnPoint")]
		public string SpawnID
		{
			get
			{
				return this.spawnID;
			}
			set
			{
				if (this.spawnID != value && base.InvokeChanging(null))
				{
					string oldSpawnID = this.spawnID;
					this.spawnID = value;
					this.CheckVisualData();
					base.InvokeChanged();
					if (base.Active && SpawnPoint.SpawnIDChanged != null)
					{
						SpawnPoint.SpawnIDChanged(this, ref oldSpawnID, ref this.spawnID);
					}
					base.Highlight(string.IsNullOrEmpty(this.spawnID) ? SpawnPoint.interfaceColor : SpawnPoint.interfaceColorWithSpawnID);
				}
			}
		}

		// Token: 0x17000311 RID: 785
		// (get) Token: 0x06000F9A RID: 3994 RVA: 0x00079C47 File Offset: 0x00078C47
		// (set) Token: 0x06000F9B RID: 3995 RVA: 0x00079C50 File Offset: 0x00078C50
		[Browsable(true)]
		[Category("SpawnPoint")]
		[DisplayName("ScanRadius")]
		public double ScanRadius
		{
			get
			{
				return this.scanRadius;
			}
			set
			{
				if (this.scanRadius != value && base.InvokeChanging(null))
				{
					double oldScanRadius = this.scanRadius;
					this.scanRadius = value;
					base.InvokeChanged();
					if (base.Active && SpawnPoint.ScanRadiusChanged != null)
					{
						SpawnPoint.ScanRadiusChanged(this, ref oldScanRadius, ref this.scanRadius);
					}
				}
			}
		}

		// Token: 0x17000312 RID: 786
		// (get) Token: 0x06000F9C RID: 3996 RVA: 0x00079CA5 File Offset: 0x00078CA5
		// (set) Token: 0x06000F9D RID: 3997 RVA: 0x00079CB0 File Offset: 0x00078CB0
		[Category("SpawnPoint")]
		[TypeConverter(typeof(SpawnTimeConverter))]
		[RefreshProperties(RefreshProperties.All)]
		[DisplayName("SpawnTime")]
		[Browsable(true)]
		public SpawnTimeAbstract SpawnTime
		{
			get
			{
				return this.spawnTime;
			}
			set
			{
				if (this.spawnTime != value && base.InvokeChanging(null))
				{
					SpawnTimeAbstract oldSpawnTime = this.spawnTime;
					this.spawnTime = value;
					base.InvokeChanged();
					if (base.Active && SpawnPoint.SpawnTimeChanged != null)
					{
						SpawnPoint.SpawnTimeChanged(this, ref oldSpawnTime, ref this.spawnTime);
					}
				}
			}
		}

		// Token: 0x17000313 RID: 787
		// (get) Token: 0x06000F9E RID: 3998 RVA: 0x00079D05 File Offset: 0x00078D05
		// (set) Token: 0x06000F9F RID: 3999 RVA: 0x00079D10 File Offset: 0x00078D10
		[RefreshProperties(RefreshProperties.All)]
		[TypeConverter(typeof(SingleSpawnControllerConverter))]
		[Browsable(true)]
		[Category("SpawnPoint")]
		public SingleSpawnController SingleSpawnController
		{
			get
			{
				return this.singleSpawnController;
			}
			set
			{
				if (this.singleSpawnController != value && base.InvokeChanging(null))
				{
					SingleSpawnController oldValue = this.singleSpawnController;
					this.singleSpawnController = value;
					base.InvokeChanged();
					if (base.Active && SpawnPoint.SingleSpawnControllerChanged != null)
					{
						SpawnPoint.SingleSpawnControllerChanged(this, ref oldValue, ref value);
					}
				}
			}
		}

		// Token: 0x17000314 RID: 788
		// (get) Token: 0x06000FA0 RID: 4000 RVA: 0x00079D61 File Offset: 0x00078D61
		// (set) Token: 0x06000FA1 RID: 4001 RVA: 0x00079D6C File Offset: 0x00078D6C
		[TypeConverter(typeof(SpawnPointDataConverter))]
		[DisplayName("Data")]
		[Category("SpawnPoint")]
		[Browsable(true)]
		public SpawnPointData SpawnPointData
		{
			get
			{
				return this.spawnPointData;
			}
			set
			{
				if (base.InvokeChanging(null))
				{
					SpawnPointType oldSpawnPointType = this.spawnPointData.SpawnPointType;
					SpawnPointData oldSpawnPointData = this.spawnPointData;
					this.spawnPointData = value;
					base.InvokeChanged();
					if (base.Active)
					{
						SpawnPointType newSpawnPointType = this.spawnPointData.SpawnPointType;
						if (oldSpawnPointType != newSpawnPointType && SpawnPoint.TypeChanged != null)
						{
							SpawnPoint.TypeChanged(this, ref oldSpawnPointData, ref this.spawnPointData);
						}
						if (SpawnPoint.DataChanged != null)
						{
							SpawnPoint.DataChanged(this, ref oldSpawnPointData, ref this.spawnPointData);
						}
					}
				}
			}
		}

		// Token: 0x17000315 RID: 789
		// (get) Token: 0x06000FA2 RID: 4002 RVA: 0x00079DEE File Offset: 0x00078DEE
		[DisplayName("AggroRadius")]
		[Browsable(true)]
		[Category("SpawnPoint")]
		public double AgrroRadius
		{
			get
			{
				return this.aggroRadius;
			}
		}

		// Token: 0x17000316 RID: 790
		// (get) Token: 0x06000FA3 RID: 4003 RVA: 0x00079DF6 File Offset: 0x00078DF6
		// (set) Token: 0x06000FA4 RID: 4004 RVA: 0x00079E00 File Offset: 0x00078E00
		[Category("SpawnPoint")]
		[DisplayName("SpawnTuner")]
		[Browsable(true)]
		public string SpawnTuner
		{
			get
			{
				return this.spawnTuner;
			}
			set
			{
				if (this.spawnTuner != value && base.InvokeChanging(null))
				{
					string oldTuner = this.spawnTuner;
					this.spawnTuner = value;
					base.InvokeChanged();
					if (base.Active && SpawnPoint.SpawnTunerChanged != null)
					{
						SpawnPoint.SpawnTunerChanged(this, ref oldTuner, ref this.spawnTuner);
					}
				}
			}
		}

		// Token: 0x17000317 RID: 791
		// (get) Token: 0x06000FA5 RID: 4005 RVA: 0x00079E5A File Offset: 0x00078E5A
		// (set) Token: 0x06000FA6 RID: 4006 RVA: 0x00079E62 File Offset: 0x00078E62
		[Browsable(false)]
		public Vec3 CachedNormal
		{
			get
			{
				return this.cachedNormal;
			}
			set
			{
				this.cachedNormal = value;
			}
		}

		// Token: 0x17000318 RID: 792
		// (get) Token: 0x06000FA7 RID: 4007 RVA: 0x00079E6B File Offset: 0x00078E6B
		[DisplayName("MobVisualObject")]
		[Browsable(true)]
		[Category("SpawnPoint")]
		public override string SceneName
		{
			get
			{
				return this.mobSceneName;
			}
		}

		// Token: 0x06000FA8 RID: 4008 RVA: 0x00079E74 File Offset: 0x00078E74
		public override IMapObject Clone(int newID, bool newTemporary, bool newActive)
		{
			SpawnPoint spawnPoint = new SpawnPoint(newID, new MapObjectType(base.Type.Type, this.spawnTable), base.CollisionMap, this.SpawnPointType, this.spawnTableType, this.spawnID, this.mobSceneName, this.aggroRadius);
			spawnPoint.scriptID = this.scriptID;
			spawnPoint.scanRadius = this.scanRadius;
			spawnPoint.spawnTime = ((this.spawnTime != null) ? this.spawnTime.Clone() : null);
			spawnPoint.SpawnTuner = this.SpawnTuner;
			spawnPoint.singleSpawnController = ((this.singleSpawnController != null) ? this.singleSpawnController.Clone() : null);
			base.CopyTo(spawnPoint, newTemporary, newActive);
			if (spawnPoint.spawnPointData != null && this.spawnPointData != null)
			{
				spawnPoint.spawnPointData.CopyFrom(this.spawnPointData);
			}
			if (!newTemporary)
			{
				spawnPoint.CheckVisualData();
			}
			return spawnPoint;
		}

		// Token: 0x06000FA9 RID: 4009 RVA: 0x00079F58 File Offset: 0x00078F58
		public override IMapObjectPack Pack()
		{
			SpawnPointPack spawnPointPack = new SpawnPointPack();
			spawnPointPack.Pack(this);
			return spawnPointPack;
		}

		// Token: 0x06000FAA RID: 4010 RVA: 0x00079F73 File Offset: 0x00078F73
		public Color GetInterfaceColor()
		{
			return SpawnPoint.interfaceColor;
		}

		// Token: 0x06000FAB RID: 4011 RVA: 0x00079F7A File Offset: 0x00078F7A
		public string GetInterfaceSingleObjectTypeName()
		{
			return SpawnPoint.interfaceSingleObjectTypeName;
		}

		// Token: 0x06000FAC RID: 4012 RVA: 0x00079F81 File Offset: 0x00078F81
		public string GetInterfaceSeveralObjectsTypeName()
		{
			return SpawnPoint.interfaceSeveralObjectsTypeName;
		}

		// Token: 0x06000FAD RID: 4013 RVA: 0x00079F88 File Offset: 0x00078F88
		public bool ContainsText(string text, bool ignoreCase)
		{
			if (!string.IsNullOrEmpty(text))
			{
				StringComparison stringComparison = ignoreCase ? StringComparison.InvariantCultureIgnoreCase : StringComparison.InvariantCulture;
				if (!string.IsNullOrEmpty(this.spawnTable) && this.spawnTable.IndexOf(text, stringComparison) != -1)
				{
					return true;
				}
				if (!string.IsNullOrEmpty(this.scriptID) && this.scriptID.IndexOf(text, stringComparison) != -1)
				{
					return true;
				}
				if (!string.IsNullOrEmpty(this.spawnID) && this.spawnID.IndexOf(text, stringComparison) != -1)
				{
					return true;
				}
				string script = this.GetScript();
				if (!string.IsNullOrEmpty(script) && script.IndexOf(text, stringComparison) != -1)
				{
					return true;
				}
				if (this.spawnTableType == SpawnTableType.Table && !string.IsNullOrEmpty(this.spawnTable))
				{
					IDatabase mainDb = IDatabase.GetMainDatabase();
					if (mainDb != null)
					{
						DBID spawnTableDBID = mainDb.GetDBIDByName(this.spawnTable);
						if (!spawnTableDBID.IsEmpty())
						{
							IObjMan spawnTableMan = mainDb.GetManipulator(spawnTableDBID);
							if (spawnTableMan != null)
							{
								foreach (string section in SpawnPoint.spawnTableSections)
								{
									int mobCount = SafeObjMan.GetInt(spawnTableMan, section);
									if (mobCount > 0)
									{
										for (int i = 0; i < mobCount; i++)
										{
											string _spawnID = SafeObjMan.GetString(spawnTableMan, string.Format("{0}.[{1}].object", section, i));
											if (!string.IsNullOrEmpty(_spawnID) && _spawnID.IndexOf(text, stringComparison) != -1)
											{
												return true;
											}
										}
									}
								}
							}
						}
					}
				}
			}
			return false;
		}

		// Token: 0x06000FAE RID: 4014 RVA: 0x0007A0EA File Offset: 0x000790EA
		public string GetStatsForDBBrowse()
		{
			return this.spawnTable;
		}

		// Token: 0x06000FAF RID: 4015 RVA: 0x0007A0F2 File Offset: 0x000790F2
		public string GetSpecialStatsForDBBrowse()
		{
			if (!string.Equals(this.mobSceneName, "Editor/Map/SpecialObjects/SpawnPoint/SpawnPoint.(StaticObject).xdb", StringComparison.OrdinalIgnoreCase))
			{
				return this.mobSceneName;
			}
			return string.Empty;
		}

		// Token: 0x06000FB0 RID: 4016 RVA: 0x0007A114 File Offset: 0x00079114
		public string GetLabel()
		{
			if (this.spawnPointData != null && this.spawnPointData.SpawnPointType == SpawnPointType.Patrol)
			{
				PatrolSpawnPointData patrolSpawnPointData = this.spawnPointData as PatrolSpawnPointData;
				if (patrolSpawnPointData != null)
				{
					return patrolSpawnPointData.Label;
				}
			}
			return string.Empty;
		}

		// Token: 0x06000FB1 RID: 4017 RVA: 0x0007A154 File Offset: 0x00079154
		public void SetLabel(string _label)
		{
			if (this.spawnPointData != null && this.spawnPointData.SpawnPointType == SpawnPointType.Patrol)
			{
				PatrolSpawnPointData patrolSpawnPointData = this.spawnPointData as PatrolSpawnPointData;
				if (patrolSpawnPointData != null)
				{
					patrolSpawnPointData.Label = _label;
				}
			}
		}

		// Token: 0x06000FB2 RID: 4018 RVA: 0x0007A190 File Offset: 0x00079190
		public string GetScript()
		{
			if (this.spawnPointData != null && this.spawnPointData.SpawnPointType == SpawnPointType.Patrol)
			{
				PatrolSpawnPointData patrolSpawnPointData = this.spawnPointData as PatrolSpawnPointData;
				if (patrolSpawnPointData != null)
				{
					return patrolSpawnPointData.Script;
				}
			}
			return string.Empty;
		}

		// Token: 0x06000FB3 RID: 4019 RVA: 0x0007A1D0 File Offset: 0x000791D0
		public void CheckVisualData()
		{
			string newMobSceneName;
			double newAggroRadius;
			SpawnPoint.ExtractVisualData(this.spawnTable, this.spawnID, out newMobSceneName, out newAggroRadius);
			if (this.mobSceneName != newMobSceneName)
			{
				string oldMobSceneName = this.mobSceneName;
				this.mobSceneName = newMobSceneName;
				bool activeBackup = base.Active;
				base.Active = false;
				if (SpawnPoint.MobSceneNameChanged != null)
				{
					SpawnPoint.MobSceneNameChanged(this, ref oldMobSceneName, ref newMobSceneName);
				}
				base.Active = activeBackup;
			}
			if (this.aggroRadius != newAggroRadius)
			{
				double oldAggroRadius = this.aggroRadius;
				this.aggroRadius = newAggroRadius;
				bool activeBackup2 = base.Active;
				base.Active = false;
				if (SpawnPoint.AggroRadiusChanged != null)
				{
					SpawnPoint.AggroRadiusChanged(this, ref oldAggroRadius, ref newAggroRadius);
				}
				base.Active = activeBackup2;
			}
			if (SpawnPoint.SpawnTunerChanged != null)
			{
				SpawnPoint.SpawnTunerChanged(this, ref this.spawnTuner, ref this.spawnTuner);
			}
		}

		// Token: 0x06000FB4 RID: 4020 RVA: 0x0007A29C File Offset: 0x0007929C
		public void SetScript(string _script)
		{
			if (this.spawnPointData != null && this.spawnPointData.SpawnPointType == SpawnPointType.Patrol)
			{
				PatrolSpawnPointData patrolSpawnPointData = this.spawnPointData as PatrolSpawnPointData;
				if (patrolSpawnPointData != null)
				{
					patrolSpawnPointData.Script = _script;
				}
			}
		}

		// Token: 0x06000FB5 RID: 4021 RVA: 0x0007A2D5 File Offset: 0x000792D5
		public void SetSpawnTableType(SpawnTableType newSpawnTableType)
		{
			this.spawnTableType = newSpawnTableType;
		}

		// Token: 0x06000FB6 RID: 4022 RVA: 0x0007A2E0 File Offset: 0x000792E0
		public void CheckSpawnTimeRareId(SpawnTimeAbstract _spawnTime)
		{
			if (this.ValidSpawnTimeOwnerInstance())
			{
				SpawnTimeRare spawnTimeRare = _spawnTime as SpawnTimeRare;
				if (spawnTimeRare != null)
				{
					spawnTimeRare.Id = this.spawnTable;
				}
			}
		}

		// Token: 0x06000FB7 RID: 4023 RVA: 0x0007A30B File Offset: 0x0007930B
		public bool ValidSpawnTimeOwnerInstance()
		{
			return this.spawnTableType == SpawnTableType.SingleDevice || this.spawnTableType == SpawnTableType.SingleMob || this.spawnTableType == SpawnTableType.AstralMob || this.spawnTableType == SpawnTableType.AstralWreck || this.spawnTableType == SpawnTableType.AstralTeleport;
		}

		// Token: 0x06000FB8 RID: 4024 RVA: 0x0007A33C File Offset: 0x0007933C
		public bool ValidSingleSpawnControllerOwnerInstance()
		{
			return SpawnPoint.SingleSpawnControllerEnabled(this.spawnTableType);
		}

		// Token: 0x04000BB6 RID: 2998
		private const string spawnTableFolder = "SpawnTables/";

		// Token: 0x04000BB7 RID: 2999
		private const string packSpawnTableName = "PackSpawnTable.(SpawnTable).xdb";

		// Token: 0x04000BB8 RID: 3000
		private const string visObjectDBType = "VisualMob";

		// Token: 0x04000BB9 RID: 3001
		private const string spawnTunerPrefix = "SpawnTuner";

		// Token: 0x04000BBA RID: 3002
		private const string visualStatePrefix = "VisState";

		// Token: 0x04000BBB RID: 3003
		private const string visualStateDBType = "CreatureVisState";

		// Token: 0x04000BBC RID: 3004
		private const string setVisualStateImpactDBType = "gameMechanics.elements.impacts.ImpactSetVisualState";

		// Token: 0x04000BBD RID: 3005
		private const string defaultIdleAnimationName = "idle";

		// Token: 0x04000BBE RID: 3006
		private const string invalidAnimationName = "invalid";

		// Token: 0x04000BBF RID: 3007
		private const string defaultVisObject = "Editor/Map/SpecialObjects/SpawnPoint/SpawnPoint.(StaticObject).xdb";

		// Token: 0x04000BC0 RID: 3008
		private const string spawnTableDBType = "gameMechanics.map.spawn.SpawnTable";

		// Token: 0x04000BC1 RID: 3009
		private const string mobWorldDBType = "gameMechanics.world.mob.MobWorld";

		// Token: 0x04000BC2 RID: 3010
		private const string deviceDBType = "gameMechanics.world.device.DeviceResource";

		// Token: 0x04000BC3 RID: 3011
		private const string astralMobWorldDBType = "gameMechanics.world.astralMob.AstralMobWorld";

		// Token: 0x04000BC4 RID: 3012
		private const string astralWreckDBType = "gameMechanics.world.astralWreck.AstralWreck";

		// Token: 0x04000BC5 RID: 3013
		private const string astralTeleportDBType = "gameMechanics.world.astralTeleports.SphericalTeleport";

		// Token: 0x04000BC6 RID: 3014
		private const string spawnTunerFolder = "SpawnTuners/";

		// Token: 0x04000BC7 RID: 3015
		private const string spawnTunerBaseType = "gameMechanics.map.SpawnTuner";

		// Token: 0x04000BC8 RID: 3016
		private const string mobSpawnTunerDBType = "gameMechanics.map.spawn.SpawnTunerMobImpacts";

		// Token: 0x04000BC9 RID: 3017
		private const string deviceSpawnTunerDBType = "gameMechanics.map.spawn.SpawnTunerDeviceImpacts";

		// Token: 0x04000BCA RID: 3018
		private const SpawnTableType defaultSpawnTableType = SpawnTableType.Table;

		// Token: 0x04000BCB RID: 3019
		private const SpawnTableType defaultAstralSpawnTableType = SpawnTableType.AstralMob;

		// Token: 0x04000BCC RID: 3020
		private const double emptyAggroRadius = 0.0;

		// Token: 0x04000BCD RID: 3021
		private const double defaultAggroRadius = 15.0;

		// Token: 0x04000BCE RID: 3022
		private const double astralAggroRadius = 1000.0;

		// Token: 0x04000BCF RID: 3023
		private const double normalThreshold = 0.0025000000000000005;

		// Token: 0x04000BD0 RID: 3024
		private static readonly Color interfaceColor = Color.FromArgb(MapObjectCreationInfo.DefaultTransparentColorAlpha, Color.Turquoise);

		// Token: 0x04000BD1 RID: 3025
		private static readonly Color interfaceColorWithSpawnID = Color.FromArgb(MapObjectCreationInfo.DefaultTransparentColorAlpha, Color.Blue);

		// Token: 0x04000BD2 RID: 3026
		private static readonly Color aggroCircleColor = Color.FromArgb(MapObjectCreationInfo.DefaultTransparentColorAlpha, Color.Red);

		// Token: 0x04000BD3 RID: 3027
		private static readonly string interfaceSingleObjectTypeName = Strings.SINGLE_SPAWN_POINT_TYPE_NAME;

		// Token: 0x04000BD4 RID: 3028
		private static readonly string interfaceSeveralObjectsTypeName = Strings.SEVERAL_SPAWN_POINTS_TYPE_NAME;

		// Token: 0x04000BD5 RID: 3029
		private static readonly string defaultSpawnID = string.Empty;

		// Token: 0x04000BD6 RID: 3030
		private static readonly string[] spawnTableSections = new string[]
		{
			"commons",
			"singles"
		};

		// Token: 0x04000BD7 RID: 3031
		private static string[] devicesDBTypes = null;

		// Token: 0x04000BD8 RID: 3032
		private static bool devicesDBTypesFilled = false;

		// Token: 0x04000BD9 RID: 3033
		private string spawnTable = string.Empty;

		// Token: 0x04000BDA RID: 3034
		private SpawnTableType spawnTableType = SpawnTableType.Table;

		// Token: 0x04000BDB RID: 3035
		private string scriptID = string.Empty;

		// Token: 0x04000BDC RID: 3036
		private string spawnID = string.Empty;

		// Token: 0x04000BDD RID: 3037
		private double scanRadius;

		// Token: 0x04000BDE RID: 3038
		private double aggroRadius;

		// Token: 0x04000BDF RID: 3039
		private SpawnPointData spawnPointData;

		// Token: 0x04000BE0 RID: 3040
		private SpawnTimeAbstract spawnTime;

		// Token: 0x04000BE1 RID: 3041
		private string spawnTuner;

		// Token: 0x04000BE2 RID: 3042
		private string mobSceneName = string.Empty;

		// Token: 0x04000BE3 RID: 3043
		private Vec3 cachedNormal = Vec3.ZNormal;

		// Token: 0x04000BE4 RID: 3044
		private SingleSpawnController singleSpawnController;

		// Token: 0x02000142 RID: 322
		// (Invoke) Token: 0x06000FBB RID: 4027
		public delegate void SpawnPointFieldChangedEvent<T>(SpawnPoint spawnPoint, ref T oldValue, ref T newValue);
	}
}
