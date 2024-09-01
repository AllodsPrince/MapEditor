namespace MapEditor.Forms.Tool
{
	// Token: 0x02000013 RID: 19
	public partial class ToolForm : global::MapEditor.Forms.Base.BaseForm, global::Tools.Landscape.ILandscapeParams, global::MapEditor.Map.States.IObjectsStateParams, global::Tools.MapObjects.IMapObjectRandomizer, global::Tools.MapObjects.IMapObjectFilter
	{
		// Token: 0x06000181 RID: 385 RVA: 0x0000A790 File Offset: 0x00009790
		protected override void Dispose(bool disposing)
		{
			if (disposing && this.components != null)
			{
				this.components.Dispose();
			}
			base.Dispose(disposing);
		}

		// Token: 0x06000182 RID: 386 RVA: 0x0000A7B0 File Offset: 0x000097B0
		private void InitializeComponent()
		{
			this.components = new global::System.ComponentModel.Container();
			global::System.ComponentModel.ComponentResourceManager resources = new global::System.ComponentModel.ComponentResourceManager(typeof(global::MapEditor.Forms.Tool.ToolForm));
			this.LandscapeHeightToolImageList = new global::System.Windows.Forms.ImageList(this.components);
			this.LandscapeCopyTypeImageList = new global::System.Windows.Forms.ImageList(this.components);
			this.LandscapeCopyHeightTypeImageList = new global::System.Windows.Forms.ImageList(this.components);
			this.ZoneComboBox = new global::System.Windows.Forms.ComboBox();
			this.MainTabImageList = new global::System.Windows.Forms.ImageList(this.components);
			this.MainTab = new global::System.Windows.Forms.TabControl();
			this.ObjectsTab = new global::System.Windows.Forms.TabPage();
			this.UnderTerrainCheckBox = new global::System.Windows.Forms.CheckBox();
			this.UnderTerrainTextBox = new global::System.Windows.Forms.TextBox();
			this.OverTerrainTextBox = new global::System.Windows.Forms.TextBox();
			this.OverTerrainCheckBox = new global::System.Windows.Forms.CheckBox();
			this.ObjectParamsLabel = new global::System.Windows.Forms.Label();
			this.RandomizeLabel = new global::System.Windows.Forms.Label();
			this.FlatRowCheckBox = new global::System.Windows.Forms.CheckBox();
			this.ObjectRowCheckBox = new global::System.Windows.Forms.CheckBox();
			this.GroupComboBox = new global::System.Windows.Forms.ComboBox();
			this.GroupCheckBox = new global::System.Windows.Forms.CheckBox();
			this.RandomObjectCheckBox = new global::System.Windows.Forms.CheckBox();
			this.ObjectTypeComboBox = new global::System.Windows.Forms.ComboBox();
			this.FilterObjectTypeCheckBox = new global::System.Windows.Forms.CheckBox();
			this.SelectionFilterLabel = new global::System.Windows.Forms.Label();
			this.FilterSelectionCheckBox = new global::System.Windows.Forms.CheckBox();
			this.RandomScaleLabel = new global::System.Windows.Forms.Label();
			this.RandomPitchLabel = new global::System.Windows.Forms.Label();
			this.RandomRotationLabel = new global::System.Windows.Forms.Label();
			this.RandomRotationToEditBox = new global::System.Windows.Forms.TextBox();
			this.RandomRotationFromEditBox = new global::System.Windows.Forms.TextBox();
			this.RandomScaleToEditBox = new global::System.Windows.Forms.TextBox();
			this.RandomScaleFromEditBox = new global::System.Windows.Forms.TextBox();
			this.RandomPitchToEditBox = new global::System.Windows.Forms.TextBox();
			this.RandomPitchFromEditBox = new global::System.Windows.Forms.TextBox();
			this.RandomScaleCheckBox = new global::System.Windows.Forms.CheckBox();
			this.RandomPitchCheckBox = new global::System.Windows.Forms.CheckBox();
			this.RandomRotationCheckBox = new global::System.Windows.Forms.CheckBox();
			this.objectsFilterEditorButton = new global::System.Windows.Forms.Button();
			this.ObjectListView = new global::System.Windows.Forms.ListView();
			this.ObjectComboBox = new global::System.Windows.Forms.ComboBox();
			this.ObjectFilterLabel = new global::System.Windows.Forms.Label();
			this.LandscapeTab = new global::System.Windows.Forms.TabPage();
			this.LandscapeRegionSideLabel = new global::System.Windows.Forms.Label();
			this.LandscapeRegionSidePanel = new global::System.Windows.Forms.Panel();
			this.LandscapeRegionSideButton02 = new global::System.Windows.Forms.RadioButton();
			this.LandscapeRegionSideImageList = new global::System.Windows.Forms.ImageList(this.components);
			this.LandscapeRegionSideButton01 = new global::System.Windows.Forms.RadioButton();
			this.LandscapeRegionSideButton00 = new global::System.Windows.Forms.RadioButton();
			this.LandscapeUseSeparateRegionsCheckBox = new global::System.Windows.Forms.CheckBox();
			this.LandscapeRegionSizeTrackBarLabel = new global::System.Windows.Forms.Label();
			this.LandscapeToolLabel = new global::System.Windows.Forms.Label();
			this.LandscapeTerrainNumberLabel = new global::System.Windows.Forms.Label();
			this.LandscapeTerrainNumberPanel = new global::System.Windows.Forms.Panel();
			this.LandscapeTerrainNumberButton01 = new global::System.Windows.Forms.RadioButton();
			this.LandscapeTerrainNumberImageList = new global::System.Windows.Forms.ImageList(this.components);
			this.LandscapeTerrainNumberButton00 = new global::System.Windows.Forms.RadioButton();
			this.LandscapeInvertHeightToolsCheckBox = new global::System.Windows.Forms.CheckBox();
			this.LandscapeRegionSizeTrackBar = new global::System.Windows.Forms.TrackBar();
			this.LandscapeRegionShapeLabel = new global::System.Windows.Forms.Label();
			this.LandscapeRegionShapePanel = new global::System.Windows.Forms.Panel();
			this.LandscapeRegionShapeButton04 = new global::System.Windows.Forms.RadioButton();
			this.LandscapeRegionShapeImageList = new global::System.Windows.Forms.ImageList(this.components);
			this.LandscapeRegionShapeButton03 = new global::System.Windows.Forms.RadioButton();
			this.LandscapeRegionShapeButton02 = new global::System.Windows.Forms.RadioButton();
			this.LandscapeRegionShapeButton01 = new global::System.Windows.Forms.RadioButton();
			this.LandscapeRegionShapeButton00 = new global::System.Windows.Forms.RadioButton();
			this.LandscapeRegionSizeEditBox = new global::System.Windows.Forms.TextBox();
			this.LandscapeRegionSizeLabel = new global::System.Windows.Forms.Label();
			this.LandscapeTabControl = new global::System.Windows.Forms.TabControl();
			this.LandscapeTabPageTile = new global::System.Windows.Forms.TabPage();
			this.LTPTileReplaceToolImage = new global::System.Windows.Forms.PictureBox();
			this.LTPTileReplaceToolTextBox = new global::System.Windows.Forms.TextBox();
			this.LTPTileReplaceToolCheckBox = new global::System.Windows.Forms.CheckBox();
			this.LTPTileAutomaticToolCheckBox = new global::System.Windows.Forms.CheckBox();
			this.LTPTileSpotToolCheckBox = new global::System.Windows.Forms.CheckBox();
			this.LTPTileSplitContainer = new global::System.Windows.Forms.SplitContainer();
			this.LTPTileAnglePictureBox = new global::System.Windows.Forms.PictureBox();
			this.LTPTileAngle01Label = new global::System.Windows.Forms.Label();
			this.LTPTileUseAngleRestrictionsCheckBox = new global::System.Windows.Forms.CheckBox();
			this.LTPTileAngle00TextBox = new global::System.Windows.Forms.TextBox();
			this.LTPTileAngle00TrackBar = new global::System.Windows.Forms.TrackBar();
			this.LTPTileAngle01TextBox = new global::System.Windows.Forms.TextBox();
			this.LTPTileAngle01TrackBar = new global::System.Windows.Forms.TrackBar();
			this.LTPTileAngle00Label = new global::System.Windows.Forms.Label();
			this.LTPTileResetStrengthSmoothButton = new global::System.Windows.Forms.Button();
			this.LTPTileStrengthSmoothTextBox = new global::System.Windows.Forms.TextBox();
			this.LTPTileStrengthSmoothPictureBox = new global::System.Windows.Forms.PictureBox();
			this.LTPTileStrengthTrackBar = new global::System.Windows.Forms.TrackBar();
			this.LTPTileSmoothTrackBar = new global::System.Windows.Forms.TrackBar();
			this.LTPTileStrengthSmoothLabel = new global::System.Windows.Forms.Label();
			this.LTPTileBrushButton = new global::System.Windows.Forms.Button();
			this.LTPTileBrushComboBox = new global::System.Windows.Forms.ComboBox();
			this.LTPTileBrushListView = new global::System.Windows.Forms.ListView();
			this.LTPTileBrushLabel = new global::System.Windows.Forms.Label();
			this.LandscapeTabPageGradient = new global::System.Windows.Forms.TabPage();
			this.LandscapeTabPageWater = new global::System.Windows.Forms.TabPage();
			this.LTPWaterBrushListView = new global::System.Windows.Forms.ListView();
			this.LTPWaterBrushButton = new global::System.Windows.Forms.Button();
			this.LTPWaterBrushComboBox = new global::System.Windows.Forms.ComboBox();
			this.label11 = new global::System.Windows.Forms.Label();
			this.LTPWaterHeightTextBox = new global::System.Windows.Forms.TextBox();
			this.LTPWaterHeightLabel = new global::System.Windows.Forms.Label();
			this.LTPWaterWaterToolPanel = new global::System.Windows.Forms.Panel();
			this.LTPWaterWaterToolButton03 = new global::System.Windows.Forms.RadioButton();
			this.LandscapeWaterToolImageList = new global::System.Windows.Forms.ImageList(this.components);
			this.LTPWaterWaterToolButton02 = new global::System.Windows.Forms.RadioButton();
			this.LTPWaterWaterToolButton01 = new global::System.Windows.Forms.RadioButton();
			this.LTPWaterWaterToolButton00 = new global::System.Windows.Forms.RadioButton();
			this.LTPWaterWaterToolLabel = new global::System.Windows.Forms.Label();
			this.LandscapeTabPageHeight = new global::System.Windows.Forms.TabPage();
			this.LTPHeightBrushButton = new global::System.Windows.Forms.Button();
			this.LTPHeightBrushComboBox = new global::System.Windows.Forms.ComboBox();
			this.LTPHeightBrushListView = new global::System.Windows.Forms.ListView();
			this.LTPHeightBrushLabel = new global::System.Windows.Forms.Label();
			this.LTPHeightSplitContainer = new global::System.Windows.Forms.SplitContainer();
			this.LTPHeightRaisePlatoCheckBox = new global::System.Windows.Forms.CheckBox();
			this.LTPHeightLowerPlatoCheckBox = new global::System.Windows.Forms.CheckBox();
			this.LTPHeightHeightTextBox = new global::System.Windows.Forms.TextBox();
			this.LTPHeightHeightLabel = new global::System.Windows.Forms.Label();
			this.LTPHeightUpdateObjectsTextBox = new global::System.Windows.Forms.TextBox();
			this.LTPHeightUpdateObjectsCheckBox = new global::System.Windows.Forms.CheckBox();
			this.LTPHeightHeightToolPanel = new global::System.Windows.Forms.Panel();
			this.LTPHeightHeightToolButton05 = new global::System.Windows.Forms.RadioButton();
			this.LTPHeightHeightToolButton04 = new global::System.Windows.Forms.RadioButton();
			this.LTPHeightHeightToolButton03 = new global::System.Windows.Forms.RadioButton();
			this.LTPHeightHeightToolButton02 = new global::System.Windows.Forms.RadioButton();
			this.LTPHeightHeightToolButton01 = new global::System.Windows.Forms.RadioButton();
			this.LTPHeightHeightToolButton00 = new global::System.Windows.Forms.RadioButton();
			this.LTPHeightHeightToolLabel = new global::System.Windows.Forms.Label();
			this.LTPHeightResetStrengthSmoothButton = new global::System.Windows.Forms.Button();
			this.LTPHeightStrengthSmoothTextBox = new global::System.Windows.Forms.TextBox();
			this.LTPHeightStrengthSmoothPictureBox = new global::System.Windows.Forms.PictureBox();
			this.LTPHeightStrengthTrackBar = new global::System.Windows.Forms.TrackBar();
			this.LTPHeightSmoothTrackBar = new global::System.Windows.Forms.TrackBar();
			this.LTPHeightStrengthSmoothLabel = new global::System.Windows.Forms.Label();
			this.LTPHeightAutomaticToolCheckBox = new global::System.Windows.Forms.CheckBox();
			this.LTPHeightPreciseToolCheckBox = new global::System.Windows.Forms.CheckBox();
			this.LandscapeTabPageClipboard = new global::System.Windows.Forms.TabPage();
			this.LTPClipboardGroupTextBox = new global::System.Windows.Forms.TextBox();
			this.LTPClipboardGroupTextBoxLabel = new global::System.Windows.Forms.Label();
			this.LTPClipboardTwoSidedCheckBox = new global::System.Windows.Forms.CheckBox();
			this.LTPClipboardBrushButton = new global::System.Windows.Forms.Button();
			this.LTPClipboardBrushComboBox = new global::System.Windows.Forms.ComboBox();
			this.LTPClipboardBrushListView = new global::System.Windows.Forms.ListView();
			this.LTPClipboardBrushLabel = new global::System.Windows.Forms.Label();
			this.LTPClipboardSplitContainer = new global::System.Windows.Forms.SplitContainer();
			this.LTPClipboardCopyTypeButton02 = new global::System.Windows.Forms.CheckBox();
			this.LTPClipboardCopyHeightTypeLabel = new global::System.Windows.Forms.Label();
			this.LTPClipboardCopyHeightTypePanel = new global::System.Windows.Forms.Panel();
			this.LTPClipboardCopyHeightTypeButton03 = new global::System.Windows.Forms.RadioButton();
			this.LTPClipboardCopyHeightTypeButton02 = new global::System.Windows.Forms.RadioButton();
			this.LTPClipboardCopyHeightTypeButton01 = new global::System.Windows.Forms.RadioButton();
			this.LTPClipboardCopyHeightTypeButton00 = new global::System.Windows.Forms.RadioButton();
			this.LTPClipboardFlipHorisontalCheckBox = new global::System.Windows.Forms.CheckBox();
			this.LTPClipboardFlipVerticalCheckBox = new global::System.Windows.Forms.CheckBox();
			this.LTPClipboardUpdateObjectsTextBox = new global::System.Windows.Forms.TextBox();
			this.LTPClipboardUpdateObjectsCheckBox = new global::System.Windows.Forms.CheckBox();
			this.LTPClipboardCopyType = new global::System.Windows.Forms.Label();
			this.LTPClipboardPreciseToolCheckBox = new global::System.Windows.Forms.CheckBox();
			this.LTPClipboardCopyTypeButton00 = new global::System.Windows.Forms.CheckBox();
			this.LTPClipboardCopyTypeButton01 = new global::System.Windows.Forms.CheckBox();
			this.LTPClipboardResetStrengthSmoothButton = new global::System.Windows.Forms.Button();
			this.LTPClipboardStrengthSmoothTextBox = new global::System.Windows.Forms.TextBox();
			this.LTPClipboardStrengthSmoothPictureBox = new global::System.Windows.Forms.PictureBox();
			this.LTPClipboardStrengthTrackBar = new global::System.Windows.Forms.TrackBar();
			this.LTPClipboardSmoothTrackBar = new global::System.Windows.Forms.TrackBar();
			this.LTPClipboardStrengthSmoothLabel = new global::System.Windows.Forms.Label();
			this.LandscapeTabPageTilePicker = new global::System.Windows.Forms.TabPage();
			this.LTPTilePickerPanel = new global::System.Windows.Forms.Panel();
			this.LTPTilePickerRadioButton03 = new global::System.Windows.Forms.RadioButton();
			this.LTPTilePickerRadioButton02 = new global::System.Windows.Forms.RadioButton();
			this.LTPTilePickerRadioButton01 = new global::System.Windows.Forms.RadioButton();
			this.LTPTilePickerRadioButton00 = new global::System.Windows.Forms.RadioButton();
			this.LTPTilePickerLabel03 = new global::System.Windows.Forms.Label();
			this.LTPTilePickerLabel02 = new global::System.Windows.Forms.Label();
			this.LTPTilePickerLabel00 = new global::System.Windows.Forms.Label();
			this.LTPTilePickerLabel01 = new global::System.Windows.Forms.Label();
			this.LTPTilePickerPictureBox03 = new global::System.Windows.Forms.PictureBox();
			this.LTPTilePickerPictureBox02 = new global::System.Windows.Forms.PictureBox();
			this.LTPTilePickerPictureBox01 = new global::System.Windows.Forms.PictureBox();
			this.LTPTilePickerPictureBox00 = new global::System.Windows.Forms.PictureBox();
			this.LandscapeTabPageHeightPicker = new global::System.Windows.Forms.TabPage();
			this.LandscapeTabPageHill = new global::System.Windows.Forms.TabPage();
			this.LTPHillSaveButton = new global::System.Windows.Forms.Button();
			this.LTPHillTwoSidedCheckBox = new global::System.Windows.Forms.CheckBox();
			this.LTPHillParamTabControl = new global::System.Windows.Forms.TabControl();
			this.HillTabPage0 = new global::System.Windows.Forms.TabPage();
			this.LTPHillPresetButton = new global::System.Windows.Forms.Button();
			this.LTPHillPresetComboBox = new global::System.Windows.Forms.ComboBox();
			this.LTPHillPresetListView = new global::System.Windows.Forms.ListView();
			this.LTPHillPresetLabel = new global::System.Windows.Forms.Label();
			this.HillTabPage1 = new global::System.Windows.Forms.TabPage();
			this.LTPHillPropertyGrid00 = new global::System.Windows.Forms.PropertyGrid();
			this.HillTabPage2 = new global::System.Windows.Forms.TabPage();
			this.LTPHillPropertyGrid01 = new global::System.Windows.Forms.PropertyGrid();
			this.LTPHillApplyButton = new global::System.Windows.Forms.Button();
			this.LTPHillUndoButton = new global::System.Windows.Forms.Button();
			this.LTPHillSetDefaultButton = new global::System.Windows.Forms.Button();
			this.LTPHillMakeButton = new global::System.Windows.Forms.Button();
			this.LandscapeTabPageRoad = new global::System.Windows.Forms.TabPage();
			this.LTPRoadSaveButton = new global::System.Windows.Forms.Button();
			this.LTPRoadTabControl = new global::System.Windows.Forms.TabControl();
			this.RoadTabPage0 = new global::System.Windows.Forms.TabPage();
			this.LTPRoadPresetButton = new global::System.Windows.Forms.Button();
			this.LTPRoadPresetComboBox = new global::System.Windows.Forms.ComboBox();
			this.LTPRoadPresetListView = new global::System.Windows.Forms.ListView();
			this.LTPRoadPresetLabel = new global::System.Windows.Forms.Label();
			this.RoadTabPage1 = new global::System.Windows.Forms.TabPage();
			this.LTPRoadPropertyGrid = new global::System.Windows.Forms.PropertyGrid();
			this.LTPRoadApplyButton = new global::System.Windows.Forms.Button();
			this.LTPRoadClearButton = new global::System.Windows.Forms.Button();
			this.LTPRoadCreateButton = new global::System.Windows.Forms.Button();
			this.LTPRoadDefaultsButton = new global::System.Windows.Forms.Button();
			this.LandscapeSubstateRightPanel = new global::System.Windows.Forms.Panel();
			this.LandscapeSubstateRightButton09 = new global::System.Windows.Forms.RadioButton();
			this.LandscapeToolImageList = new global::System.Windows.Forms.ImageList(this.components);
			this.LandscapeSubstateRightButton08 = new global::System.Windows.Forms.RadioButton();
			this.LandscapeSubstateRightButton00 = new global::System.Windows.Forms.RadioButton();
			this.LandscapeSubstateRightButton07 = new global::System.Windows.Forms.RadioButton();
			this.LandscapeSubstateRightButton06 = new global::System.Windows.Forms.RadioButton();
			this.LandscapeSubstateRightButton05 = new global::System.Windows.Forms.RadioButton();
			this.LandscapeSubstateRightButton04 = new global::System.Windows.Forms.RadioButton();
			this.LandscapeSubstateRightButton03 = new global::System.Windows.Forms.RadioButton();
			this.LandscapeSubstateRightButton02 = new global::System.Windows.Forms.RadioButton();
			this.LandscapeSubstateRightButton01 = new global::System.Windows.Forms.RadioButton();
			this.LandscapeSubstateLeftPanel = new global::System.Windows.Forms.Panel();
			this.LandscapeSubstateLeftButton09 = new global::System.Windows.Forms.RadioButton();
			this.LandscapeSubstateLeftButton08 = new global::System.Windows.Forms.RadioButton();
			this.LandscapeSubstateLeftButton00 = new global::System.Windows.Forms.RadioButton();
			this.LandscapeSubstateLeftButton07 = new global::System.Windows.Forms.RadioButton();
			this.LandscapeSubstateLeftButton06 = new global::System.Windows.Forms.RadioButton();
			this.LandscapeSubstateLeftButton05 = new global::System.Windows.Forms.RadioButton();
			this.LandscapeSubstateLeftButton04 = new global::System.Windows.Forms.RadioButton();
			this.LandscapeSubstateLeftButton03 = new global::System.Windows.Forms.RadioButton();
			this.LandscapeSubstateLeftButton02 = new global::System.Windows.Forms.RadioButton();
			this.LandscapeSubstateLeftButton01 = new global::System.Windows.Forms.RadioButton();
			this.ZonesTab = new global::System.Windows.Forms.TabPage();
			this.zoneFilterEditorButton = new global::System.Windows.Forms.Button();
			this.ZonesFilterLabel = new global::System.Windows.Forms.Label();
			this.ZoneBrushSizeLabel = new global::System.Windows.Forms.Label();
			this.ZoneBrushSizePanel = new global::System.Windows.Forms.Panel();
			this.ZoneSize3Button = new global::System.Windows.Forms.RadioButton();
			this.ZoneSizeImageList = new global::System.Windows.Forms.ImageList(this.components);
			this.ZoneSize1Button = new global::System.Windows.Forms.RadioButton();
			this.ZoneSize0Button = new global::System.Windows.Forms.RadioButton();
			this.ZoneSize2Button = new global::System.Windows.Forms.RadioButton();
			this.AddClearZonePanel = new global::System.Windows.Forms.Panel();
			this.AddToZoneRadioButton = new global::System.Windows.Forms.RadioButton();
			this.ClearRadioButton = new global::System.Windows.Forms.RadioButton();
			this.ZoneBrushSizeTextBox = new global::System.Windows.Forms.TextBox();
			this.ZonesListView = new global::System.Windows.Forms.ListView();
			this.ZonesContextMenuStrip = new global::System.Windows.Forms.ContextMenuStrip(this.components);
			this.AddZoneToolStripMenuItem = new global::System.Windows.Forms.ToolStripMenuItem();
			this.RemoveZoneToolStripMenuItem = new global::System.Windows.Forms.ToolStripMenuItem();
			this.LightsTab = new global::System.Windows.Forms.TabPage();
			this.LightFilterEditorButton = new global::System.Windows.Forms.Button();
			this.LightComboBox = new global::System.Windows.Forms.ComboBox();
			this.label7 = new global::System.Windows.Forms.Label();
			this.label8 = new global::System.Windows.Forms.Label();
			this.panel4 = new global::System.Windows.Forms.Panel();
			this.LightSize3Button = new global::System.Windows.Forms.RadioButton();
			this.LightSize1Button = new global::System.Windows.Forms.RadioButton();
			this.LightSize0Button = new global::System.Windows.Forms.RadioButton();
			this.LightSize2Button = new global::System.Windows.Forms.RadioButton();
			this.panel5 = new global::System.Windows.Forms.Panel();
			this.SetLightRadioButton = new global::System.Windows.Forms.RadioButton();
			this.radioButton17 = new global::System.Windows.Forms.RadioButton();
			this.LightBrushSizeTextBox = new global::System.Windows.Forms.TextBox();
			this.LightListView = new global::System.Windows.Forms.ListView();
			this.LightContextMenuStrip = new global::System.Windows.Forms.ContextMenuStrip(this.components);
			this.AddLightToolStripMenuItem = new global::System.Windows.Forms.ToolStripMenuItem();
			this.RemoveLightToolStripMenuItem = new global::System.Windows.Forms.ToolStripMenuItem();
			this.SoundTab = new global::System.Windows.Forms.TabPage();
			this.SoundSeparator = new global::System.Windows.Forms.Label();
			this.panel8 = new global::System.Windows.Forms.Panel();
			this.MusicRadioButton = new global::System.Windows.Forms.RadioButton();
			this.AmbienceRadioButton = new global::System.Windows.Forms.RadioButton();
			this.SoundFilterEditorButton = new global::System.Windows.Forms.Button();
			this.SoundComboBox = new global::System.Windows.Forms.ComboBox();
			this.label9 = new global::System.Windows.Forms.Label();
			this.label10 = new global::System.Windows.Forms.Label();
			this.panel6 = new global::System.Windows.Forms.Panel();
			this.SoundSize1Button = new global::System.Windows.Forms.RadioButton();
			this.SoundSize0Button = new global::System.Windows.Forms.RadioButton();
			this.SoundSize2Button = new global::System.Windows.Forms.RadioButton();
			this.panel7 = new global::System.Windows.Forms.Panel();
			this.SetSoundRadioButton = new global::System.Windows.Forms.RadioButton();
			this.ClearSoundRadioButton = new global::System.Windows.Forms.RadioButton();
			this.SoundBrushSizeTextBox = new global::System.Windows.Forms.TextBox();
			this.SoundListView = new global::System.Windows.Forms.ListView();
			this.SoundContextMenuStrip = new global::System.Windows.Forms.ContextMenuStrip(this.components);
			this.AddSoundToolStripMenuItem = new global::System.Windows.Forms.ToolStripMenuItem();
			this.RemoveSoundToolStripMenuItem = new global::System.Windows.Forms.ToolStripMenuItem();
			this.checkBox4 = new global::System.Windows.Forms.CheckBox();
			this.checkBox5 = new global::System.Windows.Forms.CheckBox();
			this.checkBox6 = new global::System.Windows.Forms.CheckBox();
			this.comboBox1 = new global::System.Windows.Forms.ComboBox();
			this.panel1 = new global::System.Windows.Forms.Panel();
			this.radioButton1 = new global::System.Windows.Forms.RadioButton();
			this.radioButton2 = new global::System.Windows.Forms.RadioButton();
			this.panel2 = new global::System.Windows.Forms.Panel();
			this.radioButton3 = new global::System.Windows.Forms.RadioButton();
			this.radioButton4 = new global::System.Windows.Forms.RadioButton();
			this.radioButton5 = new global::System.Windows.Forms.RadioButton();
			this.radioButton6 = new global::System.Windows.Forms.RadioButton();
			this.radioButton7 = new global::System.Windows.Forms.RadioButton();
			this.panel3 = new global::System.Windows.Forms.Panel();
			this.radioButton8 = new global::System.Windows.Forms.RadioButton();
			this.radioButton9 = new global::System.Windows.Forms.RadioButton();
			this.radioButton10 = new global::System.Windows.Forms.RadioButton();
			this.radioButton11 = new global::System.Windows.Forms.RadioButton();
			this.radioButton12 = new global::System.Windows.Forms.RadioButton();
			this.label1 = new global::System.Windows.Forms.Label();
			this.textBox1 = new global::System.Windows.Forms.TextBox();
			this.textBox2 = new global::System.Windows.Forms.TextBox();
			this.checkBox7 = new global::System.Windows.Forms.CheckBox();
			this.label2 = new global::System.Windows.Forms.Label();
			this.trackBar1 = new global::System.Windows.Forms.TrackBar();
			this.listView1 = new global::System.Windows.Forms.ListView();
			this.label3 = new global::System.Windows.Forms.Label();
			this.label4 = new global::System.Windows.Forms.Label();
			this.label5 = new global::System.Windows.Forms.Label();
			this.label6 = new global::System.Windows.Forms.Label();
			this.MainToolTip = new global::System.Windows.Forms.ToolTip(this.components);
			this.LeftLandscapeFormTimer = new global::System.Windows.Forms.Timer(this.components);
			this.RightLandscapeFormTimer = new global::System.Windows.Forms.Timer(this.components);
			this.RandomizerParamsTimer = new global::System.Windows.Forms.Timer(this.components);
			this.EditBoxTimer = new global::System.Windows.Forms.Timer(this.components);
			this.MainTab.SuspendLayout();
			this.ObjectsTab.SuspendLayout();
			this.LandscapeTab.SuspendLayout();
			this.LandscapeRegionSidePanel.SuspendLayout();
			this.LandscapeTerrainNumberPanel.SuspendLayout();
			((global::System.ComponentModel.ISupportInitialize)this.LandscapeRegionSizeTrackBar).BeginInit();
			this.LandscapeRegionShapePanel.SuspendLayout();
			this.LandscapeTabControl.SuspendLayout();
			this.LandscapeTabPageTile.SuspendLayout();
			((global::System.ComponentModel.ISupportInitialize)this.LTPTileReplaceToolImage).BeginInit();
			this.LTPTileSplitContainer.Panel1.SuspendLayout();
			this.LTPTileSplitContainer.Panel2.SuspendLayout();
			this.LTPTileSplitContainer.SuspendLayout();
			((global::System.ComponentModel.ISupportInitialize)this.LTPTileAnglePictureBox).BeginInit();
			((global::System.ComponentModel.ISupportInitialize)this.LTPTileAngle00TrackBar).BeginInit();
			((global::System.ComponentModel.ISupportInitialize)this.LTPTileAngle01TrackBar).BeginInit();
			((global::System.ComponentModel.ISupportInitialize)this.LTPTileStrengthSmoothPictureBox).BeginInit();
			((global::System.ComponentModel.ISupportInitialize)this.LTPTileStrengthTrackBar).BeginInit();
			((global::System.ComponentModel.ISupportInitialize)this.LTPTileSmoothTrackBar).BeginInit();
			this.LandscapeTabPageWater.SuspendLayout();
			this.LTPWaterWaterToolPanel.SuspendLayout();
			this.LandscapeTabPageHeight.SuspendLayout();
			this.LTPHeightSplitContainer.Panel1.SuspendLayout();
			this.LTPHeightSplitContainer.Panel2.SuspendLayout();
			this.LTPHeightSplitContainer.SuspendLayout();
			this.LTPHeightHeightToolPanel.SuspendLayout();
			((global::System.ComponentModel.ISupportInitialize)this.LTPHeightStrengthSmoothPictureBox).BeginInit();
			((global::System.ComponentModel.ISupportInitialize)this.LTPHeightStrengthTrackBar).BeginInit();
			((global::System.ComponentModel.ISupportInitialize)this.LTPHeightSmoothTrackBar).BeginInit();
			this.LandscapeTabPageClipboard.SuspendLayout();
			this.LTPClipboardSplitContainer.Panel1.SuspendLayout();
			this.LTPClipboardSplitContainer.Panel2.SuspendLayout();
			this.LTPClipboardSplitContainer.SuspendLayout();
			this.LTPClipboardCopyHeightTypePanel.SuspendLayout();
			((global::System.ComponentModel.ISupportInitialize)this.LTPClipboardStrengthSmoothPictureBox).BeginInit();
			((global::System.ComponentModel.ISupportInitialize)this.LTPClipboardStrengthTrackBar).BeginInit();
			((global::System.ComponentModel.ISupportInitialize)this.LTPClipboardSmoothTrackBar).BeginInit();
			this.LandscapeTabPageTilePicker.SuspendLayout();
			this.LTPTilePickerPanel.SuspendLayout();
			((global::System.ComponentModel.ISupportInitialize)this.LTPTilePickerPictureBox03).BeginInit();
			((global::System.ComponentModel.ISupportInitialize)this.LTPTilePickerPictureBox02).BeginInit();
			((global::System.ComponentModel.ISupportInitialize)this.LTPTilePickerPictureBox01).BeginInit();
			((global::System.ComponentModel.ISupportInitialize)this.LTPTilePickerPictureBox00).BeginInit();
			this.LandscapeTabPageHill.SuspendLayout();
			this.LTPHillParamTabControl.SuspendLayout();
			this.HillTabPage0.SuspendLayout();
			this.HillTabPage1.SuspendLayout();
			this.HillTabPage2.SuspendLayout();
			this.LandscapeTabPageRoad.SuspendLayout();
			this.LTPRoadTabControl.SuspendLayout();
			this.RoadTabPage0.SuspendLayout();
			this.RoadTabPage1.SuspendLayout();
			this.LandscapeSubstateRightPanel.SuspendLayout();
			this.LandscapeSubstateLeftPanel.SuspendLayout();
			this.ZonesTab.SuspendLayout();
			this.ZoneBrushSizePanel.SuspendLayout();
			this.AddClearZonePanel.SuspendLayout();
			this.ZonesContextMenuStrip.SuspendLayout();
			this.LightsTab.SuspendLayout();
			this.panel4.SuspendLayout();
			this.panel5.SuspendLayout();
			this.LightContextMenuStrip.SuspendLayout();
			this.SoundTab.SuspendLayout();
			this.panel8.SuspendLayout();
			this.panel6.SuspendLayout();
			this.panel7.SuspendLayout();
			this.SoundContextMenuStrip.SuspendLayout();
			this.panel1.SuspendLayout();
			this.panel2.SuspendLayout();
			this.panel3.SuspendLayout();
			((global::System.ComponentModel.ISupportInitialize)this.trackBar1).BeginInit();
			base.SuspendLayout();
			this.LandscapeHeightToolImageList.ImageStream = (global::System.Windows.Forms.ImageListStreamer)resources.GetObject("LandscapeHeightToolImageList.ImageStream");
			this.LandscapeHeightToolImageList.TransparentColor = global::System.Drawing.Color.Magenta;
			this.LandscapeHeightToolImageList.Images.SetKeyName(0, "height_up.bmp");
			this.LandscapeHeightToolImageList.Images.SetKeyName(1, "height_down.bmp");
			this.LandscapeHeightToolImageList.Images.SetKeyName(2, "height_plato.bmp");
			this.LandscapeHeightToolImageList.Images.SetKeyName(3, "height_plane.bmp");
			this.LandscapeHeightToolImageList.Images.SetKeyName(4, "height_level_to_plato.bmp");
			this.LandscapeHeightToolImageList.Images.SetKeyName(5, "height_level_to_plane.bmp");
			this.LandscapeCopyTypeImageList.ImageStream = (global::System.Windows.Forms.ImageListStreamer)resources.GetObject("LandscapeCopyTypeImageList.ImageStream");
			this.LandscapeCopyTypeImageList.TransparentColor = global::System.Drawing.Color.Magenta;
			this.LandscapeCopyTypeImageList.Images.SetKeyName(0, "copy_tiles.bmp");
			this.LandscapeCopyTypeImageList.Images.SetKeyName(1, "copy_heights.bmp");
			this.LandscapeCopyTypeImageList.Images.SetKeyName(2, "copy_objects.bmp");
			this.LandscapeCopyHeightTypeImageList.ImageStream = (global::System.Windows.Forms.ImageListStreamer)resources.GetObject("LandscapeCopyHeightTypeImageList.ImageStream");
			this.LandscapeCopyHeightTypeImageList.TransparentColor = global::System.Drawing.Color.Magenta;
			this.LandscapeCopyHeightTypeImageList.Images.SetKeyName(0, "copy_heights_adaptive.bmp");
			this.LandscapeCopyHeightTypeImageList.Images.SetKeyName(1, "copy_heights_min.bmp");
			this.LandscapeCopyHeightTypeImageList.Images.SetKeyName(2, "copy_heights_max.bmp");
			this.LandscapeCopyHeightTypeImageList.Images.SetKeyName(3, "copy_heights_precise.bmp");
			resources.ApplyResources(this.ZoneComboBox, "ZoneComboBox");
			this.ZoneComboBox.DropDownStyle = global::System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.ZoneComboBox.FormattingEnabled = true;
			this.ZoneComboBox.Name = "ZoneComboBox";
			this.MainTabImageList.ImageStream = (global::System.Windows.Forms.ImageListStreamer)resources.GetObject("MainTabImageList.ImageStream");
			this.MainTabImageList.TransparentColor = global::System.Drawing.Color.Magenta;
			this.MainTabImageList.Images.SetKeyName(0, "");
			this.MainTabImageList.Images.SetKeyName(1, "");
			this.MainTabImageList.Images.SetKeyName(2, "");
			this.MainTabImageList.Images.SetKeyName(3, "");
			this.MainTab.Controls.Add(this.ObjectsTab);
			this.MainTab.Controls.Add(this.LandscapeTab);
			this.MainTab.Controls.Add(this.ZonesTab);
			this.MainTab.Controls.Add(this.LightsTab);
			this.MainTab.Controls.Add(this.SoundTab);
			resources.ApplyResources(this.MainTab, "MainTab");
			this.MainTab.ImageList = this.MainTabImageList;
			this.MainTab.Name = "MainTab";
			this.MainTab.SelectedIndex = 0;
			this.MainTab.SelectedIndexChanged += new global::System.EventHandler(this.MainTab_SelectedIndexChanged);
			this.ObjectsTab.Controls.Add(this.UnderTerrainCheckBox);
			this.ObjectsTab.Controls.Add(this.UnderTerrainTextBox);
			this.ObjectsTab.Controls.Add(this.OverTerrainTextBox);
			this.ObjectsTab.Controls.Add(this.OverTerrainCheckBox);
			this.ObjectsTab.Controls.Add(this.ObjectParamsLabel);
			this.ObjectsTab.Controls.Add(this.RandomizeLabel);
			this.ObjectsTab.Controls.Add(this.FlatRowCheckBox);
			this.ObjectsTab.Controls.Add(this.ObjectRowCheckBox);
			this.ObjectsTab.Controls.Add(this.GroupComboBox);
			this.ObjectsTab.Controls.Add(this.GroupCheckBox);
			this.ObjectsTab.Controls.Add(this.RandomObjectCheckBox);
			this.ObjectsTab.Controls.Add(this.ObjectTypeComboBox);
			this.ObjectsTab.Controls.Add(this.FilterObjectTypeCheckBox);
			this.ObjectsTab.Controls.Add(this.SelectionFilterLabel);
			this.ObjectsTab.Controls.Add(this.FilterSelectionCheckBox);
			this.ObjectsTab.Controls.Add(this.RandomScaleLabel);
			this.ObjectsTab.Controls.Add(this.RandomPitchLabel);
			this.ObjectsTab.Controls.Add(this.RandomRotationLabel);
			this.ObjectsTab.Controls.Add(this.RandomRotationToEditBox);
			this.ObjectsTab.Controls.Add(this.RandomRotationFromEditBox);
			this.ObjectsTab.Controls.Add(this.RandomScaleToEditBox);
			this.ObjectsTab.Controls.Add(this.RandomScaleFromEditBox);
			this.ObjectsTab.Controls.Add(this.RandomPitchToEditBox);
			this.ObjectsTab.Controls.Add(this.RandomPitchFromEditBox);
			this.ObjectsTab.Controls.Add(this.RandomScaleCheckBox);
			this.ObjectsTab.Controls.Add(this.RandomPitchCheckBox);
			this.ObjectsTab.Controls.Add(this.RandomRotationCheckBox);
			this.ObjectsTab.Controls.Add(this.objectsFilterEditorButton);
			this.ObjectsTab.Controls.Add(this.ObjectListView);
			this.ObjectsTab.Controls.Add(this.ObjectComboBox);
			this.ObjectsTab.Controls.Add(this.ObjectFilterLabel);
			resources.ApplyResources(this.ObjectsTab, "ObjectsTab");
			this.ObjectsTab.Name = "ObjectsTab";
			this.ObjectsTab.UseVisualStyleBackColor = true;
			resources.ApplyResources(this.UnderTerrainCheckBox, "UnderTerrainCheckBox");
			this.UnderTerrainCheckBox.Name = "UnderTerrainCheckBox";
			this.UnderTerrainCheckBox.UseVisualStyleBackColor = true;
			this.UnderTerrainCheckBox.CheckedChanged += new global::System.EventHandler(this.UnderTerrainCheckBox_CheckedChanged);
			resources.ApplyResources(this.UnderTerrainTextBox, "UnderTerrainTextBox");
			this.UnderTerrainTextBox.Name = "UnderTerrainTextBox";
			this.UnderTerrainTextBox.TextChanged += new global::System.EventHandler(this.UnderTerrainTextBox_TextChanged);
			resources.ApplyResources(this.OverTerrainTextBox, "OverTerrainTextBox");
			this.OverTerrainTextBox.Name = "OverTerrainTextBox";
			this.OverTerrainTextBox.TextChanged += new global::System.EventHandler(this.OverTerrainTextBox_TextChanged);
			resources.ApplyResources(this.OverTerrainCheckBox, "OverTerrainCheckBox");
			this.OverTerrainCheckBox.Name = "OverTerrainCheckBox";
			this.OverTerrainCheckBox.UseVisualStyleBackColor = true;
			this.OverTerrainCheckBox.CheckedChanged += new global::System.EventHandler(this.OverTerrainCheckBox_CheckedChanged);
			resources.ApplyResources(this.ObjectParamsLabel, "ObjectParamsLabel");
			this.ObjectParamsLabel.Name = "ObjectParamsLabel";
			resources.ApplyResources(this.RandomizeLabel, "RandomizeLabel");
			this.RandomizeLabel.Name = "RandomizeLabel";
			resources.ApplyResources(this.FlatRowCheckBox, "FlatRowCheckBox");
			this.FlatRowCheckBox.Name = "FlatRowCheckBox";
			this.FlatRowCheckBox.UseVisualStyleBackColor = true;
			this.FlatRowCheckBox.CheckedChanged += new global::System.EventHandler(this.FlatRowCheckBox_CheckedChanged);
			resources.ApplyResources(this.ObjectRowCheckBox, "ObjectRowCheckBox");
			this.ObjectRowCheckBox.Name = "ObjectRowCheckBox";
			this.ObjectRowCheckBox.UseVisualStyleBackColor = true;
			this.ObjectRowCheckBox.CheckedChanged += new global::System.EventHandler(this.ObjectRowCheckBox_CheckedChanged);
			resources.ApplyResources(this.GroupComboBox, "GroupComboBox");
			this.GroupComboBox.FormattingEnabled = true;
			this.GroupComboBox.Name = "GroupComboBox";
			this.GroupComboBox.Sorted = true;
			this.GroupComboBox.Leave += new global::System.EventHandler(this.GroupComboBox_Leave);
			resources.ApplyResources(this.GroupCheckBox, "GroupCheckBox");
			this.GroupCheckBox.Name = "GroupCheckBox";
			this.GroupCheckBox.UseVisualStyleBackColor = true;
			resources.ApplyResources(this.RandomObjectCheckBox, "RandomObjectCheckBox");
			this.RandomObjectCheckBox.Name = "RandomObjectCheckBox";
			this.RandomObjectCheckBox.UseVisualStyleBackColor = true;
			resources.ApplyResources(this.ObjectTypeComboBox, "ObjectTypeComboBox");
			this.ObjectTypeComboBox.DropDownStyle = global::System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.ObjectTypeComboBox.FormattingEnabled = true;
			this.ObjectTypeComboBox.Name = "ObjectTypeComboBox";
			this.ObjectTypeComboBox.Sorted = true;
			resources.ApplyResources(this.FilterObjectTypeCheckBox, "FilterObjectTypeCheckBox");
			this.FilterObjectTypeCheckBox.Name = "FilterObjectTypeCheckBox";
			this.FilterObjectTypeCheckBox.UseVisualStyleBackColor = true;
			this.FilterObjectTypeCheckBox.CheckedChanged += new global::System.EventHandler(this.FilterObjectTypeCheckBox_CheckedChanged);
			resources.ApplyResources(this.SelectionFilterLabel, "SelectionFilterLabel");
			this.SelectionFilterLabel.Name = "SelectionFilterLabel";
			resources.ApplyResources(this.FilterSelectionCheckBox, "FilterSelectionCheckBox");
			this.FilterSelectionCheckBox.Name = "FilterSelectionCheckBox";
			this.FilterSelectionCheckBox.UseVisualStyleBackColor = true;
			this.FilterSelectionCheckBox.CheckedChanged += new global::System.EventHandler(this.FilterSelectionCheckBox_CheckedChanged);
			resources.ApplyResources(this.RandomScaleLabel, "RandomScaleLabel");
			this.RandomScaleLabel.Name = "RandomScaleLabel";
			resources.ApplyResources(this.RandomPitchLabel, "RandomPitchLabel");
			this.RandomPitchLabel.Name = "RandomPitchLabel";
			resources.ApplyResources(this.RandomRotationLabel, "RandomRotationLabel");
			this.RandomRotationLabel.Name = "RandomRotationLabel";
			resources.ApplyResources(this.RandomRotationToEditBox, "RandomRotationToEditBox");
			this.RandomRotationToEditBox.Name = "RandomRotationToEditBox";
			this.RandomRotationToEditBox.TextChanged += new global::System.EventHandler(this.RandomRotationToEditBox_TextChanged);
			resources.ApplyResources(this.RandomRotationFromEditBox, "RandomRotationFromEditBox");
			this.RandomRotationFromEditBox.Name = "RandomRotationFromEditBox";
			this.RandomRotationFromEditBox.TextChanged += new global::System.EventHandler(this.RandomRotationFromEditBox_TextChanged);
			resources.ApplyResources(this.RandomScaleToEditBox, "RandomScaleToEditBox");
			this.RandomScaleToEditBox.Name = "RandomScaleToEditBox";
			this.RandomScaleToEditBox.TextChanged += new global::System.EventHandler(this.RandomScaleToEditBox_TextChanged);
			resources.ApplyResources(this.RandomScaleFromEditBox, "RandomScaleFromEditBox");
			this.RandomScaleFromEditBox.Name = "RandomScaleFromEditBox";
			this.RandomScaleFromEditBox.TextChanged += new global::System.EventHandler(this.RandomScaleFromEditBox_TextChanged);
			resources.ApplyResources(this.RandomPitchToEditBox, "RandomPitchToEditBox");
			this.RandomPitchToEditBox.Name = "RandomPitchToEditBox";
			this.RandomPitchToEditBox.TextChanged += new global::System.EventHandler(this.RandomPitchToEditBox_TextChanged);
			resources.ApplyResources(this.RandomPitchFromEditBox, "RandomPitchFromEditBox");
			this.RandomPitchFromEditBox.Name = "RandomPitchFromEditBox";
			this.RandomPitchFromEditBox.TextChanged += new global::System.EventHandler(this.RandomPitchFromEditBox_TextChanged);
			resources.ApplyResources(this.RandomScaleCheckBox, "RandomScaleCheckBox");
			this.RandomScaleCheckBox.Name = "RandomScaleCheckBox";
			this.RandomScaleCheckBox.UseVisualStyleBackColor = true;
			this.RandomScaleCheckBox.CheckedChanged += new global::System.EventHandler(this.RandomScaleCheckBox_CheckedChanged);
			resources.ApplyResources(this.RandomPitchCheckBox, "RandomPitchCheckBox");
			this.RandomPitchCheckBox.Name = "RandomPitchCheckBox";
			this.RandomPitchCheckBox.UseVisualStyleBackColor = true;
			this.RandomPitchCheckBox.CheckedChanged += new global::System.EventHandler(this.RandomPitchCheckBox_CheckedChanged);
			resources.ApplyResources(this.RandomRotationCheckBox, "RandomRotationCheckBox");
			this.RandomRotationCheckBox.Name = "RandomRotationCheckBox";
			this.RandomRotationCheckBox.UseVisualStyleBackColor = true;
			this.RandomRotationCheckBox.CheckedChanged += new global::System.EventHandler(this.RandomRotationCheckBox_CheckedChanged);
			resources.ApplyResources(this.objectsFilterEditorButton, "objectsFilterEditorButton");
			this.objectsFilterEditorButton.Name = "objectsFilterEditorButton";
			this.objectsFilterEditorButton.UseVisualStyleBackColor = true;
			this.objectsFilterEditorButton.Click += new global::System.EventHandler(this.objectsFilterEditorButton_Click);
			resources.ApplyResources(this.ObjectListView, "ObjectListView");
			this.ObjectListView.HideSelection = false;
			this.ObjectListView.Name = "ObjectListView";
			this.ObjectListView.ShowItemToolTips = true;
			this.ObjectListView.Sorting = global::System.Windows.Forms.SortOrder.Ascending;
			this.ObjectListView.UseCompatibleStateImageBehavior = false;
			this.ObjectListView.View = global::System.Windows.Forms.View.List;
			resources.ApplyResources(this.ObjectComboBox, "ObjectComboBox");
			this.ObjectComboBox.DropDownStyle = global::System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.ObjectComboBox.FormattingEnabled = true;
			this.ObjectComboBox.Name = "ObjectComboBox";
			resources.ApplyResources(this.ObjectFilterLabel, "ObjectFilterLabel");
			this.ObjectFilterLabel.Name = "ObjectFilterLabel";
			this.LandscapeTab.Controls.Add(this.LandscapeRegionSideLabel);
			this.LandscapeTab.Controls.Add(this.LandscapeRegionSidePanel);
			this.LandscapeTab.Controls.Add(this.LandscapeUseSeparateRegionsCheckBox);
			this.LandscapeTab.Controls.Add(this.LandscapeRegionSizeTrackBarLabel);
			this.LandscapeTab.Controls.Add(this.LandscapeToolLabel);
			this.LandscapeTab.Controls.Add(this.LandscapeTerrainNumberLabel);
			this.LandscapeTab.Controls.Add(this.LandscapeTerrainNumberPanel);
			this.LandscapeTab.Controls.Add(this.LandscapeInvertHeightToolsCheckBox);
			this.LandscapeTab.Controls.Add(this.LandscapeRegionSizeTrackBar);
			this.LandscapeTab.Controls.Add(this.LandscapeRegionShapeLabel);
			this.LandscapeTab.Controls.Add(this.LandscapeRegionShapePanel);
			this.LandscapeTab.Controls.Add(this.LandscapeRegionSizeEditBox);
			this.LandscapeTab.Controls.Add(this.LandscapeRegionSizeLabel);
			this.LandscapeTab.Controls.Add(this.LandscapeTabControl);
			this.LandscapeTab.Controls.Add(this.LandscapeSubstateRightPanel);
			this.LandscapeTab.Controls.Add(this.LandscapeSubstateLeftPanel);
			resources.ApplyResources(this.LandscapeTab, "LandscapeTab");
			this.LandscapeTab.Name = "LandscapeTab";
			this.LandscapeTab.UseVisualStyleBackColor = true;
			resources.ApplyResources(this.LandscapeRegionSideLabel, "LandscapeRegionSideLabel");
			this.LandscapeRegionSideLabel.Name = "LandscapeRegionSideLabel";
			this.LandscapeRegionSidePanel.Controls.Add(this.LandscapeRegionSideButton02);
			this.LandscapeRegionSidePanel.Controls.Add(this.LandscapeRegionSideButton01);
			this.LandscapeRegionSidePanel.Controls.Add(this.LandscapeRegionSideButton00);
			resources.ApplyResources(this.LandscapeRegionSidePanel, "LandscapeRegionSidePanel");
			this.LandscapeRegionSidePanel.Name = "LandscapeRegionSidePanel";
			resources.ApplyResources(this.LandscapeRegionSideButton02, "LandscapeRegionSideButton02");
			this.LandscapeRegionSideButton02.ImageList = this.LandscapeRegionSideImageList;
			this.LandscapeRegionSideButton02.Name = "LandscapeRegionSideButton02";
			this.MainToolTip.SetToolTip(this.LandscapeRegionSideButton02, resources.GetString("LandscapeRegionSideButton02.ToolTip"));
			this.LandscapeRegionSideButton02.UseMnemonic = false;
			this.LandscapeRegionSideButton02.UseVisualStyleBackColor = true;
			this.LandscapeRegionSideButton02.CheckedChanged += new global::System.EventHandler(this.LandscapeRegionSideButton02_CheckedChanged);
			this.LandscapeRegionSideImageList.ImageStream = (global::System.Windows.Forms.ImageListStreamer)resources.GetObject("LandscapeRegionSideImageList.ImageStream");
			this.LandscapeRegionSideImageList.TransparentColor = global::System.Drawing.Color.Magenta;
			this.LandscapeRegionSideImageList.Images.SetKeyName(0, "left_side.bmp");
			this.LandscapeRegionSideImageList.Images.SetKeyName(1, "right_side.bmp");
			this.LandscapeRegionSideImageList.Images.SetKeyName(2, "both_sides.bmp");
			resources.ApplyResources(this.LandscapeRegionSideButton01, "LandscapeRegionSideButton01");
			this.LandscapeRegionSideButton01.ImageList = this.LandscapeRegionSideImageList;
			this.LandscapeRegionSideButton01.Name = "LandscapeRegionSideButton01";
			this.MainToolTip.SetToolTip(this.LandscapeRegionSideButton01, resources.GetString("LandscapeRegionSideButton01.ToolTip"));
			this.LandscapeRegionSideButton01.UseMnemonic = false;
			this.LandscapeRegionSideButton01.UseVisualStyleBackColor = true;
			this.LandscapeRegionSideButton01.CheckedChanged += new global::System.EventHandler(this.LandscapeRegionSideButton01_CheckedChanged);
			resources.ApplyResources(this.LandscapeRegionSideButton00, "LandscapeRegionSideButton00");
			this.LandscapeRegionSideButton00.ImageList = this.LandscapeRegionSideImageList;
			this.LandscapeRegionSideButton00.Name = "LandscapeRegionSideButton00";
			this.MainToolTip.SetToolTip(this.LandscapeRegionSideButton00, resources.GetString("LandscapeRegionSideButton00.ToolTip"));
			this.LandscapeRegionSideButton00.UseMnemonic = false;
			this.LandscapeRegionSideButton00.UseVisualStyleBackColor = true;
			this.LandscapeRegionSideButton00.CheckedChanged += new global::System.EventHandler(this.LandscapeRegionSideButton00_CheckedChanged);
			resources.ApplyResources(this.LandscapeUseSeparateRegionsCheckBox, "LandscapeUseSeparateRegionsCheckBox");
			this.LandscapeUseSeparateRegionsCheckBox.Name = "LandscapeUseSeparateRegionsCheckBox";
			this.LandscapeUseSeparateRegionsCheckBox.UseVisualStyleBackColor = true;
			resources.ApplyResources(this.LandscapeRegionSizeTrackBarLabel, "LandscapeRegionSizeTrackBarLabel");
			this.LandscapeRegionSizeTrackBarLabel.Name = "LandscapeRegionSizeTrackBarLabel";
			resources.ApplyResources(this.LandscapeToolLabel, "LandscapeToolLabel");
			this.LandscapeToolLabel.Name = "LandscapeToolLabel";
			resources.ApplyResources(this.LandscapeTerrainNumberLabel, "LandscapeTerrainNumberLabel");
			this.LandscapeTerrainNumberLabel.Name = "LandscapeTerrainNumberLabel";
			this.LandscapeTerrainNumberPanel.Controls.Add(this.LandscapeTerrainNumberButton01);
			this.LandscapeTerrainNumberPanel.Controls.Add(this.LandscapeTerrainNumberButton00);
			resources.ApplyResources(this.LandscapeTerrainNumberPanel, "LandscapeTerrainNumberPanel");
			this.LandscapeTerrainNumberPanel.Name = "LandscapeTerrainNumberPanel";
			resources.ApplyResources(this.LandscapeTerrainNumberButton01, "LandscapeTerrainNumberButton01");
			this.LandscapeTerrainNumberButton01.ImageList = this.LandscapeTerrainNumberImageList;
			this.LandscapeTerrainNumberButton01.Name = "LandscapeTerrainNumberButton01";
			this.MainToolTip.SetToolTip(this.LandscapeTerrainNumberButton01, resources.GetString("LandscapeTerrainNumberButton01.ToolTip"));
			this.LandscapeTerrainNumberButton01.UseMnemonic = false;
			this.LandscapeTerrainNumberButton01.UseVisualStyleBackColor = true;
			this.LandscapeTerrainNumberButton01.CheckedChanged += new global::System.EventHandler(this.LandscapeTerrainNumberButton01_CheckedChanged);
			this.LandscapeTerrainNumberImageList.ImageStream = (global::System.Windows.Forms.ImageListStreamer)resources.GetObject("LandscapeTerrainNumberImageList.ImageStream");
			this.LandscapeTerrainNumberImageList.TransparentColor = global::System.Drawing.Color.Magenta;
			this.LandscapeTerrainNumberImageList.Images.SetKeyName(0, "terrain_terrain.bmp");
			this.LandscapeTerrainNumberImageList.Images.SetKeyName(1, "terrain_bottom.bmp");
			resources.ApplyResources(this.LandscapeTerrainNumberButton00, "LandscapeTerrainNumberButton00");
			this.LandscapeTerrainNumberButton00.ImageList = this.LandscapeTerrainNumberImageList;
			this.LandscapeTerrainNumberButton00.Name = "LandscapeTerrainNumberButton00";
			this.MainToolTip.SetToolTip(this.LandscapeTerrainNumberButton00, resources.GetString("LandscapeTerrainNumberButton00.ToolTip"));
			this.LandscapeTerrainNumberButton00.UseMnemonic = false;
			this.LandscapeTerrainNumberButton00.UseVisualStyleBackColor = true;
			this.LandscapeTerrainNumberButton00.CheckedChanged += new global::System.EventHandler(this.LandscapeTerrainNumberButton00_CheckedChanged);
			resources.ApplyResources(this.LandscapeInvertHeightToolsCheckBox, "LandscapeInvertHeightToolsCheckBox");
			this.LandscapeInvertHeightToolsCheckBox.Name = "LandscapeInvertHeightToolsCheckBox";
			this.LandscapeInvertHeightToolsCheckBox.UseVisualStyleBackColor = true;
			this.LandscapeInvertHeightToolsCheckBox.CheckedChanged += new global::System.EventHandler(this.LandscapeInvertHeightToolsCheckBox_CheckedChanged);
			resources.ApplyResources(this.LandscapeRegionSizeTrackBar, "LandscapeRegionSizeTrackBar");
			this.LandscapeRegionSizeTrackBar.LargeChange = 8;
			this.LandscapeRegionSizeTrackBar.Maximum = 128;
			this.LandscapeRegionSizeTrackBar.Name = "LandscapeRegionSizeTrackBar";
			this.LandscapeRegionSizeTrackBar.SmallChange = 2;
			this.LandscapeRegionSizeTrackBar.TickFrequency = 32;
			this.MainToolTip.SetToolTip(this.LandscapeRegionSizeTrackBar, resources.GetString("LandscapeRegionSizeTrackBar.ToolTip"));
			this.LandscapeRegionSizeTrackBar.Scroll += new global::System.EventHandler(this.LandscapeRegionSizeTrackBar_Scroll);
			resources.ApplyResources(this.LandscapeRegionShapeLabel, "LandscapeRegionShapeLabel");
			this.LandscapeRegionShapeLabel.Name = "LandscapeRegionShapeLabel";
			this.LandscapeRegionShapePanel.Controls.Add(this.LandscapeRegionShapeButton04);
			this.LandscapeRegionShapePanel.Controls.Add(this.LandscapeRegionShapeButton03);
			this.LandscapeRegionShapePanel.Controls.Add(this.LandscapeRegionShapeButton02);
			this.LandscapeRegionShapePanel.Controls.Add(this.LandscapeRegionShapeButton01);
			this.LandscapeRegionShapePanel.Controls.Add(this.LandscapeRegionShapeButton00);
			resources.ApplyResources(this.LandscapeRegionShapePanel, "LandscapeRegionShapePanel");
			this.LandscapeRegionShapePanel.Name = "LandscapeRegionShapePanel";
			resources.ApplyResources(this.LandscapeRegionShapeButton04, "LandscapeRegionShapeButton04");
			this.LandscapeRegionShapeButton04.ImageList = this.LandscapeRegionShapeImageList;
			this.LandscapeRegionShapeButton04.Name = "LandscapeRegionShapeButton04";
			this.MainToolTip.SetToolTip(this.LandscapeRegionShapeButton04, resources.GetString("LandscapeRegionShapeButton04.ToolTip"));
			this.LandscapeRegionShapeButton04.UseMnemonic = false;
			this.LandscapeRegionShapeButton04.UseVisualStyleBackColor = true;
			this.LandscapeRegionShapeButton04.CheckedChanged += new global::System.EventHandler(this.LandscapeRegionShapeButton04_CheckedChanged);
			this.LandscapeRegionShapeImageList.ImageStream = (global::System.Windows.Forms.ImageListStreamer)resources.GetObject("LandscapeRegionShapeImageList.ImageStream");
			this.LandscapeRegionShapeImageList.TransparentColor = global::System.Drawing.Color.Magenta;
			this.LandscapeRegionShapeImageList.Images.SetKeyName(0, "ellipse.bmp");
			this.LandscapeRegionShapeImageList.Images.SetKeyName(1, "square.bmp");
			this.LandscapeRegionShapeImageList.Images.SetKeyName(2, "polygon.bmp");
			this.LandscapeRegionShapeImageList.Images.SetKeyName(3, "stripe.bmp");
			this.LandscapeRegionShapeImageList.Images.SetKeyName(4, "all_map.bmp");
			resources.ApplyResources(this.LandscapeRegionShapeButton03, "LandscapeRegionShapeButton03");
			this.LandscapeRegionShapeButton03.ImageList = this.LandscapeRegionShapeImageList;
			this.LandscapeRegionShapeButton03.Name = "LandscapeRegionShapeButton03";
			this.MainToolTip.SetToolTip(this.LandscapeRegionShapeButton03, resources.GetString("LandscapeRegionShapeButton03.ToolTip"));
			this.LandscapeRegionShapeButton03.UseMnemonic = false;
			this.LandscapeRegionShapeButton03.UseVisualStyleBackColor = true;
			this.LandscapeRegionShapeButton03.CheckedChanged += new global::System.EventHandler(this.LandscapeRegionShapeButton03_CheckedChanged);
			resources.ApplyResources(this.LandscapeRegionShapeButton02, "LandscapeRegionShapeButton02");
			this.LandscapeRegionShapeButton02.ImageList = this.LandscapeRegionShapeImageList;
			this.LandscapeRegionShapeButton02.Name = "LandscapeRegionShapeButton02";
			this.MainToolTip.SetToolTip(this.LandscapeRegionShapeButton02, resources.GetString("LandscapeRegionShapeButton02.ToolTip"));
			this.LandscapeRegionShapeButton02.UseMnemonic = false;
			this.LandscapeRegionShapeButton02.UseVisualStyleBackColor = true;
			this.LandscapeRegionShapeButton02.CheckedChanged += new global::System.EventHandler(this.LandscapeRegionShapeButton02_CheckedChanged);
			resources.ApplyResources(this.LandscapeRegionShapeButton01, "LandscapeRegionShapeButton01");
			this.LandscapeRegionShapeButton01.ImageList = this.LandscapeRegionShapeImageList;
			this.LandscapeRegionShapeButton01.Name = "LandscapeRegionShapeButton01";
			this.MainToolTip.SetToolTip(this.LandscapeRegionShapeButton01, resources.GetString("LandscapeRegionShapeButton01.ToolTip"));
			this.LandscapeRegionShapeButton01.UseMnemonic = false;
			this.LandscapeRegionShapeButton01.UseVisualStyleBackColor = true;
			this.LandscapeRegionShapeButton01.CheckedChanged += new global::System.EventHandler(this.LandscapeRegionShapeButton01_CheckedChanged);
			resources.ApplyResources(this.LandscapeRegionShapeButton00, "LandscapeRegionShapeButton00");
			this.LandscapeRegionShapeButton00.ImageList = this.LandscapeRegionShapeImageList;
			this.LandscapeRegionShapeButton00.Name = "LandscapeRegionShapeButton00";
			this.MainToolTip.SetToolTip(this.LandscapeRegionShapeButton00, resources.GetString("LandscapeRegionShapeButton00.ToolTip"));
			this.LandscapeRegionShapeButton00.UseMnemonic = false;
			this.LandscapeRegionShapeButton00.UseVisualStyleBackColor = true;
			this.LandscapeRegionShapeButton00.CheckedChanged += new global::System.EventHandler(this.LandscapeRegionShapeButton00_CheckedChanged);
			resources.ApplyResources(this.LandscapeRegionSizeEditBox, "LandscapeRegionSizeEditBox");
			this.LandscapeRegionSizeEditBox.Name = "LandscapeRegionSizeEditBox";
			this.MainToolTip.SetToolTip(this.LandscapeRegionSizeEditBox, resources.GetString("LandscapeRegionSizeEditBox.ToolTip"));
			this.LandscapeRegionSizeEditBox.TextChanged += new global::System.EventHandler(this.LandscapeRegionSizeEditBox_TextChanged);
			resources.ApplyResources(this.LandscapeRegionSizeLabel, "LandscapeRegionSizeLabel");
			this.LandscapeRegionSizeLabel.Name = "LandscapeRegionSizeLabel";
			resources.ApplyResources(this.LandscapeTabControl, "LandscapeTabControl");
			this.LandscapeTabControl.Controls.Add(this.LandscapeTabPageTile);
			this.LandscapeTabControl.Controls.Add(this.LandscapeTabPageGradient);
			this.LandscapeTabControl.Controls.Add(this.LandscapeTabPageWater);
			this.LandscapeTabControl.Controls.Add(this.LandscapeTabPageHeight);
			this.LandscapeTabControl.Controls.Add(this.LandscapeTabPageClipboard);
			this.LandscapeTabControl.Controls.Add(this.LandscapeTabPageTilePicker);
			this.LandscapeTabControl.Controls.Add(this.LandscapeTabPageHeightPicker);
			this.LandscapeTabControl.Controls.Add(this.LandscapeTabPageHill);
			this.LandscapeTabControl.Controls.Add(this.LandscapeTabPageRoad);
			this.LandscapeTabControl.Name = "LandscapeTabControl";
			this.LandscapeTabControl.SelectedIndex = 0;
			this.MainToolTip.SetToolTip(this.LandscapeTabControl, resources.GetString("LandscapeTabControl.ToolTip"));
			this.LandscapeTabControl.SelectedIndexChanged += new global::System.EventHandler(this.LandscapeTabControl_SelectedIndexChanged);
			this.LandscapeTabPageTile.Controls.Add(this.LTPTileReplaceToolImage);
			this.LandscapeTabPageTile.Controls.Add(this.LTPTileReplaceToolTextBox);
			this.LandscapeTabPageTile.Controls.Add(this.LTPTileReplaceToolCheckBox);
			this.LandscapeTabPageTile.Controls.Add(this.LTPTileAutomaticToolCheckBox);
			this.LandscapeTabPageTile.Controls.Add(this.LTPTileSpotToolCheckBox);
			this.LandscapeTabPageTile.Controls.Add(this.LTPTileSplitContainer);
			this.LandscapeTabPageTile.Controls.Add(this.LTPTileBrushButton);
			this.LandscapeTabPageTile.Controls.Add(this.LTPTileBrushComboBox);
			this.LandscapeTabPageTile.Controls.Add(this.LTPTileBrushListView);
			this.LandscapeTabPageTile.Controls.Add(this.LTPTileBrushLabel);
			resources.ApplyResources(this.LandscapeTabPageTile, "LandscapeTabPageTile");
			this.LandscapeTabPageTile.Name = "LandscapeTabPageTile";
			this.LandscapeTabPageTile.UseVisualStyleBackColor = true;
			resources.ApplyResources(this.LTPTileReplaceToolImage, "LTPTileReplaceToolImage");
			this.LTPTileReplaceToolImage.Name = "LTPTileReplaceToolImage";
			this.LTPTileReplaceToolImage.TabStop = false;
			resources.ApplyResources(this.LTPTileReplaceToolTextBox, "LTPTileReplaceToolTextBox");
			this.LTPTileReplaceToolTextBox.Name = "LTPTileReplaceToolTextBox";
			this.MainToolTip.SetToolTip(this.LTPTileReplaceToolTextBox, resources.GetString("LTPTileReplaceToolTextBox.ToolTip"));
			this.LTPTileReplaceToolTextBox.TextChanged += new global::System.EventHandler(this.LTPTileReplaceToolTextBox_TextChanged);
			resources.ApplyResources(this.LTPTileReplaceToolCheckBox, "LTPTileReplaceToolCheckBox");
			this.LTPTileReplaceToolCheckBox.Name = "LTPTileReplaceToolCheckBox";
			this.LTPTileReplaceToolCheckBox.UseVisualStyleBackColor = true;
			this.LTPTileReplaceToolCheckBox.CheckedChanged += new global::System.EventHandler(this.LTPTileReplaceToolCheckBox_CheckedChanged);
			resources.ApplyResources(this.LTPTileAutomaticToolCheckBox, "LTPTileAutomaticToolCheckBox");
			this.LTPTileAutomaticToolCheckBox.Name = "LTPTileAutomaticToolCheckBox";
			this.LTPTileAutomaticToolCheckBox.UseVisualStyleBackColor = true;
			this.LTPTileAutomaticToolCheckBox.CheckedChanged += new global::System.EventHandler(this.LTPTileAutomaticToolCheckBox_CheckedChanged);
			resources.ApplyResources(this.LTPTileSpotToolCheckBox, "LTPTileSpotToolCheckBox");
			this.LTPTileSpotToolCheckBox.Name = "LTPTileSpotToolCheckBox";
			this.LTPTileSpotToolCheckBox.UseVisualStyleBackColor = true;
			this.LTPTileSpotToolCheckBox.CheckedChanged += new global::System.EventHandler(this.LTPTileSpotToolCheckBox_CheckedChanged);
			resources.ApplyResources(this.LTPTileSplitContainer, "LTPTileSplitContainer");
			this.LTPTileSplitContainer.Name = "LTPTileSplitContainer";
			this.LTPTileSplitContainer.Panel1.Controls.Add(this.LTPTileAnglePictureBox);
			this.LTPTileSplitContainer.Panel1.Controls.Add(this.LTPTileAngle01Label);
			this.LTPTileSplitContainer.Panel1.Controls.Add(this.LTPTileUseAngleRestrictionsCheckBox);
			this.LTPTileSplitContainer.Panel1.Controls.Add(this.LTPTileAngle00TextBox);
			this.LTPTileSplitContainer.Panel1.Controls.Add(this.LTPTileAngle00TrackBar);
			this.LTPTileSplitContainer.Panel1.Controls.Add(this.LTPTileAngle01TextBox);
			this.LTPTileSplitContainer.Panel1.Controls.Add(this.LTPTileAngle01TrackBar);
			this.LTPTileSplitContainer.Panel1.Controls.Add(this.LTPTileAngle00Label);
			this.LTPTileSplitContainer.Panel2.Controls.Add(this.LTPTileResetStrengthSmoothButton);
			this.LTPTileSplitContainer.Panel2.Controls.Add(this.LTPTileStrengthSmoothTextBox);
			this.LTPTileSplitContainer.Panel2.Controls.Add(this.LTPTileStrengthSmoothPictureBox);
			this.LTPTileSplitContainer.Panel2.Controls.Add(this.LTPTileStrengthTrackBar);
			this.LTPTileSplitContainer.Panel2.Controls.Add(this.LTPTileSmoothTrackBar);
			this.LTPTileSplitContainer.Panel2.Controls.Add(this.LTPTileStrengthSmoothLabel);
			resources.ApplyResources(this.LTPTileAnglePictureBox, "LTPTileAnglePictureBox");
			this.LTPTileAnglePictureBox.BorderStyle = global::System.Windows.Forms.BorderStyle.Fixed3D;
			this.LTPTileAnglePictureBox.Name = "LTPTileAnglePictureBox";
			this.LTPTileAnglePictureBox.TabStop = false;
			this.MainToolTip.SetToolTip(this.LTPTileAnglePictureBox, resources.GetString("LTPTileAnglePictureBox.ToolTip"));
			this.LTPTileAnglePictureBox.Resize += new global::System.EventHandler(this.LTPTileAnglePictureBox_Resize);
			this.LTPTileAnglePictureBox.Paint += new global::System.Windows.Forms.PaintEventHandler(this.LTPTileAnglePictureBox_Paint);
			resources.ApplyResources(this.LTPTileAngle01Label, "LTPTileAngle01Label");
			this.LTPTileAngle01Label.Name = "LTPTileAngle01Label";
			resources.ApplyResources(this.LTPTileUseAngleRestrictionsCheckBox, "LTPTileUseAngleRestrictionsCheckBox");
			this.LTPTileUseAngleRestrictionsCheckBox.Name = "LTPTileUseAngleRestrictionsCheckBox";
			this.LTPTileUseAngleRestrictionsCheckBox.UseVisualStyleBackColor = true;
			this.LTPTileUseAngleRestrictionsCheckBox.CheckedChanged += new global::System.EventHandler(this.LTPTileUseAngleRestrictionsCheckBox_CheckedChanged);
			resources.ApplyResources(this.LTPTileAngle00TextBox, "LTPTileAngle00TextBox");
			this.LTPTileAngle00TextBox.Name = "LTPTileAngle00TextBox";
			this.MainToolTip.SetToolTip(this.LTPTileAngle00TextBox, resources.GetString("LTPTileAngle00TextBox.ToolTip"));
			this.LTPTileAngle00TextBox.TextChanged += new global::System.EventHandler(this.LTPTileAngle00TextBox_TextChanged);
			resources.ApplyResources(this.LTPTileAngle00TrackBar, "LTPTileAngle00TrackBar");
			this.LTPTileAngle00TrackBar.LargeChange = 10;
			this.LTPTileAngle00TrackBar.Maximum = 90;
			this.LTPTileAngle00TrackBar.Name = "LTPTileAngle00TrackBar";
			this.LTPTileAngle00TrackBar.TickFrequency = 10;
			this.MainToolTip.SetToolTip(this.LTPTileAngle00TrackBar, resources.GetString("LTPTileAngle00TrackBar.ToolTip"));
			this.LTPTileAngle00TrackBar.Scroll += new global::System.EventHandler(this.LTPTileAngle00TrackBar_Scroll);
			resources.ApplyResources(this.LTPTileAngle01TextBox, "LTPTileAngle01TextBox");
			this.LTPTileAngle01TextBox.Name = "LTPTileAngle01TextBox";
			this.MainToolTip.SetToolTip(this.LTPTileAngle01TextBox, resources.GetString("LTPTileAngle01TextBox.ToolTip"));
			this.LTPTileAngle01TextBox.TextChanged += new global::System.EventHandler(this.LTPTileAngle01TextBox_TextChanged);
			resources.ApplyResources(this.LTPTileAngle01TrackBar, "LTPTileAngle01TrackBar");
			this.LTPTileAngle01TrackBar.LargeChange = 10;
			this.LTPTileAngle01TrackBar.Maximum = 90;
			this.LTPTileAngle01TrackBar.Name = "LTPTileAngle01TrackBar";
			this.LTPTileAngle01TrackBar.TickFrequency = 10;
			this.MainToolTip.SetToolTip(this.LTPTileAngle01TrackBar, resources.GetString("LTPTileAngle01TrackBar.ToolTip"));
			this.LTPTileAngle01TrackBar.Value = 90;
			this.LTPTileAngle01TrackBar.Scroll += new global::System.EventHandler(this.LTPTileAngle01TrackBar_Scroll);
			resources.ApplyResources(this.LTPTileAngle00Label, "LTPTileAngle00Label");
			this.LTPTileAngle00Label.Name = "LTPTileAngle00Label";
			resources.ApplyResources(this.LTPTileResetStrengthSmoothButton, "LTPTileResetStrengthSmoothButton");
			this.LTPTileResetStrengthSmoothButton.Name = "LTPTileResetStrengthSmoothButton";
			this.MainToolTip.SetToolTip(this.LTPTileResetStrengthSmoothButton, resources.GetString("LTPTileResetStrengthSmoothButton.ToolTip"));
			this.LTPTileResetStrengthSmoothButton.UseVisualStyleBackColor = true;
			this.LTPTileResetStrengthSmoothButton.Click += new global::System.EventHandler(this.LTPTileResetStrengthSmoothButton_Click);
			resources.ApplyResources(this.LTPTileStrengthSmoothTextBox, "LTPTileStrengthSmoothTextBox");
			this.LTPTileStrengthSmoothTextBox.Name = "LTPTileStrengthSmoothTextBox";
			this.MainToolTip.SetToolTip(this.LTPTileStrengthSmoothTextBox, resources.GetString("LTPTileStrengthSmoothTextBox.ToolTip"));
			this.LTPTileStrengthSmoothTextBox.TextChanged += new global::System.EventHandler(this.LTPTileStrengthSmoothTextBox_TextChanged);
			resources.ApplyResources(this.LTPTileStrengthSmoothPictureBox, "LTPTileStrengthSmoothPictureBox");
			this.LTPTileStrengthSmoothPictureBox.BorderStyle = global::System.Windows.Forms.BorderStyle.Fixed3D;
			this.LTPTileStrengthSmoothPictureBox.Name = "LTPTileStrengthSmoothPictureBox";
			this.LTPTileStrengthSmoothPictureBox.TabStop = false;
			this.MainToolTip.SetToolTip(this.LTPTileStrengthSmoothPictureBox, resources.GetString("LTPTileStrengthSmoothPictureBox.ToolTip"));
			this.LTPTileStrengthSmoothPictureBox.Resize += new global::System.EventHandler(this.LTPTileStrengthSmoothPictureBox_Resize);
			this.LTPTileStrengthSmoothPictureBox.Paint += new global::System.Windows.Forms.PaintEventHandler(this.LTPTileStrengthSmoothPictureBox_Paint);
			resources.ApplyResources(this.LTPTileStrengthTrackBar, "LTPTileStrengthTrackBar");
			this.LTPTileStrengthTrackBar.LargeChange = 10;
			this.LTPTileStrengthTrackBar.Maximum = 100;
			this.LTPTileStrengthTrackBar.Name = "LTPTileStrengthTrackBar";
			this.LTPTileStrengthTrackBar.TickFrequency = 10;
			this.MainToolTip.SetToolTip(this.LTPTileStrengthTrackBar, resources.GetString("LTPTileStrengthTrackBar.ToolTip"));
			this.LTPTileStrengthTrackBar.Value = 10;
			this.LTPTileStrengthTrackBar.Scroll += new global::System.EventHandler(this.LTPTileStrengthTrackBar_Scroll);
			resources.ApplyResources(this.LTPTileSmoothTrackBar, "LTPTileSmoothTrackBar");
			this.LTPTileSmoothTrackBar.LargeChange = 10;
			this.LTPTileSmoothTrackBar.Maximum = 100;
			this.LTPTileSmoothTrackBar.Name = "LTPTileSmoothTrackBar";
			this.LTPTileSmoothTrackBar.TickFrequency = 10;
			this.MainToolTip.SetToolTip(this.LTPTileSmoothTrackBar, resources.GetString("LTPTileSmoothTrackBar.ToolTip"));
			this.LTPTileSmoothTrackBar.Value = 10;
			this.LTPTileSmoothTrackBar.Scroll += new global::System.EventHandler(this.LTPTileSmoothTrackBar_Scroll);
			resources.ApplyResources(this.LTPTileStrengthSmoothLabel, "LTPTileStrengthSmoothLabel");
			this.LTPTileStrengthSmoothLabel.Name = "LTPTileStrengthSmoothLabel";
			resources.ApplyResources(this.LTPTileBrushButton, "LTPTileBrushButton");
			this.LTPTileBrushButton.Name = "LTPTileBrushButton";
			this.LTPTileBrushButton.UseVisualStyleBackColor = true;
			this.LTPTileBrushButton.Click += new global::System.EventHandler(this.LTPTileBrushButton_Click);
			resources.ApplyResources(this.LTPTileBrushComboBox, "LTPTileBrushComboBox");
			this.LTPTileBrushComboBox.DropDownStyle = global::System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.LTPTileBrushComboBox.FormattingEnabled = true;
			this.LTPTileBrushComboBox.Name = "LTPTileBrushComboBox";
			this.MainToolTip.SetToolTip(this.LTPTileBrushComboBox, resources.GetString("LTPTileBrushComboBox.ToolTip"));
			resources.ApplyResources(this.LTPTileBrushListView, "LTPTileBrushListView");
			this.LTPTileBrushListView.HideSelection = false;
			this.LTPTileBrushListView.Name = "LTPTileBrushListView";
			this.LTPTileBrushListView.ShowItemToolTips = true;
			this.LTPTileBrushListView.Sorting = global::System.Windows.Forms.SortOrder.Ascending;
			this.LTPTileBrushListView.UseCompatibleStateImageBehavior = false;
			this.LTPTileBrushListView.View = global::System.Windows.Forms.View.List;
			resources.ApplyResources(this.LTPTileBrushLabel, "LTPTileBrushLabel");
			this.LTPTileBrushLabel.Name = "LTPTileBrushLabel";
			resources.ApplyResources(this.LandscapeTabPageGradient, "LandscapeTabPageGradient");
			this.LandscapeTabPageGradient.Name = "LandscapeTabPageGradient";
			this.LandscapeTabPageGradient.UseVisualStyleBackColor = true;
			this.LandscapeTabPageWater.Controls.Add(this.LTPWaterBrushListView);
			this.LandscapeTabPageWater.Controls.Add(this.LTPWaterBrushButton);
			this.LandscapeTabPageWater.Controls.Add(this.LTPWaterBrushComboBox);
			this.LandscapeTabPageWater.Controls.Add(this.label11);
			this.LandscapeTabPageWater.Controls.Add(this.LTPWaterHeightTextBox);
			this.LandscapeTabPageWater.Controls.Add(this.LTPWaterHeightLabel);
			this.LandscapeTabPageWater.Controls.Add(this.LTPWaterWaterToolPanel);
			this.LandscapeTabPageWater.Controls.Add(this.LTPWaterWaterToolLabel);
			resources.ApplyResources(this.LandscapeTabPageWater, "LandscapeTabPageWater");
			this.LandscapeTabPageWater.Name = "LandscapeTabPageWater";
			this.LandscapeTabPageWater.UseVisualStyleBackColor = true;
			resources.ApplyResources(this.LTPWaterBrushListView, "LTPWaterBrushListView");
			this.LTPWaterBrushListView.HideSelection = false;
			this.LTPWaterBrushListView.Name = "LTPWaterBrushListView";
			this.LTPWaterBrushListView.ShowItemToolTips = true;
			this.LTPWaterBrushListView.Sorting = global::System.Windows.Forms.SortOrder.Ascending;
			this.LTPWaterBrushListView.UseCompatibleStateImageBehavior = false;
			this.LTPWaterBrushListView.View = global::System.Windows.Forms.View.List;
			resources.ApplyResources(this.LTPWaterBrushButton, "LTPWaterBrushButton");
			this.LTPWaterBrushButton.Name = "LTPWaterBrushButton";
			this.LTPWaterBrushButton.UseVisualStyleBackColor = true;
			this.LTPWaterBrushButton.Click += new global::System.EventHandler(this.LTPWaterBrushButton_Click);
			resources.ApplyResources(this.LTPWaterBrushComboBox, "LTPWaterBrushComboBox");
			this.LTPWaterBrushComboBox.DropDownStyle = global::System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.LTPWaterBrushComboBox.FormattingEnabled = true;
			this.LTPWaterBrushComboBox.Name = "LTPWaterBrushComboBox";
			this.MainToolTip.SetToolTip(this.LTPWaterBrushComboBox, resources.GetString("LTPWaterBrushComboBox.ToolTip"));
			resources.ApplyResources(this.label11, "label11");
			this.label11.Name = "label11";
			resources.ApplyResources(this.LTPWaterHeightTextBox, "LTPWaterHeightTextBox");
			this.LTPWaterHeightTextBox.Name = "LTPWaterHeightTextBox";
			this.MainToolTip.SetToolTip(this.LTPWaterHeightTextBox, resources.GetString("LTPWaterHeightTextBox.ToolTip"));
			this.LTPWaterHeightTextBox.TextChanged += new global::System.EventHandler(this.LTPWaterHeightTextBox_TextChanged);
			resources.ApplyResources(this.LTPWaterHeightLabel, "LTPWaterHeightLabel");
			this.LTPWaterHeightLabel.Name = "LTPWaterHeightLabel";
			this.LTPWaterWaterToolPanel.Controls.Add(this.LTPWaterWaterToolButton03);
			this.LTPWaterWaterToolPanel.Controls.Add(this.LTPWaterWaterToolButton02);
			this.LTPWaterWaterToolPanel.Controls.Add(this.LTPWaterWaterToolButton01);
			this.LTPWaterWaterToolPanel.Controls.Add(this.LTPWaterWaterToolButton00);
			resources.ApplyResources(this.LTPWaterWaterToolPanel, "LTPWaterWaterToolPanel");
			this.LTPWaterWaterToolPanel.Name = "LTPWaterWaterToolPanel";
			resources.ApplyResources(this.LTPWaterWaterToolButton03, "LTPWaterWaterToolButton03");
			this.LTPWaterWaterToolButton03.ImageList = this.LandscapeWaterToolImageList;
			this.LTPWaterWaterToolButton03.Name = "LTPWaterWaterToolButton03";
			this.MainToolTip.SetToolTip(this.LTPWaterWaterToolButton03, resources.GetString("LTPWaterWaterToolButton03.ToolTip"));
			this.LTPWaterWaterToolButton03.UseMnemonic = false;
			this.LTPWaterWaterToolButton03.UseVisualStyleBackColor = true;
			this.LTPWaterWaterToolButton03.CheckedChanged += new global::System.EventHandler(this.LTPWaterWaterToolButton03_CheckedChanged);
			this.LandscapeWaterToolImageList.ImageStream = (global::System.Windows.Forms.ImageListStreamer)resources.GetObject("LandscapeWaterToolImageList.ImageStream");
			this.LandscapeWaterToolImageList.TransparentColor = global::System.Drawing.Color.Magenta;
			this.LandscapeWaterToolImageList.Images.SetKeyName(0, "add_water.bmp");
			this.LandscapeWaterToolImageList.Images.SetKeyName(1, "remove_water.bmp");
			this.LandscapeWaterToolImageList.Images.SetKeyName(2, "move_water_to_height.bmp");
			this.LandscapeWaterToolImageList.Images.SetKeyName(3, "pick_and_move_water_to_height.bmp");
			this.LandscapeWaterToolImageList.Images.SetKeyName(4, "remove_water_direction.bmp");
			resources.ApplyResources(this.LTPWaterWaterToolButton02, "LTPWaterWaterToolButton02");
			this.LTPWaterWaterToolButton02.ImageList = this.LandscapeWaterToolImageList;
			this.LTPWaterWaterToolButton02.Name = "LTPWaterWaterToolButton02";
			this.MainToolTip.SetToolTip(this.LTPWaterWaterToolButton02, resources.GetString("LTPWaterWaterToolButton02.ToolTip"));
			this.LTPWaterWaterToolButton02.UseMnemonic = false;
			this.LTPWaterWaterToolButton02.UseVisualStyleBackColor = true;
			this.LTPWaterWaterToolButton02.CheckedChanged += new global::System.EventHandler(this.LTPWaterWaterToolButton02_CheckedChanged);
			resources.ApplyResources(this.LTPWaterWaterToolButton01, "LTPWaterWaterToolButton01");
			this.LTPWaterWaterToolButton01.ImageList = this.LandscapeWaterToolImageList;
			this.LTPWaterWaterToolButton01.Name = "LTPWaterWaterToolButton01";
			this.MainToolTip.SetToolTip(this.LTPWaterWaterToolButton01, resources.GetString("LTPWaterWaterToolButton01.ToolTip"));
			this.LTPWaterWaterToolButton01.UseMnemonic = false;
			this.LTPWaterWaterToolButton01.UseVisualStyleBackColor = true;
			this.LTPWaterWaterToolButton01.CheckedChanged += new global::System.EventHandler(this.LTPWaterWaterToolButton01_CheckedChanged);
			resources.ApplyResources(this.LTPWaterWaterToolButton00, "LTPWaterWaterToolButton00");
			this.LTPWaterWaterToolButton00.Checked = true;
			this.LTPWaterWaterToolButton00.ImageList = this.LandscapeWaterToolImageList;
			this.LTPWaterWaterToolButton00.Name = "LTPWaterWaterToolButton00";
			this.LTPWaterWaterToolButton00.TabStop = true;
			this.MainToolTip.SetToolTip(this.LTPWaterWaterToolButton00, resources.GetString("LTPWaterWaterToolButton00.ToolTip"));
			this.LTPWaterWaterToolButton00.UseMnemonic = false;
			this.LTPWaterWaterToolButton00.UseVisualStyleBackColor = true;
			this.LTPWaterWaterToolButton00.CheckedChanged += new global::System.EventHandler(this.LTPWaterWaterToolButton00_CheckedChanged);
			resources.ApplyResources(this.LTPWaterWaterToolLabel, "LTPWaterWaterToolLabel");
			this.LTPWaterWaterToolLabel.Name = "LTPWaterWaterToolLabel";
			this.LandscapeTabPageHeight.Controls.Add(this.LTPHeightBrushButton);
			this.LandscapeTabPageHeight.Controls.Add(this.LTPHeightBrushComboBox);
			this.LandscapeTabPageHeight.Controls.Add(this.LTPHeightBrushListView);
			this.LandscapeTabPageHeight.Controls.Add(this.LTPHeightBrushLabel);
			this.LandscapeTabPageHeight.Controls.Add(this.LTPHeightSplitContainer);
			this.LandscapeTabPageHeight.Controls.Add(this.LTPHeightAutomaticToolCheckBox);
			this.LandscapeTabPageHeight.Controls.Add(this.LTPHeightPreciseToolCheckBox);
			resources.ApplyResources(this.LandscapeTabPageHeight, "LandscapeTabPageHeight");
			this.LandscapeTabPageHeight.Name = "LandscapeTabPageHeight";
			this.LandscapeTabPageHeight.UseVisualStyleBackColor = true;
			resources.ApplyResources(this.LTPHeightBrushButton, "LTPHeightBrushButton");
			this.LTPHeightBrushButton.Name = "LTPHeightBrushButton";
			this.LTPHeightBrushButton.UseVisualStyleBackColor = true;
			this.LTPHeightBrushButton.Click += new global::System.EventHandler(this.LTPHeightBrushButton_Click);
			resources.ApplyResources(this.LTPHeightBrushComboBox, "LTPHeightBrushComboBox");
			this.LTPHeightBrushComboBox.DropDownStyle = global::System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.LTPHeightBrushComboBox.FormattingEnabled = true;
			this.LTPHeightBrushComboBox.Name = "LTPHeightBrushComboBox";
			this.MainToolTip.SetToolTip(this.LTPHeightBrushComboBox, resources.GetString("LTPHeightBrushComboBox.ToolTip"));
			resources.ApplyResources(this.LTPHeightBrushListView, "LTPHeightBrushListView");
			this.LTPHeightBrushListView.HideSelection = false;
			this.LTPHeightBrushListView.Name = "LTPHeightBrushListView";
			this.LTPHeightBrushListView.ShowItemToolTips = true;
			this.LTPHeightBrushListView.Sorting = global::System.Windows.Forms.SortOrder.Ascending;
			this.LTPHeightBrushListView.UseCompatibleStateImageBehavior = false;
			this.LTPHeightBrushListView.View = global::System.Windows.Forms.View.List;
			resources.ApplyResources(this.LTPHeightBrushLabel, "LTPHeightBrushLabel");
			this.LTPHeightBrushLabel.Name = "LTPHeightBrushLabel";
			this.MainToolTip.SetToolTip(this.LTPHeightBrushLabel, resources.GetString("LTPHeightBrushLabel.ToolTip"));
			resources.ApplyResources(this.LTPHeightSplitContainer, "LTPHeightSplitContainer");
			this.LTPHeightSplitContainer.Name = "LTPHeightSplitContainer";
			this.LTPHeightSplitContainer.Panel1.Controls.Add(this.LTPHeightRaisePlatoCheckBox);
			this.LTPHeightSplitContainer.Panel1.Controls.Add(this.LTPHeightLowerPlatoCheckBox);
			this.LTPHeightSplitContainer.Panel1.Controls.Add(this.LTPHeightHeightTextBox);
			this.LTPHeightSplitContainer.Panel1.Controls.Add(this.LTPHeightHeightLabel);
			this.LTPHeightSplitContainer.Panel1.Controls.Add(this.LTPHeightUpdateObjectsTextBox);
			this.LTPHeightSplitContainer.Panel1.Controls.Add(this.LTPHeightUpdateObjectsCheckBox);
			this.LTPHeightSplitContainer.Panel1.Controls.Add(this.LTPHeightHeightToolPanel);
			this.LTPHeightSplitContainer.Panel1.Controls.Add(this.LTPHeightHeightToolLabel);
			this.LTPHeightSplitContainer.Panel2.Controls.Add(this.LTPHeightResetStrengthSmoothButton);
			this.LTPHeightSplitContainer.Panel2.Controls.Add(this.LTPHeightStrengthSmoothTextBox);
			this.LTPHeightSplitContainer.Panel2.Controls.Add(this.LTPHeightStrengthSmoothPictureBox);
			this.LTPHeightSplitContainer.Panel2.Controls.Add(this.LTPHeightStrengthTrackBar);
			this.LTPHeightSplitContainer.Panel2.Controls.Add(this.LTPHeightSmoothTrackBar);
			this.LTPHeightSplitContainer.Panel2.Controls.Add(this.LTPHeightStrengthSmoothLabel);
			resources.ApplyResources(this.LTPHeightRaisePlatoCheckBox, "LTPHeightRaisePlatoCheckBox");
			this.LTPHeightRaisePlatoCheckBox.Name = "LTPHeightRaisePlatoCheckBox";
			this.LTPHeightRaisePlatoCheckBox.UseVisualStyleBackColor = true;
			this.LTPHeightRaisePlatoCheckBox.CheckedChanged += new global::System.EventHandler(this.LTPHeightRaisePlatoCheckBox_CheckedChanged);
			resources.ApplyResources(this.LTPHeightLowerPlatoCheckBox, "LTPHeightLowerPlatoCheckBox");
			this.LTPHeightLowerPlatoCheckBox.Name = "LTPHeightLowerPlatoCheckBox";
			this.LTPHeightLowerPlatoCheckBox.UseVisualStyleBackColor = true;
			this.LTPHeightLowerPlatoCheckBox.CheckedChanged += new global::System.EventHandler(this.LTPHeightLowerPlatoCheckBox_CheckedChanged);
			resources.ApplyResources(this.LTPHeightHeightTextBox, "LTPHeightHeightTextBox");
			this.LTPHeightHeightTextBox.Name = "LTPHeightHeightTextBox";
			this.MainToolTip.SetToolTip(this.LTPHeightHeightTextBox, resources.GetString("LTPHeightHeightTextBox.ToolTip"));
			this.LTPHeightHeightTextBox.TextChanged += new global::System.EventHandler(this.LTPHeightHeightTextBox_TextChanged);
			resources.ApplyResources(this.LTPHeightHeightLabel, "LTPHeightHeightLabel");
			this.LTPHeightHeightLabel.Name = "LTPHeightHeightLabel";
			resources.ApplyResources(this.LTPHeightUpdateObjectsTextBox, "LTPHeightUpdateObjectsTextBox");
			this.LTPHeightUpdateObjectsTextBox.Name = "LTPHeightUpdateObjectsTextBox";
			this.MainToolTip.SetToolTip(this.LTPHeightUpdateObjectsTextBox, resources.GetString("LTPHeightUpdateObjectsTextBox.ToolTip"));
			this.LTPHeightUpdateObjectsTextBox.TextChanged += new global::System.EventHandler(this.LTPHeightUpdateObjectsTextBox_TextChanged);
			resources.ApplyResources(this.LTPHeightUpdateObjectsCheckBox, "LTPHeightUpdateObjectsCheckBox");
			this.LTPHeightUpdateObjectsCheckBox.Name = "LTPHeightUpdateObjectsCheckBox";
			this.LTPHeightUpdateObjectsCheckBox.UseVisualStyleBackColor = true;
			this.LTPHeightUpdateObjectsCheckBox.CheckedChanged += new global::System.EventHandler(this.LTPHeightUpdateObjectsCheckBox_CheckedChanged);
			this.LTPHeightHeightToolPanel.Controls.Add(this.LTPHeightHeightToolButton05);
			this.LTPHeightHeightToolPanel.Controls.Add(this.LTPHeightHeightToolButton04);
			this.LTPHeightHeightToolPanel.Controls.Add(this.LTPHeightHeightToolButton03);
			this.LTPHeightHeightToolPanel.Controls.Add(this.LTPHeightHeightToolButton02);
			this.LTPHeightHeightToolPanel.Controls.Add(this.LTPHeightHeightToolButton01);
			this.LTPHeightHeightToolPanel.Controls.Add(this.LTPHeightHeightToolButton00);
			resources.ApplyResources(this.LTPHeightHeightToolPanel, "LTPHeightHeightToolPanel");
			this.LTPHeightHeightToolPanel.Name = "LTPHeightHeightToolPanel";
			resources.ApplyResources(this.LTPHeightHeightToolButton05, "LTPHeightHeightToolButton05");
			this.LTPHeightHeightToolButton05.ImageList = this.LandscapeHeightToolImageList;
			this.LTPHeightHeightToolButton05.Name = "LTPHeightHeightToolButton05";
			this.MainToolTip.SetToolTip(this.LTPHeightHeightToolButton05, resources.GetString("LTPHeightHeightToolButton05.ToolTip"));
			this.LTPHeightHeightToolButton05.UseMnemonic = false;
			this.LTPHeightHeightToolButton05.UseVisualStyleBackColor = true;
			this.LTPHeightHeightToolButton05.CheckedChanged += new global::System.EventHandler(this.LTPHeightHeightToolButton05_CheckedChanged);
			resources.ApplyResources(this.LTPHeightHeightToolButton04, "LTPHeightHeightToolButton04");
			this.LTPHeightHeightToolButton04.ImageList = this.LandscapeHeightToolImageList;
			this.LTPHeightHeightToolButton04.Name = "LTPHeightHeightToolButton04";
			this.MainToolTip.SetToolTip(this.LTPHeightHeightToolButton04, resources.GetString("LTPHeightHeightToolButton04.ToolTip"));
			this.LTPHeightHeightToolButton04.UseMnemonic = false;
			this.LTPHeightHeightToolButton04.UseVisualStyleBackColor = true;
			this.LTPHeightHeightToolButton04.CheckedChanged += new global::System.EventHandler(this.LTPHeightHeightToolButton04_CheckedChanged);
			resources.ApplyResources(this.LTPHeightHeightToolButton03, "LTPHeightHeightToolButton03");
			this.LTPHeightHeightToolButton03.ImageList = this.LandscapeHeightToolImageList;
			this.LTPHeightHeightToolButton03.Name = "LTPHeightHeightToolButton03";
			this.MainToolTip.SetToolTip(this.LTPHeightHeightToolButton03, resources.GetString("LTPHeightHeightToolButton03.ToolTip"));
			this.LTPHeightHeightToolButton03.UseMnemonic = false;
			this.LTPHeightHeightToolButton03.UseVisualStyleBackColor = true;
			this.LTPHeightHeightToolButton03.CheckedChanged += new global::System.EventHandler(this.LTPHeightHeightToolButton03_CheckedChanged);
			resources.ApplyResources(this.LTPHeightHeightToolButton02, "LTPHeightHeightToolButton02");
			this.LTPHeightHeightToolButton02.ImageList = this.LandscapeHeightToolImageList;
			this.LTPHeightHeightToolButton02.Name = "LTPHeightHeightToolButton02";
			this.MainToolTip.SetToolTip(this.LTPHeightHeightToolButton02, resources.GetString("LTPHeightHeightToolButton02.ToolTip"));
			this.LTPHeightHeightToolButton02.UseMnemonic = false;
			this.LTPHeightHeightToolButton02.UseVisualStyleBackColor = true;
			this.LTPHeightHeightToolButton02.CheckedChanged += new global::System.EventHandler(this.LTPHeightHeightToolButton02_CheckedChanged);
			resources.ApplyResources(this.LTPHeightHeightToolButton01, "LTPHeightHeightToolButton01");
			this.LTPHeightHeightToolButton01.ImageList = this.LandscapeHeightToolImageList;
			this.LTPHeightHeightToolButton01.Name = "LTPHeightHeightToolButton01";
			this.MainToolTip.SetToolTip(this.LTPHeightHeightToolButton01, resources.GetString("LTPHeightHeightToolButton01.ToolTip"));
			this.LTPHeightHeightToolButton01.UseMnemonic = false;
			this.LTPHeightHeightToolButton01.UseVisualStyleBackColor = true;
			this.LTPHeightHeightToolButton01.CheckedChanged += new global::System.EventHandler(this.LTPHeightHeightToolButton01_CheckedChanged);
			resources.ApplyResources(this.LTPHeightHeightToolButton00, "LTPHeightHeightToolButton00");
			this.LTPHeightHeightToolButton00.ImageList = this.LandscapeHeightToolImageList;
			this.LTPHeightHeightToolButton00.Name = "LTPHeightHeightToolButton00";
			this.MainToolTip.SetToolTip(this.LTPHeightHeightToolButton00, resources.GetString("LTPHeightHeightToolButton00.ToolTip"));
			this.LTPHeightHeightToolButton00.UseMnemonic = false;
			this.LTPHeightHeightToolButton00.UseVisualStyleBackColor = true;
			this.LTPHeightHeightToolButton00.CheckedChanged += new global::System.EventHandler(this.LTPHeightHeightToolButton00_CheckedChanged);
			resources.ApplyResources(this.LTPHeightHeightToolLabel, "LTPHeightHeightToolLabel");
			this.LTPHeightHeightToolLabel.Name = "LTPHeightHeightToolLabel";
			resources.ApplyResources(this.LTPHeightResetStrengthSmoothButton, "LTPHeightResetStrengthSmoothButton");
			this.LTPHeightResetStrengthSmoothButton.Name = "LTPHeightResetStrengthSmoothButton";
			this.MainToolTip.SetToolTip(this.LTPHeightResetStrengthSmoothButton, resources.GetString("LTPHeightResetStrengthSmoothButton.ToolTip"));
			this.LTPHeightResetStrengthSmoothButton.UseVisualStyleBackColor = true;
			this.LTPHeightResetStrengthSmoothButton.Click += new global::System.EventHandler(this.LTPHeightResetStrengthSmoothButton_Click);
			resources.ApplyResources(this.LTPHeightStrengthSmoothTextBox, "LTPHeightStrengthSmoothTextBox");
			this.LTPHeightStrengthSmoothTextBox.Name = "LTPHeightStrengthSmoothTextBox";
			this.MainToolTip.SetToolTip(this.LTPHeightStrengthSmoothTextBox, resources.GetString("LTPHeightStrengthSmoothTextBox.ToolTip"));
			this.LTPHeightStrengthSmoothTextBox.TextChanged += new global::System.EventHandler(this.LTPHeightStrengthSmoothTextBox_TextChanged);
			resources.ApplyResources(this.LTPHeightStrengthSmoothPictureBox, "LTPHeightStrengthSmoothPictureBox");
			this.LTPHeightStrengthSmoothPictureBox.BorderStyle = global::System.Windows.Forms.BorderStyle.Fixed3D;
			this.LTPHeightStrengthSmoothPictureBox.Name = "LTPHeightStrengthSmoothPictureBox";
			this.LTPHeightStrengthSmoothPictureBox.TabStop = false;
			this.MainToolTip.SetToolTip(this.LTPHeightStrengthSmoothPictureBox, resources.GetString("LTPHeightStrengthSmoothPictureBox.ToolTip"));
			this.LTPHeightStrengthSmoothPictureBox.Resize += new global::System.EventHandler(this.LTPHeightStrengthSmoothPictureBox_Resize);
			this.LTPHeightStrengthSmoothPictureBox.Paint += new global::System.Windows.Forms.PaintEventHandler(this.LTPHeightStrengthSmoothPictureBox_Paint);
			resources.ApplyResources(this.LTPHeightStrengthTrackBar, "LTPHeightStrengthTrackBar");
			this.LTPHeightStrengthTrackBar.LargeChange = 10;
			this.LTPHeightStrengthTrackBar.Maximum = 100;
			this.LTPHeightStrengthTrackBar.Name = "LTPHeightStrengthTrackBar";
			this.LTPHeightStrengthTrackBar.TickFrequency = 10;
			this.MainToolTip.SetToolTip(this.LTPHeightStrengthTrackBar, resources.GetString("LTPHeightStrengthTrackBar.ToolTip"));
			this.LTPHeightStrengthTrackBar.Value = 10;
			this.LTPHeightStrengthTrackBar.Scroll += new global::System.EventHandler(this.LTPHeightStrengthTrackBar_Scroll);
			resources.ApplyResources(this.LTPHeightSmoothTrackBar, "LTPHeightSmoothTrackBar");
			this.LTPHeightSmoothTrackBar.LargeChange = 10;
			this.LTPHeightSmoothTrackBar.Maximum = 100;
			this.LTPHeightSmoothTrackBar.Name = "LTPHeightSmoothTrackBar";
			this.LTPHeightSmoothTrackBar.TickFrequency = 10;
			this.MainToolTip.SetToolTip(this.LTPHeightSmoothTrackBar, resources.GetString("LTPHeightSmoothTrackBar.ToolTip"));
			this.LTPHeightSmoothTrackBar.Value = 10;
			this.LTPHeightSmoothTrackBar.Scroll += new global::System.EventHandler(this.LTPHeightSmoothTrackBar_Scroll);
			resources.ApplyResources(this.LTPHeightStrengthSmoothLabel, "LTPHeightStrengthSmoothLabel");
			this.LTPHeightStrengthSmoothLabel.Name = "LTPHeightStrengthSmoothLabel";
			resources.ApplyResources(this.LTPHeightAutomaticToolCheckBox, "LTPHeightAutomaticToolCheckBox");
			this.LTPHeightAutomaticToolCheckBox.Name = "LTPHeightAutomaticToolCheckBox";
			this.LTPHeightAutomaticToolCheckBox.UseVisualStyleBackColor = true;
			this.LTPHeightAutomaticToolCheckBox.CheckedChanged += new global::System.EventHandler(this.LTPHeightAutomaticToolCheckBox_CheckedChanged);
			resources.ApplyResources(this.LTPHeightPreciseToolCheckBox, "LTPHeightPreciseToolCheckBox");
			this.LTPHeightPreciseToolCheckBox.Name = "LTPHeightPreciseToolCheckBox";
			this.LTPHeightPreciseToolCheckBox.UseVisualStyleBackColor = true;
			this.LTPHeightPreciseToolCheckBox.CheckedChanged += new global::System.EventHandler(this.LTPHeightPreciseToolCheckBox_CheckedChanged);
			this.LandscapeTabPageClipboard.Controls.Add(this.LTPClipboardGroupTextBox);
			this.LandscapeTabPageClipboard.Controls.Add(this.LTPClipboardGroupTextBoxLabel);
			this.LandscapeTabPageClipboard.Controls.Add(this.LTPClipboardTwoSidedCheckBox);
			this.LandscapeTabPageClipboard.Controls.Add(this.LTPClipboardBrushButton);
			this.LandscapeTabPageClipboard.Controls.Add(this.LTPClipboardBrushComboBox);
			this.LandscapeTabPageClipboard.Controls.Add(this.LTPClipboardBrushListView);
			this.LandscapeTabPageClipboard.Controls.Add(this.LTPClipboardBrushLabel);
			this.LandscapeTabPageClipboard.Controls.Add(this.LTPClipboardSplitContainer);
			resources.ApplyResources(this.LandscapeTabPageClipboard, "LandscapeTabPageClipboard");
			this.LandscapeTabPageClipboard.Name = "LandscapeTabPageClipboard";
			this.LandscapeTabPageClipboard.UseVisualStyleBackColor = true;
			resources.ApplyResources(this.LTPClipboardGroupTextBox, "LTPClipboardGroupTextBox");
			this.LTPClipboardGroupTextBox.Name = "LTPClipboardGroupTextBox";
			this.MainToolTip.SetToolTip(this.LTPClipboardGroupTextBox, resources.GetString("LTPClipboardGroupTextBox.ToolTip"));
			this.LTPClipboardGroupTextBox.TextChanged += new global::System.EventHandler(this.LTPClipboardGroupTextBox_TextChanged);
			resources.ApplyResources(this.LTPClipboardGroupTextBoxLabel, "LTPClipboardGroupTextBoxLabel");
			this.LTPClipboardGroupTextBoxLabel.Name = "LTPClipboardGroupTextBoxLabel";
			this.MainToolTip.SetToolTip(this.LTPClipboardGroupTextBoxLabel, resources.GetString("LTPClipboardGroupTextBoxLabel.ToolTip"));
			resources.ApplyResources(this.LTPClipboardTwoSidedCheckBox, "LTPClipboardTwoSidedCheckBox");
			this.LTPClipboardTwoSidedCheckBox.Name = "LTPClipboardTwoSidedCheckBox";
			this.LTPClipboardTwoSidedCheckBox.UseVisualStyleBackColor = true;
			this.LTPClipboardTwoSidedCheckBox.CheckedChanged += new global::System.EventHandler(this.LTPClipboardTwoSidedCheckBox_CheckedChanged);
			resources.ApplyResources(this.LTPClipboardBrushButton, "LTPClipboardBrushButton");
			this.LTPClipboardBrushButton.Name = "LTPClipboardBrushButton";
			this.LTPClipboardBrushButton.UseVisualStyleBackColor = true;
			this.LTPClipboardBrushButton.Click += new global::System.EventHandler(this.LTPClipboardBrushButton_Click);
			resources.ApplyResources(this.LTPClipboardBrushComboBox, "LTPClipboardBrushComboBox");
			this.LTPClipboardBrushComboBox.DropDownStyle = global::System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.LTPClipboardBrushComboBox.FormattingEnabled = true;
			this.LTPClipboardBrushComboBox.Name = "LTPClipboardBrushComboBox";
			this.MainToolTip.SetToolTip(this.LTPClipboardBrushComboBox, resources.GetString("LTPClipboardBrushComboBox.ToolTip"));
			resources.ApplyResources(this.LTPClipboardBrushListView, "LTPClipboardBrushListView");
			this.LTPClipboardBrushListView.HideSelection = false;
			this.LTPClipboardBrushListView.Name = "LTPClipboardBrushListView";
			this.LTPClipboardBrushListView.ShowItemToolTips = true;
			this.LTPClipboardBrushListView.Sorting = global::System.Windows.Forms.SortOrder.Ascending;
			this.LTPClipboardBrushListView.UseCompatibleStateImageBehavior = false;
			this.LTPClipboardBrushListView.View = global::System.Windows.Forms.View.List;
			resources.ApplyResources(this.LTPClipboardBrushLabel, "LTPClipboardBrushLabel");
			this.LTPClipboardBrushLabel.Name = "LTPClipboardBrushLabel";
			this.MainToolTip.SetToolTip(this.LTPClipboardBrushLabel, resources.GetString("LTPClipboardBrushLabel.ToolTip"));
			resources.ApplyResources(this.LTPClipboardSplitContainer, "LTPClipboardSplitContainer");
			this.LTPClipboardSplitContainer.Name = "LTPClipboardSplitContainer";
			this.LTPClipboardSplitContainer.Panel1.Controls.Add(this.LTPClipboardCopyTypeButton02);
			this.LTPClipboardSplitContainer.Panel1.Controls.Add(this.LTPClipboardCopyHeightTypeLabel);
			this.LTPClipboardSplitContainer.Panel1.Controls.Add(this.LTPClipboardCopyHeightTypePanel);
			this.LTPClipboardSplitContainer.Panel1.Controls.Add(this.LTPClipboardFlipHorisontalCheckBox);
			this.LTPClipboardSplitContainer.Panel1.Controls.Add(this.LTPClipboardFlipVerticalCheckBox);
			this.LTPClipboardSplitContainer.Panel1.Controls.Add(this.LTPClipboardUpdateObjectsTextBox);
			this.LTPClipboardSplitContainer.Panel1.Controls.Add(this.LTPClipboardUpdateObjectsCheckBox);
			this.LTPClipboardSplitContainer.Panel1.Controls.Add(this.LTPClipboardCopyType);
			this.LTPClipboardSplitContainer.Panel1.Controls.Add(this.LTPClipboardPreciseToolCheckBox);
			this.LTPClipboardSplitContainer.Panel1.Controls.Add(this.LTPClipboardCopyTypeButton00);
			this.LTPClipboardSplitContainer.Panel1.Controls.Add(this.LTPClipboardCopyTypeButton01);
			this.LTPClipboardSplitContainer.Panel2.Controls.Add(this.LTPClipboardResetStrengthSmoothButton);
			this.LTPClipboardSplitContainer.Panel2.Controls.Add(this.LTPClipboardStrengthSmoothTextBox);
			this.LTPClipboardSplitContainer.Panel2.Controls.Add(this.LTPClipboardStrengthSmoothPictureBox);
			this.LTPClipboardSplitContainer.Panel2.Controls.Add(this.LTPClipboardStrengthTrackBar);
			this.LTPClipboardSplitContainer.Panel2.Controls.Add(this.LTPClipboardSmoothTrackBar);
			this.LTPClipboardSplitContainer.Panel2.Controls.Add(this.LTPClipboardStrengthSmoothLabel);
			resources.ApplyResources(this.LTPClipboardCopyTypeButton02, "LTPClipboardCopyTypeButton02");
			this.LTPClipboardCopyTypeButton02.ImageList = this.LandscapeCopyTypeImageList;
			this.LTPClipboardCopyTypeButton02.Name = "LTPClipboardCopyTypeButton02";
			this.MainToolTip.SetToolTip(this.LTPClipboardCopyTypeButton02, resources.GetString("LTPClipboardCopyTypeButton02.ToolTip"));
			this.LTPClipboardCopyTypeButton02.UseVisualStyleBackColor = true;
			this.LTPClipboardCopyTypeButton02.CheckedChanged += new global::System.EventHandler(this.LTPClipboardCopyTypeButton02_CheckedChanged);
			resources.ApplyResources(this.LTPClipboardCopyHeightTypeLabel, "LTPClipboardCopyHeightTypeLabel");
			this.LTPClipboardCopyHeightTypeLabel.Name = "LTPClipboardCopyHeightTypeLabel";
			this.LTPClipboardCopyHeightTypePanel.Controls.Add(this.LTPClipboardCopyHeightTypeButton03);
			this.LTPClipboardCopyHeightTypePanel.Controls.Add(this.LTPClipboardCopyHeightTypeButton02);
			this.LTPClipboardCopyHeightTypePanel.Controls.Add(this.LTPClipboardCopyHeightTypeButton01);
			this.LTPClipboardCopyHeightTypePanel.Controls.Add(this.LTPClipboardCopyHeightTypeButton00);
			resources.ApplyResources(this.LTPClipboardCopyHeightTypePanel, "LTPClipboardCopyHeightTypePanel");
			this.LTPClipboardCopyHeightTypePanel.Name = "LTPClipboardCopyHeightTypePanel";
			resources.ApplyResources(this.LTPClipboardCopyHeightTypeButton03, "LTPClipboardCopyHeightTypeButton03");
			this.LTPClipboardCopyHeightTypeButton03.ImageList = this.LandscapeCopyHeightTypeImageList;
			this.LTPClipboardCopyHeightTypeButton03.Name = "LTPClipboardCopyHeightTypeButton03";
			this.MainToolTip.SetToolTip(this.LTPClipboardCopyHeightTypeButton03, resources.GetString("LTPClipboardCopyHeightTypeButton03.ToolTip"));
			this.LTPClipboardCopyHeightTypeButton03.UseMnemonic = false;
			this.LTPClipboardCopyHeightTypeButton03.UseVisualStyleBackColor = true;
			this.LTPClipboardCopyHeightTypeButton03.CheckedChanged += new global::System.EventHandler(this.LTPClipboardCopyHeightTypeButton03_CheckedChanged);
			resources.ApplyResources(this.LTPClipboardCopyHeightTypeButton02, "LTPClipboardCopyHeightTypeButton02");
			this.LTPClipboardCopyHeightTypeButton02.ImageList = this.LandscapeCopyHeightTypeImageList;
			this.LTPClipboardCopyHeightTypeButton02.Name = "LTPClipboardCopyHeightTypeButton02";
			this.MainToolTip.SetToolTip(this.LTPClipboardCopyHeightTypeButton02, resources.GetString("LTPClipboardCopyHeightTypeButton02.ToolTip"));
			this.LTPClipboardCopyHeightTypeButton02.UseMnemonic = false;
			this.LTPClipboardCopyHeightTypeButton02.UseVisualStyleBackColor = true;
			this.LTPClipboardCopyHeightTypeButton02.CheckedChanged += new global::System.EventHandler(this.LTPClipboardCopyHeightTypeButton02_CheckedChanged);
			resources.ApplyResources(this.LTPClipboardCopyHeightTypeButton01, "LTPClipboardCopyHeightTypeButton01");
			this.LTPClipboardCopyHeightTypeButton01.ImageList = this.LandscapeCopyHeightTypeImageList;
			this.LTPClipboardCopyHeightTypeButton01.Name = "LTPClipboardCopyHeightTypeButton01";
			this.MainToolTip.SetToolTip(this.LTPClipboardCopyHeightTypeButton01, resources.GetString("LTPClipboardCopyHeightTypeButton01.ToolTip"));
			this.LTPClipboardCopyHeightTypeButton01.UseMnemonic = false;
			this.LTPClipboardCopyHeightTypeButton01.UseVisualStyleBackColor = true;
			this.LTPClipboardCopyHeightTypeButton01.CheckedChanged += new global::System.EventHandler(this.LTPClipboardCopyHeightTypeButton01_CheckedChanged);
			resources.ApplyResources(this.LTPClipboardCopyHeightTypeButton00, "LTPClipboardCopyHeightTypeButton00");
			this.LTPClipboardCopyHeightTypeButton00.ImageList = this.LandscapeCopyHeightTypeImageList;
			this.LTPClipboardCopyHeightTypeButton00.Name = "LTPClipboardCopyHeightTypeButton00";
			this.MainToolTip.SetToolTip(this.LTPClipboardCopyHeightTypeButton00, resources.GetString("LTPClipboardCopyHeightTypeButton00.ToolTip"));
			this.LTPClipboardCopyHeightTypeButton00.UseMnemonic = false;
			this.LTPClipboardCopyHeightTypeButton00.UseVisualStyleBackColor = true;
			this.LTPClipboardCopyHeightTypeButton00.CheckedChanged += new global::System.EventHandler(this.LTPClipboardCopyHeightTypeButton00_CheckedChanged);
			resources.ApplyResources(this.LTPClipboardFlipHorisontalCheckBox, "LTPClipboardFlipHorisontalCheckBox");
			this.LTPClipboardFlipHorisontalCheckBox.Name = "LTPClipboardFlipHorisontalCheckBox";
			this.LTPClipboardFlipHorisontalCheckBox.UseVisualStyleBackColor = true;
			this.LTPClipboardFlipHorisontalCheckBox.CheckedChanged += new global::System.EventHandler(this.LTPClipboardFlipHorisontalCheckBox_CheckedChanged);
			resources.ApplyResources(this.LTPClipboardFlipVerticalCheckBox, "LTPClipboardFlipVerticalCheckBox");
			this.LTPClipboardFlipVerticalCheckBox.Name = "LTPClipboardFlipVerticalCheckBox";
			this.LTPClipboardFlipVerticalCheckBox.UseVisualStyleBackColor = true;
			this.LTPClipboardFlipVerticalCheckBox.CheckedChanged += new global::System.EventHandler(this.LTPClipboardFlipVerticalCheckBox_CheckedChanged);
			resources.ApplyResources(this.LTPClipboardUpdateObjectsTextBox, "LTPClipboardUpdateObjectsTextBox");
			this.LTPClipboardUpdateObjectsTextBox.Name = "LTPClipboardUpdateObjectsTextBox";
			this.MainToolTip.SetToolTip(this.LTPClipboardUpdateObjectsTextBox, resources.GetString("LTPClipboardUpdateObjectsTextBox.ToolTip"));
			this.LTPClipboardUpdateObjectsTextBox.TextChanged += new global::System.EventHandler(this.LTPClipboardUpdateObjectsTextBox_TextChanged);
			resources.ApplyResources(this.LTPClipboardUpdateObjectsCheckBox, "LTPClipboardUpdateObjectsCheckBox");
			this.LTPClipboardUpdateObjectsCheckBox.Name = "LTPClipboardUpdateObjectsCheckBox";
			this.LTPClipboardUpdateObjectsCheckBox.UseVisualStyleBackColor = true;
			this.LTPClipboardUpdateObjectsCheckBox.CheckedChanged += new global::System.EventHandler(this.LTPClipboardUpdateObjectsCheckBox_CheckedChanged);
			resources.ApplyResources(this.LTPClipboardCopyType, "LTPClipboardCopyType");
			this.LTPClipboardCopyType.Name = "LTPClipboardCopyType";
			resources.ApplyResources(this.LTPClipboardPreciseToolCheckBox, "LTPClipboardPreciseToolCheckBox");
			this.LTPClipboardPreciseToolCheckBox.Name = "LTPClipboardPreciseToolCheckBox";
			this.LTPClipboardPreciseToolCheckBox.UseVisualStyleBackColor = true;
			this.LTPClipboardPreciseToolCheckBox.CheckedChanged += new global::System.EventHandler(this.LTPClipboardPreciseToolCheckBox_CheckedChanged);
			resources.ApplyResources(this.LTPClipboardCopyTypeButton00, "LTPClipboardCopyTypeButton00");
			this.LTPClipboardCopyTypeButton00.ImageList = this.LandscapeCopyTypeImageList;
			this.LTPClipboardCopyTypeButton00.Name = "LTPClipboardCopyTypeButton00";
			this.MainToolTip.SetToolTip(this.LTPClipboardCopyTypeButton00, resources.GetString("LTPClipboardCopyTypeButton00.ToolTip"));
			this.LTPClipboardCopyTypeButton00.UseVisualStyleBackColor = true;
			this.LTPClipboardCopyTypeButton00.CheckedChanged += new global::System.EventHandler(this.LTPClipboardCopyTypeButton00_CheckedChanged);
			resources.ApplyResources(this.LTPClipboardCopyTypeButton01, "LTPClipboardCopyTypeButton01");
			this.LTPClipboardCopyTypeButton01.ImageList = this.LandscapeCopyTypeImageList;
			this.LTPClipboardCopyTypeButton01.Name = "LTPClipboardCopyTypeButton01";
			this.MainToolTip.SetToolTip(this.LTPClipboardCopyTypeButton01, resources.GetString("LTPClipboardCopyTypeButton01.ToolTip"));
			this.LTPClipboardCopyTypeButton01.UseVisualStyleBackColor = true;
			this.LTPClipboardCopyTypeButton01.CheckedChanged += new global::System.EventHandler(this.LTPClipboardCopyTypeButton01_CheckedChanged);
			resources.ApplyResources(this.LTPClipboardResetStrengthSmoothButton, "LTPClipboardResetStrengthSmoothButton");
			this.LTPClipboardResetStrengthSmoothButton.Name = "LTPClipboardResetStrengthSmoothButton";
			this.MainToolTip.SetToolTip(this.LTPClipboardResetStrengthSmoothButton, resources.GetString("LTPClipboardResetStrengthSmoothButton.ToolTip"));
			this.LTPClipboardResetStrengthSmoothButton.UseVisualStyleBackColor = true;
			this.LTPClipboardResetStrengthSmoothButton.Click += new global::System.EventHandler(this.LTPClipboardResetStrengthSmoothButton_Click);
			resources.ApplyResources(this.LTPClipboardStrengthSmoothTextBox, "LTPClipboardStrengthSmoothTextBox");
			this.LTPClipboardStrengthSmoothTextBox.Name = "LTPClipboardStrengthSmoothTextBox";
			this.MainToolTip.SetToolTip(this.LTPClipboardStrengthSmoothTextBox, resources.GetString("LTPClipboardStrengthSmoothTextBox.ToolTip"));
			this.LTPClipboardStrengthSmoothTextBox.TextChanged += new global::System.EventHandler(this.LTPClipboardStrengthSmoothTextBox_TextChanged);
			resources.ApplyResources(this.LTPClipboardStrengthSmoothPictureBox, "LTPClipboardStrengthSmoothPictureBox");
			this.LTPClipboardStrengthSmoothPictureBox.BorderStyle = global::System.Windows.Forms.BorderStyle.Fixed3D;
			this.LTPClipboardStrengthSmoothPictureBox.Name = "LTPClipboardStrengthSmoothPictureBox";
			this.LTPClipboardStrengthSmoothPictureBox.TabStop = false;
			this.MainToolTip.SetToolTip(this.LTPClipboardStrengthSmoothPictureBox, resources.GetString("LTPClipboardStrengthSmoothPictureBox.ToolTip"));
			this.LTPClipboardStrengthSmoothPictureBox.Resize += new global::System.EventHandler(this.LTPClipboardStrengthSmoothPictureBox_Resize);
			this.LTPClipboardStrengthSmoothPictureBox.Paint += new global::System.Windows.Forms.PaintEventHandler(this.LTPClipboardStrengthSmoothPictureBox_Paint);
			resources.ApplyResources(this.LTPClipboardStrengthTrackBar, "LTPClipboardStrengthTrackBar");
			this.LTPClipboardStrengthTrackBar.LargeChange = 10;
			this.LTPClipboardStrengthTrackBar.Maximum = 100;
			this.LTPClipboardStrengthTrackBar.Name = "LTPClipboardStrengthTrackBar";
			this.LTPClipboardStrengthTrackBar.TickFrequency = 10;
			this.MainToolTip.SetToolTip(this.LTPClipboardStrengthTrackBar, resources.GetString("LTPClipboardStrengthTrackBar.ToolTip"));
			this.LTPClipboardStrengthTrackBar.Value = 10;
			this.LTPClipboardStrengthTrackBar.Scroll += new global::System.EventHandler(this.LTPClipboardStrengthTrackBar_Scroll);
			resources.ApplyResources(this.LTPClipboardSmoothTrackBar, "LTPClipboardSmoothTrackBar");
			this.LTPClipboardSmoothTrackBar.LargeChange = 10;
			this.LTPClipboardSmoothTrackBar.Maximum = 100;
			this.LTPClipboardSmoothTrackBar.Name = "LTPClipboardSmoothTrackBar";
			this.LTPClipboardSmoothTrackBar.TickFrequency = 10;
			this.MainToolTip.SetToolTip(this.LTPClipboardSmoothTrackBar, resources.GetString("LTPClipboardSmoothTrackBar.ToolTip"));
			this.LTPClipboardSmoothTrackBar.Value = 10;
			this.LTPClipboardSmoothTrackBar.Scroll += new global::System.EventHandler(this.LTPClipboardSmoothTrackBar_Scroll);
			resources.ApplyResources(this.LTPClipboardStrengthSmoothLabel, "LTPClipboardStrengthSmoothLabel");
			this.LTPClipboardStrengthSmoothLabel.Name = "LTPClipboardStrengthSmoothLabel";
			this.LandscapeTabPageTilePicker.Controls.Add(this.LTPTilePickerPanel);
			this.LandscapeTabPageTilePicker.Controls.Add(this.LTPTilePickerLabel03);
			this.LandscapeTabPageTilePicker.Controls.Add(this.LTPTilePickerLabel02);
			this.LandscapeTabPageTilePicker.Controls.Add(this.LTPTilePickerLabel00);
			this.LandscapeTabPageTilePicker.Controls.Add(this.LTPTilePickerLabel01);
			this.LandscapeTabPageTilePicker.Controls.Add(this.LTPTilePickerPictureBox03);
			this.LandscapeTabPageTilePicker.Controls.Add(this.LTPTilePickerPictureBox02);
			this.LandscapeTabPageTilePicker.Controls.Add(this.LTPTilePickerPictureBox01);
			this.LandscapeTabPageTilePicker.Controls.Add(this.LTPTilePickerPictureBox00);
			resources.ApplyResources(this.LandscapeTabPageTilePicker, "LandscapeTabPageTilePicker");
			this.LandscapeTabPageTilePicker.Name = "LandscapeTabPageTilePicker";
			this.LandscapeTabPageTilePicker.UseVisualStyleBackColor = true;
			this.LTPTilePickerPanel.Controls.Add(this.LTPTilePickerRadioButton03);
			this.LTPTilePickerPanel.Controls.Add(this.LTPTilePickerRadioButton02);
			this.LTPTilePickerPanel.Controls.Add(this.LTPTilePickerRadioButton01);
			this.LTPTilePickerPanel.Controls.Add(this.LTPTilePickerRadioButton00);
			resources.ApplyResources(this.LTPTilePickerPanel, "LTPTilePickerPanel");
			this.LTPTilePickerPanel.Name = "LTPTilePickerPanel";
			resources.ApplyResources(this.LTPTilePickerRadioButton03, "LTPTilePickerRadioButton03");
			this.LTPTilePickerRadioButton03.Name = "LTPTilePickerRadioButton03";
			this.LTPTilePickerRadioButton03.UseVisualStyleBackColor = true;
			this.LTPTilePickerRadioButton03.CheckedChanged += new global::System.EventHandler(this.LTPTilePickerRadioButton03_CheckedChanged);
			resources.ApplyResources(this.LTPTilePickerRadioButton02, "LTPTilePickerRadioButton02");
			this.LTPTilePickerRadioButton02.Name = "LTPTilePickerRadioButton02";
			this.LTPTilePickerRadioButton02.UseVisualStyleBackColor = true;
			this.LTPTilePickerRadioButton02.CheckedChanged += new global::System.EventHandler(this.LTPTilePickerRadioButton02_CheckedChanged);
			resources.ApplyResources(this.LTPTilePickerRadioButton01, "LTPTilePickerRadioButton01");
			this.LTPTilePickerRadioButton01.Name = "LTPTilePickerRadioButton01";
			this.LTPTilePickerRadioButton01.UseVisualStyleBackColor = true;
			this.LTPTilePickerRadioButton01.CheckedChanged += new global::System.EventHandler(this.LTPTilePickerRadioButton01_CheckedChanged);
			resources.ApplyResources(this.LTPTilePickerRadioButton00, "LTPTilePickerRadioButton00");
			this.LTPTilePickerRadioButton00.Checked = true;
			this.LTPTilePickerRadioButton00.Name = "LTPTilePickerRadioButton00";
			this.LTPTilePickerRadioButton00.TabStop = true;
			this.LTPTilePickerRadioButton00.UseVisualStyleBackColor = true;
			this.LTPTilePickerRadioButton00.CheckedChanged += new global::System.EventHandler(this.LTPTilePickerRadioButton00_CheckedChanged);
			resources.ApplyResources(this.LTPTilePickerLabel03, "LTPTilePickerLabel03");
			this.LTPTilePickerLabel03.Name = "LTPTilePickerLabel03";
			resources.ApplyResources(this.LTPTilePickerLabel02, "LTPTilePickerLabel02");
			this.LTPTilePickerLabel02.Name = "LTPTilePickerLabel02";
			resources.ApplyResources(this.LTPTilePickerLabel00, "LTPTilePickerLabel00");
			this.LTPTilePickerLabel00.Name = "LTPTilePickerLabel00";
			resources.ApplyResources(this.LTPTilePickerLabel01, "LTPTilePickerLabel01");
			this.LTPTilePickerLabel01.Name = "LTPTilePickerLabel01";
			resources.ApplyResources(this.LTPTilePickerPictureBox03, "LTPTilePickerPictureBox03");
			this.LTPTilePickerPictureBox03.Name = "LTPTilePickerPictureBox03";
			this.LTPTilePickerPictureBox03.TabStop = false;
			resources.ApplyResources(this.LTPTilePickerPictureBox02, "LTPTilePickerPictureBox02");
			this.LTPTilePickerPictureBox02.Name = "LTPTilePickerPictureBox02";
			this.LTPTilePickerPictureBox02.TabStop = false;
			resources.ApplyResources(this.LTPTilePickerPictureBox01, "LTPTilePickerPictureBox01");
			this.LTPTilePickerPictureBox01.Name = "LTPTilePickerPictureBox01";
			this.LTPTilePickerPictureBox01.TabStop = false;
			resources.ApplyResources(this.LTPTilePickerPictureBox00, "LTPTilePickerPictureBox00");
			this.LTPTilePickerPictureBox00.Name = "LTPTilePickerPictureBox00";
			this.LTPTilePickerPictureBox00.TabStop = false;
			resources.ApplyResources(this.LandscapeTabPageHeightPicker, "LandscapeTabPageHeightPicker");
			this.LandscapeTabPageHeightPicker.Name = "LandscapeTabPageHeightPicker";
			this.LandscapeTabPageHeightPicker.UseVisualStyleBackColor = true;
			this.LandscapeTabPageHill.Controls.Add(this.LTPHillSaveButton);
			this.LandscapeTabPageHill.Controls.Add(this.LTPHillTwoSidedCheckBox);
			this.LandscapeTabPageHill.Controls.Add(this.LTPHillParamTabControl);
			this.LandscapeTabPageHill.Controls.Add(this.LTPHillApplyButton);
			this.LandscapeTabPageHill.Controls.Add(this.LTPHillUndoButton);
			this.LandscapeTabPageHill.Controls.Add(this.LTPHillSetDefaultButton);
			this.LandscapeTabPageHill.Controls.Add(this.LTPHillMakeButton);
			resources.ApplyResources(this.LandscapeTabPageHill, "LandscapeTabPageHill");
			this.LandscapeTabPageHill.Name = "LandscapeTabPageHill";
			this.LandscapeTabPageHill.UseVisualStyleBackColor = true;
			resources.ApplyResources(this.LTPHillSaveButton, "LTPHillSaveButton");
			this.LTPHillSaveButton.Name = "LTPHillSaveButton";
			this.LTPHillSaveButton.UseVisualStyleBackColor = true;
			this.LTPHillSaveButton.Click += new global::System.EventHandler(this.LTPHillSaveButton_Click);
			resources.ApplyResources(this.LTPHillTwoSidedCheckBox, "LTPHillTwoSidedCheckBox");
			this.LTPHillTwoSidedCheckBox.Name = "LTPHillTwoSidedCheckBox";
			this.LTPHillTwoSidedCheckBox.UseVisualStyleBackColor = true;
			this.LTPHillTwoSidedCheckBox.CheckedChanged += new global::System.EventHandler(this.LTPHillTwoSidedCheckBox_CheckedChanged);
			resources.ApplyResources(this.LTPHillParamTabControl, "LTPHillParamTabControl");
			this.LTPHillParamTabControl.Controls.Add(this.HillTabPage0);
			this.LTPHillParamTabControl.Controls.Add(this.HillTabPage1);
			this.LTPHillParamTabControl.Controls.Add(this.HillTabPage2);
			this.LTPHillParamTabControl.Name = "LTPHillParamTabControl";
			this.LTPHillParamTabControl.SelectedIndex = 0;
			this.HillTabPage0.Controls.Add(this.LTPHillPresetButton);
			this.HillTabPage0.Controls.Add(this.LTPHillPresetComboBox);
			this.HillTabPage0.Controls.Add(this.LTPHillPresetListView);
			this.HillTabPage0.Controls.Add(this.LTPHillPresetLabel);
			resources.ApplyResources(this.HillTabPage0, "HillTabPage0");
			this.HillTabPage0.Name = "HillTabPage0";
			this.HillTabPage0.UseVisualStyleBackColor = true;
			resources.ApplyResources(this.LTPHillPresetButton, "LTPHillPresetButton");
			this.LTPHillPresetButton.Name = "LTPHillPresetButton";
			this.LTPHillPresetButton.UseVisualStyleBackColor = true;
			this.LTPHillPresetButton.Click += new global::System.EventHandler(this.LTPHillPresetButton_Click);
			resources.ApplyResources(this.LTPHillPresetComboBox, "LTPHillPresetComboBox");
			this.LTPHillPresetComboBox.DropDownStyle = global::System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.LTPHillPresetComboBox.FormattingEnabled = true;
			this.LTPHillPresetComboBox.Name = "LTPHillPresetComboBox";
			this.MainToolTip.SetToolTip(this.LTPHillPresetComboBox, resources.GetString("LTPHillPresetComboBox.ToolTip"));
			resources.ApplyResources(this.LTPHillPresetListView, "LTPHillPresetListView");
			this.LTPHillPresetListView.HideSelection = false;
			this.LTPHillPresetListView.Name = "LTPHillPresetListView";
			this.LTPHillPresetListView.ShowItemToolTips = true;
			this.LTPHillPresetListView.Sorting = global::System.Windows.Forms.SortOrder.Ascending;
			this.LTPHillPresetListView.UseCompatibleStateImageBehavior = false;
			this.LTPHillPresetListView.View = global::System.Windows.Forms.View.List;
			resources.ApplyResources(this.LTPHillPresetLabel, "LTPHillPresetLabel");
			this.LTPHillPresetLabel.Name = "LTPHillPresetLabel";
			this.MainToolTip.SetToolTip(this.LTPHillPresetLabel, resources.GetString("LTPHillPresetLabel.ToolTip"));
			this.HillTabPage1.Controls.Add(this.LTPHillPropertyGrid00);
			resources.ApplyResources(this.HillTabPage1, "HillTabPage1");
			this.HillTabPage1.Name = "HillTabPage1";
			this.HillTabPage1.UseVisualStyleBackColor = true;
			resources.ApplyResources(this.LTPHillPropertyGrid00, "LTPHillPropertyGrid00");
			this.LTPHillPropertyGrid00.Name = "LTPHillPropertyGrid00";
			this.HillTabPage2.Controls.Add(this.LTPHillPropertyGrid01);
			resources.ApplyResources(this.HillTabPage2, "HillTabPage2");
			this.HillTabPage2.Name = "HillTabPage2";
			this.HillTabPage2.UseVisualStyleBackColor = true;
			resources.ApplyResources(this.LTPHillPropertyGrid01, "LTPHillPropertyGrid01");
			this.LTPHillPropertyGrid01.Name = "LTPHillPropertyGrid01";
			resources.ApplyResources(this.LTPHillApplyButton, "LTPHillApplyButton");
			this.LTPHillApplyButton.Name = "LTPHillApplyButton";
			this.LTPHillApplyButton.UseVisualStyleBackColor = true;
			this.LTPHillApplyButton.Click += new global::System.EventHandler(this.LTPHillApplyButton_Click);
			resources.ApplyResources(this.LTPHillUndoButton, "LTPHillUndoButton");
			this.LTPHillUndoButton.Name = "LTPHillUndoButton";
			this.LTPHillUndoButton.UseVisualStyleBackColor = true;
			this.LTPHillUndoButton.Click += new global::System.EventHandler(this.LTPHillUndoButton_Click);
			resources.ApplyResources(this.LTPHillSetDefaultButton, "LTPHillSetDefaultButton");
			this.LTPHillSetDefaultButton.Name = "LTPHillSetDefaultButton";
			this.LTPHillSetDefaultButton.UseVisualStyleBackColor = true;
			this.LTPHillSetDefaultButton.Click += new global::System.EventHandler(this.LTPHillSetDefaultButton_Click);
			resources.ApplyResources(this.LTPHillMakeButton, "LTPHillMakeButton");
			this.LTPHillMakeButton.Name = "LTPHillMakeButton";
			this.LTPHillMakeButton.UseVisualStyleBackColor = true;
			this.LTPHillMakeButton.Click += new global::System.EventHandler(this.LTPHillMakeButton_Click);
			this.LandscapeTabPageRoad.Controls.Add(this.LTPRoadSaveButton);
			this.LandscapeTabPageRoad.Controls.Add(this.LTPRoadTabControl);
			this.LandscapeTabPageRoad.Controls.Add(this.LTPRoadApplyButton);
			this.LandscapeTabPageRoad.Controls.Add(this.LTPRoadClearButton);
			this.LandscapeTabPageRoad.Controls.Add(this.LTPRoadCreateButton);
			this.LandscapeTabPageRoad.Controls.Add(this.LTPRoadDefaultsButton);
			resources.ApplyResources(this.LandscapeTabPageRoad, "LandscapeTabPageRoad");
			this.LandscapeTabPageRoad.Name = "LandscapeTabPageRoad";
			this.LandscapeTabPageRoad.UseVisualStyleBackColor = true;
			resources.ApplyResources(this.LTPRoadSaveButton, "LTPRoadSaveButton");
			this.LTPRoadSaveButton.Name = "LTPRoadSaveButton";
			this.LTPRoadSaveButton.UseVisualStyleBackColor = true;
			this.LTPRoadSaveButton.Click += new global::System.EventHandler(this.LTPRoadSaveButton_Click);
			resources.ApplyResources(this.LTPRoadTabControl, "LTPRoadTabControl");
			this.LTPRoadTabControl.Controls.Add(this.RoadTabPage0);
			this.LTPRoadTabControl.Controls.Add(this.RoadTabPage1);
			this.LTPRoadTabControl.Name = "LTPRoadTabControl";
			this.LTPRoadTabControl.SelectedIndex = 0;
			this.RoadTabPage0.Controls.Add(this.LTPRoadPresetButton);
			this.RoadTabPage0.Controls.Add(this.LTPRoadPresetComboBox);
			this.RoadTabPage0.Controls.Add(this.LTPRoadPresetListView);
			this.RoadTabPage0.Controls.Add(this.LTPRoadPresetLabel);
			resources.ApplyResources(this.RoadTabPage0, "RoadTabPage0");
			this.RoadTabPage0.Name = "RoadTabPage0";
			this.RoadTabPage0.UseVisualStyleBackColor = true;
			resources.ApplyResources(this.LTPRoadPresetButton, "LTPRoadPresetButton");
			this.LTPRoadPresetButton.Name = "LTPRoadPresetButton";
			this.LTPRoadPresetButton.UseVisualStyleBackColor = true;
			this.LTPRoadPresetButton.Click += new global::System.EventHandler(this.LTPRoadPresetButton_Click);
			resources.ApplyResources(this.LTPRoadPresetComboBox, "LTPRoadPresetComboBox");
			this.LTPRoadPresetComboBox.DropDownStyle = global::System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.LTPRoadPresetComboBox.FormattingEnabled = true;
			this.LTPRoadPresetComboBox.Name = "LTPRoadPresetComboBox";
			this.MainToolTip.SetToolTip(this.LTPRoadPresetComboBox, resources.GetString("LTPRoadPresetComboBox.ToolTip"));
			resources.ApplyResources(this.LTPRoadPresetListView, "LTPRoadPresetListView");
			this.LTPRoadPresetListView.HideSelection = false;
			this.LTPRoadPresetListView.Name = "LTPRoadPresetListView";
			this.LTPRoadPresetListView.ShowItemToolTips = true;
			this.LTPRoadPresetListView.Sorting = global::System.Windows.Forms.SortOrder.Ascending;
			this.LTPRoadPresetListView.UseCompatibleStateImageBehavior = false;
			this.LTPRoadPresetListView.View = global::System.Windows.Forms.View.List;
			resources.ApplyResources(this.LTPRoadPresetLabel, "LTPRoadPresetLabel");
			this.LTPRoadPresetLabel.Name = "LTPRoadPresetLabel";
			this.MainToolTip.SetToolTip(this.LTPRoadPresetLabel, resources.GetString("LTPRoadPresetLabel.ToolTip"));
			this.RoadTabPage1.Controls.Add(this.LTPRoadPropertyGrid);
			resources.ApplyResources(this.RoadTabPage1, "RoadTabPage1");
			this.RoadTabPage1.Name = "RoadTabPage1";
			this.RoadTabPage1.UseVisualStyleBackColor = true;
			resources.ApplyResources(this.LTPRoadPropertyGrid, "LTPRoadPropertyGrid");
			this.LTPRoadPropertyGrid.Name = "LTPRoadPropertyGrid";
			resources.ApplyResources(this.LTPRoadApplyButton, "LTPRoadApplyButton");
			this.LTPRoadApplyButton.Name = "LTPRoadApplyButton";
			this.LTPRoadApplyButton.UseVisualStyleBackColor = true;
			this.LTPRoadApplyButton.Click += new global::System.EventHandler(this.LTPRoadApplyButton_Click);
			resources.ApplyResources(this.LTPRoadClearButton, "LTPRoadClearButton");
			this.LTPRoadClearButton.Name = "LTPRoadClearButton";
			this.LTPRoadClearButton.UseVisualStyleBackColor = true;
			this.LTPRoadClearButton.Click += new global::System.EventHandler(this.LTPRoadClearButton_Click);
			resources.ApplyResources(this.LTPRoadCreateButton, "LTPRoadCreateButton");
			this.LTPRoadCreateButton.Name = "LTPRoadCreateButton";
			this.LTPRoadCreateButton.UseVisualStyleBackColor = true;
			this.LTPRoadCreateButton.Click += new global::System.EventHandler(this.LTPRoadCreateButton_Click);
			resources.ApplyResources(this.LTPRoadDefaultsButton, "LTPRoadDefaultsButton");
			this.LTPRoadDefaultsButton.Name = "LTPRoadDefaultsButton";
			this.LTPRoadDefaultsButton.UseVisualStyleBackColor = true;
			this.LTPRoadDefaultsButton.Click += new global::System.EventHandler(this.LTPRoadDefaultsButton_Click);
			resources.ApplyResources(this.LandscapeSubstateRightPanel, "LandscapeSubstateRightPanel");
			this.LandscapeSubstateRightPanel.Controls.Add(this.LandscapeSubstateRightButton09);
			this.LandscapeSubstateRightPanel.Controls.Add(this.LandscapeSubstateRightButton08);
			this.LandscapeSubstateRightPanel.Controls.Add(this.LandscapeSubstateRightButton00);
			this.LandscapeSubstateRightPanel.Controls.Add(this.LandscapeSubstateRightButton07);
			this.LandscapeSubstateRightPanel.Controls.Add(this.LandscapeSubstateRightButton06);
			this.LandscapeSubstateRightPanel.Controls.Add(this.LandscapeSubstateRightButton05);
			this.LandscapeSubstateRightPanel.Controls.Add(this.LandscapeSubstateRightButton04);
			this.LandscapeSubstateRightPanel.Controls.Add(this.LandscapeSubstateRightButton03);
			this.LandscapeSubstateRightPanel.Controls.Add(this.LandscapeSubstateRightButton02);
			this.LandscapeSubstateRightPanel.Controls.Add(this.LandscapeSubstateRightButton01);
			this.LandscapeSubstateRightPanel.Name = "LandscapeSubstateRightPanel";
			resources.ApplyResources(this.LandscapeSubstateRightButton09, "LandscapeSubstateRightButton09");
			this.LandscapeSubstateRightButton09.ImageList = this.LandscapeToolImageList;
			this.LandscapeSubstateRightButton09.Name = "LandscapeSubstateRightButton09";
			this.MainToolTip.SetToolTip(this.LandscapeSubstateRightButton09, resources.GetString("LandscapeSubstateRightButton09.ToolTip"));
			this.LandscapeSubstateRightButton09.UseMnemonic = false;
			this.LandscapeSubstateRightButton09.UseVisualStyleBackColor = true;
			this.LandscapeToolImageList.ImageStream = (global::System.Windows.Forms.ImageListStreamer)resources.GetObject("LandscapeToolImageList.ImageStream");
			this.LandscapeToolImageList.TransparentColor = global::System.Drawing.Color.Magenta;
			this.LandscapeToolImageList.Images.SetKeyName(0, "default_landscape_tool.bmp");
			this.LandscapeToolImageList.Images.SetKeyName(1, "tile_landscape_tool.bmp");
			this.LandscapeToolImageList.Images.SetKeyName(2, "pick_tile_landscape_tool.bmp");
			this.LandscapeToolImageList.Images.SetKeyName(3, "gradient_landscape_tool.bmp");
			this.LandscapeToolImageList.Images.SetKeyName(4, "water_landscape_tool.bmp");
			this.LandscapeToolImageList.Images.SetKeyName(5, "pick_terrain_landscape_tool.bmp");
			this.LandscapeToolImageList.Images.SetKeyName(6, "up_landscape_tool.bmp");
			this.LandscapeToolImageList.Images.SetKeyName(7, "down_landscape_tool.bmp");
			this.LandscapeToolImageList.Images.SetKeyName(8, "plato_landscape_tool.bmp");
			this.LandscapeToolImageList.Images.SetKeyName(9, "plane_terrain_landscape_tool.bmp");
			this.LandscapeToolImageList.Images.SetKeyName(10, "level_to_plato_landscape_tool.bmp");
			this.LandscapeToolImageList.Images.SetKeyName(11, "level_to_plane_landscape_tool.bmp");
			this.LandscapeToolImageList.Images.SetKeyName(12, "copy_paste_landscape_tool.bmp");
			this.LandscapeToolImageList.Images.SetKeyName(13, "hill_maker_landscape_tool.bmp");
			this.LandscapeToolImageList.Images.SetKeyName(14, "road_maker_landscape_tool.bmp");
			resources.ApplyResources(this.LandscapeSubstateRightButton08, "LandscapeSubstateRightButton08");
			this.LandscapeSubstateRightButton08.ImageList = this.LandscapeToolImageList;
			this.LandscapeSubstateRightButton08.Name = "LandscapeSubstateRightButton08";
			this.MainToolTip.SetToolTip(this.LandscapeSubstateRightButton08, resources.GetString("LandscapeSubstateRightButton08.ToolTip"));
			this.LandscapeSubstateRightButton08.UseMnemonic = false;
			this.LandscapeSubstateRightButton08.UseVisualStyleBackColor = true;
			resources.ApplyResources(this.LandscapeSubstateRightButton00, "LandscapeSubstateRightButton00");
			this.LandscapeSubstateRightButton00.ImageList = this.LandscapeToolImageList;
			this.LandscapeSubstateRightButton00.Name = "LandscapeSubstateRightButton00";
			this.MainToolTip.SetToolTip(this.LandscapeSubstateRightButton00, resources.GetString("LandscapeSubstateRightButton00.ToolTip"));
			this.LandscapeSubstateRightButton00.UseMnemonic = false;
			this.LandscapeSubstateRightButton00.UseVisualStyleBackColor = true;
			resources.ApplyResources(this.LandscapeSubstateRightButton07, "LandscapeSubstateRightButton07");
			this.LandscapeSubstateRightButton07.ImageList = this.LandscapeToolImageList;
			this.LandscapeSubstateRightButton07.Name = "LandscapeSubstateRightButton07";
			this.MainToolTip.SetToolTip(this.LandscapeSubstateRightButton07, resources.GetString("LandscapeSubstateRightButton07.ToolTip"));
			this.LandscapeSubstateRightButton07.UseMnemonic = false;
			this.LandscapeSubstateRightButton07.UseVisualStyleBackColor = true;
			resources.ApplyResources(this.LandscapeSubstateRightButton06, "LandscapeSubstateRightButton06");
			this.LandscapeSubstateRightButton06.ImageList = this.LandscapeToolImageList;
			this.LandscapeSubstateRightButton06.Name = "LandscapeSubstateRightButton06";
			this.MainToolTip.SetToolTip(this.LandscapeSubstateRightButton06, resources.GetString("LandscapeSubstateRightButton06.ToolTip"));
			this.LandscapeSubstateRightButton06.UseMnemonic = false;
			this.LandscapeSubstateRightButton06.UseVisualStyleBackColor = true;
			resources.ApplyResources(this.LandscapeSubstateRightButton05, "LandscapeSubstateRightButton05");
			this.LandscapeSubstateRightButton05.ImageList = this.LandscapeToolImageList;
			this.LandscapeSubstateRightButton05.Name = "LandscapeSubstateRightButton05";
			this.MainToolTip.SetToolTip(this.LandscapeSubstateRightButton05, resources.GetString("LandscapeSubstateRightButton05.ToolTip"));
			this.LandscapeSubstateRightButton05.UseMnemonic = false;
			this.LandscapeSubstateRightButton05.UseVisualStyleBackColor = true;
			resources.ApplyResources(this.LandscapeSubstateRightButton04, "LandscapeSubstateRightButton04");
			this.LandscapeSubstateRightButton04.ImageList = this.LandscapeToolImageList;
			this.LandscapeSubstateRightButton04.Name = "LandscapeSubstateRightButton04";
			this.MainToolTip.SetToolTip(this.LandscapeSubstateRightButton04, resources.GetString("LandscapeSubstateRightButton04.ToolTip"));
			this.LandscapeSubstateRightButton04.UseMnemonic = false;
			this.LandscapeSubstateRightButton04.UseVisualStyleBackColor = true;
			resources.ApplyResources(this.LandscapeSubstateRightButton03, "LandscapeSubstateRightButton03");
			this.LandscapeSubstateRightButton03.ImageList = this.LandscapeToolImageList;
			this.LandscapeSubstateRightButton03.Name = "LandscapeSubstateRightButton03";
			this.MainToolTip.SetToolTip(this.LandscapeSubstateRightButton03, resources.GetString("LandscapeSubstateRightButton03.ToolTip"));
			this.LandscapeSubstateRightButton03.UseMnemonic = false;
			this.LandscapeSubstateRightButton03.UseVisualStyleBackColor = true;
			resources.ApplyResources(this.LandscapeSubstateRightButton02, "LandscapeSubstateRightButton02");
			this.LandscapeSubstateRightButton02.ImageList = this.LandscapeToolImageList;
			this.LandscapeSubstateRightButton02.Name = "LandscapeSubstateRightButton02";
			this.MainToolTip.SetToolTip(this.LandscapeSubstateRightButton02, resources.GetString("LandscapeSubstateRightButton02.ToolTip"));
			this.LandscapeSubstateRightButton02.UseMnemonic = false;
			this.LandscapeSubstateRightButton02.UseVisualStyleBackColor = true;
			resources.ApplyResources(this.LandscapeSubstateRightButton01, "LandscapeSubstateRightButton01");
			this.LandscapeSubstateRightButton01.ImageList = this.LandscapeToolImageList;
			this.LandscapeSubstateRightButton01.Name = "LandscapeSubstateRightButton01";
			this.MainToolTip.SetToolTip(this.LandscapeSubstateRightButton01, resources.GetString("LandscapeSubstateRightButton01.ToolTip"));
			this.LandscapeSubstateRightButton01.UseMnemonic = false;
			this.LandscapeSubstateRightButton01.UseVisualStyleBackColor = true;
			resources.ApplyResources(this.LandscapeSubstateLeftPanel, "LandscapeSubstateLeftPanel");
			this.LandscapeSubstateLeftPanel.Controls.Add(this.LandscapeSubstateLeftButton09);
			this.LandscapeSubstateLeftPanel.Controls.Add(this.LandscapeSubstateLeftButton08);
			this.LandscapeSubstateLeftPanel.Controls.Add(this.LandscapeSubstateLeftButton00);
			this.LandscapeSubstateLeftPanel.Controls.Add(this.LandscapeSubstateLeftButton07);
			this.LandscapeSubstateLeftPanel.Controls.Add(this.LandscapeSubstateLeftButton06);
			this.LandscapeSubstateLeftPanel.Controls.Add(this.LandscapeSubstateLeftButton05);
			this.LandscapeSubstateLeftPanel.Controls.Add(this.LandscapeSubstateLeftButton04);
			this.LandscapeSubstateLeftPanel.Controls.Add(this.LandscapeSubstateLeftButton03);
			this.LandscapeSubstateLeftPanel.Controls.Add(this.LandscapeSubstateLeftButton02);
			this.LandscapeSubstateLeftPanel.Controls.Add(this.LandscapeSubstateLeftButton01);
			this.LandscapeSubstateLeftPanel.Name = "LandscapeSubstateLeftPanel";
			resources.ApplyResources(this.LandscapeSubstateLeftButton09, "LandscapeSubstateLeftButton09");
			this.LandscapeSubstateLeftButton09.ImageList = this.LandscapeToolImageList;
			this.LandscapeSubstateLeftButton09.Name = "LandscapeSubstateLeftButton09";
			this.MainToolTip.SetToolTip(this.LandscapeSubstateLeftButton09, resources.GetString("LandscapeSubstateLeftButton09.ToolTip"));
			this.LandscapeSubstateLeftButton09.UseMnemonic = false;
			this.LandscapeSubstateLeftButton09.UseVisualStyleBackColor = true;
			resources.ApplyResources(this.LandscapeSubstateLeftButton08, "LandscapeSubstateLeftButton08");
			this.LandscapeSubstateLeftButton08.ImageList = this.LandscapeToolImageList;
			this.LandscapeSubstateLeftButton08.Name = "LandscapeSubstateLeftButton08";
			this.MainToolTip.SetToolTip(this.LandscapeSubstateLeftButton08, resources.GetString("LandscapeSubstateLeftButton08.ToolTip"));
			this.LandscapeSubstateLeftButton08.UseMnemonic = false;
			this.LandscapeSubstateLeftButton08.UseVisualStyleBackColor = true;
			resources.ApplyResources(this.LandscapeSubstateLeftButton00, "LandscapeSubstateLeftButton00");
			this.LandscapeSubstateLeftButton00.ImageList = this.LandscapeToolImageList;
			this.LandscapeSubstateLeftButton00.Name = "LandscapeSubstateLeftButton00";
			this.MainToolTip.SetToolTip(this.LandscapeSubstateLeftButton00, resources.GetString("LandscapeSubstateLeftButton00.ToolTip"));
			this.LandscapeSubstateLeftButton00.UseMnemonic = false;
			this.LandscapeSubstateLeftButton00.UseVisualStyleBackColor = true;
			resources.ApplyResources(this.LandscapeSubstateLeftButton07, "LandscapeSubstateLeftButton07");
			this.LandscapeSubstateLeftButton07.ImageList = this.LandscapeToolImageList;
			this.LandscapeSubstateLeftButton07.Name = "LandscapeSubstateLeftButton07";
			this.MainToolTip.SetToolTip(this.LandscapeSubstateLeftButton07, resources.GetString("LandscapeSubstateLeftButton07.ToolTip"));
			this.LandscapeSubstateLeftButton07.UseMnemonic = false;
			this.LandscapeSubstateLeftButton07.UseVisualStyleBackColor = true;
			resources.ApplyResources(this.LandscapeSubstateLeftButton06, "LandscapeSubstateLeftButton06");
			this.LandscapeSubstateLeftButton06.ImageList = this.LandscapeToolImageList;
			this.LandscapeSubstateLeftButton06.Name = "LandscapeSubstateLeftButton06";
			this.MainToolTip.SetToolTip(this.LandscapeSubstateLeftButton06, resources.GetString("LandscapeSubstateLeftButton06.ToolTip"));
			this.LandscapeSubstateLeftButton06.UseMnemonic = false;
			this.LandscapeSubstateLeftButton06.UseVisualStyleBackColor = true;
			resources.ApplyResources(this.LandscapeSubstateLeftButton05, "LandscapeSubstateLeftButton05");
			this.LandscapeSubstateLeftButton05.ImageList = this.LandscapeToolImageList;
			this.LandscapeSubstateLeftButton05.Name = "LandscapeSubstateLeftButton05";
			this.MainToolTip.SetToolTip(this.LandscapeSubstateLeftButton05, resources.GetString("LandscapeSubstateLeftButton05.ToolTip"));
			this.LandscapeSubstateLeftButton05.UseMnemonic = false;
			this.LandscapeSubstateLeftButton05.UseVisualStyleBackColor = true;
			resources.ApplyResources(this.LandscapeSubstateLeftButton04, "LandscapeSubstateLeftButton04");
			this.LandscapeSubstateLeftButton04.ImageList = this.LandscapeToolImageList;
			this.LandscapeSubstateLeftButton04.Name = "LandscapeSubstateLeftButton04";
			this.MainToolTip.SetToolTip(this.LandscapeSubstateLeftButton04, resources.GetString("LandscapeSubstateLeftButton04.ToolTip"));
			this.LandscapeSubstateLeftButton04.UseMnemonic = false;
			this.LandscapeSubstateLeftButton04.UseVisualStyleBackColor = true;
			resources.ApplyResources(this.LandscapeSubstateLeftButton03, "LandscapeSubstateLeftButton03");
			this.LandscapeSubstateLeftButton03.ImageList = this.LandscapeToolImageList;
			this.LandscapeSubstateLeftButton03.Name = "LandscapeSubstateLeftButton03";
			this.MainToolTip.SetToolTip(this.LandscapeSubstateLeftButton03, resources.GetString("LandscapeSubstateLeftButton03.ToolTip"));
			this.LandscapeSubstateLeftButton03.UseMnemonic = false;
			this.LandscapeSubstateLeftButton03.UseVisualStyleBackColor = true;
			resources.ApplyResources(this.LandscapeSubstateLeftButton02, "LandscapeSubstateLeftButton02");
			this.LandscapeSubstateLeftButton02.ImageList = this.LandscapeToolImageList;
			this.LandscapeSubstateLeftButton02.Name = "LandscapeSubstateLeftButton02";
			this.MainToolTip.SetToolTip(this.LandscapeSubstateLeftButton02, resources.GetString("LandscapeSubstateLeftButton02.ToolTip"));
			this.LandscapeSubstateLeftButton02.UseMnemonic = false;
			this.LandscapeSubstateLeftButton02.UseVisualStyleBackColor = true;
			resources.ApplyResources(this.LandscapeSubstateLeftButton01, "LandscapeSubstateLeftButton01");
			this.LandscapeSubstateLeftButton01.ImageList = this.LandscapeToolImageList;
			this.LandscapeSubstateLeftButton01.Name = "LandscapeSubstateLeftButton01";
			this.MainToolTip.SetToolTip(this.LandscapeSubstateLeftButton01, resources.GetString("LandscapeSubstateLeftButton01.ToolTip"));
			this.LandscapeSubstateLeftButton01.UseMnemonic = false;
			this.LandscapeSubstateLeftButton01.UseVisualStyleBackColor = true;
			this.ZonesTab.Controls.Add(this.zoneFilterEditorButton);
			this.ZonesTab.Controls.Add(this.ZoneComboBox);
			this.ZonesTab.Controls.Add(this.ZonesFilterLabel);
			this.ZonesTab.Controls.Add(this.ZoneBrushSizeLabel);
			this.ZonesTab.Controls.Add(this.ZoneBrushSizePanel);
			this.ZonesTab.Controls.Add(this.AddClearZonePanel);
			this.ZonesTab.Controls.Add(this.ZoneBrushSizeTextBox);
			this.ZonesTab.Controls.Add(this.ZonesListView);
			resources.ApplyResources(this.ZonesTab, "ZonesTab");
			this.ZonesTab.Name = "ZonesTab";
			this.ZonesTab.UseVisualStyleBackColor = true;
			resources.ApplyResources(this.zoneFilterEditorButton, "zoneFilterEditorButton");
			this.zoneFilterEditorButton.Name = "zoneFilterEditorButton";
			this.zoneFilterEditorButton.UseVisualStyleBackColor = true;
			this.zoneFilterEditorButton.Click += new global::System.EventHandler(this.zoneFilterEditorButton_Click);
			resources.ApplyResources(this.ZonesFilterLabel, "ZonesFilterLabel");
			this.ZonesFilterLabel.Name = "ZonesFilterLabel";
			resources.ApplyResources(this.ZoneBrushSizeLabel, "ZoneBrushSizeLabel");
			this.ZoneBrushSizeLabel.Name = "ZoneBrushSizeLabel";
			this.ZoneBrushSizePanel.Controls.Add(this.ZoneSize3Button);
			this.ZoneBrushSizePanel.Controls.Add(this.ZoneSize1Button);
			this.ZoneBrushSizePanel.Controls.Add(this.ZoneSize0Button);
			this.ZoneBrushSizePanel.Controls.Add(this.ZoneSize2Button);
			resources.ApplyResources(this.ZoneBrushSizePanel, "ZoneBrushSizePanel");
			this.ZoneBrushSizePanel.Name = "ZoneBrushSizePanel";
			resources.ApplyResources(this.ZoneSize3Button, "ZoneSize3Button");
			this.ZoneSize3Button.ImageList = this.ZoneSizeImageList;
			this.ZoneSize3Button.Name = "ZoneSize3Button";
			this.MainToolTip.SetToolTip(this.ZoneSize3Button, resources.GetString("ZoneSize3Button.ToolTip"));
			this.ZoneSize3Button.UseMnemonic = false;
			this.ZoneSize3Button.UseVisualStyleBackColor = true;
			this.ZoneSize3Button.CheckedChanged += new global::System.EventHandler(this.ZoneSize3Button_CheckedChanged);
			this.ZoneSizeImageList.ImageStream = (global::System.Windows.Forms.ImageListStreamer)resources.GetObject("ZoneSizeImageList.ImageStream");
			this.ZoneSizeImageList.TransparentColor = global::System.Drawing.Color.Magenta;
			this.ZoneSizeImageList.Images.SetKeyName(0, "size_z0.bmp");
			this.ZoneSizeImageList.Images.SetKeyName(1, "size_z1.bmp");
			this.ZoneSizeImageList.Images.SetKeyName(2, "size_z2.bmp");
			this.ZoneSizeImageList.Images.SetKeyName(3, "size_z3.bmp");
			resources.ApplyResources(this.ZoneSize1Button, "ZoneSize1Button");
			this.ZoneSize1Button.ImageList = this.ZoneSizeImageList;
			this.ZoneSize1Button.Name = "ZoneSize1Button";
			this.MainToolTip.SetToolTip(this.ZoneSize1Button, resources.GetString("ZoneSize1Button.ToolTip"));
			this.ZoneSize1Button.UseMnemonic = false;
			this.ZoneSize1Button.UseVisualStyleBackColor = true;
			this.ZoneSize1Button.CheckedChanged += new global::System.EventHandler(this.ZoneSize1Button_CheckedChanged);
			resources.ApplyResources(this.ZoneSize0Button, "ZoneSize0Button");
			this.ZoneSize0Button.Checked = true;
			this.ZoneSize0Button.ImageList = this.ZoneSizeImageList;
			this.ZoneSize0Button.Name = "ZoneSize0Button";
			this.ZoneSize0Button.TabStop = true;
			this.MainToolTip.SetToolTip(this.ZoneSize0Button, resources.GetString("ZoneSize0Button.ToolTip"));
			this.ZoneSize0Button.UseMnemonic = false;
			this.ZoneSize0Button.UseVisualStyleBackColor = true;
			this.ZoneSize0Button.CheckedChanged += new global::System.EventHandler(this.ZoneSize0Button_CheckedChanged);
			resources.ApplyResources(this.ZoneSize2Button, "ZoneSize2Button");
			this.ZoneSize2Button.ImageList = this.ZoneSizeImageList;
			this.ZoneSize2Button.Name = "ZoneSize2Button";
			this.MainToolTip.SetToolTip(this.ZoneSize2Button, resources.GetString("ZoneSize2Button.ToolTip"));
			this.ZoneSize2Button.UseMnemonic = false;
			this.ZoneSize2Button.UseVisualStyleBackColor = true;
			this.ZoneSize2Button.CheckedChanged += new global::System.EventHandler(this.ZoneSize2Button_CheckedChanged);
			resources.ApplyResources(this.AddClearZonePanel, "AddClearZonePanel");
			this.AddClearZonePanel.Controls.Add(this.AddToZoneRadioButton);
			this.AddClearZonePanel.Controls.Add(this.ClearRadioButton);
			this.AddClearZonePanel.Name = "AddClearZonePanel";
			resources.ApplyResources(this.AddToZoneRadioButton, "AddToZoneRadioButton");
			this.AddToZoneRadioButton.Checked = true;
			this.AddToZoneRadioButton.Name = "AddToZoneRadioButton";
			this.AddToZoneRadioButton.TabStop = true;
			this.AddToZoneRadioButton.UseVisualStyleBackColor = true;
			this.AddToZoneRadioButton.CheckedChanged += new global::System.EventHandler(this.AddToZoneRadioButton_CheckedChanged);
			resources.ApplyResources(this.ClearRadioButton, "ClearRadioButton");
			this.ClearRadioButton.Name = "ClearRadioButton";
			this.ClearRadioButton.UseVisualStyleBackColor = true;
			this.ClearRadioButton.CheckedChanged += new global::System.EventHandler(this.ClearRadioButton_CheckedChanged);
			resources.ApplyResources(this.ZoneBrushSizeTextBox, "ZoneBrushSizeTextBox");
			this.ZoneBrushSizeTextBox.Name = "ZoneBrushSizeTextBox";
			this.ZoneBrushSizeTextBox.ReadOnly = true;
			resources.ApplyResources(this.ZonesListView, "ZonesListView");
			this.ZonesListView.ContextMenuStrip = this.ZonesContextMenuStrip;
			this.ZonesListView.HideSelection = false;
			this.ZonesListView.Name = "ZonesListView";
			this.ZonesListView.ShowItemToolTips = true;
			this.ZonesListView.Sorting = global::System.Windows.Forms.SortOrder.Ascending;
			this.ZonesListView.UseCompatibleStateImageBehavior = false;
			this.ZonesListView.View = global::System.Windows.Forms.View.List;
			this.ZonesContextMenuStrip.Items.AddRange(new global::System.Windows.Forms.ToolStripItem[]
			{
				this.AddZoneToolStripMenuItem,
				this.RemoveZoneToolStripMenuItem
			});
			this.ZonesContextMenuStrip.Name = "ZonesContextMenuStrip";
			resources.ApplyResources(this.ZonesContextMenuStrip, "ZonesContextMenuStrip");
			this.AddZoneToolStripMenuItem.Name = "AddZoneToolStripMenuItem";
			resources.ApplyResources(this.AddZoneToolStripMenuItem, "AddZoneToolStripMenuItem");
			this.RemoveZoneToolStripMenuItem.Name = "RemoveZoneToolStripMenuItem";
			resources.ApplyResources(this.RemoveZoneToolStripMenuItem, "RemoveZoneToolStripMenuItem");
			this.LightsTab.Controls.Add(this.LightFilterEditorButton);
			this.LightsTab.Controls.Add(this.LightComboBox);
			this.LightsTab.Controls.Add(this.label7);
			this.LightsTab.Controls.Add(this.label8);
			this.LightsTab.Controls.Add(this.panel4);
			this.LightsTab.Controls.Add(this.panel5);
			this.LightsTab.Controls.Add(this.LightBrushSizeTextBox);
			this.LightsTab.Controls.Add(this.LightListView);
			resources.ApplyResources(this.LightsTab, "LightsTab");
			this.LightsTab.Name = "LightsTab";
			this.LightsTab.UseVisualStyleBackColor = true;
			resources.ApplyResources(this.LightFilterEditorButton, "LightFilterEditorButton");
			this.LightFilterEditorButton.Name = "LightFilterEditorButton";
			this.LightFilterEditorButton.UseVisualStyleBackColor = true;
			this.LightFilterEditorButton.Click += new global::System.EventHandler(this.LightFilterEditorButton_Click);
			resources.ApplyResources(this.LightComboBox, "LightComboBox");
			this.LightComboBox.DropDownStyle = global::System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.LightComboBox.FormattingEnabled = true;
			this.LightComboBox.Name = "LightComboBox";
			resources.ApplyResources(this.label7, "label7");
			this.label7.Name = "label7";
			resources.ApplyResources(this.label8, "label8");
			this.label8.Name = "label8";
			this.panel4.Controls.Add(this.LightSize3Button);
			this.panel4.Controls.Add(this.LightSize1Button);
			this.panel4.Controls.Add(this.LightSize0Button);
			this.panel4.Controls.Add(this.LightSize2Button);
			resources.ApplyResources(this.panel4, "panel4");
			this.panel4.Name = "panel4";
			resources.ApplyResources(this.LightSize3Button, "LightSize3Button");
			this.LightSize3Button.ImageList = this.ZoneSizeImageList;
			this.LightSize3Button.Name = "LightSize3Button";
			this.MainToolTip.SetToolTip(this.LightSize3Button, resources.GetString("LightSize3Button.ToolTip"));
			this.LightSize3Button.UseMnemonic = false;
			this.LightSize3Button.UseVisualStyleBackColor = true;
			this.LightSize3Button.CheckedChanged += new global::System.EventHandler(this.LightSize3Button_CheckedChanged);
			resources.ApplyResources(this.LightSize1Button, "LightSize1Button");
			this.LightSize1Button.ImageList = this.ZoneSizeImageList;
			this.LightSize1Button.Name = "LightSize1Button";
			this.MainToolTip.SetToolTip(this.LightSize1Button, resources.GetString("LightSize1Button.ToolTip"));
			this.LightSize1Button.UseMnemonic = false;
			this.LightSize1Button.UseVisualStyleBackColor = true;
			this.LightSize1Button.CheckedChanged += new global::System.EventHandler(this.LightSize1Button_CheckedChanged);
			resources.ApplyResources(this.LightSize0Button, "LightSize0Button");
			this.LightSize0Button.Checked = true;
			this.LightSize0Button.ImageList = this.ZoneSizeImageList;
			this.LightSize0Button.Name = "LightSize0Button";
			this.LightSize0Button.TabStop = true;
			this.MainToolTip.SetToolTip(this.LightSize0Button, resources.GetString("LightSize0Button.ToolTip"));
			this.LightSize0Button.UseMnemonic = false;
			this.LightSize0Button.UseVisualStyleBackColor = true;
			this.LightSize0Button.CheckedChanged += new global::System.EventHandler(this.LightSize0Button_CheckedChanged);
			resources.ApplyResources(this.LightSize2Button, "LightSize2Button");
			this.LightSize2Button.ImageList = this.ZoneSizeImageList;
			this.LightSize2Button.Name = "LightSize2Button";
			this.MainToolTip.SetToolTip(this.LightSize2Button, resources.GetString("LightSize2Button.ToolTip"));
			this.LightSize2Button.UseMnemonic = false;
			this.LightSize2Button.UseVisualStyleBackColor = true;
			this.LightSize2Button.CheckedChanged += new global::System.EventHandler(this.LightSize2Button_CheckedChanged);
			resources.ApplyResources(this.panel5, "panel5");
			this.panel5.Controls.Add(this.SetLightRadioButton);
			this.panel5.Controls.Add(this.radioButton17);
			this.panel5.Name = "panel5";
			resources.ApplyResources(this.SetLightRadioButton, "SetLightRadioButton");
			this.SetLightRadioButton.Checked = true;
			this.SetLightRadioButton.Name = "SetLightRadioButton";
			this.SetLightRadioButton.TabStop = true;
			this.SetLightRadioButton.UseVisualStyleBackColor = true;
			this.SetLightRadioButton.CheckedChanged += new global::System.EventHandler(this.SetLightRadioButton_CheckedChanged);
			resources.ApplyResources(this.radioButton17, "radioButton17");
			this.radioButton17.Name = "radioButton17";
			this.radioButton17.UseVisualStyleBackColor = true;
			this.radioButton17.CheckedChanged += new global::System.EventHandler(this.radioButton17_CheckedChanged);
			resources.ApplyResources(this.LightBrushSizeTextBox, "LightBrushSizeTextBox");
			this.LightBrushSizeTextBox.Name = "LightBrushSizeTextBox";
			this.LightBrushSizeTextBox.ReadOnly = true;
			resources.ApplyResources(this.LightListView, "LightListView");
			this.LightListView.ContextMenuStrip = this.LightContextMenuStrip;
			this.LightListView.HideSelection = false;
			this.LightListView.Name = "LightListView";
			this.LightListView.ShowItemToolTips = true;
			this.LightListView.Sorting = global::System.Windows.Forms.SortOrder.Ascending;
			this.LightListView.UseCompatibleStateImageBehavior = false;
			this.LightListView.View = global::System.Windows.Forms.View.List;
			this.LightContextMenuStrip.Items.AddRange(new global::System.Windows.Forms.ToolStripItem[]
			{
				this.AddLightToolStripMenuItem,
				this.RemoveLightToolStripMenuItem
			});
			this.LightContextMenuStrip.Name = "ZonesContextMenuStrip";
			resources.ApplyResources(this.LightContextMenuStrip, "LightContextMenuStrip");
			this.AddLightToolStripMenuItem.Name = "AddLightToolStripMenuItem";
			resources.ApplyResources(this.AddLightToolStripMenuItem, "AddLightToolStripMenuItem");
			this.RemoveLightToolStripMenuItem.Name = "RemoveLightToolStripMenuItem";
			resources.ApplyResources(this.RemoveLightToolStripMenuItem, "RemoveLightToolStripMenuItem");
			this.SoundTab.Controls.Add(this.SoundSeparator);
			this.SoundTab.Controls.Add(this.panel8);
			this.SoundTab.Controls.Add(this.SoundFilterEditorButton);
			this.SoundTab.Controls.Add(this.SoundComboBox);
			this.SoundTab.Controls.Add(this.label9);
			this.SoundTab.Controls.Add(this.label10);
			this.SoundTab.Controls.Add(this.panel6);
			this.SoundTab.Controls.Add(this.panel7);
			this.SoundTab.Controls.Add(this.SoundBrushSizeTextBox);
			this.SoundTab.Controls.Add(this.SoundListView);
			resources.ApplyResources(this.SoundTab, "SoundTab");
			this.SoundTab.Name = "SoundTab";
			this.SoundTab.UseVisualStyleBackColor = true;
			this.SoundSeparator.BorderStyle = global::System.Windows.Forms.BorderStyle.Fixed3D;
			resources.ApplyResources(this.SoundSeparator, "SoundSeparator");
			this.SoundSeparator.Name = "SoundSeparator";
			this.panel8.Controls.Add(this.MusicRadioButton);
			this.panel8.Controls.Add(this.AmbienceRadioButton);
			resources.ApplyResources(this.panel8, "panel8");
			this.panel8.Name = "panel8";
			resources.ApplyResources(this.MusicRadioButton, "MusicRadioButton");
			this.MusicRadioButton.Checked = true;
			this.MusicRadioButton.Name = "MusicRadioButton";
			this.MusicRadioButton.TabStop = true;
			this.MusicRadioButton.UseVisualStyleBackColor = true;
			this.MusicRadioButton.CheckedChanged += new global::System.EventHandler(this.MusicRadioButton_CheckedChanged);
			resources.ApplyResources(this.AmbienceRadioButton, "AmbienceRadioButton");
			this.AmbienceRadioButton.Name = "AmbienceRadioButton";
			this.AmbienceRadioButton.UseVisualStyleBackColor = true;
			this.AmbienceRadioButton.CheckedChanged += new global::System.EventHandler(this.AmbienceRadioButton_CheckedChanged);
			resources.ApplyResources(this.SoundFilterEditorButton, "SoundFilterEditorButton");
			this.SoundFilterEditorButton.Name = "SoundFilterEditorButton";
			this.SoundFilterEditorButton.UseVisualStyleBackColor = true;
			this.SoundFilterEditorButton.Click += new global::System.EventHandler(this.SoundFilterEditorButton_Click);
			resources.ApplyResources(this.SoundComboBox, "SoundComboBox");
			this.SoundComboBox.DropDownStyle = global::System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.SoundComboBox.FormattingEnabled = true;
			this.SoundComboBox.Name = "SoundComboBox";
			resources.ApplyResources(this.label9, "label9");
			this.label9.Name = "label9";
			resources.ApplyResources(this.label10, "label10");
			this.label10.Name = "label10";
			this.panel6.Controls.Add(this.SoundSize1Button);
			this.panel6.Controls.Add(this.SoundSize0Button);
			this.panel6.Controls.Add(this.SoundSize2Button);
			resources.ApplyResources(this.panel6, "panel6");
			this.panel6.Name = "panel6";
			resources.ApplyResources(this.SoundSize1Button, "SoundSize1Button");
			this.SoundSize1Button.ImageList = this.ZoneSizeImageList;
			this.SoundSize1Button.Name = "SoundSize1Button";
			this.MainToolTip.SetToolTip(this.SoundSize1Button, resources.GetString("SoundSize1Button.ToolTip"));
			this.SoundSize1Button.UseMnemonic = false;
			this.SoundSize1Button.UseVisualStyleBackColor = true;
			this.SoundSize1Button.CheckedChanged += new global::System.EventHandler(this.SoundSize1Button_CheckedChanged);
			resources.ApplyResources(this.SoundSize0Button, "SoundSize0Button");
			this.SoundSize0Button.Checked = true;
			this.SoundSize0Button.ImageList = this.ZoneSizeImageList;
			this.SoundSize0Button.Name = "SoundSize0Button";
			this.SoundSize0Button.TabStop = true;
			this.MainToolTip.SetToolTip(this.SoundSize0Button, resources.GetString("SoundSize0Button.ToolTip"));
			this.SoundSize0Button.UseMnemonic = false;
			this.SoundSize0Button.UseVisualStyleBackColor = true;
			this.SoundSize0Button.CheckedChanged += new global::System.EventHandler(this.SoundSize0Button_CheckedChanged);
			resources.ApplyResources(this.SoundSize2Button, "SoundSize2Button");
			this.SoundSize2Button.ImageList = this.ZoneSizeImageList;
			this.SoundSize2Button.Name = "SoundSize2Button";
			this.MainToolTip.SetToolTip(this.SoundSize2Button, resources.GetString("SoundSize2Button.ToolTip"));
			this.SoundSize2Button.UseMnemonic = false;
			this.SoundSize2Button.UseVisualStyleBackColor = true;
			this.SoundSize2Button.CheckedChanged += new global::System.EventHandler(this.SoundSize2Button_CheckedChanged);
			this.panel7.Controls.Add(this.SetSoundRadioButton);
			this.panel7.Controls.Add(this.ClearSoundRadioButton);
			resources.ApplyResources(this.panel7, "panel7");
			this.panel7.Name = "panel7";
			resources.ApplyResources(this.SetSoundRadioButton, "SetSoundRadioButton");
			this.SetSoundRadioButton.Checked = true;
			this.SetSoundRadioButton.Name = "SetSoundRadioButton";
			this.SetSoundRadioButton.TabStop = true;
			this.SetSoundRadioButton.UseVisualStyleBackColor = true;
			this.SetSoundRadioButton.CheckedChanged += new global::System.EventHandler(this.SetSoundRadioButton_CheckedChanged);
			resources.ApplyResources(this.ClearSoundRadioButton, "ClearSoundRadioButton");
			this.ClearSoundRadioButton.Name = "ClearSoundRadioButton";
			this.ClearSoundRadioButton.UseVisualStyleBackColor = true;
			this.ClearSoundRadioButton.CheckedChanged += new global::System.EventHandler(this.ClearSoundRadioButton_CheckedChanged);
			resources.ApplyResources(this.SoundBrushSizeTextBox, "SoundBrushSizeTextBox");
			this.SoundBrushSizeTextBox.Name = "SoundBrushSizeTextBox";
			this.SoundBrushSizeTextBox.ReadOnly = true;
			resources.ApplyResources(this.SoundListView, "SoundListView");
			this.SoundListView.ContextMenuStrip = this.SoundContextMenuStrip;
			this.SoundListView.HideSelection = false;
			this.SoundListView.Name = "SoundListView";
			this.SoundListView.ShowItemToolTips = true;
			this.SoundListView.Sorting = global::System.Windows.Forms.SortOrder.Ascending;
			this.SoundListView.UseCompatibleStateImageBehavior = false;
			this.SoundListView.View = global::System.Windows.Forms.View.List;
			this.SoundContextMenuStrip.Items.AddRange(new global::System.Windows.Forms.ToolStripItem[]
			{
				this.AddSoundToolStripMenuItem,
				this.RemoveSoundToolStripMenuItem
			});
			this.SoundContextMenuStrip.Name = "ZonesContextMenuStrip";
			resources.ApplyResources(this.SoundContextMenuStrip, "SoundContextMenuStrip");
			this.AddSoundToolStripMenuItem.Name = "AddSoundToolStripMenuItem";
			resources.ApplyResources(this.AddSoundToolStripMenuItem, "AddSoundToolStripMenuItem");
			this.RemoveSoundToolStripMenuItem.Name = "RemoveSoundToolStripMenuItem";
			resources.ApplyResources(this.RemoveSoundToolStripMenuItem, "RemoveSoundToolStripMenuItem");
			resources.ApplyResources(this.checkBox4, "checkBox4");
			this.checkBox4.Name = "checkBox4";
			this.checkBox4.UseVisualStyleBackColor = true;
			resources.ApplyResources(this.checkBox5, "checkBox5");
			this.checkBox5.Name = "checkBox5";
			this.checkBox5.UseVisualStyleBackColor = true;
			resources.ApplyResources(this.checkBox6, "checkBox6");
			this.checkBox6.Name = "checkBox6";
			this.checkBox6.UseVisualStyleBackColor = true;
			resources.ApplyResources(this.comboBox1, "comboBox1");
			this.comboBox1.DropDownStyle = global::System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboBox1.FormattingEnabled = true;
			this.comboBox1.Name = "comboBox1";
			this.panel1.Controls.Add(this.radioButton1);
			this.panel1.Controls.Add(this.radioButton2);
			resources.ApplyResources(this.panel1, "panel1");
			this.panel1.Name = "panel1";
			resources.ApplyResources(this.radioButton1, "radioButton1");
			this.radioButton1.Name = "radioButton1";
			this.radioButton1.UseMnemonic = false;
			this.radioButton1.UseVisualStyleBackColor = true;
			resources.ApplyResources(this.radioButton2, "radioButton2");
			this.radioButton2.Name = "radioButton2";
			this.radioButton2.UseMnemonic = false;
			this.radioButton2.UseVisualStyleBackColor = true;
			this.panel2.Controls.Add(this.radioButton3);
			this.panel2.Controls.Add(this.radioButton4);
			this.panel2.Controls.Add(this.radioButton5);
			this.panel2.Controls.Add(this.radioButton6);
			this.panel2.Controls.Add(this.radioButton7);
			resources.ApplyResources(this.panel2, "panel2");
			this.panel2.Name = "panel2";
			resources.ApplyResources(this.radioButton3, "radioButton3");
			this.radioButton3.Name = "radioButton3";
			this.radioButton3.UseMnemonic = false;
			this.radioButton3.UseVisualStyleBackColor = true;
			resources.ApplyResources(this.radioButton4, "radioButton4");
			this.radioButton4.Name = "radioButton4";
			this.radioButton4.UseMnemonic = false;
			this.radioButton4.UseVisualStyleBackColor = true;
			resources.ApplyResources(this.radioButton5, "radioButton5");
			this.radioButton5.Name = "radioButton5";
			this.radioButton5.UseMnemonic = false;
			this.radioButton5.UseVisualStyleBackColor = true;
			resources.ApplyResources(this.radioButton6, "radioButton6");
			this.radioButton6.Name = "radioButton6";
			this.radioButton6.UseMnemonic = false;
			this.radioButton6.UseVisualStyleBackColor = true;
			resources.ApplyResources(this.radioButton7, "radioButton7");
			this.radioButton7.Name = "radioButton7";
			this.radioButton7.UseMnemonic = false;
			this.radioButton7.UseVisualStyleBackColor = true;
			this.panel3.Controls.Add(this.radioButton8);
			this.panel3.Controls.Add(this.radioButton9);
			this.panel3.Controls.Add(this.radioButton10);
			this.panel3.Controls.Add(this.radioButton11);
			this.panel3.Controls.Add(this.radioButton12);
			resources.ApplyResources(this.panel3, "panel3");
			this.panel3.Name = "panel3";
			resources.ApplyResources(this.radioButton8, "radioButton8");
			this.radioButton8.Name = "radioButton8";
			this.radioButton8.UseMnemonic = false;
			this.radioButton8.UseVisualStyleBackColor = true;
			resources.ApplyResources(this.radioButton9, "radioButton9");
			this.radioButton9.Name = "radioButton9";
			this.radioButton9.UseMnemonic = false;
			this.radioButton9.UseVisualStyleBackColor = true;
			resources.ApplyResources(this.radioButton10, "radioButton10");
			this.radioButton10.Name = "radioButton10";
			this.radioButton10.UseMnemonic = false;
			this.radioButton10.UseVisualStyleBackColor = true;
			resources.ApplyResources(this.radioButton11, "radioButton11");
			this.radioButton11.Name = "radioButton11";
			this.radioButton11.UseMnemonic = false;
			this.radioButton11.UseVisualStyleBackColor = true;
			resources.ApplyResources(this.radioButton12, "radioButton12");
			this.radioButton12.Name = "radioButton12";
			this.radioButton12.UseMnemonic = false;
			this.radioButton12.UseVisualStyleBackColor = true;
			resources.ApplyResources(this.label1, "label1");
			this.label1.Name = "label1";
			resources.ApplyResources(this.textBox1, "textBox1");
			this.textBox1.Name = "textBox1";
			resources.ApplyResources(this.textBox2, "textBox2");
			this.textBox2.Name = "textBox2";
			resources.ApplyResources(this.checkBox7, "checkBox7");
			this.checkBox7.Name = "checkBox7";
			this.checkBox7.UseVisualStyleBackColor = true;
			resources.ApplyResources(this.label2, "label2");
			this.label2.Name = "label2";
			resources.ApplyResources(this.trackBar1, "trackBar1");
			this.trackBar1.Minimum = 1;
			this.trackBar1.Name = "trackBar1";
			this.trackBar1.Value = 1;
			resources.ApplyResources(this.listView1, "listView1");
			this.listView1.HideSelection = false;
			this.listView1.Name = "listView1";
			this.listView1.ShowItemToolTips = true;
			this.listView1.UseCompatibleStateImageBehavior = false;
			this.listView1.View = global::System.Windows.Forms.View.List;
			resources.ApplyResources(this.label3, "label3");
			this.label3.Name = "label3";
			resources.ApplyResources(this.label4, "label4");
			this.label4.Name = "label4";
			resources.ApplyResources(this.label5, "label5");
			this.label5.Name = "label5";
			resources.ApplyResources(this.label6, "label6");
			this.label6.Name = "label6";
			this.MainToolTip.ShowAlways = true;
			this.LeftLandscapeFormTimer.Interval = 20;
			this.LeftLandscapeFormTimer.Tick += new global::System.EventHandler(this.LeftLandscapeFormTimer_Tick);
			this.RightLandscapeFormTimer.Interval = 20;
			this.RightLandscapeFormTimer.Tick += new global::System.EventHandler(this.RightLandscapeFormTimer_Tick);
			this.RandomizerParamsTimer.Interval = 1000;
			this.RandomizerParamsTimer.Tick += new global::System.EventHandler(this.RandomizerParamsTimer_Tick);
			this.EditBoxTimer.Interval = 1000;
			this.EditBoxTimer.Tick += new global::System.EventHandler(this.EditBoxTimer_Tick);
			resources.ApplyResources(this, "$this");
			base.AutoScaleMode = global::System.Windows.Forms.AutoScaleMode.Font;
			base.Controls.Add(this.MainTab);
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.Name = "ToolForm";
			base.ShowInTaskbar = false;
			base.FormClosing += new global::System.Windows.Forms.FormClosingEventHandler(this.ToolForm_FormClosing);
			this.MainTab.ResumeLayout(false);
			this.ObjectsTab.ResumeLayout(false);
			this.ObjectsTab.PerformLayout();
			this.LandscapeTab.ResumeLayout(false);
			this.LandscapeTab.PerformLayout();
			this.LandscapeRegionSidePanel.ResumeLayout(false);
			this.LandscapeTerrainNumberPanel.ResumeLayout(false);
			((global::System.ComponentModel.ISupportInitialize)this.LandscapeRegionSizeTrackBar).EndInit();
			this.LandscapeRegionShapePanel.ResumeLayout(false);
			this.LandscapeTabControl.ResumeLayout(false);
			this.LandscapeTabPageTile.ResumeLayout(false);
			this.LandscapeTabPageTile.PerformLayout();
			((global::System.ComponentModel.ISupportInitialize)this.LTPTileReplaceToolImage).EndInit();
			this.LTPTileSplitContainer.Panel1.ResumeLayout(false);
			this.LTPTileSplitContainer.Panel1.PerformLayout();
			this.LTPTileSplitContainer.Panel2.ResumeLayout(false);
			this.LTPTileSplitContainer.Panel2.PerformLayout();
			this.LTPTileSplitContainer.ResumeLayout(false);
			((global::System.ComponentModel.ISupportInitialize)this.LTPTileAnglePictureBox).EndInit();
			((global::System.ComponentModel.ISupportInitialize)this.LTPTileAngle00TrackBar).EndInit();
			((global::System.ComponentModel.ISupportInitialize)this.LTPTileAngle01TrackBar).EndInit();
			((global::System.ComponentModel.ISupportInitialize)this.LTPTileStrengthSmoothPictureBox).EndInit();
			((global::System.ComponentModel.ISupportInitialize)this.LTPTileStrengthTrackBar).EndInit();
			((global::System.ComponentModel.ISupportInitialize)this.LTPTileSmoothTrackBar).EndInit();
			this.LandscapeTabPageWater.ResumeLayout(false);
			this.LandscapeTabPageWater.PerformLayout();
			this.LTPWaterWaterToolPanel.ResumeLayout(false);
			this.LandscapeTabPageHeight.ResumeLayout(false);
			this.LandscapeTabPageHeight.PerformLayout();
			this.LTPHeightSplitContainer.Panel1.ResumeLayout(false);
			this.LTPHeightSplitContainer.Panel1.PerformLayout();
			this.LTPHeightSplitContainer.Panel2.ResumeLayout(false);
			this.LTPHeightSplitContainer.Panel2.PerformLayout();
			this.LTPHeightSplitContainer.ResumeLayout(false);
			this.LTPHeightHeightToolPanel.ResumeLayout(false);
			((global::System.ComponentModel.ISupportInitialize)this.LTPHeightStrengthSmoothPictureBox).EndInit();
			((global::System.ComponentModel.ISupportInitialize)this.LTPHeightStrengthTrackBar).EndInit();
			((global::System.ComponentModel.ISupportInitialize)this.LTPHeightSmoothTrackBar).EndInit();
			this.LandscapeTabPageClipboard.ResumeLayout(false);
			this.LandscapeTabPageClipboard.PerformLayout();
			this.LTPClipboardSplitContainer.Panel1.ResumeLayout(false);
			this.LTPClipboardSplitContainer.Panel1.PerformLayout();
			this.LTPClipboardSplitContainer.Panel2.ResumeLayout(false);
			this.LTPClipboardSplitContainer.Panel2.PerformLayout();
			this.LTPClipboardSplitContainer.ResumeLayout(false);
			this.LTPClipboardCopyHeightTypePanel.ResumeLayout(false);
			((global::System.ComponentModel.ISupportInitialize)this.LTPClipboardStrengthSmoothPictureBox).EndInit();
			((global::System.ComponentModel.ISupportInitialize)this.LTPClipboardStrengthTrackBar).EndInit();
			((global::System.ComponentModel.ISupportInitialize)this.LTPClipboardSmoothTrackBar).EndInit();
			this.LandscapeTabPageTilePicker.ResumeLayout(false);
			this.LTPTilePickerPanel.ResumeLayout(false);
			this.LTPTilePickerPanel.PerformLayout();
			((global::System.ComponentModel.ISupportInitialize)this.LTPTilePickerPictureBox03).EndInit();
			((global::System.ComponentModel.ISupportInitialize)this.LTPTilePickerPictureBox02).EndInit();
			((global::System.ComponentModel.ISupportInitialize)this.LTPTilePickerPictureBox01).EndInit();
			((global::System.ComponentModel.ISupportInitialize)this.LTPTilePickerPictureBox00).EndInit();
			this.LandscapeTabPageHill.ResumeLayout(false);
			this.LandscapeTabPageHill.PerformLayout();
			this.LTPHillParamTabControl.ResumeLayout(false);
			this.HillTabPage0.ResumeLayout(false);
			this.HillTabPage0.PerformLayout();
			this.HillTabPage1.ResumeLayout(false);
			this.HillTabPage2.ResumeLayout(false);
			this.LandscapeTabPageRoad.ResumeLayout(false);
			this.LTPRoadTabControl.ResumeLayout(false);
			this.RoadTabPage0.ResumeLayout(false);
			this.RoadTabPage0.PerformLayout();
			this.RoadTabPage1.ResumeLayout(false);
			this.LandscapeSubstateRightPanel.ResumeLayout(false);
			this.LandscapeSubstateLeftPanel.ResumeLayout(false);
			this.ZonesTab.ResumeLayout(false);
			this.ZonesTab.PerformLayout();
			this.ZoneBrushSizePanel.ResumeLayout(false);
			this.AddClearZonePanel.ResumeLayout(false);
			this.AddClearZonePanel.PerformLayout();
			this.ZonesContextMenuStrip.ResumeLayout(false);
			this.LightsTab.ResumeLayout(false);
			this.LightsTab.PerformLayout();
			this.panel4.ResumeLayout(false);
			this.panel5.ResumeLayout(false);
			this.panel5.PerformLayout();
			this.LightContextMenuStrip.ResumeLayout(false);
			this.SoundTab.ResumeLayout(false);
			this.SoundTab.PerformLayout();
			this.panel8.ResumeLayout(false);
			this.panel8.PerformLayout();
			this.panel6.ResumeLayout(false);
			this.panel7.ResumeLayout(false);
			this.panel7.PerformLayout();
			this.SoundContextMenuStrip.ResumeLayout(false);
			this.panel1.ResumeLayout(false);
			this.panel1.PerformLayout();
			this.panel2.ResumeLayout(false);
			this.panel2.PerformLayout();
			this.panel3.ResumeLayout(false);
			this.panel3.PerformLayout();
			((global::System.ComponentModel.ISupportInitialize)this.trackBar1).EndInit();
			base.ResumeLayout(false);
		}

		// Token: 0x04000038 RID: 56
		private global::System.ComponentModel.IContainer components;

		// Token: 0x04000039 RID: 57
		private global::System.Windows.Forms.ImageList MainTabImageList;

		// Token: 0x0400003A RID: 58
		private global::System.Windows.Forms.TabControl MainTab;

		// Token: 0x0400003B RID: 59
		private global::System.Windows.Forms.TabPage ObjectsTab;

		// Token: 0x0400003C RID: 60
		private global::System.Windows.Forms.ListView ObjectListView;

		// Token: 0x0400003D RID: 61
		private global::System.Windows.Forms.ComboBox ObjectComboBox;

		// Token: 0x0400003E RID: 62
		private global::System.Windows.Forms.Label ObjectFilterLabel;

		// Token: 0x0400003F RID: 63
		private global::System.Windows.Forms.CheckBox checkBox4;

		// Token: 0x04000040 RID: 64
		private global::System.Windows.Forms.CheckBox checkBox5;

		// Token: 0x04000041 RID: 65
		private global::System.Windows.Forms.CheckBox checkBox6;

		// Token: 0x04000042 RID: 66
		private global::System.Windows.Forms.ComboBox comboBox1;

		// Token: 0x04000043 RID: 67
		private global::System.Windows.Forms.Panel panel1;

		// Token: 0x04000044 RID: 68
		private global::System.Windows.Forms.RadioButton radioButton1;

		// Token: 0x04000045 RID: 69
		private global::System.Windows.Forms.RadioButton radioButton2;

		// Token: 0x04000046 RID: 70
		private global::System.Windows.Forms.Panel panel2;

		// Token: 0x04000047 RID: 71
		private global::System.Windows.Forms.RadioButton radioButton3;

		// Token: 0x04000048 RID: 72
		private global::System.Windows.Forms.RadioButton radioButton4;

		// Token: 0x04000049 RID: 73
		private global::System.Windows.Forms.RadioButton radioButton5;

		// Token: 0x0400004A RID: 74
		private global::System.Windows.Forms.RadioButton radioButton6;

		// Token: 0x0400004B RID: 75
		private global::System.Windows.Forms.RadioButton radioButton7;

		// Token: 0x0400004C RID: 76
		private global::System.Windows.Forms.Panel panel3;

		// Token: 0x0400004D RID: 77
		private global::System.Windows.Forms.RadioButton radioButton8;

		// Token: 0x0400004E RID: 78
		private global::System.Windows.Forms.RadioButton radioButton9;

		// Token: 0x0400004F RID: 79
		private global::System.Windows.Forms.RadioButton radioButton10;

		// Token: 0x04000050 RID: 80
		private global::System.Windows.Forms.RadioButton radioButton11;

		// Token: 0x04000051 RID: 81
		private global::System.Windows.Forms.RadioButton radioButton12;

		// Token: 0x04000052 RID: 82
		private global::System.Windows.Forms.Label label1;

		// Token: 0x04000053 RID: 83
		private global::System.Windows.Forms.TextBox textBox1;

		// Token: 0x04000054 RID: 84
		private global::System.Windows.Forms.TextBox textBox2;

		// Token: 0x04000055 RID: 85
		private global::System.Windows.Forms.CheckBox checkBox7;

		// Token: 0x04000056 RID: 86
		private global::System.Windows.Forms.Label label2;

		// Token: 0x04000057 RID: 87
		private global::System.Windows.Forms.TrackBar trackBar1;

		// Token: 0x04000058 RID: 88
		private global::System.Windows.Forms.ListView listView1;

		// Token: 0x04000059 RID: 89
		private global::System.Windows.Forms.Label label3;

		// Token: 0x0400005A RID: 90
		private global::System.Windows.Forms.Label label4;

		// Token: 0x0400005B RID: 91
		private global::System.Windows.Forms.Label label5;

		// Token: 0x0400005C RID: 92
		private global::System.Windows.Forms.Label label6;

		// Token: 0x0400005D RID: 93
		private global::System.Windows.Forms.Button objectsFilterEditorButton;

		// Token: 0x0400005E RID: 94
		private global::System.Windows.Forms.CheckBox RandomScaleCheckBox;

		// Token: 0x0400005F RID: 95
		private global::System.Windows.Forms.CheckBox RandomPitchCheckBox;

		// Token: 0x04000060 RID: 96
		private global::System.Windows.Forms.CheckBox RandomRotationCheckBox;

		// Token: 0x04000061 RID: 97
		private global::System.Windows.Forms.TextBox RandomScaleToEditBox;

		// Token: 0x04000062 RID: 98
		private global::System.Windows.Forms.TextBox RandomScaleFromEditBox;

		// Token: 0x04000063 RID: 99
		private global::System.Windows.Forms.TextBox RandomPitchToEditBox;

		// Token: 0x04000064 RID: 100
		private global::System.Windows.Forms.TextBox RandomPitchFromEditBox;

		// Token: 0x04000065 RID: 101
		private global::System.Windows.Forms.TextBox RandomRotationToEditBox;

		// Token: 0x04000066 RID: 102
		private global::System.Windows.Forms.TextBox RandomRotationFromEditBox;

		// Token: 0x04000067 RID: 103
		private global::System.Windows.Forms.TabPage ZonesTab;

		// Token: 0x04000068 RID: 104
		private global::System.Windows.Forms.ListView ZonesListView;

		// Token: 0x04000069 RID: 105
		private global::System.Windows.Forms.RadioButton ClearRadioButton;

		// Token: 0x0400006A RID: 106
		private global::System.Windows.Forms.RadioButton AddToZoneRadioButton;

		// Token: 0x0400006B RID: 107
		private global::System.Windows.Forms.ContextMenuStrip ZonesContextMenuStrip;

		// Token: 0x0400006C RID: 108
		private global::System.Windows.Forms.ToolStripMenuItem AddZoneToolStripMenuItem;

		// Token: 0x0400006D RID: 109
		private global::System.Windows.Forms.ToolStripMenuItem RemoveZoneToolStripMenuItem;

		// Token: 0x0400006E RID: 110
		private global::System.Windows.Forms.Label RandomScaleLabel;

		// Token: 0x0400006F RID: 111
		private global::System.Windows.Forms.Label RandomPitchLabel;

		// Token: 0x04000070 RID: 112
		private global::System.Windows.Forms.Label RandomRotationLabel;

		// Token: 0x04000071 RID: 113
		private global::System.Windows.Forms.TextBox ZoneBrushSizeTextBox;

		// Token: 0x04000072 RID: 114
		private global::System.Windows.Forms.RadioButton ZoneSize2Button;

		// Token: 0x04000073 RID: 115
		private global::System.Windows.Forms.RadioButton ZoneSize1Button;

		// Token: 0x04000074 RID: 116
		private global::System.Windows.Forms.RadioButton ZoneSize0Button;

		// Token: 0x04000075 RID: 117
		private global::System.Windows.Forms.Panel ZoneBrushSizePanel;

		// Token: 0x04000076 RID: 118
		private global::System.Windows.Forms.Panel AddClearZonePanel;

		// Token: 0x04000077 RID: 119
		private global::System.Windows.Forms.Label ZoneBrushSizeLabel;

		// Token: 0x04000078 RID: 120
		private global::System.Windows.Forms.CheckBox FilterSelectionCheckBox;

		// Token: 0x04000079 RID: 121
		private global::System.Windows.Forms.ToolTip MainToolTip;

		// Token: 0x0400007A RID: 122
		private global::System.Windows.Forms.TabPage LandscapeTab;

		// Token: 0x0400007B RID: 123
		private global::System.Windows.Forms.Panel LandscapeSubstateLeftPanel;

		// Token: 0x0400007C RID: 124
		private global::System.Windows.Forms.RadioButton LandscapeSubstateLeftButton05;

		// Token: 0x0400007D RID: 125
		private global::System.Windows.Forms.RadioButton LandscapeSubstateLeftButton04;

		// Token: 0x0400007E RID: 126
		private global::System.Windows.Forms.RadioButton LandscapeSubstateLeftButton03;

		// Token: 0x0400007F RID: 127
		private global::System.Windows.Forms.RadioButton LandscapeSubstateLeftButton02;

		// Token: 0x04000080 RID: 128
		private global::System.Windows.Forms.RadioButton LandscapeSubstateLeftButton01;

		// Token: 0x04000081 RID: 129
		private global::System.Windows.Forms.Label SelectionFilterLabel;

		// Token: 0x04000082 RID: 130
		private global::System.Windows.Forms.Timer LeftLandscapeFormTimer;

		// Token: 0x04000083 RID: 131
		private global::System.Windows.Forms.RadioButton LandscapeSubstateLeftButton06;

		// Token: 0x04000084 RID: 132
		private global::System.Windows.Forms.Panel LandscapeSubstateRightPanel;

		// Token: 0x04000085 RID: 133
		private global::System.Windows.Forms.RadioButton LandscapeSubstateRightButton06;

		// Token: 0x04000086 RID: 134
		private global::System.Windows.Forms.RadioButton LandscapeSubstateRightButton05;

		// Token: 0x04000087 RID: 135
		private global::System.Windows.Forms.RadioButton LandscapeSubstateRightButton03;

		// Token: 0x04000088 RID: 136
		private global::System.Windows.Forms.RadioButton LandscapeSubstateRightButton02;

		// Token: 0x04000089 RID: 137
		private global::System.Windows.Forms.RadioButton LandscapeSubstateRightButton01;

		// Token: 0x0400008A RID: 138
		private global::System.Windows.Forms.TextBox LandscapeRegionSizeEditBox;

		// Token: 0x0400008B RID: 139
		private global::System.Windows.Forms.Label LandscapeRegionSizeLabel;

		// Token: 0x0400008C RID: 140
		private global::System.Windows.Forms.Panel LandscapeRegionShapePanel;

		// Token: 0x0400008D RID: 141
		private global::System.Windows.Forms.RadioButton LandscapeRegionShapeButton01;

		// Token: 0x0400008E RID: 142
		private global::System.Windows.Forms.RadioButton LandscapeRegionShapeButton00;

		// Token: 0x0400008F RID: 143
		private global::System.Windows.Forms.TrackBar LandscapeRegionSizeTrackBar;

		// Token: 0x04000090 RID: 144
		private global::System.Windows.Forms.Label LandscapeRegionShapeLabel;

		// Token: 0x04000091 RID: 145
		private global::System.Windows.Forms.RadioButton LandscapeRegionShapeButton02;

		// Token: 0x04000092 RID: 146
		private global::System.Windows.Forms.RadioButton LandscapeSubstateRightButton07;

		// Token: 0x04000093 RID: 147
		private global::System.Windows.Forms.RadioButton LandscapeSubstateLeftButton07;

		// Token: 0x04000094 RID: 148
		private global::System.Windows.Forms.RadioButton LandscapeSubstateRightButton00;

		// Token: 0x04000095 RID: 149
		private global::System.Windows.Forms.RadioButton LandscapeSubstateLeftButton00;

		// Token: 0x04000096 RID: 150
		private global::System.Windows.Forms.ImageList LandscapeRegionShapeImageList;

		// Token: 0x04000097 RID: 151
		private global::System.Windows.Forms.ImageList LandscapeToolImageList;

		// Token: 0x04000098 RID: 152
		private global::System.Windows.Forms.CheckBox LandscapeInvertHeightToolsCheckBox;

		// Token: 0x04000099 RID: 153
		private global::System.Windows.Forms.ImageList LandscapeTerrainNumberImageList;

		// Token: 0x0400009A RID: 154
		private global::System.Windows.Forms.ImageList LandscapeHeightToolImageList;

		// Token: 0x0400009B RID: 155
		private global::System.Windows.Forms.Label LandscapeTerrainNumberLabel;

		// Token: 0x0400009C RID: 156
		private global::System.Windows.Forms.Panel LandscapeTerrainNumberPanel;

		// Token: 0x0400009D RID: 157
		private global::System.Windows.Forms.RadioButton LandscapeTerrainNumberButton01;

		// Token: 0x0400009E RID: 158
		private global::System.Windows.Forms.RadioButton LandscapeTerrainNumberButton00;

		// Token: 0x0400009F RID: 159
		private global::System.Windows.Forms.RadioButton LandscapeSubstateRightButton08;

		// Token: 0x040000A0 RID: 160
		private global::System.Windows.Forms.RadioButton LandscapeSubstateLeftButton08;

		// Token: 0x040000A1 RID: 161
		private global::System.Windows.Forms.ImageList LandscapeCopyTypeImageList;

		// Token: 0x040000A2 RID: 162
		private global::System.Windows.Forms.ImageList LandscapeCopyHeightTypeImageList;

		// Token: 0x040000A3 RID: 163
		private global::System.Windows.Forms.RadioButton LandscapeSubstateRightButton04;

		// Token: 0x040000A4 RID: 164
		private global::System.Windows.Forms.Timer RightLandscapeFormTimer;

		// Token: 0x040000A5 RID: 165
		private global::System.Windows.Forms.Button zoneFilterEditorButton;

		// Token: 0x040000A6 RID: 166
		private global::System.Windows.Forms.Label ZonesFilterLabel;

		// Token: 0x040000A7 RID: 167
		private global::System.Windows.Forms.ComboBox ZoneComboBox;

		// Token: 0x040000A8 RID: 168
		private global::System.Windows.Forms.CheckBox FilterObjectTypeCheckBox;

		// Token: 0x040000A9 RID: 169
		private global::System.Windows.Forms.ComboBox ObjectTypeComboBox;

		// Token: 0x040000AA RID: 170
		private global::System.Windows.Forms.ImageList LandscapeWaterToolImageList;

		// Token: 0x040000AB RID: 171
		private global::System.Windows.Forms.Label LandscapeToolLabel;

		// Token: 0x040000AC RID: 172
		private global::System.Windows.Forms.ImageList ZoneSizeImageList;

		// Token: 0x040000AD RID: 173
		private global::System.Windows.Forms.RadioButton LandscapeRegionShapeButton04;

		// Token: 0x040000AE RID: 174
		private global::System.Windows.Forms.RadioButton LandscapeRegionShapeButton03;

		// Token: 0x040000AF RID: 175
		private global::System.Windows.Forms.Label LandscapeRegionSizeTrackBarLabel;

		// Token: 0x040000B0 RID: 176
		private global::System.Windows.Forms.RadioButton LandscapeSubstateRightButton09;

		// Token: 0x040000B1 RID: 177
		private global::System.Windows.Forms.RadioButton LandscapeSubstateLeftButton09;

		// Token: 0x040000B2 RID: 178
		private global::System.Windows.Forms.Timer RandomizerParamsTimer;

		// Token: 0x040000B3 RID: 179
		private global::System.Windows.Forms.Timer EditBoxTimer;

		// Token: 0x040000B4 RID: 180
		private global::System.Windows.Forms.CheckBox RandomObjectCheckBox;

		// Token: 0x040000B5 RID: 181
		private global::System.Windows.Forms.ComboBox GroupComboBox;

		// Token: 0x040000B6 RID: 182
		private global::System.Windows.Forms.CheckBox GroupCheckBox;

		// Token: 0x040000B7 RID: 183
		private global::System.Windows.Forms.CheckBox ObjectRowCheckBox;

		// Token: 0x040000B8 RID: 184
		private global::System.Windows.Forms.CheckBox FlatRowCheckBox;

		// Token: 0x040000B9 RID: 185
		private global::System.Windows.Forms.CheckBox LandscapeUseSeparateRegionsCheckBox;

		// Token: 0x040000BA RID: 186
		private global::System.Windows.Forms.TabPage LightsTab;

		// Token: 0x040000BB RID: 187
		private global::System.Windows.Forms.Button LightFilterEditorButton;

		// Token: 0x040000BC RID: 188
		private global::System.Windows.Forms.ComboBox LightComboBox;

		// Token: 0x040000BD RID: 189
		private global::System.Windows.Forms.Label label7;

		// Token: 0x040000BE RID: 190
		private global::System.Windows.Forms.Label label8;

		// Token: 0x040000BF RID: 191
		private global::System.Windows.Forms.Panel panel4;

		// Token: 0x040000C0 RID: 192
		private global::System.Windows.Forms.RadioButton LightSize1Button;

		// Token: 0x040000C1 RID: 193
		private global::System.Windows.Forms.RadioButton LightSize0Button;

		// Token: 0x040000C2 RID: 194
		private global::System.Windows.Forms.RadioButton LightSize2Button;

		// Token: 0x040000C3 RID: 195
		private global::System.Windows.Forms.Panel panel5;

		// Token: 0x040000C4 RID: 196
		private global::System.Windows.Forms.RadioButton SetLightRadioButton;

		// Token: 0x040000C5 RID: 197
		private global::System.Windows.Forms.RadioButton radioButton17;

		// Token: 0x040000C6 RID: 198
		private global::System.Windows.Forms.TextBox LightBrushSizeTextBox;

		// Token: 0x040000C7 RID: 199
		private global::System.Windows.Forms.ListView LightListView;

		// Token: 0x040000C8 RID: 200
		private global::System.Windows.Forms.ContextMenuStrip LightContextMenuStrip;

		// Token: 0x040000C9 RID: 201
		private global::System.Windows.Forms.ToolStripMenuItem AddLightToolStripMenuItem;

		// Token: 0x040000CA RID: 202
		private global::System.Windows.Forms.ToolStripMenuItem RemoveLightToolStripMenuItem;

		// Token: 0x040000CB RID: 203
		private global::System.Windows.Forms.Label RandomizeLabel;

		// Token: 0x040000CC RID: 204
		private global::System.Windows.Forms.Label ObjectParamsLabel;

		// Token: 0x040000CD RID: 205
		private global::System.Windows.Forms.TextBox UnderTerrainTextBox;

		// Token: 0x040000CE RID: 206
		private global::System.Windows.Forms.TextBox OverTerrainTextBox;

		// Token: 0x040000CF RID: 207
		private global::System.Windows.Forms.CheckBox OverTerrainCheckBox;

		// Token: 0x040000D0 RID: 208
		private global::System.Windows.Forms.CheckBox UnderTerrainCheckBox;

		// Token: 0x040000D1 RID: 209
		private global::System.Windows.Forms.TabPage SoundTab;

		// Token: 0x040000D2 RID: 210
		private global::System.Windows.Forms.ContextMenuStrip SoundContextMenuStrip;

		// Token: 0x040000D3 RID: 211
		private global::System.Windows.Forms.ToolStripMenuItem AddSoundToolStripMenuItem;

		// Token: 0x040000D4 RID: 212
		private global::System.Windows.Forms.Button SoundFilterEditorButton;

		// Token: 0x040000D5 RID: 213
		private global::System.Windows.Forms.ComboBox SoundComboBox;

		// Token: 0x040000D6 RID: 214
		private global::System.Windows.Forms.Label label9;

		// Token: 0x040000D7 RID: 215
		private global::System.Windows.Forms.Label label10;

		// Token: 0x040000D8 RID: 216
		private global::System.Windows.Forms.Panel panel6;

		// Token: 0x040000D9 RID: 217
		private global::System.Windows.Forms.RadioButton SoundSize1Button;

		// Token: 0x040000DA RID: 218
		private global::System.Windows.Forms.RadioButton SoundSize0Button;

		// Token: 0x040000DB RID: 219
		private global::System.Windows.Forms.RadioButton SoundSize2Button;

		// Token: 0x040000DC RID: 220
		private global::System.Windows.Forms.Panel panel7;

		// Token: 0x040000DD RID: 221
		private global::System.Windows.Forms.RadioButton SetSoundRadioButton;

		// Token: 0x040000DE RID: 222
		private global::System.Windows.Forms.RadioButton ClearSoundRadioButton;

		// Token: 0x040000DF RID: 223
		private global::System.Windows.Forms.TextBox SoundBrushSizeTextBox;

		// Token: 0x040000E0 RID: 224
		private global::System.Windows.Forms.ListView SoundListView;

		// Token: 0x040000E1 RID: 225
		private global::System.Windows.Forms.Panel panel8;

		// Token: 0x040000E2 RID: 226
		private global::System.Windows.Forms.RadioButton MusicRadioButton;

		// Token: 0x040000E3 RID: 227
		private global::System.Windows.Forms.RadioButton AmbienceRadioButton;

		// Token: 0x040000E4 RID: 228
		private global::System.Windows.Forms.Label SoundSeparator;

		// Token: 0x040000E5 RID: 229
		private global::System.Windows.Forms.ToolStripMenuItem RemoveSoundToolStripMenuItem;

		// Token: 0x040000E6 RID: 230
		private global::System.Windows.Forms.Panel LandscapeRegionSidePanel;

		// Token: 0x040000E7 RID: 231
		private global::System.Windows.Forms.Label LandscapeRegionSideLabel;

		// Token: 0x040000E8 RID: 232
		private global::System.Windows.Forms.RadioButton LandscapeRegionSideButton02;

		// Token: 0x040000E9 RID: 233
		private global::System.Windows.Forms.RadioButton LandscapeRegionSideButton01;

		// Token: 0x040000EA RID: 234
		private global::System.Windows.Forms.RadioButton LandscapeRegionSideButton00;

		// Token: 0x040000EB RID: 235
		private global::System.Windows.Forms.ImageList LandscapeRegionSideImageList;

		// Token: 0x040000EC RID: 236
		private global::System.Windows.Forms.TabControl LandscapeTabControl;

		// Token: 0x040000ED RID: 237
		private global::System.Windows.Forms.TabPage LandscapeTabPageTile;

		// Token: 0x040000EE RID: 238
		private global::System.Windows.Forms.PictureBox LTPTileReplaceToolImage;

		// Token: 0x040000EF RID: 239
		private global::System.Windows.Forms.TextBox LTPTileReplaceToolTextBox;

		// Token: 0x040000F0 RID: 240
		private global::System.Windows.Forms.CheckBox LTPTileReplaceToolCheckBox;

		// Token: 0x040000F1 RID: 241
		private global::System.Windows.Forms.CheckBox LTPTileAutomaticToolCheckBox;

		// Token: 0x040000F2 RID: 242
		private global::System.Windows.Forms.CheckBox LTPTileSpotToolCheckBox;

		// Token: 0x040000F3 RID: 243
		private global::System.Windows.Forms.SplitContainer LTPTileSplitContainer;

		// Token: 0x040000F4 RID: 244
		private global::System.Windows.Forms.PictureBox LTPTileAnglePictureBox;

		// Token: 0x040000F5 RID: 245
		private global::System.Windows.Forms.Label LTPTileAngle01Label;

		// Token: 0x040000F6 RID: 246
		private global::System.Windows.Forms.CheckBox LTPTileUseAngleRestrictionsCheckBox;

		// Token: 0x040000F7 RID: 247
		private global::System.Windows.Forms.TextBox LTPTileAngle00TextBox;

		// Token: 0x040000F8 RID: 248
		private global::System.Windows.Forms.TrackBar LTPTileAngle00TrackBar;

		// Token: 0x040000F9 RID: 249
		private global::System.Windows.Forms.TextBox LTPTileAngle01TextBox;

		// Token: 0x040000FA RID: 250
		private global::System.Windows.Forms.TrackBar LTPTileAngle01TrackBar;

		// Token: 0x040000FB RID: 251
		private global::System.Windows.Forms.Label LTPTileAngle00Label;

		// Token: 0x040000FC RID: 252
		private global::System.Windows.Forms.Button LTPTileResetStrengthSmoothButton;

		// Token: 0x040000FD RID: 253
		private global::System.Windows.Forms.TextBox LTPTileStrengthSmoothTextBox;

		// Token: 0x040000FE RID: 254
		private global::System.Windows.Forms.PictureBox LTPTileStrengthSmoothPictureBox;

		// Token: 0x040000FF RID: 255
		private global::System.Windows.Forms.TrackBar LTPTileStrengthTrackBar;

		// Token: 0x04000100 RID: 256
		private global::System.Windows.Forms.TrackBar LTPTileSmoothTrackBar;

		// Token: 0x04000101 RID: 257
		private global::System.Windows.Forms.Label LTPTileStrengthSmoothLabel;

		// Token: 0x04000102 RID: 258
		private global::System.Windows.Forms.Button LTPTileBrushButton;

		// Token: 0x04000103 RID: 259
		private global::System.Windows.Forms.ComboBox LTPTileBrushComboBox;

		// Token: 0x04000104 RID: 260
		private global::System.Windows.Forms.ListView LTPTileBrushListView;

		// Token: 0x04000105 RID: 261
		private global::System.Windows.Forms.Label LTPTileBrushLabel;

		// Token: 0x04000106 RID: 262
		private global::System.Windows.Forms.TabPage LandscapeTabPageGradient;

		// Token: 0x04000107 RID: 263
		private global::System.Windows.Forms.TabPage LandscapeTabPageWater;

		// Token: 0x04000108 RID: 264
		private global::System.Windows.Forms.ListView LTPWaterBrushListView;

		// Token: 0x04000109 RID: 265
		private global::System.Windows.Forms.Button LTPWaterBrushButton;

		// Token: 0x0400010A RID: 266
		private global::System.Windows.Forms.ComboBox LTPWaterBrushComboBox;

		// Token: 0x0400010B RID: 267
		private global::System.Windows.Forms.Label label11;

		// Token: 0x0400010C RID: 268
		private global::System.Windows.Forms.TextBox LTPWaterHeightTextBox;

		// Token: 0x0400010D RID: 269
		private global::System.Windows.Forms.Label LTPWaterHeightLabel;

		// Token: 0x0400010E RID: 270
		private global::System.Windows.Forms.Panel LTPWaterWaterToolPanel;

		// Token: 0x0400010F RID: 271
		private global::System.Windows.Forms.RadioButton LTPWaterWaterToolButton03;

		// Token: 0x04000110 RID: 272
		private global::System.Windows.Forms.RadioButton LTPWaterWaterToolButton02;

		// Token: 0x04000111 RID: 273
		private global::System.Windows.Forms.RadioButton LTPWaterWaterToolButton01;

		// Token: 0x04000112 RID: 274
		private global::System.Windows.Forms.Label LTPWaterWaterToolLabel;

		// Token: 0x04000113 RID: 275
		private global::System.Windows.Forms.TabPage LandscapeTabPageHeight;

		// Token: 0x04000114 RID: 276
		private global::System.Windows.Forms.Button LTPHeightBrushButton;

		// Token: 0x04000115 RID: 277
		private global::System.Windows.Forms.ComboBox LTPHeightBrushComboBox;

		// Token: 0x04000116 RID: 278
		private global::System.Windows.Forms.ListView LTPHeightBrushListView;

		// Token: 0x04000117 RID: 279
		private global::System.Windows.Forms.Label LTPHeightBrushLabel;

		// Token: 0x04000118 RID: 280
		private global::System.Windows.Forms.SplitContainer LTPHeightSplitContainer;

		// Token: 0x04000119 RID: 281
		private global::System.Windows.Forms.TextBox LTPHeightHeightTextBox;

		// Token: 0x0400011A RID: 282
		private global::System.Windows.Forms.Label LTPHeightHeightLabel;

		// Token: 0x0400011B RID: 283
		private global::System.Windows.Forms.TextBox LTPHeightUpdateObjectsTextBox;

		// Token: 0x0400011C RID: 284
		private global::System.Windows.Forms.CheckBox LTPHeightUpdateObjectsCheckBox;

		// Token: 0x0400011D RID: 285
		private global::System.Windows.Forms.Panel LTPHeightHeightToolPanel;

		// Token: 0x0400011E RID: 286
		private global::System.Windows.Forms.RadioButton LTPHeightHeightToolButton05;

		// Token: 0x0400011F RID: 287
		private global::System.Windows.Forms.RadioButton LTPHeightHeightToolButton04;

		// Token: 0x04000120 RID: 288
		private global::System.Windows.Forms.RadioButton LTPHeightHeightToolButton03;

		// Token: 0x04000121 RID: 289
		private global::System.Windows.Forms.RadioButton LTPHeightHeightToolButton02;

		// Token: 0x04000122 RID: 290
		private global::System.Windows.Forms.RadioButton LTPHeightHeightToolButton01;

		// Token: 0x04000123 RID: 291
		private global::System.Windows.Forms.RadioButton LTPHeightHeightToolButton00;

		// Token: 0x04000124 RID: 292
		private global::System.Windows.Forms.Label LTPHeightHeightToolLabel;

		// Token: 0x04000125 RID: 293
		private global::System.Windows.Forms.CheckBox LTPHeightPreciseToolCheckBox;

		// Token: 0x04000126 RID: 294
		private global::System.Windows.Forms.Button LTPHeightResetStrengthSmoothButton;

		// Token: 0x04000127 RID: 295
		private global::System.Windows.Forms.TextBox LTPHeightStrengthSmoothTextBox;

		// Token: 0x04000128 RID: 296
		private global::System.Windows.Forms.PictureBox LTPHeightStrengthSmoothPictureBox;

		// Token: 0x04000129 RID: 297
		private global::System.Windows.Forms.TrackBar LTPHeightStrengthTrackBar;

		// Token: 0x0400012A RID: 298
		private global::System.Windows.Forms.TrackBar LTPHeightSmoothTrackBar;

		// Token: 0x0400012B RID: 299
		private global::System.Windows.Forms.Label LTPHeightStrengthSmoothLabel;

		// Token: 0x0400012C RID: 300
		private global::System.Windows.Forms.CheckBox LTPHeightAutomaticToolCheckBox;

		// Token: 0x0400012D RID: 301
		private global::System.Windows.Forms.TabPage LandscapeTabPageClipboard;

		// Token: 0x0400012E RID: 302
		private global::System.Windows.Forms.TextBox LTPClipboardGroupTextBox;

		// Token: 0x0400012F RID: 303
		private global::System.Windows.Forms.Label LTPClipboardGroupTextBoxLabel;

		// Token: 0x04000130 RID: 304
		private global::System.Windows.Forms.CheckBox LTPClipboardTwoSidedCheckBox;

		// Token: 0x04000131 RID: 305
		private global::System.Windows.Forms.Button LTPClipboardBrushButton;

		// Token: 0x04000132 RID: 306
		private global::System.Windows.Forms.ComboBox LTPClipboardBrushComboBox;

		// Token: 0x04000133 RID: 307
		private global::System.Windows.Forms.ListView LTPClipboardBrushListView;

		// Token: 0x04000134 RID: 308
		private global::System.Windows.Forms.Label LTPClipboardBrushLabel;

		// Token: 0x04000135 RID: 309
		private global::System.Windows.Forms.SplitContainer LTPClipboardSplitContainer;

		// Token: 0x04000136 RID: 310
		private global::System.Windows.Forms.CheckBox LTPClipboardCopyTypeButton02;

		// Token: 0x04000137 RID: 311
		private global::System.Windows.Forms.Label LTPClipboardCopyHeightTypeLabel;

		// Token: 0x04000138 RID: 312
		private global::System.Windows.Forms.Panel LTPClipboardCopyHeightTypePanel;

		// Token: 0x04000139 RID: 313
		private global::System.Windows.Forms.RadioButton LTPClipboardCopyHeightTypeButton03;

		// Token: 0x0400013A RID: 314
		private global::System.Windows.Forms.RadioButton LTPClipboardCopyHeightTypeButton02;

		// Token: 0x0400013B RID: 315
		private global::System.Windows.Forms.RadioButton LTPClipboardCopyHeightTypeButton01;

		// Token: 0x0400013C RID: 316
		private global::System.Windows.Forms.RadioButton LTPClipboardCopyHeightTypeButton00;

		// Token: 0x0400013D RID: 317
		private global::System.Windows.Forms.CheckBox LTPClipboardFlipHorisontalCheckBox;

		// Token: 0x0400013E RID: 318
		private global::System.Windows.Forms.CheckBox LTPClipboardFlipVerticalCheckBox;

		// Token: 0x0400013F RID: 319
		private global::System.Windows.Forms.TextBox LTPClipboardUpdateObjectsTextBox;

		// Token: 0x04000140 RID: 320
		private global::System.Windows.Forms.CheckBox LTPClipboardUpdateObjectsCheckBox;

		// Token: 0x04000141 RID: 321
		private global::System.Windows.Forms.Label LTPClipboardCopyType;

		// Token: 0x04000142 RID: 322
		private global::System.Windows.Forms.CheckBox LTPClipboardPreciseToolCheckBox;

		// Token: 0x04000143 RID: 323
		private global::System.Windows.Forms.CheckBox LTPClipboardCopyTypeButton00;

		// Token: 0x04000144 RID: 324
		private global::System.Windows.Forms.CheckBox LTPClipboardCopyTypeButton01;

		// Token: 0x04000145 RID: 325
		private global::System.Windows.Forms.Button LTPClipboardResetStrengthSmoothButton;

		// Token: 0x04000146 RID: 326
		private global::System.Windows.Forms.TextBox LTPClipboardStrengthSmoothTextBox;

		// Token: 0x04000147 RID: 327
		private global::System.Windows.Forms.PictureBox LTPClipboardStrengthSmoothPictureBox;

		// Token: 0x04000148 RID: 328
		private global::System.Windows.Forms.TrackBar LTPClipboardStrengthTrackBar;

		// Token: 0x04000149 RID: 329
		private global::System.Windows.Forms.TrackBar LTPClipboardSmoothTrackBar;

		// Token: 0x0400014A RID: 330
		private global::System.Windows.Forms.Label LTPClipboardStrengthSmoothLabel;

		// Token: 0x0400014B RID: 331
		private global::System.Windows.Forms.TabPage LandscapeTabPageTilePicker;

		// Token: 0x0400014C RID: 332
		private global::System.Windows.Forms.Panel LTPTilePickerPanel;

		// Token: 0x0400014D RID: 333
		private global::System.Windows.Forms.RadioButton LTPTilePickerRadioButton03;

		// Token: 0x0400014E RID: 334
		private global::System.Windows.Forms.RadioButton LTPTilePickerRadioButton02;

		// Token: 0x0400014F RID: 335
		private global::System.Windows.Forms.RadioButton LTPTilePickerRadioButton01;

		// Token: 0x04000150 RID: 336
		private global::System.Windows.Forms.RadioButton LTPTilePickerRadioButton00;

		// Token: 0x04000151 RID: 337
		private global::System.Windows.Forms.Label LTPTilePickerLabel03;

		// Token: 0x04000152 RID: 338
		private global::System.Windows.Forms.Label LTPTilePickerLabel02;

		// Token: 0x04000153 RID: 339
		private global::System.Windows.Forms.Label LTPTilePickerLabel00;

		// Token: 0x04000154 RID: 340
		private global::System.Windows.Forms.Label LTPTilePickerLabel01;

		// Token: 0x04000155 RID: 341
		private global::System.Windows.Forms.PictureBox LTPTilePickerPictureBox03;

		// Token: 0x04000156 RID: 342
		private global::System.Windows.Forms.PictureBox LTPTilePickerPictureBox02;

		// Token: 0x04000157 RID: 343
		private global::System.Windows.Forms.PictureBox LTPTilePickerPictureBox01;

		// Token: 0x04000158 RID: 344
		private global::System.Windows.Forms.PictureBox LTPTilePickerPictureBox00;

		// Token: 0x04000159 RID: 345
		private global::System.Windows.Forms.TabPage LandscapeTabPageHeightPicker;

		// Token: 0x0400015A RID: 346
		private global::System.Windows.Forms.TabPage LandscapeTabPageHill;

		// Token: 0x0400015B RID: 347
		private global::System.Windows.Forms.Button LTPHillSaveButton;

		// Token: 0x0400015C RID: 348
		private global::System.Windows.Forms.CheckBox LTPHillTwoSidedCheckBox;

		// Token: 0x0400015D RID: 349
		private global::System.Windows.Forms.TabControl LTPHillParamTabControl;

		// Token: 0x0400015E RID: 350
		private global::System.Windows.Forms.TabPage HillTabPage0;

		// Token: 0x0400015F RID: 351
		private global::System.Windows.Forms.Button LTPHillPresetButton;

		// Token: 0x04000160 RID: 352
		private global::System.Windows.Forms.ComboBox LTPHillPresetComboBox;

		// Token: 0x04000161 RID: 353
		private global::System.Windows.Forms.ListView LTPHillPresetListView;

		// Token: 0x04000162 RID: 354
		private global::System.Windows.Forms.Label LTPHillPresetLabel;

		// Token: 0x04000163 RID: 355
		private global::System.Windows.Forms.TabPage HillTabPage1;

		// Token: 0x04000164 RID: 356
		private global::System.Windows.Forms.PropertyGrid LTPHillPropertyGrid00;

		// Token: 0x04000165 RID: 357
		private global::System.Windows.Forms.TabPage HillTabPage2;

		// Token: 0x04000166 RID: 358
		private global::System.Windows.Forms.PropertyGrid LTPHillPropertyGrid01;

		// Token: 0x04000167 RID: 359
		private global::System.Windows.Forms.Button LTPHillApplyButton;

		// Token: 0x04000168 RID: 360
		private global::System.Windows.Forms.Button LTPHillUndoButton;

		// Token: 0x04000169 RID: 361
		private global::System.Windows.Forms.Button LTPHillSetDefaultButton;

		// Token: 0x0400016A RID: 362
		private global::System.Windows.Forms.Button LTPHillMakeButton;

		// Token: 0x0400016B RID: 363
		private global::System.Windows.Forms.TabPage LandscapeTabPageRoad;

		// Token: 0x0400016C RID: 364
		private global::System.Windows.Forms.Button LTPRoadSaveButton;

		// Token: 0x0400016D RID: 365
		private global::System.Windows.Forms.TabControl LTPRoadTabControl;

		// Token: 0x0400016E RID: 366
		private global::System.Windows.Forms.TabPage RoadTabPage0;

		// Token: 0x0400016F RID: 367
		private global::System.Windows.Forms.Button LTPRoadPresetButton;

		// Token: 0x04000170 RID: 368
		private global::System.Windows.Forms.ComboBox LTPRoadPresetComboBox;

		// Token: 0x04000171 RID: 369
		private global::System.Windows.Forms.ListView LTPRoadPresetListView;

		// Token: 0x04000172 RID: 370
		private global::System.Windows.Forms.Label LTPRoadPresetLabel;

		// Token: 0x04000173 RID: 371
		private global::System.Windows.Forms.TabPage RoadTabPage1;

		// Token: 0x04000174 RID: 372
		private global::System.Windows.Forms.PropertyGrid LTPRoadPropertyGrid;

		// Token: 0x04000175 RID: 373
		private global::System.Windows.Forms.Button LTPRoadApplyButton;

		// Token: 0x04000176 RID: 374
		private global::System.Windows.Forms.Button LTPRoadClearButton;

		// Token: 0x04000177 RID: 375
		private global::System.Windows.Forms.Button LTPRoadCreateButton;

		// Token: 0x04000178 RID: 376
		private global::System.Windows.Forms.Button LTPRoadDefaultsButton;

		// Token: 0x04000179 RID: 377
		private global::System.Windows.Forms.RadioButton LTPWaterWaterToolButton00;

		// Token: 0x0400017A RID: 378
		private global::System.Windows.Forms.CheckBox LTPHeightRaisePlatoCheckBox;

		// Token: 0x0400017B RID: 379
		private global::System.Windows.Forms.CheckBox LTPHeightLowerPlatoCheckBox;

		// Token: 0x0400017C RID: 380
		private global::System.Windows.Forms.RadioButton ZoneSize3Button;

		// Token: 0x0400017D RID: 381
		private global::System.Windows.Forms.RadioButton LightSize3Button;
	}
}
