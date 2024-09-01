using System;
using System.Collections.Generic;
using System.IO;
using Db;
using Tools.Geometry;
using Tools.MapObjects;
using Tools.SafeObjMan;

namespace MapEditor.Map
{
	// Token: 0x02000126 RID: 294
	public class Constants
	{
		// Token: 0x170002B2 RID: 690
		// (get) Token: 0x06000E85 RID: 3717 RVA: 0x0007609A File Offset: 0x0007509A
		public static Rect WorldBounds
		{
			get
			{
				return Constants.worldBounds;
			}
		}

		// Token: 0x170002B3 RID: 691
		// (get) Token: 0x06000E86 RID: 3718 RVA: 0x000760A1 File Offset: 0x000750A1
		public static int PatchSize
		{
			get
			{
				return Constants.patchSize;
			}
		}

		// Token: 0x170002B4 RID: 692
		// (get) Token: 0x06000E87 RID: 3719 RVA: 0x000760A8 File Offset: 0x000750A8
		public static int ZoneGranularity
		{
			get
			{
				return Constants.zoneGranularity;
			}
		}

		// Token: 0x170002B5 RID: 693
		// (get) Token: 0x06000E88 RID: 3720 RVA: 0x000760AF File Offset: 0x000750AF
		public static Point MapZonePieceSize
		{
			get
			{
				return Constants.mapZonePieceSize;
			}
		}

		// Token: 0x06000E89 RID: 3721 RVA: 0x000760B6 File Offset: 0x000750B6
		public static string ContinentFolder(string continentName)
		{
			return continentName + "/";
		}

		// Token: 0x06000E8A RID: 3722 RVA: 0x000760C4 File Offset: 0x000750C4
		public static string PatchFolder(string continentName, int x, int y)
		{
			int localX = x % 10;
			int localY = y % 10;
			return string.Format("{0}{1:000}_{2:000}/{3}_{4}_", new object[]
			{
				Constants.ContinentFolder(continentName),
				x - localX,
				y - localY,
				localX,
				localY
			});
		}

		// Token: 0x06000E8B RID: 3723 RVA: 0x0007611E File Offset: 0x0007511E
		public static string PatchFolder(string continentName, Point point)
		{
			return Constants.PatchFolder(continentName, point.X, point.Y);
		}

		// Token: 0x06000E8C RID: 3724 RVA: 0x00076134 File Offset: 0x00075134
		public static Position PatchCenter(Point point)
		{
			return new Position((double)(point.X * Constants.patchSize) + (double)Constants.patchSize / 2.0, (double)(point.X * Constants.patchSize) + (double)Constants.patchSize / 2.0, 0.0);
		}

		// Token: 0x06000E8D RID: 3725 RVA: 0x0007618D File Offset: 0x0007518D
		public static Position PatchMinXMinY(Point point)
		{
			return new Position((double)(point.X * Constants.patchSize), (double)(point.Y * Constants.patchSize), 0.0);
		}

		// Token: 0x06000E8E RID: 3726 RVA: 0x000761B9 File Offset: 0x000751B9
		public static int PatchIndex(double value)
		{
			if (value >= 0.0)
			{
				return (int)(value / (double)Constants.patchSize);
			}
			return (int)(value / (double)Constants.patchSize) - 1;
		}

		// Token: 0x06000E8F RID: 3727 RVA: 0x000761DC File Offset: 0x000751DC
		public static Position PatchesCenter(Rect patches)
		{
			return new Position(((double)patches.Min.X + (double)patches.Width / 2.0) * (double)Constants.patchSize, ((double)patches.Min.Y + (double)patches.Height / 2.0) * (double)Constants.patchSize, 0.0);
		}

		// Token: 0x170002B6 RID: 694
		// (get) Token: 0x06000E90 RID: 3728 RVA: 0x0007624B File Offset: 0x0007524B
		public static Point MegamapSize
		{
			get
			{
				return Constants.megamapSize;
			}
		}

		// Token: 0x06000E91 RID: 3729 RVA: 0x00076252 File Offset: 0x00075252
		public static string MegamapLocalPath(string continentName)
		{
			return Constants.megamapLocalFolder + continentName.Replace('/', '_').Replace('\\', '_') + ".bmp";
		}

		// Token: 0x06000E92 RID: 3730 RVA: 0x00076276 File Offset: 0x00075276
		public static string MegamapCommonPath(string continentName)
		{
			return Constants.megamapCommonFolder + continentName.Replace('/', '_').Replace('\\', '_') + ".bmp";
		}

		// Token: 0x06000E93 RID: 3731 RVA: 0x0007629C File Offset: 0x0007529C
		public static string GetLastChangedMegamapPath(string continentName)
		{
			string megamapLocalFilePath = Constants.MegamapLocalPath(continentName);
			string megamapCommonFilePath = Constants.MegamapCommonPath(continentName);
			string path = megamapLocalFilePath;
			if (File.Exists(megamapCommonFilePath))
			{
				if (File.Exists(megamapLocalFilePath))
				{
					if (File.GetLastWriteTime(megamapLocalFilePath) < File.GetLastWriteTime(megamapCommonFilePath))
					{
						path = megamapCommonFilePath;
					}
				}
				else
				{
					path = megamapCommonFilePath;
				}
			}
			return path;
		}

