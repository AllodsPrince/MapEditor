using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;
using Db;
using MapEditor.Map.MapObjects;
using Tools.SafeObjMan;
using Tools.WindowParams;

namespace MapEditor.Map.Dialogs
{
	// Token: 0x02000093 RID: 147
	public partial class AddFixedIdleAnimationTunerForm : Form
	{
		// Token: 0x060006EF RID: 1775 RVA: 0x000364B4 File Offset: 0x000354B4
		private string CutSpawnTunerFolder(string source)
		{
			if (source.StartsWith(this.spawnTunerFolder, StringComparison.OrdinalIgnoreCase))
			{
				return source.Substring(this.spawnTunerFolder.Length);
			}
			return source;
		}

		// Token: 0x060006F0 RID: 1776 RVA: 0x000364D8 File Offset: 0x000354D8
		private string CutVisualStateFolder(string source)
		{
			if (source.StartsWith(this.visualStateFolder, StringComparison.OrdinalIgnoreCase))
			{
				return source.Substring(this.visualStateFolder.Length);
			}
			return source;
		}

		// Token: 0x060006F1 RID: 1777 RVA: 0x000364FC File Offset: 0x000354FC
		private void FillAnimations()
		{
			if (this.mainDb != null && this.spawnPoint != null)
			{
				string character = SafeObjMan.GetDBID(this.mainDb.GetManipulator(this.mainDb.GetDBIDByName(this.spawnPoint.SceneName)), "character");
				if (!string.IsNullOrEmpty(character))
				{
					string visObject = SafeObjMan.GetDBID(this.mainDb.GetManipulator(this.mainDb.GetDBIDByName(character)), "characterVisObject");
					if (!string.IsNullOrEmpty(visObject))
					{
						IObjMan visObjectMan = this.mainDb.GetManipulator(this.mainDb.GetDBIDByName(visObject));
						if (visObjectMan != null)
						{
							int animationsCount = SafeObjMan.GetInt(visObjectMan, "states");
							for (int index = 0; index < animationsCount; index++)
							{
								DBID animationDBID = this.mainDb.GetDBIDByName(SafeObjMan.GetDBID(visObjectMan, "states.[" + index + "].animation"));
								if (!DBID.IsNullOrEmpty(animationDBID))
								{
									IObjMan animationMan = this.mainDb.GetManipulator(animationDBID);
									string animationName = SafeObjMan.GetString(animationMan, "scriptName");
									this.AnimationComboBox.Items.Add(animationName);
								}
							}
						}
					}
				}
			}
		}

		// Token: 0x060006F2 RID: 1778 RVA: 0x0003661C File Offset: 0x0003561C
		private void FillImpacts()
		{
			if (this.mainDb != null && this.spawnPoint != null)
			{
				this.impacts.Clear();
				this.impactIndex = -1;
				this.impactExisting = false;
				IObjMan spawnTunerMan = this.mainDb.GetManipulator(this.mainDb.GetDBIDByName(this.spawnTuner));
				if (spawnTunerMan != null)
				{
					int count = SafeObjMan.GetInt(spawnTunerMan, "impacts");
					for (int index = 0; index < count; index++)
					{
						string baseTypeName;
						string typeName;
						spawnTunerMan.IsStructPtr(string.Format("impacts.[{0}]", index), out baseTypeName, out typeName);
						bool equalsTovisualStateImpact = string.Equals(typeName, SpawnPoint.SetVisualStateImpactDBType);
						if (equalsTovisualStateImpact)
						{
							this.impactExisting = true;
						}
						if (this.impactIndex == -1 && !this.ForceCreateImpactCheckBox.Checked && equalsTovisualStateImpact)
						{
							this.impactIndex = index;
							this.impacts.Add(new AddFixedIdleAnimationTunerForm.ImpactInfo(typeName, AddFixedIdleAnimationTunerForm.Condition.Changed));
						}
						else
						{
							this.impacts.Add(new AddFixedIdleAnimationTunerForm.ImpactInfo(typeName, AddFixedIdleAnimationTunerForm.Condition.Existing));
						}
					}
				}
				if (this.impactIndex == -1)
				{
					this.impactIndex = this.impacts.Count;
					this.impacts.Add(new AddFixedIdleAnimationTunerForm.ImpactInfo(SpawnPoint.SetVisualStateImpactDBType, AddFixedIdleAnimationTunerForm.Condition.New));
				}
				this.ImpactListView.Items.Clear();
				for (int index2 = 0; index2 < this.impacts.Count; index2++)
				{
					ListViewItem listViewItem = new ListViewItem();
					listViewItem.Text = index2.ToString();
					listViewItem.Tag = index2;
					listViewItem.SubItems.Add(this.impacts[index2].Condition.ToString());
					listViewItem.SubItems.Add(this.impacts[index2].Name);
					this.ImpactListView.Items.Add(listViewItem);
				}
				this.ForceCreateImpactCheckBox.Enabled = this.impactExisting;
			}
		}

