namespace MapEditor
{
	// Token: 0x020000AD RID: 173
	public partial class MainForm : global::System.Windows.Forms.Form
	{
		// Token: 0x06000845 RID: 2117 RVA: 0x0004072B File Offset: 0x0003F72B
		protected override void Dispose(bool disposing)
		{
			if (disposing && this.components != null)
			{
				this.components.Dispose();
			}
			base.Dispose(disposing);
		}

		// Token: 0x06000846 RID: 2118 RVA: 0x0004074C File Offset: 0x0003F74C
		private void InitializeComponent()
		{
			this.components = new global::System.ComponentModel.Container();
			global::System.ComponentModel.ComponentResourceManager resources = new global::System.ComponentModel.ComponentResourceManager(typeof(global::MapEditor.MainForm));
			this.toolbarItemWorldCutSphere = new global::System.Windows.Forms.ToolStripButton();
			this.statusStrip = new global::System.Windows.Forms.StatusStrip();
			this.StatusbarMessge = new global::System.Windows.Forms.ToolStripStatusLabel();
			this.StatusbarPosition = new global::System.Windows.Forms.ToolStripStatusLabel();
			this.menuStrip = new global::System.Windows.Forms.MenuStrip();
			this.menuMap = new global::System.Windows.Forms.ToolStripMenuItem();
			this.menuItemMapOpen = new global::System.Windows.Forms.ToolStripMenuItem();
			this.menuItemMapSave = new global::System.Windows.Forms.ToolStripMenuItem();
			this.menuItemMapClose = new global::System.Windows.Forms.ToolStripMenuItem();
			this.propertiesToolStripMenuItem = new global::System.Windows.Forms.ToolStripMenuItem();
			this.menuItemMapSetArtDirectory = new global::System.Windows.Forms.ToolStripMenuItem();
			this.menuItemMapSeparator00 = new global::System.Windows.Forms.ToolStripSeparator();
			this.subMenuMapRecent = new global::System.Windows.Forms.ToolStripMenuItem();
			this.menuItemMapRecent0 = new global::System.Windows.Forms.ToolStripMenuItem();
			this.menuItemMapRecent1 = new global::System.Windows.Forms.ToolStripMenuItem();
			this.menuItemMapRecent2 = new global::System.Windows.Forms.ToolStripMenuItem();
			this.menuItemMapRecent3 = new global::System.Windows.Forms.ToolStripMenuItem();
			this.menuItemMapRecent4 = new global::System.Windows.Forms.ToolStripMenuItem();
			this.menuItemMapRecent5 = new global::System.Windows.Forms.ToolStripMenuItem();
			this.menuItemMapRecent6 = new global::System.Windows.Forms.ToolStripMenuItem();
			this.menuItemMapRecent7 = new global::System.Windows.Forms.ToolStripMenuItem();
			this.menuItemMapRecent8 = new global::System.Windows.Forms.ToolStripMenuItem();
			this.menuItemMapRecent9 = new global::System.Windows.Forms.ToolStripMenuItem();
			this.menuItemMapSeparator01 = new global::System.Windows.Forms.ToolStripSeparator();
			this.menuItemMapExit = new global::System.Windows.Forms.ToolStripMenuItem();
			this.menuEdit = new global::System.Windows.Forms.ToolStripMenuItem();
			this.menuItemEditUndo = new global::System.Windows.Forms.ToolStripMenuItem();
			this.menuItemEditRedo = new global::System.Windows.Forms.ToolStripMenuItem();
			this.menuItemEditSeparator00 = new global::System.Windows.Forms.ToolStripSeparator();
			this.menuItemEditCut = new global::System.Windows.Forms.ToolStripMenuItem();
			this.menuItemEditCopy = new global::System.Windows.Forms.ToolStripMenuItem();
			this.menuItemEditCopyAndSave = new global::System.Windows.Forms.ToolStripMenuItem();
			this.menuItemEditPaste = new global::System.Windows.Forms.ToolStripMenuItem();
			this.menuItemEditDelete = new global::System.Windows.Forms.ToolStripMenuItem();
			this.menuItemEditSeparator01 = new global::System.Windows.Forms.ToolStripSeparator();
			this.menuItemEditFind = new global::System.Windows.Forms.ToolStripMenuItem();
			this.menuItemEditSeparator02 = new global::System.Windows.Forms.ToolStripSeparator();
			this.menuItemEditLinkObjects = new global::System.Windows.Forms.ToolStripMenuItem();
			this.menuItemEditUnlinkObjects = new global::System.Windows.Forms.ToolStripMenuItem();
			this.menuItemEditSeparator03 = new global::System.Windows.Forms.ToolStripSeparator();
			this.menuItemEditResetAltitude = new global::System.Windows.Forms.ToolStripMenuItem();
			this.menuItemEditDropToNearest = new global::System.Windows.Forms.ToolStripMenuItem();
			this.menuItemEditResetRotation = new global::System.Windows.Forms.ToolStripMenuItem();
			this.menuItemEditResetScale = new global::System.Windows.Forms.ToolStripMenuItem();
			this.menuItemEditAlongNormal = new global::System.Windows.Forms.ToolStripMenuItem();
			this.menuItemEditSeparator04 = new global::System.Windows.Forms.ToolStripSeparator();
			this.menuItemEditArrangeLinkedRoutePoints = new global::System.Windows.Forms.ToolStripMenuItem();
			this.menuItemEditRearrangeLinkedRoutePoints = new global::System.Windows.Forms.ToolStripMenuItem();
			this.menuItemEditSeparator05 = new global::System.Windows.Forms.ToolStripSeparator();
			this.menuItemEditResetTerrainHeightsCache = new global::System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator1 = new global::System.Windows.Forms.ToolStripSeparator();
			this.convertSpawnPointsToClientSpawnPointsToolStripMenuItem = new global::System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator6 = new global::System.Windows.Forms.ToolStripSeparator();
			this.disassembleObjectToolStripMenuItem = new global::System.Windows.Forms.ToolStripMenuItem();
			this.formatToolStripMenuItem = new global::System.Windows.Forms.ToolStripMenuItem();
			this.menuItemPropertiesQuickProperties = new global::System.Windows.Forms.ToolStripMenuItem();
			this.menuItemPropertiesProperties = new global::System.Windows.Forms.ToolStripMenuItem();
			this.menuItemPropertiesSpecialProperties = new global::System.Windows.Forms.ToolStripMenuItem();
			this.menuItemPropertiesSpawnTunerProperties = new global::System.Windows.Forms.ToolStripMenuItem();
			this.menuItemPropertiesEnablePropertiesOnDoubleClick = new global::System.Windows.Forms.ToolStripMenuItem();
			this.menuItemPropertiesSeparator00 = new global::System.Windows.Forms.ToolStripSeparator();
			this.menuItemPropertiesFindQuests = new global::System.Windows.Forms.ToolStripMenuItem();
			this.menuItemPropertiesAddNewQuest = new global::System.Windows.Forms.ToolStripMenuItem();
			this.findQuestdForKillingToolStripMenuItem = new global::System.Windows.Forms.ToolStripMenuItem();
			this.menuItemPropertiesSeparator01 = new global::System.Windows.Forms.ToolStripSeparator();
			this.menuItemPropertiesCreateSpawnTable = new global::System.Windows.Forms.ToolStripMenuItem();
			this.menuItemPropertiesCloneSpawnTable = new global::System.Windows.Forms.ToolStripMenuItem();
			this.menuItemPropertiesCreateFixedIdleAnimationTuner = new global::System.Windows.Forms.ToolStripMenuItem();
			this.replaceStaticObjectToolStripMenuItem = new global::System.Windows.Forms.ToolStripMenuItem();
			this.menuItemPropertiesSeparator02 = new global::System.Windows.Forms.ToolStripSeparator();
			this.menuItemPropertiesOpenInDatabaseBrowser = new global::System.Windows.Forms.ToolStripMenuItem();
			this.menuItemPropertiesOpenVisualInDatabaseBrowser = new global::System.Windows.Forms.ToolStripMenuItem();
			this.menuItemPropertiesOpenInObjectEditor = new global::System.Windows.Forms.ToolStripMenuItem();
			this.menuItemPropertiesOpenInScriptEditor = new global::System.Windows.Forms.ToolStripMenuItem();
			this.menuItemPropertiesOpenInModelViewer = new global::System.Windows.Forms.ToolStripMenuItem();
			this.menuItemPropertiesOpenInModelEditor = new global::System.Windows.Forms.ToolStripMenuItem();
			this.menuItemPropertiesOpenInDialogEditor = new global::System.Windows.Forms.ToolStripMenuItem();
			this.menuItemPropertiesSeparator04 = new global::System.Windows.Forms.ToolStripSeparator();
			this.menuItemPropertiesOpenOrCreatePatrolScript = new global::System.Windows.Forms.ToolStripMenuItem();
			this.menuItemPropertiesOpenOrCreateSpawnTuner = new global::System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator4 = new global::System.Windows.Forms.ToolStripSeparator();
			this.addToObjectEditorToolStripMenuItem = new global::System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator5 = new global::System.Windows.Forms.ToolStripSeparator();
			this.markAsUnselectableToolStripMenuItem = new global::System.Windows.Forms.ToolStripMenuItem();
			this.selectorToolStripMenuItem = new global::System.Windows.Forms.ToolStripMenuItem();
			this.menuItemSelectorMove = new global::System.Windows.Forms.ToolStripMenuItem();
			this.menuItemSelectorRotate = new global::System.Windows.Forms.ToolStripMenuItem();
			this.menuItemSelectorScale = new global::System.Windows.Forms.ToolStripMenuItem();
			this.menuItemSelectorScaleObjectOriented = new global::System.Windows.Forms.ToolStripMenuItem();
			this.menuItemSelectorSeparator00 = new global::System.Windows.Forms.ToolStripSeparator();
			this.menuItemSelectorAlongNearest = new global::System.Windows.Forms.ToolStripMenuItem();
			this.menuItemSelectorAlongTerrain = new global::System.Windows.Forms.ToolStripMenuItem();
			this.menuItemSelectorAlongWater = new global::System.Windows.Forms.ToolStripMenuItem();
			this.menuItemSelectorAlignToGrid = new global::System.Windows.Forms.ToolStripMenuItem();
			this.menuItemSelectorPlaceAlongNormal = new global::System.Windows.Forms.ToolStripMenuItem();
			this.menuItemSelectorSeparator01 = new global::System.Windows.Forms.ToolStripSeparator();
			this.menuItemSelectorLockSelection = new global::System.Windows.Forms.ToolStripMenuItem();
			this.menuItemSelectorEditObjectAfterAdd = new global::System.Windows.Forms.ToolStripMenuItem();
			this.menuItemSelectorAutomaticallyLink = new global::System.Windows.Forms.ToolStripMenuItem();
			this.menuItemSelectorCreateCrosslinks = new global::System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator3 = new global::System.Windows.Forms.ToolStripSeparator();
			this.toolStripMenuItem2 = new global::System.Windows.Forms.ToolStripMenuItem();
			this.menuLayers = new global::System.Windows.Forms.ToolStripMenuItem();
			this.menuItemLayersAxisGeometry = new global::System.Windows.Forms.ToolStripMenuItem();
			this.menuItemLayersStaticObjects = new global::System.Windows.Forms.ToolStripMenuItem();
			this.menuItemLayersInteractiveObjects = new global::System.Windows.Forms.ToolStripMenuItem();
			this.menuItemLayersAllObjects = new global::System.Windows.Forms.ToolStripMenuItem();
			this.menuItemLayersSky = new global::System.Windows.Forms.ToolStripMenuItem();
			this.menuItemLayersFog = new global::System.Windows.Forms.ToolStripMenuItem();
			this.worldCutSphereToolStripMenuItem = new global::System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItem4 = new global::System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItem5 = new global::System.Windows.Forms.ToolStripMenuItem();
			this.menuItemLayersPassability = new global::System.Windows.Forms.ToolStripMenuItem();
			this.menuItemLayersZones = new global::System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItem1 = new global::System.Windows.Forms.ToolStripMenuItem();
			this.musicToolStripMenuItem = new global::System.Windows.Forms.ToolStripMenuItem();
			this.ambienceToolStripMenuItem = new global::System.Windows.Forms.ToolStripMenuItem();
			this.menuItemLayersDymanicSceneStatistic = new global::System.Windows.Forms.ToolStripMenuItem();
			this.setSceneTimeToolStripMenuItem = new global::System.Windows.Forms.ToolStripMenuItem();
			this.stopDayTimeToolStripMenuItem = new global::System.Windows.Forms.ToolStripMenuItem();
			this.menuItemLayersSeparator01 = new global::System.Windows.Forms.ToolStripSeparator();
			this.menuItemLayersTerrainGrid = new global::System.Windows.Forms.ToolStripMenuItem();
			this.menuItemLayersBottomGrid = new global::System.Windows.Forms.ToolStripMenuItem();
			this.menuItemLayersCollisionGeometry = new global::System.Windows.Forms.ToolStripMenuItem();
			this.menuItemLayersWireframe = new global::System.Windows.Forms.ToolStripMenuItem();
			this.linksToolStripMenuItem = new global::System.Windows.Forms.ToolStripMenuItem();
			this.menuItemLayersRoutePointsGeometry = new global::System.Windows.Forms.ToolStripMenuItem();
			this.menuItemLayersRouteObjects = new global::System.Windows.Forms.ToolStripMenuItem();
			this.menuItemLayersSpawnPointsGeometry = new global::System.Windows.Forms.ToolStripMenuItem();
			this.menuItemLayersScriptAreasGeometry = new global::System.Windows.Forms.ToolStripMenuItem();
			this.menuItemLayersAstralbordersGeometry = new global::System.Windows.Forms.ToolStripMenuItem();
			this.menuItemLayersMobsAggro = new global::System.Windows.Forms.ToolStripMenuItem();
			this.menuItemLayersMobsAggroRadius = new global::System.Windows.Forms.ToolStripMenuItem();
			this.menuItemLayersMobsAggroVolumes = new global::System.Windows.Forms.ToolStripMenuItem();
			this.projectileVisObjectsToolStripMenuItem = new global::System.Windows.Forms.ToolStripMenuItem();
			this.menuItemLayersZoneLocatorsGeometry = new global::System.Windows.Forms.ToolStripMenuItem();
			this.menuItemLayersSound = new global::System.Windows.Forms.ToolStripMenuItem();
			this.placeDynamicObjectsToSurfaceToolStripMenuItem = new global::System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItem3 = new global::System.Windows.Forms.ToolStripMenuItem();
			this.menuItemLayersSeparator02 = new global::System.Windows.Forms.ToolStripSeparator();
			this.menuItemLayersLargeTerrainGrid = new global::System.Windows.Forms.ToolStripMenuItem();
			this.menuItemLayersLargeBottomGrid = new global::System.Windows.Forms.ToolStripMenuItem();
			this.menuItemLayersSeparator03 = new global::System.Windows.Forms.ToolStripSeparator();
			this.menuItemLayersSetTerrainGridColor = new global::System.Windows.Forms.ToolStripMenuItem();
			this.menuItemLayersSetBottomGridColor = new global::System.Windows.Forms.ToolStripMenuItem();
			this.menuItemLayersSetBackgroundColor = new global::System.Windows.Forms.ToolStripMenuItem();
			this.menuView = new global::System.Windows.Forms.ToolStripMenuItem();
			this.menuItemViewMeasureDistance = new global::System.Windows.Forms.ToolStripMenuItem();
			this.menuItemViewSeparator00 = new global::System.Windows.Forms.ToolStripSeparator();
			this.menuItemViewGenerateShadows = new global::System.Windows.Forms.ToolStripMenuItem();
			this.menuItemViewFixTerrainTiles = new global::System.Windows.Forms.ToolStripMenuItem();
			this.menuItemViewSeparator01 = new global::System.Windows.Forms.ToolStripSeparator();
			this.menuItemViewMinimap = new global::System.Windows.Forms.ToolStripMenuItem();
			this.menuItemViewTool = new global::System.Windows.Forms.ToolStripMenuItem();
			this.menuItemViewList = new global::System.Windows.Forms.ToolStripMenuItem();
			this.menuItemViewRouteObjectBrowser = new global::System.Windows.Forms.ToolStripMenuItem();
			this.menuItemViewObjectEditor = new global::System.Windows.Forms.ToolStripMenuItem();
			this.menuItemViewMultiobjectBrowser = new global::System.Windows.Forms.ToolStripMenuItem();
			this.objectSetBrowserToolStripMenuItem = new global::System.Windows.Forms.ToolStripMenuItem();
			this.menuItemViewRooadBrowser = new global::System.Windows.Forms.ToolStripMenuItem();
			this.menuItemViewProperties = new global::System.Windows.Forms.ToolStripMenuItem();
			this.checkersWindowToolStripMenuItem = new global::System.Windows.Forms.ToolStripMenuItem();
			this.notSelectableObjectsToolStripMenuItem = new global::System.Windows.Forms.ToolStripMenuItem();
			this.hiddenObjectsToolStripMenuItem = new global::System.Windows.Forms.ToolStripMenuItem();
			this.menuItemViewSeparator02 = new global::System.Windows.Forms.ToolStripSeparator();
			this.menuItemViewMainToolbar = new global::System.Windows.Forms.ToolStripMenuItem();
			this.menuItemViewToolsToolbar = new global::System.Windows.Forms.ToolStripMenuItem();
			this.menuItemViewHelpString = new global::System.Windows.Forms.ToolStripMenuItem();
			this.menuItemViewStatusbar = new global::System.Windows.Forms.ToolStripMenuItem();
			this.menuItemViewSeparator03 = new global::System.Windows.Forms.ToolStripSeparator();
			this.menuItemViewLog = new global::System.Windows.Forms.ToolStripMenuItem();
			this.cameraToolStripMenuItem = new global::System.Windows.Forms.ToolStripMenuItem();
			this.menuItemCameraSlow = new global::System.Windows.Forms.ToolStripMenuItem();
			this.menuItemCameraNormal = new global::System.Windows.Forms.ToolStripMenuItem();
			this.menuItemCameraFast = new global::System.Windows.Forms.ToolStripMenuItem();
			this.menuItemCameraCustom = new global::System.Windows.Forms.ToolStripMenuItem();
			this.menuItemCameraSeparator00 = new global::System.Windows.Forms.ToolStripSeparator();
			this.menuItemCameraSetCustomSpeed = new global::System.Windows.Forms.ToolStripMenuItem();
			this.setCameraFovToolStripMenuItem = new global::System.Windows.Forms.ToolStripMenuItem();
			this.menuItemCameraSeparator01 = new global::System.Windows.Forms.ToolStripSeparator();
			this.menuItemCameraMove = new global::System.Windows.Forms.ToolStripMenuItem();
			this.menuItemCameraReset = new global::System.Windows.Forms.ToolStripMenuItem();
			this.menuItemCameraBoundingToolStripMenuItem = new global::System.Windows.Forms.ToolStripMenuItem();
			this.menuItemCameraSeparator02 = new global::System.Windows.Forms.ToolStripSeparator();
			this.menuItemFPSCamera = new global::System.Windows.Forms.ToolStripMenuItem();
			this.menuItemRTSCamera = new global::System.Windows.Forms.ToolStripMenuItem();
			this.menuTools = new global::System.Windows.Forms.ToolStripMenuItem();
			this.menuItemToolsGameWindow = new global::System.Windows.Forms.ToolStripMenuItem();
			this.menuItemToolsLayers = new global::System.Windows.Forms.ToolStripMenuItem();
			this.waterEditorToolStripMenuItem = new global::System.Windows.Forms.ToolStripMenuItem();
			this.menuItemToolsLight = new global::System.Windows.Forms.ToolStripMenuItem();
			this.menuItemToolsDatabaseBrowser = new global::System.Windows.Forms.ToolStripMenuItem();
			this.menuItemToolsModelViewer = new global::System.Windows.Forms.ToolStripMenuItem();
			this.menuItemToolsFullModelViewer = new global::System.Windows.Forms.ToolStripMenuItem();
			this.menuItemToolsFactionEditor = new global::System.Windows.Forms.ToolStripMenuItem();
			this.menuItemToolsScriptEditor = new global::System.Windows.Forms.ToolStripMenuItem();
			this.menuItemToolsQuests = new global::System.Windows.Forms.ToolStripMenuItem();
			this.menuItemToolsQuestEditor = new global::System.Windows.Forms.ToolStripMenuItem();
			this.menuItemToolsQuestDiagram = new global::System.Windows.Forms.ToolStripMenuItem();
			this.menuItemToolsQuestTexts = new global::System.Windows.Forms.ToolStripMenuItem();
			this.menuItemToolsQuickObjectCreation = new global::System.Windows.Forms.ToolStripMenuItem();
			this.menuItemToolsCreateMob = new global::System.Windows.Forms.ToolStripMenuItem();
			this.menuItemToolsCreareNPC = new global::System.Windows.Forms.ToolStripMenuItem();
			this.menuItemToolsCreateResource = new global::System.Windows.Forms.ToolStripMenuItem();
			this.menuItemToolsCreateQuestItem = new global::System.Windows.Forms.ToolStripMenuItem();
			this.visualMobScriptsToolStripMenuItem = new global::System.Windows.Forms.ToolStripMenuItem();
			this.hSVColorEditorToolStripMenuItem = new global::System.Windows.Forms.ToolStripMenuItem();
			this.vendorsTableToolStripMenuItem = new global::System.Windows.Forms.ToolStripMenuItem();
			this.importCuesToolStripMenuItem = new global::System.Windows.Forms.ToolStripMenuItem();
			this.menuItemToolsFillHeightsFromHeightmap = new global::System.Windows.Forms.ToolStripMenuItem();
			this.createSoundStaticObjectToolStripMenuItem = new global::System.Windows.Forms.ToolStripMenuItem();
			this.createMinimapToolStripMenuItem = new global::System.Windows.Forms.ToolStripMenuItem();
			this.autoFocusToolStripMenuItem = new global::System.Windows.Forms.ToolStripMenuItem();
			this.blockEditingToolStripMenuItem = new global::System.Windows.Forms.ToolStripMenuItem();
			this.menuHelp = new global::System.Windows.Forms.ToolStripMenuItem();
			this.menuItemHelpAbout = new global::System.Windows.Forms.ToolStripMenuItem();
			this.MainToolStrip = new global::System.Windows.Forms.ToolStrip();
			this.toolbarItemOpenMap = new global::System.Windows.Forms.ToolStripButton();
			this.toolbarItemSaveMap = new global::System.Windows.Forms.ToolStripButton();
			this.toolbarItemSeparator00 = new global::System.Windows.Forms.ToolStripSeparator();
			this.toolbarItemUndo = new global::System.Windows.Forms.ToolStripButton();
			this.toolbarItemRedo = new global::System.Windows.Forms.ToolStripButton();
			this.toolbarItemSeparator01 = new global::System.Windows.Forms.ToolStripSeparator();
			this.toolbarItemCut = new global::System.Windows.Forms.ToolStripButton();
			this.toolbarItemCopy = new global::System.Windows.Forms.ToolStripButton();
			this.toolbarItemPaste = new global::System.Windows.Forms.ToolStripButton();
			this.toolbarItemDelete = new global::System.Windows.Forms.ToolStripButton();
			this.toolbarItemSeparator02 = new global::System.Windows.Forms.ToolStripSeparator();
			this.toolbarItemLink = new global::System.Windows.Forms.ToolStripButton();
			this.toolbarItemUnlink = new global::System.Windows.Forms.ToolStripButton();
			this.toolbarItemSeparator03 = new global::System.Windows.Forms.ToolStripSeparator();
			this.toolbarItemMeasureDistance = new global::System.Windows.Forms.ToolStripButton();
			this.toolbarItemFindButton = new global::System.Windows.Forms.ToolStripButton();
			this.toolbarItemSeparator04 = new global::System.Windows.Forms.ToolStripSeparator();
			this.toolbarItemMoveWidget = new global::System.Windows.Forms.ToolStripButton();
			this.toolbarItemRotateWidget = new global::System.Windows.Forms.ToolStripButton();
			this.toolbarItemScaleWidget = new global::System.Windows.Forms.ToolStripButton();
			this.toolbarItemObjectOriented = new global::System.Windows.Forms.ToolStripButton();
			this.toolbarItemSeparator05 = new global::System.Windows.Forms.ToolStripSeparator();
			this.toolbarItemAlongTerrain = new global::System.Windows.Forms.ToolStripButton();
			this.toolbarItemAlongWater = new global::System.Windows.Forms.ToolStripButton();
			this.toolbarItemAlongNearest = new global::System.Windows.Forms.ToolStripButton();
			this.toolbarItemAlignToGrid = new global::System.Windows.Forms.ToolStripButton();
			this.toolbarItemGridStep = new global::System.Windows.Forms.ToolStripButton();
			this.toolbarItemPlaceAlongNormal = new global::System.Windows.Forms.ToolStripButton();
			this.toolbarItemSeparator06 = new global::System.Windows.Forms.ToolStripSeparator();
			this.toolbarItemLockSelection = new global::System.Windows.Forms.ToolStripButton();
			this.toolbarItemPlaceOnlyOneObject = new global::System.Windows.Forms.ToolStripButton();
			this.toolbarItemAutolinkAfterAdd = new global::System.Windows.Forms.ToolStripButton();
			this.toolbarItemCreateCrosslinks = new global::System.Windows.Forms.ToolStripButton();
			this.toolbarItemSeparator07 = new global::System.Windows.Forms.ToolStripSeparator();
			this.toolbarItemCameraSlow = new global::System.Windows.Forms.ToolStripButton();
			this.toolbarItemCameraNormal = new global::System.Windows.Forms.ToolStripButton();
			this.toolbarItemCameraFast = new global::System.Windows.Forms.ToolStripButton();
			this.toolbarItemCameraCustom = new global::System.Windows.Forms.ToolStripButton();
			this.toolbarItemSeparator08 = new global::System.Windows.Forms.ToolStripSeparator();
			this.toolbarItemCameraMove = new global::System.Windows.Forms.ToolStripButton();
			this.toolbarItemCameraReset = new global::System.Windows.Forms.ToolStripButton();
			this.toolbarItemCameraBounding = new global::System.Windows.Forms.ToolStripButton();
			this.toolbarItemSeparator09 = new global::System.Windows.Forms.ToolStripSeparator();
			this.toolbarItemCameraCustomSetSpeed = new global::System.Windows.Forms.ToolStripButton();
			this.toolbarItemCameraFOV = new global::System.Windows.Forms.ToolStripButton();
			this.toolbarItemTimeForm = new global::System.Windows.Forms.ToolStripButton();
			this.toolbarItemGenerateShadows = new global::System.Windows.Forms.ToolStripButton();
			this.toolbarItemSeparator13 = new global::System.Windows.Forms.ToolStripSeparator();
			this.toolbarItemToggleAllObjects = new global::System.Windows.Forms.ToolStripButton();
			this.toolbarItemSky = new global::System.Windows.Forms.ToolStripButton();
			this.toolbarItemFog = new global::System.Windows.Forms.ToolStripButton();
			this.toolbarItemPassability = new global::System.Windows.Forms.ToolStripButton();
			this.toolbarItemZones = new global::System.Windows.Forms.ToolStripButton();
			this.toolbarItemDynamicStatistic = new global::System.Windows.Forms.ToolStripButton();
			this.toolbarItemSeparator15 = new global::System.Windows.Forms.ToolStripSeparator();
			this.toolbarItemTerrainGrid = new global::System.Windows.Forms.ToolStripButton();
			this.toolbarItemBottomGrid = new global::System.Windows.Forms.ToolStripButton();
			this.toolbarItemCollisionGeometry = new global::System.Windows.Forms.ToolStripButton();
			this.toolbarItemWireframe = new global::System.Windows.Forms.ToolStripButton();
			this.toolbarItemLinkUserGeometry = new global::System.Windows.Forms.ToolStripButton();
			this.toolbarItemRoutePointsGeometry = new global::System.Windows.Forms.ToolStripButton();
			this.toolbarItemRouteObjects = new global::System.Windows.Forms.ToolStripButton();
			this.toolbarItemSpawnPointsGeometry = new global::System.Windows.Forms.ToolStripButton();
			this.toolbarItemScriptAreasGeometry = new global::System.Windows.Forms.ToolStripButton();
			this.toolbarItemAstralBordersGeometry = new global::System.Windows.Forms.ToolStripButton();
			this.toolbarItemZoneLocatorsGeometry = new global::System.Windows.Forms.ToolStripButton();
			this.toolbarItemAggroRadius = new global::System.Windows.Forms.ToolStripButton();
			this.toolbarItemProjectileVisObjects = new global::System.Windows.Forms.ToolStripButton();
			this.stepTimer = new global::System.Windows.Forms.Timer(this.components);
			this.saveNotificationTimer = new global::System.Windows.Forms.Timer(this.components);
			this.StatusbarHelp = new global::System.Windows.Forms.ToolStripStatusLabel();
			this.statusHelp = new global::System.Windows.Forms.StatusStrip();
			this.ToolsToolStrip = new global::System.Windows.Forms.ToolStrip();
			this.ServerConfigToolButton = new global::System.Windows.Forms.ToolStripButton();
			this.toolbarItemGame = new global::System.Windows.Forms.ToolStripButton();
			this.toolbarItemMinimap = new global::System.Windows.Forms.ToolStripButton();
			this.toolbarItemTool = new global::System.Windows.Forms.ToolStripButton();
			this.toolbarItemList = new global::System.Windows.Forms.ToolStripButton();
			this.toolbarItemRouteObjectBrowser = new global::System.Windows.Forms.ToolStripButton();
			this.toolbarItemObjectEditor = new global::System.Windows.Forms.ToolStripButton();
			this.toolbarItemMultiObjectBrowser = new global::System.Windows.Forms.ToolStripButton();
			this.toolStripButton1 = new global::System.Windows.Forms.ToolStripButton();
			this.toolbarItemRoadBrowser = new global::System.Windows.Forms.ToolStripButton();
			this.toolbarItemProperties = new global::System.Windows.Forms.ToolStripButton();
			this.toolbarItemCheckers = new global::System.Windows.Forms.ToolStripButton();
			this.toolbarItemLog = new global::System.Windows.Forms.ToolStripButton();
			this.toolbarItemSeparator10 = new global::System.Windows.Forms.ToolStripSeparator();
			this.toolbarItemLayers = new global::System.Windows.Forms.ToolStripButton();
			this.toolbarItemLight = new global::System.Windows.Forms.ToolStripButton();
			this.toolbarItemPropertyControl = new global::System.Windows.Forms.ToolStripButton();
			this.toolbarItemModelViewer = new global::System.Windows.Forms.ToolStripButton();
			this.toolbarItemScriptEditor = new global::System.Windows.Forms.ToolStripButton();
			this.toolbarItemSeparator11 = new global::System.Windows.Forms.ToolStripSeparator();
			this.toolbarItemQuests = new global::System.Windows.Forms.ToolStripButton();
			this.toolbarItemQuestDiagram = new global::System.Windows.Forms.ToolStripButton();
			this.toolbarItemQuestTexts = new global::System.Windows.Forms.ToolStripButton();
			this.toolbarItemSeparator12 = new global::System.Windows.Forms.ToolStripSeparator();
			this.toolbarItemCreateMob = new global::System.Windows.Forms.ToolStripButton();
			this.toolbarItemCreateNPC = new global::System.Windows.Forms.ToolStripButton();
			this.toolbarItemCreateResource = new global::System.Windows.Forms.ToolStripButton();
			this.toolbarItemCreateQuestItem = new global::System.Windows.Forms.ToolStripButton();
			this.toolStripSeparator2 = new global::System.Windows.Forms.ToolStripSeparator();
			this.toolStripButton2 = new global::System.Windows.Forms.ToolStripButton();
			this.toolbarItemSeparator14 = new global::System.Windows.Forms.ToolStripSeparator();
			this.toolbarItemAxisUserGeometry = new global::System.Windows.Forms.ToolStripButton();
			this.toolbarItemDynamicObjectsFall = new global::System.Windows.Forms.ToolStripButton();
			this.toolbarItemFixTerrainTiles = new global::System.Windows.Forms.ToolStripButton();
			this.toolbarItemToggleSound = new global::System.Windows.Forms.ToolStripButton();
			this.statusStrip.SuspendLayout();
			this.menuStrip.SuspendLayout();
			this.MainToolStrip.SuspendLayout();
			this.statusHelp.SuspendLayout();
			this.ToolsToolStrip.SuspendLayout();
			base.SuspendLayout();
			this.toolbarItemWorldCutSphere.AccessibleDescription = null;
			this.toolbarItemWorldCutSphere.AccessibleName = null;
			resources.ApplyResources(this.toolbarItemWorldCutSphere, "toolbarItemWorldCutSphere");
			this.toolbarItemWorldCutSphere.BackgroundImage = null;
			this.toolbarItemWorldCutSphere.DisplayStyle = global::System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.toolbarItemWorldCutSphere.Image = global::MapEditor.Properties.Resources.world_cut_sphere;
			this.toolbarItemWorldCutSphere.Name = "toolbarItemWorldCutSphere";
			this.toolbarItemWorldCutSphere.Tag = "toggle_world_cut_sphere";
			this.statusStrip.AccessibleDescription = null;
			this.statusStrip.AccessibleName = null;
			resources.ApplyResources(this.statusStrip, "statusStrip");
			this.statusStrip.BackgroundImage = null;
			this.statusStrip.Font = null;
			this.statusStrip.GripStyle = global::System.Windows.Forms.ToolStripGripStyle.Visible;
			this.statusStrip.Items.AddRange(new global::System.Windows.Forms.ToolStripItem[]
			{
				this.StatusbarMessge,
				this.StatusbarPosition
			});
			this.statusStrip.Name = "statusStrip";
			this.statusStrip.Resize += new global::System.EventHandler(this.StatusStrip_Resize);
			this.StatusbarMessge.AccessibleDescription = null;
			this.StatusbarMessge.AccessibleName = null;
			resources.ApplyResources(this.StatusbarMessge, "StatusbarMessge");
			this.StatusbarMessge.BackgroundImage = null;
			this.StatusbarMessge.BorderSides = (global::System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | global::System.Windows.Forms.ToolStripStatusLabelBorderSides.Right | global::System.Windows.Forms.ToolStripStatusLabelBorderSides.Top);
			this.StatusbarMessge.BorderStyle = global::System.Windows.Forms.Border3DStyle.SunkenOuter;
			this.StatusbarMessge.Name = "StatusbarMessge";
			this.StatusbarPosition.AccessibleDescription = null;
			this.StatusbarPosition.AccessibleName = null;
			resources.ApplyResources(this.StatusbarPosition, "StatusbarPosition");
			this.StatusbarPosition.BackgroundImage = null;
			this.StatusbarPosition.BorderSides = (global::System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | global::System.Windows.Forms.ToolStripStatusLabelBorderSides.Right | global::System.Windows.Forms.ToolStripStatusLabelBorderSides.Top);
			this.StatusbarPosition.BorderStyle = global::System.Windows.Forms.Border3DStyle.SunkenOuter;
			this.StatusbarPosition.Name = "StatusbarPosition";
			this.menuStrip.AccessibleDescription = null;
			this.menuStrip.AccessibleName = null;
			resources.ApplyResources(this.menuStrip, "menuStrip");
			this.menuStrip.BackgroundImage = null;
			this.menuStrip.Font = null;
			this.menuStrip.Items.AddRange(new global::System.Windows.Forms.ToolStripItem[]
			{
				this.menuMap,
				this.menuEdit,
				this.formatToolStripMenuItem,
				this.selectorToolStripMenuItem,
				this.menuLayers,
				this.menuView,
				this.cameraToolStripMenuItem,
				this.menuTools,
				this.menuHelp
			});
			this.menuStrip.Name = "menuStrip";
			this.menuMap.AccessibleDescription = null;
			this.menuMap.AccessibleName = null;
			resources.ApplyResources(this.menuMap, "menuMap");
			this.menuMap.BackgroundImage = null;
			this.menuMap.DropDownItems.AddRange(new global::System.Windows.Forms.ToolStripItem[]
			{
				this.menuItemMapOpen,
				this.menuItemMapSave,
				this.menuItemMapClose,
				this.propertiesToolStripMenuItem,
				this.menuItemMapSetArtDirectory,
				this.menuItemMapSeparator00,
				this.subMenuMapRecent,
				this.menuItemMapSeparator01,
				this.menuItemMapExit
			});
			this.menuMap.Name = "menuMap";
			this.menuMap.ShortcutKeyDisplayString = null;
			this.menuItemMapOpen.AccessibleDescription = null;
			this.menuItemMapOpen.AccessibleName = null;
			resources.ApplyResources(this.menuItemMapOpen, "menuItemMapOpen");
			this.menuItemMapOpen.BackgroundImage = null;
			this.menuItemMapOpen.Name = "menuItemMapOpen";
			this.menuItemMapOpen.ShortcutKeyDisplayString = null;
			this.menuItemMapOpen.Tag = "open_map";
			this.menuItemMapSave.AccessibleDescription = null;
			this.menuItemMapSave.AccessibleName = null;
			resources.ApplyResources(this.menuItemMapSave, "menuItemMapSave");
			this.menuItemMapSave.BackgroundImage = null;
			this.menuItemMapSave.Name = "menuItemMapSave";
			this.menuItemMapSave.ShortcutKeyDisplayString = null;
			this.menuItemMapSave.Tag = "save_map";
			this.menuItemMapClose.AccessibleDescription = null;
			this.menuItemMapClose.AccessibleName = null;
			resources.ApplyResources(this.menuItemMapClose, "menuItemMapClose");
			this.menuItemMapClose.BackgroundImage = null;
			this.menuItemMapClose.Name = "menuItemMapClose";
			this.menuItemMapClose.ShortcutKeyDisplayString = null;
			this.menuItemMapClose.Tag = "close_map";
			this.propertiesToolStripMenuItem.AccessibleDescription = null;
			this.propertiesToolStripMenuItem.AccessibleName = null;
			resources.ApplyResources(this.propertiesToolStripMenuItem, "propertiesToolStripMenuItem");
			this.propertiesToolStripMenuItem.BackgroundImage = null;
			this.propertiesToolStripMenuItem.Name = "propertiesToolStripMenuItem";
			this.propertiesToolStripMenuItem.ShortcutKeyDisplayString = null;
			this.propertiesToolStripMenuItem.Tag = "map_properties";
			this.menuItemMapSetArtDirectory.AccessibleDescription = null;
			this.menuItemMapSetArtDirectory.AccessibleName = null;
			resources.ApplyResources(this.menuItemMapSetArtDirectory, "menuItemMapSetArtDirectory");
			this.menuItemMapSetArtDirectory.BackgroundImage = null;
			this.menuItemMapSetArtDirectory.Name = "menuItemMapSetArtDirectory";
			this.menuItemMapSetArtDirectory.ShortcutKeyDisplayString = null;
			this.menuItemMapSetArtDirectory.Tag = "toggle_art_directory";
			this.menuItemMapSeparator00.AccessibleDescription = null;
			this.menuItemMapSeparator00.AccessibleName = null;
			resources.ApplyResources(this.menuItemMapSeparator00, "menuItemMapSeparator00");
			this.menuItemMapSeparator00.Name = "menuItemMapSeparator00";
			this.subMenuMapRecent.AccessibleDescription = null;
			this.subMenuMapRecent.AccessibleName = null;
			resources.ApplyResources(this.subMenuMapRecent, "subMenuMapRecent");
			this.subMenuMapRecent.BackgroundImage = null;
			this.subMenuMapRecent.DropDownItems.AddRange(new global::System.Windows.Forms.ToolStripItem[]
			{
				this.menuItemMapRecent0,
				this.menuItemMapRecent1,
				this.menuItemMapRecent2,
				this.menuItemMapRecent3,
				this.menuItemMapRecent4,
				this.menuItemMapRecent5,
				this.menuItemMapRecent6,
				this.menuItemMapRecent7,
				this.menuItemMapRecent8,
				this.menuItemMapRecent9
			});
			this.subMenuMapRecent.Name = "subMenuMapRecent";
			this.subMenuMapRecent.ShortcutKeyDisplayString = null;
			this.menuItemMapRecent0.AccessibleDescription = null;
			this.menuItemMapRecent0.AccessibleName = null;
			resources.ApplyResources(this.menuItemMapRecent0, "menuItemMapRecent0");
			this.menuItemMapRecent0.BackgroundImage = null;
			this.menuItemMapRecent0.Name = "menuItemMapRecent0";
			this.menuItemMapRecent0.ShortcutKeyDisplayString = null;
			this.menuItemMapRecent0.Tag = "recent_map_0";
			this.menuItemMapRecent1.AccessibleDescription = null;
			this.menuItemMapRecent1.AccessibleName = null;
			resources.ApplyResources(this.menuItemMapRecent1, "menuItemMapRecent1");
			this.menuItemMapRecent1.BackgroundImage = null;
			this.menuItemMapRecent1.Name = "menuItemMapRecent1";
			this.menuItemMapRecent1.ShortcutKeyDisplayString = null;
			this.menuItemMapRecent1.Tag = "recent_map_1";
			this.menuItemMapRecent2.AccessibleDescription = null;
			this.menuItemMapRecent2.AccessibleName = null;
			resources.ApplyResources(this.menuItemMapRecent2, "menuItemMapRecent2");
			this.menuItemMapRecent2.BackgroundImage = null;
			this.menuItemMapRecent2.Name = "menuItemMapRecent2";
			this.menuItemMapRecent2.ShortcutKeyDisplayString = null;
			this.menuItemMapRecent2.Tag = "recent_map_2";
			this.menuItemMapRecent3.AccessibleDescription = null;
			this.menuItemMapRecent3.AccessibleName = null;
			resources.ApplyResources(this.menuItemMapRecent3, "menuItemMapRecent3");
			this.menuItemMapRecent3.BackgroundImage = null;
			this.menuItemMapRecent3.Name = "menuItemMapRecent3";
			this.menuItemMapRecent3.ShortcutKeyDisplayString = null;
			this.menuItemMapRecent3.Tag = "recent_map_3";
			this.menuItemMapRecent4.AccessibleDescription = null;
			this.menuItemMapRecent4.AccessibleName = null;
			resources.ApplyResources(this.menuItemMapRecent4, "menuItemMapRecent4");
			this.menuItemMapRecent4.BackgroundImage = null;
			this.menuItemMapRecent4.Name = "menuItemMapRecent4";
			this.menuItemMapRecent4.ShortcutKeyDisplayString = null;
			this.menuItemMapRecent4.Tag = "recent_map_4";
			this.menuItemMapRecent5.AccessibleDescription = null;
			this.menuItemMapRecent5.AccessibleName = null;
			resources.ApplyResources(this.menuItemMapRecent5, "menuItemMapRecent5");
			this.menuItemMapRecent5.BackgroundImage = null;
			this.menuItemMapRecent5.Name = "menuItemMapRecent5";
			this.menuItemMapRecent5.ShortcutKeyDisplayString = null;
			this.menuItemMapRecent5.Tag = "recent_map_5";
			this.menuItemMapRecent6.AccessibleDescription = null;
			this.menuItemMapRecent6.AccessibleName = null;
			resources.ApplyResources(this.menuItemMapRecent6, "menuItemMapRecent6");
			this.menuItemMapRecent6.BackgroundImage = null;
			this.menuItemMapRecent6.Name = "menuItemMapRecent6";
			this.menuItemMapRecent6.ShortcutKeyDisplayString = null;
			this.menuItemMapRecent6.Tag = "recent_map_6";
			this.menuItemMapRecent7.AccessibleDescription = null;
			this.menuItemMapRecent7.AccessibleName = null;
			resources.ApplyResources(this.menuItemMapRecent7, "menuItemMapRecent7");
			this.menuItemMapRecent7.BackgroundImage = null;
			this.menuItemMapRecent7.Name = "menuItemMapRecent7";
			this.menuItemMapRecent7.ShortcutKeyDisplayString = null;
			this.menuItemMapRecent7.Tag = "recent_map_7";
			this.menuItemMapRecent8.AccessibleDescription = null;
			this.menuItemMapRecent8.AccessibleName = null;
			resources.ApplyResources(this.menuItemMapRecent8, "menuItemMapRecent8");
			this.menuItemMapRecent8.BackgroundImage = null;
			this.menuItemMapRecent8.Name = "menuItemMapRecent8";
			this.menuItemMapRecent8.ShortcutKeyDisplayString = null;
			this.menuItemMapRecent8.Tag = "recent_map_8";
			this.menuItemMapRecent9.AccessibleDescription = null;
			this.menuItemMapRecent9.AccessibleName = null;
			resources.ApplyResources(this.menuItemMapRecent9, "menuItemMapRecent9");
			this.menuItemMapRecent9.BackgroundImage = null;
			this.menuItemMapRecent9.Name = "menuItemMapRecent9";
			this.menuItemMapRecent9.ShortcutKeyDisplayString = null;
			this.menuItemMapRecent9.Tag = "recent_map_9";
			this.menuItemMapSeparator01.AccessibleDescription = null;
			this.menuItemMapSeparator01.AccessibleName = null;
			resources.ApplyResources(this.menuItemMapSeparator01, "menuItemMapSeparator01");
			this.menuItemMapSeparator01.Name = "menuItemMapSeparator01";
			this.menuItemMapExit.AccessibleDescription = null;
			this.menuItemMapExit.AccessibleName = null;
			resources.ApplyResources(this.menuItemMapExit, "menuItemMapExit");
			this.menuItemMapExit.BackgroundImage = null;
			this.menuItemMapExit.Name = "menuItemMapExit";
			this.menuItemMapExit.ShortcutKeyDisplayString = null;
			this.menuItemMapExit.Tag = "exit";
			this.menuEdit.AccessibleDescription = null;
			this.menuEdit.AccessibleName = null;
			resources.ApplyResources(this.menuEdit, "menuEdit");
			this.menuEdit.BackgroundImage = null;
			this.menuEdit.DropDownItems.AddRange(new global::System.Windows.Forms.ToolStripItem[]
			{
				this.menuItemEditUndo,
				this.menuItemEditRedo,
				this.menuItemEditSeparator00,
				this.menuItemEditCut,
				this.menuItemEditCopy,
				this.menuItemEditCopyAndSave,
				this.menuItemEditPaste,
				this.menuItemEditDelete,
				this.menuItemEditSeparator01,
				this.menuItemEditFind,
				this.menuItemEditSeparator02,
				this.menuItemEditLinkObjects,
				this.menuItemEditUnlinkObjects,
				this.menuItemEditSeparator03,
				this.menuItemEditResetAltitude,
				this.menuItemEditDropToNearest,
				this.menuItemEditResetRotation,
				this.menuItemEditResetScale,
				this.menuItemEditAlongNormal,
				this.menuItemEditSeparator04,
				this.menuItemEditArrangeLinkedRoutePoints,
				this.menuItemEditRearrangeLinkedRoutePoints,
				this.menuItemEditSeparator05,
				this.menuItemEditResetTerrainHeightsCache,
				this.toolStripSeparator1,
				this.convertSpawnPointsToClientSpawnPointsToolStripMenuItem,
				this.toolStripSeparator6,
				this.disassembleObjectToolStripMenuItem
			});
			this.menuEdit.Name = "menuEdit";
			this.menuEdit.ShortcutKeyDisplayString = null;
			this.menuItemEditUndo.AccessibleDescription = null;
			this.menuItemEditUndo.AccessibleName = null;
			resources.ApplyResources(this.menuItemEditUndo, "menuItemEditUndo");
			this.menuItemEditUndo.BackgroundImage = null;
			this.menuItemEditUndo.Name = "menuItemEditUndo";
			this.menuItemEditUndo.ShortcutKeyDisplayString = null;
			this.menuItemEditUndo.Tag = "undo";
			this.menuItemEditRedo.AccessibleDescription = null;
			this.menuItemEditRedo.AccessibleName = null;
			resources.ApplyResources(this.menuItemEditRedo, "menuItemEditRedo");
			this.menuItemEditRedo.BackgroundImage = null;
			this.menuItemEditRedo.Name = "menuItemEditRedo";
			this.menuItemEditRedo.ShortcutKeyDisplayString = null;
			this.menuItemEditRedo.Tag = "redo";
			this.menuItemEditSeparator00.AccessibleDescription = null;
			this.menuItemEditSeparator00.AccessibleName = null;
			resources.ApplyResources(this.menuItemEditSeparator00, "menuItemEditSeparator00");
			this.menuItemEditSeparator00.Name = "menuItemEditSeparator00";
			this.menuItemEditCut.AccessibleDescription = null;
			this.menuItemEditCut.AccessibleName = null;
			resources.ApplyResources(this.menuItemEditCut, "menuItemEditCut");
			this.menuItemEditCut.BackgroundImage = null;
			this.menuItemEditCut.Name = "menuItemEditCut";
			this.menuItemEditCut.ShortcutKeyDisplayString = null;
			this.menuItemEditCut.Tag = "cut";
			this.menuItemEditCopy.AccessibleDescription = null;
			this.menuItemEditCopy.AccessibleName = null;
			resources.ApplyResources(this.menuItemEditCopy, "menuItemEditCopy");
			this.menuItemEditCopy.BackgroundImage = null;
			this.menuItemEditCopy.Name = "menuItemEditCopy";
			this.menuItemEditCopy.ShortcutKeyDisplayString = null;
			this.menuItemEditCopy.Tag = "copy";
			this.menuItemEditCopyAndSave.AccessibleDescription = null;
			this.menuItemEditCopyAndSave.AccessibleName = null;
			resources.ApplyResources(this.menuItemEditCopyAndSave, "menuItemEditCopyAndSave");
			this.menuItemEditCopyAndSave.BackgroundImage = null;
			this.menuItemEditCopyAndSave.Name = "menuItemEditCopyAndSave";
			this.menuItemEditCopyAndSave.ShortcutKeyDisplayString = null;
			this.menuItemEditCopyAndSave.Tag = "copy_special";
			this.menuItemEditPaste.AccessibleDescription = null;
			this.menuItemEditPaste.AccessibleName = null;
			resources.ApplyResources(this.menuItemEditPaste, "menuItemEditPaste");
			this.menuItemEditPaste.BackgroundImage = null;
			this.menuItemEditPaste.Name = "menuItemEditPaste";
			this.menuItemEditPaste.ShortcutKeyDisplayString = null;
			this.menuItemEditPaste.Tag = "paste";
			this.menuItemEditDelete.AccessibleDescription = null;
			this.menuItemEditDelete.AccessibleName = null;
			resources.ApplyResources(this.menuItemEditDelete, "menuItemEditDelete");
			this.menuItemEditDelete.BackgroundImage = null;
			this.menuItemEditDelete.Name = "menuItemEditDelete";
			this.menuItemEditDelete.ShortcutKeyDisplayString = null;
			this.menuItemEditDelete.Tag = "delete";
			this.menuItemEditSeparator01.AccessibleDescription = null;
			this.menuItemEditSeparator01.AccessibleName = null;
			resources.ApplyResources(this.menuItemEditSeparator01, "menuItemEditSeparator01");
			this.menuItemEditSeparator01.Name = "menuItemEditSeparator01";
			this.menuItemEditFind.AccessibleDescription = null;
			this.menuItemEditFind.AccessibleName = null;
			resources.ApplyResources(this.menuItemEditFind, "menuItemEditFind");
			this.menuItemEditFind.BackgroundImage = null;
			this.menuItemEditFind.Name = "menuItemEditFind";
			this.menuItemEditFind.ShortcutKeyDisplayString = null;
			this.menuItemEditFind.Tag = "find";
			this.menuItemEditSeparator02.AccessibleDescription = null;
			this.menuItemEditSeparator02.AccessibleName = null;
			resources.ApplyResources(this.menuItemEditSeparator02, "menuItemEditSeparator02");
			this.menuItemEditSeparator02.Name = "menuItemEditSeparator02";
			this.menuItemEditLinkObjects.AccessibleDescription = null;
			this.menuItemEditLinkObjects.AccessibleName = null;
			resources.ApplyResources(this.menuItemEditLinkObjects, "menuItemEditLinkObjects");
			this.menuItemEditLinkObjects.BackgroundImage = null;
			this.menuItemEditLinkObjects.Name = "menuItemEditLinkObjects";
			this.menuItemEditLinkObjects.ShortcutKeyDisplayString = null;
			this.menuItemEditLinkObjects.Tag = "link";
			this.menuItemEditUnlinkObjects.AccessibleDescription = null;
			this.menuItemEditUnlinkObjects.AccessibleName = null;
			resources.ApplyResources(this.menuItemEditUnlinkObjects, "menuItemEditUnlinkObjects");
			this.menuItemEditUnlinkObjects.BackgroundImage = null;
			this.menuItemEditUnlinkObjects.Name = "menuItemEditUnlinkObjects";
			this.menuItemEditUnlinkObjects.ShortcutKeyDisplayString = null;
			this.menuItemEditUnlinkObjects.Tag = "unlink";
			this.menuItemEditSeparator03.AccessibleDescription = null;
			this.menuItemEditSeparator03.AccessibleName = null;
			resources.ApplyResources(this.menuItemEditSeparator03, "menuItemEditSeparator03");
			this.menuItemEditSeparator03.Name = "menuItemEditSeparator03";
			this.menuItemEditResetAltitude.AccessibleDescription = null;
			this.menuItemEditResetAltitude.AccessibleName = null;
			resources.ApplyResources(this.menuItemEditResetAltitude, "menuItemEditResetAltitude");
			this.menuItemEditResetAltitude.BackgroundImage = null;
			this.menuItemEditResetAltitude.Name = "menuItemEditResetAltitude";
			this.menuItemEditResetAltitude.ShortcutKeyDisplayString = null;
			this.menuItemEditResetAltitude.Tag = "altitude_reset";
			this.menuItemEditDropToNearest.AccessibleDescription = null;
			this.menuItemEditDropToNearest.AccessibleName = null;
			resources.ApplyResources(this.menuItemEditDropToNearest, "menuItemEditDropToNearest");
			this.menuItemEditDropToNearest.BackgroundImage = null;
			this.menuItemEditDropToNearest.Name = "menuItemEditDropToNearest";
			this.menuItemEditDropToNearest.ShortcutKeyDisplayString = null;
			this.menuItemEditDropToNearest.Tag = "drop_to_nearest";
			this.menuItemEditResetRotation.AccessibleDescription = null;
			this.menuItemEditResetRotation.AccessibleName = null;
			resources.ApplyResources(this.menuItemEditResetRotation, "menuItemEditResetRotation");
			this.menuItemEditResetRotation.BackgroundImage = null;
			this.menuItemEditResetRotation.Name = "menuItemEditResetRotation";
			this.menuItemEditResetRotation.ShortcutKeyDisplayString = null;
			this.menuItemEditResetRotation.Tag = "rotation_reset";
			this.menuItemEditResetScale.AccessibleDescription = null;
			this.menuItemEditResetScale.AccessibleName = null;
			resources.ApplyResources(this.menuItemEditResetScale, "menuItemEditResetScale");
			this.menuItemEditResetScale.BackgroundImage = null;
			this.menuItemEditResetScale.Name = "menuItemEditResetScale";
			this.menuItemEditResetScale.ShortcutKeyDisplayString = null;
			this.menuItemEditResetScale.Tag = "scale_reset";
			this.menuItemEditAlongNormal.AccessibleDescription = null;
			this.menuItemEditAlongNormal.AccessibleName = null;
			resources.ApplyResources(this.menuItemEditAlongNormal, "menuItemEditAlongNormal");
			this.menuItemEditAlongNormal.BackgroundImage = null;
			this.menuItemEditAlongNormal.Name = "menuItemEditAlongNormal";
			this.menuItemEditAlongNormal.ShortcutKeyDisplayString = null;
			this.menuItemEditAlongNormal.Tag = "rotation_along_normal";
			this.menuItemEditSeparator04.AccessibleDescription = null;
			this.menuItemEditSeparator04.AccessibleName = null;
			resources.ApplyResources(this.menuItemEditSeparator04, "menuItemEditSeparator04");
			this.menuItemEditSeparator04.Name = "menuItemEditSeparator04";
			this.menuItemEditArrangeLinkedRoutePoints.AccessibleDescription = null;
			this.menuItemEditArrangeLinkedRoutePoints.AccessibleName = null;
			resources.ApplyResources(this.menuItemEditArrangeLinkedRoutePoints, "menuItemEditArrangeLinkedRoutePoints");
			this.menuItemEditArrangeLinkedRoutePoints.BackgroundImage = null;
			this.menuItemEditArrangeLinkedRoutePoints.Name = "menuItemEditArrangeLinkedRoutePoints";
			this.menuItemEditArrangeLinkedRoutePoints.ShortcutKeyDisplayString = null;
			this.menuItemEditArrangeLinkedRoutePoints.Tag = "arrange_linked_items";
			this.menuItemEditRearrangeLinkedRoutePoints.AccessibleDescription = null;
			this.menuItemEditRearrangeLinkedRoutePoints.AccessibleName = null;
			resources.ApplyResources(this.menuItemEditRearrangeLinkedRoutePoints, "menuItemEditRearrangeLinkedRoutePoints");
			this.menuItemEditRearrangeLinkedRoutePoints.BackgroundImage = null;
			this.menuItemEditRearrangeLinkedRoutePoints.Name = "menuItemEditRearrangeLinkedRoutePoints";
			this.menuItemEditRearrangeLinkedRoutePoints.ShortcutKeyDisplayString = null;
			this.menuItemEditRearrangeLinkedRoutePoints.Tag = "rearrange_linked_items";
			this.menuItemEditSeparator05.AccessibleDescription = null;
			this.menuItemEditSeparator05.AccessibleName = null;
			resources.ApplyResources(this.menuItemEditSeparator05, "menuItemEditSeparator05");
			this.menuItemEditSeparator05.Name = "menuItemEditSeparator05";
			this.menuItemEditResetTerrainHeightsCache.AccessibleDescription = null;
			this.menuItemEditResetTerrainHeightsCache.AccessibleName = null;
			resources.ApplyResources(this.menuItemEditResetTerrainHeightsCache, "menuItemEditResetTerrainHeightsCache");
			this.menuItemEditResetTerrainHeightsCache.BackgroundImage = null;
			this.menuItemEditResetTerrainHeightsCache.Name = "menuItemEditResetTerrainHeightsCache";
			this.menuItemEditResetTerrainHeightsCache.ShortcutKeyDisplayString = null;
			this.menuItemEditResetTerrainHeightsCache.Tag = "clear_lti_data";
			this.toolStripSeparator1.AccessibleDescription = null;
			this.toolStripSeparator1.AccessibleName = null;
			resources.ApplyResources(this.toolStripSeparator1, "toolStripSeparator1");
			this.toolStripSeparator1.Name = "toolStripSeparator1";
			this.convertSpawnPointsToClientSpawnPointsToolStripMenuItem.AccessibleDescription = null;
			this.convertSpawnPointsToClientSpawnPointsToolStripMenuItem.AccessibleName = null;
			resources.ApplyResources(this.convertSpawnPointsToClientSpawnPointsToolStripMenuItem, "convertSpawnPointsToClientSpawnPointsToolStripMenuItem");
			this.convertSpawnPointsToClientSpawnPointsToolStripMenuItem.BackgroundImage = null;
			this.convertSpawnPointsToClientSpawnPointsToolStripMenuItem.Name = "convertSpawnPointsToClientSpawnPointsToolStripMenuItem";
			this.convertSpawnPointsToClientSpawnPointsToolStripMenuItem.ShortcutKeyDisplayString = null;
			this.convertSpawnPointsToClientSpawnPointsToolStripMenuItem.Tag = "convert_sp_to_client_sp";
			this.toolStripSeparator6.AccessibleDescription = null;
			this.toolStripSeparator6.AccessibleName = null;
			resources.ApplyResources(this.toolStripSeparator6, "toolStripSeparator6");
			this.toolStripSeparator6.Name = "toolStripSeparator6";
			this.disassembleObjectToolStripMenuItem.AccessibleDescription = null;
			this.disassembleObjectToolStripMenuItem.AccessibleName = null;
			resources.ApplyResources(this.disassembleObjectToolStripMenuItem, "disassembleObjectToolStripMenuItem");
			this.disassembleObjectToolStripMenuItem.BackgroundImage = null;
			this.disassembleObjectToolStripMenuItem.Name = "disassembleObjectToolStripMenuItem";
			this.disassembleObjectToolStripMenuItem.ShortcutKeyDisplayString = null;
			this.disassembleObjectToolStripMenuItem.Tag = "toggle_disassemble_object";
			this.formatToolStripMenuItem.AccessibleDescription = null;
			this.formatToolStripMenuItem.AccessibleName = null;
			resources.ApplyResources(this.formatToolStripMenuItem, "formatToolStripMenuItem");
			this.formatToolStripMenuItem.BackgroundImage = null;
			this.formatToolStripMenuItem.DropDownItems.AddRange(new global::System.Windows.Forms.ToolStripItem[]
			{
				this.menuItemPropertiesQuickProperties,
				this.menuItemPropertiesProperties,
				this.menuItemPropertiesSpecialProperties,
				this.menuItemPropertiesSpawnTunerProperties,
				this.menuItemPropertiesEnablePropertiesOnDoubleClick,
				this.menuItemPropertiesSeparator00,
				this.menuItemPropertiesFindQuests,
				this.menuItemPropertiesAddNewQuest,
				this.findQuestdForKillingToolStripMenuItem,
				this.menuItemPropertiesSeparator01,
				this.menuItemPropertiesCreateSpawnTable,
				this.menuItemPropertiesCloneSpawnTable,
				this.menuItemPropertiesCreateFixedIdleAnimationTuner,
				this.replaceStaticObjectToolStripMenuItem,
				this.menuItemPropertiesSeparator02,
				this.menuItemPropertiesOpenInDatabaseBrowser,
				this.menuItemPropertiesOpenVisualInDatabaseBrowser,
				this.menuItemPropertiesOpenInObjectEditor,
				this.menuItemPropertiesOpenInScriptEditor,
				this.menuItemPropertiesOpenInModelViewer,
				this.menuItemPropertiesOpenInModelEditor,
				this.menuItemPropertiesOpenInDialogEditor,
				this.menuItemPropertiesSeparator04,
				this.menuItemPropertiesOpenOrCreatePatrolScript,
				this.menuItemPropertiesOpenOrCreateSpawnTuner,
				this.toolStripSeparator4,
				this.addToObjectEditorToolStripMenuItem,
				this.toolStripSeparator5,
				this.markAsUnselectableToolStripMenuItem
			});
			this.formatToolStripMenuItem.Name = "formatToolStripMenuItem";
			this.formatToolStripMenuItem.ShortcutKeyDisplayString = null;
			this.menuItemPropertiesQuickProperties.AccessibleDescription = null;
			this.menuItemPropertiesQuickProperties.AccessibleName = null;
			resources.ApplyResources(this.menuItemPropertiesQuickProperties, "menuItemPropertiesQuickProperties");
			this.menuItemPropertiesQuickProperties.BackgroundImage = null;
			this.menuItemPropertiesQuickProperties.Name = "menuItemPropertiesQuickProperties";
			this.menuItemPropertiesQuickProperties.ShortcutKeyDisplayString = null;
			this.menuItemPropertiesQuickProperties.Tag = "quick_properties";
			this.menuItemPropertiesProperties.AccessibleDescription = null;
			this.menuItemPropertiesProperties.AccessibleName = null;
			resources.ApplyResources(this.menuItemPropertiesProperties, "menuItemPropertiesProperties");
			this.menuItemPropertiesProperties.BackgroundImage = null;
			this.menuItemPropertiesProperties.Name = "menuItemPropertiesProperties";
			this.menuItemPropertiesProperties.ShortcutKeyDisplayString = null;
			this.menuItemPropertiesProperties.Tag = "properties";
			this.menuItemPropertiesSpecialProperties.AccessibleDescription = null;
			this.menuItemPropertiesSpecialProperties.AccessibleName = null;
			resources.ApplyResources(this.menuItemPropertiesSpecialProperties, "menuItemPropertiesSpecialProperties");
			this.menuItemPropertiesSpecialProperties.BackgroundImage = null;
			this.menuItemPropertiesSpecialProperties.Name = "menuItemPropertiesSpecialProperties";
			this.menuItemPropertiesSpecialProperties.ShortcutKeyDisplayString = null;
			this.menuItemPropertiesSpecialProperties.Tag = "special_properties";
			this.menuItemPropertiesSpawnTunerProperties.AccessibleDescription = null;
			this.menuItemPropertiesSpawnTunerProperties.AccessibleName = null;
			resources.ApplyResources(this.menuItemPropertiesSpawnTunerProperties, "menuItemPropertiesSpawnTunerProperties");
			this.menuItemPropertiesSpawnTunerProperties.BackgroundImage = null;
			this.menuItemPropertiesSpawnTunerProperties.Name = "menuItemPropertiesSpawnTunerProperties";
			this.menuItemPropertiesSpawnTunerProperties.ShortcutKeyDisplayString = null;
			this.menuItemPropertiesSpawnTunerProperties.Tag = "toggle_spawn_tuner";
			this.menuItemPropertiesEnablePropertiesOnDoubleClick.AccessibleDescription = null;
			this.menuItemPropertiesEnablePropertiesOnDoubleClick.AccessibleName = null;
			resources.ApplyResources(this.menuItemPropertiesEnablePropertiesOnDoubleClick, "menuItemPropertiesEnablePropertiesOnDoubleClick");
			this.menuItemPropertiesEnablePropertiesOnDoubleClick.BackgroundImage = null;
			this.menuItemPropertiesEnablePropertiesOnDoubleClick.Name = "menuItemPropertiesEnablePropertiesOnDoubleClick";
			this.menuItemPropertiesEnablePropertiesOnDoubleClick.ShortcutKeyDisplayString = null;
			this.menuItemPropertiesEnablePropertiesOnDoubleClick.Tag = "toggle_double_click_properties";
			this.menuItemPropertiesSeparator00.AccessibleDescription = null;
			this.menuItemPropertiesSeparator00.AccessibleName = null;
			resources.ApplyResources(this.menuItemPropertiesSeparator00, "menuItemPropertiesSeparator00");
			this.menuItemPropertiesSeparator00.Name = "menuItemPropertiesSeparator00";
			this.menuItemPropertiesFindQuests.AccessibleDescription = null;
			this.menuItemPropertiesFindQuests.AccessibleName = null;
			resources.ApplyResources(this.menuItemPropertiesFindQuests, "menuItemPropertiesFindQuests");
			this.menuItemPropertiesFindQuests.BackgroundImage = null;
			this.menuItemPropertiesFindQuests.Name = "menuItemPropertiesFindQuests";
			this.menuItemPropertiesFindQuests.ShortcutKeyDisplayString = null;
			this.menuItemPropertiesFindQuests.Tag = "load_quest_list_by_object";
			this.menuItemPropertiesAddNewQuest.AccessibleDescription = null;
			this.menuItemPropertiesAddNewQuest.AccessibleName = null;
			resources.ApplyResources(this.menuItemPropertiesAddNewQuest, "menuItemPropertiesAddNewQuest");
			this.menuItemPropertiesAddNewQuest.BackgroundImage = null;
			this.menuItemPropertiesAddNewQuest.Name = "menuItemPropertiesAddNewQuest";
			this.menuItemPropertiesAddNewQuest.ShortcutKeyDisplayString = null;
			this.menuItemPropertiesAddNewQuest.Tag = "add_quest_to_object";
			this.findQuestdForKillingToolStripMenuItem.AccessibleDescription = null;
			this.findQuestdForKillingToolStripMenuItem.AccessibleName = null;
			resources.ApplyResources(this.findQuestdForKillingToolStripMenuItem, "findQuestdForKillingToolStripMenuItem");
			this.findQuestdForKillingToolStripMenuItem.BackgroundImage = null;
			this.findQuestdForKillingToolStripMenuItem.Name = "findQuestdForKillingToolStripMenuItem";
			this.findQuestdForKillingToolStripMenuItem.ShortcutKeyDisplayString = null;
			this.findQuestdForKillingToolStripMenuItem.Tag = "load_quest_for_killing";
			this.menuItemPropertiesSeparator01.AccessibleDescription = null;
			this.menuItemPropertiesSeparator01.AccessibleName = null;
			resources.ApplyResources(this.menuItemPropertiesSeparator01, "menuItemPropertiesSeparator01");
			this.menuItemPropertiesSeparator01.Name = "menuItemPropertiesSeparator01";
			this.menuItemPropertiesCreateSpawnTable.AccessibleDescription = null;
			this.menuItemPropertiesCreateSpawnTable.AccessibleName = null;
			resources.ApplyResources(this.menuItemPropertiesCreateSpawnTable, "menuItemPropertiesCreateSpawnTable");
			this.menuItemPropertiesCreateSpawnTable.BackgroundImage = null;
			this.menuItemPropertiesCreateSpawnTable.Name = "menuItemPropertiesCreateSpawnTable";
			this.menuItemPropertiesCreateSpawnTable.ShortcutKeyDisplayString = null;
			this.menuItemPropertiesCreateSpawnTable.Tag = "create_spawn_table";
			this.menuItemPropertiesCloneSpawnTable.AccessibleDescription = null;
			this.menuItemPropertiesCloneSpawnTable.AccessibleName = null;
			resources.ApplyResources(this.menuItemPropertiesCloneSpawnTable, "menuItemPropertiesCloneSpawnTable");
			this.menuItemPropertiesCloneSpawnTable.BackgroundImage = null;
			this.menuItemPropertiesCloneSpawnTable.Name = "menuItemPropertiesCloneSpawnTable";
			this.menuItemPropertiesCloneSpawnTable.ShortcutKeyDisplayString = null;
			this.menuItemPropertiesCloneSpawnTable.Tag = "clone_spawn_table";
			this.menuItemPropertiesCreateFixedIdleAnimationTuner.AccessibleDescription = null;
			this.menuItemPropertiesCreateFixedIdleAnimationTuner.AccessibleName = null;
			resources.ApplyResources(this.menuItemPropertiesCreateFixedIdleAnimationTuner, "menuItemPropertiesCreateFixedIdleAnimationTuner");
			this.menuItemPropertiesCreateFixedIdleAnimationTuner.BackgroundImage = null;
			this.menuItemPropertiesCreateFixedIdleAnimationTuner.Name = "menuItemPropertiesCreateFixedIdleAnimationTuner";
			this.menuItemPropertiesCreateFixedIdleAnimationTuner.ShortcutKeyDisplayString = null;
			this.menuItemPropertiesCreateFixedIdleAnimationTuner.Tag = "add_fixed_idle_animation_tuner";
			this.replaceStaticObjectToolStripMenuItem.AccessibleDescription = null;
			this.replaceStaticObjectToolStripMenuItem.AccessibleName = null;
			resources.ApplyResources(this.replaceStaticObjectToolStripMenuItem, "replaceStaticObjectToolStripMenuItem");
			this.replaceStaticObjectToolStripMenuItem.BackgroundImage = null;
			this.replaceStaticObjectToolStripMenuItem.Name = "replaceStaticObjectToolStripMenuItem";
			this.replaceStaticObjectToolStripMenuItem.ShortcutKeyDisplayString = null;
			this.replaceStaticObjectToolStripMenuItem.Tag = "toggle_replace_static_object";
			this.menuItemPropertiesSeparator02.AccessibleDescription = null;
			this.menuItemPropertiesSeparator02.AccessibleName = null;
			resources.ApplyResources(this.menuItemPropertiesSeparator02, "menuItemPropertiesSeparator02");
			this.menuItemPropertiesSeparator02.Name = "menuItemPropertiesSeparator02";
			this.menuItemPropertiesOpenInDatabaseBrowser.AccessibleDescription = null;
			this.menuItemPropertiesOpenInDatabaseBrowser.AccessibleName = null;
			resources.ApplyResources(this.menuItemPropertiesOpenInDatabaseBrowser, "menuItemPropertiesOpenInDatabaseBrowser");
			this.menuItemPropertiesOpenInDatabaseBrowser.BackgroundImage = null;
			this.menuItemPropertiesOpenInDatabaseBrowser.Name = "menuItemPropertiesOpenInDatabaseBrowser";
			this.menuItemPropertiesOpenInDatabaseBrowser.ShortcutKeyDisplayString = null;
			this.menuItemPropertiesOpenInDatabaseBrowser.Tag = "stats_properties";
			this.menuItemPropertiesOpenVisualInDatabaseBrowser.AccessibleDescription = null;
			this.menuItemPropertiesOpenVisualInDatabaseBrowser.AccessibleName = null;
			resources.ApplyResources(this.menuItemPropertiesOpenVisualInDatabaseBrowser, "menuItemPropertiesOpenVisualInDatabaseBrowser");
			this.menuItemPropertiesOpenVisualInDatabaseBrowser.BackgroundImage = null;
			this.menuItemPropertiesOpenVisualInDatabaseBrowser.Name = "menuItemPropertiesOpenVisualInDatabaseBrowser";
			this.menuItemPropertiesOpenVisualInDatabaseBrowser.ShortcutKeyDisplayString = null;
			this.menuItemPropertiesOpenVisualInDatabaseBrowser.Tag = "stats_special_properties";
			this.menuItemPropertiesOpenInObjectEditor.AccessibleDescription = null;
			this.menuItemPropertiesOpenInObjectEditor.AccessibleName = null;
			resources.ApplyResources(this.menuItemPropertiesOpenInObjectEditor, "menuItemPropertiesOpenInObjectEditor");
			this.menuItemPropertiesOpenInObjectEditor.BackgroundImage = null;
			this.menuItemPropertiesOpenInObjectEditor.Name = "menuItemPropertiesOpenInObjectEditor";
			this.menuItemPropertiesOpenInObjectEditor.ShortcutKeyDisplayString = null;
			this.menuItemPropertiesOpenInObjectEditor.Tag = "open_object_in_object_editor";
			this.menuItemPropertiesOpenInScriptEditor.AccessibleDescription = null;
			this.menuItemPropertiesOpenInScriptEditor.AccessibleName = null;
			resources.ApplyResources(this.menuItemPropertiesOpenInScriptEditor, "menuItemPropertiesOpenInScriptEditor");
			this.menuItemPropertiesOpenInScriptEditor.BackgroundImage = null;
			this.menuItemPropertiesOpenInScriptEditor.Name = "menuItemPropertiesOpenInScriptEditor";
			this.menuItemPropertiesOpenInScriptEditor.ShortcutKeyDisplayString = null;
			this.menuItemPropertiesOpenInScriptEditor.Tag = "load_script_to_scipt_editor";
			this.menuItemPropertiesOpenInModelViewer.AccessibleDescription = null;
			this.menuItemPropertiesOpenInModelViewer.AccessibleName = null;
			resources.ApplyResources(this.menuItemPropertiesOpenInModelViewer, "menuItemPropertiesOpenInModelViewer");
			this.menuItemPropertiesOpenInModelViewer.BackgroundImage = null;
			this.menuItemPropertiesOpenInModelViewer.Name = "menuItemPropertiesOpenInModelViewer";
			this.menuItemPropertiesOpenInModelViewer.ShortcutKeyDisplayString = null;
			this.menuItemPropertiesOpenInModelViewer.Tag = "load_model_to_model_viewer";
			this.menuItemPropertiesOpenInModelEditor.AccessibleDescription = null;
			this.menuItemPropertiesOpenInModelEditor.AccessibleName = null;
			resources.ApplyResources(this.menuItemPropertiesOpenInModelEditor, "menuItemPropertiesOpenInModelEditor");
			this.menuItemPropertiesOpenInModelEditor.BackgroundImage = null;
			this.menuItemPropertiesOpenInModelEditor.Name = "menuItemPropertiesOpenInModelEditor";
			this.menuItemPropertiesOpenInModelEditor.ShortcutKeyDisplayString = null;
			this.menuItemPropertiesOpenInModelEditor.Tag = "load_model_to_model_editor";
			this.menuItemPropertiesOpenInDialogEditor.AccessibleDescription = null;
			this.menuItemPropertiesOpenInDialogEditor.AccessibleName = null;
			resources.ApplyResources(this.menuItemPropertiesOpenInDialogEditor, "menuItemPropertiesOpenInDialogEditor");
			this.menuItemPropertiesOpenInDialogEditor.BackgroundImage = null;
			this.menuItemPropertiesOpenInDialogEditor.Name = "menuItemPropertiesOpenInDialogEditor";
			this.menuItemPropertiesOpenInDialogEditor.ShortcutKeyDisplayString = null;
			this.menuItemPropertiesOpenInDialogEditor.Tag = "load_dialog_editor";
			this.menuItemPropertiesSeparator04.AccessibleDescription = null;
			this.menuItemPropertiesSeparator04.AccessibleName = null;
			resources.ApplyResources(this.menuItemPropertiesSeparator04, "menuItemPropertiesSeparator04");
			this.menuItemPropertiesSeparator04.Name = "menuItemPropertiesSeparator04";
			this.menuItemPropertiesOpenOrCreatePatrolScript.AccessibleDescription = null;
			this.menuItemPropertiesOpenOrCreatePatrolScript.AccessibleName = null;
			resources.ApplyResources(this.menuItemPropertiesOpenOrCreatePatrolScript, "menuItemPropertiesOpenOrCreatePatrolScript");
			this.menuItemPropertiesOpenOrCreatePatrolScript.BackgroundImage = null;
			this.menuItemPropertiesOpenOrCreatePatrolScript.Name = "menuItemPropertiesOpenOrCreatePatrolScript";
			this.menuItemPropertiesOpenOrCreatePatrolScript.ShortcutKeyDisplayString = null;
			this.menuItemPropertiesOpenOrCreatePatrolScript.Tag = "load_patrol_script_to_scipt_editor";
			this.menuItemPropertiesOpenOrCreateSpawnTuner.AccessibleDescription = null;
			this.menuItemPropertiesOpenOrCreateSpawnTuner.AccessibleName = null;
			resources.ApplyResources(this.menuItemPropertiesOpenOrCreateSpawnTuner, "menuItemPropertiesOpenOrCreateSpawnTuner");
			this.menuItemPropertiesOpenOrCreateSpawnTuner.BackgroundImage = null;
			this.menuItemPropertiesOpenOrCreateSpawnTuner.Name = "menuItemPropertiesOpenOrCreateSpawnTuner";
			this.menuItemPropertiesOpenOrCreateSpawnTuner.ShortcutKeyDisplayString = null;
			this.menuItemPropertiesOpenOrCreateSpawnTuner.Tag = "load_spawn_tuner_to_script_editor";
			this.toolStripSeparator4.AccessibleDescription = null;
			this.toolStripSeparator4.AccessibleName = null;
			resources.ApplyResources(this.toolStripSeparator4, "toolStripSeparator4");
			this.toolStripSeparator4.Name = "toolStripSeparator4";
			this.addToObjectEditorToolStripMenuItem.AccessibleDescription = null;
			this.addToObjectEditorToolStripMenuItem.AccessibleName = null;
			resources.ApplyResources(this.addToObjectEditorToolStripMenuItem, "addToObjectEditorToolStripMenuItem");
			this.addToObjectEditorToolStripMenuItem.BackgroundImage = null;
			this.addToObjectEditorToolStripMenuItem.Name = "addToObjectEditorToolStripMenuItem";
			this.addToObjectEditorToolStripMenuItem.ShortcutKeyDisplayString = null;
			this.addToObjectEditorToolStripMenuItem.Tag = "add_to_object_editor";
			this.toolStripSeparator5.AccessibleDescription = null;
			this.toolStripSeparator5.AccessibleName = null;
			resources.ApplyResources(this.toolStripSeparator5, "toolStripSeparator5");
			this.toolStripSeparator5.Name = "toolStripSeparator5";
			this.markAsUnselectableToolStripMenuItem.AccessibleDescription = null;
			this.markAsUnselectableToolStripMenuItem.AccessibleName = null;
			resources.ApplyResources(this.markAsUnselectableToolStripMenuItem, "markAsUnselectableToolStripMenuItem");
			this.markAsUnselectableToolStripMenuItem.BackgroundImage = null;
			this.markAsUnselectableToolStripMenuItem.Name = "markAsUnselectableToolStripMenuItem";
			this.markAsUnselectableToolStripMenuItem.ShortcutKeyDisplayString = null;
			this.markAsUnselectableToolStripMenuItem.Tag = "mark_as_unselectable";
			this.selectorToolStripMenuItem.AccessibleDescription = null;
			this.selectorToolStripMenuItem.AccessibleName = null;
			resources.ApplyResources(this.selectorToolStripMenuItem, "selectorToolStripMenuItem");
			this.selectorToolStripMenuItem.BackgroundImage = null;
			this.selectorToolStripMenuItem.DropDownItems.AddRange(new global::System.Windows.Forms.ToolStripItem[]
			{
				this.menuItemSelectorMove,
				this.menuItemSelectorRotate,
				this.menuItemSelectorScale,
				this.menuItemSelectorScaleObjectOriented,
				this.menuItemSelectorSeparator00,
				this.menuItemSelectorAlongNearest,
				this.menuItemSelectorAlongTerrain,
				this.menuItemSelectorAlongWater,
				this.menuItemSelectorAlignToGrid,
				this.menuItemSelectorPlaceAlongNormal,
				this.menuItemSelectorSeparator01,
				this.menuItemSelectorLockSelection,
				this.menuItemSelectorEditObjectAfterAdd,
				this.menuItemSelectorAutomaticallyLink,
				this.menuItemSelectorCreateCrosslinks,
				this.toolStripSeparator3,
				this.toolStripMenuItem2
			});
			this.selectorToolStripMenuItem.Name = "selectorToolStripMenuItem";
			this.selectorToolStripMenuItem.ShortcutKeyDisplayString = null;
			this.menuItemSelectorMove.AccessibleDescription = null;
			this.menuItemSelectorMove.AccessibleName = null;
			resources.ApplyResources(this.menuItemSelectorMove, "menuItemSelectorMove");
			this.menuItemSelectorMove.BackgroundImage = null;
			this.menuItemSelectorMove.Name = "menuItemSelectorMove";
			this.menuItemSelectorMove.ShortcutKeyDisplayString = null;
			this.menuItemSelectorMove.Tag = "selector_move";
			this.menuItemSelectorRotate.AccessibleDescription = null;
			this.menuItemSelectorRotate.AccessibleName = null;
			resources.ApplyResources(this.menuItemSelectorRotate, "menuItemSelectorRotate");
			this.menuItemSelectorRotate.BackgroundImage = null;
			this.menuItemSelectorRotate.Name = "menuItemSelectorRotate";
			this.menuItemSelectorRotate.ShortcutKeyDisplayString = null;
			this.menuItemSelectorRotate.Tag = "selector_rotate";
			this.menuItemSelectorScale.AccessibleDescription = null;
			this.menuItemSelectorScale.AccessibleName = null;
			resources.ApplyResources(this.menuItemSelectorScale, "menuItemSelectorScale");
			this.menuItemSelectorScale.BackgroundImage = null;
			this.menuItemSelectorScale.Name = "menuItemSelectorScale";
			this.menuItemSelectorScale.ShortcutKeyDisplayString = null;
			this.menuItemSelectorScale.Tag = "selector_scale";
			this.menuItemSelectorScaleObjectOriented.AccessibleDescription = null;
			this.menuItemSelectorScaleObjectOriented.AccessibleName = null;
			resources.ApplyResources(this.menuItemSelectorScaleObjectOriented, "menuItemSelectorScaleObjectOriented");
			this.menuItemSelectorScaleObjectOriented.BackgroundImage = null;
			this.menuItemSelectorScaleObjectOriented.Name = "menuItemSelectorScaleObjectOriented";
			this.menuItemSelectorScaleObjectOriented.ShortcutKeyDisplayString = null;
			this.menuItemSelectorScaleObjectOriented.Tag = "selector_object_oriented";
			this.menuItemSelectorSeparator00.AccessibleDescription = null;
			this.menuItemSelectorSeparator00.AccessibleName = null;
			resources.ApplyResources(this.menuItemSelectorSeparator00, "menuItemSelectorSeparator00");
			this.menuItemSelectorSeparator00.Name = "menuItemSelectorSeparator00";
			this.menuItemSelectorAlongNearest.AccessibleDescription = null;
			this.menuItemSelectorAlongNearest.AccessibleName = null;
			resources.ApplyResources(this.menuItemSelectorAlongNearest, "menuItemSelectorAlongNearest");
			this.menuItemSelectorAlongNearest.BackgroundImage = null;
			this.menuItemSelectorAlongNearest.Name = "menuItemSelectorAlongNearest";
			this.menuItemSelectorAlongNearest.ShortcutKeyDisplayString = null;
			this.menuItemSelectorAlongNearest.Tag = "selector_along_object";
			this.menuItemSelectorAlongTerrain.AccessibleDescription = null;
			this.menuItemSelectorAlongTerrain.AccessibleName = null;
			resources.ApplyResources(this.menuItemSelectorAlongTerrain, "menuItemSelectorAlongTerrain");
			this.menuItemSelectorAlongTerrain.BackgroundImage = null;
			this.menuItemSelectorAlongTerrain.Name = "menuItemSelectorAlongTerrain";
			this.menuItemSelectorAlongTerrain.ShortcutKeyDisplayString = null;
			this.menuItemSelectorAlongTerrain.Tag = "selector_along_terrain";
			this.menuItemSelectorAlongWater.AccessibleDescription = null;
			this.menuItemSelectorAlongWater.AccessibleName = null;
			resources.ApplyResources(this.menuItemSelectorAlongWater, "menuItemSelectorAlongWater");
			this.menuItemSelectorAlongWater.BackgroundImage = null;
			this.menuItemSelectorAlongWater.Name = "menuItemSelectorAlongWater";
			this.menuItemSelectorAlongWater.ShortcutKeyDisplayString = null;
			this.menuItemSelectorAlongWater.Tag = "selector_along_water";
			this.menuItemSelectorAlignToGrid.AccessibleDescription = null;
			this.menuItemSelectorAlignToGrid.AccessibleName = null;
			resources.ApplyResources(this.menuItemSelectorAlignToGrid, "menuItemSelectorAlignToGrid");
			this.menuItemSelectorAlignToGrid.BackgroundImage = null;
			this.menuItemSelectorAlignToGrid.Name = "menuItemSelectorAlignToGrid";
			this.menuItemSelectorAlignToGrid.ShortcutKeyDisplayString = null;
			this.menuItemSelectorAlignToGrid.Tag = "selector_align_to_grid";
			this.menuItemSelectorPlaceAlongNormal.AccessibleDescription = null;
			this.menuItemSelectorPlaceAlongNormal.AccessibleName = null;
			resources.ApplyResources(this.menuItemSelectorPlaceAlongNormal, "menuItemSelectorPlaceAlongNormal");
			this.menuItemSelectorPlaceAlongNormal.BackgroundImage = null;
			this.menuItemSelectorPlaceAlongNormal.Name = "menuItemSelectorPlaceAlongNormal";
			this.menuItemSelectorPlaceAlongNormal.ShortcutKeyDisplayString = null;
			this.menuItemSelectorPlaceAlongNormal.Tag = "selector_place_along_normal";
			this.menuItemSelectorSeparator01.AccessibleDescription = null;
			this.menuItemSelectorSeparator01.AccessibleName = null;
			resources.ApplyResources(this.menuItemSelectorSeparator01, "menuItemSelectorSeparator01");
			this.menuItemSelectorSeparator01.Name = "menuItemSelectorSeparator01";
			this.menuItemSelectorLockSelection.AccessibleDescription = null;
			this.menuItemSelectorLockSelection.AccessibleName = null;
			resources.ApplyResources(this.menuItemSelectorLockSelection, "menuItemSelectorLockSelection");
			this.menuItemSelectorLockSelection.BackgroundImage = null;
			this.menuItemSelectorLockSelection.Name = "menuItemSelectorLockSelection";
			this.menuItemSelectorLockSelection.ShortcutKeyDisplayString = null;
			this.menuItemSelectorLockSelection.Tag = "selector_lock_selection";
			this.menuItemSelectorEditObjectAfterAdd.AccessibleDescription = null;
			this.menuItemSelectorEditObjectAfterAdd.AccessibleName = null;
			resources.ApplyResources(this.menuItemSelectorEditObjectAfterAdd, "menuItemSelectorEditObjectAfterAdd");
			this.menuItemSelectorEditObjectAfterAdd.BackgroundImage = null;
			this.menuItemSelectorEditObjectAfterAdd.Name = "menuItemSelectorEditObjectAfterAdd";
			this.menuItemSelectorEditObjectAfterAdd.ShortcutKeyDisplayString = null;
			this.menuItemSelectorEditObjectAfterAdd.Tag = "selector_edit_object_after_add";
			this.menuItemSelectorAutomaticallyLink.AccessibleDescription = null;
			this.menuItemSelectorAutomaticallyLink.AccessibleName = null;
			resources.ApplyResources(this.menuItemSelectorAutomaticallyLink, "menuItemSelectorAutomaticallyLink");
			this.menuItemSelectorAutomaticallyLink.BackgroundImage = null;
			this.menuItemSelectorAutomaticallyLink.Name = "menuItemSelectorAutomaticallyLink";
			this.menuItemSelectorAutomaticallyLink.ShortcutKeyDisplayString = null;
			this.menuItemSelectorAutomaticallyLink.Tag = "selector_autolink_after_add";
			this.menuItemSelectorCreateCrosslinks.AccessibleDescription = null;
			this.menuItemSelectorCreateCrosslinks.AccessibleName = null;
			resources.ApplyResources(this.menuItemSelectorCreateCrosslinks, "menuItemSelectorCreateCrosslinks");
			this.menuItemSelectorCreateCrosslinks.BackgroundImage = null;
			this.menuItemSelectorCreateCrosslinks.Name = "menuItemSelectorCreateCrosslinks";
			this.menuItemSelectorCreateCrosslinks.ShortcutKeyDisplayString = null;
			this.menuItemSelectorCreateCrosslinks.Tag = "selector_create_crosslinks";
			this.toolStripSeparator3.AccessibleDescription = null;
			this.toolStripSeparator3.AccessibleName = null;
			resources.ApplyResources(this.toolStripSeparator3, "toolStripSeparator3");
			this.toolStripSeparator3.Name = "toolStripSeparator3";
			this.toolStripMenuItem2.AccessibleDescription = null;
			this.toolStripMenuItem2.AccessibleName = null;
			resources.ApplyResources(this.toolStripMenuItem2, "toolStripMenuItem2");
			this.toolStripMenuItem2.BackgroundImage = null;
			this.toolStripMenuItem2.Name = "toolStripMenuItem2";
			this.toolStripMenuItem2.ShortcutKeyDisplayString = null;
			this.toolStripMenuItem2.Tag = "generate_shadows_local";
			this.menuLayers.AccessibleDescription = null;
			this.menuLayers.AccessibleName = null;
			resources.ApplyResources(this.menuLayers, "menuLayers");
			this.menuLayers.BackgroundImage = null;
			this.menuLayers.DropDownItems.AddRange(new global::System.Windows.Forms.ToolStripItem[]
			{
				this.menuItemLayersAxisGeometry,
				this.menuItemLayersStaticObjects,
				this.menuItemLayersInteractiveObjects,
				this.menuItemLayersAllObjects,
				this.menuItemLayersSky,
				this.menuItemLayersFog,
				this.worldCutSphereToolStripMenuItem,
				this.toolStripMenuItem4,
				this.toolStripMenuItem5,
				this.menuItemLayersPassability,
				this.menuItemLayersZones,
				this.toolStripMenuItem1,
				this.musicToolStripMenuItem,
				this.ambienceToolStripMenuItem,
				this.menuItemLayersDymanicSceneStatistic,
				this.setSceneTimeToolStripMenuItem,
				this.stopDayTimeToolStripMenuItem,
				this.menuItemLayersSeparator01,
				this.menuItemLayersTerrainGrid,
				this.menuItemLayersBottomGrid,
				this.menuItemLayersCollisionGeometry,
				this.menuItemLayersWireframe,
				this.linksToolStripMenuItem,
				this.menuItemLayersRoutePointsGeometry,
				this.menuItemLayersRouteObjects,
				this.menuItemLayersSpawnPointsGeometry,
				this.menuItemLayersScriptAreasGeometry,
				this.menuItemLayersAstralbordersGeometry,
				this.menuItemLayersMobsAggro,
				this.projectileVisObjectsToolStripMenuItem,
				this.menuItemLayersZoneLocatorsGeometry,
				this.menuItemLayersSound,
				this.placeDynamicObjectsToSurfaceToolStripMenuItem,
				this.toolStripMenuItem3,
				this.menuItemLayersSeparator02,
				this.menuItemLayersLargeTerrainGrid,
				this.menuItemLayersLargeBottomGrid,
				this.menuItemLayersSeparator03,
				this.menuItemLayersSetTerrainGridColor,
				this.menuItemLayersSetBottomGridColor,
				this.menuItemLayersSetBackgroundColor
			});
			this.menuLayers.Name = "menuLayers";
			this.menuLayers.ShortcutKeyDisplayString = null;
			this.menuItemLayersAxisGeometry.AccessibleDescription = null;
			this.menuItemLayersAxisGeometry.AccessibleName = null;
			resources.ApplyResources(this.menuItemLayersAxisGeometry, "menuItemLayersAxisGeometry");
			this.menuItemLayersAxisGeometry.BackgroundImage = null;
			this.menuItemLayersAxisGeometry.Name = "menuItemLayersAxisGeometry";
			this.menuItemLayersAxisGeometry.ShortcutKeyDisplayString = null;
			this.menuItemLayersAxisGeometry.Tag = "toggle_axis_user_geometry";
			this.menuItemLayersStaticObjects.AccessibleDescription = null;
			this.menuItemLayersStaticObjects.AccessibleName = null;
			resources.ApplyResources(this.menuItemLayersStaticObjects, "menuItemLayersStaticObjects");
			this.menuItemLayersStaticObjects.BackgroundImage = null;
			this.menuItemLayersStaticObjects.Name = "menuItemLayersStaticObjects";
			this.menuItemLayersStaticObjects.ShortcutKeyDisplayString = null;
			this.menuItemLayersStaticObjects.Tag = "toggle_static_objects_visible";
			this.menuItemLayersInteractiveObjects.AccessibleDescription = null;
			this.menuItemLayersInteractiveObjects.AccessibleName = null;
			resources.ApplyResources(this.menuItemLayersInteractiveObjects, "menuItemLayersInteractiveObjects");
			this.menuItemLayersInteractiveObjects.BackgroundImage = null;
			this.menuItemLayersInteractiveObjects.Name = "menuItemLayersInteractiveObjects";
			this.menuItemLayersInteractiveObjects.ShortcutKeyDisplayString = null;
			this.menuItemLayersInteractiveObjects.Tag = "toggle_interactive_objects_visible";
			this.menuItemLayersAllObjects.AccessibleDescription = null;
			this.menuItemLayersAllObjects.AccessibleName = null;
			resources.ApplyResources(this.menuItemLayersAllObjects, "menuItemLayersAllObjects");
			this.menuItemLayersAllObjects.BackgroundImage = null;
			this.menuItemLayersAllObjects.Name = "menuItemLayersAllObjects";
			this.menuItemLayersAllObjects.ShortcutKeyDisplayString = null;
			this.menuItemLayersAllObjects.Tag = "toggle_all_objects_visible";
			this.menuItemLayersSky.AccessibleDescription = null;
			this.menuItemLayersSky.AccessibleName = null;
			resources.ApplyResources(this.menuItemLayersSky, "menuItemLayersSky");
			this.menuItemLayersSky.BackgroundImage = null;
			this.menuItemLayersSky.Name = "menuItemLayersSky";
			this.menuItemLayersSky.ShortcutKeyDisplayString = null;
			this.menuItemLayersSky.Tag = "toggle_sky";
			this.menuItemLayersFog.AccessibleDescription = null;
			this.menuItemLayersFog.AccessibleName = null;
			resources.ApplyResources(this.menuItemLayersFog, "menuItemLayersFog");
			this.menuItemLayersFog.BackgroundImage = null;
			this.menuItemLayersFog.Name = "menuItemLayersFog";
			this.menuItemLayersFog.ShortcutKeyDisplayString = null;
			this.menuItemLayersFog.Tag = "toggle_fog";
			this.worldCutSphereToolStripMenuItem.AccessibleDescription = null;
			this.worldCutSphereToolStripMenuItem.AccessibleName = null;
			resources.ApplyResources(this.worldCutSphereToolStripMenuItem, "worldCutSphereToolStripMenuItem");
			this.worldCutSphereToolStripMenuItem.BackgroundImage = null;
			this.worldCutSphereToolStripMenuItem.Name = "worldCutSphereToolStripMenuItem";
			this.worldCutSphereToolStripMenuItem.ShortcutKeyDisplayString = null;
			this.worldCutSphereToolStripMenuItem.Tag = "toggle_world_cut_sphere";
			this.toolStripMenuItem4.AccessibleDescription = null;
			this.toolStripMenuItem4.AccessibleName = null;
			resources.ApplyResources(this.toolStripMenuItem4, "toolStripMenuItem4");
			this.toolStripMenuItem4.BackgroundImage = null;
			this.toolStripMenuItem4.Name = "toolStripMenuItem4";
			this.toolStripMenuItem4.ShortcutKeyDisplayString = null;
			this.toolStripMenuItem4.Tag = "toggle_grass";
			this.toolStripMenuItem5.AccessibleDescription = null;
			this.toolStripMenuItem5.AccessibleName = null;
			resources.ApplyResources(this.toolStripMenuItem5, "toolStripMenuItem5");
			this.toolStripMenuItem5.BackgroundImage = null;
			this.toolStripMenuItem5.Name = "toolStripMenuItem5";
			this.toolStripMenuItem5.ShortcutKeyDisplayString = null;
			this.toolStripMenuItem5.Tag = "toggle_water";
			this.menuItemLayersPassability.AccessibleDescription = null;
			this.menuItemLayersPassability.AccessibleName = null;
			resources.ApplyResources(this.menuItemLayersPassability, "menuItemLayersPassability");
			this.menuItemLayersPassability.BackgroundImage = null;
			this.menuItemLayersPassability.Name = "menuItemLayersPassability";
			this.menuItemLayersPassability.ShortcutKeyDisplayString = null;
			this.menuItemLayersPassability.Tag = "toggle_passability";
			this.menuItemLayersZones.AccessibleDescription = null;
			this.menuItemLayersZones.AccessibleName = null;
			resources.ApplyResources(this.menuItemLayersZones, "menuItemLayersZones");
			this.menuItemLayersZones.BackgroundImage = null;
			this.menuItemLayersZones.Name = "menuItemLayersZones";
			this.menuItemLayersZones.ShortcutKeyDisplayString = null;
			this.menuItemLayersZones.Tag = "toggle_zones";
			this.toolStripMenuItem1.AccessibleDescription = null;
			this.toolStripMenuItem1.AccessibleName = null;
			resources.ApplyResources(this.toolStripMenuItem1, "toolStripMenuItem1");
			this.toolStripMenuItem1.BackgroundImage = null;
			this.toolStripMenuItem1.Name = "toolStripMenuItem1";
			this.toolStripMenuItem1.ShortcutKeyDisplayString = null;
			this.toolStripMenuItem1.Tag = "toggle_zone_lights";
			this.musicToolStripMenuItem.AccessibleDescription = null;
			this.musicToolStripMenuItem.AccessibleName = null;
			resources.ApplyResources(this.musicToolStripMenuItem, "musicToolStripMenuItem");
			this.musicToolStripMenuItem.BackgroundImage = null;
			this.musicToolStripMenuItem.Name = "musicToolStripMenuItem";
			this.musicToolStripMenuItem.ShortcutKeyDisplayString = null;
			this.musicToolStripMenuItem.Tag = "toggle_sound_music";
			this.ambienceToolStripMenuItem.AccessibleDescription = null;
			this.ambienceToolStripMenuItem.AccessibleName = null;
			resources.ApplyResources(this.ambienceToolStripMenuItem, "ambienceToolStripMenuItem");
			this.ambienceToolStripMenuItem.BackgroundImage = null;
			this.ambienceToolStripMenuItem.Name = "ambienceToolStripMenuItem";
			this.ambienceToolStripMenuItem.ShortcutKeyDisplayString = null;
			this.ambienceToolStripMenuItem.Tag = "toggle_sound_ambience";
			this.menuItemLayersDymanicSceneStatistic.AccessibleDescription = null;
			this.menuItemLayersDymanicSceneStatistic.AccessibleName = null;
			resources.ApplyResources(this.menuItemLayersDymanicSceneStatistic, "menuItemLayersDymanicSceneStatistic");
			this.menuItemLayersDymanicSceneStatistic.BackgroundImage = null;
			this.menuItemLayersDymanicSceneStatistic.Name = "menuItemLayersDymanicSceneStatistic";
			this.menuItemLayersDymanicSceneStatistic.ShortcutKeyDisplayString = null;
			this.menuItemLayersDymanicSceneStatistic.Tag = "toggle_dynamic_statistic";
			this.setSceneTimeToolStripMenuItem.AccessibleDescription = null;
			this.setSceneTimeToolStripMenuItem.AccessibleName = null;
			resources.ApplyResources(this.setSceneTimeToolStripMenuItem, "setSceneTimeToolStripMenuItem");
			this.setSceneTimeToolStripMenuItem.BackgroundImage = null;
			this.setSceneTimeToolStripMenuItem.Name = "setSceneTimeToolStripMenuItem";
			this.setSceneTimeToolStripMenuItem.ShortcutKeyDisplayString = null;
			this.setSceneTimeToolStripMenuItem.Tag = "toggle_time";
			this.stopDayTimeToolStripMenuItem.AccessibleDescription = null;
			this.stopDayTimeToolStripMenuItem.AccessibleName = null;
			resources.ApplyResources(this.stopDayTimeToolStripMenuItem, "stopDayTimeToolStripMenuItem");
			this.stopDayTimeToolStripMenuItem.BackgroundImage = null;
			this.stopDayTimeToolStripMenuItem.Name = "stopDayTimeToolStripMenuItem";
			this.stopDayTimeToolStripMenuItem.ShortcutKeyDisplayString = null;
			this.stopDayTimeToolStripMenuItem.Tag = "toggle_stop_day_time";
			this.menuItemLayersSeparator01.AccessibleDescription = null;
			this.menuItemLayersSeparator01.AccessibleName = null;
			resources.ApplyResources(this.menuItemLayersSeparator01, "menuItemLayersSeparator01");
			this.menuItemLayersSeparator01.Name = "menuItemLayersSeparator01";
			this.menuItemLayersTerrainGrid.AccessibleDescription = null;
			this.menuItemLayersTerrainGrid.AccessibleName = null;
			resources.ApplyResources(this.menuItemLayersTerrainGrid, "menuItemLayersTerrainGrid");
			this.menuItemLayersTerrainGrid.BackgroundImage = null;
			this.menuItemLayersTerrainGrid.Name = "menuItemLayersTerrainGrid";
			this.menuItemLayersTerrainGrid.ShortcutKeyDisplayString = null;
			this.menuItemLayersTerrainGrid.Tag = "toggle_terrain_grid";
			this.menuItemLayersBottomGrid.AccessibleDescription = null;
			this.menuItemLayersBottomGrid.AccessibleName = null;
			resources.ApplyResources(this.menuItemLayersBottomGrid, "menuItemLayersBottomGrid");
			this.menuItemLayersBottomGrid.BackgroundImage = null;
			this.menuItemLayersBottomGrid.Name = "menuItemLayersBottomGrid";
			this.menuItemLayersBottomGrid.ShortcutKeyDisplayString = null;
			this.menuItemLayersBottomGrid.Tag = "toggle_bottom_grid";
			this.menuItemLayersCollisionGeometry.AccessibleDescription = null;
			this.menuItemLayersCollisionGeometry.AccessibleName = null;
			resources.ApplyResources(this.menuItemLayersCollisionGeometry, "menuItemLayersCollisionGeometry");
			this.menuItemLayersCollisionGeometry.BackgroundImage = null;
			this.menuItemLayersCollisionGeometry.Name = "menuItemLayersCollisionGeometry";
			this.menuItemLayersCollisionGeometry.ShortcutKeyDisplayString = null;
			this.menuItemLayersCollisionGeometry.Tag = "toggle_collision_geometry";
			this.menuItemLayersWireframe.AccessibleDescription = null;
			this.menuItemLayersWireframe.AccessibleName = null;
			resources.ApplyResources(this.menuItemLayersWireframe, "menuItemLayersWireframe");
			this.menuItemLayersWireframe.BackgroundImage = null;
			this.menuItemLayersWireframe.Name = "menuItemLayersWireframe";
			this.menuItemLayersWireframe.ShortcutKeyDisplayString = null;
			this.menuItemLayersWireframe.Tag = "toggle_wireframe";
			this.linksToolStripMenuItem.AccessibleDescription = null;
			this.linksToolStripMenuItem.AccessibleName = null;
			resources.ApplyResources(this.linksToolStripMenuItem, "linksToolStripMenuItem");
			this.linksToolStripMenuItem.BackgroundImage = null;
			this.linksToolStripMenuItem.Name = "linksToolStripMenuItem";
			this.linksToolStripMenuItem.ShortcutKeyDisplayString = null;
			this.linksToolStripMenuItem.Tag = "toggle_link_user_geometry";
			this.menuItemLayersRoutePointsGeometry.AccessibleDescription = null;
			this.menuItemLayersRoutePointsGeometry.AccessibleName = null;
			resources.ApplyResources(this.menuItemLayersRoutePointsGeometry, "menuItemLayersRoutePointsGeometry");
			this.menuItemLayersRoutePointsGeometry.BackgroundImage = null;
			this.menuItemLayersRoutePointsGeometry.Name = "menuItemLayersRoutePointsGeometry";
			this.menuItemLayersRoutePointsGeometry.ShortcutKeyDisplayString = null;
			this.menuItemLayersRoutePointsGeometry.Tag = "toggle_route_point_user_geometry";
			this.menuItemLayersRouteObjects.AccessibleDescription = null;
			this.menuItemLayersRouteObjects.AccessibleName = null;
			resources.ApplyResources(this.menuItemLayersRouteObjects, "menuItemLayersRouteObjects");
			this.menuItemLayersRouteObjects.BackgroundImage = null;
			this.menuItemLayersRouteObjects.Name = "menuItemLayersRouteObjects";
			this.menuItemLayersRouteObjects.ShortcutKeyDisplayString = null;
			this.menuItemLayersRouteObjects.Tag = "toggle_route_objects";
			this.menuItemLayersSpawnPointsGeometry.AccessibleDescription = null;
			this.menuItemLayersSpawnPointsGeometry.AccessibleName = null;
			resources.ApplyResources(this.menuItemLayersSpawnPointsGeometry, "menuItemLayersSpawnPointsGeometry");
			this.menuItemLayersSpawnPointsGeometry.BackgroundImage = null;
			this.menuItemLayersSpawnPointsGeometry.Name = "menuItemLayersSpawnPointsGeometry";
			this.menuItemLayersSpawnPointsGeometry.ShortcutKeyDisplayString = null;
			this.menuItemLayersSpawnPointsGeometry.Tag = "toggle_spawn_point_user_geometry";
			this.menuItemLayersScriptAreasGeometry.AccessibleDescription = null;
			this.menuItemLayersScriptAreasGeometry.AccessibleName = null;
			resources.ApplyResources(this.menuItemLayersScriptAreasGeometry, "menuItemLayersScriptAreasGeometry");
			this.menuItemLayersScriptAreasGeometry.BackgroundImage = null;
			this.menuItemLayersScriptAreasGeometry.Name = "menuItemLayersScriptAreasGeometry";
			this.menuItemLayersScriptAreasGeometry.ShortcutKeyDisplayString = null;
			this.menuItemLayersScriptAreasGeometry.Tag = "toggle_script_area_user_geometry";
			this.menuItemLayersAstralbordersGeometry.AccessibleDescription = null;
			this.menuItemLayersAstralbordersGeometry.AccessibleName = null;
			resources.ApplyResources(this.menuItemLayersAstralbordersGeometry, "menuItemLayersAstralbordersGeometry");
			this.menuItemLayersAstralbordersGeometry.BackgroundImage = null;
			this.menuItemLayersAstralbordersGeometry.Name = "menuItemLayersAstralbordersGeometry";
			this.menuItemLayersAstralbordersGeometry.ShortcutKeyDisplayString = null;
			this.menuItemLayersAstralbordersGeometry.Tag = "toggle_astral_border_user_geometry";
			this.menuItemLayersMobsAggro.AccessibleDescription = null;
			this.menuItemLayersMobsAggro.AccessibleName = null;
			resources.ApplyResources(this.menuItemLayersMobsAggro, "menuItemLayersMobsAggro");
			this.menuItemLayersMobsAggro.BackgroundImage = null;
			this.menuItemLayersMobsAggro.DropDownItems.AddRange(new global::System.Windows.Forms.ToolStripItem[]
			{
				this.menuItemLayersMobsAggroRadius,
				this.menuItemLayersMobsAggroVolumes
			});
			this.menuItemLayersMobsAggro.Name = "menuItemLayersMobsAggro";
			this.menuItemLayersMobsAggro.ShortcutKeyDisplayString = null;
			this.menuItemLayersMobsAggroRadius.AccessibleDescription = null;
			this.menuItemLayersMobsAggroRadius.AccessibleName = null;
			resources.ApplyResources(this.menuItemLayersMobsAggroRadius, "menuItemLayersMobsAggroRadius");
			this.menuItemLayersMobsAggroRadius.BackgroundImage = null;
			this.menuItemLayersMobsAggroRadius.Name = "menuItemLayersMobsAggroRadius";
			this.menuItemLayersMobsAggroRadius.ShortcutKeyDisplayString = null;
			this.menuItemLayersMobsAggroRadius.Tag = "toggle_aggro_radius";
			this.menuItemLayersMobsAggroVolumes.AccessibleDescription = null;
			this.menuItemLayersMobsAggroVolumes.AccessibleName = null;
			resources.ApplyResources(this.menuItemLayersMobsAggroVolumes, "menuItemLayersMobsAggroVolumes");
			this.menuItemLayersMobsAggroVolumes.BackgroundImage = null;
			this.menuItemLayersMobsAggroVolumes.Name = "menuItemLayersMobsAggroVolumes";
			this.menuItemLayersMobsAggroVolumes.ShortcutKeyDisplayString = null;
			this.menuItemLayersMobsAggroVolumes.Tag = "volume_aggro_radius";
			this.projectileVisObjectsToolStripMenuItem.AccessibleDescription = null;
			this.projectileVisObjectsToolStripMenuItem.AccessibleName = null;
			resources.ApplyResources(this.projectileVisObjectsToolStripMenuItem, "projectileVisObjectsToolStripMenuItem");
			this.projectileVisObjectsToolStripMenuItem.BackgroundImage = null;
			this.projectileVisObjectsToolStripMenuItem.Name = "projectileVisObjectsToolStripMenuItem";
			this.projectileVisObjectsToolStripMenuItem.ShortcutKeyDisplayString = null;
			this.projectileVisObjectsToolStripMenuItem.Tag = "toggle_projectile_vis_objects";
			this.menuItemLayersZoneLocatorsGeometry.AccessibleDescription = null;
			this.menuItemLayersZoneLocatorsGeometry.AccessibleName = null;
			resources.ApplyResources(this.menuItemLayersZoneLocatorsGeometry, "menuItemLayersZoneLocatorsGeometry");
			this.menuItemLayersZoneLocatorsGeometry.BackgroundImage = null;
			this.menuItemLayersZoneLocatorsGeometry.Name = "menuItemLayersZoneLocatorsGeometry";
			this.menuItemLayersZoneLocatorsGeometry.ShortcutKeyDisplayString = null;
			this.menuItemLayersZoneLocatorsGeometry.Tag = "toggle_zone_locator_user_geometry";
			this.menuItemLayersSound.AccessibleDescription = null;
			this.menuItemLayersSound.AccessibleName = null;
			resources.ApplyResources(this.menuItemLayersSound, "menuItemLayersSound");
			this.menuItemLayersSound.BackgroundImage = null;
			this.menuItemLayersSound.Name = "menuItemLayersSound";
			this.menuItemLayersSound.ShortcutKeyDisplayString = null;
			this.menuItemLayersSound.Tag = "toggle_sound";
			this.placeDynamicObjectsToSurfaceToolStripMenuItem.AccessibleDescription = null;
			this.placeDynamicObjectsToSurfaceToolStripMenuItem.AccessibleName = null;
			resources.ApplyResources(this.placeDynamicObjectsToSurfaceToolStripMenuItem, "placeDynamicObjectsToSurfaceToolStripMenuItem");
			this.placeDynamicObjectsToSurfaceToolStripMenuItem.BackgroundImage = null;
			this.placeDynamicObjectsToSurfaceToolStripMenuItem.Name = "placeDynamicObjectsToSurfaceToolStripMenuItem";
			this.placeDynamicObjectsToSurfaceToolStripMenuItem.ShortcutKeyDisplayString = null;
			this.placeDynamicObjectsToSurfaceToolStripMenuItem.Tag = "toggle_dynamic_objects_fall";
			this.toolStripMenuItem3.AccessibleDescription = null;
			this.toolStripMenuItem3.AccessibleName = null;
			resources.ApplyResources(this.toolStripMenuItem3, "toolStripMenuItem3");
			this.toolStripMenuItem3.BackgroundImage = null;
			this.toolStripMenuItem3.Name = "toolStripMenuItem3";
			this.toolStripMenuItem3.ShortcutKeyDisplayString = null;
			this.toolStripMenuItem3.Tag = "toggle_highlight_interactive_objects";
			this.menuItemLayersSeparator02.AccessibleDescription = null;
			this.menuItemLayersSeparator02.AccessibleName = null;
			resources.ApplyResources(this.menuItemLayersSeparator02, "menuItemLayersSeparator02");
			this.menuItemLayersSeparator02.Name = "menuItemLayersSeparator02";
			this.menuItemLayersLargeTerrainGrid.AccessibleDescription = null;
			this.menuItemLayersLargeTerrainGrid.AccessibleName = null;
			resources.ApplyResources(this.menuItemLayersLargeTerrainGrid, "menuItemLayersLargeTerrainGrid");
			this.menuItemLayersLargeTerrainGrid.BackgroundImage = null;
			this.menuItemLayersLargeTerrainGrid.Name = "menuItemLayersLargeTerrainGrid";
			this.menuItemLayersLargeTerrainGrid.ShortcutKeyDisplayString = null;
			this.menuItemLayersLargeTerrainGrid.Tag = "large_terrain_grid";
			this.menuItemLayersLargeBottomGrid.AccessibleDescription = null;
			this.menuItemLayersLargeBottomGrid.AccessibleName = null;
			resources.ApplyResources(this.menuItemLayersLargeBottomGrid, "menuItemLayersLargeBottomGrid");
			this.menuItemLayersLargeBottomGrid.BackgroundImage = null;
			this.menuItemLayersLargeBottomGrid.Name = "menuItemLayersLargeBottomGrid";
			this.menuItemLayersLargeBottomGrid.ShortcutKeyDisplayString = null;
			this.menuItemLayersLargeBottomGrid.Tag = "large_bottom_grid";
			this.menuItemLayersSeparator03.AccessibleDescription = null;
			this.menuItemLayersSeparator03.AccessibleName = null;
			resources.ApplyResources(this.menuItemLayersSeparator03, "menuItemLayersSeparator03");
			this.menuItemLayersSeparator03.Name = "menuItemLayersSeparator03";
			this.menuItemLayersSetTerrainGridColor.AccessibleDescription = null;
			this.menuItemLayersSetTerrainGridColor.AccessibleName = null;
			resources.ApplyResources(this.menuItemLayersSetTerrainGridColor, "menuItemLayersSetTerrainGridColor");
			this.menuItemLayersSetTerrainGridColor.BackgroundImage = null;
			this.menuItemLayersSetTerrainGridColor.Name = "menuItemLayersSetTerrainGridColor";
			this.menuItemLayersSetTerrainGridColor.ShortcutKeyDisplayString = null;
			this.menuItemLayersSetTerrainGridColor.Tag = "set_terrain_grid_color";
			this.menuItemLayersSetBottomGridColor.AccessibleDescription = null;
			this.menuItemLayersSetBottomGridColor.AccessibleName = null;
			resources.ApplyResources(this.menuItemLayersSetBottomGridColor, "menuItemLayersSetBottomGridColor");
			this.menuItemLayersSetBottomGridColor.BackgroundImage = null;
			this.menuItemLayersSetBottomGridColor.Name = "menuItemLayersSetBottomGridColor";
			this.menuItemLayersSetBottomGridColor.ShortcutKeyDisplayString = null;
			this.menuItemLayersSetBottomGridColor.Tag = "set_bottom_grid_color";
			this.menuItemLayersSetBackgroundColor.AccessibleDescription = null;
			this.menuItemLayersSetBackgroundColor.AccessibleName = null;
			resources.ApplyResources(this.menuItemLayersSetBackgroundColor, "menuItemLayersSetBackgroundColor");
			this.menuItemLayersSetBackgroundColor.BackgroundImage = null;
			this.menuItemLayersSetBackgroundColor.Name = "menuItemLayersSetBackgroundColor";
			this.menuItemLayersSetBackgroundColor.ShortcutKeyDisplayString = null;
			this.menuItemLayersSetBackgroundColor.Tag = "set_background_color";
			this.menuView.AccessibleDescription = null;
			this.menuView.AccessibleName = null;
			resources.ApplyResources(this.menuView, "menuView");
			this.menuView.BackgroundImage = null;
			this.menuView.DropDownItems.AddRange(new global::System.Windows.Forms.ToolStripItem[]
			{
				this.menuItemViewMeasureDistance,
				this.menuItemViewSeparator00,
				this.menuItemViewGenerateShadows,
				this.menuItemViewFixTerrainTiles,
				this.menuItemViewSeparator01,
				this.menuItemViewMinimap,
				this.menuItemViewTool,
				this.menuItemViewList,
				this.menuItemViewRouteObjectBrowser,
				this.menuItemViewObjectEditor,
				this.menuItemViewMultiobjectBrowser,
				this.objectSetBrowserToolStripMenuItem,
				this.menuItemViewRooadBrowser,
				this.menuItemViewProperties,
				this.checkersWindowToolStripMenuItem,
				this.notSelectableObjectsToolStripMenuItem,
				this.hiddenObjectsToolStripMenuItem,
				this.menuItemViewSeparator02,
				this.menuItemViewMainToolbar,
				this.menuItemViewToolsToolbar,
				this.menuItemViewHelpString,
				this.menuItemViewStatusbar,
				this.menuItemViewSeparator03,
				this.menuItemViewLog
			});
			this.menuView.Name = "menuView";
			this.menuView.ShortcutKeyDisplayString = null;
			this.menuItemViewMeasureDistance.AccessibleDescription = null;
			this.menuItemViewMeasureDistance.AccessibleName = null;
			resources.ApplyResources(this.menuItemViewMeasureDistance, "menuItemViewMeasureDistance");
			this.menuItemViewMeasureDistance.BackgroundImage = null;
			this.menuItemViewMeasureDistance.Name = "menuItemViewMeasureDistance";
			this.menuItemViewMeasureDistance.ShortcutKeyDisplayString = null;
			this.menuItemViewMeasureDistance.Tag = "toggle_measure_distance";
			this.menuItemViewSeparator00.AccessibleDescription = null;
			this.menuItemViewSeparator00.AccessibleName = null;
			resources.ApplyResources(this.menuItemViewSeparator00, "menuItemViewSeparator00");
			this.menuItemViewSeparator00.Name = "menuItemViewSeparator00";
			this.menuItemViewGenerateShadows.AccessibleDescription = null;
			this.menuItemViewGenerateShadows.AccessibleName = null;
			resources.ApplyResources(this.menuItemViewGenerateShadows, "menuItemViewGenerateShadows");
			this.menuItemViewGenerateShadows.BackgroundImage = null;
			this.menuItemViewGenerateShadows.Name = "menuItemViewGenerateShadows";
			this.menuItemViewGenerateShadows.ShortcutKeyDisplayString = null;
			this.menuItemViewGenerateShadows.Tag = "generate_shadows";
			this.menuItemViewFixTerrainTiles.AccessibleDescription = null;
			this.menuItemViewFixTerrainTiles.AccessibleName = null;
			resources.ApplyResources(this.menuItemViewFixTerrainTiles, "menuItemViewFixTerrainTiles");
			this.menuItemViewFixTerrainTiles.BackgroundImage = null;
			this.menuItemViewFixTerrainTiles.Name = "menuItemViewFixTerrainTiles";
			this.menuItemViewFixTerrainTiles.ShortcutKeyDisplayString = null;
			this.menuItemViewFixTerrainTiles.Tag = "fix_terrain_tiles";
			this.menuItemViewSeparator01.AccessibleDescription = null;
			this.menuItemViewSeparator01.AccessibleName = null;
			resources.ApplyResources(this.menuItemViewSeparator01, "menuItemViewSeparator01");
			this.menuItemViewSeparator01.Name = "menuItemViewSeparator01";
			this.menuItemViewMinimap.AccessibleDescription = null;
			this.menuItemViewMinimap.AccessibleName = null;
			resources.ApplyResources(this.menuItemViewMinimap, "menuItemViewMinimap");
			this.menuItemViewMinimap.BackgroundImage = null;
			this.menuItemViewMinimap.Name = "menuItemViewMinimap";
			this.menuItemViewMinimap.ShortcutKeyDisplayString = null;
			this.menuItemViewMinimap.Tag = "toggle_minimap";
			this.menuItemViewTool.AccessibleDescription = null;
			this.menuItemViewTool.AccessibleName = null;
			resources.ApplyResources(this.menuItemViewTool, "menuItemViewTool");
			this.menuItemViewTool.BackgroundImage = null;
			this.menuItemViewTool.Name = "menuItemViewTool";
			this.menuItemViewTool.ShortcutKeyDisplayString = null;
			this.menuItemViewTool.Tag = "toggle_tool";
			this.menuItemViewList.AccessibleDescription = null;
			this.menuItemViewList.AccessibleName = null;
			resources.ApplyResources(this.menuItemViewList, "menuItemViewList");
			this.menuItemViewList.BackgroundImage = null;
			this.menuItemViewList.Name = "menuItemViewList";
			this.menuItemViewList.ShortcutKeyDisplayString = null;
			this.menuItemViewList.Tag = "toggle_list";
			this.menuItemViewRouteObjectBrowser.AccessibleDescription = null;
			this.menuItemViewRouteObjectBrowser.AccessibleName = null;
			resources.ApplyResources(this.menuItemViewRouteObjectBrowser, "menuItemViewRouteObjectBrowser");
			this.menuItemViewRouteObjectBrowser.BackgroundImage = null;
			this.menuItemViewRouteObjectBrowser.Name = "menuItemViewRouteObjectBrowser";
			this.menuItemViewRouteObjectBrowser.ShortcutKeyDisplayString = null;
			this.menuItemViewRouteObjectBrowser.Tag = "toggle_route_object_browser";
			this.menuItemViewObjectEditor.AccessibleDescription = null;
			this.menuItemViewObjectEditor.AccessibleName = null;
			resources.ApplyResources(this.menuItemViewObjectEditor, "menuItemViewObjectEditor");
			this.menuItemViewObjectEditor.BackgroundImage = null;
			this.menuItemViewObjectEditor.Name = "menuItemViewObjectEditor";
			this.menuItemViewObjectEditor.ShortcutKeyDisplayString = null;
			this.menuItemViewObjectEditor.Tag = "toggle_object_editor";
			this.menuItemViewMultiobjectBrowser.AccessibleDescription = null;
			this.menuItemViewMultiobjectBrowser.AccessibleName = null;
			resources.ApplyResources(this.menuItemViewMultiobjectBrowser, "menuItemViewMultiobjectBrowser");
			this.menuItemViewMultiobjectBrowser.BackgroundImage = null;
			this.menuItemViewMultiobjectBrowser.Name = "menuItemViewMultiobjectBrowser";
			this.menuItemViewMultiobjectBrowser.ShortcutKeyDisplayString = null;
			this.menuItemViewMultiobjectBrowser.Tag = "toggle_multiobject_browser";
			this.objectSetBrowserToolStripMenuItem.AccessibleDescription = null;
			this.objectSetBrowserToolStripMenuItem.AccessibleName = null;
			resources.ApplyResources(this.objectSetBrowserToolStripMenuItem, "objectSetBrowserToolStripMenuItem");
			this.objectSetBrowserToolStripMenuItem.BackgroundImage = null;
			this.objectSetBrowserToolStripMenuItem.Name = "objectSetBrowserToolStripMenuItem";
			this.objectSetBrowserToolStripMenuItem.ShortcutKeyDisplayString = null;
			this.objectSetBrowserToolStripMenuItem.Tag = "toggle_object_set_browser";
			this.menuItemViewRooadBrowser.AccessibleDescription = null;
			this.menuItemViewRooadBrowser.AccessibleName = null;
			resources.ApplyResources(this.menuItemViewRooadBrowser, "menuItemViewRooadBrowser");
			this.menuItemViewRooadBrowser.BackgroundImage = null;
			this.menuItemViewRooadBrowser.Name = "menuItemViewRooadBrowser";
			this.menuItemViewRooadBrowser.ShortcutKeyDisplayString = null;
			this.menuItemViewRooadBrowser.Tag = "toggle_road_params_browser";
			this.menuItemViewProperties.AccessibleDescription = null;
			this.menuItemViewProperties.AccessibleName = null;
			resources.ApplyResources(this.menuItemViewProperties, "menuItemViewProperties");
			this.menuItemViewProperties.BackgroundImage = null;
			this.menuItemViewProperties.Name = "menuItemViewProperties";
			this.menuItemViewProperties.ShortcutKeyDisplayString = null;
			this.menuItemViewProperties.Tag = "toggle_properties";
			this.checkersWindowToolStripMenuItem.AccessibleDescription = null;
			this.checkersWindowToolStripMenuItem.AccessibleName = null;
			resources.ApplyResources(this.checkersWindowToolStripMenuItem, "checkersWindowToolStripMenuItem");
			this.checkersWindowToolStripMenuItem.BackgroundImage = null;
			this.checkersWindowToolStripMenuItem.Name = "checkersWindowToolStripMenuItem";
			this.checkersWindowToolStripMenuItem.ShortcutKeyDisplayString = null;
			this.checkersWindowToolStripMenuItem.Tag = "toggle_checkers";
			this.notSelectableObjectsToolStripMenuItem.AccessibleDescription = null;
			this.notSelectableObjectsToolStripMenuItem.AccessibleName = null;
			resources.ApplyResources(this.notSelectableObjectsToolStripMenuItem, "notSelectableObjectsToolStripMenuItem");
			this.notSelectableObjectsToolStripMenuItem.BackgroundImage = null;
			this.notSelectableObjectsToolStripMenuItem.Name = "notSelectableObjectsToolStripMenuItem";
			this.notSelectableObjectsToolStripMenuItem.ShortcutKeyDisplayString = null;
			this.notSelectableObjectsToolStripMenuItem.Tag = "toggle_unselectable_objects";
			this.hiddenObjectsToolStripMenuItem.AccessibleDescription = null;
			this.hiddenObjectsToolStripMenuItem.AccessibleName = null;
			resources.ApplyResources(this.hiddenObjectsToolStripMenuItem, "hiddenObjectsToolStripMenuItem");
			this.hiddenObjectsToolStripMenuItem.BackgroundImage = null;
			this.hiddenObjectsToolStripMenuItem.Name = "hiddenObjectsToolStripMenuItem";
			this.hiddenObjectsToolStripMenuItem.ShortcutKeyDisplayString = null;
			this.hiddenObjectsToolStripMenuItem.Tag = "toggle_hidden_objects";
			this.menuItemViewSeparator02.AccessibleDescription = null;
			this.menuItemViewSeparator02.AccessibleName = null;
			resources.ApplyResources(this.menuItemViewSeparator02, "menuItemViewSeparator02");
			this.menuItemViewSeparator02.Name = "menuItemViewSeparator02";
			this.menuItemViewMainToolbar.AccessibleDescription = null;
			this.menuItemViewMainToolbar.AccessibleName = null;
			resources.ApplyResources(this.menuItemViewMainToolbar, "menuItemViewMainToolbar");
			this.menuItemViewMainToolbar.BackgroundImage = null;
			this.menuItemViewMainToolbar.Name = "menuItemViewMainToolbar";
			this.menuItemViewMainToolbar.ShortcutKeyDisplayString = null;
			this.menuItemViewMainToolbar.Tag = "toggle_main_toolbar";
			this.menuItemViewToolsToolbar.AccessibleDescription = null;
			this.menuItemViewToolsToolbar.AccessibleName = null;
			resources.ApplyResources(this.menuItemViewToolsToolbar, "menuItemViewToolsToolbar");
			this.menuItemViewToolsToolbar.BackgroundImage = null;
			this.menuItemViewToolsToolbar.Name = "menuItemViewToolsToolbar";
			this.menuItemViewToolsToolbar.ShortcutKeyDisplayString = null;
			this.menuItemViewToolsToolbar.Tag = "toggle_tools_toolbar";
			this.menuItemViewHelpString.AccessibleDescription = null;
			this.menuItemViewHelpString.AccessibleName = null;
			resources.ApplyResources(this.menuItemViewHelpString, "menuItemViewHelpString");
			this.menuItemViewHelpString.BackgroundImage = null;
			this.menuItemViewHelpString.Name = "menuItemViewHelpString";
			this.menuItemViewHelpString.ShortcutKeyDisplayString = null;
			this.menuItemViewHelpString.Tag = "toggle_statusbar_help";
			this.menuItemViewStatusbar.AccessibleDescription = null;
			this.menuItemViewStatusbar.AccessibleName = null;
			resources.ApplyResources(this.menuItemViewStatusbar, "menuItemViewStatusbar");
			this.menuItemViewStatusbar.BackgroundImage = null;
			this.menuItemViewStatusbar.Name = "menuItemViewStatusbar";
			this.menuItemViewStatusbar.ShortcutKeyDisplayString = null;
			this.menuItemViewStatusbar.Tag = "toggle_statusbar";
			this.menuItemViewSeparator03.AccessibleDescription = null;
			this.menuItemViewSeparator03.AccessibleName = null;
			resources.ApplyResources(this.menuItemViewSeparator03, "menuItemViewSeparator03");
			this.menuItemViewSeparator03.Name = "menuItemViewSeparator03";
			this.menuItemViewLog.AccessibleDescription = null;
			this.menuItemViewLog.AccessibleName = null;
			resources.ApplyResources(this.menuItemViewLog, "menuItemViewLog");
			this.menuItemViewLog.BackgroundImage = null;
			this.menuItemViewLog.Name = "menuItemViewLog";
			this.menuItemViewLog.ShortcutKeyDisplayString = null;
			this.menuItemViewLog.Tag = "toggle_log";
			this.cameraToolStripMenuItem.AccessibleDescription = null;
			this.cameraToolStripMenuItem.AccessibleName = null;
			resources.ApplyResources(this.cameraToolStripMenuItem, "cameraToolStripMenuItem");
			this.cameraToolStripMenuItem.BackgroundImage = null;
			this.cameraToolStripMenuItem.DropDownItems.AddRange(new global::System.Windows.Forms.ToolStripItem[]
			{
				this.menuItemCameraSlow,
				this.menuItemCameraNormal,
				this.menuItemCameraFast,
				this.menuItemCameraCustom,
				this.menuItemCameraSeparator00,
				this.menuItemCameraSetCustomSpeed,
				this.setCameraFovToolStripMenuItem,
				this.menuItemCameraSeparator01,
				this.menuItemCameraMove,
				this.menuItemCameraReset,
				this.menuItemCameraBoundingToolStripMenuItem,
				this.menuItemCameraSeparator02,
				this.menuItemFPSCamera,
				this.menuItemRTSCamera
			});
			this.cameraToolStripMenuItem.Name = "cameraToolStripMenuItem";
			this.cameraToolStripMenuItem.ShortcutKeyDisplayString = null;
			this.menuItemCameraSlow.AccessibleDescription = null;
			this.menuItemCameraSlow.AccessibleName = null;
			resources.ApplyResources(this.menuItemCameraSlow, "menuItemCameraSlow");
			this.menuItemCameraSlow.BackgroundImage = null;
			this.menuItemCameraSlow.Name = "menuItemCameraSlow";
			this.menuItemCameraSlow.ShortcutKeyDisplayString = null;
			this.menuItemCameraSlow.Tag = "camera_slow";
			this.menuItemCameraNormal.AccessibleDescription = null;
			this.menuItemCameraNormal.AccessibleName = null;
			resources.ApplyResources(this.menuItemCameraNormal, "menuItemCameraNormal");
			this.menuItemCameraNormal.BackgroundImage = null;
			this.menuItemCameraNormal.Name = "menuItemCameraNormal";
			this.menuItemCameraNormal.ShortcutKeyDisplayString = null;
			this.menuItemCameraNormal.Tag = "camera_normal";
			this.menuItemCameraFast.AccessibleDescription = null;
			this.menuItemCameraFast.AccessibleName = null;
			resources.ApplyResources(this.menuItemCameraFast, "menuItemCameraFast");
			this.menuItemCameraFast.BackgroundImage = null;
			this.menuItemCameraFast.Name = "menuItemCameraFast";
			this.menuItemCameraFast.ShortcutKeyDisplayString = null;
			this.menuItemCameraFast.Tag = "camera_fast";
			this.menuItemCameraCustom.AccessibleDescription = null;
			this.menuItemCameraCustom.AccessibleName = null;
			resources.ApplyResources(this.menuItemCameraCustom, "menuItemCameraCustom");
			this.menuItemCameraCustom.BackgroundImage = null;
			this.menuItemCameraCustom.Name = "menuItemCameraCustom";
			this.menuItemCameraCustom.ShortcutKeyDisplayString = null;
			this.menuItemCameraCustom.Tag = "camera_custom";
			this.menuItemCameraSeparator00.AccessibleDescription = null;
			this.menuItemCameraSeparator00.AccessibleName = null;
			resources.ApplyResources(this.menuItemCameraSeparator00, "menuItemCameraSeparator00");
			this.menuItemCameraSeparator00.Name = "menuItemCameraSeparator00";
			this.menuItemCameraSetCustomSpeed.AccessibleDescription = null;
			this.menuItemCameraSetCustomSpeed.AccessibleName = null;
			resources.ApplyResources(this.menuItemCameraSetCustomSpeed, "menuItemCameraSetCustomSpeed");
			this.menuItemCameraSetCustomSpeed.BackgroundImage = null;
			this.menuItemCameraSetCustomSpeed.Name = "menuItemCameraSetCustomSpeed";
			this.menuItemCameraSetCustomSpeed.ShortcutKeyDisplayString = null;
			this.menuItemCameraSetCustomSpeed.Tag = "camera_speed";
			this.setCameraFovToolStripMenuItem.AccessibleDescription = null;
			this.setCameraFovToolStripMenuItem.AccessibleName = null;
			resources.ApplyResources(this.setCameraFovToolStripMenuItem, "setCameraFovToolStripMenuItem");
			this.setCameraFovToolStripMenuItem.BackgroundImage = null;
			this.setCameraFovToolStripMenuItem.Name = "setCameraFovToolStripMenuItem";
			this.setCameraFovToolStripMenuItem.ShortcutKeyDisplayString = null;
			this.setCameraFovToolStripMenuItem.Tag = "camera_fov";
			this.menuItemCameraSeparator01.AccessibleDescription = null;
			this.menuItemCameraSeparator01.AccessibleName = null;
			resources.ApplyResources(this.menuItemCameraSeparator01, "menuItemCameraSeparator01");
			this.menuItemCameraSeparator01.Name = "menuItemCameraSeparator01";
			this.menuItemCameraMove.AccessibleDescription = null;
			this.menuItemCameraMove.AccessibleName = null;
			resources.ApplyResources(this.menuItemCameraMove, "menuItemCameraMove");
			this.menuItemCameraMove.BackgroundImage = null;
			this.menuItemCameraMove.Name = "menuItemCameraMove";
			this.menuItemCameraMove.ShortcutKeyDisplayString = null;
			this.menuItemCameraMove.Tag = "camera_move";
			this.menuItemCameraReset.AccessibleDescription = null;
			this.menuItemCameraReset.AccessibleName = null;
			resources.ApplyResources(this.menuItemCameraReset, "menuItemCameraReset");
			this.menuItemCameraReset.BackgroundImage = null;
			this.menuItemCameraReset.Name = "menuItemCameraReset";
			this.menuItemCameraReset.ShortcutKeyDisplayString = null;
			this.menuItemCameraReset.Tag = "camera_reset";
			this.menuItemCameraBoundingToolStripMenuItem.AccessibleDescription = null;
			this.menuItemCameraBoundingToolStripMenuItem.AccessibleName = null;
			resources.ApplyResources(this.menuItemCameraBoundingToolStripMenuItem, "menuItemCameraBoundingToolStripMenuItem");
			this.menuItemCameraBoundingToolStripMenuItem.BackgroundImage = null;
			this.menuItemCameraBoundingToolStripMenuItem.Name = "menuItemCameraBoundingToolStripMenuItem";
			this.menuItemCameraBoundingToolStripMenuItem.ShortcutKeyDisplayString = null;
			this.menuItemCameraBoundingToolStripMenuItem.Tag = "camera_bounding";
			this.menuItemCameraSeparator02.AccessibleDescription = null;
			this.menuItemCameraSeparator02.AccessibleName = null;
			resources.ApplyResources(this.menuItemCameraSeparator02, "menuItemCameraSeparator02");
			this.menuItemCameraSeparator02.Name = "menuItemCameraSeparator02";
			this.menuItemFPSCamera.AccessibleDescription = null;
			this.menuItemFPSCamera.AccessibleName = null;
			resources.ApplyResources(this.menuItemFPSCamera, "menuItemFPSCamera");
			this.menuItemFPSCamera.BackgroundImage = null;
			this.menuItemFPSCamera.Name = "menuItemFPSCamera";
			this.menuItemFPSCamera.ShortcutKeyDisplayString = null;
			this.menuItemFPSCamera.Tag = "camera_fps";
			this.menuItemRTSCamera.AccessibleDescription = null;
			this.menuItemRTSCamera.AccessibleName = null;
			resources.ApplyResources(this.menuItemRTSCamera, "menuItemRTSCamera");
			this.menuItemRTSCamera.BackgroundImage = null;
			this.menuItemRTSCamera.Name = "menuItemRTSCamera";
			this.menuItemRTSCamera.ShortcutKeyDisplayString = null;
			this.menuItemRTSCamera.Tag = "camera_rts";
			this.menuTools.AccessibleDescription = null;
			this.menuTools.AccessibleName = null;
			resources.ApplyResources(this.menuTools, "menuTools");
			this.menuTools.BackgroundImage = null;
			this.menuTools.DropDownItems.AddRange(new global::System.Windows.Forms.ToolStripItem[]
			{
				this.menuItemToolsGameWindow,
				this.menuItemToolsLayers,
				this.waterEditorToolStripMenuItem,
				this.menuItemToolsLight,
				this.menuItemToolsDatabaseBrowser,
				this.menuItemToolsModelViewer,
				this.menuItemToolsFullModelViewer,
				this.menuItemToolsFactionEditor,
				this.menuItemToolsScriptEditor,
				this.menuItemToolsQuests,
				this.visualMobScriptsToolStripMenuItem,
				this.hSVColorEditorToolStripMenuItem,
				this.vendorsTableToolStripMenuItem,
				this.importCuesToolStripMenuItem,
				this.menuItemToolsFillHeightsFromHeightmap,
				this.createSoundStaticObjectToolStripMenuItem,
				this.createMinimapToolStripMenuItem,
				this.autoFocusToolStripMenuItem,
				this.blockEditingToolStripMenuItem
			});
			this.menuTools.Name = "menuTools";
			this.menuTools.ShortcutKeyDisplayString = null;
			this.menuItemToolsGameWindow.AccessibleDescription = null;
			this.menuItemToolsGameWindow.AccessibleName = null;
			resources.ApplyResources(this.menuItemToolsGameWindow, "menuItemToolsGameWindow");
			this.menuItemToolsGameWindow.BackgroundImage = null;
			this.menuItemToolsGameWindow.Name = "menuItemToolsGameWindow";
			this.menuItemToolsGameWindow.ShortcutKeyDisplayString = null;
			this.menuItemToolsGameWindow.Tag = "toggle_game";
			this.menuItemToolsLayers.AccessibleDescription = null;
			this.menuItemToolsLayers.AccessibleName = null;
			resources.ApplyResources(this.menuItemToolsLayers, "menuItemToolsLayers");
			this.menuItemToolsLayers.BackgroundImage = null;
			this.menuItemToolsLayers.Name = "menuItemToolsLayers";
			this.menuItemToolsLayers.ShortcutKeyDisplayString = null;
			this.menuItemToolsLayers.Tag = "toggle_layers";
			this.waterEditorToolStripMenuItem.AccessibleDescription = null;
			this.waterEditorToolStripMenuItem.AccessibleName = null;
			resources.ApplyResources(this.waterEditorToolStripMenuItem, "waterEditorToolStripMenuItem");
			this.waterEditorToolStripMenuItem.BackgroundImage = null;
			this.waterEditorToolStripMenuItem.Name = "waterEditorToolStripMenuItem";
			this.waterEditorToolStripMenuItem.ShortcutKeyDisplayString = null;
			this.waterEditorToolStripMenuItem.Tag = "toggle_water_editor";
			this.menuItemToolsLight.AccessibleDescription = null;
			this.menuItemToolsLight.AccessibleName = null;
			resources.ApplyResources(this.menuItemToolsLight, "menuItemToolsLight");
			this.menuItemToolsLight.BackgroundImage = null;
			this.menuItemToolsLight.Name = "menuItemToolsLight";
			this.menuItemToolsLight.ShortcutKeyDisplayString = null;
			this.menuItemToolsLight.Tag = "toggle_light";
			this.menuItemToolsDatabaseBrowser.AccessibleDescription = null;
			this.menuItemToolsDatabaseBrowser.AccessibleName = null;
			resources.ApplyResources(this.menuItemToolsDatabaseBrowser, "menuItemToolsDatabaseBrowser");
			this.menuItemToolsDatabaseBrowser.BackgroundImage = null;
			this.menuItemToolsDatabaseBrowser.Name = "menuItemToolsDatabaseBrowser";
			this.menuItemToolsDatabaseBrowser.ShortcutKeyDisplayString = null;
			this.menuItemToolsDatabaseBrowser.Tag = "toggle_property_control";
			this.menuItemToolsModelViewer.AccessibleDescription = null;
			this.menuItemToolsModelViewer.AccessibleName = null;
			resources.ApplyResources(this.menuItemToolsModelViewer, "menuItemToolsModelViewer");
			this.menuItemToolsModelViewer.BackgroundImage = null;
			this.menuItemToolsModelViewer.Name = "menuItemToolsModelViewer";
			this.menuItemToolsModelViewer.ShortcutKeyDisplayString = null;
			this.menuItemToolsModelViewer.Tag = "toggle_model_viewer";
			this.menuItemToolsFullModelViewer.AccessibleDescription = null;
			this.menuItemToolsFullModelViewer.AccessibleName = null;
			resources.ApplyResources(this.menuItemToolsFullModelViewer, "menuItemToolsFullModelViewer");
			this.menuItemToolsFullModelViewer.BackgroundImage = null;
			this.menuItemToolsFullModelViewer.Name = "menuItemToolsFullModelViewer";
			this.menuItemToolsFullModelViewer.ShortcutKeyDisplayString = null;
			this.menuItemToolsFullModelViewer.Tag = "toggle_model_editor";
			this.menuItemToolsFactionEditor.AccessibleDescription = null;
			this.menuItemToolsFactionEditor.AccessibleName = null;
			resources.ApplyResources(this.menuItemToolsFactionEditor, "menuItemToolsFactionEditor");
			this.menuItemToolsFactionEditor.BackgroundImage = null;
			this.menuItemToolsFactionEditor.Name = "menuItemToolsFactionEditor";
			this.menuItemToolsFactionEditor.ShortcutKeyDisplayString = null;
			this.menuItemToolsFactionEditor.Tag = "toggle_faction_editor";
			this.menuItemToolsScriptEditor.AccessibleDescription = null;
			this.menuItemToolsScriptEditor.AccessibleName = null;
			resources.ApplyResources(this.menuItemToolsScriptEditor, "menuItemToolsScriptEditor");
			this.menuItemToolsScriptEditor.BackgroundImage = null;
			this.menuItemToolsScriptEditor.Name = "menuItemToolsScriptEditor";
			this.menuItemToolsScriptEditor.ShortcutKeyDisplayString = null;
			this.menuItemToolsScriptEditor.Tag = "toggle_script_editor";
			this.menuItemToolsQuests.AccessibleDescription = null;
			this.menuItemToolsQuests.AccessibleName = null;
			resources.ApplyResources(this.menuItemToolsQuests, "menuItemToolsQuests");
			this.menuItemToolsQuests.BackgroundImage = null;
			this.menuItemToolsQuests.DropDownItems.AddRange(new global::System.Windows.Forms.ToolStripItem[]
			{
				this.menuItemToolsQuestEditor,
				this.menuItemToolsQuestDiagram,
				this.menuItemToolsQuestTexts,
				this.menuItemToolsQuickObjectCreation
			});
			this.menuItemToolsQuests.Name = "menuItemToolsQuests";
			this.menuItemToolsQuests.ShortcutKeyDisplayString = null;
			this.menuItemToolsQuests.Tag = "";
			this.menuItemToolsQuestEditor.AccessibleDescription = null;
			this.menuItemToolsQuestEditor.AccessibleName = null;
			resources.ApplyResources(this.menuItemToolsQuestEditor, "menuItemToolsQuestEditor");
			this.menuItemToolsQuestEditor.BackgroundImage = null;
			this.menuItemToolsQuestEditor.Name = "menuItemToolsQuestEditor";
			this.menuItemToolsQuestEditor.ShortcutKeyDisplayString = null;
			this.menuItemToolsQuestEditor.Tag = "toggle_quests";
			this.menuItemToolsQuestDiagram.AccessibleDescription = null;
			this.menuItemToolsQuestDiagram.AccessibleName = null;
			resources.ApplyResources(this.menuItemToolsQuestDiagram, "menuItemToolsQuestDiagram");
			this.menuItemToolsQuestDiagram.BackgroundImage = null;
			this.menuItemToolsQuestDiagram.Name = "menuItemToolsQuestDiagram";
			this.menuItemToolsQuestDiagram.ShortcutKeyDisplayString = null;
			this.menuItemToolsQuestDiagram.Tag = "toggle_quest_diagram";
			this.menuItemToolsQuestTexts.AccessibleDescription = null;
			this.menuItemToolsQuestTexts.AccessibleName = null;
			resources.ApplyResources(this.menuItemToolsQuestTexts, "menuItemToolsQuestTexts");
			this.menuItemToolsQuestTexts.BackgroundImage = null;
			this.menuItemToolsQuestTexts.Name = "menuItemToolsQuestTexts";
			this.menuItemToolsQuestTexts.ShortcutKeyDisplayString = null;
			this.menuItemToolsQuestTexts.Tag = "toggle_import_quest_texts";
			this.menuItemToolsQuickObjectCreation.AccessibleDescription = null;
			this.menuItemToolsQuickObjectCreation.AccessibleName = null;
			resources.ApplyResources(this.menuItemToolsQuickObjectCreation, "menuItemToolsQuickObjectCreation");
			this.menuItemToolsQuickObjectCreation.BackgroundImage = null;
			this.menuItemToolsQuickObjectCreation.DropDownItems.AddRange(new global::System.Windows.Forms.ToolStripItem[]
			{
				this.menuItemToolsCreateMob,
				this.menuItemToolsCreareNPC,
				this.menuItemToolsCreateResource,
				this.menuItemToolsCreateQuestItem
			});
			this.menuItemToolsQuickObjectCreation.Name = "menuItemToolsQuickObjectCreation";
			this.menuItemToolsQuickObjectCreation.ShortcutKeyDisplayString = null;
			this.menuItemToolsCreateMob.AccessibleDescription = null;
			this.menuItemToolsCreateMob.AccessibleName = null;
			resources.ApplyResources(this.menuItemToolsCreateMob, "menuItemToolsCreateMob");
			this.menuItemToolsCreateMob.BackgroundImage = null;
			this.menuItemToolsCreateMob.Name = "menuItemToolsCreateMob";
			this.menuItemToolsCreateMob.ShortcutKeyDisplayString = null;
			this.menuItemToolsCreateMob.Tag = "toggle_create_mob";
			this.menuItemToolsCreareNPC.AccessibleDescription = null;
			this.menuItemToolsCreareNPC.AccessibleName = null;
			resources.ApplyResources(this.menuItemToolsCreareNPC, "menuItemToolsCreareNPC");
			this.menuItemToolsCreareNPC.BackgroundImage = null;
			this.menuItemToolsCreareNPC.Name = "menuItemToolsCreareNPC";
			this.menuItemToolsCreareNPC.ShortcutKeyDisplayString = null;
			this.menuItemToolsCreareNPC.Tag = "toggle_create_NPC";
			this.menuItemToolsCreateResource.AccessibleDescription = null;
			this.menuItemToolsCreateResource.AccessibleName = null;
			resources.ApplyResources(this.menuItemToolsCreateResource, "menuItemToolsCreateResource");
			this.menuItemToolsCreateResource.BackgroundImage = null;
			this.menuItemToolsCreateResource.Name = "menuItemToolsCreateResource";
			this.menuItemToolsCreateResource.ShortcutKeyDisplayString = null;
			this.menuItemToolsCreateResource.Tag = "toggle_create_resource";
			this.menuItemToolsCreateQuestItem.AccessibleDescription = null;
			this.menuItemToolsCreateQuestItem.AccessibleName = null;
			resources.ApplyResources(this.menuItemToolsCreateQuestItem, "menuItemToolsCreateQuestItem");
			this.menuItemToolsCreateQuestItem.BackgroundImage = null;
			this.menuItemToolsCreateQuestItem.Name = "menuItemToolsCreateQuestItem";
			this.menuItemToolsCreateQuestItem.ShortcutKeyDisplayString = null;
			this.menuItemToolsCreateQuestItem.Tag = "toggle_create_quest_item";
			this.visualMobScriptsToolStripMenuItem.AccessibleDescription = null;
			this.visualMobScriptsToolStripMenuItem.AccessibleName = null;
			resources.ApplyResources(this.visualMobScriptsToolStripMenuItem, "visualMobScriptsToolStripMenuItem");
			this.visualMobScriptsToolStripMenuItem.BackgroundImage = null;
			this.visualMobScriptsToolStripMenuItem.Name = "visualMobScriptsToolStripMenuItem";
			this.visualMobScriptsToolStripMenuItem.ShortcutKeyDisplayString = null;
			this.visualMobScriptsToolStripMenuItem.Tag = "toggle_visual_mob_scripts";
			this.hSVColorEditorToolStripMenuItem.AccessibleDescription = null;
			this.hSVColorEditorToolStripMenuItem.AccessibleName = null;
			resources.ApplyResources(this.hSVColorEditorToolStripMenuItem, "hSVColorEditorToolStripMenuItem");
			this.hSVColorEditorToolStripMenuItem.BackgroundImage = null;
			this.hSVColorEditorToolStripMenuItem.Name = "hSVColorEditorToolStripMenuItem";
			this.hSVColorEditorToolStripMenuItem.ShortcutKeyDisplayString = null;
			this.hSVColorEditorToolStripMenuItem.Tag = "toggle_hsv_color_editor";
			this.vendorsTableToolStripMenuItem.AccessibleDescription = null;
			this.vendorsTableToolStripMenuItem.AccessibleName = null;
			resources.ApplyResources(this.vendorsTableToolStripMenuItem, "vendorsTableToolStripMenuItem");
			this.vendorsTableToolStripMenuItem.BackgroundImage = null;
			this.vendorsTableToolStripMenuItem.Name = "vendorsTableToolStripMenuItem";
			this.vendorsTableToolStripMenuItem.ShortcutKeyDisplayString = null;
			this.vendorsTableToolStripMenuItem.Tag = "toggle_vendors_table";
			this.importCuesToolStripMenuItem.AccessibleDescription = null;
			this.importCuesToolStripMenuItem.AccessibleName = null;
			resources.ApplyResources(this.importCuesToolStripMenuItem, "importCuesToolStripMenuItem");
			this.importCuesToolStripMenuItem.BackgroundImage = null;
			this.importCuesToolStripMenuItem.Name = "importCuesToolStripMenuItem";
			this.importCuesToolStripMenuItem.ShortcutKeyDisplayString = null;
			this.importCuesToolStripMenuItem.Tag = "toggle_import_cues";
			this.menuItemToolsFillHeightsFromHeightmap.AccessibleDescription = null;
			this.menuItemToolsFillHeightsFromHeightmap.AccessibleName = null;
			resources.ApplyResources(this.menuItemToolsFillHeightsFromHeightmap, "menuItemToolsFillHeightsFromHeightmap");
			this.menuItemToolsFillHeightsFromHeightmap.BackgroundImage = null;
			this.menuItemToolsFillHeightsFromHeightmap.Name = "menuItemToolsFillHeightsFromHeightmap";
			this.menuItemToolsFillHeightsFromHeightmap.ShortcutKeyDisplayString = null;
			this.menuItemToolsFillHeightsFromHeightmap.Tag = "fill_heights_from_heightmap";
			this.createSoundStaticObjectToolStripMenuItem.AccessibleDescription = null;
			this.createSoundStaticObjectToolStripMenuItem.AccessibleName = null;
			resources.ApplyResources(this.createSoundStaticObjectToolStripMenuItem, "createSoundStaticObjectToolStripMenuItem");
			this.createSoundStaticObjectToolStripMenuItem.BackgroundImage = null;
			this.createSoundStaticObjectToolStripMenuItem.Name = "createSoundStaticObjectToolStripMenuItem";
			this.createSoundStaticObjectToolStripMenuItem.ShortcutKeyDisplayString = null;
			this.createSoundStaticObjectToolStripMenuItem.Tag = "toggle_create_sound_static_object";
			this.createMinimapToolStripMenuItem.AccessibleDescription = null;
			this.createMinimapToolStripMenuItem.AccessibleName = null;
			resources.ApplyResources(this.createMinimapToolStripMenuItem, "createMinimapToolStripMenuItem");
			this.createMinimapToolStripMenuItem.BackgroundImage = null;
			this.createMinimapToolStripMenuItem.Name = "createMinimapToolStripMenuItem";
			this.createMinimapToolStripMenuItem.ShortcutKeyDisplayString = null;
			this.createMinimapToolStripMenuItem.Tag = "toggle_create_minimap";
			this.autoFocusToolStripMenuItem.AccessibleDescription = null;
			this.autoFocusToolStripMenuItem.AccessibleName = null;
			resources.ApplyResources(this.autoFocusToolStripMenuItem, "autoFocusToolStripMenuItem");
			this.autoFocusToolStripMenuItem.BackgroundImage = null;
			this.autoFocusToolStripMenuItem.Name = "autoFocusToolStripMenuItem";
			this.autoFocusToolStripMenuItem.ShortcutKeyDisplayString = null;
			this.autoFocusToolStripMenuItem.Tag = "toggle_auto_focus";
			this.blockEditingToolStripMenuItem.AccessibleDescription = null;
			this.blockEditingToolStripMenuItem.AccessibleName = null;
			resources.ApplyResources(this.blockEditingToolStripMenuItem, "blockEditingToolStripMenuItem");
			this.blockEditingToolStripMenuItem.BackgroundImage = null;
			this.blockEditingToolStripMenuItem.Name = "blockEditingToolStripMenuItem";
			this.blockEditingToolStripMenuItem.ShortcutKeyDisplayString = null;
			this.blockEditingToolStripMenuItem.Tag = "toggle_block_editing";
			this.menuHelp.AccessibleDescription = null;
			this.menuHelp.AccessibleName = null;
			resources.ApplyResources(this.menuHelp, "menuHelp");
			this.menuHelp.BackgroundImage = null;
			this.menuHelp.DropDownItems.AddRange(new global::System.Windows.Forms.ToolStripItem[]
			{
				this.menuItemHelpAbout
			});
			this.menuHelp.Name = "menuHelp";
			this.menuHelp.ShortcutKeyDisplayString = null;
			this.menuItemHelpAbout.AccessibleDescription = null;
			this.menuItemHelpAbout.AccessibleName = null;
			resources.ApplyResources(this.menuItemHelpAbout, "menuItemHelpAbout");
			this.menuItemHelpAbout.BackgroundImage = null;
			this.menuItemHelpAbout.Name = "menuItemHelpAbout";
			this.menuItemHelpAbout.ShortcutKeyDisplayString = null;
			this.menuItemHelpAbout.Tag = "about";
			this.MainToolStrip.AccessibleDescription = null;
			this.MainToolStrip.AccessibleName = null;
			resources.ApplyResources(this.MainToolStrip, "MainToolStrip");
			this.MainToolStrip.BackgroundImage = null;
			this.MainToolStrip.Font = null;
			this.MainToolStrip.Items.AddRange(new global::System.Windows.Forms.ToolStripItem[]
			{
				this.toolbarItemOpenMap,
				this.toolbarItemSaveMap,
				this.toolbarItemSeparator00,
				this.toolbarItemUndo,
				this.toolbarItemRedo,
				this.toolbarItemSeparator01,
				this.toolbarItemCut,
				this.toolbarItemCopy,
				this.toolbarItemPaste,
				this.toolbarItemDelete,
				this.toolbarItemSeparator02,
				this.toolbarItemLink,
				this.toolbarItemUnlink,
				this.toolbarItemSeparator03,
				this.toolbarItemMeasureDistance,
				this.toolbarItemFindButton,
				this.toolbarItemSeparator04,
				this.toolbarItemMoveWidget,
				this.toolbarItemRotateWidget,
				this.toolbarItemScaleWidget,
				this.toolbarItemObjectOriented,
				this.toolbarItemSeparator05,
				this.toolbarItemAlongTerrain,
				this.toolbarItemAlongWater,
				this.toolbarItemAlongNearest,
				this.toolbarItemAlignToGrid,
				this.toolbarItemGridStep,
				this.toolbarItemPlaceAlongNormal,
				this.toolbarItemSeparator06,
				this.toolbarItemLockSelection,
				this.toolbarItemPlaceOnlyOneObject,
				this.toolbarItemAutolinkAfterAdd,
				this.toolbarItemCreateCrosslinks,
				this.toolbarItemSeparator07,
				this.toolbarItemCameraSlow,
				this.toolbarItemCameraNormal,
				this.toolbarItemCameraFast,
				this.toolbarItemCameraCustom,
				this.toolbarItemSeparator08,
				this.toolbarItemCameraMove,
				this.toolbarItemCameraReset,
				this.toolbarItemCameraBounding,
				this.toolbarItemSeparator09,
				this.toolbarItemCameraCustomSetSpeed,
				this.toolbarItemCameraFOV,
				this.toolbarItemTimeForm,
				this.toolbarItemGenerateShadows,
				this.toolbarItemSeparator13,
				this.toolbarItemToggleAllObjects,
				this.toolbarItemSky,
				this.toolbarItemFog,
				this.toolbarItemWorldCutSphere,
				this.toolbarItemPassability,
				this.toolbarItemZones,
				this.toolbarItemDynamicStatistic,
				this.toolbarItemSeparator15,
				this.toolbarItemTerrainGrid,
				this.toolbarItemBottomGrid,
				this.toolbarItemCollisionGeometry,
				this.toolbarItemWireframe,
				this.toolbarItemLinkUserGeometry,
				this.toolbarItemRoutePointsGeometry,
				this.toolbarItemRouteObjects,
				this.toolbarItemSpawnPointsGeometry,
				this.toolbarItemScriptAreasGeometry,
				this.toolbarItemAstralBordersGeometry,
				this.toolbarItemZoneLocatorsGeometry,
				this.toolbarItemAggroRadius,
				this.toolbarItemProjectileVisObjects
			});
			this.MainToolStrip.Name = "MainToolStrip";
			this.toolbarItemOpenMap.AccessibleDescription = null;
			this.toolbarItemOpenMap.AccessibleName = null;
			resources.ApplyResources(this.toolbarItemOpenMap, "toolbarItemOpenMap");
			this.toolbarItemOpenMap.BackgroundImage = null;
			this.toolbarItemOpenMap.DisplayStyle = global::System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.toolbarItemOpenMap.Image = global::MapEditor.Properties.Resources.open;
			this.toolbarItemOpenMap.Name = "toolbarItemOpenMap";
			this.toolbarItemOpenMap.Tag = "open_map";
			this.toolbarItemSaveMap.AccessibleDescription = null;
			this.toolbarItemSaveMap.AccessibleName = null;
			resources.ApplyResources(this.toolbarItemSaveMap, "toolbarItemSaveMap");
			this.toolbarItemSaveMap.BackgroundImage = null;
			this.toolbarItemSaveMap.DisplayStyle = global::System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.toolbarItemSaveMap.Image = global::MapEditor.Properties.Resources.save;
			this.toolbarItemSaveMap.Name = "toolbarItemSaveMap";
			this.toolbarItemSaveMap.Tag = "save_map";
			this.toolbarItemSeparator00.AccessibleDescription = null;
			this.toolbarItemSeparator00.AccessibleName = null;
			resources.ApplyResources(this.toolbarItemSeparator00, "toolbarItemSeparator00");
			this.toolbarItemSeparator00.Name = "toolbarItemSeparator00";
			this.toolbarItemUndo.AccessibleDescription = null;
			this.toolbarItemUndo.AccessibleName = null;
			resources.ApplyResources(this.toolbarItemUndo, "toolbarItemUndo");
			this.toolbarItemUndo.BackgroundImage = null;
			this.toolbarItemUndo.DisplayStyle = global::System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.toolbarItemUndo.Image = global::MapEditor.Properties.Resources.undo;
			this.toolbarItemUndo.Name = "toolbarItemUndo";
			this.toolbarItemUndo.Tag = "undo";
			this.toolbarItemRedo.AccessibleDescription = null;
			this.toolbarItemRedo.AccessibleName = null;
			resources.ApplyResources(this.toolbarItemRedo, "toolbarItemRedo");
			this.toolbarItemRedo.BackgroundImage = null;
			this.toolbarItemRedo.DisplayStyle = global::System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.toolbarItemRedo.Image = global::MapEditor.Properties.Resources.redo;
			this.toolbarItemRedo.Name = "toolbarItemRedo";
			this.toolbarItemRedo.Tag = "redo";
			this.toolbarItemSeparator01.AccessibleDescription = null;
			this.toolbarItemSeparator01.AccessibleName = null;
			resources.ApplyResources(this.toolbarItemSeparator01, "toolbarItemSeparator01");
			this.toolbarItemSeparator01.Name = "toolbarItemSeparator01";
			this.toolbarItemCut.AccessibleDescription = null;
			this.toolbarItemCut.AccessibleName = null;
			resources.ApplyResources(this.toolbarItemCut, "toolbarItemCut");
			this.toolbarItemCut.BackgroundImage = null;
			this.toolbarItemCut.DisplayStyle = global::System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.toolbarItemCut.Image = global::MapEditor.Properties.Resources.cut;
			this.toolbarItemCut.Name = "toolbarItemCut";
			this.toolbarItemCut.Tag = "cut";
			this.toolbarItemCopy.AccessibleDescription = null;
			this.toolbarItemCopy.AccessibleName = null;
			resources.ApplyResources(this.toolbarItemCopy, "toolbarItemCopy");
			this.toolbarItemCopy.BackgroundImage = null;
			this.toolbarItemCopy.DisplayStyle = global::System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.toolbarItemCopy.Image = global::MapEditor.Properties.Resources.copy;
			this.toolbarItemCopy.Name = "toolbarItemCopy";
			this.toolbarItemCopy.Tag = "copy";
			this.toolbarItemPaste.AccessibleDescription = null;
			this.toolbarItemPaste.AccessibleName = null;
			resources.ApplyResources(this.toolbarItemPaste, "toolbarItemPaste");
			this.toolbarItemPaste.BackgroundImage = null;
			this.toolbarItemPaste.DisplayStyle = global::System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.toolbarItemPaste.Image = global::MapEditor.Properties.Resources.paste;
			this.toolbarItemPaste.Name = "toolbarItemPaste";
			this.toolbarItemPaste.Tag = "paste";
			this.toolbarItemDelete.AccessibleDescription = null;
			this.toolbarItemDelete.AccessibleName = null;
			resources.ApplyResources(this.toolbarItemDelete, "toolbarItemDelete");
			this.toolbarItemDelete.BackgroundImage = null;
			this.toolbarItemDelete.DisplayStyle = global::System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.toolbarItemDelete.Image = global::MapEditor.Properties.Resources.delete;
			this.toolbarItemDelete.Name = "toolbarItemDelete";
			this.toolbarItemDelete.Tag = "delete";
			this.toolbarItemSeparator02.AccessibleDescription = null;
			this.toolbarItemSeparator02.AccessibleName = null;
			resources.ApplyResources(this.toolbarItemSeparator02, "toolbarItemSeparator02");
			this.toolbarItemSeparator02.Name = "toolbarItemSeparator02";
			this.toolbarItemLink.AccessibleDescription = null;
			this.toolbarItemLink.AccessibleName = null;
			resources.ApplyResources(this.toolbarItemLink, "toolbarItemLink");
			this.toolbarItemLink.BackgroundImage = null;
			this.toolbarItemLink.DisplayStyle = global::System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.toolbarItemLink.Image = global::MapEditor.Properties.Resources.link;
			this.toolbarItemLink.Name = "toolbarItemLink";
			this.toolbarItemLink.Tag = "link";
			this.toolbarItemUnlink.AccessibleDescription = null;
			this.toolbarItemUnlink.AccessibleName = null;
			resources.ApplyResources(this.toolbarItemUnlink, "toolbarItemUnlink");
			this.toolbarItemUnlink.BackgroundImage = null;
			this.toolbarItemUnlink.DisplayStyle = global::System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.toolbarItemUnlink.Image = global::MapEditor.Properties.Resources.unlink;
			this.toolbarItemUnlink.Name = "toolbarItemUnlink";
			this.toolbarItemUnlink.Tag = "unlink";
			this.toolbarItemSeparator03.AccessibleDescription = null;
			this.toolbarItemSeparator03.AccessibleName = null;
			resources.ApplyResources(this.toolbarItemSeparator03, "toolbarItemSeparator03");
			this.toolbarItemSeparator03.Name = "toolbarItemSeparator03";
			this.toolbarItemMeasureDistance.AccessibleDescription = null;
			this.toolbarItemMeasureDistance.AccessibleName = null;
			resources.ApplyResources(this.toolbarItemMeasureDistance, "toolbarItemMeasureDistance");
			this.toolbarItemMeasureDistance.BackgroundImage = null;
			this.toolbarItemMeasureDistance.DisplayStyle = global::System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.toolbarItemMeasureDistance.Image = global::MapEditor.Properties.Resources.measure_distance;
			this.toolbarItemMeasureDistance.Name = "toolbarItemMeasureDistance";
			this.toolbarItemMeasureDistance.Tag = "toggle_measure_distance";
			this.toolbarItemFindButton.AccessibleDescription = null;
			this.toolbarItemFindButton.AccessibleName = null;
			resources.ApplyResources(this.toolbarItemFindButton, "toolbarItemFindButton");
			this.toolbarItemFindButton.BackgroundImage = null;
			this.toolbarItemFindButton.DisplayStyle = global::System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.toolbarItemFindButton.Image = global::MapEditor.Properties.Resources.find;
			this.toolbarItemFindButton.Name = "toolbarItemFindButton";
			this.toolbarItemFindButton.Tag = "find";
			this.toolbarItemSeparator04.AccessibleDescription = null;
			this.toolbarItemSeparator04.AccessibleName = null;
			resources.ApplyResources(this.toolbarItemSeparator04, "toolbarItemSeparator04");
			this.toolbarItemSeparator04.Name = "toolbarItemSeparator04";
			this.toolbarItemMoveWidget.AccessibleDescription = null;
			this.toolbarItemMoveWidget.AccessibleName = null;
			resources.ApplyResources(this.toolbarItemMoveWidget, "toolbarItemMoveWidget");
			this.toolbarItemMoveWidget.BackgroundImage = null;
			this.toolbarItemMoveWidget.DisplayStyle = global::System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.toolbarItemMoveWidget.Image = global::MapEditor.Properties.Resources.move_widget;
			this.toolbarItemMoveWidget.Name = "toolbarItemMoveWidget";
			this.toolbarItemMoveWidget.Tag = "selector_move";
			this.toolbarItemRotateWidget.AccessibleDescription = null;
			this.toolbarItemRotateWidget.AccessibleName = null;
			resources.ApplyResources(this.toolbarItemRotateWidget, "toolbarItemRotateWidget");
			this.toolbarItemRotateWidget.BackgroundImage = null;
			this.toolbarItemRotateWidget.DisplayStyle = global::System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.toolbarItemRotateWidget.Image = global::MapEditor.Properties.Resources.rotate_widget;
			this.toolbarItemRotateWidget.Name = "toolbarItemRotateWidget";
			this.toolbarItemRotateWidget.Tag = "selector_rotate";
			this.toolbarItemScaleWidget.AccessibleDescription = null;
			this.toolbarItemScaleWidget.AccessibleName = null;
			resources.ApplyResources(this.toolbarItemScaleWidget, "toolbarItemScaleWidget");
			this.toolbarItemScaleWidget.BackgroundImage = null;
			this.toolbarItemScaleWidget.DisplayStyle = global::System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.toolbarItemScaleWidget.Image = global::MapEditor.Properties.Resources.scale_widget;
			this.toolbarItemScaleWidget.Name = "toolbarItemScaleWidget";
			this.toolbarItemScaleWidget.Tag = "selector_scale";
			this.toolbarItemObjectOriented.AccessibleDescription = null;
			this.toolbarItemObjectOriented.AccessibleName = null;
			resources.ApplyResources(this.toolbarItemObjectOriented, "toolbarItemObjectOriented");
			this.toolbarItemObjectOriented.BackgroundImage = null;
			this.toolbarItemObjectOriented.DisplayStyle = global::System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.toolbarItemObjectOriented.Image = global::MapEditor.Properties.Resources.object_oriented;
			this.toolbarItemObjectOriented.Name = "toolbarItemObjectOriented";
			this.toolbarItemObjectOriented.Tag = "selector_object_oriented";
			this.toolbarItemSeparator05.AccessibleDescription = null;
			this.toolbarItemSeparator05.AccessibleName = null;
			resources.ApplyResources(this.toolbarItemSeparator05, "toolbarItemSeparator05");
			this.toolbarItemSeparator05.Name = "toolbarItemSeparator05";
			this.toolbarItemAlongTerrain.AccessibleDescription = null;
			this.toolbarItemAlongTerrain.AccessibleName = null;
			resources.ApplyResources(this.toolbarItemAlongTerrain, "toolbarItemAlongTerrain");
			this.toolbarItemAlongTerrain.BackgroundImage = null;
			this.toolbarItemAlongTerrain.DisplayStyle = global::System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.toolbarItemAlongTerrain.Image = global::MapEditor.Properties.Resources.along_terrain_flat;
			this.toolbarItemAlongTerrain.Name = "toolbarItemAlongTerrain";
			this.toolbarItemAlongTerrain.Tag = "selector_along_terrain";
			this.toolbarItemAlongWater.AccessibleDescription = null;
			this.toolbarItemAlongWater.AccessibleName = null;
			resources.ApplyResources(this.toolbarItemAlongWater, "toolbarItemAlongWater");
			this.toolbarItemAlongWater.BackgroundImage = null;
			this.toolbarItemAlongWater.DisplayStyle = global::System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.toolbarItemAlongWater.Image = global::MapEditor.Properties.Resources.along_water;
			this.toolbarItemAlongWater.Name = "toolbarItemAlongWater";
			this.toolbarItemAlongWater.Tag = "selector_along_water";
			this.toolbarItemAlongNearest.AccessibleDescription = null;
			this.toolbarItemAlongNearest.AccessibleName = null;
			resources.ApplyResources(this.toolbarItemAlongNearest, "toolbarItemAlongNearest");
			this.toolbarItemAlongNearest.BackgroundImage = null;
			this.toolbarItemAlongNearest.DisplayStyle = global::System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.toolbarItemAlongNearest.Image = global::MapEditor.Properties.Resources.along_nearest;
			this.toolbarItemAlongNearest.Name = "toolbarItemAlongNearest";
			this.toolbarItemAlongNearest.Tag = "selector_along_object";
			this.toolbarItemAlignToGrid.AccessibleDescription = null;
			this.toolbarItemAlignToGrid.AccessibleName = null;
			resources.ApplyResources(this.toolbarItemAlignToGrid, "toolbarItemAlignToGrid");
			this.toolbarItemAlignToGrid.BackgroundImage = null;
			this.toolbarItemAlignToGrid.DisplayStyle = global::System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.toolbarItemAlignToGrid.Image = global::MapEditor.Properties.Resources.align_to_grid;
			this.toolbarItemAlignToGrid.Name = "toolbarItemAlignToGrid";
			this.toolbarItemAlignToGrid.Tag = "selector_align_to_grid";
			this.toolbarItemGridStep.AccessibleDescription = null;
			this.toolbarItemGridStep.AccessibleName = null;
			resources.ApplyResources(this.toolbarItemGridStep, "toolbarItemGridStep");
			this.toolbarItemGridStep.BackgroundImage = null;
			this.toolbarItemGridStep.DisplayStyle = global::System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.toolbarItemGridStep.Image = global::MapEditor.Properties.Resources.grid_step;
			this.toolbarItemGridStep.Name = "toolbarItemGridStep";
			this.toolbarItemGridStep.Tag = "selector_grid_step";
			this.toolbarItemPlaceAlongNormal.AccessibleDescription = null;
			this.toolbarItemPlaceAlongNormal.AccessibleName = null;
			resources.ApplyResources(this.toolbarItemPlaceAlongNormal, "toolbarItemPlaceAlongNormal");
			this.toolbarItemPlaceAlongNormal.BackgroundImage = null;
			this.toolbarItemPlaceAlongNormal.DisplayStyle = global::System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.toolbarItemPlaceAlongNormal.Image = global::MapEditor.Properties.Resources.along_normal;
			this.toolbarItemPlaceAlongNormal.Name = "toolbarItemPlaceAlongNormal";
			this.toolbarItemPlaceAlongNormal.Tag = "selector_place_along_normal";
			this.toolbarItemSeparator06.AccessibleDescription = null;
			this.toolbarItemSeparator06.AccessibleName = null;
			resources.ApplyResources(this.toolbarItemSeparator06, "toolbarItemSeparator06");
			this.toolbarItemSeparator06.Name = "toolbarItemSeparator06";
			this.toolbarItemLockSelection.AccessibleDescription = null;
			this.toolbarItemLockSelection.AccessibleName = null;
			resources.ApplyResources(this.toolbarItemLockSelection, "toolbarItemLockSelection");
			this.toolbarItemLockSelection.BackgroundImage = null;
			this.toolbarItemLockSelection.DisplayStyle = global::System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.toolbarItemLockSelection.Image = global::MapEditor.Properties.Resources.lock_selection;
			this.toolbarItemLockSelection.Name = "toolbarItemLockSelection";
			this.toolbarItemLockSelection.Tag = "selector_lock_selection";
			this.toolbarItemPlaceOnlyOneObject.AccessibleDescription = null;
			this.toolbarItemPlaceOnlyOneObject.AccessibleName = null;
			resources.ApplyResources(this.toolbarItemPlaceOnlyOneObject, "toolbarItemPlaceOnlyOneObject");
			this.toolbarItemPlaceOnlyOneObject.BackgroundImage = null;
			this.toolbarItemPlaceOnlyOneObject.DisplayStyle = global::System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.toolbarItemPlaceOnlyOneObject.Image = global::MapEditor.Properties.Resources.edit_object_after_add;
			this.toolbarItemPlaceOnlyOneObject.Name = "toolbarItemPlaceOnlyOneObject";
			this.toolbarItemPlaceOnlyOneObject.Tag = "selector_edit_object_after_add";
			this.toolbarItemAutolinkAfterAdd.AccessibleDescription = null;
			this.toolbarItemAutolinkAfterAdd.AccessibleName = null;
			resources.ApplyResources(this.toolbarItemAutolinkAfterAdd, "toolbarItemAutolinkAfterAdd");
			this.toolbarItemAutolinkAfterAdd.BackgroundImage = null;
			this.toolbarItemAutolinkAfterAdd.DisplayStyle = global::System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.toolbarItemAutolinkAfterAdd.Image = global::MapEditor.Properties.Resources.link_automatically;
			this.toolbarItemAutolinkAfterAdd.Name = "toolbarItemAutolinkAfterAdd";
			this.toolbarItemAutolinkAfterAdd.Tag = "selector_autolink_after_add";
			this.toolbarItemCreateCrosslinks.AccessibleDescription = null;
			this.toolbarItemCreateCrosslinks.AccessibleName = null;
			resources.ApplyResources(this.toolbarItemCreateCrosslinks, "toolbarItemCreateCrosslinks");
			this.toolbarItemCreateCrosslinks.BackgroundImage = null;
			this.toolbarItemCreateCrosslinks.DisplayStyle = global::System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.toolbarItemCreateCrosslinks.Image = global::MapEditor.Properties.Resources.link_copied;
			this.toolbarItemCreateCrosslinks.Name = "toolbarItemCreateCrosslinks";
			this.toolbarItemCreateCrosslinks.Tag = "selector_create_crosslinks";
			this.toolbarItemSeparator07.AccessibleDescription = null;
			this.toolbarItemSeparator07.AccessibleName = null;
			resources.ApplyResources(this.toolbarItemSeparator07, "toolbarItemSeparator07");
			this.toolbarItemSeparator07.Name = "toolbarItemSeparator07";
			this.toolbarItemCameraSlow.AccessibleDescription = null;
			this.toolbarItemCameraSlow.AccessibleName = null;
			resources.ApplyResources(this.toolbarItemCameraSlow, "toolbarItemCameraSlow");
			this.toolbarItemCameraSlow.BackgroundImage = null;
			this.toolbarItemCameraSlow.DisplayStyle = global::System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.toolbarItemCameraSlow.Image = global::MapEditor.Properties.Resources.camera_slow;
			this.toolbarItemCameraSlow.Name = "toolbarItemCameraSlow";
			this.toolbarItemCameraSlow.Tag = "camera_slow";
			this.toolbarItemCameraNormal.AccessibleDescription = null;
			this.toolbarItemCameraNormal.AccessibleName = null;
			resources.ApplyResources(this.toolbarItemCameraNormal, "toolbarItemCameraNormal");
			this.toolbarItemCameraNormal.BackgroundImage = null;
			this.toolbarItemCameraNormal.DisplayStyle = global::System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.toolbarItemCameraNormal.Image = global::MapEditor.Properties.Resources.camera_normal;
			this.toolbarItemCameraNormal.Name = "toolbarItemCameraNormal";
			this.toolbarItemCameraNormal.Tag = "camera_normal";
			this.toolbarItemCameraFast.AccessibleDescription = null;
			this.toolbarItemCameraFast.AccessibleName = null;
			resources.ApplyResources(this.toolbarItemCameraFast, "toolbarItemCameraFast");
			this.toolbarItemCameraFast.BackgroundImage = null;
			this.toolbarItemCameraFast.DisplayStyle = global::System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.toolbarItemCameraFast.Image = global::MapEditor.Properties.Resources.camera_fast;
			this.toolbarItemCameraFast.Name = "toolbarItemCameraFast";
			this.toolbarItemCameraFast.Tag = "camera_fast";
			this.toolbarItemCameraCustom.AccessibleDescription = null;
			this.toolbarItemCameraCustom.AccessibleName = null;
			resources.ApplyResources(this.toolbarItemCameraCustom, "toolbarItemCameraCustom");
			this.toolbarItemCameraCustom.BackgroundImage = null;
			this.toolbarItemCameraCustom.DisplayStyle = global::System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.toolbarItemCameraCustom.Image = global::MapEditor.Properties.Resources.camera_custom;
			this.toolbarItemCameraCustom.Name = "toolbarItemCameraCustom";
			this.toolbarItemCameraCustom.Tag = "camera_custom";
			this.toolbarItemSeparator08.AccessibleDescription = null;
			this.toolbarItemSeparator08.AccessibleName = null;
			resources.ApplyResources(this.toolbarItemSeparator08, "toolbarItemSeparator08");
			this.toolbarItemSeparator08.Name = "toolbarItemSeparator08";
			this.toolbarItemCameraMove.AccessibleDescription = null;
			this.toolbarItemCameraMove.AccessibleName = null;
			resources.ApplyResources(this.toolbarItemCameraMove, "toolbarItemCameraMove");
			this.toolbarItemCameraMove.BackgroundImage = null;
			this.toolbarItemCameraMove.DisplayStyle = global::System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.toolbarItemCameraMove.Image = global::MapEditor.Properties.Resources.camera_move;
			this.toolbarItemCameraMove.Name = "toolbarItemCameraMove";
			this.toolbarItemCameraMove.Tag = "camera_move";
			this.toolbarItemCameraReset.AccessibleDescription = null;
			this.toolbarItemCameraReset.AccessibleName = null;
			resources.ApplyResources(this.toolbarItemCameraReset, "toolbarItemCameraReset");
			this.toolbarItemCameraReset.BackgroundImage = null;
			this.toolbarItemCameraReset.DisplayStyle = global::System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.toolbarItemCameraReset.Image = global::MapEditor.Properties.Resources.camera_reset;
			this.toolbarItemCameraReset.Name = "toolbarItemCameraReset";
			this.toolbarItemCameraReset.Tag = "camera_reset";
			this.toolbarItemCameraBounding.AccessibleDescription = null;
			this.toolbarItemCameraBounding.AccessibleName = null;
			resources.ApplyResources(this.toolbarItemCameraBounding, "toolbarItemCameraBounding");
			this.toolbarItemCameraBounding.BackgroundImage = null;
			this.toolbarItemCameraBounding.DisplayStyle = global::System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.toolbarItemCameraBounding.Image = global::MapEditor.Properties.Resources.camera_bounding;
			this.toolbarItemCameraBounding.Name = "toolbarItemCameraBounding";
			this.toolbarItemCameraBounding.Tag = "camera_bounding";
			this.toolbarItemSeparator09.AccessibleDescription = null;
			this.toolbarItemSeparator09.AccessibleName = null;
			resources.ApplyResources(this.toolbarItemSeparator09, "toolbarItemSeparator09");
			this.toolbarItemSeparator09.Name = "toolbarItemSeparator09";
			this.toolbarItemCameraCustomSetSpeed.AccessibleDescription = null;
			this.toolbarItemCameraCustomSetSpeed.AccessibleName = null;
			resources.ApplyResources(this.toolbarItemCameraCustomSetSpeed, "toolbarItemCameraCustomSetSpeed");
			this.toolbarItemCameraCustomSetSpeed.BackgroundImage = null;
			this.toolbarItemCameraCustomSetSpeed.DisplayStyle = global::System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.toolbarItemCameraCustomSetSpeed.Image = global::MapEditor.Properties.Resources.camera_set_custom_speed;
			this.toolbarItemCameraCustomSetSpeed.Name = "toolbarItemCameraCustomSetSpeed";
			this.toolbarItemCameraCustomSetSpeed.Tag = "camera_speed";
			this.toolbarItemCameraFOV.AccessibleDescription = null;
			this.toolbarItemCameraFOV.AccessibleName = null;
			resources.ApplyResources(this.toolbarItemCameraFOV, "toolbarItemCameraFOV");
			this.toolbarItemCameraFOV.BackgroundImage = null;
			this.toolbarItemCameraFOV.DisplayStyle = global::System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.toolbarItemCameraFOV.Image = global::MapEditor.Properties.Resources.camera_fov;
			this.toolbarItemCameraFOV.Name = "toolbarItemCameraFOV";
			this.toolbarItemCameraFOV.Tag = "camera_fov";
			this.toolbarItemTimeForm.AccessibleDescription = null;
			this.toolbarItemTimeForm.AccessibleName = null;
			resources.ApplyResources(this.toolbarItemTimeForm, "toolbarItemTimeForm");
			this.toolbarItemTimeForm.BackgroundImage = null;
			this.toolbarItemTimeForm.DisplayStyle = global::System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.toolbarItemTimeForm.Image = global::MapEditor.Properties.Resources.time;
			this.toolbarItemTimeForm.Name = "toolbarItemTimeForm";
			this.toolbarItemTimeForm.Tag = "toggle_time";
			this.toolbarItemGenerateShadows.AccessibleDescription = null;
			this.toolbarItemGenerateShadows.AccessibleName = null;
			resources.ApplyResources(this.toolbarItemGenerateShadows, "toolbarItemGenerateShadows");
			this.toolbarItemGenerateShadows.BackgroundImage = null;
			this.toolbarItemGenerateShadows.DisplayStyle = global::System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.toolbarItemGenerateShadows.Image = global::MapEditor.Properties.Resources.generate_shadows;
			this.toolbarItemGenerateShadows.Name = "toolbarItemGenerateShadows";
			this.toolbarItemGenerateShadows.Tag = "generate_shadows";
			this.toolbarItemSeparator13.AccessibleDescription = null;
			this.toolbarItemSeparator13.AccessibleName = null;
			resources.ApplyResources(this.toolbarItemSeparator13, "toolbarItemSeparator13");
			this.toolbarItemSeparator13.Name = "toolbarItemSeparator13";
			this.toolbarItemToggleAllObjects.AccessibleDescription = null;
			this.toolbarItemToggleAllObjects.AccessibleName = null;
			resources.ApplyResources(this.toolbarItemToggleAllObjects, "toolbarItemToggleAllObjects");
			this.toolbarItemToggleAllObjects.BackgroundImage = null;
			this.toolbarItemToggleAllObjects.DisplayStyle = global::System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.toolbarItemToggleAllObjects.Image = global::MapEditor.Properties.Resources.all_objects;
			this.toolbarItemToggleAllObjects.Name = "toolbarItemToggleAllObjects";
			this.toolbarItemToggleAllObjects.Tag = "toggle_all_objects_visible";
			this.toolbarItemSky.AccessibleDescription = null;
			this.toolbarItemSky.AccessibleName = null;
			resources.ApplyResources(this.toolbarItemSky, "toolbarItemSky");
			this.toolbarItemSky.BackgroundImage = null;
			this.toolbarItemSky.DisplayStyle = global::System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.toolbarItemSky.Image = global::MapEditor.Properties.Resources.sky;
			this.toolbarItemSky.Name = "toolbarItemSky";
			this.toolbarItemSky.Tag = "toggle_sky";
			this.toolbarItemFog.AccessibleDescription = null;
			this.toolbarItemFog.AccessibleName = null;
			resources.ApplyResources(this.toolbarItemFog, "toolbarItemFog");
			this.toolbarItemFog.BackgroundImage = null;
			this.toolbarItemFog.DisplayStyle = global::System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.toolbarItemFog.Image = global::MapEditor.Properties.Resources.fog;
			this.toolbarItemFog.Name = "toolbarItemFog";
			this.toolbarItemFog.Tag = "toggle_fog";
			this.toolbarItemPassability.AccessibleDescription = null;
			this.toolbarItemPassability.AccessibleName = null;
			resources.ApplyResources(this.toolbarItemPassability, "toolbarItemPassability");
			this.toolbarItemPassability.BackgroundImage = null;
			this.toolbarItemPassability.DisplayStyle = global::System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.toolbarItemPassability.Image = global::MapEditor.Properties.Resources.passability;
			this.toolbarItemPassability.Name = "toolbarItemPassability";
			this.toolbarItemPassability.Tag = "toggle_passability";
			this.toolbarItemZones.AccessibleDescription = null;
			this.toolbarItemZones.AccessibleName = null;
			resources.ApplyResources(this.toolbarItemZones, "toolbarItemZones");
			this.toolbarItemZones.BackgroundImage = null;
			this.toolbarItemZones.DisplayStyle = global::System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.toolbarItemZones.Image = global::MapEditor.Properties.Resources.zones;
			this.toolbarItemZones.Name = "toolbarItemZones";
			this.toolbarItemZones.Tag = "toggle_zones";
			this.toolbarItemDynamicStatistic.AccessibleDescription = null;
			this.toolbarItemDynamicStatistic.AccessibleName = null;
			resources.ApplyResources(this.toolbarItemDynamicStatistic, "toolbarItemDynamicStatistic");
			this.toolbarItemDynamicStatistic.BackgroundImage = null;
			this.toolbarItemDynamicStatistic.DisplayStyle = global::System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.toolbarItemDynamicStatistic.Image = global::MapEditor.Properties.Resources.dynamic_statistic;
			this.toolbarItemDynamicStatistic.Name = "toolbarItemDynamicStatistic";
			this.toolbarItemDynamicStatistic.Tag = "toggle_dynamic_statistic";
			this.toolbarItemSeparator15.AccessibleDescription = null;
			this.toolbarItemSeparator15.AccessibleName = null;
			resources.ApplyResources(this.toolbarItemSeparator15, "toolbarItemSeparator15");
			this.toolbarItemSeparator15.Name = "toolbarItemSeparator15";
			this.toolbarItemTerrainGrid.AccessibleDescription = null;
			this.toolbarItemTerrainGrid.AccessibleName = null;
			resources.ApplyResources(this.toolbarItemTerrainGrid, "toolbarItemTerrainGrid");
			this.toolbarItemTerrainGrid.BackgroundImage = null;
			this.toolbarItemTerrainGrid.DisplayStyle = global::System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.toolbarItemTerrainGrid.Image = global::MapEditor.Properties.Resources.terrain_grid;
			this.toolbarItemTerrainGrid.Name = "toolbarItemTerrainGrid";
			this.toolbarItemTerrainGrid.Tag = "toggle_terrain_grid";
			this.toolbarItemBottomGrid.AccessibleDescription = null;
			this.toolbarItemBottomGrid.AccessibleName = null;
			resources.ApplyResources(this.toolbarItemBottomGrid, "toolbarItemBottomGrid");
			this.toolbarItemBottomGrid.BackgroundImage = null;
			this.toolbarItemBottomGrid.DisplayStyle = global::System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.toolbarItemBottomGrid.Image = global::MapEditor.Properties.Resources.bottom_grid;
			this.toolbarItemBottomGrid.Name = "toolbarItemBottomGrid";
			this.toolbarItemBottomGrid.Tag = "toggle_bottom_grid";
			this.toolbarItemCollisionGeometry.AccessibleDescription = null;
			this.toolbarItemCollisionGeometry.AccessibleName = null;
			resources.ApplyResources(this.toolbarItemCollisionGeometry, "toolbarItemCollisionGeometry");
			this.toolbarItemCollisionGeometry.BackgroundImage = null;
			this.toolbarItemCollisionGeometry.DisplayStyle = global::System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.toolbarItemCollisionGeometry.Image = global::MapEditor.Properties.Resources.collision_geometry;
			this.toolbarItemCollisionGeometry.Name = "toolbarItemCollisionGeometry";
			this.toolbarItemCollisionGeometry.Tag = "toggle_collision_geometry";
			this.toolbarItemWireframe.AccessibleDescription = null;
			this.toolbarItemWireframe.AccessibleName = null;
			resources.ApplyResources(this.toolbarItemWireframe, "toolbarItemWireframe");
			this.toolbarItemWireframe.BackgroundImage = null;
			this.toolbarItemWireframe.DisplayStyle = global::System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.toolbarItemWireframe.Image = global::MapEditor.Properties.Resources.wireframe;
			this.toolbarItemWireframe.Name = "toolbarItemWireframe";
			this.toolbarItemWireframe.Tag = "toggle_wireframe";
			this.toolbarItemLinkUserGeometry.AccessibleDescription = null;
			this.toolbarItemLinkUserGeometry.AccessibleName = null;
			resources.ApplyResources(this.toolbarItemLinkUserGeometry, "toolbarItemLinkUserGeometry");
			this.toolbarItemLinkUserGeometry.BackgroundImage = null;
			this.toolbarItemLinkUserGeometry.DisplayStyle = global::System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.toolbarItemLinkUserGeometry.Image = global::MapEditor.Properties.Resources.link_widgets;
			this.toolbarItemLinkUserGeometry.Name = "toolbarItemLinkUserGeometry";
			this.toolbarItemLinkUserGeometry.Tag = "toggle_link_user_geometry";
			this.toolbarItemRoutePointsGeometry.AccessibleDescription = null;
			this.toolbarItemRoutePointsGeometry.AccessibleName = null;
			resources.ApplyResources(this.toolbarItemRoutePointsGeometry, "toolbarItemRoutePointsGeometry");
			this.toolbarItemRoutePointsGeometry.BackgroundImage = null;
			this.toolbarItemRoutePointsGeometry.DisplayStyle = global::System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.toolbarItemRoutePointsGeometry.Image = global::MapEditor.Properties.Resources.route_points;
			this.toolbarItemRoutePointsGeometry.Name = "toolbarItemRoutePointsGeometry";
			this.toolbarItemRoutePointsGeometry.Tag = "toggle_route_point_user_geometry";
			this.toolbarItemRouteObjects.AccessibleDescription = null;
			this.toolbarItemRouteObjects.AccessibleName = null;
			resources.ApplyResources(this.toolbarItemRouteObjects, "toolbarItemRouteObjects");
			this.toolbarItemRouteObjects.BackgroundImage = null;
			this.toolbarItemRouteObjects.DisplayStyle = global::System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.toolbarItemRouteObjects.Image = global::MapEditor.Properties.Resources.route_objects;
			this.toolbarItemRouteObjects.Name = "toolbarItemRouteObjects";
			this.toolbarItemRouteObjects.Tag = "toggle_route_objects";
			this.toolbarItemSpawnPointsGeometry.AccessibleDescription = null;
			this.toolbarItemSpawnPointsGeometry.AccessibleName = null;
			resources.ApplyResources(this.toolbarItemSpawnPointsGeometry, "toolbarItemSpawnPointsGeometry");
			this.toolbarItemSpawnPointsGeometry.BackgroundImage = null;
			this.toolbarItemSpawnPointsGeometry.DisplayStyle = global::System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.toolbarItemSpawnPointsGeometry.Image = global::MapEditor.Properties.Resources.spawn_point_widgets;
			this.toolbarItemSpawnPointsGeometry.Name = "toolbarItemSpawnPointsGeometry";
			this.toolbarItemSpawnPointsGeometry.Tag = "toggle_spawn_point_user_geometry";
			this.toolbarItemScriptAreasGeometry.AccessibleDescription = null;
			this.toolbarItemScriptAreasGeometry.AccessibleName = null;
			resources.ApplyResources(this.toolbarItemScriptAreasGeometry, "toolbarItemScriptAreasGeometry");
			this.toolbarItemScriptAreasGeometry.BackgroundImage = null;
			this.toolbarItemScriptAreasGeometry.DisplayStyle = global::System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.toolbarItemScriptAreasGeometry.Image = global::MapEditor.Properties.Resources.script_area_widgets;
			this.toolbarItemScriptAreasGeometry.Name = "toolbarItemScriptAreasGeometry";
			this.toolbarItemScriptAreasGeometry.Tag = "toggle_script_area_user_geometry";
			this.toolbarItemAstralBordersGeometry.AccessibleDescription = null;
			this.toolbarItemAstralBordersGeometry.AccessibleName = null;
			resources.ApplyResources(this.toolbarItemAstralBordersGeometry, "toolbarItemAstralBordersGeometry");
			this.toolbarItemAstralBordersGeometry.BackgroundImage = null;
			this.toolbarItemAstralBordersGeometry.DisplayStyle = global::System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.toolbarItemAstralBordersGeometry.Image = global::MapEditor.Properties.Resources.astral_borders;
			this.toolbarItemAstralBordersGeometry.Name = "toolbarItemAstralBordersGeometry";
			this.toolbarItemAstralBordersGeometry.Tag = "toggle_astral_border_user_geometry";
			this.toolbarItemZoneLocatorsGeometry.AccessibleDescription = null;
			this.toolbarItemZoneLocatorsGeometry.AccessibleName = null;
			resources.ApplyResources(this.toolbarItemZoneLocatorsGeometry, "toolbarItemZoneLocatorsGeometry");
			this.toolbarItemZoneLocatorsGeometry.BackgroundImage = null;
			this.toolbarItemZoneLocatorsGeometry.DisplayStyle = global::System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.toolbarItemZoneLocatorsGeometry.Image = global::MapEditor.Properties.Resources.zone_locators;
			this.toolbarItemZoneLocatorsGeometry.Name = "toolbarItemZoneLocatorsGeometry";
			this.toolbarItemZoneLocatorsGeometry.Tag = "toggle_zone_locator_user_geometry";
			this.toolbarItemAggroRadius.AccessibleDescription = null;
			this.toolbarItemAggroRadius.AccessibleName = null;
			resources.ApplyResources(this.toolbarItemAggroRadius, "toolbarItemAggroRadius");
			this.toolbarItemAggroRadius.BackgroundImage = null;
			this.toolbarItemAggroRadius.DisplayStyle = global::System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.toolbarItemAggroRadius.Image = global::MapEditor.Properties.Resources.mobs_aggro_radius;
			this.toolbarItemAggroRadius.Name = "toolbarItemAggroRadius";
			this.toolbarItemAggroRadius.Tag = "toggle_aggro_radius";
			this.toolbarItemProjectileVisObjects.AccessibleDescription = null;
			this.toolbarItemProjectileVisObjects.AccessibleName = null;
			resources.ApplyResources(this.toolbarItemProjectileVisObjects, "toolbarItemProjectileVisObjects");
			this.toolbarItemProjectileVisObjects.BackgroundImage = null;
			this.toolbarItemProjectileVisObjects.DisplayStyle = global::System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.toolbarItemProjectileVisObjects.Name = "toolbarItemProjectileVisObjects";
			this.toolbarItemProjectileVisObjects.Tag = "toggle_projectile_vis_objects";
			this.stepTimer.Tick += new global::System.EventHandler(this.stepTimer_Tick);
			this.saveNotificationTimer.Tick += new global::System.EventHandler(this.SaveNotificationTimer_Tick);
			this.StatusbarHelp.AccessibleDescription = null;
			this.StatusbarHelp.AccessibleName = null;
			resources.ApplyResources(this.StatusbarHelp, "StatusbarHelp");
			this.StatusbarHelp.BackgroundImage = null;
			this.StatusbarHelp.BorderSides = (global::System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | global::System.Windows.Forms.ToolStripStatusLabelBorderSides.Right | global::System.Windows.Forms.ToolStripStatusLabelBorderSides.Top);
			this.StatusbarHelp.BorderStyle = global::System.Windows.Forms.Border3DStyle.SunkenOuter;
			this.StatusbarHelp.Name = "StatusbarHelp";
			this.statusHelp.AccessibleDescription = null;
			this.statusHelp.AccessibleName = null;
			resources.ApplyResources(this.statusHelp, "statusHelp");
			this.statusHelp.BackgroundImage = null;
			this.statusHelp.Font = null;
			this.statusHelp.Items.AddRange(new global::System.Windows.Forms.ToolStripItem[]
			{
				this.StatusbarHelp
			});
			this.statusHelp.Name = "statusHelp";
			this.statusHelp.SizingGrip = false;
			this.ToolsToolStrip.AccessibleDescription = null;
			this.ToolsToolStrip.AccessibleName = null;
			resources.ApplyResources(this.ToolsToolStrip, "ToolsToolStrip");
			this.ToolsToolStrip.BackgroundImage = null;
			this.ToolsToolStrip.Font = null;
			this.ToolsToolStrip.Items.AddRange(new global::System.Windows.Forms.ToolStripItem[]
			{
				this.ServerConfigToolButton,
				this.toolbarItemGame,
				this.toolbarItemMinimap,
				this.toolbarItemTool,
				this.toolbarItemList,
				this.toolbarItemRouteObjectBrowser,
				this.toolbarItemObjectEditor,
				this.toolbarItemMultiObjectBrowser,
				this.toolStripButton1,
				this.toolbarItemRoadBrowser,
				this.toolbarItemProperties,
				this.toolbarItemCheckers,
				this.toolbarItemLog,
				this.toolbarItemSeparator10,
				this.toolbarItemLayers,
				this.toolbarItemLight,
				this.toolbarItemPropertyControl,
				this.toolbarItemModelViewer,
				this.toolbarItemScriptEditor,
				this.toolbarItemSeparator11,
				this.toolbarItemQuests,
				this.toolbarItemQuestDiagram,
				this.toolbarItemQuestTexts,
				this.toolbarItemSeparator12,
				this.toolbarItemCreateMob,
				this.toolbarItemCreateNPC,
				this.toolbarItemCreateResource,
				this.toolbarItemCreateQuestItem,
				this.toolStripSeparator2,
				this.toolStripButton2,
				this.toolbarItemSeparator14,
				this.toolbarItemAxisUserGeometry,
				this.toolbarItemDynamicObjectsFall,
				this.toolbarItemFixTerrainTiles,
				this.toolbarItemToggleSound
			});
			this.ToolsToolStrip.Name = "ToolsToolStrip";
			this.ServerConfigToolButton.AccessibleDescription = null;
			this.ServerConfigToolButton.AccessibleName = null;
			resources.ApplyResources(this.ServerConfigToolButton, "ServerConfigToolButton");
			this.ServerConfigToolButton.BackgroundImage = null;
			this.ServerConfigToolButton.DisplayStyle = global::System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.ServerConfigToolButton.Name = "ServerConfigToolButton";
			this.ServerConfigToolButton.Tag = "toggle_server_lunch";
			this.toolbarItemGame.AccessibleDescription = null;
			this.toolbarItemGame.AccessibleName = null;
			resources.ApplyResources(this.toolbarItemGame, "toolbarItemGame");
			this.toolbarItemGame.BackgroundImage = null;
			this.toolbarItemGame.DisplayStyle = global::System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.toolbarItemGame.Image = global::MapEditor.Properties.Resources.game;
			this.toolbarItemGame.Name = "toolbarItemGame";
			this.toolbarItemGame.Tag = "toggle_game";
			this.toolbarItemMinimap.AccessibleDescription = null;
			this.toolbarItemMinimap.AccessibleName = null;
			resources.ApplyResources(this.toolbarItemMinimap, "toolbarItemMinimap");
			this.toolbarItemMinimap.BackgroundImage = null;
			this.toolbarItemMinimap.DisplayStyle = global::System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.toolbarItemMinimap.Image = global::MapEditor.Properties.Resources.minimap;
			this.toolbarItemMinimap.Name = "toolbarItemMinimap";
			this.toolbarItemMinimap.Tag = "toggle_minimap";
			this.toolbarItemTool.AccessibleDescription = null;
			this.toolbarItemTool.AccessibleName = null;
			resources.ApplyResources(this.toolbarItemTool, "toolbarItemTool");
			this.toolbarItemTool.BackgroundImage = null;
			this.toolbarItemTool.DisplayStyle = global::System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.toolbarItemTool.Image = global::MapEditor.Properties.Resources.tools;
			this.toolbarItemTool.Name = "toolbarItemTool";
			this.toolbarItemTool.Tag = "toggle_tool";
			this.toolbarItemList.AccessibleDescription = null;
			this.toolbarItemList.AccessibleName = null;
			resources.ApplyResources(this.toolbarItemList, "toolbarItemList");
			this.toolbarItemList.BackgroundImage = null;
			this.toolbarItemList.DisplayStyle = global::System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.toolbarItemList.Image = global::MapEditor.Properties.Resources.list;
			this.toolbarItemList.Name = "toolbarItemList";
			this.toolbarItemList.Tag = "toggle_list";
			this.toolbarItemRouteObjectBrowser.AccessibleDescription = null;
			this.toolbarItemRouteObjectBrowser.AccessibleName = null;
			resources.ApplyResources(this.toolbarItemRouteObjectBrowser, "toolbarItemRouteObjectBrowser");
			this.toolbarItemRouteObjectBrowser.BackgroundImage = null;
			this.toolbarItemRouteObjectBrowser.DisplayStyle = global::System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.toolbarItemRouteObjectBrowser.Image = global::MapEditor.Properties.Resources.route_object_browser;
			this.toolbarItemRouteObjectBrowser.Name = "toolbarItemRouteObjectBrowser";
			this.toolbarItemRouteObjectBrowser.Tag = "toggle_route_object_browser";
			this.toolbarItemObjectEditor.AccessibleDescription = null;
			this.toolbarItemObjectEditor.AccessibleName = null;
			resources.ApplyResources(this.toolbarItemObjectEditor, "toolbarItemObjectEditor");
			this.toolbarItemObjectEditor.BackgroundImage = null;
			this.toolbarItemObjectEditor.DisplayStyle = global::System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.toolbarItemObjectEditor.Image = global::MapEditor.Properties.Resources.object_editor;
			this.toolbarItemObjectEditor.Name = "toolbarItemObjectEditor";
			this.toolbarItemObjectEditor.Tag = "toggle_object_editor";
			this.toolbarItemMultiObjectBrowser.AccessibleDescription = null;
			this.toolbarItemMultiObjectBrowser.AccessibleName = null;
			resources.ApplyResources(this.toolbarItemMultiObjectBrowser, "toolbarItemMultiObjectBrowser");
			this.toolbarItemMultiObjectBrowser.BackgroundImage = null;
			this.toolbarItemMultiObjectBrowser.DisplayStyle = global::System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.toolbarItemMultiObjectBrowser.Image = global::MapEditor.Properties.Resources.group;
			this.toolbarItemMultiObjectBrowser.Name = "toolbarItemMultiObjectBrowser";
			this.toolbarItemMultiObjectBrowser.Tag = "toggle_multiobject_browser";
			this.toolStripButton1.AccessibleDescription = null;
			this.toolStripButton1.AccessibleName = null;
			resources.ApplyResources(this.toolStripButton1, "toolStripButton1");
			this.toolStripButton1.BackgroundImage = null;
			this.toolStripButton1.DisplayStyle = global::System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.toolStripButton1.Name = "toolStripButton1";
			this.toolStripButton1.Tag = "toggle_object_set_browser";
			this.toolbarItemRoadBrowser.AccessibleDescription = null;
			this.toolbarItemRoadBrowser.AccessibleName = null;
			resources.ApplyResources(this.toolbarItemRoadBrowser, "toolbarItemRoadBrowser");
			this.toolbarItemRoadBrowser.BackgroundImage = null;
			this.toolbarItemRoadBrowser.DisplayStyle = global::System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.toolbarItemRoadBrowser.Image = global::MapEditor.Properties.Resources.roads;
			this.toolbarItemRoadBrowser.Name = "toolbarItemRoadBrowser";
			this.toolbarItemRoadBrowser.Tag = "toggle_road_params_browser";
			this.toolbarItemProperties.AccessibleDescription = null;
			this.toolbarItemProperties.AccessibleName = null;
			resources.ApplyResources(this.toolbarItemProperties, "toolbarItemProperties");
			this.toolbarItemProperties.BackgroundImage = null;
			this.toolbarItemProperties.DisplayStyle = global::System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.toolbarItemProperties.Image = global::MapEditor.Properties.Resources.properties;
			this.toolbarItemProperties.Name = "toolbarItemProperties";
			this.toolbarItemProperties.Tag = "toggle_properties";
			this.toolbarItemCheckers.AccessibleDescription = null;
			this.toolbarItemCheckers.AccessibleName = null;
			resources.ApplyResources(this.toolbarItemCheckers, "toolbarItemCheckers");
			this.toolbarItemCheckers.BackgroundImage = null;
			this.toolbarItemCheckers.DisplayStyle = global::System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.toolbarItemCheckers.Image = global::MapEditor.Properties.Resources.checkers;
			this.toolbarItemCheckers.Name = "toolbarItemCheckers";
			this.toolbarItemCheckers.Tag = "toggle_checkers";
			this.toolbarItemLog.AccessibleDescription = null;
			this.toolbarItemLog.AccessibleName = null;
			resources.ApplyResources(this.toolbarItemLog, "toolbarItemLog");
			this.toolbarItemLog.BackgroundImage = null;
			this.toolbarItemLog.DisplayStyle = global::System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.toolbarItemLog.Image = global::MapEditor.Properties.Resources.log;
			this.toolbarItemLog.Name = "toolbarItemLog";
			this.toolbarItemLog.Tag = "toggle_log";
			this.toolbarItemSeparator10.AccessibleDescription = null;
			this.toolbarItemSeparator10.AccessibleName = null;
			resources.ApplyResources(this.toolbarItemSeparator10, "toolbarItemSeparator10");
			this.toolbarItemSeparator10.Name = "toolbarItemSeparator10";
			this.toolbarItemLayers.AccessibleDescription = null;
			this.toolbarItemLayers.AccessibleName = null;
			resources.ApplyResources(this.toolbarItemLayers, "toolbarItemLayers");
			this.toolbarItemLayers.BackgroundImage = null;
			this.toolbarItemLayers.DisplayStyle = global::System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.toolbarItemLayers.Image = global::MapEditor.Properties.Resources.layer_editor;
			this.toolbarItemLayers.Name = "toolbarItemLayers";
			this.toolbarItemLayers.Tag = "toggle_layers";
			this.toolbarItemLight.AccessibleDescription = null;
			this.toolbarItemLight.AccessibleName = null;
			resources.ApplyResources(this.toolbarItemLight, "toolbarItemLight");
			this.toolbarItemLight.BackgroundImage = null;
			this.toolbarItemLight.DisplayStyle = global::System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.toolbarItemLight.Image = global::MapEditor.Properties.Resources.light_editor;
			this.toolbarItemLight.Name = "toolbarItemLight";
			this.toolbarItemLight.Tag = "toggle_light";
			this.toolbarItemPropertyControl.AccessibleDescription = null;
			this.toolbarItemPropertyControl.AccessibleName = null;
			resources.ApplyResources(this.toolbarItemPropertyControl, "toolbarItemPropertyControl");
			this.toolbarItemPropertyControl.BackgroundImage = null;
			this.toolbarItemPropertyControl.DisplayStyle = global::System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.toolbarItemPropertyControl.Image = global::MapEditor.Properties.Resources.database_editor;
			this.toolbarItemPropertyControl.Name = "toolbarItemPropertyControl";
			this.toolbarItemPropertyControl.Tag = "toggle_property_control";
			this.toolbarItemModelViewer.AccessibleDescription = null;
			this.toolbarItemModelViewer.AccessibleName = null;
			resources.ApplyResources(this.toolbarItemModelViewer, "toolbarItemModelViewer");
			this.toolbarItemModelViewer.BackgroundImage = null;
			this.toolbarItemModelViewer.DisplayStyle = global::System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.toolbarItemModelViewer.Image = global::MapEditor.Properties.Resources.model_viewer;
			this.toolbarItemModelViewer.Name = "toolbarItemModelViewer";
			this.toolbarItemModelViewer.Tag = "toggle_model_viewer";
			this.toolbarItemScriptEditor.AccessibleDescription = null;
			this.toolbarItemScriptEditor.AccessibleName = null;
			resources.ApplyResources(this.toolbarItemScriptEditor, "toolbarItemScriptEditor");
			this.toolbarItemScriptEditor.BackgroundImage = null;
			this.toolbarItemScriptEditor.DisplayStyle = global::System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.toolbarItemScriptEditor.Image = global::MapEditor.Properties.Resources.script_editor;
			this.toolbarItemScriptEditor.Name = "toolbarItemScriptEditor";
			this.toolbarItemScriptEditor.Tag = "toggle_script_editor";
			this.toolbarItemSeparator11.AccessibleDescription = null;
			this.toolbarItemSeparator11.AccessibleName = null;
			resources.ApplyResources(this.toolbarItemSeparator11, "toolbarItemSeparator11");
			this.toolbarItemSeparator11.Name = "toolbarItemSeparator11";
			this.toolbarItemQuests.AccessibleDescription = null;
			this.toolbarItemQuests.AccessibleName = null;
			resources.ApplyResources(this.toolbarItemQuests, "toolbarItemQuests");
			this.toolbarItemQuests.BackgroundImage = null;
			this.toolbarItemQuests.DisplayStyle = global::System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.toolbarItemQuests.Image = global::MapEditor.Properties.Resources.quests;
			this.toolbarItemQuests.Name = "toolbarItemQuests";
			this.toolbarItemQuests.Tag = "toggle_quests";
			this.toolbarItemQuestDiagram.AccessibleDescription = null;
			this.toolbarItemQuestDiagram.AccessibleName = null;
			resources.ApplyResources(this.toolbarItemQuestDiagram, "toolbarItemQuestDiagram");
			this.toolbarItemQuestDiagram.BackgroundImage = null;
			this.toolbarItemQuestDiagram.DisplayStyle = global::System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.toolbarItemQuestDiagram.Image = global::MapEditor.Properties.Resources.quests_diagram;
			this.toolbarItemQuestDiagram.Name = "toolbarItemQuestDiagram";
			this.toolbarItemQuestDiagram.Tag = "toggle_quest_diagram";
			this.toolbarItemQuestTexts.AccessibleDescription = null;
			this.toolbarItemQuestTexts.AccessibleName = null;
			resources.ApplyResources(this.toolbarItemQuestTexts, "toolbarItemQuestTexts");
			this.toolbarItemQuestTexts.BackgroundImage = null;
			this.toolbarItemQuestTexts.DisplayStyle = global::System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.toolbarItemQuestTexts.Image = global::MapEditor.Properties.Resources.quests_texts;
			this.toolbarItemQuestTexts.Name = "toolbarItemQuestTexts";
			this.toolbarItemQuestTexts.Tag = "toggle_import_quest_texts";
			this.toolbarItemSeparator12.AccessibleDescription = null;
			this.toolbarItemSeparator12.AccessibleName = null;
			resources.ApplyResources(this.toolbarItemSeparator12, "toolbarItemSeparator12");
			this.toolbarItemSeparator12.Name = "toolbarItemSeparator12";
			this.toolbarItemCreateMob.AccessibleDescription = null;
			this.toolbarItemCreateMob.AccessibleName = null;
			resources.ApplyResources(this.toolbarItemCreateMob, "toolbarItemCreateMob");
			this.toolbarItemCreateMob.BackgroundImage = null;
			this.toolbarItemCreateMob.DisplayStyle = global::System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.toolbarItemCreateMob.Image = global::MapEditor.Properties.Resources.create_quest_mob;
			this.toolbarItemCreateMob.Name = "toolbarItemCreateMob";
			this.toolbarItemCreateMob.Tag = "toggle_create_mob";
			this.toolbarItemCreateNPC.AccessibleDescription = null;
			this.toolbarItemCreateNPC.AccessibleName = null;
			resources.ApplyResources(this.toolbarItemCreateNPC, "toolbarItemCreateNPC");
			this.toolbarItemCreateNPC.BackgroundImage = null;
			this.toolbarItemCreateNPC.DisplayStyle = global::System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.toolbarItemCreateNPC.Image = global::MapEditor.Properties.Resources.create_quest_NPC;
			this.toolbarItemCreateNPC.Name = "toolbarItemCreateNPC";
			this.toolbarItemCreateNPC.Tag = "toggle_create_NPC";
			this.toolbarItemCreateResource.AccessibleDescription = null;
			this.toolbarItemCreateResource.AccessibleName = null;
			resources.ApplyResources(this.toolbarItemCreateResource, "toolbarItemCreateResource");
			this.toolbarItemCreateResource.BackgroundImage = null;
			this.toolbarItemCreateResource.DisplayStyle = global::System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.toolbarItemCreateResource.Image = global::MapEditor.Properties.Resources.create_quest_resource;
			this.toolbarItemCreateResource.Name = "toolbarItemCreateResource";
			this.toolbarItemCreateResource.Tag = "toggle_create_resource";
			this.toolbarItemCreateQuestItem.AccessibleDescription = null;
			this.toolbarItemCreateQuestItem.AccessibleName = null;
			resources.ApplyResources(this.toolbarItemCreateQuestItem, "toolbarItemCreateQuestItem");
			this.toolbarItemCreateQuestItem.BackgroundImage = null;
			this.toolbarItemCreateQuestItem.DisplayStyle = global::System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.toolbarItemCreateQuestItem.Image = global::MapEditor.Properties.Resources.create_quest_item;
			this.toolbarItemCreateQuestItem.Name = "toolbarItemCreateQuestItem";
			this.toolbarItemCreateQuestItem.Tag = "toggle_create_quest_item";
			this.toolStripSeparator2.AccessibleDescription = null;
			this.toolStripSeparator2.AccessibleName = null;
			resources.ApplyResources(this.toolStripSeparator2, "toolStripSeparator2");
			this.toolStripSeparator2.Name = "toolStripSeparator2";
			this.toolStripButton2.AccessibleDescription = null;
			this.toolStripButton2.AccessibleName = null;
			resources.ApplyResources(this.toolStripButton2, "toolStripButton2");
			this.toolStripButton2.BackgroundImage = null;
			this.toolStripButton2.DisplayStyle = global::System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.toolStripButton2.Name = "toolStripButton2";
			this.toolStripButton2.Tag = "load_quest_for_killing";
			this.toolbarItemSeparator14.AccessibleDescription = null;
			this.toolbarItemSeparator14.AccessibleName = null;
			resources.ApplyResources(this.toolbarItemSeparator14, "toolbarItemSeparator14");
			this.toolbarItemSeparator14.Name = "toolbarItemSeparator14";
			this.toolbarItemAxisUserGeometry.AccessibleDescription = null;
			this.toolbarItemAxisUserGeometry.AccessibleName = null;
			resources.ApplyResources(this.toolbarItemAxisUserGeometry, "toolbarItemAxisUserGeometry");
			this.toolbarItemAxisUserGeometry.BackgroundImage = null;
			this.toolbarItemAxisUserGeometry.DisplayStyle = global::System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.toolbarItemAxisUserGeometry.Image = global::MapEditor.Properties.Resources.axis;
			this.toolbarItemAxisUserGeometry.Name = "toolbarItemAxisUserGeometry";
			this.toolbarItemAxisUserGeometry.Tag = "toggle_axis_user_geometry";
			this.toolbarItemDynamicObjectsFall.AccessibleDescription = null;
			this.toolbarItemDynamicObjectsFall.AccessibleName = null;
			resources.ApplyResources(this.toolbarItemDynamicObjectsFall, "toolbarItemDynamicObjectsFall");
			this.toolbarItemDynamicObjectsFall.BackgroundImage = null;
			this.toolbarItemDynamicObjectsFall.DisplayStyle = global::System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.toolbarItemDynamicObjectsFall.Image = global::MapEditor.Properties.Resources.dynamic_objects_fall;
			this.toolbarItemDynamicObjectsFall.Name = "toolbarItemDynamicObjectsFall";
			this.toolbarItemDynamicObjectsFall.Tag = "toggle_dynamic_objects_fall";
			this.toolbarItemFixTerrainTiles.AccessibleDescription = null;
			this.toolbarItemFixTerrainTiles.AccessibleName = null;
			resources.ApplyResources(this.toolbarItemFixTerrainTiles, "toolbarItemFixTerrainTiles");
			this.toolbarItemFixTerrainTiles.BackgroundImage = null;
			this.toolbarItemFixTerrainTiles.DisplayStyle = global::System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.toolbarItemFixTerrainTiles.Image = global::MapEditor.Properties.Resources.fix_terrain_tiles;
			this.toolbarItemFixTerrainTiles.Name = "toolbarItemFixTerrainTiles";
			this.toolbarItemFixTerrainTiles.Tag = "fix_terrain_tiles";
			this.toolbarItemToggleSound.AccessibleDescription = null;
			this.toolbarItemToggleSound.AccessibleName = null;
			resources.ApplyResources(this.toolbarItemToggleSound, "toolbarItemToggleSound");
			this.toolbarItemToggleSound.BackgroundImage = null;
			this.toolbarItemToggleSound.DisplayStyle = global::System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.toolbarItemToggleSound.Image = global::MapEditor.Properties.Resources.sound;
			this.toolbarItemToggleSound.Name = "toolbarItemToggleSound";
			this.toolbarItemToggleSound.Tag = "toggle_sound";
			base.AccessibleDescription = null;
			base.AccessibleName = null;
			resources.ApplyResources(this, "$this");
			base.AutoScaleMode = global::System.Windows.Forms.AutoScaleMode.Font;
			this.BackgroundImage = null;
			base.Controls.Add(this.ToolsToolStrip);
			base.Controls.Add(this.statusHelp);
			base.Controls.Add(this.MainToolStrip);
			base.Controls.Add(this.statusStrip);
			base.Controls.Add(this.menuStrip);
			this.Font = null;
			base.MainMenuStrip = this.menuStrip;
			base.Name = "MainForm";
			base.Load += new global::System.EventHandler(this.MainForm_Load);
			base.Layout += new global::System.Windows.Forms.LayoutEventHandler(this.MainForm_Layout);
			base.FormClosing += new global::System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
			base.Resize += new global::System.EventHandler(this.MainForm_Resize);
			this.statusStrip.ResumeLayout(false);
			this.statusStrip.PerformLayout();
			this.menuStrip.ResumeLayout(false);
			this.menuStrip.PerformLayout();
			this.MainToolStrip.ResumeLayout(false);
			this.MainToolStrip.PerformLayout();
			this.statusHelp.ResumeLayout(false);
			this.statusHelp.PerformLayout();
			this.ToolsToolStrip.ResumeLayout(false);
			this.ToolsToolStrip.PerformLayout();
			base.ResumeLayout(false);
			base.PerformLayout();
		}

