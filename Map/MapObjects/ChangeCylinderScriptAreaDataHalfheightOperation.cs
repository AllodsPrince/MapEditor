using System;
using Operations;

namespace MapEditor.Map.MapObjects
{
	// Token: 0x020001DC RID: 476
	internal class ChangeCylinderScriptAreaDataHalfheightOperation : IOperation
	{
		// Token: 0x06001833 RID: 6195 RVA: 0x000A1E91 File Offset: 0x000A0E91
		public ChangeCylinderScriptAreaDataHalfheightOperation(ScriptArea _scriptArea, ref double _oldValue, ref double _newValue)
		{
			this.scriptArea = _scriptArea;
			this.oldValue = _oldValue;
			this.newValue = _newValue;
		}

		// Token: 0x06001834 RID: 6196 RVA: 0x000A1EB0 File Offset: 0x000A0EB0
		public bool Undo()
		{
			if (!this.IsEmpty)
			{
				CylinderScriptAreaData cylinderScriptAreaData = this.scriptArea.ScriptAreaData as CylinderScriptAreaData;
				if (cylinderScriptAreaData != null)
				{
					cylinderScriptAreaData.Halfheight = this.oldValue;
					return true;
				}
			}
			return false;
		}

		// Token: 0x06001835 RID: 6197 RVA: 0x000A1EE8 File Offset: 0x000A0EE8
		public bool Redo()
		{
			if (!this.IsEmpty)
			{
				CylinderScriptAreaData cylinderScriptAreaData = this.scriptArea.ScriptAreaData as CylinderScriptAreaData;
				if (cylinderScriptAreaData != null)
				{
					cylinderScriptAreaData.Halfheight = this.newValue;
					return true;
				}
			}
			return false;
		}

		// Token: 0x06001836 RID: 6198 RVA: 0x000A1F20 File Offset: 0x000A0F20
		public void Destroy()
		{
			this.scriptArea = null;
		}

		// Token: 0x170005DD RID: 1501
		// (get) Token: 0x06001837 RID: 6199 RVA: 0x000A1F29 File Offset: 0x000A0F29
		public bool IsEmpty
		{
			get
			{
				return this.scriptArea == null || !(this.scriptArea.ScriptAreaData is CylinderScriptAreaData);
			}
		}

		// Token: 0x170005DE RID: 1502
		// (get) Token: 0x06001838 RID: 6200 RVA: 0x000A1F4B File Offset: 0x000A0F4B
		public bool IsAbsolute
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170005DF RID: 1503
		// (get) Token: 0x06001839 RID: 6201 RVA: 0x000A1F4E File Offset: 0x000A0F4E
		public bool IsHighPriority
		{
			get
			{
				return false;
			}
		}

		// Token: 0x0600183A RID: 6202 RVA: 0x000A1F51 File Offset: 0x000A0F51
		public bool ContainsLabel(string label)
		{
			return label == this.Label;
		}

		// Token: 0x170005E0 RID: 1504
		// (get) Token: 0x0600183B RID: 6203 RVA: 0x000A1F5F File Offset: 0x000A0F5F
		public string Label
		{
			get
			{
				return "MapObjectContainer";
			}
		}

		// Token: 0x170005E1 RID: 1505
		// (get) Token: 0x0600183C RID: 6204 RVA: 0x000A1F66 File Offset: 0x000A0F66
		public string Description
		{
			get
			{
				return "ChangeCylinderScriptAreaDataHalfheightOperation";
			}
		}

		// Token: 0x04000FA3 RID: 4003
		private ScriptArea scriptArea;

		// Token: 0x04000FA4 RID: 4004
		private readonly double oldValue;

		// Token: 0x04000FA5 RID: 4005
		private readonly double newValue;
	}
}
