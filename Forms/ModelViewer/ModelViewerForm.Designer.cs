namespace MapEditor.Forms.ModelViewer
{
	// Token: 0x02000016 RID: 22
	public partial class ModelViewerForm : global::MapEditor.Forms.Base.BaseForm
	{
		// Token: 0x06000224 RID: 548 RVA: 0x00017A67 File Offset: 0x00016A67
		protected override void Dispose(bool disposing)
		{
			if (disposing && this.components != null)
			{
				this.components.Dispose();
			}
			base.Dispose(disposing);
		}

		// Token: 0x06000225 RID: 549 RVA: 0x00017A88 File Offset: 0x00016A88
		private void InitializeComponent()
		{
			this.components = new global::System.ComponentModel.Container();
			global::System.ComponentModel.ComponentResourceManager resources = new global::System.ComponentModel.ComponentResourceManager(typeof(global::MapEditor.Forms.ModelViewer.ModelViewerForm));
			this.ScenePanel = new global::System.Windows.Forms.Panel();
			this.SceneTimer = new global::System.Windows.Forms.Timer(this.components);
			this.EquipmentControl = new global::Tools.ModelViewerElements.Controls.Equipment.EquipmentControl();
			this.characterBasicVarControl = new global::Tools.ModelViewerElements.Controls.CharacterBasicVar.CharacterBasicVarControl();
			this.OtherPanel = new global::System.Windows.Forms.Panel();
			this.label1 = new global::System.Windows.Forms.Label();
			this.ScaleTextBox = new global::System.Windows.Forms.TextBox();
			this.ScaleFieldTimer = new global::System.Windows.Forms.Timer(this.components);
			this.menuStrip = new global::System.Windows.Forms.MenuStrip();
			this.modelToolStripMenuItem = new global::System.Windows.Forms.ToolStripMenuItem();
			this.loadToolStripMenuItem = new global::System.Windows.Forms.ToolStripMenuItem();
			this.saveToolStripMenuItem = new global::System.Windows.Forms.ToolStripMenuItem();
			this.saveAsToolStripMenuItem = new global::System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator1 = new global::System.Windows.Forms.ToolStripSeparator();
			this.recentListToolStripMenuItem = new global::System.Windows.Forms.ToolStripMenuItem();
			this.recentModelToolStripMenuItem = new global::System.Windows.Forms.ToolStripMenuItem();
			this.recentModelToolStripMenuItem1 = new global::System.Windows.Forms.ToolStripMenuItem();
			this.recentModelToolStripMenuItem2 = new global::System.Windows.Forms.ToolStripMenuItem();
			this.recentModelToolStripMenuItem3 = new global::System.Windows.Forms.ToolStripMenuItem();
			this.recentModelToolStripMenuItem4 = new global::System.Windows.Forms.ToolStripMenuItem();
			this.recentModelToolStripMenuItem5 = new global::System.Windows.Forms.ToolStripMenuItem();
			this.recentModelToolStripMenuItem6 = new global::System.Windows.Forms.ToolStripMenuItem();
			this.recentModelToolStripMenuItem7 = new global::System.Windows.Forms.ToolStripMenuItem();
			this.recentModelToolStripMenuItem8 = new global::System.Windows.Forms.ToolStripMenuItem();
			this.recentModelToolStripMenuItem9 = new global::System.Windows.Forms.ToolStripMenuItem();
			this.cameraToolStripMenuItem = new global::System.Windows.Forms.ToolStripMenuItem();
			this.slowToolStripMenuItem = new global::System.Windows.Forms.ToolStripMenuItem();
			this.averageToolStripMenuItem = new global::System.Windows.Forms.ToolStripMenuItem();
			this.fastToolStripMenuItem = new global::System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator3 = new global::System.Windows.Forms.ToolStripSeparator();
			this.centerToolStripMenuItem = new global::System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator4 = new global::System.Windows.Forms.ToolStripSeparator();
			this.mayaToolStripMenuItem = new global::System.Windows.Forms.ToolStripMenuItem();
			this.fPSToolStripMenuItem = new global::System.Windows.Forms.ToolStripMenuItem();
			this.toolStrip = new global::System.Windows.Forms.ToolStrip();
			this.OpenModelToolStripButton = new global::System.Windows.Forms.ToolStripButton();
			this.SaveModelToolStripButton = new global::System.Windows.Forms.ToolStripButton();
			this.SaveAsModelToolStripButton = new global::System.Windows.Forms.ToolStripButton();
			this.toolStripSeparator5 = new global::System.Windows.Forms.ToolStripSeparator();
			this.toolStripButton1 = new global::System.Windows.Forms.ToolStripButton();
			this.toolStripButton2 = new global::System.Windows.Forms.ToolStripButton();
			this.toolStripButton3 = new global::System.Windows.Forms.ToolStripButton();
			this.toolStripButton4 = new global::System.Windows.Forms.ToolStripButton();
			this.selectVisMobExtElemControl = new global::Tools.ModelViewerElements.Controls.SelectVisMobExtElem.SelectVisMobExtElemControl();
			this.changeCharacterControl = new global::Tools.ModelViewerElements.Controls.ChangeCharacter.ChangeCharacterControl();
			this.presets = new global::Tools.ModelViewerElements.Controls.CharacterProportionsVar.CharacterProportionPresetsControl();
			this.panel1 = new global::System.Windows.Forms.Panel();
			this.label2 = new global::System.Windows.Forms.Label();
			this.randomizeControl = new global::Tools.ModelViewerElements.Controls.RandomizeParams.RandomizeControl();
			this.OtherPanel.SuspendLayout();
			this.menuStrip.SuspendLayout();
			this.toolStrip.SuspendLayout();
			this.panel1.SuspendLayout();
			base.SuspendLayout();
			this.ScenePanel.Anchor = (global::System.Windows.Forms.AnchorStyles.Top | global::System.Windows.Forms.AnchorStyles.Bottom | global::System.Windows.Forms.AnchorStyles.Left | global::System.Windows.Forms.AnchorStyles.Right);
			this.ScenePanel.Location = new global::System.Drawing.Point(235, 54);
			this.ScenePanel.Name = "ScenePanel";
			this.ScenePanel.Size = new global::System.Drawing.Size(323, 570);
			this.ScenePanel.TabIndex = 0;
			this.EquipmentControl.Anchor = (global::System.Windows.Forms.AnchorStyles.Top | global::System.Windows.Forms.AnchorStyles.Bottom | global::System.Windows.Forms.AnchorStyles.Right);
			this.EquipmentControl.BorderStyle = global::System.Windows.Forms.BorderStyle.FixedSingle;
			this.EquipmentControl.Location = new global::System.Drawing.Point(560, 52);
			this.EquipmentControl.Name = "EquipmentControl";
			this.EquipmentControl.SaveLoadEqipmentFilePath = null;
			this.EquipmentControl.Size = new global::System.Drawing.Size(270, 572);
			this.EquipmentControl.TabIndex = 1;
			this.characterBasicVarControl.BorderStyle = global::System.Windows.Forms.BorderStyle.FixedSingle;
			this.characterBasicVarControl.Location = new global::System.Drawing.Point(0, 82);
			this.characterBasicVarControl.Name = "characterBasicVarControl";
			this.characterBasicVarControl.Size = new global::System.Drawing.Size(237, 363);
			this.characterBasicVarControl.TabIndex = 2;
			this.OtherPanel.BorderStyle = global::System.Windows.Forms.BorderStyle.FixedSingle;
			this.OtherPanel.Controls.Add(this.label1);
			this.OtherPanel.Controls.Add(this.ScaleTextBox);
			this.OtherPanel.Location = new global::System.Drawing.Point(0, 581);
			this.OtherPanel.Name = "OtherPanel";
			this.OtherPanel.Size = new global::System.Drawing.Size(232, 27);
			this.OtherPanel.TabIndex = 3;
			this.label1.AutoSize = true;
			this.label1.Location = new global::System.Drawing.Point(3, 5);
			this.label1.Name = "label1";
			this.label1.Size = new global::System.Drawing.Size(34, 13);
			this.label1.TabIndex = 1;
			this.label1.Text = "Scale";
			this.ScaleTextBox.Location = new global::System.Drawing.Point(43, 2);
			this.ScaleTextBox.Name = "ScaleTextBox";
			this.ScaleTextBox.Size = new global::System.Drawing.Size(60, 20);
			this.ScaleTextBox.TabIndex = 0;
			this.menuStrip.Items.AddRange(new global::System.Windows.Forms.ToolStripItem[]
			{
				this.modelToolStripMenuItem,
				this.cameraToolStripMenuItem
			});
			this.menuStrip.Location = new global::System.Drawing.Point(0, 0);
			this.menuStrip.Name = "menuStrip";
			this.menuStrip.Size = new global::System.Drawing.Size(833, 24);
			this.menuStrip.TabIndex = 4;
			this.menuStrip.Text = "menuStrip1";
			this.modelToolStripMenuItem.DropDownItems.AddRange(new global::System.Windows.Forms.ToolStripItem[]
			{
				this.loadToolStripMenuItem,
				this.saveToolStripMenuItem,
				this.saveAsToolStripMenuItem,
				this.toolStripSeparator1,
				this.recentListToolStripMenuItem
			});
			this.modelToolStripMenuItem.Name = "modelToolStripMenuItem";
			this.modelToolStripMenuItem.Size = new global::System.Drawing.Size(47, 20);
			this.modelToolStripMenuItem.Tag = "";
			this.modelToolStripMenuItem.Text = "Model";
			this.loadToolStripMenuItem.Name = "loadToolStripMenuItem";
			this.loadToolStripMenuItem.Size = new global::System.Drawing.Size(138, 22);
			this.loadToolStripMenuItem.Tag = "toggle_load_model";
			this.loadToolStripMenuItem.Text = "Load...";
			this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
			this.saveToolStripMenuItem.Size = new global::System.Drawing.Size(138, 22);
			this.saveToolStripMenuItem.Tag = "toggle_save_model";
			this.saveToolStripMenuItem.Text = "Save";
			this.saveAsToolStripMenuItem.Name = "saveAsToolStripMenuItem";
			this.saveAsToolStripMenuItem.Size = new global::System.Drawing.Size(138, 22);
			this.saveAsToolStripMenuItem.Tag = "toggle_save_as_model";
			this.saveAsToolStripMenuItem.Text = "Save As...";
			this.toolStripSeparator1.Name = "toolStripSeparator1";
			this.toolStripSeparator1.Size = new global::System.Drawing.Size(135, 6);
			this.recentListToolStripMenuItem.DropDownItems.AddRange(new global::System.Windows.Forms.ToolStripItem[]
			{
				this.recentModelToolStripMenuItem,
				this.recentModelToolStripMenuItem1,
				this.recentModelToolStripMenuItem2,
				this.recentModelToolStripMenuItem3,
				this.recentModelToolStripMenuItem4,
				this.recentModelToolStripMenuItem5,
				this.recentModelToolStripMenuItem6,
				this.recentModelToolStripMenuItem7,
				this.recentModelToolStripMenuItem8,
				this.recentModelToolStripMenuItem9
			});
			this.recentListToolStripMenuItem.Name = "recentListToolStripMenuItem";
			this.recentListToolStripMenuItem.Size = new global::System.Drawing.Size(138, 22);
			this.recentListToolStripMenuItem.Text = "Resent List";
			this.recentModelToolStripMenuItem.Name = "recentModelToolStripMenuItem";
			this.recentModelToolStripMenuItem.Size = new global::System.Drawing.Size(150, 22);
			this.recentModelToolStripMenuItem.Tag = "recent_model_0";
			this.recentModelToolStripMenuItem.Text = "Recent Model";
			this.recentModelToolStripMenuItem1.Name = "recentModelToolStripMenuItem1";
			this.recentModelToolStripMenuItem1.Size = new global::System.Drawing.Size(150, 22);
			this.recentModelToolStripMenuItem1.Tag = "recent_model_1";
			this.recentModelToolStripMenuItem1.Text = "Recent Model";
			this.recentModelToolStripMenuItem2.Name = "recentModelToolStripMenuItem2";
			this.recentModelToolStripMenuItem2.Size = new global::System.Drawing.Size(150, 22);
			this.recentModelToolStripMenuItem2.Tag = "recent_model_2";
			this.recentModelToolStripMenuItem2.Text = "Recent Model";
			this.recentModelToolStripMenuItem3.Name = "recentModelToolStripMenuItem3";
			this.recentModelToolStripMenuItem3.Size = new global::System.Drawing.Size(150, 22);
			this.recentModelToolStripMenuItem3.Tag = "recent_model_3";
			this.recentModelToolStripMenuItem3.Text = "Recent Model";
			this.recentModelToolStripMenuItem4.Name = "recentModelToolStripMenuItem4";
			this.recentModelToolStripMenuItem4.Size = new global::System.Drawing.Size(150, 22);
			this.recentModelToolStripMenuItem4.Tag = "recent_model_4";
			this.recentModelToolStripMenuItem4.Text = "Recent Model";
			this.recentModelToolStripMenuItem5.Name = "recentModelToolStripMenuItem5";
			this.recentModelToolStripMenuItem5.Size = new global::System.Drawing.Size(150, 22);
			this.recentModelToolStripMenuItem5.Tag = "recent_model_5";
			this.recentModelToolStripMenuItem5.Text = "Recent Model";
			this.recentModelToolStripMenuItem6.Name = "recentModelToolStripMenuItem6";
			this.recentModelToolStripMenuItem6.Size = new global::System.Drawing.Size(150, 22);
			this.recentModelToolStripMenuItem6.Tag = "recent_model_6";
			this.recentModelToolStripMenuItem6.Text = "Recent Model";
			this.recentModelToolStripMenuItem7.Name = "recentModelToolStripMenuItem7";
			this.recentModelToolStripMenuItem7.Size = new global::System.Drawing.Size(150, 22);
			this.recentModelToolStripMenuItem7.Tag = "recent_model_7";
			this.recentModelToolStripMenuItem7.Text = "Recent Model";
			this.recentModelToolStripMenuItem8.Name = "recentModelToolStripMenuItem8";
			this.recentModelToolStripMenuItem8.Size = new global::System.Drawing.Size(150, 22);
			this.recentModelToolStripMenuItem8.Tag = "recent_model_8";
			this.recentModelToolStripMenuItem8.Text = "Recent Model";
			this.recentModelToolStripMenuItem9.Name = "recentModelToolStripMenuItem9";
			this.recentModelToolStripMenuItem9.Size = new global::System.Drawing.Size(150, 22);
			this.recentModelToolStripMenuItem9.Tag = "recent_model_9";
			this.recentModelToolStripMenuItem9.Text = "Recent Model";
			this.cameraToolStripMenuItem.DropDownItems.AddRange(new global::System.Windows.Forms.ToolStripItem[]
			{
				this.slowToolStripMenuItem,
				this.averageToolStripMenuItem,
				this.fastToolStripMenuItem,
				this.toolStripSeparator3,
				this.centerToolStripMenuItem,
				this.toolStripSeparator4,
				this.mayaToolStripMenuItem,
				this.fPSToolStripMenuItem
			});
			this.cameraToolStripMenuItem.Name = "cameraToolStripMenuItem";
			this.cameraToolStripMenuItem.Size = new global::System.Drawing.Size(56, 20);
			this.cameraToolStripMenuItem.Text = "Camera";
			this.slowToolStripMenuItem.Name = "slowToolStripMenuItem";
			this.slowToolStripMenuItem.Size = new global::System.Drawing.Size(158, 22);
			this.slowToolStripMenuItem.Tag = "toggle_camera_slow";
			this.slowToolStripMenuItem.Text = "Slow speed";
			this.averageToolStripMenuItem.Name = "averageToolStripMenuItem";
			this.averageToolStripMenuItem.Size = new global::System.Drawing.Size(158, 22);
			this.averageToolStripMenuItem.Tag = "toggle_camera_normal";
			this.averageToolStripMenuItem.Text = "Average speed";
			this.fastToolStripMenuItem.Name = "fastToolStripMenuItem";
			this.fastToolStripMenuItem.Size = new global::System.Drawing.Size(158, 22);
			this.fastToolStripMenuItem.Tag = "toggle_camera_fast";
			this.fastToolStripMenuItem.Text = "Fast speed";
			this.toolStripSeparator3.Name = "toolStripSeparator3";
			this.toolStripSeparator3.Size = new global::System.Drawing.Size(155, 6);
			this.centerToolStripMenuItem.Name = "centerToolStripMenuItem";
			this.centerToolStripMenuItem.Size = new global::System.Drawing.Size(158, 22);
			this.centerToolStripMenuItem.Tag = "toggle_camera_center";
			this.centerToolStripMenuItem.Text = "Center";
			this.toolStripSeparator4.Name = "toolStripSeparator4";
			this.toolStripSeparator4.Size = new global::System.Drawing.Size(155, 6);
			this.mayaToolStripMenuItem.Name = "mayaToolStripMenuItem";
			this.mayaToolStripMenuItem.Size = new global::System.Drawing.Size(158, 22);
			this.mayaToolStripMenuItem.Tag = "toggle_camera_maya";
			this.mayaToolStripMenuItem.Text = "Maya";
			this.fPSToolStripMenuItem.Name = "fPSToolStripMenuItem";
			this.fPSToolStripMenuItem.Size = new global::System.Drawing.Size(158, 22);
			this.fPSToolStripMenuItem.Tag = "toggle_camera_fps";
			this.fPSToolStripMenuItem.Text = "FPS";
			this.toolStrip.Items.AddRange(new global::System.Windows.Forms.ToolStripItem[]
			{
				this.OpenModelToolStripButton,
				this.SaveModelToolStripButton,
				this.SaveAsModelToolStripButton,
				this.toolStripSeparator5,
				this.toolStripButton1,
				this.toolStripButton2,
				this.toolStripButton3,
				this.toolStripButton4
			});
			this.toolStrip.Location = new global::System.Drawing.Point(0, 24);
			this.toolStrip.Name = "toolStrip";
			this.toolStrip.Size = new global::System.Drawing.Size(833, 25);
			this.toolStrip.TabIndex = 5;
			this.toolStrip.Text = "toolStrip1";
			this.OpenModelToolStripButton.DisplayStyle = global::System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.OpenModelToolStripButton.Image = global::MapEditor.Properties.Resources.open;
			this.OpenModelToolStripButton.ImageTransparentColor = global::System.Drawing.Color.Magenta;
			this.OpenModelToolStripButton.Name = "OpenModelToolStripButton";
			this.OpenModelToolStripButton.Size = new global::System.Drawing.Size(23, 22);
			this.OpenModelToolStripButton.Tag = "toggle_load_model";
			this.OpenModelToolStripButton.Text = "open";
			this.SaveModelToolStripButton.DisplayStyle = global::System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.SaveModelToolStripButton.Enabled = false;
			this.SaveModelToolStripButton.Image = global::MapEditor.Properties.Resources.save;
			this.SaveModelToolStripButton.ImageTransparentColor = global::System.Drawing.Color.Magenta;
			this.SaveModelToolStripButton.Name = "SaveModelToolStripButton";
			this.SaveModelToolStripButton.Size = new global::System.Drawing.Size(23, 22);
			this.SaveModelToolStripButton.Tag = "toggle_save_model";
			this.SaveModelToolStripButton.Text = "save";
			this.SaveModelToolStripButton.ToolTipText = "save";
			this.SaveAsModelToolStripButton.DisplayStyle = global::System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.SaveAsModelToolStripButton.Enabled = false;
			this.SaveAsModelToolStripButton.Image = (global::System.Drawing.Image)resources.GetObject("SaveAsModelToolStripButton.Image");
			this.SaveAsModelToolStripButton.ImageTransparentColor = global::System.Drawing.Color.Magenta;
			this.SaveAsModelToolStripButton.Name = "SaveAsModelToolStripButton";
			this.SaveAsModelToolStripButton.Size = new global::System.Drawing.Size(23, 22);
			this.SaveAsModelToolStripButton.Tag = "toggle_save_as_model";
			this.SaveAsModelToolStripButton.Text = "save as";
			this.toolStripSeparator5.Name = "toolStripSeparator5";
			this.toolStripSeparator5.Size = new global::System.Drawing.Size(6, 25);
			this.toolStripButton1.DisplayStyle = global::System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.toolStripButton1.Image = global::MapEditor.Properties.Resources.camera_slow;
			this.toolStripButton1.ImageTransparentColor = global::System.Drawing.Color.Magenta;
			this.toolStripButton1.Name = "toolStripButton1";
			this.toolStripButton1.Size = new global::System.Drawing.Size(23, 22);
			this.toolStripButton1.Tag = "toggle_camera_slow";
			this.toolStripButton1.Text = "Slow camera speed";
			this.toolStripButton2.DisplayStyle = global::System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.toolStripButton2.Image = global::MapEditor.Properties.Resources.camera_normal;
			this.toolStripButton2.ImageTransparentColor = global::System.Drawing.Color.Magenta;
			this.toolStripButton2.Name = "toolStripButton2";
			this.toolStripButton2.Size = new global::System.Drawing.Size(23, 22);
			this.toolStripButton2.Tag = "toggle_camera_normal";
			this.toolStripButton2.Text = "Normal camera speed";
			this.toolStripButton3.DisplayStyle = global::System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.toolStripButton3.Image = global::MapEditor.Properties.Resources.camera_fast;
			this.toolStripButton3.ImageTransparentColor = global::System.Drawing.Color.Magenta;
			this.toolStripButton3.Name = "toolStripButton3";
			this.toolStripButton3.Size = new global::System.Drawing.Size(23, 22);
			this.toolStripButton3.Tag = "toggle_camera_fast";
			this.toolStripButton3.Text = "Fast camera speed";
			this.toolStripButton4.DisplayStyle = global::System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.toolStripButton4.Image = global::MapEditor.Properties.Resources.camera_reset;
			this.toolStripButton4.ImageTransparentColor = global::System.Drawing.Color.Magenta;
			this.toolStripButton4.Name = "toolStripButton4";
			this.toolStripButton4.Size = new global::System.Drawing.Size(23, 22);
			this.toolStripButton4.Tag = "toggle_camera_center";
			this.toolStripButton4.Text = "Center camera";
			this.selectVisMobExtElemControl.BorderStyle = global::System.Windows.Forms.BorderStyle.FixedSingle;
			this.selectVisMobExtElemControl.Enabled = false;
			this.selectVisMobExtElemControl.Location = new global::System.Drawing.Point(0, 54);
			this.selectVisMobExtElemControl.Name = "selectVisMobExtElemControl";
			this.selectVisMobExtElemControl.Size = new global::System.Drawing.Size(232, 24);
			this.selectVisMobExtElemControl.TabIndex = 104;
			this.changeCharacterControl.Anchor = (global::System.Windows.Forms.AnchorStyles.Bottom | global::System.Windows.Forms.AnchorStyles.Left | global::System.Windows.Forms.AnchorStyles.Right);
			this.changeCharacterControl.BorderStyle = global::System.Windows.Forms.BorderStyle.FixedSingle;
			this.changeCharacterControl.Enabled = false;
			this.changeCharacterControl.Location = new global::System.Drawing.Point(0, 624);
			this.changeCharacterControl.Name = "changeCharacterControl";
			this.changeCharacterControl.Size = new global::System.Drawing.Size(830, 33);
			this.changeCharacterControl.TabIndex = 105;
			this.presets.Location = new global::System.Drawing.Point(75, -1);
			this.presets.Name = "presets";
			this.presets.ReadOnly = false;
			this.presets.Size = new global::System.Drawing.Size(152, 24);
			this.presets.TabIndex = 106;
			this.panel1.BorderStyle = global::System.Windows.Forms.BorderStyle.FixedSingle;
			this.panel1.Controls.Add(this.presets);
			this.panel1.Controls.Add(this.label2);
			this.panel1.Location = new global::System.Drawing.Point(0, 448);
			this.panel1.Name = "panel1";
			this.panel1.Size = new global::System.Drawing.Size(232, 27);
			this.panel1.TabIndex = 106;
			this.label2.AutoSize = true;
			this.label2.Location = new global::System.Drawing.Point(2, 5);
			this.label2.Name = "label2";
			this.label2.Size = new global::System.Drawing.Size(63, 13);
			this.label2.TabIndex = 0;
			this.label2.Text = "Proportions:";
			this.randomizeControl.BorderStyle = global::System.Windows.Forms.BorderStyle.FixedSingle;
			this.randomizeControl.Location = new global::System.Drawing.Point(0, 476);
			this.randomizeControl.Name = "randomizeControl";
			this.randomizeControl.Size = new global::System.Drawing.Size(232, 99);
			this.randomizeControl.TabIndex = 107;
			base.AutoScaleDimensions = new global::System.Drawing.SizeF(6f, 13f);
			base.AutoScaleMode = global::System.Windows.Forms.AutoScaleMode.Font;
			base.ClientSize = new global::System.Drawing.Size(833, 657);
			base.Controls.Add(this.randomizeControl);
			base.Controls.Add(this.panel1);
			base.Controls.Add(this.OtherPanel);
			base.Controls.Add(this.changeCharacterControl);
			base.Controls.Add(this.selectVisMobExtElemControl);
			base.Controls.Add(this.characterBasicVarControl);
			base.Controls.Add(this.toolStrip);
			base.Controls.Add(this.menuStrip);
			base.Controls.Add(this.EquipmentControl);
			base.Controls.Add(this.ScenePanel);
			base.Name = "ModelViewerForm";
			this.Text = "Model Viewer";
			this.OtherPanel.ResumeLayout(false);
			this.OtherPanel.PerformLayout();
			this.menuStrip.ResumeLayout(false);
			this.menuStrip.PerformLayout();
			this.toolStrip.ResumeLayout(false);
			this.toolStrip.PerformLayout();
			this.panel1.ResumeLayout(false);
			this.panel1.PerformLayout();
			base.ResumeLayout(false);
			base.PerformLayout();
		}

		// Token: 0x040001CD RID: 461
		private global::System.ComponentModel.IContainer components;

		// Token: 0x040001CE RID: 462
		private global::System.Windows.Forms.Panel ScenePanel;

		// Token: 0x040001CF RID: 463
		private global::System.Windows.Forms.Timer SceneTimer;

		// Token: 0x040001D0 RID: 464
		private global::Tools.ModelViewerElements.Controls.Equipment.EquipmentControl EquipmentControl;

		// Token: 0x040001D1 RID: 465
		private global::Tools.ModelViewerElements.Controls.CharacterBasicVar.CharacterBasicVarControl characterBasicVarControl;

		// Token: 0x040001D2 RID: 466
		private global::System.Windows.Forms.Panel OtherPanel;

		// Token: 0x040001D3 RID: 467
		private global::System.Windows.Forms.Label label1;

		// Token: 0x040001D4 RID: 468
		private global::System.Windows.Forms.TextBox ScaleTextBox;

		// Token: 0x040001D5 RID: 469
		private global::System.Windows.Forms.Timer ScaleFieldTimer;

		// Token: 0x040001D6 RID: 470
		private global::System.Windows.Forms.MenuStrip menuStrip;

		// Token: 0x040001D7 RID: 471
		private global::System.Windows.Forms.ToolStripMenuItem modelToolStripMenuItem;

		// Token: 0x040001D8 RID: 472
		private global::System.Windows.Forms.ToolStripMenuItem loadToolStripMenuItem;

		// Token: 0x040001D9 RID: 473
		private global::System.Windows.Forms.ToolStrip toolStrip;

		// Token: 0x040001DA RID: 474
		private global::System.Windows.Forms.ToolStripButton OpenModelToolStripButton;

		// Token: 0x040001DB RID: 475
		private global::System.Windows.Forms.ToolStripButton SaveModelToolStripButton;

		// Token: 0x040001DC RID: 476
		private global::System.Windows.Forms.ToolStripButton SaveAsModelToolStripButton;

		// Token: 0x040001DD RID: 477
		private global::System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem;

		// Token: 0x040001DE RID: 478
		private global::System.Windows.Forms.ToolStripMenuItem saveAsToolStripMenuItem;

		// Token: 0x040001DF RID: 479
		private global::System.Windows.Forms.ToolStripSeparator toolStripSeparator1;

		// Token: 0x040001E0 RID: 480
		private global::System.Windows.Forms.ToolStripMenuItem recentListToolStripMenuItem;

		// Token: 0x040001E1 RID: 481
		private global::System.Windows.Forms.ToolStripMenuItem recentModelToolStripMenuItem;

		// Token: 0x040001E2 RID: 482
		private global::System.Windows.Forms.ToolStripMenuItem recentModelToolStripMenuItem1;

		// Token: 0x040001E3 RID: 483
		private global::System.Windows.Forms.ToolStripMenuItem recentModelToolStripMenuItem2;

		// Token: 0x040001E4 RID: 484
		private global::System.Windows.Forms.ToolStripMenuItem recentModelToolStripMenuItem3;

		// Token: 0x040001E5 RID: 485
		private global::System.Windows.Forms.ToolStripMenuItem recentModelToolStripMenuItem4;

		// Token: 0x040001E6 RID: 486
		private global::System.Windows.Forms.ToolStripMenuItem recentModelToolStripMenuItem5;

		// Token: 0x040001E7 RID: 487
		private global::System.Windows.Forms.ToolStripMenuItem recentModelToolStripMenuItem6;

		// Token: 0x040001E8 RID: 488
		private global::System.Windows.Forms.ToolStripMenuItem recentModelToolStripMenuItem7;

		// Token: 0x040001E9 RID: 489
		private global::System.Windows.Forms.ToolStripMenuItem recentModelToolStripMenuItem8;

		// Token: 0x040001EA RID: 490
		private global::System.Windows.Forms.ToolStripMenuItem recentModelToolStripMenuItem9;

		// Token: 0x040001EB RID: 491
		private global::System.Windows.Forms.ToolStripMenuItem cameraToolStripMenuItem;

		// Token: 0x040001EC RID: 492
		private global::System.Windows.Forms.ToolStripMenuItem slowToolStripMenuItem;

		// Token: 0x040001ED RID: 493
		private global::System.Windows.Forms.ToolStripMenuItem averageToolStripMenuItem;

		// Token: 0x040001EE RID: 494
		private global::System.Windows.Forms.ToolStripMenuItem fastToolStripMenuItem;

		// Token: 0x040001EF RID: 495
		private global::System.Windows.Forms.ToolStripSeparator toolStripSeparator3;

		// Token: 0x040001F0 RID: 496
		private global::System.Windows.Forms.ToolStripMenuItem centerToolStripMenuItem;

		// Token: 0x040001F1 RID: 497
		private global::System.Windows.Forms.ToolStripSeparator toolStripSeparator4;

		// Token: 0x040001F2 RID: 498
		private global::System.Windows.Forms.ToolStripMenuItem mayaToolStripMenuItem;

		// Token: 0x040001F3 RID: 499
		private global::System.Windows.Forms.ToolStripMenuItem fPSToolStripMenuItem;

		// Token: 0x040001F4 RID: 500
		private global::System.Windows.Forms.ToolStripSeparator toolStripSeparator5;

		// Token: 0x040001F5 RID: 501
		private global::System.Windows.Forms.ToolStripButton toolStripButton1;

		// Token: 0x040001F6 RID: 502
		private global::System.Windows.Forms.ToolStripButton toolStripButton2;

		// Token: 0x040001F7 RID: 503
		private global::System.Windows.Forms.ToolStripButton toolStripButton3;

		// Token: 0x040001F8 RID: 504
		private global::System.Windows.Forms.ToolStripButton toolStripButton4;

		// Token: 0x040001F9 RID: 505
		private global::Tools.ModelViewerElements.Controls.SelectVisMobExtElem.SelectVisMobExtElemControl selectVisMobExtElemControl;

		// Token: 0x040001FA RID: 506
		private global::Tools.ModelViewerElements.Controls.ChangeCharacter.ChangeCharacterControl changeCharacterControl;

		// Token: 0x040001FB RID: 507
		private global::Tools.ModelViewerElements.Controls.CharacterProportionsVar.CharacterProportionPresetsControl presets;

		// Token: 0x040001FC RID: 508
		private global::System.Windows.Forms.Panel panel1;

		// Token: 0x040001FD RID: 509
		private global::System.Windows.Forms.Label label2;

		// Token: 0x040001FE RID: 510
		private global::Tools.ModelViewerElements.Controls.RandomizeParams.RandomizeControl randomizeControl;
	}
}