		// Token: 0x040005EB RID: 1515
		private global::System.ComponentModel.IContainer components;

		// Token: 0x040005EC RID: 1516
		private global::System.Windows.Forms.StatusStrip statusStrip;

		// Token: 0x040005ED RID: 1517
		private global::System.Windows.Forms.MenuStrip menuStrip;

		// Token: 0x040005EE RID: 1518
		private global::System.Windows.Forms.ToolStrip MainToolStrip;

		// Token: 0x040005EF RID: 1519
		private global::System.Windows.Forms.ToolStripMenuItem menuMap;

		// Token: 0x040005F0 RID: 1520
		private global::System.Windows.Forms.ToolStripMenuItem menuEdit;

		// Token: 0x040005F1 RID: 1521
		private global::System.Windows.Forms.ToolStripMenuItem menuView;

		// Token: 0x040005F2 RID: 1522
		private global::System.Windows.Forms.ToolStripMenuItem menuTools;

		// Token: 0x040005F3 RID: 1523
		private global::System.Windows.Forms.ToolStripMenuItem menuHelp;

		// Token: 0x040005F4 RID: 1524
		private global::System.Windows.Forms.ToolStripMenuItem menuItemMapOpen;

		// Token: 0x040005F5 RID: 1525
		private global::System.Windows.Forms.ToolStripMenuItem menuItemMapSave;

