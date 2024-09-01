using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Db;
using MapEditor.Resources.Strings;

namespace MapEditor.Forms.LightEditor
{
	// Token: 0x020002B2 RID: 690
	public class DBIDValidator : Validator
	{
		// Token: 0x06001FC8 RID: 8136 RVA: 0x000CBA93 File Offset: 0x000CAA93
		private void OnTextBoxChanging(object sender, EventArgs e)
		{
			base.InvokeValueChangingEvent();
		}

		// Token: 0x06001FC9 RID: 8137 RVA: 0x000CBA9C File Offset: 0x000CAA9C
		private void OnChooseButtonClick(object sender, EventArgs e)
		{
			OpenFileDialog dialog = new OpenFileDialog();
			dialog.Filter = "XDB Files|*.xdb|All Files|*.*";
			if (dialog.ShowDialog() == DialogResult.OK)
			{
				string fileName = dialog.FileName.Replace('\\', '/');
				if (fileName.Length > EditorEnvironment.DataFolder.Length)
				{
					this.textBox.Text = fileName.Substring(EditorEnvironment.DataFolder.Length);
					if (!this.Validate())
					{
						MessageBox.Show(Strings.LIGHTEDITOR_FORMATERROR_TITLE, Strings.LIGHTEDITOR_TITLE, MessageBoxButtons.OK, MessageBoxIcon.Hand);
					}
				}
			}
		}

		// Token: 0x06001FCA RID: 8138 RVA: 0x000CBB1D File Offset: 0x000CAB1D
		private void OnClearButtonClick(object sender, EventArgs e)
		{
			this.Clear();
		}

		// Token: 0x06001FCB RID: 8139 RVA: 0x000CBB28 File Offset: 0x000CAB28
		public DBIDValidator(TextBox _textBox, string _baseType, Button _chooseButton, Button _clearButton)
		{
			this.textBox = _textBox;
			this.baseType = _baseType;
			this.chooseButton = _chooseButton;
			this.clearButton = _clearButton;
			if (this.textBox != null)
			{
				this.textBox.ReadOnly = true;
				this.textBox.TextChanged += this.OnTextBoxChanging;
			}
			this.types = new List<string>();
			if (!string.IsNullOrEmpty(this.baseType))
			{
				IDatabase mainDb = IDatabase.GetMainDatabase();
				if (mainDb != null)
				{
					string[] derivedTypes = mainDb.GetDerivedXDBClassTypes(_baseType);
					this.types = new List<string>(derivedTypes);
				}
				this.types.Add(this.baseType);
			}
			if (this.chooseButton != null)
			{
				this.chooseButton.Click += this.OnChooseButtonClick;
			}
			if (this.clearButton != null)
			{
				this.clearButton.Click += this.OnClearButtonClick;
			}
		}

		// Token: 0x06001FCC RID: 8140 RVA: 0x000CBC20 File Offset: 0x000CAC20
		public override bool Validate()
		{
			if (this.textBox == null)
			{
				return false;
			}
			string newValue = this.textBox.Text;
			bool result = false;
			if (!string.IsNullOrEmpty(newValue))
			{
				IDatabase mainDb = IDatabase.GetMainDatabase();
				if (mainDb != null)
				{
					DBID dbid = IDatabase.CreateDBIDByName(newValue);
					if (!dbid.IsEmpty() && mainDb.DoesObjectExist(dbid))
					{
						string type = mainDb.GetClassTypeName(dbid);
						if (this.types.Contains(type))
						{
							newValue = dbid.ToString();
							result = true;
						}
					}
				}
			}
			if (result)
			{
				this.SetValue(newValue);
				base.InvokeValueChangedEvent();
			}
			else
			{
				this.SetValue(this.value);
			}
			return result;
		}

		// Token: 0x06001FCD RID: 8141 RVA: 0x000CBCAE File Offset: 0x000CACAE
		public void SetValue(string _value)
		{
			this.value = _value;
			if (this.textBox != null)
			{
				this.textBox.Text = this.value;
			}
		}

		// Token: 0x06001FCE RID: 8142 RVA: 0x000CBCD0 File Offset: 0x000CACD0
		public override void Clear()
		{
			this.SetValue(string.Empty);
		}

		// Token: 0x06001FCF RID: 8143 RVA: 0x000CBCDD File Offset: 0x000CACDD
		public string GetValue()
		{
			return this.value;
		}

		// Token: 0x0400138F RID: 5007
		private TextBox textBox;

		// Token: 0x04001390 RID: 5008
		private string baseType;

		// Token: 0x04001391 RID: 5009
		private Button chooseButton;

		// Token: 0x04001392 RID: 5010
		private Button clearButton;

		// Token: 0x04001393 RID: 5011
		private string value = string.Empty;

		// Token: 0x04001394 RID: 5012
		private List<string> types = new List<string>();
	}
}
