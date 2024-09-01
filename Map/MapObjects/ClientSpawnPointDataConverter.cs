using System;
using System.ComponentModel;

namespace MapEditor.Map.MapObjects
{
	// Token: 0x0200022B RID: 555
	internal class ClientSpawnPointDataConverter : ExpandableObjectConverter
	{
		// Token: 0x06001A99 RID: 6809 RVA: 0x000AEA57 File Offset: 0x000ADA57
		public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
		{
			return destinationType == typeof(MobClientSpawnPointData) || destinationType == typeof(DeviceClientSpawnPointData) || base.CanConvertTo(context, destinationType);
		}
	}
}
