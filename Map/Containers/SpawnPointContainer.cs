using System;
using MapEditor.Map.MapObjectElements;
using MapEditor.Map.MapObjects;
using Tools.LinkContainer;
using Tools.MapObjects;

namespace MapEditor.Map.Containers
{
	// Token: 0x0200015C RID: 348
	public class SpawnPointContainer : TypedMapObjectContainer
	{
		// Token: 0x060010AA RID: 4266 RVA: 0x0007D074 File Offset: 0x0007C074
		private void OnSpawnPointTypeChanged(SpawnPoint spawnPoint, ref SpawnPointData oldValue, ref SpawnPointData newValue)
		{
			if (this.mapEditorMapObjectContainer != null && base.Contains(spawnPoint))
			{
				if (this.SpawnPointTypeChanged != null)
				{
					this.SpawnPointTypeChanged(this.mapEditorMapObjectContainer, spawnPoint, ref oldValue, ref newValue);
				}
				if (!spawnPoint.Temporary)
				{
					this.mapEditorMapObjectContainer.InvokeModified();
				}
			}
		}

		// Token: 0x060010AB RID: 4267 RVA: 0x0007D0C4 File Offset: 0x0007C0C4
		private void OnSpawnPointDataChanged(SpawnPoint spawnPoint, ref SpawnPointData oldValue, ref SpawnPointData newValue)
		{
			if (this.mapEditorMapObjectContainer != null && base.Contains(spawnPoint))
			{
				if (this.SpawnPointDataChanged != null)
				{
					this.SpawnPointDataChanged(this.mapEditorMapObjectContainer, spawnPoint, ref oldValue, ref newValue);
				}
				if (!spawnPoint.Temporary)
				{
					this.mapEditorMapObjectContainer.InvokeModified();
				}
			}
		}

		// Token: 0x060010AC RID: 4268 RVA: 0x0007D114 File Offset: 0x0007C114
		private void OnSpawnPointSpawnTableChanged(SpawnPoint spawnPoint, ref string oldValue, ref string newValue)
		{
			if (this.mapEditorMapObjectContainer != null && base.Contains(spawnPoint))
			{
				if (this.SpawnPointSpawnTableChanged != null)
				{
					this.SpawnPointSpawnTableChanged(this.mapEditorMapObjectContainer, spawnPoint, ref oldValue, ref newValue);
				}
				if (!spawnPoint.Temporary)
				{
					this.mapEditorMapObjectContainer.InvokeModified();
				}
			}
		}

		// Token: 0x060010AD RID: 4269 RVA: 0x0007D164 File Offset: 0x0007C164
		private void OnSpawnPointScriptIDChanged(SpawnPoint spawnPoint, ref string oldValue, ref string newValue)
		{
			if (this.mapEditorMapObjectContainer != null && base.Contains(spawnPoint))
			{
				if (this.SpawnPointScriptIDChanged != null)
				{
					this.SpawnPointScriptIDChanged(this.mapEditorMapObjectContainer, spawnPoint, ref oldValue, ref newValue);
				}
				if (!spawnPoint.Temporary)
				{
					this.mapEditorMapObjectContainer.InvokeModified();
				}
			}
		}

		// Token: 0x060010AE RID: 4270 RVA: 0x0007D1B4 File Offset: 0x0007C1B4
		private void OnSpawnPointSpawnIDChanged(SpawnPoint spawnPoint, ref string oldValue, ref string newValue)
		{
			if (this.mapEditorMapObjectContainer != null && base.Contains(spawnPoint))
			{
				if (this.SpawnPointSpawnIDChanged != null)
				{
					this.SpawnPointSpawnIDChanged(this.mapEditorMapObjectContainer, spawnPoint, ref oldValue, ref newValue);
				}
				if (!spawnPoint.Temporary)
				{
					this.mapEditorMapObjectContainer.InvokeModified();
				}
			}
		}

		// Token: 0x060010AF RID: 4271 RVA: 0x0007D204 File Offset: 0x0007C204
		private void OnSpawnPointScanRadiusChanged(SpawnPoint spawnPoint, ref double oldValue, ref double newValue)
		{
			if (this.mapEditorMapObjectContainer != null && base.Contains(spawnPoint))
			{
				if (this.SpawnPointScanRadiusChanged != null)
				{
					this.SpawnPointScanRadiusChanged(this.mapEditorMapObjectContainer, spawnPoint, ref oldValue, ref newValue);
				}
				if (!spawnPoint.Temporary)
				{
					this.mapEditorMapObjectContainer.InvokeModified();
				}
			}
		}

