using System;
using System.ComponentModel;

namespace MapEditor.Map.MapObjects
{
	// Token: 0x020001D6 RID: 470
	public class ScriptAreaDataConverter : ExpandableObjectConverter
	{
		// Token: 0x060017FF RID: 6143 RVA: 0x000A1B4D File Offset: 0x000A0B4D
		public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
		{
			return destinationType == typeof(CylinderScriptAreaData) || base.CanConvertTo(context, destinationType);
		}
	}
}
