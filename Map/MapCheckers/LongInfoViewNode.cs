using System;
using System.Collections.Generic;
using Tools.MapObjects;

namespace MapEditor.Map.MapCheckers
{
	// Token: 0x020002A4 RID: 676
	public class LongInfoViewNode
	{
		// Token: 0x06001F7A RID: 8058 RVA: 0x000C9E28 File Offset: 0x000C8E28
		public LongInfoViewNode(bool _showLeafCount)
		{
			this.showLeafCount = _showLeafCount;
		}

		// Token: 0x06001F7B RID: 8059 RVA: 0x000C9E4D File Offset: 0x000C8E4D
		public LongInfoViewNode(string _key, string _text, bool _showLeafCount)
		{
			this.key = _key;
			this.text = _text;
			this.showLeafCount = _showLeafCount;
		}

		// Token: 0x06001F7C RID: 8060 RVA: 0x000C9E80 File Offset: 0x000C8E80
		public LongInfoViewNode(IMapObject _mapObject, string _text, bool _showLeafCount)
		{
			this.mapObject = _mapObject;
			this.text = _text;
			this.showLeafCount = _showLeafCount;
		}

		// Token: 0x1700072B RID: 1835
		// (get) Token: 0x06001F7D RID: 8061 RVA: 0x000C9EB3 File Offset: 0x000C8EB3
		public int BranchCount
		{
			get
			{
				if (this.nodes == null)
				{
					return 0;
				}
				return this.nodes.Count;
			}
		}

		// Token: 0x1700072C RID: 1836
		// (get) Token: 0x06001F7E RID: 8062 RVA: 0x000C9ECC File Offset: 0x000C8ECC
		public int LeafCount
		{
			get
			{
				if (this.nodes != null)
				{
					int count = 0;
					foreach (LongInfoViewNode node in this.nodes)
					{
						count += node.LeafCount;
					}
					return count;
				}
				return 1;
			}
		}

		// Token: 0x06001F7F RID: 8063 RVA: 0x000C9F30 File Offset: 0x000C8F30
		public int GetBranchCountByChild(string childKey)
		{
			LongInfoViewNode node = this.FindChild(childKey);
			if (node == null)
			{
				return 0;
			}
			return node.BranchCount;
		}

		// Token: 0x06001F80 RID: 8064 RVA: 0x000C9F50 File Offset: 0x000C8F50
		public int GetLeafCountByChild(string childKey)
		{
			LongInfoViewNode node = this.FindChild(childKey);
			if (node == null)
			{
				return 0;
			}
			return node.LeafCount;
		}

		// Token: 0x06001F81 RID: 8065 RVA: 0x000C9F70 File Offset: 0x000C8F70
		public LongInfoViewNode FindChild(string childKey)
		{
			if (this.nodes != null)
			{
				foreach (LongInfoViewNode longInfoNode in this.nodes)
				{
					if (longInfoNode.key == childKey)
					{
						return longInfoNode;
					}
				}
			}
			return null;
		}

		// Token: 0x06001F82 RID: 8066 RVA: 0x000C9FDC File Offset: 0x000C8FDC
		public void AddNode(IMapObject _mapObject, string _text, bool _showLeafCount)
		{
			if (this.nodes == null)
			{
				this.nodes = new List<LongInfoViewNode>();
			}
			this.nodes.Add(new LongInfoViewNode(_mapObject, _text, _showLeafCount));
		}

		// Token: 0x06001F83 RID: 8067 RVA: 0x000CA004 File Offset: 0x000C9004
		public void AddNode(IMapObject _mapObject, bool _showLeafCount)
		{
			if (_mapObject != null)
			{
				this.AddNode(_mapObject, _mapObject.SceneName, _showLeafCount);
			}
		}

		// Token: 0x06001F84 RID: 8068 RVA: 0x000CA017 File Offset: 0x000C9017
		public LongInfoViewNode FindOrAddNode(string childKey, bool _showLeafCount)
		{
			return this.FindOrAddNode(childKey, childKey, _showLeafCount);
		}

		// Token: 0x06001F85 RID: 8069 RVA: 0x000CA024 File Offset: 0x000C9024
		public LongInfoViewNode FindOrAddNode(string childKey, string defaultText, bool _showLeafCount)
		{
			LongInfoViewNode node = this.FindChild(childKey);
			if (node == null)
			{
				if (this.nodes == null)
				{
					this.nodes = new List<LongInfoViewNode>();
				}
				node = new LongInfoViewNode(childKey, defaultText, _showLeafCount);
				this.nodes.Add(node);
			}
			return node;
		}

		// Token: 0x1700072D RID: 1837
		// (get) Token: 0x06001F86 RID: 8070 RVA: 0x000CA065 File Offset: 0x000C9065
		public List<LongInfoViewNode> Nodes
		{
			get
			{
				return this.nodes;
			}
		}

		// Token: 0x06001F87 RID: 8071 RVA: 0x000CA06D File Offset: 0x000C906D
		public List<LongInfoViewNode> GetSortedNodeCollection()
		{
			if (this.nodes != null)
			{
				this.nodes.Sort(LongInfoViewNode.comparer);
			}
			return this.nodes;
		}

		// Token: 0x1700072E RID: 1838
		// (get) Token: 0x06001F88 RID: 8072 RVA: 0x000CA08D File Offset: 0x000C908D
		public string Key
		{
			get
			{
				return this.key;
			}
		}

		// Token: 0x1700072F RID: 1839
		// (get) Token: 0x06001F89 RID: 8073 RVA: 0x000CA095 File Offset: 0x000C9095
		public IMapObject MapObject
		{
			get
			{
				return this.mapObject;
			}
		}

		// Token: 0x06001F8A RID: 8074 RVA: 0x000CA0A0 File Offset: 0x000C90A0
		public override string ToString()
		{
			int count = this.showLeafCount ? this.LeafCount : this.BranchCount;
			if (count <= 0)
			{
				return this.text;
			}
			return string.Format("{0} ({1})", this.text, count);
		}

		// Token: 0x0400136F RID: 4975
		private readonly string key = string.Empty;

		// Token: 0x04001370 RID: 4976
		private readonly string text = string.Empty;

		// Token: 0x04001371 RID: 4977
		private List<LongInfoViewNode> nodes;

		// Token: 0x04001372 RID: 4978
		private readonly IMapObject mapObject;

		// Token: 0x04001373 RID: 4979
		private readonly bool showLeafCount;

		// Token: 0x04001374 RID: 4980
		private static readonly LongInfoViewNode.LongInfoViewNodeComparer comparer = new LongInfoViewNode.LongInfoViewNodeComparer();

		// Token: 0x020002A5 RID: 677
		private class LongInfoViewNodeComparer : IComparer<LongInfoViewNode>
		{
			// Token: 0x06001F8C RID: 8076 RVA: 0x000CA0F1 File Offset: 0x000C90F1
			private static string GetString(LongInfoViewNode node)
			{
				if (node == null || node.text == null)
				{
					return string.Empty;
				}
				return node.text;
			}

			// Token: 0x06001F8D RID: 8077 RVA: 0x000CA10A File Offset: 0x000C910A
			public int Compare(LongInfoViewNode node1, LongInfoViewNode node2)
			{
				return string.Compare(LongInfoViewNode.LongInfoViewNodeComparer.GetString(node1), LongInfoViewNode.LongInfoViewNodeComparer.GetString(node2));
			}
		}
	}
}
