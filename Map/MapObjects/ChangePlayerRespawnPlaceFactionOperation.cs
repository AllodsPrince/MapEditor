using System;
using Operations;

namespace MapEditor.Map.MapObjects
{
	// Token: 0x020002C5 RID: 709
	internal class ChangePlayerRespawnPlaceFactionOperation : IOperation
	{
		// Token: 0x060020FD RID: 8445 RVA: 0x000D1B6C File Offset: 0x000D0B6C
		public ChangePlayerRespawnPlaceFactionOperation(PlayerRespawnPlace _playerRespawnPlace, ref string _oldValue, ref string _newValue)
		{
			this.playerRespawnPlace = _playerRespawnPlace;
			this.oldValue = _oldValue;
			this.newValue = _newValue;
		}

		// Token: 0x060020FE RID: 8446 RVA: 0x000D1B8B File Offset: 0x000D0B8B
		public bool Undo()
		{
			if (!this.IsEmpty)
			{
				this.playerRespawnPlace.Faction = this.oldValue;
				return true;
			}
			return false;
		}

		// Token: 0x060020FF RID: 8447 RVA: 0x000D1BA9 File Offset: 0x000D0BA9
		public bool Redo()
		{
			if (!this.IsEmpty)
			{
				this.playerRespawnPlace.Faction = this.newValue;
				return true;
			}
			return false;
		}

		// Token: 0x06002100 RID: 8448 RVA: 0x000D1BC7 File Offset: 0x000D0BC7
		public void Destroy()
		{
			this.playerRespawnPlace = null;
		}

		// Token: 0x17000765 RID: 1893
		// (get) Token: 0x06002101 RID: 8449 RVA: 0x000D1BD0 File Offset: 0x000D0BD0
		public bool IsEmpty
		{
			get
			{
				return this.playerRespawnPlace == null;
			}
		}

		// Token: 0x17000766 RID: 1894
		// (get) Token: 0x06002102 RID: 8450 RVA: 0x000D1BDB File Offset: 0x000D0BDB
		public bool IsAbsolute
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000767 RID: 1895
		// (get) Token: 0x06002103 RID: 8451 RVA: 0x000D1BDE File Offset: 0x000D0BDE
		public bool IsHighPriority
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06002104 RID: 8452 RVA: 0x000D1BE1 File Offset: 0x000D0BE1
		public bool ContainsLabel(string label)
		{
			return label == this.Label;
		}

		// Token: 0x17000768 RID: 1896
		// (get) Token: 0x06002105 RID: 8453 RVA: 0x000D1BEF File Offset: 0x000D0BEF
		public string Label
		{
			get
			{
				return "MapObjectContainer";
			}
		}

		// Token: 0x17000769 RID: 1897
		// (get) Token: 0x06002106 RID: 8454 RVA: 0x000D1BF6 File Offset: 0x000D0BF6
		public string Description
		{
			get
			{
				return "ChangePlayerRespawnPlaceFactionOperation";
			}
		}

		// Token: 0x0400140D RID: 5133
		private PlayerRespawnPlace playerRespawnPlace;

		// Token: 0x0400140E RID: 5134
		private readonly string oldValue;

		// Token: 0x0400140F RID: 5135
		private readonly string newValue;
	}
}
