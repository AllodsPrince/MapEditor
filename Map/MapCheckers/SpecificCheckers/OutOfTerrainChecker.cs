using System;
using System.Collections.Generic;
using MapEditor.Map.MapObjects;
using MapEditor.Resources.Strings;
using Tools.MapObjects;
using Tools.Progress;

namespace MapEditor.Map.MapCheckers.SpecificCheckers
{
	// Token: 0x0200017D RID: 381
	public class OutOfTerrainChecker : MapChecker
	{
		// Token: 0x0600124A RID: 4682 RVA: 0x00084D90 File Offset: 0x00083D90
		public static void GetMapObjectPlace(out OutOfTerrainChecker.TerrainPlace terrainPlace, out OutOfTerrainChecker.ObjectPlace objectPlace, IMapObject mapObject, MapEditorMap map, EditorScene editorScene)
		{
			terrainPlace = OutOfTerrainChecker.TerrainPlace.Undefined;
			objectPlace = OutOfTerrainChecker.ObjectPlace.Undefined;
			if (mapObject != null && map != null && editorScene != null)
			{
				Position position = mapObject.Position;
				int x;
				int y;
				double terrainHeight;
				if (!map.Data.GetPatch(ref position, out x, out y))
				{
					terrainHeight = mapObject.Position.Z;
					terrainPlace = OutOfTerrainChecker.TerrainPlace.OutOfMap;
				}
				else if (editorScene.DoesTerrainRegionExist(0, x, y))
				{
					terrainHeight = map.MapEditorMapObjectContainer.GetTerrainHeight(0, mapObject.Position.X, mapObject.Position.Y);
					if (editorScene.DoesTerrainRegionExist(1, x, y))
					{
						double bottomHeight = map.MapEditorMapObjectContainer.GetTerrainHeight(1, mapObject.Position.X, mapObject.Position.Y);
						if (terrainHeight < bottomHeight)
						{
							terrainPlace = OutOfTerrainChecker.TerrainPlace.OutOfTerrain;
						}
					}
				}
				else
				{
					terrainHeight = mapObject.Position.Z;
					terrainPlace = OutOfTerrainChecker.TerrainPlace.OutOfTerrain;
				}
				if (terrainPlace == OutOfTerrainChecker.TerrainPlace.Undefined)
				{
					if (mapObject.Position.Z > terrainHeight + OutOfTerrainChecker.positiveThreshold)
					{
						terrainPlace = OutOfTerrainChecker.TerrainPlace.OverTerrain;
					}
					else if (mapObject.Position.Z < terrainHeight - OutOfTerrainChecker.negativeThreshold)
					{
						terrainPlace = OutOfTerrainChecker.TerrainPlace.UnderTerrain;
					}
					else
					{
						terrainPlace = OutOfTerrainChecker.TerrainPlace.OnTerrain;
					}
				}
				objectPlace = OutOfTerrainChecker.ObjectPlace.OutOfObject;
				bool exists;
				double nearestHeight = map.MapEditorMapObjectContainer.GetNearestFlatHeight(ref position, mapObject.ID, out exists);
				if (exists && Math.Abs(nearestHeight - terrainHeight) > MathConsts.DOUBLE_EPSILON)
				{
					if (mapObject.Position.Z > nearestHeight + OutOfTerrainChecker.positiveThreshold)
					{
						objectPlace = OutOfTerrainChecker.ObjectPlace.OverObject;
						return;
					}
					objectPlace = OutOfTerrainChecker.ObjectPlace.OnObject;
				}
			}
		}

		// Token: 0x0600124B RID: 4683 RVA: 0x00084F03 File Offset: 0x00083F03
		public OutOfTerrainChecker(EditorScene _editorScene)
		{
			this.editorScene = _editorScene;
			base.Name = Strings.OUT_OF_TERRAIN_CHECKER_NAME;
			base.ShortDescription = Strings.OUT_OF_TERRAIN_CHECKER_SHORT_DESCRIPTION;
			base.LongDescription = Strings.OUT_OF_TERRAIN_CHECKER_LONG_DESCRIPTION;
		}

