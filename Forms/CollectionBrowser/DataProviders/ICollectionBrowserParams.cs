using System;
using System.Collections.Generic;

namespace MapEditor.Forms.CollectionBrowser.DataProviders
{
	// Token: 0x02000055 RID: 85
	public interface ICollectionBrowserParams
	{
		// Token: 0x17000098 RID: 152
		// (get) Token: 0x0600044F RID: 1103
		// (set) Token: 0x06000450 RID: 1104
		string FormLabel { get; set; }

		// Token: 0x17000099 RID: 153
		// (get) Token: 0x06000451 RID: 1105
		// (set) Token: 0x06000452 RID: 1106
		string CollectionPanelLabel { get; set; }

		// Token: 0x1700009A RID: 154
		// (get) Token: 0x06000453 RID: 1107
		// (set) Token: 0x06000454 RID: 1108
		string PropertyGridPanelLabel { get; set; }

		// Token: 0x1700009B RID: 155
		// (get) Token: 0x06000455 RID: 1109
		List<string> TypeNames { get; }

		// Token: 0x1700009C RID: 156
		// (get) Token: 0x06000456 RID: 1110
		int ItemCount { get; }

		// Token: 0x06000457 RID: 1111
		int GetItemType(int index);

		// Token: 0x06000458 RID: 1112
		object GetItem(int index);

		// Token: 0x06000459 RID: 1113
		void InsertItem(int index, int type);

		// Token: 0x0600045A RID: 1114
		void RemoveItem(int index);

		// Token: 0x0600045B RID: 1115
		void SwapItems(int left, int right);
	}
}
