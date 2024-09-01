using System;
using System.Collections.Generic;
using System.IO;
using MapEditor.Map.Dialogs;
using Tools.Containers;
using Tools.Filter;
using Tools.Geometry;

namespace MapEditor.Map.MapCheckers
{
	// Token: 0x020002CD RID: 717
	public class CMapCheckerHost
	{
		// Token: 0x17000770 RID: 1904
		// (get) Token: 0x06002137 RID: 8503 RVA: 0x000D234B File Offset: 0x000D134B
		public string Name
		{
			get
			{
				return this.Dir.Name;
			}
		}

		// Token: 0x06002138 RID: 8504 RVA: 0x000D2358 File Offset: 0x000D1358
		public CMapCheckerHost(string path)
		{
			this.Dir = new DirectoryInfo(path);
			this.filter.AddToMastHave("_");
			this.filter.AddToIgnore("svn");
			foreach (string s in Directory.GetDirectories(path))
			{
				if (this.filter.IsAllowed(new DirectoryInfo(s).Name))
				{
					this.dirs.Add(new DirectoryPatchHost(s));
				}
			}
			this.CalculateParams();
			this.CreateOpenParams();
		}

		// Token: 0x06002139 RID: 8505 RVA: 0x000D241C File Offset: 0x000D141C
		private void CreateOpenParams()
		{
			for (int i = this.MinX; i < this.MaxX; i += 4)
			{
				for (int j = this.MinY; j < this.MaxY; j += 4)
				{
					OpenMapDialog.Params tmp = new OpenMapDialog.Params();
					Point p = new Point(i, j);
					tmp.SetPatch(p);
					this.list.Add(new Pair<Point, OpenMapDialog.Params>(p, tmp));
				}
			}
		}

		// Token: 0x0600213A RID: 8506 RVA: 0x000D2480 File Offset: 0x000D1480
		private void CalculateParams()
		{
			foreach (DirectoryPatchHost dir in this.dirs)
			{
				if (dir.MaxX > this.MaxX)
				{
					this.MaxX = dir.MaxX;
				}
				if (dir.MaxY > this.MaxY)
				{
					this.MaxY = dir.MaxY;
				}
				if (dir.MinX < this.MinX)
				{
					this.MinX = dir.MinX;
				}
				if (dir.MinY < this.MinY)
				{
					this.MinY = dir.MinY;
				}
			}
		}

		// Token: 0x0400141F RID: 5151
		private List<DirectoryPatchHost> dirs = new List<DirectoryPatchHost>();

		// Token: 0x04001420 RID: 5152
		private NameFilter filter = new NameFilter();

		// Token: 0x04001421 RID: 5153
		private DirectoryInfo Dir;

		// Token: 0x04001422 RID: 5154
		public List<Pair<Point, OpenMapDialog.Params>> list = new List<Pair<Point, OpenMapDialog.Params>>();

		// Token: 0x04001423 RID: 5155
		private int MinX = 999;

		// Token: 0x04001424 RID: 5156
		private int MinY = 999;

		// Token: 0x04001425 RID: 5157
		private int MaxX;

		// Token: 0x04001426 RID: 5158
		private int MaxY;
	}
}
