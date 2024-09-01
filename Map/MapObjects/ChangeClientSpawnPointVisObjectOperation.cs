using System;
using Operations;

namespace MapEditor.Map.MapObjects
{
	// Token: 0x0200022E RID: 558
	internal class ChangeClientSpawnPointVisObjectOperation : IOperation
	{
		// Token: 0x06001AC2 RID: 6850 RVA: 0x000AEFD7 File Offset: 0x000ADFD7
		public ChangeClientSpawnPointVisObjectOperation(ClientSpawnPoint _clientSpawnPoint, ref string _oldValue, ref string _newValue)
		{
			this.clientSpawnPoint = _clientSpawnPoint;
			this.oldValue = _oldValue;
			this.newValue = _newValue;
		}

		// Token: 0x06001AC3 RID: 6851 RVA: 0x000AEFF6 File Offset: 0x000ADFF6
		public bool Undo()
		{
			if (!this.IsEmpty)
			{
				this.clientSpawnPoint.VisObject = this.oldValue;
				return true;
			}
			return false;
		}

		// Token: 0x06001AC4 RID: 6852 RVA: 0x000AF014 File Offset: 0x000AE014
		public bool Redo()
		{
			if (!this.IsEmpty)
			{
				this.clientSpawnPoint.VisObject = this.newValue;
				return true;
			}
			return false;
		}

		// Token: 0x06001AC5 RID: 6853 RVA: 0x000AF032 File Offset: 0x000AE032
		public void Destroy()
		{
			this.clientSpawnPoint = null;
		}

		// Token: 0x17000654 RID: 1620
		// (get) Token: 0x06001AC6 RID: 6854 RVA: 0x000AF03B File Offset: 0x000AE03B
		public bool IsEmpty
		{
			get
			{
				return this.clientSpawnPoint == null;
			}
		}

		// Token: 0x17000655 RID: 1621
		// (get) Token: 0x06001AC7 RID: 6855 RVA: 0x000AF046 File Offset: 0x000AE046
		public bool IsAbsolute
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000656 RID: 1622
		// (get) Token: 0x06001AC8 RID: 6856 RVA: 0x000AF049 File Offset: 0x000AE049
		public bool IsHighPriority
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06001AC9 RID: 6857 RVA: 0x000AF04C File Offset: 0x000AE04C
		public bool ContainsLabel(string label)
		{
			return label == this.Label;
		}

		// Token: 0x17000657 RID: 1623
		// (get) Token: 0x06001ACA RID: 6858 RVA: 0x000AF05A File Offset: 0x000AE05A
		public string Label
		{
			get
			{
				return "MapObjectContainer";
			}
		}

		// Token: 0x17000658 RID: 1624
		// (get) Token: 0x06001ACB RID: 6859 RVA: 0x000AF061 File Offset: 0x000AE061
		public string Description
		{
			get
			{
				return "ChangeClientSpawnPointVisObjectOperation";
			}
		}

		// Token: 0x0400113B RID: 4411
		private ClientSpawnPoint clientSpawnPoint;

		// Token: 0x0400113C RID: 4412
		private readonly string oldValue;

		// Token: 0x0400113D RID: 4413
		private readonly string newValue;
	}
}
