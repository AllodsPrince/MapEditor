using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using InputState;
using LauncherTools.InputState;
using MapEditor.Forms.Base;
using MapEditor.Scene.Types;
using Tools.MapObjects;

namespace MapEditor.Forms.HiddenObjects
{
	// Token: 0x020001B8 RID: 440
	public partial class HiddenObjectsForm : BaseForm
	{
		// Token: 0x0600154B RID: 5451 RVA: 0x0009A9DC File Offset: 0x000999DC
		private void OnAddClick(object sender, EventArgs e)
		{
			base.Context.StateContainer.Invoke("mark_as_hidden", default(MethodArgs));
		}

		// Token: 0x0600154C RID: 5452 RVA: 0x0009AA07 File Offset: 0x00099A07
		private void OnRemoveClick(object sender, EventArgs e)
		{
			this.Delete();
		}

		// Token: 0x0600154D RID: 5453 RVA: 0x0009AA0F File Offset: 0x00099A0F
		private void OnClosed(object sender, EventArgs e)
		{
			base.Context.StateContainer.UnbindState(this.formState);
		}

		// Token: 0x0600154E RID: 5454 RVA: 0x0009AA28 File Offset: 0x00099A28
		private void OnSetObjectTransparency(MethodArgs args)
		{
			if (args.form != this)
			{
				List<IMapObject> mapObjects = args.sender as List<IMapObject>;
				if (mapObjects != null)
				{
					foreach (IMapObject mapObject in mapObjects)
					{
						for (int index = this.hiddenObjectsListBox.Items.Count - 1; index >= 0; index--)
						{
							HiddenObjectsForm.MapObjectWrap wrap = this.hiddenObjectsListBox.Items[index] as HiddenObjectsForm.MapObjectWrap;
							if (wrap != null && wrap.Id == mapObject.ID)
							{
								this.hiddenObjectsListBox.Items.RemoveAt(index);
							}
						}
					}
				}
			}
		}

		// Token: 0x0600154F RID: 5455 RVA: 0x0009AAE8 File Offset: 0x00099AE8
		private void OnSetObjectTypeTransparency(MethodArgs args)
		{
			if (args.form != this && args.sender is int)
			{
				int type = (int)args.sender;
				for (int index = this.hiddenObjectsListBox.Items.Count - 1; index >= 0; index--)
				{
					HiddenObjectsForm.MapObjectWrap wrap = this.hiddenObjectsListBox.Items[index] as HiddenObjectsForm.MapObjectWrap;
					if (wrap != null && wrap.Type == type)
					{
						this.hiddenObjectsListBox.Items.RemoveAt(index);
					}
				}
			}
		}

		// Token: 0x06001550 RID: 5456 RVA: 0x0009AB6B File Offset: 0x00099B6B
		private void OnSelectedIndexChanged(object sender, EventArgs e)
		{
			this.removeButton.Enabled = (this.hiddenObjectsListBox.SelectedItems.Count > 0);
		}

