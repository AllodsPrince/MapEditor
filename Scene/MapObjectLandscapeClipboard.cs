using System;
using System.Collections.Generic;
using System.Drawing;
using Tools.Geometry;
using Tools.Groups;
using Tools.LinkContainer;
using Tools.MapObjects;

namespace MapEditor.Scene
{
	// Token: 0x0200019D RID: 413
	internal class MapObjectLandscapeClipboard
	{
		// Token: 0x06001421 RID: 5153 RVA: 0x00091A88 File Offset: 0x00090A88
		public MapObjectLandscapeClipboard(MapObjectContainer _mapObjectContainer, List<IMapObject> _mapObjects, Dictionary<int, Dictionary<int, ILinkData>> _links, bool _move)
		{
			this.mapObjectContainer = _mapObjectContainer;
			this.mapObjects = _mapObjects;
			this.links = _links;
			this.move = _move;
		}

		// Token: 0x06001422 RID: 5154 RVA: 0x00091B05 File Offset: 0x00090B05
		public void Destroy()
		{
			this.Hide();
			this.mapObjects = null;
			this.links = null;
			this.mapObjectContainer = null;
			this.move = false;
			this.startMapObjectPack = null;
			this.startMapObjectPacks = null;
		}

		// Token: 0x06001423 RID: 5155 RVA: 0x00091B38 File Offset: 0x00090B38
		public void Paste(List<IMapObject> addedMapObjects, string defaultGroupName)
		{
			if (!this.move)
			{
				List<IMapObject> newMapObjects = new List<IMapObject>();
				if (this.visibleMapObjects.Count > 0)
				{
					GroupContainer.AddGroupName(this.visibleMapObjects, defaultGroupName);
					foreach (KeyValuePair<IMapObject, IMapObject> keyValuePair in this.visibleMapObjects)
					{
						int mapObjectID = this.mapObjectContainer.AddMapObjectClone(keyValuePair.Key, false);
						IMapObject addedMapObject;
						this.mapObjectContainer.MapObjects.TryGetValue(mapObjectID, out addedMapObject);
						if (addedMapObject != null)
						{
							addedMapObject.Select = keyValuePair.Value.Select;
							addedMapObject.RemoveHighlight("MapObjectLandscapeClipboard");
							if (addedMapObjects != null)
							{
								addedMapObjects.Add(addedMapObject);
							}
						}
						newMapObjects.Add(addedMapObject);
					}
					foreach (KeyValuePair<int, Dictionary<int, ILinkData>> left in this.links)
					{
						if (left.Key >= 0 && left.Key < newMapObjects.Count)
						{
							IMapObject leftMapObject = newMapObjects[left.Key];
							foreach (KeyValuePair<int, ILinkData> right in left.Value)
							{
								if (right.Key >= 0 && right.Key < newMapObjects.Count)
								{
									IMapObject rightMapObject = newMapObjects[right.Key];
									if (leftMapObject != null && rightMapObject != null)
									{
										this.mapObjectContainer.AddLink(leftMapObject, rightMapObject, (right.Value != null) ? right.Value.Clone() : null);
									}
								}
							}
						}
					}
				}
			}
		}

