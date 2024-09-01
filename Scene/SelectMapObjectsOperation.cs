using System;
using System.Collections.Generic;
using Operations;
using Tools.MapObjects;

namespace MapEditor.Scene
{
	// Token: 0x02000191 RID: 401
	internal class SelectMapObjectsOperation : IOperation
	{
		// Token: 0x0600131F RID: 4895 RVA: 0x0008D000 File Offset: 0x0008C000
		public SelectMapObjectsOperation(MapObjectSelector _selector, ICollection<IMapObject> _mapObjectsToUnselect, ICollection<IMapObject> _mapObjectsToSelect)
		{
			this.selector = _selector;
			if (_mapObjectsToUnselect != null && _mapObjectsToUnselect.Count > 0)
			{
				this.mapObjectsToUnselect.AddRange(_mapObjectsToUnselect);
			}
			if (_mapObjectsToSelect != null && _mapObjectsToSelect.Count > 0)
			{
				this.mapObjectsToSelect.AddRange(_mapObjectsToSelect);
			}
		}

		// Token: 0x06001320 RID: 4896 RVA: 0x0008D060 File Offset: 0x0008C060
		public bool Undo()
		{
			if (!this.IsEmpty)
			{
				if (this.mapObjectsToSelect.Count > 0)
				{
					foreach (IMapObject mapObject in this.mapObjectsToSelect)
					{
						this.selector.Remove(mapObject);
					}
				}
				if (this.mapObjectsToUnselect.Count > 0)
				{
					foreach (IMapObject mapObject2 in this.mapObjectsToUnselect)
					{
						this.selector.Add(mapObject2);
					}
				}
				return true;
			}
			return false;
		}

		// Token: 0x06001321 RID: 4897 RVA: 0x0008D12C File Offset: 0x0008C12C
		public bool Redo()
		{
			if (!this.IsEmpty)
			{
				if (this.mapObjectsToUnselect.Count > 0)
				{
					foreach (IMapObject mapObject in this.mapObjectsToUnselect)
					{
						this.selector.Remove(mapObject);
					}
				}
				if (this.mapObjectsToSelect.Count > 0)
				{
					foreach (IMapObject mapObject2 in this.mapObjectsToSelect)
					{
						this.selector.Add(mapObject2);
					}
				}
				return true;
			}
			return false;
		}

		// Token: 0x06001322 RID: 4898 RVA: 0x0008D1F8 File Offset: 0x0008C1F8
		public void Destroy()
		{
			this.selector = null;
			this.mapObjectsToUnselect.Clear();
			this.mapObjectsToSelect.Clear();
		}

		// Token: 0x170003C9 RID: 969
		// (get) Token: 0x06001323 RID: 4899 RVA: 0x0008D217 File Offset: 0x0008C217
		public bool IsEmpty
		{
			get
			{
				return this.selector == null || (this.mapObjectsToUnselect.Count == 0 && this.mapObjectsToSelect.Count == 0);
			}
		}

		// Token: 0x170003CA RID: 970
		// (get) Token: 0x06001324 RID: 4900 RVA: 0x0008D240 File Offset: 0x0008C240
		public bool IsAbsolute
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170003CB RID: 971
		// (get) Token: 0x06001325 RID: 4901 RVA: 0x0008D243 File Offset: 0x0008C243
		public bool IsHighPriority
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06001326 RID: 4902 RVA: 0x0008D246 File Offset: 0x0008C246
		public bool ContainsLabel(string label)
		{
			return label == this.Label;
		}

		// Token: 0x170003CC RID: 972
		// (get) Token: 0x06001327 RID: 4903 RVA: 0x0008D254 File Offset: 0x0008C254
		public string Label
		{
			get
			{
				return "MapObjectSelector";
			}
		}

		// Token: 0x170003CD RID: 973
		// (get) Token: 0x06001328 RID: 4904 RVA: 0x0008D25B File Offset: 0x0008C25B
		public string Description
		{
			get
			{
				return "MapObjectSelectorOperation";
			}
		}

		// Token: 0x04000DDC RID: 3548
		private MapObjectSelector selector;

		// Token: 0x04000DDD RID: 3549
		private readonly List<IMapObject> mapObjectsToUnselect = new List<IMapObject>();

		// Token: 0x04000DDE RID: 3550
		private readonly List<IMapObject> mapObjectsToSelect = new List<IMapObject>();
	}
}
