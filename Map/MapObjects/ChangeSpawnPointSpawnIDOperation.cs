using System;
using Operations;

namespace MapEditor.Map.MapObjects
{
	// Token: 0x02000147 RID: 327
	internal class ChangeSpawnPointSpawnIDOperation : IOperation
	{
		// Token: 0x06000FDE RID: 4062 RVA: 0x0007A5EF File Offset: 0x000795EF
		public ChangeSpawnPointSpawnIDOperation(SpawnPoint _spawnPoint, ref string _oldValue, ref string _newValue)
		{
			this.spawnPoint = _spawnPoint;
			this.oldValue = _oldValue;
			this.newValue = _newValue;
		}

		// Token: 0x06000FDF RID: 4063 RVA: 0x0007A60E File Offset: 0x0007960E
		public bool Undo()
		{
			if (!this.IsEmpty)
			{
				this.spawnPoint.SpawnID = this.oldValue;
				return true;
			}
			return false;
		}

		// Token: 0x06000FE0 RID: 4064 RVA: 0x0007A62C File Offset: 0x0007962C
		public bool Redo()
		{
			if (!this.IsEmpty)
			{
				this.spawnPoint.SpawnID = this.newValue;
				return true;
			}
			return false;
		}

		// Token: 0x06000FE1 RID: 4065 RVA: 0x0007A64A File Offset: 0x0007964A
		public void Destroy()
		{
			this.spawnPoint = null;
		}

		// Token: 0x17000328 RID: 808
		// (get) Token: 0x06000FE2 RID: 4066 RVA: 0x0007A653 File Offset: 0x00079653
		public bool IsEmpty
		{
			get
			{
				return this.spawnPoint == null;
			}
		}

		// Token: 0x17000329 RID: 809
		// (get) Token: 0x06000FE3 RID: 4067 RVA: 0x0007A65E File Offset: 0x0007965E
		public bool IsAbsolute
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700032A RID: 810
		// (get) Token: 0x06000FE4 RID: 4068 RVA: 0x0007A661 File Offset: 0x00079661
		public bool IsHighPriority
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06000FE5 RID: 4069 RVA: 0x0007A664 File Offset: 0x00079664
		public bool ContainsLabel(string label)
		{
			return label == this.Label;
		}

		// Token: 0x1700032B RID: 811
		// (get) Token: 0x06000FE6 RID: 4070 RVA: 0x0007A672 File Offset: 0x00079672
		public string Label
		{
			get
			{
				return "MapObjectContainer";
			}
		}

		// Token: 0x1700032C RID: 812
		// (get) Token: 0x06000FE7 RID: 4071 RVA: 0x0007A679 File Offset: 0x00079679
		public string Description
		{
			get
			{
				return "ChangeSpawnPointSpawnIDOperation";
			}
		}

		// Token: 0x04000BFA RID: 3066
		private SpawnPoint spawnPoint;

		// Token: 0x04000BFB RID: 3067
		private readonly string oldValue;

		// Token: 0x04000BFC RID: 3068
		private readonly string newValue;
	}
}
