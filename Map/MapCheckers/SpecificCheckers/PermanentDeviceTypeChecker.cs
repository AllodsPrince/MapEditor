using System;
using System.Collections.Generic;
using MapEditor.Map.MapObjects;
using MapEditor.Resources.Strings;
using Tools.MapObjects;
using Tools.Progress;

namespace MapEditor.Map.MapCheckers.SpecificCheckers
{
	// Token: 0x02000067 RID: 103
	public class PermanentDeviceTypeChecker : MapChecker
	{
		// Token: 0x0600051B RID: 1307 RVA: 0x000286DC File Offset: 0x000276DC
		public PermanentDeviceTypeChecker()
		{
			base.Name = Strings.PERMANENT_DEVICE_TYPE_CHECKER_NAME;
			base.ShortDescription = string.Format("{0}", PermanentDeviceTypeChecker.yellowCount);
			base.LongDescription = string.Format(Strings.PERMANENT_DEVICE_TYPE_CHECKER_LONG_DESCRIPTION, PermanentDeviceTypeChecker.yellowCount);
		}

		// Token: 0x0600051C RID: 1308 RVA: 0x00028730 File Offset: 0x00027730
		public override void Check(MapEditorMap map, IProgressContainer progressContainer)
		{
			if (progressContainer != null)
			{
				progressContainer.AddString(Strings.RUN_PERMAMENT_DEVICE_TYPE_CHECKER);
			}
			base.LongInfoView = new LongInfoViewNode(true);
			foreach (KeyValuePair<int, IMapObject> keyValuePair in map.MapEditorMapObjectContainer.PermanentDeviceContainer.MapObjects)
			{
				PermanentDevice permanentDevice = keyValuePair.Value as PermanentDevice;
				if (permanentDevice != null && !string.IsNullOrEmpty(permanentDevice.Device))
				{
					base.LongInfoView.FindOrAddNode(permanentDevice.Device, false).AddNode(permanentDevice, false);
				}
			}
			int count = base.LongInfoView.BranchCount;
			if (count > PermanentDeviceTypeChecker.redCount)
			{
				base.Status = MapCheckerStatus.Red;
				base.ShortInfo = Strings.CHECKER_EXCEED_RED;
			}
			else if (count > PermanentDeviceTypeChecker.yellowCount)
			{
				base.Status = MapCheckerStatus.Yellow;
				base.ShortInfo = Strings.CHECKER_EXCEED_YELLOW;
			}
			else
			{
				base.Status = MapCheckerStatus.Green;
				base.ShortInfo = Strings.CHECKER_OK;
			}
			base.Status = MapCheckerStatus.Green;
			base.ShortInfo = Strings.CHECKER_OK;
			base.ShortResult = string.Format("{0}", count);
			base.LongResult = string.Format(Strings.PERMANENT_DEVICE_TYPE_CHECKER_LONG_RESULT, count);
			if (progressContainer != null)
			{
				progressContainer.Progress++;
			}
		}

		// Token: 0x040003AD RID: 941
		private static readonly int yellowCount = 100;

		// Token: 0x040003AE RID: 942
		private static readonly int redCount = 150;
	}
}
