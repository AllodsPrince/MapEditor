using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using Db;
using InputState;
using LauncherTools.InputState;
using MapEditor.Forms.Base;
using MapEditor.Resources.Images;
using MapEditor.Resources.Strings;
using Tools.DBGameObjects;
using Tools.DBGameObjects.GameObjects;
using Tools.ItemTree;
using Tools.SaveAsXdbFileDialog;

namespace MapEditor.Forms.FactionEditor
{
	// Token: 0x020000EF RID: 239
	public partial class FactionEditorForm : BaseForm
	{
		// Token: 0x06000C09 RID: 3081 RVA: 0x00067C68 File Offset: 0x00066C68
		private GameObjectClass GetGameObject(string key)
		{
			GameObjectClass gameObject = null;
			if (!string.IsNullOrEmpty(key))
			{
				gameObject = this.factions.GetObjectByDBID(key);
				if (gameObject == null)
				{
					gameObject = this.mobs.GetObjectByDBID(key);
				}
			}
			return gameObject;
		}

		// Token: 0x06000C0A RID: 3082 RVA: 0x00067C9D File Offset: 0x00066C9D
		private GameObjectClass GetGameObjectByNode(TreeNode node)
		{
			if (node != null)
			{
				return this.GetGameObject(node.Tag as string);
			}
			return null;
		}

		// Token: 0x06000C0B RID: 3083 RVA: 0x00067CB8 File Offset: 0x00066CB8
		private void SetPropertyControlObject(GameObjectClass selectedObj)
		{
			IDatabase mainDb = IDatabase.GetMainDatabase();
			if (selectedObj != null && mainDb != null)
			{
				DBID dbid = mainDb.GetDBIDByName(selectedObj.GameObject);
				if (!DBID.IsNullOrEmpty(dbid))
				{
					IObjMan objMan = mainDb.GetManipulator(dbid);
					if (objMan != null)
					{
						this.PropertyControl.SelectedObject = objMan;
						return;
					}
				}
			}
			this.PropertyControl.SelectedObject = null;
		}

		// Token: 0x06000C0C RID: 3084 RVA: 0x00067D0A File Offset: 0x00066D0A
		private void LoadFilters()
		{
			this.filterParams = Serializer.Load<FactionEditorForm.FilterParams>(FactionEditorForm.filterConfigFilePath);
			if (this.filterParams == null)
			{
				this.filterParams = new FactionEditorForm.FilterParams();
			}
			this.LoadFiltersToComboBox();
		}

		// Token: 0x06000C0D RID: 3085 RVA: 0x00067D38 File Offset: 0x00066D38
		private Filter GetSelectedFilter()
		{
			FolderItemFilter selectedFolderFilter = null;
			if (this.filterParams != null)
			{
				foreach (FolderItemFilter filter in this.filterParams.Filters)
				{
					if (filter.Name == this.filterParams.SelectedFilter)
					{
						selectedFolderFilter = filter;
						break;
					}
				}
			}
			Filter selectedFilter;
			if (selectedFolderFilter != null)
			{
				selectedFilter = new FactionEditorForm.FilterFromFolderFilter(selectedFolderFilter);
				if (this.HideMobWithFactionCheckBox.Checked)
				{
					selectedFilter = new FilterComposition(selectedFilter, this.mobWithoutFactionFilter);
				}
			}
			else
			{
				selectedFilter = (this.HideMobWithFactionCheckBox.Checked ? this.mobWithoutFactionFilter : null);
			}
			return selectedFilter;
		}

		// Token: 0x06000C0E RID: 3086 RVA: 0x00067DF0 File Offset: 0x00066DF0
		private void LoadFiltersToComboBox()
		{
			this.FilterComboBox.Items.Clear();
			if (this.filterParams != null)
			{
				foreach (FolderItemFilter filter in this.filterParams.Filters)
				{
					if (filter != null)
					{
						this.FilterComboBox.Items.Add(filter.Name);
					}
				}
				if (!string.IsNullOrEmpty(this.filterParams.SelectedFilter) && this.FilterComboBox.Items.Contains(this.filterParams.SelectedFilter))
				{
					this.FilterComboBox.SelectedItem = this.filterParams.SelectedFilter;
				}
			}
		}

