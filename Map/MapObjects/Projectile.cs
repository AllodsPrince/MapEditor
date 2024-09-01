using System;
using System.ComponentModel;
using System.Drawing;
using Db;
using MapEditor.Map.MapObjectElements;
using MapEditor.Resources.Strings;
using Tools.MapObjects;

namespace MapEditor.Map.MapObjects
{
	// Token: 0x02000249 RID: 585
	public class Projectile : MapObject, IMapObjectInterfaceExtention, SpawnTimeOwner
	{
		// Token: 0x140000C8 RID: 200
		// (add) Token: 0x06001BDB RID: 7131 RVA: 0x000B57AA File Offset: 0x000B47AA
		// (remove) Token: 0x06001BDC RID: 7132 RVA: 0x000B57C1 File Offset: 0x000B47C1
		public static event Projectile.ProjectileFieldChangedEvent<string> ProjectileDBIDChanged;

		// Token: 0x140000C9 RID: 201
		// (add) Token: 0x06001BDD RID: 7133 RVA: 0x000B57D8 File Offset: 0x000B47D8
		// (remove) Token: 0x06001BDE RID: 7134 RVA: 0x000B57EF File Offset: 0x000B47EF
		public static event Projectile.ProjectileFieldChangedEvent<SpawnTimeAbstract> SpawnTimeChanged;

		// Token: 0x140000CA RID: 202
		// (add) Token: 0x06001BDF RID: 7135 RVA: 0x000B5806 File Offset: 0x000B4806
		// (remove) Token: 0x06001BE0 RID: 7136 RVA: 0x000B581D File Offset: 0x000B481D
		public static event Projectile.ProjectileFieldChangedEvent<double> ScanRadiusChanged;

		// Token: 0x140000CB RID: 203
		// (add) Token: 0x06001BE1 RID: 7137 RVA: 0x000B5834 File Offset: 0x000B4834
		// (remove) Token: 0x06001BE2 RID: 7138 RVA: 0x000B584B File Offset: 0x000B484B
		public static event Projectile.ProjectileFieldChangedEvent<string> ScriptIDChanged;

		// Token: 0x06001BE3 RID: 7139 RVA: 0x000B5864 File Offset: 0x000B4864
		private void SetVisObject()
		{
			this.needRestartParticle = false;
			this.needRestartIdle = false;
			this.fullLoopTime = 0;
			this.restLoopTime = 0;
			if (!string.IsNullOrEmpty(this.projectileDBID))
			{
				DBID dbid = Projectile.mainDb.GetDBIDByName(this.projectileDBID);
				if (!DBID.IsNullOrEmpty(dbid))
				{
					IObjMan objMan = Projectile.mainDb.GetManipulator(dbid);
					DBID visObjDBID;
					objMan.GetValue("visObject", out visObjDBID);
					if (!DBID.IsNullOrEmpty(visObjDBID))
					{
						objMan = Projectile.mainDb.GetManipulator(visObjDBID);
						if (objMan != null)
						{
							DBID particle;
							objMan.GetValue("particle", out particle);
							if (!DBID.IsNullOrEmpty(particle))
							{
								IObjMan particleObjMan = Projectile.mainDb.GetManipulator(particle);
								if (particleObjMan != null)
								{
									bool looped;
									particleObjMan.GetValue("looped", out looped);
									this.needRestartParticle = !looped;
									if (this.needRestartParticle)
									{
										int loopFrame;
										particleObjMan.GetValue("loopFrame", out loopFrame);
										this.fullLoopTime = 1000 * loopFrame / 30;
										this.restLoopTime = this.fullLoopTime;
									}
								}
							}
							if (this.needRestartParticle)
							{
								objMan = objMan.CreateManipulator("states");
								int cnt = objMan.GetArraySize();
								for (int index = 0; index < cnt; index++)
								{
									objMan.GetValue("animation", out dbid);
									if (!DBID.IsNullOrEmpty(dbid))
									{
										IObjMan animMan = Projectile.mainDb.GetManipulator(dbid);
										if (animMan != null)
										{
											string animName;
											animMan.GetValue("scriptName", out animName);
											if (animName == "idle")
											{
												this.needRestartIdle = true;
												return;
											}
										}
									}
								}
							}
						}
					}
				}
			}
		}

		// Token: 0x06001BE4 RID: 7140 RVA: 0x000B59D8 File Offset: 0x000B49D8
		public Projectile(int _id, MapObjectType _type, ICollisionMap _collisionMap) : base(_id, _type, _collisionMap)
		{
		}

