using System;
using Operations;

namespace MapEditor.Map.MapObjects
{
	// Token: 0x02000145 RID: 325
	internal class ChangeSpawnPointSpawnTableOperation : IOperation
	{
		// Token: 0x06000FCA RID: 4042 RVA: 0x0007A4CD File Offset: 0x000794CD
		public ChangeSpawnPointSpawnTableOperation(SpawnPoint _spawnPoint, ref string _oldValue, ref string _newValue)
		{
			this.spawnPoint = _spawnPoint;
			this.oldValue = _oldValue;
			this.newValue = _newValue;
		}

		// Token: 0x06000FCB RID: 4043 RVA: 0x0007A4EC File Offset: 0x000794EC
		public bool Undo()
		{
			if (!this.IsEmpty)
			{
				this.spawnPoint.SpawnTable = this.oldValue;
				return true;
			}
			return false;
		}

		// Token: 0x06000FCC RID: 4044 RVA: 0x0007A50A File Offset: 0x0007950A
		public bool Redo()
		{
			if (!this.IsEmpty)
			{
				this.spawnPoint.SpawnTable = this.newValue;
				return true;
			}
			return false;
		}

		// Token: 0x06000FCD RID: 4045 RVA: 0x0007A528 File Offset: 0x00079528
		public void Destroy()
		{
			this.spawnPoint = null;
		}

		// Token: 0x1700031E RID: 798
		// (get) Token: 0x06000FCE RID: 4046 RVA: 0x0007A531 File Offset: 0x00079531
		public bool IsEmpty
		{
			get
			{
				return this.spawnPoint == null;
			}
		}

		// Token: 0x1700031F RID: 799
		// (get) Token: 0x06000FCF RID: 4047 RVA: 0x0007A53C File Offset: 0x0007953C
		public bool IsAbsolute
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000320 RID: 800
		// (get) Token: 0x06000FD0 RID: 4048 RVA: 0x0007A53F File Offset: 0x0007953F
		public bool IsHighPriority
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06000FD1 RID: 4049 RVA: 0x0007A542 File Offset: 0x00079542
		public bool ContainsLabel(string label)
		{
			return label == this.Label;
		}

		// Token: 0x17000321 RID: 801
		// (get) Token: 0x06000FD2 RID: 4050 RVA: 0x0007A550 File Offset: 0x00079550
		public string Label
		{
			get
			{
				return "MapObjectContainer";
			}
		}

		// Token: 0x17000322 RID: 802
		// (get) Token: 0x06000FD3 RID: 4051 RVA: 0x0007A557 File Offset: 0x00079557
		public string Description
		{
			get
			{
				return "ChangeSpawnPointSpawnTableOperation";
			}
		}

		// Token: 0x04000BF4 RID: 3060
		private SpawnPoint spawnPoint;

		// Token: 0x04000BF5 RID: 3061
		private readonly string oldValue;

		// Token: 0x04000BF6 RID: 3062
		private readonly string newValue;
	}
}
