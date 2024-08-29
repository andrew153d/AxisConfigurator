using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Media;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AxisConfigurator.Models;
using System.Windows.Input;
using System.Windows;
using System.Diagnostics;
namespace AxisConfigurator.ViewModels
{
    public class LedViewModel : INotifyPropertyChanged
    {
        public LedModel ledModel;
        public LedViewModel() 
        {
            ledModel = new LedModel();
            //StartColorSelectorDragCommand = new RelayCommand<object>(OnStartDrag);
            DragColorSelectorCommand = new RelayCommand<object>(OnColorSelectorDrag);
            DragHueSelectorCommand = new RelayCommand<object>(OnHueSelectorDrag);
            //EndDragCommand = new RelayCommand<object>(OnEndDrag);
        }
        // Convert Drawing.Color to Media.Color for the getter
        public System.Windows.Media.Color LedColor
        {
            get
            {
                return ledModel.LedColor;
            }
            set
            {
                ledModel.LedColor = value;
                OnPropertyChanged(nameof(LedColor));
                OnPropertyChanged(nameof(Color_R));
                OnPropertyChanged(nameof(Color_G));
                OnPropertyChanged(nameof(Color_B));
            }
        }

        public string Color_R
        {
            get => ledModel.LedColor.R.ToString();
            set
            {
                if (byte.TryParse(value, out byte r_val) && ledModel.LedColor.R != r_val)
                {
                    LedColor = Color.FromArgb(ledModel.LedColor.A, r_val, ledModel.LedColor.G, ledModel.LedColor.B);
                    OnPropertyChanged(nameof(LedColor));
                    OnPropertyChanged(nameof(Color_R));
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
                    LedColor = Color.FromArgb(ledModel.LedColor.A, ledModel.LedColor.R, g_val, ledModel.LedColor.B);
                    OnPropertyChanged(nameof(LedColor));
                    OnPropertyChanged(nameof(Color_G));
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
                    LedColor = Color.FromArgb(ledModel.LedColor.A, ledModel.LedColor.R, ledModel.LedColor.G, b_val);
                    OnPropertyChanged(nameof(LedColor));
                    OnPropertyChanged(nameof(Color_B));
                }
            }
        }
        

        //public ICommand StartColorSelectorDragCommand { get; private set; }
        public ICommand DragColorSelectorCommand { get; private set; }
        public ICommand DragHueSelectorCommand {  get; private set; }
        //public ICommand EndDragCommand { get; private set; }

        private void OnColorSelectorDrag(object currentPoint)
        {
            var point = currentPoint as Point?;
            if (point.HasValue)
            {
                // Update the Red variable based on the X position of the mouse
                var hsv_color = HSV.FromRGB(LedColor);
                if(point.Value.X<=200 && point.Value.X >=0)
                    hsv_color.Saturation = (float)(point.Value.X*255.0/200.0);
                if (point.Value.Y <= 200 && point.Value.Y >= 0)
                    hsv_color.Value = 255 - (float)point.Value.Y*255/200;
                LedColor = HSV.ToColor(hsv_color);
            }
        }
        private void OnHueSelectorDrag(object currentPoint)
        {
            var point = currentPoint as double?;
            if (point.HasValue)
            {
                // Update the Red variable based on the X position of the mouse
                HSV hsvColor = HSV.FromRGB(LedColor);
                hsvColor.Hue = (float)((point.Value)*360/200);
                LedColor = HSV.ToColor(hsvColor);
            }
        }

        //private void OnEndDrag(object endPoint)
        //{
        //    _isColorSelectorDragging = false;
        //}


        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

    }
    public class RelayCommand<T> : ICommand where T : class
    {
        #region Fields 
        readonly Action<T> _execute;
        readonly Predicate<T> _canExecute;
        #endregion // Fields 
        #region Constructors 
        public RelayCommand(Action<T> execute) : this(execute, null) { }
        public RelayCommand(Action<T> execute, Predicate<T> canExecute)
        {
            if (execute == null)
                throw new ArgumentNullException("execute");
            _execute = execute; _canExecute = canExecute;
        }
        #endregion // Constructors 
        #region ICommand Members 
        [DebuggerStepThrough]
        public bool CanExecute(object parameter)
        {
            return _canExecute == null ? true : _canExecute((T)parameter);
        }
        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }
        public void Execute(object parameter) { _execute((T)parameter); }
        #endregion // ICommand Members 
    }
}
