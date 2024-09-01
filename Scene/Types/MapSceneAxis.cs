using System;
using Tools.Geometry;
using Tools.MapObjects;

namespace MapEditor.Scene.Types
{
	// Token: 0x0200007F RID: 127
	internal class MapSceneAxis
	{
		// Token: 0x06000613 RID: 1555 RVA: 0x00033F30 File Offset: 0x00032F30
		private void CreateUserGeometry()
		{
			if (this.mapEditorScene != null && this.mapEditorScene.MapSceneParams.ShowAxisUserGeometry)
			{
				Rect rect;
				this.mapEditorScene.EditorScene.GetViewDimensions(this.mapEditorScene.EditorSceneViewID, out rect);
				Line line;
				this.mapEditorScene.EditorScene.GetProjectiveRay(this.mapEditorScene.EditorSceneViewID, 50, rect.Height - 75, out line);
				Position position = new Position(line.Origin + line.Direction * 16.0);
				if (this.axisUserGeometryID == -1)
				{
					this.axisUserGeometryID = this.mapEditorScene.EditorScene.CreateUserGeometry_MoveWidget(this.axisUserGeometryID, ref position, ref this.axisRotation, ref this.axisScale, false, false, true, false, false, true, true, true, true);
				}
				if (this.axisUserGeometryID != -1)
				{
					this.mapEditorScene.EditorScene.MoveUserGeometry(this.axisUserGeometryID, ref position);
				}
			}
		}

		// Token: 0x06000614 RID: 1556 RVA: 0x0003402F File Offset: 0x0003302F
		private void DetroyUserGeometry()
		{
			if (this.mapEditorScene != null && this.axisUserGeometryID != -1)
			{
				this.mapEditorScene.EditorScene.DeleteUserGeometry(this.axisUserGeometryID);
				this.axisUserGeometryID = -1;
			}
		}

		// Token: 0x06000615 RID: 1557 RVA: 0x0003405F File Offset: 0x0003305F
		private void OnShowAxisUserGeometryChanged(MapSceneParams mapSceneParams, ref bool oldValue, ref bool newValue)
		{
			if (this.mapEditorScene.MapSceneParams.ShowAxisUserGeometry)
			{
				this.CreateUserGeometry();
				return;
			}
			this.DetroyUserGeometry();
		}

		// Token: 0x06000616 RID: 1558 RVA: 0x00034080 File Offset: 0x00033080
		private void OnMiddleEditorSceneStep(EditorScene editroScene)
		{
			this.CreateUserGeometry();
		}

		// Token: 0x06000617 RID: 1559 RVA: 0x00034088 File Offset: 0x00033088
		public void Bind(MapEditorScene _mapEditorScene)
		{
			this.mapEditorScene = _mapEditorScene;
			if (this.mapEditorScene != null)
			{
				this.mapEditorScene.MapSceneParams.ShowAxisUserGeometryChanged += this.OnShowAxisUserGeometryChanged;
				this.mapEditorScene.EditorScene.MiddleStep += this.OnMiddleEditorSceneStep;
				this.CreateUserGeometry();
			}
		}

		// Token: 0x06000618 RID: 1560 RVA: 0x000340E4 File Offset: 0x000330E4
		public void Unbind()
		{
			if (this.mapEditorScene != null)
			{
				this.DetroyUserGeometry();
				this.mapEditorScene.MapSceneParams.ShowAxisUserGeometryChanged -= this.OnShowAxisUserGeometryChanged;
				this.mapEditorScene.EditorScene.MiddleStep -= this.OnMiddleEditorSceneStep;
				this.mapEditorScene = null;
			}
		}

		// Token: 0x040004A5 RID: 1189
		private int axisUserGeometryID = -1;

		// Token: 0x040004A6 RID: 1190
		private Rotation axisRotation = Rotation.Empty;

		// Token: 0x040004A7 RID: 1191
		private Scale axisScale = Scale.Normal * 0.5f;

		// Token: 0x040004A8 RID: 1192
		private MapEditorScene mapEditorScene;
	}
}
