using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using Db;
using MapEditor.Forms.Base;
using MapEditor.Resources.Strings;
using Tools.DBGameObjects;
using Tools.DBGameObjects.GameObjects;
using Tools.Diagram;
using Tools.WindowParams;

namespace MapEditor.Forms.Quests
{
	// Token: 0x02000268 RID: 616
	public partial class QuestDiagramForm : BaseForm
	{
		// Token: 0x06001D17 RID: 7447 RVA: 0x000B98EA File Offset: 0x000B88EA
		private static string GetConfigFileNameFile(string zone)
		{
			return QuestDiagramForm.configFoldereName + zone + ".xml";
		}

		// Token: 0x06001D18 RID: 7448 RVA: 0x000B98FC File Offset: 0x000B88FC
		private void LoadConfiguration(string zone)
		{
			if (string.IsNullOrEmpty(zone))
			{
				this.zoneConfig = null;
				return;
			}
			string configFileName = QuestDiagramForm.GetConfigFileNameFile(zone);
			this.zoneConfig = (Serializer.Load<QuestDiagramForm.QuestDiagramConfig>(configFileName) ?? new QuestDiagramForm.QuestDiagramConfig());
		}

		// Token: 0x06001D19 RID: 7449 RVA: 0x000B9938 File Offset: 0x000B8938
		private void SaveConfiguration()
		{
			if (this.zoneConfig != null && this.configChanged)
			{
				this.zoneConfig.Quests.Clear();
				string filePath = null;
				foreach (DiagramObject diagramObject in this.QuestDiagram.DiagramObjects)
				{
					if (string.IsNullOrEmpty(filePath))
					{
						string zone = QuestClass.GetZoneFolder(diagramObject.Key);
						if (!string.IsNullOrEmpty(zone))
						{
							filePath = QuestDiagramForm.GetConfigFileNameFile(zone);
						}
					}
					QuestDiagramForm.QuestDiagramConfig.ListElem elem = new QuestDiagramForm.QuestDiagramConfig.ListElem();
					elem.Key = diagramObject.Key;
					elem.Location = diagramObject.Location;
					this.zoneConfig.Quests.Add(elem);
				}
				if (!string.IsNullOrEmpty(filePath))
				{
					Serializer.Save(filePath, this.zoneConfig, true);
				}
			}
		}

		// Token: 0x06001D1A RID: 7450 RVA: 0x000B9A18 File Offset: 0x000B8A18
		private void OnVisibleChanged(object sender, EventArgs e)
		{
			if (base.Visible)
			{
				if (this.Changed)
				{
					this.LoadDiagram();
					return;
				}
			}
			else if (!base.Context.FormClosing)
			{
				this.SaveConfiguration();
				this.PrompToSave(this);
			}
		}

		// Token: 0x06001D1B RID: 7451 RVA: 0x000B9A4B File Offset: 0x000B8A4B
		private void OnMainFormClosingEvent(FormClosingEventArgs e)
		{
			if (base.Visible)
			{
				this.SaveConfiguration();
				this.PrompToSave(this);
			}
		}

		// Token: 0x06001D1C RID: 7452 RVA: 0x000B9A62 File Offset: 0x000B8A62
		private void BeforeLoadParams(FormParams formParams)
		{
			this.LoadZones();
			this.snapCheckBox.DataBindings.Add("Checked", this.QuestDiagram, "Snap", true, DataSourceUpdateMode.OnPropertyChanged);
		}

		// Token: 0x06001D1D RID: 7453 RVA: 0x000B9A8D File Offset: 0x000B8A8D
		private void OnPostLoadParams(FormParams formParams)
		{
			this.loaded = true;
		}

		// Token: 0x06001D1E RID: 7454 RVA: 0x000B9A98 File Offset: 0x000B8A98
		private void OnSelectedZoneChanged(object sender, EventArgs e)
		{
			this.PrompToSave(this);
			this.Cursor = Cursors.WaitCursor;
			this.SaveConfiguration();
			this.LoadConfiguration(this.ZoneComboBox.Text);
			base.Context.QuestEnvironment.Load(this.ZoneComboBox.Text);
			this.LoadDiagram();
			this.Cursor = Cursors.Default;
		}

