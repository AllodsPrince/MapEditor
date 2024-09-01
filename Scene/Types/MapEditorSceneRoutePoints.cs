using System;
using MapEditor.Map.Containers;
using MapEditor.Map.MapObjects;

namespace MapEditor.Scene.Types
{
	// Token: 0x0200021B RID: 539
	internal class MapEditorSceneRoutePoints
	{
		// Token: 0x06001A17 RID: 6679 RVA: 0x000ABF23 File Offset: 0x000AAF23
		public void OnRoutePointTypeChanged(MapEditorMapObjectContainer mapEditorMapObjectContainer, RoutePoint routePoint, ref RoutePointType oldValue, ref RoutePointType newValue)
		{
			this.mapSceneObjects.RecreateMapObject(routePoint, false);
		}

		// Token: 0x06001A18 RID: 6680 RVA: 0x000ABF34 File Offset: 0x000AAF34
		public void Bind(MapEditorScene _mapEditorScene, MapSceneObjects _mapSceneObjects)
		{
			this.mapEditorScene = _mapEditorScene;
			this.mapSceneObjects = _mapSceneObjects;
			if (this.mapEditorScene != null && this.mapSceneObjects != null)
			{
				RoutePointContainer routePointContainer = this.mapEditorScene.MapEditorMapObjectContainer.RoutePointContainer;
				if (routePointContainer != null)
				{
					routePointContainer.RoutePointTypeChanged += this.OnRoutePointTypeChanged;
				}
			}
		}

		// Token: 0x06001A19 RID: 6681 RVA: 0x000ABF88 File Offset: 0x000AAF88
		public void Unbind()
		{
			if (this.mapEditorScene != null)
			{
				RoutePointContainer routePointContainer = this.mapEditorScene.MapEditorMapObjectContainer.RoutePointContainer;
				if (routePointContainer != null)
				{
					routePointContainer.RoutePointTypeChanged -= this.OnRoutePointTypeChanged;
				}
				this.mapEditorScene = null;
			}
			this.mapSceneObjects = null;
		}

		// Token: 0x040010FD RID: 4349
		private MapEditorScene mapEditorScene;

		// Token: 0x040010FE RID: 4350
		private MapSceneObjects mapSceneObjects;
	}
}
