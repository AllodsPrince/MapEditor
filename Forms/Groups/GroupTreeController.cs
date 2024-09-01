using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using InputState;
using MapEditor.Map.Containers;
using MapEditor.Map.MapObjects;
using MapEditor.Resources.Strings;
using MapEditor.Scene;
using Tools.Groups;
using Tools.MapObjects;

namespace MapEditor.Forms.Groups
{
	// Token: 0x0200026C RID: 620
	internal class GroupTreeController
	{
		// Token: 0x06001D4C RID: 7500 RVA: 0x000BB39F File Offset: 0x000BA39F
		private void OnTreeViewItemDrag(object sender, ItemDragEventArgs e)
		{
			this.treeView.Parent.DoDragDrop(e.Item, DragDropEffects.Move);
		}

		// Token: 0x06001D4D RID: 7501 RVA: 0x000BB3BC File Offset: 0x000BA3BC
		private void OnTreeViewDragOver(object sender, DragEventArgs e)
		{
			e.Effect = DragDropEffects.Move;
			Point pt = new Point(e.X, e.Y);
			pt = this.treeView.PointToClient(pt);
			TreeNode node = this.treeView.GetNodeAt(pt);
			if (node != null)
			{
				if (this.prevHighlightedNode != node)
				{
					if (this.prevHighlightedNode != null)
					{
						if (this.prevHighlightedNodeSelected)
						{
							this.prevHighlightedNode.BackColor = SystemColors.Highlight;
							this.prevHighlightedNode.ForeColor = SystemColors.HighlightText;
						}
						else
						{
							this.prevHighlightedNode.BackColor = this.treeView.BackColor;
							this.prevHighlightedNode.ForeColor = this.treeView.ForeColor;
						}
					}
					this.prevHighlightedNode = node;
					this.prevHighlightedNodeSelected = this.treeView.GetItemSelection(node);
				}
				node.BackColor = Color.SkyBlue;
				node.ForeColor = this.treeView.ForeColor;
			}
		}

		// Token: 0x06001D4E RID: 7502 RVA: 0x000BB4A0 File Offset: 0x000BA4A0
		private void OnTreeViewDragDrop(object sender, DragEventArgs e)
		{
			Point pt = new Point(e.X, e.Y);
			pt = this.treeView.PointToClient(pt);
			TreeNode node = this.treeView.GetNodeAt(pt);
			if (node != null)
			{
				if (this.prevHighlightedNodeSelected)
				{
					this.prevHighlightedNode.BackColor = SystemColors.Highlight;
					this.prevHighlightedNode.ForeColor = SystemColors.HighlightText;
				}
				else
				{
					this.prevHighlightedNode.BackColor = this.treeView.BackColor;
					this.prevHighlightedNode.ForeColor = this.treeView.ForeColor;
				}
				Dictionary<IMapObject, IMapObject> objectList = new Dictionary<IMapObject, IMapObject>();
				foreach (object obj2 in this.treeView.SelectedNodes)
				{
					TreeNode currentNode = (TreeNode)obj2;
					IMapObject obj;
					if (this.nodeToObjectList.TryGetValue(currentNode, out obj) && !objectList.ContainsKey(obj))
					{
						objectList.Add(obj, obj);
					}
				}
				IMapObject mapObject;
				if (this.nodeToObjectList.TryGetValue(node, out mapObject))
				{
					this.groupContainer.SetGroup(objectList, mapObject.GroupName);
					this.selector.Clear();
				}
				GroupTreeNode groupTreeNode;
				if (this.nodeToGroupList.TryGetValue(node, out groupTreeNode))
				{
					string newGroupName = this.groupContainer.GetFullGroupName(groupTreeNode);
					this.groupContainer.SetGroup(objectList, newGroupName);
					this.selector.Clear();
				}
			}
		}

