using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Design;
using MapEditor.Resources.Strings;
using Tools.MapObjects;

namespace MapEditor.Map.MapObjects
{
	// Token: 0x02000174 RID: 372
	public class ExtendedSound : MapObject, IMapObjectInterfaceExtention
	{
		// Token: 0x1400006A RID: 106
		// (add) Token: 0x060011F4 RID: 4596 RVA: 0x0008461E File Offset: 0x0008361E
		// (remove) Token: 0x060011F5 RID: 4597 RVA: 0x00084635 File Offset: 0x00083635
		public static event ExtendedSound.ExtendedSoundFieldChangedEvent<Sound> CentralSoundChanged;

		// Token: 0x1400006B RID: 107
		// (add) Token: 0x060011F6 RID: 4598 RVA: 0x0008464C File Offset: 0x0008364C
		// (remove) Token: 0x060011F7 RID: 4599 RVA: 0x00084663 File Offset: 0x00083663
		public static event ExtendedSound.ExtendedSoundFieldChangedEvent<Sound> SideSoundChanged;

		// Token: 0x1400006C RID: 108
		// (add) Token: 0x060011F8 RID: 4600 RVA: 0x0008467A File Offset: 0x0008367A
		// (remove) Token: 0x060011F9 RID: 4601 RVA: 0x00084691 File Offset: 0x00083691
		public static event ExtendedSound.ExtendedSoundFieldChangedEvent<string> NameChanged;

		// Token: 0x17000392 RID: 914
		// (get) Token: 0x060011FA RID: 4602 RVA: 0x000846A8 File Offset: 0x000836A8
		public static Color InterfaceColor
		{
			get
			{
				return ExtendedSound.interfaceColor;
			}
		}

		// Token: 0x17000393 RID: 915
		// (get) Token: 0x060011FB RID: 4603 RVA: 0x000846AF File Offset: 0x000836AF
		public static string InterfaceSingleObjectTypeName
		{
			get
			{
				return ExtendedSound.interfaceSingleObjectTypeName;
			}
		}

		// Token: 0x17000394 RID: 916
		// (get) Token: 0x060011FC RID: 4604 RVA: 0x000846B6 File Offset: 0x000836B6
		public static string InterfaceSeveralObjectsTypeName
		{
			get
			{
				return ExtendedSound.interfaceSeveralObjectsTypeName;
			}
		}

		// Token: 0x17000395 RID: 917
		// (get) Token: 0x060011FD RID: 4605 RVA: 0x000846BD File Offset: 0x000836BD
		public static string DefaultVisObject
		{
			get
			{
				return ExtendedSound.defaultVisObject;
			}
		}

		// Token: 0x17000396 RID: 918
		// (get) Token: 0x060011FE RID: 4606 RVA: 0x000846C4 File Offset: 0x000836C4
		public static string ExtendedSoundsFolder
		{
			get
			{
				return ExtendedSound.extendedSoundsFolder;
			}
		}

		// Token: 0x17000397 RID: 919
		// (get) Token: 0x060011FF RID: 4607 RVA: 0x000846CB File Offset: 0x000836CB
		public static string ExtendedSoundsDBType
		{
			get
			{
				return ExtendedSound.extendedSoundsDBType;
			}
		}

		// Token: 0x17000398 RID: 920
		// (get) Token: 0x06001200 RID: 4608 RVA: 0x000846D2 File Offset: 0x000836D2
		public static string ExtendedSoundDBType
		{
			get
			{
				return ExtendedSound.extendedSoundDBType;
			}
		}

		// Token: 0x17000399 RID: 921
		// (get) Token: 0x06001201 RID: 4609 RVA: 0x000846D9 File Offset: 0x000836D9
		public static string ExtendedSoundCollectionFileName
		{
			get
			{
				return ExtendedSound.extendedSoundCollectionFileName;
			}
		}

		// Token: 0x1700039A RID: 922
		// (get) Token: 0x06001202 RID: 4610 RVA: 0x000846E0 File Offset: 0x000836E0
		public static string ExtendedSoundCollectionDBExtention
		{
			get
			{
				return ExtendedSound.extendedSoundCollectionDBExtention;
			}
		}