		// Token: 0x17000680 RID: 1664
		// (get) Token: 0x06001BE5 RID: 7141 RVA: 0x000B59EE File Offset: 0x000B49EE
		public static string DefaultStatObject
		{
			get
			{
				return Projectile.defaultStatObject;
			}
		}

		// Token: 0x17000681 RID: 1665
		// (get) Token: 0x06001BE6 RID: 7142 RVA: 0x000B59F5 File Offset: 0x000B49F5
		public static Color InterfaceColor
		{
			get
			{
				return Projectile.interfaceColor;
			}
		}

		// Token: 0x17000682 RID: 1666
		// (get) Token: 0x06001BE7 RID: 7143 RVA: 0x000B59FC File Offset: 0x000B49FC
		public static string InterfaceSingleObjectTypeName
		{
			get
			{
				return Projectile.interfaceSingleObjectTypeName;
			}
		}

		// Token: 0x17000683 RID: 1667
		// (get) Token: 0x06001BE8 RID: 7144 RVA: 0x000B5A03 File Offset: 0x000B4A03
		public static string InterfaceSeveralObjectsTypeName
		{
			get
			{
				return Projectile.interfaceSeveralObjectsTypeName;
			}
		}

		// Token: 0x17000684 RID: 1668
		// (get) Token: 0x06001BE9 RID: 7145 RVA: 0x000B5A0A File Offset: 0x000B4A0A
		public static string DefaultVisObject
		{
			get
			{
				return Projectile.defaultVisObject;
			}
		}

		// Token: 0x17000685 RID: 1669
		// (get) Token: 0x06001BEA RID: 7146 RVA: 0x000B5A11 File Offset: 0x000B4A11
		// (set) Token: 0x06001BEB RID: 7147 RVA: 0x000B5A1C File Offset: 0x000B4A1C
		[Browsable(true)]
		[Category("Projectile")]
		public string ProjectileDBID
		{
			get
			{
				return this.projectileDBID;
			}
			set
			{
				if (this.projectileDBID != value)
				{
					string oldValue = this.projectileDBID;
					if (base.InvokeChanging(null))
					{
						this.projectileDBID = value;
						this.SetVisObject();
						this.CheckSpawnTimeRareId(this.spawnTime);
						base.InvokeChanged();
						if (base.Active && Projectile.ProjectileDBIDChanged != null)
						{
							Projectile.ProjectileDBIDChanged(this, ref oldValue, ref this.projectileDBID);
						}
					}
				}
			}
		}

		// Token: 0x17000686 RID: 1670
		// (get) Token: 0x06001BEC RID: 7148 RVA: 0x000B5A88 File Offset: 0x000B4A88
		// (set) Token: 0x06001BED RID: 7149 RVA: 0x000B5A90 File Offset: 0x000B4A90
		[Category("Projectile")]
		[Browsable(true)]
		[TypeConverter(typeof(SpawnTimeConverter))]
		[DisplayName("SpawnTime")]
		[RefreshProperties(RefreshProperties.All)]
		public SpawnTimeAbstract SpawnTime
		{
			get
			{
				return this.spawnTime;
			}
			set
			{
				if (this.spawnTime != value && base.InvokeChanging(null))
				{
					SpawnTimeAbstract oldSpawnTime = this.spawnTime;
					this.spawnTime = value;
					base.InvokeChanged();
					if (base.Active && Projectile.SpawnTimeChanged != null)
					{
						Projectile.SpawnTimeChanged(this, ref oldSpawnTime, ref this.spawnTime);
					}
				}
			}
		}

		// Token: 0x17000687 RID: 1671
		// (get) Token: 0x06001BEE RID: 7150 RVA: 0x000B5AE5 File Offset: 0x000B4AE5
		// (set) Token: 0x06001BEF RID: 7151 RVA: 0x000B5AF0 File Offset: 0x000B4AF0
		[Browsable(true)]
		[DisplayName("ScriptID")]
		[Category("Projectile")]
		public string ScriptID
		{
			get
			{
				return this.scriptID;
			}
			set
			{
				value = Str.Trim(value);
				if (this.scriptID != value && base.InvokeChanging(null))
				{
					string oldScriptID = this.scriptID;
					this.scriptID = value;
					base.InvokeChanged();
					if (base.Active && Projectile.ScriptIDChanged != null)
					{
						Projectile.ScriptIDChanged(this, ref oldScriptID, ref this.scriptID);
					}
				}
			}
		}

