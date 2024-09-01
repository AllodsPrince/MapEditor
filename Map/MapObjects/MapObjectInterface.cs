using System;
using System.Collections.Generic;
using System.Drawing;
using Tools.MapObjects;

namespace MapEditor.Map.MapObjects
{
	// Token: 0x02000074 RID: 116
	public class MapObjectInterface
	{
		// Token: 0x060005B5 RID: 1461 RVA: 0x0002F888 File Offset: 0x0002E888
		public static List<string> GetMapObjectTypeList(bool single)
		{
			if (MapObjectInterface.mapObjectTypeList == null)
			{
				MapObjectInterface.mapObjectTypeList = new List<string>();
				for (int type = MapObjectFactory.Type.FirstSimpleType; type <= MapObjectFactory.Type.LastSimpleType; type++)
				{
					if (single)
					{
						MapObjectInterface.mapObjectTypeList.Add(MapObjectFactory.Type.TypeToSingleObjectTypeName(type));
					}
					else
					{
						MapObjectInterface.mapObjectTypeList.Add(MapObjectFactory.Type.TypeToSeveralObjectsTypeName(type));
					}
				}
			}
			return MapObjectInterface.mapObjectTypeList;
		}

		// Token: 0x060005B6 RID: 1462 RVA: 0x0002F8E4 File Offset: 0x0002E8E4
		public static Color GetInterfaceColor(IMapObject mapObject)
		{
			IMapObjectInterfaceExtention mapObjectInterfaceExtention = mapObject as IMapObjectInterfaceExtention;
			if (mapObjectInterfaceExtention != null)
			{
				return mapObjectInterfaceExtention.GetInterfaceColor();
			}
			return StaticObject.InterfaceColor;
		}

		// Token: 0x060005B7 RID: 1463 RVA: 0x0002F908 File Offset: 0x0002E908
		public static string GetInterfaceSingleObjectTypeName(IMapObject mapObject)
		{
			IMapObjectInterfaceExtention mapObjectInterfaceExtention = mapObject as IMapObjectInterfaceExtention;
			if (mapObjectInterfaceExtention != null)
			{
				return mapObjectInterfaceExtention.GetInterfaceSingleObjectTypeName();
			}
			return string.Empty;
		}

		// Token: 0x060005B8 RID: 1464 RVA: 0x0002F92C File Offset: 0x0002E92C
		public static string GetInterfaceSeveralObjectsTypeName(IMapObject mapObject)
		{
			IMapObjectInterfaceExtention mapObjectInterfaceExtention = mapObject as IMapObjectInterfaceExtention;
			if (mapObjectInterfaceExtention != null)
			{
				return mapObjectInterfaceExtention.GetInterfaceSeveralObjectsTypeName();
			}
			return string.Empty;
		}

		// Token: 0x060005B9 RID: 1465 RVA: 0x0002F950 File Offset: 0x0002E950
		public static string GetInterfaceSingleObjectName(IMapObject mapObject)
		{
			if (mapObject == null)
			{
				return string.Empty;
			}
			if (!string.IsNullOrEmpty(mapObject.Type.Stats))
			{
				return mapObject.Type.Stats;
			}
			return mapObject.SceneName;
		}

		// Token: 0x060005BA RID: 1466 RVA: 0x0002F990 File Offset: 0x0002E990
		public static bool ContainsText(IMapObject mapObject, string searchText, string additionalText, bool ignoreCase)
		{
			if (!string.IsNullOrEmpty(searchText))
			{
				bool result = false;
				if (!string.IsNullOrEmpty(additionalText))
				{
					if (ignoreCase)
					{
						result = additionalText.ToLower().Contains(searchText.ToLower());
					}
					else
					{
						result = additionalText.Contains(searchText);
					}
				}
				if (!result)
				{
					string sceneName = mapObject.SceneName;
					if (!string.IsNullOrEmpty(sceneName))
					{
						if (ignoreCase)
						{
							result = sceneName.ToLower().Contains(searchText.ToLower());
						}
						else
						{
							result = sceneName.Contains(searchText);
						}
					}
				}
				if (!result)
				{
					IMapObjectInterfaceExtention mapObjectInterfaceExtention = mapObject as IMapObjectInterfaceExtention;
					if (mapObjectInterfaceExtention != null)
					{
						result = mapObjectInterfaceExtention.ContainsText(searchText, ignoreCase);
					}
				}
				return result;
			}
			return true;
		}