		// Token: 0x060010B0 RID: 4272 RVA: 0x0007D254 File Offset: 0x0007C254
		private void OnSpawnPointSpawnTimeChanged(SpawnPoint spawnPoint, ref SpawnTimeAbstract oldValue, ref SpawnTimeAbstract newValue)
		{
			if (this.mapEditorMapObjectContainer != null && base.Contains(spawnPoint))
			{
				if (this.SpawnPointSpawnTimeChanged != null)
				{
					this.SpawnPointSpawnTimeChanged(this.mapEditorMapObjectContainer, spawnPoint, ref oldValue, ref newValue);
				}
				if (!spawnPoint.Temporary)
				{
					this.mapEditorMapObjectContainer.InvokeModified();
				}
			}
		}

		// Token: 0x060010B1 RID: 4273 RVA: 0x0007D2A4 File Offset: 0x0007C2A4
		private void OnSpawnPointSpawnTunerChanged(SpawnPoint spawnPoint, ref string oldValue, ref string newValue)
		{
			if (this.mapEditorMapObjectContainer != null && base.Contains(spawnPoint))
			{
				if (this.SpawnPointSpawnTunerChanged != null)
				{
					this.SpawnPointSpawnTunerChanged(this.mapEditorMapObjectContainer, spawnPoint, ref oldValue, ref newValue);
				}
				if (!spawnPoint.Temporary)
				{
					this.mapEditorMapObjectContainer.InvokeModified();
				}
			}
		}

		// Token: 0x060010B2 RID: 4274 RVA: 0x0007D2F1 File Offset: 0x0007C2F1
		private void OnSpawnPointMobSceneNameChanged(SpawnPoint spawnPoint, ref string oldValue, ref string newValue)
		{
			if (this.mapEditorMapObjectContainer != null && base.Contains(spawnPoint) && this.SpawnPointMobSceneNameChanged != null)
			{
				this.SpawnPointMobSceneNameChanged(this.mapEditorMapObjectContainer, spawnPoint, ref oldValue, ref newValue);
			}
		}

		// Token: 0x060010B3 RID: 4275 RVA: 0x0007D320 File Offset: 0x0007C320
		private void OnSpawnPointSpawnTableTypeChanged(SpawnPoint spawnPoint, ref SpawnTableType oldValue, ref SpawnTableType newValue)
		{
			if (this.mapEditorMapObjectContainer != null && base.Contains(spawnPoint) && this.SpawnPointSpawnTableTypeChanged != null)
			{
				this.SpawnPointSpawnTableTypeChanged(this.mapEditorMapObjectContainer, spawnPoint, ref oldValue, ref newValue);
			}
		}

		// Token: 0x060010B4 RID: 4276 RVA: 0x0007D34F File Offset: 0x0007C34F
		private void OnSpawnPointAggroRadiusChanged(SpawnPoint spawnPoint, ref double oldValue, ref double newValue)
		{
			if (this.mapEditorMapObjectContainer != null && base.Contains(spawnPoint))
			{
				if (this.SpawnPointAggroRadiusChanged != null)
				{
					this.SpawnPointAggroRadiusChanged(this.mapEditorMapObjectContainer, spawnPoint, ref oldValue, ref newValue);
				}
				SpawnPointContainer.SetAggroRadius(this.mapEditorMapObjectContainer, spawnPoint);
			}
		}

		// Token: 0x060010B5 RID: 4277 RVA: 0x0007D38C File Offset: 0x0007C38C
		private void OnSingleSpawnControllerChanged(SpawnPoint spawnPoint, ref SingleSpawnController oldValue, ref SingleSpawnController newValue)
		{
			if (this.mapEditorMapObjectContainer != null && base.Contains(spawnPoint))
			{
				if (this.SpawnPointSingleSpawnControllerChanged != null)
				{
					this.SpawnPointSingleSpawnControllerChanged(this.mapEditorMapObjectContainer, spawnPoint, ref oldValue, ref newValue);
				}
				if (!spawnPoint.Temporary)
				{
					this.mapEditorMapObjectContainer.InvokeModified();
				}
			}
		}

