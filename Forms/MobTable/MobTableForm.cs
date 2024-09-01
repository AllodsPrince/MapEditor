using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Windows.Forms;
using Db;
using InputState;
using MapEditor.Forms.Base;
using Tools.DbCommon;
using Tools.DBGameObjects;
using Tools.DBGameObjects.GameObjects;
using Tools.Excel;
using Tools.Geometry;

namespace MapEditor.Forms.MobTable
{
	// Token: 0x020000A4 RID: 164
	public partial class MobTableForm : BaseForm
	{
		// Token: 0x06000792 RID: 1938 RVA: 0x0003B3B4 File Offset: 0x0003A3B4
		private static void LoadMapRegion(string mapRegion, ZoneClass zone, GeneralView mobs, out List<Tools.Geometry.Point> tiles)
		{
			tiles = null;
			if (!string.IsNullOrEmpty(mapRegion))
			{
				DBID mapRegionDBID = MobTableForm.mainDb.GetDBIDByName(mapRegion);
				IObjMan mapRegionMan = MobTableForm.mainDb.GetManipulator(mapRegionDBID);
				if (mapRegionMan != null)
				{
					tiles = new List<Tools.Geometry.Point>();
					IObjMan zonesMan = mapRegionMan.CreateManipulator("tiles");
					if (zonesMan != null)
					{
						if (zonesMan.GetArraySize() == 16)
						{
							for (int y = 0; y < 16; y++)
							{
								zonesMan.SetArrayIndex(y);
								for (int x = 0; x < 16; x++)
								{
									string field = string.Format("[{0}]", x);
									DBID zoneDBID;
									zonesMan.GetValue(field, out zoneDBID);
									if (zoneDBID.ToString() == zone.GameObject)
									{
										tiles.Add(new Tools.Geometry.Point(x, y));
									}
								}
							}
						}
						zonesMan.Dispose();
					}
					IObjMan objectsMan = mapRegionMan.CreateManipulator("Objects");
					if (objectsMan != null)
					{
						int cnt = objectsMan.GetArraySize();
						for (int index = 0; index < cnt; index++)
						{
							objectsMan.SetArrayIndex(index);
							if (objectsMan.IsStructPtrInstanceCompatible("serverStatic", "gameMechanics.map.StaticDevice"))
							{
								DBID deviceDBID;
								objectsMan.GetValue("serverStatic.device", out deviceDBID);
								if (!DBID.IsNullOrEmpty(deviceDBID))
								{
									mobs.AddObjectIfNotFinded(MobTableForm.CreateTableZoneObject(zone, deviceDBID));
								}
							}
						}
						objectsMan.Dispose();
					}
					mapRegionMan.Dispose();
				}
			}
		}

		// Token: 0x06000793 RID: 1939 RVA: 0x0003B4F2 File Offset: 0x0003A4F2
		private static void GetPostionFromArea(IObjMan areaMan, out float posX, out float posY)
		{
			areaMan.GetValue("center.x", out posX);
			areaMan.GetValue("center.y", out posY);
		}

		// Token: 0x06000794 RID: 1940 RVA: 0x0003B50C File Offset: 0x0003A50C
		private static void GetPosition(IObjMan positionMan, out float posX, out float posY)
		{
			posX = 0f;
			posY = 0f;
			if (positionMan.IsStructPtrInstanceCompatible(string.Empty, "gameMechanics.map.spawn.SpawnPlaceCircle") || positionMan.IsStructPtrInstanceCompatible(string.Empty, "gameMechanics.map.spawn.SpawnPlacePoint"))
			{
				positionMan.GetValue("center.x", out posX);
				positionMan.GetValue("center.y", out posY);
			}
			if (positionMan.IsStructPtrInstanceCompatible(string.Empty, "gameMechanics.map.spawn.SpawnPlaceMultiRoaming"))
			{
				IObjMan areasMan = positionMan.CreateManipulator("areas");
				int cnt = areasMan.GetArraySize();
				for (int index = 0; index > cnt; index++)
				{
					areasMan.SetArrayIndex(index);
					float x;
					float y;
					MobTableForm.GetPostionFromArea(areasMan, out x, out y);
					posX += x / (float)cnt;
					posY += y / (float)cnt;
				}
				return;
			}
			if (positionMan.IsStructPtrInstanceCompatible(string.Empty, "gameMechanics.map.spawn.patrol.SpawnPlacePatrol"))
			{
				IObjMan patrolMan = positionMan.CreateManipulator("points");
				int cnt2 = patrolMan.GetArraySize();
				if (cnt2 > 0)
				{
					patrolMan.SetArrayIndex(0);
					patrolMan.GetValue("coords.x", out posX);
					patrolMan.GetValue("coords.y", out posY);
					return;
				}
			}
			else if (positionMan.IsStructPtrInstanceCompatible(string.Empty, "gameMechanics.map.spawn.SpawnPlaceRoamingArea"))
			{
				MobTableForm.GetPostionFromArea(positionMan, out posX, out posY);
			}
		}

		// Token: 0x06000795 RID: 1941 RVA: 0x0003B624 File Offset: 0x0003A624
		private static void ParseSpawnTableArray(IObjMan spawnTableMan, string field, ICollection<DBID> objects)
		{
			IObjMan arrayMan = spawnTableMan.CreateManipulator(field);
			int cnt = arrayMan.GetArraySize();
			for (int index = 0; index < cnt; index++)
			{
				arrayMan.SetArrayIndex(index);
				DBID objectDBID;
				arrayMan.GetValue("object", out objectDBID);
				if (!DBID.IsNullOrEmpty(objectDBID))
				{
					objects.Add(objectDBID);
				}
			}
		}

