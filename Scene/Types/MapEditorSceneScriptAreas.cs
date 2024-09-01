using System;
using System.Collections.Generic;
using System.Drawing;
using MapEditor.Map.Containers;
using MapEditor.Map.MapObjects;
using Tools.MapObjects;

namespace MapEditor.Scene.Types
{
	// Token: 0x020001EB RID: 491
	internal class MapEditorSceneScriptAreas
	{
		// Token: 0x060018B2 RID: 6322 RVA: 0x000A4648 File Offset: 0x000A3648
		private int CreateUserGeometry(IMapObject mapObject, int userGeometryID)
		{
			if (this.mapEditorScene.MapSceneParams.ShowScriptAreaUserGeometry || mapObject.Select)
			{
				ScriptArea scriptArea = mapObject as ScriptArea;
				if (scriptArea != null && scriptArea.ScriptAreaData != null && scriptArea.ScriptAreaData.ScriptAreaType == ScriptAreaType.Cylinder)
				{
					CylinderScriptAreaData cylinderScriptAreaData = scriptArea.ScriptAreaData as CylinderScriptAreaData;
					if (cylinderScriptAreaData != null)
					{
						Position position = mapObject.Position;
						return this.mapEditorScene.EditorScene.CreateUserGeometry_Cylinder(userGeometryID, ref position, 0f, cylinderScriptAreaData.Radius * 2.0, cylinderScriptAreaData.Radius * 2.0, cylinderScriptAreaData.Halfheight, MapEditorSceneScriptAreas.userGeometryColor, mapObject.Select);
					}
				}
			}
			return -1;
		}

		// Token: 0x060018B3 RID: 6323 RVA: 0x000A46F4 File Offset: 0x000A36F4
		private void DestroyUserGeometry(IMapObject mapObject, int userGeometryID)
		{
			ScriptArea scriptArea = mapObject as ScriptArea;
			if (scriptArea != null && userGeometryID != -1)
			{
				this.mapEditorScene.EditorScene.DeleteUserGeometry(userGeometryID);
			}
		}

		// Token: 0x060018B4 RID: 6324 RVA: 0x000A4720 File Offset: 0x000A3720
		private void CreateUserGeometries()
		{
			foreach (KeyValuePair<int, IMapObject> keyValuePair in this.mapEditorScene.MapEditorMapObjectContainer.MapObjects)
			{
				IMapObject mapObject = keyValuePair.Value;
				int userGeometryID;
				if (mapObject != null && mapObject.Type.Type == MapObjectFactory.Type.ScriptArea && !this.userGeometryIDMap.TryGetValue(mapObject.ID, out userGeometryID))
				{
					userGeometryID = this.CreateUserGeometry(mapObject, -1);
					if (userGeometryID != -1)
					{
						this.userGeometryIDMap[mapObject.ID] = userGeometryID;
					}
				}
			}
		}

		// Token: 0x060018B5 RID: 6325 RVA: 0x000A47CC File Offset: 0x000A37CC
		private void DestroyUserGeometries()
		{
			foreach (KeyValuePair<int, IMapObject> keyValuePair in this.mapEditorScene.MapEditorMapObjectContainer.MapObjects)
			{
				IMapObject mapObject = keyValuePair.Value;
				int userGeometryID;
				if (mapObject != null && mapObject.Type.Type == MapObjectFactory.Type.ScriptArea && !mapObject.Select && this.userGeometryIDMap.TryGetValue(mapObject.ID, out userGeometryID))
				{
					this.DestroyUserGeometry(mapObject, userGeometryID);
					this.userGeometryIDMap.Remove(mapObject.ID);
				}
			}
		}

		// Token: 0x060018B6 RID: 6326 RVA: 0x000A487C File Offset: 0x000A387C
		private void OnShowUserGeometryChanged(MapSceneParams mapSceneParams, ref bool oldValue, ref bool newValue)
		{
			if (this.mapEditorScene.MapSceneParams != null)
			{
				if (this.mapEditorScene.MapSceneParams.ShowScriptAreaUserGeometry)
				{
					this.CreateUserGeometries();
					return;
				}
				this.DestroyUserGeometries();
			}
		}

