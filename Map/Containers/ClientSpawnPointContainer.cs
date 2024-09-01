using System;
using MapEditor.Map.MapObjects;

namespace MapEditor.Map.Containers
{
	// Token: 0x02000099 RID: 153
	public class ClientSpawnPointContainer : TypedMapObjectContainer
	{
		// Token: 0x0600071F RID: 1823 RVA: 0x00037EB4 File Offset: 0x00036EB4
		private void OnClientSpawnPointVisObjectChanged(ClientSpawnPoint clientSpawnPoint, ref string oldValue, ref string newValue)
		{
			if (this.mapEditorMapObjectContainer != null && base.Contains(clientSpawnPoint))
			{
				if (this.ClientSpawnPointVisObjectChanged != null)
				{
					this.ClientSpawnPointVisObjectChanged(this.mapEditorMapObjectContainer, clientSpawnPoint, ref oldValue, ref newValue);
				}
				if (!clientSpawnPoint.Temporary)
				{
					this.mapEditorMapObjectContainer.InvokeModified();
				}
			}
		}

		// Token: 0x06000720 RID: 1824 RVA: 0x00037F04 File Offset: 0x00036F04
		private void OnClientSpawnPointSceneChanged(ClientSpawnPoint clientSpawnPoint, ref string oldValue, ref string newValue)
		{
			if (this.mapEditorMapObjectContainer != null && base.Contains(clientSpawnPoint))
			{
				if (this.ClientSpawnPointSceneChanged != null)
				{
					this.ClientSpawnPointSceneChanged(this.mapEditorMapObjectContainer, clientSpawnPoint, ref oldValue, ref newValue);
				}
				if (!clientSpawnPoint.Temporary)
				{
					this.mapEditorMapObjectContainer.InvokeModified();
				}
			}
		}

		// Token: 0x06000721 RID: 1825 RVA: 0x00037F54 File Offset: 0x00036F54
		private void OnClientSpawnPointScriptIDChanged(ClientSpawnPoint clientSpawnPoint, ref string oldValue, ref string newValue)
		{
			if (this.mapEditorMapObjectContainer != null && base.Contains(clientSpawnPoint))
			{
				if (this.ClientSpawnPointScriptIDChanged != null)
				{
					this.ClientSpawnPointScriptIDChanged(this.mapEditorMapObjectContainer, clientSpawnPoint, ref oldValue, ref newValue);
				}
				if (!clientSpawnPoint.Temporary)
				{
					this.mapEditorMapObjectContainer.InvokeModified();
				}
			}
		}

		// Token: 0x06000722 RID: 1826 RVA: 0x00037FA4 File Offset: 0x00036FA4
		private void OnClientSpawnPointDataChanged(ClientSpawnPoint clientSpawnPoint, ref ClientSpawnPointData oldValue, ref ClientSpawnPointData newValue)
		{
			if (this.mapEditorMapObjectContainer != null && base.Contains(clientSpawnPoint))
			{
				if (this.ClientSpawnPointDataChanged != null)
				{
					this.ClientSpawnPointDataChanged(this.mapEditorMapObjectContainer, clientSpawnPoint, ref oldValue, ref newValue);
				}
				if (!clientSpawnPoint.Temporary)
				{
					this.mapEditorMapObjectContainer.InvokeModified();
				}
			}
		}

		// Token: 0x06000723 RID: 1827 RVA: 0x00037FF1 File Offset: 0x00036FF1
		private void OnClientSpawnPointDataFieldChanged(ClientSpawnPointData sender, string field, object oldValue, object newValue)
		{
			if (this.ClientSpawnPointDataFieldChanged != null)
			{
				this.ClientSpawnPointDataFieldChanged(sender, field, oldValue, newValue);
			}
			this.mapEditorMapObjectContainer.InvokeModified();
		}

		// Token: 0x14000021 RID: 33
		// (add) Token: 0x06000724 RID: 1828 RVA: 0x00038016 File Offset: 0x00037016
		// (remove) Token: 0x06000725 RID: 1829 RVA: 0x0003802F File Offset: 0x0003702F
		public event ClientSpawnPointContainer.ClientSpawnPointFieldChangedEvent<string> ClientSpawnPointVisObjectChanged;

