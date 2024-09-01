using System;
using Tools.MapObjects;

namespace MapEditor.Map.MapObjects
{
	// Token: 0x020001D3 RID: 467
	public class ScriptAreaPack : SerializableMapObjectPack
	{
		// Token: 0x170005B4 RID: 1460
		// (get) Token: 0x060017CA RID: 6090 RVA: 0x000A14E5 File Offset: 0x000A04E5
		// (set) Token: 0x060017CB RID: 6091 RVA: 0x000A14ED File Offset: 0x000A04ED
		public string ScriptZone
		{
			get
			{
				return this.scriptZone;
			}
			set
			{
				this.scriptZone = value;
			}
		}

		// Token: 0x170005B5 RID: 1461
		// (get) Token: 0x060017CC RID: 6092 RVA: 0x000A14F6 File Offset: 0x000A04F6
		// (set) Token: 0x060017CD RID: 6093 RVA: 0x000A14FE File Offset: 0x000A04FE
		public string ScriptID
		{
			get
			{
				return this.scriptID;
			}
			set
			{
				this.scriptID = value;
			}
		}

		// Token: 0x170005B6 RID: 1462
		// (get) Token: 0x060017CE RID: 6094 RVA: 0x000A1507 File Offset: 0x000A0507
		// (set) Token: 0x060017CF RID: 6095 RVA: 0x000A150F File Offset: 0x000A050F
		public double ScanRadius
		{
			get
			{
				return this.scanRadius;
			}
			set
			{
				this.scanRadius = value;
			}
		}

		// Token: 0x170005B7 RID: 1463
		// (get) Token: 0x060017D0 RID: 6096 RVA: 0x000A1518 File Offset: 0x000A0518
		// (set) Token: 0x060017D1 RID: 6097 RVA: 0x000A1520 File Offset: 0x000A0520
		public ScriptAreaType ScriptAreaType
		{
			get
			{
				return this.scriptAreaType;
			}
			set
			{
				this.scriptAreaType = value;
			}
		}

		// Token: 0x170005B8 RID: 1464
		// (get) Token: 0x060017D2 RID: 6098 RVA: 0x000A1529 File Offset: 0x000A0529
		// (set) Token: 0x060017D3 RID: 6099 RVA: 0x000A1531 File Offset: 0x000A0531
		public CylinderScriptAreaDataPack CylinderScriptAreaDataPack
		{
			get
			{
				return this.cylinderScriptAreaDataPack;
			}
			set
			{
				this.cylinderScriptAreaDataPack = value;
			}
		}

		// Token: 0x060017D4 RID: 6100 RVA: 0x000A153C File Offset: 0x000A053C
		public override void Pack(IMapObject mapObject)
		{
			base.Pack(mapObject);
			ScriptArea scriptArea = mapObject as ScriptArea;
			if (scriptArea != null)
			{
				this.cylinderScriptAreaDataPack = null;
				this.scriptZone = scriptArea.ScriptZone;
				this.scriptID = scriptArea.ScriptID;
				this.scanRadius = scriptArea.ScanRadius;
				this.scriptAreaType = scriptArea.ScriptAreaType;
				if (this.scriptAreaType == ScriptAreaType.Cylinder)
				{
					this.cylinderScriptAreaDataPack = new CylinderScriptAreaDataPack();
					this.cylinderScriptAreaDataPack.Pack(scriptArea.ScriptAreaData);
				}
			}
		}

		// Token: 0x060017D5 RID: 6101 RVA: 0x000A15B8 File Offset: 0x000A05B8
		public override void Unpack(IMapObject mapObject)
		{
			base.Unpack(mapObject);
			ScriptArea scriptArea = mapObject as ScriptArea;
			if (scriptArea != null)
			{
				scriptArea.ScriptZone = this.scriptZone;
				scriptArea.ScriptID = this.scriptID;
				scriptArea.ScanRadius = this.scanRadius;
				if (this.scriptAreaType == ScriptAreaType.Cylinder && this.cylinderScriptAreaDataPack != null)
				{
					scriptArea.ScriptAreaData = new CylinderScriptAreaData(scriptArea);
					this.cylinderScriptAreaDataPack.Unpack(scriptArea.ScriptAreaData);
				}
			}
		}

		// Token: 0x04000F81 RID: 3969
		private string scriptZone = string.Empty;

		// Token: 0x04000F82 RID: 3970
		private string scriptID = string.Empty;

		// Token: 0x04000F83 RID: 3971
		private double scanRadius;

		// Token: 0x04000F84 RID: 3972
		private ScriptAreaType scriptAreaType = ScriptAreaData.DefaultScriptAreaType;

		// Token: 0x04000F85 RID: 3973
		private CylinderScriptAreaDataPack cylinderScriptAreaDataPack;
	}
}
