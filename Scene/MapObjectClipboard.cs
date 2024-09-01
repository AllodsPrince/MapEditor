using System;
using System.Collections.Generic;
using System.Drawing;
using MapEditor.Map;
using MapEditor.Map.MapObjects;
using Tools.Groups;
using Tools.LinkContainer;
using Tools.MapObjects;

namespace MapEditor.Scene
{
	// Token: 0x02000162 RID: 354
	internal class MapObjectClipboard
	{
		// Token: 0x06001119 RID: 4377 RVA: 0x0007E5BC File Offset: 0x0007D5BC
		private bool DoesAutolinkAddedObjects()
		{
			return this.selector != null && this.selector.AutolinkObjectAfterAdd;
		}

		// Token: 0x0600111A RID: 4378 RVA: 0x0007E5D3 File Offset: 0x0007D5D3
		private bool DoesCreateCrosslinks()
		{
			return this.selector != null && this.selector.CreateCrosslinks;
		}

		// Token: 0x0600111B RID: 4379 RVA: 0x0007E5EC File Offset: 0x0007D5EC
		public MapObjectClipboard(MapObjectContainer _mapObjectContainer, ContinentType _continentType, MapObjectSelector _selector)
		{
			this.mapObjectContainer = _mapObjectContainer;
			this.continentType = _continentType;
			this.selector = _selector;
			this.multiMapObject = null;
		}

		// Token: 0x0600111C RID: 4380 RVA: 0x0007E63C File Offset: 0x0007D63C
		public void Destroy()
		{
			this.Clear();
			this.mapObjectContainer = null;
			this.continentType = ContinentType.Unknown;
			this.selector = null;
		}

		// Token: 0x0600111D RID: 4381 RVA: 0x0007E65C File Offset: 0x0007D65C
		public bool Create(string stats, EditorScene editorScene)
		{
			this.previousObjectForLinkWith = null;
			MapObjectType mapObjectType = MapObjectFactory.CreateMapObjectType(stats, editorScene);
			if (MapObjectFactory.Type.KnownType(mapObjectType.Type))
			{
				this.Clear();
				IMapObject mapObject = this.mapObjectContainer.MapObjectFactory.CreateMapObject(-1, mapObjectType, this.mapObjectContainer, true, false, new MapObjectCreationInfo());
				if (mapObject != null)
				{
					MapObjectFactory.FillMapObject(mapObject, stats, this.continentType);
					mapObject.Altitude = 0.0;
					this.mapObjects.Add(mapObject);
				}
				return mapObject != null;
			}
			return false;
		}

		// Token: 0x0600111E RID: 4382 RVA: 0x0007E6E0 File Offset: 0x0007D6E0
		public bool Create(List<IMapObject> _mapObjects, Dictionary<int, Dictionary<int, ILinkData>> _links)
		{
			this.previousObjectForLinkWith = null;
			this.Clear();
			this.mapObjects.AddRange(_mapObjects);
			if (_links != null && _links.Count > 0)
			{
				foreach (KeyValuePair<int, Dictionary<int, ILinkData>> leftKeyValuePair in _links)
				{
					foreach (KeyValuePair<int, ILinkData> rightKeyValuePair in leftKeyValuePair.Value)
					{
						this.links.Add(new MapObjectClipboard.CopiedLinkData(leftKeyValuePair.Key, rightKeyValuePair.Key, null, rightKeyValuePair.Value));
					}
				}
			}
			if (this.mapObjects.Count == 1)
			{
				this.previousObjectForLinkWith = this.mapObjects[0];
			}
			return this.mapObjects.Count > 0;
		}

