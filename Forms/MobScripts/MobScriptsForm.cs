using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Windows.Forms;
using Db;
using InputState;
using LauncherTools.InputState;
using MapEditor.Forms.Base;
using MapEditor.Forms.MobScripts.Dialogs;
using Tools.ControlContainer;

namespace MapEditor.Forms.MobScripts
{
	// Token: 0x020000D8 RID: 216
	public partial class MobScriptsForm : BaseForm
	{
		// Token: 0x06000B0D RID: 2829 RVA: 0x0005B240 File Offset: 0x0005A240
		private IObjMan GetScriptMan()
		{
			if (MobScriptsForm.mainDb != null && this.mobObjMan != null)
			{
				DBID dbid;
				this.mobObjMan.GetValue("mobEventsScripts", out dbid);
				if (!DBID.IsNullOrEmpty(dbid))
				{
					return MobScriptsForm.mainDb.GetManipulator(dbid);
				}
			}
			return null;
		}

		// Token: 0x06000B0E RID: 2830 RVA: 0x0005B284 File Offset: 0x0005A284
		private void OnLoad(object sender, EventArgs e)
		{
			if (MobScriptsForm.mainDb != null && this.loadDialogForm.ShowDialog(this) == DialogResult.OK)
			{
				DBID mobDBID = this.loadDialogForm.GetSelectedObject();
				if (!DBID.IsNullOrEmpty(mobDBID))
				{
					this.mobObjMan = MobScriptsForm.mainDb.GetManipulator(mobDBID);
					if (this.mobObjMan != null)
					{
						this.LoadData();
					}
				}
			}
		}

		// Token: 0x06000B0F RID: 2831 RVA: 0x0005B2DA File Offset: 0x0005A2DA
		private void OnSave(object sender, EventArgs e)
		{
			this.SaveData();
		}

		// Token: 0x06000B10 RID: 2832 RVA: 0x0005B2E4 File Offset: 0x0005A2E4
		private void LoadData()
		{
			this.saveButton.Enabled = false;
			this.modelViewerButton.Enabled = false;
			if (this.mobObjMan != null)
			{
				this.Text = string.Format("{0} ({1})", this.tile, this.mobObjMan.DBID);
				IObjMan scriptEventsMan = this.GetScriptMan();
				this.customFieldsWrapper.Load(scriptEventsMan);
				ControlContainer cc = new ControlContainer(this);
				foreach (object obj in cc)
				{
					Control control = (Control)obj;
					foreach (object obj2 in control.DataBindings)
					{
						Binding binding = (Binding)obj2;
						binding.ReadValue();
					}
				}
				foreach (KeyValuePair<string, SayActionListControl> pair in this.SayActionListControls)
				{
					pair.Value.LoadData((scriptEventsMan != null) ? scriptEventsMan.CreateManipulator(pair.Key) : null);
				}
				this.saveButton.Enabled = true;
				this.modelViewerButton.Enabled = true;
				this.tabPages.SelectedIndex = 0;
			}
		}

		// Token: 0x06000B11 RID: 2833 RVA: 0x0005B464 File Offset: 0x0005A464
		private void SaveData()
		{
			if (this.mobObjMan != null)
			{
				IObjMan scriptEventsMan = this.GetScriptMan();
				if (scriptEventsMan == null)
				{
					InputLanguage currLang = InputLanguage.CurrentInputLanguage;
					InputLanguage.CurrentInputLanguage = InputLanguage.DefaultInputLanguage;
					SaveFileDialog saveDialog = new SaveFileDialog();
					if (!string.IsNullOrEmpty(MobScriptsForm.initDirectoryPath) && Directory.Exists(MobScriptsForm.initDirectoryPath))
					{
						saveDialog.InitialDirectory = MobScriptsForm.initDirectoryPath.Replace('/', '\\');
					}
					saveDialog.FileName = this.mobObjMan.DBID.GetFileShortName();
					saveDialog.Filter = string.Format(".{0} files|*.{0}", "(MobEventsScripts).xdb");
					saveDialog.SupportMultiDottedExtensions = true;
					if (saveDialog.ShowDialog(this) == DialogResult.OK)
					{
						DBID dbid = IDatabase.CreateDBIDByName(saveDialog.FileName);
						if (!DBID.IsNullOrEmpty(dbid))
						{
							if (!MobScriptsForm.mainDb.DoesObjectExist(dbid))
							{
								scriptEventsMan = MobScriptsForm.mainDb.CreateNewObject("MobEventsScripts");
								if (!MobScriptsForm.mainDb.AddNewObject(dbid, scriptEventsMan))
								{
									scriptEventsMan = null;
								}
							}
						}
						else
						{
							scriptEventsMan = MobScriptsForm.mainDb.GetManipulator(dbid);
						}
						if (scriptEventsMan != null)
						{
							this.mobObjMan.SetValue("mobEventsScripts", dbid);
						}
					}
					InputLanguage.CurrentInputLanguage = currLang;
				}
				if (scriptEventsMan != null)
				{
					this.Cursor = Cursors.WaitCursor;
					this.customFieldsWrapper.Save(scriptEventsMan);
					foreach (KeyValuePair<string, SayActionListControl> pair in this.SayActionListControls)
					{
						pair.Value.Save(scriptEventsMan.CreateManipulator(pair.Key));
					}
					if (MobScriptsForm.mainDb != null)
					{
						MobScriptsForm.mainDb.SaveChanges();
					}
					this.Cursor = Cursors.Default;
				}
			}
		}

