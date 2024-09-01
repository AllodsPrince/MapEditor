using System;

namespace MapEditor.Map.MapObjects
{
	// Token: 0x020001D0 RID: 464
	public class CylinderScriptAreaData : ScriptAreaData
	{
		// Token: 0x140000A8 RID: 168
		// (add) Token: 0x060017B1 RID: 6065 RVA: 0x000A1204 File Offset: 0x000A0204
		// (remove) Token: 0x060017B2 RID: 6066 RVA: 0x000A121B File Offset: 0x000A021B
		public static event CylinderScriptAreaData.CylinderScriptAreaFieldChangedEvent<double> RadiusChanged;

		// Token: 0x140000A9 RID: 169
		// (add) Token: 0x060017B3 RID: 6067 RVA: 0x000A1232 File Offset: 0x000A0232
		// (remove) Token: 0x060017B4 RID: 6068 RVA: 0x000A1249 File Offset: 0x000A0249
		public static event CylinderScriptAreaData.CylinderScriptAreaFieldChangedEvent<double> HalfheightChanged;

		// Token: 0x060017B5 RID: 6069 RVA: 0x000A1260 File Offset: 0x000A0260
		public CylinderScriptAreaData(ScriptArea _scriptArea) : base(_scriptArea, ScriptAreaType.Cylinder)
		{
		}

		// Token: 0x060017B6 RID: 6070 RVA: 0x000A1280 File Offset: 0x000A0280
		public CylinderScriptAreaData(ScriptArea _scriptArea, double _radius, double _halfheight) : base(_scriptArea, ScriptAreaType.Cylinder)
		{
			this.radius = _radius;
			this.halfheight = _halfheight;
		}

		// Token: 0x170005AE RID: 1454
		// (get) Token: 0x060017B7 RID: 6071 RVA: 0x000A12AE File Offset: 0x000A02AE
		public static double DefaultRadius
		{
			get
			{
				return CylinderScriptAreaData.defaultRadius;
			}
		}

		// Token: 0x170005AF RID: 1455
		// (get) Token: 0x060017B8 RID: 6072 RVA: 0x000A12B5 File Offset: 0x000A02B5
		public static double DefaultHalfheight
		{
			get
			{
				return CylinderScriptAreaData.defaultHalfheight;
			}
		}

		// Token: 0x170005B0 RID: 1456
		// (get) Token: 0x060017B9 RID: 6073 RVA: 0x000A12BC File Offset: 0x000A02BC
		// (set) Token: 0x060017BA RID: 6074 RVA: 0x000A12C4 File Offset: 0x000A02C4
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
					if (base.ScriptArea != null && base.ScriptArea.Active && !base.ScriptArea.InvokeChanging(null))
					{
						return;
					}
					double oldRadius = this.radius;
					this.radius = value;
					if (base.ScriptArea != null && base.ScriptArea.Active)
					{
						base.ScriptArea.InvokeChanged();
						base.InvokeChanged();
						if (CylinderScriptAreaData.RadiusChanged != null)
						{
							CylinderScriptAreaData.RadiusChanged(base.ScriptArea, ref oldRadius, ref this.radius);
						}
					}
				}
			}
		}

		// Token: 0x170005B1 RID: 1457
		// (get) Token: 0x060017BB RID: 6075 RVA: 0x000A1351 File Offset: 0x000A0351
		// (set) Token: 0x060017BC RID: 6076 RVA: 0x000A135C File Offset: 0x000A035C
		public double Halfheight
		{
			get
			{
				return this.halfheight;
			}
			set
			{
				if (this.halfheight != value)
				{
					if (base.ScriptArea != null && base.ScriptArea.Active && !base.ScriptArea.InvokeChanging(null))
					{
						return;
					}
					double oldHalfheight = this.halfheight;
					this.halfheight = value;
					if (base.ScriptArea != null && base.ScriptArea.Active)
					{
						base.ScriptArea.InvokeChanged();
						base.InvokeChanged();
						if (CylinderScriptAreaData.HalfheightChanged != null)
						{
							CylinderScriptAreaData.HalfheightChanged(base.ScriptArea, ref oldHalfheight, ref this.halfheight);
						}
					}
				}
			}
		}

		// Token: 0x060017BD RID: 6077 RVA: 0x000A13EC File Offset: 0x000A03EC
		public override void CopyFrom(ScriptAreaData scriptAreaData)
		{
			if (scriptAreaData != null && scriptAreaData.ScriptAreaType == ScriptAreaType.Cylinder)
			{
				CylinderScriptAreaData cylinderScriptAreaData = scriptAreaData as CylinderScriptAreaData;
				if (cylinderScriptAreaData != null)
				{
					this.radius = cylinderScriptAreaData.radius;
					this.halfheight = cylinderScriptAreaData.halfheight;
				}
			}
		}

		// Token: 0x04000F79 RID: 3961
		private static readonly double defaultRadius = 16.0;

		// Token: 0x04000F7A RID: 3962
		private static readonly double defaultHalfheight = 16.0;

		// Token: 0x04000F7B RID: 3963
		private double radius = CylinderScriptAreaData.defaultRadius;

		// Token: 0x04000F7C RID: 3964
		private double halfheight = CylinderScriptAreaData.defaultHalfheight;

		// Token: 0x020001D1 RID: 465
		// (Invoke) Token: 0x060017C0 RID: 6080
		public delegate void CylinderScriptAreaFieldChangedEvent<T>(ScriptArea scriptArea, ref T oldValue, ref T newValue);
	}
}
