using System;
using System.Drawing;
using System.Windows.Forms;
using Db;

namespace MapEditor.Forms.Layers
{
	// Token: 0x020002D6 RID: 726
	public class Layers
	{
		// Token: 0x14000101 RID: 257
		// (add) Token: 0x0600216B RID: 8555 RVA: 0x000D31A0 File Offset: 0x000D21A0
		// (remove) Token: 0x0600216C RID: 8556 RVA: 0x000D31B9 File Offset: 0x000D21B9
		public event Layers.SelectedIndexChangedEvent BeforeSelectedIndexChanged;

		// Token: 0x14000102 RID: 258
		// (add) Token: 0x0600216D RID: 8557 RVA: 0x000D31D2 File Offset: 0x000D21D2
		// (remove) Token: 0x0600216E RID: 8558 RVA: 0x000D31EB File Offset: 0x000D21EB
		public event Layers.SelectedIndexChangedEvent AfterSelectedIndexChanged;

		// Token: 0x14000103 RID: 259
		// (add) Token: 0x0600216F RID: 8559 RVA: 0x000D3204 File Offset: 0x000D2204
		// (remove) Token: 0x06002170 RID: 8560 RVA: 0x000D321D File Offset: 0x000D221D
		public event Layers.LayersListModifiedEvent LayersListModified;

		// Token: 0x06002171 RID: 8561 RVA: 0x000D3238 File Offset: 0x000D2238
		private void GetValue(string property, out string value)
		{
			value = string.Empty;
			if (this.Check())
			{
				this.layersMan.GetValue(string.Concat(new object[]
				{
					"Layers.[",
					this.selectedLayerIndex,
					"].",
					property
				}), out value);
			}
		}

		// Token: 0x06002172 RID: 8562 RVA: 0x000D3290 File Offset: 0x000D2290
		private void GetValue(string property, out int value)
		{
			value = 0;
			if (this.Check())
			{
				this.layersMan.GetValue(string.Concat(new object[]
				{
					"Layers.[",
					this.selectedLayerIndex,
					"].",
					property
				}), out value);
			}
		}

		// Token: 0x06002173 RID: 8563 RVA: 0x000D32E4 File Offset: 0x000D22E4
		private void GetValue(string property, out float value)
		{
			value = 0f;
			if (this.Check())
			{
				this.layersMan.GetValue(string.Concat(new object[]
				{
					"Layers.[",
					this.selectedLayerIndex,
					"].",
					property
				}), out value);
			}
		}

		// Token: 0x06002174 RID: 8564 RVA: 0x000D333C File Offset: 0x000D233C
		private void SetValue(string property, string value)
		{
			if (this.Check())
			{
				this.layersMan.SetValue(string.Concat(new object[]
				{
					"Layers.[",
					this.selectedLayerIndex,
					"].",
					property
				}), value);
			}
		}

		// Token: 0x06002175 RID: 8565 RVA: 0x000D338C File Offset: 0x000D238C
		private void SetValue(string property, int value)
		{
			if (this.Check())
			{
				this.layersMan.SetValue(string.Concat(new object[]
				{
					"Layers.[",
					this.selectedLayerIndex,
					"].",
					property
				}), value);
			}
		}

		// Token: 0x06002176 RID: 8566 RVA: 0x000D33DC File Offset: 0x000D23DC
		private void SetValue(string property, float value)
		{
			if (this.Check())
			{
				this.layersMan.SetValue(string.Concat(new object[]
				{
					"Layers.[",
					this.selectedLayerIndex,
					"].",
					property
				}), value);
			}
		}

		// Token: 0x06002177 RID: 8567 RVA: 0x000D342C File Offset: 0x000D242C
		private void SetValue(string property, DBID value)
		{
			if (this.Check())
			{
				this.layersMan.SetValue(string.Concat(new object[]
				{
					"Layers.[",
					this.selectedLayerIndex,
					"].",
					property
				}), value);
			}
		}

