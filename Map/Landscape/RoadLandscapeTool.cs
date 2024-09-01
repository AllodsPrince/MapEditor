using System;
using System.Collections.Generic;
using MapEditor.Map.MapObjects;
using Tools.Geometry;
using Tools.Landscape;
using Tools.Landscape.LandscapeToolParams;
using Tools.MapObjects;

namespace MapEditor.Map.Landscape
{
	// Token: 0x0200025F RID: 607
	public class RoadLandscapeTool : LandscapeTool
	{
		// Token: 0x06001CD7 RID: 7383 RVA: 0x000B7654 File Offset: 0x000B6654
		private static void PlaceMapObjectsPeriodically(Stripe stripe, ICollisionMap collisionMap, MapObjectContainer mapObjectContainer, string group, RoadItemSet roadItems, ICollection<int> addedMapObjectsIDs)
		{
			double minDistance = roadItems.MinDistance;
			double maxDistance = roadItems.MaxDistance;
			if (minDistance < 0.0)
			{
				minDistance = 0.0;
			}
			if (maxDistance < minDistance)
			{
				maxDistance = minDistance;
			}
			if (maxDistance < 1.0)
			{
				maxDistance = RoadLandscapeTool.defaultItemDistance;
			}
			double distanceFluctuationValue = maxDistance - minDistance;
			bool distanceFluctuation = distanceFluctuationValue > MathConsts.DOUBLE_EPSILON;
			double probability = roadItems.Probability;
			bool alwaysPlaceObject = false;
			if (probability < 0.0)
			{
				return;
			}
			if (probability + MathConsts.DOUBLE_EPSILON > 1.0)
			{
				alwaysPlaceObject = true;
			}
			bool positionFluctuation = roadItems.PositionFluctuation > MathConsts.DOUBLE_EPSILON;
			bool rotationFluctuation = roadItems.RotationFluctuation > MathConsts.DOUBLE_EPSILON;
			bool scaleFluctuation = roadItems.ScaleFluctuation > MathConsts.DOUBLE_EPSILON;
			string groupName = Str.ConcatGroupNames(group, roadItems.Group);
			Polygon polygon = stripe.Center;
			double previousLength = minDistance;
			Vec3 previousLeftPosition = Vec3.Empty;
			Vec3 previousRightPosition = Vec3.Empty;
			bool previousLeftPositionFilled = false;
			bool previousRightPositionFilled = false;
			for (int pointIndex = 0; pointIndex < polygon.Count - 1; pointIndex++)
			{
				double currentLength = (polygon.Points[pointIndex + 1] - polygon.Points[pointIndex]).Length;
				if (previousLength + currentLength > minDistance)
				{
					double rolledDistance = minDistance;
					if (distanceFluctuation)
					{
						rolledDistance += RoadLandscapeTool.random.NextDouble() * distanceFluctuationValue;
					}
					double pointLength;
					for (pointLength = rolledDistance - previousLength; pointLength < currentLength; pointLength += rolledDistance)
					{
						Vec3 leftPoint;
						Vec3 rightPoint;
						if (roadItems.Anchor == RoadAnchor.Center)
						{
							stripe.GetCenterPoints(out leftPoint, out rightPoint, pointIndex, pointLength, roadItems.Shift, collisionMap);
						}
						else
						{
							stripe.GetEdgePoints(out leftPoint, out rightPoint, pointIndex, pointLength, roadItems.Shift, collisionMap);
						}
						Vec3 rotation = Vec3.Empty;
						if (roadItems.AlignType == RoadItemAlignType.Along || roadItems.AlignType == RoadItemAlignType.Across || roadItems.AlignType == RoadItemAlignType.PreviousItem)
						{
							rotation = polygon.Points[pointIndex + 1] - polygon.Points[pointIndex];
						}
						else if (roadItems.AlignType == RoadItemAlignType.AlongFlipped || roadItems.AlignType == RoadItemAlignType.AcrossFlipped || roadItems.AlignType == RoadItemAlignType.PreviousItemFlipped)
						{
							rotation = polygon.Points[pointIndex] - polygon.Points[pointIndex + 1];
						}
						if (roadItems.Side == RoadSide.Left || roadItems.Side == RoadSide.Both)
						{
							string objectName = roadItems.Items.Get();
							if (!string.IsNullOrEmpty(objectName) && (alwaysPlaceObject || RoadLandscapeTool.random.NextDouble() <= probability))
							{
								Position _position = new Position(leftPoint);
								if (positionFluctuation)
								{
									_position.X += (RoadLandscapeTool.random.NextDouble() - 0.5) * roadItems.PositionFluctuation;
									_position.Y += (RoadLandscapeTool.random.NextDouble() - 0.5) * roadItems.PositionFluctuation;
									_position.Z = collisionMap.GetTerrainHeight(_position.X, _position.Y);
								}
								if (roadItems.AlignType == RoadItemAlignType.PreviousItem && previousLeftPositionFilled)
								{
									rotation = _position.Vec3 - previousLeftPosition;
								}
								else if (roadItems.AlignType == RoadItemAlignType.PreviousItemFlipped && previousLeftPositionFilled)
								{
									rotation = previousLeftPosition - _position.Vec3;
								}
								Quat quat = new Quat(rotation);
								Rotation _rotation = new Rotation(ref quat);
								if (rotationFluctuation)
								{
									_rotation.Yaw += (float)(3.141592653589793 * (RoadLandscapeTool.random.NextDouble() - 0.5) * roadItems.RotationFluctuation);
								}
								if (roadItems.AlignType == RoadItemAlignType.Across || roadItems.AlignType == RoadItemAlignType.AcrossFlipped)
								{
									_rotation.Yaw += 1.5707964f;
								}
								_rotation.Pitch = 0f;
								_rotation.Roll = 0f;
								Scale _scale = Scale.Normal;
								if (scaleFluctuation)
								{
									_scale = new Scale((float)((double)_scale.Ratio + (RoadLandscapeTool.random.NextDouble() - 0.5) * roadItems.ScaleFluctuation));
								}
								int mapObjectID = mapObjectContainer.AddMapObject(new MapObjectType(MapObjectFactory.Type.StaticObject, objectName), false, _position, _rotation, _scale, groupName);
								if (mapObjectID != -1)
								{
									addedMapObjectsIDs.Add(mapObjectID);
								}
								previousLeftPosition = _position.Vec3;
								previousLeftPositionFilled = true;
							}
							else
							{
								previousLeftPosition = leftPoint;
								previousLeftPositionFilled = true;
							}
						}
						if (roadItems.Side == RoadSide.Right || roadItems.Side == RoadSide.Both)
						{
							string objectName2 = roadItems.Items.Get();
							if (!string.IsNullOrEmpty(objectName2) && (alwaysPlaceObject || RoadLandscapeTool.random.NextDouble() <= probability))
							{
								Position _position2 = new Position(rightPoint);
								if (positionFluctuation)
								{
									_position2.X += (RoadLandscapeTool.random.NextDouble() - 0.5) * roadItems.PositionFluctuation;
									_position2.Y += (RoadLandscapeTool.random.NextDouble() - 0.5) * roadItems.PositionFluctuation;
									_position2.Z = collisionMap.GetTerrainHeight(_position2.X, _position2.Y);
								}
								if (roadItems.AlignType == RoadItemAlignType.PreviousItem && previousRightPositionFilled)
								{
									rotation = _position2.Vec3 - previousRightPosition;
								}
								else if (roadItems.AlignType == RoadItemAlignType.PreviousItemFlipped && previousRightPositionFilled)
								{
									rotation = previousRightPosition - _position2.Vec3;
								}
								Quat quat2 = new Quat(rotation);
								Rotation _rotation2 = new Rotation(ref quat2);
								if (rotationFluctuation)
								{
									_rotation2.Yaw += (float)(3.141592653589793 * (RoadLandscapeTool.random.NextDouble() - 0.5) * roadItems.RotationFluctuation);
								}
								if (roadItems.AlignType == RoadItemAlignType.Across || roadItems.AlignType == RoadItemAlignType.AcrossFlipped)
								{
									_rotation2.Yaw -= 1.5707964f;
								}
								_rotation2.Pitch = 0f;
								_rotation2.Roll = 0f;
								Scale _scale2 = Scale.Normal;
								if (scaleFluctuation)
								{
									_scale2 = new Scale((float)((double)_scale2.Ratio + (RoadLandscapeTool.random.NextDouble() - 0.5) * roadItems.ScaleFluctuation));
								}
								int mapObjectID2 = mapObjectContainer.AddMapObject(new MapObjectType(MapObjectFactory.Type.StaticObject, objectName2), false, _position2, _rotation2, _scale2, groupName);
								if (mapObjectID2 != -1)
								{
									addedMapObjectsIDs.Add(mapObjectID2);
								}
								previousRightPosition = _position2.Vec3;
								previousRightPositionFilled = true;
							}
							else
							{
								previousRightPosition = rightPoint;
								previousRightPositionFilled = true;
							}
						}
						rolledDistance = minDistance;
						if (distanceFluctuation)
						{
							rolledDistance += RoadLandscapeTool.random.NextDouble() * distanceFluctuationValue;
						}
					}
					previousLength = currentLength - (pointLength - rolledDistance);
				}
				else
				{
					previousLength += currentLength;
				}
			}
		}

