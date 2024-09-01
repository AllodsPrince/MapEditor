using System;
using Db;
using MapEditor.Resources.Strings;
using Tools.MapObjects;

namespace MapEditor.Map.MapObjects
{
	// Token: 0x02000037 RID: 55
	public class MapObjectFactory : IMapObjectFactory
	{
		// Token: 0x06000330 RID: 816 RVA: 0x0001E5A4 File Offset: 0x0001D5A4
		private static int GetMapObjectType(string stats, EditorScene editorScene)
		{
			if (stats == StartPoint.DefaultVisObject)
			{
				return MapObjectFactory.Type.StartPoint;
			}
			if (stats == Graveyard.CommonVisObject || stats == Graveyard.SectorVisObject)
			{
				return MapObjectFactory.Type.Graveyard;
			}
			if (stats == SpawnPoint.DefaultVisObject)
			{
				return MapObjectFactory.Type.SpawnPoint;
			}
			if (stats == ScriptArea.DefaultVisObject)
			{
				return MapObjectFactory.Type.ScriptArea;
			}
			if (stats == ZoneLocator.DefaultVisObject)
			{
				return MapObjectFactory.Type.ZoneLocator;
			}
			if (stats == RoutePoint.SimpleVisObject || stats == RoutePoint.ComplexVisObject)
			{
				return MapObjectFactory.Type.RoutePoint;
			}
			if (stats == PermanentDevice.DefaultVisObject)
			{
				return MapObjectFactory.Type.PermanentDevice;
			}
			if (stats == MapLocator.DefaultVisObject)
			{
				return MapObjectFactory.Type.MapLocator;
			}
			if (stats == PatrolNode.DefaultVisObject)
			{
				return MapObjectFactory.Type.PatrolNode;
			}
			if (stats == ClientSpawnPoint.DefaultVisObject)
			{
				return MapObjectFactory.Type.ClientSpawnPoint;
			}
			if (stats == ClientPatrolNode.DefaultVisObject)
			{
				return MapObjectFactory.Type.ClientPatrolNode;
			}
			if (stats == Sanctuary.CommonVisObject || stats == Sanctuary.SectorVisObject)
			{
				return MapObjectFactory.Type.Sanctuary;
			}
			if (stats == AstralBorder.DefaultVisObject)
			{
				return MapObjectFactory.Type.AstralBorder;
			}
			if (stats == Projectile.DefaultStatObject)
			{
				return MapObjectFactory.Type.Projectile;
			}
			if (stats == PlayerRespawnPlace.DefaultVisObject)
			{
				return MapObjectFactory.Type.PlayerRespawnPlace;
			}
			if (stats == ExtendedSound.DefaultVisObject)
			{
				return MapObjectFactory.Type.ExtendedSound;
			}
			IDatabase mainDb = IDatabase.GetMainDatabase();
			if (mainDb != null && editorScene != null)
			{
				DBID statsDBID = mainDb.GetDBIDByName(stats);
				if (mainDb.GetClassTypeName(statsDBID) == StartPoint.CharacterDBType)
				{
					return MapObjectFactory.Type.StartPoint;
				}
			}
			return MapObjectFactory.Type.StaticObject;
		}

		// Token: 0x06000331 RID: 817 RVA: 0x0001E73C File Offset: 0x0001D73C
		private static IMapObject InnerCreateMapObject(int id, MapObjectType mapObjectType, ICollisionMap collisionMap)
		{
			IMapObject mapObject = null;
			if (MapObjectFactory.Type.UnknownType(mapObjectType.Type) || mapObjectType.Type == MapObjectFactory.Type.StaticObject)
			{
				mapObject = new StaticObject(id, mapObjectType, collisionMap);
			}
			else if (mapObjectType.Type == MapObjectFactory.Type.StartPoint)
			{
				mapObject = new StartPoint(id, mapObjectType, collisionMap);
			}
			else if (mapObjectType.Type == MapObjectFactory.Type.Graveyard)
			{
				mapObject = new Graveyard(id, mapObjectType, collisionMap, Respawn.DefaultRespawnType);
			}
			else if (mapObjectType.Type == MapObjectFactory.Type.SpawnPoint)
			{
				mapObject = new SpawnPoint(id, mapObjectType, collisionMap, SpawnPointData.DefaultSpawnPointType, SpawnPoint.DefaultSpawnTableType, SpawnPoint.DefaultSpawnID);
			}
			else if (mapObjectType.Type == MapObjectFactory.Type.ScriptArea)
			{
				mapObject = new ScriptArea(id, mapObjectType, collisionMap, ScriptAreaData.DefaultScriptAreaType);
			}
			else if (mapObjectType.Type == MapObjectFactory.Type.ZoneLocator)
			{
				mapObject = new ZoneLocator(id, mapObjectType, collisionMap);
			}
			else if (mapObjectType.Type == MapObjectFactory.Type.RoutePoint)
			{
				mapObject = new RoutePoint(id, mapObjectType, collisionMap, RoutePoint.DefaultRoutePointType);
			}
			else if (mapObjectType.Type == MapObjectFactory.Type.PermanentDevice)
			{
				mapObject = new PermanentDevice(id, mapObjectType, collisionMap);
			}
			else if (mapObjectType.Type == MapObjectFactory.Type.MapLocator)
			{
				mapObject = new MapLocator(id, mapObjectType, collisionMap);
			}
			else if (mapObjectType.Type == MapObjectFactory.Type.PatrolNode)
			{
				mapObject = new PatrolNode(id, mapObjectType, collisionMap);
			}
			else if (mapObjectType.Type == MapObjectFactory.Type.ClientSpawnPoint)
			{
				mapObject = new ClientSpawnPoint(id, mapObjectType, collisionMap);
			}
			else if (mapObjectType.Type == MapObjectFactory.Type.ClientPatrolNode)
			{
				mapObject = new ClientPatrolNode(id, mapObjectType, collisionMap);
			}
			else if (mapObjectType.Type == MapObjectFactory.Type.Sanctuary)
			{
				mapObject = new Sanctuary(id, mapObjectType, collisionMap, Respawn.DefaultRespawnType);
			}
			else if (mapObjectType.Type == MapObjectFactory.Type.AstralBorder)
			{
				mapObject = new AstralBorder(id, mapObjectType, collisionMap);
			}
			else if (mapObjectType.Type == MapObjectFactory.Type.Projectile)
			{
				mapObject = new Projectile(id, mapObjectType, collisionMap);
			}
			else if (mapObjectType.Type == MapObjectFactory.Type.PlayerRespawnPlace)
			{
				mapObject = new PlayerRespawnPlace(id, mapObjectType, collisionMap);
			}
			else if (mapObjectType.Type == MapObjectFactory.Type.ExtendedSound)
			{
				mapObject = new ExtendedSound(id, mapObjectType, collisionMap);
			}
			else if (mapObjectType.Type == MapObjectFactory.Type.MultiObject)
			{
				mapObject = new MultiMapObject(id, mapObjectType, collisionMap);
			}
			else if (mapObjectType.Type == MapObjectFactory.Type.RouteObject)
			{
				mapObject = new RouteObject(id, mapObjectType, collisionMap);
			}
			return mapObject;
		}

