using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using System.Windows.Forms.HSVColor;
using Db;
using Db.Main;
using MapEditor.Forms.Base;

namespace MapEditor.Forms.HSVColorEditor
{
	// Token: 0x020000B3 RID: 179
	public partial class HSVColorEditorForm : BaseForm
	{
		// Token: 0x060008C4 RID: 2244 RVA: 0x0004BAF4 File Offset: 0x0004AAF4
		private void OnPCDragDrop(object sender, DragEventArgs e)
		{
			Array array = e.Data.GetData(DataFormats.FileDrop) as Array;
			if (array != null && array.GetValue(0) != null && HSVColorEditorForm.mainDb != null)
			{
				string objectFileName = array.GetValue(0).ToString();
				objectFileName = objectFileName.Replace('\\', '/');
				if (objectFileName.StartsWith(EditorEnvironment.DataFolder, StringComparison.OrdinalIgnoreCase))
				{
					objectFileName = objectFileName.Substring(EditorEnvironment.DataFolder.Length);
				}
				DBID objectDBID = HSVColorEditorForm.mainDb.GetDBIDByName(objectFileName);
				if (!DBID.IsNullOrEmpty(objectDBID))
				{
					IObjMan objectMan = HSVColorEditorForm.mainDb.GetManipulator(objectDBID);
					if (objectMan != null)
					{
						this.propertyControl.SelectedObject = objectMan;
					}
				}
			}
			base.Activate();
		}

		// Token: 0x060008C5 RID: 2245 RVA: 0x0004BB96 File Offset: 0x0004AB96
		private static void OnPCDragEnter(object sender, DragEventArgs e)
		{
			if (e.Data.GetDataPresent(DataFormats.FileDrop))
			{
				e.Effect = DragDropEffects.Copy;
				return;
			}
			e.Effect = DragDropEffects.None;
		}

		// Token: 0x060008C6 RID: 2246 RVA: 0x0004BBB9 File Offset: 0x0004ABB9
		private void OnSelectedObjectChanged(PropertyControl sender, DBID dbid)
		{
			this.RemoveAllItems();
			this.Text = this.baseText + " " + dbid;
		}

		// Token: 0x060008C7 RID: 2247 RVA: 0x0004BBD8 File Offset: 0x0004ABD8
		private void PaintIcon(ListViewItem item, Color color)
		{
			string key = item.ImageKey;
			if (key != null && this.imageList.Images.ContainsKey(key))
			{
				Image image = this.imageList.Images[key];
				Graphics g = Graphics.FromImage(image);
				g.FillRectangle(new SolidBrush(Color.FromArgb(255, color)), HSVColorEditorForm.bitmapRect);
				g.Dispose();
				this.imageList.Images.RemoveByKey(key);
				this.imageList.Images.Add(key, image);
			}
		}

		// Token: 0x060008C8 RID: 2248 RVA: 0x0004BC60 File Offset: 0x0004AC60
		private void OnAddFieldClick(object sender, EventArgs e)
		{
			IObjMan objMan;
			string fieldName;
			if (this.propertyControl.GetCurrentProperty(out objMan, out fieldName))
			{
				HSVColorEditorForm.FieldWrapEnum fieldWrap = new HSVColorEditorForm.FieldWrapEnum(objMan, fieldName);
				foreach (string colorName in fieldWrap)
				{
					HSVColorEditorForm.ColorFieldWrap colorFieldWrap = new HSVColorEditorForm.ColorFieldWrap(objMan, colorName);
					bool found = false;
					foreach (object obj in this.selectedFieldsListView.Items)
					{
						ListViewItem item = (ListViewItem)obj;
						if (item.Text == colorFieldWrap.ToString())
						{
							found = true;
							break;
						}
					}
					if (!found)
					{
						Bitmap bitmap = new Bitmap(HSVColorEditorForm.bitmapSize.Width, HSVColorEditorForm.bitmapSize.Height);
						ListViewItem item2 = new ListViewItem(colorFieldWrap.ToString());
						this.imageList.Images.Add(item2.Text, bitmap);
						item2.Tag = colorFieldWrap;
						item2.ImageKey = item2.Text;
						this.selectedFieldsListView.Items.Add(item2);
						this.PaintIcon(item2, colorFieldWrap.Color);
					}
				}
			}
			if (this.selectedFieldsListView.Items.Count > 0)
			{
				this.selectedFieldsListView.RedrawItems(0, this.selectedFieldsListView.Items.Count - 1, false);
			}
		}

