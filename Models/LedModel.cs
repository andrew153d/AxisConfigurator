using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Media;
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

        private Color ledColor = Color.FromArgb(255, 0, 0, 0);

        private Mode ledMode = Mode.OFF;

        public Color LedColor { get { return ledColor; } set {  ledColor = value; OnPropertyChanged(nameof(LedColor)); } }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }


    }
}
