using System;
using MapEditor.Resources.Strings;

namespace MapEditor.Forms.TypedObjectsBrowser.DataProviders
{
	// Token: 0x020001B2 RID: 434
	public class ClientSpawnTableParams : TypedObjectParams
	{
		// Token: 0x060014C0 RID: 5312 RVA: 0x00095308 File Offset: 0x00094308
		public ClientSpawnTableParams()
		{
			base.ObjectTypeNames.Add(Strings.SPAWN_POINT_OBJECT_TYPE_SINGLE_MOB);
			base.ObjectTypeNames.Add(Strings.SPAWN_POINT_OBJECT_TYPE_SINGLE_DEVICE);
			base.DBItemSources.Add(new DBItemSource(EditorEnvironment.DataFolder, "VisualMob", false, true));
			base.DBItemSources.Add(new DBItemSource(EditorEnvironment.DataFolder, "gameMechanics.world.device.DeviceResource", true, true));
		}

		// Token: 0x1700041C RID: 1052
		// (get) Token: 0x060014C1 RID: 5313 RVA: 0x00095373 File Offset: 0x00094373
		// (set) Token: 0x060014C2 RID: 5314 RVA: 0x00095389 File Offset: 0x00094389
		public string SelectedSpawnPointType
		{
			get
			{
				if (base.SelectedObjectTypeIndex == 1)
				{
					return "gameMechanics.world.device.DeviceResource";
				}
				return "VisualMob";
			}
			set
			{
				if (value == "gameMechanics.world.device.DeviceResource")
				{
					base.SelectedObjectTypeIndex = 1;
					return;
				}
				base.SelectedObjectTypeIndex = 0;
			}
		}
	}
}
