using System;
using System.ComponentModel;
using System.Drawing;
using MapEditor.Resources.Strings;
using Tools.MapObjects;

namespace MapEditor.Map.MapObjects
{
	// Token: 0x020001D4 RID: 468
	public class ScriptArea : MapObject, IMapObjectInterfaceExtention
	{
		// Token: 0x140000AA RID: 170
		// (add) Token: 0x060017D7 RID: 6103 RVA: 0x000A1651 File Offset: 0x000A0651
		// (remove) Token: 0x060017D8 RID: 6104 RVA: 0x000A1668 File Offset: 0x000A0668
		public static event ScriptArea.ScriptAreaFieldChangedEvent<ScriptAreaData> TypeChanged;

		// Token: 0x140000AB RID: 171
		// (add) Token: 0x060017D9 RID: 6105 RVA: 0x000A167F File Offset: 0x000A067F
		// (remove) Token: 0x060017DA RID: 6106 RVA: 0x000A1696 File Offset: 0x000A0696
		public static event ScriptArea.ScriptAreaFieldChangedEvent<ScriptAreaData> DataChanged;

		// Token: 0x140000AC RID: 172
		// (add) Token: 0x060017DB RID: 6107 RVA: 0x000A16AD File Offset: 0x000A06AD
		// (remove) Token: 0x060017DC RID: 6108 RVA: 0x000A16C4 File Offset: 0x000A06C4
		public static event ScriptArea.ScriptAreaFieldChangedEvent<string> ScriptZoneChanged;

		// Token: 0x140000AD RID: 173
		// (add) Token: 0x060017DD RID: 6109 RVA: 0x000A16DB File Offset: 0x000A06DB
		// (remove) Token: 0x060017DE RID: 6110 RVA: 0x000A16F2 File Offset: 0x000A06F2
		public static event ScriptArea.ScriptAreaFieldChangedEvent<string> ScriptIDChanged;

		// Token: 0x140000AE RID: 174
		// (add) Token: 0x060017DF RID: 6111 RVA: 0x000A1709 File Offset: 0x000A0709
		// (remove) Token: 0x060017E0 RID: 6112 RVA: 0x000A1720 File Offset: 0x000A0720
		public static event ScriptArea.ScriptAreaFieldChangedEvent<double> ScanRadiusChanged;

		// Token: 0x170005B9 RID: 1465
		// (get) Token: 0x060017E1 RID: 6113 RVA: 0x000A1737 File Offset: 0x000A0737
		public static Color InterfaceColor
		{
			get
			{
				return ScriptArea.interfaceColor;
			}
		}

		// Token: 0x170005BA RID: 1466
		// (get) Token: 0x060017E2 RID: 6114 RVA: 0x000A173E File Offset: 0x000A073E
		public static string InterfaceSingleObjectTypeName
		{
			get
			{
				return ScriptArea.interfaceSingleObjectTypeName;
			}
		}

		// Token: 0x170005BB RID: 1467
		// (get) Token: 0x060017E3 RID: 6115 RVA: 0x000A1745 File Offset: 0x000A0745
		public static string InterfaceSeveralObjectsTypeName
		{
			get
			{
				return ScriptArea.interfaceSeveralObjectsTypeName;
			}
		}

		// Token: 0x170005BC RID: 1468
		// (get) Token: 0x060017E4 RID: 6116 RVA: 0x000A174C File Offset: 0x000A074C
		public static string DefaultVisObject
		{
			get
			{
				return ScriptArea.defaultVisObject;
			}
		}

		// Token: 0x170005BD RID: 1469
		// (get) Token: 0x060017E5 RID: 6117 RVA: 0x000A1753 File Offset: 0x000A0753
		public static string ScriptZoneDBType
		{
			get
			{
				return ScriptArea.scriptZoneDBType;
			}
		}

		// Token: 0x060017E6 RID: 6118 RVA: 0x000A175A File Offset: 0x000A075A
		public ScriptArea(int _id, MapObjectType _type, ICollisionMap _collisionMap, ScriptAreaType scriptAreaType) : base(_id, _type, _collisionMap)
		{
			this.scriptZone = _type.Stats;
			this.scriptAreaData = ScriptAreaData.Create(scriptAreaType, this);
		}

