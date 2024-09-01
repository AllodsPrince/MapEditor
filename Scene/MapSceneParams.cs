using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using MapEditor.Scene.Types;

namespace MapEditor.Scene
{
	// Token: 0x020002B5 RID: 693
	public class MapSceneParams
	{
		// Token: 0x140000E4 RID: 228
		// (add) Token: 0x06001FE9 RID: 8169 RVA: 0x000CC7C4 File Offset: 0x000CB7C4
		// (remove) Token: 0x06001FEA RID: 8170 RVA: 0x000CC7DD File Offset: 0x000CB7DD
		public event MapSceneParams.FieldChangedEvent<bool> ShowSpawnPointUserGeometryChanged;

		// Token: 0x140000E5 RID: 229
		// (add) Token: 0x06001FEB RID: 8171 RVA: 0x000CC7F6 File Offset: 0x000CB7F6
		// (remove) Token: 0x06001FEC RID: 8172 RVA: 0x000CC80F File Offset: 0x000CB80F
		public event MapSceneParams.FieldChangedEvent<bool> ShowScriptAreaUserGeometryChanged;

		// Token: 0x140000E6 RID: 230
		// (add) Token: 0x06001FED RID: 8173 RVA: 0x000CC828 File Offset: 0x000CB828
		// (remove) Token: 0x06001FEE RID: 8174 RVA: 0x000CC841 File Offset: 0x000CB841
		public event MapSceneParams.FieldChangedEvent<bool> ShowZoneLocatorUserGeometryChanged;

		// Token: 0x140000E7 RID: 231
		// (add) Token: 0x06001FEF RID: 8175 RVA: 0x000CC85A File Offset: 0x000CB85A
		// (remove) Token: 0x06001FF0 RID: 8176 RVA: 0x000CC873 File Offset: 0x000CB873
		public event MapSceneParams.FieldChangedEvent<bool> ShowRoutePointUserGeometryChanged;

		// Token: 0x140000E8 RID: 232
		// (add) Token: 0x06001FF1 RID: 8177 RVA: 0x000CC88C File Offset: 0x000CB88C
		// (remove) Token: 0x06001FF2 RID: 8178 RVA: 0x000CC8A5 File Offset: 0x000CB8A5
		public event MapSceneParams.FieldChangedEvent<bool> ShowRouteObjectsChanged;

		// Token: 0x140000E9 RID: 233
		// (add) Token: 0x06001FF3 RID: 8179 RVA: 0x000CC8BE File Offset: 0x000CB8BE
		// (remove) Token: 0x06001FF4 RID: 8180 RVA: 0x000CC8D7 File Offset: 0x000CB8D7
		public event MapSceneParams.FieldChangedEvent<bool> ShowLinkUserGeometryChanged;

		// Token: 0x140000EA RID: 234
		// (add) Token: 0x06001FF5 RID: 8181 RVA: 0x000CC8F0 File Offset: 0x000CB8F0
		// (remove) Token: 0x06001FF6 RID: 8182 RVA: 0x000CC909 File Offset: 0x000CB909
		public event MapSceneParams.FieldChangedEvent<bool> ShowAxisUserGeometryChanged;

		// Token: 0x140000EB RID: 235
		// (add) Token: 0x06001FF7 RID: 8183 RVA: 0x000CC922 File Offset: 0x000CB922
		// (remove) Token: 0x06001FF8 RID: 8184 RVA: 0x000CC93B File Offset: 0x000CB93B
		public event MapSceneParams.FieldChangedEvent<bool> DynamicObjectsFallChanged;

		// Token: 0x140000EC RID: 236
		// (add) Token: 0x06001FF9 RID: 8185 RVA: 0x000CC954 File Offset: 0x000CB954
		// (remove) Token: 0x06001FFA RID: 8186 RVA: 0x000CC96D File Offset: 0x000CB96D
		public event MapSceneParams.FieldChangedEvent<bool> ShowDynamicStatisticChanged;

