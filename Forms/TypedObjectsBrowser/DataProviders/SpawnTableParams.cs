using System;
using MapEditor.Map;
using MapEditor.Map.MapObjects;
using MapEditor.Resources.Strings;

namespace MapEditor.Forms.TypedObjectsBrowser.DataProviders
{
	// Token: 0x020001B1 RID: 433
	public class SpawnTableParams : TypedObjectParams
	{
		// Token: 0x060014BB RID: 5307 RVA: 0x00094F5C File Offset: 0x00093F5C
		public SpawnTableParams(string _continentName, ContinentType _continentType)
		{
			this.continentName = _continentName;
			this.continentType = _continentType;
			if (this.continentType == ContinentType.AstralHub)
			{
				base.TypeNames.Add(Strings.SPAWN_POINT_TYPE_GUARD);
				base.ObjectTypeNames.Add(Strings.SPAWN_POINT_OBJECT_TYPE_ASTRAL_MOB);
				base.ObjectTypeNames.Add(Strings.SPAWN_POINT_OBJECT_TYPE_SINGLE_DEVICE);
				base.ObjectTypeNames.Add(Strings.SPAWN_POINT_OBJECT_TYPE_ASTRAL_WRECK);
				base.ObjectTypeNames.Add(Strings.SPAWN_POINT_OBJECT_TYPE_ASTRAL_TELEPORT);
				base.DBItemSources.Add(new DBItemSource(EditorEnvironment.DataFolder, SpawnPoint.AstralMobWorldDBType, false, true));
				base.DBItemSources.Add(new DBItemSource(EditorEnvironment.DataFolder, SpawnPoint.DeviceDBType, true, true));
				base.DBItemSources.Add(new DBItemSource(EditorEnvironment.DataFolder, SpawnPoint.AstralWreckDBType, false, true));
				base.DBItemSources.Add(new DBItemSource(EditorEnvironment.DataFolder, SpawnPoint.AstralTeleportDBType, false, true));
				return;
			}
			if (this.continentType == ContinentType.Continent)
			{
				base.TypeNames.Add(Strings.SPAWN_POINT_TYPE_GUARD);
				base.TypeNames.Add(Strings.SPAWN_POINT_TYPE_CIRCLE);
				base.TypeNames.Add(Strings.SPAWN_POINT_TYPE_ELLIPSE);
				base.TypeNames.Add(Strings.SPAWN_POINT_TYPE_PATROL);
				base.TypeNames.Add(Strings.SPAWN_POINT_TYPE_SPAWN_CIRCLE);
				base.ObjectTypeNames.Add(Strings.SPAWN_POINT_OBJECT_TYPE_TABLE);
				base.ObjectTypeNames.Add(Strings.SPAWN_POINT_OBJECT_TYPE_SINGLE_MOB);
				base.ObjectTypeNames.Add(Strings.SPAWN_POINT_OBJECT_TYPE_SINGLE_DEVICE);
				base.DBItemSources.Add(new DBItemSource(EditorEnvironment.DataFolder + Constants.ContinentFolder(this.continentName) + SpawnPoint.SpawnTableFolder, SpawnPoint.SpawnTableDBType, false, true));
				base.DBItemSources.Add(new DBItemSource(EditorEnvironment.DataFolder, SpawnPoint.MobWorldDBType, false, true));
				base.DBItemSources.Add(new DBItemSource(EditorEnvironment.DataFolder, SpawnPoint.DeviceDBType, true, true));
			}
		}