		// Token: 0x06001D4F RID: 7503 RVA: 0x000BB61C File Offset: 0x000BA61C
		private void OnSelectionChanged(TreeNode node, bool selected)
		{
			if (!this.handleTreeViewSelectionChange)
			{
				return;
			}
			this.handleTreeViewSelectionChange = false;
			if (this.selector != null)
			{
				if (selected)
				{
					IMapObject mapObject;
					if (this.nodeToObjectList.TryGetValue(node, out mapObject))
					{
						this.selector.Add(GroupContainer.GroupSelectionMode.Free, mapObject);
					}
					GroupTreeNode groupTreeNode;
					if (!this.nodeToGroupList.TryGetValue(node, out groupTreeNode))
					{
						goto IL_138;
					}
					this.handleTreeViewSelectionChange = false;
					this.treeView.SelectedNode = node;
					this.handleTreeViewSelectionChange = true;
					List<IMapObject> selectedList = new List<IMapObject>();
					this.groupContainer.GetMapObjectsReqursive(groupTreeNode, selectedList);
					using (List<IMapObject>.Enumerator enumerator = selectedList.GetEnumerator())
					{
						while (enumerator.MoveNext())
						{
							IMapObject selectedMapObject = enumerator.Current;
							this.selector.Add(GroupContainer.GroupSelectionMode.Free, selectedMapObject);
						}
						goto IL_138;
					}
				}
				IMapObject mapObject2;
				if (this.nodeToObjectList.TryGetValue(node, out mapObject2))
				{
					this.selector.Remove(mapObject2);
				}
				GroupTreeNode groupTreeNode2;
				if (this.nodeToGroupList.TryGetValue(node, out groupTreeNode2))
				{
					List<IMapObject> selectedList2 = new List<IMapObject>();
					this.groupContainer.GetMapObjectsReqursive(groupTreeNode2, selectedList2);
					foreach (IMapObject obj in selectedList2)
					{
						this.selector.Remove(obj);
					}
				}
			}
			IL_138:
			this.handleTreeViewSelectionChange = true;
		}

		// Token: 0x06001D50 RID: 7504 RVA: 0x000BB784 File Offset: 0x000BA784
		private void OnTreeViewDoubleClick(object sender, EventArgs e)
		{
			this.context.MainState.ActiveState = 0;
			this.context.StateContainer.Invoke("focus_on", default(MethodArgs));
		}

		// Token: 0x06001D51 RID: 7505 RVA: 0x000BB7C0 File Offset: 0x000BA7C0
		private void OnAfterLaberEdit(object sender, NodeLabelEditEventArgs e)
		{
			if (e.Label != null && e.Node != null && e.Label != e.Node.Name && e.Node != this.treeView.Nodes[0])
			{
				GroupTreeNode groupTreeNode;
				if (this.nodeToGroupList.TryGetValue(e.Node, out groupTreeNode) && !this.groupContainer.RenameGroup(groupTreeNode, e.Label))
				{
					MessageBox.Show(Strings.MAP_EDITOR_GROUP_NAME_UNIQUE, Strings.MAP_EXPORT_ERROR_TITLE);
					e.CancelEdit = true;
				}
				this.selector.Clear();
			}
			IMapObject mapObject;
			if (e.Node != null && this.nodeToObjectList.TryGetValue(e.Node, out mapObject))
			{
				e.CancelEdit = true;
			}
		}

		// Token: 0x06001D52 RID: 7506 RVA: 0x000BB880 File Offset: 0x000BA880
		private void OnObjectAdded(GroupTreeNode node, IMapObject mapObject)
		{
			if (node != null)
			{
				string objectName = MapObjectInterface.GetInterfaceSingleObjectName(mapObject);
				objectName = Str.CutFilePathAndExtention(objectName);
				if (node.ParentNode == null)
				{
					TreeNode newNode = this.treeView.Nodes[0].Nodes.Add(objectName);
					this.objectToNodeList.Add(mapObject, newNode);
					this.nodeToObjectList.Add(newNode, mapObject);
					Color color = MapObjectInterface.GetInterfaceColor(mapObject);
					color = Color.FromArgb(255, (int)(color.R / 2), (int)(color.G / 2), (int)(color.B / 2));
					this.treeView.SetNodeColor(newNode, color);
					return;
				}
				TreeNode objectListNode;
				if (this.groupToNodeList.TryGetValue(node, out objectListNode))
				{
					TreeNode newNode2 = objectListNode.Nodes.Add(objectName);
					this.objectToNodeList.Add(mapObject, newNode2);
					this.nodeToObjectList.Add(newNode2, mapObject);
					Color color2 = MapObjectInterface.GetInterfaceColor(mapObject);
					color2 = Color.FromArgb(255, (int)(color2.R / 2), (int)(color2.G / 2), (int)(color2.B / 2));
					this.treeView.SetNodeColor(newNode2, color2);
					newNode2.EnsureVisible();
				}
			}
		}

