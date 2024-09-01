using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;
using MapEditor.Forms.Base;
using MapEditor.Map;
using MapEditor.Resources.Strings;
using Tools.DBGameObjects.GameObjects;
using Tools.InputBox;

namespace MapEditor.Forms.LightEditor
{
	// Token: 0x02000244 RID: 580
	public partial class LightEditorForm : BaseForm
	{
		// Token: 0x06001B93 RID: 7059 RVA: 0x000B3A20 File Offset: 0x000B2A20
		private void OnFormVisibleChanged(object sender, EventArgs e)
		{
			if (base.Visible && this.lightEditor == null)
			{
				this.Initialize();
			}
			if (!base.Visible && this.lightEditor != null && !base.Context.FormClosing)
			{
				this.PrompToSave(base.Context.MainForm);
			}
		}

		// Token: 0x06001B94 RID: 7060 RVA: 0x000B3A71 File Offset: 0x000B2A71
		private void OnContinentSelectedIndexChanged(object sender, EventArgs e)
		{
			this.LoadLightList(string.Empty);
		}

		// Token: 0x06001B95 RID: 7061 RVA: 0x000B3A7E File Offset: 0x000B2A7E
		private void OnLightSelectedIndexChanged(object sender, EventArgs e)
		{
			this.LoadLight();
		}

		// Token: 0x06001B96 RID: 7062 RVA: 0x000B3A86 File Offset: 0x000B2A86
		private void InstantLightsSelectedIndexChanged(object sender, EventArgs e)
		{
			this.LoadInstance();
		}

		// Token: 0x06001B97 RID: 7063 RVA: 0x000B3A8E File Offset: 0x000B2A8E
		private void OnValidatorChanged(Validator validator)
		{
			this.SetInstance();
		}

		// Token: 0x06001B98 RID: 7064 RVA: 0x000B3A96 File Offset: 0x000B2A96
		private void OnValidatorChanging(Validator validator)
		{
			this.SaveButton.Enabled = true;
		}

		// Token: 0x06001B99 RID: 7065 RVA: 0x000B3AA4 File Offset: 0x000B2AA4
		private void OnNewButtonClick(object sender, EventArgs e)
		{
			this.CreateNewLight();
		}

		// Token: 0x06001B9A RID: 7066 RVA: 0x000B3AAC File Offset: 0x000B2AAC
		private void OnDeleteButtonClick(object sender, EventArgs e)
		{
			this.DeleteLight();
		}

		// Token: 0x06001B9B RID: 7067 RVA: 0x000B3AB4 File Offset: 0x000B2AB4
		private void OnSaveButtonClick(object sender, EventArgs e)
		{
			this.Save();
		}

		// Token: 0x06001B9C RID: 7068 RVA: 0x000B3ABC File Offset: 0x000B2ABC
		private void OnChangeCurrentLight()
		{
			this.PrompToSave(this);
		}

		// Token: 0x06001B9D RID: 7069 RVA: 0x000B3AC5 File Offset: 0x000B2AC5
		private void OnAddInstantLightButtonClick(object sender, EventArgs e)
		{
			this.AddInstantLight();
		}

		// Token: 0x06001B9E RID: 7070 RVA: 0x000B3ACD File Offset: 0x000B2ACD
		private void OnRemoveInstantLightButtonClick(object sender, EventArgs e)
		{
			this.RemoveInstantLight();
		}

		// Token: 0x06001B9F RID: 7071 RVA: 0x000B3AD5 File Offset: 0x000B2AD5
		private void OnCloseButtonClick(object sender, EventArgs e)
		{
			base.Close();
		}

		// Token: 0x06001BA0 RID: 7072 RVA: 0x000B3AE0 File Offset: 0x000B2AE0
		private void Initialize()
		{
			this.lightEditor = new LightEditor();
			LightEditor lightEditor = this.lightEditor;
			lightEditor.LoadNewLight = (LightEditor.LightEditorEvent)Delegate.Combine(lightEditor.LoadNewLight, new LightEditor.LightEditorEvent(this.OnChangeCurrentLight));
			this.LoadContinentList();
			this.LoadLightList(this.LightComboBox.Text);
		}

