using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using Db;
using MapEditor.Resources.Strings;
using Tools.ControlValidators;
using Tools.DBGameObjects.GameObjects;
using Tools.Geometry;

namespace MapEditor.Forms.Quests
{
	// Token: 0x02000050 RID: 80
	public partial class SetLocatorsForm : Form
	{
		// Token: 0x06000427 RID: 1063 RVA: 0x000230CA File Offset: 0x000220CA
		private void OnOkClick(object sender, EventArgs e)
		{
			if (this.locatorEditor.Check())
			{
				base.DialogResult = DialogResult.OK;
			}
		}

		// Token: 0x06000428 RID: 1064 RVA: 0x000230E0 File Offset: 0x000220E0
		private void Init()
		{
			this.okButton.Click += this.OnOkClick;
		}

		// Token: 0x06000429 RID: 1065 RVA: 0x000230FC File Offset: 0x000220FC
		public SetLocatorsForm(bool autoSet, QuestClass.QuestLocator locator)
		{
			this.InitializeComponent();
			this.locatorEditor = new SetLocatorsForm.LocatorEditor(this, this.autoSetReturnCheckBox, this.returnLocatorPanel, this.returnZoneTextBox, this.returnXTextBox, this.returnYTextBox, this.returnZoneButton, this.clearReturnButton);
			if (locator != null)
			{
				this.locatorEditor.Load(autoSet, locator);
			}
			this.Init();
		}

		// Token: 0x0600042A RID: 1066 RVA: 0x00023164 File Offset: 0x00022164
		public SetLocatorsForm(QuestClass.QuestLocator locator)
		{
			this.InitializeComponent();
			this.locatorEditor = new SetLocatorsForm.LocatorEditor(this, this.autoSetReturnCheckBox, this.returnLocatorPanel, this.returnZoneTextBox, this.returnXTextBox, this.returnYTextBox, this.returnZoneButton, this.clearReturnButton);
			if (locator != null)
			{
				this.locatorEditor.Load(locator);
			}
			this.Init();
		}

		// Token: 0x0600042B RID: 1067 RVA: 0x000231C8 File Offset: 0x000221C8
		public void GetValue(out bool autoSet, QuestClass.QuestLocator locator)
		{
			this.locatorEditor.GetValue(out autoSet, locator);
		}

		// Token: 0x0600042C RID: 1068 RVA: 0x000231D7 File Offset: 0x000221D7
		public void GetValue(QuestClass.QuestLocator locator)
		{
			this.locatorEditor.GetValue(locator);
		}

		// Token: 0x040002F1 RID: 753
		private static readonly IDatabase mainDb = IDatabase.GetMainDatabase();

		// Token: 0x040002F2 RID: 754
		private readonly SetLocatorsForm.LocatorEditor locatorEditor;

		// Token: 0x02000051 RID: 81
		private class LocatorEditor
		{
			// Token: 0x0600042E RID: 1070 RVA: 0x000231F1 File Offset: 0x000221F1
			private void OnAutoCheckChanged(object sender, EventArgs e)
			{
				this.panel.Enabled = !this.autoSetChechBox.Checked;
			}

			// Token: 0x0600042F RID: 1071 RVA: 0x0002320C File Offset: 0x0002220C
			private void OnBrowseZoneClick(object sender, EventArgs e)
			{
				OpenFileDialog openDialog = new OpenFileDialog();
				openDialog.Filter = ".(ZoneResource).xdb files|*.(ZoneResource).xdb|.xdb files|*.xdb";
				if (!string.IsNullOrEmpty(this.zoneTextBox.Text))
				{
					DBID dbid = SetLocatorsForm.mainDb.GetDBIDByName(this.zoneTextBox.Text);
					if (!DBID.IsNullOrEmpty(dbid))
					{
						openDialog.InitialDirectory = dbid.GetFileFolder(EditorEnvironment.DataFolder);
					}
				}
				openDialog.RestoreDirectory = true;
				openDialog.Multiselect = false;
				if (openDialog.ShowDialog(this.parent) == DialogResult.OK)
				{
					DBID dbid2 = SetLocatorsForm.mainDb.GetDBIDByName(openDialog.FileName);
					if (!DBID.IsNullOrEmpty(dbid2))
					{
						this.zoneTextBox.Text = dbid2.ToString();
					}
				}
			}

			// Token: 0x06000430 RID: 1072 RVA: 0x000232B2 File Offset: 0x000222B2
			private void OnClearClick(object sender, EventArgs e)
			{
				this.zoneTextBox.Clear();
				this.xTextBox.Text = "0";
				this.yTextBox.Text = "0";
			}