		// Token: 0x06002178 RID: 8568 RVA: 0x000D347C File Offset: 0x000D247C
		public Layers()
		{
			this.AfterSelectedIndexChanged = (Layers.SelectedIndexChangedEvent)Delegate.Combine(this.AfterSelectedIndexChanged, new Layers.SelectedIndexChangedEvent(this.OnAfterSelectedIndexChanged));
		}

		// Token: 0x06002179 RID: 8569 RVA: 0x000D34B8 File Offset: 0x000D24B8
		public string GetName()
		{
			return this.name;
		}

		// Token: 0x0600217A RID: 8570 RVA: 0x000D34C0 File Offset: 0x000D24C0
		public bool Load(string layersDBIDName)
		{
			this.name = layersDBIDName;
			DBID layersDBID = IDatabase.CreateDBIDByName(layersDBIDName);
			IDatabase mainDb = IDatabase.GetMainDatabase();
			if (!mainDb.DoesObjectExist(layersDBID))
			{
				return false;
			}
			this.layersMan = mainDb.GetManipulator(layersDBID);
			if (this.layersMan == null)
			{
				return false;
			}
			this.layersMan.GetValue("Layers", out this.layersCount);
			return true;
		}

		// Token: 0x0600217B RID: 8571 RVA: 0x000D351C File Offset: 0x000D251C
		public bool LoadLayersToCmb(ComboBox layersComboBox)
		{
			if (this.layersMan == null)
			{
				return false;
			}
			layersComboBox.Items.Clear();
			for (int index = 0; index < this.layersCount; index++)
			{
				string layerPropertyName = string.Format("Layers.[{0}].", index);
				string name;
				this.layersMan.GetValue(layerPropertyName + "SymbolName", out name);
				if (string.IsNullOrEmpty(name.Trim()))
				{
					break;
				}
				layersComboBox.Items.Add(name);
			}
			return true;
		}

		// Token: 0x17000771 RID: 1905
		// (get) Token: 0x0600217C RID: 8572 RVA: 0x000D3593 File Offset: 0x000D2593
		// (set) Token: 0x0600217D RID: 8573 RVA: 0x000D359C File Offset: 0x000D259C
		public int SelectedLayerIndex
		{
			get
			{
				return this.selectedLayerIndex;
			}
			set
			{
				int oldIndex = this.selectedLayerIndex;
				if (this.BeforeSelectedIndexChanged != null && oldIndex != value)
				{
					this.BeforeSelectedIndexChanged(oldIndex, value);
				}
				this.selectedLayerIndex = value;
				if (this.AfterSelectedIndexChanged != null && oldIndex != value)
				{
					this.AfterSelectedIndexChanged(oldIndex, value);
				}
			}
		}

		// Token: 0x0600217E RID: 8574 RVA: 0x000D35E9 File Offset: 0x000D25E9
		public bool Check()
		{
			return this.layersMan != null && this.selectedLayerIndex >= 0 && this.selectedLayerIndex <= this.layersCount - 1;
		}

		// Token: 0x0600217F RID: 8575 RVA: 0x000D3610 File Offset: 0x000D2610
		public string GetTerrainTexture()
		{
			string value;
			this.GetValue("DiffuseTexture", out value);
			return value;
		}

		// Token: 0x06002180 RID: 8576 RVA: 0x000D362C File Offset: 0x000D262C
		public Color GetMinimapColor()
		{
			int value;
			this.GetValue("LayerColor", out value);
			return Color.FromArgb(255, Color.FromArgb(value));
		}

		// Token: 0x06002181 RID: 8577 RVA: 0x000D3658 File Offset: 0x000D2658
		public string GetFoliageTexture(int index)
		{
			string value;
			this.GetValue("foliage" + index + ".texture", out value);
			return value;
		}

		// Token: 0x06002182 RID: 8578 RVA: 0x000D3684 File Offset: 0x000D2684
		public int GetProbability(int index)
		{
			int value;
			this.GetValue("foliage" + index + ".probability", out value);
			return value;
		}

