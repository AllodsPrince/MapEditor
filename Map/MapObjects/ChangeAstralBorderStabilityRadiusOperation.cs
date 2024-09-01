using System;
using Operations;

namespace MapEditor.Map.MapObjects
{
	// Token: 0x02000157 RID: 343
	internal class ChangeAstralBorderStabilityRadiusOperation : IOperation
	{
		// Token: 0x06001088 RID: 4232 RVA: 0x0007B941 File Offset: 0x0007A941
		public ChangeAstralBorderStabilityRadiusOperation(AstralBorder _astralBorder, ref double _oldValue, ref double _newValue)
		{
			this.astralBorder = _astralBorder;
			this.oldValue = _oldValue;
			this.newValue = _newValue;
		}

		// Token: 0x06001089 RID: 4233 RVA: 0x0007B960 File Offset: 0x0007A960
		public bool Undo()
		{
			if (!this.IsEmpty)
			{
				this.astralBorder.StabilityRadius = this.oldValue;
				return true;
			}
			return false;
		}

		// Token: 0x0600108A RID: 4234 RVA: 0x0007B97E File Offset: 0x0007A97E
		public bool Redo()
		{
			if (!this.IsEmpty)
			{
				this.astralBorder.StabilityRadius = this.newValue;
				return true;
			}
			return false;
		}

		// Token: 0x0600108B RID: 4235 RVA: 0x0007B99C File Offset: 0x0007A99C
		public void Destroy()
		{
			this.astralBorder = null;
		}

		// Token: 0x1700036C RID: 876
		// (get) Token: 0x0600108C RID: 4236 RVA: 0x0007B9A5 File Offset: 0x0007A9A5
		public bool IsEmpty
		{
			get
			{
				return this.astralBorder == null;
			}
		}

		// Token: 0x1700036D RID: 877
		// (get) Token: 0x0600108D RID: 4237 RVA: 0x0007B9B0 File Offset: 0x0007A9B0
		public bool IsAbsolute
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700036E RID: 878
		// (get) Token: 0x0600108E RID: 4238 RVA: 0x0007B9B3 File Offset: 0x0007A9B3
		public bool IsHighPriority
		{
			get
			{
				return false;
			}
		}

		// Token: 0x0600108F RID: 4239 RVA: 0x0007B9B6 File Offset: 0x0007A9B6
		public bool ContainsLabel(string label)
		{
			return label == this.Label;
		}

		// Token: 0x1700036F RID: 879
		// (get) Token: 0x06001090 RID: 4240 RVA: 0x0007B9C4 File Offset: 0x0007A9C4
		public string Label
		{
			get
			{
				return "MapObjectContainer";
			}
		}

		// Token: 0x17000370 RID: 880
		// (get) Token: 0x06001091 RID: 4241 RVA: 0x0007B9CB File Offset: 0x0007A9CB
		public string Description
		{
			get
			{
				return "ChangeAstralBorderStabilityRadiusOperation";
			}
		}

		// Token: 0x04000C28 RID: 3112
		private AstralBorder astralBorder;

		// Token: 0x04000C29 RID: 3113
		private readonly double oldValue;

		// Token: 0x04000C2A RID: 3114
		private readonly double newValue;
	}
}
