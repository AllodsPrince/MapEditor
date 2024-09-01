using System;
using MapEditor.Map.Containers;
using MapEditor.Map.MapObjectElements;
using Operations;
using Tools.MapObjects;

namespace MapEditor.Map.MapObjects
{
	// Token: 0x02000153 RID: 339
	public class SpawnPointOperationContainer
	{
		// Token: 0x06001056 RID: 4182 RVA: 0x0007AECC File Offset: 0x00079ECC
		private void OnScaleChanged(MapObjectContainer _mapObjectContainer, IMapObject mapObject, ref Scale oldValue, ref Scale newValue)
		{
			if (this.mapEditorMapObjectContainer != null && mapObject != null && mapObject.Type.Type == MapObjectFactory.Type.SpawnPoint)
			{
				SpawnPoint spawnPoint = mapObject as SpawnPoint;
				if (spawnPoint != null)
				{
					if (spawnPoint.SpawnPointType == SpawnPointType.Circle)
					{
						CircleSpawnPointData circleSpawnPointData = spawnPoint.SpawnPointData as CircleSpawnPointData;
						if (circleSpawnPointData != null)
						{
							bool activeBackup = spawnPoint.Active;
							spawnPoint.Active = true;
							circleSpawnPointData.Radius = circleSpawnPointData.Radius * (double)newValue.Radius / (double)oldValue.Radius;
							spawnPoint.Active = activeBackup;
							circleSpawnPointData.InvokeChanged();
							return;
						}
					}
					else if (spawnPoint.SpawnPointType == SpawnPointType.Ellipse)
					{
						EllipseSpawnPointData ellipseSpawnPointData = spawnPoint.SpawnPointData as EllipseSpawnPointData;
						if (ellipseSpawnPointData != null)
						{
							bool activeBackup2 = spawnPoint.Active;
							spawnPoint.Active = true;
							ellipseSpawnPointData.SemiaxisA = ellipseSpawnPointData.SemiaxisA * (double)newValue.X / (double)oldValue.X;
							ellipseSpawnPointData.SemiaxisB = ellipseSpawnPointData.SemiaxisB * (double)newValue.Y / (double)oldValue.Y;
							spawnPoint.Active = activeBackup2;
							ellipseSpawnPointData.InvokeChanged();
							return;
						}
					}
					else if (spawnPoint.SpawnPointType == SpawnPointType.SpawnCircle)
					{
						SpawnCircleSpawnPointData spawnCircleSpawnPointData = spawnPoint.SpawnPointData as SpawnCircleSpawnPointData;
						if (spawnCircleSpawnPointData != null)
						{
							bool activeBackup3 = spawnPoint.Active;
							spawnPoint.Active = true;
							spawnCircleSpawnPointData.Radius = spawnCircleSpawnPointData.Radius * (double)newValue.Radius / (double)oldValue.Radius;
							spawnPoint.Active = activeBackup3;
							spawnCircleSpawnPointData.InvokeChanged();
						}
					}
				}
			}
		}

		// Token: 0x06001057 RID: 4183 RVA: 0x0007B030 File Offset: 0x0007A030
		private void OnSpawnPointTypeChanged(MapEditorMapObjectContainer _mapEditorMapObjectContainer, SpawnPoint spawnPoint, ref SpawnPointData oldValue, ref SpawnPointData newValue)
		{
			if (this.mapEditorMapObjectContainer != null && spawnPoint != null && this.operationContainer != null && !spawnPoint.Temporary)
			{
				ChangeSpawnPointDataOperation operation = new ChangeSpawnPointDataOperation(spawnPoint, ref oldValue, ref newValue);
				this.operationContainer.Add(operation);
			}
		}

		// Token: 0x06001058 RID: 4184 RVA: 0x0007B070 File Offset: 0x0007A070
		private void OnSpawnPointDataChanged(MapEditorMapObjectContainer _mapEditorMapObjectContainer, SpawnPoint spawnPoint, ref SpawnPointData oldValue, ref SpawnPointData newValue)
		{
			if (this.mapEditorMapObjectContainer != null && spawnPoint != null && this.operationContainer != null && !spawnPoint.Temporary)
			{
				ChangeSpawnPointDataOperation operation = new ChangeSpawnPointDataOperation(spawnPoint, ref oldValue, ref newValue);
				this.operationContainer.Add(operation);
			}
		}

