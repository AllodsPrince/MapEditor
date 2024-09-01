namespace MapEditor.Forms.Game
{
	// Token: 0x02000115 RID: 277
	public partial class GameForm : global::MapEditor.Forms.Base.BaseForm
	{
		// Token: 0x06000DAE RID: 3502 RVA: 0x00073083 File Offset: 0x00072083
		protected override void Dispose(bool disposing)
		{
			if (disposing && this.components != null)
			{
				this.components.Dispose();
			}
			base.Dispose(disposing);
		}

		// Token: 0x06000DAF RID: 3503 RVA: 0x000730A4 File Offset: 0x000720A4
		private void InitializeComponent()
		{
			this.components = new global::System.ComponentModel.Container();
			global::System.ComponentModel.ComponentResourceManager resources = new global::System.ComponentModel.ComponentResourceManager(typeof(global::MapEditor.Forms.Game.GameForm));
			this.sceneDrawTimer = new global::System.Windows.Forms.Timer(this.components);
			base.SuspendLayout();
			this.sceneDrawTimer.Tick += new global::System.EventHandler(this.sceneDrawTimer_Tick);
			resources.ApplyResources(this, "$this");
			base.AutoScaleMode = global::System.Windows.Forms.AutoScaleMode.Font;
			base.Name = "GameForm";
			base.VisibleChanged += new global::System.EventHandler(this.OnVisibleChanged);
			base.ResumeLayout(false);
		}

		// Token: 0x04000AFA RID: 2810
		private global::System.ComponentModel.IContainer components;

		// Token: 0x04000AFB RID: 2811
		private global::System.Windows.Forms.Timer sceneDrawTimer;
	}
}