		// Token: 0x06000C0F RID: 3087 RVA: 0x00067EBC File Offset: 0x00066EBC
		private void LoadErrorList(IEnumerable<string> errorItems)
		{
			this.ErrorItemList.Items.Clear();
			foreach (string item in errorItems)
			{
				GameObjectClass gameObject = this.GetGameObject(item);
				if (gameObject != null)
				{
					this.ErrorItemList.Items.Add(gameObject);
				}
			}
			this.ErrorItemList.Visible = true;
		}

		// Token: 0x06000C10 RID: 3088 RVA: 0x00067F38 File Offset: 0x00066F38
		private void LoadTree()
		{
			this.Cursor = Cursors.WaitCursor;
			TreeNode selectedNode = this.FactionTreeView.SelectedNode;
			this.FactionTreeView.Nodes.Clear();
			Filter selectedFilter = this.GetSelectedFilter();
			this.factionTree.StartAddingTransaction();
			this.AddItemsToTree(this.factions, selectedFilter);
			this.AddItemsToTree(this.mobs, selectedFilter);
			List<string> errorItems;
			bool result = this.factionTree.EndAddingTransaction(out errorItems);
			this.Cursor = Cursors.Default;
			if (!result)
			{
				MessageBox.Show(Strings.FACTION_EDITOR_CANT_LOAD_TREE_ERROR, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Hand);
				this.LoadErrorList(errorItems);
			}
			else
			{
				this.ErrorItemList.Visible = false;
			}
			if (selectedNode != null)
			{
				this.FactionTreeView.SelectedNode = this.factionTree.FindTreeNode(selectedNode.Tag as string);
			}
		}

		// Token: 0x06000C11 RID: 3089 RVA: 0x00068001 File Offset: 0x00067001
		private void AddItemToTree(GameObjectClass gameObject, Filter filter)
		{
			if (filter != null && filter.IsFiltered(gameObject))
			{
				this.factionTree.AddItem(gameObject.GameObject);
			}
		}

		// Token: 0x06000C12 RID: 3090 RVA: 0x00068024 File Offset: 0x00067024
		private void AddItemsToTree(GeneralView view, Filter filter)
		{
			foreach (GameObjectClass gameObject in view)
			{
				this.AddItemToTree(gameObject, filter);
			}
		}

		// Token: 0x06000C13 RID: 3091 RVA: 0x00068070 File Offset: 0x00067070
		private void OnErrorListSelectedIndexChanged(object sender, EventArgs e)
		{
			if (this.ErrorItemList.SelectedItems != null && this.ErrorItemList.SelectedItems.Count > 0)
			{
				this.SetPropertyControlObject(this.ErrorItemList.SelectedItems[0] as GameObjectClass);
			}
		}

		// Token: 0x06000C14 RID: 3092 RVA: 0x000680B0 File Offset: 0x000670B0
		private void OnSelectedFilterChanged(object sender, EventArgs e)
		{
			if (this.filterParams != null && this.filterParams.SelectedFilter != this.FilterComboBox.Text)
			{
				this.filterParams.SelectedFilter = this.FilterComboBox.Text;
			}
			this.LoadTree();
		}

		// Token: 0x06000C15 RID: 3093 RVA: 0x00068100 File Offset: 0x00067100
		private void OnNodeSelected(object sender, TreeViewEventArgs e)
		{
			GameObjectClass selectedObj = this.GetGameObjectByNode(e.Node);
			this.SetPropertyControlObject(selectedObj);
			this.AddSubfactionButton.Enabled = (selectedObj != null && selectedObj.GetTypeName() == "gameMechanics.world.creature.Faction");
			this.ClearFactionButton.Enabled = (selectedObj != null && e.Node != null && selectedObj.GetTypeName() == "gameMechanics.world.creature.Faction" && e.Node.Parent != null);
		}

