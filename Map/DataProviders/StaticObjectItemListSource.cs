using System;
using MapEditor.Map.MapObjects;

namespace MapEditor.Map.DataProviders
{
	// Token: 0x0200028F RID: 655
	public class StaticObjectItemListSource : DBItemSource
	{
		// Token: 0x06001EC8 RID: 7880 RVA: 0x000C69D8 File Offset: 0x000C59D8
		public StaticObjectItemListSource() : base(string.Empty, StaticObject.DBType, false, false)
		{
		}
	}
}