		// Token: 0x0600111F RID: 4383 RVA: 0x0007E7E4 File Offset: 0x0007D7E4
		public bool Copy(Dictionary<IMapObject, IMapObject> _mapObjects)
		{
			this.Clear();
			Dictionary<IMapObject, int> projection = new Dictionary<IMapObject, int>();
			foreach (KeyValuePair<IMapObject, IMapObject> keyValuePair in _mapObjects)
			{
				if (keyValuePair.Key != null && !keyValuePair.Key.Temporary)
				{
					this.previousObjectForLinkWith = keyValuePair.Key;
					IMapObject mapObject = keyValuePair.Key.Clone(keyValuePair.Key.ID, true, false);
					if (mapObject != null)
					{
						mapObject.Select = keyValuePair.Value.Select;
						mapObject.Highlight("MapObjectClipboard", keyValuePair.Value.HighlightColor, true);
						mapObject.Surface = keyValuePair.Value.Surface;
						projection.Add(keyValuePair.Key, this.mapObjects.Count);
						this.mapObjects.Add(mapObject);
					}
				}
			}
			Dictionary<IMapObject, Dictionary<IMapObject, ILinkData>> _links = this.mapObjectContainer.GetLinks(_mapObjects);
			if (_links != null && _links.Count > 0)
			{
				foreach (KeyValuePair<IMapObject, Dictionary<IMapObject, ILinkData>> leftKeyValuePair in _links)
				{
					int leftIndex;
					if (projection.TryGetValue(leftKeyValuePair.Key, out leftIndex))
					{
						foreach (KeyValuePair<IMapObject, ILinkData> rightKeyValuePair in leftKeyValuePair.Value)
						{
							IMapObject rightMapObject = null;
							int rightIndex;
							if (!projection.TryGetValue(rightKeyValuePair.Key, out rightIndex))
							{
								rightIndex = -1;
								rightMapObject = rightKeyValuePair.Key;
							}
							this.links.Add(new MapObjectClipboard.CopiedLinkData(leftIndex, rightIndex, rightMapObject, rightKeyValuePair.Value));
						}
					}
				}
			}
			if (_mapObjects.Count != 1)
			{
				this.previousObjectForLinkWith = null;
			}
			this.additionalAltitude = this.CalculateAdditionalAltitude(_mapObjects);
			return this.mapObjects.Count > 0;
		}

		// Token: 0x06001120 RID: 4384 RVA: 0x0007E9FC File Offset: 0x0007D9FC
		private double CalculateAdditionalAltitude(ICollection<KeyValuePair<IMapObject, IMapObject>> _mapObjects)
		{
			if (_mapObjects.Count > 0)
			{
				if (_mapObjects.Count == 1)
				{
					if (this.selector.Surface == TerrainSurface.Terrain || this.selector.Surface == TerrainSurface.Water)
					{
						using (IEnumerator<KeyValuePair<IMapObject, IMapObject>> enumerator = _mapObjects.GetEnumerator())
						{
							if (!enumerator.MoveNext())
							{
								goto IL_2D1;
							}
							KeyValuePair<IMapObject, IMapObject> keyValuePair = enumerator.Current;
							return keyValuePair.Key.Position.Z - this.mapObjectContainer.GetHeight(0, keyValuePair.Key.Position.X, keyValuePair.Key.Position.Y, this.selector.Surface);
						}
					}
					if (this.selector.Surface == TerrainSurface.Object)
					{
						using (IEnumerator<KeyValuePair<IMapObject, IMapObject>> enumerator2 = _mapObjects.GetEnumerator())
						{
							if (enumerator2.MoveNext())
							{
								KeyValuePair<IMapObject, IMapObject> keyValuePair2 = enumerator2.Current;
								Position _position = keyValuePair2.Key.Position;
								bool exists;
								double nearestFlatHeight = this.mapObjectContainer.GetNearestFlatHeight(ref _position, keyValuePair2.Key.ID, out exists);
								if (exists && keyValuePair2.Key.Position.Z >= nearestFlatHeight)
								{
									return keyValuePair2.Key.Position.Z - nearestFlatHeight;
								}
							}
							goto IL_2D1;
						}
						goto IL_163;
					}
					goto IL_2D1;
				}
				IL_163:
				double _additionalAltitude = 0.0;
				int collectedCount = 0;
				if (this.selector.Surface == TerrainSurface.Terrain || this.selector.Surface == TerrainSurface.Water)
				{
					using (IEnumerator<KeyValuePair<IMapObject, IMapObject>> enumerator3 = _mapObjects.GetEnumerator())
					{
						while (enumerator3.MoveNext())
						{
							KeyValuePair<IMapObject, IMapObject> keyValuePair3 = enumerator3.Current;
							_additionalAltitude += keyValuePair3.Key.Position.Z - this.mapObjectContainer.GetHeight(0, keyValuePair3.Key.Position.X, keyValuePair3.Key.Position.Y, this.selector.Surface);
							collectedCount++;
						}
						goto IL_2C5;
					}
				}
				if (this.selector.Surface == TerrainSurface.Object)
				{
					foreach (KeyValuePair<IMapObject, IMapObject> keyValuePair4 in _mapObjects)
					{
						Position _position2 = keyValuePair4.Key.Position;
						bool exists2;
						double nearestFlatHeight2 = this.mapObjectContainer.GetNearestFlatHeight(ref _position2, keyValuePair4.Key.ID, out exists2);
						if (exists2 && keyValuePair4.Key.Position.Z >= nearestFlatHeight2)
						{
							_additionalAltitude += keyValuePair4.Key.Position.Z - nearestFlatHeight2;
							collectedCount++;
						}
					}
				}
				IL_2C5:
				if (collectedCount > 0)
				{
					return _additionalAltitude / (double)collectedCount;
				}
			}
			IL_2D1:
			return 0.0;
		}