		// Token: 0x060018B7 RID: 6327 RVA: 0x000A48AC File Offset: 0x000A38AC
		public void OnMapObjectAdded(MapObjectContainer mapObjectContainer, IMapObject mapObject)
		{
			int userGeometryID = this.CreateUserGeometry(mapObject, -1);
			if (userGeometryID != -1)
			{
				this.userGeometryIDMap[mapObject.ID] = userGeometryID;
			}
		}

		// Token: 0x060018B8 RID: 6328 RVA: 0x000A48D8 File Offset: 0x000A38D8
		public void OnMapObjectRemoved(MapObjectContainer mapObjectContainer, IMapObject mapObject)
		{
			int userGeometryID;
			if (this.userGeometryIDMap.TryGetValue(mapObject.ID, out userGeometryID) && userGeometryID != -1)
			{
				this.DestroyUserGeometry(mapObject, userGeometryID);
				this.userGeometryIDMap.Remove(mapObject.ID);
			}
		}

		// Token: 0x060018B9 RID: 6329 RVA: 0x000A4918 File Offset: 0x000A3918
		public void OnPositionChanged(MapObjectContainer mapObjectContainer, IMapObject mapObject, ref Position oldValue, ref Position newValue)
		{
			int userGeometryID;
			if (this.userGeometryIDMap.TryGetValue(mapObject.ID, out userGeometryID) && userGeometryID != -1)
			{
				this.CreateUserGeometry(mapObject, userGeometryID);
			}
		}

		// Token: 0x060018BA RID: 6330 RVA: 0x000A4948 File Offset: 0x000A3948
		public void OnRotationChanged(MapObjectContainer mapObjectContainer, IMapObject mapObject, ref Rotation oldValue, ref Rotation newValue)
		{
			ScriptArea scriptArea = mapObject as ScriptArea;
			if (scriptArea != null && scriptArea.ScriptAreaData != null)
			{
				ScriptAreaType scriptAreaType = scriptArea.ScriptAreaData.ScriptAreaType;
			}
		}

		// Token: 0x060018BB RID: 6331 RVA: 0x000A4978 File Offset: 0x000A3978
		public void OnSelectChanged(MapObjectContainer mapObjectContainer, IMapObject mapObject, ref bool oldValue, ref bool newValue)
		{
			int userGeometryID2;
			if (newValue || this.mapEditorScene.MapSceneParams.ShowScriptAreaUserGeometry)
			{
				int userGeometryID;
				if (this.userGeometryIDMap.TryGetValue(mapObject.ID, out userGeometryID))
				{
					this.CreateUserGeometry(mapObject, userGeometryID);
					return;
				}
				if (newValue)
				{
					userGeometryID = this.CreateUserGeometry(mapObject, -1);
					if (userGeometryID != -1)
					{
						this.userGeometryIDMap[mapObject.ID] = userGeometryID;
						return;
					}
				}
			}
			else if (this.userGeometryIDMap.TryGetValue(mapObject.ID, out userGeometryID2) && userGeometryID2 != -1)
			{
				this.DestroyUserGeometry(mapObject, userGeometryID2);
				this.userGeometryIDMap.Remove(mapObject.ID);
			}
		}

