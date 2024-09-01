using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using InputState;
using MapEditor.Forms.Base;
using MapEditor.Map.Containers;
using MapEditor.Scene;
using Tools.Groups;
using Tools.WindowParams;

namespace MapEditor.Forms.Groups
{
	// Token: 0x02000245 RID: 581
	public partial class GroupsForm : BaseForm
	{
		// Token: 0x06001BB0 RID: 7088 RVA: 0x000B492C File Offset: 0x000B392C
		private void SetGroupSelectionMode(GroupContainer.GroupSelectionMode groupSelectionMode)
		{
			this.created = false;
			if (groupSelectionMode == GroupContainer.GroupSelectionMode.Free)
			{
				this.SelectionType00RadioButton.Checked = true;
			}
			else if (groupSelectionMode == GroupContainer.GroupSelectionMode.OneLevel)
			{
				this.SelectionType01RadioButton.Checked = true;
			}
			else if (groupSelectionMode == GroupContainer.GroupSelectionMode.AllLevels)
			{
				this.SelectionType02RadioButton.Checked = true;
			}
			this.created = true;
		}

		// Token: 0x06001BB1 RID: 7089 RVA: 0x000B497A File Offset: 0x000B397A
		private void Find(MethodArgs methodArgs)
		{
			if (this.created && this.groupTreeController.Binded)
			{
				base.Context.MainState.ActiveState = 0;
				this.groupTreeController.Find();
			}
		}

		// Token: 0x06001BB2 RID: 7090 RVA: 0x000B49AD File Offset: 0x000B39AD
		private void FindNext(MethodArgs methodArgs)
		{
			if (this.created && this.groupTreeController.Binded)
			{
				base.Context.MainState.ActiveState = 0;
				this.groupTreeController.FindNext();
			}
		}

		// Token: 0x06001BB3 RID: 7091 RVA: 0x000B49E0 File Offset: 0x000B39E0
		private void FindPrevious(MethodArgs methodArgs)
		{
			if (this.created && this.groupTreeController.Binded)
			{
				base.Context.MainState.ActiveState = 0;
				this.groupTreeController.FindPrevious();
			}
		}

		// Token: 0x06001BB4 RID: 7092 RVA: 0x000B4A13 File Offset: 0x000B3A13
		private void OnPostLoadParams(FormParams formParams)
		{
			this.FilterComboBox.Text = string.Empty;
			this.created = true;
		}

		// Token: 0x06001BB5 RID: 7093 RVA: 0x000B4A2C File Offset: 0x000B3A2C
		private void UpdateFilterComboBox()
		{
			if (this.created && this.groupTreeController.Binded)
			{
				this.created = false;
				string newFilterItem = this.FilterComboBox.Text;
				if (!string.IsNullOrEmpty(newFilterItem) && !this.FilterComboBox.Items.Contains(newFilterItem))
				{
					this.FilterComboBox.Items.Insert(0, newFilterItem);
					if (this.FilterComboBox.Items.Count > GroupsForm.maxFiltersCount)
					{
						this.FilterComboBox.Items.RemoveAt(this.FilterComboBox.Items.Count - 1);
					}
				}
				this.groupTreeController.Filter = newFilterItem;
				this.created = true;
			}
		}

		// Token: 0x06001BB6 RID: 7094 RVA: 0x000B4AE0 File Offset: 0x000B3AE0
		private void FilterComboBox_TextChanged(object sender, EventArgs e)
		{
			if (this.created)
			{
				this.ComboBoxTimer.Start();
			}
		}

		// Token: 0x06001BB7 RID: 7095 RVA: 0x000B4AF5 File Offset: 0x000B3AF5
		private void ComboBoxTimer_Tick(object sender, EventArgs e)
		{
			this.ComboBoxTimer.Stop();
			this.UpdateFilterComboBox();
		}

		// Token: 0x06001BB8 RID: 7096 RVA: 0x000B4B08 File Offset: 0x000B3B08
		private void filterObjectToolStripMenuItem_Click(object sender, EventArgs e)
		{
			string filter = this.groupTreeController.GetFocusedItemText(this.contextMenuPoint);
			if (!string.IsNullOrEmpty(filter))
			{
				this.created = false;
				this.FilterComboBox.Text = filter;
				this.created = true;
				this.UpdateFilterComboBox();
			}
		}

		// Token: 0x06001BB9 RID: 7097 RVA: 0x000B4B4F File Offset: 0x000B3B4F
		private void clearFilterToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if (!string.IsNullOrEmpty(this.FilterComboBox.Text))
			{
				this.created = false;
				this.FilterComboBox.Text = string.Empty;
				this.created = true;
				this.UpdateFilterComboBox();
			}
		}

