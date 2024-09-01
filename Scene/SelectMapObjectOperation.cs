using System;
using Operations;
using Tools.MapObjects;

namespace MapEditor.Scene
{
	// Token: 0x02000190 RID: 400
	internal class SelectMapObjectOperation : IOperation
	{
		// Token: 0x06001315 RID: 4885 RVA: 0x0008CF09 File Offset: 0x0008BF09
		public SelectMapObjectOperation(MapObjectSelector _selector, IMapObject _mapObjectToUnselect, IMapObject _mapObjectToSelect)
		{
			this.selector = _selector;
			this.mapObjectToUnselect = _mapObjectToUnselect;
			this.mapObjectToSelect = _mapObjectToSelect;
		}

		// Token: 0x06001316 RID: 4886 RVA: 0x0008CF26 File Offset: 0x0008BF26
		public bool Undo()
		{
			if (!this.IsEmpty)
			{
				if (this.mapObjectToSelect != null)
				{
					this.selector.Remove(this.mapObjectToSelect);
				}
				if (this.mapObjectToUnselect != null)
				{
					this.selector.Add(this.mapObjectToUnselect);
				}
				return true;
			}
			return false;
		}

		// Token: 0x06001317 RID: 4887 RVA: 0x0008CF66 File Offset: 0x0008BF66
		public bool Redo()
		{
			if (!this.IsEmpty)
			{
				if (this.mapObjectToUnselect != null)
				{
					this.selector.Remove(this.mapObjectToUnselect);
				}
				if (this.mapObjectToSelect != null)
				{
					this.selector.Add(this.mapObjectToSelect);
				}
				return true;
			}
			return false;
		}

		// Token: 0x06001318 RID: 4888 RVA: 0x0008CFA6 File Offset: 0x0008BFA6
		public void Destroy()
		{
			this.selector = null;
			this.mapObjectToUnselect = null;
			this.mapObjectToSelect = null;
		}

		// Token: 0x170003C4 RID: 964
		// (get) Token: 0x06001319 RID: 4889 RVA: 0x0008CFBD File Offset: 0x0008BFBD
		public bool IsEmpty
		{
			get
			{
				return this.selector == null || (this.mapObjectToUnselect == null && this.mapObjectToSelect == null);
			}
		}

		// Token: 0x170003C5 RID: 965
		// (get) Token: 0x0600131A RID: 4890 RVA: 0x0008CFDC File Offset: 0x0008BFDC
		public bool IsAbsolute
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170003C6 RID: 966
		// (get) Token: 0x0600131B RID: 4891 RVA: 0x0008CFDF File Offset: 0x0008BFDF
		public bool IsHighPriority
		{
			get
			{
				return false;
			}
		}

		// Token: 0x0600131C RID: 4892 RVA: 0x0008CFE2 File Offset: 0x0008BFE2
		public bool ContainsLabel(string label)
		{
			return label == this.Label;
		}

		// Token: 0x170003C7 RID: 967
		// (get) Token: 0x0600131D RID: 4893 RVA: 0x0008CFF0 File Offset: 0x0008BFF0
		public string Label
		{
			get
			{
				return "MapObjectSelector";
			}
		}

		// Token: 0x170003C8 RID: 968
		// (get) Token: 0x0600131E RID: 4894 RVA: 0x0008CFF7 File Offset: 0x0008BFF7
		public string Description
		{
			get
			{
				return "MapObjectSelectorOperation";
			}
		}

		// Token: 0x04000DD9 RID: 3545
		private MapObjectSelector selector;

		// Token: 0x04000DDA RID: 3546
		private IMapObject mapObjectToUnselect;

		// Token: 0x04000DDB RID: 3547
		private IMapObject mapObjectToSelect;
	}
}
