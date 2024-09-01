using System;
using System.Collections.Generic;
using MapEditor.Resources.Strings;
using Tools.Progress;

namespace MapEditor.Map.MapCheckers.SpecificCheckers
{
	// Token: 0x02000068 RID: 104
	public class PatchTerrainBorderChecker : MapChecker
	{
		// Token: 0x0600051E RID: 1310 RVA: 0x0002888F File Offset: 0x0002788F
		public PatchTerrainBorderChecker(EditorScene _editorScene)
		{
			this.editorScene = _editorScene;
			base.Name = Strings.PATCH_TERRAIN_BORDERS_CHECKER_NAME;
			base.ShortDescription = Strings.PATCH_TERRAIN_BORDERS_CHECKER_SHORT_DESCRIPTION;
			base.LongDescription = Strings.PATCH_TERRAIN_BORDERS_CHECKER_LONG_DESCRIPTION;
		}

		// Token: 0x0600051F RID: 1311 RVA: 0x000288C0 File Offset: 0x000278C0
		public override void Check(MapEditorMap map, IProgressContainer progressContainer)
		{
			if (progressContainer != null)
			{
				progressContainer.AddString(Strings.RUN_PATCH_TERRAIN_BORDERS_CHECKER);
			}
			List<List<int>> terrainXX = new List<List<int>>();
			List<List<int>> terrainYY = new List<List<int>>();
			List<List<int>> bottomXX = new List<List<int>>();
			List<List<int>> bottomYY = new List<List<int>>();
			for (int patchX = 0; patchX < map.Data.MapSize.X; patchX++)
			{
				terrainXX.Add(new List<int>());
				terrainYY.Add(new List<int>());
				bottomXX.Add(new List<int>());
				bottomYY.Add(new List<int>());
				for (int patchY = 0; patchY < map.Data.MapSize.Y; patchY++)
				{
					terrainXX[patchX].Add(0);
					terrainYY[patchX].Add(0);
					bottomXX[patchX].Add(0);
					bottomYY[patchX].Add(0);
				}
			}
			for (int patchX2 = 0; patchX2 < map.Data.MapSize.X; patchX2++)
			{
				for (int patchY2 = 0; patchY2 < map.Data.MapSize.Y; patchY2++)
				{
					bool terrain00Exists = this.editorScene.DoesTerrainRegionExist(0, patchX2, patchY2);
					bool bottom00Exists = this.editorScene.DoesTerrainRegionExist(1, patchX2, patchY2);
					if (patchY2 < map.Data.MapSize.Y - 1)
					{
						bool terrain01Exists = this.editorScene.DoesTerrainRegionExist(0, patchX2, patchY2 + 1);
						bool bottom01Exists = this.editorScene.DoesTerrainRegionExist(1, patchX2, patchY2 + 1);
						if (terrain00Exists && terrain01Exists)
						{
							for (int index = 0; index < Constants.PatchSize; index++)
							{
								double height0 = (double)this.editorScene.GetTerrainHeight(0, patchX2 * Constants.PatchSize + index, (patchY2 + 1) * Constants.PatchSize - 1);
								double height = (double)this.editorScene.GetTerrainHeight(0, patchX2 * Constants.PatchSize + index, (patchY2 + 1) * Constants.PatchSize);
								if (Math.Abs(height - height0) > PatchTerrainBorderChecker.heightThrashhold)
								{
									List<int> list;
									int index7;
									(list = terrainXX[patchX2])[index7 = patchY2] = list[index7] + 1;
								}
							}
						}
						if (bottom00Exists && bottom01Exists)
						{
							for (int index2 = 0; index2 < Constants.PatchSize; index2++)
							{
								double height2 = (double)this.editorScene.GetTerrainHeight(1, patchX2 * Constants.PatchSize + index2, (patchY2 + 1) * Constants.PatchSize - 1);
								double height3 = (double)this.editorScene.GetTerrainHeight(1, patchX2 * Constants.PatchSize + index2, (patchY2 + 1) * Constants.PatchSize);
								if (Math.Abs(height3 - height2) > PatchTerrainBorderChecker.heightThrashhold)
								{
									List<int> list2;
									int index8;
									(list2 = bottomXX[patchX2])[index8 = patchY2] = list2[index8] + 1;
								}
							}
						}
					}
					if (patchX2 < map.Data.MapSize.X - 1)
					{
						bool terrain10Exists = this.editorScene.DoesTerrainRegionExist(0, patchX2 + 1, patchY2);
						bool bottom10Exists = this.editorScene.DoesTerrainRegionExist(1, patchX2 + 1, patchY2);
						if (terrain00Exists && terrain10Exists)
						{
							for (int index3 = 0; index3 < Constants.PatchSize; index3++)
							{
								double height4 = (double)this.editorScene.GetTerrainHeight(0, (patchX2 + 1) * Constants.PatchSize - 1, patchY2 * Constants.PatchSize + index3);
								double height5 = (double)this.editorScene.GetTerrainHeight(0, (patchX2 + 1) * Constants.PatchSize, patchY2 * Constants.PatchSize + index3);
								if (Math.Abs(height5 - height4) > PatchTerrainBorderChecker.heightThrashhold)
								{
									List<int> list3;
									int index9;
									(list3 = terrainYY[patchX2])[index9 = patchY2] = list3[index9] + 1;
								}
							}
						}
						if (bottom00Exists && bottom10Exists)
						{
							for (int index4 = 0; index4 < Constants.PatchSize; index4++)
							{
								double height6 = (double)this.editorScene.GetTerrainHeight(1, (patchX2 + 1) * Constants.PatchSize - 1, patchY2 * Constants.PatchSize + index4);
								double height7 = (double)this.editorScene.GetTerrainHeight(1, (patchX2 + 1) * Constants.PatchSize, patchY2 * Constants.PatchSize + index4);
								if (Math.Abs(height7 - height6) > PatchTerrainBorderChecker.heightThrashhold)
								{
									List<int> list4;
									int index10;
									(list4 = bottomYY[patchX2])[index10 = patchY2] = list4[index10] + 1;
								}
							}
						}
					}
				}
			}
			int count = 0;
			for (int patchX3 = 0; patchX3 < map.Data.MapSize.X; patchX3++)
			{
				for (int patchY3 = 0; patchY3 < map.Data.MapSize.Y; patchY3++)
				{
					if (terrainXX[patchX3][patchY3] > 0 || terrainYY[patchX3][patchY3] > 0 || bottomXX[patchX3][patchY3] > 0 || bottomYY[patchX3][patchY3] > 0)
					{
						count++;
					}
				}
			}
			if (count > 0)
			{
				base.Status = MapCheckerStatus.Red;
				base.ShortInfo = Strings.PATCH_TERRAIN_BORDERS_CHECKER_NOT_OK;
				base.LongInfoText = Strings.PATCH_TERRAIN_BORDERS_TERRAIN;
				base.LongInfoText += "\r\n";
				for (int patchY4 = map.Data.MapSize.Y - 1; patchY4 >= 0; patchY4--)
				{
					base.LongInfoText += "-";
					for (int patchX4 = 0; patchX4 < map.Data.MapSize.X; patchX4++)
					{
						if (terrainXX[patchX4][patchY4] > 0)
						{
							base.LongInfoText += "------------";
						}
						else
						{
							base.LongInfoText += "            ";
						}
						base.LongInfoText += "-";
					}
					base.LongInfoText += "\r\n";
					for (int index5 = 0; index5 < 2; index5++)
					{
						base.LongInfoText += " ";
						for (int patchX5 = 0; patchX5 < map.Data.MapSize.X; patchX5++)
						{
							base.LongInfoText += "            ";
							if (terrainYY[patchX5][patchY4] > 0)
							{
								base.LongInfoText += "|";
							}
							else
							{
								base.LongInfoText += " ";
							}
						}
						base.LongInfoText += "\r\n";
					}
				}
				base.LongInfoText += "-";
				for (int patchX6 = 0; patchX6 < map.Data.MapSize.X; patchX6++)
				{
					base.LongInfoText += "            ";
					base.LongInfoText += "-";
				}
				base.LongInfoText += "\r\n";
				base.LongInfoText += "\r\n";
				base.LongInfoText += Strings.PATCH_TERRAIN_BORDERS_BOTTOM;
				base.LongInfoText += "\r\n";
				for (int patchY5 = map.Data.MapSize.Y - 1; patchY5 >= 0; patchY5--)
				{
					base.LongInfoText += "-";
					for (int patchX7 = 0; patchX7 < map.Data.MapSize.X; patchX7++)
					{
						if (bottomXX[patchX7][patchY5] > 0)
						{
							base.LongInfoText += "------------";
						}
						else
						{
							base.LongInfoText += "            ";
						}
						base.LongInfoText += "-";
					}
					base.LongInfoText += "\r\n";
					for (int index6 = 0; index6 < 2; index6++)
					{
						base.LongInfoText += " ";
						for (int patchX8 = 0; patchX8 < map.Data.MapSize.X; patchX8++)
						{
							base.LongInfoText += "            ";
							if (bottomYY[patchX8][patchY5] > 0)
							{
								base.LongInfoText += "|";
							}
							else
							{
								base.LongInfoText += " ";
							}
						}
						base.LongInfoText += "\r\n";
					}
				}
				base.LongInfoText += "-";
				for (int patchX9 = 0; patchX9 < map.Data.MapSize.X; patchX9++)
				{
					base.LongInfoText += "            ";
					base.LongInfoText += "-";
				}
			}
			else
			{
				base.Status = MapCheckerStatus.Green;
				base.ShortInfo = Strings.CHECKER_OK;
				base.LongInfoText = string.Empty;
			}
			base.ShortResult = string.Format("{0}", count);
			base.LongResult = string.Format(Strings.PATCH_TERRAIN_BORDERS_CHECKER_LONG_RESULT, count);
			if (progressContainer != null)
			{
				progressContainer.Progress++;
			}
		}

		// Token: 0x040003AF RID: 943
		private static readonly double heightThrashhold = 5.0;

		// Token: 0x040003B0 RID: 944
		private readonly EditorScene editorScene;
	}
}
