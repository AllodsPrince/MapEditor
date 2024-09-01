using System;
using System.CodeDom.Compiler;
using System.Configuration;
using System.Runtime.CompilerServices;

namespace MapEditor.Properties
{
	// Token: 0x020001F0 RID: 496
	[CompilerGenerated]
	[GeneratedCode("Microsoft.VisualStudio.Editors.SettingsDesigner.SettingsSingleFileGenerator", "8.0.0.0")]
	internal sealed partial class Settings : ApplicationSettingsBase
	{
		// Token: 0x170005F8 RID: 1528
		// (get) Token: 0x060018D6 RID: 6358 RVA: 0x000A52C5 File Offset: 0x000A42C5
		public static Settings Default
		{
			get
			{
				return Settings.defaultInstance;
			}
		}

		// Token: 0x0400101A RID: 4122
		private static Settings defaultInstance = (Settings)SettingsBase.Synchronized(new Settings());
	}
}
