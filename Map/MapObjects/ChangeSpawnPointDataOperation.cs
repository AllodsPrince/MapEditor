using System;
using Operations;

namespace MapEditor.Map.MapObjects
{
	// Token: 0x02000144 RID: 324
	internal class ChangeSpawnPointDataOperation : IOperation
	{
		// Token: 0x06000FC0 RID: 4032 RVA: 0x0007A43C File Offset: 0x0007943C
		public ChangeSpawnPointDataOperation(SpawnPoint _spawnPoint, ref SpawnPointData _oldValue, ref SpawnPointData _newValue)
		{
			this.spawnPoint = _spawnPoint;
			this.oldValue = _oldValue;
			this.newValue = _newValue;
		}

		// Token: 0x06000FC1 RID: 4033 RVA: 0x0007A45B File Offset: 0x0007945B
		public bool Undo()
		{
			if (!this.IsEmpty)
			{
				this.spawnPoint.SpawnPointData = this.oldValue;
				return true;
			}
			return false;
		}

		// Token: 0x06000FC2 RID: 4034 RVA: 0x0007A479 File Offset: 0x00079479
		public bool Redo()
		{
			if (!this.IsEmpty)
			{
				this.spawnPoint.SpawnPointData = this.newValue;
				return true;
			}
			return false;
		}

		// Token: 0x06000FC3 RID: 4035 RVA: 0x0007A497 File Offset: 0x00079497
		public void Destroy()
		{
			this.spawnPoint = null;
		}

		// Token: 0x17000319 RID: 793
		// (get) Token: 0x06000FC4 RID: 4036 RVA: 0x0007A4A0 File Offset: 0x000794A0
		public bool IsEmpty
		{
			get
			{
				return this.spawnPoint == null;
			}
		}

		// Token: 0x1700031A RID: 794
		// (get) Token: 0x06000FC5 RID: 4037 RVA: 0x0007A4AB File Offset: 0x000794AB
		public bool IsAbsolute
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700031B RID: 795
		// (get) Token: 0x06000FC6 RID: 4038 RVA: 0x0007A4AE File Offset: 0x000794AE
		public bool IsHighPriority
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06000FC7 RID: 4039 RVA: 0x0007A4B1 File Offset: 0x000794B1
		public bool ContainsLabel(string label)
		{
			return label == this.Label;
		}

		// Token: 0x1700031C RID: 796
		// (get) Token: 0x06000FC8 RID: 4040 RVA: 0x0007A4BF File Offset: 0x000794BF
		public string Label
		{
			get
			{
				return "MapObjectContainer";
			}
		}

		// Token: 0x1700031D RID: 797
		// (get) Token: 0x06000FC9 RID: 4041 RVA: 0x0007A4C6 File Offset: 0x000794C6
		public string Description
		{
			get
			{
				return "ChangeSpawnPointDataOperation";
			}
		}

		// Token: 0x04000BF1 RID: 3057
		private SpawnPoint spawnPoint;

		// Token: 0x04000BF2 RID: 3058
		private readonly SpawnPointData oldValue;

		// Token: 0x04000BF3 RID: 3059
		private readonly SpawnPointData newValue;
	}
}
