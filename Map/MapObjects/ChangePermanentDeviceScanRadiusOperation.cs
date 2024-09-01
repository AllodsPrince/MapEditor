using System;
using Operations;

namespace MapEditor.Map.MapObjects
{
	// Token: 0x020001FE RID: 510
	internal class ChangePermanentDeviceScanRadiusOperation : IOperation
	{
		// Token: 0x06001954 RID: 6484 RVA: 0x000A64E8 File Offset: 0x000A54E8
		public ChangePermanentDeviceScanRadiusOperation(PermanentDevice _permanentDevice, ref double _oldValue, ref double _newValue)
		{
			this.permanentDevice = _permanentDevice;
			this.oldValue = _oldValue;
			this.newValue = _newValue;
		}

		// Token: 0x06001955 RID: 6485 RVA: 0x000A6507 File Offset: 0x000A5507
		public bool Undo()
		{
			if (!this.IsEmpty)
			{
				this.permanentDevice.ScanRadius = this.oldValue;
				return true;
			}
			return false;
		}

		// Token: 0x06001956 RID: 6486 RVA: 0x000A6525 File Offset: 0x000A5525
		public bool Redo()
		{
			if (!this.IsEmpty)
			{
				this.permanentDevice.ScanRadius = this.newValue;
				return true;
			}
			return false;
		}

		// Token: 0x06001957 RID: 6487 RVA: 0x000A6543 File Offset: 0x000A5543
		public void Destroy()
		{
			this.permanentDevice = null;
		}

		// Token: 0x1700061C RID: 1564
		// (get) Token: 0x06001958 RID: 6488 RVA: 0x000A654C File Offset: 0x000A554C
		public bool IsEmpty
		{
			get
			{
				return this.permanentDevice == null;
			}
		}

		// Token: 0x1700061D RID: 1565
		// (get) Token: 0x06001959 RID: 6489 RVA: 0x000A6557 File Offset: 0x000A5557
		public bool IsAbsolute
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700061E RID: 1566
		// (get) Token: 0x0600195A RID: 6490 RVA: 0x000A655A File Offset: 0x000A555A
		public bool IsHighPriority
		{
			get
			{
				return false;
			}
		}

		// Token: 0x0600195B RID: 6491 RVA: 0x000A655D File Offset: 0x000A555D
		public bool ContainsLabel(string label)
		{
			return label == this.Label;
		}

		// Token: 0x1700061F RID: 1567
		// (get) Token: 0x0600195C RID: 6492 RVA: 0x000A656B File Offset: 0x000A556B
		public string Label
		{
			get
			{
				return "MapObjectContainer";
			}
		}

		// Token: 0x17000620 RID: 1568
		// (get) Token: 0x0600195D RID: 6493 RVA: 0x000A6572 File Offset: 0x000A5572
		public string Description
		{
			get
			{
				return "ChangePermanentDeviceScanRadiusOperation";
			}
		}

		// Token: 0x0400103F RID: 4159
		private PermanentDevice permanentDevice;

		// Token: 0x04001040 RID: 4160
		private readonly double oldValue;

		// Token: 0x04001041 RID: 4161
		private readonly double newValue;
	}
}
