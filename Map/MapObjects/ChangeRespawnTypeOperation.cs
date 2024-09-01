using System;
using Operations;

namespace MapEditor.Map.MapObjects
{
	// Token: 0x02000033 RID: 51
	internal class ChangeRespawnTypeOperation : IOperation
	{
		// Token: 0x06000302 RID: 770 RVA: 0x0001E16D File Offset: 0x0001D16D
		public ChangeRespawnTypeOperation(Respawn _respawn, ref RespawnType _oldValue, ref RespawnType _newValue)
		{
			this.respawn = _respawn;
			this.oldValue = _oldValue;
			this.newValue = _newValue;
		}

		// Token: 0x06000303 RID: 771 RVA: 0x0001E18C File Offset: 0x0001D18C
		public bool Undo()
		{
			if (!this.IsEmpty)
			{
				this.respawn.RespawnType = this.oldValue;
				return true;
			}
			return false;
		}

		// Token: 0x06000304 RID: 772 RVA: 0x0001E1AA File Offset: 0x0001D1AA
		public bool Redo()
		{
			if (!this.IsEmpty)
			{
				this.respawn.RespawnType = this.newValue;
				return true;
			}
			return false;
		}

		// Token: 0x06000305 RID: 773 RVA: 0x0001E1C8 File Offset: 0x0001D1C8
		public void Destroy()
		{
			this.respawn = null;
		}

		// Token: 0x17000040 RID: 64
		// (get) Token: 0x06000306 RID: 774 RVA: 0x0001E1D1 File Offset: 0x0001D1D1
		public bool IsEmpty
		{
			get
			{
				return this.respawn == null;
			}
		}

		// Token: 0x17000041 RID: 65
		// (get) Token: 0x06000307 RID: 775 RVA: 0x0001E1DC File Offset: 0x0001D1DC
		public bool IsAbsolute
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000042 RID: 66
		// (get) Token: 0x06000308 RID: 776 RVA: 0x0001E1DF File Offset: 0x0001D1DF
		public bool IsHighPriority
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06000309 RID: 777 RVA: 0x0001E1E2 File Offset: 0x0001D1E2
		public bool ContainsLabel(string label)
		{
			return label == this.Label;
		}

		// Token: 0x17000043 RID: 67
		// (get) Token: 0x0600030A RID: 778 RVA: 0x0001E1F0 File Offset: 0x0001D1F0
		public string Label
		{
			get
			{
				return "MapObjectContainer";
			}
		}

		// Token: 0x17000044 RID: 68
		// (get) Token: 0x0600030B RID: 779 RVA: 0x0001E1F7 File Offset: 0x0001D1F7
		public string Description
		{
			get
			{
				return "ChangeRespawnTypeOperation";
			}
		}

		// Token: 0x04000262 RID: 610
		private Respawn respawn;

		// Token: 0x04000263 RID: 611
		private readonly RespawnType oldValue;

		// Token: 0x04000264 RID: 612
		private readonly RespawnType newValue;
	}
}
