using System;
using Tools.LinkContainer;

namespace MapEditor.Map.MapObjects
{
	// Token: 0x02000251 RID: 593
	public class PatrolLinkData : ILinkData
	{
		// Token: 0x06001C39 RID: 7225 RVA: 0x000B621B File Offset: 0x000B521B
		public PatrolLinkData()
		{
		}

		// Token: 0x140000CC RID: 204
		// (add) Token: 0x06001C3A RID: 7226 RVA: 0x000B6235 File Offset: 0x000B5235
		// (remove) Token: 0x06001C3B RID: 7227 RVA: 0x000B624C File Offset: 0x000B524C
		public static event PatrolLinkData.PatrolLinkDataFieldChangedEvent<PatrolNodeLinkType> TypeChanged;

		// Token: 0x140000CD RID: 205
		// (add) Token: 0x06001C3C RID: 7228 RVA: 0x000B6263 File Offset: 0x000B5263
		// (remove) Token: 0x06001C3D RID: 7229 RVA: 0x000B627A File Offset: 0x000B527A
		public static event PatrolLinkData.PatrolLinkDataFieldChangedEvent<string> StartChanged;

		// Token: 0x140000CE RID: 206
		// (add) Token: 0x06001C3E RID: 7230 RVA: 0x000B6291 File Offset: 0x000B5291
		// (remove) Token: 0x06001C3F RID: 7231 RVA: 0x000B62A8 File Offset: 0x000B52A8
		public static event PatrolLinkData.PatrolLinkDataFieldChangedEvent<int> WeightChanged;

		// Token: 0x06001C40 RID: 7232 RVA: 0x000B62BF File Offset: 0x000B52BF
		public PatrolLinkData(PatrolNodeLinkType _type, string _start, int _weight)
		{
			this.type = _type;
			this.start = _start;
			this.weight = _weight;
		}

		// Token: 0x170006A3 RID: 1699
		// (get) Token: 0x06001C41 RID: 7233 RVA: 0x000B62EE File Offset: 0x000B52EE
		// (set) Token: 0x06001C42 RID: 7234 RVA: 0x000B62F8 File Offset: 0x000B52F8
		public PatrolNodeLinkType Type
		{
			get
			{
				return this.type;
			}
			set
			{
				if (this.type != value)
				{
					PatrolNodeLinkType oldType = this.type;
					this.type = value;
					if (PatrolLinkData.TypeChanged != null)
					{
						PatrolLinkData.TypeChanged(this, ref oldType, ref this.type);
					}
				}
			}
		}

		// Token: 0x170006A4 RID: 1700
		// (get) Token: 0x06001C43 RID: 7235 RVA: 0x000B6336 File Offset: 0x000B5336
		// (set) Token: 0x06001C44 RID: 7236 RVA: 0x000B6340 File Offset: 0x000B5340
		public string Start
		{
			get
			{
				return this.start;
			}
			set
			{
				if (this.start != value)
				{
					string oldStart = this.start;
					this.start = value;
					if (PatrolLinkData.StartChanged != null)
					{
						PatrolLinkData.StartChanged(this, ref oldStart, ref this.start);
					}
				}
			}
		}

		// Token: 0x170006A5 RID: 1701
		// (get) Token: 0x06001C45 RID: 7237 RVA: 0x000B6383 File Offset: 0x000B5383
		// (set) Token: 0x06001C46 RID: 7238 RVA: 0x000B638C File Offset: 0x000B538C
		public int Weight
		{
			get
			{
				return this.weight;
			}
			set
			{
				if (this.weight != value)
				{
					int oldWeight = this.weight;
					this.weight = value;
					if (PatrolLinkData.WeightChanged != null)
					{
						PatrolLinkData.WeightChanged(this, ref oldWeight, ref this.weight);
					}
				}
			}
		}

		// Token: 0x06001C47 RID: 7239 RVA: 0x000B63CA File Offset: 0x000B53CA
		public ILinkData Clone()
		{
			return new PatrolLinkData(this.type, this.start, this.weight);
		}

		// Token: 0x04001229 RID: 4649
		private PatrolNodeLinkType type;

		// Token: 0x0400122A RID: 4650
		private string start = string.Empty;

		// Token: 0x0400122B RID: 4651
		private int weight = 1;

		// Token: 0x02000252 RID: 594
		// (Invoke) Token: 0x06001C49 RID: 7241
		public delegate void PatrolLinkDataFieldChangedEvent<T>(PatrolLinkData patrolLinkData, ref T oldValue, ref T newValue);
	}
}
