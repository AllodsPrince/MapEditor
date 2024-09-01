using System;
using System.ComponentModel;
using System.Drawing;
using Db;
using MapEditor.Resources.Strings;
using Tools.MapObjects;
using Tools.SafeObjMan;

namespace MapEditor.Map.MapObjects
{
	// Token: 0x020002C2 RID: 706
	public class PlayerRespawnPlace : MapObject, IMapObjectInterfaceExtention
	{
		// Token: 0x140000F5 RID: 245
		// (add) Token: 0x060020D1 RID: 8401 RVA: 0x000D1638 File Offset: 0x000D0638
		// (remove) Token: 0x060020D2 RID: 8402 RVA: 0x000D164F File Offset: 0x000D064F
		public static event PlayerRespawnPlace.PlayerRespawnPlaceFieldChangedEvent<string> DeviceChanged;

		// Token: 0x140000F6 RID: 246
		// (add) Token: 0x060020D3 RID: 8403 RVA: 0x000D1666 File Offset: 0x000D0666
		// (remove) Token: 0x060020D4 RID: 8404 RVA: 0x000D167D File Offset: 0x000D067D
		public static event PlayerRespawnPlace.PlayerRespawnPlaceFieldChangedEvent<string> FactionChanged;

		// Token: 0x140000F7 RID: 247
		// (add) Token: 0x060020D5 RID: 8405 RVA: 0x000D1694 File Offset: 0x000D0694
		// (remove) Token: 0x060020D6 RID: 8406 RVA: 0x000D16AB File Offset: 0x000D06AB
		public static event PlayerRespawnPlace.PlayerRespawnPlaceFieldChangedEvent<string> MobSceneNameChanged;

		// Token: 0x060020D7 RID: 8407 RVA: 0x000D16C4 File Offset: 0x000D06C4
		private static void ExtractVisualData(string _device, out string _mobSceneName)
		{
			_mobSceneName = PlayerRespawnPlace.defaultVisObject;
			if (string.IsNullOrEmpty(_device))
			{
				return;
			}
			IDatabase mainDb = IDatabase.GetMainDatabase();
			if (mainDb == null)
			{
				return;
			}
			DBID deviceDBID = mainDb.GetDBIDByName(_device);
			if (deviceDBID.IsEmpty())
			{
				return;
			}
			IObjMan deviceMan = mainDb.GetManipulator(deviceDBID);
			if (deviceMan == null)
			{
				return;
			}
			if (deviceMan.GetFieldDesc("visObj") != null)
			{
				string visMob = SafeObjMan.GetDBID(deviceMan, "visObj");
				if (!string.IsNullOrEmpty(visMob))
				{
					_mobSceneName = visMob;
				}
			}
		}

		// Token: 0x17000757 RID: 1879
		// (get) Token: 0x060020D8 RID: 8408 RVA: 0x000D172C File Offset: 0x000D072C
		public static Color InterfaceColor
		{
			get
			{
				return PlayerRespawnPlace.interfaceColor;
			}
		}

		// Token: 0x17000758 RID: 1880
		// (get) Token: 0x060020D9 RID: 8409 RVA: 0x000D1733 File Offset: 0x000D0733
		public static string InterfaceSingleObjectTypeName
		{
			get
			{
				return PlayerRespawnPlace.interfaceSingleObjectTypeName;
			}
		}

		// Token: 0x17000759 RID: 1881
		// (get) Token: 0x060020DA RID: 8410 RVA: 0x000D173A File Offset: 0x000D073A
		public static string InterfaceSeveralObjectsTypeName
		{
			get
			{
				return PlayerRespawnPlace.interfaceSeveralObjectsTypeName;
			}
		}

		// Token: 0x1700075A RID: 1882
		// (get) Token: 0x060020DB RID: 8411 RVA: 0x000D1741 File Offset: 0x000D0741
		public static string DefaultVisObject
		{
			get
			{
				return PlayerRespawnPlace.defaultVisObject;
			}
		}

		// Token: 0x1700075B RID: 1883
		// (get) Token: 0x060020DC RID: 8412 RVA: 0x000D1748 File Offset: 0x000D0748
		public static string DeviceDBType
		{
			get
			{
				return PlayerRespawnPlace.deviceDBType;
			}
		}

