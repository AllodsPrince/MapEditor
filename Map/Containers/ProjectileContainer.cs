using System;
using MapEditor.Map.MapObjectElements;
using MapEditor.Map.MapObjects;

namespace MapEditor.Map.Containers
{
	// Token: 0x02000184 RID: 388
	public class ProjectileContainer : TypedMapObjectContainer
	{
		// Token: 0x14000070 RID: 112
		// (add) Token: 0x0600127A RID: 4730 RVA: 0x00086533 File Offset: 0x00085533
		// (remove) Token: 0x0600127B RID: 4731 RVA: 0x0008654C File Offset: 0x0008554C
		public event ProjectileContainer.ProjectileFieldChangedEvent<string> ProjectileDBIDChanged;

		// Token: 0x14000071 RID: 113
		// (add) Token: 0x0600127C RID: 4732 RVA: 0x00086565 File Offset: 0x00085565
		// (remove) Token: 0x0600127D RID: 4733 RVA: 0x0008657E File Offset: 0x0008557E
		public event ProjectileContainer.ProjectileFieldChangedEvent<string> ProjectileScriptIDChanged;

		// Token: 0x14000072 RID: 114
		// (add) Token: 0x0600127E RID: 4734 RVA: 0x00086597 File Offset: 0x00085597
		// (remove) Token: 0x0600127F RID: 4735 RVA: 0x000865B0 File Offset: 0x000855B0
		public event ProjectileContainer.ProjectileFieldChangedEvent<double> ProjectileScanRadiusChanged;

		// Token: 0x14000073 RID: 115
		// (add) Token: 0x06001280 RID: 4736 RVA: 0x000865C9 File Offset: 0x000855C9
		// (remove) Token: 0x06001281 RID: 4737 RVA: 0x000865E2 File Offset: 0x000855E2
		public event ProjectileContainer.ProjectileFieldChangedEvent<SpawnTimeAbstract> ProjectileSpawnTimeChanged;

		// Token: 0x06001282 RID: 4738 RVA: 0x000865FC File Offset: 0x000855FC
		private void OnProjectileDBIDChanged(Projectile projectile, ref string oldValue, ref string newValue)
		{
			if (this.mapEditorMapObjectContainer != null && base.Contains(projectile))
			{
				if (this.ProjectileDBIDChanged != null)
				{
					this.ProjectileDBIDChanged(this.mapEditorMapObjectContainer, projectile, ref oldValue, ref newValue);
				}
				if (!projectile.Temporary)
				{
					this.mapEditorMapObjectContainer.InvokeModified();
				}
			}
		}

		// Token: 0x06001283 RID: 4739 RVA: 0x0008664C File Offset: 0x0008564C
		private void OnProjectileScanRadiusChanged(Projectile projectile, ref double oldValue, ref double newValue)
		{
			if (this.mapEditorMapObjectContainer != null && base.Contains(projectile))
			{
				if (this.ProjectileScanRadiusChanged != null)
				{
					this.ProjectileScanRadiusChanged(this.mapEditorMapObjectContainer, projectile, ref oldValue, ref newValue);
				}
				if (!projectile.Temporary)
				{
					this.mapEditorMapObjectContainer.InvokeModified();
				}
			}
		}

		// Token: 0x06001284 RID: 4740 RVA: 0x0008669C File Offset: 0x0008569C
		private void OnProjectileScriptIDChanged(Projectile projectile, ref string oldValue, ref string newValue)
		{
			if (this.mapEditorMapObjectContainer != null && base.Contains(projectile))
			{
				if (this.ProjectileScriptIDChanged != null)
				{
					this.ProjectileScriptIDChanged(this.mapEditorMapObjectContainer, projectile, ref oldValue, ref newValue);
				}
				if (!projectile.Temporary)
				{
					this.mapEditorMapObjectContainer.InvokeModified();
				}
			}
		}

		// Token: 0x06001285 RID: 4741 RVA: 0x000866EC File Offset: 0x000856EC
		private void OnProjectileSpawnTimeChanged(Projectile projectile, ref SpawnTimeAbstract oldValue, ref SpawnTimeAbstract newValue)
		{
			if (this.mapEditorMapObjectContainer != null && base.Contains(projectile))
			{
				if (this.ProjectileSpawnTimeChanged != null)
				{
					this.ProjectileSpawnTimeChanged(this.mapEditorMapObjectContainer, projectile, ref oldValue, ref newValue);
				}
				if (!projectile.Temporary)
				{
					this.mapEditorMapObjectContainer.InvokeModified();
				}
			}
		}

		// Token: 0x06001286 RID: 4742 RVA: 0x0008673C File Offset: 0x0008573C
		public ProjectileContainer(MapEditorMapObjectContainer _mapEditorMapObjectContainer) : base(MapObjectFactory.Type.Projectile, true)
		{
			this.mapEditorMapObjectContainer = _mapEditorMapObjectContainer;
			if (this.mapEditorMapObjectContainer != null)
			{
				Projectile.ProjectileDBIDChanged += this.OnProjectileDBIDChanged;
				Projectile.ScanRadiusChanged += this.OnProjectileScanRadiusChanged;
				Projectile.ScriptIDChanged += this.OnProjectileScriptIDChanged;
				Projectile.SpawnTimeChanged += this.OnProjectileSpawnTimeChanged;
				base.Destroyed += this.OnDestroyed;
			}
		}

		// Token: 0x06001287 RID: 4743 RVA: 0x000867BC File Offset: 0x000857BC
		public void OnDestroyed(TypedMapObjectContainer _typedMapObjectContainer)
		{
			if (this.mapEditorMapObjectContainer != null)
			{
				Projectile.ProjectileDBIDChanged -= this.OnProjectileDBIDChanged;
				Projectile.ScanRadiusChanged -= this.OnProjectileScanRadiusChanged;
				Projectile.ScriptIDChanged -= this.OnProjectileScriptIDChanged;
				Projectile.SpawnTimeChanged -= this.OnProjectileSpawnTimeChanged;
				base.Destroyed -= this.OnDestroyed;
				this.mapEditorMapObjectContainer = null;
			}
		}

		// Token: 0x04000D31 RID: 3377
		private MapEditorMapObjectContainer mapEditorMapObjectContainer;

		// Token: 0x02000185 RID: 389
		// (Invoke) Token: 0x06001289 RID: 4745
		public delegate void ProjectileFieldChangedEvent<T>(MapEditorMapObjectContainer _mapEditorMapObjectContainer, Projectile projectile, ref T oldValue, ref T newValue);
	}
}