		// Token: 0x06000796 RID: 1942 RVA: 0x0003B670 File Offset: 0x0003A670
		private static bool CheckPostion(float x, float y, IEnumerable<Tools.Geometry.Point> tiles)
		{
			foreach (Tools.Geometry.Point tile in tiles)
			{
				if (x >= (float)(tile.X * 16) && x < (float)((tile.X + 1) * 16) && y >= (float)(tile.Y * 16) && y < (float)((tile.Y + 1) * 16))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06000797 RID: 1943 RVA: 0x0003B6F4 File Offset: 0x0003A6F4
		private static bool LoadObjects(DBID serverObjectsDBID, IEnumerable<Tools.Geometry.Point> tiles, out List<DBID> objectList)
		{
			objectList = null;
			if (!DBID.IsNullOrEmpty(serverObjectsDBID) && tiles != null)
			{
				IObjMan serverObjectsMan = MobTableForm.mainDb.GetManipulator(serverObjectsDBID);
				if (serverObjectsMan != null)
				{
					IObjMan serverObjectsArray = serverObjectsMan.CreateManipulator("objects");
					if (serverObjectsArray != null)
					{
						int serverObjectsCnt = serverObjectsArray.GetArraySize();
						objectList = new List<DBID>(serverObjectsCnt);
						for (int serverObjIndex = 0; serverObjIndex < serverObjectsCnt; serverObjIndex++)
						{
							serverObjectsArray.SetArrayIndex(serverObjIndex);
							if (serverObjectsArray.IsStructPtrInstanceCompatible(string.Empty, "gameMechanics.map.spawn.MobSingleSpawn") || serverObjectsArray.IsStructPtrInstanceCompatible(string.Empty, "gameMechanics.map.spawn.DeviceSingleSpawn"))
							{
								DBID objectDBID;
								serverObjectsArray.GetValue("object", out objectDBID);
								if (!DBID.IsNullOrEmpty(objectDBID))
								{
									float positionX;
									float positionY;
									MobTableForm.GetPosition(serverObjectsArray.CreateManipulator("place"), out positionX, out positionY);
									if (MobTableForm.CheckPostion(positionX, positionY, tiles))
									{
										objectList.Add(objectDBID);
									}
								}
							}
							else if (serverObjectsArray.IsStructPtrInstanceCompatible(string.Empty, "gameMechanics.map.spawn.SpawnLocus"))
							{
								DBID spawnTableDBID;
								serverObjectsArray.GetValue("spawnTable", out spawnTableDBID);
								if (!DBID.IsNullOrEmpty(spawnTableDBID))
								{
									IObjMan spawnTableMan = MobTableForm.mainDb.GetManipulator(spawnTableDBID);
									if (spawnTableMan != null)
									{
										IObjMan positionsArray = serverObjectsArray.CreateManipulator("places");
										int cnt = positionsArray.GetArraySize();
										if (cnt > 0)
										{
											for (int index = 0; index < cnt; index++)
											{
												positionsArray.SetArrayIndex(index);
												float positionX2;
												float positionY2;
												MobTableForm.GetPosition(positionsArray, out positionX2, out positionY2);
												if (MobTableForm.CheckPostion(positionX2, positionY2, tiles))
												{
													MobTableForm.ParseSpawnTableArray(spawnTableMan, "commons", objectList);
													MobTableForm.ParseSpawnTableArray(spawnTableMan, "singles", objectList);
												}
											}
										}
									}
								}
							}
						}
						serverObjectsArray.Dispose();
					}
					serverObjectsMan.Dispose();
				}
			}
			return objectList != null;
		}

		// Token: 0x06000798 RID: 1944 RVA: 0x0003B888 File Offset: 0x0003A888
		private static void LoadObjects(DBID serverObjectsDBID, ZoneClass zone, IEnumerable<Tools.Geometry.Point> tiles, Tools.DBGameObjects.View mobs)
		{
			List<DBID> objectDBIDList;
			if (MobTableForm.LoadObjects(serverObjectsDBID, tiles, out objectDBIDList))
			{
				bool heroic = serverObjectsDBID.ToString().EndsWith("heroic_ServerObjects.xdb");
				foreach (DBID dbid in objectDBIDList)
				{
					MobTableForm.ZoneTableObject zoneTableObject = mobs.AddObjectIfNotFinded(MobTableForm.CreateTableZoneObject(zone, dbid)) as MobTableForm.ZoneTableObject;
					if (zoneTableObject != null)
					{
						zoneTableObject.SetHeroicType(heroic);
					}
				}
			}
		}

		// Token: 0x06000799 RID: 1945 RVA: 0x0003B90C File Offset: 0x0003A90C
		private bool LoadZone(ZoneClass zone, out GeneralView mobs)
		{
			mobs = null;
			if (zone != null)
			{
				List<string> mapRegions = null;
				string cacheFilePath = MobTableForm.cacheFodler + zone.GetFileRelativePath().Replace('\\', '_') + ".bin";
				try
				{
					FileStream stream = new FileStream(cacheFilePath, FileMode.Open);
					BinaryFormatter binFormatter = new BinaryFormatter();
					mapRegions = (binFormatter.Deserialize(stream) as List<string>);
					stream.Close();
				}
				catch (FileNotFoundException ex)
				{
					Console.WriteLine(ex.Message);
				}
				catch (IOException ex2)
				{
					Console.WriteLine(ex2.Message);
				}
				catch (SerializationException ex3)
				{
					Console.WriteLine(ex3.Message);
				}
				if (mapRegions != null)
				{
					int time = Environment.TickCount;
					mobs = new GeneralView(null);
					foreach (string mapRegion in mapRegions)
					{
						List<Tools.Geometry.Point> tiles;
						MobTableForm.LoadMapRegion(mapRegion, zone, mobs, out tiles);
						if (this.serverObjectsList == null)
						{
							this.serverObjectsList = MobTableForm.mainDb.GetObjectsList("gameMechanics.map.PatchObjects");
						}
						int suffixStart = mapRegion.Length - MobTableForm.mapRegionSuffixLen;
						if (suffixStart > 0)
						{
							string serverObjectsStart = mapRegion.Remove(suffixStart);
							foreach (DBID serverObjects in this.serverObjectsList)
							{
								if (!DBID.IsNullOrEmpty(serverObjects) && serverObjects.ToString().StartsWith(serverObjectsStart))
								{
									MobTableForm.LoadObjects(serverObjects, zone, tiles, mobs);
								}
							}
						}
					}
					Console.WriteLine(string.Format("Zone loaded for {0} ms", Environment.TickCount - time));
					return true;
				}
			}
			return false;
		}

		// Token: 0x0600079A RID: 1946 RVA: 0x0003BADC File Offset: 0x0003AADC
		private void FindSimilarZone(ZoneClass zone, out List<ZoneClass> similarZones)
		{
			similarZones = new List<ZoneClass>();
			if (zone != null)
			{
				similarZones.Add(zone);
				if (!string.IsNullOrEmpty(zone.Name) && zone.Name.Equals(zone.GetShortFolderName(), StringComparison.InvariantCultureIgnoreCase))
				{
					string zoneName = zone.Name;
					foreach (GameObjectClass gameObjectClass in this.zones)
					{
						ZoneClass _zone = (ZoneClass)gameObjectClass;
						if (zoneName.Equals(_zone.GetShortFolderName(), StringComparison.InvariantCultureIgnoreCase))
						{
							similarZones.Add(_zone);
						}
					}
				}
			}
		}

		// Token: 0x0600079B RID: 1947 RVA: 0x0003BB78 File Offset: 0x0003AB78
		private void LoadZone(ZoneClass zone)
		{
			this.mobTable.ColumnChanged -= MobTableForm.OnTableColumnChanged;
			this.mobTable.BeginLoadData();
			this.mobTable.Rows.Clear();
			this.deviceTable.ColumnChanged -= MobTableForm.OnTableColumnChanged;
			this.deviceTable.BeginLoadData();
			this.deviceTable.Rows.Clear();
			this.OnFilterClick(null, null);
			if (zone != null)
			{
				List<ZoneClass> similarZones;
				this.FindSimilarZone(zone, out similarZones);
				foreach (ZoneClass _zone in similarZones)
				{
					GeneralView objects;
					if (!this.zoneObjects.TryGetValue(_zone.GameObject, out objects) && this.LoadZone(_zone, out objects))
					{
						this.zoneObjects.Add(_zone.GameObject, objects);
					}
					if (objects != null)
					{
						foreach (GameObjectClass gameObjectClass in objects)
						{
							MobTableForm.ZoneTableObject obj = (MobTableForm.ZoneTableObject)gameObjectClass;
							MobTableForm.DataBinding.AddRowInTable(this.GetDataTable(obj.GetType()), obj, this.tableFilter);
						}
					}
				}
			}
			this.mobTable.EndLoadData();
			this.mobTable.ColumnChanged += MobTableForm.OnTableColumnChanged;
			this.deviceTable.EndLoadData();
			this.deviceTable.ColumnChanged += MobTableForm.OnTableColumnChanged;
			this.EnableDisableButtons();
			this.toExcelButton.Enabled = (this.zoneGridView.Rows.Count > 0);
			this.cuesToExcelButton.Enabled = (this.zoneGridView.Rows.Count > 0);
		}

		// Token: 0x0600079C RID: 1948 RVA: 0x0003BD50 File Offset: 0x0003AD50
		private void OnTableTypeChanged(object sender, EventArgs e)
		{
			Type type = this.mobTableRadioButton.Checked ? typeof(MobTableForm.ZoneTableMob) : typeof(MobTableForm.ZoneTableDevice);
			MobTableForm.DataBinding.BindTable(this.GetDataTable(type), this.zoneGridView, type);
			this.EnableDisableButtons();
		}

		// Token: 0x0600079D RID: 1949 RVA: 0x0003BD9C File Offset: 0x0003AD9C
		private void OnSelectedIndexChanged(object sender, EventArgs e)
		{
			this.Cursor = Cursors.WaitCursor;
			this.tableFilter = this.findTextBox.Text;
			this.LoadZone(this.zoneComboBox.SelectedItem as ZoneClass);
			this.refreshButton.Enabled = (this.zoneComboBox.SelectedItem != null);
			this.Cursor = Cursors.Default;
		}

		// Token: 0x0600079E RID: 1950 RVA: 0x0003BE02 File Offset: 0x0003AE02
		private void OnCurrentCellChanged(object sender, EventArgs e)
		{
			this.EnableDisableButtons();
		}

		// Token: 0x0600079F RID: 1951 RVA: 0x0003BE0C File Offset: 0x0003AE0C
		private void OnFilterClick(object sender, EventArgs e)
		{
			string filter = string.Empty;
			if (this.zoneGridView.Columns["IsOrdinal"] != null && this.zoneGridView.Columns["IsHeroic"] != null)
			{
				filter += string.Format("( [{0}] = True )", this.ordinalHeroicTypeRadioButton.Checked ? "IsOrdinal" : "IsHeroic");
			}
			DataGridViewColumn column = this.zoneGridView.Columns["VendorTable"];
			if (column != null)
			{
				if (this.vendorsRadioButton.Checked)
				{
					filter += string.Format(" and ( IsNull([{0}],'') <> '' )", "VendorTable");
				}
				column.Visible = this.vendorsRadioButton.Checked;
			}
			this.mobTable.DefaultView.RowFilter = filter;
			this.deviceTable.DefaultView.RowFilter = filter;
		}

		// Token: 0x060007A0 RID: 1952 RVA: 0x0003BEE8 File Offset: 0x0003AEE8
		private static void OnTableColumnChanged(object sender, DataColumnChangeEventArgs e)
		{
			if (e.Row != null && !e.Column.ReadOnly)
			{
				MobTableForm.ZoneTableObject zoneTableObject = e.Row["DBObject"] as MobTableForm.ZoneTableObject;
				if (zoneTableObject != null)
				{
					object value = e.Row[e.Column];
					if (value == DBNull.Value)
					{
						value = null;
					}
					MobTableForm.DataBinding.SetPropertyValue(zoneTableObject, e.Column.ColumnName, value);
				}
			}
		}

		// Token: 0x060007A1 RID: 1953 RVA: 0x0003BF54 File Offset: 0x0003AF54
		private void OnRefreshClick(object sender, EventArgs e)
		{
			ZoneClass zone = this.zoneComboBox.SelectedItem as ZoneClass;
			if (zone != null)
			{
				this.Cursor = Cursors.WaitCursor;
				List<ZoneClass> similarZones;
				this.FindSimilarZone(zone, out similarZones);
				foreach (ZoneClass _zone in similarZones)
				{
					this.zoneObjects.Remove(_zone.GameObject);
				}
				this.LoadZone(zone);
				this.Cursor = Cursors.Default;
			}
		}

		// Token: 0x060007A2 RID: 1954 RVA: 0x0003BFE8 File Offset: 0x0003AFE8
		private void OnFindClick(object sender, EventArgs e)
		{
			this.Cursor = Cursors.WaitCursor;
			this.tableFilter = this.findTextBox.Text;
			this.LoadZone(this.zoneComboBox.SelectedItem as ZoneClass);
			this.Cursor = Cursors.Default;
		}

		// Token: 0x060007A3 RID: 1955 RVA: 0x0003C028 File Offset: 0x0003B028
		private void OnFindKeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Return)
			{
				this.Cursor = Cursors.WaitCursor;
				this.tableFilter = this.findTextBox.Text;
				this.LoadZone(this.zoneComboBox.SelectedItem as ZoneClass);
				this.Cursor = Cursors.Default;
			}
		}

