using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace AxisConfigurator.Models
{
    public class MotionModel : INotifyPropertyChanged
    {
        public struct MotionFrame
        {
            byte control;
            float Target;
            long time;
        }

        public ObservableCollection<string> MotionMode;
        public float TargetVelocity;
        public float TargetPosition;
        public float TargetTorque;

        public float CurrentVelocity;
        public float CurrentPosition;
        public float CurrentTorque;

        public float VelocityP;
        public float VelocityI;
        public float VelocityD;

        public ObservableCollection<MotionFrame> MotionFrames;

        public MotionModel() 
        { 
            MotionFrames = new ObservableCollection<MotionFrame>();
            MotionMode = new ObservableCollection<string> {"OFF","POSITION","VELOCITY","TORQUE"};
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