		// Token: 0x170005BE RID: 1470
		// (get) Token: 0x060017E7 RID: 6119 RVA: 0x000A1796 File Offset: 0x000A0796
		// (set) Token: 0x060017E8 RID: 6120 RVA: 0x000A17B0 File Offset: 0x000A07B0
		[DisplayName("Type")]
		[RefreshProperties(RefreshProperties.All)]
		[Browsable(true)]
		[Category("ScriptArea")]
		public ScriptAreaType ScriptAreaType
		{
			get
			{
				if (this.scriptAreaData != null)
				{
					return this.scriptAreaData.ScriptAreaType;
				}
				return ScriptAreaType.Undefined;
			}
			set
			{
				if (this.scriptAreaData.ScriptAreaType != value)
				{
					ScriptAreaData newScriptAreaData = ScriptAreaData.Create(value, this);
					if (newScriptAreaData != null && base.InvokeChanging(null))
					{
						ScriptAreaData oldScriptAreaData = this.scriptAreaData;
						this.scriptAreaData = newScriptAreaData;
						base.InvokeChanged();
						if (base.Active && ScriptArea.TypeChanged != null)
						{
							ScriptArea.TypeChanged(this, ref oldScriptAreaData, ref newScriptAreaData);
						}
					}
				}
			}
		}

		// Token: 0x170005BF RID: 1471
		// (get) Token: 0x060017E9 RID: 6121 RVA: 0x000A1811 File Offset: 0x000A0811
		// (set) Token: 0x060017EA RID: 6122 RVA: 0x000A181C File Offset: 0x000A081C
		[Category("ScriptArea")]
		[Browsable(true)]
		public string ScriptZone
		{
			get
			{
				return this.scriptZone;
			}
			set
			{
				if (this.scriptZone != value && base.InvokeChanging(null))
				{
					string oldScriptZone = this.scriptZone;
					this.scriptZone = value;
					base.SetNewStats(this.scriptZone);
					base.InvokeChanged();
					if (base.Active && ScriptArea.ScriptZoneChanged != null)
					{
						ScriptArea.ScriptZoneChanged(this, ref oldScriptZone, ref this.scriptZone);
					}
				}
			}
		}

