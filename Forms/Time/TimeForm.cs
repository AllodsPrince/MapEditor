using System;
using System.ComponentModel;
using System.Windows.Forms;
using InputState;
using LauncherTools.InputState;

namespace MapEditor.Forms.Time
{
	// Token: 0x0200023D RID: 573
	public partial class TimeForm : Form
	{
		// Token: 0x06001B55 RID: 6997 RVA: 0x000B12F7 File Offset: 0x000B02F7
		private float GetTime()
		{
			return (float)this.TimeTrackBar.Value / 4f;
		}

		// Token: 0x06001B56 RID: 6998 RVA: 0x000B130B File Offset: 0x000B030B
		private void OnFormClosed(object sender, EventArgs e)
		{
			this.context.StateContainer.UnbindState(this.timeFormState);
		}

		// Token: 0x06001B57 RID: 6999 RVA: 0x000B1323 File Offset: 0x000B0323
		private void OnEnterMapEditState(MethodArgs args)
		{
			this.context.SaveTime(this.GetTime());
		}

		// Token: 0x06001B58 RID: 7000 RVA: 0x000B1336 File Offset: 0x000B0336
		private void OnLeaveMapEditState(MethodArgs args)
		{
			this.context.SaveTime(this.context.EditorScene.GetTime());
		}

		// Token: 0x06001B59 RID: 7001 RVA: 0x000B1354 File Offset: 0x000B0354
		public TimeForm(MainForm.Context _context)
		{
			this.context = _context;
			this.InitializeComponent();
			base.FormClosed += new FormClosedEventHandler(this.OnFormClosed);
			this.timeFormState.AddMethod("_enter_map_edit_state", new Method(this.OnEnterMapEditState));
			this.timeFormState.AddMethod("_leave_map_edit_state", new Method(this.OnLeaveMapEditState));
			this.context.StateContainer.BindState(this.timeFormState);
		}

		// Token: 0x06001B5A RID: 7002 RVA: 0x000B13E4 File Offset: 0x000B03E4
		private void TimeTrackBar_Scroll(object sender, EventArgs e)
		{
			this.context.EditorScene.SetTime(this.GetTime());
		}

		// Token: 0x06001B5B RID: 7003 RVA: 0x000B13FC File Offset: 0x000B03FC
		public void SetTime(float time)
		{
			this.TimeTrackBar.Value = (int)Math.Round((double)(4f * time));
		}

		// Token: 0x06001B5C RID: 7004 RVA: 0x000B1417 File Offset: 0x000B0417
		private void TimeForm_Deactivate(object sender, EventArgs e)
		{
			if (base.Visible)
			{
				base.Hide();
			}
		}

		// Token: 0x0400116D RID: 4461
		private const float coeff = 4f;

		// Token: 0x0400116E RID: 4462
		private readonly MainForm.Context context;

		// Token: 0x0400116F RID: 4463
		private readonly State timeFormState = new State("TimeFormState");
	}
}
