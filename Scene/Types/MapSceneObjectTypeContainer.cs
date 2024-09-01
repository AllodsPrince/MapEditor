using System;
using System.Collections.Generic;
using MapEditor.Map.MapObjects;
using Tools.MapObjects;

namespace MapEditor.Scene.Types
{
	// Token: 0x02000072 RID: 114
	public class MapSceneObjectTypeContainer : IMapSceneObjectTypeContainer
	{
		// Token: 0x060005A0 RID: 1440 RVA: 0x0002E6FE File Offset: 0x0002D6FE
		static MapSceneObjectTypeContainer()
		{
			MapSceneObjectTypeContainer.types.Add(MapSceneObjectTypeContainer.generalTypeKey, 0);
			MapSceneObjectTypeContainer.types.Add(MapObjectFactory.Type.StaticObject, 1);
		}

		// Token: 0x170000CA RID: 202
		// (get) Token: 0x060005A1 RID: 1441 RVA: 0x0002E730 File Offset: 0x0002D730
		public static IEnumerable<int> Types
		{
			get
			{
				return MapSceneObjectTypeContainer.types.Values;
			}
		}

		// Token: 0x060005A2 RID: 1442 RVA: 0x0002E73C File Offset: 0x0002D73C
		public static int GetType(int typeKey)
		{
			if (MapSceneObjectTypeContainer.types.ContainsKey(typeKey))
			{
				return MapSceneObjectTypeContainer.types[typeKey];
			}
			return MapSceneObjectTypeContainer.types[MapSceneObjectTypeContainer.generalTypeKey];
		}

		// Token: 0x060005A3 RID: 1443 RVA: 0x0002E768 File Offset: 0x0002D768
		public int GetMapSceneObjectType(IMapObject mapObject)
		{
			int type;
			if (!MapSceneObjectTypeContainer.types.TryGetValue(mapObject.Type.Type, out type))
			{
				type = MapSceneObjectTypeContainer.types[MapSceneObjectTypeContainer.generalTypeKey];
			}
			return type;
		}

		// Token: 0x0400043C RID: 1084
		public static readonly int generalTypeKey = -1;

		// Token: 0x0400043D RID: 1085
		private static readonly Dictionary<int, int> types = new Dictionary<int, int>();
	}
}
