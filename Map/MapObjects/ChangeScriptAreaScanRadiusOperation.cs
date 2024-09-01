using System;
using Operations;

namespace MapEditor.Map.MapObjects
{
	// Token: 0x020001DA RID: 474
	internal class ChangeScriptAreaScanRadiusOperation : IOperation
	{
		// Token: 0x0600181F RID: 6175 RVA: 0x000A1D21 File Offset: 0x000A0D21
		public ChangeScriptAreaScanRadiusOperation(ScriptArea _scriptArea, ref double _oldValue, ref double _newValue)
		{
			this.scriptArea = _scriptArea;
			this.oldValue = _oldValue;
			this.newValue = _newValue;
		}

		// Token: 0x06001820 RID: 6176 RVA: 0x000A1D40 File Offset: 0x000A0D40
		public bool Undo()
		{
			if (!this.IsEmpty)
			{
				this.scriptArea.ScanRadius = this.oldValue;
				return true;
			}
			return false;
		}

		// Token: 0x06001821 RID: 6177 RVA: 0x000A1D5E File Offset: 0x000A0D5E
		public bool Redo()
		{
			if (!this.IsEmpty)
			{
				this.scriptArea.ScanRadius = this.newValue;
				return true;
			}
			return false;
		}

		// Token: 0x06001822 RID: 6178 RVA: 0x000A1D7C File Offset: 0x000A0D7C
		public void Destroy()
		{
			this.scriptArea = null;
		}

		// Token: 0x170005D3 RID: 1491
		// (get) Token: 0x06001823 RID: 6179 RVA: 0x000A1D85 File Offset: 0x000A0D85
		public bool IsEmpty
		{
			get
			{
				return this.scriptArea == null;
			}
		}

		// Token: 0x170005D4 RID: 1492
		// (get) Token: 0x06001824 RID: 6180 RVA: 0x000A1D90 File Offset: 0x000A0D90
		public bool IsAbsolute
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170005D5 RID: 1493
		// (get) Token: 0x06001825 RID: 6181 RVA: 0x000A1D93 File Offset: 0x000A0D93
		public bool IsHighPriority
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06001826 RID: 6182 RVA: 0x000A1D96 File Offset: 0x000A0D96
		public bool ContainsLabel(string label)
		{
			return label == this.Label;
		}

		// Token: 0x170005D6 RID: 1494
		// (get) Token: 0x06001827 RID: 6183 RVA: 0x000A1DA4 File Offset: 0x000A0DA4
		public string Label
		{
			get
			{
				return "MapObjectContainer";
			}
		}

		// Token: 0x170005D7 RID: 1495
		// (get) Token: 0x06001828 RID: 6184 RVA: 0x000A1DAB File Offset: 0x000A0DAB
		public string Description
		{
			get
			{
				return "ChangeScriptAreaScanRadiusOperation";
			}
		}

		// Token: 0x04000F9D RID: 3997
		private ScriptArea scriptArea;

		// Token: 0x04000F9E RID: 3998
		private readonly double oldValue;

		// Token: 0x04000F9F RID: 3999
		private readonly double newValue;
	}
}
