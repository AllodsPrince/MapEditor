using System;
using Operations;

namespace MapEditor.Map.MapObjects
{
	// Token: 0x0200014F RID: 335
	internal class ChangePatrolSpawnPointDataLabelOperation : IOperation
	{
		// Token: 0x0600102E RID: 4142 RVA: 0x0007AB59 File Offset: 0x00079B59
		public ChangePatrolSpawnPointDataLabelOperation(SpawnPoint _spawnPoint, ref string _oldValue, ref string _newValue)
		{
			this.spawnPoint = _spawnPoint;
			this.oldValue = _oldValue;
			this.newValue = _newValue;
		}

		// Token: 0x0600102F RID: 4143 RVA: 0x0007AB78 File Offset: 0x00079B78
		public bool Undo()
		{
			if (!this.IsEmpty)
			{
				PatrolSpawnPointData patrolSpawnPointData = this.spawnPoint.SpawnPointData as PatrolSpawnPointData;
				if (patrolSpawnPointData != null)
				{
					patrolSpawnPointData.Label = this.oldValue;
					return true;
				}
			}
			return false;
		}

		// Token: 0x06001030 RID: 4144 RVA: 0x0007ABB0 File Offset: 0x00079BB0
		public bool Redo()
		{
			if (!this.IsEmpty)
			{
				PatrolSpawnPointData patrolSpawnPointData = this.spawnPoint.SpawnPointData as PatrolSpawnPointData;
				if (patrolSpawnPointData != null)
				{
					patrolSpawnPointData.Label = this.newValue;
					return true;
				}
			}
			return false;
		}

		// Token: 0x06001031 RID: 4145 RVA: 0x0007ABE8 File Offset: 0x00079BE8
		public void Destroy()
		{
			this.spawnPoint = null;
		}

		// Token: 0x17000350 RID: 848
		// (get) Token: 0x06001032 RID: 4146 RVA: 0x0007ABF1 File Offset: 0x00079BF1
		public bool IsEmpty
		{
			get
			{
				return this.spawnPoint == null || !(this.spawnPoint.SpawnPointData is PatrolSpawnPointData);
			}
		}

		// Token: 0x17000351 RID: 849
		// (get) Token: 0x06001033 RID: 4147 RVA: 0x0007AC13 File Offset: 0x00079C13
		public bool IsAbsolute
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000352 RID: 850
		// (get) Token: 0x06001034 RID: 4148 RVA: 0x0007AC16 File Offset: 0x00079C16
		public bool IsHighPriority
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06001035 RID: 4149 RVA: 0x0007AC19 File Offset: 0x00079C19
		public bool ContainsLabel(string label)
		{
			return label == this.Label;
		}

		// Token: 0x17000353 RID: 851
		// (get) Token: 0x06001036 RID: 4150 RVA: 0x0007AC27 File Offset: 0x00079C27
		public string Label
		{
			get
			{
				return "MapObjectContainer";
			}
		}

		// Token: 0x17000354 RID: 852
		// (get) Token: 0x06001037 RID: 4151 RVA: 0x0007AC2E File Offset: 0x00079C2E
		public string Description
		{
			get
			{
				return "ChangePatrolSpawnPointDataLabelOperation";
			}
		}

		// Token: 0x04000C12 RID: 3090
		private SpawnPoint spawnPoint;

		// Token: 0x04000C13 RID: 3091
		private readonly string oldValue;

		// Token: 0x04000C14 RID: 3092
		private readonly string newValue;
	}
}
