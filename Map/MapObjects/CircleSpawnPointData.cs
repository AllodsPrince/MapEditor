using System;

namespace MapEditor.Map.MapObjects
{
	// Token: 0x02000134 RID: 308
	public class CircleSpawnPointData : SpawnPointData
	{
		// Token: 0x14000041 RID: 65
		// (add) Token: 0x06000EE3 RID: 3811 RVA: 0x00077B36 File Offset: 0x00076B36
		// (remove) Token: 0x06000EE4 RID: 3812 RVA: 0x00077B4D File Offset: 0x00076B4D
		public static event CircleSpawnPointData.CircleSpawnPointDataFieldChangedEvent<double> RadiusChanged;

		// Token: 0x06000EE5 RID: 3813 RVA: 0x00077B64 File Offset: 0x00076B64
		public CircleSpawnPointData() : base(null, SpawnPointType.Circle)
		{
		}

		// Token: 0x06000EE6 RID: 3814 RVA: 0x00077B7D File Offset: 0x00076B7D
		public CircleSpawnPointData(SpawnPoint _spawnPoint) : base(_spawnPoint, SpawnPointType.Circle)
		{
		}

		// Token: 0x06000EE7 RID: 3815 RVA: 0x00077B96 File Offset: 0x00076B96
		public CircleSpawnPointData(SpawnPoint _spawnPoint, double _radius) : base(_spawnPoint, SpawnPointType.Circle)
		{
			this.radius = _radius;
		}

		// Token: 0x170002D7 RID: 727
		// (get) Token: 0x06000EE8 RID: 3816 RVA: 0x00077BB6 File Offset: 0x00076BB6
		public static double DefaultRadius
		{
			get
			{
				return 16.0;
			}
		}

		// Token: 0x170002D8 RID: 728
		// (get) Token: 0x06000EE9 RID: 3817 RVA: 0x00077BC1 File Offset: 0x00076BC1
		// (set) Token: 0x06000EEA RID: 3818 RVA: 0x00077BCC File Offset: 0x00076BCC
		public double Radius
		{
			get
			{
				return this.radius;
			}
			set
			{
				if (this.radius != value)
				{
					if (base.SpawnPoint != null && base.SpawnPoint.Active)
					{
						base.SpawnPoint.InvokeChanging(null);
					}
					double oldRadius = this.radius;
					this.radius = value;
					if (base.SpawnPoint != null && base.SpawnPoint.Active)
					{
						base.SpawnPoint.InvokeChanged();
						base.InvokeChanged();
						if (CircleSpawnPointData.RadiusChanged != null)
						{
							CircleSpawnPointData.RadiusChanged(base.SpawnPoint, ref oldRadius, ref this.radius);
						}
					}
				}
			}
		}

		// Token: 0x06000EEB RID: 3819 RVA: 0x00077C58 File Offset: 0x00076C58
		public override void CopyFrom(SpawnPointData spawnPointData)
		{
			if (spawnPointData != null && spawnPointData.SpawnPointType == SpawnPointType.Circle)
			{
				CircleSpawnPointData circleSpawnPointData = spawnPointData as CircleSpawnPointData;
				if (circleSpawnPointData != null)
				{
					this.radius = circleSpawnPointData.radius;
				}
			}
		}

		// Token: 0x04000B93 RID: 2963
		private const double defaultRadius = 16.0;

		// Token: 0x04000B94 RID: 2964
		private double radius = 16.0;

		// Token: 0x02000135 RID: 309
		// (Invoke) Token: 0x06000EED RID: 3821
		public delegate void CircleSpawnPointDataFieldChangedEvent<T>(SpawnPoint spawnPoint, ref T oldValue, ref T newValue);
	}
}
