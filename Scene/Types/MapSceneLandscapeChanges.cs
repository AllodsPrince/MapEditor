using System;
using System.Collections.Generic;
using Tools.Geometry;
using Tools.Landscape;

namespace MapEditor.Scene.Types
{
	// Token: 0x020001ED RID: 493
	internal class MapSceneLandscapeChanges
	{
		// Token: 0x060018C8 RID: 6344 RVA: 0x000A4E8C File Offset: 0x000A3E8C
		private void OnLandscapeChangesAdded(LandscapeChangesContainer landscapeChangesContainer, ILandscapeChanges landscapeChanges)
		{
			if (this.mapEditorScene.EditorScene != null)
			{
				int editorSceneTerrainChangesID = this.mapEditorScene.EditorScene.CreateLandscapeChanges(0);
				int editorSceneBottomChangesID = this.mapEditorScene.EditorScene.CreateLandscapeChanges(1);
				if (editorSceneTerrainChangesID != -1 || editorSceneBottomChangesID != -1)
				{
					this.editorSceneLandscapeChangesIDMap[landscapeChanges.ID] = new MapSceneLandscapeChanges.EditorSceneLandscapeChangesID(editorSceneTerrainChangesID, editorSceneBottomChangesID);
					return;
				}
				landscapeChanges.SetEmpty();
			}
		}

		// Token: 0x060018C9 RID: 6345 RVA: 0x000A4EF4 File Offset: 0x000A3EF4
		private void OnLandscapeChangesRemoved(LandscapeChangesContainer landscapeChangesContainer, ILandscapeChanges landscapeChanges)
		{
			MapSceneLandscapeChanges.EditorSceneLandscapeChangesID editorSceneLandscapeChangesID;
			if (this.mapEditorScene.EditorScene != null && this.editorSceneLandscapeChangesIDMap.TryGetValue(landscapeChanges.ID, out editorSceneLandscapeChangesID))
			{
				this.mapEditorScene.EditorScene.DeleteLandscapeChanges(editorSceneLandscapeChangesID.TerrainID);
				this.mapEditorScene.EditorScene.DeleteLandscapeChanges(editorSceneLandscapeChangesID.BottomID);
				this.editorSceneLandscapeChangesIDMap.Remove(landscapeChanges.ID);
			}
		}

		// Token: 0x060018CA RID: 6346 RVA: 0x000A4F64 File Offset: 0x000A3F64
		private void OnLandscapeChangesUndoApplying(LandscapeChangesContainer landscapeChangesContainer, ILandscapeChanges landscapeChanges)
		{
			MapSceneLandscapeChanges.EditorSceneLandscapeChangesID editorSceneLandscapeChangesID;
			if (this.mapEditorScene.EditorScene != null && this.editorSceneLandscapeChangesIDMap.TryGetValue(landscapeChanges.ID, out editorSceneLandscapeChangesID))
			{
				bool terrainAffectTile;
				bool terrainAffectHeight;
				Rect terrainAffectedRect;
				this.mapEditorScene.EditorScene.UndoLandscapeChanges(editorSceneLandscapeChangesID.TerrainID, out terrainAffectTile, out terrainAffectHeight, out terrainAffectedRect);
				bool bottomAffectTile;
				bool bottomAffectHeight;
				Rect bottomAffectedRect;
				this.mapEditorScene.EditorScene.UndoLandscapeChanges(editorSceneLandscapeChangesID.BottomID, out bottomAffectTile, out bottomAffectHeight, out bottomAffectedRect);
				landscapeChanges.AffectParams.AffectTile = (terrainAffectTile || bottomAffectTile);
				landscapeChanges.AffectParams.AffectHeight = (terrainAffectHeight || bottomAffectHeight);
				landscapeChanges.AffectParams.AffectedRect = Rect.Union(terrainAffectedRect, bottomAffectedRect);
			}
		}