		// Token: 0x06001059 RID: 4185 RVA: 0x0007B0B0 File Offset: 0x0007A0B0
		private void OnSpawnPointSpawnTableChanged(MapEditorMapObjectContainer _mapEditorMapObjectContainer, SpawnPoint spawnPoint, ref string oldValue, ref string newValue)
		{
			if (this.mapEditorMapObjectContainer != null && spawnPoint != null && this.operationContainer != null && !spawnPoint.Temporary)
			{
				ChangeSpawnPointSpawnTableOperation operation = new ChangeSpawnPointSpawnTableOperation(spawnPoint, ref oldValue, ref newValue);
				this.operationContainer.Add(operation);
			}
		}

		// Token: 0x0600105A RID: 4186 RVA: 0x0007B0F0 File Offset: 0x0007A0F0
		private void OnSpawnPointScriptIDChanged(MapEditorMapObjectContainer _mapEditorMapObjectContainer, SpawnPoint spawnPoint, ref string oldValue, ref string newValue)
		{
			if (this.mapEditorMapObjectContainer != null && spawnPoint != null && this.operationContainer != null && !spawnPoint.Temporary)
			{
				ChangeSpawnPointScriptIDOperation operation = new ChangeSpawnPointScriptIDOperation(spawnPoint, ref oldValue, ref newValue);
				this.operationContainer.Add(operation);
			}
		}

		// Token: 0x0600105B RID: 4187 RVA: 0x0007B130 File Offset: 0x0007A130
		private void OnSpawnPointSpawnIDChanged(MapEditorMapObjectContainer _mapEditorMapObjectContainer, SpawnPoint spawnPoint, ref string oldValue, ref string newValue)
		{
			if (this.mapEditorMapObjectContainer != null && spawnPoint != null && this.operationContainer != null && !spawnPoint.Temporary)
			{
				ChangeSpawnPointSpawnIDOperation operation = new ChangeSpawnPointSpawnIDOperation(spawnPoint, ref oldValue, ref newValue);
				this.operationContainer.Add(operation);
			}
		}

		// Token: 0x0600105C RID: 4188 RVA: 0x0007B170 File Offset: 0x0007A170
		private void OnSpawnPointScanRadiusChanged(MapEditorMapObjectContainer _mapEditorMapObjectContainer, SpawnPoint spawnPoint, ref double oldValue, ref double newValue)
		{
			if (this.mapEditorMapObjectContainer != null && spawnPoint != null && this.operationContainer != null && !spawnPoint.Temporary)
			{
				ChangeSpawnPointScanRadiusOperation operation = new ChangeSpawnPointScanRadiusOperation(spawnPoint, ref oldValue, ref newValue);
				this.operationContainer.Add(operation);
			}
		}

		// Token: 0x0600105D RID: 4189 RVA: 0x0007B1B0 File Offset: 0x0007A1B0
		private void OnSpawnPointSpawnTimeChanged(MapEditorMapObjectContainer _mapEditorMapObjectContainer, SpawnPoint spawnPoint, ref SpawnTimeAbstract oldValue, ref SpawnTimeAbstract newValue)
		{
			if (this.mapEditorMapObjectContainer != null && spawnPoint != null && this.operationContainer != null && !spawnPoint.Temporary)
			{
				ChangeSpawnPointSpawnTimeOperation operation = new ChangeSpawnPointSpawnTimeOperation(spawnPoint, ref oldValue, ref newValue);
				this.operationContainer.Add(operation);
			}
		}

		// Token: 0x0600105E RID: 4190 RVA: 0x0007B1F0 File Offset: 0x0007A1F0
		private void OnSpawnPointSpawnTunerChanged(MapEditorMapObjectContainer _mapEditorMapObjectContainer, SpawnPoint spawnPoint, ref string oldValue, ref string newValue)
		{
			if (this.mapEditorMapObjectContainer != null && spawnPoint != null && this.operationContainer != null && !spawnPoint.Temporary)
			{
				ChangeSpawnPointSpawnTunerOperation operation = new ChangeSpawnPointSpawnTunerOperation(spawnPoint, ref oldValue, ref newValue);
				this.operationContainer.Add(operation);
			}
		}

