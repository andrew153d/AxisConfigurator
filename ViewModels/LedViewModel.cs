using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AxisConfigurator.Models;
namespace AxisConfigurator.ViewModels
{
    public class LedViewModel
    {
        public LedModel ledModel;
        public LedViewModel() 
        {
            ledModel = new LedModel();
        }
        public Color LedColor
        {
            get => ledModel.LedColor;
            set
            {
                if (ledModel.LedColor != value)
                {
                    ledModel.LedColor = value;
                    OnPropertyChanged(nameof(LedColor));
                }
            }
        }
        public string Color_R
        {
            get => ledModel.LedColor.R.ToString();
            set
            {
                if (byte.TryParse(value, out byte r_val) && ledModel.LedColor.R != r_val)
                {
                    ledModel.LedColor = Color.FromArgb(ledModel.LedColor.A, r_val, ledModel.LedColor.G, ledModel.LedColor.B);
                }
            }
        }

        public string Color_G
        {
            get => ledModel.LedColor.G.ToString();
            set
            {
                if (byte.TryParse(value, out byte g_val) && ledModel.LedColor.G != g_val)
                {
                    ledModel.LedColor = Color.FromArgb(ledModel.LedColor.A, ledModel.LedColor.R, g_val, ledModel.LedColor.B);
                }
            }
        }

        public string Color_B
        {
            get => ledModel.LedColor.B.ToString();
            set
            {
                if (byte.TryParse(value, out byte b_val) && ledModel.LedColor.B != b_val)
                {
                    ledModel.LedColor = Color.FromArgb(ledModel.LedColor.A, ledModel.LedColor.R, ledModel.LedColor.G, b_val);
                }
            }
        }

         public event PropertyChangedEventHandler PropertyChanged;

    protected virtual void OnPropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    }
}