		// Token: 0x140000ED RID: 237
		// (add) Token: 0x06001FFB RID: 8187 RVA: 0x000CC986 File Offset: 0x000CB986
		// (remove) Token: 0x06001FFC RID: 8188 RVA: 0x000CC99F File Offset: 0x000CB99F
		public event MapSceneParams.FieldChangedEvent<bool> ShowMobsAggroRadiusChanged;

		// Token: 0x140000EE RID: 238
		// (add) Token: 0x06001FFD RID: 8189 RVA: 0x000CC9B8 File Offset: 0x000CB9B8
		// (remove) Token: 0x06001FFE RID: 8190 RVA: 0x000CC9D1 File Offset: 0x000CB9D1
		public event MapSceneParams.FieldChangedEvent<bool> ShowMobsAggroRadiusAsVolumesChanged;

		// Token: 0x140000EF RID: 239
		// (add) Token: 0x06001FFF RID: 8191 RVA: 0x000CC9EA File Offset: 0x000CB9EA
		// (remove) Token: 0x06002000 RID: 8192 RVA: 0x000CCA03 File Offset: 0x000CBA03
		public event MapSceneParams.FieldChangedEvent<bool> FixTerrainTilesChanged;

		// Token: 0x140000F0 RID: 240
		// (add) Token: 0x06002001 RID: 8193 RVA: 0x000CCA1C File Offset: 0x000CBA1C
		// (remove) Token: 0x06002002 RID: 8194 RVA: 0x000CCA35 File Offset: 0x000CBA35
		public event MapSceneParams.FieldChangedEvent<bool> ShowAstralBorderUserGeometryChanged;

		// Token: 0x140000F1 RID: 241
		// (add) Token: 0x06002003 RID: 8195 RVA: 0x000CCA4E File Offset: 0x000CBA4E
		// (remove) Token: 0x06002004 RID: 8196 RVA: 0x000CCA67 File Offset: 0x000CBA67
		public event MapSceneParams.FieldChangedEvent<bool> ShowProjectileVisObjectsChanged;

		// Token: 0x140000F2 RID: 242
		// (add) Token: 0x06002005 RID: 8197 RVA: 0x000CCA80 File Offset: 0x000CBA80
		// (remove) Token: 0x06002006 RID: 8198 RVA: 0x000CCA99 File Offset: 0x000CBA99
		public event MapSceneParams.FieldChangedEvent<bool> StopDayTimeChanged;

		// Token: 0x140000F3 RID: 243
		// (add) Token: 0x06002007 RID: 8199 RVA: 0x000CCAB2 File Offset: 0x000CBAB2
		// (remove) Token: 0x06002008 RID: 8200 RVA: 0x000CCACB File Offset: 0x000CBACB
		public event MapSceneParams.FieldChangedEvent<bool> HighlightObjectsChanged;

		// Token: 0x140000F4 RID: 244
		// (add) Token: 0x06002009 RID: 8201 RVA: 0x000CCAE4 File Offset: 0x000CBAE4
		// (remove) Token: 0x0600200A RID: 8202 RVA: 0x000CCAFD File Offset: 0x000CBAFD
		public event MapSceneParams.HiddenObjectsChangedEvent HiddenObjectsChanged;

		// Token: 0x17000732 RID: 1842
		// (get) Token: 0x0600200B RID: 8203 RVA: 0x000CCB16 File Offset: 0x000CBB16
		// (set) Token: 0x0600200C RID: 8204 RVA: 0x000CCB20 File Offset: 0x000CBB20
		public bool ShowSpawnPointUserGeometry
		{
			get
			{
				return this.showSpawnPointUserGeometry;
			}
			set
			{
				if (this.showSpawnPointUserGeometry != value)
				{
					bool oldValue = this.showSpawnPointUserGeometry;
					this.showSpawnPointUserGeometry = value;
					if (this.ShowSpawnPointUserGeometryChanged != null)
					{
						this.ShowSpawnPointUserGeometryChanged(this, ref oldValue, ref this.showSpawnPointUserGeometry);
					}
				}
			}
		}