		// Token: 0x060010B6 RID: 4278 RVA: 0x0007D3D9 File Offset: 0x0007C3D9
		private void OnSpawnPointDataFieldChanged(SpawnPoint spawnPoint)
		{
			if (this.mapEditorMapObjectContainer != null && base.Contains(spawnPoint) && this.SpawnPointDataFieldChanged != null)
			{
				this.SpawnPointDataFieldChanged(this.mapEditorMapObjectContainer, spawnPoint);
			}
		}

		// Token: 0x060010B7 RID: 4279 RVA: 0x0007D408 File Offset: 0x0007C408
		private void OnCircleSpawnPointDataRadiusChanged(SpawnPoint spawnPoint, ref double oldValue, ref double newValue)
		{
			if (this.mapEditorMapObjectContainer != null && base.Contains(spawnPoint))
			{
				if (this.CircleSpawnPointDataRadiusChanged != null)
				{
					this.CircleSpawnPointDataRadiusChanged(this.mapEditorMapObjectContainer, spawnPoint, ref oldValue, ref newValue);
				}
				if (!spawnPoint.Temporary)
				{
					this.mapEditorMapObjectContainer.InvokeModified();
				}
			}
		}

		// Token: 0x060010B8 RID: 4280 RVA: 0x0007D458 File Offset: 0x0007C458
		private void OnEllipseSpawnPointDataSemiaxisAChanged(SpawnPoint spawnPoint, ref double oldValue, ref double newValue)
		{
			if (this.mapEditorMapObjectContainer != null && base.Contains(spawnPoint))
			{
				if (this.EllipseSpawnPointDataSemiaxisAChanged != null)
				{
					this.EllipseSpawnPointDataSemiaxisAChanged(this.mapEditorMapObjectContainer, spawnPoint, ref oldValue, ref newValue);
				}
				if (!spawnPoint.Temporary)
				{
					this.mapEditorMapObjectContainer.InvokeModified();
				}
			}
		}

		// Token: 0x060010B9 RID: 4281 RVA: 0x0007D4A8 File Offset: 0x0007C4A8
		private void OnEllipseSpawnPointDataSemiaxisBChanged(SpawnPoint spawnPoint, ref double oldValue, ref double newValue)
		{
			if (this.mapEditorMapObjectContainer != null && base.Contains(spawnPoint))
			{
				if (this.EllipseSpawnPointDataSemiaxisBChanged != null)
				{
					this.EllipseSpawnPointDataSemiaxisBChanged(this.mapEditorMapObjectContainer, spawnPoint, ref oldValue, ref newValue);
				}
				if (!spawnPoint.Temporary)
				{
					this.mapEditorMapObjectContainer.InvokeModified();
				}
			}
		}

		// Token: 0x060010BA RID: 4282 RVA: 0x0007D4F8 File Offset: 0x0007C4F8
		private void OnPatrolSpawnPointDataLabelChanged(SpawnPoint spawnPoint, ref string oldValue, ref string newValue)
		{
			if (this.mapEditorMapObjectContainer != null && base.Contains(spawnPoint))
			{
				if (this.PatrolSpawnPointDataLabelChanged != null)
				{
					this.PatrolSpawnPointDataLabelChanged(this.mapEditorMapObjectContainer, spawnPoint, ref oldValue, ref newValue);
				}
				if (!spawnPoint.Temporary)
				{
					this.mapEditorMapObjectContainer.InvokeModified();
				}
			}
		}

		// Token: 0x060010BB RID: 4283 RVA: 0x0007D548 File Offset: 0x0007C548
		private void OnPatrolSpawnPointDataScriptChanged(SpawnPoint spawnPoint, ref string oldValue, ref string newValue)
		{
			if (this.mapEditorMapObjectContainer != null && base.Contains(spawnPoint))
			{
				if (this.PatrolSpawnPointDataScriptChanged != null)
				{
					this.PatrolSpawnPointDataScriptChanged(this.mapEditorMapObjectContainer, spawnPoint, ref oldValue, ref newValue);
				}
				if (!spawnPoint.Temporary)
				{
					this.mapEditorMapObjectContainer.InvokeModified();
				}
			}
		}