		// Token: 0x060007A4 RID: 1956 RVA: 0x0003C07C File Offset: 0x0003B07C
		private void EnableDisableButtons()
		{
			bool enabled = this.zoneGridView.CurrentRow != null && this.zoneGridView.CurrentRow.Index > -1;
			this.showProprtiesButton.Enabled = enabled;
			this.findOnMapButton.Enabled = enabled;
			this.dialogEditorButton.Enabled = enabled;
			this.scriptEventsButton.Enabled = (enabled && this.mobTableRadioButton.Checked);
		}

		// Token: 0x060007A5 RID: 1957 RVA: 0x0003C0F0 File Offset: 0x0003B0F0
		private void OnShowPropertiesClick(object sender, EventArgs e)
		{
			if (this.zoneGridView.CurrentRow != null && this.zoneGridView.CurrentRow.Index > -1)
			{
				MobTableForm.ZoneTableObject zoneObject = (MobTableForm.ZoneTableObject)this.zoneGridView["DBObject", this.zoneGridView.CurrentRow.Index].Value;
				if (base.Context != null)
				{
					base.Context.SelectExistingObjectInDatabaseEditor(zoneObject.GameObject);
				}
			}
		}

		// Token: 0x060007A6 RID: 1958 RVA: 0x0003C164 File Offset: 0x0003B164
		private void OnFindOnMapClick(object sender, EventArgs e)
		{
			if (this.zoneGridView.CurrentRow != null && this.zoneGridView.CurrentRow.Index > -1)
			{
				MobTableForm.ZoneTableObject zoneObject = (MobTableForm.ZoneTableObject)this.zoneGridView["DBObject", this.zoneGridView.CurrentRow.Index].Value;
				if (base.Context != null)
				{
					base.Context.MainState.ActiveState = 0;
					base.Context.StateContainer.Invoke("_select_object_by_string_key", new MethodArgs(this, zoneObject.GameObject, null));
				}
			}
		}

