using System;
using System.ComponentModel;
using System.Drawing.Design;

namespace MapEditor.Map.MapObjects
{
	// Token: 0x0200017C RID: 380
	public class ExtendedSoundParams
	{
		// Token: 0x06001243 RID: 4675 RVA: 0x00084D3D File Offset: 0x00083D3D
		public ExtendedSoundParams(Sound _centralSound, Sound _sideSound, string _name)
		{
			this.centralSound = _centralSound;
			this.sideSound = _sideSound;
			this.name = _name;
		}

		// Token: 0x170003B1 RID: 945
		// (get) Token: 0x06001244 RID: 4676 RVA: 0x00084D5A File Offset: 0x00083D5A
		// (set) Token: 0x06001245 RID: 4677 RVA: 0x00084D62 File Offset: 0x00083D62
		[Browsable(true)]
		[DisplayName("Name")]
		[Category("ExtendedSound")]
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

		// Token: 0x170003B2 RID: 946
		// (get) Token: 0x06001246 RID: 4678 RVA: 0x00084D6B File Offset: 0x00083D6B
		// (set) Token: 0x06001247 RID: 4679 RVA: 0x00084D73 File Offset: 0x00083D73
		[Browsable(true)]
		[Editor(typeof(SoundTypeEditor), typeof(UITypeEditor))]
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
				this.centralSound = value;
			}
		}

		// Token: 0x170003B3 RID: 947
		// (get) Token: 0x06001248 RID: 4680 RVA: 0x00084D7C File Offset: 0x00083D7C
		// (set) Token: 0x06001249 RID: 4681 RVA: 0x00084D84 File Offset: 0x00083D84
		[Browsable(true)]
		[Category("ExtendedSound")]
		[DisplayName("SideSound")]
		[TypeConverter(typeof(SoundConverter))]
		[Editor(typeof(SoundTypeEditor), typeof(UITypeEditor))]
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

		// Token: 0x04000CF0 RID: 3312
		private Sound centralSound;

		// Token: 0x04000CF1 RID: 3313
		private Sound sideSound;

		// Token: 0x04000CF2 RID: 3314
		private string name;
	}
}