		// Token: 0x060018BC RID: 6332 RVA: 0x000A4A14 File Offset: 0x000A3A14
		private void OnScriptAreaDataChanged(MapEditorMapObjectContainer mapEditorMapObjectContainer, ScriptArea scriptArea, ref ScriptAreaData oldValue, ref ScriptAreaData newValue)
		{
			int userGeometryID3;
			if (newValue == null)
			{
				int userGeometryID;
				if (this.userGeometryIDMap.TryGetValue(scriptArea.ID, out userGeometryID) && userGeometryID != -1)
				{
					this.DestroyUserGeometry(scriptArea, userGeometryID);
					this.userGeometryIDMap.Remove(scriptArea.ID);
					return;
				}
			}
			else if (newValue.ScriptAreaType == ScriptAreaType.Cylinder)
			{
				int userGeometryID2;
				if (this.userGeometryIDMap.TryGetValue(scriptArea.ID, out userGeometryID2) && userGeometryID2 != -1)
				{
					this.DestroyUserGeometry(scriptArea, userGeometryID2);
					this.userGeometryIDMap.Remove(scriptArea.ID);
				}
				userGeometryID2 = this.CreateUserGeometry(scriptArea, -1);
				if (userGeometryID2 != -1)
				{
					this.userGeometryIDMap[scriptArea.ID] = userGeometryID2;
					return;
				}
			}
			else if (this.userGeometryIDMap.TryGetValue(scriptArea.ID, out userGeometryID3) && userGeometryID3 != -1)
			{
				this.DestroyUserGeometry(scriptArea, userGeometryID3);
				this.userGeometryIDMap.Remove(scriptArea.ID);
			}
		}

		// Token: 0x060018BD RID: 6333 RVA: 0x000A4AF4 File Offset: 0x000A3AF4
		private void OnScriptAreaDataFieldChanged(MapEditorMapObjectContainer mapEditorMapObjectContainer, ScriptArea scriptArea)
		{
			int userGeometryID;
			if (scriptArea != null && scriptArea.ScriptAreaData.ScriptAreaType == ScriptAreaType.Cylinder && this.userGeometryIDMap.TryGetValue(scriptArea.ID, out userGeometryID) && userGeometryID != -1)
			{
				this.CreateUserGeometry(scriptArea, userGeometryID);
			}
		}

		// Token: 0x060018BE RID: 6334 RVA: 0x000A4B34 File Offset: 0x000A3B34
		public void Bind(MapEditorScene _mapEditorScene)
		{
			this.mapEditorScene = _mapEditorScene;
			if (this.mapEditorScene != null)
			{
				this.mapEditorScene.MapSceneParams.ShowScriptAreaUserGeometryChanged += this.OnShowUserGeometryChanged;
				ScriptAreaContainer scriptAreaContainer = this.mapEditorScene.MapEditorMapObjectContainer.ScriptAreaContainer;
				if (scriptAreaContainer != null)
				{
					scriptAreaContainer.ScriptAreaTypeChanged += this.OnScriptAreaDataChanged;
					scriptAreaContainer.ScriptAreaDataChanged += this.OnScriptAreaDataChanged;
					scriptAreaContainer.ScriptAreaDataFieldChanged += this.OnScriptAreaDataFieldChanged;
				}
				this.CreateUserGeometries();
			}
		}

		// Token: 0x060018BF RID: 6335 RVA: 0x000A4BBC File Offset: 0x000A3BBC
		public void Unbind()
		{
			if (this.mapEditorScene != null)
			{
				this.DestroyUserGeometries();
				this.mapEditorScene.MapSceneParams.ShowScriptAreaUserGeometryChanged -= this.OnShowUserGeometryChanged;
				ScriptAreaContainer scriptAreaContainer = this.mapEditorScene.MapEditorMapObjectContainer.ScriptAreaContainer;
				if (scriptAreaContainer != null)
				{
					scriptAreaContainer.ScriptAreaTypeChanged -= this.OnScriptAreaDataChanged;
					scriptAreaContainer.ScriptAreaDataChanged -= this.OnScriptAreaDataChanged;
					scriptAreaContainer.ScriptAreaDataFieldChanged -= this.OnScriptAreaDataFieldChanged;
				}
				this.mapEditorScene = null;
			}
		}

		// Token: 0x0400100F RID: 4111
		private static readonly Color userGeometryColor = Color.FromArgb(MapEditorScene.DefaultTransparentColorAlpha, ScriptArea.InterfaceColor);

		// Token: 0x04001010 RID: 4112
		private readonly Dictionary<int, int> userGeometryIDMap = new Dictionary<int, int>();

		// Token: 0x04001011 RID: 4113
		private MapEditorScene mapEditorScene;
	}
}
