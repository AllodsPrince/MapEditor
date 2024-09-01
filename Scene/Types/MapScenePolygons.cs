using System;
using System.Collections.Generic;
using System.Drawing;
using Tools.Geometry;
using Tools.MapObjects;

namespace MapEditor.Scene.Types
{
	// Token: 0x020000FA RID: 250
	internal class MapScenePolygons
	{
		// Token: 0x06000C4C RID: 3148 RVA: 0x00069A95 File Offset: 0x00068A95
		private static int GetUserGeometryCount(int pointCount, bool closed)
		{
			if (pointCount == 0)
			{
				return 0;
			}
			if (pointCount == 1)
			{
				return 1;
			}
			if (pointCount == 2)
			{
				return 3;
			}
			return pointCount * 2 - (closed ? 0 : 1);
		}

		// Token: 0x06000C4D RID: 3149 RVA: 0x00069AB4 File Offset: 0x00068AB4
		private void DeleteAllUserGeometry(IList<int> userGeometry)
		{
			int userGeometryCount = userGeometry.Count;
			for (int index = 0; index < userGeometryCount; index++)
			{
				this.mapEditorScene.EditorScene.DeleteUserGeometry(userGeometry[index]);
			}
			userGeometry.Clear();
		}

		// Token: 0x06000C4E RID: 3150 RVA: 0x00069AF4 File Offset: 0x00068AF4
		private void DeleteAllUnusedUserGeometry(List<int> userGeometry, Polygon polygon)
		{
			int usedUserGeometryCount = MapScenePolygons.GetUserGeometryCount(polygon.Count, polygon.Closed);
			int userGeometryCount = userGeometry.Count;
			if (usedUserGeometryCount < userGeometryCount)
			{
				for (int index = usedUserGeometryCount; index < userGeometryCount; index++)
				{
					this.mapEditorScene.EditorScene.DeleteUserGeometry(userGeometry[index]);
				}
				userGeometry.RemoveRange(usedUserGeometryCount, userGeometryCount - usedUserGeometryCount);
			}
		}

		// Token: 0x06000C4F RID: 3151 RVA: 0x00069B4C File Offset: 0x00068B4C
		private void CreateUserGeometry(Polygon polygon)
		{
			int pointCount = polygon.Count;
			Color userGeometryColor = polygon.Color;
			bool closed = polygon.Closed;
			if (this.userGeometryMap.ContainsKey(polygon.ID))
			{
				List<int> userGeometry;
				this.userGeometryMap.TryGetValue(polygon.ID, out userGeometry);
				int userGeometryCount = userGeometry.Count;
				for (int index = 0; index < pointCount; index++)
				{
					Position position = new Position(polygon.Points[index]);
					if (userGeometryCount > index * 2)
					{
						this.mapEditorScene.EditorScene.CreateUserGeometry_Circle(userGeometry[index * 2], ref position, 0f, this.circleRadius * 2.0, this.circleRadius * 2.0, userGeometryColor, polygon.Selected, index == polygon.Selection);
					}
					else
					{
						int circleUserGeometryID = this.mapEditorScene.EditorScene.CreateUserGeometry_Circle(-1, ref position, 0f, this.circleRadius * 2.0, this.circleRadius * 2.0, userGeometryColor, polygon.Selected, index == polygon.Selection);
						userGeometry.Add(circleUserGeometryID);
					}
					if (index < pointCount - 1 || (closed && pointCount > 2))
					{
						Position _position = new Position(polygon.Points[(index + 1 < pointCount) ? (index + 1) : 0]);
						if (userGeometryCount > index * 2 + 1)
						{
							this.mapEditorScene.EditorScene.CreateUserGeometry_Line(userGeometry[index * 2 + 1], ref position, ref _position, userGeometryColor, polygon.Selected);
						}
						else
						{
							int lineUserGeometryID = this.mapEditorScene.EditorScene.CreateUserGeometry_Line(-1, ref position, ref _position, userGeometryColor, polygon.Selected);
							userGeometry.Add(lineUserGeometryID);
						}
					}
				}
				this.DeleteAllUnusedUserGeometry(userGeometry, polygon);
				return;
			}
			List<int> userGeometry2 = new List<int>();
			this.userGeometryMap.Add(polygon.ID, userGeometry2);
			for (int index2 = 0; index2 < pointCount; index2++)
			{
				Position position2 = new Position(polygon.Points[index2]);
				int circleUserGeometryID2 = this.mapEditorScene.EditorScene.CreateUserGeometry_Circle(-1, ref position2, 0f, this.circleRadius * 2.0, this.circleRadius * 2.0, userGeometryColor, polygon.Selected, index2 == polygon.Selection);
				userGeometry2.Add(circleUserGeometryID2);
				if (index2 < pointCount - 1 || (closed && pointCount > 2))
				{
					Position _position2 = new Position(polygon.Points[(index2 + 1 < pointCount) ? (index2 + 1) : 0]);
					int lineUserGeometryID2 = this.mapEditorScene.EditorScene.CreateUserGeometry_Line(-1, ref position2, ref _position2, userGeometryColor, polygon.Selected);
					userGeometry2.Add(lineUserGeometryID2);
				}
			}
		}

