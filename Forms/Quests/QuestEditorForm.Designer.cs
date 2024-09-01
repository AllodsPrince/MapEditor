namespace MapEditor.Forms.Quests
{
	// Token: 0x020000D9 RID: 217
	public partial class QuestEditorForm : global::MapEditor.Forms.Base.BaseForm
	{
		// Token: 0x06000B1A RID: 2842 RVA: 0x0005BD44 File Offset: 0x0005AD44
		protected override void Dispose(bool disposing)
		{
			if (disposing && this.components != null)
			{
				this.components.Dispose();
			}
			base.Dispose(disposing);
		}

		// Token: 0x06000B1B RID: 2843 RVA: 0x0005BD64 File Offset: 0x0005AD64
		private void InitializeComponent()
		{
			this.components = new global::System.ComponentModel.Container();
			global::System.ComponentModel.ComponentResourceManager resources = new global::System.ComponentModel.ComponentResourceManager(typeof(global::MapEditor.Forms.Quests.QuestEditorForm));
			this.toolStripMenuItem6 = new global::System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItem7 = new global::System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItem8 = new global::System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItem9 = new global::System.Windows.Forms.ToolStripMenuItem();
			this.editGameNameToolStripMenuItem = new global::System.Windows.Forms.ToolStripMenuItem();
			this.editPropertiesToolStripMenuItem2 = new global::System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItem10 = new global::System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItem13 = new global::System.Windows.Forms.ToolStripMenuItem();
			this.ChooseQuestButton = new global::System.Windows.Forms.Button();
			this.LootTableTabPage = new global::System.Windows.Forms.TabPage();
			this.LootTableGrid = new global::System.Windows.Forms.DataGridView();
			this.dataGridViewTextBoxColumn8 = new global::System.Windows.Forms.DataGridViewTextBoxColumn();
			this.Column1 = new global::System.Windows.Forms.DataGridViewTextBoxColumn();
			this.dataGridViewTextBoxColumn9 = new global::System.Windows.Forms.DataGridViewTextBoxColumn();
			this.Column2 = new global::System.Windows.Forms.DataGridViewTextBoxColumn();
			this.Column3 = new global::System.Windows.Forms.DataGridViewTextBoxColumn();
			this.Column5 = new global::System.Windows.Forms.DataGridViewCheckBoxColumn();
			this.TextQuestTabPage = new global::System.Windows.Forms.TabPage();
			this.label15 = new global::System.Windows.Forms.Label();
			this.CompletedTextBox = new global::System.Windows.Forms.RichTextBox();
			this.GoalTextBox = new global::System.Windows.Forms.RichTextBox();
			this.label20 = new global::System.Windows.Forms.Label();
			this.label6 = new global::System.Windows.Forms.Label();
			this.IssueTextBox = new global::System.Windows.Forms.RichTextBox();
			this.label21 = new global::System.Windows.Forms.Label();
			this.KickTextBox = new global::System.Windows.Forms.RichTextBox();
			this.label10 = new global::System.Windows.Forms.Label();
			this.NotCompletedTextBox = new global::System.Windows.Forms.RichTextBox();
			this.AdvancedTabPage = new global::System.Windows.Forms.TabPage();
			this.QuestPropertyControl = new global::MapEditor.Forms.PropertyControl.ExtendedPropertyControl.ExtendedPropertyControl();
			this.label1 = new global::System.Windows.Forms.Label();
			this.CommonTabPage = new global::System.Windows.Forms.TabPage();
			this.TutorialCheckBox = new global::System.Windows.Forms.CheckBox();
			this.PVPCheckBox = new global::System.Windows.Forms.CheckBox();
			this.SecretSequenceCheckBox = new global::System.Windows.Forms.CheckBox();
			this.QuestTypeComboBox = new global::System.Windows.Forms.ComboBox();
			this.label32 = new global::System.Windows.Forms.Label();
			this.PlotLineTextBox = new global::System.Windows.Forms.TextBox();
			this.QuestGiverMenuStrip = new global::System.Windows.Forms.ContextMenuStrip(this.components);
			this.toolStripMenuItem1 = new global::System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItem4 = new global::System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItem5 = new global::System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItem2 = new global::System.Windows.Forms.ToolStripMenuItem();
			this.editGameNameToolStripMenuItem1 = new global::System.Windows.Forms.ToolStripMenuItem();
			this.editPropertiesToolStripMenuItem = new global::System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItem3 = new global::System.Windows.Forms.ToolStripMenuItem();
			this.findOnMapToolStripMenuItem = new global::System.Windows.Forms.ToolStripMenuItem();
			this.label9 = new global::System.Windows.Forms.Label();
			this.ChooseQuestGiverButton = new global::System.Windows.Forms.Button();
			this.QuestGiverTextBox = new global::System.Windows.Forms.TextBox();
			this.label8 = new global::System.Windows.Forms.Label();
			this.CounterGrid = new global::System.Windows.Forms.DataGridView();
			this.Column4 = new global::System.Windows.Forms.DataGridViewTextBoxColumn();
			this.Column16 = new global::System.Windows.Forms.DataGridViewComboBoxColumn();
			this.Column17 = new global::System.Windows.Forms.DataGridViewTextBoxColumn();
			this.Column18 = new global::System.Windows.Forms.DataGridViewTextBoxColumn();
			this.Column19 = new global::System.Windows.Forms.DataGridViewCheckBoxColumn();
			this.Column15 = new global::System.Windows.Forms.DataGridViewCheckBoxColumn();
			this.Column28 = new global::System.Windows.Forms.DataGridViewCheckBoxColumn();
			this.Column24 = new global::System.Windows.Forms.DataGridViewTextBoxColumn();
			this.Column25 = new global::System.Windows.Forms.DataGridViewTextBoxColumn();
			this.Column20 = new global::System.Windows.Forms.DataGridViewTextBoxColumn();
			this.Column21 = new global::System.Windows.Forms.DataGridViewTextBoxColumn();
			this.Column22 = new global::System.Windows.Forms.DataGridViewTextBoxColumn();
			this.CharacterClassComboBox = new global::System.Windows.Forms.ComboBox();
			this.NotStartedQuestGrid = new global::System.Windows.Forms.DataGridView();
			this.dataGridViewTextBoxColumn22 = new global::System.Windows.Forms.DataGridViewTextBoxColumn();
			this.FinishedQuestGrid = new global::System.Windows.Forms.DataGridView();
			this.dataGridViewTextBoxColumn1 = new global::System.Windows.Forms.DataGridViewTextBoxColumn();
			this.panel2 = new global::System.Windows.Forms.Panel();
			this.label27 = new global::System.Windows.Forms.Label();
			this.RewAlternativeItemsGrid = new global::System.Windows.Forms.DataGridView();
			this.dataGridViewTextBoxColumn23 = new global::System.Windows.Forms.DataGridViewTextBoxColumn();
			this.dataGridViewTextBoxColumn24 = new global::System.Windows.Forms.DataGridViewTextBoxColumn();
			this.Column26 = new global::System.Windows.Forms.DataGridViewCheckBoxColumn();
			this.dataGridViewTextBoxColumn25 = new global::System.Windows.Forms.DataGridViewTextBoxColumn();
			this.dataGridViewTextBoxColumn26 = new global::System.Windows.Forms.DataGridViewTextBoxColumn();
			this.Column27 = new global::System.Windows.Forms.DataGridViewCheckBoxColumn();
			this.label26 = new global::System.Windows.Forms.Label();
			this.RewMandatoryItemsGrid = new global::System.Windows.Forms.DataGridView();
			this.Column10 = new global::System.Windows.Forms.DataGridViewTextBoxColumn();
			this.Column11 = new global::System.Windows.Forms.DataGridViewTextBoxColumn();
			this.Column14 = new global::System.Windows.Forms.DataGridViewCheckBoxColumn();
			this.Column12 = new global::System.Windows.Forms.DataGridViewTextBoxColumn();
			this.Column13 = new global::System.Windows.Forms.DataGridViewTextBoxColumn();
			this.Column23 = new global::System.Windows.Forms.DataGridViewCheckBoxColumn();
			this.RewCurrencyGrid = new global::System.Windows.Forms.DataGridView();
			this.dataGridViewTextBoxColumn3 = new global::System.Windows.Forms.DataGridViewTextBoxColumn();
			this.dataGridViewTextBoxColumn4 = new global::System.Windows.Forms.DataGridViewTextBoxColumn();
			this.label35 = new global::System.Windows.Forms.Label();
			this.RewReputationGrid = new global::System.Windows.Forms.DataGridView();
			this.dataGridViewTextBoxColumn2 = new global::System.Windows.Forms.DataGridViewTextBoxColumn();
			this.Column29 = new global::System.Windows.Forms.DataGridViewTextBoxColumn();
			this.label34 = new global::System.Windows.Forms.Label();
			this.RewHonorQtyTextBox = new global::System.Windows.Forms.TextBox();
			this.label33 = new global::System.Windows.Forms.Label();
			this.AutoMoneyRewardErrorLabel = new global::System.Windows.Forms.Label();
			this.AutoMoneyRewardCheckBox = new global::System.Windows.Forms.CheckBox();
			this.label25 = new global::System.Windows.Forms.Label();
			this.RewExpRealQtyLabel = new global::System.Windows.Forms.Label();
			this.RewCopperQtyTextBox = new global::System.Windows.Forms.TextBox();
			this.label24 = new global::System.Windows.Forms.Label();
			this.RewSilverQtyTextBox = new global::System.Windows.Forms.TextBox();
			this.label23 = new global::System.Windows.Forms.Label();
			this.RewGoldQtyTextBox = new global::System.Windows.Forms.TextBox();
			this.label13 = new global::System.Windows.Forms.Label();
			this.RewExpQtyTextBox = new global::System.Windows.Forms.TextBox();
			this.label12 = new global::System.Windows.Forms.Label();
			this.label11 = new global::System.Windows.Forms.Label();
			this.ChooseCompeterButton = new global::System.Windows.Forms.Button();
			this.CompleterTextBox = new global::System.Windows.Forms.TextBox();
			this.label3 = new global::System.Windows.Forms.Label();
			this.label28 = new global::System.Windows.Forms.Label();
			this.label7 = new global::System.Windows.Forms.Label();
			this.label22 = new global::System.Windows.Forms.Label();
			this.label5 = new global::System.Windows.Forms.Label();
			this.PlayerLevelTextBox = new global::System.Windows.Forms.TextBox();
			this.label2 = new global::System.Windows.Forms.Label();
			this.label19 = new global::System.Windows.Forms.Label();
			this.FilePathTextBox = new global::System.Windows.Forms.TextBox();
			this.label18 = new global::System.Windows.Forms.Label();
			this.GameNameTextBox = new global::System.Windows.Forms.TextBox();
			this.label17 = new global::System.Windows.Forms.Label();
			this.QuestNameTextBox = new global::System.Windows.Forms.TextBox();
			this.QuestLevelTextBox = new global::System.Windows.Forms.TextBox();
			this.label4 = new global::System.Windows.Forms.Label();
			this.CounterMenuStrip = new global::System.Windows.Forms.ContextMenuStrip(this.components);
			this.createItemToolStripMenuItem = new global::System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItem28 = new global::System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItem29 = new global::System.Windows.Forms.ToolStripMenuItem();
			this.createExploitToolStripMenuItem = new global::System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItem30 = new global::System.Windows.Forms.ToolStripMenuItem();
			this.editGameNameToolStripMenuItem2 = new global::System.Windows.Forms.ToolStripMenuItem();
			this.editPropertiesToolStripMenuItem1 = new global::System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItem31 = new global::System.Windows.Forms.ToolStripMenuItem();
			this.RestrQuestMenuStrip = new global::System.Windows.Forms.ContextMenuStrip(this.components);
			this.toolStripMenuItem11 = new global::System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItem12 = new global::System.Windows.Forms.ToolStripMenuItem();
			this.PrevQuestMenuStrip = new global::System.Windows.Forms.ContextMenuStrip(this.components);
			this.toolStripMenuItem14 = new global::System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItem15 = new global::System.Windows.Forms.ToolStripMenuItem();
			this.AlternativeItemsMenuStrip = new global::System.Windows.Forms.ContextMenuStrip(this.components);
			this.toolStripMenuItem25 = new global::System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItem26 = new global::System.Windows.Forms.ToolStripMenuItem();
			this.editGameNameToolStripMenuItem4 = new global::System.Windows.Forms.ToolStripMenuItem();
			this.editPropertiesToolStripMenuItem4 = new global::System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItem27 = new global::System.Windows.Forms.ToolStripMenuItem();
			this.openSelectDialogToolStripMenuItem = new global::System.Windows.Forms.ToolStripMenuItem();
			this.MandatoryItemsMenuStrip = new global::System.Windows.Forms.ContextMenuStrip(this.components);
			this.toolStripMenuItem19 = new global::System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItem23 = new global::System.Windows.Forms.ToolStripMenuItem();
			this.editGameNameToolStripMenuItem3 = new global::System.Windows.Forms.ToolStripMenuItem();
			this.editPropertiesToolStripMenuItem3 = new global::System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItem24 = new global::System.Windows.Forms.ToolStripMenuItem();
			this.label16 = new global::System.Windows.Forms.Label();
			this.SaveButton = new global::System.Windows.Forms.Button();
			this.DeleteQuestButton = new global::System.Windows.Forms.Button();
			this.AddNewQuestButton = new global::System.Windows.Forms.Button();
			this.MainTabControl = new global::System.Windows.Forms.TabControl();
			this.MarkersTabPage = new global::System.Windows.Forms.TabPage();
			this.label29 = new global::System.Windows.Forms.Label();
			this.EditGoalLocatorButton = new global::System.Windows.Forms.Button();
			this.RemoveGoalLocatorButton = new global::System.Windows.Forms.Button();
			this.AddGoalLocatorButton = new global::System.Windows.Forms.Button();
			this.GoalLocatorsListView = new global::System.Windows.Forms.ListView();
			this.EditReturnLocatorButton = new global::System.Windows.Forms.Button();
			this.autoSetGoalLocatorCheckBox = new global::System.Windows.Forms.CheckBox();
			this.label30 = new global::System.Windows.Forms.Label();
			this.mapPanel = new global::System.Windows.Forms.Panel();
			this.MarkerZoneComboBox = new global::System.Windows.Forms.ComboBox();
			this.CloseButton = new global::System.Windows.Forms.Button();
			this.dataGridViewTextBoxColumn5 = new global::System.Windows.Forms.DataGridViewTextBoxColumn();
			this.dataGridViewTextBoxColumn6 = new global::System.Windows.Forms.DataGridViewTextBoxColumn();
			this.dataGridViewTextBoxColumn7 = new global::System.Windows.Forms.DataGridViewTextBoxColumn();
			this.dataGridViewTextBoxColumn10 = new global::System.Windows.Forms.DataGridViewTextBoxColumn();
			this.dataGridViewTextBoxColumn11 = new global::System.Windows.Forms.DataGridViewTextBoxColumn();
			this.dataGridViewTextBoxColumn12 = new global::System.Windows.Forms.DataGridViewTextBoxColumn();
			this.dataGridViewTextBoxColumn13 = new global::System.Windows.Forms.DataGridViewTextBoxColumn();
			this.dataGridViewTextBoxColumn14 = new global::System.Windows.Forms.DataGridViewTextBoxColumn();
			this.dataGridViewTextBoxColumn15 = new global::System.Windows.Forms.DataGridViewTextBoxColumn();
			this.dataGridViewTextBoxColumn16 = new global::System.Windows.Forms.DataGridViewTextBoxColumn();
			this.dataGridViewTextBoxColumn17 = new global::System.Windows.Forms.DataGridViewTextBoxColumn();
			this.dataGridViewTextBoxColumn18 = new global::System.Windows.Forms.DataGridViewTextBoxColumn();
			this.dataGridViewTextBoxColumn19 = new global::System.Windows.Forms.DataGridViewTextBoxColumn();
			this.dataGridViewTextBoxColumn20 = new global::System.Windows.Forms.DataGridViewTextBoxColumn();
			this.dataGridViewTextBoxColumn21 = new global::System.Windows.Forms.DataGridViewTextBoxColumn();
			this.Column6 = new global::System.Windows.Forms.DataGridViewTextBoxColumn();
			this.Column7 = new global::System.Windows.Forms.DataGridViewTextBoxColumn();
			this.Column8 = new global::System.Windows.Forms.DataGridViewTextBoxColumn();
			this.Column9 = new global::System.Windows.Forms.DataGridViewTextBoxColumn();
			this.ZoneTextBox = new global::System.Windows.Forms.TextBox();
			this.LootTableMobMenuStrip = new global::System.Windows.Forms.ContextMenuStrip(this.components);
			this.createMobToolStripMenuItem = new global::System.Windows.Forms.ToolStripMenuItem();
			this.createSteleOrChestToolStripMenuItem = new global::System.Windows.Forms.ToolStripMenuItem();
			this.clearCellToolStripMenuItem = new global::System.Windows.Forms.ToolStripMenuItem();
			this.editGameNameToolStripMenuItem5 = new global::System.Windows.Forms.ToolStripMenuItem();
			this.editPropertiesToolStripMenuItem5 = new global::System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItem16 = new global::System.Windows.Forms.ToolStripMenuItem();
			this.LootTableItemMenuStrip = new global::System.Windows.Forms.ContextMenuStrip(this.components);
			this.createItemToolStripMenuItem2 = new global::System.Windows.Forms.ToolStripMenuItem();
			this.clearCellToolStripMenuItem1 = new global::System.Windows.Forms.ToolStripMenuItem();
			this.editGameNameToolStripMenuItem6 = new global::System.Windows.Forms.ToolStripMenuItem();
			this.editPropertiesToolStripMenuItem6 = new global::System.Windows.Forms.ToolStripMenuItem();
			this.openSelectDialogToolStripMenuItem1 = new global::System.Windows.Forms.ToolStripMenuItem();
			this.SaveAsButton = new global::System.Windows.Forms.Button();
			this.ShowDiagramButton = new global::System.Windows.Forms.Button();
			this.ShowQuestLineButton = new global::System.Windows.Forms.Button();
			this.QuestTypeLabel = new global::System.Windows.Forms.Label();
			this.label14 = new global::System.Windows.Forms.Label();
			this.ResurceIdTextBox = new global::System.Windows.Forms.TextBox();
			this.toolStripMenuItem17 = new global::System.Windows.Forms.ToolStripMenuItem();
			global::System.Windows.Forms.ContextMenuStrip QuestFinisherMenuStrip = new global::System.Windows.Forms.ContextMenuStrip(this.components);
			QuestFinisherMenuStrip.SuspendLayout();
			this.LootTableTabPage.SuspendLayout();
			((global::System.ComponentModel.ISupportInitialize)this.LootTableGrid).BeginInit();
			this.TextQuestTabPage.SuspendLayout();
			this.AdvancedTabPage.SuspendLayout();
			this.CommonTabPage.SuspendLayout();
			this.QuestGiverMenuStrip.SuspendLayout();
			((global::System.ComponentModel.ISupportInitialize)this.CounterGrid).BeginInit();
			((global::System.ComponentModel.ISupportInitialize)this.NotStartedQuestGrid).BeginInit();
			((global::System.ComponentModel.ISupportInitialize)this.FinishedQuestGrid).BeginInit();
			this.panel2.SuspendLayout();
			((global::System.ComponentModel.ISupportInitialize)this.RewAlternativeItemsGrid).BeginInit();
			((global::System.ComponentModel.ISupportInitialize)this.RewMandatoryItemsGrid).BeginInit();
			((global::System.ComponentModel.ISupportInitialize)this.RewCurrencyGrid).BeginInit();
			((global::System.ComponentModel.ISupportInitialize)this.RewReputationGrid).BeginInit();
			this.CounterMenuStrip.SuspendLayout();
			this.RestrQuestMenuStrip.SuspendLayout();
			this.PrevQuestMenuStrip.SuspendLayout();
			this.AlternativeItemsMenuStrip.SuspendLayout();
			this.MandatoryItemsMenuStrip.SuspendLayout();
			this.MainTabControl.SuspendLayout();
			this.MarkersTabPage.SuspendLayout();
			this.LootTableMobMenuStrip.SuspendLayout();
			this.LootTableItemMenuStrip.SuspendLayout();
			base.SuspendLayout();
			QuestFinisherMenuStrip.BindingContext = null;
			QuestFinisherMenuStrip.Items.AddRange(new global::System.Windows.Forms.ToolStripItem[]
			{
				this.toolStripMenuItem6,
				this.toolStripMenuItem7,
				this.toolStripMenuItem8,
				this.toolStripMenuItem9,
				this.editGameNameToolStripMenuItem,
				this.editPropertiesToolStripMenuItem2,
				this.toolStripMenuItem10,
				this.toolStripMenuItem13
			});
			QuestFinisherMenuStrip.Name = "QuetsGiverMenuStrip";
			QuestFinisherMenuStrip.Region = null;
			resources.ApplyResources(QuestFinisherMenuStrip, "QuestFinisherMenuStrip");
			this.toolStripMenuItem6.Name = "toolStripMenuItem6";
			resources.ApplyResources(this.toolStripMenuItem6, "toolStripMenuItem6");
			this.toolStripMenuItem6.Tag = "create_npc";
			this.toolStripMenuItem7.Name = "toolStripMenuItem7";
			resources.ApplyResources(this.toolStripMenuItem7, "toolStripMenuItem7");
			this.toolStripMenuItem7.Tag = "create_quest_item";
			this.toolStripMenuItem8.Name = "toolStripMenuItem8";
			resources.ApplyResources(this.toolStripMenuItem8, "toolStripMenuItem8");
			this.toolStripMenuItem8.Tag = "create_device";
			this.toolStripMenuItem9.Name = "toolStripMenuItem9";
			resources.ApplyResources(this.toolStripMenuItem9, "toolStripMenuItem9");
			this.toolStripMenuItem9.Tag = "clear_cell";
			this.editGameNameToolStripMenuItem.Name = "editGameNameToolStripMenuItem";
			resources.ApplyResources(this.editGameNameToolStripMenuItem, "editGameNameToolStripMenuItem");
			this.editGameNameToolStripMenuItem.Tag = "edit_game_name";
			this.editPropertiesToolStripMenuItem2.Name = "editPropertiesToolStripMenuItem2";
			resources.ApplyResources(this.editPropertiesToolStripMenuItem2, "editPropertiesToolStripMenuItem2");
			this.editPropertiesToolStripMenuItem2.Tag = "edit_properties";
			this.toolStripMenuItem10.Name = "toolStripMenuItem10";
			resources.ApplyResources(this.toolStripMenuItem10, "toolStripMenuItem10");
			this.toolStripMenuItem10.Tag = "select_dialog";
			this.toolStripMenuItem13.Name = "toolStripMenuItem13";
			resources.ApplyResources(this.toolStripMenuItem13, "toolStripMenuItem13");
			this.toolStripMenuItem13.Tag = "find_on_map";
			this.ChooseQuestButton.ImeMode = global::System.Windows.Forms.ImeMode.NoControl;
			resources.ApplyResources(this.ChooseQuestButton, "ChooseQuestButton");
			this.ChooseQuestButton.Name = "ChooseQuestButton";
			this.ChooseQuestButton.Text = "Choose quest ...";
			this.ChooseQuestButton.UseVisualStyleBackColor = true;
			this.LootTableTabPage.Controls.Add(this.LootTableGrid);
			this.LootTableTabPage.Location = new global::System.Drawing.Point(4, 22);
			this.LootTableTabPage.Name = "LootTableTabPage";
			resources.ApplyResources(this.LootTableTabPage, "LootTableTabPage");
			this.LootTableTabPage.TabIndex = 3;
			this.LootTableTabPage.UseVisualStyleBackColor = true;
			resources.ApplyResources(this.LootTableGrid, "LootTableGrid");
			this.LootTableGrid.ColumnHeadersHeightSizeMode = global::System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.LootTableGrid.Columns.AddRange(new global::System.Windows.Forms.DataGridViewColumn[]
			{
				this.dataGridViewTextBoxColumn8,
				this.Column1,
				this.dataGridViewTextBoxColumn9,
				this.Column2,
				this.Column3,
				this.Column5
			});
			this.LootTableGrid.Name = "LootTableGrid";
			this.LootTableGrid.RowTemplate.Selected = false;
			resources.ApplyResources(this.dataGridViewTextBoxColumn8, "dataGridViewTextBoxColumn8");
			this.dataGridViewTextBoxColumn8.Name = "dataGridViewTextBoxColumn8";
			this.dataGridViewTextBoxColumn8.ReadOnly = true;
			resources.ApplyResources(this.Column1, "Column1");
			this.Column1.Name = "Column1";
			this.Column1.ReadOnly = true;
			resources.ApplyResources(this.dataGridViewTextBoxColumn9, "dataGridViewTextBoxColumn9");
			this.dataGridViewTextBoxColumn9.Name = "dataGridViewTextBoxColumn9";
			resources.ApplyResources(this.Column2, "Column2");
			this.Column2.Name = "Column2";
			resources.ApplyResources(this.Column3, "Column3");
			this.Column3.Name = "Column3";
			this.Column5.FalseValue = "false";
			resources.ApplyResources(this.Column5, "Column5");
			this.Column5.Name = "Column5";
			this.Column5.TrueValue = "true";
			this.TextQuestTabPage.Controls.Add(this.label15);
			this.TextQuestTabPage.Controls.Add(this.CompletedTextBox);
			this.TextQuestTabPage.Controls.Add(this.GoalTextBox);
			this.TextQuestTabPage.Controls.Add(this.label20);
			this.TextQuestTabPage.Controls.Add(this.label6);
			this.TextQuestTabPage.Controls.Add(this.IssueTextBox);
			this.TextQuestTabPage.Controls.Add(this.label21);
			this.TextQuestTabPage.Controls.Add(this.KickTextBox);
			this.TextQuestTabPage.Controls.Add(this.label10);
			this.TextQuestTabPage.Controls.Add(this.NotCompletedTextBox);
			this.TextQuestTabPage.Location = new global::System.Drawing.Point(4, 22);
			this.TextQuestTabPage.Name = "TextQuestTabPage";
			resources.ApplyResources(this.TextQuestTabPage, "TextQuestTabPage");
			this.TextQuestTabPage.TabIndex = 1;
			this.TextQuestTabPage.UseVisualStyleBackColor = true;
			resources.ApplyResources(this.label15, "label15");
			this.label15.ImeMode = global::System.Windows.Forms.ImeMode.NoControl;
			this.label15.Name = "label15";
			this.label15.Text = "Completed text:";
			resources.ApplyResources(this.CompletedTextBox, "CompletedTextBox");
			this.CompletedTextBox.Name = "CompletedTextBox";
			resources.ApplyResources(this.GoalTextBox, "GoalTextBox");
			this.GoalTextBox.Name = "GoalTextBox";
			resources.ApplyResources(this.label20, "label20");
			this.label20.ImeMode = global::System.Windows.Forms.ImeMode.NoControl;
			this.label20.Name = "label20";
			this.label20.Text = "Goal text:";
			resources.ApplyResources(this.label6, "label6");
			this.label6.ImeMode = global::System.Windows.Forms.ImeMode.NoControl;
			this.label6.Name = "label6";
			this.label6.Text = "Issue text:";
			resources.ApplyResources(this.IssueTextBox, "IssueTextBox");
			this.IssueTextBox.Name = "IssueTextBox";
			resources.ApplyResources(this.label21, "label21");
			this.label21.ImeMode = global::System.Windows.Forms.ImeMode.NoControl;
			this.label21.Name = "label21";
			this.label21.Text = "Kick text:";
			resources.ApplyResources(this.KickTextBox, "KickTextBox");
			this.KickTextBox.Name = "KickTextBox";
			resources.ApplyResources(this.label10, "label10");
			this.label10.ImeMode = global::System.Windows.Forms.ImeMode.NoControl;
			this.label10.Name = "label10";
			this.label10.Text = "Not completed text:";
			resources.ApplyResources(this.NotCompletedTextBox, "NotCompletedTextBox");
			this.NotCompletedTextBox.Name = "NotCompletedTextBox";
			this.AdvancedTabPage.Controls.Add(this.QuestPropertyControl);
			this.AdvancedTabPage.Controls.Add(this.label1);
			this.AdvancedTabPage.Location = new global::System.Drawing.Point(4, 22);
			this.AdvancedTabPage.Name = "AdvancedTabPage";
			resources.ApplyResources(this.AdvancedTabPage, "AdvancedTabPage");
			this.AdvancedTabPage.TabIndex = 0;
			this.AdvancedTabPage.UseVisualStyleBackColor = true;
			this.QuestPropertyControl.AllowDrop = true;
			resources.ApplyResources(this.QuestPropertyControl, "QuestPropertyControl");
			this.QuestPropertyControl.DefaultLocationFolder = "";
			this.QuestPropertyControl.Name = "QuestPropertyControl";
			this.QuestPropertyControl.SelectedObject = null;
			this.QuestPropertyControl.SelectedObjects = new object[0];
			this.QuestPropertyControl.SkipRefresh = false;
			this.QuestPropertyControl.TitleControl = null;
			this.QuestPropertyControl.TitleRelativeFrom = null;
			this.QuestPropertyControl.TitleStart = "";
			resources.ApplyResources(this.label1, "label1");
			this.label1.ImeMode = global::System.Windows.Forms.ImeMode.NoControl;
			this.label1.Name = "label1";
			this.label1.Text = "Start impacts :";
			this.CommonTabPage.Controls.Add(this.TutorialCheckBox);
			this.CommonTabPage.Controls.Add(this.PVPCheckBox);
			this.CommonTabPage.Controls.Add(this.SecretSequenceCheckBox);
			this.CommonTabPage.Controls.Add(this.QuestTypeComboBox);
			this.CommonTabPage.Controls.Add(this.label32);
			this.CommonTabPage.Controls.Add(this.PlotLineTextBox);
			this.CommonTabPage.Controls.Add(this.label9);
			this.CommonTabPage.Controls.Add(this.ChooseQuestGiverButton);
			this.CommonTabPage.Controls.Add(this.QuestGiverTextBox);
			this.CommonTabPage.Controls.Add(this.label8);
			this.CommonTabPage.Controls.Add(this.CounterGrid);
			this.CommonTabPage.Controls.Add(this.CharacterClassComboBox);
			this.CommonTabPage.Controls.Add(this.NotStartedQuestGrid);
			this.CommonTabPage.Controls.Add(this.FinishedQuestGrid);
			this.CommonTabPage.Controls.Add(this.panel2);
			this.CommonTabPage.Controls.Add(this.label11);
			this.CommonTabPage.Controls.Add(this.ChooseCompeterButton);
			this.CommonTabPage.Controls.Add(this.CompleterTextBox);
			this.CommonTabPage.Controls.Add(this.label3);
			this.CommonTabPage.Controls.Add(this.label28);
			this.CommonTabPage.Controls.Add(this.label7);
			this.CommonTabPage.Controls.Add(this.label22);
			this.CommonTabPage.Controls.Add(this.label5);
			this.CommonTabPage.Controls.Add(this.PlayerLevelTextBox);
			this.CommonTabPage.Controls.Add(this.label2);
			this.CommonTabPage.Controls.Add(this.label19);
			this.CommonTabPage.Controls.Add(this.FilePathTextBox);
			this.CommonTabPage.Controls.Add(this.label18);
			this.CommonTabPage.Controls.Add(this.GameNameTextBox);
			this.CommonTabPage.Controls.Add(this.label17);
			this.CommonTabPage.Controls.Add(this.QuestNameTextBox);
			this.CommonTabPage.Controls.Add(this.QuestLevelTextBox);
			this.CommonTabPage.Controls.Add(this.label4);
			this.CommonTabPage.Location = new global::System.Drawing.Point(4, 22);
			this.CommonTabPage.Name = "CommonTabPage";
			resources.ApplyResources(this.CommonTabPage, "CommonTabPage");
			this.CommonTabPage.TabIndex = 4;
			this.CommonTabPage.UseVisualStyleBackColor = true;
			this.TutorialCheckBox.AutoSize = true;
			this.TutorialCheckBox.ImeMode = global::System.Windows.Forms.ImeMode.NoControl;
			resources.ApplyResources(this.TutorialCheckBox, "TutorialCheckBox");
			this.TutorialCheckBox.Name = "TutorialCheckBox";
			this.TutorialCheckBox.Text = "Tutorial";
			this.TutorialCheckBox.UseVisualStyleBackColor = true;
			this.PVPCheckBox.AutoSize = true;
			this.PVPCheckBox.ImeMode = global::System.Windows.Forms.ImeMode.NoControl;
			resources.ApplyResources(this.PVPCheckBox, "PVPCheckBox");
			this.PVPCheckBox.Name = "PVPCheckBox";
			this.PVPCheckBox.Text = "PVP";
			this.PVPCheckBox.UseVisualStyleBackColor = true;
			this.SecretSequenceCheckBox.AutoSize = true;
			resources.ApplyResources(this.SecretSequenceCheckBox, "SecretSequenceCheckBox");
			this.SecretSequenceCheckBox.Name = "SecretSequenceCheckBox";
			this.SecretSequenceCheckBox.Text = "Secret Sequence";
			this.SecretSequenceCheckBox.UseVisualStyleBackColor = true;
			this.QuestTypeComboBox.DropDownStyle = global::System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.QuestTypeComboBox.FormattingEnabled = true;
			resources.ApplyResources(this.QuestTypeComboBox, "QuestTypeComboBox");
			this.QuestTypeComboBox.Name = "QuestTypeComboBox";
			resources.ApplyResources(this.label32, "label32");
			this.label32.ImeMode = global::System.Windows.Forms.ImeMode.NoControl;
			this.label32.Name = "label32";
			this.label32.Text = "Quest Type:";
			this.PlotLineTextBox.ContextMenuStrip = this.QuestGiverMenuStrip;
			resources.ApplyResources(this.PlotLineTextBox, "PlotLineTextBox");
			this.PlotLineTextBox.Name = "PlotLineTextBox";
			this.QuestGiverMenuStrip.BindingContext = null;
			this.QuestGiverMenuStrip.Items.AddRange(new global::System.Windows.Forms.ToolStripItem[]
			{
				this.toolStripMenuItem1,
				this.toolStripMenuItem4,
				this.toolStripMenuItem5,
				this.toolStripMenuItem2,
				this.editGameNameToolStripMenuItem1,
				this.editPropertiesToolStripMenuItem,
				this.toolStripMenuItem3,
				this.findOnMapToolStripMenuItem
			});
			this.QuestGiverMenuStrip.Name = "QuetsGiverMenuStrip";
			this.QuestGiverMenuStrip.Region = null;
			resources.ApplyResources(this.QuestGiverMenuStrip, "QuestGiverMenuStrip");
			this.toolStripMenuItem1.Name = "toolStripMenuItem1";
			resources.ApplyResources(this.toolStripMenuItem1, "toolStripMenuItem1");
			this.toolStripMenuItem1.Tag = "create_npc";
			this.toolStripMenuItem4.Name = "toolStripMenuItem4";
			resources.ApplyResources(this.toolStripMenuItem4, "toolStripMenuItem4");
			this.toolStripMenuItem4.Tag = "create_quest_item";
			this.toolStripMenuItem5.Name = "toolStripMenuItem5";
			resources.ApplyResources(this.toolStripMenuItem5, "toolStripMenuItem5");
			this.toolStripMenuItem5.Tag = "create_device";
			this.toolStripMenuItem2.Name = "toolStripMenuItem2";
			resources.ApplyResources(this.toolStripMenuItem2, "toolStripMenuItem2");
			this.toolStripMenuItem2.Tag = "clear_cell";
			this.editGameNameToolStripMenuItem1.Name = "editGameNameToolStripMenuItem1";
			resources.ApplyResources(this.editGameNameToolStripMenuItem1, "editGameNameToolStripMenuItem1");
			this.editGameNameToolStripMenuItem1.Tag = "edit_game_name";
			this.editPropertiesToolStripMenuItem.Name = "editPropertiesToolStripMenuItem";
			resources.ApplyResources(this.editPropertiesToolStripMenuItem, "editPropertiesToolStripMenuItem");
			this.editPropertiesToolStripMenuItem.Tag = "edit_properties";
			this.toolStripMenuItem3.Name = "toolStripMenuItem3";
			resources.ApplyResources(this.toolStripMenuItem3, "toolStripMenuItem3");
			this.toolStripMenuItem3.Tag = "select_dialog";
			this.findOnMapToolStripMenuItem.Name = "findOnMapToolStripMenuItem";
			resources.ApplyResources(this.findOnMapToolStripMenuItem, "findOnMapToolStripMenuItem");
			this.findOnMapToolStripMenuItem.Tag = "find_on_map";
			resources.ApplyResources(this.label9, "label9");
			this.label9.ImeMode = global::System.Windows.Forms.ImeMode.NoControl;
			this.label9.Name = "label9";
			this.label9.Text = "Plot Line :";
			this.ChooseQuestGiverButton.ImeMode = global::System.Windows.Forms.ImeMode.NoControl;
			resources.ApplyResources(this.ChooseQuestGiverButton, "ChooseQuestGiverButton");
			this.ChooseQuestGiverButton.Name = "ChooseQuestGiverButton";
			this.ChooseQuestGiverButton.Text = " ...";
			this.ChooseQuestGiverButton.UseVisualStyleBackColor = true;
			this.QuestGiverTextBox.ContextMenuStrip = this.QuestGiverMenuStrip;
			resources.ApplyResources(this.QuestGiverTextBox, "QuestGiverTextBox");
			this.QuestGiverTextBox.Name = "QuestGiverTextBox";
			this.QuestGiverTextBox.ReadOnly = true;
			resources.ApplyResources(this.label8, "label8");
			this.label8.ImeMode = global::System.Windows.Forms.ImeMode.NoControl;
			this.label8.Name = "label8";
			this.label8.Text = "Counters :";
			this.CounterGrid.AllowUserToAddRows = false;
			this.CounterGrid.AllowUserToDeleteRows = false;
			resources.ApplyResources(this.CounterGrid, "CounterGrid");
			this.CounterGrid.ColumnHeadersHeightSizeMode = global::System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.CounterGrid.Columns.AddRange(new global::System.Windows.Forms.DataGridViewColumn[]
			{
				this.Column4,
				this.Column16,
				this.Column17,
				this.Column18,
				this.Column19,
				this.Column15,
				this.Column28,
				this.Column24,
				this.Column25,
				this.Column20,
				this.Column21,
				this.Column22
			});
			this.CounterGrid.Name = "CounterGrid";
			resources.ApplyResources(this.Column4, "Column4");
			this.Column4.Name = "Column4";
			this.Column4.ReadOnly = true;
			resources.ApplyResources(this.Column16, "Column16");
			this.Column16.Items.AddRange(new object[]
			{
				"Item",
				"Kill",
				"Special",
				"Honor",
				"KillAvatar"
			});
			this.Column16.Name = "Column16";
			resources.ApplyResources(this.Column17, "Column17");
			this.Column17.Name = "Column17";
			this.Column17.SortMode = global::System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
			resources.ApplyResources(this.Column18, "Column18");
			this.Column18.Name = "Column18";
			this.Column18.SortMode = global::System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
			resources.ApplyResources(this.Column19, "Column19");
			this.Column19.Name = "Column19";
			resources.ApplyResources(this.Column15, "Column15");
			this.Column15.Name = "Column15";
			resources.ApplyResources(this.Column28, "Column28");
			this.Column28.Name = "Column28";
			resources.ApplyResources(this.Column24, "Column24");
			this.Column24.Name = "Column24";
			this.Column24.ReadOnly = true;
			this.Column24.SortMode = global::System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
			resources.ApplyResources(this.Column25, "Column25");
			this.Column25.Name = "Column25";
			this.Column25.ReadOnly = true;
			this.Column25.SortMode = global::System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
			resources.ApplyResources(this.Column20, "Column20");
			this.Column20.Name = "Column20";
			this.Column20.ReadOnly = true;
			this.Column20.Resizable = global::System.Windows.Forms.DataGridViewTriState.True;
			this.Column20.SortMode = global::System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
			resources.ApplyResources(this.Column21, "Column21");
			this.Column21.Name = "Column21";
			this.Column21.ReadOnly = true;
			this.Column21.Resizable = global::System.Windows.Forms.DataGridViewTriState.True;
			this.Column21.SortMode = global::System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
			resources.ApplyResources(this.Column22, "Column22");
			this.Column22.Name = "Column22";
			this.Column22.ReadOnly = true;
			this.Column22.Resizable = global::System.Windows.Forms.DataGridViewTriState.True;
			this.Column22.SortMode = global::System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
			this.CharacterClassComboBox.DropDownStyle = global::System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.CharacterClassComboBox.FormattingEnabled = true;
			resources.ApplyResources(this.CharacterClassComboBox, "CharacterClassComboBox");
			this.CharacterClassComboBox.Name = "CharacterClassComboBox";
			this.CharacterClassComboBox.Sorted = true;
			this.NotStartedQuestGrid.ColumnHeadersHeightSizeMode = global::System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.NotStartedQuestGrid.Columns.AddRange(new global::System.Windows.Forms.DataGridViewColumn[]
			{
				this.dataGridViewTextBoxColumn22
			});
			resources.ApplyResources(this.NotStartedQuestGrid, "NotStartedQuestGrid");
			this.NotStartedQuestGrid.Name = "NotStartedQuestGrid";
			this.NotStartedQuestGrid.RowTemplate.Selected = false;
			resources.ApplyResources(this.dataGridViewTextBoxColumn22, "dataGridViewTextBoxColumn22");
			this.dataGridViewTextBoxColumn22.Name = "dataGridViewTextBoxColumn22";
			this.dataGridViewTextBoxColumn22.ReadOnly = true;
			this.FinishedQuestGrid.ColumnHeadersHeightSizeMode = global::System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.FinishedQuestGrid.Columns.AddRange(new global::System.Windows.Forms.DataGridViewColumn[]
			{
				this.dataGridViewTextBoxColumn1
			});
			resources.ApplyResources(this.FinishedQuestGrid, "FinishedQuestGrid");
			this.FinishedQuestGrid.Name = "FinishedQuestGrid";
			this.FinishedQuestGrid.RowTemplate.Selected = false;
			resources.ApplyResources(this.dataGridViewTextBoxColumn1, "dataGridViewTextBoxColumn1");
			this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
			this.dataGridViewTextBoxColumn1.ReadOnly = true;
			resources.ApplyResources(this.panel2, "panel2");
			this.panel2.BorderStyle = global::System.Windows.Forms.BorderStyle.FixedSingle;
			this.panel2.Controls.Add(this.label27);
			this.panel2.Controls.Add(this.RewAlternativeItemsGrid);
			this.panel2.Controls.Add(this.label26);
			this.panel2.Controls.Add(this.RewMandatoryItemsGrid);
			this.panel2.Controls.Add(this.RewCurrencyGrid);
			this.panel2.Controls.Add(this.label35);
			this.panel2.Controls.Add(this.RewReputationGrid);
			this.panel2.Controls.Add(this.label34);
			this.panel2.Controls.Add(this.RewHonorQtyTextBox);
			this.panel2.Controls.Add(this.label33);
			this.panel2.Controls.Add(this.AutoMoneyRewardErrorLabel);
			this.panel2.Controls.Add(this.AutoMoneyRewardCheckBox);
			this.panel2.Controls.Add(this.label25);
			this.panel2.Controls.Add(this.RewExpRealQtyLabel);
			this.panel2.Controls.Add(this.RewCopperQtyTextBox);
			this.panel2.Controls.Add(this.label24);
			this.panel2.Controls.Add(this.RewSilverQtyTextBox);
			this.panel2.Controls.Add(this.label23);
			this.panel2.Controls.Add(this.RewGoldQtyTextBox);
			this.panel2.Controls.Add(this.label13);
			this.panel2.Controls.Add(this.RewExpQtyTextBox);
			this.panel2.Controls.Add(this.label12);
			this.panel2.Name = "panel2";
			resources.ApplyResources(this.label27, "label27");
			this.label27.ImeMode = global::System.Windows.Forms.ImeMode.NoControl;
			this.label27.Name = "label27";
			this.label27.Text = "Alternative items :";
			this.RewAlternativeItemsGrid.AllowUserToAddRows = false;
			this.RewAlternativeItemsGrid.AllowUserToDeleteRows = false;
			this.RewAlternativeItemsGrid.ColumnHeadersHeightSizeMode = global::System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.RewAlternativeItemsGrid.Columns.AddRange(new global::System.Windows.Forms.DataGridViewColumn[]
			{
				this.dataGridViewTextBoxColumn23,
				this.dataGridViewTextBoxColumn24,
				this.Column26,
				this.dataGridViewTextBoxColumn25,
				this.dataGridViewTextBoxColumn26,
				this.Column27
			});
			resources.ApplyResources(this.RewAlternativeItemsGrid, "RewAlternativeItemsGrid");
			this.RewAlternativeItemsGrid.Name = "RewAlternativeItemsGrid";
			resources.ApplyResources(this.dataGridViewTextBoxColumn23, "dataGridViewTextBoxColumn23");
			this.dataGridViewTextBoxColumn23.Name = "dataGridViewTextBoxColumn23";
			this.dataGridViewTextBoxColumn23.ReadOnly = true;
			this.dataGridViewTextBoxColumn23.SortMode = global::System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
			resources.ApplyResources(this.dataGridViewTextBoxColumn24, "dataGridViewTextBoxColumn24");
			this.dataGridViewTextBoxColumn24.Name = "dataGridViewTextBoxColumn24";
			this.dataGridViewTextBoxColumn24.SortMode = global::System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
			resources.ApplyResources(this.Column26, "Column26");
			this.Column26.Name = "Column26";
			resources.ApplyResources(this.dataGridViewTextBoxColumn25, "dataGridViewTextBoxColumn25");
			this.dataGridViewTextBoxColumn25.Name = "dataGridViewTextBoxColumn25";
			this.dataGridViewTextBoxColumn25.ReadOnly = true;
			this.dataGridViewTextBoxColumn25.SortMode = global::System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
			resources.ApplyResources(this.dataGridViewTextBoxColumn26, "dataGridViewTextBoxColumn26");
			this.dataGridViewTextBoxColumn26.Name = "dataGridViewTextBoxColumn26";
			this.dataGridViewTextBoxColumn26.SortMode = global::System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
			resources.ApplyResources(this.Column27, "Column27");
			this.Column27.Name = "Column27";
			resources.ApplyResources(this.label26, "label26");
			this.label26.ImeMode = global::System.Windows.Forms.ImeMode.NoControl;
			this.label26.Name = "label26";
			this.label26.Text = "Mandatory items :";
			this.RewMandatoryItemsGrid.AllowUserToAddRows = false;
			this.RewMandatoryItemsGrid.AllowUserToDeleteRows = false;
			this.RewMandatoryItemsGrid.ColumnHeadersHeightSizeMode = global::System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.RewMandatoryItemsGrid.Columns.AddRange(new global::System.Windows.Forms.DataGridViewColumn[]
			{
				this.Column10,
				this.Column11,
				this.Column14,
				this.Column12,
				this.Column13,
				this.Column23
			});
			resources.ApplyResources(this.RewMandatoryItemsGrid, "RewMandatoryItemsGrid");
			this.RewMandatoryItemsGrid.Name = "RewMandatoryItemsGrid";
			resources.ApplyResources(this.Column10, "Column10");
			this.Column10.Name = "Column10";
			this.Column10.ReadOnly = true;
			this.Column10.SortMode = global::System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
			resources.ApplyResources(this.Column11, "Column11");
			this.Column11.Name = "Column11";
			this.Column11.SortMode = global::System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
			resources.ApplyResources(this.Column14, "Column14");
			this.Column14.Name = "Column14";
			resources.ApplyResources(this.Column12, "Column12");
			this.Column12.Name = "Column12";
			this.Column12.ReadOnly = true;
			this.Column12.SortMode = global::System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
			resources.ApplyResources(this.Column13, "Column13");
			this.Column13.Name = "Column13";
			this.Column13.SortMode = global::System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
			resources.ApplyResources(this.Column23, "Column23");
			this.Column23.Name = "Column23";
			this.RewCurrencyGrid.ColumnHeadersHeightSizeMode = global::System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.RewCurrencyGrid.Columns.AddRange(new global::System.Windows.Forms.DataGridViewColumn[]
			{
				this.dataGridViewTextBoxColumn3,
				this.dataGridViewTextBoxColumn4
			});
			resources.ApplyResources(this.RewCurrencyGrid, "RewCurrencyGrid");
			this.RewCurrencyGrid.Name = "RewCurrencyGrid";
			this.RewCurrencyGrid.RowTemplate.Selected = false;
			resources.ApplyResources(this.dataGridViewTextBoxColumn3, "dataGridViewTextBoxColumn3");
			this.dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
			this.dataGridViewTextBoxColumn3.ReadOnly = true;
			resources.ApplyResources(this.dataGridViewTextBoxColumn4, "dataGridViewTextBoxColumn4");
			this.dataGridViewTextBoxColumn4.Name = "dataGridViewTextBoxColumn4";
			resources.ApplyResources(this.label35, "label35");
			this.label35.ImeMode = global::System.Windows.Forms.ImeMode.NoControl;
			this.label35.Name = "label35";
			this.label35.Text = "Currencies :";
			this.RewReputationGrid.ColumnHeadersHeightSizeMode = global::System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.RewReputationGrid.Columns.AddRange(new global::System.Windows.Forms.DataGridViewColumn[]
			{
				this.dataGridViewTextBoxColumn2,
				this.Column29
			});
			resources.ApplyResources(this.RewReputationGrid, "RewReputationGrid");
			this.RewReputationGrid.Name = "RewReputationGrid";
			this.RewReputationGrid.RowTemplate.Selected = false;
			resources.ApplyResources(this.dataGridViewTextBoxColumn2, "dataGridViewTextBoxColumn2");
			this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
			this.dataGridViewTextBoxColumn2.ReadOnly = true;
			resources.ApplyResources(this.Column29, "Column29");
			this.Column29.Name = "Column29";
			resources.ApplyResources(this.label34, "label34");
			this.label34.ImeMode = global::System.Windows.Forms.ImeMode.NoControl;
			this.label34.Name = "label34";
			this.label34.Text = "Reputations :";
			resources.ApplyResources(this.RewHonorQtyTextBox, "RewHonorQtyTextBox");
			this.RewHonorQtyTextBox.Name = "RewHonorQtyTextBox";
			resources.ApplyResources(this.label33, "label33");
			this.label33.ImeMode = global::System.Windows.Forms.ImeMode.NoControl;
			this.label33.Name = "label33";
			this.label33.Text = "Honor :";
			resources.ApplyResources(this.AutoMoneyRewardErrorLabel, "AutoMoneyRewardErrorLabel");
			this.AutoMoneyRewardErrorLabel.ForeColor = global::System.Drawing.Color.Red;
			this.AutoMoneyRewardErrorLabel.Name = "AutoMoneyRewardErrorLabel";
			this.AutoMoneyRewardErrorLabel.Text = "* invalid value!";
			this.AutoMoneyRewardCheckBox.AutoSize = true;
			this.AutoMoneyRewardCheckBox.Checked = true;
			this.AutoMoneyRewardCheckBox.CheckState = global::System.Windows.Forms.CheckState.Checked;
			resources.ApplyResources(this.AutoMoneyRewardCheckBox, "AutoMoneyRewardCheckBox");
			this.AutoMoneyRewardCheckBox.Name = "AutoMoneyRewardCheckBox";
			this.AutoMoneyRewardCheckBox.Text = "Auto Calculate Money Reward";
			this.AutoMoneyRewardCheckBox.UseVisualStyleBackColor = true;
			resources.ApplyResources(this.label25, "label25");
			this.label25.ImeMode = global::System.Windows.Forms.ImeMode.NoControl;
			this.label25.Name = "label25";
			this.label25.Text = "Recalculated experience:";
			resources.ApplyResources(this.RewExpRealQtyLabel, "RewExpRealQtyLabel");
			this.RewExpRealQtyLabel.ImeMode = global::System.Windows.Forms.ImeMode.NoControl;
			this.RewExpRealQtyLabel.Name = "RewExpRealQtyLabel";
			this.RewExpRealQtyLabel.Text = "RewExpRealQtyLabel";
			resources.ApplyResources(this.RewCopperQtyTextBox, "RewCopperQtyTextBox");
			this.RewCopperQtyTextBox.Name = "RewCopperQtyTextBox";
			resources.ApplyResources(this.label24, "label24");
			this.label24.ImeMode = global::System.Windows.Forms.ImeMode.NoControl;
			this.label24.Name = "label24";
			this.label24.Text = "Copper :";
			resources.ApplyResources(this.RewSilverQtyTextBox, "RewSilverQtyTextBox");
			this.RewSilverQtyTextBox.Name = "RewSilverQtyTextBox";
			resources.ApplyResources(this.label23, "label23");
			this.label23.ImeMode = global::System.Windows.Forms.ImeMode.NoControl;
			this.label23.Name = "label23";
			this.label23.Text = "Silver :";
			resources.ApplyResources(this.RewGoldQtyTextBox, "RewGoldQtyTextBox");
			this.RewGoldQtyTextBox.Name = "RewGoldQtyTextBox";
			resources.ApplyResources(this.label13, "label13");
			this.label13.ImeMode = global::System.Windows.Forms.ImeMode.NoControl;
			this.label13.Name = "label13";
			this.label13.Text = "Gold :";
			resources.ApplyResources(this.RewExpQtyTextBox, "RewExpQtyTextBox");
			this.RewExpQtyTextBox.Name = "RewExpQtyTextBox";
			resources.ApplyResources(this.label12, "label12");
			this.label12.ImeMode = global::System.Windows.Forms.ImeMode.NoControl;
			this.label12.Name = "label12";
			this.label12.Text = "Experience :";
			resources.ApplyResources(this.label11, "label11");
			this.label11.ImeMode = global::System.Windows.Forms.ImeMode.NoControl;
			this.label11.Name = "label11";
			this.label11.Text = "Rewards :";
			this.ChooseCompeterButton.ImeMode = global::System.Windows.Forms.ImeMode.NoControl;
			resources.ApplyResources(this.ChooseCompeterButton, "ChooseCompeterButton");
			this.ChooseCompeterButton.Name = "ChooseCompeterButton";
			this.ChooseCompeterButton.Text = " ...";
			this.ChooseCompeterButton.UseVisualStyleBackColor = true;
			this.CompleterTextBox.ContextMenuStrip = QuestFinisherMenuStrip;
			resources.ApplyResources(this.CompleterTextBox, "CompleterTextBox");
			this.CompleterTextBox.Name = "CompleterTextBox";
			this.CompleterTextBox.ReadOnly = true;
			resources.ApplyResources(this.label3, "label3");
			this.label3.ImeMode = global::System.Windows.Forms.ImeMode.NoControl;
			this.label3.Name = "label3";
			this.label3.Text = "Previous quest:";
			resources.ApplyResources(this.label28, "label28");
			this.label28.ImeMode = global::System.Windows.Forms.ImeMode.NoControl;
			this.label28.Name = "label28";
			this.label28.Text = "Restricting quest:";
			resources.ApplyResources(this.label7, "label7");
			this.label7.ImeMode = global::System.Windows.Forms.ImeMode.NoControl;
			this.label7.Name = "label7";
			this.label7.Text = "Quest Finisher :";
			resources.ApplyResources(this.label22, "label22");
			this.label22.ImeMode = global::System.Windows.Forms.ImeMode.NoControl;
			this.label22.Name = "label22";
			this.label22.Text = "Character class:";
			resources.ApplyResources(this.label5, "label5");
			this.label5.ImeMode = global::System.Windows.Forms.ImeMode.NoControl;
			this.label5.Name = "label5";
			this.label5.Text = "Player level:";
			resources.ApplyResources(this.PlayerLevelTextBox, "PlayerLevelTextBox");
			this.PlayerLevelTextBox.Name = "PlayerLevelTextBox";
			resources.ApplyResources(this.label2, "label2");
			this.label2.ImeMode = global::System.Windows.Forms.ImeMode.NoControl;
			this.label2.Name = "label2";
			this.label2.Text = "Quset Giver :";
			resources.ApplyResources(this.label19, "label19");
			this.label19.ImeMode = global::System.Windows.Forms.ImeMode.NoControl;
			this.label19.Name = "label19";
			this.label19.Text = "File path:";
			resources.ApplyResources(this.FilePathTextBox, "FilePathTextBox");
			this.FilePathTextBox.Name = "FilePathTextBox";
			this.FilePathTextBox.ReadOnly = true;
			resources.ApplyResources(this.label18, "label18");
			this.label18.ImeMode = global::System.Windows.Forms.ImeMode.NoControl;
			this.label18.Name = "label18";
			this.label18.Text = "Game name:";
			resources.ApplyResources(this.GameNameTextBox, "GameNameTextBox");
			this.GameNameTextBox.Name = "GameNameTextBox";
			resources.ApplyResources(this.label17, "label17");
			this.label17.ImeMode = global::System.Windows.Forms.ImeMode.NoControl;
			this.label17.Name = "label17";
			this.label17.Text = "Name:";
			resources.ApplyResources(this.QuestNameTextBox, "QuestNameTextBox");
			this.QuestNameTextBox.Name = "QuestNameTextBox";
			resources.ApplyResources(this.QuestLevelTextBox, "QuestLevelTextBox");
			this.QuestLevelTextBox.Name = "QuestLevelTextBox";
			resources.ApplyResources(this.label4, "label4");
			this.label4.ImeMode = global::System.Windows.Forms.ImeMode.NoControl;
			this.label4.Name = "label4";
			this.label4.Text = "Quest level:";
			this.CounterMenuStrip.BindingContext = null;
			this.CounterMenuStrip.Items.AddRange(new global::System.Windows.Forms.ToolStripItem[]
			{
				this.createItemToolStripMenuItem,
				this.toolStripMenuItem28,
				this.toolStripMenuItem29,
				this.createExploitToolStripMenuItem,
				this.toolStripMenuItem30,
				this.editGameNameToolStripMenuItem2,
				this.editPropertiesToolStripMenuItem1,
				this.toolStripMenuItem31,
				this.toolStripMenuItem17
			});
			this.CounterMenuStrip.Name = "QuetsGiverMenuStrip";
			this.CounterMenuStrip.Region = null;
			resources.ApplyResources(this.CounterMenuStrip, "CounterMenuStrip");
			this.createItemToolStripMenuItem.Name = "createItemToolStripMenuItem";
			resources.ApplyResources(this.createItemToolStripMenuItem, "createItemToolStripMenuItem");
			this.createItemToolStripMenuItem.Tag = "create_quest_item";
			this.toolStripMenuItem28.Name = "toolStripMenuItem28";
			resources.ApplyResources(this.toolStripMenuItem28, "toolStripMenuItem28");
			this.toolStripMenuItem28.Tag = "create_mob";
			this.toolStripMenuItem29.Name = "toolStripMenuItem29";
			resources.ApplyResources(this.toolStripMenuItem29, "toolStripMenuItem29");
			this.toolStripMenuItem29.Tag = "create_device";
			this.createExploitToolStripMenuItem.Name = "createExploitToolStripMenuItem";
			resources.ApplyResources(this.createExploitToolStripMenuItem, "createExploitToolStripMenuItem");
			this.createExploitToolStripMenuItem.Tag = "bind_exploit";
			this.toolStripMenuItem30.Name = "toolStripMenuItem30";
			resources.ApplyResources(this.toolStripMenuItem30, "toolStripMenuItem30");
			this.toolStripMenuItem30.Tag = "clear_cell";
			this.editGameNameToolStripMenuItem2.Name = "editGameNameToolStripMenuItem2";
			resources.ApplyResources(this.editGameNameToolStripMenuItem2, "editGameNameToolStripMenuItem2");
			this.editGameNameToolStripMenuItem2.Tag = "edit_game_name";
			this.editPropertiesToolStripMenuItem1.Name = "editPropertiesToolStripMenuItem1";
			resources.ApplyResources(this.editPropertiesToolStripMenuItem1, "editPropertiesToolStripMenuItem1");
			this.editPropertiesToolStripMenuItem1.Tag = "edit_properties";
			this.toolStripMenuItem31.Name = "toolStripMenuItem31";
			resources.ApplyResources(this.toolStripMenuItem31, "toolStripMenuItem31");
			this.toolStripMenuItem31.Tag = "select_dialog";
			this.RestrQuestMenuStrip.BindingContext = null;
			this.RestrQuestMenuStrip.Items.AddRange(new global::System.Windows.Forms.ToolStripItem[]
			{
				this.toolStripMenuItem11,
				this.toolStripMenuItem12
			});
			this.RestrQuestMenuStrip.Name = "QuetsGiverMenuStrip";
			this.RestrQuestMenuStrip.Region = null;
			resources.ApplyResources(this.RestrQuestMenuStrip, "RestrQuestMenuStrip");
			this.toolStripMenuItem11.Name = "toolStripMenuItem11";
			resources.ApplyResources(this.toolStripMenuItem11, "toolStripMenuItem11");
			this.toolStripMenuItem11.Tag = "clear_cell";
			this.toolStripMenuItem12.Name = "toolStripMenuItem12";
			resources.ApplyResources(this.toolStripMenuItem12, "toolStripMenuItem12");
			this.toolStripMenuItem12.Tag = "select_dialog";
			this.PrevQuestMenuStrip.BindingContext = null;
			this.PrevQuestMenuStrip.Items.AddRange(new global::System.Windows.Forms.ToolStripItem[]
			{
				this.toolStripMenuItem14,
				this.toolStripMenuItem15
			});
			this.PrevQuestMenuStrip.Name = "QuetsGiverMenuStrip";
			this.PrevQuestMenuStrip.Region = null;
			resources.ApplyResources(this.PrevQuestMenuStrip, "PrevQuestMenuStrip");
			this.toolStripMenuItem14.Name = "toolStripMenuItem14";
			resources.ApplyResources(this.toolStripMenuItem14, "toolStripMenuItem14");
			this.toolStripMenuItem14.Tag = "clear_cell";
			this.toolStripMenuItem15.Name = "toolStripMenuItem15";
			resources.ApplyResources(this.toolStripMenuItem15, "toolStripMenuItem15");
			this.toolStripMenuItem15.Tag = "select_dialog";
			this.AlternativeItemsMenuStrip.BindingContext = null;
			this.AlternativeItemsMenuStrip.Items.AddRange(new global::System.Windows.Forms.ToolStripItem[]
			{
				this.toolStripMenuItem25,
				this.toolStripMenuItem26,
				this.editGameNameToolStripMenuItem4,
				this.editPropertiesToolStripMenuItem4,
				this.toolStripMenuItem27
			});
			this.AlternativeItemsMenuStrip.Name = "QuetsGiverMenuStrip";
			this.AlternativeItemsMenuStrip.Region = null;
			resources.ApplyResources(this.AlternativeItemsMenuStrip, "AlternativeItemsMenuStrip");
			this.toolStripMenuItem25.Name = "toolStripMenuItem25";
			resources.ApplyResources(this.toolStripMenuItem25, "toolStripMenuItem25");
			this.toolStripMenuItem25.Tag = "create_quest_item";
			this.toolStripMenuItem26.Name = "toolStripMenuItem26";
			resources.ApplyResources(this.toolStripMenuItem26, "toolStripMenuItem26");
			this.toolStripMenuItem26.Tag = "clear_cell";
			this.editGameNameToolStripMenuItem4.Name = "editGameNameToolStripMenuItem4";
			resources.ApplyResources(this.editGameNameToolStripMenuItem4, "editGameNameToolStripMenuItem4");
			this.editGameNameToolStripMenuItem4.Tag = "edit_game_name";
			this.editPropertiesToolStripMenuItem4.Name = "editPropertiesToolStripMenuItem4";
			resources.ApplyResources(this.editPropertiesToolStripMenuItem4, "editPropertiesToolStripMenuItem4");
			this.editPropertiesToolStripMenuItem4.Tag = "edit_properties";
			this.toolStripMenuItem27.Name = "toolStripMenuItem27";
			resources.ApplyResources(this.toolStripMenuItem27, "toolStripMenuItem27");
			this.toolStripMenuItem27.Tag = "select_dialog";
			this.openSelectDialogToolStripMenuItem.Name = "openSelectDialogToolStripMenuItem";
			resources.ApplyResources(this.openSelectDialogToolStripMenuItem, "openSelectDialogToolStripMenuItem");
			this.openSelectDialogToolStripMenuItem.Tag = "select_dialog";
			this.MandatoryItemsMenuStrip.BindingContext = null;
			this.MandatoryItemsMenuStrip.Items.AddRange(new global::System.Windows.Forms.ToolStripItem[]
			{
				this.toolStripMenuItem19,
				this.toolStripMenuItem23,
				this.editGameNameToolStripMenuItem3,
				this.editPropertiesToolStripMenuItem3,
				this.toolStripMenuItem24
			});
			this.MandatoryItemsMenuStrip.Name = "QuetsGiverMenuStrip";
			this.MandatoryItemsMenuStrip.Region = null;
			resources.ApplyResources(this.MandatoryItemsMenuStrip, "MandatoryItemsMenuStrip");
			this.toolStripMenuItem19.Name = "toolStripMenuItem19";
			resources.ApplyResources(this.toolStripMenuItem19, "toolStripMenuItem19");
			this.toolStripMenuItem19.Tag = "create_quest_item";
			this.toolStripMenuItem23.Name = "toolStripMenuItem23";
			resources.ApplyResources(this.toolStripMenuItem23, "toolStripMenuItem23");
			this.toolStripMenuItem23.Tag = "clear_cell";
			this.editGameNameToolStripMenuItem3.Name = "editGameNameToolStripMenuItem3";
			resources.ApplyResources(this.editGameNameToolStripMenuItem3, "editGameNameToolStripMenuItem3");
			this.editGameNameToolStripMenuItem3.Tag = "edit_game_name";
			this.editPropertiesToolStripMenuItem3.Name = "editPropertiesToolStripMenuItem3";
			resources.ApplyResources(this.editPropertiesToolStripMenuItem3, "editPropertiesToolStripMenuItem3");
			this.editPropertiesToolStripMenuItem3.Tag = "edit_properties";
			this.toolStripMenuItem24.Name = "toolStripMenuItem24";
			resources.ApplyResources(this.toolStripMenuItem24, "toolStripMenuItem24");
			this.toolStripMenuItem24.Tag = "select_dialog";
			resources.ApplyResources(this.label16, "label16");
			this.label16.ImeMode = global::System.Windows.Forms.ImeMode.NoControl;
			this.label16.Name = "label16";
			this.label16.Text = "Zone:";
			this.SaveButton.ImeMode = global::System.Windows.Forms.ImeMode.NoControl;
			resources.ApplyResources(this.SaveButton, "SaveButton");
			this.SaveButton.Name = "SaveButton";
			this.SaveButton.Text = "Save quest";
			this.SaveButton.UseVisualStyleBackColor = true;
			this.DeleteQuestButton.ImeMode = global::System.Windows.Forms.ImeMode.NoControl;
			resources.ApplyResources(this.DeleteQuestButton, "DeleteQuestButton");
			this.DeleteQuestButton.Name = "DeleteQuestButton";
			this.DeleteQuestButton.Text = "Delete quest";
			this.DeleteQuestButton.UseVisualStyleBackColor = true;
			this.AddNewQuestButton.ImeMode = global::System.Windows.Forms.ImeMode.NoControl;
			resources.ApplyResources(this.AddNewQuestButton, "AddNewQuestButton");
			this.AddNewQuestButton.Name = "AddNewQuestButton";
			this.AddNewQuestButton.Text = "Add new quest ...";
			this.AddNewQuestButton.UseVisualStyleBackColor = true;
			resources.ApplyResources(this.MainTabControl, "MainTabControl");
			this.MainTabControl.Controls.Add(this.CommonTabPage);
			this.MainTabControl.Controls.Add(this.AdvancedTabPage);
			this.MainTabControl.Controls.Add(this.TextQuestTabPage);
			this.MainTabControl.Controls.Add(this.LootTableTabPage);
			this.MainTabControl.Controls.Add(this.MarkersTabPage);
			this.MainTabControl.Name = "MainTabControl";
			this.MainTabControl.SelectedIndex = 0;
			this.MarkersTabPage.Controls.Add(this.label29);
			this.MarkersTabPage.Controls.Add(this.EditGoalLocatorButton);
			this.MarkersTabPage.Controls.Add(this.RemoveGoalLocatorButton);
			this.MarkersTabPage.Controls.Add(this.AddGoalLocatorButton);
			this.MarkersTabPage.Controls.Add(this.GoalLocatorsListView);
			this.MarkersTabPage.Controls.Add(this.EditReturnLocatorButton);
			this.MarkersTabPage.Controls.Add(this.autoSetGoalLocatorCheckBox);
			this.MarkersTabPage.Controls.Add(this.label30);
			this.MarkersTabPage.Controls.Add(this.mapPanel);
			this.MarkersTabPage.Controls.Add(this.MarkerZoneComboBox);
			this.MarkersTabPage.Location = new global::System.Drawing.Point(4, 22);
			this.MarkersTabPage.Name = "MarkersTabPage";
			resources.ApplyResources(this.MarkersTabPage, "MarkersTabPage");
			this.MarkersTabPage.TabIndex = 5;
			this.MarkersTabPage.UseVisualStyleBackColor = true;
			resources.ApplyResources(this.label29, "label29");
			this.label29.Name = "label29";
			this.label29.Text = "goal locaotors";
			resources.ApplyResources(this.EditGoalLocatorButton, "EditGoalLocatorButton");
			this.EditGoalLocatorButton.ImeMode = global::System.Windows.Forms.ImeMode.NoControl;
			this.EditGoalLocatorButton.Name = "EditGoalLocatorButton";
			this.EditGoalLocatorButton.Text = "Edit ...";
			this.EditGoalLocatorButton.UseVisualStyleBackColor = true;
			resources.ApplyResources(this.RemoveGoalLocatorButton, "RemoveGoalLocatorButton");
			this.RemoveGoalLocatorButton.ImeMode = global::System.Windows.Forms.ImeMode.NoControl;
			this.RemoveGoalLocatorButton.Name = "RemoveGoalLocatorButton";
			this.RemoveGoalLocatorButton.Text = "Remove";
			this.RemoveGoalLocatorButton.UseVisualStyleBackColor = true;
			resources.ApplyResources(this.AddGoalLocatorButton, "AddGoalLocatorButton");
			this.AddGoalLocatorButton.ImeMode = global::System.Windows.Forms.ImeMode.NoControl;
			this.AddGoalLocatorButton.Name = "AddGoalLocatorButton";
			this.AddGoalLocatorButton.Text = "Add";
			this.AddGoalLocatorButton.UseVisualStyleBackColor = true;
			resources.ApplyResources(this.GoalLocatorsListView, "GoalLocatorsListView");
			this.GoalLocatorsListView.MultiSelect = false;
			this.GoalLocatorsListView.Name = "GoalLocatorsListView";
			this.GoalLocatorsListView.UseCompatibleStateImageBehavior = false;
			this.GoalLocatorsListView.View = global::System.Windows.Forms.View.List;
			resources.ApplyResources(this.EditReturnLocatorButton, "EditReturnLocatorButton");
			this.EditReturnLocatorButton.ImeMode = global::System.Windows.Forms.ImeMode.NoControl;
			this.EditReturnLocatorButton.Name = "EditReturnLocatorButton";
			this.EditReturnLocatorButton.Text = "Edit return locator ...";
			this.EditReturnLocatorButton.UseVisualStyleBackColor = true;
			resources.ApplyResources(this.autoSetGoalLocatorCheckBox, "autoSetGoalLocatorCheckBox");
			this.autoSetGoalLocatorCheckBox.AutoSize = true;
			this.autoSetGoalLocatorCheckBox.Name = "autoSetGoalLocatorCheckBox";
			this.autoSetGoalLocatorCheckBox.Text = "Auto Set";
			this.autoSetGoalLocatorCheckBox.UseVisualStyleBackColor = true;
			resources.ApplyResources(this.label30, "label30");
			this.label30.ImeMode = global::System.Windows.Forms.ImeMode.NoControl;
			this.label30.Name = "label30";
			this.label30.Text = "Zone:";
			resources.ApplyResources(this.mapPanel, "mapPanel");
			this.mapPanel.Name = "mapPanel";
			this.MarkerZoneComboBox.DropDownStyle = global::System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.MarkerZoneComboBox.FormattingEnabled = true;
			resources.ApplyResources(this.MarkerZoneComboBox, "MarkerZoneComboBox");
			this.MarkerZoneComboBox.Name = "MarkerZoneComboBox";
			this.MarkerZoneComboBox.Sorted = true;
			this.CloseButton.DialogResult = global::System.Windows.Forms.DialogResult.Cancel;
			this.CloseButton.ImeMode = global::System.Windows.Forms.ImeMode.NoControl;
			resources.ApplyResources(this.CloseButton, "CloseButton");
			this.CloseButton.Name = "CloseButton";
			this.CloseButton.Text = "Close";
			this.CloseButton.UseVisualStyleBackColor = true;
			resources.ApplyResources(this.dataGridViewTextBoxColumn5, "dataGridViewTextBoxColumn5");
			this.dataGridViewTextBoxColumn5.Name = "dataGridViewTextBoxColumn5";
			this.dataGridViewTextBoxColumn5.ReadOnly = true;
			resources.ApplyResources(this.dataGridViewTextBoxColumn6, "dataGridViewTextBoxColumn6");
			this.dataGridViewTextBoxColumn6.Name = "dataGridViewTextBoxColumn6";
			this.dataGridViewTextBoxColumn6.ReadOnly = true;
			resources.ApplyResources(this.dataGridViewTextBoxColumn7, "dataGridViewTextBoxColumn7");
			this.dataGridViewTextBoxColumn7.Name = "dataGridViewTextBoxColumn7";
			this.dataGridViewTextBoxColumn7.ReadOnly = true;
			resources.ApplyResources(this.dataGridViewTextBoxColumn10, "dataGridViewTextBoxColumn10");
			this.dataGridViewTextBoxColumn10.Name = "dataGridViewTextBoxColumn10";
			this.dataGridViewTextBoxColumn10.ReadOnly = true;
			resources.ApplyResources(this.dataGridViewTextBoxColumn11, "dataGridViewTextBoxColumn11");
			this.dataGridViewTextBoxColumn11.Name = "dataGridViewTextBoxColumn11";
			this.dataGridViewTextBoxColumn11.ReadOnly = true;
			resources.ApplyResources(this.dataGridViewTextBoxColumn12, "dataGridViewTextBoxColumn12");
			this.dataGridViewTextBoxColumn12.Name = "dataGridViewTextBoxColumn12";
			this.dataGridViewTextBoxColumn12.ReadOnly = true;
			resources.ApplyResources(this.dataGridViewTextBoxColumn13, "dataGridViewTextBoxColumn13");
			this.dataGridViewTextBoxColumn13.Name = "dataGridViewTextBoxColumn13";
			this.dataGridViewTextBoxColumn13.ReadOnly = true;
			resources.ApplyResources(this.dataGridViewTextBoxColumn14, "dataGridViewTextBoxColumn14");
			this.dataGridViewTextBoxColumn14.Name = "dataGridViewTextBoxColumn14";
			this.dataGridViewTextBoxColumn14.ReadOnly = true;
			resources.ApplyResources(this.dataGridViewTextBoxColumn15, "dataGridViewTextBoxColumn15");
			this.dataGridViewTextBoxColumn15.Name = "dataGridViewTextBoxColumn15";
			resources.ApplyResources(this.dataGridViewTextBoxColumn16, "dataGridViewTextBoxColumn16");
			this.dataGridViewTextBoxColumn16.Name = "dataGridViewTextBoxColumn16";
			this.dataGridViewTextBoxColumn16.ReadOnly = true;
			resources.ApplyResources(this.dataGridViewTextBoxColumn17, "dataGridViewTextBoxColumn17");
			this.dataGridViewTextBoxColumn17.Name = "dataGridViewTextBoxColumn17";
			this.dataGridViewTextBoxColumn17.ReadOnly = true;
			resources.ApplyResources(this.dataGridViewTextBoxColumn18, "dataGridViewTextBoxColumn18");
			this.dataGridViewTextBoxColumn18.Name = "dataGridViewTextBoxColumn18";
			this.dataGridViewTextBoxColumn18.ReadOnly = true;
			resources.ApplyResources(this.dataGridViewTextBoxColumn19, "dataGridViewTextBoxColumn19");
			this.dataGridViewTextBoxColumn19.Name = "dataGridViewTextBoxColumn19";
			resources.ApplyResources(this.dataGridViewTextBoxColumn20, "dataGridViewTextBoxColumn20");
			this.dataGridViewTextBoxColumn20.Name = "dataGridViewTextBoxColumn20";
			resources.ApplyResources(this.dataGridViewTextBoxColumn21, "dataGridViewTextBoxColumn21");
			this.dataGridViewTextBoxColumn21.Name = "dataGridViewTextBoxColumn21";
			resources.ApplyResources(this.Column6, "Column6");
			this.Column6.Name = "Column6";
			this.Column6.ReadOnly = true;
			resources.ApplyResources(this.Column7, "Column7");
			this.Column7.Name = "Column7";
			resources.ApplyResources(this.Column8, "Column8");
			this.Column8.Name = "Column8";
			this.Column8.ReadOnly = true;
			resources.ApplyResources(this.Column9, "Column9");
			this.Column9.Name = "Column9";
			resources.ApplyResources(this.ZoneTextBox, "ZoneTextBox");
			this.ZoneTextBox.Name = "ZoneTextBox";
			this.ZoneTextBox.ReadOnly = true;
			this.ZoneTextBox.TabStop = false;
			this.LootTableMobMenuStrip.BindingContext = null;
			this.LootTableMobMenuStrip.Items.AddRange(new global::System.Windows.Forms.ToolStripItem[]
			{
				this.createMobToolStripMenuItem,
				this.createSteleOrChestToolStripMenuItem,
				this.clearCellToolStripMenuItem,
				this.editGameNameToolStripMenuItem5,
				this.editPropertiesToolStripMenuItem5,
				this.openSelectDialogToolStripMenuItem,
				this.toolStripMenuItem16
			});
			this.LootTableMobMenuStrip.Name = "contextMenuStrip1";
			this.LootTableMobMenuStrip.Region = null;
			resources.ApplyResources(this.LootTableMobMenuStrip, "LootTableMobMenuStrip");
			this.LootTableMobMenuStrip.Tag = "";
			this.createMobToolStripMenuItem.Name = "createMobToolStripMenuItem";
			resources.ApplyResources(this.createMobToolStripMenuItem, "createMobToolStripMenuItem");
			this.createMobToolStripMenuItem.Tag = "create_mob";
			this.createSteleOrChestToolStripMenuItem.Name = "createSteleOrChestToolStripMenuItem";
			resources.ApplyResources(this.createSteleOrChestToolStripMenuItem, "createSteleOrChestToolStripMenuItem");
			this.createSteleOrChestToolStripMenuItem.Tag = "create_device";
			this.clearCellToolStripMenuItem.Name = "clearCellToolStripMenuItem";
			resources.ApplyResources(this.clearCellToolStripMenuItem, "clearCellToolStripMenuItem");
			this.clearCellToolStripMenuItem.Tag = "clear_cell";
			this.editGameNameToolStripMenuItem5.Name = "editGameNameToolStripMenuItem5";
			resources.ApplyResources(this.editGameNameToolStripMenuItem5, "editGameNameToolStripMenuItem5");
			this.editGameNameToolStripMenuItem5.Tag = "edit_game_name";
			this.editPropertiesToolStripMenuItem5.Name = "editPropertiesToolStripMenuItem5";
			resources.ApplyResources(this.editPropertiesToolStripMenuItem5, "editPropertiesToolStripMenuItem5");
			this.editPropertiesToolStripMenuItem5.Tag = "edit_properties";
			this.toolStripMenuItem16.Name = "toolStripMenuItem16";
			resources.ApplyResources(this.toolStripMenuItem16, "toolStripMenuItem16");
			this.toolStripMenuItem16.Tag = "find_on_map";
			this.LootTableItemMenuStrip.BindingContext = null;
			this.LootTableItemMenuStrip.Items.AddRange(new global::System.Windows.Forms.ToolStripItem[]
			{
				this.createItemToolStripMenuItem2,
				this.clearCellToolStripMenuItem1,
				this.editGameNameToolStripMenuItem6,
				this.editPropertiesToolStripMenuItem6,
				this.openSelectDialogToolStripMenuItem1
			});
			this.LootTableItemMenuStrip.Name = "LootTableItemMenuStrip";
			this.LootTableItemMenuStrip.Region = null;
			resources.ApplyResources(this.LootTableItemMenuStrip, "LootTableItemMenuStrip");
			this.LootTableItemMenuStrip.Tag = "create_quest_item";
			this.createItemToolStripMenuItem2.Name = "createItemToolStripMenuItem2";
			resources.ApplyResources(this.createItemToolStripMenuItem2, "createItemToolStripMenuItem2");
			this.createItemToolStripMenuItem2.Tag = "create_quest_item";
			this.clearCellToolStripMenuItem1.Name = "clearCellToolStripMenuItem1";
			resources.ApplyResources(this.clearCellToolStripMenuItem1, "clearCellToolStripMenuItem1");
			this.clearCellToolStripMenuItem1.Tag = "clear_cell";
			this.editGameNameToolStripMenuItem6.Name = "editGameNameToolStripMenuItem6";
			resources.ApplyResources(this.editGameNameToolStripMenuItem6, "editGameNameToolStripMenuItem6");
			this.editGameNameToolStripMenuItem6.Tag = "edit_game_name";
			this.editPropertiesToolStripMenuItem6.Name = "editPropertiesToolStripMenuItem6";
			resources.ApplyResources(this.editPropertiesToolStripMenuItem6, "editPropertiesToolStripMenuItem6");
			this.editPropertiesToolStripMenuItem6.Tag = "edit_properties";
			this.openSelectDialogToolStripMenuItem1.Name = "openSelectDialogToolStripMenuItem1";
			resources.ApplyResources(this.openSelectDialogToolStripMenuItem1, "openSelectDialogToolStripMenuItem1");
			this.openSelectDialogToolStripMenuItem1.Tag = "select_dialog";
			this.SaveAsButton.ImeMode = global::System.Windows.Forms.ImeMode.NoControl;
			resources.ApplyResources(this.SaveAsButton, "SaveAsButton");
			this.SaveAsButton.Name = "SaveAsButton";
			this.SaveAsButton.Text = "Save quest as...";
			this.SaveAsButton.UseVisualStyleBackColor = true;
			resources.ApplyResources(this.ShowDiagramButton, "ShowDiagramButton");
			this.ShowDiagramButton.ImeMode = global::System.Windows.Forms.ImeMode.NoControl;
			this.ShowDiagramButton.Name = "ShowDiagramButton";
			this.ShowDiagramButton.Text = "Show in diagram";
			this.ShowDiagramButton.UseVisualStyleBackColor = true;
			resources.ApplyResources(this.ShowQuestLineButton, "ShowQuestLineButton");
			this.ShowQuestLineButton.ImeMode = global::System.Windows.Forms.ImeMode.NoControl;
			this.ShowQuestLineButton.Name = "ShowQuestLineButton";
			this.ShowQuestLineButton.Text = "Show quest line";
			this.ShowQuestLineButton.UseVisualStyleBackColor = true;
			resources.ApplyResources(this.QuestTypeLabel, "QuestTypeLabel");
			this.QuestTypeLabel.ForeColor = global::System.Drawing.Color.Maroon;
			this.QuestTypeLabel.Name = "QuestTypeLabel";
			this.QuestTypeLabel.Text = "label32";
			resources.ApplyResources(this.label14, "label14");
			this.label14.Name = "label14";
			this.label14.Text = "Resource Id:";
			resources.ApplyResources(this.ResurceIdTextBox, "ResurceIdTextBox");
			this.ResurceIdTextBox.Name = "ResurceIdTextBox";
			this.ResurceIdTextBox.ReadOnly = true;
			this.ResurceIdTextBox.TabStop = false;
			this.toolStripMenuItem17.Name = "toolStripMenuItem17";
			resources.ApplyResources(this.toolStripMenuItem17, "toolStripMenuItem17");
			this.toolStripMenuItem17.Tag = "find_on_map";
			resources.ApplyResources(this, "$this");
			base.AutoScaleMode = global::System.Windows.Forms.AutoScaleMode.Font;
			base.CancelButton = this.CloseButton;
			base.Controls.Add(this.ResurceIdTextBox);
			base.Controls.Add(this.ShowQuestLineButton);
			base.Controls.Add(this.ShowDiagramButton);
			base.Controls.Add(this.ZoneTextBox);
			base.Controls.Add(this.QuestTypeLabel);
			base.Controls.Add(this.MainTabControl);
			base.Controls.Add(this.AddNewQuestButton);
			base.Controls.Add(this.CloseButton);
			base.Controls.Add(this.DeleteQuestButton);
			base.Controls.Add(this.ChooseQuestButton);
			base.Controls.Add(this.SaveButton);
			base.Controls.Add(this.SaveAsButton);
			base.Controls.Add(this.label14);
			base.Controls.Add(this.label16);
			base.Name = "QuestEditorForm";
			this.Text = "Quest Editor";
			QuestFinisherMenuStrip.ResumeLayout(false);
			this.LootTableTabPage.ResumeLayout(false);
			((global::System.ComponentModel.ISupportInitialize)this.LootTableGrid).EndInit();
			this.TextQuestTabPage.ResumeLayout(false);
			this.TextQuestTabPage.PerformLayout();
			this.AdvancedTabPage.ResumeLayout(false);
			this.AdvancedTabPage.PerformLayout();
			this.CommonTabPage.ResumeLayout(false);
			this.CommonTabPage.PerformLayout();
			this.QuestGiverMenuStrip.ResumeLayout(false);
			((global::System.ComponentModel.ISupportInitialize)this.CounterGrid).EndInit();
			((global::System.ComponentModel.ISupportInitialize)this.NotStartedQuestGrid).EndInit();
			((global::System.ComponentModel.ISupportInitialize)this.FinishedQuestGrid).EndInit();
			this.panel2.ResumeLayout(false);
			this.panel2.PerformLayout();
			((global::System.ComponentModel.ISupportInitialize)this.RewAlternativeItemsGrid).EndInit();
			((global::System.ComponentModel.ISupportInitialize)this.RewMandatoryItemsGrid).EndInit();
			((global::System.ComponentModel.ISupportInitialize)this.RewCurrencyGrid).EndInit();
			((global::System.ComponentModel.ISupportInitialize)this.RewReputationGrid).EndInit();
			this.CounterMenuStrip.ResumeLayout(false);
			this.RestrQuestMenuStrip.ResumeLayout(false);
			this.PrevQuestMenuStrip.ResumeLayout(false);
			this.AlternativeItemsMenuStrip.ResumeLayout(false);
			this.MandatoryItemsMenuStrip.ResumeLayout(false);
			this.MainTabControl.ResumeLayout(false);
			this.MarkersTabPage.ResumeLayout(false);
			this.MarkersTabPage.PerformLayout();
			this.LootTableMobMenuStrip.ResumeLayout(false);
			this.LootTableItemMenuStrip.ResumeLayout(false);
			base.ResumeLayout(false);
			base.PerformLayout();
		}

		// Token: 0x04000890 RID: 2192
		private global::System.ComponentModel.IContainer components;

		// Token: 0x04000891 RID: 2193
		private global::System.Windows.Forms.Button ChooseQuestButton;

		// Token: 0x04000892 RID: 2194
		private global::System.Windows.Forms.TabPage LootTableTabPage;

		// Token: 0x04000893 RID: 2195
		private global::System.Windows.Forms.TabPage TextQuestTabPage;

		// Token: 0x04000894 RID: 2196
		private global::System.Windows.Forms.Label label10;

		// Token: 0x04000895 RID: 2197
		private global::System.Windows.Forms.RichTextBox NotCompletedTextBox;

		// Token: 0x04000896 RID: 2198
		private global::System.Windows.Forms.TabPage AdvancedTabPage;

		// Token: 0x04000897 RID: 2199
		private global::System.Windows.Forms.TabPage CommonTabPage;

		// Token: 0x04000898 RID: 2200
		private global::System.Windows.Forms.Label label19;

		// Token: 0x04000899 RID: 2201
		private global::System.Windows.Forms.TextBox FilePathTextBox;

		// Token: 0x0400089A RID: 2202
		private global::System.Windows.Forms.Label label18;

		// Token: 0x0400089B RID: 2203
		private global::System.Windows.Forms.TextBox GameNameTextBox;

		// Token: 0x0400089C RID: 2204
		private global::System.Windows.Forms.Label label17;

		// Token: 0x0400089D RID: 2205
		private global::System.Windows.Forms.TextBox QuestNameTextBox;

		// Token: 0x0400089E RID: 2206
		private global::System.Windows.Forms.Label label16;

		// Token: 0x0400089F RID: 2207
		private global::System.Windows.Forms.TextBox QuestLevelTextBox;

		// Token: 0x040008A0 RID: 2208
		private global::System.Windows.Forms.Label label4;

		// Token: 0x040008A1 RID: 2209
		private global::System.Windows.Forms.TabControl MainTabControl;

		// Token: 0x040008A2 RID: 2210
		private global::System.Windows.Forms.Button DeleteQuestButton;

		// Token: 0x040008A3 RID: 2211
		private global::System.Windows.Forms.Button AddNewQuestButton;

		// Token: 0x040008A4 RID: 2212
		private global::System.Windows.Forms.DataGridView LootTableGrid;

		// Token: 0x040008A5 RID: 2213
		private global::System.Windows.Forms.Button SaveButton;

		// Token: 0x040008A6 RID: 2214
		private global::System.Windows.Forms.Label label21;

		// Token: 0x040008A7 RID: 2215
		private global::System.Windows.Forms.RichTextBox KickTextBox;

		// Token: 0x040008A8 RID: 2216
		private global::System.Windows.Forms.Button CloseButton;

		// Token: 0x040008A9 RID: 2217
		private global::System.Windows.Forms.Label label28;

		// Token: 0x040008AA RID: 2218
		private global::System.Windows.Forms.Label label22;

		// Token: 0x040008AB RID: 2219
		private global::System.Windows.Forms.Label label5;

		// Token: 0x040008AC RID: 2220
		private global::System.Windows.Forms.TextBox PlayerLevelTextBox;

		// Token: 0x040008AD RID: 2221
		private global::System.Windows.Forms.Label label3;

		// Token: 0x040008AE RID: 2222
		private global::System.Windows.Forms.Label label2;

		// Token: 0x040008AF RID: 2223
		private global::MapEditor.Forms.PropertyControl.ExtendedPropertyControl.ExtendedPropertyControl QuestPropertyControl;

		// Token: 0x040008B0 RID: 2224
		private global::System.Windows.Forms.Label label1;

		// Token: 0x040008B1 RID: 2225
		private global::System.Windows.Forms.Panel panel2;

		// Token: 0x040008B2 RID: 2226
		private global::System.Windows.Forms.Label label25;

		// Token: 0x040008B3 RID: 2227
		private global::System.Windows.Forms.Label RewExpRealQtyLabel;

		// Token: 0x040008B4 RID: 2228
		private global::System.Windows.Forms.TextBox RewCopperQtyTextBox;

		// Token: 0x040008B5 RID: 2229
		private global::System.Windows.Forms.Label label24;

		// Token: 0x040008B6 RID: 2230
		private global::System.Windows.Forms.TextBox RewSilverQtyTextBox;

		// Token: 0x040008B7 RID: 2231
		private global::System.Windows.Forms.Label label23;

		// Token: 0x040008B8 RID: 2232
		private global::System.Windows.Forms.TextBox RewGoldQtyTextBox;

		// Token: 0x040008B9 RID: 2233
		private global::System.Windows.Forms.Label label13;

		// Token: 0x040008BA RID: 2234
		private global::System.Windows.Forms.TextBox RewExpQtyTextBox;

		// Token: 0x040008BB RID: 2235
		private global::System.Windows.Forms.Label label12;

		// Token: 0x040008BC RID: 2236
		private global::System.Windows.Forms.Label label11;

		// Token: 0x040008BD RID: 2237
		private global::System.Windows.Forms.Button ChooseCompeterButton;

		// Token: 0x040008BE RID: 2238
		private global::System.Windows.Forms.TextBox CompleterTextBox;

		// Token: 0x040008BF RID: 2239
		private global::System.Windows.Forms.Label label7;

		// Token: 0x040008C0 RID: 2240
		private global::System.Windows.Forms.Label label15;

		// Token: 0x040008C1 RID: 2241
		private global::System.Windows.Forms.RichTextBox CompletedTextBox;

		// Token: 0x040008C2 RID: 2242
		private global::System.Windows.Forms.RichTextBox GoalTextBox;

		// Token: 0x040008C3 RID: 2243
		private global::System.Windows.Forms.Label label20;

		// Token: 0x040008C4 RID: 2244
		private global::System.Windows.Forms.Label label6;

		// Token: 0x040008C5 RID: 2245
		private global::System.Windows.Forms.RichTextBox IssueTextBox;

		// Token: 0x040008C6 RID: 2246
		private global::System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn13;

		// Token: 0x040008C7 RID: 2247
		private global::System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn14;

		// Token: 0x040008C8 RID: 2248
		private global::System.Windows.Forms.DataGridViewTextBoxColumn Column8;

		// Token: 0x040008C9 RID: 2249
		private global::System.Windows.Forms.DataGridViewTextBoxColumn Column9;

		// Token: 0x040008CA RID: 2250
		private global::System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn10;

		// Token: 0x040008CB RID: 2251
		private global::System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn11;

		// Token: 0x040008CC RID: 2252
		private global::System.Windows.Forms.DataGridViewTextBoxColumn Column6;

		// Token: 0x040008CD RID: 2253
		private global::System.Windows.Forms.DataGridViewTextBoxColumn Column7;

		// Token: 0x040008CE RID: 2254
		private global::System.Windows.Forms.DataGridView FinishedQuestGrid;

		// Token: 0x040008CF RID: 2255
		private global::System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn5;

		// Token: 0x040008D0 RID: 2256
		private global::System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn6;

		// Token: 0x040008D1 RID: 2257
		private global::System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn7;

		// Token: 0x040008D2 RID: 2258
		private global::System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn12;

		// Token: 0x040008D3 RID: 2259
		private global::System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn15;

		// Token: 0x040008D4 RID: 2260
		private global::System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn16;

		// Token: 0x040008D5 RID: 2261
		private global::System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn17;

		// Token: 0x040008D6 RID: 2262
		private global::System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn18;

		// Token: 0x040008D7 RID: 2263
		private global::System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn19;

		// Token: 0x040008D8 RID: 2264
		private global::System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn20;

		// Token: 0x040008D9 RID: 2265
		private global::System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn21;

		// Token: 0x040008DA RID: 2266
		public global::System.Windows.Forms.DataGridView NotStartedQuestGrid;

		// Token: 0x040008DB RID: 2267
		private global::System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn8;

		// Token: 0x040008DC RID: 2268
		private global::System.Windows.Forms.DataGridViewTextBoxColumn Column1;

		// Token: 0x040008DD RID: 2269
		private global::System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn9;

		// Token: 0x040008DE RID: 2270
		private global::System.Windows.Forms.DataGridViewTextBoxColumn Column2;

		// Token: 0x040008DF RID: 2271
		private global::System.Windows.Forms.DataGridViewTextBoxColumn Column3;

		// Token: 0x040008E0 RID: 2272
		private global::System.Windows.Forms.DataGridViewCheckBoxColumn Column5;

		// Token: 0x040008E1 RID: 2273
		private global::System.Windows.Forms.TextBox ZoneTextBox;

		// Token: 0x040008E2 RID: 2274
		private global::System.Windows.Forms.ComboBox CharacterClassComboBox;

		// Token: 0x040008E3 RID: 2275
		private global::System.Windows.Forms.ContextMenuStrip QuestGiverMenuStrip;

		// Token: 0x040008E4 RID: 2276
		private global::System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;

		// Token: 0x040008E5 RID: 2277
		private global::System.Windows.Forms.ToolStripMenuItem toolStripMenuItem2;

		// Token: 0x040008E6 RID: 2278
		private global::System.Windows.Forms.ToolStripMenuItem toolStripMenuItem3;

		// Token: 0x040008E7 RID: 2279
		private global::System.Windows.Forms.ToolStripMenuItem toolStripMenuItem4;

		// Token: 0x040008E8 RID: 2280
		private global::System.Windows.Forms.ToolStripMenuItem toolStripMenuItem5;

		// Token: 0x040008E9 RID: 2281
		private global::System.Windows.Forms.ToolStripMenuItem toolStripMenuItem6;

		// Token: 0x040008EA RID: 2282
		private global::System.Windows.Forms.ToolStripMenuItem toolStripMenuItem7;

		// Token: 0x040008EB RID: 2283
		private global::System.Windows.Forms.ToolStripMenuItem toolStripMenuItem8;

		// Token: 0x040008EC RID: 2284
		private global::System.Windows.Forms.ToolStripMenuItem toolStripMenuItem9;

		// Token: 0x040008ED RID: 2285
		private global::System.Windows.Forms.ToolStripMenuItem toolStripMenuItem10;

		// Token: 0x040008EE RID: 2286
		private global::System.Windows.Forms.ContextMenuStrip RestrQuestMenuStrip;

		// Token: 0x040008EF RID: 2287
		private global::System.Windows.Forms.ToolStripMenuItem toolStripMenuItem11;

		// Token: 0x040008F0 RID: 2288
		private global::System.Windows.Forms.ToolStripMenuItem toolStripMenuItem12;

		// Token: 0x040008F1 RID: 2289
		private global::System.Windows.Forms.ContextMenuStrip PrevQuestMenuStrip;

		// Token: 0x040008F2 RID: 2290
		private global::System.Windows.Forms.ToolStripMenuItem toolStripMenuItem14;

		// Token: 0x040008F3 RID: 2291
		private global::System.Windows.Forms.ToolStripMenuItem toolStripMenuItem15;

		// Token: 0x040008F4 RID: 2292
		private global::System.Windows.Forms.ContextMenuStrip MandatoryItemsMenuStrip;

		// Token: 0x040008F5 RID: 2293
		private global::System.Windows.Forms.ToolStripMenuItem toolStripMenuItem19;

		// Token: 0x040008F6 RID: 2294
		private global::System.Windows.Forms.ToolStripMenuItem toolStripMenuItem23;

		// Token: 0x040008F7 RID: 2295
		private global::System.Windows.Forms.ToolStripMenuItem toolStripMenuItem24;

		// Token: 0x040008F8 RID: 2296
		private global::System.Windows.Forms.ContextMenuStrip AlternativeItemsMenuStrip;

		// Token: 0x040008F9 RID: 2297
		private global::System.Windows.Forms.ToolStripMenuItem toolStripMenuItem25;

		// Token: 0x040008FA RID: 2298
		private global::System.Windows.Forms.ToolStripMenuItem toolStripMenuItem26;

		// Token: 0x040008FB RID: 2299
		private global::System.Windows.Forms.ToolStripMenuItem toolStripMenuItem27;

		// Token: 0x040008FC RID: 2300
		private global::System.Windows.Forms.DataGridView CounterGrid;

		// Token: 0x040008FD RID: 2301
		public global::System.Windows.Forms.ContextMenuStrip CounterMenuStrip;

		// Token: 0x040008FE RID: 2302
		private global::System.Windows.Forms.ToolStripMenuItem toolStripMenuItem28;

		// Token: 0x040008FF RID: 2303
		private global::System.Windows.Forms.ToolStripMenuItem toolStripMenuItem29;

		// Token: 0x04000900 RID: 2304
		private global::System.Windows.Forms.ToolStripMenuItem toolStripMenuItem30;

		// Token: 0x04000901 RID: 2305
		private global::System.Windows.Forms.ToolStripMenuItem toolStripMenuItem31;

		// Token: 0x04000902 RID: 2306
		private global::System.Windows.Forms.ToolStripMenuItem createItemToolStripMenuItem;

		// Token: 0x04000903 RID: 2307
		private global::System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn22;

		// Token: 0x04000904 RID: 2308
		private global::System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;

		// Token: 0x04000905 RID: 2309
		private global::System.Windows.Forms.Label label8;

		// Token: 0x04000906 RID: 2310
		private global::System.Windows.Forms.ToolStripMenuItem editGameNameToolStripMenuItem;

		// Token: 0x04000907 RID: 2311
		private global::System.Windows.Forms.ToolStripMenuItem editGameNameToolStripMenuItem1;

		// Token: 0x04000908 RID: 2312
		private global::System.Windows.Forms.ToolStripMenuItem editGameNameToolStripMenuItem2;

		// Token: 0x04000909 RID: 2313
		private global::System.Windows.Forms.ToolStripMenuItem editGameNameToolStripMenuItem4;

		// Token: 0x0400090A RID: 2314
		private global::System.Windows.Forms.ToolStripMenuItem editGameNameToolStripMenuItem3;

		// Token: 0x0400090B RID: 2315
		private global::System.Windows.Forms.ToolStripMenuItem editPropertiesToolStripMenuItem;

		// Token: 0x0400090C RID: 2316
		private global::System.Windows.Forms.ToolStripMenuItem editPropertiesToolStripMenuItem2;

		// Token: 0x0400090D RID: 2317
		private global::System.Windows.Forms.ToolStripMenuItem editPropertiesToolStripMenuItem1;

		// Token: 0x0400090E RID: 2318
		private global::System.Windows.Forms.ToolStripMenuItem editPropertiesToolStripMenuItem3;

		// Token: 0x0400090F RID: 2319
		private global::System.Windows.Forms.ToolStripMenuItem editPropertiesToolStripMenuItem4;

		// Token: 0x04000910 RID: 2320
		private global::System.Windows.Forms.Button ChooseQuestGiverButton;

		// Token: 0x04000911 RID: 2321
		private global::System.Windows.Forms.TextBox QuestGiverTextBox;

		// Token: 0x04000912 RID: 2322
		private global::System.Windows.Forms.ContextMenuStrip LootTableMobMenuStrip;

		// Token: 0x04000913 RID: 2323
		private global::System.Windows.Forms.ToolStripMenuItem createMobToolStripMenuItem;

		// Token: 0x04000914 RID: 2324
		private global::System.Windows.Forms.ToolStripMenuItem createSteleOrChestToolStripMenuItem;

		// Token: 0x04000915 RID: 2325
		private global::System.Windows.Forms.ToolStripMenuItem clearCellToolStripMenuItem;

		// Token: 0x04000916 RID: 2326
		private global::System.Windows.Forms.ToolStripMenuItem editGameNameToolStripMenuItem5;

		// Token: 0x04000917 RID: 2327
		private global::System.Windows.Forms.ToolStripMenuItem editPropertiesToolStripMenuItem5;

		// Token: 0x04000918 RID: 2328
		private global::System.Windows.Forms.ToolStripMenuItem openSelectDialogToolStripMenuItem;

		// Token: 0x04000919 RID: 2329
		private global::System.Windows.Forms.ContextMenuStrip LootTableItemMenuStrip;

		// Token: 0x0400091A RID: 2330
		private global::System.Windows.Forms.ToolStripMenuItem createItemToolStripMenuItem2;

		// Token: 0x0400091B RID: 2331
		private global::System.Windows.Forms.ToolStripMenuItem clearCellToolStripMenuItem1;

		// Token: 0x0400091C RID: 2332
		private global::System.Windows.Forms.ToolStripMenuItem editGameNameToolStripMenuItem6;

		// Token: 0x0400091D RID: 2333
		private global::System.Windows.Forms.ToolStripMenuItem editPropertiesToolStripMenuItem6;

		// Token: 0x0400091E RID: 2334
		private global::System.Windows.Forms.ToolStripMenuItem openSelectDialogToolStripMenuItem1;

		// Token: 0x0400091F RID: 2335
		private global::System.Windows.Forms.TextBox PlotLineTextBox;

		// Token: 0x04000920 RID: 2336
		private global::System.Windows.Forms.Label label9;

		// Token: 0x04000921 RID: 2337
		private global::System.Windows.Forms.CheckBox AutoMoneyRewardCheckBox;

		// Token: 0x04000922 RID: 2338
		private global::System.Windows.Forms.Label AutoMoneyRewardErrorLabel;

		// Token: 0x04000923 RID: 2339
		private global::System.Windows.Forms.Button SaveAsButton;

		// Token: 0x04000924 RID: 2340
		private global::System.Windows.Forms.ToolStripMenuItem createExploitToolStripMenuItem;

		// Token: 0x04000925 RID: 2341
		private global::System.Windows.Forms.Button ShowDiagramButton;

		// Token: 0x04000926 RID: 2342
		private global::System.Windows.Forms.TabPage MarkersTabPage;

		// Token: 0x04000927 RID: 2343
		private global::System.Windows.Forms.ComboBox MarkerZoneComboBox;

		// Token: 0x04000928 RID: 2344
		private global::System.Windows.Forms.Panel mapPanel;

		// Token: 0x04000929 RID: 2345
		private global::System.Windows.Forms.Label label30;

		// Token: 0x0400092A RID: 2346
		private global::System.Windows.Forms.CheckBox autoSetGoalLocatorCheckBox;

		// Token: 0x0400092B RID: 2347
		private global::System.Windows.Forms.Button EditReturnLocatorButton;

		// Token: 0x0400092C RID: 2348
		private global::System.Windows.Forms.Label label29;

		// Token: 0x0400092D RID: 2349
		private global::System.Windows.Forms.Button ShowQuestLineButton;

		// Token: 0x0400092E RID: 2350
		private global::System.Windows.Forms.Label QuestTypeLabel;

		// Token: 0x0400092F RID: 2351
		private global::System.Windows.Forms.CheckBox TutorialCheckBox;

		// Token: 0x04000930 RID: 2352
		private global::System.Windows.Forms.CheckBox PVPCheckBox;

		// Token: 0x04000931 RID: 2353
		private global::System.Windows.Forms.CheckBox SecretSequenceCheckBox;

		// Token: 0x04000932 RID: 2354
		private global::System.Windows.Forms.ComboBox QuestTypeComboBox;

		// Token: 0x04000933 RID: 2355
		private global::System.Windows.Forms.Label label32;

		// Token: 0x04000934 RID: 2356
		private global::System.Windows.Forms.DataGridViewTextBoxColumn Column4;

		// Token: 0x04000935 RID: 2357
		private global::System.Windows.Forms.DataGridViewComboBoxColumn Column16;

		// Token: 0x04000936 RID: 2358
		private global::System.Windows.Forms.DataGridViewTextBoxColumn Column17;

		// Token: 0x04000937 RID: 2359
		private global::System.Windows.Forms.DataGridViewTextBoxColumn Column18;

		// Token: 0x04000938 RID: 2360
		private global::System.Windows.Forms.DataGridViewCheckBoxColumn Column19;

		// Token: 0x04000939 RID: 2361
		private global::System.Windows.Forms.DataGridViewCheckBoxColumn Column15;

		// Token: 0x0400093A RID: 2362
		private global::System.Windows.Forms.DataGridViewCheckBoxColumn Column28;

		// Token: 0x0400093B RID: 2363
		private global::System.Windows.Forms.DataGridViewTextBoxColumn Column24;

		// Token: 0x0400093C RID: 2364
		private global::System.Windows.Forms.DataGridViewTextBoxColumn Column25;

		// Token: 0x0400093D RID: 2365
		private global::System.Windows.Forms.DataGridViewTextBoxColumn Column20;

		// Token: 0x0400093E RID: 2366
		private global::System.Windows.Forms.DataGridViewTextBoxColumn Column21;

		// Token: 0x0400093F RID: 2367
		private global::System.Windows.Forms.DataGridViewTextBoxColumn Column22;

		// Token: 0x04000940 RID: 2368
		private global::System.Windows.Forms.TextBox RewHonorQtyTextBox;

		// Token: 0x04000941 RID: 2369
		private global::System.Windows.Forms.Label label33;

		// Token: 0x04000942 RID: 2370
		private global::System.Windows.Forms.DataGridView RewReputationGrid;

		// Token: 0x04000943 RID: 2371
		private global::System.Windows.Forms.Label label34;

		// Token: 0x04000944 RID: 2372
		private global::System.Windows.Forms.DataGridView RewCurrencyGrid;

		// Token: 0x04000945 RID: 2373
		private global::System.Windows.Forms.Label label35;

		// Token: 0x04000946 RID: 2374
		private global::System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;

		// Token: 0x04000947 RID: 2375
		private global::System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn4;

		// Token: 0x04000948 RID: 2376
		private global::System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;

		// Token: 0x04000949 RID: 2377
		private global::System.Windows.Forms.DataGridViewTextBoxColumn Column29;

		// Token: 0x0400094A RID: 2378
		private global::System.Windows.Forms.Label label27;

		// Token: 0x0400094B RID: 2379
		private global::System.Windows.Forms.DataGridView RewAlternativeItemsGrid;

		// Token: 0x0400094C RID: 2380
		private global::System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn23;

		// Token: 0x0400094D RID: 2381
		private global::System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn24;

		// Token: 0x0400094E RID: 2382
		private global::System.Windows.Forms.DataGridViewCheckBoxColumn Column26;

		// Token: 0x0400094F RID: 2383
		private global::System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn25;

		// Token: 0x04000950 RID: 2384
		private global::System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn26;

		// Token: 0x04000951 RID: 2385
		private global::System.Windows.Forms.DataGridViewCheckBoxColumn Column27;

		// Token: 0x04000952 RID: 2386
		private global::System.Windows.Forms.Label label26;

		// Token: 0x04000953 RID: 2387
		private global::System.Windows.Forms.DataGridView RewMandatoryItemsGrid;

		// Token: 0x04000954 RID: 2388
		private global::System.Windows.Forms.DataGridViewTextBoxColumn Column10;

		// Token: 0x04000955 RID: 2389
		private global::System.Windows.Forms.DataGridViewTextBoxColumn Column11;

		// Token: 0x04000956 RID: 2390
		private global::System.Windows.Forms.DataGridViewCheckBoxColumn Column14;

		// Token: 0x04000957 RID: 2391
		private global::System.Windows.Forms.DataGridViewTextBoxColumn Column12;

		// Token: 0x04000958 RID: 2392
		private global::System.Windows.Forms.DataGridViewTextBoxColumn Column13;

		// Token: 0x04000959 RID: 2393
		private global::System.Windows.Forms.DataGridViewCheckBoxColumn Column23;

		// Token: 0x0400095A RID: 2394
		private global::System.Windows.Forms.ListView GoalLocatorsListView;

		// Token: 0x0400095B RID: 2395
		private global::System.Windows.Forms.Button RemoveGoalLocatorButton;

		// Token: 0x0400095C RID: 2396
		private global::System.Windows.Forms.Button AddGoalLocatorButton;

		// Token: 0x0400095D RID: 2397
		private global::System.Windows.Forms.Button EditGoalLocatorButton;

		// Token: 0x0400095E RID: 2398
		private global::System.Windows.Forms.Label label14;

		// Token: 0x0400095F RID: 2399
		private global::System.Windows.Forms.TextBox ResurceIdTextBox;

		// Token: 0x04000960 RID: 2400
		private global::System.Windows.Forms.ToolStripMenuItem findOnMapToolStripMenuItem;

		// Token: 0x04000961 RID: 2401
		private global::System.Windows.Forms.ToolStripMenuItem toolStripMenuItem13;

		// Token: 0x04000962 RID: 2402
		private global::System.Windows.Forms.ToolStripMenuItem toolStripMenuItem16;

		// Token: 0x04000963 RID: 2403
		private global::System.Windows.Forms.ToolStripMenuItem toolStripMenuItem17;
	}
}
