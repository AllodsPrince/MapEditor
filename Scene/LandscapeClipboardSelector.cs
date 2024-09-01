using System;
using System.ComponentModel;
using System.Xml.Serialization;
using InputState;
using LauncherTools.InputState;
using Tools.Geometry;
using Tools.MapObjects;

namespace MapEditor.Scene
{
	// Token: 0x020001BF RID: 447
	public class LandscapeClipboardSelector
	{
		// Token: 0x14000094 RID: 148
		// (add) Token: 0x060016F5 RID: 5877 RVA: 0x0009E5BE File Offset: 0x0009D5BE
		// (remove) Token: 0x060016F6 RID: 5878 RVA: 0x0009E5D7 File Offset: 0x0009D5D7
		public event LandscapeClipboardSelector.ChangeEvent Changing;

		// Token: 0x14000095 RID: 149
		// (add) Token: 0x060016F7 RID: 5879 RVA: 0x0009E5F0 File Offset: 0x0009D5F0
		// (remove) Token: 0x060016F8 RID: 5880 RVA: 0x0009E609 File Offset: 0x0009D609
		public event LandscapeClipboardSelector.ChangeEvent Changed;

		// Token: 0x14000096 RID: 150
		// (add) Token: 0x060016F9 RID: 5881 RVA: 0x0009E622 File Offset: 0x0009D622
		// (remove) Token: 0x060016FA RID: 5882 RVA: 0x0009E63B File Offset: 0x0009D63B
		public event LandscapeClipboardSelector.FieldChangedEvent<Position> PositionChanged;

		// Token: 0x14000097 RID: 151
		// (add) Token: 0x060016FB RID: 5883 RVA: 0x0009E654 File Offset: 0x0009D654
		// (remove) Token: 0x060016FC RID: 5884 RVA: 0x0009E66D File Offset: 0x0009D66D
		public event LandscapeClipboardSelector.FieldChangedEvent<Rotation> RotationChanged;

		// Token: 0x14000098 RID: 152
		// (add) Token: 0x060016FD RID: 5885 RVA: 0x0009E686 File Offset: 0x0009D686
		// (remove) Token: 0x060016FE RID: 5886 RVA: 0x0009E69F File Offset: 0x0009D69F
		public event LandscapeClipboardSelector.FieldChangedEvent<Scale> ScaleChanged;

		// Token: 0x14000099 RID: 153
		// (add) Token: 0x060016FF RID: 5887 RVA: 0x0009E6B8 File Offset: 0x0009D6B8
		// (remove) Token: 0x06001700 RID: 5888 RVA: 0x0009E6D1 File Offset: 0x0009D6D1
		public event LandscapeClipboardSelector.FieldChangedEvent<bool> ObjectOrientedChanged;

		// Token: 0x1400009A RID: 154
		// (add) Token: 0x06001701 RID: 5889 RVA: 0x0009E6EA File Offset: 0x0009D6EA
		// (remove) Token: 0x06001702 RID: 5890 RVA: 0x0009E703 File Offset: 0x0009D703
		public event LandscapeClipboardSelector.FieldChangedEvent<bool> VisibleChanged;

		// Token: 0x06001703 RID: 5891 RVA: 0x0009E71C File Offset: 0x0009D71C
		private void CreateSelectorData()
		{
			LandscapeClipboardSelector.SelectorData _selectorData = Serializer.Load<LandscapeClipboardSelector.SelectorData>(EditorEnvironment.EditorFormsFolder + this.selectorDataFileName);
			if (_selectorData != null)
			{
				this.selectorData = _selectorData;
			}
			else
			{
				this.selectorData = new LandscapeClipboardSelector.SelectorData();
			}
			this.selectorData.SelectorTypeChanged += this.OnSelectorTypeChanged;
			this.selectorData.ObjectOrientedChanged += this.OnObjectOrientedChanged;
			this.selectorData.Active = true;
		}

		// Token: 0x06001704 RID: 5892 RVA: 0x0009E790 File Offset: 0x0009D790
		private void DestroySelectorData()
		{
			if (this.selectorData != null)
			{
				Serializer.Save(EditorEnvironment.EditorFormsFolder + this.selectorDataFileName, this.selectorData, false);
				this.selectorData.SelectorTypeChanged -= this.OnSelectorTypeChanged;
				this.selectorData.ObjectOrientedChanged -= this.OnObjectOrientedChanged;
				this.selectorData = null;
			}
		}

		// Token: 0x06001705 RID: 5893 RVA: 0x0009E7F8 File Offset: 0x0009D7F8
		private void CreateWidgetData(LandscapeClipboardSelector.SelectorData _selectorData, EditorScene _editorScene)
		{
			this.widgetData = new LandscapeClipboardSelector.WidgetData(_editorScene);
			this.widgetData.Changing += this.OnWidgetDataChanging;
			this.widgetData.Changed += this.OnWidgetDataChanged;
			this.widgetData.PositionChanged += this.OnWidgetDataPositionChanged;
			this.widgetData.RotationChanged += this.OnWidgetDataRotationChanged;
			this.widgetData.ScaleChanged += this.OnWidgetDataScaleChanged;
			this.widgetData.VisibleChanged += this.OnWidgetDataVisibleChanged;
			this.widgetData.Bind(_selectorData);
		}

		// Token: 0x06001706 RID: 5894 RVA: 0x0009E8A8 File Offset: 0x0009D8A8
		private void DestroyWidgetData()
		{
			if (this.widgetData != null)
			{
				this.widgetData.Unbind();
				this.widgetData.Changing -= this.OnWidgetDataChanging;
				this.widgetData.Changed -= this.OnWidgetDataChanged;
				this.widgetData.PositionChanged -= this.OnWidgetDataPositionChanged;
				this.widgetData.RotationChanged -= this.OnWidgetDataRotationChanged;
				this.widgetData.ScaleChanged -= this.OnWidgetDataScaleChanged;
				this.widgetData = null;
			}
		}

		// Token: 0x06001707 RID: 5895 RVA: 0x0009E945 File Offset: 0x0009D945
		private void OnSelectorMove(MethodArgs methodArgs)
		{
			if (this.selectorData != null)
			{
				this.selectorData.SelectorType = LandscapeClipboardSelector.SelectorType.Move;
			}
		}

		// Token: 0x06001708 RID: 5896 RVA: 0x0009E95B File Offset: 0x0009D95B
		private void OnSelectorRotate(MethodArgs methodArgs)
		{
			if (this.selectorData != null)
			{
				this.selectorData.SelectorType = LandscapeClipboardSelector.SelectorType.Rotate;
			}
		}