		// Token: 0x0600124C RID: 4684 RVA: 0x00084F34 File Offset: 0x00083F34
		public override void Check(MapEditorMap map, IProgressContainer progressContainer)
		{
			if (progressContainer != null)
			{
				progressContainer.AddString(Strings.RUN_OUT_OF_TERRAIN_CHECKER);
			}
			List<int[,]> patchCount = new List<int[,]>();
			List<int> totalCount = new List<int>();
			for (int index = 0; index < 4; index++)
			{
				List<int[,]> list = patchCount;
				int[,] item = new int[4, 4];
				list.Add(item);
				totalCount.Add(0);
			}
			base.LongInfoView = new LongInfoViewNode(true);
			foreach (KeyValuePair<int, IMapObject> keyValuePair in map.MapEditorMapObjectContainer.MapObjects)
			{
				IMapObject mapObject = keyValuePair.Value;
				if (mapObject != null)
				{
					OutOfTerrainChecker.TerrainPlace terrainPlace;
					OutOfTerrainChecker.ObjectPlace objectPlace;
					OutOfTerrainChecker.GetMapObjectPlace(out terrainPlace, out objectPlace, mapObject, map, this.editorScene);
					if (terrainPlace != OutOfTerrainChecker.TerrainPlace.Undefined)
					{
						if (terrainPlace == OutOfTerrainChecker.TerrainPlace.OutOfMap)
						{
							base.LongInfoView.FindOrAddNode(Strings.OUT_OF_TERRAIN_CHECKER_NAME, true).FindOrAddNode(MapObjectInterface.GetInterfaceSeveralObjectsTypeName(mapObject), false).AddNode(mapObject, false);
						}
						else
						{
							Position position = mapObject.Position;
							int x;
							int y;
							if (map.Data.GetPatch(ref position, out x, out y))
							{
								if (terrainPlace == OutOfTerrainChecker.TerrainPlace.OverTerrain)
								{
									patchCount[0][x, y]++;
									List<int> list2;
									(list2 = totalCount)[0] = list2[0] + 1;
									if (objectPlace != OutOfTerrainChecker.ObjectPlace.OnObject)
									{
										base.LongInfoView.FindOrAddNode(Strings.OVER_TERRAIN_CHECKER_NAME, true).FindOrAddNode(MapObjectInterface.GetInterfaceSeveralObjectsTypeName(mapObject), false).AddNode(mapObject, false);
									}
								}
								else if (terrainPlace == OutOfTerrainChecker.TerrainPlace.OnTerrain)
								{
									patchCount[1][x, y]++;
									List<int> list3;
									(list3 = totalCount)[1] = list3[1] + 1;
								}
								else if (terrainPlace == OutOfTerrainChecker.TerrainPlace.UnderTerrain)
								{
									patchCount[2][x, y]++;
									List<int> list4;
									(list4 = totalCount)[2] = list4[2] + 1;
									if (objectPlace != OutOfTerrainChecker.ObjectPlace.OnObject)
									{
										base.LongInfoView.FindOrAddNode(Strings.UNDER_TERRAIN_CHECKER_NAME, true).FindOrAddNode(MapObjectInterface.GetInterfaceSeveralObjectsTypeName(mapObject), false).AddNode(mapObject, false);
									}
								}
								else if (terrainPlace == OutOfTerrainChecker.TerrainPlace.OutOfTerrain)
								{
									patchCount[3][x, y]++;
									List<int> list5;
									(list5 = totalCount)[3] = list5[3] + 1;
									if (objectPlace != OutOfTerrainChecker.ObjectPlace.OnObject)
									{
										base.LongInfoView.FindOrAddNode(Strings.OUT_OF_TERRAIN_CHECKER_NAME, true).FindOrAddNode(MapObjectInterface.GetInterfaceSeveralObjectsTypeName(mapObject), false).AddNode(mapObject, false);
									}
								}
							}
						}
					}
				}
			}
			base.Status = MapCheckerStatus.Green;
			if (base.Status == MapCheckerStatus.Red)
			{
				base.ShortInfo = Strings.OUT_OF_TERRAIN_CHECKER_RED;
			}
			else
			{
				base.ShortInfo = Strings.CHECKER_OK;
			}
			int totalOutOfTerrain = base.LongInfoView.GetLeafCountByChild(Strings.OUT_OF_TERRAIN_CHECKER_NAME);
			int totalUnderTerrain = base.LongInfoView.GetLeafCountByChild(Strings.UNDER_TERRAIN_CHECKER_NAME);
			int totalOverTerrain = base.LongInfoView.GetLeafCountByChild(Strings.OVER_TERRAIN_CHECKER_NAME);
			base.ShortResult = string.Format(Strings.OUT_OF_TERRAIN_CHECKER_SHORT_RESULT, totalOutOfTerrain, totalUnderTerrain, totalOverTerrain);
			base.LongResult = string.Format(Strings.OUT_OF_TERRAIN_CHECKER_LONG_RESULT, totalOutOfTerrain, totalUnderTerrain, totalOverTerrain);
			base.LongInfoText = string.Format("{0}: {1}", Strings.OVER_TERRAIN_CHECKER_NAME, totalCount[0]);
			base.LongInfoText += "\r\n";
			base.LongInfoText += MapCheckerHelper.FormatTable(patchCount[0], OutOfTerrainChecker.yellowCountForPatch, OutOfTerrainChecker.redCountForPatch);
			base.LongInfoText += "\r\n\r\n";
			base.LongInfoText += string.Format("{0}: {1}", Strings.ON_TERRAIN_CHECKER_NAME, totalCount[1]);
			base.LongInfoText += "\r\n";
			base.LongInfoText += MapCheckerHelper.FormatTable(patchCount[1], OutOfTerrainChecker.yellowCountForPatch, OutOfTerrainChecker.redCountForPatch);
			base.LongInfoText += "\r\n\r\n";
			base.LongInfoText += string.Format("{0}: {1}", Strings.UNDER_TERRAIN_CHECKER_NAME, totalCount[2]);
			base.LongInfoText += "\r\n";
			base.LongInfoText += MapCheckerHelper.FormatTable(patchCount[2], OutOfTerrainChecker.yellowCountForPatch, OutOfTerrainChecker.redCountForPatch);
			base.LongInfoText += "\r\n\r\n";
			base.LongInfoText += string.Format("{0}: {1}", Strings.OUT_OF_TERRAIN_CHECKER_NAME, totalCount[3]);
			base.LongInfoText += "\r\n";
			base.LongInfoText += MapCheckerHelper.FormatTable(patchCount[3], OutOfTerrainChecker.yellowCountForPatch, OutOfTerrainChecker.redCountForPatch);
			if (progressContainer != null)
			{
				progressContainer.Progress++;
			}
		}