		// Token: 0x06001D53 RID: 7507 RVA: 0x000BB99C File Offset: 0x000BA99C
		private void OnObjectRemoving(GroupTreeNode node, IMapObject mapObject)
		{
			if (node != null)
			{
				TreeNode objectListNode2;
				if (node.ParentNode == null)
				{
					TreeNode objectListNode;
					if (this.objectToNodeList.TryGetValue(mapObject, out objectListNode))
					{
						this.objectToNodeList.Remove(mapObject);
						this.nodeToObjectList.Remove(objectListNode);
						this.treeView.Nodes[0].Nodes.Remove(objectListNode);
						return;
					}
				}
				else if (this.objectToNodeList.TryGetValue(mapObject, out objectListNode2))
				{
					this.objectToNodeList.Remove(mapObject);
					this.nodeToObjectList.Remove(objectListNode2);
					if (objectListNode2.Parent != null)
					{
						objectListNode2.Parent.Nodes.Remove(objectListNode2);
					}
				}
			}
		}

		// Token: 0x06001D54 RID: 7508 RVA: 0x000BBA44 File Offset: 0x000BAA44
		private void OnTreeNodeAdded(GroupTreeNode parentNode, GroupTreeNode node)
		{
			if (parentNode != null)
			{
				if (parentNode.ParentNode == null)
				{
					TreeNode newNode = this.treeView.Nodes.Add(node.GroupName);
					this.groupToNodeList.Add(node, newNode);
					this.nodeToGroupList.Add(newNode, node);
					this.treeView.SetNodeColor(newNode, GroupTreeController.groupColor);
					return;
				}
				TreeNode objectListNode;
				if (this.groupToNodeList.TryGetValue(parentNode, out objectListNode))
				{
					TreeNode newNode2 = objectListNode.Nodes.Add(node.GroupName);
					this.groupToNodeList.Add(node, newNode2);
					this.nodeToGroupList.Add(newNode2, node);
					this.treeView.SetNodeColor(newNode2, GroupTreeController.groupColor);
				}
			}
		}

		// Token: 0x06001D55 RID: 7509 RVA: 0x000BBAF0 File Offset: 0x000BAAF0
		private void OnTreeNodeRemoving(GroupTreeNode parentNode, GroupTreeNode node)
		{
			if (parentNode != null)
			{
				TreeNode objectListNode2;
				if (parentNode.ParentNode == null)
				{
					TreeNode objectListNode;
					if (this.groupToNodeList.TryGetValue(node, out objectListNode))
					{
						this.groupToNodeList.Remove(node);
						this.nodeToGroupList.Remove(objectListNode);
						this.treeView.Nodes.Remove(objectListNode);
						return;
					}
				}
				else if (this.groupToNodeList.TryGetValue(node, out objectListNode2))
				{
					this.groupToNodeList.Remove(node);
					this.nodeToGroupList.Remove(objectListNode2);
					objectListNode2.Parent.Nodes.Remove(objectListNode2);
				}
			}
		}

		// Token: 0x06001D56 RID: 7510 RVA: 0x000BBB80 File Offset: 0x000BAB80
		private void RefreshObject(IMapObject mapObject)
		{
			if (mapObject != null)
			{
				string objectName = MapObjectInterface.GetInterfaceSingleObjectName(mapObject);
				objectName = Str.CutFilePathAndExtention(objectName);
				TreeNode objectListNode;
				if (this.objectToNodeList.TryGetValue(mapObject, out objectListNode))
				{
					objectListNode.Text = objectName;
				}
				foreach (NodeStorage node in this.nodeStorage)
				{
					if (node.MapObject == mapObject)
					{
						node.Node.Text = objectName;
					}
				}
			}
		}

