using System;
using MapEditor.Map.Containers;
using MapEditor.Map.MapObjects;
using Operations;
using Tools.Geometry;
using Tools.MapObjects;

namespace MapEditor.Forms.ObjectEditor
{
	// Token: 0x02000242 RID: 578
	public class ObjectEditorMap
	{
		// Token: 0x06001B86 RID: 7046 RVA: 0x000B2500 File Offset: 0x000B1500
		public ObjectEditorMap(OperationContainer _operationContainer)
		{
			this.data = new ObjectEditorMap.Variables();
			this.operationContainer = _operationContainer;
			this.mapObjectFactory = new MapObjectFactory();
			this.mapEditorMapObjectContainer = new MapEditorMapObjectContainer(this.operationContainer, this.mapObjectFactory, null, null, null, null, null);
			this.mapObjectOperationContainer = new MapObjectOperationContainer(this.operationContainer);
			this.mapObjectOperationContainer.Bind(this.mapEditorMapObjectContainer);
		}

		// Token: 0x06001B87 RID: 7047 RVA: 0x000B2570 File Offset: 0x000B1570
		public void Destroy()
		{
			if (this.data != null)
			{
				this.mapObjectOperationContainer.Unbind();
				this.mapEditorMapObjectContainer.Destroy();
				this.mapEditorMapObjectContainer = null;
				this.mapObjectOperationContainer.Destroy();
				this.mapObjectOperationContainer = null;
				this.operationContainer = null;
				this.mapObjectFactory = null;
				this.data = null;
			}
		}

		// Token: 0x17000675 RID: 1653
		// (get) Token: 0x06001B88 RID: 7048 RVA: 0x000B25C9 File Offset: 0x000B15C9
		public MapObjectFactory MapObjectFactory
		{
			get
			{
				return this.mapObjectFactory;
			}
		}

		// Token: 0x17000676 RID: 1654
		// (get) Token: 0x06001B89 RID: 7049 RVA: 0x000B25D1 File Offset: 0x000B15D1
		public MapEditorMapObjectContainer MapEditorMapObjectContainer
		{
			get
			{
				return this.mapEditorMapObjectContainer;
			}
		}

		// Token: 0x17000677 RID: 1655
		// (get) Token: 0x06001B8A RID: 7050 RVA: 0x000B25D9 File Offset: 0x000B15D9
		public OperationContainer OperationContainer
		{
			get
			{
				return this.operationContainer;
			}
		}

		// Token: 0x17000678 RID: 1656
		// (get) Token: 0x06001B8B RID: 7051 RVA: 0x000B25E1 File Offset: 0x000B15E1
		public ObjectEditorMap.Variables Data
		{
			get
			{
				return this.data;
			}
		}

		// Token: 0x04001188 RID: 4488
		private ObjectEditorMap.Variables data;

		// Token: 0x04001189 RID: 4489
		private OperationContainer operationContainer;

		// Token: 0x0400118A RID: 4490
		private MapObjectFactory mapObjectFactory;

		// Token: 0x0400118B RID: 4491
		private MapEditorMapObjectContainer mapEditorMapObjectContainer;

		// Token: 0x0400118C RID: 4492
		private MapObjectOperationContainer mapObjectOperationContainer;

		// Token: 0x02000243 RID: 579
		public class Variables
		{
			// Token: 0x17000679 RID: 1657
			// (get) Token: 0x06001B8C RID: 7052 RVA: 0x000B25E9 File Offset: 0x000B15E9
			// (set) Token: 0x06001B8D RID: 7053 RVA: 0x000B25F1 File Offset: 0x000B15F1
			public Point MinXMinYPatchCoords
			{
				get
				{
					return this.minXMinYPatchCoords;
				}
				set
				{
					this.minXMinYPatchCoords = value;
				}
			}

			// Token: 0x1700067A RID: 1658
			// (get) Token: 0x06001B8E RID: 7054 RVA: 0x000B25FA File Offset: 0x000B15FA
			// (set) Token: 0x06001B8F RID: 7055 RVA: 0x000B2602 File Offset: 0x000B1602
			public string ContinentName
			{
				get
				{
					return this.continentName;
				}
				set
				{
					this.continentName = value;
				}
			}

			// Token: 0x0400118D RID: 4493
			private Point minXMinYPatchCoords;

			// Token: 0x0400118E RID: 4494
			private string continentName;
		}
	}
}