		// Token: 0x17000733 RID: 1843
		// (get) Token: 0x0600200D RID: 8205 RVA: 0x000CCB60 File Offset: 0x000CBB60
		// (set) Token: 0x0600200E RID: 8206 RVA: 0x000CCB68 File Offset: 0x000CBB68
		public bool ShowScriptAreaUserGeometry
		{
			get
			{
				return this.showScriptAreaUserGeometry;
			}
			set
			{
				if (this.showScriptAreaUserGeometry != value)
				{
					bool oldValue = this.showScriptAreaUserGeometry;
					this.showScriptAreaUserGeometry = value;
					if (this.ShowScriptAreaUserGeometryChanged != null)
					{
						this.ShowScriptAreaUserGeometryChanged(this, ref oldValue, ref this.showScriptAreaUserGeometry);
					}
				}
			}
		}

		// Token: 0x17000734 RID: 1844
		// (get) Token: 0x0600200F RID: 8207 RVA: 0x000CCBA8 File Offset: 0x000CBBA8
		// (set) Token: 0x06002010 RID: 8208 RVA: 0x000CCBB0 File Offset: 0x000CBBB0
		public bool ShowZoneLocatorUserGeometry
		{
			get
			{
				return this.showZoneLocatorUserGeometry;
			}
			set
			{
				if (this.showZoneLocatorUserGeometry != value)
				{
					bool oldValue = this.showZoneLocatorUserGeometry;
					this.showZoneLocatorUserGeometry = value;
					if (this.ShowZoneLocatorUserGeometryChanged != null)
					{
						this.ShowZoneLocatorUserGeometryChanged(this, ref oldValue, ref this.showZoneLocatorUserGeometry);
					}
				}
			}
		}

		// Token: 0x17000735 RID: 1845
		// (get) Token: 0x06002011 RID: 8209 RVA: 0x000CCBF0 File Offset: 0x000CBBF0
		// (set) Token: 0x06002012 RID: 8210 RVA: 0x000CCBF8 File Offset: 0x000CBBF8
		public bool ShowRoutePointUserGeometry
		{
			get
			{
				return this.showRoutePointUserGeometry;
			}
			set
			{
				if (this.showRoutePointUserGeometry != value)
				{
					bool oldValue = this.showRoutePointUserGeometry;
					this.showRoutePointUserGeometry = value;
					if (this.ShowRoutePointUserGeometryChanged != null)
					{
						this.ShowRoutePointUserGeometryChanged(this, ref oldValue, ref this.showRoutePointUserGeometry);
					}
				}
			}
		}

		// Token: 0x17000736 RID: 1846
		// (get) Token: 0x06002013 RID: 8211 RVA: 0x000CCC38 File Offset: 0x000CBC38
		// (set) Token: 0x06002014 RID: 8212 RVA: 0x000CCC40 File Offset: 0x000CBC40
		public bool ShowRouteObjects
		{
			get
			{
				return this.showRouteObjects;
			}
			set
			{
				if (this.showRouteObjects != value)
				{
					bool oldValue = this.showRouteObjects;
					this.showRouteObjects = value;
					if (this.ShowRouteObjectsChanged != null)
					{
						this.ShowRouteObjectsChanged(this, ref oldValue, ref this.showRouteObjects);
					}
				}
			}
		}

		// Token: 0x17000737 RID: 1847
		// (get) Token: 0x06002015 RID: 8213 RVA: 0x000CCC80 File Offset: 0x000CBC80
		// (set) Token: 0x06002016 RID: 8214 RVA: 0x000CCC88 File Offset: 0x000CBC88
		public bool ShowLinkUserGeometry
		{
			get
			{
				return this.showLinkUserGeometry;
			}
			set
			{
				if (this.showLinkUserGeometry != value)
				{
					bool oldValue = this.showLinkUserGeometry;
					this.showLinkUserGeometry = value;
					if (this.ShowLinkUserGeometryChanged != null)
					{
						this.ShowLinkUserGeometryChanged(this, ref oldValue, ref this.showLinkUserGeometry);
					}
				}
			}
		}

