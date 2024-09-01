using System;
using MapEditor.Map.Containers;
using Operations;

namespace MapEditor.Map.MapObjects
{
	// Token: 0x020002C6 RID: 710
	public class PlayerRespawnPlaceOperationContainer
	{
		// Token: 0x06002107 RID: 8455 RVA: 0x000D1C00 File Offset: 0x000D0C00
		private void OnPlayerRespawnPlaceDeviceChanged(MapEditorMapObjectContainer _mapEditorMapObjectContainer, PlayerRespawnPlace playerRespawnPlace, ref string oldValue, ref string newValue)
		{
			if (this.mapEditorMapObjectContainer != null && playerRespawnPlace != null && this.operationContainer != null && !playerRespawnPlace.Temporary)
			{
				ChangePlayerRespawnPlaceDeviceOperation operation = new ChangePlayerRespawnPlaceDeviceOperation(playerRespawnPlace, ref oldValue, ref newValue);
				this.operationContainer.Add(operation);
			}
		}

		// Token: 0x06002108 RID: 8456 RVA: 0x000D1C40 File Offset: 0x000D0C40
		private void OnPlayerRespawnPlaceFactionChanged(MapEditorMapObjectContainer _mapEditorMapObjectContainer, PlayerRespawnPlace playerRespawnPlace, ref string oldValue, ref string newValue)
		{
			if (this.mapEditorMapObjectContainer != null && playerRespawnPlace != null && this.operationContainer != null && !playerRespawnPlace.Temporary)
			{
				ChangePlayerRespawnPlaceFactionOperation operation = new ChangePlayerRespawnPlaceFactionOperation(playerRespawnPlace, ref oldValue, ref newValue);
				this.operationContainer.Add(operation);
			}
		}

		// Token: 0x06002109 RID: 8457 RVA: 0x000D1C7E File Offset: 0x000D0C7E
		public PlayerRespawnPlaceOperationContainer(OperationContainer _operationContainer)
		{
			this.operationContainer = _operationContainer;
		}

		// Token: 0x0600210A RID: 8458 RVA: 0x000D1C8D File Offset: 0x000D0C8D
		public void Destroy()
		{
			if (this.operationContainer != null)
			{
				this.operationContainer = null;
			}
		}

		// Token: 0x0600210B RID: 8459 RVA: 0x000D1CA0 File Offset: 0x000D0CA0
		public void Bind(MapEditorMapObjectContainer _mapEditorMapObjectContainer)
		{
			this.Unbind();
			this.mapEditorMapObjectContainer = _mapEditorMapObjectContainer;
			if (this.mapEditorMapObjectContainer != null)
			{
				PlayerRespawnPlaceContainer playerRespawnPlaceContainer = this.mapEditorMapObjectContainer.PlayerRespawnPlaceContainer;
				if (playerRespawnPlaceContainer != null)
				{
					playerRespawnPlaceContainer.PlayerRespawnPlaceDeviceChanged += this.OnPlayerRespawnPlaceDeviceChanged;
					playerRespawnPlaceContainer.PlayerRespawnPlaceFactionChanged += this.OnPlayerRespawnPlaceFactionChanged;
				}
			}
		}

		// Token: 0x0600210C RID: 8460 RVA: 0x000D1CF8 File Offset: 0x000D0CF8
		public void Unbind()
		{
			if (this.mapEditorMapObjectContainer != null)
			{
				PlayerRespawnPlaceContainer playerRespawnPlaceContainer = this.mapEditorMapObjectContainer.PlayerRespawnPlaceContainer;
				if (playerRespawnPlaceContainer != null)
				{
					playerRespawnPlaceContainer.PlayerRespawnPlaceDeviceChanged -= this.OnPlayerRespawnPlaceDeviceChanged;
					playerRespawnPlaceContainer.PlayerRespawnPlaceFactionChanged -= this.OnPlayerRespawnPlaceFactionChanged;
				}
				this.mapEditorMapObjectContainer = null;
			}
		}

		// Token: 0x04001410 RID: 5136
		private MapEditorMapObjectContainer mapEditorMapObjectContainer;

		// Token: 0x04001411 RID: 5137
		private OperationContainer operationContainer;
	}
}
