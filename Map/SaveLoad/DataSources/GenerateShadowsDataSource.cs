using System;
using System.Collections.Generic;
using System.Drawing;
using MapEditor.Map.MapObjects;
using MapEditor.Resources.Strings;
using MapEditor.Scene;
using Tools.Geometry;
using Tools.MapObjects;
using Tools.Progress;

namespace MapEditor.Map.SaveLoad.DataSources
{
	// Token: 0x02000009 RID: 9
	internal class GenerateShadowsDataSource : SaveLoad.IDataSource
	{
		// Token: 0x06000029 RID: 41 RVA: 0x000034E2 File Offset: 0x000024E2
		public GenerateShadowsDataSource(MapObjectSelector _mapObjectSelector, MapEditorScene _mapEditorScene, bool _local)
		{
			this.mapObjectSelector = _mapObjectSelector;
			this.mapEditorScene = _mapEditorScene;
			this.local = _local;
		}

		// Token: 0x0600002A RID: 42 RVA: 0x00003511 File Offset: 0x00002511
		public int GetProgressSteps(bool forSave)
		{
			return 2;
		}

		// Token: 0x0600002B RID: 43 RVA: 0x00003514 File Offset: 0x00002514
		public bool Save(MapEditorMap map, MainForm.Context context, IProgressContainer progressContainer)
		{
			if (context == null || context.EditorScene == null || this.mapObjectSelector == null || this.mapEditorScene == null)
			{
				return false;
			}
			if (progressContainer != null)
			{
				progressContainer.AddString(Strings.CREATE_GENERATE_SHADOWS_DATA);
			}
			List<IMapObject> mapObjects = new List<IMapObject>();
			foreach (KeyValuePair<int, IMapObject> keyValuePair in map.MapEditorMapObjectContainer.MapObjects)
			{
				if (keyValuePair.Value != null && !keyValuePair.Value.Temporary && (keyValuePair.Value.Type.Type == MapObjectFactory.Type.StaticObject || keyValuePair.Value.Type.Type == MapObjectFactory.Type.PermanentDevice) && !string.IsNullOrEmpty(keyValuePair.Value.Type.Stats))
				{
					mapObjects.Add(keyValuePair.Value);
				}
			}
			mapObjects.Sort(MapObjectContainer.MapObjectIDComparer);
			foreach (IMapObject mapObject in mapObjects)
			{
				Tools.Geometry.Point localPatch = new Tools.Geometry.Point(Constants.PatchIndex(mapObject.Position.X), Constants.PatchIndex(mapObject.Position.Y));
				int mapRegionIndex = localPatch.X * (Constants.WorldBounds.Size.X * 10) + localPatch.Y;
				int mapObjectIndex;
				if (this.indices.TryGetValue(mapRegionIndex, out mapObjectIndex))
				{
					mapObjectIndex++;
					int editorSceneObjectID = this.mapEditorScene.MapSceneObjects.MapObjectIDToEditorSceneObjectID(mapObject.ID);
					if (editorSceneObjectID != -1)
					{
						context.EditorScene.AddGenerateShadowsData(editorSceneObjectID, localPatch, mapObjectIndex);
					}
					this.indices[mapRegionIndex] = mapObjectIndex;
				}
				else
				{
					mapObjectIndex = 0;
					int editorSceneObjectID2 = this.mapEditorScene.MapSceneObjects.MapObjectIDToEditorSceneObjectID(mapObject.ID);
					if (editorSceneObjectID2 != -1)
					{
						context.EditorScene.AddGenerateShadowsData(editorSceneObjectID2, localPatch, mapObjectIndex);
					}
					this.indices.Add(mapRegionIndex, mapObjectIndex);
				}
			}
			if (progressContainer != null)
			{
				progressContainer.AddString(Strings.GENERATE_SHADOWS);
				progressContainer.Progress++;
			}
			if (this.local)
			{
				Position position = this.mapObjectSelector.Position;
				foreach (IMapObject selectedObject in this.mapObjectSelector.MapObjects.Values)
				{
					context.EditorScene.SelectObject(selectedObject.ID, false, Color.Empty);
				}
				int deltaXY = 128;
				int x = (int)(position.X - (double)(map.Data.MinXMinYPatchCoords.X * Constants.PatchSize));
				int y = (int)(position.Y - (double)(map.Data.MinXMinYPatchCoords.Y * Constants.PatchSize));
				int xMin = x - deltaXY;
				int xMax = x + deltaXY;
				int yMin = y - deltaXY;
				int yMax = y + deltaXY;
				int mapSize = map.Data.GetMapSize();
				xMin = Math.Max(0, Math.Min(xMin, mapSize));
				xMax = Math.Max(0, Math.Min(xMax, mapSize));
				yMin = Math.Max(0, Math.Min(yMin, mapSize));
				yMax = Math.Max(0, Math.Min(yMax, mapSize));
				float zTerrMin;
				float zTerrMax;
				context.EditorScene.GetTerrainHeightMinMax(0, xMin, yMin, xMax, yMax, out zTerrMin, out zTerrMax);
				double zMin = (double)zTerrMin;
				double zMax = (double)zTerrMax;
				foreach (IMapObject mapObject2 in map.MapEditorMapObjectContainer.MapObjects.Values)
				{
					Position delta = mapObject2.Position - position;
					if (delta.X < (double)deltaXY && delta.Y < (double)deltaXY)
					{
						if (position.Z < zMin)
						{
							zMin = position.Z;
						}
						if (position.Z > zMax)
						{
							zMax = position.Z;
						}
					}
				}
				float deltaZ = (float)Math.Max(Math.Abs(zMin - position.Z), Math.Abs(zMax - position.Z)) + 1f;
				context.EditorScene.GenerateShadowsLocal(ref position, (float)deltaXY, (float)deltaXY, deltaZ);
				context.UpdateScene();
				using (Dictionary<IMapObject, IMapObject>.ValueCollection.Enumerator enumerator5 = this.mapObjectSelector.MapObjects.Values.GetEnumerator())
				{
					while (enumerator5.MoveNext())
					{
						IMapObject selectedObject2 = enumerator5.Current;
						context.EditorScene.SelectObject(selectedObject2.ID, true, MapObjectSelector.selectionColor);
					}
					goto IL_4BF;
				}
			}
			this.mapObjectSelector.Clear();
			context.EditorScene.GenerateShadows(context.EditorSceneViewID);
			context.UpdateScene();
			IL_4BF:
			context.EditorScene.DestroyGenerateShadowsData();
			if (progressContainer != null)
			{
				progressContainer.Progress++;
			}
			return true;
		}

		// Token: 0x0600002C RID: 44 RVA: 0x00003A7C File Offset: 0x00002A7C
		public bool Load(MapEditorMap map, MainForm.Context context, IProgressContainer progressContainer, out bool somethingCreated)
		{
			somethingCreated = false;
			return true;
		}

		// Token: 0x0400000A RID: 10
		private readonly MapObjectSelector mapObjectSelector;

		// Token: 0x0400000B RID: 11
		private readonly MapEditorScene mapEditorScene;

		// Token: 0x0400000C RID: 12
		private readonly bool local = true;

		// Token: 0x0400000D RID: 13
		private readonly Dictionary<int, int> indices = new Dictionary<int, int>();
	}
}
