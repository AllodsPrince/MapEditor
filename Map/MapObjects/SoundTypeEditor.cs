using System;
using System.ComponentModel;
using System.Windows.Forms.Design;
using System.Windows.Forms.UITypeEditors.FMODBrowser;

namespace MapEditor.Map.MapObjects
{
	// Token: 0x02000170 RID: 368
	public class SoundTypeEditor : FMODTypeEditor3D
	{
		// Token: 0x060011DE RID: 4574 RVA: 0x00084404 File Offset: 0x00083404
		public override object EditValue(ITypeDescriptorContext context, IServiceProvider provider, object value)
		{
			if (provider != null && provider.GetService(typeof(IWindowsFormsEditorService)) != null)
			{
				Sound sound = value as Sound;
				if (sound != null)
				{
					FMODBrowser fmodBrowser = this.CreateFMODBrowser(sound.Project, sound.Name);
					if (fmodBrowser != null)
					{
						fmodBrowser.ShowDialog();
						if (fmodBrowser.IsEventExists)
						{
							string soundProject = fmodBrowser.FMODProjectName;
							string soundName = fmodBrowser.FMODEventName;
							return new Sound(soundProject, soundName);
						}
					}
					return Sound.Empty;
				}
			}
			return value;
		}
	}
}