		// Token: 0x06001551 RID: 5457 RVA: 0x0009AB8B File Offset: 0x00099B8B
		private void OnKeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Delete)
			{
				this.Delete();
			}
		}

		// Token: 0x06001552 RID: 5458 RVA: 0x0009ABA0 File Offset: 0x00099BA0
		private void Delete()
		{
			if (this.hiddenObjectsListBox.SelectedIndices.Count > 0)
			{
				int cnt = this.hiddenObjectsListBox.SelectedIndices.Count;
				int[] indicies = new int[cnt];
				for (int index = 0; index < cnt; index++)
				{
					indicies[index] = this.hiddenObjectsListBox.SelectedIndices[index];
				}
				for (int index2 = cnt - 1; index2 >= 0; index2--)
				{
					HiddenObjectsForm.MapObjectWrap wrap = this.hiddenObjectsListBox.Items[index2] as HiddenObjectsForm.MapObjectWrap;
					if (wrap != null)
					{
						base.Context.EditorScene.SetObjectTransparency(wrap.Id, 1.0);
						this.hiddenObjectsListBox.Items.RemoveAt(index2);
					}
				}
			}
		}

		// Token: 0x06001553 RID: 5459 RVA: 0x0009AC56 File Offset: 0x00099C56
		private void OnCloseMap(MethodArgs args)
		{
			this.hiddenObjectsListBox.Items.Clear();
		}

		// Token: 0x06001554 RID: 5460 RVA: 0x0009AC68 File Offset: 0x00099C68
		public HiddenObjectsForm(MainForm.Context context) : base(EditorEnvironment.EditorFormsFolder + "HiddenObjectsForm.xml", context)
		{
			this.InitializeComponent();
			base.Closed += this.OnClosed;
			this.addButton.Click += this.OnAddClick;
			this.removeButton.Click += this.OnRemoveClick;
			this.formState.AddMethod("_set_map_object_transparency", new Method(this.OnSetObjectTransparency));
			this.formState.AddMethod("_set_map_object_type_transparency", new Method(this.OnSetObjectTypeTransparency));
			this.formState.AddMethod("_leave_map_edit_state", new Method(this.OnCloseMap));
			base.Context.StateContainer.BindState(this.formState);
			this.hiddenObjectsListBox.SelectedIndexChanged += this.OnSelectedIndexChanged;
			base.KeyDown += this.OnKeyDown;
		}

		// Token: 0x06001555 RID: 5461 RVA: 0x0009AD74 File Offset: 0x00099D74
		public void AddObject(IMapObject mapObject)
		{
			if (mapObject != null && !mapObject.Temporary)
			{
				this.hiddenObjectsListBox.Items.Add(new HiddenObjectsForm.MapObjectWrap(mapObject));
				base.Context.EditorScene.SetObjectTransparency(mapObject.ID, 0.0);
			}
		}

		// Token: 0x06001556 RID: 5462 RVA: 0x0009ADC4 File Offset: 0x00099DC4
		public bool ObjectIsHidden(IMapObject mapObject)
		{
			if (this.hiddenObjectsListBox.Items.Count > 0)
			{
				foreach (object item in this.hiddenObjectsListBox.Items)
				{
					HiddenObjectsForm.MapObjectWrap wrap = item as HiddenObjectsForm.MapObjectWrap;
					if (wrap != null && wrap.Id == mapObject.ID)
					{
						return true;
					}
				}
				return false;
			}
			return false;
		}

		// Token: 0x04000F0C RID: 3852
		private readonly State formState = new State("HiddenObjectsForm");

		// Token: 0x020001B9 RID: 441
		private class MapObjectWrap
		{
			// Token: 0x06001559 RID: 5465 RVA: 0x0009B058 File Offset: 0x0009A058
			public MapObjectWrap(IMapObject mapObject)
			{
				this.id = mapObject.ID;
				this.sceneName = mapObject.SceneName;
				this.type = MapSceneObjectTypeContainer.GetType(mapObject.Type.Type);
			}

			// Token: 0x0600155A RID: 5466 RVA: 0x0009B09C File Offset: 0x0009A09C
			public override string ToString()
			{
				return this.sceneName;
			}

			// Token: 0x17000421 RID: 1057
			// (get) Token: 0x0600155B RID: 5467 RVA: 0x0009B0A4 File Offset: 0x0009A0A4
			public int Id
			{
				get
				{
					return this.id;
				}
			}

			// Token: 0x17000422 RID: 1058
			// (get) Token: 0x0600155C RID: 5468 RVA: 0x0009B0AC File Offset: 0x0009A0AC
			public int Type
			{
				get
				{
					return this.type;
				}
			}

			// Token: 0x04000F11 RID: 3857
			private readonly int id;

			// Token: 0x04000F12 RID: 3858
			private readonly string sceneName;

			// Token: 0x04000F13 RID: 3859
			private readonly int type;
		}
	}
}
