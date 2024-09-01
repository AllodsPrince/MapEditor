using System;
using System.Collections.Generic;
using MapEditor.Map.MapObjects;
using Operations;
using Tools.LinkContainer;
using Tools.MapObjects;

namespace MapEditor.Map.Containers
{
	// Token: 0x0200023B RID: 571
	public class RoutePointContainer : TypedMapObjectContainer
	{
		// Token: 0x17000672 RID: 1650
		// (get) Token: 0x06001B31 RID: 6961 RVA: 0x000B0935 File Offset: 0x000AF935
		// (set) Token: 0x06001B30 RID: 6960 RVA: 0x000B092D File Offset: 0x000AF92D
		public static bool AllowRouteLinks
		{
			get
			{
				return RoutePointContainer.allowRouteLinks;
			}
			set
			{
				RoutePointContainer.allowRouteLinks = value;
			}
		}

		// Token: 0x17000673 RID: 1651
		// (get) Token: 0x06001B33 RID: 6963 RVA: 0x000B0944 File Offset: 0x000AF944
		// (set) Token: 0x06001B32 RID: 6962 RVA: 0x000B093C File Offset: 0x000AF93C
		public static bool AllowRouteAdvance
		{
			get
			{
				return RoutePointContainer.allowRouteAdvance;
			}
			set
			{
				RoutePointContainer.allowRouteAdvance = value;
			}
		}

		// Token: 0x06001B34 RID: 6964 RVA: 0x000B094B File Offset: 0x000AF94B
		private static void IncreaseRouteLinks(RoutePoint routePoint)
		{
			if (routePoint != null)
			{
				routePoint.RouteLinks++;
			}
		}

		// Token: 0x06001B35 RID: 6965 RVA: 0x000B095E File Offset: 0x000AF95E
		private static void DecreaseRouteLinks(RoutePoint routePoint)
		{
			if (routePoint != null)
			{
				routePoint.RouteLinks--;
			}
		}

		// Token: 0x06001B36 RID: 6966 RVA: 0x000B0974 File Offset: 0x000AF974
		private static void SetRouteAdvance(LinkContainer<IMapObject> links, RoutePoint left, RoutePoint right, RoutePoint start)
		{
			Dictionary<IMapObject, ILinkData> leftLinks = links.GetLinks(left);
			Dictionary<IMapObject, ILinkData> rightLinks = links.GetLinks(right);
			if (leftLinks != null && rightLinks != null)
			{
				int leftLinksCount = 0;
				int rightLinksCount = 0;
				foreach (KeyValuePair<IMapObject, ILinkData> keyValuePair in leftLinks)
				{
					if (keyValuePair.Key.Type.Type == MapObjectFactory.Type.RoutePoint)
					{
						leftLinksCount++;
					}
				}
				foreach (KeyValuePair<IMapObject, ILinkData> keyValuePair2 in rightLinks)
				{
					if (keyValuePair2.Key.Type.Type == MapObjectFactory.Type.RoutePoint)
					{
						rightLinksCount++;
					}
				}
				if (leftLinksCount > 0 && rightLinksCount > 0)
				{
					if (leftLinksCount == 1 && rightLinksCount == 1)
					{
						left.RouteAdvance = 0.0;
						right.RouteAdvance = 1.0;
						return;
					}
					if (leftLinksCount > 1 && rightLinksCount == 1)
					{
						RoutePoint _other = MapObjectContainer.GetOtherLinkedMapObject(leftLinks, MapObjectFactory.Type.RoutePoint, right, false) as RoutePoint;
						if (_other != null)
						{
							right.RouteAdvance = 2.0 * left.RouteAdvance - _other.RouteAdvance;
							return;
						}
					}
					else if (leftLinksCount == 1 && rightLinksCount > 1)
					{
						RoutePoint _other2 = MapObjectContainer.GetOtherLinkedMapObject(rightLinks, MapObjectFactory.Type.RoutePoint, left, false) as RoutePoint;
						if (_other2 != null)
						{
							left.RouteAdvance = 2.0 * right.RouteAdvance - _other2.RouteAdvance;
							return;
						}
					}
					else
					{
						RoutePoint _otherLeft = MapObjectContainer.GetOtherLinkedMapObject(leftLinks, MapObjectFactory.Type.RoutePoint, right, false) as RoutePoint;
						if (_otherLeft != null)
						{
							right.RouteAdvance = 2.0 * left.RouteAdvance - _otherLeft.RouteAdvance;
						}
						RoutePoint _otherRight = MapObjectContainer.GetOtherLinkedMapObject(rightLinks, MapObjectFactory.Type.RoutePoint, left, false) as RoutePoint;
						if (_otherRight != null && _otherRight != start)
						{
							RoutePointContainer.SetRouteAdvance(links, right, _otherRight, start);
						}
					}
				}
			}
		}