			// Token: 0x06000431 RID: 1073 RVA: 0x000232E0 File Offset: 0x000222E0
			public LocatorEditor(Form _parent, CheckBox _autoSetChechBox, Panel _panel, TextBox _zoneTextBox, TextBox _xTextBox, TextBox _yTextBox, Button _browseZoneButton, Button _clearButton)
			{
				this.parent = _parent;
				this.autoSetChechBox = _autoSetChechBox;
				this.panel = _panel;
				this.zoneTextBox = _zoneTextBox;
				this.xTextBox = _xTextBox;
				this.yTextBox = _yTextBox;
				this.browseZoneButton = _browseZoneButton;
				this.clearButton = _clearButton;
				this.xValidator = new DoubleTextValidator(this.xTextBox, Strings.QUEST_EDITOR_WRONGFORMATERROR_MESSAGE, Strings.QUEST_EDITOR_ERROR_TITLE);
				this.yValidator = new DoubleTextValidator(this.yTextBox, Strings.QUEST_EDITOR_WRONGFORMATERROR_MESSAGE, Strings.QUEST_EDITOR_ERROR_TITLE);
				this.autoSetChechBox.CheckedChanged += this.OnAutoCheckChanged;
				this.browseZoneButton.Click += this.OnBrowseZoneClick;
				this.clearButton.Click += this.OnClearClick;
			}

			// Token: 0x06000432 RID: 1074 RVA: 0x000233AC File Offset: 0x000223AC
			public void Load(QuestClass.QuestLocator locator)
			{
				if (locator != null)
				{
					this.autoSetChechBox.Visible = false;
					this.clearButton.Visible = false;
					this.zoneTextBox.Text = (DBID.IsNullOrEmpty(locator.Zone) ? string.Empty : locator.Zone.ToString());
					this.xTextBox.Text = locator.Position.X.ToString();
					this.yTextBox.Text = locator.Position.Y.ToString();
				}
			}

			// Token: 0x06000433 RID: 1075 RVA: 0x00023444 File Offset: 0x00022444
			public void Load(bool autoCheck, QuestClass.QuestLocator locator)
			{
				if (locator != null)
				{
					this.autoSetChechBox.Visible = true;
					this.clearButton.Visible = true;
					this.autoSetChechBox.Checked = autoCheck;
					this.zoneTextBox.Text = (DBID.IsNullOrEmpty(locator.Zone) ? string.Empty : locator.Zone.ToString());
					this.xTextBox.Text = locator.Position.X.ToString();
					this.yTextBox.Text = locator.Position.Y.ToString();
				}
			}

			// Token: 0x06000434 RID: 1076 RVA: 0x000234E7 File Offset: 0x000224E7
			public bool Check()
			{
				return this.xValidator.ValidateText() && this.yValidator.ValidateText();
			}

			// Token: 0x06000435 RID: 1077 RVA: 0x00023503 File Offset: 0x00022503
			public void GetValue(out bool autoCheck, QuestClass.QuestLocator locator)
			{
				autoCheck = this.autoSetChechBox.Checked;
				this.GetValue(locator);
			}

			// Token: 0x06000436 RID: 1078 RVA: 0x0002351C File Offset: 0x0002251C
			public void GetValue(QuestClass.QuestLocator locator)
			{
				if (locator != null)
				{
					locator.Zone = SetLocatorsForm.mainDb.GetDBIDByName(this.zoneTextBox.Text);
					locator.Position = new Vec3(double.Parse(this.xTextBox.Text), double.Parse(this.yTextBox.Text), 0.0);
				}
			}

			// Token: 0x040002F3 RID: 755
			private readonly Form parent;

			// Token: 0x040002F4 RID: 756
			private readonly CheckBox autoSetChechBox;

			// Token: 0x040002F5 RID: 757
			private readonly Panel panel;

			// Token: 0x040002F6 RID: 758
			private readonly TextBox zoneTextBox;

			// Token: 0x040002F7 RID: 759
			private readonly TextBox xTextBox;

			// Token: 0x040002F8 RID: 760
			private readonly TextBox yTextBox;

			// Token: 0x040002F9 RID: 761
			private readonly Button browseZoneButton;

			// Token: 0x040002FA RID: 762
			private readonly Button clearButton;

			// Token: 0x040002FB RID: 763
			private readonly DoubleTextValidator xValidator;

			// Token: 0x040002FC RID: 764
			private readonly DoubleTextValidator yValidator;
		}
	}
}
