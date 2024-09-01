using System;
using Operations;

namespace MapEditor.Map.MapObjects
{
	// Token: 0x020001D7 RID: 471
	internal class ChangeScriptAreaDataOperation : IOperation
	{
		// Token: 0x06001801 RID: 6145 RVA: 0x000A1B6E File Offset: 0x000A0B6E
		public ChangeScriptAreaDataOperation(ScriptArea _scriptArea, ref ScriptAreaData _oldValue, ref ScriptAreaData _newValue)
		{
			this.scriptArea = _scriptArea;
			this.oldValue = _oldValue;
			this.newValue = _newValue;
		}

		// Token: 0x06001802 RID: 6146 RVA: 0x000A1B8D File Offset: 0x000A0B8D
		public bool Undo()
		{
			if (!this.IsEmpty)
			{
				this.scriptArea.ScriptAreaData = this.oldValue;
				return true;
			}
			return false;
		}

		// Token: 0x06001803 RID: 6147 RVA: 0x000A1BAB File Offset: 0x000A0BAB
		public bool Redo()
		{
			if (!this.IsEmpty)
			{
				this.scriptArea.ScriptAreaData = this.newValue;
				return true;
			}
			return false;
		}

		// Token: 0x06001804 RID: 6148 RVA: 0x000A1BC9 File Offset: 0x000A0BC9
		public void Destroy()
		{
			this.scriptArea = null;
		}

		// Token: 0x170005C4 RID: 1476
		// (get) Token: 0x06001805 RID: 6149 RVA: 0x000A1BD2 File Offset: 0x000A0BD2
		public bool IsEmpty
		{
			get
			{
				return this.scriptArea == null;
			}
		}

		// Token: 0x170005C5 RID: 1477
		// (get) Token: 0x06001806 RID: 6150 RVA: 0x000A1BDD File Offset: 0x000A0BDD
		public bool IsAbsolute
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170005C6 RID: 1478
		// (get) Token: 0x06001807 RID: 6151 RVA: 0x000A1BE0 File Offset: 0x000A0BE0
		public bool IsHighPriority
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06001808 RID: 6152 RVA: 0x000A1BE3 File Offset: 0x000A0BE3
		public bool ContainsLabel(string label)
		{
			return label == this.Label;
		}

		// Token: 0x170005C7 RID: 1479
		// (get) Token: 0x06001809 RID: 6153 RVA: 0x000A1BF1 File Offset: 0x000A0BF1
		public string Label
		{
			get
			{
				return "MapObjectContainer";
			}
		}

		// Token: 0x170005C8 RID: 1480
		// (get) Token: 0x0600180A RID: 6154 RVA: 0x000A1BF8 File Offset: 0x000A0BF8
		public string Description
		{
			get
			{
				return "ChangeScriptAreaDataOperation";
			}
		}

		// Token: 0x04000F94 RID: 3988
		private ScriptArea scriptArea;

		// Token: 0x04000F95 RID: 3989
		private readonly ScriptAreaData oldValue;

		// Token: 0x04000F96 RID: 3990
		private readonly ScriptAreaData newValue;
	}
}
