using System;
using System.Windows.Forms;
using MapEditor.Resources.Strings;
using Tools.DBGameObjects.GameObjects;

namespace MapEditor.Forms.LightEditor
{
	// Token: 0x020002D5 RID: 725
	public class FogChecker : Checker
	{
		// Token: 0x06002169 RID: 8553 RVA: 0x000D3118 File Offset: 0x000D2118
		public bool Check(LightClass light)
		{
			if (light == null)
			{
				return false;
			}
			foreach (LightClass.InstantLight instantLight in light.InstantLights)
			{
				if (instantLight != null && instantLight.FogStart > instantLight.FogEnd)
				{
					DialogResult dialogResult = MessageBox.Show(Strings.LIGHTEDITOR_FOG_WARNING, Strings.LIGHTEDITOR_TITLE, MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);
					if (dialogResult == DialogResult.No)
					{
						return false;
					}
				}
			}
			return true;
		}
	}
}