		// Token: 0x06001BA1 RID: 7073 RVA: 0x000B3B38 File Offset: 0x000B2B38
		private void LoadContinentList()
		{
			this.ContinentComboBox.Items.Clear();
			if (this.lightEditor != null)
			{
				List<string> continentNameList = new List<string>();
				Constants.GetContinentNameList(ref continentNameList);
				foreach (string continentName in continentNameList)
				{
					this.ContinentComboBox.Items.Add(continentName);
				}
			}
		}

		// Token: 0x06001BA2 RID: 7074 RVA: 0x000B3BB8 File Offset: 0x000B2BB8
		private void LoadLightList(string selectedValue)
		{
			this.LightComboBox.Items.Clear();
			if (!string.IsNullOrEmpty(this.ContinentComboBox.Text) && this.lightEditor != null)
			{
				List<string> lightList = this.lightEditor.GetLightList(this.ContinentComboBox.Text);
				lightList.Sort();
				foreach (string light in lightList)
				{
					this.LightComboBox.Items.Add(light);
				}
			}
			this.LightComboBox.SelectedIndex = this.LightComboBox.Items.IndexOf(selectedValue);
			if (this.LightComboBox.SelectedIndex == -1)
			{
				this.LightComboBox.Text = string.Empty;
			}
			this.LoadLight();
		}

		// Token: 0x06001BA3 RID: 7075 RVA: 0x000B3C98 File Offset: 0x000B2C98
		private void LoadInstantLightsList()
		{
			this.InstantLightsListBox.Items.Clear();
			this.ClearInstantLight();
			if (this.lightEditor == null)
			{
				return;
			}
			LightClass light = this.lightEditor.GetCurrentLight();
			int count = 0;
			if (light != null)
			{
				count = light.InstantLights.Count;
				for (int instantLineNum = 0; instantLineNum < count; instantLineNum++)
				{
					this.InstantLightsListBox.Items.Add("instantLights[" + instantLineNum + "]");
				}
				if (count > 0)
				{
					this.InstantLightsListBox.SelectedIndex = 0;
				}
			}
			this.RemoveInstantLightButton.Enabled = (light != null && count > 0);
		}

		// Token: 0x06001BA4 RID: 7076 RVA: 0x000B3D38 File Offset: 0x000B2D38
		private void LoadLight()
		{
			if (this.lightEditor != null)
			{
				this.lightEditor.LoadLight(this.LightComboBox.Text);
				this.LoadInstantLightsList();
				this.SetControlsEnable();
			}
		}

		// Token: 0x06001BA5 RID: 7077 RVA: 0x000B3D68 File Offset: 0x000B2D68
		private void SetControlsEnable()
		{
			if (this.lightEditor != null)
			{
				LightClass light = this.lightEditor.GetCurrentLight();
				this.AddInstantLightButton.Enabled = (light != null);
				this.RemoveInstantLightButton.Enabled = (light != null && light.InstantLights.Count > 0);
				this.NewButton.Enabled = true;
				this.DeleteButton.Enabled = (light != null);
				this.SaveButton.Enabled = false;
			}
		}