		// Token: 0x060008C9 RID: 2249 RVA: 0x0004BE10 File Offset: 0x0004AE10
		private void OnRemoveAllFieldsClick(object sender, EventArgs e)
		{
			this.RemoveAllItems();
		}

		// Token: 0x060008CA RID: 2250 RVA: 0x0004BE18 File Offset: 0x0004AE18
		private void OnRemoveFieldClick(object sender, EventArgs e)
		{
			this.RemoveSelectedItems();
		}

		// Token: 0x060008CB RID: 2251 RVA: 0x0004BE20 File Offset: 0x0004AE20
		private void RemoveAllItems()
		{
			this.selectedFieldsListView.Items.Clear();
			for (int index = this.imageList.Images.Count - 1; index > -1; index--)
			{
				Image icon = this.imageList.Images[index];
				this.imageList.Images.RemoveAt(index);
				icon.Dispose();
			}
			this.undoButton.Enabled = false;
			this.redoButton.Enabled = false;
		}

		// Token: 0x060008CC RID: 2252 RVA: 0x0004BE9C File Offset: 0x0004AE9C
		private void RemoveSelectedItems()
		{
			for (int index = this.selectedFieldsListView.Items.Count - 1; index > -1; index--)
			{
				if (this.selectedFieldsListView.SelectedIndices.Contains(index))
				{
					if (this.imageList.Images.ContainsKey(this.selectedFieldsListView.Items[index].ImageKey))
					{
						Image icon = this.imageList.Images[this.selectedFieldsListView.Items[index].ImageKey];
						this.imageList.Images.RemoveByKey(this.selectedFieldsListView.Items[index].ImageKey);
						icon.Dispose();
					}
					this.selectedFieldsListView.Items.RemoveAt(index);
				}
			}
			for (int index2 = this.selectedFieldsListView.Items.Count - 1; index2 > -1; index2--)
			{
				this.selectedFieldsListView.Items[index2].ImageKey = this.selectedFieldsListView.Items[index2].ImageKey;
			}
			if (this.selectedFieldsListView.Items.Count > 0)
			{
				this.selectedFieldsListView.RedrawItems(0, this.selectedFieldsListView.Items.Count - 1, false);
			}
		}

		// Token: 0x060008CD RID: 2253 RVA: 0x0004BFEC File Offset: 0x0004AFEC
		private void UpdateIcons()
		{
			List<ListViewItem> listViewItems = new List<ListViewItem>();
			foreach (object obj in this.selectedFieldsListView.Items)
			{
				ListViewItem item = (ListViewItem)obj;
				listViewItems.Add(item);
			}
			this.selectedFieldsListView.Items.Clear();
			this.selectedFieldsListView.Items.AddRange(listViewItems.ToArray());
			if (this.selectedFieldsListView.Items.Count > 0)
			{
				this.selectedFieldsListView.RedrawItems(0, this.selectedFieldsListView.Items.Count - 1, false);
			}
		}

		// Token: 0x060008CE RID: 2254 RVA: 0x0004C0A8 File Offset: 0x0004B0A8
		private void StartEdit()
		{
			if (this.selectedFieldsListView.SelectedItems.Count > 0)
			{
				ListViewItem selectedItem = this.selectedFieldsListView.SelectedItems[0];
				if (selectedItem.Tag != null && selectedItem.Tag is HSVColorEditorForm.ColorFieldWrap)
				{
					HSVColorEditorForm.ColorFieldWrap selectedFieldWrap = (HSVColorEditorForm.ColorFieldWrap)selectedItem.Tag;
					if (!selectedFieldWrap.IsEmpty())
					{
						this.selectedHSV = ColorHandler.ColortoHSV(selectedFieldWrap.Color);
						HSVColorDialog dialog = new HSVColorDialog();
						dialog.Color = selectedFieldWrap.Color;
						HSVColorDialog hsvcolorDialog = dialog;
						hsvcolorDialog.ColorChanged = (HSVColorDialog.ColorChangedEventHandler)Delegate.Combine(hsvcolorDialog.ColorChanged, new HSVColorDialog.ColorChangedEventHandler(this.OnColorChanged));
						bool result = dialog.ShowDialog(this) == DialogResult.OK;
						ObjMan.StartMassEditing();
						foreach (object obj in this.selectedFieldsListView.Items)
						{
							ListViewItem listViewItem = (ListViewItem)obj;
							HSVColorEditorForm.ColorFieldWrap colorFieldWrap = listViewItem.Tag as HSVColorEditorForm.ColorFieldWrap;
							if (colorFieldWrap != null)
							{
								if (result)
								{
									colorFieldWrap.CommitColor();
									this.PaintIcon(listViewItem, colorFieldWrap.UncommitedColor);
								}
								else
								{
									colorFieldWrap.RestoreColor();
								}
							}
						}
						ObjMan.StopMassEditing();
						if (result)
						{
							this.undoButton.Enabled = true;
							this.redoButton.Enabled = false;
							this.UpdateIcons();
						}
					}
				}
			}
		}

