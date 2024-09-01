using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using InputState;
using LauncherTools.InputState;
using MapEditor.Forms.Base;
using MapEditor.Properties;
using MapEditor.Resources.Strings;
using Tools.ControlValidators;
using Tools.DBGameObjects;
using Tools.DBGameObjects.GameObjects;
using Tools.ModelViewerElements.Controls.ChangeCharacter;
using Tools.ModelViewerElements.Controls.CharacterBasicVar;
using Tools.ModelViewerElements.Controls.CharacterProportionsVar;
using Tools.ModelViewerElements.Controls.Equipment;
using Tools.ModelViewerElements.Controls.RandomizeParams;
using Tools.ModelViewerElements.Controls.SelectVisMobExtElem;
using Tools.ModelViewerElements.Model.ModelTypes;
using Tools.ModelViewerElements.RecentListDataMiner;
using Tools.ModelViewerElements.States;
using Tools.RecentList;
using Tools.SaveAsXdbFileDialog;
using Tools.WindowParams;

namespace MapEditor.Forms.ModelViewer
{
	// Token: 0x02000016 RID: 22
	public partial class ModelViewerForm : BaseForm
	{
		// Token: 0x06000209 RID: 521 RVA: 0x00016CF4 File Offset: 0x00015CF4
		private void OnSaveParams(FormParams formParams)
		{
			if (this.modelViewerScene != null && !DBID.IsNullOrEmpty(this.modelViewerScene.GetOpenedModelDBID()))
			{
				base.ParamsSaver.FormParams.SetString(0, this.modelViewerScene.GetOpenedModelDBID().GetFileFolder(string.Empty));
			}
			base.ParamsSaver.FormParams.SetString(1, this.EquipmentControl.SaveLoadEqipmentFilePath);
		}

		// Token: 0x0600020A RID: 522 RVA: 0x00016D60 File Offset: 0x00015D60
		private void BindMenu()
		{
			if (this.toolStripItemEnumarable != null)
			{
				foreach (ToolStripMenuItem item in this.toolStripItemEnumarable.MenuItems)
				{
					item.Click += this.OnMenuItemClick;
				}
				foreach (ToolStripButton item2 in this.toolStripItemEnumarable.Buttons)
				{
					item2.Click += this.OnMenuItemClick;
				}
			}
			this.recentListToolStripMenuItem.DropDownItemClicked += this.OnRecentListItemClick;
		}

		// Token: 0x0600020B RID: 523 RVA: 0x00016E2C File Offset: 0x00015E2C
		private void OnMenuItemClick(object sender, EventArgs e)
		{
			ToolStripItem item = sender as ToolStripItem;
			if (item != null)
			{
				string tag = item.Tag as string;
				string key;
				if (!string.IsNullOrEmpty(tag) && (key = tag) != null)
				{
					if (<PrivateImplementationDetails>{A59FE84B-609F-4E89-8481-91990080F471}.$$method0x6000208-1 == null)
					{
						<PrivateImplementationDetails>{A59FE84B-609F-4E89-8481-91990080F471}.$$method0x6000208-1 = new Dictionary<string, int>(9)
						{
							{
								"toggle_load_model",
								0
							},
							{
								"toggle_save_model",
								1
							},
							{
								"toggle_save_as_model",
								2
							},
							{
								"toggle_camera_slow",
								3
							},
							{
								"toggle_camera_normal",
								4
							},
							{
								"toggle_camera_fast",
								5
							},
							{
								"toggle_camera_center",
								6
							},
							{
								"toggle_camera_maya",
								7
							},
							{
								"toggle_camera_fps",
								8
							}
						};
					}
					int num;
					if (<PrivateImplementationDetails>{A59FE84B-609F-4E89-8481-91990080F471}.$$method0x6000208-1.TryGetValue(key, out num))
					{
						switch (num)
						{
						case 0:
							this.OnBrowseModel();
							return;
						case 1:
							this.OnSaveModel();
							return;
						case 2:
							this.OnSaveAs();
							return;
						case 3:
							this.OnSetCameraSpeed(-1);
							return;
						case 4:
							this.OnSetCameraSpeed(0);
							return;
						case 5:
							this.OnSetCameraSpeed(1);
							return;
						case 6:
							this.OnCenterCamera();
							return;
						case 7:
							this.OnSetCameraType(0);
							return;
						case 8:
							this.OnSetCameraType(1);
							break;
						default:
							return;
						}
					}
				}
			}
		}