		// Token: 0x06002183 RID: 8579 RVA: 0x000D36B0 File Offset: 0x000D26B0
		public int GetNumLeaves(int index)
		{
			int value;
			this.GetValue("foliage" + index + ".numLeaves", out value);
			return value;
		}

		// Token: 0x06002184 RID: 8580 RVA: 0x000D36DC File Offset: 0x000D26DC
		public float GetIntensityLimit(int index)
		{
			float value;
			this.GetValue("foliage" + index + ".intensityLimit", out value);
			return value;
		}

		// Token: 0x06002185 RID: 8581 RVA: 0x000D3708 File Offset: 0x000D2708
		public float GetMinScale(int index)
		{
			float value;
			this.GetValue("foliage" + index + ".minScale", out value);
			return value;
		}

		// Token: 0x06002186 RID: 8582 RVA: 0x000D3734 File Offset: 0x000D2734
		public float GetMaxScale(int index)
		{
			float value;
			this.GetValue("foliage" + index + ".maxScale", out value);
			return value;
		}

		// Token: 0x06002187 RID: 8583 RVA: 0x000D3760 File Offset: 0x000D2760
		public float GetTopHeight(int index)
		{
			float value;
			this.GetValue("foliage" + index + ".top.height", out value);
			return value;
		}

		// Token: 0x06002188 RID: 8584 RVA: 0x000D378C File Offset: 0x000D278C
		public float GetTopOffset(int index)
		{
			float value;
			this.GetValue("foliage" + index + ".top.offset", out value);
			return value;
		}

		// Token: 0x06002189 RID: 8585 RVA: 0x000D37B8 File Offset: 0x000D27B8
		public float GetTopWidth(int index)
		{
			float value;
			this.GetValue("foliage" + index + ".top.width", out value);
			return value;
		}

		// Token: 0x0600218A RID: 8586 RVA: 0x000D37E4 File Offset: 0x000D27E4
		public float GetBottomHeight(int index)
		{
			float value;
			this.GetValue("foliage" + index + ".bottom.height", out value);
			return value;
		}

		// Token: 0x0600218B RID: 8587 RVA: 0x000D3810 File Offset: 0x000D2810
		public float GetBottomOffset(int index)
		{
			float value;
			this.GetValue("foliage" + index + ".bottom.offset", out value);
			return value;
		}

		// Token: 0x0600218C RID: 8588 RVA: 0x000D383C File Offset: 0x000D283C
		public float GetBottomWidth(int index)
		{
			float value;
			this.GetValue("foliage" + index + ".bottom.width", out value);
			return value;
		}

		// Token: 0x0600218D RID: 8589 RVA: 0x000D3868 File Offset: 0x000D2868
		public void SetTerrain(string dbidstring)
		{
			DBID terrainDBID;
			if (!string.IsNullOrEmpty(dbidstring))
			{
				terrainDBID = IDatabase.CreateDBIDByName(dbidstring);
			}
			else
			{
				terrainDBID = DBID.Empty;
			}
			this.SetValue("DiffuseTexture", terrainDBID);
		}

		// Token: 0x0600218E RID: 8590 RVA: 0x000D3898 File Offset: 0x000D2898
		public void SetMinimapColor(Color value)
		{
			this.SetValue("LayerColor", value.ToArgb());
		}

		// Token: 0x0600218F RID: 8591 RVA: 0x000D38AC File Offset: 0x000D28AC
		public void SetFoliage(int index, string value)
		{
			DBID foliageDBID;
			if (!string.IsNullOrEmpty(value))
			{
				foliageDBID = IDatabase.CreateDBIDByName(value);
			}
			else
			{
				foliageDBID = DBID.Empty;
			}
			this.SetValue("foliage" + index + ".texture", foliageDBID);
		}

		// Token: 0x06002190 RID: 8592 RVA: 0x000D38EC File Offset: 0x000D28EC
		public void SetProbability(int index, int value)
		{
			this.SetValue("foliage" + index + ".probability", value);
		}

		// Token: 0x06002191 RID: 8593 RVA: 0x000D390A File Offset: 0x000D290A
		public void SetNumLeaves(int index, int value)
		{
			this.SetValue("foliage" + index + ".numLeaves", value);
		}