		// Token: 0x1700075C RID: 1884
		// (get) Token: 0x060020DD RID: 8413 RVA: 0x000D174F File Offset: 0x000D074F
		public static string FactionDBType
		{
			get
			{
				return PlayerRespawnPlace.factionDBType;
			}
		}

		// Token: 0x060020DE RID: 8414 RVA: 0x000D1758 File Offset: 0x000D0758
		public PlayerRespawnPlace(int _id, MapObjectType _type, ICollisionMap _collisionMap) : base(_id, _type, _collisionMap)
		{
			this.device = _type.Stats;
			PlayerRespawnPlace.ExtractVisualData(this.device, out this.mobSceneName);
			base.Highlight(PlayerRespawnPlace.interfaceColor);
		}

		// Token: 0x060020DF RID: 8415 RVA: 0x000D17B8 File Offset: 0x000D07B8
		public PlayerRespawnPlace(int _id, MapObjectType _type, ICollisionMap _collisionMap, string _device, string _faction, string _mobSceneName) : base(_id, _type, _collisionMap)
		{
			this.device = _device;
			this.faction = _faction;
			this.mobSceneName = _mobSceneName;
		}

		// Token: 0x060020E0 RID: 8416 RVA: 0x000D1808 File Offset: 0x000D0808
		public void CheckVisualData()
		{
			string newMobSceneName;
			PlayerRespawnPlace.ExtractVisualData(this.device, out newMobSceneName);
			if (!this.mobSceneName.Equals(newMobSceneName))
			{
				string oldMobSceneName = this.mobSceneName;
				this.mobSceneName = newMobSceneName;
				bool activeBackup = base.Active;
				base.Active = false;
				if (PlayerRespawnPlace.MobSceneNameChanged != null)
				{
					PlayerRespawnPlace.MobSceneNameChanged(this, ref oldMobSceneName, ref newMobSceneName);
				}
				base.Active = activeBackup;
			}
		}

		// Token: 0x1700075D RID: 1885
		// (get) Token: 0x060020E1 RID: 8417 RVA: 0x000D1869 File Offset: 0x000D0869
		// (set) Token: 0x060020E2 RID: 8418 RVA: 0x000D1874 File Offset: 0x000D0874
		[Browsable(true)]
		[DisplayName("Device")]
		[Category("PlayerRespawnPlace")]
		public string Device
		{
			get
			{
				return this.device;
			}
			set
			{
				if (this.device != value && base.InvokeChanging(null))
				{
					string oldDevice = this.device;
					this.device = value;
					this.CheckVisualData();
					base.InvokeChanged();
					if (base.Active && PlayerRespawnPlace.DeviceChanged != null)
					{
						PlayerRespawnPlace.DeviceChanged(this, ref oldDevice, ref this.device);
					}
				}
			}
		}

		// Token: 0x1700075E RID: 1886
		// (get) Token: 0x060020E3 RID: 8419 RVA: 0x000D18D4 File Offset: 0x000D08D4
		// (set) Token: 0x060020E4 RID: 8420 RVA: 0x000D18DC File Offset: 0x000D08DC
		[Browsable(true)]
		[Category("PlayerRespawnPlace")]
		[DisplayName("Faction")]
		public string Faction
		{
			get
			{
				return this.faction;
			}
			set
			{
				value = Str.Trim(value);
				if (this.faction != value && base.InvokeChanging(null))
				{
					string oldFaction = this.faction;
					this.faction = value;
					base.InvokeChanged();
					if (base.Active && PlayerRespawnPlace.FactionChanged != null)
					{
						PlayerRespawnPlace.FactionChanged(this, ref oldFaction, ref this.faction);
					}
				}
			}
		}

		// Token: 0x1700075F RID: 1887
		// (get) Token: 0x060020E5 RID: 8421 RVA: 0x000D193E File Offset: 0x000D093E
		[Category("PlayerRespawnPlace")]
		[Browsable(true)]
		[DisplayName("MobVisualObject")]
		public override string SceneName
		{
			get
			{
				return this.mobSceneName;
			}
		}