		// Token: 0x040005F6 RID: 1526
		private global::System.Windows.Forms.ToolStripSeparator menuItemMapSeparator00;

		// Token: 0x040005F7 RID: 1527
		private global::System.Windows.Forms.ToolStripMenuItem subMenuMapRecent;

		// Token: 0x040005F8 RID: 1528
		private global::System.Windows.Forms.ToolStripMenuItem menuItemMapRecent0;

		// Token: 0x040005F9 RID: 1529
		private global::System.Windows.Forms.ToolStripSeparator menuItemMapSeparator01;

		// Token: 0x040005FA RID: 1530
		private global::System.Windows.Forms.ToolStripMenuItem menuItemMapExit;

		// Token: 0x040005FB RID: 1531
		private global::System.Windows.Forms.ToolStripMenuItem menuItemEditUndo;

		// Token: 0x040005FC RID: 1532
		private global::System.Windows.Forms.ToolStripMenuItem menuItemEditRedo;

		// Token: 0x040005FD RID: 1533
		private global::System.Windows.Forms.ToolStripSeparator menuItemEditSeparator00;

		// Token: 0x040005FE RID: 1534
		private global::System.Windows.Forms.ToolStripMenuItem menuItemEditCopy;

		// Token: 0x040005FF RID: 1535
		private global::System.Windows.Forms.ToolStripMenuItem menuItemEditPaste;

