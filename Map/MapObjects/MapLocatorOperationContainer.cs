using System;
using MapEditor.Map.Containers;
using Operations;

namespace MapEditor.Map.MapObjects
{
	// Token: 0x02000062 RID: 98
	public class MapLocatorOperationContainer
	{
		// Token: 0x060004FF RID: 1279 RVA: 0x0002832C File Offset: 0x0002732C
		private void OnMapLocatorScriptIDChanged(MapEditorMapObjectContainer _mapEditorMapObjectContainer, MapLocator mapLocator, ref string oldValue, ref string newValue)
		{
			if (this.mapEditorMapObjectContainer != null && mapLocator != null && this.operationContainer != null && !mapLocator.Temporary)
			{
				ChangeMapLocatorScriptIDOperation operation = new ChangeMapLocatorScriptIDOperation(mapLocator, ref oldValue, ref newValue);
				this.operationContainer.Add(operation);
			}
		}

		// Token: 0x06000500 RID: 1280 RVA: 0x0002836C File Offset: 0x0002736C
		private void OnMapLocatorScanRadiusChanged(MapEditorMapObjectContainer _mapEditorMapObjectContainer, MapLocator mapLocator, ref double oldValue, ref double newValue)
		{
			if (this.mapEditorMapObjectContainer != null && mapLocator != null && this.operationContainer != null && !mapLocator.Temporary)
			{
				ChangeMapLocatorScanRadiusOperation operation = new ChangeMapLocatorScanRadiusOperation(mapLocator, ref oldValue, ref newValue);
				this.operationContainer.Add(operation);
			}
		}

		// Token: 0x06000501 RID: 1281 RVA: 0x000283AA File Offset: 0x000273AA
		public MapLocatorOperationContainer(OperationContainer _operationContainer)
		{
			this.operationContainer = _operationContainer;
		}

		// Token: 0x06000502 RID: 1282 RVA: 0x000283B9 File Offset: 0x000273B9
		public void Destroy()
		{
			if (this.operationContainer != null)
			{
				this.operationContainer = null;
			}
		}

		// Token: 0x06000503 RID: 1283 RVA: 0x000283CC File Offset: 0x000273CC
		public void Bind(MapEditorMapObjectContainer _mapEditorMapObjectContainer)
		{
			this.Unbind();
			this.mapEditorMapObjectContainer = _mapEditorMapObjectContainer;
			if (this.mapEditorMapObjectContainer != null)
			{
				MapLocatorContainer mapLocatorContainer = this.mapEditorMapObjectContainer.MapLocatorContainer;
				if (mapLocatorContainer != null)
				{
					mapLocatorContainer.MapLocatorScriptIDChanged += this.OnMapLocatorScriptIDChanged;
					mapLocatorContainer.MapLocatorScanRadiusChanged += this.OnMapLocatorScanRadiusChanged;
				}
			}
		}

		// Token: 0x06000504 RID: 1284 RVA: 0x00028424 File Offset: 0x00027424
		public void Unbind()
		{
			if (this.mapEditorMapObjectContainer != null)
			{
				MapLocatorContainer mapLocatorContainer = this.mapEditorMapObjectContainer.MapLocatorContainer;
				if (mapLocatorContainer != null)
				{
					mapLocatorContainer.MapLocatorScriptIDChanged -= this.OnMapLocatorScriptIDChanged;
					mapLocatorContainer.MapLocatorScanRadiusChanged -= this.OnMapLocatorScanRadiusChanged;
				}
				this.mapEditorMapObjectContainer = null;
			}
		}

		// Token: 0x040003A6 RID: 934
		private MapEditorMapObjectContainer mapEditorMapObjectContainer;

		// Token: 0x040003A7 RID: 935
		private OperationContainer operationContainer;
	}
}