		// Token: 0x060020E6 RID: 8422 RVA: 0x000D1948 File Offset: 0x000D0948
		public override IMapObject Clone(int newID, bool newTemporary, bool newActive)
		{
			PlayerRespawnPlace playerRespawnPlace = new PlayerRespawnPlace(newID, new MapObjectType(base.Type.Type, base.Type.Stats), base.CollisionMap, this.device, this.faction, this.mobSceneName);
			base.CopyTo(playerRespawnPlace, newTemporary, newActive);
			return playerRespawnPlace;
		}

		// Token: 0x060020E7 RID: 8423 RVA: 0x000D19A0 File Offset: 0x000D09A0
		public override IMapObjectPack Pack()
		{
			PlayerRespawnPlacePack playerRespawnPlacePack = new PlayerRespawnPlacePack();
			playerRespawnPlacePack.Pack(this);
			return playerRespawnPlacePack;
		}

		// Token: 0x060020E8 RID: 8424 RVA: 0x000D19BB File Offset: 0x000D09BB
		public Color GetInterfaceColor()
		{
			return PlayerRespawnPlace.interfaceColor;
		}

		// Token: 0x060020E9 RID: 8425 RVA: 0x000D19C2 File Offset: 0x000D09C2
		public string GetInterfaceSingleObjectTypeName()
		{
			return PlayerRespawnPlace.interfaceSingleObjectTypeName;
		}

		// Token: 0x060020EA RID: 8426 RVA: 0x000D19C9 File Offset: 0x000D09C9
		public string GetInterfaceSeveralObjectsTypeName()
		{
			return PlayerRespawnPlace.interfaceSeveralObjectsTypeName;
		}

		// Token: 0x060020EB RID: 8427 RVA: 0x000D19D0 File Offset: 0x000D09D0
		public bool ContainsText(string text, bool ignoreCase)
		{
			if (string.IsNullOrEmpty(text))
			{
				return true;
			}
			if (ignoreCase)
			{
				return (!string.IsNullOrEmpty(this.device) && this.device.ToLower().Contains(text.ToLower())) || (!string.IsNullOrEmpty(this.faction) && this.faction.ToLower().Contains(text.ToLower()));
			}
			return (!string.IsNullOrEmpty(this.device) && this.device.Contains(text)) || (!string.IsNullOrEmpty(this.faction) && this.faction.Contains(text));
		}

		// Token: 0x060020EC RID: 8428 RVA: 0x000D1A78 File Offset: 0x000D0A78
		public string GetStatsForDBBrowse()
		{
			return this.device;
		}

		// Token: 0x060020ED RID: 8429 RVA: 0x000D1A80 File Offset: 0x000D0A80
		public string GetSpecialStatsForDBBrowse()
		{
			return this.faction;
		}

		// Token: 0x040013FE RID: 5118
		private static readonly Color interfaceColor = Color.FromArgb(MapObjectCreationInfo.DefaultTransparentColorAlpha, Color.Turquoise);

		// Token: 0x040013FF RID: 5119
		private static readonly string interfaceSingleObjectTypeName = Strings.SINGLE_PLAYER_RESPAWN_PLACE_TYPE_NAME;

		// Token: 0x04001400 RID: 5120
		private static readonly string interfaceSeveralObjectsTypeName = Strings.SEVERAL_PLAYER_RESPAWN_PLACES_TYPE_NAME;

		// Token: 0x04001401 RID: 5121
		private static readonly string defaultVisObject = "Editor/Map/SpecialObjects/PlayerRespawnPlace/PlayerRespawnPlace.(StaticObject).xdb";

		// Token: 0x04001402 RID: 5122
		private static readonly string deviceDBType = "gameMechanics.world.device.SteleResource";

		// Token: 0x04001403 RID: 5123
		private static readonly string factionDBType = "gameMechanics.map.zone.PlayerSpawnPlaceMark";

		// Token: 0x04001404 RID: 5124
		private string device = string.Empty;

		// Token: 0x04001405 RID: 5125
		private string faction = string.Empty;

		// Token: 0x04001406 RID: 5126
		private string mobSceneName = string.Empty;

		// Token: 0x020002C3 RID: 707
		// (Invoke) Token: 0x060020F0 RID: 8432
		public delegate void PlayerRespawnPlaceFieldChangedEvent<T>(PlayerRespawnPlace playerRespawnPlace, ref T oldValue, ref T newValue);
	}
}
