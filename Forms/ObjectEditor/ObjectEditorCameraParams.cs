using System;

namespace MapEditor.Forms.ObjectEditor
{
	// Token: 0x0200029D RID: 669
	public class ObjectEditorCameraParams
	{
		// Token: 0x17000720 RID: 1824
		// (get) Token: 0x06001F24 RID: 7972 RVA: 0x000C773C File Offset: 0x000C673C
		// (set) Token: 0x06001F25 RID: 7973 RVA: 0x000C7744 File Offset: 0x000C6744
		public ObjectEditorCameraParams.ObjectEditorCameraType CameraType
		{
			get
			{
				return this.cameraType;
			}
			set
			{
				if (this.cameraType != value)
				{
					this.cameraType = value;
				}
			}
		}

		// Token: 0x17000721 RID: 1825
		// (get) Token: 0x06001F26 RID: 7974 RVA: 0x000C7756 File Offset: 0x000C6756
		// (set) Token: 0x06001F27 RID: 7975 RVA: 0x000C7760 File Offset: 0x000C6760
		public double CameraFOV
		{
			get
			{
				return this.cameraFOV;
			}
			set
			{
				if (this.cameraFOV != value)
				{
					this.cameraFOV = value;
					if (this.cameraFOV < 0.5235987755982988)
					{
						this.cameraFOV = 0.5235987755982988;
						return;
					}
					if (this.cameraFOV > 2.0943951023931953)
					{
						this.cameraFOV = 2.0943951023931953;
					}
				}
			}
		}

		// Token: 0x17000722 RID: 1826
		// (get) Token: 0x06001F28 RID: 7976 RVA: 0x000C77BE File Offset: 0x000C67BE
		// (set) Token: 0x06001F29 RID: 7977 RVA: 0x000C77C8 File Offset: 0x000C67C8
		public double CameraSpeed
		{
			get
			{
				return this.cameraSpeed;
			}
			set
			{
				if (this.cameraSpeed != value)
				{
					this.cameraSpeed = value;
					if (this.cameraSpeed < 1.0)
					{
						this.cameraSpeed = 1.0;
						return;
					}
					if (this.cameraSpeed > 250.0)
					{
						this.cameraSpeed = 250.0;
					}
				}
			}
		}

		// Token: 0x06001F2A RID: 7978 RVA: 0x000C7826 File Offset: 0x000C6826
		public double GetCameraSpeed()
		{
			if (this.cameraType == ObjectEditorCameraParams.ObjectEditorCameraType.Slow)
			{
				return 10.0;
			}
			if (this.cameraType == ObjectEditorCameraParams.ObjectEditorCameraType.Normal)
			{
				return 50.0;
			}
			return this.cameraSpeed;
		}

		// Token: 0x04001344 RID: 4932
		private const double minCameraFOV = 0.5235987755982988;

		// Token: 0x04001345 RID: 4933
		private const double normalCameraFOV = 1.0471975511965976;

		// Token: 0x04001346 RID: 4934
		private const double maxCameraFOV = 2.0943951023931953;

		// Token: 0x04001347 RID: 4935
		private const double minCameraSpeed = 1.0;

		// Token: 0x04001348 RID: 4936
		private const double slowCameraSpeed = 10.0;

		// Token: 0x04001349 RID: 4937
		private const double normalCameraSpeed = 50.0;

		// Token: 0x0400134A RID: 4938
		private const double maxCameraSpeed = 250.0;

		// Token: 0x0400134B RID: 4939
		private ObjectEditorCameraParams.ObjectEditorCameraType cameraType = ObjectEditorCameraParams.ObjectEditorCameraType.Normal;

		// Token: 0x0400134C RID: 4940
		private double cameraFOV = 1.0471975511965976;

		// Token: 0x0400134D RID: 4941
		private double cameraSpeed = 50.0;

		// Token: 0x0200029E RID: 670
		public enum ObjectEditorCameraType
		{
			// Token: 0x0400134F RID: 4943
			Slow,
			// Token: 0x04001350 RID: 4944
			Normal,
			// Token: 0x04001351 RID: 4945
			Custom
		}
	}
}
