using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;
using Db;
using MapEditor.Forms.Base;
using Tools.DBGameObjects;
using Tools.MapObjects;

namespace MapEditor.Forms.UnselectableObjects
{
	// Token: 0x0200001B RID: 27
	public partial class UnselectableObjectsForm : BaseForm
	{
		// Token: 0x06000240 RID: 576 RVA: 0x000196A0 File Offset: 0x000186A0
		private void Save()
		{
			List<string> _objects = new List<string>(this.objects.Count);
			foreach (GameObjectClass go in this.objects)
			{
				_objects.Add(go.GameObject);
			}
			Serializer.Save(UnselectableObjectsForm.unselectableObjectsListPath, _objects, false);
		}

		// Token: 0x06000241 RID: 577 RVA: 0x00019718 File Offset: 0x00018718
		private bool AddObject(IDatabase mainDb, string key)
		{
			if (!string.IsNullOrEmpty(key))
			{
				DBID dbid = mainDb.GetDBIDByName(key);
				if (!DBID.IsNullOrEmpty(dbid))
				{
					GameObjectClass go = new GameObjectClass(dbid);
					foreach (GameObjectClass _go in this.objects)
					{
						if (_go.GameObject == go.GameObject)
						{
							return false;
						}
					}
					this.objects.Add(go);
					this.objectsListBox.DataSource = null;
					this.objectsListBox.DataSource = this.objects;
					return true;
				}
			}
			return false;
		}

		// Token: 0x06000242 RID: 578 RVA: 0x000197CC File Offset: 0x000187CC
		private void Delete()
		{
			List<GameObjectClass> selectedObjects = new List<GameObjectClass>();
			foreach (object obj in this.objectsListBox.SelectedItems)
			{
				GameObjectClass selectedObject = (GameObjectClass)obj;
				selectedObjects.Add(selectedObject);
			}
			foreach (GameObjectClass objectClass in selectedObjects)
			{
				this.objects.Remove(objectClass);
			}
			this.objectsListBox.DataSource = null;
			this.objectsListBox.DataSource = this.objects;
			this.Save();
		}

		// Token: 0x06000243 RID: 579 RVA: 0x0001989C File Offset: 0x0001889C
		private void OnDeleteClick(object sender, EventArgs e)
		{
			this.Delete();
		}

		// Token: 0x06000244 RID: 580 RVA: 0x000198A4 File Offset: 0x000188A4
		private void OnKeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Delete)
			{
				this.Delete();
			}
		}

		// Token: 0x06000245 RID: 581 RVA: 0x000198B6 File Offset: 0x000188B6
		private void OnLoadForm(object sender, EventArgs e)
		{
			this.CheckObjectListIsLoaded();
		}

		// Token: 0x06000246 RID: 582 RVA: 0x000198C0 File Offset: 0x000188C0
		private void CheckObjectListIsLoaded()
		{
			if (this.objects == null)
			{
				this.objects = new List<GameObjectClass>();
				List<string> _objects = Serializer.Load<List<string>>(UnselectableObjectsForm.unselectableObjectsListPath);
				if (_objects != null)
				{
					IDatabase mainDb = IDatabase.GetMainDatabase();
					foreach (string obj in _objects)
					{
						this.AddObject(mainDb, obj);
					}
				}
			}
		}

		// Token: 0x06000247 RID: 583 RVA: 0x00019938 File Offset: 0x00018938
		public UnselectableObjectsForm(MainForm.Context context) : base(EditorEnvironment.EditorFormsFolder + "UnselectableObjectsForm.xml", context)
		{
			this.InitializeComponent();
			this.objectsListBox.DataSource = this.objects;
			base.Load += this.OnLoadForm;
			this.deleteButton.Click += this.OnDeleteClick;
			this.objectsListBox.KeyDown += this.OnKeyDown;
			base.KeyDown += this.OnKeyDown;
		}

		// Token: 0x06000248 RID: 584 RVA: 0x000199C4 File Offset: 0x000189C4
		public void AddObject(IEnumerable<IMapObject> mapObjects)
		{
			this.CheckObjectListIsLoaded();
			bool result = false;
			foreach (IMapObject mapObject in mapObjects)
			{
				result |= this.AddObject(IDatabase.GetMainDatabase(), mapObject.SceneName);
			}
			if (result)
			{
				this.Save();
			}
		}

		// Token: 0x06000249 RID: 585 RVA: 0x00019A2C File Offset: 0x00018A2C
		public bool ObjectIsUnselectable(IMapObject mapObject)
		{
			this.CheckObjectListIsLoaded();
			if (this.objects.Count > 0 && mapObject != null && !string.IsNullOrEmpty(mapObject.SceneName))
			{
				IDatabase mainDb = IDatabase.GetMainDatabase();
				DBID dbid = mainDb.GetDBIDByName(mapObject.SceneName);
				if (!DBID.IsNullOrEmpty(dbid))
				{
					GameObjectClass go = new GameObjectClass(dbid);
					foreach (GameObjectClass _go in this.objects)
					{
						if (_go.GameObject == go.GameObject)
						{
							return true;
						}
					}
					return false;
				}
			}
			return false;
		}

		// Token: 0x04000212 RID: 530
		private List<GameObjectClass> objects;

		// Token: 0x04000213 RID: 531
		private static readonly string unselectableObjectsListPath = EditorEnvironment.EditorFormsFolder + "UnselectableObjects.xml";
	}
}