		// Token: 0x060007A7 RID: 1959 RVA: 0x0003C1F8 File Offset: 0x0003B1F8
		private void OnToExcelClick(object sender, EventArgs e)
		{
			if (this.zoneGridView.Rows.Count > 0)
			{
				this.Cursor = Cursors.WaitCursor;
				List<DataGridViewColumn> columnsByString = new List<DataGridViewColumn>();
				columnsByString.Add(this.zoneGridView.Columns["DBObject"]);
				ExportImport.ToExcel(this.zoneGridView, columnsByString);
				this.Cursor = Cursors.Default;
			}
		}

		// Token: 0x060007A8 RID: 1960 RVA: 0x0003C25C File Offset: 0x0003B25C
		private void OnCuesToExcelClick(object sender, EventArgs e)
		{
			DataTable currentTable = this.zoneGridView.DataSource as DataTable;
			if (currentTable != null)
			{
				Cursor.Current = Cursors.WaitCursor;
				List<object[]> cueDataForExport = new List<object[]>();
				Dictionary<DBID, MobTableForm.ZoneTableObject.Cue> cues = new Dictionary<DBID, MobTableForm.ZoneTableObject.Cue>();
				foreach (object obj in currentTable.DefaultView)
				{
					DataRowView row = (DataRowView)obj;
					MobTableForm.ZoneTableObject zoneTableObject = row["DBObject"] as MobTableForm.ZoneTableObject;
					if (zoneTableObject != null && zoneTableObject.GetCues(cues))
					{
						foreach (MobTableForm.ZoneTableObject.Cue cue in cues.Values)
						{
							cueDataForExport.Add(new object[]
							{
								zoneTableObject.GameObject,
								zoneTableObject.GetTextValue("name", false),
								cue.NameFilePath,
								cue.GetName(),
								cue.TextFilePath,
								cue.GetText()
							});
						}
					}
				}
				object[,] result = new object[cueDataForExport.Count + 1, 6];
				if (cueDataForExport.Count > 0)
				{
					result[0, 0] = "NPC file path";
					result[0, 1] = "NPC";
					result[0, 2] = "Name file path";
					result[0, 3] = "Name";
					result[0, 4] = "Text file path";
					result[0, 5] = "Text";
					int index = 1;
					foreach (object[] line in cueDataForExport)
					{
						for (int col = 0; col < 6; col++)
						{
							result[index, col] = line[col];
						}
						index++;
					}
				}
				ExportImport.ToExcel(result, null);
				Cursor.Current = Cursors.Default;
			}
		}

		// Token: 0x060007A9 RID: 1961 RVA: 0x0003C488 File Offset: 0x0003B488
		private void OnDialogEditorClick(object sender, EventArgs e)
		{
			if (this.zoneGridView.CurrentRow != null && this.zoneGridView.CurrentRow.Index > -1)
			{
				MobTableForm.ZoneTableObject zoneObject = this.zoneGridView["DBObject", this.zoneGridView.CurrentRow.Index].Value as MobTableForm.ZoneTableObject;
				if (zoneObject != null && !EditorSceneDLLImport.ApplicationSingleton_SendMessageToWindow(string.Format("mob_{0}{1}", EditorEnvironment.ApplicationGUID, EditorEnvironment.ExtEditorSuffix), EditorEnvironment.ShowObjectMessgeId, zoneObject.GameObject))
				{
					string arg = string.Format("-module:Mob {0}", zoneObject.GameObject);
					DirectoryInfo binDirectory = new DirectoryInfo(EditorEnvironment.WorkingFolder + "bin");
					if (binDirectory.Exists)
					{
						Environment.CurrentDirectory = binDirectory.FullName;
						Process.Start(new ProcessStartInfo(Application.ExecutablePath, arg));
					}
				}
			}
		}

		// Token: 0x060007AA RID: 1962 RVA: 0x0003C55C File Offset: 0x0003B55C
		private void OnScriptEventsEditorClick(object sender, EventArgs e)
		{
			if (this.zoneGridView.CurrentRow != null && this.zoneGridView.CurrentRow.Index > -1)
			{
				MobTableForm.ZoneTableObject zoneTableObject = this.zoneGridView["DBObject", this.zoneGridView.CurrentRow.Index].Value as MobTableForm.ZoneTableObject;
				if (zoneTableObject != null)
				{
					base.Context.StateContainer.Invoke("_load_in_script_events_editor", new MethodArgs(this, zoneTableObject.GameObject, null));
				}
			}
		}

		// Token: 0x060007AB RID: 1963 RVA: 0x0003C5DC File Offset: 0x0003B5DC
		private void OnLoadForm(object sender, EventArgs e)
		{
			DBMethods.LoadObjects("gameMechanics.map.zone.ZoneResource", this.zones, false);
			foreach (GameObjectClass zone in this.zones)
			{
				this.zoneComboBox.Items.Add(zone);
			}
			this.zoneComboBox.SelectedIndexChanged += this.OnSelectedIndexChanged;
			MobTableForm.DataBinding.InitTable(this.GetDataTable(typeof(MobTableForm.ZoneTableMob)), typeof(MobTableForm.ZoneTableMob));
			MobTableForm.DataBinding.InitTable(this.GetDataTable(typeof(MobTableForm.ZoneTableDevice)), typeof(MobTableForm.ZoneTableDevice));
			MobTableForm.DataBinding.BindTable(this.GetDataTable(typeof(MobTableForm.ZoneTableMob)), this.zoneGridView, typeof(MobTableForm.ZoneTableMob));
			this.zoneGridView.CurrentCellChanged += this.OnCurrentCellChanged;
			this.mobTableRadioButton.CheckedChanged += this.OnTableTypeChanged;
			this.allObjTypeRadioButton.Click += this.OnFilterClick;
			this.vendorsRadioButton.Click += this.OnFilterClick;
			this.heroicHeroicTypeRadioButton.Click += this.OnFilterClick;
			this.ordinalHeroicTypeRadioButton.Click += this.OnFilterClick;
			this.refreshButton.Click += this.OnRefreshClick;
			this.findButton.Click += this.OnFindClick;
			this.findTextBox.KeyDown += this.OnFindKeyDown;
			this.showProprtiesButton.Click += this.OnShowPropertiesClick;
			this.findOnMapButton.Click += this.OnFindOnMapClick;
			this.dialogEditorButton.Click += this.OnDialogEditorClick;
			this.scriptEventsButton.Click += this.OnScriptEventsEditorClick;
			this.EnableDisableButtons();
			this.toExcelButton.Click += this.OnToExcelClick;
			this.toExcelButton.Enabled = false;
			this.cuesToExcelButton.Click += this.OnCuesToExcelClick;
			this.cuesToExcelButton.Enabled = false;
		}

