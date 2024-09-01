using System;
using System.ComponentModel;
using System.Drawing;
using MapEditor.Resources.Strings;
using Tools.MapObjects;

namespace MapEditor.Map.MapObjects
{
	// Token: 0x0200003C RID: 60
	public class ClientPatrolNode : MapObject, IMapObjectInterfaceExtention
	{
		// Token: 0x1400000E RID: 14
		// (add) Token: 0x0600037A RID: 890 RVA: 0x0001FB80 File Offset: 0x0001EB80
		// (remove) Token: 0x0600037B RID: 891 RVA: 0x0001FB97 File Offset: 0x0001EB97
		public static event ClientPatrolNode.ClientPatrolNodeFieldChangedEvent<string> SceneChanged;

		// Token: 0x1400000F RID: 15
		// (add) Token: 0x0600037C RID: 892 RVA: 0x0001FBAE File Offset: 0x0001EBAE
		// (remove) Token: 0x0600037D RID: 893 RVA: 0x0001FBC5 File Offset: 0x0001EBC5
		public static event ClientPatrolNode.ClientPatrolNodeFieldChangedEvent<string> ScriptIDChanged;

		// Token: 0x17000071 RID: 113
		// (get) Token: 0x0600037E RID: 894 RVA: 0x0001FBDC File Offset: 0x0001EBDC
		public static Color InterfaceColor
		{
			get
			{
				return ClientPatrolNode.interfaceColor;
			}
		}

		// Token: 0x17000072 RID: 114
		// (get) Token: 0x0600037F RID: 895 RVA: 0x0001FBE3 File Offset: 0x0001EBE3
		public static string InterfaceSingleObjectTypeName
		{
			get
			{
				return ClientPatrolNode.interfaceSingleObjectTypeName;
			}
		}

		// Token: 0x17000073 RID: 115
		// (get) Token: 0x06000380 RID: 896 RVA: 0x0001FBEA File Offset: 0x0001EBEA
		public static string InterfaceSeveralObjectsTypeName
		{
			get
			{
				return ClientPatrolNode.interfaceSeveralObjectsTypeName;
			}
		}

		// Token: 0x17000074 RID: 116
		// (get) Token: 0x06000381 RID: 897 RVA: 0x0001FBF1 File Offset: 0x0001EBF1
		public static string DefaultVisObject
		{
			get
			{
				return ClientPatrolNode.defaultVisObject;
			}
		}

		// Token: 0x06000382 RID: 898 RVA: 0x0001FBF8 File Offset: 0x0001EBF8
		public ClientPatrolNode(int _id, MapObjectType _type, ICollisionMap _collisionMap) : base(_id, _type, _collisionMap)
		{
			base.Highlight(ClientPatrolNode.interfaceColor);
		}

		// Token: 0x17000075 RID: 117
		// (get) Token: 0x06000383 RID: 899 RVA: 0x0001FC24 File Offset: 0x0001EC24
		// (set) Token: 0x06000384 RID: 900 RVA: 0x0001FC2C File Offset: 0x0001EC2C
		[Browsable(true)]
		[Category("ClientPatrolNode")]
		[DisplayName("Scene")]
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
					if (base.Active && ClientPatrolNode.SceneChanged != null)
					{
						ClientPatrolNode.SceneChanged(this, ref oldScene, ref this.scene);
					}
				}
			}
		}

		// Token: 0x17000076 RID: 118
		// (get) Token: 0x06000385 RID: 901 RVA: 0x0001FC86 File Offset: 0x0001EC86
		// (set) Token: 0x06000386 RID: 902 RVA: 0x0001FC90 File Offset: 0x0001EC90
		[Browsable(true)]
		[DisplayName("ScriptID")]
		[Category("ClientPatrolNode")]
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
					if (base.Active && ClientPatrolNode.ScriptIDChanged != null)
					{
						ClientPatrolNode.ScriptIDChanged(this, ref oldScriptID, ref this.scriptID);
					}
				}
			}
		}

		// Token: 0x17000077 RID: 119
		// (get) Token: 0x06000387 RID: 903 RVA: 0x0001FCF2 File Offset: 0x0001ECF2
		[Browsable(false)]
		public override string SceneName
		{
			get
			{
				return ClientPatrolNode.defaultVisObject;
			}
		}

		// Token: 0x06000388 RID: 904 RVA: 0x0001FCFC File Offset: 0x0001ECFC
		public override IMapObject Clone(int newID, bool newTemporary, bool newActive)
		{
			ClientPatrolNode clientPatrolNode = new ClientPatrolNode(newID, new MapObjectType(base.Type.Type, base.Type.Stats), base.CollisionMap);
			clientPatrolNode.scene = this.scene;
			clientPatrolNode.scriptID = this.scriptID;
			base.CopyTo(clientPatrolNode, newTemporary, newActive);
			return clientPatrolNode;
		}

		// Token: 0x06000389 RID: 905 RVA: 0x0001FD5C File Offset: 0x0001ED5C
		public override IMapObjectPack Pack()
		{
			ClientPatrolNodePack clientPatrolNodePack = new ClientPatrolNodePack();
			clientPatrolNodePack.Pack(this);
			return clientPatrolNodePack;
		}

		// Token: 0x0600038A RID: 906 RVA: 0x0001FD77 File Offset: 0x0001ED77
		public Color GetInterfaceColor()
		{
			return ClientPatrolNode.interfaceColor;
		}

		// Token: 0x0600038B RID: 907 RVA: 0x0001FD7E File Offset: 0x0001ED7E
		public string GetInterfaceSingleObjectTypeName()
		{
			return ClientPatrolNode.interfaceSingleObjectTypeName;
		}

		// Token: 0x0600038C RID: 908 RVA: 0x0001FD85 File Offset: 0x0001ED85
		public string GetInterfaceSeveralObjectsTypeName()
		{
			return ClientPatrolNode.interfaceSeveralObjectsTypeName;
		}

		// Token: 0x0600038D RID: 909 RVA: 0x0001FD8C File Offset: 0x0001ED8C
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

		// Token: 0x0600038E RID: 910 RVA: 0x0001FE34 File Offset: 0x0001EE34
		public string GetStatsForDBBrowse()
		{
			return this.scene;
		}

		// Token: 0x0600038F RID: 911 RVA: 0x0001FE3C File Offset: 0x0001EE3C
		public string GetSpecialStatsForDBBrowse()
		{
			return string.Empty;
		}

		// Token: 0x04000291 RID: 657
		private static readonly Color interfaceColor = Color.FromArgb(MapObjectCreationInfo.DefaultTransparentColorAlpha, Color.Green);

		// Token: 0x04000292 RID: 658
		private static readonly string interfaceSingleObjectTypeName = Strings.SINGLE_CLIENT_PATROL_NODE_TYPE_NAME;

		// Token: 0x04000293 RID: 659
		private static readonly string interfaceSeveralObjectsTypeName = Strings.SEVERAL_CLIENT_PATROL_NODES_TYPE_NAME;

		// Token: 0x04000294 RID: 660
		private static readonly string defaultVisObject = "Editor/Map/SpecialObjects/ClientPatrolNode/ClientPatrolNode.(StaticObject).xdb";

		// Token: 0x04000295 RID: 661
		private string scene = string.Empty;

		// Token: 0x04000296 RID: 662
		private string scriptID = string.Empty;

		// Token: 0x0200003D RID: 61
		// (Invoke) Token: 0x06000392 RID: 914
		public delegate void ClientPatrolNodeFieldChangedEvent<T>(ClientPatrolNode clientPatrolNode, ref T oldValue, ref T newValue);
	}
}
