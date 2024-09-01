using System;
using System.Collections.Generic;
using Tools.LinkContainer;
using Tools.MapObjects;

namespace MapEditor.Map.MapObjects
{
	// Token: 0x02000039 RID: 57
	public class ClientPatrolNodeLinkChecker : ILinkChecker
	{
		// Token: 0x0600036A RID: 874 RVA: 0x0001FA40 File Offset: 0x0001EA40
		public bool CanLink(IMapObject left, IMapObject right, MapObjectContainer mapObjectContainer)
		{
			if (left != null && right != null && left.Type.Type == MapObjectFactory.Type.ClientPatrolNode && right.Type.Type == MapObjectFactory.Type.ClientPatrolNode && !mapObjectContainer.Linked(left, right, true))
			{
				Dictionary<IMapObject, ILinkData> leftLinks = mapObjectContainer.GetLinks(left);
				Dictionary<IMapObject, ILinkData> rightLinks = mapObjectContainer.GetLinks(right);
				if ((leftLinks == null || leftLinks.Count < 2) && (rightLinks == null || rightLinks.Count < 2))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x0600036B RID: 875 RVA: 0x0001FAB3 File Offset: 0x0001EAB3
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
