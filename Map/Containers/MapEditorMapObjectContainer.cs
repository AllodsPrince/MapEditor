using System;
using System.Collections.Generic;
using MapEditor.Map.MapObjects;
using Operations;
using Tools.Landscape;
using Tools.MapObjects;

namespace MapEditor.Map.Containers
{
	// Token: 0x0200025E RID: 606
	public class MapEditorMapObjectContainer : MapObjectContainer
	{
		// Token: 0x06001CBF RID: 7359 RVA: 0x000B7250 File Offset: 0x000B6250
		private void OnPreMapObjectAdded(MapObjectContainer _mapObjectContainer, IMapObject mapObject)
		{
			if (mapObject != null)
			{
				TypedMapObjectContainer typedMapObjectContainer = this.GetTypedMapObjectContainer(mapObject);
				if (typedMapObjectContainer != null)
				{
					typedMapObjectContainer.AddMapObject(mapObject);
				}
			}
		}

		// Token: 0x06001CC0 RID: 7360 RVA: 0x000B7274 File Offset: 0x000B6274
		private void OnPreMapObjectRemoved(MapObjectContainer _mapObjectContainer, IMapObject mapObject)
		{
			if (mapObject != null)
			{
				TypedMapObjectContainer typedMapObjectContainer = this.GetTypedMapObjectContainer(mapObject);
				if (typedMapObjectContainer != null)
				{
					typedMapObjectContainer.RemoveMapObject(mapObject);
				}
			}
		}

		// Token: 0x06001CC1 RID: 7361 RVA: 0x000B7298 File Offset: 0x000B6298
		public MapEditorMapObjectContainer(OperationContainer operationContainer, IMapObjectFactory _mapObjectFactory, IMapObjectPicker _externalMapObjectPicker, ICollisionMap _externalCollisionMap, ObjectBounds _objectBounds, LandscapeToolContainer _landscapeToolContainer, EditorScene editorScene) : base(_mapObjectFactory, _externalMapObjectPicker, _externalCollisionMap, _objectBounds, _landscapeToolContainer)
		{
			this.typedMapObjectContainers = new List<TypedMapObjectContainer>();
			this.typedMapObjectContainers.Add(new StaticObjectContainer(this));
			this.typedMapObjectContainers.Add(new StartPointContainer(this, operationContainer));
			this.typedMapObjectContainers.Add(new GraveyardContainer(this));
			this.typedMapObjectContainers.Add(new SpawnPointContainer(this));
			this.typedMapObjectContainers.Add(new ScriptAreaContainer(this));
			this.typedMapObjectContainers.Add(new ZoneLocatorContainer(this));
			this.typedMapObjectContainers.Add(new RoutePointContainer(this, operationContainer));
			this.typedMapObjectContainers.Add(new PermanentDeviceContainer(this));
			this.typedMapObjectContainers.Add(new MapLocatorContainer(this));
			this.typedMapObjectContainers.Add(new PatrolNodeContainer(this));
			this.typedMapObjectContainers.Add(new ClientSpawnPointContainer(this));
			this.typedMapObjectContainers.Add(new ClientPatrolNodeContainer(this));
			this.typedMapObjectContainers.Add(new SanctuaryContainer(this));
			this.typedMapObjectContainers.Add(new AstralBorderContainer(this));
			this.typedMapObjectContainers.Add(new ProjectileContainer(this));
			this.typedMapObjectContainers.Add(new PlayerRespawnPlaceContainer(this));
			this.typedMapObjectContainers.Add(new ExtendedSoundContainer(this));
			this.typedMapObjectContainers.Add(null);
			this.typedMapObjectContainers.Add(new RouteObjectContainer(editorScene, this));
			base.PreMapObjectAdded += this.OnPreMapObjectAdded;
			base.PreMapObjectRemoved += this.OnPreMapObjectRemoved;
		}

		// Token: 0x06001CC2 RID: 7362 RVA: 0x000B7424 File Offset: 0x000B6424
		public override void Destroy()
		{
			base.PreMapObjectAdded -= this.OnPreMapObjectAdded;
			base.PreMapObjectRemoved -= this.OnPreMapObjectRemoved;
			foreach (TypedMapObjectContainer typedMapObjectContainer in this.typedMapObjectContainers)
			{
				if (typedMapObjectContainer != null)
				{
					typedMapObjectContainer.Destroy();
				}
			}
			this.typedMapObjectContainers.Clear();
			base.Destroy();
		}

