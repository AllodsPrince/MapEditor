using System;
using Operations;

namespace MapEditor.Map.MapObjects
{
	// Token: 0x02000232 RID: 562
	internal class ChangeClientSpawnPointDataOperation : IOperation
	{
		// Token: 0x06001AEB RID: 6891 RVA: 0x000AF287 File Offset: 0x000AE287
		public ChangeClientSpawnPointDataOperation(ClientSpawnPoint _clientSpawnPoint, ref ClientSpawnPointData _oldValue, ref ClientSpawnPointData _newValue)
		{
			this.clientSpawnPoint = _clientSpawnPoint;
			this.oldValue = _oldValue;
			this.newValue = _newValue;
		}

		// Token: 0x06001AEC RID: 6892 RVA: 0x000AF2A6 File Offset: 0x000AE2A6
		public bool Undo()
		{
			if (!this.IsEmpty)
			{
				this.clientSpawnPoint.ClientSpawnPointData = this.oldValue;
				return true;
			}
			return false;
		}

		// Token: 0x06001AED RID: 6893 RVA: 0x000AF2C4 File Offset: 0x000AE2C4
		public bool Redo()
		{
			if (!this.IsEmpty)
			{
				this.clientSpawnPoint.ClientSpawnPointData = this.newValue;
				return true;
			}
			return false;
		}

		// Token: 0x06001AEE RID: 6894 RVA: 0x000AF2E2 File Offset: 0x000AE2E2
		public void Destroy()
		{
			this.clientSpawnPoint = null;
		}

		// Token: 0x17000668 RID: 1640
		// (get) Token: 0x06001AEF RID: 6895 RVA: 0x000AF2EB File Offset: 0x000AE2EB
		public bool IsEmpty
		{
			get
			{
				return this.clientSpawnPoint == null;
			}
		}

		// Token: 0x17000669 RID: 1641
		// (get) Token: 0x06001AF0 RID: 6896 RVA: 0x000AF2F6 File Offset: 0x000AE2F6
		public bool IsAbsolute
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700066A RID: 1642
		// (get) Token: 0x06001AF1 RID: 6897 RVA: 0x000AF2F9 File Offset: 0x000AE2F9
		public bool IsHighPriority
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06001AF2 RID: 6898 RVA: 0x000AF2FC File Offset: 0x000AE2FC
		public bool ContainsLabel(string label)
		{
			return label == this.Label;
		}

		// Token: 0x1700066B RID: 1643
		// (get) Token: 0x06001AF3 RID: 6899 RVA: 0x000AF30A File Offset: 0x000AE30A
		public string Label
		{
			get
			{
				return "MapObjectContainer";
			}
		}

		// Token: 0x1700066C RID: 1644
		// (get) Token: 0x06001AF4 RID: 6900 RVA: 0x000AF311 File Offset: 0x000AE311
		public string Description
		{
			get
			{
				return "ChangeCleintSpawnPointDataOperation";
			}
		}

		// Token: 0x04001148 RID: 4424
		private ClientSpawnPoint clientSpawnPoint;

		// Token: 0x04001149 RID: 4425
		private readonly ClientSpawnPointData oldValue;

		// Token: 0x0400114A RID: 4426
		private readonly ClientSpawnPointData newValue;
	}
}