		// Token: 0x06000332 RID: 818 RVA: 0x0001E980 File Offset: 0x0001D980
		public static MapObjectType CreateMapObjectType(string stats, EditorScene editorScene)
		{
			MapObjectType mapObjectType = MapObjectType.Empty;
			mapObjectType.Type = MapObjectFactory.Type.Unknown;
			if (!string.IsNullOrEmpty(stats) && stats.Contains(".xdb"))
			{
				mapObjectType.Type = MapObjectFactory.GetMapObjectType(stats, editorScene);
				if (MapObjectFactory.Type.KnownType(mapObjectType.Type))
				{
					if (mapObjectType.Type == MapObjectFactory.Type.StaticObject)
					{
						mapObjectType.Stats = stats;
					}
					else if (mapObjectType.Type == MapObjectFactory.Type.StartPoint)
					{
						if (stats != StartPoint.DefaultVisObject)
						{
							mapObjectType.Stats = stats;
						}
					}
					else if (mapObjectType.Type == MapObjectFactory.Type.Graveyard)
					{
						if (stats != Graveyard.CommonVisObject && stats != Graveyard.SectorVisObject)
						{
							mapObjectType.Stats = stats;
						}
					}
					else if (mapObjectType.Type == MapObjectFactory.Type.SpawnPoint)
					{
						if (stats != SpawnPoint.DefaultVisObject)
						{
							mapObjectType.Stats = stats;
						}
					}
					else if (mapObjectType.Type == MapObjectFactory.Type.ScriptArea)
					{
						if (stats != ScriptArea.DefaultVisObject)
						{
							mapObjectType.Stats = stats;
						}
					}
					else if (mapObjectType.Type == MapObjectFactory.Type.ZoneLocator)
					{
						if (stats != ZoneLocator.DefaultVisObject)
						{
							mapObjectType.Stats = stats;
						}
					}
					else if (mapObjectType.Type == MapObjectFactory.Type.RoutePoint)
					{
						if (stats != RoutePoint.SimpleVisObject && stats != RoutePoint.ComplexVisObject)
						{
							mapObjectType.Stats = stats;
						}
					}
					else if (mapObjectType.Type == MapObjectFactory.Type.PermanentDevice)
					{
						mapObjectType.Stats = stats;
					}
					else if (mapObjectType.Type == MapObjectFactory.Type.MapLocator)
					{
						if (stats != MapLocator.DefaultVisObject)
						{
							mapObjectType.Stats = stats;
						}
					}
					else if (mapObjectType.Type == MapObjectFactory.Type.PatrolNode)
					{
						if (stats != PatrolNode.DefaultVisObject)
						{
							mapObjectType.Stats = stats;
						}
					}
					else if (mapObjectType.Type == MapObjectFactory.Type.ClientSpawnPoint)
					{
						if (stats != ClientSpawnPoint.DefaultVisObject)
						{
							mapObjectType.Stats = stats;
						}
					}
					else if (mapObjectType.Type == MapObjectFactory.Type.ClientPatrolNode)
					{
						if (stats != ClientPatrolNode.DefaultVisObject)
						{
							mapObjectType.Stats = stats;
						}
					}
					else if (mapObjectType.Type == MapObjectFactory.Type.Sanctuary)
					{
						if (stats != Sanctuary.CommonVisObject && stats != Sanctuary.SectorVisObject)
						{
							mapObjectType.Stats = stats;
						}
					}
					else if (mapObjectType.Type == MapObjectFactory.Type.AstralBorder)
					{
						if (stats != AstralBorder.DefaultVisObject)
						{
							mapObjectType.Stats = stats;
						}
					}
					else if (mapObjectType.Type == MapObjectFactory.Type.Projectile)
					{
						if (stats != Projectile.DefaultStatObject)
						{
							mapObjectType.Stats = stats;
						}
					}
					else if (mapObjectType.Type == MapObjectFactory.Type.PlayerRespawnPlace)
					{
						mapObjectType.Stats = stats;
					}
					else if (mapObjectType.Type == MapObjectFactory.Type.ExtendedSound)
					{
						mapObjectType.Stats = stats;
					}
					else
					{
						mapObjectType.Stats = stats;
					}
				}
			}
			return mapObjectType;
		}

		// Token: 0x06000333 RID: 819 RVA: 0x0001EC9C File Offset: 0x0001DC9C
		public static void FillMapObject(IMapObject mapObject, string stats, ContinentType continentType)
		{
			if (mapObject != null && !string.IsNullOrEmpty(stats))
			{
				if (continentType == ContinentType.AstralHub && mapObject.Type.Type == MapObjectFactory.Type.SpawnPoint)
				{
					SpawnPoint spawnPoint = mapObject as SpawnPoint;
					if (spawnPoint != null)
					{
						spawnPoint.SpawnPointType = SpawnPointData.DefaultAstralSpawnPointType;
						spawnPoint.SetSpawnTableType(SpawnPoint.DefaultAstralSpawnTableType);
						return;
					}
				}
				else if (mapObject.Type.Type == MapObjectFactory.Type.Graveyard)
				{
					Graveyard graveyard = mapObject as Graveyard;
					if (graveyard != null)
					{
						if (stats == Graveyard.CommonVisObject)
						{
							graveyard.RespawnType = RespawnType.Common;
							return;
						}
						if (stats == Graveyard.SectorVisObject)
						{
							graveyard.RespawnType = RespawnType.Sector;
							return;
						}
					}
				}
				else if (mapObject.Type.Type == MapObjectFactory.Type.RoutePoint)
				{
					RoutePoint routePoint = mapObject as RoutePoint;
					if (routePoint != null)
					{
						if (stats == RoutePoint.SimpleVisObject)
						{
							routePoint.RoutePointType = RoutePointType.Simple;
							return;
						}
						if (stats == RoutePoint.ComplexVisObject)
						{
							routePoint.RoutePointType = RoutePointType.PatrolNode;
							return;
						}
					}
				}
				else if (mapObject.Type.Type == MapObjectFactory.Type.Sanctuary)
				{
					Sanctuary sanctuary = mapObject as Sanctuary;
					if (sanctuary != null)
					{
						if (stats == Sanctuary.CommonVisObject)
						{
							sanctuary.RespawnType = RespawnType.Common;
							return;
						}
						if (stats == Sanctuary.SectorVisObject)
						{
							sanctuary.RespawnType = RespawnType.Sector;
						}
					}
				}
			}
		}