		// Token: 0x06001B37 RID: 6967 RVA: 0x000B0B68 File Offset: 0x000AFB68
		private void OnPreLinkAdded(MapObjectContainer _mapObjectContainer, LinkContainer<IMapObject> links, IMapObject left, IMapObject right, object data)
		{
			if (this.operationContainer != null && this.operationContainer.DoesUndoRedoInProgress)
			{
				return;
			}
			if (left != null && right != null && left.Type.Type == MapObjectFactory.Type.RoutePoint && right.Type.Type == MapObjectFactory.Type.RoutePoint)
			{
				RoutePoint _left = left as RoutePoint;
				RoutePoint _right = right as RoutePoint;
				if (_left != null && _right != null)
				{
					if (RoutePointContainer.allowRouteLinks)
					{
						RoutePointContainer.IncreaseRouteLinks(_left);
						RoutePointContainer.IncreaseRouteLinks(_right);
					}
					if (RoutePointContainer.allowRouteAdvance)
					{
						if (_left.RouteAdvance >= _right.RouteAdvance)
						{
							RoutePointContainer.SetRouteAdvance(links, _left, _right, _left);
							return;
						}
						RoutePointContainer.SetRouteAdvance(links, _right, _left, _right);
					}
				}
			}
		}

		// Token: 0x06001B38 RID: 6968 RVA: 0x000B0C10 File Offset: 0x000AFC10
		private void OnPreLinkRemoved(MapObjectContainer _mapObjectContainer, LinkContainer<IMapObject> links, IMapObject left, IMapObject right, object data)
		{
			if (this.operationContainer != null && this.operationContainer.DoesUndoRedoInProgress)
			{
				return;
			}
			if (left != null && right != null && left.Type.Type == MapObjectFactory.Type.RoutePoint && right.Type.Type == MapObjectFactory.Type.RoutePoint)
			{
				RoutePoint _left = left as RoutePoint;
				RoutePoint _right = right as RoutePoint;
				if (_left != null && _right != null)
				{
					if (RoutePointContainer.allowRouteLinks)
					{
						RoutePointContainer.DecreaseRouteLinks(_left);
						RoutePointContainer.DecreaseRouteLinks(_right);
					}
					if (RoutePointContainer.allowRouteAdvance && MapObjectContainer.Linked(links, _left, _right, true))
					{
						if (RoutePoint.RouteAdvanceIsDirect(_left, _right))
						{
							Dictionary<IMapObject, ILinkData> _rightLinks = links.GetLinks(_right);
							RoutePoint _otherRight = MapObjectContainer.GetOtherLinkedMapObject(_rightLinks, MapObjectFactory.Type.RoutePoint, _left, false) as RoutePoint;
							if (_otherRight != null)
							{
								_right.RouteAdvance = 0.0;
								_otherRight.RouteAdvance = 1.0;
								_rightLinks = links.GetLinks(_otherRight);
								RoutePoint _otherOtherRight = MapObjectContainer.GetOtherLinkedMapObject(_rightLinks, MapObjectFactory.Type.RoutePoint, _right, false) as RoutePoint;
								RoutePointContainer.SetRouteAdvance(links, _otherRight, _otherOtherRight, _otherRight);
								return;
							}
						}
						else
						{
							Dictionary<IMapObject, ILinkData> _leftLinks = links.GetLinks(_left);
							RoutePoint _otherLeft = MapObjectContainer.GetOtherLinkedMapObject(_leftLinks, MapObjectFactory.Type.RoutePoint, _right, false) as RoutePoint;
							if (_otherLeft != null)
							{
								_left.RouteAdvance = 0.0;
								_otherLeft.RouteAdvance = 1.0;
								_leftLinks = links.GetLinks(_otherLeft);
								RoutePoint _otherOtherLeft = MapObjectContainer.GetOtherLinkedMapObject(_leftLinks, MapObjectFactory.Type.RoutePoint, _left, false) as RoutePoint;
								RoutePointContainer.SetRouteAdvance(links, _otherLeft, _otherOtherLeft, _otherLeft);
							}
						}
					}
				}
			}
		}

