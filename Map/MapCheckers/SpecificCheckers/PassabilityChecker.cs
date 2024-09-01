using System;
using System.Collections.Generic;
using MapEditor.Map.MapObjects;
using MapEditor.Resources.Strings;
using Tools.Geometry;
using Tools.MapObjects;
using Tools.Progress;

namespace MapEditor.Map.MapCheckers.SpecificCheckers
{
	// Token: 0x02000159 RID: 345
	public class PassabilityChecker : MapChecker
	{
		// Token: 0x06001098 RID: 4248 RVA: 0x0007BB38 File Offset: 0x0007AB38
		public PassabilityChecker(EditorScene _editorScene)
		{
			this.editorScene = _editorScene;
			base.Name = Strings.PASSABILITY_CHECKER_NAME;
			base.ShortDescription = Strings.PASSABILITY_CHECKER_SHORT_DESCRIPTION;
			base.LongDescription = Strings.PASSABILITY_CHECKER_LONG_DESCRIPTION;
		}

		// Token: 0x06001099 RID: 4249 RVA: 0x0007BB68 File Offset: 0x0007AB68
		private static bool PointInsideSquare(ref Vec2 center, double x, double y)
		{
			return Math.Abs(x - center.X) < PassabilityChecker.squareHalfSize && Math.Abs(y - center.Y) < PassabilityChecker.squareHalfSize;
		}

		// Token: 0x0600109A RID: 4250 RVA: 0x0007BB94 File Offset: 0x0007AB94
		private static bool CircleCrossSquare(ref Vec2 center, double x, double y, double radius)
		{
			if (radius > MathConsts.DOUBLE_EPSILON)
			{
				double _x = Math.Abs(x - center.X) - PassabilityChecker.squareHalfDiagonal;
				double _y = Math.Abs(y - center.Y) - PassabilityChecker.squareHalfDiagonal;
				return _x < 0.0 || _y < 0.0 || _x * _x + _y * _y < radius * radius;
			}
			return false;
		}

		// Token: 0x0600109B RID: 4251 RVA: 0x0007BBFC File Offset: 0x0007ABFC
		private static bool EllipseCrossSquare(ref Vec2 center, double x, double y, double semiaxisA, double semiaxisB)
		{
			if (semiaxisA > MathConsts.DOUBLE_EPSILON && semiaxisB > MathConsts.DOUBLE_EPSILON)
			{
				double _x = Math.Abs(x - center.X) - PassabilityChecker.squareHalfDiagonal;
				double _y = Math.Abs(y - center.Y) - PassabilityChecker.squareHalfDiagonal;
				return _x < 0.0 || _y < 0.0 || _x * _x / (semiaxisA * semiaxisA) + _y * _y / (semiaxisB * semiaxisB) < 1.0;
			}
			return false;
		}

		// Token: 0x0600109C RID: 4252 RVA: 0x0007BC7C File Offset: 0x0007AC7C
		private static void AddToDictionary<T>(IDictionary<T, int> dictionary, T value)
		{
			if (dictionary.ContainsKey(value))
			{
				dictionary[value]++;
				return;
			}
			dictionary.Add(value, 1);
		}

