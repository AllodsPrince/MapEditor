using System;
using System.Collections.Generic;

namespace MapEditor.Map.MapCheckers
{
	// Token: 0x020002A7 RID: 679
	public class MapCheckerHelper
	{
		// Token: 0x06001F8F RID: 8079 RVA: 0x000CA128 File Offset: 0x000C9128
		public static void AddItemToItemCount(IDictionary<string, int> items, string item, bool ignoreCase)
		{
			string _item = ignoreCase ? item.ToLower() : item;
			if (items.ContainsKey(_item))
			{
				string key;
				items[key = _item] = items[key] + 1;
				return;
			}
			items.Add(_item, 1);
		}

		// Token: 0x06001F90 RID: 8080 RVA: 0x000CA168 File Offset: 0x000C9168
		public static void AddItemToItemList<Type>(IDictionary<string, List<Type>> items, string item, bool ignoreCase, Type listItem)
		{
			string _item = ignoreCase ? item.ToLower() : item;
			List<Type> listItems;
			if (items.TryGetValue(_item, out listItems))
			{
				listItems.Add(listItem);
				return;
			}
			listItems = new List<Type>();
			listItems.Add(listItem);
			items.Add(_item, listItems);
		}

		// Token: 0x06001F91 RID: 8081 RVA: 0x000CA1AC File Offset: 0x000C91AC
		public static List<string> SortItems(IDictionary<string, int> items)
		{
			if (items != null && items.Count > 0)
			{
				List<string> sortedItems = new List<string>();
				foreach (KeyValuePair<string, int> keyValuePair in items)
				{
					sortedItems.Add(keyValuePair.Key);
				}
				sortedItems.Sort(new MapCheckerHelper.ItemCountComparer(items));
				return sortedItems;
			}
			return null;
		}

		// Token: 0x06001F92 RID: 8082 RVA: 0x000CA21C File Offset: 0x000C921C
		public static List<string> SortItems<Type>(IDictionary<string, List<Type>> items)
		{
			if (items != null && items.Count > 0)
			{
				List<string> sortedItems = new List<string>();
				foreach (KeyValuePair<string, List<Type>> keyValuePair in items)
				{
					sortedItems.Add(keyValuePair.Key);
				}
				sortedItems.Sort(new MapCheckerHelper.ItemListComparer<Type>(items));
				return sortedItems;
			}
			return null;
		}

		// Token: 0x06001F93 RID: 8083 RVA: 0x000CA28C File Offset: 0x000C928C
		public static string FormatTable(int[,] table, int yellowCount, int redCount)
		{
			string result = string.Empty;
			for (int y = table.GetLength(1) - 1; y >= 0; y--)
			{
				string row = string.Empty;
				for (int x = 0; x < table.GetLength(0); x++)
				{
					if (!string.IsNullOrEmpty(row))
					{
						row += "\t";
					}
					if (redCount > 0 && table[x, y] > redCount)
					{
						row += string.Format("{0} **", table[x, y]);
					}
					else if (yellowCount > 0 && table[x, y] > yellowCount)
					{
						row += string.Format("{0} *", table[x, y]);
					}
					else
					{
						row += string.Format("{0}", table[x, y]);
					}
				}
				if (!string.IsNullOrEmpty(result))
				{
					result += "\r\n";
				}
				result += row;
			}
			return result;
		}

		// Token: 0x020002A8 RID: 680
		private class ItemCountComparer : IComparer<string>
		{
			// Token: 0x06001F95 RID: 8085 RVA: 0x000CA38C File Offset: 0x000C938C
			public ItemCountComparer(IDictionary<string, int> _items)
			{
				this.items = _items;
			}

			// Token: 0x06001F96 RID: 8086 RVA: 0x000CA39C File Offset: 0x000C939C
			public int Compare(string left, string right)
			{
				int _left;
				int _right;
				if (this.items != null && (!string.IsNullOrEmpty(left) & !string.IsNullOrEmpty(right)) && this.items.TryGetValue(left, out _left) && this.items.TryGetValue(right, out _right))
				{
					if (_left > _right)
					{
						return -1;
					}
					if (_left < _right)
					{
						return 1;
					}
				}
				return string.Compare(left, right);
			}

			// Token: 0x04001379 RID: 4985
			private readonly IDictionary<string, int> items;
		}

		// Token: 0x020002A9 RID: 681
		private class ItemListComparer<Type> : IComparer<string>
		{
			// Token: 0x06001F97 RID: 8087 RVA: 0x000CA3F9 File Offset: 0x000C93F9
			public ItemListComparer(IDictionary<string, List<Type>> _items)
			{
				this.items = _items;
			}

			// Token: 0x06001F98 RID: 8088 RVA: 0x000CA408 File Offset: 0x000C9408
			public int Compare(string left, string right)
			{
				List<Type> _left;
				List<Type> _right;
				if (this.items != null && (!string.IsNullOrEmpty(left) & !string.IsNullOrEmpty(right)) && this.items.TryGetValue(left, out _left) && this.items.TryGetValue(right, out _right) && _left != null && _right != null)
				{
					if (_left.Count > _right.Count)
					{
						return -1;
					}
					if (_left.Count < _right.Count)
					{
						return 1;
					}
				}
				return string.Compare(left, right);
			}

			// Token: 0x0400137A RID: 4986
			private readonly IDictionary<string, List<Type>> items;
		}
	}
}