		// Token: 0x06001CD8 RID: 7384 RVA: 0x000B7CC4 File Offset: 0x000B6CC4
		private static void PlaceMapObjectsByPolygon(Polygon polygon, RoadLandscapeTool.PolygonPlacement polygonPlacement, ICollisionMap collisionMap, MapObjectContainer mapObjectContainer, string group, RoadItemSet roadItems, ICollection<int> addedMapObjectsIDs)
		{
			double minDistance = roadItems.MinDistance;
			double maxDistance = roadItems.MaxDistance;
			if (minDistance < 0.0)
			{
				minDistance = 0.0;
			}
			if (maxDistance < minDistance)
			{
				maxDistance = minDistance;
			}
			if (maxDistance < 1.0)
			{
				maxDistance = RoadLandscapeTool.defaultItemDistance;
			}
			double distanceFluctuationValue = maxDistance - minDistance;
			bool distanceFluctuation = distanceFluctuationValue > MathConsts.DOUBLE_EPSILON;
			double probability = roadItems.Probability;
			bool alwaysPlaceObject = false;
			if (probability < 0.0)
			{
				return;
			}
			if (probability + MathConsts.DOUBLE_EPSILON > 1.0)
			{
				alwaysPlaceObject = true;
			}
			bool positionFluctuation = roadItems.PositionFluctuation > MathConsts.DOUBLE_EPSILON;
			bool rotationFluctuation = roadItems.RotationFluctuation > MathConsts.DOUBLE_EPSILON;
			bool scaleFluctuation = roadItems.ScaleFluctuation > MathConsts.DOUBLE_EPSILON;
			string groupName = Str.ConcatGroupNames(group, roadItems.Group);
			double previousLength = minDistance;
			Vec3 previousPosition = Vec3.Empty;
			bool previousPositionFilled = false;
			for (int pointIndex = 0; pointIndex < polygon.Count - 1; pointIndex++)
			{
				double currentLength = (polygon.Points[pointIndex + 1] - polygon.Points[pointIndex]).Length;
				if (previousLength + currentLength > minDistance)
				{
					double rolledDistance = minDistance;
					if (distanceFluctuation)
					{
						rolledDistance += RoadLandscapeTool.random.NextDouble() * distanceFluctuationValue;
					}
					double pointLength;
					for (pointLength = rolledDistance - previousLength; pointLength < currentLength; pointLength += rolledDistance)
					{
						Vec3 point = polygon.Points[pointIndex] + (polygon.Points[pointIndex + 1] - polygon.Points[pointIndex]) * (pointLength / currentLength);
						Vec3 rotation = Vec3.Empty;
						if (roadItems.AlignType == RoadItemAlignType.Along || roadItems.AlignType == RoadItemAlignType.Across || roadItems.AlignType == RoadItemAlignType.PreviousItem)
						{
							rotation = polygon.Points[pointIndex + 1] - polygon.Points[pointIndex];
						}
						else if (roadItems.AlignType == RoadItemAlignType.AlongFlipped || roadItems.AlignType == RoadItemAlignType.AcrossFlipped || roadItems.AlignType == RoadItemAlignType.PreviousItemFlipped)
						{
							rotation = polygon.Points[pointIndex] - polygon.Points[pointIndex + 1];
						}
						string objectName = roadItems.Items.Get();
						if (!string.IsNullOrEmpty(objectName) && (alwaysPlaceObject || RoadLandscapeTool.random.NextDouble() <= probability))
						{
							Position _position = new Position(point);
							if (positionFluctuation)
							{
								_position.X += (RoadLandscapeTool.random.NextDouble() - 0.5) * roadItems.PositionFluctuation;
								_position.Y += (RoadLandscapeTool.random.NextDouble() - 0.5) * roadItems.PositionFluctuation;
								_position.Z = collisionMap.GetTerrainHeight(_position.X, _position.Y);
							}
							if (roadItems.AlignType == RoadItemAlignType.PreviousItem && previousPositionFilled)
							{
								rotation = _position.Vec3 - previousPosition;
							}
							else if (roadItems.AlignType == RoadItemAlignType.PreviousItemFlipped && previousPositionFilled)
							{
								rotation = previousPosition - _position.Vec3;
							}
							Quat quat = new Quat(rotation);
							Rotation _rotation = new Rotation(ref quat);
							if (rotationFluctuation)
							{
								_rotation.Yaw += (float)(3.141592653589793 * (RoadLandscapeTool.random.NextDouble() - 0.5) * roadItems.RotationFluctuation);
							}
							if (roadItems.AlignType == RoadItemAlignType.Across || roadItems.AlignType == RoadItemAlignType.AcrossFlipped)
							{
								if (polygonPlacement == RoadLandscapeTool.PolygonPlacement.Left)
								{
									_rotation.Yaw += 1.5707964f;
								}
								else if (polygonPlacement == RoadLandscapeTool.PolygonPlacement.Right)
								{
									_rotation.Yaw -= 1.5707964f;
								}
								else if (polygonPlacement == RoadLandscapeTool.PolygonPlacement.Center)
								{
									if (RoadLandscapeTool.random.Next(0, 2) == 0)
									{
										_rotation.Yaw += 1.5707964f;
									}
									else
									{
										_rotation.Yaw -= 1.5707964f;
									}
								}
							}
							_rotation.Pitch = 0f;
							_rotation.Roll = 0f;
							Scale _scale = Scale.Normal;
							if (scaleFluctuation)
							{
								_scale = new Scale((float)((double)_scale.Ratio + (RoadLandscapeTool.random.NextDouble() - 0.5) * roadItems.ScaleFluctuation));
							}
							int mapObjectID = mapObjectContainer.AddMapObject(new MapObjectType(MapObjectFactory.Type.StaticObject, objectName), false, _position, _rotation, _scale, groupName);
							if (mapObjectID != -1)
							{
								addedMapObjectsIDs.Add(mapObjectID);
							}
							previousPosition = _position.Vec3;
							previousPositionFilled = true;
						}
						else
						{
							previousPosition = point;
							previousPositionFilled = true;
						}
						rolledDistance = minDistance;
						if (distanceFluctuation)
						{
							rolledDistance += RoadLandscapeTool.random.NextDouble() * distanceFluctuationValue;
						}
					}
					previousLength = currentLength - (pointLength - rolledDistance);
				}
				else
				{
					previousLength += currentLength;
				}
			}
		}