		// Token: 0x06001424 RID: 5156 RVA: 0x00091D14 File Offset: 0x00090D14
		public void UpdateVisibleMapObjects()
		{
			if (!this.move)
			{
				if (this.mapObjects == null || this.visibleMapObjects == null)
				{
					return;
				}
				int count = this.visibleMapObjects.Count;
				if (count <= 0)
				{
					return;
				}
				Quat newQuat = new Quat((double)this.rotation.Yaw, 0.0, 0.0);
				Quat oldQuat = new Quat(Rotation.Empty);
				Quat quatDelta = newQuat * oldQuat.Conjugate;
				using (Dictionary<IMapObject, IMapObject>.Enumerator enumerator = this.visibleMapObjects.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						KeyValuePair<IMapObject, IMapObject> keyValuePair = enumerator.Current;
						IMapObject visibleMapObject = keyValuePair.Key;
						IMapObject mapObject = keyValuePair.Value;
						if (visibleMapObject != null && mapObject != null)
						{
							Position newPosition = this.position + new Position(quatDelta.Rotate(mapObject.Position.Vec3 * (double)this.scale.Ratio));
							newPosition.Z = this.mapObjectContainer.GetTerrainHeight(newPosition.X, newPosition.Y) + mapObject.Altitude;
							if (this.mapObjectContainer.ObjectBounds != null)
							{
								this.mapObjectContainer.ObjectBounds.PositionBounds.Validate(ref newPosition);
							}
							visibleMapObject.Position = newPosition;
							Quat _oldQuat = new Quat(mapObject.Rotation);
							double yaw;
							double pitch;
							double roll;
							(quatDelta * _oldQuat).GetYawPitchRoll(out yaw, out pitch, out roll);
							Rotation newRotation = new Rotation((float)yaw, (float)pitch, (float)roll);
							if (this.mapObjectContainer.ObjectBounds != null)
							{
								this.mapObjectContainer.ObjectBounds.RotationBounds.Validate(ref newRotation);
							}
							visibleMapObject.Rotation = newRotation;
							visibleMapObject.Scale = mapObject.Scale * this.scale.Ratio;
						}
					}
					return;
				}
			}
			if (this.mapObjects != null && this.startMapObjectPack != null && this.startMapObjectPacks != null)
			{
				int count2 = this.startMapObjectPacks.Count;
				if (count2 > 0)
				{
					Quat newQuat2 = new Quat((double)this.rotation.Yaw, 0.0, 0.0);
					Quat oldQuat2 = new Quat(Rotation.Empty);
					Quat quatDelta2 = newQuat2 * oldQuat2.Conjugate;
					for (int index = 0; index < count2; index++)
					{
						if (index < this.mapObjects.Count)
						{
							MapObjectPack mapObjectPack = this.startMapObjectPacks[index];
							IMapObject mapObject2 = this.mapObjects[index];
							if (mapObjectPack != null && mapObject2 != null)
							{
								Position newPosition2 = this.position + new Position(quatDelta2.Rotate((mapObjectPack.Position - this.startMapObjectPack.Position).Vec3 * (double)this.scale.Ratio));
								newPosition2.Z = this.mapObjectContainer.GetTerrainHeight(newPosition2.X, newPosition2.Y) + mapObjectPack.Altitude;
								if (this.mapObjectContainer.ObjectBounds != null)
								{
									this.mapObjectContainer.ObjectBounds.PositionBounds.Validate(ref newPosition2);
								}
								mapObject2.Position = newPosition2;
								Quat _oldQuat2 = new Quat(mapObjectPack.Rotation);
								double yaw2;
								double pitch2;
								double roll2;
								(quatDelta2 * _oldQuat2).GetYawPitchRoll(out yaw2, out pitch2, out roll2);
								Rotation newRotation2 = new Rotation((float)yaw2, (float)pitch2, (float)roll2);
								if (this.mapObjectContainer.ObjectBounds != null)
								{
									this.mapObjectContainer.ObjectBounds.RotationBounds.Validate(ref newRotation2);
								}
								mapObject2.Rotation = newRotation2;
								mapObject2.Scale = mapObjectPack.Scale * this.scale.Ratio;
							}
						}
					}
				}
			}
		}

