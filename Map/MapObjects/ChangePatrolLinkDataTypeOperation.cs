using System;
using Operations;

namespace MapEditor.Map.MapObjects
{
	// Token: 0x02000258 RID: 600
	internal class ChangePatrolLinkDataTypeOperation : IOperation
	{
		// Token: 0x06001C8F RID: 7311 RVA: 0x000B6C49 File Offset: 0x000B5C49
		public ChangePatrolLinkDataTypeOperation(PatrolLinkData _patrolLinkData, ref PatrolNodeLinkType _oldValue, ref PatrolNodeLinkType _newValue)
		{
			this.patrolLinkData = _patrolLinkData;
			this.oldValue = _oldValue;
			this.newValue = _newValue;
		}

		// Token: 0x06001C90 RID: 7312 RVA: 0x000B6C68 File Offset: 0x000B5C68
		public bool Undo()
		{
			if (!this.IsEmpty)
			{
				this.patrolLinkData.Type = this.oldValue;
				return true;
			}
			return false;
		}

		// Token: 0x06001C91 RID: 7313 RVA: 0x000B6C86 File Offset: 0x000B5C86
		public bool Redo()
		{
			if (!this.IsEmpty)
			{
				this.patrolLinkData.Type = this.newValue;
				return true;
			}
			return false;
		}

		// Token: 0x06001C92 RID: 7314 RVA: 0x000B6CA4 File Offset: 0x000B5CA4
		public void Destroy()
		{
			this.patrolLinkData = null;
		}

		// Token: 0x170006BB RID: 1723
		// (get) Token: 0x06001C93 RID: 7315 RVA: 0x000B6CAD File Offset: 0x000B5CAD
		public bool IsEmpty
		{
			get
			{
				return this.patrolLinkData == null;
			}
		}

		// Token: 0x170006BC RID: 1724
		// (get) Token: 0x06001C94 RID: 7316 RVA: 0x000B6CB8 File Offset: 0x000B5CB8
		public bool IsAbsolute
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170006BD RID: 1725
		// (get) Token: 0x06001C95 RID: 7317 RVA: 0x000B6CBB File Offset: 0x000B5CBB
		public bool IsHighPriority
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06001C96 RID: 7318 RVA: 0x000B6CBE File Offset: 0x000B5CBE
		public bool ContainsLabel(string label)
		{
			return label == this.Label;
		}

		// Token: 0x170006BE RID: 1726
		// (get) Token: 0x06001C97 RID: 7319 RVA: 0x000B6CCC File Offset: 0x000B5CCC
		public string Label
		{
			get
			{
				return "MapObjectContainer";
			}
		}

		// Token: 0x170006BF RID: 1727
		// (get) Token: 0x06001C98 RID: 7320 RVA: 0x000B6CD3 File Offset: 0x000B5CD3
		public string Description
		{
			get
			{
				return "ChangePatrolLinkDataTypeOperation";
			}
		}

		// Token: 0x04001242 RID: 4674
		private PatrolLinkData patrolLinkData;

		// Token: 0x04001243 RID: 4675
		private readonly PatrolNodeLinkType oldValue;

		// Token: 0x04001244 RID: 4676
		private readonly PatrolNodeLinkType newValue;
	}
}