		// Token: 0x06002192 RID: 8594 RVA: 0x000D3928 File Offset: 0x000D2928
		public void SetIntensityLimit(int index, float value)
		{
			this.SetValue("foliage" + index + ".intensityLimit", value);
		}

		// Token: 0x06002193 RID: 8595 RVA: 0x000D3946 File Offset: 0x000D2946
		public void SetMinScale(int index, float value)
		{
			this.SetValue("foliage" + index + ".minScale", value);
		}

		// Token: 0x06002194 RID: 8596 RVA: 0x000D3964 File Offset: 0x000D2964
		public void SetMaxScale(int index, float value)
		{
			this.SetValue("foliage" + index + ".maxScale", value);
		}

		// Token: 0x06002195 RID: 8597 RVA: 0x000D3982 File Offset: 0x000D2982
		public void SetTopHeight(int index, float value)
		{
			this.SetValue("foliage" + index + ".top.height", value);
		}

		// Token: 0x06002196 RID: 8598 RVA: 0x000D39A0 File Offset: 0x000D29A0
		public void SetTopOffset(int index, float value)
		{
			this.SetValue("foliage" + index + ".top.offset", value);
		}

		// Token: 0x06002197 RID: 8599 RVA: 0x000D39BE File Offset: 0x000D29BE
		public void SetTopWidth(int index, float value)
		{
			this.SetValue("foliage" + index + ".top.width", value);
		}

		// Token: 0x06002198 RID: 8600 RVA: 0x000D39DC File Offset: 0x000D29DC
		public void SetBottomHeight(int index, float value)
		{
			this.SetValue("foliage" + index + ".bottom.height", value);
		}

		// Token: 0x06002199 RID: 8601 RVA: 0x000D39FA File Offset: 0x000D29FA
		public void SetBottomOffset(int index, float value)
		{
			this.SetValue("foliage" + index + ".bottom.offset", value);
		}

		// Token: 0x0600219A RID: 8602 RVA: 0x000D3A18 File Offset: 0x000D2A18
		public void SetBottomWidth(int index, float value)
		{
			this.SetValue("foliage" + index + ".bottom.width", value);
		}

		// Token: 0x0600219B RID: 8603 RVA: 0x000D3A36 File Offset: 0x000D2A36
		public bool SaveDatabase()
		{
			if (IDatabase.GetMainDatabase() != null)
			{
				IDatabase.GetMainDatabase().SaveChanges();
				this.RememberValues();
				return true;
			}
			return false;
		}

