using System;
using System.ComponentModel;

namespace MapEditor.Map.MapObjects
{
	// Token: 0x020001CE RID: 462
	public class ScriptAreaData
	{
		// Token: 0x140000A7 RID: 167
		// (add) Token: 0x060017A3 RID: 6051 RVA: 0x000A1158 File Offset: 0x000A0158
		// (remove) Token: 0x060017A4 RID: 6052 RVA: 0x000A116F File Offset: 0x000A016F
		public static event ScriptAreaData.ScriptAreaDataEvent Changed;

		// Token: 0x060017A5 RID: 6053 RVA: 0x000A1186 File Offset: 0x000A0186
		public ScriptAreaData(ScriptArea _scriptArea, ScriptAreaType _scriptAreaType)
		{
			this.scriptAreaType = _scriptAreaType;
			this.scriptArea = _scriptArea;
		}

		// Token: 0x170005AB RID: 1451
		// (get) Token: 0x060017A6 RID: 6054 RVA: 0x000A11A7 File Offset: 0x000A01A7
		public static ScriptAreaType DefaultScriptAreaType
		{
			get
			{
				return ScriptAreaData.defaultScriptAreaType;
			}
		}

		// Token: 0x170005AC RID: 1452
		// (get) Token: 0x060017A7 RID: 6055 RVA: 0x000A11AE File Offset: 0x000A01AE
		[Browsable(false)]
		public ScriptArea ScriptArea
		{
			get
			{
				return this.scriptArea;
			}
		}

		// Token: 0x170005AD RID: 1453
		// (get) Token: 0x060017A8 RID: 6056 RVA: 0x000A11B6 File Offset: 0x000A01B6
		[Browsable(false)]
		public ScriptAreaType ScriptAreaType
		{
			get
			{
				return this.scriptAreaType;
			}
		}

		// Token: 0x060017A9 RID: 6057 RVA: 0x000A11BE File Offset: 0x000A01BE
		public static ScriptAreaData Create(ScriptAreaType _scriptAreaType, ScriptArea _scriptArea)
		{
			if (_scriptAreaType == ScriptAreaType.Cylinder)
			{
				return new CylinderScriptAreaData(_scriptArea);
			}
			return null;
		}

		// Token: 0x060017AA RID: 6058 RVA: 0x000A11CC File Offset: 0x000A01CC
		public void InvokeChanged()
		{
			if (this.scriptArea != null && this.scriptArea.Active && ScriptAreaData.Changed != null)
			{
				ScriptAreaData.Changed(this.scriptArea);
			}
		}

		// Token: 0x060017AB RID: 6059 RVA: 0x000A11FA File Offset: 0x000A01FA
		public virtual void CopyFrom(ScriptAreaData scriptAreaData)
		{
		}

		// Token: 0x04000F75 RID: 3957
		private static readonly ScriptAreaType defaultScriptAreaType = ScriptAreaType.Cylinder;

		// Token: 0x04000F76 RID: 3958
		private readonly ScriptArea scriptArea;

		// Token: 0x04000F77 RID: 3959
		private readonly ScriptAreaType scriptAreaType = ScriptAreaData.defaultScriptAreaType;

		// Token: 0x020001CF RID: 463
		// (Invoke) Token: 0x060017AE RID: 6062
		public delegate void ScriptAreaDataEvent(ScriptArea _scriptArea);
	}
}
