using System;

namespace MapEditor.Scene.Types
{
	// Token: 0x02000002 RID: 2
	internal class MapSceneDynamicStatistic
	{
		// Token: 0x06000001 RID: 1 RVA: 0x00002050 File Offset: 0x00001050
		private void UpdateScene()
		{
			if (this.mapEditorScene != null)
			{
				this.mapEditorScene.EditorScene.EnableInteractiveStatistic(this.mapEditorScene.MapSceneParams.ShowDynamicStatistic);
			}
		}

		// Token: 0x06000002 RID: 2 RVA: 0x0000207A File Offset: 0x0000107A
		private void OnShowDynamicStatisticChanged(MapSceneParams mapSceneParams, ref bool oldValue, ref bool newValue)
		{
			this.UpdateScene();
		}

		// Token: 0x06000003 RID: 3 RVA: 0x00002082 File Offset: 0x00001082
		public void Bind(MapEditorScene _mapEditorScene)
		{
			this.mapEditorScene = _mapEditorScene;
			if (this.mapEditorScene != null)
			{
				this.mapEditorScene.MapSceneParams.ShowDynamicStatisticChanged += this.OnShowDynamicStatisticChanged;
				this.UpdateScene();
			}
		}

		// Token: 0x06000004 RID: 4 RVA: 0x000020B5 File Offset: 0x000010B5
		public void Unbind()
		{
			if (this.mapEditorScene != null)
			{
				this.mapEditorScene.MapSceneParams.ShowDynamicStatisticChanged -= this.OnShowDynamicStatisticChanged;
			}
		}

		// Token: 0x04000001 RID: 1
		private MapEditorScene mapEditorScene;
	}
}
