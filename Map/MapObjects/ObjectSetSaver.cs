using System;
using System.Collections.Generic;
using Tools.WeightList;

namespace MapEditor.Map.MapObjects
{
	// Token: 0x020001F4 RID: 500
	public class ObjectSetSaver
	{
		// Token: 0x170005FE RID: 1534
		// (get) Token: 0x060018F1 RID: 6385 RVA: 0x000A5976 File Offset: 0x000A4976
		// (set) Token: 0x060018F2 RID: 6386 RVA: 0x000A597E File Offset: 0x000A497E
		public WeightList<string> Objects
		{
			get
			{
				return this.objects;
			}
			set
			{
				if (value == null)
				{
					this.objects.Clear();
					return;
				}
				this.objects = value;
			}
		}

		// Token: 0x060018F3 RID: 6387 RVA: 0x000A5996 File Offset: 0x000A4996
		public void Clear()
		{
			this.objects.Clear();
		}

		// Token: 0x060018F4 RID: 6388 RVA: 0x000A59A4 File Offset: 0x000A49A4
		public static void Autocomplete(ItemList.IItemSource itemSource, WeightList<string> _objects)
		{
			if (itemSource != null && _objects != null)
			{
				IEnumerable<string> staticObjects = itemSource.Items;
				WeightList<string> itemTemplates = new WeightList<string>();
				foreach (WeightList<string>.WeightItem item in _objects.Items)
				{
					string itemTempalte = item.Item;
					itemTempalte = Str.CutFilePoint(itemTempalte);
					itemTempalte = itemTempalte.TrimEnd(ObjectSetSaver.numbers);
					itemTemplates.Add(itemTempalte, item.Weight, item.Name);
				}
				_objects.Clear();
				foreach (string staticObject in staticObjects)
				{
					string staticObjectTemplate = staticObject.TrimEnd(ObjectSetSaver.numbers);
					staticObjectTemplate = Str.CutFilePoint(staticObjectTemplate);
					staticObjectTemplate = staticObjectTemplate.TrimEnd(ObjectSetSaver.numbers);
					foreach (WeightList<string>.WeightItem itemTemplate in itemTemplates.Items)
					{
						if (itemTemplate.Item == staticObjectTemplate)
						{
							_objects.Add(staticObject, itemTemplate.Weight, itemTemplate.Name);
						}
					}
				}
			}
		}

		// Token: 0x060018F5 RID: 6389 RVA: 0x000A5B04 File Offset: 0x000A4B04
		public void Autocomplete(ItemList.IItemSource itemSource)
		{
			ObjectSetSaver.Autocomplete(itemSource, this.objects);
		}

		// Token: 0x060018F6 RID: 6390 RVA: 0x000A5B12 File Offset: 0x000A4B12
		public static void Save(ObjectSetSaver objectSetSaver, string fileName, bool addToRcs)
		{
			Serializer.Save(fileName, objectSetSaver, addToRcs);
		}

		// Token: 0x060018F7 RID: 6391 RVA: 0x000A5B1D File Offset: 0x000A4B1D
		public static ObjectSetSaver Load(string fileName)
		{
			return Serializer.Load<ObjectSetSaver>(fileName);
		}

		// Token: 0x060018F8 RID: 6392 RVA: 0x000A5B28 File Offset: 0x000A4B28
		public static ObjectSetSaver Create(string objectName, ItemList.IItemSource itemSource, bool allowTrivial)
		{
			ObjectSetSaver objectSetSaver = new ObjectSetSaver();
			objectSetSaver.objects.Add(objectName, 1);
			objectSetSaver.Autocomplete(itemSource);
			if (allowTrivial || objectSetSaver.objects.Items.Count > 0)
			{
				return objectSetSaver;
			}
			return null;
		}

		// Token: 0x04001021 RID: 4129
		private static readonly char[] numbers = "0123456789_".ToCharArray();

		// Token: 0x04001022 RID: 4130
		private WeightList<string> objects = new WeightList<string>();
	}
}
