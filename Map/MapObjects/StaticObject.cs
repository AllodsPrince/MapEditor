using System;
using System.ComponentModel;
using System.Drawing;
using MapEditor.Resources.Strings;
using Tools.MapObjects;

namespace MapEditor.Map.MapObjects
{
	// Token: 0x02000109 RID: 265
	public class StaticObject : MapObject, IMapObjectInterfaceExtention
	{
		// Token: 0x1400003A RID: 58
		// (add) Token: 0x06000D03 RID: 3331 RVA: 0x0006E280 File Offset: 0x0006D280
		// (remove) Token: 0x06000D04 RID: 3332 RVA: 0x0006E297 File Offset: 0x0006D297
		public static event StaticObject.StaticObjectFieldChangedEvent<bool> AICollisionChanged;

		// Token: 0x17000267 RID: 615
		// (get) Token: 0x06000D05 RID: 3333 RVA: 0x0006E2AE File Offset: 0x0006D2AE
		public static Color InterfaceColor
		{
			get
			{
				return StaticObject.interfaceColor;
			}
		}

		// Token: 0x17000268 RID: 616
		// (get) Token: 0x06000D06 RID: 3334 RVA: 0x0006E2B5 File Offset: 0x0006D2B5
		public static string InterfaceSingleObjectTypeName
		{
			get
			{
				return StaticObject.interfaceSingleObjectTypeName;
			}
		}

		// Token: 0x17000269 RID: 617
		// (get) Token: 0x06000D07 RID: 3335 RVA: 0x0006E2BC File Offset: 0x0006D2BC
		public static string InterfaceSeveralObjectsTypeName
		{
			get
			{
				return StaticObject.interfaceSeveralObjectsTypeName;
			}
		}

		// Token: 0x1700026A RID: 618
		// (get) Token: 0x06000D08 RID: 3336 RVA: 0x0006E2C3 File Offset: 0x0006D2C3
		public static string DBType
		{
			get
			{
				return StaticObject.dbType;
			}
		}

		// Token: 0x06000D09 RID: 3337 RVA: 0x0006E2CC File Offset: 0x0006D2CC
		public StaticObject(int _id, MapObjectType _type, ICollisionMap _collisionMap) : base(_id, _type, _collisionMap)
		{
			this.pureSound = base.SceneName.StartsWith("SFX/StaticObjects/");
			if (this.pureSound)
			{
				base.Highlight(StaticObject.interfacePureSoundColor);
			}
		}

		// Token: 0x1700026B RID: 619
		// (get) Token: 0x06000D0A RID: 3338 RVA: 0x0006E328 File Offset: 0x0006D328
		[Browsable(false)]
		public bool PureSound
		{
			get
			{
				return this.pureSound;
			}
		}

		// Token: 0x1700026C RID: 620
		// (get) Token: 0x06000D0B RID: 3339 RVA: 0x0006E330 File Offset: 0x0006D330
		// (set) Token: 0x06000D0C RID: 3340 RVA: 0x0006E338 File Offset: 0x0006D338
		[Browsable(false)]
		public string Device
		{
			get
			{
				return this.device;
			}
			set
			{
				this.device = value;
			}
		}

		// Token: 0x1700026D RID: 621
		// (get) Token: 0x06000D0D RID: 3341 RVA: 0x0006E341 File Offset: 0x0006D341
		// (set) Token: 0x06000D0E RID: 3342 RVA: 0x0006E349 File Offset: 0x0006D349
		[Browsable(false)]
		public string ScriptID
		{
			get
			{
				return this.scriptID;
			}
			set
			{
				this.scriptID = Str.Trim(value);
			}
		}

		// Token: 0x1700026E RID: 622
		// (get) Token: 0x06000D0F RID: 3343 RVA: 0x0006E357 File Offset: 0x0006D357
		// (set) Token: 0x06000D10 RID: 3344 RVA: 0x0006E35F File Offset: 0x0006D35F
		[Browsable(false)]
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

		// Token: 0x1700026F RID: 623
		// (get) Token: 0x06000D11 RID: 3345 RVA: 0x0006E368 File Offset: 0x0006D368
		// (set) Token: 0x06000D12 RID: 3346 RVA: 0x0006E370 File Offset: 0x0006D370
		[Browsable(true)]
		public bool AICollision
		{
			get
			{
				return this.aiCollision;
			}
			set
			{
				if (this.aiCollision != value && base.InvokeChanging(null))
				{
					bool oldAICollision = this.aiCollision;
					this.aiCollision = value;
					base.InvokeChanged();
					if (base.Active && StaticObject.AICollisionChanged != null)
					{
						StaticObject.AICollisionChanged(this, ref oldAICollision, ref this.aiCollision);
					}
				}
			}
		}

