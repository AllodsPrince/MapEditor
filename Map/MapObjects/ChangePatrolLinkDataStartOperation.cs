using System;
using Operations;

namespace MapEditor.Map.MapObjects
{
	// Token: 0x02000259 RID: 601
	internal class ChangePatrolLinkDataStartOperation : IOperation
	{
		// Token: 0x06001C99 RID: 7321 RVA: 0x000B6CDA File Offset: 0x000B5CDA
		public ChangePatrolLinkDataStartOperation(PatrolLinkData _patrolLinkData, ref string _oldValue, ref string _newValue)
		{
			this.patrolLinkData = _patrolLinkData;
			this.oldValue = _oldValue;
			this.newValue = _newValue;
		}

		// Token: 0x06001C9A RID: 7322 RVA: 0x000B6CF9 File Offset: 0x000B5CF9
		public bool Undo()
		{
			if (!this.IsEmpty)
			{
				this.patrolLinkData.Start = this.oldValue;
				return true;
			}
			return false;
		}

		// Token: 0x06001C9B RID: 7323 RVA: 0x000B6D17 File Offset: 0x000B5D17
		public bool Redo()
		{
			if (!this.IsEmpty)
			{
				this.patrolLinkData.Start = this.newValue;
				return true;
			}
			return false;
		}

		// Token: 0x06001C9C RID: 7324 RVA: 0x000B6D35 File Offset: 0x000B5D35
		public void Destroy()
		{
			this.patrolLinkData = null;
		}

		// Token: 0x170006C0 RID: 1728
		// (get) Token: 0x06001C9D RID: 7325 RVA: 0x000B6D3E File Offset: 0x000B5D3E
		public bool IsEmpty
		{
			get
			{
				return this.patrolLinkData == null;
			}
		}

		// Token: 0x170006C1 RID: 1729
		// (get) Token: 0x06001C9E RID: 7326 RVA: 0x000B6D49 File Offset: 0x000B5D49
		public bool IsAbsolute
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170006C2 RID: 1730
		// (get) Token: 0x06001C9F RID: 7327 RVA: 0x000B6D4C File Offset: 0x000B5D4C
		public bool IsHighPriority
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06001CA0 RID: 7328 RVA: 0x000B6D4F File Offset: 0x000B5D4F
		public bool ContainsLabel(string label)
		{
			return label == this.Label;
		}

		// Token: 0x170006C3 RID: 1731
		// (get) Token: 0x06001CA1 RID: 7329 RVA: 0x000B6D5D File Offset: 0x000B5D5D
		public string Label
		{
			get
			{
				return "MapObjectContainer";
			}
		}

		// Token: 0x170006C4 RID: 1732
		// (get) Token: 0x06001CA2 RID: 7330 RVA: 0x000B6D64 File Offset: 0x000B5D64
		public string Description
		{
			get
			{
				return "ChangePatrolLinkDataStartOperation";
			}
		}

		// Token: 0x04001245 RID: 4677
		private PatrolLinkData patrolLinkData;

		// Token: 0x04001246 RID: 4678
		private readonly string oldValue;

		// Token: 0x04001247 RID: 4679
		private readonly string newValue;
	}
}
