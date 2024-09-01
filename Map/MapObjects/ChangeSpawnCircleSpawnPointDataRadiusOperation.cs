using System;
using Operations;

namespace MapEditor.Map.MapObjects
{
	// Token: 0x02000151 RID: 337
	internal class ChangeSpawnCircleSpawnPointDataRadiusOperation : IOperation
	{
		// Token: 0x06001042 RID: 4162 RVA: 0x0007AD11 File Offset: 0x00079D11
		public ChangeSpawnCircleSpawnPointDataRadiusOperation(SpawnPoint _spawnPoint, ref double _oldValue, ref double _newValue)
		{
			this.spawnPoint = _spawnPoint;
			this.oldValue = _oldValue;
			this.newValue = _newValue;
		}

		// Token: 0x06001043 RID: 4163 RVA: 0x0007AD30 File Offset: 0x00079D30
		public bool Undo()
		{
			if (!this.IsEmpty)
			{
				SpawnCircleSpawnPointData spawnCircleSpawnPointData = this.spawnPoint.SpawnPointData as SpawnCircleSpawnPointData;
				if (spawnCircleSpawnPointData != null)
				{
					spawnCircleSpawnPointData.Radius = this.oldValue;
					return true;
				}
			}
			return false;
		}

		// Token: 0x06001044 RID: 4164 RVA: 0x0007AD68 File Offset: 0x00079D68
		public bool Redo()
		{
			if (!this.IsEmpty)
			{
				SpawnCircleSpawnPointData spawnCircleSpawnPointData = this.spawnPoint.SpawnPointData as SpawnCircleSpawnPointData;
				if (spawnCircleSpawnPointData != null)
				{
					spawnCircleSpawnPointData.Radius = this.newValue;
					return true;
				}
			}
			return false;
		}

		// Token: 0x06001045 RID: 4165 RVA: 0x0007ADA0 File Offset: 0x00079DA0
		public void Destroy()
		{
			this.spawnPoint = null;
		}

		// Token: 0x1700035A RID: 858
		// (get) Token: 0x06001046 RID: 4166 RVA: 0x0007ADA9 File Offset: 0x00079DA9
		public bool IsEmpty
		{
			get
			{
				return this.spawnPoint == null || !(this.spawnPoint.SpawnPointData is SpawnCircleSpawnPointData);
			}
		}

		// Token: 0x1700035B RID: 859
		// (get) Token: 0x06001047 RID: 4167 RVA: 0x0007ADCB File Offset: 0x00079DCB
		public bool IsAbsolute
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700035C RID: 860
		// (get) Token: 0x06001048 RID: 4168 RVA: 0x0007ADCE File Offset: 0x00079DCE
		public bool IsHighPriority
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06001049 RID: 4169 RVA: 0x0007ADD1 File Offset: 0x00079DD1
		public bool ContainsLabel(string label)
		{
			return label == this.Label;
		}

		// Token: 0x1700035D RID: 861
		// (get) Token: 0x0600104A RID: 4170 RVA: 0x0007ADDF File Offset: 0x00079DDF
		public string Label
		{
			get
			{
				return "MapObjectContainer";
			}
		}

		// Token: 0x1700035E RID: 862
		// (get) Token: 0x0600104B RID: 4171 RVA: 0x0007ADE6 File Offset: 0x00079DE6
		public string Description
		{
			get
			{
				return "ChangeSpawnCircleSpawnPointDataRadiusOperation";
			}
		}

		// Token: 0x04000C18 RID: 3096
		private SpawnPoint spawnPoint;

		// Token: 0x04000C19 RID: 3097
		private readonly double oldValue;

		// Token: 0x04000C1A RID: 3098
		private readonly double newValue;
	}
}
