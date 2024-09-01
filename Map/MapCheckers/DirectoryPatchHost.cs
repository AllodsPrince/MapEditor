using System;
using System.IO;

namespace MapEditor.Map.MapCheckers
{
	// Token: 0x02000203 RID: 515
	internal class DirectoryPatchHost
	{
		// Token: 0x06001978 RID: 6520 RVA: 0x000A6E30 File Offset: 0x000A5E30
		public DirectoryPatchHost(string path)
		{
			this.Dir = new DirectoryInfo(path);
			string[] minXY = this.Dir.Name.Split(new char[]
			{
				'_'
			});
			this.MinX = int.Parse(minXY[0]);
			this.MinY = int.Parse(minXY[1]);
			this.CalcMinMaxParams();
		}

		// Token: 0x06001979 RID: 6521 RVA: 0x000A6E90 File Offset: 0x000A5E90
		private void CalcMinMaxParams()
		{
			this.MaxX = this.MinX;
			this.MaxY = this.MinY;
			int UpX = 0;
			int UpY = 0;
			int DwnX = 9;
			int DwnY = 9;
			string[] files = Directory.GetFiles(this.Dir.FullName);
			foreach (string file in files)
			{
				FileInfo f = new FileInfo(file);
				string[] s = f.Name.Split(new char[]
				{
					'_'
				});
				int i;
				int.TryParse(s[0], out i);
				int j;
				int.TryParse(s[1], out j);
				if (i > UpX)
				{
					UpX = i;
				}
				if (i < DwnX)
				{
					DwnX = i;
				}
				if (j > UpY)
				{
					UpY = j;
				}
				if (j < DwnY)
				{
					DwnY = j;
				}
			}
			this.MaxX += UpX;
			this.MaxY += UpY;
			this.MinX += DwnX;
			this.MinY += DwnY;
		}

		// Token: 0x0400104A RID: 4170
		private DirectoryInfo Dir;

		// Token: 0x0400104B RID: 4171
		public int MinX;

		// Token: 0x0400104C RID: 4172
		public int MinY;

		// Token: 0x0400104D RID: 4173
		public int MaxX;

		// Token: 0x0400104E RID: 4174
		public int MaxY;
	}
}