		// Token: 0x06001121 RID: 4385 RVA: 0x0007ED1C File Offset: 0x0007DD1C
		public void Randomize(IMapObjectRandomizer mapObjectRandomizer)
		{
			foreach (IMapObject mapObject in this.mapObjects)
			{
				if (mapObjectRandomizer != null && mapObjectRandomizer.MapObjectRandomizerAvailable)
				{
					mapObject.Position = mapObjectRandomizer.RandomizePosition(mapObject.Position);
					mapObject.Rotation = mapObjectRandomizer.RandomizeRotation(mapObject.Rotation);
					mapObject.Scale = mapObjectRandomizer.RandomizeScale(mapObject.Scale);
				}
			}
		}

		// Token: 0x06001122 RID: 4386 RVA: 0x0007EDAC File Offset: 0x0007DDAC
		public void Paste(List<IMapObject> addedMapObjects, string defaultGroupName)
		{
			IMapObject newNextMapObjectForLinkWith = null;
			List<IMapObject> newMapObjects = new List<IMapObject>();
			if (this.multiMapObject != null)
			{
				GroupContainer.AddGroupName(this.multiMapObject.MapObjects, defaultGroupName);
				Position center = this.multiMapObject.Position;
				foreach (KeyValuePair<IMapObject, IMapObject> keyValuePair in this.multiMapObject.MapObjects)
				{
					int mapObjectID = this.mapObjectContainer.AddMapObjectClone(keyValuePair.Value, false, this.position + (keyValuePair.Key.Position - center));
					IMapObject addedMapObject;
					this.mapObjectContainer.MapObjects.TryGetValue(mapObjectID, out addedMapObject);
					if (addedMapObject != null)
					{
						newNextMapObjectForLinkWith = addedMapObject;
						if (addedMapObjects != null)
						{
							addedMapObjects.Add(addedMapObject);
						}
					}
					newMapObjects.Add(addedMapObject);
				}
				if (this.multiMapObject.MapObjects.Count != 1)
				{
					newNextMapObjectForLinkWith = null;
				}
			}
			else
			{
				GroupContainer.AddGroupName(this.mapObjects, defaultGroupName);
				Position center2 = PositionMedian.GetMedian(this.mapObjects);
				foreach (IMapObject mapObject in this.mapObjects)
				{
					int mapObjectID2 = this.mapObjectContainer.AddMapObjectClone(mapObject, false, this.position + (mapObject.Position - center2));
					IMapObject addedMapObject2;
					this.mapObjectContainer.MapObjects.TryGetValue(mapObjectID2, out addedMapObject2);
					if (addedMapObject2 != null)
					{
						newNextMapObjectForLinkWith = addedMapObject2;
						addedMapObjects.Add(addedMapObject2);
					}
					newMapObjects.Add(addedMapObject2);
				}
				if (this.mapObjects.Count != 1)
				{
					newNextMapObjectForLinkWith = null;
				}
			}
			foreach (MapObjectClipboard.CopiedLinkData link in this.links)
			{
				IMapObject leftMapObject = null;
				IMapObject rightMapObject = null;
				if (link.LeftIndex >= 0 && link.LeftIndex < newMapObjects.Count)
				{
					leftMapObject = newMapObjects[link.LeftIndex];
				}
				if (link.RightIndex >= 0 && link.RightIndex < newMapObjects.Count)
				{
					rightMapObject = newMapObjects[link.RightIndex];
				}
				else if (this.DoesCreateCrosslinks())
				{
					rightMapObject = link.RightMapObject;
				}
				if (leftMapObject != null && rightMapObject != null)
				{
					if (this.selector != null)
					{
						if (this.selector.LinkAvailable(leftMapObject, rightMapObject))
						{
							this.selector.LinkObjects(leftMapObject, rightMapObject, (link.LinkData != null) ? link.LinkData.Clone() : null);
						}
					}
					else
					{
						this.mapObjectContainer.AddLink(leftMapObject, rightMapObject, (link.LinkData != null) ? link.LinkData.Clone() : null);
					}
				}
			}
			if (this.selector != null && this.DoesAutolinkAddedObjects())
			{
				if (this.objectsAutolinked)
				{
					this.selector.UnlinkObjects(this.previousObjectForLinkWith, this.nextMapObjectForLinkWith);
				}
				if (this.previousObjectForLinkWith != null && newNextMapObjectForLinkWith != null && this.selector.LinkAvailable(this.previousObjectForLinkWith, newNextMapObjectForLinkWith) && this.selector.LinkObjects(this.previousObjectForLinkWith, newNextMapObjectForLinkWith))
				{
					this.selector.ArrangeLinkedObject(this.previousObjectForLinkWith);
					this.selector.ArrangeLinkedObject(newNextMapObjectForLinkWith);
				}
				this.previousObjectForLinkWith = newNextMapObjectForLinkWith;
				if (this.previousObjectForLinkWith != null && this.nextMapObjectForLinkWith != null && this.selector.LinkAvailable(this.previousObjectForLinkWith, this.nextMapObjectForLinkWith))
				{
					this.objectsAutolinked = this.selector.LinkObjects(this.previousObjectForLinkWith, this.nextMapObjectForLinkWith);
					if (this.objectsAutolinked)
					{
						this.selector.ArrangeLinkedObject(this.previousObjectForLinkWith);
						this.selector.ArrangeLinkedObject(this.nextMapObjectForLinkWith);
					}
				}
			}
		}

