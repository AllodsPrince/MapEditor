using System;
using System.Collections.Generic;
using Tools.LinkContainer;
using Tools.MapObjects;

namespace MapEditor.Map.MapObjects
{
	// Token: 0x02000064 RID: 100
	public class GraveyardLinkChecker : ILinkChecker
	{
		// Token: 0x06000507 RID: 1287 RVA: 0x00028484 File Offset: 0x00027484
		public bool CanLink(IMapObject left, IMapObject right, MapObjectContainer mapObjectContainer)
		{
			if (left != null && right != null)
			{
				if (left.Type.Type == MapObjectFactory.Type.Graveyard && (right.Type.Type == MapObjectFactory.Type.StaticObject || right.Type.Type == MapObjectFactory.Type.PermanentDevice))
				{
					Dictionary<IMapObject, ILinkData> links = mapObjectContainer.GetLinks(left);
					if (links == null || links.Count == 0)
					{
						return true;
					}
				}
				else if ((left.Type.Type == MapObjectFactory.Type.StaticObject || left.Type.Type == MapObjectFactory.Type.PermanentDevice) && right.Type.Type == MapObjectFactory.Type.Graveyard)
				{
					Dictionary<IMapObject, ILinkData> links2 = mapObjectContainer.GetLinks(right);
					return (links2 != null && links2.Count != 0) || true;
				}
			}
			return false;
		}

		// Token: 0x06000508 RID: 1288 RVA: 0x0002854C File Offset: 0x0002754C
		public ILinkData CreateLinkData(IMapObject left, IMapObject right, MapObjectContainer mapObjectContainer)
		{
			if (this.CanLink(left, right, mapObjectContainer))
			{
				return new GraveyardLinkData();
			}
			return null;
		}
	}
}