		// Token: 0x04000600 RID: 1536
		private global::System.Windows.Forms.ToolStripMenuItem menuItemEditDelete;

		// Token: 0x04000601 RID: 1537
		private global::System.Windows.Forms.ToolStripSeparator menuItemEditSeparator01;

		// Token: 0x04000602 RID: 1538
		private global::System.Windows.Forms.ToolStripSeparator menuItemEditSeparator02;

		// Token: 0x04000603 RID: 1539
		private global::System.Windows.Forms.ToolStripMenuItem menuItemEditFind;

		// Token: 0x04000604 RID: 1540
		private global::System.Windows.Forms.ToolStripMenuItem menuItemViewMinimap;

		// Token: 0x04000605 RID: 1541
		private global::System.Windows.Forms.ToolStripMenuItem menuItemViewObjectEditor;

		// Token: 0x04000606 RID: 1542
		private global::System.Windows.Forms.ToolStripMenuItem menuItemViewTool;

		// Token: 0x04000607 RID: 1543
		private global::System.Windows.Forms.ToolStripMenuItem menuItemViewList;

		// Token: 0x04000608 RID: 1544
		private global::System.Windows.Forms.ToolStripSeparator menuItemViewSeparator02;

		// Token: 0x04000609 RID: 1545
		private global::System.Windows.Forms.ToolStripMenuItem menuItemViewMainToolbar;

