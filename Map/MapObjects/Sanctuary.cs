using System;
using System.ComponentModel;
using System.Drawing;
using MapEditor.Resources.Strings;
using Tools.MapObjects;

namespace MapEditor.Map.MapObjects
{
	// Token: 0x020001F8 RID: 504
	public class Sanctuary : Respawn, IMapObjectInterfaceExtention
	{
		// Token: 0x170005FF RID: 1535
		// (get) Token: 0x06001901 RID: 6401 RVA: 0x000A5C86 File Offset: 0x000A4C86
		public static Color InterfaceColor
		{
			get
			{
				return Sanctuary.interfaceColor;
			}
		}

		// Token: 0x17000600 RID: 1536
		// (get) Token: 0x06001902 RID: 6402 RVA: 0x000A5C8D File Offset: 0x000A4C8D
		public static string InterfaceSingleObjectTypeName
		{
			get
			{
				return Sanctuary.interfaceSingleObjectTypeName;
			}
		}

		// Token: 0x17000601 RID: 1537
		// (get) Token: 0x06001903 RID: 6403 RVA: 0x000A5C94 File Offset: 0x000A4C94
		public static string InterfaceSeveralObjectsTypeName
		{
			get
			{
				return Sanctuary.interfaceSeveralObjectsTypeName;
			}
		}

		// Token: 0x17000602 RID: 1538
		// (get) Token: 0x06001904 RID: 6404 RVA: 0x000A5C9B File Offset: 0x000A4C9B
		public static string CommonVisObject
		{
			get
			{
				return Sanctuary.commonVisObject;
			}
		}

		// Token: 0x17000603 RID: 1539
		// (get) Token: 0x06001905 RID: 6405 RVA: 0x000A5CA2 File Offset: 0x000A4CA2
		public static string SectorVisObject
		{
			get
			{
				return Sanctuary.sectorVisObject;
			}
		}

		// Token: 0x06001906 RID: 6406 RVA: 0x000A5CA9 File Offset: 0x000A4CA9
		public Sanctuary(int _id, MapObjectType _type, ICollisionMap _collisionMap, RespawnType _respawnType) : base(_id, _type, _collisionMap, _respawnType)
		{
		}

		// Token: 0x17000604 RID: 1540
		// (get) Token: 0x06001907 RID: 6407 RVA: 0x000A5CB6 File Offset: 0x000A4CB6
		[Browsable(false)]
		public override string SceneName
		{
			get
			{
				if (base.RespawnType == RespawnType.Common)
				{
					return Sanctuary.commonVisObject;
				}
				if (base.RespawnType == RespawnType.Sector)
				{
					return Sanctuary.sectorVisObject;
				}
				return string.Empty;
			}
		}

		// Token: 0x06001908 RID: 6408 RVA: 0x000A5CDC File Offset: 0x000A4CDC
		public override IMapObject Clone(int newID, bool newTemporary, bool newActive)
		{
			Sanctuary sanctuary = new Sanctuary(newID, new MapObjectType(base.Type.Type, base.MapZone), base.CollisionMap, base.RespawnType);
			base.CopyToRespawn(sanctuary);
			base.CopyTo(sanctuary, newTemporary, newActive);
			return sanctuary;
		}

		// Token: 0x06001909 RID: 6409 RVA: 0x000A5D28 File Offset: 0x000A4D28
		public override IMapObjectPack Pack()
		{
			SanctuaryPack sanctuaryPack = new SanctuaryPack();
			sanctuaryPack.Pack(this);
			return sanctuaryPack;
		}

		// Token: 0x0600190A RID: 6410 RVA: 0x000A5D43 File Offset: 0x000A4D43
		public Color GetInterfaceColor()
		{
			return Sanctuary.interfaceColor;
		}

		// Token: 0x0600190B RID: 6411 RVA: 0x000A5D4A File Offset: 0x000A4D4A
		public string GetInterfaceSingleObjectTypeName()
		{
			return Sanctuary.interfaceSingleObjectTypeName;
		}

		// Token: 0x0600190C RID: 6412 RVA: 0x000A5D51 File Offset: 0x000A4D51
		public string GetInterfaceSeveralObjectsTypeName()
		{
			return Sanctuary.interfaceSeveralObjectsTypeName;
		}

		// Token: 0x0600190D RID: 6413 RVA: 0x000A5D58 File Offset: 0x000A4D58
		public bool ContainsText(string text, bool ignoreCase)
		{
			if (string.IsNullOrEmpty(text))
			{
				return true;
			}
			if (string.IsNullOrEmpty(base.MapZone))
			{
				return false;
			}
			if (ignoreCase)
			{
				return base.MapZone.ToLower().Contains(text.ToLower());
			}
			return base.MapZone.Contains(text);
		}

		// Token: 0x0600190E RID: 6414 RVA: 0x000A5DA4 File Offset: 0x000A4DA4
		public string GetStatsForDBBrowse()
		{
			return string.Empty;
		}

		// Token: 0x0600190F RID: 6415 RVA: 0x000A5DAB File Offset: 0x000A4DAB
		public string GetSpecialStatsForDBBrowse()
		{
			return string.Empty;
		}

		// Token: 0x04001023 RID: 4131
		private static readonly Color interfaceColor = Color.FromArgb(MapObjectCreationInfo.DefaultTransparentColorAlpha, Color.Red);

		// Token: 0x04001024 RID: 4132
		private static readonly string interfaceSingleObjectTypeName = Strings.SINGLE_SANCTUARY_TYPE_NAME;

		// Token: 0x04001025 RID: 4133
		private static readonly string interfaceSeveralObjectsTypeName = Strings.SEVERAL_SANCTUARYS_TYPE_NAME;

		// Token: 0x04001026 RID: 4134
		private static readonly string commonVisObject = "Editor/Map/SpecialObjects/CommonSanctuary/CommonSanctuary.(StaticObject).xdb";

		// Token: 0x04001027 RID: 4135
		private static readonly string sectorVisObject = "Editor/Map/SpecialObjects/Sanctuary/Sanctuary.(StaticObject).xdb";
	}
}