		// Token: 0x06000C16 RID: 3094 RVA: 0x00068180 File Offset: 0x00067180
		private void OnNodeTextEdited(object sender, NodeLabelEditEventArgs e)
		{
			IDatabase mainDb = IDatabase.GetMainDatabase();
			if (mainDb != null && e.Node != null && e.Label != null && e.Label != e.Node.Text)
			{
				GameObjectClass selectedObj = this.GetGameObjectByNode(e.Node);
				string text = e.Label;
				MobClass mob = selectedObj as MobClass;
				if (mob != null)
				{
					mob.SetGameName(text);
				}
				else
				{
					FactionClass faction = selectedObj as FactionClass;
					if (faction != null)
					{
						faction.SetGameName(text);
					}
				}
				mainDb.SaveChanges();
				this.factionTree.RestoreOrderByTimer();
			}
		}

		// Token: 0x06000C17 RID: 3095 RVA: 0x0006820C File Offset: 0x0006720C
		private void OnFilterButtonClick(object sender, EventArgs e)
		{
			FolderItemFilterDialog folderItemFilterDialog = new FolderItemFilterDialog(this.filterParams, new char[]
			{
				'/',
				'\\'
			}, 1, EditorEnvironment.EditorFormsFolder);
			folderItemFilterDialog.ItemSources = new List<ItemList.IItemSource>(2);
			folderItemFilterDialog.ItemSources.Add(new FactionEditorForm.FactionSource(this.factions));
			folderItemFilterDialog.ItemSources.Add(new FactionEditorForm.MobSource(this.mobs));
			folderItemFilterDialog.SelectedFilterName = this.FilterComboBox.Text;
			folderItemFilterDialog.ShowDialog(base.Context.MainForm);
			this.LoadFiltersToComboBox();
			this.FilterComboBox.SelectedItem = folderItemFilterDialog.SelectedFilterName;
		}

		// Token: 0x06000C18 RID: 3096 RVA: 0x000682AF File Offset: 0x000672AF
		private void OnHideMobWithFactionChecked(object sender, EventArgs e)
		{
			this.LoadTree();
		}

		// Token: 0x06000C19 RID: 3097 RVA: 0x000682B7 File Offset: 0x000672B7
		private void OnCreateNewFaction(object sender, EventArgs e)
		{
			this.CreateNewFaction(sender == this.AddSubfactionButton);
		}

		// Token: 0x06000C1A RID: 3098 RVA: 0x000682C8 File Offset: 0x000672C8
		private void OnClearFactionClick(object sender, EventArgs e)
		{
			GameObjectClass gameObject = this.GetGameObjectByNode(this.FactionTreeView.SelectedNode);
			IDatabase mainDb = IDatabase.GetMainDatabase();
			if (gameObject != null && mainDb != null)
			{
				FactionClass movingFaction = gameObject as FactionClass;
				if (movingFaction != null)
				{
					movingFaction.ParentFaction = string.Empty;
				}
				else
				{
					MobClass movingMob = gameObject as MobClass;
					if (movingMob != null)
					{
						movingMob.Faction = string.Empty;
					}
				}
				mainDb.SaveChanges();
			}
		}

		// Token: 0x06000C1B RID: 3099 RVA: 0x00068328 File Offset: 0x00067328
		private void OnCloseButtonClick(object sender, EventArgs e)
		{
			base.Context.StateContainer.Invoke("toggle_faction_editor", default(MethodArgs));
		}

