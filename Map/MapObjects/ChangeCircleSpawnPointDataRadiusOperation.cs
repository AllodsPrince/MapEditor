using System;
using Operations;

namespace MapEditor.Map.MapObjects
{
	// Token: 0x0200014C RID: 332
	internal class ChangeCircleSpawnPointDataRadiusOperation : IOperation
	{
		// Token: 0x06001010 RID: 4112 RVA: 0x0007A8C4 File Offset: 0x000798C4
		public ChangeCircleSpawnPointDataRadiusOperation(SpawnPoint _spawnPoint, ref double _oldValue, ref double _newValue)
		{
			this.spawnPoint = _spawnPoint;
			this.oldValue = _oldValue;
			this.newValue = _newValue;
		}

		// Token: 0x06001011 RID: 4113 RVA: 0x0007A8E4 File Offset: 0x000798E4
		public bool Undo()
		{
			if (!this.IsEmpty)
			{
				CircleSpawnPointData circleSpawnPointData = this.spawnPoint.SpawnPointData as CircleSpawnPointData;
				if (circleSpawnPointData != null)
				{
					circleSpawnPointData.Radius = this.oldValue;
					return true;
				}
			}
			return false;
		}

		// Token: 0x06001012 RID: 4114 RVA: 0x0007A91C File Offset: 0x0007991C
		public bool Redo()
		{
			if (!this.IsEmpty)
			{
				CircleSpawnPointData circleSpawnPointData = this.spawnPoint.SpawnPointData as CircleSpawnPointData;
				if (circleSpawnPointData != null)
				{
					circleSpawnPointData.Radius = this.newValue;
					return true;
				}
			}
			return false;
		}

		// Token: 0x06001013 RID: 4115 RVA: 0x0007A954 File Offset: 0x00079954
		public void Destroy()
		{
			this.spawnPoint = null;
		}

		// Token: 0x17000341 RID: 833
		// (get) Token: 0x06001014 RID: 4116 RVA: 0x0007A95D File Offset: 0x0007995D
		public bool IsEmpty
		{
			get
			{
				return this.spawnPoint == null || !(this.spawnPoint.SpawnPointData is CircleSpawnPointData);
			}
		}

		// Token: 0x17000342 RID: 834
		// (get) Token: 0x06001015 RID: 4117 RVA: 0x0007A97F File Offset: 0x0007997F
		public bool IsAbsolute
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000343 RID: 835
		// (get) Token: 0x06001016 RID: 4118 RVA: 0x0007A982 File Offset: 0x00079982
		public bool IsHighPriority
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06001017 RID: 4119 RVA: 0x0007A985 File Offset: 0x00079985
		public bool ContainsLabel(string label)
		{
			return label == this.Label;
		}

		// Token: 0x17000344 RID: 836
		// (get) Token: 0x06001018 RID: 4120 RVA: 0x0007A993 File Offset: 0x00079993
		public string Label
		{
			get
			{
				return "MapObjectContainer";
			}
		}

		// Token: 0x17000345 RID: 837
		// (get) Token: 0x06001019 RID: 4121 RVA: 0x0007A99A File Offset: 0x0007999A
		public string Description
		{
			get
			{
				return "ChangeCircleSpawnPointDataRadiusOperation";
			}
		}

		// Token: 0x04000C09 RID: 3081
		private SpawnPoint spawnPoint;

		// Token: 0x04000C0A RID: 3082
		private readonly double oldValue;

		// Token: 0x04000C0B RID: 3083
		private readonly double newValue;
	}
}
