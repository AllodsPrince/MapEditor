using System;
using System.Collections.Generic;
using Tools.LinkContainer;
using Tools.MapObjects;

namespace MapEditor.Map.Containers
{
	// Token: 0x0200025C RID: 604
	public class LinkCollector
	{
		// Token: 0x06001CB6 RID: 7350 RVA: 0x000B70D0 File Offset: 0x000B60D0
		private void CollectRecursive(IMapObject mapObject, int left, ILinkData linkData)
		{
			int right;
			if (this.collectedMapObjects.TryGetValue(mapObject, out right))
			{
				this.links.Add(new LinkCollector.LinkData(left, right, linkData));
				return;
			}
			if (this.mapObjectFilter == null || this.mapObjectFilter.FilterObject(mapObject))
			{
				this.linkedMapObjects.Add(mapObject);
				right = this.linkedMapObjects.Count;
				this.collectedMapObjects.Add(mapObject, right);
				Dictionary<IMapObject, ILinkData> _links = this.mapObjects.GetLinks(mapObject);
				if (_links != null && _links.Count > 0)
				{
					foreach (KeyValuePair<IMapObject, ILinkData> keyValuePair in _links)
					{
						this.CollectRecursive(keyValuePair.Key, right, keyValuePair.Value);
					}
				}
			}
		}

		// Token: 0x06001CB7 RID: 7351 RVA: 0x000B71A4 File Offset: 0x000B61A4
		public LinkCollector(MapObjectContainer _mapObjects, IMapObjectFilter _mapObjectFilter)
		{
			this.mapObjects = _mapObjects;
			this.mapObjectFilter = _mapObjectFilter;
		}

		// Token: 0x06001CB8 RID: 7352 RVA: 0x000B71DB File Offset: 0x000B61DB
		public void Collect(IMapObject mapObject)
		{
			if (this.mapObjects != null)
			{
				this.collectedMapObjects.Clear();
				this.linkedMapObjects.Clear();
				this.links.Clear();
				this.CollectRecursive(mapObject, -1, null);
			}
		}

		// Token: 0x170006CA RID: 1738
		// (get) Token: 0x06001CB9 RID: 7353 RVA: 0x000B720F File Offset: 0x000B620F
		public List<IMapObject> LinkedMapObjects
		{
			get
			{
				return this.linkedMapObjects;
			}
		}

		// Token: 0x170006CB RID: 1739
		// (get) Token: 0x06001CBA RID: 7354 RVA: 0x000B7217 File Offset: 0x000B6217
		public List<LinkCollector.LinkData> Links
		{
			get
			{
				return this.links;
			}
		}

		// Token: 0x0400124D RID: 4685
		private readonly Dictionary<IMapObject, int> collectedMapObjects = new Dictionary<IMapObject, int>();

		// Token: 0x0400124E RID: 4686
		private readonly MapObjectContainer mapObjects;

		// Token: 0x0400124F RID: 4687
		private readonly List<IMapObject> linkedMapObjects = new List<IMapObject>();

		// Token: 0x04001250 RID: 4688
		private readonly List<LinkCollector.LinkData> links = new List<LinkCollector.LinkData>();

		// Token: 0x04001251 RID: 4689
		private readonly IMapObjectFilter mapObjectFilter;

		// Token: 0x0200025D RID: 605
		public struct LinkData
		{
			// Token: 0x06001CBB RID: 7355 RVA: 0x000B721F File Offset: 0x000B621F
			public LinkData(int _left, int _right, object _data)
			{
				this.left = _left;
				this.right = _right;
				this.data = _data;
			}

			// Token: 0x170006CC RID: 1740
			// (get) Token: 0x06001CBC RID: 7356 RVA: 0x000B7236 File Offset: 0x000B6236
			public int Left
			{
				get
				{
					return this.left;
				}
			}

			// Token: 0x170006CD RID: 1741
			// (get) Token: 0x06001CBD RID: 7357 RVA: 0x000B723E File Offset: 0x000B623E
			public int Right
			{
				get
				{
					return this.right;
				}
			}

			// Token: 0x170006CE RID: 1742
			// (get) Token: 0x06001CBE RID: 7358 RVA: 0x000B7246 File Offset: 0x000B6246
			public object Data
			{
				get
				{
					return this.data;
				}
			}

			// Token: 0x04001252 RID: 4690
			private readonly int left;

			// Token: 0x04001253 RID: 4691
			private readonly int right;

			// Token: 0x04001254 RID: 4692
			private readonly object data;
		}
	}
}
