using System;
using System.Collections.Generic;
using System.Drawing;
using MapEditor.Map.Containers;
using MapEditor.Map.MapObjects;
using Tools.MapObjects;

namespace MapEditor.Scene.Types
{
	// Token: 0x02000080 RID: 128
	internal class MapEditorSceneAstralBorders
	{
		// Token: 0x0600061A RID: 1562 RVA: 0x00034170 File Offset: 0x00033170
		private int CreateUserGeometry(IMapObject mapObject, int userGeometryID)
		{
			if (this.mapEditorScene.MapSceneParams.ShowAstralBorderUserGeometry || mapObject.Select)
			{
				AstralBorder astralBorder = mapObject as AstralBorder;
				if (astralBorder != null)
				{
					Position position = mapObject.Position;
					return this.mapEditorScene.EditorScene.CreateUserGeometry_Circle(userGeometryID, ref position, 0f, astralBorder.StabilityRadius * 2.0 * this.mapEditorScene.ScaleRatio, astralBorder.StabilityRadius * 2.0 * this.mapEditorScene.ScaleRatio, MapEditorSceneAstralBorders.userGeometryColor, mapObject.Select, false);
				}
			}
			return -1;
		}

		// Token: 0x0600061B RID: 1563 RVA: 0x00034208 File Offset: 0x00033208
		private void DestroyUserGeometry(IMapObject mapObject, int userGeometryID)
		{
			AstralBorder astralBorder = mapObject as AstralBorder;
			if (astralBorder != null && userGeometryID != -1)
			{
				this.mapEditorScene.EditorScene.DeleteUserGeometry(userGeometryID);
			}
		}

		// Token: 0x0600061C RID: 1564 RVA: 0x00034234 File Offset: 0x00033234
		private void CreateUserGeometries()
		{
			foreach (KeyValuePair<int, IMapObject> keyValuePair in this.mapEditorScene.MapEditorMapObjectContainer.MapObjects)
			{
				IMapObject mapObject = keyValuePair.Value;
				int userGeometryID;
				if (mapObject != null && mapObject.Type.Type == MapObjectFactory.Type.AstralBorder && !this.userGeometryIDMap.TryGetValue(mapObject.ID, out userGeometryID))
				{
					userGeometryID = this.CreateUserGeometry(mapObject, -1);
					if (userGeometryID != -1)
					{
						this.userGeometryIDMap[mapObject.ID] = userGeometryID;
					}
				}
			}
		}

		// Token: 0x0600061D RID: 1565 RVA: 0x000342E0 File Offset: 0x000332E0
		private void DestroyUserGeometries()
		{
			foreach (KeyValuePair<int, IMapObject> keyValuePair in this.mapEditorScene.MapEditorMapObjectContainer.MapObjects)
			{
				IMapObject mapObject = keyValuePair.Value;
				int userGeometryID;
				if (mapObject != null && mapObject.Type.Type == MapObjectFactory.Type.AstralBorder && !mapObject.Select && this.userGeometryIDMap.TryGetValue(mapObject.ID, out userGeometryID))
				{
					this.DestroyUserGeometry(mapObject, userGeometryID);
					this.userGeometryIDMap.Remove(mapObject.ID);
				}
			}
		}

		// Token: 0x0600061E RID: 1566 RVA: 0x00034390 File Offset: 0x00033390
		private void OnShowUserGeometryChanged(MapSceneParams mapSceneParams, ref bool oldValue, ref bool newValue)
		{
			if (this.mapEditorScene.MapSceneParams != null)
			{
				if (this.mapEditorScene.MapSceneParams.ShowAstralBorderUserGeometry)
				{
					this.CreateUserGeometries();
					return;
				}
				this.DestroyUserGeometries();
			}
		}

		// Token: 0x0600061F RID: 1567 RVA: 0x000343C0 File Offset: 0x000333C0
		public void OnMapObjectAdded(MapObjectContainer mapObjectContainer, IMapObject mapObject)
		{
			int userGeometryID = this.CreateUserGeometry(mapObject, -1);
			if (userGeometryID != -1)
			{
				this.userGeometryIDMap[mapObject.ID] = userGeometryID;
			}
		}

		// Token: 0x06000620 RID: 1568 RVA: 0x000343EC File Offset: 0x000333EC
		public void OnMapObjectRemoved(MapObjectContainer mapObjectContainer, IMapObject mapObject)
		{
			int userGeometryID;
			if (this.userGeometryIDMap.TryGetValue(mapObject.ID, out userGeometryID) && userGeometryID != -1)
			{
				this.DestroyUserGeometry(mapObject, userGeometryID);
				this.userGeometryIDMap.Remove(mapObject.ID);
			}
		}

