using System;
using System.Collections.Generic;
using System.IO;
using MapEditor.Map.MapObjects;
using MapEditor.Resources.Strings;
using Tools.MapObjects;
using Tools.Progress;

namespace MapEditor.Map.MapCheckers.SpecificCheckers
{
	// Token: 0x02000202 RID: 514
	internal class LinksCheker : MapChecker
	{
		// Token: 0x06001972 RID: 6514 RVA: 0x000A6BB8 File Offset: 0x000A5BB8
		public LinksCheker()
		{
			base.Name = "Чекер на перекрестные ссылки";
			base.LongDescription = "Производит проверку на наличие \"чужых\" ссылок в мап-ресурсах";
			base.ShortDescription = "Запрет на создание совместных кладбищей";
		}

		// Token: 0x06001973 RID: 6515 RVA: 0x000A6BEC File Offset: 0x000A5BEC
		public override void Check(MapEditorMap map, IProgressContainer progressContainer)
		{
			base.Status = MapCheckerStatus.Green;
			foreach (KeyValuePair<int, IMapObject> keyValuePair in map.MapEditorMapObjectContainer.ScriptAreaContainer.MapObjects)
			{
				ScriptArea area = (ScriptArea)keyValuePair.Value;
				StreamReader sr = new StreamReader(EditorEnvironment.DataFolder + area.ScriptZone);
				string line = sr.ReadToEnd();
				sr.Dispose();
				this.ParsString(line);
				this.CutGood();
				if (base.LongInfoView == null)
				{
					base.LongInfoView = new LongInfoViewNode(true);
				}
				if (!this.ComparePaths(map))
				{
					base.LongInfoView.FindOrAddNode(Strings.OBJECTS_WITH_EXTERNAL_LINKS, false).AddNode(keyValuePair.Value, false);
				}
				this.paths.Clear();
			}
		}

		// Token: 0x06001974 RID: 6516 RVA: 0x000A6CD4 File Offset: 0x000A5CD4
		private bool ComparePaths(MapEditorMap map)
		{
			foreach (string path in this.paths)
			{
				if (!path.Contains("Map"))
				{
					base.Status = MapCheckerStatus.Yellow;
					return false;
				}
				if (!path.Contains(map.Data.ContinentName))
				{
					base.Status = MapCheckerStatus.Red;
					return false;
				}
			}
			return true;
		}

		// Token: 0x06001975 RID: 6517 RVA: 0x000A6D5C File Offset: 0x000A5D5C
		private void CutGood()
		{
			for (int i = 0; i < this.paths.Count; i++)
			{
				int j = this.paths[i].IndexOf(LinksCheker.End);
				this.paths[i] = this.paths[i].Substring(0, j);
			}
		}

		// Token: 0x06001976 RID: 6518 RVA: 0x000A6DB8 File Offset: 0x000A5DB8
		private void ParsString(string line)
		{
			if (line.Contains("href="))
			{
				int i = line.IndexOf(LinksCheker.Start);
				string lineSome = line.Substring(i + LinksCheker.Start.Length, line.Length - i - LinksCheker.Start.Length);
				this.paths.Add(lineSome);
				this.ParsString(lineSome);
			}
		}

		// Token: 0x04001047 RID: 4167
		private static string Start = "href=";

		// Token: 0x04001048 RID: 4168
		private static string End = "#xpointer";

		// Token: 0x04001049 RID: 4169
		private List<string> paths = new List<string>();
	}
}
