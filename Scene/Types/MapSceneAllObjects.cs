using System;

namespace MapEditor.Scene.Types
{
	// Token: 0x020001BD RID: 445
	internal class MapSceneAllObjects
	{
		// Token: 0x0600157B RID: 5499 RVA: 0x0009C524 File Offset: 0x0009B524
		private void OnHiddenObjectsChanged(int type, bool value)
		{
			this.mapEditorScene.EditorScene.SetObjectsTransparency(type, (double)(value ? 0 : 1));
		}

		// Token: 0x0600157C RID: 5500 RVA: 0x0009C53F File Offset: 0x0009B53F
		public void Bind(MapEditorScene _mapEditorScene)
		{
			this.mapEditorScene = _mapEditorScene;
			if (this.mapEditorScene != null)
			{
				this.mapEditorScene.MapSceneParams.HiddenObjectsChanged += this.OnHiddenObjectsChanged;
			}
		}

		// Token: 0x0600157D RID: 5501 RVA: 0x0009C56C File Offset: 0x0009B56C
		public void Unbind()
		{
			if (this.mapEditorScene != null)
			{
				this.mapEditorScene.MapSceneParams.HiddenObjectsChanged -= this.OnHiddenObjectsChanged;
			}
		}

		// Token: 0x04000F24 RID: 3876
		private MapEditorScene mapEditorScene;
	}
}
