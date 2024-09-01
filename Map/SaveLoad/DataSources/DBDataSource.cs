using System;
using Db;
using MapEditor.Resources.Strings;
using Tools.Progress;

namespace MapEditor.Map.SaveLoad.DataSources
{
	// Token: 0x020002A2 RID: 674
	internal class DBDataSource : SaveLoad.IDataSource
	{
		// Token: 0x06001F73 RID: 8051 RVA: 0x000C99FF File Offset: 0x000C89FF
		public int GetProgressSteps(bool forSave)
		{
			return 1;
		}

		// Token: 0x06001F74 RID: 8052 RVA: 0x000C9A04 File Offset: 0x000C8A04
		public bool Save(MapEditorMap map, MainForm.Context context, IProgressContainer progressContainer)
		{
			if (progressContainer != null)
			{
				progressContainer.AddString(Strings.SAVING_DB);
			}
			if (IDatabase.GetMainDatabase() != null)
			{
				IDatabase.GetMainDatabase().SaveChangesAndUpdateReport(EditorEnvironment.DataCommonEditorFolder + "SaveReports/");
			}
			if (progressContainer != null)
			{
				progressContainer.Progress++;
			}
			return true;
		}

		// Token: 0x06001F75 RID: 8053 RVA: 0x000C9A51 File Offset: 0x000C8A51
		public bool Load(MapEditorMap map, MainForm.Context context, IProgressContainer progressContainer, out bool somethingCreated)
		{
			if (progressContainer != null)
			{
				progressContainer.AddString(Strings.LOADING_DB);
			}
			somethingCreated = false;
			if (progressContainer != null)
			{
				progressContainer.Progress++;
			}
			return true;
		}
	}
}
