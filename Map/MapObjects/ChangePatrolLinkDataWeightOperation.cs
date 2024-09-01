using System;
using Operations;

namespace MapEditor.Map.MapObjects
{
	// Token: 0x0200025A RID: 602
	internal class ChangePatrolLinkDataWeightOperation : IOperation
	{
		// Token: 0x06001CA3 RID: 7331 RVA: 0x000B6D6B File Offset: 0x000B5D6B
		public ChangePatrolLinkDataWeightOperation(PatrolLinkData _patrolLinkData, ref int _oldValue, ref int _newValue)
		{
			this.patrolLinkData = _patrolLinkData;
			this.oldValue = _oldValue;
			this.newValue = _newValue;
		}

		// Token: 0x06001CA4 RID: 7332 RVA: 0x000B6D8A File Offset: 0x000B5D8A
		public bool Undo()
		{
			if (!this.IsEmpty)
			{
				this.patrolLinkData.Weight = this.oldValue;
				return true;
			}
			return false;
		}

		// Token: 0x06001CA5 RID: 7333 RVA: 0x000B6DA8 File Offset: 0x000B5DA8
		public bool Redo()
		{
			if (!this.IsEmpty)
			{
				this.patrolLinkData.Weight = this.newValue;
				return true;
			}
			return false;
		}

		// Token: 0x06001CA6 RID: 7334 RVA: 0x000B6DC6 File Offset: 0x000B5DC6
		public void Destroy()
		{
			this.patrolLinkData = null;
		}

		// Token: 0x170006C5 RID: 1733
		// (get) Token: 0x06001CA7 RID: 7335 RVA: 0x000B6DCF File Offset: 0x000B5DCF
		public bool IsEmpty
		{
			get
			{
				return this.patrolLinkData == null;
			}
		}

		// Token: 0x170006C6 RID: 1734
		// (get) Token: 0x06001CA8 RID: 7336 RVA: 0x000B6DDA File Offset: 0x000B5DDA
		public bool IsAbsolute
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170006C7 RID: 1735
		// (get) Token: 0x06001CA9 RID: 7337 RVA: 0x000B6DDD File Offset: 0x000B5DDD
		public bool IsHighPriority
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06001CAA RID: 7338 RVA: 0x000B6DE0 File Offset: 0x000B5DE0
		public bool ContainsLabel(string label)
		{
			return label == this.Label;
		}

		// Token: 0x170006C8 RID: 1736
		// (get) Token: 0x06001CAB RID: 7339 RVA: 0x000B6DEE File Offset: 0x000B5DEE
		public string Label
		{
			get
			{
				return "MapObjectContainer";
			}
		}

		// Token: 0x170006C9 RID: 1737
		// (get) Token: 0x06001CAC RID: 7340 RVA: 0x000B6DF5 File Offset: 0x000B5DF5
		public string Description
		{
			get
			{
				return "ChangePatrolLinkDataWeightOperation";
			}
		}

		// Token: 0x04001248 RID: 4680
		private PatrolLinkData patrolLinkData;

		// Token: 0x04001249 RID: 4681
		private readonly int oldValue;

		// Token: 0x0400124A RID: 4682
		private readonly int newValue;
	}
}