		// Token: 0x06000334 RID: 820 RVA: 0x0001EDDC File Offset: 0x0001DDDC
		public IMapObject CreateMapObject(int id, MapObjectType mapObjectType, ICollisionMap collisionMap, bool temporary, bool active)
		{
			IMapObject mapObject = MapObjectFactory.InnerCreateMapObject(id, mapObjectType, collisionMap);
			if (mapObject != null)
			{
				mapObject.Temporary = temporary;
				mapObject.Active = active;
			}
			return mapObject;
		}

		// Token: 0x06000335 RID: 821 RVA: 0x0001EE08 File Offset: 0x0001DE08
		public IMapObject CreateMapObject(int id, MapObjectType mapObjectType, ICollisionMap collisionMap, bool temporary, bool active, Position position)
		{
			IMapObject mapObject = MapObjectFactory.InnerCreateMapObject(id, mapObjectType, collisionMap);
			if (mapObject != null)
			{
				TerrainSurface surfaceBackup = mapObject.Surface;
				mapObject.Surface = TerrainSurface.None;
				mapObject.Position = position;
				mapObject.Surface = surfaceBackup;
				mapObject.Temporary = temporary;
				mapObject.Active = active;
			}
			return mapObject;
		}

		// Token: 0x06000336 RID: 822 RVA: 0x0001EE50 File Offset: 0x0001DE50
		public IMapObject CreateMapObject(int id, MapObjectType mapObjectType, ICollisionMap collisionMap, bool temporary, bool active, Position position, Rotation rotation)
		{
			IMapObject mapObject = MapObjectFactory.InnerCreateMapObject(id, mapObjectType, collisionMap);
			if (mapObject != null)
			{
				TerrainSurface surfaceBackup = mapObject.Surface;
				mapObject.Surface = TerrainSurface.None;
				mapObject.Position = position;
				mapObject.Rotation = rotation;
				mapObject.Surface = surfaceBackup;
				mapObject.Temporary = temporary;
				mapObject.Active = active;
			}
			return mapObject;
		}

		// Token: 0x06000337 RID: 823 RVA: 0x0001EEA0 File Offset: 0x0001DEA0
		public IMapObject CreateMapObject(int id, MapObjectType mapObjectType, ICollisionMap collisionMap, bool temporary, bool active, Position position, Scale scale)
		{
			IMapObject mapObject = MapObjectFactory.InnerCreateMapObject(id, mapObjectType, collisionMap);
			if (mapObject != null)
			{
				TerrainSurface surfaceBackup = mapObject.Surface;
				mapObject.Surface = TerrainSurface.None;
				mapObject.Position = position;
				mapObject.Scale = scale;
				mapObject.Surface = surfaceBackup;
				mapObject.Temporary = temporary;
				mapObject.Active = active;
			}
			return mapObject;
		}

		// Token: 0x06000338 RID: 824 RVA: 0x0001EEF0 File Offset: 0x0001DEF0
		public IMapObject CreateMapObject(int id, MapObjectType mapObjectType, ICollisionMap collisionMap, bool temporary, bool active, Position position, Rotation rotation, Scale scale)
		{
			IMapObject mapObject = MapObjectFactory.InnerCreateMapObject(id, mapObjectType, collisionMap);
			if (mapObject != null)
			{
				TerrainSurface surfaceBackup = mapObject.Surface;
				mapObject.Surface = TerrainSurface.None;
				mapObject.Position = position;
				mapObject.Rotation = rotation;
				mapObject.Scale = scale;
				mapObject.Surface = surfaceBackup;
				mapObject.Temporary = temporary;
				mapObject.Active = active;
			}
			return mapObject;
		}

		// Token: 0x06000339 RID: 825 RVA: 0x0001EF48 File Offset: 0x0001DF48
		public IMapObject CreateMapObject(int id, MapObjectType mapObjectType, ICollisionMap collisionMap, bool temporary, bool active, Position position, Rotation rotation, Scale scale, bool useManualColor, EditorSceneDLLImport.COLOR_INFO info)
		{
			IMapObject mapObject = MapObjectFactory.InnerCreateMapObject(id, mapObjectType, collisionMap);
			if (mapObject != null)
			{
				TerrainSurface surfaceBackup = mapObject.Surface;
				mapObject.Surface = TerrainSurface.None;
				mapObject.Position = position;
				mapObject.Rotation = rotation;
				mapObject.Scale = scale;
				mapObject.UseManualColor = useManualColor;
				mapObject.Info = info;
				mapObject.Surface = surfaceBackup;
				mapObject.Temporary = temporary;
				mapObject.Active = active;
			}
			return mapObject;
		}

		// Token: 0x0600033A RID: 826 RVA: 0x0001EFB0 File Offset: 0x0001DFB0
		public IMapObject CreateMapObject(int id, MapObjectType mapObjectType, ICollisionMap collisionMap, bool temporary, bool active, Position position, Rotation rotation, Scale scale, string groupName)
		{
			IMapObject mapObject = MapObjectFactory.InnerCreateMapObject(id, mapObjectType, collisionMap);
			if (mapObject != null)
			{
				TerrainSurface surfaceBackup = mapObject.Surface;
				mapObject.Surface = TerrainSurface.None;
				mapObject.Position = position;
				mapObject.Rotation = rotation;
				mapObject.Scale = scale;
				mapObject.GroupName = groupName;
				mapObject.Surface = surfaceBackup;
				mapObject.Temporary = temporary;
				mapObject.Active = active;
			}
			return mapObject;
		}