		// Token: 0x06001709 RID: 5897 RVA: 0x0009E971 File Offset: 0x0009D971
		private void OnSelectorScale(MethodArgs methodArgs)
		{
			if (this.selectorData != null)
			{
				this.selectorData.SelectorType = LandscapeClipboardSelector.SelectorType.Scale;
			}
		}

		// Token: 0x0600170A RID: 5898 RVA: 0x0009E987 File Offset: 0x0009D987
		private void OnSelectorSizeUp(MethodArgs methodArgs)
		{
			if (this.selectorData != null)
			{
				this.selectorData.SizeUpScale();
			}
		}

		// Token: 0x0600170B RID: 5899 RVA: 0x0009E99D File Offset: 0x0009D99D
		private void OnSelectorSizeDown(MethodArgs methodArgs)
		{
			if (this.selectorData != null)
			{
				this.selectorData.SizeDownScale();
			}
		}

		// Token: 0x0600170C RID: 5900 RVA: 0x0009E9B3 File Offset: 0x0009D9B3
		private void OnSelectorObjectOriented(MethodArgs methodArgs)
		{
			if (this.selectorData != null)
			{
				this.selectorData.ObjectOriented = !this.selectorData.ObjectOriented;
			}
		}

		// Token: 0x0600170D RID: 5901 RVA: 0x0009E9D6 File Offset: 0x0009D9D6
		private void OnSelectorTypeChanged(LandscapeClipboardSelector.SelectorData _selectorData, ref LandscapeClipboardSelector.SelectorType oldValue, ref LandscapeClipboardSelector.SelectorType newValue)
		{
			this.UpdateSelectorTypeControls();
		}

		// Token: 0x0600170E RID: 5902 RVA: 0x0009E9DE File Offset: 0x0009D9DE
		private void OnObjectOrientedChanged(LandscapeClipboardSelector.SelectorData _selectorData, ref bool oldValue, ref bool newValue)
		{
			this.UpdateObjectOrientedControls();
			if (this.ObjectOrientedChanged != null)
			{
				this.ObjectOrientedChanged(this, ref oldValue, ref newValue);
			}
		}

		// Token: 0x0600170F RID: 5903 RVA: 0x0009E9FC File Offset: 0x0009D9FC
		private void OnWidgetDataChanging(LandscapeClipboardSelector.WidgetData _widgetData)
		{
			if (this.widgetData.Active && this.Changing != null)
			{
				this.Changing(this);
			}
		}

		// Token: 0x06001710 RID: 5904 RVA: 0x0009EA1F File Offset: 0x0009DA1F
		private void OnWidgetDataChanged(LandscapeClipboardSelector.WidgetData _widgetData)
		{
			if (this.widgetData.Active && this.Changed != null)
			{
				this.Changed(this);
			}
		}

		// Token: 0x06001711 RID: 5905 RVA: 0x0009EA42 File Offset: 0x0009DA42
		private void OnWidgetDataPositionChanged(LandscapeClipboardSelector.WidgetData _widgetData, ref Position oldValue, ref Position newValue)
		{
			if (this.widgetData.Active && this.PositionChanged != null)
			{
				this.PositionChanged(this, ref oldValue, ref newValue);
			}
		}

		// Token: 0x06001712 RID: 5906 RVA: 0x0009EA67 File Offset: 0x0009DA67
		private void OnWidgetDataRotationChanged(LandscapeClipboardSelector.WidgetData _widgetData, ref Rotation oldValue, ref Rotation newValue)
		{
			if (this.widgetData.Active && this.RotationChanged != null)
			{
				this.RotationChanged(this, ref oldValue, ref newValue);
			}
		}

		// Token: 0x06001713 RID: 5907 RVA: 0x0009EA8C File Offset: 0x0009DA8C
		private void OnWidgetDataScaleChanged(LandscapeClipboardSelector.WidgetData _widgetData, ref Scale oldValue, ref Scale newValue)
		{
			if (this.widgetData.Active && this.ScaleChanged != null)
			{
				this.ScaleChanged(this, ref oldValue, ref newValue);
			}
		}

		// Token: 0x06001714 RID: 5908 RVA: 0x0009EAB1 File Offset: 0x0009DAB1
		private void OnWidgetDataVisibleChanged(LandscapeClipboardSelector.WidgetData _widgetData, ref bool oldValue, ref bool newValue)
		{
			if (this.widgetData.Active && this.VisibleChanged != null)
			{
				this.VisibleChanged(this, ref oldValue, ref newValue);
			}
		}

		// Token: 0x06001715 RID: 5909 RVA: 0x0009EAD8 File Offset: 0x0009DAD8
		private void UpdateSelectorTypeControls()
		{
			this.selectorState.SetMethodParams("selector_move", true, true, true, this.selectorData.SelectorType == LandscapeClipboardSelector.SelectorType.Move);
			this.selectorState.SetMethodParams("selector_rotate", true, true, true, this.selectorData.SelectorType == LandscapeClipboardSelector.SelectorType.Rotate);
			this.selectorState.SetMethodParams("selector_scale", true, true, true, this.selectorData.SelectorType == LandscapeClipboardSelector.SelectorType.Scale);
		}

		// Token: 0x06001716 RID: 5910 RVA: 0x0009EB48 File Offset: 0x0009DB48
		private void UpdateObjectOrientedControls()
		{
			this.selectorState.SetMethodParams("selector_object_oriented", true, true, true, this.selectorData.ObjectOriented);
		}

		// Token: 0x06001717 RID: 5911 RVA: 0x0009EB68 File Offset: 0x0009DB68
		public LandscapeClipboardSelector(StateContainer _stateContainer, ICollisionMap _collisionMap, EditorScene _editorScene, string _selectorDataFileName)
		{
			this.stateContainer = _stateContainer;
			this.collisionMap = _collisionMap;
			this.selectorDataFileName = _selectorDataFileName;
			this.CreateSelectorData();
			this.CreateWidgetData(this.selectorData, _editorScene);
			if (this.stateContainer != null)
			{
				this.selectorState = new State("LandscapeClipboardSelectorState");
				this.selectorState.AddMethod("selector_move", new Method(this.OnSelectorMove));
				this.selectorState.AddMethod("selector_rotate", new Method(this.OnSelectorRotate));
				this.selectorState.AddMethod("selector_scale", new Method(this.OnSelectorScale));
				this.selectorState.AddMethod("selector_object_oriented", new Method(this.OnSelectorObjectOriented));
				this.selectorState.AddMethod("selector_size_up", new Method(this.OnSelectorSizeUp));
				this.selectorState.AddMethod("selector_size_down", new Method(this.OnSelectorSizeDown));
			}
		}

