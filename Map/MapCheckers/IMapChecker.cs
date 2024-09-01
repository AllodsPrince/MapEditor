using System;
using Tools.Progress;

namespace MapEditor.Map.MapCheckers
{
	// Token: 0x02000041 RID: 65
	public interface IMapChecker
	{
		// Token: 0x17000082 RID: 130
		// (get) Token: 0x060003AF RID: 943
		string Name { get; }

		// Token: 0x17000083 RID: 131
		// (get) Token: 0x060003B0 RID: 944
		MapCheckerStatus Status { get; }

		// Token: 0x17000084 RID: 132
		// (get) Token: 0x060003B1 RID: 945
		string ShortDescription { get; }

		// Token: 0x17000085 RID: 133
		// (get) Token: 0x060003B2 RID: 946
		string ShortResult { get; }

		// Token: 0x17000086 RID: 134
		// (get) Token: 0x060003B3 RID: 947
		string ShortInfo { get; }

		// Token: 0x17000087 RID: 135
		// (get) Token: 0x060003B4 RID: 948
		string LongDescription { get; }

		// Token: 0x17000088 RID: 136
		// (get) Token: 0x060003B5 RID: 949
		string LongResult { get; }

		// Token: 0x17000089 RID: 137
		// (get) Token: 0x060003B6 RID: 950
		LongInfoViewNode LongInfoView { get; }

		// Token: 0x1700008A RID: 138
		// (get) Token: 0x060003B7 RID: 951
		string LongInfoText { get; }

		// Token: 0x060003B8 RID: 952
		int GetCheckProgressSteps();

		// Token: 0x060003B9 RID: 953
		void Check(MapEditorMap map, IProgressContainer progressContainer);

		// Token: 0x060003BA RID: 954
		int GetFixProgressSteps();

		// Token: 0x060003BB RID: 955
		void Fix(MapEditorMap map, IProgressContainer progressContainer);
	}
}