		// Token: 0x060010BC RID: 4284 RVA: 0x0007D598 File Offset: 0x0007C598
		private void OnSpawnCircleSpawnPointDataRadiusChanged(SpawnPoint spawnPoint, ref double oldValue, ref double newValue)
		{
			if (this.mapEditorMapObjectContainer != null && base.Contains(spawnPoint))
			{
				if (this.SpawnCircleSpawnPointDataRadiusChanged != null)
				{
					this.SpawnCircleSpawnPointDataRadiusChanged(this.mapEditorMapObjectContainer, spawnPoint, ref oldValue, ref newValue);
				}
				if (!spawnPoint.Temporary)
				{
					this.mapEditorMapObjectContainer.InvokeModified();
				}
			}
		}

		// Token: 0x060010BD RID: 4285 RVA: 0x0007D5E8 File Offset: 0x0007C5E8
		private void OnSpawnCircleSpawnPointDataMultiplicityChanged(SpawnPoint spawnPoint, ref int oldValue, ref int newValue)
		{
			if (this.mapEditorMapObjectContainer != null && base.Contains(spawnPoint))
			{
				if (this.SpawnCircleSpawnPointDataMultiplicityChanged != null)
				{
					this.SpawnCircleSpawnPointDataMultiplicityChanged(this.mapEditorMapObjectContainer, spawnPoint, ref oldValue, ref newValue);
				}
				if (!spawnPoint.Temporary)
				{
					this.mapEditorMapObjectContainer.InvokeModified();
				}
			}
		}

		// Token: 0x060010BE RID: 4286 RVA: 0x0007D638 File Offset: 0x0007C638
		private static void SetAggroRadius(MapObjectContainer _mapObjectContainer, IMapObject mapObject)
		{
			LinkCollector linkCollector = new LinkCollector(_mapObjectContainer, new SpawnPointLinkFilter());
			linkCollector.Collect(mapObject);
			double aggroRadius = 0.0;
			bool spawnPointPresent = false;
			foreach (IMapObject _mapObject in linkCollector.LinkedMapObjects)
			{
				if (_mapObject.Type.Type == MapObjectFactory.Type.SpawnPoint)
				{
					SpawnPoint _spawnPoint = _mapObject as SpawnPoint;
					if (_spawnPoint != null && _spawnPoint.AgrroRadius > aggroRadius)
					{
						aggroRadius = _spawnPoint.AgrroRadius;
					}
					spawnPointPresent = true;
				}
			}
			if (spawnPointPresent)
			{
				foreach (IMapObject _mapObject2 in linkCollector.LinkedMapObjects)
				{
					if (_mapObject2 != null)
					{
						if (_mapObject2.Type.Type == MapObjectFactory.Type.PatrolNode)
						{
							PatrolNode _patrolNode = _mapObject2 as PatrolNode;
							if (_patrolNode != null)
							{
								_patrolNode.SetAggroRadius(aggroRadius);
							}
						}
						else if (_mapObject2.Type.Type == MapObjectFactory.Type.RoutePoint)
						{
							RoutePoint _routePoint = _mapObject2 as RoutePoint;
							if (_routePoint != null && _routePoint.RoutePointType == RoutePointType.PatrolNode)
							{
								_routePoint.SetAggroRadius(aggroRadius);
							}
						}
					}
				}
			}
		}

		// Token: 0x060010BF RID: 4287 RVA: 0x0007D788 File Offset: 0x0007C788
		private static void ClearAggroRadius(MapObjectContainer _mapObjectContainer, IMapObject mapObject)
		{
			LinkCollector linkCollector = new LinkCollector(_mapObjectContainer, new SpawnPointLinkFilter());
			linkCollector.Collect(mapObject);
			bool spawnPointNotPresent = true;
			foreach (IMapObject _mapObject in linkCollector.LinkedMapObjects)
			{
				if (_mapObject.Type.Type == MapObjectFactory.Type.SpawnPoint)
				{
					spawnPointNotPresent = false;
					break;
				}
			}
			if (spawnPointNotPresent)
			{
				foreach (IMapObject _mapObject2 in linkCollector.LinkedMapObjects)
				{
					if (_mapObject2 != null)
					{
						if (_mapObject2.Type.Type == MapObjectFactory.Type.PatrolNode)
						{
							PatrolNode _patrolNode = _mapObject2 as PatrolNode;
							if (_patrolNode != null)
							{
								_patrolNode.SetAggroRadius(SpawnPoint.EmptyAggroRadius);
							}
						}
						else if (_mapObject2.Type.Type == MapObjectFactory.Type.RoutePoint)
						{
							RoutePoint _routePoint = _mapObject2 as RoutePoint;
							if (_routePoint != null && _routePoint.RoutePointType == RoutePointType.PatrolNode)
							{
								_routePoint.SetAggroRadius(SpawnPoint.EmptyAggroRadius);
							}
						}
					}
				}
			}
		}

