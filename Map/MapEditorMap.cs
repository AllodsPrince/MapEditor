using System;
using MapEditor.Map.Containers;
using MapEditor.Map.Landscape;
using MapEditor.Map.MapObjectElements;
using MapEditor.Map.MapObjects;
using MapEditor.Map.Minimap;
using MapEditor.Scene;
using MapInfo;
using Operations;
using Tools.Geometry;
using Tools.Groups;
using Tools.Landscape;
using Tools.MapObjects;
using Tools.MapSound;
using Tools.MapZoneLights;

namespace MapEditor.Map
{
	// Token: 0x02000128 RID: 296
	public class MapEditorMap
	{
		// Token: 0x06000EAF RID: 3759 RVA: 0x00076BE8 File Offset: 0x00075BE8
		public MapEditorMap(MainForm.Context context, int patchX, int patchY, string mapResourceName, string continentName, ContinentType continentType, ref Position startCameraPosition, int mapSize)
		{
			this.data = new Variables(patchX, patchY, mapResourceName, continentName, continentType, ref startCameraPosition, mapSize);
			this.operationContainer = context.OperationContainer;
			this.mapObjectFactory = new MapObjectFactory();
			this.mapEditorLandscapeToolContainer = new MapEditorLandscapeToolContainer();
			this.terrainRegionContainer = new TerrainRegionContainer();
			this.landscapeChangesContainer = new LandscapeChangesContainer();
			this.editingBlocker = new EditingBlocker(this, context.EditorScene);
			ObjectBounds objectBounds = null;
			if (continentType == ContinentType.Continent)
			{
				objectBounds = new ObjectBounds();
				objectBounds.PositionBounds.CheckX = true;
				objectBounds.PositionBounds.CheckY = true;
				objectBounds.PositionBounds.Min = new Vec3((double)(this.data.MinXMinYPatchCoords.X * Constants.PatchSize), (double)(this.data.MinXMinYPatchCoords.Y * Constants.PatchSize), 0.0);
				objectBounds.PositionBounds.Max = new Vec3((double)((this.data.MinXMinYPatchCoords.X + this.data.MapSize.X) * Constants.PatchSize), (double)((this.data.MinXMinYPatchCoords.Y + this.data.MapSize.Y) * Constants.PatchSize), 0.0);
				objectBounds.ScaleBounds.CheckRatio = true;
				objectBounds.ScaleBounds.MinRatio = 0.1f;
				objectBounds.ScaleBounds.MaxRatio = 10f;
				objectBounds.ScaleBounds.CheckX = true;
				objectBounds.ScaleBounds.MinX = 0.1f;
				objectBounds.ScaleBounds.MaxX = 10f;
				objectBounds.ScaleBounds.CheckY = true;
				objectBounds.ScaleBounds.MinY = 0.1f;
				objectBounds.ScaleBounds.MaxY = 10f;
				objectBounds.ScaleBounds.CheckZ = true;
				objectBounds.ScaleBounds.MinZ = 0.1f;
				objectBounds.ScaleBounds.MaxZ = 10f;
			}
			this.mapEditorMapObjectContainer = new MapEditorMapObjectContainer(context.OperationContainer, this.mapObjectFactory, null, null, objectBounds, this.mapEditorLandscapeToolContainer, context.EditorScene);
			this.groupContainer = new GroupContainer();
			this.groupContainer.Bind(this.mapEditorMapObjectContainer);
			this.mapObjectOperationContainer = new MapObjectOperationContainer(this.operationContainer);
			this.startPointOperationContainer = new StartPointOperationContainer(this.operationContainer);
			this.respawnOperationContainer = new RespawnOperationContainer(this.operationContainer);
			this.spawnPointOperationContainer = new SpawnPointOperationContainer(this.operationContainer);
			this.scriptAreaOperationContainer = new ScriptAreaOperationContainer(this.operationContainer);
			this.zoneLocatorOperationContainer = new ZoneLocatorOperationContainer(this.operationContainer);
			this.routePointOperationContainer = new RoutePointOperationContainer(this.operationContainer);
			this.permanentDeviceOperationContainer = new PermanentDeviceOperationContainer(this.operationContainer);
			this.staticObjectOperationContainer = new StaticObjectOperationContainer(this.operationContainer);
			this.mapLocatorOperationContainer = new MapLocatorOperationContainer(this.operationContainer);
			this.patrolNodeOperationContainer = new PatrolNodeOperationContainer(this.operationContainer);
			this.clientSpawnPointOperationContainer = new ClientSpawnPointOperationContainer(this.operationContainer);
			this.clientPatrolNodeOperationContainer = new ClientPatrolNodeOperationContainer(this.operationContainer);
			this.astralBorderOperationContainer = new AstralBorderOperationContainer(this.operationContainer);
			this.spawnTimeOperationContainer = new SpawnTimeOperationContainer(this.operationContainer);
			this.singleSpawnControllerOperationContainer = new SingleSpawnControllerOperationContainer(this.operationContainer);
			this.projectileOperationContainer = new ProjectileOperationContainer(this.operationContainer);
			this.playerRespawnPlaceOperationContainer = new PlayerRespawnPlaceOperationContainer(this.operationContainer);
			this.extendedSoundOperationContainer = new ExtendedSoundOperationContainer(this.operationContainer);
			int mapZoneContainerX = Constants.PatchSize * this.data.MapSize.X / Constants.MapZonePieceSize.X;
			int mapZoneContainerY = Constants.PatchSize * this.data.MapSize.Y / Constants.MapZonePieceSize.Y;
			this.mapZoneContainer = new MapZoneContainer(mapZoneContainerX, mapZoneContainerY);
			this.zoneLightContainer = new ZoneLightContainer(mapZoneContainerX, mapZoneContainerY);
			this.mapMusicContainer = new MapSoundContainer(mapZoneContainerX, mapZoneContainerY);
			this.mapAmbienceContainer = new MapSoundContainer(mapZoneContainerX, mapZoneContainerY);
			this.terrainRegionOperationContainer = new TerrainRegionOperationContainer(this.operationContainer);
			this.landscapeToolOperationContainer = new LandscapeToolOperationContainer(this.operationContainer);
			this.mapZoneOperationContainer = new MapZoneOperationContainer(this.operationContainer);
			this.zoneLightOperationContainer = new ZoneLightOperationContainer(this.operationContainer);
			this.mapMusicOperationContainer = new MapSoundOperationContainer(this.operationContainer);
			this.mapAmbienceOperationContainer = new MapSoundOperationContainer(this.operationContainer);
			this.groupOperationContainer = new GroupOperationContainer(this.operationContainer);
			this.mapObjectOperationContainer.Bind(this.mapEditorMapObjectContainer);
			this.startPointOperationContainer.Bind(this.mapEditorMapObjectContainer);
			this.respawnOperationContainer.Bind(this.mapEditorMapObjectContainer);
			this.spawnPointOperationContainer.Bind(this.mapEditorMapObjectContainer);
			this.scriptAreaOperationContainer.Bind(this.mapEditorMapObjectContainer);
			this.zoneLocatorOperationContainer.Bind(this.mapEditorMapObjectContainer);
			this.routePointOperationContainer.Bind(this.mapEditorMapObjectContainer);
			this.permanentDeviceOperationContainer.Bind(this.mapEditorMapObjectContainer);
			this.staticObjectOperationContainer.Bind(this.mapEditorMapObjectContainer);
			this.mapLocatorOperationContainer.Bind(this.mapEditorMapObjectContainer);
			this.patrolNodeOperationContainer.Bind(this.mapEditorMapObjectContainer);
			this.clientSpawnPointOperationContainer.Bind(this.mapEditorMapObjectContainer);
			this.clientPatrolNodeOperationContainer.Bind(this.mapEditorMapObjectContainer);
			this.astralBorderOperationContainer.Bind(this.mapEditorMapObjectContainer);
			this.spawnTimeOperationContainer.Bind(this.mapEditorMapObjectContainer);
			this.singleSpawnControllerOperationContainer.Bind(this.mapEditorMapObjectContainer);
			this.projectileOperationContainer.Bind(this.mapEditorMapObjectContainer);
			this.playerRespawnPlaceOperationContainer.Bind(this.mapEditorMapObjectContainer);
			this.extendedSoundOperationContainer.Bind(this.mapEditorMapObjectContainer);
			this.terrainRegionOperationContainer.Bind(this.terrainRegionContainer);
			this.landscapeToolOperationContainer.Bind(this.mapEditorLandscapeToolContainer, this.landscapeChangesContainer);
			this.mapZoneOperationContainer.Bind(this.mapZoneContainer);
			this.zoneLightOperationContainer.Bind(this.zoneLightContainer);
			this.mapMusicOperationContainer.Bind(this.mapMusicContainer);
			this.mapAmbienceOperationContainer.Bind(this.mapAmbienceContainer);
			this.groupOperationContainer.Bind(this.groupContainer);
			this.minimapContainer = new MinimapContainer(this, this.operationContainer, context);
			this.polygonContainer = new PolygonContainer(null);
			this.squareContainer = new AxisAlignedSquareContainer(null);
			this.stripeContainer = new StripeContainer(this.polygonContainer);
		}

