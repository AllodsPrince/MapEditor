using System;
using System.ComponentModel;
using System.Windows.Forms;
using Db;
using InputState;
using LauncherTools.InputState;
using MapEditor.Forms.Base;
using Tools.RecentList;
using Tools.WindowParams;

namespace MapEditor.Forms.ScriptEditor
{
	// Token: 0x0200004C RID: 76
	public partial class ScriptEditorForm : BaseForm
	{
		// Token: 0x0600040B RID: 1035 RVA: 0x00021B7E File Offset: 0x00020B7E
		private void OnLoadParams(FormParams formParams)
		{
			this.scriptEditorControl.LoadParams(formParams, 1);
		}

		// Token: 0x0600040C RID: 1036 RVA: 0x00021B8D File Offset: 0x00020B8D
		private void OnSaveParams(FormParams formParams)
		{
			this.scriptEditorControl.SaveParams(formParams, 1);
		}

		// Token: 0x0600040D RID: 1037 RVA: 0x00021B9C File Offset: 0x00020B9C
		private void OnSelectedObjectChanged(DBID dbid)
		{
			this.Text = string.Format("{0}: {1}", this.caption, dbid);
			if (this.recentList != null && !DBID.IsNullOrEmpty(dbid))
			{
				this.recentList.AddItem(dbid.ToString());
			}
		}

		// Token: 0x0600040E RID: 1038 RVA: 0x00021BD8 File Offset: 0x00020BD8
		private void LoadByObjMan(IObjMan objMan)
		{
			if (base.Context != null && objMan != null)
			{
				if (!base.Visible)
				{
					base.Context.StateContainer.Invoke("toggle_script_editor", default(MethodArgs));
				}
				this.scriptEditorControl.SelectedObject = objMan;
				if (base.Visible)
				{
					base.Focus();
				}
			}
		}

		// Token: 0x0600040F RID: 1039 RVA: 0x00021C31 File Offset: 0x00020C31
		private void OnLoadByObjMan(MethodArgs args)
		{
			this.LoadByObjMan(args.sender as IObjMan);
		}

		// Token: 0x06000410 RID: 1040 RVA: 0x00021C48 File Offset: 0x00020C48
		private void OnLoadByString(MethodArgs args)
		{
			string sender = args.sender as string;
			if (sender != null)
			{
				DBID dbid = IDatabase.CreateDBIDByName(sender);
				if (!DBID.IsNullOrEmpty(dbid))
				{
					IDatabase mainDb = IDatabase.GetMainDatabase();
					if (mainDb != null)
					{
						this.LoadByObjMan(mainDb.GetManipulator(dbid));
					}
				}
			}
		}

		// Token: 0x06000411 RID: 1041 RVA: 0x00021C8C File Offset: 0x00020C8C
		private void OnRecentListItemClick(object sender, ToolStripItemClickedEventArgs e)
		{
			string tag = e.ClickedItem.Tag as string;
			if (this.recentList != null && !string.IsNullOrEmpty(tag))
			{
				string item;
				this.recentList.GetItem(tag, out item);
				if (!string.IsNullOrEmpty(item))
				{
					IDatabase mainDb = IDatabase.GetMainDatabase();
					if (mainDb != null)
					{
						DBID dbid = mainDb.GetDBIDByName(item);
						if (!DBID.IsNullOrEmpty(dbid))
						{
							this.scriptEditorControl.SelectedObject = mainDb.GetManipulator(dbid);
							this.scriptEditorControl.Invalidate();
						}
					}
				}
			}
		}

		// Token: 0x06000412 RID: 1042 RVA: 0x00021D07 File Offset: 0x00020D07
		private void OnLoadForm(object sender, EventArgs e)
		{
			this.caption = this.Text;
			if (this.recentList != null)
			{
				this.recentList.LoadRecentList();
			}
		}

		// Token: 0x06000413 RID: 1043 RVA: 0x00021D28 File Offset: 0x00020D28
		private void OnCloseForm(object sender, FormClosedEventArgs e)
		{
			base.Context.StateContainer.UnbindState(this.scriptEditorFormState);
			if (this.recentList != null)
			{
				this.recentList.SaveRecentList();
			}
		}

		// Token: 0x06000414 RID: 1044 RVA: 0x00021D54 File Offset: 0x00020D54
		public ScriptEditorForm(MainForm.Context _context) : base(EditorEnvironment.EditorFormsFolder + "ScriptEditorForm.xml", _context)
		{
			this.InitializeComponent();
			this.scriptEditorControl.Init(EditorEnvironment.EditorFormsFolder);
			this.recentList = new RecentList(EditorEnvironment.EditorFormsFolder + "ScriptEditor_RecentList.xml", this.historyToolStripMenuItem, new ScriptEditorForm.RecentListDataMiner());
			this.scriptEditorControl.SelectedObjectChanged += this.OnSelectedObjectChanged;
			base.Load += this.OnLoadForm;
			base.FormClosed += this.OnCloseForm;
			this.historyToolStripMenuItem.DropDownItemClicked += this.OnRecentListItemClick;
			if (base.ParamsSaver != null)
			{
				base.ParamsSaver.AutoregisterControls = false;
				this.scriptEditorControl.ParamSaverRegisterControl(base.ParamsSaver);
				base.ParamsSaver.LoadParams += this.OnLoadParams;
				base.ParamsSaver.SaveParams += this.OnSaveParams;
			}
			this.scriptEditorFormState.AddMethod("_load_to_script_editor", new Method(this.OnLoadByObjMan));
			this.scriptEditorFormState.AddMethod("_load_to_script_editor_by_string", new Method(this.OnLoadByString));
			base.Context.StateContainer.BindState(this.scriptEditorFormState);
		}

		// Token: 0x040002CD RID: 717
		private readonly State scriptEditorFormState = new State("ScriptEditorFormState");

		// Token: 0x040002CE RID: 718
		private readonly RecentList recentList;

		// Token: 0x040002CF RID: 719
		private string caption;

		// Token: 0x0200004D RID: 77
		private class RecentListDataMiner : RecentList.IItemDataMiner
		{
			// Token: 0x06000417 RID: 1047 RVA: 0x00022198 File Offset: 0x00021198
			public bool GetItemData(string item, out RecentList.ItemData itemData)
			{
				itemData = new RecentList.ItemData();
				IDatabase mainDb = IDatabase.GetMainDatabase();
				if (mainDb != null)
				{
					DBID dbid = mainDb.GetDBIDByName(item);
					if (!DBID.IsNullOrEmpty(dbid))
					{
						itemData.Text = dbid.GetFileShortName();
						return true;
					}
				}
				itemData.Text = string.Empty;
				return false;
			}
		}
	}
}
