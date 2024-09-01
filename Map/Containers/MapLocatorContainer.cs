using System;
using MapEditor.Map.MapObjects;

namespace MapEditor.Map.Containers
{
	// Token: 0x0200029B RID: 667
	public class MapLocatorContainer : TypedMapObjectContainer
	{
		// Token: 0x06001F18 RID: 7960 RVA: 0x000C758C File Offset: 0x000C658C
		private void OnMapLocatorScriptIDChanged(MapLocator mapLocator, ref string oldValue, ref string newValue)
		{
			if (this.mapEditorMapObjectContainer != null && base.Contains(mapLocator))
			{
				if (this.MapLocatorScriptIDChanged != null)
				{
					this.MapLocatorScriptIDChanged(this.mapEditorMapObjectContainer, mapLocator, ref oldValue, ref newValue);
				}
				if (!mapLocator.Temporary)
				{
					this.mapEditorMapObjectContainer.InvokeModified();
				}
			}
		}

		// Token: 0x06001F19 RID: 7961 RVA: 0x000C75DC File Offset: 0x000C65DC
		private void OnMapLocatorScanRadiusChanged(MapLocator mapLocator, ref double oldValue, ref double newValue)
		{
			if (this.mapEditorMapObjectContainer != null && base.Contains(mapLocator))
			{
				if (this.MapLocatorScanRadiusChanged != null)
				{
					this.MapLocatorScanRadiusChanged(this.mapEditorMapObjectContainer, mapLocator, ref oldValue, ref newValue);
				}
				if (!mapLocator.Temporary)
				{
					this.mapEditorMapObjectContainer.InvokeModified();
				}
			}
		}

		// Token: 0x140000DF RID: 223
		// (add) Token: 0x06001F1A RID: 7962 RVA: 0x000C7629 File Offset: 0x000C6629
		// (remove) Token: 0x06001F1B RID: 7963 RVA: 0x000C7642 File Offset: 0x000C6642
		public event MapLocatorContainer.MapLocatorFieldChangedEvent<string> MapLocatorScriptIDChanged;

		// Token: 0x140000E0 RID: 224
		// (add) Token: 0x06001F1C RID: 7964 RVA: 0x000C765B File Offset: 0x000C665B
		// (remove) Token: 0x06001F1D RID: 7965 RVA: 0x000C7674 File Offset: 0x000C6674
		public event MapLocatorContainer.MapLocatorFieldChangedEvent<double> MapLocatorScanRadiusChanged;

		// Token: 0x06001F1E RID: 7966 RVA: 0x000C7690 File Offset: 0x000C6690
		public MapLocatorContainer(MapEditorMapObjectContainer _mapEditorMapObjectContainer) : base(MapObjectFactory.Type.MapLocator, false)
		{
			this.mapEditorMapObjectContainer = _mapEditorMapObjectContainer;
			if (this.mapEditorMapObjectContainer != null)
			{
				MapLocator.ScriptIDChanged += this.OnMapLocatorScriptIDChanged;
				MapLocator.ScanRadiusChanged += this.OnMapLocatorScanRadiusChanged;
				base.Destroyed += this.OnDestroyed;
			}
		}

		// Token: 0x06001F1F RID: 7967 RVA: 0x000C76EC File Offset: 0x000C66EC
		public void OnDestroyed(TypedMapObjectContainer _typedMapObjectContainer)
		{
			if (this.mapEditorMapObjectContainer != null)
			{
				MapLocator.ScriptIDChanged -= this.OnMapLocatorScriptIDChanged;
				MapLocator.ScanRadiusChanged -= this.OnMapLocatorScanRadiusChanged;
				base.Destroyed -= this.OnDestroyed;
				this.mapEditorMapObjectContainer = null;
			}
		}

		// Token: 0x04001341 RID: 4929
		private MapEditorMapObjectContainer mapEditorMapObjectContainer;

		// Token: 0x0200029C RID: 668
		// (Invoke) Token: 0x06001F21 RID: 7969
		public delegate void MapLocatorFieldChangedEvent<T>(MapEditorMapObjectContainer _mapEditorMapObjectContainer, MapLocator mapLocator, ref T oldValue, ref T newValue);
	}
}
