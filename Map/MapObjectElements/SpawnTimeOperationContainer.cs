using System;
using MapEditor.Map.Containers;
using Operations;

namespace MapEditor.Map.MapObjectElements
{
	// Token: 0x0200028E RID: 654
	public class SpawnTimeOperationContainer
	{
		// Token: 0x06001EC3 RID: 7875 RVA: 0x000C693C File Offset: 0x000C593C
		private void OnSpawnTimeFieldChanged(SpawnTimeAbstract spawnTime, string field, object oldValue, object newValue)
		{
			if (spawnTime != null && !string.IsNullOrEmpty(field))
			{
				ChangeSpawnTimeFieldOperation operation = new ChangeSpawnTimeFieldOperation(spawnTime, field, oldValue, newValue);
				this.operationContainer.Add(operation);
				if (this.mapEditorMapObjectContainer != null)
				{
					this.mapEditorMapObjectContainer.InvokeModified();
				}
			}
		}

		// Token: 0x06001EC4 RID: 7876 RVA: 0x000C697E File Offset: 0x000C597E
		public SpawnTimeOperationContainer(OperationContainer _operationContainer)
		{
			this.operationContainer = _operationContainer;
		}

		// Token: 0x06001EC5 RID: 7877 RVA: 0x000C698D File Offset: 0x000C598D
		public void Destroy()
		{
			if (this.operationContainer != null)
			{
				this.operationContainer = null;
			}
		}

		// Token: 0x06001EC6 RID: 7878 RVA: 0x000C699E File Offset: 0x000C599E
		public void Bind(MapEditorMapObjectContainer _mapEditorMapObjectContainer)
		{
			this.Unbind();
			this.mapEditorMapObjectContainer = _mapEditorMapObjectContainer;
			SpawnTimeAbstract.Changed += this.OnSpawnTimeFieldChanged;
		}

		// Token: 0x06001EC7 RID: 7879 RVA: 0x000C69BE File Offset: 0x000C59BE
		public void Unbind()
		{
			this.mapEditorMapObjectContainer = null;
			SpawnTimeAbstract.Changed -= this.OnSpawnTimeFieldChanged;
		}

		// Token: 0x0400132C RID: 4908
		private OperationContainer operationContainer;

		// Token: 0x0400132D RID: 4909
		private MapEditorMapObjectContainer mapEditorMapObjectContainer;
	}
}
