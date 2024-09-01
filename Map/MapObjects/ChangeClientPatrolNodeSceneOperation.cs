using System;
using Operations;

namespace MapEditor.Map.MapObjects
{
	// Token: 0x0200003E RID: 62
	internal class ChangeClientPatrolNodeSceneOperation : IOperation
	{
		// Token: 0x06000395 RID: 917 RVA: 0x0001FE77 File Offset: 0x0001EE77
		public ChangeClientPatrolNodeSceneOperation(ClientPatrolNode _clientPatrolNode, ref string _oldValue, ref string _newValue)
		{
			this.clientPatrolNode = _clientPatrolNode;
			this.oldValue = _oldValue;
			this.newValue = _newValue;
		}

		// Token: 0x06000396 RID: 918 RVA: 0x0001FE96 File Offset: 0x0001EE96
		public bool Undo()
		{
			if (!this.IsEmpty)
			{
				this.clientPatrolNode.Scene = this.oldValue;
				return true;
			}
			return false;
		}

		// Token: 0x06000397 RID: 919 RVA: 0x0001FEB4 File Offset: 0x0001EEB4
		public bool Redo()
		{
			if (!this.IsEmpty)
			{
				this.clientPatrolNode.Scene = this.newValue;
				return true;
			}
			return false;
		}

		// Token: 0x06000398 RID: 920 RVA: 0x0001FED2 File Offset: 0x0001EED2
		public void Destroy()
		{
			this.clientPatrolNode = null;
		}

		// Token: 0x17000078 RID: 120
		// (get) Token: 0x06000399 RID: 921 RVA: 0x0001FEDB File Offset: 0x0001EEDB
		public bool IsEmpty
		{
			get
			{
				return this.clientPatrolNode == null;
			}
		}

		// Token: 0x17000079 RID: 121
		// (get) Token: 0x0600039A RID: 922 RVA: 0x0001FEE6 File Offset: 0x0001EEE6
		public bool IsAbsolute
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700007A RID: 122
		// (get) Token: 0x0600039B RID: 923 RVA: 0x0001FEE9 File Offset: 0x0001EEE9
		public bool IsHighPriority
		{
			get
			{
				return false;
			}
		}

		// Token: 0x0600039C RID: 924 RVA: 0x0001FEEC File Offset: 0x0001EEEC
		public bool ContainsLabel(string label)
		{
			return label == this.Label;
		}

		// Token: 0x1700007B RID: 123
		// (get) Token: 0x0600039D RID: 925 RVA: 0x0001FEFA File Offset: 0x0001EEFA
		public string Label
		{
			get
			{
				return "MapObjectContainer";
			}
		}

		// Token: 0x1700007C RID: 124
		// (get) Token: 0x0600039E RID: 926 RVA: 0x0001FF01 File Offset: 0x0001EF01
		public string Description
		{
			get
			{
				return "ChangeClientPatrolNodeSceneOperation";
			}
		}

		// Token: 0x04000299 RID: 665
		private ClientPatrolNode clientPatrolNode;

		// Token: 0x0400029A RID: 666
		private readonly string oldValue;

		// Token: 0x0400029B RID: 667
		private readonly string newValue;
	}
}
