using System;
using System.ComponentModel;
using System.Drawing;
using MapEditor.Resources.Strings;
using Tools.MapObjects;

namespace MapEditor.Map.MapObjects
{
	// Token: 0x02000254 RID: 596
	public class PatrolNode : MapObject, IMapObjectInterfaceExtention
	{
		// Token: 0x140000CF RID: 207
		// (add) Token: 0x06001C51 RID: 7249 RVA: 0x000B645D File Offset: 0x000B545D
		// (remove) Token: 0x06001C52 RID: 7250 RVA: 0x000B6474 File Offset: 0x000B5474
		public static event PatrolNode.PatrolNodeFieldChangedEvent<string> LabelChanged;

		// Token: 0x140000D0 RID: 208
		// (add) Token: 0x06001C53 RID: 7251 RVA: 0x000B648B File Offset: 0x000B548B
		// (remove) Token: 0x06001C54 RID: 7252 RVA: 0x000B64A2 File Offset: 0x000B54A2
		public static event PatrolNode.PatrolNodeFieldChangedEvent<string> ScriptChanged;

		// Token: 0x140000D1 RID: 209
		// (add) Token: 0x06001C55 RID: 7253 RVA: 0x000B64B9 File Offset: 0x000B54B9
		// (remove) Token: 0x06001C56 RID: 7254 RVA: 0x000B64D0 File Offset: 0x000B54D0
		public static event PatrolNode.PatrolNodeFieldChangedEvent<double> AggroRadiusChanged;

		// Token: 0x06001C57 RID: 7255 RVA: 0x000B64E8 File Offset: 0x000B54E8
		public static bool GetAggroRadius(IMapObject mapObject, out double aggroRadius)
		{
			if (mapObject != null)
			{
				if (mapObject.Type.Type == MapObjectFactory.Type.PatrolNode)
				{
					PatrolNode patrolNode = mapObject as PatrolNode;
					if (patrolNode != null)
					{
						aggroRadius = patrolNode.AgrroRadius;
						return aggroRadius > MathConsts.DOUBLE_EPSILON;
					}
				}
				else if (mapObject.Type.Type == MapObjectFactory.Type.RoutePoint)
				{
					RoutePoint routePoint = mapObject as RoutePoint;
					if (routePoint != null && routePoint.RoutePointType == RoutePointType.PatrolNode)
					{
						aggroRadius = routePoint.AgrroRadius;
						return aggroRadius > MathConsts.DOUBLE_EPSILON;
					}
				}
				else if (mapObject.Type.Type == MapObjectFactory.Type.SpawnPoint)
				{
					SpawnPoint spawnPoint = mapObject as SpawnPoint;
					if (spawnPoint != null && (spawnPoint.SpawnPointType == SpawnPointType.Guard || spawnPoint.SpawnPointType == SpawnPointType.Patrol))
					{
						aggroRadius = spawnPoint.AgrroRadius;
						return aggroRadius > MathConsts.DOUBLE_EPSILON;
					}
				}
			}
			aggroRadius = SpawnPoint.EmptyAggroRadius;
			return false;
		}

		// Token: 0x06001C58 RID: 7256 RVA: 0x000B65B8 File Offset: 0x000B55B8
		public static bool GetLabel(IMapObject mapObject, out string label)
		{
			if (mapObject != null)
			{
				if (mapObject.Type.Type == MapObjectFactory.Type.PatrolNode)
				{
					PatrolNode patrolNode = mapObject as PatrolNode;
					if (patrolNode != null)
					{
						label = patrolNode.GetLabel();
						return true;
					}
				}
				else if (mapObject.Type.Type == MapObjectFactory.Type.RoutePoint)
				{
					RoutePoint routePoint = mapObject as RoutePoint;
					if (routePoint != null && routePoint.RoutePointType == RoutePointType.PatrolNode)
					{
						label = routePoint.GetLabel();
						return true;
					}
				}
				else if (mapObject.Type.Type == MapObjectFactory.Type.SpawnPoint)
				{
					SpawnPoint spawnPoint = mapObject as SpawnPoint;
					if (spawnPoint != null && spawnPoint.SpawnPointType == SpawnPointType.Patrol)
					{
						label = spawnPoint.GetLabel();
						return true;
					}
				}
			}
			label = string.Empty;
			return false;
		}

