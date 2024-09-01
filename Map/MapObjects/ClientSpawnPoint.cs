using System;
using System.ComponentModel;
using System.Drawing;
using Db;
using MapEditor.Resources.Strings;
using Tools.MapObjects;
using Tools.SafeObjMan;

namespace MapEditor.Map.MapObjects
{
	// Token: 0x0200022C RID: 556
	public class ClientSpawnPoint : MapObject, IMapObjectInterfaceExtention
	{
		// Token: 0x140000B8 RID: 184
		// (add) Token: 0x06001A9B RID: 6811 RVA: 0x000AEA85 File Offset: 0x000ADA85
		// (remove) Token: 0x06001A9C RID: 6812 RVA: 0x000AEA9C File Offset: 0x000ADA9C
		public static event ClientSpawnPoint.ClientSpawnPointFieldChangedEvent<string> VisObjectChanged;

		// Token: 0x140000B9 RID: 185
		// (add) Token: 0x06001A9D RID: 6813 RVA: 0x000AEAB3 File Offset: 0x000ADAB3
		// (remove) Token: 0x06001A9E RID: 6814 RVA: 0x000AEACA File Offset: 0x000ADACA
		public static event ClientSpawnPoint.ClientSpawnPointFieldChangedEvent<string> SceneChanged;

		// Token: 0x140000BA RID: 186
		// (add) Token: 0x06001A9F RID: 6815 RVA: 0x000AEAE1 File Offset: 0x000ADAE1
		// (remove) Token: 0x06001AA0 RID: 6816 RVA: 0x000AEAF8 File Offset: 0x000ADAF8
		public static event ClientSpawnPoint.ClientSpawnPointFieldChangedEvent<string> ScriptIDChanged;

		// Token: 0x140000BB RID: 187
		// (add) Token: 0x06001AA1 RID: 6817 RVA: 0x000AEB0F File Offset: 0x000ADB0F
		// (remove) Token: 0x06001AA2 RID: 6818 RVA: 0x000AEB26 File Offset: 0x000ADB26
		public static event ClientSpawnPoint.ClientSpawnPointFieldChangedEvent<ClientSpawnPointData> DataChanged;

		// Token: 0x06001AA3 RID: 6819 RVA: 0x000AEB40 File Offset: 0x000ADB40
		public static string GetFixedIdleAnimationName(ClientSpawnPoint сlientSpawnPoint, IDatabase mainDb)
		{
			if (сlientSpawnPoint != null && !string.IsNullOrEmpty(сlientSpawnPoint.VisObject))
			{
				DBID visualObjectDBID = mainDb.GetDBIDByName(сlientSpawnPoint.VisObject);
				IObjMan visMobMan = mainDb.GetManipulator(visualObjectDBID);
				if (visMobMan != null & string.Equals(mainDb.GetClassTypeName(visualObjectDBID), ClientSpawnPoint.visObjectDBType))
				{
					string animation = SafeObjMan.GetString(visMobMan, "fixedIdleAnimation");
					if (string.IsNullOrEmpty(animation) || string.Equals(animation, ClientSpawnPoint.invalidAnimationName))
					{
						animation = ClientSpawnPoint.defaultIdleAnimationName;
					}
					return animation;
				}
			}
			return string.Empty;
		}

		// Token: 0x17000648 RID: 1608
		// (get) Token: 0x06001AA4 RID: 6820 RVA: 0x000AEBBB File Offset: 0x000ADBBB
		public static Color InterfaceColor
		{
			get
			{
				return ClientSpawnPoint.interfaceColor;
			}
		}

		// Token: 0x17000649 RID: 1609
		// (get) Token: 0x06001AA5 RID: 6821 RVA: 0x000AEBC2 File Offset: 0x000ADBC2
		public static string InterfaceSingleObjectTypeName
		{
			get
			{
				return ClientSpawnPoint.interfaceSingleObjectTypeName;
			}
		}

		// Token: 0x1700064A RID: 1610
		// (get) Token: 0x06001AA6 RID: 6822 RVA: 0x000AEBC9 File Offset: 0x000ADBC9
		public static string InterfaceSeveralObjectsTypeName
		{
			get
			{
				return ClientSpawnPoint.interfaceSeveralObjectsTypeName;
			}
		}

		// Token: 0x1700064B RID: 1611
		// (get) Token: 0x06001AA7 RID: 6823 RVA: 0x000AEBD0 File Offset: 0x000ADBD0
		public static string DefaultVisObject
		{
			get
			{
				return ClientSpawnPoint.defaultVisObject;
			}
		}

		// Token: 0x1700064C RID: 1612
		// (get) Token: 0x06001AA8 RID: 6824 RVA: 0x000AEBD7 File Offset: 0x000ADBD7
		public static string SceneDBType
		{
			get
			{
				return ClientSpawnPoint.sceneDBType;
			}
		}

