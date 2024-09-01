using AxisConfigurator.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AxisConfigurator.ViewModels
{
    public class MotionViewModel : INotifyPropertyChanged
    {

        public MotionModel motionModel;

        public MotionViewModel()
        {
            motionModel = new MotionModel();
        }

        //public ControlType controlType;
        public float TargetVelocity;
        public float TargetPosition;
        public float TargetTorque;

        public float CurrentVelocity;
        public float CurrentPosition;
        public float CurrentTorque;

        public float VelocityP;
        public float VelocityI;
        public float VelocityD;




        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
