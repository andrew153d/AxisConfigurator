using System;
using System.Windows.Media;
using System.Globalization;
using System.Windows.Data;
using static AxisConfigurator.Views.LedView;
using System.Web;
namespace AxisConfigurator.Converters
{
    public class ColorToHueConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is Color color)
            {
                return (int)HSV.FromRGB(color).Hue;
            }
            throw new InvalidOperationException("The target must be a Color.");
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public class ColorToSaturationConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is Color color)
            {
                return (int)HSV.FromRGB(color).Saturation;
            }
            throw new InvalidOperationException("The target must be a Color.");
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public class ColorToValueConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is Color color)
            {
                return (int)HSV.FromRGB(color).Value;
            }
            throw new InvalidOperationException("The target must be a Color.");
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value;
            //throw new NotImplementedException();
        }
    }

    public class ColorToRedConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is Color color)
            {
                return color.R;
            }
            throw new InvalidOperationException("The target must be a Color.");
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is int redComponent)
            {
                // Ensure the redComponent value is within valid byte range
                if (redComponent < 0 || redComponent > 255)
                {
                    throw new ArgumentOutOfRangeException(nameof(value), "Red component must be between 0 and 255.");
                }

                // Create a color with the specified red component and default values for green, blue, and alpha
                return Color.FromArgb(255, (byte)redComponent, 0, 0);
            }
            throw new InvalidOperationException("The value must be an integer representing the red component.");
        }
    }

    public class ColorToGreenConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is Color color)
            {
                return color.G;
            }
            throw new InvalidOperationException("The target must be a Color.");
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public class ColorToBlueConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is Color color)
            {
                return color.B;
            }
            throw new InvalidOperationException("The target must be a Color.");
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public class ColorToHueOnlyColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is Color color)
            {
                var newHSVColor = HSV.FromRGB(color);
                newHSVColor.Value = 255;
                newHSVColor.Saturation = 255;

                return HSV.ToColor(newHSVColor);
            }
            throw new InvalidOperationException("The target must be a Color.");
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public class ColorToHexStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is Color color)
            {
                string hex = color.ToString();

                return hex;
            }
            throw new InvalidOperationException("The target must be a Color.");
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is string hex && !string.IsNullOrWhiteSpace(hex))
            {
                
                // Ensure the string starts with '#' and is in the correct format
                if (hex.StartsWith("#") && (hex.Length == 7 || hex.Length == 9))
                {
                    // If the string has 9 characters, it includes alpha
                    if (hex.Length == 9)
                    {
                        // Parse ARGB
                        
                        byte a = byte.Parse(hex.Substring(1, 2));
                        byte r = byte.Parse(hex.Substring(3, 2));
                        byte g = byte.Parse(hex.Substring(5, 2));
                        byte b = byte.Parse(hex.Substring(7, 2));
                        return Color.FromArgb(a, r, g, b);
                    }
                    else
                    {
                        // Parse RGB
                        byte r = byte.Parse(hex.Substring(1, 2));
                        byte g = byte.Parse(hex.Substring(3, 2));
                        byte b = byte.Parse(hex.Substring(5, 2));
                        return Color.FromRgb(r, g, b);
                    }
                }
            }
            // Return Transparent if the input is invalid
            return Colors.Transparent;
        }
    }

    public class ColorToHueSliderConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is Color color) { 
                var colodr = HSV.FromRGB(color);
                var hue = colodr.Hue;
                int ret = (int)(hue*200/360);
                return ret;
            }
            throw new InvalidOperationException("The target must be a Color.");
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
    public class ColorToSaturationSelectorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is Color color)
            {
                HSV colodr = HSV.FromRGB(color);
                float hue = colodr.Saturation;
                int ret = (int)(hue * 200 / 255) - 5;
                return ret;
            }
            throw new InvalidOperationException("The target must be a Color.");
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
    public class ColorToValueSelectorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is Color color)
            {
                HSV colodr = HSV.FromRGB(color);
                float val = colodr.Value;
                int ret = 200-(int)(val * 200 / 255)-5;
                return ret;
            }
            throw new InvalidOperationException("The target must be a Color.");
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}