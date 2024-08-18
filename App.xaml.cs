using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace AxisConfigurator
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            //globals
            //var history = new ObservableCollection<string>();

            //create models
            //var model = new ConverterModel(s => s.ToUpper(), history);
            var ledModel = new Models.LedModel();

            //create view model, pass it the model  
            //var converterPresenter = new ConverterPresenter(model, history);
            var ledViewModel = new ViewModels.LedViewModel(ledModel);
            //create main window and set its datacontext
            //var mainWindow = new ConvertWindow {DataContext = converterPresenter};
            var ledView = new Views.LedView();
            var mainWindow = new MainWindow(ledView);
            //show the main window
            mainWindow.Show();
        }
    }
}
