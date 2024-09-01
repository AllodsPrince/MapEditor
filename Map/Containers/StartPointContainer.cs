using System;
using System.Collections.Generic;
using MapEditor.Map.MapObjects;
using Operations;
using Tools.MapObjects;

namespace MapEditor.Map.Containers
{
	// Token: 0x02000238 RID: 568
	public class StartPointContainer : TypedMapObjectContainer
	{
		// Token: 0x06001B1A RID: 6938 RVA: 0x000B0409 File Offset: 0x000AF409
		private void BeginTransaction()
		{
			if (this.operationContainer != null)
			{
				this.transactionInProgress = this.operationContainer.DoesTransactionInProgress;
				if (!this.transactionInProgress)
				{
					this.operationContainer.BeginTransaction();
				}
			}
		}

		// Token: 0x06001B1B RID: 6939 RVA: 0x000B0438 File Offset: 0x000AF438
		private void EndTransaction()
		{
			if (this.operationContainer != null && !this.transactionInProgress)
			{
				this.operationContainer.EndTransaction();
			}
		}

		// Token: 0x06001B1C RID: 6940 RVA: 0x000B0458 File Offset: 0x000AF458
		private void OnPreMapObjectAdded(MapObjectContainer _mapObjectContainer, IMapObject mapObject)
		{
			this.addObjectEventData.UpdateStartPoints(this.existingStartPoints, mapObject);
			if (this.addObjectEventData.ExistingStartPoint != null)
			{
				this.BeginTransaction();
				if (this.mapEditorMapObjectContainer != null)
				{
					this.mapEditorMapObjectContainer.RemoveMapObject(this.addObjectEventData.ExistingStartPoint);
				}
			}
		}

		// Token: 0x06001B1D RID: 6941 RVA: 0x000B04AC File Offset: 0x000AF4AC
		private void OnPostMapObjectAdded(MapObjectContainer _mapObjectContainer, IMapObject mapObject)
		{
			if (this.addObjectEventData.ExistingStartPoint != null)
			{
				this.EndTransaction();
			}
			if (this.addObjectEventData.CurrentStartPoint != null && !string.IsNullOrEmpty(this.addObjectEventData.CurrentStartPoint.Character))
			{
				this.existingStartPoints.Add(this.addObjectEventData.CurrentStartPoint.Character, this.addObjectEventData.CurrentStartPoint);
			}
			this.addObjectEventData.Clear();
		}

		// Token: 0x06001B1E RID: 6942 RVA: 0x000B0521 File Offset: 0x000AF521
		private void OnPreMapObjectRemoved(MapObjectContainer _mapObjectContainer, IMapObject mapObject)
		{
			this.removeObjectEventData.UpdateStartPoints(this.existingStartPoints, mapObject);
		}

		// Token: 0x06001B1F RID: 6943 RVA: 0x000B0538 File Offset: 0x000AF538
		private void OnPostMapObjectRemoved(MapObjectContainer _mapObjectContainer, IMapObject mapObject)
		{
			if (this.removeObjectEventData.CurrentStartPoint != null && !string.IsNullOrEmpty(this.removeObjectEventData.CurrentStartPoint.Character))
			{
				this.existingStartPoints.Remove(this.removeObjectEventData.CurrentStartPoint.Character);
			}
			this.removeObjectEventData.Clear();
		}

		// Token: 0x06001B20 RID: 6944 RVA: 0x000B0590 File Offset: 0x000AF590
		private void OnPreChangeCharacter(IMapObject mapObject, ref string oldValue)
		{
			if (!string.IsNullOrEmpty(oldValue))
			{
				this.existingStartPoints.Remove(oldValue);
			}
			this.changeCharacterEventData.UpdateStartPoints(this.existingStartPoints, mapObject);
			if (this.changeCharacterEventData.ExistingStartPoint != null)
			{
				this.BeginTransaction();
				if (this.mapEditorMapObjectContainer != null)
				{
					this.mapEditorMapObjectContainer.RemoveMapObject(this.changeCharacterEventData.ExistingStartPoint);
				}
			}
		}

		// Token: 0x06001B21 RID: 6945 RVA: 0x000B05F8 File Offset: 0x000AF5F8
		private void OnPostChangeCharacter()
		{
			if (this.changeCharacterEventData.ExistingStartPoint != null)
			{
				this.EndTransaction();
			}
			if (this.changeCharacterEventData.CurrentStartPoint != null && !string.IsNullOrEmpty(this.changeCharacterEventData.CurrentStartPoint.Character))
			{
				this.existingStartPoints.Add(this.changeCharacterEventData.CurrentStartPoint.Character, this.changeCharacterEventData.CurrentStartPoint);
			}
			this.changeCharacterEventData.Clear();
		}

		// Token: 0x06001B22 RID: 6946 RVA: 0x000B0670 File Offset: 0x000AF670
		private void OnStartPointCharacterChanged(StartPoint startPoint, ref string oldValue, ref string newValue)
		{
			if (this.mapEditorMapObjectContainer != null && base.Contains(startPoint))
			{
				this.OnPreChangeCharacter(startPoint, ref oldValue);
				if (this.StartPointCharacterChanged != null)
				{
					this.StartPointCharacterChanged(this.mapEditorMapObjectContainer, startPoint, ref oldValue, ref newValue);
				}
				if (!startPoint.Temporary)
				{
					this.mapEditorMapObjectContainer.InvokeModified();
				}
				this.OnPostChangeCharacter();
			}
		}

