using System;
using System.Drawing;
using System.IO;
using Db;
using Tools.ItemDataContainer;

namespace MapEditor.Map.DataProviders
{
	// Token: 0x02000295 RID: 661
	public class DBObjectItemDataMiner : IItemDataMiner
	{
		// Token: 0x06001EE4 RID: 7908 RVA: 0x000C6CB4 File Offset: 0x000C5CB4
		public static bool IsValid(string item, string _className, bool _searchDerivedClasses)
		{
			if (!item.EndsWith("xdb"))
			{
				return false;
			}
			IDatabase mainDb = IDatabase.GetMainDatabase();
			if (mainDb == null)
			{
				return false;
			}
			bool found = false;
			DBID dbid = mainDb.GetDBIDByName(item);
			string dbidClassName = mainDb.GetClassTypeName(dbid);
			if (_searchDerivedClasses)
			{
				string[] derivedClassNames = mainDb.GetDerivedXDBClassTypes(_className);
				if (derivedClassNames != null && derivedClassNames.Length > 0)
				{
					foreach (string __className in derivedClassNames)
					{
						if (dbidClassName == __className)
						{
							found = true;
							break;
						}
					}
				}
			}
			else
			{
				found = (dbidClassName == _className);
			}
			return found;
		}

		// Token: 0x06001EE5 RID: 7909 RVA: 0x000C6D3B File Offset: 0x000C5D3B
		private bool IsValid(string item)
		{
			return DBObjectItemDataMiner.IsValid(item, this.className, this.searchDerivedClasses);
		}

		// Token: 0x06001EE6 RID: 7910 RVA: 0x000C6D4F File Offset: 0x000C5D4F
		public DBObjectItemDataMiner(string _className, bool _searchDerivedClasses, Bitmap _image)
		{
			this.className = _className;
			this.searchDerivedClasses = _searchDerivedClasses;
			this.image = _image;
		}

		// Token: 0x06001EE7 RID: 7911 RVA: 0x000C6D78 File Offset: 0x000C5D78
		public bool GetItemData(string item, out string text, out string toolTip, out Bitmap smallBitmap, out Bitmap largeBitmap)
		{
			if (this.IsValid(item))
			{
				string _item = item.Replace('\\', '/');
				_item = Str.CutFilePathAndExtention(_item);
				_item = Str.CutFileExtention(_item);
				text = _item;
				toolTip = item;
				Point smallBitmapSize = new Point(16, 16);
				Point largeBitmapSize = new Point(64, 64);
				smallBitmap = new Bitmap(this.image, smallBitmapSize.X, smallBitmapSize.Y);
				largeBitmap = new Bitmap(this.image, largeBitmapSize.X, largeBitmapSize.Y);
				return true;
			}
			text = item;
			toolTip = item;
			smallBitmap = null;
			largeBitmap = null;
			return false;
		}

		// Token: 0x06001EE8 RID: 7912 RVA: 0x000C6E0C File Offset: 0x000C5E0C
		public bool CacheValid(string item, DateTime dateTime, out bool cacheValid)
		{
			cacheValid = false;
			if (this.IsValid(item))
			{
				string filePath = EditorEnvironment.DataFolder + item;
				if (File.Exists(filePath))
				{
					cacheValid = (File.GetLastWriteTime(filePath) < dateTime);
				}
				return true;
			}
			return false;
		}

		// Token: 0x1700071E RID: 1822
		// (get) Token: 0x06001EE9 RID: 7913 RVA: 0x000C6E4A File Offset: 0x000C5E4A
		public string StandardIconKey
		{
			get
			{
				return this.className + "IconKey";
			}
		}

		// Token: 0x1700071F RID: 1823
		// (get) Token: 0x06001EEA RID: 7914 RVA: 0x000C6E5C File Offset: 0x000C5E5C
		public bool Cache
		{
			get
			{
				return false;
			}
		}

		// Token: 0x04001332 RID: 4914
		private readonly string className = string.Empty;

		// Token: 0x04001333 RID: 4915
		private readonly bool searchDerivedClasses;

		// Token: 0x04001334 RID: 4916
		private readonly Bitmap image;
	}
}