		// Token: 0x06000621 RID: 1569 RVA: 0x0003442C File Offset: 0x0003342C
		public void OnPositionChanged(MapObjectContainer mapObjectContainer, IMapObject mapObject, ref Position oldValue, ref Position newValue)
		{
			int userGeometryID;
			if (this.userGeometryIDMap.TryGetValue(mapObject.ID, out userGeometryID) && userGeometryID != -1)
			{
				this.CreateUserGeometry(mapObject, userGeometryID);
			}
		}

		// Token: 0x06000622 RID: 1570 RVA: 0x0003445C File Offset: 0x0003345C
		public void OnRotationChanged(MapObjectContainer mapObjectContainer, IMapObject mapObject, ref Rotation oldValue, ref Rotation newValue)
		{
			AstralBorder astralBorder = mapObject as AstralBorder;
		}

		// Token: 0x06000623 RID: 1571 RVA: 0x00034474 File Offset: 0x00033474
		public void OnSelectChanged(MapObjectContainer mapObjectContainer, IMapObject mapObject, ref bool oldValue, ref bool newValue)
		{
			int userGeometryID2;
			if (newValue || this.mapEditorScene.MapSceneParams.ShowAstralBorderUserGeometry)
			{
				int userGeometryID;
				if (this.userGeometryIDMap.TryGetValue(mapObject.ID, out userGeometryID))
				{
					this.CreateUserGeometry(mapObject, userGeometryID);
					return;
				}
				if (newValue)
				{
					userGeometryID = this.CreateUserGeometry(mapObject, -1);
					if (userGeometryID != -1)
					{
						this.userGeometryIDMap[mapObject.ID] = userGeometryID;
						return;
					}
				}
			}
			else if (this.userGeometryIDMap.TryGetValue(mapObject.ID, out userGeometryID2) && userGeometryID2 != -1)
			{
				this.DestroyUserGeometry(mapObject, userGeometryID2);
				this.userGeometryIDMap.Remove(mapObject.ID);
			}
		}

		// Token: 0x06000624 RID: 1572 RVA: 0x00034510 File Offset: 0x00033510
		private void OnAstralBorderStabilityRadiusChanged(MapEditorMapObjectContainer mapEditorMapObjectContainer, AstralBorder astralBorder, ref double oldValue, ref double newValue)
		{
			int userGeometryID;
			if (astralBorder != null && this.userGeometryIDMap.TryGetValue(astralBorder.ID, out userGeometryID) && userGeometryID != -1)
			{
				this.CreateUserGeometry(astralBorder, userGeometryID);
			}
		}

		// Token: 0x06000625 RID: 1573 RVA: 0x00034544 File Offset: 0x00033544
		public void Bind(MapEditorScene _mapEditorScene)
		{
			this.mapEditorScene = _mapEditorScene;
			if (this.mapEditorScene != null)
			{
				this.mapEditorScene.MapSceneParams.ShowAstralBorderUserGeometryChanged += this.OnShowUserGeometryChanged;
				AstralBorderContainer astralBorderContainer = this.mapEditorScene.MapEditorMapObjectContainer.AstralBorderContainer;
				if (astralBorderContainer != null)
				{
					astralBorderContainer.AstralBorderStabilityRadiusChanged += this.OnAstralBorderStabilityRadiusChanged;
				}
				this.CreateUserGeometries();
			}
		}

		// Token: 0x06000626 RID: 1574 RVA: 0x000345A8 File Offset: 0x000335A8
		public void Unbind()
		{
			if (this.mapEditorScene != null)
			{
				this.DestroyUserGeometries();
				this.mapEditorScene.MapSceneParams.ShowAstralBorderUserGeometryChanged -= this.OnShowUserGeometryChanged;
				AstralBorderContainer astralBorderContainer = this.mapEditorScene.MapEditorMapObjectContainer.AstralBorderContainer;
				if (astralBorderContainer != null)
				{
					astralBorderContainer.AstralBorderStabilityRadiusChanged -= this.OnAstralBorderStabilityRadiusChanged;
				}
				this.mapEditorScene = null;
			}
		}

		// Token: 0x040004A9 RID: 1193
		private static readonly Color userGeometryColor = Color.FromArgb(MapEditorScene.DefaultTransparentColorAlpha, AstralBorder.InterfaceColor);

		// Token: 0x040004AA RID: 1194
		private readonly Dictionary<int, int> userGeometryIDMap = new Dictionary<int, int>();

		// Token: 0x040004AB RID: 1195
		private MapEditorScene mapEditorScene;
	}
}