		// Token: 0x06000E94 RID: 3732 RVA: 0x000762E4 File Offset: 0x000752E4
		public static int GetMapNameList(ref List<string> mapNameList)
		{
			IDatabase mainDb = IDatabase.GetMainDatabase();
			List<DBID> mapResourceList = mainDb.GetObjectsList("mapLoader.MapResource");
			int result = 0;
			foreach (DBID mapResource in mapResourceList)
			{
				string mapName = mapResource.ToString();
				mapName = mapName.Replace('\\', '/');
				if (!mapName.ToLower().Contains("system/corks"))
				{
					mapNameList.Add(mapName);
				}
				result++;
			}
			return result;
		}

		// Token: 0x06000E95 RID: 3733 RVA: 0x00076378 File Offset: 0x00075378
		public static int GetContinentNameList(ref List<string> continentNameList)
		{
			IDatabase mainDb = IDatabase.GetMainDatabase();
			List<DBID> continentResourceList = mainDb.GetObjectsList("mapLoader.MapResource");
			int result = 0;
			foreach (DBID continentResource in continentResourceList)
			{
				string continentName = continentResource.ToString();
				continentName = continentName.Replace('\\', '/');
				continentName = Str.CutFileName(continentName, false);
				if (!continentName.ToLower().Contains("system/corks"))
				{
					continentNameList.Add(continentName);
				}
				result++;
			}
			return result;
		}

		// Token: 0x06000E96 RID: 3734 RVA: 0x00076414 File Offset: 0x00075414
		public static ContinentType GetContinentType(string mapResourceName)
		{
			IDatabase mainDb = IDatabase.GetMainDatabase();
			if (mainDb == null)
			{
				return ContinentType.Unknown;
			}
			bool hasPatches = true;
			DBID mapDBID = mainDb.GetDBIDByName(mapResourceName);
			IObjMan mapMan = mainDb.GetManipulator(mapDBID);
			if (mapMan != null)
			{
				DBID mapTemplateDBID = mainDb.GetDBIDByName(SafeObjMan.GetDBID(mapMan, "mapTemplate"));
				IObjMan mapTemplateMan = mainDb.GetManipulator(mapTemplateDBID);
				if (mapTemplateMan != null)
				{
					hasPatches = SafeObjMan.GetBool(mapTemplateMan, "hasPatches");
				}
			}
			if (!hasPatches)
			{
				return ContinentType.AstralHub;
			}
			return ContinentType.Continent;
		}

		// Token: 0x06000E97 RID: 3735 RVA: 0x00076478 File Offset: 0x00075478
		public static string GetServerPrefix(string mapResourceName)
		{
			IDatabase mainDb = IDatabase.GetMainDatabase();
			if (mainDb != null)
			{
				DBID mapDBID = mainDb.GetDBIDByName(mapResourceName);
				IObjMan mapMan = mainDb.GetManipulator(mapDBID);
				if (mapMan != null)
				{
					return SafeObjMan.GetString(mapMan, "mapPrefix");
				}
			}
			return string.Empty;
		}

		// Token: 0x06000E98 RID: 3736 RVA: 0x000764B4 File Offset: 0x000754B4
		public static double GetContinentMapSizeInPatches(string mapResourceName)
		{
			IDatabase mainDb = IDatabase.GetMainDatabase();
			if (mainDb != null)
			{
				DBID mapDBID = mainDb.GetDBIDByName(mapResourceName);
				IObjMan mapMan = mainDb.GetManipulator(mapDBID);
				if (mapMan != null)
				{
					DBID mapTemplateDBID = mainDb.GetDBIDByName(SafeObjMan.GetDBID(mapMan, "mapTemplate"));
					IObjMan mapTemplateMan = mainDb.GetManipulator(mapTemplateDBID);
					if (mapTemplateMan != null && !SafeObjMan.GetBool(mapTemplateMan, "hasPatches"))
					{
						return SafeObjMan.GetDouble(mapTemplateMan, "mapSize");
					}
				}
			}
			return 4.0;
		}

		// Token: 0x04000B3F RID: 2879
		public const int defaultMapSize = 4;

		// Token: 0x04000B40 RID: 2880
		private static readonly int worldSize = 100;

		// Token: 0x04000B41 RID: 2881
		private static readonly Rect worldBounds = new Rect(0, 0, Constants.worldSize, Constants.worldSize);

		// Token: 0x04000B42 RID: 2882
		private static readonly int patchSize = 256;

		// Token: 0x04000B43 RID: 2883
		private static readonly int zoneGranularity = 16;

		// Token: 0x04000B44 RID: 2884
		private static readonly Point mapZonePieceSize = new Point(16, 16);

		// Token: 0x04000B45 RID: 2885
		private static readonly Point megamapSize = new Point(1024, 1024);

		// Token: 0x04000B46 RID: 2886
		private static readonly string megamapLocalFolder = EditorEnvironment.CommonEditorFolder + "CommonFolder\\Megamaps\\Local\\";

		// Token: 0x04000B47 RID: 2887
		private static readonly string megamapCommonFolder = EditorEnvironment.CommonEditorFolder + "CommonFolder\\Megamaps\\Common\\";
	}
}