		// Token: 0x06001425 RID: 5157 RVA: 0x00092114 File Offset: 0x00091114
		private void Show()
		{
			if (!this.move)
			{
				if (this.mapObjects == null || this.mapObjects.Count <= 0)
				{
					return;
				}
				this.Hide();
				int count = this.mapObjects.Count;
				for (int index = 0; index < count; index++)
				{
					int newLeftMapObjectID = this.mapObjectContainer.AddMapObjectClone(this.mapObjects[index], true);
					IMapObject visibleMapObject;
					this.mapObjectContainer.TryGetMapObject(newLeftMapObjectID, out visibleMapObject);
					if (visibleMapObject != null)
					{
						visibleMapObject.Select = true;
						visibleMapObject.Highlight("MapObjectLandscapeClipboard", MapObjectLandscapeClipboard.selectionColor, true);
					}
					this.visibleMapObjects.Add(visibleMapObject, this.mapObjects[index]);
					this.visibleMapObjectsListForLinkage.Add(visibleMapObject);
				}
				this.UpdateVisibleMapObjects();
				if (this.links == null || this.links.Count <= 0)
				{
					return;
				}
				using (Dictionary<int, Dictionary<int, ILinkData>>.Enumerator enumerator = this.links.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						KeyValuePair<int, Dictionary<int, ILinkData>> left = enumerator.Current;
						if (left.Key >= 0 && left.Key < this.visibleMapObjects.Count)
						{
							IMapObject leftMapObject = this.visibleMapObjectsListForLinkage[left.Key];
							foreach (KeyValuePair<int, ILinkData> right in left.Value)
							{
								if (right.Key >= 0 && right.Key < this.visibleMapObjects.Count)
								{
									IMapObject rightMapObject = this.visibleMapObjectsListForLinkage[right.Key];
									if (leftMapObject != null && rightMapObject != null)
									{
										this.mapObjectContainer.AddLink(leftMapObject, rightMapObject, (right.Value != null) ? right.Value.Clone() : null);
									}
								}
							}
						}
					}
					return;
				}
			}
			if (this.mapObjects != null && this.startMapObjectPack != null && this.startMapObjectPacks != null)
			{
				this.startMapObjectPack.Position = this.position;
				this.startMapObjectPack.Rotation = this.rotation;
				this.startMapObjectPack.Scale = this.scale;
				this.startMapObjectPacks.Clear();
				for (int index2 = 0; index2 < this.mapObjects.Count; index2++)
				{
					this.startMapObjectPacks.Add(new MapObjectPack());
					this.startMapObjectPacks[index2].Pack(this.mapObjects[index2]);
				}
			}
		}

		// Token: 0x06001426 RID: 5158 RVA: 0x000923BC File Offset: 0x000913BC
		private void Hide()
		{
			if (!this.move)
			{
				if (this.visibleMapObjects.Count > 0)
				{
					foreach (KeyValuePair<IMapObject, IMapObject> keyValuePair in this.visibleMapObjects)
					{
						this.mapObjectContainer.RemoveMapObject(keyValuePair.Key);
					}
					this.visibleMapObjects.Clear();
					this.visibleMapObjectsListForLinkage.Clear();
					return;
				}
			}
			else
			{
				this.startMapObjectPacks.Clear();
			}
		}

		// Token: 0x06001427 RID: 5159 RVA: 0x00092454 File Offset: 0x00091454
		public void SetClipboardParams(ref Position _position, ref Rotation _rotation, ref Scale _scale, bool update)
		{
			this.position = _position;
			this.rotation = _rotation;
			this.scale = _scale;
			if (update)
			{
				this.UpdateVisibleMapObjects();
			}
		}

		// Token: 0x170003F4 RID: 1012
		// (get) Token: 0x06001428 RID: 5160 RVA: 0x00092484 File Offset: 0x00091484
		// (set) Token: 0x06001429 RID: 5161 RVA: 0x0009248C File Offset: 0x0009148C
		public Position Position
		{
			get
			{
				return this.position;
			}
			set
			{
				this.position = value;
				this.UpdateVisibleMapObjects();
			}
		}

		// Token: 0x170003F5 RID: 1013
		// (get) Token: 0x0600142A RID: 5162 RVA: 0x0009249B File Offset: 0x0009149B
		// (set) Token: 0x0600142B RID: 5163 RVA: 0x000924A3 File Offset: 0x000914A3
		public Rotation Rotation
		{
			get
			{
				return this.rotation;
			}
			set
			{
				this.rotation = value;
				this.UpdateVisibleMapObjects();
			}
		}

		// Token: 0x170003F6 RID: 1014
		// (get) Token: 0x0600142C RID: 5164 RVA: 0x000924B2 File Offset: 0x000914B2
		// (set) Token: 0x0600142D RID: 5165 RVA: 0x000924BA File Offset: 0x000914BA
		public Scale Scale
		{
			get
			{
				return this.scale;
			}
			set
			{
				this.scale = value;
				this.UpdateVisibleMapObjects();
			}
		}

		// Token: 0x170003F7 RID: 1015
		// (get) Token: 0x0600142E RID: 5166 RVA: 0x000924C9 File Offset: 0x000914C9
		// (set) Token: 0x0600142F RID: 5167 RVA: 0x000924D1 File Offset: 0x000914D1
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

		// Token: 0x170003F8 RID: 1016
		// (get) Token: 0x06001430 RID: 5168 RVA: 0x000924F8 File Offset: 0x000914F8
		public bool Empty
		{
			get
			{
				return this.mapObjects.Count == 0;
			}
		}

		// Token: 0x04000E3C RID: 3644
		private const string highlighKey = "MapObjectLandscapeClipboard";

		// Token: 0x04000E3D RID: 3645
		private static readonly Color selectionColor = Color.FromArgb(MapObjectCreationInfo.DefaultTransparentColorAlpha + 1, Color.Yellow);

		// Token: 0x04000E3E RID: 3646
		private MapObjectContainer mapObjectContainer;

		// Token: 0x04000E3F RID: 3647
		private List<IMapObject> mapObjects;

		// Token: 0x04000E40 RID: 3648
		private Dictionary<int, Dictionary<int, ILinkData>> links;

		// Token: 0x04000E41 RID: 3649
		private bool move;

		// Token: 0x04000E42 RID: 3650
		private MapObjectPack startMapObjectPack = new MapObjectPack();

		// Token: 0x04000E43 RID: 3651
		private List<MapObjectPack> startMapObjectPacks = new List<MapObjectPack>();

		// Token: 0x04000E44 RID: 3652
		private readonly Dictionary<IMapObject, IMapObject> visibleMapObjects = new Dictionary<IMapObject, IMapObject>();

		// Token: 0x04000E45 RID: 3653
		private readonly List<IMapObject> visibleMapObjectsListForLinkage = new List<IMapObject>();

		// Token: 0x04000E46 RID: 3654
		private bool visible;

		// Token: 0x04000E47 RID: 3655
		private Position position = Position.Empty;

		// Token: 0x04000E48 RID: 3656
		private Rotation rotation = Rotation.Empty;

		// Token: 0x04000E49 RID: 3657
		private Scale scale = Scale.Normal;
	}
}
