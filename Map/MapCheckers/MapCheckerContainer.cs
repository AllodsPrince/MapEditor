using System;
using System.Collections.Generic;
using System.Windows.Forms;
using MapEditor.Resources.Strings;
using Tools.Progress;
using Win32;

namespace MapEditor.Map.MapCheckers
{
	// Token: 0x020002AA RID: 682
	public class MapCheckerContainer
	{
		// Token: 0x06001F99 RID: 8089 RVA: 0x000CA47F File Offset: 0x000C947F
		public MapCheckerContainer(MapEditorMap _map)
		{
			this.map = _map;
		}

		// Token: 0x17000730 RID: 1840
		// (get) Token: 0x06001F9A RID: 8090 RVA: 0x000CA4A4 File Offset: 0x000C94A4
		public List<IMapChecker> MapCheckers
		{
			get
			{
				return this.mapCheckers;
			}
		}

		// Token: 0x06001F9B RID: 8091 RVA: 0x000CA4AC File Offset: 0x000C94AC
		public int GetCheckProgressCount()
		{
			int progressCount = 0;
			foreach (IMapChecker mapChecker in this.mapCheckers)
			{
				progressCount += mapChecker.GetCheckProgressSteps();
			}
			return progressCount;
		}

		// Token: 0x06001F9C RID: 8092 RVA: 0x000CA504 File Offset: 0x000C9504
		public int GetFixProgressCount()
		{
			int progressCount = 0;
			foreach (IMapChecker mapChecker in this.mapCheckers)
			{
				progressCount += mapChecker.GetFixProgressSteps();
			}
			return progressCount;
		}

		// Token: 0x06001F9D RID: 8093 RVA: 0x000CA55C File Offset: 0x000C955C
		public void Check(IMapChecker mapChecker, Form parentForm)
		{
			if (mapChecker != null && mapChecker.GetCheckProgressSteps() > 0)
			{
				this.progressForm.Create(Strings.RUN_MAP_CHECKERS);
				this.progressForm.ProgressCount = mapChecker.GetCheckProgressSteps();
				this.progressForm.Progress = 0;
				int time = Kernel.GetTickCount();
				int timeTotal = time;
				mapChecker.Check(this.map, this.progressForm);
				timeTotal = Kernel.GetTickCount() - timeTotal;
				Console.WriteLine("Map check complete : {0} ms", timeTotal);
				this.progressForm.Delete();
			}
		}

		// Token: 0x06001F9E RID: 8094 RVA: 0x000CA5E0 File Offset: 0x000C95E0
		public void CheckAll(Form parentForm)
		{
			int progressCount = this.GetCheckProgressCount();
			if (progressCount > 0)
			{
				this.progressForm.Create(Strings.RUN_MAP_CHECKERS);
				this.progressForm.ProgressCount = progressCount;
				this.progressForm.Progress = 0;
				int time = Kernel.GetTickCount();
				int timeTotal = time;
				foreach (IMapChecker mapChecker in this.mapCheckers)
				{
					if (mapChecker != null && mapChecker.GetCheckProgressSteps() > 0)
					{
						mapChecker.Check(this.map, this.progressForm);
					}
				}
				timeTotal = Kernel.GetTickCount() - timeTotal;
				Console.WriteLine("Map check complete : {0} ms", timeTotal);
				this.progressForm.Delete();
			}
		}

		// Token: 0x06001F9F RID: 8095 RVA: 0x000CA6AC File Offset: 0x000C96AC
		public void Fix(IMapChecker mapChecker, Form parentForm)
		{
			if (mapChecker != null && mapChecker.GetFixProgressSteps() > 0)
			{
				this.progressForm.Create(Strings.RUN_MAP_CHECKERS);
				this.progressForm.ProgressCount = mapChecker.GetFixProgressSteps();
				this.progressForm.Progress = 0;
				int time = Kernel.GetTickCount();
				int timeTotal = time;
				mapChecker.Fix(this.map, this.progressForm);
				timeTotal = Kernel.GetTickCount() - timeTotal;
				Console.WriteLine("Map check complete : {0} ms", timeTotal);
				this.progressForm.Delete();
			}
		}

		// Token: 0x06001FA0 RID: 8096 RVA: 0x000CA730 File Offset: 0x000C9730
		public void FixAll(Form parentForm)
		{
			int progressCount = this.GetFixProgressCount();
			if (progressCount > 0)
			{
				this.progressForm.Create(Strings.RUN_MAP_CHECKERS_FIX);
				this.progressForm.ProgressCount = this.GetFixProgressCount();
				this.progressForm.Progress = 0;
				int time = Kernel.GetTickCount();
				int timeTotal = time;
				foreach (IMapChecker mapChecker in this.mapCheckers)
				{
					if (mapChecker != null && mapChecker.GetFixProgressSteps() > 0)
					{
						mapChecker.Fix(this.map, this.progressForm);
					}
				}
				timeTotal = Kernel.GetTickCount() - timeTotal;
				Console.WriteLine("Map fix complete : {0} ms", timeTotal);
				this.progressForm.Delete();
			}
		}

		// Token: 0x0400137B RID: 4987
		private readonly ProgressForm progressForm = new ProgressForm();

		// Token: 0x0400137C RID: 4988
		private readonly MapEditorMap map;

		// Token: 0x0400137D RID: 4989
		private readonly List<IMapChecker> mapCheckers = new List<IMapChecker>();
	}
}