		// Token: 0x17000738 RID: 1848
		// (get) Token: 0x06002017 RID: 8215 RVA: 0x000CCCC8 File Offset: 0x000CBCC8
		// (set) Token: 0x06002018 RID: 8216 RVA: 0x000CCCD0 File Offset: 0x000CBCD0
		public bool ShowAxisUserGeometry
		{
			get
			{
				return this.showAxisUserGeometry;
			}
			set
			{
				if (this.showAxisUserGeometry != value)
				{
					bool oldValue = this.showAxisUserGeometry;
					this.showAxisUserGeometry = value;
					if (this.ShowAxisUserGeometryChanged != null)
					{
						this.ShowAxisUserGeometryChanged(this, ref oldValue, ref this.showAxisUserGeometry);
					}
				}
			}
		}

		// Token: 0x17000739 RID: 1849
		// (get) Token: 0x06002019 RID: 8217 RVA: 0x000CCD10 File Offset: 0x000CBD10
		// (set) Token: 0x0600201A RID: 8218 RVA: 0x000CCD18 File Offset: 0x000CBD18
		public bool DynamicObjectsFall
		{
			get
			{
				return this.dynamicObjectsFall;
			}
			set
			{
				if (this.dynamicObjectsFall != value)
				{
					bool oldValue = this.dynamicObjectsFall;
					this.dynamicObjectsFall = value;
					if (this.DynamicObjectsFallChanged != null)
					{
						this.DynamicObjectsFallChanged(this, ref oldValue, ref this.dynamicObjectsFall);
					}
				}
			}
		}

		// Token: 0x1700073A RID: 1850
		// (get) Token: 0x0600201B RID: 8219 RVA: 0x000CCD58 File Offset: 0x000CBD58
		// (set) Token: 0x0600201C RID: 8220 RVA: 0x000CCDB0 File Offset: 0x000CBDB0
		[XmlIgnore]
		public bool ShowAllObjects
		{
			get
			{
				foreach (int type in MapSceneObjectTypeContainer.Types)
				{
					if (!this.IsObjectHidden(type))
					{
						return true;
					}
				}
				return false;
			}
			set
			{
				foreach (int type in MapSceneObjectTypeContainer.Types)
				{
					this.SetObjectHidden(type, !value);
				}
			}
		}

		// Token: 0x0600201D RID: 8221 RVA: 0x000CCE00 File Offset: 0x000CBE00
		public bool IsObjectHidden(int type)
		{
			bool hidden;
			return this.hiddenObjectTypes.TryGetValue(type, out hidden) && hidden;
		}

		// Token: 0x0600201E RID: 8222 RVA: 0x000CCE20 File Offset: 0x000CBE20
		public void SetObjectHidden(int type, bool value)
		{
			this.hiddenObjectTypes[type] = value;
			if (this.HiddenObjectsChanged != null)
			{
				this.HiddenObjectsChanged(type, value);
			}
		}

		// Token: 0x1700073B RID: 1851
		// (get) Token: 0x0600201F RID: 8223 RVA: 0x000CCE44 File Offset: 0x000CBE44
		// (set) Token: 0x06002020 RID: 8224 RVA: 0x000CCE4C File Offset: 0x000CBE4C
		public bool ShowDynamicStatistic
		{
			get
			{
				return this.showDynamicStatistic;
			}
			set
			{
				if (this.showDynamicStatistic != value)
				{
					bool oldValue = this.showDynamicStatistic;
					this.showDynamicStatistic = value;
					if (this.ShowDynamicStatisticChanged != null)
					{
						this.ShowDynamicStatisticChanged(this, ref oldValue, ref this.showDynamicStatistic);
					}
				}
			}
		}

