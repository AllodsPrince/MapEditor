using System;
using Operations;

namespace MapEditor.Map.MapObjects
{
	// Token: 0x02000152 RID: 338
	internal class ChangeSpawnCircleSpawnPointDataMultiplicityOperation : IOperation
	{
		// Token: 0x0600104C RID: 4172 RVA: 0x0007ADED File Offset: 0x00079DED
		public ChangeSpawnCircleSpawnPointDataMultiplicityOperation(SpawnPoint _spawnPoint, ref int _oldValue, ref int _newValue)
		{
			this.spawnPoint = _spawnPoint;
			this.oldValue = _oldValue;
			this.newValue = _newValue;
		}

		// Token: 0x0600104D RID: 4173 RVA: 0x0007AE0C File Offset: 0x00079E0C
		public bool Undo()
		{
			if (!this.IsEmpty)
			{
				SpawnCircleSpawnPointData spawnCircleSpawnPointData = this.spawnPoint.SpawnPointData as SpawnCircleSpawnPointData;
				if (spawnCircleSpawnPointData != null)
				{
					spawnCircleSpawnPointData.Multiplicity = this.oldValue;
					return true;
				}
			}
			return false;
		}

		// Token: 0x0600104E RID: 4174 RVA: 0x0007AE44 File Offset: 0x00079E44
		public bool Redo()
		{
			if (!this.IsEmpty)
			{
				SpawnCircleSpawnPointData spawnCircleSpawnPointData = this.spawnPoint.SpawnPointData as SpawnCircleSpawnPointData;
				if (spawnCircleSpawnPointData != null)
				{
					spawnCircleSpawnPointData.Multiplicity = this.newValue;
					return true;
				}
			}
			return false;
		}

		// Token: 0x0600104F RID: 4175 RVA: 0x0007AE7C File Offset: 0x00079E7C
		public void Destroy()
		{
			this.spawnPoint = null;
		}

		// Token: 0x1700035F RID: 863
		// (get) Token: 0x06001050 RID: 4176 RVA: 0x0007AE85 File Offset: 0x00079E85
		public bool IsEmpty
		{
			get
			{
				return this.spawnPoint == null || !(this.spawnPoint.SpawnPointData is SpawnCircleSpawnPointData);
			}
		}

		// Token: 0x17000360 RID: 864
		// (get) Token: 0x06001051 RID: 4177 RVA: 0x0007AEA7 File Offset: 0x00079EA7
		public bool IsAbsolute
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000361 RID: 865
		// (get) Token: 0x06001052 RID: 4178 RVA: 0x0007AEAA File Offset: 0x00079EAA
		public bool IsHighPriority
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06001053 RID: 4179 RVA: 0x0007AEAD File Offset: 0x00079EAD
		public bool ContainsLabel(string label)
		{
			return label == this.Label;
		}

		// Token: 0x17000362 RID: 866
		// (get) Token: 0x06001054 RID: 4180 RVA: 0x0007AEBB File Offset: 0x00079EBB
		public string Label
		{
			get
			{
				return "MapObjectContainer";
			}
		}

		// Token: 0x17000363 RID: 867
		// (get) Token: 0x06001055 RID: 4181 RVA: 0x0007AEC2 File Offset: 0x00079EC2
		public string Description
		{
			get
			{
				return "ChangeSpawnCircleSpawnPointDataMultiplicityOperation";
			}
		}

		// Token: 0x04000C1B RID: 3099
		private SpawnPoint spawnPoint;

		// Token: 0x04000C1C RID: 3100
		private readonly int oldValue;

		// Token: 0x04000C1D RID: 3101
		private readonly int newValue;
	}
}
