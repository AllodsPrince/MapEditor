using System;
using MapEditor.Resources.Strings;
using Tools.MapObjects;
using Tools.Progress;

namespace MapEditor.Map.SaveLoad.DataSources
{
	// Token: 0x02000247 RID: 583
	internal class AstralSkyDataSource : SaveLoad.IDataSource
	{
		// Token: 0x06001BCA RID: 7114 RVA: 0x000B55B1 File Offset: 0x000B45B1
		public int GetProgressSteps(bool forSave)
		{
			return 1;
		}

		// Token: 0x06001BCB RID: 7115 RVA: 0x000B55B4 File Offset: 0x000B45B4
		public bool Save(MapEditorMap map, MainForm.Context context, IProgressContainer progressContainer)
		{
			if (progressContainer != null)
			{
				progressContainer.AddString(Strings.SAVING_ASTRAL_SKY);
			}
			if (progressContainer != null)
			{
				progressContainer.Progress++;
			}
			return true;
		}

		// Token: 0x06001BCC RID: 7116 RVA: 0x000B55D8 File Offset: 0x000B45D8
		public bool Load(MapEditorMap map, MainForm.Context context, IProgressContainer progressContainer, out bool somethingCreated)
		{
			somethingCreated = false;
			if (progressContainer != null)
			{
				progressContainer.AddString(Strings.LOADING_ASTRAL_SKY);
			}
			double additionalRatio = map.Data.ScaleRatio;
			Position additionalPosition = new Position((double)(map.Data.MapSize.X * Constants.PatchSize) / 2.0, (double)(map.Data.MapSize.Y * Constants.PatchSize) / 2.0, 0.0);
			string mapResourceName = map.Data.MapResourceName;
			context.EditorScene.LoadAstral(mapResourceName, ref additionalPosition, additionalRatio);
			if (progressContainer != null)
			{
				progressContainer.Progress++;
			}
			return true;
		}
	}
}