		// Token: 0x060010C0 RID: 4288 RVA: 0x0007D8B4 File Offset: 0x0007C8B4
		private static void OnPostLinkAdded(MapObjectContainer _mapObjectContainer, LinkContainer<IMapObject> _links, IMapObject left, IMapObject right, ILinkData linkData)
		{
			SpawnPointContainer.SetAggroRadius(_mapObjectContainer, left);
		}

		// Token: 0x060010C1 RID: 4289 RVA: 0x0007D8BD File Offset: 0x0007C8BD
		private static void OnPostLinkRemoved(MapObjectContainer _mapObjectContainer, LinkContainer<IMapObject> _links, IMapObject left, IMapObject right, ILinkData linkData)
		{
			SpawnPointContainer.ClearAggroRadius(_mapObjectContainer, left);
			SpawnPointContainer.ClearAggroRadius(_mapObjectContainer, right);
		}

		// Token: 0x14000055 RID: 85
		// (add) Token: 0x060010C2 RID: 4290 RVA: 0x0007D8CD File Offset: 0x0007C8CD
		// (remove) Token: 0x060010C3 RID: 4291 RVA: 0x0007D8E6 File Offset: 0x0007C8E6
		public event SpawnPointContainer.SpawnPointFieldChangedEvent<SpawnPointData> SpawnPointTypeChanged;

		// Token: 0x14000056 RID: 86
		// (add) Token: 0x060010C4 RID: 4292 RVA: 0x0007D8FF File Offset: 0x0007C8FF
		// (remove) Token: 0x060010C5 RID: 4293 RVA: 0x0007D918 File Offset: 0x0007C918
		public event SpawnPointContainer.SpawnPointFieldChangedEvent<SpawnPointData> SpawnPointDataChanged;

		// Token: 0x14000057 RID: 87
		// (add) Token: 0x060010C6 RID: 4294 RVA: 0x0007D931 File Offset: 0x0007C931
		// (remove) Token: 0x060010C7 RID: 4295 RVA: 0x0007D94A File Offset: 0x0007C94A
		public event SpawnPointContainer.SpawnPointFieldChangedEvent<string> SpawnPointSpawnTableChanged;

		// Token: 0x14000058 RID: 88
		// (add) Token: 0x060010C8 RID: 4296 RVA: 0x0007D963 File Offset: 0x0007C963
		// (remove) Token: 0x060010C9 RID: 4297 RVA: 0x0007D97C File Offset: 0x0007C97C
		public event SpawnPointContainer.SpawnPointFieldChangedEvent<string> SpawnPointScriptIDChanged;

		// Token: 0x14000059 RID: 89
		// (add) Token: 0x060010CA RID: 4298 RVA: 0x0007D995 File Offset: 0x0007C995
		// (remove) Token: 0x060010CB RID: 4299 RVA: 0x0007D9AE File Offset: 0x0007C9AE
		public event SpawnPointContainer.SpawnPointFieldChangedEvent<string> SpawnPointSpawnIDChanged;

		// Token: 0x1400005A RID: 90
		// (add) Token: 0x060010CC RID: 4300 RVA: 0x0007D9C7 File Offset: 0x0007C9C7
		// (remove) Token: 0x060010CD RID: 4301 RVA: 0x0007D9E0 File Offset: 0x0007C9E0
		public event SpawnPointContainer.SpawnPointFieldChangedEvent<double> SpawnPointScanRadiusChanged;

