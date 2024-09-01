using System;
using System.Windows.Forms;
using Tools.Groups;
using Tools.MapObjects;

namespace MapEditor.Forms.Groups
{
	// Token: 0x0200026B RID: 619
	internal class NodeStorage
	{
		// Token: 0x06001D43 RID: 7491 RVA: 0x000BB345 File Offset: 0x000BA345
		public NodeStorage(TreeNode _node, TreeNode _parentNode)
		{
			this.node = _node;
			this.parentNode = _parentNode;
		}

		// Token: 0x170006EA RID: 1770
		// (get) Token: 0x06001D44 RID: 7492 RVA: 0x000BB35B File Offset: 0x000BA35B
		// (set) Token: 0x06001D45 RID: 7493 RVA: 0x000BB363 File Offset: 0x000BA363
		public TreeNode Node
		{
			get
			{
				return this.node;
			}
			set
			{
				this.node = value;
			}
		}

		// Token: 0x170006EB RID: 1771
		// (get) Token: 0x06001D46 RID: 7494 RVA: 0x000BB36C File Offset: 0x000BA36C
		// (set) Token: 0x06001D47 RID: 7495 RVA: 0x000BB374 File Offset: 0x000BA374
		public TreeNode ParentNode
		{
			get
			{
				return this.parentNode;
			}
			set
			{
				this.parentNode = value;
			}
		}

		// Token: 0x170006EC RID: 1772
		// (get) Token: 0x06001D48 RID: 7496 RVA: 0x000BB37D File Offset: 0x000BA37D
		// (set) Token: 0x06001D49 RID: 7497 RVA: 0x000BB385 File Offset: 0x000BA385
		public GroupTreeNode GroupTreeNode
		{
			get
			{
				return this.groupTreeNode;
			}
			set
			{
				this.groupTreeNode = value;
			}
		}

		// Token: 0x170006ED RID: 1773
		// (get) Token: 0x06001D4A RID: 7498 RVA: 0x000BB38E File Offset: 0x000BA38E
		// (set) Token: 0x06001D4B RID: 7499 RVA: 0x000BB396 File Offset: 0x000BA396
		public IMapObject MapObject
		{
			get
			{
				return this.mapObject;
			}
			set
			{
				this.mapObject = value;
			}
		}

		// Token: 0x0400129D RID: 4765
		private TreeNode node;

		// Token: 0x0400129E RID: 4766
		private TreeNode parentNode;

		// Token: 0x0400129F RID: 4767
		private GroupTreeNode groupTreeNode;

		// Token: 0x040012A0 RID: 4768
		private IMapObject mapObject;
	}
}