		// Token: 0x1700064D RID: 1613
		// (get) Token: 0x06001AA9 RID: 6825 RVA: 0x000AEBDE File Offset: 0x000ADBDE
		public static string VisObjectDBType
		{
			get
			{
				return ClientSpawnPoint.visObjectDBType;
			}
		}

		// Token: 0x1700064E RID: 1614
		// (get) Token: 0x06001AAA RID: 6826 RVA: 0x000AEBE5 File Offset: 0x000ADBE5
		public static string SceneFolder
		{
			get
			{
				return ClientSpawnPoint.sceneFolder;
			}
		}

		// Token: 0x06001AAB RID: 6827 RVA: 0x000AEBEC File Offset: 0x000ADBEC
		public ClientSpawnPoint(int _id, MapObjectType _type, ICollisionMap _collisionMap) : base(_id, _type, _collisionMap)
		{
			base.Highlight(ClientSpawnPoint.interfaceColor);
			this.clientSpawnPointData = ClientSpawnPoint.defaultSpawnPointData;
			this.visObject = base.Type.Stats;
		}

		// Token: 0x1700064F RID: 1615
		// (get) Token: 0x06001AAC RID: 6828 RVA: 0x000AEC4D File Offset: 0x000ADC4D
		// (set) Token: 0x06001AAD RID: 6829 RVA: 0x000AEC58 File Offset: 0x000ADC58
		[DisplayName("VisObject")]
		[Category("ClientSpawnPoint")]
		[Browsable(true)]
		public string VisObject
		{
			get
			{
				return this.visObject;
			}
			set
			{
				if (this.visObject != value && base.InvokeChanging(null))
				{
					string oldVisObject = this.visObject;
					this.visObject = value;
					base.SetNewStats(this.visObject);
					base.InvokeChanged();
					if (base.Active && ClientSpawnPoint.VisObjectChanged != null)
					{
						ClientSpawnPoint.VisObjectChanged(this, ref oldVisObject, ref this.visObject);
					}
				}
			}
		}

		// Token: 0x17000650 RID: 1616
		// (get) Token: 0x06001AAE RID: 6830 RVA: 0x000AECBE File Offset: 0x000ADCBE
		// (set) Token: 0x06001AAF RID: 6831 RVA: 0x000AECC8 File Offset: 0x000ADCC8
		[DisplayName("Scene")]
		[Category("ClientSpawnPoint")]
		[Browsable(true)]
		public string Scene
		{
			get
			{
				return this.scene;
			}
			set
			{
				if (this.scene != value && base.InvokeChanging(null))
				{
					string oldScene = this.scene;
					this.scene = value;
					base.InvokeChanged();
					if (base.Active && ClientSpawnPoint.SceneChanged != null)
					{
						ClientSpawnPoint.SceneChanged(this, ref oldScene, ref this.scene);
					}
				}
			}
		}

