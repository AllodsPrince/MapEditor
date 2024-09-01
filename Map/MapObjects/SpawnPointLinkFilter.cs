using System;
using Tools.MapObjects;

namespace MapEditor.Map.MapObjects
{
	// Token: 0x0200012A RID: 298
	public class SpawnPointLinkFilter : IMapObjectFilter
	{
		// Token: 0x06000EC6 RID: 3782 RVA: 0x00077940 File Offset: 0x00076940
		public static bool CanLink(IMapObject mapObject)
		{
			if (mapObject != null)
			{
				if (mapObject.Type.Type == MapObjectFactory.Type.SpawnPoint)
				{
					SpawnPoint _mapObject = mapObject as SpawnPoint;
					if (_mapObject != null && _mapObject.SpawnPointType == SpawnPointType.Patrol)
					{
						return true;
					}
				}
				if (mapObject.Type.Type == MapObjectFactory.Type.PatrolNode)
				{
					PatrolNode _mapObject2 = mapObject as PatrolNode;
					if (_mapObject2 != null)
					{
						return true;
					}
				}
				if (mapObject.Type.Type == MapObjectFactory.Type.RoutePoint)
				{
					RoutePoint _mapObject3 = mapObject as RoutePoint;
					if (_mapObject3 != null && _mapObject3.RoutePointType == RoutePointType.PatrolNode)
					{
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x06000EC7 RID: 3783 RVA: 0x000779C8 File Offset: 0x000769C8
		public bool FilterObject(IMapObject mapObject)
		{
			return SpawnPointLinkFilter.CanLink(mapObject);
		}
	}
}