		// Token: 0x06000C1C RID: 3100 RVA: 0x00068354 File Offset: 0x00067354
		private void OnDBObjectChanged(DBID dbid)
		{
			IDatabase mainDb = IDatabase.GetMainDatabase();
			if (this.factionTree != null && !DBID.IsNullOrEmpty(dbid) && mainDb != null)
			{
				string type = mainDb.GetClassTypeName(dbid);
				if (type == "gameMechanics.world.creature.Faction" || type == "gameMechanics.world.mob.MobWorld")
				{
					GameObjectClass gameObjectClass = new GameObjectClass(dbid);
					TreeNode itemNode = this.factionTree.FindTreeNode(gameObjectClass.GameObject);
					if (itemNode != null)
					{
						if (!this.factionTree.UpdateNode(itemNode))
						{
							this.LoadTree();
							return;
						}
					}
					else
					{
						Filter selectedFilter = this.GetSelectedFilter();
						if (selectedFilter != null && selectedFilter.IsFiltered(gameObjectClass))
						{
							this.LoadTree();
						}
					}
				}
			}
		}

		// Token: 0x06000C1D RID: 3101 RVA: 0x000683F0 File Offset: 0x000673F0
		private void OnDBObjectAdded(DBID dbid)
		{
			IDatabase mainDb = IDatabase.GetMainDatabase();
			if (this.factionTree != null && !DBID.IsNullOrEmpty(dbid) && mainDb != null)
			{
				string type = mainDb.GetClassTypeName(dbid);
				if (type == "gameMechanics.world.creature.Faction")
				{
					FactionClass newFaction = new FactionClass(dbid);
					this.factions.AddObjectIfNotFinded(newFaction);
					Filter selectedFilter = this.GetSelectedFilter();
					this.AddItemToTree(newFaction, selectedFilter);
				}
			}
		}

		// Token: 0x06000C1E RID: 3102 RVA: 0x00068450 File Offset: 0x00067450
		private void OnFactionContextMenuStripOpening(object sender, CancelEventArgs e)
		{
			TreeNode mouseNode = this.FactionTreeView.GetNodeAt(this.FactionTreeView.PointToClient(Control.MousePosition));
			if (mouseNode != null)
			{
				this.FactionTreeView.SelectedNode = mouseNode;
			}
			foreach (object obj in this.FactionContextMenuStrip.Items)
			{
				ToolStripItem toolStrip = (ToolStripItem)obj;
				string tag = toolStrip.Tag as string;
				string a;
				if ((a = tag) != null)
				{
					if (!(a == "toggle_new_faction"))
					{
						if (!(a == "toggle_new_subfaction"))
						{
							if (!(a == "toggle_clear_faction"))
							{
								if (a == "toggle_hide_mob_with_faction")
								{
									ToolStripMenuItem item = toolStrip as ToolStripMenuItem;
									if (item != null)
									{
										item.Checked = this.HideMobWithFactionCheckBox.Checked;
									}
									toolStrip.Enabled = this.HideMobWithFactionCheckBox.Enabled;
								}
							}
							else
							{
								toolStrip.Enabled = (this.ClearFactionButton.Enabled && mouseNode != null);
							}
						}
						else
						{
							toolStrip.Enabled = (this.AddSubfactionButton.Enabled && mouseNode != null);
						}
					}
					else
					{
						toolStrip.Enabled = this.AddFactionButton.Enabled;
					}
				}
			}
		}

		// Token: 0x06000C1F RID: 3103 RVA: 0x000685AC File Offset: 0x000675AC
		private void OnFactionContextMenuStripClick(object sender, ToolStripItemClickedEventArgs e)
		{
			if (e != null && e.ClickedItem != null)
			{
				string tag = e.ClickedItem.Tag as string;
				string a;
				if ((a = tag) != null)
				{
					if (a == "toggle_new_faction")
					{
						this.CreateNewFaction(false);
						return;
					}
					if (a == "toggle_new_subfaction")
					{
						this.CreateNewFaction(true);
						return;
					}
					if (a == "toggle_clear_faction")
					{
						this.OnClearFactionClick(sender, e);
						return;
					}
					if (!(a == "toggle_hide_mob_with_faction"))
					{
						return;
					}
					ToolStripMenuItem item = e.ClickedItem as ToolStripMenuItem;
					if (item != null)
					{
						this.HideMobWithFactionCheckBox.Checked = !item.Checked;
					}
				}
			}
		}

