using System;
using Operations;

namespace MapEditor.Map.MapObjects
{
	// Token: 0x02000148 RID: 328
	internal class ChangeSpawnPointScanRadiusOperation : IOperation
	{
		// Token: 0x06000FE8 RID: 4072 RVA: 0x0007A680 File Offset: 0x00079680
		public ChangeSpawnPointScanRadiusOperation(SpawnPoint _spawnPoint, ref double _oldValue, ref double _newValue)
		{
			this.spawnPoint = _spawnPoint;
			this.oldValue = _oldValue;
			this.newValue = _newValue;
		}

		// Token: 0x06000FE9 RID: 4073 RVA: 0x0007A69F File Offset: 0x0007969F
		public bool Undo()
		{
			if (!this.IsEmpty)
			{
				this.spawnPoint.ScanRadius = this.oldValue;
				return true;
			}
			return false;
		}

		// Token: 0x06000FEA RID: 4074 RVA: 0x0007A6BD File Offset: 0x000796BD
		public bool Redo()
		{
			if (!this.IsEmpty)
			{
				this.spawnPoint.ScanRadius = this.newValue;
				return true;
			}
			return false;
		}

		// Token: 0x06000FEB RID: 4075 RVA: 0x0007A6DB File Offset: 0x000796DB
		public void Destroy()
		{
			this.spawnPoint = null;
		}

		// Token: 0x1700032D RID: 813
		// (get) Token: 0x06000FEC RID: 4076 RVA: 0x0007A6E4 File Offset: 0x000796E4
		public bool IsHighPriority
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700032E RID: 814
		// (get) Token: 0x06000FED RID: 4077 RVA: 0x0007A6E7 File Offset: 0x000796E7
		public bool IsEmpty
		{
			get
			{
				return this.spawnPoint == null;
			}
		}

		// Token: 0x1700032F RID: 815
		// (get) Token: 0x06000FEE RID: 4078 RVA: 0x0007A6F2 File Offset: 0x000796F2
		public bool IsAbsolute
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06000FEF RID: 4079 RVA: 0x0007A6F5 File Offset: 0x000796F5
		public bool ContainsLabel(string label)
		{
			return label == this.Label;
		}

		// Token: 0x17000330 RID: 816
		// (get) Token: 0x06000FF0 RID: 4080 RVA: 0x0007A703 File Offset: 0x00079703
		public string Label
		{
			get
			{
				return "MapObjectContainer";
			}
		}

		// Token: 0x17000331 RID: 817
		// (get) Token: 0x06000FF1 RID: 4081 RVA: 0x0007A70A File Offset: 0x0007970A
		public string Description
		{
			get
			{
				return "ChangeSpawnPointScanRadiusOperation";
			}
		}

		// Token: 0x04000BFD RID: 3069
		private SpawnPoint spawnPoint;

		// Token: 0x04000BFE RID: 3070
		private readonly double oldValue;

		// Token: 0x04000BFF RID: 3071
		private readonly double newValue;
	}
}
