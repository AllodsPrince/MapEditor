using System;
using System.ComponentModel;
using System.Drawing;
using Db;
using MapEditor.Resources.Strings;
using Tools.MapObjects;
using Tools.SafeObjMan;

namespace MapEditor.Map.MapObjects
{
	// Token: 0x020001A4 RID: 420
	public class ZoneLocator : MapObject, IMapObjectInterfaceExtention
	{
		// Token: 0x06001456 RID: 5206 RVA: 0x00093AD0 File Offset: 0x00092AD0
		private void UpdateMapZoneColor()
		{
			if (!string.IsNullOrEmpty(this.mapZone))
			{
				IDatabase mainDb = IDatabase.GetMainDatabase();
				if (mainDb != null)
				{
					DBID dbid = IDatabase.CreateDBIDByName(this.mapZone);
					IObjMan man = mainDb.GetManipulator(dbid);
					if (man != null)
					{
						this.MapZoneColor = Color.FromArgb(MapObjectCreationInfo.DefaultTransparentColorAlpha, Color.FromArgb(SafeObjMan.GetInt(man, "color")));
						base.Highlight(this.MapZoneColor);
						return;
					}
				}
			}
			else
			{
				base.Highlight(MapObjectCreationInfo.DefaultHighlightColor);
			}
		}

		// Token: 0x1400008F RID: 143
		// (add) Token: 0x06001457 RID: 5207 RVA: 0x00093B42 File Offset: 0x00092B42
		// (remove) Token: 0x06001458 RID: 5208 RVA: 0x00093B59 File Offset: 0x00092B59
		public static event ZoneLocator.ZoneLocatorFieldChangedEvent<string> MapZoneChanged;

		// Token: 0x14000090 RID: 144
		// (add) Token: 0x06001459 RID: 5209 RVA: 0x00093B70 File Offset: 0x00092B70
		// (remove) Token: 0x0600145A RID: 5210 RVA: 0x00093B87 File Offset: 0x00092B87
		public static event ZoneLocator.ZoneLocatorFieldChangedEvent<Color> MapZoneColorChanged;

		// Token: 0x170003FD RID: 1021
		// (get) Token: 0x0600145B RID: 5211 RVA: 0x00093B9E File Offset: 0x00092B9E
		public static Color InterfaceColor
		{
			get
			{
				return ZoneLocator.interfaceColor;
			}
		}

		// Token: 0x170003FE RID: 1022
		// (get) Token: 0x0600145C RID: 5212 RVA: 0x00093BA5 File Offset: 0x00092BA5
		public static string InterfaceSingleObjectTypeName
		{
			get
			{
				return ZoneLocator.interfaceSingleObjectTypeName;
			}
		}

		// Token: 0x170003FF RID: 1023
		// (get) Token: 0x0600145D RID: 5213 RVA: 0x00093BAC File Offset: 0x00092BAC
		public static string InterfaceSeveralObjectsTypeName
		{
			get
			{
				return ZoneLocator.interfaceSeveralObjectsTypeName;
			}
		}

		// Token: 0x17000400 RID: 1024
		// (get) Token: 0x0600145E RID: 5214 RVA: 0x00093BB3 File Offset: 0x00092BB3
		public static string DefaultVisObject
		{
			get
			{
				return ZoneLocator.defaultVisObject;
			}
		}

		// Token: 0x17000401 RID: 1025
		// (get) Token: 0x0600145F RID: 5215 RVA: 0x00093BBA File Offset: 0x00092BBA
		public static string ZoneFolder
		{
			get
			{
				return ZoneLocator.zoneFolder;
			}
		}

		// Token: 0x17000402 RID: 1026
		// (get) Token: 0x06001460 RID: 5216 RVA: 0x00093BC1 File Offset: 0x00092BC1
		public static string ZoneDBType
		{
			get
			{
				return ZoneLocator.zoneDBType;
			}
		}

		// Token: 0x06001461 RID: 5217 RVA: 0x00093BC8 File Offset: 0x00092BC8
		public ZoneLocator(int _id, MapObjectType _type, ICollisionMap _collisionMap) : base(_id, _type, _collisionMap)
		{
			this.mapZone = _type.Stats;
			this.UpdateMapZoneColor();
		}

		// Token: 0x17000403 RID: 1027
		// (get) Token: 0x06001462 RID: 5218 RVA: 0x00093BFC File Offset: 0x00092BFC
		// (set) Token: 0x06001463 RID: 5219 RVA: 0x00093C04 File Offset: 0x00092C04
		[Category("ZoneLocator")]
		[Browsable(true)]
		public string MapZone
		{
			get
			{
				return this.mapZone;
			}
			set
			{
				if (this.mapZone != value && base.InvokeChanging(null))
				{
					string oldMapZone = this.mapZone;
					this.mapZone = value;
					base.SetNewStats(this.mapZone);
					this.UpdateMapZoneColor();
					base.InvokeChanged();
					if (base.Active && ZoneLocator.MapZoneChanged != null)
					{
						ZoneLocator.MapZoneChanged(this, ref oldMapZone, ref this.mapZone);
					}
				}
			}
		}

