using AxisConfigurator.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Formats.Asn1;
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
        public ObservableCollection<string> MotionMode
        {
            get { return motionModel.MotionMode; }
        }


        //public ControlType controlType;
        public float TargetVelocity
        {
            get => motionModel.TargetVelocity;
            set
            {
                motionModel.TargetVelocity = value;
                OnPropertyChanged(nameof(TargetVelocity));
            }
        }
        public float TargetPosition
        {
            get => motionModel.TargetPosition;
            set
            {
                motionModel.TargetPosition = value;
                OnPropertyChanged(nameof(TargetPosition));
            }
        }
        public float TargetTorque
        {
            get => motionModel.TargetTorque;
            set
            {
                motionModel.TargetTorque = value;
                OnPropertyChanged(nameof(TargetTorque));
            }
        }

        public float CurrentVelocity
        {
            get => motionModel.CurrentVelocity;
        }
        public float CurrentPosition
        {
            get => motionModel.CurrentPosition;
        }
        public float CurrentTorque
        {
            get => motionModel.CurrentTorque;
        }

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