		// Token: 0x06001718 RID: 5912 RVA: 0x0009EC72 File Offset: 0x0009DC72
		public void Destroy()
		{
			this.Unbind();
			this.DestroyWidgetData();
			this.DestroySelectorData();
			if (this.stateContainer != null)
			{
				this.selectorState = null;
				this.stateContainer = null;
			}
			this.collisionMap = null;
		}

		// Token: 0x06001719 RID: 5913 RVA: 0x0009ECA3 File Offset: 0x0009DCA3
		public void Bind()
		{
			if (this.stateContainer != null && this.selectorState != null)
			{
				this.stateContainer.BindState(this.selectorState);
			}
			this.UpdateSelectorTypeControls();
			this.UpdateObjectOrientedControls();
			this.widgetData.Active = true;
		}

		// Token: 0x0600171A RID: 5914 RVA: 0x0009ECDE File Offset: 0x0009DCDE
		public void Unbind()
		{
			this.widgetData.Active = false;
			if (this.stateContainer != null && this.selectorState != null)
			{
				this.stateContainer.UnbindState(this.selectorState);
			}
		}

		// Token: 0x17000597 RID: 1431
		// (set) Token: 0x0600171B RID: 5915 RVA: 0x0009ED0D File Offset: 0x0009DD0D
		[Browsable(false)]
		public bool Active
		{
			set
			{
				if (this.widgetData != null)
				{
					this.widgetData.Active = value;
				}
			}
		}

		// Token: 0x17000598 RID: 1432
		// (get) Token: 0x0600171C RID: 5916 RVA: 0x0009ED23 File Offset: 0x0009DD23
		// (set) Token: 0x0600171D RID: 5917 RVA: 0x0009ED3A File Offset: 0x0009DD3A
		[Browsable(false)]
		public bool Visible
		{
			get
			{
				return this.widgetData != null && this.widgetData.Visible;
			}
			set
			{
				if (this.widgetData != null)
				{
					this.widgetData.Visible = value;
				}
			}
		}

		// Token: 0x17000599 RID: 1433
		// (get) Token: 0x0600171E RID: 5918 RVA: 0x0009ED50 File Offset: 0x0009DD50
		// (set) Token: 0x0600171F RID: 5919 RVA: 0x0009ED58 File Offset: 0x0009DD58
		[Browsable(false)]
		public double AdditionalHeight
		{
			get
			{
				return this.additionalHeight;
			}
			set
			{
				this.additionalHeight = value;
			}
		}

		// Token: 0x1700059A RID: 1434
		// (get) Token: 0x06001720 RID: 5920 RVA: 0x0009ED61 File Offset: 0x0009DD61
		// (set) Token: 0x06001721 RID: 5921 RVA: 0x0009ED7C File Offset: 0x0009DD7C
		[Browsable(true)]
		[Category("Geometry")]
		[TypeConverter(typeof(PositionConverter))]
		public Position Position
		{
			get
			{
				if (this.widgetData != null)
				{
					return this.widgetData.Position;
				}
				return Position.Empty;
			}
			set
			{
				if (this.widgetData != null)
				{
					this.widgetData.Position = value;
				}
			}
		}

		// Token: 0x1700059B RID: 1435
		// (get) Token: 0x06001722 RID: 5922 RVA: 0x0009ED92 File Offset: 0x0009DD92
		// (set) Token: 0x06001723 RID: 5923 RVA: 0x0009EDAD File Offset: 0x0009DDAD
		[TypeConverter(typeof(RotationConverter))]
		[Category("Geometry")]
		[Browsable(true)]
		public Rotation Rotation
		{
			get
			{
				if (this.widgetData != null)
				{
					return this.widgetData.Rotation;
				}
				return Rotation.Empty;
			}
			set
			{
				if (this.widgetData != null)
				{
					this.widgetData.Rotation = value;
				}
			}
		}

		// Token: 0x1700059C RID: 1436
		// (get) Token: 0x06001724 RID: 5924 RVA: 0x0009EDC3 File Offset: 0x0009DDC3
		// (set) Token: 0x06001725 RID: 5925 RVA: 0x0009EDDE File Offset: 0x0009DDDE
		[TypeConverter(typeof(ScaleConverter))]
		[Browsable(true)]
		[Category("Geometry")]
		public Scale Scale
		{
			get
			{
				if (this.widgetData != null)
				{
					return this.widgetData.Scale;
				}
				return Scale.Empty;
			}
			set
			{
				if (this.widgetData != null)
				{
					this.widgetData.Scale = value;
				}
			}
		}

		// Token: 0x1700059D RID: 1437
		// (get) Token: 0x06001726 RID: 5926 RVA: 0x0009EDF4 File Offset: 0x0009DDF4
		[Browsable(false)]
		public bool Picked
		{
			get
			{
				return this.startPickData != null;
			}
		}

		// Token: 0x06001727 RID: 5927 RVA: 0x0009EE04 File Offset: 0x0009DE04
		public void Pick(int x, int y)
		{
			if (this.widgetData.Created)
			{
				this.selectorData.SelectedPart = LandscapeClipboardSelector.SelectedPart.Unknown;
				Position positon = this.Position;
				Rotation rotation = this.Rotation;
				Scale scale = this.Scale;
				this.startPickData = new PickData(x, y, this.collisionMap, ref positon, ref rotation, ref scale, this.selectorData.ObjectOriented, TerrainSurface.Terrain);
				double pickAxisRadius = (double)this.startPickData.Scale.Ratio * this.selectorData.Scale * (double)LandscapeClipboardSelector.axisArrowRadius;
				double pickAxisSize = (double)this.startPickData.Scale.Ratio * this.selectorData.Scale * (double)(LandscapeClipboardSelector.axisSize - LandscapeClipboardSelector.axisArrowRadius);
				Vec3 centerSpot = this.startPickData.Position.Vec3;
				Vec3 axisXSpot = centerSpot + this.startPickData.AxisData.AxisX * pickAxisSize;
				Vec3 axisYSpot = centerSpot + this.startPickData.AxisData.AxisY * pickAxisSize;
				Vec3 axisZSpot = centerSpot + this.startPickData.AxisData.AxisZ * pickAxisSize;
				if (this.startPickData.ProjectiveRay.GetDistance(centerSpot) < pickAxisRadius)
				{
					if (this.selectorData.SelectorType == LandscapeClipboardSelector.SelectorType.Move)
					{
						this.selectorData.SelectedPart = LandscapeClipboardSelector.SelectedPart.Center;
						return;
					}
					if (this.selectorData.SelectorType == LandscapeClipboardSelector.SelectorType.Scale)
					{
						this.selectorData.SelectedPart = LandscapeClipboardSelector.SelectedPart.Center;
					}
					return;
				}
				else if (this.startPickData.ProjectiveRay.GetDistance(axisXSpot) < pickAxisRadius)
				{
					if (this.selectorData.SelectorType == LandscapeClipboardSelector.SelectorType.Rotate)
					{
						this.startPickData.SavedPoint = this.startPickData.PickedPointOnPlaneZ;
					}
					if (this.selectorData.SelectorType == LandscapeClipboardSelector.SelectorType.Scale && KeyStatus.Shift)
					{
						this.selectorData.SelectedPart = LandscapeClipboardSelector.SelectedPart.Center;
						return;
					}
					this.selectorData.SelectedPart = LandscapeClipboardSelector.SelectedPart.AxisX;
					return;
				}
				else if (this.startPickData.ProjectiveRay.GetDistance(axisYSpot) < pickAxisRadius)
				{
					if (this.selectorData.SelectorType == LandscapeClipboardSelector.SelectorType.Rotate)
					{
						this.ClearPick();
						return;
					}
					if (this.selectorData.SelectorType == LandscapeClipboardSelector.SelectorType.Scale && KeyStatus.Shift)
					{
						this.selectorData.SelectedPart = LandscapeClipboardSelector.SelectedPart.Center;
						return;
					}
					this.selectorData.SelectedPart = LandscapeClipboardSelector.SelectedPart.AxisY;
					return;
				}
				else if (this.startPickData.ProjectiveRay.GetDistance(axisZSpot) < pickAxisRadius)
				{
					if (this.selectorData.SelectorType == LandscapeClipboardSelector.SelectorType.Rotate)
					{
						this.ClearPick();
						return;
					}
					if (this.selectorData.SelectorType == LandscapeClipboardSelector.SelectorType.Scale && KeyStatus.Shift)
					{
						this.selectorData.SelectedPart = LandscapeClipboardSelector.SelectedPart.Center;
						return;
					}
					this.selectorData.SelectedPart = LandscapeClipboardSelector.SelectedPart.AxisZ;
					return;
				}
				else
				{
					this.ClearPick();
				}
			}
		}

