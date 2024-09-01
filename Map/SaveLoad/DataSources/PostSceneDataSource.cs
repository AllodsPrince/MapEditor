using System;
using MapEditor.Resources.Strings;
using Tools.Progress;

namespace MapEditor.Map.SaveLoad.DataSources
{
	// Token: 0x0200002B RID: 43
	internal class PostSceneDataSource : SaveLoad.IDataSource
	{
		// Token: 0x060002D4 RID: 724 RVA: 0x0001DD43 File Offset: 0x0001CD43
		public int GetProgressSteps(bool forSave)
		{
			return 1;
		}

		// Token: 0x060002D5 RID: 725 RVA: 0x0001DD46 File Offset: 0x0001CD46
		public bool Save(MapEditorMap map, MainForm.Context context, IProgressContainer progressContainer)
		{
			if (progressContainer != null)
			{
				progressContainer.AddString(Strings.SAVING_POSTSCENE);
			}
			context.EditorScene.PostSave();
			if (progressContainer != null)
			{
				progressContainer.Progress++;
			}
			return true;
		}

		// Token: 0x060002D6 RID: 726 RVA: 0x0001DD73 File Offset: 0x0001CD73
		public bool Load(MapEditorMap map, MainForm.Context context, IProgressContainer progressContainer, out bool somethingCreated)
		{
			somethingCreated = false;
			if (progressContainer != null)
			{
				progressContainer.AddString(Strings.LOADING_POSTSCENE);
			}
			context.EditorScene.PostLoad();
			if (progressContainer != null)
			{
				progressContainer.Progress++;
			}
			return true;
		}
	}
}