		// Token: 0x0600219C RID: 8604 RVA: 0x000D3A54 File Offset: 0x000D2A54
		public bool CopyLayer(string newLayerName)
		{
			if (!this.Check())
			{
				return false;
			}
			int newLayerIndex = 0;
			for (int index = 0; index < this.layersCount; index++)
			{
				string layerPropertyName = string.Format("Layers.[{0}].", index);
				string name;
				this.layersMan.GetValue(layerPropertyName + "SymbolName", out name);
				if (string.IsNullOrEmpty(name.Trim()))
				{
					newLayerIndex = index;
					break;
				}
			}
			this.layersMan.SetValue("Layers.[" + newLayerIndex + "].SymbolName", newLayerName);
			DBID DBIDValue;
			this.layersMan.GetValue("Layers.[" + this.selectedLayerIndex + "].DiffuseTexture", out DBIDValue);
			this.layersMan.SetValue("Layers.[" + newLayerIndex + "].DiffuseTexture", DBIDValue);
			int intValue;
			float floatValue;
			for (int i = 0; i < 4; i++)
			{
				this.layersMan.GetValue(string.Concat(new object[]
				{
					"Layers.[",
					this.selectedLayerIndex,
					"].foliage",
					i,
					".texture"
				}), out DBIDValue);
				if (!(DBIDValue == null))
				{
					this.layersMan.SetValue(string.Concat(new object[]
					{
						"Layers.[",
						newLayerIndex,
						"].foliage",
						i,
						".texture"
					}), DBIDValue);
					this.layersMan.GetValue(string.Concat(new object[]
					{
						"Layers.[",
						this.selectedLayerIndex,
						"].foliage",
						i,
						".probability"
					}), out intValue);
					this.layersMan.SetValue(string.Concat(new object[]
					{
						"Layers.[",
						newLayerIndex,
						"].foliage",
						i,
						".probability"
					}), intValue);
					this.layersMan.GetValue(string.Concat(new object[]
					{
						"Layers.[",
						this.selectedLayerIndex,
						"].foliage",
						i,
						".numLeaves"
					}), out intValue);
					this.layersMan.SetValue(string.Concat(new object[]
					{
						"Layers.[",
						newLayerIndex,
						"].foliage",
						i,
						".numLeaves"
					}), intValue);
					this.layersMan.GetValue(string.Concat(new object[]
					{
						"Layers.[",
						this.selectedLayerIndex,
						"].foliage",
						i,
						".minScale"
					}), out floatValue);
					this.layersMan.SetValue(string.Concat(new object[]
					{
						"Layers.[",
						newLayerIndex,
						"].foliage",
						i,
						".minScale"
					}), floatValue);
					this.layersMan.GetValue(string.Concat(new object[]
					{
						"Layers.[",
						this.selectedLayerIndex,
						"].foliage",
						i,
						".maxScale"
					}), out floatValue);
					this.layersMan.SetValue(string.Concat(new object[]
					{
						"Layers.[",
						newLayerIndex,
						"].foliage",
						i,
						".maxScale"
					}), floatValue);
					this.layersMan.GetValue(string.Concat(new object[]
					{
						"Layers.[",
						this.selectedLayerIndex,
						"].foliage",
						i,
						".top.height"
					}), out floatValue);
					this.layersMan.SetValue(string.Concat(new object[]
					{
						"Layers.[",
						newLayerIndex,
						"].foliage",
						i,
						".top.height"
					}), floatValue);
					this.layersMan.GetValue(string.Concat(new object[]
					{
						"Layers.[",
						this.selectedLayerIndex,
						"].foliage",
						i,
						".top.offset"
					}), out floatValue);
					this.layersMan.SetValue(string.Concat(new object[]
					{
						"Layers.[",
						newLayerIndex,
						"].foliage",
						i,
						".top.offset"
					}), floatValue);
					this.layersMan.GetValue(string.Concat(new object[]
					{
						"Layers.[",
						this.selectedLayerIndex,
						"].foliage",
						i,
						".top.width"
					}), out floatValue);
					this.layersMan.SetValue(string.Concat(new object[]
					{
						"Layers.[",
						newLayerIndex,
						"].foliage",
						i,
						".top.width"
					}), floatValue);
					this.layersMan.GetValue(string.Concat(new object[]
					{
						"Layers.[",
						this.selectedLayerIndex,
						"].foliage",
						i,
						".bottom.height"
					}), out floatValue);
					this.layersMan.SetValue(string.Concat(new object[]
					{
						"Layers.[",
						newLayerIndex,
						"].foliage",
						i,
						".bottom.height"
					}), floatValue);
					this.layersMan.GetValue(string.Concat(new object[]
					{
						"Layers.[",
						this.selectedLayerIndex,
						"].foliage",
						i,
						".bottom.offset"
					}), out floatValue);
					this.layersMan.SetValue(string.Concat(new object[]
					{
						"Layers.[",
						newLayerIndex,
						"].foliage",
						i,
						".bottom.offset"
					}), floatValue);
					this.layersMan.GetValue(string.Concat(new object[]
					{
						"Layers.[",
						this.selectedLayerIndex,
						"].foliage",
						i,
						".bottom.width"
					}), out floatValue);
					this.layersMan.SetValue(string.Concat(new object[]
					{
						"Layers.[",
						newLayerIndex,
						"].foliage",
						i,
						".bottom.width"
					}), floatValue);
				}
			}
			this.layersMan.GetValue("Layers.[" + this.selectedLayerIndex + "].EyeSpecularLightColor", out intValue);
			this.layersMan.SetValue("Layers.[" + newLayerIndex + "].EyeSpecularLightColor", intValue);
			this.layersMan.GetValue("Layers.[" + this.selectedLayerIndex + "].DirectionalSpeculatLightColor", out intValue);
			this.layersMan.SetValue("Layers.[" + newLayerIndex + "].DirectionalSpeculatLightColor", intValue);
			this.layersMan.GetValue("Layers.[" + this.selectedLayerIndex + "].EyeExponent", out floatValue);
			this.layersMan.SetValue("Layers.[" + newLayerIndex + "].EyeExponent", floatValue);
			this.layersMan.GetValue("Layers.[" + this.selectedLayerIndex + "].DirectionalExponent", out floatValue);
			this.layersMan.SetValue("Layers.[" + newLayerIndex + "].DirectionalExponent", floatValue);
			this.layersMan.GetValue("Layers.[" + this.selectedLayerIndex + "].LayerColor", out floatValue);
			this.layersMan.SetValue("Layers.[" + newLayerIndex + "].LayerColor", floatValue);
			if (this.LayersListModified != null)
			{
				this.LayersListModified();
			}
			return true;
		}

