using System;
using System.ComponentModel;
using System.Globalization;

namespace MapEditor.Map.MapObjects
{
	// Token: 0x0200016F RID: 367
	public class SoundConverter : ExpandableObjectConverter
	{
		// Token: 0x060011D9 RID: 4569 RVA: 0x00084357 File Offset: 0x00083357
		public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
		{
			return destinationType == typeof(ExtendedSound) || base.CanConvertTo(context, destinationType);
		}

		// Token: 0x060011DA RID: 4570 RVA: 0x00084370 File Offset: 0x00083370
		public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
		{
			if (destinationType == typeof(string) && value is ExtendedSound)
			{
				return value.ToString();
			}
			return base.ConvertTo(context, culture, value, destinationType);
		}

		// Token: 0x060011DB RID: 4571 RVA: 0x0008439A File Offset: 0x0008339A
		public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
		{
			return sourceType == typeof(string) || base.CanConvertFrom(context, sourceType);
		}

		// Token: 0x060011DC RID: 4572 RVA: 0x000843B4 File Offset: 0x000833B4
		public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
		{
			if (value is string)
			{
				try
				{
					return new Sound((string)value);
				}
				catch (SystemException)
				{
					return Sound.Empty;
				}
			}
			return base.ConvertFrom(context, culture, value);
		}
	}
}
