using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using MapEditor.Forms.Base;

namespace MapEditor.Forms.ServerLuncher
{
	// Token: 0x0200006F RID: 111
	public partial class ServerLuncherForm : BaseForm
	{
		// Token: 0x0600057A RID: 1402 RVA: 0x0002D186 File Offset: 0x0002C186
		public ServerLuncherForm(MainForm.Context context) : base("serv_lancher", context)
		{
			this.InitializeComponent();
			this.GetTimer();
			base.Visible = false;
		}

		// Token: 0x0600057B RID: 1403 RVA: 0x0002D1B2 File Offset: 0x0002C1B2
		private void GetTimer()
		{
			this.m_Time.Tick += this.FetchLogFromSever;
			this.m_Time.Interval = 1000;
			this.m_Time.Start();
		}

		// Token: 0x0600057C RID: 1404 RVA: 0x0002D1E6 File Offset: 0x0002C1E6
		private void FetchLogFromSever(object sender, EventArgs e)
		{
		}

		// Token: 0x0600057D RID: 1405 RVA: 0x0002D1E8 File Offset: 0x0002C1E8
		protected override void OnClosing(CancelEventArgs e)
		{
			e.Cancel = true;
			base.Hide();
		}

		// Token: 0x0600057E RID: 1406 RVA: 0x0002D1F7 File Offset: 0x0002C1F7
		private void Cancel_Click(object sender, EventArgs e)
		{
			base.Hide();
		}

		// Token: 0x0600057F RID: 1407 RVA: 0x0002D1FF File Offset: 0x0002C1FF
		private void button1_Click(object sender, EventArgs e)
		{
			this.StartNewBatFile("");
		}

		// Token: 0x06000580 RID: 1408 RVA: 0x0002D20C File Offset: 0x0002C20C
		private void StartNewBatFile(string name)
		{
			if (ServerLuncherForm.Console != null)
			{
				return;
			}
			ServerLuncherForm.Console = new Process();
			ServerLuncherForm.Console.StartInfo.FileName = name;
			ServerLuncherForm.Console.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
			ServerLuncherForm.Console.StartInfo.RedirectStandardOutput = true;
			ServerLuncherForm.Console.Exited += this.Console_Exited;
			ServerLuncherForm.Console.OutputDataReceived += this.Console_OutputDataReceived;
		}

		// Token: 0x06000581 RID: 1409 RVA: 0x0002D288 File Offset: 0x0002C288
		public void CreateDeployPropertes(string ipAdress, string deployPath, int port, string servName)
		{
			try
			{
				StreamWriter sw = File.CreateText(ServerLuncherForm.SettingsFilePath);
				sw.WriteLine("deploy.ip=" + ipAdress);
				sw.WriteLine("deploy.path=" + deployPath);
				sw.WriteLine("deploy.frontend.port" + port);
				sw.WriteLine("deploy.server.name" + servName);
				sw.Close();
			}
			catch (DirectoryNotFoundException ex)
			{
				this.AddToLog("Неудалось найти директорию " + ex.Data);
			}
			catch (Exception exc)
			{
				this.AddToLog(exc.Message);
			}
		}

		// Token: 0x06000582 RID: 1410 RVA: 0x0002D338 File Offset: 0x0002C338
		private void Console_OutputDataReceived(object sender, DataReceivedEventArgs e)
		{
			if (e.Data == "0")
			{
				this.AddToLog("Запуск " + ServerLuncherForm.Console.StartInfo.FileName + " прошел успешно");
			}
			if (e.Data == "1")
			{
				this.AddToLog("При запуске " + ServerLuncherForm.Console.StartInfo.FileName + " возникли ошибки");
			}
		}

		// Token: 0x06000583 RID: 1411 RVA: 0x0002D3B4 File Offset: 0x0002C3B4
		private void AddToLog(string p)
		{
			TextBox textBox = this.textBoxLog;
			object text = textBox.Text;
			textBox.Text = string.Concat(new object[]
			{
				text,
				DateTime.Now,
				" ",
				p,
				"\n"
			});
		}

		// Token: 0x06000584 RID: 1412 RVA: 0x0002D405 File Offset: 0x0002C405
		private void Console_Exited(object sender, EventArgs e)
		{
			ServerLuncherForm.Console = null;
		}

		// Token: 0x04000417 RID: 1047
		private readonly Timer m_Time = new Timer();

		// Token: 0x04000418 RID: 1048
		public static string SettingsFilePath;

		// Token: 0x04000419 RID: 1049
		public static Process Console;
	}
}