		// Token: 0x060007AC RID: 1964 RVA: 0x0003C838 File Offset: 0x0003B838
		public MobTableForm(MainForm.Context context) : base(EditorEnvironment.EditorFormsFolder + "MobTableForm.xml", context)
		{
			this.InitializeComponent();
			base.ParamsSaver.AutoregisterControls = false;
			base.Load += this.OnLoadForm;
		}

		// Token: 0x060007AD RID: 1965 RVA: 0x0003C8B0 File Offset: 0x0003B8B0
		private static MobTableForm.ZoneTableObject CreateTableZoneObject(ZoneClass zone, DBID dbid)
		{
			if (!DBID.IsNullOrEmpty(dbid))
			{
				string type = MobTableForm.mainDb.GetClassTypeName(dbid);
				if (type == "gameMechanics.world.mob.MobWorld")
				{
					return new MobTableForm.ZoneTableMob(zone, MobTableForm.mainDb.GetManipulator(dbid));
				}
				if (DBMethods.TypeIsDerivedFrom(type, "gameMechanics.world.device.DeviceResource"))
				{
					return new MobTableForm.ZoneTableDevice(zone, MobTableForm.mainDb.GetManipulator(dbid));
				}
			}
			return null;
		}

		// Token: 0x060007AE RID: 1966 RVA: 0x0003C910 File Offset: 0x0003B910
		private DataTable GetDataTable(Type type)
		{
			if (type == typeof(MobTableForm.ZoneTableMob))
			{
				return this.mobTable;
			}
			if (type == typeof(MobTableForm.ZoneTableDevice))
			{
				return this.deviceTable;
			}
			return null;
		}

		// Token: 0x04000583 RID: 1411
		private const string cacheFileExt = ".bin";

		// Token: 0x04000584 RID: 1412
		private const int zoneTileCnt = 16;

		// Token: 0x04000585 RID: 1413
		private const int zoneTileSize = 16;

		// Token: 0x04000586 RID: 1414
		private const string heroicServerObjectsSuffix = "heroic_ServerObjects.xdb";

		// Token: 0x04000587 RID: 1415
		private static readonly string cacheFodler = EditorEnvironment.CommonEditorFolder.Replace('/', '\\') + "CommonFolder\\Caches\\Zones\\";

		// Token: 0x04000588 RID: 1416
		private static readonly IDatabase mainDb = IDatabase.GetMainDatabase();

		// Token: 0x04000589 RID: 1417
		private static readonly int mapRegionSuffixLen = "MapRegion.xdb".Length;

		// Token: 0x0400058A RID: 1418
		private readonly Dictionary<string, GeneralView> zoneObjects = new Dictionary<string, GeneralView>();

		// Token: 0x0400058B RID: 1419
		private readonly GeneralView zones = new GeneralView(string.Empty);

		// Token: 0x0400058C RID: 1420
		private List<DBID> serverObjectsList;

		// Token: 0x0400058D RID: 1421
		private readonly DataTable mobTable = new DataTable();

		// Token: 0x0400058E RID: 1422
		private readonly DataTable deviceTable = new DataTable();

		// Token: 0x0400058F RID: 1423
		private string tableFilter;

		// Token: 0x020000A5 RID: 165
		private class TableColPropAttribute : Attribute
		{
			// Token: 0x060007B2 RID: 1970 RVA: 0x0003D822 File Offset: 0x0003C822
			public TableColPropAttribute(int _colIndex)
			{
				this.colIndex = _colIndex;
			}

			// Token: 0x060007B3 RID: 1971 RVA: 0x0003D831 File Offset: 0x0003C831
			public TableColPropAttribute(int _colIndex, bool _visible)
			{
				this.colIndex = _colIndex;
				this.visible = _visible;
			}

			// Token: 0x060007B4 RID: 1972 RVA: 0x0003D847 File Offset: 0x0003C847
			public TableColPropAttribute(int _colIndex, bool _visible, bool _primaryKey)
			{
				this.colIndex = _colIndex;
				this.visible = _visible;
				this.primaryKey = _primaryKey;
			}

			// Token: 0x17000128 RID: 296
			// (get) Token: 0x060007B5 RID: 1973 RVA: 0x0003D864 File Offset: 0x0003C864
			public int ColIndex
			{
				get
				{
					return this.colIndex;
				}
			}

			// Token: 0x17000129 RID: 297
			// (get) Token: 0x060007B6 RID: 1974 RVA: 0x0003D86C File Offset: 0x0003C86C
			public bool Visible
			{
				get
				{
					return this.visible;
				}
			}

			// Token: 0x1700012A RID: 298
			// (get) Token: 0x060007B7 RID: 1975 RVA: 0x0003D874 File Offset: 0x0003C874
			public bool PrimaryKey
			{
				get
				{
					return this.primaryKey;
				}
			}

			// Token: 0x040005AA RID: 1450
			private readonly int colIndex;

			// Token: 0x040005AB RID: 1451
			private readonly bool visible;

			// Token: 0x040005AC RID: 1452
			private readonly bool primaryKey;
		}

		// Token: 0x020000A6 RID: 166
		private abstract class ZoneTableObject : GameObjectClass
		{
			// Token: 0x1700012B RID: 299
			// (get) Token: 0x060007B8 RID: 1976
			public abstract GameObjectClass DBObject { get; }

			// Token: 0x1700012C RID: 300
			// (get) Token: 0x060007B9 RID: 1977
			public abstract string VendorTable { get; }

			// Token: 0x1700012D RID: 301
			// (get) Token: 0x060007BA RID: 1978
			public abstract bool IsOrdinal { get; }

			// Token: 0x1700012E RID: 302
			// (get) Token: 0x060007BB RID: 1979
			public abstract bool IsHeroic { get; }

			// Token: 0x060007BC RID: 1980 RVA: 0x0003D87C File Offset: 0x0003C87C
			private static void GetCue(IObjMan cueMan, IDictionary<DBID, MobTableForm.ZoneTableObject.Cue> cues)
			{
				if (cueMan != null)
				{
					string nameFile;
					cueMan.GetValue("name", out nameFile);
					string textFile;
					cueMan.GetValue("text", out textFile);
					cues.Add(cueMan.DBID, new MobTableForm.ZoneTableObject.Cue(nameFile, textFile));
					IObjMan nextCuesManArray = cueMan.CreateManipulator("nextCues");
					int count = nextCuesManArray.GetArraySize();
					for (int index = 0; index < count; index++)
					{
						nextCuesManArray.SetArrayIndex(index);
						DBID cueDBID;
						nextCuesManArray.GetValue(string.Empty, out cueDBID);
						if (!DBID.IsNullOrEmpty(cueDBID) && !cues.ContainsKey(cueDBID))
						{
							IObjMan nextCueMan = MobTableForm.mainDb.GetManipulator(cueDBID);
							MobTableForm.ZoneTableObject.GetCue(nextCueMan, cues);
							nextCueMan.Dispose();
						}
					}
					nextCuesManArray.Dispose();
				}
			}