		// Token: 0x06001D57 RID: 7511 RVA: 0x000BBC0C File Offset: 0x000BAC0C
		private void OnMapObjectContainerSelectChanged(MapObjectContainer _mapObjectContainer, IMapObject mapObject, ref bool oldValue, ref bool newValue)
		{
			if (!this.handleTreeViewSelectionChange)
			{
				return;
			}
			TreeNode objectListNode;
			if (mapObject != null && this.objectToNodeList.TryGetValue(mapObject, out objectListNode))
			{
				this.treeView.SetItemSelection(objectListNode, newValue);
			}
			if (!newValue)
			{
				this.treeView.SelectedNode = null;
			}
		}

		// Token: 0x06001D58 RID: 7512 RVA: 0x000BBC55 File Offset: 0x000BAC55
		private void OnSpawnPointSpawnTableChanged(MapObjectContainer _mapObjectContainer, SpawnPoint spawnPoint, ref string oldValue, ref string newValue)
		{
			this.RefreshObject(spawnPoint);
		}

		// Token: 0x06001D59 RID: 7513 RVA: 0x000BBC5E File Offset: 0x000BAC5E
		private void OnPermanentDeviceDeviceChanged(MapObjectContainer _mapObjectContainer, PermanentDevice permanentDevice, ref string oldValue, ref string newValue)
		{
			this.RefreshObject(permanentDevice);
		}

		// Token: 0x06001D5A RID: 7514 RVA: 0x000BBC67 File Offset: 0x000BAC67
		private void OnClientSpawnPointVisObjectChanged(MapObjectContainer _mapObjectContainer, ClientSpawnPoint clientSpawnPoint, ref string oldValue, ref string newValue)
		{
			this.RefreshObject(clientSpawnPoint);
		}

		// Token: 0x06001D5B RID: 7515 RVA: 0x000BBC70 File Offset: 0x000BAC70
		private void OnPlayerRespawnPlaceDeviceChanged(MapObjectContainer _mapObjectContainer, PlayerRespawnPlace playerRespawnPlace, ref string oldValue, ref string newValue)
		{
			this.RefreshObject(playerRespawnPlace);
		}

		// Token: 0x06001D5C RID: 7516 RVA: 0x000BBC7C File Offset: 0x000BAC7C
		private void OnFocus(MethodArgs methodArgs)
		{
			if (this.selector != null && this.selector.MapObjects.Count > 0)
			{
				foreach (KeyValuePair<IMapObject, IMapObject> keyValuePair in this.selector.MapObjects)
				{
					TreeNode node;
					if (this.objectToNodeList.TryGetValue(keyValuePair.Key, out node))
					{
						node.EnsureVisible();
					}
				}
			}
		}

		// Token: 0x06001D5D RID: 7517 RVA: 0x000BBD04 File Offset: 0x000BAD04
		private void RestoreTree()
		{
			foreach (NodeStorage storage in this.nodeStorage)
			{
				if (storage.ParentNode == null)
				{
					this.treeView.Nodes.Add(storage.Node);
				}
				else
				{
					storage.ParentNode.Nodes.Add(storage.Node);
				}
			}
			this.nodeStorage.Clear();
		}

		// Token: 0x06001D5E RID: 7518 RVA: 0x000BBD94 File Offset: 0x000BAD94
		private bool ApplyFilter(TreeNodeCollection nodeCollection, string filterString)
		{
			List<TreeNode> deleteList = new List<TreeNode>();
			bool nodeVisible = false;
			foreach (object obj in nodeCollection)
			{
				TreeNode node = (TreeNode)obj;
				NodeStorage storage = new NodeStorage(node, node.Parent);
				IMapObject mapObject;
				if (this.nodeToObjectList.TryGetValue(node, out mapObject))
				{
					if (MapObjectInterface.ContainsText(mapObject, filterString, node.Text, true))
					{
						nodeVisible = true;
					}
					else
					{
						GroupTreeNode _groupTreeNode;
						this.nodeToGroupList.TryGetValue(node, out _groupTreeNode);
						IMapObject _mapObject;
						this.nodeToObjectList.TryGetValue(node, out _mapObject);
						storage.GroupTreeNode = _groupTreeNode;
						storage.MapObject = _mapObject;
						this.nodeStorage.Add(storage);
						deleteList.Add(node);
					}
				}
				if (this.ApplyFilter(node.Nodes, filterString))
				{
					this.nodeStorage.Remove(storage);
					deleteList.Remove(node);
				}
			}
			foreach (TreeNode node2 in deleteList)
			{
				nodeCollection.Remove(node2);
			}
			return nodeVisible;
		}