		// Token: 0x0600109D RID: 4253 RVA: 0x0007BCB0 File Offset: 0x0007ACB0
		public override void Check(MapEditorMap map, IProgressContainer progressContainer)
		{
			if (progressContainer != null)
			{
				progressContainer.AddString(Strings.RUN_PASSABILITY_CHECKER);
			}
			base.LongInfoView = new LongInfoViewNode(true);
			Dictionary<SpawnPoint, int> spawnPoints = new Dictionary<SpawnPoint, int>();
			Dictionary<PatrolNode, int> patrolNodes = new Dictionary<PatrolNode, int>();
			Vec2 minXMinY = new Vec2((double)(map.Data.MinXMinYPatchCoords.X * Constants.PatchSize), (double)(map.Data.MinXMinYPatchCoords.Y * Constants.PatchSize));
			Rect bounds = new Rect(0, 0, map.Data.MapSize.X * Constants.PatchSize, map.Data.MapSize.Y * Constants.PatchSize);
			this.editorScene.SynchronizeObjects(true);
			foreach (KeyValuePair<int, IMapObject> keyValuePair in map.MapEditorMapObjectContainer.SpawnPointContainer.MapObjects)
			{
				SpawnPoint spawnPoint = keyValuePair.Value as SpawnPoint;
				if (spawnPoint != null)
				{
					OutOfTerrainChecker.TerrainPlace terrainPlace;
					OutOfTerrainChecker.ObjectPlace objectPlace;
					OutOfTerrainChecker.GetMapObjectPlace(out terrainPlace, out objectPlace, spawnPoint, map, this.editorScene);
					if (terrainPlace != OutOfTerrainChecker.TerrainPlace.Undefined && (terrainPlace == OutOfTerrainChecker.TerrainPlace.OnTerrain || (terrainPlace == OutOfTerrainChecker.TerrainPlace.OverTerrain && objectPlace == OutOfTerrainChecker.ObjectPlace.OutOfObject)))
					{
						if (spawnPoint.SpawnPointType == SpawnPointType.Patrol || spawnPoint.SpawnPointType == SpawnPointType.Guard)
						{
							int x = (int)(spawnPoint.Position.X - minXMinY.X);
							int y = (int)(spawnPoint.Position.Y - minXMinY.Y);
							if (bounds.Inside(x, y) && !this.editorScene.IsPassable(x, y))
							{
								Vec2 center = new Vec2(minXMinY.X + (double)x + 0.5, minXMinY.Y + (double)y + 0.5);
								if (PassabilityChecker.PointInsideSquare(ref center, spawnPoint.Position.X, spawnPoint.Position.Y))
								{
									PassabilityChecker.AddToDictionary<SpawnPoint>(spawnPoints, spawnPoint);
								}
							}
						}
						else if (spawnPoint.SpawnPointType == SpawnPointType.Circle)
						{
							CircleSpawnPointData circleSpawnPointData = spawnPoint.SpawnPointData as CircleSpawnPointData;
							if (circleSpawnPointData != null)
							{
								Rect indices = new Rect((int)(spawnPoint.Position.X - circleSpawnPointData.Radius - minXMinY.X), (int)(spawnPoint.Position.Y - circleSpawnPointData.Radius - minXMinY.Y), (int)(spawnPoint.Position.X + circleSpawnPointData.Radius - minXMinY.X + 1.0), (int)(spawnPoint.Position.Y + circleSpawnPointData.Radius - minXMinY.Y + 1.0));
								if (indices.Validate(bounds) >= 0)
								{
									for (int x2 = indices.Min.X; x2 < indices.Max.X; x2++)
									{
										for (int y2 = indices.Min.Y; y2 < indices.Max.Y; y2++)
										{
											if (!this.editorScene.IsPassable(x2, y2))
											{
												Vec2 center2 = new Vec2(minXMinY.X + (double)x2 + 0.5, minXMinY.Y + (double)y2 + 0.5);
												if (PassabilityChecker.CircleCrossSquare(ref center2, spawnPoint.Position.X, spawnPoint.Position.Y, circleSpawnPointData.Radius))
												{
													PassabilityChecker.AddToDictionary<SpawnPoint>(spawnPoints, spawnPoint);
												}
											}
										}
									}
								}
							}
						}
						else if (spawnPoint.SpawnPointType == SpawnPointType.Ellipse)
						{
							EllipseSpawnPointData ellipseSpawnPointData = spawnPoint.SpawnPointData as EllipseSpawnPointData;
							if (ellipseSpawnPointData != null)
							{
								Rect indices2 = new Rect((int)(spawnPoint.Position.X - ellipseSpawnPointData.SemiaxisA - minXMinY.X), (int)(spawnPoint.Position.Y - ellipseSpawnPointData.SemiaxisB - minXMinY.Y), (int)(spawnPoint.Position.X + ellipseSpawnPointData.SemiaxisA - minXMinY.X + 1.0), (int)(spawnPoint.Position.Y + ellipseSpawnPointData.SemiaxisB - minXMinY.Y + 1.0));
								if (indices2.Validate(bounds) >= 0)
								{
									for (int x3 = indices2.Min.X; x3 < indices2.Max.X; x3++)
									{
										for (int y3 = indices2.Min.Y; y3 < indices2.Max.Y; y3++)
										{
											if (!this.editorScene.IsPassable(x3, y3))
											{
												Vec2 center3 = new Vec2(minXMinY.X + (double)x3 + 0.5, minXMinY.Y + (double)y3 + 0.5);
												if (PassabilityChecker.EllipseCrossSquare(ref center3, spawnPoint.Position.X, spawnPoint.Position.Y, ellipseSpawnPointData.SemiaxisA, ellipseSpawnPointData.SemiaxisB))
												{
													PassabilityChecker.AddToDictionary<SpawnPoint>(spawnPoints, spawnPoint);
												}
											}
										}
									}
								}
							}
						}
						else if (spawnPoint.SpawnPointType == SpawnPointType.SpawnCircle)
						{
							SpawnCircleSpawnPointData spawnCircleSpawnPointData = spawnPoint.SpawnPointData as SpawnCircleSpawnPointData;
							if (spawnCircleSpawnPointData != null)
							{
								Rect indices3 = new Rect((int)(spawnPoint.Position.X - spawnCircleSpawnPointData.Radius - minXMinY.X), (int)(spawnPoint.Position.Y - spawnCircleSpawnPointData.Radius - minXMinY.Y), (int)(spawnPoint.Position.X + spawnCircleSpawnPointData.Radius - minXMinY.X + 1.0), (int)(spawnPoint.Position.Y + spawnCircleSpawnPointData.Radius - minXMinY.Y + 1.0));
								if (indices3.Validate(bounds) >= 0)
								{
									for (int x4 = indices3.Min.X; x4 < indices3.Max.X; x4++)
									{
										for (int y4 = indices3.Min.Y; y4 < indices3.Max.Y; y4++)
										{
											if (!this.editorScene.IsPassable(x4, y4))
											{
												Vec2 center4 = new Vec2(minXMinY.X + (double)x4 + 0.5, minXMinY.Y + (double)y4 + 0.5);
												if (PassabilityChecker.CircleCrossSquare(ref center4, spawnPoint.Position.X, spawnPoint.Position.Y, spawnCircleSpawnPointData.Radius))
												{
													PassabilityChecker.AddToDictionary<SpawnPoint>(spawnPoints, spawnPoint);
												}
											}
										}
									}
								}
							}
						}
					}
				}
			}
			foreach (KeyValuePair<int, IMapObject> keyValuePair2 in map.MapEditorMapObjectContainer.PatrolNodeContainer.MapObjects)
			{
				PatrolNode patrolNode = keyValuePair2.Value as PatrolNode;
				if (patrolNode != null)
				{
					OutOfTerrainChecker.TerrainPlace terrainPlace2;
					OutOfTerrainChecker.ObjectPlace objectPlace2;
					OutOfTerrainChecker.GetMapObjectPlace(out terrainPlace2, out objectPlace2, patrolNode, map, this.editorScene);
					if (terrainPlace2 != OutOfTerrainChecker.TerrainPlace.Undefined && (terrainPlace2 == OutOfTerrainChecker.TerrainPlace.OnTerrain || terrainPlace2 == OutOfTerrainChecker.TerrainPlace.OverTerrain) && objectPlace2 == OutOfTerrainChecker.ObjectPlace.OutOfObject)
					{
						int x5 = (int)(patrolNode.Position.X - minXMinY.X);
						int y5 = (int)(patrolNode.Position.Y - minXMinY.Y);
						if (bounds.Inside(x5, y5) && !this.editorScene.IsPassable(x5, y5))
						{
							Vec2 center5 = new Vec2(minXMinY.X + (double)x5 + 0.5, minXMinY.Y + (double)y5 + 0.5);
							if (PassabilityChecker.PointInsideSquare(ref center5, patrolNode.Position.X, patrolNode.Position.Y))
							{
								PassabilityChecker.AddToDictionary<PatrolNode>(patrolNodes, patrolNode);
							}
						}
					}
				}
			}
			if (spawnPoints.Count > 0)
			{
				string name = SpawnPoint.InterfaceSeveralObjectsTypeName;
				foreach (KeyValuePair<SpawnPoint, int> keyValuePair3 in spawnPoints)
				{
					string tableType;
					if (keyValuePair3.Key.SpawnTableType == SpawnTableType.SingleMob)
					{
						tableType = Strings.SPAWN_POINT_OBJECT_TYPE_SINGLE_MOB;
					}
					else if (keyValuePair3.Key.SpawnTableType == SpawnTableType.SingleDevice)
					{
						tableType = Strings.SPAWN_POINT_OBJECT_TYPE_SINGLE_DEVICE;
					}
					else
					{
						tableType = Strings.SPAWN_POINT_OBJECT_TYPE_TABLE;
					}
					string pointType;
					if (keyValuePair3.Key.SpawnPointType == SpawnPointType.Circle)
					{
						pointType = Strings.SPAWN_POINT_TYPE_CIRCLE;
					}
					else if (keyValuePair3.Key.SpawnPointType == SpawnPointType.Ellipse)
					{
						pointType = Strings.SPAWN_POINT_TYPE_ELLIPSE;
					}
					else if (keyValuePair3.Key.SpawnPointType == SpawnPointType.Patrol)
					{
						pointType = Strings.SPAWN_POINT_TYPE_PATROL;
					}
					else if (keyValuePair3.Key.SpawnPointType == SpawnPointType.SpawnCircle)
					{
						pointType = Strings.SPAWN_POINT_TYPE_SPAWN_CIRCLE;
					}
					else
					{
						pointType = Strings.SPAWN_POINT_TYPE_GUARD;
					}
					base.LongInfoView.FindOrAddNode(name, true).FindOrAddNode(tableType, true).FindOrAddNode(pointType, false).AddNode(keyValuePair3.Key, string.Format("{0}, count: {1}", keyValuePair3.Key.SceneName, keyValuePair3.Value), false);
				}
			}
			if (patrolNodes.Count > 0)
			{
				string name2 = PatrolNode.InterfaceSeveralObjectsTypeName;
				foreach (KeyValuePair<PatrolNode, int> keyValuePair4 in patrolNodes)
				{
					base.LongInfoView.FindOrAddNode(name2, false).AddNode(keyValuePair4.Key, string.Format("{0}, count: {1}", keyValuePair4.Key.SceneName, keyValuePair4.Value), false);
				}
			}
			if (spawnPoints.Count + patrolNodes.Count > 0)
			{
				base.Status = MapCheckerStatus.Red;
				base.ShortInfo = Strings.PASSABILITY_CHECKER_SHORT_INFO_NOT_OK;
				base.LongResult = string.Format(Strings.PASSABILITY_CHECKER_LONG_RESULT_NOT_OK, spawnPoints.Count, patrolNodes.Count);
			}
			else
			{
				base.Status = MapCheckerStatus.Green;
				base.ShortInfo = Strings.PASSABILITY_CHECKER_SHORT_INFO_OK;
				base.LongResult = Strings.PASSABILITY_CHECKER_LONG_RESULT_OK;
			}
			base.ShortResult = string.Format("{0}", spawnPoints.Count + patrolNodes.Count);
			if (progressContainer != null)
			{
				progressContainer.Progress++;
			}
		}

		// Token: 0x04000C2D RID: 3117
		private static readonly double squareHalfSize = 0.5;

		// Token: 0x04000C2E RID: 3118
		private static readonly double squareHalfDiagonal = Math.Sqrt(2.0) / 2.0;

		// Token: 0x04000C2F RID: 3119
		private readonly EditorScene editorScene;
	}
}
