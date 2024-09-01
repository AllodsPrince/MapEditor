using System;
using Operations;

namespace MapEditor.Map.MapObjects
{
	// Token: 0x020002C4 RID: 708
	internal class ChangePlayerRespawnPlaceDeviceOperation : IOperation
	{
		// Token: 0x060020F3 RID: 8435 RVA: 0x000D1ADB File Offset: 0x000D0ADB
		public ChangePlayerRespawnPlaceDeviceOperation(PlayerRespawnPlace _playerRespawnPlace, ref string _oldValue, ref string _newValue)
		{
			this.playerRespawnPlace = _playerRespawnPlace;
			this.oldValue = _oldValue;
			this.newValue = _newValue;
		}

		// Token: 0x060020F4 RID: 8436 RVA: 0x000D1AFA File Offset: 0x000D0AFA
		public bool Undo()
		{
			if (!this.IsEmpty)
			{
				this.playerRespawnPlace.Device = this.oldValue;
				return true;
			}
			return false;
		}

		// Token: 0x060020F5 RID: 8437 RVA: 0x000D1B18 File Offset: 0x000D0B18
		public bool Redo()
		{
			if (!this.IsEmpty)
			{
				this.playerRespawnPlace.Device = this.newValue;
				return true;
			}
			return false;
		}

		// Token: 0x060020F6 RID: 8438 RVA: 0x000D1B36 File Offset: 0x000D0B36
		public void Destroy()
		{
			this.playerRespawnPlace = null;
		}

		// Token: 0x17000760 RID: 1888
		// (get) Token: 0x060020F7 RID: 8439 RVA: 0x000D1B3F File Offset: 0x000D0B3F
		public bool IsEmpty
		{
			get
			{
				return this.playerRespawnPlace == null;
			}
		}

		// Token: 0x17000761 RID: 1889
		// (get) Token: 0x060020F8 RID: 8440 RVA: 0x000D1B4A File Offset: 0x000D0B4A
		public bool IsAbsolute
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000762 RID: 1890
		// (get) Token: 0x060020F9 RID: 8441 RVA: 0x000D1B4D File Offset: 0x000D0B4D
		public bool IsHighPriority
		{
			get
			{
				return false;
			}
		}

		// Token: 0x060020FA RID: 8442 RVA: 0x000D1B50 File Offset: 0x000D0B50
		public bool ContainsLabel(string label)
		{
			return label == this.Label;
		}

		// Token: 0x17000763 RID: 1891
		// (get) Token: 0x060020FB RID: 8443 RVA: 0x000D1B5E File Offset: 0x000D0B5E
		public string Label
		{
			get
			{
				return "MapObjectContainer";
			}
		}

		// Token: 0x17000764 RID: 1892
		// (get) Token: 0x060020FC RID: 8444 RVA: 0x000D1B65 File Offset: 0x000D0B65
		public string Description
		{
			get
			{
				return "ChangePlayerRespawnPlaceDeviceOperation";
			}
		}

		// Token: 0x0400140A RID: 5130
		private PlayerRespawnPlace playerRespawnPlace;

		// Token: 0x0400140B RID: 5131
		private readonly string oldValue;

		// Token: 0x0400140C RID: 5132
		private readonly string newValue;
	}
}
