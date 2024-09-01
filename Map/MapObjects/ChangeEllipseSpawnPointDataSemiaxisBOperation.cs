using System;
using Operations;

namespace MapEditor.Map.MapObjects
{
	// Token: 0x0200014E RID: 334
	internal class ChangeEllipseSpawnPointDataSemiaxisBOperation : IOperation
	{
		// Token: 0x06001024 RID: 4132 RVA: 0x0007AA7D File Offset: 0x00079A7D
		public ChangeEllipseSpawnPointDataSemiaxisBOperation(SpawnPoint _spawnPoint, ref double _oldValue, ref double _newValue)
		{
			this.spawnPoint = _spawnPoint;
			this.oldValue = _oldValue;
			this.newValue = _newValue;
		}

		// Token: 0x06001025 RID: 4133 RVA: 0x0007AA9C File Offset: 0x00079A9C
		public bool Undo()
		{
			if (!this.IsEmpty)
			{
				EllipseSpawnPointData ellipseSpawnPointData = this.spawnPoint.SpawnPointData as EllipseSpawnPointData;
				if (ellipseSpawnPointData != null)
				{
					ellipseSpawnPointData.SemiaxisB = this.oldValue;
					return true;
				}
			}
			return false;
		}

		// Token: 0x06001026 RID: 4134 RVA: 0x0007AAD4 File Offset: 0x00079AD4
		public bool Redo()
		{
			if (!this.IsEmpty)
			{
				EllipseSpawnPointData ellipseSpawnPointData = this.spawnPoint.SpawnPointData as EllipseSpawnPointData;
				if (ellipseSpawnPointData != null)
				{
					ellipseSpawnPointData.SemiaxisB = this.newValue;
					return true;
				}
			}
			return false;
		}

		// Token: 0x06001027 RID: 4135 RVA: 0x0007AB0C File Offset: 0x00079B0C
		public void Destroy()
		{
			this.spawnPoint = null;
		}

		// Token: 0x1700034B RID: 843
		// (get) Token: 0x06001028 RID: 4136 RVA: 0x0007AB15 File Offset: 0x00079B15
		public bool IsEmpty
		{
			get
			{
				return this.spawnPoint == null || !(this.spawnPoint.SpawnPointData is EllipseSpawnPointData);
			}
		}

		// Token: 0x1700034C RID: 844
		// (get) Token: 0x06001029 RID: 4137 RVA: 0x0007AB37 File Offset: 0x00079B37
		public bool IsAbsolute
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700034D RID: 845
		// (get) Token: 0x0600102A RID: 4138 RVA: 0x0007AB3A File Offset: 0x00079B3A
		public bool IsHighPriority
		{
			get
			{
				return false;
			}
		}

		// Token: 0x0600102B RID: 4139 RVA: 0x0007AB3D File Offset: 0x00079B3D
		public bool ContainsLabel(string label)
		{
			return label == this.Label;
		}

		// Token: 0x1700034E RID: 846
		// (get) Token: 0x0600102C RID: 4140 RVA: 0x0007AB4B File Offset: 0x00079B4B
		public string Label
		{
			get
			{
				return "MapObjectContainer";
			}
		}

		// Token: 0x1700034F RID: 847
		// (get) Token: 0x0600102D RID: 4141 RVA: 0x0007AB52 File Offset: 0x00079B52
		public string Description
		{
			get
			{
				return "ChangeEllipseSpawnPointDataSemiaxisBOperation";
			}
		}

		// Token: 0x04000C0F RID: 3087
		private SpawnPoint spawnPoint;

		// Token: 0x04000C10 RID: 3088
		private readonly double oldValue;

		// Token: 0x04000C11 RID: 3089
		private readonly double newValue;
	}
}
