using System;
using Tools.Landscape;
using Tools.Landscape.LandscapeToolParams;

namespace MapEditor.Map.Landscape
{
	// Token: 0x02000235 RID: 565
	public class HeightPickerLandscapeTool : LandscapeTool
	{
		// Token: 0x06001B0C RID: 6924 RVA: 0x000B00C0 File Offset: 0x000AF0C0
		public HeightPickerLandscapeTool(int _id) : base(_id)
		{
			base.Temporary = true;
		}

		// Token: 0x06001B0D RID: 6925 RVA: 0x000B00D0 File Offset: 0x000AF0D0
		public override void Create()
		{
		}

		// Token: 0x06001B0E RID: 6926 RVA: 0x000B00D4 File Offset: 0x000AF0D4
		public override void Apply()
		{
			if (base.LandscapeToolContext != null)
			{
				HeightPickerLandscapeToolParams heightPickerLandscapeToolParams = base.LandscapeToolParams as HeightPickerLandscapeToolParams;
				if (heightPickerLandscapeToolParams != null)
				{
					double height = base.LandscapeToolContext.LandscapeToolContextPosition.CollisionMapPosition.Z;
					if (heightPickerLandscapeToolParams.Relative)
					{
						if (!this.startHeightFilled)
						{
							this.startHeight = height;
							this.startHeightFilled = true;
						}
						heightPickerLandscapeToolParams.Height = height - this.startHeight;
					}
					else
					{
						heightPickerLandscapeToolParams.Height = height;
					}
					heightPickerLandscapeToolParams.InvokeLandscapeToolHeightCallback();
				}
			}
		}

		// Token: 0x06001B0F RID: 6927 RVA: 0x000B014C File Offset: 0x000AF14C
		public override void Destroy()
		{
		}

		// Token: 0x04001150 RID: 4432
		private double startHeight;

		// Token: 0x04001151 RID: 4433
		private bool startHeightFilled;
	}
}
