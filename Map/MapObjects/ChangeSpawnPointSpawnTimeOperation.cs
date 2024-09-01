using System;
using MapEditor.Map.MapObjectElements;
using Operations;

namespace MapEditor.Map.MapObjects
{
	// Token: 0x02000149 RID: 329
	internal class ChangeSpawnPointSpawnTimeOperation : IOperation
	{
		// Token: 0x06000FF2 RID: 4082 RVA: 0x0007A711 File Offset: 0x00079711
		public ChangeSpawnPointSpawnTimeOperation(SpawnPoint _spawnPoint, ref SpawnTimeAbstract _oldValue, ref SpawnTimeAbstract _newValue)
		{
			this.spawnPoint = _spawnPoint;
			this.oldValue = _oldValue;
			this.newValue = _newValue;
		}

		// Token: 0x06000FF3 RID: 4083 RVA: 0x0007A730 File Offset: 0x00079730
		public bool Undo()
		{
			if (!this.IsEmpty)
			{
				this.spawnPoint.SpawnTime = this.oldValue;
				return true;
			}
			return false;
		}

		// Token: 0x06000FF4 RID: 4084 RVA: 0x0007A74E File Offset: 0x0007974E
		public bool Redo()
		{
			if (!this.IsEmpty)
			{
				this.spawnPoint.SpawnTime = this.newValue;
				return true;
			}
			return false;
		}

		// Token: 0x06000FF5 RID: 4085 RVA: 0x0007A76C File Offset: 0x0007976C
		public void Destroy()
		{
			this.spawnPoint = null;
		}

		// Token: 0x17000332 RID: 818
		// (get) Token: 0x06000FF6 RID: 4086 RVA: 0x0007A775 File Offset: 0x00079775
		public bool IsHighPriority
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000333 RID: 819
		// (get) Token: 0x06000FF7 RID: 4087 RVA: 0x0007A778 File Offset: 0x00079778
		public bool IsEmpty
		{
			get
			{
				return this.spawnPoint == null;
			}
		}

		// Token: 0x17000334 RID: 820
		// (get) Token: 0x06000FF8 RID: 4088 RVA: 0x0007A783 File Offset: 0x00079783
		public bool IsAbsolute
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06000FF9 RID: 4089 RVA: 0x0007A786 File Offset: 0x00079786
		public bool ContainsLabel(string label)
		{
			return label == this.Label;
		}

		// Token: 0x17000335 RID: 821
		// (get) Token: 0x06000FFA RID: 4090 RVA: 0x0007A794 File Offset: 0x00079794
		public string Label
		{
			get
			{
				return "MapObjectContainer";
			}
		}

		// Token: 0x17000336 RID: 822
		// (get) Token: 0x06000FFB RID: 4091 RVA: 0x0007A79B File Offset: 0x0007979B
		public string Description
		{
			get
			{
				return "ChangeSpawnPointSpawnTimeOperation";
			}
		}

		// Token: 0x04000C00 RID: 3072
		private SpawnPoint spawnPoint;

		// Token: 0x04000C01 RID: 3073
		private readonly SpawnTimeAbstract oldValue;

		// Token: 0x04000C02 RID: 3074
		private readonly SpawnTimeAbstract newValue;
	}
}
