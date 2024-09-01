using System;
using System.Collections.Generic;
using Tools.MapObjects;

namespace MapEditor.Map.MapObjects
{
	// Token: 0x02000108 RID: 264
	public class StaticObjectPack : SerializableMapObjectPack
	{
		// Token: 0x17000265 RID: 613
		// (get) Token: 0x06000CFC RID: 3324 RVA: 0x0006E1D1 File Offset: 0x0006D1D1
		// (set) Token: 0x06000CFD RID: 3325 RVA: 0x0006E1D9 File Offset: 0x0006D1D9
		public List<MapObjectType> MapObjectTypes
		{
			get
			{
				return this.mapObjectTypes;
			}
			set
			{
				this.mapObjectTypes = value;
			}
		}

		// Token: 0x17000266 RID: 614
		// (get) Token: 0x06000CFE RID: 3326 RVA: 0x0006E1E2 File Offset: 0x0006D1E2
		// (set) Token: 0x06000CFF RID: 3327 RVA: 0x0006E1EA File Offset: 0x0006D1EA
		public bool AICollision
		{
			get
			{
				return this.aiCollision;
			}
			set
			{
				this.aiCollision = value;
			}
		}

		// Token: 0x06000D00 RID: 3328 RVA: 0x0006E1F4 File Offset: 0x0006D1F4
		public override void Pack(IMapObject mapObject)
		{
			base.Pack(mapObject);
			StaticObject staticObject = mapObject as StaticObject;
			if (staticObject != null)
			{
				this.mapObjectTypes.Clear();
				this.mapObjectTypes.Add(staticObject.Type);
				this.aiCollision = staticObject.AICollision;
			}
		}

		// Token: 0x06000D01 RID: 3329 RVA: 0x0006E23C File Offset: 0x0006D23C
		public override void Unpack(IMapObject mapObject)
		{
			base.Unpack(mapObject);
			StaticObject staticObject = mapObject as StaticObject;
			if (staticObject != null)
			{
				staticObject.AICollision = this.aiCollision;
			}
		}

		// Token: 0x04000A83 RID: 2691
		private List<MapObjectType> mapObjectTypes = new List<MapObjectType>();

		// Token: 0x04000A84 RID: 2692
		private bool aiCollision = true;
	}
}
