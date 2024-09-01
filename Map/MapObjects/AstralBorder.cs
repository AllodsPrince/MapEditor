using System;
using System.ComponentModel;
using System.Drawing;
using MapEditor.Resources.Strings;
using Tools.MapObjects;

namespace MapEditor.Map.MapObjects
{
	// Token: 0x02000155 RID: 341
	public class AstralBorder : MapObject, IMapObjectInterfaceExtention
	{
		// Token: 0x14000054 RID: 84
		// (add) Token: 0x06001070 RID: 4208 RVA: 0x0007B79A File Offset: 0x0007A79A
		// (remove) Token: 0x06001071 RID: 4209 RVA: 0x0007B7B1 File Offset: 0x0007A7B1
		public static event AstralBorder.AstralBorderFieldChangedEvent<double> StabilityRadiusChanged;

		// Token: 0x17000365 RID: 869
		// (get) Token: 0x06001072 RID: 4210 RVA: 0x0007B7C8 File Offset: 0x0007A7C8
		public static Color InterfaceColor
		{
			get
			{
				return AstralBorder.interfaceColor;
			}
		}

		// Token: 0x17000366 RID: 870
		// (get) Token: 0x06001073 RID: 4211 RVA: 0x0007B7CF File Offset: 0x0007A7CF
		public static string InterfaceSingleObjectTypeName
		{
			get
			{
				return AstralBorder.interfaceSingleObjectTypeName;
			}
		}

		// Token: 0x17000367 RID: 871
		// (get) Token: 0x06001074 RID: 4212 RVA: 0x0007B7D6 File Offset: 0x0007A7D6
		public static string InterfaceSeveralObjectsTypeName
		{
			get
			{
				return AstralBorder.interfaceSeveralObjectsTypeName;
			}
		}

		// Token: 0x17000368 RID: 872
		// (get) Token: 0x06001075 RID: 4213 RVA: 0x0007B7DD File Offset: 0x0007A7DD
		public static string DefaultVisObject
		{
			get
			{
				return AstralBorder.defaultVisObject;
			}
		}

		// Token: 0x17000369 RID: 873
		// (get) Token: 0x06001076 RID: 4214 RVA: 0x0007B7E4 File Offset: 0x0007A7E4
		public static double DefaultStabilityRadius
		{
			get
			{
				return AstralBorder.defaultStabilityRadius;
			}
		}

		// Token: 0x06001077 RID: 4215 RVA: 0x0007B7EB File Offset: 0x0007A7EB
		public AstralBorder(int _id, MapObjectType _type, ICollisionMap _collisionMap) : base(_id, _type, _collisionMap)
		{
		}

		// Token: 0x1700036A RID: 874
		// (get) Token: 0x06001078 RID: 4216 RVA: 0x0007B801 File Offset: 0x0007A801
		// (set) Token: 0x06001079 RID: 4217 RVA: 0x0007B80C File Offset: 0x0007A80C
		[Category("AstralBorder")]
		[Browsable(true)]
		[DisplayName("StabilityRadius")]
		public double StabilityRadius
		{
			get
			{
				return this.stabilityRadius;
			}
			set
			{
				if (this.stabilityRadius != value && base.InvokeChanging(null))
				{
					double oldStabilityRadius = this.stabilityRadius;
					this.stabilityRadius = value;
					base.InvokeChanged();
					if (base.Active && AstralBorder.StabilityRadiusChanged != null)
					{
						AstralBorder.StabilityRadiusChanged(this, ref oldStabilityRadius, ref this.stabilityRadius);
					}
				}
			}
		}

		// Token: 0x1700036B RID: 875
		// (get) Token: 0x0600107A RID: 4218 RVA: 0x0007B861 File Offset: 0x0007A861
		[Browsable(false)]
		public override string SceneName
		{
			get
			{
				return AstralBorder.defaultVisObject;
			}
		}

		// Token: 0x0600107B RID: 4219 RVA: 0x0007B868 File Offset: 0x0007A868
		public override IMapObject Clone(int newID, bool newTemporary, bool newActive)
		{
			AstralBorder astralBorder = new AstralBorder(newID, new MapObjectType(base.Type.Type, string.Empty), base.CollisionMap);
			astralBorder.stabilityRadius = this.stabilityRadius;
			base.CopyTo(astralBorder, newTemporary, newActive);
			return astralBorder;
		}

		// Token: 0x0600107C RID: 4220 RVA: 0x0007B8B0 File Offset: 0x0007A8B0
		public override IMapObjectPack Pack()
		{
			AstralBorderPack astralBorderPack = new AstralBorderPack();
			astralBorderPack.Pack(this);
			return astralBorderPack;
		}

		// Token: 0x0600107D RID: 4221 RVA: 0x0007B8CB File Offset: 0x0007A8CB
		public Color GetInterfaceColor()
		{
			return AstralBorder.interfaceColor;
		}

		// Token: 0x0600107E RID: 4222 RVA: 0x0007B8D2 File Offset: 0x0007A8D2
		public string GetInterfaceSingleObjectTypeName()
		{
			return AstralBorder.interfaceSingleObjectTypeName;
		}

		// Token: 0x0600107F RID: 4223 RVA: 0x0007B8D9 File Offset: 0x0007A8D9
		public string GetInterfaceSeveralObjectsTypeName()
		{
			return AstralBorder.interfaceSeveralObjectsTypeName;
		}

		// Token: 0x06001080 RID: 4224 RVA: 0x0007B8E0 File Offset: 0x0007A8E0
		public bool ContainsText(string text, bool ignoreCase)
		{
			return false;
		}

		// Token: 0x06001081 RID: 4225 RVA: 0x0007B8E3 File Offset: 0x0007A8E3
		public string GetStatsForDBBrowse()
		{
			return string.Empty;
		}

		// Token: 0x06001082 RID: 4226 RVA: 0x0007B8EA File Offset: 0x0007A8EA
		public string GetSpecialStatsForDBBrowse()
		{
			return string.Empty;
		}

		// Token: 0x04000C21 RID: 3105
		private static readonly Color interfaceColor = Color.FromArgb(MapObjectCreationInfo.DefaultTransparentColorAlpha, Color.Orange);

		// Token: 0x04000C22 RID: 3106
		private static readonly string interfaceSingleObjectTypeName = Strings.SINGLE_ASTRAL_BORDER_TYPE_NAME;

		// Token: 0x04000C23 RID: 3107
		private static readonly string interfaceSeveralObjectsTypeName = Strings.SEVERAL_ASTRAL_BORDERS_TYPE_NAME;

		// Token: 0x04000C24 RID: 3108
		private static readonly double defaultStabilityRadius = 6000.0;

		// Token: 0x04000C25 RID: 3109
		private static readonly string defaultVisObject = "Editor/Map/SpecialObjects/AstralBorder/AstralBorder.(StaticObject).xdb";

		// Token: 0x04000C26 RID: 3110
		private double stabilityRadius = AstralBorder.defaultStabilityRadius;

		// Token: 0x02000156 RID: 342
		// (Invoke) Token: 0x06001085 RID: 4229
		public delegate void AstralBorderFieldChangedEvent<T>(AstralBorder astralBorder, ref T oldValue, ref T newValue);
	}
}
