using System;
using MapEditor.Resources.Strings;
using Tools.Progress;

namespace MapEditor.Map.SaveLoad.DataSources
{
	// Token: 0x020002BA RID: 698
	internal class TerrainDataSource : SaveLoad.IDataSource
	{
		// Token: 0x060020A0 RID: 8352 RVA: 0x000CF038 File Offset: 0x000CE038
		public TerrainDataSource(bool _createTerrain, bool _createBottom)
		{
			this.createTerrain = _createTerrain;
			this.createBottom = _createBottom;
		}

		// Token: 0x17000753 RID: 1875
		// (get) Token: 0x060020A1 RID: 8353 RVA: 0x000CF04E File Offset: 0x000CE04E
		// (set) Token: 0x060020A2 RID: 8354 RVA: 0x000CF056 File Offset: 0x000CE056
		public bool Modified
		{
			get
			{
				return this.modified;
			}
			set
			{
				this.modified = value;
			}
		}

		// Token: 0x060020A3 RID: 8355 RVA: 0x000CF05F File Offset: 0x000CE05F
		public int GetProgressSteps(bool forSave)
		{
			return 1;
		}

		// Token: 0x060020A4 RID: 8356 RVA: 0x000CF064 File Offset: 0x000CE064
		public bool Save(MapEditorMap map, MainForm.Context context, IProgressContainer progressContainer)
		{
			if (progressContainer != null)
			{
				progressContainer.AddString(Strings.SAVING_TERRAIN);
			}
			bool result = true;
			if (this.modified)
			{
				result = context.EditorScene.SaveTerrain();
				this.modified = false;
			}
			if (progressContainer != null)
			{
				progressContainer.Progress++;
			}
			return result;
		}

		// Token: 0x060020A5 RID: 8357 RVA: 0x000CF0B0 File Offset: 0x000CE0B0
		public bool Load(MapEditorMap map, MainForm.Context context, IProgressContainer progressContainer, out bool somethingCreated)
		{
			if (progressContainer != null)
			{
				progressContainer.AddString(Strings.LOADING_TERRAIN);
			}
			somethingCreated = context.EditorScene.CreateTerrainRegion(Constants.ContinentFolder(map.Data.ContinentName), map.Data.MinXMinYPatchCoords.X, map.Data.MinXMinYPatchCoords.Y, map.Data.MapSize.X, map.Data.MapSize.Y, this.createTerrain, this.createBottom);
			context.EditorScene.UpdateWater();
			this.modified = somethingCreated;
			if (progressContainer != null)
			{
				progressContainer.Progress++;
			}
			return true;
		}

		// Token: 0x040013F1 RID: 5105
		private readonly bool createTerrain;

		// Token: 0x040013F2 RID: 5106
		private readonly bool createBottom;

		// Token: 0x040013F3 RID: 5107
		private bool modified;
	}
}
