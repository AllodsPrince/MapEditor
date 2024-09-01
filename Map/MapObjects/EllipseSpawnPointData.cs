using System;

namespace MapEditor.Map.MapObjects
{
	// Token: 0x02000136 RID: 310
	public class EllipseSpawnPointData : SpawnPointData
	{
		// Token: 0x14000042 RID: 66
		// (add) Token: 0x06000EF0 RID: 3824 RVA: 0x00077C87 File Offset: 0x00076C87
		// (remove) Token: 0x06000EF1 RID: 3825 RVA: 0x00077C9E File Offset: 0x00076C9E
		public static event EllipseSpawnPointData.EllipseSpawnPointDataFieldChangedEvent<double> SemiaxisAChanged;

		// Token: 0x14000043 RID: 67
		// (add) Token: 0x06000EF2 RID: 3826 RVA: 0x00077CB5 File Offset: 0x00076CB5
		// (remove) Token: 0x06000EF3 RID: 3827 RVA: 0x00077CCC File Offset: 0x00076CCC
		public static event EllipseSpawnPointData.EllipseSpawnPointDataFieldChangedEvent<double> SemiaxisBChanged;

		// Token: 0x06000EF4 RID: 3828 RVA: 0x00077CE3 File Offset: 0x00076CE3
		public EllipseSpawnPointData(SpawnPoint _spawnPoint) : base(_spawnPoint, SpawnPointType.Ellipse)
		{
		}

		// Token: 0x06000EF5 RID: 3829 RVA: 0x00077D0B File Offset: 0x00076D0B
		public EllipseSpawnPointData() : base(null, SpawnPointType.Ellipse)
		{
		}

		// Token: 0x06000EF6 RID: 3830 RVA: 0x00077D33 File Offset: 0x00076D33
		public EllipseSpawnPointData(SpawnPoint _spawnPoint, double _semiaxisA, double _semiaxisB) : base(_spawnPoint, SpawnPointType.Ellipse)
		{
			this.semiaxisA = _semiaxisA;
			this.semiaxisB = _semiaxisB;
		}

		// Token: 0x170002D9 RID: 729
		// (get) Token: 0x06000EF7 RID: 3831 RVA: 0x00077D69 File Offset: 0x00076D69
		public static double DefaultSemiaxis
		{
			get
			{
				return 16.0;
			}
		}

		// Token: 0x170002DA RID: 730
		// (get) Token: 0x06000EF8 RID: 3832 RVA: 0x00077D74 File Offset: 0x00076D74
		// (set) Token: 0x06000EF9 RID: 3833 RVA: 0x00077D7C File Offset: 0x00076D7C
		public double SemiaxisA
		{
			get
			{
				return this.semiaxisA;
			}
			set
			{
				if (this.semiaxisA != value)
				{
					if (base.SpawnPoint != null && base.SpawnPoint.Active && !base.SpawnPoint.InvokeChanging(null))
					{
						return;
					}
					double oldSemiaxisA = this.semiaxisA;
					this.semiaxisA = value;
					if (base.SpawnPoint != null && base.SpawnPoint.Active)
					{
						base.SpawnPoint.InvokeChanged();
						base.InvokeChanged();
						if (EllipseSpawnPointData.SemiaxisAChanged != null)
						{
							EllipseSpawnPointData.SemiaxisAChanged(base.SpawnPoint, ref oldSemiaxisA, ref this.semiaxisA);
						}
					}
				}
			}
		}

		// Token: 0x170002DB RID: 731
		// (get) Token: 0x06000EFA RID: 3834 RVA: 0x00077E09 File Offset: 0x00076E09
		// (set) Token: 0x06000EFB RID: 3835 RVA: 0x00077E14 File Offset: 0x00076E14
		public double SemiaxisB
		{
			get
			{
				return this.semiaxisB;
			}
			set
			{
				if (this.semiaxisB != value)
				{
					if (base.SpawnPoint != null && base.SpawnPoint.Active && !base.SpawnPoint.InvokeChanging(null))
					{
						return;
					}
					double oldSemiaxisB = this.semiaxisB;
					this.semiaxisB = value;
					if (base.SpawnPoint != null && base.SpawnPoint.Active)
					{
						base.SpawnPoint.InvokeChanged();
						base.InvokeChanged();
						if (EllipseSpawnPointData.SemiaxisBChanged != null)
						{
							EllipseSpawnPointData.SemiaxisBChanged(base.SpawnPoint, ref oldSemiaxisB, ref this.semiaxisB);
						}
					}
				}
			}
		}

		// Token: 0x06000EFC RID: 3836 RVA: 0x00077EA4 File Offset: 0x00076EA4
		public override void CopyFrom(SpawnPointData spawnPointData)
		{
			if (spawnPointData != null && spawnPointData.SpawnPointType == SpawnPointType.Ellipse)
			{
				EllipseSpawnPointData ellipseSpawnPointData = spawnPointData as EllipseSpawnPointData;
				if (ellipseSpawnPointData != null)
				{
					this.semiaxisA = ellipseSpawnPointData.semiaxisA;
					this.semiaxisB = ellipseSpawnPointData.semiaxisB;
				}
			}
		}

		// Token: 0x04000B96 RID: 2966
		private const double defaultSemiaxis = 16.0;

		// Token: 0x04000B97 RID: 2967
		private double semiaxisA = 16.0;

		// Token: 0x04000B98 RID: 2968
		private double semiaxisB = 16.0;

		// Token: 0x02000137 RID: 311
		// (Invoke) Token: 0x06000EFE RID: 3838
		public delegate void EllipseSpawnPointDataFieldChangedEvent<T>(SpawnPoint spawnPoint, ref T oldValue, ref T newValue);
	}
}
