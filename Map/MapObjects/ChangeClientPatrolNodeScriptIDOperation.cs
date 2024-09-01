using System;
using Operations;

namespace MapEditor.Map.MapObjects
{
	// Token: 0x0200003F RID: 63
	internal class ChangeClientPatrolNodeScriptIDOperation : IOperation
	{
		// Token: 0x0600039F RID: 927 RVA: 0x0001FF08 File Offset: 0x0001EF08
		public ChangeClientPatrolNodeScriptIDOperation(ClientPatrolNode _clientPatrolNode, ref string _oldValue, ref string _newValue)
		{
			this.clientPatrolNode = _clientPatrolNode;
			this.oldValue = _oldValue;
			this.newValue = _newValue;
		}

		// Token: 0x060003A0 RID: 928 RVA: 0x0001FF27 File Offset: 0x0001EF27
		public bool Undo()
		{
			if (!this.IsEmpty)
			{
				this.clientPatrolNode.ScriptID = this.oldValue;
				return true;
			}
			return false;
		}

		// Token: 0x060003A1 RID: 929 RVA: 0x0001FF45 File Offset: 0x0001EF45
		public bool Redo()
		{
			if (!this.IsEmpty)
			{
				this.clientPatrolNode.ScriptID = this.newValue;
				return true;
			}
			return false;
		}

		// Token: 0x060003A2 RID: 930 RVA: 0x0001FF63 File Offset: 0x0001EF63
		public void Destroy()
		{
			this.clientPatrolNode = null;
		}

		// Token: 0x1700007D RID: 125
		// (get) Token: 0x060003A3 RID: 931 RVA: 0x0001FF6C File Offset: 0x0001EF6C
		public bool IsEmpty
		{
			get
			{
				return this.clientPatrolNode == null;
			}
		}

		// Token: 0x1700007E RID: 126
		// (get) Token: 0x060003A4 RID: 932 RVA: 0x0001FF77 File Offset: 0x0001EF77
		public bool IsAbsolute
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700007F RID: 127
		// (get) Token: 0x060003A5 RID: 933 RVA: 0x0001FF7A File Offset: 0x0001EF7A
		public bool IsHighPriority
		{
			get
			{
				return false;
			}
		}

		// Token: 0x060003A6 RID: 934 RVA: 0x0001FF7D File Offset: 0x0001EF7D
		public bool ContainsLabel(string label)
		{
			return label == this.Label;
		}

		// Token: 0x17000080 RID: 128
		// (get) Token: 0x060003A7 RID: 935 RVA: 0x0001FF8B File Offset: 0x0001EF8B
		public string Label
		{
			get
			{
				return "MapObjectContainer";
			}
		}

		// Token: 0x17000081 RID: 129
		// (get) Token: 0x060003A8 RID: 936 RVA: 0x0001FF92 File Offset: 0x0001EF92
		public string Description
		{
			get
			{
				return "ChangeClientPatrolNodeScriptIDOperation";
			}
		}

		// Token: 0x0400029C RID: 668
		private ClientPatrolNode clientPatrolNode;

		// Token: 0x0400029D RID: 669
		private readonly string oldValue;

		// Token: 0x0400029E RID: 670
		private readonly string newValue;
	}
}
