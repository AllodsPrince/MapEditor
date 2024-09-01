using System;
using Operations;

namespace MapEditor.Map.MapObjects
{
	// Token: 0x020001DB RID: 475
	internal class ChangeCylinderScriptAreaDataRadiusOperation : IOperation
	{
		// Token: 0x06001829 RID: 6185 RVA: 0x000A1DB2 File Offset: 0x000A0DB2
		public ChangeCylinderScriptAreaDataRadiusOperation(ScriptArea _scriptArea, ref double _oldValue, ref double _newValue)
		{
			this.scriptArea = _scriptArea;
			this.oldValue = _oldValue;
			this.newValue = _newValue;
		}

		// Token: 0x0600182A RID: 6186 RVA: 0x000A1DD4 File Offset: 0x000A0DD4
		public bool Undo()
		{
			if (!this.IsEmpty)
			{
				CylinderScriptAreaData cylinderScriptAreaData = this.scriptArea.ScriptAreaData as CylinderScriptAreaData;
				if (cylinderScriptAreaData != null)
				{
					cylinderScriptAreaData.Radius = this.oldValue;
					return true;
				}
			}
			return false;
		}

		// Token: 0x0600182B RID: 6187 RVA: 0x000A1E0C File Offset: 0x000A0E0C
		public bool Redo()
		{
			if (!this.IsEmpty)
			{
				CylinderScriptAreaData cylinderScriptAreaData = this.scriptArea.ScriptAreaData as CylinderScriptAreaData;
				if (cylinderScriptAreaData != null)
				{
					cylinderScriptAreaData.Radius = this.newValue;
					return true;
				}
			}
			return false;
		}

		// Token: 0x0600182C RID: 6188 RVA: 0x000A1E44 File Offset: 0x000A0E44
		public void Destroy()
		{
			this.scriptArea = null;
		}

		// Token: 0x170005D8 RID: 1496
		// (get) Token: 0x0600182D RID: 6189 RVA: 0x000A1E4D File Offset: 0x000A0E4D
		public bool IsEmpty
		{
			get
			{
				return this.scriptArea == null || !(this.scriptArea.ScriptAreaData is CylinderScriptAreaData);
			}
		}

		// Token: 0x170005D9 RID: 1497
		// (get) Token: 0x0600182E RID: 6190 RVA: 0x000A1E6F File Offset: 0x000A0E6F
		public bool IsAbsolute
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170005DA RID: 1498
		// (get) Token: 0x0600182F RID: 6191 RVA: 0x000A1E72 File Offset: 0x000A0E72
		public bool IsHighPriority
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06001830 RID: 6192 RVA: 0x000A1E75 File Offset: 0x000A0E75
		public bool ContainsLabel(string label)
		{
			return label == this.Label;
		}

		// Token: 0x170005DB RID: 1499
		// (get) Token: 0x06001831 RID: 6193 RVA: 0x000A1E83 File Offset: 0x000A0E83
		public string Label
		{
			get
			{
				return "MapObjectContainer";
			}
		}

		// Token: 0x170005DC RID: 1500
		// (get) Token: 0x06001832 RID: 6194 RVA: 0x000A1E8A File Offset: 0x000A0E8A
		public string Description
		{
			get
			{
				return "ChangeCylinderScriptAreaDataRadiusOperation";
			}
		}

		// Token: 0x04000FA0 RID: 4000
		private ScriptArea scriptArea;

		// Token: 0x04000FA1 RID: 4001
		private readonly double oldValue;

		// Token: 0x04000FA2 RID: 4002
		private readonly double newValue;
	}
}