		// Token: 0x06001D1F RID: 7455 RVA: 0x000B9AFA File Offset: 0x000B8AFA
		private void OnSave(object sender, EventArgs e)
		{
			this.Cursor = Cursors.WaitCursor;
			this.Save();
			this.Cursor = Cursors.Default;
		}

		// Token: 0x06001D20 RID: 7456 RVA: 0x000B9B18 File Offset: 0x000B8B18
		private void OnQuestChanged(object sender, IEnumerable<string> changedQuests)
		{
			if (sender == this)
			{
				return;
			}
			bool needReload = false;
			foreach (string questDBID in changedQuests)
			{
				QuestClass quest = base.Context.QuestEnvironment.Quests.GetObjectByDBID(questDBID) as QuestClass;
				if (quest != null && quest.GetZoneFolder() == this.ZoneComboBox.Text)
				{
					needReload = true;
					break;
				}
			}
			if (needReload)
			{
				this.ReloadDiagram();
			}
		}

		// Token: 0x06001D21 RID: 7457 RVA: 0x000B9BA4 File Offset: 0x000B8BA4
		private void OnObjectDeleting(object sender, GameObjectClass deletingObject)
		{
			if (deletingObject.GetTypeName() == "gameMechanics.constructor.schemes.quest.QuestResource" && deletingObject.GameObject.Contains(this.ZoneComboBox.Text))
			{
				this.ReloadDiagram();
			}
		}

		// Token: 0x06001D22 RID: 7458 RVA: 0x000B9BD8 File Offset: 0x000B8BD8
		private void OnDiagramMemberAddedOrRemoved(DiagramMember diagramMember)
		{
			DiagramEdge diagramEdge = diagramMember as DiagramEdge;
			if (diagramEdge != null)
			{
				this.Changed = true;
			}
		}

		// Token: 0x06001D23 RID: 7459 RVA: 0x000B9BF6 File Offset: 0x000B8BF6
		private void OnDiagramDoubleClick(object sender, MouseEventArgs e)
		{
			this.LoadQuestToQuestEditor(this.QuestDiagram.GetDiagramObject(e.Location));
		}

		// Token: 0x06001D24 RID: 7460 RVA: 0x000B9C10 File Offset: 0x000B8C10
		private void OnDiagramKeyDown(object sender, KeyEventArgs e)
		{
			Keys keyCode = e.KeyCode;
			if (keyCode != Keys.Return)
			{
				if (keyCode != Keys.B)
				{
					if (keyCode != Keys.S)
					{
						return;
					}
					List<DiagramObject> singles = new List<DiagramObject>();
					foreach (DiagramObject diagramObj in this.QuestDiagram.DiagramObjects)
					{
						if (diagramObj.OutEdges.Count == 0)
						{
							QuestClass quest = base.Context.QuestEnvironment.Quests.GetObjectByDBID(diagramObj.Key) as QuestClass;
							if (quest != null)
							{
								List<string> prevQuests;
								List<string> notStartedQuest;
								string characterClass;
								quest.GetPrerequisits(out prevQuests, out notStartedQuest, out characterClass);
								if (prevQuests == null || prevQuests.Count == 0)
								{
									singles.Add(diagramObj);
								}
							}
						}
					}
					this.QuestDiagram.BeginRepaintTransaction();
					this.QuestDiagram.ClearGluedObject();
					this.QuestDiagram.SelectedDiagramMember = null;
					foreach (DiagramObject diagramObj2 in singles)
					{
						if (this.QuestDiagram.SelectedDiagramMember == null)
						{
							this.QuestDiagram.SelectedDiagramMember = diagramObj2;
						}
						this.QuestDiagram.AddGluedObject(diagramObj2);
					}
					this.QuestDiagram.EndRepaintTransaction();
				}
				else
				{
					DiagramObject selectedObject = this.QuestDiagram.SelectedDiagramMember as DiagramObject;
					if (selectedObject != null)
					{
						List<DiagramObject> brunch;
						this.QuestDiagram.FindBrunch(selectedObject, out brunch);
						this.QuestDiagram.BeginRepaintTransaction();
						this.QuestDiagram.ClearGluedObject();
						foreach (DiagramObject dObj in brunch)
						{
							this.QuestDiagram.AddGluedObject(dObj);
						}
						this.QuestDiagram.EndRepaintTransaction();
						return;
					}
				}
				return;
			}
			this.LoadQuestToQuestEditor(this.QuestDiagram.SelectedDiagramMember as DiagramObject);
		}