		// Token: 0x060018CB RID: 6347 RVA: 0x000A500C File Offset: 0x000A400C
		private void OnLandscapeChangesRedoApplying(LandscapeChangesContainer landscapeChangesContainer, ILandscapeChanges landscapeChanges)
		{
			MapSceneLandscapeChanges.EditorSceneLandscapeChangesID editorSceneLandscapeChangesID;
			if (this.editorSceneLandscapeChangesIDMap.TryGetValue(landscapeChanges.ID, out editorSceneLandscapeChangesID))
			{
				bool terrainAffectTile;
				bool terrainAffectHeight;
				Rect terrainAffectedRect;
				this.mapEditorScene.EditorScene.RedoLandscapeChanges(editorSceneLandscapeChangesID.TerrainID, out terrainAffectTile, out terrainAffectHeight, out terrainAffectedRect);
				bool bottomAffectTile;
				bool bottomAffectHeight;
				Rect bottomAffectedRect;
				this.mapEditorScene.EditorScene.RedoLandscapeChanges(editorSceneLandscapeChangesID.BottomID, out bottomAffectTile, out bottomAffectHeight, out bottomAffectedRect);
				landscapeChanges.AffectParams.AffectTile = (terrainAffectTile || bottomAffectTile);
				landscapeChanges.AffectParams.AffectHeight = (terrainAffectHeight || bottomAffectHeight);
				landscapeChanges.AffectParams.AffectedRect = Rect.Union(terrainAffectedRect, bottomAffectedRect);
			}
		}

		// Token: 0x060018CC RID: 6348 RVA: 0x000A50A4 File Offset: 0x000A40A4
		public void Bind(MapEditorScene _mapEditorScene)
		{
			this.mapEditorScene = _mapEditorScene;
			if (this.mapEditorScene != null && this.mapEditorScene.LandscapeChangesContainer != null)
			{
				this.mapEditorScene.LandscapeChangesContainer.LandscapeChangesAdded += this.OnLandscapeChangesAdded;
				this.mapEditorScene.LandscapeChangesContainer.LandscapeChangesRemoved += this.OnLandscapeChangesRemoved;
				this.mapEditorScene.LandscapeChangesContainer.LandscapeChangesUndoApplying += this.OnLandscapeChangesUndoApplying;
				this.mapEditorScene.LandscapeChangesContainer.LandscapeChangesRedoApplying += this.OnLandscapeChangesRedoApplying;
			}
		}

		// Token: 0x060018CD RID: 6349 RVA: 0x000A5140 File Offset: 0x000A4140
		public void Unbind()
		{
			if (this.mapEditorScene != null)
			{
				if (this.mapEditorScene.LandscapeChangesContainer != null)
				{
					this.mapEditorScene.LandscapeChangesContainer.LandscapeChangesAdded -= this.OnLandscapeChangesAdded;
					this.mapEditorScene.LandscapeChangesContainer.LandscapeChangesRemoved -= this.OnLandscapeChangesRemoved;
					this.mapEditorScene.LandscapeChangesContainer.LandscapeChangesUndoApplying -= this.OnLandscapeChangesUndoApplying;
					this.mapEditorScene.LandscapeChangesContainer.LandscapeChangesRedoApplying -= this.OnLandscapeChangesRedoApplying;
				}
				this.mapEditorScene = null;
			}
		}

		// Token: 0x04001014 RID: 4116
		private readonly Dictionary<int, MapSceneLandscapeChanges.EditorSceneLandscapeChangesID> editorSceneLandscapeChangesIDMap = new Dictionary<int, MapSceneLandscapeChanges.EditorSceneLandscapeChangesID>();

		// Token: 0x04001015 RID: 4117
		private MapEditorScene mapEditorScene;

		// Token: 0x020001EE RID: 494
		internal struct EditorSceneLandscapeChangesID
		{
			// Token: 0x060018CF RID: 6351 RVA: 0x000A51EF File Offset: 0x000A41EF
			public EditorSceneLandscapeChangesID(int _terrainID, int _bottomID)
			{
				this.terrainID = _terrainID;
				this.bottomID = _bottomID;
			}

			// Token: 0x170005F6 RID: 1526
			// (get) Token: 0x060018D0 RID: 6352 RVA: 0x000A51FF File Offset: 0x000A41FF
			public int TerrainID
			{
				get
				{
					return this.terrainID;
				}
			}

			// Token: 0x170005F7 RID: 1527
			// (get) Token: 0x060018D1 RID: 6353 RVA: 0x000A5207 File Offset: 0x000A4207
			public int BottomID
			{
				get
				{
					return this.bottomID;
				}
			}

			// Token: 0x04001016 RID: 4118
			private readonly int terrainID;

			// Token: 0x04001017 RID: 4119
			private readonly int bottomID;
		}
	}
}