		// Token: 0x1400005B RID: 91
		// (add) Token: 0x060010CE RID: 4302 RVA: 0x0007D9F9 File Offset: 0x0007C9F9
		// (remove) Token: 0x060010CF RID: 4303 RVA: 0x0007DA12 File Offset: 0x0007CA12
		public event SpawnPointContainer.SpawnPointFieldChangedEvent<SpawnTimeAbstract> SpawnPointSpawnTimeChanged;

		// Token: 0x1400005C RID: 92
		// (add) Token: 0x060010D0 RID: 4304 RVA: 0x0007DA2B File Offset: 0x0007CA2B
		// (remove) Token: 0x060010D1 RID: 4305 RVA: 0x0007DA44 File Offset: 0x0007CA44
		public event SpawnPointContainer.SpawnPointFieldChangedEvent<string> SpawnPointSpawnTunerChanged;

		// Token: 0x1400005D RID: 93
		// (add) Token: 0x060010D2 RID: 4306 RVA: 0x0007DA5D File Offset: 0x0007CA5D
		// (remove) Token: 0x060010D3 RID: 4307 RVA: 0x0007DA76 File Offset: 0x0007CA76
		public event SpawnPointContainer.SpawnPointFieldChangedEvent<string> SpawnPointMobSceneNameChanged;

		// Token: 0x1400005E RID: 94
		// (add) Token: 0x060010D4 RID: 4308 RVA: 0x0007DA8F File Offset: 0x0007CA8F
		// (remove) Token: 0x060010D5 RID: 4309 RVA: 0x0007DAA8 File Offset: 0x0007CAA8
		public event SpawnPointContainer.SpawnPointFieldChangedEvent<SpawnTableType> SpawnPointSpawnTableTypeChanged;

		// Token: 0x1400005F RID: 95
		// (add) Token: 0x060010D6 RID: 4310 RVA: 0x0007DAC1 File Offset: 0x0007CAC1
		// (remove) Token: 0x060010D7 RID: 4311 RVA: 0x0007DADA File Offset: 0x0007CADA
		public event SpawnPointContainer.SpawnPointFieldChangedEvent<double> SpawnPointAggroRadiusChanged;

		// Token: 0x14000060 RID: 96
		// (add) Token: 0x060010D8 RID: 4312 RVA: 0x0007DAF3 File Offset: 0x0007CAF3
		// (remove) Token: 0x060010D9 RID: 4313 RVA: 0x0007DB0C File Offset: 0x0007CB0C
		public event SpawnPointContainer.SpawnPointFieldChangedEvent<SingleSpawnController> SpawnPointSingleSpawnControllerChanged;

		// Token: 0x14000061 RID: 97
		// (add) Token: 0x060010DA RID: 4314 RVA: 0x0007DB25 File Offset: 0x0007CB25
		// (remove) Token: 0x060010DB RID: 4315 RVA: 0x0007DB3E File Offset: 0x0007CB3E
		public event SpawnPointContainer.SpawnPointDataEvent SpawnPointDataFieldChanged;

		// Token: 0x14000062 RID: 98
		// (add) Token: 0x060010DC RID: 4316 RVA: 0x0007DB57 File Offset: 0x0007CB57
		// (remove) Token: 0x060010DD RID: 4317 RVA: 0x0007DB70 File Offset: 0x0007CB70
		public event SpawnPointContainer.SpawnPointDataFieldChangedEvent<double> CircleSpawnPointDataRadiusChanged;

		// Token: 0x14000063 RID: 99
		// (add) Token: 0x060010DE RID: 4318 RVA: 0x0007DB89 File Offset: 0x0007CB89
		// (remove) Token: 0x060010DF RID: 4319 RVA: 0x0007DBA2 File Offset: 0x0007CBA2
		public event SpawnPointContainer.SpawnPointDataFieldChangedEvent<double> EllipseSpawnPointDataSemiaxisAChanged;

		// Token: 0x14000064 RID: 100
		// (add) Token: 0x060010E0 RID: 4320 RVA: 0x0007DBBB File Offset: 0x0007CBBB
		// (remove) Token: 0x060010E1 RID: 4321 RVA: 0x0007DBD4 File Offset: 0x0007CBD4
		public event SpawnPointContainer.SpawnPointDataFieldChangedEvent<double> EllipseSpawnPointDataSemiaxisBChanged;