		// Token: 0x06001B39 RID: 6969 RVA: 0x000B0D94 File Offset: 0x000AFD94
		private void OnRoutePointRouteChanged(RoutePoint routePoint, ref string oldValue, ref string newValue)
		{
			if (this.mapEditorMapObjectContainer != null && base.Contains(routePoint))
			{
				if (this.RoutePointRouteChanged != null)
				{
					this.RoutePointRouteChanged(this.mapEditorMapObjectContainer, routePoint, ref oldValue, ref newValue);
				}
				if (!routePoint.Temporary)
				{
					this.mapEditorMapObjectContainer.InvokeModified();
				}
			}
		}

		// Token: 0x06001B3A RID: 6970 RVA: 0x000B0DE4 File Offset: 0x000AFDE4
		private void OnRoutePointTypeChanged(RoutePoint routePoint, ref RoutePointType oldValue, ref RoutePointType newValue)
		{
			if (this.mapEditorMapObjectContainer != null && base.Contains(routePoint))
			{
				if (this.RoutePointTypeChanged != null)
				{
					this.RoutePointTypeChanged(this.mapEditorMapObjectContainer, routePoint, ref oldValue, ref newValue);
				}
				if (!routePoint.Temporary)
				{
					this.mapEditorMapObjectContainer.InvokeModified();
				}
			}
		}

		// Token: 0x06001B3B RID: 6971 RVA: 0x000B0E34 File Offset: 0x000AFE34
		private void OnRoutePointSpeedChanged(RoutePoint routePoint, ref double oldValue, ref double newValue)
		{
			if (this.mapEditorMapObjectContainer != null && base.Contains(routePoint))
			{
				if (this.RoutePointSpeedChanged != null)
				{
					this.RoutePointSpeedChanged(this.mapEditorMapObjectContainer, routePoint, ref oldValue, ref newValue);
				}
				if (!routePoint.Temporary)
				{
					this.mapEditorMapObjectContainer.InvokeModified();
				}
			}
		}

		// Token: 0x06001B3C RID: 6972 RVA: 0x000B0E84 File Offset: 0x000AFE84
		private void OnRoutePointLatencyChanged(RoutePoint routePoint, ref double oldValue, ref double newValue)
		{
			if (this.mapEditorMapObjectContainer != null && base.Contains(routePoint))
			{
				if (this.RoutePointLatencyChanged != null)
				{
					this.RoutePointLatencyChanged(this.mapEditorMapObjectContainer, routePoint, ref oldValue, ref newValue);
				}
				if (!routePoint.Temporary)
				{
					this.mapEditorMapObjectContainer.InvokeModified();
				}
			}
		}

		// Token: 0x06001B3D RID: 6973 RVA: 0x000B0ED4 File Offset: 0x000AFED4
		private void OnRoutePointAggroRadiusChanged(RoutePoint routePoint, ref double oldValue, ref double newValue)
		{
			if (this.mapEditorMapObjectContainer != null && base.Contains(routePoint))
			{
				if (this.RoutePointAggroRadiusChanged != null)
				{
					this.RoutePointAggroRadiusChanged(this.mapEditorMapObjectContainer, routePoint, ref oldValue, ref newValue);
				}
				if (!routePoint.Temporary)
				{
					this.mapEditorMapObjectContainer.InvokeModified();
				}
			}
		}