		// Token: 0x0600020C RID: 524 RVA: 0x00016F68 File Offset: 0x00015F68
		private void SetMenuItemEnabled(string tag, bool enabled)
		{
			if (this.toolStripItemEnumarable != null)
			{
				foreach (ToolStripMenuItem item in this.toolStripItemEnumarable.MenuItems)
				{
					if (item.Tag as string == tag)
					{
						item.Enabled = enabled;
					}
				}
				foreach (ToolStripButton item2 in this.toolStripItemEnumarable.Buttons)
				{
					if (item2.Tag as string == tag)
					{
						item2.Enabled = enabled;
					}
				}
			}
		}

		// Token: 0x0600020D RID: 525 RVA: 0x0001702C File Offset: 0x0001602C
		private void SetMenuItemChecked(string tag, bool _checked)
		{
			if (this.toolStripItemEnumarable != null)
			{
				foreach (ToolStripMenuItem item in this.toolStripItemEnumarable.MenuItems)
				{
					if (item.Tag as string == tag)
					{
						item.Checked = _checked;
					}
				}
				foreach (ToolStripButton item2 in this.toolStripItemEnumarable.Buttons)
				{
					if (item2.Tag as string == tag)
					{
						item2.Checked = _checked;
					}
				}
			}
		}

		// Token: 0x0600020E RID: 526 RVA: 0x000170F0 File Offset: 0x000160F0
		private void OnRecentListItemClick(object sender, ToolStripItemClickedEventArgs e)
		{
			string tag = e.ClickedItem.Tag as string;
			if (this.modelViewerScene != null && this.recentList != null && !string.IsNullOrEmpty(tag))
			{
				string item;
				this.recentList.GetItem(tag, out item);
				if (!string.IsNullOrEmpty(item))
				{
					this.modelViewerScene.OpenModel(item);
				}
			}
		}

		// Token: 0x0600020F RID: 527 RVA: 0x0001714C File Offset: 0x0001614C
		private void OnSetCameraSpeed(int speed)
		{
			if (base.ParamsSaver.FormParams != null)
			{
				base.ParamsSaver.FormParams.SetInt(0, speed);
			}
			if (this.modelViewerScene != null)
			{
				switch (speed)
				{
				case -1:
					this.modelViewerScene.SetCameraSpeed(10.0);
					this.SetMenuItemChecked("toggle_camera_slow", true);
					this.SetMenuItemChecked("toggle_camera_normal", false);
					this.SetMenuItemChecked("toggle_camera_fast", false);
					return;
				case 1:
					this.modelViewerScene.SetCameraSpeed(250.0);
					this.SetMenuItemChecked("toggle_camera_slow", false);
					this.SetMenuItemChecked("toggle_camera_normal", false);
					this.SetMenuItemChecked("toggle_camera_fast", true);
					return;
				}
				this.modelViewerScene.SetCameraSpeed(50.0);
				this.SetMenuItemChecked("toggle_camera_slow", false);
				this.SetMenuItemChecked("toggle_camera_normal", true);
				this.SetMenuItemChecked("toggle_camera_fast", false);
			}
		}

		// Token: 0x06000210 RID: 528 RVA: 0x00017245 File Offset: 0x00016245
		private void OnCenterCamera()
		{
			if (this.modelViewerScene != null)
			{
				this.modelViewerScene.CenterCamera();
			}
		}

		// Token: 0x06000211 RID: 529 RVA: 0x0001725C File Offset: 0x0001625C
		private void OnSetCameraType(int type)
		{
			if (base.ParamsSaver.FormParams != null)
			{
				base.ParamsSaver.FormParams.SetInt(1, type);
			}
			if (this.modelViewerScene != null)
			{
				if (type == 1)
				{
					this.modelViewerScene.SetCameraType("FPSType");
					this.SetMenuItemChecked("toggle_camera_maya", false);
					this.SetMenuItemChecked("toggle_camera_fps", true);
					return;
				}
				this.modelViewerScene.SetCameraType("MayaType");
				this.SetMenuItemChecked("toggle_camera_maya", true);
				this.SetMenuItemChecked("toggle_camera_fps", false);
			}
		}