		// Token: 0x06001BA6 RID: 7078 RVA: 0x000B3DE4 File Offset: 0x000B2DE4
		private void LoadInstance()
		{
			this.ClearInstantLight();
			if (this.lightEditor == null)
			{
				return;
			}
			LightClass light = this.lightEditor.GetCurrentLight();
			if (light == null)
			{
				this.ClearInstantLight();
				return;
			}
			int index = this.InstantLightsListBox.SelectedIndex;
			if (index < 0 || index > light.InstantLights.Count - 1)
			{
				return;
			}
			LightClass.InstantLight instantLight = light.InstantLights[index];
			if (instantLight == null)
			{
				return;
			}
			this.timeValidator.SetValue(instantLight.Time);
			this.skyMeshValidator.SetValue(instantLight.SkyMeshDBIDString);
			this.ambientFactorValidator.SetValue(instantLight.AmbientFactor);
			this.fogStartValidator.SetValue(instantLight.FogStart);
			this.fogEndValidator.SetValue(instantLight.FogEnd);
			this.sunLightYawValidator.SetValue(instantLight.SunLightYaw);
			this.sunLightPitchValidator.SetValue(instantLight.SunLightPitch);
			this.ambientColorValidator.SetValue(instantLight.AmbientColor);
			this.diffuseColorValidator.SetValue(instantLight.DiffuseColor);
			this.fogColorValidator.SetValue(instantLight.FogColor);
			this.specularColorValidator.SetValue(instantLight.SpecularColor);
			this.specularWaterColorValidator.SetValue(instantLight.SpecularWaterColor);
			this.waterGradientStartValidator.SetValue(instantLight.WaterGradientStart);
			this.waterGradientEndValidator.SetValue(instantLight.WaterGradientEnd);
			this.contourColorValidator.SetValue(instantLight.ContourColor);
		}

		// Token: 0x06001BA7 RID: 7079 RVA: 0x000B3F48 File Offset: 0x000B2F48
		private void ClearInstantLight()
		{
			foreach (Validator validator in this.validators)
			{
				validator.Clear();
			}
		}

		// Token: 0x06001BA8 RID: 7080 RVA: 0x000B3F9C File Offset: 0x000B2F9C
		private void SetInstance()
		{
			if (this.lightEditor == null)
			{
				return;
			}
			LightClass light = this.lightEditor.GetCurrentLight();
			if (light == null)
			{
				return;
			}
			int index = this.InstantLightsListBox.SelectedIndex;
			if (index < 0 || index > light.InstantLights.Count - 1)
			{
				return;
			}
			LightClass.InstantLight instantLight = light.InstantLights[index];
			if (instantLight == null)
			{
				return;
			}
			instantLight.Time = this.timeValidator.GetValue();
			instantLight.SkyMeshDBIDString = this.skyMeshValidator.GetValue();
			instantLight.AmbientFactor = this.ambientFactorValidator.GetValue();
			instantLight.FogStart = this.fogStartValidator.GetValue();
			instantLight.FogEnd = this.fogEndValidator.GetValue();
			instantLight.SunLightYaw = this.sunLightYawValidator.GetValue();
			instantLight.SunLightPitch = this.sunLightPitchValidator.GetValue();
			instantLight.AmbientColor = this.ambientColorValidator.GetValue();
			instantLight.DiffuseColor = this.diffuseColorValidator.GetValue();
			instantLight.FogColor = this.fogColorValidator.GetValue();
			instantLight.SpecularColor = this.specularColorValidator.GetValue();
			instantLight.SpecularWaterColor = this.specularWaterColorValidator.GetValue();
			instantLight.WaterGradientStart = this.waterGradientStartValidator.GetValue();
			instantLight.WaterGradientEnd = this.waterGradientEndValidator.GetValue();
			instantLight.ContourColor = this.contourColorValidator.GetValue();
			light.SetToDB();
		}

		// Token: 0x06001BA9 RID: 7081 RVA: 0x000B40FC File Offset: 0x000B30FC
		private void CreateNewLight()
		{
			if (this.lightEditor != null)
			{
				InputBoxForm inputBox = new InputBoxForm();
				inputBox.InputCaption = Strings.LIGHTEDITOR_TITLE;
				if (inputBox.ShowDialog() == DialogResult.OK)
				{
					string name = inputBox.InputText.Trim();
					if (!string.IsNullOrEmpty(name))
					{
						string newObjectStringDBID;
						bool result = this.lightEditor.CreateNewLight(name, this.ContinentComboBox.Text, out newObjectStringDBID);
						if (result)
						{
							this.LoadLightList(newObjectStringDBID);
							return;
						}
						MessageBox.Show(Strings.LIGHTEDITOR_ADDERROE_MESSAGE, Strings.LIGHTEDITOR_TITLE, MessageBoxButtons.OK, MessageBoxIcon.Hand);
					}
				}
			}
		}

