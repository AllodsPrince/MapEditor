using System;
using MapEditor.Map.MapObjectElements;
using Tools.MapObjects;

namespace MapEditor.Map.MapObjects
{
	// Token: 0x02000248 RID: 584
	public class ProjectilePack : SerializableMapObjectPack
	{
		// Token: 0x1700067B RID: 1659
		// (get) Token: 0x06001BCE RID: 7118 RVA: 0x000B5697 File Offset: 0x000B4697
		// (set) Token: 0x06001BCF RID: 7119 RVA: 0x000B569F File Offset: 0x000B469F
		public string ProjectileDBID
		{
			get
			{
				return this.projectileDBID;
			}
			set
			{
				this.projectileDBID = value;
			}
		}

		// Token: 0x1700067C RID: 1660
		// (get) Token: 0x06001BD0 RID: 7120 RVA: 0x000B56A8 File Offset: 0x000B46A8
		// (set) Token: 0x06001BD1 RID: 7121 RVA: 0x000B56B0 File Offset: 0x000B46B0
		public string ScriptID
		{
			get
			{
				return this.scriptID;
			}
			set
			{
				this.scriptID = value;
			}
		}

		// Token: 0x1700067D RID: 1661
		// (get) Token: 0x06001BD2 RID: 7122 RVA: 0x000B56B9 File Offset: 0x000B46B9
		// (set) Token: 0x06001BD3 RID: 7123 RVA: 0x000B56C1 File Offset: 0x000B46C1
		public double ScanRadius
		{
			get
			{
				return this.scanRadius;
			}
			set
			{
				this.scanRadius = value;
			}
		}

		// Token: 0x1700067E RID: 1662
		// (get) Token: 0x06001BD4 RID: 7124 RVA: 0x000B56CA File Offset: 0x000B46CA
		// (set) Token: 0x06001BD5 RID: 7125 RVA: 0x000B56D2 File Offset: 0x000B46D2
		public SpawnTimeAbstract SpawnTime
		{
			get
			{
				return this.spawnTime;
			}
			set
			{
				this.spawnTime = value;
			}
		}

		// Token: 0x1700067F RID: 1663
		// (get) Token: 0x06001BD6 RID: 7126 RVA: 0x000B56DB File Offset: 0x000B46DB
		// (set) Token: 0x06001BD7 RID: 7127 RVA: 0x000B56E3 File Offset: 0x000B46E3
		public bool GetSceneNameFromVisObject
		{
			get
			{
				return this.getSceneNameFromVisObject;
			}
			set
			{
				this.getSceneNameFromVisObject = value;
			}
		}

		// Token: 0x06001BD8 RID: 7128 RVA: 0x000B56EC File Offset: 0x000B46EC
		public override void Pack(IMapObject mapObject)
		{
			base.Pack(mapObject);
			Projectile projectile = mapObject as Projectile;
			if (projectile != null)
			{
				this.projectileDBID = projectile.ProjectileDBID;
				this.scriptID = projectile.ScriptID;
				this.scanRadius = projectile.ScanRadius;
				this.spawnTime = projectile.SpawnTime;
				this.getSceneNameFromVisObject = projectile.GetSceneNameFromVisObject;
			}
		}

		// Token: 0x06001BD9 RID: 7129 RVA: 0x000B5748 File Offset: 0x000B4748
		public override void Unpack(IMapObject mapObject)
		{
			base.Unpack(mapObject);
			Projectile projectile = mapObject as Projectile;
			if (projectile != null)
			{
				projectile.ProjectileDBID = this.projectileDBID;
				projectile.ScriptID = this.scriptID;
				projectile.ScanRadius = this.scanRadius;
				projectile.SpawnTime = this.spawnTime;
				projectile.GetSceneNameFromVisObject = this.getSceneNameFromVisObject;
			}
		}

		// Token: 0x040011FC RID: 4604
		private string projectileDBID;

		// Token: 0x040011FD RID: 4605
		private string scriptID;

		// Token: 0x040011FE RID: 4606
		private double scanRadius;

		// Token: 0x040011FF RID: 4607
		private SpawnTimeAbstract spawnTime;

		// Token: 0x04001200 RID: 4608
		private bool getSceneNameFromVisObject;
	}
}
