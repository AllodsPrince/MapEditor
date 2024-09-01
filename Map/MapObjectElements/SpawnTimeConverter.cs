using System;
using System.ComponentModel;
using System.Globalization;

namespace MapEditor.Map.MapObjectElements
{
	// Token: 0x0200028C RID: 652
	internal class SpawnTimeConverter : ExpandableObjectConverter
	{
		// Token: 0x06001EB0 RID: 7856 RVA: 0x000C6517 File Offset: 0x000C5517
		public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
		{
			return destinationType.BaseType == typeof(SpawnTimeAbstract) || destinationType == typeof(SpawnTimeAbstract) || base.CanConvertTo(context, destinationType);
		}

		// Token: 0x06001EB1 RID: 7857 RVA: 0x000C6542 File Offset: 0x000C5542
		public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
		{
			if (destinationType == typeof(string))
			{
				if (value is SpawnTimeAbstract)
				{
					return value.GetType().Name;
				}
				if (value == null)
				{
					return "Empty";
				}
			}
			return base.ConvertTo(context, culture, value, destinationType);
		}

		// Token: 0x06001EB2 RID: 7858 RVA: 0x000C657A File Offset: 0x000C557A
		public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
		{
			return sourceType == typeof(string) || base.CanConvertFrom(context, sourceType);
		}

		// Token: 0x06001EB3 RID: 7859 RVA: 0x000C6594 File Offset: 0x000C5594
		public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
		{
			if (value is string)
			{
				SpawnTimeAbstract spawnTime = SpawnTimeAbstract.Create((string)value);
				SpawnTimeOwner spawnTimeOwner = context.Instance as SpawnTimeOwner;
				if (spawnTimeOwner != null)
				{
					spawnTimeOwner.CheckSpawnTimeRareId(spawnTime);
				}
				return spawnTime;
			}
			return base.ConvertFrom(context, culture, value);
		}

		// Token: 0x06001EB4 RID: 7860 RVA: 0x000C65D6 File Offset: 0x000C55D6
		public override bool GetStandardValuesSupported(ITypeDescriptorContext context)
		{
			return true;
		}

		// Token: 0x06001EB5 RID: 7861 RVA: 0x000C65D9 File Offset: 0x000C55D9
		public override bool GetStandardValuesExclusive(ITypeDescriptorContext context)
		{
			return true;
		}

		// Token: 0x06001EB6 RID: 7862 RVA: 0x000C65DC File Offset: 0x000C55DC
		public override TypeConverter.StandardValuesCollection GetStandardValues(ITypeDescriptorContext context)
		{
			SpawnTimeOwner spawnTimeOwner = context.Instance as SpawnTimeOwner;
			string[] values;
			if (spawnTimeOwner != null && spawnTimeOwner.ValidSpawnTimeOwnerInstance())
			{
				string[] _values = SpawnTimeAbstract.GetTypes();
				values = new string[_values.Length + 1];
				values[0] = "Empty";
				_values.CopyTo(values, 1);
			}
			else
			{
				values = new string[]
				{
					"Empty"
				};
			}
			return new TypeConverter.StandardValuesCollection(values);
		}

		// Token: 0x04001327 RID: 4903
		private const string nullValue = "Empty";
	}
}