		// Token: 0x060006F3 RID: 1779 RVA: 0x00036808 File Offset: 0x00035808
		private void FillVisualStates()
		{
			if (this.mainDb != null && this.spawnPoint != null)
			{
				this.visualStates.Clear();
				this.visualStateIndex = -1;
				this.visualStateExisting = false;
				int uniqueStateID = 0;
				IObjMan visMobMan = this.mainDb.GetManipulator(this.mainDb.GetDBIDByName(this.spawnPoint.SceneName));
				if (visMobMan != null)
				{
					int count = SafeObjMan.GetInt(visMobMan, "visualStates");
					for (int index = 0; index < count; index++)
					{
						string idleAnimation = string.Empty;
						int stateID = 0;
						string path = SafeObjMan.GetDBID(visMobMan, string.Format("visualStates.[{0}]", index));
						bool equalsToSelectedAnimation = false;
						IObjMan visualStateMan = this.mainDb.GetManipulator(this.mainDb.GetDBIDByName(path));
						if (visualStateMan != null)
						{
							idleAnimation = SafeObjMan.GetString(visualStateMan, "fixedIdleAnimation");
							stateID = SafeObjMan.GetInt(visualStateMan, "stateID");
							if (uniqueStateID < stateID)
							{
								uniqueStateID = stateID;
							}
							equalsToSelectedAnimation = string.Equals(idleAnimation, this.AnimationComboBox.Text);
						}
						if (equalsToSelectedAnimation)
						{
							this.visualStateExisting = true;
						}
						if (this.visualStateIndex == -1 && !this.ForceCreateVisualStateCheckBox.Checked && equalsToSelectedAnimation)
						{
							this.visualStateIndex = index;
							this.visualStates.Add(new AddFixedIdleAnimationTunerForm.VisualStateInfo(idleAnimation, path, stateID, AddFixedIdleAnimationTunerForm.Condition.Located));
						}
						else
						{
							this.visualStates.Add(new AddFixedIdleAnimationTunerForm.VisualStateInfo(idleAnimation, path, stateID, AddFixedIdleAnimationTunerForm.Condition.Existing));
						}
					}
				}
				uniqueStateID++;
				if (this.visualStateIndex == -1)
				{
					int index2 = 0;
					while (index2 < this.visualStates.Count)
					{
						if (string.IsNullOrEmpty(this.visualStates[index2].Name))
						{
							this.visualStateExisting = true;
							if (!this.ForceCreateVisualStateCheckBox.Checked)
							{
								this.visualStateIndex = index2;
								this.visualStates[index2] = new AddFixedIdleAnimationTunerForm.VisualStateInfo(this.AnimationComboBox.Text, string.Empty, uniqueStateID, AddFixedIdleAnimationTunerForm.Condition.Changed);
								break;
							}
							break;
						}
						else
						{
							index2++;
						}
					}
				}
				if (this.visualStateIndex == -1)
				{
					this.visualStateIndex = this.visualStates.Count;
					this.visualStates.Add(new AddFixedIdleAnimationTunerForm.VisualStateInfo(this.AnimationComboBox.Text, string.Empty, uniqueStateID, AddFixedIdleAnimationTunerForm.Condition.New));
				}
				this.created = false;
				if (string.IsNullOrEmpty(this.visualStates[this.visualStateIndex].Name))
				{
					this.VisualStateNameTextBox.Text = this.CutVisualStateFolder(this.visualState);
				}
				else
				{
					this.VisualStateNameTextBox.Text = this.CutVisualStateFolder(this.visualStates[this.visualStateIndex].Name);
				}
				this.created = true;
				this.VisualStateListView.Items.Clear();
				for (int index3 = 0; index3 < this.visualStates.Count; index3++)
				{
					ListViewItem listViewItem = new ListViewItem();
					listViewItem.Text = index3.ToString();
					listViewItem.Tag = index3;
					listViewItem.SubItems.Add(this.visualStates[index3].Condition.ToString());
					listViewItem.SubItems.Add(this.visualStates[index3].Animation);
					listViewItem.SubItems.Add(this.visualStates[index3].StateID.ToString());
					if (index3 == this.visualStateIndex)
					{
						listViewItem.SubItems.Add(this.VisualStateNameTextBox.Text);
					}
					else
					{
						listViewItem.SubItems.Add(this.CutVisualStateFolder(this.visualStates[index3].Name));
					}
					this.VisualStateListView.Items.Add(listViewItem);
				}
				this.ForceCreateVisualStateCheckBox.Enabled = this.visualStateExisting;
				this.VisualStateNameTextBox.Enabled = string.IsNullOrEmpty(this.visualStates[this.visualStateIndex].Name);
			}
		}

