using System;
using MapEditor.Map.Containers;
using MapEditor.Map.MapObjects;
using Tools.MapObjects;

namespace MapEditor.Scene.Types
{
	// Token: 0x02000270 RID: 624
	internal class MapEditorSceneProjectiles
	{
		// Token: 0x06001D78 RID: 7544 RVA: 0x000BC638 File Offset: 0x000BB638
		private void ProjectileAnimationStep(Projectile projectile, int delta)
		{
			if (projectile.NeedRestartParticle && projectile.LoopTimeStep(delta))
			{
				int id = this.mapSceneObjects.MapObjectIDToEditorSceneObjectID(projectile.ID);
				this.mapEditorScene.EditorScene.RestartParticle(id);
				if (projectile.NeedRestartIdle)
				{
					this.mapEditorScene.EditorScene.PlayObjectAnimation(id, ref this.animationProperties);
				}
			}
		}

		// Token: 0x06001D79 RID: 7545 RVA: 0x000BC698 File Offset: 0x000BB698
		private void OnEditorSceneAfterStep(EditorScene _editorScene)
		{
			if (this.mapEditorScene != null && this.mapSceneObjects != null && this.mapEditorScene.MapSceneParams.ShowProjectileVisObjects)
			{
				int currentTick = Environment.TickCount;
				if (this.lastStepTick > 0)
				{
					int delta = currentTick - this.lastStepTick;
					foreach (IMapObject mapObject in this.mapEditorScene.MapEditorMapObjectContainer.ProjectileContainer.MapObjects.Values)
					{
						Projectile projectile = (Projectile)mapObject;
						this.ProjectileAnimationStep(projectile, delta);
					}
				}
				this.lastStepTick = currentTick;
			}
		}

		// Token: 0x06001D7A RID: 7546 RVA: 0x000BC74C File Offset: 0x000BB74C
		private void OnProjectileDBIDChanged(MapEditorMapObjectContainer container, Projectile projectile, ref string oldValue, ref string newValue)
		{
			if (this.mapSceneObjects != null)
			{
				this.mapSceneObjects.RecreateMapObject(projectile, false);
			}
		}

		// Token: 0x06001D7B RID: 7547 RVA: 0x000BC763 File Offset: 0x000BB763
		public MapEditorSceneProjectiles()
		{
			this.animationProperties.Name = "idle";
			this.animationProperties.Looped = false;
		}

		// Token: 0x06001D7C RID: 7548 RVA: 0x000BC7A0 File Offset: 0x000BB7A0
		public void OnMapObjectAdded(MapObjectContainer mapObjectContainer, IMapObject mapObject)
		{
			Projectile projectile = mapObject as Projectile;
			if (projectile != null)
			{
				projectile.GetSceneNameFromVisObject = this.mapEditorScene.MapSceneParams.ShowProjectileVisObjects;
			}
		}

		// Token: 0x06001D7D RID: 7549 RVA: 0x000BC7D0 File Offset: 0x000BB7D0
		public void Bind(MapEditorScene _mapEditorScene, MapSceneObjects _mapSceneObjects)
		{
			this.mapEditorScene = _mapEditorScene;
			this.mapSceneObjects = _mapSceneObjects;
			if (this.mapEditorScene != null)
			{
				if (this.mapEditorScene.EditorScene != null)
				{
					this.mapEditorScene.EditorScene.AfterStep += this.OnEditorSceneAfterStep;
				}
				ProjectileContainer projectileContainer = this.mapEditorScene.MapEditorMapObjectContainer.ProjectileContainer;
				if (projectileContainer != null)
				{
					projectileContainer.ProjectileDBIDChanged += this.OnProjectileDBIDChanged;
				}
			}
		}

		// Token: 0x06001D7E RID: 7550 RVA: 0x000BC844 File Offset: 0x000BB844
		public void Unbind()
		{
			if (this.mapEditorScene != null)
			{
				if (this.mapEditorScene.EditorScene != null)
				{
					this.mapEditorScene.EditorScene.AfterStep -= this.OnEditorSceneAfterStep;
				}
				ProjectileContainer projectileContainer = this.mapEditorScene.MapEditorMapObjectContainer.ProjectileContainer;
				if (projectileContainer != null)
				{
					projectileContainer.ProjectileDBIDChanged -= this.OnProjectileDBIDChanged;
				}
				this.mapEditorScene = null;
				this.mapSceneObjects = null;
			}
		}

		// Token: 0x040012BB RID: 4795
		private MapEditorScene mapEditorScene;

		// Token: 0x040012BC RID: 4796
		private MapSceneObjects mapSceneObjects;

		// Token: 0x040012BD RID: 4797
		private AnimationProperties animationProperties = new AnimationProperties(1f);

		// Token: 0x040012BE RID: 4798
		private int lastStepTick = -1;
	}
}
