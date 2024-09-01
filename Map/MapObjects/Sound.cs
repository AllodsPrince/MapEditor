using System;
using System.ComponentModel;

namespace MapEditor.Map.MapObjects
{
	// Token: 0x0200016D RID: 365
	public class Sound
	{
		// Token: 0x17000388 RID: 904
		// (get) Token: 0x060011C9 RID: 4553 RVA: 0x000841B5 File Offset: 0x000831B5
		// (set) Token: 0x060011CA RID: 4554 RVA: 0x000841BD File Offset: 0x000831BD
		[ReadOnly(true)]
		public string Project
		{
			get
			{
				return this.project;
			}
			set
			{
				this.project = value;
			}
		}

		// Token: 0x17000389 RID: 905
		// (get) Token: 0x060011CB RID: 4555 RVA: 0x000841C6 File Offset: 0x000831C6
		// (set) Token: 0x060011CC RID: 4556 RVA: 0x000841CE File Offset: 0x000831CE
		[ReadOnly(true)]
		public string Name
		{
			get
			{
				return this.name;
			}
			set
			{
				this.name = value;
			}
		}

		// Token: 0x1700038A RID: 906
		// (get) Token: 0x060011CD RID: 4557 RVA: 0x000841D7 File Offset: 0x000831D7
		public static Sound Empty
		{
			get
			{
				return Sound.empty;
			}
		}

		// Token: 0x060011CE RID: 4558 RVA: 0x000841DE File Offset: 0x000831DE
		public Sound()
		{
		}

		// Token: 0x060011CF RID: 4559 RVA: 0x000841FC File Offset: 0x000831FC
		public Sound(string _project, string _name)
		{
			this.project = _project;
			this.name = _name;
		}

		// Token: 0x060011D0 RID: 4560 RVA: 0x00084228 File Offset: 0x00083228
		public Sound(string str)
		{
			if (!string.IsNullOrEmpty(str))
			{
				string[] parts = str.Split(",".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
				if (parts.Length == 2)
				{
					this.name = parts[0].Replace("Name:", string.Empty).Trim();
					this.project = parts[1].Replace("Project:", string.Empty).Trim();
				}
			}
		}

		// Token: 0x060011D1 RID: 4561 RVA: 0x000842AC File Offset: 0x000832AC
		public override string ToString()
		{
			return string.Format("{0} {1} {2} {3} {4}", new object[]
			{
				"Name:",
				this.name,
				",",
				"Project:",
				this.project
			});
		}

		// Token: 0x060011D2 RID: 4562 RVA: 0x000842F5 File Offset: 0x000832F5
		public bool ContainsText(string text, bool ignoreCase)
		{
			return Sound.StringContainsText(this.project, text, ignoreCase) || Sound.StringContainsText(this.name, text, ignoreCase);
		}

		// Token: 0x060011D3 RID: 4563 RVA: 0x00084315 File Offset: 0x00083315
		private static bool StringContainsText(string str, string text, bool ignoreCase)
		{
			if (!string.IsNullOrEmpty(text) && !string.IsNullOrEmpty(str))
			{
				if (ignoreCase)
				{
					if (str.ToLower().Contains(text.ToLower()))
					{
						return true;
					}
				}
				else if (str.Contains(text))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x04000CC7 RID: 3271
		private const string PROJECT = "Project:";

		// Token: 0x04000CC8 RID: 3272
		private const string NAME = "Name:";

		// Token: 0x04000CC9 RID: 3273
		private const string SEPARATOR = ",";

		// Token: 0x04000CCA RID: 3274
		private static readonly Sound empty = new Sound();

		// Token: 0x04000CCB RID: 3275
		private string project = string.Empty;

		// Token: 0x04000CCC RID: 3276
		private string name = string.Empty;

		// Token: 0x0200016E RID: 366
		// (Invoke) Token: 0x060011D6 RID: 4566
		public delegate void SoundFieldChangedEvent<T>(ref T oldValue, ref T newValue);
	}
}
