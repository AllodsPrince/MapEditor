using System;
using MapEditor.Map.Containers;
using Operations;

namespace MapEditor.Map.MapObjects
{
	// Token: 0x02000090 RID: 144
	public class RoutePointOperationContainer
	{
		// Token: 0x060006DE RID: 1758 RVA: 0x00035D68 File Offset: 0x00034D68
		private void OnRoutePointRouteChanged(MapEditorMapObjectContainer _mapEditorMapObjectContainer, RoutePoint routePoint, ref string oldValue, ref string newValue)
		{
			if (this.mapEditorMapObjectContainer != null && routePoint != null && this.operationContainer != null && !routePoint.Temporary)
			{
				ChangeRoutePointRouteOperation operation = new ChangeRoutePointRouteOperation(routePoint, ref oldValue, ref newValue);
				this.operationContainer.Add(operation);
			}
		}

		// Token: 0x060006DF RID: 1759 RVA: 0x00035DA8 File Offset: 0x00034DA8
		private void OnRoutePointTypeChanged(MapEditorMapObjectContainer _mapEditorMapObjectContainer, RoutePoint routePoint, ref RoutePointType oldValue, ref RoutePointType newValue)
		{
			if (this.mapEditorMapObjectContainer != null && routePoint != null && this.operationContainer != null && !routePoint.Temporary)
			{
				ChangeRoutePointTypeOperation operation = new ChangeRoutePointTypeOperation(routePoint, ref oldValue, ref newValue);
				this.operationContainer.Add(operation);
			}
		}

		// Token: 0x060006E0 RID: 1760 RVA: 0x00035DE8 File Offset: 0x00034DE8
		private void OnRoutePointSpeedChanged(MapEditorMapObjectContainer _mapEditorMapObjectContainer, RoutePoint routePoint, ref double oldValue, ref double newValue)
		{
			if (this.mapEditorMapObjectContainer != null && routePoint != null && this.operationContainer != null && !routePoint.Temporary)
			{
				ChangeRoutePointSpeedOperation operation = new ChangeRoutePointSpeedOperation(routePoint, ref oldValue, ref newValue);
				this.operationContainer.Add(operation);
			}
		}

		// Token: 0x060006E1 RID: 1761 RVA: 0x00035E28 File Offset: 0x00034E28
		private void OnRoutePointLatencyChanged(MapEditorMapObjectContainer _mapEditorMapObjectContainer, RoutePoint routePoint, ref double oldValue, ref double newValue)
		{
			if (this.mapEditorMapObjectContainer != null && routePoint != null && this.operationContainer != null && !routePoint.Temporary)
			{
				ChangeRoutePointLatencyOperation operation = new ChangeRoutePointLatencyOperation(routePoint, ref oldValue, ref newValue);
				this.operationContainer.Add(operation);
			}
		}

		// Token: 0x060006E2 RID: 1762 RVA: 0x00035E68 File Offset: 0x00034E68
		private void OnRoutePointLabelChanged(MapEditorMapObjectContainer _mapEditorMapObjectContainer, RoutePoint routePoint, ref string oldValue, ref string newValue)
		{
			if (this.mapEditorMapObjectContainer != null && routePoint != null && this.operationContainer != null && !routePoint.Temporary)
			{
				ChangeRoutePointLabelOperation operation = new ChangeRoutePointLabelOperation(routePoint, ref oldValue, ref newValue);
				this.operationContainer.Add(operation);
			}
		}

		// Token: 0x060006E3 RID: 1763 RVA: 0x00035EA8 File Offset: 0x00034EA8
		private void OnRoutePointScriptChanged(MapEditorMapObjectContainer _mapEditorMapObjectContainer, RoutePoint routePoint, ref string oldValue, ref string newValue)
		{
			if (this.mapEditorMapObjectContainer != null && routePoint != null && this.operationContainer != null && !routePoint.Temporary)
			{
				ChangeRoutePointScriptOperation operation = new ChangeRoutePointScriptOperation(routePoint, ref oldValue, ref newValue);
				this.operationContainer.Add(operation);
			}
		}

		// Token: 0x060006E4 RID: 1764 RVA: 0x00035EE6 File Offset: 0x00034EE6
		public RoutePointOperationContainer(OperationContainer _operationContainer)
		{
			this.operationContainer = _operationContainer;
		}

		// Token: 0x060006E5 RID: 1765 RVA: 0x00035EF5 File Offset: 0x00034EF5
		public void Destroy()
		{
			if (this.operationContainer != null)
			{
				this.operationContainer = null;
			}
		}

		// Token: 0x060006E6 RID: 1766 RVA: 0x00035F08 File Offset: 0x00034F08
		public void Bind(MapEditorMapObjectContainer _mapEditorMapObjectContainer)
		{
			this.Unbind();
			this.mapEditorMapObjectContainer = _mapEditorMapObjectContainer;
			if (this.mapEditorMapObjectContainer != null)
			{
				RoutePointContainer routePointContainer = this.mapEditorMapObjectContainer.RoutePointContainer;
				if (routePointContainer != null)
				{
					routePointContainer.RoutePointRouteChanged += this.OnRoutePointRouteChanged;
					routePointContainer.RoutePointTypeChanged += this.OnRoutePointTypeChanged;
					routePointContainer.RoutePointSpeedChanged += this.OnRoutePointSpeedChanged;
					routePointContainer.RoutePointLatencyChanged += this.OnRoutePointLatencyChanged;
					routePointContainer.RoutePointLabelChanged += this.OnRoutePointLabelChanged;
					routePointContainer.RoutePointScriptChanged += this.OnRoutePointScriptChanged;
				}
			}
		}

		// Token: 0x060006E7 RID: 1767 RVA: 0x00035FA8 File Offset: 0x00034FA8
		public void Unbind()
		{
			if (this.mapEditorMapObjectContainer != null)
			{
				RoutePointContainer routePointContainer = this.mapEditorMapObjectContainer.RoutePointContainer;
				if (routePointContainer != null)
				{
					routePointContainer.RoutePointRouteChanged -= this.OnRoutePointRouteChanged;
					routePointContainer.RoutePointTypeChanged -= this.OnRoutePointTypeChanged;
					routePointContainer.RoutePointSpeedChanged -= this.OnRoutePointSpeedChanged;
					routePointContainer.RoutePointLatencyChanged -= this.OnRoutePointLatencyChanged;
					routePointContainer.RoutePointLabelChanged -= this.OnRoutePointLabelChanged;
					routePointContainer.RoutePointScriptChanged -= this.OnRoutePointScriptChanged;
				}
				this.mapEditorMapObjectContainer = null;
			}
		}

		// Token: 0x040004F1 RID: 1265
		private MapEditorMapObjectContainer mapEditorMapObjectContainer;

		// Token: 0x040004F2 RID: 1266
		private OperationContainer operationContainer;
	}
}
