using System;
using System.IO;
using MapEditor.Resources.Strings;
using Tools.Geometry;
using Tools.Progress;

namespace MapEditor.Map.SaveLoad.DataSources
{
	// Token: 0x02000028 RID: 40
	internal class SaveBackupDataSource : SaveLoad.IDataSource
	{
		// Token: 0x060002C6 RID: 710 RVA: 0x0001DA54 File Offset: 0x0001CA54
		private static bool ApplyToAllPatches(MapEditorMap map, MainForm.Context context, string backupFolder, SaveBackupDataSource.PatchMethod patchMethod, IProgressContainer progressContainer)
		{
			bool result = true;
			for (int x = 0; x < map.Data.MapSize.X; x++)
			{
				for (int y = 0; y < map.Data.MapSize.Y; y++)
				{
					if (!patchMethod(map, context, new Point(map.Data.MinXMinYPatchCoords.X + x, map.Data.MinXMinYPatchCoords.Y + y), backupFolder))
					{
						result = false;
					}
					if (progressContainer != null)
					{
						progressContainer.Progress++;
					}
				}
			}
			return result;
		}

		// Token: 0x060002C7 RID: 711 RVA: 0x0001DAF8 File Offset: 0x0001CAF8
		private static bool SaveMapRegion(MapEditorMap map, MainForm.Context context, Point patchIndex, string backupFolder)
		{
			string patchFolder = EditorEnvironment.DataFolder + Constants.PatchFolder(map.Data.ContinentName, patchIndex);
			string backupPatchFolder = backupFolder + Constants.PatchFolder(map.Data.ContinentName, patchIndex);
			if (Directory.Exists(patchFolder))
			{
				Directory.CreateDirectory(backupPatchFolder);
				string[] files = Directory.GetFiles(patchFolder, "*", SearchOption.AllDirectories);
				foreach (string file in files)
				{
					try
					{
						FileAttributes fileAttributes = File.GetAttributes(file);
						if (fileAttributes == FileAttributes.Normal || fileAttributes == FileAttributes.Archive)
						{
							string backupFile = file.Substring(patchFolder.Length);
							backupFile = backupPatchFolder + backupFile;
							File.Copy(file, backupFile, true);
						}
					}
					catch (Exception e)
					{
						Console.WriteLine(e);
					}
				}
			}
			return true;
		}

		// Token: 0x060002C8 RID: 712 RVA: 0x0001DBC8 File Offset: 0x0001CBC8
		public SaveBackupDataSource(MapEditorMap map)
		{
			this.mapSize = map.Data.MapSize;
		}

		// Token: 0x060002C9 RID: 713 RVA: 0x0001DBE4 File Offset: 0x0001CBE4
		public int GetProgressSteps(bool forSave)
		{
			if (forSave)
			{
				return this.mapSize.X * this.mapSize.Y;
			}
			return 1;
		}

		// Token: 0x060002CA RID: 714 RVA: 0x0001DC14 File Offset: 0x0001CC14
		public bool Save(MapEditorMap map, MainForm.Context context, IProgressContainer progressContainer)
		{
			if (progressContainer != null)
			{
				progressContainer.AddString(Strings.SAVING_BACKUP);
			}
			SaveBackupData saveBackupData = new SaveBackupData();
			string backupFolder;
			string oldBackupFolder;
			saveBackupData.GetBackupFolders(out backupFolder, out oldBackupFolder);
			if (SaveBackupDataSource.ApplyToAllPatches(map, context, backupFolder, new SaveBackupDataSource.PatchMethod(SaveBackupDataSource.SaveMapRegion), progressContainer))
			{
				if (Directory.Exists(oldBackupFolder))
				{
					Directory.Delete(oldBackupFolder, true);
				}
				return true;
			}
			return false;
		}

		// Token: 0x060002CB RID: 715 RVA: 0x0001DC68 File Offset: 0x0001CC68
		public bool Load(MapEditorMap map, MainForm.Context context, IProgressContainer progressContainer, out bool somethingCreated)
		{
			somethingCreated = false;
			if (progressContainer != null)
			{
				progressContainer.AddString(Strings.LOADING_BACKUP);
			}
			if (progressContainer != null)
			{
				progressContainer.Progress++;
			}
			return true;
		}

		// Token: 0x0400024B RID: 587
		private readonly Point mapSize;

		// Token: 0x02000029 RID: 41
		// (Invoke) Token: 0x060002CD RID: 717
		private delegate bool PatchMethod(MapEditorMap map, MainForm.Context context, Point patchIndex, string backupFolder);
	}
}