		// Token: 0x06000C20 RID: 3104 RVA: 0x00068651 File Offset: 0x00067651
		private void OnTreeItemDrag(object sender, ItemDragEventArgs e)
		{
			this.FactionTreeView.DoDragDrop(e.Item, DragDropEffects.Move);
		}

		// Token: 0x06000C21 RID: 3105 RVA: 0x00068666 File Offset: 0x00067666
		private static void OnTreeDragEnter(object sender, DragEventArgs e)
		{
			if (e.Data.GetDataPresent("System.Windows.Forms.TreeNode", false))
			{
				e.Effect = DragDropEffects.Move;
				return;
			}
			e.Effect = DragDropEffects.None;
		}

		// Token: 0x06000C22 RID: 3106 RVA: 0x0006868C File Offset: 0x0006768C
		private void OnTreeDragOver(object sender, DragEventArgs e)
		{
			if (!e.Data.GetDataPresent("System.Windows.Forms.TreeNode", false))
			{
				return;
			}
			TreeNode mouseNode = this.FactionTreeView.GetNodeAt(this.FactionTreeView.PointToClient(Control.MousePosition));
			TreeNode dropNode = e.Data.GetData("System.Windows.Forms.TreeNode") as TreeNode;
			if (this.FactionTreeView.SelectedNode != mouseNode && mouseNode != null)
			{
				this.FactionTreeView.SelectedNode = mouseNode;
			}
			if (mouseNode != null && dropNode != null && dropNode.Parent != mouseNode && dropNode != mouseNode)
			{
				GameObjectClass mouseGameObject = this.factions.GetObjectByDBID(mouseNode.Tag as string);
				if (mouseGameObject != null && !ItemTree.IsItemDecsendant(dropNode, mouseGameObject.GameObject))
				{
					e.Effect = DragDropEffects.Move;
					return;
				}
			}
			e.Effect = DragDropEffects.None;
		}

		// Token: 0x06000C23 RID: 3107 RVA: 0x00068748 File Offset: 0x00067748
		private void OnTreeDragDrop(object sender, DragEventArgs e)
		{
			if (!e.Data.GetDataPresent("System.Windows.Forms.TreeNode", false))
			{
				return;
			}
			FactionClass parent = this.GetGameObjectByNode(this.FactionTreeView.SelectedNode) as FactionClass;
			TreeNode movingNode = e.Data.GetData("System.Windows.Forms.TreeNode") as TreeNode;
			IDatabase mainDb = IDatabase.GetMainDatabase();
			if (mainDb != null && parent != null && movingNode != null)
			{
				GameObjectClass movingObject = this.GetGameObjectByNode(movingNode);
				if (movingObject != null && !ItemTree.IsItemDecsendant(movingNode, parent.GameObject))
				{
					FactionClass movingFaction = movingObject as FactionClass;
					if (movingFaction != null)
					{
						movingFaction.ParentFaction = parent.GameObject;
					}
					else
					{
						MobClass movingMob = movingObject as MobClass;
						if (movingMob != null)
						{
							movingMob.Faction = parent.GameObject;
						}
					}
					mainDb.SaveChanges();
				}
			}
		}