		// Token: 0x0400060A RID: 1546
		private global::System.Windows.Forms.ToolStripMenuItem menuItemViewStatusbar;

		// Token: 0x0400060B RID: 1547
		private global::System.Windows.Forms.ToolStripMenuItem menuItemHelpAbout;

		// Token: 0x0400060C RID: 1548
		private global::System.Windows.Forms.ToolStripButton toolbarItemOpenMap;

		// Token: 0x0400060D RID: 1549
		private global::System.Windows.Forms.ToolStripButton toolbarItemSaveMap;

		// Token: 0x0400060E RID: 1550
		private global::System.Windows.Forms.ToolStripButton toolbarItemUndo;

		// Token: 0x0400060F RID: 1551
		private global::System.Windows.Forms.ToolStripButton toolbarItemRedo;

		// Token: 0x04000610 RID: 1552
		private global::System.Windows.Forms.ToolStripSeparator toolbarItemSeparator00;

		// Token: 0x04000611 RID: 1553
		private global::System.Windows.Forms.ToolStripSeparator toolbarItemSeparator01;

		// Token: 0x04000612 RID: 1554
		private global::System.Windows.Forms.ToolStripButton toolbarItemCopy;

		// Token: 0x04000613 RID: 1555
		private global::System.Windows.Forms.ToolStripButton toolbarItemPaste;

