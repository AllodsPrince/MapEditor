using System;
using System.ComponentModel;
using System.Drawing;
using MapEditor.Resources.Strings;
using Tools.MapObjects;

namespace MapEditor.Map.MapObjects
{
	// Token: 0x02000066 RID: 102
	public class Graveyard : Respawn, IMapObjectInterfaceExtention
	{
		// Token: 0x170000BF RID: 191
		// (get) Token: 0x0600050B RID: 1291 RVA: 0x00028570 File Offset: 0x00027570
		public static Color InterfaceColor
		{
			get
			{
				return Graveyard.interfaceColor;
			}
		}

		// Token: 0x170000C0 RID: 192
		// (get) Token: 0x0600050C RID: 1292 RVA: 0x00028577 File Offset: 0x00027577
		public static string InterfaceSingleObjectTypeName
		{
			get
			{
				return Graveyard.interfaceSingleObjectTypeName;
			}
		}

		// Token: 0x170000C1 RID: 193
		// (get) Token: 0x0600050D RID: 1293 RVA: 0x0002857E File Offset: 0x0002757E
		public static string InterfaceSeveralObjectsTypeName
		{
			get
			{
				return Graveyard.interfaceSeveralObjectsTypeName;
			}
		}

		// Token: 0x170000C2 RID: 194
		// (get) Token: 0x0600050E RID: 1294 RVA: 0x00028585 File Offset: 0x00027585
		public static string CommonVisObject
		{
			get
			{
				return Graveyard.commonVisObject;
			}
		}

		// Token: 0x170000C3 RID: 195
		// (get) Token: 0x0600050F RID: 1295 RVA: 0x0002858C File Offset: 0x0002758C
		public static string SectorVisObject
		{
			get
			{
				return Graveyard.sectorVisObject;
			}
		}

		// Token: 0x06000510 RID: 1296 RVA: 0x00028593 File Offset: 0x00027593
		public Graveyard(int _id, MapObjectType _type, ICollisionMap _collisionMap, RespawnType _respawnType) : base(_id, _type, _collisionMap, _respawnType)
		{
		}

		// Token: 0x170000C4 RID: 196
		// (get) Token: 0x06000511 RID: 1297 RVA: 0x000285A0 File Offset: 0x000275A0
		[Browsable(false)]
		public override string SceneName
		{
			get
			{
				if (base.RespawnType == RespawnType.Common)
				{
					return Graveyard.commonVisObject;
				}
				if (base.RespawnType == RespawnType.Sector)
				{
					return Graveyard.sectorVisObject;
				}
				return string.Empty;
			}
		}

		// Token: 0x06000512 RID: 1298 RVA: 0x000285C8 File Offset: 0x000275C8
		public override IMapObject Clone(int newID, bool newTemporary, bool newActive)
		{
			Graveyard graveyard = new Graveyard(newID, new MapObjectType(base.Type.Type, base.MapZone), base.CollisionMap, base.RespawnType);
			base.CopyToRespawn(graveyard);
			base.CopyTo(graveyard, newTemporary, newActive);
			return graveyard;
		}

		// Token: 0x06000513 RID: 1299 RVA: 0x00028614 File Offset: 0x00027614
		public override IMapObjectPack Pack()
		{
			GraveyardPack graveyardPack = new GraveyardPack();
			graveyardPack.Pack(this);
			return graveyardPack;
		}

		// Token: 0x06000514 RID: 1300 RVA: 0x0002862F File Offset: 0x0002762F
		public Color GetInterfaceColor()
		{
			return Graveyard.interfaceColor;
		}

		// Token: 0x06000515 RID: 1301 RVA: 0x00028636 File Offset: 0x00027636
		public string GetInterfaceSingleObjectTypeName()
		{
			return Graveyard.interfaceSingleObjectTypeName;
		}

		// Token: 0x06000516 RID: 1302 RVA: 0x0002863D File Offset: 0x0002763D
		public string GetInterfaceSeveralObjectsTypeName()
		{
			return Graveyard.interfaceSeveralObjectsTypeName;
		}

		// Token: 0x06000517 RID: 1303 RVA: 0x00028644 File Offset: 0x00027644
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

		// Token: 0x06000518 RID: 1304 RVA: 0x00028690 File Offset: 0x00027690
		public string GetStatsForDBBrowse()
		{
			return string.Empty;
		}

		// Token: 0x06000519 RID: 1305 RVA: 0x00028697 File Offset: 0x00027697
		public string GetSpecialStatsForDBBrowse()
		{
			return string.Empty;
		}

		// Token: 0x040003A8 RID: 936
		private static readonly Color interfaceColor = Color.FromArgb(MapObjectCreationInfo.DefaultTransparentColorAlpha, Color.Red);

		// Token: 0x040003A9 RID: 937
		private static readonly string interfaceSingleObjectTypeName = Strings.SINGLE_GRAVEYARD_TYPE_NAME;

		// Token: 0x040003AA RID: 938
		private static readonly string interfaceSeveralObjectsTypeName = Strings.SEVERAL_GRAVEYARDS_TYPE_NAME;

		// Token: 0x040003AB RID: 939
		private static readonly string commonVisObject = "Editor/Map/SpecialObjects/CommonGraveyard/CommonGraveyard.(StaticObject).xdb";

		// Token: 0x040003AC RID: 940
		private static readonly string sectorVisObject = "Editor/Map/SpecialObjects/Graveyard/Graveyard.(StaticObject).xdb";
	}
}
