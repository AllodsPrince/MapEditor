using System;
using Operations;

namespace MapEditor.Map.MapObjects
{
	// Token: 0x020001D9 RID: 473
	internal class ChangeScriptAreaScriptIDOperation : IOperation
	{
		// Token: 0x06001815 RID: 6165 RVA: 0x000A1C90 File Offset: 0x000A0C90
		public ChangeScriptAreaScriptIDOperation(ScriptArea _scriptArea, ref string _oldValue, ref string _newValue)
		{
			this.scriptArea = _scriptArea;
			this.oldValue = _oldValue;
			this.newValue = _newValue;
		}

		// Token: 0x06001816 RID: 6166 RVA: 0x000A1CAF File Offset: 0x000A0CAF
		public bool Undo()
		{
			if (!this.IsEmpty)
			{
				this.scriptArea.ScriptID = this.oldValue;
				return true;
			}
			return false;
		}

		// Token: 0x06001817 RID: 6167 RVA: 0x000A1CCD File Offset: 0x000A0CCD
		public bool Redo()
		{
			if (!this.IsEmpty)
			{
				this.scriptArea.ScriptID = this.newValue;
				return true;
			}
			return false;
		}

		// Token: 0x06001818 RID: 6168 RVA: 0x000A1CEB File Offset: 0x000A0CEB
		public void Destroy()
		{
			this.scriptArea = null;
		}

		// Token: 0x170005CE RID: 1486
		// (get) Token: 0x06001819 RID: 6169 RVA: 0x000A1CF4 File Offset: 0x000A0CF4
		public bool IsEmpty
		{
			get
			{
				return this.scriptArea == null;
			}
		}

		// Token: 0x170005CF RID: 1487
		// (get) Token: 0x0600181A RID: 6170 RVA: 0x000A1CFF File Offset: 0x000A0CFF
		public bool IsAbsolute
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170005D0 RID: 1488
		// (get) Token: 0x0600181B RID: 6171 RVA: 0x000A1D02 File Offset: 0x000A0D02
		public bool IsHighPriority
		{
			get
			{
				return false;
			}
		}

		// Token: 0x0600181C RID: 6172 RVA: 0x000A1D05 File Offset: 0x000A0D05
		public bool ContainsLabel(string label)
		{
			return label == this.Label;
		}

		// Token: 0x170005D1 RID: 1489
		// (get) Token: 0x0600181D RID: 6173 RVA: 0x000A1D13 File Offset: 0x000A0D13
		public string Label
		{
			get
			{
				return "MapObjectContainer";
			}
		}

		// Token: 0x170005D2 RID: 1490
		// (get) Token: 0x0600181E RID: 6174 RVA: 0x000A1D1A File Offset: 0x000A0D1A
		public string Description
		{
			get
			{
				return "ChangeScriptAreaScriptIDOperation";
			}
		}

		// Token: 0x04000F9A RID: 3994
		private ScriptArea scriptArea;

		// Token: 0x04000F9B RID: 3995
		private readonly string oldValue;

		// Token: 0x04000F9C RID: 3996
		private readonly string newValue;
	}
}