		// Token: 0x06001CD9 RID: 7385 RVA: 0x000B815C File Offset: 0x000B715C
		private static void PlaceMapObjects(Stripe stripe, ICollisionMap collisionMap, MapObjectContainer mapObjectContainer, string group, RoadItemSet roadItems, ICollection<int> addedMapObjectsIDs)
		{
			double shift = Math.Abs(roadItems.Shift);
			if (roadItems.Anchor == RoadAnchor.Center && shift < MathConsts.DOUBLE_EPSILON)
			{
				RoadLandscapeTool.PlaceMapObjectsByPolygon(stripe.Center, RoadLandscapeTool.PolygonPlacement.Center, collisionMap, mapObjectContainer, group, roadItems, addedMapObjectsIDs);
				return;
			}
			if (roadItems.DistanceType == RoadDistanceType.Absolute)
			{
				Polygon leftPolygon = (roadItems.Side == RoadSide.Left || roadItems.Side == RoadSide.Both) ? new Polygon(-1, collisionMap) : null;
				Polygon rightPolygon = (roadItems.Side == RoadSide.Right || roadItems.Side == RoadSide.Both) ? new Polygon(-1, collisionMap) : null;
				for (int index = 0; index < stripe.Center.Count; index++)
				{
					Vec3 leftPoint;
					Vec3 rightPoint;
					if (roadItems.Anchor == RoadAnchor.Center)
					{
						stripe.GetCenterPoints(out leftPoint, out rightPoint, index, shift, collisionMap);
					}
					else
					{
						stripe.GetEdgePoints(out leftPoint, out rightPoint, index, shift, collisionMap);
					}
					if (leftPolygon != null)
					{
						leftPolygon.AddPoint(leftPoint, false, false);
					}
					if (rightPolygon != null)
					{
						rightPolygon.AddPoint(rightPoint, false, false);
					}
				}
				if (leftPolygon != null)
				{
					RoadLandscapeTool.PlaceMapObjectsByPolygon(leftPolygon, RoadLandscapeTool.PolygonPlacement.Left, collisionMap, mapObjectContainer, group, roadItems, addedMapObjectsIDs);
				}
				if (rightPolygon != null)
				{
					RoadLandscapeTool.PlaceMapObjectsByPolygon(rightPolygon, RoadLandscapeTool.PolygonPlacement.Right, collisionMap, mapObjectContainer, group, roadItems, addedMapObjectsIDs);
					return;
				}
			}
			else
			{
				RoadLandscapeTool.PlaceMapObjectsPeriodically(stripe, collisionMap, mapObjectContainer, group, roadItems, addedMapObjectsIDs);
			}
		}