		// Token: 0x06001BBA RID: 7098 RVA: 0x000B4B88 File Offset: 0x000B3B88
		private void ObjectsTreeContextMenuStrip_Opening(object sender, CancelEventArgs e)
		{
			this.contextMenuPoint = new Point(this.ObjectsTreeContextMenuStrip.Left, this.ObjectsTreeContextMenuStrip.Top);
			this.filterObjectToolStripMenuItem.Enabled = !string.IsNullOrEmpty(this.groupTreeController.GetFocusedItemText(this.contextMenuPoint));
			this.clearFilterToolStripMenuItem.Enabled = !string.IsNullOrEmpty(this.FilterComboBox.Text);
		}

		// Token: 0x06001BBB RID: 7099 RVA: 0x000B4BF8 File Offset: 0x000B3BF8
		private void GroupButton_Click(object sender, EventArgs e)
		{
			if (this.created)
			{
				base.Context.MainState.ActiveState = 0;
				base.Context.StateContainer.Invoke("group_objects", new MethodArgs(this, null, null));
			}
		}

		// Token: 0x06001BBC RID: 7100 RVA: 0x000B4C30 File Offset: 0x000B3C30
		private void UngroupButton_Click(object sender, EventArgs e)
		{
			if (this.created)
			{
				base.Context.MainState.ActiveState = 0;
				base.Context.StateContainer.Invoke("ungroup_objects", new MethodArgs(this, null, null));
			}
		}

		// Token: 0x06001BBD RID: 7101 RVA: 0x000B4C68 File Offset: 0x000B3C68
		private void FlattenButton_Click(object sender, EventArgs e)
		{
			if (this.created)
			{
				base.Context.MainState.ActiveState = 0;
				base.Context.StateContainer.Invoke("flatten_objects", new MethodArgs(this, null, null));
			}
		}

		// Token: 0x06001BBE RID: 7102 RVA: 0x000B4CA0 File Offset: 0x000B3CA0
		private void CreateMultiobjectButton_Click(object sender, EventArgs e)
		{
			if (this.created)
			{
				base.Context.MainState.ActiveState = 0;
				base.Context.StateContainer.Invoke("copy_special", default(MethodArgs));
			}
		}

		// Token: 0x06001BBF RID: 7103 RVA: 0x000B4CE4 File Offset: 0x000B3CE4
		private void SelectionTypeRadioButton_CheckedChanged(object sender, EventArgs e)
		{
			if (this.created)
			{
				if (this.SelectionType00RadioButton.Checked)
				{
					base.Context.StateContainer.Invoke("group_selection_type_free", new MethodArgs(this, null, null));
					return;
				}
				if (this.SelectionType01RadioButton.Checked)
				{
					base.Context.StateContainer.Invoke("group_selection_type_one_level", new MethodArgs(this, null, null));
					return;
				}
				if (this.SelectionType02RadioButton.Checked)
				{
					base.Context.StateContainer.Invoke("group_selection_type_all_levels", new MethodArgs(this, null, null));
				}
			}
		}

		// Token: 0x06001BC0 RID: 7104 RVA: 0x000B4D7C File Offset: 0x000B3D7C
		public GroupsForm(MainForm.Context context) : base(EditorEnvironment.EditorFormsFolder + "GroupsForm.xml", context)
		{
			this.InitializeComponent();
			this.groupTreeController = new GroupTreeController(this.ObjectsTreeView, context);
			base.Context.StateContainer.AddMethod("find", new Method(this.Find));
			base.Context.StateContainer.AddMethod("find_next", new Method(this.FindNext));
			base.Context.StateContainer.AddMethod("find_previous", new Method(this.FindPrevious));
			if (base.ParamsSaver != null)
			{
				base.ParamsSaver.RegisterControl(this.FilterComboBox, false);
				base.ParamsSaver.PostLoadParams += this.OnPostLoadParams;
			}
		}

		// Token: 0x06001BC1 RID: 7105 RVA: 0x000B4E58 File Offset: 0x000B3E58
		public void Bind(GroupContainer groupContainer, MapEditorMapObjectContainer mapObjectContainer, MapObjectSelector selector)
		{
			if (this.groupTreeController != null)
			{
				this.groupTreeController.Bind(groupContainer, mapObjectContainer, selector);
			}
			this.SetGroupSelectionMode(selector.GroupSelectionMode);
		}

		// Token: 0x06001BC2 RID: 7106 RVA: 0x000B4E7C File Offset: 0x000B3E7C
		public void Unbind()
		{
			if (this.groupTreeController != null)
			{
				this.groupTreeController.Unbind();
			}
		}

		// Token: 0x040011E6 RID: 4582
		private bool created;

		// Token: 0x040011E7 RID: 4583
		private static readonly int maxFiltersCount = 25;

		// Token: 0x040011E8 RID: 4584
		private readonly GroupTreeController groupTreeController;

		// Token: 0x040011E9 RID: 4585
		private Point contextMenuPoint = new Point(0, 0);
	}
}
