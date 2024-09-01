using System;
using System.Collections.Generic;

namespace MapEditor.Forms.CollectionBrowser.DataProviders
{
	// Token: 0x02000056 RID: 86
	public class CollectionBrowserParams : ICollectionBrowserParams
	{
		// Token: 0x1700009D RID: 157
		// (get) Token: 0x0600045C RID: 1116 RVA: 0x0002411B File Offset: 0x0002311B
		// (set) Token: 0x0600045D RID: 1117 RVA: 0x00024123 File Offset: 0x00023123
		public string FormLabel
		{
			get
			{
				return this.formLabel;
			}
			set
			{
				this.formLabel = value;
			}
		}

		// Token: 0x1700009E RID: 158
		// (get) Token: 0x0600045E RID: 1118 RVA: 0x0002412C File Offset: 0x0002312C
		// (set) Token: 0x0600045F RID: 1119 RVA: 0x00024134 File Offset: 0x00023134
		public string CollectionPanelLabel
		{
			get
			{
				return this.collectionPanelLabel;
			}
			set
			{
				this.collectionPanelLabel = value;
			}
		}

		// Token: 0x1700009F RID: 159
		// (get) Token: 0x06000460 RID: 1120 RVA: 0x0002413D File Offset: 0x0002313D
		// (set) Token: 0x06000461 RID: 1121 RVA: 0x00024145 File Offset: 0x00023145
		public string PropertyGridPanelLabel
		{
			get
			{
				return this.propertyGridPanelLabel;
			}
			set
			{
				this.propertyGridPanelLabel = value;
			}
		}

		// Token: 0x170000A0 RID: 160
		// (get) Token: 0x06000462 RID: 1122 RVA: 0x0002414E File Offset: 0x0002314E
		public List<string> TypeNames
		{
			get
			{
				return this.typeNames;
			}
		}

		// Token: 0x170000A1 RID: 161
		// (get) Token: 0x06000463 RID: 1123 RVA: 0x00024156 File Offset: 0x00023156
		public virtual int ItemCount
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x06000464 RID: 1124 RVA: 0x00024159 File Offset: 0x00023159
		public virtual int GetItemType(int index)
		{
			return -1;
		}

		// Token: 0x06000465 RID: 1125 RVA: 0x0002415C File Offset: 0x0002315C
		public virtual object GetItem(int index)
		{
			return null;
		}

		// Token: 0x06000466 RID: 1126 RVA: 0x0002415F File Offset: 0x0002315F
		public virtual void InsertItem(int index, int type)
		{
		}

		// Token: 0x06000467 RID: 1127 RVA: 0x00024161 File Offset: 0x00023161
		public virtual void RemoveItem(int index)
		{
		}

		// Token: 0x06000468 RID: 1128 RVA: 0x00024163 File Offset: 0x00023163
		public virtual void SwapItems(int left, int right)
		{
		}

		// Token: 0x0400030C RID: 780
		private string formLabel = string.Empty;

		// Token: 0x0400030D RID: 781
		private string collectionPanelLabel = string.Empty;

		// Token: 0x0400030E RID: 782
		private string propertyGridPanelLabel = string.Empty;

		// Token: 0x0400030F RID: 783
		private readonly List<string> typeNames = new List<string>();
	}
}
