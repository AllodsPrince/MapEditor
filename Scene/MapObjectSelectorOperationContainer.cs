using System;
using System.Collections.Generic;
using Operations;
using Tools.MapObjects;

namespace MapEditor.Scene
{
	// Token: 0x02000192 RID: 402
	public class MapObjectSelectorOperationContainer
	{
		// Token: 0x06001329 RID: 4905 RVA: 0x0008D264 File Offset: 0x0008C264
		private void OnMapObjectAdded(MultiMapObject multiMapObject, IMapObject mapObject)
		{
			if (this.operationContainer == null || this.mapObjectSelector == null || this.multiOperation)
			{
				return;
			}
			SelectMapObjectOperation operation = new SelectMapObjectOperation(this.mapObjectSelector, null, mapObject);
			this.operationContainer.Add(operation);
		}

		// Token: 0x0600132A RID: 4906 RVA: 0x0008D2A4 File Offset: 0x0008C2A4
		private void OnMapObjectRemoved(MultiMapObject multiMapObject, IMapObject mapObject)
		{
			if (this.operationContainer == null || this.mapObjectSelector == null || this.multiOperation)
			{
				return;
			}
			SelectMapObjectOperation operation = new SelectMapObjectOperation(this.mapObjectSelector, null, mapObject);
			this.operationContainer.Add(operation);
		}

		// Token: 0x0600132B RID: 4907 RVA: 0x0008D2E4 File Offset: 0x0008C2E4
		private void OnMapObjectListClearing(MultiMapObject multiMapObject, Dictionary<IMapObject, IMapObject> mapObjects)
		{
			if (this.operationContainer == null || this.mapObjectSelector == null || this.multiOperation)
			{
				return;
			}
			SelectMapObjectsOperation operation = new SelectMapObjectsOperation(this.mapObjectSelector, mapObjects.Keys, null);
			this.operationContainer.Add(operation);
		}

		// Token: 0x0600132C RID: 4908 RVA: 0x0008D32C File Offset: 0x0008C32C
		private void OnMapObjectListSyncronized(MultiMapObject multiMapObject, List<IMapObject> mapObjectsRemoved, List<IMapObject> mapObjectsAdded)
		{
			if (this.operationContainer == null || this.mapObjectSelector == null || this.multiOperation)
			{
				return;
			}
			SelectMapObjectsOperation operation = new SelectMapObjectsOperation(this.mapObjectSelector, mapObjectsRemoved, mapObjectsAdded);
			this.operationContainer.Add(operation);
		}

		// Token: 0x0600132D RID: 4909 RVA: 0x0008D36C File Offset: 0x0008C36C
		private void BeginTransaction()
		{
			if (this.operationContainer == null || this.mapObjectSelector == null)
			{
				return;
			}
			this.multiOperation = true;
			this.initialSelectedMapObjects.AddRange(this.mapObjectSelector.MapObjects.Keys);
		}

		// Token: 0x0600132E RID: 4910 RVA: 0x0008D3A4 File Offset: 0x0008C3A4
		private void EndTransaction()
		{
			if (this.operationContainer == null || this.mapObjectSelector == null)
			{
				return;
			}
			List<IMapObject> mapObjectsToUnselect = new List<IMapObject>(this.initialSelectedMapObjects);
			List<IMapObject> mapObjectsToSelect = new List<IMapObject>();
			foreach (KeyValuePair<IMapObject, IMapObject> keyValuePair in this.mapObjectSelector.MapObjects)
			{
				if (this.initialSelectedMapObjects.Contains(keyValuePair.Key))
				{
					mapObjectsToUnselect.Remove(keyValuePair.Key);
				}
				else
				{
					mapObjectsToSelect.Add(keyValuePair.Key);
				}
			}
			if (mapObjectsToUnselect.Count > 0 || mapObjectsToSelect.Count > 0)
			{
				SelectMapObjectsOperation operation = new SelectMapObjectsOperation(this.mapObjectSelector, mapObjectsToUnselect, mapObjectsToSelect);
				this.operationContainer.Add(operation);
			}
			this.initialSelectedMapObjects.Clear();
			this.multiOperation = false;
		}

		// Token: 0x0600132F RID: 4911 RVA: 0x0008D488 File Offset: 0x0008C488
		private void OnTransactionBegun(OperationContainer _operationContainer)
		{
			this.BeginTransaction();
		}

		// Token: 0x06001330 RID: 4912 RVA: 0x0008D490 File Offset: 0x0008C490
		private void OnTransactionUpdated(OperationContainer _operationContainer)
		{
			this.EndTransaction();
			this.BeginTransaction();
		}

		// Token: 0x06001331 RID: 4913 RVA: 0x0008D49E File Offset: 0x0008C49E
		private void OnTransactionEnding(OperationContainer _operationContainer)
		{
			this.EndTransaction();
		}

		// Token: 0x06001332 RID: 4914 RVA: 0x0008D4A8 File Offset: 0x0008C4A8
		public MapObjectSelectorOperationContainer(OperationContainer _operationContainer, MapObjectSelector _mapObjectSelector)
		{
			this.operationContainer = _operationContainer;
			if (this.operationContainer != null)
			{
				this.operationContainer.TransactionBegun += this.OnTransactionBegun;
				this.operationContainer.TransactionUpdated += this.OnTransactionUpdated;
				this.operationContainer.TransactionEnding += this.OnTransactionEnding;
			}
			this.mapObjectSelector = _mapObjectSelector;
			if (this.mapObjectSelector != null && this.mapObjectSelector.MultiMapObject != null)
			{
				this.mapObjectSelector.MultiMapObject.MapObjectAdded += this.OnMapObjectAdded;
				this.mapObjectSelector.MultiMapObject.MapObjectRemoved += this.OnMapObjectRemoved;
				this.mapObjectSelector.MultiMapObject.MapObjectListClearing += this.OnMapObjectListClearing;
				this.mapObjectSelector.MultiMapObject.MapObjectListSyncronized += this.OnMapObjectListSyncronized;
			}
		}

		// Token: 0x06001333 RID: 4915 RVA: 0x0008D5A8 File Offset: 0x0008C5A8
		public void Destroy()
		{
			this.multiOperation = false;
			if (this.mapObjectSelector != null && this.mapObjectSelector.MultiMapObject != null)
			{
				this.mapObjectSelector.MultiMapObject.MapObjectAdded -= this.OnMapObjectAdded;
				this.mapObjectSelector.MultiMapObject.MapObjectRemoved -= this.OnMapObjectRemoved;
				this.mapObjectSelector.MultiMapObject.MapObjectListClearing -= this.OnMapObjectListClearing;
				this.mapObjectSelector.MultiMapObject.MapObjectListSyncronized -= this.OnMapObjectListSyncronized;
				this.mapObjectSelector = null;
			}
			if (this.operationContainer != null)
			{
				this.operationContainer.TransactionBegun -= this.OnTransactionBegun;
				this.operationContainer.TransactionUpdated -= this.OnTransactionUpdated;
				this.operationContainer.TransactionEnding -= this.OnTransactionEnding;
				this.operationContainer = null;
			}
			this.initialSelectedMapObjects.Clear();
		}

		// Token: 0x04000DDF RID: 3551
		private MapObjectSelector mapObjectSelector;

		// Token: 0x04000DE0 RID: 3552
		private OperationContainer operationContainer;

		// Token: 0x04000DE1 RID: 3553
		private bool multiOperation;

		// Token: 0x04000DE2 RID: 3554
		private readonly List<IMapObject> initialSelectedMapObjects = new List<IMapObject>();
	}
}
