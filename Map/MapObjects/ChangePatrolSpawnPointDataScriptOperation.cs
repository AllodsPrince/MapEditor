using System;
using Operations;

namespace MapEditor.Map.MapObjects
{
	// Token: 0x02000150 RID: 336
	internal class ChangePatrolSpawnPointDataScriptOperation : IOperation
	{
		// Token: 0x06001038 RID: 4152 RVA: 0x0007AC35 File Offset: 0x00079C35
		public ChangePatrolSpawnPointDataScriptOperation(SpawnPoint _spawnPoint, ref string _oldValue, ref string _newValue)
		{
			this.spawnPoint = _spawnPoint;
			this.oldValue = _oldValue;
			this.newValue = _newValue;
		}

		// Token: 0x06001039 RID: 4153 RVA: 0x0007AC54 File Offset: 0x00079C54
		public bool Undo()
		{
			if (!this.IsEmpty)
			{
				PatrolSpawnPointData patrolSpawnPointData = this.spawnPoint.SpawnPointData as PatrolSpawnPointData;
				if (patrolSpawnPointData != null)
				{
					patrolSpawnPointData.Script = this.oldValue;
					return true;
				}
			}
			return false;
		}

		// Token: 0x0600103A RID: 4154 RVA: 0x0007AC8C File Offset: 0x00079C8C
		public bool Redo()
		{
			if (!this.IsEmpty)
			{
				PatrolSpawnPointData patrolSpawnPointData = this.spawnPoint.SpawnPointData as PatrolSpawnPointData;
				if (patrolSpawnPointData != null)
				{
					patrolSpawnPointData.Script = this.newValue;
					return true;
				}
			}
			return false;
		}

		// Token: 0x0600103B RID: 4155 RVA: 0x0007ACC4 File Offset: 0x00079CC4
		public void Destroy()
		{
			this.spawnPoint = null;
		}

		// Token: 0x17000355 RID: 853
		// (get) Token: 0x0600103C RID: 4156 RVA: 0x0007ACCD File Offset: 0x00079CCD
		public bool IsEmpty
		{
			get
			{
				return this.spawnPoint == null || !(this.spawnPoint.SpawnPointData is PatrolSpawnPointData);
			}
		}

		// Token: 0x17000356 RID: 854
		// (get) Token: 0x0600103D RID: 4157 RVA: 0x0007ACEF File Offset: 0x00079CEF
		public bool IsAbsolute
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000357 RID: 855
		// (get) Token: 0x0600103E RID: 4158 RVA: 0x0007ACF2 File Offset: 0x00079CF2
		public bool IsHighPriority
		{
			get
			{
				return false;
			}
		}

		// Token: 0x0600103F RID: 4159 RVA: 0x0007ACF5 File Offset: 0x00079CF5
		public bool ContainsLabel(string label)
		{
			return label == this.Label;
		}

		// Token: 0x17000358 RID: 856
		// (get) Token: 0x06001040 RID: 4160 RVA: 0x0007AD03 File Offset: 0x00079D03
		public string Label
		{
			get
			{
				return "MapObjectContainer";
			}
		}

		// Token: 0x17000359 RID: 857
		// (get) Token: 0x06001041 RID: 4161 RVA: 0x0007AD0A File Offset: 0x00079D0A
		public string Description
		{
			get
			{
				return "ChangePatrolSpawnPointDataScriptOperation";
			}
		}

		// Token: 0x04000C15 RID: 3093
		private SpawnPoint spawnPoint;

		// Token: 0x04000C16 RID: 3094
		private readonly string oldValue;

		// Token: 0x04000C17 RID: 3095
		private readonly string newValue;
	}
}