		// Token: 0x140000BC RID: 188
		// (add) Token: 0x06001B23 RID: 6947 RVA: 0x000B06CB File Offset: 0x000AF6CB
		// (remove) Token: 0x06001B24 RID: 6948 RVA: 0x000B06E4 File Offset: 0x000AF6E4
		public event StartPointContainer.StartPointFieldChangedEvent<string> StartPointCharacterChanged;

		// Token: 0x06001B25 RID: 6949 RVA: 0x000B0700 File Offset: 0x000AF700
		public StartPointContainer(MapEditorMapObjectContainer _mapEditorMapObjectContainer, OperationContainer _operationContainer) : base(MapObjectFactory.Type.StartPoint, false)
		{
			this.mapEditorMapObjectContainer = _mapEditorMapObjectContainer;
			this.operationContainer = _operationContainer;
			if (this.mapEditorMapObjectContainer != null)
			{
				this.mapEditorMapObjectContainer.PreMapObjectAdded += this.OnPreMapObjectAdded;
				this.mapEditorMapObjectContainer.PostMapObjectAdded += this.OnPostMapObjectAdded;
				this.mapEditorMapObjectContainer.PreMapObjectRemoved += this.OnPreMapObjectRemoved;
				this.mapEditorMapObjectContainer.PostMapObjectRemoved += this.OnPostMapObjectRemoved;
				StartPoint.CharacterChanged += this.OnStartPointCharacterChanged;
				base.Destroyed += this.OnDestroyed;
			}
		}

		// Token: 0x06001B26 RID: 6950 RVA: 0x000B07DC File Offset: 0x000AF7DC
		public void OnDestroyed(TypedMapObjectContainer _typedMapObjectContainer)
		{
			if (this.mapEditorMapObjectContainer != null)
			{
				this.existingStartPoints.Clear();
				this.mapEditorMapObjectContainer.PreMapObjectAdded -= this.OnPreMapObjectAdded;
				this.mapEditorMapObjectContainer.PostMapObjectAdded -= this.OnPostMapObjectAdded;
				this.mapEditorMapObjectContainer.PreMapObjectRemoved -= this.OnPreMapObjectRemoved;
				this.mapEditorMapObjectContainer.PostMapObjectRemoved -= this.OnPostMapObjectRemoved;
				StartPoint.CharacterChanged -= this.OnStartPointCharacterChanged;
				base.Destroyed -= this.OnDestroyed;
				this.mapEditorMapObjectContainer = null;
				this.operationContainer = null;
			}
		}

		// Token: 0x04001158 RID: 4440
		private MapEditorMapObjectContainer mapEditorMapObjectContainer;

		// Token: 0x04001159 RID: 4441
		private OperationContainer operationContainer;

		// Token: 0x0400115A RID: 4442
		private readonly Dictionary<string, StartPoint> existingStartPoints = new Dictionary<string, StartPoint>();

		// Token: 0x0400115B RID: 4443
		private bool transactionInProgress;

		// Token: 0x0400115C RID: 4444
		private readonly StartPointContainer.EventData addObjectEventData = new StartPointContainer.EventData();

		// Token: 0x0400115D RID: 4445
		private readonly StartPointContainer.EventData removeObjectEventData = new StartPointContainer.EventData();

		// Token: 0x0400115E RID: 4446
		private readonly StartPointContainer.EventData changeCharacterEventData = new StartPointContainer.EventData();

		// Token: 0x02000239 RID: 569
		private class EventData
		{
			// Token: 0x17000670 RID: 1648
			// (get) Token: 0x06001B27 RID: 6951 RVA: 0x000B088C File Offset: 0x000AF88C
			public StartPoint CurrentStartPoint
			{
				get
				{
					return this.currentStartPoint;
				}
			}

			// Token: 0x17000671 RID: 1649
			// (get) Token: 0x06001B28 RID: 6952 RVA: 0x000B0894 File Offset: 0x000AF894
			public StartPoint ExistingStartPoint
			{
				get
				{
					return this.existingStartPoint;
				}
			}

			// Token: 0x06001B29 RID: 6953 RVA: 0x000B089C File Offset: 0x000AF89C
			public void UpdateStartPoints(IDictionary<string, StartPoint> existingStartPoints, IMapObject mapObject)
			{
				this.Clear();
				if (mapObject != null && !mapObject.Temporary && mapObject.Type.Type == MapObjectFactory.Type.StartPoint)
				{
					this.currentStartPoint = (mapObject as StartPoint);
					if (this.currentStartPoint != null && !string.IsNullOrEmpty(this.currentStartPoint.Character) && !existingStartPoints.TryGetValue(this.currentStartPoint.Character, out this.existingStartPoint))
					{
						this.existingStartPoint = null;
					}
				}
			}

			// Token: 0x06001B2A RID: 6954 RVA: 0x000B0915 File Offset: 0x000AF915
			public void Clear()
			{
				this.currentStartPoint = null;
				this.existingStartPoint = null;
			}

			// Token: 0x04001160 RID: 4448
			private StartPoint currentStartPoint;

			// Token: 0x04001161 RID: 4449
			private StartPoint existingStartPoint;
		}

		// Token: 0x0200023A RID: 570
		// (Invoke) Token: 0x06001B2D RID: 6957
		public delegate void StartPointFieldChangedEvent<T>(MapEditorMapObjectContainer _mapEditorMapObjectContainer, StartPoint startPoint, ref T oldValue, ref T newValue);
	}
}
