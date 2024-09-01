using System;
using System.Drawing;
using System.IO;
using System.Runtime.InteropServices;
using MapEditor.Resources.Strings;
using Tools.Progress;

namespace MapEditor.Map.SaveLoad.DataSources
{
	// Token: 0x020002BE RID: 702
	internal class MinimapDataSource : SaveLoad.IDataSource
	{
		// Token: 0x060020BD RID: 8381 RVA: 0x000D0AE8 File Offset: 0x000CFAE8
		private static bool SaveMegamap(MapEditorMap map)
		{
			if (map != null && map.MinimapContainer != null)
			{
				Bitmap minimap;
				map.MinimapContainer.GetTerrainMinimap(out minimap);
				if (minimap != null)
				{
					try
					{
						Bitmap megamap = new Bitmap(Constants.MegamapSize.X, Constants.MegamapSize.Y);
						Graphics graphics = Graphics.FromImage(megamap);
						string path = Constants.GetLastChangedMegamapPath(map.Data.ContinentName);
						if (File.Exists(path))
						{
							Bitmap olbMgmBitmap = new Bitmap(path);
							Rectangle mgmRectangle = new Rectangle(0, 0, megamap.Width, megamap.Height);
							graphics.DrawImage(olbMgmBitmap, mgmRectangle);
							olbMgmBitmap.Dispose();
						}
						int xBeg = Constants.MegamapSize.X * map.Data.MinXMinYPatchCoords.X / (Constants.WorldBounds.Width + 1);
						int yBeg = Constants.MegamapSize.Y - Constants.MegamapSize.Y * (map.Data.MinXMinYPatchCoords.Y + map.Data.MapSize.Y) / (Constants.WorldBounds.Height + 1);
						int xEnd = Constants.MegamapSize.X * (map.Data.MinXMinYPatchCoords.X + map.Data.MapSize.X) / (Constants.WorldBounds.Width + 1);
						int yEnd = Constants.MegamapSize.Y - Constants.MegamapSize.Y * map.Data.MinXMinYPatchCoords.Y / (Constants.WorldBounds.Height + 1);
						graphics.DrawImage(minimap, xBeg, yBeg, xEnd - xBeg, yEnd - yBeg);
						megamap.Save(Constants.MegamapLocalPath(map.Data.ContinentName));
						return true;
					}
					catch (ExternalException e)
					{
						Console.WriteLine(e);
					}
					return false;
				}
			}
			return false;
		}

		// Token: 0x060020BE RID: 8382 RVA: 0x000D0D08 File Offset: 0x000CFD08
		public int GetProgressSteps(bool forSave)
		{
			return 1;
		}

		// Token: 0x060020BF RID: 8383 RVA: 0x000D0D0C File Offset: 0x000CFD0C
		public bool Save(MapEditorMap map, MainForm.Context context, IProgressContainer progressContainer)
		{
			if (progressContainer != null)
			{
				progressContainer.AddString(Strings.SAVING_MINIMAP);
			}
			bool result = MinimapDataSource.SaveMegamap(map);
			if (progressContainer != null)
			{
				progressContainer.Progress++;
			}
			return result;
		}

		// Token: 0x060020C0 RID: 8384 RVA: 0x000D0D40 File Offset: 0x000CFD40
		public bool Load(MapEditorMap map, MainForm.Context context, IProgressContainer progressContainer, out bool somethingCreated)
		{
			somethingCreated = false;
			if (progressContainer != null)
			{
				progressContainer.AddString(Strings.LOADING_MINIMAP);
			}
			if (progressContainer != null)
			{
				progressContainer.Progress++;
			}
			return true;
		}
	}
}