		// Token: 0x06001CC3 RID: 7363 RVA: 0x000B74B0 File Offset: 0x000B64B0
		public TypedMapObjectContainer GetTypedMapObjectContainer(int type)
		{
			int index = type - MapEditor.Map.MapObjects.MapObjectFactory.Type.FirstSimpleType;
			if (index >= 0 && index < this.typedMapObjectContainers.Count)
			{
				return this.typedMapObjectContainers[index];
			}
			return null;
		}

		// Token: 0x06001CC4 RID: 7364 RVA: 0x000B74E8 File Offset: 0x000B64E8
		public TypedMapObjectContainer GetTypedMapObjectContainer(IMapObject mapObject)
		{
			if (mapObject != null)
			{
				return this.GetTypedMapObjectContainer(mapObject.Type.Type);
			}
			return null;
		}

		// Token: 0x170006CF RID: 1743
		// (get) Token: 0x06001CC5 RID: 7365 RVA: 0x000B750E File Offset: 0x000B650E
		public StaticObjectContainer StaticObjectContainer
		{
			get
			{
				return this.GetTypedMapObjectContainer(MapEditor.Map.MapObjects.MapObjectFactory.Type.StaticObject) as StaticObjectContainer;
			}
		}

		// Token: 0x170006D0 RID: 1744
		// (get) Token: 0x06001CC6 RID: 7366 RVA: 0x000B7520 File Offset: 0x000B6520
		public StartPointContainer StartPointContainer
		{
			get
			{
				return this.GetTypedMapObjectContainer(MapEditor.Map.MapObjects.MapObjectFactory.Type.StartPoint) as StartPointContainer;
			}
		}

		// Token: 0x170006D1 RID: 1745
		// (get) Token: 0x06001CC7 RID: 7367 RVA: 0x000B7532 File Offset: 0x000B6532
		public GraveyardContainer GraveyardContainer
		{
			get
			{
				return this.GetTypedMapObjectContainer(MapEditor.Map.MapObjects.MapObjectFactory.Type.Graveyard) as GraveyardContainer;
			}
		}

		// Token: 0x170006D2 RID: 1746
		// (get) Token: 0x06001CC8 RID: 7368 RVA: 0x000B7544 File Offset: 0x000B6544
		public SpawnPointContainer SpawnPointContainer
		{
			get
			{
				return this.GetTypedMapObjectContainer(MapEditor.Map.MapObjects.MapObjectFactory.Type.SpawnPoint) as SpawnPointContainer;
			}
		}

		// Token: 0x170006D3 RID: 1747
		// (get) Token: 0x06001CC9 RID: 7369 RVA: 0x000B7556 File Offset: 0x000B6556
		public ScriptAreaContainer ScriptAreaContainer
		{
			get
			{
				return this.GetTypedMapObjectContainer(MapEditor.Map.MapObjects.MapObjectFactory.Type.ScriptArea) as ScriptAreaContainer;
			}
		}

		// Token: 0x170006D4 RID: 1748
		// (get) Token: 0x06001CCA RID: 7370 RVA: 0x000B7568 File Offset: 0x000B6568
		public ZoneLocatorContainer ZoneLocatorContainer
		{
			get
			{
				return this.GetTypedMapObjectContainer(MapEditor.Map.MapObjects.MapObjectFactory.Type.ZoneLocator) as ZoneLocatorContainer;
			}
		}

		// Token: 0x170006D5 RID: 1749
		// (get) Token: 0x06001CCB RID: 7371 RVA: 0x000B757A File Offset: 0x000B657A
		public RoutePointContainer RoutePointContainer
		{
			get
			{
				return this.GetTypedMapObjectContainer(MapEditor.Map.MapObjects.MapObjectFactory.Type.RoutePoint) as RoutePointContainer;
			}
		}

		// Token: 0x170006D6 RID: 1750
		// (get) Token: 0x06001CCC RID: 7372 RVA: 0x000B758C File Offset: 0x000B658C
		public PermanentDeviceContainer PermanentDeviceContainer
		{
			get
			{
				return this.GetTypedMapObjectContainer(MapEditor.Map.MapObjects.MapObjectFactory.Type.PermanentDevice) as PermanentDeviceContainer;
			}
		}

