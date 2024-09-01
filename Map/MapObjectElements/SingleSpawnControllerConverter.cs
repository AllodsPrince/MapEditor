using System;
using System.ComponentModel;
using System.Globalization;

namespace MapEditor.Map.MapObjectElements
{
	// Token: 0x020002C9 RID: 713
	internal class SingleSpawnControllerConverter : ExpandableObjectConverter
	{
		// Token: 0x0600211C RID: 8476 RVA: 0x000D1F50 File Offset: 0x000D0F50
		public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
		{
			return destinationType == typeof(SingleSpawnController) || destinationType == typeof(string) || base.CanConvertTo(context, destinationType);
		}

		// Token: 0x0600211D RID: 8477 RVA: 0x000D1F76 File Offset: 0x000D0F76
		public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
		{
			if (destinationType == typeof(string))
			{
				if (value is SingleSpawnController)
				{
					return value.GetType().Name;
				}
				if (value == null)
				{
					return "null";
				}
			}
			return base.ConvertTo(context, culture, value, destinationType);
		}

		// Token: 0x0600211E RID: 8478 RVA: 0x000D1FAE File Offset: 0x000D0FAE
		public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
		{
			return sourceType == typeof(string) || base.CanConvertFrom(context, sourceType);
		}

		// Token: 0x0600211F RID: 8479 RVA: 0x000D1FC8 File Offset: 0x000D0FC8
		public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
		{
			if (!(value is string))
			{
				return base.ConvertFrom(context, culture, value);
			}
			string _value = (string)value;
			if (_value != "null")
			{
				return new SingleSpawnController();
			}
			return null;
		}

		// Token: 0x06002120 RID: 8480 RVA: 0x000D2002 File Offset: 0x000D1002
		public override bool GetStandardValuesSupported(ITypeDescriptorContext context)
		{
			return true;
		}

		// Token: 0x06002121 RID: 8481 RVA: 0x000D2005 File Offset: 0x000D1005
		public override bool GetStandardValuesExclusive(ITypeDescriptorContext context)
		{
			return true;
		}

		// Token: 0x06002122 RID: 8482 RVA: 0x000D2008 File Offset: 0x000D1008
		public override TypeConverter.StandardValuesCollection GetStandardValues(ITypeDescriptorContext context)
		{
			SingleSpawnControllerOwner owner = context.Instance as SingleSpawnControllerOwner;
			string[] values;
			if (owner != null && owner.ValidSingleSpawnControllerOwnerInstance())
			{
				values = new string[]
				{
					"null",
					typeof(SingleSpawnController).Name
				};
			}
			else
			{
				values = new string[]
				{
					"null"
				};
			}
			return new TypeConverter.StandardValuesCollection(values);
		}

		// Token: 0x04001417 RID: 5143
		private const string nullValue = "null";
	}
}