		// Token: 0x06001D25 RID: 7461 RVA: 0x000B9E0C File Offset: 0x000B8E0C
		private void OnQuestMoved(DiagramMember diagramMember)
		{
			this.configChanged = (this.zoneConfig != null);
		}

		// Token: 0x06001D26 RID: 7462 RVA: 0x000B9E20 File Offset: 0x000B8E20
		private void OnCloseButtonClick(object sender, EventArgs e)
		{
			base.Close();
		}

		// Token: 0x06001D27 RID: 7463 RVA: 0x000B9E28 File Offset: 0x000B8E28
		private void OnFilterCheckBoxChecked(object sender, EventArgs e)
		{
			this.ReloadDiagram();
		}

		// Token: 0x06001D28 RID: 7464 RVA: 0x000B9E30 File Offset: 0x000B8E30
		private void LoadQuestToQuestEditor(DiagramObject diagramObject)
		{
			if (diagramObject != null)
			{
				QuestClass quest = base.Context.QuestEnvironment.Quests.GetObjectByDBID(diagramObject.Key) as QuestClass;
				if (quest != null)
				{
					base.Context.QuestEnvironment.QuestEditor.Load(quest);
				}
			}
		}

		// Token: 0x06001D29 RID: 7465 RVA: 0x000B9E7A File Offset: 0x000B8E7A
		private void LoadZones()
		{
			QuestEnvironment.LoadZones(this.ZoneComboBox, false);
		}

		// Token: 0x06001D2A RID: 7466 RVA: 0x000B9E88 File Offset: 0x000B8E88
		private void LoadDiagram()
		{
			this.configChanged = false;
			this.QuestDiagram.RectangularDiagramMemberMoved -= this.OnQuestMoved;
			this.QuestDiagram.Clear();
			if (!string.IsNullOrEmpty(this.ZoneComboBox.Text))
			{
				this.QuestDiagram.BeginRepaintTransaction();
				FilteredView quests = new FilteredView(base.Context.QuestEnvironment.Quests);
				quests.AddFilter(new DBIDFilter(this.ZoneComboBox.Text));
				Dictionary<string, QuestClass> filteredQuets;
				this.FilterQuests(quests, out filteredQuets);
				this.LoadQuests(filteredQuets.Values);
				this.LoadLinks(filteredQuets.Values);
				if (this.QuestDiagram.Snap)
				{
					this.QuestDiagram.SnapDiagramMemberPositions();
				}
				this.QuestDiagram.EndRepaintTransaction();
			}
			this.QuestDiagram.RectangularDiagramMemberMoved += this.OnQuestMoved;
			this.Changed = false;
		}

		// Token: 0x06001D2B RID: 7467 RVA: 0x000B9F6C File Offset: 0x000B8F6C
		private void Save()
		{
			IDatabase mainDb = IDatabase.GetMainDatabase();
			if (mainDb == null)
			{
				return;
			}
			Dictionary<string, List<string>> prevQuestHash = this.CreatePrevQuestHash();
			foreach (KeyValuePair<string, List<string>> pair in prevQuestHash)
			{
				QuestClass quest = base.Context.QuestEnvironment.Quests.GetObjectByDBID(pair.Key) as QuestClass;
				if (quest != null)
				{
					quest.SetFinishedQuestPrereq(pair.Value);
				}
			}
			base.Context.QuestEnvironment.InvokeQuestChanged(this, new List<string>(prevQuestHash.Keys));
			mainDb.SaveChanges();
			this.Changed = false;
		}

