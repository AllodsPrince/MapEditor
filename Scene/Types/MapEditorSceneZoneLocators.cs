using System;
using System.Collections.Generic;
using System.Drawing;
using MapEditor.Map.Containers;
using MapEditor.Map.MapObjects;
using Tools.MapObjects;

namespace MapEditor.Scene.Types
{
	// Token: 0x02000057 RID: 87
	internal class MapEditorSceneZoneLocators
	{
		// Token: 0x0600046A RID: 1130 RVA: 0x00024199 File Offset: 0x00023199
		private static Color GetMapZoneColor(ZoneLocator zoneLocator)
		{
			if (zoneLocator.Select)
			{
				return Color.FromArgb(MapObjectCreationInfo.DefaultTransparentColorAlpha, zoneLocator.MapZoneColor);
			}
			return Color.FromArgb(MapObjectCreationInfo.DefaultTransparentColorAlpha / 2, zoneLocator.MapZoneColor);
		}

		// Token: 0x0600046B RID: 1131 RVA: 0x000241C8 File Offset: 0x000231C8
		private void CreateMapZoneHighlights()
		{
			ZoneLocatorContainer zoneLocatorContainer = this.mapEditorScene.MapEditorMapObjectContainer.ZoneLocatorContainer;
			if (zoneLocatorContainer != null)
			{
				foreach (KeyValuePair<int, IMapObject> keyValuePair in zoneLocatorContainer.MapObjects)
				{
					ZoneLocator zoneLocator = keyValuePair.Value as ZoneLocator;
					if (zoneLocator != null && !zoneLocator.Select)
					{
						Position position = zoneLocator.Position;
						this.mapEditorScene.EditorScene.HighlightAreas(ref position, true, MapEditorSceneZoneLocators.GetMapZoneColor(zoneLocator));
					}
				}
			}
		}

		// Token: 0x0600046C RID: 1132 RVA: 0x00024264 File Offset: 0x00023264
		private void DestroyMapZoneHighlights()
		{
			ZoneLocatorContainer zoneLocatorContainer = this.mapEditorScene.MapEditorMapObjectContainer.ZoneLocatorContainer;
			if (zoneLocatorContainer != null)
			{
				foreach (KeyValuePair<int, IMapObject> keyValuePair in zoneLocatorContainer.MapObjects)
				{
					ZoneLocator zoneLocator = keyValuePair.Value as ZoneLocator;
					if (zoneLocator != null && !zoneLocator.Select)
					{
						Position position = zoneLocator.Position;
						this.mapEditorScene.EditorScene.HighlightAreas(ref position, false, MapObjectCreationInfo.DefaultHighlightColor);
					}
				}
			}
		}

		// Token: 0x0600046D RID: 1133 RVA: 0x000242FC File Offset: 0x000232FC
		private void OnShowUserGeometryChanged(MapSceneParams mapSceneParams, ref bool oldValue, ref bool newValue)
		{
			if (this.mapEditorScene.MapSceneParams != null)
			{
				if (this.mapEditorScene.MapSceneParams.ShowZoneLocatorUserGeometry)
				{
					this.CreateMapZoneHighlights();
					return;
				}
				this.DestroyMapZoneHighlights();
			}
		}

		// Token: 0x0600046E RID: 1134 RVA: 0x0002432C File Offset: 0x0002332C
		public void OnMapObjectAdded(MapObjectContainer mapObjectContainer, IMapObject mapObject)
		{
			if (this.mapEditorScene.MapSceneParams.ShowZoneLocatorUserGeometry || mapObject.Select)
			{
				ZoneLocator zoneLocator = mapObject as ZoneLocator;
				if (zoneLocator != null)
				{
					Position position = mapObject.Position;
					this.mapEditorScene.EditorScene.HighlightAreas(ref position, true, MapEditorSceneZoneLocators.GetMapZoneColor(zoneLocator));
				}
			}
		}

		// Token: 0x0600046F RID: 1135 RVA: 0x00024380 File Offset: 0x00023380
		public void OnMapObjectRemoved(MapObjectContainer mapObjectContainer, IMapObject mapObject)
		{
			if (this.mapEditorScene.MapSceneParams.ShowZoneLocatorUserGeometry || mapObject.Select)
			{
				ZoneLocator zoneLocator = mapObject as ZoneLocator;
				if (zoneLocator != null)
				{
					Position position = mapObject.Position;
					this.mapEditorScene.EditorScene.HighlightAreas(ref position, false, MapObjectCreationInfo.DefaultHighlightColor);
				}
			}
		}