		// Token: 0x0600033B RID: 827 RVA: 0x0001F010 File Offset: 0x0001E010
		public IMapObject CreateMapObject(int id, MapObjectType mapObjectType, ICollisionMap collisionMap, bool temporary, bool active, MapObjectCreationInfo objectCreationInfo)
		{
			IMapObject mapObject = MapObjectFactory.InnerCreateMapObject(id, mapObjectType, collisionMap);
			if (mapObject != null)
			{
				TerrainSurface surfaceBackup = mapObject.Surface;
				mapObject.Surface = TerrainSurface.None;
				mapObject.Position = objectCreationInfo.Position;
				mapObject.Scale = objectCreationInfo.Scale;
				mapObject.Rotation = objectCreationInfo.Rotation;
				mapObject.Volume = objectCreationInfo.Volume;
				mapObject.GroupName = objectCreationInfo.GroupName;
				if (objectCreationInfo.Select != MapObjectCreationInfo.DefaultSelect)
				{
					mapObject.Select = objectCreationInfo.Select;
				}
				if (objectCreationInfo.SelectionColor != MapObjectCreationInfo.DefaultHighlightColor)
				{
					mapObject.Highlight(null, objectCreationInfo.SelectionColor, false);
				}
				mapObject.Surface = surfaceBackup;
				mapObject.Temporary = temporary;
				mapObject.Active = active;
			}
			return mapObject;
		}

		// Token: 0x0600033C RID: 828 RVA: 0x0001F0D0 File Offset: 0x0001E0D0
		public IMapObject CloneMapObject(IMapObject mapObject, int id, bool temporary, bool active)
		{
			if (mapObject != null)
			{
				IMapObject newMapObject = mapObject.Clone(id, temporary, false);
				if (newMapObject != null)
				{
					newMapObject.Active = active;
					return newMapObject;
				}
			}
			return null;
		}

		// Token: 0x0600033D RID: 829 RVA: 0x0001F0F8 File Offset: 0x0001E0F8
		public IMapObject CloneMapObject(IMapObject mapObject, int id, bool temporary, bool active, Position position)
		{
			if (mapObject != null)
			{
				IMapObject newMapObject = mapObject.Clone(id, temporary, false);
				if (newMapObject != null)
				{
					TerrainSurface surfaceBackup = newMapObject.Surface;
					newMapObject.Surface = TerrainSurface.None;
					newMapObject.Position = position;
					newMapObject.Surface = surfaceBackup;
					newMapObject.Active = active;
					return newMapObject;
				}
			}
			return null;
		}

		// Token: 0x0600033E RID: 830 RVA: 0x0001F140 File Offset: 0x0001E140
		public IMapObject CloneMapObject(IMapObject mapObject, int id, bool temporary, bool active, Position position, Rotation rotation)
		{
			if (mapObject != null)
			{
				IMapObject newMapObject = mapObject.Clone(id, temporary, false);
				if (newMapObject != null)
				{
					TerrainSurface surfaceBackup = newMapObject.Surface;
					newMapObject.Surface = TerrainSurface.None;
					newMapObject.Position = position;
					newMapObject.Rotation = rotation;
					newMapObject.Surface = surfaceBackup;
					newMapObject.Active = active;
					return newMapObject;
				}
			}
			return null;
		}

		// Token: 0x0600033F RID: 831 RVA: 0x0001F190 File Offset: 0x0001E190
		public IMapObject CloneMapObject(IMapObject mapObject, int id, bool temporary, bool active, Position position, Scale scale)
		{
			if (mapObject != null)
			{
				IMapObject newMapObject = mapObject.Clone(id, temporary, false);
				if (newMapObject != null)
				{
					TerrainSurface surfaceBackup = newMapObject.Surface;
					newMapObject.Surface = TerrainSurface.None;
					newMapObject.Position = position;
					newMapObject.Scale = scale;
					newMapObject.Surface = surfaceBackup;
					newMapObject.Active = active;
					return newMapObject;
				}
			}
			return null;
		}

		// Token: 0x06000340 RID: 832 RVA: 0x0001F1E0 File Offset: 0x0001E1E0
		public IMapObject CloneMapObject(IMapObject mapObject, int id, bool temporary, bool active, Position position, Rotation rotation, Scale scale)
		{
			if (mapObject != null)
			{
				IMapObject newMapObject = mapObject.Clone(id, temporary, false);
				if (newMapObject != null)
				{
					TerrainSurface surfaceBackup = newMapObject.Surface;
					newMapObject.Surface = TerrainSurface.None;
					newMapObject.Position = position;
					newMapObject.Rotation = rotation;
					newMapObject.Scale = scale;
					newMapObject.Surface = surfaceBackup;
					newMapObject.Active = active;
					return newMapObject;
				}
			}
			return null;
		}

		// Token: 0x06000341 RID: 833 RVA: 0x0001F238 File Offset: 0x0001E238
		public IMapObject CloneMapObject(IMapObject mapObject, int id, bool temporary, bool active, MapObjectCreationInfo objectCreationInfo)
		{
			if (mapObject != null)
			{
				IMapObject newMapObject = mapObject.Clone(id, temporary, false);
				if (newMapObject != null)
				{
					TerrainSurface surfaceBackup = newMapObject.Surface;
					newMapObject.Surface = TerrainSurface.None;
					newMapObject.Position = objectCreationInfo.Position;
					newMapObject.Scale = objectCreationInfo.Scale;
					newMapObject.Rotation = objectCreationInfo.Rotation;
					newMapObject.Volume = objectCreationInfo.Volume;
					if (objectCreationInfo.Select != MapObjectCreationInfo.DefaultSelect)
					{
						newMapObject.Select = objectCreationInfo.Select;
					}
					if (objectCreationInfo.SelectionColor != MapObjectCreationInfo.DefaultHighlightColor)
					{
						newMapObject.Highlight(null, objectCreationInfo.SelectionColor, false);
					}
					newMapObject.Surface = surfaceBackup;
					newMapObject.Active = active;
					return newMapObject;
				}
			}
			return null;
		}

		// Token: 0x02000038 RID: 56
		public class Type
		{
			// Token: 0x17000053 RID: 83
			// (get) Token: 0x06000343 RID: 835 RVA: 0x0001F2F4 File Offset: 0x0001E2F4
			public static int Unknown
			{
				get
				{
					return MapObjectFactory.Type.unknown;
				}
			}

			// Token: 0x17000054 RID: 84
			// (get) Token: 0x06000344 RID: 836 RVA: 0x0001F2FB File Offset: 0x0001E2FB
			public static int FirstKnownType
			{
				get
				{
					return MapObjectFactory.Type.firstKnown;
				}
			}

