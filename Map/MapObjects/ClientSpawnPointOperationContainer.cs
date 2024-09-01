using System;
using MapEditor.Map.Containers;
using Operations;

namespace MapEditor.Map.MapObjects
{
	// Token: 0x02000233 RID: 563
	public class ClientSpawnPointOperationContainer
	{
		// Token: 0x06001AF5 RID: 6901 RVA: 0x000AF318 File Offset: 0x000AE318
		private void OnClientSpawnPointVisObjectChanged(MapEditorMapObjectContainer _mapEditorMapObjectContainer, ClientSpawnPoint clientSpawnPoint, ref string oldValue, ref string newValue)
		{
			if (this.mapEditorMapObjectContainer != null && clientSpawnPoint != null && this.operationContainer != null && !clientSpawnPoint.Temporary)
			{
				ChangeClientSpawnPointVisObjectOperation operation = new ChangeClientSpawnPointVisObjectOperation(clientSpawnPoint, ref oldValue, ref newValue);
				this.operationContainer.Add(operation);
			}
		}

		// Token: 0x06001AF6 RID: 6902 RVA: 0x000AF358 File Offset: 0x000AE358
		private void OnClientSpawnPointSceneChanged(MapEditorMapObjectContainer _mapEditorMapObjectContainer, ClientSpawnPoint clientSpawnPoint, ref string oldValue, ref string newValue)
		{
			if (this.mapEditorMapObjectContainer != null && clientSpawnPoint != null && this.operationContainer != null && !clientSpawnPoint.Temporary)
			{
				ChangeClientSpawnPointSceneOperation operation = new ChangeClientSpawnPointSceneOperation(clientSpawnPoint, ref oldValue, ref newValue);
				this.operationContainer.Add(operation);
			}
		}

		// Token: 0x06001AF7 RID: 6903 RVA: 0x000AF398 File Offset: 0x000AE398
		private void OnClientSpawnPointScriptIDChanged(MapEditorMapObjectContainer _mapEditorMapObjectContainer, ClientSpawnPoint clientSpawnPoint, ref string oldValue, ref string newValue)
		{
			if (this.mapEditorMapObjectContainer != null && clientSpawnPoint != null && this.operationContainer != null && !clientSpawnPoint.Temporary)
			{
				ChangeClientSpawnPointScriptIDOperation operation = new ChangeClientSpawnPointScriptIDOperation(clientSpawnPoint, ref oldValue, ref newValue);
				this.operationContainer.Add(operation);
			}
		}

		// Token: 0x06001AF8 RID: 6904 RVA: 0x000AF3D8 File Offset: 0x000AE3D8
		private void OnClientSpawnPointDataFieldChanged(ClientSpawnPointData sender, string field, object oldValue, object newValue)
		{
			if (sender != null && !string.IsNullOrEmpty(field))
			{
				ChangeClientSpawnPointDataFieldOperation operation = new ChangeClientSpawnPointDataFieldOperation(sender, field, oldValue, newValue);
				this.operationContainer.Add(operation);
			}
		}

		// Token: 0x06001AF9 RID: 6905 RVA: 0x000AF408 File Offset: 0x000AE408
		private void OnClientSpawnPointDataChanged(MapEditorMapObjectContainer _mapEditorMapObjectContainer, ClientSpawnPoint clientSpawnPoint, ref ClientSpawnPointData oldValue, ref ClientSpawnPointData newValue)
		{
			if (this.mapEditorMapObjectContainer != null && clientSpawnPoint != null && this.operationContainer != null && !clientSpawnPoint.Temporary)
			{
				ChangeClientSpawnPointDataOperation operation = new ChangeClientSpawnPointDataOperation(clientSpawnPoint, ref oldValue, ref newValue);
				this.operationContainer.Add(operation);
			}
		}

		// Token: 0x06001AFA RID: 6906 RVA: 0x000AF446 File Offset: 0x000AE446
		public ClientSpawnPointOperationContainer(OperationContainer _operationContainer)
		{
			this.operationContainer = _operationContainer;
		}

		// Token: 0x06001AFB RID: 6907 RVA: 0x000AF455 File Offset: 0x000AE455
		public void Destroy()
		{
			if (this.operationContainer != null)
			{
				this.operationContainer = null;
			}
		}

		// Token: 0x06001AFC RID: 6908 RVA: 0x000AF468 File Offset: 0x000AE468
		public void Bind(MapEditorMapObjectContainer _mapEditorMapObjectContainer)
		{
			this.Unbind();
			this.mapEditorMapObjectContainer = _mapEditorMapObjectContainer;
			if (this.mapEditorMapObjectContainer != null)
			{
				ClientSpawnPointContainer clientSpawnPointContainer = this.mapEditorMapObjectContainer.ClientSpawnPointContainer;
				if (clientSpawnPointContainer != null)
				{
					clientSpawnPointContainer.ClientSpawnPointVisObjectChanged += this.OnClientSpawnPointVisObjectChanged;
					clientSpawnPointContainer.ClientSpawnPointSceneChanged += this.OnClientSpawnPointSceneChanged;
					clientSpawnPointContainer.ClientSpawnPointScriptIDChanged += this.OnClientSpawnPointScriptIDChanged;
					clientSpawnPointContainer.ClientSpawnPointDataFieldChanged += this.OnClientSpawnPointDataFieldChanged;
					clientSpawnPointContainer.ClientSpawnPointDataChanged += this.OnClientSpawnPointDataChanged;
				}
			}
		}

		// Token: 0x06001AFD RID: 6909 RVA: 0x000AF4F4 File Offset: 0x000AE4F4
		public void Unbind()
		{
			if (this.mapEditorMapObjectContainer != null)
			{
				ClientSpawnPointContainer clientSpawnPointContainer = this.mapEditorMapObjectContainer.ClientSpawnPointContainer;
				if (clientSpawnPointContainer != null)
				{
					clientSpawnPointContainer.ClientSpawnPointVisObjectChanged -= this.OnClientSpawnPointVisObjectChanged;
					clientSpawnPointContainer.ClientSpawnPointSceneChanged -= this.OnClientSpawnPointSceneChanged;
					clientSpawnPointContainer.ClientSpawnPointScriptIDChanged -= this.OnClientSpawnPointScriptIDChanged;
					clientSpawnPointContainer.ClientSpawnPointDataFieldChanged -= this.OnClientSpawnPointDataFieldChanged;
					clientSpawnPointContainer.ClientSpawnPointDataChanged -= this.OnClientSpawnPointDataChanged;
				}
				this.mapEditorMapObjectContainer = null;
			}
		}

		// Token: 0x0400114B RID: 4427
		private MapEditorMapObjectContainer mapEditorMapObjectContainer;

		// Token: 0x0400114C RID: 4428
		private OperationContainer operationContainer;
	}
}
