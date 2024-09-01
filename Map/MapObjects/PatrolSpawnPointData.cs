using System;

namespace MapEditor.Map.MapObjects
{
	// Token: 0x02000138 RID: 312
	public class PatrolSpawnPointData : SpawnPointData
	{
		// Token: 0x14000044 RID: 68
		// (add) Token: 0x06000F01 RID: 3841 RVA: 0x00077EDF File Offset: 0x00076EDF
		// (remove) Token: 0x06000F02 RID: 3842 RVA: 0x00077EF6 File Offset: 0x00076EF6
		public static event PatrolSpawnPointData.PatrolSpawnPointDataFieldChangedEvent<string> LabelChanged;

		// Token: 0x14000045 RID: 69
		// (add) Token: 0x06000F03 RID: 3843 RVA: 0x00077F0D File Offset: 0x00076F0D
		// (remove) Token: 0x06000F04 RID: 3844 RVA: 0x00077F24 File Offset: 0x00076F24
		public static event PatrolSpawnPointData.PatrolSpawnPointDataFieldChangedEvent<string> ScriptChanged;

		// Token: 0x06000F05 RID: 3845 RVA: 0x00077F3B File Offset: 0x00076F3B
		public PatrolSpawnPointData() : base(null, SpawnPointType.Patrol)
		{
		}

		// Token: 0x06000F06 RID: 3846 RVA: 0x00077F5B File Offset: 0x00076F5B
		public PatrolSpawnPointData(SpawnPoint _spawnPoint) : base(_spawnPoint, SpawnPointType.Patrol)
		{
		}

		// Token: 0x170002DC RID: 732
		// (get) Token: 0x06000F07 RID: 3847 RVA: 0x00077F7B File Offset: 0x00076F7B
		// (set) Token: 0x06000F08 RID: 3848 RVA: 0x00077F84 File Offset: 0x00076F84
		public string Label
		{
			get
			{
				return this.label;
			}
			set
			{
				if (this.label != value)
				{
					if (base.SpawnPoint != null && base.SpawnPoint.Active && !base.SpawnPoint.InvokeChanging(null))
					{
						return;
					}
					string oldLabel = this.label;
					this.label = value;
					if (base.SpawnPoint != null && base.SpawnPoint.Active)
					{
						base.SpawnPoint.InvokeChanged();
						base.InvokeChanged();
						if (PatrolSpawnPointData.LabelChanged != null)
						{
							PatrolSpawnPointData.LabelChanged(base.SpawnPoint, ref oldLabel, ref this.label);
						}
					}
				}
			}
		}

		// Token: 0x170002DD RID: 733
		// (get) Token: 0x06000F09 RID: 3849 RVA: 0x00078016 File Offset: 0x00077016
		// (set) Token: 0x06000F0A RID: 3850 RVA: 0x00078020 File Offset: 0x00077020
		public string Script
		{
			get
			{
				return this.script;
			}
			set
			{
				if (this.script != value)
				{
					if (base.SpawnPoint != null && base.SpawnPoint.Active && !base.SpawnPoint.InvokeChanging(null))
					{
						return;
					}
					string oldScript = this.script;
					this.script = value;
					if (base.SpawnPoint != null && base.SpawnPoint.Active)
					{
						base.SpawnPoint.InvokeChanged();
						base.InvokeChanged();
						if (PatrolSpawnPointData.ScriptChanged != null)
						{
							PatrolSpawnPointData.ScriptChanged(base.SpawnPoint, ref oldScript, ref this.script);
						}
					}
				}
			}
		}

		// Token: 0x06000F0B RID: 3851 RVA: 0x000780B4 File Offset: 0x000770B4
		public override void CopyFrom(SpawnPointData spawnPointData)
		{
			if (spawnPointData != null && spawnPointData.SpawnPointType == SpawnPointType.Patrol)
			{
				PatrolSpawnPointData patrolSpawnPointData = spawnPointData as PatrolSpawnPointData;
				if (patrolSpawnPointData != null)
				{
					this.script = patrolSpawnPointData.script;
				}
			}
		}

		// Token: 0x04000B9B RID: 2971
		private string label = string.Empty;

		// Token: 0x04000B9C RID: 2972
		private string script = string.Empty;

		// Token: 0x02000139 RID: 313
		// (Invoke) Token: 0x06000F0D RID: 3853
		public delegate void PatrolSpawnPointDataFieldChangedEvent<T>(SpawnPoint spawnPoint, ref T oldValue, ref T newValue);
	}
}