		// Token: 0x06000C24 RID: 3108 RVA: 0x000687F8 File Offset: 0x000677F8
		private void CreateNewFaction(bool addToNode)
		{
			GameObjectClass gameObject = this.GetGameObjectByNode(this.FactionTreeView.SelectedNode);
			if (addToNode && (gameObject == null || gameObject.GetTypeName() != "gameMechanics.world.creature.Faction"))
			{
				return;
			}
			string folder = "World/Factions";
			if (gameObject != null && gameObject.GetTypeName() == "gameMechanics.world.creature.Faction")
			{
				folder = DBMethods.GetContainingFolderWithoutSlash(gameObject.GameObject);
			}
			SaveAsXdbFileDialogForm dialog = new SaveAsXdbFileDialogForm(folder, "gameMechanics.world.creature.Faction");
			dialog.WarnIfFileExists = false;
			if (dialog.ShowDialog(this) == DialogResult.OK)
			{
				string fileName = dialog.GetFileName();
				if (!File.Exists(fileName))
				{
					IDatabase mainDb = IDatabase.GetMainDatabase();
					DBID dbid = IDatabase.CreateDBIDByName(fileName);
					if (mainDb != null && !dbid.IsEmpty() && !mainDb.DoesObjectExist(dbid))
					{
						IObjMan objMan = mainDb.CreateNewObject("gameMechanics.world.creature.Faction");
						if (objMan != null)
						{
							mainDb.AddNewObject(dbid, objMan);
							FactionClass faction = new FactionClass(objMan);
							if (addToNode)
							{
								faction.ParentFaction = gameObject.GameObject;
							}
							mainDb.SaveChanges();
							return;
						}
					}
				}
				MessageBox.Show(string.Format(Strings.FACTION_EDITOR_CANT_CREATE_OBJECT_ERROR, fileName), this.Text, MessageBoxButtons.OK, MessageBoxIcon.Hand);
			}
		}

		// Token: 0x06000C25 RID: 3109 RVA: 0x00068908 File Offset: 0x00067908
		private void LoadData()
		{
			this.factions = new GeneralView(null);
			DBMethods.LoadObjects("gameMechanics.world.creature.Faction", this.factions, false);
			this.mobs = new GeneralView(null);
			DBMethods.LoadObjects("gameMechanics.world.mob.MobWorld", this.mobs, false);
			this.LoadFilters();
			List<ItemTree.IDataMiner> dataMiners = new List<ItemTree.IDataMiner>(2);
			dataMiners.Add(new FactionEditorForm.FactionDataMiner(this.factions));
			dataMiners.Add(new FactionEditorForm.MobDataMiner(this.mobs));
			this.factionTree.Bind(this.FactionTreeView, dataMiners);
			this.LoadTree();
			IDatabase mainDb = IDatabase.GetMainDatabase();
			if (mainDb != null)
			{
				this.dbEventsGenerator = new DbEventsGenerator(mainDb);
				this.dbEventsGenerator.DBObjectChanged += this.OnDBObjectChanged;
				this.dbEventsGenerator.DBObjectAdded += this.OnDBObjectAdded;
			}
		}

		// Token: 0x06000C26 RID: 3110 RVA: 0x000689D9 File Offset: 0x000679D9
		private void OnLoadForm(object sender, EventArgs e)
		{
			this.FactionTreeView.Sorted = true;
			this.LoadData();
		}

		// Token: 0x06000C27 RID: 3111 RVA: 0x000689F0 File Offset: 0x000679F0
		private void OnClosedForm(object sender, EventArgs e)
		{
			if (this.filterParams != null)
			{
				Serializer.Save(FactionEditorForm.filterConfigFilePath, this.filterParams, false);
			}
			this.factionTree.Unbind();
			if (base.Context != null && base.Context.StateContainer != null)
			{
				base.Context.StateContainer.BindState(this.factionEditorState);
			}
		}