		// Token: 0x14000065 RID: 101
		// (add) Token: 0x060010E2 RID: 4322 RVA: 0x0007DBED File Offset: 0x0007CBED
		// (remove) Token: 0x060010E3 RID: 4323 RVA: 0x0007DC06 File Offset: 0x0007CC06
		public event SpawnPointContainer.SpawnPointDataFieldChangedEvent<string> PatrolSpawnPointDataLabelChanged;

		// Token: 0x14000066 RID: 102
		// (add) Token: 0x060010E4 RID: 4324 RVA: 0x0007DC1F File Offset: 0x0007CC1F
		// (remove) Token: 0x060010E5 RID: 4325 RVA: 0x0007DC38 File Offset: 0x0007CC38
		public event SpawnPointContainer.SpawnPointDataFieldChangedEvent<string> PatrolSpawnPointDataScriptChanged;

		// Token: 0x14000067 RID: 103
		// (add) Token: 0x060010E6 RID: 4326 RVA: 0x0007DC51 File Offset: 0x0007CC51
		// (remove) Token: 0x060010E7 RID: 4327 RVA: 0x0007DC6A File Offset: 0x0007CC6A
		public event SpawnPointContainer.SpawnPointDataFieldChangedEvent<double> SpawnCircleSpawnPointDataRadiusChanged;

		// Token: 0x14000068 RID: 104
		// (add) Token: 0x060010E8 RID: 4328 RVA: 0x0007DC83 File Offset: 0x0007CC83
		// (remove) Token: 0x060010E9 RID: 4329 RVA: 0x0007DC9C File Offset: 0x0007CC9C
		public event SpawnPointContainer.SpawnPointDataFieldChangedEvent<int> SpawnCircleSpawnPointDataMultiplicityChanged;

		// Token: 0x060010EA RID: 4330 RVA: 0x0007DCB8 File Offset: 0x0007CCB8
		public SpawnPointContainer(MapEditorMapObjectContainer _mapEditorMapObjectContainer) : base(MapObjectFactory.Type.SpawnPoint, false)
		{
			this.mapEditorMapObjectContainer = _mapEditorMapObjectContainer;
			if (this.mapEditorMapObjectContainer != null)
			{
				SpawnPoint.TypeChanged += this.OnSpawnPointTypeChanged;
				SpawnPoint.DataChanged += this.OnSpawnPointDataChanged;
				SpawnPoint.SpawnTableChanged += this.OnSpawnPointSpawnTableChanged;
				SpawnPoint.ScriptIDChanged += this.OnSpawnPointScriptIDChanged;
				SpawnPoint.SpawnIDChanged += this.OnSpawnPointSpawnIDChanged;
				SpawnPoint.ScanRadiusChanged += this.OnSpawnPointScanRadiusChanged;
				SpawnPoint.SpawnTimeChanged += this.OnSpawnPointSpawnTimeChanged;
				SpawnPoint.SpawnTunerChanged += this.OnSpawnPointSpawnTunerChanged;
				SpawnPoint.MobSceneNameChanged += this.OnSpawnPointMobSceneNameChanged;
				SpawnPoint.SpawnTableTypeChanged += this.OnSpawnPointSpawnTableTypeChanged;
				SpawnPoint.AggroRadiusChanged += this.OnSpawnPointAggroRadiusChanged;
				SpawnPoint.SingleSpawnControllerChanged += this.OnSingleSpawnControllerChanged;
				SpawnPointData.Changed += this.OnSpawnPointDataFieldChanged;
				CircleSpawnPointData.RadiusChanged += this.OnCircleSpawnPointDataRadiusChanged;
				EllipseSpawnPointData.SemiaxisAChanged += this.OnEllipseSpawnPointDataSemiaxisAChanged;
				EllipseSpawnPointData.SemiaxisBChanged += this.OnEllipseSpawnPointDataSemiaxisBChanged;
				PatrolSpawnPointData.LabelChanged += this.OnPatrolSpawnPointDataLabelChanged;
				PatrolSpawnPointData.ScriptChanged += this.OnPatrolSpawnPointDataScriptChanged;
				SpawnCircleSpawnPointData.RadiusChanged += this.OnSpawnCircleSpawnPointDataRadiusChanged;
				SpawnCircleSpawnPointData.MultiplicityChanged += this.OnSpawnCircleSpawnPointDataMultiplicityChanged;
				this.mapEditorMapObjectContainer.PostLinkAdded += SpawnPointContainer.OnPostLinkAdded;
				this.mapEditorMapObjectContainer.PostLinkRemoved += SpawnPointContainer.OnPostLinkRemoved;
				base.Destroyed += this.OnDestroyed;
			}
		}

