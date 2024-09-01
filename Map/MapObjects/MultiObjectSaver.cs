using System;
using System.Collections.Generic;
using Tools.LinkContainer;
using Tools.MapObjects;

namespace MapEditor.Map.MapObjects
{
	// Token: 0x020001F3 RID: 499
	public class MultiObjectSaver
	{
		// Token: 0x170005FC RID: 1532
		// (get) Token: 0x060018E6 RID: 6374 RVA: 0x000A5550 File Offset: 0x000A4550
		// (set) Token: 0x060018E7 RID: 6375 RVA: 0x000A5558 File Offset: 0x000A4558
		public List<SerializableMapObjectPack> SerializableMapObjectPacks
		{
			get
			{
				return this.serializableMapObjectPacks;
			}
			set
			{
				this.serializableMapObjectPacks = value;
			}
		}

		// Token: 0x170005FD RID: 1533
		// (get) Token: 0x060018E8 RID: 6376 RVA: 0x000A5561 File Offset: 0x000A4561
		// (set) Token: 0x060018E9 RID: 6377 RVA: 0x000A5569 File Offset: 0x000A4569
		public List<LinkDataPack> LinkDataPacks
		{
			get
			{
				return this.linkDataPacks;
			}
			set
			{
				if (value == null)
				{
					this.linkDataPacks.Clear();
					return;
				}
				this.linkDataPacks = value;
			}
		}

		// Token: 0x060018EA RID: 6378 RVA: 0x000A5581 File Offset: 0x000A4581
		public void Clear()
		{
			this.serializableMapObjectPacks.Clear();
			this.linkDataPacks.Clear();
		}

		// Token: 0x060018EB RID: 6379 RVA: 0x000A5599 File Offset: 0x000A4599
		public static void Save(MultiObjectSaver multiObjectSaver, string fileName, bool addToRcs)
		{
			Serializer.Save(fileName, multiObjectSaver, addToRcs);
		}

		// Token: 0x060018EC RID: 6380 RVA: 0x000A55A4 File Offset: 0x000A45A4
		public static MultiObjectSaver Load(string fileName)
		{
			return Serializer.Load<MultiObjectSaver>(fileName);
		}

		// Token: 0x060018ED RID: 6381 RVA: 0x000A55AC File Offset: 0x000A45AC
		public void Pack(List<IMapObject> mapObjects, Dictionary<int, Dictionary<int, ILinkData>> links, bool saveSpecialObjects, bool saveLinks)
		{
			this.Clear();
			if (mapObjects != null)
			{
				Dictionary<int, int> indices = new Dictionary<int, int>(mapObjects.Count);
				int index = 0;
				foreach (IMapObject iMapObject in mapObjects)
				{
					if (iMapObject != null)
					{
						if (saveSpecialObjects || iMapObject.Type.Type == MapObjectFactory.Type.StaticObject)
						{
							MapObject mapObject = iMapObject as MapObject;
							if (mapObject != null)
							{
								SerializableMapObjectPack serializableMapObjectPack = mapObject.Pack() as SerializableMapObjectPack;
								if (serializableMapObjectPack != null)
								{
									indices.Add(index, this.serializableMapObjectPacks.Count);
									this.serializableMapObjectPacks.Add(serializableMapObjectPack);
								}
							}
						}
						index++;
					}
				}
				if (saveLinks && links != null)
				{
					foreach (KeyValuePair<int, Dictionary<int, ILinkData>> leftKeyValuePair in links)
					{
						int leftIndex;
						if (indices.TryGetValue(leftKeyValuePair.Key, out leftIndex))
						{
							foreach (KeyValuePair<int, ILinkData> rightKeyValuePair in leftKeyValuePair.Value)
							{
								int rightIndex;
								if (indices.TryGetValue(rightKeyValuePair.Key, out rightIndex))
								{
									LinkDataPack linkDataPack = new LinkDataPack();
									linkDataPack.LeftIndex = leftIndex;
									linkDataPack.RightIndex = rightIndex;
									linkDataPack.Pack(rightKeyValuePair.Value);
									this.linkDataPacks.Add(linkDataPack);
								}
							}
						}
					}
				}
			}
		}

		// Token: 0x060018EE RID: 6382 RVA: 0x000A5748 File Offset: 0x000A4748
		public void Unpack(List<IMapObject> mapObjects, Dictionary<int, Dictionary<int, ILinkData>> links, ICollisionMap collisionMap, bool shuffleStaticObjects, bool loadSpecialObjects, bool loadLinks)
		{
			if (mapObjects != null)
			{
				mapObjects.Clear();
			}
			if (links != null)
			{
				links.Clear();
			}
			if (mapObjects != null)
			{
				MapObjectFactory factory = new MapObjectFactory();
				Dictionary<int, int> indices = new Dictionary<int, int>(this.serializableMapObjectPacks.Count);
				int index = 0;
				foreach (SerializableMapObjectPack serializableMapObjectPack in this.serializableMapObjectPacks)
				{
					if (serializableMapObjectPack != null && (loadSpecialObjects || serializableMapObjectPack.Type.Type == MapObjectFactory.Type.StaticObject))
					{
						IMapObject mapObject = factory.CreateMapObject(-1, serializableMapObjectPack.Type, collisionMap, true, false);
						serializableMapObjectPack.Unpack(mapObject);
						if (shuffleStaticObjects && serializableMapObjectPack.Type.Type == MapObjectFactory.Type.StaticObject)
						{
							StaticObjectPack staticObjectPack = serializableMapObjectPack as StaticObjectPack;
							StaticObject staticObject = mapObject as StaticObject;
							if (staticObjectPack != null && staticObject != null)
							{
								staticObject.Type = staticObjectPack.MapObjectTypes[MultiObjectSaver.rand.Next(staticObjectPack.MapObjectTypes.Count)];
							}
						}
						indices.Add(index, mapObjects.Count);
						mapObjects.Add(mapObject);
					}
					index++;
				}
				if (loadLinks && links != null)
				{
					foreach (LinkDataPack linkDataPack in this.linkDataPacks)
					{
						int leftIndex;
						int rightIndex;
						if (indices.TryGetValue(linkDataPack.LeftIndex, out leftIndex) && indices.TryGetValue(linkDataPack.RightIndex, out rightIndex))
						{
							Dictionary<int, ILinkData> right;
							if (!links.TryGetValue(leftIndex, out right))
							{
								if (!links.TryGetValue(rightIndex, out right))
								{
									right = new Dictionary<int, ILinkData>();
									links.Add(leftIndex, right);
								}
								else
								{
									rightIndex = leftIndex;
								}
							}
							if (!right.ContainsKey(rightIndex))
							{
								right.Add(rightIndex, linkDataPack.Unpack());
							}
						}
					}
				}
			}
		}

		// Token: 0x0400101E RID: 4126
		private static readonly Random rand = new Random(DateTime.Now.Millisecond);

		// Token: 0x0400101F RID: 4127
		private List<SerializableMapObjectPack> serializableMapObjectPacks = new List<SerializableMapObjectPack>();

		// Token: 0x04001020 RID: 4128
		private List<LinkDataPack> linkDataPacks = new List<LinkDataPack>();
	}
}
