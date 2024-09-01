using System;
using System.ComponentModel;

namespace MapEditor.Map.MapObjects
{
	// Token: 0x02000143 RID: 323
	internal class SpawnPointDataConverter : ExpandableObjectConverter
	{
		// Token: 0x06000FBE RID: 4030 RVA: 0x0007A3DC File Offset: 0x000793DC
		public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
		{
			return destinationType == typeof(GuardSpawnPointData) || destinationType == typeof(CircleSpawnPointData) || destinationType == typeof(EllipseSpawnPointData) || destinationType == typeof(PatrolSpawnPointData) || destinationType == typeof(SpawnCircleSpawnPointData) || base.CanConvertTo(context, destinationType);
		}
	}
}
