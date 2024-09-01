namespace MapEditor.Forms.ServerLuncher
{
	// Token: 0x0200006F RID: 111
	public partial class ServerLuncherForm : global::MapEditor.Forms.Base.BaseForm
	{
		// Token: 0x06000585 RID: 1413 RVA: 0x0002D40D File Offset: 0x0002C40D
		protected override void Dispose(bool disposing)
		{
			if (disposing && this.components != null)
			{
				this.components.Dispose();
			}
			base.Dispose(disposing);
		}

		// Token: 0x06000586 RID: 1414 RVA: 0x0002D42C File Offset: 0x0002C42C
		private void InitializeComponent()
		{
			global::System.ComponentModel.ComponentResourceManager resources = new global::System.ComponentModel.ComponentResourceManager(typeof(global::MapEditor.Forms.ServerLuncher.ServerLuncherForm));
			this.textBox1 = new global::System.Windows.Forms.TextBox();
			this.textBox2 = new global::System.Windows.Forms.TextBox();
			this.panel1 = new global::System.Windows.Forms.Panel();
			this.label3 = new global::System.Windows.Forms.Label();
			this.label2 = new global::System.Windows.Forms.Label();
			this.label1 = new global::System.Windows.Forms.Label();
			this.panel2 = new global::System.Windows.Forms.Panel();
			this.button1 = new global::System.Windows.Forms.Button();
			this.button2 = new global::System.Windows.Forms.Button();
			this.Cancel = new global::System.Windows.Forms.Button();
			this.button3 = new global::System.Windows.Forms.Button();
			this.textBoxLog = new global::System.Windows.Forms.TextBox();
			this.textBox3 = new global::System.Windows.Forms.TextBox();
			this.label4 = new global::System.Windows.Forms.Label();
			this.textBox4 = new global::System.Windows.Forms.TextBox();
			this.textBox5 = new global::System.Windows.Forms.TextBox();
			this.label5 = new global::System.Windows.Forms.Label();
			this.panel1.SuspendLayout();
			this.panel2.SuspendLayout();
			base.SuspendLayout();
			resources.ApplyResources(this.textBox1, "textBox1");
			this.textBox1.Name = "textBox1";
			resources.ApplyResources(this.textBox2, "textBox2");
			this.textBox2.Name = "textBox2";
			this.panel1.Controls.Add(this.label5);
			this.panel1.Controls.Add(this.textBox5);
			this.panel1.Controls.Add(this.label4);
			this.panel1.Controls.Add(this.textBox3);
			this.panel1.Controls.Add(this.label3);
			this.panel1.Controls.Add(this.label2);
			this.panel1.Controls.Add(this.label1);
			this.panel1.Controls.Add(this.textBox1);
			this.panel1.Controls.Add(this.textBox2);
			resources.ApplyResources(this.panel1, "panel1");
			this.panel1.Name = "panel1";
			resources.ApplyResources(this.label3, "label3");
			this.label3.Name = "label3";
			resources.ApplyResources(this.label2, "label2");
			this.label2.Name = "label2";
			resources.ApplyResources(this.label1, "label1");
			this.label1.Name = "label1";
			this.panel2.Controls.Add(this.textBoxLog);
			resources.ApplyResources(this.panel2, "panel2");
			this.panel2.Name = "panel2";
			resources.ApplyResources(this.button1, "button1");
			this.button1.Name = "button1";
			this.button1.UseVisualStyleBackColor = true;
			this.button1.Click += new global::System.EventHandler(this.button1_Click);
			resources.ApplyResources(this.button2, "button2");
			this.button2.Name = "button2";
			this.button2.UseVisualStyleBackColor = true;
			resources.ApplyResources(this.Cancel, "Cancel");
			this.Cancel.Name = "Cancel";
			this.Cancel.UseVisualStyleBackColor = true;
			this.Cancel.Click += new global::System.EventHandler(this.Cancel_Click);
			resources.ApplyResources(this.button3, "button3");
			this.button3.Name = "button3";
			this.button3.UseVisualStyleBackColor = true;
			this.textBoxLog.BackColor = global::System.Drawing.Color.FromArgb(255, 224, 192);
			resources.ApplyResources(this.textBoxLog, "textBoxLog");
			this.textBoxLog.Name = "textBoxLog";
			resources.ApplyResources(this.textBox3, "textBox3");
			this.textBox3.Name = "textBox3";
			resources.ApplyResources(this.label4, "label4");
			this.label4.Name = "label4";
			this.textBox4.BackColor = global::System.Drawing.Color.FromArgb(255, 128, 128);
			resources.ApplyResources(this.textBox4, "textBox4");
			this.textBox4.Name = "textBox4";
			resources.ApplyResources(this.textBox5, "textBox5");
			this.textBox5.Name = "textBox5";
			resources.ApplyResources(this.label5, "label5");
			this.label5.Name = "label5";
			resources.ApplyResources(this, "$this");
			base.AutoScaleMode = global::System.Windows.Forms.AutoScaleMode.Font;
			base.Controls.Add(this.textBox4);
			base.Controls.Add(this.button3);
			base.Controls.Add(this.Cancel);
			base.Controls.Add(this.button2);
			base.Controls.Add(this.button1);
			base.Controls.Add(this.panel2);
			base.Controls.Add(this.panel1);
			base.Name = "ServerLuncherForm";
			this.panel1.ResumeLayout(false);
			this.panel1.PerformLayout();
			this.panel2.ResumeLayout(false);
			this.panel2.PerformLayout();
			base.ResumeLayout(false);
			base.PerformLayout();
		}

		// Token: 0x0400041A RID: 1050
		private global::System.ComponentModel.IContainer components;

		// Token: 0x0400041B RID: 1051
		private global::System.Windows.Forms.TextBox textBox1;

		// Token: 0x0400041C RID: 1052
		private global::System.Windows.Forms.TextBox textBox2;

		// Token: 0x0400041D RID: 1053
		private global::System.Windows.Forms.Panel panel1;

		// Token: 0x0400041E RID: 1054
		private global::System.Windows.Forms.Label label3;

		// Token: 0x0400041F RID: 1055
		private global::System.Windows.Forms.Label label2;

		// Token: 0x04000420 RID: 1056
		private global::System.Windows.Forms.Label label1;

		// Token: 0x04000421 RID: 1057
		private global::System.Windows.Forms.Panel panel2;

		// Token: 0x04000422 RID: 1058
		private global::System.Windows.Forms.Button button1;

		// Token: 0x04000423 RID: 1059
		private global::System.Windows.Forms.Button button2;

		// Token: 0x04000424 RID: 1060
		private global::System.Windows.Forms.Button Cancel;

		// Token: 0x04000425 RID: 1061
		private global::System.Windows.Forms.Button button3;

		// Token: 0x04000426 RID: 1062
		private global::System.Windows.Forms.TextBox textBoxLog;

		// Token: 0x04000427 RID: 1063
		private global::System.Windows.Forms.Label label4;

		// Token: 0x04000428 RID: 1064
		private global::System.Windows.Forms.TextBox textBox3;

		// Token: 0x04000429 RID: 1065
		private global::System.Windows.Forms.TextBox textBox4;

		// Token: 0x0400042A RID: 1066
		private global::System.Windows.Forms.Label label5;

		// Token: 0x0400042B RID: 1067
		private global::System.Windows.Forms.TextBox textBox5;
	}
}
