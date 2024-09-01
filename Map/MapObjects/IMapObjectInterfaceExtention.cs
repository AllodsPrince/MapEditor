using System;
using System.Drawing;

namespace MapEditor.Map.MapObjects
{
	// Token: 0x0200003B RID: 59
	public interface IMapObjectInterfaceExtention
	{
		// Token: 0x06000374 RID: 884
		Color GetInterfaceColor();

		// Token: 0x06000375 RID: 885
		string GetInterfaceSingleObjectTypeName();

		// Token: 0x06000376 RID: 886
		string GetInterfaceSeveralObjectsTypeName();

		// Token: 0x06000377 RID: 887
		bool ContainsText(string text, bool ignoreCase);

		// Token: 0x06000378 RID: 888
		string GetStatsForDBBrowse();

		// Token: 0x06000379 RID: 889
		string GetSpecialStatsForDBBrowse();
	}
}