		// Token: 0x06001728 RID: 5928 RVA: 0x0009F0B4 File Offset: 0x0009E0B4
		public void ProcessPick(int x, int y)
		{
			if (this.Picked)
			{
				PickData currentPickData = this.startPickData.GetNewIntersection(x, y, this.collisionMap, TerrainSurface.Terrain);
				if (this.selectorData.SelectorType == LandscapeClipboardSelector.SelectorType.Move)
				{
					if (this.selectorData.SelectedPart == LandscapeClipboardSelector.SelectedPart.Center)
					{
						this.Position = this.startPickData.Position + (currentPickData.PickedPointOnPlaneZ - this.startPickData.PickedPointOnPlaneZ);
						return;
					}
					if (this.selectorData.SelectedPart == LandscapeClipboardSelector.SelectedPart.AxisX)
					{
						this.Position = this.startPickData.Position + (currentPickData.PickedPointOnAxisX - this.startPickData.PickedPointOnAxisX);
						return;
					}
					if (this.selectorData.SelectedPart == LandscapeClipboardSelector.SelectedPart.AxisY)
					{
						this.Position = this.startPickData.Position + (currentPickData.PickedPointOnAxisY - this.startPickData.PickedPointOnAxisY);
						return;
					}
					if (this.selectorData.SelectedPart == LandscapeClipboardSelector.SelectedPart.AxisZ)
					{
						double oldZ = this.Position.Z;
						this.Position = this.startPickData.Position + (currentPickData.PickedPointOnAxisZ - this.startPickData.PickedPointOnAxisZ);
						this.additionalHeight += this.Position.Z - oldZ;
						return;
					}
				}
				else if (this.selectorData.SelectorType == LandscapeClipboardSelector.SelectorType.Rotate)
				{
					Vec3 start = Vec3.Empty;
					Vec3 current = Vec3.Empty;
					bool filled = false;
					if (this.selectorData.SelectedPart == LandscapeClipboardSelector.SelectedPart.AxisX)
					{
						start = this.startPickData.SavedPoint - this.startPickData.Position.Vec3;
						current = currentPickData.PickedPointOnPlaneZ - this.startPickData.Position.Vec3;
						this.startPickData.SavedPoint = currentPickData.PickedPointOnPlaneZ;
						filled = true;
					}
					if (filled)
					{
						Vec3 v = start;
						Vec3 v2 = start + current;
						v.Normalize();
						v2.Normalize();
						Vec3 cross = Vec3.Cross(v, v2);
						double dot = Vec3.Dot(v, v2);
						Quat startQuat = new Quat(this.Rotation);
						Quat currentQuat = new Quat(cross.X, cross.Y, cross.Z, dot);
						currentQuat *= startQuat;
						double yaw;
						double pitch;
						double roll;
						currentQuat.GetYawPitchRoll(out yaw, out pitch, out roll);
						this.Rotation = new Rotation((float)yaw, (float)pitch, (float)roll);
						return;
					}
				}
				else if (this.selectorData.SelectorType == LandscapeClipboardSelector.SelectorType.Scale)
				{
					double startDistanceToCenter = this.startPickData.DistanceToCenter;
					double currentDistanceToCenter = currentPickData.DistanceToCenter;
					if (startDistanceToCenter < 1.0)
					{
						startDistanceToCenter += 1.0;
						currentDistanceToCenter += 1.0;
					}
					float _ratio = this.startPickData.Scale.Ratio * (float)(currentDistanceToCenter / startDistanceToCenter);
					float _x = (this.selectorData.SelectedPart == LandscapeClipboardSelector.SelectedPart.AxisX || this.selectorData.SelectedPart == LandscapeClipboardSelector.SelectedPart.Center) ? (this.startPickData.Scale.X * (float)(currentDistanceToCenter / startDistanceToCenter)) : this.startPickData.Scale.X;
					float _y = (this.selectorData.SelectedPart == LandscapeClipboardSelector.SelectedPart.AxisY || this.selectorData.SelectedPart == LandscapeClipboardSelector.SelectedPart.Center) ? (this.startPickData.Scale.Y * (float)(currentDistanceToCenter / startDistanceToCenter)) : this.startPickData.Scale.Y;
					float _z = (this.selectorData.SelectedPart == LandscapeClipboardSelector.SelectedPart.AxisZ || this.selectorData.SelectedPart == LandscapeClipboardSelector.SelectedPart.Center) ? (this.startPickData.Scale.Z * (float)(currentDistanceToCenter / startDistanceToCenter)) : this.startPickData.Scale.Z;
					float _radius = (this.selectorData.SelectedPart == LandscapeClipboardSelector.SelectedPart.AxisX || this.selectorData.SelectedPart == LandscapeClipboardSelector.SelectedPart.AxisY || this.selectorData.SelectedPart == LandscapeClipboardSelector.SelectedPart.Center) ? (this.startPickData.Scale.Radius * (float)(currentDistanceToCenter / startDistanceToCenter)) : this.startPickData.Scale.Radius;
					this.Scale = new Scale(_ratio, _x, _y, _z, _radius);
				}
			}
		}