		// Token: 0x060008CF RID: 2255 RVA: 0x0004C210 File Offset: 0x0004B210
		private void OnSelectedFieldDoubleClick(object sender, EventArgs e)
		{
			this.StartEdit();
		}

		// Token: 0x060008D0 RID: 2256 RVA: 0x0004C218 File Offset: 0x0004B218
		private void OnColorChanged(object sender, ColorHandler.HSV hsv)
		{
			ColorHandler.HSV hsvDelta = new ColorHandler.HSV(hsv.Hue - this.selectedHSV.Hue, hsv.Saturation - this.selectedHSV.Saturation, hsv.value - this.selectedHSV.value);
			ObjMan.StartMassEditing();
			foreach (object obj in this.selectedFieldsListView.Items)
			{
				ListViewItem listViewItem = (ListViewItem)obj;
				HSVColorEditorForm.ColorFieldWrap colorFieldWrap = listViewItem.Tag as HSVColorEditorForm.ColorFieldWrap;
				if (colorFieldWrap != null)
				{
					ColorHandler.HSV colorHSV = ColorHandler.ColortoHSV(colorFieldWrap.UncommitedColor);
					colorHSV.Hue += hsvDelta.Hue;
					colorHSV.Saturation += hsvDelta.Saturation;
					colorHSV.value += hsvDelta.value;
					colorHSV.Hue = Math.Max(Math.Min(colorHSV.Hue, 255), 0);
					colorHSV.Saturation = Math.Max(Math.Min(colorHSV.Saturation, 255), 0);
					colorHSV.value = Math.Max(Math.Min(colorHSV.value, 255), 0);
					colorFieldWrap.Color = ColorHandler.HSVtoColor(colorHSV);
				}
			}
			ObjMan.StopMassEditing();
		}