		// Token: 0x06001123 RID: 4387 RVA: 0x0007F184 File Offset: 0x0007E184
		public void Paste(Position _position, List<IMapObject> addedMapObjects, string defaultGroupName)
		{
			this.position = _position;
			this.Paste(addedMapObjects, defaultGroupName);
		}

		// Token: 0x06001124 RID: 4388 RVA: 0x0007F195 File Offset: 0x0007E195
		public void Clear()
		{
			this.Hide();
			this.mapObjects.Clear();
			this.links.Clear();
			this.additionalAltitude = 0.0;
		}

		// Token: 0x06001125 RID: 4389 RVA: 0x0007F1C2 File Offset: 0x0007E1C2
		public void Show()
		{
			this.Show(this.position);
		}

		// Token: 0x06001126 RID: 4390 RVA: 0x0007F1D0 File Offset: 0x0007E1D0
		public void Show(Position _position)
		{
			this.position = _position;
			IMapObject _mapObject = this.mapObjectContainer.MapObjectFactory.CreateMapObject(-1, new MapObjectType(MapObjectFactory.Type.MultiObject, string.Empty), this.mapObjectContainer, true, true);
			this.multiMapObject = (_mapObject as MultiMapObject);
			if (this.multiMapObject != null)
			{
				this.multiMapObject.ObjectBounds = this.mapObjectContainer.ObjectBounds;
				this.multiMapObject.Select = true;
				this.multiMapObject.Highlight("MapObjectClipboard", MapObjectClipboard.selectionColor, true);
				this.nextMapObjectForLinkWith = null;
				List<IMapObject> newMapObjects = new List<IMapObject>();
				foreach (IMapObject mapObject in this.mapObjects)
				{
					int newMapObjectID = this.mapObjectContainer.AddMapObjectClone(mapObject, true);
					IMapObject newMapObject;
					this.mapObjectContainer.TryGetMapObject(newMapObjectID, out newMapObject);
					if (newMapObject != null)
					{
						this.nextMapObjectForLinkWith = newMapObject;
						this.multiMapObject.Add(newMapObject);
					}
					newMapObjects.Add(newMapObject);
				}
				if (this.mapObjects.Count != 1)
				{
					this.nextMapObjectForLinkWith = null;
				}
				this.multiMapObject.Position = this.position;
				foreach (MapObjectClipboard.CopiedLinkData link in this.links)
				{
					IMapObject leftMapObject = null;
					IMapObject rightMapObject = null;
					if (link.LeftIndex >= 0 && link.LeftIndex < newMapObjects.Count)
					{
						leftMapObject = newMapObjects[link.LeftIndex];
					}
					if (link.RightIndex >= 0 && link.RightIndex < newMapObjects.Count)
					{
						rightMapObject = newMapObjects[link.RightIndex];
					}
					else if (this.DoesCreateCrosslinks())
					{
						rightMapObject = link.RightMapObject;
					}
					if (leftMapObject != null && rightMapObject != null)
					{
						if (this.selector != null)
						{
							if (this.selector.LinkAvailable(leftMapObject, rightMapObject))
							{
								this.selector.LinkObjects(leftMapObject, rightMapObject, (link.LinkData != null) ? link.LinkData.Clone() : null);
							}
						}
						else
						{
							this.mapObjectContainer.AddLink(leftMapObject, rightMapObject, (link.LinkData != null) ? link.LinkData.Clone() : null);
						}
					}
				}
				if (this.DoesAutolinkAddedObjects() && this.previousObjectForLinkWith != null && this.nextMapObjectForLinkWith != null && this.selector.LinkAvailable(this.previousObjectForLinkWith, this.nextMapObjectForLinkWith))
				{
					this.objectsAutolinked = this.selector.LinkObjects(this.previousObjectForLinkWith, this.nextMapObjectForLinkWith);
					if (this.objectsAutolinked)
					{
						this.selector.ArrangeLinkedObject(this.previousObjectForLinkWith);
						this.selector.ArrangeLinkedObject(this.nextMapObjectForLinkWith);
					}
				}
			}
		}

