using System;

namespace MapEditor.Map.MapObjects
{
	// Token: 0x02000036 RID: 54
	public class ObjectCreationRandomParams
	{
		// Token: 0x1700004A RID: 74
		// (get) Token: 0x0600031D RID: 797 RVA: 0x0001E4C9 File Offset: 0x0001D4C9
		// (set) Token: 0x0600031E RID: 798 RVA: 0x0001E4D1 File Offset: 0x0001D4D1
		public bool RandomRotation
		{
			get
			{
				return this.randomRotation;
			}
			set
			{
				this.randomRotation = value;
			}
		}

		// Token: 0x1700004B RID: 75
		// (get) Token: 0x0600031F RID: 799 RVA: 0x0001E4DA File Offset: 0x0001D4DA
		// (set) Token: 0x06000320 RID: 800 RVA: 0x0001E4E2 File Offset: 0x0001D4E2
		public bool RandomPitch
		{
			get
			{
				return this.randomPitch;
			}
			set
			{
				this.randomPitch = value;
			}
		}

		// Token: 0x1700004C RID: 76
		// (get) Token: 0x06000321 RID: 801 RVA: 0x0001E4EB File Offset: 0x0001D4EB
		// (set) Token: 0x06000322 RID: 802 RVA: 0x0001E4F3 File Offset: 0x0001D4F3
		public bool RandomScale
		{
			get
			{
				return this.randomScale;
			}
			set
			{
				this.randomScale = value;
			}
		}

		// Token: 0x1700004D RID: 77
		// (get) Token: 0x06000323 RID: 803 RVA: 0x0001E4FC File Offset: 0x0001D4FC
		// (set) Token: 0x06000324 RID: 804 RVA: 0x0001E504 File Offset: 0x0001D504
		public float PitchFrom
		{
			get
			{
				return this.pitchFrom;
			}
			set
			{
				this.pitchFrom = value;
			}
		}

		// Token: 0x1700004E RID: 78
		// (get) Token: 0x06000325 RID: 805 RVA: 0x0001E50D File Offset: 0x0001D50D
		// (set) Token: 0x06000326 RID: 806 RVA: 0x0001E515 File Offset: 0x0001D515
		public float PitchTo
		{
			get
			{
				return this.pitchTo;
			}
			set
			{
				this.pitchTo = value;
			}
		}

		// Token: 0x1700004F RID: 79
		// (get) Token: 0x06000327 RID: 807 RVA: 0x0001E51E File Offset: 0x0001D51E
		// (set) Token: 0x06000328 RID: 808 RVA: 0x0001E526 File Offset: 0x0001D526
		public float RotationFrom
		{
			get
			{
				return this.rotationFrom;
			}
			set
			{
				this.rotationFrom = value;
			}
		}

		// Token: 0x17000050 RID: 80
		// (get) Token: 0x06000329 RID: 809 RVA: 0x0001E52F File Offset: 0x0001D52F
		// (set) Token: 0x0600032A RID: 810 RVA: 0x0001E537 File Offset: 0x0001D537
		public float RotationTo
		{
			get
			{
				return this.rotationTo;
			}
			set
			{
				this.rotationTo = value;
			}
		}

		// Token: 0x17000051 RID: 81
		// (get) Token: 0x0600032B RID: 811 RVA: 0x0001E540 File Offset: 0x0001D540
		// (set) Token: 0x0600032C RID: 812 RVA: 0x0001E548 File Offset: 0x0001D548
		public float ScaleFrom
		{
			get
			{
				return this.scaleFrom;
			}
			set
			{
				this.scaleFrom = value;
			}
		}

		// Token: 0x17000052 RID: 82
		// (get) Token: 0x0600032D RID: 813 RVA: 0x0001E551 File Offset: 0x0001D551
		// (set) Token: 0x0600032E RID: 814 RVA: 0x0001E559 File Offset: 0x0001D559
		public float ScaleTo
		{
			get
			{
				return this.scaleTo;
			}
			set
			{
				this.scaleTo = value;
			}
		}

		// Token: 0x0400026A RID: 618
		private bool randomRotation;

		// Token: 0x0400026B RID: 619
		private bool randomPitch;

		// Token: 0x0400026C RID: 620
		private bool randomScale;

		// Token: 0x0400026D RID: 621
		private float pitchFrom = -45f;

		// Token: 0x0400026E RID: 622
		private float pitchTo = 45f;

		// Token: 0x0400026F RID: 623
		private float rotationFrom;

		// Token: 0x04000270 RID: 624
		private float rotationTo = 360f;

		// Token: 0x04000271 RID: 625
		private float scaleFrom = 0.75f;

		// Token: 0x04000272 RID: 626
		private float scaleTo = 1.25f;
	}
}
