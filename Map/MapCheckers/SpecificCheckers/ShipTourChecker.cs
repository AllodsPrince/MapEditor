using System;
using System.Collections.Generic;
using Db;
using MapEditor.Map.MapObjects;
using MapEditor.Resources.Strings;
using Tools.Geometry;
using Tools.MapObjects;
using Tools.Progress;

namespace MapEditor.Map.MapCheckers.SpecificCheckers
{
	// Token: 0x02000234 RID: 564
	public class ShipTourChecker : MapChecker
	{
		// Token: 0x06001AFE RID: 6910 RVA: 0x000AF57C File Offset: 0x000AE57C
		private static bool SegmentIntersectsAxeAlignSegment(int axeIndex, Vec2 begin, Vec2 end, int e0, int e1, int value)
		{
			Vec2 d = end - begin;
			int altIndex = (axeIndex == 0) ? 1 : 0;
			if (Math.Abs(d[altIndex]) < MathConsts.DOUBLE_EPSILON)
			{
				double t = ((double)value - begin[altIndex]) / d[altIndex];
				if (t > 0.0 || t < 1.0)
				{
					double _value = begin[axeIndex] + d[axeIndex] * t;
					return _value > (double)e0 && _value < (double)e1;
				}
			}
			return false;
		}

		// Token: 0x06001AFF RID: 6911 RVA: 0x000AF600 File Offset: 0x000AE600
		private static bool RectIntersectsSqure(int squareX_0, int squareY_0, int squareSize, Vec2 p0, Vec2 p1, Vec2 p2, Vec2 p3)
		{
			Vec2[] pVertexes = new Vec2[]
			{
				p0,
				p1,
				p2,
				p3
			};
			int squareX_ = squareX_0 + squareSize;
			int squareY_ = squareY_0 + squareSize;
			foreach (Vec2 pVertex in pVertexes)
			{
				if (pVertex.X >= (double)squareX_0 && pVertex.X <= (double)squareX_ && pVertex.Y >= (double)squareY_0 && pVertex.Y <= (double)squareY_)
				{
					return true;
				}
			}
			for (int pEdgeIndex = 0; pEdgeIndex < 4; pEdgeIndex++)
			{
				Vec2 firstVertex = pVertexes[pEdgeIndex];
				Vec2 secondVertex = (pEdgeIndex != 3) ? pVertexes[pEdgeIndex + 1] : pVertexes[0];
				if (ShipTourChecker.SegmentIntersectsAxeAlignSegment(0, firstVertex, secondVertex, squareX_0, squareX_, squareY_0))
				{
					return true;
				}
				if (ShipTourChecker.SegmentIntersectsAxeAlignSegment(0, firstVertex, secondVertex, squareX_0, squareX_, squareY_))
				{
					return true;
				}
				if (ShipTourChecker.SegmentIntersectsAxeAlignSegment(1, firstVertex, secondVertex, squareY_0, squareY_, squareX_0))
				{
					return true;
				}
				if (ShipTourChecker.SegmentIntersectsAxeAlignSegment(1, firstVertex, secondVertex, squareY_0, squareY_, squareX_))
				{
					return true;
				}
			}
			Vec2[] squareVertexes = new Vec2[]
			{
				new Vec2((double)squareX_0, (double)squareY_0),
				new Vec2((double)squareX_0, (double)squareY_),
				new Vec2((double)squareX_, (double)squareY_),
				new Vec2((double)squareX_, (double)squareY_0)
			};
			Vec2[] array2 = squareVertexes;
			int num = 0;
			if (num >= array2.Length)
			{
				return false;
			}
			Vec2 squareVertex = array2[num];
			for (int index = 0; index < 4; index++)
			{
				Vec2 prevVertex = (index != 0) ? pVertexes[index - 1] : pVertexes[3];
				Vec2 a = prevVertex - pVertexes[index];
				Vec2 nextVertex = (index != 3) ? pVertexes[index + 1] : pVertexes[0];
				Vec2 b = nextVertex - pVertexes[index];
				Vec2 v = squareVertex - pVertexes[index];
				double det = Vec2.Det(a, b);
				if (Math.Abs(det) < MathConsts.DOUBLE_EPSILON)
				{
					return false;
				}
				bool sign = det > 0.0;
				bool sign2 = Vec2.Det(a, v) > 0.0;
				if (sign != sign2)
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06001B00 RID: 6912 RVA: 0x000AF8C0 File Offset: 0x000AE8C0
		private static Vec2 TranslateToMapCoord(MapEditorMap map, Vec2 v)
		{
			return new Vec2(v.X - (double)(map.Data.MinXMinYPatchCoords.X * Constants.PatchSize), v.Y - (double)(map.Data.MinXMinYPatchCoords.Y * Constants.PatchSize));
		}

		// Token: 0x06001B01 RID: 6913 RVA: 0x000AF918 File Offset: 0x000AE918
		private static bool CheckRect(MapEditorMap map, Vec2 p0, Vec2 p1, Vec2 p2, Vec2 p3, int zoneTileCount)
		{
			bool translated = false;
			Vec2 q0 = Vec2.Empty;
			Vec2 q = Vec2.Empty;
			Vec2 q2 = Vec2.Empty;
			Vec2 q3 = Vec2.Empty;
			for (int tileX = 0; tileX < zoneTileCount; tileX++)
			{
				for (int tileY = 0; tileY < zoneTileCount; tileY++)
				{
					if (string.IsNullOrEmpty(map.MapZoneContainer.GetZone(tileX, tileY)))
					{
						if (!translated)
						{
							q0 = ShipTourChecker.TranslateToMapCoord(map, p0);
							q = ShipTourChecker.TranslateToMapCoord(map, p1);
							q2 = ShipTourChecker.TranslateToMapCoord(map, p2);
							q3 = ShipTourChecker.TranslateToMapCoord(map, p3);
							translated = true;
						}
						if (ShipTourChecker.RectIntersectsSqure(tileX * Constants.MapZonePieceSize.X - 2, tileY * Constants.MapZonePieceSize.Y - 2, Constants.MapZonePieceSize.X + 4, q0, q, q2, q3))
						{
							return false;
						}
					}
				}
			}
			return true;
		}

		// Token: 0x06001B02 RID: 6914 RVA: 0x000AF9F0 File Offset: 0x000AE9F0
		private static bool CheckPoint(MapEditorMap map, Vec3 point, double radius, int zoneTileCount)
		{
			Vec2 p0 = new Vec2(point.X - radius, point.Y - radius);
			Vec2 p = new Vec2(point.X - radius, point.Y + radius);
			Vec2 p2 = new Vec2(point.X + radius, point.Y + radius);
			Vec2 p3 = new Vec2(point.X + radius, point.Y - radius);
			return ShipTourChecker.CheckRect(map, p0, p, p2, p3, zoneTileCount);
		}

		// Token: 0x06001B03 RID: 6915 RVA: 0x000AFA6C File Offset: 0x000AEA6C
		private static bool CheckSegment(MapEditorMap map, Vec3 startPoint, Vec3 endPoint, double radius, int zoneTileCount)
		{
			if (!ShipTourChecker.CheckPoint(map, startPoint, radius, zoneTileCount))
			{
				return false;
			}
			if (!ShipTourChecker.CheckPoint(map, endPoint, radius, zoneTileCount))
			{
				return false;
			}
			Vec2 startPoint2 = new Vec2(startPoint.X, startPoint.Y);
			Vec2 endPoint2 = new Vec2(endPoint.X, endPoint.Y);
			Vec2 middleLine = startPoint2 - endPoint2;
			if (Math.Abs(middleLine.X) + Math.Abs(middleLine.Y) > 0.0)
			{
				Vec2 normalLine = new Vec2(-middleLine.Y, middleLine.X);
				normalLine.Normalize();
				normalLine *= radius;
				Vec2 p0 = startPoint2 + normalLine;
				Vec2 p = endPoint2 + normalLine;
				Vec2 p2 = endPoint2 - normalLine;
				Vec2 p3 = startPoint2 + normalLine;
				return ShipTourChecker.CheckRect(map, p0, p, p2, p3, zoneTileCount);
			}
			return ShipTourChecker.CheckPoint(map, startPoint, radius, zoneTileCount);
		}

		// Token: 0x06001B04 RID: 6916 RVA: 0x000AFB50 File Offset: 0x000AEB50
		private static Vec3 GetNodeValue(Vec3 x0, Vec3 x1, Vec3 dx0, Vec3 dx1, int nodesCnt, double intervalLen, int nodeIndex)
		{
			if (nodeIndex == 0)
			{
				return x0;
			}
			if (nodeIndex == nodesCnt - 1)
			{
				return x1;
			}
			double t = intervalLen * (double)nodeIndex;
			return ShipTourChecker.GetSplineValue(t, x0, x1, dx0, dx1);
		}

		// Token: 0x06001B05 RID: 6917 RVA: 0x000AFB80 File Offset: 0x000AEB80
		private static Vec3 GetSplineValue(double t, Vec3 x0, Vec3 x1, Vec3 dx0, Vec3 dx1)
		{
			double t2 = t * t;
			double t3 = t2 * t;
			return (t3 - 2.0 * t2 + t) * dx0 + (-2.0 * t3 + 3.0 * t2) * (x1 - x0) + (t3 - t2) * dx1 + x0;
		}

		// Token: 0x06001B06 RID: 6918 RVA: 0x000AFBE8 File Offset: 0x000AEBE8
		private static Vec3 GetSpline2DerivativeValue(double t, Vec3 x0, Vec3 x1, Vec3 dx0, Vec3 dx1)
		{
			return (6.0 * t - 4.0) * dx0 + (-12.0 * t + 6.0) * (x1 - x0) + (6.0 * t - 2.0) * dx1;
		}

		// Token: 0x06001B07 RID: 6919 RVA: 0x000AFC58 File Offset: 0x000AEC58
		private static int DecomposeInterval(Vec3 x0, Vec3 x1, Vec3 dx0, Vec3 dx1)
		{
			Vec3 d2_0 = ShipTourChecker.GetSpline2DerivativeValue(0.0, x0, x1, dx0, dx1).Abs();
			Vec3 d2_ = ShipTourChecker.GetSpline2DerivativeValue(1.0, x0, x1, dx0, dx1).Abs();
			double max = Vec3.Maximize(d2_0, d2_).Length;
			return (int)Math.Sqrt(max / 0.8) + 1;
		}

		// Token: 0x06001B08 RID: 6920 RVA: 0x000AFCC0 File Offset: 0x000AECC0
		private static bool CheckSegment(MapEditorMap map, RoutePoint beginPoint, RoutePoint endPoint, float radius, int zoneTileCount)
		{
			Vec3 beginVec = beginPoint.Position.Vec3;
			Vec3 endVec = endPoint.Position.Vec3;
			int intervalCount = ShipTourChecker.DecomposeInterval(beginPoint.Position.Vec3, endPoint.Position.Vec3, beginPoint.Tangent, endPoint.Tangent);
			int nodeCount = intervalCount + 1;
			double intervalLen = 1.0 / (double)intervalCount;
			Vec3 value_0 = beginPoint.Position.Vec3;
			for (int index = 0; index < intervalCount; index++)
			{
				Vec3 value_ = ShipTourChecker.GetNodeValue(beginVec, endVec, beginPoint.Tangent, endPoint.Tangent, nodeCount, intervalLen, index + 1);
				if (!ShipTourChecker.CheckSegment(map, value_0, value_, (double)radius, zoneTileCount))
				{
					return false;
				}
				value_0 = value_;
			}
			return true;
		}

		// Token: 0x06001B09 RID: 6921 RVA: 0x000AFD84 File Offset: 0x000AED84
		private float GetRadius(RoutePoint routePoint)
		{
			string shipResource = RoutePoint.GetShipResource(routePoint.Route);
			if (!string.IsNullOrEmpty(shipResource))
			{
				DBID shipResDBID = this.mainDb.GetDBIDByName(shipResource);
				if (!DBID.IsNullOrEmpty(shipResDBID))
				{
					IObjMan objMan = this.mainDb.GetManipulator(shipResDBID);
					DBID visShipDBID;
					objMan.GetValue("visualShip", out visShipDBID);
					objMan.Dispose();
					if (!DBID.IsNullOrEmpty(visShipDBID))
					{
						objMan = this.mainDb.GetManipulator(visShipDBID);
						float radius;
						objMan.GetValue("radius", out radius);
						objMan.Dispose();
						return radius;
					}
				}
			}
			return 0f;
		}

		// Token: 0x06001B0A RID: 6922 RVA: 0x000AFE0C File Offset: 0x000AEE0C
		public ShipTourChecker()
		{
			base.Name = Strings.SHIP_TOUR_CHECKER_NAME;
			base.ShortDescription = string.Format(Strings.SHIP_TOUR_CHECKER_SHORT_DESCRIPTION, new object[0]);
			base.LongDescription = string.Format(string.Format(Strings.SHIP_TOUR_CHECKER_LONG_DESCRIPTION, 2), new object[0]);
		}

		// Token: 0x06001B0B RID: 6923 RVA: 0x000AFE6C File Offset: 0x000AEE6C
		public override void Check(MapEditorMap map, IProgressContainer progressContainer)
		{
			if (progressContainer != null)
			{
				progressContainer.AddString(Strings.RUN_SHIP_TOUR_CHECKER);
			}
			base.LongInfoView = new LongInfoViewNode(true);
			int zoneTileCount = Constants.PatchSize * map.Data.MapSize.X / Constants.MapZonePieceSize.X;
			List<string> processedToors = new List<string>();
			foreach (KeyValuePair<int, IMapObject> keyValuePair in map.MapEditorMapObjectContainer.RoutePointContainer.MapObjects)
			{
				RoutePoint routePoint = keyValuePair.Value as RoutePoint;
				if (routePoint != null && !string.IsNullOrEmpty(routePoint.Route) && !processedToors.Contains(routePoint.Route))
				{
					List<RoutePoint> routePoints = RoutePoint.GetRoute(RoutePoint.GetStartRoutePoint(routePoint, map.MapEditorMapObjectContainer), map.MapEditorMapObjectContainer);
					float radius = this.GetRadius(routePoint);
					if (routePoints.Count != 1)
					{
						for (int pointIndex = 0; pointIndex < routePoints.Count - 1; pointIndex++)
						{
							RoutePoint firstRoute = routePoints[pointIndex];
							RoutePoint secondRoute = routePoints[pointIndex + 1];
							if (!ShipTourChecker.CheckSegment(map, firstRoute, secondRoute, radius, zoneTileCount))
							{
								string key = string.Format("{0} {1}_{2}", routePoint.Route, pointIndex, pointIndex + 1);
								base.LongInfoView.FindOrAddNode(key, false).AddNode(firstRoute, false);
								base.LongInfoView.FindOrAddNode(key, false).AddNode(secondRoute, false);
							}
						}
					}
					else if (!ShipTourChecker.CheckPoint(map, routePoint.Position.Vec3, (double)radius, zoneTileCount))
					{
						base.LongInfoView.AddNode(routePoint, false);
					}
					processedToors.Add(routePoint.Route);
				}
			}
			int count = base.LongInfoView.BranchCount;
			if (count > 0)
			{
				base.Status = MapCheckerStatus.Red;
				base.ShortInfo = Strings.CHECKER_EXCEED_RED;
			}
			else
			{
				base.Status = MapCheckerStatus.Green;
				base.ShortInfo = Strings.CHECKER_OK;
			}
			base.ShortResult = string.Format("{0}", count);
			base.LongResult = string.Format(Strings.SHIP_TOUR_CHECKER_LONG_RESULT, count);
			if (progressContainer != null)
			{
				progressContainer.Progress++;
			}
		}

		// Token: 0x0400114D RID: 4429
		private const int ZoneOffset = 2;

		// Token: 0x0400114E RID: 4430
		private const double Accuracy = 0.1;

		// Token: 0x0400114F RID: 4431
		private readonly IDatabase mainDb = IDatabase.GetMainDatabase();
	}
}