		// Token: 0x0600105F RID: 4191 RVA: 0x0007B230 File Offset: 0x0007A230
		private void OnSpawnPointSingleSpawnControllerChanged(MapEditorMapObjectContainer _mapEditorMapObjectContainer, SpawnPoint spawnPoint, ref SingleSpawnController oldValue, ref SingleSpawnController newValue)
		{
			if (this.mapEditorMapObjectContainer != null && spawnPoint != null && this.operationContainer != null && !spawnPoint.Temporary)
			{
				ChangeSpawnPointSingleSpawnControllerOperation operation = new ChangeSpawnPointSingleSpawnControllerOperation(spawnPoint, ref oldValue, ref newValue);
				this.operationContainer.Add(operation);
			}
		}

		// Token: 0x06001060 RID: 4192 RVA: 0x0007B270 File Offset: 0x0007A270
		private void OnCircleSpawnPointDataRadiusChanged(MapEditorMapObjectContainer _mapEditorMapObjectContainer, SpawnPoint spawnPoint, ref double oldValue, ref double newValue)
		{
			if (this.mapEditorMapObjectContainer != null && spawnPoint != null && this.operationContainer != null && !spawnPoint.Temporary)
			{
				ChangeCircleSpawnPointDataRadiusOperation operation = new ChangeCircleSpawnPointDataRadiusOperation(spawnPoint, ref oldValue, ref newValue);
				this.operationContainer.Add(operation);
			}
		}

		// Token: 0x06001061 RID: 4193 RVA: 0x0007B2B0 File Offset: 0x0007A2B0
		private void OnEllipseSpawnPointDataSemiaxisAChanged(MapEditorMapObjectContainer _mapEditorMapObjectContainer, SpawnPoint spawnPoint, ref double oldValue, ref double newValue)
		{
			if (this.mapEditorMapObjectContainer != null && spawnPoint != null && this.operationContainer != null && !spawnPoint.Temporary)
			{
				ChangeEllipseSpawnPointDataSemiaxisAOperation operation = new ChangeEllipseSpawnPointDataSemiaxisAOperation(spawnPoint, ref oldValue, ref newValue);
				this.operationContainer.Add(operation);
			}
		}

		// Token: 0x06001062 RID: 4194 RVA: 0x0007B2F0 File Offset: 0x0007A2F0
		private void OnEllipseSpawnPointDataSemiaxisBChanged(MapEditorMapObjectContainer _mapEditorMapObjectContainer, SpawnPoint spawnPoint, ref double oldValue, ref double newValue)
		{
			if (this.mapEditorMapObjectContainer != null && spawnPoint != null && this.operationContainer != null && !spawnPoint.Temporary)
			{
				ChangeEllipseSpawnPointDataSemiaxisBOperation operation = new ChangeEllipseSpawnPointDataSemiaxisBOperation(spawnPoint, ref oldValue, ref newValue);
				this.operationContainer.Add(operation);
			}
		}

		// Token: 0x06001063 RID: 4195 RVA: 0x0007B330 File Offset: 0x0007A330
		private void OnPatrolSpawnPointDataLabelChanged(MapEditorMapObjectContainer _mapEditorMapObjectContainer, SpawnPoint spawnPoint, ref string oldValue, ref string newValue)
		{
			if (this.mapEditorMapObjectContainer != null && spawnPoint != null && this.operationContainer != null && !spawnPoint.Temporary)
			{
				ChangePatrolSpawnPointDataLabelOperation operation = new ChangePatrolSpawnPointDataLabelOperation(spawnPoint, ref oldValue, ref newValue);
				this.operationContainer.Add(operation);
			}
		}