		// Token: 0x06001D5F RID: 7519 RVA: 0x000BBED0 File Offset: 0x000BAED0
		private void SelectObjectBySearchIndex(int index)
		{
			this.selector.Clear();
			this.selector.Add(this.findResultList[index]);
			this.context.StateContainer.Invoke("focus_on", default(MethodArgs));
			TreeNode node;
			this.objectToNodeList.TryGetValue(this.findResultList[index], out node);
			if (node != null)
			{
				node.EnsureVisible();
			}
		}

		// Token: 0x06001D60 RID: 7520 RVA: 0x000BBF40 File Offset: 0x000BAF40
		public GroupTreeController(TreeViewWithMultiselection _treeView, MainForm.Context _context)
		{
			this.treeView = _treeView;
			this.context = _context;
			this.treeView.ItemDrag += this.OnTreeViewItemDrag;
			this.treeView.DragOver += this.OnTreeViewDragOver;
			this.treeView.DragDrop += this.OnTreeViewDragDrop;
			this.treeView.AfterLabelEdit += this.OnAfterLaberEdit;
			this.treeView.SelectionChanged += this.OnSelectionChanged;
			this.treeView.DoubleClick += this.OnTreeViewDoubleClick;
			this.context.StateContainer.AddMethod("focus_on", new Method(this.OnFocus));
		}

		// Token: 0x06001D61 RID: 7521 RVA: 0x000BC068 File Offset: 0x000BB068
		public void Bind(GroupContainer _groupContainer, MapEditorMapObjectContainer _mapObjectContainer, MapObjectSelector _selector)
		{
			this.Unbind();
			this.groupContainer = _groupContainer;
			this.mapObjectContainer = _mapObjectContainer;
			this.selector = _selector;
			if (this.groupContainer != null)
			{
				this.treeView.Nodes.Add("Ungrouped");
				this.treeView.SetNodeColor(this.treeView.Nodes[0], GroupTreeController.groupColor);
				GroupTreeNode.ObjectAdded += this.OnObjectAdded;
				GroupTreeNode.ObjectRemoving += this.OnObjectRemoving;
				GroupTreeNode.TreeNodeAdded += this.OnTreeNodeAdded;
				GroupTreeNode.TreeNodeRemoving += this.OnTreeNodeRemoving;
			}
			if (this.mapObjectContainer != null)
			{
				this.mapObjectContainer.SelectChanged += this.OnMapObjectContainerSelectChanged;
				if (this.mapObjectContainer.SpawnPointContainer != null)
				{
					this.mapObjectContainer.SpawnPointContainer.SpawnPointSpawnTableChanged += new SpawnPointContainer.SpawnPointFieldChangedEvent<string>(this.OnSpawnPointSpawnTableChanged);
				}
				if (this.mapObjectContainer.PermanentDeviceContainer != null)
				{
					this.mapObjectContainer.PermanentDeviceContainer.PermanentDeviceDeviceChanged += new PermanentDeviceContainer.PermanentDeviceFieldChangedEvent<string>(this.OnPermanentDeviceDeviceChanged);
				}
				if (this.mapObjectContainer.ClientSpawnPointContainer != null)
				{
					this.mapObjectContainer.ClientSpawnPointContainer.ClientSpawnPointVisObjectChanged += new ClientSpawnPointContainer.ClientSpawnPointFieldChangedEvent<string>(this.OnClientSpawnPointVisObjectChanged);
				}
				if (this.mapObjectContainer.PlayerRespawnPlaceContainer != null)
				{
					this.mapObjectContainer.PlayerRespawnPlaceContainer.PlayerRespawnPlaceDeviceChanged += new PlayerRespawnPlaceContainer.PlayerRespawnPlaceFieldChangedEvent<string>(this.OnPlayerRespawnPlaceDeviceChanged);
				}
			}
		}

