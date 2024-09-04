using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Configuration6
{
    [TypeConverter(typeof(PointTypeConverter))]
    public class Point
    {
        public double X { get; set; }
        public double Y { get; set; }
    }

    public class PointTypeConverter : TypeConverter
    {
        public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
            => sourceType == typeof(string);

        public override object ConvertFrom(ITypeDescriptorContext context, System.Globalization.CultureInfo culture, object value)
        {
            string[] split = value.ToString().Split(',');
            double x = double.Parse(split[0].Trim().TrimStart('('));
            double y = double.Parse(split[1].Trim().TrimEnd(')'));
            return new Point { X = x, Y = y };
        }
    }
}

