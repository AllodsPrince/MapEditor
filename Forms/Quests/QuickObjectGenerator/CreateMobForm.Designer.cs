namespace MapEditor.Forms.Quests.QuickObjectGenerator
{
	// Token: 0x02000217 RID: 535
	public partial class CreateMobForm : global::MapEditor.Forms.Base.BaseForm
	{
		// Token: 0x060019FB RID: 6651 RVA: 0x000A935B File Offset: 0x000A835B
		protected override void Dispose(bool disposing)
		{
			if (disposing && this.components != null)
			{
				this.components.Dispose();
			}
			base.Dispose(disposing);
		}

		// Token: 0x060019FC RID: 6652 RVA: 0x000A937C File Offset: 0x000A837C
		private void InitializeComponent()
		{
			this.components = new global::System.ComponentModel.Container();
			global::System.ComponentModel.ComponentResourceManager resources = new global::System.ComponentModel.ComponentResourceManager(typeof(global::MapEditor.Forms.Quests.QuickObjectGenerator.CreateMobForm));
			this.MinLevelTextBox = new global::System.Windows.Forms.TextBox();
			this.SpeedTextBox = new global::System.Windows.Forms.TextBox();
			this.label8 = new global::System.Windows.Forms.Label();
			this.label7 = new global::System.Windows.Forms.Label();
			this.TitleTextBox = new global::System.Windows.Forms.TextBox();
			this.label6 = new global::System.Windows.Forms.Label();
			this.NameTextBox = new global::System.Windows.Forms.TextBox();
			this.label4 = new global::System.Windows.Forms.Label();
			this.label5 = new global::System.Windows.Forms.Label();
			this.CreateButton = new global::System.Windows.Forms.Button();
			this.ZoneComboBox = new global::System.Windows.Forms.ComboBox();
			this.FileNameTextBox = new global::System.Windows.Forms.TextBox();
			this.label2 = new global::System.Windows.Forms.Label();
			this.label1 = new global::System.Windows.Forms.Label();
			this.label3 = new global::System.Windows.Forms.Label();
			this.CreatureComboBox = new global::System.Windows.Forms.ComboBox();
			this.MaxLevelTextBox = new global::System.Windows.Forms.TextBox();
			this.label9 = new global::System.Windows.Forms.Label();
			this.label10 = new global::System.Windows.Forms.Label();
			this.MainTabControl = new global::System.Windows.Forms.TabControl();
			this.CommonTabPage = new global::System.Windows.Forms.TabPage();
			this.EditCreatureListButton = new global::System.Windows.Forms.Button();
			this.SpawnTableMapPanel = new global::System.Windows.Forms.Panel();
			this.MapComboBox = new global::System.Windows.Forms.ComboBox();
			this.label32 = new global::System.Windows.Forms.Label();
			this.panel5 = new global::System.Windows.Forms.Panel();
			this.ExistingSTRadioButton = new global::System.Windows.Forms.RadioButton();
			this.NewSTRadioButton = new global::System.Windows.Forms.RadioButton();
			this.panel4 = new global::System.Windows.Forms.Panel();
			this.NewVMRadioButton = new global::System.Windows.Forms.RadioButton();
			this.ExistingVMRadioButton = new global::System.Windows.Forms.RadioButton();
			this.panel1 = new global::System.Windows.Forms.Panel();
			this.WildRadioButton = new global::System.Windows.Forms.RadioButton();
			this.NeutralRadioButton = new global::System.Windows.Forms.RadioButton();
			this.SpawnTablePanel = new global::System.Windows.Forms.Panel();
			this.label33 = new global::System.Windows.Forms.Label();
			this.SpawnTableTextBox = new global::System.Windows.Forms.TextBox();
			this.ChooseSpawnTablebButton = new global::System.Windows.Forms.Button();
			this.label13 = new global::System.Windows.Forms.Label();
			this.NewVisMobParamsPanel = new global::System.Windows.Forms.Panel();
			this.VMScaleTextBox = new global::System.Windows.Forms.TextBox();
			this.label12 = new global::System.Windows.Forms.Label();
			this.ChooseVisMobButton = new global::System.Windows.Forms.Button();
			this.VisMobTextBox = new global::System.Windows.Forms.TextBox();
			this.AdditionalTabPage = new global::System.Windows.Forms.TabPage();
			this.label11 = new global::System.Windows.Forms.Label();
			this.AddFileNameTextBox = new global::System.Windows.Forms.TextBox();
			this.panel6 = new global::System.Windows.Forms.Panel();
			this.AddNewVMRadioButton = new global::System.Windows.Forms.RadioButton();
			this.AddExistingVMRadioButton = new global::System.Windows.Forms.RadioButton();
			this.AddCreateButton = new global::System.Windows.Forms.Button();
			this.AddSpawnTablePanel = new global::System.Windows.Forms.Panel();
			this.AddSpawnTableTextBox = new global::System.Windows.Forms.TextBox();
			this.AddChooseSpawnTablebButton = new global::System.Windows.Forms.Button();
			this.label31 = new global::System.Windows.Forms.Label();
			this.AddChooseVisMobButton = new global::System.Windows.Forms.Button();
			this.AddNewVisMobParamsPanel = new global::System.Windows.Forms.Panel();
			this.AddVMScaleTextBox = new global::System.Windows.Forms.TextBox();
			this.label15 = new global::System.Windows.Forms.Label();
			this.AddVisMobTextBox = new global::System.Windows.Forms.TextBox();
			this.label17 = new global::System.Windows.Forms.Label();
			this.ChooseSourceMobButton = new global::System.Windows.Forms.Button();
			this.SourceMobTextBox = new global::System.Windows.Forms.TextBox();
			this.label14 = new global::System.Windows.Forms.Label();
			this.groupBox4 = new global::System.Windows.Forms.GroupBox();
			this.CreateMiniBossRadioButton = new global::System.Windows.Forms.RadioButton();
			this.CreateSilverRadioButton = new global::System.Windows.Forms.RadioButton();
			this.panel2 = new global::System.Windows.Forms.Panel();
			this.textBox6 = new global::System.Windows.Forms.TextBox();
			this.button1 = new global::System.Windows.Forms.Button();
			this.label18 = new global::System.Windows.Forms.Label();
			this.groupBox6 = new global::System.Windows.Forms.GroupBox();
			this.radioButton3 = new global::System.Windows.Forms.RadioButton();
			this.radioButton4 = new global::System.Windows.Forms.RadioButton();
			this.groupBox7 = new global::System.Windows.Forms.GroupBox();
			this.radioButton5 = new global::System.Windows.Forms.RadioButton();
			this.radioButton6 = new global::System.Windows.Forms.RadioButton();
			this.groupBox8 = new global::System.Windows.Forms.GroupBox();
			this.radioButton7 = new global::System.Windows.Forms.RadioButton();
			this.radioButton8 = new global::System.Windows.Forms.RadioButton();
			this.panel3 = new global::System.Windows.Forms.Panel();
			this.textBox7 = new global::System.Windows.Forms.TextBox();
			this.label19 = new global::System.Windows.Forms.Label();
			this.textBox8 = new global::System.Windows.Forms.TextBox();
			this.label20 = new global::System.Windows.Forms.Label();
			this.button2 = new global::System.Windows.Forms.Button();
			this.textBox9 = new global::System.Windows.Forms.TextBox();
			this.comboBox1 = new global::System.Windows.Forms.ComboBox();
			this.label21 = new global::System.Windows.Forms.Label();
			this.label22 = new global::System.Windows.Forms.Label();
			this.label23 = new global::System.Windows.Forms.Label();
			this.textBox10 = new global::System.Windows.Forms.TextBox();
			this.textBox11 = new global::System.Windows.Forms.TextBox();
			this.button3 = new global::System.Windows.Forms.Button();
			this.label24 = new global::System.Windows.Forms.Label();
			this.label25 = new global::System.Windows.Forms.Label();
			this.comboBox2 = new global::System.Windows.Forms.ComboBox();
			this.label26 = new global::System.Windows.Forms.Label();
			this.label27 = new global::System.Windows.Forms.Label();
			this.textBox12 = new global::System.Windows.Forms.TextBox();
			this.textBox13 = new global::System.Windows.Forms.TextBox();
			this.textBox14 = new global::System.Windows.Forms.TextBox();
			this.label28 = new global::System.Windows.Forms.Label();
			this.label29 = new global::System.Windows.Forms.Label();
			this.textBox15 = new global::System.Windows.Forms.TextBox();
			this.label30 = new global::System.Windows.Forms.Label();
			this.SourceMobTextBoxTimer = new global::System.Windows.Forms.Timer(this.components);
			this.ResultTextBox = new global::System.Windows.Forms.RichTextBox();
			this.SetFileNameTimer = new global::System.Windows.Forms.Timer(this.components);
			this.label16 = new global::System.Windows.Forms.Label();
			this.AddNameTextBox = new global::System.Windows.Forms.TextBox();
			this.label34 = new global::System.Windows.Forms.Label();
			this.AddTitleTextBox = new global::System.Windows.Forms.TextBox();
			this.MainTabControl.SuspendLayout();
			this.CommonTabPage.SuspendLayout();
			this.SpawnTableMapPanel.SuspendLayout();
			this.panel5.SuspendLayout();
			this.panel4.SuspendLayout();
			this.panel1.SuspendLayout();
			this.SpawnTablePanel.SuspendLayout();
			this.NewVisMobParamsPanel.SuspendLayout();
			this.AdditionalTabPage.SuspendLayout();
			this.panel6.SuspendLayout();
			this.AddSpawnTablePanel.SuspendLayout();
			this.AddNewVisMobParamsPanel.SuspendLayout();
			this.groupBox4.SuspendLayout();
			this.panel2.SuspendLayout();
			this.groupBox6.SuspendLayout();
			this.groupBox7.SuspendLayout();
			this.groupBox8.SuspendLayout();
			this.panel3.SuspendLayout();
			base.SuspendLayout();
			resources.ApplyResources(this.MinLevelTextBox, "MinLevelTextBox");
			this.MinLevelTextBox.Name = "MinLevelTextBox";
			resources.ApplyResources(this.SpeedTextBox, "SpeedTextBox");
			this.SpeedTextBox.Name = "SpeedTextBox";
			resources.ApplyResources(this.label8, "label8");
			this.label8.Name = "label8";
			resources.ApplyResources(this.label7, "label7");
			this.label7.Name = "label7";
			resources.ApplyResources(this.TitleTextBox, "TitleTextBox");
			this.TitleTextBox.Name = "TitleTextBox";
			resources.ApplyResources(this.label6, "label6");
			this.label6.Name = "label6";
			resources.ApplyResources(this.NameTextBox, "NameTextBox");
			this.NameTextBox.Name = "NameTextBox";
			resources.ApplyResources(this.label4, "label4");
			this.label4.Name = "label4";
			resources.ApplyResources(this.label5, "label5");
			this.label5.Name = "label5";
			resources.ApplyResources(this.CreateButton, "CreateButton");
			this.CreateButton.Name = "CreateButton";
			this.CreateButton.UseVisualStyleBackColor = true;
			resources.ApplyResources(this.ZoneComboBox, "ZoneComboBox");
			this.ZoneComboBox.DropDownStyle = global::System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.ZoneComboBox.FormattingEnabled = true;
			this.ZoneComboBox.Name = "ZoneComboBox";
			this.ZoneComboBox.Sorted = true;
			resources.ApplyResources(this.FileNameTextBox, "FileNameTextBox");
			this.FileNameTextBox.Name = "FileNameTextBox";
			resources.ApplyResources(this.label2, "label2");
			this.label2.Name = "label2";
			resources.ApplyResources(this.label1, "label1");
			this.label1.Name = "label1";
			resources.ApplyResources(this.label3, "label3");
			this.label3.Name = "label3";
			resources.ApplyResources(this.CreatureComboBox, "CreatureComboBox");
			this.CreatureComboBox.DropDownStyle = global::System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.CreatureComboBox.FormattingEnabled = true;
			this.CreatureComboBox.Name = "CreatureComboBox";
			this.CreatureComboBox.Sorted = true;
			resources.ApplyResources(this.MaxLevelTextBox, "MaxLevelTextBox");
			this.MaxLevelTextBox.Name = "MaxLevelTextBox";
			resources.ApplyResources(this.label9, "label9");
			this.label9.Name = "label9";
			resources.ApplyResources(this.label10, "label10");
			this.label10.Name = "label10";
			resources.ApplyResources(this.MainTabControl, "MainTabControl");
			this.MainTabControl.Controls.Add(this.CommonTabPage);
			this.MainTabControl.Controls.Add(this.AdditionalTabPage);
			this.MainTabControl.Name = "MainTabControl";
			this.MainTabControl.SelectedIndex = 0;
			this.CommonTabPage.Controls.Add(this.EditCreatureListButton);
			this.CommonTabPage.Controls.Add(this.SpawnTableMapPanel);
			this.CommonTabPage.Controls.Add(this.panel5);
			this.CommonTabPage.Controls.Add(this.panel4);
			this.CommonTabPage.Controls.Add(this.panel1);
			this.CommonTabPage.Controls.Add(this.SpawnTablePanel);
			this.CommonTabPage.Controls.Add(this.label13);
			this.CommonTabPage.Controls.Add(this.NewVisMobParamsPanel);
			this.CommonTabPage.Controls.Add(this.ChooseVisMobButton);
			this.CommonTabPage.Controls.Add(this.VisMobTextBox);
			this.CommonTabPage.Controls.Add(this.ZoneComboBox);
			this.CommonTabPage.Controls.Add(this.label1);
			this.CommonTabPage.Controls.Add(this.label2);
			this.CommonTabPage.Controls.Add(this.label10);
			this.CommonTabPage.Controls.Add(this.FileNameTextBox);
			this.CommonTabPage.Controls.Add(this.MaxLevelTextBox);
			this.CommonTabPage.Controls.Add(this.CreateButton);
			this.CommonTabPage.Controls.Add(this.label9);
			this.CommonTabPage.Controls.Add(this.label5);
			this.CommonTabPage.Controls.Add(this.CreatureComboBox);
			this.CommonTabPage.Controls.Add(this.label4);
			this.CommonTabPage.Controls.Add(this.label3);
			this.CommonTabPage.Controls.Add(this.NameTextBox);
			this.CommonTabPage.Controls.Add(this.SpeedTextBox);
			this.CommonTabPage.Controls.Add(this.MinLevelTextBox);
			this.CommonTabPage.Controls.Add(this.label6);
			this.CommonTabPage.Controls.Add(this.label8);
			this.CommonTabPage.Controls.Add(this.TitleTextBox);
			this.CommonTabPage.Controls.Add(this.label7);
			resources.ApplyResources(this.CommonTabPage, "CommonTabPage");
			this.CommonTabPage.Name = "CommonTabPage";
			this.CommonTabPage.UseVisualStyleBackColor = true;
			resources.ApplyResources(this.EditCreatureListButton, "EditCreatureListButton");
			this.EditCreatureListButton.Name = "EditCreatureListButton";
			this.EditCreatureListButton.UseVisualStyleBackColor = true;
			resources.ApplyResources(this.SpawnTableMapPanel, "SpawnTableMapPanel");
			this.SpawnTableMapPanel.Controls.Add(this.MapComboBox);
			this.SpawnTableMapPanel.Controls.Add(this.label32);
			this.SpawnTableMapPanel.Name = "SpawnTableMapPanel";
			resources.ApplyResources(this.MapComboBox, "MapComboBox");
			this.MapComboBox.DropDownStyle = global::System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.MapComboBox.FormattingEnabled = true;
			this.MapComboBox.Name = "MapComboBox";
			this.MapComboBox.Sorted = true;
			resources.ApplyResources(this.label32, "label32");
			this.label32.Name = "label32";
			this.panel5.Controls.Add(this.ExistingSTRadioButton);
			this.panel5.Controls.Add(this.NewSTRadioButton);
			resources.ApplyResources(this.panel5, "panel5");
			this.panel5.Name = "panel5";
			resources.ApplyResources(this.ExistingSTRadioButton, "ExistingSTRadioButton");
			this.ExistingSTRadioButton.Name = "ExistingSTRadioButton";
			this.ExistingSTRadioButton.UseVisualStyleBackColor = true;
			resources.ApplyResources(this.NewSTRadioButton, "NewSTRadioButton");
			this.NewSTRadioButton.Checked = true;
			this.NewSTRadioButton.Name = "NewSTRadioButton";
			this.NewSTRadioButton.TabStop = true;
			this.NewSTRadioButton.UseVisualStyleBackColor = true;
			this.panel4.Controls.Add(this.NewVMRadioButton);
			this.panel4.Controls.Add(this.ExistingVMRadioButton);
			resources.ApplyResources(this.panel4, "panel4");
			this.panel4.Name = "panel4";
			resources.ApplyResources(this.NewVMRadioButton, "NewVMRadioButton");
			this.NewVMRadioButton.Name = "NewVMRadioButton";
			this.NewVMRadioButton.UseVisualStyleBackColor = true;
			resources.ApplyResources(this.ExistingVMRadioButton, "ExistingVMRadioButton");
			this.ExistingVMRadioButton.Checked = true;
			this.ExistingVMRadioButton.Name = "ExistingVMRadioButton";
			this.ExistingVMRadioButton.TabStop = true;
			this.ExistingVMRadioButton.UseVisualStyleBackColor = true;
			this.panel1.Controls.Add(this.WildRadioButton);
			this.panel1.Controls.Add(this.NeutralRadioButton);
			resources.ApplyResources(this.panel1, "panel1");
			this.panel1.Name = "panel1";
			resources.ApplyResources(this.WildRadioButton, "WildRadioButton");
			this.WildRadioButton.Name = "WildRadioButton";
			this.WildRadioButton.UseVisualStyleBackColor = true;
			resources.ApplyResources(this.NeutralRadioButton, "NeutralRadioButton");
			this.NeutralRadioButton.Checked = true;
			this.NeutralRadioButton.Name = "NeutralRadioButton";
			this.NeutralRadioButton.TabStop = true;
			this.NeutralRadioButton.UseVisualStyleBackColor = true;
			resources.ApplyResources(this.SpawnTablePanel, "SpawnTablePanel");
			this.SpawnTablePanel.Controls.Add(this.label33);
			this.SpawnTablePanel.Controls.Add(this.SpawnTableTextBox);
			this.SpawnTablePanel.Controls.Add(this.ChooseSpawnTablebButton);
			this.SpawnTablePanel.Name = "SpawnTablePanel";
			resources.ApplyResources(this.label33, "label33");
			this.label33.Name = "label33";
			resources.ApplyResources(this.SpawnTableTextBox, "SpawnTableTextBox");
			this.SpawnTableTextBox.Name = "SpawnTableTextBox";
			resources.ApplyResources(this.ChooseSpawnTablebButton, "ChooseSpawnTablebButton");
			this.ChooseSpawnTablebButton.Name = "ChooseSpawnTablebButton";
			this.ChooseSpawnTablebButton.UseVisualStyleBackColor = true;
			resources.ApplyResources(this.label13, "label13");
			this.label13.Name = "label13";
			resources.ApplyResources(this.NewVisMobParamsPanel, "NewVisMobParamsPanel");
			this.NewVisMobParamsPanel.Controls.Add(this.VMScaleTextBox);
			this.NewVisMobParamsPanel.Controls.Add(this.label12);
			this.NewVisMobParamsPanel.Name = "NewVisMobParamsPanel";
			resources.ApplyResources(this.VMScaleTextBox, "VMScaleTextBox");
			this.VMScaleTextBox.Name = "VMScaleTextBox";
			resources.ApplyResources(this.label12, "label12");
			this.label12.Name = "label12";
			resources.ApplyResources(this.ChooseVisMobButton, "ChooseVisMobButton");
			this.ChooseVisMobButton.Name = "ChooseVisMobButton";
			this.ChooseVisMobButton.UseVisualStyleBackColor = true;
			resources.ApplyResources(this.VisMobTextBox, "VisMobTextBox");
			this.VisMobTextBox.Name = "VisMobTextBox";
			this.AdditionalTabPage.Controls.Add(this.label16);
			this.AdditionalTabPage.Controls.Add(this.AddNameTextBox);
			this.AdditionalTabPage.Controls.Add(this.label34);
			this.AdditionalTabPage.Controls.Add(this.AddTitleTextBox);
			this.AdditionalTabPage.Controls.Add(this.label11);
			this.AdditionalTabPage.Controls.Add(this.AddFileNameTextBox);
			this.AdditionalTabPage.Controls.Add(this.panel6);
			this.AdditionalTabPage.Controls.Add(this.AddCreateButton);
			this.AdditionalTabPage.Controls.Add(this.AddSpawnTablePanel);
			this.AdditionalTabPage.Controls.Add(this.AddChooseVisMobButton);
			this.AdditionalTabPage.Controls.Add(this.AddNewVisMobParamsPanel);
			this.AdditionalTabPage.Controls.Add(this.AddVisMobTextBox);
			this.AdditionalTabPage.Controls.Add(this.label17);
			this.AdditionalTabPage.Controls.Add(this.ChooseSourceMobButton);
			this.AdditionalTabPage.Controls.Add(this.SourceMobTextBox);
			this.AdditionalTabPage.Controls.Add(this.label14);
			this.AdditionalTabPage.Controls.Add(this.groupBox4);
			resources.ApplyResources(this.AdditionalTabPage, "AdditionalTabPage");
			this.AdditionalTabPage.Name = "AdditionalTabPage";
			this.AdditionalTabPage.UseVisualStyleBackColor = true;
			resources.ApplyResources(this.label11, "label11");
			this.label11.Name = "label11";
			resources.ApplyResources(this.AddFileNameTextBox, "AddFileNameTextBox");
			this.AddFileNameTextBox.Name = "AddFileNameTextBox";
			this.panel6.Controls.Add(this.AddNewVMRadioButton);
			this.panel6.Controls.Add(this.AddExistingVMRadioButton);
			resources.ApplyResources(this.panel6, "panel6");
			this.panel6.Name = "panel6";
			resources.ApplyResources(this.AddNewVMRadioButton, "AddNewVMRadioButton");
			this.AddNewVMRadioButton.Name = "AddNewVMRadioButton";
			this.AddNewVMRadioButton.UseVisualStyleBackColor = true;
			resources.ApplyResources(this.AddExistingVMRadioButton, "AddExistingVMRadioButton");
			this.AddExistingVMRadioButton.Checked = true;
			this.AddExistingVMRadioButton.Name = "AddExistingVMRadioButton";
			this.AddExistingVMRadioButton.TabStop = true;
			this.AddExistingVMRadioButton.UseVisualStyleBackColor = true;
			resources.ApplyResources(this.AddCreateButton, "AddCreateButton");
			this.AddCreateButton.Name = "AddCreateButton";
			this.AddCreateButton.UseVisualStyleBackColor = true;
			resources.ApplyResources(this.AddSpawnTablePanel, "AddSpawnTablePanel");
			this.AddSpawnTablePanel.Controls.Add(this.AddSpawnTableTextBox);
			this.AddSpawnTablePanel.Controls.Add(this.AddChooseSpawnTablebButton);
			this.AddSpawnTablePanel.Controls.Add(this.label31);
			this.AddSpawnTablePanel.Name = "AddSpawnTablePanel";
			resources.ApplyResources(this.AddSpawnTableTextBox, "AddSpawnTableTextBox");
			this.AddSpawnTableTextBox.Name = "AddSpawnTableTextBox";
			resources.ApplyResources(this.AddChooseSpawnTablebButton, "AddChooseSpawnTablebButton");
			this.AddChooseSpawnTablebButton.Name = "AddChooseSpawnTablebButton";
			this.AddChooseSpawnTablebButton.UseVisualStyleBackColor = true;
			resources.ApplyResources(this.label31, "label31");
			this.label31.Name = "label31";
			resources.ApplyResources(this.AddChooseVisMobButton, "AddChooseVisMobButton");
			this.AddChooseVisMobButton.Name = "AddChooseVisMobButton";
			this.AddChooseVisMobButton.UseVisualStyleBackColor = true;
			resources.ApplyResources(this.AddNewVisMobParamsPanel, "AddNewVisMobParamsPanel");
			this.AddNewVisMobParamsPanel.Controls.Add(this.AddVMScaleTextBox);
			this.AddNewVisMobParamsPanel.Controls.Add(this.label15);
			this.AddNewVisMobParamsPanel.Name = "AddNewVisMobParamsPanel";
			resources.ApplyResources(this.AddVMScaleTextBox, "AddVMScaleTextBox");
			this.AddVMScaleTextBox.Name = "AddVMScaleTextBox";
			resources.ApplyResources(this.label15, "label15");
			this.label15.Name = "label15";
			resources.ApplyResources(this.AddVisMobTextBox, "AddVisMobTextBox");
			this.AddVisMobTextBox.Name = "AddVisMobTextBox";
			resources.ApplyResources(this.label17, "label17");
			this.label17.Name = "label17";
			resources.ApplyResources(this.ChooseSourceMobButton, "ChooseSourceMobButton");
			this.ChooseSourceMobButton.Name = "ChooseSourceMobButton";
			this.ChooseSourceMobButton.UseVisualStyleBackColor = true;
			resources.ApplyResources(this.SourceMobTextBox, "SourceMobTextBox");
			this.SourceMobTextBox.Name = "SourceMobTextBox";
			resources.ApplyResources(this.label14, "label14");
			this.label14.Name = "label14";
			this.groupBox4.Controls.Add(this.CreateMiniBossRadioButton);
			this.groupBox4.Controls.Add(this.CreateSilverRadioButton);
			resources.ApplyResources(this.groupBox4, "groupBox4");
			this.groupBox4.Name = "groupBox4";
			this.groupBox4.TabStop = false;
			resources.ApplyResources(this.CreateMiniBossRadioButton, "CreateMiniBossRadioButton");
			this.CreateMiniBossRadioButton.Name = "CreateMiniBossRadioButton";
			this.CreateMiniBossRadioButton.UseVisualStyleBackColor = true;
			resources.ApplyResources(this.CreateSilverRadioButton, "CreateSilverRadioButton");
			this.CreateSilverRadioButton.Checked = true;
			this.CreateSilverRadioButton.Name = "CreateSilverRadioButton";
			this.CreateSilverRadioButton.TabStop = true;
			this.CreateSilverRadioButton.UseVisualStyleBackColor = true;
			resources.ApplyResources(this.panel2, "panel2");
			this.panel2.Controls.Add(this.textBox6);
			this.panel2.Controls.Add(this.button1);
			this.panel2.Name = "panel2";
			resources.ApplyResources(this.textBox6, "textBox6");
			this.textBox6.Name = "textBox6";
			resources.ApplyResources(this.button1, "button1");
			this.button1.Name = "button1";
			this.button1.UseVisualStyleBackColor = true;
			resources.ApplyResources(this.label18, "label18");
			this.label18.Name = "label18";
			this.groupBox6.Controls.Add(this.radioButton3);
			this.groupBox6.Controls.Add(this.radioButton4);
			resources.ApplyResources(this.groupBox6, "groupBox6");
			this.groupBox6.Name = "groupBox6";
			this.groupBox6.TabStop = false;
			resources.ApplyResources(this.radioButton3, "radioButton3");
			this.radioButton3.Name = "radioButton3";
			this.radioButton3.UseVisualStyleBackColor = true;
			resources.ApplyResources(this.radioButton4, "radioButton4");
			this.radioButton4.Checked = true;
			this.radioButton4.Name = "radioButton4";
			this.radioButton4.TabStop = true;
			this.radioButton4.UseVisualStyleBackColor = true;
			this.groupBox7.Controls.Add(this.radioButton5);
			this.groupBox7.Controls.Add(this.radioButton6);
			resources.ApplyResources(this.groupBox7, "groupBox7");
			this.groupBox7.Name = "groupBox7";
			this.groupBox7.TabStop = false;
			resources.ApplyResources(this.radioButton5, "radioButton5");
			this.radioButton5.Name = "radioButton5";
			this.radioButton5.UseVisualStyleBackColor = true;
			resources.ApplyResources(this.radioButton6, "radioButton6");
			this.radioButton6.Checked = true;
			this.radioButton6.Name = "radioButton6";
			this.radioButton6.TabStop = true;
			this.radioButton6.UseVisualStyleBackColor = true;
			this.groupBox8.Controls.Add(this.radioButton7);
			this.groupBox8.Controls.Add(this.radioButton8);
			resources.ApplyResources(this.groupBox8, "groupBox8");
			this.groupBox8.Name = "groupBox8";
			this.groupBox8.TabStop = false;
			resources.ApplyResources(this.radioButton7, "radioButton7");
			this.radioButton7.Name = "radioButton7";
			this.radioButton7.UseVisualStyleBackColor = true;
			resources.ApplyResources(this.radioButton8, "radioButton8");
			this.radioButton8.Checked = true;
			this.radioButton8.Name = "radioButton8";
			this.radioButton8.TabStop = true;
			this.radioButton8.UseVisualStyleBackColor = true;
			this.panel3.Controls.Add(this.textBox7);
			this.panel3.Controls.Add(this.label19);
			this.panel3.Controls.Add(this.textBox8);
			this.panel3.Controls.Add(this.label20);
			resources.ApplyResources(this.panel3, "panel3");
			this.panel3.Name = "panel3";
			resources.ApplyResources(this.textBox7, "textBox7");
			this.textBox7.Name = "textBox7";
			resources.ApplyResources(this.label19, "label19");
			this.label19.Name = "label19";
			resources.ApplyResources(this.textBox8, "textBox8");
			this.textBox8.Name = "textBox8";
			resources.ApplyResources(this.label20, "label20");
			this.label20.Name = "label20";
			resources.ApplyResources(this.button2, "button2");
			this.button2.Name = "button2";
			this.button2.UseVisualStyleBackColor = true;
			resources.ApplyResources(this.textBox9, "textBox9");
			this.textBox9.Name = "textBox9";
			resources.ApplyResources(this.comboBox1, "comboBox1");
			this.comboBox1.FormattingEnabled = true;
			this.comboBox1.Items.AddRange(new object[]
			{
				resources.GetString("comboBox1.Items"),
				resources.GetString("comboBox1.Items1"),
				resources.GetString("comboBox1.Items2"),
				resources.GetString("comboBox1.Items3"),
				resources.GetString("comboBox1.Items4")
			});
			this.comboBox1.Name = "comboBox1";
			resources.ApplyResources(this.label21, "label21");
			this.label21.Name = "label21";
			resources.ApplyResources(this.label22, "label22");
			this.label22.Name = "label22";
			resources.ApplyResources(this.label23, "label23");
			this.label23.Name = "label23";
			resources.ApplyResources(this.textBox10, "textBox10");
			this.textBox10.Name = "textBox10";
			resources.ApplyResources(this.textBox11, "textBox11");
			this.textBox11.Name = "textBox11";
			resources.ApplyResources(this.button3, "button3");
			this.button3.Name = "button3";
			this.button3.UseVisualStyleBackColor = true;
			resources.ApplyResources(this.label24, "label24");
			this.label24.Name = "label24";
			resources.ApplyResources(this.label25, "label25");
			this.label25.Name = "label25";
			resources.ApplyResources(this.comboBox2, "comboBox2");
			this.comboBox2.FormattingEnabled = true;
			this.comboBox2.Items.AddRange(new object[]
			{
				resources.GetString("comboBox2.Items"),
				resources.GetString("comboBox2.Items1"),
				resources.GetString("comboBox2.Items2"),
				resources.GetString("comboBox2.Items3"),
				resources.GetString("comboBox2.Items4"),
				resources.GetString("comboBox2.Items5"),
				resources.GetString("comboBox2.Items6")
			});
			this.comboBox2.Name = "comboBox2";
			resources.ApplyResources(this.label26, "label26");
			this.label26.Name = "label26";
			resources.ApplyResources(this.label27, "label27");
			this.label27.Name = "label27";
			resources.ApplyResources(this.textBox12, "textBox12");
			this.textBox12.Name = "textBox12";
			resources.ApplyResources(this.textBox13, "textBox13");
			this.textBox13.Name = "textBox13";
			resources.ApplyResources(this.textBox14, "textBox14");
			this.textBox14.Name = "textBox14";
			resources.ApplyResources(this.label28, "label28");
			this.label28.Name = "label28";
			resources.ApplyResources(this.label29, "label29");
			this.label29.Name = "label29";
			resources.ApplyResources(this.textBox15, "textBox15");
			this.textBox15.Name = "textBox15";
			resources.ApplyResources(this.label30, "label30");
			this.label30.Name = "label30";
			this.SourceMobTextBoxTimer.Interval = 300;
			resources.ApplyResources(this.ResultTextBox, "ResultTextBox");
			this.ResultTextBox.Name = "ResultTextBox";
			this.ResultTextBox.ReadOnly = true;
			resources.ApplyResources(this.label16, "label16");
			this.label16.Name = "label16";
			resources.ApplyResources(this.AddNameTextBox, "AddNameTextBox");
			this.AddNameTextBox.Name = "AddNameTextBox";
			resources.ApplyResources(this.label34, "label34");
			this.label34.Name = "label34";
			resources.ApplyResources(this.AddTitleTextBox, "AddTitleTextBox");
			this.AddTitleTextBox.Name = "AddTitleTextBox";
			resources.ApplyResources(this, "$this");
			base.AutoScaleMode = global::System.Windows.Forms.AutoScaleMode.Font;
			base.Controls.Add(this.ResultTextBox);
			base.Controls.Add(this.MainTabControl);
			base.Name = "CreateMobForm";
			this.MainTabControl.ResumeLayout(false);
			this.CommonTabPage.ResumeLayout(false);
			this.CommonTabPage.PerformLayout();
			this.SpawnTableMapPanel.ResumeLayout(false);
			this.SpawnTableMapPanel.PerformLayout();
			this.panel5.ResumeLayout(false);
			this.panel5.PerformLayout();
			this.panel4.ResumeLayout(false);
			this.panel4.PerformLayout();
			this.panel1.ResumeLayout(false);
			this.panel1.PerformLayout();
			this.SpawnTablePanel.ResumeLayout(false);
			this.SpawnTablePanel.PerformLayout();
			this.NewVisMobParamsPanel.ResumeLayout(false);
			this.NewVisMobParamsPanel.PerformLayout();
			this.AdditionalTabPage.ResumeLayout(false);
			this.AdditionalTabPage.PerformLayout();
			this.panel6.ResumeLayout(false);
			this.panel6.PerformLayout();
			this.AddSpawnTablePanel.ResumeLayout(false);
			this.AddSpawnTablePanel.PerformLayout();
			this.AddNewVisMobParamsPanel.ResumeLayout(false);
			this.AddNewVisMobParamsPanel.PerformLayout();
			this.groupBox4.ResumeLayout(false);
			this.groupBox4.PerformLayout();
			this.panel2.ResumeLayout(false);
			this.panel2.PerformLayout();
			this.groupBox6.ResumeLayout(false);
			this.groupBox6.PerformLayout();
			this.groupBox7.ResumeLayout(false);
			this.groupBox7.PerformLayout();
			this.groupBox8.ResumeLayout(false);
			this.groupBox8.PerformLayout();
			this.panel3.ResumeLayout(false);
			this.panel3.PerformLayout();
			base.ResumeLayout(false);
		}

		// Token: 0x04001077 RID: 4215
		private global::System.ComponentModel.IContainer components;

		// Token: 0x04001078 RID: 4216
		private global::System.Windows.Forms.TextBox SpeedTextBox;

		// Token: 0x04001079 RID: 4217
		private global::System.Windows.Forms.Label label8;

		// Token: 0x0400107A RID: 4218
		private global::System.Windows.Forms.Label label7;

		// Token: 0x0400107B RID: 4219
		private global::System.Windows.Forms.TextBox TitleTextBox;

		// Token: 0x0400107C RID: 4220
		private global::System.Windows.Forms.Label label6;

		// Token: 0x0400107D RID: 4221
		private global::System.Windows.Forms.TextBox NameTextBox;

		// Token: 0x0400107E RID: 4222
		private global::System.Windows.Forms.Label label4;

		// Token: 0x0400107F RID: 4223
		private global::System.Windows.Forms.Label label5;

		// Token: 0x04001080 RID: 4224
		private global::System.Windows.Forms.Button CreateButton;

		// Token: 0x04001081 RID: 4225
		private global::System.Windows.Forms.ComboBox ZoneComboBox;

		// Token: 0x04001082 RID: 4226
		private global::System.Windows.Forms.TextBox FileNameTextBox;

		// Token: 0x04001083 RID: 4227
		private global::System.Windows.Forms.Label label2;

		// Token: 0x04001084 RID: 4228
		private global::System.Windows.Forms.Label label1;

		// Token: 0x04001085 RID: 4229
		private global::System.Windows.Forms.Label label3;

		// Token: 0x04001086 RID: 4230
		private global::System.Windows.Forms.ComboBox CreatureComboBox;

		// Token: 0x04001087 RID: 4231
		private global::System.Windows.Forms.TextBox MaxLevelTextBox;

		// Token: 0x04001088 RID: 4232
		private global::System.Windows.Forms.Label label9;

		// Token: 0x04001089 RID: 4233
		private global::System.Windows.Forms.Label label10;

		// Token: 0x0400108A RID: 4234
		private global::System.Windows.Forms.TextBox MinLevelTextBox;

		// Token: 0x0400108B RID: 4235
		private global::System.Windows.Forms.TabControl MainTabControl;

		// Token: 0x0400108C RID: 4236
		private global::System.Windows.Forms.TabPage CommonTabPage;

		// Token: 0x0400108D RID: 4237
		private global::System.Windows.Forms.TabPage AdditionalTabPage;

		// Token: 0x0400108E RID: 4238
		private global::System.Windows.Forms.Button ChooseVisMobButton;

		// Token: 0x0400108F RID: 4239
		private global::System.Windows.Forms.TextBox VisMobTextBox;

		// Token: 0x04001090 RID: 4240
		private global::System.Windows.Forms.Panel NewVisMobParamsPanel;

		// Token: 0x04001091 RID: 4241
		private global::System.Windows.Forms.Label label12;

		// Token: 0x04001092 RID: 4242
		private global::System.Windows.Forms.RadioButton NewVMRadioButton;

		// Token: 0x04001093 RID: 4243
		private global::System.Windows.Forms.RadioButton ExistingVMRadioButton;

		// Token: 0x04001094 RID: 4244
		private global::System.Windows.Forms.Label label13;

		// Token: 0x04001095 RID: 4245
		private global::System.Windows.Forms.RadioButton ExistingSTRadioButton;

		// Token: 0x04001096 RID: 4246
		private global::System.Windows.Forms.RadioButton NewSTRadioButton;

		// Token: 0x04001097 RID: 4247
		private global::System.Windows.Forms.Button ChooseSpawnTablebButton;

		// Token: 0x04001098 RID: 4248
		private global::System.Windows.Forms.TextBox SpawnTableTextBox;

		// Token: 0x04001099 RID: 4249
		private global::System.Windows.Forms.Button ChooseSourceMobButton;

		// Token: 0x0400109A RID: 4250
		private global::System.Windows.Forms.Label label14;

		// Token: 0x0400109B RID: 4251
		private global::System.Windows.Forms.GroupBox groupBox4;

		// Token: 0x0400109C RID: 4252
		private global::System.Windows.Forms.RadioButton CreateMiniBossRadioButton;

		// Token: 0x0400109D RID: 4253
		private global::System.Windows.Forms.RadioButton CreateSilverRadioButton;

		// Token: 0x0400109E RID: 4254
		public global::System.Windows.Forms.TextBox SourceMobTextBox;

		// Token: 0x0400109F RID: 4255
		private global::System.Windows.Forms.Button AddChooseVisMobButton;

		// Token: 0x040010A0 RID: 4256
		private global::System.Windows.Forms.RadioButton AddNewVMRadioButton;

		// Token: 0x040010A1 RID: 4257
		private global::System.Windows.Forms.RadioButton AddExistingVMRadioButton;

		// Token: 0x040010A2 RID: 4258
		private global::System.Windows.Forms.Panel AddNewVisMobParamsPanel;

		// Token: 0x040010A3 RID: 4259
		private global::System.Windows.Forms.TextBox AddVMScaleTextBox;

		// Token: 0x040010A4 RID: 4260
		private global::System.Windows.Forms.Label label15;

		// Token: 0x040010A5 RID: 4261
		private global::System.Windows.Forms.TextBox AddVisMobTextBox;

		// Token: 0x040010A6 RID: 4262
		private global::System.Windows.Forms.Label label17;

		// Token: 0x040010A7 RID: 4263
		private global::System.Windows.Forms.Panel panel2;

		// Token: 0x040010A8 RID: 4264
		private global::System.Windows.Forms.TextBox textBox6;

		// Token: 0x040010A9 RID: 4265
		private global::System.Windows.Forms.Button button1;

		// Token: 0x040010AA RID: 4266
		private global::System.Windows.Forms.Label label18;

		// Token: 0x040010AB RID: 4267
		private global::System.Windows.Forms.GroupBox groupBox6;

		// Token: 0x040010AC RID: 4268
		private global::System.Windows.Forms.RadioButton radioButton3;

		// Token: 0x040010AD RID: 4269
		private global::System.Windows.Forms.RadioButton radioButton4;

		// Token: 0x040010AE RID: 4270
		private global::System.Windows.Forms.GroupBox groupBox7;

		// Token: 0x040010AF RID: 4271
		private global::System.Windows.Forms.RadioButton radioButton5;

		// Token: 0x040010B0 RID: 4272
		private global::System.Windows.Forms.RadioButton radioButton6;

		// Token: 0x040010B1 RID: 4273
		private global::System.Windows.Forms.GroupBox groupBox8;

		// Token: 0x040010B2 RID: 4274
		private global::System.Windows.Forms.RadioButton radioButton7;

		// Token: 0x040010B3 RID: 4275
		private global::System.Windows.Forms.RadioButton radioButton8;

		// Token: 0x040010B4 RID: 4276
		private global::System.Windows.Forms.Panel panel3;

		// Token: 0x040010B5 RID: 4277
		private global::System.Windows.Forms.TextBox textBox7;

		// Token: 0x040010B6 RID: 4278
		private global::System.Windows.Forms.Label label19;

		// Token: 0x040010B7 RID: 4279
		private global::System.Windows.Forms.TextBox textBox8;

		// Token: 0x040010B8 RID: 4280
		private global::System.Windows.Forms.Label label20;

		// Token: 0x040010B9 RID: 4281
		private global::System.Windows.Forms.Button button2;

		// Token: 0x040010BA RID: 4282
		private global::System.Windows.Forms.TextBox textBox9;

		// Token: 0x040010BB RID: 4283
		private global::System.Windows.Forms.ComboBox comboBox1;

		// Token: 0x040010BC RID: 4284
		private global::System.Windows.Forms.Label label21;

		// Token: 0x040010BD RID: 4285
		private global::System.Windows.Forms.Label label22;

		// Token: 0x040010BE RID: 4286
		private global::System.Windows.Forms.Label label23;

		// Token: 0x040010BF RID: 4287
		private global::System.Windows.Forms.TextBox textBox10;

		// Token: 0x040010C0 RID: 4288
		private global::System.Windows.Forms.TextBox textBox11;

		// Token: 0x040010C1 RID: 4289
		private global::System.Windows.Forms.Button button3;

		// Token: 0x040010C2 RID: 4290
		private global::System.Windows.Forms.Label label24;

		// Token: 0x040010C3 RID: 4291
		private global::System.Windows.Forms.Label label25;

		// Token: 0x040010C4 RID: 4292
		private global::System.Windows.Forms.ComboBox comboBox2;

		// Token: 0x040010C5 RID: 4293
		private global::System.Windows.Forms.Label label26;

		// Token: 0x040010C6 RID: 4294
		private global::System.Windows.Forms.Label label27;

		// Token: 0x040010C7 RID: 4295
		private global::System.Windows.Forms.TextBox textBox12;

		// Token: 0x040010C8 RID: 4296
		private global::System.Windows.Forms.TextBox textBox13;

		// Token: 0x040010C9 RID: 4297
		private global::System.Windows.Forms.TextBox textBox14;

		// Token: 0x040010CA RID: 4298
		private global::System.Windows.Forms.Label label28;

		// Token: 0x040010CB RID: 4299
		private global::System.Windows.Forms.Label label29;

		// Token: 0x040010CC RID: 4300
		private global::System.Windows.Forms.TextBox textBox15;

		// Token: 0x040010CD RID: 4301
		private global::System.Windows.Forms.Label label30;

		// Token: 0x040010CE RID: 4302
		public global::System.Windows.Forms.TextBox VMScaleTextBox;

		// Token: 0x040010CF RID: 4303
		private global::System.Windows.Forms.Panel AddSpawnTablePanel;

		// Token: 0x040010D0 RID: 4304
		private global::System.Windows.Forms.TextBox AddSpawnTableTextBox;

		// Token: 0x040010D1 RID: 4305
		private global::System.Windows.Forms.Button AddChooseSpawnTablebButton;

		// Token: 0x040010D2 RID: 4306
		private global::System.Windows.Forms.Label label31;

		// Token: 0x040010D3 RID: 4307
		public global::System.Windows.Forms.Panel SpawnTablePanel;

		// Token: 0x040010D4 RID: 4308
		private global::System.Windows.Forms.Button AddCreateButton;

		// Token: 0x040010D5 RID: 4309
		private global::System.Windows.Forms.Panel panel5;

		// Token: 0x040010D6 RID: 4310
		private global::System.Windows.Forms.Panel panel4;

		// Token: 0x040010D7 RID: 4311
		private global::System.Windows.Forms.Panel panel1;

		// Token: 0x040010D8 RID: 4312
		private global::System.Windows.Forms.RadioButton WildRadioButton;

		// Token: 0x040010D9 RID: 4313
		private global::System.Windows.Forms.RadioButton NeutralRadioButton;

		// Token: 0x040010DA RID: 4314
		private global::System.Windows.Forms.Panel panel6;

		// Token: 0x040010DB RID: 4315
		private global::System.Windows.Forms.ComboBox MapComboBox;

		// Token: 0x040010DC RID: 4316
		private global::System.Windows.Forms.Label label32;

		// Token: 0x040010DD RID: 4317
		private global::System.Windows.Forms.Panel SpawnTableMapPanel;

		// Token: 0x040010DE RID: 4318
		private global::System.Windows.Forms.Label label33;

		// Token: 0x040010DF RID: 4319
		private global::System.Windows.Forms.Timer SourceMobTextBoxTimer;

		// Token: 0x040010E0 RID: 4320
		private global::System.Windows.Forms.RichTextBox ResultTextBox;

		// Token: 0x040010E1 RID: 4321
		private global::System.Windows.Forms.Button EditCreatureListButton;

		// Token: 0x040010E2 RID: 4322
		private global::System.Windows.Forms.Timer SetFileNameTimer;

		// Token: 0x040010E3 RID: 4323
		private global::System.Windows.Forms.Label label11;

		// Token: 0x040010E4 RID: 4324
		private global::System.Windows.Forms.TextBox AddFileNameTextBox;

		// Token: 0x040010E5 RID: 4325
		private global::System.Windows.Forms.Label label16;

		// Token: 0x040010E6 RID: 4326
		private global::System.Windows.Forms.TextBox AddNameTextBox;

		// Token: 0x040010E7 RID: 4327
		private global::System.Windows.Forms.Label label34;

		// Token: 0x040010E8 RID: 4328
		private global::System.Windows.Forms.TextBox AddTitleTextBox;
	}
}