		// Token: 0x06000C50 RID: 3152 RVA: 0x00069E10 File Offset: 0x00068E10
		private void DestroyUserGeometry(Polygon polygon)
		{
			if (this.userGeometryMap.ContainsKey(polygon.ID))
			{
				List<int> userGeometry;
				this.userGeometryMap.TryGetValue(polygon.ID, out userGeometry);
				this.DeleteAllUserGeometry(userGeometry);
				this.userGeometryMap.Remove(polygon.ID);
			}
		}

		// Token: 0x06000C51 RID: 3153 RVA: 0x00069E5D File Offset: 0x00068E5D
		private void OnPolygonAdded(PolygonContainer polygonContainer, Polygon polygon)
		{
			if (this.mapEditorScene != null)
			{
				this.CreateUserGeometry(polygon);
			}
		}

		// Token: 0x06000C52 RID: 3154 RVA: 0x00069E6E File Offset: 0x00068E6E
		private void OnPolygonRemoved(PolygonContainer polygonContainer, Polygon polygon)
		{
			if (this.mapEditorScene != null)
			{
				this.DestroyUserGeometry(polygon);
			}
		}

		// Token: 0x06000C53 RID: 3155 RVA: 0x00069E80 File Offset: 0x00068E80
		private void OnPolygonClosedChanged(PolygonContainer polygonContainer, Polygon polygon, ref bool oldValue, ref bool newValue)
		{
			if (this.mapEditorScene != null && this.userGeometryMap.ContainsKey(polygon.ID))
			{
				List<int> userGeometry;
				this.userGeometryMap.TryGetValue(polygon.ID, out userGeometry);
				int pointCount = polygon.Points.Count;
				Color userGeometryColor = polygon.Color;
				int userGeometryCount = userGeometry.Count;
				if (polygon.Closed && pointCount > 2)
				{
					Position position = new Position(polygon.Points[pointCount - 1]);
					Position _position = new Position(polygon.Points[0]);
					if (userGeometryCount >= pointCount * 2)
					{
						this.mapEditorScene.EditorScene.CreateUserGeometry_Line(userGeometry[pointCount * 2 - 1], ref position, ref _position, userGeometryColor, polygon.Selected);
					}
					else
					{
						int lineUserGeometryID = this.mapEditorScene.EditorScene.CreateUserGeometry_Line(-1, ref position, ref _position, userGeometryColor, polygon.Selected);
						userGeometry.Add(lineUserGeometryID);
					}
				}
				this.DeleteAllUnusedUserGeometry(userGeometry, polygon);
			}
		}

		// Token: 0x06000C54 RID: 3156 RVA: 0x00069F7D File Offset: 0x00068F7D
		private void OnPolygonSelectedChanged(PolygonContainer polygonContainer, Polygon polygon, ref bool oldValue, ref bool newValue)
		{
			this.CreateUserGeometry(polygon);
		}

