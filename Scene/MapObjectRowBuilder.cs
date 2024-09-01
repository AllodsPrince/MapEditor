using System;
using System.Collections.Generic;
using System.Drawing;
using MapEditor.Map;
using MapEditor.Map.MapObjects;
using Tools.Geometry;
using Tools.Groups;
using Tools.MapObjects;
using Tools.WeightList;

namespace MapEditor.Scene
{
	// Token: 0x020000BB RID: 187
	internal class MapObjectRowBuilder
	{
		// Token: 0x06000999 RID: 2457 RVA: 0x0005059C File Offset: 0x0004F59C
		private void Create()
		{
			if (this.editorScene != null && this.objectSetSaver != null && this.objectSetSaver.Objects.Items.Count > 0)
			{
				foreach (WeightList<string>.WeightItem item in this.objectSetSaver.Objects.Items)
				{
					MapObjectRowBuilder.ObjectParams objectParam = new MapObjectRowBuilder.ObjectParams();
					Volume volume;
					this.editorScene.GetObjectVolume(item.Item, out volume);
					if (volume.Extents.Y > volume.Extents.X)
					{
						objectParam.Extents = volume.Extents.Y;
						objectParam.MaximumDelta = volume.Extents.X;
						objectParam.Center = volume.Center.Y;
						objectParam.AdditionalYaw = 1.5707964f;
					}
					else
					{
						objectParam.Extents = volume.Extents.X;
						objectParam.MaximumDelta = volume.Extents.Y;
						objectParam.Center = volume.Center.X;
						objectParam.AdditionalYaw = 0f;
					}
					this.objectParams.Add(item.Item, objectParam);
				}
				double avarageSize = 0.0;
				foreach (KeyValuePair<string, MapObjectRowBuilder.ObjectParams> keyValuePair in this.objectParams)
				{
					avarageSize += keyValuePair.Value.Extents;
				}
				if (this.objectParams.Count > 1)
				{
					avarageSize /= (double)this.objectParams.Count;
				}
				double bigAverageSize = 0.0;
				double smallAverageSize = 0.0;
				foreach (KeyValuePair<string, MapObjectRowBuilder.ObjectParams> keyValuePair2 in this.objectParams)
				{
					if (keyValuePair2.Value.Extents > avarageSize + MathConsts.DOUBLE_EPSILON)
					{
						this.bigObjects.Add(keyValuePair2.Key, this.objectSetSaver.Objects.GetWeight(keyValuePair2.Key));
						bigAverageSize += keyValuePair2.Value.Extents;
					}
					else
					{
						this.smallObjects.Add(keyValuePair2.Key, this.objectSetSaver.Objects.GetWeight(keyValuePair2.Key));
						smallAverageSize += keyValuePair2.Value.Extents;
					}
				}
				this.bigObjectsRatio = 1;
				if (this.bigObjects.Items.Count > 0 && this.smallObjects.Items.Count > 0)
				{
					if (this.bigObjects.Items.Count > 1)
					{
						bigAverageSize /= (double)this.bigObjects.Items.Count;
					}
					if (this.smallObjects.Items.Count > 1)
					{
						smallAverageSize /= (double)this.smallObjects.Items.Count;
					}
					this.bigObjectsRatio = (int)(bigAverageSize / smallAverageSize);
				}
			}
		}

		// Token: 0x0600099A RID: 2458 RVA: 0x0005091C File Offset: 0x0004F91C
		private bool RemoveObjectFromRow()
		{
			if (this.formattedObjectParams.Count > 0)
			{
				int filledIndex = this.formattedObjectParams.Count - 1;
				MapObjectRowBuilder.FormattedObjectParams formattedObjectParam = this.formattedObjectParams[filledIndex];
				if (this.visible && formattedObjectParam.VisibleMapOjbect != null)
				{
					this.mapObjectContainer.RemoveMapObject(formattedObjectParam.VisibleMapOjbect);
				}
				this.mapObjectTotalDistance -= formattedObjectParam.Distance;
				this.formattedObjectParams.RemoveAt(filledIndex);
				return true;
			}
			return false;
		}

