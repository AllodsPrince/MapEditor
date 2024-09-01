using System;
using Operations;

namespace MapEditor.Map.MapObjects
{
	// Token: 0x0200010B RID: 267
	internal class ChangeStaticObjectAICollisionOperation : IOperation
	{
		// Token: 0x06000D22 RID: 3362 RVA: 0x0006E5EB File Offset: 0x0006D5EB
		public ChangeStaticObjectAICollisionOperation(StaticObject _staticObject, ref bool _oldValue, ref bool _newValue)
		{
			this.staticObject = _staticObject;
			this.oldValue = _oldValue;
			this.newValue = _newValue;
		}

		// Token: 0x06000D23 RID: 3363 RVA: 0x0006E60A File Offset: 0x0006D60A
		public bool Undo()
		{
			if (!this.IsEmpty)
			{
				this.staticObject.AICollision = this.oldValue;
				return true;
			}
			return false;
		}

		// Token: 0x06000D24 RID: 3364 RVA: 0x0006E628 File Offset: 0x0006D628
		public bool Redo()
		{
			if (!this.IsEmpty)
			{
				this.staticObject.AICollision = this.newValue;
				return true;
			}
			return false;
		}

		// Token: 0x06000D25 RID: 3365 RVA: 0x0006E646 File Offset: 0x0006D646
		public void Destroy()
		{
			this.staticObject = null;
		}

		// Token: 0x17000271 RID: 625
		// (get) Token: 0x06000D26 RID: 3366 RVA: 0x0006E64F File Offset: 0x0006D64F
		public bool IsEmpty
		{
			get
			{
				return this.staticObject == null;
			}
		}

		// Token: 0x17000272 RID: 626
		// (get) Token: 0x06000D27 RID: 3367 RVA: 0x0006E65A File Offset: 0x0006D65A
		public bool IsAbsolute
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000273 RID: 627
		// (get) Token: 0x06000D28 RID: 3368 RVA: 0x0006E65D File Offset: 0x0006D65D
		public bool IsHighPriority
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06000D29 RID: 3369 RVA: 0x0006E660 File Offset: 0x0006D660
		public bool ContainsLabel(string label)
		{
			return label == this.Label;
		}

		// Token: 0x17000274 RID: 628
		// (get) Token: 0x06000D2A RID: 3370 RVA: 0x0006E66E File Offset: 0x0006D66E
		public string Label
		{
			get
			{
				return "MapObjectContainer";
			}
		}

		// Token: 0x17000275 RID: 629
		// (get) Token: 0x06000D2B RID: 3371 RVA: 0x0006E675 File Offset: 0x0006D675
		public string Description
		{
			get
			{
				return "ChangeStaticAICollisionOperation";
			}
		}

		// Token: 0x04000A92 RID: 2706
		private StaticObject staticObject;

		// Token: 0x04000A93 RID: 2707
		private readonly bool oldValue;

		// Token: 0x04000A94 RID: 2708
		private readonly bool newValue;
	}
}
