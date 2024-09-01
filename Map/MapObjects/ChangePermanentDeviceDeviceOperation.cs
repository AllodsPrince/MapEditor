using System;
using Operations;

namespace MapEditor.Map.MapObjects
{
	// Token: 0x020001FC RID: 508
	internal class ChangePermanentDeviceDeviceOperation : IOperation
	{
		// Token: 0x06001940 RID: 6464 RVA: 0x000A63C6 File Offset: 0x000A53C6
		public ChangePermanentDeviceDeviceOperation(PermanentDevice _permanentDevice, ref string _oldValue, ref string _newValue)
		{
			this.permanentDevice = _permanentDevice;
			this.oldValue = _oldValue;
			this.newValue = _newValue;
		}

		// Token: 0x06001941 RID: 6465 RVA: 0x000A63E5 File Offset: 0x000A53E5
		public bool Undo()
		{
			if (!this.IsEmpty)
			{
				this.permanentDevice.Device = this.oldValue;
				return true;
			}
			return false;
		}

		// Token: 0x06001942 RID: 6466 RVA: 0x000A6403 File Offset: 0x000A5403
		public bool Redo()
		{
			if (!this.IsEmpty)
			{
				this.permanentDevice.Device = this.newValue;
				return true;
			}
			return false;
		}

		// Token: 0x06001943 RID: 6467 RVA: 0x000A6421 File Offset: 0x000A5421
		public void Destroy()
		{
			this.permanentDevice = null;
		}

		// Token: 0x17000612 RID: 1554
		// (get) Token: 0x06001944 RID: 6468 RVA: 0x000A642A File Offset: 0x000A542A
		public bool IsEmpty
		{
			get
			{
				return this.permanentDevice == null;
			}
		}

		// Token: 0x17000613 RID: 1555
		// (get) Token: 0x06001945 RID: 6469 RVA: 0x000A6435 File Offset: 0x000A5435
		public bool IsAbsolute
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000614 RID: 1556
		// (get) Token: 0x06001946 RID: 6470 RVA: 0x000A6438 File Offset: 0x000A5438
		public bool IsHighPriority
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06001947 RID: 6471 RVA: 0x000A643B File Offset: 0x000A543B
		public bool ContainsLabel(string label)
		{
			return label == this.Label;
		}

		// Token: 0x17000615 RID: 1557
		// (get) Token: 0x06001948 RID: 6472 RVA: 0x000A6449 File Offset: 0x000A5449
		public string Label
		{
			get
			{
				return "MapObjectContainer";
			}
		}

		// Token: 0x17000616 RID: 1558
		// (get) Token: 0x06001949 RID: 6473 RVA: 0x000A6450 File Offset: 0x000A5450
		public string Description
		{
			get
			{
				return "ChangePermanentDeviceDeviceOperation";
			}
		}

		// Token: 0x04001039 RID: 4153
		private PermanentDevice permanentDevice;

		// Token: 0x0400103A RID: 4154
		private readonly string oldValue;

		// Token: 0x0400103B RID: 4155
		private readonly string newValue;
	}
}