		// Token: 0x06000EB0 RID: 3760 RVA: 0x0007727C File Offset: 0x0007627C
		public void Destroy()
		{
			if (this.data != null)
			{
				this.groupContainer.Unbind();
				this.terrainRegionOperationContainer.Unbind();
				this.landscapeToolOperationContainer.Unbind();
				this.mapObjectOperationContainer.Unbind();
				this.startPointOperationContainer.Unbind();
				this.respawnOperationContainer.Unbind();
				this.spawnPointOperationContainer.Unbind();
				this.scriptAreaOperationContainer.Unbind();
				this.zoneLocatorOperationContainer.Unbind();
				this.routePointOperationContainer.Unbind();
				this.permanentDeviceOperationContainer.Unbind();
				this.staticObjectOperationContainer.Unbind();
				this.mapLocatorOperationContainer.Unbind();
				this.patrolNodeOperationContainer.Unbind();
				this.clientSpawnPointOperationContainer.Unbind();
				this.clientPatrolNodeOperationContainer.Unbind();
				this.astralBorderOperationContainer.Unbind();
				this.mapZoneOperationContainer.Unbind();
				this.zoneLightOperationContainer.Unbind();
				this.mapMusicOperationContainer.Unbind();
				this.mapAmbienceOperationContainer.Unbind();
				this.groupOperationContainer.Unbind();
				this.spawnTimeOperationContainer.Unbind();
				this.singleSpawnControllerOperationContainer.Unbind();
				this.projectileOperationContainer.Unbind();
				this.playerRespawnPlaceOperationContainer.Unbind();
				this.extendedSoundOperationContainer.Unbind();
				this.mapEditorLandscapeToolContainer.Destroy();
				this.terrainRegionContainer.Destroy();
				this.landscapeChangesContainer.Destroy();
				this.mapEditorMapObjectContainer.Destroy();
				this.minimapContainer.Destroy();
				this.polygonContainer.Destroy();
				this.squareContainer.Destroy();
				this.stripeContainer.Destroy();
				this.terrainRegionOperationContainer.Destroy();
				this.landscapeToolOperationContainer.Destroy();
				this.mapObjectOperationContainer.Destroy();
				this.startPointOperationContainer.Destroy();
				this.respawnOperationContainer.Destroy();
				this.spawnPointOperationContainer.Destroy();
				this.scriptAreaOperationContainer.Destroy();
				this.zoneLocatorOperationContainer.Destroy();
				this.routePointOperationContainer.Destroy();
				this.permanentDeviceOperationContainer.Destroy();
				this.staticObjectOperationContainer.Destroy();
				this.mapLocatorOperationContainer.Destroy();
				this.patrolNodeOperationContainer.Destroy();
				this.clientSpawnPointOperationContainer.Destroy();
				this.clientPatrolNodeOperationContainer.Destroy();
				this.astralBorderOperationContainer.Destroy();
				this.mapZoneOperationContainer.Destroy();
				this.zoneLightOperationContainer.Destroy();
				this.mapMusicOperationContainer.Destroy();
				this.mapAmbienceOperationContainer.Destroy();
				this.groupOperationContainer.Destroy();
				this.spawnTimeOperationContainer.Destroy();
				this.singleSpawnControllerOperationContainer.Destroy();
				this.projectileOperationContainer.Destroy();
				this.playerRespawnPlaceOperationContainer.Destroy();
				this.extendedSoundOperationContainer.Destroy();
				this.terrainRegionOperationContainer = null;
				this.landscapeToolOperationContainer = null;
				this.mapObjectOperationContainer = null;
				this.startPointOperationContainer = null;
				this.respawnOperationContainer = null;
				this.spawnPointOperationContainer = null;
				this.scriptAreaOperationContainer = null;
				this.zoneLocatorOperationContainer = null;
				this.routePointOperationContainer = null;
				this.permanentDeviceOperationContainer = null;
				this.staticObjectOperationContainer = null;
				this.mapLocatorOperationContainer = null;
				this.patrolNodeOperationContainer = null;
				this.clientSpawnPointOperationContainer = null;
				this.clientPatrolNodeOperationContainer = null;
				this.astralBorderOperationContainer = null;
				this.mapZoneOperationContainer = null;
				this.zoneLightOperationContainer = null;
				this.mapMusicOperationContainer = null;
				this.mapAmbienceOperationContainer = null;
				this.groupOperationContainer = null;
				this.spawnTimeOperationContainer = null;
				this.singleSpawnControllerOperationContainer = null;
				this.projectileOperationContainer = null;
				this.playerRespawnPlaceOperationContainer = null;
				this.extendedSoundOperationContainer = null;
				this.mapEditorLandscapeToolContainer = null;
				this.terrainRegionContainer = null;
				this.landscapeChangesContainer = null;
				this.mapEditorMapObjectContainer = null;
				this.mapObjectFactory = null;
				this.mapZoneContainer = null;
				this.zoneLightContainer = null;
				this.mapMusicContainer = null;
				this.mapAmbienceContainer = null;
				this.groupContainer = null;
				this.minimapContainer = null;
				this.polygonContainer = null;
				this.squareContainer = null;
				this.stripeContainer = null;
				this.operationContainer = null;
				this.data = null;
			}
		}