		// Token: 0x0600219D RID: 8605 RVA: 0x000D4388 File Offset: 0x000D3388
		public bool NewLayer(string newLayerName, string dbidstring)
		{
			int newLayerIndex = 0;
			for (int index = 0; index < this.layersCount; index++)
			{
				string layerPropertyName = string.Format("Layers.[{0}].", index);
				string name;
				this.layersMan.GetValue(layerPropertyName + "SymbolName", out name);
				if (string.IsNullOrEmpty(name.Trim()))
				{
					newLayerIndex = index;
					break;
				}
			}
			this.layersMan.SetValue("Layers.[" + newLayerIndex + "].SymbolName", newLayerName);
			DBID terrainDBID = IDatabase.CreateDBIDByName(dbidstring);
			this.layersMan.SetValue("Layers.[" + newLayerIndex + "].DiffuseTexture", terrainDBID);
			if (this.LayersListModified != null)
			{
				this.LayersListModified();
			}
			return true;
		}

		// Token: 0x0600219E RID: 8606 RVA: 0x000D4441 File Offset: 0x000D3441
		public bool RenameLayer(string newName)
		{
			if (!this.Check())
			{
				return false;
			}
			this.SetValue("SymbolName", newName);
			if (this.LayersListModified != null)
			{
				this.LayersListModified();
			}
			return true;
		}

		// Token: 0x0600219F RID: 8607 RVA: 0x000D4470 File Offset: 0x000D3470
		public bool RememberValues()
		{
			if (!this.Check())
			{
				return false;
			}
			this.currentLayer.minimapColor = this.GetMinimapColor();
			this.currentLayer.terrainTexture = this.GetTerrainTexture();
			for (int i = 0; i < 4; i++)
			{
				this.currentLayer.foliages[i].foliageTexture = this.GetFoliageTexture(i);
				this.currentLayer.foliages[i].probability = this.GetProbability(i);
				this.currentLayer.foliages[i].numLeaves = this.GetNumLeaves(i);
				this.currentLayer.foliages[i].minScale = this.GetMinScale(i);
				this.currentLayer.foliages[i].maxScale = this.GetMaxScale(i);
				this.currentLayer.foliages[i].topWidth = this.GetBottomWidth(i);
				this.currentLayer.foliages[i].topHeight = this.GetTopHeight(i);
				this.currentLayer.foliages[i].topOffset = this.GetTopOffset(i);
				this.currentLayer.foliages[i].bottomWidth = this.GetBottomWidth(i);
				this.currentLayer.foliages[i].bottomHeight = this.GetBottomHeight(i);
				this.currentLayer.foliages[i].bottomOffset = this.GetBottomOffset(i);
			}
			return true;
		}