		// Token: 0x06001729 RID: 5929 RVA: 0x0009F4F2 File Offset: 0x0009E4F2
		public void ClearPick()
		{
			this.selectorData.SelectedPart = LandscapeClipboardSelector.SelectedPart.Unknown;
			this.startPickData = null;
		}

		// Token: 0x04000F27 RID: 3879
		private static readonly float axisArrowRadius = 0.1f;

		// Token: 0x04000F28 RID: 3880
		private static readonly float axisSize = 1.5f;

		// Token: 0x04000F29 RID: 3881
		private StateContainer stateContainer;

		// Token: 0x04000F2A RID: 3882
		private ICollisionMap collisionMap;

		// Token: 0x04000F2B RID: 3883
		private State selectorState;

		// Token: 0x04000F2C RID: 3884
		private readonly string selectorDataFileName = string.Empty;

		// Token: 0x04000F2D RID: 3885
		private LandscapeClipboardSelector.SelectorData selectorData;

		// Token: 0x04000F2E RID: 3886
		private PickData startPickData;

		// Token: 0x04000F2F RID: 3887
		private LandscapeClipboardSelector.WidgetData widgetData;

		// Token: 0x04000F30 RID: 3888
		private double additionalHeight;

		// Token: 0x020001C0 RID: 448
		public enum SelectorType
		{
			// Token: 0x04000F39 RID: 3897
			Unknown,
			// Token: 0x04000F3A RID: 3898
			Move,
			// Token: 0x04000F3B RID: 3899
			Rotate,
			// Token: 0x04000F3C RID: 3900
			Scale
		}

		// Token: 0x020001C1 RID: 449
		public enum SelectedPart
		{
			// Token: 0x04000F3E RID: 3902
			Unknown,
			// Token: 0x04000F3F RID: 3903
			AxisX,
			// Token: 0x04000F40 RID: 3904
			AxisY,
			// Token: 0x04000F41 RID: 3905
			AxisZ,
			// Token: 0x04000F42 RID: 3906
			Center
		}

		// Token: 0x020001C2 RID: 450
		public class SelectorData
		{
			// Token: 0x0600172B RID: 5931 RVA: 0x0009F520 File Offset: 0x0009E520
			private bool ChangeScale(double newScale)
			{
				if (newScale > LandscapeClipboardSelector.SelectorData.maxScale)
				{
					newScale = LandscapeClipboardSelector.SelectorData.maxScale;
				}
				else if (newScale < LandscapeClipboardSelector.SelectorData.minScale)
				{
					newScale = LandscapeClipboardSelector.SelectorData.minScale;
				}
				if (newScale != this.scale)
				{
					this.InvokeChanging();
					double oldScale = this.scale;
					this.scale = newScale;
					this.InvokeChanged();
					if (this.active && this.ScaleChanged != null)
					{
						this.ScaleChanged(this, ref oldScale, ref this.scale);
					}
					return true;
				}
				return false;
			}

			// Token: 0x1400009B RID: 155
			// (add) Token: 0x0600172C RID: 5932 RVA: 0x0009F597 File Offset: 0x0009E597
			// (remove) Token: 0x0600172D RID: 5933 RVA: 0x0009F5B0 File Offset: 0x0009E5B0
			public event LandscapeClipboardSelector.SelectorData.ChangeEvent Changing;

			// Token: 0x1400009C RID: 156
			// (add) Token: 0x0600172E RID: 5934 RVA: 0x0009F5C9 File Offset: 0x0009E5C9
			// (remove) Token: 0x0600172F RID: 5935 RVA: 0x0009F5E2 File Offset: 0x0009E5E2
			public event LandscapeClipboardSelector.SelectorData.ChangeEvent Changed;

			// Token: 0x1400009D RID: 157
			// (add) Token: 0x06001730 RID: 5936 RVA: 0x0009F5FB File Offset: 0x0009E5FB
			// (remove) Token: 0x06001731 RID: 5937 RVA: 0x0009F614 File Offset: 0x0009E614
			public event LandscapeClipboardSelector.SelectorData.FieldChangedEvent<LandscapeClipboardSelector.SelectorType> SelectorTypeChanged;

			// Token: 0x1400009E RID: 158
			// (add) Token: 0x06001732 RID: 5938 RVA: 0x0009F62D File Offset: 0x0009E62D
			// (remove) Token: 0x06001733 RID: 5939 RVA: 0x0009F646 File Offset: 0x0009E646
			public event LandscapeClipboardSelector.SelectorData.FieldChangedEvent<LandscapeClipboardSelector.SelectedPart> SelectedPartChanged;

			// Token: 0x1400009F RID: 159
			// (add) Token: 0x06001734 RID: 5940 RVA: 0x0009F65F File Offset: 0x0009E65F
			// (remove) Token: 0x06001735 RID: 5941 RVA: 0x0009F678 File Offset: 0x0009E678
			public event LandscapeClipboardSelector.SelectorData.FieldChangedEvent<bool> ObjectOrientedChanged;

			// Token: 0x140000A0 RID: 160
			// (add) Token: 0x06001736 RID: 5942 RVA: 0x0009F691 File Offset: 0x0009E691
			// (remove) Token: 0x06001737 RID: 5943 RVA: 0x0009F6AA File Offset: 0x0009E6AA
			public event LandscapeClipboardSelector.SelectorData.FieldChangedEvent<double> ScaleChanged;

			// Token: 0x06001738 RID: 5944 RVA: 0x0009F6C3 File Offset: 0x0009E6C3
			public void InvokeChanging()
			{
				if (this.active && this.Changing != null)
				{
					this.Changing(this);
				}
			}

			// Token: 0x06001739 RID: 5945 RVA: 0x0009F6E1 File Offset: 0x0009E6E1
			public void InvokeChanged()
			{
				if (this.active && this.Changed != null)
				{
					this.Changed(this);
				}
			}

			// Token: 0x1700059E RID: 1438
			// (set) Token: 0x0600173A RID: 5946 RVA: 0x0009F6FF File Offset: 0x0009E6FF
			[XmlIgnore]
			public bool Active
			{
				set
				{
					this.active = value;
				}
			}

