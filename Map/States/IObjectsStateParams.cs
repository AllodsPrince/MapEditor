using System;
using Tools.MapObjects;

namespace MapEditor.Map.States
{
	// Token: 0x02000012 RID: 18
	public interface IObjectsStateParams : IMapObjectRandomizer
	{
		// Token: 0x14000007 RID: 7
		// (add) Token: 0x0600005A RID: 90
		// (remove) Token: 0x0600005B RID: 91
		event ObjectsStateParamsFieldChangedEvent AddRowChanged;

		// Token: 0x14000008 RID: 8
		// (add) Token: 0x0600005C RID: 92
		// (remove) Token: 0x0600005D RID: 93
		event ObjectsStateParamsFieldChangedEvent OneSidedRowChanged;

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x0600005E RID: 94
		// (set) Token: 0x0600005F RID: 95
		bool EnableDoubleClickProperties { get; set; }

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x06000060 RID: 96
		bool RandomizeName { get; }

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x06000061 RID: 97
		bool AddRow { get; }

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x06000062 RID: 98
		bool OneSidedRow { get; }

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x06000063 RID: 99
		string Group { get; }

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x06000064 RID: 100
		ItemList.IItemSource NameSource { get; }

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x06000065 RID: 101
		ItemList ItemList { get; }
	}
}