		// Token: 0x060008D1 RID: 2257 RVA: 0x0004C38C File Offset: 0x0004B38C
		private void OnSelectedFieldsListViewKeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Return)
			{
				this.StartEdit();
				return;
			}
			if (e.KeyCode == Keys.Delete)
			{
				this.RemoveSelectedItems();
				return;
			}
			if (e.KeyCode == Keys.Z && e.Control && this.undoButton.Enabled)
			{
				this.OnUndoButtonClick(sender, e);
				return;
			}
			if (e.KeyCode == Keys.Y && e.Control && this.redoButton.Enabled)
			{
				this.OnRedoButtonClick(sender, e);
			}
		}

		// Token: 0x060008D2 RID: 2258 RVA: 0x0004C40C File Offset: 0x0004B40C
		private void Find(bool others)
		{
			string value = this.findComboBox.Text;
			if (!string.IsNullOrEmpty(value))
			{
				foreach (object obj in this.selectedFieldsListView.Items)
				{
					ListViewItem item = (ListViewItem)obj;
					HSVColorEditorForm.FieldWrap fieldWrap = item.Tag as HSVColorEditorForm.FieldWrap;
					if (fieldWrap != null)
					{
						item.Selected = (fieldWrap.FieldName != null && (others ^ fieldWrap.FieldName.IndexOf(value) != -1));
					}
				}
				if (this.selectedFieldsListView.SelectedItems.Count > 0)
				{
					this.finding = true;
					this.findComboBox.Items.Remove(value);
					this.findComboBox.Items.Insert(0, value);
					if (this.findComboBox.Items.Count > 10)
					{
						this.findComboBox.Items.RemoveAt(10);
					}
					this.findComboBox.SelectedIndex = 0;
					this.finding = false;
				}
			}
		}

		// Token: 0x060008D3 RID: 2259 RVA: 0x0004C528 File Offset: 0x0004B528
		private void OnFindOthersButtonClick(object sender, EventArgs e)
		{
			this.Find(true);
		}

		// Token: 0x060008D4 RID: 2260 RVA: 0x0004C531 File Offset: 0x0004B531
		private void OnFindButtonClick(object sender, EventArgs e)
		{
			this.Find(false);
		}

		// Token: 0x060008D5 RID: 2261 RVA: 0x0004C53A File Offset: 0x0004B53A
		private void OnFindComboBoxKeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Return)
			{
				this.Find(false);
			}
		}

		// Token: 0x060008D6 RID: 2262 RVA: 0x0004C54D File Offset: 0x0004B54D
		private void OnSelectedIndexChanged(object sender, EventArgs e)
		{
			if (!this.finding)
			{
				this.Find(false);
			}
		}

		// Token: 0x060008D7 RID: 2263 RVA: 0x0004C560 File Offset: 0x0004B560
		private void OnUndoButtonClick(object sender, EventArgs e)
		{
			ObjMan.StartMassEditing();
			foreach (object obj in this.selectedFieldsListView.Items)
			{
				ListViewItem listViewItem = (ListViewItem)obj;
				HSVColorEditorForm.ColorFieldWrap colorFieldWrap = listViewItem.Tag as HSVColorEditorForm.ColorFieldWrap;
				if (colorFieldWrap != null)
				{
					colorFieldWrap.Undo();
					this.PaintIcon(listViewItem, colorFieldWrap.UncommitedColor);
				}
			}
			ObjMan.StopMassEditing();
			this.undoButton.Enabled = false;
			this.redoButton.Enabled = true;
			this.UpdateIcons();
		}

		// Token: 0x060008D8 RID: 2264 RVA: 0x0004C604 File Offset: 0x0004B604
		private void OnRedoButtonClick(object sender, EventArgs e)
		{
			ObjMan.StartMassEditing();
			foreach (object obj in this.selectedFieldsListView.Items)
			{
				ListViewItem listViewItem = (ListViewItem)obj;
				HSVColorEditorForm.ColorFieldWrap colorFieldWrap = listViewItem.Tag as HSVColorEditorForm.ColorFieldWrap;
				if (colorFieldWrap != null)
				{
					colorFieldWrap.Redo();
					this.PaintIcon(listViewItem, colorFieldWrap.UncommitedColor);
				}
			}
			ObjMan.StopMassEditing();
			this.UpdateIcons();
			this.undoButton.Enabled = true;
			this.redoButton.Enabled = false;
		}

		// Token: 0x060008D9 RID: 2265 RVA: 0x0004C6A8 File Offset: 0x0004B6A8
		public HSVColorEditorForm(MainForm.Context _context) : base(EditorEnvironment.EditorFormsFolder + "HSVColorEditorForm.xml", _context)
		{
			this.InitializeComponent();
			this.baseText = base.Text;
			base.ParamsSaver.AutoregisterControls = false;
			base.ParamsSaver.RegisterControl(this.splitContainer);
			base.ParamsSaver.RegisterControl(this.findComboBox, false);
			this.selectedFieldsListView.SmallImageList = this.imageList;
			this.propertyControl.DragDrop += this.OnPCDragDrop;
			this.propertyControl.DragEnter += HSVColorEditorForm.OnPCDragEnter;
			this.propertyControl.OnSelectXDBObject += this.OnSelectedObjectChanged;
			this.addFieldButton.Click += this.OnAddFieldClick;
			this.removeFieldButton.Click += this.OnRemoveFieldClick;
			this.removeAllFieldsButton.Click += this.OnRemoveAllFieldsClick;
			this.selectedFieldsListView.DoubleClick += this.OnSelectedFieldDoubleClick;
			this.selectedFieldsListView.KeyDown += this.OnSelectedFieldsListViewKeyDown;
			this.findButton.Click += this.OnFindButtonClick;
			this.findOtherButton.Click += this.OnFindOthersButtonClick;
			this.findComboBox.KeyDown += this.OnFindComboBoxKeyDown;
			this.findComboBox.SelectedIndexChanged += this.OnSelectedIndexChanged;
			this.undoButton.Click += this.OnUndoButtonClick;
			this.redoButton.Click += this.OnRedoButtonClick;
		}

		// Token: 0x04000774 RID: 1908
		private const string colorEditorFieldName = "int_color_with_alpha";

		// Token: 0x04000775 RID: 1909
		private static readonly IDatabase mainDb = IDatabase.GetMainDatabase();

		// Token: 0x04000776 RID: 1910
		private ColorHandler.HSV selectedHSV;

		// Token: 0x04000777 RID: 1911
		private readonly string baseText = string.Empty;

		// Token: 0x04000778 RID: 1912
		private bool finding;

		// Token: 0x04000779 RID: 1913
		private readonly ImageList imageList = new ImageList();

		// Token: 0x0400077A RID: 1914
		private static readonly Size bitmapSize = new Size(20, 20);

		// Token: 0x0400077B RID: 1915
		private static readonly Rectangle bitmapRect = new Rectangle(new Point(0, 0), HSVColorEditorForm.bitmapSize);

		// Token: 0x020000B4 RID: 180
		private class ColorFieldsEnumerator : IEnumerator<string>, IDisposable, IEnumerator
		{
			// Token: 0x060008DD RID: 2269 RVA: 0x0004D0B9 File Offset: 0x0004C0B9
			public ColorFieldsEnumerator(IObjMan _objMan, string _fieldName)
			{
				this.objMan = _objMan;
				this.fieldName = _fieldName;
				this.Reset();
			}

			// Token: 0x060008DE RID: 2270 RVA: 0x0004D0D5 File Offset: 0x0004C0D5
			public void Reset()
			{
				this.currentIterator = null;
			}

			// Token: 0x1700017C RID: 380
			// (get) Token: 0x060008DF RID: 2271 RVA: 0x0004D0DE File Offset: 0x0004C0DE
			public string Current
			{
				get
				{
					if (this.currentIterator == null || this.currentIterator.IsEnd())
					{
						return null;
					}
					return this.currentIterator.Name;
				}
			}

			// Token: 0x1700017D RID: 381
			// (get) Token: 0x060008E0 RID: 2272 RVA: 0x0004D102 File Offset: 0x0004C102
			object IEnumerator.Current
			{
				get
				{
					return this.Current;
				}
			}

			// Token: 0x060008E1 RID: 2273 RVA: 0x0004D10C File Offset: 0x0004C10C
			public bool MoveNext()
			{
				if (this.objMan == null || string.IsNullOrEmpty(this.fieldName))
				{
					return false;
				}
				for (;;)
				{
					if (this.currentIterator != null)
					{
						this.currentIterator.Next();
					}
					else
					{
						this.currentIterator = this.objMan.CreateIterator(false);
					}
					if (this.currentIterator.IsEnd())
					{
						break;
					}
					if (this.currentIterator.Name.StartsWith(this.fieldName) && !this.currentIterator.FieldDesc.IsHidden && this.currentIterator.FieldDesc.EditorName == "int_color_with_alpha")
					{
						goto Block_6;
					}
				}
				return false;
				Block_6:
				return !this.currentIterator.IsEnd();
			}

			// Token: 0x060008E2 RID: 2274 RVA: 0x0004D1BB File Offset: 0x0004C1BB
			public void Dispose()
			{
				if (this.currentIterator != null)
				{
					this.currentIterator.Dispose();
					this.currentIterator = null;
				}
			}

			// Token: 0x04000789 RID: 1929
			private readonly IObjMan objMan;

			// Token: 0x0400078A RID: 1930
			private readonly string fieldName;

			// Token: 0x0400078B RID: 1931
			private IObjManIterator currentIterator;
		}

		// Token: 0x020000B5 RID: 181
		private class FieldWrap
		{
			// Token: 0x060008E3 RID: 2275 RVA: 0x0004D1D7 File Offset: 0x0004C1D7
			public FieldWrap(IObjMan _objMan, string _fieldName)
			{
				this.objMan = _objMan;
				this.fieldName = _fieldName;
			}

			// Token: 0x060008E4 RID: 2276 RVA: 0x0004D1F0 File Offset: 0x0004C1F0
			public override string ToString()
			{
				return string.Format("{0} {1}", (this.objMan != null && !DBID.IsNullOrEmpty(this.objMan.DBID)) ? this.objMan.DBID.GetFileShortName() : string.Empty, this.fieldName ?? string.Empty);
			}

			// Token: 0x060008E5 RID: 2277 RVA: 0x0004D247 File Offset: 0x0004C247
			public virtual bool IsEmpty()
			{
				return this.objMan == null || DBID.IsNullOrEmpty(this.objMan.DBID) || string.IsNullOrEmpty(this.fieldName);
			}

			// Token: 0x1700017E RID: 382
			// (get) Token: 0x060008E6 RID: 2278 RVA: 0x0004D273 File Offset: 0x0004C273
			public string FieldName
			{
				get
				{
					return this.fieldName;
				}
			}

			// Token: 0x0400078C RID: 1932
			protected readonly IObjMan objMan;

			// Token: 0x0400078D RID: 1933
			protected readonly string fieldName;
		}

		// Token: 0x020000B6 RID: 182
		private class FieldWrapEnum : HSVColorEditorForm.FieldWrap, IEnumerable<string>, IEnumerable
		{
			// Token: 0x060008E7 RID: 2279 RVA: 0x0004D27B File Offset: 0x0004C27B
			public FieldWrapEnum(IObjMan _objMan, string _fieldName) : base(_objMan, _fieldName)
			{
			}

			// Token: 0x060008E8 RID: 2280 RVA: 0x0004D288 File Offset: 0x0004C288
			public override bool IsEmpty()
			{
				if (base.IsEmpty())
				{
					return true;
				}
				using (IEnumerator<string> enumerator = this.GetEnumerator())
				{
					if (enumerator.MoveNext())
					{
						string text = enumerator.Current;
						return false;
					}
				}
				return true;
			}

			// Token: 0x060008E9 RID: 2281 RVA: 0x0004D2DC File Offset: 0x0004C2DC
			public IEnumerator<string> GetEnumerator()
			{
				return new HSVColorEditorForm.ColorFieldsEnumerator(this.objMan, this.fieldName);
			}

			// Token: 0x060008EA RID: 2282 RVA: 0x0004D2EF File Offset: 0x0004C2EF
			IEnumerator IEnumerable.GetEnumerator()
			{
				return this.GetEnumerator();
			}
		}

		// Token: 0x020000B7 RID: 183
		private class ColorFieldWrap : HSVColorEditorForm.FieldWrap
		{
			// Token: 0x060008EB RID: 2283 RVA: 0x0004D2F7 File Offset: 0x0004C2F7
			public ColorFieldWrap(IObjMan _objMan, string _fieldName) : base(_objMan, _fieldName)
			{
				this.CommitColor();
				this.undoColor = this.uncommited;
				this.redoColor = this.uncommited;
			}

			// Token: 0x1700017F RID: 383
			// (get) Token: 0x060008EC RID: 2284 RVA: 0x0004D320 File Offset: 0x0004C320
			// (set) Token: 0x060008ED RID: 2285 RVA: 0x0004D348 File Offset: 0x0004C348
			public Color Color
			{
				get
				{
					int color;
					this.objMan.GetValue(this.fieldName, out color);
					return Color.FromArgb(color);
				}
				set
				{
					Color color = Color.FromArgb((int)this.uncommited.A, value);
					this.objMan.SetValue(this.fieldName, color.ToArgb());
				}
			}

			// Token: 0x060008EE RID: 2286 RVA: 0x0004D37F File Offset: 0x0004C37F
			public void CommitColor()
			{
				this.undoColor = this.uncommited;
				this.uncommited = this.Color;
				this.redoColor = this.uncommited;
			}

			// Token: 0x060008EF RID: 2287 RVA: 0x0004D3A5 File Offset: 0x0004C3A5
			public void RestoreColor()
			{
				this.Color = this.uncommited;
			}

			// Token: 0x17000180 RID: 384
			// (get) Token: 0x060008F0 RID: 2288 RVA: 0x0004D3B3 File Offset: 0x0004C3B3
			public Color UncommitedColor
			{
				get
				{
					return this.uncommited;
				}
			}

			// Token: 0x060008F1 RID: 2289 RVA: 0x0004D3BB File Offset: 0x0004C3BB
			public void Undo()
			{
				if (this.undoColor != this.uncommited)
				{
					this.redoColor = this.uncommited;
					this.uncommited = this.undoColor;
					this.Color = this.uncommited;
				}
			}

			// Token: 0x060008F2 RID: 2290 RVA: 0x0004D3F4 File Offset: 0x0004C3F4
			public void Redo()
			{
				if (this.redoColor != this.uncommited)
				{
					this.undoColor = this.uncommited;
					this.uncommited = this.redoColor;
					this.Color = this.uncommited;
				}
			}

			// Token: 0x0400078E RID: 1934
			private Color uncommited;

			// Token: 0x0400078F RID: 1935
			private Color undoColor;

			// Token: 0x04000790 RID: 1936
			private Color redoColor;
		}
	}
}
