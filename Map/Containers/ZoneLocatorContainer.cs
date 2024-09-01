using System;
using System.Drawing;
using MapEditor.Map.MapObjects;

namespace MapEditor.Map.Containers
{
	// Token: 0x02000265 RID: 613
	public class ZoneLocatorContainer : TypedMapObjectContainer
	{
		// Token: 0x06001CF7 RID: 7415 RVA: 0x000B8C18 File Offset: 0x000B7C18
		private void OnZoneLocatorMapZoneChanged(ZoneLocator zoneLocator, ref string oldValue, ref string newValue)
		{
			if (this.mapEditorMapObjectContainer != null && base.Contains(zoneLocator))
			{
				if (this.ZoneLocatorMapZoneChanged != null)
				{
					this.ZoneLocatorMapZoneChanged(this.mapEditorMapObjectContainer, zoneLocator, ref oldValue, ref newValue);
				}
				if (!zoneLocator.Temporary)
				{
					this.mapEditorMapObjectContainer.InvokeModified();
				}
			}
		}

		// Token: 0x06001CF8 RID: 7416 RVA: 0x000B8C68 File Offset: 0x000B7C68
		private void OnZoneLocatorMapZoneColorChanged(ZoneLocator zoneLocator, ref Color oldValue, ref Color newValue)
		{
			if (this.mapEditorMapObjectContainer != null && base.Contains(zoneLocator))
			{
				if (this.ZoneLocatorMapZoneColorChanged != null)
				{
					this.ZoneLocatorMapZoneColorChanged(this.mapEditorMapObjectContainer, zoneLocator, ref oldValue, ref newValue);
				}
				if (!zoneLocator.Temporary)
				{
					this.mapEditorMapObjectContainer.InvokeModified();
				}
			}
		}

		// Token: 0x140000D2 RID: 210
		// (add) Token: 0x06001CF9 RID: 7417 RVA: 0x000B8CB5 File Offset: 0x000B7CB5
		// (remove) Token: 0x06001CFA RID: 7418 RVA: 0x000B8CCE File Offset: 0x000B7CCE
		public event ZoneLocatorContainer.ZoneLocatorFieldChangedEvent<string> ZoneLocatorMapZoneChanged;

		// Token: 0x140000D3 RID: 211
		// (add) Token: 0x06001CFB RID: 7419 RVA: 0x000B8CE7 File Offset: 0x000B7CE7
		// (remove) Token: 0x06001CFC RID: 7420 RVA: 0x000B8D00 File Offset: 0x000B7D00
		public event ZoneLocatorContainer.ZoneLocatorFieldChangedEvent<Color> ZoneLocatorMapZoneColorChanged;

		// Token: 0x06001CFD RID: 7421 RVA: 0x000B8D1C File Offset: 0x000B7D1C
		public ZoneLocatorContainer(MapEditorMapObjectContainer _mapEditorMapObjectContainer) : base(MapObjectFactory.Type.ZoneLocator, false)
		{
			this.mapEditorMapObjectContainer = _mapEditorMapObjectContainer;
			if (this.mapEditorMapObjectContainer != null)
			{
				ZoneLocator.MapZoneChanged += this.OnZoneLocatorMapZoneChanged;
				ZoneLocator.MapZoneColorChanged += this.OnZoneLocatorMapZoneColorChanged;
				base.Destroyed += this.OnDestroyed;
			}
		}

		// Token: 0x06001CFE RID: 7422 RVA: 0x000B8D78 File Offset: 0x000B7D78
		public void OnDestroyed(TypedMapObjectContainer _typedMapObjectContainer)
		{
			if (this.mapEditorMapObjectContainer != null)
			{
				ZoneLocator.MapZoneChanged -= this.OnZoneLocatorMapZoneChanged;
				ZoneLocator.MapZoneColorChanged -= this.OnZoneLocatorMapZoneColorChanged;
				base.Destroyed -= this.OnDestroyed;
				this.mapEditorMapObjectContainer = null;
			}
		}

		// Token: 0x0400126D RID: 4717
		private MapEditorMapObjectContainer mapEditorMapObjectContainer;

		// Token: 0x02000266 RID: 614
		// (Invoke) Token: 0x06001D00 RID: 7424
		public delegate void ZoneLocatorFieldChangedEvent<T>(MapEditorMapObjectContainer _mapEditorMapObjectContainer, ZoneLocator zoneLocator, ref T oldValue, ref T newValue);
	}
}