			// Token: 0x1700059F RID: 1439
			// (get) Token: 0x0600173B RID: 5947 RVA: 0x0009F708 File Offset: 0x0009E708
			// (set) Token: 0x0600173C RID: 5948 RVA: 0x0009F710 File Offset: 0x0009E710
			public LandscapeClipboardSelector.SelectorType SelectorType
			{
				get
				{
					return this.selectorType;
				}
				set
				{
					if (this.selectorType != value)
					{
						this.InvokeChanging();
						LandscapeClipboardSelector.SelectorType oldSelectorType = this.selectorType;
						this.selectorType = value;
						this.InvokeChanged();
						if (this.active && this.SelectorTypeChanged != null)
						{
							this.SelectorTypeChanged(this, ref oldSelectorType, ref this.selectorType);
						}
					}
				}
			}

			// Token: 0x170005A0 RID: 1440
			// (get) Token: 0x0600173D RID: 5949 RVA: 0x0009F764 File Offset: 0x0009E764
			// (set) Token: 0x0600173E RID: 5950 RVA: 0x0009F76C File Offset: 0x0009E76C
			[XmlIgnore]
			public LandscapeClipboardSelector.SelectedPart SelectedPart
			{
				get
				{
					return this.selectedPart;
				}
				set
				{
					if (this.selectedPart != value)
					{
						this.InvokeChanging();
						LandscapeClipboardSelector.SelectedPart oldSelectedPart = this.selectedPart;
						this.selectedPart = value;
						this.InvokeChanged();
						if (this.active && this.SelectedPartChanged != null)
						{
							this.SelectedPartChanged(this, ref oldSelectedPart, ref this.selectedPart);
						}
					}
				}
			}

			// Token: 0x170005A1 RID: 1441
			// (get) Token: 0x0600173F RID: 5951 RVA: 0x0009F7C0 File Offset: 0x0009E7C0
			// (set) Token: 0x06001740 RID: 5952 RVA: 0x0009F7C8 File Offset: 0x0009E7C8
			public bool ObjectOriented
			{
				get
				{
					return this.objectOriented;
				}
				set
				{
					if (this.objectOriented != value)
					{
						this.InvokeChanging();
						bool oldObjectOriented = this.objectOriented;
						this.objectOriented = value;
						this.InvokeChanged();
						if (this.active && this.ObjectOrientedChanged != null)
						{
							this.ObjectOrientedChanged(this, ref oldObjectOriented, ref this.objectOriented);
						}
					}
				}
			}

			// Token: 0x170005A2 RID: 1442
			// (get) Token: 0x06001741 RID: 5953 RVA: 0x0009F81C File Offset: 0x0009E81C
			// (set) Token: 0x06001742 RID: 5954 RVA: 0x0009F824 File Offset: 0x0009E824
			public double Scale
			{
				get
				{
					return this.scale;
				}
				set
				{
					this.ChangeScale(value);
				}
			}

			// Token: 0x06001743 RID: 5955 RVA: 0x0009F82E File Offset: 0x0009E82E
			public bool SizeUpScale()
			{
				return this.ChangeScale(this.scale * LandscapeClipboardSelector.SelectorData.scaleMultiplier);
			}

			// Token: 0x06001744 RID: 5956 RVA: 0x0009F842 File Offset: 0x0009E842
			public bool SizeDownScale()
			{
				return this.ChangeScale(this.scale / LandscapeClipboardSelector.SelectorData.scaleMultiplier);
			}

			// Token: 0x04000F43 RID: 3907
			private static readonly double maxScale = 256.0;

			// Token: 0x04000F44 RID: 3908
			private static readonly double minScale = 1.0;

			// Token: 0x04000F45 RID: 3909
			private static readonly double scaleMultiplier = 1.5;

			// Token: 0x04000F46 RID: 3910
			private static readonly double defaultScale = 4.0;

			// Token: 0x04000F47 RID: 3911
			private bool active;

			// Token: 0x04000F48 RID: 3912
			private LandscapeClipboardSelector.SelectorType selectorType = LandscapeClipboardSelector.SelectorType.Move;

			// Token: 0x04000F49 RID: 3913
			private LandscapeClipboardSelector.SelectedPart selectedPart;

			// Token: 0x04000F4A RID: 3914
			private bool objectOriented = true;

			// Token: 0x04000F4B RID: 3915
			private double scale = LandscapeClipboardSelector.SelectorData.defaultScale;

			// Token: 0x020001C3 RID: 451
			// (Invoke) Token: 0x06001748 RID: 5960
			public delegate void ChangeEvent(LandscapeClipboardSelector.SelectorData selectorData);

			// Token: 0x020001C4 RID: 452
			// (Invoke) Token: 0x0600174C RID: 5964
			public delegate void FieldChangedEvent<T>(LandscapeClipboardSelector.SelectorData selectorData, ref T oldValue, ref T newValue);
		}

		// Token: 0x020001C5 RID: 453
		private class WidgetData
		{
			// Token: 0x140000A1 RID: 161
			// (add) Token: 0x0600174F RID: 5967 RVA: 0x0009F8B1 File Offset: 0x0009E8B1
			// (remove) Token: 0x06001750 RID: 5968 RVA: 0x0009F8CA File Offset: 0x0009E8CA
			public event LandscapeClipboardSelector.WidgetData.ChangeEvent Changing;

			// Token: 0x140000A2 RID: 162
			// (add) Token: 0x06001751 RID: 5969 RVA: 0x0009F8E3 File Offset: 0x0009E8E3
			// (remove) Token: 0x06001752 RID: 5970 RVA: 0x0009F8FC File Offset: 0x0009E8FC
			public event LandscapeClipboardSelector.WidgetData.ChangeEvent Changed;

			// Token: 0x140000A3 RID: 163
			// (add) Token: 0x06001753 RID: 5971 RVA: 0x0009F915 File Offset: 0x0009E915
			// (remove) Token: 0x06001754 RID: 5972 RVA: 0x0009F92E File Offset: 0x0009E92E
			public event LandscapeClipboardSelector.WidgetData.FieldChangedEvent<Position> PositionChanged;

			// Token: 0x140000A4 RID: 164
			// (add) Token: 0x06001755 RID: 5973 RVA: 0x0009F947 File Offset: 0x0009E947
			// (remove) Token: 0x06001756 RID: 5974 RVA: 0x0009F960 File Offset: 0x0009E960
			public event LandscapeClipboardSelector.WidgetData.FieldChangedEvent<Rotation> RotationChanged;

			// Token: 0x140000A5 RID: 165
			// (add) Token: 0x06001757 RID: 5975 RVA: 0x0009F979 File Offset: 0x0009E979
			// (remove) Token: 0x06001758 RID: 5976 RVA: 0x0009F992 File Offset: 0x0009E992
			public event LandscapeClipboardSelector.WidgetData.FieldChangedEvent<Scale> ScaleChanged;