		// Token: 0x06001D62 RID: 7522 RVA: 0x000BC1DC File Offset: 0x000BB1DC
		public void Unbind()
		{
			if (this.groupContainer != null)
			{
				this.treeView.Nodes.Clear();
				this.groupToNodeList.Clear();
				this.objectToNodeList.Clear();
				this.nodeToGroupList.Clear();
				this.nodeToObjectList.Clear();
				GroupTreeNode.ObjectAdded -= this.OnObjectAdded;
				GroupTreeNode.ObjectRemoving -= this.OnObjectRemoving;
				GroupTreeNode.TreeNodeAdded -= this.OnTreeNodeAdded;
				GroupTreeNode.TreeNodeRemoving -= this.OnTreeNodeRemoving;
				this.groupContainer = null;
			}
			if (this.mapObjectContainer != null)
			{
				this.mapObjectContainer.SelectChanged -= this.OnMapObjectContainerSelectChanged;
				if (this.mapObjectContainer.SpawnPointContainer != null)
				{
					this.mapObjectContainer.SpawnPointContainer.SpawnPointSpawnTableChanged -= new SpawnPointContainer.SpawnPointFieldChangedEvent<string>(this.OnSpawnPointSpawnTableChanged);
				}
				if (this.mapObjectContainer.PermanentDeviceContainer != null)
				{
					this.mapObjectContainer.PermanentDeviceContainer.PermanentDeviceDeviceChanged -= new PermanentDeviceContainer.PermanentDeviceFieldChangedEvent<string>(this.OnPermanentDeviceDeviceChanged);
				}
				if (this.mapObjectContainer.ClientSpawnPointContainer != null)
				{
					this.mapObjectContainer.ClientSpawnPointContainer.ClientSpawnPointVisObjectChanged -= new ClientSpawnPointContainer.ClientSpawnPointFieldChangedEvent<string>(this.OnClientSpawnPointVisObjectChanged);
				}
				if (this.mapObjectContainer.PlayerRespawnPlaceContainer != null)
				{
					this.mapObjectContainer.PlayerRespawnPlaceContainer.PlayerRespawnPlaceDeviceChanged -= new PlayerRespawnPlaceContainer.PlayerRespawnPlaceFieldChangedEvent<string>(this.OnPlayerRespawnPlaceDeviceChanged);
				}
				this.mapObjectContainer = null;
			}
		}

		// Token: 0x170006EE RID: 1774
		// (get) Token: 0x06001D63 RID: 7523 RVA: 0x000BC348 File Offset: 0x000BB348
		public bool Binded
		{
			get
			{
				return this.groupContainer != null;
			}
		}

		// Token: 0x170006EF RID: 1775
		// (get) Token: 0x06001D64 RID: 7524 RVA: 0x000BC356 File Offset: 0x000BB356
		// (set) Token: 0x06001D65 RID: 7525 RVA: 0x000BC35E File Offset: 0x000BB35E
		public string Filter
		{
			get
			{
				return this.filter;
			}
			set
			{
				this.filter = value;
				this.RefreshFilter();
			}
		}

		// Token: 0x06001D66 RID: 7526 RVA: 0x000BC36D File Offset: 0x000BB36D
		public void RefreshFilter()
		{
			this.treeView.BeginUpdate();
			this.RestoreTree();
			this.ApplyFilter(this.treeView.Nodes, this.filter);
			this.treeView.EndUpdate();
		}

		// Token: 0x06001D67 RID: 7527 RVA: 0x000BC3A4 File Offset: 0x000BB3A4
		public string GetFocusedItemText(Point cursorPoint)
		{
			Point point = this.treeView.PointToClient(cursorPoint);
			TreeViewHitTestInfo hitTestInfo = this.treeView.HitTest(point.X, point.Y);
			if (hitTestInfo.Node != null)
			{
				return hitTestInfo.Node.Text;
			}
			return string.Empty;
		}

