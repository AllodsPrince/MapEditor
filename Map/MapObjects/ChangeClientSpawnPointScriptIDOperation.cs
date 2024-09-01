using System;
using Operations;

namespace MapEditor.Map.MapObjects
{
	// Token: 0x02000230 RID: 560
	internal class ChangeClientSpawnPointScriptIDOperation : IOperation
	{
		// Token: 0x06001AD6 RID: 6870 RVA: 0x000AF0F9 File Offset: 0x000AE0F9
		public ChangeClientSpawnPointScriptIDOperation(ClientSpawnPoint _clientSpawnPoint, ref string _oldValue, ref string _newValue)
		{
			this.clientSpawnPoint = _clientSpawnPoint;
			this.oldValue = _oldValue;
			this.newValue = _newValue;
		}

		// Token: 0x06001AD7 RID: 6871 RVA: 0x000AF118 File Offset: 0x000AE118
		public bool Undo()
		{
			if (!this.IsEmpty)
			{
				this.clientSpawnPoint.ScriptID = this.oldValue;
				return true;
			}
			return false;
		}

		// Token: 0x06001AD8 RID: 6872 RVA: 0x000AF136 File Offset: 0x000AE136
		public bool Redo()
		{
			if (!this.IsEmpty)
			{
				this.clientSpawnPoint.ScriptID = this.newValue;
				return true;
			}
			return false;
		}

		// Token: 0x06001AD9 RID: 6873 RVA: 0x000AF154 File Offset: 0x000AE154
		public void Destroy()
		{
			this.clientSpawnPoint = null;
		}

		// Token: 0x1700065E RID: 1630
		// (get) Token: 0x06001ADA RID: 6874 RVA: 0x000AF15D File Offset: 0x000AE15D
		public bool IsEmpty
		{
			get
			{
				return this.clientSpawnPoint == null;
			}
		}

		// Token: 0x1700065F RID: 1631
		// (get) Token: 0x06001ADB RID: 6875 RVA: 0x000AF168 File Offset: 0x000AE168
		public bool IsAbsolute
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000660 RID: 1632
		// (get) Token: 0x06001ADC RID: 6876 RVA: 0x000AF16B File Offset: 0x000AE16B
		public bool IsHighPriority
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06001ADD RID: 6877 RVA: 0x000AF16E File Offset: 0x000AE16E
		public bool ContainsLabel(string label)
		{
			return label == this.Label;
		}

		// Token: 0x17000661 RID: 1633
		// (get) Token: 0x06001ADE RID: 6878 RVA: 0x000AF17C File Offset: 0x000AE17C
		public string Label
		{
			get
			{
				return "MapObjectContainer";
			}
		}

		// Token: 0x17000662 RID: 1634
		// (get) Token: 0x06001ADF RID: 6879 RVA: 0x000AF183 File Offset: 0x000AE183
		public string Description
		{
			get
			{
				return "ChangeClientSpawnPointScriptIDOperation";
			}
		}

		// Token: 0x04001141 RID: 4417
		private ClientSpawnPoint clientSpawnPoint;

		// Token: 0x04001142 RID: 4418
		private readonly string oldValue;

		// Token: 0x04001143 RID: 4419
		private readonly string newValue;
	}
}
