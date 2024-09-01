using System;
using System.Collections.Generic;
using Db;
using MapEditor.Map.MapObjects;

namespace MapEditor.Map.Containers
{
	// Token: 0x02000097 RID: 151
	public class PermanentDeviceContainer : TypedMapObjectContainer
	{
		// Token: 0x0600070B RID: 1803 RVA: 0x00037B40 File Offset: 0x00036B40
		private void OnPermanentDeviceDeviceChanged(PermanentDevice permanentDevice, ref string oldValue, ref string newValue)
		{
			if (this.mapEditorMapObjectContainer != null && base.Contains(permanentDevice))
			{
				if (this.PermanentDeviceDeviceChanged != null)
				{
					this.PermanentDeviceDeviceChanged(this.mapEditorMapObjectContainer, permanentDevice, ref oldValue, ref newValue);
				}
				if (!permanentDevice.Temporary)
				{
					this.mapEditorMapObjectContainer.InvokeModified();
				}
			}
		}

		// Token: 0x0600070C RID: 1804 RVA: 0x00037B90 File Offset: 0x00036B90
		private void OnPermanentDeviceScriptIDChanged(PermanentDevice permanentDevice, ref string oldValue, ref string newValue)
		{
			if (this.mapEditorMapObjectContainer != null && base.Contains(permanentDevice))
			{
				if (this.PermanentDeviceScriptIDChanged != null)
				{
					this.PermanentDeviceScriptIDChanged(this.mapEditorMapObjectContainer, permanentDevice, ref oldValue, ref newValue);
				}
				if (!permanentDevice.Temporary)
				{
					this.mapEditorMapObjectContainer.InvokeModified();
				}
			}
		}

		// Token: 0x0600070D RID: 1805 RVA: 0x00037BE0 File Offset: 0x00036BE0
		private void OnPermanentDeviceScanRadiusChanged(PermanentDevice permanentDevice, ref double oldValue, ref double newValue)
		{
			if (this.mapEditorMapObjectContainer != null && base.Contains(permanentDevice))
			{
				if (this.PermanentDeviceScanRadiusChanged != null)
				{
					this.PermanentDeviceScanRadiusChanged(this.mapEditorMapObjectContainer, permanentDevice, ref oldValue, ref newValue);
				}
				if (!permanentDevice.Temporary)
				{
					this.mapEditorMapObjectContainer.InvokeModified();
				}
			}
		}

		// Token: 0x0600070E RID: 1806 RVA: 0x00037C30 File Offset: 0x00036C30
		private void OnPermanentDeviceAICollisionChanged(PermanentDevice permanentDevice, ref bool oldValue, ref bool newValue)
		{
			if (this.mapEditorMapObjectContainer != null && base.Contains(permanentDevice))
			{
				if (this.PermanentDeviceAICollisionChanged != null)
				{
					this.PermanentDeviceAICollisionChanged(this.mapEditorMapObjectContainer, permanentDevice, ref oldValue, ref newValue);
				}
				if (!permanentDevice.Temporary)
				{
					this.mapEditorMapObjectContainer.InvokeModified();
				}
			}
		}

		// Token: 0x1400001D RID: 29
		// (add) Token: 0x0600070F RID: 1807 RVA: 0x00037C7D File Offset: 0x00036C7D
		// (remove) Token: 0x06000710 RID: 1808 RVA: 0x00037C96 File Offset: 0x00036C96
		public event PermanentDeviceContainer.PermanentDeviceFieldChangedEvent<string> PermanentDeviceDeviceChanged;

		// Token: 0x1400001E RID: 30
		// (add) Token: 0x06000711 RID: 1809 RVA: 0x00037CAF File Offset: 0x00036CAF
		// (remove) Token: 0x06000712 RID: 1810 RVA: 0x00037CC8 File Offset: 0x00036CC8
		public event PermanentDeviceContainer.PermanentDeviceFieldChangedEvent<string> PermanentDeviceScriptIDChanged;