		// Token: 0x060005BB RID: 1467 RVA: 0x0002FA18 File Offset: 0x0002EA18
		public static bool ItemListContainsMapObjectTemplate(ItemList itemList, IMapObject mapObject)
		{
			if (itemList != null && mapObject != null)
			{
				if (mapObject.Type.Type == MapObjectFactory.Type.StaticObject)
				{
					return itemList.IsValidItem(mapObject.SceneName);
				}
				if (mapObject.Type.Type == MapObjectFactory.Type.StartPoint)
				{
					return itemList.IsValidItem(StartPoint.DefaultVisObject);
				}
				if (mapObject.Type.Type == MapObjectFactory.Type.Graveyard)
				{
					return itemList.IsValidItem(Graveyard.CommonVisObject) || itemList.IsValidItem(Graveyard.SectorVisObject);
				}
				if (mapObject.Type.Type == MapObjectFactory.Type.SpawnPoint)
				{
					return itemList.IsValidItem(SpawnPoint.DefaultVisObject);
				}
				if (mapObject.Type.Type == MapObjectFactory.Type.ScriptArea)
				{
					return itemList.IsValidItem(ScriptArea.DefaultVisObject);
				}
				if (mapObject.Type.Type == MapObjectFactory.Type.ZoneLocator)
				{
					return itemList.IsValidItem(ZoneLocator.DefaultVisObject);
				}
				if (mapObject.Type.Type == MapObjectFactory.Type.RoutePoint)
				{
					return itemList.IsValidItem(RoutePoint.SimpleVisObject) || itemList.IsValidItem(RoutePoint.ComplexVisObject);
				}
				if (mapObject.Type.Type == MapObjectFactory.Type.PermanentDevice)
				{
					return itemList.IsValidItem(PermanentDevice.DefaultVisObject);
				}
				if (mapObject.Type.Type == MapObjectFactory.Type.MapLocator)
				{
					return itemList.IsValidItem(MapLocator.DefaultVisObject);
				}
				if (mapObject.Type.Type == MapObjectFactory.Type.PatrolNode)
				{
					return itemList.IsValidItem(PatrolNode.DefaultVisObject);
				}
				if (mapObject.Type.Type == MapObjectFactory.Type.ClientSpawnPoint)
				{
					return itemList.IsValidItem(ClientSpawnPoint.DefaultVisObject);
				}
				if (mapObject.Type.Type == MapObjectFactory.Type.ClientPatrolNode)
				{
					return itemList.IsValidItem(ClientPatrolNode.DefaultVisObject);
				}
				if (mapObject.Type.Type == MapObjectFactory.Type.ClientSpawnPoint)
				{
					return itemList.IsValidItem(ClientSpawnPoint.DefaultVisObject);
				}
				if (mapObject.Type.Type == MapObjectFactory.Type.Sanctuary)
				{
					return itemList.IsValidItem(Sanctuary.CommonVisObject) || itemList.IsValidItem(Sanctuary.SectorVisObject);
				}
				if (mapObject.Type.Type == MapObjectFactory.Type.AstralBorder)
				{
					return itemList.IsValidItem(AstralBorder.DefaultVisObject);
				}
				if (mapObject.Type.Type == MapObjectFactory.Type.Projectile)
				{
					return itemList.IsValidItem(Projectile.DefaultVisObject);
				}
				if (mapObject.Type.Type == MapObjectFactory.Type.PlayerRespawnPlace)
				{
					return itemList.IsValidItem(PlayerRespawnPlace.DefaultVisObject);
				}
				if (mapObject.Type.Type == MapObjectFactory.Type.ExtendedSound)
				{
					return itemList.IsValidItem(ExtendedSound.DefaultVisObject);
				}
			}
			return false;
		}

		// Token: 0x0400044F RID: 1103
		private static List<string> mapObjectTypeList;
	}
}
