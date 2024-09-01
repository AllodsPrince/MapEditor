using System;
using Operations;

namespace MapEditor.Map.MapObjects
{
	// Token: 0x0200014A RID: 330
	internal class ChangeSpawnPointSpawnTunerOperation : IOperation
	{
		// Token: 0x06000FFC RID: 4092 RVA: 0x0007A7A2 File Offset: 0x000797A2
		public ChangeSpawnPointSpawnTunerOperation(SpawnPoint _spawnPoint, ref string _oldValue, ref string _newValue)
		{
			this.spawnPoint = _spawnPoint;
			this.oldValue = _oldValue;
			this.newValue = _newValue;
		}

		// Token: 0x06000FFD RID: 4093 RVA: 0x0007A7C1 File Offset: 0x000797C1
		public bool Undo()
		{
			if (!this.IsEmpty)
			{
				this.spawnPoint.SpawnTuner = this.oldValue;
				return true;
			}
			return false;
		}

		// Token: 0x06000FFE RID: 4094 RVA: 0x0007A7DF File Offset: 0x000797DF
		public bool Redo()
		{
			if (!this.IsEmpty)
			{
				this.spawnPoint.SpawnTuner = this.newValue;
				return true;
			}
			return false;
		}

		// Token: 0x06000FFF RID: 4095 RVA: 0x0007A7FD File Offset: 0x000797FD
		public void Destroy()
		{
			this.spawnPoint = null;
		}

		// Token: 0x17000337 RID: 823
		// (get) Token: 0x06001000 RID: 4096 RVA: 0x0007A806 File Offset: 0x00079806
		public bool IsHighPriority
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000338 RID: 824
		// (get) Token: 0x06001001 RID: 4097 RVA: 0x0007A809 File Offset: 0x00079809
		public bool IsEmpty
		{
			get
			{
				return this.spawnPoint == null;
			}
		}

		// Token: 0x17000339 RID: 825
		// (get) Token: 0x06001002 RID: 4098 RVA: 0x0007A814 File Offset: 0x00079814
		public bool IsAbsolute
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06001003 RID: 4099 RVA: 0x0007A817 File Offset: 0x00079817
		public bool ContainsLabel(string label)
		{
			return label == this.Label;
		}

		// Token: 0x1700033A RID: 826
		// (get) Token: 0x06001004 RID: 4100 RVA: 0x0007A825 File Offset: 0x00079825
		public string Label
		{
			get
			{
				return "MapObjectContainer";
			}
		}

		// Token: 0x1700033B RID: 827
		// (get) Token: 0x06001005 RID: 4101 RVA: 0x0007A82C File Offset: 0x0007982C
		public string Description
		{
			get
			{
				return "ChangeSpawnPointSpawnTunerOperation";
			}
		}

		// Token: 0x04000C03 RID: 3075
		private SpawnPoint spawnPoint;

		// Token: 0x04000C04 RID: 3076
		private readonly string oldValue;

		// Token: 0x04000C05 RID: 3077
		private readonly string newValue;
	}
}
