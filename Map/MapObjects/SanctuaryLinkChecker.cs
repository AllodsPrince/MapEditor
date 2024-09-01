using System;
using System.Collections.Generic;
using Tools.LinkContainer;
using Tools.MapObjects;

namespace MapEditor.Map.MapObjects
{
	// Token: 0x020001F6 RID: 502
	public class SanctuaryLinkChecker : ILinkChecker
	{
		// Token: 0x060018FD RID: 6397 RVA: 0x000A5B9C File Offset: 0x000A4B9C
		public bool CanLink(IMapObject left, IMapObject right, MapObjectContainer mapObjectContainer)
		{
			if (left != null && right != null)
			{
				if (left.Type.Type == MapObjectFactory.Type.Sanctuary && (right.Type.Type == MapObjectFactory.Type.StaticObject || right.Type.Type == MapObjectFactory.Type.PermanentDevice))
				{
					Dictionary<IMapObject, ILinkData> links = mapObjectContainer.GetLinks(left);
					if (links == null || links.Count == 0)
					{
						return true;
					}
				}
				else if ((left.Type.Type == MapObjectFactory.Type.StaticObject || left.Type.Type == MapObjectFactory.Type.PermanentDevice) && right.Type.Type == MapObjectFactory.Type.Sanctuary)
				{
					Dictionary<IMapObject, ILinkData> links2 = mapObjectContainer.GetLinks(right);
					if (links2 == null || links2.Count == 0)
					{
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x060018FE RID: 6398 RVA: 0x000A5C62 File Offset: 0x000A4C62
		public ILinkData CreateLinkData(IMapObject left, IMapObject right, MapObjectContainer mapObjectContainer)
		{
			if (this.CanLink(left, right, mapObjectContainer))
			{
				return new SanctuaryLinkData();
			}
			return null;
		}
	}
}
