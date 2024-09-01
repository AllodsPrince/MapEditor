using System;
using System.Xml.Serialization;
using Tools.MapObjects;

namespace MapEditor.Map.MapObjects
{
	// Token: 0x0200002E RID: 46
	[XmlInclude(typeof(ProjectilePack))]
	[XmlInclude(typeof(SanctuaryPack))]
	[XmlInclude(typeof(AstralBorderPack))]
	[XmlInclude(typeof(GraveyardPack))]
	[XmlInclude(typeof(ClientPatrolNodePack))]
	[XmlInclude(typeof(ExtendedSoundPack))]
	[XmlInclude(typeof(StaticObjectPack))]
	[XmlInclude(typeof(StartPointPack))]
	[XmlInclude(typeof(PlayerRespawnPlacePack))]
	[XmlInclude(typeof(SpawnPointPack))]
	[XmlInclude(typeof(ScriptAreaPack))]
	[XmlInclude(typeof(ZoneLocatorPack))]
	[XmlInclude(typeof(RoutePointPack))]
	[XmlInclude(typeof(PermanentDevicePack))]
	[XmlInclude(typeof(MapLocatorPack))]
	[XmlInclude(typeof(PatrolNodePack))]
	[XmlInclude(typeof(ClientSpawnPointPack))]
	public class SerializableMapObjectPack : MapObjectPack
	{
	}
}