		// Token: 0x06001064 RID: 4196 RVA: 0x0007B370 File Offset: 0x0007A370
		private void OnPatrolSpawnPointDataScriptChanged(MapEditorMapObjectContainer _mapEditorMapObjectContainer, SpawnPoint spawnPoint, ref string oldValue, ref string newValue)
		{
			if (this.mapEditorMapObjectContainer != null && spawnPoint != null && this.operationContainer != null && !spawnPoint.Temporary)
			{
				ChangePatrolSpawnPointDataScriptOperation operation = new ChangePatrolSpawnPointDataScriptOperation(spawnPoint, ref oldValue, ref newValue);
				this.operationContainer.Add(operation);
			}
		}

		// Token: 0x06001065 RID: 4197 RVA: 0x0007B3B0 File Offset: 0x0007A3B0
		private void OnSpawnCircleSpawnPointDataRadiusChanged(MapEditorMapObjectContainer _mapEditorMapObjectContainer, SpawnPoint spawnPoint, ref double oldValue, ref double newValue)
		{
			if (this.mapEditorMapObjectContainer != null && spawnPoint != null && this.operationContainer != null && !spawnPoint.Temporary)
			{
				ChangeSpawnCircleSpawnPointDataRadiusOperation operation = new ChangeSpawnCircleSpawnPointDataRadiusOperation(spawnPoint, ref oldValue, ref newValue);
				this.operationContainer.Add(operation);
			}
		}

		// Token: 0x06001066 RID: 4198 RVA: 0x0007B3F0 File Offset: 0x0007A3F0
		private void OnSpawnCircleSpawnPointDataMultiplicityChanged(MapEditorMapObjectContainer _mapEditorMapObjectContainer, SpawnPoint spawnPoint, ref int oldValue, ref int newValue)
		{
			if (this.mapEditorMapObjectContainer != null && spawnPoint != null && this.operationContainer != null && !spawnPoint.Temporary)
			{
				ChangeSpawnCircleSpawnPointDataMultiplicityOperation operation = new ChangeSpawnCircleSpawnPointDataMultiplicityOperation(spawnPoint, ref oldValue, ref newValue);
				this.operationContainer.Add(operation);
			}
		}

		// Token: 0x06001067 RID: 4199 RVA: 0x0007B42E File Offset: 0x0007A42E
		public SpawnPointOperationContainer(OperationContainer _operationContainer)
		{
			this.operationContainer = _operationContainer;
		}

		// Token: 0x06001068 RID: 4200 RVA: 0x0007B43D File Offset: 0x0007A43D
		public void Destroy()
		{
			if (this.operationContainer != null)
			{
				this.operationContainer = null;
			}
		}

		// Token: 0x06001069 RID: 4201 RVA: 0x0007B450 File Offset: 0x0007A450
		public void Bind(MapEditorMapObjectContainer _mapEditorMapObjectContainer)
		{
			this.Unbind();
			this.mapEditorMapObjectContainer = _mapEditorMapObjectContainer;
			if (this.mapEditorMapObjectContainer != null)
			{
				this.mapEditorMapObjectContainer.ScaleChanged += this.OnScaleChanged;
				SpawnPointContainer spawnPointContainer = this.mapEditorMapObjectContainer.SpawnPointContainer;
				if (spawnPointContainer != null)
				{
					spawnPointContainer.SpawnPointTypeChanged += this.OnSpawnPointTypeChanged;
					spawnPointContainer.SpawnPointDataChanged += this.OnSpawnPointDataChanged;
					spawnPointContainer.SpawnPointSpawnTableChanged += this.OnSpawnPointSpawnTableChanged;
					spawnPointContainer.SpawnPointScriptIDChanged += this.OnSpawnPointScriptIDChanged;
					spawnPointContainer.SpawnPointSpawnIDChanged += this.OnSpawnPointSpawnIDChanged;
					spawnPointContainer.SpawnPointScanRadiusChanged += this.OnSpawnPointScanRadiusChanged;
					spawnPointContainer.SpawnPointSpawnTimeChanged += this.OnSpawnPointSpawnTimeChanged;
					spawnPointContainer.SpawnPointSpawnTunerChanged += this.OnSpawnPointSpawnTunerChanged;
					spawnPointContainer.SpawnPointSingleSpawnControllerChanged += this.OnSpawnPointSingleSpawnControllerChanged;
					spawnPointContainer.CircleSpawnPointDataRadiusChanged += this.OnCircleSpawnPointDataRadiusChanged;
					spawnPointContainer.EllipseSpawnPointDataSemiaxisAChanged += this.OnEllipseSpawnPointDataSemiaxisAChanged;
					spawnPointContainer.EllipseSpawnPointDataSemiaxisBChanged += this.OnEllipseSpawnPointDataSemiaxisBChanged;
					spawnPointContainer.PatrolSpawnPointDataLabelChanged += this.OnPatrolSpawnPointDataLabelChanged;
					spawnPointContainer.PatrolSpawnPointDataScriptChanged += this.OnPatrolSpawnPointDataScriptChanged;
					spawnPointContainer.SpawnCircleSpawnPointDataRadiusChanged += this.OnSpawnCircleSpawnPointDataRadiusChanged;
					spawnPointContainer.SpawnCircleSpawnPointDataMultiplicityChanged += this.OnSpawnCircleSpawnPointDataMultiplicityChanged;
				}
			}
		}

