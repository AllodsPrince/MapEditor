using System;
using Tools.Progress;

namespace MapEditor.Map.MapCheckers
{
	// Token: 0x02000042 RID: 66
	public class MapChecker : IMapChecker
	{
		// Token: 0x1700008B RID: 139
		// (get) Token: 0x060003BC RID: 956 RVA: 0x000200E3 File Offset: 0x0001F0E3
		// (set) Token: 0x060003BD RID: 957 RVA: 0x000200EB File Offset: 0x0001F0EB
		public string Name
		{
			get
			{
				return this.name;
			}
			set
			{
				this.name = value;
			}
		}

		// Token: 0x1700008C RID: 140
		// (get) Token: 0x060003BE RID: 958 RVA: 0x000200F4 File Offset: 0x0001F0F4
		// (set) Token: 0x060003BF RID: 959 RVA: 0x000200FC File Offset: 0x0001F0FC
		public MapCheckerStatus Status
		{
			get
			{
				return this.status;
			}
			set
			{
				this.status = value;
			}
		}

		// Token: 0x1700008D RID: 141
		// (get) Token: 0x060003C0 RID: 960 RVA: 0x00020105 File Offset: 0x0001F105
		// (set) Token: 0x060003C1 RID: 961 RVA: 0x0002010D File Offset: 0x0001F10D
		public string ShortDescription
		{
			get
			{
				return this.shortDescription;
			}
			set
			{
				this.shortDescription = value;
			}
		}

		// Token: 0x1700008E RID: 142
		// (get) Token: 0x060003C2 RID: 962 RVA: 0x00020116 File Offset: 0x0001F116
		// (set) Token: 0x060003C3 RID: 963 RVA: 0x0002011E File Offset: 0x0001F11E
		public string ShortResult
		{
			get
			{
				return this.shortResult;
			}
			set
			{
				this.shortResult = value;
			}
		}

		// Token: 0x1700008F RID: 143
		// (get) Token: 0x060003C4 RID: 964 RVA: 0x00020127 File Offset: 0x0001F127
		// (set) Token: 0x060003C5 RID: 965 RVA: 0x0002012F File Offset: 0x0001F12F
		public string ShortInfo
		{
			get
			{
				return this.shortInfo;
			}
			set
			{
				this.shortInfo = value;
			}
		}

		// Token: 0x17000090 RID: 144
		// (get) Token: 0x060003C6 RID: 966 RVA: 0x00020138 File Offset: 0x0001F138
		// (set) Token: 0x060003C7 RID: 967 RVA: 0x00020140 File Offset: 0x0001F140
		public string LongDescription
		{
			get
			{
				return this.longDescription;
			}
			set
			{
				this.longDescription = value;
			}
		}

		// Token: 0x17000091 RID: 145
		// (get) Token: 0x060003C8 RID: 968 RVA: 0x00020149 File Offset: 0x0001F149
		// (set) Token: 0x060003C9 RID: 969 RVA: 0x00020151 File Offset: 0x0001F151
		public string LongResult
		{
			get
			{
				return this.longResult;
			}
			set
			{
				this.longResult = value;
			}
		}

		// Token: 0x17000092 RID: 146
		// (get) Token: 0x060003CA RID: 970 RVA: 0x0002015A File Offset: 0x0001F15A
		// (set) Token: 0x060003CB RID: 971 RVA: 0x00020162 File Offset: 0x0001F162
		public LongInfoViewNode LongInfoView
		{
			get
			{
				return this.longInfoView;
			}
			set
			{
				this.longInfoView = value;
			}
		}

		// Token: 0x17000093 RID: 147
		// (get) Token: 0x060003CC RID: 972 RVA: 0x0002016B File Offset: 0x0001F16B
		// (set) Token: 0x060003CD RID: 973 RVA: 0x00020173 File Offset: 0x0001F173
		public string LongInfoText
		{
			get
			{
				return this.longInfoText;
			}
			set
			{
				this.longInfoText = value;
			}
		}

		// Token: 0x060003CE RID: 974 RVA: 0x0002017C File Offset: 0x0001F17C
		public virtual int GetCheckProgressSteps()
		{
			return 1;
		}

		// Token: 0x060003CF RID: 975 RVA: 0x0002017F File Offset: 0x0001F17F
		public virtual void Check(MapEditorMap map, IProgressContainer progressContainer)
		{
		}

		// Token: 0x060003D0 RID: 976 RVA: 0x00020181 File Offset: 0x0001F181
		public virtual int GetFixProgressSteps()
		{
			return 0;
		}

		// Token: 0x060003D1 RID: 977 RVA: 0x00020184 File Offset: 0x0001F184
		public virtual void Fix(MapEditorMap map, IProgressContainer progressContainer)
		{
		}

		// Token: 0x040002A1 RID: 673
		private string name = string.Empty;

		// Token: 0x040002A2 RID: 674
		private MapCheckerStatus status;

		// Token: 0x040002A3 RID: 675
		private string shortDescription = string.Empty;

		// Token: 0x040002A4 RID: 676
		private string shortResult = string.Empty;

		// Token: 0x040002A5 RID: 677
		private string shortInfo = string.Empty;

		// Token: 0x040002A6 RID: 678
		private string longDescription = string.Empty;

		// Token: 0x040002A7 RID: 679
		private string longResult = string.Empty;

		// Token: 0x040002A8 RID: 680
		private LongInfoViewNode longInfoView;

		// Token: 0x040002A9 RID: 681
		private string longInfoText;
	}
}