		// Token: 0x06001C59 RID: 7257 RVA: 0x000B6664 File Offset: 0x000B5664
		public static bool SetLabel(IMapObject mapObject, string _label)
		{
			if (mapObject != null)
			{
				if (mapObject.Type.Type == MapObjectFactory.Type.PatrolNode)
				{
					PatrolNode patrolNode = mapObject as PatrolNode;
					if (patrolNode != null)
					{
						patrolNode.SetLabel(_label);
						return true;
					}
				}
				else if (mapObject.Type.Type == MapObjectFactory.Type.RoutePoint)
				{
					RoutePoint routePoint = mapObject as RoutePoint;
					if (routePoint != null && routePoint.RoutePointType == RoutePointType.PatrolNode)
					{
						routePoint.SetLabel(_label);
						return true;
					}
				}
				else if (mapObject.Type.Type == MapObjectFactory.Type.SpawnPoint)
				{
					SpawnPoint spawnPoint = mapObject as SpawnPoint;
					if (spawnPoint != null)
					{
						spawnPoint.SetLabel(_label);
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x06001C5A RID: 7258 RVA: 0x000B66FC File Offset: 0x000B56FC
		public static bool GetScript(IMapObject mapObject, out string script)
		{
			if (mapObject != null)
			{
				if (mapObject.Type.Type == MapObjectFactory.Type.PatrolNode)
				{
					PatrolNode patrolNode = mapObject as PatrolNode;
					if (patrolNode != null)
					{
						script = patrolNode.GetScript();
						return true;
					}
				}
				else if (mapObject.Type.Type == MapObjectFactory.Type.RoutePoint)
				{
					RoutePoint routePoint = mapObject as RoutePoint;
					if (routePoint != null && routePoint.RoutePointType == RoutePointType.PatrolNode)
					{
						script = routePoint.GetScript();
						return true;
					}
				}
				else if (mapObject.Type.Type == MapObjectFactory.Type.SpawnPoint)
				{
					SpawnPoint spawnPoint = mapObject as SpawnPoint;
					if (spawnPoint != null && spawnPoint.SpawnPointType == SpawnPointType.Patrol)
					{
						script = spawnPoint.GetScript();
						return true;
					}
				}
			}
			script = string.Empty;
			return false;
		}

		// Token: 0x06001C5B RID: 7259 RVA: 0x000B67A8 File Offset: 0x000B57A8
		public static bool SetScript(IMapObject mapObject, string _script)
		{
			if (mapObject != null)
			{
				if (mapObject.Type.Type == MapObjectFactory.Type.PatrolNode)
				{
					PatrolNode patrolNode = mapObject as PatrolNode;
					if (patrolNode != null)
					{
						patrolNode.SetScript(_script);
						return true;
					}
				}
				else if (mapObject.Type.Type == MapObjectFactory.Type.RoutePoint)
				{
					RoutePoint routePoint = mapObject as RoutePoint;
					if (routePoint != null && routePoint.RoutePointType == RoutePointType.PatrolNode)
					{
						routePoint.SetScript(_script);
						return true;
					}
				}
				else if (mapObject.Type.Type == MapObjectFactory.Type.SpawnPoint)
				{
					SpawnPoint spawnPoint = mapObject as SpawnPoint;
					if (spawnPoint != null)
					{
						spawnPoint.SetScript(_script);
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x170006A7 RID: 1703
		// (get) Token: 0x06001C5C RID: 7260 RVA: 0x000B683F File Offset: 0x000B583F
		public static Color InterfaceColor
		{
			get
			{
				return PatrolNode.interfaceColor;
			}
		}

		// Token: 0x170006A8 RID: 1704
		// (get) Token: 0x06001C5D RID: 7261 RVA: 0x000B6846 File Offset: 0x000B5846
		public static string InterfaceSingleObjectTypeName
		{
			get
			{
				return PatrolNode.interfaceSingleObjectTypeName;
			}
		}

		// Token: 0x170006A9 RID: 1705
		// (get) Token: 0x06001C5E RID: 7262 RVA: 0x000B684D File Offset: 0x000B584D
		public static string InterfaceSeveralObjectsTypeName
		{
			get
			{
				return PatrolNode.interfaceSeveralObjectsTypeName;
			}
		}

		// Token: 0x170006AA RID: 1706
		// (get) Token: 0x06001C5F RID: 7263 RVA: 0x000B6854 File Offset: 0x000B5854
		public static string DefaultVisObject
		{
			get
			{
				return PatrolNode.defaultVisObject;
			}
		}

		// Token: 0x170006AB RID: 1707
		// (get) Token: 0x06001C60 RID: 7264 RVA: 0x000B685B File Offset: 0x000B585B
		public static string ScriptFolder
		{
			get
			{
				return PatrolNode.scriptFolder;
			}
		}

		// Token: 0x170006AC RID: 1708
		// (get) Token: 0x06001C61 RID: 7265 RVA: 0x000B6862 File Offset: 0x000B5862
		public static string ScriptDBType
		{
			get
			{
				return PatrolNode.scriptDBType;
			}
		}

		// Token: 0x06001C62 RID: 7266 RVA: 0x000B6869 File Offset: 0x000B5869
		public PatrolNode(int _id, MapObjectType _type, ICollisionMap _collisionMap) : base(_id, _type, _collisionMap)
		{
		}

		// Token: 0x170006AD RID: 1709
		// (get) Token: 0x06001C63 RID: 7267 RVA: 0x000B6895 File Offset: 0x000B5895
		// (set) Token: 0x06001C64 RID: 7268 RVA: 0x000B68A0 File Offset: 0x000B58A0
		[DisplayName("Label")]
		[Browsable(true)]
		[Category("PatrolNode")]
		public string Label
		{
			get
			{
				return this.label;
			}
			set
			{
				if (this.label != value && base.InvokeChanging(null))
				{
					string oldLabel = this.label;
					this.label = value;
					base.InvokeChanged();
					if (base.Active && PatrolNode.LabelChanged != null)
					{
						PatrolNode.LabelChanged(this, ref oldLabel, ref this.label);
					}
				}
			}
		}

		// Token: 0x170006AE RID: 1710
		// (get) Token: 0x06001C65 RID: 7269 RVA: 0x000B68FA File Offset: 0x000B58FA
		// (set) Token: 0x06001C66 RID: 7270 RVA: 0x000B6904 File Offset: 0x000B5904
		[DisplayName("Script")]
		[Browsable(true)]
		[Category("PatrolNode")]
		public string Script
		{
			get
			{
				return this.script;
			}
			set
			{
				if (this.script != value && base.InvokeChanging(null))
				{
					string oldScript = this.script;
					this.script = value;
					base.InvokeChanged();
					if (base.Active && PatrolNode.ScriptChanged != null)
					{
						PatrolNode.ScriptChanged(this, ref oldScript, ref this.script);
					}
				}
			}
		}

		// Token: 0x170006AF RID: 1711
		// (get) Token: 0x06001C67 RID: 7271 RVA: 0x000B695E File Offset: 0x000B595E
		[DisplayName("AggroRadius")]
		[Browsable(true)]
		[Category("PatrolNode")]
		public double AgrroRadius
		{
			get
			{
				return this.aggroRadius;
			}
		}

		// Token: 0x170006B0 RID: 1712
		// (get) Token: 0x06001C68 RID: 7272 RVA: 0x000B6966 File Offset: 0x000B5966
		[Browsable(false)]
		public override string SceneName
		{
			get
			{
				return PatrolNode.defaultVisObject;
			}
		}

		// Token: 0x06001C69 RID: 7273 RVA: 0x000B6970 File Offset: 0x000B5970
		public override IMapObject Clone(int newID, bool newTemporary, bool newActive)
		{
			PatrolNode patrolNode = new PatrolNode(newID, new MapObjectType(base.Type.Type, string.Empty), base.CollisionMap);
			patrolNode.script = this.script;
			base.CopyTo(patrolNode, newTemporary, newActive);
			return patrolNode;
		}

		// Token: 0x06001C6A RID: 7274 RVA: 0x000B69B8 File Offset: 0x000B59B8
		public override IMapObjectPack Pack()
		{
			PatrolNodePack patrolNodePack = new PatrolNodePack();
			patrolNodePack.Pack(this);
			return patrolNodePack;
		}

		// Token: 0x06001C6B RID: 7275 RVA: 0x000B69D3 File Offset: 0x000B59D3
		public Color GetInterfaceColor()
		{
			return PatrolNode.interfaceColor;
		}

		// Token: 0x06001C6C RID: 7276 RVA: 0x000B69DA File Offset: 0x000B59DA
		public string GetInterfaceSingleObjectTypeName()
		{
			return PatrolNode.interfaceSingleObjectTypeName;
		}

		// Token: 0x06001C6D RID: 7277 RVA: 0x000B69E1 File Offset: 0x000B59E1
		public string GetInterfaceSeveralObjectsTypeName()
		{
			return PatrolNode.interfaceSeveralObjectsTypeName;
		}

		// Token: 0x06001C6E RID: 7278 RVA: 0x000B69E8 File Offset: 0x000B59E8
		public bool ContainsText(string text, bool ignoreCase)
		{
			if (string.IsNullOrEmpty(text))
			{
				return true;
			}
			if (ignoreCase)
			{
				return !string.IsNullOrEmpty(this.script) && this.script.ToLower().Contains(text.ToLower());
			}
			return !string.IsNullOrEmpty(this.script) && this.script.Contains(text);
		}

		// Token: 0x06001C6F RID: 7279 RVA: 0x000B6A49 File Offset: 0x000B5A49
		public string GetStatsForDBBrowse()
		{
			return string.Empty;
		}

		// Token: 0x06001C70 RID: 7280 RVA: 0x000B6A50 File Offset: 0x000B5A50
		public string GetSpecialStatsForDBBrowse()
		{
			return string.Empty;
		}

		// Token: 0x06001C71 RID: 7281 RVA: 0x000B6A57 File Offset: 0x000B5A57
		public string GetLabel()
		{
			return this.label;
		}

		// Token: 0x06001C72 RID: 7282 RVA: 0x000B6A5F File Offset: 0x000B5A5F
		public void SetLabel(string _label)
		{
			this.Label = _label;
		}

		// Token: 0x06001C73 RID: 7283 RVA: 0x000B6A68 File Offset: 0x000B5A68
		public string GetScript()
		{
			return this.script;
		}

		// Token: 0x06001C74 RID: 7284 RVA: 0x000B6A70 File Offset: 0x000B5A70
		public void SetScript(string _script)
		{
			this.Script = _script;
		}

		// Token: 0x06001C75 RID: 7285 RVA: 0x000B6A7C File Offset: 0x000B5A7C
		public void SetAggroRadius(double newAggroRadius)
		{
			if (this.aggroRadius != newAggroRadius && base.InvokeChanging(null))
			{
				double oldAggroRadius = this.aggroRadius;
				this.aggroRadius = newAggroRadius;
				base.InvokeChanged();
				if (base.Active && PatrolNode.AggroRadiusChanged != null)
				{
					PatrolNode.AggroRadiusChanged(this, ref oldAggroRadius, ref this.aggroRadius);
				}
			}
		}

		// Token: 0x04001230 RID: 4656
		private static readonly Color interfaceColor = Color.FromArgb(MapObjectCreationInfo.DefaultTransparentColorAlpha, Color.Black);

		// Token: 0x04001231 RID: 4657
		private static readonly string interfaceSingleObjectTypeName = Strings.SINGLE_PATROL_NODE_TYPE_NAME;

		// Token: 0x04001232 RID: 4658
		private static readonly string interfaceSeveralObjectsTypeName = Strings.SEVERAL_PATROL_NODES_TYPE_NAME;

		// Token: 0x04001233 RID: 4659
		private string label = string.Empty;

		// Token: 0x04001234 RID: 4660
		private string script = string.Empty;

		// Token: 0x04001235 RID: 4661
		private double aggroRadius = SpawnPoint.EmptyAggroRadius;

		// Token: 0x04001236 RID: 4662
		private static readonly string defaultVisObject = "Editor/Map/SpecialObjects/PatrolNode/PatrolNode.(StaticObject).xdb";

		// Token: 0x04001237 RID: 4663
		private static readonly string scriptFolder = "PatrolScripts/";

		// Token: 0x04001238 RID: 4664
		private static readonly string scriptDBType = "gameMechanics.map.spawn.patrol.ScriptResource";

		// Token: 0x02000255 RID: 597
		// (Invoke) Token: 0x06001C78 RID: 7288
		public delegate void PatrolNodeFieldChangedEvent<T>(PatrolNode patrolNode, ref T oldValue, ref T newValue);
	}
}
