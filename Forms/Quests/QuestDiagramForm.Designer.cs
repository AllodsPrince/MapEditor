namespace MapEditor.Forms.Quests
{
	// Token: 0x02000268 RID: 616
	public partial class QuestDiagramForm : global::MapEditor.Forms.Base.BaseForm
	{
		// Token: 0x06001D37 RID: 7479 RVA: 0x000BAB6F File Offset: 0x000B9B6F
		protected override void Dispose(bool disposing)
		{
			if (disposing && this.components != null)
			{
				this.components.Dispose();
			}
			base.Dispose(disposing);
		}

		// Token: 0x06001D38 RID: 7480 RVA: 0x000BAB90 File Offset: 0x000B9B90
		private void InitializeComponent()
		{
			global::System.ComponentModel.ComponentResourceManager resources = new global::System.ComponentModel.ComponentResourceManager(typeof(global::MapEditor.Forms.Quests.QuestDiagramForm));
			this.ZoneComboBox = new global::System.Windows.Forms.ComboBox();
			this.label1 = new global::System.Windows.Forms.Label();
			this.SaveButton = new global::System.Windows.Forms.Button();
			this.QuestDiagram = new global::Tools.Diagram.Diagram();
			this.CloseButton = new global::System.Windows.Forms.Button();
			this.richTextBox1 = new global::System.Windows.Forms.RichTextBox();
			this.singleQuestsCheckBox = new global::System.Windows.Forms.CheckBox();
			this.questLinesCheckBox = new global::System.Windows.Forms.CheckBox();
			this.pvpQuestsCheckBox = new global::System.Windows.Forms.CheckBox();
			this.label2 = new global::System.Windows.Forms.Label();
			this.plotLineColorLabel = new global::System.Windows.Forms.Label();
			this.comlexComditionColorLabel = new global::System.Windows.Forms.Label();
			this.label5 = new global::System.Windows.Forms.Label();
			this.snapCheckBox = new global::System.Windows.Forms.CheckBox();
			this.disabledQuestColorLabel = new global::System.Windows.Forms.Label();
			this.label4 = new global::System.Windows.Forms.Label();
			base.SuspendLayout();
			this.ZoneComboBox.DropDownStyle = global::System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.ZoneComboBox.FormattingEnabled = true;
			resources.ApplyResources(this.ZoneComboBox, "ZoneComboBox");
			this.ZoneComboBox.Name = "ZoneComboBox";
			this.ZoneComboBox.Sorted = true;
			resources.ApplyResources(this.label1, "label1");
			this.label1.Name = "label1";
			this.label1.Text = "Zone";
			resources.ApplyResources(this.SaveButton, "SaveButton");
			this.SaveButton.Name = "SaveButton";
			this.SaveButton.Text = "Save";
			this.SaveButton.UseVisualStyleBackColor = true;
			this.QuestDiagram.AllowUserDeleteEdge = true;
			resources.ApplyResources(this.QuestDiagram, "QuestDiagram");
			this.QuestDiagram.BorderStyle = global::System.Windows.Forms.BorderStyle.Fixed3D;
			this.QuestDiagram.CaptionFont = new global::System.Drawing.Font("Microsoft Sans Serif", 8.25f, global::System.Drawing.FontStyle.Regular, global::System.Drawing.GraphicsUnit.Point, 204);
			this.QuestDiagram.Name = "QuestDiagram";
			this.QuestDiagram.SelectedDiagramMember = null;
			this.QuestDiagram.Snap = false;
			this.QuestDiagram.TextFont = new global::System.Drawing.Font("Microsoft Sans Serif", 8.25f, global::System.Drawing.FontStyle.Regular, global::System.Drawing.GraphicsUnit.Point, 204);
			resources.ApplyResources(this.CloseButton, "CloseButton");
			this.CloseButton.DialogResult = global::System.Windows.Forms.DialogResult.Cancel;
			this.CloseButton.ImeMode = global::System.Windows.Forms.ImeMode.NoControl;
			this.CloseButton.Name = "CloseButton";
			this.CloseButton.Text = "Close";
			this.CloseButton.UseVisualStyleBackColor = true;
			resources.ApplyResources(this.richTextBox1, "richTextBox1");
			this.richTextBox1.Name = "richTextBox1";
			this.richTextBox1.ReadOnly = true;
			this.singleQuestsCheckBox.AutoSize = true;
			this.singleQuestsCheckBox.Checked = true;
			this.singleQuestsCheckBox.CheckState = global::System.Windows.Forms.CheckState.Checked;
			resources.ApplyResources(this.singleQuestsCheckBox, "singleQuestsCheckBox");
			this.singleQuestsCheckBox.Name = "singleQuestsCheckBox";
			this.singleQuestsCheckBox.Text = "Single quests";
			this.singleQuestsCheckBox.UseVisualStyleBackColor = true;
			this.questLinesCheckBox.AutoSize = true;
			this.questLinesCheckBox.Checked = true;
			this.questLinesCheckBox.CheckState = global::System.Windows.Forms.CheckState.Checked;
			this.questLinesCheckBox.ImeMode = global::System.Windows.Forms.ImeMode.NoControl;
			resources.ApplyResources(this.questLinesCheckBox, "questLinesCheckBox");
			this.questLinesCheckBox.Name = "questLinesCheckBox";
			this.questLinesCheckBox.Text = "Quest lines";
			this.questLinesCheckBox.UseVisualStyleBackColor = true;
			this.pvpQuestsCheckBox.AutoSize = true;
			this.pvpQuestsCheckBox.Checked = true;
			this.pvpQuestsCheckBox.CheckState = global::System.Windows.Forms.CheckState.Checked;
			this.pvpQuestsCheckBox.ImeMode = global::System.Windows.Forms.ImeMode.NoControl;
			resources.ApplyResources(this.pvpQuestsCheckBox, "pvpQuestsCheckBox");
			this.pvpQuestsCheckBox.Name = "pvpQuestsCheckBox";
			this.pvpQuestsCheckBox.Text = "PVP quests";
			this.pvpQuestsCheckBox.UseVisualStyleBackColor = true;
			resources.ApplyResources(this.label2, "label2");
			this.label2.Name = "label2";
			this.label2.Text = "Plot line quest:";
			this.plotLineColorLabel.BackColor = global::System.Drawing.Color.RosyBrown;
			resources.ApplyResources(this.plotLineColorLabel, "plotLineColorLabel");
			this.plotLineColorLabel.Name = "plotLineColorLabel";
			this.comlexComditionColorLabel.BackColor = global::System.Drawing.Color.DarkOrange;
			this.comlexComditionColorLabel.ImeMode = global::System.Windows.Forms.ImeMode.NoControl;
			resources.ApplyResources(this.comlexComditionColorLabel, "comlexComditionColorLabel");
			this.comlexComditionColorLabel.Name = "comlexComditionColorLabel";
			resources.ApplyResources(this.label5, "label5");
			this.label5.ImeMode = global::System.Windows.Forms.ImeMode.NoControl;
			this.label5.Name = "label5";
			this.label5.Text = "Complex start conditions:";
			this.snapCheckBox.AutoSize = true;
			resources.ApplyResources(this.snapCheckBox, "snapCheckBox");
			this.snapCheckBox.Name = "snapCheckBox";
			this.snapCheckBox.Text = "Snap";
			this.snapCheckBox.UseVisualStyleBackColor = true;
			this.disabledQuestColorLabel.BackColor = global::System.Drawing.Color.LightGray;
			this.disabledQuestColorLabel.ImeMode = global::System.Windows.Forms.ImeMode.NoControl;
			resources.ApplyResources(this.disabledQuestColorLabel, "disabledQuestColorLabel");
			this.disabledQuestColorLabel.Name = "disabledQuestColorLabel";
			resources.ApplyResources(this.label4, "label4");
			this.label4.ImeMode = global::System.Windows.Forms.ImeMode.NoControl;
			this.label4.Name = "label4";
			this.label4.Text = "Disabled quest:";
			resources.ApplyResources(this, "$this");
			base.AutoScaleMode = global::System.Windows.Forms.AutoScaleMode.Font;
			base.CancelButton = this.CloseButton;
			base.Controls.Add(this.disabledQuestColorLabel);
			base.Controls.Add(this.label4);
			base.Controls.Add(this.snapCheckBox);
			base.Controls.Add(this.comlexComditionColorLabel);
			base.Controls.Add(this.label5);
			base.Controls.Add(this.plotLineColorLabel);
			base.Controls.Add(this.label2);
			base.Controls.Add(this.pvpQuestsCheckBox);
			base.Controls.Add(this.questLinesCheckBox);
			base.Controls.Add(this.singleQuestsCheckBox);
			base.Controls.Add(this.richTextBox1);
			base.Controls.Add(this.CloseButton);
			base.Controls.Add(this.SaveButton);
			base.Controls.Add(this.label1);
			base.Controls.Add(this.ZoneComboBox);
			base.Controls.Add(this.QuestDiagram);
			base.Name = "QuestDiagramForm";
			this.Text = "Quest Diagram";
			base.ResumeLayout(false);
			base.PerformLayout();
		}

		// Token: 0x04001289 RID: 4745
		private global::System.ComponentModel.IContainer components;

		// Token: 0x0400128A RID: 4746
		private global::Tools.Diagram.Diagram QuestDiagram;

		// Token: 0x0400128B RID: 4747
		private global::System.Windows.Forms.ComboBox ZoneComboBox;

		// Token: 0x0400128C RID: 4748
		private global::System.Windows.Forms.Label label1;

		// Token: 0x0400128D RID: 4749
		private global::System.Windows.Forms.Button SaveButton;

		// Token: 0x0400128E RID: 4750
		private global::System.Windows.Forms.Button CloseButton;

		// Token: 0x0400128F RID: 4751
		private global::System.Windows.Forms.RichTextBox richTextBox1;

		// Token: 0x04001290 RID: 4752
		private global::System.Windows.Forms.CheckBox singleQuestsCheckBox;

		// Token: 0x04001291 RID: 4753
		private global::System.Windows.Forms.CheckBox questLinesCheckBox;

		// Token: 0x04001292 RID: 4754
		private global::System.Windows.Forms.CheckBox pvpQuestsCheckBox;

		// Token: 0x04001293 RID: 4755
		private global::System.Windows.Forms.Label label2;

		// Token: 0x04001294 RID: 4756
		private global::System.Windows.Forms.Label plotLineColorLabel;

		// Token: 0x04001295 RID: 4757
		private global::System.Windows.Forms.Label comlexComditionColorLabel;

		// Token: 0x04001296 RID: 4758
		private global::System.Windows.Forms.Label label5;

		// Token: 0x04001297 RID: 4759
		private global::System.Windows.Forms.CheckBox snapCheckBox;

		// Token: 0x04001298 RID: 4760
		private global::System.Windows.Forms.Label disabledQuestColorLabel;

		// Token: 0x04001299 RID: 4761
		private global::System.Windows.Forms.Label label4;
	}
}
