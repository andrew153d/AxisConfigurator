using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AxisConfigurator.Models
{
    public class LedModel : INotifyPropertyChanged
    {
        enum Mode
        {
            SOLID,
            ERROR,
            OFF
        }

        private Color ledColor = Color.Black;
        private Mode ledMode = Mode.OFF;

        public event PropertyChangedEventHandler? PropertyChanged;

        public Color LedColor { get { return ledColor; } set {  ledColor = value; OnPropertyChanged(nameof(LedColor)); } }
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }


    }
}
