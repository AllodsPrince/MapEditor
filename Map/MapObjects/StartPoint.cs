using System;
using System.ComponentModel;
using System.Drawing;
using MapEditor.Resources.Strings;
using Tools.MapObjects;

namespace MapEditor.Map.MapObjects
{
	// Token: 0x0200027D RID: 637
	public class StartPoint : MapObject, IMapObjectInterfaceExtention
	{
		// Token: 0x140000D4 RID: 212
		// (add) Token: 0x06001E41 RID: 7745 RVA: 0x000C58AD File Offset: 0x000C48AD
		// (remove) Token: 0x06001E42 RID: 7746 RVA: 0x000C58C4 File Offset: 0x000C48C4
		public static event StartPoint.StartPointFieldChangedEvent<string> CharacterChanged;

		// Token: 0x170006F8 RID: 1784
		// (get) Token: 0x06001E43 RID: 7747 RVA: 0x000C58DB File Offset: 0x000C48DB
		public static Color InterfaceColor
		{
			get
			{
				return StartPoint.interfaceColor;
			}
		}

		// Token: 0x170006F9 RID: 1785
		// (get) Token: 0x06001E44 RID: 7748 RVA: 0x000C58E2 File Offset: 0x000C48E2
		public static string InterfaceSingleObjectTypeName
		{
			get
			{
				return StartPoint.interfaceSingleObjectTypeName;
			}
		}

		// Token: 0x170006FA RID: 1786
		// (get) Token: 0x06001E45 RID: 7749 RVA: 0x000C58E9 File Offset: 0x000C48E9
		public static string InterfaceSeveralObjectsTypeName
		{
			get
			{
				return StartPoint.interfaceSeveralObjectsTypeName;
			}
		}

		// Token: 0x170006FB RID: 1787
		// (get) Token: 0x06001E46 RID: 7750 RVA: 0x000C58F0 File Offset: 0x000C48F0
		public static string DefaultVisObject
		{
			get
			{
				return StartPoint.defaultVisObject;
			}
		}

		// Token: 0x170006FC RID: 1788
		// (get) Token: 0x06001E47 RID: 7751 RVA: 0x000C58F7 File Offset: 0x000C48F7
		public static string CharacterFolder
		{
			get
			{
				return StartPoint.characterFolder;
			}
		}

		// Token: 0x170006FD RID: 1789
		// (get) Token: 0x06001E48 RID: 7752 RVA: 0x000C58FE File Offset: 0x000C48FE
		public static string CharacterDBType
		{
			get
			{
				return StartPoint.characterDBType;
			}
		}

		// Token: 0x06001E49 RID: 7753 RVA: 0x000C5905 File Offset: 0x000C4905
		public StartPoint(int _id, MapObjectType _type, ICollisionMap _collisionMap) : base(_id, _type, _collisionMap)
		{
			this.character = _type.Stats;
		}

		// Token: 0x170006FE RID: 1790
		// (get) Token: 0x06001E4A RID: 7754 RVA: 0x000C5928 File Offset: 0x000C4928
		// (set) Token: 0x06001E4B RID: 7755 RVA: 0x000C5930 File Offset: 0x000C4930
		[Category("StartPoint")]
		[Browsable(true)]
		public string Character
		{
			get
			{
				return this.character;
			}
			set
			{
				if (this.character != value && base.InvokeChanging(null))
				{
					string oldCharacter = this.character;
					this.character = value;
					base.SetNewStats(this.character);
					base.InvokeChanged();
					if (base.Active && StartPoint.CharacterChanged != null)
					{
						StartPoint.CharacterChanged(this, ref oldCharacter, ref this.character);
					}
				}
			}
		}

		// Token: 0x170006FF RID: 1791
		// (get) Token: 0x06001E4C RID: 7756 RVA: 0x000C5996 File Offset: 0x000C4996
		[Browsable(false)]
		public override string SceneName
		{
			get
			{
				return StartPoint.defaultVisObject;
			}
		}

		// Token: 0x06001E4D RID: 7757 RVA: 0x000C59A0 File Offset: 0x000C49A0
		public override IMapObject Clone(int newID, bool newTemporary, bool newActive)
		{
			StartPoint startPoint = new StartPoint(newID, new MapObjectType(base.Type.Type, this.character), base.CollisionMap);
			base.CopyTo(startPoint, newTemporary, newActive);
			return startPoint;
		}

		// Token: 0x06001E4E RID: 7758 RVA: 0x000C59E0 File Offset: 0x000C49E0
		public override IMapObjectPack Pack()
		{
			StartPointPack startPointPack = new StartPointPack();
			startPointPack.Pack(this);
			return startPointPack;
		}

		// Token: 0x06001E4F RID: 7759 RVA: 0x000C59FB File Offset: 0x000C49FB
		public Color GetInterfaceColor()
		{
			return StartPoint.interfaceColor;
		}

		// Token: 0x06001E50 RID: 7760 RVA: 0x000C5A02 File Offset: 0x000C4A02
		public string GetInterfaceSingleObjectTypeName()
		{
			return StartPoint.interfaceSingleObjectTypeName;
		}

		// Token: 0x06001E51 RID: 7761 RVA: 0x000C5A09 File Offset: 0x000C4A09
		public string GetInterfaceSeveralObjectsTypeName()
		{
			return StartPoint.interfaceSeveralObjectsTypeName;
		}

		// Token: 0x06001E52 RID: 7762 RVA: 0x000C5A10 File Offset: 0x000C4A10
		public bool ContainsText(string text, bool ignoreCase)
		{
			if (string.IsNullOrEmpty(text))
			{
				return true;
			}
			if (string.IsNullOrEmpty(this.character))
			{
				return false;
			}
			if (ignoreCase)
			{
				return this.character.ToLower().Contains(text.ToLower());
			}
			return this.character.Contains(text);
		}

		// Token: 0x06001E53 RID: 7763 RVA: 0x000C5A5C File Offset: 0x000C4A5C
		public string GetStatsForDBBrowse()
		{
			return this.character;
		}

		// Token: 0x06001E54 RID: 7764 RVA: 0x000C5A64 File Offset: 0x000C4A64
		public string GetSpecialStatsForDBBrowse()
		{
			return string.Empty;
		}

		// Token: 0x040012FC RID: 4860
		private static readonly Color interfaceColor = Color.FromArgb(MapObjectCreationInfo.DefaultTransparentColorAlpha, Color.LightSkyBlue);

		// Token: 0x040012FD RID: 4861
		private static readonly string interfaceSingleObjectTypeName = Strings.SINGLE_START_POINT_TYPE_NAME;

		// Token: 0x040012FE RID: 4862
		private static readonly string interfaceSeveralObjectsTypeName = Strings.SEVERAL_START_POINTS_TYPE_NAME;

		// Token: 0x040012FF RID: 4863
		private static readonly string defaultVisObject = "Editor/Map/SpecialObjects/StartPoint/StartPoint.(StaticObject).xdb";

		// Token: 0x04001300 RID: 4864
		private static readonly string characterFolder = "Mechanics/Characters";

		// Token: 0x04001301 RID: 4865
		private static readonly string characterDBType = "gameMechanics.world.avatar.Character";

		// Token: 0x04001302 RID: 4866
		private string character = string.Empty;

		// Token: 0x0200027E RID: 638
		// (Invoke) Token: 0x06001E57 RID: 7767
		public delegate void StartPointFieldChangedEvent<T>(StartPoint startPoint, ref T oldValue, ref T newValue);
	}
}