		// Token: 0x0600106A RID: 4202 RVA: 0x0007B5C0 File Offset: 0x0007A5C0
		public void Unbind()
		{
			if (this.mapEditorMapObjectContainer != null)
			{
				this.mapEditorMapObjectContainer.ScaleChanged -= this.OnScaleChanged;
				SpawnPointContainer spawnPointContainer = this.mapEditorMapObjectContainer.SpawnPointContainer;
				if (spawnPointContainer != null)
				{
					spawnPointContainer.SpawnPointTypeChanged -= this.OnSpawnPointTypeChanged;
					spawnPointContainer.SpawnPointDataChanged -= this.OnSpawnPointDataChanged;
					spawnPointContainer.SpawnPointSpawnTableChanged -= this.OnSpawnPointSpawnTableChanged;
					spawnPointContainer.SpawnPointScriptIDChanged -= this.OnSpawnPointScriptIDChanged;
					spawnPointContainer.SpawnPointSpawnIDChanged -= this.OnSpawnPointSpawnIDChanged;
					spawnPointContainer.SpawnPointScanRadiusChanged -= this.OnSpawnPointScanRadiusChanged;
					spawnPointContainer.SpawnPointSpawnTimeChanged -= this.OnSpawnPointSpawnTimeChanged;
					spawnPointContainer.SpawnPointSpawnTunerChanged -= this.OnSpawnPointSpawnTunerChanged;
					spawnPointContainer.SpawnPointSingleSpawnControllerChanged -= this.OnSpawnPointSingleSpawnControllerChanged;
					spawnPointContainer.CircleSpawnPointDataRadiusChanged -= this.OnCircleSpawnPointDataRadiusChanged;
					spawnPointContainer.EllipseSpawnPointDataSemiaxisAChanged -= this.OnEllipseSpawnPointDataSemiaxisAChanged;
					spawnPointContainer.EllipseSpawnPointDataSemiaxisBChanged -= this.OnEllipseSpawnPointDataSemiaxisBChanged;
					spawnPointContainer.PatrolSpawnPointDataLabelChanged -= this.OnPatrolSpawnPointDataLabelChanged;
					spawnPointContainer.PatrolSpawnPointDataScriptChanged -= this.OnPatrolSpawnPointDataScriptChanged;
					spawnPointContainer.SpawnCircleSpawnPointDataRadiusChanged -= this.OnSpawnCircleSpawnPointDataRadiusChanged;
					spawnPointContainer.SpawnCircleSpawnPointDataMultiplicityChanged -= this.OnSpawnCircleSpawnPointDataMultiplicityChanged;
				}
				this.mapEditorMapObjectContainer = null;
			}
		}

		// Token: 0x04000C1E RID: 3102
		private MapEditorMapObjectContainer mapEditorMapObjectContainer;

		// Token: 0x04000C1F RID: 3103
		private OperationContainer operationContainer;
	}
}
