using System;
using MapEditor.Resources.Strings;
using Tools.Progress;

namespace MapEditor.Map.SaveLoad.DataSources
{
	// Token: 0x0200002A RID: 42
	internal class PreSceneDataSource : SaveLoad.IDataSource
	{
		// Token: 0x060002D0 RID: 720 RVA: 0x0001DC8E File Offset: 0x0001CC8E
		public int GetProgressSteps(bool forSave)
		{
			return 1;
		}

		// Token: 0x060002D1 RID: 721 RVA: 0x0001DC91 File Offset: 0x0001CC91
		public bool Save(MapEditorMap map, MainForm.Context context, IProgressContainer progressContainer)
		{
			if (progressContainer != null)
			{
				progressContainer.AddString(Strings.SAVING_PRESCENE);
			}
			context.EditorScene.PreSave();
			if (progressContainer != null)
			{
				progressContainer.Progress++;
			}
			return true;
		}

		// Token: 0x060002D2 RID: 722 RVA: 0x0001DCC0 File Offset: 0x0001CCC0
		public bool Load(MapEditorMap map, MainForm.Context context, IProgressContainer progressContainer, out bool somethingCreated)
		{
			somethingCreated = false;
			if (progressContainer != null)
			{
				progressContainer.AddString(Strings.LOADING_PRESCENE);
			}
			context.EditorScene.PreLoad();
			context.EditorScene.SetStatisticOffset((double)(map.Data.MinXMinYPatchCoords.X * Constants.PatchSize), (double)(map.Data.MinXMinYPatchCoords.Y * Constants.PatchSize));
			if (progressContainer != null)
			{
				progressContainer.Progress++;
			}
			return true;
		}
	}
}