		// Token: 0x1400001F RID: 31
		// (add) Token: 0x06000713 RID: 1811 RVA: 0x00037CE1 File Offset: 0x00036CE1
		// (remove) Token: 0x06000714 RID: 1812 RVA: 0x00037CFA File Offset: 0x00036CFA
		public event PermanentDeviceContainer.PermanentDeviceFieldChangedEvent<double> PermanentDeviceScanRadiusChanged;

		// Token: 0x14000020 RID: 32
		// (add) Token: 0x06000715 RID: 1813 RVA: 0x00037D13 File Offset: 0x00036D13
		// (remove) Token: 0x06000716 RID: 1814 RVA: 0x00037D2C File Offset: 0x00036D2C
		public event PermanentDeviceContainer.PermanentDeviceFieldChangedEvent<bool> PermanentDeviceAICollisionChanged;

		// Token: 0x06000717 RID: 1815 RVA: 0x00037D48 File Offset: 0x00036D48
		static PermanentDeviceContainer()
		{
			IDatabase mainDb = IDatabase.GetMainDatabase();
			if (mainDb != null)
			{
				string[] types = mainDb.GetDerivedStructPtrTypes(PermanentDevice.DeviceDBType);
				PermanentDeviceContainer.deviceTypes = new Dictionary<string, string>(types.Length);
				foreach (string type in types)
				{
					if (!PermanentDeviceContainer.deviceTypes.ContainsKey(type))
					{
						PermanentDeviceContainer.deviceTypes.Add(type, type);
					}
				}
			}
		}

		// Token: 0x06000718 RID: 1816 RVA: 0x00037DAC File Offset: 0x00036DAC
		public PermanentDeviceContainer(MapEditorMapObjectContainer _mapEditorMapObjectContainer) : base(MapObjectFactory.Type.PermanentDevice, false)
		{
			this.mapEditorMapObjectContainer = _mapEditorMapObjectContainer;
			if (this.mapEditorMapObjectContainer != null)
			{
				PermanentDevice.DeviceChanged += this.OnPermanentDeviceDeviceChanged;
				PermanentDevice.ScriptIDChanged += this.OnPermanentDeviceScriptIDChanged;
				PermanentDevice.ScanRadiusChanged += this.OnPermanentDeviceScanRadiusChanged;
				PermanentDevice.AICollisionChanged += this.OnPermanentDeviceAICollisionChanged;
				base.Destroyed += this.OnDestroyed;
			}
		}

		// Token: 0x06000719 RID: 1817 RVA: 0x00037E2A File Offset: 0x00036E2A
		public static bool IsDeviceType(string type)
		{
			return PermanentDeviceContainer.deviceTypes != null && PermanentDeviceContainer.deviceTypes.ContainsKey(type);
		}

		// Token: 0x0600071A RID: 1818 RVA: 0x00037E40 File Offset: 0x00036E40
		public void OnDestroyed(TypedMapObjectContainer _typedMapObjectContainer)
		{
			if (this.mapEditorMapObjectContainer != null)
			{
				PermanentDevice.DeviceChanged -= this.OnPermanentDeviceDeviceChanged;
				PermanentDevice.ScriptIDChanged -= this.OnPermanentDeviceScriptIDChanged;
				PermanentDevice.ScanRadiusChanged -= this.OnPermanentDeviceScanRadiusChanged;
				PermanentDevice.AICollisionChanged -= this.OnPermanentDeviceAICollisionChanged;
				base.Destroyed -= this.OnDestroyed;
				this.mapEditorMapObjectContainer = null;
			}
		}

		// Token: 0x0400052B RID: 1323
		private MapEditorMapObjectContainer mapEditorMapObjectContainer;

		// Token: 0x0400052C RID: 1324
		private static readonly Dictionary<string, string> deviceTypes;

		// Token: 0x02000098 RID: 152
		// (Invoke) Token: 0x0600071C RID: 1820
		public delegate void PermanentDeviceFieldChangedEvent<T>(MapEditorMapObjectContainer _mapEditorMapObjectContainer, PermanentDevice permanentDevice, ref T oldValue, ref T newValue);
	}
}
