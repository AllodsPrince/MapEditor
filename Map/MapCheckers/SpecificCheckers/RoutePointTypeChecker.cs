using System;
using System.Collections.Generic;
using MapEditor.Map.MapObjects;
using MapEditor.Resources.Strings;
using Tools.MapObjects;
using Tools.Progress;

namespace MapEditor.Map.MapCheckers.SpecificCheckers
{
	// Token: 0x020002CC RID: 716
	public class RoutePointTypeChecker : MapChecker
	{
		// Token: 0x06002134 RID: 8500 RVA: 0x000D2198 File Offset: 0x000D1198
		public RoutePointTypeChecker()
		{
			base.Name = Strings.ROUTE_CHECKER_NAME;
			base.ShortDescription = string.Format("{0}", RoutePointTypeChecker.yellowCount);
			base.LongDescription = string.Format(Strings.ROUTE_CHECKER_LONG_DESCRIPTION, RoutePointTypeChecker.yellowCount);
		}

		// Token: 0x06002135 RID: 8501 RVA: 0x000D21EC File Offset: 0x000D11EC
		public override void Check(MapEditorMap map, IProgressContainer progressContainer)
		{
			if (progressContainer != null)
			{
				progressContainer.AddString(Strings.RUN_ROUTE_POINT_TYPE_CHECKER);
			}
			base.LongInfoView = new LongInfoViewNode(true);
			foreach (KeyValuePair<int, IMapObject> keyValuePair in map.MapEditorMapObjectContainer.RoutePointContainer.MapObjects)
			{
				RoutePoint routePoint = keyValuePair.Value as RoutePoint;
				if (routePoint != null && !string.IsNullOrEmpty(routePoint.Route))
				{
					base.LongInfoView.FindOrAddNode(routePoint.Route, false).AddNode(routePoint, false);
				}
			}
			int count = base.LongInfoView.BranchCount;
			if (count > RoutePointTypeChecker.redCount)
			{
				base.Status = MapCheckerStatus.Red;
				base.ShortInfo = Strings.CHECKER_EXCEED_RED;
			}
			else if (count > RoutePointTypeChecker.yellowCount)
			{
				base.Status = MapCheckerStatus.Yellow;
				base.ShortInfo = Strings.CHECKER_EXCEED_YELLOW;
			}
			else
			{
				base.Status = MapCheckerStatus.Green;
				base.ShortInfo = Strings.CHECKER_OK;
			}
			base.Status = MapCheckerStatus.Green;
			base.ShortInfo = Strings.CHECKER_OK;
			base.ShortResult = string.Format("{0}", count);
			base.LongResult = string.Format(Strings.ROUTE_CHECKER_LONG_RESULT, count);
			if (progressContainer != null)
			{
				progressContainer.Progress++;
			}
		}

		// Token: 0x0400141D RID: 5149
		private static readonly int yellowCount = 100;

		// Token: 0x0400141E RID: 5150
		private static readonly int redCount = 150;
	}
}