		// Token: 0x06000470 RID: 1136 RVA: 0x000243D0 File Offset: 0x000233D0
		public void OnPositionChanged(MapObjectContainer mapObjectContainer, IMapObject mapObject, ref Position oldValue, ref Position newValue)
		{
			if (this.mapEditorScene.MapSceneParams.ShowZoneLocatorUserGeometry || mapObject.Select)
			{
				ZoneLocator zoneLocator = mapObject as ZoneLocator;
				if (zoneLocator != null)
				{
					this.mapEditorScene.EditorScene.HighlightAreas(ref oldValue, false, MapObjectCreationInfo.DefaultHighlightColor);
					this.mapEditorScene.EditorScene.HighlightAreas(ref newValue, true, MapEditorSceneZoneLocators.GetMapZoneColor(zoneLocator));
				}
			}
		}

		// Token: 0x06000471 RID: 1137 RVA: 0x00024431 File Offset: 0x00023431
		public void OnRotationChanged(MapObjectContainer mapObjectContainer, IMapObject mapObject, ref Rotation oldValue, ref Rotation newValue)
		{
		}

		// Token: 0x06000472 RID: 1138 RVA: 0x00024434 File Offset: 0x00023434
		public void OnSelectChanged(MapObjectContainer mapObjectContainer, IMapObject mapObject, ref bool oldValue, ref bool newValue)
		{
			if (oldValue != newValue && (this.mapEditorScene.MapSceneParams.ShowZoneLocatorUserGeometry || oldValue || newValue))
			{
				ZoneLocator zoneLocator = mapObject as ZoneLocator;
				if (zoneLocator != null)
				{
					Position position = mapObject.Position;
					if (this.mapEditorScene.MapSceneParams.ShowZoneLocatorUserGeometry || oldValue)
					{
						this.mapEditorScene.EditorScene.HighlightAreas(ref position, false, MapObjectCreationInfo.DefaultHighlightColor);
					}
					if (this.mapEditorScene.MapSceneParams.ShowZoneLocatorUserGeometry || newValue)
					{
						this.mapEditorScene.EditorScene.HighlightAreas(ref position, true, MapEditorSceneZoneLocators.GetMapZoneColor(zoneLocator));
					}
				}
			}
		}

		// Token: 0x06000473 RID: 1139 RVA: 0x000244D8 File Offset: 0x000234D8
		private void OnZoneLocatorMapZoneColorChanged(MapEditorMapObjectContainer mapEditorMapObjectContainer, ZoneLocator zoneLocator, ref Color oldValue, ref Color newValue)
		{
			if ((this.mapEditorScene.MapSceneParams.ShowZoneLocatorUserGeometry || zoneLocator.Select) && zoneLocator != null)
			{
				Position position = zoneLocator.Position;
				this.mapEditorScene.EditorScene.HighlightAreas(ref position, false, MapObjectCreationInfo.DefaultHighlightColor);
				this.mapEditorScene.EditorScene.HighlightAreas(ref position, true, MapEditorSceneZoneLocators.GetMapZoneColor(zoneLocator));
			}
		}

		// Token: 0x06000474 RID: 1140 RVA: 0x0002453C File Offset: 0x0002353C
		public void Bind(MapEditorScene _mapEditorScene)
		{
			this.mapEditorScene = _mapEditorScene;
			if (this.mapEditorScene != null)
			{
				this.mapEditorScene.MapSceneParams.ShowZoneLocatorUserGeometryChanged += this.OnShowUserGeometryChanged;
				ZoneLocatorContainer zoneLocatorContainer = this.mapEditorScene.MapEditorMapObjectContainer.ZoneLocatorContainer;
				if (zoneLocatorContainer != null)
				{
					zoneLocatorContainer.ZoneLocatorMapZoneColorChanged += this.OnZoneLocatorMapZoneColorChanged;
				}
				this.CreateMapZoneHighlights();
			}
		}

		// Token: 0x06000475 RID: 1141 RVA: 0x000245A0 File Offset: 0x000235A0
		public void Unbind()
		{
			if (this.mapEditorScene != null)
			{
				this.DestroyMapZoneHighlights();
				this.mapEditorScene.MapSceneParams.ShowZoneLocatorUserGeometryChanged -= this.OnShowUserGeometryChanged;
				ZoneLocatorContainer zoneLocatorContainer = this.mapEditorScene.MapEditorMapObjectContainer.ZoneLocatorContainer;
				if (zoneLocatorContainer != null)
				{
					zoneLocatorContainer.ZoneLocatorMapZoneColorChanged -= this.OnZoneLocatorMapZoneColorChanged;
				}
				this.mapEditorScene = null;
			}
		}

		// Token: 0x04000310 RID: 784
		private MapEditorScene mapEditorScene;
	}
}
