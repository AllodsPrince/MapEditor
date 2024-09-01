using System;
using Operations;

namespace MapEditor.Map.MapObjects
{
	// Token: 0x02000034 RID: 52
	internal class ChangeFactionOperation : IOperation
	{
		// Token: 0x0600030C RID: 780 RVA: 0x0001E1FE File Offset: 0x0001D1FE
		public ChangeFactionOperation(Respawn _respawn, ref Faction _oldValue, ref Faction _newValue)
		{
			this.respawn = _respawn;
			this.oldValue = _oldValue;
			this.newValue = _newValue;
		}

		// Token: 0x0600030D RID: 781 RVA: 0x0001E21D File Offset: 0x0001D21D
		public bool Undo()
		{
			if (!this.IsEmpty)
			{
				this.respawn.Faction = this.oldValue;
				return true;
			}
			return false;
		}

		// Token: 0x0600030E RID: 782 RVA: 0x0001E23B File Offset: 0x0001D23B
		public bool Redo()
		{
			if (!this.IsEmpty)
			{
				this.respawn.Faction = this.newValue;
				return true;
			}
			return false;
		}

		// Token: 0x0600030F RID: 783 RVA: 0x0001E259 File Offset: 0x0001D259
		public void Destroy()
		{
			this.respawn = null;
		}

		// Token: 0x17000045 RID: 69
		// (get) Token: 0x06000310 RID: 784 RVA: 0x0001E262 File Offset: 0x0001D262
		public bool IsEmpty
		{
			get
			{
				return this.respawn == null;
			}
		}

		// Token: 0x17000046 RID: 70
		// (get) Token: 0x06000311 RID: 785 RVA: 0x0001E26D File Offset: 0x0001D26D
		public bool IsAbsolute
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000047 RID: 71
		// (get) Token: 0x06000312 RID: 786 RVA: 0x0001E270 File Offset: 0x0001D270
		public bool IsHighPriority
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06000313 RID: 787 RVA: 0x0001E273 File Offset: 0x0001D273
		public bool ContainsLabel(string label)
		{
			return label == this.Label;
		}

		// Token: 0x17000048 RID: 72
		// (get) Token: 0x06000314 RID: 788 RVA: 0x0001E281 File Offset: 0x0001D281
		public string Label
		{
			get
			{
				return "MapObjectContainer";
			}
		}

		// Token: 0x17000049 RID: 73
		// (get) Token: 0x06000315 RID: 789 RVA: 0x0001E288 File Offset: 0x0001D288
		public string Description
		{
			get
			{
				return "ChangeFactionOperation";
			}
		}

		// Token: 0x04000265 RID: 613
		private Respawn respawn;

		// Token: 0x04000266 RID: 614
		private readonly Faction oldValue;

		// Token: 0x04000267 RID: 615
		private readonly Faction newValue;
	}
}