		// Token: 0x06000C55 RID: 3157 RVA: 0x00069F88 File Offset: 0x00068F88
		private void OnPolygonSelectionChanged(PolygonContainer polygonContainer, Polygon polygon, ref int oldValue, ref int newValue)
		{
			if (this.mapEditorScene != null)
			{
				int pointCount = polygon.Points.Count;
				Color userGeometryColor = polygon.Color;
				if (oldValue >= 0 && oldValue < pointCount && this.userGeometryMap.ContainsKey(polygon.ID))
				{
					List<int> userGeometry;
					this.userGeometryMap.TryGetValue(polygon.ID, out userGeometry);
					Position position = new Position(polygon.Points[oldValue]);
					this.mapEditorScene.EditorScene.DeleteUserGeometry(userGeometry[oldValue * 2]);
					userGeometry[oldValue * 2] = this.mapEditorScene.EditorScene.CreateUserGeometry_Circle(-1, ref position, 0f, this.circleRadius * 2.0, this.circleRadius * 2.0, userGeometryColor, polygon.Selected, false);
				}
				if (newValue >= 0 && newValue < pointCount && this.userGeometryMap.ContainsKey(polygon.ID))
				{
					List<int> userGeometry2;
					this.userGeometryMap.TryGetValue(polygon.ID, out userGeometry2);
					Position position2 = new Position(polygon.Points[newValue]);
					this.mapEditorScene.EditorScene.DeleteUserGeometry(userGeometry2[newValue * 2]);
					userGeometry2[newValue * 2] = this.mapEditorScene.EditorScene.CreateUserGeometry_Circle(-1, ref position2, 0f, this.circleRadius * 2.0, this.circleRadius * 2.0, userGeometryColor, polygon.Selected, true);
				}
			}
		}

		// Token: 0x06000C56 RID: 3158 RVA: 0x0006A128 File Offset: 0x00069128
		private void OnPolygonColorChanged(PolygonContainer polygonContainer, Polygon polygon, ref Color oldValue, ref Color newValue)
		{
			this.CreateUserGeometry(polygon);
		}

		// Token: 0x06000C57 RID: 3159 RVA: 0x0006A131 File Offset: 0x00069131
		private void OnPolygonChanged(PolygonContainer polygonContainer, Polygon polygon)
		{
			this.CreateUserGeometry(polygon);
		}