		// Token: 0x06001127 RID: 4391 RVA: 0x0007F4A4 File Offset: 0x0007E4A4
		public void Hide()
		{
			if (this.multiMapObject != null)
			{
				foreach (KeyValuePair<IMapObject, IMapObject> keyValuePair in this.multiMapObject.MapObjects)
				{
					this.mapObjectContainer.RemoveMapObject(keyValuePair.Key);
				}
				this.objectsAutolinked = false;
				this.previousObjectForLinkWith = null;
				this.nextMapObjectForLinkWith = null;
				this.multiMapObject.Destroy();
				this.multiMapObject = null;
			}
		}

		// Token: 0x17000371 RID: 881
		// (get) Token: 0x06001128 RID: 4392 RVA: 0x0007F538 File Offset: 0x0007E538
		// (set) Token: 0x06001129 RID: 4393 RVA: 0x0007F540 File Offset: 0x0007E540
		public Position Position
		{
			get
			{
				return this.position;
			}
			set
			{
				this.position = value;
				if (this.multiMapObject != null)
				{
					if (this.selector.AlignToGrid)
					{
						this.multiMapObject.Position = value.AlignToGrid(this.selector.GridStep);
					}
					else
					{
						this.multiMapObject.Position = value;
					}
					if (this.selector.PlaceAlongNormal)
					{
						foreach (KeyValuePair<IMapObject, IMapObject> keyValuePair in this.multiMapObject.MapObjects)
						{
							Rotation newRotation;
							if (this.selector.GetRotationAlongNormal(keyValuePair.Key, out newRotation))
							{
								keyValuePair.Key.Rotation = newRotation;
								keyValuePair.Value.Rotation = newRotation;
							}
						}
						this.multiMapObject.RecreateRotationMedian();
					}
					if (this.selector != null && this.DoesAutolinkAddedObjects() && this.objectsAutolinked)
					{
						if (this.previousObjectForLinkWith != null)
						{
							this.selector.ArrangeLinkedObject(this.previousObjectForLinkWith);
						}
						if (this.nextMapObjectForLinkWith != null)
						{
							this.selector.ArrangeLinkedObject(this.nextMapObjectForLinkWith);
						}
					}
				}
			}
		}