		// Token: 0x06001CDA RID: 7386 RVA: 0x000B826C File Offset: 0x000B726C
		private void PlaceMapObjects()
		{
			this.RemoveMapObjects();
			RoadLandscapeToolParams roadLandscapeToolParams = base.LandscapeToolParams as RoadLandscapeToolParams;
			if (roadLandscapeToolParams != null && base.LandscapeRegion != null && base.LandscapeRegion.LandscapeRegionParams.Type == LandscapeRegionType.Stripe)
			{
				for (int index = 0; index < roadLandscapeToolParams.RoadParams.ItemSets.Count; index++)
				{
					RoadLandscapeTool.PlaceMapObjects(base.LandscapeRegion.Stripe, base.LandscapeToolContext.CollisionMap, base.LandscapeToolContext.MapObjectContainer, roadLandscapeToolParams.RoadParams.Group, roadLandscapeToolParams.RoadParams.ItemSets[index], this.mapObjects);
				}
			}
		}

		// Token: 0x06001CDB RID: 7387 RVA: 0x000B830C File Offset: 0x000B730C
		private void RemoveMapObjects()
		{
			foreach (int mapObjectID in this.mapObjects)
			{
				base.LandscapeToolContext.MapObjectContainer.RemoveMapObject(mapObjectID);
			}
		}

