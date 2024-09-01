using System;
using MapEditor.Map.MapObjectElements;
using Operations;

namespace MapEditor.Map.MapObjects
{
	// Token: 0x0200014B RID: 331
	internal class ChangeSpawnPointSingleSpawnControllerOperation : IOperation
	{
		// Token: 0x06001006 RID: 4102 RVA: 0x0007A833 File Offset: 0x00079833
		public ChangeSpawnPointSingleSpawnControllerOperation(SpawnPoint _spawnPoint, ref SingleSpawnController _oldValue, ref SingleSpawnController _newValue)
		{
			this.spawnPoint = _spawnPoint;
			this.oldValue = _oldValue;
			this.newValue = _newValue;
		}

		// Token: 0x06001007 RID: 4103 RVA: 0x0007A852 File Offset: 0x00079852
		public bool Undo()
		{
			if (!this.IsEmpty)
			{
				this.spawnPoint.SingleSpawnController = this.oldValue;
				return true;
			}
			return false;
		}

		// Token: 0x06001008 RID: 4104 RVA: 0x0007A870 File Offset: 0x00079870
		public bool Redo()
		{
			if (!this.IsEmpty)
			{
				this.spawnPoint.SingleSpawnController = this.newValue;
				return true;
			}
			return false;
		}

		// Token: 0x06001009 RID: 4105 RVA: 0x0007A88E File Offset: 0x0007988E
		public void Destroy()
		{
			this.spawnPoint = null;
		}

		// Token: 0x1700033C RID: 828
		// (get) Token: 0x0600100A RID: 4106 RVA: 0x0007A897 File Offset: 0x00079897
		public bool IsHighPriority
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700033D RID: 829
		// (get) Token: 0x0600100B RID: 4107 RVA: 0x0007A89A File Offset: 0x0007989A
		public bool IsEmpty
		{
			get
			{
				return this.spawnPoint == null;
			}
		}

		// Token: 0x1700033E RID: 830
		// (get) Token: 0x0600100C RID: 4108 RVA: 0x0007A8A5 File Offset: 0x000798A5
		public bool IsAbsolute
		{
			get
			{
				return false;
			}
		}

		// Token: 0x0600100D RID: 4109 RVA: 0x0007A8A8 File Offset: 0x000798A8
		public bool ContainsLabel(string label)
		{
			return label == this.Label;
		}

		// Token: 0x1700033F RID: 831
		// (get) Token: 0x0600100E RID: 4110 RVA: 0x0007A8B6 File Offset: 0x000798B6
		public string Label
		{
			get
			{
				return "MapObjectContainer";
			}
		}

		// Token: 0x17000340 RID: 832
		// (get) Token: 0x0600100F RID: 4111 RVA: 0x0007A8BD File Offset: 0x000798BD
		public string Description
		{
			get
			{
				return "ChangeSpawnPointSingleSpawnControllerOperation";
			}
		}

		// Token: 0x04000C06 RID: 3078
		private SpawnPoint spawnPoint;

		// Token: 0x04000C07 RID: 3079
		private readonly SingleSpawnController oldValue;

		// Token: 0x04000C08 RID: 3080
		private readonly SingleSpawnController newValue;
	}
}
