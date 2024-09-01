using System;
using System.IO;
using MapEditor.Resources.Strings;
using Tools.Geometry;
using Tools.MapObjects;
using Tools.Progress;

namespace MapEditor.Map.SaveLoad.DataSources
{
	// Token: 0x020001F1 RID: 497
	internal class StartPositionDataSource : SaveLoad.IDataSource
	{
		// Token: 0x060018D9 RID: 6361 RVA: 0x000A52EA File Offset: 0x000A42EA
		public int GetProgressSteps(bool forSave)
		{
			return 1;
		}

		// Token: 0x060018DA RID: 6362 RVA: 0x000A52F0 File Offset: 0x000A42F0
		public bool Save(MapEditorMap map, MainForm.Context context, IProgressContainer progressContainer)
		{
			if (progressContainer != null)
			{
				progressContainer.AddString(Strings.SAVING_START_POSITION);
			}
			string fileName = EditorEnvironment.DataFolder + "Maps/start.txt";
			CameraPlacement placement;
			context.EditorScene.GetPlacement(context.EditorSceneViewID, out placement);
			Position position = placement.Position;
			if (Math.Abs(position.Z) > (double)MathConsts.FLOAT_EPSILON)
			{
				Quat quat = new Quat(placement.Rotation);
				Vec3 direction = quat.Rotate(new Vec3(1.0, 0.0, 0.0));
				position += position.Z * direction;
			}
			Directory.CreateDirectory(EditorEnvironment.DataFolder + "Maps/");
			using (StreamWriter file = new StreamWriter(fileName))
			{
				file.WriteLine("{0}", map.Data.MapResourceName);
				file.WriteLine("{0}", map.Data.MinXMinYPatchCoords.X);
				file.WriteLine("{0}", map.Data.MinXMinYPatchCoords.Y);
				file.WriteLine("{0}", position.X - (double)(map.Data.MinXMinYPatchCoords.X * Constants.PatchSize));
				file.WriteLine("{0}", position.Y - (double)(map.Data.MinXMinYPatchCoords.Y * Constants.PatchSize));
			}
			if (progressContainer != null)
			{
				progressContainer.Progress++;
			}
			return true;
		}

		// Token: 0x060018DB RID: 6363 RVA: 0x000A54B0 File Offset: 0x000A44B0
		public bool Load(MapEditorMap map, MainForm.Context context, IProgressContainer progressContainer, out bool somethingCreated)
		{
			somethingCreated = false;
			if (progressContainer != null)
			{
				progressContainer.AddString(Strings.LOADING_START_POSITION);
			}
			if (progressContainer != null)
			{
				progressContainer.Progress++;
			}
			return true;
		}
	}
}
