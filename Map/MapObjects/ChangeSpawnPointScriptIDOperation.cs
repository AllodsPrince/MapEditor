using System;
using Operations;

namespace MapEditor.Map.MapObjects
{
	// Token: 0x02000146 RID: 326
	internal class ChangeSpawnPointScriptIDOperation : IOperation
	{
		// Token: 0x06000FD4 RID: 4052 RVA: 0x0007A55E File Offset: 0x0007955E
		public ChangeSpawnPointScriptIDOperation(SpawnPoint _spawnPoint, ref string _oldValue, ref string _newValue)
		{
			this.spawnPoint = _spawnPoint;
			this.oldValue = _oldValue;
			this.newValue = _newValue;
		}

		// Token: 0x06000FD5 RID: 4053 RVA: 0x0007A57D File Offset: 0x0007957D
		public bool Undo()
		{
			if (!this.IsEmpty)
			{
				this.spawnPoint.ScriptID = this.oldValue;
				return true;
			}
			return false;
		}

		// Token: 0x06000FD6 RID: 4054 RVA: 0x0007A59B File Offset: 0x0007959B
		public bool Redo()
		{
			if (!this.IsEmpty)
			{
				this.spawnPoint.ScriptID = this.newValue;
				return true;
			}
			return false;
		}

		// Token: 0x06000FD7 RID: 4055 RVA: 0x0007A5B9 File Offset: 0x000795B9
		public void Destroy()
		{
			this.spawnPoint = null;
		}

		// Token: 0x17000323 RID: 803
		// (get) Token: 0x06000FD8 RID: 4056 RVA: 0x0007A5C2 File Offset: 0x000795C2
		public bool IsEmpty
		{
			get
			{
				return this.spawnPoint == null;
			}
		}

		// Token: 0x17000324 RID: 804
		// (get) Token: 0x06000FD9 RID: 4057 RVA: 0x0007A5CD File Offset: 0x000795CD
		public bool IsAbsolute
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000325 RID: 805
		// (get) Token: 0x06000FDA RID: 4058 RVA: 0x0007A5D0 File Offset: 0x000795D0
		public bool IsHighPriority
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06000FDB RID: 4059 RVA: 0x0007A5D3 File Offset: 0x000795D3
		public bool ContainsLabel(string label)
		{
			return label == this.Label;
		}

		// Token: 0x17000326 RID: 806
		// (get) Token: 0x06000FDC RID: 4060 RVA: 0x0007A5E1 File Offset: 0x000795E1
		public string Label
		{
			get
			{
				return "MapObjectContainer";
			}
		}

		// Token: 0x17000327 RID: 807
		// (get) Token: 0x06000FDD RID: 4061 RVA: 0x0007A5E8 File Offset: 0x000795E8
		public string Description
		{
			get
			{
				return "ChangeSpawnPointScriptIDOperation";
			}
		}

		// Token: 0x04000BF7 RID: 3063
		private SpawnPoint spawnPoint;

		// Token: 0x04000BF8 RID: 3064
		private readonly string oldValue;

		// Token: 0x04000BF9 RID: 3065
		private readonly string newValue;
	}
}
