using System;
using System.ComponentModel;
using System.Drawing;
using MapEditor.Resources.Strings;
using Tools.MapObjects;

namespace MapEditor.Map.MapObjects
{
	// Token: 0x020001FA RID: 506
	public class PermanentDevice : MapObject, IMapObjectInterfaceExtention
	{
		// Token: 0x140000B1 RID: 177
		// (add) Token: 0x0600191C RID: 6428 RVA: 0x000A5EF7 File Offset: 0x000A4EF7
		// (remove) Token: 0x0600191D RID: 6429 RVA: 0x000A5F0E File Offset: 0x000A4F0E
		public static event PermanentDevice.PermanentDeviceFieldChangedEvent<string> DeviceChanged;

		// Token: 0x140000B2 RID: 178
		// (add) Token: 0x0600191E RID: 6430 RVA: 0x000A5F25 File Offset: 0x000A4F25
		// (remove) Token: 0x0600191F RID: 6431 RVA: 0x000A5F3C File Offset: 0x000A4F3C
		public static event PermanentDevice.PermanentDeviceFieldChangedEvent<string> ScriptIDChanged;

		// Token: 0x140000B3 RID: 179
		// (add) Token: 0x06001920 RID: 6432 RVA: 0x000A5F53 File Offset: 0x000A4F53
		// (remove) Token: 0x06001921 RID: 6433 RVA: 0x000A5F6A File Offset: 0x000A4F6A
		public static event PermanentDevice.PermanentDeviceFieldChangedEvent<double> ScanRadiusChanged;

		// Token: 0x140000B4 RID: 180
		// (add) Token: 0x06001922 RID: 6434 RVA: 0x000A5F81 File Offset: 0x000A4F81
		// (remove) Token: 0x06001923 RID: 6435 RVA: 0x000A5F98 File Offset: 0x000A4F98
		public static event PermanentDevice.PermanentDeviceFieldChangedEvent<bool> AICollisionChanged;

		// Token: 0x17000609 RID: 1545
		// (get) Token: 0x06001924 RID: 6436 RVA: 0x000A5FAF File Offset: 0x000A4FAF
		public static Color InterfaceColor
		{
			get
			{
				return PermanentDevice.interfaceColor;
			}
		}

		// Token: 0x1700060A RID: 1546
		// (get) Token: 0x06001925 RID: 6437 RVA: 0x000A5FB6 File Offset: 0x000A4FB6
		public static string InterfaceSingleObjectTypeName
		{
			get
			{
				return PermanentDevice.interfaceSingleObjectTypeName;
			}
		}

		// Token: 0x1700060B RID: 1547
		// (get) Token: 0x06001926 RID: 6438 RVA: 0x000A5FBD File Offset: 0x000A4FBD
		public static string InterfaceSeveralObjectsTypeName
		{
			get
			{
				return PermanentDevice.interfaceSeveralObjectsTypeName;
			}
		}

		// Token: 0x1700060C RID: 1548
		// (get) Token: 0x06001927 RID: 6439 RVA: 0x000A5FC4 File Offset: 0x000A4FC4
		public static string DefaultVisObject
		{
			get
			{
				return PermanentDevice.defaultVisObject;
			}
		}

		// Token: 0x1700060D RID: 1549
		// (get) Token: 0x06001928 RID: 6440 RVA: 0x000A5FCB File Offset: 0x000A4FCB
		public static string DeviceDBType
		{
			get
			{
				return PermanentDevice.deviceDBType;
			}
		}

		// Token: 0x06001929 RID: 6441 RVA: 0x000A5FD2 File Offset: 0x000A4FD2
		public PermanentDevice(int _id, MapObjectType _type, ICollisionMap _collisionMap) : base(_id, _type, _collisionMap)
		{
			base.Highlight(PermanentDevice.interfaceColor);
		}

		// Token: 0x1700060E RID: 1550
		// (get) Token: 0x0600192A RID: 6442 RVA: 0x000A6005 File Offset: 0x000A5005
		// (set) Token: 0x0600192B RID: 6443 RVA: 0x000A6010 File Offset: 0x000A5010
		[Category("PermanentDevice")]
		[Browsable(true)]
		[DisplayName("Device")]
		public string Device
		{
			get
			{
				return this.device;
			}
			set
			{
				if (this.device != value && base.InvokeChanging(null))
				{
					string oldDevice = this.device;
					this.device = value;
					base.InvokeChanged();
					if (base.Active && PermanentDevice.DeviceChanged != null)
					{
						PermanentDevice.DeviceChanged(this, ref oldDevice, ref this.device);
					}
				}
			}
		}

