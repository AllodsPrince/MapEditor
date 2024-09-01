using System;

namespace MapEditor.Map.MapObjects
{
	// Token: 0x0200013A RID: 314
	public class SpawnCircleSpawnPointData : SpawnPointData
	{
		// Token: 0x14000046 RID: 70
		// (add) Token: 0x06000F10 RID: 3856 RVA: 0x000780E3 File Offset: 0x000770E3
		// (remove) Token: 0x06000F11 RID: 3857 RVA: 0x000780FA File Offset: 0x000770FA
		public static event SpawnCircleSpawnPointData.SpawnCircleSpawnPointDataFieldChangedEvent<double> RadiusChanged;

		// Token: 0x14000047 RID: 71
		// (add) Token: 0x06000F12 RID: 3858 RVA: 0x00078111 File Offset: 0x00077111
		// (remove) Token: 0x06000F13 RID: 3859 RVA: 0x00078128 File Offset: 0x00077128
		public static event SpawnCircleSpawnPointData.SpawnCircleSpawnPointDataFieldChangedEvent<int> MultiplicityChanged;

		// Token: 0x06000F14 RID: 3860 RVA: 0x0007813F File Offset: 0x0007713F
		public SpawnCircleSpawnPointData() : base(null, SpawnPointType.SpawnCircle)
		{
		}

		// Token: 0x06000F15 RID: 3861 RVA: 0x0007815F File Offset: 0x0007715F
		public SpawnCircleSpawnPointData(SpawnPoint _spawnPoint) : base(_spawnPoint, SpawnPointType.SpawnCircle)
		{
		}

		// Token: 0x06000F16 RID: 3862 RVA: 0x0007817F File Offset: 0x0007717F
		public SpawnCircleSpawnPointData(SpawnPoint _spawnPoint, double _radius, int _multiplicity) : base(_spawnPoint, SpawnPointType.SpawnCircle)
		{
			this.radius = _radius;
			this.multiplicity = _multiplicity;
		}

		// Token: 0x170002DE RID: 734
		// (get) Token: 0x06000F17 RID: 3863 RVA: 0x000781AD File Offset: 0x000771AD
		public static double DefaultRadius
		{
			get
			{
				return 16.0;
			}
		}

		// Token: 0x170002DF RID: 735
		// (get) Token: 0x06000F18 RID: 3864 RVA: 0x000781B8 File Offset: 0x000771B8
		public static int DefaultMultiplicity
		{
			get
			{
				return 1;
			}
		}

		// Token: 0x170002E0 RID: 736
		// (get) Token: 0x06000F19 RID: 3865 RVA: 0x000781BB File Offset: 0x000771BB
		// (set) Token: 0x06000F1A RID: 3866 RVA: 0x000781C4 File Offset: 0x000771C4
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
					if (base.SpawnPoint != null && base.SpawnPoint.Active && !base.SpawnPoint.InvokeChanging(null))
					{
						return;
					}
					double oldRadius = this.radius;
					this.radius = value;
					if (base.SpawnPoint != null && base.SpawnPoint.Active)
					{
						base.SpawnPoint.InvokeChanged();
						base.InvokeChanged();
						if (SpawnCircleSpawnPointData.RadiusChanged != null)
						{
							SpawnCircleSpawnPointData.RadiusChanged(base.SpawnPoint, ref oldRadius, ref this.radius);
						}
					}
				}
			}
		}

		// Token: 0x170002E1 RID: 737
		// (get) Token: 0x06000F1B RID: 3867 RVA: 0x00078251 File Offset: 0x00077251
		// (set) Token: 0x06000F1C RID: 3868 RVA: 0x0007825C File Offset: 0x0007725C
		public int Multiplicity
		{
			get
			{
				return this.multiplicity;
			}
			set
			{
				if (this.multiplicity != value)
				{
					if (base.SpawnPoint != null && base.SpawnPoint.Active && !base.SpawnPoint.InvokeChanging(null))
					{
						return;
					}
					int oldMultiplicity = this.multiplicity;
					this.multiplicity = value;
					if (base.SpawnPoint != null && base.SpawnPoint.Active)
					{
						base.SpawnPoint.InvokeChanged();
						base.InvokeChanged();
						if (SpawnCircleSpawnPointData.MultiplicityChanged != null)
						{
							SpawnCircleSpawnPointData.MultiplicityChanged(base.SpawnPoint, ref oldMultiplicity, ref this.multiplicity);
						}
					}
				}
			}
		}

		// Token: 0x06000F1D RID: 3869 RVA: 0x000782EC File Offset: 0x000772EC
		public override void CopyFrom(SpawnPointData spawnPointData)
		{
			if (spawnPointData != null && spawnPointData.SpawnPointType == SpawnPointType.SpawnCircle)
			{
				SpawnCircleSpawnPointData spawnCircleSpawnPointData = spawnPointData as SpawnCircleSpawnPointData;
				if (spawnCircleSpawnPointData != null)
				{
					this.radius = spawnCircleSpawnPointData.radius;
					this.multiplicity = spawnCircleSpawnPointData.multiplicity;
				}
			}
		}

		// Token: 0x04000B9F RID: 2975
		private const double defaultRadius = 16.0;

		// Token: 0x04000BA0 RID: 2976
		private const int defaultMultiplicity = 1;

		// Token: 0x04000BA1 RID: 2977
		private double radius = 16.0;

		// Token: 0x04000BA2 RID: 2978
		private int multiplicity = 1;

		// Token: 0x0200013B RID: 315
		// (Invoke) Token: 0x06000F1F RID: 3871
		public delegate void SpawnCircleSpawnPointDataFieldChangedEvent<T>(SpawnPoint spawnPoint, ref T oldValue, ref T newValue);
	}
}