		// Token: 0x170002BF RID: 703
		// (get) Token: 0x06000EB1 RID: 3761 RVA: 0x00077659 File Offset: 0x00076659
		public MapObjectFactory MapObjectFactory
		{
			get
			{
				return this.mapObjectFactory;
			}
		}

		// Token: 0x170002C0 RID: 704
		// (get) Token: 0x06000EB2 RID: 3762 RVA: 0x00077661 File Offset: 0x00076661
		public MapEditorMapObjectContainer MapEditorMapObjectContainer
		{
			get
			{
				return this.mapEditorMapObjectContainer;
			}
		}

		// Token: 0x170002C1 RID: 705
		// (get) Token: 0x06000EB3 RID: 3763 RVA: 0x00077669 File Offset: 0x00076669
		public GroupContainer Groups
		{
			get
			{
				return this.groupContainer;
			}
		}

		// Token: 0x170002C2 RID: 706
		// (get) Token: 0x06000EB4 RID: 3764 RVA: 0x00077671 File Offset: 0x00076671
		public MapEditorLandscapeToolContainer MapEditorLandscapeToolContainer
		{
			get
			{
				return this.mapEditorLandscapeToolContainer;
			}
		}

		// Token: 0x170002C3 RID: 707
		// (get) Token: 0x06000EB5 RID: 3765 RVA: 0x00077679 File Offset: 0x00076679
		public TerrainRegionContainer TerrainRegionContainer
		{
			get
			{
				return this.terrainRegionContainer;
			}
		}

