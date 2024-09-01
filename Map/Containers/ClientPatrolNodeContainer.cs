using System;
using System.Collections.Generic;
using MapEditor.Map.MapObjects;
using Tools.LinkContainer;
using Tools.MapObjects;

namespace MapEditor.Map.Containers
{
	// Token: 0x02000215 RID: 533
	public class ClientPatrolNodeContainer : TypedMapObjectContainer
	{
		// Token: 0x060019D6 RID: 6614 RVA: 0x000A80B4 File Offset: 0x000A70B4
		private void OnClientPatrolNodeSceneChanged(ClientPatrolNode clientPatrolNode, ref string oldValue, ref string newValue)
		{
			if (this.mapEditorMapObjectContainer != null && base.Contains(clientPatrolNode))
			{
				if (this.ClientPatrolNodeSceneChanged != null)
				{
					this.ClientPatrolNodeSceneChanged(this.mapEditorMapObjectContainer, clientPatrolNode, ref oldValue, ref newValue);
				}
				if (!clientPatrolNode.Temporary)
				{
					this.mapEditorMapObjectContainer.InvokeModified();
				}
			}
		}

		// Token: 0x060019D7 RID: 6615 RVA: 0x000A8104 File Offset: 0x000A7104
		private void OnClientPatrolNodeScriptIDChanged(ClientPatrolNode clientPatrolNode, ref string oldValue, ref string newValue)
		{
			if (this.mapEditorMapObjectContainer != null && base.Contains(clientPatrolNode))
			{
				if (this.ClientPatrolNodeScriptIDChanged != null)
				{
					this.ClientPatrolNodeScriptIDChanged(this.mapEditorMapObjectContainer, clientPatrolNode, ref oldValue, ref newValue);
				}
				if (!clientPatrolNode.Temporary)
				{
					this.mapEditorMapObjectContainer.InvokeModified();
				}
			}
		}

		// Token: 0x140000B5 RID: 181
		// (add) Token: 0x060019D8 RID: 6616 RVA: 0x000A8151 File Offset: 0x000A7151
		// (remove) Token: 0x060019D9 RID: 6617 RVA: 0x000A816A File Offset: 0x000A716A
		public event ClientPatrolNodeContainer.ClientPatrolNodeFieldChangedEvent<string> ClientPatrolNodeSceneChanged;

		// Token: 0x140000B6 RID: 182
		// (add) Token: 0x060019DA RID: 6618 RVA: 0x000A8183 File Offset: 0x000A7183
		// (remove) Token: 0x060019DB RID: 6619 RVA: 0x000A819C File Offset: 0x000A719C
		public event ClientPatrolNodeContainer.ClientPatrolNodeFieldChangedEvent<string> ClientPatrolNodeScriptIDChanged;

		// Token: 0x060019DC RID: 6620 RVA: 0x000A81B8 File Offset: 0x000A71B8
		public ClientPatrolNodeContainer(MapEditorMapObjectContainer _mapEditorMapObjectContainer) : base(MapObjectFactory.Type.ClientPatrolNode, false)
		{
			this.mapEditorMapObjectContainer = _mapEditorMapObjectContainer;
			if (this.mapEditorMapObjectContainer != null)
			{
				ClientPatrolNode.SceneChanged += this.OnClientPatrolNodeSceneChanged;
				ClientPatrolNode.ScriptIDChanged += this.OnClientPatrolNodeScriptIDChanged;
				base.Destroyed += this.OnDestroyed;
			}
		}

		// Token: 0x060019DD RID: 6621 RVA: 0x000A8214 File Offset: 0x000A7214
		public void OnDestroyed(TypedMapObjectContainer _typedMapObjectContainer)
		{
			if (this.mapEditorMapObjectContainer != null)
			{
				ClientPatrolNode.SceneChanged -= this.OnClientPatrolNodeSceneChanged;
				ClientPatrolNode.ScriptIDChanged -= this.OnClientPatrolNodeScriptIDChanged;
				base.Destroyed -= this.OnDestroyed;
				this.mapEditorMapObjectContainer = null;
			}
		}

		// Token: 0x060019DE RID: 6622 RVA: 0x000A8264 File Offset: 0x000A7264
		private IMapObject GetStartClientPatrolNode(IMapObject middle, IMapObject ignore)
		{
			if (middle != null && ignore != null)
			{
				Dictionary<IMapObject, ILinkData> links = this.mapEditorMapObjectContainer.GetLinks(middle);
				IMapObject next = MapObjectContainer.GetOtherLinkedMapObject(links, MapObjectFactory.Type.ClientPatrolNode, ignore, true);
				if (next != null)
				{
					return this.GetStartClientPatrolNode(next, middle);
				}
			}
			return middle;
		}

		// Token: 0x060019DF RID: 6623 RVA: 0x000A82A0 File Offset: 0x000A72A0
		private ClientPatrolNode GetStartClientPatrolNode(ClientPatrolNode middle)
		{
			if (middle != null)
			{
				Dictionary<IMapObject, ILinkData> links = this.mapEditorMapObjectContainer.GetLinks(middle);
				if (links != null && links.Count > 0)
				{
					IMapObject left = MapObjectContainer.GetOtherLinkedMapObject(links, MapObjectFactory.Type.ClientPatrolNode, null, true);
					IMapObject right = MapObjectContainer.GetOtherLinkedMapObject(links, MapObjectFactory.Type.ClientPatrolNode, left, true);
					if (left != null)
					{
						left = this.GetStartClientPatrolNode(left, middle);
					}
					else
					{
						left = middle;
					}
					if (right != null)
					{
						right = this.GetStartClientPatrolNode(right, middle);
					}
					else
					{
						right = middle;
					}
					ClientPatrolNode _left = left as ClientPatrolNode;
					ClientPatrolNode _right = right as ClientPatrolNode;
					if (_left != null && !string.IsNullOrEmpty(_left.ScriptID))
					{
						return _left;
					}
					return _right;
				}
			}
			return middle;
		}

		// Token: 0x060019E0 RID: 6624 RVA: 0x000A832C File Offset: 0x000A732C
		public void GetLocator(ClientPatrolNode clientPatrolNode, out string scriptID, out List<ClientPatrolNode> clientPatrolNodes)
		{
			scriptID = string.Empty;
			clientPatrolNodes = null;
			if (clientPatrolNode != null)
			{
				ClientPatrolNode current = this.GetStartClientPatrolNode(clientPatrolNode);
				if (current != null)
				{
					clientPatrolNodes = new List<ClientPatrolNode>();
					scriptID = current.ScriptID;
					IMapObject previous = null;
					while (current != null)
					{
						clientPatrolNodes.Add(current);
						Dictionary<IMapObject, ILinkData> links = this.mapEditorMapObjectContainer.GetLinks(current);
						ClientPatrolNode next = MapObjectContainer.GetOtherLinkedMapObject(links, MapObjectFactory.Type.ClientPatrolNode, previous, true) as ClientPatrolNode;
						previous = current;
						current = next;
					}
				}
			}
		}

		// Token: 0x04001067 RID: 4199
		private MapEditorMapObjectContainer mapEditorMapObjectContainer;

		// Token: 0x02000216 RID: 534
		// (Invoke) Token: 0x060019E2 RID: 6626
		public delegate void ClientPatrolNodeFieldChangedEvent<T>(MapEditorMapObjectContainer _mapEditorMapObjectContainer, ClientPatrolNode clientPatrolNode, ref T oldValue, ref T newValue);
	}
}