		// Token: 0x06001D2C RID: 7468 RVA: 0x000BA024 File Offset: 0x000B9024
		private Dictionary<string, List<string>> CreatePrevQuestHash()
		{
			Dictionary<string, List<string>> prevQuestHash = new Dictionary<string, List<string>>();
			foreach (DiagramObject diagramObject in this.QuestDiagram.DiagramObjects)
			{
				prevQuestHash.Add(diagramObject.Key, new List<string>(1));
			}
			foreach (DiagramObject diagramObject2 in this.QuestDiagram.DiagramObjects)
			{
				foreach (DiagramEdge outEdge in diagramObject2.OutEdges)
				{
					DiagramObject endDiagramObject = outEdge.EndDiagramObject;
					prevQuestHash[endDiagramObject.Key].Add(diagramObject2.Key);
				}
			}
			return prevQuestHash;
		}

		// Token: 0x06001D2D RID: 7469 RVA: 0x000BA128 File Offset: 0x000B9128
		private void ReloadDiagram()
		{
			if (!this.loaded)
			{
				return;
			}
			this.SaveConfiguration();
			if (!base.Visible)
			{
				this.LoadDiagram();
				return;
			}
			Cursor.Current = Cursors.WaitCursor;
			this.LoadDiagram();
			Cursor.Current = Cursors.Default;
		}

		// Token: 0x06001D2E RID: 7470 RVA: 0x000BA164 File Offset: 0x000B9164
		private void PrompToSave(IWin32Window dialogOwner)
		{
			if (this.Changed)
			{
				DialogResult result = MessageBox.Show(dialogOwner, Strings.QUEST_EDITOR_SAVE_PROMP_MESSAGE, this.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);
				if (result == DialogResult.Yes)
				{
					Cursor.Current = Cursors.WaitCursor;
					this.Save();
					Cursor.Current = Cursors.Default;
				}
			}
		}

		// Token: 0x06001D2F RID: 7471 RVA: 0x000BA1AC File Offset: 0x000B91AC
		private void LoadQuest(QuestClass quest, Point location)
		{
			int cnt = string.IsNullOrEmpty(quest.PlotLine) ? 1 : 2;
			string[] text = new string[cnt];
			text[0] = string.Format("file: {0}", quest.Name);
			Color color = quest.IsDisabled() ? this.disabledQuestColorLabel.BackColor : (string.IsNullOrEmpty(quest.PlotLine) ? Color.White : this.plotLineColorLabel.BackColor);
			if (!string.IsNullOrEmpty(quest.PlotLine))
			{
				text[1] = string.Format("plot line: {0}", quest.PlotLine);
			}
			string caption = string.Concat(new object[]
			{
				quest.GameName,
				" (",
				quest.Level,
				')'
			});
			this.QuestDiagram.AddDiagramObject(quest.GameObject, caption, text, location, QuestDiagramForm.defaultSize, color, null);
		}