			// Token: 0x1700012F RID: 303
			// (get) Token: 0x060007BD RID: 1981 RVA: 0x0003D92A File Offset: 0x0003C92A
			protected short HeroicType
			{
				get
				{
					return this.heroicType;
				}
			}

			// Token: 0x060007BE RID: 1982 RVA: 0x0003D934 File Offset: 0x0003C934
			protected string GetVendorTable()
			{
				string value = null;
				IObjMan objMan = base.GetObjectMainipulator();
				if (objMan != null && !objMan.IsStructPtrZero("interactions"))
				{
					objMan.GetValue("interactions.vendorTable", out value);
				}
				return value;
			}

			// Token: 0x060007BF RID: 1983 RVA: 0x0003D968 File Offset: 0x0003C968
			protected int GetIntValue(string fieldName)
			{
				int value = 0;
				IObjMan objMan = base.GetObjectMainipulator();
				if (objMan != null)
				{
					objMan.GetValue(fieldName, out value);
				}
				return value;
			}

			// Token: 0x060007C0 RID: 1984 RVA: 0x0003D98C File Offset: 0x0003C98C
			protected void SetValue(string fieldName, int value)
			{
				IObjMan objMan = base.GetObjectMainipulator();
				if (objMan != null)
				{
					objMan.SetValue(fieldName, value);
				}
			}

			// Token: 0x060007C1 RID: 1985 RVA: 0x0003D9AB File Offset: 0x0003C9AB
			protected ZoneTableObject(ZoneClass _zone, IObjMan objMan) : base(objMan)
			{
				this.zone = _zone;
			}

			// Token: 0x060007C2 RID: 1986 RVA: 0x0003D9C2 File Offset: 0x0003C9C2
			public void SetHeroicType(bool heroic)
			{
				if (heroic)
				{
					this.heroicType |= 4;
					return;
				}
				this.heroicType |= 2;
			}

			// Token: 0x060007C3 RID: 1987 RVA: 0x0003D9E8 File Offset: 0x0003C9E8
			public bool GetCues(IDictionary<DBID, MobTableForm.ZoneTableObject.Cue> cues)
			{
				IObjMan objMan = base.GetObjectMainipulator(true);
				if (objMan != null && cues != null)
				{
					cues.Clear();
					IFieldDesc desc = objMan.GetFieldDesc("interactions.cues");
					if (desc != null)
					{
						IObjMan cuesObjMan = objMan.CreateManipulator("interactions.cues");
						int count = cuesObjMan.GetArraySize();
						for (int index = 0; index < count; index++)
						{
							cuesObjMan.SetArrayIndex(index);
							DBID cueDBID;
							cuesObjMan.GetValue("cue", out cueDBID);
							if (!DBID.IsNullOrEmpty(cueDBID))
							{
								IObjMan cueMan = MobTableForm.mainDb.GetManipulator(cueDBID);
								MobTableForm.ZoneTableObject.GetCue(cueMan, cues);
								cueMan.Dispose();
							}
						}
						cuesObjMan.Dispose();
						return count > 0;
					}
				}
				return false;
			}

			// Token: 0x060007C4 RID: 1988 RVA: 0x0003DA85 File Offset: 0x0003CA85
			public override string ToString()
			{
				return base.GameObject;
			}

			// Token: 0x040005AD RID: 1453
			protected const short ordinalHeroicFlag = 2;

			// Token: 0x040005AE RID: 1454
			protected const short heroicHeroicFlag = 4;

			// Token: 0x040005AF RID: 1455
			public const string mainObjectPropName = "DBObject";

			// Token: 0x040005B0 RID: 1456
			public const string vendorTablePropName = "VendorTable";

			// Token: 0x040005B1 RID: 1457
			public const string isOrdinalPropName = "IsOrdinal";

			// Token: 0x040005B2 RID: 1458
			public const string isHeroicPropName = "IsHeroic";

			// Token: 0x040005B3 RID: 1459
			private short heroicType = 2;

			// Token: 0x040005B4 RID: 1460
			protected readonly ZoneClass zone;

			// Token: 0x020000A7 RID: 167
			public struct Cue
			{
				// Token: 0x060007C5 RID: 1989 RVA: 0x0003DA90 File Offset: 0x0003CA90
				private static string GetFileText(string _filePath)
				{
					string filePath = _filePath;
					DbCommonMethods.CheckFileRef(ref filePath, false);
					if (!string.IsNullOrEmpty(filePath))
					{
						return IDatabase.GetText(filePath);
					}
					return null;
				}

				// Token: 0x060007C6 RID: 1990 RVA: 0x0003DAB8 File Offset: 0x0003CAB8
				public Cue(string _nameFilePath, string _textFilePath)
				{
					this.nameFilePath = _nameFilePath;
					this.textFilePath = _textFilePath;
					DbCommonMethods.CheckFileRef(ref this.nameFilePath, true);
					DbCommonMethods.CheckFileRef(ref this.textFilePath, true);
				}

				// Token: 0x17000130 RID: 304
				// (get) Token: 0x060007C7 RID: 1991 RVA: 0x0003DAE2 File Offset: 0x0003CAE2
				public string NameFilePath
				{
					get
					{
						return this.nameFilePath;
					}
				}

				// Token: 0x17000131 RID: 305
				// (get) Token: 0x060007C8 RID: 1992 RVA: 0x0003DAEA File Offset: 0x0003CAEA
				public string TextFilePath
				{
					get
					{
						return this.textFilePath;
					}
				}

				// Token: 0x060007C9 RID: 1993 RVA: 0x0003DAF2 File Offset: 0x0003CAF2
				public string GetName()
				{
					return MobTableForm.ZoneTableObject.Cue.GetFileText(this.nameFilePath);
				}

				// Token: 0x060007CA RID: 1994 RVA: 0x0003DB00 File Offset: 0x0003CB00
				public string GetText()
				{
					string text = MobTableForm.ZoneTableObject.Cue.GetFileText(this.textFilePath);
					DBMethods.RemoveTags(ref text);
					return text;
				}

				// Token: 0x040005B5 RID: 1461
				private readonly string nameFilePath;

				// Token: 0x040005B6 RID: 1462
				private readonly string textFilePath;
			}
		}

