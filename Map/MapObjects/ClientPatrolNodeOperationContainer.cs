using System;
using MapEditor.Map.Containers;
using Operations;

namespace MapEditor.Map.MapObjects
{
	// Token: 0x02000040 RID: 64
	public class ClientPatrolNodeOperationContainer
	{
		// Token: 0x060003A9 RID: 937 RVA: 0x0001FF9C File Offset: 0x0001EF9C
		private void OnClientPatrolNodeSceneChanged(MapEditorMapObjectContainer _mapEditorMapObjectContainer, ClientPatrolNode clientPatrolNode, ref string oldValue, ref string newValue)
		{
			if (this.mapEditorMapObjectContainer != null && clientPatrolNode != null && this.operationContainer != null && !clientPatrolNode.Temporary)
			{
				ChangeClientPatrolNodeSceneOperation operation = new ChangeClientPatrolNodeSceneOperation(clientPatrolNode, ref oldValue, ref newValue);
				this.operationContainer.Add(operation);
			}
		}

		// Token: 0x060003AA RID: 938 RVA: 0x0001FFDC File Offset: 0x0001EFDC
		private void OnClientPatrolNodeScriptIDChanged(MapEditorMapObjectContainer _mapEditorMapObjectContainer, ClientPatrolNode clientPatrolNode, ref string oldValue, ref string newValue)
		{
			if (this.mapEditorMapObjectContainer != null && clientPatrolNode != null && this.operationContainer != null && !clientPatrolNode.Temporary)
			{
				ChangeClientPatrolNodeScriptIDOperation operation = new ChangeClientPatrolNodeScriptIDOperation(clientPatrolNode, ref oldValue, ref newValue);
				this.operationContainer.Add(operation);
			}
		}

		// Token: 0x060003AB RID: 939 RVA: 0x0002001A File Offset: 0x0001F01A
		public ClientPatrolNodeOperationContainer(OperationContainer _operationContainer)
		{
			this.operationContainer = _operationContainer;
		}

		// Token: 0x060003AC RID: 940 RVA: 0x00020029 File Offset: 0x0001F029
		public void Destroy()
		{
			if (this.operationContainer != null)
			{
				this.operationContainer = null;
			}
		}

		// Token: 0x060003AD RID: 941 RVA: 0x0002003C File Offset: 0x0001F03C
		public void Bind(MapEditorMapObjectContainer _mapEditorMapObjectContainer)
		{
			this.Unbind();
			this.mapEditorMapObjectContainer = _mapEditorMapObjectContainer;
			if (this.mapEditorMapObjectContainer != null)
			{
				ClientPatrolNodeContainer clientPatrolNodeContainer = this.mapEditorMapObjectContainer.ClientPatrolNodeContainer;
				if (clientPatrolNodeContainer != null)
				{
					clientPatrolNodeContainer.ClientPatrolNodeSceneChanged += this.OnClientPatrolNodeSceneChanged;
					clientPatrolNodeContainer.ClientPatrolNodeScriptIDChanged += this.OnClientPatrolNodeScriptIDChanged;
				}
			}
		}

		// Token: 0x060003AE RID: 942 RVA: 0x00020094 File Offset: 0x0001F094
		public void Unbind()
		{
			if (this.mapEditorMapObjectContainer != null)
			{
				ClientPatrolNodeContainer clientPatrolNodeContainer = this.mapEditorMapObjectContainer.ClientPatrolNodeContainer;
				if (clientPatrolNodeContainer != null)
				{
					clientPatrolNodeContainer.ClientPatrolNodeSceneChanged -= this.OnClientPatrolNodeSceneChanged;
					clientPatrolNodeContainer.ClientPatrolNodeScriptIDChanged -= this.OnClientPatrolNodeScriptIDChanged;
				}
				this.mapEditorMapObjectContainer = null;
			}
		}

		// Token: 0x0400029F RID: 671
		private MapEditorMapObjectContainer mapEditorMapObjectContainer;

		// Token: 0x040002A0 RID: 672
		private OperationContainer operationContainer;
	}
}
