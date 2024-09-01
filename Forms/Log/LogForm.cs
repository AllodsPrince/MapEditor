using System;
using System.ComponentModel;
using System.Windows.Forms;
using Logging;
using MapEditor.Forms.Base;

namespace MapEditor.Forms.Log
{
	// Token: 0x0200001C RID: 28
	public partial class LogForm : BaseForm
	{
		// Token: 0x0600024D RID: 589 RVA: 0x00019C1C File Offset: 0x00018C1C
		public LogForm(MainForm.Context context) : base(EditorEnvironment.EditorFormsFolder + "LogForm.xml", context)
		{
			this.InitializeComponent();
			Logger.AddListener(LogLevel.Info, new Logger.LogListenerDelegate(this.LogInfoListener), new Logger.LogFormatterDelegate(LogForm.LogFormatter));
			Logger.AddListener(LogLevel.Warning, new Logger.LogListenerDelegate(this.LogWarningListener), new Logger.LogFormatterDelegate(LogForm.LogFormatter));
			Logger.AddListener(LogLevel.Error, new Logger.LogListenerDelegate(this.LogErrorListener), new Logger.LogFormatterDelegate(LogForm.LogFormatter));
			Logger.AddListener(LogLevel.Fatal, new Logger.LogListenerDelegate(this.LogFatalListener), new Logger.LogFormatterDelegate(LogForm.LogFormatter));
		}

		// Token: 0x0600024E RID: 590 RVA: 0x00019CBD File Offset: 0x00018CBD
		public void LogInfoListener(string message, string file, int lineNum)
		{
			if (this.toggleInfo.Checked)
			{
				RichTextBox richTextBox = this.logTextBox;
				richTextBox.Text += message;
			}
		}

		// Token: 0x0600024F RID: 591 RVA: 0x00019CE3 File Offset: 0x00018CE3
		public void LogWarningListener(string message, string file, int lineNum)
		{
			if (this.toggleWarning.Checked)
			{
				RichTextBox richTextBox = this.logTextBox;
				richTextBox.Text += message;
			}
		}

		// Token: 0x06000250 RID: 592 RVA: 0x00019D09 File Offset: 0x00018D09
		public void LogErrorListener(string message, string file, int lineNum)
		{
			if (this.toggleError.Checked)
			{
				RichTextBox richTextBox = this.logTextBox;
				richTextBox.Text += message;
			}
		}

		// Token: 0x06000251 RID: 593 RVA: 0x00019D2F File Offset: 0x00018D2F
		public void LogFatalListener(string message, string file, int lineNum)
		{
			if (this.toggleFatal.Checked)
			{
				RichTextBox richTextBox = this.logTextBox;
				richTextBox.Text += message;
			}
		}

		// Token: 0x06000252 RID: 594 RVA: 0x00019D55 File Offset: 0x00018D55
		public static string LogFormatter(int logLevel, string channelName, string message, string fileName, int lineNumber)
		{
			return channelName + ": " + message + Environment.NewLine;
		}

		// Token: 0x06000253 RID: 595 RVA: 0x00019D68 File Offset: 0x00018D68
		private void FormClosingHandler(object sender, FormClosingEventArgs e)
		{
			if (e.CloseReason == CloseReason.UserClosing)
			{
				e.Cancel = true;
				base.Hide();
			}
		}

		// Token: 0x06000254 RID: 596 RVA: 0x00019D80 File Offset: 0x00018D80
		private void SelectAllContextMenuHandler(object sender, EventArgs e)
		{
			this.logTextBox.SelectAll();
		}

		// Token: 0x06000255 RID: 597 RVA: 0x00019D8D File Offset: 0x00018D8D
		private void CopyContextMenuHandler(object sender, EventArgs e)
		{
			Clipboard.SetText(this.logTextBox.SelectedRtf, TextDataFormat.Rtf);
		}

		// Token: 0x06000256 RID: 598 RVA: 0x00019DA0 File Offset: 0x00018DA0
		private void ClearContextMenuHandler(object sender, EventArgs e)
		{
			this.logTextBox.Clear();
		}
	}
}
