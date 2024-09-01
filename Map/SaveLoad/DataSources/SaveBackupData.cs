using System;
using System.IO;

namespace MapEditor.Map.SaveLoad.DataSources
{
	// Token: 0x02000027 RID: 39
	public class SaveBackupData
	{
		// Token: 0x17000031 RID: 49
		// (get) Token: 0x060002BE RID: 702 RVA: 0x0001D9A4 File Offset: 0x0001C9A4
		// (set) Token: 0x060002BF RID: 703 RVA: 0x0001D9AC File Offset: 0x0001C9AC
		public string BackupForder
		{
			get
			{
				return this.backupForder;
			}
			set
			{
				this.backupForder = value;
			}
		}

		// Token: 0x17000032 RID: 50
		// (get) Token: 0x060002C0 RID: 704 RVA: 0x0001D9B5 File Offset: 0x0001C9B5
		// (set) Token: 0x060002C1 RID: 705 RVA: 0x0001D9BD File Offset: 0x0001C9BD
		public string BackupForder0
		{
			get
			{
				return this.backupForder0;
			}
			set
			{
				this.backupForder0 = value;
			}
		}

		// Token: 0x17000033 RID: 51
		// (get) Token: 0x060002C2 RID: 706 RVA: 0x0001D9C6 File Offset: 0x0001C9C6
		// (set) Token: 0x060002C3 RID: 707 RVA: 0x0001D9CE File Offset: 0x0001C9CE
		public string BackupForder1
		{
			get
			{
				return this.backupForder1;
			}
			set
			{
				this.backupForder1 = value;
			}
		}

		// Token: 0x060002C4 RID: 708 RVA: 0x0001D9D8 File Offset: 0x0001C9D8
		public void GetBackupFolders(out string _backupFolder, out string _oldBackupFolder)
		{
			string _backupFolder2 = EditorEnvironment.EditorFolder + this.backupForder + this.backupForder0;
			string _backupFolder3 = EditorEnvironment.EditorFolder + this.backupForder + this.backupForder1;
			if (Directory.Exists(_backupFolder2))
			{
				_backupFolder = _backupFolder3;
				_oldBackupFolder = _backupFolder2;
				return;
			}
			_backupFolder = _backupFolder2;
			_oldBackupFolder = _backupFolder3;
		}

		// Token: 0x04000248 RID: 584
		private string backupForder = "Backup/";

		// Token: 0x04000249 RID: 585
		private string backupForder0 = "0/";

		// Token: 0x0400024A RID: 586
		private string backupForder1 = "1/";
	}
}
