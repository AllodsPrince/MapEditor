using System;
using Tools.LinkContainer;
using Tools.MapObjects;

namespace MapEditor.Map.MapObjects
{
	// Token: 0x02000129 RID: 297
	public class SpawnPointLinkChecker : ILinkChecker
	{
		// Token: 0x06000EC3 RID: 3779 RVA: 0x000776EC File Offset: 0x000766EC
		public bool CanLink(IMapObject left, IMapObject right, MapObjectContainer mapObjectContainer)
		{
			if (left != null && right != null)
			{
				if (left.Type.Type == MapObjectFactory.Type.SpawnPoint)
				{
					SpawnPoint _left = left as SpawnPoint;
					if (_left != null && _left.SpawnPointType == SpawnPointType.Patrol)
					{
						if (right.Type.Type == MapObjectFactory.Type.PatrolNode)
						{
							return true;
						}
						if (right.Type.Type == MapObjectFactory.Type.RoutePoint)
						{
							RoutePoint _right = right as RoutePoint;
							if (_right != null && _right.RoutePointType == RoutePointType.PatrolNode)
							{
								return true;
							}
						}
					}
				}
				else if (right.Type.Type == MapObjectFactory.Type.SpawnPoint)
				{
					SpawnPoint _right2 = right as SpawnPoint;
					if (_right2 != null && _right2.SpawnPointType == SpawnPointType.Patrol)
					{
						if (left.Type.Type == MapObjectFactory.Type.PatrolNode)
						{
							return true;
						}
						if (left.Type.Type == MapObjectFactory.Type.RoutePoint)
						{
							RoutePoint _left2 = left as RoutePoint;
							if (_left2 != null && _left2.RoutePointType == RoutePointType.PatrolNode)
							{
								return true;
							}
						}
					}
				}
				else if (left.Type.Type == MapObjectFactory.Type.PatrolNode)
				{
					if (right.Type.Type == MapObjectFactory.Type.PatrolNode)
					{
						return true;
					}
					if (right.Type.Type == MapObjectFactory.Type.RoutePoint)
					{
						RoutePoint _right3 = right as RoutePoint;
						if (_right3 != null && _right3.RoutePointType == RoutePointType.PatrolNode)
						{
							return true;
						}
					}
				}
				else if (right.Type.Type == MapObjectFactory.Type.PatrolNode)
				{
					if (left.Type.Type == MapObjectFactory.Type.PatrolNode)
					{
						return true;
					}
					if (left.Type.Type == MapObjectFactory.Type.RoutePoint)
					{
						RoutePoint _left3 = left as RoutePoint;
						if (_left3 != null && _left3.RoutePointType == RoutePointType.PatrolNode)
						{
							return true;
						}
					}
				}
				if (left.Type.Type == MapObjectFactory.Type.RoutePoint)
				{
					RoutePoint _left4 = left as RoutePoint;
					if (_left4 != null && _left4.RoutePointType == RoutePointType.PatrolNode && _left4.RouteLinks < 2 && right.Type.Type == MapObjectFactory.Type.RoutePoint)
					{
						RoutePoint _right4 = right as RoutePoint;
						if (_right4 != null && _right4.RoutePointType == RoutePointType.PatrolNode && _right4.RouteLinks < 2)
						{
							return true;
						}
					}
				}
			}
			return false;
		}

		// Token: 0x06000EC4 RID: 3780 RVA: 0x00077922 File Offset: 0x00076922
		public ILinkData CreateLinkData(IMapObject left, IMapObject right, MapObjectContainer mapObjectContainer)
		{
			if (this.CanLink(left, right, mapObjectContainer))
			{
				return new PatrolLinkData();
			}
			return null;
		}
	}
}
