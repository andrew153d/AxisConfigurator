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
            //this.ledViewModel = ledViewModel;
            //this.DataContext = ledViewModel;
        }
        // ------- LED Page ------- //
        private bool _isColorSelectorDragging;
        private bool _isHueSelectorDragging;
        //brushes
        LinearGradientBrush hueSelectorGradient = new LinearGradientBrush();
        ImageBrush colorPickerImageBrush = new ImageBrush();
        Color currentColor = Color.FromArgb(255, 0, 0, 0);
        private void InitializeLedTab()
        {
            //fill the gradient
            hueSelectorGradient.StartPoint = new Point(0, 0);
            hueSelectorGradient.EndPoint = new Point(1, 0);

            // Add gradient stops
            for (int i = 0; i <= 255; i++)
            {
                float hue = (float)i / 255f;
                var color = HsbToRgb(hue, 1, 1); // Full saturation and value
                hueSelectorGradient.GradientStops.Add(new GradientStop(color, (float)i / 255f));
            }

            LoadColorPickerBackground(RgbToHsv(currentColor.R, currentColor.G, currentColor.B).Hue);
            DrawGradient();
        }

        private void LoadColorPickerBackground(float hue)
        {
            int width = (int)200;
            int height = (int)200;

            // Create a visual to draw on
            DrawingVisual drawingVisual = new DrawingVisual();
            using (DrawingContext dc = drawingVisual.RenderOpen())
            {
                int step = 3;
                // Loop through each pixel and set color based on saturation and brightness
                for (int y = 0; y < height; y += step)
                {
                    float brightness = 1.0f - (float)y / height; // Brightness decreases from top to bottom
                    for (int x = 0; x < width; x += step)
                    {
                        float saturation = (float)x / width; // Saturation increases from left to right

                        // Convert HSB to RGB
                        Color color = HsbToRgb((float)hue, saturation, brightness); // Fixed hue (e.g., 0.5 for greenish)

                        // Draw the pixel
                        dc.DrawRectangle(new SolidColorBrush(color), null, new Rect(x, y, step, step));
                    }
                }
            }

            // Create a RenderTargetBitmap to display the visual
            RenderTargetBitmap bitmap = new RenderTargetBitmap(width, height, 96, 96, PixelFormats.Pbgra32);
            bitmap.Render(drawingVisual);

            // Set the bitmap as the content of the canvas
            colorPickerImageBrush = new ImageBrush(bitmap);
        }

        private void UpdateRGBTextBoxes(byte R, byte G, byte B)
        {
            RGBR_TextBox.Text = R.ToString();
            RGBG_TextBox.Text = G.ToString();
            RGBB_TextBox.Text = B.ToString();
        }

        private void UpdateHSVTextBoxes(byte H, byte S, byte V)
        {
            HSVH_TextBox.Text = H.ToString();
            HSVS_TextBox.Text = S.ToString();
            HSVV_TextBox.Text = V.ToString();
        }
        private void UpdateHexTextBoxes(byte R, byte G, byte B)
        {
            string text = "#" + R.ToString() + G.ToString() + B.ToString();
            HEX_TextBox.Text = text;
        }
        private void UpdateColorDisplayTextBoxes(byte R, byte G, byte B)
        {
            ColorDisplay.Fill = new SolidColorBrush(Color.FromArgb(255, R, G, B));

        }

        public void OnLedTabSelected()
        {
            // Ensure the canvas is properly initialized
            if (DrawingCanvas.ActualWidth > 0 && DrawingCanvas.ActualHeight > 0)
            {
                DrawGradient();
            }
            else
            {
                // Register an event handler to call DrawGradient after layout is updated
                DrawingCanvas.SizeChanged += GradientCanvas_SizeChanged;
            }
        }

        private void GradientCanvas_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            // Ensure the canvas size is valid before calling DrawGradient
            if (DrawingCanvas.ActualWidth > 0 && DrawingCanvas.ActualHeight > 0)
            {
                DrawGradient();
                // Unsubscribe to avoid redundant calls
                DrawingCanvas.SizeChanged -= GradientCanvas_SizeChanged;
            }
        }

        private void ColorSelector_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            // Start dragging
            _isColorSelectorDragging = true;
            DraggableCircle.CaptureMouse(); // Capture mouse to ensure it stays within the control
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
                if (currentPoint.X < DrawingCanvas.Width && currentPoint.X > 0)
                    Canvas.SetLeft(DraggableCircle, newLeft);
                if (currentPoint.Y < DrawingCanvas.Height && currentPoint.Y > 0)
                    Canvas.SetTop(DraggableCircle, newTop);
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

                Canvas.SetLeft(DraggableBox, newLeft);
                LoadColorPickerBackground(((float)currentPoint.X / (float)HueSelectorCanvas.Width));
                DrawGradient();
            }
        }

        private void DrawGradient()
        {

            DrawingCanvas.Background = colorPickerImageBrush;
            HueSelectorCanvas.Background = hueSelectorGradient;

        }

        private Color HsbToRgb(float hue, float saturation, float brightness)
        {
            if (saturation == 0)
            {
                // If saturation is 0, return brightness value (grayscale)
                int gray = (int)(brightness * 255);
                return Color.FromArgb(255, (byte)gray, (byte)gray, (byte)gray);
            }

            float h = hue * 6.0f;
            int i = (int)Math.Floor(h);
            float f = h - i;
            float p = brightness * (1 - saturation);
            float q = brightness * (1 - saturation * f);
            float t = brightness * (1 - saturation * (1 - f));

            float r = 0, g = 0, b = 0;
            switch (i % 6)
            {
                case 0: r = brightness; g = t; b = p; break;
                case 1: r = q; g = brightness; b = p; break;
                case 2: r = p; g = brightness; b = t; break;
                case 3: r = p; g = q; b = brightness; break;
                case 4: r = t; g = p; b = brightness; break;
                case 5: r = brightness; g = p; b = q; break;
            }

            return Color.FromArgb(255, (byte)(r * 255), (byte)(g * 255), (byte)(b * 255));
        }

        public class HSV
        {
            public float Hue { get; set; }
            public float Saturation { get; set; }
            public float Value { get; set; }

            public HSV(float hue, float saturation, float value)
            {
                Hue = hue;
                Saturation = saturation;
                Value = value;
            }

            // Optional: Override ToString() for easy debugging
            public override string ToString()
            {
                return $"Hue: {Hue:F2}°, Saturation: {Saturation:P0}, Value: {Value:P0}";
            }
        }

        public static HSV RgbToHsv(byte r, byte g, byte b)
        {
            // Convert RGB values from [0, 255] range to [0.0, 1.0] range
            double rNorm = r / 255.0;
            double gNorm = g / 255.0;
            double bNorm = b / 255.0;

            // Find the maximum and minimum values among the normalized RGB values
            double max = Math.Max(rNorm, Math.Max(gNorm, bNorm));
            double min = Math.Min(rNorm, Math.Min(gNorm, bNorm));
            double delta = max - min;

            // Calculate Hue
            double h = 0;
            if (delta != 0)
            {
                if (max == rNorm)
                {
                    h = (gNorm - bNorm) / delta;
                }
                else if (max == gNorm)
                {
                    h = 2 + (bNorm - rNorm) / delta;
                }
                else
                {
                    h = 4 + (rNorm - gNorm) / delta;
                }
                h *= 60; // Convert to degrees
                if (h < 0)
                {
                    h += 360;
                }
            }

            // Calculate Saturation
            double s = (max == 0) ? 0 : delta / max;

            // Calculate Value
            double v = max;

            return new HSV((float)h, (float)s, (float)v);
        }
    }
}
