using System;
using System.Collections.Generic;
using Tools.Landscape.LandscapeToolParams;

namespace MapEditor.Map.DataProviders
{
	// Token: 0x0200020C RID: 524
	internal class RoadItemSetItemListSource : ItemList.IItemSource
	{
		// Token: 0x060019A5 RID: 6565 RVA: 0x000A74EC File Offset: 0x000A64EC
		private void FillItems()
		{
			this.items.Clear();
			if (this.roadITemSet != null)
			{
				for (int index = 0; index < this.roadITemSet.Items.Items.Count; index++)
				{
					this.items.Add(this.roadITemSet.Items.Items[index].Item);
				}
			}
		}

		// Token: 0x1700062C RID: 1580
		// (get) Token: 0x060019A6 RID: 6566 RVA: 0x000A7552 File Offset: 0x000A6552
		// (set) Token: 0x060019A7 RID: 6567 RVA: 0x000A755A File Offset: 0x000A655A
		public RoadItemSet RoadItemSet
		{
			get
			{
				return this.roadITemSet;
			}
			set
			{
				this.roadITemSet = value;
				this.outdated = true;
			}
		}

		// Token: 0x1700062D RID: 1581
		// (get) Token: 0x060019A8 RID: 6568 RVA: 0x000A756A File Offset: 0x000A656A
		public IEnumerable<string> Items
		{
			get
			{
				if (this.outdated)
				{
					this.FillItems();
				}
				return this.items;
			}
		}

		// Token: 0x04001059 RID: 4185
		private readonly List<string> items = new List<string>();

		// Token: 0x0400105A RID: 4186
		private bool outdated = true;

		// Token: 0x0400105B RID: 4187
		private RoadItemSet roadITemSet;
	}
}