		// Token: 0x06001B3E RID: 6974 RVA: 0x000B0F24 File Offset: 0x000AFF24
		private void OnRoutePointLabelChanged(RoutePoint routePoint, ref string oldValue, ref string newValue)
		{
			if (this.mapEditorMapObjectContainer != null && base.Contains(routePoint))
			{
				if (this.RoutePointLabelChanged != null)
				{
					this.RoutePointLabelChanged(this.mapEditorMapObjectContainer, routePoint, ref oldValue, ref newValue);
				}
				if (!routePoint.Temporary)
				{
					this.mapEditorMapObjectContainer.InvokeModified();
				}
			}
		}

		// Token: 0x06001B3F RID: 6975 RVA: 0x000B0F74 File Offset: 0x000AFF74
		private void OnRoutePointScriptChanged(RoutePoint routePoint, ref string oldValue, ref string newValue)
		{
			if (this.mapEditorMapObjectContainer != null && base.Contains(routePoint))
			{
				if (this.RoutePointScriptChanged != null)
				{
					this.RoutePointScriptChanged(this.mapEditorMapObjectContainer, routePoint, ref oldValue, ref newValue);
				}
				if (!routePoint.Temporary)
				{
					this.mapEditorMapObjectContainer.InvokeModified();
				}
			}
		}

		// Token: 0x140000BD RID: 189
		// (add) Token: 0x06001B40 RID: 6976 RVA: 0x000B0FC1 File Offset: 0x000AFFC1
		// (remove) Token: 0x06001B41 RID: 6977 RVA: 0x000B0FDA File Offset: 0x000AFFDA
		public event RoutePointContainer.RoutePointFieldChangedEvent<string> RoutePointRouteChanged;

		// Token: 0x140000BE RID: 190
		// (add) Token: 0x06001B42 RID: 6978 RVA: 0x000B0FF3 File Offset: 0x000AFFF3
		// (remove) Token: 0x06001B43 RID: 6979 RVA: 0x000B100C File Offset: 0x000B000C
		public event RoutePointContainer.RoutePointFieldChangedEvent<RoutePointType> RoutePointTypeChanged;

		// Token: 0x140000BF RID: 191
		// (add) Token: 0x06001B44 RID: 6980 RVA: 0x000B1025 File Offset: 0x000B0025
		// (remove) Token: 0x06001B45 RID: 6981 RVA: 0x000B103E File Offset: 0x000B003E
		public event RoutePointContainer.RoutePointFieldChangedEvent<double> RoutePointSpeedChanged;

		// Token: 0x140000C0 RID: 192
		// (add) Token: 0x06001B46 RID: 6982 RVA: 0x000B1057 File Offset: 0x000B0057
		// (remove) Token: 0x06001B47 RID: 6983 RVA: 0x000B1070 File Offset: 0x000B0070
		public event RoutePointContainer.RoutePointFieldChangedEvent<double> RoutePointLatencyChanged;

		// Token: 0x140000C1 RID: 193
		// (add) Token: 0x06001B48 RID: 6984 RVA: 0x000B1089 File Offset: 0x000B0089
		// (remove) Token: 0x06001B49 RID: 6985 RVA: 0x000B10A2 File Offset: 0x000B00A2
		public event RoutePointContainer.RoutePointFieldChangedEvent<double> RoutePointAggroRadiusChanged;

		// Token: 0x140000C2 RID: 194
		// (add) Token: 0x06001B4A RID: 6986 RVA: 0x000B10BB File Offset: 0x000B00BB
		// (remove) Token: 0x06001B4B RID: 6987 RVA: 0x000B10D4 File Offset: 0x000B00D4
		public event RoutePointContainer.RoutePointFieldChangedEvent<string> RoutePointLabelChanged;