		// Token: 0x1700073C RID: 1852
		// (get) Token: 0x06002021 RID: 8225 RVA: 0x000CCE8C File Offset: 0x000CBE8C
		// (set) Token: 0x06002022 RID: 8226 RVA: 0x000CCE94 File Offset: 0x000CBE94
		public bool ShowMobsAggroRadius
		{
			get
			{
				return this.showMobsAggroRadius;
			}
			set
			{
				if (this.showMobsAggroRadius != value)
				{
					bool oldValue = this.showMobsAggroRadius;
					this.showMobsAggroRadius = value;
					if (this.ShowMobsAggroRadiusChanged != null)
					{
						this.ShowMobsAggroRadiusChanged(this, ref oldValue, ref this.showMobsAggroRadius);
					}
				}
			}
		}

		// Token: 0x1700073D RID: 1853
		// (get) Token: 0x06002023 RID: 8227 RVA: 0x000CCED4 File Offset: 0x000CBED4
		// (set) Token: 0x06002024 RID: 8228 RVA: 0x000CCEDC File Offset: 0x000CBEDC
		public bool ShowMobsAggroRadiusAsVolumes
		{
			get
			{
				return this.showMobsAggroRadiusAsVolumes;
			}
			set
			{
				if (this.showMobsAggroRadiusAsVolumes != value)
				{
					bool oldValue = this.showMobsAggroRadiusAsVolumes;
					this.showMobsAggroRadiusAsVolumes = value;
					if (this.ShowMobsAggroRadiusAsVolumesChanged != null)
					{
						this.ShowMobsAggroRadiusAsVolumesChanged(this, ref oldValue, ref this.showMobsAggroRadiusAsVolumes);
					}
				}
			}
		}

		// Token: 0x1700073E RID: 1854
		// (get) Token: 0x06002025 RID: 8229 RVA: 0x000CCF1C File Offset: 0x000CBF1C
		// (set) Token: 0x06002026 RID: 8230 RVA: 0x000CCF24 File Offset: 0x000CBF24
		public bool ShowProjectileVisObjects
		{
			get
			{
				return this.showProjectileVisObjects;
			}
			set
			{
				if (this.showProjectileVisObjects != value)
				{
					bool oldValue = this.showProjectileVisObjects;
					this.showProjectileVisObjects = value;
					if (this.ShowProjectileVisObjectsChanged != null)
					{
						this.ShowProjectileVisObjectsChanged(this, ref oldValue, ref this.showProjectileVisObjects);
					}
				}
			}
		}

		// Token: 0x1700073F RID: 1855
		// (get) Token: 0x06002027 RID: 8231 RVA: 0x000CCF64 File Offset: 0x000CBF64
		// (set) Token: 0x06002028 RID: 8232 RVA: 0x000CCF6C File Offset: 0x000CBF6C
		[XmlIgnore]
		public bool FixTerrainTiles
		{
			get
			{
				return this.fixTerrainTiles;
			}
			set
			{
				if (this.fixTerrainTiles != value)
				{
					bool oldValue = this.fixTerrainTiles;
					this.fixTerrainTiles = value;
					if (this.FixTerrainTilesChanged != null)
					{
						this.FixTerrainTilesChanged(this, ref oldValue, ref this.fixTerrainTiles);
					}
				}
			}
		}

		// Token: 0x17000740 RID: 1856
		// (get) Token: 0x06002029 RID: 8233 RVA: 0x000CCFAC File Offset: 0x000CBFAC
		// (set) Token: 0x0600202A RID: 8234 RVA: 0x000CCFB4 File Offset: 0x000CBFB4
		public bool ShowAstralBorderUserGeometry
		{
			get
			{
				return this.showAstralBorderUserGeometry;
			}
			set
			{
				if (this.showAstralBorderUserGeometry != value)
				{
					bool oldValue = this.showAstralBorderUserGeometry;
					this.showAstralBorderUserGeometry = value;
					if (this.ShowAstralBorderUserGeometryChanged != null)
					{
						this.ShowAstralBorderUserGeometryChanged(this, ref oldValue, ref this.showAstralBorderUserGeometry);
					}
				}
			}
		}

