using System;
using MapEditor.Map.Containers;
using Operations;

namespace MapEditor.Map.MapObjects
{
	// Token: 0x0200010C RID: 268
	public class StaticObjectOperationContainer
	{
		// Token: 0x06000D2C RID: 3372 RVA: 0x0006E67C File Offset: 0x0006D67C
		private void OnStaticObjectAICollisionChanged(MapEditorMapObjectContainer _mapEditorMapObjectContainer, StaticObject staticObject, ref bool oldValue, ref bool newValue)
		{
			if (this.mapEditorMapObjectContainer != null && staticObject != null && this.operationContainer != null && !staticObject.Temporary)
			{
				ChangeStaticObjectAICollisionOperation operation = new ChangeStaticObjectAICollisionOperation(staticObject, ref oldValue, ref newValue);
				this.operationContainer.Add(operation);
			}
		}

		// Token: 0x06000D2D RID: 3373 RVA: 0x0006E6BA File Offset: 0x0006D6BA
		public StaticObjectOperationContainer(OperationContainer _operationContainer)
		{
			this.operationContainer = _operationContainer;
		}

		// Token: 0x06000D2E RID: 3374 RVA: 0x0006E6C9 File Offset: 0x0006D6C9
		public void Destroy()
		{
			if (this.operationContainer != null)
			{
				this.operationContainer = null;
			}
		}

		// Token: 0x06000D2F RID: 3375 RVA: 0x0006E6DC File Offset: 0x0006D6DC
		public void Bind(MapEditorMapObjectContainer _mapEditorMapObjectContainer)
		{
			this.Unbind();
			this.mapEditorMapObjectContainer = _mapEditorMapObjectContainer;
			if (this.mapEditorMapObjectContainer != null)
			{
				StaticObjectContainer staticObjectContainer = this.mapEditorMapObjectContainer.StaticObjectContainer;
				if (staticObjectContainer != null)
				{
					staticObjectContainer.StaticObjectAICollisionChanged += this.OnStaticObjectAICollisionChanged;
				}
			}
		}

		// Token: 0x06000D30 RID: 3376 RVA: 0x0006E720 File Offset: 0x0006D720
		public void Unbind()
		{
			if (this.mapEditorMapObjectContainer != null)
			{
				StaticObjectContainer staticObjectContainer = this.mapEditorMapObjectContainer.StaticObjectContainer;
				if (staticObjectContainer != null)
				{
					staticObjectContainer.StaticObjectAICollisionChanged -= this.OnStaticObjectAICollisionChanged;
				}
				this.mapEditorMapObjectContainer = null;
			}
		}

		// Token: 0x04000A95 RID: 2709
		private MapEditorMapObjectContainer mapEditorMapObjectContainer;

		// Token: 0x04000A96 RID: 2710
		private OperationContainer operationContainer;
	}
}