		// Token: 0x06001D68 RID: 7528 RVA: 0x000BC3F4 File Offset: 0x000BB3F4
		public void Find()
		{
			FindObjectForm findObjectForm = new FindObjectForm();
			if (findObjectForm.ShowDialog() == DialogResult.OK)
			{
				string findName = findObjectForm.ObjectName;
				this.findResultList.Clear();
				this.findResultIndex = -1;
				foreach (KeyValuePair<TreeNode, IMapObject> keyValuePair in this.nodeToObjectList)
				{
					if (MapObjectInterface.ContainsText(keyValuePair.Value, findName, null, true))
					{
						this.findResultList.Add(keyValuePair.Value);
					}
				}
				if (this.findResultList.Count > 0)
				{
					this.findResultIndex = 0;
					this.SelectObjectBySearchIndex(0);
				}
			}
		}

		// Token: 0x06001D69 RID: 7529 RVA: 0x000BC4AC File Offset: 0x000BB4AC
		public void FindNext()
		{
			if (this.findResultIndex == -1)
			{
				this.Find();
				return;
			}
			this.findResultIndex++;
			if (this.findResultIndex >= this.findResultList.Count)
			{
				MessageBox.Show(Strings.MAP_SEARCH_END, "Find");
				this.findResultIndex = 0;
			}
			this.SelectObjectBySearchIndex(this.findResultIndex);
		}

		// Token: 0x06001D6A RID: 7530 RVA: 0x000BC510 File Offset: 0x000BB510
		public void FindPrevious()
		{
			if (this.findResultIndex == -1)
			{
				this.Find();
				return;
			}
			this.findResultIndex--;
			if (this.findResultIndex < 0)
			{
				MessageBox.Show(Strings.MAP_SEARCH_END, "Find");
				this.findResultIndex = this.findResultList.Count - 1;
			}
			this.SelectObjectBySearchIndex(this.findResultIndex);
		}

		// Token: 0x040012A1 RID: 4769
		private static readonly Color groupColor = Color.FromArgb(255, 0, 128, 0);

		// Token: 0x040012A2 RID: 4770
		private readonly TreeViewWithMultiselection treeView;

		// Token: 0x040012A3 RID: 4771
		private readonly MainForm.Context context;

		// Token: 0x040012A4 RID: 4772
		private readonly List<NodeStorage> nodeStorage = new List<NodeStorage>();

		// Token: 0x040012A5 RID: 4773
		private GroupContainer groupContainer;

		// Token: 0x040012A6 RID: 4774
		private MapEditorMapObjectContainer mapObjectContainer;

		// Token: 0x040012A7 RID: 4775
		private MapObjectSelector selector;

		// Token: 0x040012A8 RID: 4776
		private TreeNode prevHighlightedNode;

		// Token: 0x040012A9 RID: 4777
		private bool prevHighlightedNodeSelected;

		// Token: 0x040012AA RID: 4778
		private bool handleTreeViewSelectionChange = true;

		// Token: 0x040012AB RID: 4779
		private string filter = string.Empty;

		// Token: 0x040012AC RID: 4780
		private readonly List<IMapObject> findResultList = new List<IMapObject>();

		// Token: 0x040012AD RID: 4781
		private int findResultIndex = -1;

		// Token: 0x040012AE RID: 4782
		private readonly Dictionary<GroupTreeNode, TreeNode> groupToNodeList = new Dictionary<GroupTreeNode, TreeNode>();

		// Token: 0x040012AF RID: 4783
		private readonly Dictionary<IMapObject, TreeNode> objectToNodeList = new Dictionary<IMapObject, TreeNode>();

		// Token: 0x040012B0 RID: 4784
		private readonly Dictionary<TreeNode, GroupTreeNode> nodeToGroupList = new Dictionary<TreeNode, GroupTreeNode>();

		// Token: 0x040012B1 RID: 4785
		private readonly Dictionary<TreeNode, IMapObject> nodeToObjectList = new Dictionary<TreeNode, IMapObject>();
	}
}
