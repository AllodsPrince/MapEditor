using System;
using MapEditor.Map;
using MapEditor.Map.MapObjects;
using MapEditor.Resources.Strings;

namespace MapEditor.Forms.TypedObjectsBrowser.DataProviders
{
	// Token: 0x020001B3 RID: 435
	public class ScriptZoneParams : TypedObjectParams
	{
		// Token: 0x060014C3 RID: 5315 RVA: 0x000953A8 File Offset: 0x000943A8
		public ScriptZoneParams(string continentName)
		{
			base.TypeNames.Add(Strings.SCRIPT_AREA_TYPE_CYLINDER);
			base.ObjectTypeNames.Add(Strings.SCRIPT_AREA_OBLECT_TYPE_ZONE);
			base.DBItemSources.Add(new DBItemSource(EditorEnvironment.DataFolder + Constants.ContinentFolder(continentName) + "ScriptZones/", ScriptArea.ScriptZoneDBType, false, true));
			base.SelectedTypeIndex = 0;
			base.SelectedObjectTypeIndex = 0;
		}

		// Token: 0x1700041D RID: 1053
		// (get) Token: 0x060014C4 RID: 5316 RVA: 0x00095415 File Offset: 0x00094415
		// (set) Token: 0x060014C5 RID: 5317 RVA: 0x00095422 File Offset: 0x00094422
		public ScriptAreaType SelectedScriptAreaType
		{
			get
			{
				if (base.SelectedTypeIndex == 0)
				{
					return ScriptAreaType.Cylinder;
				}
				return ScriptAreaType.Undefined;
			}
			set
			{
				if (value == ScriptAreaType.Cylinder)
				{
					base.SelectedTypeIndex = 0;
					return;
				}
				base.SelectedTypeIndex = -1;
			}
		}
	}
}
