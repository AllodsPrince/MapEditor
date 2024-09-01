using System;
using System.ComponentModel;
using System.Windows.Forms;
using Db;
using Tools.WindowParams;

namespace MapEditor.Map.Dialogs
{
	// Token: 0x0200010F RID: 271
	public partial class CreateSpawnTunerForm : Form
	{
		// Token: 0x06000D3A RID: 3386 RVA: 0x0006EDCC File Offset: 0x0006DDCC
		private void UpdateOKButton()
		{
			if (this.mainDb != null)
			{
				DBID spawnTunerDBID = this.mainDb.GetDBIDByName(this.spawnTuner);
				this.CreateButton.Enabled = (spawnTunerDBID.IsEmpty() || !this.mainDb.DoesObjectExist(spawnTunerDBID));
				return;
			}
			this.CreateButton.Enabled = false;
		}

		// Token: 0x06000D3B RID: 3387 RVA: 0x0006EE25 File Offset: 0x0006DE25
		private void NewSpawnTunerTextBox_TextChanged(object sender, EventArgs e)
		{
			this.CreateButton.Enabled = false;
			this.TextBoxTimer.Start();
		}

		// Token: 0x06000D3C RID: 3388 RVA: 0x0006EE3E File Offset: 0x0006DE3E
		private void TextBoxTimer_Tick(object sender, EventArgs e)
		{
			this.TextBoxTimer.Stop();
			this.spawnTuner = this.spawnTunerFolder + this.NewSpawnTunerTextBox.Text;
			this.UpdateOKButton();
		}

		// Token: 0x06000D3D RID: 3389 RVA: 0x0006EE70 File Offset: 0x0006DE70
		public CreateSpawnTunerForm(string _spawnTunerFolder, string _spawnTuner, IDatabase _mainDb)
		{
			this.InitializeComponent();
			this.paramsSaver = new FormParamsSaver(this, EditorEnvironment.EditorFormsFolder + "CreateSpawnTunerForm.xml", false);
			this.paramsSaver.AutoregisterControls = false;
			this.spawnTunerFolder = _spawnTunerFolder;
			this.spawnTuner = _spawnTuner;
			this.mainDb = _mainDb;
			this.NewSpawnTunerTextBox.Text = this.spawnTuner.Substring(this.spawnTunerFolder.Length);
			this.UpdateOKButton();
		}

		// Token: 0x17000276 RID: 630
		// (get) Token: 0x06000D3E RID: 3390 RVA: 0x0006EF03 File Offset: 0x0006DF03
		public string SpawnTuner
		{
			get
			{
				return this.spawnTuner;
			}
		}

		// Token: 0x04000A98 RID: 2712
		private readonly FormParamsSaver paramsSaver;

		// Token: 0x04000A99 RID: 2713
		private readonly string spawnTunerFolder = string.Empty;

		// Token: 0x04000A9A RID: 2714
		private readonly IDatabase mainDb;

		// Token: 0x04000A9B RID: 2715
		private string spawnTuner = string.Empty;
	}
}
