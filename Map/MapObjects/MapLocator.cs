using System;
using System.ComponentModel;
using System.Drawing;
using MapEditor.Resources.Strings;
using Tools.MapObjects;

namespace MapEditor.Map.MapObjects
{
	// Token: 0x0200005E RID: 94
	public class MapLocator : MapObject, IMapObjectInterfaceExtention
	{
		// Token: 0x14000014 RID: 20
		// (add) Token: 0x060004D0 RID: 1232 RVA: 0x00027F81 File Offset: 0x00026F81
		// (remove) Token: 0x060004D1 RID: 1233 RVA: 0x00027F98 File Offset: 0x00026F98
		public static event MapLocator.MapLocatorFieldChangedEvent<string> ScriptIDChanged;

		// Token: 0x14000015 RID: 21
		// (add) Token: 0x060004D2 RID: 1234 RVA: 0x00027FAF File Offset: 0x00026FAF
		// (remove) Token: 0x060004D3 RID: 1235 RVA: 0x00027FC6 File Offset: 0x00026FC6
		public static event MapLocator.MapLocatorFieldChangedEvent<double> ScanRadiusChanged;

		// Token: 0x170000AE RID: 174
		// (get) Token: 0x060004D4 RID: 1236 RVA: 0x00027FDD File Offset: 0x00026FDD
		public static Color InterfaceColor
		{
			get
			{
				return MapLocator.interfaceColor;
			}
		}

		// Token: 0x170000AF RID: 175
		// (get) Token: 0x060004D5 RID: 1237 RVA: 0x00027FE4 File Offset: 0x00026FE4
		public static string InterfaceSingleObjectTypeName
		{
			get
			{
				return MapLocator.interfaceSingleObjectTypeName;
			}
		}

		// Token: 0x170000B0 RID: 176
		// (get) Token: 0x060004D6 RID: 1238 RVA: 0x00027FEB File Offset: 0x00026FEB
		public static string InterfaceSeveralObjectsTypeName
		{
			get
			{
				return MapLocator.interfaceSeveralObjectsTypeName;
			}
		}

		// Token: 0x170000B1 RID: 177
		// (get) Token: 0x060004D7 RID: 1239 RVA: 0x00027FF2 File Offset: 0x00026FF2
		public static string DefaultVisObject
		{
			get
			{
				return MapLocator.defaultVisObject;
			}
		}

		// Token: 0x060004D8 RID: 1240 RVA: 0x00027FF9 File Offset: 0x00026FF9
		public MapLocator(int _id, MapObjectType _type, ICollisionMap _collisionMap) : base(_id, _type, _collisionMap)
		{
		}

		// Token: 0x170000B2 RID: 178
		// (get) Token: 0x060004D9 RID: 1241 RVA: 0x0002800F File Offset: 0x0002700F
		// (set) Token: 0x060004DA RID: 1242 RVA: 0x00028018 File Offset: 0x00027018
		[Browsable(true)]
		[Category("MapLocator")]
		[DisplayName("ScriptID")]
		public string ScriptID
		{
			get
			{
				return this.scriptID;
			}
			set
			{
				if (this.scriptID != value)
				{
					value = Str.Trim(value);
					if (base.InvokeChanging(null))
					{
						string oldScriptID = this.scriptID;
						this.scriptID = value;
						base.InvokeChanged();
						if (base.Active && MapLocator.ScriptIDChanged != null)
						{
							MapLocator.ScriptIDChanged(this, ref oldScriptID, ref this.scriptID);
						}
					}
				}
			}
		}