		// Token: 0x17000741 RID: 1857
		// (get) Token: 0x0600202B RID: 8235 RVA: 0x000CCFF4 File Offset: 0x000CBFF4
		// (set) Token: 0x0600202C RID: 8236 RVA: 0x000CCFFC File Offset: 0x000CBFFC
		public bool StopDayTime
		{
			get
			{
				return this.stopDayTime;
			}
			set
			{
				if (this.stopDayTime != value)
				{
					bool oldValue = this.stopDayTime;
					this.stopDayTime = value;
					if (this.StopDayTimeChanged != null)
					{
						this.StopDayTimeChanged(this, ref oldValue, ref this.stopDayTime);
					}
				}
			}
		}

		// Token: 0x17000742 RID: 1858
		// (get) Token: 0x0600202D RID: 8237 RVA: 0x000CD03C File Offset: 0x000CC03C
		// (set) Token: 0x0600202E RID: 8238 RVA: 0x000CD044 File Offset: 0x000CC044
		[XmlIgnore]
		public bool HighlightObjects
		{
			get
			{
				return this.highlightObjects;
			}
			set
			{
				if (this.highlightObjects != value)
				{
					bool oldHighlightObjects = this.highlightObjects;
					this.highlightObjects = value;
					if (this.HighlightObjectsChanged != null)
					{
						this.HighlightObjectsChanged(this, ref oldHighlightObjects, ref value);
					}
				}
			}
		}

		// Token: 0x040013AB RID: 5035
		private bool showSpawnPointUserGeometry;

		// Token: 0x040013AC RID: 5036
		private bool showScriptAreaUserGeometry;

		// Token: 0x040013AD RID: 5037
		private bool showZoneLocatorUserGeometry;

		// Token: 0x040013AE RID: 5038
		private bool showRoutePointUserGeometry;

		// Token: 0x040013AF RID: 5039
		private bool showRouteObjects;

		// Token: 0x040013B0 RID: 5040
		private bool showLinkUserGeometry;

		// Token: 0x040013B1 RID: 5041
		private bool showAxisUserGeometry;

		// Token: 0x040013B2 RID: 5042
		private bool dynamicObjectsFall;

		// Token: 0x040013B3 RID: 5043
		private readonly Dictionary<int, bool> hiddenObjectTypes = new Dictionary<int, bool>();

		// Token: 0x040013B4 RID: 5044
		private bool showDynamicStatistic;

		// Token: 0x040013B5 RID: 5045
		private bool showMobsAggroRadius;

		// Token: 0x040013B6 RID: 5046
		private bool showMobsAggroRadiusAsVolumes;

		// Token: 0x040013B7 RID: 5047
		private bool showAstralBorderUserGeometry;

		// Token: 0x040013B8 RID: 5048
		private bool fixTerrainTiles = true;

		// Token: 0x040013B9 RID: 5049
		private bool showProjectileVisObjects;

		// Token: 0x040013BA RID: 5050
		private bool stopDayTime;

		// Token: 0x040013BB RID: 5051
		private bool highlightObjects = true;

		// Token: 0x020002B6 RID: 694
		// (Invoke) Token: 0x06002031 RID: 8241
		public delegate void FieldChangedEvent<T>(MapSceneParams mapSceneParams, ref T oldValue, ref T newValue);

		// Token: 0x020002B7 RID: 695
		// (Invoke) Token: 0x06002035 RID: 8245
		public delegate void HiddenObjectsChangedEvent(int type, bool hidden);
	}
}
