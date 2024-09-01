using System;
using System.Collections.Generic;
using MapEditor.Map.MapObjects;
using Tools.MapObjects;

namespace MapEditor.Map.Containers
{
	// Token: 0x0200000B RID: 11
	public class TypedMapObjectContainer
	{
		// Token: 0x14000001 RID: 1
		// (add) Token: 0x06000030 RID: 48 RVA: 0x00003B0F File Offset: 0x00002B0F
		// (remove) Token: 0x06000031 RID: 49 RVA: 0x00003B28 File Offset: 0x00002B28
		public event TypedMapObjectContainer.MapObjectAddedEvent MapObjectAdded;

		// Token: 0x14000002 RID: 2
		// (add) Token: 0x06000032 RID: 50 RVA: 0x00003B41 File Offset: 0x00002B41
		// (remove) Token: 0x06000033 RID: 51 RVA: 0x00003B5A File Offset: 0x00002B5A
		public event TypedMapObjectContainer.MapObjectRemovedEvent MapObjectRemoved;

		// Token: 0x14000003 RID: 3
		// (add) Token: 0x06000034 RID: 52 RVA: 0x00003B73 File Offset: 0x00002B73
		// (remove) Token: 0x06000035 RID: 53 RVA: 0x00003B8C File Offset: 0x00002B8C
		public event TypedMapObjectContainer.DestroyedEvent Destroyed;

		// Token: 0x06000036 RID: 54 RVA: 0x00003BA5 File Offset: 0x00002BA5
		public TypedMapObjectContainer(int _type, bool _allowTemporary)
		{
			this.type = _type;
			this.allowTemporary = _allowTemporary;
		}

		// Token: 0x06000037 RID: 55 RVA: 0x00003BD1 File Offset: 0x00002BD1
		public void Destroy()
		{
			this.mapObjects.Clear();
			if (this.Destroyed != null)
			{
				this.Destroyed(this);
			}
		}

		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000038 RID: 56 RVA: 0x00003BF2 File Offset: 0x00002BF2
		public Dictionary<int, IMapObject> MapObjects
		{
			get
			{
				return this.mapObjects;
			}
		}

		// Token: 0x06000039 RID: 57 RVA: 0x00003BFC File Offset: 0x00002BFC
		public void AddMapObject(IMapObject mapObject)
		{
			if (mapObject != null && (this.allowTemporary || !mapObject.Temporary) && mapObject.Type.Type == this.type)
			{
				this.mapObjects.Add(mapObject.ID, mapObject);
				if (this.MapObjectAdded != null)
				{
					this.MapObjectAdded(this, mapObject);
				}
			}
		}

		// Token: 0x0600003A RID: 58 RVA: 0x00003C5C File Offset: 0x00002C5C
		public void RemoveMapObject(IMapObject mapObject)
		{
			if (mapObject != null && (this.allowTemporary || !mapObject.Temporary) && mapObject.Type.Type == this.type)
			{
				this.mapObjects.Remove(mapObject.ID);
				if (this.MapObjectRemoved != null)
				{
					this.MapObjectRemoved(this, mapObject);
				}
			}
		}

		// Token: 0x0600003B RID: 59 RVA: 0x00003CBC File Offset: 0x00002CBC
		public bool Contains(IMapObject mapObject)
		{
			IMapObject _mapObject;
			return mapObject != null && (!this.mapObjects.TryGetValue(mapObject.ID, out _mapObject) || object.ReferenceEquals(_mapObject, mapObject));
		}

		// Token: 0x04000010 RID: 16
		private readonly int type = MapObjectFactory.Type.Unknown;

		// Token: 0x04000011 RID: 17
		private readonly bool allowTemporary;

		// Token: 0x04000012 RID: 18
		private readonly Dictionary<int, IMapObject> mapObjects = new Dictionary<int, IMapObject>();

		// Token: 0x0200000C RID: 12
		// (Invoke) Token: 0x0600003D RID: 61
		public delegate void MapObjectAddedEvent(TypedMapObjectContainer typedMapObjectContainer, IMapObject mapObject);

		// Token: 0x0200000D RID: 13
		// (Invoke) Token: 0x06000041 RID: 65
		public delegate void MapObjectRemovedEvent(TypedMapObjectContainer typedMapObjectContainer, IMapObject mapObject);

		// Token: 0x0200000E RID: 14
		// (Invoke) Token: 0x06000045 RID: 69
		public delegate void DestroyedEvent(TypedMapObjectContainer typedMapObjectContainer);
	}
}