		// Token: 0x17000372 RID: 882
		// (get) Token: 0x0600112A RID: 4394 RVA: 0x0007F670 File Offset: 0x0007E670
		// (set) Token: 0x0600112B RID: 4395 RVA: 0x0007F68C File Offset: 0x0007E68C
		public Rotation Rotation
		{
			get
			{
				if (this.multiMapObject != null)
				{
					return this.multiMapObject.Rotation;
				}
				return Rotation.Empty;
			}
			set
			{
				if (this.multiMapObject != null)
				{
					this.multiMapObject.Rotation = value;
					if (this.selector != null && this.DoesAutolinkAddedObjects() && this.objectsAutolinked)
					{
						if (this.previousObjectForLinkWith != null)
						{
							this.selector.ArrangeLinkedObject(this.previousObjectForLinkWith);
						}
						if (this.nextMapObjectForLinkWith != null)
						{
							this.selector.ArrangeLinkedObject(this.nextMapObjectForLinkWith);
						}
					}
				}
			}
		}

		// Token: 0x17000373 RID: 883
		// (get) Token: 0x0600112C RID: 4396 RVA: 0x0007F6F7 File Offset: 0x0007E6F7
		// (set) Token: 0x0600112D RID: 4397 RVA: 0x0007F714 File Offset: 0x0007E714
		public Scale Scale
		{
			get
			{
				if (this.multiMapObject != null)
				{
					return this.multiMapObject.Scale;
				}
				return Scale.Empty;
			}
			set
			{
				if (this.multiMapObject != null)
				{
					this.multiMapObject.Scale = value;
					if (this.selector != null && this.DoesAutolinkAddedObjects() && this.objectsAutolinked)
					{
						if (this.previousObjectForLinkWith != null)
						{
							this.selector.ArrangeLinkedObject(this.previousObjectForLinkWith);
						}
						if (this.nextMapObjectForLinkWith != null)
						{
							this.selector.ArrangeLinkedObject(this.nextMapObjectForLinkWith);
						}
					}
				}
			}
		}

		// Token: 0x17000374 RID: 884
		// (get) Token: 0x0600112E RID: 4398 RVA: 0x0007F77F File Offset: 0x0007E77F
		// (set) Token: 0x0600112F RID: 4399 RVA: 0x0007F7A0 File Offset: 0x0007E7A0
		public double Altitude
		{
			get
			{
				if (this.multiMapObject != null)
				{
					return this.multiMapObject.Altitude;
				}
				return 0.0;
			}
			set
			{
				if (this.multiMapObject != null)
				{
					this.multiMapObject.Altitude = value;
					if (this.selector != null && this.DoesAutolinkAddedObjects() && this.objectsAutolinked)
					{
						if (this.previousObjectForLinkWith != null)
						{
							this.selector.ArrangeLinkedObject(this.previousObjectForLinkWith);
						}
						if (this.nextMapObjectForLinkWith != null)
						{
							this.selector.ArrangeLinkedObject(this.nextMapObjectForLinkWith);
						}
					}
				}
			}
		}

		// Token: 0x17000375 RID: 885
		// (get) Token: 0x06001130 RID: 4400 RVA: 0x0007F80B File Offset: 0x0007E80B
		public double AdditionalAltitude
		{
			get
			{
				return this.additionalAltitude;
			}
		}

		// Token: 0x17000376 RID: 886
		// (get) Token: 0x06001131 RID: 4401 RVA: 0x0007F813 File Offset: 0x0007E813
		public bool Empty
		{
			get
			{
				return this.mapObjects.Count == 0;
			}
		}

		// Token: 0x17000377 RID: 887
		// (get) Token: 0x06001132 RID: 4402 RVA: 0x0007F823 File Offset: 0x0007E823
		public bool ObjectsAutolinked
		{
			get
			{
				return this.objectsAutolinked;
			}
		}