		// Token: 0x170002C4 RID: 708
		// (get) Token: 0x06000EB6 RID: 3766 RVA: 0x00077681 File Offset: 0x00076681
		public LandscapeChangesContainer LandscapeChangesContainer
		{
			get
			{
				return this.landscapeChangesContainer;
			}
		}

		// Token: 0x170002C5 RID: 709
		// (get) Token: 0x06000EB7 RID: 3767 RVA: 0x00077689 File Offset: 0x00076689
		public LandscapeToolOperationContainer LandscapeToolOperationContainer
		{
			get
			{
				return this.landscapeToolOperationContainer;
			}
		}

		// Token: 0x170002C6 RID: 710
		// (get) Token: 0x06000EB8 RID: 3768 RVA: 0x00077691 File Offset: 0x00076691
		public OperationContainer OperationContainer
		{
			get
			{
				return this.operationContainer;
			}
		}

		// Token: 0x170002C7 RID: 711
		// (get) Token: 0x06000EB9 RID: 3769 RVA: 0x00077699 File Offset: 0x00076699
		public MapZoneContainer MapZoneContainer
		{
			get
			{
				return this.mapZoneContainer;
			}
		}

		// Token: 0x170002C8 RID: 712
		// (get) Token: 0x06000EBA RID: 3770 RVA: 0x000776A1 File Offset: 0x000766A1
		public ZoneLightContainer ZoneLightContainer
		{
			get
			{
				return this.zoneLightContainer;
			}
		}

