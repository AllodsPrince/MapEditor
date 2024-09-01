using System;
using Operations;

namespace MapEditor.Map.MapObjects
{
	// Token: 0x02000032 RID: 50
	internal class ChangeRespawnMapZoneOperation : IOperation
	{
		// Token: 0x060002F8 RID: 760 RVA: 0x0001E0DC File Offset: 0x0001D0DC
		public ChangeRespawnMapZoneOperation(Respawn _respawn, ref string _oldValue, ref string _newValue)
		{
			this.respawn = _respawn;
			this.oldValue = _oldValue;
			this.newValue = _newValue;
		}

		// Token: 0x060002F9 RID: 761 RVA: 0x0001E0FB File Offset: 0x0001D0FB
		public bool Undo()
		{
			if (!this.IsEmpty)
			{
				this.respawn.MapZone = this.oldValue;
				return true;
			}
			return false;
		}

		// Token: 0x060002FA RID: 762 RVA: 0x0001E119 File Offset: 0x0001D119
		public bool Redo()
		{
			if (!this.IsEmpty)
			{
				this.respawn.MapZone = this.newValue;
				return true;
			}
			return false;
		}

		// Token: 0x060002FB RID: 763 RVA: 0x0001E137 File Offset: 0x0001D137
		public void Destroy()
		{
			this.respawn = null;
		}

		// Token: 0x1700003B RID: 59
		// (get) Token: 0x060002FC RID: 764 RVA: 0x0001E140 File Offset: 0x0001D140
		public bool IsEmpty
		{
			get
			{
				return this.respawn == null;
			}
		}

		// Token: 0x1700003C RID: 60
		// (get) Token: 0x060002FD RID: 765 RVA: 0x0001E14B File Offset: 0x0001D14B
		public bool IsAbsolute
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700003D RID: 61
		// (get) Token: 0x060002FE RID: 766 RVA: 0x0001E14E File Offset: 0x0001D14E
		public bool IsHighPriority
		{
			get
			{
				return false;
			}
		}

		// Token: 0x060002FF RID: 767 RVA: 0x0001E151 File Offset: 0x0001D151
		public bool ContainsLabel(string label)
		{
			return label == this.Label;
		}

		// Token: 0x1700003E RID: 62
		// (get) Token: 0x06000300 RID: 768 RVA: 0x0001E15F File Offset: 0x0001D15F
		public string Label
		{
			get
			{
				return "MapObjectContainer";
			}
		}

		// Token: 0x1700003F RID: 63
		// (get) Token: 0x06000301 RID: 769 RVA: 0x0001E166 File Offset: 0x0001D166
		public string Description
		{
			get
			{
				return "ChangeRespanwMapZoneOperation";
			}
		}

		// Token: 0x0400025F RID: 607
		private Respawn respawn;

		// Token: 0x04000260 RID: 608
		private readonly string oldValue;

		// Token: 0x04000261 RID: 609
		private readonly string newValue;
	}
}