		// Token: 0x04000C56 RID: 3158
		private const string highlightKey = "MapObjectClipboard";

		// Token: 0x04000C57 RID: 3159
		private static readonly Color selectionColor = Color.FromArgb(MapObjectCreationInfo.DefaultTransparentColorAlpha + 1, Color.Yellow);

		// Token: 0x04000C58 RID: 3160
		private MapObjectContainer mapObjectContainer;

		// Token: 0x04000C59 RID: 3161
		private ContinentType continentType;

		// Token: 0x04000C5A RID: 3162
		private MapObjectSelector selector;

		// Token: 0x04000C5B RID: 3163
		private readonly List<IMapObject> mapObjects = new List<IMapObject>();

		// Token: 0x04000C5C RID: 3164
		private readonly List<MapObjectClipboard.CopiedLinkData> links = new List<MapObjectClipboard.CopiedLinkData>();

		// Token: 0x04000C5D RID: 3165
		private IMapObject previousObjectForLinkWith;

		// Token: 0x04000C5E RID: 3166
		private IMapObject nextMapObjectForLinkWith;

		// Token: 0x04000C5F RID: 3167
		private bool objectsAutolinked;

		// Token: 0x04000C60 RID: 3168
		private MultiMapObject multiMapObject;

		// Token: 0x04000C61 RID: 3169
		private Position position = MapObjectCreationInfo.DefaultPosition;

		// Token: 0x04000C62 RID: 3170
		private double additionalAltitude;

		// Token: 0x02000163 RID: 355
		internal class CopiedLinkData
		{
			// Token: 0x06001134 RID: 4404 RVA: 0x0007F843 File Offset: 0x0007E843
			public CopiedLinkData(int _leftIndex, int _rightIndex, IMapObject _rightMapObject, ILinkData _linkData)
			{
				this.leftIndex = _leftIndex;
				this.rightIndex = _rightIndex;
				this.rightMapObject = _rightMapObject;
				this.linkData = _linkData;
			}

			// Token: 0x17000378 RID: 888
			// (get) Token: 0x06001135 RID: 4405 RVA: 0x0007F876 File Offset: 0x0007E876
			// (set) Token: 0x06001136 RID: 4406 RVA: 0x0007F87E File Offset: 0x0007E87E
			public int LeftIndex
			{
				get
				{
					return this.leftIndex;
				}
				set
				{
					this.leftIndex = value;
				}
			}

			// Token: 0x17000379 RID: 889
			// (get) Token: 0x06001137 RID: 4407 RVA: 0x0007F887 File Offset: 0x0007E887
			// (set) Token: 0x06001138 RID: 4408 RVA: 0x0007F88F File Offset: 0x0007E88F
			public int RightIndex
			{
				get
				{
					return this.rightIndex;
				}
				set
				{
					this.rightIndex = value;
				}
			}

			// Token: 0x1700037A RID: 890
			// (get) Token: 0x06001139 RID: 4409 RVA: 0x0007F898 File Offset: 0x0007E898
			// (set) Token: 0x0600113A RID: 4410 RVA: 0x0007F8A0 File Offset: 0x0007E8A0
			public IMapObject RightMapObject
			{
				get
				{
					return this.rightMapObject;
				}
				set
				{
					this.rightMapObject = value;
				}
			}

			// Token: 0x1700037B RID: 891
			// (get) Token: 0x0600113B RID: 4411 RVA: 0x0007F8A9 File Offset: 0x0007E8A9
			// (set) Token: 0x0600113C RID: 4412 RVA: 0x0007F8B1 File Offset: 0x0007E8B1
			public ILinkData LinkData
			{
				get
				{
					return this.linkData;
				}
				set
				{
					this.linkData = value;
				}
			}

			// Token: 0x04000C63 RID: 3171
			private int leftIndex = -1;

			// Token: 0x04000C64 RID: 3172
			private int rightIndex = -1;

			// Token: 0x04000C65 RID: 3173
			private IMapObject rightMapObject;

			// Token: 0x04000C66 RID: 3174
			private ILinkData linkData;
		}
	}
}
