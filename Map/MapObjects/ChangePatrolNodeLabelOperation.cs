using System;
using Operations;

namespace MapEditor.Map.MapObjects
{
	// Token: 0x02000256 RID: 598
	internal class ChangePatrolNodeLabelOperation : IOperation
	{
		// Token: 0x06001C7B RID: 7291 RVA: 0x000B6B27 File Offset: 0x000B5B27
		public ChangePatrolNodeLabelOperation(PatrolNode _patrolNode, ref string _oldValue, ref string _newValue)
		{
			this.patrolNode = _patrolNode;
			this.oldValue = _oldValue;
			this.newValue = _newValue;
		}

		// Token: 0x06001C7C RID: 7292 RVA: 0x000B6B46 File Offset: 0x000B5B46
		public bool Undo()
		{
			if (!this.IsEmpty)
			{
				this.patrolNode.Label = this.oldValue;
				return true;
			}
			return false;
		}

		// Token: 0x06001C7D RID: 7293 RVA: 0x000B6B64 File Offset: 0x000B5B64
		public bool Redo()
		{
			if (!this.IsEmpty)
			{
				this.patrolNode.Label = this.newValue;
				return true;
			}
			return false;
		}

		// Token: 0x06001C7E RID: 7294 RVA: 0x000B6B82 File Offset: 0x000B5B82
		public void Destroy()
		{
			this.patrolNode = null;
		}

		// Token: 0x170006B1 RID: 1713
		// (get) Token: 0x06001C7F RID: 7295 RVA: 0x000B6B8B File Offset: 0x000B5B8B
		public bool IsEmpty
		{
			get
			{
				return this.patrolNode == null;
			}
		}

		// Token: 0x170006B2 RID: 1714
		// (get) Token: 0x06001C80 RID: 7296 RVA: 0x000B6B96 File Offset: 0x000B5B96
		public bool IsAbsolute
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170006B3 RID: 1715
		// (get) Token: 0x06001C81 RID: 7297 RVA: 0x000B6B99 File Offset: 0x000B5B99
		public bool IsHighPriority
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06001C82 RID: 7298 RVA: 0x000B6B9C File Offset: 0x000B5B9C
		public bool ContainsLabel(string label)
		{
			return label == this.Label;
		}

		// Token: 0x170006B4 RID: 1716
		// (get) Token: 0x06001C83 RID: 7299 RVA: 0x000B6BAA File Offset: 0x000B5BAA
		public string Label
		{
			get
			{
				return "MapObjectContainer";
			}
		}

		// Token: 0x170006B5 RID: 1717
		// (get) Token: 0x06001C84 RID: 7300 RVA: 0x000B6BB1 File Offset: 0x000B5BB1
		public string Description
		{
			get
			{
				return "ChangePatrolNodeLabelOperation";
			}
		}

		// Token: 0x0400123C RID: 4668
		private PatrolNode patrolNode;

		// Token: 0x0400123D RID: 4669
		private readonly string oldValue;

		// Token: 0x0400123E RID: 4670
		private readonly string newValue;
	}
}