		// Token: 0x06000C28 RID: 3112 RVA: 0x00068A50 File Offset: 0x00067A50
		public FactionEditorForm(MainForm.Context context) : base(EditorEnvironment.EditorFormsFolder + "FactionEditorForm.xml", context)
		{
			this.InitializeComponent();
			base.Load += this.OnLoadForm;
			base.FormClosed += new FormClosedEventHandler(this.OnClosedForm);
			this.FilterButton.Click += this.OnFilterButtonClick;
			this.FilterComboBox.SelectedIndexChanged += this.OnSelectedFilterChanged;
			this.FactionTreeView.AfterSelect += this.OnNodeSelected;
			this.FactionTreeView.AfterLabelEdit += this.OnNodeTextEdited;
			this.FactionTreeView.ItemDrag += this.OnTreeItemDrag;
			this.FactionTreeView.DragEnter += FactionEditorForm.OnTreeDragEnter;
			this.FactionTreeView.DragDrop += this.OnTreeDragDrop;
			this.FactionTreeView.DragOver += this.OnTreeDragOver;
			this.AddFactionButton.Click += this.OnCreateNewFaction;
			this.AddSubfactionButton.Click += this.OnCreateNewFaction;
			this.HideMobWithFactionCheckBox.CheckedChanged += this.OnHideMobWithFactionChecked;
			this.ClearFactionButton.Click += this.OnClearFactionClick;
			this.CloseButton.Click += this.OnCloseButtonClick;
			this.ErrorItemList.SelectedIndexChanged += this.OnErrorListSelectedIndexChanged;
			this.FactionContextMenuStrip.Opening += this.OnFactionContextMenuStripOpening;
			this.FactionContextMenuStrip.ItemClicked += this.OnFactionContextMenuStripClick;
			if (base.ParamsSaver != null)
			{
				base.ParamsSaver.RegisterControl(this.SplitContainer);
			}
			context.StateContainer.BindState(this.factionEditorState);
		}

		// Token: 0x040009F4 RID: 2548
		private readonly ItemTree factionTree = new ItemTree();

		// Token: 0x040009F5 RID: 2549
		private FactionEditorForm.FilterParams filterParams;

		// Token: 0x040009F6 RID: 2550
		private GeneralView factions;

		// Token: 0x040009F7 RID: 2551
		private GeneralView mobs;

		// Token: 0x040009F8 RID: 2552
		private DbEventsGenerator dbEventsGenerator;

		// Token: 0x040009F9 RID: 2553
		private readonly FactionEditorForm.MobWithoutFactionFilter mobWithoutFactionFilter = new FactionEditorForm.MobWithoutFactionFilter();

		// Token: 0x040009FA RID: 2554
		private readonly State factionEditorState = new State("FactionEditorState");

		// Token: 0x040009FB RID: 2555
		private static readonly string filterConfigFilePath = EditorEnvironment.EditorFolder + "Filters/FactionEditorFilters.xml";

		// Token: 0x020000F0 RID: 240
		private class FactionDataMiner : ItemTree.IDataMiner
		{
			// Token: 0x06000C2C RID: 3116 RVA: 0x00069552 File Offset: 0x00068552
			public FactionDataMiner(GeneralView _factions)
			{
				this.factions = _factions;
			}

			// Token: 0x06000C2D RID: 3117 RVA: 0x00069564 File Offset: 0x00068564
			public bool GetItemData(string item, out string parent, out string text, out string toolTip, out string imageKey, out Bitmap bitmap)
			{
				if (this.factions != null)
				{
					FactionClass faction = this.factions.GetObjectByDBID(item) as FactionClass;
					if (faction != null)
					{
						parent = faction.ParentFaction;
						text = faction.GameName;
						toolTip = item;
						if (string.IsNullOrEmpty(text))
						{
							text = faction.Name;
						}
						imageKey = "faction_image_key";
						bitmap = Images.script_zone;
						return true;
					}
				}
				parent = null;
				text = null;
				imageKey = null;
				bitmap = null;
				toolTip = null;
				return false;
			}

			// Token: 0x04000A13 RID: 2579
			private readonly GeneralView factions;
		}

		// Token: 0x020000F1 RID: 241
		private class MobDataMiner : ItemTree.IDataMiner
		{
			// Token: 0x06000C2E RID: 3118 RVA: 0x000695D8 File Offset: 0x000685D8
			public MobDataMiner(GeneralView _mobs)
			{
				this.mobs = _mobs;
			}