			// Token: 0x17000055 RID: 85
			// (get) Token: 0x06000345 RID: 837 RVA: 0x0001F302 File Offset: 0x0001E302
			public static int LastKnownType
			{
				get
				{
					return MapObjectFactory.Type.lastKnown;
				}
			}

			// Token: 0x17000056 RID: 86
			// (get) Token: 0x06000346 RID: 838 RVA: 0x0001F309 File Offset: 0x0001E309
			public static int FirstSimpleType
			{
				get
				{
					return MapObjectFactory.Type.firstSimple;
				}
			}

			// Token: 0x17000057 RID: 87
			// (get) Token: 0x06000347 RID: 839 RVA: 0x0001F310 File Offset: 0x0001E310
			public static int LastSimpleType
			{
				get
				{
					return MapObjectFactory.Type.lastSimple;
				}
			}

			// Token: 0x17000058 RID: 88
			// (get) Token: 0x06000348 RID: 840 RVA: 0x0001F317 File Offset: 0x0001E317
			public static int FirstSpecialType
			{
				get
				{
					return MapObjectFactory.Type.firstSpecial;
				}
			}

			// Token: 0x17000059 RID: 89
			// (get) Token: 0x06000349 RID: 841 RVA: 0x0001F31E File Offset: 0x0001E31E
			public static int LastSpecialType
			{
				get
				{
					return MapObjectFactory.Type.lastSpecial;
				}
			}

			// Token: 0x1700005A RID: 90
			// (get) Token: 0x0600034A RID: 842 RVA: 0x0001F325 File Offset: 0x0001E325
			public static int FirstTemporaryType
			{
				get
				{
					return MapObjectFactory.Type.firstTemporary;
				}
			}

			// Token: 0x1700005B RID: 91
			// (get) Token: 0x0600034B RID: 843 RVA: 0x0001F32C File Offset: 0x0001E32C
			public static int LastTemporaryType
			{
				get
				{
					return MapObjectFactory.Type.lastTemporary;
				}
			}

			// Token: 0x1700005C RID: 92
			// (get) Token: 0x0600034C RID: 844 RVA: 0x0001F333 File Offset: 0x0001E333
			public static int StaticObject
			{
				get
				{
					return MapObjectFactory.Type.staticObject;
				}
			}

			// Token: 0x1700005D RID: 93
			// (get) Token: 0x0600034D RID: 845 RVA: 0x0001F33A File Offset: 0x0001E33A
			public static int StartPoint
			{
				get
				{
					return MapObjectFactory.Type.startPoint;
				}
			}

			// Token: 0x1700005E RID: 94
			// (get) Token: 0x0600034E RID: 846 RVA: 0x0001F341 File Offset: 0x0001E341
			public static int Graveyard
			{
				get
				{
					return MapObjectFactory.Type.graveyard;
				}
			}

			// Token: 0x1700005F RID: 95
			// (get) Token: 0x0600034F RID: 847 RVA: 0x0001F348 File Offset: 0x0001E348
			public static int SpawnPoint
			{
				get
				{
					return MapObjectFactory.Type.spawnPoint;
				}
			}

			// Token: 0x17000060 RID: 96
			// (get) Token: 0x06000350 RID: 848 RVA: 0x0001F34F File Offset: 0x0001E34F
			public static int ScriptArea
			{
				get
				{
					return MapObjectFactory.Type.scriptArea;
				}
			}

			// Token: 0x17000061 RID: 97
			// (get) Token: 0x06000351 RID: 849 RVA: 0x0001F356 File Offset: 0x0001E356
			public static int ZoneLocator
			{
				get
				{
					return MapObjectFactory.Type.zoneLocator;
				}
			}

			// Token: 0x17000062 RID: 98
			// (get) Token: 0x06000352 RID: 850 RVA: 0x0001F35D File Offset: 0x0001E35D
			public static int RoutePoint
			{
				get
				{
					return MapObjectFactory.Type.routePoint;
				}
			}

			// Token: 0x17000063 RID: 99
			// (get) Token: 0x06000353 RID: 851 RVA: 0x0001F364 File Offset: 0x0001E364
			public static int PermanentDevice
			{
				get
				{
					return MapObjectFactory.Type.permanentDevice;
				}
			}

			// Token: 0x17000064 RID: 100
			// (get) Token: 0x06000354 RID: 852 RVA: 0x0001F36B File Offset: 0x0001E36B
			public static int MapLocator
			{
				get
				{
					return MapObjectFactory.Type.mapLocator;
				}
			}

			// Token: 0x17000065 RID: 101
			// (get) Token: 0x06000355 RID: 853 RVA: 0x0001F372 File Offset: 0x0001E372
			public static int PatrolNode
			{
				get
				{
					return MapObjectFactory.Type.patrolNode;
				}
			}

			// Token: 0x17000066 RID: 102
			// (get) Token: 0x06000356 RID: 854 RVA: 0x0001F379 File Offset: 0x0001E379
			public static int ClientSpawnPoint
			{
				get
				{
					return MapObjectFactory.Type.clientSpawnPoint;
				}
			}

			// Token: 0x17000067 RID: 103
			// (get) Token: 0x06000357 RID: 855 RVA: 0x0001F380 File Offset: 0x0001E380
			public static int ClientPatrolNode
			{
				get
				{
					return MapObjectFactory.Type.clientPatrolNode;
				}
			}

			// Token: 0x17000068 RID: 104
			// (get) Token: 0x06000358 RID: 856 RVA: 0x0001F387 File Offset: 0x0001E387
			public static int Sanctuary
			{
				get
				{
					return MapObjectFactory.Type.sanctuary;
				}
			}

			// Token: 0x17000069 RID: 105
			// (get) Token: 0x06000359 RID: 857 RVA: 0x0001F38E File Offset: 0x0001E38E
			public static int AstralBorder
			{
				get
				{
					return MapObjectFactory.Type.astralBorder;
				}
			}

			// Token: 0x1700006A RID: 106
			// (get) Token: 0x0600035A RID: 858 RVA: 0x0001F395 File Offset: 0x0001E395
			public static int Projectile
			{
				get
				{
					return MapObjectFactory.Type.projectile;
				}
			}

			// Token: 0x1700006B RID: 107
			// (get) Token: 0x0600035B RID: 859 RVA: 0x0001F39C File Offset: 0x0001E39C
			public static int PlayerRespawnPlace
			{
				get
				{
					return MapObjectFactory.Type.playerRespawnPlace;
				}
			}