		// Token: 0x04000614 RID: 1556
		private global::System.Windows.Forms.ToolStripButton toolbarItemDelete;

		// Token: 0x04000615 RID: 1557
		private global::System.Windows.Forms.ToolStripSeparator toolbarItemSeparator02;

		// Token: 0x04000616 RID: 1558
		private global::System.Windows.Forms.ToolStripButton toolbarItemFindButton;

		// Token: 0x04000617 RID: 1559
		private global::System.Windows.Forms.ToolStripSeparator toolbarItemSeparator03;

		// Token: 0x04000618 RID: 1560
		private global::System.Windows.Forms.ToolStripSeparator toolbarItemSeparator04;

		// Token: 0x04000619 RID: 1561
		private global::System.Windows.Forms.ToolStripStatusLabel StatusbarMessge;

		// Token: 0x0400061A RID: 1562
		private global::System.Windows.Forms.ToolStripStatusLabel StatusbarPosition;

		// Token: 0x0400061B RID: 1563
		private global::System.Windows.Forms.ToolStripMenuItem menuItemViewProperties;

		// Token: 0x0400061C RID: 1564
		private global::System.Windows.Forms.ToolStripSeparator menuItemViewSeparator03;

		// Token: 0x0400061D RID: 1565
		private global::System.Windows.Forms.ToolStripMenuItem menuItemViewLog;