		// Token: 0x020000A8 RID: 168
		private class ZoneTableMob : MobTableForm.ZoneTableObject
		{
			// Token: 0x060007CB RID: 1995 RVA: 0x0003DB21 File Offset: 0x0003CB21
			public ZoneTableMob(ZoneClass _zone, IObjMan objMan) : base(_zone, objMan)
			{
			}

			// Token: 0x17000132 RID: 306
			// (get) Token: 0x060007CC RID: 1996 RVA: 0x0003DB2B File Offset: 0x0003CB2B
			[MobTableForm.TableColPropAttribute(0, true, true)]
			public override GameObjectClass DBObject
			{
				get
				{
					return this;
				}
			}

			// Token: 0x17000133 RID: 307
			// (get) Token: 0x060007CD RID: 1997 RVA: 0x0003DB2E File Offset: 0x0003CB2E
			[MobTableForm.TableColPropAttribute(1, false, true)]
			public GameObjectClass Zone
			{
				get
				{
					return this.zone;
				}
			}

			// Token: 0x17000134 RID: 308
			// (get) Token: 0x060007CE RID: 1998 RVA: 0x0003DB36 File Offset: 0x0003CB36
			[MobTableForm.TableColPropAttribute(2, true)]
			public string ZoneName
			{
				get
				{
					if (this.Zone == null)
					{
						return string.Empty;
					}
					return this.Zone.GameName;
				}
			}

			// Token: 0x17000135 RID: 309
			// (get) Token: 0x060007CF RID: 1999 RVA: 0x0003DB51 File Offset: 0x0003CB51
			// (set) Token: 0x060007D0 RID: 2000 RVA: 0x0003DB5F File Offset: 0x0003CB5F
			[MobTableForm.TableColPropAttribute(3, true)]
			public string NameText
			{
				get
				{
					return base.GetTextValue("name", false);
				}
				set
				{
					base.SetTextValue("name", value, this.Name + "_Name.txt", false);
				}
			}

			// Token: 0x17000136 RID: 310
			// (get) Token: 0x060007D1 RID: 2001 RVA: 0x0003DB7F File Offset: 0x0003CB7F
			// (set) Token: 0x060007D2 RID: 2002 RVA: 0x0003DB8D File Offset: 0x0003CB8D
			[MobTableForm.TableColPropAttribute(4, true)]
			public string TitleText
			{
				get
				{
					return base.GetTextValue("title", false);
				}
				set
				{
					base.SetTextValue("title", value, this.Name + "_Title.txt", false);
				}
			}

			// Token: 0x17000137 RID: 311
			// (get) Token: 0x060007D3 RID: 2003 RVA: 0x0003DBAD File Offset: 0x0003CBAD
			[MobTableForm.TableColPropAttribute(5)]
			public override string VendorTable
			{
				get
				{
					return base.GetVendorTable();
				}
			}

			// Token: 0x17000138 RID: 312
			// (get) Token: 0x060007D4 RID: 2004 RVA: 0x0003DBB5 File Offset: 0x0003CBB5
			[MobTableForm.TableColPropAttribute(6)]
			public override bool IsOrdinal
			{
				get
				{
					return (base.HeroicType & 2) != 0;
				}
			}

			// Token: 0x17000139 RID: 313
			// (get) Token: 0x060007D5 RID: 2005 RVA: 0x0003DBC5 File Offset: 0x0003CBC5
			[MobTableForm.TableColPropAttribute(7)]
			public override bool IsHeroic
			{
				get
				{
					return (base.HeroicType & 4) != 0;
				}
			}

			// Token: 0x1700013A RID: 314
			// (get) Token: 0x060007D6 RID: 2006 RVA: 0x0003DBD5 File Offset: 0x0003CBD5
			// (set) Token: 0x060007D7 RID: 2007 RVA: 0x0003DBE2 File Offset: 0x0003CBE2
			[MobTableForm.TableColPropAttribute(8, true)]
			public int LvlMin
			{
				get
				{
					return base.GetIntValue("levelMin");
				}
				set
				{
					base.SetValue("levelMin", value);
				}
			}

			// Token: 0x1700013B RID: 315
			// (get) Token: 0x060007D8 RID: 2008 RVA: 0x0003DBF0 File Offset: 0x0003CBF0
			// (set) Token: 0x060007D9 RID: 2009 RVA: 0x0003DBFD File Offset: 0x0003CBFD
			[MobTableForm.TableColPropAttribute(9, true)]
			public int LvlMax
			{
				get
				{
					return base.GetIntValue("levelMax");
				}
				set
				{
					base.SetValue("levelMax", value);
				}
			}
		}

		// Token: 0x020000A9 RID: 169
		private class ZoneTableDevice : MobTableForm.ZoneTableObject
		{
			// Token: 0x060007DA RID: 2010 RVA: 0x0003DC0B File Offset: 0x0003CC0B
			public ZoneTableDevice(ZoneClass _zone, IObjMan objMan) : base(_zone, objMan)
			{
			}

			// Token: 0x1700013C RID: 316
			// (get) Token: 0x060007DB RID: 2011 RVA: 0x0003DC15 File Offset: 0x0003CC15
			[MobTableForm.TableColPropAttribute(0, true, true)]
			public override GameObjectClass DBObject
			{
				get
				{
					return this;
				}
			}

			// Token: 0x1700013D RID: 317
			// (get) Token: 0x060007DC RID: 2012 RVA: 0x0003DC18 File Offset: 0x0003CC18
			[MobTableForm.TableColPropAttribute(1, false, true)]
			public GameObjectClass Zone
			{
				get
				{
					return this.zone;
				}
			}

			// Token: 0x1700013E RID: 318
			// (get) Token: 0x060007DD RID: 2013 RVA: 0x0003DC20 File Offset: 0x0003CC20
			[MobTableForm.TableColPropAttribute(2, true)]
			public string ZoneName
			{
				get
				{
					if (this.Zone == null)
					{
						return string.Empty;
					}
					return this.Zone.GameName;
				}
			}

			// Token: 0x1700013F RID: 319
			// (get) Token: 0x060007DE RID: 2014 RVA: 0x0003DC3B File Offset: 0x0003CC3B
			// (set) Token: 0x060007DF RID: 2015 RVA: 0x0003DC49 File Offset: 0x0003CC49
			[MobTableForm.TableColPropAttribute(3, true)]
			public string NameText
			{
				get
				{
					return base.GetTextValue("name", false);
				}
				set
				{
					base.SetTextValue("name", value, this.Name + "_Name.txt", false);
				}
			}

			// Token: 0x17000140 RID: 320
			// (get) Token: 0x060007E0 RID: 2016 RVA: 0x0003DC69 File Offset: 0x0003CC69
			// (set) Token: 0x060007E1 RID: 2017 RVA: 0x0003DC77 File Offset: 0x0003CC77
			[MobTableForm.TableColPropAttribute(4, true)]
			public string ExploitingText
			{
				get
				{
					return base.GetTextValue("exploitingText", false);
				}
				set
				{
					base.SetTextValue("exploitingText", value, this.Name + "_ExploitingText.txt", false);
				}
			}