			// Token: 0x1700006C RID: 108
			// (get) Token: 0x0600035C RID: 860 RVA: 0x0001F3A3 File Offset: 0x0001E3A3
			public static int ExtendedSound
			{
				get
				{
					return MapObjectFactory.Type.extendedSound;
				}
			}

			// Token: 0x1700006D RID: 109
			// (get) Token: 0x0600035D RID: 861 RVA: 0x0001F3AA File Offset: 0x0001E3AA
			public static int MultiObject
			{
				get
				{
					return MapObjectFactory.Type.multiObject;
				}
			}

			// Token: 0x1700006E RID: 110
			// (get) Token: 0x0600035E RID: 862 RVA: 0x0001F3B1 File Offset: 0x0001E3B1
			public static int RouteObject
			{
				get
				{
					return MapObjectFactory.Type.routeObject;
				}
			}

			// Token: 0x0600035F RID: 863 RVA: 0x0001F3B8 File Offset: 0x0001E3B8
			public static bool KnownType(int type)
			{
				return type >= MapObjectFactory.Type.firstKnown && type <= MapObjectFactory.Type.lastKnown;
			}

			// Token: 0x06000360 RID: 864 RVA: 0x0001F3CF File Offset: 0x0001E3CF
			public static bool SimpleType(int type)
			{
				return type >= MapObjectFactory.Type.firstSimple && type <= MapObjectFactory.Type.lastSimple;
			}

			// Token: 0x06000361 RID: 865 RVA: 0x0001F3E6 File Offset: 0x0001E3E6
			public static bool SpecialType(int type)
			{
				return type >= MapObjectFactory.Type.firstSpecial && type <= MapObjectFactory.Type.lastSpecial;
			}

			// Token: 0x06000362 RID: 866 RVA: 0x0001F3FD File Offset: 0x0001E3FD
			public static bool TemporaryType(int type)
			{
				return type >= MapObjectFactory.Type.firstTemporary && type <= MapObjectFactory.Type.lastTemporary;
			}

			// Token: 0x06000363 RID: 867 RVA: 0x0001F414 File Offset: 0x0001E414
			public static bool UnknownType(int type)
			{
				return type < MapObjectFactory.Type.firstKnown || type > MapObjectFactory.Type.lastKnown;
			}

			// Token: 0x06000364 RID: 868 RVA: 0x0001F428 File Offset: 0x0001E428
			public static string TypeToSingleObjectTypeName(int type)
			{
				if (MapObjectFactory.Type.KnownType(type))
				{
					if (type == MapObjectFactory.Type.staticObject)
					{
						return MapEditor.Map.MapObjects.StaticObject.InterfaceSingleObjectTypeName;
					}
					if (type == MapObjectFactory.Type.startPoint)
					{
						return MapEditor.Map.MapObjects.StartPoint.InterfaceSingleObjectTypeName;
					}
					if (type == MapObjectFactory.Type.graveyard)
					{
						return MapEditor.Map.MapObjects.Graveyard.InterfaceSingleObjectTypeName;
					}
					if (type == MapObjectFactory.Type.spawnPoint)
					{
						return MapEditor.Map.MapObjects.SpawnPoint.InterfaceSingleObjectTypeName;
					}
					if (type == MapObjectFactory.Type.scriptArea)
					{
						return MapEditor.Map.MapObjects.ScriptArea.InterfaceSingleObjectTypeName;
					}
					if (type == MapObjectFactory.Type.zoneLocator)
					{
						return MapEditor.Map.MapObjects.ZoneLocator.InterfaceSingleObjectTypeName;
					}
					if (type == MapObjectFactory.Type.routePoint)
					{
						return MapEditor.Map.MapObjects.RoutePoint.InterfaceSingleObjectTypeName;
					}
					if (type == MapObjectFactory.Type.permanentDevice)
					{
						return MapEditor.Map.MapObjects.PermanentDevice.InterfaceSingleObjectTypeName;
					}
					if (type == MapObjectFactory.Type.mapLocator)
					{
						return MapEditor.Map.MapObjects.MapLocator.InterfaceSingleObjectTypeName;
					}
					if (type == MapObjectFactory.Type.patrolNode)
					{
						return MapEditor.Map.MapObjects.PatrolNode.InterfaceSingleObjectTypeName;
					}
					if (type == MapObjectFactory.Type.clientSpawnPoint)
					{
						return MapEditor.Map.MapObjects.ClientSpawnPoint.InterfaceSingleObjectTypeName;
					}
					if (type == MapObjectFactory.Type.clientPatrolNode)
					{
						return MapEditor.Map.MapObjects.ClientPatrolNode.InterfaceSingleObjectTypeName;
					}
					if (type == MapObjectFactory.Type.sanctuary)
					{
						return MapEditor.Map.MapObjects.Sanctuary.InterfaceSingleObjectTypeName;
					}
					if (type == MapObjectFactory.Type.astralBorder)
					{
						return MapEditor.Map.MapObjects.AstralBorder.InterfaceSingleObjectTypeName;
					}
					if (type == MapObjectFactory.Type.projectile)
					{
						return MapEditor.Map.MapObjects.Projectile.InterfaceSingleObjectTypeName;
					}
					if (type == MapObjectFactory.Type.playerRespawnPlace)
					{
						return MapEditor.Map.MapObjects.PlayerRespawnPlace.InterfaceSingleObjectTypeName;
					}
					if (type == MapObjectFactory.Type.extendedSound)
					{
						return MapEditor.Map.MapObjects.ExtendedSound.InterfaceSingleObjectTypeName;
					}
					if (type == MapObjectFactory.Type.multiObject)
					{
						return Strings.SINGLE_MULTI_OBJECT_TYPE_NAME;
					}
					if (type == MapObjectFactory.Type.routeObject)
					{
						return Strings.SINGLE_ROUTE_OBJECT_TYPE_NAME;
					}
				}
				return string.Empty;
			}