		// Token: 0x0400061E RID: 1566
		private global::System.Windows.Forms.ToolStripSeparator toolbarItemSeparator05;

		// Token: 0x0400061F RID: 1567
		private global::System.Windows.Forms.Timer stepTimer;

		// Token: 0x04000620 RID: 1568
		private global::System.Windows.Forms.Timer saveNotificationTimer;

		// Token: 0x04000621 RID: 1569
		private global::System.Windows.Forms.ToolStripMenuItem menuItemMapClose;

		// Token: 0x04000622 RID: 1570
		private global::System.Windows.Forms.ToolStripMenuItem menuLayers;

		// Token: 0x04000623 RID: 1571
		private global::System.Windows.Forms.ToolStripMenuItem menuItemLayersTerrainGrid;

		// Token: 0x04000624 RID: 1572
		private global::System.Windows.Forms.ToolStripMenuItem menuItemLayersSetTerrainGridColor;

		// Token: 0x04000625 RID: 1573
		private global::System.Windows.Forms.ToolStripMenuItem menuItemLayersSetBackgroundColor;

		// Token: 0x04000626 RID: 1574
		private global::System.Windows.Forms.ToolStripMenuItem menuItemToolsLayers;

		// Token: 0x04000627 RID: 1575
		private global::System.Windows.Forms.ToolStripMenuItem menuItemToolsQuests;

		// Token: 0x04000628 RID: 1576
		private global::System.Windows.Forms.ToolStripMenuItem menuItemToolsDatabaseBrowser;

		// Token: 0x04000629 RID: 1577
		private global::System.Windows.Forms.ToolStripSeparator toolbarItemSeparator07;

		// Token: 0x0400062A RID: 1578
		private global::System.Windows.Forms.ToolStripButton toolbarItemCameraSlow;

		// Token: 0x0400062B RID: 1579
		private global::System.Windows.Forms.ToolStripButton toolbarItemCameraNormal;

		// Token: 0x0400062C RID: 1580
		private global::System.Windows.Forms.ToolStripButton toolbarItemCameraFast;

		// Token: 0x0400062D RID: 1581
		private global::System.Windows.Forms.ToolStripMenuItem cameraToolStripMenuItem;

		// Token: 0x0400062E RID: 1582
		private global::System.Windows.Forms.ToolStripMenuItem menuItemCameraSlow;

		// Token: 0x0400062F RID: 1583
		private global::System.Windows.Forms.ToolStripMenuItem menuItemCameraNormal;

		// Token: 0x04000630 RID: 1584
		private global::System.Windows.Forms.ToolStripMenuItem menuItemCameraFast;

		// Token: 0x04000631 RID: 1585
		private global::System.Windows.Forms.ToolStripSeparator menuItemCameraSeparator00;

		// Token: 0x04000632 RID: 1586
		private global::System.Windows.Forms.ToolStripSeparator toolbarItemSeparator09;

		// Token: 0x04000633 RID: 1587
		private global::System.Windows.Forms.ToolStripButton toolbarItemCameraReset;

		// Token: 0x04000634 RID: 1588
		private global::System.Windows.Forms.ToolStripButton toolbarItemCameraBounding;

		// Token: 0x04000635 RID: 1589
		private global::System.Windows.Forms.ToolStripButton toolbarItemCameraFOV;

		// Token: 0x04000636 RID: 1590
		private global::System.Windows.Forms.ToolStripSeparator toolbarItemSeparator06;

		// Token: 0x04000637 RID: 1591
		private global::System.Windows.Forms.ToolStripButton toolbarItemMoveWidget;

		// Token: 0x04000638 RID: 1592
		private global::System.Windows.Forms.ToolStripButton toolbarItemRotateWidget;

		// Token: 0x04000639 RID: 1593
		private global::System.Windows.Forms.ToolStripButton toolbarItemScaleWidget;

		// Token: 0x0400063A RID: 1594
		private global::System.Windows.Forms.ToolStripButton toolbarItemAlongTerrain;

		// Token: 0x0400063B RID: 1595
		private global::System.Windows.Forms.ToolStripMenuItem menuItemMapRecent1;

		// Token: 0x0400063C RID: 1596
		private global::System.Windows.Forms.ToolStripMenuItem menuItemMapRecent2;

		// Token: 0x0400063D RID: 1597
		private global::System.Windows.Forms.ToolStripMenuItem menuItemMapRecent3;

		// Token: 0x0400063E RID: 1598
		private global::System.Windows.Forms.ToolStripMenuItem menuItemMapRecent4;

		// Token: 0x0400063F RID: 1599
		private global::System.Windows.Forms.ToolStripMenuItem menuItemMapRecent5;

		// Token: 0x04000640 RID: 1600
		private global::System.Windows.Forms.ToolStripMenuItem menuItemMapRecent6;

		// Token: 0x04000641 RID: 1601
		private global::System.Windows.Forms.ToolStripMenuItem menuItemMapRecent7;