		// Token: 0x1700041A RID: 1050
		// (get) Token: 0x060014BC RID: 5308 RVA: 0x0009514C File Offset: 0x0009414C
		// (set) Token: 0x060014BD RID: 5309 RVA: 0x000951AC File Offset: 0x000941AC
		public SpawnPointType SelectedSpawnPointType
		{
			get
			{
				if (this.continentType == ContinentType.AstralHub)
				{
					if (base.SelectedTypeIndex == 0)
					{
						return SpawnPointType.Guard;
					}
				}
				else if (this.continentType == ContinentType.Continent)
				{
					if (base.SelectedTypeIndex == 0)
					{
						return SpawnPointType.Guard;
					}
					if (base.SelectedTypeIndex == 1)
					{
						return SpawnPointType.Circle;
					}
					if (base.SelectedTypeIndex == 2)
					{
						return SpawnPointType.Ellipse;
					}
					if (base.SelectedTypeIndex == 3)
					{
						return SpawnPointType.Patrol;
					}
					if (base.SelectedTypeIndex == 4)
					{
						return SpawnPointType.SpawnCircle;
					}
				}
				return SpawnPointType.Undefined;
			}
			set
			{
				if (this.continentType == ContinentType.AstralHub)
				{
					if (value == SpawnPointType.Guard)
					{
						base.SelectedTypeIndex = 0;
						return;
					}
				}
				else if (this.continentType == ContinentType.Continent)
				{
					if (value == SpawnPointType.Guard)
					{
						base.SelectedTypeIndex = 0;
						return;
					}
					if (value == SpawnPointType.Circle)
					{
						base.SelectedTypeIndex = 1;
						return;
					}
					if (value == SpawnPointType.Ellipse)
					{
						base.SelectedTypeIndex = 2;
						return;
					}
					if (value == SpawnPointType.Patrol)
					{
						base.SelectedTypeIndex = 3;
						return;
					}
					if (value == SpawnPointType.SpawnCircle)
					{
						base.SelectedTypeIndex = 4;
						return;
					}
				}
				base.SelectedTypeIndex = -1;
			}
		}

		// Token: 0x1700041B RID: 1051
		// (get) Token: 0x060014BE RID: 5310 RVA: 0x0009521C File Offset: 0x0009421C
		// (set) Token: 0x060014BF RID: 5311 RVA: 0x0009528C File Offset: 0x0009428C
		public SpawnTableType SelectedSpawnTableType
		{
			get
			{
				if (this.continentType == ContinentType.AstralHub)
				{
					if (base.SelectedObjectTypeIndex == 0)
					{
						return SpawnTableType.AstralMob;
					}
					if (base.SelectedObjectTypeIndex == 1)
					{
						return SpawnTableType.SingleDevice;
					}
					if (base.SelectedObjectTypeIndex == 2)
					{
						return SpawnTableType.AstralWreck;
					}
					if (base.SelectedObjectTypeIndex == 3)
					{
						return SpawnTableType.AstralTeleport;
					}
				}
				else if (this.continentType == ContinentType.Continent)
				{
					if (base.SelectedObjectTypeIndex == 0)
					{
						return SpawnTableType.Table;
					}
					if (base.SelectedObjectTypeIndex == 1)
					{
						return SpawnTableType.SingleMob;
					}
					if (base.SelectedObjectTypeIndex == 2)
					{
						return SpawnTableType.SingleDevice;
					}
				}
				return SpawnPoint.DefaultSpawnTableType;
			}
			set
			{
				if (this.continentType == ContinentType.AstralHub)
				{
					if (value == SpawnTableType.AstralMob)
					{
						base.SelectedObjectTypeIndex = 0;
						return;
					}
					if (value == SpawnTableType.SingleDevice)
					{
						base.SelectedObjectTypeIndex = 1;
						return;
					}
					if (value == SpawnTableType.AstralWreck)
					{
						base.SelectedObjectTypeIndex = 2;
						return;
					}
					if (value == SpawnTableType.AstralTeleport)
					{
						base.SelectedObjectTypeIndex = 3;
						return;
					}
				}
				else if (this.continentType == ContinentType.Continent)
				{
					if (value == SpawnTableType.Table)
					{
						base.SelectedObjectTypeIndex = 0;
						return;
					}
					if (value == SpawnTableType.SingleMob)
					{
						base.SelectedObjectTypeIndex = 1;
						return;
					}
					if (value == SpawnTableType.SingleDevice)
					{
						base.SelectedObjectTypeIndex = 2;
						return;
					}
				}
				base.SelectedObjectTypeIndex = 0;
			}
		}

		// Token: 0x04000E84 RID: 3716
		private readonly string continentName = string.Empty;

		// Token: 0x04000E85 RID: 3717
		private readonly ContinentType continentType;
	}
}
