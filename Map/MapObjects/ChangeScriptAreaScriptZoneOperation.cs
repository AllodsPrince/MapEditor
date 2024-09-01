using System;
using Operations;

namespace MapEditor.Map.MapObjects
{
	// Token: 0x020001D8 RID: 472
	internal class ChangeScriptAreaScriptZoneOperation : IOperation
	{
		// Token: 0x0600180B RID: 6155 RVA: 0x000A1BFF File Offset: 0x000A0BFF
		public ChangeScriptAreaScriptZoneOperation(ScriptArea _scriptArea, ref string _oldValue, ref string _newValue)
		{
			this.scriptArea = _scriptArea;
			this.oldValue = _oldValue;
			this.newValue = _newValue;
		}

		// Token: 0x0600180C RID: 6156 RVA: 0x000A1C1E File Offset: 0x000A0C1E
		public bool Undo()
		{
			if (!this.IsEmpty)
			{
				this.scriptArea.ScriptZone = this.oldValue;
				return true;
			}
			return false;
		}

		// Token: 0x0600180D RID: 6157 RVA: 0x000A1C3C File Offset: 0x000A0C3C
		public bool Redo()
		{
			if (!this.IsEmpty)
			{
				this.scriptArea.ScriptZone = this.newValue;
				return true;
			}
			return false;
		}

		// Token: 0x0600180E RID: 6158 RVA: 0x000A1C5A File Offset: 0x000A0C5A
		public void Destroy()
		{
			this.scriptArea = null;
		}

		// Token: 0x170005C9 RID: 1481
		// (get) Token: 0x0600180F RID: 6159 RVA: 0x000A1C63 File Offset: 0x000A0C63
		public bool IsEmpty
		{
			get
			{
				return this.scriptArea == null;
			}
		}

		// Token: 0x170005CA RID: 1482
		// (get) Token: 0x06001810 RID: 6160 RVA: 0x000A1C6E File Offset: 0x000A0C6E
		public bool IsAbsolute
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170005CB RID: 1483
		// (get) Token: 0x06001811 RID: 6161 RVA: 0x000A1C71 File Offset: 0x000A0C71
		public bool IsHighPriority
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06001812 RID: 6162 RVA: 0x000A1C74 File Offset: 0x000A0C74
		public bool ContainsLabel(string label)
		{
			return label == this.Label;
		}

		// Token: 0x170005CC RID: 1484
		// (get) Token: 0x06001813 RID: 6163 RVA: 0x000A1C82 File Offset: 0x000A0C82
		public string Label
		{
			get
			{
				return "MapObjectContainer";
			}
		}

		// Token: 0x170005CD RID: 1485
		// (get) Token: 0x06001814 RID: 6164 RVA: 0x000A1C89 File Offset: 0x000A0C89
		public string Description
		{
			get
			{
				return "ChangeScriptAreaScriptZoneOperation";
			}
		}

		// Token: 0x04000F97 RID: 3991
		private ScriptArea scriptArea;

		// Token: 0x04000F98 RID: 3992
		private readonly string oldValue;

		// Token: 0x04000F99 RID: 3993
		private readonly string newValue;
	}
}
