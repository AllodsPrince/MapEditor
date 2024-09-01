using System;
using Operations;

namespace MapEditor.Map.MapObjects
{
	// Token: 0x020001FF RID: 511
	internal class ChangePermanentDeviceAICollisionOperation : IOperation
	{
		// Token: 0x0600195E RID: 6494 RVA: 0x000A6579 File Offset: 0x000A5579
		public ChangePermanentDeviceAICollisionOperation(PermanentDevice _permanentDevice, ref bool _oldValue, ref bool _newValue)
		{
			this.permanentDevice = _permanentDevice;
			this.oldValue = _oldValue;
			this.newValue = _newValue;
		}

		// Token: 0x0600195F RID: 6495 RVA: 0x000A6598 File Offset: 0x000A5598
		public bool Undo()
		{
			if (!this.IsEmpty)
			{
				this.permanentDevice.AICollision = this.oldValue;
				return true;
			}
			return false;
		}

		// Token: 0x06001960 RID: 6496 RVA: 0x000A65B6 File Offset: 0x000A55B6
		public bool Redo()
		{
			if (!this.IsEmpty)
			{
				this.permanentDevice.AICollision = this.newValue;
				return true;
			}
			return false;
		}

		// Token: 0x06001961 RID: 6497 RVA: 0x000A65D4 File Offset: 0x000A55D4
		public void Destroy()
		{
			this.permanentDevice = null;
		}

		// Token: 0x17000621 RID: 1569
		// (get) Token: 0x06001962 RID: 6498 RVA: 0x000A65DD File Offset: 0x000A55DD
		public bool IsEmpty
		{
			get
			{
				return this.permanentDevice == null;
			}
		}

		// Token: 0x17000622 RID: 1570
		// (get) Token: 0x06001963 RID: 6499 RVA: 0x000A65E8 File Offset: 0x000A55E8
		public bool IsAbsolute
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000623 RID: 1571
		// (get) Token: 0x06001964 RID: 6500 RVA: 0x000A65EB File Offset: 0x000A55EB
		public bool IsHighPriority
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06001965 RID: 6501 RVA: 0x000A65EE File Offset: 0x000A55EE
		public bool ContainsLabel(string label)
		{
			return label == this.Label;
		}

		// Token: 0x17000624 RID: 1572
		// (get) Token: 0x06001966 RID: 6502 RVA: 0x000A65FC File Offset: 0x000A55FC
		public string Label
		{
			get
			{
				return "MapObjectContainer";
			}
		}

		// Token: 0x17000625 RID: 1573
		// (get) Token: 0x06001967 RID: 6503 RVA: 0x000A6603 File Offset: 0x000A5603
		public string Description
		{
			get
			{
				return "ChangePermanentDeviceAICollisionOperation";
			}
		}

		// Token: 0x04001042 RID: 4162
		private PermanentDevice permanentDevice;

		// Token: 0x04001043 RID: 4163
		private readonly bool oldValue;

		// Token: 0x04001044 RID: 4164
		private readonly bool newValue;
	}
}