		// Token: 0x170002C9 RID: 713
		// (get) Token: 0x06000EBB RID: 3771 RVA: 0x000776A9 File Offset: 0x000766A9
		public MapSoundContainer MapMusicContainer
		{
			get
			{
				return this.mapMusicContainer;
			}
		}

		// Token: 0x170002CA RID: 714
		// (get) Token: 0x06000EBC RID: 3772 RVA: 0x000776B1 File Offset: 0x000766B1
		public MapSoundContainer MapAmbienceContainer
		{
			get
			{
				return this.mapAmbienceContainer;
			}
		}

		// Token: 0x170002CB RID: 715
		// (get) Token: 0x06000EBD RID: 3773 RVA: 0x000776B9 File Offset: 0x000766B9
		public MinimapContainer MinimapContainer
		{
			get
			{
				return this.minimapContainer;
			}
		}

		// Token: 0x170002CC RID: 716
		// (get) Token: 0x06000EBE RID: 3774 RVA: 0x000776C1 File Offset: 0x000766C1
		public PolygonContainer PolygonContainer
		{
			get
			{
				return this.polygonContainer;
			}
		}

		// Token: 0x170002CD RID: 717
		// (get) Token: 0x06000EBF RID: 3775 RVA: 0x000776C9 File Offset: 0x000766C9
		public AxisAlignedSquareContainer SquareContainer
		{
			get
			{
				return this.squareContainer;
			}
		}

		// Token: 0x170002CE RID: 718
		// (get) Token: 0x06000EC0 RID: 3776 RVA: 0x000776D1 File Offset: 0x000766D1
		public StripeContainer StripeContainer
		{
			get
			{
				return this.stripeContainer;
			}
		}

		// Token: 0x170002CF RID: 719
		// (get) Token: 0x06000EC1 RID: 3777 RVA: 0x000776D9 File Offset: 0x000766D9
		public Variables Data
		{
			get
			{
				return this.data;
			}
		}

		// Token: 0x170002D0 RID: 720
		// (get) Token: 0x06000EC2 RID: 3778 RVA: 0x000776E1 File Offset: 0x000766E1
		public EditingBlocker EditingBlocker
		{
			get
			{
				return this.editingBlocker;
			}
		}

		// Token: 0x04000B52 RID: 2898
		private Variables data;

		// Token: 0x04000B53 RID: 2899
		private OperationContainer operationContainer;

		// Token: 0x04000B54 RID: 2900
		private MapObjectFactory mapObjectFactory;

		// Token: 0x04000B55 RID: 2901
		private MapEditorMapObjectContainer mapEditorMapObjectContainer;

		// Token: 0x04000B56 RID: 2902
		private GroupContainer groupContainer;

		// Token: 0x04000B57 RID: 2903
		private MapEditorLandscapeToolContainer mapEditorLandscapeToolContainer;

		// Token: 0x04000B58 RID: 2904
		private TerrainRegionContainer terrainRegionContainer;

		// Token: 0x04000B59 RID: 2905
		private LandscapeChangesContainer landscapeChangesContainer;

