using System;
using MapEditor.Map.MapObjects;

namespace MapEditor.Map.Containers
{
	// Token: 0x020002AC RID: 684
	public class StaticObjectContainer : TypedMapObjectContainer
	{
		// Token: 0x140000E1 RID: 225
		// (add) Token: 0x06001FA7 RID: 8103 RVA: 0x000CB5DC File Offset: 0x000CA5DC
		// (remove) Token: 0x06001FA8 RID: 8104 RVA: 0x000CB5F5 File Offset: 0x000CA5F5
		public event StaticObjectContainer.StaticObjectFieldChangedEvent<bool> StaticObjectAICollisionChanged;

		// Token: 0x06001FA9 RID: 8105 RVA: 0x000CB610 File Offset: 0x000CA610
		private void OnStaticObjectAICollisionChanged(StaticObject staticObject, ref bool oldValue, ref bool newValue)
		{
			if (this.mapEditorMapObjectContainer != null && base.Contains(staticObject))
			{
				if (this.StaticObjectAICollisionChanged != null)
				{
					this.StaticObjectAICollisionChanged(this.mapEditorMapObjectContainer, staticObject, ref oldValue, ref newValue);
				}
				if (!staticObject.Temporary)
				{
					this.mapEditorMapObjectContainer.InvokeModified();
				}
			}
		}

		// Token: 0x06001FAA RID: 8106 RVA: 0x000CB65D File Offset: 0x000CA65D
		public StaticObjectContainer(MapEditorMapObjectContainer _mapEditorMapObjectContainer) : base(MapObjectFactory.Type.StaticObject, false)
		{
			this.mapEditorMapObjectContainer = _mapEditorMapObjectContainer;
			if (this.mapEditorMapObjectContainer != null)
			{
				StaticObject.AICollisionChanged += this.OnStaticObjectAICollisionChanged;
				base.Destroyed += this.OnDestroyed;
			}
		}

		// Token: 0x06001FAB RID: 8107 RVA: 0x000CB69D File Offset: 0x000CA69D
		public void OnDestroyed(TypedMapObjectContainer _typedMapObjectContainer)
		{
			if (this.mapEditorMapObjectContainer != null)
			{
				StaticObject.AICollisionChanged -= this.OnStaticObjectAICollisionChanged;
				base.Destroyed -= this.OnDestroyed;
				this.mapEditorMapObjectContainer = null;
			}
		}

		// Token: 0x04001383 RID: 4995
		private MapEditorMapObjectContainer mapEditorMapObjectContainer;

		// Token: 0x020002AD RID: 685
		// (Invoke) Token: 0x06001FAD RID: 8109
		public delegate void StaticObjectFieldChangedEvent<T>(MapEditorMapObjectContainer _mapEditorMapObjectContainer, StaticObject staticObject, ref T oldValue, ref T newValue);
	}
}
