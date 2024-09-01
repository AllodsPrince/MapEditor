using System;
using System.ComponentModel;
using System.Xml.Serialization;

namespace MapEditor.Map.MapObjects
{
	// Token: 0x02000131 RID: 305
	[XmlInclude(typeof(PatrolSpawnPointData))]
	[XmlInclude(typeof(SpawnCircleSpawnPointData))]
	[XmlInclude(typeof(EllipseSpawnPointData))]
	[XmlInclude(typeof(CircleSpawnPointData))]
	[XmlInclude(typeof(GuardSpawnPointData))]
	public class SpawnPointData
	{
		// Token: 0x14000040 RID: 64
		// (add) Token: 0x06000ED1 RID: 3793 RVA: 0x00077A26 File Offset: 0x00076A26
		// (remove) Token: 0x06000ED2 RID: 3794 RVA: 0x00077A3D File Offset: 0x00076A3D
		public static event SpawnPointData.SpawnPointDataEvent Changed;

		// Token: 0x06000ED3 RID: 3795 RVA: 0x00077A54 File Offset: 0x00076A54
		public SpawnPointData()
		{
		}

		// Token: 0x06000ED4 RID: 3796 RVA: 0x00077A63 File Offset: 0x00076A63
		public SpawnPointData(SpawnPoint _spawnPoint, SpawnPointType _spawnPointType)
		{
			this.spawnPointType = _spawnPointType;
			this.spawnPoint = _spawnPoint;
		}

		// Token: 0x170002D3 RID: 723
		// (get) Token: 0x06000ED5 RID: 3797 RVA: 0x00077A80 File Offset: 0x00076A80
		public static SpawnPointType DefaultSpawnPointType
		{
			get
			{
				return SpawnPointType.Circle;
			}
		}

		// Token: 0x170002D4 RID: 724
		// (get) Token: 0x06000ED6 RID: 3798 RVA: 0x00077A83 File Offset: 0x00076A83
		public static SpawnPointType DefaultAstralSpawnPointType
		{
			get
			{
				return SpawnPointType.Guard;
			}
		}

		// Token: 0x170002D5 RID: 725
		// (get) Token: 0x06000ED7 RID: 3799 RVA: 0x00077A86 File Offset: 0x00076A86
		[XmlIgnore]
		[Browsable(false)]
		public SpawnPoint SpawnPoint
		{
			get
			{
				return this.spawnPoint;
			}
		}

		// Token: 0x170002D6 RID: 726
		// (get) Token: 0x06000ED8 RID: 3800 RVA: 0x00077A8E File Offset: 0x00076A8E
		[XmlIgnore]
		[Browsable(false)]
		public SpawnPointType SpawnPointType
		{
			get
			{
				return this.spawnPointType;
			}
		}

		// Token: 0x06000ED9 RID: 3801 RVA: 0x00077A96 File Offset: 0x00076A96
		public static SpawnPointData Create(SpawnPointType _spawnPointType, SpawnPoint _spawnPoint)
		{
			if (_spawnPointType == SpawnPointType.Guard)
			{
				return new GuardSpawnPointData(_spawnPoint);
			}
			if (_spawnPointType == SpawnPointType.Circle)
			{
				return new CircleSpawnPointData(_spawnPoint);
			}
			if (_spawnPointType == SpawnPointType.Ellipse)
			{
				return new EllipseSpawnPointData(_spawnPoint);
			}
			if (_spawnPointType == SpawnPointType.Patrol)
			{
				return new PatrolSpawnPointData(_spawnPoint);
			}
			if (_spawnPointType == SpawnPointType.SpawnCircle)
			{
				return new SpawnCircleSpawnPointData(_spawnPoint);
			}
			return null;
		}

		// Token: 0x06000EDA RID: 3802 RVA: 0x00077AD0 File Offset: 0x00076AD0
		public void InvokeChanged()
		{
			if (this.spawnPoint != null && this.spawnPoint.Active && SpawnPointData.Changed != null)
			{
				SpawnPointData.Changed(this.spawnPoint);
			}
		}

		// Token: 0x06000EDB RID: 3803 RVA: 0x00077AFE File Offset: 0x00076AFE
		public virtual void CopyFrom(SpawnPointData spawnPointData)
		{
		}

		// Token: 0x04000B8E RID: 2958
		private const SpawnPointType defaultSpawnPointType = SpawnPointType.Circle;

		// Token: 0x04000B8F RID: 2959
		private const SpawnPointType defaultAstralSpawnPointType = SpawnPointType.Guard;

		// Token: 0x04000B90 RID: 2960
		private readonly SpawnPoint spawnPoint;

		// Token: 0x04000B91 RID: 2961
		private readonly SpawnPointType spawnPointType = SpawnPointType.Circle;

		// Token: 0x02000132 RID: 306
		// (Invoke) Token: 0x06000EDD RID: 3805
		public delegate void SpawnPointDataEvent(SpawnPoint _spawnPoint);
	}
}