		// Token: 0x06000D13 RID: 3347 RVA: 0x0006E3C8 File Offset: 0x0006D3C8
		public PermanentDevice CloneToPermanentDevice()
		{
			PermanentDevice permanentDevice = new PermanentDevice(base.ID, new MapObjectType(MapObjectFactory.Type.PermanentDevice, base.Type.Stats), base.CollisionMap);
			permanentDevice.Device = this.device;
			permanentDevice.ScriptID = this.scriptID;
			permanentDevice.ScanRadius = this.scanRadius;
			Color color = permanentDevice.HighlightColor;
			base.CopyTo(permanentDevice, base.Temporary, false);
			permanentDevice.Highlight(color);
			permanentDevice.Active = base.Active;
			permanentDevice.AICollision = this.aiCollision;
			return permanentDevice;
		}

		// Token: 0x06000D14 RID: 3348 RVA: 0x0006E458 File Offset: 0x0006D458
		public override IMapObject Clone(int newID, bool newTemporary, bool newActive)
		{
			StaticObject staticObject = new StaticObject(newID, new MapObjectType(base.Type.Type, base.Type.Stats), base.CollisionMap);
			staticObject.device = this.device;
			staticObject.scriptID = this.scriptID;
			staticObject.scanRadius = this.scanRadius;
			staticObject.aiCollision = this.aiCollision;
			base.CopyTo(staticObject, newTemporary, newActive);
			return staticObject;
		}

		// Token: 0x17000270 RID: 624
		// (get) Token: 0x06000D15 RID: 3349 RVA: 0x0006E4CD File Offset: 0x0006D4CD
		public override string DefaultSceneName
		{
			get
			{
				if (!this.pureSound)
				{
					return base.DefaultSceneName;
				}
				return "Editor/Map/SpecialObjects/SoundObject/SoundObject.(StaticObject).xdb";
			}
		}

		// Token: 0x06000D16 RID: 3350 RVA: 0x0006E4E4 File Offset: 0x0006D4E4
		public override IMapObjectPack Pack()
		{
			StaticObjectPack staticObjectPack = new StaticObjectPack();
			staticObjectPack.Pack(this);
			return staticObjectPack;
		}

		// Token: 0x06000D17 RID: 3351 RVA: 0x0006E4FF File Offset: 0x0006D4FF
		public Color GetInterfaceColor()
		{
			return StaticObject.interfaceColor;
		}

		// Token: 0x06000D18 RID: 3352 RVA: 0x0006E506 File Offset: 0x0006D506
		public string GetInterfaceSingleObjectTypeName()
		{
			return StaticObject.interfaceSingleObjectTypeName;
		}

		// Token: 0x06000D19 RID: 3353 RVA: 0x0006E50D File Offset: 0x0006D50D
		public string GetInterfaceSeveralObjectsTypeName()
		{
			return StaticObject.interfaceSeveralObjectsTypeName;
		}

		// Token: 0x06000D1A RID: 3354 RVA: 0x0006E514 File Offset: 0x0006D514
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
				return this.SceneName.ToLower().Contains(text.ToLower());
			}
			return this.SceneName.Contains(text);
		}

		// Token: 0x06000D1B RID: 3355 RVA: 0x0006E560 File Offset: 0x0006D560
		public string GetStatsForDBBrowse()
		{
			return base.Type.Stats;
		}

		// Token: 0x06000D1C RID: 3356 RVA: 0x0006E57C File Offset: 0x0006D57C
		public string GetSpecialStatsForDBBrowse()
		{
			return base.Type.Stats;
		}

		// Token: 0x04000A85 RID: 2693
		public const string pureSoundPath = "SFX/StaticObjects/";

		// Token: 0x04000A86 RID: 2694
		private const string pureSoundObjectTemplate = "Editor/Map/SpecialObjects/SoundObject/SoundObject.(StaticObject).xdb";

		// Token: 0x04000A87 RID: 2695
		private static readonly Color interfaceColor = Color.FromArgb(MapObjectCreationInfo.DefaultTransparentColorAlpha, Color.Black);

		// Token: 0x04000A88 RID: 2696
		private static readonly string interfaceSingleObjectTypeName = Strings.SINGLE_STATIC_OBJECT_TYPE_NAME;

		// Token: 0x04000A89 RID: 2697
		private static readonly string interfaceSeveralObjectsTypeName = Strings.SEVERAL_STATIC_OBJECTS_TYPE_NAME;

		// Token: 0x04000A8A RID: 2698
		private static readonly string dbType = "mapLoader.StaticObject";

		// Token: 0x04000A8B RID: 2699
		private string device = string.Empty;

		// Token: 0x04000A8C RID: 2700
		private string scriptID = string.Empty;

		// Token: 0x04000A8D RID: 2701
		private double scanRadius;

		// Token: 0x04000A8E RID: 2702
		private bool aiCollision = true;

		// Token: 0x04000A8F RID: 2703
		private static readonly Color interfacePureSoundColor = Color.FromArgb(MapObjectCreationInfo.DefaultTransparentColorAlpha, Color.Tomato);

		// Token: 0x04000A90 RID: 2704
		private readonly bool pureSound;

		// Token: 0x0200010A RID: 266
		// (Invoke) Token: 0x06000D1F RID: 3359
		public delegate void StaticObjectFieldChangedEvent<T>(StaticObject staticObject, ref T oldValue, ref T newValue);
	}
}
