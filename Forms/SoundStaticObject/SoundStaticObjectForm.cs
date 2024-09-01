using System;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using System.Windows.Forms.UITypeEditors.FMODBrowser;
using Db;
using MapEditor.Forms.Base;
using MapEditor.Resources.Strings;

namespace MapEditor.Forms.SoundStaticObject
{
	// Token: 0x02000052 RID: 82
	public partial class SoundStaticObjectForm : BaseForm
	{
		// Token: 0x06000439 RID: 1081 RVA: 0x00023A8C File Offset: 0x00022A8C
		private void OnDragDrop(object sender, DragEventArgs e)
		{
			Array array = e.Data.GetData(DataFormats.FileDrop) as Array;
			if (array != null && array.GetValue(0) != null)
			{
				IDatabase mainDb = IDatabase.GetMainDatabase();
				if (mainDb != null)
				{
					string objectFileName = array.GetValue(0).ToString();
					if (objectFileName.EndsWith(".xdb"))
					{
						DBID objectDBID = mainDb.GetDBIDByName(objectFileName);
						if (!DBID.IsNullOrEmpty(objectDBID) && mainDb.GetClassTypeName(objectDBID) == "FMODProject")
						{
							this.LoadSound(objectDBID.GetFilePath(EditorEnvironment.DataFolder), string.Empty);
						}
					}
				}
			}
			base.Activate();
		}

		// Token: 0x0600043A RID: 1082 RVA: 0x00023B1D File Offset: 0x00022B1D
		private static void OnDragEnter(object sender, DragEventArgs e)
		{
			e.Effect = (e.Data.GetDataPresent(DataFormats.FileDrop) ? DragDropEffects.Copy : DragDropEffects.None);
		}

		// Token: 0x0600043B RID: 1083 RVA: 0x00023B3B File Offset: 0x00022B3B
		private void OnBrowseClick(object sender, EventArgs e)
		{
			this.LoadSound((!string.IsNullOrEmpty(this.projectTextBox.Text)) ? (EditorEnvironment.DataFolder + this.projectTextBox.Text) : null, this.eventTextBox.Text);
		}

		// Token: 0x0600043C RID: 1084 RVA: 0x00023B78 File Offset: 0x00022B78
		private void LoadSound(string fmodProj, string name)
		{
			FMODBrowser fmodDlg = new FMODBrowser3D(fmodProj, name ?? string.Empty);
			fmodDlg.ShowDialog();
			if (fmodDlg.IsEventExists)
			{
				this.projectTextBox.Text = fmodDlg.FMODProjectName;
				this.eventTextBox.Text = fmodDlg.FMODEventName;
				this.resultObjNameTextBox.Text = fmodDlg.FMODEventName.Substring(fmodDlg.FMODEventName.LastIndexOf('/') + 1);
			}
		}

		// Token: 0x0600043D RID: 1085 RVA: 0x00023BEC File Offset: 0x00022BEC
		private bool CheckNewDBID(IDatabase mainDb, string fileName, out DBID dbid)
		{
			dbid = IDatabase.CreateDBIDByName(SoundStaticObjectForm.folderPath + fileName);
			if (DBID.IsNullOrEmpty(dbid))
			{
				MessageBox.Show(this, string.Format(Strings.SOUND_STATIC_OBJECT_CREATE_ERROR, fileName), this.Text, MessageBoxButtons.OK, MessageBoxIcon.Hand);
				return false;
			}
			if (mainDb.DoesObjectExist(dbid))
			{
				MessageBox.Show(this, string.Format(Strings.SOUND_STATIC_OBJECT_EXSTS_ERROR, dbid.ToString()), this.Text, MessageBoxButtons.OK, MessageBoxIcon.Hand);
				return false;
			}
			return true;
		}

		// Token: 0x0600043E RID: 1086 RVA: 0x00023C5F File Offset: 0x00022C5F
		private bool CreateObject(IDatabase mainDb, string type, DBID dbid, out IObjMan objMan)
		{
			objMan = mainDb.CreateNewObject(type);
			if (objMan == null || !mainDb.AddNewObject(dbid, objMan))
			{
				MessageBox.Show(this, string.Format(Strings.SOUND_STATIC_OBJECT_CREATE_ERROR, dbid.ToString()), this.Text, MessageBoxButtons.OK, MessageBoxIcon.Hand);
				return false;
			}
			return true;
		}

		// Token: 0x0600043F RID: 1087 RVA: 0x00023CA0 File Offset: 0x00022CA0
		private void OnCreateClick(object sender, EventArgs e)
		{
			string project = this.projectTextBox.Text;
			string eventName = this.eventTextBox.Text;
			string fileName = this.resultObjNameTextBox.Text;
			if (string.IsNullOrEmpty(project) || string.IsNullOrEmpty(eventName) || string.IsNullOrEmpty(fileName))
			{
				MessageBox.Show(this, Strings.SOUND_STATIC_OBJECT_FIELDS_ERROR, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Hand);
				return;
			}
			if (!Directory.Exists(SoundStaticObjectForm.folderPath))
			{
				Directory.CreateDirectory(SoundStaticObjectForm.folderPath);
			}
			IDatabase mainDb = IDatabase.GetMainDatabase();
			DBID projectDBID = mainDb.GetDBIDByName(project);
			if (DBID.IsNullOrEmpty(projectDBID))
			{
				MessageBox.Show(this, string.Format(Strings.SOUND_STATIC_OBJECT_CREATE_ERROR, projectDBID.ToString()), this.Text, MessageBoxButtons.OK, MessageBoxIcon.Hand);
			}
			DBID visObjectDBID;
			DBID staticObjectDBID;
			if (!this.CheckNewDBID(mainDb, fileName + ".(VisObjectTemplate).xdb", out visObjectDBID) || !this.CheckNewDBID(mainDb, fileName + ".(StaticObject).xdb", out staticObjectDBID))
			{
				return;
			}
			IObjMan visObjectMan;
			IObjMan staticObjectMan;
			if (!this.CreateObject(mainDb, "VisObjectTemplate", visObjectDBID, out visObjectMan) || !this.CreateObject(mainDb, "mapLoader.StaticObject", staticObjectDBID, out staticObjectMan))
			{
				this.Cursor = Cursors.Default;
				return;
			}
			visObjectMan.SetValue("sound.project", projectDBID);
			visObjectMan.SetValue("sound.name", eventName);
			staticObjectMan.SetValue("ObjectTemplate", visObjectDBID);
			this.Cursor = Cursors.WaitCursor;
			mainDb.SaveChanges();
			this.Cursor = Cursors.Default;
			MessageBox.Show(this, Strings.SOUND_STATIC_OBJECT_CREATED_SUCCESSFULLY, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
		}

		// Token: 0x06000440 RID: 1088 RVA: 0x00023E08 File Offset: 0x00022E08
		public SoundStaticObjectForm(MainForm.Context context) : base(EditorEnvironment.EditorFormsFolder + "SoundStaticObjectForm.xml", context)
		{
			this.InitializeComponent();
			base.ParamsSaver.AutoregisterControls = false;
			base.DragEnter += SoundStaticObjectForm.OnDragEnter;
			base.DragDrop += this.OnDragDrop;
			this.brouseButton.Click += this.OnBrowseClick;
			this.createButton.Click += this.OnCreateClick;
		}

		// Token: 0x04000307 RID: 775
		private static readonly string folderPath = EditorEnvironment.DataFolder + "SFX/StaticObjects/";
	}
}
