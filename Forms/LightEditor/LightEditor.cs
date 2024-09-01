using System;
using System.Collections.Generic;
using Tools.DBGameObjects;
using Tools.DBGameObjects.GameObjects;

namespace MapEditor.Forms.LightEditor
{
	// Token: 0x02000053 RID: 83
	public class LightEditor
	{
		// Token: 0x06000442 RID: 1090 RVA: 0x00023EA8 File Offset: 0x00022EA8
		private void LoadView()
		{
			DBMethods.LoadObjects("ZoneLights", this.lights, true);
			foreach (GameObjectClass gameObjectClass in this.lights)
			{
				LightClass light = (LightClass)gameObjectClass;
				if (light != null)
				{
					light.Load();
				}
			}
		}

		// Token: 0x06000443 RID: 1091 RVA: 0x00023F10 File Offset: 0x00022F10
		public LightEditor()
		{
			this.LoadView();
		}

		// Token: 0x06000444 RID: 1092 RVA: 0x00023F30 File Offset: 0x00022F30
		public List<string> GetLightList(string continentName)
		{
			List<string> lightList = new List<string>();
			foreach (GameObjectClass gameObjectClass in this.lights)
			{
				LightClass light = (LightClass)gameObjectClass;
				if (light.GameObject.Contains(continentName + "/ZoneLights"))
				{
					lightList.Add(light.GameObject);
				}
			}
			return lightList;
		}

		// Token: 0x06000445 RID: 1093 RVA: 0x00023FA8 File Offset: 0x00022FA8
		public bool LoadLight(string dbidString)
		{
			if (this.currentLight == null && string.IsNullOrEmpty(dbidString))
			{
				return false;
			}
			if (this.currentLight != null && this.currentLight.GameObject == dbidString)
			{
				return false;
			}
			if (this.LoadNewLight != null)
			{
				this.LoadNewLight();
			}
			if (string.IsNullOrEmpty(dbidString))
			{
				this.currentLight = null;
			}
			else
			{
				this.currentLight = (this.lights.GetObjectByDBID(dbidString) as LightClass);
			}
			return this.currentLight != null;
		}

		// Token: 0x06000446 RID: 1094 RVA: 0x0002402B File Offset: 0x0002302B
		public LightClass GetCurrentLight()
		{
			return this.currentLight;
		}

		// Token: 0x06000447 RID: 1095 RVA: 0x00024033 File Offset: 0x00023033
		public bool AddInstantLight()
		{
			if (this.currentLight == null)
			{
				return false;
			}
			this.currentLight.AddInstantLight();
			return true;
		}

		// Token: 0x06000448 RID: 1096 RVA: 0x0002404B File Offset: 0x0002304B
		public bool RemoveInstantLight(int index)
		{
			if (this.currentLight == null)
			{
				return false;
			}
			this.currentLight.RemoveInstantLight(index);
			return true;
		}

		// Token: 0x06000449 RID: 1097 RVA: 0x00024064 File Offset: 0x00023064
		public bool CreateNewLight(string name, string continent, out string newObjectDBID)
		{
			newObjectDBID = string.Empty;
			if (string.IsNullOrEmpty(name) || string.IsNullOrEmpty(continent))
			{
				return false;
			}
			name = string.Concat(new object[]
			{
				continent,
				'/',
				"ZoneLights",
				'/',
				name,
				".xdb"
			}).Replace('\\', '/');
			LightClass newLight;
			if (LightClass.CreareNewLight(name, out newLight))
			{
				this.lights.AddObjectIfNotFinded(newLight);
				newObjectDBID = newLight.GameObject;
				return true;
			}
			return false;
		}

		// Token: 0x0600044A RID: 1098 RVA: 0x000240F0 File Offset: 0x000230F0
		public bool DeleteLight()
		{
			if (this.currentLight != null)
			{
				bool result = this.currentLight.DeleteObjectFromDatabase();
				this.currentLight = null;
				return result;
			}
			return false;
		}

		// Token: 0x04000308 RID: 776
		private const string lightDirectoryName = "ZoneLights";

		// Token: 0x04000309 RID: 777
		private readonly LightView lights = new LightView("LightEditorLightView");

		// Token: 0x0400030A RID: 778
		private LightClass currentLight;

		// Token: 0x0400030B RID: 779
		public LightEditor.LightEditorEvent LoadNewLight;

		// Token: 0x02000054 RID: 84
		// (Invoke) Token: 0x0600044C RID: 1100
		public delegate void LightEditorEvent();
	}
}
