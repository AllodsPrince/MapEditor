using System;
using MapEditor.Map.Containers;
using Operations;

namespace MapEditor.Map.MapObjectElements
{
	// Token: 0x020002CB RID: 715
	public class SingleSpawnControllerOperationContainer
	{
		// Token: 0x0600212F RID: 8495 RVA: 0x000D2104 File Offset: 0x000D1104
		private void OnSingleSpawnControllerFieldChanged(SingleSpawnController sender, string oldValue, string newValue)
		{
			if (sender != null)
			{
				ChangeSingleSpawnControllerFieldOperation operation = new ChangeSingleSpawnControllerFieldOperation(sender, oldValue, newValue);
				this.operationContainer.Add(operation);
				if (this.mapEditorMapObjectContainer != null)
				{
					this.mapEditorMapObjectContainer.InvokeModified();
				}
			}
		}

		// Token: 0x06002130 RID: 8496 RVA: 0x000D213C File Offset: 0x000D113C
		public SingleSpawnControllerOperationContainer(OperationContainer _operationContainer)
		{
			this.operationContainer = _operationContainer;
		}

		// Token: 0x06002131 RID: 8497 RVA: 0x000D214B File Offset: 0x000D114B
		public void Destroy()
		{
			if (this.operationContainer != null)
			{
				this.operationContainer = null;
			}
		}

		// Token: 0x06002132 RID: 8498 RVA: 0x000D215C File Offset: 0x000D115C
		public void Bind(MapEditorMapObjectContainer _mapEditorMapObjectContainer)
		{
			this.Unbind();
			this.mapEditorMapObjectContainer = _mapEditorMapObjectContainer;
			SingleSpawnController.Changed += this.OnSingleSpawnControllerFieldChanged;
		}

		// Token: 0x06002133 RID: 8499 RVA: 0x000D217C File Offset: 0x000D117C
		public void Unbind()
		{
			this.mapEditorMapObjectContainer = null;
			SingleSpawnController.Changed -= this.OnSingleSpawnControllerFieldChanged;
		}

		// Token: 0x0400141B RID: 5147
		private OperationContainer operationContainer;

		// Token: 0x0400141C RID: 5148
		private MapEditorMapObjectContainer mapEditorMapObjectContainer;
	}
}
