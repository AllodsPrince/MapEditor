using System;
using Operations;

namespace MapEditor.Map.MapObjects
{
	// Token: 0x020001A6 RID: 422
	internal class ChangeZoneLocatorMapZoneOperation : IOperation
	{
		// Token: 0x06001474 RID: 5236 RVA: 0x00093DFB File Offset: 0x00092DFB
		public ChangeZoneLocatorMapZoneOperation(ZoneLocator _zoneLocator, ref string _oldValue, ref string _newValue)
		{
			this.zoneLocator = _zoneLocator;
			this.oldValue = _oldValue;
			this.newValue = _newValue;
		}

		// Token: 0x06001475 RID: 5237 RVA: 0x00093E1A File Offset: 0x00092E1A
		public bool Undo()
		{
			if (!this.IsEmpty)
			{
				this.zoneLocator.MapZone = this.oldValue;
				return true;
			}
			return false;
		}

		// Token: 0x06001476 RID: 5238 RVA: 0x00093E38 File Offset: 0x00092E38
		public bool Redo()
		{
			if (!this.IsEmpty)
			{
				this.zoneLocator.MapZone = this.newValue;
				return true;
			}
			return false;
		}

		// Token: 0x06001477 RID: 5239 RVA: 0x00093E56 File Offset: 0x00092E56
		public void Destroy()
		{
			this.zoneLocator = null;
		}

		// Token: 0x17000406 RID: 1030
		// (get) Token: 0x06001478 RID: 5240 RVA: 0x00093E5F File Offset: 0x00092E5F
		public bool IsEmpty
		{
			get
			{
				return this.zoneLocator == null;
			}
		}

		// Token: 0x17000407 RID: 1031
		// (get) Token: 0x06001479 RID: 5241 RVA: 0x00093E6A File Offset: 0x00092E6A
		public bool IsAbsolute
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000408 RID: 1032
		// (get) Token: 0x0600147A RID: 5242 RVA: 0x00093E6D File Offset: 0x00092E6D
		public bool IsHighPriority
		{
			get
			{
				return false;
			}
		}

		// Token: 0x0600147B RID: 5243 RVA: 0x00093E70 File Offset: 0x00092E70
		public bool ContainsLabel(string label)
		{
			return label == this.Label;
		}

		// Token: 0x17000409 RID: 1033
		// (get) Token: 0x0600147C RID: 5244 RVA: 0x00093E7E File Offset: 0x00092E7E
		public string Label
		{
			get
			{
				return "MapObjectContainer";
			}
		}

		// Token: 0x1700040A RID: 1034
		// (get) Token: 0x0600147D RID: 5245 RVA: 0x00093E85 File Offset: 0x00092E85
		public string Description
		{
			get
			{
				return "ChangeZoneLocatorMapZoneOperation";
			}
		}

		// Token: 0x04000E5F RID: 3679
		private ZoneLocator zoneLocator;

		// Token: 0x04000E60 RID: 3680
		private readonly string oldValue;

		// Token: 0x04000E61 RID: 3681
		private readonly string newValue;
	}
}