			// Token: 0x06000365 RID: 869 RVA: 0x0001F550 File Offset: 0x0001E550
			public static int SingleObjectTypeNameToType(string type)
			{
				if (type == MapEditor.Map.MapObjects.StaticObject.InterfaceSingleObjectTypeName)
				{
					return MapObjectFactory.Type.staticObject;
				}
				if (type == MapEditor.Map.MapObjects.StartPoint.InterfaceSingleObjectTypeName)
				{
					return MapObjectFactory.Type.startPoint;
				}
				if (type == MapEditor.Map.MapObjects.Graveyard.InterfaceSingleObjectTypeName)
				{
					return MapObjectFactory.Type.graveyard;
				}
				if (type == MapEditor.Map.MapObjects.SpawnPoint.InterfaceSingleObjectTypeName)
				{
					return MapObjectFactory.Type.spawnPoint;
				}
				if (type == MapEditor.Map.MapObjects.ScriptArea.InterfaceSingleObjectTypeName)
				{
					return MapObjectFactory.Type.scriptArea;
				}
				if (type == MapEditor.Map.MapObjects.ZoneLocator.InterfaceSingleObjectTypeName)
				{
					return MapObjectFactory.Type.zoneLocator;
				}
				if (type == MapEditor.Map.MapObjects.RoutePoint.InterfaceSingleObjectTypeName)
				{
					return MapObjectFactory.Type.routePoint;
				}
				if (type == MapEditor.Map.MapObjects.PermanentDevice.InterfaceSingleObjectTypeName)
				{
					return MapObjectFactory.Type.permanentDevice;
				}
				if (type == MapEditor.Map.MapObjects.MapLocator.InterfaceSingleObjectTypeName)
				{
					return MapObjectFactory.Type.mapLocator;
				}
				if (type == MapEditor.Map.MapObjects.PatrolNode.InterfaceSingleObjectTypeName)
				{
					return MapObjectFactory.Type.patrolNode;
				}
				if (type == MapEditor.Map.MapObjects.ClientSpawnPoint.InterfaceSingleObjectTypeName)
				{
					return MapObjectFactory.Type.clientSpawnPoint;
				}
				if (type == MapEditor.Map.MapObjects.ClientPatrolNode.InterfaceSingleObjectTypeName)
				{
					return MapObjectFactory.Type.clientPatrolNode;
				}
				if (type == MapEditor.Map.MapObjects.Sanctuary.InterfaceSingleObjectTypeName)
				{
					return MapObjectFactory.Type.sanctuary;
				}
				if (type == MapEditor.Map.MapObjects.AstralBorder.InterfaceSingleObjectTypeName)
				{
					return MapObjectFactory.Type.astralBorder;
				}
				if (type == MapEditor.Map.MapObjects.Projectile.InterfaceSingleObjectTypeName)
				{
					return MapObjectFactory.Type.projectile;
				}
				if (type == MapEditor.Map.MapObjects.PlayerRespawnPlace.InterfaceSingleObjectTypeName)
				{
					return MapObjectFactory.Type.playerRespawnPlace;
				}
				if (type == MapEditor.Map.MapObjects.ExtendedSound.InterfaceSingleObjectTypeName)
				{
					return MapObjectFactory.Type.extendedSound;
				}
				if (type == Strings.SINGLE_MULTI_OBJECT_TYPE_NAME)
				{
					return MapObjectFactory.Type.multiObject;
				}
				if (type == Strings.SINGLE_ROUTE_OBJECT_TYPE_NAME)
				{
					return MapObjectFactory.Type.multiObject;
				}
				return MapObjectFactory.Type.unknown;
			}

			// Token: 0x06000366 RID: 870 RVA: 0x0001F6CC File Offset: 0x0001E6CC
			public static string TypeToSeveralObjectsTypeName(int type)
			{
				if (MapObjectFactory.Type.KnownType(type))
				{
					if (type == MapObjectFactory.Type.staticObject)
					{
						return MapEditor.Map.MapObjects.StaticObject.InterfaceSeveralObjectsTypeName;
					}
					if (type == MapObjectFactory.Type.startPoint)
					{
						return MapEditor.Map.MapObjects.StartPoint.InterfaceSeveralObjectsTypeName;
					}
					if (type == MapObjectFactory.Type.graveyard)
					{
						return MapEditor.Map.MapObjects.Graveyard.InterfaceSeveralObjectsTypeName;
					}
					if (type == MapObjectFactory.Type.spawnPoint)
					{
						return MapEditor.Map.MapObjects.SpawnPoint.InterfaceSeveralObjectsTypeName;
					}
					if (type == MapObjectFactory.Type.scriptArea)
					{
						return MapEditor.Map.MapObjects.ScriptArea.InterfaceSeveralObjectsTypeName;
					}
					if (type == MapObjectFactory.Type.zoneLocator)
					{
						return MapEditor.Map.MapObjects.ZoneLocator.InterfaceSeveralObjectsTypeName;
					}
					if (type == MapObjectFactory.Type.routePoint)
					{
						return MapEditor.Map.MapObjects.RoutePoint.InterfaceSeveralObjectsTypeName;
					}
					if (type == MapObjectFactory.Type.permanentDevice)
					{
						return MapEditor.Map.MapObjects.PermanentDevice.InterfaceSeveralObjectsTypeName;
					}
					if (type == MapObjectFactory.Type.mapLocator)
					{
						return MapEditor.Map.MapObjects.MapLocator.InterfaceSeveralObjectsTypeName;
					}
					if (type == MapObjectFactory.Type.patrolNode)
					{
						return MapEditor.Map.MapObjects.PatrolNode.InterfaceSeveralObjectsTypeName;
					}
					if (type == MapObjectFactory.Type.clientSpawnPoint)
					{
						return MapEditor.Map.MapObjects.ClientSpawnPoint.InterfaceSeveralObjectsTypeName;
					}
					if (type == MapObjectFactory.Type.clientPatrolNode)
					{
						return MapEditor.Map.MapObjects.ClientPatrolNode.InterfaceSeveralObjectsTypeName;
					}
					if (type == MapObjectFactory.Type.sanctuary)
					{
						return MapEditor.Map.MapObjects.Sanctuary.InterfaceSeveralObjectsTypeName;
					}
					if (type == MapObjectFactory.Type.astralBorder)
					{
						return MapEditor.Map.MapObjects.AstralBorder.InterfaceSeveralObjectsTypeName;
					}
					if (type == MapObjectFactory.Type.projectile)
					{
						return MapEditor.Map.MapObjects.Projectile.InterfaceSeveralObjectsTypeName;
					}
					if (type == MapObjectFactory.Type.playerRespawnPlace)
					{
						return MapEditor.Map.MapObjects.PlayerRespawnPlace.InterfaceSeveralObjectsTypeName;
					}
					if (type == MapObjectFactory.Type.extendedSound)
					{
						return MapEditor.Map.MapObjects.ExtendedSound.InterfaceSeveralObjectsTypeName;
					}
					if (type == MapObjectFactory.Type.multiObject)
					{
						return Strings.SEVERAL_MULTI_OBJECTS_TYPE_NAME;
					}
					if (type == MapObjectFactory.Type.routeObject)
					{
						return Strings.SEVERAL_ROUTE_OBJECTS_TYPE_NAME;
					}
				}
				return string.Empty;
			}

