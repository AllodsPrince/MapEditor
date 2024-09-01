using System;
using Operations;

namespace MapEditor.Map.MapObjects
{
	// Token: 0x02000257 RID: 599
	internal class ChangePatrolNodeScriptOperation : IOperation
	{
		// Token: 0x06001C85 RID: 7301 RVA: 0x000B6BB8 File Offset: 0x000B5BB8
		public ChangePatrolNodeScriptOperation(PatrolNode _patrolNode, ref string _oldValue, ref string _newValue)
		{
			this.patrolNode = _patrolNode;
			this.oldValue = _oldValue;
			this.newValue = _newValue;
		}

		// Token: 0x06001C86 RID: 7302 RVA: 0x000B6BD7 File Offset: 0x000B5BD7
		public bool Undo()
		{
			if (!this.IsEmpty)
			{
				this.patrolNode.Script = this.oldValue;
				return true;
			}
			return false;
		}

		// Token: 0x06001C87 RID: 7303 RVA: 0x000B6BF5 File Offset: 0x000B5BF5
		public bool Redo()
		{
			if (!this.IsEmpty)
			{
				this.patrolNode.Script = this.newValue;
				return true;
			}
			return false;
		}

		// Token: 0x06001C88 RID: 7304 RVA: 0x000B6C13 File Offset: 0x000B5C13
		public void Destroy()
		{
			this.patrolNode = null;
		}

		// Token: 0x170006B6 RID: 1718
		// (get) Token: 0x06001C89 RID: 7305 RVA: 0x000B6C1C File Offset: 0x000B5C1C
		public bool IsEmpty
		{
			get
			{
				return this.patrolNode == null;
			}
		}

		// Token: 0x170006B7 RID: 1719
		// (get) Token: 0x06001C8A RID: 7306 RVA: 0x000B6C27 File Offset: 0x000B5C27
		public bool IsAbsolute
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170006B8 RID: 1720
		// (get) Token: 0x06001C8B RID: 7307 RVA: 0x000B6C2A File Offset: 0x000B5C2A
		public bool IsHighPriority
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06001C8C RID: 7308 RVA: 0x000B6C2D File Offset: 0x000B5C2D
		public bool ContainsLabel(string label)
		{
			return label == this.Label;
		}

		// Token: 0x170006B9 RID: 1721
		// (get) Token: 0x06001C8D RID: 7309 RVA: 0x000B6C3B File Offset: 0x000B5C3B
		public string Label
		{
			get
			{
				return "MapObjectContainer";
			}
		}

		// Token: 0x170006BA RID: 1722
		// (get) Token: 0x06001C8E RID: 7310 RVA: 0x000B6C42 File Offset: 0x000B5C42
		public string Description
		{
			get
			{
				return "ChangePatrolNodeScriptOperation";
			}
		}

		// Token: 0x0400123F RID: 4671
		private PatrolNode patrolNode;

		// Token: 0x04001240 RID: 4672
		private readonly string oldValue;

		// Token: 0x04001241 RID: 4673
		private readonly string newValue;
	}
}