		// Token: 0x06001D30 RID: 7472 RVA: 0x000BA294 File Offset: 0x000B9294
		private void LoadQuests(IEnumerable<QuestClass> zoneQuests)
		{
			if (this.zoneConfig != null)
			{
				List<QuestClass> newQuests = new List<QuestClass>();
				int bottom = 0;
				foreach (QuestClass quest in zoneQuests)
				{
					QuestDiagramForm.QuestDiagramConfig.ListElem elem = this.zoneConfig.Find(quest.GameObject);
					if (elem != null)
					{
						Point configLocation = elem.Location;
						if (base.WindowState != FormWindowState.Minimized && (configLocation.X > this.QuestDiagram.ClientSize.Width - 1 || configLocation.Y > this.QuestDiagram.ClientSize.Height - 1))
						{
							configLocation = QuestDiagramForm.defaultObjectLocation;
						}
						this.LoadQuest(quest, configLocation);
						DiagramObject newObject;
						if (this.QuestDiagram.GetDiagramObject(quest.GameObject, out newObject))
						{
							int _bottom = configLocation.Y + newObject.Size.Height;
							if (_bottom > bottom)
							{
								bottom = _bottom;
							}
						}
					}
					else
					{
						newQuests.Add(quest);
					}
				}
				Point autoLocation = new Point(0, bottom + QuestDiagramForm.defaultSeparator.X);
				foreach (QuestClass newQuest in newQuests)
				{
					this.LoadQuest(newQuest, autoLocation);
					DiagramObject newObject2;
					if (this.QuestDiagram.GetDiagramObject(newQuest.GameObject, out newObject2))
					{
						autoLocation.X += newObject2.Size.Width + QuestDiagramForm.defaultSeparator.X;
						if (autoLocation.X + newObject2.Size.Width > this.QuestDiagram.Width)
						{
							autoLocation.X = 0;
							autoLocation.Y += QuestDiagramForm.defaultSeparator.Y;
						}
					}
				}
			}
		}

		// Token: 0x06001D31 RID: 7473 RVA: 0x000BA498 File Offset: 0x000B9498
		private void LoadLinks(IEnumerable<QuestClass> zoneQuests)
		{
			foreach (QuestClass quest in zoneQuests)
			{
				List<string> prevQuests;
				List<string> notStartedQuest;
				string characterClass;
				bool otherConditions;
				quest.GetPrerequisits(out prevQuests, out notStartedQuest, out characterClass, out otherConditions);
				if (prevQuests != null)
				{
					foreach (string prevQuest in prevQuests)
					{
						DiagramObject beginDiagramObject;
						DiagramObject endDiagramObject;
						if (this.QuestDiagram.GetDiagramObject(prevQuest, out beginDiagramObject) && this.QuestDiagram.GetDiagramObject(quest.GameObject, out endDiagramObject))
						{
							if (notStartedQuest.Count == 0 && string.IsNullOrEmpty(characterClass) && !otherConditions)
							{
								this.QuestDiagram.AddDiagramEdge(beginDiagramObject, endDiagramObject);
							}
							else
							{
								this.QuestDiagram.AddDiagramEdge(beginDiagramObject, endDiagramObject, this.complexConditionPen);
							}
						}
					}
				}
			}
		}

		// Token: 0x06001D32 RID: 7474 RVA: 0x000BA590 File Offset: 0x000B9590
		private void LoadQuest(QuestClass quest)
		{
			if (quest != null)
			{
				string zone = quest.GetZoneFolder();
				if (!string.IsNullOrEmpty(zone))
				{
					if (!base.Visible)
					{
						base.Context.ShowQuestDiagram(true);
					}
					int index = this.ZoneComboBox.Items.IndexOf(zone);
					if (index > -1)
					{
						this.ZoneComboBox.SelectedIndex = index;
						foreach (DiagramObject diagramObject in this.QuestDiagram.DiagramObjects)
						{
							if (diagramObject.Key == quest.GameObject)
							{
								this.QuestDiagram.SelectedDiagramMember = diagramObject;
								break;
							}
						}
					}
				}
			}
		}

