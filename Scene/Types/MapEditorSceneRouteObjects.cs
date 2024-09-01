using System;
using System.Collections.Generic;
using MapEditor.Map.Containers;
using MapEditor.Map.MapObjects;
using Tools.MapObjects;

namespace MapEditor.Scene.Types
{
	// Token: 0x020001EC RID: 492
	internal class MapEditorSceneRouteObjects
	{
		// Token: 0x060018C2 RID: 6338 RVA: 0x000A4C70 File Offset: 0x000A3C70
		private void UpdateScene()
		{
			if (this.mapEditorScene != null)
			{
				RouteObjectContainer routeObjectContainer = this.mapEditorScene.MapEditorMapObjectContainer.RouteObjectContainer;
				if (routeObjectContainer != null)
				{
					foreach (KeyValuePair<int, IMapObject> keyValuePair in routeObjectContainer.MapObjects)
					{
						int editorSceneObjectID = this.mapSceneObjects.MapObjectIDToEditorSceneObjectID(keyValuePair.Value.ID);
						if (editorSceneObjectID != -1)
						{
							this.mapEditorScene.EditorScene.SetObjectTransparency(editorSceneObjectID, this.mapEditorScene.MapSceneParams.ShowRouteObjects ? 1.0 : 0.0);
						}
					}
				}
			}
		}

		// Token: 0x060018C3 RID: 6339 RVA: 0x000A4D34 File Offset: 0x000A3D34
		private void OnShowRouteObjectsChanged(MapSceneParams mapSceneParams, ref bool oldValue, ref bool newValue)
		{
			this.UpdateScene();
		}

		// Token: 0x060018C4 RID: 6340 RVA: 0x000A4D3C File Offset: 0x000A3D3C
		public void OnRouteObjectStatsChanged(MapObjectContainer mapObjectContainer, RouteObject routeObject)
		{
			this.mapSceneObjects.RecreateMapObject(routeObject, true);
			int editorSceneObjectID = this.mapSceneObjects.MapObjectIDToEditorSceneObjectID(routeObject.ID);
			if (editorSceneObjectID != -1)
			{
				this.mapEditorScene.EditorScene.SetObjectTransparency(editorSceneObjectID, this.mapEditorScene.MapSceneParams.ShowRouteObjects ? 1.0 : 0.0);
			}
		}

		// Token: 0x060018C5 RID: 6341 RVA: 0x000A4DA4 File Offset: 0x000A3DA4
		public void Bind(MapEditorScene _mapEditorScene, MapSceneObjects _mapSceneObjects)
		{
			this.mapEditorScene = _mapEditorScene;
			this.mapSceneObjects = _mapSceneObjects;
			if (this.mapEditorScene != null)
			{
				this.mapEditorScene.MapSceneParams.ShowRouteObjectsChanged += this.OnShowRouteObjectsChanged;
				this.UpdateScene();
				RouteObjectContainer routeObjectContainer = this.mapEditorScene.MapEditorMapObjectContainer.RouteObjectContainer;
				if (this.mapEditorScene != null && this.mapSceneObjects != null)
				{
					routeObjectContainer.RouteObjectStatsChanged += new RouteObjectContainer.RouteObjectChangedEvent(this.OnRouteObjectStatsChanged);
				}
			}
		}

		// Token: 0x060018C6 RID: 6342 RVA: 0x000A4E1C File Offset: 0x000A3E1C
		public void Unbind()
		{
			if (this.mapEditorScene != null)
			{
				this.mapEditorScene.MapSceneParams.ShowRouteObjectsChanged -= this.OnShowRouteObjectsChanged;
				RouteObjectContainer routeObjectContainer = this.mapEditorScene.MapEditorMapObjectContainer.RouteObjectContainer;
				if (routeObjectContainer != null)
				{
					routeObjectContainer.RouteObjectStatsChanged -= new RouteObjectContainer.RouteObjectChangedEvent(this.OnRouteObjectStatsChanged);
				}
				this.mapEditorScene = null;
			}
			this.mapSceneObjects = null;
		}

		// Token: 0x04001012 RID: 4114
		private MapEditorScene mapEditorScene;

		// Token: 0x04001013 RID: 4115
		private MapSceneObjects mapSceneObjects;
	}
}
