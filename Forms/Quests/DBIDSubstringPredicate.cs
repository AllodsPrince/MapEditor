using System;

namespace MapEditor.Forms.Quests
{
	// Token: 0x0200006B RID: 107
	public class DBIDSubstringPredicate
	{
		// Token: 0x06000535 RID: 1333 RVA: 0x0002A2AE File Offset: 0x000292AE
		public DBIDSubstringPredicate(string _zone)
		{
			this.substr = _zone;
		}

		// Token: 0x06000536 RID: 1334 RVA: 0x0002A2BD File Offset: 0x000292BD
		public bool Filter(DBID dbid)
		{
			return dbid.ToString().ToUpper().Contains(this.substr.ToUpper());
		}

		// Token: 0x040003C7 RID: 967
		private readonly string substr;
	}
}
