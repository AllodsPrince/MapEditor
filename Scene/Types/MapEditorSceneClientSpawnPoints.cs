using System;
using Db;
using MapEditor.Map.Containers;
using MapEditor.Map.MapObjects;

namespace MapEditor.Scene.Types
{
	// Token: 0x020001BC RID: 444
	internal class MapEditorSceneClientSpawnPoints
	{
		// Token: 0x06001577 RID: 5495 RVA: 0x0009C3D0 File Offset: 0x0009B3D0
		private void OnClientSpawnPointVisObjectChanged(MapEditorMapObjectContainer mapEditorMapObjectContainer, ClientSpawnPoint clientSpawnPoint, ref string oldValue, ref string newValue)
		{
			this.mapSceneObjects.RecreateMapObject(clientSpawnPoint, false);
			string animationName = ClientSpawnPoint.GetFixedIdleAnimationName(clientSpawnPoint, this.mainDb);
			if (!string.IsNullOrEmpty(animationName))
			{
				AnimationProperties animationProperties = new AnimationProperties(1f);
				animationProperties.Name = animationName;
				animationProperties.LowerName = animationName;
				animationProperties.Speed = 1f;
				animationProperties.Lower = true;
				animationProperties.Upper = true;
				animationProperties.Looped = true;
				int editorSceneObjectID = this.mapSceneObjects.MapObjectIDToEditorSceneObjectID(clientSpawnPoint.ID);
				if (editorSceneObjectID != -1)
				{
					this.mapEditorScene.EditorScene.PlayObjectAnimation(editorSceneObjectID, ref animationProperties);
				}
			}
		}

		// Token: 0x06001578 RID: 5496 RVA: 0x0009C470 File Offset: 0x0009B470
		public void Bind(MapEditorScene _mapEditorScene, MapSceneObjects _mapSceneObjects)
		{
			this.mapEditorScene = _mapEditorScene;
			this.mapSceneObjects = _mapSceneObjects;
			this.mainDb = IDatabase.GetMainDatabase();
			if (this.mapEditorScene != null && this.mapSceneObjects != null)
			{
				ClientSpawnPointContainer clientSpawnPointContainer = this.mapEditorScene.MapEditorMapObjectContainer.ClientSpawnPointContainer;
				if (clientSpawnPointContainer != null)
				{
					clientSpawnPointContainer.ClientSpawnPointVisObjectChanged += this.OnClientSpawnPointVisObjectChanged;
				}
			}
		}

		// Token: 0x06001579 RID: 5497 RVA: 0x0009C4CC File Offset: 0x0009B4CC
		public void Unbind()
		{
			if (this.mapEditorScene != null)
			{
				ClientSpawnPointContainer clientSpawnPointContainer = this.mapEditorScene.MapEditorMapObjectContainer.ClientSpawnPointContainer;
				if (clientSpawnPointContainer != null)
				{
					clientSpawnPointContainer.ClientSpawnPointVisObjectChanged -= this.OnClientSpawnPointVisObjectChanged;
				}
				this.mapEditorScene = null;
			}
			this.mapSceneObjects = null;
			this.mainDb = null;
		}

		// Token: 0x04000F21 RID: 3873
		private MapEditorScene mapEditorScene;

		// Token: 0x04000F22 RID: 3874
		private MapSceneObjects mapSceneObjects;

		// Token: 0x04000F23 RID: 3875
		private IDatabase mainDb;
	}
}
