using System;
using Operations;

namespace MapEditor.Map.MapObjects
{
	// Token: 0x0200022F RID: 559
	internal class ChangeClientSpawnPointSceneOperation : IOperation
	{
		// Token: 0x06001ACC RID: 6860 RVA: 0x000AF068 File Offset: 0x000AE068
		public ChangeClientSpawnPointSceneOperation(ClientSpawnPoint _clientSpawnPoint, ref string _oldValue, ref string _newValue)
		{
			this.clientSpawnPoint = _clientSpawnPoint;
			this.oldValue = _oldValue;
			this.newValue = _newValue;
		}

		// Token: 0x06001ACD RID: 6861 RVA: 0x000AF087 File Offset: 0x000AE087
		public bool Undo()
		{
			if (!this.IsEmpty)
			{
				this.clientSpawnPoint.Scene = this.oldValue;
				return true;
			}
			return false;
		}

		// Token: 0x06001ACE RID: 6862 RVA: 0x000AF0A5 File Offset: 0x000AE0A5
		public bool Redo()
		{
			if (!this.IsEmpty)
			{
				this.clientSpawnPoint.Scene = this.newValue;
				return true;
			}
			return false;
		}

		// Token: 0x06001ACF RID: 6863 RVA: 0x000AF0C3 File Offset: 0x000AE0C3
		public void Destroy()
		{
			this.clientSpawnPoint = null;
		}

		// Token: 0x17000659 RID: 1625
		// (get) Token: 0x06001AD0 RID: 6864 RVA: 0x000AF0CC File Offset: 0x000AE0CC
		public bool IsEmpty
		{
			get
			{
				return this.clientSpawnPoint == null;
			}
		}

		// Token: 0x1700065A RID: 1626
		// (get) Token: 0x06001AD1 RID: 6865 RVA: 0x000AF0D7 File Offset: 0x000AE0D7
		public bool IsAbsolute
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700065B RID: 1627
		// (get) Token: 0x06001AD2 RID: 6866 RVA: 0x000AF0DA File Offset: 0x000AE0DA
		public bool IsHighPriority
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06001AD3 RID: 6867 RVA: 0x000AF0DD File Offset: 0x000AE0DD
		public bool ContainsLabel(string label)
		{
			return label == this.Label;
		}

		// Token: 0x1700065C RID: 1628
		// (get) Token: 0x06001AD4 RID: 6868 RVA: 0x000AF0EB File Offset: 0x000AE0EB
		public string Label
		{
			get
			{
				return "MapObjectContainer";
			}
		}

		// Token: 0x1700065D RID: 1629
		// (get) Token: 0x06001AD5 RID: 6869 RVA: 0x000AF0F2 File Offset: 0x000AE0F2
		public string Description
		{
			get
			{
				return "ChangeClientSpawnPointSceneOperation";
			}
		}

		// Token: 0x0400113E RID: 4414
		private ClientSpawnPoint clientSpawnPoint;

		// Token: 0x0400113F RID: 4415
		private readonly string oldValue;

		// Token: 0x04001140 RID: 4416
		private readonly string newValue;
	}
}
