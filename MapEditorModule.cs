using System;
using Tools.EditorModule;

namespace MapEditor
{
	// Token: 0x02000021 RID: 33
	internal class MapEditorModule : ModuleInterface
	{
		// Token: 0x17000030 RID: 48
		// (get) Token: 0x060002A7 RID: 679 RVA: 0x0001D15E File Offset: 0x0001C15E
		internal static MapEditorModule Module
		{
			get
			{
				if (MapEditorModule.module == null)
				{
					MapEditorModule.module = new MapEditorModule();
				}
				return MapEditorModule.module;
			}
		}

		// Token: 0x060002A8 RID: 680 RVA: 0x0001D176 File Offset: 0x0001C176
		public object CreateMainForm(string[] args)
		{
			if (this.mainForm == null)
			{
				this.mainForm = new MainForm();
			}
			return this.mainForm;
		}

		// Token: 0x060002A9 RID: 681 RVA: 0x0001D191 File Offset: 0x0001C191
		public bool Startup(string[] args)
		{
			return true;
		}

		// Token: 0x04000242 RID: 578
		private static MapEditorModule module;

		// Token: 0x04000243 RID: 579
		private MainForm mainForm;
	}
}
