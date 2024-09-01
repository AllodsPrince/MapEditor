using System;
using Tools.LinkContainer;
using Tools.MapObjects;

namespace MapEditor.Map.MapObjects
{
	// Token: 0x02000085 RID: 133
	public class RoutePointLinkChecker : ILinkChecker
	{
		// Token: 0x0600064C RID: 1612 RVA: 0x00034A24 File Offset: 0x00033A24
		public bool CanLink(IMapObject left, IMapObject right, MapObjectContainer mapObjectContainer)
		{
			if (left != null && right != null && left.Type.Type == MapObjectFactory.Type.RoutePoint && right.Type.Type == MapObjectFactory.Type.RoutePoint)
			{
				RoutePoint _left = left as RoutePoint;
				RoutePoint _right = right as RoutePoint;
				if (_left != null && _right != null && _left.RoutePointType == RoutePointType.Simple && _right.RoutePointType == RoutePointType.Simple && string.Equals(_left.Route, _right.Route, StringComparison.OrdinalIgnoreCase) && _left.RouteLinks < 2 && _right.RouteLinks < 2)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x0600064D RID: 1613 RVA: 0x00034AB0 File Offset: 0x00033AB0
		public ILinkData CreateLinkData(IMapObject left, IMapObject right, MapObjectContainer mapObjectContainer)
		{
			return null;
		}
	}
}
