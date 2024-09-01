using System;
using System.ComponentModel;
using System.Windows.Forms;
using InputState;
using MapEditor.Forms.Base;

namespace MapEditor.Forms.Game
{
	// Token: 0x02000115 RID: 277
	public partial class GameForm : BaseForm
	{
		// Token: 0x06000DB0 RID: 3504 RVA: 0x00073134 File Offset: 0x00072134
		public GameForm(MainForm.Context context) : base(EditorEnvironment.EditorFormsFolder + "GameForm.xml", context)
		{
			this.InitializeComponent();
			this.sceneDrawTimer.Interval = 10;
			this.sceneDrawTimer.Enabled = true;
			this.gameScene = new GameScene();
		}

		// Token: 0x06000DB1 RID: 3505 RVA: 0x00073181 File Offset: 0x00072181
		private void sceneDrawTimer_Tick(object sender, EventArgs e)
		{
			if (base.Visible && this.gameScene != null)
			{
				this.sceneDrawTimer.Stop();
				if (!this.gameScene.Step())
				{
					base.Visible = false;
				}
				this.sceneDrawTimer.Start();
			}
		}

		// Token: 0x06000DB2 RID: 3506 RVA: 0x000731C0 File Offset: 0x000721C0
		private void CreateGameScene()
		{
			base.Context.StateContainer.Invoke("_save_map_before_start_game", default(MethodArgs));
			this.gameScene.Create(this, true);
		}

		// Token: 0x06000DB3 RID: 3507 RVA: 0x000731F9 File Offset: 0x000721F9
		private void DestroyGameScene()
		{
			this.gameScene.Destroy();
		}

		// Token: 0x06000DB4 RID: 3508 RVA: 0x00073206 File Offset: 0x00072206
		private void OnVisibleChanged(object sender, EventArgs e)
		{
			if (base.Visible)
			{
				this.CreateGameScene();
				return;
			}
			this.DestroyGameScene();
		}

		// Token: 0x04000AFC RID: 2812
		private readonly GameScene gameScene;
	}
}
