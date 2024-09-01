using System;
using System.Collections.Generic;
using Tools.MapObjects;

namespace MapEditor.Map.MapObjects
{
	// Token: 0x02000171 RID: 369
	public class ExtendedSoundSaveData
	{
		// Token: 0x1700038B RID: 907
		// (get) Token: 0x060011E0 RID: 4576 RVA: 0x0008447A File Offset: 0x0008347A
		public List<Position> BoundPoints
		{
			get
			{
				return this.boundPoints;
			}
		}

		// Token: 0x1700038C RID: 908
		// (get) Token: 0x060011E1 RID: 4577 RVA: 0x00084482 File Offset: 0x00083482
		public List<int> BoundIndices
		{
			get
			{
				return this._boundIndices;
			}
		}

		// Token: 0x1700038D RID: 909
		// (get) Token: 0x060011E2 RID: 4578 RVA: 0x0008448A File Offset: 0x0008348A
		// (set) Token: 0x060011E3 RID: 4579 RVA: 0x00084492 File Offset: 0x00083492
		public Sound CentralSound
		{
			get
			{
				return this.centralSound;
			}
			set
			{
				this.centralSound = value;
			}
		}

		// Token: 0x1700038E RID: 910
		// (get) Token: 0x060011E4 RID: 4580 RVA: 0x0008449B File Offset: 0x0008349B
		// (set) Token: 0x060011E5 RID: 4581 RVA: 0x000844A3 File Offset: 0x000834A3
		public Sound SideSound
		{
			get
			{
				return this.sideSound;
			}
			set
			{
				this.sideSound = value;
			}
		}

		// Token: 0x1700038F RID: 911
		// (get) Token: 0x060011E6 RID: 4582 RVA: 0x000844AC File Offset: 0x000834AC
		// (set) Token: 0x060011E7 RID: 4583 RVA: 0x000844B4 File Offset: 0x000834B4
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

		// Token: 0x060011E8 RID: 4584 RVA: 0x000844BD File Offset: 0x000834BD
		public ExtendedSoundSaveData(List<Position> _boundPoints, List<int> _boundIndicies, Sound _centralSound, Sound _sideSound, string _name)
		{
			this.boundPoints = _boundPoints;
			this._boundIndices = _boundIndicies;
			this.centralSound = _centralSound;
			this.sideSound = _sideSound;
			this.name = _name;
		}

		// Token: 0x060011E9 RID: 4585 RVA: 0x000844EA File Offset: 0x000834EA
		public ExtendedSoundSaveData()
		{
			this.boundPoints = new List<Position>();
			this._boundIndices = new List<int>();
		}

		// Token: 0x04000CCD RID: 3277
		private readonly List<Position> boundPoints;

		// Token: 0x04000CCE RID: 3278
		private readonly List<int> _boundIndices;

		// Token: 0x04000CCF RID: 3279
		private Sound centralSound;

		// Token: 0x04000CD0 RID: 3280
		private Sound sideSound;

		// Token: 0x04000CD1 RID: 3281
		private string name;
	}
}