		// Token: 0x06000212 RID: 530 RVA: 0x000172E8 File Offset: 0x000162E8
		private void OnPostLoadParams(FormParams formParams)
		{
			this.toolStripItemEnumarable = new ModelViewerForm.ToolStripItemEnumarable(this);
			this.SetMenuItemEnabled("toggle_save_model", false);
			this.SetMenuItemEnabled("toggle_save_as_model", false);
			this.SetMenuItemEnabled("toggle_model_open_scripts", false);
			if (this.modelViewerScene != null)
			{
				this.modelViewerScene.CreateScene();
				this.EquipmentControl.Init(this, this.modelViewerFormState, EditorEnvironment.EditorFormsFolder);
				GeneralView visualItems = new GeneralView("ModelViewerEquipmentView");
				DBMethods.LoadObjects(VisualItem.TypeName, visualItems, false);
				this.EquipmentControl.SetItemsView(visualItems);
				this.characterBasicVarControl.Init(this.modelViewerFormState, base.ParamsSaver);
				this.selectVisMobExtElemControl.Init(this.modelViewerFormState, base.ParamsSaver);
				this.changeCharacterControl.Init(this.modelViewerFormState);
				this.randomizeControl.Init();
				this.randomizeControl.AddParams(this.characterBasicVarControl);
				this.randomizeControl.AddParams(this.presets);
				this.BindMenu();
			}
			if (this.recentList != null)
			{
				this.recentList.LoadRecentList();
			}
			this.EquipmentControl.SaveLoadEqipmentFilePath = base.ParamsSaver.FormParams.GetString(1);
			this.OnSetCameraSpeed(base.ParamsSaver.FormParams.GetInt(0));
			this.OnSetCameraType(base.ParamsSaver.FormParams.GetInt(1));
		}

		// Token: 0x06000213 RID: 531 RVA: 0x00017444 File Offset: 0x00016444
		private void OnFormClosed(object sender, EventArgs e)
		{
			this.EquipmentControl.SaveConfigs();
			if (this.recentList != null)
			{
				this.recentList.SaveRecentList();
			}
			if (this.modelViewerFormState != null)
			{
				base.Context.StateContainer.UnbindState(this.modelViewerFormState);
			}
			if (this.modelViewerScene != null)
			{
				this.modelViewerScene.DestroyScene();
			}
		}

		// Token: 0x06000214 RID: 532 RVA: 0x000174A0 File Offset: 0x000164A0
		protected override void OnVisibleChanged(EventArgs e)
		{
			base.OnVisibleChanged(e);
			if (this.modelViewerScene != null)
			{
				if (base.Visible)
				{
					this.modelViewerScene.Start();
					return;
				}
				this.modelViewerScene.Stop();
			}
		}

		// Token: 0x06000215 RID: 533 RVA: 0x000174D0 File Offset: 0x000164D0
		private void OnSceneMouseEnter(object sender, EventArgs e)
		{
			this.ScenePanel.Focus();
		}

		// Token: 0x06000216 RID: 534 RVA: 0x000174E0 File Offset: 0x000164E0
		private void OnModelLoaded()
		{
			this.SetMenuItemEnabled("toggle_save_model", false);
			this.SetMenuItemEnabled("toggle_save_as_model", true);
			if (this.modelViewerScene != null)
			{
				string text = this.modelViewerScene.GetOpenedModelDBID().ToString();
				this.Text = this.modelViewerScene.GetOpenedModelDBID().ToString();
				this.SetMenuItemEnabled("toggle_model_open_scripts", this.modelViewerScene.GetOpenedModelType() == VisualMob.TypeName);
				if (this.recentList != null && !string.IsNullOrEmpty(text))
				{
					this.recentList.AddItem(text);
				}
			}
		}

		// Token: 0x06000217 RID: 535 RVA: 0x00017570 File Offset: 0x00016570
		private void OnModelModified(string type)
		{
			if (type == VisualMob.TypeName)
			{
				this.SetMenuItemEnabled("toggle_save_model", true);
			}
		}

		// Token: 0x06000218 RID: 536 RVA: 0x0001758C File Offset: 0x0001658C
		private void OnBrowseModel()
		{
			if (this.modelViewerScene != null)
			{
				OpenFileDialog openDialog = new OpenFileDialog();
				openDialog.SupportMultiDottedExtensions = true;
				openDialog.Filter = ".(VisualMob).xdb files|*.(VisualMob).xdb|.(VisCharacterTemplate).xdb files|*.(VisCharacterTemplate).xdb|.(VisObjectTemplate).xdb files|*.(VisObjectTemplate).xdb|.xdb files|*.xdb";
				openDialog.RestoreDirectory = true;
				openDialog.Multiselect = false;
				string initFolder = null;
				if (!DBID.IsNullOrEmpty(this.modelViewerScene.GetOpenedModelDBID()))
				{
					initFolder = this.modelViewerScene.GetOpenedModelDBID().GetFileFolder(string.Empty);
				}
				else if (base.ParamsSaver != null && base.ParamsSaver.FormParams != null)
				{
					initFolder = base.ParamsSaver.FormParams.GetString(0);
				}
				if (!string.IsNullOrEmpty(initFolder))
				{
					if (initFolder.IndexOf(':') == -1)
					{
						initFolder = EditorEnvironment.DataFolder + initFolder;
					}
					DirectoryInfo dir = new DirectoryInfo(initFolder);
					if (dir.Exists)
					{
						openDialog.InitialDirectory = dir.FullName;
					}
				}
				if (openDialog.ShowDialog(this) == DialogResult.OK)
				{
					this.modelViewerScene.OpenModel(openDialog.FileName);
				}
			}
		}