		// Token: 0x1700060F RID: 1551
		// (get) Token: 0x0600192C RID: 6444 RVA: 0x000A606A File Offset: 0x000A506A
		// (set) Token: 0x0600192D RID: 6445 RVA: 0x000A6074 File Offset: 0x000A5074
		[Category("PermanentDevice")]
		[DisplayName("ScriptID")]
		[Browsable(true)]
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
					if (base.Active && PermanentDevice.ScriptIDChanged != null)
					{
						PermanentDevice.ScriptIDChanged(this, ref oldScriptID, ref this.scriptID);
					}
				}
			}
		}

		// Token: 0x17000610 RID: 1552
		// (get) Token: 0x0600192E RID: 6446 RVA: 0x000A60D6 File Offset: 0x000A50D6
		// (set) Token: 0x0600192F RID: 6447 RVA: 0x000A60E0 File Offset: 0x000A50E0
		[DisplayName("ScanRadius")]
		[Browsable(true)]
		[Category("PermanentDevice")]
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
					if (base.Active && PermanentDevice.ScanRadiusChanged != null)
					{
						PermanentDevice.ScanRadiusChanged(this, ref oldScanRadius, ref this.scanRadius);
					}
				}
			}
		}

		// Token: 0x17000611 RID: 1553
		// (get) Token: 0x06001930 RID: 6448 RVA: 0x000A6135 File Offset: 0x000A5135
		// (set) Token: 0x06001931 RID: 6449 RVA: 0x000A6140 File Offset: 0x000A5140
		[DisplayName("AICollision")]
		[Category("PermanentDevice")]
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
					if (base.Active && PermanentDevice.AICollisionChanged != null)
					{
						PermanentDevice.AICollisionChanged(this, ref oldAICollision, ref this.aiCollision);
					}
				}
			}
		}

		// Token: 0x06001932 RID: 6450 RVA: 0x000A6198 File Offset: 0x000A5198
		public StaticObject CloneToStaticObject()
		{
			StaticObject staticObject = new StaticObject(base.ID, new MapObjectType(MapObjectFactory.Type.StaticObject, base.Type.Stats), base.CollisionMap);
			staticObject.Device = this.device;
			staticObject.ScriptID = this.scriptID;
			staticObject.ScanRadius = this.scanRadius;
			staticObject.AICollision = this.aiCollision;
			Color color = staticObject.HighlightColor;
			base.CopyTo(staticObject, base.Temporary, false);
			staticObject.Highlight(color);
			staticObject.Active = base.Active;
			return staticObject;
		}

		// Token: 0x06001933 RID: 6451 RVA: 0x000A6228 File Offset: 0x000A5228
		public override IMapObject Clone(int newID, bool newTemporary, bool newActive)
		{
			PermanentDevice permanentDevice = new PermanentDevice(newID, new MapObjectType(base.Type.Type, base.Type.Stats), base.CollisionMap);
			permanentDevice.device = this.device;
			permanentDevice.scriptID = this.scriptID;
			permanentDevice.scanRadius = this.scanRadius;
			permanentDevice.aiCollision = this.aiCollision;
			base.CopyTo(permanentDevice, newTemporary, newActive);
			return permanentDevice;
		}

		// Token: 0x06001934 RID: 6452 RVA: 0x000A62A0 File Offset: 0x000A52A0
		public override IMapObjectPack Pack()
		{
			PermanentDevicePack permanentDevicePack = new PermanentDevicePack();
			permanentDevicePack.Pack(this);
			return permanentDevicePack;
		}

		// Token: 0x06001935 RID: 6453 RVA: 0x000A62BB File Offset: 0x000A52BB
		public Color GetInterfaceColor()
		{
			return PermanentDevice.interfaceColor;
		}

		// Token: 0x06001936 RID: 6454 RVA: 0x000A62C2 File Offset: 0x000A52C2
		public string GetInterfaceSingleObjectTypeName()
		{
			return PermanentDevice.interfaceSingleObjectTypeName;
		}

		// Token: 0x06001937 RID: 6455 RVA: 0x000A62C9 File Offset: 0x000A52C9
		public string GetInterfaceSeveralObjectsTypeName()
		{
			return PermanentDevice.interfaceSeveralObjectsTypeName;
		}

		// Token: 0x06001938 RID: 6456 RVA: 0x000A62D0 File Offset: 0x000A52D0
		public bool ContainsText(string text, bool ignoreCase)
		{
			if (string.IsNullOrEmpty(text))
			{
				return true;
			}
			if (ignoreCase)
			{
				return (!string.IsNullOrEmpty(this.device) && this.device.ToLower().Contains(text.ToLower())) || (!string.IsNullOrEmpty(this.scriptID) && this.scriptID.ToLower().Contains(text.ToLower()));
			}
			return (!string.IsNullOrEmpty(this.device) && this.device.Contains(text)) || (!string.IsNullOrEmpty(this.scriptID) && this.scriptID.Contains(text));
		}

		// Token: 0x06001939 RID: 6457 RVA: 0x000A6378 File Offset: 0x000A5378
		public string GetStatsForDBBrowse()
		{
			return this.device;
		}

		// Token: 0x0600193A RID: 6458 RVA: 0x000A6380 File Offset: 0x000A5380
		public string GetSpecialStatsForDBBrowse()
		{
			return this.SceneName;
		}

		// Token: 0x0400102C RID: 4140
		private static readonly Color interfaceColor = Color.FromArgb(MapObjectCreationInfo.DefaultTransparentColorAlpha, Color.Turquoise);

		// Token: 0x0400102D RID: 4141
		private static readonly string interfaceSingleObjectTypeName = Strings.SINGLE_PERMANENT_DEVICE_TYPE_NAME;

		// Token: 0x0400102E RID: 4142
		private static readonly string interfaceSeveralObjectsTypeName = Strings.SEVERAL_PERMANENT_DEVICES_TYPE_NAME;

		// Token: 0x0400102F RID: 4143
		private static readonly string defaultVisObject = "Editor/Map/SpecialObjects/PermanentDevice/PermanentDevice.(StaticObject).xdb";

		// Token: 0x04001030 RID: 4144
		private static readonly string deviceDBType = "gameMechanics.world.device.DeviceResource";

		// Token: 0x04001031 RID: 4145
		private string device = string.Empty;

		// Token: 0x04001032 RID: 4146
		private string scriptID = string.Empty;

		// Token: 0x04001033 RID: 4147
		private double scanRadius;

		// Token: 0x04001034 RID: 4148
		private bool aiCollision = true;

		// Token: 0x020001FB RID: 507
		// (Invoke) Token: 0x0600193D RID: 6461
		public delegate void PermanentDeviceFieldChangedEvent<T>(PermanentDevice permanentDevice, ref T oldValue, ref T newValue);
	}
}