			// Token: 0x06000C2F RID: 3119 RVA: 0x000695E8 File Offset: 0x000685E8
			public bool GetItemData(string item, out string parent, out string text, out string toolTip, out string imageKey, out Bitmap bitmap)
			{
				if (this.mobs != null)
				{
					MobClass mob = this.mobs.GetObjectByDBID(item) as MobClass;
					if (mob != null)
					{
						parent = mob.Faction;
						text = mob.GameName;
						toolTip = item;
						if (string.IsNullOrEmpty(text))
						{
							text = mob.Name;
						}
						if (!string.IsNullOrEmpty(parent))
						{
							imageKey = "mob_world_image_key";
							bitmap = Images.mob_spawn;
						}
						else
						{
							imageKey = "mob_world_red_image_key";
							bitmap = Images.mob_spawn_red;
						}
						return true;
					}
				}
				parent = null;
				text = null;
				toolTip = null;
				imageKey = null;
				bitmap = null;
				return false;
			}

			// Token: 0x04000A14 RID: 2580
			private readonly GeneralView mobs;
		}

		// Token: 0x020000F2 RID: 242
		private class FactionSource : ItemList.IItemSource
		{
			// Token: 0x06000C30 RID: 3120 RVA: 0x00069677 File Offset: 0x00068677
			public FactionSource(GeneralView _factions)
			{
				this.factions = _factions;
			}

			// Token: 0x17000242 RID: 578
			// (get) Token: 0x06000C31 RID: 3121 RVA: 0x00069686 File Offset: 0x00068686
			public IEnumerable<string> Items
			{
				get
				{
					return this.factions.KeyCollection;
				}
			}

			// Token: 0x04000A15 RID: 2581
			private readonly GeneralView factions;
		}

		// Token: 0x020000F3 RID: 243
		private class MobSource : ItemList.IItemSource
		{
			// Token: 0x06000C32 RID: 3122 RVA: 0x00069693 File Offset: 0x00068693
			public MobSource(GeneralView _mobs)
			{
				this.mobs = _mobs;
			}

			// Token: 0x17000243 RID: 579
			// (get) Token: 0x06000C33 RID: 3123 RVA: 0x000696A2 File Offset: 0x000686A2
			public IEnumerable<string> Items
			{
				get
				{
					return this.mobs.KeyCollection;
				}
			}

			// Token: 0x04000A16 RID: 2582
			private readonly GeneralView mobs;
		}

		// Token: 0x020000F4 RID: 244
		public class FilterParams : FolderItemFilters.FolderItemFiltersParams
		{
			// Token: 0x17000244 RID: 580
			// (get) Token: 0x06000C34 RID: 3124 RVA: 0x000696AF File Offset: 0x000686AF
			// (set) Token: 0x06000C35 RID: 3125 RVA: 0x000696B7 File Offset: 0x000686B7
			public string SelectedFilter
			{
				get
				{
					return this.selectedFilter;
				}
				set
				{
					this.selectedFilter = value;
				}
			}

			// Token: 0x04000A17 RID: 2583
			private string selectedFilter = string.Empty;
		}

		// Token: 0x020000F5 RID: 245
		public class MobWithoutFactionFilter : Filter
		{
			// Token: 0x06000C37 RID: 3127 RVA: 0x000696D4 File Offset: 0x000686D4
			public bool IsFiltered(GameObjectClass gameObject)
			{
				MobClass mob = gameObject as MobClass;
				return mob != null && string.IsNullOrEmpty(mob.Faction);
			}
		}

		// Token: 0x020000F6 RID: 246
		public class FilterFromFolderFilter : Filter
		{
			// Token: 0x06000C39 RID: 3129 RVA: 0x00069700 File Offset: 0x00068700
			public FilterFromFolderFilter(FolderItemFilter _folderFilter)
			{
				this.folderFilter = _folderFilter;
			}

			// Token: 0x06000C3A RID: 3130 RVA: 0x0006970F File Offset: 0x0006870F
			public bool IsFiltered(GameObjectClass gameObject)
			{
				return this.folderFilter != null && !this.folderFilter.Separator && this.folderFilter.Valid(gameObject.GameObject);
			}

			// Token: 0x04000A18 RID: 2584
			private readonly FolderItemFilter folderFilter;
		}
	}
}
