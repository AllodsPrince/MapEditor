namespace MapEditor.Forms.Quests.QuestLine
{
	// Token: 0x020001B6 RID: 438
	public partial class QuestLineForm : global::System.Windows.Forms.Form
	{
		// Token: 0x06001539 RID: 5433 RVA: 0x00099EE8 File Offset: 0x00098EE8
		protected override void Dispose(bool disposing)
		{
			if (disposing && this.components != null)
			{
				this.components.Dispose();
			}
			base.Dispose(disposing);
		}

		// Token: 0x0600153A RID: 5434 RVA: 0x00099F08 File Offset: 0x00098F08
		private void InitializeComponent()
		{
			this.QuestDiagram = new global::Tools.Diagram.Diagram();
			base.SuspendLayout();
			this.QuestDiagram.AllowUserDeleteEdge = true;
			this.QuestDiagram.AutoScroll = true;
			this.QuestDiagram.BackColor = global::System.Drawing.SystemColors.ControlLightLight;
			this.QuestDiagram.BorderStyle = global::System.Windows.Forms.BorderStyle.Fixed3D;
			this.QuestDiagram.CaptionFont = new global::System.Drawing.Font("Microsoft Sans Serif", 8.25f, global::System.Drawing.FontStyle.Regular, global::System.Drawing.GraphicsUnit.Point, 204);
			this.QuestDiagram.Dock = global::System.Windows.Forms.DockStyle.Fill;
			this.QuestDiagram.Location = new global::System.Drawing.Point(0, 0);
			this.QuestDiagram.Name = "QuestDiagram";
			this.QuestDiagram.SelectedDiagramMember = null;
			this.QuestDiagram.Size = new global::System.Drawing.Size(292, 273);
			this.QuestDiagram.TabIndex = 1;
			this.QuestDiagram.TextFont = new global::System.Drawing.Font("Microsoft Sans Serif", 8.25f, global::System.Drawing.FontStyle.Regular, global::System.Drawing.GraphicsUnit.Point, 204);
			base.AutoScaleDimensions = new global::System.Drawing.SizeF(6f, 13f);
			base.AutoScaleMode = global::System.Windows.Forms.AutoScaleMode.Font;
			base.ClientSize = new global::System.Drawing.Size(292, 273);
			base.Controls.Add(this.QuestDiagram);
			base.Name = "QuestLineForm";
			this.Text = "Quest Line View";
			base.ResumeLayout(false);
		}

		// Token: 0x04000F04 RID: 3844
		private global::System.ComponentModel.IContainer components;

		// Token: 0x04000F05 RID: 3845
		private global::Tools.Diagram.Diagram QuestDiagram;
	}
}