		// Token: 0x06000C58 RID: 3160 RVA: 0x0006A13C File Offset: 0x0006913C
		private void OnPolygonPointInserted(PolygonContainer polygonContainer, Polygon polygon, int pointIndex)
		{
			if (this.mapEditorScene != null)
			{
				int pointCount = polygon.Points.Count;
				Color userGeometryColor = polygon.Color;
				if (pointIndex >= 0 && pointIndex < pointCount && this.userGeometryMap.ContainsKey(polygon.ID))
				{
					List<int> userGeometry;
					this.userGeometryMap.TryGetValue(polygon.ID, out userGeometry);
					if (pointIndex == 0)
					{
						Position position = new Position(polygon.Points[0]);
						int circleUserGeometryID = this.mapEditorScene.EditorScene.CreateUserGeometry_Circle(-1, ref position, 0f, this.circleRadius * 2.0, this.circleRadius * 2.0, userGeometryColor, polygon.Selected, pointIndex == polygon.Selection);
						userGeometry.Insert(0, circleUserGeometryID);
						if (pointCount > 1)
						{
							Position _position = new Position(polygon.Points[1]);
							int lineUserGeometryID = this.mapEditorScene.EditorScene.CreateUserGeometry_Line(-1, ref position, ref _position, userGeometryColor, polygon.Selected);
							userGeometry.Insert(1, lineUserGeometryID);
							if (polygon.Closed && pointCount > 2)
							{
								Position position_ = new Position(polygon.Points[pointCount - 1]);
								if (userGeometry.Count == pointCount * 2)
								{
									this.mapEditorScene.EditorScene.CreateUserGeometry_Line(userGeometry[pointCount * 2 - 1], ref position_, ref position, userGeometryColor, polygon.Selected);
									return;
								}
								lineUserGeometryID = this.mapEditorScene.EditorScene.CreateUserGeometry_Line(-1, ref position_, ref position, userGeometryColor, polygon.Selected);
								userGeometry.Add(lineUserGeometryID);
								return;
							}
						}
					}
					else if (pointIndex == pointCount - 1)
					{
						Position position2 = new Position(polygon.Points[pointIndex]);
						Position position_2 = new Position(polygon.Points[pointIndex - 1]);
						int lineUserGeometryID2 = this.mapEditorScene.EditorScene.CreateUserGeometry_Line(-1, ref position_2, ref position2, userGeometryColor, polygon.Selected);
						userGeometry.Insert(pointIndex * 2 - 1, lineUserGeometryID2);
						int circleUserGeometryID2 = this.mapEditorScene.EditorScene.CreateUserGeometry_Circle(-1, ref position2, 0f, this.circleRadius * 2.0, this.circleRadius * 2.0, userGeometryColor, polygon.Selected, pointIndex == polygon.Selection);
						userGeometry.Insert(pointIndex * 2, circleUserGeometryID2);
						if (polygon.Closed && pointCount > 2)
						{
							Position _position2 = new Position(polygon.Points[0]);
							if (userGeometry.Count == pointCount * 2)
							{
								this.mapEditorScene.EditorScene.CreateUserGeometry_Line(userGeometry[pointCount * 2 - 1], ref position2, ref _position2, userGeometryColor, polygon.Selected);
								return;
							}
							lineUserGeometryID2 = this.mapEditorScene.EditorScene.CreateUserGeometry_Line(-1, ref position2, ref _position2, userGeometryColor, polygon.Selected);
							userGeometry.Add(lineUserGeometryID2);
							return;
						}
					}
					else
					{
						Position position3 = new Position(polygon.Points[pointIndex]);
						Position _position3 = new Position(polygon.Points[pointIndex + 1]);
						Position position_3 = new Position(polygon.Points[pointIndex - 1]);
						int circleUserGeometryID3 = this.mapEditorScene.EditorScene.CreateUserGeometry_Circle(-1, ref position3, 0f, this.circleRadius * 2.0, this.circleRadius * 2.0, userGeometryColor, polygon.Selected, pointIndex == polygon.Selection);
						userGeometry.Insert(pointIndex * 2, circleUserGeometryID3);
						int lineUserGeometryID3 = this.mapEditorScene.EditorScene.CreateUserGeometry_Line(-1, ref position3, ref _position3, userGeometryColor, polygon.Selected);
						userGeometry.Insert(pointIndex * 2 + 1, lineUserGeometryID3);
						this.mapEditorScene.EditorScene.CreateUserGeometry_Line(userGeometry[pointIndex * 2 - 1], ref position_3, ref position3, userGeometryColor, polygon.Selected);
						if (polygon.Closed && pointCount > 2 && userGeometry.Count != pointCount * 2)
						{
							Position closedPosition = new Position(polygon.Points[pointCount - 1]);
							Position _closedPosition = new Position(polygon.Points[0]);
							lineUserGeometryID3 = this.mapEditorScene.EditorScene.CreateUserGeometry_Line(-1, ref closedPosition, ref _closedPosition, userGeometryColor, polygon.Selected);
							userGeometry.Add(lineUserGeometryID3);
						}
					}
				}
			}
		}