		// Token: 0x06001BAA RID: 7082 RVA: 0x000B4178 File Offset: 0x000B3178
		private void DeleteLight()
		{
			if (this.lightEditor != null)
			{
				DialogResult dialogResult = MessageBox.Show(Strings.LIGHTEDITOR_DELETE_MESSAGE, Strings.LIGHTEDITOR_TITLE, MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);
				if (dialogResult == DialogResult.Yes)
				{
					this.Cursor = Cursors.WaitCursor;
					if (this.lightEditor.DeleteLight())
					{
						this.LoadLightList(string.Empty);
					}
					this.SetControlsEnable();
					this.Cursor = Cursors.Default;
				}
			}
		}

		// Token: 0x06001BAB RID: 7083 RVA: 0x000B41D8 File Offset: 0x000B31D8
		private void AddInstantLight()
		{
			if (this.lightEditor != null)
			{
				this.lightEditor.AddInstantLight();
				this.LoadInstantLightsList();
				int count = this.InstantLightsListBox.Items.Count;
				if (count > 0)
				{
					this.InstantLightsListBox.SelectedIndex = count - 1;
				}
			}
		}

		// Token: 0x06001BAC RID: 7084 RVA: 0x000B4222 File Offset: 0x000B3222
		private void RemoveInstantLight()
		{
			if (this.lightEditor != null && this.InstantLightsListBox.SelectedIndex > -1)
			{
				this.lightEditor.RemoveInstantLight(this.InstantLightsListBox.SelectedIndex);
				this.LoadInstantLightsList();
			}
		}

		// Token: 0x06001BAD RID: 7085 RVA: 0x000B4258 File Offset: 0x000B3258
		private void Save()
		{
			if (this.lightEditor != null)
			{
				LightClass light = this.lightEditor.GetCurrentLight();
				if (light != null)
				{
					this.Cursor = Cursors.WaitCursor;
					this.SetInstance();
					foreach (Checker checker in this.checkers)
					{
						if (!checker.Check(light))
						{
							this.Cursor = Cursors.Default;
							return;
						}
					}
					light.Save();
					this.SaveButton.Enabled = false;
					this.Cursor = Cursors.Default;
				}
			}
		}

		// Token: 0x06001BAE RID: 7086 RVA: 0x000B4304 File Offset: 0x000B3304
		private void PrompToSave(IWin32Window dialogOwner)
		{
			if (this.lightEditor == null)
			{
				return;
			}
			LightClass light = this.lightEditor.GetCurrentLight();
			if (light == null)
			{
				return;
			}
			if (light.WasChanged())
			{
				DialogResult dialogResult = MessageBox.Show(dialogOwner, Strings.MAP_SAVE_CHANGES_MESSAGE, Strings.LIGHTEDITOR_TITLE, MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);
				if (dialogResult == DialogResult.Yes)
				{
					this.Save();
					return;
				}
				light.RevertChanges();
			}
		}