		// Token: 0x04000CF3 RID: 3315
		private readonly EditorScene editorScene;

		// Token: 0x04000CF4 RID: 3316
		private static readonly int yellowCountForPatch = 300;

		// Token: 0x04000CF5 RID: 3317
		private static readonly int redCountForPatch = 500;

		// Token: 0x04000CF6 RID: 3318
		private static readonly double negativeThreshold = 2.0;

		// Token: 0x04000CF7 RID: 3319
		private static readonly double positiveThreshold = 2.0;

		// Token: 0x0200017E RID: 382
		public enum TerrainPlace
		{
			// Token: 0x04000CF9 RID: 3321
			Undefined,
			// Token: 0x04000CFA RID: 3322
			OutOfMap,
			// Token: 0x04000CFB RID: 3323
			OverTerrain,
			// Token: 0x04000CFC RID: 3324
			OnTerrain,
			// Token: 0x04000CFD RID: 3325
			UnderTerrain,
			// Token: 0x04000CFE RID: 3326
			OutOfTerrain
		}

		// Token: 0x0200017F RID: 383
		public enum ObjectPlace
		{
			// Token: 0x04000D00 RID: 3328
			Undefined,
			// Token: 0x04000D01 RID: 3329
			OverObject,
			// Token: 0x04000D02 RID: 3330
			OnObject,
			// Token: 0x04000D03 RID: 3331
			OutOfObject
		}
	}
}
