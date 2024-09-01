using System;
using MapEditor.Map.Containers;
using MapEditor.Map.MapObjectElements;
using Operations;

namespace MapEditor.Map.MapObjects
{
	// Token: 0x0200024F RID: 591
	public class ProjectileOperationContainer
	{
		// Token: 0x06001C31 RID: 7217 RVA: 0x000B600C File Offset: 0x000B500C
		private void OnProjectileDBIDChanged(MapEditorMapObjectContainer _mapEditorMapObjectContainer, Projectile projectile, ref string oldValue, ref string newValue)
		{
			if (this.mapEditorMapObjectContainer != null && projectile != null && this.operationContainer != null && !projectile.Temporary)
			{
				ChangeProjectileDBIDOperation operation = new ChangeProjectileDBIDOperation(projectile, ref oldValue, ref newValue);
				this.operationContainer.Add(operation);
			}
		}

		// Token: 0x06001C32 RID: 7218 RVA: 0x000B604C File Offset: 0x000B504C
		private void OnProjectileScriptIDChanged(MapEditorMapObjectContainer _mapEditorMapObjectContainer, Projectile projectile, ref string oldValue, ref string newValue)
		{
			if (this.mapEditorMapObjectContainer != null && projectile != null && this.operationContainer != null && !projectile.Temporary)
			{
				ChangeProjectileScriptIDOperation operation = new ChangeProjectileScriptIDOperation(projectile, ref oldValue, ref newValue);
				this.operationContainer.Add(operation);
			}
		}

		// Token: 0x06001C33 RID: 7219 RVA: 0x000B608C File Offset: 0x000B508C
		private void OnProjectileScanRadiusChanged(MapEditorMapObjectContainer _mapEditorMapObjectContainer, Projectile projectile, ref double oldValue, ref double newValue)
		{
			if (this.mapEditorMapObjectContainer != null && projectile != null && this.operationContainer != null && !projectile.Temporary)
			{
				ChangeProjectileScanRadiusOperation operation = new ChangeProjectileScanRadiusOperation(projectile, ref oldValue, ref newValue);
				this.operationContainer.Add(operation);
			}
		}

		// Token: 0x06001C34 RID: 7220 RVA: 0x000B60CC File Offset: 0x000B50CC
		private void OnProjectileSpawnTimeChanged(MapEditorMapObjectContainer _mapEditorMapObjectContainer, Projectile projectile, ref SpawnTimeAbstract oldValue, ref SpawnTimeAbstract newValue)
		{
			if (this.mapEditorMapObjectContainer != null && projectile != null && this.operationContainer != null && !projectile.Temporary)
			{
				ChangeProjectileSpawnTimeOperation operation = new ChangeProjectileSpawnTimeOperation(projectile, ref oldValue, ref newValue);
				this.operationContainer.Add(operation);
			}
		}

		// Token: 0x06001C35 RID: 7221 RVA: 0x000B610A File Offset: 0x000B510A
		public ProjectileOperationContainer(OperationContainer _operationContainer)
		{
			this.operationContainer = _operationContainer;
		}

		// Token: 0x06001C36 RID: 7222 RVA: 0x000B6119 File Offset: 0x000B5119
		public void Destroy()
		{
			if (this.operationContainer != null)
			{
				this.operationContainer = null;
			}
		}

		// Token: 0x06001C37 RID: 7223 RVA: 0x000B612C File Offset: 0x000B512C
		public void Bind(MapEditorMapObjectContainer _mapEditorMapObjectContainer)
		{
			this.Unbind();
			this.mapEditorMapObjectContainer = _mapEditorMapObjectContainer;
			if (this.mapEditorMapObjectContainer != null)
			{
				ProjectileContainer projectileContainer = this.mapEditorMapObjectContainer.ProjectileContainer;
				if (projectileContainer != null)
				{
					projectileContainer.ProjectileDBIDChanged += this.OnProjectileDBIDChanged;
					projectileContainer.ProjectileScriptIDChanged += this.OnProjectileScriptIDChanged;
					projectileContainer.ProjectileScanRadiusChanged += this.OnProjectileScanRadiusChanged;
					projectileContainer.ProjectileSpawnTimeChanged += this.OnProjectileSpawnTimeChanged;
				}
			}
		}

		// Token: 0x06001C38 RID: 7224 RVA: 0x000B61A8 File Offset: 0x000B51A8
		public void Unbind()
		{
			if (this.mapEditorMapObjectContainer != null)
			{
				ProjectileContainer projectileContainer = this.mapEditorMapObjectContainer.ProjectileContainer;
				if (projectileContainer != null)
				{
					projectileContainer.ProjectileDBIDChanged -= this.OnProjectileDBIDChanged;
					projectileContainer.ProjectileScriptIDChanged -= this.OnProjectileScriptIDChanged;
					projectileContainer.ProjectileScanRadiusChanged -= this.OnProjectileScanRadiusChanged;
					projectileContainer.ProjectileSpawnTimeChanged -= this.OnProjectileSpawnTimeChanged;
				}
				this.mapEditorMapObjectContainer = null;
			}
		}

		// Token: 0x04001223 RID: 4643
		private MapEditorMapObjectContainer mapEditorMapObjectContainer;

		// Token: 0x04001224 RID: 4644
		private OperationContainer operationContainer;
	}
}