		// Token: 0x0600099B RID: 2459 RVA: 0x00050998 File Offset: 0x0004F998
		private bool AddObjectToRow(string stats, Vec3 start, Vec3 direction, Rotation rotation, TerrainSurface surface, bool oneSided)
		{
			MapObjectType mapObjectType = MapObjectFactory.CreateMapObjectType(stats, this.editorScene);
			if (MapObjectFactory.Type.KnownType(mapObjectType.Type))
			{
				MapObjectRowBuilder.FormattedObjectParams formattedObjectParam = new MapObjectRowBuilder.FormattedObjectParams();
				MapObjectRowBuilder.ObjectParams objectParam;
				if (this.objectParams.TryGetValue(mapObjectType.Stats, out objectParam))
				{
					formattedObjectParam.AdditionalYaw = objectParam.AdditionalYaw;
					formattedObjectParam.Delta = objectParam.Extents - Math.Abs(objectParam.Center);
					if (formattedObjectParam.Delta > objectParam.MaximumDelta)
					{
						formattedObjectParam.Delta = objectParam.MaximumDelta;
					}
					if (oneSided)
					{
						formattedObjectParam.Distance = (objectParam.Extents - formattedObjectParam.Delta) * 2.0;
						formattedObjectParam.AdditionalDistance = objectParam.Extents - formattedObjectParam.Delta - objectParam.Center;
					}
					else if (this.formattedObjectParams.Count == 0)
					{
						formattedObjectParam.Distance = objectParam.Extents * 2.0 - formattedObjectParam.Delta;
						formattedObjectParam.AdditionalDistance = objectParam.Extents - formattedObjectParam.Delta - objectParam.Center;
					}
					else
					{
						formattedObjectParam.Distance = objectParam.Extents * 2.0;
						formattedObjectParam.AdditionalDistance = objectParam.Extents - objectParam.Center;
					}
					if (formattedObjectParam.Distance < objectParam.MaximumDelta)
					{
						formattedObjectParam.Distance = objectParam.MaximumDelta;
					}
					if (formattedObjectParam.Distance < MathConsts.DOUBLE_EPSILON)
					{
						formattedObjectParam.Distance = 1.0;
					}
					MapObjectCreationInfo mapObjectCreationInfo = new MapObjectCreationInfo();
					mapObjectCreationInfo.Position = new Position(start + direction * (this.mapObjectTotalDistance + formattedObjectParam.AdditionalDistance));
					mapObjectCreationInfo.Rotation = new Rotation(rotation.Yaw + formattedObjectParam.AdditionalYaw, 0f, 0f);
					mapObjectCreationInfo.Select = true;
					mapObjectCreationInfo.SelectionColor = MapObjectRowBuilder.selectionColor;
					IMapObject mapObject = this.mapObjectContainer.MapObjectFactory.CreateMapObject(-1, mapObjectType, this.mapObjectContainer, true, false, mapObjectCreationInfo);
					if (mapObject != null)
					{
						mapObject.Surface = surface;
						MapObjectFactory.FillMapObject(mapObject, stats, this.continentType);
						formattedObjectParam.MapObject = mapObject;
						if (this.visible)
						{
							int newMapObjectID = this.mapObjectContainer.AddMapObjectClone(formattedObjectParam.MapObject, true);
							IMapObject newMapObject;
							this.mapObjectContainer.TryGetMapObject(newMapObjectID, out newMapObject);
							if (newMapObject != null)
							{
								formattedObjectParam.VisibleMapOjbect = newMapObject;
							}
						}
						this.mapObjectTotalDistance += formattedObjectParam.Distance;
						this.formattedObjectParams.Add(formattedObjectParam);
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x0600099C RID: 2460 RVA: 0x00050BFC File Offset: 0x0004FBFC
		private void Show()
		{
			foreach (MapObjectRowBuilder.FormattedObjectParams formattedObjectParam in this.formattedObjectParams)
			{
				if (formattedObjectParam.VisibleMapOjbect != null)
				{
					this.mapObjectContainer.RemoveMapObject(formattedObjectParam.VisibleMapOjbect);
					formattedObjectParam.VisibleMapOjbect = null;
				}
				int newMapObjectID = this.mapObjectContainer.AddMapObjectClone(formattedObjectParam.MapObject, true);
				IMapObject newMapObject;
				this.mapObjectContainer.TryGetMapObject(newMapObjectID, out newMapObject);
				if (newMapObject != null)
				{
					formattedObjectParam.VisibleMapOjbect = newMapObject;
				}
			}
		}

		// Token: 0x0600099D RID: 2461 RVA: 0x00050C98 File Offset: 0x0004FC98
		private void Hide()
		{
			foreach (MapObjectRowBuilder.FormattedObjectParams formattedObjectParam in this.formattedObjectParams)
			{
				this.mapObjectContainer.RemoveMapObject(formattedObjectParam.VisibleMapOjbect);
				formattedObjectParam.VisibleMapOjbect = null;
			}
		}

		// Token: 0x0600099E RID: 2462 RVA: 0x00050D00 File Offset: 0x0004FD00
		public MapObjectRowBuilder(MapObjectContainer _mapObjectContainer, ContinentType _continentType, ObjectSetSaver _objectSetSaver, EditorScene _editorScene)
		{
			this.mapObjectContainer = _mapObjectContainer;
			this.continentType = _continentType;
			this.objectSetSaver = _objectSetSaver;
			this.editorScene = _editorScene;
			this.Create();
		}

		// Token: 0x0600099F RID: 2463 RVA: 0x00050D84 File Offset: 0x0004FD84
		public void Destroy()
		{
			this.Clear();
			this.visible = false;
			this.formattedObjectParams = null;
			this.objectParams = null;
			this.bigObjects = null;
			this.smallObjects = null;
			this.mapObjectContainer = null;
			this.continentType = ContinentType.Unknown;
			this.objectSetSaver = null;
			this.editorScene = null;
			this.random = null;
		}

		// Token: 0x170001F3 RID: 499
		// (get) Token: 0x060009A0 RID: 2464 RVA: 0x00050DDD File Offset: 0x0004FDDD
		// (set) Token: 0x060009A1 RID: 2465 RVA: 0x00050DE5 File Offset: 0x0004FDE5
		public bool Visible
		{
			get
			{
				return this.visible;
			}
			set
			{
				if (this.visible != value)
				{
					this.visible = value;
					if (this.visible)
					{
						this.Show();
						return;
					}
					this.Hide();
				}
			}
		}

		// Token: 0x060009A2 RID: 2466 RVA: 0x00050E0C File Offset: 0x0004FE0C
		public void FormatRow(Vec3 start, Vec3 end, TerrainSurface surface, bool oneSided, bool recreateObjects, out Vec3 _end)
		{
			_end = Vec3.Empty;
			if (this.editorScene != null)
			{
				Vec3 direction = end - start;
				double distance = direction.Normalize();
				Quat quat = new Quat(direction);
				Rotation rotation = new Rotation(ref quat);
				if (recreateObjects)
				{
					this.Clear();
				}
				if (this.formattedObjectParams.Count > 0)
				{
					int filledIndex = 0;
					double filledDistance = 0.0;
					while (filledIndex < this.formattedObjectParams.Count)
					{
						if (filledDistance >= distance)
						{
							break;
						}
						MapObjectRowBuilder.FormattedObjectParams formattedObjectParam = this.formattedObjectParams[filledIndex];
						if (formattedObjectParam.MapObject != null)
						{
							formattedObjectParam.MapObject.Surface = surface;
							formattedObjectParam.MapObject.Position = new Position(start + direction * (filledDistance + formattedObjectParam.AdditionalDistance));
							formattedObjectParam.MapObject.Rotation = new Rotation(rotation.Yaw + formattedObjectParam.AdditionalYaw, 0f, 0f);
						}
						if (this.visible && formattedObjectParam.VisibleMapOjbect != null)
						{
							formattedObjectParam.VisibleMapOjbect.Surface = surface;
							formattedObjectParam.VisibleMapOjbect.Position = new Position(start + direction * (filledDistance + formattedObjectParam.AdditionalDistance));
							formattedObjectParam.VisibleMapOjbect.Rotation = new Rotation(rotation.Yaw + formattedObjectParam.AdditionalYaw, 0f, 0f);
						}
						filledDistance += this.formattedObjectParams[filledIndex].Distance;
						filledIndex++;
					}
					while (filledIndex < this.formattedObjectParams.Count && this.RemoveObjectFromRow())
					{
						if (this.smallObjectsCount > 0)
						{
							this.smallObjectsCount--;
						}
					}
					if (this.bigObjectsRatio > 1 && this.smallObjectsCount == 0 && this.formattedObjectParams.Count > 0 && filledDistance - distance > MathConsts.DOUBLE_EPSILON && this.formattedObjectParams[this.formattedObjectParams.Count - 1].Distance / (filledDistance - distance) < (double)this.bigObjectsRatio)
					{
						this.RemoveObjectFromRow();
					}
				}
				while (this.mapObjectTotalDistance < distance)
				{
					if (this.bigObjectsRatio > 1)
					{
						if (this.smallObjectsCount == this.bigObjectsRatio)
						{
							int index = 0;
							while (index < this.bigObjectsRatio && this.RemoveObjectFromRow())
							{
								index++;
							}
							this.smallObjectsCount = 0;
							string stats = this.bigObjects.Get();
							if (!this.AddObjectToRow(stats, start, direction, rotation, surface, oneSided))
							{
								break;
							}
						}
						else
						{
							string stats2 = this.smallObjects.Get();
							if (!this.AddObjectToRow(stats2, start, direction, rotation, surface, oneSided))
							{
								break;
							}
							this.smallObjectsCount++;
						}
					}
					else
					{
						string stats3 = this.objectSetSaver.Objects.Get();
						if (!this.AddObjectToRow(stats3, start, direction, rotation, surface, oneSided))
						{
							break;
						}
					}
				}
				if (oneSided)
				{
					_end = start + direction * this.mapObjectTotalDistance;
					return;
				}
				double delta = 0.0;
				if (this.formattedObjectParams.Count > 0)
				{
					delta = this.formattedObjectParams[this.formattedObjectParams.Count - 1].Delta;
				}
				_end = start + direction * (this.mapObjectTotalDistance + delta);
			}
		}

		// Token: 0x060009A3 RID: 2467 RVA: 0x00051164 File Offset: 0x00050164
		public void Paste(List<IMapObject> addedMapObjects, string defaultGroupName)
		{
			if (string.IsNullOrEmpty(defaultGroupName))
			{
				string groupIndex = this.random.Next().ToString();
				List<string[]> groupNameList = new List<string[]>();
				int commonGroupIndex = -1;
				List<IMapObject> mapObjects = new List<IMapObject>();
				foreach (MapObjectRowBuilder.FormattedObjectParams formattedObjectParam in this.formattedObjectParams)
				{
					mapObjects.Add(formattedObjectParam.MapObject);
				}
				GroupContainer.SplitGroupNames(mapObjects, ref groupNameList, ref commonGroupIndex);
				if (commonGroupIndex <= -1)
				{
					goto IL_1C0;
				}
				int index = 0;
				using (List<IMapObject>.Enumerator enumerator2 = mapObjects.GetEnumerator())
				{
					while (enumerator2.MoveNext())
					{
						IMapObject mapObject = enumerator2.Current;
						string newGroupName = "";
						int groupCount = groupNameList[index].Length;
						for (int i = 0; i < groupCount; i++)
						{
							if (i == commonGroupIndex)
							{
								groupNameList[index][i] = groupNameList[index][i].TrimEnd("0123456789".ToCharArray());
								newGroupName += groupNameList[index][i];
								newGroupName += groupIndex;
								newGroupName += '/';
							}
							else
							{
								newGroupName += groupNameList[index][i];
								newGroupName += '/';
							}
						}
						index++;
						newGroupName = newGroupName.TrimEnd(new char[]
						{
							'/'
						});
						mapObject.GroupName = newGroupName;
					}
					goto IL_1C0;
				}
			}
			foreach (MapObjectRowBuilder.FormattedObjectParams formattedObjectParam2 in this.formattedObjectParams)
			{
				formattedObjectParam2.MapObject.GroupName = defaultGroupName;
			}
			IL_1C0:
			foreach (MapObjectRowBuilder.FormattedObjectParams formattedObjectParam3 in this.formattedObjectParams)
			{
				int mapObjectID = this.mapObjectContainer.AddMapObjectClone(formattedObjectParam3.MapObject, false);
				IMapObject addedMapObject;
				this.mapObjectContainer.MapObjects.TryGetValue(mapObjectID, out addedMapObject);
				if (addedMapObject != null)
				{
					addedMapObject.Select = MapObjectCreationInfo.DefaultSelect;
					addedMapObject.Highlight(null, MapObjectCreationInfo.DefaultHighlightColor, false);
					if (addedMapObjects != null)
					{
						addedMapObjects.Add(addedMapObject);
					}
				}
			}
		}

		// Token: 0x060009A4 RID: 2468 RVA: 0x000513EC File Offset: 0x000503EC
		public void Clear()
		{
			if (this.visible)
			{
				this.Hide();
			}
			this.mapObjectTotalDistance = 0.0;
			this.smallObjectsCount = 0;
			this.formattedObjectParams.Clear();
		}

		// Token: 0x170001F4 RID: 500
		// (get) Token: 0x060009A5 RID: 2469 RVA: 0x0005141D File Offset: 0x0005041D
		public bool Empty
		{
			get
			{
				return this.formattedObjectParams.Count == 0;
			}
		}

		// Token: 0x040007B9 RID: 1977
		private static readonly Color selectionColor = Color.FromArgb(MapObjectCreationInfo.DefaultTransparentColorAlpha + 1, Color.Yellow);

		// Token: 0x040007BA RID: 1978
		private MapObjectContainer mapObjectContainer;

		// Token: 0x040007BB RID: 1979
		private ObjectSetSaver objectSetSaver;

		// Token: 0x040007BC RID: 1980
		private ContinentType continentType;

		// Token: 0x040007BD RID: 1981
		private EditorScene editorScene;

		// Token: 0x040007BE RID: 1982
		private List<MapObjectRowBuilder.FormattedObjectParams> formattedObjectParams = new List<MapObjectRowBuilder.FormattedObjectParams>();

		// Token: 0x040007BF RID: 1983
		private Dictionary<string, MapObjectRowBuilder.ObjectParams> objectParams = new Dictionary<string, MapObjectRowBuilder.ObjectParams>();

		// Token: 0x040007C0 RID: 1984
		private WeightList<string> bigObjects = new WeightList<string>();

		// Token: 0x040007C1 RID: 1985
		private WeightList<string> smallObjects = new WeightList<string>();

		// Token: 0x040007C2 RID: 1986
		private int bigObjectsRatio = 1;

		// Token: 0x040007C3 RID: 1987
		private double mapObjectTotalDistance;

		// Token: 0x040007C4 RID: 1988
		private int smallObjectsCount;

		// Token: 0x040007C5 RID: 1989
		private bool visible;

		// Token: 0x040007C6 RID: 1990
		private Random random = new Random(DateTime.Now.Millisecond);

		// Token: 0x020000BC RID: 188
		internal class ObjectParams
		{
			// Token: 0x170001F5 RID: 501
			// (get) Token: 0x060009A7 RID: 2471 RVA: 0x00051445 File Offset: 0x00050445
			// (set) Token: 0x060009A8 RID: 2472 RVA: 0x0005144D File Offset: 0x0005044D
			public double Extents
			{
				get
				{
					return this.extents;
				}
				set
				{
					this.extents = value;
				}
			}

			// Token: 0x170001F6 RID: 502
			// (get) Token: 0x060009A9 RID: 2473 RVA: 0x00051456 File Offset: 0x00050456
			// (set) Token: 0x060009AA RID: 2474 RVA: 0x0005145E File Offset: 0x0005045E
			public double MaximumDelta
			{
				get
				{
					return this.maximumDelta;
				}
				set
				{
					this.maximumDelta = value;
				}
			}

			// Token: 0x170001F7 RID: 503
			// (get) Token: 0x060009AB RID: 2475 RVA: 0x00051467 File Offset: 0x00050467
			// (set) Token: 0x060009AC RID: 2476 RVA: 0x0005146F File Offset: 0x0005046F
			public double Center
			{
				get
				{
					return this.center;
				}
				set
				{
					this.center = value;
				}
			}

			// Token: 0x170001F8 RID: 504
			// (get) Token: 0x060009AD RID: 2477 RVA: 0x00051478 File Offset: 0x00050478
			// (set) Token: 0x060009AE RID: 2478 RVA: 0x00051480 File Offset: 0x00050480
			public float AdditionalYaw
			{
				get
				{
					return this.additionalYaw;
				}
				set
				{
					this.additionalYaw = value;
				}
			}

			// Token: 0x040007C7 RID: 1991
			private double extents;

			// Token: 0x040007C8 RID: 1992
			private double maximumDelta;

			// Token: 0x040007C9 RID: 1993
			private double center;

			// Token: 0x040007CA RID: 1994
			private float additionalYaw;
		}

		// Token: 0x020000BD RID: 189
		internal class FormattedObjectParams
		{
			// Token: 0x170001F9 RID: 505
			// (get) Token: 0x060009B0 RID: 2480 RVA: 0x00051491 File Offset: 0x00050491
			// (set) Token: 0x060009B1 RID: 2481 RVA: 0x00051499 File Offset: 0x00050499
			public IMapObject MapObject
			{
				get
				{
					return this.mapObject;
				}
				set
				{
					this.mapObject = value;
				}
			}

			// Token: 0x170001FA RID: 506
			// (get) Token: 0x060009B2 RID: 2482 RVA: 0x000514A2 File Offset: 0x000504A2
			// (set) Token: 0x060009B3 RID: 2483 RVA: 0x000514AA File Offset: 0x000504AA
			public IMapObject VisibleMapOjbect
			{
				get
				{
					return this.visibleMapOjbect;
				}
				set
				{
					this.visibleMapOjbect = value;
				}
			}

			// Token: 0x170001FB RID: 507
			// (get) Token: 0x060009B4 RID: 2484 RVA: 0x000514B3 File Offset: 0x000504B3
			// (set) Token: 0x060009B5 RID: 2485 RVA: 0x000514BB File Offset: 0x000504BB
			public double Distance
			{
				get
				{
					return this.distance;
				}
				set
				{
					this.distance = value;
				}
			}

			// Token: 0x170001FC RID: 508
			// (get) Token: 0x060009B6 RID: 2486 RVA: 0x000514C4 File Offset: 0x000504C4
			// (set) Token: 0x060009B7 RID: 2487 RVA: 0x000514CC File Offset: 0x000504CC
			public double AdditionalDistance
			{
				get
				{
					return this.additionalDistance;
				}
				set
				{
					this.additionalDistance = value;
				}
			}

			// Token: 0x170001FD RID: 509
			// (get) Token: 0x060009B8 RID: 2488 RVA: 0x000514D5 File Offset: 0x000504D5
			// (set) Token: 0x060009B9 RID: 2489 RVA: 0x000514DD File Offset: 0x000504DD
			public float AdditionalYaw
			{
				get
				{
					return this.additionalYaw;
				}
				set
				{
					this.additionalYaw = value;
				}
			}

			// Token: 0x170001FE RID: 510
			// (get) Token: 0x060009BA RID: 2490 RVA: 0x000514E6 File Offset: 0x000504E6
			// (set) Token: 0x060009BB RID: 2491 RVA: 0x000514EE File Offset: 0x000504EE
			public double Delta
			{
				get
				{
					return this.delta;
				}
				set
				{
					this.delta = value;
				}
			}

			// Token: 0x040007CB RID: 1995
			private IMapObject mapObject;

			// Token: 0x040007CC RID: 1996
			private IMapObject visibleMapOjbect;

			// Token: 0x040007CD RID: 1997
			private double distance;

			// Token: 0x040007CE RID: 1998
			private double additionalDistance;

			// Token: 0x040007CF RID: 1999
			private float additionalYaw;

			// Token: 0x040007D0 RID: 2000
			private double delta;
		}
	}
}