		// Token: 0x06000C59 RID: 3161 RVA: 0x0006A580 File Offset: 0x00069580
		private void OnPolygonPointDeleted(PolygonContainer polygonContainer, Polygon polygon, int pointIndex)
		{
			if (this.mapEditorScene != null)
			{
				int pointCount = polygon.Points.Count;
				Color userGeometryColor = polygon.Color;
				if (pointIndex >= 0 && pointIndex <= pointCount && this.userGeometryMap.ContainsKey(polygon.ID))
				{
					List<int> userGeometry;
					this.userGeometryMap.TryGetValue(polygon.ID, out userGeometry);
					if (pointCount == 0)
					{
						this.DeleteAllUserGeometry(userGeometry);
						return;
					}
					if (pointIndex == 0)
					{
						this.mapEditorScene.EditorScene.DeleteUserGeometry(userGeometry[0]);
						userGeometry.RemoveAt(0);
						this.mapEditorScene.EditorScene.DeleteUserGeometry(userGeometry[0]);
						userGeometry.RemoveAt(0);
						if (polygon.Closed && pointCount == 2)
						{
							this.mapEditorScene.EditorScene.DeleteUserGeometry(userGeometry[3]);
							userGeometry.RemoveAt(3);
							return;
						}
						if (polygon.Closed && pointCount > 2)
						{
							Position position = new Position(polygon.Points[0]);
							Position position_ = new Position(polygon.Points[pointCount - 1]);
							this.mapEditorScene.EditorScene.CreateUserGeometry_Line(userGeometry[pointCount * 2 - 1], ref position_, ref position, userGeometryColor, polygon.Selected);
							return;
						}
					}
					else if (pointIndex == pointCount)
					{
						this.mapEditorScene.EditorScene.DeleteUserGeometry(userGeometry[userGeometry.Count - 1]);
						userGeometry.RemoveAt(userGeometry.Count - 1);
						this.mapEditorScene.EditorScene.DeleteUserGeometry(userGeometry[userGeometry.Count - 1]);
						userGeometry.RemoveAt(userGeometry.Count - 1);
						if (polygon.Closed && pointCount == 2)
						{
							this.mapEditorScene.EditorScene.DeleteUserGeometry(userGeometry[3]);
							userGeometry.RemoveAt(3);
							return;
						}
						if (polygon.Closed && pointCount > 2)
						{
							Position position2 = new Position(polygon.Points[0]);
							Position position_2 = new Position(polygon.Points[pointCount - 1]);
							this.mapEditorScene.EditorScene.CreateUserGeometry_Line(userGeometry[pointCount * 2 - 1], ref position_2, ref position2, userGeometryColor, polygon.Selected);
							return;
						}
					}
					else
					{
						this.mapEditorScene.EditorScene.DeleteUserGeometry(userGeometry[pointIndex * 2]);
						userGeometry.RemoveAt(pointIndex * 2);
						this.mapEditorScene.EditorScene.DeleteUserGeometry(userGeometry[pointIndex * 2]);
						userGeometry.RemoveAt(pointIndex * 2);
						Position position3 = new Position(polygon.Points[pointIndex]);
						Position position_3 = new Position(polygon.Points[pointIndex - 1]);
						this.mapEditorScene.EditorScene.CreateUserGeometry_Line(userGeometry[pointIndex * 2 - 1], ref position_3, ref position3, userGeometryColor, polygon.Selected);
						if (polygon.Closed && pointCount < 3 && userGeometry.Count == pointCount * 2)
						{
							this.mapEditorScene.EditorScene.DeleteUserGeometry(userGeometry[userGeometry.Count - 1]);
							userGeometry.RemoveAt(userGeometry.Count - 1);
						}
					}
				}
			}
		}

		// Token: 0x06000C5A RID: 3162 RVA: 0x0006A8A0 File Offset: 0x000698A0
		private void OnPolygonPointChanged(PolygonContainer polygonContainer, Polygon polygon, int pointIndex)
		{
			if (this.mapEditorScene != null)
			{
				int pointCount = polygon.Points.Count;
				Color userGeometryColor = polygon.Color;
				if (pointIndex >= 0 && pointIndex < pointCount && this.userGeometryMap.ContainsKey(polygon.ID))
				{
					List<int> userGeometry;
					this.userGeometryMap.TryGetValue(polygon.ID, out userGeometry);
					Position position = new Position(polygon.Points[pointIndex]);
					this.mapEditorScene.EditorScene.CreateUserGeometry_Circle(userGeometry[pointIndex * 2], ref position, 0f, this.circleRadius * 2.0, this.circleRadius * 2.0, userGeometryColor, polygon.Selected, pointIndex == polygon.Selection);
					if (pointIndex == 0)
					{
						if (pointCount > 1)
						{
							Position _position = new Position(polygon.Points[pointIndex + 1]);
							this.mapEditorScene.EditorScene.CreateUserGeometry_Line(userGeometry[pointIndex * 2 + 1], ref position, ref _position, userGeometryColor, polygon.Selected);
							if (polygon.Closed && pointCount > 2)
							{
								Position position_ = new Position(polygon.Points[polygon.Count - 1]);
								this.mapEditorScene.EditorScene.CreateUserGeometry_Line(userGeometry[polygon.Count * 2 - 1], ref position_, ref position, userGeometryColor, polygon.Selected);
								return;
							}
						}
					}
					else
					{
						if (pointIndex == pointCount - 1)
						{
							if (polygon.Closed && pointCount > 2)
							{
								Position _position2 = new Position(polygon.Points[0]);
								this.mapEditorScene.EditorScene.CreateUserGeometry_Line(userGeometry[polygon.Count * 2 - 1], ref position, ref _position2, userGeometryColor, polygon.Selected);
							}
							Position position_2 = new Position(polygon.Points[pointIndex - 1]);
							this.mapEditorScene.EditorScene.CreateUserGeometry_Line(userGeometry[pointIndex * 2 - 1], ref position_2, ref position, userGeometryColor, polygon.Selected);
							return;
						}
						Position _position3 = new Position(polygon.Points[pointIndex + 1]);
						Position position_3 = new Position(polygon.Points[pointIndex - 1]);
						this.mapEditorScene.EditorScene.CreateUserGeometry_Line(userGeometry[pointIndex * 2 + 1], ref position, ref _position3, userGeometryColor, polygon.Selected);
						this.mapEditorScene.EditorScene.CreateUserGeometry_Line(userGeometry[pointIndex * 2 - 1], ref position_3, ref position, userGeometryColor, polygon.Selected);
					}
				}
			}
		}

