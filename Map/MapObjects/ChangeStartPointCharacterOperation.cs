using System;
using Operations;

namespace MapEditor.Map.MapObjects
{
	// Token: 0x0200027F RID: 639
	public class ChangeStartPointCharacterOperation : IOperation
	{
		// Token: 0x06001E5A RID: 7770 RVA: 0x000C5ABF File Offset: 0x000C4ABF
		public ChangeStartPointCharacterOperation(StartPoint _startPoint, ref string _oldValue, ref string _newValue)
		{
			this.startPoint = _startPoint;
			this.oldValue = _oldValue;
			this.newValue = _newValue;
		}

		// Token: 0x06001E5B RID: 7771 RVA: 0x000C5ADE File Offset: 0x000C4ADE
		public bool Undo()
		{
			if (!this.IsEmpty)
			{
				this.startPoint.Character = this.oldValue;
				return true;
			}
			return false;
		}

		// Token: 0x06001E5C RID: 7772 RVA: 0x000C5AFC File Offset: 0x000C4AFC
		public bool Redo()
		{
			if (!this.IsEmpty)
			{
				this.startPoint.Character = this.newValue;
				return true;
			}
			return false;
		}

		// Token: 0x06001E5D RID: 7773 RVA: 0x000C5B1A File Offset: 0x000C4B1A
		public void Destroy()
		{
			this.startPoint = null;
		}

		// Token: 0x17000700 RID: 1792
		// (get) Token: 0x06001E5E RID: 7774 RVA: 0x000C5B23 File Offset: 0x000C4B23
		public bool IsEmpty
		{
			get
			{
				return this.startPoint == null;
			}
		}

		// Token: 0x17000701 RID: 1793
		// (get) Token: 0x06001E5F RID: 7775 RVA: 0x000C5B2E File Offset: 0x000C4B2E
		public bool IsAbsolute
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000702 RID: 1794
		// (get) Token: 0x06001E60 RID: 7776 RVA: 0x000C5B31 File Offset: 0x000C4B31
		public bool IsHighPriority
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06001E61 RID: 7777 RVA: 0x000C5B34 File Offset: 0x000C4B34
		public bool ContainsLabel(string label)
		{
			return label == this.Label;
		}

		// Token: 0x17000703 RID: 1795
		// (get) Token: 0x06001E62 RID: 7778 RVA: 0x000C5B42 File Offset: 0x000C4B42
		public string Label
		{
			get
			{
				return "MapObjectContainer";
			}
		}

		// Token: 0x17000704 RID: 1796
		// (get) Token: 0x06001E63 RID: 7779 RVA: 0x000C5B49 File Offset: 0x000C4B49
		public string Description
		{
			get
			{
				return "ChangeStartPointCharacterOperation";
			}
		}

		// Token: 0x04001304 RID: 4868
		private StartPoint startPoint;

		// Token: 0x04001305 RID: 4869
		private readonly string oldValue;

		// Token: 0x04001306 RID: 4870
		private readonly string newValue;
	}
}