		// Token: 0x06001D33 RID: 7475 RVA: 0x000BA64C File Offset: 0x000B964C
		private void FilterQuests(Tools.DBGameObjects.View quests, out Dictionary<string, QuestClass> filteredQuestDict)
		{
			filteredQuestDict = new Dictionary<string, QuestClass>();
			foreach (GameObjectClass gameObjectClass in quests)
			{
				QuestClass quest = (QuestClass)gameObjectClass;
				filteredQuestDict.Add(quest.GameObject, quest);
			}
			if (!this.singleQuestsCheckBox.Checked || !this.questLinesCheckBox.Checked)
			{
				Dictionary<string, QuestClass> startQuests = new Dictionary<string, QuestClass>();
				Dictionary<string, QuestClass> endQuests = new Dictionary<string, QuestClass>();
				foreach (QuestClass quest2 in filteredQuestDict.Values)
				{
					List<string> prevQuests;
					List<string> notStartedQuest;
					string characterClass;
					quest2.GetPrerequisits(out prevQuests, out notStartedQuest, out characterClass);
					if (prevQuests.Count > 0)
					{
						startQuests.Add(quest2.GameObject, quest2);
						foreach (string prevQuest in prevQuests)
						{
							if (!endQuests.ContainsKey(prevQuest))
							{
								endQuests.Add(prevQuest, (QuestClass)quests.GetObjectByDBID(prevQuest));
							}
						}
					}
				}
				if (!this.singleQuestsCheckBox.Checked)
				{
					List<QuestClass> _quest = new List<QuestClass>(filteredQuestDict.Values);
					foreach (QuestClass quest3 in _quest)
					{
						if (!startQuests.ContainsKey(quest3.GameObject) && !endQuests.ContainsKey(quest3.GameObject))
						{
							filteredQuestDict.Remove(quest3.GameObject);
						}
					}
				}
				if (!this.questLinesCheckBox.Checked)
				{
					foreach (string key in startQuests.Keys)
					{
						filteredQuestDict.Remove(key);
					}
					foreach (string key2 in endQuests.Keys)
					{
						filteredQuestDict.Remove(key2);
					}
				}
			}
			if (!this.pvpQuestsCheckBox.Checked)
			{
				List<QuestClass> _quest2 = new List<QuestClass>(filteredQuestDict.Values);
				foreach (QuestClass quest4 in _quest2)
				{
					if (quest4.PVP)
					{
						filteredQuestDict.Remove(quest4.GameObject);
					}
				}
			}
		}

		// Token: 0x170006E6 RID: 1766
		// (get) Token: 0x06001D34 RID: 7476 RVA: 0x000BA918 File Offset: 0x000B9918
		// (set) Token: 0x06001D35 RID: 7477 RVA: 0x000BA920 File Offset: 0x000B9920
		private bool Changed
		{
			get
			{
				return this.changed;
			}
			set
			{
				this.changed = value;
				this.SaveButton.Enabled = this.changed;
			}
		}

		// Token: 0x06001D36 RID: 7478 RVA: 0x000BA93C File Offset: 0x000B993C
		public QuestDiagramForm(MainForm.Context context) : base(EditorEnvironment.EditorFormsFolder + "QuestDiagramForm.xml", context)
		{
			this.InitializeComponent();
			Pen commonPen = new Pen(Color.Black, 1f);
			Pen selectedPen = new Pen(Color.Blue, 3f);
			this.complexConditionPen = new Pen(this.comlexComditionColorLabel.BackColor, 2f);
			this.QuestDiagram.SetPen(DiagramMemberTypes.OBJECT, commonPen, selectedPen);
			this.QuestDiagram.SetPen(DiagramMemberTypes.EDGE, commonPen, selectedPen);
			base.ParamsSaver.AutoregisterControls = false;
			base.ParamsSaver.PostLoadParams += this.OnPostLoadParams;
			base.ParamsSaver.LoadParams += this.BeforeLoadParams;
			base.ParamsSaver.RegisterControl(this.snapCheckBox);
			base.VisibleChanged += this.OnVisibleChanged;
			this.SaveButton.Click += this.OnSave;
			this.CloseButton.Click += this.OnCloseButtonClick;
			this.ZoneComboBox.SelectedIndexChanged += this.OnSelectedZoneChanged;
			this.QuestDiagram.DiagramMemberRemoved += this.OnDiagramMemberAddedOrRemoved;
			this.QuestDiagram.DiagramMemberAdded += this.OnDiagramMemberAddedOrRemoved;
			this.QuestDiagram.MouseDoubleClick += this.OnDiagramDoubleClick;
			this.QuestDiagram.KeyDown += this.OnDiagramKeyDown;
			base.Context.QuestEnvironment.QuestChanged += this.OnQuestChanged;
			base.Context.QuestEnvironment.ObjectDeleting += this.OnObjectDeleting;
			base.Context.QuestEnvironment.QuestEditor.LoadQuestToDiagram += this.LoadQuest;
			base.Context.AddCloseFormEvent(new MainForm.Context.CloseFormEvent(this.OnMainFormClosingEvent));
			this.singleQuestsCheckBox.CheckedChanged += this.OnFilterCheckBoxChecked;
			this.questLinesCheckBox.CheckedChanged += this.OnFilterCheckBoxChecked;
			this.pvpQuestsCheckBox.CheckedChanged += this.OnFilterCheckBoxChecked;
		}

