using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using System.Windows.Forms.Consistency;
using Db;
using MapEditor.Forms.Base;
using MapEditor.Forms.PropertyControl.ExtendedPropertyControl;
using MapEditor.Resources.Images;
using MapEditor.Resources.Strings;

namespace MapEditor.Forms.PropertyControl
{
	// Token: 0x02000110 RID: 272
	public partial class PropertyControlForm : BaseForm
	{
		// Token: 0x06000D41 RID: 3393 RVA: 0x0006F170 File Offset: 0x0006E170
		private void OnSelectXDBObject(PropertyControl sender, DBID dbid)
		{
			this.Text = (DBID.IsNullOrEmpty(dbid) ? Strings.DATABASE_BROWSER_TITLE : dbid.ToString());
		}

		// Token: 0x06000D42 RID: 3394 RVA: 0x0006F18D File Offset: 0x0006E18D
		private void OnAlwaysOnTopCheckedChanged(object sender, EventArgs e)
		{
			base.TopMost = this.alwaysOnTopButton.Checked;
		}

		// Token: 0x06000D43 RID: 3395 RVA: 0x0006F1A0 File Offset: 0x0006E1A0
		public PropertyControlForm(MainForm.Context context) : base(EditorEnvironment.EditorFormsFolder + "PropertyControlForm.xml", context)
		{
			this.InitializeComponent();
			this.errorsButtonProc = new ErrorsButtonProc(this.errorsButton, this);
			this.errorsButtonProc.HideIfEmpty = false;
			this.propertyControl.StateContainer = context.StateContainer;
			this.propertyControl.AddButton("_load_to_script_editor", Strings.PROPERTY_CONTROL_SCRIPT_EDITOR, Images.script_diagram);
			this.alwaysOnTopButton = this.propertyControl.AddButton("_always_on_top", "Always on top", Images.always_on_top);
			this.alwaysOnTopButton.CheckedChanged += this.OnAlwaysOnTopCheckedChanged;
			this.alwaysOnTopButton.Name = "_always_on_top";
			this.alwaysOnTopButton.CheckOnClick = true;
			this.alwaysOnTopButton.ImageTransparentColor = Color.Magenta;
			base.TopMost = this.alwaysOnTopButton.Checked;
			this.propertyControl.OnSelectXDBObject += this.OnSelectXDBObject;
			base.ParamsSaver.AutoregisterControls = false;
			base.ParamsSaver.RegisterControl(this.alwaysOnTopButton);
		}

		// Token: 0x17000277 RID: 631
		// (get) Token: 0x06000D44 RID: 3396 RVA: 0x0006F2BB File Offset: 0x0006E2BB
		public PropertyControl PropertyControl
		{
			get
			{
				return this.propertyControl;
			}
		}

		// Token: 0x06000D45 RID: 3397 RVA: 0x0006F2C4 File Offset: 0x0006E2C4
		private void propertyControl_DragDrop(object sender, DragEventArgs e)
		{
			Array array = e.Data.GetData(DataFormats.FileDrop) as Array;
			if (array != null && array.GetValue(0) != null)
			{
				IDatabase mainDb = IDatabase.GetMainDatabase();
				if (mainDb != null)
				{
					string objectFileName = array.GetValue(0).ToString();
					objectFileName = objectFileName.Replace('\\', '/');
					if (objectFileName.StartsWith(EditorEnvironment.DataFolder, StringComparison.OrdinalIgnoreCase))
					{
						objectFileName = objectFileName.Substring(EditorEnvironment.DataFolder.Length);
					}
					DBID objectDBID = IDatabase.CreateDBIDByName(objectFileName);
					IObjMan objectMan = mainDb.GetManipulator(objectDBID);
					if (objectMan != null)
					{
						this.propertyControl.SelectedObject = objectMan;
					}
				}
			}
			base.Activate();
		}

		// Token: 0x06000D46 RID: 3398 RVA: 0x0006F35A File Offset: 0x0006E35A
		private void propertyControl_DragEnter(object sender, DragEventArgs e)
		{
			e.Effect = (e.Data.GetDataPresent(DataFormats.FileDrop) ? DragDropEffects.Copy : DragDropEffects.None);
		}

		// Token: 0x06000D47 RID: 3399 RVA: 0x0006F378 File Offset: 0x0006E378
		private void CloseButton_Click(object sender, EventArgs e)
		{
			base.Close();
		}

		// Token: 0x04000AA3 RID: 2723
		private readonly ErrorsButtonProc errorsButtonProc;

		// Token: 0x04000AA4 RID: 2724
		private readonly ToolStripButton alwaysOnTopButton;
	}
}
