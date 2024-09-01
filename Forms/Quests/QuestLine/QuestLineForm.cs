using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using MapEditor.Resources.Strings;
using Tools.DBGameObjects;
using Tools.DBGameObjects.GameObjects;
using Tools.Diagram;
using Tools.WindowParams;

namespace MapEditor.Forms.Quests.QuestLine
{
	// Token: 0x020001B6 RID: 438
	public partial class QuestLineForm : Form
	{
		// Token: 0x0600153B RID: 5435 RVA: 0x0009A05C File Offset: 0x0009905C
		private void FindAncestors(QuestClass quest, Dictionary<string, QuestClass> _questLine)
		{
			if (quest != null)
			{
				List<string> prevQuests;
				List<string> notStartedQuest;
				string characterClass;
				quest.GetPrerequisits(out prevQuests, out notStartedQuest, out characterClass);
				if (prevQuests != null)
				{
					foreach (string prevQuestKey in prevQuests)
					{
						if (!_questLine.ContainsKey(prevQuestKey))
						{
							QuestClass prevQuest = (QuestClass)this.questEnvironment.Quests.GetObjectByDBID(prevQuestKey);
							_questLine.Add(prevQuestKey, prevQuest);
							this.FindAncestors(prevQuest, _questLine);
						}
					}
				}
			}
		}

		// Token: 0x0600153C RID: 5436 RVA: 0x0009A0EC File Offset: 0x000990EC
		private void LoadQuestLineData(QuestClass selectedQuest)
		{
			this.questLine.Clear();
			this.questLine.Add(selectedQuest.GameObject, selectedQuest);
			this.FindAncestors(selectedQuest, this.questLine);
			Dictionary<string, QuestClass> _questLine = new Dictionary<string, QuestClass>();
			foreach (GameObjectClass gameObjectClass in this.questEnvironment.Quests)
			{
				QuestClass quest = (QuestClass)gameObjectClass;
				if (!this.questLine.ContainsKey(quest.GameObject))
				{
					bool inBrunch = false;
					_questLine.Clear();
					this.FindAncestors(quest, _questLine);
					foreach (string _key in _questLine.Keys)
					{
						if (this.questLine.ContainsKey(_key))
						{
							inBrunch = true;
							break;
						}
					}
					if (inBrunch)
					{
						this.questLine.Add(quest.GameObject, quest);
						foreach (KeyValuePair<string, QuestClass> pair in _questLine)
						{
							if (!this.questLine.ContainsKey(pair.Key))
							{
								this.questLine.Add(pair.Key, pair.Value);
							}
						}
					}
				}
			}
		}

		// Token: 0x0600153D RID: 5437 RVA: 0x0009A268 File Offset: 0x00099268
		private static bool LocateNextLevel(IList<List<DiagramObject>> diagramLevels, IDictionary<string, DiagramObject> configureatedDiagramObjects)
		{
			bool result = false;
			List<DiagramObject> lastLevel = diagramLevels[diagramLevels.Count - 1];
			if (lastLevel != null && lastLevel.Count > 0)
			{
				List<DiagramObject> newLevel = null;
				int nextObjX = 0;
				int nextObjY = lastLevel[0].Location.Y + 60;
				foreach (DiagramObject lastLevelObj in lastLevel)
				{
					if (lastLevelObj.OutEdges.Count > 0)
					{
						bool firstChild = true;
						foreach (DiagramEdge dEdge in lastLevelObj.OutEdges)
						{
							if (!configureatedDiagramObjects.ContainsKey(dEdge.EndDiagramObject.Key))
							{
								if (newLevel == null)
								{
									result = true;
									newLevel = new List<DiagramObject>();
									diagramLevels.Add(newLevel);
								}
								newLevel.Add(dEdge.EndDiagramObject);
								dEdge.EndDiagramObject.Location = new Point(nextObjX, nextObjY);
								configureatedDiagramObjects.Add(dEdge.EndDiagramObject.Key, dEdge.EndDiagramObject);
								if (!firstChild)
								{
									for (int levelIndex = 0; levelIndex < diagramLevels.Count - 1; levelIndex++)
									{
										List<DiagramObject> level = diagramLevels[levelIndex];
										for (int index = 0; index < level.Count; index++)
										{
											DiagramObject dObj = level[index];
											if (dObj.Location.X >= nextObjX)
											{
												dObj.Location = new Point(dObj.Location.X + 30, dObj.Location.Y);
											}
										}
									}
								}
								nextObjX += dEdge.EndDiagramObject.Size.Width + 30;
								firstChild = false;
							}
						}
					}
				}
			}
			return result;
		}