		// Token: 0x04000642 RID: 1602
		private global::System.Windows.Forms.ToolStripMenuItem menuItemMapRecent8;

		// Token: 0x04000643 RID: 1603
		private global::System.Windows.Forms.ToolStripMenuItem menuItemMapRecent9;

		// Token: 0x04000644 RID: 1604
		private global::System.Windows.Forms.ToolStripButton toolbarItemObjectOriented;

		// Token: 0x04000645 RID: 1605
		private global::System.Windows.Forms.ToolStripMenuItem menuItemLayersZones;

		// Token: 0x04000646 RID: 1606
		private global::System.Windows.Forms.ToolStripMenuItem menuItemLayersPassability;

		// Token: 0x04000647 RID: 1607
		private global::System.Windows.Forms.ToolStripSeparator menuItemLayersSeparator01;

		// Token: 0x04000648 RID: 1608
		private global::System.Windows.Forms.ToolStripButton toolbarItemTimeForm;

		// Token: 0x04000649 RID: 1609
		private global::System.Windows.Forms.ToolStripSeparator toolbarItemSeparator08;

		// Token: 0x0400064A RID: 1610
		private global::System.Windows.Forms.ToolStripMenuItem menuItemViewMultiobjectBrowser;

		// Token: 0x0400064B RID: 1611
		private global::System.Windows.Forms.ToolStripMenuItem menuItemCameraReset;

		// Token: 0x0400064C RID: 1612
		private global::System.Windows.Forms.ToolStripMenuItem menuItemCameraBoundingToolStripMenuItem;

		// Token: 0x0400064D RID: 1613
		private global::System.Windows.Forms.ToolStripMenuItem menuItemLayersSpawnPointsGeometry;

		// Token: 0x0400064E RID: 1614
		private global::System.Windows.Forms.ToolStripMenuItem menuItemToolsLight;

		// Token: 0x0400064F RID: 1615
		private global::System.Windows.Forms.ToolStripButton toolbarItemTerrainGrid;

		// Token: 0x04000650 RID: 1616
		private global::System.Windows.Forms.ToolStripMenuItem menuItemLayersSound;

		// Token: 0x04000651 RID: 1617
		private global::System.Windows.Forms.ToolStripButton toolbarItemSpawnPointsGeometry;

		// Token: 0x04000652 RID: 1618
		private global::System.Windows.Forms.ToolStripButton toolbarItemPassability;

		// Token: 0x04000653 RID: 1619
		private global::System.Windows.Forms.ToolStripButton toolbarItemZones;

		// Token: 0x04000654 RID: 1620
		private global::System.Windows.Forms.ToolStripButton toolbarItemLink;

		// Token: 0x04000655 RID: 1621
		private global::System.Windows.Forms.ToolStripButton toolbarItemUnlink;

		// Token: 0x04000656 RID: 1622
		private global::System.Windows.Forms.ToolStripMenuItem menuItemEditLinkObjects;

		// Token: 0x04000657 RID: 1623
		private global::System.Windows.Forms.ToolStripMenuItem menuItemEditUnlinkObjects;

		// Token: 0x04000658 RID: 1624
		private global::System.Windows.Forms.ToolStripMenuItem linksToolStripMenuItem;

		// Token: 0x04000659 RID: 1625
		private global::System.Windows.Forms.ToolStripButton toolbarItemLinkUserGeometry;

		// Token: 0x0400065A RID: 1626
		private global::System.Windows.Forms.ToolStripButton toolbarItemAlignToGrid;

		// Token: 0x0400065B RID: 1627
		private global::System.Windows.Forms.ToolStripMenuItem menuItemToolsQuestEditor;

		// Token: 0x0400065C RID: 1628
		private global::System.Windows.Forms.ToolStripMenuItem menuItemToolsQuestDiagram;

		// Token: 0x0400065D RID: 1629
		private global::System.Windows.Forms.ToolStripButton toolbarItemMeasureDistance;

		// Token: 0x0400065E RID: 1630
		private global::System.Windows.Forms.ToolStripSeparator menuItemCameraSeparator01;

		// Token: 0x0400065F RID: 1631
		private global::System.Windows.Forms.ToolStripMenuItem menuItemFPSCamera;

		// Token: 0x04000660 RID: 1632
		private global::System.Windows.Forms.ToolStripMenuItem menuItemRTSCamera;

		// Token: 0x04000661 RID: 1633
		private global::System.Windows.Forms.ToolStripMenuItem menuItemLayersScriptAreasGeometry;

		// Token: 0x04000662 RID: 1634
		private global::System.Windows.Forms.ToolStripButton toolbarItemScriptAreasGeometry;

		// Token: 0x04000663 RID: 1635
		private global::System.Windows.Forms.ToolStripMenuItem menuItemLayersAxisGeometry;

		// Token: 0x04000664 RID: 1636
		private global::System.Windows.Forms.ToolStripMenuItem menuItemLayersFog;

		// Token: 0x04000665 RID: 1637
		private global::System.Windows.Forms.ToolStripButton toolbarItemCollisionGeometry;

		// Token: 0x04000666 RID: 1638
		private global::System.Windows.Forms.ToolStripMenuItem menuItemLayersCollisionGeometry;

		// Token: 0x04000667 RID: 1639
		private global::System.Windows.Forms.ToolStripButton toolbarItemGridStep;

		// Token: 0x04000668 RID: 1640
		private global::System.Windows.Forms.ToolStripButton toolbarItemWireframe;

		// Token: 0x04000669 RID: 1641
		private global::System.Windows.Forms.ToolStripMenuItem menuItemLayersWireframe;

		// Token: 0x0400066A RID: 1642
		private global::System.Windows.Forms.ToolStripButton toolbarItemLockSelection;

		// Token: 0x0400066B RID: 1643
		private global::System.Windows.Forms.ToolStripButton toolbarItemPlaceOnlyOneObject;

		// Token: 0x0400066C RID: 1644
		private global::System.Windows.Forms.ToolStripMenuItem selectorToolStripMenuItem;

		// Token: 0x0400066D RID: 1645
		private global::System.Windows.Forms.ToolStripMenuItem menuItemEditResetAltitude;

		// Token: 0x0400066E RID: 1646
		private global::System.Windows.Forms.ToolStripMenuItem menuItemEditDropToNearest;

		// Token: 0x0400066F RID: 1647
		private global::System.Windows.Forms.ToolStripMenuItem menuItemEditResetRotation;

		// Token: 0x04000670 RID: 1648
		private global::System.Windows.Forms.ToolStripMenuItem menuItemEditResetScale;

		// Token: 0x04000671 RID: 1649
		private global::System.Windows.Forms.ToolStripMenuItem menuItemSelectorMove;

		// Token: 0x04000672 RID: 1650
		private global::System.Windows.Forms.ToolStripMenuItem menuItemSelectorRotate;

		// Token: 0x04000673 RID: 1651
		private global::System.Windows.Forms.ToolStripMenuItem menuItemSelectorScale;

		// Token: 0x04000674 RID: 1652
		private global::System.Windows.Forms.ToolStripSeparator menuItemSelectorSeparator00;

		// Token: 0x04000675 RID: 1653
		private global::System.Windows.Forms.ToolStripMenuItem menuItemSelectorScaleObjectOriented;

		// Token: 0x04000676 RID: 1654
		private global::System.Windows.Forms.ToolStripMenuItem menuItemSelectorAlongTerrain;

		// Token: 0x04000677 RID: 1655
		private global::System.Windows.Forms.ToolStripMenuItem menuItemSelectorAlignToGrid;

		// Token: 0x04000678 RID: 1656
		private global::System.Windows.Forms.ToolStripMenuItem menuItemSelectorLockSelection;

		// Token: 0x04000679 RID: 1657
		private global::System.Windows.Forms.ToolStripMenuItem menuItemSelectorEditObjectAfterAdd;

		// Token: 0x0400067A RID: 1658
		private global::System.Windows.Forms.ToolStripButton toolbarItemZoneLocatorsGeometry;

		// Token: 0x0400067B RID: 1659
		private global::System.Windows.Forms.ToolStripMenuItem menuItemLayersZoneLocatorsGeometry;

		// Token: 0x0400067C RID: 1660
		private global::System.Windows.Forms.ToolStripMenuItem placeDynamicObjectsToSurfaceToolStripMenuItem;

		// Token: 0x0400067D RID: 1661
		private global::System.Windows.Forms.ToolStripButton toolbarItemBottomGrid;

		// Token: 0x0400067E RID: 1662
		private global::System.Windows.Forms.ToolStripMenuItem menuItemLayersBottomGrid;

		// Token: 0x0400067F RID: 1663
		private global::System.Windows.Forms.ToolStripMenuItem menuItemLayersSetBottomGridColor;

		// Token: 0x04000680 RID: 1664
		private global::System.Windows.Forms.ToolStripMenuItem menuItemLayersRoutePointsGeometry;

		// Token: 0x04000681 RID: 1665
		private global::System.Windows.Forms.ToolStripButton toolbarItemRoutePointsGeometry;

		// Token: 0x04000682 RID: 1666
		private global::System.Windows.Forms.ToolStripMenuItem menuItemEditArrangeLinkedRoutePoints;

		// Token: 0x04000683 RID: 1667
		private global::System.Windows.Forms.ToolStripMenuItem menuItemToolsQuestTexts;

		// Token: 0x04000684 RID: 1668
		private global::System.Windows.Forms.ToolStripMenuItem menuItemToolsQuickObjectCreation;

		// Token: 0x04000685 RID: 1669
		private global::System.Windows.Forms.ToolStripMenuItem menuItemToolsCreareNPC;

		// Token: 0x04000686 RID: 1670
		private global::System.Windows.Forms.ToolStripMenuItem menuItemToolsCreateMob;

		// Token: 0x04000687 RID: 1671
		private global::System.Windows.Forms.ToolStripMenuItem menuItemToolsCreateResource;

		// Token: 0x04000688 RID: 1672
		private global::System.Windows.Forms.ToolStripMenuItem menuItemToolsCreateQuestItem;

		// Token: 0x04000689 RID: 1673
		private global::System.Windows.Forms.ToolStripMenuItem menuItemEditCopyAndSave;

		// Token: 0x0400068A RID: 1674
		private global::System.Windows.Forms.ToolStripMenuItem menuItemToolsModelViewer;

		// Token: 0x0400068B RID: 1675
		private global::System.Windows.Forms.ToolStripStatusLabel StatusbarHelp;

		// Token: 0x0400068C RID: 1676
		private global::System.Windows.Forms.StatusStrip statusHelp;

		// Token: 0x0400068D RID: 1677
		private global::System.Windows.Forms.ToolStripMenuItem menuItemViewHelpString;

		// Token: 0x0400068E RID: 1678
		private global::System.Windows.Forms.ToolStripSeparator menuItemLayersSeparator02;

		// Token: 0x0400068F RID: 1679
		private global::System.Windows.Forms.ToolStripButton toolbarItemGenerateShadows;

		// Token: 0x04000690 RID: 1680
		private global::System.Windows.Forms.ToolStripMenuItem menuItemLayersAllObjects;

		// Token: 0x04000691 RID: 1681
		private global::System.Windows.Forms.ToolStripButton toolbarItemToggleAllObjects;

		// Token: 0x04000692 RID: 1682
		private global::System.Windows.Forms.ToolStripMenuItem menuItemToolsFactionEditor;

		// Token: 0x04000693 RID: 1683
		private global::System.Windows.Forms.ToolStripMenuItem menuItemToolsScriptEditor;

		// Token: 0x04000694 RID: 1684
		private global::System.Windows.Forms.ToolStripButton toolbarItemFog;

		// Token: 0x04000695 RID: 1685
		private global::System.Windows.Forms.ToolStripMenuItem menuItemViewRooadBrowser;

		// Token: 0x04000696 RID: 1686
		private global::System.Windows.Forms.ToolStrip ToolsToolStrip;

		// Token: 0x04000697 RID: 1687
		private global::System.Windows.Forms.ToolStripButton toolbarItemMinimap;

		// Token: 0x04000698 RID: 1688
		private global::System.Windows.Forms.ToolStripButton toolbarItemTool;

		// Token: 0x04000699 RID: 1689
		private global::System.Windows.Forms.ToolStripButton toolbarItemList;

		// Token: 0x0400069A RID: 1690
		private global::System.Windows.Forms.ToolStripButton toolbarItemObjectEditor;

		// Token: 0x0400069B RID: 1691
		private global::System.Windows.Forms.ToolStripButton toolbarItemMultiObjectBrowser;

		// Token: 0x0400069C RID: 1692
		private global::System.Windows.Forms.ToolStripButton toolbarItemRoadBrowser;

		// Token: 0x0400069D RID: 1693
		private global::System.Windows.Forms.ToolStripButton toolbarItemProperties;

		// Token: 0x0400069E RID: 1694
		private global::System.Windows.Forms.ToolStripButton toolbarItemLog;

		// Token: 0x0400069F RID: 1695
		private global::System.Windows.Forms.ToolStripSeparator toolbarItemSeparator10;

		// Token: 0x040006A0 RID: 1696
		private global::System.Windows.Forms.ToolStripButton toolbarItemLayers;

		// Token: 0x040006A1 RID: 1697
		private global::System.Windows.Forms.ToolStripButton toolbarItemLight;

		// Token: 0x040006A2 RID: 1698
		private global::System.Windows.Forms.ToolStripButton toolbarItemPropertyControl;

		// Token: 0x040006A3 RID: 1699
		private global::System.Windows.Forms.ToolStripButton toolbarItemModelViewer;

		// Token: 0x040006A4 RID: 1700
		private global::System.Windows.Forms.ToolStripButton toolbarItemQuests;

		// Token: 0x040006A5 RID: 1701
		private global::System.Windows.Forms.ToolStripButton toolbarItemQuestDiagram;

		// Token: 0x040006A6 RID: 1702
		private global::System.Windows.Forms.ToolStripButton toolbarItemScriptEditor;

		// Token: 0x040006A7 RID: 1703
		private global::System.Windows.Forms.ToolStripSeparator toolbarItemSeparator11;

		// Token: 0x040006A8 RID: 1704
		private global::System.Windows.Forms.ToolStripButton toolbarItemQuestTexts;

		// Token: 0x040006A9 RID: 1705
		private global::System.Windows.Forms.ToolStripSeparator toolbarItemSeparator12;

		// Token: 0x040006AA RID: 1706
		private global::System.Windows.Forms.ToolStripButton toolbarItemCreateMob;

		// Token: 0x040006AB RID: 1707
		private global::System.Windows.Forms.ToolStripButton toolbarItemCreateNPC;

		// Token: 0x040006AC RID: 1708
		private global::System.Windows.Forms.ToolStripButton toolbarItemCreateResource;

		// Token: 0x040006AD RID: 1709
		private global::System.Windows.Forms.ToolStripButton toolbarItemCreateQuestItem;

		// Token: 0x040006AE RID: 1710
		private global::System.Windows.Forms.ToolStripMenuItem menuItemViewToolsToolbar;

		// Token: 0x040006AF RID: 1711
		private global::System.Windows.Forms.ToolStripSeparator menuItemEditSeparator03;

		// Token: 0x040006B0 RID: 1712
		private global::System.Windows.Forms.ToolStripMenuItem menuItemEditResetTerrainHeightsCache;

		// Token: 0x040006B1 RID: 1713
		private global::System.Windows.Forms.ToolStripSeparator menuItemEditSeparator04;

		// Token: 0x040006B2 RID: 1714
		private global::System.Windows.Forms.ToolStripMenuItem menuItemEditAlongNormal;

		// Token: 0x040006B3 RID: 1715
		private global::System.Windows.Forms.ToolStripButton toolbarItemAutolinkAfterAdd;

		// Token: 0x040006B4 RID: 1716
		private global::System.Windows.Forms.ToolStripMenuItem menuItemSelectorAutomaticallyLink;

		// Token: 0x040006B5 RID: 1717
		private global::System.Windows.Forms.ToolStripSeparator menuItemSelectorSeparator01;

		// Token: 0x040006B6 RID: 1718
		private global::System.Windows.Forms.ToolStripMenuItem menuItemSelectorCreateCrosslinks;

		// Token: 0x040006B7 RID: 1719
		private global::System.Windows.Forms.ToolStripButton toolbarItemCreateCrosslinks;

		// Token: 0x040006B8 RID: 1720
		private global::System.Windows.Forms.ToolStripMenuItem menuItemEditRearrangeLinkedRoutePoints;

		// Token: 0x040006B9 RID: 1721
		private global::System.Windows.Forms.ToolStripButton toolbarItemCheckers;

		// Token: 0x040006BA RID: 1722
		private global::System.Windows.Forms.ToolStripMenuItem checkersWindowToolStripMenuItem;

		// Token: 0x040006BB RID: 1723
		private global::System.Windows.Forms.ToolStripButton toolbarItemAggroRadius;

		// Token: 0x040006BC RID: 1724
		private global::System.Windows.Forms.ToolStripSeparator toolbarItemSeparator13;

		// Token: 0x040006BD RID: 1725
		private global::System.Windows.Forms.ToolStripMenuItem menuItemLayersMobsAggro;

		// Token: 0x040006BE RID: 1726
		private global::System.Windows.Forms.ToolStripMenuItem menuItemLayersMobsAggroRadius;

		// Token: 0x040006BF RID: 1727
		private global::System.Windows.Forms.ToolStripMenuItem menuItemLayersMobsAggroVolumes;

		// Token: 0x040006C0 RID: 1728
		private global::System.Windows.Forms.ToolStripMenuItem menuItemLayersDymanicSceneStatistic;

		// Token: 0x040006C1 RID: 1729
		private global::System.Windows.Forms.ToolStripButton toolbarItemDynamicStatistic;

		// Token: 0x040006C2 RID: 1730
		private global::System.Windows.Forms.ToolStripButton toolbarItemSky;

		// Token: 0x040006C3 RID: 1731
		private global::System.Windows.Forms.ToolStripMenuItem menuItemLayersSky;

		// Token: 0x040006C4 RID: 1732
		private global::System.Windows.Forms.ToolStripMenuItem menuItemToolsGameWindow;

		// Token: 0x040006C5 RID: 1733
		private global::System.Windows.Forms.ToolStripButton toolbarItemGame;

		// Token: 0x040006C6 RID: 1734
		private global::System.Windows.Forms.ToolStripMenuItem menuItemMapSetArtDirectory;

		// Token: 0x040006C7 RID: 1735
		private global::System.Windows.Forms.ToolStripButton toolbarItemAlongWater;

		// Token: 0x040006C8 RID: 1736
		private global::System.Windows.Forms.ToolStripMenuItem menuItemSelectorAlongWater;

		// Token: 0x040006C9 RID: 1737
		private global::System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;

		// Token: 0x040006CA RID: 1738
		private global::System.Windows.Forms.ToolStripMenuItem objectSetBrowserToolStripMenuItem;

		// Token: 0x040006CB RID: 1739
		private global::System.Windows.Forms.ToolStripButton toolStripButton1;

		// Token: 0x040006CC RID: 1740
		private global::System.Windows.Forms.ToolStripMenuItem menuItemEditCut;

		// Token: 0x040006CD RID: 1741
		private global::System.Windows.Forms.ToolStripButton toolbarItemCut;

		// Token: 0x040006CE RID: 1742
		private global::System.Windows.Forms.ToolStripButton toolbarItemAlongNearest;

		// Token: 0x040006CF RID: 1743
		private global::System.Windows.Forms.ToolStripMenuItem menuItemSelectorAlongNearest;

		// Token: 0x040006D0 RID: 1744
		private global::System.Windows.Forms.ToolStripButton toolbarItemRouteObjectBrowser;

		// Token: 0x040006D1 RID: 1745
		private global::System.Windows.Forms.ToolStripMenuItem menuItemViewRouteObjectBrowser;

		// Token: 0x040006D2 RID: 1746
		private global::System.Windows.Forms.ToolStripSeparator toolbarItemSeparator14;

		// Token: 0x040006D3 RID: 1747
		private global::System.Windows.Forms.ToolStripButton toolbarItemAxisUserGeometry;

		// Token: 0x040006D4 RID: 1748
		private global::System.Windows.Forms.ToolStripButton toolbarItemRouteObjects;

		// Token: 0x040006D5 RID: 1749
		private global::System.Windows.Forms.ToolStripButton toolbarItemDynamicObjectsFall;

		// Token: 0x040006D6 RID: 1750
		private global::System.Windows.Forms.ToolStripButton toolbarItemFixTerrainTiles;

		// Token: 0x040006D7 RID: 1751
		private global::System.Windows.Forms.ToolStripMenuItem menuItemLayersRouteObjects;

		// Token: 0x040006D8 RID: 1752
		private global::System.Windows.Forms.ToolStripButton toolbarItemToggleSound;

		// Token: 0x040006D9 RID: 1753
		private global::System.Windows.Forms.ToolStripMenuItem visualMobScriptsToolStripMenuItem;

		// Token: 0x040006DA RID: 1754
		private global::System.Windows.Forms.ToolStripMenuItem menuItemLayersLargeBottomGrid;

		// Token: 0x040006DB RID: 1755
		private global::System.Windows.Forms.ToolStripMenuItem menuItemLayersLargeTerrainGrid;

		// Token: 0x040006DC RID: 1756
		private global::System.Windows.Forms.ToolStripSeparator menuItemLayersSeparator03;

		// Token: 0x040006DD RID: 1757
		private global::System.Windows.Forms.ToolStripMenuItem musicToolStripMenuItem;

		// Token: 0x040006DE RID: 1758
		private global::System.Windows.Forms.ToolStripMenuItem ambienceToolStripMenuItem;

		// Token: 0x040006DF RID: 1759
		private global::System.Windows.Forms.ToolStripMenuItem propertiesToolStripMenuItem;

		// Token: 0x040006E0 RID: 1760
		private global::System.Windows.Forms.ToolStripButton toolbarItemAstralBordersGeometry;

		// Token: 0x040006E1 RID: 1761
		private global::System.Windows.Forms.ToolStripMenuItem menuItemLayersAstralbordersGeometry;

		// Token: 0x040006E2 RID: 1762
		private global::System.Windows.Forms.ToolStripButton toolbarItemPlaceAlongNormal;

		// Token: 0x040006E3 RID: 1763
		private global::System.Windows.Forms.ToolStripMenuItem menuItemSelectorPlaceAlongNormal;

		// Token: 0x040006E4 RID: 1764
		private global::System.Windows.Forms.ToolStripMenuItem menuItemCameraMove;

		// Token: 0x040006E5 RID: 1765
		private global::System.Windows.Forms.ToolStripButton toolbarItemCameraMove;

		// Token: 0x040006E6 RID: 1766
		private global::System.Windows.Forms.ToolStripMenuItem formatToolStripMenuItem;

		// Token: 0x040006E7 RID: 1767
		private global::System.Windows.Forms.ToolStripMenuItem menuItemPropertiesFindQuests;

		// Token: 0x040006E8 RID: 1768
		private global::System.Windows.Forms.ToolStripMenuItem menuItemPropertiesAddNewQuest;

		// Token: 0x040006E9 RID: 1769
		private global::System.Windows.Forms.ToolStripMenuItem menuItemPropertiesProperties;

		// Token: 0x040006EA RID: 1770
		private global::System.Windows.Forms.ToolStripMenuItem menuItemPropertiesQuickProperties;

		// Token: 0x040006EB RID: 1771
		private global::System.Windows.Forms.ToolStripMenuItem menuItemPropertiesSpecialProperties;

		// Token: 0x040006EC RID: 1772
		private global::System.Windows.Forms.ToolStripSeparator menuItemPropertiesSeparator00;

		// Token: 0x040006ED RID: 1773
		private global::System.Windows.Forms.ToolStripMenuItem menuItemPropertiesOpenInDatabaseBrowser;

		// Token: 0x040006EE RID: 1774
		private global::System.Windows.Forms.ToolStripMenuItem menuItemPropertiesSpawnTunerProperties;

		// Token: 0x040006EF RID: 1775
		private global::System.Windows.Forms.ToolStripMenuItem menuItemPropertiesEnablePropertiesOnDoubleClick;

		// Token: 0x040006F0 RID: 1776
		private global::System.Windows.Forms.ToolStripMenuItem menuItemPropertiesOpenInObjectEditor;

		// Token: 0x040006F1 RID: 1777
		private global::System.Windows.Forms.ToolStripMenuItem menuItemPropertiesOpenInScriptEditor;

		// Token: 0x040006F2 RID: 1778
		private global::System.Windows.Forms.ToolStripMenuItem menuItemPropertiesOpenInModelViewer;

		// Token: 0x040006F3 RID: 1779
		private global::System.Windows.Forms.ToolStripMenuItem menuItemPropertiesOpenOrCreatePatrolScript;

		// Token: 0x040006F4 RID: 1780
		private global::System.Windows.Forms.ToolStripMenuItem menuItemPropertiesOpenOrCreateSpawnTuner;

		// Token: 0x040006F5 RID: 1781
		private global::System.Windows.Forms.ToolStripMenuItem menuItemPropertiesOpenInDialogEditor;

		// Token: 0x040006F6 RID: 1782
		private global::System.Windows.Forms.ToolStripSeparator menuItemEditSeparator05;

		// Token: 0x040006F7 RID: 1783
		private global::System.Windows.Forms.ToolStripMenuItem menuItemPropertiesCreateSpawnTable;

		// Token: 0x040006F8 RID: 1784
		private global::System.Windows.Forms.ToolStripMenuItem menuItemPropertiesCloneSpawnTable;

		// Token: 0x040006F9 RID: 1785
		private global::System.Windows.Forms.ToolStripSeparator menuItemPropertiesSeparator01;

		// Token: 0x040006FA RID: 1786
		private global::System.Windows.Forms.ToolStripMenuItem menuItemPropertiesCreateFixedIdleAnimationTuner;

		// Token: 0x040006FB RID: 1787
		private global::System.Windows.Forms.ToolStripSeparator menuItemPropertiesSeparator02;

		// Token: 0x040006FC RID: 1788
		private global::System.Windows.Forms.ToolStripSeparator menuItemPropertiesSeparator04;

		// Token: 0x040006FD RID: 1789
		private global::System.Windows.Forms.ToolStripMenuItem menuItemCameraCustom;

		// Token: 0x040006FE RID: 1790
		private global::System.Windows.Forms.ToolStripButton toolbarItemCameraCustom;

		// Token: 0x040006FF RID: 1791
		private global::System.Windows.Forms.ToolStripSeparator toolbarItemSeparator15;

		// Token: 0x04000700 RID: 1792
		private global::System.Windows.Forms.ToolStripButton toolbarItemCameraCustomSetSpeed;

		// Token: 0x04000701 RID: 1793
		private global::System.Windows.Forms.ToolStripMenuItem menuItemCameraSetCustomSpeed;

		// Token: 0x04000702 RID: 1794
		private global::System.Windows.Forms.ToolStripMenuItem setCameraFovToolStripMenuItem;

		// Token: 0x04000703 RID: 1795
		private global::System.Windows.Forms.ToolStripSeparator menuItemCameraSeparator02;

		// Token: 0x04000704 RID: 1796
		private global::System.Windows.Forms.ToolStripMenuItem menuItemPropertiesOpenVisualInDatabaseBrowser;

		// Token: 0x04000705 RID: 1797
		private global::System.Windows.Forms.ToolStripSeparator toolStripSeparator1;

		// Token: 0x04000706 RID: 1798
		private global::System.Windows.Forms.ToolStripMenuItem convertSpawnPointsToClientSpawnPointsToolStripMenuItem;

		// Token: 0x04000707 RID: 1799
		private global::System.Windows.Forms.ToolStripMenuItem findQuestdForKillingToolStripMenuItem;

		// Token: 0x04000708 RID: 1800
		private global::System.Windows.Forms.ToolStripButton toolStripButton2;

		// Token: 0x04000709 RID: 1801
		private global::System.Windows.Forms.ToolStripSeparator toolStripSeparator2;

		// Token: 0x0400070A RID: 1802
		private global::System.Windows.Forms.ToolStripButton toolbarItemProjectileVisObjects;

		// Token: 0x0400070B RID: 1803
		private global::System.Windows.Forms.ToolStripMenuItem stopDayTimeToolStripMenuItem;

		// Token: 0x0400070C RID: 1804
		private global::System.Windows.Forms.ToolStripMenuItem waterEditorToolStripMenuItem;

		// Token: 0x0400070D RID: 1805
		private global::System.Windows.Forms.ToolStripMenuItem hSVColorEditorToolStripMenuItem;

		// Token: 0x0400070E RID: 1806
		private global::System.Windows.Forms.ToolStripMenuItem menuItemLayersStaticObjects;

		// Token: 0x0400070F RID: 1807
		private global::System.Windows.Forms.ToolStripMenuItem menuItemLayersInteractiveObjects;

		// Token: 0x04000710 RID: 1808
		private global::System.Windows.Forms.ToolStripMenuItem vendorsTableToolStripMenuItem;

		// Token: 0x04000711 RID: 1809
		private global::System.Windows.Forms.ToolStripMenuItem setSceneTimeToolStripMenuItem;

		// Token: 0x04000712 RID: 1810
		private global::System.Windows.Forms.ToolStripSeparator toolStripSeparator3;

		// Token: 0x04000713 RID: 1811
		private global::System.Windows.Forms.ToolStripMenuItem toolStripMenuItem2;

		// Token: 0x04000714 RID: 1812
		private global::System.Windows.Forms.ToolStripMenuItem addToObjectEditorToolStripMenuItem;

		// Token: 0x04000715 RID: 1813
		private global::System.Windows.Forms.ToolStripMenuItem toolStripMenuItem3;

		// Token: 0x04000716 RID: 1814
		private global::System.Windows.Forms.ToolStripMenuItem notSelectableObjectsToolStripMenuItem;

		// Token: 0x04000717 RID: 1815
		private global::System.Windows.Forms.ToolStripSeparator toolStripSeparator4;

		// Token: 0x04000718 RID: 1816
		private global::System.Windows.Forms.ToolStripSeparator toolStripSeparator5;

		// Token: 0x04000719 RID: 1817
		private global::System.Windows.Forms.ToolStripMenuItem markAsUnselectableToolStripMenuItem;

		// Token: 0x0400071A RID: 1818
		private global::System.Windows.Forms.ToolStripButton ServerConfigToolButton;

		// Token: 0x0400071B RID: 1819
		private global::System.Windows.Forms.ToolStripMenuItem importCuesToolStripMenuItem;

		// Token: 0x0400071C RID: 1820
		private global::System.Windows.Forms.ToolStripMenuItem menuItemToolsFillHeightsFromHeightmap;

		// Token: 0x0400071D RID: 1821
		private global::System.Windows.Forms.ToolStripMenuItem createSoundStaticObjectToolStripMenuItem;

		// Token: 0x0400071E RID: 1822
		private global::System.Windows.Forms.ToolStripMenuItem createMinimapToolStripMenuItem;

		// Token: 0x0400071F RID: 1823
		private global::System.Windows.Forms.ToolStripSeparator toolStripSeparator6;

		// Token: 0x04000720 RID: 1824
		private global::System.Windows.Forms.ToolStripMenuItem disassembleObjectToolStripMenuItem;

		// Token: 0x04000721 RID: 1825
		private global::System.Windows.Forms.ToolStripMenuItem autoFocusToolStripMenuItem;

		// Token: 0x04000722 RID: 1826
		private global::System.Windows.Forms.ToolStripMenuItem hiddenObjectsToolStripMenuItem;

		// Token: 0x04000723 RID: 1827
		private global::System.Windows.Forms.ToolStripMenuItem toolStripMenuItem4;

		// Token: 0x04000724 RID: 1828
		private global::System.Windows.Forms.ToolStripMenuItem toolStripMenuItem5;

		// Token: 0x04000725 RID: 1829
		private global::System.Windows.Forms.ToolStripButton toolbarItemWorldCutSphere;

		// Token: 0x04000726 RID: 1830
		private global::System.Windows.Forms.ToolStripMenuItem menuItemViewMeasureDistance;

		// Token: 0x04000727 RID: 1831
		private global::System.Windows.Forms.ToolStripSeparator menuItemViewSeparator00;

		// Token: 0x04000728 RID: 1832
		private global::System.Windows.Forms.ToolStripMenuItem menuItemViewGenerateShadows;

		// Token: 0x04000729 RID: 1833
		private global::System.Windows.Forms.ToolStripMenuItem menuItemViewFixTerrainTiles;

		// Token: 0x0400072A RID: 1834
		private global::System.Windows.Forms.ToolStripSeparator menuItemViewSeparator01;

		// Token: 0x0400072B RID: 1835
		private global::System.Windows.Forms.ToolStripMenuItem worldCutSphereToolStripMenuItem;

		// Token: 0x0400072C RID: 1836
		private global::System.Windows.Forms.ToolStripMenuItem projectileVisObjectsToolStripMenuItem;

		// Token: 0x0400072D RID: 1837
		private global::System.Windows.Forms.ToolStripMenuItem replaceStaticObjectToolStripMenuItem;

		// Token: 0x0400072E RID: 1838
		private global::System.Windows.Forms.ToolStripMenuItem menuItemToolsFullModelViewer;

		// Token: 0x0400072F RID: 1839
		private global::System.Windows.Forms.ToolStripMenuItem menuItemPropertiesOpenInModelEditor;

		// Token: 0x04000730 RID: 1840
		private global::System.Windows.Forms.ToolStripMenuItem blockEditingToolStripMenuItem;
	}
}
