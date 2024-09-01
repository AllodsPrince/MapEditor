using System;
using MapEditor.Map.Containers;
using Operations;

namespace MapEditor.Map.MapObjects
{
	// Token: 0x02000280 RID: 640
	public class StartPointOperationContainer
	{
		// Token: 0x06001E64 RID: 7780 RVA: 0x000C5B50 File Offset: 0x000C4B50
		private void OnStartPointCharacterChanged(MapEditorMapObjectContainer _mapEditorMapObjectContainer, StartPoint startPoint, ref string oldValue, ref string newValue)
		{
			if (this.mapEditorMapObjectContainer != null && startPoint != null && this.operationContainer != null && !startPoint.Temporary)
			{
				ChangeStartPointCharacterOperation operation = new ChangeStartPointCharacterOperation(startPoint, ref oldValue, ref newValue);
				this.operationContainer.Add(operation);
			}
		}

		// Token: 0x06001E65 RID: 7781 RVA: 0x000C5B8E File Offset: 0x000C4B8E
		public StartPointOperationContainer(OperationContainer _operationContainer)
		{
			this.operationContainer = _operationContainer;
		}

		// Token: 0x06001E66 RID: 7782 RVA: 0x000C5B9D File Offset: 0x000C4B9D
		public void Destroy()
		{
			if (this.operationContainer != null)
			{
				this.operationContainer = null;
			}
		}

		// Token: 0x06001E67 RID: 7783 RVA: 0x000C5BB0 File Offset: 0x000C4BB0
		public void Bind(MapEditorMapObjectContainer _mapEditorMapObjectContainer)
		{
			this.Unbind();
			this.mapEditorMapObjectContainer = _mapEditorMapObjectContainer;
			if (this.mapEditorMapObjectContainer != null)
			{
				StartPointContainer startPointContainer = this.mapEditorMapObjectContainer.StartPointContainer;
				if (startPointContainer != null)
				{
					startPointContainer.StartPointCharacterChanged += this.OnStartPointCharacterChanged;
				}
			}
		}

		// Token: 0x06001E68 RID: 7784 RVA: 0x000C5BF4 File Offset: 0x000C4BF4
		public void Unbind()
		{
			if (this.mapEditorMapObjectContainer != null)
			{
				StartPointContainer startPointContainer = this.mapEditorMapObjectContainer.StartPointContainer;
				if (startPointContainer != null)
				{
					startPointContainer.StartPointCharacterChanged -= this.OnStartPointCharacterChanged;
				}
				this.mapEditorMapObjectContainer = null;
			}
		}

		// Token: 0x04001307 RID: 4871
		private MapEditorMapObjectContainer mapEditorMapObjectContainer;

		// Token: 0x04001308 RID: 4872
		private OperationContainer operationContainer;
	}
}