		// Token: 0x14000022 RID: 34
		// (add) Token: 0x06000726 RID: 1830 RVA: 0x00038048 File Offset: 0x00037048
		// (remove) Token: 0x06000727 RID: 1831 RVA: 0x00038061 File Offset: 0x00037061
		public event ClientSpawnPointContainer.ClientSpawnPointFieldChangedEvent<string> ClientSpawnPointSceneChanged;

		// Token: 0x14000023 RID: 35
		// (add) Token: 0x06000728 RID: 1832 RVA: 0x0003807A File Offset: 0x0003707A
		// (remove) Token: 0x06000729 RID: 1833 RVA: 0x00038093 File Offset: 0x00037093
		public event ClientSpawnPointContainer.ClientSpawnPointFieldChangedEvent<string> ClientSpawnPointScriptIDChanged;

		// Token: 0x14000024 RID: 36
		// (add) Token: 0x0600072A RID: 1834 RVA: 0x000380AC File Offset: 0x000370AC
		// (remove) Token: 0x0600072B RID: 1835 RVA: 0x000380C5 File Offset: 0x000370C5
		public event ClientSpawnPointContainer.ClientSpawnPointFieldChangedEvent<ClientSpawnPointData> ClientSpawnPointDataChanged;

		// Token: 0x14000025 RID: 37
		// (add) Token: 0x0600072C RID: 1836 RVA: 0x000380DE File Offset: 0x000370DE
		// (remove) Token: 0x0600072D RID: 1837 RVA: 0x000380F7 File Offset: 0x000370F7
		public event ClientSpawnPointContainer.ClientSpawnPointDataFieldChangedEvent ClientSpawnPointDataFieldChanged;

		// Token: 0x0600072E RID: 1838 RVA: 0x00038110 File Offset: 0x00037110
		public ClientSpawnPointContainer(MapEditorMapObjectContainer _mapEditorMapObjectContainer) : base(MapObjectFactory.Type.ClientSpawnPoint, false)
		{
			this.mapEditorMapObjectContainer = _mapEditorMapObjectContainer;
			if (this.mapEditorMapObjectContainer != null)
			{
				ClientSpawnPoint.VisObjectChanged += this.OnClientSpawnPointVisObjectChanged;
				ClientSpawnPoint.SceneChanged += this.OnClientSpawnPointSceneChanged;
				ClientSpawnPoint.ScriptIDChanged += this.OnClientSpawnPointScriptIDChanged;
				ClientSpawnPoint.DataChanged += this.OnClientSpawnPointDataChanged;
				ClientSpawnPointData.Changed += this.OnClientSpawnPointDataFieldChanged;
				base.Destroyed += this.OnDestroyed;
			}
		}

		// Token: 0x0600072F RID: 1839 RVA: 0x000381A0 File Offset: 0x000371A0
		public void OnDestroyed(TypedMapObjectContainer _typedMapObjectContainer)
		{
			if (this.mapEditorMapObjectContainer != null)
			{
				ClientSpawnPoint.VisObjectChanged -= this.OnClientSpawnPointVisObjectChanged;
				ClientSpawnPoint.SceneChanged -= this.OnClientSpawnPointSceneChanged;
				ClientSpawnPoint.ScriptIDChanged -= this.OnClientSpawnPointScriptIDChanged;
				ClientSpawnPointData.Changed -= this.OnClientSpawnPointDataFieldChanged;
				ClientSpawnPoint.DataChanged -= this.OnClientSpawnPointDataChanged;
				base.Destroyed -= this.OnDestroyed;
				this.mapEditorMapObjectContainer = null;
			}
		}

		// Token: 0x04000531 RID: 1329
		private MapEditorMapObjectContainer mapEditorMapObjectContainer;

		// Token: 0x0200009A RID: 154
		// (Invoke) Token: 0x06000731 RID: 1841
		public delegate void ClientSpawnPointFieldChangedEvent<T>(MapEditorMapObjectContainer _mapEditorMapObjectContainer, ClientSpawnPoint clientSpawnPoint, ref T oldValue, ref T newValue);

		// Token: 0x0200009B RID: 155
		// (Invoke) Token: 0x06000735 RID: 1845
		public delegate void ClientSpawnPointDataFieldChangedEvent(ClientSpawnPointData sender, string field, object oldValue, object newValue);
	}
}