		// Token: 0x06001BAF RID: 7087 RVA: 0x000B4358 File Offset: 0x000B3358
		public LightEditorForm(MainForm.Context _context) : base(EditorEnvironment.EditorFormsFolder + "LightEditorForm.xml", _context)
		{
			this.InitializeComponent();
			this.timeValidator = new TextHoursValidator(this.TimeTextBox);
			this.skyMeshValidator = new DBIDValidator(this.SkyMeshTextBox, "SkyMesh", this.ChooseSkyMeshButton, this.ClearSkyMeshButton);
			this.ambientFactorValidator = new TextFloatValidator(this.AmbientFactorTextBox);
			this.fogStartValidator = new TextFloatValidator(this.FogStartTextBox);
			this.fogEndValidator = new TextFloatValidator(this.FogEndTextBox);
			this.sunLightYawValidator = new TextFloatValidator(this.SunLightYawTextBox);
			this.sunLightPitchValidator = new TextFloatValidator(this.SunLightPitchTextBox);
			this.ambientColorValidator = new ColorValidator(this.AmbientColorPanel, this.AmbientColorRadioButton, this.AlphaColorTextBox, this.HueColorTextBox, this.SatColorTextBox, this.LumColorTextBox, this.AlphaColorTrackBar, this.HueColorTrackBar, this.SatColorTrackBar, this.LumColorTrackBar, this.AmbilentColorAHSLLabel);
			this.diffuseColorValidator = new ColorValidator(this.DiffuseColorPanel, this.DiffuseColorRadioButton, this.AlphaColorTextBox, this.HueColorTextBox, this.SatColorTextBox, this.LumColorTextBox, this.AlphaColorTrackBar, this.HueColorTrackBar, this.SatColorTrackBar, this.LumColorTrackBar, this.DiffuseColorAHSLLabel);
			this.fogColorValidator = new ColorValidator(this.FogColorPanel, this.FogColorRadioButton, this.AlphaColorTextBox, this.HueColorTextBox, this.SatColorTextBox, this.LumColorTextBox, this.AlphaColorTrackBar, this.HueColorTrackBar, this.SatColorTrackBar, this.LumColorTrackBar, this.FogColorAHSLLabel);
			this.specularColorValidator = new ColorValidator(this.SpecularColorPanel, this.SpecularColorRadioButton, this.AlphaColorTextBox, this.HueColorTextBox, this.SatColorTextBox, this.LumColorTextBox, this.AlphaColorTrackBar, this.HueColorTrackBar, this.SatColorTrackBar, this.LumColorTrackBar, this.SpecularColorAHSLLabel);
			this.specularWaterColorValidator = new ColorValidator(this.SpecularWaterColorPanel, this.SpecularWaterColorRadioButton, this.AlphaColorTextBox, this.HueColorTextBox, this.SatColorTextBox, this.LumColorTextBox, this.AlphaColorTrackBar, this.HueColorTrackBar, this.SatColorTrackBar, this.LumColorTrackBar, this.SpecularWaterColorAHSLLabel);
			this.waterGradientStartValidator = new ColorValidator(this.WaterGradientStartPanel, this.WaterGradientStartRadioButton, this.AlphaColorTextBox, this.HueColorTextBox, this.SatColorTextBox, this.LumColorTextBox, this.AlphaColorTrackBar, this.HueColorTrackBar, this.SatColorTrackBar, this.LumColorTrackBar, this.WaterGradientStartAHSLLabel);
			this.waterGradientEndValidator = new ColorValidator(this.WaterGradientEndPanel, this.WaterGradientEndRadioButton, this.AlphaColorTextBox, this.HueColorTextBox, this.SatColorTextBox, this.LumColorTextBox, this.AlphaColorTrackBar, this.HueColorTrackBar, this.SatColorTrackBar, this.LumColorTrackBar, this.WaterGradientEndAHSLLabel);
			this.contourColorValidator = new ColorValidator(this.ContourColorPanel, this.ContourColorRadioButton, this.AlphaColorTextBox, this.HueColorTextBox, this.SatColorTextBox, this.LumColorTextBox, this.AlphaColorTrackBar, this.HueColorTrackBar, this.SatColorTrackBar, this.LumColorTrackBar, this.ContourColorAHSLLabel);
			this.validators.Add(this.timeValidator);
			this.validators.Add(this.skyMeshValidator);
			this.validators.Add(this.ambientFactorValidator);
			this.validators.Add(this.fogStartValidator);
			this.validators.Add(this.fogEndValidator);
			this.validators.Add(this.sunLightYawValidator);
			this.validators.Add(this.sunLightPitchValidator);
			this.validators.Add(this.ambientColorValidator);
			this.validators.Add(this.diffuseColorValidator);
			this.validators.Add(this.fogColorValidator);
			this.validators.Add(this.specularColorValidator);
			this.validators.Add(this.specularWaterColorValidator);
			this.validators.Add(this.waterGradientStartValidator);
			this.validators.Add(this.waterGradientEndValidator);
			this.validators.Add(this.contourColorValidator);
			TimeChecker timeChecker = new TimeChecker();
			FogChecker fogChecker = new FogChecker();
			this.checkers.Add(timeChecker);
			this.checkers.Add(fogChecker);
			base.VisibleChanged += this.OnFormVisibleChanged;
			this.ContinentComboBox.SelectedIndexChanged += this.OnContinentSelectedIndexChanged;
			this.LightComboBox.SelectedIndexChanged += this.OnLightSelectedIndexChanged;
			this.InstantLightsListBox.SelectedIndexChanged += this.InstantLightsSelectedIndexChanged;
			foreach (Validator validator in this.validators)
			{
				validator.ValueChanged += this.OnValidatorChanged;
				validator.ValueChanging += this.OnValidatorChanging;
			}
			this.AddInstantLightButton.Click += this.OnAddInstantLightButtonClick;
			this.RemoveInstantLightButton.Click += this.OnRemoveInstantLightButtonClick;
			this.NewButton.Click += this.OnNewButtonClick;
			this.DeleteButton.Click += this.OnDeleteButtonClick;
			this.SaveButton.Click += this.OnSaveButtonClick;
			this.CloseButton.Click += this.OnCloseButtonClick;
			this.AddInstantLightButton.Enabled = false;
			this.RemoveInstantLightButton.Enabled = false;
			this.NewButton.Enabled = true;
			this.DeleteButton.Enabled = false;
			this.SaveButton.Enabled = false;
		}