			// Token: 0x06000367 RID: 871 RVA: 0x0001F7F4 File Offset: 0x0001E7F4
			public static int SeveralObjectsTypeNameToType(string type)
			{
				if (type == MapEditor.Map.MapObjects.StaticObject.InterfaceSeveralObjectsTypeName)
				{
					return MapObjectFactory.Type.staticObject;
				}
				if (type == MapEditor.Map.MapObjects.StartPoint.InterfaceSeveralObjectsTypeName)
				{
					return MapObjectFactory.Type.startPoint;
				}
				if (type == MapEditor.Map.MapObjects.Graveyard.InterfaceSeveralObjectsTypeName)
				{
					return MapObjectFactory.Type.graveyard;
				}
				if (type == MapEditor.Map.MapObjects.SpawnPoint.InterfaceSeveralObjectsTypeName)
				{
					return MapObjectFactory.Type.spawnPoint;
				}
				if (type == MapEditor.Map.MapObjects.ScriptArea.InterfaceSeveralObjectsTypeName)
				{
					return MapObjectFactory.Type.scriptArea;
				}
				if (type == MapEditor.Map.MapObjects.ZoneLocator.InterfaceSeveralObjectsTypeName)
				{
					return MapObjectFactory.Type.zoneLocator;
				}
				if (type == MapEditor.Map.MapObjects.RoutePoint.InterfaceSeveralObjectsTypeName)
				{
					return MapObjectFactory.Type.routePoint;
				}
				if (type == MapEditor.Map.MapObjects.PermanentDevice.InterfaceSeveralObjectsTypeName)
				{
					return MapObjectFactory.Type.permanentDevice;
				}
				if (type == MapEditor.Map.MapObjects.MapLocator.InterfaceSeveralObjectsTypeName)
				{
					return MapObjectFactory.Type.mapLocator;
				}
				if (type == MapEditor.Map.MapObjects.PatrolNode.InterfaceSeveralObjectsTypeName)
				{
					return MapObjectFactory.Type.patrolNode;
				}
				if (type == MapEditor.Map.MapObjects.ClientSpawnPoint.InterfaceSeveralObjectsTypeName)
				{
					return MapObjectFactory.Type.clientSpawnPoint;
				}
				if (type == MapEditor.Map.MapObjects.ClientPatrolNode.InterfaceSeveralObjectsTypeName)
				{
					return MapObjectFactory.Type.clientPatrolNode;
				}
				if (type == MapEditor.Map.MapObjects.Sanctuary.InterfaceSeveralObjectsTypeName)
				{
					return MapObjectFactory.Type.sanctuary;
				}
				if (type == MapEditor.Map.MapObjects.AstralBorder.InterfaceSeveralObjectsTypeName)
				{
					return MapObjectFactory.Type.astralBorder;
				}
				if (type == MapEditor.Map.MapObjects.Projectile.InterfaceSeveralObjectsTypeName)
				{
					return MapObjectFactory.Type.projectile;
				}
				if (type == MapEditor.Map.MapObjects.PlayerRespawnPlace.InterfaceSeveralObjectsTypeName)
				{
					return MapObjectFactory.Type.playerRespawnPlace;
				}
				if (type == MapEditor.Map.MapObjects.ExtendedSound.InterfaceSeveralObjectsTypeName)
				{
					return MapObjectFactory.Type.extendedSound;
				}
				if (type == Strings.SEVERAL_MULTI_OBJECTS_TYPE_NAME)
				{
					return MapObjectFactory.Type.multiObject;
				}
				if (type == Strings.SEVERAL_ROUTE_OBJECTS_TYPE_NAME)
				{
					return MapObjectFactory.Type.routeObject;
				}
				return MapObjectFactory.Type.unknown;
			}

			// Token: 0x04000273 RID: 627
			private static readonly int unknown = 0;

			// Token: 0x04000274 RID: 628
			private static readonly int firstKnown = 2;

			// Token: 0x04000275 RID: 629
			private static readonly int firstSimple = 2;

			// Token: 0x04000276 RID: 630
			private static readonly int staticObject = 2;

			// Token: 0x04000277 RID: 631
			private static readonly int firstSpecial = 3;

			// Token: 0x04000278 RID: 632
			private static readonly int startPoint = 3;

			// Token: 0x04000279 RID: 633
			private static readonly int graveyard = 4;

			// Token: 0x0400027A RID: 634
			private static readonly int spawnPoint = 5;

			// Token: 0x0400027B RID: 635
			private static readonly int scriptArea = 6;

			// Token: 0x0400027C RID: 636
			private static readonly int zoneLocator = 7;

			// Token: 0x0400027D RID: 637
			private static readonly int routePoint = 8;

			// Token: 0x0400027E RID: 638
			private static readonly int permanentDevice = 9;

			// Token: 0x0400027F RID: 639
			private static readonly int mapLocator = 10;

			// Token: 0x04000280 RID: 640
			private static readonly int patrolNode = 11;

			// Token: 0x04000281 RID: 641
			private static readonly int clientSpawnPoint = 12;

			// Token: 0x04000282 RID: 642
			private static readonly int clientPatrolNode = 13;

			// Token: 0x04000283 RID: 643
			private static readonly int sanctuary = 14;

			// Token: 0x04000284 RID: 644
			private static readonly int astralBorder = 15;

			// Token: 0x04000285 RID: 645
			private static readonly int projectile = 16;

			// Token: 0x04000286 RID: 646
			private static readonly int playerRespawnPlace = 17;

			// Token: 0x04000287 RID: 647
			private static readonly int extendedSound = 18;

			// Token: 0x04000288 RID: 648
			private static readonly int lastSpecial = 18;

			// Token: 0x04000289 RID: 649
			private static readonly int lastSimple = 18;

			// Token: 0x0400028A RID: 650
			private static readonly int firstTemporary = 19;

			// Token: 0x0400028B RID: 651
			private static readonly int multiObject = 19;

			// Token: 0x0400028C RID: 652
			private static readonly int routeObject = 20;

			// Token: 0x0400028D RID: 653
			private static readonly int lastTemporary = 20;

			// Token: 0x0400028E RID: 654
			private static readonly int lastKnown = 20;
		}
	}
}