		// Token: 0x06000C5B RID: 3163 RVA: 0x0006AB38 File Offset: 0x00069B38
		public void Bind(MapEditorScene _mapEditorScene)
		{
			this.mapEditorScene = _mapEditorScene;
			if (this.mapEditorScene != null && this.mapEditorScene.PolygonContainer != null)
			{
				this.mapEditorScene.PolygonContainer.PolygonAdded += this.OnPolygonAdded;
				this.mapEditorScene.PolygonContainer.PolygonRemoved += this.OnPolygonRemoved;
				this.mapEditorScene.PolygonContainer.PolygonClosedChanged += this.OnPolygonClosedChanged;
				this.mapEditorScene.PolygonContainer.PolygonSelectedChanged += this.OnPolygonSelectedChanged;
				this.mapEditorScene.PolygonContainer.PolygonSelectionChanged += this.OnPolygonSelectionChanged;
				this.mapEditorScene.PolygonContainer.PolygonColorChanged += this.OnPolygonColorChanged;
				this.mapEditorScene.PolygonContainer.PolygonChanged += this.OnPolygonChanged;
				this.mapEditorScene.PolygonContainer.PolygonPointInserted += this.OnPolygonPointInserted;
				this.mapEditorScene.PolygonContainer.PolygonPointDeleted += this.OnPolygonPointDeleted;
				this.mapEditorScene.PolygonContainer.PolygonPointChanged += this.OnPolygonPointChanged;
			}
		}

		// Token: 0x06000C5C RID: 3164 RVA: 0x0006AC80 File Offset: 0x00069C80
		public void Unbind()
		{
			if (this.mapEditorScene != null)
			{
				if (this.mapEditorScene.PolygonContainer != null)
				{
					this.mapEditorScene.PolygonContainer.PolygonAdded -= this.OnPolygonAdded;
					this.mapEditorScene.PolygonContainer.PolygonRemoved -= this.OnPolygonRemoved;
					this.mapEditorScene.PolygonContainer.PolygonClosedChanged -= this.OnPolygonClosedChanged;
					this.mapEditorScene.PolygonContainer.PolygonSelectedChanged -= this.OnPolygonSelectedChanged;
					this.mapEditorScene.PolygonContainer.PolygonSelectionChanged -= this.OnPolygonSelectionChanged;
					this.mapEditorScene.PolygonContainer.PolygonColorChanged -= this.OnPolygonColorChanged;
					this.mapEditorScene.PolygonContainer.PolygonChanged -= this.OnPolygonChanged;
					this.mapEditorScene.PolygonContainer.PolygonPointInserted -= this.OnPolygonPointInserted;
					this.mapEditorScene.PolygonContainer.PolygonPointDeleted -= this.OnPolygonPointDeleted;
					this.mapEditorScene.PolygonContainer.PolygonPointChanged -= this.OnPolygonPointChanged;
				}
				this.mapEditorScene = null;
			}
		}

		// Token: 0x04000A1E RID: 2590
		private MapEditorScene mapEditorScene;

		// Token: 0x04000A1F RID: 2591
		private readonly double circleRadius = 0.5;

		// Token: 0x04000A20 RID: 2592
		private readonly Dictionary<int, List<int>> userGeometryMap = new Dictionary<int, List<int>>();
	}
}