		// Token: 0x060006F4 RID: 1780 RVA: 0x00036BE4 File Offset: 0x00035BE4
		private bool EnableCreation()
		{
			if (this.mainDb == null)
			{
				return false;
			}
			if (string.IsNullOrEmpty(this.AnimationComboBox.Text))
			{
				return false;
			}
			if (this.impactIndex < 0 || this.impactIndex >= this.impacts.Count)
			{
				return false;
			}
			if (this.visualStateIndex < 0 || this.visualStateIndex >= this.visualStates.Count)
			{
				return false;
			}
			if (this.SpawnTunerNameTextBox.Enabled)
			{
				DBID spawnTunerDBID = this.mainDb.GetDBIDByName(this.spawnTuner);
				if (string.IsNullOrEmpty(this.SpawnTunerNameTextBox.Text) || (!DBID.IsNullOrEmpty(spawnTunerDBID) && this.mainDb.DoesObjectExist(spawnTunerDBID)))
				{
					return false;
				}
			}
			if (this.VisualStateNameTextBox.Enabled)
			{
				DBID visualStateDBID = this.mainDb.GetDBIDByName(this.visualState);
				if (string.IsNullOrEmpty(this.VisualStateNameTextBox.Text) || (!DBID.IsNullOrEmpty(visualStateDBID) && this.mainDb.DoesObjectExist(visualStateDBID)))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x060006F5 RID: 1781 RVA: 0x00036CDE File Offset: 0x00035CDE
		private void UpdateOKButton()
		{
			this._okButton.Enabled = this.EnableCreation();
		}

		// Token: 0x060006F6 RID: 1782 RVA: 0x00036CF1 File Offset: 0x00035CF1
		private void FillListViews()
		{
			this.FillImpacts();
			this.FillVisualStates();
			this.UpdateOKButton();
		}

		// Token: 0x060006F7 RID: 1783 RVA: 0x00036D05 File Offset: 0x00035D05
		private void OnPostLoadParams(FormParams formParams)
		{
			this.created = true;
			this.FillListViews();
		}

		// Token: 0x060006F8 RID: 1784 RVA: 0x00036D14 File Offset: 0x00035D14
		private void InitialUpdateControls()
		{
			if (this.mainDb != null && this.spawnPoint != null)
			{
				this.VisMobTextBox.Text = this.spawnPoint.SceneName;
				this.FillAnimations();
				if (string.IsNullOrEmpty(this.spawnPoint.SpawnTuner))
				{
					string type = SpawnPoint.GetSpawnTunerType(this.spawnPoint);
					if (!string.IsNullOrEmpty(type))
					{
						this.spawnTuner = SpawnPoint.GetUniqueSpawnTunerName(this.continentName, type, this.mainDb);
						this.SpawnTunerNameTextBox.Enabled = true;
					}
				}
				else
				{
					this.spawnTuner = this.spawnPoint.SpawnTuner;
					this.SpawnTunerNameTextBox.Enabled = false;
				}
				this.SpawnTunerNameTextBox.Text = this.CutSpawnTunerFolder(this.spawnTuner);
			}
		}

		// Token: 0x060006F9 RID: 1785 RVA: 0x00036DD3 File Offset: 0x00035DD3
		private void AddFixedIdleAnimationTunerForm_FormClosing(object sender, FormClosingEventArgs e)
		{
			this.paramsSaver.PostLoadParams -= this.OnPostLoadParams;
		}

		// Token: 0x060006FA RID: 1786 RVA: 0x00036DEC File Offset: 0x00035DEC
		private void ForceCreateImpactCheckBox_CheckedChanged(object sender, EventArgs e)
		{
			if (this.created)
			{
				this.FillListViews();
			}
		}

		// Token: 0x060006FB RID: 1787 RVA: 0x00036DFC File Offset: 0x00035DFC
		private void ForceCreateVisualStateCheckBox_CheckedChanged(object sender, EventArgs e)
		{
			if (this.created)
			{
				this.FillListViews();
			}
		}

		// Token: 0x060006FC RID: 1788 RVA: 0x00036E0C File Offset: 0x00035E0C
		private void SpawnTunerNameTextBox_TextChanged(object sender, EventArgs e)
		{
			if (this.created)
			{
				this.spawnTuner = this.spawnTunerFolder + this.SpawnTunerNameTextBox.Text;
				this.UpdateOKButton();
			}
		}

		// Token: 0x060006FD RID: 1789 RVA: 0x00036E38 File Offset: 0x00035E38
		private void VisualStateNameTextBox_TextChanged(object sender, EventArgs e)
		{
			if (this.created)
			{
				this.visualState = this.visualStateFolder + this.VisualStateNameTextBox.Text;
				if (this.visualStateIndex >= 0 && this.visualStateIndex < this.VisualStateListView.Items.Count)
				{
					this.VisualStateListView.Items[this.visualStateIndex].SubItems[4].Text = this.VisualStateNameTextBox.Text;
				}
				this.UpdateOKButton();
			}
		}

		// Token: 0x060006FE RID: 1790 RVA: 0x00036EC1 File Offset: 0x00035EC1
		private void AnimationComboBox_TextChanged(object sender, EventArgs e)
		{
			if (this.created)
			{
				this.FillListViews();
			}
		}

		// Token: 0x060006FF RID: 1791 RVA: 0x00036ED4 File Offset: 0x00035ED4
		private void _okButton_Click(object sender, EventArgs e)
		{
			if (this.EnableCreation())
			{
				IObjMan spawnTunerMan;
				if (this.SpawnTunerNameTextBox.Enabled)
				{
					string type = SpawnPoint.GetSpawnTunerType(this.spawnPoint);
					SpawnPoint.CreateSpawnTuner(type, this.spawnTuner, this.mainDb, out spawnTunerMan);
				}
				else
				{
					spawnTunerMan = this.mainDb.GetManipulator(this.mainDb.GetDBIDByName(this.spawnTuner));
				}
				if (spawnTunerMan != null)
				{
					if (this.SpawnTunerNameTextBox.Enabled)
					{
						this.spawnPoint.SpawnTuner = this.spawnTuner;
					}
					if (this.impacts[this.impactIndex].Condition == AddFixedIdleAnimationTunerForm.Condition.New)
					{
						spawnTunerMan.Insert("impacts", this.impactIndex);
						spawnTunerMan.SetStructPtrInstance(string.Format("impacts.[{0}]", this.impactIndex), SpawnPoint.SetVisualStateImpactDBType);
					}
					SafeObjMan.SetInt(spawnTunerMan, string.Format("impacts.[{0}].visualState", this.impactIndex), this.visualStates[this.visualStateIndex].StateID);
					if (this.VisualStateNameTextBox.Enabled)
					{
						IObjMan visualStateMan;
						SpawnPoint.CreateVisualState(this.visualState, this.mainDb, out visualStateMan);
						if (visualStateMan != null)
						{
							SafeObjMan.SetInt(visualStateMan, "stateID", this.visualStates[this.visualStateIndex].StateID);
							SafeObjMan.SetString(visualStateMan, "fixedIdleAnimation", this.AnimationComboBox.Text);
							IObjMan visMobMan = this.mainDb.GetManipulator(this.mainDb.GetDBIDByName(this.spawnPoint.SceneName));
							if (visMobMan != null)
							{
								if (this.visualStates[this.visualStateIndex].Condition == AddFixedIdleAnimationTunerForm.Condition.New)
								{
									visMobMan.Insert("visualStates", this.visualStateIndex);
								}
								SafeObjMan.SetDBID(visMobMan, string.Format("visualStates.[{0}]", this.visualStateIndex), this.visualState);
							}
						}
					}
				}
			}
		}

		// Token: 0x06000700 RID: 1792 RVA: 0x000370AC File Offset: 0x000360AC
		public AddFixedIdleAnimationTunerForm(SpawnPoint _spawnPoint, string _continentName)
		{
			this.InitializeComponent();
			this.mainDb = IDatabase.GetMainDatabase();
			this.paramsSaver = new FormParamsSaver(this, EditorEnvironment.EditorFormsFolder + "AddFixedIdleAnimationTunerForm.xml", false);
			this.paramsSaver.AutoregisterControls = false;
			this.paramsSaver.RegisterControl(this.AnimationComboBox, true);
			this.paramsSaver.RegisterControl(this.MainSplitContainer);
			this.paramsSaver.RegisterControl(this.ImpactListView);
			this.paramsSaver.RegisterControl(this.VisualStateListView);
			this.paramsSaver.RegisterControl(this.ForceCreateImpactCheckBox);
			this.paramsSaver.RegisterControl(this.ForceCreateVisualStateCheckBox);
			this.paramsSaver.PostLoadParams += this.OnPostLoadParams;
			this.spawnPoint = _spawnPoint;
			this.continentName = _continentName;
			this.spawnTunerFolder = SpawnPoint.GetSpawnTunerFolder(this.continentName);
			this.visualStateFolder = SpawnPoint.GetVisualStateFolder(this.spawnPoint.SceneName);
			this.visualState = SpawnPoint.GetUniqueVisualStateName(this.spawnPoint.SceneName, this.mainDb);
			this.InitialUpdateControls();
		}

		// Token: 0x040004F7 RID: 1271
		private readonly IDatabase mainDb;

		// Token: 0x040004F8 RID: 1272
		private readonly FormParamsSaver paramsSaver;

		// Token: 0x040004F9 RID: 1273
		private readonly SpawnPoint spawnPoint;

		// Token: 0x040004FA RID: 1274
		private readonly string continentName = string.Empty;

		// Token: 0x040004FB RID: 1275
		private readonly string spawnTunerFolder = string.Empty;

		// Token: 0x040004FC RID: 1276
		private readonly string visualStateFolder = string.Empty;

		// Token: 0x040004FD RID: 1277
		private bool created;

		// Token: 0x040004FE RID: 1278
		private string spawnTuner = string.Empty;

		// Token: 0x040004FF RID: 1279
		private readonly List<AddFixedIdleAnimationTunerForm.ImpactInfo> impacts = new List<AddFixedIdleAnimationTunerForm.ImpactInfo>();

		// Token: 0x04000500 RID: 1280
		private int impactIndex = -1;

		// Token: 0x04000501 RID: 1281
		private bool impactExisting;

		// Token: 0x04000502 RID: 1282
		private readonly List<AddFixedIdleAnimationTunerForm.VisualStateInfo> visualStates = new List<AddFixedIdleAnimationTunerForm.VisualStateInfo>();

		// Token: 0x04000503 RID: 1283
		private int visualStateIndex = -1;

		// Token: 0x04000504 RID: 1284
		private bool visualStateExisting;

		// Token: 0x04000505 RID: 1285
		private string visualState = string.Empty;

		// Token: 0x02000094 RID: 148
		private enum Condition
		{
			// Token: 0x04000521 RID: 1313
			New,
			// Token: 0x04000522 RID: 1314
			Changed,
			// Token: 0x04000523 RID: 1315
			Located,
			// Token: 0x04000524 RID: 1316
			Existing
		}

		// Token: 0x02000095 RID: 149
		private class ImpactInfo
		{
			// Token: 0x06000703 RID: 1795 RVA: 0x00037A98 File Offset: 0x00036A98
			public ImpactInfo(string _name, AddFixedIdleAnimationTunerForm.Condition _condition)
			{
				this.name = _name;
				this.condition = _condition;
			}

			// Token: 0x17000115 RID: 277
			// (get) Token: 0x06000704 RID: 1796 RVA: 0x00037AC0 File Offset: 0x00036AC0
			public string Name
			{
				get
				{
					return this.name;
				}
			}

			// Token: 0x17000116 RID: 278
			// (get) Token: 0x06000705 RID: 1797 RVA: 0x00037AC8 File Offset: 0x00036AC8
			public AddFixedIdleAnimationTunerForm.Condition Condition
			{
				get
				{
					return this.condition;
				}
			}

			// Token: 0x04000525 RID: 1317
			private readonly string name = string.Empty;

			// Token: 0x04000526 RID: 1318
			private readonly AddFixedIdleAnimationTunerForm.Condition condition = AddFixedIdleAnimationTunerForm.Condition.Existing;
		}

		// Token: 0x02000096 RID: 150
		private class VisualStateInfo
		{
			// Token: 0x06000706 RID: 1798 RVA: 0x00037AD0 File Offset: 0x00036AD0
			public VisualStateInfo(string _animation, string _name, int _stateID, AddFixedIdleAnimationTunerForm.Condition _condition)
			{
				this.animation = _animation;
				this.name = _name;
				this.stateID = _stateID;
				this.condition = _condition;
			}

			// Token: 0x17000117 RID: 279
			// (get) Token: 0x06000707 RID: 1799 RVA: 0x00037B1D File Offset: 0x00036B1D
			public string Animation
			{
				get
				{
					return this.animation;
				}
			}

			// Token: 0x17000118 RID: 280
			// (get) Token: 0x06000708 RID: 1800 RVA: 0x00037B25 File Offset: 0x00036B25
			public string Name
			{
				get
				{
					return this.name;
				}
			}

			// Token: 0x17000119 RID: 281
			// (get) Token: 0x06000709 RID: 1801 RVA: 0x00037B2D File Offset: 0x00036B2D
			public int StateID
			{
				get
				{
					return this.stateID;
				}
			}

			// Token: 0x1700011A RID: 282
			// (get) Token: 0x0600070A RID: 1802 RVA: 0x00037B35 File Offset: 0x00036B35
			public AddFixedIdleAnimationTunerForm.Condition Condition
			{
				get
				{
					return this.condition;
				}
			}

			// Token: 0x04000527 RID: 1319
			private readonly string animation = string.Empty;

			// Token: 0x04000528 RID: 1320
			private readonly string name = string.Empty;

			// Token: 0x04000529 RID: 1321
			private readonly int stateID;

			// Token: 0x0400052A RID: 1322
			private readonly AddFixedIdleAnimationTunerForm.Condition condition = AddFixedIdleAnimationTunerForm.Condition.Existing;
		}
	}
}