		// Token: 0x170000B3 RID: 179
		// (get) Token: 0x060004DB RID: 1243 RVA: 0x0002807A File Offset: 0x0002707A
		// (set) Token: 0x060004DC RID: 1244 RVA: 0x00028084 File Offset: 0x00027084
		[Category("MapLocator")]
		[Browsable(true)]
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
					if (base.Active && MapLocator.ScanRadiusChanged != null)
					{
						MapLocator.ScanRadiusChanged(this, ref oldScanRadius, ref this.scanRadius);
					}
				}
			}
		}

		// Token: 0x170000B4 RID: 180
		// (get) Token: 0x060004DD RID: 1245 RVA: 0x000280D9 File Offset: 0x000270D9
		[Browsable(false)]
		public override string SceneName
		{
			get
			{
				return MapLocator.defaultVisObject;
			}
		}

		// Token: 0x060004DE RID: 1246 RVA: 0x000280E0 File Offset: 0x000270E0
		public override IMapObject Clone(int newID, bool newTemporary, bool newActive)
		{
			MapLocator mapLocator = new MapLocator(newID, new MapObjectType(base.Type.Type, string.Empty), base.CollisionMap);
			mapLocator.scriptID = this.scriptID;
			mapLocator.scanRadius = this.scanRadius;
			base.CopyTo(mapLocator, newTemporary, newActive);
			return mapLocator;
		}

		// Token: 0x060004DF RID: 1247 RVA: 0x00028134 File Offset: 0x00027134
		public override IMapObjectPack Pack()
		{
			MapLocatorPack mapLocatorPack = new MapLocatorPack();
			mapLocatorPack.Pack(this);
			return mapLocatorPack;
		}

		// Token: 0x060004E0 RID: 1248 RVA: 0x0002814F File Offset: 0x0002714F
		public Color GetInterfaceColor()
		{
			return MapLocator.interfaceColor;
		}

		// Token: 0x060004E1 RID: 1249 RVA: 0x00028156 File Offset: 0x00027156
		public string GetInterfaceSingleObjectTypeName()
		{
			return MapLocator.interfaceSingleObjectTypeName;
		}

		// Token: 0x060004E2 RID: 1250 RVA: 0x0002815D File Offset: 0x0002715D
		public string GetInterfaceSeveralObjectsTypeName()
		{
			return MapLocator.interfaceSeveralObjectsTypeName;
		}

		// Token: 0x060004E3 RID: 1251 RVA: 0x00028164 File Offset: 0x00027164
		public bool ContainsText(string text, bool ignoreCase)
		{
			if (string.IsNullOrEmpty(text))
			{
				return true;
			}
			if (ignoreCase)
			{
				return !string.IsNullOrEmpty(this.scriptID) && this.scriptID.ToLower().Contains(text.ToLower());
			}
			return !string.IsNullOrEmpty(this.scriptID) && this.scriptID.Contains(text);
		}

		// Token: 0x060004E4 RID: 1252 RVA: 0x000281C5 File Offset: 0x000271C5
		public string GetStatsForDBBrowse()
		{
			return string.Empty;
		}

		// Token: 0x060004E5 RID: 1253 RVA: 0x000281CC File Offset: 0x000271CC
		public string GetSpecialStatsForDBBrowse()
		{
			return string.Empty;
		}

		// Token: 0x04000398 RID: 920
		private static readonly Color interfaceColor = Color.FromArgb(MapObjectCreationInfo.DefaultTransparentColorAlpha, Color.Black);

		// Token: 0x04000399 RID: 921
		private static readonly string interfaceSingleObjectTypeName = Strings.SINGLE_MAP_LOCATOR_TYPE_NAME;

		// Token: 0x0400039A RID: 922
		private static readonly string interfaceSeveralObjectsTypeName = Strings.SEVERAL_MAP_LOCATORS_TYPE_NAME;

		// Token: 0x0400039B RID: 923
		private static readonly string defaultVisObject = "Editor/Map/SpecialObjects/MapLocator/MapLocator.(StaticObject).xdb";

		// Token: 0x0400039C RID: 924
		private string scriptID = string.Empty;

		// Token: 0x0400039D RID: 925
		private double scanRadius;

		// Token: 0x0200005F RID: 95
		// (Invoke) Token: 0x060004E8 RID: 1256
		public delegate void MapLocatorFieldChangedEvent<T>(MapLocator mapLocator, ref T oldValue, ref T newValue);
	}
}