		// Token: 0x0600153E RID: 5438 RVA: 0x0009A474 File Offset: 0x00099474
		private void ConfigureDiagramObjects()
		{
			List<List<DiagramObject>> diagramLevels = new List<List<DiagramObject>>();
			Dictionary<string, DiagramObject> configureatedDiagramObjects = new Dictionary<string, DiagramObject>();
			diagramLevels.Add(new List<DiagramObject>());
			int posX = 0;
			foreach (DiagramObject dObj in this.QuestDiagram.DiagramObjects)
			{
				QuestClass questObj = this.questLine[dObj.Key];
				List<string> prevQuests;
				List<string> notStartedQuest;
				string characterClass;
				questObj.GetPrerequisits(out prevQuests, out notStartedQuest, out characterClass);
				if (prevQuests.Count == 0)
				{
					diagramLevels[0].Add(dObj);
					dObj.Location = new Point(posX, 0);
					posX += dObj.Size.Width + 30;
					configureatedDiagramObjects.Add(dObj.Key, dObj);
				}
			}
			bool result = true;
			while (result)
			{
				result = QuestLineForm.LocateNextLevel(diagramLevels, configureatedDiagramObjects);
			}
		}

		// Token: 0x0600153F RID: 5439 RVA: 0x0009A554 File Offset: 0x00099554
		private void LoadDiagram()
		{
			if (this.questLine != null)
			{
				this.QuestDiagram.BeginRepaintTransaction();
				this.QuestDiagram.Clear();
				Size defaultSize = new Size(0, 0);
				Point defaultLocation = new Point(0, 0);
				foreach (QuestClass quest in this.questLine.Values)
				{
					if (quest != null)
					{
						string[] text = new string[]
						{
							quest.Name,
							quest.GetZoneFolder()
						};
						this.QuestDiagram.AddDiagramObject(quest.GameObject, quest.GameName, text, defaultLocation, defaultSize, quest.IsDisabled() ? Color.LightGray : Color.Bisque, null);
					}
				}
				foreach (QuestClass quest2 in this.questLine.Values)
				{
					List<string> prevQuests;
					List<string> notStartedQuest;
					string characterClass;
					quest2.GetPrerequisits(out prevQuests, out notStartedQuest, out characterClass);
					if (prevQuests != null)
					{
						foreach (string prevQuestKey in prevQuests)
						{
							DiagramObject beginObject;
							this.QuestDiagram.GetDiagramObject(prevQuestKey, out beginObject);
							DiagramObject endObject;
							this.QuestDiagram.GetDiagramObject(quest2.GameObject, out endObject);
							this.QuestDiagram.AddDiagramEdge(beginObject, endObject);
						}
					}
				}
				this.ConfigureDiagramObjects();
				this.QuestDiagram.EndRepaintTransaction();
			}
		}

		// Token: 0x06001540 RID: 5440 RVA: 0x0009A6F8 File Offset: 0x000996F8
		private void OnQuestChanged(object sender, IEnumerable<string> changedQuests)
		{
			this.ShowDataChangedWarning();
		}

		// Token: 0x06001541 RID: 5441 RVA: 0x0009A700 File Offset: 0x00099700
		private void OnObjectDeleting(object sender, GameObjectClass deletingObject)
		{
			if (deletingObject.GetTypeName() == "gameMechanics.constructor.schemes.quest.QuestResource")
			{
				this.ShowDataChangedWarning();
			}
		}

