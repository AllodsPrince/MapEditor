using System;
using Tools.LinkContainer;

namespace MapEditor.Map.MapObjects
{
	// Token: 0x020001F2 RID: 498
	public class LinkDataPack
	{
		// Token: 0x170005F9 RID: 1529
		// (get) Token: 0x060018DD RID: 6365 RVA: 0x000A54DE File Offset: 0x000A44DE
		// (set) Token: 0x060018DE RID: 6366 RVA: 0x000A54E6 File Offset: 0x000A44E6
		public PatrolLinkData PatrolLinkData
		{
			get
			{
				return this.patrolLinkData;
			}
			set
			{
				this.patrolLinkData = value;
			}
		}

		// Token: 0x170005FA RID: 1530
		// (get) Token: 0x060018DF RID: 6367 RVA: 0x000A54EF File Offset: 0x000A44EF
		// (set) Token: 0x060018E0 RID: 6368 RVA: 0x000A54F7 File Offset: 0x000A44F7
		public int LeftIndex
		{
			get
			{
				return this.leftIndex;
			}
			set
			{
				this.leftIndex = value;
			}
		}

		// Token: 0x170005FB RID: 1531
		// (get) Token: 0x060018E1 RID: 6369 RVA: 0x000A5500 File Offset: 0x000A4500
		// (set) Token: 0x060018E2 RID: 6370 RVA: 0x000A5508 File Offset: 0x000A4508
		public int RightIndex
		{
			get
			{
				return this.rightIndex;
			}
			set
			{
				this.rightIndex = value;
			}
		}

		// Token: 0x060018E3 RID: 6371 RVA: 0x000A5511 File Offset: 0x000A4511
		public void Pack(ILinkData linkData)
		{
			if (linkData != null && linkData is PatrolLinkData)
			{
				this.patrolLinkData = (linkData as PatrolLinkData);
				return;
			}
			this.patrolLinkData = null;
		}

		// Token: 0x060018E4 RID: 6372 RVA: 0x000A5532 File Offset: 0x000A4532
		public ILinkData Unpack()
		{
			return this.patrolLinkData;
		}

		// Token: 0x0400101B RID: 4123
		private int leftIndex = -1;

		// Token: 0x0400101C RID: 4124
		private int rightIndex = -1;

		// Token: 0x0400101D RID: 4125
		private PatrolLinkData patrolLinkData;
	}
}
