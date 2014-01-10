using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Data;
using System.Diagnostics;

namespace UIControls.Converters
{
    public class StringToDoubleConverter : IValueConverter
    {
        public int Precision { get; set; }

        public bool Invert { get; set; }

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            try
            {
                if (Invert)
                {
                    string s = value.ToString();
                    double d;
                    if (double.TryParse(s, out d))
                    {
                        return d;
                    }
                    else
                    {
                        return double.NaN;
                    }
                }
                else
                {
                    if (value != null)
                    {
                        double d = (double)value;
                        if (Precision > 0 && Precision < 10)
                        {
                            return string.Format("{0:f" + Precision + "}", d);
                        }
                        else
                        {
                            return string.Format("{0:f2}", d);
                        }
                    }
                    else
                    {
                        return null;
                    }
                }
            }
            catch
            {
                Debug.WriteLine("String2Double: wrong input type");
                return null;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (!Invert)
            {
                string s = value.ToString();
                double d;
                if (double.TryParse(s, out d))
                {
                    return d;
                }
                else
                {
                    return double.NaN;
                }
            }
            else
            {
                if (value != null)
                {
                    double d = (double)value;
                    if (Precision > 0 && Precision < 10)
                    {
                        return string.Format("{0:f" + Precision + "}", d);
                    }
                    else
                    {
                        return string.Format("{0:f2}", d);
                    }
                }
                else
                {
                    return null;
                }
            }
        }
    }
}
