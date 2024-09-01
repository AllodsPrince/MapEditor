using System;
using Db;

namespace MapEditor.Forms.MobScripts
{
	// Token: 0x0200001A RID: 26
	public class CustomFieldsWrapper
	{
		// Token: 0x06000233 RID: 563 RVA: 0x00019537 File Offset: 0x00018537
		public CustomFieldsWrapper()
		{
			this.Load(null);
		}

		// Token: 0x17000021 RID: 33
		// (get) Token: 0x06000234 RID: 564 RVA: 0x00019546 File Offset: 0x00018546
		// (set) Token: 0x06000235 RID: 565 RVA: 0x0001954E File Offset: 0x0001854E
		public float MobHealth
		{
			get
			{
				return this.mobHealth;
			}
			set
			{
				this.mobHealth = value;
			}
		}

		// Token: 0x17000022 RID: 34
		// (get) Token: 0x06000236 RID: 566 RVA: 0x00019557 File Offset: 0x00018557
		// (set) Token: 0x06000237 RID: 567 RVA: 0x0001955F File Offset: 0x0001855F
		public float EnemyHealth
		{
			get
			{
				return this.enemyHealth;
			}
			set
			{
				this.enemyHealth = value;
			}
		}

		// Token: 0x17000023 RID: 35
		// (get) Token: 0x06000238 RID: 568 RVA: 0x00019568 File Offset: 0x00018568
		// (set) Token: 0x06000239 RID: 569 RVA: 0x00019570 File Offset: 0x00018570
		public float IdlePeriodMin
		{
			get
			{
				return this.idlePeriodMin;
			}
			set
			{
				this.idlePeriodMin = value;
			}
		}

		// Token: 0x17000024 RID: 36
		// (get) Token: 0x0600023A RID: 570 RVA: 0x00019579 File Offset: 0x00018579
		// (set) Token: 0x0600023B RID: 571 RVA: 0x00019581 File Offset: 0x00018581
		public float IdlePeriodMax
		{
			get
			{
				return this.idlePeriodMax;
			}
			set
			{
				this.idlePeriodMax = value;
			}
		}

		// Token: 0x17000025 RID: 37
		// (get) Token: 0x0600023C RID: 572 RVA: 0x0001958A File Offset: 0x0001858A
		// (set) Token: 0x0600023D RID: 573 RVA: 0x00019592 File Offset: 0x00018592
		public bool AutoplayDeathAnim
		{
			get
			{
				return this.autoplayDeathAnim;
			}
			set
			{
				this.autoplayDeathAnim = value;
			}
		}

		// Token: 0x0600023E RID: 574 RVA: 0x0001959C File Offset: 0x0001859C
		public void Load(IObjMan objMan)
		{
			if (objMan != null)
			{
				objMan.GetValue("mobHealth.health", out this.mobHealth);
				objMan.GetValue("enemyHealth.health", out this.enemyHealth);
				objMan.GetValue("idleScriptParams.idlePeriodMin", out this.idlePeriodMin);
				objMan.GetValue("idleScriptParams.idlePeriodMax", out this.idlePeriodMax);
				objMan.GetValue("deathScriptParams.autoplayDeathAnim", out this.autoplayDeathAnim);
				return;
			}
			this.mobHealth = 0.2f;
			this.enemyHealth = 0.2f;
			this.idlePeriodMin = 20f;
			this.idlePeriodMax = 30f;
			this.autoplayDeathAnim = true;
		}

		// Token: 0x0600023F RID: 575 RVA: 0x00019638 File Offset: 0x00018638
		public void Save(IObjMan objMan)
		{
			if (objMan != null)
			{
				objMan.SetValue("mobHealth.health", this.mobHealth);
				objMan.SetValue("enemyHealth.health", this.enemyHealth);
				objMan.SetValue("idleScriptParams.idlePeriodMin", this.idlePeriodMin);
				objMan.SetValue("idleScriptParams.idlePeriodMax", this.idlePeriodMax);
				objMan.SetValue("deathScriptParams.autoplayDeathAnim", this.autoplayDeathAnim);
			}
		}

		// Token: 0x04000209 RID: 521
		private const float helthDefaultVal = 0.2f;

		// Token: 0x0400020A RID: 522
		private const float periodMinDefaultVal = 20f;

		// Token: 0x0400020B RID: 523
		private const float periodMaxDefaultVal = 30f;

		// Token: 0x0400020C RID: 524
		private const bool autoPlayDefaultVal = true;

		// Token: 0x0400020D RID: 525
		private float mobHealth;

		// Token: 0x0400020E RID: 526
		private float enemyHealth;

		// Token: 0x0400020F RID: 527
		private float idlePeriodMin;

		// Token: 0x04000210 RID: 528
		private float idlePeriodMax;

		// Token: 0x04000211 RID: 529
		private bool autoplayDeathAnim;
	}
}