		// Token: 0x06000219 RID: 537 RVA: 0x00017672 File Offset: 0x00016672
		private void OnSaveModel()
		{
			if (this.modelViewerScene != null)
			{
				this.modelViewerScene.SaveModel();
				this.OnModelLoaded();
			}
		}

		// Token: 0x0600021A RID: 538 RVA: 0x00017690 File Offset: 0x00016690
		private void OnSaveAs()
		{
			if (this.modelViewerScene != null && !string.IsNullOrEmpty(this.modelViewerScene.SaveModelAsTargetType))
			{
				SaveAsXdbFileDialogForm createVisualMobDialog = new SaveAsXdbFileDialogForm(this.modelViewerScene.GetOpenedModelDBID().GetFileFolder(string.Empty), this.modelViewerScene.SaveModelAsTargetType);
				if (createVisualMobDialog.ShowDialog() == DialogResult.OK)
				{
					string visualMob = createVisualMobDialog.GetFileName();
					this.modelViewerScene.SaveAsModel(visualMob);
					this.modelViewerScene.OpenModel(visualMob);
				}
			}
		}

		// Token: 0x0600021B RID: 539 RVA: 0x00017705 File Offset: 0x00016705
		private void OnScaleValueChanged(object sender, EventArgs e)
		{
			this.ScaleFieldTimer.Stop();
			this.ScaleFieldTimer.Start();
		}

		// Token: 0x0600021C RID: 540 RVA: 0x0001771D File Offset: 0x0001671D
		private void OnScaleTimerTick(object sender, EventArgs e)
		{
			this.ValidateScale();
		}

		// Token: 0x0600021D RID: 541 RVA: 0x00017726 File Offset: 0x00016726
		private void OnScaleValidating(object sender, CancelEventArgs e)
		{
			if (!this.ValidateScale())
			{
				e.Cancel = true;
			}
		}

		// Token: 0x0600021E RID: 542 RVA: 0x00017738 File Offset: 0x00016738
		private bool ValidateScale()
		{
			this.ScaleFieldTimer.Stop();
			if (this.scaleTextBoxValidiator.ValidateText())
			{
				base.Context.StateContainer.Invoke("_model_viewer_set_model_scale", new MethodArgs(this, this.ScaleTextBox.Text, null));
				return true;
			}
			this.ScaleTextBox.Text = "1";
			return false;
		}

		// Token: 0x0600021F RID: 543 RVA: 0x00017798 File Offset: 0x00016798
		private void OnSetScale(MethodArgs args)
		{
			string scale = args.sender as string;
			if (scale != null)
			{
				this.ScaleTextBox.Text = scale;
			}
		}

		// Token: 0x06000220 RID: 544 RVA: 0x000177C1 File Offset: 0x000167C1
		private void OnBindProportions(MethodArgs args)
		{
			this.presets.Bind(args.sender as CharacterProportionsVarStateParams);
		}

		// Token: 0x06000221 RID: 545 RVA: 0x000177DA File Offset: 0x000167DA
		private void OnUnbindProportions(MethodArgs args)
		{
			this.presets.Bind(null);
		}

		// Token: 0x06000222 RID: 546 RVA: 0x000177E8 File Offset: 0x000167E8
		private void OnLoadByMessage(MethodArgs args)
		{
			string sender = args.sender as string;
			if (base.Context != null && this.modelViewerScene != null && sender != null && sender.EndsWith(".xdb"))
			{
				if (!base.Visible)
				{
					base.Context.StateContainer.Invoke("toggle_model_viewer", default(MethodArgs));
				}
				if (base.Visible)
				{
					this.modelViewerScene.OpenModel(sender);
					base.Focus();
				}
			}
		}