			// Token: 0x140000A6 RID: 166
			// (add) Token: 0x06001759 RID: 5977 RVA: 0x0009F9AB File Offset: 0x0009E9AB
			// (remove) Token: 0x0600175A RID: 5978 RVA: 0x0009F9C4 File Offset: 0x0009E9C4
			public event LandscapeClipboardSelector.WidgetData.FieldChangedEvent<bool> VisibleChanged;

			// Token: 0x0600175B RID: 5979 RVA: 0x0009F9DD File Offset: 0x0009E9DD
			private void InvokeChanging()
			{
				if (this.active && this.Changing != null)
				{
					this.Changing(this);
				}
			}

			// Token: 0x0600175C RID: 5980 RVA: 0x0009F9FB File Offset: 0x0009E9FB
			private void InvokeChanged()
			{
				if (this.active && this.Changed != null)
				{
					this.Changed(this);
				}
			}

			// Token: 0x0600175D RID: 5981 RVA: 0x0009FA1C File Offset: 0x0009EA1C
			private void CreateUserGeometry()
			{
				if (!this.visible)
				{
					if (this.userGeometryID != -1)
					{
						this.DestroyUserGeometry();
					}
					return;
				}
				Position _position = this.position;
				Rotation _rotation = this.selectorData.ObjectOriented ? this.rotation : Rotation.Empty;
				Scale _scale = this.scale * (float)this.selectorData.Scale;
				switch (this.selectorData.SelectorType)
				{
				default:
					this.DestroyUserGeometry();
					return;
				case LandscapeClipboardSelector.SelectorType.Move:
					this.userGeometryID = this.editorScene.CreateUserGeometry_MoveWidget(this.userGeometryID, ref _position, ref _rotation, ref _scale, false, false, true, false, false, this.selectorData.SelectedPart == LandscapeClipboardSelector.SelectedPart.Center, this.selectorData.SelectedPart == LandscapeClipboardSelector.SelectedPart.AxisX, this.selectorData.SelectedPart == LandscapeClipboardSelector.SelectedPart.AxisY, this.selectorData.SelectedPart == LandscapeClipboardSelector.SelectedPart.AxisZ);
					return;
				case LandscapeClipboardSelector.SelectorType.Rotate:
					this.userGeometryID = this.editorScene.CreateUserGeometry_RotationWidget(this.userGeometryID, ref _position, ref _rotation, ref _scale, false, false, this.selectorData.SelectedPart == LandscapeClipboardSelector.SelectedPart.Center, this.selectorData.SelectedPart == LandscapeClipboardSelector.SelectedPart.AxisX, this.selectorData.SelectedPart == LandscapeClipboardSelector.SelectedPart.AxisY, this.selectorData.SelectedPart == LandscapeClipboardSelector.SelectedPart.AxisZ);
					return;
				case LandscapeClipboardSelector.SelectorType.Scale:
					this.userGeometryID = this.editorScene.CreateUserGeometry_ScaleWidget(this.userGeometryID, ref _position, ref _rotation, ref _scale, this.selectorData.SelectedPart == LandscapeClipboardSelector.SelectedPart.Center || this.selectorData.SelectedPart == LandscapeClipboardSelector.SelectedPart.AxisX, this.selectorData.SelectedPart == LandscapeClipboardSelector.SelectedPart.Center || this.selectorData.SelectedPart == LandscapeClipboardSelector.SelectedPart.AxisY, this.selectorData.SelectedPart == LandscapeClipboardSelector.SelectedPart.Center || this.selectorData.SelectedPart == LandscapeClipboardSelector.SelectedPart.AxisZ);
					return;
				}
			}

			// Token: 0x0600175E RID: 5982 RVA: 0x0009FBD1 File Offset: 0x0009EBD1
			private void DestroyUserGeometry()
			{
				if (this.userGeometryID != -1)
				{
					this.editorScene.DeleteUserGeometry(this.userGeometryID);
					this.userGeometryID = -1;
				}
			}

			// Token: 0x0600175F RID: 5983 RVA: 0x0009FBF4 File Offset: 0x0009EBF4
			private void OnSelectorTypeChanged(LandscapeClipboardSelector.SelectorData _electorData, ref LandscapeClipboardSelector.SelectorType oldValue, ref LandscapeClipboardSelector.SelectorType newValue)
			{
				if (this.userGeometryID != -1)
				{
					this.DestroyUserGeometry();
					this.CreateUserGeometry();
				}
			}

			// Token: 0x06001760 RID: 5984 RVA: 0x0009FC0B File Offset: 0x0009EC0B
			private void OnSelectedPartChanged(LandscapeClipboardSelector.SelectorData _electorData, ref LandscapeClipboardSelector.SelectedPart oldValue, ref LandscapeClipboardSelector.SelectedPart newValue)
			{
				if (this.userGeometryID != -1)
				{
					this.CreateUserGeometry();
				}
			}

			// Token: 0x06001761 RID: 5985 RVA: 0x0009FC1C File Offset: 0x0009EC1C
			private void OnSelectorScaleChanged(LandscapeClipboardSelector.SelectorData _electorData, ref double oldValue, ref double newValue)
			{
				if (this.userGeometryID != -1)
				{
					Scale _scale = this.scale * (float)this.selectorData.Scale;
					this.editorScene.ScaleUserGeometry(this.userGeometryID, ref _scale);
				}
			}

			// Token: 0x06001762 RID: 5986 RVA: 0x0009FC60 File Offset: 0x0009EC60
			private void OnObjectOrientedChanged(LandscapeClipboardSelector.SelectorData _electorData, ref bool oldValue, ref bool newValue)
			{
				if (this.userGeometryID != -1)
				{
					Rotation _rotation = this.selectorData.ObjectOriented ? this.rotation : Rotation.Empty;
					this.editorScene.RotateUserGeometry(this.userGeometryID, ref _rotation);
				}
			}

			// Token: 0x06001763 RID: 5987 RVA: 0x0009FCA4 File Offset: 0x0009ECA4
			public WidgetData(EditorScene _editorScene)
			{
				this.editorScene = _editorScene;
			}

			// Token: 0x06001764 RID: 5988 RVA: 0x0009FCDC File Offset: 0x0009ECDC
			public void Bind(LandscapeClipboardSelector.SelectorData _selectorData)
			{
				this.Unbind();
				this.selectorData = _selectorData;
				if (this.selectorData != null)
				{
					this.selectorData.SelectorTypeChanged += this.OnSelectorTypeChanged;
					this.selectorData.SelectedPartChanged += this.OnSelectedPartChanged;
					this.selectorData.ScaleChanged += this.OnSelectorScaleChanged;
					this.selectorData.ScaleChanged += this.OnSelectorScaleChanged;
					this.selectorData.ObjectOrientedChanged += this.OnObjectOrientedChanged;
				}
				this.CreateUserGeometry();
			}

