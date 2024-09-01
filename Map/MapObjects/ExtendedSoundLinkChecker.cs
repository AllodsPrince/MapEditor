using System;
using System.Collections.Generic;
using Tools.LinkContainer;
using Tools.MapObjects;

namespace MapEditor.Map.MapObjects
{
	// Token: 0x02000172 RID: 370
	public class ExtendedSoundLinkChecker : ILinkChecker
	{
		// Token: 0x060011EA RID: 4586 RVA: 0x00084508 File Offset: 0x00083508
		public bool CanLink(IMapObject left, IMapObject right, MapObjectContainer mapObjectContainer)
		{
			if (left != null && right != null && left.Type.Type == MapObjectFactory.Type.ExtendedSound && right.Type.Type == MapObjectFactory.Type.ExtendedSound && !mapObjectContainer.Linked(left, right, false))
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

		// Token: 0x060011EB RID: 4587 RVA: 0x0008457B File Offset: 0x0008357B
		public ILinkData CreateLinkData(IMapObject left, IMapObject right, MapObjectContainer mapObjectContainer)
		{
			return null;
		}
	}
}
