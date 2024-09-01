using System;
using System.Collections.Generic;
using Db;
using MapEditor.Map.MapObjects;

namespace MapEditor.Map.Containers
{
	// Token: 0x02000296 RID: 662
	public class PlayerRespawnPlaceContainer : TypedMapObjectContainer
	{
		// Token: 0x06001EEB RID: 7915 RVA: 0x000C6E60 File Offset: 0x000C5E60
		private void OnPlayerRespawnPlaceDeviceChanged(PlayerRespawnPlace playerRespawnPlace, ref string oldValue, ref string newValue)
		{
			if (this.mapEditorMapObjectContainer != null && base.Contains(playerRespawnPlace))
			{
				if (this.PlayerRespawnPlaceDeviceChanged != null)
				{
					this.PlayerRespawnPlaceDeviceChanged(this.mapEditorMapObjectContainer, playerRespawnPlace, ref oldValue, ref newValue);
				}
				if (!playerRespawnPlace.Temporary)
				{
					this.mapEditorMapObjectContainer.InvokeModified();
				}
			}
		}

		// Token: 0x06001EEC RID: 7916 RVA: 0x000C6EB0 File Offset: 0x000C5EB0
		private void OnPlayerRespawnPlaceFactionChanged(PlayerRespawnPlace playerRespawnPlace, ref string oldValue, ref string newValue)
		{
			if (this.mapEditorMapObjectContainer != null && base.Contains(playerRespawnPlace))
			{
				if (this.PlayerRespawnPlaceFactionChanged != null)
				{
					this.PlayerRespawnPlaceFactionChanged(this.mapEditorMapObjectContainer, playerRespawnPlace, ref oldValue, ref newValue);
				}
				if (!playerRespawnPlace.Temporary)
				{
					this.mapEditorMapObjectContainer.InvokeModified();
				}
			}
		}

		// Token: 0x06001EED RID: 7917 RVA: 0x000C6EFD File Offset: 0x000C5EFD
		private void OnPlayerRespawnPlaceMobSceneNameChanged(PlayerRespawnPlace playerRespawnPlace, ref string oldValue, ref string newValue)
		{
			if (this.mapEditorMapObjectContainer != null && base.Contains(playerRespawnPlace) && this.PlayerRespawnPlaceMobSceneNameChanged != null)
			{
				this.PlayerRespawnPlaceMobSceneNameChanged(this.mapEditorMapObjectContainer, playerRespawnPlace, ref oldValue, ref newValue);
			}
		}

		// Token: 0x140000D6 RID: 214
		// (add) Token: 0x06001EEE RID: 7918 RVA: 0x000C6F2C File Offset: 0x000C5F2C
		// (remove) Token: 0x06001EEF RID: 7919 RVA: 0x000C6F45 File Offset: 0x000C5F45
		public event PlayerRespawnPlaceContainer.PlayerRespawnPlaceFieldChangedEvent<string> PlayerRespawnPlaceDeviceChanged;

		// Token: 0x140000D7 RID: 215
		// (add) Token: 0x06001EF0 RID: 7920 RVA: 0x000C6F5E File Offset: 0x000C5F5E
		// (remove) Token: 0x06001EF1 RID: 7921 RVA: 0x000C6F77 File Offset: 0x000C5F77
		public event PlayerRespawnPlaceContainer.PlayerRespawnPlaceFieldChangedEvent<string> PlayerRespawnPlaceFactionChanged;

		// Token: 0x140000D8 RID: 216
		// (add) Token: 0x06001EF2 RID: 7922 RVA: 0x000C6F90 File Offset: 0x000C5F90
		// (remove) Token: 0x06001EF3 RID: 7923 RVA: 0x000C6FA9 File Offset: 0x000C5FA9
		public event PlayerRespawnPlaceContainer.PlayerRespawnPlaceFieldChangedEvent<string> PlayerRespawnPlaceMobSceneNameChanged;

		// Token: 0x06001EF4 RID: 7924 RVA: 0x000C6FC4 File Offset: 0x000C5FC4
		static PlayerRespawnPlaceContainer()
		{
			IDatabase mainDb = IDatabase.GetMainDatabase();
			if (mainDb != null)
			{
				string[] types = mainDb.GetDerivedStructPtrTypes(PlayerRespawnPlace.DeviceDBType);
				PlayerRespawnPlaceContainer.deviceTypes = new Dictionary<string, string>(types.Length);
				foreach (string type in types)
				{
					if (!PlayerRespawnPlaceContainer.deviceTypes.ContainsKey(type))
					{
						PlayerRespawnPlaceContainer.deviceTypes.Add(type, type);
					}
				}
			}
		}

		// Token: 0x06001EF5 RID: 7925 RVA: 0x000C7028 File Offset: 0x000C6028
		public PlayerRespawnPlaceContainer(MapEditorMapObjectContainer _mapEditorMapObjectContainer) : base(MapObjectFactory.Type.PlayerRespawnPlace, false)
		{
			this.mapEditorMapObjectContainer = _mapEditorMapObjectContainer;
			if (this.mapEditorMapObjectContainer != null)
			{
				PlayerRespawnPlace.DeviceChanged += this.OnPlayerRespawnPlaceDeviceChanged;
				PlayerRespawnPlace.FactionChanged += this.OnPlayerRespawnPlaceFactionChanged;
				PlayerRespawnPlace.MobSceneNameChanged += this.OnPlayerRespawnPlaceMobSceneNameChanged;
				base.Destroyed += this.OnDestroyed;
			}
		}

		// Token: 0x06001EF6 RID: 7926 RVA: 0x000C7095 File Offset: 0x000C6095
		public static bool IsDeviceType(string type)
		{
			return PlayerRespawnPlaceContainer.deviceTypes != null && PlayerRespawnPlaceContainer.deviceTypes.ContainsKey(type);
		}

		// Token: 0x06001EF7 RID: 7927 RVA: 0x000C70AC File Offset: 0x000C60AC
		public void OnDestroyed(TypedMapObjectContainer _typedMapObjectContainer)
		{
			if (this.mapEditorMapObjectContainer != null)
			{
				PlayerRespawnPlace.DeviceChanged -= this.OnPlayerRespawnPlaceDeviceChanged;
				PlayerRespawnPlace.FactionChanged -= this.OnPlayerRespawnPlaceFactionChanged;
				PlayerRespawnPlace.MobSceneNameChanged -= this.OnPlayerRespawnPlaceMobSceneNameChanged;
				base.Destroyed -= this.OnDestroyed;
				this.mapEditorMapObjectContainer = null;
			}
		}

		// Token: 0x04001335 RID: 4917
		private MapEditorMapObjectContainer mapEditorMapObjectContainer;

		// Token: 0x04001336 RID: 4918
		private static readonly Dictionary<string, string> deviceTypes;

		// Token: 0x02000297 RID: 663
		// (Invoke) Token: 0x06001EF9 RID: 7929
		public delegate void PlayerRespawnPlaceFieldChangedEvent<T>(MapEditorMapObjectContainer _mapEditorMapObjectContainer, PlayerRespawnPlace playerRespawnPlace, ref T oldValue, ref T newValue);
	}
}
