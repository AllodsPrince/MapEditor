using System;
using MapEditor.Resources.Strings;
using Tools.Progress;

namespace MapEditor.Map.MapCheckers.SpecificCheckers
{
	// Token: 0x020002A3 RID: 675
	public class EditorSceneChecker : MapChecker
	{
		// Token: 0x06001F77 RID: 8055 RVA: 0x000C9A80 File Offset: 0x000C8A80
		public EditorSceneChecker(EditorScene _editorScene)
		{
			this.editorScene = _editorScene;
			base.Name = Strings.EDITOR_SCENE_CHECKER_NAME;
			base.ShortDescription = string.Format("{0}", EditorSceneChecker.yellowObjectsForPatch);
			base.LongDescription = string.Format(Strings.EDITOR_SCENE_CHECKER_LONG_DESCRIPTION, EditorSceneChecker.yellowObjectsForPatch, EditorSceneChecker.yellowVerticesForPatch, EditorSceneChecker.yellowMaterialsForPatch);
		}

		// Token: 0x06001F78 RID: 8056 RVA: 0x000C9AF0 File Offset: 0x000C8AF0
		public override void Check(MapEditorMap map, IProgressContainer progressContainer)
		{
			if (progressContainer != null)
			{
				progressContainer.AddString(Strings.RUN_EDITOR_SCENE_CHECKER);
			}
			int totalCount = 0;
			int maxCount = 0;
			int[,] array = new int[4, 4];
			int[,] objectsCount = array;
			int[,] array2 = new int[4, 4];
			int[,] verticesCount = array2;
			int[,] array3 = new int[4, 4];
			int[,] materialsCount = array3;
			for (int x = 0; x < 4; x++)
			{
				for (int y = 0; y < 4; y++)
				{
					int objectCount = this.editorScene.GetObjectStatistic(x, y);
					if (objectCount > maxCount)
					{
						maxCount = objectCount;
					}
					totalCount += objectCount;
					objectsCount[x, y] = objectCount;
					verticesCount[x, y] = this.editorScene.GetVertexStatistic(x, y);
					materialsCount[x, y] = this.editorScene.GetMaterialStatistic(x, y);
				}
			}
			base.Status = MapCheckerStatus.Green;
			for (int x2 = 0; x2 < 4; x2++)
			{
				for (int y2 = 0; y2 < 4; y2++)
				{
					if (objectsCount[x2, y2] > EditorSceneChecker.redObjectsForPatch || verticesCount[x2, y2] > EditorSceneChecker.redVerticesForPatch || materialsCount[x2, y2] > EditorSceneChecker.redMaterialsForPatch)
					{
						base.Status = MapCheckerStatus.Red;
						break;
					}
					if ((base.Status == MapCheckerStatus.Green && objectsCount[x2, y2] > EditorSceneChecker.yellowObjectsForPatch) || verticesCount[x2, y2] > EditorSceneChecker.yellowVerticesForPatch || materialsCount[x2, y2] > EditorSceneChecker.yellowMaterialsForPatch)
					{
						base.Status = MapCheckerStatus.Yellow;
					}
				}
				if (base.Status == MapCheckerStatus.Red)
				{
					break;
				}
			}
			if (base.Status == MapCheckerStatus.Red)
			{
				base.ShortInfo = Strings.CHECKER_EXCEED_RED;
			}
			else if (base.Status == MapCheckerStatus.Yellow)
			{
				base.ShortInfo = Strings.CHECKER_EXCEED_YELLOW;
			}
			else
			{
				base.ShortInfo = Strings.CHECKER_OK;
			}
			base.ShortResult = string.Format("{0}", maxCount);
			base.LongResult = string.Format(Strings.EDITOR_SCENE_CHECKER_LONG_RESULT, totalCount);
			base.LongInfoText = Strings.EDITOR_SCENE_CHECKER_OBJECTS;
			base.LongInfoText += "\r\n";
			base.LongInfoText += MapCheckerHelper.FormatTable(objectsCount, EditorSceneChecker.yellowObjectsForPatch, EditorSceneChecker.redObjectsForPatch);
			base.LongInfoText += "\r\n\r\n";
			base.LongInfoText += Strings.EDITOR_SCENE_CHECKER_VERTICES;
			base.LongInfoText += "\r\n";
			base.LongInfoText += MapCheckerHelper.FormatTable(verticesCount, EditorSceneChecker.yellowVerticesForPatch, EditorSceneChecker.redVerticesForPatch);
			base.LongInfoText += "\r\n\r\n";
			base.LongInfoText += Strings.EDITOR_SCENE_CHECKER_MATERIALS;
			base.LongInfoText += "\r\n";
			base.LongInfoText += MapCheckerHelper.FormatTable(materialsCount, EditorSceneChecker.yellowMaterialsForPatch, EditorSceneChecker.redMaterialsForPatch);
			if (progressContainer != null)
			{
				progressContainer.Progress++;
			}
		}

		// Token: 0x04001368 RID: 4968
		private readonly EditorScene editorScene;

		// Token: 0x04001369 RID: 4969
		private static readonly int yellowObjectsForPatch = 300;

		// Token: 0x0400136A RID: 4970
		private static readonly int redObjectsForPatch = 500;

		// Token: 0x0400136B RID: 4971
		private static readonly int yellowVerticesForPatch = 150000;

		// Token: 0x0400136C RID: 4972
		private static readonly int redVerticesForPatch = 200000;

		// Token: 0x0400136D RID: 4973
		private static readonly int yellowMaterialsForPatch = 600;

		// Token: 0x0400136E RID: 4974
		private static readonly int redMaterialsForPatch = 1000;
	}
}