		// Token: 0x170005C0 RID: 1472
		// (get) Token: 0x060017EB RID: 6123 RVA: 0x000A1882 File Offset: 0x000A0882
		// (set) Token: 0x060017EC RID: 6124 RVA: 0x000A188C File Offset: 0x000A088C
		[Category("ScriptArea")]
		[DisplayName("ScriptID")]
		[Browsable(true)]
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
					if (base.Active && ScriptArea.ScriptIDChanged != null)
					{
						ScriptArea.ScriptIDChanged(this, ref oldScriptID, ref this.scriptID);
					}
				}
			}
		}

		// Token: 0x170005C1 RID: 1473
		// (get) Token: 0x060017ED RID: 6125 RVA: 0x000A18EE File Offset: 0x000A08EE
		// (set) Token: 0x060017EE RID: 6126 RVA: 0x000A18F8 File Offset: 0x000A08F8
		[Browsable(true)]
		[DisplayName("ScanRadius")]
		[Category("ScriptArea")]
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
					if (base.Active && ScriptArea.ScanRadiusChanged != null)
					{
						ScriptArea.ScanRadiusChanged(this, ref oldScanRadius, ref this.scanRadius);
					}
				}
			}
		}

		// Token: 0x170005C2 RID: 1474
		// (get) Token: 0x060017EF RID: 6127 RVA: 0x000A194D File Offset: 0x000A094D
		// (set) Token: 0x060017F0 RID: 6128 RVA: 0x000A1958 File Offset: 0x000A0958
		[TypeConverter(typeof(ScriptAreaDataConverter))]
		[Browsable(true)]
		[DisplayName("Data")]
		[Category("ScriptArea")]
		public ScriptAreaData ScriptAreaData
		{
			get
			{
				return this.scriptAreaData;
			}
			set
			{
				if (base.InvokeChanging(null))
				{
					ScriptAreaData oldScriptAreaData = this.scriptAreaData;
					this.scriptAreaData = value;
					base.InvokeChanged();
					if (base.Active && ScriptArea.DataChanged != null)
					{
						ScriptArea.DataChanged(this, ref oldScriptAreaData, ref this.scriptAreaData);
					}
				}
			}
		}

		// Token: 0x170005C3 RID: 1475
		// (get) Token: 0x060017F1 RID: 6129 RVA: 0x000A19A4 File Offset: 0x000A09A4
		[Browsable(false)]
		public override string SceneName
		{
			get
			{
				return ScriptArea.defaultVisObject;
			}
		}

		// Token: 0x060017F2 RID: 6130 RVA: 0x000A19AC File Offset: 0x000A09AC
		public override IMapObject Clone(int newID, bool newTemporary, bool newActive)
		{
			ScriptArea scriptArea = new ScriptArea(newID, new MapObjectType(base.Type.Type, this.scriptZone), base.CollisionMap, this.ScriptAreaType);
			scriptArea.scriptID = this.scriptID;
			scriptArea.scanRadius = this.scanRadius;
			base.CopyTo(scriptArea, newTemporary, newActive);
			if (scriptArea.scriptAreaData != null && this.scriptAreaData != null)
			{
				scriptArea.scriptAreaData.CopyFrom(this.scriptAreaData);
			}
			return scriptArea;
		}

		// Token: 0x060017F3 RID: 6131 RVA: 0x000A1A28 File Offset: 0x000A0A28
		public override IMapObjectPack Pack()
		{
			ScriptAreaPack scriptAreaPack = new ScriptAreaPack();
			scriptAreaPack.Pack(this);
			return scriptAreaPack;
		}

		// Token: 0x060017F4 RID: 6132 RVA: 0x000A1A43 File Offset: 0x000A0A43
		public Color GetInterfaceColor()
		{
			return ScriptArea.interfaceColor;
		}

		// Token: 0x060017F5 RID: 6133 RVA: 0x000A1A4A File Offset: 0x000A0A4A
		public string GetInterfaceSingleObjectTypeName()
		{
			return ScriptArea.interfaceSingleObjectTypeName;
		}

		// Token: 0x060017F6 RID: 6134 RVA: 0x000A1A51 File Offset: 0x000A0A51
		public string GetInterfaceSeveralObjectsTypeName()
		{
			return ScriptArea.interfaceSeveralObjectsTypeName;
		}

		// Token: 0x060017F7 RID: 6135 RVA: 0x000A1A58 File Offset: 0x000A0A58
		public bool ContainsText(string text, bool ignoreCase)
		{
			if (string.IsNullOrEmpty(text))
			{
				return true;
			}
			if (ignoreCase)
			{
				return (!string.IsNullOrEmpty(this.scriptZone) && this.scriptZone.ToLower().Contains(text.ToLower())) || (!string.IsNullOrEmpty(this.scriptID) && this.scriptID.ToLower().Contains(text.ToLower()));
			}
			return (!string.IsNullOrEmpty(this.scriptZone) && this.scriptZone.Contains(text)) || (!string.IsNullOrEmpty(this.scriptID) && this.scriptID.Contains(text));
		}

		// Token: 0x060017F8 RID: 6136 RVA: 0x000A1B00 File Offset: 0x000A0B00
		public string GetStatsForDBBrowse()
		{
			return this.scriptZone;
		}

		// Token: 0x060017F9 RID: 6137 RVA: 0x000A1B08 File Offset: 0x000A0B08
		public string GetSpecialStatsForDBBrowse()
		{
			return string.Empty;
		}

		// Token: 0x04000F86 RID: 3974
		private static readonly Color interfaceColor = Color.FromArgb(MapObjectCreationInfo.DefaultTransparentColorAlpha, Color.Orange);

		// Token: 0x04000F87 RID: 3975
		private static readonly string interfaceSingleObjectTypeName = Strings.SINGLE_SCRIPT_AREA_TYPE_NAME;

		// Token: 0x04000F88 RID: 3976
		private static readonly string interfaceSeveralObjectsTypeName = Strings.SEVERAL_SCRIPT_AREAS_TYPE_NAME;

		// Token: 0x04000F89 RID: 3977
		private static readonly string defaultVisObject = "Editor/Map/SpecialObjects/ScriptArea/ScriptArea.(StaticObject).xdb";

		// Token: 0x04000F8A RID: 3978
		private static readonly string scriptZoneDBType = "gameMechanics.map.scriptZone.ScriptZone";

		// Token: 0x04000F8B RID: 3979
		private string scriptZone = string.Empty;

		// Token: 0x04000F8C RID: 3980
		private string scriptID = string.Empty;

		// Token: 0x04000F8D RID: 3981
		private double scanRadius;

		// Token: 0x04000F8E RID: 3982
		private ScriptAreaData scriptAreaData;

		// Token: 0x020001D5 RID: 469
		// (Invoke) Token: 0x060017FC RID: 6140
		public delegate void ScriptAreaFieldChangedEvent<T>(ScriptArea scriptArea, ref T oldValue, ref T newValue);
	}
}