		// Token: 0x170006D7 RID: 1751
		// (get) Token: 0x06001CCD RID: 7373 RVA: 0x000B759E File Offset: 0x000B659E
		public MapLocatorContainer MapLocatorContainer
		{
			get
			{
				return this.GetTypedMapObjectContainer(MapEditor.Map.MapObjects.MapObjectFactory.Type.MapLocator) as MapLocatorContainer;
			}
		}

		// Token: 0x170006D8 RID: 1752
		// (get) Token: 0x06001CCE RID: 7374 RVA: 0x000B75B0 File Offset: 0x000B65B0
		public PatrolNodeContainer PatrolNodeContainer
		{
			get
			{
				return this.GetTypedMapObjectContainer(MapEditor.Map.MapObjects.MapObjectFactory.Type.PatrolNode) as PatrolNodeContainer;
			}
		}

		// Token: 0x170006D9 RID: 1753
		// (get) Token: 0x06001CCF RID: 7375 RVA: 0x000B75C2 File Offset: 0x000B65C2
		public ClientSpawnPointContainer ClientSpawnPointContainer
		{
			get
			{
				return this.GetTypedMapObjectContainer(MapEditor.Map.MapObjects.MapObjectFactory.Type.ClientSpawnPoint) as ClientSpawnPointContainer;
			}
		}

		// Token: 0x170006DA RID: 1754
		// (get) Token: 0x06001CD0 RID: 7376 RVA: 0x000B75D4 File Offset: 0x000B65D4
		public ClientPatrolNodeContainer ClientPatrolNodeContainer
		{
			get
			{
				return this.GetTypedMapObjectContainer(MapEditor.Map.MapObjects.MapObjectFactory.Type.ClientPatrolNode) as ClientPatrolNodeContainer;
			}
		}

		// Token: 0x170006DB RID: 1755
		// (get) Token: 0x06001CD1 RID: 7377 RVA: 0x000B75E6 File Offset: 0x000B65E6
		public SanctuaryContainer SanctuaryContainer
		{
			get
			{
				return this.GetTypedMapObjectContainer(MapEditor.Map.MapObjects.MapObjectFactory.Type.Sanctuary) as SanctuaryContainer;
			}
		}

		// Token: 0x170006DC RID: 1756
		// (get) Token: 0x06001CD2 RID: 7378 RVA: 0x000B75F8 File Offset: 0x000B65F8
		public AstralBorderContainer AstralBorderContainer
		{
			get
			{
				return this.GetTypedMapObjectContainer(MapEditor.Map.MapObjects.MapObjectFactory.Type.AstralBorder) as AstralBorderContainer;
			}
		}

		// Token: 0x170006DD RID: 1757
		// (get) Token: 0x06001CD3 RID: 7379 RVA: 0x000B760A File Offset: 0x000B660A
		public ProjectileContainer ProjectileContainer
		{
			get
			{
				return this.GetTypedMapObjectContainer(MapEditor.Map.MapObjects.MapObjectFactory.Type.Projectile) as ProjectileContainer;
			}
		}

		// Token: 0x170006DE RID: 1758
		// (get) Token: 0x06001CD4 RID: 7380 RVA: 0x000B761C File Offset: 0x000B661C
		public PlayerRespawnPlaceContainer PlayerRespawnPlaceContainer
		{
			get
			{
				return this.GetTypedMapObjectContainer(MapEditor.Map.MapObjects.MapObjectFactory.Type.PlayerRespawnPlace) as PlayerRespawnPlaceContainer;
			}
		}

		// Token: 0x170006DF RID: 1759
		// (get) Token: 0x06001CD5 RID: 7381 RVA: 0x000B762E File Offset: 0x000B662E
		public RouteObjectContainer RouteObjectContainer
		{
			get
			{
				return this.GetTypedMapObjectContainer(MapEditor.Map.MapObjects.MapObjectFactory.Type.RouteObject) as RouteObjectContainer;
			}
		}

		// Token: 0x170006E0 RID: 1760
		// (get) Token: 0x06001CD6 RID: 7382 RVA: 0x000B7640 File Offset: 0x000B6640
		public ExtendedSoundContainer ExtendedSoundContainer
		{
			get
			{
				return this.GetTypedMapObjectContainer(MapEditor.Map.MapObjects.MapObjectFactory.Type.ExtendedSound) as ExtendedSoundContainer;
			}
		}

		// Token: 0x04001255 RID: 4693
		private readonly List<TypedMapObjectContainer> typedMapObjectContainers;
	}
}