		// Token: 0x060010EB RID: 4331 RVA: 0x0007DE78 File Offset: 0x0007CE78
		public void OnDestroyed(TypedMapObjectContainer _typedMapObjectContainer)
		{
			if (this.mapEditorMapObjectContainer != null)
			{
				SpawnPoint.TypeChanged -= this.OnSpawnPointTypeChanged;
				SpawnPoint.DataChanged -= this.OnSpawnPointDataChanged;
				SpawnPoint.SpawnTableChanged -= this.OnSpawnPointSpawnTableChanged;
				SpawnPoint.ScriptIDChanged -= this.OnSpawnPointScriptIDChanged;
				SpawnPoint.SpawnIDChanged -= this.OnSpawnPointSpawnIDChanged;
				SpawnPoint.ScanRadiusChanged -= this.OnSpawnPointScanRadiusChanged;
				SpawnPoint.SpawnTimeChanged -= this.OnSpawnPointSpawnTimeChanged;
				SpawnPoint.SpawnTunerChanged -= this.OnSpawnPointSpawnTunerChanged;
				SpawnPoint.MobSceneNameChanged -= this.OnSpawnPointMobSceneNameChanged;
				SpawnPoint.SpawnTableTypeChanged -= this.OnSpawnPointSpawnTableTypeChanged;
				SpawnPoint.AggroRadiusChanged -= this.OnSpawnPointAggroRadiusChanged;
				SpawnPointData.Changed -= this.OnSpawnPointDataFieldChanged;
				SpawnPointData.Changed -= this.OnSpawnPointDataFieldChanged;
				CircleSpawnPointData.RadiusChanged -= this.OnCircleSpawnPointDataRadiusChanged;
				EllipseSpawnPointData.SemiaxisAChanged -= this.OnEllipseSpawnPointDataSemiaxisAChanged;
				EllipseSpawnPointData.SemiaxisBChanged -= this.OnEllipseSpawnPointDataSemiaxisBChanged;
				PatrolSpawnPointData.LabelChanged -= this.OnPatrolSpawnPointDataLabelChanged;
				PatrolSpawnPointData.ScriptChanged -= this.OnPatrolSpawnPointDataScriptChanged;
				SpawnCircleSpawnPointData.RadiusChanged -= this.OnSpawnCircleSpawnPointDataRadiusChanged;
				SpawnCircleSpawnPointData.MultiplicityChanged -= this.OnSpawnCircleSpawnPointDataMultiplicityChanged;
				this.mapEditorMapObjectContainer.PostLinkAdded -= SpawnPointContainer.OnPostLinkAdded;
				this.mapEditorMapObjectContainer.PostLinkRemoved -= SpawnPointContainer.OnPostLinkRemoved;
				base.Destroyed -= this.OnDestroyed;
				this.mapEditorMapObjectContainer = null;
			}
		}

		// Token: 0x04000C3F RID: 3135
		private MapEditorMapObjectContainer mapEditorMapObjectContainer;

		// Token: 0x0200015D RID: 349
		// (Invoke) Token: 0x060010ED RID: 4333
		public delegate void SpawnPointFieldChangedEvent<T>(MapEditorMapObjectContainer _mapEditorMapObjectContainer, SpawnPoint spawnPoint, ref T oldValue, ref T newValue);

		// Token: 0x0200015E RID: 350
		// (Invoke) Token: 0x060010F1 RID: 4337
		public delegate void SpawnPointDataEvent(MapEditorMapObjectContainer _mapEditorMapObjectContainer, SpawnPoint spawnPoint);

		// Token: 0x0200015F RID: 351
		// (Invoke) Token: 0x060010F5 RID: 4341
		public delegate void SpawnPointDataFieldChangedEvent<T>(MapEditorMapObjectContainer _mapEditorMapObjectContainer, SpawnPoint spawnPoint, ref T oldValue, ref T newValue);
	}
}
