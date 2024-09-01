using System;
using System.Collections;
using System.Drawing;
using System.Windows.Forms;

namespace MapEditor.Forms.Groups
{
	// Token: 0x0200018E RID: 398
	internal class TreeViewWithMultiselection : TreeView
	{
		// Token: 0x06001303 RID: 4867 RVA: 0x0008C8DC File Offset: 0x0008B8DC
		private static bool IsParent(TreeNode parentNode, TreeNode childNode)
		{
			if (parentNode == childNode)
			{
				return true;
			}
			TreeNode node = childNode;
			bool found = false;
			while (!found && node != null)
			{
				node = node.Parent;
				found = (node == parentNode);
			}
			return found;
		}

		// Token: 0x06001304 RID: 4868 RVA: 0x0008C908 File Offset: 0x0008B908
		private void UpdateNodeSelection(TreeNode node, bool selected)
		{
			if (selected)
			{
				node.ForeColor = SystemColors.HighlightText;
				node.BackColor = SystemColors.Highlight;
				return;
			}
			NodeColorInfo nodeColorInfo = node.Tag as NodeColorInfo;
			if (nodeColorInfo != null)
			{
				node.ForeColor = nodeColorInfo.ForeColor;
			}
			else
			{
				node.ForeColor = this.ForeColor;
			}
			node.BackColor = this.BackColor;
		}

		// Token: 0x06001305 RID: 4869 RVA: 0x0008C964 File Offset: 0x0008B964
		private void PaintSelectedNodes()
		{
			foreach (object obj in this.selectedNodes)
			{
				TreeNode node = (TreeNode)obj;
				node.ForeColor = SystemColors.HighlightText;
				node.BackColor = SystemColors.Highlight;
			}
		}

		// Token: 0x06001306 RID: 4870 RVA: 0x0008C9CC File Offset: 0x0008B9CC
		private void RemovePaintFromNodes()
		{
			if (this.selectedNodes.Count == 0)
			{
				return;
			}
			foreach (object obj in this.selectedNodes)
			{
				TreeNode node = (TreeNode)obj;
				NodeColorInfo nodeColorInfo = node.Tag as NodeColorInfo;
				if (nodeColorInfo != null)
				{
					node.ForeColor = nodeColorInfo.ForeColor;
				}
				else
				{
					node.ForeColor = this.ForeColor;
				}
				node.BackColor = this.BackColor;
			}
		}

		// Token: 0x06001307 RID: 4871 RVA: 0x0008CA64 File Offset: 0x0008BA64
		protected override void OnBeforeSelect(TreeViewCancelEventArgs e)
		{
			base.OnBeforeSelect(e);
			bool bControl = Control.ModifierKeys == Keys.Control;
			bool bShift = Control.ModifierKeys == Keys.Shift;
			if (bControl && this.selectedNodes.Contains(e.Node))
			{
				e.Cancel = true;
				this.RemovePaintFromNodes();
				this.selectedNodes.Remove(e.Node);
				if (this.SelectionChanged != null)
				{
					this.SelectionChanged(e.Node, false);
				}
				this.PaintSelectedNodes();
				return;
			}
			if (!bShift)
			{
				this.firstNode = e.Node;
			}
		}

