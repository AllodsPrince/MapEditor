using System;
using System.Collections.Generic;
using Tools.LinkContainer;
using Tools.MapObjects;

namespace MapEditor.Map.MapObjects
{
	// Token: 0x0200013D RID: 317
	internal class SpawnPointLinkCollector
	{
		// Token: 0x06000F35 RID: 3893 RVA: 0x000784D8 File Offset: 0x000774D8
		private bool LinkExists(int left, int right)
		{
			for (int index = 0; index < this.links.Count; index++)
			{
				if (this.links[index].Left == left && this.links[index].Right == right)
				{
					return true;
				}
				if (this.links[index].Left == right && this.links[index].Right == left)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06000F36 RID: 3894 RVA: 0x00078550 File Offset: 0x00077550
		private bool AlreadyCollectedPatrolRoutePoint(IMapObject mapObject)
		{
			int index;
			return this.collectedPatrolNodes.TryGetValue(mapObject, out index) && index == -1;
		}

		// Token: 0x06000F37 RID: 3895 RVA: 0x00078573 File Offset: 0x00077573
		private bool GetPatrolNodeIndex(IMapObject mapObject, out int index)
		{
			index = -1;
			if (SpawnPointLinkFilter.CanLink(mapObject))
			{
				if (!this.collectedPatrolNodes.TryGetValue(mapObject, out index))
				{
					index = -1;
				}
				return true;
			}
			return false;
		}

		// Token: 0x06000F38 RID: 3896 RVA: 0x00078598 File Offset: 0x00077598
		private RoutePoint GetNextPatrolRoute(RoutePoint next, RoutePoint previous, RoutePoint start, bool firstStep, out bool isClosed)
		{
			isClosed = false;
			if (next != null)
			{
				if (!string.IsNullOrEmpty(next.Script))
				{
					return null;
				}
				Dictionary<IMapObject, ILinkData> _links = this.mapObjects.GetLinks(next);
				if (_links != null && _links.Count > 0)
				{
					Dictionary<RoutePoint, ILinkData> _routePoints = new Dictionary<RoutePoint, ILinkData>();
					foreach (KeyValuePair<IMapObject, ILinkData> keyValuePair in _links)
					{
						RoutePoint _routePoint = keyValuePair.Key as RoutePoint;
						if (_routePoint != null && !_routePoint.Temporary)
						{
							_routePoints.Add(_routePoint, keyValuePair.Value);
						}
					}
					if (_routePoints.Count == 1 || _routePoints.Count > 2)
					{
						return null;
					}
					RoutePoint newNext = null;
					foreach (KeyValuePair<RoutePoint, ILinkData> keyValuePair2 in _routePoints)
					{
						if (start != null && object.ReferenceEquals(keyValuePair2.Key, start) && !firstStep)
						{
							isClosed = true;
							return null;
						}
						if (!object.ReferenceEquals(keyValuePair2.Key, previous))
						{
							newNext = keyValuePair2.Key;
						}
					}
					return newNext;
				}
			}
			isClosed = false;
			return null;
		}

		// Token: 0x06000F39 RID: 3897 RVA: 0x000786D8 File Offset: 0x000776D8
		private IMapObject CollectPatrolRoute(RoutePoint start, RoutePoint next, out List<RoutePoint> patrolRoute, out bool isClosed, out PatrolLinkData linkData)
		{
			patrolRoute = null;
			isClosed = false;
			linkData = null;
			if (start != null && next != null)
			{
				patrolRoute = new List<RoutePoint>();
				patrolRoute.Add(start);
				patrolRoute.Add(next);
				for (next = this.GetNextPatrolRoute(patrolRoute[patrolRoute.Count - 1], patrolRoute[patrolRoute.Count - 2], start, true, out isClosed); next != null; next = this.GetNextPatrolRoute(patrolRoute[patrolRoute.Count - 1], patrolRoute[patrolRoute.Count - 2], start, false, out isClosed))
				{
					patrolRoute.Add(next);
				}
				next = patrolRoute[patrolRoute.Count - 1];
				for (int index = 0; index < patrolRoute.Count; index++)
				{
					PatrolLinkData _linkData = null;
					if (index < patrolRoute.Count - 1)
					{
						_linkData = (this.mapObjects.GetLinkData(patrolRoute[index], patrolRoute[index + 1]) as PatrolLinkData);
					}
					else if (isClosed)
					{
						_linkData = (this.mapObjects.GetLinkData(patrolRoute[index], patrolRoute[0]) as PatrolLinkData);
					}
					if (linkData == null)
					{
						linkData = _linkData;
					}
					else if (_linkData != null && _linkData.Weight > linkData.Weight)
					{
						linkData = _linkData;
					}
				}
			}
			return next;
		}

		// Token: 0x06000F3A RID: 3898 RVA: 0x0007881C File Offset: 0x0007781C
		private void CollectRecursiveWithPatrolRoutes(IMapObject mapObject, int _left)
		{
			if (this.mapObjects != null)
			{
				int left = _left;
				bool allowedLink = true;
				if (left == -1)
				{
					allowedLink = this.GetPatrolNodeIndex(mapObject, out left);
				}
				if (allowedLink)
				{
					if (left == -1)
					{
						left = this.patrolNodes.Count;
						this.collectedPatrolNodes.Add(mapObject, this.patrolNodes.Count);
						this.patrolNodes.Add(mapObject);
					}
					Dictionary<IMapObject, ILinkData> _links = this.mapObjects.GetLinks(mapObject);
					if (_links != null && _links.Count > 0)
					{
						List<IMapObject> recurseObjects = new List<IMapObject>();
						List<int> recurseIndices = new List<int>();
						foreach (KeyValuePair<IMapObject, ILinkData> keyValuePair in _links)
						{
							IMapObject linkedMapObject = keyValuePair.Key;
							PatrolLinkData linkData = keyValuePair.Value as PatrolLinkData;
							int right;
							if (!linkedMapObject.Temporary && !this.AlreadyCollectedPatrolRoutePoint(linkedMapObject) && this.GetPatrolNodeIndex(linkedMapObject, out right))
							{
								List<RoutePoint> patrolRoute = null;
								bool isClosed = false;
								if (mapObject.Type.Type == MapObjectFactory.Type.RoutePoint && linkedMapObject.Type.Type == MapObjectFactory.Type.RoutePoint)
								{
									IMapObject newLinkedMapObject = this.CollectPatrolRoute(mapObject as RoutePoint, linkedMapObject as RoutePoint, out patrolRoute, out isClosed, out linkData);
									if (patrolRoute != null && patrolRoute.Count > 1)
									{
										for (int index = 1; index < patrolRoute.Count - 1; index++)
										{
											this.collectedPatrolNodes.Add(patrolRoute[index], -1);
										}
										if (isClosed)
										{
											this.collectedPatrolNodes.Add(patrolRoute[patrolRoute.Count - 1], -1);
											linkedMapObject = mapObject;
											right = left;
										}
										else if (newLinkedMapObject != null && !object.ReferenceEquals(newLinkedMapObject, linkedMapObject))
										{
											linkedMapObject = newLinkedMapObject;
											this.GetPatrolNodeIndex(linkedMapObject, out right);
										}
									}
								}
								if (right == -1)
								{
									right = this.patrolNodes.Count;
									if (!isClosed)
									{
										this.collectedPatrolNodes.Add(linkedMapObject, this.patrolNodes.Count);
										this.patrolNodes.Add(linkedMapObject);
										recurseObjects.Add(linkedMapObject);
										recurseIndices.Add(right);
									}
								}
								if (left != -1 && right != -1 && !this.LinkExists(left, right))
								{
									this.links.Add(new SpawnPointLinkCollector.LinkEntry(left, right, linkData, patrolRoute, isClosed));
								}
							}
						}
						for (int recurseIndex = 0; recurseIndex < recurseObjects.Count; recurseIndex++)
						{
							this.CollectRecursiveWithPatrolRoutes(recurseObjects[recurseIndex], recurseIndices[recurseIndex]);
						}
					}
				}
			}
		}

		// Token: 0x06000F3B RID: 3899 RVA: 0x00078AB4 File Offset: 0x00077AB4
		public SpawnPointLinkCollector(MapObjectContainer _mapObjects)
		{
			this.mapObjects = _mapObjects;
		}

		// Token: 0x06000F3C RID: 3900 RVA: 0x00078AE4 File Offset: 0x00077AE4
		public void Collect(IMapObject mapObject)
		{
			if (this.mapObjects != null)
			{
				this.collectedPatrolNodes.Clear();
				this.patrolNodes.Clear();
				this.links.Clear();
				this.CollectRecursiveWithPatrolRoutes(mapObject, -1);
			}
		}

		// Token: 0x170002EA RID: 746
		// (get) Token: 0x06000F3D RID: 3901 RVA: 0x00078B17 File Offset: 0x00077B17
		public List<IMapObject> PatrolNodes
		{
			get
			{
				return this.patrolNodes;
			}
		}

		// Token: 0x170002EB RID: 747
		// (get) Token: 0x06000F3E RID: 3902 RVA: 0x00078B1F File Offset: 0x00077B1F
		public List<SpawnPointLinkCollector.LinkEntry> Links
		{
			get
			{
				return this.links;
			}
		}

		// Token: 0x04000BAD RID: 2989
		private readonly Dictionary<IMapObject, int> collectedPatrolNodes = new Dictionary<IMapObject, int>();

		// Token: 0x04000BAE RID: 2990
		private readonly MapObjectContainer mapObjects;

		// Token: 0x04000BAF RID: 2991
		private readonly List<IMapObject> patrolNodes = new List<IMapObject>();

		// Token: 0x04000BB0 RID: 2992
		private readonly List<SpawnPointLinkCollector.LinkEntry> links = new List<SpawnPointLinkCollector.LinkEntry>();

		// Token: 0x0200013E RID: 318
		internal class LinkEntry
		{
			// Token: 0x06000F3F RID: 3903 RVA: 0x00078B27 File Offset: 0x00077B27
			public LinkEntry(int _left, int _right, PatrolLinkData _data, List<RoutePoint> _patrolRoute, bool _isClosed)
			{
				this.left = _left;
				this.right = _right;
				this.data = _data;
				this.patrolRoute = _patrolRoute;
				this.isClosed = _isClosed;
			}

			// Token: 0x170002EC RID: 748
			// (get) Token: 0x06000F40 RID: 3904 RVA: 0x00078B62 File Offset: 0x00077B62
			// (set) Token: 0x06000F41 RID: 3905 RVA: 0x00078B6A File Offset: 0x00077B6A
			public int Left
			{
				get
				{
					return this.left;
				}
				set
				{
					this.left = value;
				}
			}

			// Token: 0x170002ED RID: 749
			// (get) Token: 0x06000F42 RID: 3906 RVA: 0x00078B73 File Offset: 0x00077B73
			// (set) Token: 0x06000F43 RID: 3907 RVA: 0x00078B7B File Offset: 0x00077B7B
			public int Right
			{
				get
				{
					return this.right;
				}
				set
				{
					this.right = value;
				}
			}

			// Token: 0x170002EE RID: 750
			// (get) Token: 0x06000F44 RID: 3908 RVA: 0x00078B84 File Offset: 0x00077B84
			// (set) Token: 0x06000F45 RID: 3909 RVA: 0x00078B8C File Offset: 0x00077B8C
			public PatrolLinkData Data
			{
				get
				{
					return this.data;
				}
				set
				{
					this.data = value;
				}
			}

			// Token: 0x170002EF RID: 751
			// (get) Token: 0x06000F46 RID: 3910 RVA: 0x00078B95 File Offset: 0x00077B95
			// (set) Token: 0x06000F47 RID: 3911 RVA: 0x00078B9D File Offset: 0x00077B9D
			public List<RoutePoint> PatrolRoute
			{
				get
				{
					return this.patrolRoute;
				}
				set
				{
					this.patrolRoute = value;
				}
			}

			// Token: 0x170002F0 RID: 752
			// (get) Token: 0x06000F48 RID: 3912 RVA: 0x00078BA6 File Offset: 0x00077BA6
			// (set) Token: 0x06000F49 RID: 3913 RVA: 0x00078BAE File Offset: 0x00077BAE
			public bool Closed
			{
				get
				{
					return this.isClosed;
				}
				set
				{
					this.isClosed = value;
				}
			}

			// Token: 0x04000BB1 RID: 2993
			private int left = -1;

			// Token: 0x04000BB2 RID: 2994
			private int right = -1;

			// Token: 0x04000BB3 RID: 2995
			private PatrolLinkData data;

			// Token: 0x04000BB4 RID: 2996
			private List<RoutePoint> patrolRoute;

			// Token: 0x04000BB5 RID: 2997
			private bool isClosed;
		}
	}
}