		// Token: 0x140000C3 RID: 195
		// (add) Token: 0x06001B4C RID: 6988 RVA: 0x000B10ED File Offset: 0x000B00ED
		// (remove) Token: 0x06001B4D RID: 6989 RVA: 0x000B1106 File Offset: 0x000B0106
		public event RoutePointContainer.RoutePointFieldChangedEvent<string> RoutePointScriptChanged;

		// Token: 0x06001B4E RID: 6990 RVA: 0x000B1120 File Offset: 0x000B0120
		public RoutePointContainer(MapEditorMapObjectContainer _mapEditorMapObjectContainer, OperationContainer _operationContainer) : base(MapObjectFactory.Type.RoutePoint, false)
		{
			this.mapEditorMapObjectContainer = _mapEditorMapObjectContainer;
			this.operationContainer = _operationContainer;
			if (this.mapEditorMapObjectContainer != null)
			{
				RoutePoint.RouteChanged += this.OnRoutePointRouteChanged;
				RoutePoint.RoutePointTypeChanged += this.OnRoutePointTypeChanged;
				RoutePoint.SpeedChanged += this.OnRoutePointSpeedChanged;
				RoutePoint.LatencyChanged += this.OnRoutePointLatencyChanged;
				RoutePoint.AggroRadiusChanged += this.OnRoutePointAggroRadiusChanged;
				RoutePoint.LabelChanged += this.OnRoutePointLabelChanged;
				RoutePoint.ScriptChanged += this.OnRoutePointScriptChanged;
				this.mapEditorMapObjectContainer.PreLinkAdded += new MapObjectContainer.LinkEvent(this.OnPreLinkAdded);
				this.mapEditorMapObjectContainer.PreLinkRemoved += new MapObjectContainer.LinkEvent(this.OnPreLinkRemoved);
				base.Destroyed += this.OnDestroyed;
			}
		}

		// Token: 0x06001B4F RID: 6991 RVA: 0x000B120C File Offset: 0x000B020C
		public void OnDestroyed(TypedMapObjectContainer _typedMapObjectContainer)
		{
			if (this.mapEditorMapObjectContainer != null)
			{
				RoutePoint.RouteChanged -= this.OnRoutePointRouteChanged;
				RoutePoint.RoutePointTypeChanged -= this.OnRoutePointTypeChanged;
				RoutePoint.SpeedChanged -= this.OnRoutePointSpeedChanged;
				RoutePoint.LatencyChanged -= this.OnRoutePointLatencyChanged;
				RoutePoint.AggroRadiusChanged -= this.OnRoutePointAggroRadiusChanged;
				RoutePoint.LabelChanged -= this.OnRoutePointLabelChanged;
				RoutePoint.ScriptChanged -= this.OnRoutePointScriptChanged;
				this.mapEditorMapObjectContainer.PreLinkAdded -= new MapObjectContainer.LinkEvent(this.OnPreLinkAdded);
				this.mapEditorMapObjectContainer.PreLinkRemoved -= new MapObjectContainer.LinkEvent(this.OnPreLinkRemoved);
				base.Destroyed -= this.OnDestroyed;
				this.mapEditorMapObjectContainer = null;
			}
			this.operationContainer = null;
		}

		// Token: 0x04001162 RID: 4450
		private static bool allowRouteLinks = true;

		// Token: 0x04001163 RID: 4451
		private static bool allowRouteAdvance = true;

		// Token: 0x04001164 RID: 4452
		private MapEditorMapObjectContainer mapEditorMapObjectContainer;

		// Token: 0x04001165 RID: 4453
		private OperationContainer operationContainer;

		// Token: 0x0200023C RID: 572
		// (Invoke) Token: 0x06001B52 RID: 6994
		public delegate void RoutePointFieldChangedEvent<T>(MapEditorMapObjectContainer _mapEditorMapObjectContainer, RoutePoint routePoint, ref T oldValue, ref T newValue);
	}
}