		// Token: 0x06001308 RID: 4872 RVA: 0x0008CAF8 File Offset: 0x0008BAF8
		protected override void OnAfterSelect(TreeViewEventArgs e)
		{
			base.OnAfterSelect(e);
			if (e.Action == TreeViewAction.Unknown)
			{
				return;
			}
			bool bControl = Control.ModifierKeys == Keys.Control;
			bool bShift = Control.ModifierKeys == Keys.Shift;
			if (bControl)
			{
				if (!this.selectedNodes.Contains(e.Node))
				{
					this.selectedNodes.Add(e.Node);
					if (this.SelectionChanged != null)
					{
						this.SelectionChanged(e.Node, true);
					}
				}
				else
				{
					this.RemovePaintFromNodes();
					this.selectedNodes.Remove(e.Node);
					if (this.SelectionChanged != null)
					{
						this.SelectionChanged(e.Node, false);
					}
				}
				this.PaintSelectedNodes();
				return;
			}
			if (bShift && this.firstNode != null)
			{
				Queue myQueue = new Queue();
				TreeNode uppernode = this.firstNode;
				TreeNode bottomnode = e.Node;
				bool bParent = TreeViewWithMultiselection.IsParent(this.firstNode, e.Node);
				if (!bParent)
				{
					bParent = TreeViewWithMultiselection.IsParent(bottomnode, uppernode);
					if (bParent)
					{
						TreeNode t = uppernode;
						uppernode = bottomnode;
						bottomnode = t;
					}
				}
				if (bParent)
				{
					for (TreeNode i = bottomnode; i != uppernode.Parent; i = i.Parent)
					{
						if (!this.selectedNodes.Contains(i))
						{
							myQueue.Enqueue(i);
						}
					}
				}
				else if ((uppernode.Parent == null && bottomnode.Parent == null) || (uppernode.Parent != null && uppernode.Parent.Nodes.Contains(bottomnode)))
				{
					int nIndexUpper = uppernode.Index;
					int nIndexBottom = bottomnode.Index;
					if (nIndexBottom < nIndexUpper)
					{
						TreeNode t2 = uppernode;
						uppernode = bottomnode;
						bottomnode = t2;
						nIndexUpper = uppernode.Index;
						nIndexBottom = bottomnode.Index;
					}
					TreeNode j = uppernode;
					while (nIndexUpper <= nIndexBottom)
					{
						if (!this.selectedNodes.Contains(j))
						{
							myQueue.Enqueue(j);
						}
						j = j.NextNode;
						nIndexUpper++;
					}
				}
				else
				{
					if (!this.selectedNodes.Contains(uppernode))
					{
						myQueue.Enqueue(uppernode);
					}
					if (!this.selectedNodes.Contains(bottomnode))
					{
						myQueue.Enqueue(bottomnode);
					}
				}
				this.selectedNodes.AddRange(myQueue);
				foreach (object obj in myQueue)
				{
					TreeNode node = (TreeNode)obj;
					if (this.SelectionChanged != null)
					{
						this.SelectionChanged(node, true);
					}
				}
				this.PaintSelectedNodes();
				this.firstNode = e.Node;
				return;
			}
			if (this.selectedNodes != null && this.selectedNodes.Count > 0)
			{
				this.RemovePaintFromNodes();
				this.processSelection = false;
				foreach (object obj2 in this.selectedNodes)
				{
					TreeNode node2 = (TreeNode)obj2;
					if (this.SelectionChanged != null)
					{
						this.SelectionChanged(node2, false);
					}
				}
				this.selectedNodes.Clear();
				this.processSelection = true;
			}
			this.selectedNodes.Add(e.Node);
			if (this.SelectionChanged != null)
			{
				this.SelectionChanged(e.Node, true);
			}
		}

		// Token: 0x14000075 RID: 117
		// (add) Token: 0x06001309 RID: 4873 RVA: 0x0008CE38 File Offset: 0x0008BE38
		// (remove) Token: 0x0600130A RID: 4874 RVA: 0x0008CE51 File Offset: 0x0008BE51
		public event TreeViewWithMultiselection.SelectionChangedEvent SelectionChanged;

		// Token: 0x170003C3 RID: 963
		// (get) Token: 0x0600130B RID: 4875 RVA: 0x0008CE6A File Offset: 0x0008BE6A
		// (set) Token: 0x0600130C RID: 4876 RVA: 0x0008CE72 File Offset: 0x0008BE72
		public ArrayList SelectedNodes
		{
			get
			{
				return this.selectedNodes;
			}
			set
			{
				this.RemovePaintFromNodes();
				if (value != null)
				{
					this.selectedNodes = value;
				}
				else
				{
					this.selectedNodes.Clear();
				}
				this.PaintSelectedNodes();
			}
		}

		// Token: 0x0600130D RID: 4877 RVA: 0x0008CE97 File Offset: 0x0008BE97
		public void SetNodeColor(TreeNode node, Color foreColor)
		{
			node.Tag = new NodeColorInfo(foreColor);
			node.ForeColor = foreColor;
		}

		// Token: 0x0600130E RID: 4878 RVA: 0x0008CEAC File Offset: 0x0008BEAC
		public void SetItemSelection(TreeNode node, bool selection)
		{
			if (this.processSelection)
			{
				if (selection)
				{
					this.selectedNodes.Add(node);
				}
				else
				{
					this.selectedNodes.Remove(node);
				}
			}
			this.UpdateNodeSelection(node, selection);
		}

		// Token: 0x0600130F RID: 4879 RVA: 0x0008CEDC File Offset: 0x0008BEDC
		public bool GetItemSelection(TreeNode node)
		{
			return this.selectedNodes.Contains(node);
		}

		// Token: 0x04000DD5 RID: 3541
		private ArrayList selectedNodes = new ArrayList();

		// Token: 0x04000DD6 RID: 3542
		private TreeNode firstNode;

		// Token: 0x04000DD7 RID: 3543
		private bool processSelection = true;

		// Token: 0x0200018F RID: 399
		// (Invoke) Token: 0x06001312 RID: 4882
		public delegate void SelectionChangedEvent(TreeNode node, bool selected);
	}
}