		// Token: 0x04000B5A RID: 2906
		private MapZoneContainer mapZoneContainer;

		// Token: 0x04000B5B RID: 2907
		private ZoneLightContainer zoneLightContainer;

		// Token: 0x04000B5C RID: 2908
		private MapSoundContainer mapMusicContainer;

		// Token: 0x04000B5D RID: 2909
		private MapSoundContainer mapAmbienceContainer;

		// Token: 0x04000B5E RID: 2910
		private MinimapContainer minimapContainer;

		// Token: 0x04000B5F RID: 2911
		private PolygonContainer polygonContainer;

		// Token: 0x04000B60 RID: 2912
		private AxisAlignedSquareContainer squareContainer;

		// Token: 0x04000B61 RID: 2913
		private StripeContainer stripeContainer;

		// Token: 0x04000B62 RID: 2914
		private MapObjectOperationContainer mapObjectOperationContainer;

		// Token: 0x04000B63 RID: 2915
		private StartPointOperationContainer startPointOperationContainer;

		// Token: 0x04000B64 RID: 2916
		private RespawnOperationContainer respawnOperationContainer;

		// Token: 0x04000B65 RID: 2917
		private SpawnPointOperationContainer spawnPointOperationContainer;

		// Token: 0x04000B66 RID: 2918
		private ScriptAreaOperationContainer scriptAreaOperationContainer;

		// Token: 0x04000B67 RID: 2919
		private ZoneLocatorOperationContainer zoneLocatorOperationContainer;

		// Token: 0x04000B68 RID: 2920
		private RoutePointOperationContainer routePointOperationContainer;

		// Token: 0x04000B69 RID: 2921
		private PermanentDeviceOperationContainer permanentDeviceOperationContainer;

		// Token: 0x04000B6A RID: 2922
		private StaticObjectOperationContainer staticObjectOperationContainer;

		// Token: 0x04000B6B RID: 2923
		private MapLocatorOperationContainer mapLocatorOperationContainer;

		// Token: 0x04000B6C RID: 2924
		private PatrolNodeOperationContainer patrolNodeOperationContainer;

		// Token: 0x04000B6D RID: 2925
		private ClientSpawnPointOperationContainer clientSpawnPointOperationContainer;

		// Token: 0x04000B6E RID: 2926
		private ClientPatrolNodeOperationContainer clientPatrolNodeOperationContainer;

		// Token: 0x04000B6F RID: 2927
		private AstralBorderOperationContainer astralBorderOperationContainer;

		// Token: 0x04000B70 RID: 2928
		private SpawnTimeOperationContainer spawnTimeOperationContainer;

		// Token: 0x04000B71 RID: 2929
		private SingleSpawnControllerOperationContainer singleSpawnControllerOperationContainer;

		// Token: 0x04000B72 RID: 2930
		private ProjectileOperationContainer projectileOperationContainer;

		// Token: 0x04000B73 RID: 2931
		private PlayerRespawnPlaceOperationContainer playerRespawnPlaceOperationContainer;

		// Token: 0x04000B74 RID: 2932
		private ExtendedSoundOperationContainer extendedSoundOperationContainer;

		// Token: 0x04000B75 RID: 2933
		private TerrainRegionOperationContainer terrainRegionOperationContainer;

		// Token: 0x04000B76 RID: 2934
		private LandscapeToolOperationContainer landscapeToolOperationContainer;

		// Token: 0x04000B77 RID: 2935
		private MapZoneOperationContainer mapZoneOperationContainer;

		// Token: 0x04000B78 RID: 2936
		private ZoneLightOperationContainer zoneLightOperationContainer;

		// Token: 0x04000B79 RID: 2937
		private MapSoundOperationContainer mapMusicOperationContainer;

		// Token: 0x04000B7A RID: 2938
		private MapSoundOperationContainer mapAmbienceOperationContainer;

		// Token: 0x04000B7B RID: 2939
		private GroupOperationContainer groupOperationContainer;

		// Token: 0x04000B7C RID: 2940
		private readonly EditingBlocker editingBlocker;
	}
}