		// Token: 0x06001542 RID: 5442 RVA: 0x0009A71A File Offset: 0x0009971A
		private void ShowDataChangedWarning()
		{
			if (this.upToDate)
			{
				MessageBox.Show(Strings.QUEST_EDITOR_QUEST_LINE_NOT_UP_TO_DATE, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				this.upToDate = false;
			}
		}

		// Token: 0x06001543 RID: 5443 RVA: 0x0009A73F File Offset: 0x0009973F
		private void OnDiagramDoubleClick(object sender, MouseEventArgs e)
		{
			this.LoadQuestToQuestEditor(this.QuestDiagram.GetDiagramObject(e.Location));
		}

		// Token: 0x06001544 RID: 5444 RVA: 0x0009A758 File Offset: 0x00099758
		private void OnDiagramKeyDown(object sender, KeyEventArgs e)
		{
			Keys keyCode = e.KeyCode;
			if (keyCode != Keys.Return)
			{
				return;
			}
			this.LoadQuestToQuestEditor(this.QuestDiagram.SelectedDiagramMember as DiagramObject);
		}

		// Token: 0x06001545 RID: 5445 RVA: 0x0009A788 File Offset: 0x00099788
		private void LoadQuestToQuestEditor(DiagramObject diagramObject)
		{
			if (diagramObject != null)
			{
				this.Cursor = Cursors.WaitCursor;
				QuestClass quest = this.questEnvironment.Quests.GetObjectByDBID(diagramObject.Key) as QuestClass;
				if (quest != null)
				{
					this.questEnvironment.QuestEditor.Load(quest);
				}
				this.Cursor = Cursors.Default;
			}
		}

		// Token: 0x06001546 RID: 5446 RVA: 0x0009A7E0 File Offset: 0x000997E0
		public QuestLineForm(QuestEnvironment _questEnvironment)
		{
			this.InitializeComponent();
			this.formParamsSaver = new FormParamsSaver(this, EditorEnvironment.EditorFormsFolder + "QuestLineForm.xml", false);
			this.formParamsSaver.AutoregisterControls = false;
			this.captionBase = base.Text;
			this.QuestDiagram.AllowUserDeleteEdge = false;
			Pen commonPen = new Pen(Color.Black, 1f);
			Pen selectedPen = new Pen(Color.Blue, 3f);
			this.QuestDiagram.SetPen(DiagramMemberTypes.OBJECT, commonPen, selectedPen);
			this.QuestDiagram.SetPen(DiagramMemberTypes.EDGE, commonPen, selectedPen);
			this.QuestDiagram.MouseDoubleClick += this.OnDiagramDoubleClick;
			this.QuestDiagram.KeyDown += this.OnDiagramKeyDown;
			this.questEnvironment = _questEnvironment;
			this.questEnvironment.QuestChanged += this.OnQuestChanged;
			this.questEnvironment.ObjectDeleting += this.OnObjectDeleting;
		}

		// Token: 0x06001547 RID: 5447 RVA: 0x0009A8E4 File Offset: 0x000998E4
		public void LoadLine(QuestClass quest)
		{
			this.LoadQuestLineData(quest);
			this.LoadDiagram();
			DiagramObject diagramObject;
			this.QuestDiagram.GetDiagramObject(quest.GameObject, out diagramObject);
			this.QuestDiagram.SelectedDiagramMember = diagramObject;
			this.upToDate = true;
			this.Text = this.captionBase + " - " + quest.GameName;
		}

		// Token: 0x04000F02 RID: 3842
		private const int xStep = 30;

		// Token: 0x04000F03 RID: 3843
		private const int yStep = 60;

		// Token: 0x04000F06 RID: 3846
		private readonly FormParamsSaver formParamsSaver;

		// Token: 0x04000F07 RID: 3847
		private readonly QuestEnvironment questEnvironment;

		// Token: 0x04000F08 RID: 3848
		private readonly Dictionary<string, QuestClass> questLine = new Dictionary<string, QuestClass>();

		// Token: 0x04000F09 RID: 3849
		private readonly string captionBase;

		// Token: 0x04000F0A RID: 3850
		private bool upToDate;
	}
}
