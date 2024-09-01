using System;
using Tools.MapObjects;

namespace MapEditor.Map.MapObjects
{
	// Token: 0x02000173 RID: 371
	public class ExtendedSoundPack : SerializableMapObjectPack
	{
		// Token: 0x17000390 RID: 912
		// (get) Token: 0x060011ED RID: 4589 RVA: 0x00084586 File Offset: 0x00083586
		// (set) Token: 0x060011EE RID: 4590 RVA: 0x0008458E File Offset: 0x0008358E
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

		// Token: 0x17000391 RID: 913
		// (get) Token: 0x060011EF RID: 4591 RVA: 0x00084597 File Offset: 0x00083597
		// (set) Token: 0x060011F0 RID: 4592 RVA: 0x0008459F File Offset: 0x0008359F
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

		// Token: 0x060011F1 RID: 4593 RVA: 0x000845A8 File Offset: 0x000835A8
		public override void Pack(IMapObject mapObject)
		{
			base.Pack(mapObject);
			ExtendedSound extendedSound = mapObject as ExtendedSound;
			if (extendedSound != null)
			{
				this.centralSound = extendedSound.CentralSound;
				this.sideSound = extendedSound.SideSound;
			}
		}

		// Token: 0x060011F2 RID: 4594 RVA: 0x000845E0 File Offset: 0x000835E0
		public override void Unpack(IMapObject mapObject)
		{
			base.Unpack(mapObject);
			ExtendedSound extendedSound = mapObject as ExtendedSound;
			if (extendedSound != null)
			{
				extendedSound.CentralSound = this.centralSound;
				extendedSound.SideSound = this.sideSound;
			}
		}

		// Token: 0x04000CD2 RID: 3282
		private Sound centralSound;

		// Token: 0x04000CD3 RID: 3283
		private Sound sideSound;
	}
}
