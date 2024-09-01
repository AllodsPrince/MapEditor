namespace MapEditor.Forms.SoundStaticObject
{
	// Token: 0x02000052 RID: 82
	public partial class SoundStaticObjectForm : global::MapEditor.Forms.Base.BaseForm
	{
		// Token: 0x06000437 RID: 1079 RVA: 0x0002357B File Offset: 0x0002257B
		protected override void Dispose(bool disposing)
		{
			if (disposing && this.components != null)
			{
				this.components.Dispose();
			}
			base.Dispose(disposing);
		}

		// Token: 0x06000438 RID: 1080 RVA: 0x0002359C File Offset: 0x0002259C
		private void InitializeComponent()
		{
			this.brouseButton = new global::System.Windows.Forms.Button();
			this.label1 = new global::System.Windows.Forms.Label();
			this.label2 = new global::System.Windows.Forms.Label();
			this.label3 = new global::System.Windows.Forms.Label();
			this.projectTextBox = new global::System.Windows.Forms.TextBox();
			this.eventTextBox = new global::System.Windows.Forms.TextBox();
			this.label4 = new global::System.Windows.Forms.Label();
			this.resultObjNameTextBox = new global::System.Windows.Forms.TextBox();
			this.createButton = new global::System.Windows.Forms.Button();
			base.SuspendLayout();
			this.brouseButton.Anchor = (global::System.Windows.Forms.AnchorStyles.Top | global::System.Windows.Forms.AnchorStyles.Right);
			this.brouseButton.Location = new global::System.Drawing.Point(467, 5);
			this.brouseButton.Name = "brouseButton";
			this.brouseButton.Size = new global::System.Drawing.Size(65, 20);
			this.brouseButton.TabIndex = 2;
			this.brouseButton.Text = "Browse ...";
			this.brouseButton.UseVisualStyleBackColor = true;
			this.label1.AutoSize = true;
			this.label1.Font = new global::System.Drawing.Font("Microsoft Sans Serif", 8.25f, global::System.Drawing.FontStyle.Bold, global::System.Drawing.GraphicsUnit.Point, 204);
			this.label1.Location = new global::System.Drawing.Point(3, 9);
			this.label1.Name = "label1";
			this.label1.Size = new global::System.Drawing.Size(129, 13);
			this.label1.TabIndex = 3;
			this.label1.Text = "Brose or drop source:";
			this.label2.AutoSize = true;
			this.label2.Location = new global::System.Drawing.Point(3, 32);
			this.label2.Name = "label2";
			this.label2.Size = new global::System.Drawing.Size(76, 13);
			this.label2.TabIndex = 4;
			this.label2.Text = "FMOD project:";
			this.label3.AutoSize = true;
			this.label3.Location = new global::System.Drawing.Point(3, 54);
			this.label3.Name = "label3";
			this.label3.Size = new global::System.Drawing.Size(38, 13);
			this.label3.TabIndex = 5;
			this.label3.Text = "Event:";
			this.projectTextBox.Anchor = (global::System.Windows.Forms.AnchorStyles.Top | global::System.Windows.Forms.AnchorStyles.Left | global::System.Windows.Forms.AnchorStyles.Right);
			this.projectTextBox.Location = new global::System.Drawing.Point(98, 29);
			this.projectTextBox.Name = "projectTextBox";
			this.projectTextBox.ReadOnly = true;
			this.projectTextBox.Size = new global::System.Drawing.Size(434, 20);
			this.projectTextBox.TabIndex = 6;
			this.eventTextBox.Anchor = (global::System.Windows.Forms.AnchorStyles.Top | global::System.Windows.Forms.AnchorStyles.Left | global::System.Windows.Forms.AnchorStyles.Right);
			this.eventTextBox.Location = new global::System.Drawing.Point(98, 51);
			this.eventTextBox.Name = "eventTextBox";
			this.eventTextBox.ReadOnly = true;
			this.eventTextBox.Size = new global::System.Drawing.Size(434, 20);
			this.eventTextBox.TabIndex = 7;
			this.label4.AutoSize = true;
			this.label4.Location = new global::System.Drawing.Point(3, 89);
			this.label4.Name = "label4";
			this.label4.Size = new global::System.Drawing.Size(89, 13);
			this.label4.TabIndex = 8;
			this.label4.Text = "Result obj. name:";
			this.resultObjNameTextBox.Anchor = (global::System.Windows.Forms.AnchorStyles.Top | global::System.Windows.Forms.AnchorStyles.Left | global::System.Windows.Forms.AnchorStyles.Right);
			this.resultObjNameTextBox.Location = new global::System.Drawing.Point(98, 86);
			this.resultObjNameTextBox.Name = "resultObjNameTextBox";
			this.resultObjNameTextBox.Size = new global::System.Drawing.Size(434, 20);
			this.resultObjNameTextBox.TabIndex = 9;
			this.createButton.Location = new global::System.Drawing.Point(6, 124);
			this.createButton.Name = "createButton";
			this.createButton.Size = new global::System.Drawing.Size(65, 20);
			this.createButton.TabIndex = 10;
			this.createButton.Text = "Create";
			this.createButton.UseVisualStyleBackColor = true;
			this.AllowDrop = true;
			base.AutoScaleDimensions = new global::System.Drawing.SizeF(6f, 13f);
			base.AutoScaleMode = global::System.Windows.Forms.AutoScaleMode.Font;
			base.ClientSize = new global::System.Drawing.Size(535, 150);
			base.Controls.Add(this.createButton);
			base.Controls.Add(this.resultObjNameTextBox);
			base.Controls.Add(this.label4);
			base.Controls.Add(this.eventTextBox);
			base.Controls.Add(this.projectTextBox);
			base.Controls.Add(this.label3);
			base.Controls.Add(this.label2);
			base.Controls.Add(this.label1);
			base.Controls.Add(this.brouseButton);
			base.Name = "SoundStaticObjectForm";
			this.Text = "Sound Static Object Wizard";
			base.ResumeLayout(false);
			base.PerformLayout();
		}

		// Token: 0x040002FD RID: 765
		private global::System.ComponentModel.IContainer components;

		// Token: 0x040002FE RID: 766
		private global::System.Windows.Forms.Button brouseButton;

		// Token: 0x040002FF RID: 767
		private global::System.Windows.Forms.Label label1;

		// Token: 0x04000300 RID: 768
		private global::System.Windows.Forms.Label label2;

		// Token: 0x04000301 RID: 769
		private global::System.Windows.Forms.Label label3;

		// Token: 0x04000302 RID: 770
		private global::System.Windows.Forms.TextBox projectTextBox;

		// Token: 0x04000303 RID: 771
		private global::System.Windows.Forms.TextBox eventTextBox;

		// Token: 0x04000304 RID: 772
		private global::System.Windows.Forms.Label label4;

		// Token: 0x04000305 RID: 773
		private global::System.Windows.Forms.TextBox resultObjNameTextBox;

		// Token: 0x04000306 RID: 774
		private global::System.Windows.Forms.Button createButton;
	}
}