		// Token: 0x060021A0 RID: 8608 RVA: 0x000D45D0 File Offset: 0x000D35D0
		public bool ResetValues()
		{
			if (!this.Check())
			{
				return false;
			}
			this.SetMinimapColor(this.currentLayer.minimapColor);
			this.SetTerrain(this.currentLayer.terrainTexture);
			for (int i = 0; i < 4; i++)
			{
				this.SetFoliage(i, this.currentLayer.foliages[i].foliageTexture);
				this.SetProbability(i, this.currentLayer.foliages[i].probability);
				this.SetNumLeaves(i, this.currentLayer.foliages[i].numLeaves);
				this.SetMinScale(i, this.currentLayer.foliages[i].minScale);
				this.SetMaxScale(i, this.currentLayer.foliages[i].maxScale);
				this.SetTopWidth(i, this.currentLayer.foliages[i].topWidth);
				this.SetTopHeight(i, this.currentLayer.foliages[i].topHeight);
				this.SetTopOffset(i, this.currentLayer.foliages[i].topOffset);
				this.SetBottomWidth(i, this.currentLayer.foliages[i].bottomWidth);
				this.SetBottomHeight(i, this.currentLayer.foliages[i].bottomHeight);
				this.SetBottomOffset(i, this.currentLayer.foliages[i].bottomOffset);
			}
			return true;
		}

		// Token: 0x060021A1 RID: 8609 RVA: 0x000D472F File Offset: 0x000D372F
		private void OnAfterSelectedIndexChanged(int oldIndex, int newIndex)
		{
			this.RememberValues();
		}

		// Token: 0x04001433 RID: 5171
		private const int foliageCount = 4;

		// Token: 0x04001434 RID: 5172
		private IObjMan layersMan;

		// Token: 0x04001435 RID: 5173
		private int selectedLayerIndex = -1;

		// Token: 0x04001436 RID: 5174
		private int layersCount;

		// Token: 0x04001437 RID: 5175
		private readonly Layers.Layer currentLayer = new Layers.Layer();

		// Token: 0x04001438 RID: 5176
		private string name;

		// Token: 0x020002D7 RID: 727
		// (Invoke) Token: 0x060021A3 RID: 8611
		public delegate void SelectedIndexChangedEvent(int oldIndex, int newIndex);

		// Token: 0x020002D8 RID: 728
		// (Invoke) Token: 0x060021A7 RID: 8615
		public delegate void LayersListModifiedEvent();

		// Token: 0x020002D9 RID: 729
		private class Layer
		{
			// Token: 0x060021AA RID: 8618 RVA: 0x000D4738 File Offset: 0x000D3738
			public Layer()
			{
				for (int i = 0; i < this.foliages.Length; i++)
				{
					this.foliages[i] = new Layers.Layer.Foliage();
				}
			}

			// Token: 0x0400143C RID: 5180
			public string terrainTexture = string.Empty;

			// Token: 0x0400143D RID: 5181
			public Color minimapColor = default(Color);

			// Token: 0x0400143E RID: 5182
			public readonly Layers.Layer.Foliage[] foliages = new Layers.Layer.Foliage[4];

			// Token: 0x020002DA RID: 730
			public class Foliage
			{
				// Token: 0x0400143F RID: 5183
				public string foliageTexture = string.Empty;

				// Token: 0x04001440 RID: 5184
				public int probability;

				// Token: 0x04001441 RID: 5185
				public int numLeaves;

				// Token: 0x04001442 RID: 5186
				public float minScale;

				// Token: 0x04001443 RID: 5187
				public float maxScale;

				// Token: 0x04001444 RID: 5188
				public float topWidth;

				// Token: 0x04001445 RID: 5189
				public float topHeight;

				// Token: 0x04001446 RID: 5190
				public float topOffset;

				// Token: 0x04001447 RID: 5191
				public float bottomWidth;

				// Token: 0x04001448 RID: 5192
				public float bottomHeight;

				// Token: 0x04001449 RID: 5193
				public float bottomOffset;
			}
		}
	}
}
