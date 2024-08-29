using AxisConfigurator.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace AxisConfigurator.Views
{
    /// <summary>
    /// Interaction logic for LedView.xaml
    /// </summary>
    public partial class LedView : UserControl
    {
        private readonly LedViewModel ledViewModel = new LedViewModel();
        public LedView()
        {
            InitializeComponent();
            InitializeLedTab();

            DataContext = ledViewModel;
        }

        
        private bool _isColorSelectorDragging;
        private bool _isHueSelectorDragging;

        private LedViewModel ViewModel => (LedViewModel)DataContext;
            
        //brushes
        LinearGradientBrush hueSelectorGradient = new LinearGradientBrush();
        Color currentColor = Color.FromArgb(255, 0, 0, 0);

        private void InitializeLedTab()
        {
            //fill the gradient
            hueSelectorGradient.StartPoint = new Point(0, 0);
            hueSelectorGradient.EndPoint = new Point(1, 0);

            // Add gradient stops
            for (int i = 0; i <= 360; i++)
            {
                float hue = (float)i;
                var color = HSV.ToColor(new HSV(hue, 255, 255)); // Full saturation and value
                hueSelectorGradient.GradientStops.Add(new GradientStop(color, (float)i / 360f));
            }

            HueSelectorCanvas.Background = hueSelectorGradient;
        }

        private void ColorSelector_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            // Start dragging
            _isColorSelectorDragging = true;
            DraggableCircle.CaptureMouse(); // Capture mouse to ensure it stays within the control
            var position = e.GetPosition(DrawingCanvas);
        }

        private void ColorSelector_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            // End dragging
            _isColorSelectorDragging = false;
            DraggableCircle.ReleaseMouseCapture();
        }

        private void ColorSelector_MouseMove(object sender, MouseEventArgs e)
        {
            if (_isColorSelectorDragging)
            {
                // Get the current mouse position
                Point currentPoint = e.GetPosition(DrawingCanvas);

                // Update circle's position so that its center snaps to the mouse position
                double radius = 5;
                double newLeft = currentPoint.X - radius;
                double newTop = currentPoint.Y - radius;

                // Set the new position of the circle
                //if (currentPoint.X < DrawingCanvas.Width && currentPoint.X > 0)
                    //Canvas.SetLeft(DraggableCircle, newLeft);
                //if (currentPoint.Y < DrawingCanvas.Height && currentPoint.Y > 0)
                    //Canvas.SetTop(DraggableCircle, newTop);
                var position = e.GetPosition(DrawingCanvas);
                ViewModel.DragColorSelectorCommand.Execute(position);
            }
        }

        private void HueSelector_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            // Start dragging
            _isHueSelectorDragging = true;
            DraggableBox.CaptureMouse(); // Capture mouse to ensure it stays within the control
        }

        private void HueSelector_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            // End dragging
            _isHueSelectorDragging = false;
            DraggableBox.ReleaseMouseCapture();
        }

        private void HueSelector_MouseMove(object sender, MouseEventArgs e)
        {
            if (_isHueSelectorDragging)
            {
                // Get the current mouse position
                Point currentPoint = e.GetPosition(HueSelectorCanvas);

                // Update circle's position so that its center snaps to the mouse position
                double radius = 5;
                double newLeft = currentPoint.X - radius;

                // Set the new position of the circle
                if (currentPoint.X > HueSelectorCanvas.Width || currentPoint.X < 0)
                    return;
                ViewModel.DragHueSelectorCommand.Execute(newLeft);
                //Canvas.SetLeft(DraggableBox, newLeft);
            }
        }
    }
}
