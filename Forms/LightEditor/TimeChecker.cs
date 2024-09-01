using System;
using System.Collections.Generic;
using System.Windows.Forms;
using MapEditor.Resources.Strings;
using Tools.DBGameObjects.GameObjects;

namespace MapEditor.Forms.LightEditor
{
	// Token: 0x020002D4 RID: 724
	public class TimeChecker : Checker
	{
		// Token: 0x06002167 RID: 8551 RVA: 0x000D307C File Offset: 0x000D207C
		public bool Check(LightClass light)
		{
			if (light == null)
			{
				return false;
			}
			List<int> timeList = new List<int>();
			foreach (LightClass.InstantLight instantLight in light.InstantLights)
			{
				if (instantLight != null)
				{
					if (timeList.Contains(instantLight.Time))
					{
						DialogResult dialogResult = MessageBox.Show(Strings.LIGHTEDITOR_TIME_WARNING, Strings.LIGHTEDITOR_TITLE, MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);
						if (dialogResult == DialogResult.No)
						{
							return false;
						}
					}
					else
					{
						timeList.Add(instantLight.Time);
					}
				}
			}
			return true;
		}
	}
}
