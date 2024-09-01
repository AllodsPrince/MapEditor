using System;
using System.Collections.Generic;

namespace MapEditor.Forms.TypedObjectsBrowser.DataProviders
{
	// Token: 0x020001B0 RID: 432
	public class TypedObjectParams
	{
		// Token: 0x17000412 RID: 1042
		// (get) Token: 0x060014AF RID: 5295 RVA: 0x00094EA5 File Offset: 0x00093EA5
		// (set) Token: 0x060014B0 RID: 5296 RVA: 0x00094EAD File Offset: 0x00093EAD
		public string Name
		{
			get
			{
				return this.name;
			}
			set
			{
				this.name = value;
			}
		}

		// Token: 0x17000413 RID: 1043
		// (get) Token: 0x060014B1 RID: 5297 RVA: 0x00094EB6 File Offset: 0x00093EB6
		public List<string> TypeNames
		{
			get
			{
				return this.typeNames;
			}
		}

		// Token: 0x17000414 RID: 1044
		// (get) Token: 0x060014B2 RID: 5298 RVA: 0x00094EBE File Offset: 0x00093EBE
		public int TypeCount
		{
			get
			{
				return this.typeNames.Count;
			}
		}

		// Token: 0x17000415 RID: 1045
		// (get) Token: 0x060014B3 RID: 5299 RVA: 0x00094ECB File Offset: 0x00093ECB
		// (set) Token: 0x060014B4 RID: 5300 RVA: 0x00094ED3 File Offset: 0x00093ED3
		public int SelectedTypeIndex
		{
			get
			{
				return this.selectedTypeIndex;
			}
			set
			{
				this.selectedTypeIndex = value;
			}
		}

		// Token: 0x17000416 RID: 1046
		// (get) Token: 0x060014B5 RID: 5301 RVA: 0x00094EDC File Offset: 0x00093EDC
		public List<string> ObjectTypeNames
		{
			get
			{
				return this.objectTypeNames;
			}
		}

		// Token: 0x17000417 RID: 1047
		// (get) Token: 0x060014B6 RID: 5302 RVA: 0x00094EE4 File Offset: 0x00093EE4
		public int ObjectTypeCount
		{
			get
			{
				return this.objectTypeNames.Count;
			}
		}

		// Token: 0x17000418 RID: 1048
		// (get) Token: 0x060014B7 RID: 5303 RVA: 0x00094EF1 File Offset: 0x00093EF1
		// (set) Token: 0x060014B8 RID: 5304 RVA: 0x00094EF9 File Offset: 0x00093EF9
		public int SelectedObjectTypeIndex
		{
			get
			{
				return this.selectedObjectTypeIndex;
			}
			set
			{
				this.selectedObjectTypeIndex = value;
			}
		}

		// Token: 0x17000419 RID: 1049
		// (get) Token: 0x060014B9 RID: 5305 RVA: 0x00094F02 File Offset: 0x00093F02
		public List<DBItemSource> DBItemSources
		{
			get
			{
				return this.dbItemSources;
			}
		}

		// Token: 0x04000E7E RID: 3710
		private string name = string.Empty;

		// Token: 0x04000E7F RID: 3711
		private readonly List<string> typeNames = new List<string>();

		// Token: 0x04000E80 RID: 3712
		private int selectedTypeIndex = -1;

		// Token: 0x04000E81 RID: 3713
		private readonly List<string> objectTypeNames = new List<string>();

		// Token: 0x04000E82 RID: 3714
		private int selectedObjectTypeIndex = -1;

		// Token: 0x04000E83 RID: 3715
		private readonly List<DBItemSource> dbItemSources = new List<DBItemSource>();
	}
}
