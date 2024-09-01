using System;
using System.Collections.Generic;
using System.IO;
using MapEditor.Resources.Strings;
using Tools.Progress;
using Win32;

namespace MapEditor.Map.SaveLoad
{
	// Token: 0x02000003 RID: 3
	public class SaveLoad
	{
		// Token: 0x06000006 RID: 6 RVA: 0x000020E4 File Offset: 0x000010E4
		private void GetDisabledDataSources(out List<string> disabledDataSources)
		{
			disabledDataSources = null;
			if (!string.IsNullOrEmpty(this.saveLoadConfigFileName))
			{
				try
				{
					StreamReader streamReader = new StreamReader(this.saveLoadConfigFileName);
					for (string disabledDataSource = streamReader.ReadLine(); disabledDataSource != null; disabledDataSource = streamReader.ReadLine())
					{
						if (!string.IsNullOrEmpty(disabledDataSource))
						{
							if (disabledDataSources == null)
							{
								disabledDataSources = new List<string>();
							}
							disabledDataSources.Add(disabledDataSource);
						}
					}
				}
				catch (ArgumentException e)
				{
					Console.WriteLine(e);
				}
				catch (DirectoryNotFoundException e2)
				{
					Console.WriteLine(e2);
				}
				catch (FileNotFoundException e3)
				{
					Console.WriteLine(e3);
				}
				catch (IOException e4)
				{
					Console.WriteLine(e4);
				}
			}
		}

		// Token: 0x06000007 RID: 7 RVA: 0x0000219C File Offset: 0x0000119C
		private static bool CanUseDataSource(ICollection<string> disabledDataSources, string dataSourceType)
		{
			if (disabledDataSources != null)
			{
				string shortDataSourceType = dataSourceType.Substring(dataSourceType.LastIndexOf('.') + 1);
				if (disabledDataSources.Contains(shortDataSourceType))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06000008 RID: 8 RVA: 0x000021CC File Offset: 0x000011CC
		private int GetProgressCount(ICollection<string> disabledDataSources, bool forSave)
		{
			int progressCount = 0;
			foreach (SaveLoad.IDataSource dataSource in this.dataSources)
			{
				if (SaveLoad.CanUseDataSource(disabledDataSources, dataSource.GetType().ToString()))
				{
					progressCount += dataSource.GetProgressSteps(forSave);
				}
			}
			return progressCount;
		}

		// Token: 0x06000009 RID: 9 RVA: 0x00002238 File Offset: 0x00001238
		public SaveLoad(MapEditorMap _map, MainForm.Context _context, string _saveLoadConfigFileName)
		{
			this.saveLoadConfigFileName = _saveLoadConfigFileName;
			this.map = _map;
			this.context = _context;
		}

		// Token: 0x0600000A RID: 10 RVA: 0x00002276 File Offset: 0x00001276
		public void AddDataSource(SaveLoad.IDataSource dataSource)
		{
			this.dataSources.Add(dataSource);
		}

		// Token: 0x0600000B RID: 11 RVA: 0x00002284 File Offset: 0x00001284
		public void Clear()
		{
			this.dataSources.Clear();
		}

		// Token: 0x0600000C RID: 12 RVA: 0x00002294 File Offset: 0x00001294
		public bool Save()
		{
			List<string> disabledDataSources;
			this.GetDisabledDataSources(out disabledDataSources);
			this.progressForm.Create(Strings.MAP_EDITOR_SAVING);
			this.progressForm.ProgressCount = this.GetProgressCount(disabledDataSources, true);
			this.progressForm.Progress = 0;
			int time = Kernel.GetTickCount();
			int timeTotal = time;
			bool result = true;
			foreach (SaveLoad.IDataSource dataSource in this.dataSources)
			{
				bool useDataSource = SaveLoad.CanUseDataSource(disabledDataSources, dataSource.GetType().ToString());
				if (useDataSource && !dataSource.Save(this.map, this.context, this.progressForm))
				{
					result = false;
				}
				int timePassed = time;
				time = Kernel.GetTickCount();
				timePassed = time - timePassed;
				if (useDataSource)
				{
					Console.WriteLine("Save complete: {0} {1} ms", dataSource.GetType(), timePassed);
					this.progressForm.AddString(string.Format(Strings.SAVING_SAVER_FINISHED, timePassed));
				}
			}
			timeTotal = Kernel.GetTickCount() - timeTotal;
			Console.WriteLine("Map saved: {0} {1} ms", "total", timeTotal);
			this.progressForm.Delete();
			return result;
		}

		// Token: 0x0600000D RID: 13 RVA: 0x000023D0 File Offset: 0x000013D0
		public bool Load(out bool somethingCreated)
		{
			List<string> disabledDataSources;
			this.GetDisabledDataSources(out disabledDataSources);
			this.progressForm.Create(Strings.MAP_EDITOR_LOADING);
			this.progressForm.ProgressCount = this.GetProgressCount(disabledDataSources, false);
			this.progressForm.Progress = 0;
			int time = Kernel.GetTickCount();
			int timeTotal = time;
			somethingCreated = false;
			bool result = true;
			foreach (SaveLoad.IDataSource dataSource in this.dataSources)
			{
				bool _somethingCreated = false;
				bool useDataSource = SaveLoad.CanUseDataSource(disabledDataSources, dataSource.GetType().ToString());
				if (useDataSource && !dataSource.Load(this.map, this.context, this.progressForm, out _somethingCreated))
				{
					result = false;
				}
				if (_somethingCreated)
				{
					somethingCreated = true;
				}
				int timePassed = time;
				time = Kernel.GetTickCount();
				timePassed = time - timePassed;
				if (useDataSource)
				{
					Console.WriteLine("Load complete: {0} {1} ms", dataSource.GetType(), timePassed);
					this.progressForm.AddString(string.Format(Strings.LOADING_LOADER_FINISHED, timePassed));
				}
			}
			timeTotal = Kernel.GetTickCount() - timeTotal;
			Console.WriteLine("Map loaded: {0} {1} ms", "total", timeTotal);
			this.progressForm.Delete();
			return result;
		}

		// Token: 0x04000002 RID: 2
		private readonly ProgressForm progressForm = new ProgressForm();

		// Token: 0x04000003 RID: 3
		private readonly MapEditorMap map;

		// Token: 0x04000004 RID: 4
		private readonly MainForm.Context context;

		// Token: 0x04000005 RID: 5
		private readonly List<SaveLoad.IDataSource> dataSources = new List<SaveLoad.IDataSource>();

		// Token: 0x04000006 RID: 6
		private readonly string saveLoadConfigFileName = string.Empty;

		// Token: 0x02000004 RID: 4
		public interface IDataSource
		{
			// Token: 0x0600000E RID: 14
			int GetProgressSteps(bool forSave);

			// Token: 0x0600000F RID: 15
			bool Save(MapEditorMap map, MainForm.Context context, IProgressContainer progressContainer);

			// Token: 0x06000010 RID: 16
			bool Load(MapEditorMap map, MainForm.Context context, IProgressContainer progressContainer, out bool somethingCreated);
		}
	}
}