		// Token: 0x1700039B RID: 923
		// (get) Token: 0x06001203 RID: 4611 RVA: 0x000846E7 File Offset: 0x000836E7
		public static string ExtendedSoundDBExtention
		{
			get
			{
				return ExtendedSound.extendedSoundDBExtention;
			}
		}

		// Token: 0x1700039C RID: 924
		// (get) Token: 0x06001204 RID: 4612 RVA: 0x000846EE File Offset: 0x000836EE
		public static string ExtendedSoundDefaultName
		{
			get
			{
				return ExtendedSound.extendedSoundDefaultName;
			}
		}

		// Token: 0x06001205 RID: 4613 RVA: 0x000846F5 File Offset: 0x000836F5
		public ExtendedSound(int _id, MapObjectType _type, ICollisionMap _collisionMap) : base(_id, _type, _collisionMap)
		{
		}

		// Token: 0x1700039D RID: 925
		// (get) Token: 0x06001206 RID: 4614 RVA: 0x00084721 File Offset: 0x00083721
		// (set) Token: 0x06001207 RID: 4615 RVA: 0x0008472C File Offset: 0x0008372C
		[Editor(typeof(SoundTypeEditor), typeof(UITypeEditor))]
		[Browsable(true)]
		[Category("ExtendedSound")]
		[DisplayName("CentralSound")]
		[TypeConverter(typeof(SoundConverter))]
		public Sound CentralSound
		{
			get
			{
				return this.centralSound;
			}
			set
			{
				if (this.centralSound != value && base.InvokeChanging(null))
				{
					Sound oldSound = this.centralSound;
					this.centralSound = value;
					base.InvokeChanged();
					if (base.Active && ExtendedSound.CentralSoundChanged != null)
					{
						ExtendedSound.CentralSoundChanged(this, ref oldSound, ref this.centralSound);
					}
				}
			}
		}

		// Token: 0x1700039E RID: 926
		// (get) Token: 0x06001208 RID: 4616 RVA: 0x00084781 File Offset: 0x00083781
		// (set) Token: 0x06001209 RID: 4617 RVA: 0x0008478C File Offset: 0x0008378C
		[Category("ExtendedSound")]
		[TypeConverter(typeof(SoundConverter))]
		[Browsable(true)]
		[DisplayName("SideSound")]
		public Sound SideSound
		{
			get
			{
				return this.sideSound;
			}
			set
			{
				if (this.sideSound != value && base.InvokeChanging(null))
				{
					Sound oldSound = this.sideSound;
					this.sideSound = value;
					base.InvokeChanged();
					if (base.Active && ExtendedSound.SideSoundChanged != null)
					{
						ExtendedSound.SideSoundChanged(this, ref oldSound, ref this.sideSound);
					}
				}
			}
		}

		// Token: 0x1700039F RID: 927
		// (get) Token: 0x0600120A RID: 4618 RVA: 0x000847E1 File Offset: 0x000837E1
		// (set) Token: 0x0600120B RID: 4619 RVA: 0x000847EC File Offset: 0x000837EC
		[DisplayName("Name")]
		[Category("ExtendedSound")]
		[Browsable(true)]
		public string Name
		{
			get
			{
				return this.name;
			}
			set
			{
				if (this.name != value && base.InvokeChanging(null))
				{
					string oldName = this.name;
					this.name = value;
					base.InvokeChanged();
					if (base.Active && ExtendedSound.NameChanged != null)
					{
						ExtendedSound.NameChanged(this, ref oldName, ref this.name);
					}
				}
			}
		}

		// Token: 0x170003A0 RID: 928
		// (get) Token: 0x0600120C RID: 4620 RVA: 0x00084846 File Offset: 0x00083846
		[Browsable(false)]
		public override string SceneName
		{
			get
			{
				return ExtendedSound.defaultVisObject;
			}
		}

		// Token: 0x0600120D RID: 4621 RVA: 0x00084850 File Offset: 0x00083850
		public override IMapObject Clone(int newID, bool newTemporary, bool newActive)
		{
			ExtendedSound extendedSound = new ExtendedSound(newID, new MapObjectType(base.Type.Type, string.Empty), base.CollisionMap);
			extendedSound.centralSound = this.centralSound;
			extendedSound.sideSound = this.sideSound;
			base.CopyTo(extendedSound, newTemporary, newActive);
			return extendedSound;
		}

