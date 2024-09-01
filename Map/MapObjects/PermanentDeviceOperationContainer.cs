using System;
using MapEditor.Map.Containers;
using Operations;

namespace MapEditor.Map.MapObjects
{
	// Token: 0x02000200 RID: 512
	public class PermanentDeviceOperationContainer
	{
		// Token: 0x06001968 RID: 6504 RVA: 0x000A660C File Offset: 0x000A560C
		private void OnPermanentDeviceDeviceChanged(MapEditorMapObjectContainer _mapEditorMapObjectContainer, PermanentDevice permanentDevice, ref string oldValue, ref string newValue)
		{
			if (this.mapEditorMapObjectContainer != null && permanentDevice != null && this.operationContainer != null && !permanentDevice.Temporary)
			{
				ChangePermanentDeviceDeviceOperation operation = new ChangePermanentDeviceDeviceOperation(permanentDevice, ref oldValue, ref newValue);
				this.operationContainer.Add(operation);
			}
		}

		// Token: 0x06001969 RID: 6505 RVA: 0x000A664C File Offset: 0x000A564C
		private void OnPermanentDeviceScriptIDChanged(MapEditorMapObjectContainer _mapEditorMapObjectContainer, PermanentDevice permanentDevice, ref string oldValue, ref string newValue)
		{
			if (this.mapEditorMapObjectContainer != null && permanentDevice != null && this.operationContainer != null && !permanentDevice.Temporary)
			{
				ChangePermanentDeviceScriptIDOperation operation = new ChangePermanentDeviceScriptIDOperation(permanentDevice, ref oldValue, ref newValue);
				this.operationContainer.Add(operation);
			}
		}

		// Token: 0x0600196A RID: 6506 RVA: 0x000A668C File Offset: 0x000A568C
		private void OnPermanentDeviceScanRadiusChanged(MapEditorMapObjectContainer _mapEditorMapObjectContainer, PermanentDevice permanentDevice, ref double oldValue, ref double newValue)
		{
			if (this.mapEditorMapObjectContainer != null && permanentDevice != null && this.operationContainer != null && !permanentDevice.Temporary)
			{
				ChangePermanentDeviceScanRadiusOperation operation = new ChangePermanentDeviceScanRadiusOperation(permanentDevice, ref oldValue, ref newValue);
				this.operationContainer.Add(operation);
			}
		}

		// Token: 0x0600196B RID: 6507 RVA: 0x000A66CC File Offset: 0x000A56CC
		private void OnPermanentDeviceAICollisionChanged(MapEditorMapObjectContainer _mapEditorMapObjectContainer, PermanentDevice permanentDevice, ref bool oldValue, ref bool newValue)
		{
			if (this.mapEditorMapObjectContainer != null && permanentDevice != null && this.operationContainer != null && !permanentDevice.Temporary)
			{
				ChangePermanentDeviceAICollisionOperation operation = new ChangePermanentDeviceAICollisionOperation(permanentDevice, ref oldValue, ref newValue);
				this.operationContainer.Add(operation);
			}
		}

		// Token: 0x0600196C RID: 6508 RVA: 0x000A670A File Offset: 0x000A570A
		public PermanentDeviceOperationContainer(OperationContainer _operationContainer)
		{
			this.operationContainer = _operationContainer;
		}

		// Token: 0x0600196D RID: 6509 RVA: 0x000A6719 File Offset: 0x000A5719
		public void Destroy()
		{
			if (this.operationContainer != null)
			{
				this.operationContainer = null;
			}
		}

		// Token: 0x0600196E RID: 6510 RVA: 0x000A672C File Offset: 0x000A572C
		public void Bind(MapEditorMapObjectContainer _mapEditorMapObjectContainer)
		{
			this.Unbind();
			this.mapEditorMapObjectContainer = _mapEditorMapObjectContainer;
			if (this.mapEditorMapObjectContainer != null)
			{
				PermanentDeviceContainer permanentDeviceContainer = this.mapEditorMapObjectContainer.PermanentDeviceContainer;
				if (permanentDeviceContainer != null)
				{
					permanentDeviceContainer.PermanentDeviceDeviceChanged += this.OnPermanentDeviceDeviceChanged;
					permanentDeviceContainer.PermanentDeviceScriptIDChanged += this.OnPermanentDeviceScriptIDChanged;
					permanentDeviceContainer.PermanentDeviceScanRadiusChanged += this.OnPermanentDeviceScanRadiusChanged;
					permanentDeviceContainer.PermanentDeviceAICollisionChanged += this.OnPermanentDeviceAICollisionChanged;
				}
			}
		}

		// Token: 0x0600196F RID: 6511 RVA: 0x000A67A8 File Offset: 0x000A57A8
		public void Unbind()
		{
			if (this.mapEditorMapObjectContainer != null)
			{
				PermanentDeviceContainer permanentDeviceContainer = this.mapEditorMapObjectContainer.PermanentDeviceContainer;
				if (permanentDeviceContainer != null)
				{
					permanentDeviceContainer.PermanentDeviceDeviceChanged -= this.OnPermanentDeviceDeviceChanged;
					permanentDeviceContainer.PermanentDeviceScriptIDChanged -= this.OnPermanentDeviceScriptIDChanged;
					permanentDeviceContainer.PermanentDeviceScanRadiusChanged -= this.OnPermanentDeviceScanRadiusChanged;
					permanentDeviceContainer.PermanentDeviceAICollisionChanged -= this.OnPermanentDeviceAICollisionChanged;
				}
				this.mapEditorMapObjectContainer = null;
			}
		}

		// Token: 0x04001045 RID: 4165
		private MapEditorMapObjectContainer mapEditorMapObjectContainer;

		// Token: 0x04001046 RID: 4166
		private OperationContainer operationContainer;
	}
}
