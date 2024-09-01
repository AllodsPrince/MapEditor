using System;
using System.Collections.Generic;
using MapEditor.Map.MapObjects;
using MapEditor.Resources.Strings;
using Tools.MapObjects;
using Tools.Progress;

namespace MapEditor.Map.MapCheckers.SpecificCheckers
{
	// Token: 0x02000201 RID: 513
	public class ScriptIDChecker : MapChecker
	{
		// Token: 0x06001970 RID: 6512 RVA: 0x000A681B File Offset: 0x000A581B
		public ScriptIDChecker()
		{
			base.Name = Strings.SCRIPT_ID_CHECKER_NAME;
			base.ShortDescription = Strings.SCRIPT_ID_CHECKER_SHORT_DESCRIPTION;
			base.LongDescription = Strings.SCRIPT_ID_CHECKER_LONG_DESCRIPTION;
		}

		// Token: 0x06001971 RID: 6513 RVA: 0x000A6844 File Offset: 0x000A5844
		public override void Check(MapEditorMap map, IProgressContainer progressContainer)
		{
			if (progressContainer != null)
			{
				progressContainer.AddString(Strings.RUN_SCRIPT_ID_CHECKER);
			}
			base.LongInfoView = new LongInfoViewNode(true);
			foreach (KeyValuePair<int, IMapObject> keyValuePair in map.MapEditorMapObjectContainer.MapLocatorContainer.MapObjects)
			{
				MapLocator mapLocator = keyValuePair.Value as MapLocator;
				if (mapLocator != null && !string.IsNullOrEmpty(mapLocator.ScriptID))
				{
					base.LongInfoView.FindOrAddNode(mapLocator.ScriptID, false).AddNode(mapLocator, false);
				}
			}
			foreach (KeyValuePair<int, IMapObject> keyValuePair2 in map.MapEditorMapObjectContainer.PermanentDeviceContainer.MapObjects)
			{
				PermanentDevice permanentDevice = keyValuePair2.Value as PermanentDevice;
				if (permanentDevice != null && !string.IsNullOrEmpty(permanentDevice.ScriptID))
				{
					base.LongInfoView.FindOrAddNode(permanentDevice.ScriptID, false).AddNode(permanentDevice, false);
				}
			}
			foreach (KeyValuePair<int, IMapObject> keyValuePair3 in map.MapEditorMapObjectContainer.SpawnPointContainer.MapObjects)
			{
				SpawnPoint spawnPoint = keyValuePair3.Value as SpawnPoint;
				if (spawnPoint != null && !string.IsNullOrEmpty(spawnPoint.ScriptID))
				{
					base.LongInfoView.FindOrAddNode(spawnPoint.ScriptID, false).AddNode(spawnPoint, false);
				}
			}
			foreach (KeyValuePair<int, IMapObject> keyValuePair4 in map.MapEditorMapObjectContainer.ScriptAreaContainer.MapObjects)
			{
				ScriptArea scriptArea = keyValuePair4.Value as ScriptArea;
				if (scriptArea != null && !string.IsNullOrEmpty(scriptArea.ScriptID))
				{
					base.LongInfoView.FindOrAddNode(scriptArea.ScriptID, false).AddNode(scriptArea, false);
				}
			}
			bool unique = true;
			base.LongInfoText = string.Empty;
			int count = 0;
			if (base.LongInfoView.Nodes != null)
			{
				foreach (LongInfoViewNode node in base.LongInfoView.Nodes)
				{
					int _count = node.BranchCount;
					if (_count > 1)
					{
						unique = false;
						if (!string.IsNullOrEmpty(base.LongInfoText))
						{
							base.LongInfoText += "\r\n";
						}
						base.LongInfoText += string.Format(Strings.SCRIPT_ID_CHECKER_LONG_INFO_NOT_UNIQUE, node.Key, _count);
					}
					count += _count;
				}
			}
			if (!unique)
			{
				base.Status = MapCheckerStatus.Red;
				base.ShortInfo = Strings.SCRIPT_ID_CHECKER_SHORT_INFO_NOT_UNIQUE;
				base.LongResult = string.Format(Strings.SCRIPT_ID_CHECKER_LONG_RESULT_NOT_UNIQUE, count, base.LongInfoText);
			}
			else
			{
				base.Status = MapCheckerStatus.Green;
				base.ShortInfo = Strings.CHECKER_OK;
				base.LongResult = string.Format(Strings.SCRIPT_ID_CHECKER_LONG_RESULT, count);
			}
			base.ShortResult = string.Format("{0}", count);
			if (progressContainer != null)
			{
				progressContainer.Progress++;
			}
		}
	}
}