		// Token: 0x0600120E RID: 4622 RVA: 0x000848A4 File Offset: 0x000838A4
		public override IMapObjectPack Pack()
		{
			ExtendedSoundPack extendedSoundPack = new ExtendedSoundPack();
			extendedSoundPack.Pack(this);
			return extendedSoundPack;
		}

		// Token: 0x0600120F RID: 4623 RVA: 0x000848BF File Offset: 0x000838BF
		public Color GetInterfaceColor()
		{
			return ExtendedSound.interfaceColor;
		}

		// Token: 0x06001210 RID: 4624 RVA: 0x000848C6 File Offset: 0x000838C6
		public string GetInterfaceSingleObjectTypeName()
		{
			return ExtendedSound.interfaceSingleObjectTypeName;
		}

		// Token: 0x06001211 RID: 4625 RVA: 0x000848CD File Offset: 0x000838CD
		public string GetInterfaceSeveralObjectsTypeName()
		{
			return ExtendedSound.interfaceSeveralObjectsTypeName;
		}

		// Token: 0x06001212 RID: 4626 RVA: 0x000848D4 File Offset: 0x000838D4
		public bool ContainsText(string text, bool ignoreCase)
		{
			return this.centralSound.ContainsText(text, ignoreCase) || this.sideSound.ContainsText(text, ignoreCase);
		}

		// Token: 0x06001213 RID: 4627 RVA: 0x000848F4 File Offset: 0x000838F4
		public string GetStatsForDBBrowse()
		{
			return this.centralSound.ToString();
		}

		// Token: 0x06001214 RID: 4628 RVA: 0x00084901 File Offset: 0x00083901
		public string GetSpecialStatsForDBBrowse()
		{
			return this.sideSound.ToString();
		}

		// Token: 0x04000CD4 RID: 3284
		private static readonly string extendedSoundsFolder = "GlobalObjects/ExtendedSounds/";

		// Token: 0x04000CD5 RID: 3285
		private static readonly string extendedSoundsDBType = "ExtendedSoundCollection";

		// Token: 0x04000CD6 RID: 3286
		private static readonly string extendedSoundDBType = "ExtendedSound";

		// Token: 0x04000CD7 RID: 3287
		private static readonly string extendedSoundCollectionFileName = "ExtendedSoundCollection.xdb";

		// Token: 0x04000CD8 RID: 3288
		private static readonly string extendedSoundCollectionDBExtention = ".(ExtendedSoundCollection).xdb";

		// Token: 0x04000CD9 RID: 3289
		private static readonly string extendedSoundDBExtention = ".(ExtendedSound).xdb";

		// Token: 0x04000CDA RID: 3290
		private static readonly string extendedSoundDefaultName = "ExtendedSound";

		// Token: 0x04000CDB RID: 3291
		private static readonly Color interfaceColor = Color.FromArgb(MapObjectCreationInfo.DefaultTransparentColorAlpha, Color.Black);

		// Token: 0x04000CDC RID: 3292
		private static readonly string interfaceSingleObjectTypeName = Strings.SINGLE_EXTENDED_SOUND_TYPE_NAME;

		// Token: 0x04000CDD RID: 3293
		private static readonly string interfaceSeveralObjectsTypeName = Strings.SEVERAL_EXTENDED_SOUNDS_TYPE_NAME;

		// Token: 0x04000CDE RID: 3294
		private Sound centralSound = new Sound();

		// Token: 0x04000CDF RID: 3295
		private Sound sideSound = new Sound();

		// Token: 0x04000CE0 RID: 3296
		private string name = string.Empty;

		// Token: 0x04000CE1 RID: 3297
		private static readonly string defaultVisObject = "Editor/Map/SpecialObjects/ExtendedSound/ExtendedSound.(StaticObject).xdb";

		// Token: 0x02000175 RID: 373
		// (Invoke) Token: 0x06001217 RID: 4631
		public delegate void ExtendedSoundFieldChangedEvent<T>(ExtendedSound extendedSound, ref T oldValue, ref T newValue);
	}
}