		// Token: 0x0400118F RID: 4495
		private const string instantLightName = "instantLights";

		// Token: 0x040011D4 RID: 4564
		private LightEditor lightEditor;

		// Token: 0x040011D5 RID: 4565
		private readonly TextHoursValidator timeValidator;

		// Token: 0x040011D6 RID: 4566
		private readonly DBIDValidator skyMeshValidator;

		// Token: 0x040011D7 RID: 4567
		private readonly TextFloatValidator ambientFactorValidator;

		// Token: 0x040011D8 RID: 4568
		private readonly TextFloatValidator fogStartValidator;

		// Token: 0x040011D9 RID: 4569
		private readonly TextFloatValidator fogEndValidator;

		// Token: 0x040011DA RID: 4570
		private readonly TextFloatValidator sunLightYawValidator;

		// Token: 0x040011DB RID: 4571
		private readonly TextFloatValidator sunLightPitchValidator;

		// Token: 0x040011DC RID: 4572
		private readonly ColorValidator ambientColorValidator;

		// Token: 0x040011DD RID: 4573
		private readonly ColorValidator diffuseColorValidator;

		// Token: 0x040011DE RID: 4574
		private readonly ColorValidator fogColorValidator;

		// Token: 0x040011DF RID: 4575
		private readonly ColorValidator specularColorValidator;

		// Token: 0x040011E0 RID: 4576
		private readonly ColorValidator specularWaterColorValidator;

		// Token: 0x040011E1 RID: 4577
		private readonly ColorValidator waterGradientStartValidator;

		// Token: 0x040011E2 RID: 4578
		private readonly ColorValidator waterGradientEndValidator;

		// Token: 0x040011E3 RID: 4579
		private readonly ColorValidator contourColorValidator;

		// Token: 0x040011E4 RID: 4580
		private readonly List<Validator> validators = new List<Validator>();

		// Token: 0x040011E5 RID: 4581
		private readonly List<Checker> checkers = new List<Checker>();
	}
}
