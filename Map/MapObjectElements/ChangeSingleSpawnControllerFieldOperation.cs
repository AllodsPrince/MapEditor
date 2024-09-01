using System;
using Operations;

namespace MapEditor.Map.MapObjectElements
{
	// Token: 0x020002CA RID: 714
	internal class ChangeSingleSpawnControllerFieldOperation : IOperation
	{
		// Token: 0x06002124 RID: 8484 RVA: 0x000D206E File Offset: 0x000D106E
		private void SetValue(string value)
		{
			this.sender.DbObject = value;
		}

		// Token: 0x06002125 RID: 8485 RVA: 0x000D207C File Offset: 0x000D107C
		public ChangeSingleSpawnControllerFieldOperation(SingleSpawnController _sender, string _oldValue, string _newValue)
		{
			this.sender = _sender;
			this.oldValue = _oldValue;
			this.newValue = _newValue;
		}

		// Token: 0x06002126 RID: 8486 RVA: 0x000D2099 File Offset: 0x000D1099
		public bool Undo()
		{
			if (!this.IsEmpty)
			{
				this.SetValue(this.oldValue);
				return true;
			}
			return false;
		}

		// Token: 0x06002127 RID: 8487 RVA: 0x000D20B2 File Offset: 0x000D10B2
		public bool Redo()
		{
			if (!this.IsEmpty)
			{
				this.SetValue(this.newValue);
				return true;
			}
			return false;
		}

		// Token: 0x06002128 RID: 8488 RVA: 0x000D20CB File Offset: 0x000D10CB
		public void Destroy()
		{
			this.sender = null;
		}

		// Token: 0x1700076B RID: 1899
		// (get) Token: 0x06002129 RID: 8489 RVA: 0x000D20D4 File Offset: 0x000D10D4
		public bool IsEmpty
		{
			get
			{
				return this.sender == null;
			}
		}

		// Token: 0x1700076C RID: 1900
		// (get) Token: 0x0600212A RID: 8490 RVA: 0x000D20DF File Offset: 0x000D10DF
		public bool IsAbsolute
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700076D RID: 1901
		// (get) Token: 0x0600212B RID: 8491 RVA: 0x000D20E2 File Offset: 0x000D10E2
		public bool IsHighPriority
		{
			get
			{
				return false;
			}
		}

		// Token: 0x0600212C RID: 8492 RVA: 0x000D20E5 File Offset: 0x000D10E5
		public bool ContainsLabel(string label)
		{
			return label == this.Label;
		}

		// Token: 0x1700076E RID: 1902
		// (get) Token: 0x0600212D RID: 8493 RVA: 0x000D20F3 File Offset: 0x000D10F3
		public string Label
		{
			get
			{
				return "MapObjectContainer";
			}
		}

		// Token: 0x1700076F RID: 1903
		// (get) Token: 0x0600212E RID: 8494 RVA: 0x000D20FA File Offset: 0x000D10FA
		public string Description
		{
			get
			{
				return "ChangeSingleSpawnControllerFieldOperation";
			}
		}

		// Token: 0x04001418 RID: 5144
		private SingleSpawnController sender;

		// Token: 0x04001419 RID: 5145
		private readonly string oldValue;

		// Token: 0x0400141A RID: 5146
		private readonly string newValue;
	}
}