		// Token: 0x17000688 RID: 1672
		// (get) Token: 0x06001BF0 RID: 7152 RVA: 0x000B5B52 File Offset: 0x000B4B52
		// (set) Token: 0x06001BF1 RID: 7153 RVA: 0x000B5B5C File Offset: 0x000B4B5C
		[Category("SProjectile")]
		[Browsable(true)]
		[DisplayName("ScanRadius")]
		public double ScanRadius
		{
			get
			{
				return this.scanRadius;
			}
			set
			{
				if (this.scanRadius != value && base.InvokeChanging(null))
				{
					double oldScanRadius = this.scanRadius;
					this.scanRadius = value;
					base.InvokeChanged();
					if (base.Active && Projectile.ScanRadiusChanged != null)
					{
						Projectile.ScanRadiusChanged(this, ref oldScanRadius, ref this.scanRadius);
					}
				}
			}
		}

		// Token: 0x06001BF2 RID: 7154 RVA: 0x000B5BB4 File Offset: 0x000B4BB4
		public void CheckSpawnTimeRareId(SpawnTimeAbstract _spawnTime)
		{
			if (this.ValidSpawnTimeOwnerInstance())
			{
				SpawnTimeRare spawnTimeRare = _spawnTime as SpawnTimeRare;
				if (spawnTimeRare != null)
				{
					spawnTimeRare.Id = this.projectileDBID;
				}
			}
		}

		// Token: 0x06001BF3 RID: 7155 RVA: 0x000B5BDF File Offset: 0x000B4BDF
		public bool ValidSpawnTimeOwnerInstance()
		{
			return true;
		}

		// Token: 0x17000689 RID: 1673
		// (get) Token: 0x06001BF4 RID: 7156 RVA: 0x000B5BE2 File Offset: 0x000B4BE2
		[Browsable(false)]
		public override string SceneName
		{
			get
			{
				if (!this.getSceneNameFromVisObject || string.IsNullOrEmpty(this.projectileDBID))
				{
					return Projectile.defaultStatObject;
				}
				return this.projectileDBID;
			}
		}

		// Token: 0x06001BF5 RID: 7157 RVA: 0x000B5C08 File Offset: 0x000B4C08
		public override IMapObject Clone(int newID, bool newTemporary, bool newActive)
		{
			Projectile projectile = new Projectile(newID, new MapObjectType(base.Type.Type, string.Empty), base.CollisionMap);
			projectile.ProjectileDBID = this.projectileDBID;
			projectile.spawnTime = this.spawnTime;
			projectile.scriptID = this.scriptID;
			projectile.scanRadius = this.scanRadius;
			projectile.getSceneNameFromVisObject = this.getSceneNameFromVisObject;
			base.CopyTo(projectile, newTemporary, newActive);
			return projectile;
		}

		// Token: 0x1700068A RID: 1674
		// (get) Token: 0x06001BF6 RID: 7158 RVA: 0x000B5C80 File Offset: 0x000B4C80
		[Browsable(false)]
		public override string DefaultSceneName
		{
			get
			{
				return Projectile.defaultVisObject;
			}
		}

		// Token: 0x06001BF7 RID: 7159 RVA: 0x000B5C88 File Offset: 0x000B4C88
		public override IMapObjectPack Pack()
		{
			ProjectilePack projectilePack = new ProjectilePack();
			projectilePack.Pack(this);
			return projectilePack;
		}

		// Token: 0x06001BF8 RID: 7160 RVA: 0x000B5CA3 File Offset: 0x000B4CA3
		public Color GetInterfaceColor()
		{
			return Projectile.interfaceColor;
		}

		// Token: 0x06001BF9 RID: 7161 RVA: 0x000B5CAA File Offset: 0x000B4CAA
		public string GetInterfaceSingleObjectTypeName()
		{
			return Projectile.interfaceSingleObjectTypeName;
		}

		// Token: 0x06001BFA RID: 7162 RVA: 0x000B5CB1 File Offset: 0x000B4CB1
		public string GetInterfaceSeveralObjectsTypeName()
		{
			return Projectile.interfaceSeveralObjectsTypeName;
		}

		// Token: 0x06001BFB RID: 7163 RVA: 0x000B5CB8 File Offset: 0x000B4CB8
		public bool ContainsText(string text, bool ignoreCase)
		{
			if (string.IsNullOrEmpty(text))
			{
				return true;
			}
			if (string.IsNullOrEmpty(this.SceneName))
			{
				return false;
			}
			if (ignoreCase)
			{
				return this.SceneName.IndexOf(text, StringComparison.CurrentCultureIgnoreCase) != -1;
			}
			return this.SceneName.Contains(text);
		}