		// Token: 0x06001CDC RID: 7388 RVA: 0x000B836C File Offset: 0x000B736C
		public RoadLandscapeTool(int _id) : base(_id)
		{
			base.AffectParams.AffectHeight = true;
			base.AffectParams.AffectTile = true;
		}

		// Token: 0x06001CDD RID: 7389 RVA: 0x000B83A6 File Offset: 0x000B73A6
		public override void Create()
		{
		}

		// Token: 0x06001CDE RID: 7390 RVA: 0x000B83A8 File Offset: 0x000B73A8
		public override void Apply()
		{
			RoadLandscapeToolParams roadLandscapeToolParams = base.LandscapeToolParams as RoadLandscapeToolParams;
			if (roadLandscapeToolParams != null && base.LandscapeRegion != null)
			{
				this.somethingChanged = false;
				if (this.nextOperation == RoadLandscapeTool.Operation.Init)
				{
					if (this.editorSceneLandscapeToolID != -1)
					{
						Rect affectedRect;
						base.LandscapeToolContext.EditorScene.UndoRoadLandscapeTool(this.editorSceneLandscapeToolID, out affectedRect);
						this.RemoveMapObjects();
						this.Destroy();
					}
					if (base.LandscapeRegion.LandscapeRegionParams.Type != LandscapeRegionType.Ellipse && base.LandscapeRegion.LandscapeRegionParams.Type != LandscapeRegionType.Square)
					{
						if (base.LandscapeRegion.LandscapeRegionParams.Type == LandscapeRegionType.Polygon)
						{
							Vec3 mapOffset = base.LandscapeToolContext.MapOffset;
							this.editorSceneLandscapeToolID = base.LandscapeToolContext.EditorScene.CreateRoadLandscapeTool(ref mapOffset, base.LandscapeRegion.Polygon, roadLandscapeToolParams);
						}
						else if (base.LandscapeRegion.LandscapeRegionParams.Type == LandscapeRegionType.Stripe)
						{
							Vec3 mapOffset2 = base.LandscapeToolContext.MapOffset;
							this.editorSceneLandscapeToolID = base.LandscapeToolContext.EditorScene.CreateRoadLandscapeTool(ref mapOffset2, base.LandscapeRegion.Stripe.Bounds, roadLandscapeToolParams);
						}
						else
						{
							LandscapeRegionType type = base.LandscapeRegion.LandscapeRegionParams.Type;
						}
					}
					if (this.editorSceneLandscapeToolID != -1)
					{
						base.LandscapeToolContext.EditorScene.SetRoadLandscapeToolParams(this.editorSceneLandscapeToolID, roadLandscapeToolParams);
						Rect affectedRect2;
						base.LandscapeToolContext.EditorScene.ApplyRoadLandscapeTool(this.editorSceneLandscapeToolID, out affectedRect2);
						this.PlaceMapObjects();
						base.AffectParams.AffectedRect = affectedRect2;
						this.somethingChanged = true;
						return;
					}
				}
				else if (this.nextOperation == RoadLandscapeTool.Operation.Apply)
				{
					if (this.editorSceneLandscapeToolID != -1)
					{
						base.LandscapeToolContext.EditorScene.SetRoadLandscapeToolParams(this.editorSceneLandscapeToolID, roadLandscapeToolParams);
						Rect affectedRect3;
						base.LandscapeToolContext.EditorScene.ApplyRoadLandscapeTool(this.editorSceneLandscapeToolID, out affectedRect3);
						this.PlaceMapObjects();
						base.AffectParams.AffectedRect = affectedRect3;
						this.somethingChanged = true;
						return;
					}
				}
				else if (this.nextOperation == RoadLandscapeTool.Operation.Reset)
				{
					if (this.editorSceneLandscapeToolID != -1)
					{
						Rect affectedRect4;
						base.LandscapeToolContext.EditorScene.UndoRoadLandscapeTool(this.editorSceneLandscapeToolID, out affectedRect4);
						this.RemoveMapObjects();
						base.AffectParams.AffectedRect = affectedRect4;
						this.somethingChanged = true;
						this.Destroy();
						return;
					}
				}
				else if (this.nextOperation == RoadLandscapeTool.Operation.Complete)
				{
					this.Destroy();
				}
			}
		}

