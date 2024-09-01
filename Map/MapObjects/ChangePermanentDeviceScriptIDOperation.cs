using System;
using Operations;

namespace MapEditor.Map.MapObjects
{
	// Token: 0x020001FD RID: 509
	internal class ChangePermanentDeviceScriptIDOperation : IOperation
	{
		// Token: 0x0600194A RID: 6474 RVA: 0x000A6457 File Offset: 0x000A5457
		public ChangePermanentDeviceScriptIDOperation(PermanentDevice _permanentDevice, ref string _oldValue, ref string _newValue)
		{
			this.permanentDevice = _permanentDevice;
			this.oldValue = _oldValue;
			this.newValue = _newValue;
		}

		// Token: 0x0600194B RID: 6475 RVA: 0x000A6476 File Offset: 0x000A5476
		public bool Undo()
		{
			if (!this.IsEmpty)
			{
				this.permanentDevice.ScriptID = this.oldValue;
				return true;
			}
			return false;
		}

		// Token: 0x0600194C RID: 6476 RVA: 0x000A6494 File Offset: 0x000A5494
		public bool Redo()
		{
			if (!this.IsEmpty)
			{
				this.permanentDevice.ScriptID = this.newValue;
				return true;
			}
			return false;
		}

		// Token: 0x0600194D RID: 6477 RVA: 0x000A64B2 File Offset: 0x000A54B2
		public void Destroy()
		{
			this.permanentDevice = null;
		}

		// Token: 0x17000617 RID: 1559
		// (get) Token: 0x0600194E RID: 6478 RVA: 0x000A64BB File Offset: 0x000A54BB
		public bool IsEmpty
		{
			get
			{
				return this.permanentDevice == null;
			}
		}

		// Token: 0x17000618 RID: 1560
		// (get) Token: 0x0600194F RID: 6479 RVA: 0x000A64C6 File Offset: 0x000A54C6
		public bool IsAbsolute
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000619 RID: 1561
		// (get) Token: 0x06001950 RID: 6480 RVA: 0x000A64C9 File Offset: 0x000A54C9
		public bool IsHighPriority
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06001951 RID: 6481 RVA: 0x000A64CC File Offset: 0x000A54CC
		public bool ContainsLabel(string label)
		{
			return label == this.Label;
		}

		// Token: 0x1700061A RID: 1562
		// (get) Token: 0x06001952 RID: 6482 RVA: 0x000A64DA File Offset: 0x000A54DA
		public string Label
		{
			get
			{
				return "MapObjectContainer";
			}
		}

		// Token: 0x1700061B RID: 1563
		// (get) Token: 0x06001953 RID: 6483 RVA: 0x000A64E1 File Offset: 0x000A54E1
		public string Description
		{
			get
			{
				return "ChangePermanentDeviceScriptIDOperation";
			}
		}

		// Token: 0x0400103C RID: 4156
		private PermanentDevice permanentDevice;

		// Token: 0x0400103D RID: 4157
		private readonly string oldValue;

		// Token: 0x0400103E RID: 4158
		private readonly string newValue;
	}
}