		// Token: 0x06000B12 RID: 2834 RVA: 0x0005B600 File Offset: 0x0005A600
		private static void OnParseFloat(object sender, ConvertEventArgs e)
		{
			if (e.Value != null)
			{
				float value;
				if (float.TryParse(e.Value.ToString(), NumberStyles.Float, CultureInfo.CurrentCulture, out value))
				{
					e.Value = value;
					return;
				}
				if (float.TryParse(e.Value.ToString(), NumberStyles.Float, CultureInfo.InvariantCulture, out value))
				{
					e.Value = value;
				}
			}
		}

		// Token: 0x06000B13 RID: 2835 RVA: 0x0005B66A File Offset: 0x0005A66A
		private void OnFrmClosed(object sender, FormClosedEventArgs e)
		{
			base.Context.StateContainer.UnbindState(this.formState);
		}

		// Token: 0x06000B14 RID: 2836 RVA: 0x0005B684 File Offset: 0x0005A684
		private void OnModelViewerClick(object sender, EventArgs e)
		{
			if (this.mobObjMan != null && !DBID.IsNullOrEmpty(this.mobObjMan.DBID))
			{
				DBID visMobDBID;
				this.mobObjMan.GetValue("visMob", out visMobDBID);
				base.Context.StateContainer.Invoke("_load_to_model_editor_by_string", new MethodArgs(this, visMobDBID.ToString(), null));
			}
		}

		// Token: 0x06000B15 RID: 2837 RVA: 0x0005B6E0 File Offset: 0x0005A6E0
		private void OnLoadByStringMessage(MethodArgs args)
		{
			string id = args.sender as string;
			if (MobScriptsForm.mainDb != null && !string.IsNullOrEmpty(id))
			{
				DBID dbid = MobScriptsForm.mainDb.GetDBIDByName(id);
				if (!DBID.IsNullOrEmpty(dbid))
				{
					IObjMan _mobObjMan = MobScriptsForm.mainDb.GetManipulator(dbid);
					if (_mobObjMan != null)
					{
						this.mobObjMan = _mobObjMan;
						if (!base.Visible)
						{
							base.Context.StateContainer.Invoke("toggle_visual_mob_scripts", new MethodArgs(this, null, null));
						}
						this.LoadData();
						base.Focus();
					}
				}
			}
		}

		// Token: 0x06000B16 RID: 2838 RVA: 0x0005B768 File Offset: 0x0005A768
		public MobScriptsForm(MainForm.Context context) : base(EditorEnvironment.EditorFormsFolder + "VisualMobScriptsForm.xml", context)
		{
			this.InitializeComponent();
			base.FormClosed += this.OnFrmClosed;
			this.tile = base.Text;
			this.loadButton.Click += this.OnLoad;
			this.saveButton.Click += this.OnSave;
			this.modelViewerButton.Click += this.OnModelViewerClick;
			this.mobHealthTextBox.DataBindings.Add("Text", this.customFieldsWrapper, "MobHealth");
			this.mobHealthTextBox.DataBindings[0].Parse += MobScriptsForm.OnParseFloat;
			this.enemyHealthTextBox.DataBindings.Add("Text", this.customFieldsWrapper, "EnemyHealth");
			this.enemyHealthTextBox.DataBindings[0].Parse += MobScriptsForm.OnParseFloat;
			this.idlePeriodMinTextBox.DataBindings.Add("Text", this.customFieldsWrapper, "IdlePeriodMin");
			this.idlePeriodMinTextBox.DataBindings[0].Parse += MobScriptsForm.OnParseFloat;
			this.idlePeriodMaxTextBox.DataBindings.Add("Text", this.customFieldsWrapper, "IdlePeriodMax");
			this.idlePeriodMaxTextBox.DataBindings[0].Parse += MobScriptsForm.OnParseFloat;
			this.autoplayDeathAnimCheckBox.DataBindings.Add("Checked", this.customFieldsWrapper, "AutoplayDeathAnim");
			this.autoplayDeathAnimCheckBox.DataBindings[0].Parse += MobScriptsForm.OnParseFloat;
			this.SayActionListControls = new Dictionary<string, SayActionListControl>(5);
			this.SayActionListControls.Add("aggroScript", this.aggroControl);
			this.SayActionListControls.Add("mobHealth.script", this.mobHealthControl);
			this.SayActionListControls.Add("enemyHealth.script", this.enemyHelthControl);
			this.SayActionListControls.Add("idleScriptParams.script", this.idleScirptControl);
			this.SayActionListControls.Add("deathScriptParams.script", this.deathScirptControl);
			this.formState.AddMethod("_load_in_script_events_editor", new Method(this.OnLoadByStringMessage));
			base.Context.StateContainer.BindState(this.formState);
		}

		// Token: 0x0400087B RID: 2171
		private readonly CustomFieldsWrapper customFieldsWrapper = new CustomFieldsWrapper();

		// Token: 0x0400087C RID: 2172
		private readonly Dictionary<string, SayActionListControl> SayActionListControls;

		// Token: 0x0400087D RID: 2173
		private static readonly IDatabase mainDb = IDatabase.GetMainDatabase();

		// Token: 0x0400087E RID: 2174
		private readonly LoadDialogForm loadDialogForm = new LoadDialogForm();

		// Token: 0x0400087F RID: 2175
		private static readonly string initDirectoryPath = EditorEnvironment.DataFolder + "World/Ask";

		// Token: 0x04000880 RID: 2176
		private readonly string tile;

		// Token: 0x04000881 RID: 2177
		private readonly State formState = new State("VisualMobScriptsFormtate");

		// Token: 0x04000882 RID: 2178
		private IObjMan mobObjMan;
	}
}