		// Token: 0x06001CDF RID: 7391 RVA: 0x000B85EE File Offset: 0x000B75EE
		public override void Destroy()
		{
			if (this.editorSceneLandscapeToolID != -1)
			{
				base.LandscapeToolContext.EditorScene.DeleteLandscapeTool(this.editorSceneLandscapeToolID);
				this.editorSceneLandscapeToolID = -1;
			}
			this.mapObjects.Clear();
		}

		// Token: 0x170006E1 RID: 1761
		// (get) Token: 0x06001CE0 RID: 7392 RVA: 0x000B8621 File Offset: 0x000B7621
		// (set) Token: 0x06001CE1 RID: 7393 RVA: 0x000B8629 File Offset: 0x000B7629
		public RoadLandscapeTool.Operation NextOperation
		{
			get
			{
				return this.nextOperation;
			}
			set
			{
				this.nextOperation = value;
			}
		}

		// Token: 0x170006E2 RID: 1762
		// (get) Token: 0x06001CE2 RID: 7394 RVA: 0x000B8632 File Offset: 0x000B7632
		// (set) Token: 0x06001CE3 RID: 7395 RVA: 0x000B863A File Offset: 0x000B763A
		public bool SomethingChanged
		{
			get
			{
				return this.somethingChanged;
			}
			set
			{
				this.somethingChanged = value;
			}
		}

		// Token: 0x04001256 RID: 4694
		private static readonly Random random = new Random(DateTime.Now.Millisecond);

		// Token: 0x04001257 RID: 4695
		private static readonly double defaultItemDistance = 8.0;

		// Token: 0x04001258 RID: 4696
		private RoadLandscapeTool.Operation nextOperation = RoadLandscapeTool.Operation.Apply;

		// Token: 0x04001259 RID: 4697
		private bool somethingChanged;

		// Token: 0x0400125A RID: 4698
		private int editorSceneLandscapeToolID = -1;

		// Token: 0x0400125B RID: 4699
		private readonly List<int> mapObjects = new List<int>();

		// Token: 0x02000260 RID: 608
		public enum Operation
		{
			// Token: 0x0400125D RID: 4701
			Init,
			// Token: 0x0400125E RID: 4702
			Apply,
			// Token: 0x0400125F RID: 4703
			Reset,
			// Token: 0x04001260 RID: 4704
			Complete
		}

		// Token: 0x02000261 RID: 609
		private enum PolygonPlacement
		{
			// Token: 0x04001262 RID: 4706
			Center,
			// Token: 0x04001263 RID: 4707
			Left,
			// Token: 0x04001264 RID: 4708
			Right
		}
	}
}