			// Token: 0x06001765 RID: 5989 RVA: 0x0009FD78 File Offset: 0x0009ED78
			public void Unbind()
			{
				this.DestroyUserGeometry();
				if (this.selectorData != null)
				{
					this.selectorData.SelectorTypeChanged -= this.OnSelectorTypeChanged;
					this.selectorData.SelectedPartChanged -= this.OnSelectedPartChanged;
					this.selectorData.ScaleChanged -= this.OnSelectorScaleChanged;
					this.selectorData.ObjectOrientedChanged -= this.OnObjectOrientedChanged;
					this.selectorData = null;
				}
			}

			// Token: 0x170005A3 RID: 1443
			// (get) Token: 0x06001766 RID: 5990 RVA: 0x0009FDF6 File Offset: 0x0009EDF6
			[Browsable(false)]
			public bool Created
			{
				get
				{
					return this.userGeometryID != -1;
				}
			}

			// Token: 0x170005A4 RID: 1444
			// (get) Token: 0x06001767 RID: 5991 RVA: 0x0009FE04 File Offset: 0x0009EE04
			// (set) Token: 0x06001768 RID: 5992 RVA: 0x0009FE0C File Offset: 0x0009EE0C
			[Browsable(false)]
			public bool Active
			{
				get
				{
					return this.active;
				}
				set
				{
					this.active = value;
				}
			}

			// Token: 0x170005A5 RID: 1445
			// (get) Token: 0x06001769 RID: 5993 RVA: 0x0009FE15 File Offset: 0x0009EE15
			// (set) Token: 0x0600176A RID: 5994 RVA: 0x0009FE20 File Offset: 0x0009EE20
			[Browsable(false)]
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
						bool oldVisible = this.visible;
						this.visible = value;
						this.CreateUserGeometry();
						if (this.active && this.VisibleChanged != null)
						{
							this.VisibleChanged(this, ref oldVisible, ref this.visible);
						}
					}
				}
			}

			// Token: 0x170005A6 RID: 1446
			// (get) Token: 0x0600176B RID: 5995 RVA: 0x0009FE6E File Offset: 0x0009EE6E
			// (set) Token: 0x0600176C RID: 5996 RVA: 0x0009FE78 File Offset: 0x0009EE78
			[TypeConverter(typeof(PositionConverter))]
			[Category("Geometry")]
			[Browsable(true)]
			public Position Position
			{
				get
				{
					return this.position;
				}
				set
				{
					this.InvokeChanging();
					Position oldPositionValue = this.position;
					this.position = value;
					if (this.userGeometryID != -1)
					{
						Position _position = this.position;
						this.editorScene.MoveUserGeometry(this.userGeometryID, ref _position);
					}
					this.InvokeChanged();
					if (this.active && this.PositionChanged != null)
					{
						this.PositionChanged(this, ref oldPositionValue, ref this.position);
					}
				}
			}

			// Token: 0x170005A7 RID: 1447
			// (get) Token: 0x0600176D RID: 5997 RVA: 0x0009FEE6 File Offset: 0x0009EEE6
			// (set) Token: 0x0600176E RID: 5998 RVA: 0x0009FEF0 File Offset: 0x0009EEF0
			[Browsable(true)]
			[Category("Geometry")]
			[TypeConverter(typeof(RotationConverter))]
			public Rotation Rotation
			{
				get
				{
					return this.rotation;
				}
				set
				{
					this.InvokeChanging();
					Rotation oldRotationValue = this.rotation;
					this.rotation = value;
					if (this.userGeometryID != -1)
					{
						Rotation _rotation = this.selectorData.ObjectOriented ? this.rotation : Rotation.Empty;
						this.editorScene.RotateUserGeometry(this.userGeometryID, ref _rotation);
					}
					this.InvokeChanged();
					if (this.active && this.RotationChanged != null)
					{
						this.RotationChanged(this, ref oldRotationValue, ref this.rotation);
					}
				}
			}

			// Token: 0x170005A8 RID: 1448
			// (get) Token: 0x0600176F RID: 5999 RVA: 0x0009FF72 File Offset: 0x0009EF72
			// (set) Token: 0x06001770 RID: 6000 RVA: 0x0009FF7C File Offset: 0x0009EF7C
			[TypeConverter(typeof(ScaleConverter))]
			[Category("Geometry")]
			[Browsable(true)]
			public Scale Scale
			{
				get
				{
					return this.scale;
				}
				set
				{
					this.InvokeChanging();
					Scale oldScaleValue = this.scale;
					this.scale = value;
					if (this.userGeometryID != -1)
					{
						Scale _scale = this.scale * (float)this.selectorData.Scale;
						this.editorScene.ScaleUserGeometry(this.userGeometryID, ref _scale);
					}
					this.InvokeChanged();
					if (this.active && this.ScaleChanged != null)
					{
						this.ScaleChanged(this, ref oldScaleValue, ref this.scale);
					}
				}
			}

			// Token: 0x04000F52 RID: 3922
			private readonly EditorScene editorScene;

			// Token: 0x04000F53 RID: 3923
			private LandscapeClipboardSelector.SelectorData selectorData;

			// Token: 0x04000F54 RID: 3924
			private Position position = Position.Empty;

			// Token: 0x04000F55 RID: 3925
			private Rotation rotation = Rotation.Empty;

			// Token: 0x04000F56 RID: 3926
			private Scale scale = Scale.Normal;

			// Token: 0x04000F57 RID: 3927
			private bool visible;

			// Token: 0x04000F58 RID: 3928
			private bool active;

			// Token: 0x04000F59 RID: 3929
			private int userGeometryID = -1;

			// Token: 0x020001C6 RID: 454
			// (Invoke) Token: 0x06001772 RID: 6002
			public delegate void ChangeEvent(LandscapeClipboardSelector.WidgetData widgetData);

			// Token: 0x020001C7 RID: 455
			// (Invoke) Token: 0x06001776 RID: 6006
			public delegate void FieldChangedEvent<T>(LandscapeClipboardSelector.WidgetData WidgetData, ref T oldValue, ref T newValue);
		}

		// Token: 0x020001C8 RID: 456
		// (Invoke) Token: 0x0600177A RID: 6010
		public delegate void ChangeEvent(LandscapeClipboardSelector landscapeClipboardSelector);

		// Token: 0x020001C9 RID: 457
		// (Invoke) Token: 0x0600177E RID: 6014
		public delegate void FieldChangedEvent<T>(LandscapeClipboardSelector landscapeClipboardSelector, ref T oldValue, ref T newValue);
	}
}
