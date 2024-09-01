using System;
using MapEditor.Map.Containers;
using Operations;

namespace MapEditor.Map.MapObjects
{
	// Token: 0x020001A7 RID: 423
	public class ZoneLocatorOperationContainer
	{
		// Token: 0x0600147E RID: 5246 RVA: 0x00093E8C File Offset: 0x00092E8C
		private void OnZoneLocatorMapZoneChanged(MapEditorMapObjectContainer _mapEditorMapObjectContainer, ZoneLocator zoneLocator, ref string oldValue, ref string newValue)
		{
			if (this.mapEditorMapObjectContainer != null && zoneLocator != null && this.operationContainer != null && !zoneLocator.Temporary)
			{
				ChangeZoneLocatorMapZoneOperation operation = new ChangeZoneLocatorMapZoneOperation(zoneLocator, ref oldValue, ref newValue);
				this.operationContainer.Add(operation);
			}
		}

		// Token: 0x0600147F RID: 5247 RVA: 0x00093ECA File Offset: 0x00092ECA
		public ZoneLocatorOperationContainer(OperationContainer _operationContainer)
		{
			this.operationContainer = _operationContainer;
		}

		// Token: 0x06001480 RID: 5248 RVA: 0x00093ED9 File Offset: 0x00092ED9
		public void Destroy()
		{
			if (this.operationContainer != null)
			{
				this.operationContainer = null;
			}
		}

		// Token: 0x06001481 RID: 5249 RVA: 0x00093EEC File Offset: 0x00092EEC
		public void Bind(MapEditorMapObjectContainer _mapEditorMapObjectContainer)
		{
			this.Unbind();
			this.mapEditorMapObjectContainer = _mapEditorMapObjectContainer;
			if (this.mapEditorMapObjectContainer != null)
			{
				ZoneLocatorContainer zoneLocatorContainer = this.mapEditorMapObjectContainer.ZoneLocatorContainer;
				if (zoneLocatorContainer != null)
				{
					zoneLocatorContainer.ZoneLocatorMapZoneChanged += this.OnZoneLocatorMapZoneChanged;
				}
			}
		}

		// Token: 0x06001482 RID: 5250 RVA: 0x00093F30 File Offset: 0x00092F30
		public void Unbind()
		{
			if (this.mapEditorMapObjectContainer != null)
			{
				ZoneLocatorContainer zoneLocatorContainer = this.mapEditorMapObjectContainer.ZoneLocatorContainer;
				if (zoneLocatorContainer != null)
				{
					zoneLocatorContainer.ZoneLocatorMapZoneChanged -= this.OnZoneLocatorMapZoneChanged;
				}
				this.mapEditorMapObjectContainer = null;
			}
		}

		// Token: 0x04000E62 RID: 3682
		private MapEditorMapObjectContainer mapEditorMapObjectContainer;

		// Token: 0x04000E63 RID: 3683
		private OperationContainer operationContainer;
	}
}