		// Token: 0x04001280 RID: 4736
		private QuestDiagramForm.QuestDiagramConfig zoneConfig;

		// Token: 0x04001281 RID: 4737
		private static readonly string configFoldereName = EditorEnvironment.EditorFolder + "Quests/QuestDiagram/";

		// Token: 0x04001282 RID: 4738
		private static readonly Point defaultObjectLocation = new Point(0, 0);

		// Token: 0x04001283 RID: 4739
		private static readonly Size defaultSize = new Size(0, 0);

		// Token: 0x04001284 RID: 4740
		private static readonly Point defaultSeparator = new Point(5, 50);

		// Token: 0x04001285 RID: 4741
		private bool changed;

		// Token: 0x04001286 RID: 4742
		private bool loaded;

		// Token: 0x04001287 RID: 4743
		private bool configChanged;

		// Token: 0x04001288 RID: 4744
		private readonly Pen complexConditionPen;

		// Token: 0x02000269 RID: 617
		public class QuestDiagramConfig
		{
			// Token: 0x170006E7 RID: 1767
			// (get) Token: 0x06001D3A RID: 7482 RVA: 0x000BB292 File Offset: 0x000BA292
			// (set) Token: 0x06001D3B RID: 7483 RVA: 0x000BB29A File Offset: 0x000BA29A
			public List<QuestDiagramForm.QuestDiagramConfig.ListElem> Quests
			{
				get
				{
					return this.quests;
				}
				set
				{
					this.quests = value;
				}
			}

			// Token: 0x06001D3C RID: 7484 RVA: 0x000BB2A4 File Offset: 0x000BA2A4
			public QuestDiagramForm.QuestDiagramConfig.ListElem Find(string key)
			{
				foreach (QuestDiagramForm.QuestDiagramConfig.ListElem elem in this.quests)
				{
					if (elem.Key == key)
					{
						return elem;
					}
				}
				return null;
			}

			// Token: 0x0400129A RID: 4762
			private List<QuestDiagramForm.QuestDiagramConfig.ListElem> quests = new List<QuestDiagramForm.QuestDiagramConfig.ListElem>();

			// Token: 0x0200026A RID: 618
			public class ListElem
			{
				// Token: 0x170006E8 RID: 1768
				// (get) Token: 0x06001D3E RID: 7486 RVA: 0x000BB31B File Offset: 0x000BA31B
				// (set) Token: 0x06001D3F RID: 7487 RVA: 0x000BB323 File Offset: 0x000BA323
				public string Key
				{
					get
					{
						return this.key;
					}
					set
					{
						this.key = value;
					}
				}

				// Token: 0x170006E9 RID: 1769
				// (get) Token: 0x06001D40 RID: 7488 RVA: 0x000BB32C File Offset: 0x000BA32C
				// (set) Token: 0x06001D41 RID: 7489 RVA: 0x000BB334 File Offset: 0x000BA334
				public Point Location
				{
					get
					{
						return this.location;
					}
					set
					{
						this.location = value;
					}
				}

				// Token: 0x0400129B RID: 4763
				private string key;

				// Token: 0x0400129C RID: 4764
				private Point location;
			}
		}
	}
}