		// Token: 0x06001BFC RID: 7164 RVA: 0x000B5CF6 File Offset: 0x000B4CF6
		public string GetStatsForDBBrowse()
		{
			return this.SceneName;
		}

		// Token: 0x06001BFD RID: 7165 RVA: 0x000B5CFE File Offset: 0x000B4CFE
		public string GetSpecialStatsForDBBrowse()
		{
			return this.SceneName;
		}

		// Token: 0x1700068B RID: 1675
		// (get) Token: 0x06001BFE RID: 7166 RVA: 0x000B5D06 File Offset: 0x000B4D06
		public static string SceneDBType
		{
			get
			{
				return Projectile.projectileDBType;
			}
		}

		// Token: 0x1700068C RID: 1676
		// (get) Token: 0x06001BFF RID: 7167 RVA: 0x000B5D0D File Offset: 0x000B4D0D
		// (set) Token: 0x06001C00 RID: 7168 RVA: 0x000B5D15 File Offset: 0x000B4D15
		[Browsable(false)]
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

		// Token: 0x1700068D RID: 1677
		// (get) Token: 0x06001C01 RID: 7169 RVA: 0x000B5D1E File Offset: 0x000B4D1E
		[Browsable(false)]
		public bool NeedRestartParticle
		{
			get
			{
				return this.needRestartParticle;
			}
		}

		// Token: 0x1700068E RID: 1678
		// (get) Token: 0x06001C02 RID: 7170 RVA: 0x000B5D26 File Offset: 0x000B4D26
		[Browsable(false)]
		public bool NeedRestartIdle
		{
			get
			{
				return this.needRestartIdle;
			}
		}

		// Token: 0x06001C03 RID: 7171 RVA: 0x000B5D30 File Offset: 0x000B4D30
		public bool LoopTimeStep(int time)
		{
			this.restLoopTime -= time;
			bool result = this.restLoopTime <= 0;
			if (result)
			{
				this.restLoopTime = this.fullLoopTime;
			}
			return result;
		}

		// Token: 0x04001201 RID: 4609
		private const string idle = "idle";

		// Token: 0x04001202 RID: 4610
		private const int fps = 30;

		// Token: 0x04001203 RID: 4611
		private static readonly string defaultStatObject = "Editor/Map/SpecialObjects/Projectile/Projectile.(StaticObject).xdb";

		// Token: 0x04001204 RID: 4612
		private static readonly string defaultVisObject = "Editor/Map/SpecialObjects/Projectile/Projectile.(VisObjectTemplate).xdb";

		// Token: 0x04001205 RID: 4613
		private static readonly Color interfaceColor = Color.FromArgb(MapObjectCreationInfo.DefaultTransparentColorAlpha, Color.Lavender);

		// Token: 0x04001206 RID: 4614
		private static readonly string interfaceSingleObjectTypeName = Strings.SINGLE_PROJECTILE_TYPE_NAME;

		// Token: 0x04001207 RID: 4615
		private static readonly string interfaceSeveralObjectsTypeName = Strings.SEVERAL_PROJECTILE_TYPE_NAME;

		// Token: 0x04001208 RID: 4616
		private static readonly string projectileDBType = "gameMechanics.world.projectile.ProjectileResource";

		// Token: 0x04001209 RID: 4617
		private static readonly IDatabase mainDb = IDatabase.GetMainDatabase();

		// Token: 0x0400120A RID: 4618
		private string projectileDBID;

		// Token: 0x0400120B RID: 4619
		private SpawnTimeAbstract spawnTime;

		// Token: 0x0400120C RID: 4620
		private string scriptID = string.Empty;

		// Token: 0x0400120D RID: 4621
		private double scanRadius;

		// Token: 0x0400120E RID: 4622
		private bool getSceneNameFromVisObject;

		// Token: 0x0400120F RID: 4623
		private bool needRestartParticle;

		// Token: 0x04001210 RID: 4624
		private bool needRestartIdle;

		// Token: 0x04001211 RID: 4625
		private int fullLoopTime;

		// Token: 0x04001212 RID: 4626
		private int restLoopTime;

		// Token: 0x0200024A RID: 586
		// (Invoke) Token: 0x06001C06 RID: 7174
		public delegate void ProjectileFieldChangedEvent<T>(Projectile projectile, ref T oldValue, ref T newValue);
	}
}
