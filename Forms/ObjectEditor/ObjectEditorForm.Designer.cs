namespace MapEditor.Forms.ObjectEditor
{
	// Token: 0x0200005A RID: 90
	public partial class ObjectEditorForm : global::MapEditor.Forms.Base.BaseForm, global::Tools.Statusbar.IStatusbar
	{
		// Token: 0x06000480 RID: 1152 RVA: 0x00024943 File Offset: 0x00023943
		protected override void Dispose(bool disposing)
		{
			if (disposing && this.components != null)
			{
				this.components.Dispose();
			}
			base.Dispose(disposing);
		}

		// Token: 0x06000481 RID: 1153 RVA: 0x00024964 File Offset: 0x00023964
		private void InitializeComponent()
		{
			this.components = new global::System.ComponentModel.Container();
			global::System.ComponentModel.ComponentResourceManager resources = new global::System.ComponentModel.ComponentResourceManager(typeof(global::MapEditor.Forms.ObjectEditor.ObjectEditorForm));
			this.sceneDrawTimer = new global::System.Windows.Forms.Timer(this.components);
			this.toolStrip = new global::System.Windows.Forms.ToolStrip();
			this.toolbarItemOpenMap = new global::System.Windows.Forms.ToolStripButton();
			this.toolbarItemSaveMap = new global::System.Windows.Forms.ToolStripButton();
			this.toolbarItemGenerateIcon = new global::System.Windows.Forms.ToolStripButton();
			this.toolStripSeparator00 = new global::System.Windows.Forms.ToolStripSeparator();
			this.toolbarItemUndo = new global::System.Windows.Forms.ToolStripButton();
			this.toolbarItemRedo = new global::System.Windows.Forms.ToolStripButton();
			this.toolStripSeparator01 = new global::System.Windows.Forms.ToolStripSeparator();
			this.toolbarItemMoveWidget = new global::System.Windows.Forms.ToolStripButton();
			this.toolbarItemRotateWidget = new global::System.Windows.Forms.ToolStripButton();
			this.toolbarItemScaleWidget = new global::System.Windows.Forms.ToolStripButton();
			this.toolbarItemObjectOriented = new global::System.Windows.Forms.ToolStripButton();
			this.toolbarItemAlignToGrid = new global::System.Windows.Forms.ToolStripButton();
			this.toolbarItemGridStep = new global::System.Windows.Forms.ToolStripButton();
			this.toolStripSeparator02 = new global::System.Windows.Forms.ToolStripSeparator();
			this.toolbarItemLockSelection = new global::System.Windows.Forms.ToolStripButton();
			this.toolbarItemPlaceOnlyOneObject = new global::System.Windows.Forms.ToolStripButton();
			this.toolStripSeparator03 = new global::System.Windows.Forms.ToolStripSeparator();
			this.toolbarItemCameraSlow = new global::System.Windows.Forms.ToolStripButton();
			this.toolbarItemCameraNormal = new global::System.Windows.Forms.ToolStripButton();
			this.toolbarItemCameraCustom = new global::System.Windows.Forms.ToolStripButton();
			this.toolStripSeparator04 = new global::System.Windows.Forms.ToolStripSeparator();
			this.toolbarItemCameraMove = new global::System.Windows.Forms.ToolStripButton();
			this.toolbarItemCameraReset = new global::System.Windows.Forms.ToolStripButton();
			this.toolStripSeparator05 = new global::System.Windows.Forms.ToolStripSeparator();
			this.toolbarItemCameraCustomSetSpeed = new global::System.Windows.Forms.ToolStripButton();
			this.toolbarItemCameraFOV = new global::System.Windows.Forms.ToolStripButton();
			this.toolStripSeparator06 = new global::System.Windows.Forms.ToolStripSeparator();
			this.toolbarItemToggleGrid = new global::System.Windows.Forms.ToolStripButton();
			this.toolbarItemToggleCollisionGeometry = new global::System.Windows.Forms.ToolStripButton();
			this.menuStrip = new global::System.Windows.Forms.MenuStrip();
			this.fileToolStripMenuItem = new global::System.Windows.Forms.ToolStripMenuItem();
			this.menuItemFileOpen = new global::System.Windows.Forms.ToolStripMenuItem();
			this.menuItemFileSave = new global::System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator1 = new global::System.Windows.Forms.ToolStripSeparator();
			this.recentFilesToolStripMenuItem = new global::System.Windows.Forms.ToolStripMenuItem();
			this.recentObjectToolStripMenuItem = new global::System.Windows.Forms.ToolStripMenuItem();
			this.recentObject1ToolStripMenuItem = new global::System.Windows.Forms.ToolStripMenuItem();
			this.recentObject2ToolStripMenuItem = new global::System.Windows.Forms.ToolStripMenuItem();
			this.recentObject3ToolStripMenuItem = new global::System.Windows.Forms.ToolStripMenuItem();
			this.recentObject4ToolStripMenuItem = new global::System.Windows.Forms.ToolStripMenuItem();
			this.recentObject5ToolStripMenuItem = new global::System.Windows.Forms.ToolStripMenuItem();
			this.recentObject6ToolStripMenuItem = new global::System.Windows.Forms.ToolStripMenuItem();
			this.recentObject7ToolStripMenuItem = new global::System.Windows.Forms.ToolStripMenuItem();
			this.recentObject8ToolStripMenuItem = new global::System.Windows.Forms.ToolStripMenuItem();
			this.recentObject9ToolStripMenuItem = new global::System.Windows.Forms.ToolStripMenuItem();
			this.menuItemFileSeparator00 = new global::System.Windows.Forms.ToolStripSeparator();
			this.menuItemFileGenerateIcon = new global::System.Windows.Forms.ToolStripMenuItem();
			this.menuItemFileSeparator01 = new global::System.Windows.Forms.ToolStripSeparator();
			this.menuItemFileClose = new global::System.Windows.Forms.ToolStripMenuItem();
			this.menuItemEditUndo = new global::System.Windows.Forms.ToolStripMenuItem();
			this.undoToolStripMenuItem = new global::System.Windows.Forms.ToolStripMenuItem();
			this.menuItemEditRedo = new global::System.Windows.Forms.ToolStripMenuItem();
			this.menuItemEditSeparator00 = new global::System.Windows.Forms.ToolStripSeparator();
			this.menuItemEditCut = new global::System.Windows.Forms.ToolStripMenuItem();
			this.menuItemEditCopy = new global::System.Windows.Forms.ToolStripMenuItem();
			this.menuItemEditPaste = new global::System.Windows.Forms.ToolStripMenuItem();
			this.menuItemEditDelete = new global::System.Windows.Forms.ToolStripMenuItem();
			this.menuItemEditSeparator01 = new global::System.Windows.Forms.ToolStripSeparator();
			this.menuItemEditResetAltitude = new global::System.Windows.Forms.ToolStripMenuItem();
			this.menuItemEditDropToNearest = new global::System.Windows.Forms.ToolStripMenuItem();
			this.menuItemEditResetRotation = new global::System.Windows.Forms.ToolStripMenuItem();
			this.menuItemEditResetScale = new global::System.Windows.Forms.ToolStripMenuItem();
			this.menuItemEditSeparator02 = new global::System.Windows.Forms.ToolStripSeparator();
			this.menuItemEditProperties = new global::System.Windows.Forms.ToolStripMenuItem();
			this.selectorToolStripMenuItem = new global::System.Windows.Forms.ToolStripMenuItem();
			this.menuItemSelectorMove = new global::System.Windows.Forms.ToolStripMenuItem();
			this.menuItemSelectorRotate = new global::System.Windows.Forms.ToolStripMenuItem();
			this.menuItemSelectorScale = new global::System.Windows.Forms.ToolStripMenuItem();
			this.menuItemSelectorSeparator00 = new global::System.Windows.Forms.ToolStripSeparator();
			this.menuItemSelectorScaleObjectOriented = new global::System.Windows.Forms.ToolStripMenuItem();
			this.menuItemSelectorAlignToGrid = new global::System.Windows.Forms.ToolStripMenuItem();
			this.menuItemSelectorLockSelection = new global::System.Windows.Forms.ToolStripMenuItem();
			this.menuItemSelectorEditObjectAfterAdd = new global::System.Windows.Forms.ToolStripMenuItem();
			this.viewToolStripMenuItem = new global::System.Windows.Forms.ToolStripMenuItem();
			this.menuItemGrid = new global::System.Windows.Forms.ToolStripMenuItem();
			this.menuItemGridCollisionGeometry = new global::System.Windows.Forms.ToolStripMenuItem();
			this.menuItemViewSeparator00 = new global::System.Windows.Forms.ToolStripSeparator();
			this.menuItemViewSetBacgroundColor = new global::System.Windows.Forms.ToolStripMenuItem();
			this.menuItemViewSeparator01 = new global::System.Windows.Forms.ToolStripSeparator();
			this.menuItemViewToolbar = new global::System.Windows.Forms.ToolStripMenuItem();
			this.menuItemViewStatusbar = new global::System.Windows.Forms.ToolStripMenuItem();
			this.cameraToolStripMenuItem = new global::System.Windows.Forms.ToolStripMenuItem();
			this.menuItemCameraSlow = new global::System.Windows.Forms.ToolStripMenuItem();
			this.menuItemCameraMedium = new global::System.Windows.Forms.ToolStripMenuItem();
			this.menuItemCameraCustom = new global::System.Windows.Forms.ToolStripMenuItem();
			this.menuItemCameraSeparator00 = new global::System.Windows.Forms.ToolStripSeparator();
			this.menuItemCameraSetCustomSpeed = new global::System.Windows.Forms.ToolStripMenuItem();
			this.setCameraFovToolStripMenuItem = new global::System.Windows.Forms.ToolStripMenuItem();
			this.menuItemCameraSeparator01 = new global::System.Windows.Forms.ToolStripSeparator();
			this.menuItemCameraMove = new global::System.Windows.Forms.ToolStripMenuItem();
			this.menuItemCameraReset = new global::System.Windows.Forms.ToolStripMenuItem();
			this.statusStrip = new global::System.Windows.Forms.StatusStrip();
			this.StatusbarMessge = new global::System.Windows.Forms.ToolStripStatusLabel();
			this.StatusbarPosition = new global::System.Windows.Forms.ToolStripStatusLabel();
			this.saveAsToolStripMenuItem = new global::System.Windows.Forms.ToolStripMenuItem();
			this.toolStripButton1 = new global::System.Windows.Forms.ToolStripButton();
			this.toolStripSeparator2 = new global::System.Windows.Forms.ToolStripSeparator();
			this.toolStrip.SuspendLayout();
			this.menuStrip.SuspendLayout();
			this.statusStrip.SuspendLayout();
			base.SuspendLayout();
			this.sceneDrawTimer.Tick += new global::System.EventHandler(this.sceneDrawTimer_Tick);
			this.toolStrip.Items.AddRange(new global::System.Windows.Forms.ToolStripItem[]
			{
				this.toolbarItemOpenMap,
				this.toolbarItemSaveMap,
				this.toolStripButton1,
				this.toolStripSeparator2,
				this.toolbarItemGenerateIcon,
				this.toolStripSeparator00,
				this.toolbarItemUndo,
				this.toolbarItemRedo,
				this.toolStripSeparator01,
				this.toolbarItemMoveWidget,
				this.toolbarItemRotateWidget,
				this.toolbarItemScaleWidget,
				this.toolbarItemObjectOriented,
				this.toolbarItemAlignToGrid,
				this.toolbarItemGridStep,
				this.toolStripSeparator02,
				this.toolbarItemLockSelection,
				this.toolbarItemPlaceOnlyOneObject,
				this.toolStripSeparator03,
				this.toolbarItemCameraSlow,
				this.toolbarItemCameraNormal,
				this.toolbarItemCameraCustom,
				this.toolStripSeparator04,
				this.toolbarItemCameraMove,
				this.toolbarItemCameraReset,
				this.toolStripSeparator05,
				this.toolbarItemCameraCustomSetSpeed,
				this.toolbarItemCameraFOV,
				this.toolStripSeparator06,
				this.toolbarItemToggleGrid,
				this.toolbarItemToggleCollisionGeometry
			});
			resources.ApplyResources(this.toolStrip, "toolStrip");
			this.toolStrip.Name = "toolStrip";
			this.toolbarItemOpenMap.DisplayStyle = global::System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.toolbarItemOpenMap.Image = global::MapEditor.Properties.Resources.open;
			resources.ApplyResources(this.toolbarItemOpenMap, "toolbarItemOpenMap");
			this.toolbarItemOpenMap.Name = "toolbarItemOpenMap";
			this.toolbarItemOpenMap.Tag = "open_map";
			this.toolbarItemSaveMap.DisplayStyle = global::System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.toolbarItemSaveMap.Image = global::MapEditor.Properties.Resources.save;
			resources.ApplyResources(this.toolbarItemSaveMap, "toolbarItemSaveMap");
			this.toolbarItemSaveMap.Name = "toolbarItemSaveMap";
			this.toolbarItemSaveMap.Tag = "save_map";
			this.toolbarItemGenerateIcon.DisplayStyle = global::System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			resources.ApplyResources(this.toolbarItemGenerateIcon, "toolbarItemGenerateIcon");
			this.toolbarItemGenerateIcon.Name = "toolbarItemGenerateIcon";
			this.toolbarItemGenerateIcon.Tag = "generate_object_icon";
			this.toolStripSeparator00.Name = "toolStripSeparator00";
			resources.ApplyResources(this.toolStripSeparator00, "toolStripSeparator00");
			this.toolbarItemUndo.DisplayStyle = global::System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.toolbarItemUndo.Image = global::MapEditor.Properties.Resources.undo;
			resources.ApplyResources(this.toolbarItemUndo, "toolbarItemUndo");
			this.toolbarItemUndo.Name = "toolbarItemUndo";
			this.toolbarItemUndo.Tag = "undo";
			this.toolbarItemRedo.DisplayStyle = global::System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.toolbarItemRedo.Image = global::MapEditor.Properties.Resources.redo;
			resources.ApplyResources(this.toolbarItemRedo, "toolbarItemRedo");
			this.toolbarItemRedo.Name = "toolbarItemRedo";
			this.toolbarItemRedo.Tag = "redo";
			this.toolStripSeparator01.Name = "toolStripSeparator01";
			resources.ApplyResources(this.toolStripSeparator01, "toolStripSeparator01");
			this.toolbarItemMoveWidget.DisplayStyle = global::System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.toolbarItemMoveWidget.Image = global::MapEditor.Properties.Resources.move_widget;
			resources.ApplyResources(this.toolbarItemMoveWidget, "toolbarItemMoveWidget");
			this.toolbarItemMoveWidget.Name = "toolbarItemMoveWidget";
			this.toolbarItemMoveWidget.Tag = "selector_move";
			this.toolbarItemRotateWidget.DisplayStyle = global::System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.toolbarItemRotateWidget.Image = global::MapEditor.Properties.Resources.rotate_widget;
			resources.ApplyResources(this.toolbarItemRotateWidget, "toolbarItemRotateWidget");
			this.toolbarItemRotateWidget.Name = "toolbarItemRotateWidget";
			this.toolbarItemRotateWidget.Tag = "selector_rotate";
			this.toolbarItemScaleWidget.DisplayStyle = global::System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.toolbarItemScaleWidget.Image = global::MapEditor.Properties.Resources.scale_widget;
			resources.ApplyResources(this.toolbarItemScaleWidget, "toolbarItemScaleWidget");
			this.toolbarItemScaleWidget.Name = "toolbarItemScaleWidget";
			this.toolbarItemScaleWidget.Tag = "selector_scale";
			this.toolbarItemObjectOriented.DisplayStyle = global::System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.toolbarItemObjectOriented.Image = global::MapEditor.Properties.Resources.object_oriented;
			resources.ApplyResources(this.toolbarItemObjectOriented, "toolbarItemObjectOriented");
			this.toolbarItemObjectOriented.Name = "toolbarItemObjectOriented";
			this.toolbarItemObjectOriented.Tag = "selector_object_oriented";
			this.toolbarItemAlignToGrid.DisplayStyle = global::System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.toolbarItemAlignToGrid.Image = global::MapEditor.Properties.Resources.align_to_grid;
			resources.ApplyResources(this.toolbarItemAlignToGrid, "toolbarItemAlignToGrid");
			this.toolbarItemAlignToGrid.Name = "toolbarItemAlignToGrid";
			this.toolbarItemAlignToGrid.Tag = "selector_align_to_grid";
			this.toolbarItemGridStep.Image = global::MapEditor.Properties.Resources.grid_step;
			resources.ApplyResources(this.toolbarItemGridStep, "toolbarItemGridStep");
			this.toolbarItemGridStep.Name = "toolbarItemGridStep";
			this.toolbarItemGridStep.Tag = "selector_grid_step";
			this.toolStripSeparator02.Name = "toolStripSeparator02";
			resources.ApplyResources(this.toolStripSeparator02, "toolStripSeparator02");
			this.toolbarItemLockSelection.DisplayStyle = global::System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.toolbarItemLockSelection.Image = global::MapEditor.Properties.Resources.lock_selection;
			resources.ApplyResources(this.toolbarItemLockSelection, "toolbarItemLockSelection");
			this.toolbarItemLockSelection.Name = "toolbarItemLockSelection";
			this.toolbarItemLockSelection.Tag = "selector_lock_selection";
			this.toolbarItemPlaceOnlyOneObject.DisplayStyle = global::System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.toolbarItemPlaceOnlyOneObject.Image = global::MapEditor.Properties.Resources.edit_object_after_add;
			resources.ApplyResources(this.toolbarItemPlaceOnlyOneObject, "toolbarItemPlaceOnlyOneObject");
			this.toolbarItemPlaceOnlyOneObject.Name = "toolbarItemPlaceOnlyOneObject";
			this.toolbarItemPlaceOnlyOneObject.Tag = "selector_edit_object_after_add";
			this.toolStripSeparator03.Name = "toolStripSeparator03";
			resources.ApplyResources(this.toolStripSeparator03, "toolStripSeparator03");
			this.toolbarItemCameraSlow.DisplayStyle = global::System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.toolbarItemCameraSlow.Image = global::MapEditor.Properties.Resources.camera_slow;
			resources.ApplyResources(this.toolbarItemCameraSlow, "toolbarItemCameraSlow");
			this.toolbarItemCameraSlow.Name = "toolbarItemCameraSlow";
			this.toolbarItemCameraSlow.Tag = "camera_slow";
			this.toolbarItemCameraNormal.DisplayStyle = global::System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.toolbarItemCameraNormal.Image = global::MapEditor.Properties.Resources.camera_normal;
			resources.ApplyResources(this.toolbarItemCameraNormal, "toolbarItemCameraNormal");
			this.toolbarItemCameraNormal.Name = "toolbarItemCameraNormal";
			this.toolbarItemCameraNormal.Tag = "camera_normal";
			this.toolbarItemCameraCustom.DisplayStyle = global::System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.toolbarItemCameraCustom.Image = global::MapEditor.Properties.Resources.camera_custom;
			resources.ApplyResources(this.toolbarItemCameraCustom, "toolbarItemCameraCustom");
			this.toolbarItemCameraCustom.Name = "toolbarItemCameraCustom";
			this.toolbarItemCameraCustom.Tag = "camera_custom";
			this.toolStripSeparator04.Name = "toolStripSeparator04";
			resources.ApplyResources(this.toolStripSeparator04, "toolStripSeparator04");
			this.toolbarItemCameraMove.DisplayStyle = global::System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.toolbarItemCameraMove.Image = global::MapEditor.Properties.Resources.camera_move;
			resources.ApplyResources(this.toolbarItemCameraMove, "toolbarItemCameraMove");
			this.toolbarItemCameraMove.Name = "toolbarItemCameraMove";
			this.toolbarItemCameraMove.Tag = "camera_move";
			this.toolbarItemCameraReset.DisplayStyle = global::System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.toolbarItemCameraReset.Image = global::MapEditor.Properties.Resources.camera_reset;
			resources.ApplyResources(this.toolbarItemCameraReset, "toolbarItemCameraReset");
			this.toolbarItemCameraReset.Name = "toolbarItemCameraReset";
			this.toolbarItemCameraReset.Tag = "camera_reset";
			this.toolStripSeparator05.Name = "toolStripSeparator05";
			resources.ApplyResources(this.toolStripSeparator05, "toolStripSeparator05");
			this.toolbarItemCameraCustomSetSpeed.DisplayStyle = global::System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.toolbarItemCameraCustomSetSpeed.Image = global::MapEditor.Properties.Resources.camera_set_custom_speed;
			resources.ApplyResources(this.toolbarItemCameraCustomSetSpeed, "toolbarItemCameraCustomSetSpeed");
			this.toolbarItemCameraCustomSetSpeed.Name = "toolbarItemCameraCustomSetSpeed";
			this.toolbarItemCameraCustomSetSpeed.Tag = "camera_speed";
			this.toolbarItemCameraFOV.DisplayStyle = global::System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.toolbarItemCameraFOV.Image = global::MapEditor.Properties.Resources.camera_fov;
			resources.ApplyResources(this.toolbarItemCameraFOV, "toolbarItemCameraFOV");
			this.toolbarItemCameraFOV.Name = "toolbarItemCameraFOV";
			this.toolbarItemCameraFOV.Tag = "camera_fov";
			this.toolStripSeparator06.Name = "toolStripSeparator06";
			resources.ApplyResources(this.toolStripSeparator06, "toolStripSeparator06");
			this.toolbarItemToggleGrid.DisplayStyle = global::System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			resources.ApplyResources(this.toolbarItemToggleGrid, "toolbarItemToggleGrid");
			this.toolbarItemToggleGrid.Name = "toolbarItemToggleGrid";
			this.toolbarItemToggleGrid.Tag = "toggle_grid";
			this.toolbarItemToggleCollisionGeometry.DisplayStyle = global::System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.toolbarItemToggleCollisionGeometry.Image = global::MapEditor.Properties.Resources.collision_geometry;
			resources.ApplyResources(this.toolbarItemToggleCollisionGeometry, "toolbarItemToggleCollisionGeometry");
			this.toolbarItemToggleCollisionGeometry.Name = "toolbarItemToggleCollisionGeometry";
			this.toolbarItemToggleCollisionGeometry.Tag = "toggle_collision_geometry";
			this.menuStrip.Items.AddRange(new global::System.Windows.Forms.ToolStripItem[]
			{
				this.fileToolStripMenuItem,
				this.menuItemEditUndo,
				this.selectorToolStripMenuItem,
				this.viewToolStripMenuItem,
				this.cameraToolStripMenuItem
			});
			resources.ApplyResources(this.menuStrip, "menuStrip");
			this.menuStrip.Name = "menuStrip";
			this.fileToolStripMenuItem.DropDownItems.AddRange(new global::System.Windows.Forms.ToolStripItem[]
			{
				this.menuItemFileOpen,
				this.menuItemFileSave,
				this.saveAsToolStripMenuItem,
				this.toolStripSeparator1,
				this.recentFilesToolStripMenuItem,
				this.menuItemFileSeparator00,
				this.menuItemFileGenerateIcon,
				this.menuItemFileSeparator01,
				this.menuItemFileClose
			});
			this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
			resources.ApplyResources(this.fileToolStripMenuItem, "fileToolStripMenuItem");
			this.fileToolStripMenuItem.Tag = "generate_object_icon";
			this.menuItemFileOpen.Name = "menuItemFileOpen";
			resources.ApplyResources(this.menuItemFileOpen, "menuItemFileOpen");
			this.menuItemFileOpen.Tag = "open_map";
			this.menuItemFileSave.Name = "menuItemFileSave";
			resources.ApplyResources(this.menuItemFileSave, "menuItemFileSave");
			this.menuItemFileSave.Tag = "save_map";
			this.toolStripSeparator1.Name = "toolStripSeparator1";
			resources.ApplyResources(this.toolStripSeparator1, "toolStripSeparator1");
			this.recentFilesToolStripMenuItem.DropDownItems.AddRange(new global::System.Windows.Forms.ToolStripItem[]
			{
				this.recentObjectToolStripMenuItem,
				this.recentObject1ToolStripMenuItem,
				this.recentObject2ToolStripMenuItem,
				this.recentObject3ToolStripMenuItem,
				this.recentObject4ToolStripMenuItem,
				this.recentObject5ToolStripMenuItem,
				this.recentObject6ToolStripMenuItem,
				this.recentObject7ToolStripMenuItem,
				this.recentObject8ToolStripMenuItem,
				this.recentObject9ToolStripMenuItem
			});
			this.recentFilesToolStripMenuItem.Name = "recentFilesToolStripMenuItem";
			resources.ApplyResources(this.recentFilesToolStripMenuItem, "recentFilesToolStripMenuItem");
			this.recentObjectToolStripMenuItem.Name = "recentObjectToolStripMenuItem";
			resources.ApplyResources(this.recentObjectToolStripMenuItem, "recentObjectToolStripMenuItem");
			this.recentObjectToolStripMenuItem.Tag = "recent_object_0";
			this.recentObject1ToolStripMenuItem.Name = "recentObject1ToolStripMenuItem";
			resources.ApplyResources(this.recentObject1ToolStripMenuItem, "recentObject1ToolStripMenuItem");
			this.recentObject1ToolStripMenuItem.Tag = "recent_object_1";
			this.recentObject2ToolStripMenuItem.Name = "recentObject2ToolStripMenuItem";
			resources.ApplyResources(this.recentObject2ToolStripMenuItem, "recentObject2ToolStripMenuItem");
			this.recentObject2ToolStripMenuItem.Tag = "recent_object_2";
			this.recentObject3ToolStripMenuItem.Name = "recentObject3ToolStripMenuItem";
			resources.ApplyResources(this.recentObject3ToolStripMenuItem, "recentObject3ToolStripMenuItem");
			this.recentObject3ToolStripMenuItem.Tag = "recent_object_3";
			this.recentObject4ToolStripMenuItem.Name = "recentObject4ToolStripMenuItem";
			resources.ApplyResources(this.recentObject4ToolStripMenuItem, "recentObject4ToolStripMenuItem");
			this.recentObject4ToolStripMenuItem.Tag = "recent_object_4";
			this.recentObject5ToolStripMenuItem.Name = "recentObject5ToolStripMenuItem";
			resources.ApplyResources(this.recentObject5ToolStripMenuItem, "recentObject5ToolStripMenuItem");
			this.recentObject5ToolStripMenuItem.Tag = "recent_object_5";
			this.recentObject6ToolStripMenuItem.Name = "recentObject6ToolStripMenuItem";
			resources.ApplyResources(this.recentObject6ToolStripMenuItem, "recentObject6ToolStripMenuItem");
			this.recentObject6ToolStripMenuItem.Tag = "recent_object_6";
			this.recentObject7ToolStripMenuItem.Name = "recentObject7ToolStripMenuItem";
			resources.ApplyResources(this.recentObject7ToolStripMenuItem, "recentObject7ToolStripMenuItem");
			this.recentObject7ToolStripMenuItem.Tag = "recent_object_7";
			this.recentObject8ToolStripMenuItem.Name = "recentObject8ToolStripMenuItem";
			resources.ApplyResources(this.recentObject8ToolStripMenuItem, "recentObject8ToolStripMenuItem");
			this.recentObject8ToolStripMenuItem.Tag = "recent_object_8";
			this.recentObject9ToolStripMenuItem.Name = "recentObject9ToolStripMenuItem";
			resources.ApplyResources(this.recentObject9ToolStripMenuItem, "recentObject9ToolStripMenuItem");
			this.recentObject9ToolStripMenuItem.Tag = "recent_object_9";
			this.menuItemFileSeparator00.Name = "menuItemFileSeparator00";
			resources.ApplyResources(this.menuItemFileSeparator00, "menuItemFileSeparator00");
			this.menuItemFileGenerateIcon.Name = "menuItemFileGenerateIcon";
			resources.ApplyResources(this.menuItemFileGenerateIcon, "menuItemFileGenerateIcon");
			this.menuItemFileGenerateIcon.Tag = "generate_object_icon";
			this.menuItemFileSeparator01.Name = "menuItemFileSeparator01";
			resources.ApplyResources(this.menuItemFileSeparator01, "menuItemFileSeparator01");
			this.menuItemFileClose.Name = "menuItemFileClose";
			resources.ApplyResources(this.menuItemFileClose, "menuItemFileClose");
			this.menuItemFileClose.Tag = "close_object_editor";
			this.menuItemEditUndo.DropDownItems.AddRange(new global::System.Windows.Forms.ToolStripItem[]
			{
				this.undoToolStripMenuItem,
				this.menuItemEditRedo,
				this.menuItemEditSeparator00,
				this.menuItemEditCut,
				this.menuItemEditCopy,
				this.menuItemEditPaste,
				this.menuItemEditDelete,
				this.menuItemEditSeparator01,
				this.menuItemEditResetAltitude,
				this.menuItemEditDropToNearest,
				this.menuItemEditResetRotation,
				this.menuItemEditResetScale,
				this.menuItemEditSeparator02,
				this.menuItemEditProperties
			});
			this.menuItemEditUndo.Name = "menuItemEditUndo";
			resources.ApplyResources(this.menuItemEditUndo, "menuItemEditUndo");
			this.undoToolStripMenuItem.Name = "undoToolStripMenuItem";
			resources.ApplyResources(this.undoToolStripMenuItem, "undoToolStripMenuItem");
			this.undoToolStripMenuItem.Tag = "undo";
			this.menuItemEditRedo.Name = "menuItemEditRedo";
			resources.ApplyResources(this.menuItemEditRedo, "menuItemEditRedo");
			this.menuItemEditRedo.Tag = "redo";
			this.menuItemEditSeparator00.Name = "menuItemEditSeparator00";
			resources.ApplyResources(this.menuItemEditSeparator00, "menuItemEditSeparator00");
			this.menuItemEditCut.Name = "menuItemEditCut";
			resources.ApplyResources(this.menuItemEditCut, "menuItemEditCut");
			this.menuItemEditCut.Tag = "cut";
			this.menuItemEditCopy.Name = "menuItemEditCopy";
			resources.ApplyResources(this.menuItemEditCopy, "menuItemEditCopy");
			this.menuItemEditCopy.Tag = "copy";
			this.menuItemEditPaste.Name = "menuItemEditPaste";
			resources.ApplyResources(this.menuItemEditPaste, "menuItemEditPaste");
			this.menuItemEditPaste.Tag = "paste";
			this.menuItemEditDelete.Name = "menuItemEditDelete";
			resources.ApplyResources(this.menuItemEditDelete, "menuItemEditDelete");
			this.menuItemEditDelete.Tag = "delete";
			this.menuItemEditSeparator01.Name = "menuItemEditSeparator01";
			resources.ApplyResources(this.menuItemEditSeparator01, "menuItemEditSeparator01");
			this.menuItemEditResetAltitude.Name = "menuItemEditResetAltitude";
			resources.ApplyResources(this.menuItemEditResetAltitude, "menuItemEditResetAltitude");
			this.menuItemEditResetAltitude.Tag = "altitude_reset";
			this.menuItemEditDropToNearest.Name = "menuItemEditDropToNearest";
			resources.ApplyResources(this.menuItemEditDropToNearest, "menuItemEditDropToNearest");
			this.menuItemEditDropToNearest.Tag = "drop_to_nearest";
			this.menuItemEditResetRotation.Name = "menuItemEditResetRotation";
			resources.ApplyResources(this.menuItemEditResetRotation, "menuItemEditResetRotation");
			this.menuItemEditResetRotation.Tag = "rotation_reset";
			this.menuItemEditResetScale.Name = "menuItemEditResetScale";
			resources.ApplyResources(this.menuItemEditResetScale, "menuItemEditResetScale");
			this.menuItemEditResetScale.Tag = "scale_reset";
			this.menuItemEditSeparator02.Name = "menuItemEditSeparator02";
			resources.ApplyResources(this.menuItemEditSeparator02, "menuItemEditSeparator02");
			this.menuItemEditProperties.Name = "menuItemEditProperties";
			resources.ApplyResources(this.menuItemEditProperties, "menuItemEditProperties");
			this.menuItemEditProperties.Tag = "properties";
			this.selectorToolStripMenuItem.DropDownItems.AddRange(new global::System.Windows.Forms.ToolStripItem[]
			{
				this.menuItemSelectorMove,
				this.menuItemSelectorRotate,
				this.menuItemSelectorScale,
				this.menuItemSelectorSeparator00,
				this.menuItemSelectorScaleObjectOriented,
				this.menuItemSelectorAlignToGrid,
				this.menuItemSelectorLockSelection,
				this.menuItemSelectorEditObjectAfterAdd
			});
			this.selectorToolStripMenuItem.Name = "selectorToolStripMenuItem";
			resources.ApplyResources(this.selectorToolStripMenuItem, "selectorToolStripMenuItem");
			this.menuItemSelectorMove.Name = "menuItemSelectorMove";
			resources.ApplyResources(this.menuItemSelectorMove, "menuItemSelectorMove");
			this.menuItemSelectorMove.Tag = "selector_move";
			this.menuItemSelectorRotate.Name = "menuItemSelectorRotate";
			resources.ApplyResources(this.menuItemSelectorRotate, "menuItemSelectorRotate");
			this.menuItemSelectorRotate.Tag = "selector_rotate";
			this.menuItemSelectorScale.Name = "menuItemSelectorScale";
			resources.ApplyResources(this.menuItemSelectorScale, "menuItemSelectorScale");
			this.menuItemSelectorScale.Tag = "selector_scale";
			this.menuItemSelectorSeparator00.Name = "menuItemSelectorSeparator00";
			resources.ApplyResources(this.menuItemSelectorSeparator00, "menuItemSelectorSeparator00");
			this.menuItemSelectorScaleObjectOriented.Name = "menuItemSelectorScaleObjectOriented";
			resources.ApplyResources(this.menuItemSelectorScaleObjectOriented, "menuItemSelectorScaleObjectOriented");
			this.menuItemSelectorScaleObjectOriented.Tag = "selector_object_oriented";
			this.menuItemSelectorAlignToGrid.Name = "menuItemSelectorAlignToGrid";
			resources.ApplyResources(this.menuItemSelectorAlignToGrid, "menuItemSelectorAlignToGrid");
			this.menuItemSelectorAlignToGrid.Tag = "selector_align_to_grid";
			this.menuItemSelectorLockSelection.Name = "menuItemSelectorLockSelection";
			resources.ApplyResources(this.menuItemSelectorLockSelection, "menuItemSelectorLockSelection");
			this.menuItemSelectorLockSelection.Tag = "selector_lock_selection";
			this.menuItemSelectorEditObjectAfterAdd.Name = "menuItemSelectorEditObjectAfterAdd";
			resources.ApplyResources(this.menuItemSelectorEditObjectAfterAdd, "menuItemSelectorEditObjectAfterAdd");
			this.menuItemSelectorEditObjectAfterAdd.Tag = "selector_edit_object_after_add";
			this.viewToolStripMenuItem.DropDownItems.AddRange(new global::System.Windows.Forms.ToolStripItem[]
			{
				this.menuItemGrid,
				this.menuItemGridCollisionGeometry,
				this.menuItemViewSeparator00,
				this.menuItemViewSetBacgroundColor,
				this.menuItemViewSeparator01,
				this.menuItemViewToolbar,
				this.menuItemViewStatusbar
			});
			this.viewToolStripMenuItem.Name = "viewToolStripMenuItem";
			resources.ApplyResources(this.viewToolStripMenuItem, "viewToolStripMenuItem");
			this.menuItemGrid.Name = "menuItemGrid";
			resources.ApplyResources(this.menuItemGrid, "menuItemGrid");
			this.menuItemGrid.Tag = "toggle_grid";
			this.menuItemGridCollisionGeometry.Name = "menuItemGridCollisionGeometry";
			resources.ApplyResources(this.menuItemGridCollisionGeometry, "menuItemGridCollisionGeometry");
			this.menuItemGridCollisionGeometry.Tag = "toggle_collision_geometry";
			this.menuItemViewSeparator00.Name = "menuItemViewSeparator00";
			resources.ApplyResources(this.menuItemViewSeparator00, "menuItemViewSeparator00");
			this.menuItemViewSetBacgroundColor.Name = "menuItemViewSetBacgroundColor";
			resources.ApplyResources(this.menuItemViewSetBacgroundColor, "menuItemViewSetBacgroundColor");
			this.menuItemViewSetBacgroundColor.Tag = "set_obj_editor_background_color";
			this.menuItemViewSeparator01.Name = "menuItemViewSeparator01";
			resources.ApplyResources(this.menuItemViewSeparator01, "menuItemViewSeparator01");
			this.menuItemViewToolbar.Name = "menuItemViewToolbar";
			resources.ApplyResources(this.menuItemViewToolbar, "menuItemViewToolbar");
			this.menuItemViewToolbar.Tag = "toggle_toolbar";
			this.menuItemViewStatusbar.Name = "menuItemViewStatusbar";
			resources.ApplyResources(this.menuItemViewStatusbar, "menuItemViewStatusbar");
			this.menuItemViewStatusbar.Tag = "toggle_statusbar";
			this.cameraToolStripMenuItem.DropDownItems.AddRange(new global::System.Windows.Forms.ToolStripItem[]
			{
				this.menuItemCameraSlow,
				this.menuItemCameraMedium,
				this.menuItemCameraCustom,
				this.menuItemCameraSeparator00,
				this.menuItemCameraSetCustomSpeed,
				this.setCameraFovToolStripMenuItem,
				this.menuItemCameraSeparator01,
				this.menuItemCameraMove,
				this.menuItemCameraReset
			});
			this.cameraToolStripMenuItem.Name = "cameraToolStripMenuItem";
			resources.ApplyResources(this.cameraToolStripMenuItem, "cameraToolStripMenuItem");
			this.menuItemCameraSlow.Name = "menuItemCameraSlow";
			resources.ApplyResources(this.menuItemCameraSlow, "menuItemCameraSlow");
			this.menuItemCameraSlow.Tag = "camera_slow";
			this.menuItemCameraMedium.Name = "menuItemCameraMedium";
			resources.ApplyResources(this.menuItemCameraMedium, "menuItemCameraMedium");
			this.menuItemCameraMedium.Tag = "camera_normal";
			this.menuItemCameraCustom.Name = "menuItemCameraCustom";
			resources.ApplyResources(this.menuItemCameraCustom, "menuItemCameraCustom");
			this.menuItemCameraCustom.Tag = "camera_custom";
			this.menuItemCameraSeparator00.Name = "menuItemCameraSeparator00";
			resources.ApplyResources(this.menuItemCameraSeparator00, "menuItemCameraSeparator00");
			this.menuItemCameraSetCustomSpeed.Name = "menuItemCameraSetCustomSpeed";
			resources.ApplyResources(this.menuItemCameraSetCustomSpeed, "menuItemCameraSetCustomSpeed");
			this.menuItemCameraSetCustomSpeed.Tag = "camera_speed";
			this.setCameraFovToolStripMenuItem.Name = "setCameraFovToolStripMenuItem";
			resources.ApplyResources(this.setCameraFovToolStripMenuItem, "setCameraFovToolStripMenuItem");
			this.setCameraFovToolStripMenuItem.Tag = "camera_fov";
			this.menuItemCameraSeparator01.Name = "menuItemCameraSeparator01";
			resources.ApplyResources(this.menuItemCameraSeparator01, "menuItemCameraSeparator01");
			this.menuItemCameraMove.Name = "menuItemCameraMove";
			resources.ApplyResources(this.menuItemCameraMove, "menuItemCameraMove");
			this.menuItemCameraMove.Tag = "camera_move";
			this.menuItemCameraReset.Name = "menuItemCameraReset";
			resources.ApplyResources(this.menuItemCameraReset, "menuItemCameraReset");
			this.menuItemCameraReset.Tag = "camera_reset";
			this.statusStrip.Items.AddRange(new global::System.Windows.Forms.ToolStripItem[]
			{
				this.StatusbarMessge,
				this.StatusbarPosition
			});
			resources.ApplyResources(this.statusStrip, "statusStrip");
			this.statusStrip.Name = "statusStrip";
			this.statusStrip.Resize += new global::System.EventHandler(this.statusStrip_Resize);
			resources.ApplyResources(this.StatusbarMessge, "StatusbarMessge");
			this.StatusbarMessge.BorderSides = (global::System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | global::System.Windows.Forms.ToolStripStatusLabelBorderSides.Right | global::System.Windows.Forms.ToolStripStatusLabelBorderSides.Top);
			this.StatusbarMessge.BorderStyle = global::System.Windows.Forms.Border3DStyle.SunkenOuter;
			this.StatusbarMessge.Name = "StatusbarMessge";
			resources.ApplyResources(this.StatusbarPosition, "StatusbarPosition");
			this.StatusbarPosition.BorderSides = (global::System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | global::System.Windows.Forms.ToolStripStatusLabelBorderSides.Right | global::System.Windows.Forms.ToolStripStatusLabelBorderSides.Top);
			this.StatusbarPosition.BorderStyle = global::System.Windows.Forms.Border3DStyle.SunkenOuter;
			this.StatusbarPosition.Name = "StatusbarPosition";
			this.saveAsToolStripMenuItem.Name = "saveAsToolStripMenuItem";
			resources.ApplyResources(this.saveAsToolStripMenuItem, "saveAsToolStripMenuItem");
			this.saveAsToolStripMenuItem.Tag = "toggle_save_as";
			this.toolStripButton1.DisplayStyle = global::System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			resources.ApplyResources(this.toolStripButton1, "toolStripButton1");
			this.toolStripButton1.Name = "toolStripButton1";
			this.toolStripButton1.Tag = "toggle_save_as";
			this.toolStripSeparator2.Name = "toolStripSeparator2";
			resources.ApplyResources(this.toolStripSeparator2, "toolStripSeparator2");
			resources.ApplyResources(this, "$this");
			base.AutoScaleMode = global::System.Windows.Forms.AutoScaleMode.Font;
			base.Controls.Add(this.statusStrip);
			base.Controls.Add(this.toolStrip);
			base.Controls.Add(this.menuStrip);
			base.MainMenuStrip = this.menuStrip;
			base.Name = "ObjectEditorForm";
			base.Load += new global::System.EventHandler(this.ObjectEditorForm_Load);
			base.FormClosed += new global::System.Windows.Forms.FormClosedEventHandler(this.ObjectEditorForm_FormClosed);
			base.Resize += new global::System.EventHandler(this.ObjectEditorForm_Resize);
			this.toolStrip.ResumeLayout(false);
			this.toolStrip.PerformLayout();
			this.menuStrip.ResumeLayout(false);
			this.menuStrip.PerformLayout();
			this.statusStrip.ResumeLayout(false);
			this.statusStrip.PerformLayout();
			base.ResumeLayout(false);
			base.PerformLayout();
		}

		// Token: 0x04000313 RID: 787
		private global::System.ComponentModel.IContainer components;

		// Token: 0x04000314 RID: 788
		private global::System.Windows.Forms.Timer sceneDrawTimer;

		// Token: 0x04000315 RID: 789
		private global::System.Windows.Forms.ToolStrip toolStrip;

		// Token: 0x04000316 RID: 790
		private global::System.Windows.Forms.ToolStripSeparator toolStripSeparator00;

		// Token: 0x04000317 RID: 791
		private global::System.Windows.Forms.ToolStripSeparator toolStripSeparator02;

		// Token: 0x04000318 RID: 792
		private global::System.Windows.Forms.ToolStripSeparator toolStripSeparator03;

		// Token: 0x04000319 RID: 793
		private global::System.Windows.Forms.ToolStripSeparator toolStripSeparator01;

		// Token: 0x0400031A RID: 794
		private global::System.Windows.Forms.ToolStripButton toolbarItemCameraSlow;

		// Token: 0x0400031B RID: 795
		private global::System.Windows.Forms.ToolStripButton toolbarItemCameraNormal;

		// Token: 0x0400031C RID: 796
		private global::System.Windows.Forms.ToolStripButton toolbarItemMoveWidget;

		// Token: 0x0400031D RID: 797
		private global::System.Windows.Forms.ToolStripButton toolbarItemRotateWidget;

		// Token: 0x0400031E RID: 798
		private global::System.Windows.Forms.ToolStripButton toolbarItemScaleWidget;

		// Token: 0x0400031F RID: 799
		private global::System.Windows.Forms.ToolStripButton toolbarItemRedo;

		// Token: 0x04000320 RID: 800
		private global::System.Windows.Forms.ToolStripButton toolbarItemUndo;

		// Token: 0x04000321 RID: 801
		private global::System.Windows.Forms.ToolStripButton toolbarItemSaveMap;

		// Token: 0x04000322 RID: 802
		private global::System.Windows.Forms.ToolStripButton toolbarItemOpenMap;

		// Token: 0x04000323 RID: 803
		private global::System.Windows.Forms.ToolStripButton toolbarItemCameraReset;

		// Token: 0x04000324 RID: 804
		private global::System.Windows.Forms.ToolStripButton toolbarItemCameraFOV;

		// Token: 0x04000325 RID: 805
		private global::System.Windows.Forms.MenuStrip menuStrip;

		// Token: 0x04000326 RID: 806
		private global::System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;

		// Token: 0x04000327 RID: 807
		private global::System.Windows.Forms.ToolStripMenuItem menuItemFileOpen;

		// Token: 0x04000328 RID: 808
		private global::System.Windows.Forms.ToolStripMenuItem menuItemFileSave;

		// Token: 0x04000329 RID: 809
		private global::System.Windows.Forms.ToolStripMenuItem menuItemEditUndo;

		// Token: 0x0400032A RID: 810
		private global::System.Windows.Forms.ToolStripMenuItem cameraToolStripMenuItem;

		// Token: 0x0400032B RID: 811
		private global::System.Windows.Forms.ToolStripMenuItem undoToolStripMenuItem;

		// Token: 0x0400032C RID: 812
		private global::System.Windows.Forms.ToolStripMenuItem menuItemEditRedo;

		// Token: 0x0400032D RID: 813
		private global::System.Windows.Forms.ToolStripMenuItem menuItemCameraSlow;

		// Token: 0x0400032E RID: 814
		private global::System.Windows.Forms.ToolStripMenuItem menuItemCameraMedium;

		// Token: 0x0400032F RID: 815
		private global::System.Windows.Forms.ToolStripSeparator menuItemCameraSeparator00;

		// Token: 0x04000330 RID: 816
		private global::System.Windows.Forms.ToolStripMenuItem menuItemCameraReset;

		// Token: 0x04000331 RID: 817
		private global::System.Windows.Forms.ToolStripSeparator menuItemFileSeparator00;

		// Token: 0x04000332 RID: 818
		private global::System.Windows.Forms.ToolStripMenuItem menuItemFileClose;

		// Token: 0x04000333 RID: 819
		private global::System.Windows.Forms.ToolStripSeparator menuItemEditSeparator00;

		// Token: 0x04000334 RID: 820
		private global::System.Windows.Forms.ToolStripMenuItem menuItemEditCopy;

		// Token: 0x04000335 RID: 821
		private global::System.Windows.Forms.ToolStripMenuItem menuItemEditPaste;

		// Token: 0x04000336 RID: 822
		private global::System.Windows.Forms.ToolStripMenuItem menuItemEditDelete;

		// Token: 0x04000337 RID: 823
		private global::System.Windows.Forms.ToolStripMenuItem menuItemEditCut;

		// Token: 0x04000338 RID: 824
		private global::System.Windows.Forms.ToolStripMenuItem viewToolStripMenuItem;

		// Token: 0x04000339 RID: 825
		private global::System.Windows.Forms.ToolStripMenuItem menuItemViewToolbar;

		// Token: 0x0400033A RID: 826
		private global::System.Windows.Forms.ToolStripSeparator menuItemViewSeparator00;

		// Token: 0x0400033B RID: 827
		private global::System.Windows.Forms.StatusStrip statusStrip;

		// Token: 0x0400033C RID: 828
		private global::System.Windows.Forms.ToolStripMenuItem menuItemViewStatusbar;

		// Token: 0x0400033D RID: 829
		private global::System.Windows.Forms.ToolStripMenuItem menuItemGrid;

		// Token: 0x0400033E RID: 830
		private global::System.Windows.Forms.ToolStripButton toolbarItemToggleGrid;

		// Token: 0x0400033F RID: 831
		private global::System.Windows.Forms.ToolStripButton toolbarItemGenerateIcon;

		// Token: 0x04000340 RID: 832
		private global::System.Windows.Forms.ToolStripMenuItem menuItemViewSetBacgroundColor;

		// Token: 0x04000341 RID: 833
		private global::System.Windows.Forms.ToolStripButton toolbarItemObjectOriented;

		// Token: 0x04000342 RID: 834
		private global::System.Windows.Forms.ToolStripButton toolbarItemAlignToGrid;

		// Token: 0x04000343 RID: 835
		private global::System.Windows.Forms.ToolStripMenuItem menuItemFileGenerateIcon;

		// Token: 0x04000344 RID: 836
		private global::System.Windows.Forms.ToolStripSeparator menuItemFileSeparator01;

		// Token: 0x04000345 RID: 837
		private global::System.Windows.Forms.ToolStripStatusLabel StatusbarMessge;

		// Token: 0x04000346 RID: 838
		private global::System.Windows.Forms.ToolStripStatusLabel StatusbarPosition;

		// Token: 0x04000347 RID: 839
		private global::System.Windows.Forms.ToolStripMenuItem menuItemGridCollisionGeometry;

		// Token: 0x04000348 RID: 840
		private global::System.Windows.Forms.ToolStripSeparator menuItemViewSeparator01;

		// Token: 0x04000349 RID: 841
		private global::System.Windows.Forms.ToolStripButton toolbarItemToggleCollisionGeometry;

		// Token: 0x0400034A RID: 842
		private global::System.Windows.Forms.ToolStripButton toolbarItemGridStep;

		// Token: 0x0400034B RID: 843
		private global::System.Windows.Forms.ToolStripMenuItem selectorToolStripMenuItem;

		// Token: 0x0400034C RID: 844
		private global::System.Windows.Forms.ToolStripMenuItem menuItemSelectorMove;

		// Token: 0x0400034D RID: 845
		private global::System.Windows.Forms.ToolStripMenuItem menuItemSelectorRotate;

		// Token: 0x0400034E RID: 846
		private global::System.Windows.Forms.ToolStripMenuItem menuItemSelectorScale;

		// Token: 0x0400034F RID: 847
		private global::System.Windows.Forms.ToolStripSeparator menuItemSelectorSeparator00;

		// Token: 0x04000350 RID: 848
		private global::System.Windows.Forms.ToolStripMenuItem menuItemSelectorScaleObjectOriented;

		// Token: 0x04000351 RID: 849
		private global::System.Windows.Forms.ToolStripMenuItem menuItemSelectorAlignToGrid;

		// Token: 0x04000352 RID: 850
		private global::System.Windows.Forms.ToolStripMenuItem menuItemSelectorLockSelection;

		// Token: 0x04000353 RID: 851
		private global::System.Windows.Forms.ToolStripMenuItem menuItemSelectorEditObjectAfterAdd;

		// Token: 0x04000354 RID: 852
		private global::System.Windows.Forms.ToolStripButton toolbarItemPlaceOnlyOneObject;

		// Token: 0x04000355 RID: 853
		private global::System.Windows.Forms.ToolStripButton toolbarItemLockSelection;

		// Token: 0x04000356 RID: 854
		private global::System.Windows.Forms.ToolStripSeparator toolStripSeparator04;

		// Token: 0x04000357 RID: 855
		private global::System.Windows.Forms.ToolStripSeparator toolStripSeparator1;

		// Token: 0x04000358 RID: 856
		private global::System.Windows.Forms.ToolStripMenuItem recentFilesToolStripMenuItem;

		// Token: 0x04000359 RID: 857
		private global::System.Windows.Forms.ToolStripMenuItem recentObjectToolStripMenuItem;

		// Token: 0x0400035A RID: 858
		private global::System.Windows.Forms.ToolStripMenuItem recentObject1ToolStripMenuItem;

		// Token: 0x0400035B RID: 859
		private global::System.Windows.Forms.ToolStripMenuItem recentObject2ToolStripMenuItem;

		// Token: 0x0400035C RID: 860
		private global::System.Windows.Forms.ToolStripMenuItem recentObject3ToolStripMenuItem;

		// Token: 0x0400035D RID: 861
		private global::System.Windows.Forms.ToolStripMenuItem recentObject4ToolStripMenuItem;

		// Token: 0x0400035E RID: 862
		private global::System.Windows.Forms.ToolStripMenuItem recentObject5ToolStripMenuItem;

		// Token: 0x0400035F RID: 863
		private global::System.Windows.Forms.ToolStripMenuItem recentObject6ToolStripMenuItem;

		// Token: 0x04000360 RID: 864
		private global::System.Windows.Forms.ToolStripMenuItem recentObject7ToolStripMenuItem;

		// Token: 0x04000361 RID: 865
		private global::System.Windows.Forms.ToolStripMenuItem recentObject8ToolStripMenuItem;

		// Token: 0x04000362 RID: 866
		private global::System.Windows.Forms.ToolStripMenuItem recentObject9ToolStripMenuItem;

		// Token: 0x04000363 RID: 867
		private global::System.Windows.Forms.ToolStripSeparator menuItemEditSeparator01;

		// Token: 0x04000364 RID: 868
		private global::System.Windows.Forms.ToolStripMenuItem menuItemEditDropToNearest;

		// Token: 0x04000365 RID: 869
		private global::System.Windows.Forms.ToolStripMenuItem menuItemEditResetRotation;

		// Token: 0x04000366 RID: 870
		private global::System.Windows.Forms.ToolStripMenuItem menuItemEditResetScale;

		// Token: 0x04000367 RID: 871
		private global::System.Windows.Forms.ToolStripSeparator menuItemEditSeparator02;

		// Token: 0x04000368 RID: 872
		private global::System.Windows.Forms.ToolStripMenuItem menuItemEditProperties;

		// Token: 0x04000369 RID: 873
		private global::System.Windows.Forms.ToolStripMenuItem menuItemEditResetAltitude;

		// Token: 0x0400036A RID: 874
		private global::System.Windows.Forms.ToolStripButton toolbarItemCameraCustom;

		// Token: 0x0400036B RID: 875
		private global::System.Windows.Forms.ToolStripButton toolbarItemCameraMove;

		// Token: 0x0400036C RID: 876
		private global::System.Windows.Forms.ToolStripButton toolbarItemCameraCustomSetSpeed;

		// Token: 0x0400036D RID: 877
		private global::System.Windows.Forms.ToolStripSeparator toolStripSeparator05;

		// Token: 0x0400036E RID: 878
		private global::System.Windows.Forms.ToolStripSeparator toolStripSeparator06;

		// Token: 0x0400036F RID: 879
		private global::System.Windows.Forms.ToolStripMenuItem menuItemCameraCustom;

		// Token: 0x04000370 RID: 880
		private global::System.Windows.Forms.ToolStripMenuItem menuItemCameraMove;

		// Token: 0x04000371 RID: 881
		private global::System.Windows.Forms.ToolStripMenuItem menuItemCameraSetCustomSpeed;

		// Token: 0x04000372 RID: 882
		private global::System.Windows.Forms.ToolStripMenuItem setCameraFovToolStripMenuItem;

		// Token: 0x04000373 RID: 883
		private global::System.Windows.Forms.ToolStripSeparator menuItemCameraSeparator01;

		// Token: 0x04000374 RID: 884
		private global::System.Windows.Forms.ToolStripButton toolStripButton1;

		// Token: 0x04000375 RID: 885
		private global::System.Windows.Forms.ToolStripSeparator toolStripSeparator2;

		// Token: 0x04000376 RID: 886
		private global::System.Windows.Forms.ToolStripMenuItem saveAsToolStripMenuItem;
	}
}