			// Token: 0x17000141 RID: 321
			// (get) Token: 0x060007E2 RID: 2018 RVA: 0x0003DC97 File Offset: 0x0003CC97
			// (set) Token: 0x060007E3 RID: 2019 RVA: 0x0003DCA5 File Offset: 0x0003CCA5
			[MobTableForm.TableColPropAttribute(5, true)]
			public string OpeningText
			{
				get
				{
					return base.GetTextValue("openingText", false);
				}
				set
				{
					base.SetTextValue("openingText", value, this.Name + "_OpeningText.txt", false);
				}
			}

			// Token: 0x17000142 RID: 322
			// (get) Token: 0x060007E4 RID: 2020 RVA: 0x0003DCC5 File Offset: 0x0003CCC5
			[MobTableForm.TableColPropAttribute(6)]
			public override string VendorTable
			{
				get
				{
					return base.GetVendorTable();
				}
			}

			// Token: 0x17000143 RID: 323
			// (get) Token: 0x060007E5 RID: 2021 RVA: 0x0003DCCD File Offset: 0x0003CCCD
			[MobTableForm.TableColPropAttribute(7)]
			public override bool IsOrdinal
			{
				get
				{
					return (base.HeroicType & 2) != 0;
				}
			}

			// Token: 0x17000144 RID: 324
			// (get) Token: 0x060007E6 RID: 2022 RVA: 0x0003DCDD File Offset: 0x0003CCDD
			[MobTableForm.TableColPropAttribute(8)]
			public override bool IsHeroic
			{
				get
				{
					return (base.HeroicType & 4) != 0;
				}
			}
		}

		// Token: 0x020000AA RID: 170
		private static class DataBinding
		{
			// Token: 0x060007E7 RID: 2023 RVA: 0x0003DCF0 File Offset: 0x0003CCF0
			private static MobTableForm.TableColPropAttribute FindAttribute(ICustomAttributeProvider property)
			{
				if (property != null)
				{
					object[] attributes = property.GetCustomAttributes(false);
					foreach (object attribute in attributes)
					{
						MobTableForm.TableColPropAttribute tableColPropAttribute = attribute as MobTableForm.TableColPropAttribute;
						if (tableColPropAttribute != null)
						{
							return tableColPropAttribute;
						}
					}
				}
				return null;
			}

			// Token: 0x060007E8 RID: 2024 RVA: 0x0003DD35 File Offset: 0x0003CD35
			private static MobTableForm.TableColPropAttribute FindAttribute(Type type, string propertyName)
			{
				return MobTableForm.DataBinding.FindAttribute(type.GetProperty(propertyName));
			}

			// Token: 0x060007E9 RID: 2025 RVA: 0x0003DD43 File Offset: 0x0003CD43
			public static object GetPropertyValue(object obj, string propertyName)
			{
				return obj.GetType().GetProperty(propertyName).GetValue(obj, null);
			}

			// Token: 0x060007EA RID: 2026 RVA: 0x0003DD58 File Offset: 0x0003CD58
			public static void SetPropertyValue(object obj, string propertyName, object value)
			{
				obj.GetType().GetProperty(propertyName).SetValue(obj, value, null);
			}

			// Token: 0x060007EB RID: 2027 RVA: 0x0003DD70 File Offset: 0x0003CD70
			public static void InitTable(DataTable dataTable, Type type)
			{
				if (dataTable == null)
				{
					return;
				}
				PropertyInfo[] properties = type.GetProperties();
				PropertyInfo[] colFields = new PropertyInfo[properties.Length];
				int colPropLastIndex = 0;
				foreach (PropertyInfo property in properties)
				{
					MobTableForm.TableColPropAttribute tableColPropAttribute = MobTableForm.DataBinding.FindAttribute(property);
					if (tableColPropAttribute != null)
					{
						if (tableColPropAttribute.ColIndex > colPropLastIndex)
						{
							colPropLastIndex = tableColPropAttribute.ColIndex;
						}
						colFields[tableColPropAttribute.ColIndex] = property;
					}
				}
				Array.Resize<PropertyInfo>(ref colFields, colPropLastIndex + 1);
				dataTable.BeginInit();
				List<DataColumn> primaryKeyColumns = new List<DataColumn>();
				for (int index = 0; index <= colPropLastIndex; index++)
				{
					PropertyInfo prop = colFields[index];
					DataColumn column = dataTable.Columns.Add(prop.Name, prop.PropertyType);
					column.ReadOnly = !prop.CanWrite;
					MobTableForm.TableColPropAttribute attribute = MobTableForm.DataBinding.FindAttribute(prop);
					if (attribute != null && attribute.PrimaryKey)
					{
						primaryKeyColumns.Add(column);
					}
				}
				dataTable.PrimaryKey = primaryKeyColumns.ToArray();
				dataTable.EndInit();
				dataTable.ColumnChanged += MobTableForm.OnTableColumnChanged;
			}

			// Token: 0x060007EC RID: 2028 RVA: 0x0003DE74 File Offset: 0x0003CE74
			public static void AddRowInTable(DataTable dataTable, GameObjectClass zoneObject, string tableFilter)
			{
				if (dataTable == null)
				{
					return;
				}
				if (zoneObject != null && (string.IsNullOrEmpty(tableFilter) || zoneObject.FindInXDBFile(tableFilter)))
				{
					int cnt = dataTable.PrimaryKey.Length;
					object[] keyValues = new object[cnt];
					for (int index = 0; index < cnt; index++)
					{
						keyValues[index] = MobTableForm.DataBinding.GetPropertyValue(zoneObject, dataTable.PrimaryKey[index].ColumnName);
					}
					if (dataTable.Rows.Find(keyValues) == null)
					{
						object[] values = new object[dataTable.Columns.Count];
						for (int index2 = 0; index2 < dataTable.Columns.Count; index2++)
						{
							values[index2] = MobTableForm.DataBinding.GetPropertyValue(zoneObject, dataTable.Columns[index2].ColumnName);
						}
						dataTable.Rows.Add(values);
					}
				}
			}

			// Token: 0x060007ED RID: 2029 RVA: 0x0003DF34 File Offset: 0x0003CF34
			public static void BindTable(DataTable dataTable, DataGridView gridView, Type type)
			{
				gridView.DataSource = dataTable;
				foreach (object obj in gridView.Columns)
				{
					DataGridViewColumn column = (DataGridViewColumn)obj;
					column.ReadOnly = dataTable.Columns[column.DataPropertyName].ReadOnly;
					MobTableForm.TableColPropAttribute attribute = MobTableForm.DataBinding.FindAttribute(type, column.DataPropertyName);
					if (attribute != null)
					{
						column.Visible = attribute.Visible;
					}
				}
			}
		}
	}
}
