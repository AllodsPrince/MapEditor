using System;
using System.Collections.Generic;
using MapEditor.Map.MapObjects;
using MapEditor.Resources.Strings;
using Tools.MapObjects;
using Tools.Progress;

namespace MapEditor.Map.MapCheckers.SpecificCheckers
{
	// Token: 0x02000091 RID: 145
	public class ScanRadiusUsageChecker : MapChecker
	{
		// Token: 0x060006E8 RID: 1768 RVA: 0x00036044 File Offset: 0x00035044
		public ScanRadiusUsageChecker()
		{
			base.Name = Strings.SCAN_RADIUS_CHECKER_NAME;
			base.ShortDescription = string.Format("{0}", ScanRadiusUsageChecker.yellowCount);
			base.LongDescription = string.Format(Strings.SCAN_RADIUS_CHECKER_LONG_DESCRIPTION, ScanRadiusUsageChecker.yellowCount);
		}

		// Token: 0x060006E9 RID: 1769 RVA: 0x00036098 File Offset: 0x00035098
		public override void Check(MapEditorMap map, IProgressContainer progressContainer)
		{
			if (progressContainer != null)
			{
				progressContainer.AddString(Strings.RUN_SCAN_RADIUS_CHECKER);
			}
			base.LongInfoView = new LongInfoViewNode(true);
			int count = 0;
			foreach (KeyValuePair<int, IMapObject> keyValuePair in map.MapEditorMapObjectContainer.MapLocatorContainer.MapObjects)
			{
				MapLocator mapLocator = keyValuePair.Value as MapLocator;
				if (mapLocator != null && mapLocator.ScanRadius > 0.0)
				{
					count++;
					base.LongInfoView.FindOrAddNode(MapObjectInterface.GetInterfaceSingleObjectTypeName(mapLocator), false).AddNode(mapLocator, false);
				}
			}
			foreach (KeyValuePair<int, IMapObject> keyValuePair2 in map.MapEditorMapObjectContainer.PermanentDeviceContainer.MapObjects)
			{
				PermanentDevice permanentDevice = keyValuePair2.Value as PermanentDevice;
				if (permanentDevice != null && permanentDevice.ScanRadius > 0.0)
				{
					count++;
					base.LongInfoView.FindOrAddNode(MapObjectInterface.GetInterfaceSingleObjectTypeName(permanentDevice), false).AddNode(permanentDevice, false);
				}
			}
			foreach (KeyValuePair<int, IMapObject> keyValuePair3 in map.MapEditorMapObjectContainer.SpawnPointContainer.MapObjects)
			{
				SpawnPoint spawnPoint = keyValuePair3.Value as SpawnPoint;
				if (spawnPoint != null && spawnPoint.ScanRadius > 0.0)
				{
					count++;
					base.LongInfoView.FindOrAddNode(MapObjectInterface.GetInterfaceSingleObjectTypeName(spawnPoint), false).AddNode(spawnPoint, false);
				}
			}
			foreach (KeyValuePair<int, IMapObject> keyValuePair4 in map.MapEditorMapObjectContainer.ScriptAreaContainer.MapObjects)
			{
				ScriptArea scriptArea = keyValuePair4.Value as ScriptArea;
				if (scriptArea != null && scriptArea.ScanRadius > 0.0)
				{
					count++;
					base.LongInfoView.FindOrAddNode(MapObjectInterface.GetInterfaceSingleObjectTypeName(scriptArea), false).AddNode(scriptArea, false);
				}
			}
			if (count > ScanRadiusUsageChecker.redCount)
			{
				base.Status = MapCheckerStatus.Red;
				base.ShortInfo = Strings.CHECKER_EXCEED_RED;
			}
			else if (count > ScanRadiusUsageChecker.yellowCount)
			{
				base.Status = MapCheckerStatus.Yellow;
				base.ShortInfo = Strings.CHECKER_EXCEED_YELLOW;
			}
			else
			{
				base.Status = MapCheckerStatus.Green;
				base.ShortInfo = Strings.CHECKER_OK;
			}
			base.ShortResult = string.Format("{0}", count);
			base.LongResult = string.Format(Strings.SCAN_RADIUS_CHECKER_LONG_RESULT, count);
			if (progressContainer != null)
			{
				progressContainer.Progress++;
			}
		}

		// Token: 0x040004F3 RID: 1267
		private static readonly int yellowCount = 5;

		// Token: 0x040004F4 RID: 1268
		private static readonly int redCount = 7;
	}
}