		// Token: 0x06000223 RID: 547 RVA: 0x00017864 File Offset: 0x00016864
		public ModelViewerForm(MainForm.Context _context) : base(EditorEnvironment.EditorFormsFolder + "ModelViewerForm.xml", _context)
		{
			this.InitializeComponent();
			this.presets.ReadOnly = true;
			base.ParamsSaver.AutoregisterControls = false;
			base.ParamsSaver.PostLoadParams += this.OnPostLoadParams;
			this.modelViewerScene = new ModelViewerScene(this.ScenePanel, this.SceneTimer, base.Context.StateContainer);
			this.modelViewerScene.ModelLoaded += this.OnModelLoaded;
			this.modelViewerScene.ModelModified += this.OnModelModified;
			base.Closed += this.OnFormClosed;
			base.ParamsSaver.SaveParams += this.OnSaveParams;
			this.ScenePanel.MouseEnter += this.OnSceneMouseEnter;
			this.ScaleTextBox.TextChanged += this.OnScaleValueChanged;
			this.ScaleTextBox.Validating += this.OnScaleValidating;
			this.ScaleFieldTimer.Tick += this.OnScaleTimerTick;
			this.scaleTextBoxValidiator = new FloatTextValidator(this.ScaleTextBox, Strings.MODEL_VIEWER_WRONG_FORMAT, Strings.MODEL_VIEWER_CAPTION);
			this.modelViewerFormState.AddMethod("_model_viewer_load_model_scale", new Method(this.OnSetScale));
			this.modelViewerFormState.AddMethod("_character_proportions_variation_bind_params", new Method(this.OnBindProportions));
			this.modelViewerFormState.AddMethod("_character_proportions_variation_unbind_params", new Method(this.OnUnbindProportions));
			this.modelViewerFormState.AddMethod("_load_to_model_editor_by_string", new Method(this.OnLoadByMessage));
			base.Context.StateContainer.BindState(this.modelViewerFormState);
			this.recentList = new RecentList(EditorEnvironment.EditorFormsFolder + "ModelViewer_Open_RecentList.xml", this.recentListToolStripMenuItem, new RecentListDataMiner());
		}

		// Token: 0x040001C4 RID: 452
		private const int intParamIndexCameraSpeed = 0;

		// Token: 0x040001C5 RID: 453
		private const int intParamIndexCameraType = 1;

		// Token: 0x040001C6 RID: 454
		private const int strParamIndexOpenFilePath = 0;

		// Token: 0x040001C7 RID: 455
		private const int strParamIndexEquipmentPath = 1;

		// Token: 0x040001C8 RID: 456
		private readonly ModelViewerScene modelViewerScene;

		// Token: 0x040001C9 RID: 457
		private readonly State modelViewerFormState = new State("ModelViewerFormState");

		// Token: 0x040001CA RID: 458
		private readonly FloatTextValidator scaleTextBoxValidiator;

		// Token: 0x040001CB RID: 459
		private ModelViewerForm.ToolStripItemEnumarable toolStripItemEnumarable;

		// Token: 0x040001CC RID: 460
		private readonly RecentList recentList;

		// Token: 0x02000017 RID: 23
		private class ToolStripItemEnumarable
		{
			// Token: 0x06000226 RID: 550 RVA: 0x00018F2C File Offset: 0x00017F2C
			public ToolStripItemEnumarable(ModelViewerForm _form)
			{
				this.form = _form;
				foreach (object obj in this.form.menuStrip.Items)
				{
					ToolStripMenuItem caption = (ToolStripMenuItem)obj;
					foreach (object obj2 in caption.DropDownItems)
					{
						ToolStripItem item = (ToolStripItem)obj2;
						ToolStripMenuItem _item = item as ToolStripMenuItem;
						if (_item != null)
						{
							this.menuItems.Add(_item);
						}
					}
				}
				foreach (object obj3 in this.form.toolStrip.Items)
				{
					ToolStripItem item2 = (ToolStripItem)obj3;
					ToolStripButton _item2 = item2 as ToolStripButton;
					if (_item2 != null)
					{
						this.buttons.Add(_item2);
					}
				}
			}

			// Token: 0x1700001E RID: 30
			// (get) Token: 0x06000227 RID: 551 RVA: 0x0001907C File Offset: 0x0001807C
			public IEnumerable<ToolStripMenuItem> MenuItems
			{
				get
				{
					return this.menuItems;
				}
			}

			// Token: 0x1700001F RID: 31
			// (get) Token: 0x06000228 RID: 552 RVA: 0x00019084 File Offset: 0x00018084
			public IEnumerable<ToolStripButton> Buttons
			{
				get
				{
					return this.buttons;
				}
			}

			// Token: 0x040001FF RID: 511
			private readonly ModelViewerForm form;

			// Token: 0x04000200 RID: 512
			private readonly List<ToolStripMenuItem> menuItems = new List<ToolStripMenuItem>();

			// Token: 0x04000201 RID: 513
			private readonly List<ToolStripButton> buttons = new List<ToolStripButton>();
		}
	}
}
