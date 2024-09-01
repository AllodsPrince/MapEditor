using System;
using System.Collections.Generic;
using MapEditor.Map.MapObjects;
using Tools.MapObjects;

namespace MapEditor.Map.Containers
{
	// Token: 0x02000182 RID: 386
	public class RouteObjectContainer : TypedMapObjectContainer
	{
		// Token: 0x0600126A RID: 4714 RVA: 0x00086290 File Offset: 0x00085290
		private void OnEditorSceneBeforeStep(EditorScene _editorScene)
		{
			foreach (KeyValuePair<int, IMapObject> keyValuePair in base.MapObjects)
			{
				RouteObject routeObject = keyValuePair.Value as RouteObject;
				if (routeObject != null)
				{
					routeObject.Step(this.mapEditorMapObjectContainer);
				}
			}
		}

		// Token: 0x0600126B RID: 4715 RVA: 0x000862F8 File Offset: 0x000852F8
		private void OnRouteObjectCreated(RouteObject routeObject)
		{
			if (this.mapEditorMapObjectContainer != null && base.Contains(routeObject) && this.RouteObjectCreated != null)
			{
				this.RouteObjectCreated(this.mapEditorMapObjectContainer, routeObject);
			}
		}

		// Token: 0x0600126C RID: 4716 RVA: 0x00086325 File Offset: 0x00085325
		private void OnRouteObjectStatsChanged(RouteObject routeObject)
		{
			if (this.mapEditorMapObjectContainer != null && base.Contains(routeObject) && this.RouteObjectStatsChanged != null)
			{
				this.RouteObjectStatsChanged(this.mapEditorMapObjectContainer, routeObject);
			}
		}

		// Token: 0x0600126D RID: 4717 RVA: 0x00086352 File Offset: 0x00085352
		private void OnRouteObjectDestroyed(RouteObject routeObject)
		{
			if (this.mapEditorMapObjectContainer != null && base.Contains(routeObject) && this.RouteObjectDestroyed != null)
			{
				this.RouteObjectDestroyed(this.mapEditorMapObjectContainer, routeObject);
			}
		}

		// Token: 0x1400006D RID: 109
		// (add) Token: 0x0600126E RID: 4718 RVA: 0x0008637F File Offset: 0x0008537F
		// (remove) Token: 0x0600126F RID: 4719 RVA: 0x00086398 File Offset: 0x00085398
		public event RouteObjectContainer.RouteObjectChangedEvent RouteObjectCreated;

		// Token: 0x1400006E RID: 110
		// (add) Token: 0x06001270 RID: 4720 RVA: 0x000863B1 File Offset: 0x000853B1
		// (remove) Token: 0x06001271 RID: 4721 RVA: 0x000863CA File Offset: 0x000853CA
		public event RouteObjectContainer.RouteObjectChangedEvent RouteObjectDestroyed;

		// Token: 0x1400006F RID: 111
		// (add) Token: 0x06001272 RID: 4722 RVA: 0x000863E3 File Offset: 0x000853E3
		// (remove) Token: 0x06001273 RID: 4723 RVA: 0x000863FC File Offset: 0x000853FC
		public event RouteObjectContainer.RouteObjectChangedEvent RouteObjectStatsChanged;

		// Token: 0x06001274 RID: 4724 RVA: 0x00086418 File Offset: 0x00085418
		public RouteObjectContainer(EditorScene _editorScene, MapEditorMapObjectContainer _mapEditorMapObjectContainer) : base(MapObjectFactory.Type.RouteObject, true)
		{
			this.editorScene = _editorScene;
			this.mapEditorMapObjectContainer = _mapEditorMapObjectContainer;
			if (this.editorScene != null)
			{
				this.editorScene.BeforeStep += this.OnEditorSceneBeforeStep;
			}
			if (this.mapEditorMapObjectContainer != null)
			{
				RouteObject.Created += this.OnRouteObjectCreated;
				RouteObject.Destroyed += this.OnRouteObjectDestroyed;
				RouteObject.StatsChanged += this.OnRouteObjectStatsChanged;
				base.Destroyed += this.OnDestroyed;
			}
		}

		// Token: 0x06001275 RID: 4725 RVA: 0x000864AC File Offset: 0x000854AC
		public void OnDestroyed(TypedMapObjectContainer _typedMapObjectContainer)
		{
			if (this.editorScene != null)
			{
				this.editorScene.BeforeStep -= this.OnEditorSceneBeforeStep;
				this.editorScene = null;
			}
			if (this.mapEditorMapObjectContainer != null)
			{
				RouteObject.Created -= this.OnRouteObjectCreated;
				RouteObject.Destroyed -= this.OnRouteObjectDestroyed;
				RouteObject.StatsChanged -= this.OnRouteObjectStatsChanged;
				base.Destroyed -= this.OnDestroyed;
				this.mapEditorMapObjectContainer = null;
			}
		}

		// Token: 0x04000D2C RID: 3372
		private EditorScene editorScene;

		// Token: 0x04000D2D RID: 3373
		private MapEditorMapObjectContainer mapEditorMapObjectContainer;

		// Token: 0x02000183 RID: 387
		// (Invoke) Token: 0x06001277 RID: 4727
		public delegate void RouteObjectChangedEvent(MapEditorMapObjectContainer _mapEditorMapObjectContainer, RouteObject routeObject);
	}
}
