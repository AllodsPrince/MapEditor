using System;
using Operations;

namespace MapEditor.Map.MapObjects
{
	// Token: 0x0200014D RID: 333
	internal class ChangeEllipseSpawnPointDataSemiaxisAOperation : IOperation
	{
		// Token: 0x0600101A RID: 4122 RVA: 0x0007A9A1 File Offset: 0x000799A1
		public ChangeEllipseSpawnPointDataSemiaxisAOperation(SpawnPoint _spawnPoint, ref double _oldValue, ref double _newValue)
		{
			this.spawnPoint = _spawnPoint;
			this.oldValue = _oldValue;
			this.newValue = _newValue;
		}

		// Token: 0x0600101B RID: 4123 RVA: 0x0007A9C0 File Offset: 0x000799C0
		public bool Undo()
		{
			if (!this.IsEmpty)
			{
				EllipseSpawnPointData ellipseSpawnPointData = this.spawnPoint.SpawnPointData as EllipseSpawnPointData;
				if (ellipseSpawnPointData != null)
				{
					ellipseSpawnPointData.SemiaxisA = this.oldValue;
					return true;
				}
			}
			return false;
		}

		// Token: 0x0600101C RID: 4124 RVA: 0x0007A9F8 File Offset: 0x000799F8
		public bool Redo()
		{
			if (!this.IsEmpty)
			{
				EllipseSpawnPointData ellipseSpawnPointData = this.spawnPoint.SpawnPointData as EllipseSpawnPointData;
				if (ellipseSpawnPointData != null)
				{
					ellipseSpawnPointData.SemiaxisA = this.newValue;
					return true;
				}
			}
			return false;
		}

		// Token: 0x0600101D RID: 4125 RVA: 0x0007AA30 File Offset: 0x00079A30
		public void Destroy()
		{
			this.spawnPoint = null;
		}

		// Token: 0x17000346 RID: 838
		// (get) Token: 0x0600101E RID: 4126 RVA: 0x0007AA39 File Offset: 0x00079A39
		public bool IsEmpty
		{
			get
			{
				return this.spawnPoint == null || !(this.spawnPoint.SpawnPointData is EllipseSpawnPointData);
			}
		}

		// Token: 0x17000347 RID: 839
		// (get) Token: 0x0600101F RID: 4127 RVA: 0x0007AA5B File Offset: 0x00079A5B
		public bool IsAbsolute
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000348 RID: 840
		// (get) Token: 0x06001020 RID: 4128 RVA: 0x0007AA5E File Offset: 0x00079A5E
		public bool IsHighPriority
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06001021 RID: 4129 RVA: 0x0007AA61 File Offset: 0x00079A61
		public bool ContainsLabel(string label)
		{
			return label == this.Label;
		}

		// Token: 0x17000349 RID: 841
		// (get) Token: 0x06001022 RID: 4130 RVA: 0x0007AA6F File Offset: 0x00079A6F
		public string Label
		{
			get
			{
				return "MapObjectContainer";
			}
		}

		// Token: 0x1700034A RID: 842
		// (get) Token: 0x06001023 RID: 4131 RVA: 0x0007AA76 File Offset: 0x00079A76
		public string Description
		{
			get
			{
				return "ChangeEllipseSpawnPointDataSemiaxisAOperation";
			}
		}

		// Token: 0x04000C0C RID: 3084
		private SpawnPoint spawnPoint;

		// Token: 0x04000C0D RID: 3085
		private readonly double oldValue;

		// Token: 0x04000C0E RID: 3086
		private readonly double newValue;
	}
}
