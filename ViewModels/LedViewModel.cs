using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AxisConfigurator.Models;
namespace AxisConfigurator.ViewModels
{
    public class LedViewModel
    {
        public LedModel ledModel;
        public LedViewModel(LedModel model) 
        { 
            ledModel = model;
        }
    }
}