		// Token: 0x17000651 RID: 1617
		// (get) Token: 0x06001AB0 RID: 6832 RVA: 0x000AED22 File Offset: 0x000ADD22
		// (set) Token: 0x06001AB1 RID: 6833 RVA: 0x000AED2C File Offset: 0x000ADD2C
		[DisplayName("ScriptID")]
		[Browsable(true)]
		[Category("ClientSpawnPoint")]
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
					if (base.Active && ClientSpawnPoint.ScriptIDChanged != null)
					{
						ClientSpawnPoint.ScriptIDChanged(this, ref oldScriptID, ref this.scriptID);
					}
				}
			}
		}

		// Token: 0x17000652 RID: 1618
		// (get) Token: 0x06001AB2 RID: 6834 RVA: 0x000AED8E File Offset: 0x000ADD8E
		// (set) Token: 0x06001AB3 RID: 6835 RVA: 0x000AED98 File Offset: 0x000ADD98
		[DisplayName("Data")]
		[TypeConverter(typeof(ClientSpawnPointDataConverter))]
		[Browsable(true)]
		[Category("ClientSpawnPoint")]
		public ClientSpawnPointData ClientSpawnPointData
		{
			get
			{
				return this.clientSpawnPointData;
			}
			set
			{
				if (this.clientSpawnPointData != value && base.InvokeChanging(null))
				{
					ClientSpawnPointData oldClientSpawnPointData = this.clientSpawnPointData;
					this.clientSpawnPointData = value;
					base.InvokeChanged();
					if (base.Active && ClientSpawnPoint.DataChanged != null)
					{
						ClientSpawnPoint.DataChanged(this, ref oldClientSpawnPointData, ref this.clientSpawnPointData);
					}
				}
			}
		}

		// Token: 0x17000653 RID: 1619
		// (get) Token: 0x06001AB4 RID: 6836 RVA: 0x000AEDED File Offset: 0x000ADDED
		[Browsable(false)]
		public override string SceneName
		{
			get
			{
				if (string.IsNullOrEmpty(this.VisObject))
				{
					return ClientSpawnPoint.defaultVisObject;
				}
				return this.VisObject;
			}
		}

		// Token: 0x06001AB5 RID: 6837 RVA: 0x000AEE08 File Offset: 0x000ADE08
		public override IMapObject Clone(int newID, bool newTemporary, bool newActive)
		{
			ClientSpawnPoint clientSpawnPoint = new ClientSpawnPoint(newID, new MapObjectType(base.Type.Type, base.Type.Stats), base.CollisionMap);
			clientSpawnPoint.clientSpawnPointData = this.clientSpawnPointData;
			clientSpawnPoint.scene = this.scene;
			clientSpawnPoint.scriptID = this.scriptID;
			base.CopyTo(clientSpawnPoint, newTemporary, newActive);
			return clientSpawnPoint;
		}

		// Token: 0x06001AB6 RID: 6838 RVA: 0x000AEE74 File Offset: 0x000ADE74
		public override IMapObjectPack Pack()
		{
			ClientSpawnPointPack clientSpawnPointPack = new ClientSpawnPointPack();
			clientSpawnPointPack.Pack(this);
			return clientSpawnPointPack;
		}

		// Token: 0x06001AB7 RID: 6839 RVA: 0x000AEE8F File Offset: 0x000ADE8F
		public Color GetInterfaceColor()
		{
			return ClientSpawnPoint.interfaceColor;
		}

		// Token: 0x06001AB8 RID: 6840 RVA: 0x000AEE96 File Offset: 0x000ADE96
		public string GetInterfaceSingleObjectTypeName()
		{
			return ClientSpawnPoint.interfaceSingleObjectTypeName;
		}

		// Token: 0x06001AB9 RID: 6841 RVA: 0x000AEE9D File Offset: 0x000ADE9D
		public string GetInterfaceSeveralObjectsTypeName()
		{
			return ClientSpawnPoint.interfaceSeveralObjectsTypeName;
		}

		// Token: 0x06001ABA RID: 6842 RVA: 0x000AEEA4 File Offset: 0x000ADEA4
		public bool ContainsText(string text, bool ignoreCase)
		{
			if (string.IsNullOrEmpty(text))
			{
				return true;
			}
			if (ignoreCase)
			{
				return (!string.IsNullOrEmpty(this.scene) && this.scene.ToLower().Contains(text.ToLower())) || (!string.IsNullOrEmpty(this.scriptID) && this.scriptID.ToLower().Contains(text.ToLower()));
			}
			return (!string.IsNullOrEmpty(this.scene) && this.scene.Contains(text)) || (!string.IsNullOrEmpty(this.scriptID) && this.scriptID.Contains(text));
		}

		// Token: 0x06001ABB RID: 6843 RVA: 0x000AEF4C File Offset: 0x000ADF4C
		public string GetStatsForDBBrowse()
		{
			return this.scene;
		}

		// Token: 0x06001ABC RID: 6844 RVA: 0x000AEF54 File Offset: 0x000ADF54
		public string GetSpecialStatsForDBBrowse()
		{
			return this.VisObject;
		}

		// Token: 0x04001129 RID: 4393
		private static readonly Color interfaceColor = Color.FromArgb(MapObjectCreationInfo.DefaultTransparentColorAlpha, Color.Green);

		// Token: 0x0400112A RID: 4394
		private static readonly string interfaceSingleObjectTypeName = Strings.SINGLE_CLIENT_SPAWN_POINT_TYPE_NAME;

		// Token: 0x0400112B RID: 4395
		private static readonly string interfaceSeveralObjectsTypeName = Strings.SEVERAL_CLIENT_SPAWN_POINTS_TYPE_NAME;

		// Token: 0x0400112C RID: 4396
		private static readonly string defaultVisObject = "Editor/Map/SpecialObjects/ClientSpawnPoint/ClientSpawnPoint.(StaticObject).xdb";

		// Token: 0x0400112D RID: 4397
		private static readonly string sceneDBType = "GameViewScene";

		// Token: 0x0400112E RID: 4398
		private static readonly string visObjectDBType = "VisualMob";

		// Token: 0x0400112F RID: 4399
		private static readonly string defaultIdleAnimationName = "idle";

		// Token: 0x04001130 RID: 4400
		private static readonly string invalidAnimationName = "invalid";

		// Token: 0x04001131 RID: 4401
		private static readonly string sceneFolder = "GlobalObjects/GameViewScenes/";

		// Token: 0x04001132 RID: 4402
		private static readonly ClientSpawnPointData defaultSpawnPointData = new MobClientSpawnPointData();

		// Token: 0x04001133 RID: 4403
		private string visObject = string.Empty;

		// Token: 0x04001134 RID: 4404
		private string scene = string.Empty;

		// Token: 0x04001135 RID: 4405
		private string scriptID = string.Empty;

		// Token: 0x04001136 RID: 4406
		private ClientSpawnPointData clientSpawnPointData;

		// Token: 0x0200022D RID: 557
		// (Invoke) Token: 0x06001ABF RID: 6847
		public delegate void ClientSpawnPointFieldChangedEvent<T>(ClientSpawnPoint clientSpawnPoint, ref T oldValue, ref T newValue);
	}
}
