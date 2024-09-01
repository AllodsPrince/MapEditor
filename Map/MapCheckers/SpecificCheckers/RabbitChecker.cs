using System;
using Tools.Progress;

namespace MapEditor.Map.MapCheckers.SpecificCheckers
{
	// Token: 0x020000D6 RID: 214
	internal class RabbitChecker : MapChecker
	{
		// Token: 0x06000AFE RID: 2814 RVA: 0x00059DFD File Offset: 0x00058DFD
		public RabbitChecker(EditorScene editorScene)
		{
			this.EditorScene = editorScene;
		}

		// Token: 0x06000AFF RID: 2815 RVA: 0x00059E0C File Offset: 0x00058E0C
		public override void Check(MapEditorMap map, IProgressContainer progressContainer)
		{
		}

		// Token: 0x04000851 RID: 2129
		private EditorScene EditorScene;
	}
}
