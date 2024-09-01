using System;
using Tools.MapObjects;

namespace MapEditor.Map.MapObjects
{
	// Token: 0x0200027C RID: 636
	public class StartPointPack : SerializableMapObjectPack
	{
		// Token: 0x170006F7 RID: 1783
		// (get) Token: 0x06001E3C RID: 7740 RVA: 0x000C5833 File Offset: 0x000C4833
		// (set) Token: 0x06001E3D RID: 7741 RVA: 0x000C583B File Offset: 0x000C483B
		public string Character
		{
			get
			{
				return this.character;
			}
			set
			{
				this.character = value;
			}
		}

		// Token: 0x06001E3E RID: 7742 RVA: 0x000C5844 File Offset: 0x000C4844
		public override void Pack(IMapObject mapObject)
		{
			base.Pack(mapObject);
			StartPoint startPoint = mapObject as StartPoint;
			if (startPoint != null)
			{
				this.character = startPoint.Character;
			}
		}

		// Token: 0x06001E3F RID: 7743 RVA: 0x000C5870 File Offset: 0x000C4870
		public override void Unpack(IMapObject mapObject)
		{
			base.Unpack(mapObject);
			StartPoint startPoint = mapObject as StartPoint;
			if (startPoint != null)
			{
				startPoint.Character = this.character;
			}
		}

		// Token: 0x040012FB RID: 4859
		private string character = string.Empty;
	}
}