		// Token: 0x17000404 RID: 1028
		// (get) Token: 0x06001464 RID: 5220 RVA: 0x00093C70 File Offset: 0x00092C70
		// (set) Token: 0x06001465 RID: 5221 RVA: 0x00093C78 File Offset: 0x00092C78
		[Browsable(true)]
		[Category("ZoneLocator")]
		public Color MapZoneColor
		{
			get
			{
				return this.mapZoneColor;
			}
			set
			{
				if (this.mapZoneColor != value && base.InvokeChanging(null))
				{
					Color oldMapZoneColor = this.mapZoneColor;
					this.mapZoneColor = value;
					base.InvokeChanged();
					if (base.Active && ZoneLocator.MapZoneColorChanged != null)
					{
						ZoneLocator.MapZoneColorChanged(this, ref oldMapZoneColor, ref this.mapZoneColor);
					}
				}
			}
		}

		// Token: 0x17000405 RID: 1029
		// (get) Token: 0x06001466 RID: 5222 RVA: 0x00093CD2 File Offset: 0x00092CD2
		[Browsable(false)]
		public override string SceneName
		{
			get
			{
				return ZoneLocator.defaultVisObject;
			}
		}

		// Token: 0x06001467 RID: 5223 RVA: 0x00093CDC File Offset: 0x00092CDC
		public override IMapObject Clone(int newID, bool newTemporary, bool newActive)
		{
			ZoneLocator zoneLocator = new ZoneLocator(newID, new MapObjectType(base.Type.Type, this.mapZone), base.CollisionMap);
			base.CopyTo(zoneLocator, newTemporary, newActive);
			return zoneLocator;
		}

		// Token: 0x06001468 RID: 5224 RVA: 0x00093D1C File Offset: 0x00092D1C
		public override IMapObjectPack Pack()
		{
			ZoneLocatorPack soneLocatorPack = new ZoneLocatorPack();
			soneLocatorPack.Pack(this);
			return soneLocatorPack;
		}

		// Token: 0x06001469 RID: 5225 RVA: 0x00093D37 File Offset: 0x00092D37
		public Color GetInterfaceColor()
		{
			return ZoneLocator.interfaceColor;
		}

		// Token: 0x0600146A RID: 5226 RVA: 0x00093D3E File Offset: 0x00092D3E
		public string GetInterfaceSingleObjectTypeName()
		{
			return ZoneLocator.interfaceSingleObjectTypeName;
		}

		// Token: 0x0600146B RID: 5227 RVA: 0x00093D45 File Offset: 0x00092D45
		public string GetInterfaceSeveralObjectsTypeName()
		{
			return ZoneLocator.interfaceSeveralObjectsTypeName;
		}

		// Token: 0x0600146C RID: 5228 RVA: 0x00093D4C File Offset: 0x00092D4C
		public bool ContainsText(string text, bool ignoreCase)
		{
			if (string.IsNullOrEmpty(text))
			{
				return true;
			}
			if (string.IsNullOrEmpty(this.mapZone))
			{
				return false;
			}
			if (ignoreCase)
			{
				return this.mapZone.ToLower().Contains(text.ToLower());
			}
			return this.mapZone.Contains(text);
		}

		// Token: 0x0600146D RID: 5229 RVA: 0x00093D98 File Offset: 0x00092D98
		public string GetStatsForDBBrowse()
		{
			return this.mapZone;
		}

		// Token: 0x0600146E RID: 5230 RVA: 0x00093DA0 File Offset: 0x00092DA0
		public string GetSpecialStatsForDBBrowse()
		{
			return string.Empty;
		}

		// Token: 0x04000E55 RID: 3669
		private static readonly Color interfaceColor = Color.FromArgb(MapObjectCreationInfo.DefaultTransparentColorAlpha, Color.GreenYellow);

		// Token: 0x04000E56 RID: 3670
		private static readonly string interfaceSingleObjectTypeName = Strings.SINGLE_ZONE_LOCATOR_TYPE_NAME;

		// Token: 0x04000E57 RID: 3671
		private static readonly string interfaceSeveralObjectsTypeName = Strings.SEVERAL_ZONE_LOCATORS_TYPE_NAME;

		// Token: 0x04000E58 RID: 3672
		private static readonly string defaultVisObject = "Editor/Map/SpecialObjects/ZoneLocator/ZoneLocator.(StaticObject).xdb";

		// Token: 0x04000E59 RID: 3673
		private static readonly string zoneFolder = "Zones/";

		// Token: 0x04000E5A RID: 3674
		private static readonly string zoneDBType = "gameMechanics.map.zone.ZoneResource";

		// Token: 0x04000E5B RID: 3675
		private string mapZone = string.Empty;

		// Token: 0x04000E5C RID: 3676
		private Color mapZoneColor = MapObjectCreationInfo.DefaultHighlightColor;

		// Token: 0x020001A5 RID: 421
		// (Invoke) Token: 0x06001471 RID: 5233
		public delegate void ZoneLocatorFieldChangedEvent<T>(ZoneLocator zoneLocator, ref T oldValue, ref T newValue);
	}
}
